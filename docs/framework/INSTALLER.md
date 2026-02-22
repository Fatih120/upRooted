# Installer Reference

> **What this is:** Console TUI installer reference — Rust backend, detection, HTML patching, hook deployment, env var management, diagnostics, embedded artifacts.
> **Read when:** Modifying the installer; debugging install/uninstall issues; understanding how artifacts get deployed; adding new embedded files.
> **Skip if:** You need end-user install instructions → [INSTALLATION.md](../install/INSTALLATION.md). You need the build pipeline → [BUILD.md](../install/BUILD.md).
> **Does NOT cover:** End-user instructions → [INSTALLATION.md](../install/INSTALLATION.md) | Build pipeline → [BUILD.md](../install/BUILD.md) | What the hook does at runtime → [HOOK_REFERENCE.md](HOOK_REFERENCE.md)

> **Related docs:**
> [Architecture](ARCHITECTURE.md) |
> [Installation Guide](../install/INSTALLATION.md) |
> [Build Guide](../install/BUILD.md) |
> [Hook Reference](HOOK_REFERENCE.md)

---

## Overview

The Uprooted Installer is a lightweight console application (~600KB) that automates
every aspect of Uprooted lifecycle management: detection of the Root Communications
installation, full install/uninstall/repair of the mod framework, and verbose
diagnostics. It ships as a single Rust binary with all required artifacts compiled
directly into the binary via `include_bytes!()`, meaning there are no external
downloads, no temporary extractors, and no network dependencies at runtime.

The default mode renders a btop-inspired TUI with a centered bordered box, step
indicators, animated spinners, and color-coded checkmarks/crosses. A `--plain` flag
falls back to simple sequential ANSI output for scripts and CI environments.

**Version:** 0.4.2

**Stack:**

| Layer    | Technology                          | Location                    |
|----------|-------------------------------------|-----------------------------|
| TUI      | ratatui + crossterm                 | `installer/src-tauri/src/main.rs` (inline `tui` module) |
| CLI      | Plain ANSI output                   | `installer/src-tauri/src/cli.rs`  |
| Backend  | Rust (detection, patcher, hook)     | `installer/src-tauri/src/`  |
| Parser   | clap (derive)                       | `installer/src-tauri/src/main.rs` |

**Release profile** (anti-reverse-engineering):

| Setting         | Value      |
|-----------------|------------|
| `strip`         | `symbols`  |
| `lto`           | `true`     |
| `codegen-units` | `1`        |
| `panic`         | `abort`    |
| `opt-level`     | `z`        |

---

## CLI Interface

The installer is invoked as a single binary with optional flags parsed by clap:

```
uprooted [OPTIONS]

Options:
    --uninstall    Uninstall Uprooted (remove env vars, restore HTML, delete files)
    --repair       Repair installation (re-deploy files, re-patch HTML)
    --diagnose     Run verbose diagnostics (check files, env vars, patches)
    --plain        Plain ANSI output instead of TUI (for scripts / CI)
    -h, --help     Print help
    -V, --version  Print version
```

### Modes of Operation

| Invocation                  | Behavior                                              |
|-----------------------------|-------------------------------------------------------|
| `uprooted`                  | Interactive mode selector (Install / Uninstall / Repair) |
| `uprooted --plain`          | Install with plain ANSI output (skips mode selector)  |
| `uprooted --uninstall`      | Uninstall with TUI (skips mode selector)              |
| `uprooted --uninstall --plain` | Uninstall with plain ANSI output                   |
| `uprooted --repair`         | Repair with TUI (skips mode selector)                 |
| `uprooted --repair --plain` | Repair with plain ANSI output                         |
| `uprooted --diagnose`       | Verbose 7-step diagnostics (always plain output)      |

---

## Mode Selector

### Source: `installer/src-tauri/src/main.rs` (`tui::run_mode_selector()`)

When launched with no CLI flags and not in `--plain` mode, the installer displays an
interactive mode selector screen before proceeding. This allows users who double-click
the binary to access Install, Uninstall, and Repair without needing CLI flags.

The mode selector renders a centered 50-column box with the Uprooted version, three
selectable options (Install, Uninstall, Repair), and navigation hints. Arrow keys
move the `▶` cursor, Enter confirms the selection, and Q/Esc quits.

