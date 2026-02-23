---
name: nose
description: Bump all version references across the codebase to a new version number and stamp changelog headers
allowed-tools:
  - Bash
  - Read
  - Edit
  - Grep
  - Glob
  - AskUserQuestion
args:
  - name: version
    description: Target version number (e.g. 0.5.0, 0.5.1-rc, 0.5.1-canary.1)
    required: true
---

# Version Bump + Changelog Stamp

Mechanical version bump across all Uprooted version references, plus changelog header stamping. No content review, no build verification, no git operations.

For the full release pipeline (doc sweep + build + CI + gates), use `/release` instead.

## Phase 1: Discover Current Version

Read `hook/StartupHook.cs` and extract the `CurrentVersion` const value. This is the **previous version**.

Verify `$ARGUMENTS` differs from the previous version. If they match, warn the user and stop.

Store both values:
- `PREV` = previous version (from StartupHook.cs)
- `VER` = target version (from `$ARGUMENTS`)

## Phase 2: Bump Version Strings

For each file below, read it first, then replace `PREV` with `VER` in the specified fields. If a file doesn't exist or doesn't contain the previous version, skip it silently.

### Source code

| File | What to update |
|------|---------------|
| `VERSION` | Entire file content (single line) |
| `package.json` | `"version"` field |
| `installer/src-tauri/Cargo.toml` | `version =` in `[package]` |
| `hook/UprootedSettings.cs` | `Version` property default string |
| `hook/StartupHook.cs` | `CurrentVersion` const string |
| `hook/ContentPages.cs` | `Version` in all `PluginInfo` entries (`replace_all`) |
| `hook/SidebarInjector.cs` | Version string in comments and display text (`replace_all`) |
| `hook/SESSION_STATE.md` | `## Release:` header |
| `src/plugins/themes/index.ts` | `version:` field |
| `src/plugins/sentry-blocker/index.ts` | `version:` field |
| `src/plugins/link-embeds/index.ts` | `version:` field |
| `src/plugins/settings-panel/index.ts` | `version:` field |

### Scripts and installers

| File | What to update |
|------|---------------|
| `Install-Uprooted.ps1` | Banner text |
| `Uninstall-Uprooted.ps1` | Banner text |
| `install-uprooted-linux.sh` | Comment header + `VERSION=` variable |
| `packaging/PKGBUILD` | `pkgver=` |

Note: `scripts/install-hook.ps1` reads version from `package.json` dynamically — no edit needed.

### Site and README

| File | What to update |
|------|---------------|
| `README.md` | Badge URL version string |
| `site/src/pages/index.astro` | `<span class="version">` (if file exists) |

### Documentation

Use `replace_all` for version strings. **Do NOT touch changelog historical entries** — only update references to the "current" version.

| File | What to update |
|------|---------------|
| `NEW-SESSION.md` | Version references + footer date (set to today) |
| `docs/install/BUILD.md` | Output filenames, version table, propagation text, footer |
| `docs/install/INSTALLATION.md` | Installer filenames, log output examples |
| `docs/plugins/ROOT_ENVIRONMENT.md` | `__UPROOTED_VERSION__` example, compatibility table |
| `docs/framework/ARCHITECTURE.md` | Footer date and version |
| `docs/framework/INSTALLER.md` | Version field, TUI display |
| `docs/framework/HOOK_REFERENCE.md` | Settings code example, INI example |
| `docs/ROADMAP.md` | Compatibility matrix |
| `CONTRIBUTING.md` | Footer |

### Exclusions

Never edit version strings in:
- `pnpm-lock.yaml`, `installer/src-tauri/Cargo.lock`
- `node_modules/`, `.git/`
- `CHANGELOG.md` historical entries (only the `[Unreleased]` header gets stamped in Phase 3)
- `CHANGELOG_PUBLIC.md` historical entries
- `tools/presence-api/package-lock.json`

## Phase 3: Stamp Changelog Headers

### CHANGELOG.md

1. If the top section header is `## [Unreleased]`, rename it to `## [VER] - YYYY-MM-DD` (today's date).
2. If the top section already shows `VER`, just ensure the date is today.
3. Add a compare link at the bottom of the file (before the first existing link):
   ```
   [VER]: https://github.com/The-Uprooted-Project/uprooted-private/compare/vPREV...vVER
   ```

### CHANGELOG_PUBLIC.md

If the top section header already contains `VER`, update the date to today. Otherwise leave it alone (content creation is a `/release` concern, not `/nose`).

### NEXT-RELEASE.md

Update the header metadata line to reference `VER`:
```
> Changes included in vVER. This file is replaced each release.
```
And the `# What's New` heading if present.

## Phase 4: Verify and Report

Run a verification grep for `PREV` across the repo, excluding: `Cargo.lock`, `pnpm-lock.yaml`, `node_modules/`, `.git/`, `package-lock.json`, and changelog historical entries.

Report to the user:
- How many files were updated
- Any files from the tables above that were skipped (and why)
- Any remaining references to `PREV` that were not updated (with file paths)
