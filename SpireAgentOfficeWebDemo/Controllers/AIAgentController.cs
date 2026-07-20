using SpireAgentOffice.Models;
using SpireAgentOffice.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SpireAgentOffice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AIAgentController : ControllerBase
{
    private readonly SpireXlsService _excelService;
    private readonly SpireWordService _wordService;
    private readonly SpirePdfService _pdfService;
    private readonly SpirePptService _pptService;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public AIAgentController(
        SpireXlsService excelService,
        SpireWordService wordService,
        SpirePdfService pdfService,
        SpirePptService pptService,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
        _excelService = excelService;
        _wordService = wordService;
        _pdfService = pdfService;
        _pptService = pptService;
        _configuration = configuration;
        _env = env;
    }

    #region Excel

    /// <summary>
    /// Process the preset Sample.xlsx Excel document using a natural language instruction,
    /// returning the file byte stream for direct frontend download.
    /// </summary>
    [HttpPost("executeXls")]
    [ProducesResponseType(typeof(FileResult), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Execute([FromBody] AIExecuteRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Instruction))
            return BadRequest(new { error = "Instruction cannot be empty." });
        if (string.IsNullOrWhiteSpace(request.SaveFormat))
            return BadRequest(new { error = "SaveFormat cannot be empty." });

        string saveFormat = request.SaveFormat;

        try
        {
            byte[] resultBytes = await _excelService.ExecuteAsync(request.Instruction, saveFormat, request.ApiKey);
            string mimeType = DocumentFormatHelper.GetMimeType(saveFormat);
            string fileName = $"ai_output_{DateTime.Now:yyyyMMddHHmmss}{DocumentFormatHelper.GetExtension(saveFormat)}";

            return File(resultBytes, mimeType, fileName);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
        }
    }

    #endregion

    #region Word

    /// <summary>
    /// Process the preset Sample.docx Word document using a natural language instruction,
    /// returning the file byte stream for direct frontend download.
    /// </summary>
    [HttpPost("executeDoc")]
    [ProducesResponseType(typeof(FileResult), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> ExecuteDoc([FromBody] AIExecuteRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Instruction))
            return BadRequest(new { error = "Instruction cannot be empty." });
        if (string.IsNullOrWhiteSpace(request.SaveFormat))
            return BadRequest(new { error = "SaveFormat cannot be empty." });

        string saveFormat = request.SaveFormat;

        try
        {
            byte[] resultBytes = await _wordService.ExecuteAsync(request.Instruction, saveFormat, request.ApiKey);
            string mimeType = DocumentFormatHelper.GetMimeType(saveFormat);
            string fileName = $"word_output_{DateTime.Now:yyyyMMddHHmmss}{DocumentFormatHelper.GetExtension(saveFormat)}";

            return File(resultBytes, mimeType, fileName);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
        }
    }

    #endregion

    #region PDF

    /// <summary>
    /// Process the preset Sample.pdf document using a natural language instruction,
    /// returning the file byte stream for direct frontend download.
    /// </summary>
    [HttpPost("executePdf")]
    [ProducesResponseType(typeof(FileResult), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> ExecutePdf([FromBody] AIExecuteRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Instruction))
            return BadRequest(new { error = "Instruction cannot be empty." });
        if (string.IsNullOrWhiteSpace(request.SaveFormat))
            return BadRequest(new { error = "SaveFormat cannot be empty." });

        string saveFormat = request.SaveFormat;

        try
        {
            byte[] resultBytes = await _pdfService.ExecuteAsync(request.Instruction, saveFormat, request.ApiKey);
            string mimeType = DocumentFormatHelper.GetMimeType(saveFormat);
            string fileName = $"pdf_output_{DateTime.Now:yyyyMMddHHmmss}{DocumentFormatHelper.GetExtension(saveFormat)}";

            return File(resultBytes, mimeType, fileName);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
        }
    }

    #endregion

    #region PowerPoint

    /// <summary>
    /// Process the preset Sample.pptx PowerPoint document using a natural language instruction,
    /// returning the file byte stream for direct frontend download.
    /// </summary>
    [HttpPost("executePpt")]
    [ProducesResponseType(typeof(FileResult), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> ExecutePpt([FromBody] AIExecuteRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Instruction))
            return BadRequest(new { error = "Instruction cannot be empty." });
        if (string.IsNullOrWhiteSpace(request.SaveFormat))
            return BadRequest(new { error = "SaveFormat cannot be empty." });

        string saveFormat = request.SaveFormat;

        try
        {
            byte[] resultBytes = await _pptService.ExecuteAsync(request.Instruction, saveFormat, request.ApiKey);
            string mimeType = DocumentFormatHelper.GetMimeType(saveFormat);
            string fileName = $"ppt_output_{DateTime.Now:yyyyMMddHHmmss}{DocumentFormatHelper.GetExtension(saveFormat)}";

            return File(resultBytes, mimeType, fileName);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
        }
    }

    #endregion

    #region Key Management

    /// <summary>
    /// Check whether a SpireToken key is configured in appsettings.json.
    /// </summary>
    [HttpGet("key-status")]
    public IActionResult KeyStatus()
    {
        bool hasKey = !string.IsNullOrEmpty(_configuration["SpireAI:ApiKey"]);
        return Ok(new { configured = hasKey });
    }

    /// <summary>
    /// Save the SpireToken key to appsettings.json.
    /// </summary>
    [HttpPost("save-key")]
    public async Task<IActionResult> SaveKey([FromBody] SaveKeyRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.ApiKey))
            return BadRequest(new { error = "SpireToken Key cannot be empty." });

        string appSettingsPath = Path.Combine(_env.ContentRootPath, "appsettings.json");

        try
        {
            string json = await System.IO.File.ReadAllTextAsync(appSettingsPath);
            var node = JsonNode.Parse(json);
            if (node == null)
                return StatusCode(500, new { error = "Failed to parse appsettings.json" });

            node["SpireAI"] ??= new JsonObject();
            node["SpireAI"]!["ApiKey"] = request.ApiKey;

            var options = new JsonSerializerOptions { WriteIndented = true };
            await System.IO.File.WriteAllTextAsync(appSettingsPath, node.ToJsonString(options));

            return Ok(new { success = true, message = "SpireToken Key saved successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Failed to save key: {ex.Message}" });
        }
    }

    #endregion

    #region Health

    /// <summary>
    /// Health check endpoint.
    /// </summary>
    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
    }

    #endregion
}
