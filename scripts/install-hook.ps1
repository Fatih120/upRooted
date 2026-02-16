#Requires -Version 5.1
<#
.SYNOPSIS
    Install Uprooted into Root Communications.
.DESCRIPTION
    1. Copies profiler DLL, hook DLL, and launcher to %LOCALAPPDATA%\Root\uprooted\
    2. Modifies Root's shortcuts (Start Menu, Desktop, Startup, Taskbar) to use the launcher
    3. Modifies the rootapp:// protocol handler to use the launcher
    4. Sets Enabled=true in uprooted-settings.ini

    After install, launching Root from any shortcut or rootapp:// link will
    automatically load Uprooted via the CLR profiler.

    Run uninstall-hook.ps1 to reverse all changes.
#>

$ErrorActionPreference = "Stop"

function Write-Step($msg) { Write-Host "[*] $msg" -ForegroundColor Cyan }
function Write-OK($msg) { Write-Host "[+] $msg" -ForegroundColor Green }
function Write-Warn($msg) { Write-Host "[!] $msg" -ForegroundColor Yellow }
function Write-Err($msg) { Write-Host "[-] $msg" -ForegroundColor Red }

Write-Host ""
Write-Host "  Uprooted Installer" -ForegroundColor Green
Write-Host "  ==================" -ForegroundColor Green
Write-Host ""

# --- Paths ---

$LocalAppData = $env:LOCALAPPDATA
$RootDir = Join-Path $LocalAppData "Root"
$RootCurrent = Join-Path $RootDir "current"
$RootExe = Join-Path $RootCurrent "Root.exe"
$UprootedDir = Join-Path $RootDir "uprooted"
$ProfileDir = Join-Path $LocalAppData "Root Communications\Root\profile\default"
$SettingsFile = Join-Path $ProfileDir "uprooted-settings.ini"

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$RepoRoot = Split-Path $ScriptDir -Parent
$ToolsDir = Join-Path $RepoRoot "tools"
$HookBinDir = Join-Path $RepoRoot "hook\bin\Release\net10.0"

# Source artifacts
$SrcProfilerDll = Join-Path $ToolsDir "uprooted_profiler.dll"
$SrcHookDll = Join-Path $HookBinDir "UprootedHook.dll"
$SrcLauncher = Join-Path $ToolsDir "UprootedLauncher.exe"

# Destination artifacts
$DstProfilerDll = Join-Path $UprootedDir "uprooted_profiler.dll"
$DstHookDll = Join-Path $UprootedDir "UprootedHook.dll"
$DstLauncher = Join-Path $UprootedDir "UprootedLauncher.exe"
$BackupFile = Join-Path $UprootedDir "shortcuts-backup.txt"

# --- Preflight checks ---

Write-Step "Checking prerequisites..."

if (-not (Test-Path $RootExe)) {
    Write-Err "Root.exe not found at $RootExe"
    Write-Err "Is Root Communications installed?"
    exit 1
}
Write-OK "Root.exe found"

# Check for source artifacts
$missing = @()
if (-not (Test-Path $SrcProfilerDll)) { $missing += "uprooted_profiler.dll (run build_all.cmd)" }
if (-not (Test-Path $SrcHookDll)) { $missing += "UprootedHook.dll (run: dotnet build hook/ -c Release)" }
if (-not (Test-Path $SrcLauncher)) { $missing += "UprootedLauncher.exe (run: build_launcher.cmd)" }

if ($missing.Count -gt 0) {
    Write-Err "Missing artifacts:"
    foreach ($m in $missing) { Write-Host "  - $m" -ForegroundColor Red }
    Write-Host ""
    Write-Host "Build all artifacts first:" -ForegroundColor Yellow
    Write-Host "  cd $ToolsDir" -ForegroundColor Yellow
    Write-Host "  build_launcher.cmd" -ForegroundColor Yellow
    Write-Host "  cd $RepoRoot\hook && dotnet build -c Release" -ForegroundColor Yellow
    exit 1
}
Write-OK "All artifacts found"

# Stop Root if running
$rootProcs = Get-Process -Name Root -ErrorAction SilentlyContinue
if ($rootProcs) {
    Write-Warn "Root is running (PID: $($rootProcs.Id -join ', ')). Stopping..."
    Stop-Process -Name Root -Force -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    Write-OK "Root stopped"
}

# --- Step 1: Copy artifacts ---

Write-Step "Copying artifacts to $UprootedDir..."

New-Item -ItemType Directory -Path $UprootedDir -Force | Out-Null

Copy-Item $SrcProfilerDll $DstProfilerDll -Force
Copy-Item $SrcHookDll $DstHookDll -Force
Copy-Item $SrcLauncher $DstLauncher -Force

# Also copy hook deps.json if it exists
$srcDeps = Join-Path $HookBinDir "UprootedHook.deps.json"
if (Test-Path $srcDeps) {
    Copy-Item $srcDeps (Join-Path $UprootedDir "UprootedHook.deps.json") -Force
}

