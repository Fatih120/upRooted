# Quick test: did Root survive the injection?
Get-Process Root -ErrorAction SilentlyContinue | Stop-Process -Force
Start-Sleep -Seconds 2

Remove-Item 'C:\Users\bash\PROFILER_LOG.txt' -ErrorAction SilentlyContinue

$env:CORECLR_ENABLE_PROFILING = "1"
$env:CORECLR_PROFILER = "{D1A6F5A0-1234-4567-89AB-CDEF01234567}"
$env:CORECLR_PROFILER_PATH = "C:\Users\bash\claude\Root.Dev\uprooted\tools\uprooted_profiler.dll"

$proc = Start-Process "$env:LOCALAPPDATA\Root\current\Root.exe" -PassThru
Write-Host "Root.exe PID: $($proc.Id)"

# Check every 2 seconds for 16 seconds
for ($i = 0; $i -lt 8; $i++) {
    Start-Sleep -Seconds 2
    $elapsed = ($i + 1) * 2
    $alive = -not $proc.HasExited
    Write-Host "T+${elapsed}s: alive=$alive"
    if (-not $alive) {
        Write-Host "ROOT.EXE CRASHED! Exit code: $($proc.ExitCode)"
        break
    }
}

Write-Host "`n=== PROFILER LOG (last 20 lines) ==="
if (Test-Path 'C:\Users\bash\PROFILER_LOG.txt') {
    Get-Content 'C:\Users\bash\PROFILER_LOG.txt' | Select-Object -Last 20
} else {
    Write-Host "NO LOG"
}
