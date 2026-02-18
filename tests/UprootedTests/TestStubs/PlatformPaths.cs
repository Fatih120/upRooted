namespace Uprooted;

// Stub: returns a configurable temp directory — set ProfileDirOverride before each test
internal static class PlatformPaths
{
    internal static string ProfileDirOverride =
        Path.Combine(Path.GetTempPath(), $"uprooted-test-{Environment.ProcessId}");

    internal static string GetProfileDir() => ProfileDirOverride;
    internal static string GetUprootedDir() => ProfileDirOverride;
}
