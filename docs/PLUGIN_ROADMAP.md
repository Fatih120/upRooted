# Plugin Roadmap

Planned plugins for Uprooted, with architectural notes and implementation strategies. Each entry documents how the plugin would be adapted from its inspiration (typically Vencord) to work within Uprooted's architecture.

> **Related docs:** [Roadmap](ROADMAP.md) | [Plugin Quickstart](plugins/GETTING_STARTED.md) | [Plugin API Reference](plugins/API_REFERENCE.md) | [Built-in Plugins](plugins/builtin/INDEX.md) | [Architecture](framework/ARCHITECTURE.md)

---

## Architecture Notes

### Chat is Avalonia-native

Root renders chat using Avalonia's native UI controls (TextBlocks, StackPanels, etc.) - not in a browser view. DotNetBrowser is auxiliary, used for WebRTC, OAuth, and sub-apps. This means any plugin that modifies chat content must be implemented as a C# hook-layer feature, not a TypeScript browser-side plugin.

### Plugin tiers

Uprooted plugins fall into two implementation tiers:

- **C# native (hook layer)** - For anything that touches chat, the visual tree, or gRPC traffic. Built via Avalonia reflection, visual tree manipulation, and/or gRPC interception. Examples: LinkEmbedEngine, ThemeEngine, SidebarInjector.
- **TypeScript browser-side** - For anything inside DotNetBrowser's Chromium context (WebRTC sub-apps, bridge interception, DOM injection). Examples: sentry-blocker, browser-side settings panel, browser-side themes.

### Reference implementation

The `LinkEmbedEngine` (`hook/LinkEmbedEngine.cs`) is the proven pattern for C# native chat plugins. It demonstrates:

- Visual tree watching for chat content changes
- Reflection-based Avalonia control creation (Border, TextBlock, Image, etc.)
- Async metadata fetching with caching
- Inline injection of custom controls into the chat message flow
- Handling VirtualizingStackPanel recycling

New C# chat plugins should follow this pattern.

---

## Shipped Plugins

### ~~MessageLogger~~ ✓ (shipped in v0.3.6-rc)

See [`docs/plugins/builtin/message-logger.md`](plugins/builtin/message-logger.md) for the full design reference and [`hook/MessageLogger.cs`](../../hook/MessageLogger.cs) + [`hook/MessageStore.cs`](../../hook/MessageStore.cs) for the implementation.

---

## Planned Plugins

### ClearURLs (IMPLEMENTED - v0.3.5)

**Inspired by:** Vencord's ClearURLs plugin (which uses the [ClearURLs](https://docs.clearurls.xyz/latest/) browser extension rule database to strip tracking parameters from URLs).

**Layer:** C# hook (`hook/ClearUrlsEngine.cs`)

**Status:** Shipped. Strips tracking parameters from URLs in the compose editor when the user presses Enter to send.

#### Implementation

**Compose-input interception** — hooks AvaloniaEdit.TextEditor's TextArea child via Avalonia's `AddHandler(RoutedEvent, Delegate, RoutingStrategies, handledEventsToo=true)` with all routing strategies (Bubble|Tunnel|Direct). When Enter is pressed, scans the editor text for URLs with tracking parameters and strips them in-place before the message is dispatched.

