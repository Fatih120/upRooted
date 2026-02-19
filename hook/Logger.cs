namespace Uprooted;

internal static class Logger
{
    private static readonly string LogPath;
    private static readonly object Lock = new();
    private static bool _enabled = true;

    static Logger()
    {
        var profileDir = PlatformPaths.GetProfileDir();
        Directory.CreateDirectory(profileDir);
        LogPath = Path.Combine(profileDir, "uprooted-hook.log");
    }

    /// <summary>Returns the full path to the hook log file.</summary>
    internal static string GetLogPath() => LogPath;

    /// <summary>Whether logging is active (developer channel only).</summary>
    internal static bool IsEnabled => _enabled;

    /// <summary>
    /// Disable all logging and delete the log file.
    /// Called on stable channel — only developer channel should produce logs.
    /// </summary>
    internal static void Disable()
    {
        _enabled = false;
        try
        {
            lock (Lock)
            {
                if (File.Exists(LogPath))
                    File.Delete(LogPath);
            }
        }
        catch { }
    }

    internal static void Log(string message)
    {
        if (!_enabled) return;
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
        if (!_enabled) return;
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
        if (!_enabled) return;
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
