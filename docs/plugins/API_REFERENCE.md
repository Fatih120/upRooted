# API Reference

Complete reference for the Uprooted plugin API. All types are defined in `src/types/plugin.ts` and the API modules under `src/api/`.

## Table of Contents

- [UprootedPlugin Interface](#uprootedplugin-interface)
- [Patch Interface](#patch-interface)
- [Settings Definition](#settings-definition)
- [CSS API](#css-api)
- [DOM API](#dom-api)
- [Native API](#native-api)
- [Bridge API](#bridge-api)
- [Global Objects](#global-objects)
- [PluginLoader Class](#pluginloader-class)
- [BridgeEvent Interface](#bridgeevent-interface)

---

## UprootedPlugin Interface

Every plugin must export a default object satisfying `UprootedPlugin`. This is the contract between your plugin and the Uprooted loader.

```typescript
interface UprootedPlugin {
  name: string;           // Unique identifier (used as key in settings, CSS IDs, etc.)
  description: string;    // Human-readable description shown in the plugins page
  version: string;        // Semver version string
  authors: Author[];      // List of plugin authors

  start?(): void | Promise<void>;   // Called when the plugin is enabled
  stop?(): void | Promise<void>;    // Called when the plugin is disabled

  patches?: Patch[];                // Bridge method intercepts (applied on start, removed on stop)
  css?: string;                     // CSS string injected while the plugin is active
  settings?: SettingsDefinition;    // Plugin-specific settings schema
}

interface Author {
  name: string;
  id?: string;   // Optional unique identifier
}
```

### Required Fields

| Field | Type | Description |
|-------|------|-------------|
| `name` | `string` | Unique plugin identifier. Used as the key in settings JSON, CSS element IDs (`uprooted-css-plugin-{name}`), and log messages. Must be unique across all registered plugins. |
| `description` | `string` | Shown in the Uprooted settings panel plugins page. Keep it short (one sentence). |
| `version` | `string` | Plugin version, displayed alongside the name in the plugins page. |
| `authors` | `Author[]` | At least one author. The `id` field is optional. |

### Optional Fields

| Field | Type | Description |
|-------|------|-------------|
| `start` | `() => void \| Promise<void>` | Lifecycle hook called when the plugin starts. Use this for DOM setup, event listeners, or any initialization. Can be async. |
| `stop` | `() => void \| Promise<void>` | Lifecycle hook called when the plugin stops. Clean up anything your `start` created — DOM nodes, intervals, event listeners. Can be async. |
| `patches` | `Patch[]` | Array of bridge method intercepts. Automatically installed on start and removed on stop. See [Patch Interface](#patch-interface). |
| `css` | `string` | Raw CSS string. Injected as a `<style>` element on start, removed on stop. Element ID: `uprooted-css-plugin-{name}`. |
| `settings` | `SettingsDefinition` | Defines configurable fields that appear in the settings UI. Values are persisted in the settings JSON. See [Settings Definition](#settings-definition). |

### Lifecycle Order

When a plugin starts:
1. `patches` are installed (bridge intercepts become active)
2. `css` is injected into the page
3. `start()` is called

When a plugin stops:
1. `stop()` is called
2. `css` is removed from the page
3. All event handlers for this plugin's patches are removed

---

## Patch Interface

Patches let you intercept bridge method calls without touching the bridge directly. The loader installs and removes them automatically with the plugin lifecycle.

```typescript
interface Patch {
  bridge: "nativeToWebRtc" | "webRtcToNative";
  method: string;
  before?(args: unknown[]): boolean | void | Promise<boolean | void>;
  after?(result: unknown, args: unknown[]): void | Promise<void>;
  replace?(...args: unknown[]): unknown | Promise<unknown>;
}
```

### Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `bridge` | `"nativeToWebRtc" \| "webRtcToNative"` | Yes | Which bridge to intercept. See [BRIDGE_REFERENCE.md](BRIDGE_REFERENCE.md). |
| `method` | `string` | Yes | The method name on the bridge to intercept. Must match exactly. |
| `before` | `(args: unknown[]) => boolean \| void` | No | Called before the original method executes. Return `false` to cancel the call. You can mutate `args` in-place to modify what the original receives. |
| `after` | `(result: unknown, args: unknown[]) => void` | No | **Not yet implemented.** Defined in the interface for future use. Currently, the loader only invokes `before` and `replace` handlers. |
| `replace` | `(...args: unknown[]) => unknown` | No | Completely replaces the original method. The original is never called. Your return value is used instead. |

### Execution Priority

- `replace` takes priority over `before`. If `replace` is set, it runs instead of the original method, and `before` is ignored.
- `before` runs if `replace` is not set. If it returns `false`, the original method is skipped.
- `after` is defined in the interface but **not yet invoked by the loader**. It will be implemented in a future version.
- Multiple plugins can patch the same method. They execute in registration order. If any plugin cancels the call, later plugins' handlers for that method are skipped.

### Examples

```typescript
// Log every theme change before it happens
{
  bridge: "nativeToWebRtc",
  method: "setTheme",
  before(args) {
    console.log("Theme changing to:", args[0]);
    // Return nothing (undefined) to allow the call to proceed
  }
}

// Block kick commands
{
  bridge: "nativeToWebRtc",
  method: "kick",
  before(args) {
    console.log("Blocked kick for user:", args[0]);
    return false;  // Cancel the call
  }
}

// Replace disconnect to add custom cleanup
{
  bridge: "nativeToWebRtc",
  method: "disconnect",
  replace() {
    console.log("Custom disconnect handler");
    // Original disconnect is NOT called
  }
}
```

---

## Settings Definition

Define configurable settings for your plugin. These are rendered in the Uprooted settings panel and persisted in the settings JSON file.

```typescript
interface SettingsDefinition {
  [key: string]: SettingField;
}

type SettingField =
  | { type: "boolean"; default: boolean; description: string }
  | { type: "string"; default: string; description: string }
  | { type: "number"; default: number; description: string; min?: number; max?: number }
  | { type: "select"; default: string; description: string; options: string[] };
```

### Field Types

| Type | Rendered As | Value Type | Extra Fields |
|------|------------|------------|--------------|
| `boolean` | Toggle switch | `boolean` | None |
| `string` | Text input | `string` | None |
| `number` | Number input | `number` | `min?`, `max?` |
| `select` | Dropdown | `string` | `options: string[]` |

### Reading Settings at Runtime

Plugin settings are stored in `window.__UPROOTED_SETTINGS__`. Access your plugin's config:

```typescript
const settings = window.__UPROOTED_SETTINGS__?.plugins?.["my-plugin"]?.config;
const myValue = settings?.myKey as string ?? "default-fallback";
```

### Example

```typescript
settings: {
  enabled: {
    type: "boolean",
    default: true,
    description: "Enable this feature"
  },
  username: {
    type: "string",
    default: "",
    description: "Your display name override"
  },
  volume: {
    type: "number",
    default: 50,
    description: "Notification volume",
    min: 0,
    max: 100
  },
  theme: {
    type: "select",
    default: "auto",
    description: "Color scheme",
    options: ["auto", "dark", "light"]
  }
}
```

---

## CSS API

Defined in `src/api/css.ts`. Manages `<style>` elements in the page head.

### `injectCss(id, css)`

Inject a CSS string into the page. If a style with the same ID already exists, its content is replaced.

```typescript
function injectCss(id: string, css: string): void
```

- **id** — Unique identifier. The actual element ID will be `uprooted-css-{id}`.
- **css** — Raw CSS string to inject.

> **Note:** When using the `css` field on your plugin, the loader calls `injectCss("plugin-{name}", css)` automatically. Use this function directly only if you need dynamic CSS injection in `start()`.

### `removeCss(id)`

Remove a previously injected CSS style element by ID.

```typescript
function removeCss(id: string): void
```

### `removeAllCss()`

Remove all Uprooted-injected CSS from the page (all elements with IDs starting with `uprooted-css-`).

```typescript
function removeAllCss(): void
```

### Example

```typescript
import { injectCss, removeCss } from "../api/css.js";

// Inject some custom styles
injectCss("my-plugin-highlight", `
  .some-element { background: red !important; }
`);

// Later, remove them
removeCss("my-plugin-highlight");
```

---

## DOM API

Defined in `src/api/dom.ts`. Utilities for working with Root's DOM.

### `waitForElement(selector, timeout?)`

Wait for an element matching a CSS selector to appear in the DOM. Uses `MutationObserver` internally.

```typescript
function waitForElement<T extends Element = Element>(
  selector: string,
  timeout?: number   // default: 10000 (10 seconds)
): Promise<T>
```

- Returns a `Promise` that resolves with the element when found.
- Rejects with an `Error` if the element doesn't appear within the timeout.
- If the element already exists, resolves immediately.

```typescript
import { waitForElement } from "../api/dom.js";

const sidebar = await waitForElement<HTMLDivElement>(".sidebar-container");
sidebar.style.border = "2px solid red";
```

### `observe(target, callback, options?)`

Observe a DOM element for mutations. Thin wrapper around `MutationObserver`.

```typescript
function observe(
  target: Element,
  callback: MutationCallback,
  options?: MutationObserverInit   // default: { childList: true, subtree: true }
): () => void   // Returns a disconnect function
```

- Returns a function that disconnects the observer when called. Store this in your plugin and call it in `stop()`.

```typescript
import { observe } from "../api/dom.js";

let disconnect: (() => void) | null = null;

// In start():
const container = document.querySelector(".chat-messages");
if (container) {
  disconnect = observe(container, (mutations) => {
    for (const mutation of mutations) {
      console.log("DOM changed:", mutation.addedNodes.length, "nodes added");
    }
  });
}

// In stop():
disconnect?.();
disconnect = null;
```

### `nextFrame()`

Wait for the next animation frame. Useful for batching DOM reads after writes.

```typescript
function nextFrame(): Promise<void>
```

```typescript
import { nextFrame } from "../api/dom.js";

element.style.width = "100px";
await nextFrame();
const computedWidth = element.getBoundingClientRect().width;
```

---

## Native API

Defined in `src/api/native.ts`. Utilities for interacting with Root's native layer.

### `getCurrentTheme()`

Get the current Root theme from the `data-theme` attribute on `<html>`.

```typescript
function getCurrentTheme(): string | null
```

Returns `"dark"`, `"light"`, `"pure-dark"`, or `null` if not set.

### `setCssVariable(name, value)`

Set a CSS custom property on `:root`. This is how themes work — overriding `--rootsdk-*` variables.

```typescript
function setCssVariable(name: string, value: string): void
```

```typescript
setCssVariable("--rootsdk-brand-primary", "#ff0000");
```

### `setCssVariables(vars)`

Set multiple CSS variables at once.

```typescript
function setCssVariables(vars: Record<string, string>): void
```

```typescript
setCssVariables({
  "--rootsdk-brand-primary": "#ff0000",
  "--rootsdk-background-primary": "#1a1a2e",
});
```

### `removeCssVariable(name)`

Remove a CSS variable override, reverting to the stylesheet default.

```typescript
function removeCssVariable(name: string): void
```

### `nativeLog(message)`

Send a log message through Root's native bridge. The message appears in .NET/Root logs, prefixed with `[Uprooted]`.

```typescript
function nativeLog(message: string): void
```

This calls `window.__webRtcToNative?.log()` under the hood. It's the only way to get log output visible outside the Chromium context (since there's no DevTools).

```typescript
nativeLog("Plugin initialized successfully");
// Appears in Root logs as: [Uprooted] Plugin initialized successfully
```

---

## Bridge API

Defined in `src/api/bridge.ts`. You generally **do not call bridge methods directly**. Instead, use [Patch](#patch-interface) definitions on your plugin to intercept bridge traffic.

### How It Works

Uprooted replaces Root's two bridge globals (`window.__nativeToWebRtc` and `window.__webRtcToNative`) with ES6 Proxy wrappers. When any code calls a bridge method:

1. The Proxy intercepts the call
2. A `BridgeEvent` is created with the method name and arguments
3. The event is emitted to all registered plugin patch handlers
4. If no handler cancels the event, the original method is called
5. The return value is passed back to the caller

This is transparent to Root's own code — it doesn't know the bridges are proxied.

### Bridge Installation

The proxies are installed at startup via `installBridgeProxy()`. This handles both cases:
- **Immediate:** If the bridge objects already exist on `window`, they're wrapped right away.
- **Deferred:** If Root assigns them later, `Object.defineProperty` setters intercept the assignment and wrap the value automatically.

### Direct Access

If you need the raw bridge objects (rare), they're available on `window`:

```typescript
// These are the PROXIED versions — your patches will still fire
window.__nativeToWebRtc?.setTheme("dark");
window.__webRtcToNative?.log("hello");
```

There is no way to access the un-proxied originals from plugin code.

---

## Global Objects

These globals are available on `window` inside Root's Chromium context.

### Uprooted Globals

| Global | Type | Description |
|--------|------|-------------|
| `window.__UPROOTED_SETTINGS__` | `UprootedSettings` | The settings object loaded at startup. Contains `enabled`, `plugins` (per-plugin config), and `customCss`. |
| `window.__UPROOTED_VERSION__` | `string` | Uprooted version string (e.g. `"0.1.7"`). Set during initialization. |
| `window.__UPROOTED_LOADER__` | `PluginLoader` | The active plugin loader instance. Exposed for the settings panel; avoid depending on this in regular plugins. |

### Root Bridge Globals

| Global | Type | Description |
|--------|------|-------------|
| `window.__nativeToWebRtc` | `INativeToWebRtc` | Native-to-WebRTC bridge (proxied by Uprooted). C# host calls these to control WebRTC. |
| `window.__webRtcToNative` | `IWebRtcToNative` | WebRTC-to-native bridge (proxied by Uprooted). JS calls these to notify the C# host. |

### Settings Structure

```typescript
interface UprootedSettings {
  enabled: boolean;                              // Global kill switch
  plugins: Record<string, PluginSettings>;       // Per-plugin settings
  customCss: string;                             // Global custom CSS
}

interface PluginSettings {
  enabled: boolean;                              // Whether this plugin should start
  config: Record<string, unknown>;               // Plugin-specific config values
}
```

---

## PluginLoader Class

Defined in `src/core/pluginLoader.ts`. Manages plugin registration, lifecycle, and bridge event dispatch.

### Methods

#### `register(plugin)`

Register a plugin. Does not start it. If a plugin with the same name is already registered, the call is ignored with a warning.

```typescript
register(plugin: UprootedPlugin): void
```

#### `start(name)`

Start a plugin by name. Installs patches, injects CSS, and calls `start()`. No-op if already active.

```typescript
start(name: string): Promise<void>
```

#### `stop(name)`

Stop a plugin by name. Calls `stop()`, removes CSS, and uninstalls all patch handlers. No-op if not active.

```typescript
stop(name: string): Promise<void>
```

#### `startAll()`

Start all registered plugins that are enabled in settings. Plugins default to enabled if not explicitly configured.

```typescript
startAll(): Promise<void>
```

#### `emit(eventName, event)`

Emit a bridge event. Called by the bridge proxy — you don't call this directly.

```typescript
emit(eventName: "bridge:nativeToWebRtc" | "bridge:webRtcToNative", event: BridgeEvent): void
```

---

## BridgeEvent Interface

The event object passed to patch handlers via the loader's event system.

```typescript
interface BridgeEvent {
  method: string;        // The bridge method that was called
  args: unknown[];       // The arguments passed to the method
  cancelled: boolean;    // Set to true to prevent the original call
  returnValue?: unknown; // Set by replace handlers; used as the return value if cancelled
}
```

- `method` and `args` are set by the bridge proxy.
- `cancelled` starts as `false`. Set it to `true` (or return `false` from `before`) to prevent the original method from executing.
- `returnValue` is only used when `cancelled` is `true`. Set it in a `replace` handler to provide a custom return value.
