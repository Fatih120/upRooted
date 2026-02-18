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
    // Stable channel (public repo)
    private const string StableApiUrl = "https://api.github.com/repos/watchthelight/uprooted/releases/latest";
    private const string StableDownloadBase = "https://github.com/watchthelight/uprooted/releases/download";

    // Developer channel (private repo)
    private const string DevApiUrl = "https://api.github.com/repos/The-Uprooted-Project/uprooted-private/releases/latest";
    private const string DevDownloadBase = "https://github.com/The-Uprooted-Project/uprooted-private/releases/download";

    private const int CheckIntervalHours = 6;
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

    // Files to download per update (profiler DLL excluded — locked on Windows, rarely changes)
    private static readonly string[] UpdateFiles =
    {
        "UprootedHook.dll",
        "UprootedHook.deps.json",
        "uprooted-preload.js",
        "uprooted.css",
        "nsfw-filter.js",
        "link-embeds.js"
    };

    // State
    private string? _latestVersion;
    private string? _latestTag;
    private bool _updateApplied;
    private string? _lastError;
    private int _checking; // 0 = idle, 1 = checking (atomic via Interlocked)
    private Timer? _periodicTimer;

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
    private static object? s_httpClient;
    private static MethodInfo? s_getStringAsync;
    private static MethodInfo? s_getByteArrayAsync;
    private static MethodInfo? s_getAsync;
    private static MethodInfo? s_sendAsync;
    private static Type? s_httpRequestMessageType;
    private static object? s_httpMethodGet;
    private static bool s_httpResolved;

    // Regex to extract tag_name from GitHub API JSON (no System.Text.Json)
    private static readonly Regex TagNameRegex = new(
        @"""tag_name""\s*:\s*""([^""]+)""",
        RegexOptions.Compiled);

    /// <summary>
    /// Initialize the auto-updater. Starts a periodic check timer if enabled.
    /// Always callable for manual checks even if auto-check is disabled.
    /// </summary>
    internal void Initialize()
    {
        var settings = UprootedSettings.Load();
        if (settings.AutoUpdateEnabled)
        {
            // Check if enough time has passed since last check
            if (ShouldCheck(settings))
            {
                Logger.Log("AutoUpdate", "Running initial update check...");
                CheckForUpdate();
            }
            else
            {
                Logger.Log("AutoUpdate", "Skipping initial check (throttled)");
            }

            // Start periodic timer (check every 6 hours)
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
            }, null, TimeSpan.FromHours(CheckIntervalHours), TimeSpan.FromHours(CheckIntervalHours));
        }
        else
        {
            Logger.Log("AutoUpdate", "Auto-check disabled, skipping periodic timer");
        }
    }

    /// <summary>
    /// Manually trigger an update check. Runs synchronously — caller should
    /// invoke on a background thread and update UI when done.
    /// </summary>
    internal void CheckForUpdate()
    {
        if (Interlocked.CompareExchange(ref _checking, 1, 0) != 0) return;
        try { RunCheck(); }
        finally { Interlocked.Exchange(ref _checking, 0); }
    }

    /// <summary>
    /// Returns (statusText, statusColor) for the UI.
    /// </summary>
    internal (string text, string color) GetStatus()
    {
        var channelSuffix = IsDevChannel ? " (Dev)" : "";

        if (_checking != 0)
            return ("Checking for updates...", ContentPages.TextMuted);

        if (_updateApplied)
            return ($"Updated to v{_latestVersion}{channelSuffix} \u2014 restart Root to apply", ContentPages.AccentGreen);

        if (_lastError != null)
            return ($"Check failed: {_lastError}", "#E04040");

        var currentVersion = UprootedSettings.Load().Version;
        if (_latestVersion != null)
        {
            var cmp = CompareVersions(_latestVersion, currentVersion);
            if (cmp > 0)
                return ($"Update available: v{_latestVersion}{channelSuffix}", "#C0A820");
            if (cmp < 0)
                return ($"Ahead of latest release (v{currentVersion} > v{_latestVersion}){channelSuffix}", ContentPages.AccentGreen);
        }

        return ($"Up to date (v{currentVersion}){channelSuffix}", ContentPages.AccentGreen);
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
            return (DateTime.UtcNow - last).TotalHours >= CheckIntervalHours;
        }
        catch
        {
            return true;
        }
    }

    private void RunCheck()
    {
        _lastError = null;

        try
        {
            var settings = UprootedSettings.Load();
            var isDev = settings.AutoUpdateChannel == "developer";
            var apiUrl = isDev ? DevApiUrl : StableApiUrl;
            var authToken = isDev ? DecryptPat() : null;

            Logger.Log("AutoUpdate", $"Checking for updates (channel: {settings.AutoUpdateChannel})...");

            // Update last check timestamp immediately (we attempted a check regardless of outcome)
            settings.LastUpdateCheck = DateTime.UtcNow.ToString("o");
            settings.Save();

            // Hit GitHub API
            var json = HttpGetString(apiUrl, authToken);
            if (json == null)
            {
                _lastError = "Could not reach GitHub";
                Logger.Log("AutoUpdate", "Failed to fetch release info");
                return;
            }

            // Parse tag_name
            var match = TagNameRegex.Match(json);
            if (!match.Success)
            {
                _lastError = "No published release found";
                Logger.Log("AutoUpdate", "tag_name not found in response (no published release?)");
                return;
            }

            _latestTag = match.Groups[1].Value;
            _latestVersion = _latestTag.TrimStart('v');
            Logger.Log("AutoUpdate", $"Latest release: {_latestTag} (version: {_latestVersion})");

            // Compare versions
            var currentVersion = settings.Version;
            var cmp = CompareVersions(_latestVersion, currentVersion);
            Logger.Log("AutoUpdate", $"Version comparison: latest={_latestVersion} current={currentVersion} result={cmp}");

            if (cmp > 0)
            {
                Logger.Log("AutoUpdate", $"Update available: {currentVersion} -> {_latestVersion}");

                // Save pending version for UI
                settings = UprootedSettings.Load();
                settings.PendingUpdateVersion = _latestVersion;
                settings.Save();

                // Auto-download and apply
                DownloadAndApply();
            }
            else
            {
                Logger.Log("AutoUpdate", "Already up to date");
            }
        }
        catch (Exception ex)
        {
            _lastError = ex.Message;
            Logger.Log("AutoUpdate", $"Check error: {ex.Message}");
        }
    }

    private void DownloadAndApply()
    {
        var uprootedDir = PlatformPaths.GetUprootedDir();
        var stagingDir = Path.Combine(uprootedDir, "update-staging");

        var isDev = UprootedSettings.Load().AutoUpdateChannel == "developer";
        var downloadBase = isDev ? DevDownloadBase : StableDownloadBase;
        var authToken = isDev ? DecryptPat() : null;

        try
        {
            // Create staging directory
            if (Directory.Exists(stagingDir))
                Directory.Delete(stagingDir, true);
            Directory.CreateDirectory(stagingDir);

            Logger.Log("AutoUpdate", $"Downloading {UpdateFiles.Length} files to staging: {stagingDir}");

            // Download all files to staging
            foreach (var filename in UpdateFiles)
            {
                var url = $"{downloadBase}/{_latestTag}/{filename}";
                Logger.Log("AutoUpdate", $"Downloading {filename}...");

                var bytes = HttpGetBytes(url, authToken);
                if (bytes == null || bytes.Length == 0)
                {
                    _lastError = $"Failed to download {filename}";
                    Logger.Log("AutoUpdate", $"Download failed for {filename}");
                    return; // Abort — don't overwrite any production files
                }

                File.WriteAllBytes(Path.Combine(stagingDir, filename), bytes);
                Logger.Log("AutoUpdate", $"  {filename}: {bytes.Length} bytes");
            }

            // Verify all files present and non-empty
            foreach (var filename in UpdateFiles)
            {
                var path = Path.Combine(stagingDir, filename);
                if (!File.Exists(path))
                {
                    _lastError = $"Missing staged file: {filename}";
                    Logger.Log("AutoUpdate", _lastError);
                    return;
                }
                var info = new FileInfo(path);
                if (info.Length == 0)
                {
                    _lastError = $"Empty staged file: {filename}";
                    Logger.Log("AutoUpdate", _lastError);
                    return;
                }
            }

            // Copy from staging to uprooted dir (overwrite)
            Logger.Log("AutoUpdate", "All files verified, applying update...");
            foreach (var filename in UpdateFiles)
            {
                var src = Path.Combine(stagingDir, filename);
                var dst = Path.Combine(uprootedDir, filename);
                File.Copy(src, dst, overwrite: true);
                Logger.Log("AutoUpdate", $"  Applied: {filename}");
            }

            // Clean up staging
            try { Directory.Delete(stagingDir, true); }
            catch { /* non-fatal */ }

            // Update settings
            var settings = UprootedSettings.Load();
            settings.Version = _latestVersion!;
            settings.PendingUpdateVersion = "";
            settings.Save();

            _updateApplied = true;
            Logger.Log("AutoUpdate", $"Update applied: v{_latestVersion} — restart Root to use new version");
        }
        catch (Exception ex)
        {
            _lastError = $"Apply failed: {ex.Message}";
            Logger.Log("AutoUpdate", $"Download/apply error: {ex.Message}");
            // Leave staging dir for debugging, don't overwrite production files
        }
    }

    // ===== Version comparison =====

    /// <summary>
    /// Compare two version strings. Returns positive if a > b, negative if a &lt; b, 0 if equal.
    /// Handles: "0.3.6", "0.3.6rc", "0.3.7-beta", "0.4.0"
    /// Pre-release suffixes sort below the bare version (0.3.6 > 0.3.6rc).
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

    private static void EnsureHttpResolved()
    {
        if (s_httpResolved) return;
        s_httpResolved = true;

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
                Logger.Log("AutoUpdate", "HttpClient type not found");
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

            Logger.Log("AutoUpdate", $"HTTP resolved: client={s_httpClient != null}, " +
                $"GetStringAsync={s_getStringAsync != null}, " +
                $"GetByteArrayAsync={s_getByteArrayAsync != null}, " +
                $"GetAsync={s_getAsync != null}, " +
                $"SendAsync={s_sendAsync != null}");
        }
        catch (Exception ex)
        {
            Logger.Log("AutoUpdate", $"HTTP resolve error: {ex.Message}");
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
