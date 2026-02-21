# Architecture Reference

> **What this is:** Authoritative architecture reference — system design, layer boundaries, startup phases, threading, error handling, critical rules, and AI contributor guide.
> **Read when:** Starting any task; understanding how the system fits together; checking critical rules before writing C# or TypeScript code.
> **Skip if:** You need implementation-level detail for a specific class → [HOOK_REFERENCE.md](HOOK_REFERENCE.md). You need Avalonia reflection patterns → [AVALONIA_PATTERNS.md](AVALONIA_PATTERNS.md).
> **Does NOT cover:** Per-class implementation detail → [HOOK_REFERENCE.md](HOOK_REFERENCE.md) | Avalonia reflection specifics → [AVALONIA_PATTERNS.md](AVALONIA_PATTERNS.md) | Native profiler internals → [CLR_PROFILER.md](CLR_PROFILER.md)

Authoritative architecture reference for the Uprooted client modification framework. This document describes the system design, layer boundaries, component roles, data flow, conventions, and constraints. For the narrative walkthrough of how it was built, see [How It Works](../HOW_IT_WORKS.md).

> **Related docs:**
> [Index](../INDEX.md) |
> [How It Works](../HOW_IT_WORKS.md) |
> [Hook Reference](HOOK_REFERENCE.md) |
> [TypeScript Reference](TYPESCRIPT_REFERENCE.md) |
> [CLR Profiler](CLR_PROFILER.md) |
> [Installer Reference](INSTALLER.md)

