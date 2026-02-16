# Test injection with longer wait and check all markers
Get-Process Root -ErrorAction SilentlyContinue | Stop-Process -Force
Start-Sleep -Seconds 2

Remove-Item 'C:\Users\bash\PROFILER_LOG.txt' -ErrorAction SilentlyContinue
Remove-Item 'C:\Users\bash\UPROOTED_LOADED.txt' -ErrorAction SilentlyContinue
$hookLog = "$env:LOCALAPPDATA\Root Communications\Root\profile\default\uprooted-hook.log"
Remove-Item $hookLog -ErrorAction SilentlyContinue

$env:CORECLR_ENABLE_PROFILING = "1"
$env:CORECLR_PROFILER = "{D1A6F5A0-1234-4567-89AB-CDEF01234567}"
$env:CORECLR_PROFILER_PATH = "C:\Users\bash\claude\Root.Dev\uprooted\tools\uprooted_profiler.dll"
$env:DOTNET_ReadyToRun = "0"

$proc = Start-Process "$env:LOCALAPPDATA\Root\current\Root.exe" -PassThru
Write-Host "Root.exe PID: $($proc.Id)"

# Check every 3 seconds for 30 seconds
for ($i = 0; $i -lt 10; $i++) {
    Start-Sleep -Seconds 3
    $elapsed = ($i + 1) * 3
    $alive = -not $proc.HasExited
    $marker = Test-Path 'C:\Users\bash\UPROOTED_LOADED.txt'
    $hookExists = Test-Path $hookLog
    Write-Host "T+${elapsed}s: alive=$alive marker=$marker hookLog=$hookExists"
    if (-not $alive) {
        Write-Host "CRASHED! Exit code: $($proc.ExitCode)"
        break
    }
    if ($marker -or $hookExists) {
        Write-Host "*** HOOK LOADED! ***"
    }
}

Write-Host "`n=== PROFILER LOG ==="
if (Test-Path 'C:\Users\bash\PROFILER_LOG.txt') {
    Get-Content 'C:\Users\bash\PROFILER_LOG.txt'
} else {
    Write-Host "NO LOG"
}

Write-Host "`n=== MARKER FILE ==="
if (Test-Path 'C:\Users\bash\UPROOTED_LOADED.txt') {
    Get-Content 'C:\Users\bash\UPROOTED_LOADED.txt'
} else {
    Write-Host "NO MARKER"
}

Write-Host "`n=== HOOK LOG ==="
if (Test-Path $hookLog) {
    Get-Content $hookLog
} else {
    Write-Host "NO HOOK LOG"
}
