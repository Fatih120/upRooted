# Root Internals Reference

Root Communications desktop application architecture from a reverse engineering perspective. This document describes Root's internal structure, process model, embedded browser integration, sub-application lifecycle, authentication flow, theme infrastructure, update mechanism, file layout, backend protocol, and known behavioral quirks -- everything a framework contributor needs to understand about the host application that Uprooted modifies at runtime.

For the browser-side perspective relevant to plugin development, see [Root Environment](../plugins/ROOT_ENVIRONMENT.md).

> **Related docs:** [Architecture](../framework/ARCHITECTURE.md) | [Root Environment](../plugins/ROOT_ENVIRONMENT.md) | [gRPC Protocol](GRPC_PROTOCOL.md) | [Reverse Engineering](REVERSE_ENGINEERING.md) | [Security Research](SECURITY_RESEARCH.md)

---

## Table of Contents

1. [Overview](#1-overview)
2. [Process Architecture](#2-process-architecture)
3. [DotNetBrowser Integration](#3-dotnetbrowser-integration)
4. [Sub-Application Lifecycle](#4-sub-application-lifecycle)
5. [Authentication and Tokens](#5-authentication-and-tokens)
6. [Theme System](#6-theme-system)
7. [Update Mechanism (Velopack)](#7-update-mechanism-velopack)
8. [File Layout](#8-file-layout)
9. [gRPC Backend](#9-grpc-backend)
10. [Effects SDK](#10-effects-sdk)
11. [Known Behaviors](#11-known-behaviors)

---

## 1. Overview

### What Root Communications Is

Root Communications is a desktop community platform -- comparable to Discord, but smaller and more focused on action-oriented features. Its tagline is "Not Just Chat, Built for Action!" The application combines real-time messaging, voice/video calls (via WebRTC), and seven embedded mini-applications: polls, task tracker, raid planner, stickerwall, suggestions board, hexatris game, and a Minecraft server setup tool.

Root is available on Windows, macOS, Linux, iOS, and Android. The desktop client is a single self-contained executable. The company (Root Communications, Inc.) is based in West Hollywood, California.

### Why Understanding Root's Internals Matters

Uprooted is a client modification framework that injects custom UI, plugin infrastructure, and theme overrides into Root at runtime. Unlike a typical browser extension or overlay, Uprooted operates at two layers simultaneously: inside the .NET managed process (via CLR profiler injection) and inside the embedded Chromium browser (via HTML script injection). Understanding how Root's layers interact, how data flows between them, and where the boundaries lie is essential for:

- Knowing which layer to target for a given modification
- Avoiding injection patterns that cause freezes, crashes, or detection
- Surviving Root's auto-update cycle without losing patches
- Understanding the security implications of bridge interception
- Debugging issues that span the .NET/Chromium boundary

### Scope of This Document

This document covers Root's architecture as observed through reverse engineering of version 0.9.86 and surrounding versions. All findings come from static analysis of the binary, source map extraction, runtime observation, and network traffic analysis. Nothing here is based on Root's internal documentation or source code access.

This is the **native/.NET/system-level view** aimed at Uprooted framework contributors who need to understand the host application deeply. Plugin authors who only need to work within the Chromium context should start with [Root Environment](../plugins/ROOT_ENVIRONMENT.md) instead.

---

## 2. Process Architecture

### The .NET 10 Host

Root.exe is a **self-contained .NET 10 application**. "Self-contained" means the entire .NET runtime, all managed assemblies, and all native dependencies are bundled into a single 617 MB executable. At launch, the runtime extracts assemblies to memory rather than to disk. There is no separate `dotnet` runtime installation required.

We confirmed the .NET 10 target via PE headers, assembly manifest strings (`Avalonia.Controls`, `Avalonia.Base`), and the embedded `.runtimeconfig.json` specifying `Microsoft.NETCore.App` version `10.0.0`.

All 11 EXE/DLL files carry valid Authenticode signatures (see Appendix). However, .NET does not verify Authenticode at assembly load time, and no runtime integrity check is performed on loaded code. This is what makes profiler injection and startup hook injection viable.

### Avalonia UI Framework

Root uses **Avalonia 11.3.10** as its UI framework. Avalonia is a cross-platform .NET UI framework comparable to WPF but not dependent on Windows. Key Avalonia concepts relevant to Uprooted:

| Concept | Relevance to Uprooted |
|---------|----------------------|
| `Application.Current` | Global singleton; Uprooted waits for this in Phase 2 of startup |
| `MainWindow` | The top-level window; Uprooted waits for this in Phase 3 |
| `Dispatcher` | UI thread marshaling; all Avalonia mutations must go through this |
| `ResourceDictionary` | Theme values stored here; Uprooted's ThemeEngine injects overrides |
| Visual tree | Hierarchical control structure; Uprooted walks this to find injection points |
| `ContentControl.Content` | Direct assignment causes UI freeze -- Uprooted uses Grid overlay instead |
| `DispatcherPriority` | A struct (not enum) in Avalonia 11+ -- incorrect usage causes build failures |

Root's native Avalonia UI handles the application shell, window chrome, sidebar navigation, settings pages, **and the entire chat/community UI**. Chat messages, channels, and community views are rendered as native Avalonia controls (1647+ visual tree nodes confirmed, 0 browser-like controls). DotNetBrowser handles auxiliary functions: WebRTC voice/video, OAuth flows, and sub-app iframes (polls, task tracker, etc.).

### Embedded DotNetBrowser (Chromium 144)

Root embeds **DotNetBrowser**, a commercial Chromium-based browser control for .NET, for auxiliary web functions (WebRTC, OAuth, sub-apps). This is not WebView2 (Microsoft's Chromium wrapper) or CEF (Chromium Embedded Framework) -- it is a distinct commercial product with its own binary IPC protocol.

> **Critical:** Despite DotNetBrowser being present, Root does NOT use it to render chat or community views. Chat is rendered entirely in native Avalonia controls. See [Critical Finding: Chat is Avalonia-Native](#critical-finding-chat-is-avalonia-native) below.

The Chromium instance runs with notable flags:

| Flag | Effect |
|------|--------|
| `--incognito` | No persistent localStorage, IndexedDB, or cookies |
| `--disable-web-security` | CORS disabled; cross-origin fetch requests succeed freely |

DevTools access is disabled. No Chrome DevTools Protocol (CDP) endpoints respond -- probing `/json/version` and `/json/list` returns nothing.

### Layer Interaction Model

The three layers interact through well-defined boundaries:

```
+----------------------------------------------------------+
|                Root.exe (.NET 10 Process)                 |
|                                                          |
|  +------------------+     +---------------------------+  |
|  | Avalonia UI       |     | DotNetBrowser (Chromium)  |  |
|  | - Window chrome   |     | - WebRTC voice/video      |  |
|  | - Settings pages  |     | - OAuth flows             |  |
|  | - Sidebar nav     |     | - Sub-app iframes         |  |
|  | - Chat UI (native)|     |   (polls, tasks, etc.)    |  |
|  | - Community views |     |                           |  |
|  +--------+---------+     +-------------+-------------+  |
|           |                             |                |
|           |    Binary IPC (localhost)    |                |
|           |    Named pipes              |                |
|           +-------- JS Bridges ---------+                |
|                                                          |
|  +---------------------------------------------------+  |
|  | Uprooted C# Hook          | Uprooted TS Layer     |  |
|  | (reflection, visual tree)  | (DOM, bridge proxy)   |  |
|  +---------------------------------------------------+  |
+----------------------------------------------------------+
```

The .NET host creates JavaScript bridge objects that are injected into the Chromium window scope. These bridges are the primary communication channel between the two layers. The binary IPC on a dynamic localhost port handles lower-level Chromium lifecycle management.

---

## 3. DotNetBrowser Integration

### How Root Embeds Chromium

Root accesses DotNetBrowser programmatically through its ViewModel chain, NOT through a `BrowserView` Avalonia control. Root does not ship `DotNetBrowser.AvaloniaUi` (the package that provides `BrowserView`). No browser-like controls exist in the Avalonia visual tree (confirmed: 1647+ nodes scanned, 0 browser controls found). The browser engine is accessed via: `MainWindow.DataContext` (MainViewModel) → `BrowserService` → `BrowserEngineManager` → `IEngine` → `Profiles[0].Browsers._values` (ConcurrentDictionary).

The embedded Chromium engine runs as a set of child processes managed by the DotNetBrowser runtime:

- A **browser process** handling navigation, storage, and coordination
- One or more **renderer processes** for web content
- A **GPU process** for hardware-accelerated rendering

These child processes communicate with Root.exe via a proprietary binary protocol on a dynamically assigned localhost port (observed as port 49212 during testing, but it changes each launch). Four established loopback TCP connections are maintained between the .NET host and Chromium processes.

### Binary IPC Protocol

The DotNetBrowser IPC is **not** the Chrome DevTools Protocol. We probed the dynamic port with raw TCP, HTTP, WebSocket upgrades, and CDP endpoint requests (`/json/version`, `/json/list`) -- none returned meaningful responses. The protocol is proprietary to DotNetBrowser. Uprooted does not interact with it; all Chromium-side modifications go through the JavaScript bridges or direct HTML/DOM manipulation.

### Named Pipe Infrastructure

Binary analysis of Root.exe reveals named pipe endpoints used for internal IPC:

| Named Pipe | Direction | Purpose |
|------------|-----------|---------|
| `AppToNativeBridge` | App to .NET | Primary app-to-native communication |
| `AppToNativePrivateBridge` | App to .NET | Private/privileged channel |
| `NativeToAppPrivateBridge` | .NET to App | Native-to-app private channel |
| `NativeToWebRtcBridge` | .NET to WebRTC | Media bridge for voice/video |

The `NativeToWebRtcBridge` pipe is particularly relevant -- it carries the bearer token during the WebRTC initialization sequence. Token interception via this pipe is one of the extraction methods documented in our research.

### Browser Engine Access and Shell Page

Root accesses the DotNetBrowser engine programmatically (no `BrowserView` in the visual tree). The browser loads a shell page at `rootapp://app/__index.html`:

```html
<html lang="en">
<head>
  <title>Root App Container</title>
  <style>html, body { margin:0; padding:0; width:100%; height:100%; overflow:hidden; }
  iframe { display:block; width:100%; height:100%; border:none; }</style>
</head>
<body>
  <iframe id="app-iframe"></iframe>  <!-- permanently about:blank, no src -->
</body>
</html>
```

The shell page contains a single `<iframe id="app-iframe">` that **permanently stays at `about:blank`** with no `src` attribute and no content. This iframe is not used for navigation or rendering.

Additional HTML files in the profile directory serve specific functions:

- `WebRtcBundle/index.html` -- The WebRTC voice/video context
- `DotNetBrowser/RootApps/Bundle/Host/index.html` -- The sub-app host container

The host `index.html` is a bare iframe container with no Content Security Policy (CSP) and no sandbox attributes, which makes script injection straightforward.

### JavaScript Bridge Objects

Root creates four bridge objects on the `window` scope inside Chromium:

| Bridge Object | Context | Direction | Methods |
|---------------|---------|-----------|---------|
| `__rootSdkBridgeWebToNative` | Sub-apps | Web to .NET | 8 methods (send, search, upload, restart, etc.) |
| `__rootSdkBridgeNativeToWeb` | Sub-apps | .NET to Web | 2 methods (receive, setTheme) |
| `__nativeToWebRtc` | WebRTC | .NET to Web | 42 methods (initialize, kick, permissions, packets, etc.) |
| `__webRtcToNative` | WebRTC | Web to .NET | 28 methods (kickPeer, setAdminMute, speaking, etc.) |

Additionally, `__rootSdkBridgeDev` exists in the codebase for development/debugging but ports are closed in production builds.

These bridges are the primary mechanism through which the .NET host controls web content and web content notifies the .NET host. Uprooted proxies the WebRTC bridges (`__nativeToWebRtc` and `__webRtcToNative`) using ES6 Proxy objects, allowing plugins to intercept, modify, or cancel any bridge call. The sub-app bridge (`__rootSdkBridgeWebToNative`) is not currently proxied.

### Critical Finding: Chat is Avalonia-Native

Extensive investigation on 2026-02-17 (Root v0.9.92) revealed that **Root's chat UI is rendered entirely in native Avalonia controls, NOT in DotNetBrowser**. This finding has significant implications for any Uprooted feature that targets chat messages.

**Evidence:**

1. **Visual tree scan:** 1647+ Avalonia nodes in the main window, 0 browser-like controls (no `BrowserView`, no `WebView`, no `ChromiumWebBrowser`)
2. **DotNetBrowser shell page:** Loads `rootapp://app/__index.html`, title "Root App Container", size 1280x800. Contains a single `<iframe id="app-iframe">` that permanently stays at `about:blank`
3. **Assembly analysis:** `DotNetBrowser.AvaloniaUi` is NOT loaded — Root doesn't ship the package that provides `BrowserView`
4. **IBrowser access:** Found via ViewModel chain walking (MainWindow.DataContext → BrowserService → BrowserEngineManager → IEngine → Profiles[0].Browsers), confirming programmatic-only access

**Implications for Uprooted:**

- JavaScript injection into DotNetBrowser **cannot** modify chat messages, channel lists, or community views
- Features like link embeds and NSFW filtering that target chat content must use **Avalonia-native approaches**: watching the visual tree for URL-containing TextBlocks, creating native embed controls, etc.
- TypeScript plugins in the WebRTC context can still intercept bridge calls and modify sub-app behavior
- The DotNetBrowser shell page's about:blank iframe is a dead end — injecting JS there has no visible effect on the chat UI

---

## 4. Sub-Application Lifecycle

### The Sub-App Model

Root's web UI is not a single monolithic application. Instead, it uses a **host iframe container** that loads individual sub-applications as isolated iframes. Each sub-app is a self-contained React/Vite bundle (except Stickerwall, which uses Alpine.js and PixiJS).

### Registry and Discovery

Sub-apps are registered in `RootApps/registry.json`, which maps UUIDs to filesystem paths:

| UUID Prefix | App | Framework | Version (observed) |
|-------------|-----|-----------|-------------------|
| `002c8f0a-...` | Hexatris | React/Vite | 0.4.2 |
| `002c6a0c-...` | Polls | React/Vite | 0.4.4 |
| `002c6a0d-a29d-...` | Suggestions | React/Vite | 0.4.4 |
| `002c6a0d-3616-...` | Minecraft Easy Setup | React/Vite | 0.4.5 |
| `002a3d83-e2e0-...` | Task Tracker | React/Vite | 0.4.5 |
| `002a3d84-...` | Stickerwall | Alpine.js + PixiJS | 0.4.8 |
| `002a3d83-c35a-...` | Raid Planner | React/Vite | 0.4.11 |

Each sub-app directory contains a Vite build output: `app/client/dist/assets/index-<hash>.js`, along with CSS, images, and an `index.html` entry point.

### SPA-Like Navigation

Root's overall navigation model is SPA-like from the user's perspective. The native Avalonia shell provides the persistent sidebar, window chrome, and all chat/community UI. DotNetBrowser's role is limited: sub-apps are loaded into their own HTML files within the host container (`Host/index.html`), which acts as the frame orchestrator for these auxiliary features.

Within each sub-app, TanStack Router handles client-side routing. Sub-apps communicate with the native host exclusively through the `__rootSdkBridgeWebToNative` bridge -- they do not make direct network requests to Root's backend. Instead, they serialize protobuf messages and pass them to the bridge's `.send()` method, which forwards them through the .NET host to the backend.

### The WebRTC Context

The WebRTC bundle is loaded separately from the sub-app host. It has its own `index.html` in `WebRtcBundle/` and uses a different set of bridge objects (`__nativeToWebRtc` and `__webRtcToNative`). This is the context where Uprooted's TypeScript layer runs.

The WebRTC context is fundamentally different from the sub-app contexts:

- It manages peer connections, media streams, and signaling
- Its bridge objects are only populated when a voice session is active
- It receives the bearer token via the `initialize()` bridge call
- It has its own Sentry error reporting configuration
- It loads external WASM resources from Effects SDK for audio/video processing

Uprooted injects its preload script into the WebRTC context's `index.html`, making this the primary execution environment for plugins.

### Context Isolation

Each sub-app iframe runs in its own JavaScript context with standard iframe isolation: no cross-context DOM access, no shared globals, `postMessage` with wildcard `"*"` for cross-frame communication, and CSS variables scoped to their own document. Uprooted plugins in the WebRTC context cannot directly modify sub-app UI.

---

## 5. Authentication and Tokens

### Token Architecture

Root's authentication system uses a custom bearer token format -- not JWT. Through source map extraction and runtime analysis, we identified the token structure:

```
Bearer token: 128 bytes (base64url-encoded)
  Bytes  0-15:  userId    (UUID, 16 bytes)
  Bytes 16-31:  deviceId  (UUID, 16 bytes)
  Bytes 32-127: Signature (96 bytes, cryptographic)
```

The 96-byte signature prevents token forgery, but the token has no embedded expiration -- validity is determined server-side.

### Token Flow Between Layers

1. The .NET host authenticates with the backend and obtains a bearer token
2. The token is stored on disk as `Store/AuthToken` (390 bytes, binary/encrypted)
3. When a voice session starts, the .NET host calls `__nativeToWebRtc.initialize({token: {clientToken, userId, deviceId}, ...})`
4. The WebRTC bundle stores the token in its service layer
5. All gRPC service clients and the PacketManager WebSocket use it as `Authorization: Bearer`

### Token Storage Locations

| Location | Format |
|----------|--------|
| `Store/AuthToken` | Binary/encrypted (390 bytes), on disk |
| `data.json` (Roaming) | Encrypted (1,592 bytes), on disk |
| Chromium `Login Data` SQLite | Standard Chromium credential DB, on disk |
| JavaScript memory | Plaintext string, accessible to any WebRTC context code |
| `sessionStorage.__rootUser` | JSON with userId, deviceId, nickname |

### Token Lifecycle

- **No embedded expiration**: The server determines validity; the token carries no expiry timestamp
- **Replay viable**: Extracted tokens can be reused from other contexts
- **RefreshToken endpoint**: `UserGrpcService.RefreshToken` extends access from an existing token
- **No revocation observed**: Closing the app does not invalidate the token server-side
- **Artifacts persist**: `data.json`, `Login Data`, and related SQLite databases remain on disk with no secure deletion

### Implications for Uprooted

Uprooted's bridge proxy intercepts the `initialize()` call, meaning the token passes through our proxy. We do not store, log, or exfiltrate the token. Framework contributors must be aware that bridge interception gives full access to authentication credentials.

---

## 6. Theme System

### Dual Theme Architecture

Root uses two independent theme systems that operate in parallel, targeting different rendering layers:

**1. CSS Variables (Chromium/Web Side)**

25 CSS custom properties per theme variant, controlling all web UI colors. Applied via the `data-theme` attribute on the `<html>` element. Three variants: `dark`, `light`, and `pure-dark`.

**2. AXAML Resources (Avalonia/.NET Side)**

Compiled Avalonia XAML themes embedded in the Root.exe binary. These primarily contain SVG asset path references (theme-specific icon variants), not color definitions. Three compiled themes:

| Theme | Binary Offset | Encoding | Size |
|-------|---------------|----------|------|
| `Light.axaml` | `0x19EED01F` | UTF-16LE | ~28.5 KB |
| `Dark.axaml` | `0x19EF3FB0` | UTF-16LE | ~21.5 KB |
| `PureDark.axaml` | `0x19EF93D8` | UTF-16LE | ~196 B |

### Key Discovery: Color Lives in CSS

The critical finding from binary analysis: **native Avalonia AXAML contains no color brush resources**. All UI colors are delivered via CSS variables to the Chromium webview. The native Avalonia side uses hardcoded hex colors in code, not theme-switchable resources.

Each AXAML theme defines only three header properties plus ~72 SVG resource key mappings:

| Property | Light | Dark | Pure Dark |
|----------|-------|------|-----------|
| `ThemeName` | Light Theme | Dark Theme | Pure Dark Theme |
| `ThemeMargin` | `0 0 12 0` | `0 0 12 0` | `0 0 12 0` |
| `ScrollShadow` | `#19000000` (10% black) | `#80000000` (50% black) | `#000000` (pure black) |
| `LoadingSpinner` | `LoadingSpinnerLight.json` | `LoadingSpinnerDark.json` | (inherits Dark) |

PureDark is minimal (196 bytes) and inherits all SVG references from Dark. It only overrides the header properties.

### CSS Variable System

The CSS theme definitions were extracted from the Root.exe binary at offset `0x12AE9676`. Each variable uses a two-tier override mechanism:

```css
--color-brand-primary: var(--rootsdk-brand-primary, #3B6AF8);
```

The `--color-*` variable is the consumed value. It references a `--rootsdk-*` override variable with a fallback default. This design allows SDK consumers (and Uprooted) to override any color by setting the corresponding `--rootsdk-*` variable, without touching the base `--color-*` definitions.

25 variables per theme. The three variants differ primarily in background and text colors:

- **Dark**: Navy-blue backgrounds (`#0D1521`, `#121A26`, `#07101B`)
- **Pure Dark**: Neutral gray backgrounds (`#161617`, `#1F1F22`, `#111113`)
- **Light**: White/gray backgrounds (`#FBFBFB`, `#FFFFFF`, `#F5F6F8`) with darkened text

Brand primary (`#3B6AF8`) is consistent across all themes. Brand secondary shifts from bright green (`#A8FF5D`) in dark themes to muted green (`#58AC30`) in light theme.

### Theme Switching Flow

1. User selects a theme in Root's settings
2. The .NET host calls `window.__rootSdkBridgeNativeToWeb.setTheme(theme)` on the Chromium side
3. Root's JavaScript sets `data-theme` on the `<html>` element
4. CSS attribute selectors activate the corresponding variable set
5. On the native side, Root swaps the compiled AXAML resource dictionary

Uprooted intercepts this flow at two levels:

- **C# ThemeEngine**: Injects override values into Avalonia's `ResourceDictionary` and `Styles[0].Resources`, allowing native UI color changes. Also manipulates the DWM title bar color via Windows API.
- **TypeScript theme plugin**: Sets `--rootsdk-*` CSS variables on `document.documentElement`, overriding the web UI colors.

### How Uprooted Hooks Into Themes

The C# `ThemeEngine` (2,218 lines) uses reflection to:

1. Access `Application.Current.Resources` (the global Avalonia resource dictionary)
2. Inject `SolidColorBrush` overrides for native controls
3. Monitor `Styles[0].Resources` for theme-specific resource keys
4. Apply DWM title bar coloring via `DwmSetWindowAttribute` on Windows

The TypeScript theme plugin overrides CSS variables using the `--rootsdk-*` mechanism, which was designed for this exact purpose. Setting inline styles on `:root` takes precedence over stylesheet defaults via CSS specificity rules.

For the complete CSS variable table (all 25 variables across all three themes), see [Root Environment -- CSS Variable System](../plugins/ROOT_ENVIRONMENT.md#css-variable-system).

---

## 7. Update Mechanism (Velopack)

### Velopack Integration

Root uses **Velopack v0.0.1298** for automatic updates. Velopack is a cross-platform app packaging and update framework for .NET applications. It manages:

- Delta and full package downloads
- Application restart during updates
- Shortcut creation and management
- Version tracking via the `sq.version` manifest

### Update Flow

The update check sequence:

1. Root queries the update endpoint with current version information:
   ```
   GET https://installer.rootapp.com/installer/Windows/X64/releases.win.json
       ?arch=x64&os=win&rid=win-x64&id=Root&localVersion=0.9.86
   ```

2. The `.betaId` file (a persistent UUID) is included in update requests, enabling installation tracking

3. The server responds with a JSON manifest containing package URLs, SHA256 hashes, and version metadata

4. Velopack downloads the update package, verifies SHA256 hashes, and applies it

5. On Windows, Velopack uses `Update.exe` to manage the update lifecycle

### The sq.version Manifest

Located at `Root\current\sq.version`, this XML file tracks the installed version:

```xml
<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd">
<metadata>
  <id>Root</id>
  <version>0.9.86</version>
  <channel>win</channel>
  <mainExe>Root.exe</mainExe>
  <os>win</os>
  <rid>win</rid>
  <shortcutLocations>Startup,Desktop,StartMenuRoot</shortcutLocations>
  <shortcutAmuid>velopack.Root</shortcutAmuid>
</metadata>
</package>
```

### Autostart

Root configures autostart via a Startup folder shortcut (`Root.lnk`), not a registry Run key. Shortcut locations are specified in `sq.version`: Startup, Desktop, and StartMenuRoot.

### Why Updates Break HTML Patches

When Velopack applies an update, it **replaces the profile directory's web content** -- including the `WebRtcBundle/index.html` and `RootApps/*/index.html` files that Uprooted patches with `<script>` and `<link>` tags.

This is the primary reason Uprooted's `HtmlPatchVerifier` exists. The verifier runs as Phase 0 of the startup sequence (before Avalonia is even loaded) and:

1. Scans all target HTML files for Uprooted's injection markers (`<!-- uprooted:start -->` / `<!-- uprooted:end -->`)
2. Re-patches any files where markers are missing
3. Creates `FileSystemWatcher` instances on `WebRtcBundle/` and each `RootApps/*/` directory
4. When a watched `index.html` is overwritten (e.g., during a Root update), the watcher triggers re-patching with a debounced 1-second delay and a 2-second cooldown to avoid self-triggering

This self-healing mechanism means Uprooted survives Root updates transparently. The user does not need to manually re-install or re-patch after an update. However, there is a brief window during the update where patches are absent -- if the user happens to be using the app during that exact moment, they may see unmodified UI until the watcher fires.

### Update Security Notes

The update manifest contains SHA256 hashes for each package but **no cryptographic signature on the manifest itself**. Standard TLS validation is used (no certificate pinning). If HTTPS were compromised (rogue CA, corporate MITM proxy), the entire update channel could theoretically be hijacked. The `velopack.log` file in the Root install directory records update activity for debugging.

---

## 8. File Layout

### Installation Directory

Root installs to a user-local directory (no admin privileges required):

```
Windows:  %LOCALAPPDATA%\Root\
Linux:    ~/.local/share/Root/      (approximate equivalent)
```

The install directory structure:

```
Root\
  current\
    Root.exe                      617 MB   Self-contained .NET 10 + Avalonia
    uiohook.dll                   709 KB   UI input hooks (keyboard/mouse)
    av_libglesv2.dll              5.4 MB   OpenGL ES rendering
    libSkiaSharp.dll              9.4 MB   Skia 2D graphics (Avalonia renderer)
    libHarfBuzzSharp.dll          1.8 MB   Text shaping
    D3DCompiler_47_cor3.dll       4.9 MB   Direct3D shader compiler
    PresentationNative_cor3.dll   1.2 MB   WPF interop
    wpfgfx_cor3.dll               1.9 MB   WPF graphics
    PenImc_cor3.dll               158 KB   Pen/stylus input
    vcruntime140_cor3.dll         125 KB   VC++ runtime
    libonigwrap.dll               538 KB   Oniguruma regex
    sq.version                    458 B    Velopack version manifest
    rooticon.ico                           Application icon
    rooticonstaging.ico                    Staging environment icon
    Helpers\Emojis\emoji.json     1.9 MB   Emoji data
  packages\
    .betaId                                Persistent installation UUID
  velopack.log                    3 KB     Update log
```

All files in the install directory are user-writable. All EXE/DLL files carry Authenticode signatures, but no runtime signature verification is performed.

### Profile Data Directory

Root stores user data, web content, and cached resources in a separate profile directory (3,646 files, 1.18 GB observed):

```
Windows:  %LOCALAPPDATA%\Root Communications\Root\profile\default\
```

```
profile\default\
  Store\
    AuthToken                     390 B    Authentication token (binary/encrypted)
  RootApps\
    registry.json                          Sub-app UUID-to-path mapping
    002c8f0a-...\0.4.2\           Hexatris
    002c6a0c-...\0.4.4\           Polls
    002c6a0d-a29d-...\0.4.4\      Suggestions
    002c6a0d-3616-...\0.4.5\      Minecraft Easy Setup
    002a3d83-e2e0-...\0.4.5\      Task Tracker
    002a3d84-...\0.4.8\           Stickerwall
    002a3d83-c35a-...\0.4.11\     Raid Planner (largest, 80k+ lines)
  WebRtcBundle\
    rootapp-desktop-webrtc.js     4.2 MB   WebRTC bundle (13,930 lines)
    rootapp-desktop-webrtc.js.map 13.5 MB  Source map (shipped in production)
    index.html                             WebRTC entry point
  DotNetBrowser\
    RootApps\Bundle\Host\
      index.html                           Host iframe container
    WindowsX64\data\                       Chromium internal databases (8 files)
  Cache\                                   FASTER log files, cached media
```

### Roaming Data

Additional data is stored in the roaming profile:

```
Windows:  %APPDATA%\Root Communications\Root\profile\default\
```

```
profile\default\
  data.json                       1,592 B  Encrypted/binary user data
```

### DotNetBrowser Data

DotNetBrowser stores Chromium-level data under `DotNetBrowser\WindowsX64\data\`, including cache databases, `Login Data` (40 KB), `Account Web Data` (76 KB), `Web Data` (150 KB), and `Trust Tokens` SQLite databases. Despite `--incognito` mode, these databases persist on disk after app close with no secure deletion.

### Uprooted's File Locations

Uprooted installs its assets adjacent to (not inside) Root's directories:

```
Windows:  %LOCALAPPDATA%\Root\uprooted\
            uprooted-preload.js          Compiled TypeScript bundle
            uprooted.css                 Compiled CSS
            uprooted-settings.json       Settings (JSON)

Linux:    ~/.local/share/uprooted/
            (same files)
```

The HTML patches in Root's profile directory reference these files via `file://` URLs. The C# hook DLL (`UprootedHook.dll`) is loaded from a separate location configured via environment variables (`DOTNET_STARTUP_HOOKS` or CLR profiler registration).

---

## 9. gRPC Backend

### Protocol Overview

Root's backend communication uses **gRPC-web over HTTPS**. All API traffic goes to `api.rootapp.com`, which sits behind Cloudflare (observed IP: `172.66.154.209`).

The wire format for each request:

```
POST https://api.rootapp.com/root.v2.<ServiceName>/<Method>
Content-Type: application/grpc-web+proto
Authorization: Bearer <token>
Body: [0x00] [4-byte big-endian length] [protobuf binary]
```

The first byte (`0x00`) indicates no compression. The next four bytes encode the protobuf payload length as a big-endian 32-bit integer.

### Service Catalog

Source map extraction revealed **27 gRPC services with 163 total methods** in the `root.*` namespace. These service client stubs were found in the extracted `@rootplatform/apiclient-internal` package.

The services cover the full application domain:

| Service | Methods | Domain |
|---------|---------|--------|
| `UserGrpcService` | 31 | Authentication, profile, settings, blocking |
| `v2.MessageGrpcService` | 15 | Messaging, reactions, pins, search |
| `CommunityAppGrpcService` | 14 | Sub-app management, settings, ratings |
| `WebRtcGrpcService` | 12 | Voice/video sessions, tracks, signaling |
| `CommunityGrpcService` | 11 | Community CRUD, membership |
| `FileGrpcService` | 9 | File upload, search, download |
| `DirectoryGrpcService` | 8 | Directory management |
| `AccessRuleGrpcService` | 7 | Channel/role access control |
| `CommunityMemberBanGrpcService` | 7 | Bans, kicks (single and bulk) |
| `NotificationGrpcService` | 7 | Notification management |
| `ChannelGrpcService` | 6 | Channel CRUD |
| `ChannelGroupGrpcService` | 6 | Channel group management |
| `CommunityMemberGrpcService` | 6 | Member management |
| `CommunityRoleGrpcService` | 6 | Role CRUD |
| `CommunityMemberInviteGrpcService` | 6 | Invitations |
| `DirectMessageGrpcService` | 6 | Direct messages |
| `LinkGrpcService` | 6 | Invite links |
| `AppReviewGrpcService` | 5 | App store reviews |
| `AppStoreGrpcService` | 6 | App store browsing |
| `FriendshipGroupGrpcService` | 5 | Friend groups |
| `CommunityMemberRoleGrpcService` | 4 | Role assignment |
| `AssetGrpcService` | 3 | Asset upload/status |
| `CommunityAppLogGrpcService` | 3 | App audit logs |
| `FriendshipInviteGrpcService` | 2 | Friend requests |
| `FriendshipGrpcService` | 2 | Friend management |
| `CommunityLogGrpcService` | 1 | Community audit log |
| `SupportGrpcService` | 1 | Support tickets |

Active testing against production found 32 actually implemented endpoints across 8 services. The rest return gRPC status 12 (UNIMPLEMENTED), suggesting the client-side stubs were generated ahead of backend implementation.

### UUID Encoding

Root uses a non-standard UUID encoding in protobuf messages:

```protobuf
message UUID {
  fixed64 high64 = 1;   // First 8 bytes of UUID, big-endian
  fixed64 low64  = 2;   // Last 8 bytes of UUID, big-endian
}
```

The UUID is split into two 64-bit halves. Each half is stored as a `fixed64` (little-endian on the wire), but the byte order within each half is big-endian. This requires careful encoding:

```python
def uuid_pb(uuid_str):
    b = uuid.UUID(uuid_str).bytes        # 16 bytes, big-endian
    high = struct.pack("<q", int.from_bytes(b[0:8], "big"))   # to LE wire
    low  = struct.pack("<q", int.from_bytes(b[8:16], "big"))
    return f_fixed64(1, high) + f_fixed64(2, low)
```

### Sub-App Protobuf Services

The seven sub-apps have their own protobuf service definitions that communicate through the `__rootSdkBridgeWebToNative.send()` method rather than making direct network requests. The .NET host forwards these to the backend.

Notable sub-app services include:

- **Raid Planner**: 86 protobuf message types across 7 service endpoints (templates, raids, users, comments, action logs, permissions, community data)
- **Minecraft Easy Setup**: 51 message types covering SFTP management, plugin installation, server commands, and analytics

### Permissions Model

The extracted source includes a permission module with:

- 21 channel-level permission flags (read, write, manage, etc.)
- 14 community-level permission flags (admin, moderate, invite, etc.)

These permissions are enforced server-side but also evaluated client-side for UI display. The `updateMyPermission()` bridge method can modify the client-side permission state, but this does not bypass server-side enforcement.

For detailed protocol documentation including message schemas and field definitions, see [gRPC Protocol](GRPC_PROTOCOL.md).

---

## 10. Effects SDK

### Audio/Video Processing Pipeline

Root integrates the **Effects SDK** (from effectssdk.ai) for real-time audio and video processing in the WebRTC context. This SDK provides AI-powered noise suppression, background effects, and audio enhancement using ONNX Runtime compiled to WebAssembly.

### Runtime Resource Loading

The Effects SDK loads WASM model files at runtime from external CDN URLs:

| Resource | Size | Purpose |
|----------|------|---------|
| `audio-model-3.3.2.wasm` | 11.2 MB | Balanced quality noise suppression |
| `audio-model-2.5.0.tsvb` | 52.8 MB | High quality noise suppression |
| `audio-model-1.0.1.tsvb` | 7.1 MB | Speed-optimized noise suppression |

Base URLs configured in the SDK:

```
Settings.API_URL = "https://effectssdk.ai/sdk/session/"
Settings.SDK_URL = "https://effectssdk.ai/sdk/audio/"
```

The models are downloaded over HTTPS and cached in the browser's Cache API with no expiration and no integrity verification (no SRI hashes, no checksums). The infrastructure runs behind Cloudflare CDN with `Access-Control-Allow-Origin: *`.

### Processing Architecture

The Effects SDK operates within the WebRTC context using Web Workers and AudioWorklets:

- **Effects SDK Worker**: Exchanges `Float32Array` audio frames with the main thread
- **Noise Gate Worklet**: Manages push-to-talk state and talking indicators
- **Native Screen Audio Worklet**: Processes screen share audio buffers

The balanced model runs as native WASM (compiled via Emscripten) with direct `HEAPF32` memory access for real-time audio processing.

### Implications for Uprooted

Uprooted does not currently interact with the Effects SDK pipeline. However, framework contributors should be aware that:

- The SDK has access to raw audio and video streams in the WebRTC context
- External WASM resources are loaded without integrity verification
- The `CUSTOMER_ID` field is empty in the shipped bundle, suggesting a development/free-tier configuration
- Some SDK URLs return 404 (e.g., the session API and older ORT WASM paths), indicating stale references in the codebase

---

## 11. Known Behaviors

### Quirks and Undocumented Behaviors

This section documents behaviors discovered through reverse engineering and runtime observation that are not obvious from the application's public-facing design. These are important for Uprooted contributors to understand, as they affect injection strategy, timing, and reliability.

### Incognito Mode Implications

Root runs Chromium with `--incognito`, which eliminates `localStorage`, `IndexedDB`, and persistent cookies. However, the incognito flag does not prevent:

- `sessionStorage` from working (per-session, cleared on process exit)
- Chromium SQLite databases from persisting on disk after app close
- The Cache API from functioning (used by Effects SDK)
- Session cookies from being set (they just don't persist across restarts)

This creates an inconsistency: `localStorage.setItem()` silently fails or throws, but lower-level Chromium storage mechanisms continue to write to disk. Uprooted must use `window.__UPROOTED_SETTINGS__` (injected via HTML patch) instead of any browser storage API.

### Disabled Web Security

The `--disable-web-security` flag means:

- CORS is completely disabled -- `fetch()` to any origin succeeds
- Same-origin policy is not enforced for cross-origin requests
- This applies globally to all contexts (WebRTC, sub-apps, host iframe)

While this makes Uprooted's job easier (no CORS restrictions for plugin API calls), it also means any code running in the Chromium context can make authenticated requests to any domain.

### Source Map in Production

Root ships `rootapp-desktop-webrtc.js.map` (13.5 MB) to every user's machine alongside the minified bundle. This source map contains the complete original TypeScript source for the WebRTC layer, including:

- 802 original source files
- Full gRPC client stubs for all 27 backend services
- Complete bridge interface definitions
- Mock bridge implementation with test credentials
- Redux middleware and state management logic

This is an informational finding, not a vulnerability, but it is the single most valuable artifact for reverse engineering Root's architecture.

### DevBar Code in Production

Root ships a development toolbar (`DevBar`) in production builds of all sub-apps. The DevBar includes user selector dropdowns, community role browsers, theme selector, and devhost status indicators. While the DevBar appears to be conditionally activated (gated behind a user-agent check for `"rootplatform"`), the code is fully present and functional. The gate is trivially bypassable.

### Bridge Timing and Availability

The WebRTC bridge objects (`__nativeToWebRtc` and `__webRtcToNative`) are only set when a voice session is active. Before the user joins a call:

- `window.__nativeToWebRtc` is `undefined`
- `window.__webRtcToNative` is `undefined`
- Uprooted's bridge proxies are installed preemptively and activate when the bridges appear

The sub-app bridge (`__rootSdkBridgeWebToNative`) is available in sub-app iframes at all times, as it handles non-voice operations like messaging and user management.

### Development Bridge Ports

Root's codebase contains a "dev bridge" that exposes three localhost services:

| Port | Protocol | Purpose |
|------|----------|---------|
| 8080 | WebSocket | Real-time messaging via protobuf |
| 8081 | REST HTTP | File uploads, user search, profile lookup |
| 8082 | WebSocket | Update notifications |

These ports are **closed in production builds**. The code references them as `"http://" + window.location.hostname + ":8080"` and similar patterns, suggesting they activate in development/staging environments. All endpoints are unauthenticated over plaintext HTTP.

### ContentControl.Content Freeze

Directly assigning to `ContentControl.Content` in Avalonia (e.g., replacing a settings page's content) causes the UI thread to freeze indefinitely. This is a known Avalonia behavior when content replacement triggers layout cycles. Uprooted works around this by using a `Grid` overlay pattern -- adding a child to the existing layout rather than replacing content.

### System.Text.Json Prohibition

Using `System.Text.Json` in the hook context causes `MissingMethodException`. This occurs because the CLR profiler injection context has a different assembly loading path than normal .NET applications. Uprooted uses manual string concatenation for all JSON construction in the C# hook layer. The `HtmlPatchVerifier.BuildSettingsJson()` method demonstrates this pattern.

### RoutedEvent Handler Registration

Using `EventInfo.AddEventHandler()` for Avalonia RoutedEvents fails silently or throws. Avalonia's RoutedEvent system is not compatible with .NET's standard event handler registration via reflection. Uprooted uses Expression lambda compilation to register event handlers through reflection, bypassing the `EventInfo` mechanism entirely.

### CSS Class Instability

Root's React/Vite sub-apps use CSS modules or similar tooling, producing class names like `_container_a1b2c` that change between builds. DOM selectors relying on these class names break after every Root update. Stable selectors include `data-*` attributes, ARIA attributes, tag structure, and ID attributes where present.

### postMessage Wildcard Origin

Multiple components in Root use `postMessage(data, "*")` with a wildcard target origin. The Sentry session replay system sends full DOM snapshots, mouse movements, and input values via `postMessage` to `"*"`, and various cross-iframe communication follows the same pattern. In the DotNetBrowser context, this is somewhat mitigated by the absence of untrusted windows.

### Sentry Telemetry

Root has two separate Sentry projects: one for sub-apps (SDK v1.33.7) and one for the WebRTC bundle (with `sendDefaultPii: true` and `replaysOnErrorSampleRate: 0.25`). The WebRTC configuration means 25% of errors trigger session replay recording sent to Sentry's US servers. No `beforeSend` or `beforeBreadcrumb` hooks scrub sensitive data.

Uprooted's built-in `sentryBlocker` plugin intercepts and blocks Sentry reporting from the WebRTC context -- both a privacy feature and a practical necessity to prevent Uprooted's presence from being reported via error telemetry.

### Root URI Scheme

Root defines a custom `root://` URI scheme for internal asset references (e.g., `root://image/<id>`). The bridge method `uriToUrl()` resolves these to HTTP URLs. For non-`root://` schemes, input is returned unchanged -- including `javascript:` and `data:` URIs. The `uriToImageUrl()` method has a double-`?` bug (`asset?query=...?resolution=...`) causing the resolution parameter to be misinterpreted.

### Process Guard

Uprooted's `StartupHook.Initialize()` checks `Environment.ProcessPath` for "Root" (case-insensitive) and returns immediately for non-Root processes. This prevents the hook from activating in other .NET processes that share environment variable configuration.

---

## Appendix: Network Endpoints and Code Signing

### External Endpoints

| Endpoint | Protocol | Purpose |
|----------|----------|---------|
| `api.rootapp.com` | HTTPS/WSS | Backend API + WebSocket messaging |
| `installer.rootapp.com` | HTTPS | Velopack auto-updates |
| `o447951.ingest.sentry.io` | HTTPS | Sentry telemetry (sub-apps) |
| `o4509469920133120.ingest.us.sentry.io` | HTTPS | Sentry telemetry (WebRTC) |
| `effectssdk.ai` | HTTPS | WASM audio/video AI models |
| `127.0.0.1:<dynamic>` | TCP (binary) | DotNetBrowser IPC |

All external endpoints behind Cloudflare. No certificate pinning on any connection. No services, scheduled tasks, or raw listeners beyond the dynamic DotNetBrowser IPC port.

### Code Signing

All 11 binaries in Root's install directory are Authenticode-signed. Root Communications, Inc. signs the primary binaries (`Root.exe`, `av_libglesv2.dll`, `libonigwrap.dll`, `uiohook.dll`), Microsoft signs the runtime libraries, and Microsoft .NET signs the WPF interop DLLs. The Root certificate is issued by Microsoft ID Verified CS AOC CA 02 (Azure Code Signing) with notably short validity windows (3-day periods observed), suggesting automated CI/CD signing with ephemeral certificates. No runtime Authenticode verification is performed on loaded binaries.
