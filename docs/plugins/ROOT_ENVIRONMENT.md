# Root Environment Reference

Runtime context for plugin developers. Understanding Root's Chromium environment is essential for writing plugins that work reliably.

> **Related Docs:**
> [Getting Started](GETTING_STARTED.md) |
> [Bridge Reference](BRIDGE_REFERENCE.md) |
> [Architecture](../ARCHITECTURE.md) |
> [Hook Reference](../HOOK_REFERENCE.md)

## Table of Contents

- [Chromium Environment](#chromium-environment)
- [Injection Targets](#injection-targets)
- [Sub-App Context Comparison](#sub-app-context-comparison)
- [CSS Variable System](#css-variable-system)
- [Theme CSS Variable Architecture](#theme-css-variable-architecture)
- [Window Globals](#window-globals)
- [Available Web APIs](#available-web-apis)
- [Debugging Strategies](#debugging-strategies)
- [Constraints & Gotchas](#constraints--gotchas)
- [Version Compatibility](#version-compatibility)

---

## Chromium Environment

Root.exe is a .NET 10 / Avalonia desktop app that embeds **DotNetBrowser** (a Chromium wrapper) for its web UI. Your plugin code runs inside this Chromium instance.

Key properties:

| Property | Value |
|----------|-------|
| Browser engine | DotNetBrowser (Chromium-based) |
| Incognito mode | `--incognito` flag is set |
| Web security | `--disable-web-security` flag is set |
| DevTools | Not available (no access) |
| CORS | Disabled (cross-origin requests work freely) |
| localStorage | Not available (incognito mode) |
| sessionStorage | Available (per-session only) |
| IndexedDB | Not available (incognito mode) |
| Cookies | Session-only (not persisted) |

The `--disable-web-security` flag means you can make fetch requests to any origin without CORS restrictions. The `--incognito` flag means no persistent client-side storage.

---

## Injection Targets

Root's Chromium hosts multiple web contexts. Uprooted's TypeScript layer runs in the **WebRTC context**.

### WebRTC Context (where your plugin runs)

The primary injection target. Contains:
- Voice/video call UI
- Bridge objects (`__nativeToWebRtc`, `__webRtcToNative`)
- Media management (WebRTC peer connections)
- Full DOM access to the call interface

This context is loaded from Root's bundled HTML files. Uprooted's C# hook patches these HTML files to inject the `<script>` tag that loads `uprooted-preload.js`. See [Hook Reference](../HOOK_REFERENCE.md) for details on how the HTML patching works.

### Sub-App Contexts (separate iframes)

Root also embeds 7 React/Vite sub-apps in iframes:
- Polls, Tasks, Raids, Suggestions, Stickers, Hexatris, Minecraft

These run in **separate iframe contexts**. Your plugin cannot directly access their DOM or JS globals. They communicate with the native host via a different bridge (`__rootSdkBridgeWebToNative`), which Uprooted does not proxy.

---

## Sub-App Context Comparison

The different web contexts in Root have different capabilities. This table summarizes what is available in each context.

| Feature | WebRTC Context (plugins) | Sub-App Iframes | Notes |
|---------|--------------------------|-----------------|-------|
| `__nativeToWebRtc` | Available (after join) | Not available | Only exists in WebRTC context |
| `__webRtcToNative` | Available (after join) | Not available | Only exists in WebRTC context |
| `__rootSdkBridgeWebToNative` | Not available | Available | Sub-app bridge, not proxied by Uprooted |
| DOM access | Full (own context) | Own iframe only | Cannot cross iframe boundaries |
| Uprooted globals | Available | Not available | `__UPROOTED_SETTINGS__`, `__UPROOTED_VERSION__`, etc. |
| CSS variables (`--color-*`) | Available | Available | Shared across all contexts via Root's theme system |
| `--rootsdk-*` overrides | Effective in own context | Not effective | Overrides only apply to the document where they are set |
| Fetch API | Available (no CORS) | Available (no CORS) | `--disable-web-security` applies globally |
| WebRTC APIs | Full access | Not available | Only the WebRTC context manages peer connections |
| React | Minimal (not a React app) | Full React apps | Sub-apps are React/Vite bundles |

### Implications for Plugin Developers

- **You cannot modify sub-app UI.** The Polls, Tasks, and other iframe-based features are isolated. Your CSS injections and DOM manipulations only affect the WebRTC context.
- **Theme variable overrides are context-scoped.** Setting `--rootsdk-brand-primary` on `:root` in your context does not propagate to iframes. However, Root's own theme system sets `--color-*` variables in all contexts via the native layer.
- **Bridge methods are WebRTC-only.** The 71 methods documented in [BRIDGE_REFERENCE.md](BRIDGE_REFERENCE.md) are only available in the WebRTC context. Sub-apps use a completely different bridge protocol.

---

## CSS Variable System

Root's entire color system is driven by CSS custom properties. There are two layers:

### Base Variables (`--color-*`)

Set by Root's theme system via `data-theme` attribute on `<html>`. These are the actual values used by CSS rules.

### Override Variables (`--rootsdk-*`)

Each `--color-*` variable is defined using `var(--rootsdk-*, fallback)`, allowing SDK/plugin overrides. Setting a `--rootsdk-*` variable overrides the corresponding `--color-*` value.

**Example:** Root's CSS might define:
```css
--color-brand-primary: var(--rootsdk-brand-primary, #3B6AF8);
```

To override, set `--rootsdk-brand-primary` -- no need to touch `--color-brand-primary` directly.

### Theme Variables (25 per theme)

#### Dark Theme (default)

| Variable | Override | Default | Description |
|----------|----------|---------|-------------|
| `--color-brand-primary` | `--rootsdk-brand-primary` | `#3B6AF8` | Primary brand blue |
| `--color-brand-secondary` | `--rootsdk-brand-secondary` | `#A8FF5D` | Secondary brand green |
| `--color-brand-tertiary` | `--rootsdk-brand-tertiary` | `#49D6AC` | Tertiary brand teal |
| `--color-text-primary` | `--rootsdk-text-primary` | `#F2F2F2` | Primary text (white) |
| `--color-text-secondary` | `--rootsdk-text-secondary` | `#A7A7A8` | Secondary text (gray) |
| `--color-text-tertiary` | `--rootsdk-text-tertiary` | `#7B7B89` | Tertiary text (dark gray) |
| `--color-text-white` | `--rootsdk-text-white` | `#F2F2F2` | Always-white text |
| `--color-background-primary` | `--rootsdk-background-primary` | `#0D1521` | Main background |
| `--color-background-secondary` | `--rootsdk-background-secondary` | `#121A26` | Secondary background |
| `--color-background-tertiary` | `--rootsdk-background-tertiary` | `#07101B` | Tertiary background |
| `--color-input` | `--rootsdk-input` | `#090E13` | Input field background |
| `--color-border` | `--rootsdk-border` | `#242C36` | Border color |
| `--color-highlight-light` | `--rootsdk-highlight-light` | `#FFFFFF0A` | Light highlight (4%) |
| `--color-highlight-normal` | `--rootsdk-highlight-normal` | `#FFFFFF19` | Normal highlight (10%) |
| `--color-highlight-strong` | `--rootsdk-highlight-strong` | `#FFFFFF30` | Strong highlight (19%) |
| `--color-info` | `--rootsdk-info` | `#F0F250` | Info yellow |
| `--color-warning` | `--rootsdk-warning` | `#E88F3D` | Warning orange |
| `--color-error` | `--rootsdk-error` | `#F03F36` | Error red |
| `--color-muted` | `--rootsdk-muted` | `#4F5C6F` | Muted/disabled color |
| `--color-link` | `--rootsdk-link` | `#88A5FF` | Link color |
| `--color-background-blur` | `--rootsdk-background-blur` | `#00000080` | Blur overlay |
| `--color-self-mention` | `--rootsdk-self-mention` | `#FF2D1F66` | Self-mention highlight |
| `--color-community-mention` | `--rootsdk-community-mention` | `#A8FF5D33` | Community mention |
| `--color-channel-mention` | `--rootsdk-channel-mention` | `#E88F3D4D` | Channel mention |
| `--color-transparent` | `--rootsdk-transparent` | `transparent` | Transparent |

#### Theme Differences

**Light theme** swaps backgrounds to whites/grays (`#FBFBFB`, `#FFFFFF`, `#F5F6F8`), darkens text (`#131313`), and adjusts accent colors to be more saturated for contrast.

**Pure Dark theme** replaces the navy-blue backgrounds with true neutral grays (`#161617`, `#1F1F22`, `#111113`). All other variables are identical to the dark theme.

See the [themes.json](../../src/plugins/themes/themes.json) file for the exact override values used by Uprooted's built-in themes.

---

## Theme CSS Variable Architecture

This section explains how Root's CSS variable system works end-to-end and how Uprooted's theme plugin interacts with it.

### How Root Sets Theme Variables

1. Root's C# host calls `nativeToWebRtc.setTheme(theme)` when the user changes the theme in settings. This is a bridge call that plugins can intercept (see [Bridge Reference -- setTheme](BRIDGE_REFERENCE.md#setthemetheme)).
2. Root's WebRTC JS layer sets the `data-theme` attribute on `<html>` (e.g. `data-theme="dark"`).
3. Root's CSS uses attribute selectors to apply theme-specific variable values:
   ```css
   [data-theme="dark"] {
     --color-brand-primary: var(--rootsdk-brand-primary, #3B6AF8);
     --color-background-primary: var(--rootsdk-background-primary, #0D1521);
     /* ... */
   }
   ```

### How Uprooted Overrides Variables

Uprooted's theme plugin (`src/plugins/themes/index.ts`) overrides the `--rootsdk-*` variables by setting inline styles on `document.documentElement`:

```typescript
// From src/api/native.ts:19
document.documentElement.style.setProperty("--rootsdk-brand-primary", "#e11d48");
```

This works because CSS specificity rules make inline styles on `:root` take precedence over the stylesheet defaults. Since `--color-brand-primary` is defined as `var(--rootsdk-brand-primary, #3B6AF8)`, setting the override variable changes the computed value of the base variable.

### Custom Theme Generation

The built-in theme plugin can generate a full set of theme variables from just two inputs: an accent color and a background color. The `generateCustomVariables()` function in `src/plugins/themes/index.ts:82` uses color math helpers (`darken`, `lighten`, `luminance`) to derive all 10 override variables:

- Brand primary = accent color
- Brand secondary = accent lightened 15%
- Brand tertiary = accent darkened 15%
- Background primary = background color
- Background secondary = background lightened 8%
- Background tertiary = background darkened 8%
- Input = background darkened 5%
- Border = background lightened 18%
- Link = accent lightened 30%
- Muted = background lightened 25% (dark themes) or darkened 25% (light themes)

### Best Practices for Theme Plugins

- **Always clean up variables in `stop()`.** Call `removeCssVariable()` for every variable you set. The built-in theme plugin tracks all variable names in a Set and iterates it on stop.
- **Flush before applying.** When switching between themes, remove all previous overrides first to avoid stale values. See `src/plugins/themes/index.ts:117`.
- **Use `--rootsdk-*` not `--color-*`.** Setting `--color-*` directly will work but will not be removed cleanly when themes change, because Root resets `--color-*` via its own stylesheet.
- **Consider the `setTheme` bridge call.** When Root changes theme, your overrides may conflict. Intercept `setTheme` to detect theme changes and re-apply or clear your overrides accordingly.

---

## Window Globals

Objects available on `window` in Root's Chromium context.

### Root Bridges

| Global | Always Present | Description |
|--------|----------------|-------------|
| `__nativeToWebRtc` | After join | INativeToWebRtc -- native host controls (proxied by Uprooted) |
| `__webRtcToNative` | After join | IWebRtcToNative -- WebRTC notifications (proxied by Uprooted) |

These are only populated when a voice session is active. They may be `undefined` before the user joins a call.

### Uprooted Globals

| Global | Always Present | Description |
|--------|----------------|-------------|
| `__UPROOTED_SETTINGS__` | Yes | Settings object with `enabled`, `plugins`, `customCss` |
| `__UPROOTED_VERSION__` | Yes | Version string (e.g. `"0.1.95"`) |
| `__UPROOTED_LOADER__` | Yes | PluginLoader instance |

### Other Root Globals

Root may also expose additional globals depending on context. These are not part of Uprooted's API and may change without notice:

| Global | Description |
|--------|-------------|
| `__rootSdkBridgeWebToNative` | Sub-app bridge (not proxied by Uprooted) |
| Various media/RTC objects | WebRTC peer connections and media managers |

---

## Available Web APIs

Since you're running in a Chromium context, standard web APIs are available:

### Fully Available
- **DOM API** -- `document.querySelector`, `createElement`, `MutationObserver`, etc.
- **Fetch API** -- `fetch()` with no CORS restrictions
- **WebRTC** -- `RTCPeerConnection`, `MediaStream`, `getUserMedia`
- **MediaDevices** -- `navigator.mediaDevices.enumerateDevices()`
- **Timers** -- `setTimeout`, `setInterval`, `requestAnimationFrame`
- **Web Audio** -- `AudioContext`, `GainNode`, analyzers
- **Canvas** -- `<canvas>` rendering, `OffscreenCanvas`
- **Clipboard** -- `navigator.clipboard` (read/write)
- **Notifications** -- `Notification` API (if permitted)
- **WebSocket** -- Full WebSocket support

### Not Available or Restricted
- **localStorage** -- Returns empty / throws (incognito mode)
- **IndexedDB** -- Not available (incognito mode)
- **Service Workers** -- Not registered
- **DevTools** -- No access to Chrome DevTools
- **Extensions** -- No Chrome extension APIs

---

## Debugging Strategies

Without DevTools, debugging requires alternative approaches.

### Error Banner

Uprooted catches fatal initialization errors and displays a red banner at the top of the page:

```
[Uprooted] Fatal: TypeError: Cannot read properties of undefined...
```

This banner appears automatically for uncaught errors during `main()`. For your own plugin code, you can create similar indicators:

```typescript
start() {
  try {
    // ... your code
  } catch (err) {
    const banner = document.createElement("div");
    banner.style.cssText = "position:fixed;top:0;left:0;right:0;z-index:999999;" +
      "padding:8px 16px;background:#dc2626;color:#fff;font:12px monospace;";
    banner.textContent = `[MyPlugin] Error: ${err}`;
    document.body.appendChild(banner);
  }
}
```

### nativeLog

Send messages to Root's .NET log output:

```typescript
import { nativeLog } from "../api/native.js";

nativeLog("Plugin started");
nativeLog(`Found ${count} elements`);
```

These appear in Root's internal logs with the `[Uprooted]` prefix. The function is defined in `src/api/native.ts:42`.

### DOM Indicators

Create visible DOM elements to show plugin state:

```typescript
const indicator = document.createElement("div");
indicator.id = "my-plugin-debug";
indicator.style.cssText = "position:fixed;bottom:8px;right:8px;z-index:999999;" +
  "padding:4px 8px;background:#333;color:#0f0;font:11px monospace;border-radius:4px;";
indicator.textContent = "MyPlugin: active";
document.body.appendChild(indicator);
```

### Console Logging

`console.log` works but is only visible if you can attach an external debugger. Useful mainly for structured logging that you can later review if a debugger becomes available.

---

## Constraints & Gotchas

### No Persistent Storage

Root runs Chromium with `--incognito`. This means:
- `localStorage.getItem()` returns `null`
- `localStorage.setItem()` may silently fail or throw
- IndexedDB is unavailable
- Cookies are session-only

**Workaround:** Use `window.__UPROOTED_SETTINGS__` for configuration. For runtime state, keep it in memory (it resets on restart anyway). For data that must persist across sessions, the only path is through the settings JSON file which Uprooted's C# hook reads at startup.

### Obfuscated CSS Classes

Root's React/Vite sub-apps use CSS modules or similar, producing class names like `_container_a1b2c` that change between versions. Don't rely on these.

**Workaround:** Use data attributes, ARIA attributes, tag structure, or other stable selectors when possible. The WebRTC context is more stable since it's not a Vite bundle.

### React Re-Renders

Root's UI (especially sub-apps) uses React, which can remove and re-create DOM nodes at any time. If you inject elements into a React-managed subtree:
- Your injected nodes may be removed without warning
- Event listeners on React-managed nodes may stop working

**Workaround:** Use `MutationObserver` (via `observe()` in `src/api/dom.ts:44`) to detect when your injected content is removed, and re-inject it. Always clean up observers in `stop()`. See [Getting Started -- Tutorial 5: DOM Injection](GETTING_STARTED.md#tutorial-5-dom-injection) for a full walkthrough.

### Bridge Timing

The bridge objects (`__nativeToWebRtc`, `__webRtcToNative`) are only set when a voice session is active. If your plugin patches bridge methods, the patches are installed at plugin start regardless -- they'll fire when the bridge becomes active.

However, if you need to call bridge methods directly in `start()`, check for their existence first:

```typescript
start() {
  if (window.__webRtcToNative) {
    window.__webRtcToNative.log("Plugin active during voice session");
  }
}
```

### No Module Imports at Runtime

Plugins are bundled into the Uprooted preload script at build time. You cannot use dynamic `import()` at runtime -- there's no module server or filesystem access from the Chromium context.

### Single Context

All plugins share a single JS execution context. This means:
- Global variable pollution is possible -- namespace your globals
- One plugin's uncaught error can affect others
- `window` is shared across all plugins

See [API Reference -- Error Handling](API_REFERENCE.md#error-handling) for how the loader isolates plugin errors, and [API Reference -- Plugin-to-Plugin Communication](API_REFERENCE.md#plugin-to-plugin-communication) for patterns to safely share state between plugins.

### CSS Specificity

Root's own styles may use `!important` or high-specificity selectors. Your injected CSS may need `!important` to override them:

```css
.some-root-element {
  background: #ff0000 !important;
}
```

For CSS variable overrides, use the `--rootsdk-*` pattern instead of trying to override `--color-*` directly -- this is the designed override mechanism.

---

## Version Compatibility

This table tracks which Uprooted versions have been tested against which Root Communications versions.

| Uprooted Version | Root Version(s) | Status | Notes |
|-------------------|-----------------|--------|-------|
| 0.1.x | 0.9.86+ | Supported | Current development line |
| 0.1.92+ | 0.9.86 - 0.9.90 | Tested | Verified working |
| 0.1.95 | 0.9.86 - latest | In development | Active development target |

### Compatibility Notes

- **Root updates may break HTML patches.** Root's auto-updater can overwrite the HTML files that Uprooted patches. The C# hook's `HtmlPatchVerifier` (see [Hook Reference](../HOOK_REFERENCE.md)) automatically detects and re-applies patches using a `FileSystemWatcher`.
- **Bridge method signatures are stable.** Root's bridge interfaces (`INativeToWebRtc`, `IWebRtcToNative`) have remained stable across all tested versions. New methods may be added, but existing method signatures have not changed.
- **CSS variable names are stable.** The 25 `--color-*` / `--rootsdk-*` variable pairs have been consistent across all tested Root versions. New variables may be added in future versions.
- **Sub-app iframe structure may change.** The number and type of embedded sub-apps can change between Root versions. Do not rely on specific iframes being present.

### If Something Breaks After a Root Update

1. Check that Uprooted's HTML patches are still applied (look for the `<script>` tag in Root's HTML files)
2. Verify the bridge objects are still being proxied (`window.__nativeToWebRtc` should be a Proxy)
3. Check `nativeLog` output for startup errors
4. If CSS looks wrong, Root may have changed variable names -- compare against the variable table above
