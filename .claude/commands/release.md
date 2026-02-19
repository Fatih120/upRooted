---
name: release
description: Full release pipeline — doc sweep, version bump, build verification, changelog audit, commit, tag, push
allowed-tools:
  - Bash
  - Read
  - Write
  - Edit
  - Grep
  - Glob
  - Task
  - AskUserQuestion
args:
  - name: version
    description: Target version number (e.g. 0.5.0, 0.5.0-rc)
    required: true
---

# Release Pipeline

Full release preparation, verification, and ship. Combines the work of `/ok` (documentation sweep) and `/nose` (version bump) with build verification, changelog audit, and two explicit user gates before anything is committed or pushed.

**This command does NOT call `/ok` or `/nose`** — it inlines and adapts their logic for release context (broader scope, automatic fixes instead of per-file questions).

## Phase 1: Pre-flight

1. Run `git pull` to ensure we have the latest.

2. Run `git status` to check for uncommitted changes. If there are uncommitted changes:
   - Show them to the user.
   - Ask via `AskUserQuestion`: "There are uncommitted changes. How should we handle them?"
     - **Abort** — stop the release, user deals with changes first
     - **Continue anyway** — user confirms changes are expected (e.g., they're part of the release)
   - If abort, stop entirely.

3. Run `git log --oneline -10` to confirm we are on `main` and see recent history.

4. Read `hook/StartupHook.cs` and find the current version string (the `CurrentVersion` const or startup banner). This is the **previous version** — needed for find-and-replace in Phase 3 and for changelog compare links. Verify `$ARGUMENTS.version` is different from it. If they match, warn the user: "Version is already set to X — nothing to bump. Continue anyway?" If the user declines, stop.

5. Detect pre-release: if `$ARGUMENTS.version` contains a hyphen (e.g., `0.5.0-rc`, `0.5.0-beta.1`), set `IS_PRERELEASE=true`. This affects the GitHub Actions reminder in Phase 7.

---

## Phase 2: Documentation Sweep

Unlike `/ok` which scopes to recent session commits, this sweep is **comprehensive** — audit all release-critical documentation for accuracy.

### 2a. Changelog

Read `CHANGELOG.md`. Check:
- Does the current (top) version section contain all notable changes since the previous release? Cross-reference with `git log --oneline <previous-tag>..HEAD`.
- Is the date on the current version section set to today's date? If not, update it.
- Are entries properly categorized (Added, Changed, Fixed, Removed, Infrastructure, Documentation)?
- Does a compare link exist at the bottom for the current version?

If the top section's version header doesn't match `$ARGUMENTS.version`, note this — it will be updated in Phase 3.

### 2b. Public changelog

Read `CHANGELOG_PUBLIC.md`. Verify:
- The top version section matches the feature set in `CHANGELOG.md` (user-friendly language, no internal details like file paths).
- Has the correct date (today).
- Uses the established format (New, Improvements, Fixes, Linux).

### 2c. Next release notes

Read `NEXT-RELEASE.md`. Check:
- If it says "(No changes yet)" but there ARE changes since the last tag (check `git log`), flag this as a **warning** — the content should have been incorporated into the changelogs. Ask the user whether to continue or abort to fix changelogs first.
- If it has content, verify that content appears in `CHANGELOG.md`. If not, flag it.

### 2d. Tasks

Read `TASKS.md`. Check:
- Are there items in "Up Next" that were actually completed? Move them to "Done" with today's date.
- Remove any items from "Done" that are duplicated or clearly wrong.

### 2e. Session state

Read `hook/SESSION_STATE.md`. Check:
- Are "Files Modified Recently" and "Next Steps" current?
- The release header will be updated in Phase 3.

### 2f. New session reference

Read `NEW-SESSION.md` sections 1, 4, and 6:
- Section 1: version will be updated in Phase 3.
- Section 4 (File Map): spot-check line counts for any hook `.cs` files that changed recently — run `wc -l` on them and compare.
- Section 6 (Current State): does it match reality?

### 2g. Repository structure

Read the `CLAUDE.md` Repository Structure tree. Quick-check: are there `.cs` files listed in the tree that don't exist, or files in `hook/` that are missing from the tree? Run `ls hook/*.cs` to compare.

### 2h. Apply fixes

For any stale content found above, apply surgical edits. Do not rewrite sections that are already accurate. Priority:
1. `CHANGELOG.md` — must be complete and accurate
2. `CHANGELOG_PUBLIC.md` — must mirror CHANGELOG in user-friendly form
3. `TASKS.md` — completed items moved to Done
4. `hook/SESSION_STATE.md`, `NEW-SESSION.md`, `CLAUDE.md` — accuracy fixes only

---

## Phase 3: Version Bump

### 3a. Content freshness audit

Read and audit these files for stale content beyond just the version number:

| File | What goes stale | How to check |
|------|----------------|--------------|
| `hook/SESSION_STATE.md` | Release version, recent work, next steps, files modified | Compare against git history |
| `NEW-SESSION.md` | Current State section, version references | Should match SESSION_STATE.md |
| `docs/ROADMAP.md` | Compatibility matrix, known issues | Check if issues listed are still open |
| `docs/framework/HOOK_REFERENCE.md` | Code examples, property lists | Spot-check a few against source |
| `docs/install/INSTALLATION.md` | Log output examples | Check against StartupHook.cs |

Unlike `/nose` which asks the user per-file, **fix stale content in-place** — this is a release, everything must be accurate. Only skip if the staleness is cosmetic and immaterial to the release.

### 3b. Find all version references

Grep for the previous version string across all source, script, doc, and config files.

**Exclude from edits** (third-party versions or historical entries):
- `pnpm-lock.yaml`
- `installer/src-tauri/Cargo.lock`
- `node_modules/`
- `.git/`
- `CHANGELOG.md` (historical version references must not change)
- `CHANGELOG_PUBLIC.md` (same)

### 3c. Update version references

Update all files from the previous version to `$ARGUMENTS.version`:

#### Source code
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

#### Scripts and installers
| File | What to update |
|------|---------------|
| `Install-Uprooted.ps1` | Banner text |
| `Uninstall-Uprooted.ps1` | Banner text |
| `scripts/install-hook.ps1` | Settings default version |
| `install-uprooted-linux.sh` | Comment header + `VERSION=` variable |
| `packaging/PKGBUILD` | `pkgver=` |
| `installer/src-tauri/Cargo.toml` | `version =` in `[package]` section |

#### Site and README
| File | What to update |
|------|---------------|
| `README.md` | Badge URL version string |
| `site/src/pages/index.astro` | `<span class="version">` |

#### Documentation
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

**Note:** This file list is a known baseline. The grep in 3b may discover additional files — update those too. Conversely, if any file from this list no longer exists or no longer contains a version reference, skip it silently.

### 3d. Update changelog headers

Update `CHANGELOG.md`:
- If the top section header doesn't show `$ARGUMENTS.version`, update it.
- Ensure the date is today.
- Add or update the compare link at the bottom:
  ```
  [<version>]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v<previous>...v<version>
  ```

Update `CHANGELOG_PUBLIC.md`:
- Ensure the top section shows `v$ARGUMENTS.version` with today's date.

### 3e. Verify no stale refs

Run a verification grep to confirm no references to the old version remain (excluding Cargo.lock, pnpm-lock.yaml, node_modules, `.git/`, and changelog historical entries).

### 3f. Report

Tell the user:
- How many files were updated
- Any files that were skipped (and why)
- Any content rewritten beyond just the version number

---

## GATE 1: User Approval

Present a summary to the user via `AskUserQuestion`:

**Show:**
- Documentation sweep results (files updated, issues found and fixed)
- Version bump results (N files updated from `old` to `new`, any stale refs remaining, any files skipped)

**Options:**
- **Proceed to build verification** — continue to Phase 4
- **Review changes first** — run `git diff` and show the output, then ask again
- **Abort** — stop entirely (user can `git checkout .` to undo)

If the user chooses "Review changes first", show `git diff --stat` followed by the full diff, then re-present the gate question.

---

## Phase 4: Build Verification

### 4a. C# hook build

```bash
dotnet build hook/ -c Release
```

If this fails, **stop and report the error**. Do not proceed.

### 4b. C# tests

```bash
dotnet test tests/
```

If any tests fail, **stop and report**. Do not proceed.

### 4c. TypeScript build

Check if `pnpm` is available and `src/` exists:

```bash
pnpm build
```

If pnpm is not installed or the command fails, report the failure. Ask the user whether to proceed without the TypeScript build or abort.

### 4d. Verify build outputs

Check that these files exist and are non-empty:
- `hook/bin/Release/net10.0/UprootedHook.dll`
- `dist/uprooted-preload.js` (only if TypeScript build ran)
- `dist/uprooted.css` (only if TypeScript build ran)

If any expected output is missing, report and stop.

---

## Phase 5: Release Artifact Audit

### 5a. Changelog final check

Read `CHANGELOG.md` and verify:
- Top section header matches `$ARGUMENTS.version` with today's date
- Section has actual content (not empty)
- Compare link exists at the bottom for this version
- Previous version's compare link endpoint uses the correct tag format

### 5b. Public changelog final check

Read `CHANGELOG_PUBLIC.md` and verify:
- Top section header matches `v$ARGUMENTS.version` with today's date
- Content is present

### 5c. Version consistency spot-check

Read these 4 files and verify the version matches `$ARGUMENTS.version`:
- `hook/StartupHook.cs` — `CurrentVersion` const
- `hook/UprootedSettings.cs` — `Version` property default
- `package.json` — `"version"` field
- `installer/src-tauri/Cargo.toml` — `version =` in `[package]`

If any mismatch, **stop and report**.

### 5d. Git tag collision check

```bash
git tag -l "v$ARGUMENTS.version"
```

If the tag already exists, ask the user via `AskUserQuestion`:
- **Delete and recreate** — `git tag -d "v<version>"` then continue
- **Abort** — stop entirely

---

## GATE 2: Ship Approval

Present a comprehensive summary to the user via `AskUserQuestion`:

**Show:**

```
Build verification:
  C# hook:       BUILD_RESULT
  C# tests:      TEST_RESULT (N tests passed)
  TypeScript:    TS_RESULT

Release artifact audit:
  CHANGELOG.md:        STATUS
  CHANGELOG_PUBLIC.md: STATUS
  Version consistency:  STATUS
  Git tag v<version>:  STATUS

Pre-release: yes/no

Files to be committed:
  [git status --short]
```

**Options:**
- **Ship it** — commit, tag, and push
- **Review diff** — show `git diff --stat` and full diff, then ask again
- **Abort** — stop (changes remain uncommitted, user can revert)

---

## Phase 6: Commit, Tag, Push

### 6a. Reset NEXT-RELEASE.md

Overwrite with:

```markdown
# Next Release

> Changes since v<version>. This file is replaced each release.

(No changes yet)
```

### 6b. Stage all changes

```bash
git add -A
```

At this point only release-related files should be modified — the user has reviewed the diff at Gate 2.

### 6c. Commit

```bash
git commit -m "$(cat <<'EOF'
release: v<version>

Co-Authored-By: Claude Opus 4.6 <noreply@anthropic.com>
EOF
)"
```

### 6d. Tag

```bash
git tag -a "v<version>" -m "Release v<version>"
```

### 6e. Push

```bash
git push && git push --tags
```

If push fails (e.g., remote has new commits), **stop and report**. Do NOT force-push. The user must resolve manually (`git pull --rebase`, then re-push).

---

## Phase 7: Post-Release Summary

Report to the user:

```
Release v<version> shipped.

  Commit: <short hash>
  Tag:    v<version>
  Branch: main -> origin/main

Next steps:
  1. GitHub Actions -> "Build Installer" -> Run workflow:
     - Version: <version> (or leave blank to read from package.json)
     - Pre-release: <true if IS_PRERELEASE, false otherwise>
  2. GitHub Actions -> "Build Linux Installer" -> Run workflow (same inputs)
  3. After both workflows complete, verify GitHub releases page has:
     - Uprooted-<version>-Setup.exe
     - auto-update.uprpkg
     - Linux artifacts (Uprooted-<version>-linux-amd64)
  4. If stable release, verify public repo release notes match CHANGELOG_PUBLIC.md.
```

---

## Error Handling

At every phase, if something fails:

1. **Report clearly** what failed and why.
2. **Do NOT proceed** to the next phase.
3. **Do NOT attempt to undo** previous phases' changes — the user can `git checkout .` or `git stash` if needed.
4. **Suggest a fix** if obvious (e.g., "Build failed due to syntax error on line 42").

The two gates ensure the user has explicit control. No git operations (commit, tag, push) happen without passing both gates.
