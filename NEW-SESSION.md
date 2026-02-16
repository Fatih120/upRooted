# New Session Quick-Start

> **Related docs:** [CLAUDE.md](CLAUDE.md) | [Architecture](docs/ARCHITECTURE.md) | [Index](docs/INDEX.md) | [Session State](hook/SESSION_STATE.md)

---

## 1. Project Identity

**Uprooted** -- client mod framework for Root Communications desktop app (like Vencord for Discord).
Version: 0.2.1 (package.json) / 0.1.1 (hook display). Target: Root v0.9.86+.
This is the **PRIVATE** repo (`watchthelight/uprooted-private`). Never leak code to the public repo (`watchthelight/uprooted`).
Contributors: `watchthelight` (owner), `agomusio` (admin).

## 2. Architecture One-Liner

Two independent injection layers into one app:
1. **C# .NET hook** (`hook/`) -- CLR profiler IL-injects into Root.exe, adds native Avalonia sidebar/settings UI via reflection.
2. **TypeScript browser injection** (`src/`) -- `<script>` tags in HTML inject into DotNetBrowser Chromium; plugin runtime, theme CSS, bridge proxies.

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
| `StartupHook.cs` | 179 | 5-phase Avalonia wait + initialization orchestrator |
| `HtmlPatchVerifier.cs` | 344 | Phase 0: self-healing HTML patches + FileSystemWatcher |
| `AvaloniaReflection.cs` | 1943 | Reflection cache for ~80 Avalonia types (CRITICAL, largest file) |
| `VisualTreeWalker.cs` | 554 | DFS visual tree traversal, settings layout discovery |
| `SidebarInjector.cs` | 1088 | 200ms timer poll, sidebar injection, click events |
| `ContentPages.cs` | 1753 | Settings page builders (Uprooted, Plugins, Themes) |
| `ThemeEngine.cs` | 2218 | ResourceDictionary overrides, live theme preview (2nd largest) |
| `ColorPickerPopup.cs` | 533 | HSV color picker overlay for custom accent/bg |
| `ColorUtils.cs` | 262 | HSL/RGB conversion, contrast calculation |
| `UprootedSettings.cs` | 91 | INI-based settings (System.Text.Json workaround) |
| `PlatformPaths.cs` | 29 | Cross-platform path resolution |
| `Logger.cs` | 28 | Thread-safe file logging, swallows own exceptions |

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
| `types/plugin.ts` | `UprootedPlugin`, `Patch`, `SettingField` interfaces |
| `types/bridge.ts` | `INativeToWebRtc` (42 methods), `IWebRtcToNative` (29 methods) |

### Other Key Files

| File | Purpose |
|------|---------|
| `tools/uprooted_profiler.c` | CLR profiler DLL source (Windows) |
| `installer/src-tauri/src/patcher.rs` | Rust: HTML patch install/uninstall/repair |
| `installer/src-tauri/src/hook.rs` | Rust: file deployment + env var management |
| `scripts/build_installer.ps1` | Full installer build pipeline (5 steps) |
| `scripts/build.ts` | esbuild bundler: `src/` -> `dist/` (IIFE + CSS) |
| `Install-Uprooted.ps1` | PowerShell one-click installer |

## 5. Reading Order

| Order | Document | Why |
|------:|----------|-----|
| 1 | `NEW-SESSION.md` (this file) | Orientation, rules, file map |
| 2 | `docs/ARCHITECTURE.md` | Layer boundaries, data flow, all constraints |
| 3 | `hook/SESSION_STATE.md` | What changed last, pending issues, build commands |
| 4 | `docs/HOOK_REFERENCE.md` | C# hook deep dive (1417 lines, all 14 .cs files) |
| 5 | `docs/HOW_IT_WORKS.md` | Narrative walkthrough from reverse engineering to running mod |

## 6. Current State

**Source:** `hook/SESSION_STATE.md` (2026-02-14)

**Versions:** package.json 0.2.1 | hook display "0.1.1" | Target Root 0.9.87

