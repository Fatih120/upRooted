# TypeScript Patterns

Code patterns used in the Uprooted TypeScript layer (`src/` directory). All code runs in Root's embedded Chromium context.

## Plugin Definition Pattern

Plugins export a default object satisfying the `UprootedPlugin` interface.

```typescript
import type { UprootedPlugin } from "../types/plugin.js";

export default {
  name: "my-plugin",
  description: "What this plugin does",
  version: "0.1.0",
  authors: [{ name: "Author Name" }],

  // Lifecycle hooks (optional)
  start() {
    console.log("[Uprooted:my-plugin] Started");
  },
  stop() {
    console.log("[Uprooted:my-plugin] Stopped");
  },

  // Bridge method patches (optional)
  patches: [
    {
      bridge: "nativeToWebRtc",
      method: "setMute",
      before(event) {
        console.log("Mute called with:", event.args);
        // Return false to cancel the original call
        // Modify event.args to change arguments
      },
    },
  ],

  // CSS injection (optional)
  css: `:root { --custom-var: #ff0000; }`,
} satisfies UprootedPlugin;
```

## Bridge Interception Pattern

ES6 Proxy wraps Root's bridge globals for plugin patches.

```typescript
function createBridgeProxy<T extends object>(
  target: T,
  eventPrefix: "bridge:nativeToWebRtc" | "bridge:webRtcToNative",
): T {
  return new Proxy(target, {
    get(obj, prop, receiver) {
      const original = Reflect.get(obj, prop, receiver);
      if (typeof original !== "function") return original;

      return (...args: unknown[]) => {
        const event: BridgeEvent = {
          method: String(prop),
          args,
          cancelled: false,
        };

        pluginLoader?.emit(eventPrefix, event);

        if (event.cancelled) return event.returnValue;
        return (original as Function).apply(obj, event.args);
      };
    },
  });
}
```

**Deferred installation** handles cases where Root assigns globals after preload:

```typescript
// Object.defineProperty trap for lazy proxy wrapping
Object.defineProperty(window, "__nativeToWebRtc", {
  configurable: true,
  set(value) {
    // Root just assigned the bridge — wrap it immediately
    Object.defineProperty(window, "__nativeToWebRtc", {
      value: createBridgeProxy(value, "bridge:nativeToWebRtc"),
      writable: true,
      configurable: true,
    });
  },
  get() { return undefined; },
});
```

## Patch Execution Priority

When a bridge method is called, patches execute in this order:

1. **`replace`** takes priority over `before`. If `replace` is set, it runs INSTEAD of the original call and `before` is ignored.
2. **`before`** runs if no `replace`. Returning `false` cancels the original call. Can modify `event.args`.
3. **Original call** executes if not cancelled, with (potentially modified) args.
4. **`after`** is defined in the `Patch` interface but is **NOT YET IMPLEMENTED** by the loader. Plugins defining `after` handlers get no runtime behavior.

## Plugin Lifecycle Pattern

`PluginLoader` manages plugin registration, start/stop, and event routing.

```typescript
// Registration (does not start)
loader.register(myPlugin);

// Start — installs patches, injects CSS, calls start()
await loader.start("my-plugin");

// Stop — calls stop(), removes CSS, uninstalls patches
await loader.stop("my-plugin");
```

**Known issue:** No concurrent operation guards. Calling `start()` immediately after `stop()` can cause race conditions. No `starting`/`stopping` Set to track in-progress operations.

## Settings Pattern

Settings persist to disk via JSON file. Browser has no filesystem access.

```typescript
// Settings inlined into HTML by patcher at install time:
// <script>window.__UPROOTED_SETTINGS__ = {...};</script>
const settings: UprootedSettings = window.__UPROOTED_SETTINGS__ ?? DEFAULT_SETTINGS;

// Runtime access
const pluginEnabled = settings.plugins?.["theme"]?.enabled ?? true;
const pluginConfig = settings.plugins?.["theme"]?.config;

