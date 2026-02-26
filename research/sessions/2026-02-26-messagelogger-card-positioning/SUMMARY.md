# Message System Decompilation Summary

## 1. Message IDs: MessageGuid / MessageUuid Lifecycle

### Type System
- **MessageGuid** is a **value struct** wrapping `(ulong High64, ulong Low64)` -- implements `IRootGuid<MessageGuid>`
- **MessageUuid** is a **protobuf class** wrapping the same `(High64, Low64)` pair -- used in gRPC wire format
- **Implicit conversions** exist both ways: `MessageUuid -> MessageGuid` and vice versa
- All Root IDs (Community, Channel, User, Message, etc.) share the same RootGuid format

### ID Structure (128-bit)
- **High64**: Upper 48 bits = milliseconds since Root epoch (2020-01-01 UTC), lower 16 bits include type tag + random
- **Low64**: Random with variant bits
- **ROOT_EPOCH** = 1577836800000 (Jan 1, 2020 UTC)
- `ToEpoch()` = `(High64 >> 16) + ROOT_EPOCH` -- Unix timestamp in milliseconds
- `ToDateTime()` = `DateTime.UnixEpoch.AddMilliseconds(ToEpoch())`
- Type tag at `High64 & 0xFF` -- `RootGuidType.Message`

### Key Properties
- **MessageGuid.Empty** exists for placeholder messages (local-only before server assigns real ID)
- IDs are **comparable/orderable** -- they sort by creation time naturally
- `GetStableHashCode()` = `(int)(Low64 & 0xFFFFFFFF)`

## 2. ConnectMessages() and the DynamicData Pipeline

### SourceList Architecture
MessageService uses a `SourceList<Message>` (`_messageCache`) as its reactive data source:

```csharp
private readonly SourceList<Message> _messageCache = new SourceList<Message>();

public IObservable<IChangeSet<Message>> ConnectMessages()
{
    return _messageCache.Connect().Synchronize(_sourceListGate);
}

public IObservable<IChangeSet<Message>> ConnectVirtualizedMessages()
{
    return _messageCache.Connect().Synchronize(_sourceListGate).Virtualise(_virtualRequest);
}
```

### Important: ConnectMessages vs ConnectVirtualizedMessages
- **ConnectMessages()**: Raw observable of ALL messages in the cache. No virtualization filtering.
- **ConnectVirtualizedMessages()**: Same observable but filtered through a sliding virtualization window (typically 20 items).
- The UI subscribes to **ConnectVirtualizedMessages()** for display.
- **ConnectMessages()** gives full access to all cached messages before virtualization trim.

### ChangeSet Events
DynamicData `IChangeSet<T>` fires:
- **Add**: New message added to _messageCache
- **Remove**: Message removed (cache trim, dispose, or explicit removal)
- **Replace/Refresh**: Message updated in place (content change, property change)

### Message Cache Management
- Cache is trimmed at 150 messages: `trimOldestMessagesFromCache()` removes oldest when count > 150
- Virtualization window slides through cached messages
- **clearMessageCache()** disposes ALL messages and clears the list

## 3. How Deletions Propagate Through the System

### Full Packet Flow for Message Deletion

```
Hub Server -> gRPC stream -> ClientHubPacket
    -> RootSession.packetReaderAsync() reads packets
    -> RootSession.handlePacket(hubPacket)
    -> packet.PacketType.IsCommunityPacket() ? CommunityService.HandlePacket(packet)
                                              : DirectMessageService.HandlePacket(packet)

Community path:
    CommunityService.HandlePacket(packet)
    -> community = GetCommunity(communityGuid)
    -> community.HandlePacket(packet)
    -> Channels.HandlePacket(packet)  [via ChannelService]
    -> PacketType.ChannelMessageDeleted:
        -> MessageDeletedPacket cast
        -> GetChannel(messageDeletedPacket.ContainerId)?.OnMessageDeletedPacketReceived(packet)
        -> Channel.OnMessageDeletedPacketReceived:
            -> if (Type == ChannelType.Text) Messages.OnMessageDeletedPacketReceived(packet)
        -> MessageService.OnMessageDeletedPacketReceived:
            -> processMessageDeleted(packet.Id)

DM path:
    DirectMessageService.HandlePacket(packet)
    -> PacketType.DirectMessageMessageDeleted:
        -> MessageDeletedPacket cast
        -> GetDirectMessage(containerId)?.OnMessageDeletedPacketReceived(packet)
        -> [same flow: Messages.OnMessageDeletedPacketReceived -> processMessageDeleted]
```

