using System.Reflection;
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
    private const string GitHubApiUrl = "https://api.github.com/repos/watchthelight/uprooted/releases/latest";
    private const string DownloadBaseUrl = "https://github.com/watchthelight/uprooted/releases/download";
    private const int CheckIntervalHours = 6;
    private const int HttpTimeoutSeconds = 30;

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
    private volatile bool _checking;
    private Timer? _periodicTimer;

    // Singleton for UI access
    internal static AutoUpdater? Instance { get; set; }

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
                RunCheck();
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
                        RunCheck();
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
        if (_checking) return;
        RunCheck();
    }

    /// <summary>
    /// Returns (statusText, statusColor) for the UI.
    /// </summary>
    internal (string text, string color) GetStatus()
    {
        if (_checking)
            return ("Checking for updates...", ContentPages.TextMuted);

        if (_updateApplied)
            return ($"Updated to v{_latestVersion} \u2014 restart Root to apply", ContentPages.AccentGreen);

        if (_lastError != null)
            return ($"Check failed: {_lastError}", "#E04040");

        if (_latestVersion != null)
        {
            var current = UprootedSettings.Load().Version;
            var cmp = CompareVersions(_latestVersion, current);
            if (cmp > 0)
                return ($"Update available: v{_latestVersion}", "#C0A820");
        }

        var currentVersion = UprootedSettings.Load().Version;
        return ($"Up to date (v{currentVersion})", ContentPages.AccentGreen);
    }

    internal bool IsChecking => _checking;
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
        if (_checking) return;
        _checking = true;
        _lastError = null;

        try
        {
            Logger.Log("AutoUpdate", "Checking for updates...");

            // Update last check timestamp immediately (we attempted a check regardless of outcome)
            var settings = UprootedSettings.Load();
            settings.LastUpdateCheck = DateTime.UtcNow.ToString("o");
            settings.Save();

            // Hit GitHub API
            var json = HttpGetString(GitHubApiUrl);
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
        finally
        {
            _checking = false;
        }
    }

    private void DownloadAndApply()
    {
        var uprootedDir = PlatformPaths.GetUprootedDir();
        var stagingDir = Path.Combine(uprootedDir, "update-staging");

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
                var url = $"{DownloadBaseUrl}/{_latestTag}/{filename}";
                Logger.Log("AutoUpdate", $"Downloading {filename}...");

                var bytes = HttpGetBytes(url);
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

    private static string? HttpGetString(string url)
    {
        EnsureHttpResolved();
        if (s_httpClient == null) return null;

        // Try GetStringAsync first
        if (s_getStringAsync != null)
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

        // Fallback: SendAsync
        if (s_sendAsync != null && s_httpRequestMessageType != null && s_httpMethodGet != null)
        {
            try
            {
                var request = Activator.CreateInstance(s_httpRequestMessageType, s_httpMethodGet, new Uri(url));
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

    private static byte[]? HttpGetBytes(string url)
    {
        EnsureHttpResolved();
        if (s_httpClient == null) return null;

        // Try GetByteArrayAsync first
        if (s_getByteArrayAsync != null)
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

        // Fallback: GetAsync or SendAsync -> stream -> bytes
        try
        {
            object? response = null;

            if (s_getAsync != null)
            {
                var paramType = s_getAsync.GetParameters()[0].ParameterType;
                object arg = paramType == typeof(Uri) ? new Uri(url) : (object)url;
                var task = s_getAsync.Invoke(s_httpClient, new[] { arg });
                response = task?.GetType().GetProperty("Result")?.GetValue(task);
            }

            if (response == null && s_sendAsync != null && s_httpRequestMessageType != null && s_httpMethodGet != null)
            {
                var request = Activator.CreateInstance(s_httpRequestMessageType, s_httpMethodGet, new Uri(url));
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
