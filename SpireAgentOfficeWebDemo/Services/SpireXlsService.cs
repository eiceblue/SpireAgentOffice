using Spire.Xls;
using Spire.Agent.Office.AI;
using Spire.Agent.Office.Extensions;
using SpireAgentOffice.Helpers;
using SpireAgentOffice.Models;
namespace SpireAgentOffice.Services;

/// <summary>
/// Service for AI-powered Excel document processing using Spire AI Agent.
/// Processes the preset Sample.xlsx based on natural language instructions,
/// supporting output formats: PDF, HTML, XLSX, XLS, CSV, etc.
/// </summary>
public class SpireXlsService
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public SpireXlsService(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    /// <summary>
    /// Execute a natural language instruction and return the document as byte array (file stream).
    /// </summary>
    public async Task<byte[]> ExecuteAsync(string instruction, string saveFormat, string? apiKey = null)
    {
        string key = apiKey ?? _configuration["SpireAI:ApiKey"] ?? throw new InvalidOperationException(
            "Spire AI API key is not configured. Set SpireAI:ApiKey in appsettings.json or pass apiKey.");

        string tempFile = Path.Combine(Path.GetTempPath(), $"ai_output_{Guid.NewGuid():N}{DocumentFormatHelper.GetExtension(saveFormat)}");

        try
        {
            ProcessWithAI(instruction, saveFormat, key, tempFile);
            return await File.ReadAllBytesAsync(tempFile);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    /// <summary>
    /// Core AI processing logic -- shared by both return methods.
    /// </summary>
    private void ProcessWithAI(string instruction, string saveFormat, string apiKey, string outputPath)
    {
        var _sw = System.Diagnostics.Stopwatch.StartNew();
        string _timingFile = Path.Combine(LogHelper.LogDirectory, "XlsAI_Timing.txt");
        Directory.CreateDirectory(LogHelper.LogDirectory);

        long _prevTicks = 0;
        void _LogTiming(string step)
        {
            long currentTicks = _sw.ElapsedTicks;
            double elapsedMs = (currentTicks - _prevTicks) * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
            _prevTicks = currentTicks;
            File.AppendAllText(_timingFile,
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{step}] +{elapsedMs:F1}ms{Environment.NewLine}");
        }

        _LogTiming("Start");

        string inputPath = Path.Combine(_env.ContentRootPath, "Data", "Sample.xlsx");

        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException($"Preset Excel file not found: {inputPath}");
        }
        _LogTiming("CheckFileExists");

        // Check file size
        var fileInfo = new FileInfo(inputPath);
        _LogTiming($"FileSize_{fileInfo.Length}");

        AIOptions options = new AIOptions {
            SpireToken = apiKey,
            TimeoutMs = 300000
        };

        using (Workbook workbook = new Workbook())
        {
            _LogTiming("NewWorkbook");

            workbook.LoadFromFile(inputPath);
            _LogTiming("LoadFromFile");

            AIDocumentProcessor processor = workbook.AI(options);
            _LogTiming("CreateAIProcessor");

            AIResult result = processor.ExecuteInstruction(workbook, instruction, outputPath);
            _LogTiming("AIExecuteInstruction");

            // Log AI execution result (success or failure) with token usage
            LogHelper.WriteLog(result, "XlsAI");
            _LogTiming("WriteLog");

            if (result == null || !result.Success)
            {
                throw new InvalidOperationException(
                    $"AI instruction execution failed: {result?.ErrorMessage ?? "Unknown error"}");
            }
        }
    }


}
