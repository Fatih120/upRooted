# New Session Quick-Start

> **Related docs:** [CLAUDE.md](CLAUDE.md) | [Architecture](docs/framework/ARCHITECTURE.md) | [Index](docs/INDEX.md) | [Session State](hook/SESSION_STATE.md) | [Tasks](TASKS.md)

---

## 1. Project Identity

**Uprooted** -- client mod framework for Root Communications desktop app (like Vencord for Discord).
Version: 0.3.6rc. Target: Root v0.9.92+.
This is the **PRIVATE** repo (`watchthelight/uprooted-private`). Never leak code to the public repo (`watchthelight/uprooted`).
Contributors: `watchthelight` (owner), `agomusio` (admin).

## 2. Architecture One-Liner

Two independent injection layers into one app:
1. **C# .NET hook** (`hook/`) -- CLR profiler IL-injects into Root.exe, adds native Avalonia sidebar/settings UI via reflection.
2. **TypeScript browser injection** (`src/`) -- `<script>` tags in HTML inject into DotNetBrowser Chromium (WebRTC + sub-apps, NOT chat); plugin runtime, theme CSS, bridge proxies.

## 3. Critical Rules -- Never Do These

- [ ] **Never use `Type.GetType()` for Avalonia types** -- fails silently in single-file .NET 10. Use `AvaloniaReflection`.
- [ ] **Never modify `ContentControl.Content` directly** -- freezes Avalonia UI permanently. Use Grid overlay pattern.
- [ ] **Never use `System.Text.Json` in hook** -- `MissingMethodException` in profiler context. Use INI-based `UprootedSettings`.
- [ ] **Never use `EventInfo.AddEventHandler` for RoutedEvents** -- incompatible with Avalonia. Use `AvaloniaReflection.SubscribeEvent()` (Expression lambdas).
- [ ] **Never use `localStorage`** -- Root runs Chromium with `--incognito`, wipes on every launch. Use file-based settings.
- [ ] **`DispatcherPriority` is a struct, not enum** -- do not use `Enum.Parse`. `RunOnUIThread()` handles the fallback chain.
- [ ] **Never block the UI thread** -- all heavy work on background thread, UI mutations via `RunOnUIThread()`.
- [ ] **Never add children to `VirtualizingStackPanel`** -- items get recycled. Add to parent container.
- [ ] **Never cross-contaminate repos** -- private and public repos are strictly separate.

## 4. File Map

### C# Hook Layer (`hook/`)

| File | Lines | Purpose |
|------|------:|---------|
| `Entry.cs` | 33 | Profiler injection entry point, `[ModuleInitializer]` guard |
| `NativeEntry.cs` | 66 | Alternative entry via hostfxr, diagnostic logging |
| `StartupHook.cs` | ~340 | Multi-phase startup orchestrator (Phase 0-5) |
| `HtmlPatchVerifier.cs` | 429 | Phase 0: self-healing HTML patches + FileSystemWatcher |
| `AvaloniaReflection.cs` | 2030 | Reflection cache for ~80 Avalonia types (CRITICAL, largest file) |
| `VisualTreeWalker.cs` | 554 | DFS visual tree traversal, settings layout discovery |
| `SidebarInjector.cs` | 1366 | 200ms timer poll, sidebar injection, header management, click events |
| `ContentPages.cs` | ~2450 | Settings page builders (Uprooted, Plugins, Themes) |
| `ThemeEngine.cs` | 2360 | ResourceDictionary overrides, live theme preview (2nd largest) |
| `ColorPickerPopup.cs` | 533 | HSV color picker overlay for custom accent/bg |
| `ColorUtils.cs` | 262 | HSL/RGB conversion, contrast calculation |
| `UprootedSettings.cs` | ~145 | INI-based settings (System.Text.Json workaround) + 10s TTL cache |
| `DotNetBrowserReflection.cs` | 1914 | Reflection cache for DotNetBrowser types, IBrowser discovery |
| `BrowserDiscovery.cs` | 496 | Phase 4.5 diagnostic scanner (visual tree + assembly dump) |
| `ClearUrlsEngine.cs` | 467 | ClearURLs: strip tracking params from compose editor URLs on send (AvaloniaEdit routed event interception) |
| `LinkEmbedEngine.cs` | 1754 | Avalonia-native link embed engine (OG/oEmbed fetch + animated image embeds + visual tree injection) |
| `MessageLogger.cs` | ~1230 | Message logger (WIP): per-type property cache, event-based deletion via Remove events, post-subscription settling filter, Discord-style deleted message rows, channel switch handling |
| `MessageStore.cs` | 232 | Flat-file persistence for message log (pipe-delimited, URI-encoded, append-only) |
| `AnimatedImage.cs` | 795 | Animated GIF/WebP decoder + timer playback (SkiaSharp reflection) |
| `AutoUpdater.cs` | ~810 | In-process auto-updater (encrypted .uprpkg download, GitHub releases, HTTP via reflection, version compare) |
| `ProfileBadgeInjector.cs` | ~340 | "Uprooted Dev" profile badge injector (dev channel only, popup tree scan) |
| `NsfwFilter.cs` | 305 | NSFW filter JS injection (needs Avalonia-native redesign) |
| `PlatformPaths.cs` | 29 | Cross-platform path resolution |
| `Logger.cs` | 46 | Thread-safe file logging, swallows own exceptions |

