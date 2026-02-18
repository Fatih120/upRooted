using System.Linq.Expressions;
using System.Reflection;

namespace Uprooted;

/// <summary>
/// Message logger plugin — preserves deleted messages and tracks edits.
///
/// Deletion detection: All ObservableCollection Remove events are treated as genuine
/// deletions. Channel switches are detected separately (ItemsSource reference swap or
/// Reset event), NOT via individual Remove events. Virtualization only affects visual
/// containers, not the data source, so Remove events = real data removal.
///
/// Display: Deleted messages are re-injected into the chat as red-tinted inline panels
/// (since Root removes them entirely). Right-click shows "Clear message history" to
/// actually remove the persisted message.
///
/// Edit detection: Content snapshot comparison on each poll tick.
/// </summary>
internal class MessageLogger
{
    private const string Tag = "MsgLogger";
    private const int ScanIntervalMs = 1500;
    private const int InitialDelayMs = 3000;

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly object _mainWindow;
    private readonly MessageStore _store;

    private Timer? _scanTimer;
    private int _scanning;
    private int _heartbeatCounter;

    // Discovery results
    private object? _chatItemsControl;
    private object? _currentItemsSource;
    private Type? _messageItemType;

    // Per-type property accessor cache (different ViewModel types have different PropertyInfo)
    private readonly Dictionary<Type, TypeProps> _typePropsCache = new();
    private bool _propertiesResolved; // true once at least one type is resolved

    private class TypeProps
    {
        public PropertyInfo? Bridge;     // ViewModel.Message bridge (null if direct)
        public PropertyInfo? MessageId;
        public PropertyInfo? Content;
        public PropertyInfo? AuthorId;
        public PropertyInfo? Timestamp;
        public PropertyInfo? HasBeenDeleted;
        public PropertyInfo? HasBeenEdited;
    }

    // Collection subscription
    private Delegate? _collectionChangedHandler;
    private EventInfo? _collectionChangedEvent;
    private bool _subscribed;

    // Channel tracking
    private string _currentChannelId = "";

    // Remove debounce — rapid Removes = channel switch, not deletions
    private readonly List<(object item, DateTime time)> _pendingRemoves = new();
    private const int ChannelSwitchThreshold = 3; // 3+ removes in window = channel switch
    private const int DebounceWindowMs = 300;

    // Message caches
    private readonly Dictionary<string, CachedMessage> _messageCache = new();
    private readonly Dictionary<string, string> _contentSnapshot = new();

    // Visual indicator tracking — tag → injected control
    private readonly Dictionary<string, object> _injectedCards = new();

    // Avalonia types for context menu (resolved lazily)
    private Type? _contextMenuType;
    private Type? _menuItemType;
    private bool _contextMenuTypesResolved;

    internal MessageLogger(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _walker = new VisualTreeWalker(resolver);
        _mainWindow = mainWindow;
        _store = new MessageStore();
    }

    internal void Initialize()
    {
        Logger.Log(Tag, "Starting message logger");

        _store.LoadAll(_messageCache);
        Logger.Log(Tag, $"Loaded {_messageCache.Count} cached messages from store");

        var settings = UprootedSettings.Load();
        if (_messageCache.Count > settings.MessageLoggerMaxMessages)
        {
            _store.Truncate(settings.MessageLoggerMaxMessages);
            Logger.Log(Tag, $"Truncated store to {settings.MessageLoggerMaxMessages}");
        }

        _r.RunOnUIThread(() =>
        {
            try { RunDiscoveryScan(); }
            catch (Exception ex) { Logger.LogException(Tag, "Discovery error", ex); }
        });

        _scanTimer = new Timer(OnScanTick, null, InitialDelayMs, ScanIntervalMs);
        Logger.Log(Tag, $"Scan timer started ({ScanIntervalMs}ms)");
    }

    // ===== Timer =====