---

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Architecture Diagram](#2-architecture-diagram)
3. [Repository Structure](#3-repository-structure)
4. [Core Abstractions](#4-core-abstractions)
5. [Data Flow -- Multi-Phase Startup](#5-data-flow----multi-phase-startup)
6. [Threading Model](#6-threading-model)
7. [Error Handling](#7-error-handling)
8. [Conventions](#8-conventions)
9. [Critical Rules](#9-critical-rules)
10. [Known Limitations](#10-known-limitations)
11. [Security Considerations](#11-security-considerations)
12. [AI Contributor Guide](#12-ai-contributor-guide) *(→ CLAUDE.md)*

---

## 1. Project Overview

**Uprooted** is a client modification framework for [Root Communications](https://rootapp.com) desktop (v0.9.93+), analogous to [Vencord](https://vencord.dev/) for Discord. It provides a plugin and theme system that hooks into Root's desktop app at runtime, allowing users to customize the UI and intercept internal bridge calls without permanently modifying the application binary.

### What it does

- Injects custom UI (sidebar sections, settings pages, color pickers) into Root's native Avalonia settings panel.
- Proxies Root's internal JavaScript bridge interfaces so plugins can intercept, modify, or cancel bridge method calls.
- Provides a theme engine that overrides Root's Avalonia resource dictionaries and CSS variable system.
- Ships a lightweight console TUI installer (~600KB) that deploys all artifacts and configures CLR profiler injection.
- Self-heals HTML patches when Root auto-updates overwrite them (via `FileSystemWatcher`).
- Maintains a large ILSpy corpus for Root internals (`research/ilspy-dumps/`): 273 decompiled files, with findings distilled into `ROOT_CONTROL_REFERENCE.md` and related docs.

### What it is not

- Not a backend exploit. Uprooted operates entirely on the client side and does not interact with Root's gRPC backend.
- Not a binary patcher (the legacy approach was superseded). The current injection uses CLR profiler IL injection or .NET startup hooks.
- Not yet publicly distributed. The project is awaiting explicit approval from Root's developers before distributing working injection code.

### Dual-layer architecture

Uprooted has two independent injection layers that operate in parallel:

1. **C# .NET hook** (`hook/`) -- Injects into Root's managed .NET 10/Avalonia process via CLR profiler. Adds native Avalonia controls to the settings page sidebar and applies themes via resource dictionary manipulation.
2. **TypeScript browser injection** (`src/`) -- Injects into Root's DotNetBrowser Chromium context (WebRTC bundle and sub-apps — NOT the chat UI, which is Avalonia-native) via HTML `<script>` tags. Provides the plugin runtime, theme CSS engine, and bridge proxy system.

The C# layer handles native UI integration and Avalonia theming. The TypeScript layer handles web content modification and bridge interception. They share no runtime state but target the same application. The only shared state is the settings file on disk and the visual result (both layers inject into the same app).

### Repositories

| Repo | Visibility | Purpose |
|------|------------|---------|
| [`The-Uprooted-Project/uprooted`](https://github.com/The-Uprooted-Project/uprooted) | **Public** | Scaffold, types, documentation, landing page. No working injection code until Root developer approval. |
| [`The-Uprooted-Project/uprooted-private`](https://github.com/The-Uprooted-Project/uprooted-private) | **Private** | Full working codebase: injection code, debug tooling, build artifacts, profiler DLLs, installer, test harnesses. Active development repo. |

Collaborators on `uprooted-private`: `watchthelight` (owner) and `agomusio` (admin). These repos are strictly separate -- never copy, reference, or leak code between them.

---

## 2. Architecture Diagram

```
                         Root.exe (.NET 10, Avalonia UI)
                         ================================
                                       |
               +-----------------------+-----------------------+
               |                                               |
       Native Avalonia UI                          DotNetBrowser (Chromium 144)
               |                                               |
  [CLR Profiler Injection]                          [HTML Script Injection]
  uprooted_profiler.dll (C)                         <script> + <link> tags
               |                                               |
  IL prepended to <Main>$:                          preload.ts runs before
  Assembly.LoadFrom(Hook.dll)                       Root's JavaScript bundles
               |                                               |
       +-------+-------+                              +-------+-------+
       |               |                              |               |
   Entry.cs      NativeEntry.cs                  pluginLoader.ts  bridge.ts
   [ModuleInit]  [hostfxr alt]                   Plugin lifecycle  ES6 Proxy
       |                                              |               |
  StartupHook.Initialize()                     +------+------+   Bridge event
       |                                       |      |      |   interception
  Background thread                         themes  sentry  settings
  Multi-phase startup (0-5)                 plugin  blocker  panel
       |                                       |      |      |
  Phase 0: HtmlPatchVerifier                   +------+------+
       |   (verify + FileSystemWatcher)               |
  Version migration                            Built-in plugins
       |   (force-disable unstable plugins     (UprootedPlugin interface)
       |    on upgrade, stamp version)
  Phase 1: Wait for Avalonia assemblies
       |
  Phase 2: Wait for Application.Current
       |
  Phase 3: Wait for MainWindow
       |   +-- AvaloniaReflection (type cache)
       |   +-- VisualTreeWalker (layout discovery)
       |
  Phase 3.5: ThemeEngine init
       |   (ResourceDictionary overrides: ThemeDictionaries + visual tree walk)
       |
  Phase 4: SidebarInjector.StartMonitoring()
       |   200ms timer poll
       |   +-- Detect settings page open/close
       |   +-- Inject UPROOTED sidebar section
       |   +-- ContentPages (Uprooted, Plugins, Themes)
       |   +-- ColorPickerPopup (custom accent/bg)
       |
  Phase 4.5: BrowserDiscovery (10s delay)
       |   Diagnostic visual tree + assembly scan
       |
  Phase 5: DotNetBrowser features
       |   Event-driven assembly detection
       |   (ManualResetEventSlim, 90s timeout)
       |   +-- DotNetBrowserReflection (type cache)
       |   +-- IBrowser discovery via ViewModel chain
       |   +-- Browser-assisted features (e.g. video thumbnails)
       +-- Phase 4.5b: LinkEmbedEngine (Avalonia-native)
       +-- Phase 4.5g: NsfwFilter (Avalonia-native)
       |
  UI thread dispatch via RunOnUIThread()
```

The two layers are entirely independent at runtime. The C# hook operates within Root's .NET process using reflection to manipulate Avalonia controls. The TypeScript layer operates within DotNetBrowser's Chromium context using standard DOM and JavaScript APIs. Neither layer communicates with the other during execution.

---

## 3. Repository Structure

> Full file tree with descriptions: [`CLAUDE.md` § Repository Structure](../../CLAUDE.md#repository-structure).
> Build commands: [`CLAUDE.md` § Build](../../CLAUDE.md#build).

---

## 4. Core Abstractions

This section describes the key classes and modules that form the framework's backbone, organized by layer. For implementation details, see [Hook Reference](HOOK_REFERENCE.md) and [TypeScript Reference](TYPESCRIPT_REFERENCE.md).

### C# Hook Layer

| Class | File | Role |
|-------|------|------|
| `Entry` | `hook/Entry.cs` | Profiler injection entry point. `[ModuleInitializer]` and constructor both guarded by `Interlocked.CompareExchange` for one-time initialization. Calls `StartupHook.Initialize()`. |
| `StartupHook` | `hook/StartupHook.cs` | Initialization orchestrator. Process name guard, background thread, multi-phase startup (Phase 0-5). Coordinates Avalonia injection and DotNetBrowser feature loading. |
| `HtmlPatchVerifier` | `hook/HtmlPatchVerifier.cs` | Self-healing HTML patches. Runs at Phase 0 (no Avalonia needed). Verifies injection markers exist in profile HTML files. Sets up `FileSystemWatcher` to re-patch when Root auto-updates overwrite files. |
| `AvaloniaReflection` | `hook/AvaloniaReflection.cs` | Reflection cache for Avalonia types. Scans loaded assemblies filtered by `"Avalonia"` prefix, builds type dictionary, caches `PropertyInfo`/`MethodInfo` handles. All control creation and property manipulation flows through this class. The most critical file in the codebase (2920 lines). |
| `VisualTreeWalker` | `hook/VisualTreeWalker.cs` | Settings page layout discovery. Walks the Avalonia visual tree to find the "APP SETTINGS" anchor text, then discovers the nav container, layout grid, content area, ListBox, and back button by structural analysis. |
| `SidebarInjector` | `hook/SidebarInjector.cs` | Settings page monitor. 200ms timer polls the visual tree, detects settings page open/close, injects the UPROOTED sidebar section, manages content page overlays, handles click events and cleanup. |
| `ContentPages` | `hook/ContentPages.cs` | Page builders for the Uprooted, Plugins, and Themes settings pages. Creates native Avalonia controls via reflection, matching Root's exact card styling (background, corner radius, border, padding, typography). |
| `ThemeEngine` | `hook/ThemeEngine.cs` | Native Avalonia theme engine (v2, resource-first). Applies overrides in `Application.Resources.ThemeDictionaries[variant]` for Root's 32 keys, supports named themes and custom accent/background, custom ping color override, variant switching, and full revert. |
| `ColorPickerPopup` | `hook/ColorPickerPopup.cs` | HSL color picker popup for selecting custom theme accent and background colors. Presented as an overlay on the Themes settings page. |
| `ColorUtils` | `hook/ColorUtils.cs` | Color parsing, HSL conversion, luminance calculation, and contrast ratio computation. Used by `ThemeEngine` and `ContentPages`. |
| `UprootedSettings` | `hook/UprootedSettings.cs` | INI-based settings persistence. Loads and saves key=value pairs from `uprooted-settings.ini` in the profile directory. Uses INI format instead of JSON because `System.Text.Json` is broken in the profiler-injected context. |
| `PlatformPaths` | `hook/PlatformPaths.cs` | Cross-platform path resolution. Returns profile directory and Uprooted install directory for Windows and Linux. |
| `DotNetBrowserReflection` | `hook/DotNetBrowserReflection.cs` | Reflection cache for DotNetBrowser types (IBrowser, IFrame, IEngine). Discovers IBrowser via ViewModel chain walking. Provides `ExecuteJavaScript` wrapper. |
| `BrowserDiscovery` | `hook/BrowserDiscovery.cs` | Phase 4.5 diagnostic scanner. Dumps full visual tree structure and loaded assemblies after 10s delay to aid in architecture discovery. |
| `LinkEmbedEngine` | `hook/LinkEmbedEngine.cs` | Avalonia-native link embed engine. Scans visual tree for CTextBlock nodes with URLs, fetches OG/oEmbed metadata via reflection-based HTTP, and builds native embed cards (including direct image fast path, Reddit, and video placeholders). |
| `AnimatedImage` | `hook/AnimatedImage.cs` | Animated GIF/WebP decoder using SkiaSharp `SKCodec` reflection. Extracts frames with disposal method handling, per-frame delay timers, and automatic cleanup on card removal. Falls back to static first frame if frame APIs unavailable. |
| `MessageLogger` | `hook/MessageLogger.cs` | Message logger plugin (WIP). DateTimeOffset property fixes (`DeletedAt`/`EditedAt`), INPC event-driven detection, self-delete fallback via collection presence checks, author name resolution, dedup, visual indicators, and `MessageStore` persistence. |
| `MessageStore` | `hook/MessageStore.cs` | Flat-file persistence for message log data. Pipe-delimited format with URI-encoded fields, append-only writes via buffered flush timer, startup truncation for retention limits. |
| `AutoUpdater` | `hook/AutoUpdater.cs` | In-process auto-updater with two channels. **Stable** checks `The-Uprooted-Project/uprooted` (public) via `/releases/latest`. **Developer** checks `The-Uprooted-Project/uprooted-private` (private) via `/releases?per_page=1` (includes pre-releases) with XOR-encrypted PAT for auth. Downloads encrypted `.uprpkg` package, decrypts (multi-layer XOR: 64-byte master key + 32-byte nonce + position-dependent derivation), unpacks 6 files to staging, verifies, overwrites in-place. Hash-based same-version hotfix detection on manual checks. HTTP via reflection (trimming-safe). See [Hook Reference § AutoUpdater](HOOK_REFERENCE.md#autoupdater-update-channels-and-package-system). |
| `ClearUrlsEngine` | `hook/ClearUrlsEngine.cs` | Strips tracking parameters (utm_*, fbclid, gclid, etc.) from URLs in the compose editor on send. Hooks AvaloniaEdit TextArea via routed events with `handledEventsToo: true`. |
| `ProfileBadgeInjector` | `hook/ProfileBadgeInjector.cs` | Injects "Uprooted Dev" badge into profile popups when update channel is Developer. Event-driven OverlayLayer detection + 500ms fallback poll (1130 lines). |
| `NsfwFilter` | `hook/NsfwFilter.cs` | Avalonia-native NSFW content filter. 500ms scan timer DFS-walks visual tree for `Image` controls, classifies via Vision API with `SemaphoreSlim(3)` concurrency cap, hides NSFW images with overlay (click-to-reveal). Phase 4.5g, no DotNetBrowser dependency. |
| `TranslateEngine` | `hook/TranslateEngine.cs` | DeepL-powered message translation (Phase 4.5i). Scans visual tree for CTextBlock nodes, adds translate context menu items, fetches translations via DeepL API (1145 lines). |
| `TranslateConfigPopup` | `hook/TranslateConfigPopup.cs` | Translate configuration popup UI — language picker, API key input, test connection button (511 lines). |
| `UprootedPresenceBeacon` | `hook/UprootedPresenceBeacon.cs` | Presence beacon for Uprooted user detection via gRPC metadata injection (Phase 4.5j, 470 lines). |
| `ReconLogger` | `hook/ReconLogger.cs` | Visual tree + style property diagnostic dumper for development/debugging (786 lines). |
| `Logger` | `hook/Logger.cs` | Thread-safe buffered file logging (~170 lines). Writes to `uprooted-hook.log` in the profile directory. Supports two formats: classic `[Category] message` and structured wide events `[Category|operation] key=value dur_ms=N`. Buffered I/O (100ms flush timer). `OnLine` callback for live subscribers (used by `LogConsole`). Swallows its own exceptions to avoid crashing Root. |
| `WideEvent` | `hook/WideEvent.cs` | Structured wide event builder (~150 lines). `IDisposable` — accumulates key-value fields during an operation, emits a single log line on `Dispose()` with automatic `dur_ms`. Supports tail sampling via `TailSampler` for high-frequency operations, parent-child linking, error tracking. |
| `TailSampler` | `hook/TailSampler.cs` | Tail sampling for high-frequency scan ticks (~72 lines). Controls when sampled `WideEvent`s emit: on error, slow duration, notable work, or periodic heartbeat. |
| `LogConsole` | `hook/LogConsole.cs` | Dev-only live log terminal (~200 lines). Spawns a system console window connected via named pipe (`\\.\pipe\uprooted-log-console`). Streams live log output with color coding, 200-line backfill on connect. Dev channel only, live toggle. |

### TypeScript Browser Layer

| Module | File | Role |
|--------|------|------|
| `preload` | `src/core/preload.ts` | Browser entry point. Reads settings from `window.__UPROOTED_SETTINGS__`, installs bridge proxies, creates `PluginLoader`, registers built-in plugins, starts enabled ones. |
| `pluginLoader` | `src/core/pluginLoader.ts` | Plugin lifecycle manager. Handles register/start/stop, routes bridge events to patch handlers, manages CSS injection/removal per plugin. |
| `patcher` | `src/core/patcher.ts` | HTML injection engine. Inserts `<script>` and `<link>` tags into Root's profile HTML files. Creates `.uprooted.bak` backups. Uses `<!-- uprooted -->` comment markers for detection. |
| `settings` | `src/core/settings.ts` | File-based settings persistence (Node.js/CLI only). Reads/writes `uprooted-settings.json` in Root's profile directory. |
| `bridge` | `src/api/bridge.ts` | ES6 Proxy wrappers for `window.__nativeToWebRtc` and `window.__webRtcToNative`. Intercepts all method calls and routes to plugin patch handlers. Deferred installation via `Object.defineProperty` traps. |
| `css` | `src/api/css.ts` | Inject/remove `<style>` elements by ID. Used for plugin CSS lifecycle. |
| `dom` | `src/api/dom.ts` | DOM utilities: `waitForElement()`, `observe()`, `nextFrame()`. MutationObserver-based element waiting. |
| `native` | `src/api/native.ts` | CSS variable get/set, native bridge logging via `nativeLog()`. |

### Built-in Plugins

| Plugin | Directory | Role |
|--------|-----------|------|
| `sentry-blocker` | `src/plugins/sentry-blocker/` | Privacy plugin. Wraps `fetch`, `XMLHttpRequest.open`, and `navigator.sendBeacon` to block requests to Sentry telemetry endpoints. |
| `themes` | `src/plugins/themes/` | CSS variable theme engine. Reads theme definitions from `themes.json`, applies `--rootsdk-*` variable overrides via `document.documentElement.style.setProperty()`. |
| `settings-panel` | `src/plugins/settings-panel/` | Web-side settings UI. Injects UPROOTED section into Root's browser-rendered settings sidebar using DOM manipulation and MutationObserver. |

### Plugin Interface

Every plugin implements the `UprootedPlugin` interface (defined in `src/types/plugin.ts`):

```typescript
interface UprootedPlugin {
  name: string;           // Unique identifier, matches directory name
  description: string;
  version: string;
  authors: Author[];
  start?(): void | Promise<void>;   // Called when enabled
  stop?(): void | Promise<void>;    // Called when disabled
  patches?: Patch[];                // Bridge method intercepts
  css?: string;                     // CSS injected while active
  settings?: SettingsDefinition;    // Plugin-specific config schema
}
```

### Patch Interface

Patches intercept calls on Root's bridge objects (defined in `src/types/plugin.ts`):

```typescript
interface Patch {
  bridge: "nativeToWebRtc" | "webRtcToNative";
  method: string;
  before?(args: unknown[]): boolean | void;   // Return false to cancel
  replace?(...args: unknown[]): unknown;       // Replace entirely
  after?(result: unknown, args: unknown[]): void;  // Post-execution (not yet implemented)
}
```

See [Plugin API Reference](../plugins/API_REFERENCE.md) for the full plugin development API and [Bridge Reference](../plugins/BRIDGE_REFERENCE.md) for the bridge method catalog.

---

## 5. Data Flow -- Multi-Phase Startup

The C# hook uses a phased startup sequence (Phase 0 through Phase 5) to safely initialize within Root's .NET process without blocking application startup. Each phase has a timeout and will abort gracefully if the required condition is not met. Source: `hook/StartupHook.cs`.

For the full implementation walkthrough, see [Hook Reference](HOOK_REFERENCE.md).

### Phase 0: HTML Patch Verification

**Purpose:** Ensure HTML injection markers are present in Root's profile HTML files before the browser context loads.

- **File:** `hook/HtmlPatchVerifier.cs`
- **Thread:** Background (no Avalonia needed)
- **Action:** Scans `WebRtcBundle/index.html` and `RootApps/*/index.html` for injection markers. If markers are missing (e.g., Root auto-update overwrote them), re-patches the files in place. Creates a `FileSystemWatcher` for each directory to detect future overwrites and re-patch automatically.
- **Failure mode:** Non-fatal. If Phase 0 fails, the TypeScript layer will not load, but the C# hook continues.

### Version Migration (between Phase 0 and Phase 1)

**Purpose:** Force-disable unstable plugins when upgrading to a new build.

- **File:** `hook/StartupHook.cs`
- **Thread:** Background
- **Mechanism:** Compares `CurrentVersion` const against `settings.Version` using `AutoUpdater.CompareVersions()`. On upgrade, iterates the `ForceDisableOnUpgrade` dictionary and sets `Plugins[id] = false` for each plugin listed in versions between old (exclusive) and current (inclusive). On downgrade, stamps version only. No-op when versions match.
- **Failure mode:** Non-fatal. Settings update only; no UI or Avalonia dependency.

### Phase 1: Wait for Avalonia Assemblies

**Purpose:** Wait for Root's Avalonia UI framework to load into the process.

- **Thread:** Background
- **Timeout:** 30 seconds, polling every 250ms
- **Condition:** `Avalonia.Controls` assembly found in `AppDomain.CurrentDomain.GetAssemblies()`
- **On success:** Proceeds to type resolution. Creates `AvaloniaReflection` instance and calls `Resolve()` to scan all Avalonia assemblies and cache type/property/method handles.
- **On failure:** Logs error and returns. No UI injection will occur.

### Phase 2: Wait for Application.Current

**Purpose:** Wait for Root to initialize its Avalonia `Application` singleton.

- **Thread:** Background
- **Timeout:** 30 seconds, polling every 500ms
- **Condition:** `AvaloniaReflection.GetAppCurrent()` returns non-null
- **On failure:** Logs error and returns.

### Phase 3: Wait for MainWindow

**Purpose:** Wait for Root to create and display its main window.

- **Thread:** Background
- **Timeout:** 60 seconds, polling every 500ms
- **Condition:** `AvaloniaReflection.GetMainWindow()` returns non-null (accessed via `Application.Current.ApplicationLifetime.MainWindow`)
- **On failure:** Logs error and returns.

### Phase 3.5: Theme Engine Initialization

**Purpose:** Apply the user's saved theme before the settings page is opened.

- **Thread:** UI thread (dispatched via `RunOnUIThread()`)
- **File:** `hook/ThemeEngine.cs`
- **Action:** Creates `ThemeEngine` instance, loads settings from `uprooted-settings.ini`, applies saved theme (named theme or custom accent/background). ThemeEngine v2 is resource-first: it writes overrides into `Application.Resources.ThemeDictionaries[variant]` (Root's 32 keys like `BrandPrimary`, `TextPrimary`, etc.) and keeps custom ping color overrides in sync — see [`research/ROOT_THEME_SYSTEM_FINDINGS.md`](../../research/ROOT_THEME_SYSTEM_FINDINGS.md).
- **Failure mode:** Non-fatal. Theme errors are caught and logged; startup continues.

### Phase 4: Settings Page Monitoring

**Purpose:** Begin monitoring Root's visual tree for the settings page, ready to inject the UPROOTED section when the user opens Settings.

- **File:** `hook/SidebarInjector.cs`
- **Action:** Creates `SidebarInjector` with references to `AvaloniaReflection`, the main window, and the `ThemeEngine`. Calls `StartMonitoring()` which uses `LayoutUpdated` detection with a throttled fast path plus a 200ms timer safety poll; work dispatches to the UI thread and injects/removes the sidebar section as needed.
- **Ongoing:** This phase runs for the lifetime of the process.

### Phase 4.5: Browser Discovery (Diagnostic)

**Purpose:** Dump visual tree structure and loaded assemblies for architecture discovery and debugging.

- **File:** `hook/BrowserDiscovery.cs`
- **Thread:** Background (10-second delay after Phase 4)
- **Action:** Performs a full DFS visual tree dump of the main window and scans all loaded assemblies. Results are logged for diagnostic purposes.
- **Failure mode:** Non-fatal. Purely informational.

### Phase 4.5b: Native Link Embeds

**Purpose:** Start the Avalonia-native link embed engine after chat has populated.

- **File:** `hook/StartupHook.cs`, `hook/LinkEmbedEngine.cs`
- **Thread:** Background (15s delay for chat to populate, then 500ms poll timer)
- **Mechanism:** `LinkEmbedEngine` scans the visual tree for `CTextBlock` / `RootMarkdownTextBlock` nodes containing URLs, fetches OG metadata via reflection-based HttpClient, and injects native Avalonia embed cards below messages.
- **Status:** Broadly functional: YouTube/oEmbed, direct image fast path, animated GIF/WebP, Reddit provider labeling, Twitter/X embed-fixer normalization, and extension or content-type detected video placeholders.

### Phase 4.5g: Avalonia-Native NSFW Filter

**Purpose:** Start NSFW image classification/blurring in the native Avalonia message tree.

- **File:** `hook/StartupHook.cs`, `hook/NsfwFilter.cs`
- **Thread:** Background (deferred startup delay), work dispatched to UI as needed
- **Mechanism:** DFS visual tree scan for `Image` controls, classify with Vision API (bounded concurrency), blur with click-to-reveal overlay.
- **Status:** Avalonia-native redesign is deployed; production validation is still pending.

### Phase 4.5h: Rootcord Engine

**Purpose:** Start the Discord-style vertical server sidebar replacement (experimental).

- **File:** `hook/RootcordEngine.cs`
- **Thread:** Background (8s delay, dormant if disabled in settings)
- **Status:** Experimental. Deployed with crash guards, awaiting validation.

### Phase 4.5i: Translate Engine

**Purpose:** Start the DeepL-powered message translation engine.

- **File:** `hook/TranslateEngine.cs`, `hook/TranslateConfigPopup.cs`
- **Thread:** Background (deferred startup), UI dispatch for context menu injection
- **Status:** Deployed, awaiting validation.

### Phase 4.5j: Presence Beacon

**Purpose:** Start Uprooted user detection via gRPC metadata.

- **File:** `hook/UprootedPresenceBeacon.cs`
- **Thread:** Background
- **Status:** Deployed, awaiting validation.

### Phase 5: DotNetBrowser Feature Loading

**Purpose:** Detect DotNetBrowser assemblies and initialize browser-dependent features.

- **File:** `hook/StartupHook.cs`, `hook/DotNetBrowserReflection.cs`
- **Thread:** Background
- **Detection:** Event-driven via `AppDomain.CurrentDomain.AssemblyLoad` event + `ManualResetEventSlim`. Waits for an assembly matching `DotNetBrowser` or `DotNetBrowser.Core` (NOT `DotNetBrowser.AvaloniaUi`, which Root does not ship).
- **Timeout:** 90 seconds
- **On success:** Resolves DotNetBrowser types via `DotNetBrowserReflection`, discovers `IBrowser` via ViewModel chain walking (`MainWindow.DataContext` → `BrowserService` → `BrowserEngineManager` → `IEngine` → `Profiles[0].Browsers._values`), then enables browser-assisted features.

### TypeScript Startup (Independent)

The TypeScript layer has its own startup sequence, independent of the C# phases:

1. Patcher inserts `<script>` and `<link>` tags into profile HTML files (at install time, before `</head>`).
2. On page load, `preload.ts` runs before Root's JavaScript bundles.
3. Reads settings from `window.__UPROOTED_SETTINGS__` (inlined by patcher).
4. Installs ES6 Proxy wrappers on `window.__nativeToWebRtc` and `window.__webRtcToNative`.
5. Creates `PluginLoader`, registers built-in plugins, starts enabled ones in order.
6. Plugins start: sentry-blocker (network intercepts) -> themes (CSS variables) -> settings-panel (DOM injection).

See [TypeScript Reference](TYPESCRIPT_REFERENCE.md) for details.

---

## 6. Threading Model

### C# Hook Threading

The C# hook must operate within Root's .NET process without blocking the application. Three thread contexts are used:

**Background thread (`Uprooted-Injector`):**
- Spawned in `StartupHook.Initialize()` with `IsBackground = true`.
- Runs the multi-phase startup sequence (Phase 0 through Phase 5).
- All polling waits (`WaitForAvaloniaAssemblies`, `WaitFor`) happen here.
- Never touches Avalonia controls directly.

**UI thread (`Dispatcher.UIThread`):**
- All Avalonia control creation, property manipulation, and visual tree traversal must happen on the UI thread.
- Access via `AvaloniaReflection.RunOnUIThread(Action)`, which calls `Dispatcher.UIThread.Post(action, DispatcherPriority.Normal)`.
- `DispatcherPriority` is a struct (not an enum) in Avalonia 11+ -- resolved via a fallback chain: static property `.Normal` -> static field -> `Enum.Parse` -> `Activator.CreateInstance`.

**Timer thread -> UI dispatch:**
- `SidebarInjector.StartMonitoring()` creates a `System.Threading.Timer` with a 200ms period.
- The timer callback runs on a thread pool thread, but immediately dispatches all work to the UI thread via `RunOnUIThread()`.
- An `_injecting` interlocked flag prevents concurrent `CheckAndInject` calls if a timer tick fires before the previous one completes.
- An `_ourItemClicked` flag coordinates between `PointerPressed` event handlers on injected controls and Root's ListBox selection logic.

### TypeScript Threading

The TypeScript layer is single-threaded (browser main thread):

- All plugin `start()` methods are called sequentially with `await` in a loop.
- `MutationObserver` callbacks in the settings panel are debounced (80ms) to prevent rapid re-injection during DOM mutations.
- Bridge proxy intercepts are synchronous -- event handlers run inline within the proxied method call.
- DotNetBrowser's Chromium runs in its own process, but the JavaScript context is single-threaded per standard browser behavior.

### Thread Safety Patterns

| Pattern | Where | Mechanism |
|---------|-------|-----------|
| One-time init guard | `hook/Entry.cs` | `Interlocked.CompareExchange(ref _initialized, 1, 0)` |
| Concurrent injection guard | `hook/SidebarInjector.cs` | `Interlocked.CompareExchange(ref _injecting, 1, 0)` |
| Click coordination | `hook/SidebarInjector.cs` | `_ourItemClicked` volatile flag between PointerPressed and ListBox events |
| Patch cooldown | `hook/HtmlPatchVerifier.cs` | `DateTime` comparison + `Interlocked` guard + debounce timer |
| Logger thread safety | `hook/Logger.cs` | `lock` around `StringBuilder` buffer, 100ms flush timer, eager flush at 8 KB |

---

## 7. Error Handling

### Design Principle

Never throw from injected code. Both layers prioritize Root's stability over error reporting. If Uprooted fails, Root must continue running normally.

### C# Hook Error Handling

**Profiler IL injection:** The IL prepended to `<Main>$` is wrapped in try/catch. If `Assembly.LoadFrom` or `CreateInstance` fails, the exception is silently caught and Root starts normally. See [CLR Profiler](CLR_PROFILER.md).

**Phase failure recovery:** Each startup phase has a timeout. If a phase fails:
- Phase 0 (HTML patches): Non-fatal warning logged, continues to Phase 1.
- Phases 1-3: Fatal -- logs error and returns from the background thread. No UI injection occurs, but Root runs unaffected.
- Phase 3.5 (themes): Non-fatal -- catches exception, logs, and continues to Phase 4.
- Phase 4 (monitoring): Outer try/catch around the entire `InjectorLoop`. Fatal errors are logged.

**Reflection null safety:** All reflection calls in `AvaloniaReflection` handle null returns. If a type, property, or method is not found, the caller receives null and must handle it gracefully. Control creation methods (e.g., `CreateTextBlock`, `CreateStackPanel`) return `null` on failure.

**Logger resilience:** `Logger.Log()` swallows its own exceptions. A logging failure never propagates to the caller.

**Self-healing patches:** `HtmlPatchVerifier` detects when Root auto-updates overwrite patched HTML files. The `FileSystemWatcher` re-patches files automatically with a 2-second cooldown to avoid rapid-fire re-patching.

### TypeScript Error Handling

**Preload entry:** Top-level try/catch wraps the entire bootstrap. Fatal errors are displayed as a red banner at the top of the page with the error message and stack trace.

**Plugin lifecycle:** Plugin `start()` and `stop()` errors are caught individually. One plugin failing does not prevent others from starting.

**Bridge events:** Handler errors within the proxy are caught and logged. A failing handler does not interrupt the event chain or prevent the original bridge call.

**Settings fallback:** If `window.__UPROOTED_SETTINGS__` is missing or malformed, the runtime falls back to `DEFAULT_SETTINGS` (defined in `src/types/settings.ts`).

### Debugging

| Layer | Primary tool | Location |
|-------|-------------|----------|
| C# hook | Log file | `%LOCALAPPDATA%/Root Communications/Root/profile/default/uprooted-hook.log` |
| C# hook | Named pipe dev console | `\\.\pipe\uprooted-log-console` — `LogConsole.cs` spawns a live terminal window (dev channel only) |
| TypeScript | Error banner | Red `#uprooted-error` div at page top on fatal errors |
| TypeScript | Debug overlay | Green `#uprooted-debug` div at page bottom (when `DEBUG = true` in `panel.ts`) |
| Profiler | Log file | `%LOCALAPPDATA%/Root/uprooted/profiler.log` |

Root's Chromium has no DevTools (`--remote-debugging-port` is not available). Console output goes to DotNetBrowser's internal console (not externally accessible). The `nativeLog()` API sends messages through the bridge to .NET logging.

---

## 8. Conventions

### Naming

**TypeScript files:** camelCase module files (`pluginLoader.ts`, `preload.ts`). Kebab-case for plugin directories (`sentry-blocker/`, `settings-panel/`). Index files for barrel exports and plugin entry points.

**TypeScript identifiers:** camelCase for functions and variables (`loadSettings`, `activePlugins`). PascalCase for types and interfaces (`UprootedPlugin`, `BridgeEvent`). UPPER_SNAKE_CASE for constants (`BACKUP_SUFFIX`, `INJECTION_MARKER`). Branded string types for semantic distinction (`UserGuid`, `DeviceGuid`).

**C# files:** PascalCase class names matching file names (`AvaloniaReflection.cs`, `SidebarInjector.cs`).

**C# identifiers:** PascalCase for public/internal members. `_camelCase` for private fields. `s_camelCase` for static fields. `Uprooted` namespace for most classes; `UprootedHook` namespace for `Entry.cs`; global namespace for `StartupHook` (required by .NET startup hooks).

### File Organization

- **`src/api/`**: Stable public APIs for plugin authors. General-purpose, not plugin-specific.
- **`src/core/`**: Internal runtime infrastructure. Entry points, lifecycle management, persistence.
- **`src/types/`**: Shared TypeScript type definitions. No runtime code.
- **`src/plugins/`**: Self-contained plugins. Each directory contains at minimum `index.ts` with a default export satisfying `UprootedPlugin`.
- **`hook/`**: All code that runs inside Root's .NET process. Avalonia interaction, reflection, settings.
- **`scripts/`**: Build, install, and utility scripts. Not deployed to production.
- **`tools/`**: Binary tools and their source. Compiled artifacts and build helpers.

### Import Style (TypeScript)

- `import type` for type-only imports: `import type { UprootedPlugin } from "../types/plugin.js";`
- All imports include `.js` extension (ES modules).
- `@uprooted/*` path alias maps to `src/*` (defined in `tsconfig.json`).
- Barrel exports via namespace: `export * as css from "./css.js";`

### Access Modifiers (C#)

- `internal` for all non-entry classes. `public` only for `Entry` (must be accessible for `CreateInstance`).
- Nullable enabled project-wide.
- Implicit usings enabled.

### Commit Format

```
type: concise description of what changed and why
```

Types: `fix`, `feat`, `refactor`, `docs`, `chore`, `style`.

Examples:
- `fix: self-heal HTML patches after Root auto-update overwrites`
- `feat: add Phase 0 startup verification for HTML patches`
- `refactor: prefer in-place stripping over stale backup restore`

### Logging

| Layer | Format | Destination |
|-------|--------|-------------|
| TypeScript | `[Uprooted] message` or `[Uprooted:plugin-name] message` | Browser console (DotNetBrowser internal) |
| C# hook (classic) | `[HH:mm:ss.fff] [Category] message` | `uprooted-hook.log` in profile directory |
| C# hook (wide event) | `[HH:mm:ss.fff] [Category|operation] key=value dur_ms=N` | `uprooted-hook.log` in profile directory |

### Element Identification

**C# (Avalonia):** Injected controls use `Control.Tag` strings prefixed with `"uprooted-"`:
- `"uprooted-injected"` -- The sidebar section container
- `"uprooted-content"` -- Active content page overlay
- `"uprooted-version"` -- Version text
- `"uprooted-item-{page}"` -- Individual nav items
- `"uprooted-highlight-{page}"` -- Nav item highlight overlays

**TypeScript (DOM):** Injected elements use `data-uprooted` attributes:
- `data-uprooted="section"` -- Sidebar section container
- `data-uprooted="header"` -- Section header
- `data-uprooted="item"` -- Nav item
- `data-uprooted-page="..."` -- Page identifier
- `data-uprooted="content"` -- Content panel
- `data-uprooted="version"` -- Version text

### Content Overlay Pattern (C# Settings Pages)

The injector does NOT modify Root's content panel directly. Instead:

1. The Uprooted page is added as a new child of the settings Grid.
2. `Grid.Column` and `Grid.Row` are set to match the content area's position.
3. An opaque background (`#0D1521`) ensures the Uprooted page covers Root's content via z-order.
4. When the user clicks a Root sidebar item, the overlay is removed from the Grid.

This avoids the UI freeze that occurs when modifying `ContentControl.Content` or `ScrollContentPresenter.Content` directly.

### Styling Invariants (C# Content Pages)

Root's settings page uses exact styling values. The C# `ContentPages` class replicates these:

| Element | Value |
|---------|-------|
| Card background | `#081408` |
| Card corner radius | 12 |
| Card border | `#19ffffff`, thickness 0.5 |
| Card inner padding | 24px all sides |
| Page container margin | 24, 24, 24, 0 |
| Title text | FontSize=14, Bold, `#fff2f2f2` |
| Label text | FontSize=13, Weight=450, `#a3f2f2f2` |
| Body text | FontSize=13, Normal, `#a3f2f2f2` |
| Accent green | `#2D7D46` (Uprooted brand) |
| Accent blue | `#3b6af8` (Root brand) |

*Note: Card background (`#081408`) and border (`#19ffffff`) are Uprooted-specific values. Root's own settings cards use its resource keys: `BackgroundSecondary` for card backgrounds, `Border` for borders, `TextPrimary`/`TextSecondary` for text. See [`research/ROOT_THEME_SYSTEM_FINDINGS.md`](../../research/ROOT_THEME_SYSTEM_FINDINGS.md) for Root's actual key-color mappings.*

---

## 9. Critical Rules

> **Compact list:** [`CLAUDE.md` § Critical Rules](../../CLAUDE.md#critical-rules). Full explanations with code examples below.

These rules reflect hard-won lessons from real bugs. Violating them causes crashes, freezes, or silent failures.

### C# Hook Rules

1. **Never use `Type.GetType()` for Avalonia types.** Root.exe is a single-file .NET 10 binary. Assembly-qualified type names are not resolvable in this context. Always use `AvaloniaReflection` to access Avalonia types, properties, and methods.

2. **Never modify `ContentControl.Content` directly.** Setting or replacing the `Content` property on a `ContentControl` or `ScrollContentPresenter` causes the Avalonia UI to freeze permanently. Use the Grid overlay pattern instead (add a sibling element with matching Grid.Column/Row and an opaque background).

3. **Never use `System.Text.Json` in the hook.** Calling `System.Text.Json` serialization or deserialization in the profiler-injected context causes `MissingMethodException`. Use the INI-based `UprootedSettings` class or manual parsing instead.

4. **Never use `EventInfo.AddEventHandler` for RoutedEvents.** Avalonia's `RoutedEvent` system is not compatible with the standard CLR event subscription pattern. Use `AvaloniaReflection.SubscribeEvent()`, which compiles `Expression.Lambda` delegates matching the exact event handler signature.

5. **`DispatcherPriority` is a struct, not an enum, in Avalonia 11+.** Do not use `Enum.Parse` to resolve it. `AvaloniaReflection.RunOnUIThread()` handles this with a fallback chain: static property `.Normal` -> static field -> `Enum.Parse` -> `Activator.CreateInstance`.

6. **Never block the UI thread.** All heavy work (assembly scanning, type resolution, waiting) runs on the background thread. UI mutations dispatch to `Dispatcher.UIThread` via `RunOnUIThread()`.

7. **Never add children directly to a `VirtualizingStackPanel`.** Items in a virtualizing container get recycled. Add to the parent container instead.

8. **Call `ClearValue(AvaloniaProperty)` to restore bindings after `SetValue`.** `SetValue` overrides data bindings permanently. The only way to re-enable them is `ClearValue`.

### TypeScript Rules

9. **Never use `localStorage` for persistent state.** Root runs Chromium with `--incognito`, which wipes localStorage on every launch. Use file-based persistence (settings JSON inlined by the patcher).

10. **Never force-push to main.** Both collaborators work on the same branch. Force-pushing destroys the other person's commits.

### General Rules

11. **Plugin CSS must be namespaced by plugin name.** Use `injectCss("plugin-{name}", css)` to create `<style id="uprooted-css-plugin-{name}">`. This prevents collisions and enables per-plugin cleanup.

12. **All injected elements must be identifiable.** C# controls use `Control.Tag` strings starting with `"uprooted-"`. DOM elements use `data-uprooted` attributes. These are the cleanup handles for removal.

13. **When replicating Root UI elements, ALWAYS examine Root's decompiled source first.** Root's .NET assemblies are fully decompilable via ILSpy — decompiled sources live in `research/ilspy-dumps/`. Don't reinvent controls from scratch when Root already has working implementations. Root's custom types (`RootScrollViewer`, `RootBorder`, `RootSvgImage`, etc.) can be found at runtime via `AppDomain.CurrentDomain.GetAssemblies()` and instantiated with `Activator.CreateInstance` — their styles and templates are already loaded in the app.

---

## 10. Known Limitations

### Settings Persistence (C# Hook)

`System.Text.Json` is broken in the profiler-injected context (`MissingMethodException`). The hook uses an INI-based format (`uprooted-settings.ini`) with manual key=value parsing. This works for simple settings (theme name, accent color, plugin toggles) but does not support nested structures.

The TypeScript layer uses a separate JSON-based settings file (`uprooted-settings.json`) inlined into HTML by the patcher. Runtime changes made via the browser-side settings panel only update the in-memory `window.__UPROOTED_SETTINGS__` object -- they do not persist to disk because the browser has no file system access in Root's incognito Chromium. Settings written by the installer/CLI do persist.

### Display-Only Features

- **Theme switching in C# pages:** The Themes page shows available themes with "ACTIVE" badges and has click handlers for theme switching and a color picker for custom themes. Theme selection persists via the INI settings file.
- **Plugin management in C# pages:** The Plugins page lists plugins with toggle switches, but the toggles are display-only and cannot actually enable/disable plugins at runtime.
- **Custom CSS injection:** Only available in the TypeScript layer (session-only). The C# page shows "will be available in a future update."

### Profiler Injection Scope

The CLR profiler environment variables are set as user-scoped environment variables using both `DOTNET_` (primary, for .NET 10+) and `CORECLR_` (legacy, for .NET 8/9) prefixes. They affect ALL .NET applications the user launches, not just Root. The profiler DLL has a process name guard (checks for "Root" and returns `E_FAIL` for other processes), so other apps see minimal overhead (profiler loads, checks name, detaches). However, this is a known limitation.

### Fragile Integration Points

| Integration | Fragility | Impact if broken |
|-------------|-----------|------------------|
| "APP SETTINGS" text anchor | Root renames the text | Settings page injection silently fails |
| Settings Grid column/row layout | Root restructures the settings page | Content overlay positioned incorrectly |
| `--rootsdk-*` CSS variables | Root renames variables | Themes apply to wrong elements or have no effect |
| `<Main>$` JIT compilation | Root ships with AOT | Profiler IL injection fails entirely |
| `__nativeToWebRtc` / `__webRtcToNative` globals | Root renames or freezes them | Bridge proxy cannot intercept IPC |
| Profile HTML file paths | Root moves to different directory | HTML patching cannot find target files |
| DotNetBrowser as embedded browser | Root switches browser engine | TypeScript layer entirely incompatible |

### Chat is Avalonia-Native (Not Browser-Rendered)

Root's chat UI, channel lists, and community views are rendered entirely in native Avalonia controls (1647+ visual tree nodes, 0 browser controls). DotNetBrowser is auxiliary -- used for WebRTC voice/video, OAuth flows, and sub-app iframes (polls, task tracker, etc.). The shell page at `rootapp://app/__index.html` contains an `<iframe id="app-iframe">` that permanently stays at `about:blank`.

Link embeds are Avalonia-native (`LinkEmbedEngine.cs`, Phase 4.5b): the engine scans the visual tree for URL-containing CTextBlocks and injects native embed cards. `NsfwFilter` is also Avalonia-native now (`NsfwFilter.cs`, Phase 4.5g), with visual-tree image classification and blur/reveal overlays.

- **Link embeds:** `hook/LinkEmbedEngine.cs` — Avalonia-native, visual tree scan + OG/oEmbed + native card injection
- **NSFW filter:** `hook/NsfwFilter.cs` — Avalonia-native, visual tree image interception + blur/reveal (validation in progress)

### `after` Patch Handler Not Implemented

The `after` callback is defined in the `Patch` interface but is not yet invoked by the `PluginLoader`. Plugins defining `after` handlers currently get no runtime behavior.

---

## 11. Security Considerations

### Process Boundary

The C# profiler DLL includes a process name guard. On `ICorProfilerCallback::Initialize`, it checks if the process name is "Root". If not, it returns `E_FAIL` and the profiler detaches immediately. This prevents the hook from activating in unrelated .NET applications.

### Environment Variable Cleanup

The `Uninstall-Uprooted.ps1` and `uninstall-uprooted-linux.sh` scripts remove all CLR profiler environment variables. The installer's uninstall function also removes registry entries (Windows) or shell profile entries (Linux) and broadcasts `WM_SETTINGCHANGE`.

### No Credential Storage

Uprooted does not store, intercept, or exfiltrate any credentials. It does not interact with Root's gRPC backend or authentication system. Bridge proxies can observe method calls but the built-in plugins do not log sensitive data.

### Plugin Trust Model

Plugins have unrestricted access to Root's bridge and DOM. There is no sandboxing, capability system, or permission model. A malicious plugin could:
- Exfiltrate authentication tokens from bridge calls
- Modify messages before they are sent
- Impersonate the user
- Access any origin (Root's Chromium has `--disable-web-security`)

This is consistent with the trust model of similar projects (Vencord, BetterDiscord) where users are responsible for vetting the plugins they install.

### HTML Injection Security

The patcher uses comment markers (`<!-- uprooted:start -->` / `<!-- uprooted:end -->`) to detect injected content. Root's HTML files are not integrity-checked by the application, which is what makes injection possible but also means any process with write access to the profile directory could inject arbitrary scripts.

---

## 12. AI Contributor Guide

> Full AI contributor guidance: [`CLAUDE.md`](../../CLAUDE.md). Common pitfalls and layer identification: [`CLAUDE.md` § Critical Rules](../../CLAUDE.md#critical-rules).

---

**Canonical for:** system design, layer boundaries, critical rules (full with code), threading model, error handling patterns, startup phase overview, known limitations, security considerations
**Not canonical for:** per-class implementation detail → [HOOK_REFERENCE.md](HOOK_REFERENCE.md) | Avalonia reflection patterns → [AVALONIA_PATTERNS.md](AVALONIA_PATTERNS.md) | native profiler → [CLR_PROFILER.md](CLR_PROFILER.md) | repository structure → [CLAUDE.md](../../CLAUDE.md)
*Architecture reference for Uprooted v0.4.2. Last updated 2026-02-21.*