Key technical details:
- Root's compose input is `AvaloniaEdit.TextEditor`, not `Avalonia.Controls.TextBox`
- AvaloniaEdit marks Enter as `Handled=true` — requires `handledEventsToo=true` AND all three routing strategies (Bubble alone is insufficient)
- TextEditor.Text CLR property works (AvaloniaEdit is third-party, not subject to Root's trimming)
- Timer-based discovery (2s interval) finds TextArea controls in the visual tree
- 33 tracking parameters in a case-insensitive HashSet for O(1) lookup
- Idempotent: if no tracking params remain, no write occurs
- Fragment (#hash) preserving

#### Future enhancements

- Display-time cleaning of incoming messages (cosmetic, visual tree approach)
- gRPC send-time interception (alternative to compose-input approach)
- ClearURLs rules database import for broader coverage
- Custom rules and domain exceptions settings

---

### MessageLogger

**Inspired by:** Vencord's MessageLogger plugin (logs deleted and edited messages, shows them with visual indicators in chat).

**Layer:** C# hook (chat is Avalonia-native)

#### Message deletion detection

Two possible approaches:

- **Visual tree watching:** Monitor the chat's VirtualizingStackPanel for children being removed. Unreliable on its own because virtualization constantly adds/removes children as the user scrolls - distinguishing a genuine deletion from recycling is non-trivial.
- **gRPC interception:** Intercept gRPC responses that signal message deletions. More reliable because the deletion event is unambiguous at the protocol level. The gRPC Protocol reference documents the message service endpoints.

The gRPC approach is preferred. Visual tree watching alone would require heuristics to distinguish deletions from virtualization, which would produce false positives.

#### Message edit detection

Intercept gRPC message update responses and cache the previous content before Root's UI updates. Store the edit history per message ID.

#### Storage

Use a simple flat-file format (not System.Text.Json - broken in profiler context). Store per message:

- Message ID
- Channel/room ID
- Original content
- Edit history (timestamped list of previous versions)
- Deletion timestamp (if deleted)

Retention policy needed to prevent unbounded growth:

- Option A: Last N messages (e.g., 1000)
- Option B: Time-based (e.g., last 24 hours)
- Option C: Size-based (e.g., max 10MB)

#### Display

Modify the visual tree to show logged messages:

- **Deleted messages:** Show with a red-tinted overlay or red text styling, matching Vencord's "Delete Style" options. Use the same Avalonia control creation pattern as LinkEmbedEngine (reflection-based Border, TextBlock, etc.).
- **Edited messages:** Show edit history inline below the message, or via a tooltip/popup on hover. Each previous version displayed with a timestamp.

#### Settings

| Setting          | Type   | Default       | Description                                                 |
| ---------------- | ------ | ------------- | ----------------------------------------------------------- |
| Log Deletes      | bool   | true          | Log deleted messages                                        |
| Log Edits        | bool   | true          | Log edited messages                                         |
| Delete Style     | select | "red overlay" | How deleted messages are shown: "red overlay" or "red text" |
| Collapse Deleted | bool   | false         | Collapse deleted message content behind a click-to-reveal   |
| Inline Edits     | bool   | true          | Show edit history inline vs. tooltip-only                   |
| Ignore Bots      | bool   | false         | Skip logging bot messages                                   |
| Ignore Self      | bool   | false         | Skip logging your own messages                              |
| Ignore Users     | text   | (empty)       | Comma-separated user IDs to exclude from logging            |

#### Complexity

High. This plugin requires either:

- gRPC interception infrastructure (new capability, not yet built), or
- Reliable visual tree change detection that can distinguish real deletions from VirtualizingStackPanel recycling

Additionally: storage format design, retention policy implementation, edit history UI, and the usual Avalonia reflection control creation. This is one of the more ambitious planned plugins.

---

### NoReplyPing

**Inspired by:** Vencord's NoReplyPing plugin (disables the "mention user" ping that fires by default when replying to a message).

**Layer:** C# hook (chat UI is Avalonia-native)

#### What it does

When a user clicks "Reply" on a chat message in Root, the reply composer appears with a ping/mention toggle checked by default. This plugin watches for the reply composer in the visual tree and automatically unchecks the ping toggle, so replies do not mention the original author unless the user re-enables it manually.

#### Approach

**Visual tree polling (same pattern as SidebarInjector)**

A timer (200–500ms interval) scans the chat input area of the visual tree for a reply composer control and its ping toggle child. When the reply composer is detected and the toggle is checked, the plugin programmatically unchecks it. An `Interlocked` guard and tag-based dedup prevent repeated writes on subsequent timer ticks.

This is the simplest approach and requires no new infrastructure. The only prerequisite is identifying Root's exact control class names and property names for the reply composer — discoverable via the diagnostic visual tree dump.

Alternative: subscribe to a DataContext or property change event on the chat input to detect when reply mode is activated. More reactive than polling but requires more exploration of Root's MVVM layer.

#### Settings

| Setting | Type | Default | Description |
|---------|------|---------|-------------|
| Enabled | bool | true | Master toggle |

#### Complexity

**Low.** Pattern is identical to `SidebarInjector`'s periodic visual tree scan and control mutation. The only unknowns are Root's control type names and hierarchy for the reply UI, which require a single visual tree dump to resolve. No new framework infrastructure needed.

---

### Translate

**Inspired by:** Vencord's Translate plugin (right-click any message to translate it).

**Layer:** C# hook (chat is Avalonia-native)

#### What it does

Adds per-message translation to chat. User triggers translation of a specific message (via right-click context menu or a translate button) and sees the translated text displayed inline below the original, in the same chat flow. Supports a configurable target language and multiple translation providers.

#### Approaches

**Approach 1 — Right-click context menu (preferred)**

Detect when Root's context menu opens over a chat message. Inject a "Translate" item into the menu. On click, fetch the translated text, then inject a styled `TextBlock` (using reflection, following the `LinkEmbedEngine` pattern) below the message in the visual tree.

- Pros: Non-intrusive, familiar UX. Only fetches translations on demand.
- Cons: Requires discovering how Root constructs its context menus in Avalonia (may be a native `ContextMenu` on the message container, or a custom popup). Context menu extension may need Expression.Lambda event hookup.

**Approach 2 — Translate button on pointer enter**

Watch for `PointerEntered` events on message containers. Inject a small translate `Button` next to the message timestamp area. On click, fetch and display the translation inline. Remove the button on `PointerExited`.

- Pros: Discoverable without right-clicking.
- Cons: More visual noise. Button injection/removal on every hover is higher-frequency than context menu interception.

**Approach 3 — Auto-translate (optional feature, not default)**

Detect the language of incoming messages. If the language does not match the user's target language, automatically fetch and display the translation below the message. Only practical with a fast, rate-limit-tolerant provider.

Both Approach 1 and an optional Approach 3 can coexist as separate settings toggles.

#### Translation providers

| Provider | API Key? | Cost | Rate Limits | Notes |
|----------|----------|------|-------------|-------|
| Google Translate (unofficial) | No | Free | Per-IP, unpredictable | Same endpoint Vencord uses. No auth needed. |
| Google Cloud Translation API | Yes | Paid (free tier) | Generous with key | Stable, official, requires billing setup |
| DeepL Free API | Yes (free account) | Free tier | 500k chars/month | Higher quality for European languages |
| LibreTranslate | Self-hosted / yes | Free | None (self-hosted) | Open source, requires user to supply endpoint |

Default to the unofficial Google Translate endpoint (no API key required). Allow users to override with their preferred provider and key.

#### Display

Translated text is injected as a styled `Border` + `TextBlock` block directly below the original message, using the same reflection-based control creation as `LinkEmbedEngine`. Include a small language label (`🌐 Translated from French`) and a dismiss button to hide the translation. If `Show original` is enabled, both texts appear stacked; if disabled, only the translation is shown.

#### Settings

| Setting | Type | Default | Description |
|---------|------|---------|-------------|
| Enabled | bool | true | Master toggle |
| Target language | string | "en" | ISO 639-1 language code to translate into |
| Provider | select | "google" | Translation provider |
| API key | text | (empty) | API key for providers that require one (DeepL, Google Cloud) |
| Show original | bool | true | Keep original message text visible alongside translation |
| Auto-translate | bool | false | Automatically translate messages not in the target language |
| Auto-translate threshold | select | "always" | When auto-translate fires: "always", "on detection failure", or "never" |

#### Complexity

**Medium.** Visual tree injection of the translated text block follows the `LinkEmbedEngine` pattern and is well-understood. The harder parts are:
1. Hooking into Root's right-click context menu in Avalonia (needs visual tree exploration to find the context menu control and injection point).
2. Rate limiting / error handling for the unofficial Google Translate endpoint.
3. Language detection for auto-translate (either use the translation API's detection response or a lightweight client-side heuristic).

No new framework infrastructure required beyond what `LinkEmbedEngine` already demonstrates.

---

## Prerequisites

Several capabilities need to exist before the planned plugins can reach their full potential:

### gRPC message interception layer

ClearURLs v1 ships without gRPC (uses compose-input interception instead), but future display-time cleaning of incoming messages and MessageLogger (deletion/edit detection) would benefit from gRPC interception. This would be a framework-level capability - not plugin-specific - that intercepts gRPC-web traffic between Root's UI and its backend. See [gRPC Protocol](research/GRPC_PROTOCOL.md) for the protocol reference.

### Plugin toggle functionality in native UI

Already tracked in [Roadmap - Short-term Goals](ROADMAP.md). Users need to enable/disable individual plugins from the native settings UI.

### Reliable visual tree change detection

A utility that can distinguish genuine content changes (message added, message deleted) from VirtualizingStackPanel recycling (scroll-triggered add/remove). This would benefit multiple plugins beyond MessageLogger.

---

## Future Plugin Ideas

Space for additional plugin concepts as they emerge. When adding a new idea, include at minimum: the inspiration source, which layer it needs (C# hook vs. TypeScript browser), and a rough complexity estimate.

- _(Ideas will be added here as they are proposed)_

---

*Last updated: 2026-02-18 — added NoReplyPing and Translate plugin plans*
