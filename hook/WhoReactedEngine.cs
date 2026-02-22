using System.Collections;
using System.Reflection;

namespace Uprooted;

/// <summary>
/// WhoReacted plugin — renders small circular user avatars next to each reaction pill
/// in the chat message list. Inspired by Vencord's whoReacted plugin for Discord.
///
/// Root's chat is Avalonia-native. Each message's reactions are rendered as ReactionView
/// controls inside an ItemsControl (Row 6 of the message Grid). Each ReactionView contains
/// a ReactionViewModel with a Reaction domain object holding a HashSet&lt;UserGuid&gt; of
/// reactor user IDs.
///
/// Pipeline (avoids blocking UI thread with HTTP):
///   1. UI thread: scan visual tree, find unprocessed ReactionViews, resolve user IDs + avatar URIs
///   2. ThreadPool: download avatar bytes for any URIs not already cached
///   3. UI thread: create Image controls, inject avatar panels into reaction Grid
/// </summary>
internal sealed class WhoReactedEngine : IDisposable
{
    private const string Tag = "WhoReacted";
    private const int ScanIntervalMs = 500;
    private const int MaxAvatarsPerReaction = 5;
    private const double AvatarSize = 16.0;
    private const double AvatarOverlap = -4.0;
    private const string TagPrefix = "uprooted-whoreacted:";

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly object _mainWindow;

    private Timer? _scanTimer;
    private int _scanning;
    private readonly TailSampler _sampler = new(heartbeatTicks: 60, slowThresholdMs: 50);

    // Avatar byte cache: ProfilePictureUri → downloaded bytes (thread-safe access via lock)
    private const int MaxAvatarBytesCache = 200;
    private readonly Dictionary<string, byte[]> _avatarBytesCache = new();
    private readonly object _bytesCacheLock = new();

    // Avatar bitmap cache: ProfilePictureUri → Bitmap object (UI thread only)
    private readonly Dictionary<string, object> _avatarBitmapCache = new();

    // Failed URIs (don't retry on every tick)
    private readonly HashSet<string> _failedUris = new();

    // HTTP for avatar downloads (same reflection pattern as LinkEmbedEngine)
    private static object? s_httpClient;
    private static MethodInfo? s_getByteArrayAsync;
    private static bool s_httpResolved;

    // Reflection caches for Root's domain types (resolved once)
    private PropertyInfo? _vmReactionProp;           // ReactionViewModel.Reaction
    private FieldInfo? _vmUserCacheField;             // ReactionViewModel._globalUserCacheService
    private PropertyInfo? _reactionUsersProp;         // Reaction.Users
    private PropertyInfo? _reactionShortCodeProp;     // Reaction.ShortCode
    private PropertyInfo? _reactionMessageIdProp;     // Reaction.MessageId
    private MethodInfo? _getUsersByIdsSync;            // IGlobalUserCacheService.GetUsersByIds
    private PropertyInfo? _globalUserProfilePicProp;  // GlobalUser.ProfilePictureUri
    private PropertyInfo? _globalUserIdProp;           // GlobalUser.Id
    private bool _reflectionResolved;

    // Pending injection data (collected on UI thread, consumed after download)
    private sealed class PendingInjection
    {
        public object ReactionView = null!;
        public object InnerGrid = null!;
        public string FullTag = "";
        public List<string> AvatarUris = new();
    }

    internal WhoReactedEngine(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _mainWindow = mainWindow;
    }

    internal void Initialize()
    {
        using var ev = WideEvent.Begin(Tag, "initialize");

        EnsureHttpResolved();
        ev.Set("http_resolved", s_httpClient != null);

        _scanTimer = new Timer(OnScanTick, null, 0, ScanIntervalMs);
        ev.Set("scan_interval_ms", ScanIntervalMs);
    }

    public void Dispose()
    {
        var t = _scanTimer;
        _scanTimer = null;
        t?.Dispose();
    }

    // ===== Timer callback =====

