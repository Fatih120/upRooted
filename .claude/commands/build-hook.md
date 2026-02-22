---
name: build-hook
description: Build the Uprooted C# hook (UprootedHook.dll) in Release configuration
allowed-tools:
  - Bash
  - Read
---

# Build Hook

Build the Uprooted C# .NET hook from the `hook/` directory.

## Instructions

Run the following command from the repository root:

```
dotnet build hook/UprootedHook.csproj -c Release
```

After the build completes:

1. Report whether the build succeeded or failed
2. If it failed, read the error output and identify the issue
3. Check if the output DLL was produced at `hook/bin/Release/net10.0/UprootedHook.dll`

If the build fails due to missing .NET SDK, inform the user they need .NET 10 SDK installed.

## Notes

- The hook targets .NET 10.0 with nullable enabled and implicit usings
- The hook has zero NuGet dependencies
- Output DLL must be deployed to Root's profile directory to take effect
- The hook is loaded via CLR profiler injection — it runs inside Root.exe's process