Write-OK "Artifacts copied:"
Write-OK "  uprooted_profiler.dll ($([math]::Round((Get-Item $DstProfilerDll).Length / 1KB, 1)) KB)"
Write-OK "  UprootedHook.dll ($([math]::Round((Get-Item $DstHookDll).Length / 1KB, 1)) KB)"
Write-OK "  UprootedLauncher.exe ($([math]::Round((Get-Item $DstLauncher).Length / 1KB, 1)) KB)"

# --- Step 2: Patch shortcuts ---

Write-Step "Patching shortcuts..."

$shell = New-Object -ComObject WScript.Shell
$backupLines = @()
$patchCount = 0

$shortcutPaths = @(
    # Start Menu
    (Join-Path $env:APPDATA "Microsoft\Windows\Start Menu\Programs\Root.lnk"),
    # Desktop
    (Join-Path ([Environment]::GetFolderPath('Desktop')) "Root.lnk"),
    # Startup
    (Join-Path $env:APPDATA "Microsoft\Windows\Start Menu\Programs\Startup\Root.lnk"),
    # Taskbar pin
    (Join-Path $env:APPDATA "Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar\Root.lnk")
)

foreach ($lnkPath in $shortcutPaths) {
    if (-not (Test-Path $lnkPath)) {
        Write-Host "  Skip (not found): $lnkPath" -ForegroundColor DarkGray
        continue
    }

    try {
        $lnk = $shell.CreateShortcut($lnkPath)
        $origTarget = $lnk.TargetPath
        $origWorkDir = $lnk.WorkingDirectory
        $origArgs = $lnk.Arguments
        $origIcon = $lnk.IconLocation

        # Save backup info
        $backupLines += "$lnkPath|$origTarget|$origWorkDir|$origArgs|$origIcon"

        # Only patch if currently pointing to Root.exe
        if ($origTarget -like "*Root.exe") {
            $lnk.TargetPath = $DstLauncher
            $lnk.WorkingDirectory = $UprootedDir
            # Keep the Root.exe icon
            if (-not $origIcon -or $origIcon -eq ",0") {
                $lnk.IconLocation = "$RootExe,0"
            }
            $lnk.Save()
            $patchCount++
            Write-OK "  Patched: $lnkPath"
        } else {
            Write-Host "  Skip (not Root.exe): $lnkPath -> $origTarget" -ForegroundColor DarkGray
        }
    } catch {
        Write-Warn "  Failed to patch: $lnkPath - $($_.Exception.Message)"
    }
}

# Save backup for uninstall
$backupLines | Set-Content $BackupFile -Encoding UTF8
Write-OK "Shortcut backup saved ($patchCount patched)"

# --- Step 3: Patch protocol handler ---

Write-Step "Patching rootapp:// protocol handler..."

$regPath = "HKCU:\SOFTWARE\Classes\rootapp\shell\open\command"
if (Test-Path $regPath) {
    $origCmd = (Get-ItemProperty $regPath).'(default)'
    # Save original for uninstall
    Add-Content $BackupFile "REGISTRY|$regPath|$origCmd"

    $newCmd = "`"$DstLauncher`" `"%1`""
    Set-ItemProperty $regPath -Name '(default)' -Value $newCmd
    Write-OK "Protocol handler patched: $newCmd"
} else {
    Write-Warn "rootapp:// protocol handler not found in registry"
}

# --- Step 4: Update settings ---

Write-Step "Updating settings..."

New-Item -ItemType Directory -Path $ProfileDir -Force | Out-Null

# Read existing settings or create new
$settings = @{}
if (Test-Path $SettingsFile) {
    Get-Content $SettingsFile | ForEach-Object {
        $eq = $_.IndexOf('=')
        if ($eq -ge 0) {
            $key = $_.Substring(0, $eq).Trim()
            $val = $_.Substring($eq + 1).Trim()
            $settings[$key] = $val
        }
    }
}

$settings["Enabled"] = "true"
if (-not $settings.ContainsKey("Version")) { $settings["Version"] = "0.1.10" }
if (-not $settings.ContainsKey("ActiveTheme")) { $settings["ActiveTheme"] = "default-dark" }

$content = ($settings.GetEnumerator() | ForEach-Object { "$($_.Key)=$($_.Value)" }) -join "`n"
Set-Content $SettingsFile $content -NoNewline
Write-OK "Settings updated (Enabled=true)"

# --- Done ---

Write-Host ""
Write-Host "  Uprooted installed successfully!" -ForegroundColor Green
Write-Host ""
Write-Host "  Root will now auto-load Uprooted on every launch." -ForegroundColor White
Write-Host "  To uninstall: .\uninstall-hook.ps1" -ForegroundColor DarkGray
Write-Host "  To disable without uninstalling: set Enabled=false in" -ForegroundColor DarkGray
Write-Host "    $SettingsFile" -ForegroundColor DarkGray
Write-Host ""

# Ask if user wants to launch Root now
$response = Read-Host "Launch Root now? (y/n)"
if ($response -eq 'y' -or $response -eq 'Y') {
    Write-Step "Launching Root with Uprooted..."
    Start-Process $DstLauncher
    Write-OK "Root launched!"
}
