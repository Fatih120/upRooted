# Codebase Structure

**Analysis Date:** 2026-02-17

## Directory Layout

```
uprooted-private/
├── .github/                    # GitHub Actions workflows and issue templates
├── .planning/                  # GSD planning documents (generated)
├── .claude/                    # Claude Code configuration and skills
│   ├── commands/               # Custom /commands
│   ├── agents/                 # Agent configurations
│   └── skills/                 # Skill definitions
├── docs/                       # User and developer documentation
│   ├── INDEX.md                # Documentation navigation hub
│   ├── ARCHITECTURE.md         # Architecture reference
│   ├── HOW_IT_WORKS.md         # Reverse engineering walkthrough
│   ├── ROADMAP.md              # Roadmap and known issues
│   ├── archives/               # Miscellaneous notes and one-off fixes
│   ├── dev/                    # Developer environment docs
│   ├── framework/              # Framework internals (Hook, TypeScript, Profiler, Installer)
│   ├── install/                # Installation and build guides
│   ├── plugins/                # Plugin development guide
│   │   └── builtin/            # Built-in plugin documentation
│   └── research/               # Reverse engineering notes
├── hook/                       # .NET hook for Avalonia UI injection (sidebar, settings pages, themes)
│   ├── *.cs                    # C# source files for UI injection and theme engine
│   └── UprootedHook.csproj     # .NET project file
├── hook-test/                  # Tests for .NET hook
│   ├── testapp/                # Test application
│   └── UprootedHook/           # Hook test assembly
├── installer/                  # Desktop installer application (Tauri + TypeScript)
│   ├── src/                    # TypeScript frontend
│   │   ├── main.ts             # Entry point, router, titlebar setup
│   │   ├── starfield.ts        # Starfield animation component
│   │   ├── lib/                # Tauri bridge and state management
│   │   │   ├── tauri.ts        # Tauri command wrappers (detect, install, settings, themes)
│   │   │   └── state.ts        # Local state management
│   │   ├── pages/              # Page controllers
│   │   │   ├── main.ts         # Installation/uninstallation UI
│   │   │   └── themes.ts       # Theme preview and selection
│   │   └── styles/             # Page stylesheets
│   ├── src-tauri/              # Rust/Tauri backend
│   │   ├── src/                # Rust source (detection, patcher, hook deployment)
│   │   ├── tauri.conf.json     # Tauri app configuration
│   │   ├── capabilities/       # Tauri permission scopes
│   │   └── artifacts/          # Staging directory for build artifacts
│   ├── package.json
│   └── vite.config.ts          # Vite configuration
├── legacy/                     # Deprecated/archived code
├── packaging/                  # Packaging and distribution scripts
├── research/                   # Reverse engineering workspaces and analysis
├── site/                       # Marketing website (Astro)
│   ├── src/
│   │   ├── layouts/
│   │   └── pages/
│   └── package.json
├── scripts/                    # Build and CLI scripts
│   ├── build.ts                # esbuild bundler for preload + CSS
│   ├── install.ts              # Installation CLI entry point
│   ├── uninstall.ts            # Uninstallation CLI entry point
│   ├── patch_binary.py         # Binary patching utility for Windows
│   ├── install-hook.ps1        # PowerShell hook installation
│   ├── build-installer.ps1     # Full installer build pipeline
│   └── *.py, *.ps1             # Utilities and diagnostics
├── src/                        # Main Uprooted framework (browser-side)
│   ├── api/                    # Public plugin API
│   │   ├── index.ts            # Barrel export for @uprooted/api
│   │   ├── bridge.ts           # ES6 Proxy wrapper for native bridge
│   │   ├── css.ts              # CSS injection/removal utilities
│   │   ├── dom.ts              # DOM helpers (waitForElement, observe, nextFrame)
│   │   └── native.ts           # Native layer helpers (CSS variables, theme)
│   ├── core/                   # Core runtime
│   │   ├── preload.ts          # Main entry point (injected into browser)
│   │   ├── pluginLoader.ts     # Plugin lifecycle and event routing
│   │   ├── settings.ts         # Settings file I/O (Node.js CLI only)
│   │   └── patcher.ts          # HTML file injection/patching
│   ├── plugins/                # Built-in plugins
│   │   ├── sentry-blocker/     # Privacy plugin: blocks Sentry telemetry
│   │   │   └── index.ts
│   │   ├── themes/             # Theme engine plugin
│   │   │   ├── index.ts        # Plugin definition + color math
│   │   │   ├── themes.json     # Theme definitions
│   │   │   └── forest-green.css # Theme CSS, loaded at build time
│   │   ├── link-embeds/        # Link preview rendering plugin
│   │   │   ├── index.ts        # Plugin definition and lifecycle
│   │   │   ├── embeds.ts       # Embed card rendering
│   │   │   ├── providers.ts    # OG metadata fetching (YouTube, generic sites)
│   │   │   └── style.css       # Embed styles
│   │   └── settings-panel/     # Settings UI plugin
│   │       ├── index.ts        # Plugin entry, lifecycle
│   │       ├── panel.ts        # DOM injection and observation
│   │       ├── components.ts   # UI component helpers
│   │       └── panel.css       # Panel styles
│   └── types/                  # TypeScript type definitions
│       ├── bridge.ts           # INativeToWebRtc, IWebRtcToNative interfaces
│       ├── plugin.ts           # UprootedPlugin, Patch interfaces
│       ├── settings.ts         # UprootedSettings, PluginSettings
│       └── root.ts             # Root-specific types (UserGuid, DeviceGuid, themes)
├── tests/                      # C# unit tests (ColorUtils, GradientBrush)
│   └── UprootedTests/
├── tools/                      # Development tools and native binaries
│   ├── uprooted_profiler.c     # CLR profiler source (Windows)
│   ├── uprooted_profiler_linux.c # CLR profiler source (Linux)
│   ├── uprooted_profiler.dll   # Compiled profiler (Windows)
│   ├── uprooted_profiler.def   # Profiler export definitions
│   └── (other build artifacts and utilities)
├── dist/                       # Prebuilt TypeScript bundle (generated by `pnpm build`)
│   ├── uprooted-preload.js     # IIFE bundle injected via <script> tag
│   ├── uprooted-preload.js.map # Source map
│   └── uprooted.css            # Combined CSS from all plugins
├── .gitignore
├── package.json                # Root workspace package.json
├── pnpm-workspace.yaml         # pnpm monorepo definition
├── pnpm-lock.yaml              # Dependency lockfile
├── tsconfig.json               # Root TypeScript config
├── tsconfig.build.json         # Build-specific TypeScript config
├── uprooted.sln                # Visual Studio solution (hook + tests)
├── CLAUDE.md                   # AI contributor guidance
├── CONTRIBUTING.md             # Contribution guidelines
├── README.md                   # Repository landing page
├── LICENSE                     # Custom license
├── CHANGELOG.md                # Internal changelog
├── CHANGELOG_PUBLIC.md         # Public changelog (mirrors GitHub release notes)
├── TASKS.md                    # Active task board
├── Install-Uprooted.ps1        # PowerShell one-click installer (Windows)
├── Uninstall-Uprooted.ps1      # PowerShell uninstaller (Windows)
├── install-uprooted-linux.sh   # Bash installer for Linux
└── uninstall-uprooted-linux.sh # Bash uninstaller for Linux
```

