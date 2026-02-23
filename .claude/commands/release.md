---
name: release
description: Full release pipeline — doc sweep, version bump, build verification, changelog audit, commit, tag, push, CI build monitoring, artifact verification
allowed-tools:
  - Bash
  - Read
  - Write
  - Edit
  - Grep
  - Glob
  - Task
  - AskUserQuestion
---

# Release Pipeline

Full release preparation, verification, and ship. Combines the work of `/ok` (documentation sweep) and `/nose` (version bump) with build verification, changelog audit, and two explicit user gates before anything is committed or pushed.

**This command does NOT call `/ok` or `/nose`** — it inlines and adapts their logic for release context (broader scope, automatic fixes instead of per-file questions).

## Three Release Channels

| Channel | Target Repo | Branch | Pre-release? | Package Managers | Auto-Updater Channel |
|---------|-------------|--------|--------------|------------------|---------------------|
| **stable** | `The-Uprooted-Project/uprooted` (public) | `main` | No | Yes (all) | `stable` |
| **canary** | `The-Uprooted-Project/uprooted-canary` (public) | `main` | Yes | No | `canary` |
| **dev** | `The-Uprooted-Project/uprooted-private` (private) | `dev` | Yes | No | `developer` |

## Argument Parsing

The user invokes: `/release <channel> <version>`

Parse `$ARGUMENTS` (the raw argument string) into two values:
- **CHANNEL** — first word: `stable`, `canary`, or `dev`
- **VERSION** — second word: the version number (e.g. `0.6.0`, `0.6.0-canary.1`, `0.6.0-dev.1`)

If `$ARGUMENTS` doesn't contain exactly two space-separated values, or CHANNEL is not one of `stable`/`canary`/`dev`, stop with:
```
Usage: /release <stable|canary|dev> <version>

Channels:
  stable  — public repo, main branch, triggers package manager updates
  canary  — canary repo, main branch, bleeding edge
  dev     — private repo, dev branch, invite only

Examples:
  /release stable 0.6.0
  /release canary 0.6.0-canary.1
  /release dev 0.6.0-dev.1
```

Derive:
- **BRANCH** = `dev` if CHANNEL is `dev`, otherwise `main`
- **IS_PRERELEASE** = `true` if CHANNEL is `canary` or `dev`, `false` for `stable`
- **TARGET_REPO**:
  - `stable` → `The-Uprooted-Project/uprooted`
  - `canary` → `The-Uprooted-Project/uprooted-canary`
  - `dev` → `The-Uprooted-Project/uprooted-private`

Throughout this document, **CHANNEL**, **VERSION**, **BRANCH**, and **TARGET_REPO** refer to these parsed values.

---

## Phase 1: Pre-flight

1. **Discover git remote name** — run `git remote -v` and find the remote pointing to `uprooted-private.git`. Store as `REMOTE`. Do NOT assume the remote is named `origin` — it may be `private` or something else.

2. **Switch to the correct branch** — if BRANCH is `dev` and we're not on `dev`, run `git checkout dev`. If BRANCH is `main` and we're not on `main`, run `git checkout main`.

3. Run `git pull $REMOTE $BRANCH` to ensure we have the latest.

