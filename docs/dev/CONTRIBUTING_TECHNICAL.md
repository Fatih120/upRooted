# Technical Onboarding Guide

> **What this is:** Technical onboarding for new contributors — dev environment setup, project layout orientation, debugging workflows, failure mode table, scripts reference, verification checklists.
> **Read when:** Setting up your dev environment; learning the debugging workflow; understanding common failure modes; finding the right script.
> **Skip if:** You need the build pipeline details → [BUILD.md](../install/BUILD.md). You need branch/PR rules → [CONTRIBUTING.md](../../CONTRIBUTING.md). You need architecture → [ARCHITECTURE.md](../framework/ARCHITECTURE.md).
> **Does NOT cover:** Build pipeline detail → [BUILD.md](../install/BUILD.md) | Branch/PR rules → [CONTRIBUTING.md](../../CONTRIBUTING.md) | Architecture → [ARCHITECTURE.md](../framework/ARCHITECTURE.md)

> **Related docs:** [Contributing](../../CONTRIBUTING.md) | [Build Guide](../install/BUILD.md) | [Architecture](../framework/ARCHITECTURE.md) | [Hook Reference](../framework/HOOK_REFERENCE.md)

This document covers the technical setup, build process, debugging workflows,
and common failure modes for new contributors to Uprooted. For branch rules,
commit format, PR process, and code style, see [CONTRIBUTING.md](../../CONTRIBUTING.md).

---

## Table of Contents

