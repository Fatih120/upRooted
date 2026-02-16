# Architecture

**Analysis Date:** 2026-02-16

## Pattern Overview

**Overall:** Plugin-based mod framework with bridge interception pattern

**Key Characteristics:**
- Layered injection architecture: HTML file patching → preload script → plugin loader → runtime patches
- Dual-bridge proxy pattern: intercepts both native→webrtc and webrtc→native calls
- Plugin system with lifecycle hooks (start/stop) and declarative patches
- Settings persistence via file-based JSON outside browser (localStorage unavailable in incognito mode)
- Modular plugin API exposing DOM, CSS, native, and bridge utilities

## Layers

**Installation Layer:**
- Purpose: Inject Uprooted into Root Communications' profile HTML files
- Location: `src/core/patcher.ts`, installation scripts in `scripts/`
- Contains: File system operations, HTML injection logic, backup/restore functionality
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
- Purpose: Proxy Root's native↔WebRTC bridge to enable plugin patches
- Location: `src/api/bridge.ts`
- Contains: ES6 Proxy wrappers around __nativeToWebRtc and __webRtcToNative globals
- Depends on: PluginLoader (for event emission), bridge type definitions
- Used by: Plugins applying patches, all native→JS communications

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
  - `sentry-blocker`: Intercepts fetch/XHR/sendBeacon to block Sentry telemetry
  - `themes`: Applies CSS variable overrides for theme customization
  - `settings-panel`: Injects settings UI into Root's sidebar
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
- Depends on: File system access, JSON serialization
- Used by: All other layers for configuration state

## Data Flow

**Installation Flow:**

1. User runs installer application (desktop app built with Tauri)
2. Installer detects Root Communications installation directory
3. Patcher reads Root's profile HTML files
4. Injects three elements into `<head>` before `</head>`:
   - Settings script tag: `window.__UPROOTED_SETTINGS__ = {...}`
   - Preload script tag: `<script src="file:///path/to/uprooted-preload.js">`
   - CSS link tag: `<link rel="stylesheet" href="file:///path/to/uprooted.css">`
5. Settings JSON loaded from `%LOCALAPPDATA%/Root Communications/Root/profile/default/uprooted-settings.json`

**Runtime Flow:**

1. Root's browser loads with injected preload script
2. Preload script checks if `window.__UPROOTED_SETTINGS__.enabled` is true
3. Preload installs bridge proxies on `__nativeToWebRtc` and `__webRtcToNative`
4. PluginLoader initializes with settings, registers built-in plugins
5. Built-in plugins start in order:
   - sentry-blocker: wraps fetch/XHR/sendBeacon earliest (before Sentry init)
   - themes: applies CSS variables based on settings
   - settings-panel: injects UI components into DOM
6. When plugins are disabled via settings panel, they can be stopped and restarted at runtime

**Bridge Event Flow (Plugin Patching):**

1. Root's C# host calls `__nativeToWebRtc.setMute(true)`
2. ES6 Proxy intercepts the property access, returns wrapped function
3. Wrapped function creates BridgeEvent with method name and arguments
4. PluginLoader.emit() is called with the event
5. Plugin handlers fire in registration order
6. If patch has `replace()`, it sets returnValue and cancels original call
7. If patch has `before()`, it can inspect/modify args or cancel call by returning false
8. Original method is called only if not cancelled

**State Management:**

- **Global Settings:** Loaded from disk at startup, accessed via `window.__UPROOTED_SETTINGS__`
- **Plugin State:** Each plugin manages its own state via closure variables (e.g., sentry-blocker tracks blockedCount)
- **Active Plugins:** PluginLoader maintains Set of started plugin names
- **Event Handlers:** PluginLoader maintains Map of `[eventName:methodName] → handler[]` for efficient dispatch

## Key Abstractions

**UprootedPlugin Interface:**
- Purpose: Standardized plugin definition contract
- Examples: `src/plugins/themes/index.ts`, `src/plugins/sentry-blocker/index.ts`, `src/plugins/settings-panel/index.ts`
- Pattern: Export default object satisfying `UprootedPlugin` type with name, version, start/stop lifecycle hooks, patches array, and optional CSS

**Patch System:**
- Purpose: Declaratively intercept bridge method calls
- Pattern: Plugin defines `patches` array with bridge ("nativeToWebRtc" or "webRtcToNative"), method name, and optional before/replace callbacks
- Examples: Sentry-blocker wraps fetch at platform level; themes sets CSS variables; settings panel observes DOM

**Settings Schema:**
- Purpose: Global settings + per-plugin config with defaults
- Examples: `src/types/settings.ts` defines UprootedSettings, plugins.themes.config contains selected theme name
- Pattern: Plugin settings are keyed by plugin name, each plugin's schema defined in its `settings` field

**Bridge Type System:**
- Purpose: TypeScript interfaces for native↔WebRTC communication contracts
- Examples: `INativeToWebRtc` (C# → JS), `IWebRtcToNative` (JS → C#) in `src/types/bridge.ts`
- Pattern: Branded string types (UserGuid, DeviceGuid) prevent accidental GUID type confusion

## Entry Points

**Installation Entry:**
- Location: `scripts/install.ts`
- Triggers: User runs `npm run install-root` command or Tauri installer UI
- Responsibilities: Load settings, find Root's profile directory, patch HTML files, create backups

**Uninstallation Entry:**
- Location: `scripts/uninstall.ts`
- Triggers: User runs uninstall command
- Responsibilities: Restore backup HTML files or strip injection markers

**Browser Runtime Entry:**
- Location: `src/core/preload.ts`
- Triggers: Root's browser loads, preload script executes in Chromium context
- Responsibilities: Check enabled flag, set up bridge proxies, initialize plugin loader, start plugins

**Installer UI Entry:**
- Location: `installer/src/main.ts`
- Triggers: Tauri desktop application launches
- Responsibilities: Set up titlebar, route between pages (installer, themes), lazy-initialize page controllers

## Error Handling

**Strategy:** Try-catch with visible error banner for preload failures

**Patterns:**
- **Installation:** Errors logged to console, process exits with status 1
- **Preload:** Errors caught at top level, displayed in red banner at top of page with stack trace
- **Plugin Lifecycle:** Plugin start/stop errors logged and caught, don't prevent other plugins from starting
- **Bridge Events:** Handler errors logged but don't interrupt event chain
- **Settings Load:** Falls back to DEFAULT_SETTINGS if JSON parse fails or file missing

## Cross-Cutting Concerns

**Logging:**
- Console output prefixed with `[Uprooted]` or `[Uprooted:plugin-name]`
- Native logging available via `nativeLog()` helper which routes through bridge to .NET logs
- Errors always logged even if silent mode preferred

**Validation:**
- Settings validation deferred to each layer (installer validates, preload trusts)
- Bridge type interfaces prevent accidental type mismatches (UserGuid ≠ string)
- Plugin schema validation in installer (enforces valid setting types)

**Authentication:**
- No authentication layer (runs in user's own browser context)
- All calls to native layer pre-authorized by Root's startup process

**Performance:**
- Bridge proxy uses ES6 Proxy for zero-copy interception
- MutationObserver for DOM waiting (efficient vs. polling)
- Event handler registration uses Map for O(1) lookup
- CSS injection uses ID-keyed `<style>` elements for efficient updates