// Runtime changes are in-memory only (lost on restart)
// Only the installer/CLI can write settings back to disk
```

**Known issue:** Settings merge uses shallow object spread. Nested `plugins` object may not merge correctly when user settings only contain some plugin configs.

## Logging Pattern

All console output prefixed with `[Uprooted]` for identification.

```typescript
// General messages
console.log("[Uprooted] Started plugin: themes");

// Plugin-specific messages
console.log("[Uprooted:sentry-blocker] Blocked fetch to sentry.io");
console.error("[Uprooted] Failed to start plugin:", err);
console.warn(`[Uprooted] Plugin "${name}" already registered, skipping.`);

// Counter reporting
console.log(`[Uprooted:sentry-blocker] Blocked ${blockedCount} requests`);
```

## DOM Utilities Pattern

TreeWalker-based text matching and MutationObserver for DOM changes.

```typescript
// Wait for element with timeout
export function waitForElement<T extends Element = Element>(
  selector: string,
  timeout = 10000,
): Promise<T> {
  // ... MutationObserver + polling ...
}

// Text-based DOM discovery
const walker = document.createTreeWalker(
  document.body,
  NodeFilter.SHOW_TEXT,
  { acceptNode: (node) =>
    node.textContent?.includes("APP SETTINGS")
      ? NodeFilter.FILTER_ACCEPT
      : NodeFilter.FILTER_REJECT
  }
);

// MutationObserver with debounce
let debounceTimer: number;
const observer = new MutationObserver(() => {
  clearTimeout(debounceTimer);
  debounceTimer = window.setTimeout(() => {
    tryInject();
  }, 80);
});
observer.observe(document.body, { childList: true, subtree: true });

// Identification attributes
element.setAttribute("data-uprooted", "section");
element.setAttribute("data-uprooted-page", "settings");
```

## CSS Injection Pattern

ID-keyed `<style>` elements for efficient injection and removal.

```typescript
// Inject CSS for a plugin
export function injectCss(id: string, css: string): void {
  let el = document.getElementById(`uprooted-css-${id}`);
  if (!el) {
    el = document.createElement("style");
    el.id = `uprooted-css-${id}`;
    document.head.appendChild(el);
  }
  el.textContent = css;
}

// Remove CSS when plugin stops
export function removeCss(id: string): void {
  document.getElementById(`uprooted-css-${id}`)?.remove();
}
```

## Import Conventions

```typescript
// Type-only imports
import type { UprootedPlugin, Patch } from "../types/plugin.js";
import type { UprootedSettings } from "../types/settings.js";

// Named imports
import { injectCss, removeCss } from "../api/css.js";

// All imports include .js extension (ES modules)
// Path aliases: @uprooted/* -> src/*

// Barrel exports (api/index.ts)
export * as css from "./css.js";
export * as dom from "./dom.js";
export * as native from "./native.js";
export * as bridge from "./bridge.js";
```

## Error Handling Pattern

Never let errors propagate — catch and log at every boundary.

```typescript
// Plugin lifecycle wraps start/stop
try {
  await plugin.start?.();
  this.activePlugins.add(name);
} catch (err) {
  console.error(`[Uprooted] Failed to start plugin "${name}":`, err);
}

// Top-level preload error displays red banner
try {
  // ... bootstrap ...
} catch (err) {
  // Show error banner at top of page with stack trace
  const banner = document.createElement("div");
  banner.style.cssText = "background:red;color:white;padding:8px;position:fixed;top:0;z-index:99999";
  banner.textContent = `[Uprooted] Fatal: ${err}`;
  document.body.prepend(banner);
}

// Settings load falls back to defaults
try {
  return JSON.parse(raw);
} catch {
  return { ...DEFAULT_SETTINGS };
}
```

## Build Configuration

```
Build tool:     esbuild
Format:         IIFE (Immediately Invoked Function Expression)
Target:         ES2022, Chrome 120
Output:         dist/uprooted-preload.js (~200KB) + dist/uprooted.css
Excludes:       node:fs, node:path (CLI-only modules)
Defines:        __UPROOTED_VERSION__ from package.json
TypeScript:     strict mode, isolatedModules, forceConsistentCasingInFileNames
```
