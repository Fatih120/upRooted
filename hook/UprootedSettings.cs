namespace Uprooted;

internal class UprootedSettings
{
    public bool Enabled { get; set; } = true;
    public string Version { get; set; } = "0.1.2";
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
            _settingsPath = Path.Combine(profileDir, "uprooted-settings.ini");
        }
        catch
        {
            _settingsPath = "uprooted-settings.ini";
        }
        return _settingsPath;
    }

    internal static UprootedSettings Load()
    {
        var settings = new UprootedSettings();
        var path = GetSettingsPath();
        if (!File.Exists(path)) return settings;
        try
        {
            foreach (var line in File.ReadAllLines(path))
            {
                var eq = line.IndexOf('=');
                if (eq < 0) continue;
                var key = line[..eq].Trim();
                var val = line[(eq + 1)..].Trim();
                switch (key)
                {
                    case "ActiveTheme": settings.ActiveTheme = val; break;
                    case "Enabled": settings.Enabled = val == "true"; break;
                    case "CustomCss": settings.CustomCss = val; break;
                }
            }
            Logger.Log("Settings", $"Loaded settings from {path}: ActiveTheme={settings.ActiveTheme}");
        }
        catch (Exception ex)
        {
            Logger.Log("Settings", $"Failed to load settings: {ex.Message}");
        }
        return settings;
    }

    internal void Save()
    {
        try
        {
            var path = GetSettingsPath();
            var content = string.Join("\n",
                "ActiveTheme=" + ActiveTheme,
                "Enabled=" + (Enabled ? "true" : "false"),
                "Version=" + Version,
                "CustomCss=" + CustomCss);
            File.WriteAllText(path, content);
            Logger.Log("Settings", "Saved settings to " + path + ": ActiveTheme=" + ActiveTheme);
        }
        catch (Exception ex)
        {
            Logger.Log("Settings", "Failed to save settings: " + ex.Message);
        }
    }
}
