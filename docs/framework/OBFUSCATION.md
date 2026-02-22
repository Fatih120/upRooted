# Obfuscation

> **PRIVATE — DO NOT REFERENCE IN PUBLIC-FACING CONTENT.** This document, its contents, and the existence of obfuscation must never appear in `CHANGELOG_PUBLIC.md`, `NEXT-RELEASE.md`, GitHub release notes, the public repo, or any user-facing documentation.

> **What this is:** Complete reference for the ConfuserEx2 obfuscation pipeline — protections enabled, protected names, build integration, verification, and troubleshooting.
> **Read when:** Debugging obfuscation failures; adding new reflection targets; understanding why the hook DLL looks scrambled in ILSpy; deploying or smoke-testing an obfuscated build.
> **Skip if:** You need the hook architecture → [HOOK_REFERENCE.md](HOOK_REFERENCE.md). You need the build pipeline overview → [BUILD.md](../install/BUILD.md).

> **Related docs:**
> [Hook Reference](HOOK_REFERENCE.md) |
> [Architecture](ARCHITECTURE.md) |
> [CLR Profiler](CLR_PROFILER.md) |
> [Build](../install/BUILD.md)

---

## Table of Contents

1. [Why Obfuscate](#why-obfuscate)
2. [Protections Enabled](#protections-enabled)
3. [Protected Names](#protected-names)
4. [Build Pipeline](#build-pipeline)
5. [Architecture](#architecture)
6. [Skipping Obfuscation](#skipping-obfuscation)
7. [Post-Build Verification](#post-build-verification)
8. [Adding New Reflection Targets](#adding-new-reflection-targets)
9. [ConfuserEx Warnings](#confuserex-warnings)
10. [Troubleshooting](#troubleshooting)
11. [Known Gaps](#known-gaps)
12. [Future Work](#future-work)

---

## Why Obfuscate

`UprootedHook.dll` ships as a .NET IL assembly. Without obfuscation it is trivially decompilable — anyone with ILSpy or dnSpy can read every line of source, including the internal attack surface against Root. Obfuscation raises the bar for:

- **Reverse engineering** — renamed types/methods, encrypted constants, scrambled control flow
- **Weaponization** — makes it harder for bad actors to fork the hook into something malicious
- **IP protection** — novel injection techniques and UI construction patterns are harder to study

Obfuscation is defense-in-depth, not a silver bullet. A determined attacker with a debugger can still reverse the protections. The goal is to make casual decompilation useless.

---

## Protections Enabled

Protections confirmed safe on .NET 10 via bisect testing:

| Protection    | Mode                | Effect                                                                                             |
| ------------- | ------------------- | -------------------------------------------------------------------------------------------------- |
| **rename**    | ascii (Unicode RTL) | Type, method, field, and property names → unreadable characters                                    |
| **ctrl flow** | normal              | Control flow graph restructured with opaque predicates                                             |
| **ref proxy** | mild                | Method calls redirected through proxy delegates (mild only — normal crashes the ConfuserEx engine) |

These are configured in `hook/UprootedHook.crproj`.

### Broken on .NET 10

These protections were individually bisect-tested and all crash the hook on load:

| Protection             | Failure Mode                                                                       |
| ---------------------- | ---------------------------------------------------------------------------------- |
| **constants**          | Hook fails to load — encrypted string/numeric decryption breaks on .NET 10 runtime |
| **anti ildasm**        | Hook fails to load — injected invalid metadata rejected by .NET 10 runtime         |
| **resources**          | Hook fails to load — encrypted embedded resource access fails on .NET 10           |
| **ref proxy (normal)** | ConfuserEx engine crashes with "Unknown error occurred" — only mild mode works     |

### Not Yet Tested

| Protection           | Risk                                    | Status     |
| -------------------- | --------------------------------------- | ---------- |
| **anti tamper**      | May conflict with .NET assembly loading | Test later |
| **anti debug**       | May use Framework-only APIs             | Test later |
| **anti dump**        | May not work on modern runtime          | Test later |
| **invalid metadata** | Modern runtime may reject               | Test later |

---

## Protected Names

Certain types and methods **must not be renamed** because they are resolved by string name at runtime — either by the CLR, native code, or reflection. Obfuscating these names silently breaks the hook at load time.

### Entry Points (excluded from ALL obfuscation)

These have `[System.Reflection.Obfuscation(Exclude = true)]` on the class AND are excluded via the `.crproj` rule:

| Type                   | Why                                                                                                                          |
| ---------------------- | ---------------------------------------------------------------------------------------------------------------------------- |
| `UprootedHook.Entry`   | Hardcoded in native profiler C code (`tools/uprooted_profiler.c` line 39) as `Assembly.CreateInstance("UprootedHook.Entry")` |
| `Uprooted.NativeEntry` | hostfxr native boundary — `load_assembly_and_get_function_pointer` resolves by name                                          |
| `StartupHook`          | CLR convention — `DOTNET_STARTUP_HOOKS` expects exactly this class name with `public static void Initialize()`               |

### Reflection Targets (excluded from rename only)

These have `[Obfuscation(Exclude = false, Feature = "-rename")]` on the method. The containing type CAN be renamed — only the method name must survive.

| Method                                     | Lookup Site                                                                                | Why                                                         |
| ------------------------------------------ | ------------------------------------------------------------------------------------------ | ----------------------------------------------------------- |
| `ReconLogger.Enable()`                     | `ContentPages.cs` — `typeof(ReconLogger).GetMethod("Enable")`                              | Settings toggle resolves method by string name              |
| `ReconLogger.Disable()`                    | `ContentPages.cs` — `typeof(ReconLogger).GetMethod("Disable")`                             | Settings toggle resolves method by string name              |
| `MessageLogger.SubscribeToObservable<T>()` | `MessageLogger.cs` — `typeof(MessageLogger).GetMethod(nameof(SubscribeToObservable), ...)` | Generic method resolved by name, then `MakeGenericMethod()` |

Note: `ReconLogger` itself is also excluded from renaming (the type name survives) because `ContentPages.cs` resolves it via `typeof(ReconLogger).GetMethod(methodName)` where the type is also used. `MessageLogger`'s type IS renamed — `typeof(MessageLogger)` resolves by token (compile-time), so only the method name matters.

---

## Build Pipeline

Obfuscation is integrated into the standard Release build:

```
dotnet build hook/UprootedHook.csproj -c Release
```

This triggers three stages:

```
Compile (MSBuild)
  │
  ▼
ObfuscateAssembly (AfterTargets="Build")
  │  Runs: dotnet run --project tools/confuse/ -- <crproj> <baseDir> <outputDir>
  │  Input:  bin/Release/net10.0/UprootedHook.dll
  │  Output: bin/Release/net10.0/confused/UprootedHook.dll
  │  Then:   Copy confused/UprootedHook.dll → bin/Release/net10.0/UprootedHook.dll
  │
  ▼
VerifyObfuscation (AfterTargets="ObfuscateAssembly")
     Runs: dotnet run --project tools/verify-obfuscation/ -- <dll-path>
     Asserts all protected names survived
     Fails the build on violation
```

The final `UprootedHook.dll` in `bin/Release/net10.0/` is the obfuscated version. The deploy script (`scripts/deploy-hook.ps1`) picks it up from there — no changes needed to the deploy flow.

---

## Architecture

### Why Not `Confuser.MSBuild` Directly?

The `Confuser.MSBuild` NuGet package (v1.6.0) ships MSBuild task integration, but it is **broken on .NET Core SDK**:

1. The `netstandard/` folder is missing `dnlib.dll`
2. Even after copying it, `Confuser.Protections.dll` and siblings fail to load — the .NET Core MSBuild task host doesn't probe sibling directories for dependencies
3. Template placeholders (`{InputPath}`, `{OutputDir}`) are not resolved by the `CreateProjectTask` in the netstandard path

### The Workaround

Two small .NET 10 console apps wrap the ConfuserEx engine:

**`tools/confuse/`** — ConfuserEx engine wrapper

- References the ConfuserEx library DLLs directly from the NuGet cache (`netstandard/` folder)
- Copies `Confuser.Runtime.dll` from the `netframework/` folder (only location it exists)
- Loads the `.crproj` config, overrides paths, adds the module, and calls `ConfuserEngine.Run()`
- The `Confuser.MSBuild` NuGet package is referenced with `<IncludeAssets>none</IncludeAssets>` to prevent importing the broken MSBuild targets while still ensuring the package is restored

**`tools/verify-obfuscation/`** — Protected name verifier

- Uses `System.Reflection.Metadata` + `System.Reflection.PortableExecutable` to read PE metadata
- Does NOT load the assembly (obfuscated Unicode names crash `Assembly.Load`)
- Scans all type definitions and method definitions for expected names
- Returns exit code 1 on any missing name

### File Layout

```
hook/
├── UprootedHook.crproj          # ConfuserEx protection config
├── UprootedHook.csproj          # MSBuild targets (ObfuscateAssembly, VerifyObfuscation)
├── Entry.cs                     # [Obfuscation(Exclude = true)]
├── NativeEntry.cs               # [Obfuscation(Exclude = true)]
├── StartupHook.cs               # [Obfuscation(Exclude = true)]
├── ReconLogger.cs               # [Obfuscation(Feature = "-rename")] on Enable/Disable
└── MessageLogger.cs             # [Obfuscation(Feature = "-rename")] on SubscribeToObservable

tools/
├── confuse/
│   ├── confuse.csproj           # Engine wrapper project
│   └── Program.cs               # Loads .crproj, runs ConfuserEngine
└── verify-obfuscation/
    ├── verify-obfuscation.csproj
    └── Program.cs               # PE metadata scanner for protected names
```

---

## Skipping Obfuscation

Obfuscation is gated on two conditions: `Configuration == Release` and `Obfuscate == true`.

```bash
# Debug builds — no obfuscation (default)
dotnet build hook/UprootedHook.csproj -c Debug

# Release builds — obfuscated (default)
dotnet build hook/UprootedHook.csproj -c Release

# Release builds — skip obfuscation (for debugging the unobfuscated DLL)
dotnet build hook/UprootedHook.csproj -c Release -p:Obfuscate=false
```

---

## Post-Build Verification

The `VerifyObfuscation` target runs automatically after every obfuscated build. It checks three categories:

**Entry points** — full type name must exist:

```
  OK: UprootedHook.Entry
  OK: Uprooted.NativeEntry
  OK: StartupHook
```

**Reflection targets (type + method)** — both type and method name must survive:

```
  OK: Uprooted.ReconLogger.Enable
  OK: Uprooted.ReconLogger.Disable
```

**Reflection targets (method name only)** — method name must exist in any type:

```
  OK: *.SubscribeToObservable (method name survived, type renamed)
```

If any check fails, the build fails with:

```
OBFUSCATION VERIFICATION FAILED -- protected names were renamed!
```

---

## Adding New Reflection Targets

When you add code that resolves a hook-internal method or type by string name, you must protect it from renaming. Follow this checklist:

### 1. Add the attribute

For **entry points** (resolved by external code via string name):

```csharp
[System.Reflection.Obfuscation(Exclude = true)]
public class MyEntryPoint { ... }
```

For **methods resolved via typeof().GetMethod("name")**:

```csharp
[Obfuscation(Exclude = false, Feature = "-rename")]
private static void MyReflectedMethod() { ... }
```

### 2. Add to the `.crproj` (belt-and-suspenders, entry points only)

```xml
<rule pattern="member-type('type') and full-name('Namespace.ClassName')" preset="none" inherit="false">
  <protection id="rename" action="remove" />
</rule>
```

### 3. Add to the verifier

Edit `tools/verify-obfuscation/Program.cs`:

```csharp
// For types that must keep their name:
AssertTypeExists("Namespace.ClassName");

// For methods on named types:
AssertMethodExists("Namespace.ClassName", "MethodName");

// For methods where the type is renamed but the method name must survive:
AssertMethodNameSurvived("MethodName");
```

### 4. Rebuild and verify

```bash
dotnet build hook/UprootedHook.csproj -c Release
```

The verification gate will catch regressions.

### Patterns That Are Safe

These patterns do NOT need protection:

- `typeof(SomeType).GetMethod(...)` where `SomeType` is an **external** type (Avalonia, System.Net, DotNetBrowser) — external types are not renamed
- `someObject.GetType().GetProperty("Name")` where the property belongs to an **external** type
- `Activator.CreateInstance(someType)` using a `Type` variable — resolved by token, not by name

---

## ConfuserEx Warnings

During obfuscation you'll see many warnings like:

```
WARN: Tracing arguments for System.Reflection.PropertyInfo System.Type::GetProperty(...)
call in Uprooted.AvaloniaReflection::ResolveMembers(...) failed.
```

**These are expected and safe.** ConfuserEx's renamer tries to trace reflection calls to automatically protect referenced names. When it can't trace the arguments (because they're computed at runtime from external types), it warns. Since all those reflection calls target **external** Avalonia/DotNetBrowser types (not our own types), they're unaffected by renaming.

---

## Troubleshooting

### Hook crashes on load after obfuscation

1. Rebuild without obfuscation to confirm the crash is obfuscation-related:
   ```bash
   dotnet build hook/UprootedHook.csproj -c Release -p:Obfuscate=false
   ```
2. Disable protections one at a time in `UprootedHook.crproj`, starting with the most aggressive:
   - `ctrl flow` → most likely to cause runtime issues on modern .NET
   - `constants` → encrypted strings may cause issues with certain patterns
   - `resources` → embedded resource access may fail
   - `anti ildasm` → least likely to cause issues
   - `rename` → disable last (this is the core protection)

### Verification fails after adding new code

A new method that uses `GetMethod("name")` or `GetType("name")` against a hook-internal type will break under rename. See [Adding New Reflection Targets](#adding-new-reflection-targets).

### Build fails with "Could not open file Confuser.Runtime.dll"

The `Confuser.Runtime.dll` is copied from the NuGet package's `netframework/` folder to the confuse tool's output directory. If it's missing:

```bash
dotnet restore tools/confuse/confuse.csproj
dotnet build tools/confuse/ -c Release
```

### Build fails with "Unknown error occurred"

Check the ConfuserEx version. Some protection arguments changed between versions:

- `ctrl flow` predicate: use `normal` or `expression`, NOT `switch` (removed in ConfuserEx2)

---

## Known Gaps

- **Symbol map not generated** — the `renameMap=true` argument is set but ConfuserEx2 1.6.0 does not produce a `symbols.map` file. Obfuscated stack traces in crash reports will need manual correlation with the source.
- **No automated smoke test** — the verification gate checks that names survived, but doesn't test that the DLL actually loads in Root. Smoke testing must be done manually via `deploy-hook.ps1` on Windows.

---

## Future Work

| Item                 | Notes                                                                                            |
| -------------------- | ------------------------------------------------------------------------------------------------ |
| `anti tamper`        | Detect assembly modification at load time. May conflict with .NET assembly loading.              |
| `anti debug`         | Detect attached debuggers. May use Framework-only APIs.                                          |
| `anti dump`          | Prevent memory dumping of the loaded assembly. May not work on modern runtime.                   |
| `invalid metadata`   | Inject invalid metadata to confuse decompilers. Modern runtime may reject.                       |
| `constants` re-test  | Broken on .NET 10.0.3 — re-test on future ConfuserEx2 releases or .NET updates.                 |
| Symbol map           | Investigate why `renameMap` doesn't produce output; may need CLI flag or different argument name. |
| Automated smoke test | Script that loads the obfuscated DLL in a test harness and verifies entry point resolution.      |
