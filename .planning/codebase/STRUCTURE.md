# Codebase Structure

**Analysis Date:** 2026-02-16

## Directory Layout

```
uprooted-private/
├── .github/                    # GitHub Actions workflows and issue templates
├── .planning/                  # GSD planning documents (generated)
├── docs/                       # User and developer documentation
│   └── plugins/                # Plugin development guide
├── hook/                       # .NET hook for Avalonia UI injection (sidebar, settings pages, themes, version display)
│   ├── *.cs                    # C# source files for UI injection and theme engine
│   └── UprootedHook.csproj     # .NET project file
├── hook-test/                  # Tests for .NET hook
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
│   │   ├── src/                # Rust source
│   │   ├── tauri.conf.json     # Tauri app configuration
│   │   └── capabilities/       # Tauri permission scopes
│   ├── package.json
│   └── vite.config.ts          # Vite configuration
├── legacy/                     # Deprecated/archived code
├── packaging/                  # Packaging and distribution scripts
├── research/                   # Research and analysis workspaces
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
│   │   └── settings-panel/     # Settings UI plugin
│   │       ├── index.ts        # Plugin entry, lifecycle
│   │       ├── panel.ts        # DOM injection and observation
│   │       └── components.ts   # UI component helpers
│   └── types/                  # TypeScript type definitions
│       ├── bridge.ts           # INativeToWebRtc, IWebRtcToNative interfaces
│       ├── plugin.ts           # UprootedPlugin, Patch interfaces
│       ├── settings.ts         # UprootedSettings, PluginSettings
│       └── root.ts             # Root-specific types (UserGuid, DeviceGuid, themes)
├── tests/                      # C# unit tests (ColorUtils, GradientBrush)
├── tools/                      # Development tools
├── .gitignore
├── package.json                # Root workspace package.json
├── pnpm-workspace.yaml         # pnpm monorepo definition
├── pnpm-lock.yaml              # Dependency lockfile
├── tsconfig.json               # Root TypeScript config
├── tsconfig.build.json         # Build-specific TypeScript config
├── README.md
├── LICENSE
├── CONTRIBUTING.md
└── *.ps1, *.sh                 # Installation/uninstallation scripts for Windows/Linux
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
- Contains: Sentry blocking, theme system, settings UI
- Key files: `src/plugins/themes/index.ts` (color math + CSS variables), `src/plugins/sentry-blocker/index.ts` (network blocking)

**src/types:**
- Purpose: Centralized TypeScript interfaces for type safety
- Contains: Bridge method signatures, plugin definitions, settings schema
- Key files: `src/types/bridge.ts` (native↔WebRTC contracts), `src/types/plugin.ts` (plugin API)

**scripts:**
- Purpose: Build, installation, and utility scripts
- Contains: esbuild bundler, HTML patcher, install/uninstall CLIs, installer build pipeline
- Key files: `scripts/build.ts` (bundles preload + CSS), `scripts/install.ts` (CLI installer), `scripts/build_installer.ps1` (full installer build pipeline)

**installer/src:**
- Purpose: Desktop application frontend for installation and configuration
- Contains: Page routing, Tauri API wrappers, theme management UI
- Key files: `installer/src/main.ts` (app bootstrap), `installer/src/lib/tauri.ts` (command wrappers)

**installer/src-tauri:**
- Purpose: Rust backend for desktop application
- Contains: File system operations, settings persistence, theme loading
- Technology: Tauri 2.x framework

**hook:**
- Purpose: .NET runtime hook for Avalonia theme injection (optional enhancement)
- Contains: C# source for theme engine and UI injection
- Technology: .NET assembly hooking

**site:**
- Purpose: Marketing and documentation website
- Contains: Static site built with Astro
- Key files: `site/src/pages/index.astro` (homepage)

## Key File Locations

**Entry Points:**
- `src/core/preload.ts`: Browser runtime entry point (executed in Root's Chromium context)
- `scripts/install.ts`: CLI installation entry point (Node.js)
- `scripts/uninstall.ts`: CLI uninstallation entry point (Node.js)
- `installer/src/main.ts`: Desktop app entry point (Tauri frontend)

**Configuration:**
- `tsconfig.json`: TypeScript compiler options, path aliases (@uprooted/*)
- `pnpm-workspace.yaml`: Monorepo workspace definition
- `installer/tauri.conf.json`: Tauri application configuration
- `src/types/settings.ts`: Settings schema and defaults

**Core Logic:**
- `src/core/pluginLoader.ts`: Plugin lifecycle management and event routing
- `src/api/bridge.ts`: ES6 Proxy implementation for bridge interception
- `src/core/patcher.ts`: HTML file injection logic
- `src/plugins/themes/index.ts`: Theme application + color math

**Type Definitions:**
- `src/types/bridge.ts`: INativeToWebRtc, IWebRtcToNative method signatures
- `src/types/plugin.ts`: UprootedPlugin interface, Patch definition
- `src/types/settings.ts`: UprootedSettings, PluginSettings, DEFAULT_SETTINGS

**Testing:**
- `tests/`: C# unit tests for hook utilities (ColorUtils, GradientBrush). No TypeScript tests exist.

## Naming Conventions

**Files:**
- Plugin files: `src/plugins/{plugin-name}/index.ts` (default export is the plugin)
- CSS files: `*.css` co-located with their plugin (e.g., `src/plugins/themes/forest-green.css`)
- Types: `*.ts` in `src/types/` for reusable types
- Scripts: `scripts/{action}.ts` for build/install/uninstall actions
- Pages: `installer/src/pages/{name}.ts` for installer pages

**Directories:**
- Plugin directories: lowercase with hyphens (sentry-blocker, settings-panel)
- API modules: lowercase single-word names (api, core, types, plugins)
- Feature directories: by feature name (themes, sentry-blocker)

**Functions & Variables:**
- Utility functions: camelCase (injectCss, setCssVariable, waitForElement)
- Constants: UPPER_SNAKE_CASE (PROFILE_DIR, INJECTION_MARKER, DEFAULT_SETTINGS)
- Types: PascalCase (UprootedPlugin, BridgeEvent, PluginSettings)
- Component classes: PascalCase (PluginLoader)

**Exports:**
- Barrel exports in index.ts: `export * as module from "./file.js"`
- Plugin default exports: `export default {...satisfies UprootedPlugin}`
- Public API: all exports from `src/api/index.ts`

## Where to Add New Code

**New Built-in Plugin:**
1. Create directory: `src/plugins/{plugin-name}/`
2. Create entry: `src/plugins/{plugin-name}/index.ts` with default export satisfying `UprootedPlugin`
3. Define patches in plugin's `patches` array
4. If CSS needed: `src/plugins/{plugin-name}/{name}.css` (auto-collected at build time)
5. Register in preload: Add `loader.register(yourPlugin)` in `src/core/preload.ts` before `loader.startAll()`

**New Utility/Helper:**
- Shared utilities: `src/api/` (if part of public API) or `src/core/` (if internal only)
- Plugin-specific helpers: within plugin directory

**New Type Definition:**
- Reusable types: `src/types/{domain}.ts`
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

## Special Directories

**dist/:**
- Purpose: Build output
- Generated: Yes (by `pnpm build`)
- Committed: No (in .gitignore)
- Contents: `uprooted-preload.js` (IIFE bundle) and `uprooted.css` (combined plugin CSS)

**installer/src-tauri/artifacts/:**
- Purpose: Staged binary artifacts for Tauri bundling
- Generated: Yes (by `scripts/build_installer.ps1`)
- Contents: `uprooted_profiler.dll`, `UprootedHook.dll`, `UprootedHook.deps.json`, `uprooted-preload.js`, `uprooted.css`

**node_modules/:**
- Purpose: Installed dependencies
- Generated: Yes (by pnpm)
- Committed: No (in .gitignore)

**.planning/codebase/:**
- Purpose: GSD codebase analysis documents
- Generated: Yes (by /gsd:map-codebase)
- Committed: Yes (tracked in git)

**hook-test/:**
- Purpose: Test harness for .NET hook
- Built with: .NET
- Run via: PowerShell scripts

**research/:**
- Purpose: Development and analysis workspaces
- Contents: Pentesting notes, source analysis, dev scripts

**legacy/:**
- Purpose: Archived code
- Status: Not actively maintained

## Monorepo Structure

This is a pnpm workspace with three packages:

1. **root package** (`./package.json`): Shared scripts and workspaces definition
   - Scripts: `build` (builds src/), `dev`, `install-root`, `uninstall-root`, `site:build`, `site:dev`, `installer:dev`, `installer:build`

2. **installer** (`./installer/package.json`): Tauri desktop application
   - Type: Vite + TypeScript frontend + Tauri backend
   - Scripts: `dev` (Vite dev server), `build` (TypeScript + Vite), `tauri` (Tauri CLI)

3. **site** (`./site/package.json`): Marketing website
   - Type: Astro static site
   - Scripts: `dev`, `build`

Run pnpm commands from root; package filtering with `--filter` (e.g., `pnpm --filter installer build`)

## Installer Build Pipeline

`scripts/build_installer.ps1` orchestrates the full installer build in 5 steps:

1. `pnpm build` — TypeScript layer -> `dist/uprooted-preload.js` + `dist/uprooted.css`
2. `dotnet build hook/ -c Release` — C# hook -> `UprootedHook.dll`
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
- Defines: root, installer, site as workspace packages

**pnpm-lock.yaml:**
- Lockfile for reproducible installs
- Committed to git
