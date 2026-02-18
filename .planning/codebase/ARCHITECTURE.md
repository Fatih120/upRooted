# Architecture

**Analysis Date:** 2026-02-17

## Pattern Overview

**Overall:** Dual-layer client modification framework with CLR profiler injection and TypeScript HTML injection

Uprooted has two independent injection layers that operate in parallel:

1. **C# .NET hook** (`hook/`) — Injects into Root's managed .NET 10/Avalonia process via CLR profiler. Adds native Avalonia controls to the settings page sidebar, applies themes via resource dictionary manipulation, and discovers DotNetBrowser features.
2. **TypeScript browser injection** (`src/`) — Injects into Root's embedded Chromium (DotNetBrowser) context via HTML `<script>` tags. Provides the plugin runtime, theme CSS engine, bridge proxy system, and link embeds.

The C# layer handles native UI integration and Avalonia theming. The TypeScript layer handles web content modification, bridge interception, and link embed rendering. They share no runtime state but target the same application. The only shared state is the settings file on disk and the visual result (both layers inject into the same app).

**Key Characteristics:**
- Dual-layer architecture: C# native injection + TypeScript browser injection
- CLR profiler IL injection for process entry (no binary modification)
- Reflection-based Avalonia control creation (single-file .NET app constraint)
- ES6 Proxy bridge interception for plugin patches
- Plugin system with lifecycle hooks (start/stop) and declarative patches
- Settings persistence via file-based JSON outside browser (localStorage unavailable in incognito mode)
- Modular plugin API exposing DOM, CSS, native, and bridge utilities
- Self-healing HTML patches via FileSystemWatcher (Phase 0)
- Avalonia-native link embed engine (Phase 4.5b)

## Layers

### C# Native Injection Layers

**CLR Profiler Layer:**
- Purpose: Get managed code executing inside Root's .NET process
- Location: `tools/uprooted_profiler.c` (source) and `tools/uprooted_profiler.dll` (compiled)
- Contains: `ICorProfilerCallback` implementation, IL injection into first suitable method
- Mechanism: Prepends IL to a target method that calls `Assembly.LoadFrom("UprootedHook.dll")` + `CreateInstance("UprootedHook.Entry")`, wrapped in try/catch
- Depends on: Environment variables (`CORECLR_ENABLE_PROFILING`, `CORECLR_PROFILER`, `CORECLR_PROFILER_PATH`, `DOTNET_ReadyToRun=0`)
- Critical constraint: `DOTNET_ReadyToRun=0` disables precompiled native images, forcing JIT compilation so the profiler can modify IL

**Hook Entry Layer:**
- Purpose: Initialize Uprooted inside Root's .NET process
- Location: `hook/Entry.cs`, `hook/NativeEntry.cs`, `hook/StartupHook.cs`
- Contains: `[ModuleInitializer]` + constructor entry (both guarded by `Interlocked.CompareExchange` for one-time init), process name guard, background thread spawning
- Pattern: Background thread runs multi-phase startup (Phase 0-5):
  - Phase 0: HTML patch verification + self-healing
  - Phase 1: Wait for Avalonia assemblies to load (30s timeout, poll 250ms)
  - Phase 2: Resolve all Avalonia types via reflection into `AvaloniaReflection` cache
  - Phase 3: Wait for `Application.Current` (30s timeout, poll 500ms)
  - Phase 3+: Wait for `MainWindow` via `ApplicationLifetime` (60s timeout, poll 500ms)
  - Phase 3.5: Theme engine initialization
  - Phase 4: SidebarInjector.StartMonitoring() (200ms timer)
  - Phase 4.5: Browser discovery diagnostic (10s delay)
  - Phase 4.5b: LinkEmbedEngine startup (15s delay, then 500ms poll)
  - Phase 5: DotNetBrowser feature loading detection
- Critical rule: Must never block Root's startup — all heavy work on background thread

