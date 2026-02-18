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

_Last updated: 2026-02-18_
