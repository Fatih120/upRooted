---
name: ok
description: Post-work documentation sweep — update session state, changelog, tasks, NEW-SESSION, and all stale docs to match committed code
allowed-tools:
  - Bash
  - Read
  - Write
  - Edit
  - Grep
  - Glob
  - Task
---

# OK — Documentation Sweep

After work has been committed and pushed, run this command to bring all documentation into sync with the codebase. Scoped to what actually changed — only update docs that are stale relative to recent commits.

**Multi-instance awareness:** Other Claude instances may be working in this environment concurrently. Only touch documentation related to changes YOU made or that are visibly stale relative to committed code. Do not rewrite docs wholesale. Do not touch files that are already accurate. If you see uncommitted changes in files you didn't modify, leave them alone — another instance may be mid-work.

## Instructions

### Phase 1: Scope your changes

1. Run `git log --oneline -20` to see recent commit history.

2. Identify which commits are from your current work session (you'll recognize them from the conversation context). If unclear, ask the user which commits to cover.

3. Run `git diff <base>..HEAD --stat` scoped to your session's commits to see exactly what source files changed.

4. For any hook `.cs` files that changed in your commits, run `wc -l` to get current line counts.

### Phase 2: Read only affected docs

Read the docs that could be stale based on your changes. Skip docs unrelated to your work.

- **Always read:** `CHANGELOG.md`, `TASKS.md`
- **If hook/ files changed:** `hook/SESSION_STATE.md`, the file map in `NEW-SESSION.md` (section 4), `CLAUDE.md` (Repository Structure tree)
- **If features were added/completed:** `NEW-SESSION.md` section 6 (Current State)
- **If version was bumped:** check version references across all docs
- **If files were added/removed/renamed:** `CLAUDE.md` tree, `NEW-SESSION.md` file map, `docs/framework/HOOK_REFERENCE.md`

### Phase 3: Identify stale content

Compare what the docs say against what actually happened. Only flag items that are genuinely wrong or missing — not "could be slightly better" rewording.

Check for:
- **Line counts** — are file map tables accurate for files that changed?
- **Feature descriptions** — do file/feature descriptions match current functionality?
- **Changelog** — are all committed changes reflected? Are compare links present?
- **Tasks** — should any "Up Next" items move to "Done"?
- **Session state** — does it reflect the latest work, correct "Files Modified", correct "Next Steps"?
- **Known issues** — have any been resolved? Are there new ones?

Build a concise list of stale items.

### Phase 4: Apply targeted updates

Work through the stale items. For each update:

- Edit in place — surgical changes only
- Preserve existing formatting and style
- Do not rewrite sections that are already accurate
- Do not touch files you have no reason to update

**Priority order:**
1. `CHANGELOG.md` — must accurately reflect commits, include compare links
2. `TASKS.md` — move completed items to Done
3. `hook/SESSION_STATE.md` — version, recent work, files modified, next steps
4. `NEW-SESSION.md` — file map line counts, section 6 state
5. `CLAUDE.md` — repository structure tree (only if files added/removed/renamed)
6. Other docs only if they reference something your changes made stale

### Phase 5: Summary

Report what was updated:

- List each file edited and what changed (1 line per file)
- List files checked but found accurate (brief)
- Flag anything you couldn't verify (e.g., "unknown if X feature is working — needs user confirmation")
- If everything is already in sync, say so: "Docs are up to date."
