namespace Uprooted;

internal class UprootedSettings
{
    public bool Enabled { get; set; } = true;
    public string Version { get; set; } = "0.5.1-rc";
    public string ActiveTheme { get; set; } = "default-dark";
    public Dictionary<string, bool> Plugins { get; set; } = new(StringComparer.OrdinalIgnoreCase);
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
    public string PendingUpdateVersion { get; set; } = "0.5.1-rc";
    // SHA-256 of the last applied .uprpkg — used to detect same-version hotfixes
    public string LastPackageHash { get; set; } = "";

    // Migration flag: tracks whether we've migrated from enabled-by-default to disabled-by-default plugins
    public bool PluginDefaultsMigrated { get; set; } = false;

    // Translate plugin settings
    public bool   TranslateAutoTranslate { get; set; } = false;
    public bool   TranslateShowOriginal  { get; set; } = true;
    public string TranslateFromLang      { get; set; } = "auto"; // received from
    public string TranslateToLang        { get; set; } = "en";   // received to
    public string TranslateSendFromLang  { get; set; } = "auto"; // sent from
    public string TranslateSendToLang    { get; set; } = "en";   // sent to
    public string TranslateService                 { get; set; } = "google"; // "google"|"deepl"|"deepl-pro"
    public string TranslateDeeplApiKey             { get; set; } = "";
    public bool   TranslateShowAutoTranslateAlert  { get; set; } = true;

    // Focus Mode settings
    public bool FocusModeHideMedia { get; set; } = true;
    public bool FocusModeHideEmbeds { get; set; } = true;
    public bool FocusModeHideReactions { get; set; } = true;
    public bool FocusModeHideTyping { get; set; } = true;
    public bool FocusModeHideBadges { get; set; } = true;
    public bool FocusModeShowPlaceholders { get; set; } = true;

    // Rootcord settings
    public bool RootcordUseOriginalTabs { get; set; } = false;

    // User Bio settings
    public bool   UserBioViewOnly { get; set; } = false;
    public string UserBioText     { get; set; } = "";

    private static string? _settingsPath;

    // Time-based cache: avoid re-reading from disk on every 500ms timer tick
    private static UprootedSettings? _cached;
    private static DateTime _cachedAt;
    private static readonly TimeSpan CacheTtl = TimeSpan.FromSeconds(10);
    private static readonly object _saveLock = new();

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
        // Lock ensures _cached and _cachedAt are read/written atomically.
        // Without this, another thread could see a new _cached with a stale _cachedAt,
        // making the TTL check incorrect.
        lock (_saveLock)
        {
            var cached = _cached;
            if (cached != null && DateTime.UtcNow - _cachedAt < CacheTtl)
                return cached;
        }

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
                    case "Translate.AutoTranslate":  settings.TranslateAutoTranslate = val == "true"; break;
                    case "Translate.ShowOriginal":   settings.TranslateShowOriginal  = val == "true"; break;
                    case "Translate.FromLang":       settings.TranslateFromLang      = val; break;
                    case "Translate.ToLang":         settings.TranslateToLang        = val; break;
                    case "Translate.SendFromLang":   settings.TranslateSendFromLang  = val; break;
                    case "Translate.SendToLang":     settings.TranslateSendToLang    = val; break;
                    case "Translate.Service":        settings.TranslateService       = val; break;
                    case "Translate.DeeplApiKey":    settings.TranslateDeeplApiKey   = Uri.UnescapeDataString(val); break;
                    case "Translate.ShowAutoTranslateAlert": settings.TranslateShowAutoTranslateAlert = val == "true"; break;
                    case "FocusMode.HideMedia": settings.FocusModeHideMedia = val == "true"; break;
                    case "FocusMode.HideEmbeds": settings.FocusModeHideEmbeds = val == "true"; break;
                    case "FocusMode.HideReactions": settings.FocusModeHideReactions = val == "true"; break;
                    case "FocusMode.HideTyping": settings.FocusModeHideTyping = val == "true"; break;
                    case "FocusMode.HideBadges": settings.FocusModeHideBadges = val == "true"; break;
                    case "FocusMode.ShowPlaceholders": settings.FocusModeShowPlaceholders = val == "true"; break;
                    case "Rootcord.UseOriginalTabs": settings.RootcordUseOriginalTabs = val == "true"; break;
                    case "UserBio.ViewOnly": settings.UserBioViewOnly = val == "true"; break;
                    case "UserBio.Text":    settings.UserBioText = Uri.UnescapeDataString(val); break;
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

        lock (_saveLock)
        {
            _cached = settings;
            _cachedAt = DateTime.UtcNow;
        }
        return settings;
    }

    /// <summary>
    /// Invalidate the settings cache so the next Load() re-reads from disk.
    /// Call after Save() or when settings are known to have changed externally.
    /// </summary>
    internal static void InvalidateCache()
    {
        lock (_saveLock) { _cached = null; }
    }

    internal void Save()
    {
        // Serialize concurrent saves; write to a temp file then rename for atomicity
        // (prevents corruption if the process is killed mid-write).
        lock (_saveLock)
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
                    "Migrated.PluginDefaults=" + (PluginDefaultsMigrated ? "true" : "false"),
                    "Translate.AutoTranslate=" + (TranslateAutoTranslate ? "true" : "false"),
                    "Translate.ShowOriginal="  + (TranslateShowOriginal  ? "true" : "false"),
                    "Translate.FromLang="      + TranslateFromLang,
                    "Translate.ToLang="        + TranslateToLang,
                    "Translate.SendFromLang="  + TranslateSendFromLang,
                    "Translate.SendToLang="    + TranslateSendToLang,
                    "Translate.Service="       + TranslateService,
                    "Translate.DeeplApiKey="   + Uri.EscapeDataString(TranslateDeeplApiKey),
                    "Translate.ShowAutoTranslateAlert=" + (TranslateShowAutoTranslateAlert ? "true" : "false"),
                    "FocusMode.HideMedia=" + (FocusModeHideMedia ? "true" : "false"),
                    "FocusMode.HideEmbeds=" + (FocusModeHideEmbeds ? "true" : "false"),
                    "FocusMode.HideReactions=" + (FocusModeHideReactions ? "true" : "false"),
                    "FocusMode.HideTyping=" + (FocusModeHideTyping ? "true" : "false"),
                    "FocusMode.HideBadges=" + (FocusModeHideBadges ? "true" : "false"),
                    "FocusMode.ShowPlaceholders=" + (FocusModeShowPlaceholders ? "true" : "false"),
                    "Rootcord.UseOriginalTabs=" + (RootcordUseOriginalTabs ? "true" : "false"),
                    "UserBio.ViewOnly=" + (UserBioViewOnly ? "true" : "false"),
                    "UserBio.Text=" + Uri.EscapeDataString(UserBioText)
                };
                foreach (var (name, enabled) in Plugins)
                    lines.Add($"Plugin.{name}={( enabled ? "true" : "false" )}");

                // Atomic write: write to .tmp then replace, so a crash mid-write
                // never leaves the settings file partially written.
                var tmp = path + ".tmp";
                File.WriteAllText(tmp, string.Join("\n", lines));
                File.Move(tmp, path, overwrite: true);

                InvalidateCache();
                Logger.Log("Settings", "Saved settings to " + path + ": ActiveTheme=" + ActiveTheme);
            }
            catch (Exception ex)
            {
                Logger.Log("Settings", "Failed to save settings: " + ex.Message);
            }
        }
    }
}
