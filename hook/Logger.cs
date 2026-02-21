namespace Uprooted;

/// <summary>
/// Thread-safe file logger. Writes to uprooted-hook.log in Root's profile directory.
///
/// IMPORTANT: Logging is ALWAYS active for ALL users (stable and developer).
/// Do NOT add channel-gating, _enabled flags, or Disable() calls.
/// Without logs from stable users we cannot diagnose bug reports.
/// The developer channel only adds extra verbose/diagnostic output on top of this baseline.
/// </summary>
internal static class Logger
{
    private static readonly string LogPath;
    private static readonly object Lock = new();

    static Logger()
    {
        var profileDir = PlatformPaths.GetProfileDir();
        Directory.CreateDirectory(profileDir);
        LogPath = Path.Combine(profileDir, "uprooted-hook.log");
    }

    /// <summary>Returns the full path to the hook log file.</summary>
    internal static string GetLogPath() => LogPath;

    internal static void Log(string message)
    {
        try
        {
            lock (Lock)
            {
                File.AppendAllText(LogPath, $"[{DateTime.Now:HH:mm:ss.fff}] {message}\n");
            }
        }
        catch { }
    }

    internal static void Log(string category, string message) => Log($"[{category}] {message}");

    /// <summary>Writes blank lines to the log as a visual separator (e.g. on startup).</summary>
    internal static void LogSeparator(int newlines = 3)
    {
        try
        {
            lock (Lock)
            {
                File.AppendAllText(LogPath, new string('\n', newlines));
            }
        }
        catch { }
    }

    /// <summary>Log exception with full inner exception chain for debugging.</summary>
    internal static void LogException(string category, string context, Exception ex)
    {
        Log(category, $"{context}: {ex.GetType().Name}: {ex.Message}");
        var inner = ex.InnerException;
        int depth = 0;
        while (inner != null && depth < 5)
        {
            Log(category, $"  Inner[{depth}]: {inner.GetType().Name}: {inner.Message}");
            inner = inner.InnerException;
            depth++;
        }
        Log(category, $"  StackTrace: {ex.StackTrace}");
    }
}
