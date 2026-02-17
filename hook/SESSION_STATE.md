# Uprooted Hook - Session State (2026-02-16)

## Release: v0.2.2

### What Changed

#### Fix: Linux AppImage Loading Failure
Three interconnected fixes for Uprooted failing to load when Root is launched as an AppImage on Linux:

1. **AppImage-aware profiler process guard** (`tools/uprooted_profiler_linux.c`)
   - Old: strict `strcmp(exeName, "Root")` rejected AppImage binary names
   - New: accepts case-insensitive "root", checks `APPIMAGE` env var, accepts `Root-*` prefixes
   - Added `#define _GNU_SOURCE` for `strcasestr`

2. **Runtime env var detection** (`installer/src-tauri/src/hook.rs`)
   - Added `env_vars_active` field to `HookStatus` -- checks actual `std::env::var()` on Linux
   - Config files say "configured" but env vars aren't active until re-login
   - Installer now warns: "configured (not active -- restart session or use wrapper)"
   - Added `~/.profile` as fallback for non-systemd sessions (X11, login shells)
   - Uninstaller cleans up `~/.profile` entries

3. **Installer frontend warnings** (`installer/src/pages/main.ts`, `installer/src/lib/tauri.ts`)
   - Status dot shows yellow when env vars configured but not active
   - Log warns with actionable fix: "restart session or use wrapper"
   - Scenario analysis detects the gap and suggests app menu launch

4. **Linux installer diagnostics** (`install-uprooted-linux.sh`)
   - Added `--diagnose` mode: checks env vars in current shell, config files, deployed files, running Root processes (reads `/proc/PID/environ`), log files, HTML patch status
   - Added `~/.profile` fallback alongside `environment.d`
   - Install output now suggests `--diagnose` if things aren't working

#### Feature: NSFW Content Filter Plugin (complete)

Full Google Cloud Vision SafeSearch-based content filter. See previous session notes for details.

**New files:** `hook/DotNetBrowserReflection.cs`, `hook/NsfwFilter.cs`, `hook/nsfw-filter.js`
**Modified:** UprootedSettings, ContentPages, SidebarInjector, StartupHook (Phase 5), HtmlPatchVerifier, UprootedHook.csproj

#### Version Bump: 0.1.96 -> 0.2.2

Bumped across all files: package.json (x2), Cargo.toml, Cargo.lock, tauri.conf.json, C# source, TypeScript plugins, install scripts, PKGBUILD, site, all docs, _publish staging.

## Current Architecture

### Startup Phases
| Phase | File | Purpose |
|-------|------|---------|
| 0 | HtmlPatchVerifier | Verify/repair HTML patches + FileSystemWatcher |
| 1 | StartupHook | Wait for Avalonia assemblies (30s) |
| 2 | StartupHook | Wait for Application.Current (30s) |
| 3 | StartupHook | Wait for MainWindow (60s) |
| 3.5 | StartupHook | Initialize ThemeEngine + apply saved theme |
| 4 | SidebarInjector | Start settings page monitor (200ms poll) |
| 5 | NsfwFilter | Background thread: DotNetBrowser discovery + JS injection |

### Sidebar Nav Items
1. Uprooted -> `BuildUprootedPage()`
2. Plugins -> `BuildPluginsPage()` (includes Content Filter as a plugin card with gear/info lightboxes)
3. Themes -> `BuildThemesPage()`

### Release Artifacts (v0.2.2)
| Platform | Artifact | CI Workflow |
|----------|----------|-------------|
| Windows | `Uprooted-0.2.2.exe` | build-installer.yml |
| Linux (Debian/Ubuntu) | `uprooted_0.2.2_amd64.deb` | build-linux.yml |
| Linux (portable) | `Uprooted-0.2.2.AppImage` | build-linux.yml |
| Arch Linux | `uprooted-bin-0.2.2-1-x86_64.pkg.tar.zst` | build-linux.yml (build-arch job) |

## Build & Test

```powershell
# Windows:
Stop-Process -Name Root -Force -ErrorAction SilentlyContinue; Start-Sleep 1
dotnet build hook -c Release
powershell -ExecutionPolicy Bypass -File scripts\test-hook.ps1
Get-Content "$env:LOCALAPPDATA\Root Communications\Root\profile\default\uprooted-hook.log" -Tail 30
```

```bash
# Linux:
./install-uprooted-linux.sh --diagnose  # Check installation health
./install-uprooted-linux.sh             # Build and install from source
~/.local/share/uprooted/launch-root.sh  # Launch Root with Uprooted
```
