using System.Linq.Expressions;
using System.Reflection;

namespace Uprooted;

/// <summary>
/// Message logger plugin — preserves deleted messages and tracks edits.
///
/// Deletion detection: Root's ObservableCollection is a WINDOWED/VIRTUALIZED data source.
/// Remove events fire for BOTH genuine deletions AND buffer management (scroll-off,
/// window shifts). We use the DeletedAt property on the bridge target (Message model)
/// as the primary signal — non-null DateTimeOffset means genuine deletion, null means
/// buffer management. Also subscribes to INotifyPropertyChanged for instant detection.
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
    private const int ScanIntervalMs = 500;
    /// <summary>
    /// Seconds after a message's Add event during which Replace events are treated as
    /// send-completion content settling (not genuine edits). Genuine user edits arrive
    /// well after this window; send-completion Replaces arrive within 0.5-2s.
    /// </summary>
    private const double EditGracePeriodSeconds = 5.0;

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
        public PropertyInfo? DeletedAt;      // DateTimeOffset? — non-null means deleted
        public PropertyInfo? EditedAt;       // DateTimeOffset? — non-null means edited
        public PropertyInfo? SenderMember;   // Message.SenderMember (IMessageContainerMember)
    }

    // Collection subscription
    private Delegate? _collectionChangedHandler;
    private EventInfo? _collectionChangedEvent;
    private bool _subscribed;

    // Channel tracking
    private string _currentChannelId = "";
    private int _freshnessCounter;

    // Deletion detection via CollectionChanged Remove events (buffered + debounced)
    private readonly List<BufferedRemove> _pendingRemoves = new();
    private int _deletionEpoch;       // incremented on channel switch / bulk remove — cancels running pollers
    private int _boolDumpCount;       // limits diagnostic property dumps to first 3 removes

    // Diagnostic counters: track Add/Remove correlation per flush window
    private int _addsSinceFlush;
    private int _removesSinceFlush;

    private struct BufferedRemove
    {
        public string MessageId;
        public string Content;
        public object? ViewModel;       // the ViewModel item (for re-reading bridge target)
        public object? BridgeTarget;    // captured bridge target (may become stale)
        public TypeProps? Props;        // property accessor for DeletedAt read
        public int RemoveIndex;         // OldStartingIndex from the event args
    }

    // Message caches
    private readonly Dictionary<string, CachedMessage> _messageCache = new();
    private readonly Dictionary<string, string> _contentSnapshot = new();
    // Edit detection: tracks messages seen via Add events (not initial snapshot), with Add timestamp.
    // Used by HandleReplaced to gate Replace events — only messages in this dict past
    // EditGracePeriodSeconds are eligible. SnapshotMessages does NOT populate this.
    private readonly Dictionary<string, DateTime> _addedViaEvent = new();

    // PropertyChanged subscriptions for instant deletion/edit detection
    // Keyed by msgId → (bridgeTarget, handler) for cleanup on channel switch
    private readonly Dictionary<string, (object target, Delegate handler)> _propertyChangedSubs = new();

    // Visual indicator tracking — tag → injected control
    private readonly Dictionary<string, object> _injectedCards = new();

    // Audit log integration: entries received before the message was cached in the visual tree.
    // Keyed by messageId. Applied the next time the message is added to _messageCache via HandleAdded.
    // All access is on the UI thread (SetAuditLogEngine dispatches OnEntry to UI thread).
    private readonly Dictionary<string, AuditLogEntry> _pendingAuditDeletes = new();

    // Insertion-order tracking — used for injection positioning (timestamp-based is unreliable
    // because Root timestamps may not resolve correctly or may be UtcNow on cache miss)
    private readonly List<string> _orderedIds = new();
    private readonly Dictionary<string, int> _orderedIdIndex = new();

    // One-time diagnostic tree dump (1E)
    private bool _firstTreeDumpDone;

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
            try
            {
                RunDiscoveryScan();
                // Subscribe immediately — don't wait for timer tick
                if (_chatItemsControl != null && _currentItemsSource != null)
                {
                    SubscribeToCollection(_currentItemsSource);
                    SnapshotMessages(_currentItemsSource);
                }
            }
            catch (Exception ex) { Logger.LogException(Tag, "Discovery error", ex); }
        });

        _scanTimer = new Timer(OnScanTick, null, ScanIntervalMs, ScanIntervalMs);
        Logger.Log(Tag, $"Scan timer started ({ScanIntervalMs}ms)");
    }

    // ===== Audit Log Integration =====

    /// <summary>
    /// Wire an AuditLogEngine to this logger. OnEntry fires on the UI thread,
    /// so ProcessAuditEntry can safely access _messageCache and other UI-thread state.
    /// </summary>
    internal void SetAuditLogEngine(AuditLogEngine engine)
    {
        engine.OnEntry += entry =>
        {
            // OnEntry is already dispatched to the UI thread by AuditLogEngine.ParseDataFrame.
            try { ProcessAuditEntry(entry); }
            catch (Exception ex) { Logger.Log(Tag, $"ProcessAuditEntry: {ex.Message}"); }
        };
        Logger.Log(Tag, "AuditLogEngine wired — audit entries will supplement visual-tree detection");
    }

    /// <summary>
    /// Process a decoded audit log entry.
    ///
    /// Phase 1: ActionMessageDelete/Edit are both -1/-2 (sentinels), so this is a no-op.
    ///          OnEntry still fires for diagnostic logging in AuditLogEngine.
    ///
    /// Phase 2+: Update AuditLogEngine.ActionMessageDelete/Edit constants and rebuild.
    ///           This method then handles audit-driven deletions with actor attribution.
    /// </summary>
    private void ProcessAuditEntry(AuditLogEntry entry)
    {
        if (entry.ActionType == AuditLogEngine.ActionMessageDelete)
        {
            Logger.Log(Tag, $"[audit] DELETE: msg={entry.MessageId} actor={entry.ActorId} ch={entry.ChannelId}");

            if (string.IsNullOrEmpty(entry.MessageId)) return;

            if (_messageCache.TryGetValue(entry.MessageId, out var cached))
            {
                // Message is already cached — apply deletion with actor info
                if (!cached.IsDeleted)
                {
                    cached.IsDeleted = true;
                    cached.DeletedAt = DateTime.UtcNow;
                    cached.DeletedBy = entry.ActorId;
                    cached.DeletedByName = entry.ActorName;
                    _store.RecordDeletion(cached.MessageId, cached.ChannelId,
                        cached.DeletedAt.Value, cached.DeletedBy, cached.DeletedByName);
                    Logger.Log(Tag, $"[audit] Marked deleted: {entry.MessageId} by @{entry.ActorName}");
                }
                else if (string.IsNullOrEmpty(cached.DeletedBy) && !string.IsNullOrEmpty(entry.ActorId))
                {
                    // Already marked deleted (from visual tree), but now we have actor info
                    cached.DeletedBy = entry.ActorId;
                    cached.DeletedByName = entry.ActorName;
                    Logger.Log(Tag, $"[audit] Actor info added to existing deletion: {entry.MessageId} by @{entry.ActorName}");
                }

                // If a card is already injected for this message, remove it so the next
                // scan tick re-injects it with the updated actor name in the label.
                if (!string.IsNullOrEmpty(entry.ActorName))
                {
                    var cardTag = $"uprooted-del:{entry.MessageId}";
                    if (_injectedCards.TryGetValue(cardTag, out var existingCard))
                    {
                        var parent = _r.GetParent(existingCard);
                        if (parent != null) RemoveChild(parent, existingCard);
                        _injectedCards.Remove(cardTag);
                        Logger.Log(Tag, $"[audit] Removed stale card for re-injection with actor name: {entry.MessageId}");
                    }
                }
            }
            else
            {
                // Message not cached yet — store for application when it's next seen in Add event
                _pendingAuditDeletes[entry.MessageId] = entry;
                Logger.Log(Tag, $"[audit] Pending delete queued: {entry.MessageId} (not in cache yet)");
            }
        }
        else if (entry.ActionType == AuditLogEngine.ActionMessageEdit)
        {
            Logger.Log(Tag, $"[audit] EDIT: msg={entry.MessageId} actor={entry.ActorId}");
            // Edit handling is primarily via CollectionChanged Replace events.
            // Audit log confirms the edit happened; actor info could be recorded if needed.
        }
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
                    if (_heartbeatCounter % 60 == 0) // every ~30s at 500ms interval
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

                    EnsureCollectionSubscription();
                    if (_propertiesResolved)
                    {
                        FlushPendingRemoves(settings);
                        // Edit detection is event-driven (HandleReplaced), not poll-based.
                        // PollEdits is kept for reference but not called.
                        InjectDeletedMessageCards(settings);
                        InjectEditIndicators(settings);
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
            // Resolve properties from first item (only for chat controls, not emoji pickers etc.)
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
            DeletedAt = FindProp(props, "DeletedAt", "HasBeenDeleted", "IsDeleted"),
            EditedAt = FindProp(props, "EditedAt", "HasBeenEdited", "IsEdited")
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
                    // DeletedAt/EditedAt may live on the bridge target, not the ViewModel
                    if (tp.DeletedAt == null)
                        tp.DeletedAt = FindProp(nested, "DeletedAt", "HasBeenDeleted", "IsDeleted");
                    if (tp.EditedAt == null)
                        tp.EditedAt = FindProp(nested, "EditedAt", "HasBeenEdited", "IsEdited");

                    Logger.Log(Tag, $"Resolved via {type.Name}.{prop.Name} (DeletedAt={tp.DeletedAt?.Name ?? "?"}, EditedAt={tp.EditedAt?.Name ?? "?"})");
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

        tp.SenderMember = FindProp(props, "SenderMember");

        Logger.Log(Tag, $"Props on {typeName}{(bridge != null ? $" (via .{bridge.Name})" : "")}: " +
            $"Id={id.Name}, Content={content.Name}, " +
            $"DeletedAt={tp.DeletedAt?.Name ?? "?"}, EditedAt={tp.EditedAt?.Name ?? "?"}, SenderMember={tp.SenderMember?.Name ?? "?"}");
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
        if (_chatItemsControl == null)
        {
            Logger.Log(Tag, "[ensure] no chatItemsControl — rediscovering");
            TryRediscoverChat();
            return;
        }

        // Every ~15 ticks (7.5s at 500ms interval), verify we're still tracking the ACTIVE
        // RootMessageItemsControl. Root creates new control instances on channel switch;
        // the old one may linger alive in the tree but with stale data.
        _freshnessCounter++;
        if (_freshnessCounter >= 15)
        {
            _freshnessCounter = 0;
            var activeControl = FindActiveMessageControl();
            if (activeControl != null && !ReferenceEquals(activeControl, _chatItemsControl))
            {
                var activeSource = ReadItemsSource(activeControl);
                int activeCount = 0;
                try { activeCount = (int)(activeSource?.GetType().GetProperty("Count")?.GetValue(activeSource) ?? 0); }
                catch { }
                Logger.Log(Tag, $"[ensure] Active control changed — switching to {activeControl.GetType().Name} with {activeCount} items");
                // Flush buffered removes before discarding — real deletions from old channel shouldn't be lost
                var flushSettings = UprootedSettings.Load();
                FlushPendingRemoves(flushSettings);
                UnsubscribeCollection();
                _chatItemsControl = activeControl;
                _currentItemsSource = activeSource;
                _deletionEpoch++;
                _contentSnapshot.Clear();
                _addedViaEvent.Clear();
                _pendingRemoves.Clear();
                ClearInjectedCards("active control changed");
                ReadCurrentChannelId();
                if (activeSource != null)
                {
                    SubscribeToCollection(activeSource);
                    SnapshotMessages(activeSource);
                }
                return;
            }
        }

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
            // Flush buffered removes before discarding — real deletions from old source shouldn't be lost
            var flushSettings = UprootedSettings.Load();
            FlushPendingRemoves(flushSettings);
            UnsubscribeCollection();
            _currentItemsSource = currentSource;
            _deletionEpoch++;
            _contentSnapshot.Clear();
            _addedViaEvent.Clear();
            _pendingRemoves.Clear();
            ClearInjectedCards("ItemsSource ref changed");
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

    /// <summary>
    /// Quick scan to find a RootMessageItemsControl that differs from the one we're
    /// currently tracking. Returns null if the current control is still the only active one.
    /// If multiple exist, returns the one with a different ItemsSource reference (new channel).
    /// </summary>
    private object? FindActiveMessageControl()
    {
        try
        {
            object? lastFound = null;
            foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
            {
                var tn = node.GetType().Name;
                if (!tn.Contains("Message", StringComparison.OrdinalIgnoreCase) ||
                    !tn.Contains("ItemsControl")) continue;

                var source = ReadItemsSource(node);
                if (source == null) continue;

                int count = 0;
                try { count = (int)(source.GetType().GetProperty("Count")?.GetValue(source) ?? 0); }
                catch { }

                if (count == 0) continue;

                // If this is a DIFFERENT control or has a different ItemsSource, prefer it
                if (!ReferenceEquals(node, _chatItemsControl) ||
                    !ReferenceEquals(source, _currentItemsSource))
                    return node;

                lastFound = node;
            }
            return lastFound;
        }
        catch { }
        return null;
    }

    private void TryRediscoverChat()
    {
        Logger.Log(Tag, "[rediscover] Scanning for chat control...");
        object? bestControl = null;
        object? bestSource = null;
        int bestCount = 0;

        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            var tn = node.GetType().Name;
            // Look for RootMessageItemsControl specifically, or any message-related ItemsControl
            bool isMessageControl = tn.Contains("Message", StringComparison.OrdinalIgnoreCase);
            if (!isMessageControl && !tn.Contains("ItemsControl") && tn != "ListBox") continue;

            var source = ReadItemsSource(node);
            if (source == null) continue;

            var sourceType = source.GetType();
            bool hasINCC = sourceType.GetInterfaces().Any(i => i.Name == "INotifyCollectionChanged");
            if (!hasINCC) continue;

            int count = 0;
            try { count = (int)(sourceType.GetProperty("Count")?.GetValue(source) ?? 0); }
            catch { }

            // Check if items are message-like
            bool isChat = isMessageControl;
            if (!isChat && count > 0)
            {
                try
                {
                    var en = (source as System.Collections.IEnumerable)?.GetEnumerator();
                    if (en != null && en.MoveNext() && en.Current != null)
                    {
                        // For generic controls, ONLY trust explicit MessageViewModel type name
                        // (don't call TryResolvePropertyAccessors — emoji/reaction items can
                        // have matching property names like Id/Content and produce false positives)
                        isChat = en.Current.GetType().Name.Contains("MessageViewModel");
                    }
                }
                catch { }
            }

            if (!isChat) continue;

            // Prefer message-named controls (RootMessageItemsControl) over generic ones
            bool bestIsMessageNamed = bestControl != null &&
                bestControl.GetType().Name.Contains("Message", StringComparison.OrdinalIgnoreCase);
            bool shouldReplace = bestControl == null
                || (isMessageControl && !bestIsMessageNamed)    // message-named always beats generic
                || (isMessageControl == bestIsMessageNamed && count > bestCount); // same tier: more items wins

            if (shouldReplace)
            {
                bestControl = node;
                bestSource = source;
                bestCount = count;
            }
        }

        if (bestControl != null && bestSource != null)
        {
            Logger.Log(Tag, $"[rediscover] Found: {bestControl.GetType().Name} with {bestCount} items");
            _chatItemsControl = bestControl;
            _deletionEpoch++;
            _currentItemsSource = bestSource;
            _contentSnapshot.Clear();
            _addedViaEvent.Clear();
            ClearInjectedCards("rediscover: new control found");
            ReadCurrentChannelId();
            SubscribeToCollection(bestSource);
            SnapshotMessages(bestSource);
        }
        else
        {
            Logger.Log(Tag, "[rediscover] No chat control found");
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
            _pendingRemoves.Clear();
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

    // ===== PropertyChanged Subscription for Instant Detection =====

    /// <summary>
    /// Subscribe to INotifyPropertyChanged on the Message bridge target for instant
    /// DeletedAt/EditedAt detection. PropertyChanged is a standard CLR event (not a
    /// RoutedEvent), so EventInfo.AddEventHandler is safe here.
    /// </summary>
    private void SubscribePropertyChanged(object item, string msgId)
    {
        if (_propertyChangedSubs.ContainsKey(msgId)) return;

        var tp = GetTypeProps(item);
        if (tp == null) return;

        object? target = GetMessageTarget(item, tp);
        if (target == null) return;

        try
        {
            var pcEvent = target.GetType().GetEvent("PropertyChanged");
            if (pcEvent == null) return;

            // Build handler using Expression.Lambda to match the exact delegate type
            // PropertyChanged uses PropertyChangedEventHandler(object, PropertyChangedEventArgs)
            var handlerType = pcEvent.EventHandlerType;
            if (handlerType == null) return;

            var invokeMethod = handlerType.GetMethod("Invoke");
            if (invokeMethod == null) return;

            var paramTypes = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();
            if (paramTypes.Length != 2) return;

            var capturedMsgId = msgId;
            var capturedEpoch = _deletionEpoch;
            var callback = new Action<object, object>((sender, args) =>
            {
                try
                {
                    // Abort if epoch changed (channel switch)
                    if (_deletionEpoch != capturedEpoch) return;

                    // Read PropertyName from the args
                    var propNameProp = args.GetType().GetProperty("PropertyName");
                    var propName = propNameProp?.GetValue(args) as string;
                    if (propName == null) return;

                    if (propName == "DeletedAt")
                        OnDeletedAtChanged(capturedMsgId, sender);
                    else if (propName == "EditedAt")
                        OnEditedAtChanged(capturedMsgId, sender);
                }
                catch (Exception ex) { Logger.Log(Tag, $"[INPC] handler error: {ex.Message}"); }
            });

            var sParam = Expression.Parameter(paramTypes[0], "sender");
            var eParam = Expression.Parameter(paramTypes[1], "e");
            var body = Expression.Invoke(
                Expression.Constant(callback),
                Expression.Convert(sParam, typeof(object)),
                Expression.Convert(eParam, typeof(object)));
            var handler = Expression.Lambda(handlerType, body, sParam, eParam).Compile();

            pcEvent.AddEventHandler(target, handler);
            _propertyChangedSubs[msgId] = (target, handler);
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"[INPC] Subscribe error for {msgId}: {ex.Message}");
        }
    }

    /// <summary>
    /// Called when DeletedAt changes on a subscribed message — instant deletion detection.
    /// </summary>
    private void OnDeletedAtChanged(string msgId, object sender)
    {
        Logger.Log(Tag, $"[INPC] DeletedAt changed: {msgId}");
        _r.RunOnUIThread(() =>
        {
            try
            {
                if (_messageCache.TryGetValue(msgId, out var cached) && !cached.IsDeleted)
                {
                    cached.IsDeleted = true;
                    cached.DeletedAt = DateTime.UtcNow;
                    _store.RecordDeletion(cached.MessageId, cached.ChannelId, DateTime.UtcNow);
                    Logger.Log(Tag, $"[INPC] MSG DEL (instant): {msgId} ({Truncate(cached.Content, 80)})");

                    var settings = UprootedSettings.Load();
                    InjectDeletedMessageCards(settings);
                }
            }
            catch (Exception ex) { Logger.Log(Tag, $"[INPC] OnDeletedAt error: {ex.Message}"); }
        });
    }

    /// <summary>
    /// Called when EditedAt changes on a subscribed message — instant edit detection.
    /// </summary>
    private void OnEditedAtChanged(string msgId, object sender)
    {
        Logger.Log(Tag, $"[INPC] EditedAt changed: {msgId}");
        _r.RunOnUIThread(() =>
        {
            try
            {
                if (_messageCache.TryGetValue(msgId, out var cached))
                {
                    // Capture previous content from snapshot before it's updated
                    _contentSnapshot.TryGetValue(msgId, out var prevContent);

                    // Read current content from the message model
                    string? currentContent = null;
                    if (_propertyChangedSubs.TryGetValue(msgId, out var sub))
                    {
                        var tp = _typePropsCache.Values.FirstOrDefault(t => t.SenderMember != null) ??
                                 _typePropsCache.Values.FirstOrDefault();
                        if (tp?.Content != null)
                        {
                            try { currentContent = tp.Content.GetValue(sub.target) as string; }
                            catch { }
                        }
                    }

                    if (!string.IsNullOrEmpty(prevContent) &&
                        !string.IsNullOrEmpty(currentContent) &&
                        prevContent != currentContent)
                    {
                        cached.Edits.Add(new MessageEdit { EditTime = DateTime.UtcNow, PreviousContent = prevContent });
                        cached.Content = currentContent;
                        _contentSnapshot[msgId] = currentContent;
                        _store.RecordEdit(msgId, cached.ChannelId, DateTime.UtcNow, prevContent);
                        Logger.Log(Tag, $"[INPC] MSG EDIT (instant): {msgId}: \"{Truncate(prevContent, 40)}\" -> \"{Truncate(currentContent, 40)}\"");

                        var settings = UprootedSettings.Load();
                        InjectEditIndicators(settings);
                    }
                }
            }
            catch (Exception ex) { Logger.Log(Tag, $"[INPC] OnEditedAt error: {ex.Message}"); }
        });
    }

    /// <summary>
    /// Unsubscribe all PropertyChanged handlers. Called on channel switch / epoch change.
    /// </summary>
    private void UnsubscribeAllPropertyChanged()
    {
        foreach (var (msgId, sub) in _propertyChangedSubs)
        {
            try
            {
                var pcEvent = sub.target.GetType().GetEvent("PropertyChanged");
                pcEvent?.RemoveEventHandler(sub.target, sub.handler);
            }
            catch { }
        }
        if (_propertyChangedSubs.Count > 0)
            Logger.Log(Tag, $"[INPC] Unsubscribed {_propertyChangedSubs.Count} PropertyChanged handlers");
        _propertyChangedSubs.Clear();
    }

    private void SnapshotMessages(object collection)
    {
        if (!_propertiesResolved) return;
        _orderedIds.Clear();
        _orderedIdIndex.Clear();
        try
        {
            int snapshotCount = 0;
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
                if (!_orderedIdIndex.ContainsKey(msgId))
                {
                    _orderedIdIndex[msgId] = _orderedIds.Count;
                    _orderedIds.Add(msgId);
                }
                snapshotCount++;
            }
            Logger.Log(Tag, $"Snapshot: {snapshotCount} messages cached");
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

            switch (action)
            {
                case 0: // Add — cache new messages for content preservation
                    var newItems = argsType.GetProperty("NewItems")?.GetValue(args) as System.Collections.IList;
                    if (newItems != null) HandleAdded(newItems);
                    break;

                case 1: // Remove — buffer for debounced deletion detection
                    var oldItems = argsType.GetProperty("OldItems")?.GetValue(args) as System.Collections.IList;
                    int removeIndex = -1;
                    try { removeIndex = (int)(argsType.GetProperty("OldStartingIndex")?.GetValue(args) ?? -1); }
                    catch { }
                    if (oldItems != null) HandleRemoved(oldItems, removeIndex);
                    break;

                case 2: // Replace — snapshot update + event-driven edit detection
                    var oldReplaceItems = argsType.GetProperty("OldItems")?.GetValue(args) as System.Collections.IList;
                    var newReplaceItems = argsType.GetProperty("NewItems")?.GetValue(args) as System.Collections.IList;
                    if (newReplaceItems != null) HandleReplaced(oldReplaceItems, newReplaceItems);
                    break;

                case 4: // Reset — Root rebuilt the collection, force re-discovery
                    Logger.Log(Tag, "Collection Reset — forcing re-discovery");
                    _deletionEpoch++;
                    _contentSnapshot.Clear();
                    _addedViaEvent.Clear();
                    _orderedIds.Clear();
                    _orderedIdIndex.Clear();
                    _pendingRemoves.Clear();
                    ClearInjectedCards("CollectionChanged Reset (action=4)");
                    UnsubscribeCollection();
                    _chatItemsControl = null;
                    _currentItemsSource = null;
                    _subscribed = false;
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
            // Record Add timestamp for edit detection grace-period gating.
            // Only messages seen via Add events (not initial snapshot) are eligible.
            _addedViaEvent[msgId] = DateTime.UtcNow;
            CacheMessage(item, msgId);

            // Subscribe to PropertyChanged on the bridge target for instant deletion/edit detection
            SubscribePropertyChanged(item, msgId);

            // Apply any pending audit log deletion that arrived before this message was cached
            if (_pendingAuditDeletes.TryGetValue(msgId, out var pendingAudit))
            {
                _pendingAuditDeletes.Remove(msgId);
                if (_messageCache.TryGetValue(msgId, out var auditCached))
                {
                    auditCached.IsDeleted = true;
                    auditCached.DeletedAt = DateTime.UtcNow;
                    auditCached.DeletedBy = pendingAudit.ActorId;
                    auditCached.DeletedByName = pendingAudit.ActorName;
                    _store.RecordDeletion(auditCached.MessageId, auditCached.ChannelId,
                        auditCached.DeletedAt.Value, auditCached.DeletedBy, auditCached.DeletedByName);
                    Logger.Log(Tag, $"[audit] Applied pending delete on Add: {msgId} by @{pendingAudit.ActorName}");
                }
            }

            if (!_orderedIdIndex.ContainsKey(msgId))
            {
                _orderedIdIndex[msgId] = _orderedIds.Count;
                _orderedIds.Add(msgId);
            }
            _addsSinceFlush++;
        }
        Logger.Log(Tag, $"[add-event] +{items.Count} (total adds since flush: {_addsSinceFlush})");
    }

    /// <summary>
    /// Called on CollectionChanged Replace (action=2). Updates snapshots and detects genuine
    /// edits using two strategies:
    ///   1. Primary: Check if EditedAt is non-null on new item (confirms genuine edit regardless
    ///      of timing). This bypasses the grace period entirely.
    ///   2. Fallback: Grace-period gating — the message must have arrived via an Add event and
    ///      the Replace must arrive more than EditGracePeriodSeconds after the Add.
    /// </summary>
    private void HandleReplaced(System.Collections.IList? oldItems, System.Collections.IList newItems)
    {
        if (!_propertiesResolved) return;
        var settings = UprootedSettings.Load();

        for (int i = 0; i < newItems.Count; i++)
        {
            var newItem = newItems[i];
            if (newItem == null) continue;
            var msgId = ReadMessageId(newItem);
            if (msgId == null) continue;
            var newContent = ReadContent(newItem) ?? "";

            // Capture old content from snapshot BEFORE overwriting it.
            // Fall back to OldItems if not in snapshot (e.g. first Replace after restart).
            _contentSnapshot.TryGetValue(msgId, out var oldContent);
            if (string.IsNullOrEmpty(oldContent) && oldItems != null && i < oldItems.Count)
            {
                var oldItem = oldItems[i];
                if (oldItem != null) oldContent = ReadContent(oldItem) ?? "";
            }

            // Always update snapshot and cache with new content.
            _contentSnapshot[msgId] = newContent;
            if (_messageCache.TryGetValue(msgId, out var existingCached))
                existingCached.Content = newContent;
            else
                CacheMessage(newItem, msgId);

            // Re-subscribe PropertyChanged on the new item (Replace swaps the ViewModel)
            SubscribePropertyChanged(newItem, msgId);

            // Edit detection — gated by Add-event eligibility.
            if (!settings.MessageLoggerLogEdits) continue;
            if (string.IsNullOrEmpty(oldContent) || oldContent == newContent) continue;

            // Strategy 1: EditedAt non-null on new item → confirmed edit (bypasses grace period)
            bool? editedAtConfirmed = ReadHasBeenEdited(newItem);
            bool confirmedByEditedAt = editedAtConfirmed == true;

            // Strategy 2: Grace-period gating (fallback for items without EditedAt)
            bool confirmedByGracePeriod = false;
            if (!confirmedByEditedAt && _addedViaEvent.TryGetValue(msgId, out var addTime))
            {
                double ageSeconds = (DateTime.UtcNow - addTime).TotalSeconds;
                if (ageSeconds < EditGracePeriodSeconds)
                {
                    Logger.Log(Tag, $"[edit-gate] {msgId}: age={ageSeconds:F1}s < grace ({EditGracePeriodSeconds}s), EditedAt={editedAtConfirmed}, skipping");
                    continue;
                }
                confirmedByGracePeriod = true;
            }

            if (!confirmedByEditedAt && !confirmedByGracePeriod) continue;

            var method = confirmedByEditedAt ? "EditedAt" : "grace-period";
            Logger.Log(Tag, $"[edit-detect] {msgId} (via {method}): \"{Truncate(oldContent, 40)}\" -> \"{Truncate(newContent, 40)}\"");
            if (_messageCache.TryGetValue(msgId, out var cached))
            {
                cached.Edits.Add(new MessageEdit { EditTime = DateTime.UtcNow, PreviousContent = oldContent });
                cached.Content = newContent;
                _store.RecordEdit(msgId, cached.ChannelId, DateTime.UtcNow, oldContent);
            }
        }
    }

    /// <summary>
    /// Buffer removed items and spawn a fast async poller per item.
    ///
    /// Captures the ViewModel and bridge target references NOW (while alive).
    /// Root sets DeletedAt asynchronously after the Remove event, so we
    /// poll the stored references every 300ms for up to 3s.
    /// The poller also re-reads the bridge from the ViewModel on each tick
    /// (in case Root replaces the bridge target object).
    /// An epoch counter cancels pollers after channel switches.
    /// </summary>
    private void HandleRemoved(System.Collections.IList items, int removeIndex)
    {
        if (!_propertiesResolved) return;
        foreach (var item in items)
        {
            if (item == null) continue;
            var msgId = ReadMessageId(item);
            if (msgId == null) continue;

            // Skip already-deleted messages — don't spawn pollers for them (prevents spam)
            if (_messageCache.TryGetValue(msgId, out var alreadyCached) && alreadyCached.IsDeleted)
            {
                Logger.Log(Tag, $"[remove-event] skipped (already deleted): {msgId}");
                continue;
            }

            var content = ReadContent(item) ?? "";

            // Also check cache for richer content (sometimes ViewModel content is empty on removal)
            if (string.IsNullOrEmpty(content) && alreadyCached != null)
                content = alreadyCached.Content;

            // Capture ViewModel + bridge target references
            var tp = GetTypeProps(item);
            object? bridgeTarget = null;
            if (tp != null)
            {
                try { bridgeTarget = GetMessageTarget(item, tp); }
                catch { }
            }

            _removesSinceFlush++;
            var buffered = new BufferedRemove
            {
                MessageId = msgId,
                Content = content,
                ViewModel = item,
                BridgeTarget = bridgeTarget,
                Props = tp,
                RemoveIndex = removeIndex,
            };
            _pendingRemoves.Add(buffered);
            Logger.Log(Tag, $"[remove-event] buffered: {msgId} idx={removeIndex} bridgeTarget={bridgeTarget != null} ({Truncate(content, 40)})");

            // Spawn async poller
            if (tp != null)
            {
                var epoch = _deletionEpoch;
                var doDump = _boolDumpCount < 3;
                if (doDump) _boolDumpCount++;
                StartDeletionPoller(buffered, epoch, doDump);
            }
        }
    }

    /// <summary>
    /// Async poller: checks DeletedAt every 300ms for up to 3s.
    ///
    /// On each tick:
    ///   1. Check epoch — abort if channel switched
    ///   2. Re-read bridge target from ViewModel (catches bridge replacement)
    ///   3. Read DeletedAt from both captured and fresh bridge targets
    ///   4. If non-null → mark as deleted + inject card on UI thread
    ///
    /// Fallback: If DeletedAt stays null after 3s (Root doesn't set it for self-deletes),
    /// check if the message is actually absent from the live collection. If gone, it's
    /// a genuine deletion.
    ///
    /// On first and last attempt for the first few removes, dumps boolean +
    /// DateTimeOffset? properties on the bridge target for diagnostics.
    /// </summary>
    private void StartDeletionPoller(BufferedRemove remove, int epochAtRemoval, bool dumpBoolProps)
    {
        Task.Run(async () =>
        {
            for (int attempt = 0; attempt < 10; attempt++)
            {
                await Task.Delay(300);

                // Epoch check: abort if channel switched
                if (_deletionEpoch != epochAtRemoval)
                {
                    Logger.Log(Tag, $"[async-poll] {remove.MessageId}: epoch changed ({epochAtRemoval}->{_deletionEpoch}), aborting");
                    return;
                }

                // Try BOTH the captured bridge target AND a fresh re-read from the ViewModel
                bool? valCaptured = ReadDeferredHasBeenDeleted(remove.BridgeTarget, remove.Props);
                bool? valFresh = ReadFreshHasBeenDeleted(remove.ViewModel, remove.Props);

                if (valCaptured == true || valFresh == true)
                {
                    Logger.Log(Tag, $"[async-poll] {remove.MessageId}: DeletedAt=non-null at attempt {attempt} ({(attempt + 1) * 300}ms) captured={valCaptured} fresh={valFresh}");
                    _r.RunOnUIThread(() =>
                    {
                        if (_deletionEpoch != epochAtRemoval) return;
                        MarkAsDeleted(remove);
                        var settings = UprootedSettings.Load();
                        InjectDeletedMessageCards(settings);
                    });
                    return;
                }

                // Diagnostic: dump all boolean + DateTimeOffset? properties on first and last attempt
                if (dumpBoolProps && (attempt == 0 || attempt == 9))
                    DumpBooleanProperties(remove, attempt);
            }

            // After 3s, DeletedAt still null. Root doesn't set it for self-initiated deletes.
            // Fallback: check if the message is absent from the live collection — if gone, it's
            // a genuine deletion (not buffer management, which re-adds at a different index).
            if (_deletionEpoch != epochAtRemoval) return;

            _r.RunOnUIThread(() =>
            {
                try
                {
                    if (_deletionEpoch != epochAtRemoval) return;

                    // Check if the message ID is still in the live collection
                    bool stillPresent = IsMessageInCollection(remove.MessageId);
                    if (!stillPresent && !string.IsNullOrEmpty(remove.Content))
                    {
                        Logger.Log(Tag, $"[async-poll] {remove.MessageId}: DeletedAt=null but ABSENT from collection — marking as deleted (self-delete fallback)");
                        MarkAsDeleted(remove);
                        var settings = UprootedSettings.Load();
                        InjectDeletedMessageCards(settings);
                    }
                    else
                    {
                        Logger.Log(Tag, $"[async-poll] {remove.MessageId}: DeletedAt=null, present={stillPresent} — buffer management");
                    }
                }
                catch (Exception ex) { Logger.Log(Tag, $"[async-poll] fallback error: {ex.Message}"); }
            });
        });
    }

    /// <summary>
    /// Check if a message ID is currently present in the live collection.
    /// Must be called on the UI thread.
    /// </summary>
    private bool IsMessageInCollection(string msgId)
    {
        if (_currentItemsSource == null) return false;
        try
        {
            var en = (_currentItemsSource as System.Collections.IEnumerable)?.GetEnumerator();
            if (en == null) return false;
            while (en.MoveNext())
            {
                if (en.Current == null) continue;
                var id = ReadMessageId(en.Current);
                if (id == msgId) return true;
            }
        }
        catch { }
        return false;
    }

    /// <summary>
    /// Re-read DeletedAt from ViewModel by re-traversing the bridge.
    /// Catches cases where Root replaces the bridge target object after removal.
    /// </summary>
    private static bool? ReadFreshHasBeenDeleted(object? viewModel, TypeProps? props)
    {
        if (viewModel == null || props == null) return null;
        try
        {
            object? target = viewModel;
            if (props.Bridge != null)
                target = props.Bridge.GetValue(viewModel);
            if (target == null || props.DeletedAt == null) return null;
            var val = props.DeletedAt.GetValue(target);
            // DateTimeOffset? — non-null means deleted; also handle legacy bool
            if (val is bool b) return b;
            if (val is DateTimeOffset) return true;
            if (props.DeletedAt.PropertyType == typeof(DateTimeOffset?) && val == null) return false;
            return null;
        }
        catch { return null; }
    }

    /// <summary>
    /// Diagnostic: log boolean + DateTimeOffset? properties on the bridge target.
    /// Reveals which property Root uses for deletion/edit signaling.
    /// </summary>
    private static void DumpBooleanProperties(BufferedRemove remove, int attempt)
    {
        try
        {
            var target = remove.BridgeTarget;
            if (target == null) return;
            var tag = attempt == 0 ? "prop-dump-t0" : "prop-dump-t3s";
            var props = target.GetType().GetProperties(
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var p in props)
            {
                if (!p.CanRead) continue;

                // Dump bool properties
                if (p.PropertyType == typeof(bool))
                {
                    try
                    {
                        var v = p.GetValue(target);
                        Logger.Log("MsgLogger", $"[{tag}] {remove.MessageId}: {p.Name}={v}");
                    }
                    catch { }
                }
                // Dump DateTimeOffset? and DateTimeOffset properties (DeletedAt, EditedAt, PinnedAt etc.)
                else if (p.PropertyType == typeof(DateTimeOffset?) || p.PropertyType == typeof(DateTimeOffset))
                {
                    try
                    {
                        var v = p.GetValue(target);
                        Logger.Log("MsgLogger", $"[{tag}] {remove.MessageId}: {p.Name}={v}");
                    }
                    catch { }
                }
            }
        }
        catch { }
    }

    /// <summary>
    /// Flush buffered removes: cleanup content snapshots and detect bulk channel switches.
    /// Actual deletion detection is handled by async pollers (StartDeletionPoller).
    /// This just manages housekeeping and the bulk safety net.
    /// </summary>
    private void FlushPendingRemoves(UprootedSettings settings)
    {
        if (_pendingRemoves.Count == 0)
        {
            _addsSinceFlush = 0;
            _removesSinceFlush = 0;
            return;
        }

        int count = _pendingRemoves.Count;

        int collectionSize = 0;
        try
        {
            if (_currentItemsSource != null)
                collectionSize = (int)(_currentItemsSource.GetType().GetProperty("Count")?.GetValue(_currentItemsSource) ?? 0);
        }
        catch { }

        Logger.Log(Tag, $"[flush] removes={count} adds={_addsSinceFlush} collectionSize={collectionSize}");

        // Bulk safety net: 10+ removes is a channel switch
        if (count >= 10)
        {
            _deletionEpoch++;
            Logger.Log(Tag, $"Bulk removes ({count}) — channel switch, discarding (epoch={_deletionEpoch})");
            _pendingRemoves.Clear();
            _contentSnapshot.Clear();
            _addedViaEvent.Clear();
            _orderedIds.Clear();
            _orderedIdIndex.Clear();
            ClearInjectedCards("bulk removes (channel switch)");
            _addsSinceFlush = 0;
            _removesSinceFlush = 0;
            return;
        }

        // Clean up content snapshots for removed messages
        foreach (var removed in _pendingRemoves)
            _contentSnapshot.Remove(removed.MessageId);

        _pendingRemoves.Clear();
        _addsSinceFlush = 0;
        _removesSinceFlush = 0;
    }

    /// <summary>
    /// Mark a buffered remove as a genuine deletion — persist to cache and store.
    /// </summary>
    private void MarkAsDeleted(BufferedRemove removed)
    {
        Logger.Log(Tag, $"[DIAG-FLUSH] About to mark MSG DEL: {removed.MessageId}, cache has={_messageCache.ContainsKey(removed.MessageId)}, IsDeleted already={(_messageCache.TryGetValue(removed.MessageId, out var peekCached) ? peekCached.IsDeleted.ToString() : "N/A")}");
        if (_messageCache.TryGetValue(removed.MessageId, out var cached) && !cached.IsDeleted)
        {
            cached.IsDeleted = true;
            cached.DeletedAt = DateTime.UtcNow;
            _store.RecordDeletion(removed.MessageId, cached.ChannelId, DateTime.UtcNow);
            Logger.Log(Tag, $"MSG DEL: {removed.MessageId} ({Truncate(cached.Content, 80)})");
        }
        else if (!_messageCache.ContainsKey(removed.MessageId) && !string.IsNullOrEmpty(removed.Content))
        {
            var newCached = new CachedMessage
            {
                MessageId = removed.MessageId,
                ChannelId = _currentChannelId,
                AuthorId = "",
                AuthorName = "Unknown",
                Timestamp = DateTime.UtcNow,
                Content = removed.Content,
                IsDeleted = true,
                DeletedAt = DateTime.UtcNow
            };
            _messageCache[removed.MessageId] = newCached;
            _store.RecordMessage(newCached.MessageId, newCached.ChannelId,
                newCached.AuthorId, newCached.AuthorName, newCached.Timestamp, newCached.Content);
            _store.RecordDeletion(removed.MessageId, newCached.ChannelId, DateTime.UtcNow);
            Logger.Log(Tag, $"MSG DEL (uncached): {removed.MessageId} ({Truncate(removed.Content, 80)})");
        }
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
                    bool? hasBeenEdited = ReadHasBeenEdited(item);
                    // null = property not discoverable; treat as confirmed (unknown Root version)
                    bool confirmed = hasBeenEdited != false;
                    if (confirmed && _messageCache.TryGetValue(msgId, out var cached))
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
    /// into the nearest visible message's layout Grid as a new row. This follows the
    /// LinkEmbedEngine pattern — injecting into message Grids rather than the
    /// VirtualizingStackPanel (which doesn't arrange non-data-bound children).
    ///
    /// When the VSP recycles a message container (scroll), the card is lost. The next
    /// scan tick detects the missing tag and re-injects.
    /// </summary>
    private void InjectDeletedMessageCards(UprootedSettings settings)
    {
        if (!settings.MessageLoggerLogDeletes || _chatItemsControl == null) return;

        Logger.Log(Tag, "[DIAG-INJ] === InjectDeletedMessageCards start ===");

        object? vsp = FindMessagePanel();
        Logger.Log(Tag, $"[DIAG-INJ] FindMessagePanel: {(vsp != null ? "found" : "null")}, type={vsp?.GetType().Name ?? "N/A"}");
        if (vsp == null) return;

        // Build list of visible messages with their containers (VSP realized items), in VSP order
        var visibleMessages = new List<(string msgId, object container)>();
        int childCount = _r.GetChildCount(vsp);
        Logger.Log(Tag, $"[DIAG-INJ] VSP childCount={childCount}");
        for (int i = 0; i < childCount; i++)
        {
            var child = _r.GetChild(vsp, i);
            if (child == null) continue;
            var dc = ReadDC(child);
            var msgId = dc != null ? ReadMessageId(dc) : null;
            if (i < 5 || (i == childCount - 1)) // Log first 5 + last for brevity
                Logger.Log(Tag, $"[DIAG-INJ]   child[{i}]: type={child.GetType().Name}, DC={dc?.GetType().Name ?? "null"}, msgId={msgId ?? "null"}");
            if (dc == null || msgId == null) continue;
            if (!_messageCache.ContainsKey(msgId)) continue;
            visibleMessages.Add((msgId, child));
        }

        Logger.Log(Tag, $"[DIAG-INJ] visibleMessages: {visibleMessages.Count} found");
        if (visibleMessages.Count == 0) return;

        // Count deleted messages for this channel
        int deletedInChannel = 0, totalDeleted = 0;
        foreach (var c in _messageCache.Values)
        {
            if (c.IsDeleted) totalDeleted++;
            if (c.IsDeleted && c.ChannelId == _currentChannelId) deletedInChannel++;
        }
        Logger.Log(Tag, $"[DIAG-INJ] deletedInChannel: {deletedInChannel} (of {totalDeleted} total)");

        // One-time tree dump (1E): on first call with both deleted + visible messages
        if (!_firstTreeDumpDone && deletedInChannel > 0 && visibleMessages.Count > 0)
        {
            _firstTreeDumpDone = true;
            Logger.Log(Tag, "[DIAG-TREE] === One-time visual tree dump ===");
            Logger.Log(Tag, $"[DIAG-TREE] VSP type: {vsp.GetType().Name}");
            var firstContainer = visibleMessages[0].container;
            DumpContainerTree(firstContainer, 1, 4);
            Logger.Log(Tag, "[DIAG-TREE] === End tree dump ===");
        }

        int injected = 0, skipped = 0;

        // For each deleted message in current channel
        foreach (var cached in _messageCache.Values)
        {
            if (!cached.IsDeleted || cached.ChannelId != _currentChannelId) continue;
            if (cached.DeletedAt == null) continue;
            if ((DateTime.UtcNow - cached.DeletedAt.Value).TotalHours > 24) continue;

            Logger.Log(Tag, $"[DIAG-INJ]   trying: {cached.MessageId} ts={cached.Timestamp:HH:mm:ss}");

            var cardTag = $"uprooted-del:{cached.MessageId}";

            // Find the best visible message to attach to: the visible message with the largest
            // insertion-order index that still precedes the deleted message in collection order.
            // This is more reliable than timestamp comparison (timestamps may not resolve correctly).
            object? targetContainer = null;
            string? targetMsgId = null;
            int deletedOrder = _orderedIdIndex.TryGetValue(cached.MessageId, out var doi) ? doi : int.MaxValue;
            for (int i = visibleMessages.Count - 1; i >= 0; i--)
            {
                if (_orderedIdIndex.TryGetValue(visibleMessages[i].msgId, out var vIdx) && vIdx <= deletedOrder)
                { targetContainer = visibleMessages[i].container; targetMsgId = visibleMessages[i].msgId; break; }
            }
            // Fallback: attach to the first visible message (deleted msg older than all visible)
            if (targetContainer == null)
            {
                targetContainer = visibleMessages[0].container;
                targetMsgId = visibleMessages[0].msgId;
            }
            Logger.Log(Tag, $"[DIAG-INJ]     targetContainer: {(targetContainer != null ? "found" : "null")}, targetMsgId={targetMsgId ?? "null"}");
            if (targetContainer == null) { skipped++; continue; }

            // Find the message layout Grid inside the container
            var (grid, col) = FindMessageGridInContainer(targetContainer);
            Logger.Log(Tag, $"[DIAG-INJ]     FindMessageGridInContainer: grid={grid != null}, col={col}");
            if (grid == null)
            {
                // Dump first 10 descendants for debugging
                var descTypes = new List<string>();
                foreach (var desc in _walker.DescendantsDepthFirst(targetContainer))
                {
                    descTypes.Add(desc.GetType().Name);
                    if (descTypes.Count >= 10) break;
                }
                Logger.Log(Tag, $"[DIAG-INJ]     (grid null) DFS descendants: {string.Join(", ", descTypes)}");
                skipped++;
                continue;
            }

            // Dedup: check if card already exists in this Grid (tag-based)
            bool exists = false;
            var gridChildren = _r.GetChildren(grid);
            if (gridChildren != null)
            {
                foreach (var c in gridChildren)
                    if (c != null && _r.GetTag(c) == cardTag) { exists = true; break; }
            }
            Logger.Log(Tag, $"[DIAG-INJ]     dedup check: exists={exists}");
            if (exists) { skipped++; continue; }

            var card = BuildDeletedMessageCard(cached, cardTag);
            Logger.Log(Tag, $"[DIAG-INJ]     BuildDeletedMessageCard: {(card != null ? "success" : "null")}");
            if (card == null) { skipped++; continue; }

            // Add a new Auto-height row to the Grid and place the card there
            int newRow = AddAutoRowToGrid(grid);
            Logger.Log(Tag, $"[DIAG-INJ]     AddAutoRowToGrid: row={newRow}");
            _r.SetGridRow(card, newRow);
            _r.SetGridColumn(card, col);
            SetGridColumnSpan(card, 99);

            _r.AddChild(grid, card);
            _injectedCards[cardTag] = card;
            int gridChildCount = 0;
            try { gridChildCount = _r.GetChildCount(grid); } catch { }
            Logger.Log(Tag, $"[DIAG-INJ]     AddChild result: card added, Grid now has {gridChildCount} children");
            injected++;
        }

        Logger.Log(Tag, $"[DIAG-INJ] === InjectDeletedMessageCards done: {injected} injected, {skipped} skipped ===");
    }

    /// <summary>
    /// Walk into a message container to find the message layout Grid.
    /// Follows the same tree path as LinkEmbedEngine: find RootMarkdownTextBlock,
    /// then its parent Grid is the injection target.
    /// Returns (grid, column) where column matches the message text position.
    /// </summary>
    private (object? grid, int column) FindMessageGridInContainer(object container)
    {
        foreach (var node in _walker.DescendantsDepthFirst(container))
        {
            if (node.GetType().Name != "RootMarkdownTextBlock") continue;

            int col = _r.GetGridColumn(node);
            var grid = _r.GetParent(node);
            if (grid != null && _r.IsGrid(grid))
                return (grid, col);

            // One more level up (Grid may be wrapped)
            if (grid != null)
            {
                var above = _r.GetParent(grid);
                if (above != null && _r.IsGrid(above))
                    return (above, col);
            }
        }
        return (null, 0);
    }

    /// <summary>
    /// Add a new Auto-height RowDefinition to a Grid and return its row index.
    /// Pattern from LinkEmbedEngine.
    /// </summary>
    private int AddAutoRowToGrid(object grid)
    {
        try
        {
            var rowDefsProp = grid.GetType().GetProperty("RowDefinitions");
            var rowDefs = rowDefsProp?.GetValue(grid);
            if (rowDefs == null) return 99;

            int count = 0;
            var countProp = rowDefs.GetType().GetProperty("Count");
            if (countProp != null)
                count = (int)countProp.GetValue(rowDefs)!;

            if (_r.GridLengthType != null && _r.GridUnitTypeEnum != null)
            {
                var autoUnit = Enum.Parse(_r.GridUnitTypeEnum, "Auto");
                var autoLength = Activator.CreateInstance(_r.GridLengthType, 1.0, autoUnit);

                var rowDefType = _r.GridType?.Assembly.GetType("Avalonia.Controls.RowDefinition");
                if (rowDefType != null)
                {
                    var rowDef = Activator.CreateInstance(rowDefType);
                    rowDefType.GetProperty("Height")?.SetValue(rowDef, autoLength);

                    var addMethod = rowDefs.GetType().GetMethod("Add");
                    addMethod?.Invoke(rowDefs, new[] { rowDef });

                    return count;
                }
            }
        }
        catch (Exception ex) { Logger.Log(Tag, $"AddAutoRowToGrid error: {ex.Message}"); }
        return 99;
    }

    private void SetGridColumnSpan(object control, int span)
    {
        if (_r.GridType == null) return;
        var setColumnSpan = _r.GridType.GetMethod("SetColumnSpan",
            BindingFlags.Public | BindingFlags.Static);
        setColumnSpan?.Invoke(null, new object[] { control, span });
    }

    /// <summary>
    /// Build a deleted message card: red-tinted background stripe with preserved content.
    /// Right-click context menu has "Clear message history".
    /// </summary>
    private object? BuildDeletedMessageCard(CachedMessage cached, string tag)
    {
        try
        {
            // Message content — white text, wrapping
            var body = _r.CreateTextBlock(cached.Content, 13, "#DDDDDD");
            if (body == null) return null;
            _r.SetTextWrapping(body, "Wrap");

            // If we have actor attribution, wrap body + actor label in a StackPanel
            object? cardContent = body;
            if (!string.IsNullOrEmpty(cached.DeletedByName) || !string.IsNullOrEmpty(cached.DeletedBy))
            {
                var actorDisplay = !string.IsNullOrEmpty(cached.DeletedByName)
                    ? $"Deleted by @{cached.DeletedByName}"
                    : $"Deleted by {cached.DeletedBy[..Math.Min(8, cached.DeletedBy.Length)]}…";
                var actorLabel = _r.CreateTextBlock(actorDisplay, 10, "#99FF6666");
                if (actorLabel != null)
                {
                    _r.SetMargin(actorLabel, 0, 2, 0, 0);
                    var inner = _r.CreateStackPanel(vertical: true, spacing: 0);
                    if (inner != null)
                    {
                        _r.AddChild(inner, body);
                        _r.AddChild(inner, actorLabel);
                        cardContent = inner;
                    }
                }
            }

            // Full-width red-tinted background stripe
            var card = _r.CreateBorder("#28FF4444", cornerRadius: 0, child: cardContent);
            if (card == null) return null;

            // Red left accent border (3px) via nested border
            var leftAccent = _r.CreateBorder("#60FF4444", cornerRadius: 0, child: null);
            if (leftAccent != null)
            {
                leftAccent.GetType().GetProperty("Width")?.SetValue(leftAccent, 3.0);
                _r.SetHorizontalAlignment(leftAccent, "Left");
            }

            // Wrap in a panel to overlay the left accent
            var wrapper = _r.CreatePanel();
            if (wrapper != null)
            {
                _r.AddChild(wrapper, card);
                if (leftAccent != null) _r.AddChild(wrapper, leftAccent);
                _r.SetTag(wrapper, tag);
                _r.SetPadding(card, 8, 4, 8, 4);
                AttachContextMenu(wrapper, cached);
                return wrapper;
            }

            // Fallback if panel creation fails
            _r.SetTag(card, tag);
            _r.SetPadding(card, 8, 4, 8, 4);
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
    /// For each message with edit history in the current channel, inject an amber-tinted
    /// inline card into the message's layout Grid (below the message text) showing the
    /// previous content and an "(edited)" label. Mirrors InjectDeletedMessageCards().
    /// </summary>
    private void InjectEditIndicators(UprootedSettings settings)
    {
        if (!settings.MessageLoggerLogEdits || _chatItemsControl == null) return;

        var vsp = FindMessagePanel();
        if (vsp == null) return;

        // Build msgId → container map from visible VSP children
        var visible = new Dictionary<string, object>();
        int childCount = _r.GetChildCount(vsp);
        for (int i = 0; i < childCount; i++)
        {
            var child = _r.GetChild(vsp, i);
            if (child == null) continue;
            var dc = ReadDC(child);
            var msgId = dc != null ? ReadMessageId(dc) : null;
            if (msgId != null) visible[msgId] = child;
        }

        foreach (var cached in _messageCache.Values)
        {
            if (cached.Edits.Count == 0 || cached.IsDeleted) continue;
            if (!visible.TryGetValue(cached.MessageId, out var container)) continue;

            var editTag = $"uprooted-edit:{cached.MessageId}";

            var (grid, col) = FindMessageGridInContainer(container);
            if (grid == null) continue;

            // Dedup check
            var gridChildren = _r.GetChildren(grid);
            if (gridChildren != null)
            {
                bool exists = false;
                foreach (var c in gridChildren)
                    if (c != null && _r.GetTag(c) == editTag) { exists = true; break; }
                if (exists) continue;
            }

            var card = BuildEditIndicatorCard(cached, editTag);
            if (card == null) continue;

            int newRow = AddAutoRowToGrid(grid);
            _r.SetGridRow(card, newRow);
            _r.SetGridColumn(card, col);
            SetGridColumnSpan(card, 99);
            _r.AddChild(grid, card);
            _injectedCards[editTag] = card;
            Logger.Log(Tag, $"[EDIT] Injected indicator for {cached.MessageId} ({cached.Edits.Count} edits)");
        }
    }

    /// <summary>
    /// Build an edit indicator card: amber-tinted background stripe showing the previous
    /// content (faded, italic) and an "(edited)" label. Mirrors BuildDeletedMessageCard().
    /// </summary>
    private object? BuildEditIndicatorCard(CachedMessage cached, string tag)
    {
        try
        {
            var lastEdit = cached.Edits[cached.Edits.Count - 1];
            string editLabel = cached.Edits.Count > 1
                ? $"(edited {cached.Edits.Count}x)"
                : "(edited)";

            // Previous content — faded, italic
            var prevText = _r.CreateTextBlock(lastEdit.PreviousContent, 13, "#99BBBBBB");
            if (prevText == null) return null;
            _r.SetTextWrapping(prevText, "Wrap");
            SetFontStyleItalic(prevText);

            // "(edited)" label — very small, faded amber
            var label = _r.CreateTextBlock(editLabel, 10, "#99D4A843");
            if (label == null) return null;
            _r.SetMargin(label, 0, 2, 0, 0);

            // Stack previous content + label vertically
            var inner = _r.CreateStackPanel(vertical: true, spacing: 0);
            if (inner == null) return null;
            _r.AddChild(inner, prevText);
            _r.AddChild(inner, label);

            // Amber-tinted background stripe
            var card = _r.CreateBorder("#1AFFCC44", cornerRadius: 0, child: inner);
            if (card == null) return null;
            _r.SetPadding(card, 8, 4, 8, 4);

            // Amber left accent border (3px)
            var leftAccent = _r.CreateBorder("#50FFCC44", cornerRadius: 0, child: null);
            if (leftAccent != null)
            {
                leftAccent.GetType().GetProperty("Width")?.SetValue(leftAccent, 3.0);
                _r.SetHorizontalAlignment(leftAccent, "Left");
            }

            var wrapper = _r.CreatePanel();
            if (wrapper != null)
            {
                _r.AddChild(wrapper, card);
                if (leftAccent != null) _r.AddChild(wrapper, leftAccent);
                _r.SetTag(wrapper, tag);
                return wrapper;
            }

            _r.SetTag(card, tag);
            return card;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"BuildEditCard error: {ex.Message}");
            return null;
        }
    }

    private void SetFontStyleItalic(object textBlock)
    {
        try
        {
            var prop = textBlock.GetType().GetProperty("FontStyle");
            if (prop == null) return;
            var ft = prop.PropertyType;
            if (ft.IsEnum)
                prop.SetValue(textBlock, Enum.Parse(ft, "Italic"));
        }
        catch { }
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

    /// <summary>
    /// Diagnostic: dump the visual tree of a container for debugging grid injection.
    /// </summary>
    private void DumpContainerTree(object node, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;
        var indent = new string(' ', depth * 2);
        var typeName = node.GetType().Name;
        var childCount = _r.GetChildCount(node);
        Logger.Log(Tag, $"[DIAG-TREE] {indent}{typeName} (children={childCount})");
        for (int i = 0; i < childCount && i < 8; i++)
        {
            var child = _r.GetChild(node, i);
            if (child != null) DumpContainerTree(child, depth + 1, maxDepth);
        }
    }

    private void ClearInjectedCards(string reason = "unknown")
    {
        Logger.Log(Tag, $"[DIAG] ClearInjectedCards: {reason}, clearing {_injectedCards.Count} cards");
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
        UnsubscribeAllPropertyChanged();
        _boolDumpCount = 0; // reset so next epoch gets fresh diagnostic dumps
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

    /// <summary>
    /// Read DeletedAt from a stored bridge target reference (deferred read).
    /// Called at flush time, 1.5–3s after the Remove event, giving Root time to set the flag.
    /// DeletedAt is DateTimeOffset? — non-null means deleted.
    /// Returns true/false/null. Handles disposed objects gracefully.
    /// </summary>
    private static bool? ReadDeferredHasBeenDeleted(object? bridgeTarget, TypeProps? props)
    {
        if (bridgeTarget == null || props?.DeletedAt == null) return null;
        try
        {
            var val = props.DeletedAt.GetValue(bridgeTarget);
            // DateTimeOffset? — non-null means deleted; also handle legacy bool
            if (val is bool b) return b;
            if (val is DateTimeOffset) return true;
            // null DateTimeOffset? = not deleted
            if (props.DeletedAt.PropertyType == typeof(DateTimeOffset?) && val == null) return false;
            return null;
        }
        catch { return null; }
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

    private bool? ReadHasBeenEdited(object item)
    {
        var tp = GetTypeProps(item);
        if (tp?.EditedAt == null) return null;
        var t = GetMessageTarget(item, tp);
        if (t == null) return null;
        try
        {
            var val = tp.EditedAt.GetValue(t);
            // DateTimeOffset? — non-null means edited; also handle legacy bool
            if (val is bool b) return b;
            if (val is DateTimeOffset) return true;
            if (tp.EditedAt.PropertyType == typeof(DateTimeOffset?) && val == null) return false;
            return null;
        }
        catch { return null; }
    }

    /// <summary>
    /// Resolve author display name via the deep chain: Message.SenderMember.GlobalUser.UserName.
    /// Falls back to SenderMember.GlobalUser.ToString() if UserName is missing.
    /// </summary>
    private static string? ReadAuthorName(object messageTarget, TypeProps tp)
    {
        if (tp.SenderMember == null) return null;
        try
        {
            var sender = tp.SenderMember.GetValue(messageTarget);
            if (sender == null) return null;

            var globalUserProp = sender.GetType().GetProperty("GlobalUser",
                BindingFlags.Public | BindingFlags.Instance);
            var globalUser = globalUserProp?.GetValue(sender);
            if (globalUser == null) return null;

            // Try UserName first, then DisplayName, then Name
            var userNameProp = globalUser.GetType().GetProperty("UserName",
                BindingFlags.Public | BindingFlags.Instance)
                ?? globalUser.GetType().GetProperty("DisplayName",
                    BindingFlags.Public | BindingFlags.Instance)
                ?? globalUser.GetType().GetProperty("Name",
                    BindingFlags.Public | BindingFlags.Instance);

            var name = userNameProp?.GetValue(globalUser) as string;
            if (!string.IsNullOrEmpty(name)) return name;

            // Fallback: ToString on GlobalUser
            var str = globalUser.ToString();
            if (!string.IsNullOrEmpty(str) && str != globalUser.GetType().FullName) return str;

            return null;
        }
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
            // Resolve author: direct AuthorId prop, or deep chain SenderMember.GlobalUser.UserName
            string authorId = tp.AuthorId != null ? (tp.AuthorId.GetValue(t)?.ToString() ?? "") : "";
            string authorName = ReadAuthorName(t, tp) ?? "Unknown";

            return new CachedMessage
            {
                MessageId = msgId,
                ChannelId = _currentChannelId,
                AuthorId = authorId,
                AuthorName = authorName,
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
