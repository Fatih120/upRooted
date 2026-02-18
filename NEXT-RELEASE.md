# Next Release

> Changes since v0.3.6-rc. This file is replaced each release.

## v0.3.7-rc

### Fixed

- **Custom theme color swatches** — accent and background swatches in the custom theme section now display the correct color. The ThemeEngine visual tree walker was recoloring them because they lacked the `uprooted-no-recolor` tag (which the ping color swatch already had). Both swatches now tagged correctly in `ContentPages.cs`.
- **Auto-updater dev channel** — developer channel now correctly detects pre-release versions. Was using `/releases/latest` which only returns non-prerelease releases. Fixed by switching to `/releases?per_page=1` which returns the most recent release including pre-releases.

### Removed

- **About Themes card** — removed informational "ABOUT THEMES" card from the bottom of the Themes settings page.

### Infrastructure

- **CI: dynamic version** — build workflows no longer hardcode the version. Version is read from `package.json` by default; accepts an optional `version` input on `workflow_dispatch`. Releases only publish when triggered manually — push builds run the full pipeline and upload CI artifacts only.
- **CI: prerelease flag** — `workflow_dispatch` now accepts a `prerelease` boolean input (defaults `true`) to control whether the GitHub release is marked as a pre-release.
- **Unit testing expanded** — 170 tests (113 new) across 5 modules. New: `ClearUrlsEngineTests` (58), `UprootedSettingsTests` (22), `MessageStoreTests` (18) covering all 33 tracking params, INI parsing edge cases, and flat-file persistence. Test stubs for `Logger`, `PlatformPaths`, `AvaloniaReflection`, `VisualTreeWalker` eliminate disk I/O in tests.
- **Docker test sandbox** — `tests/Dockerfile.unittest` runs all unit tests in a clean `mcr.microsoft.com/dotnet/sdk:10.0` container with XPlat coverage. `tests/run-docker-tests.sh` builds image and extracts coverage to `tests/coverage/`.
- **Linux installer sandbox** — `tests/docker-installer/Dockerfile` tests the full `install-uprooted-linux.sh` flow in Ubuntu 24.04: curl shim intercepts the GitHub artifact download, fake Root.AppImage + profile directory provided, 14-point verification script checks env vars, wrapper script, and HTML patch injection.