4. Run `git status` to check for uncommitted changes. If there are uncommitted changes:
   - Show them to the user.
   - Ask via `AskUserQuestion`: "There are uncommitted changes. How should we handle them?"
     - **Abort** — stop the release, user deals with changes first
     - **Continue anyway** — user confirms changes are expected (e.g., they're part of the release)
   - If abort, stop entirely.

5. Run `git log --oneline -10` to confirm branch and see recent history.

6. Read `hook/StartupHook.cs` and find the current version string (the `CurrentVersion` const). This is the **previous version** (`PREV`). Verify `VERSION` is different from it. If they match, warn the user: "Version is already set to X — nothing to bump. Continue anyway?" If the user declines, stop.

---

## Phase 2: Documentation Sweep

Unlike `/ok` which scopes to recent session commits, this sweep is **comprehensive** — audit all release-critical documentation for accuracy.

### 2a. Changelog

Read `CHANGELOG.md`. Check:
- Does the current (top) version section contain all notable changes since the previous release? Cross-reference with `git log --oneline <previous-tag>..HEAD`.
- Is the date on the current version section set to today's date? If not, update it.
- Are entries properly categorized (Added, Changed, Fixed, Removed, Infrastructure, Documentation)?
- Does a compare link exist at the bottom for the current version?

If the top section's version header doesn't match `VERSION`, note this — it will be updated in Phase 3.

### 2b. Public changelog

Read `CHANGELOG_PUBLIC.md`. Verify:
- The top version section matches the feature set in `CHANGELOG.md` (user-friendly language, no internal details like file paths).
- Has the correct date (today).
- Uses the established format (New, Improvements, Fixes, Linux).

**Sensitivity guard:** `CHANGELOG_PUBLIC.md` and `NEXT-RELEASE.md` have HTML comments forbidding mention of obfuscation, ConfuserEx, or protected names. The doc sweep must respect these — never add obfuscation-related entries to these files.

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

### 2f. New session reference + file map

Read `NEW-SESSION.md` sections 1, 4, and 5:
- Section 1: version will be updated in Phase 3.
- Section 4 (File Map): run `wc -l hook/*.cs` and compare against the table. Auto-fix line counts that are off by more than 10%. Check for `.cs` files in `hook/` missing from the table — add them.
- Section 5 (Other Layers): spot-check TypeScript and Installer sections.

### 2g. Repository structure

Read the `CLAUDE.md` Repository Structure tree. Quick-check:
- Run `ls hook/*.cs` — are there files in `hook/` missing from the tree? Add them.
- Are there files listed in the tree that don't exist? Remove them.
- Are line counts in the tree wildly wrong? Fix them.

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

Run the same file-by-file version bump as `/nose` Phase 2. The file list is defined in `/nose` — refer to that skill for the canonical set. Key files:

**Source code:** `VERSION`, `package.json`, `installer/src-tauri/Cargo.toml`, `hook/UprootedSettings.cs`, `hook/StartupHook.cs`, `hook/ContentPages.cs` (all PluginInfo `replace_all`), `hook/SidebarInjector.cs`, `hook/SESSION_STATE.md`, `src/plugins/{themes,sentry-blocker,link-embeds,settings-panel}/index.ts`

**Scripts:** `Install-Uprooted.ps1`, `Uninstall-Uprooted.ps1`, `install-uprooted-linux.sh`, `packaging/PKGBUILD`

Note: `scripts/install-hook.ps1` reads version from `package.json` dynamically — no edit needed.

**Site and README:** `README.md`, `site/src/pages/index.astro` (if exists)

**Documentation:** `NEW-SESSION.md`, `docs/install/BUILD.md`, `docs/install/INSTALLATION.md`, `docs/plugins/ROOT_ENVIRONMENT.md`, `docs/framework/ARCHITECTURE.md`, `docs/framework/INSTALLER.md`, `docs/framework/HOOK_REFERENCE.md`, `docs/ROADMAP.md`, `CONTRIBUTING.md`

**Exclusions:** `pnpm-lock.yaml`, `Cargo.lock`, `node_modules/`, `.git/`, `package-lock.json`, changelog historical entries.

The grep in 3b may discover additional files — update those too. If any file from this list no longer exists or no longer contains a version reference, skip it silently.

### 3d. ForceDisableOnUpgrade

Read `hook/StartupHook.cs` and find the `ForceDisableOnUpgrade` dictionary. Check if `VERSION` (or its base version without `-rc` suffix) already has an entry.

If no entry exists, grep `hook/ContentPages.cs` for all `PluginInfo` entries with `TestingStatus = 0` (Experimental) or `TestingStatus = 1` (Alpha). Present the list to the user via `AskUserQuestion`:
- "Should any plugins be force-disabled for users upgrading to VERSION?" with options listing the experimental/alpha plugins.

If the user selects plugins, add a new entry to the `ForceDisableOnUpgrade` dictionary.

### 3e. Plugin metadata review

Grep `hook/ContentPages.cs` for:
1. `TestingStatus = 5` (Planned) — ask user if these should be promoted since the feature shipped.
2. Descriptions containing "Planned for a future release" or similar stale text — flag for update.
3. Any `HasSettings = false` on plugins that now have settings lightboxes — flag for correction.

### 3g. Update changelog headers

Run the same changelog stamping as `/nose` Phase 3:
- `CHANGELOG.md`: stamp `[Unreleased]` → `[VERSION] - today`, add compare link
- `CHANGELOG_PUBLIC.md`: update top section date
- `NEXT-RELEASE.md`: update header metadata

### 3h. Verify no stale refs

Run a verification grep to confirm no references to the old version remain (excluding Cargo.lock, pnpm-lock.yaml, node_modules, `.git/`, `package-lock.json`, and changelog historical entries).

### 3i. Report

Tell the user:
- How many files were updated
- Any files that were skipped (and why)
- Any content rewritten beyond just the version number
- ForceDisableOnUpgrade changes (if any)
- Plugin metadata changes (if any)

---

## GATE 1: User Approval

Present a summary to the user via `AskUserQuestion`:

**Show:**
- Release channel: `CHANNEL` (branch: `BRANCH`, target: `TARGET_REPO`)
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
dotnet build hook/UprootedHook.csproj -c Release
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
- Top section header matches `VERSION` with today's date
- Section has actual content (not empty)
- Compare link exists at the bottom for this version
- Previous version's compare link endpoint uses the correct tag format

### 5b. Public changelog final check

Read `CHANGELOG_PUBLIC.md` and verify:
- Top section header matches `v{VERSION}` with today's date
- Content is present

### 5c. Version consistency spot-check

Read these 5 files and verify the version matches `VERSION`:
- `VERSION` — entire file content
- `hook/StartupHook.cs` — `CurrentVersion` const
- `hook/UprootedSettings.cs` — `Version` property default
- `package.json` — `"version"` field
- `installer/src-tauri/Cargo.toml` — `version =` in `[package]`

If any mismatch, **stop and report**.

### 5d. Git tag collision check

```bash
git tag -l "v{VERSION}"
```

If the tag already exists, ask the user via `AskUserQuestion`:
- **Delete and recreate** — `git tag -d "v<version>"` then continue
- **Abort** — stop entirely

---

## GATE 2: Ship Approval

Present a comprehensive summary to the user via `AskUserQuestion`:

**Show:**

```
Channel: CHANNEL
  Branch: BRANCH -> TARGET_REPO
  Pre-release: IS_PRERELEASE

Build verification:
  C# hook:       BUILD_RESULT
  C# tests:      TEST_RESULT (N tests passed)
  TypeScript:    TS_RESULT

Release artifact audit:
  CHANGELOG.md:        STATUS
  CHANGELOG_PUBLIC.md: STATUS
  Version consistency:  STATUS
  Git tag v<version>:  STATUS

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
git commit -m "release: v<version> (<channel>)"
```

### 6d. Tag

```bash
git tag -a "v<version>" -m "Release v<version> (<channel> channel)"
```

### 6e. Push

Use the `REMOTE` discovered in Phase 1 (not hardcoded `origin`):

```bash
git push $REMOTE $BRANCH && git push $REMOTE --tags
```

If push fails (e.g., remote has new commits), **stop and report**. Do NOT force-push. The user must resolve manually (`git pull --rebase`, then re-push).

**Note:** Per CLAUDE.md, never add `Co-Authored-By` trailers. The commit is authored by whoever `git config user.name`/`user.email` returns.

---

## Phase 7: Post-Release Summary

Report to the user:

```
Release v<version> (<channel>) shipped.

  Commit: <short hash>
  Tag:    v<version>
  Branch: <BRANCH> -> $REMOTE/<BRANCH>
  Target: <TARGET_REPO>

Proceeding to trigger and monitor CI builds...
```

---

## Phase 8: Trigger, Monitor, and Verify CI Builds

This phase triggers the unified build workflow, monitors it to completion, and verifies all release artifacts are present on the target repo(s).

### 8a. Channel routing

| CHANNEL | Build from | Artifacts uploaded to | Package managers |
|---------|-----------|----------------------|------------------|
| `stable` | `main` | private repo + public repo (`uprooted`) | Yes |
| `canary` | `main` | private repo + canary repo (`uprooted-canary`) | No |
| `dev` | `dev` | private repo only | No |

### 8b. Detect or trigger workflow

First, check if push-triggered builds are already running for the release tag:

```bash
gh run list --limit=4 --json databaseId,name,status,headBranch,event
```

If runs matching the build workflow are already in progress, use those run IDs directly — do NOT re-trigger.

If no matching runs exist, trigger the unified build workflow via `gh workflow run`:

```bash
gh workflow run "Build" -f version=<version>
```

Wait 5 seconds, then find the run ID:

```bash
gh run list --workflow=build.yml --limit=1 --json databaseId,status
```

### 8c. Monitor build

Poll the workflow run every 30 seconds until complete. Use `gh run view <id> --json status,conclusion`.

Display a progress update each poll cycle:
```
Monitoring CI build...
  Build: <status> (<elapsed>)
```

If the run fails:
1. Show the failure immediately.
2. Run `gh run view <id> --log-failed` to fetch the failed step log.
3. Show the relevant error output to the user.
4. Ask via `AskUserQuestion`:
   - **Retry** — re-trigger the workflow
   - **Continue anyway** — proceed to artifact verification (partial release)
   - **Abort** — stop (release commit/tag already pushed; artifacts are incomplete)

Timeout: if the build hasn't completed after 30 minutes, warn the user and ask whether to keep waiting or proceed.

### 8d. Trigger release workflow

Once the build succeeds, the release.yml workflow should have been triggered by the release event. If not (e.g., for `workflow_dispatch`), trigger it:

```bash
gh workflow run "Release" -f channel=<CHANNEL> -f version=<VERSION>
```

Monitor this workflow the same way.

### 8e. Verify artifacts on target repo

Once builds succeed, verify all expected release assets exist on the target repo:

```bash
gh release view "v<version>" --repo <TARGET_REPO> --json assets --jq '.assets[].name'
```

**Expected assets:**

| # | Asset | Source |
|---|-------|--------|
| 1 | `Uprooted-<version>-windows-amd64.exe` | Build matrix |
| 2 | `Uprooted-<version>-linux-amd64` | Build matrix |
| 3 | `Uprooted-<version>-linux-arm64` | Build matrix |
| 4 | `Uprooted-<version>-macos-amd64` | Build matrix |
| 5 | `Uprooted-<version>-macos-arm64` | Build matrix |
| 6 | `uprooted_<version>_amd64.deb` | Package job |
| 7 | `uprooted_<version>_arm64.deb` | Package job |
| 8 | `Uprooted-<version>-x86_64.AppImage` | Package job |
| 9 | `auto-update.uprpkg` | Package job |
| 10 | `uprooted-linux-artifacts.tar.gz` | Package job |
| 11 | `install-uprooted-linux.sh` | Package job |
| 12 | `checksums.txt` | Package job |

Check each asset exists with non-zero size.

Also verify on the private repo if CHANNEL is not `dev` (since artifacts are always uploaded to private first).

### 8f. Verify release metadata

```bash
gh release view "v<version>" --repo <TARGET_REPO> --json tagName,isDraft,isPrerelease
```

Verify:
- `tagName` matches `v<version>`
- `isDraft` is `false`
- `isPrerelease` matches `IS_PRERELEASE`

### 8g. Final report

Present a comprehensive verification report:

```
CI Build & Artifact Verification — v<version> (<channel>)

Build workflow:    ✓ passed (<duration>)
Release workflow:  ✓ passed (<duration>)

Target: <TARGET_REPO>
  Release: v<version> | prerelease: <yes/no>
  Assets:
    ✓ Uprooted-<version>-windows-amd64.exe    (<size>)
    ✓ Uprooted-<version>-linux-amd64           (<size>)
    ✓ auto-update.uprpkg                        (<size>)
    ✓ checksums.txt                             (<size>)
    ... (all assets)

Package managers: <triggered/skipped>
  (stable only: Chocolatey, winget, Scoop, Homebrew, AUR, Cloudsmith)

Release v<version> (<channel>) is complete.
```

Use `✓` for present assets and `✗ MISSING` for absent ones. If any assets are missing, flag the report as incomplete.

---

## Error Handling

At every phase, if something fails:

1. **Report clearly** what failed and why.
2. **Do NOT proceed** to the next phase.
3. **Do NOT attempt to undo** previous phases' changes — the user can `git checkout .` or `git stash` if needed.
4. **Suggest a fix** if obvious (e.g., "Build failed due to syntax error on line 42").

The two gates ensure the user has explicit control. No git operations (commit, tag, push) happen without passing both gates.
