# External Integrations

**Analysis Date:** 2026-02-17

## APIs & External Services

**Root Communications Platform:**
- Type: Host application integration (dual-layer)
- Purpose: Uprooted is a client mod framework for Root Communications
- C# integration: CLR profiler IL injection into Root's .NET 10/Avalonia process
  - Entry point: `tools/uprooted_profiler.c` (Windows) / `tools/uprooted_profiler_linux.c` (Linux)
  - Profiler GUID: `{D1A6F5A0-1234-4567-89AB-CDEF01234567}`
  - Managed hook: `UprootedHook.dll` loaded via IL injection
- TypeScript integration: HTML `<script>` tag injection into DotNetBrowser Chromium context
  - Implementation: `hook/HtmlPatchVerifier.cs` patches HTML files with script tags
  - Bundle: `dist/uprooted-preload.js` (IIFE format, `globalName: "Uprooted"`)
- No external API calls — operates as embedded plugin system

**Root's gRPC-web Backend:**
- Endpoint: `https://api.rootapp.com/`
- Protocol: gRPC-web (HTTP/1.1 compatible)
- Services: UserGrpcService, CommunityGrpcService, MessageGrpcService (v2), etc.
- Serialization: Protocol Buffers
- Headers required: `Content-Type: application/grpc-web+proto`, `Authorization: Bearer {token}`, `X-Grpc-Web: 1`
- Uprooted can proxy/intercept via `window.__nativeToWebRtc` and `window.__webRtcToNative` bridge objects
- Reference: `docs/research/GRPC_PROTOCOL.md`, `research/pentesting/grpc_lib.py`

**Sentry Integration (Blocked):**
- Service: Sentry.io error tracking (blocked by design)
- Implementation: `src/plugins/sentry-blocker/index.ts`
- Details:
  - Plugin intercepts and blocks all requests to `*.sentry.io` domains
  - Wraps native `fetch()` and `XMLHttpRequest.prototype.open()`
  - Prevents auth header leakage in Sentry breadcrumbs
  - Built-in plugin registered early to wrap fetch before other code
  - Maintains block counter for diagnostic logging

**GitHub Integration:**
- Type: Repository + documentation links only
- Repository: https://github.com/The-Uprooted-Project/uprooted (public scaffold)
- Issue tracking: GitHub Issues (referenced in UI)
- CI/CD: GitHub Actions workflows for installer builds
- No API integration (static links in settings panel)

**Root WebRTC Bridge (Proxied):**
- Type: JavaScript bridge objects on `window`
- `window.__nativeToWebRtc` — INativeToWebRtc (42 methods): C# host controls WebRTC session
- `window.__webRtcToNative` — IWebRtcToNative (29 methods): WebRTC JS notifies C# host
- Status: Proxied by Uprooted via ES6 Proxy wrappers — plugins can intercept all method calls
- Only available when a voice session is active
- Documentation: `docs/plugins/ROOT_ENVIRONMENT.md`

**Root Sub-App Bridge (NOT Proxied):**
- Type: JavaScript bridge object on `window` in sub-app iframe contexts
- `window.__rootSdkBridgeWebToNative` — Sub-app bridge for protobuf communication
- `window.__rootSdkBridgeNativeToWeb` — Sub-app inbound bridge
- Status: NOT proxied by Uprooted — Uprooted plugins cannot intercept sub-app bridge calls
- Used by: 7 embedded React/Vite/Alpine.js sub-apps:
  - Hexatris (multiplayer game)
  - Polls (community polls)
  - Suggestions (suggestion board)
  - Minecraft Easy Setup (server management)
  - Task Tracker (task management)
  - Stickerwall (Alpine.js + PixiJS sticker board)
  - Raid Planner (raid scheduling, 80k+ lines)

## Data Storage

**Local Storage:**
- Settings: JSON-based configuration file
  - Location: Platform-specific user profile directory
  - Implementation: `src/core/settings.ts`, `installer/src-tauri/src/settings.rs`
  - Windows: `%LocalAppData%\Root\uprooted\uprooted-settings.json`
  - Linux: `~/.local/share/uprooted/uprooted-settings.json`
  - INI fallback: `uprooted-settings.ini` (legacy)
  - Root's HTML patch locations: `%LocalAppData%\Root Communications\Root\profile\default\` (Windows)

**Hook Logs:**
- Profiler log: Native C profiler output
  - Windows: `%LocalAppData%\Root\uprooted\profiler.log`
  - Linux: `~/.local/share/uprooted/profiler.log`
  - Implementation: `tools/uprooted_profiler.c` lines 172-186
- Managed hook log: C# hook output
  - Windows: `%LocalAppData%\Root Communications\Root\profile\default\uprooted-hook.log`
  - Implementation: `hook/Logger.cs` with thread-safe `File.AppendAllText` and swallowed catch

**File System:**
- Installation artifacts: Local machine installation directory
  - Hook DLL path resolved by: `hook/PlatformPaths.cs`
  - Installer artifacts embedded in binary via `installer/src-tauri/src/embedded.rs`
- No central database or remote data storage
- All data persists locally only

