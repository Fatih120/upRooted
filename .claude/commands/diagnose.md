---
name: diagnose
description: Check the local Uprooted installation for common issues — env vars, file deployment, HTML patches, settings
allowed-tools:
  - Bash
  - Read
  - Grep
  - Glob
---

# Diagnose

Check the local Uprooted installation for common issues. This wraps the diagnostic scripts and interprets their output.

## Instructions

### Step 1: Identify platform

Detect whether we're on Linux or Windows (check for `/home` vs `C:\Users`, or `uname`).

### Step 2: Check file deployment

Verify the core files exist in the install directory:

**Linux** (`~/.local/share/uprooted/`):
- `UprootedHook.dll`
- `uprooted_profiler.so` (or similar)
- Wrapper script

**Windows** (`%LOCALAPPDATA%\Root\uprooted\`):
- `UprootedHook.dll`
- `uprooted_profiler.dll`
- `UprootedLauncher.exe`

For each file, report: exists (yes/no), size, last modified timestamp.

### Step 3: Check environment variables

Check if the CLR profiler environment variables are set. These are required for hook injection:

```bash
# Linux
env | grep -E "DOTNET_|CORECLR_" | sort

# Windows (PowerShell)
# [Environment]::GetEnvironmentVariable("DOTNET_STARTUP_HOOKS", "User")
# [Environment]::GetEnvironmentVariable("CORECLR_PROFILER", "User")
```

Expected variables:
- `DOTNET_STARTUP_HOOKS` — path to `UprootedHook.dll`
- `DOTNET_PROFILER` or `CORECLR_PROFILER` — profiler GUID `{D1A6F5A0-1234-4567-89AB-CDEF01234567}`
- `DOTNET_PROFILER_PATH` or `CORECLR_PROFILER_PATH` — path to profiler DLL/SO
- `DOTNET_ENABLE_PROFILING` or `CORECLR_ENABLE_PROFILING` — `1`

### Step 4: Check HTML patches

Look for Uprooted's `<script>` and `<link>` tag injections in Root's HTML files:

**Linux:** `~/.local/share/Root Communications/Root/profile/default/`
**Windows:** `%LOCALAPPDATA%\Root Communications\Root\profile\default\`

Search for files matching `*.html` and grep for `uprooted` in them. Report which files are patched and which are not.

### Step 5: Check settings file

Read `uprooted-settings.ini` from the profile directory. Report:
- Whether it exists
- Current settings (Enabled, Version, ActiveTheme, plugin states)
- Any unexpected or missing values

### Step 6: Check hook log

Look for the most recent hook log file:
- `uprooted-hook.log` in the profile directory

If it exists:
- Report its size and last modified time
- Read the last 20 lines
- Look for error patterns: `Exception`, `FAILED`, `Error`, `timeout`
- Check if the startup phases completed successfully (look for Phase 0-5 log entries)

### Step 7: Diagnosis

Based on all checks, report a diagnosis:

**Healthy** — All files deployed, env vars set, HTML patched, settings present, log shows clean startup.

**Issues found** — List each problem with a suggested fix:

| Symptom | Likely cause | Fix |
|---------|-------------|-----|
| Missing DLL | Not installed or build not deployed | Run `/deploy` or the installer |
| Missing env vars | Installer didn't set them, or shell doesn't inherit | Re-run installer, check `.profile` or shell rc |
| Unpatched HTML | Root auto-updated and overwrote patches | Run installer with `--repair`, or the hook's Phase 0 will self-heal on next launch |
| Settings missing | First run or deleted | Will be created on next launch with defaults |
| Log shows errors | Depends on the error | Read the specific error and suggest targeted fix |
| No log file | Hook never loaded | Check env vars and profiler path first |
