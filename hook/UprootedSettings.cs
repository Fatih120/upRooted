namespace Uprooted;

internal class UprootedSettings
{
    public bool Enabled { get; set; } = true;
    public string Version { get; set; } = "0.1.0";
    public string ActiveTheme { get; set; } = "default-dark";
    public Dictionary<string, bool> Plugins { get; set; } = new();
    public string CustomCss { get; set; } = "";

    private static string? _settingsPath;

    private static string GetSettingsPath()
    {
        if (_settingsPath != null) return _settingsPath;
        try
        {
            var profileDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Root Communications", "Root", "profile", "default");
            _settingsPath = Path.Combine(profileDir, "uprooted-settings.json");
        }
        catch
        {
            _settingsPath = "uprooted-settings.json";
        }
        return _settingsPath;
    }

    internal static UprootedSettings Load()
    {
        // Skip file I/O and JSON for now - just use defaults
        // JSON deserialization causes MissingMethodException in profiler-injected context
        return new UprootedSettings();
    }

    internal void Save()
    {
        // TODO: persist settings once core injection is stable
    }
}