## Directory Purposes

**src/api:**
- Purpose: Public plugin author API (stable interface)
- Contains: Browser-side utilities for DOM, CSS, bridge interception, native integration
- Key files: `src/api/index.ts` (barrel export), `src/api/bridge.ts` (proxy implementation)

**src/core:**
- Purpose: Runtime bootstrap and plugin management
- Contains: Preload entry point, plugin loader, HTML injection, settings persistence
- Key files: `src/core/preload.ts` (browser entry), `src/core/pluginLoader.ts` (lifecycle)

**src/plugins:**
- Purpose: Built-in plugins providing core functionality
- Contains: Sentry blocking, theme system, link embeds, settings UI
- Key files: `src/plugins/themes/index.ts` (color math + CSS variables), `src/plugins/sentry-blocker/index.ts` (network blocking), `src/plugins/link-embeds/index.ts` (embed rendering), `src/plugins/settings-panel/index.ts` (DOM injection)

**src/types:**
- Purpose: Centralized TypeScript interfaces for type safety
- Contains: Bridge method signatures, plugin definitions, settings schema
- Key files: `src/types/bridge.ts` (native↔WebRTC contracts), `src/types/plugin.ts` (plugin API)

**scripts:**
- Purpose: Build, installation, and utility scripts
- Contains: esbuild bundler, HTML patcher, install/uninstall CLIs, installer build pipeline
- Key files: `scripts/build.ts` (bundles preload + CSS), `scripts/install.ts` (CLI installer), `scripts/build-installer.ps1` (full installer build pipeline)

