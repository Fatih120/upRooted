<p align="center">
  <img src="https://uprooted.sh/og.png" width="700" alt="uprooted" />
</p>

<p align="center">
  a client mod framework for root communications
</p>

<p align="center">
  <a href="https://uprooted.sh"><img src="https://img.shields.io/badge/web-uprooted.sh-2D7D46?style=flat" alt="website" /></a>
  <a href="https://github.com/The-Uprooted-Project/uprooted/releases/latest"><img src="https://img.shields.io/badge/download-latest-2D7D46?style=flat" alt="download" /></a>
  <a href="https://github.com/The-Uprooted-Project/uprooted/releases"><img src="https://img.shields.io/github/downloads/The-Uprooted-Project/uprooted/total?style=flat&color=2D7D46&label=downloads" alt="downloads" /></a>
  <img src="https://img.shields.io/badge/version-0.5.1-dev3-2D7D46?style=flat" alt="version" />
  <img src="https://img.shields.io/badge/license-custom-blue?style=flat" alt="license" />
  <img src="https://img.shields.io/badge/platform-windows | linux-lightgrey?style=flat" alt="platform" />
</p>

---

## what is this

Uprooted is a mod framework for Root Communications (like Vencord for Discord). It injects custom UI, themes, and plugins into Root's desktop app at runtime without modifying the binary. Two independent injection layers work together: a C# .NET hook for native Avalonia UI and a TypeScript layer for the embedded Chromium browser.

Root is a 617 MB self-contained .NET 10 desktop app built on Avalonia UI 11 with an embedded DotNetBrowser (Chromium) for WebRTC, OAuth, and sub-applications. The main chat interface is rendered natively in Avalonia, not in the browser.

## how injection works

### the CLR profiler

.NET provides a CLR Profiler API that lets a native DLL hook into the runtime before any managed code runs. Our profiler (`uprooted_profiler.dll`, written in C) registers itself through environment variables and gets loaded by the .NET runtime at process startup.

When Root loads a suitable module, the profiler injects 26 bytes of IL (Intermediate Language) into the first available method body. That IL calls `Assembly.LoadFrom` to load our managed hook DLL, wrapped in a try/catch so Root continues normally if anything fails. The profiler's job is done after this single injection: Root's original code runs immediately after, completely unaware.

The injected IL loads `UprootedHook.dll` and creates an instance of `UprootedHook.Entry`. The entry point uses both `[ModuleInitializer]` and a constructor fallback with `Interlocked.CompareExchange` to guarantee exactly one initialization regardless of which path fires first.

### phased startup

