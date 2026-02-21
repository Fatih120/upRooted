namespace Uprooted;

/// <summary>
/// Thread-safe file logger. Writes to uprooted-hook.log in Root's profile directory.
///
/// IMPORTANT: Logging is ALWAYS active for ALL users (stable and developer).
/// Do NOT add channel-gating, _enabled flags, or Disable() calls.
/// Without logs from stable users we cannot diagnose bug reports.
/// The developer channel only adds extra verbose/diagnostic output on top of this baseline.
///
/// Performance: log lines are buffered and flushed every 100ms (or when the buffer
/// exceeds 8 KB) to avoid hundreds of synchronous file open/write/close operations
/// during startup. AppDomain.ProcessExit flushes any unflushed lines.
/// </summary>
internal static class Logger
{
    private static readonly string LogPath;
    private static readonly object Lock = new();
    private static readonly System.Text.StringBuilder _buffer = new();

    /// <summary>
    /// Optional subscriber for live log lines (used by LogConsole).
    /// Called for every logged line including wide events.
    /// Set to null to unsubscribe. Single consumer — not thread-safe for multiple subscribers.
    /// </summary>
    internal static Action<string>? OnLine;
    private static readonly Timer _flushTimer;

    static Logger()
    {
        try
        {
            var profileDir = PlatformPaths.GetProfileDir();
            Directory.CreateDirectory(profileDir);
            LogPath = Path.Combine(profileDir, "uprooted-hook.log");
        }
        catch
        {
            // Fall back to the current working directory so an invalid profile path
            // doesn't throw TypeInitializationException and crash the entire hook.
            LogPath = Path.Combine(Path.GetTempPath(), "uprooted-hook.log");
        }

        // Flush buffered log lines to disk every 100ms.
        // This converts N individual file-open/write/close operations into one write per tick.
        _flushTimer = new Timer(_ => Flush(), null, 100, 100);

        // Guarantee final flush on clean shutdown.
        AppDomain.CurrentDomain.ProcessExit += (_, _) => Flush();
    }

    /// <summary>Returns the full path to the hook log file.</summary>
    internal static string GetLogPath() => LogPath;

    /// <summary>
    /// Flush buffered log lines to disk.
    /// Called automatically every 100ms and on process exit.
    /// </summary>
    internal static void Flush()
    {
        string? content = null;
        lock (Lock)
        {
            if (_buffer.Length == 0) return;
            content = _buffer.ToString();
            _buffer.Clear();
        }
        try { File.AppendAllText(LogPath, content); }
        catch { }
    }

    internal static void Log(string message)
    {
        string? eager = null;
        var formatted = $"[{DateTime.Now:HH:mm:ss.fff}] {message}";
        try { OnLine?.Invoke(formatted); } catch { }
        try
        {
            lock (Lock)
            {
                _buffer.Append(formatted);
                _buffer.Append('\n');
                // Eager flush when buffer is large so a crash doesn't lose many lines.
                if (_buffer.Length > 8192)
                {
                    eager = _buffer.ToString();
                    _buffer.Clear();
                }
            }
        }
        catch { }

        // Write outside the lock to avoid blocking other threads during file I/O.
        if (eager != null)
        {
            try { File.AppendAllText(LogPath, eager); }
            catch { }
        }
    }

    internal static void Log(string category, string message) => Log($"[{category}] {message}");

    /// <summary>Writes blank lines to the log as a visual separator (e.g. on startup).</summary>
    internal static void LogSeparator(int newlines = 3)
    {
        lock (Lock) { _buffer.Append(new string('\n', newlines)); }
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

    // ===== Wide event emission (called by WideEvent.Dispose/Finish) =====

    /// <summary>
    /// Emit a structured wide event line.
    /// Format: [HH:mm:ss.fff] [Category|operation] key=value key=value dur_ms=N
    /// </summary>
    internal static void EmitWideEvent(string category, string operation,
        List<KeyValuePair<string, string>> fields, int durationMs)
    {
        string? eager = null;
        try
        {
            var sb = new System.Text.StringBuilder(128);
            sb.Append($"[{DateTime.Now:HH:mm:ss.fff}] [{category}|{operation}]");

            foreach (var kv in fields)
            {
                sb.Append(' ');
                sb.Append(kv.Key);
                sb.Append('=');
                sb.Append(EscapeValue(kv.Value));
            }

            sb.Append(" dur_ms=");
            sb.Append(durationMs);

            try { OnLine?.Invoke(sb.ToString()); } catch { }
            sb.Append('\n');

            lock (Lock)
            {
                _buffer.Append(sb);
                if (_buffer.Length > 8192)
                {
                    eager = _buffer.ToString();
                    _buffer.Clear();
                }
            }
        }
        catch { }

        if (eager != null)
        {
            try { File.AppendAllText(LogPath, eager); }
            catch { }
        }
    }

    private static string EscapeValue(string value)
    {
        if (value.IndexOfAny(_escapeChars) < 0)
            return value;
        return value
            .Replace("\\", "\\\\")
            .Replace("\n", "\\n")
            .Replace("\r", "\\r");
    }

    private static readonly char[] _escapeChars = { '\\', '\n', '\r' };
}
