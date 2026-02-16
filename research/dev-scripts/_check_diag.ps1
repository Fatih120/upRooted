# Check Root.exe's diagnostic pipe and profiler status
$rootProc = Get-Process Root -ErrorAction SilentlyContinue | Select-Object -First 1
if (-not $rootProc) {
    Write-Host "Root.exe not running!"
    exit 1
}

$pid = $rootProc.Id
Write-Host "Root.exe PID: $pid"

# Check for diagnostic named pipe
$pipeName = "dotnet-diagnostic-$pid"
Write-Host "`nLooking for diagnostic pipe: \\.\pipe\$pipeName"
$pipes = [System.IO.Directory]::GetFiles('\\.\pipe\') | Where-Object { $_ -like "*dotnet-diagnostic*" }
Write-Host "All dotnet-diagnostic pipes:"
foreach ($p in $pipes) {
    Write-Host "  $p"
}

$targetPipe = $pipes | Where-Object { $_ -like "*$pid*" }
if ($targetPipe) {
    Write-Host "`n*** DIAGNOSTIC PIPE FOUND: $targetPipe ***"
    Write-Host "Diagnostics ARE enabled in this process"
} else {
    Write-Host "`n*** NO DIAGNOSTIC PIPE for PID $pid ***"
    Write-Host "Diagnostics may be DISABLED"
}

# Check loaded modules for any profiler
Write-Host "`nLoaded modules containing 'profil' or 'uprooted':"
$rootProc.Modules | Where-Object { $_.ModuleName -match "profil|uprooted" } | ForEach-Object {
    Write-Host "  $($_.ModuleName) at $($_.BaseAddress)"
}

# Check all env vars in process (using WMI)
Write-Host "`nChecking process environment for CORECLR/DOTNET vars..."
$wmiProc = Get-WmiObject Win32_Process -Filter "ProcessId = $pid" -ErrorAction SilentlyContinue
if ($wmiProc) {
    # Can't easily read env vars of another process, but we can check user env
    Write-Host "User env CORECLR_ENABLE_PROFILING: $([System.Environment]::GetEnvironmentVariable('CORECLR_ENABLE_PROFILING', 'User'))"
    Write-Host "User env CORECLR_PROFILER: $([System.Environment]::GetEnvironmentVariable('CORECLR_PROFILER', 'User'))"
    Write-Host "User env CORECLR_PROFILER_PATH: $([System.Environment]::GetEnvironmentVariable('CORECLR_PROFILER_PATH', 'User'))"
}

# Check if Root.exe has the env vars in its own environment
Write-Host "`nProcess env (from current process, should inherit user vars):"
Write-Host "CORECLR_ENABLE_PROFILING: $env:CORECLR_ENABLE_PROFILING"
Write-Host "CORECLR_PROFILER: $env:CORECLR_PROFILER"
Write-Host "DOTNET_EnableDiagnostics: $env:DOTNET_EnableDiagnostics"
