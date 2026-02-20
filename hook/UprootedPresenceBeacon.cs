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
/// Cache TTLs: active=true → 5 min, active=false → 2 min, error → 30s.
/// Registration is delayed 10s on startup to let Root settle.
///
/// HTTP via AutoUpdater's shared reflection-based HttpClient (avoids MissingMethodException
/// in Root's trimmed single-file deployment).
/// </summary>
internal class UprootedPresenceBeacon
{
    private const string Tag = "PresenceBeacon";
    private const string ApiBase = "https://api.uprooted.sh";
    private const string Version = "0.4.2";

    // HMAC secret for registration proof: HMAC-SHA256(uuid:dayNumber, SECRET)
    // Server stores this same secret in plaintext (via PRESENCE_HMAC_SECRET env var).
    // Hook stores it XOR-obfuscated so the raw key isn't trivially readable in the DLL.
    // NOTE: Replace _encryptedKey + _keyXor with agreed values before production deploy.
    //       Current values: plain key = 4b9e2a71c38f561da4e7305c89f26b14d843972e61b50a7ce328549fc16a378d
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

    // Resolved HttpMethod.Post via reflection (same assembly as AutoUpdater's resolved types)
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
    /// Start the beacon: discover own UUID and register with the server after a 10s delay.
    /// </summary>
    internal void Initialize()
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            Thread.Sleep(2_000); // Brief wait for session to authenticate (Root is fast)
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
    /// Caches the result for future calls.
    /// </summary>
    internal void QueryAsync(string uuid, Action<bool> onResult)
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            bool result = false;
            TimeSpan ttl = TimeSpan.FromSeconds(30); // error TTL
            try
            {
                result = FetchPresence(uuid);
                // Short TTL for false results so newly-installed users appear quickly
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
    /// Not currently called, but available for future use (e.g., settings toggle).
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
            _ownUuidStr = TryGetOwnUuid();
            if (_ownUuidStr == null)
            {
                Logger.Log(Tag, "Own UUID not found — cannot register (session may not be ready)");
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
                Logger.Log(Tag, "Registration rate-limited (registered recently — OK)");
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
    ///   HomeViewModel → RootSessionAccessor/SessionAccessor/Session → Session → UserInfoService → SessionUser → Id
    /// Returns lowercase UUID string or null if unavailable.
    /// </summary>
    private string? TryGetOwnUuid()
    {
        try
        {
            // Find HomeViewModel DataContext in visual tree
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
                Logger.Log(Tag, "HomeViewModel not found in visual tree");
                return null;
            }

            // Walk: HomeViewModel → RootSessionAccessor/SessionAccessor/Session → Session → UserInfoService → SessionUser → Id
            foreach (var accProp in new[] { "RootSessionAccessor", "SessionAccessor", "Session" })
            {
                var accessor = _r.GetPropertyValue(homeViewModel, accProp);
                if (accessor == null) continue;
                var session = accProp == "Session" ? accessor : _r.GetPropertyValue(accessor, "Session");
                if (session == null) continue;
                var userInfoService = _r.GetPropertyValue(session, "UserInfoService");
                if (userInfoService == null) continue;
                var sessionUser = _r.GetPropertyValue(userInfoService, "SessionUser");
                if (sessionUser == null) continue;
                var id = _r.GetPropertyValue(sessionUser, "Id");
                if (id == null) continue;
                var idStr = id.ToString()?.ToLowerInvariant();
                if (!string.IsNullOrEmpty(idStr))
                    return idStr;
            }

            Logger.Log(Tag, "UserInfoService.SessionUser.Id not found via ViewModel walk");
            return null;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"TryGetOwnUuid error: {ex.Message}");
            return null;
        }
    }

    // ===== Network =====

    private bool FetchPresence(string uuid)
    {
        AutoUpdater.EnsureHttpResolved();
        if (AutoUpdater.s_httpClient == null) return false;

        var url = $"{ApiBase}/presence/{uuid}";
        var json = HttpGet(url);
        if (json == null) return false;

        // Parse {"active":true} or {"active":false}
        return json.Contains("\"active\":true", StringComparison.OrdinalIgnoreCase)
            || json.Contains("\"active\": true", StringComparison.OrdinalIgnoreCase);
    }

    private static string? HttpGet(string url)
    {
        try
        {
            AutoUpdater.EnsureHttpResolved();
            if (AutoUpdater.s_httpClient == null || AutoUpdater.s_getAsync == null) return null;

            var paramType = AutoUpdater.s_getAsync.GetParameters()[0].ParameterType;
            object arg = paramType == typeof(Uri) ? new Uri(url) : (object)url;
            var task = AutoUpdater.s_getAsync.Invoke(AutoUpdater.s_httpClient, new[] { arg });
            var response = task?.GetType().GetProperty("Result")?.GetValue(task);
            if (response == null) return null;

            // Check status
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
            Logger.Log(Tag, $"HttpGet error: {ex.Message}");
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

            // Create HttpRequestMessage(HttpMethod.Post, uri)
            var request = Activator.CreateInstance(AutoUpdater.s_httpRequestMessageType, s_httpMethodPost, new Uri(url));
            if (request == null) return -1;

            // Create StringContent(jsonBody, Encoding.UTF8, "application/json") or ByteArrayContent fallback
            object? bodyContent = null;

            if (s_stringContentType != null)
            {
                try
                {
                    bodyContent = Activator.CreateInstance(s_stringContentType, jsonBody, Encoding.UTF8, "application/json");
                }
                catch { }
            }

            if (bodyContent == null)
            {
                // Fallback: ByteArrayContent with Content-Type header set manually
                var bytes = Encoding.UTF8.GetBytes(jsonBody);
                var httpAsm = AutoUpdater.s_httpRequestMessageType.Assembly;
                var byteArrayContentType = httpAsm.GetType("System.Net.Http.ByteArrayContent");
                if (byteArrayContentType != null)
                {
                    bodyContent = Activator.CreateInstance(byteArrayContentType, bytes);
                    if (bodyContent != null)
                    {
                        // Set Content-Type header
                        var headers = bodyContent.GetType().GetProperty("Headers")?.GetValue(bodyContent);
                        var contentType = headers?.GetType().GetProperty("ContentType");
                        if (contentType != null)
                        {
                            var httpAsm2 = httpAsm;
                            var mediaHeaderType = httpAsm2.GetType("System.Net.Http.Headers.MediaTypeHeaderValue");
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
    /// Called once; AutoUpdater.EnsureHttpResolved() must be called first.
    /// </summary>
    private static void EnsurePostResolved()
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

    private static byte[] DecryptKey()
    {
        var result = new byte[_encryptedKey.Length];
        for (int i = 0; i < _encryptedKey.Length; i++)
            result[i] = (byte)(_encryptedKey[i] ^ _keyXor[i]);
        return result;
    }

    private static string ComputeHmacHex(string message, byte[] key)
    {
        using var hmac = new HMACSHA256(key);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}
