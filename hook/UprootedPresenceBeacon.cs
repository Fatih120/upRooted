using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Uprooted;

/// <summary>
/// Presence beacon for cross-client Uprooted recognition.
///
/// On startup: registers the local user's UUID with api.uprooted.sh via a
/// HMAC-authenticated POST. Registration uses HMAC-SHA256(uuid:dayNumber, SHARED_SECRET)
/// to prove the client is a legitimate Uprooted install without exposing the UUID.
/// UUIDs are stored as SHA-256 hashes server-side — no plaintext at rest.
///
/// On profile popup: ProfileBadgeInjector calls TryGetCached() or QueryAsync() to check
/// whether the viewed user has Uprooted installed, then injects a small green icon
/// inline next to their username if they do.
///
/// Cache TTLs: active=true → 5 min, active=false → 20s, error → 30s.
/// Registration fires 2s after Initialize(); if session is not ready, retries every
/// 5s for up to 60s before giving up.
///
/// HTTP uses AutoUpdater's shared reflection-based HttpClient (avoids MissingMethodException
/// in Root's trimmed single-file deployment). GET requests use SendAsync with per-request
/// headers to avoid contamination from AutoUpdater's DefaultRequestHeaders (GitHub Accept).
/// </summary>
internal class UprootedPresenceBeacon
{
    private const string Tag = "PresenceBeacon";
    private const string ApiBase = "https://api.uprooted.sh";

    // Keep in sync with StartupHook.CurrentVersion.
    // Can't share the constant — StartupHook is in the global namespace, not Uprooted.
    private const string Version = "0.4.3";

    // HMAC secret for registration proof: HMAC-SHA256(uuid:dayNumber, SECRET)
    // Server stores this same secret in plaintext (via PRESENCE_HMAC_SECRET env var).
    // Hook stores it XOR-obfuscated so the raw key isn't trivially readable in the DLL.
    // Plain key = 4b9e2a71c38f561da4e7305c89f26b14d843972e61b50a7ce328549fc16a378d
    private static readonly byte[] _encryptedKey =
    {
        0x7A, 0x69, 0x46, 0x54, 0x6A, 0xC1, 0xD5, 0x6F, 0x72, 0x76, 0x6F, 0x76, 0x4D, 0x75, 0x72, 0x7A,
        0x6B, 0x6C, 0x43, 0x74, 0x69, 0x7C, 0x7E, 0x9D, 0x7E, 0x7A, 0x7F, 0x67, 0x67, 0x56, 0x47, 0x66,
    };
    private static readonly byte[] _keyXor =
    {
        0x31, 0xF7, 0x6C, 0x25, 0xA9, 0x4E, 0x83, 0x72, 0xD6, 0x91, 0x5F, 0x2A, 0xC4, 0x87, 0x19, 0x6E,
        0xB3, 0x2F, 0xD4, 0x5A, 0x08, 0xC9, 0x74, 0xE1, 0x9D, 0x52, 0x2B, 0xF8, 0xA6, 0x3C, 0x70, 0xEB,
    };

    // HttpMethod.Post resolved from the System.Net.Http assembly (after EnsureHttpResolved)
    private static object? s_httpMethodPost;
    private static Type? s_stringContentType;
    private static bool s_postResolved;
    private static readonly object s_postLock = new();

    // Local presence cache: uuid → (isUprooted, expiry)
    private readonly Dictionary<string, (bool active, DateTime expiry)> _cache = new();
    private readonly object _cacheLock = new();

    private readonly AvaloniaReflection _r;
    private readonly object _mainWindow;
    private string? _ownUuidStr;

    internal UprootedPresenceBeacon(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _mainWindow = mainWindow;
    }

    /// <summary>
    /// Own UUID once acquired (lowercase). Null until registration succeeds.
    /// Used by ProfileBadgeInjector to skip presence icon on own profile.
    /// </summary>
    internal string? OwnUuidStr => _ownUuidStr;

