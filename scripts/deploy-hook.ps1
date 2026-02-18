#Requires -Version 5.1
<#
.SYNOPSIS
    Hot-deploy the hook DLL from the build output to the Root install directory.
.DESCRIPTION
    Lightweight deploy script for iterating on the C# hook:
      1. Stops Root and DotNetBrowser chromium processes (unlocks the DLL)
      2. Copies build artifacts to %LOCALAPPDATA%\Root\uprooted\
      3. Relaunches Root with Uprooted

    Run from a Windows terminal after building inside the devcontainer:
      dotnet build hook/ -c Release

    The workspace is bind-mounted, so build output is visible from both sides.
    Unlike install-hook.ps1, this skips shortcuts, registry, and settings -- just
    the DLL swap and restart cycle.
#>

param(
    [switch]$NoRelaunch
)

function Write-Step($msg) { Write-Host "[*] $msg" -ForegroundColor Cyan }
function Write-OK($msg)   { Write-Host "[+] $msg" -ForegroundColor Green }
function Write-Warn($msg) { Write-Host "[!] $msg" -ForegroundColor Yellow }
function Write-Err($msg)  { Write-Host "[-] $msg" -ForegroundColor Red }

try {

# --- Paths ---

$ScriptDir  = Split-Path -Parent $MyInvocation.MyCommand.Path
$RepoRoot   = Split-Path $ScriptDir -Parent
$HookBinDir = Join-Path $RepoRoot "hook\bin\Release\net10.0"
$ToolsDir   = Join-Path $RepoRoot "tools"
$UprootedDir = Join-Path $env:LOCALAPPDATA "Root\uprooted"
$RootExe = Join-Path $env:LOCALAPPDATA "Root\current\Root.exe"
$Launcher = Join-Path $UprootedDir "UprootedLauncher.exe"

# --- Preflight ---

if (-not (Test-Path (Join-Path $HookBinDir "UprootedHook.dll"))) {
    Write-Err "No build output at $HookBinDir"
    Write-Err "Build first: dotnet build hook/ -c Release"
    Read-Host "Press Enter to exit"
    exit 1
}

if (-not (Test-Path $UprootedDir)) {
    Write-Err "Uprooted not installed ($UprootedDir missing)"
    Write-Err "Run install-hook.ps1 for first-time setup"
    Read-Host "Press Enter to exit"
    exit 1
}

# --- Stop Root + Chromium ---

$rootProcs = Get-Process -Name Root -ErrorAction SilentlyContinue
$chromiumProcs = Get-Process -Name chromium -ErrorAction SilentlyContinue

if ($rootProcs -or $chromiumProcs) {
    $pids = @()
    if ($rootProcs) { $pids += $rootProcs.Id }
    if ($chromiumProcs) { $pids += $chromiumProcs.Id }
    Write-Step "Stopping $($pids.Count) processes (PID: $($pids -join ', '))..."

    if ($rootProcs) { $rootProcs | Stop-Process -Force -ErrorAction SilentlyContinue }
    if ($chromiumProcs) { $chromiumProcs | Stop-Process -Force -ErrorAction SilentlyContinue }

    Start-Sleep -Seconds 3
    Write-OK "Processes stopped"
} else {
    Write-Step "Root not running, skipping stop"
}

# --- Copy artifacts ---

Write-Step "Deploying to $UprootedDir..."

# Hook DLL
Copy-Item (Join-Path $HookBinDir "UprootedHook.dll") (Join-Path $UprootedDir "UprootedHook.dll") -Force
Write-OK "  UprootedHook.dll"

# Deps JSON (if present)
$depsJson = Join-Path $HookBinDir "UprootedHook.deps.json"
if (Test-Path $depsJson) {
    Copy-Item $depsJson (Join-Path $UprootedDir "UprootedHook.deps.json") -Force
    Write-OK "  UprootedHook.deps.json"
}

# JS scripts bundled with the hook
foreach ($jsFile in @("nsfw-filter.js", "link-embeds.js")) {
    $src = Join-Path $HookBinDir $jsFile
    if (Test-Path $src) {
        Copy-Item $src (Join-Path $UprootedDir $jsFile) -Force
        Write-OK "  $jsFile"
    } else {
        Write-Warn "  $jsFile not found in build output (skipped)"
    }
}

# Native artifacts from tools/ (not part of dotnet build)
$srcProfiler = Join-Path $ToolsDir "uprooted_profiler.dll"
if (Test-Path $srcProfiler) {
    Copy-Item $srcProfiler (Join-Path $UprootedDir "uprooted_profiler.dll") -Force
    Write-OK "  uprooted_profiler.dll"
}
$srcLauncher = Join-Path $ToolsDir "UprootedLauncher.exe"
if (Test-Path $srcLauncher) {
    Copy-Item $srcLauncher (Join-Path $UprootedDir "UprootedLauncher.exe") -Force
    Write-OK "  UprootedLauncher.exe"
}

# --- Relaunch ---

if (-not $NoRelaunch) {
    # Prefer launcher (sets up CLR profiler env vars) over bare Root.exe
    if (Test-Path $Launcher) {
        Write-Step "Launching Root via UprootedLauncher..."
        Start-Process $Launcher
        Start-Sleep -Seconds 2
        $launched = Get-Process -Name Root -ErrorAction SilentlyContinue
        if ($launched) {
            Write-OK "Root launched (PID: $($launched.Id -join ', '))"
        } else {
            Write-OK "Launcher started (Root may still be loading)"
        }
    } elseif (Test-Path $RootExe) {
        Write-Warn "UprootedLauncher.exe not found — launching Root.exe directly (no hook injection)"
        Start-Process $RootExe
        Write-OK "Root launched (without Uprooted)"
    } else {
        Write-Warn "Neither launcher nor Root.exe found"
        Write-OK "Deploy complete (manual relaunch needed)"
    }
} else {
    Write-OK "Deploy complete (skipped relaunch)"
}

} catch {
    Write-Err "Fatal error: $($_.Exception.Message)"
    Write-Host $_.ScriptStackTrace -ForegroundColor DarkGray
    Read-Host "Press Enter to exit"
    exit 1
}
