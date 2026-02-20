# New Session Quick-Start

> **Related docs:** [CLAUDE.md](CLAUDE.md) | [Architecture](docs/framework/ARCHITECTURE.md) | [Index](docs/INDEX.md) | [Session State](hook/SESSION_STATE.md) | [Tasks](TASKS.md) | [Root Control Reference](docs/framework/ROOT_CONTROL_REFERENCE.md)
>
> **AI Workflow Dispatch — jump directly to your task, skip the rest:**
>
> | Task | Start here | Follow with |
> |------|-----------|-------------|
> | Fix theme colors / controls not updating | [ROOT_CONTROL_REFERENCE §Theme System](docs/framework/ROOT_CONTROL_REFERENCE.md#theme-system-mechanics) | [THEME_ENGINE_DEEP_DIVE §Migration](docs/framework/THEME_ENGINE_DEEP_DIVE.md#resource-first-migration-plan) |
> | Build C# hook feature (Avalonia, visual tree) | [HOOK_REFERENCE](docs/framework/HOOK_REFERENCE.md) | [AVALONIA_PATTERNS](docs/framework/AVALONIA_PATTERNS.md) → [ROOT_CONTROL_REFERENCE](docs/framework/ROOT_CONTROL_REFERENCE.md) |
> | Build TypeScript plugin | [TYPESCRIPT_REFERENCE](docs/framework/TYPESCRIPT_REFERENCE.md) | [API_REFERENCE](docs/plugins/API_REFERENCE.md) → [BRIDGE_REFERENCE](docs/plugins/BRIDGE_REFERENCE.md) |
> | Message features (Logger, embeds, NoReplyPing, Translate) | [ROOT_CONTROL_REFERENCE §Message View](docs/framework/ROOT_CONTROL_REFERENCE.md#message-view-internals) | [HOOK_REFERENCE](docs/framework/HOOK_REFERENCE.md) |
> | Theme system (palette, resource keys, 32-key catalog) | [ROOT_THEME_SYSTEM_FINDINGS](research/ROOT_THEME_SYSTEM_FINDINGS.md) | [THEME_ENGINE_DEEP_DIVE](docs/framework/THEME_ENGINE_DEEP_DIVE.md) |
> | CLR profiler / startup / IL injection | [CLR_PROFILER](docs/framework/CLR_PROFILER.md) | [DOTNET_RUNTIME](docs/framework/DOTNET_RUNTIME.md) |
> | gRPC / SilentTyping / network interception | [GRPC_PROTOCOL](docs/research/GRPC_PROTOCOL.md) | [ROOT_INTERNALS §9](docs/research/ROOT_INTERNALS.md#9-grpc-backend) |
> | Root internals (DotNetBrowser, auth, process) | [ROOT_INTERNALS](docs/research/ROOT_INTERNALS.md) | [ROOT_CONTROL_REFERENCE §App Startup](docs/framework/ROOT_CONTROL_REFERENCE.md#app-startup-chain) |
> | Release / version bump | [BUILD](docs/install/BUILD.md) | [CHANGELOG](CHANGELOG.md) |
> | Add / run tests | [TESTING](docs/dev/TESTING.md) | [SESSION_STATE](hook/SESSION_STATE.md) for current counts |

---

## 1. Project Identity

**Uprooted** -- client mod framework for Root Communications desktop app (like Vencord for Discord).
Version: 0.4.2. Target: Root v0.9.92+.
This is the **PRIVATE** repo (`watchthelight/uprooted-private`). Never leak code to the public repo (`watchthelight/uprooted`).
Contributors: `watchthelight` (owner), `agomusio` (admin).

## 2. Architecture One-Liner

Two independent injection layers into one app:
1. **C# .NET hook** (`hook/`) -- CLR profiler IL-injects into Root.exe, adds native Avalonia sidebar/settings UI via reflection.
2. **TypeScript browser injection** (`src/`) -- `<script>` tags in HTML inject into DotNetBrowser Chromium (WebRTC + sub-apps, NOT chat); plugin runtime, theme CSS, bridge proxies.

## 3. Critical Rules -- Never Do These

> Full list with explanations: [ARCHITECTURE.md §9 Critical Rules](docs/framework/ARCHITECTURE.md#9-critical-rules). The 3 most common:

- [ ] **Never use `Type.GetType()` for Avalonia types** -- fails silently in single-file .NET 10. Use `AvaloniaReflection`.
- [ ] **Never modify `ContentControl.Content` directly** -- freezes Avalonia UI permanently. Use Grid overlay pattern.
- [ ] **Never use `System.Text.Json` in hook** -- `MissingMethodException` in profiler context. Use INI-based `UprootedSettings`.

## 4. File Map

### C# Hook Layer (`hook/`)

| File | Lines | Purpose |
|------|------:|---------|
| `Entry.cs` | 37 | Profiler injection entry point, `[ModuleInitializer]` guard |
| `NativeEntry.cs` | 66 | Alternative entry via hostfxr, diagnostic logging |
| `StartupHook.cs` | 577 | Multi-phase startup orchestrator (Phase 0-5, 4.5a-g deferred features, version migration, dev-channel log gate) |
| `HtmlPatchVerifier.cs` | 442 | Phase 0: self-healing HTML patches + FileSystemWatcher |
| `AvaloniaReflection.cs` | ~2383 | Reflection cache for ~80 Avalonia types + ThemeDictionaries access (CRITICAL, largest file) |
| `VisualTreeWalker.cs` | 554 | DFS visual tree traversal, settings layout discovery |
| `SidebarInjector.cs` | ~1729 | LayoutUpdated event (50ms throttle) + timer poll, sidebar injection, header management (structural back button search + collapse pattern + title override), selection suppression, click events, theme walk burst triggers, settings reload on nav click |
| `ContentPages.cs` | ~3602 | Settings page builders (Uprooted, Plugins, Themes); background update notification overlay |
| `ThemeEngine.cs` | ~1280 | Resource-first theme engine v2: ThemeDictionaries override (Root's 32 keys), OKLCH palette generation, live preview, custom ping color, live-recoloring tag walker, custom text color, light theme variant switching |
| `ColorPickerPopup.cs` | 533 | HSV color picker overlay for custom accent/bg |
| `ColorUtils.cs` | ~414 | HSL/RGB/OKLCH conversion, contrast calculation, gamut mapping |
| `UprootedSettings.cs` | 210 | INI-based settings (System.Text.Json workaround) + 10s TTL cache; `LastPackageHash` for hotfix detection |
| `DotNetBrowserReflection.cs` | 1933 | Reflection cache for DotNetBrowser types, IBrowser discovery |
| `BrowserDiscovery.cs` | 496 | Phase 4.5 diagnostic scanner (visual tree + assembly dump) |
| `ClearUrlsEngine.cs` | 482 | ClearURLs: strip tracking params from compose editor URLs on send (AvaloniaEdit routed event interception) |
| `LinkEmbedEngine.cs` | 2409 | Avalonia-native link embed engine (OG/oEmbed fetch + animated image + video embeds + Reddit + visual tree injection) |
| `MessageLogger.cs` | ~2100 | Message logger (WIP): property fix (DeletedAt/EditedAt DateTimeOffset?), author names (SenderMember chain), INPC event-driven detection, self-delete fallback (collection-presence), dedup, 500ms scan; edit detection (dual-strategy EditedAt + grace period); Discord-style deleted/edit indicator rows |
| `MessageStore.cs` | 232 | Flat-file persistence for message log (pipe-delimited, URI-encoded, append-only) |
| `AnimatedImage.cs` | 761 | Animated GIF/WebP decoder + timer playback (SkiaSharp reflection, persistent canvas compositing) |
| `AutoUpdater.cs` | 909 | In-process auto-updater (encrypted .uprpkg download, GitHub releases, HTTP via reflection, version compare, hash-based same-version hotfix detection, `BackgroundUpdateApplied` event) |
| `ProfileBadgeInjector.cs` | 535 | "Uprooted Dev" profile badge injector (event-driven + fallback poll, dev-username gated, tightened IsProfilePopup heuristic) |
| `SilentTypingEngine.cs` | ~90 | Silent typing: DiagnosticListener-based interception — subscribes to .NET HTTP diagnostics, redirects SetTypingIndicator to localhost:0. Replaces 482-line handler injection. |
| `NsfwFilter.cs` | 473 | NSFW content filter (Phase 4.5g, Avalonia-native visual tree scan) |
| `RootcordEngine.cs` | ~2873 | Rootcord plugin: Discord-style vertical server sidebar replacing Root's horizontal tab bar (experimental, live toggle, Apply/Revert lifecycle, tab monitoring, user card popup, community members sidebar swap) |
| `DesktopNotification.cs` | 56 | OS-level toast notifications (PowerShell WinRT on Windows, notify-send on Linux); fires on background auto-update |
| `AuditLogEngine.cs` | ~674 | Audit log viewer: intercepts CommunityLogGrpcService/List HTTP responses, decodes gRPC-web frames + protobuf fields, exposes parsed entries via OnEntry event |
| `PlatformPaths.cs` | 29 | Cross-platform path resolution |
| `Logger.cs` | 92 | Thread-safe file logging, startup separator, dev-channel gate (stable = no log file), runtime Enable/Disable |

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
| `scripts/build-installer.ps1` | Full installer build pipeline (5 steps) |
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

**Source:** [`hook/SESSION_STATE.md`](hook/SESSION_STATE.md) (2026-02-18) — *this section is a static snapshot; SESSION_STATE.md is the live source*

**Versions:** 0.4.2 | Target Root 0.9.92

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
- MessageLogger plugin (WIP): property fix (DeletedAt/EditedAt DateTimeOffset?), INPC event-driven detection, self-delete fallback (collection-presence), author name resolution (SenderMember chain), dedup, 500ms scan, per-type property cache, Discord-style deleted/edit indicators, flat-file persistence (MessageStore.cs). Card injection positioning needs investigation.
- TUI installer mode selector: interactive Install/Uninstall/Repair menu when run without flags
- Linux Root detection: 7-strategy search in bash installer, 5-strategy search in Rust installer (glob, .desktop, /proc, PATH)
- AutoUpdater: encrypted `.uprpkg` package download (single file replaces 6 individual artifacts), multi-layer XOR decryption, developer channel with encrypted PAT, staging + verify + overwrite
- ProfileBadgeInjector: "Uprooted Dev" badge on profile popups for hardcoded developer usernames only (event-driven detection via OverlayLayer CollectionChanged + 500ms fallback poll, 5s startup delay)
- Restart banners: plugins page (state-aware — hides when user reverts), updates section; both with Restart button
- DIAGNOSTICS card: "Open" button opens log file in Explorer
- Custom ping/reply highlight color: standalone override for mention/reply highlight, persists across theme switches. Ping Color toggle merged inline into Custom Theme card (separated by 1px divider). ThemeEngine applies as Phase 6 after theme apply + live updates.
- Silent Typing: C# reimplementation (`SilentTypingEngine.cs`, Phase 4.5f) — DiagnosticListener-based interception: subscribes to .NET's built-in HTTP diagnostics, intercepts SetTypingIndicator requests before they leave the process, redirects to localhost:0. ~90 lines (replaced 482-line HttpClient handler injection). Original DiagnosticListener approach by Kurumi Nanase. TypeScript plugin gutted to no-op stub (v0.2.0)
- Version-gated plugin force-disable on upgrade: `ForceDisableOnUpgrade` dictionary in `StartupHook` runs between Phase 0 and Phase 1, cumulative across skipped versions, downgrade-safe, `CurrentVersion` const replaces hardcoded banner string
- Embed card accent color: link embed left border strip uses active theme accent (`ThemeEngine.GetAccentColor()`). `NotifyThemeChanged()` updates all live cards on theme switch or live drag; preserves site-specific OG colors (Reddit orange, og:theme-color).
- Theme flash fix: walk bursts after injection completes, on ListBox selection changes, and on Uprooted tab switches prevent flash of unthemed content when opening settings or switching tabs. 50ms rapid follow-up added to catch async-loaded controls.
- Plugin names: PascalCase convention (SentryBlocker, LinkEmbeds, MessageLogger, ContentFilter) matching Vencord-style naming
- Reddit link embeds: dedicated handler with old.reddit.com OG fetch, subreddit provider label (e.g. "r/programming"), Reddit orange accent
- LinkEmbedEngine: HTTP status code + Content-Type validation in HttpGetBytes (rejects Cloudflare challenge pages), OG fallback for image-extension URLs that serve HTML (Zipline /view/, /u/), image border corners fix, robust OG regex for meta tags with extra attributes, timeout increased to 10s
- Video embeds (.mp4, .webm, .mov): dark 16:9 placeholder with centered play button overlay, click opens in browser. Detected by extension or `video/*` Content-Type. No HTTP fetch for extension-matched URLs.
- LinkEmbeds "Show file names" toggle: image-only embeds hide filename by default, live toggle via `RefreshTitleVisibility()` in settings lightbox
- Lightbox font sizes scaled up (section headers 18, labels 19, descriptions 16, titles 26, inputs 17, pills 52×26, card width 560)
- Log startup separator: 3 blank lines before first [Entry] log on Root launch; watch-log colors [Entry] green, fallback messages yellow
- Overlay scrollbar: settings pages use `CreateOverlayScrollViewer` — relocates vertical ScrollBar into content Grid column via deferred LayoutUpdated, matching Root's native RootScrollViewer overlay (no content displacement on scroll)
- Themes "Open" button: taller (28px), border stroke, bold text matching toggle switch proportions
- Filter toggle "Enabled" color: hardcoded `#40A050` instead of theme-dependent AccentGreen

**Testing (new 2026-02-18):**
- 170 xUnit tests in `tests/UprootedTests/` — 113 new tests added this session
- New: `ClearUrlsEngineTests` (58), `UprootedSettingsTests` (22), `MessageStoreTests` (18)
- Test stubs in `TestStubs/` eliminate disk I/O; sequential xunit collection prevents static-state interference
- Docker unit sandbox: `tests/Dockerfile.unittest` + `tests/run-docker-tests.sh` (SDK 10.0 + XPlat coverage)
- Linux installer sandbox: `tests/docker-installer/Dockerfile` (Ubuntu 24.04 end-to-end + 14-point verify.sh)
- All 170 pass. Zero bugs found. See `tests/BUG_REPORT.md`.

**Known issues:**
- Theme Engine v2 (resource-first, OKLCH) deployed but not yet validated in production
- NSFW filter needs Avalonia-native redesign (chat is not in DotNetBrowser)
- `after` patch handler defined in interface but not yet invoked by PluginLoader
- MessageLogger (WIP): card injection positioning (`FindMessageGridInContainer` returns null — container structure may have changed); self-delete fallback untested; edit indicators need real-world validation
- ProfileBadgeInjector: `IsProfilePopup` heuristic may false-positive on non-profile popups — needs tree dump log analysis to refine

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
powershell -File scripts/build-installer.ps1
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
| Where is the startup sequence? | `hook/StartupHook.cs` -- Phase 0-5 (4.5a-g for deferred features) |
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
| Where are build scripts? | `scripts/` -- `build.ts`, `build-installer.ps1`, `install-hook.ps1` |
| Where is the hook log? | `%LOCALAPPDATA%/Root Communications/Root/profile/default/uprooted-hook.log` |
| Where is the profiler log? | `%LOCALAPPDATA%/Root/uprooted/profiler.log` |
| Where is session state? | `hook/SESSION_STATE.md` |
| Where are the docs? | `docs/INDEX.md` is the navigation hub |
| Where is the testing guide? | `docs/dev/TESTING.md` — stubs, Docker sandboxes, how to add tests |
| Where are the tests? | `tests/UprootedTests/` — 170 xUnit tests (net10.0, all passing) |

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
| Hook Reference | `docs/framework/HOOK_REFERENCE.md` | C# hook deep dive -- all 28 .cs files |
| TypeScript Reference | `docs/framework/TYPESCRIPT_REFERENCE.md` | Browser injection layer -- plugin runtime, bridge, themes |
| Root Control Reference | `docs/framework/ROOT_CONTROL_REFERENCE.md` | Root Avalonia control hierarchy, theme system mechanics, message view internals, app startup chain |
| CLR Profiler | `docs/framework/CLR_PROFILER.md` | Native C profiler DLL -- IL injection, env vars, attach |
| Installer Reference | `docs/framework/INSTALLER.md` | Console TUI installer -- detection, patching, deployment |
| Build Guide | `docs/install/BUILD.md` | Build pipeline for all layers |
| Installation Guide | `docs/install/INSTALLATION.md` | End-user install/uninstall instructions |
| Roadmap | `docs/ROADMAP.md` | Known issues, planned features, future direction |
| Documentation Index | `docs/INDEX.md` | Navigation hub for all documentation |
| Contributing | `CONTRIBUTING.md` | Branch rules, PR process, code style |
| Testing Guide | `docs/dev/TESTING.md` | Test suites, stubs, Docker sandboxes, how to add tests |
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
| Plugin Contribution Guide | `docs/plugins/CONTRIBUTING_PLUGINS.md` | How to contribute built-in plugins -- conventions, review process |
| Plugin Roadmap | `docs/PLUGIN_ROADMAP.md` | Planned plugins with architecture notes and implementation strategies |
| Built-in Plugins | `docs/plugins/builtin/INDEX.md` | Per-plugin docs for 7 shipped plugins (sentry-blocker, themes, link-embeds, clear-urls, message-logger, silent-typing, content-filter) |

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
| Mitigation Countermeasures | `docs/research/MITIGATION_COUNTERMEASURES.md` | Countermeasures and hardening recommendations from security research |
| Research Index | `docs/research/RESEARCH_INDEX.md` | Navigation hub for all research documents |
| Planning Reference | `docs/dev/PLANNING_REFERENCE.md` | Project planning, milestones, decision log |
| Contributing (Technical) | `docs/dev/CONTRIBUTING_TECHNICAL.md` | Technical contribution guide -- code patterns, testing, review |

---

**Canonical for:** AI session entry point, critical rules quick-reference (§3), common pitfalls (§10), abbreviated file map (§4), workflow dispatch table
**Not canonical for:** full critical rules → [ARCHITECTURE.md §9](docs/framework/ARCHITECTURE.md#9-critical-rules) | full file map → [CLAUDE.md](CLAUDE.md) | implementation detail → [HOOK_REFERENCE.md](docs/framework/HOOK_REFERENCE.md)
*Quick-start reference for Uprooted v0.4.2. Last updated 2026-02-19.*
