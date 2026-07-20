using System.Text;

namespace SpireAgentOffice.Helpers;

/// <summary>
/// Helper class for logging AI execution results and periodic cleanup of old log/temp files.
/// Logs are saved to bin/Debug/net10.0/log/, and cleanup runs every 7 days for files older than 7 days
/// in both the log directory and .office_use_tmp directory.
/// </summary>
public static class LogHelper
{
    private static readonly object _lock = new();
    private static bool _cleanupInitialized;
    private static Timer? _cleanupTimer;
    private static readonly TimeSpan CleanupInterval = TimeSpan.FromDays(7);
    private static readonly TimeSpan FileRetentionPeriod = TimeSpan.FromDays(7);

    /// <summary>
    /// Gets the log directory path under the app's base directory.
    /// Equivalent to bin/Debug/net10.0/log/
    /// </summary>
    public static string LogDirectory => Path.Combine(AppContext.BaseDirectory, "log");

    /// <summary>
    /// Gets the temporary directory path under the app's base directory.
    /// Equivalent to bin/Debug/net10.0/.office_use_tmp
    /// </summary>
    public static string OfficeTempDirectory => Path.Combine(AppContext.BaseDirectory, ".office_use_tmp");

    /// <summary>
    /// Write an AI execution result to the daily log file.
    /// </summary>
    /// <param name="aiResult">The AI execution result (success/failure with token usage).</param>
    /// <param name="taskName">Task identifier used as the log file name (e.g., "PdfAI", "WordAI").</param>
    public static void WriteLog(dynamic? aiResult, string taskName)
    {
        EnsureCleanupInitialized();

        string logFilePath = Path.Combine(LogDirectory, $"{taskName}.txt");
        string? logDir = Path.GetDirectoryName(logFilePath);
        if (!string.IsNullOrEmpty(logDir) && !Directory.Exists(logDir))
            Directory.CreateDirectory(logDir);

        var logBuilder = new StringBuilder();

        // Determine execution status: Success/Failure/Skipped
        string status = aiResult == null ? "SKIPPED" :
            aiResult.Success ? "SUCCESS" : $"FAILED: {aiResult.ErrorMessage}";

        logBuilder.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{taskName}] {status}");

        if (aiResult != null)
        {
            // Log execution duration
            logBuilder.AppendLine($" | Duration: {aiResult.Duration.TotalSeconds:F2}s");

            // Log token usage statistics
            var tu = aiResult.TokenUsage;
            if (tu != null)
            {
                logBuilder.Append($" | In: {tu.InputTokens:N0}");           // Input tokens
                logBuilder.Append($" | Out: {tu.OutputTokens:N0}");         // Output tokens
                logBuilder.Append($" | CacheR: {tu.CacheReadTokens:N0}");   // Cache read tokens
                logBuilder.Append($" | CacheW: {tu.CacheWriteTokens:N0}");  // Cache write tokens
                logBuilder.Append($" | CacheT: {tu.TotalCacheTokens:N0}");  // Total cache tokens
                logBuilder.Append($" | Total: {tu.TotalTokens:N0}");        // Total tokens
            }
        }

        logBuilder.AppendLine();
        File.AppendAllText(logFilePath, logBuilder.ToString());
    }

    /// <summary>
    /// Cleans up files older than 7 days from both the log and temp directories.
    /// </summary>
    public static void CleanupOldFiles()
    {
        string[] directoriesToClean = [LogDirectory, OfficeTempDirectory];
        DateTime cutoff = DateTime.Now - FileRetentionPeriod;

        foreach (string dir in directoriesToClean)
        {
            if (!Directory.Exists(dir)) continue;

            try
            {
                foreach (string file in Directory.GetFiles(dir))
                {
                    try
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.LastWriteTime < cutoff || fi.CreationTime < cutoff)
                        {
                            fi.Delete();
                        }
                    }
                    catch
                    {
                        // Skip files that can't be accessed or deleted
                    }
                }

                // Also remove empty subdirectories
                foreach (string subDir in Directory.GetDirectories(dir))
                {
                    try
                    {
                        if (Directory.GetFileSystemEntries(subDir).Length == 0)
                        {
                            Directory.Delete(subDir);
                        }
                    }
                    catch
                    {
                        // Skip directories that can't be accessed or deleted
                    }
                }
            }
            catch
            {
                // Skip directories that can't be accessed
            }
        }
    }

    /// <summary>
    /// Ensures the cleanup timer is initialized once on first use.
    /// </summary>
    private static void EnsureCleanupInitialized()
    {
        if (_cleanupInitialized) return;

        lock (_lock)
        {
            if (_cleanupInitialized) return;

            // Run initial cleanup on first use
            CleanupOldFiles();

            // Schedule periodic cleanup every 7 days
            _cleanupTimer = new Timer(
                _ => CleanupOldFiles(),
                null,
                CleanupInterval,   // First fire after 7 days
                CleanupInterval    // Then repeat every 7 days
            );

            _cleanupInitialized = true;
        }
    }
}
