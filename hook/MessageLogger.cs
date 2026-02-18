using System.Linq.Expressions;
using System.Reflection;

namespace Uprooted;

/// <summary>
/// Message logger plugin — logs deleted and edited messages, shows visual indicators.
///
/// Phase 1: Discovery scan — explores chat visual tree and data model, logs to hook log.
/// Phase 2: Collection subscription — watches ObservableCollection for add/remove/replace events.
/// Phase 3: Visual indicators — injects red cards for deletions, "(edited)" for edits.
///
/// Architecture: Root's chat is Avalonia-native MVVM. Messages are held in an observable
/// collection on a ViewModel, bound to an ItemsControl. We find the collection via the
/// visual tree, subscribe to CollectionChanged, and read DataContext for message properties.
///
/// Follows the LinkEmbedEngine pattern: timer + Interlocked guard, tag-based dedup,
/// AvaloniaReflection for all control creation, Expression.Lambda for event subscription.
/// </summary>
internal class MessageLogger
{
    private const string Tag = "MsgLogger";
    private const int ScanIntervalMs = 1000;
    private const int VisualIndicatorDelayMs = 2000; // Delay before injecting indicators
    private const int BatchRemoveThreshold = 20; // More than this = channel switch, not deletions

    private readonly AvaloniaReflection _r;
    private readonly VisualTreeWalker _walker;
    private readonly object _mainWindow;
    private readonly MessageStore _store;

    private Timer? _scanTimer;
    private int _scanning; // Interlocked reentrancy guard

    // Discovery results — populated by Phase 1 scan
    private object? _chatItemsControl;
    private object? _currentItemsSource;
    private Type? _messageItemType;

    // Property accessors discovered in Phase 1
    // Properties may live on the ViewModel directly OR on a nested Message object.
    // _propMessageObject is the bridge: ViewModel.Message -> nested object.
    private PropertyInfo? _propMessageObject; // null if props are directly on ViewModel
    private PropertyInfo? _propMessageId;
    private PropertyInfo? _propContent;
    private PropertyInfo? _propAuthorName;
    private PropertyInfo? _propAuthorId;
    private PropertyInfo? _propTimestamp;
    private PropertyInfo? _propChannelId;
    private PropertyInfo? _propHasBeenDeleted; // ViewModel-level bool
    private PropertyInfo? _propHasBeenEdited;  // ViewModel-level bool
    private bool _propertiesResolved;

    // Collection subscription
    private Delegate? _collectionChangedHandler;
    private EventInfo? _collectionChangedEvent;
    private bool _subscribed;

    // In-memory cache
    private readonly Dictionary<string, CachedMessage> _messageCache = new();

    // Snapshot for tracking current collection state (message ID → content)
    private readonly Dictionary<string, string> _knownMessages = new();

    // Pending remove batch for channel switch detection
    private readonly List<string> _pendingRemoves = new();

    // Visual indicator tracking
    private readonly List<object> _injectedIndicators = new();

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

        // Load existing message log from disk
        _store.LoadAll(_messageCache);
        Logger.Log(Tag, $"Loaded {_messageCache.Count} cached messages from store");

        // Truncate if too large
        var settings = UprootedSettings.Load();
        int maxMessages = settings.MessageLoggerMaxMessages;
        if (_messageCache.Count > maxMessages)
        {
            _store.Truncate(maxMessages);
            Logger.Log(Tag, $"Truncated store to {maxMessages}");
        }

        // Run Phase 1 discovery on UI thread
        _r.RunOnUIThread(() =>
        {
            try
            {
                RunDiscoveryScan();
            }
            catch (Exception ex)
            {
                Logger.LogException(Tag, "Discovery scan error", ex);
            }
        });

