#Requires -Version 5.1
<#
.SYNOPSIS
    Build pipeline for Uprooted Installer - produces a self-contained Uprooted.exe.
.DESCRIPTION
    1. Builds TypeScript layer (pnpm build -> dist/)
    2. Builds C# hook DLL (dotnet build -> UprootedHook.dll)
    3. Compiles native profiler DLL (cl.exe -> uprooted_profiler.dll)
    4. Stages all artifacts into installer/src-tauri/artifacts/
    5. Builds console TUI installer (cargo build --release -> Uprooted.exe)
#>

$ErrorActionPreference = "Stop"

$RepoRoot = Split-Path $PSScriptRoot -Parent
$ArtifactsDir = Join-Path $RepoRoot "installer\src-tauri\artifacts"
$HookProjectDir = Join-Path $RepoRoot "hook"
$HookProjectFile = Join-Path $HookProjectDir "UprootedHook.csproj"
$ToolsDir = Join-Path $RepoRoot "tools"
$InstallerDir = Join-Path $RepoRoot "installer"

function Write-Step($msg) { Write-Host "[*] $msg" -ForegroundColor Cyan }
function Write-OK($msg) { Write-Host "[+] $msg" -ForegroundColor Green }
function Write-Err($msg) { Write-Host "[-] $msg" -ForegroundColor Red }

Write-Host ""
Write-Host "  Uprooted Installer Build Pipeline" -ForegroundColor Green
Write-Host "  ==================================" -ForegroundColor Green
Write-Host ""

# Disable CLR profiling for this process so dotnet build doesn't lock DLLs
if ($env:DOTNET_ENABLE_PROFILING -eq "1" -or $env:CORECLR_ENABLE_PROFILING -eq "1") {
    Write-Step "Disabling CLR profiling for build process..."
    $env:DOTNET_ENABLE_PROFILING = "0"
    $env:CORECLR_ENABLE_PROFILING = "0"
    Write-OK "Profiling disabled (this process only)"
}

# Ensure artifacts directory exists
New-Item -ItemType Directory -Path $ArtifactsDir -Force | Out-Null

# Step 1: Build TypeScript layer
Write-Step "Building TypeScript (pnpm build)..."
Push-Location $RepoRoot
try {
    $result = & pnpm build 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Err "pnpm build failed:"
        $result | ForEach-Object { Write-Host "  $_" }
        exit 1
    }
    Write-OK "TypeScript built"

    # Stage JS and CSS
    $preloadJs = Join-Path $RepoRoot "dist\uprooted-preload.js"
    $themeCss = Join-Path $RepoRoot "dist\uprooted.css"

    if (-not (Test-Path $preloadJs)) { Write-Err "uprooted-preload.js not found at $preloadJs"; exit 1 }
    if (-not (Test-Path $themeCss)) { Write-Err "uprooted.css not found at $themeCss"; exit 1 }

    Copy-Item $preloadJs $ArtifactsDir -Force
    Copy-Item $themeCss $ArtifactsDir -Force
    Write-OK "Staged uprooted-preload.js + uprooted.css"
} finally {
    Pop-Location
}

# Step 2: Build C# hook DLL
Write-Step "Building UprootedHook.dll (dotnet build)..."
$buildResult = & dotnet build $HookProjectFile -c Release 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Err "dotnet build failed:"
    $buildResult | ForEach-Object { Write-Host "  $_" }
    exit 1
}

$hookDll = Join-Path $HookProjectDir "bin\Release\net10.0\UprootedHook.dll"
$hookDeps = Join-Path $HookProjectDir "bin\Release\net10.0\UprootedHook.deps.json"

if (-not (Test-Path $hookDll)) { Write-Err "UprootedHook.dll not found at $hookDll"; exit 1 }

Copy-Item $hookDll $ArtifactsDir -Force
if (Test-Path $hookDeps) {
    Copy-Item $hookDeps $ArtifactsDir -Force
}

