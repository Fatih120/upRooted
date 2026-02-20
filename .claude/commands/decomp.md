---
name: decomp
description: Search ILSpy decompilation dumps for info relevant to the current issue
allowed-tools:
  - Read
  - Grep
  - Glob
---

# Decomp

Search Root's ILSpy decompilation dumps for classes, controls, view models, styles, and theme data relevant to whatever you're currently working on.

## Instructions

### Step 1: Identify the current issue

Look at the conversation history to understand what the user is working on. Identify:
- Which Root controls, views, or view models are involved
- Which visual tree elements or Avalonia types matter
- Whether theme/color resources are relevant
- Any class names, property names, or namespace hints mentioned

If the conversation has no prior context (user invoked `/decomp` cold), ask what they're looking into.

### Step 2: Read the dump index

Read `research/ILSPY_DUMP_INDEX.md`. This is the master inventory of 273 decompiled files from Root v0.9.92. Use it to find:
- Files whose **name** or **description** match the current issue
- Files in the same **namespace** as related classes
- Files tagged as **Analyzed: Y** (already distilled into docs) vs **N** (raw only)

Pay attention to the file categories:
- **Views** — UI code-behind (XAML-generated)
- **ViewModels** — data/behavior
- **Custom Controls** — Avalonia control subclasses (Root's own controls)
- **Style Files** — compiled XAML templates (control templates, visual states)
- **Markdown Components** — CTextBlock/CSpan/CHyperlink rich text system
- **Theme System** — color keys, theme switching
- **Session / Core Domain** — authentication, session state

### Step 3: Check theme findings (if relevant)

If the issue involves colors, brushes, theme keys, resource dictionaries, or visual styling, also read `research/ROOT_THEME_SYSTEM_FINDINGS.md`. It contains:
- All 32 resource keys with hex values for Dark/PureDark/Light themes
- App.cs ThemeDictionaries wiring
- 27 style files analysis (which keys each control uses)
- The definitive override path for ThemeEngine

Skip this file if the issue is unrelated to theming.

### Step 4: Read the relevant dumps

Based on the index, read the most relevant decompiled source files from `research/ilspy-dumps/`. Prioritize:
1. Files directly named after the class/control in question
2. Files in the same view/viewmodel pair (e.g., `MessageView.cs` + `MessageViewModel.cs`)
3. Style files for the control (e.g., `Style_ToggleSwitch.cs` for toggle switches)
4. Factory files if you need to understand DI wiring

**Important rules for reading dumps:**
- Files marked `-GREPONLY` are too large to read in full (30k+ lines). Use Grep to search them for specific patterns instead of Read.
- `StylesAll-GREPONLY.cs.txt` has all compiled styles — grep it for specific key names or control types.
- `AvaloniaAll-GREPONLY.cs` and `AvaloniaEditAll-GREPONLY.cs` are framework dumps — only grep if you need framework internals.

Read up to 4-5 files max. If a file is very long (1000+ lines), focus on the sections most relevant to the issue.

### Step 5: Report findings

Give the user a concise report:

1. **Files examined** — list the dump files you read, with line counts
2. **Key findings** — what you learned that's relevant to the current issue:
   - Class hierarchy, base types, interfaces implemented
   - Properties, methods, or events that matter
   - How Root builds or styles the control in question
   - Resource keys or theme values used
   - Any gotchas or non-obvious behavior
3. **Implications** — how this affects the user's current work:
   - "Root uses X, so we should do Y"
   - "This property is bindable, so we can intercept it via INPC"
   - "The style template expects key Z — our ThemeEngine needs to provide it"
4. **Gaps** — if the needed dumps don't exist yet, say which classes should be decompiled next

Keep the report focused and actionable. Don't summarize the entire file — only the parts relevant to the issue at hand.
