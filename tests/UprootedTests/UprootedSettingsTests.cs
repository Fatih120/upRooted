using System.Reflection;
using Xunit;

namespace Uprooted.Tests;

/// <summary>
/// Tests for UprootedSettings — INI file parsing, save/load roundtrip, cache, migration.
/// Uses a unique temp directory per test; resets static state via reflection.
/// </summary>
[Collection("SequentialTests")]
public class UprootedSettingsTests : IDisposable
{
    private readonly string _tempDir;
    private static readonly FieldInfo SettingsPathField =
        typeof(UprootedSettings).GetField("_settingsPath", BindingFlags.NonPublic | BindingFlags.Static)!;

    public UprootedSettingsTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), $"uprooted-settings-{Guid.NewGuid():N}");
        Directory.CreateDirectory(_tempDir);
        PlatformPaths.ProfileDirOverride = _tempDir;
        ResetStatics();
    }

    private static void ResetStatics()
    {
        SettingsPathField.SetValue(null, null);
        UprootedSettings.InvalidateCache();
    }

    public void Dispose()
    {
        ResetStatics();
        try { Directory.Delete(_tempDir, recursive: true); } catch { }
    }

    private string SettingsFilePath => Path.Combine(_tempDir, "uprooted-settings.ini");

    private void WriteSettings(params string[] lines) =>
        File.WriteAllText(SettingsFilePath, string.Join("\n", lines));

    // ── Default values when file is missing ──

    [Fact]
    public void DefaultValues_WhenFileMissing()
    {
        var s = UprootedSettings.Load();
        Assert.Equal("default-dark", s.ActiveTheme);
        Assert.True(s.Enabled);
        Assert.Equal("#3B6AF8", s.CustomAccent);
        Assert.Equal("#0D1521", s.CustomBackground);
        Assert.False(s.NsfwFilterEnabled);
        Assert.Equal(0.6, s.NsfwThreshold);
        Assert.Equal(10000, s.MessageLoggerMaxMessages);
        Assert.True(s.MessageLoggerLogDeletes);
        Assert.True(s.MessageLoggerLogEdits);
        Assert.False(s.MessageLoggerIgnoreSelf);
        Assert.True(s.AutoUpdateEnabled);
        Assert.Equal("stable", s.AutoUpdateChannel);
    }

    [Fact]
    public void NewInstall_PluginDefaultsMigratedIsTrue()
    {
        // New install with no file: migration flag set to avoid false "legacy" migration
        var s = UprootedSettings.Load();
        Assert.True(s.PluginDefaultsMigrated);
    }

    // ── Scalar key parsing ──

    [Fact]
    public void ActiveTheme_Parsed()
    {
        WriteSettings("ActiveTheme=monokai-dark");
        var s = UprootedSettings.Load();
        Assert.Equal("monokai-dark", s.ActiveTheme);
    }

    [Fact]
    public void Enabled_True_ParsedCorrectly()
    {
        WriteSettings("Enabled=true", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.True(s.Enabled);
    }

    [Fact]
    public void Enabled_False_ParsedCorrectly()
    {
        WriteSettings("Enabled=false", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.False(s.Enabled);
    }

    [Fact]
    public void Enabled_Garbage_DefaultsToFalse()
    {
        WriteSettings("Enabled=yes", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        // "yes" != "true" → false
        Assert.False(s.Enabled);
    }

    [Fact]
    public void NsfwThreshold_Float_ParsedWithInvariantCulture()
    {
        WriteSettings("NsfwThreshold=0.75", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.Equal(0.75, s.NsfwThreshold, precision: 10);
    }

    [Fact]
    public void NsfwThreshold_CommaSeparated_FailsAndUsesDefault()
    {
        // European locale comma — not valid with InvariantCulture
        WriteSettings("NsfwThreshold=0,75", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.Equal(0.6, s.NsfwThreshold, precision: 10);
    }

    [Fact]
    public void MessageLoggerMaxMessages_Valid_Parsed()
    {
        WriteSettings("MessageLogger.MaxMessages=5000", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.Equal(5000, s.MessageLoggerMaxMessages);
    }

    [Fact]
    public void MessageLoggerMaxMessages_Negative_UsesDefault()
    {
        WriteSettings("MessageLogger.MaxMessages=-1", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.Equal(10000, s.MessageLoggerMaxMessages);
    }

    [Fact]
    public void MessageLoggerMaxMessages_NotANumber_UsesDefault()
    {
        WriteSettings("MessageLogger.MaxMessages=abc", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.Equal(10000, s.MessageLoggerMaxMessages);
    }

    // ── Plugin dictionary ──

    [Fact]
    public void PluginEnabled_ParsedIntoDict()
    {
        WriteSettings("Plugin.sentry-blocker=true", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.True(s.Plugins.ContainsKey("sentry-blocker"));
        Assert.True(s.Plugins["sentry-blocker"]);
    }

    [Fact]
    public void PluginDisabled_ParsedIntoDict()
    {
        WriteSettings("Plugin.themes=false", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.True(s.Plugins.ContainsKey("themes"));
        Assert.False(s.Plugins["themes"]);
    }

    // ── Line parsing edge cases ──

    [Fact]
    public void LineWithNoEquals_IsSkipped()
    {
        WriteSettings("this line has no equals sign", "ActiveTheme=nord", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.Equal("nord", s.ActiveTheme);
    }

    [Fact]
    public void ValueWithEquals_FullValuePreserved()
    {
        // Only the first '=' splits key from value; remainder is part of value
        WriteSettings("CustomCss=body { color: red; } div { background: url(a=b); }", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.Equal("body { color: red; } div { background: url(a=b); }", s.CustomCss);
    }

    [Fact]
    public void AutoUpdateChannel_Parsed()
    {
        WriteSettings("AutoUpdate.Channel=dev", "Migrated.PluginDefaults=true");
        var s = UprootedSettings.Load();
        Assert.Equal("dev", s.AutoUpdateChannel);
    }

    // ── Save/Load roundtrip ──

    [Fact]
    public void SaveLoad_Roundtrip_PreservesAllFields()
    {
        // Load defaults (new install), mutate, save, reload, verify
        var s = UprootedSettings.Load();
        s.ActiveTheme = "monokai-dark";
        s.Enabled = false;
        s.CustomAccent = "#FF5500";
        s.NsfwThreshold = 0.85;
        s.MessageLoggerMaxMessages = 2500;
        s.AutoUpdateChannel = "dev";
        s.Plugins["sentry-blocker"] = true;
        s.Plugins["clear-urls"] = false;
        s.Save();

        ResetStatics();
        var s2 = UprootedSettings.Load();

        Assert.Equal("monokai-dark", s2.ActiveTheme);
        Assert.False(s2.Enabled);
        Assert.Equal("#FF5500", s2.CustomAccent);
        Assert.Equal(0.85, s2.NsfwThreshold, precision: 10);
        Assert.Equal(2500, s2.MessageLoggerMaxMessages);
        Assert.Equal("dev", s2.AutoUpdateChannel);
        Assert.True(s2.Plugins["sentry-blocker"]);
        Assert.False(s2.Plugins["clear-urls"]);
    }

    // ── Cache behavior ──

    [Fact]
    public void Cache_SameInstanceWithinTtl()
    {
        var s1 = UprootedSettings.Load();
        var s2 = UprootedSettings.Load();
        Assert.Same(s1, s2);
    }

    [Fact]
    public void InvalidateCache_CausesReloadFromDisk()
    {
        WriteSettings("ActiveTheme=first-theme", "Migrated.PluginDefaults=true");
        var s1 = UprootedSettings.Load();
        Assert.Equal("first-theme", s1.ActiveTheme);

        // Update file on disk
        WriteSettings("ActiveTheme=second-theme", "Migrated.PluginDefaults=true");
        UprootedSettings.InvalidateCache();

        var s2 = UprootedSettings.Load();
        Assert.Equal("second-theme", s2.ActiveTheme);
        Assert.NotSame(s1, s2);
    }

    // ── Migration ──

    [Fact]
    public void Migration_ExistingFileWithoutFlag_LegacyPluginsEnabled()
    {
        // File with no Migrated.PluginDefaults key — simulates pre-migration install
        WriteSettings("ActiveTheme=default-dark");
        var s = UprootedSettings.Load();

        // All 6 legacy plugins should be enabled
        Assert.True(s.Plugins.GetValueOrDefault("sentry-blocker"));
        Assert.True(s.Plugins.GetValueOrDefault("themes"));
        Assert.True(s.Plugins.GetValueOrDefault("settings-panel"));
        Assert.True(s.Plugins.GetValueOrDefault("link-embeds"));
        Assert.True(s.Plugins.GetValueOrDefault("clear-urls"));
        Assert.True(s.Plugins.GetValueOrDefault("message-logger"));
        Assert.True(s.PluginDefaultsMigrated);
    }

    [Fact]
    public void Migration_ExplicitlyDisabledPluginKept()
    {
        // User had explicitly disabled a plugin before migration
        WriteSettings("Plugin.sentry-blocker=false");
        var s = UprootedSettings.Load();

        // Explicit entry takes precedence over migration default
        Assert.False(s.Plugins["sentry-blocker"]);
        // Other legacy plugins still get enabled
        Assert.True(s.Plugins.GetValueOrDefault("themes"));
    }
}
