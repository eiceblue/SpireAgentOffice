namespace SpireAgentOffice.Models;

/// <summary>
/// Request model for saving the API key to appsettings.json.
/// </summary>
public class SaveKeyRequest
{
    /// <summary>
    /// The Spire AI API key to save.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
}
