---
name: hi
description: Read project context — NEW-SESSION.md + TASKS.md — to orient yourself in the Uprooted codebase
allowed-tools:
  - Read
  - Glob
---

# Onboard

Orient yourself in the Uprooted codebase with **two file reads**, give a short summary, then **wait for the user to give instructions**.

## Phase 1: Orient (2 files only)

1. Read these two files **in parallel**:
   - `NEW-SESSION.md` — dispatch table, file map, architecture one-liner, startup phases
   - `TASKS.md` — active task board with priority-ordered work items

2. **Give a short summary** (not a wall of text):

   - **Version** — current version from NEW-SESSION.md
   - **Recent work** — 2-3 bullet points from TASKS.md "Done" section (most recent)
   - **Top pending tasks** — 3-4 from TASKS.md "Ready to Build" (highest priority first)

   Do NOT include: injection layer descriptions, ILSpy stats, plugin roadmap, dev commands, architecture explanations. The user already knows these.

3. **STOP. Wait for the user to give instructions.** Do NOT ask what to work on. Do NOT use AskUserQuestion. Just present the summary and wait.

## Phase 2: Deep-Read (triggered by user's task, not by /hi)

When the user states their task:

4. Use `NEW-SESSION.md` § Task Dispatch Table to identify which docs to read for the task.

5. Read those docs (and only relevant sections of large files). Also read `hook/SESSION_STATE.md` if working on `hook/` code.

6. **Report what you loaded** — briefly list the docs and any quirks/patterns directly relevant to the task. Confirm you are ready to begin work.
