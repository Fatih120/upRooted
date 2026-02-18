$hookLog = "$env:LOCALAPPDATA\Root Communications\Root\profile\default\uprooted-hook.log"
Remove-Item $hookLog -ErrorAction SilentlyContinue

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$profilerDll = Join-Path $scriptDir "..\tools\uprooted_profiler.dll"

$env:DOTNET_EnableDiagnostics = "1"
$env:DOTNET_ENABLE_PROFILING = "1"
$env:DOTNET_PROFILER = "{D1A6F5A0-1234-4567-89AB-CDEF01234567}"
$env:DOTNET_PROFILER_PATH = (Resolve-Path $profilerDll).Path
$env:DOTNET_ReadyToRun = "0"

$proc = Start-Process "$env:LOCALAPPDATA\Root\current\Root.exe" -PassThru
Write-Host "Root.exe PID: $($proc.Id)"
Write-Host "Waiting for hook to load..."

# Wait for hook to be ready
for ($i = 0; $i -lt 30; $i++) {
    Start-Sleep -Seconds 2
    $elapsed = ($i + 1) * 2
    if ($proc.HasExited) {
        Write-Host "CRASHED at T+${elapsed}s"
        if (Test-Path $hookLog) {
            Write-Host "--- Last log entries ---"
            Get-Content $hookLog | Select-Object -Last 20
        }
        break
    }
    if (Test-Path $hookLog) {
        $content = Get-Content $hookLog -Raw
        if ($content -match "Uprooted Hook Ready") {
            Write-Host "=== HOOK READY (T+${elapsed}s) ==="
            Get-Content $hookLog | Select-Object -Last 15
            Write-Host ""
            Write-Host "Open Settings in Root to trigger injection."
            Write-Host "Then check: Get-Content '$hookLog' -Tail 20"
            break
        }
    }
    if ($elapsed -le 10 -or ($elapsed % 10) -eq 0) {
        Write-Host "T+${elapsed}s: waiting..."
    }
}