        // Start polling timer (handles collection re-subscription + visual indicators)
        _scanTimer = new Timer(OnScanTick, null, VisualIndicatorDelayMs, ScanIntervalMs);
        Logger.Log(Tag, $"Scan timer started ({ScanIntervalMs}ms interval)");
    }

    // ===== Timer callback =====

    private void OnScanTick(object? state)
    {
        if (Interlocked.CompareExchange(ref _scanning, 1, 0) != 0) return;
        try
        {
            // Check settings — stop if plugin disabled
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
                    // Re-discover collection if needed (user navigated to different room)
                    EnsureCollectionSubscription();

                    // Phase 3: Visual indicators
                    if (_propertiesResolved)
                        InjectVisualIndicators(settings);
                }
                catch (Exception ex)
                {
                    Logger.Log(Tag, $"Scan error: {ex.Message}");
                }
                finally
                {
                    Interlocked.Exchange(ref _scanning, 0);
                }
            });

            // Fallback release in case UI thread doesn't run
            Task.Delay(ScanIntervalMs * 2).ContinueWith(_ =>
            {
                Interlocked.CompareExchange(ref _scanning, 0, 1);
            });
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"OnScanTick error: {ex.Message}");
            Interlocked.Exchange(ref _scanning, 0);
        }
    }

    // ===== Phase 1: Discovery Scan =====

    private void RunDiscoveryScan()
    {
        Logger.Log(Tag, "========================================");
        Logger.Log(Tag, "=== Phase 1: Data Model Discovery ===");
        Logger.Log(Tag, "========================================");

        // Exclude settings subtree from search
        var settingsText = _walker.FindFirstTextBlock(_mainWindow, "APP SETTINGS");
        object? settingsSubtree = null;
        if (settingsText != null)
        {
            var current = _r.GetParent(settingsText);
            for (int i = 0; i < 20 && current != null; i++)
            {
                if (_r.IsGrid(current) && _r.GetChildCount(current) >= 3)
                {
                    settingsSubtree = current;
                    break;
                }
                current = _r.GetParent(current);
            }
        }

        // Find all ItemsControl-like containers outside settings
        var itemsControls = new List<object>();
        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            if (settingsSubtree != null && IsDescendantOf(node, settingsSubtree)) continue;

            var typeName = node.GetType().Name;
            if (typeName.Contains("ItemsControl") || typeName == "ListBox" || typeName == "ItemsRepeater")
                itemsControls.Add(node);
        }

        Logger.Log(Tag, $"Found {itemsControls.Count} ItemsControl-like containers outside settings");

        foreach (var ic in itemsControls)
        {
            InspectItemsControl(ic);
        }

        // Walk up from CTextBlock to find message ViewModel
        InspectFromCTextBlock(settingsSubtree);

        Logger.Log(Tag, $"Discovery complete: propertiesResolved={_propertiesResolved}, " +
            $"chatItemsControl={_chatItemsControl != null}, " +
            $"itemsSource={_currentItemsSource != null}");

        Logger.Log(Tag, "========================================");
        Logger.Log(Tag, "=== Phase 1 Discovery Complete ===");
        Logger.Log(Tag, "========================================");
    }

    private void InspectItemsControl(object itemsControl)
    {
        var typeName = itemsControl.GetType().Name;
        Logger.Log(Tag, $"--- Inspecting {typeName} ---");

        // Read ItemsSource
        object? itemsSource = null;
        try
        {
            itemsSource = itemsControl.GetType()
                .GetProperty("ItemsSource", BindingFlags.Public | BindingFlags.Instance)
                ?.GetValue(itemsControl);
        }
        catch { }

        // Fallback: try Items property
        if (itemsSource == null)
        {
            try
            {
                itemsSource = itemsControl.GetType()
                    .GetProperty("Items", BindingFlags.Public | BindingFlags.Instance)
                    ?.GetValue(itemsControl);
            }
            catch { }
        }

        if (itemsSource == null)
        {
            Logger.Log(Tag, "  ItemsSource: null");
            return;
        }

        var sourceType = itemsSource.GetType();
        Logger.Log(Tag, $"  ItemsSource type: {sourceType.FullName}");

        // Check INotifyCollectionChanged
        bool implementsINCC = false;
        foreach (var iface in sourceType.GetInterfaces())
        {
            if (iface.Name == "INotifyCollectionChanged")
            {
                implementsINCC = true;
                break;
            }
        }
        Logger.Log(Tag, $"  INotifyCollectionChanged: {implementsINCC}");

        // Count items
        int itemCount = 0;
        try
        {
            var countProp = sourceType.GetProperty("Count");
            if (countProp != null)
                itemCount = (int)countProp.GetValue(itemsSource)!;
        }
        catch { }
        Logger.Log(Tag, $"  Item count: {itemCount}");

        // Inspect first few items
        if (itemCount > 0)
        {
            try
            {
                var enumerator = (itemsSource as System.Collections.IEnumerable)?.GetEnumerator();
                if (enumerator != null)
                {
                    int inspected = 0;
                    while (enumerator.MoveNext() && inspected < 3)
                    {
                        var item = enumerator.Current;
                        if (item == null) continue;

                        Logger.Log(Tag, $"  Item[{inspected}] type: {item.GetType().FullName}");
                        InspectDataObject(item, "    ");

                        // Try to resolve property accessors from this item type
                        if (!_propertiesResolved)
                            TryResolvePropertyAccessors(item.GetType());

                        inspected++;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"  Enumeration error: {ex.Message}");
            }
        }

        // Mark as candidate chat collection if it's the actual message list.
        // Identify by control type name (RootMessageItemsControl) or by items being MessageViewModels.
        bool isChatControl = typeName.Contains("Message", StringComparison.OrdinalIgnoreCase);
        if (!isChatControl && itemCount > 0)
        {
            try
            {
                var enumerator2 = (itemsSource as System.Collections.IEnumerable)?.GetEnumerator();
                if (enumerator2 != null && enumerator2.MoveNext() && enumerator2.Current != null)
                    isChatControl = enumerator2.Current.GetType().Name.Contains("MessageViewModel");
            }
            catch { }
        }

        if (isChatControl && implementsINCC && _chatItemsControl == null)
        {
            Logger.Log(Tag, $"  *** Chat messages collection found: {typeName}, {itemCount} items ***");
            _chatItemsControl = itemsControl;
            _currentItemsSource = itemsSource;
        }
    }

    private void InspectDataObject(object obj, string indent)
    {
        var type = obj.GetType();
        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            string valuePreview;
            try
            {
                var value = prop.GetValue(obj);
                if (value == null)
                    valuePreview = "null";
                else if (value is string s)
                    valuePreview = $"\"{(s.Length > 80 ? s[..77] + "..." : s)}\"";
                else if (value is DateTime dt)
                    valuePreview = dt.ToString("O");
                else if (value is bool b)
                    valuePreview = b.ToString();
                else if (value.GetType().IsPrimitive)
                    valuePreview = value.ToString() ?? "?";
                else
                    valuePreview = $"[{value.GetType().Name}]";
            }
            catch
            {
                valuePreview = "[error]";
            }

            Logger.Log(Tag, $"{indent}{prop.Name} ({prop.PropertyType.Name}): {valuePreview}");
        }
    }

    private void InspectFromCTextBlock(object? settingsSubtree)
    {
        Logger.Log(Tag, "--- Inspecting DataContext via CTextBlock walk-up ---");

        // Find a CTextBlock in the chat area (not in settings)
        object? ctextBlock = null;
        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            if (settingsSubtree != null && IsDescendantOf(node, settingsSubtree)) continue;
            if (node.GetType().Name == "CTextBlock")
            {
                ctextBlock = node;
                break;
            }
        }

        if (ctextBlock == null)
        {
            Logger.Log(Tag, "  No CTextBlock found");
            return;
        }

        // Walk up the visual tree, logging DataContext at each level
        var current = ctextBlock;
        for (int i = 0; i < 15 && current != null; i++)
        {
            var curTypeName = current.GetType().Name;

            // Read DataContext
            object? dc = null;
            try
            {
                dc = current.GetType()
                    .GetProperty("DataContext", BindingFlags.Public | BindingFlags.Instance)
                    ?.GetValue(current);
            }
            catch { }

            var dcInfo = dc != null ? $"DC={dc.GetType().Name}" : "DC=null";
            Logger.Log(Tag, $"  [{i}] {curTypeName} ({dcInfo})");

            // If DataContext looks like a message ViewModel, inspect it fully
            if (dc != null)
            {
                var dcTypeName = dc.GetType().Name;
                if (dcTypeName.Contains("Message", StringComparison.OrdinalIgnoreCase) ||
                    dcTypeName.Contains("Chat", StringComparison.OrdinalIgnoreCase))
                {
                    Logger.Log(Tag, $"  *** Interesting DataContext: {dc.GetType().FullName} ***");
                    InspectDataObject(dc, "      ");

                    if (!_propertiesResolved)
                        TryResolvePropertyAccessors(dc.GetType());
                }
            }

            current = _r.GetParent(current);
        }
    }

    // ===== Property Resolution =====

    /// <summary>
    /// Try to resolve property accessors from a message item type by searching for
    /// common property name patterns. Caches results on first success.
    /// </summary>
    private void TryResolvePropertyAccessors(Type type)
    {
        _messageItemType = type;
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // ViewModel-level booleans (always on the ViewModel itself)
        _propHasBeenDeleted = FindProp(props, "HasBeenDeleted");
        _propHasBeenEdited = FindProp(props, "HasBeenEdited");

        // Try direct resolution first
        if (TryResolveOnProps(props, type.Name, null))
            return;

        // Direct resolution failed — try nested Message object pattern
        // Root's ViewModel has a .Message property containing the data model
        Logger.Log(Tag, $"Direct resolution failed on {type.Name}, trying nested objects...");
        foreach (var prop in props)
        {
            if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string) &&
                !prop.PropertyType.IsArray && prop.PropertyType.Namespace != "System")
            {
                var nestedProps = prop.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if (TryResolveOnProps(nestedProps, prop.PropertyType.Name, prop))
                {
                    Logger.Log(Tag, $"Resolved via nested property: {type.Name}.{prop.Name}");
                    return;
                }
            }
        }

        Logger.Log(Tag, $"Property resolution FAILED on {type.Name}");
    }

    /// <summary>
    /// Attempt to resolve message ID + content from a set of properties.
    /// If bridge is non-null, properties live on a nested object (ViewModel.bridge -> nested).
    /// </summary>
    private bool TryResolveOnProps(PropertyInfo[] props, string typeName, PropertyInfo? bridge)
    {
        var id = FindProp(props, "MessageId", "Id", "Uuid", "Nonce", "ID");
        var content = FindProp(props, "MessageContent", "Content", "Text", "Body", "RawContent");

        if (id == null || content == null)
            return false;

        _propMessageObject = bridge;
        _propMessageId = id;
        _propContent = content;
        _propAuthorName = FindProp(props, "AuthorName", "Author", "SenderName", "Sender",
            "DisplayName", "Username", "UserName", "Name");
        _propAuthorId = FindProp(props, "SenderUserId", "AuthorId", "SenderId", "UserId",
            "AuthorID", "SenderID", "UserID");
        _propTimestamp = FindProp(props, "SentAtUtc", "SentAt", "Timestamp", "CreatedAt",
            "Date", "Time", "Created");
        _propChannelId = FindProp(props, "ChannelId", "RoomId", "ConversationId",
            "ChannelID", "RoomID", "ConversationID");

        _propertiesResolved = true;
        Logger.Log(Tag, $"Properties resolved on {typeName}{(bridge != null ? $" (via .{bridge.Name})" : "")}: " +
            $"Id={_propMessageId.Name}, Content={_propContent.Name}, " +
            $"Author={_propAuthorName?.Name ?? "?"}, AuthorId={_propAuthorId?.Name ?? "?"}, " +
            $"Timestamp={_propTimestamp?.Name ?? "?"}, " +
            $"Channel={_propChannelId?.Name ?? "?"}");
        return true;
    }

    private static PropertyInfo? FindProp(PropertyInfo[] props, params string[] candidates)
    {
        foreach (var name in candidates)
        {
            foreach (var p in props)
            {
                if (p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return p;
            }
        }
        return null;
    }

    // ===== Phase 2: Collection Subscription =====

    /// <summary>
    /// Check if the current ItemsSource is still valid and subscribed.
    /// If the user navigated to a different room, re-discover and re-subscribe.
    /// </summary>
    private void EnsureCollectionSubscription()
    {
        if (_chatItemsControl == null) return;

        // Read current ItemsSource
        object? currentSource = null;
        try
        {
            currentSource = _chatItemsControl.GetType()
                .GetProperty("ItemsSource", BindingFlags.Public | BindingFlags.Instance)
                ?.GetValue(_chatItemsControl);
        }
        catch { }

        if (currentSource == null)
        {
            // ItemsControl may have been recycled — try to re-discover
            TryRediscoverChatControl();
            return;
        }

        // Check if source changed (user switched rooms)
        if (!ReferenceEquals(currentSource, _currentItemsSource))
        {
            Logger.Log(Tag, "ItemsSource changed — user switched rooms, re-subscribing");
            UnsubscribeCollection();
            _currentItemsSource = currentSource;
            _knownMessages.Clear();
            SubscribeToCollection(currentSource);
            TakeSnapshot(currentSource);
        }
        else if (!_subscribed)
        {
            SubscribeToCollection(currentSource);
            TakeSnapshot(currentSource);
        }
    }

    private void TryRediscoverChatControl()
    {
        // Walk the tree to find a new ItemsControl with chat messages
        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            var typeName = node.GetType().Name;
            // Match RootMessageItemsControl, ItemsControl, ListBox, ItemsRepeater
            if (!typeName.Contains("ItemsControl") && typeName != "ListBox" && typeName != "ItemsRepeater")
                continue;

            object? source = null;
            try
            {
                source = node.GetType()
                    .GetProperty("ItemsSource", BindingFlags.Public | BindingFlags.Instance)
                    ?.GetValue(node);
            }
            catch { }

            if (source == null) continue;

            // Check if it has items of the expected message type
            if (_messageItemType != null)
            {
                try
                {
                    var enumerator = (source as System.Collections.IEnumerable)?.GetEnumerator();
                    if (enumerator != null && enumerator.MoveNext() && enumerator.Current != null)
                    {
                        if (_messageItemType.IsAssignableFrom(enumerator.Current.GetType()))
                        {
                            Logger.Log(Tag, $"Re-discovered chat ItemsControl: {typeName}");
                            _chatItemsControl = node;
                            _currentItemsSource = source;
                            _knownMessages.Clear();
                            SubscribeToCollection(source);
                            TakeSnapshot(source);
                            return;
                        }
                    }
                }
                catch { }
            }
        }
    }

    /// <summary>
    /// Subscribe to CollectionChanged on an INotifyCollectionChanged collection.
    /// Uses Expression.Lambda to build a handler compatible with the trimmed runtime.
    /// </summary>
    private void SubscribeToCollection(object collection)
    {
        try
        {
            // Find CollectionChanged event — try directly on type first, then interfaces
            EventInfo? eventInfo = collection.GetType().GetEvent("CollectionChanged");
            if (eventInfo == null)
            {
                foreach (var iface in collection.GetType().GetInterfaces())
                {
                    if (iface.Name == "INotifyCollectionChanged")
                    {
                        eventInfo = iface.GetEvent("CollectionChanged");
                        break;
                    }
                }
            }

            if (eventInfo == null)
            {
                Logger.Log(Tag, "CollectionChanged event not found, using snapshot diffing");
                _subscribed = false;
                return;
            }

            var handlerType = eventInfo.EventHandlerType!;
            var invokeMethod = handlerType.GetMethod("Invoke")!;
            var paramTypes = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();

            // Build: (sender, e) => OnCollectionChanged(e)
            var callbackTarget = new Action<object>(OnCollectionChanged);
            var senderParam = Expression.Parameter(paramTypes[0], "sender");
            var argsParam = Expression.Parameter(paramTypes[1], "e");
            var callbackExpr = Expression.Constant(callbackTarget);
            var castArgs = Expression.Convert(argsParam, typeof(object));
            var invokeExpr = Expression.Invoke(callbackExpr, castArgs);
            var lambda = Expression.Lambda(handlerType, invokeExpr, senderParam, argsParam);
            var handler = lambda.Compile();

            eventInfo.AddEventHandler(collection, handler);
            _collectionChangedHandler = handler;
            _collectionChangedEvent = eventInfo;
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
        {
            try
            {
                _collectionChangedEvent.RemoveEventHandler(_currentItemsSource, _collectionChangedHandler);
            }
            catch { }
        }
        _collectionChangedHandler = null;
        _collectionChangedEvent = null;
        _subscribed = false;
    }

    /// <summary>
    /// Take a snapshot of all message IDs + content in the current collection.
    /// Used for initial population and change tracking.
    /// </summary>
    private void TakeSnapshot(object collection)
    {
        if (!_propertiesResolved) return;

        try
        {
            var enumerator = (collection as System.Collections.IEnumerable)?.GetEnumerator();
            if (enumerator == null) return;

            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (item == null) continue;

                var msgId = ReadMessageId(item);
                var content = ReadContent(item);
                if (msgId == null) continue;

                _knownMessages[msgId] = content ?? "";

                // Record in cache if not already present
                if (!_messageCache.ContainsKey(msgId))
                {
                    var cached = ExtractCachedMessage(item, msgId);
                    if (cached != null)
                    {
                        _messageCache[msgId] = cached;
                        _store.RecordMessage(cached.MessageId, cached.ChannelId,
                            cached.AuthorId, cached.AuthorName, cached.Timestamp, cached.Content);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"TakeSnapshot error: {ex.Message}");
        }
    }

    // ===== CollectionChanged Event Handler =====

    private void OnCollectionChanged(object args)
    {
        try
        {
            var settings = UprootedSettings.Load();

            // Read Action property (NotifyCollectionChangedAction: Add=0, Remove=1, Replace=2, Move=3, Reset=4)
            var actionProp = args.GetType().GetProperty("Action");
            int action = actionProp != null ? (int)actionProp.GetValue(args)! : -1;

            var newItems = args.GetType().GetProperty("NewItems")?.GetValue(args) as System.Collections.IList;
            var oldItems = args.GetType().GetProperty("OldItems")?.GetValue(args) as System.Collections.IList;

            switch (action)
            {
                case 0: // Add
                    if (newItems != null)
                        HandleItemsAdded(newItems, settings);
                    break;

                case 1: // Remove
                    if (oldItems != null)
                        HandleItemsRemoved(oldItems, settings);
                    break;

                case 2: // Replace
                    if (oldItems != null && newItems != null)
                        HandleItemsReplaced(oldItems, newItems, settings);
                    break;

                case 4: // Reset — channel switch, don't treat as deletion
                    Logger.Log(Tag, "Collection Reset — channel switch detected");
                    _knownMessages.Clear();
                    break;
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"OnCollectionChanged error: {ex.Message}");
        }
    }

    private void HandleItemsAdded(System.Collections.IList items, UprootedSettings settings)
    {
        if (!_propertiesResolved) return;

        foreach (var item in items)
        {
            if (item == null) continue;

            var msgId = ReadMessageId(item);
            if (msgId == null) continue;

            var content = ReadContent(item) ?? "";
            _knownMessages[msgId] = content;

            if (!_messageCache.ContainsKey(msgId))
            {
                var cached = ExtractCachedMessage(item, msgId);
                if (cached != null)
                {
                    _messageCache[msgId] = cached;
                    _store.RecordMessage(cached.MessageId, cached.ChannelId,
                        cached.AuthorId, cached.AuthorName, cached.Timestamp, cached.Content);
                    Logger.Log(Tag, $"MSG ADD: [{cached.AuthorName}] {Truncate(cached.Content, 60)}");
                }
            }
        }
    }

    private void HandleItemsRemoved(System.Collections.IList items, UprootedSettings settings)
    {
        if (!_propertiesResolved) return;

        // Batch removes > threshold = channel switch heuristic
        if (items.Count > BatchRemoveThreshold)
        {
            Logger.Log(Tag, $"Batch remove of {items.Count} items — treating as channel switch");
            _knownMessages.Clear();
            return;
        }

        if (!settings.MessageLoggerLogDeletes) return;

        foreach (var item in items)
        {
            if (item == null) continue;

            var msgId = ReadMessageId(item);
            if (msgId == null) continue;

            _knownMessages.Remove(msgId);

            if (_messageCache.TryGetValue(msgId, out var cached) && !cached.IsDeleted)
            {
                cached.IsDeleted = true;
                cached.DeletedAt = DateTime.UtcNow;
                _store.RecordDeletion(msgId, cached.ChannelId, DateTime.UtcNow);
                Logger.Log(Tag, $"MSG DEL: [{cached.AuthorName}] {Truncate(cached.Content, 60)}");
            }
        }
    }

    private void HandleItemsReplaced(System.Collections.IList oldItems, System.Collections.IList newItems,
        UprootedSettings settings)
    {
        if (!_propertiesResolved || !settings.MessageLoggerLogEdits) return;

        // Process old/new item pairs
        int count = Math.Min(oldItems.Count, newItems.Count);
        for (int i = 0; i < count; i++)
        {
            var oldItem = oldItems[i];
            var newItem = newItems[i];
            if (oldItem == null || newItem == null) continue;

            var msgId = ReadMessageId(newItem);
            if (msgId == null) continue;

            var oldContent = ReadContent(oldItem) ?? "";
            var newContent = ReadContent(newItem) ?? "";

            if (oldContent != newContent)
            {
                _knownMessages[msgId] = newContent;

                if (_messageCache.TryGetValue(msgId, out var cached))
                {
                    cached.Edits.Add(new MessageEdit
                    {
                        EditTime = DateTime.UtcNow,
                        PreviousContent = cached.Content
                    });
                    cached.Content = newContent;
                    _store.RecordEdit(msgId, cached.ChannelId, DateTime.UtcNow, oldContent);
                    Logger.Log(Tag, $"MSG EDIT: [{cached.AuthorName}] {Truncate(oldContent, 40)} -> {Truncate(newContent, 40)}");
                }
            }
        }
    }

    // ===== Message Property Readers =====

    /// <summary>
    /// Resolve the target object for property reads. If _propMessageObject is set,
    /// properties live on a nested object (ViewModel.Message); otherwise directly on item.
    /// </summary>
    private object? GetMessageTarget(object item)
    {
        if (_propMessageObject == null) return item;
        try { return _propMessageObject.GetValue(item); }
        catch { return null; }
    }

    private string? ReadMessageId(object item)
    {
        if (_propMessageId == null) return null;
        var target = GetMessageTarget(item);
        if (target == null) return null;
        try { return _propMessageId.GetValue(target)?.ToString(); }
        catch { return null; }
    }

    private string? ReadContent(object item)
    {
        if (_propContent == null) return null;
        var target = GetMessageTarget(item);
        if (target == null) return null;
        try { return _propContent.GetValue(target) as string; }
        catch { return null; }
    }

    private CachedMessage? ExtractCachedMessage(object item, string msgId)
    {
        var target = GetMessageTarget(item);
        if (target == null) return null;

        try
        {
            // Read timestamp — SentAtUtc is DateTimeOffset, convert to DateTime
            DateTime timestamp = DateTime.UtcNow;
            if (_propTimestamp != null)
            {
                var tsValue = _propTimestamp.GetValue(target);
                if (tsValue is DateTimeOffset dto)
                    timestamp = dto.UtcDateTime;
                else if (tsValue is DateTime dt)
                    timestamp = dt;
            }

            return new CachedMessage
            {
                MessageId = msgId,
                ChannelId = _propChannelId != null
                    ? (_propChannelId.GetValue(target)?.ToString() ?? "") : "",
                AuthorId = _propAuthorId != null
                    ? (_propAuthorId.GetValue(target)?.ToString() ?? "") : "",
                AuthorName = _propAuthorName != null
                    ? (_propAuthorName.GetValue(target)?.ToString() ?? "") : "Unknown",
                Timestamp = timestamp,
                Content = ReadContent(item) ?? ""
            };
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"ExtractCachedMessage error: {ex.Message}");
            return null;
        }
    }

    // ===== Phase 3: Visual Indicators =====

    /// <summary>
    /// Walk visible message elements and inject indicators for deleted/edited messages.
    /// Like LinkEmbedEngine, indicators are lost on scroll (VirtualizingStackPanel recycling)
    /// and re-injected from cache when the message reappears.
    /// </summary>
    private void InjectVisualIndicators(UprootedSettings settings)
    {
        // Walk CTextBlocks in the chat area
        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            var typeName = node.GetType().Name;
            if (typeName != "CTextBlock") continue;

            // Walk up to find the message layout Grid
            var (grid, col, colSpan) = FindMessageGrid(node);
            if (grid == null) continue;

            // Read DataContext to get message ID
            object? dc = null;
            var current = node;
            for (int i = 0; i < 10 && current != null; i++)
            {
                try
                {
                    dc = current.GetType()
                        .GetProperty("DataContext", BindingFlags.Public | BindingFlags.Instance)
                        ?.GetValue(current);
                }
                catch { }

                if (dc != null && _messageItemType != null &&
                    _messageItemType.IsAssignableFrom(dc.GetType()))
                    break;

                dc = null;
                current = _r.GetParent(current);
            }

            if (dc == null) continue;

            var msgId = ReadMessageId(dc);
            if (msgId == null) continue;

            if (!_messageCache.TryGetValue(msgId, out var cached)) continue;

            // Inject edit indicator
            if (settings.MessageLoggerLogEdits && cached.Edits.Count > 0)
            {
                InjectEditIndicator(grid, col, colSpan, cached);
            }

            // Check for deleted messages that were adjacent to this one
            // (deleted messages are removed from the visual tree, so we inject
            // the indicator into a neighboring visible message's Grid)
            if (settings.MessageLoggerLogDeletes)
            {
                InjectDeletionIndicators(grid, col, colSpan, cached);
            }
        }
    }

    /// <summary>
    /// Inject "(edited)" indicator below a message that has edit history.
    /// </summary>
    private void InjectEditIndicator(object grid, int col, int colSpan, CachedMessage cached)
    {
        var editTag = $"uprooted-msglog-edit:{cached.MessageId}";

        // Check if already injected
        var children = _r.GetChildren(grid);
        if (children != null)
        {
            foreach (var child in children)
            {
                if (child != null && _r.GetTag(child) == editTag) return;
            }
        }

        // Build "(edited)" indicator
        var editCount = cached.Edits.Count;
        var lastEdit = cached.Edits[editCount - 1];
        var editText = _r.CreateTextBlock(
            $"(edited{(editCount > 1 ? $" {editCount}x" : "")})",
            11, "#88AAAAAA");
        if (editText == null) return;

        _r.SetTag(editText, editTag);
        _r.SetMargin(editText, 0, 2, 0, 0);
        _r.SetCursorHand(editText);

        // Click to show edit history (log to console for now; full UI in future iteration)
        var capturedMsg = cached;
        _r.SubscribeEvent(editText, "PointerPressed", () =>
        {
            Logger.Log(Tag, $"Edit history for {capturedMsg.MessageId}:");
            foreach (var edit in capturedMsg.Edits)
            {
                Logger.Log(Tag, $"  [{edit.EditTime:HH:mm:ss}] {Truncate(edit.PreviousContent, 100)}");
            }
        });

        // Add to Grid in a new row
        int newRow = AddAutoRowToGrid(grid);
        _r.SetGridRow(editText, newRow);
        _r.SetGridColumn(editText, col);
        SetGridColumnSpan(editText, colSpan);
        _r.AddChild(grid, editText);
        _injectedIndicators.Add(editText);
    }

    /// <summary>
    /// Inject deletion indicator cards for messages deleted near this visible message.
    /// Checks if any deleted message in the cache has a timestamp close to this message.
    /// </summary>
    private void InjectDeletionIndicators(object grid, int col, int colSpan, CachedMessage visibleMsg)
    {
        // Look for deleted messages in the same channel near this message's timestamp
        foreach (var cached in _messageCache.Values)
        {
            if (!cached.IsDeleted || cached.ChannelId != visibleMsg.ChannelId) continue;
            if (cached.DeletedAt == null) continue;

            // Only show deletion indicators for messages deleted recently (last 24 hours)
            if ((DateTime.UtcNow - cached.DeletedAt.Value).TotalHours > 24) continue;

            // Check if the deleted message was near this visible message (within 5 minutes)
            var timeDiff = Math.Abs((cached.Timestamp - visibleMsg.Timestamp).TotalMinutes);
            if (timeDiff > 5) continue;

            var delTag = $"uprooted-msglog-del:{cached.MessageId}";

            // Check if already injected anywhere in the tree
            var children = _r.GetChildren(grid);
            if (children != null)
            {
                bool alreadyInjected = false;
                foreach (var child in children)
                {
                    if (child != null && _r.GetTag(child) == delTag)
                    {
                        alreadyInjected = true;
                        break;
                    }
                }
                if (alreadyInjected) continue;
            }

            // Build deletion card
            var delCard = BuildDeletionCard(cached);
            if (delCard == null) continue;

            _r.SetTag(delCard, delTag);
            _r.SetMargin(delCard, 0, 4, 0, 4);

            int newRow = AddAutoRowToGrid(grid);
            _r.SetGridRow(delCard, newRow);
            _r.SetGridColumn(delCard, col);
            SetGridColumnSpan(delCard, colSpan);
            _r.AddChild(grid, delCard);
            _injectedIndicators.Add(delCard);
        }
    }

    /// <summary>
    /// Build a red-tinted card showing a deleted message's content.
    /// Content is initially collapsed; clicking reveals the original text.
    /// </summary>
    private object? BuildDeletionCard(CachedMessage cached)
    {
        try
        {
            var content = _r.CreateStackPanel(vertical: true, spacing: 4);
            if (content == null) return null;

            // Header: "[deleted] AuthorName — HH:mm"
            var timeStr = cached.DeletedAt?.ToLocalTime().ToString("HH:mm") ?? "??:??";
            var headerText = _r.CreateTextBlock(
                $"[deleted] {cached.AuthorName} \u2014 {timeStr}",
                12, "#FF6B6B", "SemiBold");
            if (headerText != null)
                _r.AddChild(content, headerText);

            // Collapsed content (shown on click)
            var bodyText = _r.CreateTextBlock(
                Truncate(cached.Content, 200),
                12, "#CCFF6B6B");
            if (bodyText != null)
            {
                _r.SetTextWrapping(bodyText, "Wrap");
                // Start collapsed
                bodyText.GetType().GetProperty("IsVisible")?.SetValue(bodyText, false);
                _r.AddChild(content, bodyText);
            }

            // Red semi-transparent border
            var card = _r.CreateBorder("#20FF4444", cornerRadius: 6, child: content);
            if (card == null) return null;

            _r.SetPadding(card, 10, 6, 10, 6);
            _r.SetHorizontalAlignment(card, "Left");
            _r.SetMaxWidth(card, 500);
            _r.SetCursorHand(card);

            // Click to toggle body visibility
            if (bodyText != null)
            {
                var bodyRef = bodyText;
                _r.SubscribeEvent(card, "PointerPressed", () =>
                {
                    try
                    {
                        var isVisible = (bool)(bodyRef.GetType()
                            .GetProperty("IsVisible")?.GetValue(bodyRef) ?? false);
                        bodyRef.GetType().GetProperty("IsVisible")?.SetValue(bodyRef, !isVisible);
                    }
                    catch { }
                });
            }

            return card;
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"BuildDeletionCard error: {ex.Message}");
            return null;
        }
    }

    // ===== Visual Tree Helpers =====

    /// <summary>
    /// Find the message content Grid from a CTextBlock, following the same walk-up
    /// pattern as LinkEmbedEngine.FindInjectionGrid.
    /// </summary>
    private (object? grid, int column, int columnSpan) FindMessageGrid(object control)
    {
        var current = control;
        object? markdownTextBlock = null;

        for (int i = 0; i < 6; i++)
        {
            current = _r.GetParent(current);
            if (current == null) break;

            if (current.GetType().Name == "RootMarkdownTextBlock")
            {
                markdownTextBlock = current;
                break;
            }
        }

        if (markdownTextBlock == null) return (null, 0, 1);

        var grid = _r.GetParent(markdownTextBlock);
        if (grid == null || !_r.IsGrid(grid))
        {
            var above = _r.GetParent(grid!);
            if (above != null && _r.IsGrid(above))
                grid = above;
            else
                return (null, 0, 1);
        }

        int col = _r.GetGridColumn(markdownTextBlock);
        return (grid, col, 99);
    }

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
                    rowDefs.GetType().GetMethod("Add")?.Invoke(rowDefs, new[] { rowDef });
                    return count;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"AddAutoRowToGrid error: {ex.Message}");
        }
        return 99;
    }

    private void SetGridColumnSpan(object control, int span)
    {
        if (_r.GridType == null) return;
        var setColumnSpan = _r.GridType.GetMethod("SetColumnSpan",
            BindingFlags.Public | BindingFlags.Static);
        setColumnSpan?.Invoke(null, new object[] { control, span });
    }

    // ===== Utility =====

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
