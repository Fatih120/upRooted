namespace Uprooted;

internal class UprootedSettings
{
    public bool Enabled { get; set; } = true;
    public string Version { get; set; } = "0.4.2";
    public string ActiveTheme { get; set; } = "default-dark";
    public Dictionary<string, bool> Plugins { get; set; } = new();
    public string CustomCss { get; set; } = "";
    public string CustomAccent { get; set; } = "#3B6AF8";
    public string CustomBackground { get; set; } = "#0D1521";
    public string CustomText { get; set; } = "";           // Empty = auto-derive from bg
    public string CustomSvgMode { get; set; } = "auto";    // "auto", "light", or "dark"
    public bool NsfwFilterEnabled { get; set; } = false;
    public string NsfwApiKey { get; set; } = "";
    public double NsfwThreshold { get; set; } = 0.6;

    // MessageLogger settings
    public bool MessageLoggerLogDeletes { get; set; } = true;
    public bool MessageLoggerLogEdits { get; set; } = true;
    public bool MessageLoggerIgnoreSelf { get; set; } = false;
    public int MessageLoggerMaxMessages { get; set; } = 10000;

    // LinkEmbeds settings
    public bool LinkEmbedsShowFilenames { get; set; } = false;

    // Show experimental (TestingStatus=0) plugins on the Plugins page
    public bool ShowExperimentalPlugins { get; set; } = false;

    // Custom ping/reply highlight color (empty = use theme default)
    public string CustomPingColor { get; set; } = "";

    // Auto-update settings
    public bool AutoUpdateEnabled { get; set; } = true;
    public bool AutoUpdateNotify { get; set; } = true;
    public string AutoUpdateChannel { get; set; } = "stable";
    public string LastUpdateCheck { get; set; } = "";
    public string PendingUpdateVersion { get; set; } = "";
    // SHA-256 of the last applied .uprpkg — used to detect same-version hotfixes
    public string LastPackageHash { get; set; } = "";

    // Migration flag: tracks whether we've migrated from enabled-by-default to disabled-by-default plugins
    public bool PluginDefaultsMigrated { get; set; } = false;

    private static string? _settingsPath;

    // Time-based cache: avoid re-reading from disk on every 500ms timer tick
    private static UprootedSettings? _cached;
    private static DateTime _cachedAt;
    private static readonly TimeSpan CacheTtl = TimeSpan.FromSeconds(10);

