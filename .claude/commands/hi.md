---
name: hi
description: Read project context — NEW-SESSION.md, then docs/INDEX.md — to orient yourself in the Uprooted codebase
allowed-tools:
  - Read
  - Glob
  - AskUserQuestion
---

# Onboard

Orient yourself in the Uprooted codebase with **minimal reads**, then deep-read only the docs relevant to today's task.

Two phases: **orient** (2 files → short summary → ask task) and **deep-read** (targeted docs only).

## Phase 1: Orient (2 files only)

1. Read these two files **in parallel**:
   - `NEW-SESSION.md` — quick-start reference card (architecture, critical rules, file map, current state). Internalize the critical rules and common pitfalls.
   - `TASKS.md` — active task board. Note the top pending tasks.

2. **Give a short summary** (not a wall of text). Use this structure:

   - **Version** — current version from NEW-SESSION.md
   - **Recent work** — 2-3 bullet points from what changed recently
   - **Known issues** — only if there are any
   - **Top pending tasks** — top 3-4 from TASKS.md "Up Next" section

   Do NOT include: injection layer descriptions (the user knows), ILSpy stats, plugin roadmap, or the dev commands table. The user already knows these — they're in CLAUDE.md and available via `/help`.

3. **Ask what the user wants to work on.** Use `AskUserQuestion` with the top pending tasks from TASKS.md as options. If the user already stated their task in the same message as `/hi`, skip the question and go straight to Phase 2.

## Phase 2: Deep-Read (targeted to task)

Once you know the task, load only the docs that prevent rediscovering known bugs and patterns.

4. Read `docs/INDEX.md`. Use the **AI Workflow Paths** section and **Quick-Reference Topic Finder** table to identify the docs relevant to the user's task. Think broadly:
   - Docs about the feature area (e.g., MessageLogger → Hook Reference § MessageLogger, Message Logger plugin doc)
   - Docs about patterns the task uses (e.g., visual tree work → Avalonia Patterns, Root Control Reference)
   - Source files listed in TASKS.md for the task
   - `hook/SESSION_STATE.md` if working on the C# hook (has recent context and pending issues)
   - Built-in plugin docs (`docs/plugins/builtin/`) if relevant
   - Research docs only if the task involves gRPC, DotNetBrowser, or Root internals

5. **Read every doc identified in step 4.** These contain hard-won quirks that prevent wasted debugging cycles. Read source files too if listed in TASKS.md or directly relevant.

6. **Report what you read** — briefly list the docs and any quirks/patterns directly applicable to the task. Confirm you are ready to begin work.
