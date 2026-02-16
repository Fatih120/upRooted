# Build profiler and test
$ErrorActionPreference = 'Continue'

# Kill Root first
Get-Process Root -ErrorAction SilentlyContinue | Stop-Process -Force
Start-Sleep -Seconds 2

# Build profiler
Write-Host "=== Building profiler ==="
Push-Location "C:\Users\bash\claude\Root.Dev\uprooted\tools"

# Set up MSVC
$vcvars = "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\VC\Auxiliary\Build\vcvars64.bat"
$buildCmd = @"
call "$vcvars" >nul 2>&1
cd /d "C:\Users\bash\claude\Root.Dev\uprooted\tools"
del /q uprooted_profiler.dll uprooted_profiler.obj uprooted_profiler.lib uprooted_profiler.exp 2>nul
cl.exe /LD /O2 /Fe:uprooted_profiler.dll uprooted_profiler.c /link ole32.lib kernel32.lib /DEF:uprooted_profiler.def
"@

cmd.exe /c $buildCmd

Pop-Location

if (Test-Path "C:\Users\bash\claude\Root.Dev\uprooted\tools\uprooted_profiler.dll") {
    $sz = (Get-Item "C:\Users\bash\claude\Root.Dev\uprooted\tools\uprooted_profiler.dll").Length
    Write-Host "profiler DLL built OK ($sz bytes)"
} else {
    Write-Host "BUILD FAILED!"
    exit 1
}

# Clear logs
Remove-Item 'C:\Users\bash\PROFILER_LOG.txt' -ErrorAction SilentlyContinue
Remove-Item 'C:\Users\bash\PROXY_DLL_LOG.txt' -ErrorAction SilentlyContinue

# Set env vars in current process
$env:CORECLR_ENABLE_PROFILING = "1"
$env:CORECLR_PROFILER = "{D1A6F5A0-1234-4567-89AB-CDEF01234567}"
$env:CORECLR_PROFILER_PATH = "C:\Users\bash\claude\Root.Dev\uprooted\tools\uprooted_profiler.dll"

# Launch Root
Write-Host "`n=== Launching Root.exe ==="
$proc = Start-Process "$env:LOCALAPPDATA\Root\current\Root.exe" -PassThru
Write-Host "PID: $($proc.Id)"

# Wait and check
Start-Sleep -Seconds 12

Write-Host "`n=== PROFILER LOG ==="
if (Test-Path 'C:\Users\bash\PROFILER_LOG.txt') {
    Get-Content 'C:\Users\bash\PROFILER_LOG.txt'
} else {
    Write-Host "NO PROFILER LOG"
}