**installer/src:**
- Purpose: Desktop application frontend for installation and configuration
- Contains: Page routing, Tauri API wrappers, theme management UI
- Key files: `installer/src/main.ts` (app bootstrap), `installer/src/lib/tauri.ts` (command wrappers)

**installer/src-tauri:**
- Purpose: Rust backend for desktop application
- Contains: File system operations, settings persistence, theme loading, HTML patching
- Technology: Tauri 2.x framework

**hook:**
- Purpose: .NET runtime hook for Avalonia UI injection, theme engine, and feature detection
- Contains: C# source for sidebar injection, settings pages, themes, link embeds, DotNetBrowser integration
- Technology: .NET 10.0 assembly with reflection-based architecture

**site:**
- Purpose: Marketing and documentation website
- Contains: Static site built with Astro
- Key files: `site/src/pages/index.astro` (homepage)

**docs:**
- Purpose: Complete technical documentation for users and developers
- Contains: Architecture reference, plugin development guide, research notes
- Key subdirectories: `framework/` (internals), `plugins/` (development), `install/` (build/deployment), `dev/` (contributor guides)

## Key File Locations

**Entry Points:**
- `src/core/preload.ts`: Browser runtime entry point (executed in Root's Chromium context)
- `hook/Entry.cs`: C# profiler injection entry point
- `hook/StartupHook.cs`: C# initialization orchestrator (multi-phase startup)
- `scripts/install.ts`: CLI installation entry point (Node.js)
- `scripts/uninstall.ts`: CLI uninstallation entry point (Node.js)
- `installer/src/main.ts`: Desktop app entry point (Tauri frontend)

**Configuration:**
- `tsconfig.json`: TypeScript compiler options, path aliases (@uprooted/*)
- `pnpm-workspace.yaml`: Monorepo workspace definition
- `installer/tauri.conf.json`: Tauri application configuration
- `src/types/settings.ts`: Settings schema and defaults
- `hook/UprootedHook.csproj`: .NET project configuration

**Core Logic:**
- `src/core/pluginLoader.ts`: Plugin lifecycle management and event routing
- `src/api/bridge.ts`: ES6 Proxy implementation for bridge interception
- `src/core/patcher.ts`: HTML file injection logic
- `src/plugins/themes/index.ts`: Theme application + color math
- `src/plugins/link-embeds/index.ts`: Link preview rendering with OG metadata
- `hook/SidebarInjector.cs`: Settings page monitoring and injection (200ms timer)
- `hook/AvaloniaReflection.cs`: Reflection cache for Avalonia types (1943 lines)
- `hook/ThemeEngine.cs`: Native theme engine with resource override (2218 lines)
- `hook/LinkEmbedEngine.cs`: Avalonia-native link embed rendering (1260 lines)
- `hook/HtmlPatchVerifier.cs`: Self-healing HTML patches with FileSystemWatcher

**Type Definitions:**
- `src/types/bridge.ts`: INativeToWebRtc, IWebRtcToNative method signatures
- `src/types/plugin.ts`: UprootedPlugin interface, Patch definition
- `src/types/settings.ts`: UprootedSettings, PluginSettings, DEFAULT_SETTINGS

**Testing:**
- `tests/UprootedTests/`: C# unit tests for hook utilities (ColorUtils, GradientBrush)
- `hook-test/`: Integration test harness for the .NET hook

## Naming Conventions

**Files:**
- Plugin files: `src/plugins/{plugin-name}/index.ts` (default export is the plugin)
- Plugin helpers: `src/plugins/{plugin-name}/{helper}.ts` for internal utilities
- CSS files: `*.css` co-located with their plugin (e.g., `src/plugins/themes/forest-green.css`)
- Types: `*.ts` in `src/types/` for reusable types
- Scripts: `scripts/{action}.ts` for build/install/uninstall actions
- Pages: `installer/src/pages/{name}.ts` for installer pages
- Hook files: `hook/{Class}.cs` where class name matches file name (PascalCase)

**Directories:**
- Plugin directories: lowercase with hyphens (sentry-blocker, settings-panel, link-embeds)
- API modules: lowercase single-word names (api, core, types, plugins)
- Feature directories: by feature name (themes, sentry-blocker, link-embeds)
- Documentation: uppercase with hyphens for docs (ARCHITECTURE.md, HOOK_REFERENCE.md)

**Functions & Variables:**
- Utility functions: camelCase (injectCss, setCssVariable, waitForElement)
- Constants: UPPER_SNAKE_CASE (PROFILE_DIR, INJECTION_MARKER, DEFAULT_SETTINGS)
- Types: PascalCase (UprootedPlugin, BridgeEvent, PluginSettings)
- Component classes: PascalCase (PluginLoader)
- C# private fields: `_camelCase`; static fields: `s_camelCase`

**Exports:**
- Barrel exports in index.ts: `export * as module from "./file.js"`
- Plugin default exports: `export default {...satisfies UprootedPlugin}`
- Public API: all exports from `src/api/index.ts`

## Where to Add New Code

**New Built-in Plugin:**
1. Create directory: `src/plugins/{plugin-name}/`
2. Create entry: `src/plugins/{plugin-name}/index.ts` with default export satisfying `UprootedPlugin`
3. Define patches in plugin's `patches` array (if bridge interception needed)
4. If CSS needed: `src/plugins/{plugin-name}/{name}.css` (auto-collected at build time)
5. Add helper modules if needed: `src/plugins/{plugin-name}/{helper}.ts`
6. Register in preload: Add `loader.register(yourPlugin)` in `src/core/preload.ts` before `loader.startAll()`

**New Utility/Helper:**
- Shared utilities: `src/api/` (if part of public API) or `src/core/` (if internal only)
- Plugin-specific helpers: within plugin directory (e.g., `src/plugins/{name}/{helper}.ts`)

**New Type Definition:**
- Reusable types: `src/types/{domain}.ts` (e.g., `bridge.ts`, `plugin.ts`, `settings.ts`)
- Plugin-specific types: within plugin directory or in plugin's index.ts

**New Settings Field:**
- Update `UprootedSettings` interface in `src/types/settings.ts`
- Update `DEFAULT_SETTINGS` in same file
- If plugin-specific: define schema in plugin's `settings` field

**New Bridge Interception:**
1. Import `INativeToWebRtc` or `IWebRtcToNative` from `src/types/bridge.ts`
2. Create plugin with patch targeting the method
3. Patch fires before original method (or replaces it entirely)

**New Installer Page:**
1. Create `installer/src/pages/{name}.ts` with `export async function init(el: HTMLElement): Promise<void>`
2. Page HTML already rendered; controller finds and populates it
3. Register in `pageInits` map in `installer/src/main.ts`

**New C# Hook Feature:**
1. Create file in `hook/` directory with PascalCase name matching class
2. Implement feature using reflection via `AvaloniaReflection`
3. Never use `Type.GetType()` for Avalonia types — always use the reflection cache
4. Integrate into `StartupHook.cs` at appropriate phase (Phase 0-5)
5. For UI features: dispatch to UI thread via `RunOnUIThread()`

## Special Directories

**dist/:**
- Purpose: Build output
- Generated: Yes (by `pnpm build`)
- Committed: No (in .gitignore)
- Contents: `uprooted-preload.js` (IIFE bundle) and `uprooted.css` (combined plugin CSS)

**installer/src-tauri/artifacts/:**
- Purpose: Staging directory for build artifacts
- Generated: Yes (by `scripts/build-installer.ps1`)
- Contents: `uprooted_profiler.dll`, `UprootedHook.dll`, `UprootedHook.deps.json`, `uprooted-preload.js`, `uprooted.css`

**node_modules/:**
- Purpose: Installed dependencies
- Generated: Yes (by pnpm)
- Committed: No (in .gitignore)

**.planning/codebase/:**
- Purpose: GSD codebase analysis documents
- Generated: Yes (by /gsd:map-codebase)
- Committed: Yes (tracked in git)
- Contents: ARCHITECTURE.md, STRUCTURE.md, CONVENTIONS.md, TESTING.md, STACK.md, INTEGRATIONS.md, CONCERNS.md

**hook-test/:**
- Purpose: Test harness for .NET hook
- Built with: .NET
- Run via: PowerShell scripts

**research/:**
- Purpose: Development and analysis workspaces
- Contents: Pentesting notes, source analysis, dev scripts

**legacy/:**
- Purpose: Archived code (superseded binary patchers)
- Status: Not actively maintained

**docs/archives/:**
- Purpose: Miscellaneous notes and one-off fixes
- Contents: Historical context and ad-hoc documentation

## Monorepo Structure

This is a pnpm workspace with three packages:

1. **root package** (`./package.json`): Shared scripts and workspaces definition
   - Scripts: `build` (builds src/), `dev`, `install-root`, `uninstall-root`, `site:build`, `site:dev`, `installer:dev`, `installer:build`
   - Workspaces: `site/` (other packages are standalone)

2. **site** (`./site/package.json`): Marketing website
   - Type: Astro static site
   - Scripts: `dev`, `build`

3. **installer** (`./installer/src-tauri/`): Tauri desktop application (standalone Rust/Tauri, NOT in pnpm workspace)
   - Type: Rust console TUI + TypeScript frontend
   - Scripts: `dev` (Vite dev server), `build` (TypeScript + Vite), `tauri` (Tauri CLI)

Run pnpm commands from root; package filtering with `--filter` (e.g., `pnpm --filter site build`)

The installer is NOT part of the pnpm workspace because it has its own Cargo/Rust dependencies.

## Installer Build Pipeline

`scripts/build-installer.ps1` orchestrates the full installer build in 5 steps:

1. `pnpm build` — TypeScript layer -> `dist/uprooted-preload.js` + `dist/uprooted.css`
2. `dotnet build hook/UprootedHook.csproj -c Release` — C# hook -> `UprootedHook.dll`
3. `cl.exe` via VS Build Tools — Profiler C source -> `uprooted_profiler.dll`
4. Stage all 5 files to `installer/src-tauri/artifacts/`
5. `pnpm tauri build` — Tauri app -> `Uprooted Installer.exe`

The resulting portable executable embeds all 5 binary artifacts and deploys them to `%LOCALAPPDATA%\Root\uprooted\` on install.

## Configuration Files

**tsconfig.json:**
- Target: ES2022
- Module: ES2022
- Path aliases: `@uprooted/*` → `src/*`
- Strict mode enabled
- Source maps included for debugging

**tsconfig.build.json:**
- Subset for build scripts (if special rules needed)

**pnpm-workspace.yaml:**
- Defines: root, site as workspace packages (installer is separate Rust project)

**pnpm-lock.yaml:**
- Lockfile for reproducible installs
- Committed to git

**hook/UprootedHook.csproj:**
- Target: `.NET 10.0`
- Nullable: enabled
- Implicit usings: enabled
- No external assembly dependencies (reflection-only)

---

*Structure analysis: 2026-02-17*
