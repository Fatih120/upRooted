$dllPath = "C:\Users\bash\claude\Root.Dev\uprooted\hook-test\bin\Release\net10.0\MinimalHook.dll"
$rootExe = "C:\Users\bash\AppData\Local\Root\current\Root.exe"

Remove-Item "C:\Users\bash\HOOK_LOADED.txt" -Force -ErrorAction SilentlyContinue
Remove-Item "C:\Users\bash\HOOK_ERROR.txt" -Force -ErrorAction SilentlyContinue

$psi = New-Object System.Diagnostics.ProcessStartInfo
$psi.FileName = $rootExe
$psi.UseShellExecute = $false
$psi.EnvironmentVariables["DOTNET_STARTUP_HOOKS"] = $dllPath
# Disable ReadyToRun to force JIT - maybe IL is intact
$psi.EnvironmentVariables["DOTNET_ReadyToRun"] = "0"

Write-Host "Launching Root with DOTNET_ReadyToRun=0 and DOTNET_STARTUP_HOOKS..."
$proc = [System.Diagnostics.Process]::Start($psi)
Write-Host "Root PID: $($proc.Id)"

Write-Host "Waiting 15 seconds..."
Start-Sleep -Seconds 15

if (Test-Path "C:\Users\bash\HOOK_LOADED.txt") {
    Write-Host "SUCCESS: Hook loaded!"
    Get-Content "C:\Users\bash\HOOK_LOADED.txt"
} elseif (Test-Path "C:\Users\bash\HOOK_ERROR.txt") {
    Write-Host "HOOK ERROR:"
    Get-Content "C:\Users\bash\HOOK_ERROR.txt"
} else {
    Write-Host "No hook marker - still not loading"
}

$logPath = "C:\Users\bash\AppData\Local\Root Communications\Root\profile\default\uprooted-hook.log"
if (Test-Path $logPath) {
    Write-Host "`n=== Uprooted Log ==="
    Get-Content $logPath -Tail 20
}

Stop-Process -Id $proc.Id -Force -ErrorAction SilentlyContinue
