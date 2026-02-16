# External Integrations

**Analysis Date:** 2026-02-16

## APIs & External Services

**Sentry Integration (Blocked):**
- Service: Sentry.io error tracking
- Status: Actively blocked
- Implementation: `src/plugins/sentry-blocker/index.ts`
- Details:
  - Plugin intercepts and blocks all requests to `*.sentry.io` domains
  - Wraps native `fetch()` and `XMLHttpRequest.prototype.open()`
  - Prevents auth header leakage in Sentry breadcrumbs
  - Built-in plugin registered early to wrap fetch before other code
  - Maintains block counter for diagnostic logging

**GitHub Integration:**
- Type: Repository + documentation links only
- Repository: https://github.com/watchthelight/uprooted
- Issue tracking: GitHub Issues (referenced in UI)
- No API integration (static links in settings panel)

**Root Communications Platform:**
- Type: Host application integration (dual-layer)
- Purpose: Uprooted is a client mod framework for Root Communications
- C# integration: CLR profiler IL injection into Root's .NET 10/Avalonia process
- TypeScript integration: HTML `<script>` tag injection into DotNetBrowser Chromium context
- No external API calls — operates as embedded plugin system

**Root WebRTC Bridge (Proxied):**
- Type: JavaScript bridge objects on `window`
- `window.__nativeToWebRtc` — INativeToWebRtc (42 methods): C# host controls WebRTC session
- `window.__webRtcToNative` — IWebRtcToNative (29 methods): WebRTC JS notifies C# host
- Status: Proxied by Uprooted via ES6 Proxy wrappers — plugins can intercept all method calls
- Only available when a voice session is active

**Root Sub-App Bridge (NOT Proxied):**
- Type: JavaScript bridge object on `window` in sub-app iframe contexts
- `window.__rootSdkBridgeWebToNative` — Sub-app bridge for protobuf communication
- `window.__rootSdkBridgeNativeToWeb` — Sub-app inbound bridge
- Status: NOT proxied by Uprooted — Uprooted plugins cannot intercept sub-app bridge calls
- Used by: 7 embedded React/Vite sub-apps running in separate iframe contexts:
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
  - Format: UprootedSettings structure with plugin config
  - File: `uprooted-settings.json` (inferred from code)

**File System:**
- Installation artifacts: Local machine installation directory
- No central database or remote data storage
- All data persists locally only

**Caching:**
- None detected - no caching framework

## Authentication & Identity

**Authentication:**
- Not applicable - desktop application for Root Communications
- No user login system
- No API keys or tokens managed
- Host application (Root) provides all user context

## Monitoring & Observability

**Error Tracking:**
- Sentry: Explicitly blocked (see Sentry Integration above)
- No alternative error tracking service detected

**Logs:**
- Approach: Console logging via `nativeLog()` and `console.log()`
- Implementation: `src/api/native.ts` provides native logging bridge
- Build logs: CI/CD workflow logging only (GitHub Actions)
- No remote log aggregation

## CI/CD & Deployment

**Hosting:**
- GitHub repository hosting
- No cloud deployment (desktop application)

**CI Pipeline:**
- GitHub Actions workflows present
- `build-installer.yml` - Windows installer build
- `build-linux.yml` - Linux artifact build
- Triggered by: Push events, manual workflow dispatch
- Artifacts:
  - Windows: Portable `.exe` (Uprooted-X.X.X.exe)
  - Linux: Application binaries
  - Published via GitHub releases/artifacts

**Build Artifacts:**
- Produced: `Uprooted-[version].exe` (Windows portable executable)
- Storage: GitHub Actions artifact storage
- Manual downloads from releases

## Environment Configuration

**Required for Installation:**
- Windows Registry (installer reads/writes Root Communications path)
- User profile directory path (for settings storage)
- Environment variables for profiler hook (set during installation)

**Installer Environment Variables:**
- Root Communications installation path (auto-detected)
- .NET runtime directory (auto-detected)
- UprootedHook.dll path (set by installer)

**Build Environment Variables:**
- `VITE_*` - Passed to Vite bundler (installer)
- `TAURI_*` - Passed to Tauri framework
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
- Sentry webhooks: Explicitly blocked
- No other outgoing webhooks detected

## Native System Integrations

**Windows Specifics:**
- Registry access: `winreg` crate
  - Reads Root Communications installation path
  - Modifies environment variables via registry
- Process detection: `windows-sys` crate
  - Detects running Root Communications process
  - Monitors for process termination (hook status check)
- Window interaction: Avalonia reflection in hook
  - Accesses Root UI element tree
  - Modifies UI components via reflection

**Linux Specifics:**
- File system installation
- Library detection via glob patterns
- Shell command execution (Tauri plugin)

## Third-Party SDK Dependencies

**Tauri Ecosystem:**
- `@tauri-apps/api` - Invokes Rust backend from TypeScript
- `tauri-plugin-shell` - Execute shell commands from Rust

**Rust Crates:**
- `serde`, `serde_json` - Data serialization
- `glob` - File pattern matching
- `opener` - Cross-platform file opening

**NPM Packages:**
- `tsx` - TypeScript execution in build scripts
- `esbuild` - JavaScript bundling
- `vite`, `astro` - Frontend bundling

## Platform-Specific Libraries

**Windows:**
- `windows-sys` 0.59 - Provides Win32 API bindings for:
  - `Win32_UI_WindowsAndMessaging` - Window enumeration
  - `Win32_Foundation` - Core types
  - `Win32_System_Diagnostics_ToolHelp` - Process enumeration
  - `Win32_System_Threading` - Thread information
- MSVC C++ compiler - Profiler DLL compilation

**Linux:**
- WebKit2GTK 4.1 - Browser runtime (via Tauri)
- GCC/Clang - C compilation for Linux profiler

## Installation Delivery

**Windows:**
- Self-contained portable executable
- PowerShell installer script (fallback)
- PowerShell uninstall script
- Git repository clone option

**Linux:**
- Shell script installer (`install-uprooted-linux.sh`)
- Shell script uninstall
- Git repository clone option

---

*Integration audit: 2026-02-16*
