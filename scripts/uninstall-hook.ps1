#Requires -Version 5.1
<#
.SYNOPSIS
    Uninstall Uprooted from Root Communications.
.DESCRIPTION
    1. Restores all shortcuts to point to Root.exe
    2. Restores the rootapp:// protocol handler
    3. Sets Enabled=false in uprooted-settings.ini
    4. Optionally removes Uprooted files

    Reverses everything done by install-hook.ps1.
#>

$ErrorActionPreference = "Stop"

function Write-Step($msg) { Write-Host "[*] $msg" -ForegroundColor Cyan }
function Write-OK($msg) { Write-Host "[+] $msg" -ForegroundColor Green }
function Write-Warn($msg) { Write-Host "[!] $msg" -ForegroundColor Yellow }
function Write-Err($msg) { Write-Host "[-] $msg" -ForegroundColor Red }

Write-Host ""
Write-Host "  Uprooted Uninstaller" -ForegroundColor Red
Write-Host "  ====================" -ForegroundColor Red
Write-Host ""

# --- Paths ---

$LocalAppData = $env:LOCALAPPDATA
$RootDir = Join-Path $LocalAppData "Root"
$RootCurrent = Join-Path $RootDir "current"
$RootExe = Join-Path $RootCurrent "Root.exe"
$UprootedDir = Join-Path $RootDir "uprooted"
$ProfileDir = Join-Path $LocalAppData "Root Communications\Root\profile\default"
$SettingsFile = Join-Path $ProfileDir "uprooted-settings.ini"
$BackupFile = Join-Path $UprootedDir "shortcuts-backup.txt"

# Stop Root if running
$rootProcs = Get-Process -Name Root -ErrorAction SilentlyContinue
if ($rootProcs) {
    Write-Warn "Root is running (PID: $($rootProcs.Id -join ', ')). Stopping..."
    Stop-Process -Name Root -Force -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    Write-OK "Root stopped"
}

# --- Step 1: Restore shortcuts from backup ---

Write-Step "Restoring shortcuts..."

$shell = New-Object -ComObject WScript.Shell
$restoreCount = 0

if (Test-Path $BackupFile) {
    Get-Content $BackupFile | ForEach-Object {
        $parts = $_ -split '\|'

        if ($parts[0] -eq "REGISTRY") {
            # Registry entry - handled in Step 2
            return
        }

        if ($parts.Count -lt 5) { return }

        $lnkPath = $parts[0]
        $origTarget = $parts[1]
        $origWorkDir = $parts[2]
        $origArgs = $parts[3]
        $origIcon = $parts[4]

        if (-not (Test-Path $lnkPath)) {
            Write-Host "  Skip (not found): $lnkPath" -ForegroundColor DarkGray
            return
        }

        try {
            $lnk = $shell.CreateShortcut($lnkPath)
            $lnk.TargetPath = $origTarget
            $lnk.WorkingDirectory = $origWorkDir
            $lnk.Arguments = $origArgs
            if ($origIcon) { $lnk.IconLocation = $origIcon }
            $lnk.Save()
            $restoreCount++
            Write-OK "  Restored: $lnkPath -> $origTarget"
        } catch {
            Write-Warn "  Failed to restore: $lnkPath - $($_.Exception.Message)"
        }
    }
    Write-OK "Shortcuts restored ($restoreCount)"
} else {
    Write-Warn "No backup file found, restoring shortcuts to Root.exe directly..."

    $shortcutPaths = @(
        (Join-Path $env:APPDATA "Microsoft\Windows\Start Menu\Programs\Root.lnk"),
        (Join-Path ([Environment]::GetFolderPath('Desktop')) "Root.lnk"),
        (Join-Path $env:APPDATA "Microsoft\Windows\Start Menu\Programs\Startup\Root.lnk"),
        (Join-Path $env:APPDATA "Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar\Root.lnk")
    )

    foreach ($lnkPath in $shortcutPaths) {
        if (-not (Test-Path $lnkPath)) { continue }
        try {
            $lnk = $shell.CreateShortcut($lnkPath)
            if ($lnk.TargetPath -like "*UprootedLauncher*") {
                $lnk.TargetPath = $RootExe
                $lnk.WorkingDirectory = $RootCurrent
                $lnk.IconLocation = "$RootExe,0"
                $lnk.Save()
                $restoreCount++
                Write-OK "  Restored: $lnkPath"
            }
        } catch {
            Write-Warn "  Failed: $lnkPath - $($_.Exception.Message)"
        }
    }
}

# --- Step 2: Restore protocol handler ---

Write-Step "Restoring protocol handler..."

$regPath = "HKCU:\SOFTWARE\Classes\rootapp\shell\open\command"
if (Test-Path $regPath) {
    $currentCmd = (Get-ItemProperty $regPath).'(default)'

    if ($currentCmd -like "*UprootedLauncher*") {
        # Try to find original from backup
        $origCmd = $null
        if (Test-Path $BackupFile) {
            Get-Content $BackupFile | ForEach-Object {
                $parts = $_ -split '\|'
                if ($parts[0] -eq "REGISTRY" -and $parts[1] -eq $regPath) {
                    $origCmd = $parts[2]
                }
            }
        }

        if (-not $origCmd) {
            $origCmd = "`"$RootExe`" `"%1`""
        }

        Set-ItemProperty $regPath -Name '(default)' -Value $origCmd
        Write-OK "Protocol handler restored: $origCmd"
    } else {
        Write-Host "  Protocol handler already points to Root.exe" -ForegroundColor DarkGray
    }
} else {
    Write-Host "  No rootapp:// handler found" -ForegroundColor DarkGray
}

# --- Step 3: Update settings ---

Write-Step "Updating settings..."

if (Test-Path $SettingsFile) {
    $settings = @{}
    Get-Content $SettingsFile | ForEach-Object {
        $eq = $_.IndexOf('=')
        if ($eq -ge 0) {
            $key = $_.Substring(0, $eq).Trim()
            $val = $_.Substring($eq + 1).Trim()
            $settings[$key] = $val
        }
    }
    $settings["Enabled"] = "false"
    $content = ($settings.GetEnumerator() | ForEach-Object { "$($_.Key)=$($_.Value)" }) -join "`n"
    Set-Content $SettingsFile $content -NoNewline
    Write-OK "Settings updated (Enabled=false)"
} else {
    Write-Host "  No settings file found" -ForegroundColor DarkGray
}

# --- Step 4: Ask about file removal ---

Write-Host ""
$response = Read-Host "Remove Uprooted files from $UprootedDir? (y/n)"
if ($response -eq 'y' -or $response -eq 'Y') {
    Write-Step "Removing Uprooted files..."
    Remove-Item $UprootedDir -Recurse -Force -ErrorAction SilentlyContinue
    Write-OK "Files removed"
} else {
    Write-Host "  Files kept at $UprootedDir" -ForegroundColor DarkGray
}

# --- Done ---

Write-Host ""
Write-Host "  Uprooted uninstalled." -ForegroundColor Green
Write-Host "  Root will now launch normally." -ForegroundColor White
Write-Host ""
