# TypeScript Plugin Building Reference

Complete reference for building Uprooted TypeScript plugins that run in Root's embedded Chromium context.

## UprootedPlugin Interface

```typescript
import type { UprootedPlugin } from "../types/plugin.js";

export default {
  name: "my-plugin",           // Unique kebab-case identifier
  description: "...",          // Short description
  version: "0.1.0",           // Semver
  authors: [{ name: "..." }], // Author list

  // Lifecycle (both optional)
  start() { },                // Called when plugin is enabled
  stop() { },                 // Called when plugin is disabled

  // Bridge patches (optional)
  patches: [ /* Patch[] */ ],

  // CSS injection (optional) — injected on start, removed on stop
  css: `/* CSS string */`,

  // Settings schema (optional)
  settings: { /* SettingsDefinition */ },
} satisfies UprootedPlugin;
```

## Lifecycle Order

1. `patches` installed (bridge proxy handlers registered)
2. `css` injected into `<head>` as `<style id="uprooted-css-plugin-{name}">`
3. `start()` called
4. *...plugin runs...*
5. `stop()` called
6. `css` removed from `<head>`
7. Patch handlers unregistered

## SettingField Types

```typescript
// Boolean toggle
{ type: "boolean", default: true, description: "Enable feature X" }

// Text input
{ type: "string", default: "hello", description: "Custom greeting" }

// Number with optional range
{ type: "number", default: 50, description: "Volume %", min: 0, max: 100 }

// Dropdown select
{ type: "select", default: "dark", description: "Theme", options: ["dark", "light", "custom"] }
```

Access at runtime: `window.__UPROOTED_SETTINGS__?.plugins?.["my-plugin"]?.config`

## CSS API

```typescript
import { css } from "../api/index.js";
// Or: import { injectCss, removeCss, removeAllCss } from "../api/css.js";

// Inject — creates/updates <style id="uprooted-css-{id}"> in <head>
css.injectCss(id: string, cssText: string): void

// Remove single
css.removeCss(id: string): void

// Remove ALL Uprooted CSS (prefix "uprooted-css-")
css.removeAllCss(): void
```

The plugin loader auto-injects `plugin.css` with ID `plugin-{name}` on start and removes on stop. Use the CSS API directly only for dynamic CSS beyond the static `css` property.

## DOM API

```typescript
import { dom } from "../api/index.js";
// Or: import { waitForElement, observe, nextFrame } from "../api/dom.js";

// Wait for selector to appear (MutationObserver + polling)
dom.waitForElement<T extends Element>(selector: string, timeout?: number): Promise<T>
// Default timeout: 10000ms. Rejects on timeout.

// Observe element mutations. Returns disconnect function.
dom.observe(
  target: Element,
  callback: MutationCallback,
  options?: MutationObserverInit  // Default: { childList: true, subtree: true }
): () => void

// Wait one animation frame (for batching DOM reads/writes)
dom.nextFrame(): Promise<void>
```

## Native API

```typescript
import { native } from "../api/index.js";
// Or import individual functions from "../api/native.js"

// Get Root's current theme from data attribute
native.getCurrentTheme(): string | null

// Set a single CSS variable on :root
native.setCssVariable(name: string, value: string): void

// Set multiple CSS variables at once
native.setCssVariables(vars: Record<string, string>): void

// Remove a CSS variable override (falls back to stylesheet default)
native.removeCssVariable(name: string): void

// Log through Root's native bridge (appears in .NET logs)
native.nativeLog(message: string): void
```

## Patch Interface

```typescript
interface Patch {
  bridge: "nativeToWebRtc" | "webRtcToNative";
  method: string;  // Method name on the bridge

  // Runs before the original. Return false to cancel the call.
  // Modify event.args to change arguments passed to original.
  before?(args: unknown[]): boolean | void | Promise<boolean | void>;

  // Replace the original call entirely. Return value becomes the call result.
  // If replace is set, before is ignored.
  replace?(...args: unknown[]): unknown | Promise<unknown>;

  // Called after the original (DEFINED IN INTERFACE BUT NOT YET IMPLEMENTED).
  // Plugins defining after() get no runtime behavior currently.
  after?(result: unknown, args: unknown[]): void | Promise<void>;
}
```

**Execution priority:**
1. `replace` — if defined, runs INSTEAD of original. `before` is skipped.
2. `before` — if no `replace`. Return `false` to cancel. Can mutate `event.args`.
3. Original call — runs with (possibly modified) args if not cancelled.
4. `after` — **unimplemented**, no-op.

## Plugin Registration

In `src/core/preload.ts`:

```typescript
import myPlugin from "../plugins/my-plugin/index.js";
// ... after other register calls:
loader.register(myPlugin);
```

The loader calls `startAll()` which starts all plugins where `settings.plugins[name].enabled !== false` (defaults to enabled).

## Minimal Template

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";

