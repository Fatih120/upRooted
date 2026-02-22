# Technology Stack

**Analysis Date:** 2026-02-17

## Languages

**Primary:**
- TypeScript 5.7.0 - Core plugin system, preload injection, build scripts
- Rust 2024 edition - Console TUI installer (ratatui + crossterm), native system integration, Windows/Linux specific code
- C# (.NET 10) - Hook system for Root Communications profiler injection
- C - CLR profiler DLL for Windows binary patching, shared object for Linux
- JavaScript/ES2022 - Runtime execution in browser context

**Native:**
- C - CLR profiler DLL (`uprooted_profiler.dll` on Windows, `libuprooted_profiler.so` on Linux) implementing `ICorProfilerCallback` COM interface for IL injection

**Secondary:**
- PowerShell - Installation scripts, build scripts, testing automation
- Python - Log analysis, binary patching utilities (research directory)
- Bash/Shell - Linux installation and verification scripts

## Runtime

**JavaScript:**
- Node.js 20+ (minimum requirement via `engines.node`)
- Used for: TypeScript transpilation, build tools, package management

**Native:**
- Rust stable toolchain - Console TUI installer runtime
- .NET 10 runtime - Hook DLL execution environment (embedded in Root.exe)
- Windows SDK - MSVC compiler (VS 2022 Build Tools for profiler DLL compilation)
- WebKit2GTK 4.1 - Linux desktop Tauri runtime (installer frontend)
- Chromium 144 (DotNetBrowser) - Root's embedded browser (not used by Uprooted directly, but present in host)

**Package Manager:**
- pnpm 10+ - Monorepo management, lockfile version 9.0
- Lockfile: `pnpm-lock.yaml` (present)
- Cargo (Rust) - Rust package management

## Frameworks

**Core Application:**
- Desktop TUI Installer: ratatui 0.29 + crossterm 0.28 + clap 4 (Rust)
  - No web frontend, no Tauri GUI wrapper
  - Pure console TUI with embedded artifacts via `include_bytes!()`

**Frontend:**
- Vite 6.1.0 - Module bundler for installer UI (if any web UI components present)
- Astro 5.3.0 - Static site generation for marketing website (site/)

**Build & Development:**
- esbuild 0.24.2 - Fast bundler for Uprooted preload script to IIFE bundle
- tsx 4.19.0 - TypeScript runtime for build scripts
- TypeScript 5.7.0 - Type checking across all TS code

**Testing:**
- Not detected - No test framework configured (no jest.config, vitest.config, etc.)
- UnitTests directory present for C# (UprootedTests/) but not integrated into build

## Key Dependencies

**Critical (TypeScript/JavaScript):**
- `esbuild` 0.24.2 - Bundles `src/core/preload.ts` to IIFE format for `<script>` tag injection
- `typescript` 5.7.0 - Type checking
- `tsx` 4.19.0 - Runs build/install scripts written in TypeScript

**Critical (Rust/Installer):**
- `serde`, `serde_json` 1 - Serialization/deserialization for settings and JSON management
- `ratatui` 0.29 - Terminal UI framework
- `crossterm` 0.28 - Cross-platform terminal backend
- `clap` 4 - CLI argument parsing (derive macro)
- `glob` 0.3 - File pattern matching in Rust
- `winreg` 0.55 - Windows registry access (Windows-only)
- `windows-sys` 0.59 - Windows API bindings (Windows-only)

**Native (C/MSVC):**
- `uprooted_profiler.dll` - CLR profiler implementing `ICorProfilerCallback` COM interface
  - Injects IL bytecode into .NET methods before JIT compilation
  - Process name guard (only activates for Root.exe)
  - Enabled via user-scoped env vars: `CORECLR_ENABLE_PROFILING`, `CORECLR_PROFILER`, `CORECLR_PROFILER_PATH`
  - Requires `DOTNET_ReadyToRun=0` to disable precompiled native images (R2R bypass)

**Build & Bundling:**
- `esbuild` - JavaScript/CSS bundling to IIFE format
- `vite` - Frontend asset bundling (installer frontend)
- `astro` - Static site generation

**Types:**
- `@types/node` 22.0.0 - Node.js type definitions

## Configuration

**Environment:**
- Vite: Reads `VITE_*` and `TAURI_*` prefixed environment variables
- No `.env` files in repository (not detected)
- Build-time configuration via TypeScript constants and `define` options

**Build:**
- TypeScript: `tsconfig.json` (root level for src, separate for installer)
  - Target: ES2022 for core, ES2021 for installer
  - Module resolution: bundler
  - Path alias: `@uprooted/*` → `src/*`