    /// <summary>
    /// Start the beacon. Registers in the background after a brief 2s delay.
    /// If the session is not ready, retries every 5s for up to 60s.
    /// </summary>
    internal void Initialize()
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            Thread.Sleep(2_000); // Brief wait for Root's session to authenticate
            TryRegister();
        });
    }

    // ===== Public query API (called by ProfileBadgeInjector) =====

    /// <summary>
    /// Return cached presence result for a UUID without a network call.
    /// Returns null if not cached (or cache expired), true/false if cached.
    /// </summary>
    internal bool? TryGetCached(string uuid)
    {
        lock (_cacheLock)
        {
            if (_cache.TryGetValue(uuid, out var entry))
            {
                if (DateTime.UtcNow < entry.expiry)
                    return entry.active;
                _cache.Remove(uuid);
            }
        }
        return null;
    }

    /// <summary>
    /// Query the server in the background for a UUID's presence.
    /// Calls onResult(true/false) on a thread pool thread when complete.
    /// Caches the result; active=true TTL 5min, active=false TTL 20s, error TTL 30s.
    /// </summary>
    internal void QueryAsync(string uuid, Action<bool> onResult)
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            bool result = false;
            TimeSpan ttl = TimeSpan.FromSeconds(30); // error TTL — retry soon on failure
            try
            {
                result = FetchPresence(uuid);
                // Short false-TTL so newly-installed users appear quickly on next popup open
                ttl = result ? TimeSpan.FromMinutes(5) : TimeSpan.FromSeconds(20);
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"QueryAsync error for {uuid}: {ex.Message}");
            }

            lock (_cacheLock)
                _cache[uuid] = (result, DateTime.UtcNow + ttl);

            try { onResult(result); }
            catch { }
        });
    }

    /// <summary>
    /// Invalidate a cached entry so the next check re-queries the server.
    /// </summary>
    internal void InvalidateCache(string uuid)
    {
        lock (_cacheLock) { _cache.Remove(uuid); }
    }

    // ===== Registration =====

    private void TryRegister()
    {
        try
        {
            // Poll for own UUID — session may not have authenticated yet.
            // Root shows a login screen first; HomeViewModel only appears after auth.
            // 60 retries × 5s = 5 minutes — enough for slow logins or 2FA.
            const int maxRetries = 60;
            for (int attempt = 0; attempt <= maxRetries; attempt++)
            {
                if (attempt > 0) Thread.Sleep(5_000);
                _ownUuidStr = TryGetOwnUuid();
                if (_ownUuidStr != null) break;
                if (attempt == 0)
                    Logger.Log(Tag, "Own UUID not ready — will retry every 5s (up to 5 min)");
            }

            if (_ownUuidStr == null)
            {
                Logger.Log(Tag, "Own UUID not found after 5 min — registration skipped");
                return;
            }

            Logger.Log(Tag, $"Registering presence for UUID {_ownUuidStr[..8]}...");

            var utcDayNumber = (int)((DateTime.UtcNow - DateTime.UnixEpoch).TotalDays);
            var message = $"{_ownUuidStr}:{utcDayNumber}";
            var hmacHex = ComputeHmacHex(message, DecryptKey());

            // Build JSON manually (no System.Text.Json)
            var body = $"{{\"uuid\":\"{_ownUuidStr}\",\"token\":\"{hmacHex}\",\"ts\":{utcDayNumber},\"v\":\"{Version}\"}}";

            var statusCode = HttpPost($"{ApiBase}/presence/register", body);

            if (statusCode is 200 or 204)
                Logger.Log(Tag, "Registered successfully");
            else if (statusCode == 429)
                Logger.Log(Tag, "Registration rate-limited (already registered recently — OK)");
            else
                Logger.Log(Tag, $"Registration failed: HTTP {statusCode}");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Registration error: {ex.Message}");
        }
    }

    // ===== Own UUID discovery =====

    /// <summary>
    /// Walk the visual tree to find HomeViewModel, then resolve:
    ///   HomeViewModel → RootSessionAccessor/SessionAccessor/Session → UserInfoService → SessionUser → Id
    /// Returns lowercase UUID string, or null if the session is not yet authenticated.
    /// </summary>
    private string? TryGetOwnUuid()
    {
        try
        {
            // Visual tree walk must run on UI thread. RunOnUIThread is fire-and-forget
            // (Dispatcher.Post), so we need a ManualResetEventSlim to block until the
            // UI thread finishes before reading result. Without this, result is always
            // null because the calling thread races ahead of the dispatch.
            string? result = null;
            using var done = new ManualResetEventSlim(false);
            _r.RunOnUIThread(() =>
            {
                try { result = TryGetOwnUuidOnUIThread(); }
                catch (Exception ex) { Logger.Log(Tag, $"TryGetOwnUuid UI error: {ex.Message}"); }
                finally { done.Set(); }
            });
            // 5s safety timeout — prevents deadlock if dispatcher is blocked at startup
            if (!done.Wait(TimeSpan.FromSeconds(5)))
                Logger.Log(Tag, "TryGetOwnUuid: UI dispatch timed out");
            return result;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"TryGetOwnUuid error: {ex.Message}");
            return null;
        }
    }

    private string? TryGetOwnUuidOnUIThread()
    {
        object? homeViewModel = null;
        var walker = new VisualTreeWalker(_r);
        foreach (var node in walker.DescendantsDepthFirst(_mainWindow))
        {
            var dc = _r.GetDataContext(node);
            if (dc?.GetType().Name.Contains("HomeViewModel") == true)
            {
                homeViewModel = dc;
                break;
            }
        }

        if (homeViewModel == null)
        {
            Logger.Log(Tag, "TryGetOwnUuid: HomeViewModel not found in visual tree");
            return null;
        }

        // Walk: HomeViewModel → accessor → Session → UserInfoService → SessionUser → Id
        foreach (var accProp in new[] { "RootSessionAccessor", "SessionAccessor", "Session" })
        {
            var accessor = _r.GetPropertyValue(homeViewModel, accProp);
            if (accessor == null) continue;
            var session = accProp == "Session" ? accessor : _r.GetPropertyValue(accessor, "Session");
            if (session == null)
            {
                Logger.Log(Tag, $"TryGetOwnUuid: Session null via {accProp} (not authenticated yet?)");
                continue;
            }
            var userInfoService = _r.GetPropertyValue(session, "UserInfoService");
            if (userInfoService == null)
            {
                Logger.Log(Tag, $"TryGetOwnUuid: UserInfoService null on {session.GetType().Name}");
                continue;
            }
            var sessionUser = _r.GetPropertyValue(userInfoService, "SessionUser");
            if (sessionUser == null)
            {
                Logger.Log(Tag, $"TryGetOwnUuid: SessionUser null on {userInfoService.GetType().Name}");
                continue;
            }
            var id = _r.GetPropertyValue(sessionUser, "Id");
            if (id == null)
            {
                Logger.Log(Tag, $"TryGetOwnUuid: Id null on {sessionUser.GetType().Name}");
                continue;
            }
            var idStr = id.ToString()?.ToLowerInvariant();
            if (!string.IsNullOrEmpty(idStr)) return idStr;
        }

        return null;
    }

    // ===== Network =====

    private bool FetchPresence(string uuid)
    {
        var url = $"{ApiBase}/presence/{uuid}";
        var json = HttpGetPresence(url);
        if (json == null) return false;

        return json.Contains("\"active\":true", StringComparison.OrdinalIgnoreCase)
            || json.Contains("\"active\": true", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// HTTP GET via SendAsync with per-request Accept header.
    /// Uses SendAsync instead of GetAsync to avoid inheriting AutoUpdater's
    /// DefaultRequestHeaders (which include Accept: application/vnd.github+json).
    /// </summary>
    private static string? HttpGetPresence(string url)
    {
        try
        {
            AutoUpdater.EnsureHttpResolved();
            EnsurePostResolved(); // Resolves s_httpMethodPost which we need for GET too

            if (AutoUpdater.s_httpClient == null || AutoUpdater.s_sendAsync == null
                || AutoUpdater.s_httpRequestMessageType == null || s_httpMethodPost == null)
            {
                Logger.Log(Tag, "HttpGetPresence: HTTP not resolved");
                return null;
            }

            // Resolve HttpMethod.Get from the already-resolved assembly
            var httpAsm = AutoUpdater.s_httpRequestMessageType.Assembly;
            var httpMethodType = httpAsm.GetType("System.Net.Http.HttpMethod");
            var httpMethodGet = httpMethodType?.GetProperty("Get",
                BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
            if (httpMethodGet == null) return null;

            // Create per-request HttpRequestMessage with clean Accept header
            var request = Activator.CreateInstance(AutoUpdater.s_httpRequestMessageType, httpMethodGet, new Uri(url));
            if (request == null) return null;

            // Set Accept: application/json on the per-request headers
            try
            {
                var headers = request.GetType().GetProperty("Headers")?.GetValue(request);
                var tryAdd = headers?.GetType().GetMethod("TryAddWithoutValidation",
                    new[] { typeof(string), typeof(string) });
                tryAdd?.Invoke(headers, new object[] { "Accept", "application/json" });
            }
            catch { }

            var sendParams = AutoUpdater.s_sendAsync.GetParameters();
            object?[] args = sendParams.Length == 1
                ? new[] { request }
                : new object?[] { request, System.Threading.CancellationToken.None };

            var task = AutoUpdater.s_sendAsync.Invoke(AutoUpdater.s_httpClient, args);
            var response = task?.GetType().GetProperty("Result")?.GetValue(task);
            if (response == null) return null;

            var statusCodeProp = response.GetType().GetProperty("StatusCode");
            var statusCode = statusCodeProp != null ? Convert.ToInt32(statusCodeProp.GetValue(response)) : -1;
            if (statusCode >= 0 && (statusCode < 200 || statusCode >= 300))
            {
                Logger.Log(Tag, $"GET {url} returned HTTP {statusCode}");
                return null;
            }

            var content = response.GetType().GetProperty("Content")?.GetValue(response);
            if (content == null) return null;

            var readTask = content.GetType().GetMethod("ReadAsStringAsync",
                BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)?.Invoke(content, null);
            return readTask?.GetType().GetProperty("Result")?.GetValue(readTask) as string;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"HttpGetPresence error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// HTTP POST with JSON body via reflection-based HttpClient.
    /// Returns HTTP status code, or -1 on error.
    /// </summary>
    private static int HttpPost(string url, string jsonBody)
    {
        try
        {
            AutoUpdater.EnsureHttpResolved();
            EnsurePostResolved();

            if (AutoUpdater.s_httpClient == null || AutoUpdater.s_sendAsync == null
                || AutoUpdater.s_httpRequestMessageType == null || s_httpMethodPost == null)
            {
                Logger.Log(Tag, "HttpPost: HTTP not resolved");
                return -1;
            }

            var request = Activator.CreateInstance(AutoUpdater.s_httpRequestMessageType, s_httpMethodPost, new Uri(url));
            if (request == null) return -1;

            // Create StringContent(jsonBody, Encoding.UTF8, "application/json") or ByteArrayContent fallback
            object? bodyContent = null;

            if (s_stringContentType != null)
            {
                try { bodyContent = Activator.CreateInstance(s_stringContentType, jsonBody, Encoding.UTF8, "application/json"); }
                catch { }
            }

            if (bodyContent == null)
            {
                var httpAsm = AutoUpdater.s_httpRequestMessageType.Assembly;
                var byteArrayContentType = httpAsm.GetType("System.Net.Http.ByteArrayContent");
                if (byteArrayContentType != null)
                {
                    var bytes = Encoding.UTF8.GetBytes(jsonBody);
                    bodyContent = Activator.CreateInstance(byteArrayContentType, bytes);
                    if (bodyContent != null)
                    {
                        var headers = bodyContent.GetType().GetProperty("Headers")?.GetValue(bodyContent);
                        var contentType = headers?.GetType().GetProperty("ContentType");
                        if (contentType != null)
                        {
                            var mediaHeaderType = httpAsm.GetType("System.Net.Http.Headers.MediaTypeHeaderValue");
                            if (mediaHeaderType != null)
                            {
                                var mediaHeader = Activator.CreateInstance(mediaHeaderType, "application/json");
                                contentType.SetValue(headers, mediaHeader);
                            }
                        }
                    }
                }
            }

            if (bodyContent == null)
            {
                Logger.Log(Tag, "HttpPost: cannot create body content");
                return -1;
            }

            request.GetType().GetProperty("Content")?.SetValue(request, bodyContent);

            var sendParams = AutoUpdater.s_sendAsync.GetParameters();
            object?[] args = sendParams.Length == 1
                ? new[] { request }
                : new object?[] { request, System.Threading.CancellationToken.None };

            var task = AutoUpdater.s_sendAsync.Invoke(AutoUpdater.s_httpClient, args);
            var response = task?.GetType().GetProperty("Result")?.GetValue(task);
            if (response == null) return -1;

            var statusCodeProp = response.GetType().GetProperty("StatusCode");
            return statusCodeProp != null ? Convert.ToInt32(statusCodeProp.GetValue(response)) : -1;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"HttpPost error: {ex.Message}");
            return -1;
        }
    }

    /// <summary>
    /// Resolve HttpMethod.Post and StringContent type from the System.Net.Http assembly.
    /// Called once after AutoUpdater.EnsureHttpResolved().
    /// </summary>
    internal static void EnsurePostResolved()
    {
        lock (s_postLock)
        {
            if (s_postResolved) return;
            s_postResolved = true;

            if (AutoUpdater.s_httpRequestMessageType == null) return;

            try
            {
                var httpAsm = AutoUpdater.s_httpRequestMessageType.Assembly;
                var httpMethodType = httpAsm.GetType("System.Net.Http.HttpMethod");
                s_httpMethodPost = httpMethodType?.GetProperty("Post",
                    BindingFlags.Public | BindingFlags.Static)?.GetValue(null);

                s_stringContentType = httpAsm.GetType("System.Net.Http.StringContent");

                Logger.Log(Tag, $"Post resolved: HttpMethod.Post={s_httpMethodPost != null}, StringContent={s_stringContentType != null}");
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"EnsurePostResolved error: {ex.Message}");
            }
        }
    }

    // ===== HMAC + key =====

    internal static byte[] DecryptKey()
    {
        var result = new byte[_encryptedKey.Length];
        for (int i = 0; i < _encryptedKey.Length; i++)
            result[i] = (byte)(_encryptedKey[i] ^ _keyXor[i]);
        return result;
    }

    internal static string ComputeHmacHex(string message, byte[] key)
    {
        using var hmac = new HMACSHA256(key);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}
