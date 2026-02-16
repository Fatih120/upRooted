---
name: build-installer
description: Build the full Uprooted installer (TypeScript + C# hook + Tauri)
allowed-tools:
  - Bash
  - Read
---

# Build Installer

Build the complete Uprooted installer using the full build pipeline.

## Instructions

The full build pipeline is orchestrated by `scripts/build_installer.ps1`. Run:

```
powershell -File scripts/build_installer.ps1
```

This pipeline performs the following steps in order:

1. `pnpm build` — Build TypeScript (esbuild: `src/` -> `dist/uprooted-preload.js` + `dist/uprooted.css`)
2. `dotnet build hook/ -c Release` — Build C# hook (`UprootedHook.dll`)
3. C/MSVC compile — Build profiler DLL (`uprooted_profiler.dll`)
4. Stage all 5 artifacts to `installer/src-tauri/artifacts/`
5. `cargo tauri build` — Build final Tauri installer

After the build, report:
1. Whether each step succeeded
2. The location and size of the final installer output
3. Any warnings or errors encountered

## Alternative: Partial Builds

If only specific components need rebuilding:

- **TypeScript only**: `pnpm build`
- **C# hook only**: `dotnet build hook/ -c Release`
- **Tauri only**: `cd installer && cargo tauri build`

## Prerequisites

- Node.js + pnpm
- .NET 10 SDK
- Rust + cargo
- Tauri CLI (`cargo install tauri-cli`)
- Visual Studio Build Tools (for profiler DLL compilation)