    private void OnScanTick(object? state)
    {
        if (Interlocked.CompareExchange(ref _scanning, 1, 0) != 0) return;
        var ev = WideEvent.BeginSampled(Tag, "scan_tick", _sampler);
        try
        {
            var settings = UprootedSettings.Load();
            if (settings.Plugins.TryGetValue("who-reacted", out var enabled) && !enabled)
            {
                ev.Set("skipped", "disabled");
                ev.Dispose();
                Interlocked.Exchange(ref _scanning, 0);
                return;
            }

            // Phase 1: Collect pending reactions on UI thread
            var pending = new List<PendingInjection>();
            _r.RunOnUIThread(() =>
            {
                try
                {
                    CollectPendingReactions(pending, ev);
                }
                catch (Exception ex)
                {
                    ev.SetError(ex);
                }

                if (pending.Count == 0)
                {
                    ev.Dispose();
                    Interlocked.Exchange(ref _scanning, 0);
                    return;
                }

                // Phase 2: Download avatars on ThreadPool (non-blocking)
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        DownloadPendingAvatars(pending, ev);
                    }
                    catch (Exception ex)
                    {
                        ev.Set("download_error", ex.Message);
                    }

                    // Phase 3: Inject on UI thread
                    _r.RunOnUIThread(() =>
                    {
                        try
                        {
                            InjectPendingAvatars(pending, ev);
                        }
                        catch (Exception ex)
                        {
                            ev.SetError(ex);
                        }
                        finally
                        {
                            ev.Dispose();
                            Interlocked.Exchange(ref _scanning, 0);
                        }
                    });
                });
            });
        }
        catch (Exception ex)
        {
            ev.SetError(ex);
            ev.Dispose();
            Interlocked.Exchange(ref _scanning, 0);
        }
    }

    // ===== Phase 1: Collect pending reactions (UI thread) =====

    private void CollectPendingReactions(List<PendingInjection> pending, WideEvent ev)
    {
        int scanned = 0;
        int alreadyProcessed = 0;

        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            if (node.GetType().Name != "ReactionView") continue;
            scanned++;

            // Already processed?
            var tag = _r.GetTag(node);
            if (tag != null && tag.StartsWith(TagPrefix, StringComparison.Ordinal))
            {
                alreadyProcessed++;
                continue;
            }

            // Get DataContext (ReactionViewModel)
            var vm = _r.GetDataContext(node);
            if (vm == null || !vm.GetType().Name.Contains("ReactionViewModel")) continue;

            // Resolve reflection paths once
            if (!_reflectionResolved) ResolveReflection(vm);
            if (_vmReactionProp == null) continue;

            // Get Reaction domain object
            var reaction = _vmReactionProp.GetValue(vm);
            if (reaction == null) continue;

            // Get Users HashSet
            var users = _reactionUsersProp?.GetValue(reaction);
            if (users == null) continue;

            // Build tag
            var shortCode = _reactionShortCodeProp?.GetValue(reaction) as string ?? "?";
            var messageId = _reactionMessageIdProp?.GetValue(reaction);
            var msgIdStr = messageId?.ToString() ?? "?";
            var fullTag = $"{TagPrefix}{msgIdStr}:{shortCode}";

            // Tag immediately to prevent re-processing on next tick
            _r.SetTag(node, fullTag);

            // Resolve user IDs → avatar URIs
            var userCacheService = _vmUserCacheField?.GetValue(vm);
            if (userCacheService == null || _getUsersByIdsSync == null) continue;

            var userIds = new List<object>();
            if (users is IEnumerable enumerable)
            {
                foreach (var userId in enumerable)
                {
                    userIds.Add(userId);
                    if (userIds.Count >= MaxAvatarsPerReaction) break;
                }
            }
            if (userIds.Count == 0) continue;

            // Resolve GlobalUser objects (synchronous — this is a cache lookup, not a network call)
            List<string> avatarUris;
            try
            {
                var result = _getUsersByIdsSync.Invoke(userCacheService, new object[] { userIds });
                avatarUris = ExtractAvatarUris(result);
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"GetUsersByIds failed: {ex.Message}");
                continue;
            }
            if (avatarUris.Count == 0) continue;

            // Find inner Grid
            var innerGrid = FindInnerGrid(node);
            if (innerGrid == null) continue;

            pending.Add(new PendingInjection
            {
                ReactionView = node,
                InnerGrid = innerGrid,
                FullTag = fullTag,
                AvatarUris = avatarUris
            });
        }

        ev.Set("reactions_scanned", scanned);
        ev.Set("already_processed", alreadyProcessed);
        ev.Set("pending", pending.Count);
    }

    private List<string> ExtractAvatarUris(object? result)
    {
        var uris = new List<string>();
        if (result == null) return uris;

        IEnumerable enumerable;
        if (result is IEnumerable e)
            enumerable = e;
        else
            return uris;

        foreach (var user in enumerable)
        {
            if (user == null) continue;
            var uri = _globalUserProfilePicProp?.GetValue(user) as string;
            if (!string.IsNullOrEmpty(uri))
                uris.Add(uri!);
        }
        return uris;
    }

    // ===== Phase 2: Download avatars (ThreadPool) =====

    private void DownloadPendingAvatars(List<PendingInjection> pending, WideEvent ev)
    {
        int downloaded = 0;
        int cached = 0;

        // Collect all unique URIs that need downloading
        var needed = new HashSet<string>();
        foreach (var p in pending)
            foreach (var uri in p.AvatarUris)
            {
                lock (_bytesCacheLock)
                {
                    if (_avatarBytesCache.ContainsKey(uri)) { cached++; continue; }
                }
                if (_failedUris.Contains(uri)) continue;
                needed.Add(uri);
            }

        // Download each unique URI
        foreach (var uri in needed)
        {
            var bytes = DownloadBytes(uri);
            if (bytes != null && bytes.Length > 0)
            {
                lock (_bytesCacheLock)
                {
                    if (_avatarBytesCache.Count >= MaxAvatarBytesCache)
                    {
                        var firstKey = _avatarBytesCache.Keys.First();
                        _avatarBytesCache.Remove(firstKey);
                    }
                    _avatarBytesCache[uri] = bytes;
                }
                downloaded++;
            }
            else
            {
                _failedUris.Add(uri);
            }
        }

        ev.Set("avatars_downloaded", downloaded);
        ev.Set("avatars_cached", cached);
    }

    // ===== Phase 3: Inject avatars (UI thread) =====

    private void InjectPendingAvatars(List<PendingInjection> pending, WideEvent ev)
    {
        int injected = 0;

        foreach (var p in pending)
        {
            try
            {
                // Verify the ReactionView is still in the visual tree
                var currentTag = _r.GetTag(p.ReactionView);
                if (currentTag != p.FullTag) continue;

                // Add columns to the inner Grid: spacer (4px) + avatars (Auto)
                _r.AddGridColumnPixel(p.InnerGrid, 4);
                _r.AddGridColumnAuto(p.InnerGrid);

                // Create horizontal StackPanel for avatars
                var avatarPanel = _r.CreateStackPanel(vertical: false, spacing: 0);
                if (avatarPanel == null) continue;

                _r.SetTag(avatarPanel, p.FullTag);
                SetVerticalAlignment(avatarPanel, "Center");
                _r.SetGridColumn(avatarPanel, 4);

                int added = 0;
                foreach (var uri in p.AvatarUris)
                {
                    if (added >= MaxAvatarsPerReaction) break;

                    var avatar = CreateCircularAvatar(uri);
                    if (avatar == null) continue;

                    if (added > 0)
                        _r.SetMargin(avatar, AvatarOverlap, 0, 0, 0);

                    _r.AddChild(avatarPanel, avatar);
                    added++;
                }

                if (added > 0)
                {
                    _r.AddChild(p.InnerGrid, avatarPanel);
                    injected++;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"Inject error: {ex.Message}");
            }
        }

        ev.Set("injected", injected);
    }

    // ===== Visual tree helpers =====

    /// <summary>
    /// ReactionView structure: UserControl > Panel > [borders..., Button "ReactionBorder"]
    ///   Button.Content = Grid [Col0: emoji 16px, Col1: spacer 10px, Col2: count Auto]
    /// </summary>
    private object? FindInnerGrid(object reactionView)
    {
        try
        {
            foreach (var child in _r.GetVisualChildren(reactionView))
            {
                foreach (var panelChild in _r.GetVisualChildren(child))
                {
                    if (panelChild.GetType().Name == "Button")
                    {
                        var content = panelChild.GetType().GetProperty("Content")?.GetValue(panelChild);
                        if (content != null && content.GetType().Name == "Grid")
                            return content;
                    }
                }
            }
        }
        catch { }
        return null;
    }

    private void SetVerticalAlignment(object control, string alignment)
    {
        try
        {
            if (_r.VerticalAlignmentType == null) return;
            var val = Enum.Parse(_r.VerticalAlignmentType, alignment);
            control.GetType().GetProperty("VerticalAlignment")?.SetValue(control, val);
        }
        catch { }
    }

    // ===== Create circular avatar control (UI thread, uses byte cache) =====

    private object? CreateCircularAvatar(string profilePictureUri)
    {
        try
        {
            // Get or create Bitmap from cached bytes
            if (!_avatarBitmapCache.TryGetValue(profilePictureUri, out var bitmap))
            {
                byte[]? bytes;
                lock (_bytesCacheLock)
                {
                    _avatarBytesCache.TryGetValue(profilePictureUri, out bytes);
                }
                if (bytes == null || bytes.Length == 0) return null;

                using var stream = new System.IO.MemoryStream(bytes);
                bitmap = _r.CreateBitmapFromStream(stream);
                if (bitmap == null) return null;

                _avatarBitmapCache[profilePictureUri] = bitmap;
            }

            // Image control
            var image = _r.CreateImage("UniformToFill");
            if (image == null) return null;

            _r.SetImageSource(image, bitmap);
            image.GetType().GetProperty("Width")?.SetValue(image, AvatarSize);
            image.GetType().GetProperty("Height")?.SetValue(image, AvatarSize);

            // Border for circular clipping
            var border = _r.CreateBorder(cornerRadius: AvatarSize / 2.0, child: image);
            if (border == null) return null;

            border.GetType().GetProperty("Width")?.SetValue(border, AvatarSize);
            border.GetType().GetProperty("Height")?.SetValue(border, AvatarSize);
            _r.SetClipToBounds(border, true);

            return border;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"CreateCircularAvatar error: {ex.Message}");
            return null;
        }
    }

    // ===== Reflection resolution =====

    private void ResolveReflection(object reactionViewModel)
    {
        _reflectionResolved = true;
        try
        {
            var vmType = reactionViewModel.GetType();
            const BindingFlags priv = BindingFlags.NonPublic | BindingFlags.Instance;
            const BindingFlags pub = BindingFlags.Public | BindingFlags.Instance;

            _vmReactionProp = vmType.GetProperty("Reaction", pub);
            _vmUserCacheField = vmType.GetField("_globalUserCacheService", priv);

            if (_vmReactionProp == null) { Logger.Log(Tag, "WARN: Reaction prop not found"); return; }
            if (_vmUserCacheField == null) { Logger.Log(Tag, "WARN: _globalUserCacheService field not found"); return; }

            // Reaction domain model
            var reaction = _vmReactionProp.GetValue(reactionViewModel);
            if (reaction != null)
            {
                var rt = reaction.GetType();
                _reactionUsersProp = rt.GetProperty("Users", pub);
                _reactionShortCodeProp = rt.GetProperty("ShortCode", pub);
                _reactionMessageIdProp = rt.GetProperty("MessageId", pub);
            }

            // IGlobalUserCacheService — find sync GetUsersByIds
            var userCache = _vmUserCacheField.GetValue(reactionViewModel);
            if (userCache != null)
            {
                // Search concrete type then interfaces
                _getUsersByIdsSync = FindSyncGetUsersByIds(userCache.GetType(), pub);
                if (_getUsersByIdsSync == null)
                {
                    foreach (var iface in userCache.GetType().GetInterfaces())
                    {
                        _getUsersByIdsSync = FindSyncGetUsersByIds(iface, pub);
                        if (_getUsersByIdsSync != null) break;
                    }
                }

                // Resolve GlobalUser property infos from return type
                if (_getUsersByIdsSync != null)
                {
                    var returnType = _getUsersByIdsSync.ReturnType;
                    var guType = returnType.IsGenericType ? returnType.GetGenericArguments().FirstOrDefault() : null;
                    if (guType != null)
                    {
                        _globalUserProfilePicProp = guType.GetProperty("ProfilePictureUri", pub);
                        _globalUserIdProp = guType.GetProperty("Id", pub);
                    }
                }
            }

            Logger.Log(Tag, $"Reflection resolved: Reaction={_vmReactionProp != null} Users={_reactionUsersProp != null} " +
                $"UserCache={_getUsersByIdsSync != null} ProfilePic={_globalUserProfilePicProp != null}");
        }
        catch (Exception ex) { Logger.Log(Tag, $"ResolveReflection error: {ex.Message}"); }
    }

    private static MethodInfo? FindSyncGetUsersByIds(Type type, BindingFlags flags)
    {
        foreach (var m in type.GetMethods(flags))
        {
            if (m.Name == "GetUsersByIds"
                && !m.ReturnType.Name.Contains("Task")
                && !m.ReturnType.Name.Contains("ValueTask"))
                return m;
        }
        return null;
    }

    // ===== HTTP (reflection-based, same pattern as LinkEmbedEngine) =====

    private static void EnsureHttpResolved()
    {
        if (s_httpResolved) return;
        s_httpResolved = true;
        try
        {
            Type? httpClientType = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try { httpClientType = asm.GetType("System.Net.Http.HttpClient"); if (httpClientType != null) break; }
                catch { }
            }
            if (httpClientType == null) return;

            s_httpClient = Activator.CreateInstance(httpClientType);
            if (s_httpClient == null) return;

            httpClientType.GetProperty("Timeout")?.SetValue(s_httpClient, TimeSpan.FromSeconds(10));
            s_getByteArrayAsync = httpClientType.GetMethod("GetByteArrayAsync", new[] { typeof(string) });

            Logger.Log(Tag, $"HTTP resolved: client={s_httpClient != null} getBytes={s_getByteArrayAsync != null}");
        }
        catch (Exception ex) { Logger.Log(Tag, $"HTTP resolve error: {ex.Message}"); }
    }

    /// <summary>
    /// Downloads bytes from a URL. Called on ThreadPool, safe to block.
    /// </summary>
    private static byte[]? DownloadBytes(string url)
    {
        if (s_httpClient == null || s_getByteArrayAsync == null) return null;
        try
        {
            var task = s_getByteArrayAsync.Invoke(s_httpClient, new object[] { url });
            if (task == null) return null;
            return task.GetType().GetProperty("Result")?.GetValue(task) as byte[];
        }
        catch { return null; }
    }
}
