---
name: nose
description: Bump all version references across the codebase to a new version number
allowed-tools:
  - Bash
  - Read
  - Edit
  - Grep
  - Glob
  - AskUserQuestion
args:
  - name: version
    description: Target version number (e.g. 0.3.3)
    required: true
---

# Version Bump

Update all Uprooted version references across the codebase to the target version.

**Important:** This is not a blind find-and-replace. Before updating any file's version string, verify the file's surrounding content is still accurate. A version bump on stale content is worse than no bump at all.

## Phase 1: Audit content freshness

Before touching version strings, read and audit these files for stale content. If any file's content is outdated (not just the version number), flag it to the user and ask whether to update the content, skip the file, or proceed with just the version bump.

### Files that go stale frequently

| File | What goes stale | How to check |
|------|----------------|--------------|
| `hook/SESSION_STATE.md` | Release version, "Critical Finding", "Recent work", "Next Steps", "Files Modified" sections | Compare against recent `git log --oneline -20` and current codebase state |
| `NEW-SESSION.md` | "Current State" section (lines ~96-121) which mirrors SESSION_STATE.md | Should match SESSION_STATE.md content |
| `docs/ROADMAP.md` | Compatibility matrix, known issues, planned features | Check if issues listed are still open or have been resolved |
| `docs/framework/HOOK_REFERENCE.md` | Code examples, property lists, file descriptions | Spot-check a few code examples against actual source files |
| `docs/install/INSTALLATION.md` | Log output examples, step descriptions | Check log format matches current StartupHook.cs output |

For each flagged file, ask the user:
- **Update content + version** — rewrite the stale sections to match current state, then bump version
- **Skip file** — leave it alone entirely (don't bump version on stale content)
- **Version only** — user confirms the content is fine despite looking stale, just bump the number

Only proceed to Phase 2 after the user has resolved all flagged files.

If no files have stale content beyond the version number, proceed directly to Phase 2.

## Phase 2: Find all version references

Run a grep to find all current version references. Search for patterns matching the previous version strings across all source, script, doc, and config files.

Exclude these from edits (they contain third-party dependency versions, not Uprooted's version):
- `pnpm-lock.yaml`
- `installer/src-tauri/Cargo.lock`
- `node_modules/`
- `.git/`

## Phase 3: Update version references

Update all files to `$ARGUMENTS.version`:

### Source code
| File | What to update |
|------|---------------|
| `hook/UprootedSettings.cs` | `Version` property default |
| `hook/StartupHook.cs` | `CurrentVersion` const + startup log banner string |
| `hook/SidebarInjector.cs` | Version comments (3 occurrences) |
| `hook/ContentPages.cs` | `Version` field in all `PluginInfo` entries (use `replace_all`) |
| `hook/SESSION_STATE.md` | Release header |
| `package.json` | `"version"` field |
| `src/plugins/themes/index.ts` | `version` field |
| `src/plugins/sentry-blocker/index.ts` | `version` field |
| `src/plugins/link-embeds/index.ts` | `version` field |
| `src/plugins/settings-panel/index.ts` | `version` field |

### Scripts and installers
| File | What to update |
|------|---------------|
| `Install-Uprooted.ps1` | Banner text |
| `Uninstall-Uprooted.ps1` | Banner text |
| `scripts/install-hook.ps1` | Settings default version |
| `install-uprooted-linux.sh` | Comment header + `VERSION=` variable |
| `packaging/PKGBUILD` | `pkgver=` |
| `installer/src-tauri/Cargo.toml` | `version =` in `[package]` section |

### Site and README
| File | What to update |
|------|---------------|
| `README.md` | Badge URL version string |
| `site/src/pages/index.astro` | `<span class="version">` |

### Documentation
| File | What to update |
|------|---------------|
| `NEW-SESSION.md` | Version line, Versions line, footer |
| `docs/ROADMAP.md` | Compatibility matrix current version |
| `docs/install/BUILD.md` | Output filenames, "Where Versions Live" table, version propagation text |
| `docs/install/INSTALLATION.md` | All installer filenames and log output examples |
| `docs/plugins/ROOT_ENVIRONMENT.md` | `__UPROOTED_VERSION__` example, compatibility table |
| `docs/framework/ARCHITECTURE.md` | package.json comment, footer |
| `docs/framework/INSTALLER.md` | Version field, TUI version display |
| `docs/framework/HOOK_REFERENCE.md` | UprootedSettings code example, INI example |

**Note:** This file list is a known baseline. The grep in Phase 2 may discover additional files — update those too. Conversely, if any file from this list no longer exists or no longer contains a version reference, skip it silently.

## Phase 4: Verify

1. Run a verification grep to confirm no stale version references remain (excluding Cargo.lock, pnpm-lock.yaml, and node_modules).

2. Report a summary to the user:
   - How many files were updated
   - Any files that were skipped (and why)
   - Any content that was rewritten beyond just the version number
   - Whether any stale references remain
