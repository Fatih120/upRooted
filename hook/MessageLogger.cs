using System.Linq.Expressions;
using System.Reflection;

namespace Uprooted;

/// <summary>
/// Message logger plugin — preserves deleted messages and tracks edits.
///
/// Deletion detection: subscribes to Channel.Messages.ConnectMessages() (DynamicData
/// IObservable&lt;IChangeSet&lt;Message&gt;&gt;) which fires Remove events only for genuine
/// deletions (not virtualization buffer management). Falls back to ObservableCollection
/// subscription if ConnectMessages() is unavailable.
///
/// Edit detection: content snapshot comparison on Replace/Refresh change events.
/// Root shows "(edited)" natively — we only persist edit history, no visual indicators.
///
/// Display: Deleted messages are re-injected into the chat as red-tinted inline panels.
/// Right-click shows "Clear message history" to remove the persisted message.
/// </summary>
internal class MessageLogger : IDisposable
{
    private const string Tag = "MsgLogger";
    private const int ScanIntervalMs = 500;

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly object _mainWindow;
    private readonly MessageStore _store;

    private Timer? _scanTimer;
    private int _scanning;
    private readonly TailSampler _sampler = new(heartbeatTicks: 60, slowThresholdMs: 50);

    // Message caches
    private readonly Dictionary<string, CachedMessage> _messageCache = new();
    private readonly Dictionary<string, string> _contentSnapshot = new();

    // DynamicData subscription (primary path)
    private IDisposable? _changeSetSub;
    private object? _currentContentVM;
    private bool _usingDynamicData;

    // Fallback: ObservableCollection subscription
    private object? _chatItemsControl;
    private object? _currentItemsSource;
    private Delegate? _collectionChangedHandler;
    private EventInfo? _collectionChangedEvent;
    private bool _subscribed;

    // Property resolution (per-type cache)
    private readonly Dictionary<Type, TypeProps> _typePropsCache = new();
    private bool _propertiesResolved;

    private class TypeProps
    {
        public PropertyInfo? Bridge;
        public PropertyInfo? MessageId;
        public PropertyInfo? Content;
        public PropertyInfo? AuthorId;
        public PropertyInfo? Timestamp;
        public PropertyInfo? DeletedAt;
        public PropertyInfo? EditedAt;
        public PropertyInfo? SenderMember;
    }

    // Channel tracking
    private string _currentChannelId = "";
    private int _channelCheckCounter;

    // Visual indicator tracking — tag → injected control
    private readonly Dictionary<string, object> _injectedCards = new();
    private readonly Dictionary<string, (object grid, object rowDef, object? container, Delegate? dcHandler)> _injectedRowDefs = new();

    // Audit log integration
    private readonly Dictionary<string, AuditLogEntry> _pendingAuditDeletes = new();

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
        using var ev = WideEvent.Begin(Tag, "initialize");

        _store.LoadAll(_messageCache);
        ev.Set("cached_loaded", _messageCache.Count);

        var settings = UprootedSettings.Load();
        if (_messageCache.Count > settings.MessageLoggerMaxMessages)
        {
            _store.Truncate(settings.MessageLoggerMaxMessages);
            ev.Set("truncated_to", settings.MessageLoggerMaxMessages);
        }

        _r.RunOnUIThread(() =>
        {
            try
            {
                // Try DynamicData subscription first (primary path)
                var contentVM = FindContentViewModel();
                if (contentVM != null && TrySubscribeConnectMessages(contentVM))
                {
                    _currentContentVM = contentVM;
                    _usingDynamicData = true;
                    ev.Set("mode", "dynamic_data");
                }
                else
                {
                    // Fallback: ObservableCollection subscription
                    FindChatItemsControl();
                    if (_chatItemsControl != null)
                    {
                        _currentItemsSource = ReadItemsSource(_chatItemsControl);
                        if (_currentItemsSource != null)
                        {
                            ResolvePropsFromCollection(_currentItemsSource);
                            SubscribeToCollection(_currentItemsSource);
                            SnapshotMessages(_currentItemsSource);
                        }
                    }
                    ev.Set("mode", "collection_fallback");
                }

                // Also find chat control for visual injection (needed by both paths)
                if (_chatItemsControl == null)
                    FindChatItemsControl();
                ReadCurrentChannelId();
            }
            catch (Exception ex) { Logger.LogException(Tag, "Initialize error", ex); }
        });