**HTML Patch Verifier Layer:**
- Purpose: Ensure HTML injection markers exist in Root's profile HTML files; self-heal when Root auto-updates overwrite them
- Location: `hook/HtmlPatchVerifier.cs`
- Contains: Marker verification, file re-patching, `FileSystemWatcher` for directory monitoring
- Mechanism: Phase 0 runs on background thread before Avalonia needed. Scans profile HTML files for injection markers. If missing, re-patches in place. Creates FileSystemWatcher for each HTML directory with 2-second cooldown to debounce rapid overwrites.

**Avalonia Reflection Layer:**
- Purpose: Resolve and cache Avalonia types, properties, and methods via reflection
- Location: `hook/AvaloniaReflection.cs` (1943 lines — largest and most critical file)
- Contains: Dictionary of ~80 Avalonia types built by scanning loaded assemblies filtered by `"Avalonia"` prefix, cached `PropertyInfo`/`MethodInfo` handles, control creation helpers
- Why needed: Root.exe is a single-file .NET 10 binary — `Type.GetType("Avalonia.Controls.TextBlock, Avalonia.Controls")` does NOT work because assembly-qualified names can't be resolved
- Key design decisions:
  - `DispatcherPriority` is a struct (not an enum) in Avalonia 11+ — resolved via fallback chain (static property → static field → Enum.Parse → Activator.CreateInstance)
  - Event subscription uses `Expression.Lambda` because CLR `EventInfo.AddEventHandler` doesn't work for Avalonia `RoutedEvent`s
  - `ClearValue(AvaloniaProperty)` removes local value overrides to re-enable data bindings — calling `SetValue` overrides bindings permanently
- Critical rule: ALL Avalonia type access must go through this class. Never use `typeof()`, `Type.GetType()`, or direct assembly references for Avalonia types.

**Visual Tree Walker Layer:**
- Purpose: Discover Root's settings page structure by walking the Avalonia visual tree
- Location: `hook/VisualTreeWalker.cs` (554 lines)
- Contains: `FindSettingsLayout()` algorithm that discovers settings page via structural analysis
- Algorithm:
  1. Depth-first search for TextBlock with `Text == "APP SETTINGS"` (anchor point)
  2. Walk UP to find StackPanel with >= 8 children (nav container)
  3. Walk UP to find multi-column Grid (skip single-column Grids) — layout grid
  4. Identify content area as Grid child in same row but different (higher) column
  5. Find ListBox inside nav container
  6. Find back button (TextBlock containing `"<"`, walk up to clickable ancestor)
- Fragile: Root provides no stable selectors — layout discovered by structure, not IDs/class names