### TypeScript Layer (`src/`)

| File | Purpose |
|------|---------|
| `core/preload.ts` | Browser entry point, bootstrap, bridge proxy install |
| `core/pluginLoader.ts` | Plugin lifecycle manager, event router |
| `core/patcher.ts` | HTML file injection (`<script>` + `<link>` tags) |
| `api/bridge.ts` | ES6 Proxy wrappers for Root's IPC bridge globals |
| `api/css.ts` | Inject/remove `<style>` elements by ID |
| `api/dom.ts` | `waitForElement()`, `observe()`, `nextFrame()` |
| `plugins/sentry-blocker/` | Blocks Sentry telemetry (fetch/XHR/sendBeacon) |
| `plugins/themes/` | CSS variable theme engine (`--rootsdk-*` overrides) |
| `plugins/settings-panel/` | DOM-injected settings UI in browser sidebar |
| `plugins/link-embeds/` | Discord-style link previews (OpenGraph + YouTube, browser-side) |
| `plugins/nsfw-filter/` | NSFW content filtering (browser-side) |
| `types/plugin.ts` | `UprootedPlugin`, `Patch`, `SettingField` interfaces |
| `types/bridge.ts` | `INativeToWebRtc` (42 methods), `IWebRtcToNative` (29 methods) |

### Other Key Files

| File | Purpose |
|------|---------|
| `tools/uprooted_profiler.c` | CLR profiler DLL source (Windows) |
| `installer/src-tauri/src/patcher.rs` | Rust: HTML patch install/uninstall/repair |
| `installer/src-tauri/src/hook.rs` | Rust: file deployment + env var management (DOTNET_ + CORECLR_) |
| `scripts/build_installer.ps1` | Full installer build pipeline (5 steps) |
| `scripts/build.ts` | esbuild bundler: `src/` -> `dist/` (IIFE + CSS) |
| `scripts/pack-update.py` | Packs 6 update files into encrypted `.uprpkg` (standalone Python, `--verify` flag) |
| `Install-Uprooted.ps1` | PowerShell one-click installer |

## 5. Reading Order

| Order | Document | Why |
|------:|----------|-----|
| 1 | `NEW-SESSION.md` (this file) | Orientation, rules, file map |
| 2 | `docs/framework/ARCHITECTURE.md` | Layer boundaries, data flow, all constraints |
| 3 | `hook/SESSION_STATE.md` | What changed last, pending issues, build commands |
| 4 | `docs/framework/HOOK_REFERENCE.md` | C# hook deep dive (all 18 .cs files) |
| 5 | `docs/HOW_IT_WORKS.md` | Narrative walkthrough from reverse engineering to running mod |

## 6. Current State

**Source:** `hook/SESSION_STATE.md` (2026-02-18)

**Versions:** 0.3.6rc | Target Root 0.9.92

**Critical finding (2026-02-17):**
- **Chat is Avalonia-native** -- 1647+ visual tree nodes, 0 browser controls. DotNetBrowser is auxiliary (WebRTC, OAuth, sub-apps), NOT the chat renderer.
- DotNetBrowser loads `rootapp://app/__index.html` -- shell page with `<iframe id="app-iframe">` permanently at `about:blank`
- `DotNetBrowser.AvaloniaUi` NOT loaded -- Root accesses browser engine programmatically via ViewModel chain

