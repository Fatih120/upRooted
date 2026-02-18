---
name: build-installer
description: Build the Uprooted console TUI installer (Rust)
allowed-tools:
  - Bash
  - Read
---

# Build Installer

Build the Uprooted console TUI installer from the Rust source.

## Instructions

The installer is a standalone Rust binary using ratatui for the TUI interface. Build it with:

```
cd installer/src-tauri && cargo build --release
```

After the build, report:
1. Whether the build succeeded
2. The location and size of the output binary
3. Any warnings or errors encountered

## Alternative: Full Pipeline Build

For a full build including all embedded artifacts (TypeScript bundle, C# hook, profiler DLL):

```
powershell -File scripts/build-installer.ps1
```

This pipeline:
1. `pnpm build` — Build TypeScript (esbuild: `src/` -> `dist/uprooted-preload.js` + `dist/uprooted.css`)
2. `dotnet build hook/ -c Release` — Build C# hook (`UprootedHook.dll`)
3. C/MSVC compile — Build profiler DLL (`uprooted_profiler.dll`)
4. Stage all artifacts to `installer/src-tauri/artifacts/`
5. `cargo build --release` — Build final installer binary

## Partial Builds

If only specific components need rebuilding:

- **TypeScript only**: `pnpm build`
- **C# hook only**: `dotnet build hook/ -c Release`
- **Installer only**: `cd installer/src-tauri && cargo build --release`

## Prerequisites

- Rust / Cargo (stable)
- Node.js + pnpm (for TypeScript bundle)
- .NET 10 SDK (for C# hook)
- Visual Studio Build Tools (for profiler DLL, Windows only)
