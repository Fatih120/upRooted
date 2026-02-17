---
name: session-state
description: Update SESSION_STATE.md and NEW-SESSION.md to reflect current codebase state after a work cycle
allowed-tools:
  - Bash
  - Read
  - Edit
  - Grep
  - Glob
  - AskUserQuestion
---

# Session State Update

After a work cycle, update `hook/SESSION_STATE.md` and the "Current State" section of `NEW-SESSION.md` to reflect what actually happened.

## Instructions

### Step 1: Gather current state

Read these in parallel to understand what changed:

1. `git log --oneline -20` — recent commit history
2. `git diff HEAD~10 --stat` — files changed in recent commits (adjust range as needed to cover the current work cycle)
3. `hook/SESSION_STATE.md` — current content to compare against
4. `NEW-SESSION.md` lines 96-121 — the "Current State" section

### Step 2: Identify what's stale

Compare the SESSION_STATE.md content against the git history and current source files. Flag sections that are out of date:

- **Release version** — does it match the version in `hook/UprootedSettings.cs`?
- **Recent work** — does it describe work that's already captured, or is new work missing?
- **Files Modified** — does the list match what actually changed?
- **Next Steps** — are any completed? Are there new ones?
- **Startup Phases** — has the phase table changed?
- **Deployment section** — are paths and commands still accurate?

### Step 3: Draft updates

Draft the updated `hook/SESSION_STATE.md` with:
- Correct release version
- Updated "Recent work" reflecting commits since last update
- Updated "Files Modified in This Session" based on `git diff --stat`
- Updated "Next Steps" — move completed items out, add new ones from ROADMAP.md or discovered during this cycle
- Keep sections that are still accurate unchanged

Also draft the matching update for `NEW-SESSION.md` section 6 ("Current State"), which should mirror the key points from SESSION_STATE.md:
- Version
- Recent work summary (brief)
- Known issues

### Step 4: Present diff

Show the user what will change in each file. Use a clear before/after format. Ask for approval before applying edits.

### Step 5: Apply

After approval, apply the edits to both files. Update the "Last updated" date in NEW-SESSION.md's footer if present.

### Step 6: Summary

Report what was updated and remind the user to commit these changes (they're documentation, not code — a `docs:` commit type is appropriate).
