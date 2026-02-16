# Technology Stack

**Analysis Date:** 2026-02-16

## Languages

**Primary:**
- TypeScript 5.7-5.9 - Core plugin system, preload injection, build scripts
- Rust 2021 edition - Tauri backend, native system integration, Windows/Linux specific code
- C# (.NET 10) - Hook system for Root Communications profiler injection
- C - Profiler DLL for Windows binary patching
- JavaScript/ES2022 - Runtime execution in browser context

**Native:**
- C - CLR profiler DLL (`uprooted_profiler.dll`) implementing `ICorProfilerCallback` COM interface for IL injection

**Secondary:**
- PowerShell - Installation scripts, build scripts, testing automation
- Python - Log analysis, binary patching utilities
- Bash/Shell - Linux installation and verification scripts

## Runtime

**JavaScript:**
- Node.js 20+ (minimum requirement via `engines.node`)
- Used for: TypeScript transpilation, build tools, package management

**Native:**
- Rust stable toolchain - Tauri application runtime
- .NET 10 runtime - Hook DLL execution environment
- Windows SDK - MSVC compiler (Windows builds)
- WebKit2GTK 4.1 - Linux desktop app runtime

**Package Manager:**
- pnpm 10 - Monorepo management, lockfile version 9.0
- Lockfile: `pnpm-lock.yaml` (present)

## Frameworks

**Core Application:**
- Tauri 2.5.0 - Desktop app framework combining TypeScript frontend with Rust backend
  - `@tauri-apps/api@2.5.0` - TypeScript bindings to Rust commands
  - `@tauri-apps/cli@2.5.0` - Build and development tooling
  - `tauri-plugin-shell@2` - Shell command execution plugin

**Frontend:**
- Vite 6.1.0 - Module bundler for installer UI
- Astro 5.3.0 - Static site generation for website

**Build & Development:**
- esbuild 0.24.2 - Fast bundler for Uprooted preload script
- tsx 4.19.0 - TypeScript runtime for scripts
- TypeScript 5.7.0 - Type checking across all TS code

**Testing:**
- Not detected - No test framework configured

## Key Dependencies

**Critical:**
- `tauri` 2.x - Desktop application framework, primary runtime
- `serde`, `serde_json` - Serialization/deserialization in Rust backend
- `winreg` - Windows registry access (Windows-only)
- `windows-sys` - Windows API bindings for process/UI detection
- `glob` - File pattern matching in Rust
- `opener` - Cross-platform file/directory opener

**Native (C/MSVC):**
- `uprooted_profiler.dll` - CLR profiler implementing `ICorProfilerCallback` COM interface
  - Injects IL bytecode into .NET methods before JIT compilation
  - Process name guard (only activates for Root.exe)
  - Enabled via user-scoped env vars: `CORECLR_ENABLE_PROFILING`, `CORECLR_PROFILER`, `CORECLR_PROFILER_PATH`
  - Requires `DOTNET_ReadyToRun=0` to disable precompiled native images

**Build & Bundling:**
- `esbuild` - JavaScript/CSS bundling
- `vite` - Frontend asset bundling
- `astro` - Static site generation

**Types:**
- `@types/node@22.0.0` - Node.js type definitions

## Configuration

**Environment:**
- Vite: Reads `VITE_*` and `TAURI_*` prefixed environment variables
- Tauri: Custom protocol mode for app communication
- No `.env` files in repository (not detected)

**Build:**
- TypeScript: `tsconfig.json` (root level for src, separate for installer)
  - Target: ES2022 for core, ES2021 for installer
  - Module resolution: bundler
  - Path alias: `@uprooted/*` → `src/*`
- Vite: `vite.config.ts` (installer only)
  - Target: Chrome 120
  - Sourcemaps enabled
  - esbuild minification
- Astro: `astro.config.mjs` (site)
  - Static output
  - Site URL: https://uprooted.sh

## Injection Methods

**Method 1: CLR Profiler (default, recommended)**
- Native C DLL loaded by .NET runtime at process startup
- Injects IL into first suitable method to load `UprootedHook.dll`
- Requires: `DOTNET_ReadyToRun=0` (disables precompiled images, forces JIT)
- Survives Root updates (no binary modification)

**Method 2: Startup Hooks (alternative)**
- Requires patching Root.exe to change `"System.StartupHookProvider.IsSupported": false` to `true`
- Then set `DOTNET_STARTUP_HOOKS` to UprootedHook.dll path
- Cleaner than profiler but breaks on Root updates (requires re-patching binary)

## Platform Requirements

**Development:**
- Windows with MSVC toolchain (for full builds with profiler DLL)
- Node.js 20+
- Rust stable toolchain
- .NET 10 (preview releases supported)
- pnpm 10

**Production/Target:**
- Windows (primary installer target)
  - x86_64 architecture
  - MSVC runtime
- Linux
  - Requires libwebkit2gtk-4.1-0
  - Debian package support via deb format

## Monorepo Structure

**Workspace:** pnpm monorepo with three packages
- `.` (root) - Core Uprooted library, build scripts, CLI
- `site/` - Marketing website (Astro)
- `installer/` - Desktop application (Tauri + Vite frontend)

**Build Pipeline:**
1. Root package: `pnpm build` → esbuild bundles `src/core/preload.ts` to `dist/uprooted-preload.js`
2. Root package: Collects CSS from all plugins → `dist/uprooted.css`
3. Installer: TypeScript + Vite frontend bundling
4. Installer: Rust backend compilation via Tauri
5. Site: Astro static generation

---

*Stack analysis: 2026-02-16*
