# Kill Root, clear log, launch with profiler env vars, wait, read log
Get-Process Root -ErrorAction SilentlyContinue | Stop-Process -Force
Start-Sleep -Seconds 2

Remove-Item 'C:\Users\bash\PROFILER_LOG.txt' -ErrorAction SilentlyContinue

$env:CORECLR_ENABLE_PROFILING = "1"
$env:CORECLR_PROFILER = "{D1A6F5A0-1234-4567-89AB-CDEF01234567}"
$env:CORECLR_PROFILER_PATH = "C:\Users\bash\claude\Root.Dev\uprooted\tools\uprooted_profiler.dll"

Write-Host "Launching Root.exe with profiler..."
$proc = Start-Process "$env:LOCALAPPDATA\Root\current\Root.exe" -PassThru
Write-Host "Root.exe PID: $($proc.Id)"

Write-Host "Waiting 15 seconds..."
Start-Sleep -Seconds 15

Write-Host "`n=== PROFILER LOG ==="
if (Test-Path 'C:\Users\bash\PROFILER_LOG.txt') {
    Get-Content 'C:\Users\bash\PROFILER_LOG.txt'
} else {
    Write-Host "NO PROFILER LOG"
}