CLI flags (`--uninstall`, `--repair`, `--plain`) bypass the mode selector entirely
for backward compatibility with scripts and automation.

---

## TUI Mode

### Source: `installer/src-tauri/src/main.rs` (inline `tui` module)

The TUI renders a centered bordered box in an alternate screen using ratatui's
`CrosstermBackend`. It displays the installer version, a list of steps with status
indicators, and a footer message on completion.

### Visual Elements

- **Border:** Cyan-colored box centered in the terminal, 60 columns wide.
- **Title:** Operation name ("Install", "Uninstall", "Repair") in the border.
- **Version:** `Uprooted v0.4.2` displayed at the top of the box.
- **Step indicators:**
  - `○` (dark gray) -- pending
  - Braille spinner `⠋⠙⠹⠸⠼⠴⠦⠧⠇⠏` (yellow, animated) -- running
  - `✓` (green) -- completed
  - `✗` (red) -- failed, with error detail on the next line
- **Footer:** Green bold success message or red bold error message.
- **Exit:** "Press any key to exit..." prompt. On success, auto-closes after 3 seconds.

### State Machine

The `AppState` struct tracks all steps, their statuses (`Pending`, `Running`, `Done`,
`Failed(String)`), a spinner tick counter, and completion state. The `run_tui()`
function enters the alternate screen, performs an initial render, executes all steps
synchronously (updating state between steps), renders the final state, then waits for
a keypress or timeout before restoring the terminal.

### Install Steps (TUI)

1. **Check for running Root process** -- auto-close Root if running.
2. **Detect Root installation** -- locate Root executable and profile directory.
3. **Deploy hook files** -- extract all embedded artifacts to disk.
4. **Set environment variables** -- write CLR profiler env vars (dual-prefix).
5. **Patch HTML files** -- inject `<script>` and `<link>` tags into Root's HTML.
6. **Verify installation** -- re-run detection to confirm everything is in place.

### Uninstall Steps (TUI)

1. **Check for running Root process** -- auto-close Root if running.
2. **Remove environment variables** -- delete CLR profiler env vars.
3. **Restore HTML files** -- strip injected tags from Root's HTML.
4. **Remove deployed files** -- delete the entire uprooted directory.

### Repair Steps (TUI)

1. **Check for running Root process** -- auto-close Root if running.
2. **Re-deploy hook files** -- overwrite all artifacts.
3. **Set environment variables** -- rewrite all env vars.
4. **Re-patch HTML files** -- strip existing patches, re-inject fresh ones.
5. **Verify installation** -- re-run detection to confirm.

---

## Plain Mode

### Source: `installer/src-tauri/src/cli.rs`

Plain mode (`--plain`) uses simple sequential ANSI output with color codes and
Unicode checkmarks/crosses. Each operation prints a header with the version, then
logs each step's result on a single line. No alternate screen, no spinner animation,
no raw mode -- suitable for piping, CI, or terminals that do not support the TUI.

Helper functions: `ok()` prints `✓` in green, `fail()` prints `✗` in red, `warn()`
prints `⚠` in yellow, `header()` prints a numbered step header in cyan.

---

## Diagnostics Mode

### Source: `installer/src-tauri/src/cli.rs` (`run_diagnose()`)

The `--diagnose` flag runs a comprehensive 7-step diagnostic that performs every
installer operation and reports detailed results. This is always plain output
(no TUI), and it actively deploys files and sets env vars as part of the diagnostic.

### Diagnostic Steps

1. **Detection** -- Locate Root executable, profile directory, HTML files. Report
   per-file hook status and per-env-var status. Check if env vars are active in the
   current session (Linux only -- detects the gap between "configured" and "active").