    private void OnScanTick(object? state)
    {
        if (Interlocked.CompareExchange(ref _scanning, 1, 0) != 0) return;
        try
        {
            var settings = UprootedSettings.Load();
            if (settings.Plugins.TryGetValue("message-logger", out var enabled) && !enabled)
            {
                Interlocked.Exchange(ref _scanning, 0);
                return;
            }

            _r.RunOnUIThread(() =>
            {
                try
                {
                    _heartbeatCounter++;
                    if (_heartbeatCounter % 20 == 0) // every ~30s
                    {
                        int srcCount = 0;
                        try
                        {
                            if (_currentItemsSource != null)
                                srcCount = (int)(_currentItemsSource.GetType().GetProperty("Count")?.GetValue(_currentItemsSource) ?? 0);
                        }
                        catch { }
                        Logger.Log(Tag, $"[heartbeat] subscribed={_subscribed} resolved={_propertiesResolved} srcItems={srcCount} snapshots={_contentSnapshot.Count} cache={_messageCache.Count}");
                    }

                    FlushPendingRemoves(settings);
                    EnsureCollectionSubscription();
                    if (_propertiesResolved)
                    {
                        PollEdits(settings);
                        InjectDeletedMessageCards(settings);
                    }
                }
                catch (Exception ex) { Logger.Log(Tag, $"Scan error: {ex.Message}"); }
                finally { Interlocked.Exchange(ref _scanning, 0); }
            });

            Task.Delay(ScanIntervalMs * 2).ContinueWith(_ =>
                Interlocked.CompareExchange(ref _scanning, 0, 1));
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Tick error: {ex.Message}");
            Interlocked.Exchange(ref _scanning, 0);
        }
    }

    // ===== Phase 1: Discovery =====

    private void RunDiscoveryScan()
    {
        Logger.Log(Tag, "=== Phase 1: Discovery ===");

        var settingsText = _walker.FindFirstTextBlock(_mainWindow, "APP SETTINGS");
        object? settingsSubtree = null;
        if (settingsText != null)
        {
            var current = _r.GetParent(settingsText);
            for (int i = 0; i < 20 && current != null; i++)
            {
                if (_r.IsGrid(current) && _r.GetChildCount(current) >= 3)
                { settingsSubtree = current; break; }
                current = _r.GetParent(current);
            }
        }

        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            if (settingsSubtree != null && IsDescendantOf(node, settingsSubtree)) continue;
            var typeName = node.GetType().Name;
            if (typeName.Contains("ItemsControl") || typeName == "ListBox" || typeName == "ItemsRepeater")
                InspectItemsControl(node);
        }

        // Walk up from CTextBlock for ViewModel property resolution
        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            if (settingsSubtree != null && IsDescendantOf(node, settingsSubtree)) continue;
            if (node.GetType().Name != "CTextBlock") continue;

