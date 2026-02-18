# Testing Guide

> **Related docs:** [Contributing Technical](CONTRIBUTING_TECHNICAL.md) | [Build Guide](../install/BUILD.md) | [Architecture](../framework/ARCHITECTURE.md)

This document covers the C# unit test suite, Docker sandboxes, test stubs, and patterns for adding new tests.

---

## Table of Contents

1. [Running Tests](#running-tests)
2. [Test Project Structure](#test-project-structure)
3. [Test Stubs](#test-stubs)
4. [Writing New Tests](#writing-new-tests)
5. [Docker Unit Test Sandbox](#docker-unit-test-sandbox)
6. [Linux Installer Docker Sandbox](#linux-installer-docker-sandbox)
7. [Bug Report](#bug-report)
8. [Coverage](#coverage)

---

## Running Tests

**Local (Windows or Linux):**

```bash
dotnet test tests/UprootedTests/
```

**Docker (Linux container, clean environment):**

```bash
bash tests/run-docker-tests.sh
```

Extracts XPlat coverage XML to `tests/coverage/`.

**Current result:** 170 tests, all passing. Zero bugs found in pure-logic modules.

---

## Test Project Structure

```
tests/
├── UprootedTests/
│   ├── UprootedTests.csproj          # Links source files from hook/; auto-includes TestStubs/
│   ├── TestInfrastructure.cs         # xUnit collection definition for sequential tests
│   ├── ColorUtilsTests.cs            # 57 tests: HSL/HSV/RGB color math
│   ├── GradientBrushTests.cs         # 15 tests: Avalonia gradient brush via reflection
│   ├── ClearUrlsEngineTests.cs       # 58 tests: CleanUrl static method (all 33 tracking params + edge cases)
│   ├── UprootedSettingsTests.cs      # 22 tests: INI parse, coercion, cache, migration, roundtrip
│   ├── MessageStoreTests.cs          # 18 tests: records, URI encoding, Truncate, malformed tolerance
│   └── TestStubs/
│       ├── Logger.cs                 # Discards all output (no disk I/O)
│       ├── PlatformPaths.cs          # Returns configurable temp dir
│       ├── AvaloniaReflection.cs     # Minimal stub: RunOnUIThread + GetVisualChildren stubs
│       └── VisualTreeWalker.cs       # Returns empty tree
├── Dockerfile.unittest               # SDK 10.0 container: runs all tests + coverage
├── run-docker-tests.sh               # Build image, run, extract coverage to tests/coverage/
├── docker-installer/
│   ├── Dockerfile                    # Ubuntu 24.04: installs Uprooted end-to-end, verifies
│   ├── curl-shim.sh                  # Intercepts GitHub download, returns fake tarball
│   └── verify.sh                     # 14 PASS/FAIL checks on installer outputs
└── BUG_REPORT.md                     # Results from last full test run
```

**How source linking works:** The `.csproj` uses `<Compile Include="..\..\hook\X.cs" Link="X.cs" />` to compile source files directly from `hook/` — no separate DLL build required. The SDK default glob picks up `TestStubs/*.cs` automatically.

---

## Test Stubs

The test project does NOT link `Logger.cs`, `PlatformPaths.cs`, `AvaloniaReflection.cs`, or `VisualTreeWalker.cs` from `hook/`. Instead, stub versions in `TestStubs/` are compiled in their place (same `Uprooted` namespace, same class names). This prevents disk I/O and Avalonia runtime requirements during tests.

| Stub | What it does |
|------|-------------|
| `Logger` | All methods are no-ops; `GetLogPath()` returns `/dev/null` |
| `PlatformPaths` | Returns `ProfileDirOverride` (a mutable static); each test sets it to a unique temp dir |
| `AvaloniaReflection` | `RunOnUIThread(action)` calls `action()` directly; all other methods return stubs |
| `VisualTreeWalker` | `DescendantsDepthFirst()` returns `Enumerable.Empty<object>()` |

### Static state reset pattern

`UprootedSettings` and `MessageStore` use static fields that persist across tests. The test classes reset them in their constructors:

```csharp
[Collection("SequentialTests")]
public class UprootedSettingsTests : IDisposable
{
    private static readonly FieldInfo SettingsPathField =
        typeof(UprootedSettings).GetField("_settingsPath", BindingFlags.NonPublic | BindingFlags.Static)!;

    public UprootedSettingsTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), $"uprooted-settings-{Guid.NewGuid():N}");
        Directory.CreateDirectory(_tempDir);
        PlatformPaths.ProfileDirOverride = _tempDir;       // point PlatformPaths at temp dir
        SettingsPathField.SetValue(null, null);             // clear cached settings path
        UprootedSettings.InvalidateCache();                 // clear cached settings instance
    }
}
```

Both `UprootedSettingsTests` and `MessageStoreTests` are in the `[Collection("SequentialTests")]` collection (defined in `TestInfrastructure.cs`), so xunit runs them sequentially and prevents parallel interference through the shared `PlatformPaths.ProfileDirOverride` static.

### Flushing MessageStore

`MessageStore.FlushBuffer` is private and called by a background timer. Tests flush it synchronously via reflection:

```csharp
private static readonly MethodInfo FlushMethod =
    typeof(MessageStore).GetMethod("FlushBuffer", BindingFlags.NonPublic | BindingFlags.Instance)!;

private void Flush() => FlushMethod.Invoke(_store, new object?[] { null });
```

---

## Writing New Tests

### Adding a new module under test

1. **Add source link** in `UprootedTests.csproj`:
   ```xml
   <Compile Include="..\..\hook\MyNewFile.cs" Link="MyNewFile.cs" />
   ```

2. **Check dependencies** — does `MyNewFile.cs` use anything not already stubbed? If it references a hook class with real I/O, add a stub to `TestStubs/`.

3. **Create a test file** in `tests/UprootedTests/MyNewFileTests.cs`.

4. **Use `[Collection("SequentialTests")]`** if your tests use `PlatformPaths` or other shared statics.

### Testing static methods

Prefer testing static methods first — they have no instance state and need no setup:

```csharp
[Fact]
public void CleanUrl_RemovesUtmSource()
{
    var result = ClearUrlsEngine.CleanUrl("https://example.com?utm_source=google");
    Assert.Equal("https://example.com", result);
}
```

### Testing file-based logic

Use a temp dir and clean up in `Dispose()`:

```csharp
public class MyTests : IDisposable
{
    private readonly string _dir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));

    public MyTests()
    {
        Directory.CreateDirectory(_dir);
        PlatformPaths.ProfileDirOverride = _dir;
        UprootedSettings.InvalidateCache();
        typeof(UprootedSettings)
            .GetField("_settingsPath", BindingFlags.NonPublic | BindingFlags.Static)!
            .SetValue(null, null);
    }

    public void Dispose() { try { Directory.Delete(_dir, true); } catch { } }
}
```

---

## Docker Unit Test Sandbox

`tests/Dockerfile.unittest` and `tests/run-docker-tests.sh` run the full test suite in a clean `mcr.microsoft.com/dotnet/sdk:10.0` Linux container.

```bash
bash tests/run-docker-tests.sh
```

What it does:
1. Builds the Docker image from `tests/Dockerfile.unittest` (copies `hook/` and `tests/UprootedTests/` into the container)
2. Runs `dotnet test --collect:"XPlat Code Coverage" --results-directory /coverage`
3. Mounts `tests/coverage/` from the host so coverage XML is available after the run

Coverage XML is at `tests/coverage/**/*.xml`. Feed it to a coverage reporter (e.g. `reportgenerator`) for HTML output.

---

## Linux Installer Docker Sandbox

`tests/docker-installer/Dockerfile` tests the full `install-uprooted-linux.sh` bash script in a clean Ubuntu 24.04 environment without internet access.

```bash
docker build -f tests/docker-installer/Dockerfile -t uprooted-installer-test .
```

Build fails if any installer verification step fails.

### How it works

| Step | What happens |
|------|-------------|
| 1. Fake tarball | 5 expected artifact files packed into a `.tar.gz` stored at `/fake-tarball/artifacts.tar.gz` |
| 2. `curl` shim | `tests/docker-installer/curl-shim.sh` replaces `/usr/bin/curl`; intercepts GitHub artifact download URL and serves the fake tarball instead |
| 3. Fake Root | `$HOME/Applications/Root.AppImage` created as an empty executable file |
| 4. Fake HTML | `$PROFILE_DIR/RootApps/MainApp/index.html` created with a bare `<head></head>` |
| 5. Installer runs | `bash install-uprooted-linux.sh --prebuilt --root-path ...` |
| 6. Verify | `tests/docker-installer/verify.sh` checks 14 conditions: artifact files, env conf, `.profile` update, wrapper script, HTML patch injection |

### What `verify.sh` checks

- All 5 artifact files exist in `~/.local/share/uprooted/`
- `~/.config/environment.d/uprooted.conf` exists with `DOTNET_ENABLE_PROFILING=1`, profiler GUID, profiler path, and `CORECLR_ENABLE_PROFILING=1`
- `~/.profile` contains the Uprooted env vars block
- `~/.local/share/uprooted/launch-root.sh` exists and is executable
- `MainApp/index.html` contains the `<!-- uprooted:start -->` patch, `uprooted-preload.js` script tag, and `uprooted.css` link tag

---

## Bug Report

After every significant test run, results are recorded in `tests/BUG_REPORT.md`. The report documents:
- Pass/fail counts per module
- Any behavioral observations confirmed by tests (even non-bugs)
- Potential edge cases not yet covered

**Last run (2026-02-18):** 170/170 tests passing, zero bugs found.

---

## Coverage

Current coverage is unmeasured per-line, but by module:

| Module | Test Count | Key Paths Covered |
|--------|-----------|-------------------|
| `ClearUrlsEngine.CleanUrl` | 58 | All 33 params, all fragment/no-query/empty-query branches, prefix safety, case folding |
| `UprootedSettings.Load/Save` | 22 | All scalar keys, all bool/float/int coercions, cache TTL, migration path, new-install path |
| `MessageStore.LoadAll/Truncate` | 18 | All 4 record types, URI encode/decode, malformed skip, blank/comment skip, truncate boundary |

Untested (requires Avalonia or Root runtime): `ClearUrlsEngine.Initialize/DiscoverAndHook`, `UprootedSettings` cache TTL via real 10s sleep, `MessageStore` timer flush concurrency.
