# Message Logger

Logs deleted and edited messages. Deleted messages appear with red styling; edited messages show the previous content with an amber edit indicator.

> **Status:** Shipped WIP (v0.4.0) — deletion detection + edit detection + edit indicators working
> **Layer:** C# hook (Avalonia-native) — chat is not rendered in DotNetBrowser
> **Files:** [`hook/MessageLogger.cs`](../../../hook/MessageLogger.cs), [`hook/MessageStore.cs`](../../../hook/MessageStore.cs)

---

## What it does

When someone deletes a message, Root removes it from the chat entirely. Message Logger intercepts these events, caches the original content, and re-injects a styled replacement control into the visual tree so deleted messages remain visible.

| Event | Default Root behavior | With Message Logger |
|-------|----------------------|-------------------|
| Message deleted | Message disappears from chat | Message stays, shown with red full-width stripe and 3px red left border |
| Message edited | Content silently replaced | Previous content shown with amber left border + "(edited)" label inline below the message |

## How it works

### Detection: per-item async pollers

MessageLogger subscribes to the chat message collection's `CollectionChanged` event. When an item is removed, a background poller is spawned for that item:

- Probes `HasBeenDeleted` (a boolean property on the message ViewModel) every 300ms for up to 3 seconds
- If `HasBeenDeleted` becomes `true` within the window → genuine user deletion → re-inject the message
- If the 3-second window expires without `true` → buffer management / scroll virtualization discard → ignored

This approach avoids the false-positive problem with naive `CollectionChanged` Remove listeners (Avalonia's `VirtualizingStackPanel` constantly removes items during scrolling).

**Epoch-based cancellation:** Each poller carries the channel's epoch at spawn time. When the user switches channels, the epoch increments and all in-flight pollers self-cancel, preventing cross-channel leakage.

**Per-type property cache:** A `Dictionary<Type, TypeProps>` handles multiple ViewModel types; nested `.Message` bridge property resolution finds `HasBeenDeleted` even when the collection item isn't the message directly.

### Detection: edit events

MessageLogger subscribes to `CollectionChanged` Replace events (action=2). When a Replace fires, `HandleReplaced()` runs two gates before recording an edit:

1. **Add-event eligibility** — the message must have arrived via a `CollectionChanged` Add event and been recorded in `_addedViaEvent`. Messages from the initial snapshot (when subscribing to a new collection) are excluded — their pre-subscription content is unknown.
2. **Grace period** — the Replace must arrive more than 5 seconds after the Add (`EditGracePeriodSeconds = 5.0`). Root fires optimistic Replaces within 0.5–2s of a send (content settling after server round-trip). Genuine user edits arrive after this window.

When both gates pass and the content has changed, the edit is recorded in the message cache and persisted to `MessageStore`.

### Display: edit indicators

`InjectEditIndicators()` runs each scan tick. It finds edited messages visible in the `VirtualizingStackPanel`, then injects an amber-tinted inline card as a new Grid row below the message:

- Semi-transparent amber `Border` background (`#1AFFCC44`)
- 3px amber left accent border (`#50FFCC44`)
- Previous content text (faded `#99BBBBBB`, italic)
- `(edited)` or `(edited Nx)` label (amber `#99D4A843`, small)

Cards use tag `uprooted-edit:{msgId}` for dedup. On VSP scroll recycling, the missing card is re-detected and re-injected on the next tick.

### Storage

Logged messages are stored by `MessageStore` (`hook/MessageStore.cs`) in a flat file:

- **Location:** `{Root profile dir}/uprooted-message-log.dat`
- **Format:** pipe-delimited append-only records — `MSG|EDIT|DEL` record types with URI-encoded fields
- **Flush:** buffered writes every 5 seconds
- **Retention:** configurable max message count (enforced on flush)

No `System.Text.Json` — broken in profiler context. All serialization is manual string formatting.

### Display: deleted messages

Deleted messages are re-injected as Discord-style full-width cards using reflection-based Avalonia control creation (`AvaloniaReflection`):

- Semi-transparent red-tinted `Border` background
- 3px red left accent border
- Right-click "Clear message history" context menu

Cards are tagged so the LinkEmbedEngine re-injection pattern can restore them after VirtualizingStackPanel recycling.

## Lifecycle

**Initialization (Phase 4.5c):**
1. Subscribe to the chat message collection's `CollectionChanged` event
2. Load the message log from `MessageStore` into an in-memory cache
3. Register per-collection poller infrastructure

**Runtime:**
1. On `CollectionChanged` Remove: spawn async poller for the removed item
2. Poller confirms `HasBeenDeleted = true` → cache content → re-inject deleted card
3. Periodic flush of in-memory cache to `MessageStore`
4. Retention policy applied on flush

**Shutdown:**
1. Cancel all in-flight pollers (epoch increment)
2. Flush remaining cache to disk
3. Unsubscribe from collection events

## Settings

| Setting | INI key | Default | Description |
|---------|---------|---------|-------------|
| Log Deleted Messages | `MessageLogger.LogDeleted` | `true` | Enable deletion detection and re-injection |
| Log Edited Messages | `MessageLogger.LogEdits` | `true` | Enable edit detection and amber edit indicators |
| Ignore Own Messages | `MessageLogger.IgnoreOwn` | `false` | Skip logging your own messages |
| Max Messages | `MessageLogger.MaxMessages` | `500` | Retention limit (enforced on flush) |

Settings are managed through `UprootedSettings` (INI-based, 10s TTL cache).

## Known Limitations

- **Edit detection needs validation** — The 5-second grace period filters send-completion Replaces in theory; real-world testing with actual edits is needed to confirm no false positives. Run `scripts/analyze-msglogger.ps1` and check for `[edit-gate]` / `[edit-detect]` entries.
- **Edit detection: initial snapshot exclusion** — Messages loaded from the initial snapshot (before the subscription activates) are not eligible for edit detection. Only messages that arrive via Add events after subscription are tracked.
- **VirtualizingStackPanel recycling** — injected cards (deleted + edit indicators) are destroyed when scrolled out of view and must be re-injected when scrolled back. The tag-based dedup pattern handles this but adds per-tick overhead.
- **No System.Text.Json** — all serialization uses manual string formatting (pipe-delimited) due to MissingMethodException in profiler context.
- **Storage is local-only** — no sync across devices.
- **No image/attachment logging** — only text content is logged.
- **Performance impact** — per-item pollers add baseline threading overhead proportional to message deletion frequency.

## Diagnostics

Enable `DIAG-INJ` / `DIAG-FLUSH` logging by checking the hook log. The analysis script `scripts/analyze-msglogger.ps1` parses hook log output for `MsgLogger` and `DIAG` entries and summarizes deletion events, poller outcomes, and injection counts.