    private static string GetSettingsPath()
    {
        if (_settingsPath != null) return _settingsPath;
        try
        {
            var profileDir = PlatformPaths.GetProfileDir();
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
        // Return cached instance if still fresh (avoids disk I/O on every 500ms timer tick)
        if (_cached != null && DateTime.UtcNow - _cachedAt < CacheTtl)
            return _cached;

        var settings = new UprootedSettings();
        var path = GetSettingsPath();
        if (!File.Exists(path))
        {
            // New install: mark migration as done (no legacy plugins to preserve)
            settings.PluginDefaultsMigrated = true;
            _cached = settings;
            _cachedAt = DateTime.UtcNow;
            return settings;
        }
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
                    case "Version": settings.Version = val; break;
                    case "CustomCss": settings.CustomCss = val; break;
                    case "CustomAccent": settings.CustomAccent = val; break;
                    case "CustomBackground": settings.CustomBackground = val; break;
                    case "CustomText": settings.CustomText = val; break;
                    case "CustomSvgMode": settings.CustomSvgMode = val; break;
                    case "NsfwFilterEnabled": settings.NsfwFilterEnabled = val == "true"; break;
                    case "NsfwApiKey": settings.NsfwApiKey = val; break;
                    case "NsfwThreshold":
                        if (double.TryParse(val, System.Globalization.NumberStyles.Float,
                            System.Globalization.CultureInfo.InvariantCulture, out var threshold))
                            settings.NsfwThreshold = threshold;
                        break;
                    case "MessageLogger.LogDeletes": settings.MessageLoggerLogDeletes = val == "true"; break;
                    case "MessageLogger.LogEdits": settings.MessageLoggerLogEdits = val == "true"; break;
                    case "MessageLogger.IgnoreSelf": settings.MessageLoggerIgnoreSelf = val == "true"; break;
                    case "MessageLogger.MaxMessages":
                        if (int.TryParse(val, out var maxMsg) && maxMsg > 0)
                            settings.MessageLoggerMaxMessages = maxMsg;
                        break;
                    case "LinkEmbeds.ShowFilenames": settings.LinkEmbedsShowFilenames = val == "true"; break;
                    case "ShowExperimentalPlugins": settings.ShowExperimentalPlugins = val == "true"; break;
                    case "CustomPingColor": settings.CustomPingColor = val; break;
                    case "AutoUpdate.Enabled": settings.AutoUpdateEnabled = val == "true"; break;
                    case "AutoUpdate.Notify": settings.AutoUpdateNotify = val == "true"; break;
                    case "AutoUpdate.Channel": settings.AutoUpdateChannel = val; break;
                    case "AutoUpdate.LastCheck": settings.LastUpdateCheck = val; break;
                    case "AutoUpdate.PendingVersion": settings.PendingUpdateVersion = val; break;
                    case "AutoUpdate.LastPackageHash": settings.LastPackageHash = val; break;
                    case "Migrated.PluginDefaults": settings.PluginDefaultsMigrated = val == "true"; break;
                    case var k when k.StartsWith("Plugin."):
                        var pluginName = k["Plugin.".Length..];
                        settings.Plugins[pluginName] = val == "true";
                        break;
                }
            }
            Logger.Log("Settings", $"Loaded settings from {path}: ActiveTheme={settings.ActiveTheme}");
        }
        catch (Exception ex)
        {
            Logger.Log("Settings", $"Failed to load settings: {ex.Message}");
        }

        // One-time migration: existing users upgrading from enabled-by-default plugins.
        // Explicitly enable all legacy plugins that don't have an explicit entry,
        // preserving the user's previous experience where everything was on.
        if (!settings.PluginDefaultsMigrated)
        {
            string[] legacyEnabled = { "sentry-blocker", "themes", "settings-panel", "link-embeds", "clear-urls", "message-logger" };
            foreach (var name in legacyEnabled)
            {
                if (!settings.Plugins.ContainsKey(name))
                    settings.Plugins[name] = true;
            }
            settings.PluginDefaultsMigrated = true;
            try
            {
                settings.Save();
                Logger.Log("Settings", "Migration: preserved legacy plugin defaults (all enabled), flag saved");
            }
            catch (Exception ex)
            {
                Logger.Log("Settings", $"Migration save failed: {ex.Message}");
            }
        }

        _cached = settings;
        _cachedAt = DateTime.UtcNow;
        return settings;
    }

    /// <summary>
    /// Invalidate the settings cache so the next Load() re-reads from disk.
    /// Call after Save() or when settings are known to have changed externally.
    /// </summary>
    internal static void InvalidateCache()
    {
        _cached = null;
    }

    internal void Save()
    {
        try
        {
            var path = GetSettingsPath();
            var lines = new List<string>
            {
                "ActiveTheme=" + ActiveTheme,
                "Enabled=" + (Enabled ? "true" : "false"),
                "Version=" + Version,
                "CustomCss=" + CustomCss,
                "CustomAccent=" + CustomAccent,
                "CustomBackground=" + CustomBackground,
                "CustomText=" + CustomText,
                "CustomSvgMode=" + CustomSvgMode,
                "NsfwFilterEnabled=" + (NsfwFilterEnabled ? "true" : "false"),
                "NsfwApiKey=" + NsfwApiKey,
                "NsfwThreshold=" + NsfwThreshold.ToString(System.Globalization.CultureInfo.InvariantCulture),
                "MessageLogger.LogDeletes=" + (MessageLoggerLogDeletes ? "true" : "false"),
                "MessageLogger.LogEdits=" + (MessageLoggerLogEdits ? "true" : "false"),
                "MessageLogger.IgnoreSelf=" + (MessageLoggerIgnoreSelf ? "true" : "false"),
                "MessageLogger.MaxMessages=" + MessageLoggerMaxMessages,
                "LinkEmbeds.ShowFilenames=" + (LinkEmbedsShowFilenames ? "true" : "false"),
                "ShowExperimentalPlugins=" + (ShowExperimentalPlugins ? "true" : "false"),
                "CustomPingColor=" + CustomPingColor,
                "AutoUpdate.Enabled=" + (AutoUpdateEnabled ? "true" : "false"),
                "AutoUpdate.Notify=" + (AutoUpdateNotify ? "true" : "false"),
                "AutoUpdate.Channel=" + AutoUpdateChannel,
                "AutoUpdate.LastCheck=" + LastUpdateCheck,
                "AutoUpdate.PendingVersion=" + PendingUpdateVersion,
                "AutoUpdate.LastPackageHash=" + LastPackageHash,
                "Migrated.PluginDefaults=" + (PluginDefaultsMigrated ? "true" : "false")
            };
            foreach (var (name, enabled) in Plugins)
            {
                lines.Add($"Plugin.{name}={( enabled ? "true" : "false" )}");
            }
            File.WriteAllText(path, string.Join("\n", lines));
            InvalidateCache();
            Logger.Log("Settings", "Saved settings to " + path + ": ActiveTheme=" + ActiveTheme);
        }
        catch (Exception ex)
        {
            Logger.Log("Settings", "Failed to save settings: " + ex.Message);
        }
    }
}