2. **Process check** -- Report whether Root is currently running.
3. **File deployment** -- Deploy all artifacts and list each deployed file with size.
4. **Environment variables** -- Set env vars and verify post-set values.
5. **HTML patching** -- Patch HTML files and list each patched file.
6. **Post-install verification** -- Re-run full detection, report overall pass/fail.
7. **Hook log tail** -- Read the last 50 lines of `uprooted-hook.log` from the
   profile directory (shows the C# hook's runtime log from previous Root launches).

The diagnostic reports platform, architecture, and a UTC timestamp (computed without
the chrono crate using a civil-date algorithm from Unix epoch seconds).

---

## Rust Backend

### Source: `installer/src-tauri/src/main.rs`

The `main()` function parses CLI arguments via clap's derive API, then dispatches to
the appropriate handler:

- `--diagnose` always calls `cli::run_diagnose()`.
- `--uninstall` / `--repair` set the `InstallerMode` directly (bypass mode selector).
- `--plain` with no mode flag goes straight to install (bypass mode selector).
- Default (no flags) calls `tui::run_mode_selector()` for interactive mode selection, then dispatches to the chosen TUI handler.
- The final dispatch matches `(InstallerMode, plain)` to route to the correct TUI or plain handler.

The `#[derive(Parser)]` struct defines four boolean flags: `uninstall`, `repair`,
`diagnose`, and `plain`. An `InstallerMode` enum (`Install`, `Uninstall`, `Repair`)
is used for dispatch.

### Modules

| Module         | File              | Purpose                                              |
|----------------|-------------------|------------------------------------------------------|
| `cli`          | `cli.rs`          | Plain ANSI output handlers and diagnostics           |
| `detection`    | `detection.rs`    | Root installation discovery and state assessment     |
| `embedded`     | `embedded.rs`     | `include_bytes!()` artifact constants                |
| `hook`         | `hook.rs`         | File deployment, env vars, process management        |
| `patcher`      | `patcher.rs`      | HTML injection and removal                           |
| `settings`     | `settings.rs`     | JSON settings read/write                             |

The `tui` module is defined inline in `main.rs` rather than as a separate file.

### Data Structures

Three key structs are used across modules:

- **`DetectionResult`** (`detection.rs`) -- `root_found`, `root_path`,
  `profile_dir`, `html_files`, `is_installed`, and a nested `hook_status`.
- **`HookStatus`** (`hook.rs`) -- per-file booleans (`profiler_dll`, `hook_dll`,
  `hook_deps`, `preload_js`, `theme_css`), per-env-var booleans
  (`env_enable_profiling`, `env_profiler_guid`, `env_profiler_path`,
  `env_ready_to_run`), aggregate flags (`files_ok`, `env_ok`), and
  `env_vars_active` (whether env vars are present in the current process
  environment, relevant on Linux where environment.d requires re-login).
- **`PatchResult`** (`patcher.rs`) -- `success`, `message`, `files_patched`.

---

## Detection

### Source: `installer/src-tauri/src/detection.rs`

The detection module locates the Root Communications installation and determines the
current state of Uprooted deployment.

### Root Executable Path

**Windows** (`detection.rs`):
```
%LOCALAPPDATA%\Root\current\Root.exe
```
The installer checks if this single path exists.

**Linux** (`detection.rs`):
Uses 5 strategies in order, returning the first match:

1. **Exact well-known paths** (fastest) -- checks `~/Applications/Root.AppImage`,
   `~/Downloads/Root.AppImage`, `~/.local/bin/Root.AppImage`, `/opt/Root.AppImage`,
   `/usr/bin/Root.AppImage`, `~/.local/bin/Root`
2. **Glob patterns** -- scans common directories (`~/Applications`, `~/Downloads`,
   `~/.local/bin`, `~/Desktop`, `~`, `/opt`, `/usr/bin`, `/usr/local/bin`) for any
   file matching `root*.appimage` (case-insensitive), catching versioned filenames
   like `Root-0.9.93.AppImage`
3. **`.desktop` file search** -- scans `~/.local/share/applications`,
   `/usr/share/applications`, `/usr/local/share/applications` for desktop entries
   with "Root" in the `Name=` field, extracts the `Exec=` path
4. **Running process check** -- reads `/proc/*/exe` symlinks for any active Root process
5. **PATH lookup** -- runs `which Root` to check the system PATH

Falls back to `~/Applications/Root.AppImage` if no strategy matches.

### Profile Directory

**Windows:**
```
%LOCALAPPDATA%\Root Communications\Root\profile\default
```

**Linux:**
```
~/.local/share/Root Communications/Root/profile/default
```

### HTML Target Discovery

`find_target_html_files()` scans the profile directory for patchable HTML files in
two locations:

1. `profile/default/WebRtcBundle/index.html` -- the main WebRTC UI bundle.
2. `profile/default/RootApps/*/index.html` -- every sub-app directory under RootApps.

Only files that actually exist on disk are returned.

### Installation State

`check_is_installed()` reads each discovered HTML file and checks whether it contains
any uprooted injection markers (delegating to `patcher::is_patched()`). Returns
`true` if at least one file is patched.

### Full Detection Flow

`detect()` composes all the above:
1. Resolve Root executable path.
2. Resolve profile directory.
3. Discover HTML targets.
4. Check if any HTML file is patched.
5. Check hook file and env var status.
6. Return the complete `DetectionResult`.

---

## HTML Patcher

### Source: `installer/src-tauri/src/patcher.rs`

The patcher injects Uprooted's `<script>` and `<link>` tags into Root's HTML files
so that the TypeScript layer loads when Root renders its Chromium webviews.

### Markers and Injected Content

The patcher delimits injected content with HTML comment markers:
`MARKER_START` = `<!-- uprooted:start -->`, `MARKER_END` = `<!-- uprooted:end -->`.
A `LEGACY_MARKER` (`<!-- uprooted -->`) is recognized for detection of older installs
from the bash installer. Backups use the suffix `.uprooted.bak`.

Three elements are injected before the `</head>` tag:

```html
<!-- uprooted:start -->
    <script>window.__UPROOTED_SETTINGS__={...};</script>
    <script src="file:///path/to/uprooted-preload.js"></script>
    <link rel="stylesheet" href="file:///path/to/uprooted.css">
<!-- uprooted:end -->
```

1. **Settings inline script** -- serializes `UprootedSettings` as JSON into a global
   variable so the TypeScript preload can read settings synchronously on load.
2. **Preload script** -- loads the main TypeScript bundle (plugin system, theme engine,
   bridge proxies) via `file:///` URL pointing to the deployed artifact.
3. **Theme stylesheet** -- loads CSS variables and base styles. Paths use forward
   slashes even on Windows.

### Patch Detection

`is_patched()` returns `true` if the content contains any of:
- `MARKER_START` (`<!-- uprooted:start -->`)
- `LEGACY_MARKER` (`<!-- uprooted -->`)
- The string `uprooted-preload` (catches bare script tags from the bash installer)

### Install Flow

`install()`:

1. Resolve script and CSS paths from the uprooted directory (forward-slash normalized).
2. Load current settings and serialize to JSON.
3. Build the injection string with markers + script + link tags.
4. Find all target HTML files via `detection::find_target_html_files()`.
5. For each file:
   a. Skip if already patched.
   b. Create a `.uprooted.bak` backup (only if one does not already exist).
   c. Inject the block before `</head>` via string replacement.
   d. Write the modified content back to disk.
6. Return `PatchResult` with the list of patched files.

### Uninstall Flow

`uninstall()`:

1. Find all target HTML files.
2. For each patched file:
   a. **Preferred:** Strip the injection in-place using `strip_injection()`. This
      preserves Root's current HTML (important if Root has auto-updated since install).
      Delete the backup file after successful stripping.
   b. **Fallback:** If stripping produced no change (edge case), restore from the
      `.uprooted.bak` backup file, then delete the backup.

### Strip Injection

`strip_injection()` performs line-by-line filtering to remove all traces of Uprooted
injection:

- Lines between `MARKER_START` and `MARKER_END` (inclusive) are dropped.
- Lines containing `LEGACY_MARKER` are dropped.
- Bare `uprooted-preload` script tags (from the bash installer, which did not use
  markers) are dropped.
- Bare `uprooted.css` link tags are dropped.
- Bare `__UPROOTED_SETTINGS__` inline scripts are dropped.

This multi-strategy approach ensures clean removal regardless of which installer
version originally applied the patches.

### Repair Flow

`repair()`:

1. For each patched HTML file, strip the existing injection in-place.
2. Update the backup file to the clean state (so backups reflect current Root HTML).
3. Call `install()` to re-inject fresh patches.

This is different from uninstall+install because it preserves the backup chain and
avoids deleting/re-deploying hook files unnecessarily.

---

## Hook Deployment

### Source: `installer/src-tauri/src/hook.rs`

The hook module handles deploying artifact files to disk, configuring CLR profiler
environment variables, and managing Root processes.

### Uprooted Directory

All artifacts are deployed to a single directory:

| Platform | Path                                  |
|----------|---------------------------------------|
| Windows  | `%LOCALAPPDATA%\Root\uprooted\`       |
| Linux    | `~/.local/share/uprooted/`            |

See `get_uprooted_dir()`.

### Deployed Files

`deploy_files()` extracts seven embedded artifacts:

| File                       | Source Constant           | Purpose                                  |
|----------------------------|---------------------------|------------------------------------------|
| `uprooted_profiler.dll`   | `embedded::PROFILER`      | CLR profiler native DLL (Windows)        |
| `libuprooted_profiler.so` | `embedded::PROFILER`      | CLR profiler native library (Linux)      |
| `UprootedHook.dll`        | `embedded::HOOK_DLL`      | C# .NET hook assembly (managed code)     |
| `UprootedHook.deps.json`  | `embedded::HOOK_DEPS_JSON`| .NET dependency manifest for the hook    |
| `uprooted-preload.js`     | `embedded::PRELOAD_JS`    | TypeScript bundle (compiled to JS)       |
| `uprooted.css`            | `embedded::THEME_CSS`     | Base theme stylesheet                    |
| `nsfw-filter.js`          | `embedded::NSFW_FILTER_JS`| NSFW filter browser script               |
| `link-embeds.js`          | `embedded::LINK_EMBEDS_JS`| Link embed browser script                |

On Linux, the profiler `.so` file is set to `0o755` (executable) after deployment.

The directory is created with `fs::create_dir_all()` if it does not exist.

### Environment Variables (Dual-Prefix)

The CLR profiler requires environment variables to be set for `Root.exe` to load
the hook on startup. The installer sets **both** `DOTNET_` (primary, .NET 10+) and
`CORECLR_` (legacy, .NET 8/9) prefixes for maximum compatibility:

| Variable                       | Value                                    | Purpose                          |
|--------------------------------|------------------------------------------|----------------------------------|
| `DOTNET_EnableDiagnostics`     | `1`                                      | Enable CLR diagnostics           |
| `DOTNET_ENABLE_PROFILING`      | `1`                                      | Enable CLR profiling             |
| `DOTNET_PROFILER`              | `{D1A6F5A0-1234-4567-89AB-CDEF01234567}` | GUID identifying the profiler   |
| `DOTNET_PROFILER_PATH`         | Full path to profiler DLL/SO             | Where the runtime loads the profiler from |
| `DOTNET_ReadyToRun`            | `0`                                      | Disable R2R to ensure JIT hooks work |
| `CORECLR_ENABLE_PROFILING`     | `1`                                      | Legacy: enable CLR profiling     |
| `CORECLR_PROFILER`             | `{D1A6F5A0-1234-4567-89AB-CDEF01234567}` | Legacy: profiler GUID           |
| `CORECLR_PROFILER_PATH`        | Full path to profiler DLL/SO             | Legacy: profiler path            |

Additionally, the legacy `DOTNET_STARTUP_HOOKS` variable is deleted if present.

#### Windows Implementation

`set_env_vars()`:
- Opens `HKEY_CURRENT_USER\Environment` via the `winreg` crate.
- Writes all eight env vars (five `DOTNET_` + three `CORECLR_`) as REG_SZ values
  (user-scoped, not system-wide).
- Deletes `DOTNET_STARTUP_HOOKS` if it exists (legacy cleanup).
- Calls `broadcast_env_change()` to notify running processes.

`remove_env_vars()`:
- Opens `HKEY_CURRENT_USER\Environment` with `KEY_WRITE` access.
- Deletes all env var names: the `DOTNET_` set, the `CORECLR_` set, and the legacy
  `DOTNET_STARTUP_HOOKS`.
- Calls `broadcast_env_change()`.

`broadcast_env_change()`:
- Sends `WM_SETTINGCHANGE` (0x001A) to `HWND_BROADCAST` (0xFFFF) with the
  `"Environment"` string as the lParam.
- Uses `SendMessageTimeoutW` with `SMTO_ABORTIFHUNG` and a 5-second timeout.
- This causes Explorer, shells, and other applications to re-read environment
  variables from the registry, so Root picks up the changes on next launch without
  requiring a reboot or re-login.

`check_env_vars()`:
- Reads env vars from `HKCU\Environment`, checking `DOTNET_` first with `CORECLR_`
  fallback for each variable.
- Returns a tuple of four booleans: (enable, guid, path, r2r).

#### Linux Implementation

`set_env_vars()` uses five complementary mechanisms:

1. **`~/.config/environment.d/uprooted.conf`**:
   A systemd user session environment file. Variables set here are picked up after
   re-login, equivalent to the Windows registry approach. This is the primary
   mechanism. Contains both `DOTNET_` and `CORECLR_` prefixed variables.

2. **Wrapper script `launch-root.sh`**:
   Written to the uprooted directory. Exports all env vars (both prefixes) then
   `exec`s the Root binary. This works immediately from a terminal without re-login.
   Set to `0o755`.

3. **`.desktop` file**:
   `create_desktop_file()` writes
   `~/.local/share/applications/root-uprooted.desktop` with the `Name`
   "Root (Uprooted)" and `Exec` pointing to the wrapper script. This creates an
   application menu entry for launching Root with mods enabled.

4. **KDE Plasma env script** (`~/.config/plasma-workspace/env/uprooted.sh`):
   Sourced on Plasma session startup. Ensures env vars are available for
   desktop-launched apps in KDE, which may not inherit `environment.d` variables.

5. **`~/.profile` fallback**:
   Appends env var exports for non-systemd sessions (X11 login shells, etc.).
   Only adds the block if `DOTNET_ENABLE_PROFILING` is not already present.

`remove_env_vars()`:
- Deletes `~/.config/environment.d/uprooted.conf`.
- Deletes `~/.config/plasma-workspace/env/uprooted.sh`.
- Deletes the `launch-root.sh` wrapper script.
- Deletes `~/.local/share/applications/root-uprooted.desktop`.
- Removes the Uprooted block from `~/.profile` (scans for the comment marker and
  strips all associated export lines).

`check_env_vars()`:
- Reads `environment.d/uprooted.conf`; falls back to `launch-root.sh`; falls back
  to `~/.profile`.
- Checks for substring presence of each expected env var assignment, accepting
  either `DOTNET_` or `CORECLR_` prefix.

#### Runtime Environment Check

`check_env_vars_active()` determines whether env vars are present in the **current
process environment** (not just configured in files). On Windows, this mirrors the
config check since registry changes apply to new processes immediately. On Linux,
this detects the gap between "configured in environment.d" and "actually active"
-- the user may need to re-login for environment.d changes to take effect.

### File Removal

`remove_files()` deletes the entire uprooted directory with `fs::remove_dir_all()`.
This is called during uninstall after HTML has been restored.

### Hook Status Check

`check_hook_status()` checks:
- Existence of all five core deployed files (profiler, hook DLL, deps, preload, CSS).
- Correctness of all environment variables (via `check_env_vars()`).
- Sets `files_ok = true` only if all five files exist.
- Sets `env_ok = true` only if enable + guid + path are all correct.
- Sets `env_vars_active` based on `check_env_vars_active()`.

### Process Management

**`check_root_running()`**: On Windows, uses `find_root_pids()` (toolhelp snapshot
via `CreateToolhelp32Snapshot` + `Process32FirstW`/`Process32NextW`, collecting PIDs
matching `"Root.exe"` case-insensitively). On Linux, runs `pgrep -x Root`.

**`kill_root_processes()`**: On Windows, calls `OpenProcess` with
`PROCESS_TERMINATE` then `TerminateProcess` for each PID. On Linux, runs `pkill -x
Root`. Returns the count of terminated processes.

---

## Settings

### Source: `installer/src-tauri/src/settings.rs`

The settings module provides read/write access to Uprooted's configuration file from
the installer. The same file is read by the TypeScript runtime inside Root.

### Settings File Location

```
{profile_dir}/uprooted-settings.json
```

Resolved via `detection::get_profile_dir()`.

### Data Structure

`UprootedSettings` contains three fields: `enabled` (master toggle), `plugins` (a
`HashMap<String, PluginSettings>` where each plugin has an `enabled` flag and a
`config` map of arbitrary JSON values), and `custom_css` (user custom CSS string).
Fields are serialized with `camelCase` naming via `#[serde(rename_all = "camelCase")]`
for JavaScript compatibility.

### Load

`load_settings()`:
- If the settings file exists and parses successfully, return the deserialized struct.
- Otherwise, return `UprootedSettings::default()` (enabled=true, no plugins, no CSS).

### Save

`save_settings()`:
- Creates parent directories if needed.
- Serializes to pretty-printed JSON via `serde_json::to_string_pretty()`.
- Writes to disk.

---

## Embedded Artifacts

### Source: `installer/src-tauri/src/embedded.rs`

All deployment artifacts are embedded directly into the installer binary using Rust's
`include_bytes!()` macro. This makes the installer fully self-contained -- no network
access, no sidecar files, no extraction steps.

### Artifact List

| Constant          | File                                         | Platform  | Purpose                         |
|-------------------|----------------------------------------------|-----------|---------------------------------|
| `PROFILER`        | `artifacts/uprooted_profiler.dll`            | Windows   | CLR profiler native DLL         |
| `PROFILER`        | `artifacts/libuprooted_profiler.so`          | Linux     | CLR profiler native shared lib  |
| `HOOK_DLL`        | `artifacts/UprootedHook.dll`                 | Both      | C# .NET hook assembly           |
| `HOOK_DEPS_JSON`  | `artifacts/UprootedHook.deps.json`           | Both      | .NET dependency manifest        |
| `PRELOAD_JS`      | `artifacts/uprooted-preload.js`              | Both      | TypeScript bundle (compiled)    |
| `THEME_CSS`       | `artifacts/uprooted.css`                     | Both      | Base theme CSS                  |
| `NSFW_FILTER_JS`  | `artifacts/nsfw-filter.js`                   | Both      | NSFW filter browser script      |
| `LINK_EMBEDS_JS`  | `artifacts/link-embeds.js`                   | Both      | Link embed browser script       |

The `PROFILER` constant uses `#[cfg(target_os)]` conditional compilation to include
the correct native binary for the build target.

### Artifact Staging

The `installer/src-tauri/artifacts/` directory is populated by `scripts/build-installer.ps1`
before `cargo build --release`: build the C# hook (`dotnet build hook/UprootedHook.csproj -c Release`),
build the TS bundle (`pnpm build`), copy outputs to `artifacts/`, then run the
release build. Missing artifacts cause a compile error since `include_bytes!()` is
evaluated at compile time. See [Build Guide](../install/BUILD.md) for the full pipeline.

---

## Build

### Dependencies

**Rust** (`Cargo.toml`): `ratatui` 0.29, `crossterm` 0.28, `clap` 4 (with `derive`
feature), `serde`/`serde_json` 1, `glob` 0.3. Windows-only: `winreg` 0.55
(registry), `windows-sys` 0.59 (Win32 API for `SendMessageTimeoutW`,
`TerminateProcess`, toolhelp snapshots, `AllocConsole`).

**Rust edition:** 2024

### Build Commands

```bash
# Development build
cd installer/src-tauri && cargo build

# Release build (requires artifacts staged first)
cd installer/src-tauri && cargo build --release

# Full pipeline (builds hook, stages artifacts, builds installer)
powershell -File scripts/build-installer.ps1
```

The production sequence: `dotnet build hook/UprootedHook.csproj -c Release`, `pnpm build` (TS bundle),
copy outputs to `artifacts/`, then `cargo build --release` compiles the backend with
`include_bytes!()` pulling from `artifacts/`. The release profile applies symbol
stripping, LTO, single codegen unit, abort-on-panic, and size optimization to produce
a minimal binary.

---

## Cross-References

- See [Hook Reference](HOOK_REFERENCE.md) for what the deployed hook does at runtime
  (multi-phase startup, sidebar injection, Avalonia reflection, DotNetBrowser features).
- See [Installation Guide](../install/INSTALLATION.md) for end-user install instructions.
- See [Architecture](ARCHITECTURE.md) for how the installer fits into the dual-layer
  injection model.
- The `uprooted-preload.js` deployed by the installer is compiled from the public
  repo's TypeScript source -- it bootstraps the plugin system, theme engine, and bridge
  proxies inside Root's Chromium webviews.

---

**Canonical for:** Rust installer internals, TUI/CLI modes, detection algorithm (Windows + Linux 5-strategy), HTML patcher (markers, inject/strip/repair), hook deployment (8 artifacts), environment variable management (dual-prefix, Windows registry, Linux 5-mechanism), diagnostics mode, embedded artifacts, process management, settings JSON
**Not canonical for:** end-user instructions → [INSTALLATION.md](../install/INSTALLATION.md) | build pipeline → [BUILD.md](../install/BUILD.md) | runtime hook behavior → [HOOK_REFERENCE.md](HOOK_REFERENCE.md)
*Installer reference for Uprooted v0.4.2. Last updated 2026-02-19.*
