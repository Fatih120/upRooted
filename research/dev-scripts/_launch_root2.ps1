# Kill existing Root.exe
Get-Process Root -ErrorAction SilentlyContinue | Stop-Process -Force
Start-Sleep -Seconds 3

# Clear old logs
Remove-Item 'C:\Users\bash\PROFILER_LOG.txt' -ErrorAction SilentlyContinue
Remove-Item 'C:\Users\bash\PROXY_DLL_LOG.txt' -ErrorAction SilentlyContinue

$rootPath = "$env:LOCALAPPDATA\Root\current\Root.exe"

# Set env vars in CURRENT process so child inherits them
$env:CORECLR_ENABLE_PROFILING = "1"
$env:CORECLR_PROFILER = "{D1A6F5A0-1234-4567-89AB-CDEF01234567}"
$env:CORECLR_PROFILER_PATH = "C:\Users\bash\claude\Root.Dev\uprooted\tools\uprooted_profiler.dll"

Write-Host "Env vars set in current process:"
Write-Host "  CORECLR_ENABLE_PROFILING = $env:CORECLR_ENABLE_PROFILING"
Write-Host "  CORECLR_PROFILER = $env:CORECLR_PROFILER"
Write-Host "  CORECLR_PROFILER_PATH = $env:CORECLR_PROFILER_PATH"
Write-Host ""
Write-Host "Launching: $rootPath"

# Start Root.exe - it inherits current process env vars
$proc = Start-Process $rootPath -PassThru
Write-Host "Root.exe started with PID: $($proc.Id)"

Start-Sleep -Seconds 10

# Check for diagnostic pipe
$procId = $proc.Id
$pipes = [System.IO.Directory]::GetFiles('\\.\pipe\') | Where-Object { $_ -like "*dotnet-diagnostic-$procId*" }
if ($pipes) {
    Write-Host "`n*** DIAGNOSTIC PIPE FOUND for PID $procId ***"
    foreach ($p in $pipes) { Write-Host "  $p" }
} else {
    Write-Host "`n*** NO DIAGNOSTIC PIPE for PID $procId ***"
}

# Check logs
Write-Host "`n=== PROFILER LOG ==="
if (Test-Path 'C:\Users\bash\PROFILER_LOG.txt') {
    Get-Content 'C:\Users\bash\PROFILER_LOG.txt'
} else {
    Write-Host "*** NO PROFILER LOG - profiler did NOT load ***"
}

Write-Host "`n=== PROXY LOG (first 15 lines) ==="
if (Test-Path 'C:\Users\bash\PROXY_DLL_LOG.txt') {
    Get-Content 'C:\Users\bash\PROXY_DLL_LOG.txt' | Select-Object -First 15
} else {
    Write-Host "No proxy log"
}