### processMessageDeleted(MessageGuid id) -- THE KEY METHOD

```csharp
private void processMessageDeleted(MessageGuid P_0)
{
    Message messageById = getMessageById(P_0);
    if (messageById != null)
    {
        messageById.SetAsDeleted();           // Sets DeletedAt, clears content & lists
        // Update visual layout: if deleted message showed user profile,
        // propagate that to the next message
        if (messageById.ShowUserProfile)
        {
            Message messageAfterId = getMessageAfterId(P_0);
            if (messageAfterId != null && messageAfterId.SenderUserId == messageById.SenderUserId
                && !messageAfterId.ShowUserProfile)
            {
                messageAfterId.SetShowUserProfile(true);
            }
        }
        // Clear "new" divider if it was on the deleted message
        if (_lastMessageWithNew != null && _lastMessageWithNew.MessageId == P_0)
        {
            _lastMessageWithNew.SetNewDividerStatus(false);
            _lastMessageWithNew = null;
        }
    }
    // Also update reply references
    foreach (var reply in getMessageRepliesContainingMessage(P_0))
        reply.UpdateMessageContent(null);
    foreach (var reply in getPinnedMessageRepliesContainingMessage(P_0))
        reply.UpdateMessageContent(null);
    // Remove from pinned cache
    var pinned = getPinnedMessageById(P_0);
    if (pinned != null) { _pinnedMessagesCache.Remove(pinned); pinned.Dispose(); }
}
```

### Message.SetAsDeleted() -- What Happens to the Message Object

```csharp
public void SetAsDeleted()
{
    DeletedAt = DateTimeOffset.UtcNow;   // Sets HasBeenDeleted = true (computed property)
    MessageContent = string.Empty;        // WIPES the content!
    _linkCache.Clear(); _linkCache.Dispose();
    _fileCache.Clear(); _fileCache.Dispose();
    _mediaCache.Clear(); _mediaCache.Dispose();
    _reactions.Clear(); _reactions.Dispose();
}
```

**CRITICAL**: `SetAsDeleted()` **destroys** the message content and all attachments. After this call:
- `MessageContent` is `""` (empty string)
- `DeletedAt` is set to `DateTimeOffset.UtcNow`
- `HasBeenDeleted` returns `true`
- All caches (links, files, media, reactions) are cleared AND disposed

The message **stays in `_messageCache`** (it is NOT removed). This means:
- ConnectMessages() will fire a **property change notification** (via ObservableObject)
- The message appears as a tombstone in the UI (empty content, deleted styling)

### User-Initiated Deletion (vs Server-Push)

When the user deletes their own message:
```csharp
public async Task DeleteMessageAsync(MessageGuid P_0)
{
    Message message = getMessageById(P_0);
    if (message?.IsPlaceholder ?? false)
    {
        _messageCache.Remove(message);  // Placeholder: just remove
        message.Dispose();
        return;
    }
    // Real message: send to server, then process locally
    await _messageRepository.DeleteMessageAsync(request);
    processMessageDeleted(P_0);  // Same path as server-push
}
```

### MessageDeletedPacket Structure

```protobuf
message MessageDeletedPacket {
    PacketType packet_type = 1;
    CommunityUuid community_id = 3;
    MessageContainerUuid container_id = 4;
    MessageUuid id = 5;
    Timestamp deleted_at = 6;
}
```

## 4. Hook Points for Detecting Deletions

