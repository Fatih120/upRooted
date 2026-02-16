# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Uprooted** is a client mod framework for Root Communications desktop app (like Vencord for Discord). Dual-layer injection: C# .NET hook into Root.exe (Avalonia) + TypeScript injection into embedded Chromium.

Read `docs/FRAMEWORK_GUIDE.md` first — it is the authoritative reference.

## Collaboration

This is an **active collaborative repo** between `watchthelight` and `agomusio` (both have admin access).

### Git Workflow (IMPORTANT)

- **Always `git pull` before starting any work** — the other contributor may have pushed changes
- **Always push after committing** — don't leave unpushed commits sitting locally
- **Write clear, descriptive commit messages** following this format:
  - `type: concise description of what changed and why`
  - Types: `fix:`, `feat:`, `refactor:`, `docs:`, `chore:`, `style:`
  - Body (optional): explain the "why" for non-obvious changes
  - Examples:
    - `fix: self-heal HTML patches after Root auto-update overwrites`
    - `feat: add Phase 0 startup verification for HTML patches`
    - `refactor: prefer in-place stripping over stale backup restore`
- **Never force-push to main** without explicit approval from both contributors
- **Check `git log` before committing** to see recent history and match style
- If there are merge conflicts, resolve carefully — don't discard the other person's work

## Repository Structure

```
uprooted-private/
├── hook/                          # C# .NET hook (CLR profiler injection)
│   ├── StartupHook.cs             # 5-phase Avalonia wait + initialization
│   ├── HtmlPatchVerifier.cs       # Self-healing HTML patches (Phase 0 + FileSystemWatcher)
│   ├── AvaloniaReflection.cs      # Reflection cache for ~80 Avalonia types
│   ├── SidebarInjector.cs         # Timer-based sidebar injection (200ms poll)
│   ├── ContentPages.cs            # Settings page builders
│   ├── ThemeEngine.cs             # Native Avalonia theme application
│   ├── PlatformPaths.cs           # Platform-specific path resolution
│   ├── UprootedSettings.cs        # INI-based settings (no System.Text.Json)
│   ├── Entry.cs                   # Profiler injection entry point
│   ├── Logger.cs                  # File-based logging
│   └── SESSION_STATE.md           # Session state/context handoff
├── installer/src-tauri/src/       # Tauri installer (Rust)
│   ├── patcher.rs                 # HTML patch install/uninstall/repair
│   ├── hook.rs                    # File deployment + env var management
│   ├── detection.rs               # Root installation detection
│   └── settings.rs                # JSON settings management
├── install-uprooted-linux.sh      # Standalone bash installer for Linux
├── dist/                          # Prebuilt TypeScript bundle (from public repo)
├── docs/
│   ├── FRAMEWORK_GUIDE.md         # Authoritative reference (read first!)
│   ├── HOW_IT_WORKS.md            # Complete technical walkthrough
│   └── FUTURE_PLANS.md            # Roadmap
└── tools/                         # Build utilities
```

## Build

```bash
# C# hook
dotnet build hook/ -c Release

# Installer (Tauri/Rust)
cd installer && cargo tauri build

# Full installer with embedded artifacts
powershell -File scripts/build_installer.ps1
```

## Critical Rules

These cause real bugs — do not violate:

- **Never use `Type.GetType()` for Avalonia types** — use `AvaloniaReflection`
- **Never modify `ContentControl.Content` directly** — causes UI freeze, use Grid overlay
- **Never use `System.Text.Json` in hook** — causes MissingMethodException in profiler context
- **Never use `EventInfo.AddEventHandler` for RoutedEvents** — use Expression lambdas
- **Never use localStorage** — Root runs Chromium with `--incognito`
- **`DispatcherPriority` is a struct not enum** in Avalonia 11+

## Related Repos

- **Public scaffold**: `watchthelight/uprooted` — TypeScript source, plugin API, theme engine
- These repos are **strictly separate** — never copy, reference, or leak code/commits between them
