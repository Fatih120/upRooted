using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Uprooted;

/// <summary>
/// In-process auto-updater that checks the public GitHub releases for new versions,
/// downloads updated files, and overwrites them in-place. Changes take effect on
/// next Root restart.
///
/// Uses reflection-based HttpClient (same pattern as LinkEmbedEngine) to avoid
/// MissingMethodException in Root's trimmed single-file deployment.
/// </summary>
internal class AutoUpdater
{
    // Stable channel (public repo — tagged releases from main)
    private const string StableApiUrl = "https://api.github.com/repos/The-Uprooted-Project/uprooted/releases/latest";
    private const string StableDownloadBase = "https://github.com/The-Uprooted-Project/uprooted/releases/download";

    // Canary channel (public canary repo — bleeding edge builds from main)
    private const string CanaryApiUrl = "https://api.github.com/repos/The-Uprooted-Project/uprooted-canary/releases?per_page=1";
    private const string CanaryDownloadBase = "https://github.com/The-Uprooted-Project/uprooted-canary/releases/download";

    // Developer channel (private repo — builds from dev branch, invite/password only)
    // Use /releases?per_page=1 instead of /releases/latest — the latter only returns
    // non-prerelease releases, but dev-channel builds are published as pre-releases.
    private const string DevApiUrl = "https://api.github.com/repos/The-Uprooted-Project/uprooted-private/releases?per_page=1";
    private const string DevDownloadBase = "https://github.com/The-Uprooted-Project/uprooted-private/releases/download";

    private const int CheckIntervalMinutes = 1;
    private const int HttpTimeoutSeconds = 30;

    // SHA-256 hash of the developer channel password
    private const string DevPasswordHash = "81bb7db1c3e4c805aff0096621f2c5c910f522d8ffd4550e14d03ccbbeb99966";

    // XOR-encrypted GitHub PAT (read-only, scoped to The-Uprooted-Project/uprooted-private)
    private static readonly byte[] EncryptedPat =
    {
        0x0B, 0x47, 0xD9, 0x4B, 0x96, 0xA5, 0x7E, 0xFE, 0xFD, 0x7E, 0x20, 0xE8, 0x71, 0x69, 0x7B, 0xA6,
        0xA4, 0xD4, 0x02, 0x19, 0x03, 0x7D, 0xBB, 0x10, 0x2A, 0x1C, 0xFB, 0x66, 0x6A, 0xE4, 0x2E, 0x46,
        0x27, 0x27, 0x78, 0xDE, 0xF1, 0xDF, 0x65, 0xB3, 0x76, 0x9A, 0x76, 0xC4, 0x27, 0x03, 0x0A, 0xF0,
        0x68, 0x93, 0xF4, 0x86, 0x15, 0xF6, 0x1D, 0xB1, 0x67, 0x3C, 0x95, 0xE0, 0xCF, 0xB0, 0x4B, 0x3B,
        0xF7, 0x02, 0xDF, 0x96, 0x4B, 0x2D, 0x5E, 0x05, 0xB4, 0xF5, 0x7F, 0xD9, 0x22, 0x4E, 0x61, 0x59,
        0x73, 0x3C, 0x1A, 0x3B, 0x90, 0x84, 0xF4, 0x83, 0xD9, 0xE2, 0x8E, 0x58, 0xD9
    };
    private static readonly byte[] PatXorKey =
    {
        0x6C, 0x2E, 0xAD, 0x23, 0xE3, 0xC7, 0x21, 0x8E, 0x9C, 0x0A, 0x7F, 0xD9, 0x40, 0x2B, 0x32, 0xE7,
        0xE8, 0x93, 0x44, 0x48, 0x33, 0x2F, 0xD9, 0x58, 0x7F, 0x4A, 0xBC, 0x36, 0x5B, 0xA2, 0x4B, 0x24,
        0x41, 0x78, 0x4A, 0x9D, 0x81, 0xAA, 0x30, 0xF1, 0x31, 0xD9, 0x24, 0x8B, 0x17, 0x74, 0x45, 0xA7,
        0x2A, 0xAB, 0xAC, 0xB0, 0x50, 0xB9, 0x49, 0xD3, 0x26, 0x46, 0xD2, 0x8F, 0xA9, 0xF2, 0x3B, 0x79,
        0xAE, 0x6D, 0x87, 0xC5, 0x2C, 0x40, 0x04, 0x6B, 0xD7, 0x80, 0x06, 0x9A, 0x43, 0x02, 0x2C, 0x10,
        0x47, 0x7E, 0x4E, 0x6B, 0xDD, 0xC9, 0xAD, 0xC2, 0x98, 0x8F, 0xD9, 0x6E, 0xB5
    };