        _scanTimer = new Timer(OnScanTick, null, ScanIntervalMs, ScanIntervalMs);
        ev.Set("scan_interval_ms", ScanIntervalMs);
    }

    public void Dispose()
    {
        var t = _scanTimer;
        _scanTimer = null;
        t?.Dispose();
        UnsubscribeConnectMessages();
        UnsubscribeCollection();
        _store.Dispose();
    }

    // ===== Audit Log Integration =====

    internal void SetAuditLogEngine(AuditLogEngine engine)
    {
        engine.OnEntry += entry =>
        {
            try { ProcessAuditEntry(entry); }
            catch (Exception ex) { Logger.Log(Tag, $"ProcessAuditEntry: {ex.Message}"); }
        };
        Logger.Log(Tag, "AuditLogEngine wired");
    }

    private void ProcessAuditEntry(AuditLogEntry entry)
    {
        if (entry.ActionType != AuditLogEngine.ActionMessageDelete) return;
        if (string.IsNullOrEmpty(entry.MessageId)) return;

        using var ev = WideEvent.Begin(Tag, "audit_delete");
        ev.Set("msg_id", entry.MessageId);
        ev.Set("actor_id", entry.ActorId);
        ev.Set("actor_name", entry.ActorName);

        if (_messageCache.TryGetValue(entry.MessageId, out var cached))
        {
            if (!cached.IsDeleted)
            {
                cached.IsDeleted = true;
                cached.DeletedAt = DateTime.UtcNow;
                cached.DeletedBy = entry.ActorId;
                cached.DeletedByName = entry.ActorName;
                _store.RecordDeletion(cached.MessageId, cached.ChannelId,
                    cached.DeletedAt.Value, cached.DeletedBy, cached.DeletedByName);
                ev.Set("result", "marked_deleted");
            }
            else if (string.IsNullOrEmpty(cached.DeletedBy) && !string.IsNullOrEmpty(entry.ActorId))
            {
                cached.DeletedBy = entry.ActorId;
                cached.DeletedByName = entry.ActorName;
                ev.Set("result", "actor_added");
            }
            else
            {
                ev.Set("result", "already_deleted");
            }

            // Refresh card with actor name
            if (!string.IsNullOrEmpty(entry.ActorName))
            {
                var cardTag = $"uprooted-del:{entry.MessageId}";
                if (_injectedCards.TryGetValue(cardTag, out var existingCard))
                {
                    var parent = _r.GetParent(existingCard);
                    if (parent != null) RemoveChild(parent, existingCard);
                    _injectedCards.Remove(cardTag);
                    ev.Set("card_refreshed", true);
                }
            }
        }
        else
        {
            _pendingAuditDeletes[entry.MessageId] = entry;
            ev.Set("result", "pending_queued");
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
                using var ev = WideEvent.BeginSampled(Tag, "scan_tick", _sampler);
                try
                {
                    ev.Set("mode", _usingDynamicData ? "dd" : "coll");
                    ev.Set("cache", _messageCache.Count);
                    ev.Set("snapshots", _contentSnapshot.Count);

                    _channelCheckCounter++;
                    CheckForChannelSwitch();
                    InjectDeletedMessageCards(settings, ev);
                }
                catch (Exception ex) { ev.SetError(ex); }
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

    // ===== DynamicData Subscription =====

    /// <summary>
    /// Walk visual tree to find TextChannelContentViewModel or DirectMessageContentViewModel.
    /// </summary>
    private object? FindContentViewModel()
    {
        try
        {
            foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
            {
                var dc = ReadDC(node);
                if (dc == null) continue;
                var typeName = dc.GetType().Name;
                if (typeName == "TextChannelContentViewModel" ||
                    typeName == "DirectMessageContentViewModel")
                    return dc;
            }
        }
        catch (Exception ex) { Logger.Log(Tag, $"FindContentVM error: {ex.Message}"); }
        return null;
    }

    /// <summary>
    /// Navigate from content ViewModel to IMessageService, call ConnectMessages(), subscribe.
    /// Paths: TextChannelContentVM.Channel.Messages / DirectMessageContentVM.DirectMessageViewModel.DirectMessage.Messages
    /// </summary>
    private bool TrySubscribeConnectMessages(object contentVM)
    {
        try
        {
            var vmType = contentVM.GetType();
            object? messageService = null;

            if (vmType.Name == "TextChannelContentViewModel")
            {
                var channel = vmType.GetProperty("Channel")?.GetValue(contentVM);
                messageService = channel?.GetType().GetProperty("Messages")?.GetValue(channel);
            }
            else if (vmType.Name == "DirectMessageContentViewModel")
            {
                var dmVM = vmType.GetProperty("DirectMessageViewModel")?.GetValue(contentVM);
                var dm = dmVM?.GetType().GetProperty("DirectMessage")?.GetValue(dmVM);
                messageService = dm?.GetType().GetProperty("Messages")?.GetValue(dm);
            }

            if (messageService == null)
            {
                Logger.Log(Tag, $"[dd] Failed to resolve IMessageService from {vmType.Name}");
                return false;
            }

            // Find ConnectMessages method (instance or extension)
            var connectMethod = FindConnectMessagesMethod(messageService);
            if (connectMethod == null)
            {
                Logger.Log(Tag, "[dd] ConnectMessages method not found");
                return false;
            }

            // Invoke to get IObservable<IChangeSet<Message>>
            object? observable = connectMethod.IsStatic
                ? connectMethod.Invoke(null, new[] { messageService })
                : connectMethod.Invoke(messageService, null);

            if (observable == null)
            {
                Logger.Log(Tag, "[dd] ConnectMessages returned null");
                return false;
            }

            // Find IObservable<T> interface on the result
            var iObservableIface = observable.GetType().GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IObservable<>));
            if (iObservableIface == null)
            {
                Logger.Log(Tag, "[dd] Result does not implement IObservable<T>");
                return false;
            }

            var changeSetType = iObservableIface.GetGenericArguments()[0]; // IChangeSet<Message>

            // Resolve Message type properties BEFORE subscribing (avoids race)
            if (changeSetType.IsGenericType)
            {
                var messageType = changeSetType.GetGenericArguments().FirstOrDefault();
                if (messageType != null)
                    TryResolvePropertyAccessors(messageType);
            }

            // Subscribe via generic helper
            var subscribeHelper = typeof(MessageLogger)
                .GetMethod(nameof(SubscribeToObservable), BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(changeSetType);

            _changeSetSub = (IDisposable)subscribeHelper.Invoke(null,
                new object[] { observable, new Action<object>(OnChangeSetReceived) })!;

            Logger.Log(Tag, $"[dd] Subscribed to ConnectMessages via {vmType.Name}");
            return true;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"[dd] Subscribe error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Find ConnectMessages — instance method on the service type, then scan loaded
    /// assemblies for a static extension method.
    /// </summary>
    private static MethodInfo? FindConnectMessagesMethod(object messageService)
    {
        var msType = messageService.GetType();

        // Try instance method
        var method = msType.GetMethod("ConnectMessages", BindingFlags.Public | BindingFlags.Instance);
        if (method != null) return method;

        // Try interfaces
        foreach (var iface in msType.GetInterfaces())
        {
            method = iface.GetMethod("ConnectMessages", BindingFlags.Public | BindingFlags.Instance);
            if (method != null) return method;
        }

        // Scan loaded assemblies for static extension method
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                foreach (var type in asm.GetTypes())
                {
                    if (!type.IsClass || !type.IsAbstract || !type.IsSealed) continue;
                    foreach (var m in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
                    {
                        if (m.Name != "ConnectMessages") continue;
                        var parms = m.GetParameters();
                        if (parms.Length >= 1 && parms[0].ParameterType.IsAssignableFrom(msType))
                            return m;
                    }
                }
            }
            catch { } // Skip assemblies that throw on GetTypes()
        }

        return null;
    }

    // Generic helper to subscribe to IObservable<T> via BCL IObserver<T>
    [Obfuscation(Exclude = false, Feature = "-rename")]
    private static IDisposable SubscribeToObservable<T>(object observable, Action<object> onNext)
    {
        return ((IObservable<T>)observable).Subscribe(new ActionObserver<T>(v => onNext(v!)));
    }

    private class ActionObserver<T> : IObserver<T>
    {
        private readonly Action<T> _onNext;
        internal ActionObserver(Action<T> onNext) => _onNext = onNext;
        public void OnNext(T value) { try { _onNext(value); } catch { } }
        public void OnError(Exception error) { }
        public void OnCompleted() { }
    }

    private void UnsubscribeConnectMessages()
    {
        try { _changeSetSub?.Dispose(); } catch { }
        _changeSetSub = null;
        _currentContentVM = null;
        _usingDynamicData = false;
    }

    // ===== DynamicData Change Handlers =====

    private void OnChangeSetReceived(object changeSet)
    {
        _r.RunOnUIThread(() =>
        {
            try
            {
                if (changeSet is not System.Collections.IEnumerable enumerable) return;

                foreach (var change in enumerable)
                {
                    var changeType = change.GetType();
                    var reason = changeType.GetProperty("Reason")?.GetValue(change);
                    if (reason == null) continue;

                    // Navigate Change<T>.Item.Current to get the Message object
                    var itemChange = changeType.GetProperty("Item")?.GetValue(change);
                    var current = itemChange?.GetType().GetProperty("Current")?.GetValue(itemChange);
                    if (current == null) continue;

                    switch (reason.ToString())
                    {
                        case "Add":
                            HandleMessageAdded(current);
                            break;
                        case "Remove":
                            HandleMessageRemoved(current);
                            break;
                        case "Replace":
                        case "Refresh":
                            HandleMessageRefreshed(current);
                            break;
                    }
                }
            }
            catch (Exception ex) { Logger.Log(Tag, $"[dd] ChangeSet error: {ex.Message}"); }
        });
    }

    private void HandleMessageAdded(object message)
    {
        var msgId = ReadMessageId(message);
        if (msgId == null) return;

        _contentSnapshot[msgId] = ReadContent(message) ?? "";
        CacheMessage(message, msgId);

        // Apply pending audit log deletion
        if (_pendingAuditDeletes.TryGetValue(msgId, out var pendingAudit))
        {
            _pendingAuditDeletes.Remove(msgId);
            if (_messageCache.TryGetValue(msgId, out var cached))
            {
                cached.IsDeleted = true;
                cached.DeletedAt = DateTime.UtcNow;
                cached.DeletedBy = pendingAudit.ActorId;
                cached.DeletedByName = pendingAudit.ActorName;
                _store.RecordDeletion(cached.MessageId, cached.ChannelId,
                    cached.DeletedAt.Value, cached.DeletedBy, cached.DeletedByName);
                Logger.Log(Tag, $"[audit] Applied pending delete on Add: {msgId}");
            }
        }
    }

    /// <summary>
    /// ConnectMessages Remove = genuine deletion. No disambiguation needed.
    /// </summary>
    private void HandleMessageRemoved(object message)
    {
        var msgId = ReadMessageId(message);
        if (msgId == null) return;

        if (_messageCache.TryGetValue(msgId, out var cached))
        {
            if (!cached.IsDeleted)
            {
                using var ev = WideEvent.Begin(Tag, "msg_deleted");
                cached.IsDeleted = true;
                cached.DeletedAt = DateTime.UtcNow;
                _store.RecordDeletion(cached.MessageId, cached.ChannelId, DateTime.UtcNow);
                ev.Set("msg_id", msgId);
                ev.Set("content", Truncate(cached.Content, 80));
                ev.Set("source", "connect_messages");
            }
        }
        else
        {
            // Not in cache — create entry from the message being removed
            var content = ReadContent(message) ?? "";
            if (!string.IsNullOrEmpty(content))
            {
                using var ev = WideEvent.Begin(Tag, "msg_deleted");
                var newCached = new CachedMessage
                {
                    MessageId = msgId,
                    ChannelId = _currentChannelId,
                    AuthorName = "Unknown",
                    Timestamp = DateTime.UtcNow,
                    Content = content,
                    IsDeleted = true,
                    DeletedAt = DateTime.UtcNow
                };
                _messageCache[msgId] = newCached;
                _store.RecordMessage(newCached.MessageId, newCached.ChannelId,
                    "", "Unknown", newCached.Timestamp, content);
                _store.RecordDeletion(msgId, newCached.ChannelId, DateTime.UtcNow);
                ev.Set("msg_id", msgId);
                ev.Set("content", Truncate(content, 80));
                ev.Set("source", "connect_messages_uncached");
            }
        }

        _contentSnapshot.Remove(msgId);
    }

    /// <summary>
    /// Detect edits by comparing content to snapshot. EditedAt non-null confirms genuine edit.
    /// </summary>
    private void HandleMessageRefreshed(object message)
    {
        var settings = UprootedSettings.Load();
        if (!settings.MessageLoggerLogEdits) return;

        var msgId = ReadMessageId(message);
        if (msgId == null) return;

        var newContent = ReadContent(message) ?? "";
        _contentSnapshot.TryGetValue(msgId, out var oldContent);
        _contentSnapshot[msgId] = newContent;

        // Update cache content
        if (_messageCache.TryGetValue(msgId, out var existing))
            existing.Content = newContent;
        else
            CacheMessage(message, msgId);

        if (string.IsNullOrEmpty(oldContent) || oldContent == newContent) return;

        // Confirm via EditedAt — null means content settling, not a genuine edit
        var tp = GetTypeProps(message);
        if (tp?.EditedAt != null)
        {
            var target = GetMessageTarget(message, tp);
            if (target != null)
            {
                try
                {
                    var val = tp.EditedAt.GetValue(target);
                    if (val == null) return; // Not edited — just send-completion settling
                }
                catch { }
            }
        }

        if (_messageCache.TryGetValue(msgId, out var cached))
        {
            using var ev = WideEvent.Begin(Tag, "msg_edited");
            cached.Edits.Add(new MessageEdit { EditTime = DateTime.UtcNow, PreviousContent = oldContent });
            cached.Content = newContent;
            _store.RecordEdit(msgId, cached.ChannelId, DateTime.UtcNow, oldContent);
            ev.Set("msg_id", msgId);
            ev.Set("old", Truncate(oldContent, 40));
            ev.Set("new", Truncate(newContent, 40));
            ev.Set("source", "connect_messages");
        }
    }

    // ===== Fallback: ObservableCollection Subscription =====

    private void FindChatItemsControl()
    {
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
            if (!typeName.Contains("ItemsControl") && typeName != "ListBox") continue;

            var source = ReadItemsSource(node);
            if (source == null) continue;
            var sourceType = source.GetType();
            if (!sourceType.GetInterfaces().Any(i => i.Name == "INotifyCollectionChanged")) continue;

            int count = 0;
            try { count = (int)(sourceType.GetProperty("Count")?.GetValue(source) ?? 0); }
            catch { }

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

            if (isChat && _chatItemsControl == null)
            {
                Logger.Log(Tag, $"Chat control found: {typeName}, {count} items");
                _chatItemsControl = node;
                _currentItemsSource = source;
                break;
            }
        }
    }

    private void ResolvePropsFromCollection(object collection)
    {
        try
        {
            var en = (collection as System.Collections.IEnumerable)?.GetEnumerator();
            if (en != null && en.MoveNext() && en.Current != null)
                TryResolvePropertyAccessors(en.Current.GetType());
        }
        catch { }
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
            Logger.Log(Tag, "Subscribed to CollectionChanged (fallback)");
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
        using var ev = WideEvent.Begin(Tag, "snapshot");
        try
        {
            int count = 0;
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
                count++;
            }
            ev.Set("messages", count);
        }
        catch (Exception ex) { ev.SetError(ex); }
    }

    /// <summary>
    /// Minimal CollectionChanged handler for fallback mode.
    /// Remove events check DeletedAt immediately — no pollers.
    /// </summary>
    private void OnCollectionChanged(object args)
    {
        if (!_propertiesResolved) return;
        try
        {
            var argsType = args.GetType();
            var action = (int)(argsType.GetProperty("Action")?.GetValue(args) ?? -1);

            switch (action)
            {
                case 0: // Add
                {
                    var newItems = argsType.GetProperty("NewItems")?.GetValue(args) as System.Collections.IList;
                    if (newItems == null) break;
                    foreach (var item in newItems)
                    {
                        if (item == null) continue;
                        var msgId = ReadMessageId(item);
                        if (msgId == null) continue;
                        _contentSnapshot[msgId] = ReadContent(item) ?? "";
                        CacheMessage(item, msgId);

                        if (_pendingAuditDeletes.TryGetValue(msgId, out var pendingAudit))
                        {
                            _pendingAuditDeletes.Remove(msgId);
                            if (_messageCache.TryGetValue(msgId, out var cached))
                            {
                                cached.IsDeleted = true;
                                cached.DeletedAt = DateTime.UtcNow;
                                cached.DeletedBy = pendingAudit.ActorId;
                                cached.DeletedByName = pendingAudit.ActorName;
                                _store.RecordDeletion(cached.MessageId, cached.ChannelId,
                                    cached.DeletedAt.Value, cached.DeletedBy, cached.DeletedByName);
                            }
                        }
                    }
                    break;
                }

                case 1: // Remove — check DeletedAt immediately (no pollers)
                {
                    var oldItems = argsType.GetProperty("OldItems")?.GetValue(args) as System.Collections.IList;
                    if (oldItems == null) break;
                    foreach (var item in oldItems)
                    {
                        if (item == null) continue;
                        var msgId = ReadMessageId(item);
                        if (msgId == null) continue;

                        var tp = GetTypeProps(item);
                        var target = tp != null ? GetMessageTarget(item, tp) : null;
                        bool isDeleted = false;
                        if (target != null && tp?.DeletedAt != null)
                        {
                            try
                            {
                                var val = tp.DeletedAt.GetValue(target);
                                isDeleted = val is DateTimeOffset || (val is bool b && b);
                            }
                            catch { }
                        }

                        if (isDeleted && _messageCache.TryGetValue(msgId, out var cached) && !cached.IsDeleted)
                        {
                            using var ev = WideEvent.Begin(Tag, "msg_deleted");
                            cached.IsDeleted = true;
                            cached.DeletedAt = DateTime.UtcNow;
                            _store.RecordDeletion(cached.MessageId, cached.ChannelId, DateTime.UtcNow);
                            ev.Set("msg_id", msgId);
                            ev.Set("source", "collection_fallback");
                        }
                        _contentSnapshot.Remove(msgId);
                    }
                    break;
                }

                case 2: // Replace — edit detection
                {
                    var newReplaceItems = argsType.GetProperty("NewItems")?.GetValue(args) as System.Collections.IList;
                    var oldReplaceItems = argsType.GetProperty("OldItems")?.GetValue(args) as System.Collections.IList;
                    if (newReplaceItems == null) break;

                    var settings = UprootedSettings.Load();
                    for (int i = 0; i < newReplaceItems.Count; i++)
                    {
                        var newItem = newReplaceItems[i];
                        if (newItem == null) continue;
                        var msgId = ReadMessageId(newItem);
                        if (msgId == null) continue;

                        var newContent = ReadContent(newItem) ?? "";
                        _contentSnapshot.TryGetValue(msgId, out var oldContent);
                        if (string.IsNullOrEmpty(oldContent) && oldReplaceItems != null && i < oldReplaceItems.Count)
                        {
                            var oldItem = oldReplaceItems[i];
                            if (oldItem != null) oldContent = ReadContent(oldItem) ?? "";
                        }

                        _contentSnapshot[msgId] = newContent;
                        if (_messageCache.TryGetValue(msgId, out var existingCached))
                            existingCached.Content = newContent;
                        else
                            CacheMessage(newItem, msgId);

                        if (!settings.MessageLoggerLogEdits) continue;
                        if (string.IsNullOrEmpty(oldContent) || oldContent == newContent) continue;

                        bool confirmed = ReadHasBeenEdited(newItem) != false;
                        if (confirmed && _messageCache.TryGetValue(msgId, out var cached))
                        {
                            using var ev = WideEvent.Begin(Tag, "msg_edited");
                            cached.Edits.Add(new MessageEdit { EditTime = DateTime.UtcNow, PreviousContent = oldContent });
                            cached.Content = newContent;
                            _store.RecordEdit(msgId, cached.ChannelId, DateTime.UtcNow, oldContent);
                            ev.Set("msg_id", msgId);
                            ev.Set("source", "collection_fallback");
                        }
                    }
                    break;
                }

                case 4: // Reset
                    Logger.Log(Tag, "Collection Reset — clearing state");
                    _contentSnapshot.Clear();
                    ClearInjectedCards("collection reset");
                    UnsubscribeCollection();
                    _chatItemsControl = null;
                    _currentItemsSource = null;
                    break;
            }
        }
        catch (Exception ex) { Logger.Log(Tag, $"CollectionChanged error: {ex.Message}"); }
    }

    // ===== Channel Switch Detection =====

    private void CheckForChannelSwitch()
    {
        if (_usingDynamicData)
        {
            // Every 15 ticks (~7.5s), check if the content VM changed
            if (_channelCheckCounter % 15 != 0) return;

            var currentVM = FindContentViewModel();
            if (currentVM != null && !ReferenceEquals(currentVM, _currentContentVM))
            {
                Logger.Log(Tag, "[dd] Channel switch detected — resubscribing");
                UnsubscribeConnectMessages();
                _contentSnapshot.Clear();
                ClearInjectedCards("channel switch");

                if (TrySubscribeConnectMessages(currentVM))
                {
                    _currentContentVM = currentVM;
                    _usingDynamicData = true;
                }
                ReadCurrentChannelId();
                // Re-find chat control for visual injection
                _chatItemsControl = null;
                FindChatItemsControl();
            }
        }
        else
        {
            // Periodically try to upgrade from fallback to DynamicData
            if (_channelCheckCounter % 30 == 0)
            {
                var vm = FindContentViewModel();
                if (vm != null && TrySubscribeConnectMessages(vm))
                {
                    UnsubscribeCollection();
                    _currentContentVM = vm;
                    _usingDynamicData = true;
                    Logger.Log(Tag, "[dd] Upgraded from fallback to DynamicData");
                    ReadCurrentChannelId();
                    return;
                }
            }

            // Fallback channel switch: check ItemsSource ref
            if (_chatItemsControl == null)
            {
                FindChatItemsControl();
                if (_chatItemsControl != null)
                {
                    _currentItemsSource = ReadItemsSource(_chatItemsControl);
                    if (_currentItemsSource != null && !_subscribed)
                    {
                        ResolvePropsFromCollection(_currentItemsSource);
                        SubscribeToCollection(_currentItemsSource);
                        SnapshotMessages(_currentItemsSource);
                    }
                }
                ReadCurrentChannelId();
                return;
            }

            var currentSource = ReadItemsSource(_chatItemsControl);
            if (currentSource == null)
            {
                _subscribed = false;
                _chatItemsControl = null;
                _currentItemsSource = null;
                return;
            }

            if (!ReferenceEquals(currentSource, _currentItemsSource))
            {
                UnsubscribeCollection();
                _currentItemsSource = currentSource;
                _contentSnapshot.Clear();
                ClearInjectedCards("ItemsSource changed");
                ReadCurrentChannelId();
                ResolvePropsFromCollection(currentSource);
                SubscribeToCollection(currentSource);
                SnapshotMessages(currentSource);
            }
        }
    }

    private void ReadCurrentChannelId()
    {
        // Try reading from content VM (most reliable)
        if (_currentContentVM != null)
        {
            try
            {
                var vmType = _currentContentVM.GetType();
                var channelProp = vmType.GetProperty("Channel");
                if (channelProp != null)
                {
                    var channel = channelProp.GetValue(_currentContentVM);
                    var id = channel?.GetType().GetProperty("Id")?.GetValue(channel)?.ToString();
                    if (!string.IsNullOrEmpty(id)) { _currentChannelId = id!; return; }
                }
                var dmVmProp = vmType.GetProperty("DirectMessageViewModel");
                if (dmVmProp != null)
                {
                    var dmVM = dmVmProp.GetValue(_currentContentVM);
                    var dm = dmVM?.GetType().GetProperty("DirectMessage")?.GetValue(dmVM);
                    var id = dm?.GetType().GetProperty("Id")?.GetValue(dm)?.ToString();
                    if (!string.IsNullOrEmpty(id)) { _currentChannelId = id!; return; }
                }
            }
            catch { }
        }

        // Fallback: walk up from chat control
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
                    if (!string.IsNullOrEmpty(val)) { _currentChannelId = val!; return; }
                }
            }
            current = _r.GetParent(current);
        }
    }

    // ===== Visual Injection — Deleted Message Cards =====

    private void InjectDeletedMessageCards(UprootedSettings settings, WideEvent? scanEv = null)
    {
        if (!settings.MessageLoggerLogDeletes) return;

        if (_chatItemsControl == null)
        {
            FindChatItemsControl();
            if (_chatItemsControl == null) return;
        }

        object? vsp = FindMessagePanel();
        if (vsp == null) return;

        // Build list of visible messages with their containers
        var visibleMessages = new List<(string msgId, object container)>();
        int childCount = _r.GetChildCount(vsp);
        for (int i = 0; i < childCount; i++)
        {
            var child = _r.GetChild(vsp, i);
            if (child == null) continue;
            var dc = ReadDC(child);
            var msgId = dc != null ? ReadMessageId(dc) : null;
            if (dc == null || msgId == null) continue;
            if (!_messageCache.ContainsKey(msgId)) continue;
            visibleMessages.Add((msgId, child));
        }

        if (visibleMessages.Count == 0) return;

        int deletedInChannel = 0;
        foreach (var c in _messageCache.Values)
            if (c.IsDeleted && c.ChannelId == _currentChannelId) deletedInChannel++;

        int injected = 0, skipped = 0;

        foreach (var cached in _messageCache.Values)
        {
            if (!cached.IsDeleted || cached.ChannelId != _currentChannelId) continue;
            if (cached.DeletedAt == null) continue;
            if ((DateTime.UtcNow - cached.DeletedAt.Value).TotalHours > 24) continue;

            var cardTag = $"uprooted-del:{cached.MessageId}";

            // Timestamp-based positioning: find the visible message just before the deleted one
            object? targetContainer = null;
            for (int i = visibleMessages.Count - 1; i >= 0; i--)
            {
                if (_messageCache.TryGetValue(visibleMessages[i].msgId, out var visCached) &&
                    visCached.Timestamp <= cached.Timestamp)
                { targetContainer = visibleMessages[i].container; break; }
            }
            if (targetContainer == null)
                targetContainer = visibleMessages[0].container;
            if (targetContainer == null) { skipped++; continue; }

            var (grid, col) = FindMessageGridInContainer(targetContainer);
            if (grid == null) { skipped++; continue; }

            // Dedup: check if card already exists
            bool exists = false;
            var gridChildren = _r.GetChildren(grid);
            if (gridChildren != null)
                foreach (var c in gridChildren)
                    if (c != null && _r.GetTag(c) == cardTag) { exists = true; break; }
            if (exists) { skipped++; continue; }

            var card = BuildDeletedMessageCard(cached, cardTag);
            if (card == null) { skipped++; continue; }

            var (newRow, rowDef) = AddAutoRowToGrid(grid);
            if (newRow < 0) { skipped++; continue; }

            _r.SetGridRow(card, newRow);
            _r.SetGridColumn(card, col);
            SetGridColumnSpan(card, 99);

            _r.AddChild(grid, card);
            _injectedCards[cardTag] = card;
            SubscribeContainerDataContextChanged(cardTag, targetContainer, grid, rowDef!);
            injected++;
        }

        if (injected > 0 || skipped > 0)
        {
            scanEv?.Set("del_injected", injected);
            scanEv?.Set("del_skipped", skipped);
            scanEv?.Set("del_in_channel", deletedInChannel);
            if (injected > 0) scanEv?.MarkNotable();
        }
    }

    /// <summary>
    /// Walk into a message container to find the content Grid.
    /// Tries RootMarkdownTextBlock parent, then RootLinkButton parent.
    /// </summary>
    private (object? grid, int column) FindMessageGridInContainer(object container)
    {
        foreach (var node in DescendantsHybrid(container))
        {
            var typeName = node.GetType().Name;

            if (typeName == "RootMarkdownTextBlock")
            {
                var grid = _r.GetParent(node);
                if (grid != null && _r.IsGrid(grid))
                    return (grid, 0);
                if (grid != null)
                {
                    var above = _r.GetParent(grid);
                    if (above != null && _r.IsGrid(above))
                        return (above, 0);
                }
            }

            if (typeName == "RootLinkButton")
            {
                var grid = _r.GetParent(node);
                if (grid != null && _r.IsGrid(grid))
                    return (grid, 0);
            }
        }

        return (null, 0);
    }

    /// <summary>
    /// Depth-first traversal handling Avalonia 11's UserControl visual-tree boundary.
    /// Falls back to Content/Children/Border.Child when visual children are empty.
    /// </summary>
    private IEnumerable<object> DescendantsHybrid(object root)
    {
        var stack = new Stack<object>();
        var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
        stack.Push(root);

        while (stack.Count > 0)
        {
            var node = stack.Pop();
            if (!visited.Add(node)) continue;
            yield return node;

            bool hadVisualKids = false;
            foreach (var kid in _r.GetVisualChildren(node))
            { stack.Push(kid); hadVisualKids = true; }

            if (!hadVisualKids)
            {
                var panelKids = _r.GetChildren(node);
                if (panelKids != null && panelKids.Count > 0)
                {
                    for (int i = panelKids.Count - 1; i >= 0; i--)
                        if (panelKids[i] != null) stack.Push(panelKids[i]!);
                }
                else
                {
                    try
                    {
                        var cp = node.GetType()
                            .GetProperty("Content", BindingFlags.Public | BindingFlags.Instance);
                        if (cp != null)
                        {
                            var c = cp.GetValue(node);
                            if (c != null && c.GetType().IsClass && c is not string)
                                stack.Push(c);
                        }
                    }
                    catch { }

                    var borderChild = _r.GetBorderChild(node);
                    if (borderChild != null) stack.Push(borderChild);
                }
            }
        }
    }

    private object? FindMessagePanel()
    {
        if (_chatItemsControl == null) return null;
        foreach (var node in _walker.DescendantsDepthFirst(_chatItemsControl))
        {
            var name = node.GetType().Name;
            if (name.Contains("VirtualizingStackPanel") || name.Contains("StackPanel"))
            {
                if (_r.GetChildCount(node) >= 1) return node;
            }
        }
        return null;
    }

    // ===== Card Building =====

    private object? BuildDeletedMessageCard(CachedMessage cached, string tag)
    {
        try
        {
            var body = _r.CreateTextBlock(cached.Content, 13, "#DDDDDD");
            if (body == null) return null;
            _r.SetTextWrapping(body, "Wrap");

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

            var card = _r.CreateBorder("#28FF4444", cornerRadius: 0, child: cardContent);
            if (card == null) return null;

            var leftAccent = _r.CreateBorder("#60FF4444", cornerRadius: 0, child: null);
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
                _r.SetPadding(card, 8, 4, 8, 4);
                AttachContextMenu(wrapper, cached);
                return wrapper;
            }

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

    private void AttachContextMenu(object card, CachedMessage cached)
    {
        try
        {
            ResolveContextMenuTypes();
            if (_contextMenuType == null || _menuItemType == null) return;

            var menuItem = Activator.CreateInstance(_menuItemType);
            if (menuItem == null) return;
            _menuItemType.GetProperty("Header")?.SetValue(menuItem, "Clear message history");

            var clickEvent = _menuItemType.GetEvent("Click");
            if (clickEvent != null)
            {
                var capturedId = cached.MessageId;
                var capturedCard = card;
                var callback = new Action(() =>
                {
                    try
                    {
                        _messageCache.Remove(capturedId);
                        _store.RecordClear(capturedId);
                        var parent = _r.GetParent(capturedCard);
                        if (parent != null) RemoveChild(parent, capturedCard);
                        _injectedCards.Remove($"uprooted-del:{capturedId}");
                        Logger.Log(Tag, $"Cleared history: {capturedId}");
                    }
                    catch (Exception ex) { Logger.Log(Tag, $"Clear error: {ex.Message}"); }
                });

                var handlerType = clickEvent.EventHandlerType!;
                var paramTypes = handlerType.GetMethod("Invoke")!.GetParameters()
                    .Select(p => p.ParameterType).ToArray();
                var sParam = Expression.Parameter(paramTypes[0], "s");
                var eParam = Expression.Parameter(paramTypes[1], "e");
                var invokeBody = Expression.Invoke(Expression.Constant(callback));
                clickEvent.AddEventHandler(menuItem,
                    Expression.Lambda(handlerType, invokeBody, sParam, eParam).Compile());
            }

            var contextMenu = Activator.CreateInstance(_contextMenuType);
            if (contextMenu == null) return;
            var itemsProp = _contextMenuType.GetProperty("Items");
            var itemsObj = itemsProp?.GetValue(contextMenu);
            itemsObj?.GetType().GetMethod("Add", new[] { typeof(object) })
                ?.Invoke(itemsObj, new[] { menuItem });
            card.GetType().GetProperty("ContextMenu")?.SetValue(card, contextMenu);
        }
        catch (Exception ex) { Logger.Log(Tag, $"ContextMenu error: {ex.Message}"); }
    }

    private void ResolveContextMenuTypes()
    {
        if (_contextMenuTypesResolved) return;
        _contextMenuTypesResolved = true;
        var controlsAsm = _r.ControlType?.Assembly;
        if (controlsAsm == null) return;
        _contextMenuType = controlsAsm.GetType("Avalonia.Controls.ContextMenu");
        _menuItemType = controlsAsm.GetType("Avalonia.Controls.MenuItem");
    }

    // ===== Grid Helpers =====

    private (int row, object? rowDef) AddAutoRowToGrid(object grid)
    {
        try
        {
            var rowDefs = grid.GetType().GetProperty("RowDefinitions")?.GetValue(grid);
            if (rowDefs == null) return (-1, null);

            int count = (int)(rowDefs.GetType().GetProperty("Count")?.GetValue(rowDefs) ?? 0);

            if (_r.GridLengthType != null && _r.GridUnitTypeEnum != null)
            {
                var autoUnit = Enum.Parse(_r.GridUnitTypeEnum, "Auto");
                var autoLength = Activator.CreateInstance(_r.GridLengthType, 1.0, autoUnit);
                var rowDefType = _r.GridType?.Assembly.GetType("Avalonia.Controls.RowDefinition");
                if (rowDefType != null)
                {
                    var rowDef = Activator.CreateInstance(rowDefType);
                    rowDefType.GetProperty("Height")?.SetValue(rowDef, autoLength);
                    rowDefs.GetType().GetMethod("Add")?.Invoke(rowDefs, new[] { rowDef });
                    return (count, rowDef);
                }
            }
        }
        catch (Exception ex) { Logger.Log(Tag, $"AddAutoRowToGrid error: {ex.Message}"); }
        return (-1, null);
    }

    private void SetGridColumnSpan(object control, int span)
    {
        if (_r.GridType == null) return;
        _r.GridType.GetMethod("SetColumnSpan", BindingFlags.Public | BindingFlags.Static)
            ?.Invoke(null, new object[] { control, span });
    }

    // ===== Recycling Cleanup =====

    private void SubscribeContainerDataContextChanged(string cardTag, object container, object grid, object rowDef)
    {
        try
        {
            var dcEvent = container.GetType().GetEvent("DataContextChanged");
            if (dcEvent == null)
            {
                _injectedRowDefs[cardTag] = (grid, rowDef, null, null);
                return;
            }

            var handlerType = dcEvent.EventHandlerType!;
            var paramTypes = handlerType.GetMethod("Invoke")!.GetParameters()
                .Select(p => p.ParameterType).ToArray();

            var capturedTag = cardTag;
            var callback = new Action(() =>
            {
                try { _r.RunOnUIThread(() => CleanupInjectedRow(capturedTag)); }
                catch { }
            });

            var sParam = Expression.Parameter(paramTypes[0], "s");
            var eParam = paramTypes.Length > 1
                ? Expression.Parameter(paramTypes[1], "e")
                : Expression.Parameter(typeof(object), "e");
            var invokeBody = Expression.Invoke(Expression.Constant(callback));
            var lambda = paramTypes.Length > 1
                ? Expression.Lambda(handlerType, invokeBody, sParam, eParam)
                : Expression.Lambda(handlerType, invokeBody, sParam);
            var handler = lambda.Compile();

            dcEvent.AddEventHandler(container, handler);
            _injectedRowDefs[cardTag] = (grid, rowDef, container, handler);
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"[recycle-sub] {cardTag}: {ex.Message}");
            _injectedRowDefs[cardTag] = (grid, rowDef, null, null);
        }
    }

    private void CleanupInjectedRow(string cardTag)
    {
        if (!_injectedRowDefs.TryGetValue(cardTag, out var entry)) return;
        _injectedRowDefs.Remove(cardTag);

        if (_injectedCards.TryGetValue(cardTag, out var card))
        {
            _r.GetChildren(entry.grid)?.Remove(card);
            _injectedCards.Remove(cardTag);
        }

        RemoveRowDefinition(entry.grid, entry.rowDef);

        if (entry.dcHandler != null && entry.container != null)
        {
            try
            {
                entry.container.GetType().GetEvent("DataContextChanged")
                    ?.RemoveEventHandler(entry.container, entry.dcHandler);
            }
            catch { }
        }
    }

    private static void RemoveRowDefinition(object grid, object rowDef)
    {
        try
        {
            var rowDefs = grid.GetType().GetProperty("RowDefinitions")?.GetValue(grid);
            rowDefs?.GetType().GetMethod("Remove")?.Invoke(rowDefs, new[] { rowDef });
        }
        catch { }
    }

    private void ClearInjectedCards(string reason = "unknown")
    {
        int total = _injectedCards.Count;
        if (total > 0)
            Logger.Log(Tag, $"Clearing {total} cards: {reason}");

        var tags = _injectedCards.Keys.ToList();
        foreach (var tag in tags)
            CleanupInjectedRow(tag);

        // Safety net for orphaned RowDef-only entries
        foreach (var (_, entry) in _injectedRowDefs)
        {
            RemoveRowDefinition(entry.grid, entry.rowDef);
            if (entry.dcHandler != null && entry.container != null)
            {
                try { entry.container.GetType().GetEvent("DataContextChanged")
                          ?.RemoveEventHandler(entry.container, entry.dcHandler); }
                catch { }
            }
        }
        _injectedRowDefs.Clear();
        _injectedCards.Clear();
    }

    // ===== Property Resolution =====

    private TypeProps? TryResolvePropertyAccessors(Type type)
    {
        if (_typePropsCache.TryGetValue(type, out var cached)) return cached;

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var tp = new TypeProps();

        // Hard-coded paths: try known property names first
        tp.MessageId = FindProp(props, "MessageId", "Id");
        tp.Content = FindProp(props, "MessageContent", "Content");
        tp.DeletedAt = FindProp(props, "DeletedAt");
        tp.EditedAt = FindProp(props, "EditedAt");

        if (tp.MessageId != null && tp.Content != null)
        {
            tp.AuthorId = FindProp(props, "SenderUserId", "AuthorId");
            tp.Timestamp = FindProp(props, "SentAtUtc", "SentAt", "Timestamp");
            tp.SenderMember = FindProp(props, "SenderMember");
            Logger.Log(Tag, $"Props on {type.Name}: Id={tp.MessageId.Name}, Content={tp.Content.Name}");
            _typePropsCache[type] = tp;
            _propertiesResolved = true;
            return tp;
        }

        // Try nested properties (ViewModel.Message bridge)
        foreach (var prop in props)
        {
            if (!prop.PropertyType.IsClass || prop.PropertyType == typeof(string) ||
                prop.PropertyType.IsArray || prop.PropertyType.Namespace == "System") continue;

            var nested = prop.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var nestedId = FindProp(nested, "MessageId", "Id");
            var nestedContent = FindProp(nested, "MessageContent", "Content");

            if (nestedId != null && nestedContent != null)
            {
                tp.Bridge = prop;
                tp.MessageId = nestedId;
                tp.Content = nestedContent;
                tp.DeletedAt = FindProp(nested, "DeletedAt") ?? tp.DeletedAt;
                tp.EditedAt = FindProp(nested, "EditedAt") ?? tp.EditedAt;
                tp.AuthorId = FindProp(nested, "SenderUserId", "AuthorId");
                tp.Timestamp = FindProp(nested, "SentAtUtc", "SentAt", "Timestamp");
                tp.SenderMember = FindProp(nested, "SenderMember");
                Logger.Log(Tag, $"Props on {type.Name} via .{prop.Name}: Id={nestedId.Name}, Content={nestedContent.Name}");
                _typePropsCache[type] = tp;
                _propertiesResolved = true;
                return tp;
            }
        }

        return null;
    }

    private TypeProps? GetTypeProps(object item)
    {
        var type = item.GetType();
        if (_typePropsCache.TryGetValue(type, out var cached)) return cached;
        return TryResolvePropertyAccessors(type);
    }

    private static PropertyInfo? FindProp(PropertyInfo[] props, params string[] names)
    {
        foreach (var n in names)
            foreach (var p in props)
                if (p.Name.Equals(n, StringComparison.OrdinalIgnoreCase)) return p;
        return null;
    }

    // ===== Message Helpers =====

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
            if (val is bool b) return b;
            if (val is DateTimeOffset) return true;
            if (tp.EditedAt.PropertyType == typeof(DateTimeOffset?) && val == null) return false;
            return null;
        }
        catch { return null; }
    }

    private static string? ReadAuthorName(object messageTarget, TypeProps tp)
    {
        if (tp.SenderMember == null) return null;
        try
        {
            var sender = tp.SenderMember.GetValue(messageTarget);
            if (sender == null) return null;
            var globalUser = sender.GetType().GetProperty("GlobalUser",
                BindingFlags.Public | BindingFlags.Instance)?.GetValue(sender);
            if (globalUser == null) return null;

            var userNameProp = globalUser.GetType().GetProperty("UserName",
                BindingFlags.Public | BindingFlags.Instance)
                ?? globalUser.GetType().GetProperty("DisplayName",
                    BindingFlags.Public | BindingFlags.Instance)
                ?? globalUser.GetType().GetProperty("Name",
                    BindingFlags.Public | BindingFlags.Instance);

            var name = userNameProp?.GetValue(globalUser) as string;
            if (!string.IsNullOrEmpty(name)) return name;

            var str = globalUser.ToString();
            if (!string.IsNullOrEmpty(str) && str != globalUser.GetType().FullName) return str;
            return null;
        }
        catch { return null; }
    }

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
