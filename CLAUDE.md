# CLAUDE.md

> **Related docs:** [NEW-SESSION.md](NEW-SESSION.md) | [Architecture](docs/framework/ARCHITECTURE.md) | [Index](docs/INDEX.md) | [Contributing](CONTRIBUTING.md)

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Uprooted** is a client mod framework for Root Communications desktop app (like Vencord for Discord). Dual-layer injection: C# .NET hook into Root.exe (Avalonia) + TypeScript injection into embedded Chromium.

For comprehensive documentation, see [docs/INDEX.md](docs/INDEX.md) for navigation or [docs/framework/ARCHITECTURE.md](docs/framework/ARCHITECTURE.md) for architecture reference.

**First thing every session:** Run `/hi`. It loads project context and waits for your instructions.

## Collaboration

This is an **active collaborative repo** between `watchthelight` and `agomusio` (both have admin access).

### Git Workflow (IMPORTANT)

- **Always commit as the logged-in git user** -- never add `Co-Authored-By` trailers. Commits should be authored by whoever `git config user.name`/`user.email` returns, not by Claude.
- **Always `git pull` before starting any work** -- the other contributor may have pushed changes
- **Always push after committing** -- don't leave unpushed commits sitting locally
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
- If there are merge conflicts, resolve carefully -- don't discard the other person's work

## Repository Structure

```
uprooted-private/
├── hook/                              # C# .NET hook (CLR profiler injection) — 35 .cs files
│   ├── StartupHook.cs                 # Multi-phase startup orchestrator (Phase 0-5)
│   ├── HtmlPatchVerifier.cs           # Self-healing HTML patches (Phase 0 + FileSystemWatcher)
│   ├── AvaloniaReflection.cs          # Reflection cache for ~80 Avalonia types (2920 lines)
│   ├── SidebarInjector.cs             # Sidebar injection (LayoutUpdated event + safety poll)
│   ├── ContentPages.cs                # Settings page builders (3893 lines)
│   ├── ThemeEngine.cs                 # Native Avalonia theme engine (resource dict injection, live preview, custom ping color)
│   ├── ColorUtils.cs                  # HSL/HSV/RGB/OKLCH color conversion
│   ├── ColorPickerPopup.cs            # HSV color picker UI
│   ├── VisualTreeWalker.cs            # Visual tree DFS traversal
│   ├── NativeEntry.cs                 # Native method proxies
│   ├── PlatformPaths.cs               # Platform-specific path resolution
│   ├── UprootedSettings.cs            # INI-based settings (no System.Text.Json) + 10s TTL cache
│   ├── DotNetBrowserReflection.cs     # Reflection cache for DotNetBrowser types, IBrowser discovery
│   ├── BrowserDiscovery.cs            # Phase 4.5 diagnostic scanner
│   ├── ClearUrlsEngine.cs             # ClearURLs: strip tracking params from compose editor URLs on send
│   ├── LinkEmbedEngine.cs             # Avalonia-native link embed engine (OG/oEmbed + animated images + video embeds)
│   ├── AnimatedImage.cs              # Animated GIF/WebP decoder + timer playback (SkiaSharp reflection)
│   ├── MessageLogger.cs              # Message logger: edit/delete detection, visual indicators, collection subscription
│   ├── MessageStore.cs               # Flat-file persistence for message log (pipe-delimited, append-only)
│   ├── AuditLogEngine.cs             # Audit log viewer (community mod actions)
│   ├── AutoUpdater.cs                # In-process auto-updater (encrypted .uprpkg download, stable + dev channels)
│   ├── DesktopNotification.cs        # OS-level notifications (WinRT toast on Windows, notify-send on Linux)
│   ├── ProfileBadgeInjector.cs       # Injects "Uprooted Dev" badge into profile popups (1130 lines)
│   ├── SilentTypingEngine.cs         # Blocks SetTypingIndicator gRPC calls via DiagnosticListener interception
│   ├── NsfwFilter.cs                  # NSFW content filter (Avalonia-native visual tree scan)
│   ├── RootcordEngine.cs             # Rootcord plugin: Discord-style vertical server sidebar (experimental, live toggle)
│   ├── TranslateEngine.cs            # Translate plugin: DeepL-powered message translation (1145 lines)
│   ├── TranslateConfigPopup.cs       # Translate config popup UI (language picker, API key)
│   ├── UprootedPresenceBeacon.cs     # Presence beacon: Uprooted user detection via gRPC metadata
│   ├── ReconLogger.cs                # Recon logger: visual tree + style property diagnostic dumper
│   ├── Entry.cs                       # Profiler injection entry point
│   ├── LogConsole.cs                  # Dev-only live log terminal via named pipe (~200 lines)
│   ├── Logger.cs                      # File-based logging + wide event emission (~170 lines)
│   ├── TailSampler.cs                # Tail sampling for high-frequency scan ticks (~72 lines)
│   ├── WideEvent.cs                  # Structured wide event builder (IDisposable, key=value, dur_ms) (~150 lines)
│   └── SESSION_STATE.md               # Session state/context handoff
├── installer/src-tauri/src/           # Console TUI installer (Rust)
│   ├── main.rs                        # Console TUI entry point (ratatui)
│   ├── cli.rs                         # Plain ANSI output mode (--plain, --diagnose)
│   ├── patcher.rs                     # HTML patch install/uninstall/repair
│   ├── hook.rs                        # File deployment + env var management (DOTNET_ + CORECLR_)
│   ├── detection.rs                   # Root installation detection
│   ├── settings.rs                    # JSON settings management
│   └── embedded.rs                    # Embedded resource management
├── tools/                             # Native profiler and build utilities
│   ├── uprooted_profiler.c            # CLR profiler DLL (Windows)
│   ├── uprooted_profiler_linux.c      # CLR profiler shared object (Linux)
│   ├── uprooted_profiler.def          # Profiler DLL export definitions
│   └── (build scripts, proxy DLLs, diagnostic tools)
├── scripts/                           # Build and install automation
│   ├── build-installer.ps1            # Full installer build with embedded artifacts
│   ├── install-hook.ps1               # Hook deployment script
│   ├── uninstall-hook.ps1             # Hook removal script
│   ├── diagnose.ps1                   # Installation diagnostics
│   ├── pack-update.py                 # Packs 6 update files into encrypted .uprpkg (no pip deps)
│   └── (additional build/test/analysis scripts)
├── tests/                             # Test suites
│   └── UprootedTests/                 # C# unit tests (ColorUtils, GradientBrush)
├── docs/                              # Documentation
│   ├── INDEX.md                       # Documentation navigation hub (start here)
│   ├── HOW_IT_WORKS.md                # Complete technical walkthrough
│   ├── ROADMAP.md                     # Known issues, planned features, future direction
│   ├── install/                       # Installation and build
│   │   ├── INSTALLATION.md            # End-user install guide
│   │   └── BUILD.md                   # Build pipeline for all layers
│   ├── framework/                     # Framework internals
│   │   ├── ARCHITECTURE.md            # System architecture and design decisions
│   │   ├── HOOK_REFERENCE.md          # C# hook layer deep dive
│   │   ├── TYPESCRIPT_REFERENCE.md    # TypeScript browser injection reference
│   │   ├── CLR_PROFILER.md            # Native C profiler internals
│   │   ├── INSTALLER.md              # Console TUI installer reference
│   │   ├── THEME_ENGINE_DEEP_DIVE.md  # Theme engine algorithm deep dive
│   │   ├── AVALONIA_PATTERNS.md       # Avalonia UI patterns via reflection
│   │   └── DOTNET_RUNTIME.md          # .NET runtime constraints
│   ├── plugins/                       # Plugin author documentation
│   │   ├── GETTING_STARTED.md         # Plugin quickstart tutorial
│   │   ├── API_REFERENCE.md           # Plugin API surface
│   │   ├── BRIDGE_REFERENCE.md        # Root bridge IPC reference
│   │   ├── ROOT_ENVIRONMENT.md        # Root app internals (DOM, CSS, Chromium)
│   │   └── EXAMPLES.md               # Annotated example plugins
│   ├── research/                      # Security research and reverse engineering
│   │   ├── SECURITY_RESEARCH.md       # Security findings
│   │   ├── REVERSE_ENGINEERING.md     # RE methodology
│   │   ├── GRPC_PROTOCOL.md           # gRPC-web protocol reference
│   │   ├── GRPC_LIB_REFERENCE.md      # gRPC library reference
│   │   ├── RESEARCH_INDEX.md          # Research file navigation
│   │   └── ROOT_INTERNALS.md          # Root's native architecture
│   ├── dev/                           # Developer environment
│   │   ├── CONTRIBUTING_TECHNICAL.md   # Technical onboarding
│   │   └── PLANNING_REFERENCE.md      # Planning analysis index
│   └── archives/                      # Miscellaneous notes, one-off fixes, historical context
├── dist/                              # Prebuilt TypeScript bundle (from public repo)
├── install-uprooted-linux.sh          # Standalone bash installer for Linux
├── TASKS.md                           # Active task board (read by /hi each session)
├── NEXT-RELEASE.md                    # Changes since last release (replaced each release)
├── CHANGELOG.md                       # Internal changelog (Keep a Changelog format)
├── CHANGELOG_PUBLIC.md                # Public changelog (mirrors GitHub release notes)
├── CONTRIBUTING.md                    # Contribution guidelines
├── CLAUDE.md                          # AI contributor guide (this file)
└── README.md                          # Repository landing page
```