    // 64-byte master key for .uprpkg encryption (shared with scripts/pack-update.py)
    private static readonly byte[] PackageMasterKey =
    {
        0x76, 0xC3, 0x47, 0x44, 0xE3, 0xD1, 0x44, 0xA2, 0x4C, 0xCA, 0xA3, 0x95, 0x5C, 0x02, 0x62, 0xF4,
        0x26, 0x1D, 0x46, 0x30, 0x4F, 0xB3, 0x5D, 0x5A, 0x53, 0x5D, 0x71, 0x7B, 0x67, 0x71, 0x13, 0x71,
        0xD0, 0x92, 0x47, 0x07, 0x32, 0x06, 0x84, 0xA8, 0x9C, 0xF4, 0x06, 0x56, 0x9D, 0x1D, 0xC8, 0x0B,
        0xED, 0x62, 0x59, 0x79, 0xC8, 0x72, 0x20, 0xD5, 0x8C, 0x1B, 0xA0, 0xE1, 0xA4, 0xE5, 0x84, 0x0B
    };

    // Files to download per update (profiler DLL excluded — locked on Windows, rarely changes)
    private static readonly string[] UpdateFiles =
    {
        "UprootedHook.dll",
        "UprootedHook.deps.json",
        "UprootedHook.net9.dll",
        "UprootedHook.net9.deps.json",
        "uprooted-preload.js",
        "uprooted.css",
        "nsfw-filter.js",
        "link-embeds.js"
    };

    // State
    private string? _latestVersion;
    private string? _latestTag;
    private string? _assetApiUrl; // GitHub API URL for the .uprpkg asset (private repo downloads)
    private bool _updateApplied;
    private string? _lastError;
    private int _checking; // 0 = idle, 1 = checking (atomic via Interlocked)
    private bool _isManualCheck;
    private Timer? _periodicTimer;

    /// <summary>
    /// Fired when a background (non-manual) auto-update is successfully applied.
    /// The string argument is the new version string.
    /// Used to show a popup notification to the user without requiring them to open settings.
    /// </summary>
    internal static event Action<string>? BackgroundUpdateApplied;

    // Singleton for UI access
    internal static AutoUpdater? Instance { get; set; }

    internal static bool IsDevChannel => UprootedSettings.Load().AutoUpdateChannel == "developer";

