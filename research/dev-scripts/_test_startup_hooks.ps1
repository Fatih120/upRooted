# Test if DOTNET_STARTUP_HOOKS works with Root.exe
# The runtime config has System.StartupHookProvider.IsSupported: true (previously patched)

Get-Process Root -ErrorAction SilentlyContinue | Stop-Process -Force
Start-Sleep -Seconds 3

# Clear logs
Remove-Item 'C:\Users\bash\PROFILER_LOG.txt' -ErrorAction SilentlyContinue
Remove-Item 'C:\Users\bash\PROXY_DLL_LOG.txt' -ErrorAction SilentlyContinue

# Set ONLY startup hooks, disable profiler for this test
$env:CORECLR_ENABLE_PROFILING = $null
$env:CORECLR_PROFILER = $null
$env:CORECLR_PROFILER_PATH = $null
$env:DOTNET_STARTUP_HOOKS = "C:\Users\bash\claude\Root.Dev\uprooted\hook\bin\Release\net10.0\UprootedHook.dll"

Write-Host "DOTNET_STARTUP_HOOKS = $env:DOTNET_STARTUP_HOOKS"
Write-Host "Testing if startup hooks work..."

# Check if the hook DLL exists
if (Test-Path $env:DOTNET_STARTUP_HOOKS) {
    Write-Host "Hook DLL exists: $((Get-Item $env:DOTNET_STARTUP_HOOKS).Length) bytes"
} else {
    Write-Host "ERROR: Hook DLL not found!"
    exit 1
}

# Launch Root
$proc = Start-Process "$env:LOCALAPPDATA\Root\current\Root.exe" -PassThru
Write-Host "Root.exe PID: $($proc.Id)"

Start-Sleep -Seconds 10

# Check for hook log file
$hookLog = "$env:LOCALAPPDATA\Root Communications\Root\profile\default\uprooted-hook.log"
Write-Host "`n=== HOOK LOG ==="
if (Test-Path $hookLog) {
    Write-Host "*** STARTUP HOOKS WORK! ***"
    Get-Content $hookLog
} else {
    Write-Host "No hook log at: $hookLog"
    Write-Host "Startup hooks did NOT work."
}

# Also check proxy log for basic functionality
Write-Host "`n=== PROXY LOG (first 5 lines) ==="
if (Test-Path 'C:\Users\bash\PROXY_DLL_LOG.txt') {
    Get-Content 'C:\Users\bash\PROXY_DLL_LOG.txt' | Select-Object -First 5
} else {
    Write-Host "No proxy log"
}
