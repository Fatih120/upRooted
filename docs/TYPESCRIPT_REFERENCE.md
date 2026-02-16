# TypeScript Browser Injection Layer Reference

> **Related Documentation**
>
> - [Architecture Overview](ARCHITECTURE.md) -- System-level overview of Uprooted's dual-layer design
> - [Hook Reference](HOOK_REFERENCE.md) -- C# .NET hook that injects scripts into the HTML
> - [Plugin API Reference](plugins/API_REFERENCE.md) -- Full API surface available to plugin authors
> - [Bridge Reference](plugins/BRIDGE_REFERENCE.md) -- Bridge proxy interception and method catalog
> - [Build Guide](BUILD.md) -- Build instructions for all components

---

## Table of Contents

1. [Overview](#overview)
2. [Core Runtime](#core-runtime)
3. [Plugin API](#plugin-api)
4. [Built-in Plugins](#built-in-plugins)
5. [Type Definitions](#type-definitions)
6. [Build System](#build-system)

---

## Overview

The TypeScript browser injection layer is Uprooted's client-side runtime. It runs inside
Root Communications' embedded DotNetBrowser Chromium instance and provides the plugin
system, theme engine, and bridge proxies that make client modification possible.

### How It Gets There

The C# hook (see [Hook Reference](HOOK_REFERENCE.md)) patches Root's profile HTML files
(`WebRtcBundle/index.html` and `RootApps/*/index.html`) by inserting tags before `</head>`:

1. A `<script>` block setting `window.__UPROOTED_SETTINGS__` with JSON settings
2. A `<script src="file:///...">` loading `dist/uprooted-preload.js` (the bundled IIFE)
3. A `<link rel="stylesheet">` loading `dist/uprooted.css` (combined plugin CSS)

Because these tags appear before Root's own bundle scripts, Uprooted's preload runs first,
allowing bridge proxies to be installed before Root's WebRTC code touches the globals.

### Relationship to the C# Hook Layer

| Concern | C# Hook | TypeScript Layer |
|---------|---------|-----------------|
| Injection target | Avalonia UI (.NET) | Chromium DOM (browser) |
| Communication | Reflection into Avalonia types | ES6 Proxy on `window` globals |
| Settings | INI file via `UprootedSettings.cs` | JSON file via `settings.ts`, inlined into HTML |
| Plugin system | None (monolithic) | Full lifecycle with patches, CSS, events |
| Theme engine | `ThemeEngine.cs` for native Avalonia | CSS variable overrides in the browser |

### Source Layout

```
src/
  core/
    preload.ts          Entry point -- initialization sequence
    pluginLoader.ts     Plugin lifecycle management
    patcher.ts          HTML patch install/uninstall (CLI tool, not browser)
    settings.ts         File-based settings I/O (CLI tool, not browser)
  api/
    bridge.ts           ES6 Proxy wrapper for Root's bridge globals
    css.ts              Runtime CSS injection/removal
    dom.ts              DOM query and mutation helpers
    native.ts           Native bridge call wrappers
    index.ts            Barrel export for plugin authors
  plugins/
    sentry-blocker/     Blocks Sentry telemetry
    themes/             CSS variable theme engine
    settings-panel/     In-app settings sidebar injection
  types/
    bridge.ts           INativeToWebRtc / IWebRtcToNative interfaces
    plugin.ts           UprootedPlugin, Patch, SettingField interfaces
    root.ts             Window augmentation (global type declarations)
    settings.ts         UprootedSettings / PluginSettings interfaces
```

Note: `patcher.ts` and `settings.ts` in `core/` are Node.js CLI scripts used by the
installer. They are **not** part of the browser bundle -- esbuild marks `node:fs` and
`node:path` as external.

---

## Core Runtime

### preload.ts -- Initialization

**File**: `src/core/preload.ts`

Single entry point for the TypeScript layer. Bundled as an IIFE that executes on load.

#### Initialization Sequence (line 22, `main()`)

1. **Read settings** (line 24): Reads `window.__UPROOTED_SETTINGS__`. If `enabled` is
   false, exits silently.
2. **Set version global** (line 34): Sets `window.__UPROOTED_VERSION__` from the
   compile-time constant `__UPROOTED_VERSION__` (injected by esbuild from `package.json`).
3. **Install bridge proxies** (line 37): Calls `installBridgeProxy()` -- wraps
   `window.__nativeToWebRtc` and `window.__webRtcToNative` with ES6 Proxies.
4. **Initialize plugin loader** (line 40): Creates `PluginLoader` with settings, exposes
   it on `window.__UPROOTED_LOADER__` (line 43).
5. **Wire loader into bridge** (line 46): Calls `setPluginLoader(loader)` so bridge proxy
   events reach the plugin system.
6. **Register built-in plugins** (lines 48-51): `sentry-blocker` first (wraps fetch
   earliest), then `themes`, then `settings-panel`.
7. **Inject custom CSS** (lines 54-56): If `settings.customCss` is non-empty.
8. **Start enabled plugins** (lines 59-61): Calls `loader.startAll()`.

#### Error Handling

On fatal exception, a red fixed-position error banner is injected into the DOM (lines
64-71). This is necessary because Root's Chromium has no accessible DevTools.

#### DOM Readiness

If `document.readyState === "loading"`, defers to `DOMContentLoaded`. Otherwise runs
immediately (line 77).

#### Global Surface

| Global | Type | Purpose |
|--------|------|---------|
| `window.__UPROOTED_SETTINGS__` | `UprootedSettings` | Inlined settings from JSON file |
| `window.__UPROOTED_VERSION__` | `string` | Version from `package.json` |
| `window.__UPROOTED_LOADER__` | `PluginLoader` | Plugin loader instance |

---

### pluginLoader.ts -- Plugin Lifecycle

**File**: `src/core/pluginLoader.ts`

#### Class: PluginLoader (line 20)

```typescript
export class PluginLoader {
  private plugins = new Map<string, UprootedPlugin>();
  private activePlugins = new Set<string>();
  private eventHandlers = new Map<string, EventHandler[]>();
  private settings: UprootedSettings;
  constructor(settings: UprootedSettings);
}
```

#### Registration: `register(plugin)` -- Line 31

Adds a plugin to the map. Does not start it. Skips duplicates with a warning.

#### Starting: `start(name)` -- Line 40

1. Install patches (line 51): Register bridge event handlers for each `Patch`.
2. Inject CSS (line 58): `injectCss("plugin-${name}", plugin.css)`.
3. Call `start()` hook (line 63): Awaited for async init.
4. Add to `activePlugins`.

#### Stopping: `stop(name)` -- Line 73

1. Call `stop()` hook (awaited).
2. Remove CSS via `removeCss("plugin-${name}")`.
3. Remove all bridge event handlers for this plugin.
4. Remove from `activePlugins`.

#### Batch Start: `startAll()` -- Line 96

Iterates registered plugins, starts those with `enabled !== false` (defaults to `true`).
Plugins start sequentially in registration order.

#### Event System: `emit(eventName, event)` -- Line 107

Called by the bridge proxy on every bridge method call. Dispatches to handlers registered
for `"eventName:methodName"`. If any handler sets `event.cancelled = true`, subsequent
handlers are skipped and the original call is suppressed.

#### BridgeEvent Interface (line 11)

```typescript
export interface BridgeEvent {
  method: string;       // Bridge method being called
  args: unknown[];      // Arguments (mutable -- plugins can modify)
  cancelled: boolean;   // Set true to suppress the original call
  returnValue?: unknown; // Return value when cancelled (from replace patches)
}
```

#### Patch Installation (line 118)

Converts `Patch` declarations to event handlers:
- `patch.replace`: Sets `event.returnValue` and cancels the original call.
- `patch.before`: Calls with args; returns `false` to cancel.
- Each handler tagged with `__plugin` for cleanup on stop.

Note: The `Patch` interface defines an `after` callback, but `installPatch` does not wire
it up yet. Planned for a future release.

---

### patcher.ts -- HTML Injection (CLI)

**File**: `src/core/patcher.ts` -- Node.js only, not in the browser bundle.

Injects or removes Uprooted's `<script>` and `<link>` tags from Root's HTML files. Used by
the installer. See [Hook Reference](HOOK_REFERENCE.md) for the runtime equivalent in C#.

#### Target Files

```
%LOCALAPPDATA%/Root Communications/Root/profile/default/
  WebRtcBundle/index.html
  RootApps/*/index.html
```

#### Injection Marker (line 18)

```typescript
const INJECTION_MARKER = "<!-- uprooted -->";
```

Used for idempotency (skip if present) and clean removal (strip marked lines).

#### `install(distDir)` -- Line 42

Backs up originals to `*.uprooted.bak`, injects settings + script + CSS tags before
`</head>`.

#### `uninstall()` -- Line 84

Restores from backup if available; otherwise strips lines containing the marker.

---

### settings.ts -- File-Based Settings I/O (CLI)

**File**: `src/core/settings.ts` -- Node.js only.

Root runs Chromium with `--incognito`, so localStorage is wiped every launch. Settings are
stored as JSON on disk and inlined into HTML by the patcher.

```typescript
// Settings file location (line 21):
// %LOCALAPPDATA%/Root Communications/Root/profile/default/uprooted-settings.json
```

- **`loadSettings()`** (line 23): Reads JSON, merges with `DEFAULT_SETTINGS`. Returns
  defaults if file missing or corrupt.
- **`saveSettings(settings)`** (line 35): Writes formatted JSON, creates dirs as needed.
- **`getSettingsPath()`** (line 44): Returns absolute path for the installer UI.

---

## Plugin API

The `src/api/` directory contains the runtime API available to plugins inside the browser.

### bridge.ts -- ES6 Proxy Bridge Interception

**File**: `src/api/bridge.ts`

Root's WebRTC layer communicates with .NET through two globals:

| Global | Direction | Purpose |
|--------|-----------|---------|
| `window.__nativeToWebRtc` | .NET --> Browser | C# host controlling WebRTC |
| `window.__webRtcToNative` | Browser --> .NET | JS notifying native host |

#### `installBridgeProxy()` -- Line 49

Handles two scenarios:

1. **Immediate** (lines 53-71): If globals exist, wrap them now.
2. **Deferred** (lines 76-102): Install `Object.defineProperty` setter traps so assignment
   by Root automatically wraps the value.

```typescript
// Deferred proxy example (src/api/bridge.ts:80-90):
let _ntw: INativeToWebRtc | undefined;
Object.defineProperty(window, "__nativeToWebRtc", {
  get: () => _ntw,
  set: (val: INativeToWebRtc) => {
    _ntw = createBridgeProxy(val, "bridge:nativeToWebRtc");
  },
  configurable: true,
});
```

#### `createBridgeProxy<T>(target, eventPrefix)` -- Line 21

ES6 Proxy with a `get` trap. For function properties, returns a wrapper that creates a
`BridgeEvent`, emits it through the plugin loader, and either returns the event's
`returnValue` (if cancelled) or calls the original function with potentially modified args.

#### `setPluginLoader(loader)` -- Line 17

Wires the loader into the bridge module. Called once during initialization.

---

### css.ts -- CSS Injection Utilities

**File**: `src/api/css.ts`

Manages `<style>` elements in `<head>`, prefixed with `uprooted-css-` (line 8).

- **`injectCss(id, css)`** (line 14): Creates or replaces a `<style>` element.
- **`removeCss(id)`** (line 30): Removes a style element by ID.
- **`removeAllCss()`** (line 39): Removes all `uprooted-css-*` style elements.

---

### dom.ts -- DOM Manipulation Helpers

**File**: `src/api/dom.ts`

- **`waitForElement<T>(selector, timeout?)`** (line 9): Returns a Promise that resolves
  when a matching element appears. Uses `MutationObserver`, default 10s timeout.
- **`observe(target, callback, options?)`** (line 44): `MutationObserver` wrapper returning
  a disconnect function.
- **`nextFrame()`** (line 57): Promise resolving on next `requestAnimationFrame`.

---

### native.ts -- Native Bridge Call Wrappers

**File**: `src/api/native.ts`

- **`getCurrentTheme()`** (line 11): Reads `data-theme` from `<html>`.
- **`setCssVariable(name, value)`** (line 19): Sets a CSS variable on `:root`.
- **`removeCssVariable(name)`** (line 26): Removes a CSS variable override.
- **`setCssVariables(vars)`** (line 33): Batch-sets CSS variables from a record.
- **`nativeLog(message)`** (line 42): Sends `[Uprooted] ${message}` through
  `window.__webRtcToNative.log()` to .NET logs.

---

### index.ts -- API Barrel Export

**File**: `src/api/index.ts`

```typescript
export * as css from "./css.js";
export * as dom from "./dom.js";
export * as native from "./native.js";
export * as bridge from "./bridge.js";
```

See [Plugin Getting Started](plugins/GETTING_STARTED.md) for writing plugins that use
this API.

---

## Built-in Plugins

Registered in `preload.ts` (lines 48-51). All follow the `UprootedPlugin` interface.

### sentry-blocker -- Telemetry Blocking

**File**: `src/plugins/sentry-blocker/index.ts`

Blocks Sentry error tracking. Root sends telemetry to Sentry with `sendDefaultPii: true`
(IP addresses), `replaysOnErrorSampleRate: 0.25` (DOM replays with mouse and inputs),
and leaks Bearer tokens in request breadcrumbs.

#### Why Fetch-Level Blocking?

Sentry initializes at module evaluation time in Root's bundle, before Uprooted runs.
Overriding `Sentry.init` is not viable. Instead, this plugin wraps the network APIs.

#### Intercepted APIs (`start()`, line 34)

1. **`window.fetch`** (lines 38-46): Returns a fake `Response(null, { status: 200 })` for
   Sentry URLs. Sentry sees success and does not retry.
2. **`XMLHttpRequest.prototype.open`** (lines 49-62): Redirects Sentry requests to
   `about:blank`.
3. **`navigator.sendBeacon`** (lines 65-73): Returns `true` without sending data.

URL detection via `isSentryUrl()` (line 23): checks if URL string contains `"sentry.io"`.
Handles `string`, `URL`, and `Request` input types.

#### Cleanup (`stop()`, line 78)

Restores all three original functions and logs total blocked count.

---

### themes -- CSS Variable Theme Engine

**File**: `src/plugins/themes/index.ts`

Root's color system uses CSS variables (`--rootsdk-*`). This plugin overrides them.

#### Theme Definitions

Loaded from `themes.json` at build time. Each theme has `name`, `display_name`, and a
`variables` record mapping CSS variable names to values.

#### Color Math (lines 48-77)

- `parseHex(hex)` / `toHex(r,g,b)` -- hex <-> RGB conversion
- `darken(hex, percent)` / `lighten(hex, percent)` -- shade adjustment
- `luminance(hex)` -- WCAG relative luminance

#### `generateCustomVariables(accent, bg)` -- Line 82

Derives 10 CSS variables from two colors. Uses luminance to detect dark/light background:

```typescript
export function generateCustomVariables(accent: string, bg: string): Record<string, string> {
  const isDark = luminance(bg) < 0.3;
  return {
    "--rootsdk-brand-primary": accent,
    "--rootsdk-brand-secondary": lighten(accent, 15),
    "--rootsdk-brand-tertiary": darken(accent, 15),
    "--rootsdk-background-primary": bg,
    "--rootsdk-background-secondary": lighten(bg, 8),
    "--rootsdk-background-tertiary": darken(bg, 8),
    "--rootsdk-input": darken(bg, 5),
    "--rootsdk-border": lighten(bg, 18),
    "--rootsdk-link": lighten(accent, 30),
    "--rootsdk-muted": isDark ? lighten(bg, 25) : darken(bg, 25),
  };
}
```

#### `start()` -- Line 116

Flushes all known variables, reads theme name from settings, applies the matching theme's
variables (or generates custom variables from accent + background colors).

#### `stop()` -- Line 140

Removes all known CSS variables, restoring Root's defaults.

---

### settings-panel -- In-App Settings UI

Three files: `index.ts` (plugin entry), `panel.ts` (DOM discovery), `components.ts` (UI).

#### index.ts -- Plugin Entry

**File**: `src/plugins/settings-panel/index.ts`

On `start()`, retrieves `PluginLoader` from `window.__UPROOTED_LOADER__` and passes it to
`startObserving()`. On `stop()`, calls `stopObserving()`.

#### panel.ts -- Sidebar Discovery and Injection

**File**: `src/plugins/settings-panel/panel.ts`

Uses a Vencord-inspired MutationObserver approach to detect Root's settings page.

**`startObserving()`** (line 49): Sets up MutationObserver on `document.body` with 80ms
debounce. Calls `tryInject()` on every mutation.

**`tryInject()`** (line 106) -- Core injection logic:

1. Guard: If `[data-uprooted]` elements exist, skip.
2. Find `"APP SETTINGS"` text via `findByExactText()` -- confirms settings page is open.
3. Find `"Advanced"` text -- the last sidebar item, used as insertion point.
4. `findSettingsLayout()` (line 170): Walks up DOM to find a flex-row/grid ancestor with
   sidebar + content children.
5. `findItemElement()` (line 234): Walks up from "Advanced" text to its nav item parent.
6. `injectSidebarSection()` (line 284): Clones the header as "UPROOTED", creates nav items
   ("Uprooted", "Plugins", "Themes") by cloning the template, strips React attributes.
7. `injectVersionText()` (line 489): Appends version text near Root's version display.

**Content swapping** -- `onUprootedItemClick()` (line 397): Hides Root's content panel,
builds the requested page, inserts it as a sibling. `onSidebarClick()` (line 464) restores
Root's panel when a Root sidebar item is clicked.

#### components.ts -- UI Components

**File**: `src/plugins/settings-panel/components.ts`

**Primitive components**:

| Function | Line | Purpose |
|----------|------|---------|
| `createToggle(checked, onChange)` | 20 | Checkbox toggle switch |
| `createSelect(options, selected, onChange)` | 41 | Dropdown select |
| `createTextarea(value, placeholder, onChange)` | 62 | Text input, 300ms debounce |
| `createRow(label, description, control)` | 83 | Settings row with label + control |
| `createSection(label)` | 111 | Section header |

**Page builders**:

- **`buildUprootedPage()`** (line 126): About page with version, description, links.
- **`buildPluginsPage(loader)`** (line 183): Lists plugins with toggle switches and status
  badges. Shows privacy notice for sentry-blocker. Accesses loader internals via `any` cast.
- **`buildThemesPage(loader)`** (line 297): Theme dropdown with live preview, theme cards
  with color swatches, custom theme color pickers, and custom CSS textarea.

---

## Type Definitions

### src/types/bridge.ts -- Bridge Interfaces

**File**: `src/types/bridge.ts`

#### Branded GUID Types (lines 1-2)

```typescript
export type UserGuid = string & { readonly __brand: "UserGuid" };
export type DeviceGuid = string & { readonly __brand: "DeviceGuid" };
```

Opaque branded types preventing accidental mixing of user/device IDs at compile time.

#### Supporting Types (lines 4-58)

```typescript
export type TileType = "camera" | "screen" | "audio";
export type Theme = "dark" | "light" | "pure-dark";
export type ScreenQualityMode = "motion" | "detail" | "auto";
export type Codec = string;
export type WebRtcPermission = Record<string, boolean>;

export interface Coordinates { x: number; y: number; }
export interface IUserResponse { userId: UserGuid; displayName: string; avatarUrl?: string; [key: string]: unknown; }
export interface VolumeBoosterSettings { enabled: boolean; gain: number; }
export interface WebRtcError { code: string; message: string; }
export interface IPacket { type: string; data: unknown; }
export interface UserMediaStreamConstraints { audio?: MediaTrackConstraints | boolean; video?: MediaTrackConstraints | boolean; }
export interface DisplayMediaStreamConstraints { audio?: MediaTrackConstraints | boolean; video?: MediaTrackConstraints | boolean; }

export interface InitializeDesktopWebRtcPayload {
  token: string; channelId: string; communityId: string;
  userId: UserGuid; deviceId: DeviceGuid; theme: Theme;
  [key: string]: unknown;
}
```

#### `IWebRtcToNative` (line 64) -- Browser to .NET

Methods called by JavaScript to notify the .NET host. Key methods:

| Method | Purpose |
|--------|---------|
| `initialized()` | WebRTC session ready |
| `disconnected()` | Session disconnected |
| `localMuteWasSet(isMuted)` | Mute state changed |
| `localDeafenWasSet(isDeafened)` | Deafen state changed |
| `localAudioStarted/Stopped/Failed()` | Audio lifecycle |
| `localVideoStarted/Stopped/Failed()` | Video lifecycle |
| `localScreenStarted/Stopped/Failed()` | Screen share lifecycle |
| `getUserProfile(userId)` | Fetch user profile (async) |
| `getUserProfiles(userIds)` | Fetch multiple profiles (async) |
| `setSpeaking(isSpeaking, deviceId, userId)` | Speaking indicator |
| `setHandRaised(isHandRaised, deviceId, userId)` | Hand raise state |
| `failed(error)` | Report WebRTC error |
| `setAdminMute/setAdminDeafen(deviceId, state)` | Admin controls |
| `kickPeer(userId)` | Kick a peer |
| `viewProfileMenu/viewContextMenu(userId, coords, ...)` | UI menus |
| `log(message)` | Send log to .NET |

#### `INativeToWebRtc` (line 105) -- .NET to Browser

Methods called by the C# host to control WebRTC. Key methods:

| Method | Purpose |
|--------|---------|
| `initialize(state)` | Initialize WebRTC with token, channel, user info |
| `disconnect()` | End the session |
| `setIsVideoOn/setIsScreenShareOn/setIsAudioOn(state)` | Toggle media |
| `updateVideoDeviceId/updateAudioInputDeviceId/...` | Change devices |
| `updateProfile(user)` | Update user profile |
| `setMute/setDeafen/setHandRaised(state)` | User state |
| `setTheme(theme)` | Change UI theme |
| `setNoiseGateThreshold/setDenoisePower(value)` | Audio processing |
| `setScreenQualityMode(mode)` | Screen share quality |
| `setPreferredCodecs(codecs)` | Codec preference |
| `setTileVolume/setOutputVolume/setInputVolume(value)` | Volume controls |
| `customizeVolumeBooster(settings)` | Volume booster config |
| `kick(userId)` | Kick a user |
| `receivePacket/receiveRawPacket(data)` | Data channel packets |
| `nativeLoopbackAudioStarted/receiveNativeLoopbackAudioData/...` | Loopback audio |

See [Bridge Reference](plugins/BRIDGE_REFERENCE.md) for the full method catalog with
parameter types and interception examples.

---

### src/types/plugin.ts -- Plugin Interfaces

**File**: `src/types/plugin.ts`

```typescript
export interface Author {
  name: string;
  id?: string;
}

export interface Patch {
  bridge: "nativeToWebRtc" | "webRtcToNative";
  method: string;
  before?(args: unknown[]): boolean | void | Promise<boolean | void>;
  after?(result: unknown, args: unknown[]): void | Promise<void>;
  replace?(...args: unknown[]): unknown | Promise<unknown>;
}

export type SettingField =
  | { type: "boolean"; default: boolean; description: string }
  | { type: "string"; default: string; description: string }
  | { type: "number"; default: number; description: string; min?: number; max?: number }
  | { type: "select"; default: string; description: string; options: string[] };

export interface SettingsDefinition {
  [key: string]: SettingField;
}

export interface UprootedPlugin {
  name: string;
  description: string;
  version: string;
  authors: Author[];
  start?(): void | Promise<void>;
  stop?(): void | Promise<void>;
  patches?: Patch[];
  css?: string;
  settings?: SettingsDefinition;
}
```

Patch priority: `replace` > `before` > `after`. A `replace` patch cancels the original
call and provides the return value. A `before` patch can cancel by returning `false`.

---

### src/types/root.ts -- Window Augmentation

**File**: `src/types/root.ts`

Extends the global `Window` with Root's runtime globals and Uprooted's injected properties:

```typescript
declare global {
  interface Window {
    // Root's bridge globals (set by DotNetBrowser)
    __nativeToWebRtc: INativeToWebRtc;
    __webRtcToNative: IWebRtcToNative;
    __mediaManager: IMediaManager;     // getDevices(kind?) -> Promise<string>
    __rootApiBaseUrl: string;
    __rootSdkBridgeWebToNative: Record<string, (...args: unknown[]) => unknown>;

    // Uprooted injections
    __UPROOTED_SETTINGS__: UprootedSettings;
    __UPROOTED_VERSION__: string;
    __UPROOTED_LOADER__: PluginLoader;
  }
}
```

---

### src/types/settings.ts -- Settings Interfaces

**File**: `src/types/settings.ts`

```typescript
export interface PluginSettings {
  enabled: boolean;
  config: Record<string, unknown>;
}

export interface UprootedSettings {
  enabled: boolean;                          // Global kill switch
  plugins: Record<string, PluginSettings>;   // Per-plugin settings by name
  customCss: string;                         // Global custom CSS
}

export const DEFAULT_SETTINGS: UprootedSettings = {
  enabled: true,
  plugins: {},    // Empty = all plugins default to enabled
  customCss: "",
};
```

When `plugins[name]` is not present, `PluginLoader` defaults `enabled` to `true`
(see `pluginLoader.ts` line 99).

---

## Build System

**File**: `scripts/build.ts`

Uses [esbuild](https://esbuild.github.io/) to produce two output files.

### esbuild Configuration (line 45)

```typescript
const ctx = await esbuild.context({
  entryPoints: [path.join(SRC, "core", "preload.ts")],
  bundle: true,
  format: "iife",
  globalName: "Uprooted",
  outfile: path.join(DIST, "uprooted-preload.js"),
  platform: "browser",
  target: "chrome120",
  sourcemap: true,
  define: {
    __UPROOTED_VERSION__: JSON.stringify(/* from package.json */),
  },
  external: ["node:fs", "node:path"],
});
```

| Option | Value | Rationale |
|--------|-------|-----------|
| `format` | `"iife"` | Self-executing, no module system needed |
| `globalName` | `"Uprooted"` | Exposes exports on `window.Uprooted` |
| `platform` | `"browser"` | Browser APIs, no Node.js polyfills |
| `target` | `"chrome120"` | DotNetBrowser's Chromium version |
| `external` | `["node:fs", "node:path"]` | CLI-only modules excluded from bundle |

### CSS Collection (line 18)

`collectPluginCss()` recursively walks `src/plugins/` for `.css` files and concatenates
them with source path comments.

### Watch Mode

```bash
pnpm build --watch    # ctx.watch() for incremental rebuild
```

CSS collection runs once at startup. New CSS files require a restart.

### Output

```
dist/
  uprooted-preload.js      # IIFE bundle
  uprooted-preload.js.map  # Source map
  uprooted.css              # Combined plugin CSS
```

Loaded by `file:///` URLs from injected HTML tags.

### Build Commands

```bash
pnpm build              # Production build
npx tsx scripts/build.ts # Direct invocation
```