### Best Hook Point: IMessageService.OnMessageDeletedPacketReceived

The cleanest interception point is `OnMessageDeletedPacketReceived(MessageDeletedPacket)` on the `IMessageService` (implemented by `MessageService`).

**Why this is ideal:**
- Called for BOTH community channels AND DMs
- Called BEFORE `processMessageDeleted()` wipes the message content
- The `MessageDeletedPacket` contains `Id` (which message), `ContainerId` (which channel), `CommunityId` (which community)
- You can look up the message content BEFORE `SetAsDeleted()` destroys it

### Alternative: Subscribe to ConnectMessages() changeset

The current MessageLogger approach subscribes to `ConnectMessages()`. When a deletion happens:
1. `SetAsDeleted()` sets `DeletedAt` and clears `MessageContent`
2. Property change notifications fire via `ObservableObject`
3. The DynamicData changeset fires a **Refresh** event (property change detected)

**Problem**: By the time the changeset fires, `MessageContent` is already `""`. You need to have cached the content beforehand.

### Alternative: Intercept processMessageDeleted before SetAsDeleted

Hook into `MessageService.processMessageDeleted` before it calls `message.SetAsDeleted()`. This requires reflection to either:
- Replace the method (not easily possible in .NET)
- Subscribe to the `DeletedAt` property CHANGING event (via `INotifyPropertyChanging` from `ObservableObject`)

### Recommended Approach

**Option A (Pre-cache + PropertyChanged):**
1. **Pre-cache**: Subscribe to `ConnectMessages()` and snapshot every message content into a dictionary
2. **Detect deletion**: When `DeletedAt` property changes from null to non-null (via PropertyChanged), the message was deleted
3. **Retrieve content**: Look up the pre-cached content using the message MessageId

**Option B (Packet-level interception):**
1. **Intercept at the packet level**: Hook `Channel.OnMessageDeletedPacketReceived` or `MessageService.OnMessageDeletedPacketReceived`
2. **At interception time**: Look up the message by ID in `_messageCache` -> read its content -> log it -> then let the original handler proceed

## 5. Edit Detection

For edits, the flow is:
- `OnMessageEditedPacketReceived(MessagePacket)` -> `processMessageEditedPacket`
- `processMessageEditedPacket`: calls `message.UpdateMessage(packet)` which sets new `MessageContent`, `EditedAt`, etc.
- The `MessagePacket` contains the NEW content
- Previous content must be cached before the update

## 6. GlobalMessageStreamService (New Message Notifications)

For community channels only, `CommunityService.HandlePacket` also calls:
```csharp
_globalMessageStreamService.OnChannelMessageReceived(messagePacket, channel.ContainerId, community.Id, channel.Name, community.Name);
```

This fires a `Subject<GlobalMessageEvent>` that can be subscribed to. **Note**: This only fires for `ChannelMessageCreated`, NOT for deletions or edits.

## 7. Key Types Summary

| Type | Assembly | Purpose |
|------|----------|---------|
| `Message` | CoreDomain | UI model, ObservableObject, has SetAsDeleted() |
| `MessageService` | CoreDomain | Manages message cache, handles packets |
| `IMessageService` | CoreDomain | Interface with all packet handlers |
| `MessagePacket` | WebApi.Shared | Protobuf: full message with content |
| `MessageDeletedPacket` | WebApi.Shared | Protobuf: just ID + timestamps |
| `MessageUuid` | Core | Protobuf UUID, converts to/from MessageGuid |
| `MessageGuid` | Core | Value struct, sortable, contains epoch |
| `RootGuidInternals` | Core | Epoch conversion, type detection |
| `ChannelService` | CoreDomain | Routes packets to channels |
| `Channel` | CoreDomain | Has Messages (IMessageService) |
| `DirectMessageService` | CoreDomain | Routes DM packets |
| `GlobalMessageStreamService` | CoreDomain | New message notifications (create only) |
| `RootSession` | CoreDomain | Top-level packet reader and router |