    /// <summary>
    /// Validate the developer channel password by comparing SHA-256 hashes.
    /// </summary>
    internal static bool ValidateDevPassword(string input)
    {
        try
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            var hex = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            return hex == DevPasswordHash;
        }
        catch
        {
            return false;
        }
    }

    private static string DecryptPat()
    {
        var result = new byte[EncryptedPat.Length];
        for (int i = 0; i < EncryptedPat.Length; i++)
            result[i] = (byte)(EncryptedPat[i] ^ PatXorKey[i]);
        return Encoding.ASCII.GetString(result);
    }

    // HTTP via reflection (own copy — project convention of self-contained files)
    // Internal so ContentPages can reuse for API key validation
    internal static object? s_httpClient;
    private static MethodInfo? s_getStringAsync;
    private static MethodInfo? s_getByteArrayAsync;
    internal static MethodInfo? s_getAsync;
    internal static MethodInfo? s_sendAsync;
    internal static Type? s_httpRequestMessageType;
    private static object? s_httpMethodGet;
    private static bool s_httpResolved;

    // Regex to extract tag_name from GitHub API JSON (no System.Text.Json)
    private static readonly Regex TagNameRegex = new(
        @"""tag_name""\s*:\s*""([^""]+)""",
        RegexOptions.Compiled);

    // Regex to extract the API URL for auto-update.uprpkg from the assets array.
    // Matches: "url":"https://api.github.com/.../releases/assets/12345" ... "name":"auto-update.uprpkg"
    // (GitHub returns "url" before "name" in the assets objects)
    private static readonly Regex AssetApiUrlRegex = new(
        @"""url""\s*:\s*""(https://api\.github\.com/repos/[^""]+/releases/assets/\d+)""[^}]*?""name""\s*:\s*""auto-update\.uprpkg""",
        RegexOptions.Compiled);

    /// <summary>
    /// Initialize the auto-updater. Starts a periodic check timer if enabled.
    /// Always callable for manual checks even if auto-check is disabled.
    /// </summary>
    internal void Initialize()
    {
        using var ev = WideEvent.Begin("AutoUpdate", "init");
        var settings = UprootedSettings.Load();
        ev.Set("auto_update_enabled", settings.AutoUpdateEnabled);
        ev.Set("channel", settings.AutoUpdateChannel ?? "stable");

        if (settings.AutoUpdateEnabled)
        {
            // Check if enough time has passed since last check
            var shouldCheck = ShouldCheck(settings);
            ev.Set("initial_check", shouldCheck);
            if (shouldCheck)
            {
                CheckForUpdate();
            }

            // Start periodic timer (check every minute)
            _periodicTimer = new Timer(_ =>
            {
                try
                {
                    var s = UprootedSettings.Load();
                    if (s.AutoUpdateEnabled && ShouldCheck(s))
                        CheckForUpdate();
                }
                catch (Exception ex)
                {
                    Logger.Log("AutoUpdate", $"Periodic check error: {ex.Message}");
                }
            }, null, TimeSpan.FromMinutes(CheckIntervalMinutes), TimeSpan.FromMinutes(CheckIntervalMinutes));
        }
    }

    /// <summary>
    /// Manually trigger an update check. Runs synchronously — caller should
    /// invoke on a background thread and update UI when done.
    /// isManual=true: triggered by the user pressing "Check for Updates" — will also apply
    /// a same-version update if the .uprpkg hash differs (catches silent hotfixes).
    /// isManual=false: background periodic check — only applies on version bump.
    /// </summary>
    internal void CheckForUpdate(bool isManual = false)
    {
        if (Interlocked.CompareExchange(ref _checking, 1, 0) != 0) return;
        _isManualCheck = isManual;
        try { RunCheck(); }
        finally { Interlocked.Exchange(ref _checking, 0); }
    }

    /// <summary>
    /// Returns (statusText, statusColor) for the UI.
    /// </summary>
    internal (string text, string color) GetStatus()
    {
        var channelTag = IsDevChannel ? " [Dev]" : "";

        if (_checking != 0)
            return ("Checking for updates...", ContentPages.TextMuted);

        if (_updateApplied)
            return ($"Updated to v{_latestVersion}{channelTag} \u2014 restart Root to apply", ContentPages.AccentGreen);

        if (_lastError != null)
            return ($"Check failed: {_lastError}", "#E04040");

        var currentVersion = UprootedSettings.Load().Version;
        if (_latestVersion != null)
        {
            var cmp = CompareVersions(_latestVersion, currentVersion);
            if (cmp > 0)
                return ($"Update available: v{_latestVersion}{channelTag}", "#C0A820");
            if (cmp < 0)
                return ($"Ahead of latest release (v{currentVersion} > v{_latestVersion}){channelTag}", ContentPages.AccentGreen);
        }

        return ($"Up to date (v{currentVersion}){channelTag}", ContentPages.AccentGreen);
    }

    internal bool IsChecking => _checking != 0;
    internal bool HasUpdate => _latestVersion != null && CompareVersions(_latestVersion, UprootedSettings.Load().Version) > 0;
    internal bool UpdateApplied => _updateApplied;

    private static bool ShouldCheck(UprootedSettings settings)
    {
        if (string.IsNullOrEmpty(settings.LastUpdateCheck)) return true;
        try
        {
            var last = DateTime.Parse(settings.LastUpdateCheck, null, System.Globalization.DateTimeStyles.RoundtripKind);
            return (DateTime.UtcNow - last).TotalMinutes >= CheckIntervalMinutes;
        }
        catch
        {
            return true;
        }
    }

    private void RunCheck()
    {
        _lastError = null;
        using var ev = WideEvent.Begin("AutoUpdate", "check");

        try
        {
            var settings = UprootedSettings.Load();
            var channel = settings.AutoUpdateChannel ?? "stable";
            var isDev = channel == "developer";
            var isCanary = channel == "canary";
            var apiUrl = isDev ? DevApiUrl : isCanary ? CanaryApiUrl : StableApiUrl;
            var authToken = isDev ? DecryptPat() : null;

            ev.Set("channel", channel);
            ev.Set("is_manual", _isManualCheck);

            // Update last check timestamp immediately (we attempted a check regardless of outcome)
            settings.LastUpdateCheck = DateTime.UtcNow.ToString("o");
            settings.Save();

            // Hit GitHub API
            var json = HttpGetString(apiUrl, authToken);
            if (json == null)
            {
                _lastError = "Could not reach GitHub";
                ev.SetError(_lastError);
                return;
            }

            // Parse tag_name
            var match = TagNameRegex.Match(json);
            if (!match.Success)
            {
                _lastError = "No published release found";
                ev.SetError(_lastError);
                return;
            }

            _latestTag = match.Groups[1].Value;
            _latestVersion = _latestTag.TrimStart('v');
            ev.Set("latest_version", _latestVersion);

            // For private repos, extract the API asset URL (browser download URLs return 404 with Bearer auth)
            _assetApiUrl = null;
            if (isDev)
            {
                var assetMatch = AssetApiUrlRegex.Match(json);
                if (assetMatch.Success)
                    _assetApiUrl = assetMatch.Groups[1].Value;
                ev.Set("has_asset_api_url", assetMatch.Success);
            }

            // Compare versions
            var currentVersion = settings.Version;
            var cmp = CompareVersions(_latestVersion, currentVersion);
            ev.Set("current_version", currentVersion);
            ev.Set("version_cmp", cmp);

            if (cmp > 0)
            {
                ev.Set("result", "update_available");

                // Save pending version for UI
                settings = UprootedSettings.Load();
                settings.PendingUpdateVersion = _latestVersion;
                settings.Save();

                // Download and apply
                DownloadAndApply(isDev, isCanary, ev);
            }
            else if (cmp == 0 && _isManualCheck)
            {
                // Same version — on a manual check, still download the package and compare
                // its SHA-256 against the stored hash. A differing hash means a silent hotfix
                // was published under the same version tag.
                ev.Set("result", "same_version_hotfix_check");
                DownloadAndApply(isDev, isCanary, ev);
            }
            else
            {
                ev.Set("result", "up_to_date");
            }
        }
        catch (Exception ex)
        {
            _lastError = ex.Message;
            ev.SetError(ex);
        }
    }

    private void DownloadAndApply(bool isDev, bool isCanary, WideEvent parentEv)
    {
        using var ev = WideEvent.Begin("AutoUpdate", "download", parentEv);
        var uprootedDir = PlatformPaths.GetUprootedDir();
        var stagingDir = Path.Combine(uprootedDir, "update-staging");

        // Use channel determined by RunCheck — don't re-read settings (avoids race if user
        // toggles the channel while the check is in progress)
        var downloadBase = isDev ? DevDownloadBase : isCanary ? CanaryDownloadBase : StableDownloadBase;
        var authToken = isDev ? DecryptPat() : null;

        ev.Set("version", _latestVersion);
        ev.Set("channel", isDev ? "developer" : isCanary ? "canary" : "stable");

        try
        {
            // Create staging directory
            if (Directory.Exists(stagingDir))
                Directory.Delete(stagingDir, true);
            Directory.CreateDirectory(stagingDir);

            // Download single encrypted package
            // For private repos, use the GitHub API asset URL with Accept: application/octet-stream
            // (browser download URLs at github.com/.../releases/download/... return 404 with Bearer auth)
            var browserUrl = $"{downloadBase}/{_latestTag}/auto-update.uprpkg";
            var pkgUrl = _assetApiUrl ?? browserUrl;
            var useApiDownload = _assetApiUrl != null;
            ev.Set("use_api_download", useApiDownload);

            var pkgBytes = useApiDownload
                ? HttpGetBytesViaApi(pkgUrl, authToken)
                : HttpGetBytes(pkgUrl, authToken);
            if (pkgBytes == null || pkgBytes.Length == 0)
            {
                _lastError = "Download failed (check log for HTTP status)";
                ev.SetError(_lastError);
                return;
            }

            ev.Set("pkg_bytes", pkgBytes.Length);

            // Compute package SHA-256 for hotfix detection (same-version updates)
            var newHash = ComputeSha256Hex(pkgBytes);
            ev.Set("pkg_hash", newHash[..16]);

            // If the installed version matches the release version, only apply when the
            // hash differs — this lets the "Check for Updates" button catch silent hotfixes
            // without re-applying an identical package.
            var installedVersion = UprootedSettings.Load().Version;
            if (CompareVersions(_latestVersion!, installedVersion) == 0)
            {
                var storedHash = UprootedSettings.Load().LastPackageHash;
                if (!string.IsNullOrEmpty(storedHash) && storedHash == newHash)
                {
                    ev.Set("applied", false);
                    ev.Set("skip_reason", "hash_matches");
                    // No update needed; leave _updateApplied false so UI shows "Up to date"
                    try { Directory.Delete(stagingDir, true); } catch { }
                    return;
                }
                ev.Set("hotfix", string.IsNullOrEmpty(storedHash) ? "baseline" : "hash_differs");
            }

            // Decrypt and unpack
            var files = UnpackPackage(pkgBytes);
            if (files == null)
            {
                // _lastError already set by UnpackPackage
                ev.SetError(_lastError ?? "unpack_failed");
                return;
            }

            ev.Set("files_unpacked", files.Count);

            // Write extracted files to staging
            foreach (var (filename, data) in files)
            {
                File.WriteAllBytes(Path.Combine(stagingDir, filename), data);
            }

            // Verify expected files present
            foreach (var filename in UpdateFiles)
            {
                var path = Path.Combine(stagingDir, filename);
                if (!File.Exists(path))
                {
                    _lastError = $"Missing file in package: {filename}";
                    ev.SetError(_lastError);
                    return;
                }
                var info = new FileInfo(path);
                if (info.Length == 0)
                {
                    _lastError = $"Empty file in package: {filename}";
                    ev.SetError(_lastError);
                    return;
                }
            }

            // Copy from staging to uprooted dir (overwrite)
            foreach (var filename in UpdateFiles)
            {
                var src = Path.Combine(stagingDir, filename);
                var dst = Path.Combine(uprootedDir, filename);
                CopyFileRobust(src, dst);
            }

            // Select correct TFM for this runtime — net10.0 is the primary DLL,
            // but if running on .NET 9 we overwrite it with the net9.0 variant.
            if (Environment.Version.Major < 10)
            {
                var net9Dll = Path.Combine(uprootedDir, "UprootedHook.net9.dll");
                var net9Deps = Path.Combine(uprootedDir, "UprootedHook.net9.deps.json");
                if (File.Exists(net9Dll))
                    CopyFileRobust(net9Dll, Path.Combine(uprootedDir, "UprootedHook.dll"));
                if (File.Exists(net9Deps))
                    CopyFileRobust(net9Deps, Path.Combine(uprootedDir, "UprootedHook.deps.json"));
                ev.Set("tfm_selected", "net9.0");
            }
            else
            {
                ev.Set("tfm_selected", "net10.0");
            }
            // Clean up variant files
            try { File.Delete(Path.Combine(uprootedDir, "UprootedHook.net9.dll")); } catch { }
            try { File.Delete(Path.Combine(uprootedDir, "UprootedHook.net9.deps.json")); } catch { }

            // Clean up staging
            try { Directory.Delete(stagingDir, true); }
            catch { /* non-fatal */ }

            // Update settings
            var settings = UprootedSettings.Load();
            settings.Version = _latestVersion!;
            settings.PendingUpdateVersion = "";
            settings.LastPackageHash = newHash;
            settings.Save();

            _updateApplied = true;
            ev.Set("applied", true);

            // Notify UI for background (non-manual) updates — the user hasn't pressed a button
            // so they may not be looking at the Updates settings page. Show a popup.
            if (!_isManualCheck)
            {
                BackgroundUpdateApplied?.Invoke(_latestVersion!);
            }
        }
        catch (Exception ex)
        {
            _lastError = $"Apply failed: {ex.Message}";
            ev.SetError(ex);
            // Leave staging dir for debugging, don't overwrite production files
        }
    }

    /// <summary>
    /// Copy src to dst safely when dst may be a loaded CLR assembly.
    /// Always uses rename-then-copy rather than direct overwrite:
    ///   - Windows: process keeps old handle valid after rename, no access error.
    ///   - Linux: CLR holds flock(LOCK_SH) on loaded assemblies; File.Copy with
    ///     overwrite=true blocks indefinitely (flock never released while loaded).
    ///     Rename moves the directory entry to a new path, bypassing the lock, then
    ///     File.Copy writes a fresh inode at the original path.
    /// </summary>
    private static void CopyFileRobust(string src, string dst)
    {
        if (!File.Exists(dst))
        {
            File.Copy(src, dst);
            return;
        }

        var old = dst + ".old";
        try { File.Delete(old); } catch { /* stale .old from previous run */ }
        File.Move(dst, old);      // atomic rename: old inode moves to .old path
        try
        {
            File.Copy(src, dst);  // new inode written at original path
        }
        catch
        {
            // Rollback: restore original so we don't leave the user with nothing
            try { File.Move(old, dst); } catch { /* best effort */ }
            throw;
        }
        try { File.Delete(old); } catch { /* non-fatal — cleaned up on next run */ }
    }

    /// <summary>
    /// Decrypt and unpack a .uprpkg binary package.
    /// Returns filename→decrypted bytes map, or null on error (sets _lastError).
    /// </summary>
    private Dictionary<string, byte[]>? UnpackPackage(byte[] data)
    {
        try
        {
            // Minimum size: 4 magic + 1 version + 2 count + 32 nonce = 39 bytes
            if (data.Length < 39)
            {
                _lastError = "Package too small";
                Logger.Log("AutoUpdate", _lastError);
                return null;
            }

            // Validate magic "UPRK"
            if (data[0] != (byte)'U' || data[1] != (byte)'P' || data[2] != (byte)'R' || data[3] != (byte)'K')
            {
                _lastError = "Invalid package (bad magic)";
                Logger.Log("AutoUpdate", _lastError);
                return null;
            }

            // Version check
            if (data[4] != 0x01)
            {
                _lastError = $"Unsupported package version: {data[4]}";
                Logger.Log("AutoUpdate", _lastError);
                return null;
            }

            // File count (uint16 LE)
            int fileCount = data[5] | (data[6] << 8);
            if (fileCount == 0 || fileCount > 100)
            {
                _lastError = $"Invalid file count: {fileCount}";
                Logger.Log("AutoUpdate", _lastError);
                return null;
            }

            // Nonce (32 bytes at offset 7)
            var nonce = new byte[32];
            Array.Copy(data, 7, nonce, 0, 32);

            var result = new Dictionary<string, byte[]>(fileCount);
            int offset = 39; // past header

            for (int i = 0; i < fileCount; i++)
            {
                // Filename length (uint16 LE)
                if (offset + 2 > data.Length)
                {
                    _lastError = $"Package truncated at file {i} name length";
                    Logger.Log("AutoUpdate", _lastError);
                    return null;
                }
                int nameLen = data[offset] | (data[offset + 1] << 8);
                offset += 2;

                // Filename (UTF-8)
                if (offset + nameLen > data.Length)
                {
                    _lastError = $"Package truncated at file {i} name";
                    Logger.Log("AutoUpdate", _lastError);
                    return null;
                }
                var filename = Encoding.UTF8.GetString(data, offset, nameLen);
                offset += nameLen;

                // Data length (uint32 LE)
                if (offset + 4 > data.Length)
                {
                    _lastError = $"Package truncated at file {i} data length";
                    Logger.Log("AutoUpdate", _lastError);
                    return null;
                }
                uint dataLen = (uint)(data[offset] | (data[offset + 1] << 8) |
                                      (data[offset + 2] << 16) | (data[offset + 3] << 24));
                offset += 4;

                // Encrypted data
                if (offset + dataLen > data.Length)
                {
                    _lastError = $"Package truncated at file {i} data";
                    Logger.Log("AutoUpdate", _lastError);
                    return null;
                }

                // Decrypt
                var decrypted = new byte[dataLen];
                for (uint pos = 0; pos < dataLen; pos++)
                {
                    byte key = (byte)(PackageMasterKey[pos % 64] ^ nonce[pos % 32] ^ ((pos >> 8) & 0xFF));
                    decrypted[pos] = (byte)(data[offset + pos] ^ key);
                }
                offset += (int)dataLen;

                result[filename] = decrypted;
            }

            return result;
        }
        catch (Exception ex)
        {
            _lastError = $"Package unpack error: {ex.Message}";
            Logger.Log("AutoUpdate", _lastError);
            return null;
        }
    }

    // ===== Utilities =====

    private static string ComputeSha256Hex(byte[] data)
    {
        using var sha = SHA256.Create();
        return BitConverter.ToString(sha.ComputeHash(data)).Replace("-", "").ToLowerInvariant();
    }

    // ===== Version comparison =====

    /// <summary>
    /// Compare two version strings. Returns positive if a > b, negative if a &lt; b, 0 if equal.
    /// Handles: "0.3.6", "0.3.6-rc", "0.3.7-beta", "0.4.0"
    /// Pre-release suffixes sort below the bare version (0.3.6 > 0.3.6-rc).
    /// </summary>
    internal static int CompareVersions(string a, string b)
    {
        ParseVersion(a, out var aMajor, out var aMinor, out var aPatch, out var aSuffix);
        ParseVersion(b, out var bMajor, out var bMinor, out var bPatch, out var bSuffix);

        var cmp = aMajor.CompareTo(bMajor);
        if (cmp != 0) return cmp;

        cmp = aMinor.CompareTo(bMinor);
        if (cmp != 0) return cmp;

        cmp = aPatch.CompareTo(bPatch);
        if (cmp != 0) return cmp;

        // Both have no suffix = equal; one has suffix = that one is lower
        if (aSuffix.Length == 0 && bSuffix.Length == 0) return 0;
        if (aSuffix.Length == 0) return 1;  // a is release, b is pre-release
        if (bSuffix.Length == 0) return -1; // a is pre-release, b is release

        return string.Compare(aSuffix, bSuffix, StringComparison.OrdinalIgnoreCase);
    }

    private static void ParseVersion(string v, out int major, out int minor, out int patch, out string suffix)
    {
        major = minor = patch = 0;
        suffix = "";

        v = v.TrimStart('v');

        var parts = v.Split('.');
        if (parts.Length >= 1) int.TryParse(parts[0], out major);
        if (parts.Length >= 2) int.TryParse(parts[1], out minor);

        if (parts.Length >= 3)
        {
            var patchStr = parts[2];
            // Split patch from suffix: "6rc" -> patch=6, suffix="rc"; "7-beta" -> patch=7, suffix="beta"
            var idx = 0;
            while (idx < patchStr.Length && char.IsDigit(patchStr[idx])) idx++;

            if (idx > 0) int.TryParse(patchStr[..idx], out patch);
            if (idx < patchStr.Length)
            {
                suffix = patchStr[idx..].TrimStart('-');
            }
        }
    }

    // ===== HTTP via reflection =====

    internal static void EnsureHttpResolved()
    {
        if (s_httpResolved) return;
        s_httpResolved = true;

        using var ev = WideEvent.Begin("AutoUpdate", "http_resolve");
        try
        {
            Type? httpClientType = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.GetName().Name != "System.Net.Http") continue;
                httpClientType = asm.GetType("System.Net.Http.HttpClient");
                break;
            }

            if (httpClientType == null)
            {
                ev.SetError("HttpClient type not found");
                return;
            }

            s_httpClient = Activator.CreateInstance(httpClientType);
            if (s_httpClient == null) return;

            // Set timeout
            var timeoutProp = httpClientType.GetProperty("Timeout");
            timeoutProp?.SetValue(s_httpClient, TimeSpan.FromSeconds(HttpTimeoutSeconds));

            // Set headers: identity encoding (avoid trimmed decompression), User-Agent for GitHub API
            try
            {
                var defaultHeaders = httpClientType.GetProperty("DefaultRequestHeaders")?.GetValue(s_httpClient);
                if (defaultHeaders != null)
                {
                    var tryAdd = defaultHeaders.GetType().GetMethod("TryAddWithoutValidation",
                        new[] { typeof(string), typeof(string) });
                    tryAdd?.Invoke(defaultHeaders, new object[] { "Accept-Encoding", "identity" });
                    tryAdd?.Invoke(defaultHeaders, new object[] { "User-Agent", "Uprooted-AutoUpdater/1.0" });
                    // GitHub API requires Accept header
                    tryAdd?.Invoke(defaultHeaders, new object[] { "Accept", "application/vnd.github+json" });
                }
            }
            catch (Exception ex)
            {
                Logger.Log("AutoUpdate", $"HTTP headers error: {ex.Message}");
            }

            // Resolve methods
            s_getStringAsync = httpClientType.GetMethod("GetStringAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(string) }, null);

            s_getByteArrayAsync = httpClientType.GetMethod("GetByteArrayAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(string) }, null);

            s_getAsync = httpClientType.GetMethod("GetAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(string) }, null);
            s_getAsync ??= httpClientType.GetMethod("GetAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, new[] { typeof(Uri) }, null);

            // Resolve SendAsync for deep fallback
            try
            {
                var httpAsm = httpClientType.Assembly;
                s_httpRequestMessageType = httpAsm.GetType("System.Net.Http.HttpRequestMessage");
                var httpMethodType = httpAsm.GetType("System.Net.Http.HttpMethod");
                s_httpMethodGet = httpMethodType?.GetProperty("Get",
                    BindingFlags.Public | BindingFlags.Static)?.GetValue(null);

                if (s_httpRequestMessageType != null)
                {
                    s_sendAsync = httpClientType.GetMethod("SendAsync",
                        BindingFlags.Public | BindingFlags.Instance,
                        null, new[] { s_httpRequestMessageType }, null);
                    s_sendAsync ??= httpClientType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Where(m => m.Name == "SendAsync" && !m.IsGenericMethod)
                        .FirstOrDefault(m =>
                        {
                            var p = m.GetParameters();
                            return p.Length >= 1 && p[0].ParameterType.IsAssignableFrom(s_httpRequestMessageType);
                        });
                }
            }
            catch (Exception ex)
            {
                Logger.Log("AutoUpdate", $"SendAsync resolve error: {ex.Message}");
            }

            ev.Set("client", s_httpClient != null);
            ev.Set("get_string_async", s_getStringAsync != null);
            ev.Set("get_byte_array_async", s_getByteArrayAsync != null);
            ev.Set("get_async", s_getAsync != null);
            ev.Set("send_async", s_sendAsync != null);
        }
        catch (Exception ex)
        {
            ev.SetError(ex);
        }
    }

    private static string? HttpGetString(string url, string? authToken = null)
    {
        EnsureHttpResolved();
        if (s_httpClient == null) return null;

        // When auth is needed, must use SendAsync to set per-request Authorization header
        if (authToken == null && s_getStringAsync != null)
        {
            try
            {
                var task = s_getStringAsync.Invoke(s_httpClient, new object[] { url });
                if (task != null)
                {
                    var result = task.GetType().GetProperty("Result")?.GetValue(task) as string;
                    if (result != null) return result;
                }
            }
            catch (Exception ex)
            {
                var inner = UnwrapException(ex);
                Logger.Log("AutoUpdate", $"GetStringAsync failed for {url}: {inner.Message}");
                // Fall through to SendAsync
            }
        }

        // Fallback / auth path: SendAsync
        if (s_sendAsync != null && s_httpRequestMessageType != null && s_httpMethodGet != null)
        {
            try
            {
                var request = Activator.CreateInstance(s_httpRequestMessageType, s_httpMethodGet, new Uri(url));
                if (request != null)
                    SetRequestAuth(request, authToken);

                var sendParams = s_sendAsync.GetParameters();
                object?[] args = sendParams.Length == 1
                    ? new[] { request }
                    : new object?[] { request, System.Threading.CancellationToken.None };

                var task = s_sendAsync.Invoke(s_httpClient, args);
                var response = task?.GetType().GetProperty("Result")?.GetValue(task);
                if (response == null) return null;

                // SendAsync doesn't throw on non-2xx — check status explicitly
                var statusCode = GetResponseStatusCode(response);
                if (statusCode >= 0 && (statusCode < 200 || statusCode >= 300))
                {
                    Logger.Log("AutoUpdate", $"HTTP {statusCode} from {url}");
                    return null;
                }

                var content = response.GetType().GetProperty("Content")?.GetValue(response);
                if (content == null) return null;

                var readTask = content.GetType().GetMethod("ReadAsStringAsync",
                    BindingFlags.Public | BindingFlags.Instance,
                    null, Type.EmptyTypes, null)?.Invoke(content, null);
                return readTask?.GetType().GetProperty("Result")?.GetValue(readTask) as string;
            }
            catch (Exception ex)
            {
                var inner = UnwrapException(ex);
                Logger.Log("AutoUpdate", $"SendAsync (string) failed for {url}: {inner.Message}");
            }
        }

        return null;
    }

    private static byte[]? HttpGetBytes(string url, string? authToken = null)
    {
        EnsureHttpResolved();
        if (s_httpClient == null) return null;

        // When auth is needed, skip GetByteArrayAsync (can't set per-request headers)
        if (authToken == null && s_getByteArrayAsync != null)
        {
            try
            {
                var task = s_getByteArrayAsync.Invoke(s_httpClient, new object[] { url });
                if (task != null)
                {
                    var result = task.GetType().GetProperty("Result")?.GetValue(task) as byte[];
                    if (result != null) return result;
                }
            }
            catch { /* Fall through */ }
        }

        // Fallback / auth path: GetAsync or SendAsync -> stream -> bytes
        try
        {
            object? response = null;

            // GetAsync can't carry auth headers, skip when auth is needed
            if (authToken == null && s_getAsync != null)
            {
                var paramType = s_getAsync.GetParameters()[0].ParameterType;
                object arg = paramType == typeof(Uri) ? new Uri(url) : (object)url;
                var task = s_getAsync.Invoke(s_httpClient, new[] { arg });
                response = task?.GetType().GetProperty("Result")?.GetValue(task);
            }

            if (response == null && s_sendAsync != null && s_httpRequestMessageType != null && s_httpMethodGet != null)
            {
                var request = Activator.CreateInstance(s_httpRequestMessageType, s_httpMethodGet, new Uri(url));
                if (request != null)
                    SetRequestAuth(request, authToken);

                var sendParams = s_sendAsync.GetParameters();
                object?[] args = sendParams.Length == 1
                    ? new[] { request }
                    : new object?[] { request, System.Threading.CancellationToken.None };

                var task = s_sendAsync.Invoke(s_httpClient, args);
                response = task?.GetType().GetProperty("Result")?.GetValue(task);
            }

            if (response == null) return null;

            // SendAsync doesn't throw on non-2xx — check status explicitly
            var statusCode = GetResponseStatusCode(response);
            if (statusCode >= 0 && (statusCode < 200 || statusCode >= 300))
            {
                Logger.Log("AutoUpdate", $"HTTP {statusCode} downloading {url}");
                return null;
            }

            var content = response.GetType().GetProperty("Content")?.GetValue(response);
            if (content == null) return null;

            var readTask = content.GetType().GetMethod("ReadAsStreamAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, Type.EmptyTypes, null)?.Invoke(content, null);
            if (readTask == null) return null;

            var stream = readTask.GetType().GetProperty("Result")?.GetValue(readTask) as Stream;
            if (stream == null) return null;

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
        catch (Exception ex)
        {
            var inner = UnwrapException(ex);
            Logger.Log("AutoUpdate", $"HTTP GET bytes error for {url}: {inner.Message}");
            return null;
        }
    }

    /// <summary>
    /// Download bytes from a GitHub API asset URL using Accept: application/octet-stream.
    /// This is the correct way to download release assets from private repos — the browser
    /// download URL (github.com/.../releases/download/...) returns 404 with Bearer token auth.
    /// GitHub responds with a 302 redirect to a signed CDN URL; HttpClient follows it automatically.
    /// </summary>
    private static byte[]? HttpGetBytesViaApi(string apiUrl, string? authToken)
    {
        EnsureHttpResolved();
        if (s_httpClient == null || s_sendAsync == null || s_httpRequestMessageType == null || s_httpMethodGet == null)
            return null;

        try
        {
            var request = Activator.CreateInstance(s_httpRequestMessageType, s_httpMethodGet, new Uri(apiUrl));
            if (request == null) return null;

            // Set auth
            SetRequestAuth(request, authToken);

            // Set Accept: application/octet-stream (tells GitHub API to return the binary, not JSON metadata)
            var headers = request.GetType().GetProperty("Headers")?.GetValue(request);
            if (headers != null)
            {
                var tryAdd = headers.GetType().GetMethod("TryAddWithoutValidation",
                    new[] { typeof(string), typeof(string) });
                tryAdd?.Invoke(headers, new object[] { "Accept", "application/octet-stream" });
            }

            var sendParams = s_sendAsync.GetParameters();
            object?[] args = sendParams.Length == 1
                ? new[] { request }
                : new object?[] { request, System.Threading.CancellationToken.None };

            var task = s_sendAsync.Invoke(s_httpClient, args);
            var response = task?.GetType().GetProperty("Result")?.GetValue(task);
            if (response == null) return null;

            var statusCode = GetResponseStatusCode(response);
            if (statusCode >= 0 && (statusCode < 200 || statusCode >= 300))
            {
                Logger.Log("AutoUpdate", $"HTTP {statusCode} downloading (API) {apiUrl}");
                return null;
            }

            var content = response.GetType().GetProperty("Content")?.GetValue(response);
            if (content == null) return null;

            var readTask = content.GetType().GetMethod("ReadAsStreamAsync",
                BindingFlags.Public | BindingFlags.Instance,
                null, Type.EmptyTypes, null)?.Invoke(content, null);
            if (readTask == null) return null;

            var stream = readTask.GetType().GetProperty("Result")?.GetValue(readTask) as Stream;
            if (stream == null) return null;

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return ms.ToArray();
        }
        catch (Exception ex)
        {
            var inner = UnwrapException(ex);
            Logger.Log("AutoUpdate", $"API download error for {apiUrl}: {inner.Message}");
            return null;
        }
    }

    /// <summary>
    /// Set Authorization: Bearer header on an HttpRequestMessage via reflection.
    /// </summary>
    private static void SetRequestAuth(object request, string? authToken)
    {
        if (authToken == null) return;
        try
        {
            var headers = request.GetType().GetProperty("Headers")?.GetValue(request);
            if (headers == null) return;
            var tryAdd = headers.GetType().GetMethod("TryAddWithoutValidation",
                new[] { typeof(string), typeof(string) });
            tryAdd?.Invoke(headers, new object[] { "Authorization", $"Bearer {authToken}" });
        }
        catch (Exception ex)
        {
            Logger.Log("AutoUpdate", $"SetRequestAuth error: {ex.Message}");
        }
    }

    /// <summary>
    /// Extract the HTTP status code from an HttpResponseMessage via reflection.
    /// Returns -1 if the status code cannot be determined.
    /// </summary>
    private static int GetResponseStatusCode(object? response)
    {
        if (response == null) return -1;
        try
        {
            var statusCodeProp = response.GetType().GetProperty("StatusCode");
            if (statusCodeProp != null)
                return Convert.ToInt32(statusCodeProp.GetValue(response));
        }
        catch { }
        return -1;
    }

    private static Exception UnwrapException(Exception ex)
    {
        var inner = ex;
        if (inner is TargetInvocationException tie && tie.InnerException != null)
            inner = tie.InnerException;
        if (inner is AggregateException ae && ae.InnerException != null)
            inner = ae.InnerException;
        return inner;
    }
}
