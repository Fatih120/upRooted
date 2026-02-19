# Built-in Plugins

> **What this is:** Built-in plugin overview — what ships with Uprooted, load order, C# vs TypeScript runtime differences.
> **Read when:** Understanding what plugins are included; checking load order; understanding the two plugin runtimes.

Uprooted ships seven built-in plugins across two runtime layers. All are registered automatically at startup and appear in the Plugin Settings page.

> **Related docs:** [Plugin API Reference](../API_REFERENCE.md) | [Root Environment](../ROOT_ENVIRONMENT.md) | [TypeScript Reference](../../framework/TYPESCRIPT_REFERENCE.md)

---

## Plugin Overview

### TypeScript / DotNetBrowser Layer

| Plugin | Purpose | Settings | Source |
|--------|---------|----------|--------|
| [Sentry Blocker](sentry-blocker.md) | Blocks Sentry telemetry to protect user privacy | None | `src/plugins/sentry-blocker/` |
| [Themes](themes.md) | CSS variable theme engine with presets and custom colors | Theme selector, accent/background colors | `src/plugins/themes/` + `hook/ThemeEngine.cs` |
| [Link Embeds](link-embeds.md) | Discord-style rich link previews, YouTube thumbnails, animated GIFs | Show file names toggle | `hook/LinkEmbedEngine.cs` + `src/plugins/link-embeds/` |
| SilentTyping | Prevents your typing indicator from being sent | None | `src/plugins/silent-typing/` |

### C# Hook / Avalonia-Native Layer

These plugins run inside Root's .NET process via the CLR profiler hook and modify the Avalonia visual tree directly. They are not browser plugins.

| Plugin | Purpose | Settings | Source |
|--------|---------|----------|--------|
| ClearURLs | Strips tracking parameters (utm_*, fbclid, gclid, etc.) from URLs before sending | None | `hook/ClearUrlsEngine.cs` |
| [Message Logger](message-logger.md) | Logs deleted messages with visual indicators | Delete/edit toggles, retention limit, ignore own messages | `hook/MessageLogger.cs`, `hook/MessageStore.cs` |
| ContentFilter | Blurs images flagged as NSFW using Google Cloud Vision | API key, threshold | `hook/NsfwFilter.cs` |

### Core Framework (not toggleable)

| Component | Purpose | Source |
|-----------|---------|--------|
| [Settings Panel](settings-panel.md) | Injects Uprooted UI into Root's settings sidebar | `src/plugins/settings-panel/` + `hook/SidebarInjector.cs` |

## Load Order

### TypeScript plugins (DotNetBrowser startup)

Registered and started in this order:

1. **sentry-blocker** -- must run first to block telemetry before Sentry sends anything
2. **themes** -- applies CSS variables before the UI renders
3. **settings-panel** -- depends on the other plugins being registered so it can list them
4. **link-embeds** -- enhances chat content after the page is loaded
5. **silent-typing** -- blocks typing indicator gRPC calls

### C# hook plugins (phased startup)

Initialized after Avalonia is ready, with delays to ensure chat is populated:

- **ClearUrlsEngine** -- Phase 4.5a (14s delay), hooks AvaloniaEdit TextArea
- **LinkEmbedEngine** -- Phase 4.5b (14s delay), visual tree watching for chat links
- **MessageLogger** -- Phase 4.5c (20s delay), subscribes to chat ObservableCollection

## Runtime Context

### TypeScript / DotNetBrowser plugins

These run inside DotNetBrowser's embedded Chromium instance. Key constraints:

- **No localStorage** -- Root runs Chromium with `--incognito`, so all browser storage is wiped on restart
- **No CORS restrictions** -- Root runs Chromium with `--disable-web-security`, so fetch works cross-origin
- **Settings are session-only** -- runtime changes via the settings panel reset on restart; use the installer for persistent configuration
- **Chat is NOT in this context** -- Root's chat UI is native Avalonia. DotNetBrowser handles WebRTC, OAuth, and sub-apps only

### C# hook / Avalonia-native plugins

These run inside Root's .NET process via the CLR profiler. Key characteristics:

- **Settings persist** -- stored in `uprooted-settings.ini` in the profile directory, reloaded on each access with 10s TTL cache
- **Direct visual tree access** -- can create, modify, and inject native Avalonia controls
- **No `System.Text.Json`** -- causes `MissingMethodException` in profiler context; use `UprootedSettings` (INI) for persistence
- **UI thread required** -- all Avalonia mutations must dispatch via `AvaloniaReflection.RunOnUIThread()`

## Shared Globals

| Global | Type | Purpose |
|--------|------|---------|
| `window.__UPROOTED_SETTINGS__` | `UprootedSettings` | Settings loaded from `uprooted-settings.json` by the installer/patcher |
| `window.__UPROOTED_LOADER__` | `PluginLoader` | Plugin lifecycle manager (used by settings-panel) |
| `window.__UPROOTED_VERSION__` | `string` | Version string (e.g. `"0.4.1"`) |

---

**Canonical for:** built-in plugin inventory, load order, C# vs TypeScript runtime layer differences
*Built-in plugins index. Last updated 2026-02-19.*
