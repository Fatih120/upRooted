---
name: ok
description: Pull latest changes, show git status and recent commits — run this at the start of every session
allowed-tools:
  - Bash
  - Read
---

# Sync

Start-of-session sync ritual. Run this before doing any work.

## Instructions

1. Run `git pull` to fetch and merge any changes from the other contributor.

2. Run `git status` to show the current working tree state (modified files, untracked files, ahead/behind).

3. Run `git log --oneline -10` to show recent commit history.

4. Report a brief summary to the user:
   - Whether pull brought in new changes (and how many commits)
   - Any uncommitted local changes
   - The last few commit messages for context
   - Any warnings (e.g., merge conflicts, diverged branches)
