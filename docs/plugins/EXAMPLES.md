# Example Plugins

Copy-paste example plugins demonstrating common patterns. Each example is a complete, self-contained plugin.

## Table of Contents

- [Minimal Template](#minimal-template)
- [Theme Logger](#theme-logger)
- [Anti-Kick](#anti-kick)
- [Voice Activity Monitor](#voice-activity-monitor)
- [Custom Theme](#custom-theme)
- [Settings Example](#settings-example)
- [DOM Injector](#dom-injector)
- [Call Logger](#call-logger)

---

## Minimal Template

The absolute bare minimum plugin. Use this as your starting point.

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";

export default {
  name: "my-plugin",
  description: "Does something cool",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  start() {
    console.log("[my-plugin] Started");
  },

  stop() {
    console.log("[my-plugin] Stopped");
  },
} satisfies UprootedPlugin;
```

To register: add `import myPlugin from "../plugins/my-plugin/index.js";` and `loader.register(myPlugin);` in `src/core/preload.ts`.

---

## Theme Logger

Uses a `before` patch handler to log every theme change. Demonstrates basic bridge interception.

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";
import type { Theme } from "../../types/bridge.js";
import { nativeLog } from "../../api/native.js";

export default {
  name: "theme-logger",
  description: "Logs theme changes to native log",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  patches: [
    {
      bridge: "nativeToWebRtc",
      method: "setTheme",
      before(args) {
        const theme = args[0] as Theme;
        nativeLog(`Theme changed to: ${theme}`);
      },
    },
  ],

  start() {
    const current = document.documentElement.getAttribute("data-theme");
    nativeLog(`Theme Logger active. Current theme: ${current ?? "unknown"}`);
  },
} satisfies UprootedPlugin;
```

**Key concepts:** `before` handler, accessing `args`, using `nativeLog`.

---

## Anti-Kick

Uses a `before` handler that returns `false` to cancel bridge calls. Blocks both directions of kick commands.

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";
import { nativeLog } from "../../api/native.js";

export default {
  name: "anti-kick",
  description: "Blocks kick commands (both directions)",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  patches: [
    {
      bridge: "nativeToWebRtc",
      method: "kick",
      before(args) {
        nativeLog(`Blocked outgoing kick for user: ${args[0]}`);
        return false; // Cancel the kick
      },
    },
    {
      bridge: "webRtcToNative",
      method: "kickPeer",
      before(args) {
        nativeLog(`Blocked incoming kick request for user: ${args[0]}`);
        return false;
      },
    },
  ],
} satisfies UprootedPlugin;
```

**Key concepts:** Returning `false` from `before` to cancel, patching both bridge directions, different method names for the same action (`kick` vs `kickPeer`).

---

## Voice Activity Monitor

Monitors speaking events and injects a CSS indicator showing who's talking. Demonstrates `before` handlers, CSS injection, and DOM manipulation.

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";
import type { UserGuid, DeviceGuid } from "../../types/bridge.js";
import { injectCss, removeCss } from "../../api/css.js";

let speakingUsers = new Map<string, string>(); // userId -> deviceId
let indicator: HTMLDivElement | null = null;

function updateIndicator(): void {
  if (!indicator) return;
  if (speakingUsers.size === 0) {
    indicator.style.display = "none";
    return;
  }
  indicator.style.display = "block";
  const users = Array.from(speakingUsers.keys());
  indicator.textContent = `Speaking: ${users.join(", ")}`;
}

export default {
  name: "voice-monitor",
  description: "Shows a live indicator of who is speaking",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  css: `
    #voice-monitor-indicator {
      position: fixed;
      top: 8px;
      left: 50%;
      transform: translateX(-50%);
      z-index: 999999;
      padding: 6px 16px;
      background: rgba(45, 125, 70, 0.9);
      color: #fff;
      font: 12px monospace;
      border-radius: 20px;
      pointer-events: none;
      transition: opacity 0.2s;
    }
  `,

  patches: [
    {
      bridge: "webRtcToNative",
      method: "setSpeaking",
      before(args) {
        const [isSpeaking, deviceId, userId] = args as [boolean, DeviceGuid, UserGuid];
        if (isSpeaking) {
          speakingUsers.set(userId, deviceId);
        } else {
          speakingUsers.delete(userId);
        }
        updateIndicator();
      },
    },
  ],

  start() {
    indicator = document.createElement("div");
    indicator.id = "voice-monitor-indicator";
    indicator.style.display = "none";
    document.body.appendChild(indicator);
  },

  stop() {
    indicator?.remove();
    indicator = null;
    speakingUsers.clear();
  },
} satisfies UprootedPlugin;
```

**Key concepts:** Using `css` field + DOM elements together, tracking state across events, cleaning up in `stop()`.

---

## Custom Theme

Applies a custom color scheme by overriding Root's CSS variables. Demonstrates the native API for theming.

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";
import { setCssVariables, removeCssVariable } from "../../api/native.js";

// All the variables we override (for cleanup)
const THEME_VARS: Record<string, string> = {
  "--rootsdk-brand-primary": "#e11d48",
  "--rootsdk-brand-secondary": "#fb7185",
  "--rootsdk-brand-tertiary": "#be123c",
  "--rootsdk-background-primary": "#1a1a2e",
  "--rootsdk-background-secondary": "#22223b",
  "--rootsdk-background-tertiary": "#16161a",
  "--rootsdk-input": "#16161a",
  "--rootsdk-border": "#3a3a5c",
  "--rootsdk-link": "#fb923c",
  "--rootsdk-muted": "#4a4a6a",
};

export default {
  name: "rose-theme",
  description: "A rose/purple custom theme",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  start() {
    setCssVariables(THEME_VARS);
  },

  stop() {
    for (const name of Object.keys(THEME_VARS)) {
      removeCssVariable(name);
    }
  },
} satisfies UprootedPlugin;
```

**Key concepts:** `setCssVariables` for batch overrides, `removeCssVariable` for cleanup, using `--rootsdk-*` override pattern.

See [ROOT_ENVIRONMENT.md](ROOT_ENVIRONMENT.md#css-variable-system) for the full list of overridable variables.

---

## Settings Example

Demonstrates all four setting types (boolean, string, number, select) and reading them at runtime.

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";
import { nativeLog } from "../../api/native.js";
import { injectCss, removeCss } from "../../api/css.js";

export default {
  name: "settings-demo",
  description: "Demonstrates all setting types",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  settings: {
    enabled: {
      type: "boolean",
      default: true,
      description: "Enable the visual overlay",
    },
    label: {
      type: "string",
      default: "Uprooted",
      description: "Text shown in the overlay",
    },
    opacity: {
      type: "number",
      default: 80,
      description: "Overlay opacity (0-100)",
      min: 0,
      max: 100,
    },
    position: {
      type: "select",
      default: "bottom-right",
      description: "Overlay position",
      options: ["top-left", "top-right", "bottom-left", "bottom-right"],
    },
  },

  start() {
    // Read settings with fallbacks
    const config = window.__UPROOTED_SETTINGS__?.plugins?.["settings-demo"]?.config;
    const enabled = (config?.enabled as boolean) ?? true;
    const label = (config?.label as string) ?? "Uprooted";
    const opacity = (config?.opacity as number) ?? 80;
    const position = (config?.position as string) ?? "bottom-right";

    nativeLog(`Settings Demo: enabled=${enabled}, label="${label}", opacity=${opacity}, pos=${position}`);

    if (!enabled) return;

    const [v, h] = position.split("-");

    injectCss("settings-demo-overlay", `
      #settings-demo-overlay {
        position: fixed;
        ${v}: 12px;
        ${h}: 12px;
        z-index: 999999;
        padding: 6px 12px;
        background: rgba(45, 125, 70, ${opacity / 100});
        color: #fff;
        font: 12px sans-serif;
        border-radius: 6px;
        pointer-events: none;
      }
    `);

    const overlay = document.createElement("div");
    overlay.id = "settings-demo-overlay";
    overlay.textContent = label;
    document.body.appendChild(overlay);
  },

  stop() {
    document.getElementById("settings-demo-overlay")?.remove();
    removeCss("settings-demo-overlay");
  },
} satisfies UprootedPlugin;
```

**Key concepts:** All four `SettingField` types, reading config from `__UPROOTED_SETTINGS__`, fallback defaults, dynamic CSS based on settings.

---

## DOM Injector

Waits for a specific DOM element, injects custom content, and uses MutationObserver to re-inject if Root removes it. Demonstrates the DOM API.

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";
import { waitForElement, observe } from "../../api/dom.js";
import { nativeLog } from "../../api/native.js";

let disconnect: (() => void) | null = null;

function createBadge(): HTMLDivElement {
  const badge = document.createElement("div");
  badge.id = "dom-injector-badge";
  badge.textContent = "Modded";
  badge.style.cssText =
    "display: inline-flex; align-items: center; padding: 2px 8px; " +
    "background: #2D7D46; color: #fff; font: 10px sans-serif; " +
    "border-radius: 4px; margin-left: 8px;";
  return badge;
}

export default {
  name: "dom-injector",
  description: "Injects a 'Modded' badge next to the app title",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  async start() {
    try {
      // Wait for the title element to appear (up to 15 seconds)
      const title = await waitForElement<HTMLElement>("h1, [class*='title']", 15000);

      // Inject badge
      const injectBadge = () => {
        if (document.getElementById("dom-injector-badge")) return; // Already there
        title.parentElement?.appendChild(createBadge());
      };

      injectBadge();

      // Watch for React re-renders that might remove our badge
      if (title.parentElement) {
        disconnect = observe(title.parentElement, () => {
          if (!document.getElementById("dom-injector-badge")) {
            nativeLog("Badge was removed by React, re-injecting");
            injectBadge();
          }
        });
      }
    } catch (err) {
      nativeLog(`DOM Injector failed: ${err}`);
    }
  },

  stop() {
    disconnect?.();
    disconnect = null;
    document.getElementById("dom-injector-badge")?.remove();
  },
} satisfies UprootedPlugin;
```

**Key concepts:** `waitForElement` with timeout, `observe` for re-injection on React re-renders, cleanup in `stop()`, error handling with `nativeLog`.

---

## Call Logger

Monitors multiple bridge methods to log the full lifecycle of a voice call. Demonstrates multi-method patching with dynamic patch generation.

```typescript
import type { UprootedPlugin } from "../../types/plugin.js";
import type { Patch } from "../../types/plugin.js";
import type { InitializeDesktopWebRtcPayload, Theme } from "../../types/bridge.js";
import { nativeLog } from "../../api/native.js";

// Format a timestamp for log lines
function ts(): string {
  return new Date().toLocaleTimeString("en-US", { hour12: false });
}

// Build patches dynamically for cleaner code
function logPatch(
  bridge: "nativeToWebRtc" | "webRtcToNative",
  method: string,
  format?: (args: unknown[]) => string,
): Patch {
  return {
    bridge,
    method,
    before(args) {
      const detail = format ? format(args) : args.map(String).join(", ");
      nativeLog(`[${ts()}] ${bridge}.${method}(${detail})`);
    },
  };
}

export default {
  name: "call-logger",
  description: "Logs the full voice call lifecycle",
  version: "0.1.0",
  authors: [{ name: "YourName" }],

  patches: [
    // Session lifecycle
    logPatch("nativeToWebRtc", "initialize", (args) => {
      const s = args[0] as InitializeDesktopWebRtcPayload;
      return `channel=${s.channelId}, user=${s.userId}`;
    }),
    logPatch("nativeToWebRtc", "disconnect"),
    logPatch("webRtcToNative", "initialized"),
    logPatch("webRtcToNative", "disconnected"),
    logPatch("webRtcToNative", "failed", (args) => JSON.stringify(args[0])),

    // Media toggles
    logPatch("nativeToWebRtc", "setIsAudioOn"),
    logPatch("nativeToWebRtc", "setIsVideoOn"),
    logPatch("nativeToWebRtc", "setIsScreenShareOn"),

    // User state
    logPatch("nativeToWebRtc", "setMute"),
    logPatch("nativeToWebRtc", "setDeafen"),
    logPatch("nativeToWebRtc", "setHandRaised"),
    logPatch("nativeToWebRtc", "setTheme", (args) => args[0] as string),

    // Remote events
    logPatch("webRtcToNative", "setSpeaking", (args) => {
      const [speaking, , userId] = args;
      return `${userId} ${speaking ? "started" : "stopped"} speaking`;
    }),

    // Moderation
    logPatch("nativeToWebRtc", "kick"),
    logPatch("nativeToWebRtc", "setAdminMute"),
    logPatch("nativeToWebRtc", "setAdminDeafen"),
  ],

  start() {
    nativeLog(`[${ts()}] Call Logger active — monitoring ${this.patches!.length} methods`);
  },

  stop() {
    nativeLog(`[${ts()}] Call Logger stopped`);
  },
} satisfies UprootedPlugin;
```

**Key concepts:** Dynamic patch generation with a helper function, formatting args per-method, monitoring both bridge directions, timestamp logging, using `this.patches` to reference own config.
