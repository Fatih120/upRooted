# New Session Quick-Start

> Rules: [CLAUDE.md](CLAUDE.md) | Live context: [SESSION_STATE.md](hook/SESSION_STATE.md) | Tasks: [TASKS.md](TASKS.md)

## 1. Identity

**Uprooted** v0.5.1-dev7 — client mod framework for Root Communications desktop (v0.9.93+).
Private repo (`The-Uprooted-Project/uprooted-private`). Contributors: `watchthelight` + `agomusio`.

## 2. Architecture

Two independent injection layers into one app:
1. **C# .NET hook** (`hook/`) — CLR profiler IL-injects into Root.exe, adds native Avalonia sidebar/settings UI via reflection.
2. **TypeScript browser injection** (`src/`) — `<script>` tags in HTML inject into DotNetBrowser Chromium (WebRTC + sub-apps, NOT chat); plugin runtime, theme CSS, bridge proxies.

**Chat is Avalonia-native** — 1647+ visual tree nodes, 0 browser controls. DotNetBrowser is auxiliary.

## 3. Task Dispatch Table

Given your task, read **only** these docs (in order). Don't load everything.

| If your task involves... | Read these docs |
|--------------------------|-----------------|
| **Theme colors / controls not updating** | [ROOT_CONTROL_REFERENCE §Theme System](docs/framework/ROOT_CONTROL_REFERENCE.md#theme-system-mechanics) → [THEME_ENGINE_DEEP_DIVE](docs/framework/THEME_ENGINE_DEEP_DIVE.md) → [ROOT_THEME_SYSTEM_FINDINGS](research/ROOT_THEME_SYSTEM_FINDINGS.md) |
| **C# hook feature (Avalonia, visual tree)** | [HOOK_REFERENCE](docs/framework/HOOK_REFERENCE.md) → [AVALONIA_PATTERNS](docs/framework/AVALONIA_PATTERNS.md) → [ROOT_CONTROL_REFERENCE](docs/framework/ROOT_CONTROL_REFERENCE.md) |
| **TypeScript plugin** | [TYPESCRIPT_REFERENCE](docs/framework/TYPESCRIPT_REFERENCE.md) → [API_REFERENCE](docs/plugins/API_REFERENCE.md) → [BRIDGE_REFERENCE](docs/plugins/BRIDGE_REFERENCE.md) |
| **Message features (Logger, embeds, Translate)** | [ROOT_CONTROL_REFERENCE §Message View](docs/framework/ROOT_CONTROL_REFERENCE.md#message-view-internals) → [HOOK_REFERENCE](docs/framework/HOOK_REFERENCE.md) |
| **Theme system (palette, resource keys)** | [ROOT_THEME_SYSTEM_FINDINGS](research/ROOT_THEME_SYSTEM_FINDINGS.md) → [THEME_ENGINE_DEEP_DIVE](docs/framework/THEME_ENGINE_DEEP_DIVE.md) |
| **CLR profiler / startup / IL injection** | [CLR_PROFILER](docs/framework/CLR_PROFILER.md) → [DOTNET_RUNTIME](docs/framework/DOTNET_RUNTIME.md) |
| **gRPC / SilentTyping / network interception** | [GRPC_PROTOCOL](docs/research/GRPC_PROTOCOL.md) → [ROOT_INTERNALS §9](docs/research/ROOT_INTERNALS.md#9-grpc-backend) |
| **Root internals (DotNetBrowser, auth, process)** | [ROOT_INTERNALS](docs/research/ROOT_INTERNALS.md) → [ROOT_CONTROL_REFERENCE](docs/framework/ROOT_CONTROL_REFERENCE.md) |
| **Release / version bump** | [BUILD](docs/install/BUILD.md) → [CHANGELOG](CHANGELOG.md) |
| **Tests** | [TESTING](docs/dev/TESTING.md) |
| **Settings pages / UI cards** | [HOOK_REFERENCE §ContentPages](docs/framework/HOOK_REFERENCE.md) → [AVALONIA_PATTERNS](docs/framework/AVALONIA_PATTERNS.md) |
| **Sidebar injection / nav items** | [HOOK_REFERENCE §SidebarInjector](docs/framework/HOOK_REFERENCE.md) → [ROOT_CONTROL_REFERENCE §Settings Page](docs/framework/ROOT_CONTROL_REFERENCE.md#settings-page-pattern) |
| **Installer / deployment** | [INSTALLER](docs/framework/INSTALLER.md) → [BUILD](docs/install/BUILD.md) |
| **Security / mitigation research** | [SECURITY_RESEARCH](docs/research/SECURITY_RESEARCH.md) → [MITIGATION_COUNTERMEASURES](docs/research/MITIGATION_COUNTERMEASURES.md) |
| **Obfuscation / protected names** | [OBFUSCATION](docs/framework/OBFUSCATION.md) |
| **ILSpy decompilation / Root controls** | [ROOT_CONTROL_REFERENCE](docs/framework/ROOT_CONTROL_REFERENCE.md) → [ILSPY_DUMP_INDEX](research/ILSPY_DUMP_INDEX.md) |

**Always also read** `hook/SESSION_STATE.md` if working on any `hook/` code.

## 4. File Map — Hook Layer (37 .cs files)

| File | Lines | Purpose |
|------|------:|---------|
| `Entry.cs` | 37 | Profiler injection entry point |
| `NativeEntry.cs` | 66 | Alternative entry via hostfxr |
| `StartupHook.cs` | 702 | Multi-phase startup orchestrator (Phase 0-5) |
| `HtmlPatchVerifier.cs` | 460 | Phase 0: self-healing HTML patches |
| `AvaloniaReflection.cs` | 3446 | Reflection cache for ~80 Avalonia types |
| `VisualTreeWalker.cs` | 573 | DFS visual tree traversal |
| `SidebarInjector.cs` | 2048 | Settings page monitor + sidebar injection |
| `ContentPages.cs` | 5086 | Settings page builders (Uprooted, Plugins, Themes) + Dev Console |
| `ThemeEngine.cs` | 3031 | Resource-first theme engine v2, in-place switching, bind-once walker, WeakRef live preview |
| `ColorPickerPopup.cs` | 536 | HSV color picker overlay |
| `ColorUtils.cs` | 414 | HSL/RGB/OKLCH conversion |
| `UprootedSettings.cs` | 277 | INI-based settings + 10s TTL cache |
| `DotNetBrowserReflection.cs` | 1926 | DotNetBrowser type cache, IBrowser discovery |
| `BrowserDiscovery.cs` | 496 | Phase 4.5 diagnostic scanner |
| `ClearUrlsEngine.cs` | 486 | Strip tracking params from URLs on send |
| `LinkEmbedEngine.cs` | 2493 | Avalonia-native link embeds (OG/oEmbed/video) |
| `AnimatedImage.cs` | 761 | Animated GIF/WebP decoder (SkiaSharp) |
| `MessageLogger.cs` | 1707 | Edit/delete detection + visual indicators |
| `MessageStore.cs` | 278 | Flat-file message persistence |
| `AuditLogEngine.cs` | 680 | Audit log viewer (gRPC-web decode) |
| `AutoUpdater.cs` | 1084 | Encrypted .uprpkg auto-updater |
| `DesktopNotification.cs` | 86 | OS-level toast notifications |
| `ProfileBadgeInjector.cs` | 1288 | "Uprooted Dev" profile badge |
| `SilentTypingEngine.cs` | 91 | DiagnosticListener typing block |
| `NsfwFilter.cs` | 484 | Avalonia-native NSFW filter |
| `RootcordEngine.cs` | 5066 | Discord-style vertical sidebar (experimental) |
| `TranslateEngine.cs` | 1994 | Google Translate + DeepL-powered message translation |
| `TranslateConfigPopup.cs` | 555 | Translate config UI |
| `UprootedPresenceBeacon.cs` | 500 | Uprooted user detection via gRPC |
| `WhoReactedEngine.cs` | 577 | Who Reacted: shows reaction authors on messages |
| `UserBioEngine.cs` | 1208 | User Bio: injects bio text + own-profile editor into profile popups |
| `ReconLogger.cs` | 788 | Visual tree + style diagnostic dumper |
| `LogConsole.cs` | ~351 | Dev-only live log terminal via named pipe (Windows) / FIFO (Linux) |
| `Logger.cs` | ~170 | Thread-safe file logging + wide event emission |
| `TailSampler.cs` | ~72 | Tail sampling for high-frequency scan ticks |
| `WideEvent.cs` | ~150 | Structured wide event builder (IDisposable, key=value, dur_ms) |
| `PlatformPaths.cs` | 29 | Cross-platform path resolution |

## 5. File Map — Other Layers

**TypeScript** (`src/`): `core/preload.ts` (entry), `core/pluginLoader.ts` (lifecycle), `api/bridge.ts` (ES6 Proxy), `plugins/` (sentry-blocker, themes, settings-panel, link-embeds, nsfw-filter, silent-typing)

**Installer** (`installer/src-tauri/src/`): `main.rs` (TUI), `patcher.rs` (HTML), `hook.rs` (deploy), `detection.rs`, `settings.rs`, `cli.rs`, `embedded.rs`

**Scripts**: `build-installer.ps1`, `install-hook.ps1`, `pack-update.py`, `diagnose.ps1`, `watch-log.ps1`

**Tests**: `tests/UprootedTests/` — 170 xUnit tests (ColorUtils, ClearUrls, Settings, MessageStore, GradientBrush)

## 6. Where Things Live

| Question | Answer |
|----------|--------|
| C# entry point? | `hook/Entry.cs` (profiler) or `hook/NativeEntry.cs` (hostfxr) |
| TypeScript entry point? | `src/core/preload.ts` |
| Startup sequence? | `hook/StartupHook.cs` — Phase 0-5 |
| Avalonia reflection? | `hook/AvaloniaReflection.cs` |
| Settings pages? | `hook/ContentPages.cs` |
| Theme engine? | `hook/ThemeEngine.cs` (C#/Avalonia) or `src/plugins/themes/` (CSS) |
| Bridge proxy? | `src/api/bridge.ts` |
| Settings stored? | `uprooted-settings.ini` (C#) or `uprooted-settings.json` (TS) |
| Hook log? | `%LOCALAPPDATA%/Root Communications/Root/profile/default/uprooted-hook.log` |
| Profiler log? | `%LOCALAPPDATA%/Root/uprooted/profiler.log` |
| Docs hub? | `docs/INDEX.md` |
| Tests? | `tests/UprootedTests/` |

## 7. Startup Phases

| Phase | File | Purpose |
|-------|------|---------|
| 0 | HtmlPatchVerifier | Self-heal HTML patches + FileSystemWatcher |
| 0→1 | StartupHook | Version migration: force-disable unstable plugins |
| 1 | StartupHook | Wait for Avalonia assemblies (30s) |
| 2 | StartupHook | Wait for Application.Current (30s) |
| 3 | StartupHook | Wait for MainWindow (60s) |
| 3.5 | ThemeEngine | Initialize + apply saved theme |
| 4 | SidebarInjector | Start settings page monitor |
| 4.5 | BrowserDiscovery | Diagnostic visual tree + assembly scan |
| 4.5a | ClearUrlsEngine | Strip tracking params (14s delay) |
| 4.5b | LinkEmbedEngine | Avalonia-native link embeds |
| 4.5c | MessageLogger | Message edit/delete detection |
| 4.5d | AutoUpdater | Background update check (1 min interval) |
| 4.5e | ProfileBadgeInjector | Dev badge on profile popup (5s delay) |
| 4.5f | SilentTypingEngine | DiagnosticListener interception (12s delay) |
| 4.5g | NsfwFilter | Avalonia-native content filter (20s delay) |
| 4.5h | RootcordEngine | Discord-style sidebar (8s delay) |
| 4.5i | TranslateEngine | DeepL translation |
| 4.5j | UprootedPresenceBeacon | Uprooted user detection |
| 5 | StartupHook | DotNetBrowser assembly detection |

---

*Quick-start reference for Uprooted v0.5.1-dev7. Last updated 2026-02-23.*
