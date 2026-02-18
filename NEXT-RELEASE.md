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
  - New files: `hook/MessageLogger.cs` (982 lines), `hook/MessageStore.cs` (216 lines)
- **Reusable settings toggle component** — `BuildSettingsToggle` in ContentPages for any boolean plugin setting (pill toggle with label + description)
- **TUI installer mode selector** — running the installer without `--install`/`--uninstall`/`--repair` flags now shows an interactive menu with arrow key navigation (Install / Uninstall / Repair)

### Changed

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
