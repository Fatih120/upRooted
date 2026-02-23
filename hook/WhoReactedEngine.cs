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
/// Avatar loading uses Root's own BitmapCache (handles root://asset/ URIs internally).
///
/// Pipeline:
///   1. UI thread: scan visual tree, find unprocessed ReactionViews, resolve user IDs + avatar URIs
///   2. ThreadPool: load avatars via BitmapCache.GetBitmapAsync (Root's internal cache + CDN)
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

    // Avatar bitmap cache: ProfilePictureUri → Bitmap object (survives VSP recycling)
    private const int MaxBitmapCache = 200;
    private readonly Dictionary<string, object> _bitmapCache = new();
    private readonly object _bitmapCacheLock = new();

    // Failed URIs (don't retry on every tick)
    private readonly HashSet<string> _failedUris = new();

    // Root's BitmapCache instance (handles root://asset/ URIs)
    private object? _rootBitmapCache;
    private MethodInfo? _getBitmapAsync;       // BitmapCache.GetBitmapAsync(string, string?, int)
    private PropertyInfo? _bitmapWrapperBitmap; // BitmapWrapper.Bitmap

    // Reflection caches for Root's domain types (resolved once)
    private PropertyInfo? _vmReactionProp;           // ReactionViewModel.Reaction
    private FieldInfo? _vmUserCacheField;             // ReactionViewModel._globalUserCacheService
    private FieldInfo? _vmBitmapCacheField;            // ReactionViewModel._bitmapCache
    private PropertyInfo? _reactionUsersProp;         // Reaction.Users
    private PropertyInfo? _reactionShortCodeProp;     // Reaction.ShortCode
    private PropertyInfo? _reactionMessageIdProp;     // Reaction.MessageId
    private MethodInfo? _getUsersByIdsSync;            // IGlobalUserCacheService.GetUsersByIds
    private Type? _userGuidType;                       // RootApp.Core.Identifiers.UserGuid
    private PropertyInfo? _globalUserProfilePicProp;  // GlobalUser.ProfilePictureUri
    private bool _reflectionResolved;

    // Pending injection data (collected on UI thread, consumed after bitmap load)
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
        _scanTimer = new Timer(OnScanTick, null, 3000, ScanIntervalMs);
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

                // Phase 2: Load avatars via BitmapCache on ThreadPool
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        LoadPendingAvatars(pending, ev);

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
                    }
                    catch (Exception ex)
                    {
                        ev.Set("load_error", ex.Message);
                        ev.Dispose();
                        Interlocked.Exchange(ref _scanning, 0);
                    }
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

            var tag = _r.GetTag(node);
            if (tag != null && tag.StartsWith(TagPrefix, StringComparison.Ordinal))
            {
                alreadyProcessed++;
                continue;
            }

            var vm = _r.GetDataContext(node);
            if (vm == null || !vm.GetType().Name.Contains("ReactionViewModel")) continue;

            if (!_reflectionResolved) ResolveReflection(vm);
            if (_vmReactionProp == null) continue;

            var reaction = _vmReactionProp.GetValue(vm);
            if (reaction == null) continue;

            var users = _reactionUsersProp?.GetValue(reaction);
            if (users == null) continue;

            var shortCode = _reactionShortCodeProp?.GetValue(reaction) as string ?? "?";
            var messageId = _reactionMessageIdProp?.GetValue(reaction);
            var msgIdStr = messageId?.ToString() ?? "?";
            var fullTag = $"{TagPrefix}{msgIdStr}:{shortCode}";

            var userCacheService = _vmUserCacheField?.GetValue(vm);
            if (userCacheService == null || _getUsersByIdsSync == null) continue;

            var userIds = CreateTypedUserIdList(users);
            if (userIds == null) continue;

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

            var innerGrid = FindInnerGrid(node);
            if (innerGrid == null) continue;

            // Tag AFTER all validation passes
            _r.SetTag(node, fullTag);

            Logger.Log(Tag, $"Queued {shortCode} with {avatarUris.Count} avatar(s)");

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

    private object? CreateTypedUserIdList(object usersHashSet)
    {
        try
        {
            if (_userGuidType == null) return null;
            var listType = typeof(List<>).MakeGenericType(_userGuidType);
            var list = Activator.CreateInstance(listType);
            if (list == null) return null;
            var addMethod = listType.GetMethod("Add");
            if (addMethod == null) return null;

            int count = 0;
            if (usersHashSet is IEnumerable enumerable)
            {
                foreach (var userId in enumerable)
                {
                    addMethod.Invoke(list, new[] { userId });
                    count++;
                    if (count >= MaxAvatarsPerReaction) break;
                }
            }
            return count > 0 ? list : null;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"CreateTypedUserIdList error: {ex.Message}");
            return null;
        }
    }

    private List<string> ExtractAvatarUris(object? result)
    {
        var uris = new List<string>();
        if (result is not IEnumerable enumerable) return uris;
        foreach (var user in enumerable)
        {
            if (user == null) continue;
            var uri = _globalUserProfilePicProp?.GetValue(user) as string;
            if (!string.IsNullOrEmpty(uri))
                uris.Add(uri!);
        }
        return uris;
    }

    // ===== Phase 2: Load avatars via Root's BitmapCache (ThreadPool) =====

    private void LoadPendingAvatars(List<PendingInjection> pending, WideEvent ev)
    {
        if (_rootBitmapCache == null || _getBitmapAsync == null)
        {
            ev.Set("load_error", "BitmapCache not resolved");
            return;
        }

        int loaded = 0;
        int cached = 0;
        int failed = 0;

        var needed = new HashSet<string>();
        foreach (var p in pending)
            foreach (var uri in p.AvatarUris)
            {
                lock (_bitmapCacheLock) { if (_bitmapCache.ContainsKey(uri)) { cached++; continue; } }
                if (_failedUris.Contains(uri)) continue;
                needed.Add(uri);
            }

        foreach (var uri in needed)
        {
            try
            {
                // Call BitmapCache.GetBitmapAsync(uri, null, 120) → Task<BitmapWrapper?>
                var task = _getBitmapAsync.Invoke(_rootBitmapCache, new object?[] { uri, null, 120 });
                if (task == null) { failed++; continue; }

                // .Result → BitmapWrapper?
                var wrapper = task.GetType().GetProperty("Result")?.GetValue(task);
                if (wrapper == null) { _failedUris.Add(uri); failed++; continue; }

                // .Bitmap → Avalonia.Media.Imaging.Bitmap
                if (_bitmapWrapperBitmap == null)
                    _bitmapWrapperBitmap = wrapper.GetType().GetProperty("Bitmap");

                var bitmap = _bitmapWrapperBitmap?.GetValue(wrapper);
                if (bitmap == null) { _failedUris.Add(uri); failed++; continue; }

                lock (_bitmapCacheLock)
                {
                    if (_bitmapCache.Count >= MaxBitmapCache)
                    {
                        var firstKey = _bitmapCache.Keys.First();
                        _bitmapCache.Remove(firstKey);
                    }
                    _bitmapCache[uri] = bitmap;
                }
                loaded++;
            }
            catch (Exception ex)
            {
                _failedUris.Add(uri);
                failed++;
                var inner = ex is TargetInvocationException tie ? tie.InnerException ?? ex : ex;
                if (inner is AggregateException ae && ae.InnerException != null) inner = ae.InnerException;
                Logger.Log(Tag, $"BitmapCache load error for {uri}: {inner.Message}");
            }
        }

        ev.Set("avatars_loaded", loaded);
        ev.Set("avatars_cached", cached);
        if (failed > 0) ev.Set("avatars_failed", failed);
    }

    // ===== Phase 3: Inject avatars (UI thread) =====

    private void InjectPendingAvatars(List<PendingInjection> pending, WideEvent ev)
    {
        int injected = 0;

        foreach (var p in pending)
        {
            try
            {
                var currentTag = _r.GetTag(p.ReactionView);
                if (currentTag != p.FullTag) continue;

                _r.AddGridColumnPixel(p.InnerGrid, 4);
                _r.AddGridColumnAuto(p.InnerGrid);

                var avatarPanel = _r.CreateStackPanel(vertical: false, spacing: 0);
                if (avatarPanel == null) continue;

                _r.SetTag(avatarPanel, p.FullTag);
                SetVerticalAlignment(avatarPanel, "Center");
                _r.SetGridColumn(avatarPanel, 4);

                int added = 0;
                foreach (var uri in p.AvatarUris)
                {
                    if (added >= MaxAvatarsPerReaction) break;

                    object? bitmap;
                    lock (_bitmapCacheLock) { _bitmapCache.TryGetValue(uri, out bitmap); }
                    if (bitmap == null) continue;

                    var avatar = CreateCircularAvatar(bitmap);
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

    private object? FindInnerGrid(object reactionView)
    {
        try
        {
            foreach (var node in _walker.DescendantsDepthFirst(reactionView))
            {
                if (node.GetType().Name != "Button") continue;
                foreach (var btnChild in _walker.DescendantsDepthFirst(node))
                {
                    if (btnChild.GetType().Name == "Grid")
                        return btnChild;
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

    // ===== Create circular avatar control (UI thread) =====

    private object? CreateCircularAvatar(object bitmap)
    {
        try
        {
            var image = _r.CreateImage("UniformToFill");
            if (image == null) return null;

            _r.SetImageSource(image, bitmap);
            image.GetType().GetProperty("Width")?.SetValue(image, AvatarSize);
            image.GetType().GetProperty("Height")?.SetValue(image, AvatarSize);

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
            _vmBitmapCacheField = vmType.GetField("_bitmapCache", priv);

            if (_vmReactionProp == null) { Logger.Log(Tag, "WARN: Reaction prop not found"); return; }
            if (_vmUserCacheField == null) { Logger.Log(Tag, "WARN: _globalUserCacheService not found"); return; }
            if (_vmBitmapCacheField == null) { Logger.Log(Tag, "WARN: _bitmapCache not found"); return; }

            // Grab Root's BitmapCache instance from this ViewModel
            _rootBitmapCache = _vmBitmapCacheField.GetValue(reactionViewModel);
            if (_rootBitmapCache != null)
            {
                // Find GetBitmapAsync(string, string?, int) overload
                var cacheType = _rootBitmapCache.GetType();
                foreach (var m in cacheType.GetMethods(pub))
                {
                    if (m.Name != "GetBitmapAsync") continue;
                    var parms = m.GetParameters();
                    if (parms.Length == 3 && parms[0].ParameterType == typeof(string))
                    {
                        _getBitmapAsync = m;
                        break;
                    }
                }
                // Fallback: any GetBitmapAsync
                _getBitmapAsync ??= cacheType.GetMethod("GetBitmapAsync", pub);
            }

            // Reaction domain model
            var reaction = _vmReactionProp.GetValue(reactionViewModel);
            if (reaction != null)
            {
                var rt = reaction.GetType();
                _reactionUsersProp = rt.GetProperty("Users", pub);
                _reactionShortCodeProp = rt.GetProperty("ShortCode", pub);
                _reactionMessageIdProp = rt.GetProperty("MessageId", pub);

                if (_reactionUsersProp != null)
                {
                    var usersType = _reactionUsersProp.PropertyType;
                    if (usersType.IsGenericType)
                        _userGuidType = usersType.GetGenericArguments().FirstOrDefault();
                }
            }

            // IGlobalUserCacheService — find sync GetUsersByIds
            var userCache = _vmUserCacheField.GetValue(reactionViewModel);
            if (userCache != null)
            {
                _getUsersByIdsSync = FindSyncGetUsersByIds(userCache.GetType(), pub);
                if (_getUsersByIdsSync == null)
                {
                    foreach (var iface in userCache.GetType().GetInterfaces())
                    {
                        _getUsersByIdsSync = FindSyncGetUsersByIds(iface, pub);
                        if (_getUsersByIdsSync != null) break;
                    }
                }

                if (_getUsersByIdsSync != null)
                {
                    var returnType = _getUsersByIdsSync.ReturnType;
                    var guType = returnType.IsGenericType ? returnType.GetGenericArguments().FirstOrDefault() : null;
                    if (guType != null)
                        _globalUserProfilePicProp = guType.GetProperty("ProfilePictureUri", pub);
                }
            }

            Logger.Log(Tag, $"Reflection resolved: Reaction={_vmReactionProp != null} Users={_reactionUsersProp != null} " +
                $"UserGuid={_userGuidType?.Name} UserCache={_getUsersByIdsSync != null} " +
                $"BitmapCache={_rootBitmapCache != null} GetBitmapAsync={_getBitmapAsync != null} " +
                $"ProfilePic={_globalUserProfilePicProp != null}");
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
}
