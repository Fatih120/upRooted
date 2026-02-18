# Next Release

> Changes since v0.3.5. This file is replaced each release.

## v0.3.6-rc

### Added

- **Message Logger plugin** — detects deleted and edited messages in chat, preserves original content with visual indicators
  - Deletion detection via `ObservableCollection` `CollectionChanged` subscription (Remove events = real deletions; channel switches detected separately via ItemsSource swap/Reset)
  - Edit detection via content snapshot comparison on each poll tick
  - Deleted messages re-injected into chat as red-tinted inline panels with author, timestamp, and original content
  - Edit history accessible per message (timestamped list of previous versions)
  - Flat-file persistence (`uprooted-message-log.dat`) — pipe-delimited, append-only, URI-encoded fields
  - Buffered flush every 5 seconds, automatic retention enforcement (configurable max messages)
  - Right-click context menu on logged messages for "Clear message history"
  - Settings page: Log Deletes toggle, Log Edits toggle, Ignore Own Messages toggle, Max Messages retention limit
  - New files: `hook/MessageLogger.cs` (1185 lines), `hook/MessageStore.cs` (232 lines)
- **Auto-updater** — in-process update checker that polls GitHub releases, downloads updated files to a staging directory, verifies integrity, then overwrites in-place. Changes take effect on next Root restart.
  - Periodic background checks (every 6 hours, throttled by last-check timestamp)
  - Manual "Check for Updates" button in About → UPDATES card
  - Version comparison supporting pre-release suffixes (e.g. `0.3.6rc` < `0.3.6`)
  - HTTP via reflection (same trimming-safe pattern as LinkEmbedEngine)
  - Settings: auto-check toggle, notification toggle, persisted last-check timestamp
  - New file: `hook/AutoUpdater.cs`
- **Developer update channel** — password-gated channel that pulls pre-release builds from the private repo instead of public releases
  - Channel selector in About → UPDATES card (green "Stable" badge / gold "Developer" badge)
  - Switching to Developer requires password entry (SHA-256 validated, inline prompt with masked input)
  - Switching back to Stable is immediate (no password needed)
  - Developer channel uses authenticated GitHub API requests (XOR-obfuscated PAT embedded in binary, decrypted at runtime)
  - Status text shows "(Dev)" suffix when on Developer channel
  - Channel preference persisted in settings (`AutoUpdate.Channel`)
- **Reusable settings toggle component** — `BuildSettingsToggle` in ContentPages for any boolean plugin setting (pill toggle with label + description)
- **TUI installer mode selector** — running the installer without `--uninstall`/`--repair`/`--plain` flags now shows an interactive menu with arrow key navigation (Install / Uninstall / Repair)
- **ProfileBadgeInjector** — injects an "Uprooted Dev" badge into Root's profile popup overlay when update channel is set to Developer; 500ms timer polls all TopLevel windows for new popups
  - Heuristic popup detection: avatar Image/Ellipse + username TextBlock + "Roles" header
  - Diagnostic tree dump on first popup detection (logged to uprooted-hook.log) for iterative refinement
  - New file: `hook/ProfileBadgeInjector.cs`, Phase 4.5e in `StartupHook.cs` (25s delay)
- **Restart banners** — prominent "Restart Root to apply changes" notices with a **Restart** button that relaunches Root
  - Plugins page: amber banner appears when any plugin state diverges from initial; disappears if user reverts all changes back to original state (themes excluded — they apply live)
  - Updates section: green banner appears after auto-updater applies an update
  - Restart button launches new Root.exe via `Process.Start` and calls `Environment.Exit(0)`
- **Diagnostics "Open" button** — the DIAGNOSTICS card on the About page now shows an "Open" button next to the log file path that opens the file in Explorer (Windows) or the containing directory (Linux)

### Changed

- **Sentry Blocker**: testing status promoted Alpha → Beta
- **Link Embeds**: testing status promoted Alpha → Beta
- **Sidebar: Uprooted section repositioned above App Settings** — the UPROOTED nav section (About, Plugins, Themes) now appears between the USER SETTINGS and APP SETTINGS sections instead of at the bottom of the sidebar, so users don't have to scroll to reach Uprooted settings. Uses `FindAppSettingsInsertionPoint` to walk up from the "APP SETTINGS" TextBlock and insert into the items panel at the correct index. Falls back to appending at the end if the insertion point can't be determined.
- **Linux installer: smarter Root auto-detection** — `find_root()` now uses 7 search strategies instead of just hardcoded paths:
  1. Exact well-known paths (fastest, same as before)
  2. Glob patterns (`Root*.AppImage`) in common directories — catches versioned filenames like `Root-0.9.92.AppImage`
  3. `.desktop` file search — scans application directories for any desktop entry mentioning Root, extracts `Exec=` path
  4. Running process check — reads `/proc/*/exe` symlinks for any active Root process
  5. PATH lookup via `command -v`
  6. `locate` database (fast indexed search, case-insensitive)
  7. Shallow `find` in `$HOME` (depth 4, case-insensitive, last resort)
- **Rust installer: matching Root auto-detection** — `detection.rs` `get_root_exe_path()` updated with glob, `.desktop` file, `/proc`, and PATH strategies matching the bash installer
- Version bump to 0.3.6-rc across all components (package.json, Cargo.toml, PKGBUILD, installer scripts, plugin versions)

### Documentation

- Added `docs/plugins/builtin/message-logger.md` — design doc for the Message Logger plugin
- Updated architecture docs, hook reference, installer reference, and plugin getting-started guide for v0.3.6-rc
- Added plugin roadmap references to TASKS.md and NEW-SESSION.md
- Build artifact directories added to `.gitignore`
