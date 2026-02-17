---
name: ok
description: Pull latest, verify builds, run tests, review changes — the full preflight check before committing
allowed-tools:
  - Bash
  - Read
  - Grep
  - Glob
  - Task
---

# OK — Preflight

Session sync + build verification + test + code review in one command. Run this before doing any work, and again before committing.

## Instructions

### Phase 1: Git sync

1. Run `git pull` to fetch and merge any changes from the other contributor.

2. Run `git status` to show the current working tree state (modified files, untracked files, ahead/behind).

3. Run `git log --oneline -10` to show recent commit history.

4. Report a brief sync summary:
   - Whether pull brought in new changes (and how many commits)
   - Any uncommitted local changes
   - The last few commit messages for context
   - Any warnings (e.g., merge conflicts, diverged branches)

### Phase 2: Build verification

Only run this phase if there are uncommitted changes to source files (`hook/`, `src/`, `installer/`). If the tree is clean, skip to the summary.

5. **C# hook build** — Run `dotnet build hook/ -c Release`. Report pass/fail. If it fails, show the first error and stop (no point continuing with a broken build).

6. **TypeScript build** — Run `pnpm build` from the repo root. Report pass/fail. If it fails, show the error.

7. **C# tests** — Run `dotnet test tests/UprootedTests/ --no-restore --verbosity quiet`. Report pass/fail and test count.

### Phase 3: Code review

Only run this phase if there are uncommitted changes to `hook/` or `src/` files.

8. Invoke the `uprooted-reviewer` agent on the current changes. This checks critical rule violations, coding conventions, and architectural constraints.

9. Surface any CRITICAL or WARNING findings from the reviewer. INFO findings can be mentioned briefly.

### Phase 4: Summary

10. Report a consolidated summary:
    - **Sync**: clean / incoming changes / conflicts
    - **Build**: hook pass/fail, TS pass/fail (or skipped if no changes)
    - **Tests**: pass/fail with count (or skipped)
    - **Review**: clean / N findings by severity (or skipped)
    - **Verdict**: "Ready to commit" or "Issues to resolve" with the blocking items listed

If everything passes, end with: "You're OK."
