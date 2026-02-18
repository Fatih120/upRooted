using System.Linq.Expressions;
using System.Reflection;

namespace Uprooted;

/// <summary>
/// Message logger plugin — logs deleted and edited messages, shows visual indicators.
///
/// Architecture: Root's chat is Avalonia-native MVVM. Messages live in an ObservableCollection
/// bound to RootMessageItemsControl. Each item is a MessageViewModel with a nested .Message
/// object containing MessageId, MessageContent, SenderUserId, SentAtUtc, etc.
///
/// Deletion detection uses the ViewModel's HasBeenDeleted bool (set by Root when a message
/// is actually deleted) rather than collection Remove events, which also fire on channel
/// switches and virtualization recycling.
///
/// Visual indicators use in-place background tinting on the message's visual tree element
/// (like Vencord's CSS class approach), not injected card/embed controls.
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
    private int _scanning; // Interlocked reentrancy guard

    // Discovery results
    private object? _chatItemsControl;
    private object? _currentItemsSource;
    private Type? _messageItemType;

    // Property accessors on the nested Message object (via _propMessageObject bridge)
    private PropertyInfo? _propMessageObject; // ViewModel.Message -> nested Message
    private PropertyInfo? _propMessageId;
    private PropertyInfo? _propContent;
    private PropertyInfo? _propAuthorId;
    private PropertyInfo? _propTimestamp;
    private bool _propertiesResolved;

    // ViewModel-level properties (directly on the MessageViewModel)
    private PropertyInfo? _propHasBeenDeleted;
    private PropertyInfo? _propHasBeenEdited;

    // Collection subscription
    private Delegate? _collectionChangedHandler;
    private EventInfo? _collectionChangedEvent;
    private bool _subscribed;

    // Current channel tracking — read from TextChannelContentViewModel DataContext
    private string _currentChannelId = "";

    // In-memory message cache keyed by message ID
    private readonly Dictionary<string, CachedMessage> _messageCache = new();

    // Track which message IDs we've already tinted (to avoid re-applying per tick)
    private readonly HashSet<string> _tintedDeletedIds = new();
    private readonly HashSet<string> _tintedEditedIds = new();

    // Track content per message ID for edit detection via polling
    private readonly Dictionary<string, string> _contentSnapshot = new();

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
        int maxMessages = settings.MessageLoggerMaxMessages;
        if (_messageCache.Count > maxMessages)
        {
            _store.Truncate(maxMessages);
            Logger.Log(Tag, $"Truncated store to {maxMessages}");
        }

        // Run Phase 1 discovery on UI thread
        _r.RunOnUIThread(() =>
        {
            try { RunDiscoveryScan(); }
            catch (Exception ex) { Logger.LogException(Tag, "Discovery scan error", ex); }
        });

        _scanTimer = new Timer(OnScanTick, null, InitialDelayMs, ScanIntervalMs);
        Logger.Log(Tag, $"Scan timer started ({ScanIntervalMs}ms interval)");
    }

    // ===== Timer callback =====

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
                    EnsureCollectionSubscription();

                    if (_propertiesResolved)
                    {
                        // Poll messages for HasBeenDeleted / edit changes
                        PollMessageStates(settings);

                        // Apply in-place visual indicators
                        ApplyVisualIndicators(settings);
                    }
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

        // Find all ItemsControl-like containers
        var itemsControls = new List<object>();
        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            if (settingsSubtree != null && IsDescendantOf(node, settingsSubtree)) continue;
            var typeName = node.GetType().Name;
            if (typeName.Contains("ItemsControl") || typeName == "ListBox" || typeName == "ItemsRepeater")
                itemsControls.Add(node);
        }

        Logger.Log(Tag, $"Found {itemsControls.Count} ItemsControl-like containers");

        foreach (var ic in itemsControls)
            InspectItemsControl(ic);

        // Walk up from CTextBlock for ViewModel discovery
        InspectFromCTextBlock(settingsSubtree);

        // Read channel ID from the chat control's DataContext
        ReadCurrentChannelId();

        Logger.Log(Tag, $"Discovery complete: resolved={_propertiesResolved}, " +
            $"control={_chatItemsControl != null}, channel={_currentChannelId}");
        Logger.Log(Tag, "=== Phase 1 Discovery Complete ===");
    }

    private void InspectItemsControl(object itemsControl)
    {
        var typeName = itemsControl.GetType().Name;

        object? itemsSource = null;
        try
        {
            itemsSource = itemsControl.GetType()
                .GetProperty("ItemsSource", BindingFlags.Public | BindingFlags.Instance)
                ?.GetValue(itemsControl);
        }
        catch { }

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

        if (itemsSource == null) return;

        var sourceType = itemsSource.GetType();
        bool implementsINCC = false;
        foreach (var iface in sourceType.GetInterfaces())
        {
            if (iface.Name == "INotifyCollectionChanged") { implementsINCC = true; break; }
        }

        int itemCount = 0;
        try
        {
            var countProp = sourceType.GetProperty("Count");
            if (countProp != null) itemCount = (int)countProp.GetValue(itemsSource)!;
        }
        catch { }

        // Inspect items for property resolution
        if (itemCount > 0 && !_propertiesResolved)
        {
            try
            {
                var enumerator = (itemsSource as System.Collections.IEnumerable)?.GetEnumerator();
                if (enumerator != null)
                {
                    int inspected = 0;
                    while (enumerator.MoveNext() && inspected < 2)
                    {
                        var item = enumerator.Current;
                        if (item != null && !_propertiesResolved)
                            TryResolvePropertyAccessors(item.GetType());
                        inspected++;
                    }
                }
            }
            catch { }
        }

        // Only adopt the actual chat message collection
        bool isChatControl = typeName.Contains("Message", StringComparison.OrdinalIgnoreCase);
        if (!isChatControl && itemCount > 0)
        {
            try
            {
                var en = (itemsSource as System.Collections.IEnumerable)?.GetEnumerator();
                if (en != null && en.MoveNext() && en.Current != null)
                    isChatControl = en.Current.GetType().Name.Contains("MessageViewModel");
            }
            catch { }
        }

        if (isChatControl && implementsINCC && _chatItemsControl == null)
        {
            Logger.Log(Tag, $"Chat collection found: {typeName}, {itemCount} items");
            _chatItemsControl = itemsControl;
            _currentItemsSource = itemsSource;
        }
    }

    private void InspectFromCTextBlock(object? settingsSubtree)
    {
        object? ctextBlock = null;
        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            if (settingsSubtree != null && IsDescendantOf(node, settingsSubtree)) continue;
            if (node.GetType().Name == "CTextBlock") { ctextBlock = node; break; }
        }
        if (ctextBlock == null) return;

        var current = ctextBlock;
        for (int i = 0; i < 15 && current != null; i++)
        {
            object? dc = null;
            try
            {
                dc = current.GetType()
                    .GetProperty("DataContext", BindingFlags.Public | BindingFlags.Instance)
                    ?.GetValue(current);
            }
            catch { }

            if (dc != null && !_propertiesResolved)
            {
                var dcTypeName = dc.GetType().Name;
                if (dcTypeName.Contains("Message", StringComparison.OrdinalIgnoreCase))
                    TryResolvePropertyAccessors(dc.GetType());
            }
            current = _r.GetParent(current);
        }
    }

    // ===== Channel ID tracking =====

    /// <summary>
    /// Read the current channel ID from the DataContext hierarchy above the chat control.
    /// The TextChannelContentViewModel or its parent should have a channel/container identifier.
    /// </summary>
    private void ReadCurrentChannelId()
    {
        if (_chatItemsControl == null) return;

        // Walk up from chat control to find TextChannelContentViewModel
        var current = _chatItemsControl;
        for (int i = 0; i < 10 && current != null; i++)
        {
            object? dc = null;
            try
            {
                dc = current.GetType()
                    .GetProperty("DataContext", BindingFlags.Public | BindingFlags.Instance)
                    ?.GetValue(current);
            }
            catch { }

            if (dc != null)
            {
                // Try to read a channel/container ID from the ViewModel
                var dcType = dc.GetType();
                var channelProp = dcType.GetProperty("ChannelId", BindingFlags.Public | BindingFlags.Instance)
                    ?? dcType.GetProperty("MessageContainerId", BindingFlags.Public | BindingFlags.Instance)
                    ?? dcType.GetProperty("ContainerId", BindingFlags.Public | BindingFlags.Instance);

                if (channelProp != null)
                {
                    var val = channelProp.GetValue(dc)?.ToString();
                    if (!string.IsNullOrEmpty(val))
                    {
                        _currentChannelId = val!;
                        Logger.Log(Tag, $"Channel ID: {_currentChannelId} (from {dcType.Name}.{channelProp.Name})");
                        return;
                    }
                }

                // Fallback: use the DataContext's hash as a pseudo channel ID
                // This changes when switching channels even if we can't read the actual ID
                if (dcType.Name.Contains("TextChannel") || dcType.Name.Contains("Content"))
                {
                    _currentChannelId = $"dc:{dc.GetHashCode()}";
                    Logger.Log(Tag, $"Channel pseudo-ID: {_currentChannelId} (from {dcType.Name} hash)");
                    return;
                }
            }
            current = _r.GetParent(current);
        }
    }

    // ===== Property Resolution =====

    private void TryResolvePropertyAccessors(Type type)
    {
        _messageItemType = type;
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // ViewModel-level booleans
        _propHasBeenDeleted = FindProp(props, "HasBeenDeleted");
        _propHasBeenEdited = FindProp(props, "HasBeenEdited");

        if (TryResolveOnProps(props, type.Name, null))
            return;

        // Try nested Message object
        Logger.Log(Tag, $"Direct resolution failed on {type.Name}, trying nested...");
        foreach (var prop in props)
        {
            if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string) &&
                !prop.PropertyType.IsArray && prop.PropertyType.Namespace != "System")
            {
                var nestedProps = prop.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if (TryResolveOnProps(nestedProps, prop.PropertyType.Name, prop))
                {
                    Logger.Log(Tag, $"Resolved via nested: {type.Name}.{prop.Name}");
                    return;
                }
            }
        }
    }

    private bool TryResolveOnProps(PropertyInfo[] props, string typeName, PropertyInfo? bridge)
    {
        var id = FindProp(props, "MessageId", "Id", "Uuid", "Nonce", "ID");
        var content = FindProp(props, "MessageContent", "Content", "Text", "Body", "RawContent");
        if (id == null || content == null) return false;

        _propMessageObject = bridge;
        _propMessageId = id;
        _propContent = content;
        _propAuthorId = FindProp(props, "SenderUserId", "AuthorId", "SenderId", "UserId");
        _propTimestamp = FindProp(props, "SentAtUtc", "SentAt", "Timestamp", "CreatedAt");

        _propertiesResolved = true;
        Logger.Log(Tag, $"Properties resolved on {typeName}{(bridge != null ? $" (via .{bridge.Name})" : "")}: " +
            $"Id={_propMessageId.Name}, Content={_propContent.Name}, " +
            $"HasBeenDeleted={_propHasBeenDeleted?.Name ?? "?"}, " +
            $"HasBeenEdited={_propHasBeenEdited?.Name ?? "?"}");
        return true;
    }

    private static PropertyInfo? FindProp(PropertyInfo[] props, params string[] candidates)
    {
        foreach (var name in candidates)
            foreach (var p in props)
                if (p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return p;
        return null;
    }

    // ===== Phase 2: Collection Subscription =====

    private void EnsureCollectionSubscription()
    {
        if (_chatItemsControl == null) return;

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
            TryRediscoverChatControl();
            return;
        }

        if (!ReferenceEquals(currentSource, _currentItemsSource))
        {
            Logger.Log(Tag, "ItemsSource changed — channel switch");
            UnsubscribeCollection();
            _currentItemsSource = currentSource;
            _contentSnapshot.Clear();
            _tintedDeletedIds.Clear();
            _tintedEditedIds.Clear();
            ReadCurrentChannelId();
            SubscribeToCollection(currentSource);
            SnapshotCurrentMessages(currentSource);
        }
        else if (!_subscribed)
        {
            SubscribeToCollection(currentSource);
            SnapshotCurrentMessages(currentSource);
        }
    }

    private void TryRediscoverChatControl()
    {
        foreach (var node in _walker.DescendantsDepthFirst(_mainWindow))
        {
            var typeName = node.GetType().Name;
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

            if (_messageItemType != null)
            {
                try
                {
                    var en = (source as System.Collections.IEnumerable)?.GetEnumerator();
                    if (en != null && en.MoveNext() && en.Current != null &&
                        _messageItemType.IsAssignableFrom(en.Current.GetType()))
                    {
                        Logger.Log(Tag, $"Re-discovered chat: {typeName}");
                        _chatItemsControl = node;
                        _currentItemsSource = source;
                        _contentSnapshot.Clear();
                        _tintedDeletedIds.Clear();
                        _tintedEditedIds.Clear();
                        ReadCurrentChannelId();
                        SubscribeToCollection(source);
                        SnapshotCurrentMessages(source);
                        return;
                    }
                }
                catch { }
            }
        }
    }

    private void SubscribeToCollection(object collection)
    {
        try
        {
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
                Logger.Log(Tag, "CollectionChanged event not found");
                _subscribed = false;
                return;
            }

            var handlerType = eventInfo.EventHandlerType!;
            var invokeMethod = handlerType.GetMethod("Invoke")!;
            var paramTypes = invokeMethod.GetParameters().Select(p => p.ParameterType).ToArray();

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
            try { _collectionChangedEvent.RemoveEventHandler(_currentItemsSource, _collectionChangedHandler); }
            catch { }
        }
        _collectionChangedHandler = null;
        _collectionChangedEvent = null;
        _subscribed = false;
    }

    /// <summary>
    /// Snapshot all current message IDs and content for edit tracking.
    /// Also cache message data for later deletion/edit display.
    /// </summary>
    private void SnapshotCurrentMessages(object collection)
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
                if (msgId == null) continue;

                var content = ReadContent(item) ?? "";
                _contentSnapshot[msgId] = content;

                if (!_messageCache.ContainsKey(msgId))
                {
                    var cached = ExtractCachedMessage(item, msgId);
                    if (cached != null)
                    {
                        cached.ChannelId = _currentChannelId;
                        _messageCache[msgId] = cached;
                        _store.RecordMessage(cached.MessageId, cached.ChannelId,
                            cached.AuthorId, cached.AuthorName, cached.Timestamp, cached.Content);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Snapshot error: {ex.Message}");
        }
    }

    // ===== CollectionChanged — only for Add events (new messages) =====

    private void OnCollectionChanged(object args)
    {
        try
        {
            var actionProp = args.GetType().GetProperty("Action");
            int action = actionProp != null ? (int)actionProp.GetValue(args)! : -1;

            // Only handle Add — new messages entering the collection
            if (action == 0) // Add
            {
                var newItems = args.GetType().GetProperty("NewItems")?.GetValue(args) as System.Collections.IList;
                if (newItems != null) HandleItemsAdded(newItems);
            }
            else if (action == 4) // Reset — channel switch
            {
                Logger.Log(Tag, "Collection Reset — channel switch");
                _contentSnapshot.Clear();
                _tintedDeletedIds.Clear();
                _tintedEditedIds.Clear();
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"OnCollectionChanged error: {ex.Message}");
        }
    }

    private void HandleItemsAdded(System.Collections.IList items)
    {
        if (!_propertiesResolved) return;

        foreach (var item in items)
        {
            if (item == null) continue;
            var msgId = ReadMessageId(item);
            if (msgId == null) continue;

            var content = ReadContent(item) ?? "";
            _contentSnapshot[msgId] = content;

            if (!_messageCache.ContainsKey(msgId))
            {
                var cached = ExtractCachedMessage(item, msgId);
                if (cached != null)
                {
                    cached.ChannelId = _currentChannelId;
                    _messageCache[msgId] = cached;
                    _store.RecordMessage(cached.MessageId, cached.ChannelId,
                        cached.AuthorId, cached.AuthorName, cached.Timestamp, cached.Content);
                }
            }
        }
    }

    // ===== Polling: check HasBeenDeleted + content changes =====

    /// <summary>
    /// Poll all visible messages in the collection. Check HasBeenDeleted for genuine
    /// deletions, and compare content for edit detection. This avoids false positives
    /// from collection Remove events (which also fire on channel switch/virtualization).
    /// </summary>
    private void PollMessageStates(UprootedSettings settings)
    {
        if (_currentItemsSource == null) return;

        try
        {
            var enumerator = (_currentItemsSource as System.Collections.IEnumerable)?.GetEnumerator();
            if (enumerator == null) return;

            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (item == null) continue;

                var msgId = ReadMessageId(item);
                if (msgId == null) continue;

                // --- Deletion detection via HasBeenDeleted ---
                if (settings.MessageLoggerLogDeletes && _propHasBeenDeleted != null)
                {
                    try
                    {
                        var isDeleted = (bool)(_propHasBeenDeleted.GetValue(item) ?? false);
                        if (isDeleted && _messageCache.TryGetValue(msgId, out var cached) && !cached.IsDeleted)
                        {
                            cached.IsDeleted = true;
                            cached.DeletedAt = DateTime.UtcNow;
                            _store.RecordDeletion(msgId, cached.ChannelId, DateTime.UtcNow);
                            Logger.Log(Tag, $"MSG DEL: [{cached.AuthorName}] {Truncate(cached.Content, 60)}");
                        }
                    }
                    catch { }
                }

                // --- Edit detection via content comparison ---
                if (settings.MessageLoggerLogEdits)
                {
                    var currentContent = ReadContent(item) ?? "";
                    if (_contentSnapshot.TryGetValue(msgId, out var previousContent) &&
                        previousContent != currentContent && previousContent.Length > 0)
                    {
                        if (_messageCache.TryGetValue(msgId, out var cached))
                        {
                            cached.Edits.Add(new MessageEdit
                            {
                                EditTime = DateTime.UtcNow,
                                PreviousContent = previousContent
                            });
                            cached.Content = currentContent;
                            _store.RecordEdit(msgId, cached.ChannelId, DateTime.UtcNow, previousContent);
                            Logger.Log(Tag, $"MSG EDIT: [{cached.AuthorName}] {Truncate(previousContent, 40)} -> {Truncate(currentContent, 40)}");
                        }
                    }
                    _contentSnapshot[msgId] = currentContent;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"PollMessageStates error: {ex.Message}");
        }
    }

    // ===== Phase 3: In-place Visual Indicators =====

    /// <summary>
    /// Walk visible message containers and apply in-place background tinting.
    /// Deleted messages get a semi-transparent red background.
    /// Edited messages get a subtle "(edited)" indicator.
    /// Unlike Vencord's CSS class, we set the Background brush via reflection.
    /// </summary>
    private void ApplyVisualIndicators(UprootedSettings settings)
    {
        if (_chatItemsControl == null || _currentItemsSource == null) return;

        // Walk the visual children of the chat items control to find message containers
        foreach (var node in _walker.DescendantsDepthFirst(_chatItemsControl))
        {
            // Look for the per-message container (typically a ContentPresenter or Panel with DC=MessageViewModel)
            object? dc = null;
            try
            {
                dc = node.GetType()
                    .GetProperty("DataContext", BindingFlags.Public | BindingFlags.Instance)
                    ?.GetValue(node);
            }
            catch { }

            if (dc == null || _messageItemType == null || !_messageItemType.IsAssignableFrom(dc.GetType()))
                continue;

            var msgId = ReadMessageId(dc);
            if (msgId == null) continue;
            if (!_messageCache.TryGetValue(msgId, out var cached)) continue;

            // --- Deleted: red background tint ---
            if (settings.MessageLoggerLogDeletes && cached.IsDeleted)
            {
                if (!_tintedDeletedIds.Contains(msgId))
                {
                    ApplyDeletedTint(node, cached);
                    _tintedDeletedIds.Add(msgId);
                }
            }

            // --- Edited: inject small "(edited)" text ---
            if (settings.MessageLoggerLogEdits && cached.Edits.Count > 0)
            {
                if (!_tintedEditedIds.Contains(msgId))
                {
                    ApplyEditedIndicator(node, cached);
                    _tintedEditedIds.Add(msgId);
                }
            }
        }
    }

    /// <summary>
    /// Apply a red semi-transparent background to the message's container element.
    /// Works by finding the nearest Border or Panel ancestor and setting its Background.
    /// </summary>
    private void ApplyDeletedTint(object messageElement, CachedMessage cached)
    {
        try
        {
            // Walk up to find a Border or the message root element we can tint
            var target = messageElement;
            for (int i = 0; i < 5; i++)
            {
                var parent = _r.GetParent(target);
                if (parent == null) break;

                var parentName = parent.GetType().Name;
                // Stop at the message-level container (Border, Grid, or ContentPresenter)
                if (parentName == "Border" || parentName == "ContentPresenter" ||
                    (parentName == "Grid" && _r.IsGrid(parent)))
                {
                    target = parent;
                    break;
                }
                target = parent;
            }

            // Set Background to semi-transparent red
            _r.SetBackground(target, "#20FF4444");

            Logger.Log(Tag, $"Tinted deleted: {Truncate(cached.Content, 40)}");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"ApplyDeletedTint error: {ex.Message}");
        }
    }

    /// <summary>
    /// Add a small "(edited Nx)" text after the message content.
    /// Finds the CTextBlock/RootMarkdownTextBlock and adds a sibling.
    /// </summary>
    private void ApplyEditedIndicator(object messageElement, CachedMessage cached)
    {
        try
        {
            // Find the RootMarkdownTextBlock or CTextBlock within this message element
            object? textBlock = null;
            foreach (var child in _walker.DescendantsDepthFirst(messageElement))
            {
                var childName = child.GetType().Name;
                if (childName == "RootMarkdownTextBlock" || childName == "CTextBlock")
                {
                    textBlock = child;
                    break;
                }
            }

            if (textBlock == null) return;

            // Find the parent container to add the indicator as a sibling
            var parent = _r.GetParent(textBlock);
            if (parent == null) return;

            var editTag = $"uprooted-edited:{cached.MessageId}";

            // Check if already injected
            var children = _r.GetChildren(parent);
            if (children != null)
            {
                foreach (var c in children)
                    if (c != null && _r.GetTag(c) == editTag) return;
            }

            var editCount = cached.Edits.Count;
            var editLabel = _r.CreateTextBlock(
                $"(edited{(editCount > 1 ? $" {editCount}x" : "")})",
                10, "#88FF9999");
            if (editLabel == null) return;

            _r.SetTag(editLabel, editTag);
            _r.SetMargin(editLabel, 4, 0, 0, 0);

            // Try to add as sibling in the parent
            if (_r.IsGrid(parent))
            {
                // Add in same row/column as the text block
                int row = _r.GetGridRow(textBlock);
                int col = _r.GetGridColumn(textBlock);
                _r.SetGridRow(editLabel, row);
                _r.SetGridColumn(editLabel, col);
                _r.AddChild(parent, editLabel);
            }
            else
            {
                _r.AddChild(parent, editLabel);
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"ApplyEditedIndicator error: {ex.Message}");
        }
    }

    // ===== Message Property Readers =====

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
            DateTime timestamp = DateTime.UtcNow;
            if (_propTimestamp != null)
            {
                var tsValue = _propTimestamp.GetValue(target);
                if (tsValue is DateTimeOffset dto) timestamp = dto.UtcDateTime;
                else if (tsValue is DateTime dt) timestamp = dt;
            }

            return new CachedMessage
            {
                MessageId = msgId,
                ChannelId = _currentChannelId,
                AuthorId = _propAuthorId != null
                    ? (_propAuthorId.GetValue(target)?.ToString() ?? "") : "",
                AuthorName = "Unknown", // Will be resolved from visual tree later
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
