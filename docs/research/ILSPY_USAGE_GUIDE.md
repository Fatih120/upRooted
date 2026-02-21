# ILSpy Dumps — Developer Usage Guide

> **How to use Root's decompiled source code as a development reference.**
> **Quick navigation:** [ILSPY_NAVIGATION.md](../../research/ILSPY_NAVIGATION.md)
> **Full inventory:** [ILSPY_DUMP_INDEX.md](../../research/ILSPY_DUMP_INDEX.md)

---

## What We Have

`research/ilspy-dumps/` contains ~1,395 decompiled C# source files from Root v0.9.93, extracted from the 577 MB single-file bundle (`Root.exe`) using `ilspycmd`. This covers:

- **All UI** — every View, ViewModel, control, style
- **All gRPC services** — every RPC descriptor
- **Complete API data model** — every request/response DTO (~700 files)
- **Browser system** — DotNetBrowser management, all JS↔native bridges
- **WebRTC stack** — full WebRTC service and constraints
- **Core types** — UUID/GUID types for every entity, asset types
- **Auth flow** — login, register, CAPTCHA (Turnstile)
- **All utility code** — MIME detection, image sizers, HTTP client factories

**Curated summaries** of the most important files are in:
- [ROOT_CONTROL_REFERENCE.md](../framework/ROOT_CONTROL_REFERENCE.md) — controls, views, VMs
- [ROOT_THEME_SYSTEM_FINDINGS.md](../../research/ROOT_THEME_SYSTEM_FINDINGS.md) — color keys, theme system

---

## When to Use the Dumps

Use the raw dumps (not the curated docs) when you need:

- **Exact property names** — confirm `PropertyName` before attempting INPC/reflection intercept
- **Method signatures** — confirm parameters before calling via reflection
- **Visual tree structure** — understand what a View builds so you can walk/inject it
- **gRPC wire format** — see what fields a request/response has for AuditLog, SilentTyping, etc.
- **API entity shapes** — what does `MessageCreateRequest` look like? What fields does `ChannelResponse` expose?
- **Bridge contracts** — what methods does `AppToNativeBridge` expose to JS?
- **Control template internals** — what `PART_*` elements does a control expect?

---

## The Three Navigation Steps

### Step 1: Use the Quick Lookup table

Open [ILSPY_NAVIGATION.md](../../research/ILSPY_NAVIGATION.md) and find your task in the **Quick Lookup by Task** table. It gives you primary + secondary files to read.

### Step 2: Read the files directly

Read files from `research/ilspy-dumps/<FileName>.cs`. All files are flat — no subdirectories.

**For large files** (1000+ lines), use `Read` with `offset` and `limit` to target the relevant section, or `Grep` for specific patterns first.

### Step 3: Check GREPONLY files if needed

Three files are too large to read fully — always use `Grep` on them:

| File | Use when |
|------|----------|
| `-AvaloniaResources-GREPONLY.cs` | Need resource key registration, XAML trampoline lookup |
| `XamlIlHelpers-GREPONLY.cs` | Need type resolver methods, property setter internals |
| `StylesAll-GREPONLY.cs.txt` | Need to find which style file defines a specific selector |

---

## Common Scenarios

### "I need to intercept a Root property change"

