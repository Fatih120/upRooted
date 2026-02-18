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

### ClearURLs

**Inspired by:** Vencord's ClearURLs plugin (which uses the [ClearURLs](https://docs.clearurls.xyz/latest/) browser extension rule database to strip tracking parameters from URLs).

**Layer:** C# hook (chat is Avalonia-native)

#### Approaches

**Approach 1 - Display-time cleaning (cosmetic)**

Watch the visual tree for URL TextBlocks in chat messages. When a URL is detected, parse it, strip known tracking parameters (utm_source, fbclid, etc.), and update the displayed text. The original URL is still what was sent/received over the wire, but the user sees the clean version.

- Pros: No new infrastructure needed. Follows the same visual tree watching pattern as LinkEmbedEngine.
- Cons: Cosmetic only. Clicking the link still navigates to the tracking URL. Other users still see the dirty URL.

**Approach 2 - Send-time interception (real cleaning)**

Intercept outgoing messages at the gRPC layer before they reach Root's API. Strip tracking parameters from URLs in message content so clean URLs are actually sent. Recipients see clean URLs too.

- Pros: URLs are genuinely cleaned. Other users benefit.
- Cons: Requires gRPC message interception (not yet built). Modifies user's outgoing message content, which some users may not expect.

Both approaches can coexist - display-time for incoming messages, send-time for outgoing.

#### Rules source

Fetch from the ClearURLs project's [rules database](https://docs.clearurls.xyz/latest/specs/rules/) (same approach as Vencord) or bundle a static snapshot. The rules database is a JSON file mapping domain patterns to parameter names to strip.

#### Settings

| Setting | Type | Default | Description |
|---------|------|---------|-------------|
| Enabled | bool | true | Master toggle |
| Clean outgoing | bool | false | Strip tracking params from messages you send (requires gRPC interception) |
| Custom rules | text | (empty) | Additional domain=param rules in key-value format |
| Domain exceptions | text | (empty) | Comma-separated domains to skip cleaning |

#### Complexity

Medium. URL parsing and regex rules are straightforward. The harder part is intercepting message content at the right layer. Display-time cleaning can ship first using the LinkEmbedEngine pattern; send-time cleaning depends on the gRPC interception prerequisite.

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

| Setting | Type | Default | Description |
|---------|------|---------|-------------|
| Log Deletes | bool | true | Log deleted messages |
| Log Edits | bool | true | Log edited messages |
| Delete Style | select | "red overlay" | How deleted messages are shown: "red overlay" or "red text" |
| Collapse Deleted | bool | false | Collapse deleted message content behind a click-to-reveal |
| Inline Edits | bool | true | Show edit history inline vs. tooltip-only |
| Ignore Bots | bool | false | Skip logging bot messages |
| Ignore Self | bool | false | Skip logging your own messages |
| Ignore Users | text | (empty) | Comma-separated user IDs to exclude from logging |

#### Complexity

High. This plugin requires either:
- gRPC interception infrastructure (new capability, not yet built), or
- Reliable visual tree change detection that can distinguish real deletions from VirtualizingStackPanel recycling

Additionally: storage format design, retention policy implementation, edit history UI, and the usual Avalonia reflection control creation. This is one of the more ambitious planned plugins.

---

## Prerequisites

Several capabilities need to exist before the planned plugins can reach their full potential:

### gRPC message interception layer

Both ClearURLs (send-time cleaning) and MessageLogger (deletion/edit detection) benefit from gRPC interception. This would be a framework-level capability - not plugin-specific - that intercepts gRPC-web traffic between Root's UI and its backend. See [gRPC Protocol](research/GRPC_PROTOCOL.md) for the protocol reference.

### Plugin toggle functionality in native UI

Already tracked in [Roadmap - Short-term Goals](ROADMAP.md). Users need to enable/disable individual plugins from the native settings UI.

### Reliable visual tree change detection

A utility that can distinguish genuine content changes (message added, message deleted) from VirtualizingStackPanel recycling (scroll-triggered add/remove). This would benefit multiple plugins beyond MessageLogger.

---

## Future Plugin Ideas

Space for additional plugin concepts as they emerge. When adding a new idea, include at minimum: the inspiration source, which layer it needs (C# hook vs. TypeScript browser), and a rough complexity estimate.

- *(Ideas will be added here as they are proposed)*

---

*Last updated: 2026-02-18*