**Recent work (all working):**
- Fixed infinite inject/detach loop -- replaced broken `VisualRoot` property check with `FindFirstTextBlock("APP SETTINGS")`
- Fixed chatty TreeWalker logging -- silent text block search instead of full `FindSettingsLayout`
- Replaced crashing `DumpDiagnostics` with focused `DumpVersionRecon`
- Added "Uprooted 0.1.1" version text in grey version box (matches native style)
- Cosmetic sidebar fixes -- margins, font weight, hover highlights now match native items

**Known issues:**
- Style verification pending -- user has not confirmed latest cosmetic fixes visually
- C# settings persistence uses INI (System.Text.Json broken in profiler context)
- Plugin toggles on Plugins page are display-only (cannot enable/disable at runtime)
- `after` patch handler defined in interface but not yet invoked by PluginLoader

## 7. Build Commands

```bash
# TypeScript bundle (src/ -> dist/)
pnpm build

# C# hook (hook/ -> hook/bin/Release/net10.0/UprootedHook.dll)
dotnet build hook/ -c Release

# C# tests
dotnet test tests/

# Tauri installer
cd installer && cargo tauri build

# Full installer with all embedded artifacts
powershell -File scripts/build_installer.ps1
```

## 8. Where Things Live

| Question | Answer |
|----------|--------|
| Where is the C# entry point? | `hook/Entry.cs` (profiler) or `hook/NativeEntry.cs` (hostfxr) |
| Where is the TypeScript entry point? | `src/core/preload.ts` |
| Where is the startup sequence? | `hook/StartupHook.cs` -- 5 phases |
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
| Architecture | `docs/ARCHITECTURE.md` | System design, layer boundaries, data flow, critical rules |
| How It Works | `docs/HOW_IT_WORKS.md` | Narrative technical walkthrough from RE to running mod |
| Hook Reference | `docs/HOOK_REFERENCE.md` | C# hook deep dive -- all 14 .cs files (1417 lines) |
| TypeScript Reference | `docs/TYPESCRIPT_REFERENCE.md` | Browser injection layer -- plugin runtime, bridge, themes |
| CLR Profiler | `docs/CLR_PROFILER.md` | Native C profiler DLL -- IL injection, env vars, attach |
| Installer Reference | `docs/INSTALLER.md` | Tauri/Rust installer -- detection, patching, deployment |
| Build Guide | `docs/BUILD.md` | Build pipeline for all layers |
| Installation Guide | `docs/INSTALLATION.md` | End-user install/uninstall instructions |
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

### Research and Deep Dives

| Document | Path | Description |
|----------|------|-------------|
| Theme Engine Deep Dive | `docs/THEME_ENGINE_DEEP_DIVE.md` | Theme system internals -- ResourceDict injection, CSS vars, live preview |
| Avalonia Patterns | `docs/AVALONIA_PATTERNS.md` | Avalonia reflection patterns, control creation, event handling |
| .NET Runtime | `docs/DOTNET_RUNTIME.md` | .NET 10 runtime constraints, profiler context limitations |
| Root Internals | `docs/ROOT_INTERNALS.md` | Root app architecture -- Avalonia, DotNetBrowser, gRPC |
| gRPC Protocol | `docs/GRPC_PROTOCOL.md` | Root's gRPC backend protocol analysis |
| gRPC Lib Reference | `docs/GRPC_LIB_REFERENCE.md` | gRPC library usage and integration reference |
| Reverse Engineering | `docs/REVERSE_ENGINEERING.md` | RE methodology -- decompilation, IL analysis, runtime inspection |
| Security Research | `docs/SECURITY_RESEARCH.md` | Security analysis -- trust model, attack surface, mitigations |
| Research Index | `docs/RESEARCH_INDEX.md` | Navigation hub for all research documents |
| Planning Reference | `docs/PLANNING_REFERENCE.md` | Project planning, milestones, decision log |
| Contributing (Technical) | `docs/CONTRIBUTING_TECHNICAL.md` | Technical contribution guide -- code patterns, testing, review |

---

*Quick-start reference for Uprooted v0.2.1. Last updated 2026-02-16.*
