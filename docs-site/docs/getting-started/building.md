# Building from Source

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 22+](https://nodejs.org/) with [pnpm](https://pnpm.io/)
- [Rust (stable)](https://rustup.rs/)
- **Windows**: Visual Studio Build Tools (MSVC)
- **Linux**: GCC
- **macOS**: Xcode Command Line Tools (clang)

## Build Steps

### 1. TypeScript layer

```bash
pnpm install --frozen-lockfile
pnpm build
```

### 2. C# hook

```bash
dotnet build hook/UprootedHook.csproj -c Release
```

### 3. Native profiler

=== "Windows"

    ```powershell
    cl.exe /LD /O2 /GS- /Fe:uprooted_profiler.dll tools/uprooted_profiler.c /link ole32.lib kernel32.lib shell32.lib /DEF:tools/uprooted_profiler.def
    ```

=== "Linux"

    ```bash
    gcc -shared -fPIC -O2 -s -fvisibility=hidden -o libuprooted_profiler.so tools/uprooted_profiler_linux.c
    ```

=== "macOS"

    ```bash
    clang -shared -fPIC -O2 -o libuprooted_profiler.dylib tools/uprooted_profiler_macos.c
    ```

### 4. Console TUI installer

```bash
cd installer/src-tauri
cargo build --release
```

The installer binary embeds all 7 artifacts via `include_bytes!()`.