            var current = node;
            for (int i = 0; i < 15 && current != null; i++)
            {
                object? dc = ReadDC(current);
                if (dc != null && !_propertiesResolved &&
                    dc.GetType().Name.Contains("Message", StringComparison.OrdinalIgnoreCase))
                    TryResolvePropertyAccessors(dc.GetType());
                current = _r.GetParent(current);
            }
            if (_propertiesResolved) break;
        }

        ReadCurrentChannelId();
        Logger.Log(Tag, $"Discovery: resolved={_propertiesResolved}, control={_chatItemsControl != null}, ch={_currentChannelId}");
    }

    private void InspectItemsControl(object ic)
    {
        var typeName = ic.GetType().Name;
        object? source = ReadItemsSource(ic);
        if (source == null) return;

        var sourceType = source.GetType();
        bool hasINCC = sourceType.GetInterfaces().Any(i => i.Name == "INotifyCollectionChanged");
        int count = 0;
        try { count = (int)(sourceType.GetProperty("Count")?.GetValue(source) ?? 0); }
        catch { }

        // Resolve properties from first item (per-type cache handles duplicates)
        if (count > 0)
        {
            try
            {
                var en = (source as System.Collections.IEnumerable)?.GetEnumerator();
                if (en != null && en.MoveNext() && en.Current != null)
                    TryResolvePropertyAccessors(en.Current.GetType());
            }
            catch { }
        }

        // Only adopt the actual chat message collection
        bool isChat = typeName.Contains("Message", StringComparison.OrdinalIgnoreCase);
        if (!isChat && count > 0)
        {
            try
            {
                var en = (source as System.Collections.IEnumerable)?.GetEnumerator();
                if (en != null && en.MoveNext() && en.Current != null)
                    isChat = en.Current.GetType().Name.Contains("MessageViewModel");
            }
            catch { }
        }

        if (isChat && hasINCC && _chatItemsControl == null)
        {
            Logger.Log(Tag, $"Chat collection: {typeName}, {count} items");
            _chatItemsControl = ic;
            _currentItemsSource = source;
        }
    }

    private void ReadCurrentChannelId()
    {
        if (_chatItemsControl == null) return;
        var current = _chatItemsControl;
        for (int i = 0; i < 10 && current != null; i++)
        {
            object? dc = ReadDC(current);
            if (dc != null)
            {
                var t = dc.GetType();
                var prop = t.GetProperty("ChannelId", BindingFlags.Public | BindingFlags.Instance)
                    ?? t.GetProperty("MessageContainerId", BindingFlags.Public | BindingFlags.Instance)
                    ?? t.GetProperty("ContainerId", BindingFlags.Public | BindingFlags.Instance);
                if (prop != null)
                {
                    var val = prop.GetValue(dc)?.ToString();
                    if (!string.IsNullOrEmpty(val))
                    { _currentChannelId = val!; return; }
                }
                if (t.Name.Contains("TextChannel") || t.Name.Contains("Content"))
                { _currentChannelId = $"dc:{dc.GetHashCode()}"; return; }
            }
            current = _r.GetParent(current);
        }
    }

    // ===== Property Resolution =====

    /// <summary>
    /// Resolve property accessors for a specific ViewModel type. Results are cached
    /// per-type since different ViewModels (MessageViewModel, ChannelStartMessageViewModel)
    /// have separate PropertyInfo objects that only work on their own type.
    /// </summary>
    private TypeProps? TryResolvePropertyAccessors(Type type)
    {
        if (_typePropsCache.TryGetValue(type, out var cached)) return cached;

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var tp = new TypeProps
        {
            HasBeenDeleted = FindProp(props, "HasBeenDeleted", "IsDeleted"),
            HasBeenEdited = FindProp(props, "HasBeenEdited", "IsEdited")
        };

        // Try direct resolution first
        if (TryResolveOnProps(props, type.Name, null, tp))
        {
            _typePropsCache[type] = tp;
            _propertiesResolved = true;
            _messageItemType = type;
            return tp;
        }

        // Try nested properties (e.g. ViewModel.Message)
        foreach (var prop in props)
        {
            if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string) &&
                !prop.PropertyType.IsArray && prop.PropertyType.Namespace != "System")
            {
                var nested = prop.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if (TryResolveOnProps(nested, prop.PropertyType.Name, prop, tp))
                {
                    Logger.Log(Tag, $"Resolved via {type.Name}.{prop.Name}");
                    _typePropsCache[type] = tp;
                    _propertiesResolved = true;
                    _messageItemType = type;
                    return tp;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Get or resolve the TypeProps for an item. This is the main entry point
    /// for reading properties — automatically handles type mismatches.
    /// </summary>
    private TypeProps? GetTypeProps(object item)
    {
        var type = item.GetType();
        if (_typePropsCache.TryGetValue(type, out var cached)) return cached;
        return TryResolvePropertyAccessors(type);
    }

    private bool TryResolveOnProps(PropertyInfo[] props, string typeName, PropertyInfo? bridge, TypeProps tp)
    {
        var id = FindProp(props, "MessageId", "Id", "Uuid", "Nonce");
        var content = FindProp(props, "MessageContent", "Content", "Text", "Body", "RawContent");
        if (id == null || content == null) return false;

        tp.Bridge = bridge;
        tp.MessageId = id;
        tp.Content = content;
        tp.AuthorId = FindProp(props, "SenderUserId", "AuthorId", "SenderId", "UserId");
        tp.Timestamp = FindProp(props, "SentAtUtc", "SentAt", "Timestamp", "CreatedAt");

        Logger.Log(Tag, $"Props on {typeName}{(bridge != null ? $" (via .{bridge.Name})" : "")}: " +
            $"Id={id.Name}, Content={content.Name}, " +
            $"HasBeenDeleted={tp.HasBeenDeleted?.Name ?? "?"}");
        return true;
    }

    private static PropertyInfo? FindProp(PropertyInfo[] props, params string[] names)
    {
        foreach (var n in names)
            foreach (var p in props)
                if (p.Name.Equals(n, StringComparison.OrdinalIgnoreCase)) return p;
        return null;
    }

    // ===== Collection Subscription =====

    private void EnsureCollectionSubscription()
    {
        if (_chatItemsControl == null) { Logger.Log(Tag, "[ensure] no chatItemsControl"); return; }

        var currentSource = ReadItemsSource(_chatItemsControl);
        if (currentSource == null)
        {
            Logger.Log(Tag, "[ensure] ItemsSource is null — control may be dead, rediscovering");
            _subscribed = false;
            TryRediscoverChat();
            return;
        }

        // Check if the control is still in the visual tree
        bool controlAlive = _r.GetParent(_chatItemsControl) != null;

        if (!ReferenceEquals(currentSource, _currentItemsSource))
        {
            int newCount = 0;
            try { newCount = (int)(currentSource.GetType().GetProperty("Count")?.GetValue(currentSource) ?? 0); }
            catch { }
            Logger.Log(Tag, $"[ensure] ItemsSource ref changed — new count={newCount}, alive={controlAlive}");
            UnsubscribeCollection();
            _currentItemsSource = currentSource;
            _pendingRemoves.Clear();
            _contentSnapshot.Clear();
            ClearInjectedCards();
            ReadCurrentChannelId();
            SubscribeToCollection(currentSource);
            SnapshotMessages(currentSource);
        }
        else if (!controlAlive)
        {
            Logger.Log(Tag, "[ensure] Control orphaned — rediscovering");
            UnsubscribeCollection();
            _chatItemsControl = null;
            _currentItemsSource = null;
            TryRediscoverChat();
        }
        else if (!_subscribed)
        {
            SubscribeToCollection(currentSource);
            SnapshotMessages(currentSource);
        }
    }

    private void TryRediscoverChat()
    {
        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            var tn = node.GetType().Name;
            if (!tn.Contains("ItemsControl") && tn != "ListBox" && tn != "ItemsRepeater") continue;
            var source = ReadItemsSource(node);
            if (source == null) continue;
            try
            {
                var en = (source as System.Collections.IEnumerable)?.GetEnumerator();
                if (en != null && en.MoveNext() && en.Current != null &&
                    _typePropsCache.ContainsKey(en.Current.GetType()))
                {
                    Logger.Log(Tag, $"Re-discovered chat: {tn}");
                    _chatItemsControl = node;
                    _currentItemsSource = source;
                    _contentSnapshot.Clear();
                    ClearInjectedCards();
                    ReadCurrentChannelId();
                    SubscribeToCollection(source);
                    SnapshotMessages(source);
                    return;
                }
            }
            catch { }
        }
    }

    private void SubscribeToCollection(object collection)
    {
        try
        {
            EventInfo? ev = collection.GetType().GetEvent("CollectionChanged");
            if (ev == null)
            {
                foreach (var iface in collection.GetType().GetInterfaces())
                    if (iface.Name == "INotifyCollectionChanged")
                    { ev = iface.GetEvent("CollectionChanged"); break; }
            }
            if (ev == null) { _subscribed = false; return; }

            var ht = ev.EventHandlerType!;
            var pts = ht.GetMethod("Invoke")!.GetParameters().Select(p => p.ParameterType).ToArray();
            var cb = new Action<object>(OnCollectionChanged);
            var s = Expression.Parameter(pts[0], "s");
            var e = Expression.Parameter(pts[1], "e");
            var body = Expression.Invoke(Expression.Constant(cb), Expression.Convert(e, typeof(object)));
            var handler = Expression.Lambda(ht, body, s, e).Compile();

            ev.AddEventHandler(collection, handler);
            _collectionChangedHandler = handler;
            _collectionChangedEvent = ev;
            _subscribed = true;
            Logger.Log(Tag, "Subscribed to CollectionChanged");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Subscribe error: {ex.Message}");
            _subscribed = false;
        }
    }

    private void UnsubscribeCollection()
    {
        if (_collectionChangedHandler != null && _currentItemsSource != null && _collectionChangedEvent != null)
        { try { _collectionChangedEvent.RemoveEventHandler(_currentItemsSource, _collectionChangedHandler); } catch { } }
        _collectionChangedHandler = null;
        _collectionChangedEvent = null;
        _subscribed = false;
    }

    private void SnapshotMessages(object collection)
    {
        if (!_propertiesResolved) return;
        try
        {
            var en = (collection as System.Collections.IEnumerable)?.GetEnumerator();
            if (en == null) return;
            while (en.MoveNext())
            {
                var item = en.Current;
                if (item == null) continue;
                var msgId = ReadMessageId(item);
                if (msgId == null) continue;
                _contentSnapshot[msgId] = ReadContent(item) ?? "";
                CacheMessage(item, msgId);
            }
        }
        catch (Exception ex) { Logger.Log(Tag, $"Snapshot error: {ex.Message}"); }
    }

    // ===== CollectionChanged =====

    private void OnCollectionChanged(object args)
    {
        try
        {
            var argsType = args.GetType();
            var action = (int)(argsType.GetProperty("Action")?.GetValue(args) ?? -1);
            var newItems = argsType.GetProperty("NewItems")?.GetValue(args) as System.Collections.IList;
            var oldItems = argsType.GetProperty("OldItems")?.GetValue(args) as System.Collections.IList;

            Logger.Log(Tag, $"CC: action={action} new={newItems?.Count ?? 0} old={oldItems?.Count ?? 0}");

            switch (action)
            {
                case 0: // Add
                    // Flush pending removes — if adds are coming in, the removes were a channel switch
                    if (_pendingRemoves.Count > 0)
                    {
                        Logger.Log(Tag, $"Discarding {_pendingRemoves.Count} pending removes (Add arrived = channel switch)");
                        _pendingRemoves.Clear();
                        _contentSnapshot.Clear();
                    }
                    if (newItems != null) HandleAdded(newItems);
                    break;

                case 1: // Remove — queue for debounce
                    if (oldItems != null)
                    {
                        var now = DateTime.UtcNow;
                        foreach (var item in oldItems)
                            if (item != null) _pendingRemoves.Add((item, now));
                    }
                    break;

                case 2: // Replace
                    Logger.Log(Tag, "Collection Replace event");
                    if (newItems != null) HandleAdded(newItems);
                    break;

                case 3: // Move
                    break;

                case 4: // Reset
                    Logger.Log(Tag, "Collection Reset — channel switch");
                    _pendingRemoves.Clear();
                    _contentSnapshot.Clear();
                    ClearInjectedCards();
                    break;
            }
        }
        catch (Exception ex) { Logger.Log(Tag, $"CollectionChanged error: {ex.Message}"); }
    }

    private void HandleAdded(System.Collections.IList items)
    {
        if (!_propertiesResolved) return;
        foreach (var item in items)
        {
            if (item == null) continue;
            var msgId = ReadMessageId(item);
            if (msgId == null) continue;
            _contentSnapshot[msgId] = ReadContent(item) ?? "";
            CacheMessage(item, msgId);
        }
    }

    /// <summary>
    /// Process the pending Remove queue after the debounce window. If many items were
    /// removed rapidly (channel switch), discard them. If few items trickled in
    /// (real deletions), process them.
    /// </summary>
    private void FlushPendingRemoves(UprootedSettings settings)
    {
        if (_pendingRemoves.Count == 0) return;

        var now = DateTime.UtcNow;
        var oldest = _pendingRemoves[0].time;
        if ((now - oldest).TotalMilliseconds < DebounceWindowMs) return; // still within window

        var count = _pendingRemoves.Count;

        if (count >= ChannelSwitchThreshold)
        {
            Logger.Log(Tag, $"Channel switch detected: {count} removes in {(now - oldest).TotalMilliseconds:F0}ms — discarding");
            _pendingRemoves.Clear();
            _contentSnapshot.Clear();
            ClearInjectedCards();
            return;
        }

        if (!_propertiesResolved || !settings.MessageLoggerLogDeletes)
        {
            _pendingRemoves.Clear();
            return;
        }

        Logger.Log(Tag, $"Processing {count} pending remove(s) as deletion(s)");
        foreach (var (item, _) in _pendingRemoves)
        {
            var msgId = ReadMessageId(item);
            if (msgId == null) continue;

            _contentSnapshot.Remove(msgId);

            if (_messageCache.TryGetValue(msgId, out var cached) && !cached.IsDeleted)
            {
                cached.IsDeleted = true;
                cached.DeletedAt = DateTime.UtcNow;
                _store.RecordDeletion(msgId, cached.ChannelId, DateTime.UtcNow);
                Logger.Log(Tag, $"MSG DEL: {Truncate(cached.Content, 80)}");
            }
            else if (!_messageCache.ContainsKey(msgId))
            {
                var cached2 = ExtractCachedMessage(item, msgId);
                if (cached2 != null)
                {
                    cached2.IsDeleted = true;
                    cached2.DeletedAt = DateTime.UtcNow;
                    _messageCache[msgId] = cached2;
                    _store.RecordMessage(cached2.MessageId, cached2.ChannelId,
                        cached2.AuthorId, cached2.AuthorName, cached2.Timestamp, cached2.Content);
                    _store.RecordDeletion(msgId, cached2.ChannelId, DateTime.UtcNow);
                    Logger.Log(Tag, $"MSG DEL (new): {Truncate(cached2.Content, 80)}");
                }
            }
        }
        _pendingRemoves.Clear();
    }

    // ===== Edit Detection via Polling =====

    private void PollEdits(UprootedSettings settings)
    {
        if (!settings.MessageLoggerLogEdits || _currentItemsSource == null) return;
        try
        {
            var en = (_currentItemsSource as System.Collections.IEnumerable)?.GetEnumerator();
            if (en == null) return;
            while (en.MoveNext())
            {
                var item = en.Current;
                if (item == null) continue;
                var msgId = ReadMessageId(item);
                if (msgId == null) continue;

                var content = ReadContent(item) ?? "";
                if (_contentSnapshot.TryGetValue(msgId, out var prev) &&
                    prev != content && prev.Length > 0)
                {
                    if (_messageCache.TryGetValue(msgId, out var cached))
                    {
                        cached.Edits.Add(new MessageEdit { EditTime = DateTime.UtcNow, PreviousContent = prev });
                        cached.Content = content;
                        _store.RecordEdit(msgId, cached.ChannelId, DateTime.UtcNow, prev);
                        Logger.Log(Tag, $"MSG EDIT: {Truncate(prev, 30)} -> {Truncate(content, 30)}");
                    }
                }
                _contentSnapshot[msgId] = content;
            }
        }
        catch (Exception ex) { Logger.Log(Tag, $"PollEdits error: {ex.Message}"); }
    }

    // ===== Phase 3: Visual — inject deleted message cards inline =====

    /// <summary>
    /// For each deleted message in the current channel, inject a red-tinted inline card
    /// into the chat showing the preserved content. Cards are placed after the nearest
    /// visible message by timestamp.
    /// </summary>
    private void InjectDeletedMessageCards(UprootedSettings settings)
    {
        if (!settings.MessageLoggerLogDeletes || _chatItemsControl == null) return;

        // Find the VirtualizingStackPanel or Panel that holds message containers
        object? messagePanel = FindMessagePanel();
        if (messagePanel == null) return;

        // Get deleted messages for current channel
        foreach (var cached in _messageCache.Values)
        {
            if (!cached.IsDeleted || cached.ChannelId != _currentChannelId) continue;
            if (cached.DeletedAt == null) continue;
            if ((DateTime.UtcNow - cached.DeletedAt.Value).TotalHours > 24) continue;

            var cardTag = $"uprooted-del:{cached.MessageId}";
            if (_injectedCards.ContainsKey(cardTag)) continue;

            var card = BuildDeletedMessageCard(cached, cardTag);
            if (card == null) continue;

            // Add to the message panel
            _r.AddChild(messagePanel, card);
            _injectedCards[cardTag] = card;
        }

        // Also apply edit indicators on visible messages
        if (settings.MessageLoggerLogEdits)
            ApplyEditIndicators();
    }

    /// <summary>
    /// Build a red-tinted card that shows the deleted message content inline.
    /// Content is immediately visible (no click-to-reveal).
    /// Right-click context menu has "Clear message history".
    /// </summary>
    private object? BuildDeletedMessageCard(CachedMessage cached, string tag)
    {
        try
        {
            // Content stack: header + body
            var stack = _r.CreateStackPanel(vertical: true, spacing: 2);
            if (stack == null) return null;

            // Author + timestamp header
            var timeStr = cached.DeletedAt?.ToLocalTime().ToString("HH:mm") ?? "??:??";
            var header = _r.CreateTextBlock($"[deleted] — {timeStr}", 11, "#FFCC4444", "SemiBold");
            if (header != null) _r.AddChild(stack, header);

            // Message content — immediately visible, red-tinted text
            var body = _r.CreateTextBlock(cached.Content, 13, "#DDDDDD");
            if (body != null)
            {
                _r.SetTextWrapping(body, "Wrap");
                _r.AddChild(stack, body);
            }

            // Red semi-transparent border wrapping everything
            var card = _r.CreateBorder("#30FF3333", cornerRadius: 4, child: stack);
            if (card == null) return null;

            _r.SetTag(card, tag);
            _r.SetPadding(card, 12, 8, 12, 8);
            _r.SetMargin(card, 56, 2, 16, 2); // Left margin to align with message content (past avatars)

            // Right-click context menu: "Clear message history"
            AttachContextMenu(card, cached);

            return card;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"BuildCard error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Attach a ContextMenu with "Clear message history" to a control.
    /// When clicked, removes the persisted deleted message from cache and store,
    /// and removes the visual card.
    /// </summary>
    private void AttachContextMenu(object card, CachedMessage cached)
    {
        try
        {
            ResolveContextMenuTypes();
            if (_contextMenuType == null || _menuItemType == null) return;

            // Create MenuItem
            var menuItem = Activator.CreateInstance(_menuItemType);
            if (menuItem == null) return;

            // Set Header text
            _menuItemType.GetProperty("Header")?.SetValue(menuItem, "Clear message history");

            // Subscribe to Click event
            var clickEvent = _menuItemType.GetEvent("Click");
            if (clickEvent != null)
            {
                var capturedId = cached.MessageId;
                var capturedCard = card;
                var callback = new Action(() =>
                {
                    try
                    {
                        // Remove from cache
                        _messageCache.Remove(capturedId);
                        _store.RecordClear(capturedId);

                        // Remove the visual card
                        var parent = _r.GetParent(capturedCard);
                        if (parent != null)
                            RemoveChild(parent, capturedCard);

                        var cardTag = $"uprooted-del:{capturedId}";
                        _injectedCards.Remove(cardTag);

                        Logger.Log(Tag, $"Cleared history: {capturedId}");
                    }
                    catch (Exception ex) { Logger.Log(Tag, $"Clear error: {ex.Message}"); }
                });

                // Build lambda: (sender, e) => callback()
                var handlerType = clickEvent.EventHandlerType!;
                var invokeMethod = handlerType.GetMethod("Invoke")!;
                var paramTypes = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();
                var sParam = Expression.Parameter(paramTypes[0], "s");
                var eParam = Expression.Parameter(paramTypes[1], "e");
                var invokeBody = Expression.Invoke(Expression.Constant(callback));
                var lambda = Expression.Lambda(handlerType, invokeBody, sParam, eParam);
                clickEvent.AddEventHandler(menuItem, lambda.Compile());
            }

            // Create ContextMenu and add the MenuItem
            var contextMenu = Activator.CreateInstance(_contextMenuType);
            if (contextMenu == null) return;

            // ContextMenu.Items is an ItemCollection — use Add method
            var itemsProp = _contextMenuType.GetProperty("Items");
            var itemsObj = itemsProp?.GetValue(contextMenu);
            if (itemsObj != null)
            {
                var addMethod = itemsObj.GetType().GetMethod("Add", new[] { typeof(object) });
                addMethod?.Invoke(itemsObj, new[] { menuItem });
            }

            // Set ContextMenu on the card
            card.GetType().GetProperty("ContextMenu")?.SetValue(card, contextMenu);
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"ContextMenu error: {ex.Message}");
        }
    }

    private void ResolveContextMenuTypes()
    {
        if (_contextMenuTypesResolved) return;
        _contextMenuTypesResolved = true;

        // Find types from Avalonia.Controls assembly
        var controlsAsm = _r.ControlType?.Assembly;
        if (controlsAsm == null) return;

        _contextMenuType = controlsAsm.GetType("Avalonia.Controls.ContextMenu");
        _menuItemType = controlsAsm.GetType("Avalonia.Controls.MenuItem");

        Logger.Log(Tag, $"ContextMenu types: menu={_contextMenuType != null}, item={_menuItemType != null}");
    }

    /// <summary>
    /// Apply "(edited)" text indicators on visible messages that have edit history.
    /// </summary>
    private void ApplyEditIndicators()
    {
        if (_chatItemsControl == null) return;

        foreach (var node in _walker.DescendantsDepthFirst(_chatItemsControl))
        {
            object? dc = ReadDC(node);
            if (dc == null) continue;
            // Only process items we have resolved property accessors for
            if (!_typePropsCache.ContainsKey(dc.GetType()) && TryResolvePropertyAccessors(dc.GetType()) == null)
                continue;

            var msgId = ReadMessageId(dc);
            if (msgId == null) continue;
            if (!_messageCache.TryGetValue(msgId, out var cached)) continue;
            if (cached.Edits.Count == 0) continue;

            // Find RootMarkdownTextBlock to inject sibling
            object? mdBlock = null;
            foreach (var child in _walker.DescendantsDepthFirst(node))
            {
                if (child.GetType().Name == "RootMarkdownTextBlock")
                { mdBlock = child; break; }
            }
            if (mdBlock == null) continue;

            var parent = _r.GetParent(mdBlock);
            if (parent == null) continue;

            var editTag = $"uprooted-edit:{cached.MessageId}";
            var children = _r.GetChildren(parent);
            if (children != null)
            {
                bool exists = false;
                foreach (var c in children)
                    if (c != null && _r.GetTag(c) == editTag) { exists = true; break; }
                if (exists) continue;
            }

            var editCount = cached.Edits.Count;
            var label = _r.CreateTextBlock(
                $"(edited{(editCount > 1 ? $" {editCount}x" : "")})",
                10, "#88FF9999");
            if (label == null) continue;
            _r.SetTag(label, editTag);
            _r.SetMargin(label, 4, 2, 0, 0);
            _r.AddChild(parent, label);
        }
    }

    /// <summary>
    /// Find the panel that holds message containers (VirtualizingStackPanel or similar).
    /// </summary>
    private object? FindMessagePanel()
    {
        if (_chatItemsControl == null) return null;

        // Walk children of the ItemsControl to find the items host panel
        foreach (var node in _walker.DescendantsDepthFirst(_chatItemsControl))
        {
            var name = node.GetType().Name;
            if (name.Contains("VirtualizingStackPanel") || name.Contains("StackPanel"))
            {
                // Make sure it has message items as children
                int childCount = _r.GetChildCount(node);
                if (childCount >= 1) return node;
            }
        }
        return null;
    }

    private void ClearInjectedCards()
    {
        foreach (var (tag, card) in _injectedCards)
        {
            try
            {
                var parent = _r.GetParent(card);
                if (parent != null) RemoveChild(parent, card);
            }
            catch { }
        }
        _injectedCards.Clear();
    }

    // ===== Message helpers =====

    private void CacheMessage(object item, string msgId)
    {
        if (_messageCache.ContainsKey(msgId)) return;
        var cached = ExtractCachedMessage(item, msgId);
        if (cached != null)
        {
            _messageCache[msgId] = cached;
            _store.RecordMessage(cached.MessageId, cached.ChannelId,
                cached.AuthorId, cached.AuthorName, cached.Timestamp, cached.Content);
        }
    }

    private object? GetMessageTarget(object item, TypeProps tp)
    {
        if (tp.Bridge == null) return item;
        try { return tp.Bridge.GetValue(item); }
        catch { return null; }
    }

    private string? ReadMessageId(object item)
    {
        var tp = GetTypeProps(item);
        if (tp?.MessageId == null) return null;
        var t = GetMessageTarget(item, tp);
        if (t == null) return null;
        try { return tp.MessageId.GetValue(t)?.ToString(); }
        catch { return null; }
    }

    private string? ReadContent(object item)
    {
        var tp = GetTypeProps(item);
        if (tp?.Content == null) return null;
        var t = GetMessageTarget(item, tp);
        if (t == null) return null;
        try { return tp.Content.GetValue(t) as string; }
        catch { return null; }
    }

    private CachedMessage? ExtractCachedMessage(object item, string msgId)
    {
        var tp = GetTypeProps(item);
        if (tp == null) return null;
        var t = GetMessageTarget(item, tp);
        if (t == null) return null;
        try
        {
            DateTime ts = DateTime.UtcNow;
            if (tp.Timestamp != null)
            {
                var v = tp.Timestamp.GetValue(t);
                if (v is DateTimeOffset dto) ts = dto.UtcDateTime;
                else if (v is DateTime dt) ts = dt;
            }
            return new CachedMessage
            {
                MessageId = msgId,
                ChannelId = _currentChannelId,
                AuthorId = tp.AuthorId != null ? (tp.AuthorId.GetValue(t)?.ToString() ?? "") : "",
                AuthorName = "Unknown",
                Timestamp = ts,
                Content = ReadContent(item) ?? ""
            };
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Extract error: {ex.Message}");
            return null;
        }
    }

    // ===== Utility =====

    private static object? ReadDC(object control)
    {
        try
        {
            return control.GetType()
                .GetProperty("DataContext", BindingFlags.Public | BindingFlags.Instance)
                ?.GetValue(control);
        }
        catch { return null; }
    }

    private static object? ReadItemsSource(object control)
    {
        try
        {
            return control.GetType()
                .GetProperty("ItemsSource", BindingFlags.Public | BindingFlags.Instance)
                ?.GetValue(control)
                ?? control.GetType()
                    .GetProperty("Items", BindingFlags.Public | BindingFlags.Instance)
                    ?.GetValue(control);
        }
        catch { return null; }
    }

    private void RemoveChild(object parent, object child)
    {
        try
        {
            var children = parent.GetType().GetProperty("Children")?.GetValue(parent);
            children?.GetType().GetMethod("Remove")?.Invoke(children, new[] { child });
        }
        catch { }
    }

    private bool IsDescendantOf(object node, object ancestor)
    {
        var current = _r.GetParent(node);
        for (int i = 0; i < 50 && current != null; i++)
        {
            if (ReferenceEquals(current, ancestor)) return true;
            current = _r.GetParent(current);
        }
        return false;
    }

    private static string Truncate(string text, int maxLength)
    {
        if (text.Length <= maxLength) return text;
        return text[..(maxLength - 3)] + "...";
    }
}