**DotNetBrowser (Root's Chromium):**
- Caching: None — launched with `--incognito` flag
- localStorage: Disabled (incognito mode)
- IndexedDB: Disabled (incognito mode)
- Cookies: Session-only

**Caching:**
- None detected in Uprooted layer - no caching framework

## Authentication & Identity

**Authentication:**
- Not applicable to Uprooted itself - desktop framework for Root Communications
- Root provides all user context via its authentication system
- Root backend auth: OAuth flows (handled by Root, not Uprooted)
- gRPC API auth: Bearer tokens in `Authorization: Bearer {token}` header
- No user login system in Uprooted

## Monitoring & Observability

**Error Tracking:**
- Sentry: Explicitly blocked (see Sentry Integration above)
- No alternative error tracking service detected

**Logs:**
- Console logging via `nativeLog()` and `console.log()`
  - Implementation: `src/api/native.ts` provides native logging bridge
- Profiler logs: File-based in platform-specific directories (see Data Storage)
- Build logs: CI/CD workflow logging only (GitHub Actions)
- No remote log aggregation

**Debugging:**
- TypeScript source maps: `dist/uprooted-preload.js.map` for browser DevTools
- C# debugger attachment: Visual Studio or Rider to Root.exe
- Native profiler debugging: Visual Studio native debugger or WinDbg

## CI/CD & Deployment

**Hosting:**
- GitHub repository hosting (public: The-Uprooted-Project/uprooted, private: The-Uprooted-Project/uprooted-private)
- Desktop application (no cloud backend)

**CI Pipeline:**
- GitHub Actions workflows
- `build-installer.yml` - Windows installer build
  - Triggers: Manual dispatch + path-based push triggers
  - Environment: `windows-latest`
  - Installs: MSVC (ilammy/msvc-dev-cmd), .NET 10 Preview, Node.js 22, pnpm 10, Rust stable
  - Steps: pnpm build → dotnet build hook → cl.exe profiler compile → cargo build --release
  - Output: `Uprooted-{version}-Setup.exe`
- `build-linux.yml` - Linux artifact build
  - Environment: `ubuntu-latest`
  - Installs: gcc, Node.js, Rust stable
  - Steps: pnpm build → dotnet build hook → gcc profiler compile → cargo build --release
  - Output: `Uprooted-{version}-linux-amd64`

**Build Artifacts:**
- Produced: Self-contained portable executables
  - Windows: `Uprooted-[version]-Setup.exe` (with embedded artifacts)
  - Linux: `Uprooted-[version]-linux-amd64` (with embedded artifacts)
- Storage: GitHub Actions artifact storage + releases
- Manual downloads from GitHub releases page

## Environment Configuration

**Installation Requirements:**
- Windows Registry (installer reads/writes Root Communications path)
  - Uses `winreg` crate (0.55) for registry access
  - Reads: Root installation directory detection
  - Writes: Environment variables via registry (user-scoped)
- User profile directory path (for settings storage)
- Environment variables for profiler hook (set during installation)

**Profiler Environment Variables (Set by Installer):**
- Windows: Both `CORECLR_` and `DOTNET_` prefixes set for .NET 8/9/10 compatibility
  - `CORECLR_ENABLE_PROFILING=1` / `DOTNET_ENABLE_PROFILING=1`
  - `CORECLR_PROFILER={D1A6F5A0-1234-4567-89AB-CDEF01234567}` / `DOTNET_PROFILER=...`
  - `CORECLR_PROFILER_PATH=<path>` / `DOTNET_PROFILER_PATH=<path>`
  - `DOTNET_ReadyToRun=0` (disables R2R to force JIT)
- Linux: Shell profile and systemd unit overrides
- Root Communications installation path (auto-detected via registry or glob patterns)
- .NET runtime directory (auto-detected)
- UprootedHook.dll path (set by installer)

**Build Environment Variables:**
- `VITE_*` - Passed to Vite bundler (installer)
- `TAURI_*` - Passed to Tauri framework (if used)
- `CC`, `CXX` - C/C++ compilers (Windows builds)
- `CARGO_TARGET_X86_64_PC_WINDOWS_MSVC_LINKER` - Rust linker configuration

**Secrets Location:**
- No secrets stored in repository
- No `.env` files present
- Credentials passed via GitHub Actions environment (if any builds required auth)

## Webhooks & Callbacks

**Incoming:**
- Not applicable - desktop application with no server component

**Outgoing:**
- Sentry webhooks: Explicitly blocked (see Sentry Integration)
- No other outgoing webhooks detected

## Native System Integrations

**Windows Specifics:**
- Registry access: `winreg` crate 0.55
  - Reads Root Communications installation path from HKEY_CURRENT_USER
  - Modifies environment variables via registry (user-scoped)
  - Patches protocol handlers: `rootapp://` scheme registration
  - Manages shortcuts: Start Menu, Desktop, Startup, Taskbar (with backup/restore)
- Process detection: `windows-sys` crate 0.59
  - `Win32_UI_WindowsAndMessaging` - Window enumeration
  - `Win32_Foundation` - Core types
  - `Win32_System_Diagnostics_ToolHelp` - Process enumeration
  - `Win32_System_Threading` - Thread information
  - Detects running Root Communications process
  - Monitors for process termination (hook status check)
- Window interaction: Avalonia reflection in hook
  - Accesses Root UI element tree via reflection
  - Modifies UI components via reflection (sidebar injection, theme engine)
  - DotNetBrowser discovery via reflection: `DotNetBrowserReflection.cs`

**Linux Specifics:**
- File system installation
  - Installation path: `~/.local/share/uprooted/`
  - Shell script: `install-uprooted-linux.sh`
- Library detection via glob patterns
  - Searches for libwebkit2gtk-4.1-0 via `glob` crate
- Shell command execution: Tauri shell plugin (if used)
- Executable permissions: chmod +x on installer

## Third-Party SDK Dependencies

**Tauri Ecosystem:**
- `@tauri-apps/api` - Invokes Rust backend from TypeScript (if web UI present)
- `tauri-plugin-shell` - Execute shell commands from Rust (if web UI present)
- Note: Installer is pure TUI, not Tauri GUI; these may be unused

**Rust Crates (Installer):**
- `serde`, `serde_json` 1 - Data serialization (JSON settings)
- `glob` 0.3 - File pattern matching
- `ratatui` 0.29 - Terminal UI framework
- `crossterm` 0.28 - Terminal abstraction
- `clap` 4 - CLI argument parsing

**NPM Packages (Build):**
- `tsx` 4.19.0 - TypeScript execution in build scripts
- `esbuild` 0.24.2 - JavaScript bundling
- `vite` 6.1.0, `astro` 5.3.0 - Frontend bundling

## Platform-Specific Libraries

**Windows:**
- `windows-sys` 0.59 - Win32 API bindings for process/window detection
- `winreg` 0.55 - Windows Registry API
- MSVC C++ compiler - Profiler DLL compilation (VS 2022 Build Tools)
- `ole32.lib`, `kernel32.lib`, `shell32.lib` - Profiler linker dependencies

**Linux:**
- GCC/Clang - C compilation for Linux profiler
- POSIX APIs - For path resolution, process detection, atomic operations
- WebKit2GTK 4.1 - Browser runtime (via Tauri, if needed)

## Installation Delivery

**Windows:**
- Self-contained portable executable: `Uprooted-[version]-Setup.exe`
  - All artifacts embedded via `include_bytes!()`
  - Console TUI interface
  - No external dependencies (except .NET 10 and MSVC runtime already on system)
- PowerShell installer script (legacy): `install-hook.ps1` (optional fallback)
- PowerShell uninstall script: `uninstall-hook.ps1`
- Git repository clone option: https://github.com/The-Uprooted-Project/uprooted

**Linux:**
- Self-contained executable: `Uprooted-[version]-linux-amd64`
  - All artifacts embedded via `include_bytes!()`
  - Console TUI interface
- Shell script installer (legacy): `install-uprooted-linux.sh`
- Shell script uninstall: `uninstall-uprooted-linux.sh`
- Git repository clone option: https://github.com/The-Uprooted-Project/uprooted

## Installer Mechanics

**Artifact Embedding:**
- Key mechanism: Rust's `include_bytes!()` in `installer/src-tauri/src/embedded.rs`
- Compile-time inclusion (not runtime loading)
- All 7 artifacts must exist before `cargo build --release`:
  ```rust
  pub const PROFILER: &[u8] = include_bytes!("../artifacts/uprooted_profiler.dll");
  pub const HOOK_DLL: &[u8] = include_bytes!("../artifacts/UprootedHook.dll");
  pub const HOOK_DEPS_JSON: &[u8] = include_bytes!("../artifacts/UprootedHook.deps.json");
  pub const PRELOAD_JS: &[u8] = include_bytes!("../artifacts/uprooted-preload.js");
  pub const THEME_CSS: &[u8] = include_bytes!("../artifacts/uprooted.css");
  pub const NSFW_FILTER_JS: &[u8] = include_bytes!("../artifacts/nsfw-filter.js");
  pub const LINK_EMBEDS_JS: &[u8] = include_bytes!("../artifacts/link-embeds.js");
  ```

**Installation Steps (Windows):**
1. Installer detects Root installation path via Windows Registry
2. Creates `%LocalAppData%\Root\uprooted\` directory
3. Deploys: `uprooted_profiler.dll`, `UprootedHook.dll`, `UprootedHook.deps.json`
4. Patches shortcuts: Start Menu, Desktop, Startup, Taskbar
5. Sets profiler environment variables (user-scoped registry)
6. Patches HTML files in Root's app directory with `<script>` tags (HtmlPatchVerifier)
7. Configures protocol handler for `rootapp://` scheme

**Installation Steps (Linux):**
1. Installer detects Root installation via file globbing
2. Creates `~/.local/share/uprooted/` directory
3. Deploys: `libuprooted_profiler.so`, `UprootedHook.dll`, `UprootedHook.deps.json`
4. Sets environment variables via shell profile or systemd unit
5. Patches HTML files
6. Sets executable permissions

---

*Integration audit: 2026-02-17*