export default {
  name: "my-plugin",
  description: "Does a thing",
  version: "0.1.0",
  authors: [{ name: "You" }],

  start() {
    console.log("[Uprooted:my-plugin] Started");
  },

  stop() {
    console.log("[Uprooted:my-plugin] Stopped");
  },
} satisfies UprootedPlugin;
```

## Full-Featured Template

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";
import { css, dom, native } from "../../api/index.js";

let cleanup: (() => void) | null = null;

export default {
  name: "my-plugin",
  description: "Full-featured plugin example",
  version: "0.1.0",
  authors: [{ name: "You" }],

  settings: {
    enabled: { type: "boolean", default: true, description: "Enable this feature" },
    mode: { type: "select", default: "auto", description: "Operating mode", options: ["auto", "manual"] },
  },

  patches: [
    {
      bridge: "nativeToWebRtc",
      method: "setTheme",
      before(args) {
        console.log("[Uprooted:my-plugin] Theme changing to:", args[0]);
        // Don't return false — let the call through
      },
    },
  ],

  css: `
    .my-plugin-indicator {
      position: fixed;
      bottom: 8px;
      right: 8px;
      background: rgba(0,0,0,0.5);
      color: white;
      padding: 4px 8px;
      border-radius: 4px;
      font-size: 12px;
      z-index: 9999;
      pointer-events: none;
    }
  `,

  async start() {
    try {
      const config = window.__UPROOTED_SETTINGS__?.plugins?.["my-plugin"]?.config;
      const mode = (config?.mode as string) ?? "auto";

      // Wait for Root's UI to be ready
      const container = await dom.waitForElement<HTMLElement>(".app-container", 15000);

      // Create DOM element
      const indicator = document.createElement("div");
      indicator.className = "my-plugin-indicator";
      indicator.setAttribute("data-uprooted", "my-plugin");
      indicator.textContent = `Mode: ${mode}`;
      container.appendChild(indicator);

      // Set up observer for dynamic content
      const disconnect = dom.observe(container, () => {
        // React to DOM changes
      });

      cleanup = () => {
        indicator.remove();
        disconnect();
      };

      console.log("[Uprooted:my-plugin] Started in", mode, "mode");
    } catch (err) {
      console.error("[Uprooted:my-plugin] Failed to start:", err);
    }
  },

  stop() {
    cleanup?.();
    cleanup = null;
    console.log("[Uprooted:my-plugin] Stopped");
  },
} satisfies UprootedPlugin;
```

## Debugging Without DevTools

Root's embedded Chromium has no DevTools access. Use these techniques:

```typescript
// 1. Native log — appears in uprooted-hook.log
native.nativeLog("Debug: some value = " + JSON.stringify(value));

// 2. Console log with prefix (visible if console is somehow accessed)
console.log("[Uprooted:my-plugin] Debug:", data);

// 3. DOM indicator — visible overlay for state debugging
const debug = document.createElement("div");
debug.style.cssText = "position:fixed;top:0;left:0;background:red;color:white;padding:4px;z-index:99999;font-size:10px;pointer-events:none";
debug.textContent = `State: ${JSON.stringify(state)}`;
debug.setAttribute("data-uprooted", "debug");
document.body.appendChild(debug);

// 4. Error banner (preload.ts pattern for fatal errors)
const banner = document.createElement("div");
banner.style.cssText = "background:red;color:white;padding:8px;position:fixed;top:0;z-index:99999;width:100%";
banner.textContent = `[Uprooted] Fatal: ${err}`;
document.body.prepend(banner);
```

## Environment Constraints

- **No localStorage** — Root runs Chromium with `--incognito`. All localStorage cleared on launch.
- **No dynamic imports** — Bundle is IIFE format. All plugins must be statically imported in preload.ts.
- **No DevTools** — DotNetBrowser does not expose DevTools. Debug via DOM indicators and native logging.
- **Single JS context** — All plugins share one global scope. Use unique prefixes for globals.
- **No Node.js APIs** — Running in browser context. `node:fs`, `node:path` etc. are unavailable.
- **ES2022 target** — Chrome 120 Chromium. Modern syntax is fine (optional chaining, nullish coalescing, etc.).
- **Settings are read-only at runtime** — `window.__UPROOTED_SETTINGS__` is inlined at install time. Changes require the installer to write to disk and Root to restart.

## Import Conventions

```typescript
// Type-only imports (stripped at compile)
import type { UprootedPlugin, Patch } from "../../types/plugin.js";
import type { UprootedSettings } from "../../types/settings.js";

// Value imports
import { css, dom, native } from "../../api/index.js";

// All imports use .js extension (ES module resolution)
// Relative paths from plugin location: ../../api/, ../../types/
```

## Naming Conventions

- Plugin name: `kebab-case` (e.g., `"sentry-blocker"`)
- File: `src/plugins/<name>/index.ts`
- Log prefix: `[Uprooted:<name>]`
- CSS ID: `uprooted-css-plugin-<name>` (auto-managed)
- DOM attributes: `data-uprooted="<name>"`
- Settings key: `settings.plugins["<name>"]`
