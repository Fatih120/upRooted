---
name: dump
description: "Deep-load session context, then use ILSpy tooling to dump every verbose type for what we need more info on, organizing it into a folder specific to that debugging session in research, then will carry on with debugging."
allowed-tools:
  - Read
  - Write
  - Edit
  - Grep
  - Glob
  - Bash
  - Task
  - AskUserQuestion
args:
  - name: task
    description: "What to dump types for (e.g., 'Rootcord plugin', 'MessageLogger card positioning', 'ProfileBadge popup detection')"
    required: false
---

# Dump — Fresh ILSpy Decompilation for Debugging

Decompile exactly the Root types a hook engine references, organize into a session folder, analyze, and continue debugging. Never reuse stale dumps: always decompile fresh.

---

## Phase 1: Orient (always: 3 files in parallel)

Read simultaneously:
1. `NEW-SESSION.md`
2. `TASKS.md`
3. `hook/SESSION_STATE.md`

Give a tight summary:
- **v{version}** — from NEW-SESSION.md
- **Recent** — 2 bullets from TASKS.md Done (most recent)
- **Pending** — top 3 from TASKS.md Ready to Build
- **Hook state** — 1 sentence from SESSION_STATE.md

---

## Phase 2: Identify Task

### If argument provided

Map the task name to hook source file(s) using this table (case-insensitive matching):

| Keywords | Hook files |
|----------|-----------|
| `rootcord` | `hook/RootcordEngine.cs` |
| `message logger`, `messagelogger` | `hook/MessageLogger.cs`, `hook/MessageStore.cs` |
| `profile badge`, `profilebadge` | `hook/ProfileBadgeInjector.cs` |
| `translate` | `hook/TranslateEngine.cs`, `hook/TranslateConfigPopup.cs` |
| `link embed`, `linkembed` | `hook/LinkEmbedEngine.cs`, `hook/AnimatedImage.cs` |
| `theme`, `theme engine` | `hook/ThemeEngine.cs` |
| `sidebar`, `sidebar injector` | `hook/SidebarInjector.cs` |
| `content pages`, `settings` | `hook/ContentPages.cs` |
| `clearurls`, `clear urls` | `hook/ClearUrlsEngine.cs` |
| `nsfw`, `nsfw filter` | `hook/NsfwFilter.cs` |
| `who reacted`, `whoreacted` | `hook/WhoReactedEngine.cs` |
| `user bio`, `userbio` | `hook/UserBioEngine.cs` |
| `presence`, `beacon` | `hook/UprootedPresenceBeacon.cs` |
| `audit log`, `auditlog` | `hook/AuditLogEngine.cs` |
| `silent typing`, `silenttyping` | `hook/SilentTypingEngine.cs` |
| `auto update`, `autoupdater` | `hook/AutoUpdater.cs` |
| `color picker` | `hook/ColorPickerPopup.cs`, `hook/ColorUtils.cs` |
| `notification`, `desktop notification` | `hook/DesktopNotification.cs` |
| `startup` | `hook/StartupHook.cs` |
| `html patch` | `hook/HtmlPatchVerifier.cs` |
| `browser`, `dotnetbrowser` | `hook/DotNetBrowserReflection.cs`, `hook/BrowserDiscovery.cs` |

If no match: use Glob to find the closest `.cs` file in `hook/`, or ask the user.

### If no argument

Use **AskUserQuestion** (header: "Task", multiSelect: false):
- **Question**: "What are you debugging?"
- **Options**: Build from the top 3-4 items from TASKS.md (Ready to Build or In Progress), plus a generic "Other engine/file" option.

---

## Phase 3: Discover Trouble

**Auto-skip this phase** if the user already described the trouble in the argument (e.g., `/dump Rootcord member panel layout` — the trouble is layout).

Otherwise, use **AskUserQuestion** (header: "Trouble", multiSelect: false):
- **Question**: "What's been the trouble with {task}?"
- **Options**:
  1. **Layout / positioning / visual tree structure** — grid columns, alignment, sizing, panel nesting
  2. **Property binding / INPC / data not updating** — DataContext, PropertyChanged, stale values
  3. **Crash / exception / null reference** — NullRef, InvalidCast, MissingMethod
  4. **Timing / race condition / event ordering** — async, LayoutUpdated, Loaded, deferred init

---

## Phase 4: Extract Type References

Read the hook source file(s) identified in Phase 2. Then grep each file for patterns that reference Root types:

### String type names (passed to reflection)
```
Grep pattern: "RootApp\.|"Avalonia\.|"AvaloniaEdit\.
```
These are full or partial type names passed to `_r.FindType()`, `Type.GetType()`, or string comparisons.

### AvaloniaReflection calls
```
Grep pattern: _r\.(Is|Get|Create|Set|Find)[A-Z]
```
These tell us:
- `_r.Is{Type}(x)` → what Avalonia types are checked
- `_r.Get{Property}(x)` → what properties/children are accessed
- `_r.Create{Type}()` → what controls are created
- `_r.Set{Property}(x, v)` → what properties are mutated
- `_r.Find{Type}()` → what types are searched for

### Reflection property access
```
Grep pattern: GetPropertyValue|SetPropertyValue|GetProperty\(|\.GetValue\(|\.SetValue\(
```
Extract the property names in quotes: these map to ViewModel properties.

