$ErrorActionPreference = "SilentlyContinue"

Write-Host "=== Root Launch Diagnostics ==="
Write-Host ""

# Check shortcuts
$locations = @(
    [Environment]::GetFolderPath("Desktop"),
    [Environment]::GetFolderPath("StartMenu"),
    "$env:APPDATA\Microsoft\Windows\Start Menu\Programs"
)

$shell = New-Object -ComObject WScript.Shell

foreach ($loc in $locations) {
    $lnks = Get-ChildItem $loc -Filter "*Root*" -Recurse -ErrorAction SilentlyContinue
    foreach ($lnk in $lnks) {
        $shortcut = $shell.CreateShortcut($lnk.FullName)
        Write-Host "SHORTCUT: $($lnk.FullName)"
        Write-Host "  Target: $($shortcut.TargetPath)"
        Write-Host "  Args:   $($shortcut.Arguments)"
        Write-Host "  WorkDir: $($shortcut.WorkingDirectory)"
        $exists = Test-Path $shortcut.TargetPath
        Write-Host "  Target exists: $exists"
        Write-Host ""
    }
}

# Check taskbar pins
$taskbarPath = "$env:APPDATA\Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar"
if (Test-Path $taskbarPath) {
    $pins = Get-ChildItem $taskbarPath -Filter "*Root*" -ErrorAction SilentlyContinue
    foreach ($pin in $pins) {
        $shortcut = $shell.CreateShortcut($pin.FullName)
        Write-Host "TASKBAR PIN: $($pin.FullName)"
        Write-Host "  Target: $($shortcut.TargetPath)"
        Write-Host "  Args:   $($shortcut.Arguments)"
        $exists = Test-Path $shortcut.TargetPath
        Write-Host "  Target exists: $exists"
        Write-Host ""
    }
}

# Check velopack log for errors
$veloLog = "$env:LOCALAPPDATA\Root\velopack.log"
if (Test-Path $veloLog) {
    Write-Host "=== Last 20 lines of velopack.log ==="
    Get-Content $veloLog -Tail 20
    Write-Host ""
}

# Check if Root is running
$rootProcs = Get-Process -Name "Root" -ErrorAction SilentlyContinue
if ($rootProcs) {
    Write-Host "Root.exe is currently running (PID: $($rootProcs.Id -join ', '))"
} else {
    Write-Host "Root.exe is NOT running"
}

# Check env vars (live process env)
Write-Host ""
Write-Host "=== Current process env vars ==="
Write-Host "CORECLR_ENABLE_PROFILING: $env:CORECLR_ENABLE_PROFILING"
Write-Host "CORECLR_PROFILER: $env:CORECLR_PROFILER"
Write-Host "CORECLR_PROFILER_PATH: $env:CORECLR_PROFILER_PATH"
Write-Host "DOTNET_ReadyToRun: $env:DOTNET_ReadyToRun"

# Check registry env vars
Write-Host ""
Write-Host "=== Registry env vars (HKCU\Environment) ==="
$regEnv = Get-ItemProperty -Path "HKCU:\Environment" -ErrorAction SilentlyContinue
$profVars = @("CORECLR_ENABLE_PROFILING", "CORECLR_PROFILER", "CORECLR_PROFILER_PATH", "DOTNET_ReadyToRun")
foreach ($v in $profVars) {
    $val = $regEnv.$v
    if ($val) {
        Write-Host "  ${v}: $val"
    } else {
        Write-Host "  ${v}: <not set>"
    }
}