- Vite: `vite.config.ts` (installer only)
  - Target: Chrome 120 (matches Root's DotNetBrowser Chromium version)
  - Sourcemaps enabled
  - esbuild minification
- Astro: `astro.config.mjs` (site)
  - Static output
  - Site URL: https://uprooted.sh
- Rust: `Cargo.toml` with release profile optimizations
  - `strip = "symbols"` - Remove debug symbols
  - `lto = true` - Link-time optimization
  - `codegen-units = 1` - Single codegen unit for better optimization
  - `panic = "abort"` - Abort on panic (smaller binary)
  - `opt-level = "z"` - Optimize for size

## Profiler Injection

**Primary Method: CLR Profiler (default, recommended)**
- Native C DLL/SO loaded by .NET runtime at process startup
- Sets environment variables: `CORECLR_ENABLE_PROFILING=1`, `CORECLR_PROFILER={D1A6F5A0-1234-4567-89AB-CDEF01234567}`, `CORECLR_PROFILER_PATH`, `DOTNET_ReadyToRun=0`
- Injects IL into first suitable method (typically Sentry.dll or similar third-party assembly) to load `UprootedHook.dll`
- Process guard: checks executable name and detaches for non-Root processes
- Event mask: `COR_PRF_MONITOR_JIT_COMPILATION` (0x20) + `COR_PRF_MONITOR_MODULE_LOADS` (0x04) + `COR_PRF_DISABLE_ALL_NGEN_IMAGES` (0x80000)
- Survives Root updates (no binary modification)
- Source: `tools/uprooted_profiler.c` (Windows), `tools/uprooted_profiler_linux.c` (Linux)

**Fallback Method: DOTNET_STARTUP_HOOKS**
- Alternative entry point via `DOTNET_STARTUP_HOOKS` environment variable
- Requires patching Root.exe to change startup hook support flag (breaks on Root updates)
- Both paths guarded by atomic `Interlocked.CompareExchange` to prevent double initialization
- Implementation: `hook/StartupHook.cs` with required signature (no namespace, `internal class StartupHook`, `public static void Initialize()`)

## Platform Requirements

**Development:**
- Windows with MSVC toolchain (VS 2022 Build Tools minimum for profiler DLL compilation)
  - `vcvarsall.bat` for MSVC environment setup (auto-located via `vswhere.exe`)
  - `cl.exe` compiler flags: `/LD /O2 /GS- /DEBUG:NONE /OPT:REF /OPT:ICF`
- Node.js 20+
- Rust stable toolchain
- .NET 10 SDK (preview releases supported)
- pnpm 10+

**Production/Target:**
- Windows (primary installer target)
  - x86_64 architecture
  - .NET 10 runtime (self-contained in Root.exe)
  - MSVC runtime
- Linux
  - x86_64 architecture
  - Requires libwebkit2gtk-4.1-0 (for Tauri/installer, not Root itself)
  - GCC for profiler compilation
  - Debian package support via deb format

## Monorepo Structure

**Workspace:** pnpm monorepo with three packages
- `.` (root) - Core Uprooted library, build scripts, CLI, profiler/hook source
- `site/` - Marketing website (Astro)
- `installer/` - Rust TUI application (ratatui, no pnpm, independent cargo workspace)

**Rust Edition:** 2024 (installer/src-tauri/Cargo.toml)

**Build Pipeline:**
1. TypeScript layer (`pnpm build`):
   - esbuild bundles `src/core/preload.ts` → `dist/uprooted-preload.js` (IIFE format with globalName="Uprooted")
   - Collects CSS from `src/plugins/**/*.css` → `dist/uprooted.css`
   - Generates additional artifacts: `dist/nsfw-filter.js`, `dist/link-embeds.js`
   - Injects version constant `__UPROOTED_VERSION__` from package.json
2. C# Hook (`dotnet build hook/UprootedHook.csproj -c Release`):
   - Targets `net10.0`
   - Output: `hook/bin/Release/net10.0/UprootedHook.dll` + `UprootedHook.deps.json`
3. Native Profiler:
   - Windows (`cl.exe`): Compiles `tools/uprooted_profiler.c` with exports from `tools/uprooted_profiler.def`
   - Linux (`gcc`): Compiles `tools/uprooted_profiler_linux.c` with `-shared -fPIC -O2 -s -fvisibility=hidden`
4. Installer (`cargo build --release`):
   - Rust TUI embeds all 7 artifacts via `include_bytes!()` in `installer/src-tauri/src/embedded.rs`
   - Required artifacts directory: `installer/src-tauri/artifacts/`
   - Output: `installer/src-tauri/target/release/uprooted.exe` (Windows) or `uprooted` (Linux)

**Artifact Staging:** All artifacts embedded in installer binary at compile time via:
- `uprooted_profiler.dll` or `libuprooted_profiler.so`
- `UprootedHook.dll`
- `UprootedHook.deps.json`
- `uprooted-preload.js`
- `uprooted.css`
- `nsfw-filter.js`
- `link-embeds.js`

## Root's Technology Stack (Host Application)

**Runtime:** .NET 10 (self-contained executable, 617 MB)
- All assemblies bundled; extracted to memory at runtime
- Avalonia 11.3.10 UI framework

**UI:** Native Avalonia controls (not DotNetBrowser)
- Chat messages, channels, community views = Avalonia controls (~1647+ visual tree nodes)
- Sidebar, settings pages = Avalonia controls

**Browser:** DotNetBrowser (Chromium 144)
- Used for: WebRTC (voice/video), OAuth flows, embedded sub-apps
- Launched with `--incognito` (no persistent storage) and `--disable-web-security` (CORS disabled)
- DevTools disabled

**Sub-Apps (Embedded):** 7 React/Vite/Alpine.js applications in iframe contexts
- Hexatris, Polls, Suggestions, Minecraft Easy Setup, Task Tracker, Stickerwall, Raid Planner
- Use `window.__rootSdkBridgeWebToNative` and `window.__rootSdkBridgeNativeToWeb` (NOT proxied by Uprooted)

**Backend Protocol:** gRPC-web (HTTP/1.1 or HTTP/2)
- Endpoint: `https://api.rootapp.com/{package}.{Service}/{Method}`
- Protobuf serialization
- Length-prefixed frames with trailer encoding

---

*Stack analysis: 2026-02-17*