**Recent work (all working):**
- Settings header: back arrow hidden on Uprooted tabs, page title set in header, X close button preserved
- Settings tabs renamed: sidebar "Uprooted" → "About", content headers "Plugins" → "Plugin Settings", "Themes" → "Theme Settings"
- Settings crash fix: back arrow click no longer freezes Root (DetachedFromVisualTree safety net + Click event for Buttons)
- LinkEmbedEngine broadly functional: YouTube, Twitter/X, generic OG sites, direct image URLs, oEmbed discovery
- Animated GIF/WebP embeds playing inline via SkiaSharp `SKCodec` reflection (`AnimatedImage.cs`)
- Direct image URL fast path (`.jpg`, `.png`, `.gif`, `.webp`, `.bmp`, `.svg` -- zero network)
- Content-Type gate on OG fetch (skips PDFs/binaries, synthesizes image embed for `image/*`)
- Bot UA for Twitter/X and embed-fixer domains; embed-fixer normalization (fixupx/fxtwitter/fixvx → vxtwitter.com)
- Tenor URL skip (Root embeds natively, avoids double-embedding)
- Settings cache: 10s TTL on `UprootedSettings.Load()` to reduce disk I/O
- Console TUI installer replaces Tauri GUI (~600KB vs ~100MB)
- DotNetBrowserReflection: full type cache, IBrowser discovery via ViewModel chain walking
- Theme preset: "Cosmic Smoothie" (purple accent #7328BA, dark bg #0A041E) — full TreeColorMap + ResourceDictionary + CSS variables
- Plugin search box: font size bump, horizontal padding, vertical centering
- ClearURLs plugin: strips tracking params (utm_source, fbclid, gclid, si, etc.) from URLs on send via AvaloniaEdit TextArea routed event interception
- MessageLogger plugin (WIP): event-based deletion detection via CollectionChanged Remove events, per-type ViewModel property cache, post-subscription settling filter, Discord-style deleted message rows (red-tinted stripe + left accent), channel switch handling, flat-file persistence (MessageStore.cs), settings UI with toggle pills. Edit detection disabled pending reliability fix.
- TUI installer mode selector: interactive Install/Uninstall/Repair menu when run without flags
- Linux Root detection: 7-strategy search in bash installer, 5-strategy search in Rust installer (glob, .desktop, /proc, PATH)
- AutoUpdater: encrypted `.uprpkg` package download (single file replaces 6 individual artifacts), multi-layer XOR decryption, developer channel with encrypted PAT, staging + verify + overwrite
- ProfileBadgeInjector: "Uprooted Dev" badge on profile popups (dev channel only), diagnostic tree dump on first detection
- Restart banners: plugins page (state-aware — hides when user reverts), updates section; both with Restart button
- DIAGNOSTICS card: "Open" button opens log file in Explorer

**Known issues:**
- Reddit embeds not yet implemented (OG tags available but no dedicated handler)
- Video preview embeds (.mp4) not yet implemented
- NSFW filter needs Avalonia-native redesign (chat is not in DotNetBrowser)
- `after` patch handler defined in interface but not yet invoked by PluginLoader
- MessageLogger (WIP): edit detection disabled (false positives from content changes during send/render); edit indicators disabled (break message layout); deletion detection relies on Remove events which may need tuning for new Root behaviors
- ProfileBadgeInjector: needs real-world popup structure from tree dump logs to refine heuristics

## 7. Build Commands

```bash
# TypeScript bundle (src/ -> dist/)
pnpm build

# C# hook (hook/ -> hook/bin/Release/net10.0/UprootedHook.dll)
dotnet build hook/ -c Release

# C# tests
dotnet test tests/

# Console TUI installer
cd installer/src-tauri && cargo build --release

# Full installer with all embedded artifacts
powershell -File scripts/build_installer.ps1
```

### Devcontainer Deployment

Claude runs inside a devcontainer and **cannot deploy or launch Root**. If `/deploy` fails with permission errors, that's expected — the container has no access to the Windows host filesystem.

**The user handles deployment and log tailing on their Windows machine:**
- `scripts/deploy-hook.ps1` — stops Root, copies the DLL, relaunches via UprootedLauncher
- `scripts/watch-log.ps1` — tails the hook log in real time

The workspace is bind-mounted, so `dotnet build hook/ -c Release` inside the container produces output visible to both sides. Claude builds, the user deploys.

## 8. Where Things Live

| Question | Answer |
|----------|--------|
| Where is the C# entry point? | `hook/Entry.cs` (profiler) or `hook/NativeEntry.cs` (hostfxr) |
| Where is the TypeScript entry point? | `src/core/preload.ts` |
| Where is the startup sequence? | `hook/StartupHook.cs` -- Phase 0-5 (4.5a-e for deferred features) |
| Where is Avalonia reflection? | `hook/AvaloniaReflection.cs` (1943 lines) |
| Where is the sidebar injection? | `hook/SidebarInjector.cs` |
| Where are settings pages built? | `hook/ContentPages.cs` |
| Where is the theme engine? | `hook/ThemeEngine.cs` (C#/Avalonia) or `src/plugins/themes/` (CSS) |
| Where is the bridge proxy? | `src/api/bridge.ts` |
| Where is the plugin interface? | `src/types/plugin.ts` |
| Where are settings stored? | `uprooted-settings.ini` (C# hook) or `uprooted-settings.json` (TypeScript) |
| Where is the profiler DLL source? | `tools/uprooted_profiler.c` |
| Where is the installer backend? | `installer/src-tauri/src/` (Rust) |
| Where is the HTML patcher? | `src/core/patcher.ts` (TS) or `hook/HtmlPatchVerifier.cs` (C# self-heal) |
| Where are build scripts? | `scripts/` -- `build.ts`, `build_installer.ps1`, `install-hook.ps1` |
| Where is the hook log? | `%LOCALAPPDATA%/Root Communications/Root/profile/default/uprooted-hook.log` |
| Where is the profiler log? | `%LOCALAPPDATA%/Root/uprooted/profiler.log` |
| Where is session state? | `hook/SESSION_STATE.md` |
| Where are the docs? | `docs/INDEX.md` is the navigation hub |

## 9. Collaboration Rules

- **Always `git pull` before starting work** -- another contributor may have pushed
- **Always push after committing** -- do not leave unpushed commits
- **Commit format:** `type: concise description` -- types: `fix`, `feat`, `refactor`, `docs`, `chore`, `style`
- **Never force-push to main** without explicit approval from both contributors
- **Resolve merge conflicts carefully** -- never discard the other person's work
- **Check `git log` before committing** to match recent style
- **Private/public repos are strictly separate** -- never cross content between them
- **Two contributors:** `watchthelight` (owner) + `agomusio` (admin)
- **Document discovered quirks** -- when you discover important new quirks or unexpected behaviors in Root or Uprooted's code (runtime gotchas, API surprises, silent failures, platform-specific edge cases), always document them in the relevant docs. Update `NEW-SESSION.md` (Critical Rules or Common Pitfalls), `docs/framework/HOOK_REFERENCE.md`, `docs/framework/ARCHITECTURE.md`, or the appropriate reference file. Knowledge that stays only in code comments or session context gets lost.

## 10. Common Pitfalls

| # | Mistake | Fix |
|--:|---------|-----|
| 1 | Using `Type.GetType("Avalonia.Controls.TextBlock")` | Use `AvaloniaReflection` -- single-file .NET breaks qualified names |
| 2 | Setting `ContentControl.Content = newControl` | Use Grid overlay pattern (add sibling, match Column/Row, opaque bg) |
| 3 | Calling `System.Text.Json.JsonSerializer.Serialize()` in hook | Use `UprootedSettings` (INI format) or manual string parsing |
| 4 | Using `eventInfo.AddEventHandler(target, handler)` | Use `AvaloniaReflection.SubscribeEvent()` with Expression lambdas |
| 5 | Storing data in `localStorage` | Root uses `--incognito`; use file-based settings inlined by patcher |
| 6 | Treating `DispatcherPriority` as an enum | It is a struct in Avalonia 11+; `RunOnUIThread()` handles fallback |
| 7 | Creating Avalonia controls on background thread | Dispatch via `resolver.RunOnUIThread(action)` -- cross-thread = crash |
| 8 | Ignoring null from `AvaloniaReflection` methods | Every method can return null; types may not exist in future Avalonia |
| 9 | Confusing the two settings systems | C# uses `uprooted-settings.ini` (INI); TS uses `uprooted-settings.json` (JSON) |
| 10 | Assuming TS changes affect native Avalonia UI | They are separate runtimes; TS = Chromium, C# = .NET; no shared state |

## 11. Document Index

### Core Documentation

| Document | Path | Description |
|----------|------|-------------|
| New Session Guide | `NEW-SESSION.md` | This file -- AI agent quick-start reference card |
| AI Contributor Guide | `CLAUDE.md` | Repository rules, structure, build commands for AI sessions |
| Architecture | `docs/framework/ARCHITECTURE.md` | System design, layer boundaries, data flow, critical rules |
| How It Works | `docs/HOW_IT_WORKS.md` | Narrative technical walkthrough from RE to running mod |
| Hook Reference | `docs/framework/HOOK_REFERENCE.md` | C# hook deep dive -- all 18 .cs files |
| TypeScript Reference | `docs/framework/TYPESCRIPT_REFERENCE.md` | Browser injection layer -- plugin runtime, bridge, themes |
| CLR Profiler | `docs/framework/CLR_PROFILER.md` | Native C profiler DLL -- IL injection, env vars, attach |
| Installer Reference | `docs/framework/INSTALLER.md` | Console TUI installer -- detection, patching, deployment |
| Build Guide | `docs/install/BUILD.md` | Build pipeline for all layers |
| Installation Guide | `docs/install/INSTALLATION.md` | End-user install/uninstall instructions |
| Roadmap | `docs/ROADMAP.md` | Known issues, planned features, future direction |
| Documentation Index | `docs/INDEX.md` | Navigation hub for all documentation |
| Contributing | `CONTRIBUTING.md` | Branch rules, PR process, code style |
| Session State | `hook/SESSION_STATE.md` | Last session context handoff (what changed, pending work) |

### Plugin Documentation

| Document | Path | Description |
|----------|------|-------------|
| Plugin Quickstart | `docs/plugins/GETTING_STARTED.md` | Scaffold your first plugin tutorial |
| Plugin API Reference | `docs/plugins/API_REFERENCE.md` | Full plugin API surface -- lifecycle, settings, storage |
| Bridge Reference | `docs/plugins/BRIDGE_REFERENCE.md` | Root bridge IPC -- 42+29 methods, interceptors |
| Root Environment | `docs/plugins/ROOT_ENVIRONMENT.md` | Root internals -- DOM, CSS variables, Chromium context |
| Plugin Examples | `docs/plugins/EXAMPLES.md` | Annotated example plugins covering common patterns |
| Advanced Plugin Dev | `docs/plugins/ADVANCED_DEVELOPMENT.md` | Advanced plugin patterns and techniques |
| Plugin Roadmap | `docs/PLUGIN_ROADMAP.md` | Planned plugins with architecture notes and implementation strategies |
| Built-in Plugins | `docs/plugins/builtin/INDEX.md` | Per-plugin docs for all shipped plugins (sentry-blocker, themes, settings-panel, link-embeds) |

### Research and Deep Dives

| Document | Path | Description |
|----------|------|-------------|
| Theme Engine Deep Dive | `docs/framework/THEME_ENGINE_DEEP_DIVE.md` | Theme system internals -- ResourceDict injection, CSS vars, live preview |
| Avalonia Patterns | `docs/framework/AVALONIA_PATTERNS.md` | Avalonia reflection patterns, control creation, event handling |
| .NET Runtime | `docs/framework/DOTNET_RUNTIME.md` | .NET 10 runtime constraints, profiler context limitations |
| Root Internals | `docs/research/ROOT_INTERNALS.md` | Root app architecture -- Avalonia, DotNetBrowser, gRPC |
| gRPC Protocol | `docs/research/GRPC_PROTOCOL.md` | Root's gRPC backend protocol analysis |
| gRPC Lib Reference | `docs/research/GRPC_LIB_REFERENCE.md` | gRPC library usage and integration reference |
| Reverse Engineering | `docs/research/REVERSE_ENGINEERING.md` | RE methodology -- decompilation, IL analysis, runtime inspection |
| Security Research | `docs/research/SECURITY_RESEARCH.md` | Security analysis -- trust model, attack surface, mitigations |
| Research Index | `docs/research/RESEARCH_INDEX.md` | Navigation hub for all research documents |
| Planning Reference | `docs/dev/PLANNING_REFERENCE.md` | Project planning, milestones, decision log |
| Contributing (Technical) | `docs/dev/CONTRIBUTING_TECHNICAL.md` | Technical contribution guide -- code patterns, testing, review |

---

*Quick-start reference for Uprooted v0.3.6rc. Last updated 2026-02-18.*