1. [Overview](#overview)
2. [Development Environment](#development-environment)
3. [Project Layout Orientation](#project-layout-orientation)
4. [Build Verification](#build-verification)
5. [Testing](#testing)
6. [Debugging the C# Hook](#debugging-the-c-hook)
7. [Debugging TypeScript](#debugging-typescript)
8. [Common Failure Modes](#common-failure-modes)
9. [Making Changes Safely](#making-changes-safely)
10. [Verification Checklist](#verification-checklist)
11. [Scripts Reference](#scripts-reference)
12. [Log File Locations](#log-file-locations)
13. [Settings File](#settings-file)

---

## Overview

Uprooted is a dual-layer client mod framework. Contributing effectively
requires understanding that the C# hook, the TypeScript browser injection,
the native CLR profiler, and the console TUI installer are all independent
components that must build and deploy together. This guide walks you through
setting up each layer, verifying that it works, and diagnosing problems when
it does not.

If you only plan to work on one layer (for example, TypeScript plugins), you
still need to understand the full deployment pipeline -- your changes must
integrate with the rest of the system when deployed to a Root installation.

---

## Development Environment

### Prerequisites Summary

| Tool            | Minimum Version  | Used For                           |
| --------------- | ---------------- | ---------------------------------- |
| Node.js         | 20+              | TypeScript build, scripts          |
| pnpm            | 10+              | Package management                 |
| .NET SDK        | 10.0 (preview)   | C# hook compilation                |
| MSVC / GCC      | VS 2022 / GCC 9+ | Native CLR profiler DLL/SO        |
| Rust            | stable            | Console TUI installer             |
| Root Communications | desktop app   | Runtime testing target            |
| Python          | 3.8+             | Log analysis script (optional)     |

### C# Hook Setup

The hook is a .NET 10 class library with no NuGet dependencies. All Avalonia
access is done through runtime reflection.

```bash
# Verify .NET SDK version
dotnet --version   # Should report 10.x.x

# Build the hook
dotnet build hook/UprootedHook.csproj -c Release

# Output appears at:
#   hook/bin/Release/net10.0/UprootedHook.dll
#   hook/bin/Release/net10.0/UprootedHook.deps.json
```

Use `-c Debug` during development for debug symbols. Always use `-c Release`
for deployment and when building the installer.

### TypeScript Setup

The TypeScript layer builds with esbuild into a single IIFE bundle that gets
injected into Root's embedded Chromium via a `<script>` tag.

```bash
# Install dependencies
pnpm install

# One-time build
pnpm build

# Watch mode for active development (incremental rebuilds on file change)
pnpm dev

# Output appears at:
#   dist/uprooted-preload.js
#   dist/uprooted-preload.js.map
#   dist/uprooted.css
```

### Rust Installer Setup

The installer is a console TUI application built with ratatui and crossterm.

```bash
# Install Rust stable toolchain
rustup install stable

# Build the installer
cd installer/src-tauri && cargo build --release
```

No system packages needed (no WebKitGTK dependency).

### Native Profiler Setup

The CLR profiler is a C shared library that implements `ICorProfilerCallback`.
It loads the managed hook DLL into Root's CLR at runtime.

**Windows (MSVC):**

```bash
# From a Visual Studio Developer Command Prompt, or after running:
#   vcvarsall.bat x64

cl.exe /LD /O2 ^
  /Fe:uprooted_profiler.dll ^
  tools/uprooted_profiler.c ^
  /link ole32.lib kernel32.lib shell32.lib ^
  /DEF:tools/uprooted_profiler.def
```

**Linux (GCC):**

```bash
gcc -shared -fPIC -O2 \
  -o libuprooted_profiler.so \
  tools/uprooted_profiler_linux.c
```

The `scripts/build-installer.ps1` script locates MSVC automatically via
`vswhere.exe` on Windows, so you rarely need to compile the profiler manually.

---

## Project Layout Orientation

```
uprooted-private/
  hook/                         # C# .NET hook (the core injection layer)
    StartupHook.cs              #   Multi-phase startup (Phase 0-5)
    AvaloniaReflection.cs       #   Reflection cache for ~80 Avalonia types
    SidebarInjector.cs          #   Timer-based sidebar injection (200ms poll)
    ContentPages.cs             #   Settings page UI builders
    HtmlPatchVerifier.cs        #   Self-healing HTML patches + FileSystemWatcher
    ThemeEngine.cs              #   Native Avalonia theme engine
    ColorUtils.cs               #   HSL/HSV/RGB color math
    ColorPickerPopup.cs         #   HSV color picker UI
    VisualTreeWalker.cs         #   Visual tree DFS traversal
    PlatformPaths.cs            #   Platform-specific path resolution
    UprootedSettings.cs         #   INI-based settings persistence
    Logger.cs                   #   Thread-safe file logging
    Entry.cs                    #   Profiler injection entry point ([ModuleInitializer])
    NativeEntry.cs              #   Alternative entry via hostfxr

  src/                          # TypeScript browser injection (from public repo)
    core/                       #   Preload, plugin loader, settings
    api/                        #   Bridge proxy, CSS injection, DOM utilities
    plugins/                    #   Built-in plugins (themes, sentry-blocker, etc.)

  installer/                    # Console TUI installer (Rust)
    src-tauri/                  #   Rust source (detection, patching, deployment, TUI)
      src/embedded.rs           #   Artifact embedding via include_bytes!()
      artifacts/                #   Staging directory for build artifacts

  tools/                        # Native profiler and build utilities
    uprooted_profiler.c         #   CLR profiler (Windows)
    uprooted_profiler_linux.c   #   CLR profiler (Linux)
    uprooted_profiler.def       #   DLL export definitions

  scripts/                      # Build, install, and diagnostic automation
  tests/UprootedTests/          # C# unit tests (ColorUtils, GradientBrush)
  hook-test/                    # Manual C# hook test harness
  dist/                         # Prebuilt TypeScript bundle
  docs/                         # Documentation
```

Key relationships: The native profiler (`tools/`) loads the C# hook (`hook/`)
into Root's CLR. The hook patches Root's HTML to inject the TypeScript bundle
(`dist/`). The installer (`installer/`) embeds all three layers into a single
binary via `include_bytes!()`.

---

## Build Verification

> For the full build pipeline with prerequisites and advanced options, see [BUILD.md](../install/BUILD.md).

After setting up your environment, verify each layer builds cleanly.

### TypeScript

```bash
pnpm build
```

Verify that these files exist and are non-empty:
- `dist/uprooted-preload.js`
- `dist/uprooted.css`

### C# Hook

```bash
dotnet build hook/UprootedHook.csproj -c Release
```

Verify that these files exist:
- `hook/bin/Release/net10.0/UprootedHook.dll`
- `hook/bin/Release/net10.0/UprootedHook.deps.json`

### C# Tests

```bash
dotnet test tests/UprootedTests/
```

All 170 tests should pass. Covers `ColorUtils`, `GradientBrush`, `ClearUrlsEngine`, `UprootedSettings`, and `MessageStore`.

### Full Pipeline (Windows)

```powershell
powershell -File scripts/build-installer.ps1
```

This builds all layers, stages artifacts, and produces the final `uprooted.exe`
installer. It will report errors if any stage fails and verify that all
required artifacts are present before starting the Rust build.

---

## Testing

### Automated Tests

**C# unit tests** in `tests/UprootedTests/` — 170 tests, all passing:

| Test File                     | Covers                                                                    |
| ----------------------------- | ------------------------------------------------------------------------- |
| `ColorUtilsTests.cs`         | HSL/HSV/RGB color conversion math                                         |
| `GradientBrushTests.cs`      | Gradient brush creation and parsing (Avalonia types via reflection)        |
| `ClearUrlsEngineTests.cs`    | `CleanUrl` static method: all 33 tracking params, fragments, case, edge cases |
| `UprootedSettingsTests.cs`   | INI parsing, type coercion, cache invalidation, save/load roundtrip, migration |
| `MessageStoreTests.cs`       | Flat-file persistence: records, URI encoding, malformed tolerance, Truncate |

Run with:

```bash
dotnet test tests/UprootedTests/
```

The test project links source files directly from `hook/` (no separate build) and
provides stubs for `Logger`, `PlatformPaths`, `AvaloniaReflection`, and `VisualTreeWalker`
in `tests/UprootedTests/TestStubs/` to eliminate all disk I/O and Avalonia runtime
dependencies during testing. `UprootedSettingsTests` and `MessageStoreTests` run in a
sequential xunit collection (`[Collection("SequentialTests")]`) to prevent interference
through shared static state.

For detailed coverage analysis and design notes, see [`docs/dev/TESTING.md`](TESTING.md).

#### Docker test sandbox

Run all tests in an isolated Linux container (matches CI environment):

```bash
bash tests/run-docker-tests.sh
```

This builds `tests/Dockerfile.unittest` (SDK 10.0 base), runs all tests with
XPlat code coverage, and extracts the coverage XML to `tests/coverage/`.

#### Linux installer sandbox

Test the full `install-uprooted-linux.sh` flow in an Ubuntu 24.04 container:

```bash
# Build and run the installer Docker test (requires Docker):
docker build -f tests/docker-installer/Dockerfile -t uprooted-installer-test .
```

The container creates a fake Root.AppImage + profile HTML, intercepts the GitHub
artifact download via a curl shim, runs the installer, then runs `verify.sh` which
checks 14 post-install conditions. Build fails if any verification step fails.

**TypeScript tests:** No automated test suite exists for the TypeScript layer.
The recommended framework for future tests is Vitest (native ESM support, fast
execution, minimal configuration).

### Manual Test Harness

The `hook-test/` directory contains a standalone .NET project for testing the
hook DLL outside of Root's process. This is not an automated suite -- it is a
manual verification tool for the reflection-based injection logic.

```bash
# From hook-test directory on Windows:
test_minimal.cmd
```

### Manual Testing Workflow

The primary testing method is deploying to a local Root installation and
checking logs:

```bash
# 1. Build what you changed
pnpm build                          # TypeScript changes
dotnet build hook/UprootedHook.csproj -c Release       # C# hook changes

# 2. Deploy to Root
powershell -File scripts/install-hook.ps1

# 3. Launch Root and test
#    The install script offers to launch at the end

# 4. Check hook log for errors
#    Windows: %LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log
#    Use: powershell -File scripts/watch-log.ps1
```

### What Is Not Tested

The following areas lack automated coverage and require manual verification:

- Plugin lifecycle management (start, stop, handler cleanup)
- Bridge interception (ES6 Proxy wrapping of Root's bridge objects)
- Settings persistence and corruption recovery
- Avalonia visual tree injection (depends on Root's actual runtime UI)
- Concurrent injection/cleanup race conditions
- HTML patch self-healing after Root auto-update

---

## Debugging the C# Hook

### Log File

The hook writes timestamped, categorized logs to `uprooted-hook.log`. The
`Logger` class (`hook/Logger.cs`) is thread-safe and swallows its own
exceptions to avoid crashing Root:

```csharp
// Logger.Log writes: [HH:mm:ss.fff] message
Logger.Log("Startup", "Phase 1 OK: Avalonia assemblies loaded");
// Produces: [14:23:01.456] [Startup] Phase 1 OK: Avalonia assemblies loaded
```

The log file is located at:
- **Windows:** `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log`
- **Linux:** `~/.local/share/Root Communications/Root/profile/default/uprooted-hook.log`

### Reading Logs in Real Time

Use the `watch-log.ps1` script for color-coded, filterable live tailing:

```powershell
# Default: show last 50 lines, then follow
powershell -File scripts/watch-log.ps1

# Filter to specific category
powershell -File scripts/watch-log.ps1 -filter "Injector"

# Errors only
powershell -File scripts/watch-log.ps1 -errors

# Hide noisy diagnostic lines
powershell -File scripts/watch-log.ps1 -noDiag
```

Color coding in watch-log.ps1:
- Cyan: `[Startup]` messages
- Green: `[Injector]` messages
- Yellow: `[TreeWalker]` messages
- Red: errors, failures, crashes
- Magenta: `[Diag]` and `[Style]` messages

### Post-Mortem Log Analysis

For analyzing logs after a session (crashes, timing issues, visual tree
problems), use the Python analyzer:

```bash
python scripts/analyze-log.py [path-to-logfile]
```

This generates a structured report with:
- Startup timeline with phase completion times
- Injection events and state changes
- Visual tree structure from diagnostic dumps
- ListBox items and their styles
- Back button / freeze analysis
- All errors and warnings, with line numbers
- Category summary and key measurements

### Log Patterns to Look For

**Healthy startup:**
```
[Startup] === Uprooted Hook v0.2.2 Loaded ===
[Startup] Phase 0 OK: 0 file(s) repaired
[Startup] Phase 1 OK: Avalonia assemblies loaded
[Startup] Phase 2 OK: Application.Current is set
[Startup] Phase 3 OK: MainWindow = ...
[Startup] Uprooted Hook Ready
```

**Phase failure (stall):**
```
[Startup] Phase 1 FAILED: Avalonia assemblies not found after 30s
```
This means the hook loaded but Root's Avalonia assemblies were not available
within the timeout. Check that Root.exe is actually launching.

**Injection failure:**
```
[Injector] Settings page detected but sidebar ListBox not found
```
The sidebar injector found the settings page but could not locate the expected
ListBox control in the visual tree. Root may have updated its UI structure.

### Attaching a Debugger

To attach a .NET debugger to the hook running inside Root:

1. Launch Root with the hook loaded (via `test-hook.ps1` or the installer).
2. In Visual Studio or JetBrains Rider, use "Attach to Process" and select
   the Root.exe process.
3. Set breakpoints in the hook source files.
4. The hook runs on a background thread named `Uprooted-Injector`, but UI
   operations dispatch to Avalonia's UI thread.

Note: The profiler disables ReadyToRun (`DOTNET_ReadyToRun=0`), which means
all methods are JIT-compiled. This makes debugging easier but startup is
slightly slower.

---

## Debugging TypeScript

### Console Logging

All TypeScript-side log messages are prefixed with `[Uprooted]` or
`[Uprooted:plugin-name]`:

```
[Uprooted] Started plugin: sentry-blocker
[Uprooted:sentry-blocker] Blocked fetch to sentry.io (3 total)
```

Fatal errors display an on-screen red banner at the top of the page with the
full error stack trace, so you can see failures even without DevTools open.

### DevTools Access

Root uses DotNetBrowser (Chromium 144) for its web content. If DevTools are
accessible, you can use the standard Chrome DevTools console, network tab, and
elements panel to inspect the TypeScript layer.

Look for:
- `window.Uprooted` -- the global IIFE export containing the plugin API
- `window.__UPROOTED_VERSION__` -- the build-time version string
- Style elements with IDs prefixed `uprooted-css-` -- injected CSS

### Source Maps

The build produces `dist/uprooted-preload.js.map` alongside the bundle. If
source maps are deployed, you can set breakpoints in the original TypeScript
source files through DevTools.

### Common TypeScript Debug Steps

1. Open DevTools console and check for `[Uprooted]` messages.
2. Verify `window.Uprooted` exists (if not, the script tag injection failed).
3. Check the Network tab for blocked requests (sentry-blocker plugin).
4. Inspect CSS variables on `:root` to verify theme application.
5. Look for error banners at the top of the page.

---

## Common Failure Modes

| Symptom | Likely Cause | Fix |
| ------- | ------------ | --- |
| Hook does not load at all | Profiler environment variables not set, or wrong `DOTNET_PROFILER_PATH` | Run `scripts/diagnose.ps1` to check env vars. Verify `DOTNET_ENABLE_PROFILING=1`, `DOTNET_PROFILER={D1A6F5A0-1234-4567-89AB-CDEF01234567}`, and `DOTNET_PROFILER_PATH` points to an existing `uprooted_profiler.dll`. Legacy `CORECLR_` prefix vars should also be set for .NET 8/9 compatibility. |
| UI freeze when navigating away from settings | Direct modification of `ContentControl.Content` | Never set `ContentControl.Content` directly. Use Grid overlay pattern instead. See Architecture doc, Critical Rules section. |
| `MissingMethodException` at runtime | Using `System.Text.Json` in hook code | `System.Text.Json` fails in the profiler context. Use the INI-based `UprootedSettings` class for all serialization. Parse manually if needed. |
| Sidebar items do not appear | Timing issue -- settings page not fully loaded when injector runs | Check log for `[Injector]` messages. The `SidebarInjector` uses a 200ms timer poll. If Root's UI takes longer to render, the injector may miss its window. Restart Root and reopen Settings. |
| Theme does not apply (native) | Resource dictionary priority -- Root's styles override injected ones | Theme resources must be inserted at the correct index in the resource dictionary chain. Check `[Theme]` log entries for insertion errors. |
| Theme does not apply (CSS) | CSS variables not injected, or wrong specificity | Check DevTools for `uprooted-css-*` style elements. Verify `:root` has the expected CSS custom properties. |
| HTML patches lost after Root update | Root's auto-updater (Velopack) overwrites patched HTML files | Phase 0 (`HtmlPatchVerifier`) should self-heal on startup. The `FileSystemWatcher` catches overwrites at runtime. Check log for `Phase 0` entries. If patches are missing, rerun the installer. |
| Hook loads but crashes immediately | .NET version mismatch or missing assembly | Check `uprooted-hook.log` for the first few lines. Verify .NET 10 SDK is installed. Run `dotnet --info` to check runtime version. |
| `RoutedEvent` handler not firing | Used `EventInfo.AddEventHandler` for Avalonia RoutedEvents | Use Expression lambda compilation instead. See Critical Rules in CONTRIBUTING.md. |
| Settings not persisting | `System.Text.Json` workaround -- settings use INI format | Verify `uprooted-settings.ini` exists and is writable. Check `[Settings]` log entries for load/save errors. |
| Profiler loads but hook DLL not found | `UprootedHook.dll` not deployed to expected location | Windows: check `%LOCALAPPDATA%\Root\uprooted\UprootedHook.dll`. Linux: check `~/.local/share/uprooted/UprootedHook.dll`. |

---

## Making Changes Safely

### C# Hook Changes

1. **Always build before deploying.** Run `dotnet build hook/UprootedHook.csproj -c Release` and
   check for compiler warnings, not just errors.

2. **Never throw exceptions from injected code.** Wrap everything in
   try-catch and log the error. An unhandled exception in the hook can crash
   Root.

3. **Use `AvaloniaReflection` for all type access.** Never use `typeof()` or
   `Type.GetType()` for Avalonia types. The reflection cache handles assembly
   resolution correctly.

4. **Test with `test-hook.ps1` before committing.** This launches Root with
   the profiler attached and monitors the log for the "Hook Ready" message.

5. **Check for threading issues.** UI operations must run on Avalonia's
   dispatcher thread. Use `resolver.RunOnUIThread()` for any code that
   touches Avalonia controls.

6. **Run the unit tests.** `dotnet test tests/UprootedTests/` -- they should
   all pass after your changes.

### TypeScript Changes

1. **Run `pnpm build` and verify no errors.** The esbuild process will fail
   on import resolution errors.

2. **Check that `dist/uprooted-preload.js` is regenerated.** Stale builds
   are a common source of confusion.

3. **Prefix all console logs with `[Uprooted]`.** This makes it easy to
   filter Uprooted messages from Root's own console output.

4. **Do not use `localStorage`.** Root runs Chromium with `--incognito`,
   which clears localStorage on exit. Use the settings file instead.

5. **Test plugin start and stop.** If you add a plugin, verify it cleans up
   properly when stopped (remove CSS, disconnect observers, restore patched
   functions).

### Native Profiler Changes

1. **This is the most sensitive layer.** A bug in the profiler can prevent
   Root from launching entirely or cause hard crashes with no log output.

2. **Test on a clean Root installation** before pushing changes to the
   profiler source.

3. **Verify the .def file exports.** The CLR requires `DllGetClassObject`
   and `DllCanUnloadNow` to be exported with the correct names.

### Installer Changes

1. **All artifacts must be staged** before `cargo build --release`. Missing
   artifacts cause a compile-time error because `include_bytes!()` is
   resolved at build time.

2. **Test install and uninstall flows.** The installer modifies shortcuts,
   registry entries, and environment variables. Verify that `uninstall`
   cleanly reverses everything.

3. **Run `cargo clippy`** in `installer/src-tauri/` to catch Rust linting
   issues.

---

## Verification Checklist

Run through this before submitting a pull request.

### Build

- [ ] `pnpm build` completes without errors
- [ ] `dotnet build hook/UprootedHook.csproj -c Release` completes without errors or warnings
- [ ] `dotnet test tests/UprootedTests/` -- all tests pass
- [ ] `dist/uprooted-preload.js` and `dist/uprooted.css` exist and are non-empty

### Code Quality

> These checks enforce the [Critical Rules from ARCHITECTURE.md](../framework/ARCHITECTURE.md#9-critical-rules).

- [ ] No use of `Type.GetType()` for Avalonia types (use `AvaloniaReflection`)
- [ ] No use of `System.Text.Json` in hook code
- [ ] No direct `ContentControl.Content` modification
- [ ] No `localStorage` usage in TypeScript
- [ ] No unhandled exceptions in injected C# code (all try-catch with logging)
- [ ] All log messages use category prefix (`[Category]` in C#, `[Uprooted]` in TS)
- [ ] Commit message follows `type: description` format

### Deployment Test (if applicable)

- [ ] Deployed to local Root installation via `install-hook.ps1`
- [ ] Root launches without crash
- [ ] `uprooted-hook.log` shows clean startup through all phases
- [ ] Settings page opens without UI freeze
- [ ] Sidebar items appear and are clickable
- [ ] Navigating away from settings does not freeze the UI
- [ ] TypeScript console shows `[Uprooted]` startup messages (if DevTools accessible)

---

## Scripts Reference

All scripts live in the `scripts/` directory. PowerShell scripts (`.ps1`) are
Windows-specific unless noted.

**Encoding rule:** PowerShell 5.1 (the default on Windows) interprets `.ps1` files as Windows-1252 unless they have a UTF-8 BOM. If a script contains non-ASCII characters (em-dashes, accented letters, etc.), PowerShell will misparse them as broken strings and fail with cryptic `TerminatorExpectedAtEndOfString` errors. Either keep scripts ASCII-only or add a UTF-8 BOM (`EF BB BF`).

### Build Scripts

| Script                   | Language   | Purpose                                           |
| ------------------------ | ---------- | ------------------------------------------------- |
| `build-installer.ps1`   | PowerShell | Full pipeline: build all layers, stage artifacts, produce installer binary |
| `build.ts`              | TypeScript | esbuild bundler for TypeScript layer (called by `pnpm build`) |

### Install / Uninstall Scripts

| Script                   | Language   | Purpose                                           |
| ------------------------ | ---------- | ------------------------------------------------- |
| `install-hook.ps1`      | PowerShell | First-time setup: deploy artifacts, patch shortcuts, set env vars, patch protocol handler |
| `uninstall-hook.ps1`    | PowerShell | Reverse everything `install-hook.ps1` did; restore shortcuts and registry |
| `deploy-hook.ps1`       | PowerShell | Hot-deploy DLL only: stop Root + chromium, copy artifacts, relaunch. Day-to-day dev loop on Windows |
| `install.ts`            | TypeScript | Lightweight HTML-only patching (no native hook); called by `pnpm install-root` |
| `uninstall.ts`          | TypeScript | Reverse HTML patches; called by `pnpm uninstall-root` |

### Diagnostic Scripts

| Script                   | Language   | Purpose                                           |
| ------------------------ | ---------- | ------------------------------------------------- |
| `diagnose.ps1`          | PowerShell | Inspect Root installation: shortcuts, taskbar pins, velopack log, running processes, CLR profiler env vars (process and registry) |
| `verify-install.ps1`    | PowerShell | Quick check of shortcut targets, protocol handler, and settings file contents |
| `watch-log.ps1`         | PowerShell | Real-time color-coded log tailing with filtering (`-filter`, `-errors`, `-noDiag`) |
| `analyze-log.py`        | Python     | Post-mortem log analysis: startup timeline, injection events, visual tree dumps, errors, measurements |
| `poll-log.ps1`          | PowerShell | Poll log until "VERSION BOX RECON" appears (used during style development) |
| `poll-style-recon.ps1`  | PowerShell | Poll log until "END STYLE RECON" appears (used during style matching) |

### Testing Scripts

| Script                   | Language   | Purpose                                           |
| ------------------------ | ---------- | ------------------------------------------------- |
| `test-hook.ps1`         | PowerShell | Launch Root with profiler env vars set, monitor log for "Hook Ready", report crashes |
| `test_sandbox.wsb`      | WSB config | Windows Sandbox configuration for isolated testing |

### Usage Examples

```powershell
# Deploy and test in one flow
dotnet build hook/UprootedHook.csproj -c Release
powershell -File scripts/install-hook.ps1
powershell -File scripts/test-hook.ps1

# Watch the log while testing
powershell -File scripts/watch-log.ps1 -filter "Injector"

# After a crash, analyze the log
python scripts/analyze-log.py

# Check if installation is healthy
powershell -File scripts/diagnose.ps1
powershell -File scripts/verify-install.ps1
```

---

## Log File Locations

### C# Hook Log

The primary log file for the hook layer. Written by `Logger.cs` with
timestamps and category tags.

| Platform | Path |
| -------- | ---- |
| Windows  | `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log` |
| Linux    | `~/.local/share/Root Communications/Root/profile/default/uprooted-hook.log` |

This path is determined by `PlatformPaths.GetProfileDir()`, which resolves
`Environment.SpecialFolder.LocalApplicationData` and appends
`Root Communications/Root/profile/default`.

The log is created fresh when the hook loads (it appends, so multiple Root
sessions accumulate in the same file). Delete it before a test run if you want
a clean log.

### Settings File

| Platform | Path |
| -------- | ---- |
| Windows  | `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-settings.ini` |
| Linux    | `~/.local/share/Root Communications/Root/profile/default/uprooted-settings.ini` |

### Deployed Artifacts

| Platform | Path |
| -------- | ---- |
| Windows  | `%LOCALAPPDATA%\Root\uprooted\` |
| Linux    | `~/.local/share/uprooted/` |

This directory (from `PlatformPaths.GetUprootedDir()`) contains the deployed
`uprooted_profiler.dll` (or `.so`), `UprootedHook.dll`, and
`UprootedLauncher.exe`.

### Velopack Log

Root's auto-updater log (useful for diagnosing patch loss):

| Platform | Path |
| -------- | ---- |
| Windows  | `%LOCALAPPDATA%\Root\velopack.log` |

---

## Settings File

### Format

Uprooted uses a plain-text INI format for settings, not JSON. This is a
deliberate choice: `System.Text.Json` throws `MissingMethodException` in the
CLR profiler context, so all serialization is done manually.

The settings file is `uprooted-settings.ini`, located in the profile directory
(see [Log File Locations](#log-file-locations) above).

### Example Contents

```ini
ActiveTheme=default-dark
Enabled=true
Version=0.2.2
CustomCss=
CustomAccent=#3B6AF8
CustomBackground=#0D1521
Plugin.sentry-blocker=true
Plugin.themes=true
```

### How It Works

`UprootedSettings.cs` manages loading and saving. Key behaviors:

- **Load:** Reads each line, splits on the first `=`, and matches the key
  name to a property. Unrecognized keys are ignored. Plugin states are stored
  as `Plugin.<name>=true|false`.

- **Save:** Writes all properties as `Key=Value` lines, one per line. Plugin
  entries are appended with the `Plugin.` prefix.

- **Defaults:** If the file does not exist or cannot be read, defaults are
  used: `Enabled=true`, `ActiveTheme=default-dark`, `CustomAccent=#3B6AF8`,
  `CustomBackground=#0D1521`.

- **Error handling:** Both `Load()` and `Save()` catch all exceptions and log
  them. A failed load returns defaults. A failed save logs the error and
  continues (the application does not crash).

- **Path resolution:** Uses `PlatformPaths.GetProfileDir()` to find the
  settings directory. Falls back to the current working directory if path
  resolution fails.

### Modifying Settings

To change settings programmatically from the hook:

```csharp
var settings = UprootedSettings.Load();
settings.ActiveTheme = "my-theme";
settings.Plugins["my-plugin"] = true;
settings.Save();
```

To change settings manually, edit the INI file directly while Root is not
running. The hook reads settings once during Phase 3.5 of startup.

---

**Canonical for:** dev environment setup, project layout orientation, debugging workflows (both layers), failure mode table, scripts reference, verification checklists, log file locations
**Not canonical for:** build pipeline → [BUILD.md](../install/BUILD.md) | branch/PR rules → [CONTRIBUTING.md](../../CONTRIBUTING.md) | critical rules (full) → [ARCHITECTURE.md §9](../framework/ARCHITECTURE.md#9-critical-rules) | architecture → [ARCHITECTURE.md](../framework/ARCHITECTURE.md)
*Technical onboarding guide for Uprooted. Last updated 2026-02-19.*
