# Full injection test: profiler -> IL injection -> managed hook
Get-Process Root -ErrorAction SilentlyContinue | Stop-Process -Force
Start-Sleep -Seconds 2

# Clear all logs
Remove-Item 'C:\Users\bash\PROFILER_LOG.txt' -ErrorAction SilentlyContinue
$hookLog = "$env:LOCALAPPDATA\Root Communications\Root\profile\default\uprooted-hook.log"
Remove-Item $hookLog -ErrorAction SilentlyContinue

# Verify DLLs exist
$profDll = "C:\Users\bash\claude\Root.Dev\uprooted\tools\uprooted_profiler.dll"
$hookDll = "C:\Users\bash\claude\Root.Dev\uprooted\hook\bin\Release\net10.0\UprootedHook.dll"
Write-Host "Profiler DLL: $(if (Test-Path $profDll) { (Get-Item $profDll).Length.ToString() + ' bytes' } else { 'MISSING!' })"
Write-Host "Hook DLL: $(if (Test-Path $hookDll) { (Get-Item $hookDll).Length.ToString() + ' bytes' } else { 'MISSING!' })"

# Set profiler env vars in current process
$env:CORECLR_ENABLE_PROFILING = "1"
$env:CORECLR_PROFILER = "{D1A6F5A0-1234-4567-89AB-CDEF01234567}"
$env:CORECLR_PROFILER_PATH = $profDll

Write-Host "`nLaunching Root.exe with profiler..."
$proc = Start-Process "$env:LOCALAPPDATA\Root\current\Root.exe" -PassThru
Write-Host "Root.exe PID: $($proc.Id)"

Write-Host "Waiting 20 seconds for injection..."
Start-Sleep -Seconds 20

Write-Host "`n=== PROFILER LOG ==="
if (Test-Path 'C:\Users\bash\PROFILER_LOG.txt') {
    Get-Content 'C:\Users\bash\PROFILER_LOG.txt'
} else {
    Write-Host "NO PROFILER LOG"
}

Write-Host "`n=== HOOK LOG ==="
if (Test-Path $hookLog) {
    Write-Host "*** MANAGED HOOK IS RUNNING! ***"
    Get-Content $hookLog
} else {
    Write-Host "No hook log (managed code may not have loaded)"
}
