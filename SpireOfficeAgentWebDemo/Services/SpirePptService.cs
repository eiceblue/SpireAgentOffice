using Spire.Presentation;
using Spire.Agent.Office.AI;
using Spire.Agent.Office.Extensions;
using SpireAgentOffice.Helpers;
using SpireAgentOffice.Models;

namespace SpireAgentOffice.Services;

/// <summary>
/// Service for AI-powered PowerPoint document processing using Spire AI Agent.
/// Processes the preset Sample.pptx based on natural language instructions.
/// </summary>
public class SpirePptService
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public SpirePptService(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    /// <summary>
    /// Execute a natural language instruction and return the document as byte array.
    /// </summary>
    public async Task<byte[]> ExecuteAsync(string instruction, string saveFormat, string? apiKey = null)
    {
        string key = apiKey ?? _configuration["SpireAI:ApiKey"] ?? throw new InvalidOperationException(
            "Spire AI API key is not configured. Set SpireAI:ApiKey in appsettings.json or pass apiKey.");

        string tempFile = Path.Combine(Path.GetTempPath(), $"ai_ppt_{Guid.NewGuid():N}{DocumentFormatHelper.GetExtension(saveFormat)}");

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
    /// Core AI processing logic for PowerPoint -- shared by both return methods.
    /// </summary>
    private void ProcessWithAI(string instruction, string saveFormat, string apiKey, string outputPath)
    {
        var _sw = System.Diagnostics.Stopwatch.StartNew();
        string _timingFile = Path.Combine(LogHelper.LogDirectory, "PptAI_Timing.txt");
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

        string inputPath = Path.Combine(_env.ContentRootPath, "Data", "Sample.pptx");
        _LogTiming("CheckFileExists");

        AIOptions options = new AIOptions
        {
            SpireToken = apiKey,
            TimeoutMs = 300000
        };

        using (Presentation ppt = new Presentation())
        {
            _LogTiming("NewPresentation");

            // Load the preset document if it exists; otherwise use an empty Presentation
            if (File.Exists(inputPath))
            {
                ppt.LoadFromFile(inputPath);
            }
            _LogTiming("LoadFromFile");

            AIDocumentProcessor processor = ppt.AI(options);
            _LogTiming("CreateAIProcessor");

            AIResult result = processor.ExecuteInstruction(ppt, instruction, outputPath);
            _LogTiming("AIExecuteInstruction");

            // Log AI execution result (success or failure) with token usage
            LogHelper.WriteLog(result, "PptAI");
            _LogTiming("WriteLog");

            if (result == null || !result.Success)
            {
                throw new InvalidOperationException(
                    $"AI instruction execution failed: {result?.ErrorMessage ?? "Unknown error"}");
            }
        }
    }
}