## Build

```bash
# C# hook
dotnet build hook/UprootedHook.csproj -c Release

# Console TUI installer (Rust)
cd installer/src-tauri && cargo build --release
```

## Critical Rules

These cause real bugs -- do not violate:

- **Never use `Type.GetType()` for Avalonia types** -- use `AvaloniaReflection`
- **Never modify `ContentControl.Content` directly** -- causes UI freeze, use Grid overlay
- **Never use `System.Text.Json` in hook** -- causes MissingMethodException in profiler context
- **Never use `EventInfo.AddEventHandler` for RoutedEvents** -- use Expression lambdas
- **Never use localStorage** -- Root runs Chromium with `--incognito`
- **`DispatcherPriority` is a struct not enum** in Avalonia 11+
- **Never gate `Logger` to developer channel only** -- stable users MUST produce logs so we can diagnose their bug reports. `Logger.Disable()` and `Logger.Enable()` do not exist. Do not add them. Do not add `_enabled` flags or channel checks inside `Logger`. This was removed intentionally -- see `hook/Logger.cs` header comment.
- **When replicating Root UI elements, ALWAYS examine Root's source first** -- Root's .NET assemblies are fully decompilable via ILSpy. Decompiled sources live in `research/ilspy-dumps/`. Don't reinvent controls (scrollbars, themes, etc.) from scratch when Root already has working implementations you can instantiate directly or study for exact behavior. Example: `RootScrollViewer` provides overlay scrollbars natively -- use it instead of hacking stock `ScrollViewer`.

## Related Repos

- **Public scaffold**: `The-Uprooted-Project/uprooted` -- TypeScript source, plugin API, theme engine
- These repos are **strictly separate** -- never copy, reference, or leak code/commits between them
