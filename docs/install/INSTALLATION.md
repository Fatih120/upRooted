# Installation Guide

> **What this is:** End-user install/uninstall procedures — Windows and Linux, console installer and manual methods, troubleshooting, file locations, verification.
> **Read when:** Installing or uninstalling Uprooted; diagnosing install issues; checking file deployment locations.
> **Skip if:** You need to build from source → [BUILD.md](BUILD.md). You need installer internals → [INSTALLER.md](../framework/INSTALLER.md).
> **Does NOT cover:** Building from source → [BUILD.md](BUILD.md) | Installer code reference → [INSTALLER.md](../framework/INSTALLER.md) | Architecture → [ARCHITECTURE.md](../framework/ARCHITECTURE.md)

Step-by-step instructions for installing, verifying, repairing, and uninstalling Uprooted on Windows and Linux.

> **Related docs:** [Index](../INDEX.md) | [Build Guide](BUILD.md) | [Installer Reference](../framework/INSTALLER.md) | [Architecture](../framework/ARCHITECTURE.md)

---

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Quick Install (Console Installer)](#quick-install-console-installer)
3. [PowerShell Install (Manual Windows)](#powershell-install-manual-windows)
4. [Linux Install](#linux-install)
5. [Arch Linux](#arch-linux)
6. [Verification Steps](#verification-steps)
7. [Uninstallation](#uninstallation)
8. [Repair (After Root Updates)](#repair-after-root-updates)
9. [Troubleshooting](#troubleshooting)
10. [File Locations Table](#file-locations-table)

---

## Prerequisites

Before installing Uprooted, make sure you have the following:

- **Operating system:** Windows 10 or later, or a Linux distribution with a graphical desktop
- **Root Communications desktop app** installed and **logged in at least once** --
  Uprooted patches HTML files inside Root's profile directory, which is only created
  after the first successful login.
- **.NET 10 SDK** -- only required if you are building from source. The prebuilt
  installer bundles all necessary artifacts. See [Build Guide](BUILD.md) for details.

**Important:** Uprooted does **not** require administrator or root privileges.
On Windows, all environment variables are set at user scope (`HKCU\Environment`).
On Linux, everything lives under `~/.local/share/uprooted/`.

---

## Quick Install (Console Installer)

This is the recommended method for most users. The console TUI installer (ratatui-based,
btop-inspired interface) handles detection, file deployment, environment variable
configuration, and HTML patching automatically. It is a single portable binary (~600KB)
with no external dependencies.

### Steps

1. **Download the installer** from the
   [latest release](https://github.com/The-Uprooted-Project/uprooted/releases/latest).
   - Windows: `Uprooted-<version>-Setup.exe`
   - Linux: use the [bash installer](#linux-install) instead (see below)

2. **Close Root** if it is currently running. The installer will warn you if Root is
   still open.

3. **Run the installer** from a terminal:
   ```bash
   # Windows (PowerShell or Command Prompt)
   .\Uprooted-<version>-Setup.exe
   ```
   The TUI will guide you through the installation process. It will:
   - Auto-detect Root's installation path
     - Windows: `%LOCALAPPDATA%\Root\current\Root.exe`
     - Linux: 5-strategy search — well-known paths, glob for versioned filenames,
       `.desktop` file parsing, running process detection via `/proc`, and PATH lookup
   - Deploy five files to the Uprooted install directory (see
     [File Locations Table](#file-locations-table)):
     - `uprooted_profiler.dll` (Windows) or `libuprooted_profiler.so` (Linux)
     - `UprootedHook.dll`
     - `UprootedHook.deps.json`
     - `uprooted-preload.js`
     - `uprooted.css`
   - Set CLR profiler environment variables (user-scoped, using both `DOTNET_` and
     `CORECLR_` prefixes for compatibility with .NET 10+ and .NET 8/9)
   - Patch Root's HTML files with `<script>` and `<link>` tags for the TypeScript layer

4. **Follow the TUI prompts** and wait for the process to complete.

5. **Restart Root.** Open Root normally -- Uprooted will load automatically via
   the CLR profiler hook.

6. **Verify:** Open Root's Settings page. You should see an "UPROOTED" section in the
   sidebar. See [Verification Steps](#verification-steps) for more details.

### CLI Flags

The installer supports the following command-line flags for non-interactive use:

| Flag | Description |
|------|-------------|
| `--uninstall` | Remove Uprooted (files, env vars, HTML patches) |
| `--repair` | Re-deploy files and re-patch HTML without changing settings |
| `--diagnose` | Run installation diagnostics and print a report |
| `--plain` | Disable the TUI and use plain text output (for scripts/CI) |

---

## PowerShell Install (Manual Windows)

Use this method if you prefer command-line installation, want to build from source,
or need to choose between injection methods.

### Usage

```powershell
# Default: Profiler method (recommended)
.\Install-Uprooted.ps1

# Alternative: Startup Hooks method
.\Install-Uprooted.ps1 -Method StartupHooks
```

### Injection Methods

Uprooted supports two injection methods. Both accomplish the same result -- loading
`UprootedHook.dll` into the Root process -- but they work differently:

**Profiler method (default):**

- Uses a native CLR profiler DLL (`uprooted_profiler.dll`) for IL injection.
- The profiler injects IL that calls `Assembly.LoadFrom` + `Assembly.CreateInstance`
  to bootstrap the hook.
- Does **not** patch `Root.exe` -- no binary modification needed.
- Survives Root updates without re-patching the executable.
- Downside: the profiler environment variables are visible to all .NET applications
  on your system. The profiler DLL has a built-in process guard that only activates
  for `Root.exe`, so other apps are unaffected in practice.

**Startup Hooks method:**

- Uses the standard .NET `DOTNET_STARTUP_HOOKS` environment variable.
- Requires patching `Root.exe` to flip the embedded
  `System.StartupHookProvider.IsSupported` flag from `false` to `true` (Root ships
  with startup hooks disabled).
- Cleaner environment -- only one env var (`DOTNET_STARTUP_HOOKS`) is set.
- Downside: Root updates overwrite `Root.exe`, so you must re-run the installer
  after each update.

### What the Script Does (Step by Step)

1. **Verifies Root.exe exists** at `%LOCALAPPDATA%\Root\current\Root.exe`.

2. **Checks if Root is running.** If so, prompts to close it.

3. **Method-specific setup:**
   - **Profiler:** Verifies `tools\uprooted_profiler.dll` exists in the repo.
   - **StartupHooks:** Binary-patches `Root.exe` to enable startup hooks. Creates
     a backup at `Root.exe.uprooted.bak` before patching. If the exe is already
     patched, it skips this step.

4. **Builds UprootedHook.dll** by running `dotnet build hook/UprootedHook.csproj -c Release`.
   Requires .NET 10 SDK.

5. **Copies files** to `%LOCALAPPDATA%\Root\uprooted\`:
   - `UprootedHook.dll`
   - `UprootedHook.deps.json`
   - `uprooted_profiler.dll` (Profiler method only)

6. **Sets persistent user-scoped environment variables:**

   **Profiler method sets (both prefixes for .NET 10+ and .NET 8/9 compatibility):**
   ```
   DOTNET_EnableDiagnostics    = 1
   DOTNET_ENABLE_PROFILING     = 1
   DOTNET_PROFILER             = {D1A6F5A0-1234-4567-89AB-CDEF01234567}
   DOTNET_PROFILER_PATH        = %LOCALAPPDATA%\Root\uprooted\uprooted_profiler.dll
   CORECLR_ENABLE_PROFILING    = 1
   CORECLR_PROFILER            = {D1A6F5A0-1234-4567-89AB-CDEF01234567}
   CORECLR_PROFILER_PATH       = %LOCALAPPDATA%\Root\uprooted\uprooted_profiler.dll
   DOTNET_ReadyToRun           = 0
   ```

   **StartupHooks method sets:**
   ```
   DOTNET_STARTUP_HOOKS = %LOCALAPPDATA%\Root\uprooted\UprootedHook.dll
   ```

   Each method cleans up environment variables from the other method to prevent
   conflicts.

7. **Done.** Launch Root to activate Uprooted.

### Note on Environment Variable Scope

The Profiler method sets `DOTNET_ENABLE_PROFILING=1` and `CORECLR_ENABLE_PROFILING=1`
at user scope, which means **every .NET application** you run will attempt to load the
profiler DLL. The profiler itself has a process guard (`Root.exe` only) and will
immediately detach from any non-Root process. This is harmless but worth knowing.
Both prefixes are set because .NET 10+ reads `DOTNET_` while .NET 8/9 reads `CORECLR_`.

If this concerns you, use the StartupHooks method instead, which only sets
`DOTNET_STARTUP_HOOKS`. See [Troubleshooting](#troubleshooting) for more details.

---

## Linux Install

The bash installer is the recommended method for Linux. It downloads pre-built
artifacts from GitHub: no build tools, compilers, or SDKs are required. The only
dependency is `curl` or `wget`.

### Usage

```bash
# Download and run (auto-detects Root.AppImage)
curl -LO https://raw.githubusercontent.com/The-Uprooted-Project/uprooted/main/install-uprooted-linux.sh
chmod +x install-uprooted-linux.sh
./install-uprooted-linux.sh

# Specify Root path manually
./install-uprooted-linux.sh --root-path /path/to/Root.AppImage
```

### CLI Flags

| Flag | Description |
|------|-------------|
| `--root-path PATH` | Manually specify Root AppImage location (skips auto-detection) |
| `--channel CH` | Release channel: `stable` (default), `canary`, `dev` (requires `GITHUB_TOKEN`) |
| `--local` | Deploy from local repo build output instead of downloading (dev use) |
| `--desktop` | Create a `.desktop` file for the app menu |
| `--repair` | Re-deploy artifacts and re-patch HTML |
| `--uninstall` | Remove Uprooted completely |
| `--diagnose` | Print installation health report |

### What the Script Does (Step by Step)

1. **Finds Root.AppImage.** Uses 7 strategies in order, returning the first match:
   1. Exact well-known paths (`~/Applications/Root.AppImage`, `~/Downloads/Root.AppImage`,
      `~/.local/bin/Root.AppImage`, `/opt/Root.AppImage`, `~/.local/bin/Root`)
   2. Glob for variant filenames (`Root*.AppImage`) in common directories
   3. `.desktop` file search in application directories (extracts `Exec=` path)
   4. Running process detection via `/proc/*/exe` symlinks
   5. PATH lookup via `command -v Root`
   6. `locate` database search (fast indexed, case-insensitive)
   7. Shallow `find` in `$HOME` (depth 4, last resort)

   Use `--root-path` to override auto-detection.

2. **Downloads pre-built artifacts** from the GitHub release matching the script's
   version and channel. The tarball contains all five deployment files.

3. **Deploys files** to `~/.local/share/uprooted/`:
   - `libuprooted_profiler.so` (with execute permission)
   - `UprootedHook.dll`
   - `UprootedHook.deps.json`
   - `uprooted-preload.js`
   - `uprooted.css`

4. **Creates a wrapper script** at `~/.local/share/uprooted/launch-root.sh` that
   exports the CLR profiler environment variables and then executes Root. The env
   vars are scoped to Root's process only: no global env vars are set by default.

5. **Patches HTML files (optional)** in Root's profile directory
   (`~/.local/share/Root Communications/Root/profile/default/`):
   - Looks for `index.html` in `WebRtcBundle/` and `RootApps/*/`
   - Injects `<script>` and `<link>` tags inside `<!-- uprooted:start -->` /
     `<!-- uprooted:end -->` markers before the `</head>` tag
   - Creates `.uprooted.bak` backup of each original file
   - **Note:** HTML patches only affect DotNetBrowser sub-apps (WebRTC, etc.).
     All core features (sidebar, settings, themes, chat plugins) are
     Avalonia-native and work without them. On first install, HTML files may
     not exist yet: this is normal and does not affect functionality.

6. **Kills and relaunches Root** via the wrapper script so Uprooted loads
   immediately.

### How to Launch After Install

The installer auto-launches Root after installation. For future sessions, just
launch Root normally: the wrapper script handles env vars internally.

Optionally, pass `--desktop` during install to create a "Root (Uprooted)" entry
in your application menu.

---

## Arch Linux

An AUR-style PKGBUILD is available at `packaging/arch/PKGBUILD` in the repository.

The version and checksum placeholders (`%%VERSION%%`, `%%SHA256%%`) are filled in by
the release CI pipeline. To build locally:

```bash
cd packaging/arch/
# Edit PKGBUILD to replace %%VERSION%% and %%SHA256%% with actual values
makepkg -si
```

A proper AUR submission (`uprooted-bin`) is planned for a future release. For now,
use the [bash installer](#linux-install). Uninstall with
`./install-uprooted-linux.sh --uninstall`.

---

## Verification Steps

After installation, follow these steps to confirm Uprooted is working correctly.

### 1. Check the Settings Sidebar

1. Open Root Communications.
2. Navigate to **Settings** (gear icon).
3. Look for the **UPROOTED** section heading in the left sidebar, below Root's
   built-in settings categories.
4. You should see navigation items such as "Uprooted", "Themes", and "Plugins"
   under the UPROOTED heading.

If the sidebar items appear, both the C# hook layer and the sidebar injector are
working.

### 2. Check the Log File

Uprooted writes detailed logs to:
- **Windows:** `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log`
- **Linux:** `~/.local/share/Root Communications/Root/profile/default/uprooted-hook.log`

Open the log file after launching Root. A successful startup looks like this:

```
[14:32:01.123] [Startup|init] version=0.4.2 process=Root.exe pid=12345 dotnet=10.0.0
[14:32:01.130] [Startup|phase] phase=0 action=verify_html_patches
[14:32:01.145] [HtmlPatch|check] files=2 ok=2 repaired=0 dur_ms=15
[14:32:01.146] [Startup|phase] phase=1 action=wait_avalonia
[14:32:01.850] [Startup|phase] phase=1 status=ok dur_ms=704
[14:32:01.851] [Startup|phase] phase=2 action=wait_application
[14:32:02.100] [Startup|phase] phase=2 status=ok dur_ms=249
[14:32:02.101] [Startup|phase] phase=3 action=wait_mainwindow
[14:32:02.500] [Startup|phase] phase=3 status=ok dur_ms=399
[14:32:02.501] [Startup|phase] phase=3.5 action=init_theme_engine
[14:32:02.650] [Startup|phase] phase=4 action=start_sidebar_monitor
[14:32:02.651] [Startup|ready] total_ms=1528
```

Each line uses the format `[HH:mm:ss.fff] [Category|operation] key=value key=value dur_ms=N`.
Some older log lines may use the simpler format `[HH:mm:ss.fff] [Category] message` --
both formats are valid and can appear in the same log file.

All phases should complete. If any phase shows `status=failed`, see
[Troubleshooting](#troubleshooting).

### 3. Check TypeScript Features

Open Root's developer tools (if accessible) or look for Uprooted-specific UI
changes (theme overrides, plugin effects). If the C# hook is working but themes
and plugins are not, the HTML files may not be patched. Check the log for
`[HtmlPatch]` messages and see
[Missing TypeScript features](#missing-typescript-features) in Troubleshooting.

---

## Uninstallation

### Console Installer

If you installed via the console TUI installer, run it again with the `--uninstall`
flag:

```bash
# Windows (console TUI installer)
.\Uprooted-<version>-Setup.exe --uninstall
```

The installer will:
- Remove all deployed files from the Uprooted install directory
- Remove all environment variables (both `DOTNET_` and `CORECLR_` profiler vars,
  plus startup hooks)
- Strip injected tags from HTML files (or restore from backups)

Restart Root to confirm it runs without Uprooted.

### PowerShell (Windows)

Run the uninstall script:

```powershell
.\Uninstall-Uprooted.ps1
```

The script performs these steps:

1. **Prompts to close Root** if it is running.

2. **Removes all environment variables** (user-scoped):
   - `DOTNET_EnableDiagnostics`
   - `DOTNET_ENABLE_PROFILING`
   - `DOTNET_PROFILER`
   - `DOTNET_PROFILER_PATH`
   - `CORECLR_ENABLE_PROFILING`
   - `CORECLR_PROFILER`
   - `CORECLR_PROFILER_PATH`
   - `DOTNET_ReadyToRun`
   - `DOTNET_STARTUP_HOOKS`

3. **Restores Root.exe from backup** if it was binary-patched (StartupHooks method).
   Deletes the `.uprooted.bak` backup file.

4. **Deletes the install directory** at `%LOCALAPPDATA%\Root\uprooted\`.

5. **Optionally removes log and settings files:**
   - `uprooted-hook.log`
   - `uprooted-settings.json`

   The script will ask before deleting these. Choose "n" to keep your logs for
   debugging or your settings for a future reinstall.

### Linux

Run the installer with the `--uninstall` flag:

```bash
./install-uprooted-linux.sh --uninstall
```

The script performs these steps:

1. **Strips HTML patches.** Removes all Uprooted-injected lines (markers, script
   tags, CSS links) from `index.html` files and deletes `.uprooted.bak` backups.

2. **Removes `~/.config/environment.d/uprooted.conf`** (legacy session-wide env vars).

3. **Removes KDE Plasma env script** (`~/.config/plasma-workspace/env/uprooted.sh`)
   if present.

4. **Cleans Uprooted env vars from `~/.profile`** (legacy fallback for non-systemd
   sessions).

5. **Removes `~/.local/share/applications/root-uprooted.desktop`** (menu entry).

6. **Removes `~/.local/share/uprooted/`** (all deployed files, including the
   wrapper script).

### Manual Cleanup

If the uninstall scripts are not available or something was missed, here is
everything Uprooted touches:

**Windows:**
```
# Files
%LOCALAPPDATA%\Root\uprooted\                              (entire directory)

# Environment variables (user-scoped, HKCU\Environment)
DOTNET_EnableDiagnostics
DOTNET_ENABLE_PROFILING
DOTNET_PROFILER
DOTNET_PROFILER_PATH
CORECLR_ENABLE_PROFILING
CORECLR_PROFILER
CORECLR_PROFILER_PATH
DOTNET_ReadyToRun
DOTNET_STARTUP_HOOKS

# Optional data files
%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log
%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-settings.json

# Root.exe backup (if StartupHooks method was used)
%LOCALAPPDATA%\Root\current\Root.exe.uprooted.bak
```

To remove Windows environment variables manually:
```powershell
[System.Environment]::SetEnvironmentVariable("DOTNET_EnableDiagnostics", $null, "User")
[System.Environment]::SetEnvironmentVariable("DOTNET_ENABLE_PROFILING", $null, "User")
[System.Environment]::SetEnvironmentVariable("DOTNET_PROFILER", $null, "User")
[System.Environment]::SetEnvironmentVariable("DOTNET_PROFILER_PATH", $null, "User")
[System.Environment]::SetEnvironmentVariable("CORECLR_ENABLE_PROFILING", $null, "User")
[System.Environment]::SetEnvironmentVariable("CORECLR_PROFILER", $null, "User")
[System.Environment]::SetEnvironmentVariable("CORECLR_PROFILER_PATH", $null, "User")
[System.Environment]::SetEnvironmentVariable("DOTNET_ReadyToRun", $null, "User")
[System.Environment]::SetEnvironmentVariable("DOTNET_STARTUP_HOOKS", $null, "User")
```

**Linux:**
```
# Files
~/.local/share/uprooted/                                   (entire directory, including launch-root.sh wrapper)
~/.config/environment.d/uprooted.conf                      (legacy env vars, if present)
~/.config/plasma-workspace/env/uprooted.sh                 (KDE env vars, if present)
~/.local/share/applications/root-uprooted.desktop           (menu entry, if created with --desktop)

# Optional data files
~/.local/share/Root Communications/Root/profile/default/uprooted-hook.log
~/.local/share/Root Communications/Root/profile/default/uprooted-settings.json
```

**Both platforms -- HTML file cleanup:**

If patched HTML files remain, they contain injected content between
`<!-- uprooted:start -->` and `<!-- uprooted:end -->` markers. You can:

- Delete the markers and everything between them manually, or
- Delete the `.uprooted.bak` backup files and let Root regenerate fresh HTML on
  the next launch, or
- Re-run the install script and then the uninstall script to clean up properly

---

## Repair (After Root Updates)

When Root Communications updates, it may overwrite its HTML files, removing the
Uprooted `<script>` and `<link>` tags. The C# hook itself is unaffected (it lives
outside Root's install directory). All core features (sidebar, settings, themes,
chat plugins) are Avalonia-native and continue working. Only the TypeScript browser
layer (DotNetBrowser sub-apps) is affected until HTML patches are restored.

### Automatic Repair

Uprooted has a built-in self-healing mechanism:

- **Phase 0 (startup):** Every time Root launches, the hook verifies all HTML files
  still contain the Uprooted markers. If any file is missing patches, it repairs
  them automatically before Avalonia even starts.

- **FileSystemWatcher (runtime):** After startup, the hook monitors `WebRtcBundle/`
  and `RootApps/*/` directories for changes to `index.html`. If Root overwrites an
  HTML file at runtime, the watcher detects it and re-patches within seconds.

This means in most cases, you do not need to do anything after a Root update.
Just restart Root and the hook will repair the patches itself.

### Manual Repair

If automatic repair does not trigger (e.g. the hook DLL was also updated), you have
these options:

**Console installer (Windows):**
```bash
.\Uprooted-<version>-Setup.exe --repair
```
This strips any old injection, re-deploys files, and re-patches HTML fresh.

**PowerShell (Windows):**
```powershell
# Re-run the install script (it is idempotent)
.\Install-Uprooted.ps1
```

**Linux:**
```bash
# Re-run the install script
./install-uprooted-linux.sh
```

### StartupHooks Method: Root.exe Re-patching

If you used the StartupHooks injection method, a Root update will also overwrite
`Root.exe`, disabling startup hooks again. Re-run the installer to re-patch:

```powershell
.\Install-Uprooted.ps1 -Method StartupHooks
```

The Profiler method does not have this problem because it does not modify `Root.exe`.

---

## Troubleshooting

### Root freezes on startup

**Cause:** The `ContentControl.Content` property was directly modified, which
triggers a layout cycle freeze in Avalonia.

**Fix:** This should not happen with the current version of Uprooted (it uses a Grid
overlay instead of modifying Content). If it does occur:

1. Kill Root from Task Manager or `pkill Root`.
2. Uninstall Uprooted to remove the hook.
3. Report the issue with your `uprooted-hook.log` file.

### Missing UPROOTED sidebar in Settings

**Possible causes:**

- **Avalonia version mismatch:** If Root updated to a new Avalonia version, the
  reflection cache may fail to resolve types. Check the log for:
  ```
  [Startup] Type resolution failed, aborting
  ```
  or
  ```
  [Startup] Phase 1 FAILED: Avalonia assemblies not found after 30s
  ```

- **Hook not loading:** Verify environment variables are set:
  ```powershell
  # Windows (PowerShell) -- check both prefixes
  [System.Environment]::GetEnvironmentVariable("DOTNET_ENABLE_PROFILING", "User")
  [System.Environment]::GetEnvironmentVariable("CORECLR_ENABLE_PROFILING", "User")
  # Both should output: 1
  ```
  ```bash
  # Linux (wrapper script is the source of truth for env vars)
  cat ~/.local/share/uprooted/launch-root.sh
  ```

- **Wrong profiler path:** Both `DOTNET_PROFILER_PATH` and `CORECLR_PROFILER_PATH`
  must point to the actual profiler DLL/SO file. Verify the file exists at the path
  shown in the environment variables.

### Missing TypeScript features

Themes, plugins, and other browser-side features require the HTML files to be
patched with Uprooted's `<script>` and `<link>` tags.

**Check if HTML files are patched:**

Open one of the target HTML files and look for `<!-- uprooted:start -->`:
- Windows: `%LOCALAPPDATA%\Root Communications\Root\profile\default\WebRtcBundle\index.html`
- Linux: `~/.local/share/Root Communications/Root/profile/default/WebRtcBundle/index.html`

If the markers are missing:
1. Make sure Root has been launched at least once (to create the profile directory).
2. Re-run the install script or use the console installer's `--repair` flag.
3. Check the log for `[HtmlPatch]` errors.

### Environment variable leakage

The Profiler method sets `DOTNET_ENABLE_PROFILING=1` and
`CORECLR_ENABLE_PROFILING=1` at user scope. This means every .NET application you
launch will attempt to load the profiler. The profiler has a process guard that
immediately detaches from any process that is not `Root.exe`, so there is no
functional impact.

However, if this concerns you:

- **Switch to the StartupHooks method** (`.\Install-Uprooted.ps1 -Method StartupHooks`),
  which only sets `DOTNET_STARTUP_HOOKS` and does not trigger profiler loading in
  other apps.
- **On Linux, no action needed:** the bash installer scopes env vars to Root's
  process via the wrapper script (`~/.local/share/uprooted/launch-root.sh`). No
  global env vars are set. If you have a legacy `~/.config/environment.d/uprooted.conf`
  from an older install, the `--uninstall` flag cleans it up.

### Hook loads but no log file

The log file is written to the Root profile directory. If the directory does not
exist, the logger creates it. If the log file is still missing:

- Verify the profile directory exists (launch Root once first).
- Check file permissions on the profile directory.
- On Linux, make sure the environment variables are actually set in your session
  (`env | grep -E 'DOTNET_|CORECLR_'`).

### Log file location

The log file is at:
- **Windows:** `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log`
- **Linux:** `~/.local/share/Root Communications/Root/profile/default/uprooted-hook.log`

The log uses structured wide events: `[HH:mm:ss.fff] [Category|operation] key=value ...`.
Some lines use the simpler legacy format: `[HH:mm:ss.fff] [Category] message`. Key categories:

| Category | What it covers |
|----------|----------------|
| `Startup` | Hook initialization, startup phases 0--4 |
| `HtmlPatch` | HTML file verification, patching, and FileSystemWatcher events |
| `Injector` | Sidebar injection into the Avalonia settings page |
| `Entry` | Profiler entry point (ModuleInitializer or constructor) |
| `Theme` | Theme engine operations (apply, revert, walker) |
| `LinkEmbed` | Link embed engine (OG fetch, embed injection) |
| `MsgLogger` | Message logger (edit/delete detection) |
| `Recon` | Style reconnaissance for matching native Avalonia UI |

High-frequency scan engines (LinkEmbed, MsgLogger, NsfwFilter, Theme walker) use tail
sampling -- instead of logging every scan tick, they emit a 30-second heartbeat summary
with totals. Look for lines like `[Category|heartbeat] scans=N hits=N errors=N`.

---

## File Locations Table

All paths Uprooted uses on each platform.

| File | Windows | Linux |
|------|---------|-------|
| **Hook DLL** | `%LOCALAPPDATA%\Root\uprooted\UprootedHook.dll` | `~/.local/share/uprooted/UprootedHook.dll` |
| **Hook deps** | `%LOCALAPPDATA%\Root\uprooted\UprootedHook.deps.json` | `~/.local/share/uprooted/UprootedHook.deps.json` |
| **Profiler DLL/SO** | `%LOCALAPPDATA%\Root\uprooted\uprooted_profiler.dll` | `~/.local/share/uprooted/libuprooted_profiler.so` |
| **TypeScript bundle** | `%LOCALAPPDATA%\Root\uprooted\uprooted-preload.js` | `~/.local/share/uprooted/uprooted-preload.js` |
| **Theme CSS** | `%LOCALAPPDATA%\Root\uprooted\uprooted.css` | `~/.local/share/uprooted/uprooted.css` |
| **Settings file** | `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-settings.json` | `~/.local/share/Root Communications/Root/profile/default/uprooted-settings.json` |
| **Log file** | `%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log` | `~/.local/share/Root Communications/Root/profile/default/uprooted-hook.log` |
| **Root HTML files** | `%LOCALAPPDATA%\Root Communications\Root\profile\default\WebRtcBundle\index.html` and `RootApps\*\index.html` | `~/.local/share/Root Communications/Root/profile/default/WebRtcBundle/index.html` and `RootApps/*/index.html` |
| **Root executable** | `%LOCALAPPDATA%\Root\current\Root.exe` | `~/Applications/Root.AppImage` (varies) |
| **Env var config** | User registry (`HKCU\Environment`) | `~/.local/share/uprooted/launch-root.sh` (wrapper script) |
| **Wrapper script** | N/A | `~/.local/share/uprooted/launch-root.sh` |
| **Desktop entry** | N/A | `~/.local/share/applications/root-uprooted.desktop` |
| **Root.exe backup** | `%LOCALAPPDATA%\Root\current\Root.exe.uprooted.bak` (StartupHooks only) | N/A |

---

See [Installer Reference](../framework/INSTALLER.md) for technical details on how the console
installer works internally. See [Build Guide](BUILD.md) for building all layers
from source.

---

**Canonical for:** install/uninstall procedures (Windows + Linux), console installer usage, manual PowerShell/bash install, troubleshooting, file locations, verification steps, Arch Linux instructions
**Not canonical for:** build from source → [BUILD.md](BUILD.md) | installer internals → [INSTALLER.md](../framework/INSTALLER.md) | runtime behavior → [HOOK_REFERENCE.md](../framework/HOOK_REFERENCE.md)
*Installation guide for Uprooted. Last updated 2026-02-24.*
