namespace SpireAgentOffice.Models;

/// <summary>
/// Request model for executing AI document instructions
/// </summary>
public class AIExecuteRequest
{
    /// <summary>
    /// Natural language instruction for processing the preset document.
    /// </summary>
    public string Instruction { get; set; } = string.Empty;

    /// <summary>
    /// Output save format: "pdf", "html", "docx", "xlsx", "pptx" etc.
    /// Must be explicitly provided by the caller.
    /// </summary>
    public string? SaveFormat { get; set; }

    /// <summary>
    /// Optional API key. If provided, overrides the key in appsettings.json.
    /// </summary>
    public string? ApiKey { get; set; }
}