# Stage DotNetBrowser JS scripts (injected into Chromium by the hook)
$hookBinDir = Join-Path $HookProjectDir "bin\Release\net10.0"
foreach ($jsFile in @("nsfw-filter.js", "link-embeds.js")) {
    $srcJs = Join-Path $hookBinDir $jsFile
    if (Test-Path $srcJs) {
        Copy-Item $srcJs $ArtifactsDir -Force
        Write-OK "  Staged $jsFile"
    }
}
Write-OK "Staged UprootedHook.dll + deps.json + JS scripts"

# Step 3: Compile profiler DLL
Write-Step "Compiling uprooted_profiler.dll (cl.exe)..."

$profilerC = Join-Path $ToolsDir "uprooted_profiler.c"
$profilerDef = Join-Path $ToolsDir "uprooted_profiler.def"
$profilerOut = Join-Path $ArtifactsDir "uprooted_profiler.dll"

if (-not (Test-Path $profilerC)) { Write-Err "uprooted_profiler.c not found"; exit 1 }

# Find vcvarsall.bat
$vsWhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
if (-not (Test-Path $vsWhere)) {
    Write-Err "vswhere.exe not found. Install VS 2022 Build Tools."
    exit 1
}

$vsPath = & $vsWhere -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath 2>$null
if (-not $vsPath) {
    Write-Err "Visual Studio with C++ tools not found."
    exit 1
}

$vcvarsall = Join-Path $vsPath "VC\Auxiliary\Build\vcvarsall.bat"
if (-not (Test-Path $vcvarsall)) {
    Write-Err "vcvarsall.bat not found at $vcvarsall"
    exit 1
}

# Build profiler DLL using cl.exe
$defArg = ""
if (Test-Path $profilerDef) {
    $defArg = "/DEF:`"$profilerDef`""
}

$clCmd = "`"$vcvarsall`" x64 && cl.exe /LD /O2 /Fe:`"$profilerOut`" `"$profilerC`" /link ole32.lib kernel32.lib shell32.lib $defArg"
$ErrorActionPreference = "Continue"
$clResult = cmd /c $clCmd 2>&1
$ErrorActionPreference = "Stop"
if ($LASTEXITCODE -ne 0) {
    Write-Err "cl.exe compilation failed:"
    $clResult | ForEach-Object { Write-Host "  $_" }
    exit 1
}

if (-not (Test-Path $profilerOut)) { Write-Err "Profiler DLL not produced"; exit 1 }
Write-OK "Staged uprooted_profiler.dll"

# Verify all artifacts
Write-Step "Verifying staged artifacts..."
$required = @(
    "uprooted_profiler.dll",
    "UprootedHook.dll",
    "UprootedHook.deps.json",
    "uprooted-preload.js",
    "uprooted.css",
    "nsfw-filter.js",
    "link-embeds.js"
)
foreach ($f in $required) {
    $p = Join-Path $ArtifactsDir $f
    if (-not (Test-Path $p)) {
        Write-Err "Missing artifact: $f"
        exit 1
    }
    $size = (Get-Item $p).Length
    Write-OK "  $f ($size bytes)"
}

# Step 4: Build console TUI installer
Write-Step "Building console TUI installer (cargo build --release)..."
$cargoDir = Join-Path $InstallerDir "src-tauri"
Push-Location $cargoDir
try {
    $cargoResult = & cargo build --release 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Err "cargo build failed:"
        $cargoResult | ForEach-Object { Write-Host "  $_" }
        exit 1
    }
    Write-OK "Installer build complete"
} finally {
    Pop-Location
}

# Find output
$exePath = Join-Path $cargoDir "target\release\uprooted-installer.exe"
if (-not (Test-Path $exePath)) {
    # Try alternate name
    $exePath = Join-Path $cargoDir "target\release\Uprooted Installer.exe"
}
if (Test-Path $exePath) {
    $exeSize = (Get-Item $exePath).Length
    Write-Host ""
    Write-OK "Build successful!"
    Write-OK "Output: $exePath"
    Write-OK "Size: $([math]::Round($exeSize / 1MB, 1)) MB"
} else {
    Write-Err "Expected output not found in: $(Join-Path $cargoDir 'target\release\')"
    Write-Host "Check target\release\ for the built binary."
}

Write-Host ""