### Type name references from strings
```
Grep pattern: nameof\(|typeof\(|"[A-Z][a-zA-Z]+View"|"[A-Z][a-zA-Z]+ViewModel"
```

Build a list of:
- **Needed types**: Root types the hook code references or manipulates
- **Needed properties**: Properties accessed via reflection (map to ViewModels)
- **Needed views**: UI views being traversed or injected into

---

## Phase 5: Resolve Full Type Names

### 5a: Verify extracted assemblies exist

Check if `/tmp/root-extracted/` exists and has DLLs:
```bash
ls /tmp/root-extracted/RootApp.Client.Avalonia.dll 2>/dev/null && echo "EXISTS" || echo "MISSING"
```

If missing, extract fresh:
```bash
ilspycmd -d -o /tmp/root-extracted "C:/Users/bash/AppData/Local/Root/current/Root.exe"
```
This takes 1-2 minutes for the 577 MB bundle.

### 5b: Resolve each type

For each needed type from Phase 4, find its full qualified name:
```bash
ilspycmd -l c /tmp/root-extracted/RootApp.Client.Avalonia.dll | grep -i "TypeName"
```

If not found in the primary assembly, search others:
- UI types: `RootApp.Client.Avalonia.dll`
- DTOs/entities: `RootApp.WebApi.Shared.Entities.dll`
- gRPC services: `RootApp.WebApi.Shared.dll`
- Core domain: `RootApp.Core.dll`

### 5c: Expand related types

For completeness, also include:
- For each **View** → also get its **ViewModel** (and vice versa)
- For gRPC-accessed data → get the **GrpcService** + **Request/Response DTOs**
- For controls being created → get their **Style_** files if they exist

Build the final decompilation list: a set of `(assembly_dll, full_type_name, output_filename, reason)` tuples.

---

## Phase 6: Create Session Folder & Decompile

### 6a: Create session folder

```bash
mkdir -p "research/sessions/YYYY-MM-DD-<task-slug>/"
```
Example: `research/sessions/2026-02-24-rootcord-layout/`

The slug is lowercase, hyphenated, derived from the task name + trouble category if applicable.

### 6b: Decompile each type

For each type in the decompilation list:
```bash
ilspycmd /tmp/root-extracted/<assembly>.dll \
  -t "Full.Namespace.TypeName" \
  -r /tmp/root-extracted \
  > "research/sessions/YYYY-MM-DD-slug/TypeName.cs"
```

Run decompilations in parallel where possible (batch with `&&` or parallel Bash calls).

Verify each output file is non-empty and contains valid C# (not an error message). If a type fails to decompile, note it and continue.

### 6c: Write SESSION.md index

Write a `SESSION.md` file in the session folder:

```markdown
# Dump Session: {Task Name}
Date: {YYYY-MM-DD}
Task: {task description from user}
Hook files: {comma-separated hook file paths}

## Types Dumped
| File | Assembly | Full Type | Lines | Why |
|------|----------|-----------|-------|-----|
| TypeName.cs | RootApp.Client.Avalonia | Full.Namespace.TypeName | {N} | {brief reason} |
| ... | ... | ... | ... | ... |

## Key Findings
(filled in after reading)
```

---

## Phase 7: Read, Analyze & Report

Read the session dumps. For large files (>500 lines), use `offset`/`limit` to read the most relevant sections first, then grep for specifics.

Extract findings relevant to the trouble described in Phase 3:
- **Layout trouble**: grid/panel structure, named controls, RowDefinitions/ColumnDefinitions, alignment, margins
- **Binding trouble**: INotifyPropertyChanged properties, DataContext wiring, command bindings
- **Crash trouble**: null-checked properties, type casts, method signatures, parameter types
- **Timing trouble**: event subscriptions, async methods, Loaded/Initialized handlers, dispatcher calls

### Report format

1. **Types dumped** — list with line counts and assembly source
2. **Key findings** — 5-8 bullets directly relevant to the trouble:
   - Visual tree structure, named controls, grid/panel layout
   - Property names for INPC intercept
   - Method signatures for reflection calls
   - Data shapes (DTO fields, gRPC message structure)
3. **Root cause hypothesis** — what the dumps reveal about the problem
4. **Suggested approach** — concrete next steps for fixing

Update the `## Key Findings` section in `SESSION.md` with the actual findings.

---

## Phase 8: Continue Debugging

Ask the user if they want to:
1. Begin implementing the fix based on findings
2. Dump additional related types
3. Just use the session dumps as reference

The session dumps remain in `research/sessions/` for reference throughout the debugging session.

---

## Important Notes

- **Extracted assemblies path**: `/tmp/root-extracted/` (329 DLLs from Root.exe)
- **Root.exe location**: `C:/Users/bash/AppData/Local/Root/current/Root.exe`
- **ILSpy decompile**: `ilspycmd <assembly.dll> -t "Full.Namespace.Type" -r /tmp/root-extracted`
- **ILSpy list classes**: `ilspycmd -l c <assembly.dll>`
- **ILSpy extract bundle**: `ilspycmd -d -o /tmp/root-extracted "C:/Users/bash/AppData/Local/Root/current/Root.exe"`
- **Session folders**: `research/sessions/` directory (created on first use)
- All types are decompiled fresh: never copy from existing `research/ilspy-dumps/`
- If `split-ilspy-dump.py` is needed for batch splitting: `scripts/split-ilspy-dump.py`
