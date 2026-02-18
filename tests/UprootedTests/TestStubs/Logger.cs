namespace Uprooted;

// Stub: discards all log output — no disk I/O during tests
internal static class Logger
{
    internal static void Log(string message) { }
    internal static void Log(string category, string message) { }
    internal static void LogSeparator(int newlines = 3) { }
    internal static string GetLogPath() => "/dev/null";
    internal static void LogException(string category, string context, Exception ex) { }
}