1. Find the ViewModel file (e.g., `ChannelViewModel.cs`)
2. Look for `[ObservableProperty]` attributes or `PropertyChanged` → confirms INPC support
3. Find the exact property name (C# source, not generated backing field)
4. In hook code, use `AvaloniaReflection` or standard INPC subscription

### "I need to walk the visual tree to find X"

1. Find the View file (e.g., `ChannelView.cs`)
2. Read the constructor and `InitializeComponent` + inline XAML closures
3. Named controls are in `this.FindControl<T>("PART_Name")` or `[Name]` attributes
4. Use this to write a `VisualTreeWalker` predicate targeting the right type/name

### "I need to call a Root method via reflection"

1. Find the class file in dumps
2. Read the method signature and its parameter types
3. Note the full namespace (first comment in old-format dumps, or `namespace` declaration in new-format)
4. Use `AvaloniaReflection.GetCachedType(fullTypeName)` and `GetMethod(methodName, ...)`

### "I need to understand a gRPC message structure"

1. Find the service file: e.g., `MessageGrpcService.cs` — shows all RPC method names
2. Find the entity file: e.g., `MessageCreateRequest.cs` — shows all protobuf fields
3. Fields use `[global::ProtoBuf.ProtoMember(N)]` attributes with field numbers
4. For the AuditLogEngine / SilentTyping: field names + numbers give us the binary encoding

### "I need to add a new feature that hooks into a Root flow"

1. Start with the ViewModel (business logic)
2. Cross-reference with the View (UI structure)
3. Check the gRPC service if network calls are involved
4. Check entity DTOs for data shapes
5. Use `AvaloniaReflection` to get types at runtime

### "I need to know what CSS variables the browser layer has"

Read `RootThemeCss.cs` — contains the full CSS variable map used in Root's browser layer.

---

## File Format Notes

Files added in the **original batch (2026-02-19)** start with ILSpy header comments:
```
// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=...
// RootApp.Client.Avalonia.UI.Messages.MessageView
```

Files added in the **new batch (2026-02-21)** are standard C# project output:
```csharp
namespace RootApp.Client.Avalonia.UI.Messages;

public class MessageView : UserControl { ... }
```

Both formats are fully readable C# source. The new-format files don't have the ILSpy version header but are otherwise identical in content.

---

## Re-Running Decompilation

Root.exe is a single-file .NET bundle. To re-decompile after a Root update:

```bash
# 1. Extract all assemblies from the bundle
ilspycmd -d -o /tmp/root-extracted "/path/to/Root.exe" --disable-updatecheck

# 2. Decompile specific type to stdout
ilspycmd /tmp/root-extracted/RootApp.Client.Avalonia.dll \
  -t "RootApp.Client.Avalonia.UI.Messages.MessageView" \
  -r /tmp/root-extracted \
  --disable-updatecheck

# 3. Decompile whole assembly as project (one file per type)
ilspycmd -p -o /tmp/outdir \
  -r /tmp/root-extracted \
  /tmp/root-extracted/RootApp.Client.Avalonia.dll \
  --disable-updatecheck

# 4. Split a multi-class dump (legacy workflow)
python3 scripts/split-ilspy-dump.py input.cs -o research/ilspy-dumps/

# 5. List all types in an assembly
ilspycmd -l c /tmp/root-extracted/RootApp.Client.Avalonia.dll --disable-updatecheck
```

**Important**: Assembly file path must come **before** flags like `-t`.

Root.exe location: `%LOCALAPPDATA%\Root\current\Root.exe`

---

## Assembly Reference

| Assembly | Key Contents |
|----------|-------------|
| `RootApp.Client.Avalonia.dll` | All UI (Views, ViewModels, controls, styles), browser system, bridges |
| `RootApp.WebApi.Shared.Entities.dll` | All request/response DTOs (~700 types) |
| `RootApp.WebApi.Shared.dll` | gRPC service descriptors, permissions |
| `RootApp.Core.dll` | UUID/GUID types, asset types, SemanticVersion |
| `RootApp.Utility.dll` | MIME detection, HTTP client, image sizers |
| `RootApp.Client.Domain.dll` | LocalDataStore, DataStoreKeys |
| `RootApp.Client.CoreDomain.dll` | RootSession, IRootSessionAccessor |
| `RootApp.Client.Domain.Windows.dll` | Windows P/Invoke, malware scanner |
| `RootApp.AppHub.Client.dll` | AppHub WebSocket, RPC messages |
| `RootApp.WebApi.Client.Shared.dll` | ApiConnection, auth interceptor |

---

*Last updated: 2026-02-21. Root v0.9.93.*
