# Getting Started with Uprooted Plugins

A step-by-step guide to building your first plugin for Root Communications using Uprooted.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Project Setup](#project-setup)
- [Tutorial 1: Hello World](#tutorial-1-hello-world)
- [Tutorial 2: CSS Injection](#tutorial-2-css-injection)
- [Tutorial 3: Bridge Interception](#tutorial-3-bridge-interception)
- [Tutorial 4: Plugin Settings](#tutorial-4-plugin-settings)
- [Build & Test Workflow](#build--test-workflow)
- [Next Steps](#next-steps)

---

## Prerequisites

Before you start, you need:

- **Node.js** (v18+)
- **pnpm** (v8+) — `npm install -g pnpm`
- **Root Communications** v0.9.86 installed
- **Uprooted** installed — run `powershell -File Install-Uprooted.ps1`

Verify your Uprooted installation is working by launching Root and checking for the "UPROOTED" section in Root's settings sidebar.

---

## Project Setup

1. Clone the Uprooted repository:

```bash
git clone https://github.com/watchthelight/uprooted.git
cd uprooted
```

2. Install dependencies:

```bash
pnpm install
```

3. Verify the build works:

```bash
pnpm build
```

This should produce `dist/uprooted-preload.js` and `dist/uprooted.css`. If the build succeeds, you're ready to write plugins.

### Project Structure (what matters for plugins)

```
src/
├── types/
│   ├── plugin.ts      # UprootedPlugin, Patch, SettingsDefinition
│   ├── bridge.ts      # INativeToWebRtc, IWebRtcToNative
│   └── settings.ts    # UprootedSettings
├── api/
│   ├── css.ts         # injectCss, removeCss, removeAllCss
│   ├── dom.ts         # waitForElement, observe, nextFrame
│   ├── native.ts      # getCurrentTheme, setCssVariable(s), nativeLog
│   └── bridge.ts      # Bridge proxy (internal — you use patches, not this)
├── core/
│   ├── preload.ts     # Entry point
│   └── pluginLoader.ts # Plugin lifecycle manager
└── plugins/
    ├── themes/        # Built-in theme plugin (good reference)
    └── settings-panel/# Built-in settings panel
```

---

## Tutorial 1: Hello World

Create your first plugin that logs a message and shows a visual indicator.

### Step 1: Create the plugin file

Create `src/plugins/hello-world/index.ts`:

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";
import { nativeLog } from "../../api/native.js";

export default {
  name: "hello-world",
  description: "My first Uprooted plugin",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  start() {
    nativeLog("Hello World plugin started!");

    // Create a visible indicator (since we have no DevTools)
    const badge = document.createElement("div");
    badge.id = "hello-world-badge";
    badge.textContent = "Hello from Uprooted!";
    badge.style.cssText =
      "position: fixed; bottom: 12px; right: 12px; z-index: 999999; " +
      "padding: 8px 16px; background: #2D7D46; color: #fff; " +
      "font: 14px sans-serif; border-radius: 8px; pointer-events: none;";
    document.body.appendChild(badge);
  },

  stop() {
    document.getElementById("hello-world-badge")?.remove();
    nativeLog("Hello World plugin stopped!");
  },
} satisfies UprootedPlugin;
```

### Step 2: Register the plugin

Open `src/core/preload.ts` and add your plugin:

```typescript
import helloWorldPlugin from "../plugins/hello-world/index.js";

// ... inside main(), after the existing loader.register() calls:
loader.register(helloWorldPlugin);
```

### Step 3: Build and test

```bash
pnpm build
```

Then reinstall Uprooted and restart Root. You should see a green badge in the bottom-right corner saying "Hello from Uprooted!".

### What's Happening

1. The `preload.ts` entry point loads your plugin and calls `loader.register()`
2. The loader calls `startAll()`, which checks if your plugin is enabled in settings
3. Since there's no explicit setting, it defaults to enabled
4. Your `start()` function runs, creating the badge and logging via the native bridge

---

## Tutorial 2: CSS Injection

Add custom styles to Root's UI using the `css` field or the CSS API.

### Using the `css` Field (Static)

The simplest approach — declare CSS as a string on your plugin object:

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";

export default {
  name: "round-avatars",
  description: "Makes all avatars perfectly round",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  css: `
    img[class*="avatar"] {
      border-radius: 50% !important;
    }
  `,
} satisfies UprootedPlugin;
```

The loader automatically injects this CSS when the plugin starts and removes it when the plugin stops. The `<style>` element gets the ID `uprooted-css-plugin-round-avatars`.

### Using the CSS API (Dynamic)

For CSS that changes at runtime, use `injectCss` directly:

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";
import { injectCss, removeCss } from "../../api/css.js";

export default {
  name: "dynamic-styles",
  description: "Changes styles based on time of day",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  start() {
    const hour = new Date().getHours();
    const isNight = hour < 6 || hour > 20;

    injectCss("dynamic-styles-time", `
      :root {
        --rootsdk-brand-primary: ${isNight ? "#6366f1" : "#f59e0b"} !important;
      }
    `);
  },

  stop() {
    removeCss("dynamic-styles-time");
  },
} satisfies UprootedPlugin;
```

### CSS Variable Overrides

The most powerful theming approach uses Root's CSS variable system:

```typescript
import { setCssVariables, removeCssVariable } from "../../api/native.js";

// In start():
setCssVariables({
  "--rootsdk-brand-primary": "#e11d48",
  "--rootsdk-background-primary": "#1a1a2e",
});

// In stop():
removeCssVariable("--rootsdk-brand-primary");
removeCssVariable("--rootsdk-background-primary");
```

See [ROOT_ENVIRONMENT.md](ROOT_ENVIRONMENT.md#css-variable-system) for the full list of 25 CSS variables.

---

## Tutorial 3: Bridge Interception

Intercept communication between Root's C# host and WebRTC layer.

### Before Handler (observe and optionally cancel)

Log every time the theme changes:

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";

export default {
  name: "theme-logger",
  description: "Logs theme changes",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  patches: [
    {
      bridge: "nativeToWebRtc",
      method: "setTheme",
      before(args) {
        console.log("Theme changing to:", args[0]);
        // Returning undefined (or nothing) allows the call to proceed
      },
    },
  ],
} satisfies UprootedPlugin;
```

### Before Handler with Cancellation

Block kick commands:

```typescript
patches: [
  {
    bridge: "nativeToWebRtc",
    method: "kick",
    before(args) {
      console.log("Blocked kick for:", args[0]);
      return false; // Cancel — the original kick() is never called
    },
  },
],
```

### Replace Handler (full override)

Replace the disconnect handler with a custom one:

```typescript
patches: [
  {
    bridge: "nativeToWebRtc",
    method: "disconnect",
    replace() {
      console.log("Custom disconnect — adding cleanup");
      // Do your cleanup here
      // Note: the original disconnect() is NOT called
    },
  },
],
```

See [BRIDGE_REFERENCE.md](BRIDGE_REFERENCE.md) for all 71 bridge methods you can intercept.

---

## Tutorial 4: Plugin Settings

Define user-configurable settings that appear in the Uprooted settings panel.

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";
import { nativeLog } from "../../api/native.js";

export default {
  name: "my-configurable-plugin",
  description: "A plugin with settings",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  settings: {
    greeting: {
      type: "string",
      default: "Hello!",
      description: "Message shown on startup",
    },
    showBadge: {
      type: "boolean",
      default: true,
      description: "Show a visible badge in the UI",
    },
    badgeSize: {
      type: "number",
      default: 14,
      description: "Badge font size in pixels",
      min: 8,
      max: 32,
    },
    position: {
      type: "select",
      default: "bottom-right",
      description: "Badge position on screen",
      options: ["top-left", "top-right", "bottom-left", "bottom-right"],
    },
  },

  start() {
    // Read settings
    const config = window.__UPROOTED_SETTINGS__?.plugins?.["my-configurable-plugin"]?.config;
    const greeting = (config?.greeting as string) ?? "Hello!";
    const showBadge = (config?.showBadge as boolean) ?? true;
    const badgeSize = (config?.badgeSize as number) ?? 14;
    const position = (config?.position as string) ?? "bottom-right";

    nativeLog(greeting);

    if (showBadge) {
      const badge = document.createElement("div");
      badge.id = "my-plugin-badge";
      badge.textContent = greeting;

      const [vertical, horizontal] = position.split("-");
      badge.style.cssText =
        `position: fixed; ${vertical}: 12px; ${horizontal}: 12px; z-index: 999999; ` +
        `padding: 8px 16px; background: #2D7D46; color: #fff; ` +
        `font: ${badgeSize}px sans-serif; border-radius: 8px;`;
      document.body.appendChild(badge);
    }
  },

  stop() {
    document.getElementById("my-plugin-badge")?.remove();
  },
} satisfies UprootedPlugin;
```

### How Settings Work

1. You define a `settings` object with field schemas (type, default, description)
2. The Uprooted settings panel renders appropriate UI controls
3. Values are stored in the settings JSON under `plugins.{name}.config`
4. In `start()`, read from `window.__UPROOTED_SETTINGS__?.plugins?.[name]?.config`
5. Always provide fallback defaults in case the setting isn't configured yet

---

## Build & Test Workflow

### Build

```bash
pnpm build
```

This runs esbuild to bundle everything into `dist/uprooted-preload.js`.

### Install

```bash
powershell -File Install-Uprooted.ps1
```

This copies the built files to Root's installation directory.

### Test

1. Launch Root Communications
2. Join a voice channel (required for bridge objects to be available)
3. Open Root's settings — look for the "UPROOTED" section in the sidebar

### Debugging Without DevTools

Root does **not** expose Chrome DevTools, so you can't use the console or inspector. Use these strategies instead:

**Error Banner:** Fatal errors during Uprooted initialization display a red banner. For your own code, wrap `start()` in try/catch and create a similar banner.

**nativeLog:** Send messages to Root's .NET logs:
```typescript
import { nativeLog } from "../../api/native.js";
nativeLog("Debug: reached checkpoint A");
```

**DOM Indicators:** Create visible elements to show state:
```typescript
const debug = document.createElement("pre");
debug.id = "my-plugin-debug";
debug.style.cssText = "position:fixed;top:0;left:0;z-index:999999;" +
  "padding:8px;background:#000c;color:#0f0;font:11px monospace;max-height:200px;overflow:auto;";
document.body.appendChild(debug);

// Later, append log lines:
debug.textContent += "Event received\n";
```

**console.log:** Still works internally. If you ever get debugger access (external Chromium debugger), these logs will be there.

See [ROOT_ENVIRONMENT.md](ROOT_ENVIRONMENT.md#debugging-strategies) for more detail.

---

## Next Steps

- [API_REFERENCE.md](API_REFERENCE.md) — Complete API documentation for all interfaces and functions
- [BRIDGE_REFERENCE.md](BRIDGE_REFERENCE.md) — All 71 bridge methods with type signatures
- [ROOT_ENVIRONMENT.md](ROOT_ENVIRONMENT.md) — Runtime constraints, CSS variables, and available APIs
- [EXAMPLES.md](EXAMPLES.md) — Copy-paste example plugins covering common patterns
- Study the built-in plugins at `src/plugins/themes/` and `src/plugins/settings-panel/` for real-world patterns