The hook spawns a background thread named `Uprooted-Injector` (never blocks Root's UI thread) and walks through a sequence of phases:

| Phase | What it does | Timeout |
|-------|-------------|---------|
| 0 | Verify and repair HTML patches for TypeScript injection. Start a FileSystemWatcher to self-heal if Root's auto-updater overwrites them | None (non-fatal) |
| 1 | Poll every 250ms for the `Avalonia.Controls` assembly to appear in the AppDomain | 30s |
| 2 | Scan all loaded assemblies and cache ~80 Avalonia types via reflection. `Type.GetType()` does not work in single-file .NET apps, so we build our own type dictionary | 30s |
| 3 | Wait for `Application.Current` to become non-null | 30s |
| 3.5 | Initialize the theme engine: inject a ResourceDictionary into Avalonia's style tree, apply the saved theme | - |
| 4 | Wait for `MainWindow` via the `ApplicationLifetime` property chain | 60s |
| 4.5 | (Optional) DotNetBrowser assembly detection and IBrowser discovery | 90s |
| 5 | Start the sidebar monitoring timer (200ms poll) | - |

If any phase times out, the hook logs the failure and stops. Root keeps running normally.

> For full implementation detail, see [HOOK_REFERENCE.md](docs/framework/HOOK_REFERENCE.md) and [ARCHITECTURE.md](docs/framework/ARCHITECTURE.md).

### sidebar injection

A 200ms timer polls the visual tree for a `TextBlock` with the text `"APP SETTINGS"`. When the user opens Root's settings page, the timer finds this anchor and runs a 6-step visual tree walk to discover the layout structure: the nav container (StackPanel with 8+ children), the content area (Grid with 2+ columns), the ListBox, and the back button.

Once the layout is mapped, the injector adds an `"UPROOTED"` section header and three nav items ("About", "Plugin Settings", "Theme Settings") into the sidebar. Content pages are shown using a Grid overlay pattern: instead of replacing `ContentControl.Content` (which causes an Avalonia UI freeze), our page is added as a sibling in the same Grid cell with an opaque background, covering Root's content via z-order.

Everything injected is tagged with `Control.Tag = "uprooted-injected"` for clean removal when the settings page closes.

### the TypeScript layer

Root runs DotNetBrowser (Chromium) for WebRTC, OAuth, and 7 sub-applications. The installer patches HTML files in Root's profile directory to add `<script>` and `<link>` tags that load our TypeScript bundle before Root's own JavaScript.

The preload script installs ES6 Proxy wrappers on Root's bridge globals (`window.__nativeToWebRtc`, `window.__webRtcToNative`) using `Object.defineProperty` setters, so any code running in the browser context can intercept IPC calls between Root's .NET host and Chromium. Plugins can inspect, modify, or cancel bridge calls.

Root runs Chromium with `--incognito`, so `localStorage` is wiped on every launch. Settings are persisted to a JSON file and inlined into the HTML at patch time.

## features

### native Avalonia

- **Settings pages** injected into Root's sidebar: About, Plugin Settings, Theme Settings
- **Theme engine** with runtime switching: ResourceDictionary injection for Avalonia controls, DWM title bar coloring on Windows 11, preset themes (Default, Crimson, Loki, Cosmic Smoothie)
- **Live theme preview** with HSV color picker: visual tree walk recolors controls during drag at 60fps
- **Link embeds**: Discord-style rich previews for URLs posted in chat. Fetches OpenGraph and oEmbed metadata, renders native Avalonia embed cards with images. Supports YouTube, Twitter/X, and any site with OG tags. Animated GIF/WebP playback via SkiaSharp reflection
- **Direct image embeds**: image URLs (.jpg, .png, .gif, .webp) render inline with zero network overhead
- **Self-healing HTML patches**: FileSystemWatcher detects when Root's auto-updater overwrites patched files and re-patches them within seconds

### browser-side (TypeScript)

- **Plugin system** with lifecycle hooks, bridge method interception, and CSS injection
- **Sentry blocker**: intercepts `fetch`, `XMLHttpRequest`, and `navigator.sendBeacon` to block telemetry
- **CSS theme engine**: overrides Root's `--rootsdk-*` CSS variables for web UI theming
- **Bridge proxies**: ES6 Proxy wrappers on all 70+ IPC bridge methods between .NET and Chromium

### installer

- **Console TUI installer** (~600KB single binary, Rust/ratatui) replacing the old 100MB Tauri GUI
- Automatic Root detection, file deployment, environment variable management
- HTML patching with backup/restore
- Auto-closes Root before install, repair, and uninstall
- `--plain` mode for CI and scripting, `--diagnose` mode for troubleshooting

## project structure

```
uprooted-private/
  hook/                     C# .NET hook (injected into Root.exe via CLR profiler)
    Entry.cs                  profiler injection entry point
    StartupHook.cs            multi-phase startup orchestrator
    AvaloniaReflection.cs     reflection cache for ~80 Avalonia types
    SidebarInjector.cs        200ms timer poll, sidebar injection, click events
    ContentPages.cs           settings page builders (About, Plugins, Themes)
    ThemeEngine.cs            ResourceDictionary overrides, live preview, DWM title bar
    LinkEmbedEngine.cs        link embed engine (OG/oEmbed fetch + visual tree injection)
    AnimatedImage.cs          animated GIF/WebP decoder (SkiaSharp reflection)
    MessageLogger.cs          message logger: edit/delete detection, visual indicators
    MessageStore.cs           flat-file persistence for message log
    HtmlPatchVerifier.cs      self-healing HTML patches + FileSystemWatcher
    VisualTreeWalker.cs       DFS visual tree traversal, settings layout discovery
    ColorPickerPopup.cs       HSV color picker overlay
    ColorUtils.cs             HSL/HSV/RGB color conversion
    NativeEntry.cs            native hostfxr entry point
    NsfwFilter.cs             NSFW filter (needs Avalonia-native redesign)
    PlatformPaths.cs          cross-platform path resolution
    BrowserDiscovery.cs       Phase 4.5 diagnostic scanner
    UprootedSettings.cs       INI-based settings (System.Text.Json is broken in profiler context)
    DotNetBrowserReflection.cs  reflection cache for DotNetBrowser types
    Logger.cs                 thread-safe file logging with structured wide events
    WideEvent.cs              structured event builder (key=value serialization, duration tracking)
    TailSampler.cs            periodic heartbeat aggregator for high-frequency scan loops
    LogConsole.cs             named pipe server for real-time log streaming (dev only)

  src/                      TypeScript browser injection layer
    core/preload.ts           browser entry point, bridge proxy install
    core/pluginLoader.ts      plugin lifecycle manager
    core/patcher.ts           HTML file injection
    api/bridge.ts             ES6 Proxy wrappers for Root's IPC bridges
    plugins/sentry-blocker/   blocks Sentry telemetry
    plugins/themes/           CSS variable theme engine
    plugins/settings-panel/   DOM-injected settings UI
    plugins/link-embeds/      browser-side link previews

  tools/                    native profiler and build utilities
    uprooted_profiler.c       CLR profiler DLL source (Windows)
    uprooted_profiler_linux.c CLR profiler shared object (Linux)

  installer/src-tauri/src/  console TUI installer (Rust)
    main.rs                   ratatui TUI entry point
    cli.rs                    plain/diagnose modes
    patcher.rs                HTML patch install/uninstall/repair
    hook.rs                   file deployment + env var management
    detection.rs              Root installation detection

  scripts/                  build and install automation
    build-installer.ps1       full installer build with embedded artifacts
    install-hook.ps1          hook deployment script
    diagnose.ps1              installation diagnostics
```

## build

```bash
# C# hook
dotnet build hook/UprootedHook.csproj -c Release

# TypeScript bundle
pnpm build

# console TUI installer
cd installer/src-tauri && cargo build --release

# full installer with all embedded artifacts
powershell -File scripts/build-installer.ps1
```

## the boot sequence

Every time Root.exe launches after installation:

```
 1. .NET runtime sees DOTNET_ENABLE_PROFILING=1
 2. Runtime loads uprooted_profiler.dll as CLR profiler
 3. Profiler checks process name: must be "Root"
 4. Profiler waits for a suitable module to load
 5. Profiler injects 26 bytes of IL into first available method
 6. IL loads UprootedHook.dll via Assembly.LoadFrom
 7. Entry.ModuleInit() fires, spawns background thread
 8. Phase 0: verify/repair HTML patches, start FileSystemWatcher
 9. Phase 1-2: wait for Avalonia, cache ~80 types via reflection
10. Phase 3: wait for Application.Current
11. Phase 3.5: initialize theme engine, apply saved theme
12. Phase 4: wait for MainWindow
13. SidebarInjector starts 200ms monitoring timer

    ... user opens Settings ...

14. Timer finds "APP SETTINGS" TextBlock
15. Visual tree walk discovers layout structure
16. Sidebar injection: UPROOTED section + nav items + content pages

    ... meanwhile, in Chromium ...

17. DotNetBrowser loads patched HTML files
18. uprooted-preload.js runs before Root's bundles
19. Bridge proxies installed, plugins started, CSS themes applied
```

## what breaks on Root updates

| Root change | Impact |
|------------|--------|
| Renames "APP SETTINGS" text | Visual tree walker can't find anchor, no injection |
| Changes settings Grid layout | Content area detection fails |
| Moves to AOT compilation | Profiler IL injection stops working |
| Moves HTML to different path | Patcher can't find files, TypeScript layer not injected |
| Adds HTML integrity checks | Patcher modifications detected |
| Switches away from DotNetBrowser | Bridge globals don't exist |
| Uses Object.freeze on bridges | Proxy wrapping fails |

## terms of use

**by using uprooted, you agree to the following:**

1. **do not distribute uprooted or its artifacts** (installers, DLLs, modified binaries) outside of this repository.
2. **do not discuss uprooted in Root's public channels** (official Root servers, Root support, Root social media).
3. violations will result in your UUID being **permanently blacklisted** from uprooted.

these rules exist to protect the project and its users. if you want to share uprooted with someone, send them a link to this repository.

## policy

**uprooted is not affiliated with root communications.** this is an independent community project. all modifications are cosmetic-only and do not interact with root's backend services.

## links

- [uprooted.sh](https://uprooted.sh)
- [download latest release](https://github.com/The-Uprooted-Project/uprooted/releases/latest)
- [uprooted server](https://rootapp.gg/AC0ILwUxgQqJ2MOSMXdGjw)
- admin@watchthelight.org

## license

[uprooted license v1.0](LICENSE) - use pieces with credit, don't redistribute the whole thing

---

**Canonical for:** repository landing page, project overview, feature list, quick-start install
**Not canonical for:** architecture detail → [ARCHITECTURE.md](docs/framework/ARCHITECTURE.md) | implementation → [HOOK_REFERENCE.md](docs/framework/HOOK_REFERENCE.md) | full install guide → [INSTALLATION.md](docs/install/INSTALLATION.md)
*Repository landing page for Uprooted v0.5.0. Last updated 2026-02-23.*
