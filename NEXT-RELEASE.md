# Next Release

> Changes since v0.5.1-dev1. This file is replaced each release.

### Fixed

- **Hook fails to load on Linux AppImage (TypeLoadException)** — The hook DLL targeted `net10.0` but Root's AppImage bundles .NET 9. `Assembly.LoadFrom` threw a `TypeLoadException` (could not load `System.AssemblyLoadEventHandler` from `System.Runtime, Version=10.0.0.0`) which was silently swallowed by the profiler's injected try/catch. Downgraded hook target to `net9.0` which loads on both .NET 9 and .NET 10 runtimes. Also fixed the ConfuserEx obfuscation tool to probe NuGet targeting pack reference assemblies so obfuscation works across TFM boundaries.
  - Files: `hook/UprootedHook.csproj`, `tools/confuse/Program.cs`
- **Experimental plugins cause hang on boot after update** — Users who enabled experimental plugins (Rootcord, WhoReacted, etc.) before updating could get stuck in an unrecoverable hang on startup. The only fix was manually deleting the settings file. Now ALL experimental plugins are blanket-disabled on every version upgrade. Users can re-enable them from Settings > Plugins after confirming the update is stable.
  - File: `hook/StartupHook.cs`
