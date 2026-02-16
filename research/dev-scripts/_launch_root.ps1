# Kill existing Root.exe
Get-Process Root -ErrorAction SilentlyContinue | Stop-Process -Force
Start-Sleep -Seconds 2

# Launch Root.exe
$rootPath = "$env:LOCALAPPDATA\Root\current\Root.exe"
Write-Host "Launching: $rootPath"
Write-Host "CORECLR_ENABLE_PROFILING = $([System.Environment]::GetEnvironmentVariable('CORECLR_ENABLE_PROFILING', 'User'))"
Write-Host "CORECLR_PROFILER = $([System.Environment]::GetEnvironmentVariable('CORECLR_PROFILER', 'User'))"
Write-Host "CORECLR_PROFILER_PATH = $([System.Environment]::GetEnvironmentVariable('CORECLR_PROFILER_PATH', 'User'))"

Start-Process $rootPath
Start-Sleep -Seconds 10

# Check logs
Write-Host "`n=== PROFILER LOG ==="
if (Test-Path 'C:\Users\bash\PROFILER_LOG.txt') {
    Get-Content 'C:\Users\bash\PROFILER_LOG.txt'
} else {
    Write-Host "*** NO PROFILER LOG - profiler did NOT load ***"
}

Write-Host "`n=== PROXY LOG (first 20 lines) ==="
if (Test-Path 'C:\Users\bash\PROXY_DLL_LOG.txt') {
    Get-Content 'C:\Users\bash\PROXY_DLL_LOG.txt' | Select-Object -First 20
} else {
    Write-Host "No proxy log yet"
}