**Sidebar Injector Layer:**
- Purpose: Monitor settings page state and inject/remove UPROOTED section
- Location: `hook/SidebarInjector.cs` (1088 lines)
- Contains: 200ms timer-based polling, injection/cleanup logic, click event handling, content page management
- Mechanism: Timer fires on UI thread, lightweight check for "APP SETTINGS" TextBlock, inject if found and not already injected, cleanup if not found
- Injected controls:
  - "UPROOTED" section header (matches Root's "APP SETTINGS" style)
  - Three nav items: "Uprooted", "Plugins", "Themes" (with pointer event handlers)
  - Version text inserted into Root's grey version box
- Content overlay pattern: Does NOT modify Root's content panel. Adds Uprooted page as a sibling in the layout Grid with matching Column/Row and opaque background (`#0D1521`). Avoids freeze caused by modifying `ContentControl.Content` or `ScrollContentPresenter.Content`.
- Tag-based identification: All injected Avalonia controls use `Control.Tag` strings starting with `"uprooted-"` (e.g., `"uprooted-injected"`, `"uprooted-content"`, `"uprooted-item-{page}"`)
- Threading: `_injecting` interlocked flag prevents concurrent `CheckAndInject` calls. `_ourItemClicked` flag coordinates between `PointerPressed` handlers on custom items and the ListBox.

**Content Pages Layer:**
- Purpose: Build native Avalonia settings pages through reflection
- Location: `hook/ContentPages.cs` (1753 lines)
- Contains: Page builders for Uprooted, Plugins, Themes pages — all via reflection-based control creation matching Root's exact card styling
- Styling invariants: card background `#081408`, corner radius 12, border `#19ffffff` thickness 0.5, inner padding 24px, page margin 24/24/24/0
- Current limitations: Theme switching and plugin management have click handlers but are display-only for plugin toggles; theme switching is live

**Theme Engine Layer:**
- Purpose: Apply native Avalonia theme overrides at runtime
- Location: `hook/ThemeEngine.cs` (2218 lines)
- Contains: Resource dictionary manipulation, visual tree color override, theme persistence
- Mechanism: Overrides `Application.Styles[0].Resources` and injects into `Application.Resources.MergedDictionaries`. Walks visual tree to replace hardcoded brush values. Supports named themes and custom accent/background colors.
- Critical pattern: Never modify colors on controls directly — override resource dictionaries first so new controls pick up the theme automatically

**Color Picker Popup Layer:**
- Purpose: HSL color picker popup for custom theme accent and background
- Location: `hook/ColorPickerPopup.cs` (533 lines)
- Contains: HSV color picker UI, gradient rendering, color conversion
- Presented as an overlay on the Themes settings page

**Color Utils Layer:**
- Purpose: Color parsing, HSL/HSV conversion, contrast calculation
- Location: `hook/ColorUtils.cs` (262 lines)
- Contains: Hex parsing, color space conversions, luminance/contrast math
- Used by: ThemeEngine and ContentPages

**Hook Settings Layer:**
- Purpose: Settings for the C# hook (INI-based)
- Location: `hook/UprootedSettings.cs` (91 lines)
- Contains: INI file reading/writing (no System.Text.Json due to profiler context constraint)
- Known bug: `System.Text.Json` causes `MissingMethodException` in profiler-injected context, preventing JSON deserialization. INI format is workaround.

**Hook Logging Layer:**
- Purpose: Thread-safe file logging for the C# hook
- Location: `hook/Logger.cs` (28 lines)
- Contains: Writes to `uprooted-hook.log` in profile directory with timestamps and categories
- Critical rule: Logger swallows its own exceptions to avoid crashing Root

**DotNetBrowser Reflection Layer:**
- Purpose: Discover and cache DotNetBrowser types for browser-dependent features
- Location: `hook/DotNetBrowserReflection.cs` (1914 lines)
- Contains: Type cache for DotNetBrowser.Core types, IBrowser discovery via ViewModel chain, ExecuteJavaScript wrapper
- Pattern: Walks ViewModel hierarchy to find IBrowser instances

**Browser Discovery Layer:**
- Purpose: Phase 4.5 diagnostic scanner for architecture discovery
- Location: `hook/BrowserDiscovery.cs` (498 lines)
- Contains: Full visual tree dump, loaded assembly inventory
- Timing: 10-second delay after Phase 4 to allow UI to render

**Link Embed Engine Layer:**
- Purpose: Avalonia-native link embed rendering with OG metadata fetch
- Location: `hook/LinkEmbedEngine.cs` (1260 lines)
- Contains: Visual tree scanning for URL-containing CTextBlocks, OG metadata fetching via HttpClient, native embed card injection
- Timing: Phase 4.5b (15s delay after startup, 500ms poll timer)
- Status: YouTube fully working (oEmbed + predictable thumbnails). Generic sites need improvement (bot-hostile UAs, direct image URLs rejected, some sites need JS rendering)

**NSFW Filter Layer:**
- Purpose: NSFW content filtering via JavaScript injection
- Location: `hook/NsfwFilter.cs` (305 lines)
- Contains: JS injection wrapper for DotNetBrowser
- Known limitation: Chat is Avalonia-native, not browser-rendered, so filter cannot modify chat messages. Needs Avalonia-native redesign.

**Platform Paths Layer:**
- Purpose: Cross-platform path resolution
- Location: `hook/PlatformPaths.cs` (29 lines)
- Contains: Profile directory and Uprooted install path resolution for Windows and Linux

### TypeScript Browser Injection Layers

**Installation Layer:**
- Purpose: Inject Uprooted into Root Communications' profile HTML files
- Location: `src/core/patcher.ts`, installation scripts in `scripts/`
- Contains: File system operations, HTML injection logic, backup/restore functionality
- Injects: `<!-- uprooted -->` comment markers for detection and cleanup
- Depends on: Node.js filesystem APIs, settings module
- Used by: Installation CLI and Tauri desktop application

**Core Runtime Layer (Preload):**
- Purpose: Bootstrap Uprooted in the browser context before Root's bundles load
- Location: `src/core/preload.ts`
- Contains: Entry point, plugin loader initialization, bridge proxy setup, error handling
- Depends on: PluginLoader, bridge proxy, plugin system
- Used by: Injected into Root's HTML before script evaluation

**Plugin Management Layer:**
- Purpose: Discover, register, start/stop, and manage plugin lifecycle
- Location: `src/core/pluginLoader.ts`
- Contains: Plugin registry, event routing, CSS/patch installation/removal
- Depends on: Plugin types, bridge events, CSS utilities
- Used by: Preload script and settings panel plugin

**Bridge Interception Layer:**
- Purpose: Proxy Root's native<->WebRTC bridge to enable plugin patches
- Location: `src/api/bridge.ts`
- Contains: ES6 Proxy wrappers around `__nativeToWebRtc` and `__webRtcToNative` globals
- Deferred installation: Uses `Object.defineProperty` getter/setter traps to automatically wrap any value assigned to bridge globals after preload runs
- Depends on: PluginLoader (for event emission), bridge type definitions
- Used by: Plugins applying patches, all native<->JS communications

**Plugin API Layer:**
- Purpose: Provide public APIs for plugin authors
- Location: `src/api/` (bridge.ts, css.ts, dom.ts, native.ts exported via index.ts)
- Contains: Bridge utilities, CSS injection, DOM observation, native helpers
- Depends on: Web APIs (DOM, CSS, fetch, etc.)
- Used by: Built-in and third-party plugins

**Built-in Plugins:**
- Purpose: Core functionality provided with Uprooted
- Location: `src/plugins/`
- Contains:
  - `sentry-blocker/`: Intercepts fetch/XHR/sendBeacon to block Sentry telemetry
  - `themes/`: Applies CSS variable overrides for theme customization
  - `settings-panel/`: Injects settings UI into Root's web-rendered sidebar
  - `link-embeds/`: Renders link previews in chat using DOM injection and OG metadata
- Depends on: Plugin API, types, settings
- Used by: Preload script during initialization

**Installer Application Layer:**
- Purpose: Desktop UI for installation, configuration, and theme management
- Location: `installer/src/`
- Contains: Page routing, Tauri integration, theme UI, installation UI
- Depends on: Tauri API, local state management
- Used by: End users for setup and configuration

**Settings Persistence Layer:**
- Purpose: Store configuration outside browser localStorage (unavailable in Root's incognito mode)
- Location: `src/core/settings.ts` (CLI), `installer/src/lib/tauri.ts` (Tauri backend bridges)
- Contains: Settings file I/O, defaults, per-plugin configuration
- File: `%LOCALAPPDATA%/Root Communications/Root/profile/default/uprooted-settings.json`
- Mechanism: Patcher inlines settings as `window.__UPROOTED_SETTINGS__` in HTML at install/patch time. Runtime changes from the settings panel update the in-memory object; the installer/CLI writes changes back to disk.
- Depends on: File system access, JSON serialization
- Used by: All other layers for configuration state

## Data Flow

**C# Hook Lifecycle:**

1. Windows starts Root.exe
2. .NET runtime sees `CORECLR_ENABLE_PROFILING=1`, loads `uprooted_profiler.dll`
3. Profiler checks process name — if not "Root", returns `E_FAIL` and detaches
4. Profiler waits for suitable module to load (one that imports `System.Object`)
5. Profiler creates cross-module MemberRefs for `Assembly.LoadFrom` + `CreateInstance`
6. Profiler prepends 26 bytes of IL to first method with a body (wrapped in try/catch)
7. When that method is JIT-compiled and called, IL loads `UprootedHook.dll`
8. `Entry.ModuleInit()` fires via `[ModuleInitializer]` (or constructor fallback)
9. `StartupHook.Initialize()` spawns background thread
10. Thread runs multi-phase startup: Phase 0 (HTML patches) → Phase 1-3 (Avalonia wait) → Phase 3.5 (theme init) → Phase 4 (sidebar monitor) → Phase 4.5 (diagnostic) → Phase 4.5b (link embeds) → Phase 5 (DotNetBrowser features)
11. SidebarInjector.StartMonitoring() begins 200ms timer
12. Timer detects settings page open -> VisualTreeWalker discovers layout -> injects UPROOTED section
13. User sees "Uprooted", "Plugins", "Themes" in the settings sidebar

**TypeScript Injection Lifecycle:**

1. Patcher inserts `<script>` and `<link>` tags into profile HTML files (before `</head>`)
2. On page load, `preload.ts` runs before Root's bundles
3. Reads settings from `window.__UPROOTED_SETTINGS__` (inlined by patcher)
4. Installs ES6 Proxy wrappers on `window.__nativeToWebRtc` and `window.__webRtcToNative`
5. Creates `PluginLoader`, registers built-in plugins, starts enabled ones
6. Built-in plugins start in order:
   - sentry-blocker: wraps fetch/XHR/sendBeacon earliest (before Sentry init)
   - themes: applies CSS variables based on settings
   - link-embeds: sets up DOM observer for link rendering
   - settings-panel: injects UI components into DOM
7. When plugins are disabled via settings panel, they can be stopped and restarted at runtime

**Bridge Event Flow (Plugin Patching):**

1. Root's C# host calls `__nativeToWebRtc.setMute(true)`
2. ES6 Proxy intercepts the property access, returns wrapped function
3. Wrapped function creates BridgeEvent: `{ method, args, cancelled: false, returnValue: undefined }`
4. `PluginLoader.emit()` fires the event to all registered handlers for that method
5. Handlers execute in plugin registration order
6. Execution priority: `replace` takes priority over `before` — if `replace` is set, it runs instead of the original and `before` is ignored
7. If `before` runs (no `replace`), returning `false` cancels the original call
8. If any plugin cancels the call, later plugins' handlers for that method are skipped
9. Original method is called only if not cancelled, with (potentially modified) args
10. **Note:** `after` handler is defined in the `Patch` interface but is **not yet implemented** by the loader

**Installation Flow (Installer App):**

1. User runs Uprooted Installer (Tauri desktop app or CLI)
2. Installer deploys 5 files to `%LOCALAPPDATA%\Root\uprooted\`:
   - `uprooted_profiler.dll` (CLR profiler, native C DLL)
   - `UprootedHook.dll` + `UprootedHook.deps.json` (managed hook assembly)
   - `uprooted-preload.js` + `uprooted.css` (TypeScript injection payload)
3. Sets user-scoped CLR profiler env vars via Windows registry (or shell profile on Linux) and broadcasts `WM_SETTINGCHANGE`
4. Patches HTML files in Root's profile directory (WebRtcBundle + RootApps)
5. Uninstall reverses all three steps: removes env vars, restores HTML backups, deletes deployed files

**State Management:**

- **Global Settings:** Persisted to `uprooted-settings.json`, inlined into HTML by patcher, accessed at runtime via `window.__UPROOTED_SETTINGS__`
- **Plugin State:** Each plugin manages its own state via closure variables (e.g., sentry-blocker tracks blockedCount)
- **Active Plugins:** PluginLoader maintains Set of started plugin names
- **Event Handlers:** PluginLoader maintains Map of `[eventName:methodName] -> handler[]` for efficient dispatch
- **C# Hook State:** `SidebarInjector` maintains injection state with interlocked flags for thread safety
- **Link Embeds State:** LinkEmbedEngine scans visual tree periodically, maintains set of processed URLs to avoid duplicates

## Key Abstractions

**UprootedPlugin Interface:**
- Purpose: Standardized plugin definition contract
- Examples: `src/plugins/themes/index.ts`, `src/plugins/sentry-blocker/index.ts`, `src/plugins/settings-panel/index.ts`, `src/plugins/link-embeds/index.ts`
- Pattern: Export default object satisfying `UprootedPlugin` type with name, version, start/stop lifecycle hooks, patches array, and optional CSS

**Patch System:**
- Purpose: Declaratively intercept bridge method calls
- Pattern: Plugin defines `patches` array with bridge ("nativeToWebRtc" or "webRtcToNative"), method name, and optional before/replace callbacks
- Priority: `replace` > `before`. If `replace` is set, `before` is ignored. `after` is defined but not yet invoked by the loader.
- Examples: Sentry-blocker wraps fetch at platform level; themes sets CSS variables; settings panel observes DOM; link-embeds renders previews

**Settings Schema:**
- Purpose: Global settings + per-plugin config with defaults
- Location: `src/types/settings.ts` defines `UprootedSettings`, `PluginSettings`, `DEFAULT_SETTINGS`
- Pattern: Plugin settings keyed by plugin name under `plugins.{name}.config`, persisted to disk via settings JSON file
- Runtime access: `window.__UPROOTED_SETTINGS__?.plugins?.["name"]?.config`

**Bridge Type System:**
- Purpose: TypeScript interfaces for native<->WebRTC communication contracts
- Examples: `INativeToWebRtc` (42 methods, C# -> JS), `IWebRtcToNative` (29 methods, JS -> C#) in `src/types/bridge.ts`
- Pattern: Branded string types (UserGuid, DeviceGuid) prevent accidental GUID type confusion

**AvaloniaReflection Pattern (C#):**
- Purpose: Cached reflection access to Avalonia types in single-file .NET app context
- Location: `hook/AvaloniaReflection.cs`
- Pattern: Scan loaded assemblies -> build type dictionary -> cache PropertyInfo/MethodInfo -> create controls via Activator.CreateInstance + cached setters

**Content Overlay Pattern (C#):**
- Purpose: Display Uprooted pages without modifying Root's content panel (which causes freezes)
- Pattern: Add Uprooted page as Grid sibling with matching Column/Row and opaque background -> remove on Root sidebar click

**Tag-Based Identification:**
- C# layer: `Control.Tag` strings starting with `"uprooted-"` (e.g., `"uprooted-injected"`, `"uprooted-content"`)
- TS layer: `data-uprooted` attributes on DOM elements (e.g., `data-uprooted="section"`, `data-uprooted-page="..."`)

## Entry Points

**CLR Profiler Entry (C#):**
- Location: `uprooted_profiler.dll` (C/MSVC)
- Triggers: .NET runtime loads profiler via `CORECLR_ENABLE_PROFILING` env var at Root.exe startup
- Responsibilities: Process guard, wait for suitable module, inject IL to load `UprootedHook.dll`

**Managed Hook Entry (C#):**
- Location: `hook/Entry.cs` -> `hook/StartupHook.cs`
- Triggers: Profiler-injected IL calls `Assembly.LoadFrom` + `CreateInstance`
- Responsibilities: One-time init guard, process name check, spawn background thread, multi-phase Avalonia/DotNetBrowser wait, start sidebar monitoring

**Browser Runtime Entry (TS):**
- Location: `src/core/preload.ts`
- Triggers: Root's browser loads, preload script executes in Chromium context
- Responsibilities: Check enabled flag, set up bridge proxies, initialize plugin loader, start plugins

**Installation Entry (TS):**
- Location: `scripts/install.ts`
- Triggers: User runs `pnpm install-root` or Tauri installer UI
- Responsibilities: Load settings, find Root's profile directory, patch HTML files, create backups

**Uninstallation Entry (TS):**
- Location: `scripts/uninstall.ts`
- Triggers: User runs uninstall command
- Responsibilities: Restore backup HTML files or strip injection markers

**Installer UI Entry:**
- Location: `installer/src/main.ts`
- Triggers: Tauri desktop application launches
- Responsibilities: Set up titlebar, route between pages (installer, themes), lazy-initialize page controllers

## Error Handling

**Strategy:** Never throw from injected code. Catch and log. Both layers prioritize stability over error reporting.

**C# Hook Patterns:**
- Logger swallows its own exceptions to avoid crashing Root
- All reflection calls handle null gracefully (types/properties may not exist)
- Profiler IL injection wrapped in try/catch — if DLL fails to load, Root continues normally
- One-time init via `Interlocked.CompareExchange` prevents double execution
- Background thread for all heavy work — never block Root's startup or UI thread
- Phase failures are logged but non-fatal for early phases; later phases abort gracefully

**TypeScript Patterns:**
- **Preload:** Errors caught at top level, displayed in red banner at top of page with stack trace
- **Plugin Lifecycle:** Plugin start/stop errors logged and caught, don't prevent other plugins from starting
- **Bridge Events:** Handler errors logged but don't interrupt event chain
- **Settings Load:** Falls back to `DEFAULT_SETTINGS` if JSON parse fails or file missing
- **Installation:** Errors logged to console, process exits with status 1
- **Link Embeds:** OG fetch errors logged, element still renders with fallback content

## Cross-Cutting Concerns

**Logging:**
- TypeScript: Console output prefixed with `[Uprooted]` or `[Uprooted:plugin-name]`
- TypeScript: Native logging via `nativeLog()` routes through bridge to .NET logs
- C#: Thread-safe file logging to `uprooted-hook.log` with `[Category]` prefix and timestamps

**Threading (C#):**
- Background thread for initialization (multi-phase wait and browser feature detection)
- Timer callback dispatches to UI thread via `Dispatcher.UIThread`
- `_injecting` interlocked flag prevents concurrent CheckAndInject calls
- `_ourItemClicked` flag coordinates pointer events between custom items and ListBox

**Threading (TypeScript):**
- Single-threaded (browser main thread)
- MutationObserver callbacks debounced (80ms) in settings panel
- Plugin `start()` is async-safe but called sequentially (`await` in loop)
- Link embed rendering uses RAF (requestAnimationFrame) for DOM insertion

**Validation:**
- Settings validation deferred to each layer (installer validates, preload trusts)
- Bridge type interfaces prevent accidental type mismatches (UserGuid != string)
- Plugin schema validation in installer (enforces valid setting types)

**Authentication:**
- No authentication layer (runs in user's own browser context)
- All calls to native layer pre-authorized by Root's startup process
- Plugins have unrestricted access to Root's bridge and DOM (no sandboxing)

**Performance:**
- Bridge proxy uses ES6 Proxy for zero-copy interception
- MutationObserver for DOM waiting (efficient vs. polling)
- Event handler registration uses Map for O(1) lookup
- CSS injection uses ID-keyed `<style>` elements for efficient updates
- C# sidebar injector uses 200ms timer polling (not event-driven — Avalonia RoutedEvent subscription via reflection unreliable)
- Link embed engine uses 500ms poll with deduplication to avoid reprocessing URLs

---

*Architecture analysis: 2026-02-17*
