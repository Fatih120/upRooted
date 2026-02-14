# Test 1: MinimalHook (known working)
$minimalDll = "C:\Users\bash\claude\Root.Dev\uprooted\hook-test\bin\Release\net10.0\MinimalHook.dll"
$uprootedDll = "C:\Users\bash\claude\Root.Dev\uprooted\hook\bin\Release\net10.0\UprootedHook.dll"
$rootExe = "C:\Users\bash\AppData\Local\Root\current\Root.exe"

# Clean up marker files
Remove-Item "C:\Users\bash\HOOK_LOADED.txt" -Force -ErrorAction SilentlyContinue
Remove-Item "C:\Users\bash\HOOK_ERROR.txt" -Force -ErrorAction SilentlyContinue

$testDll = $args[0]
if (-not $testDll) { $testDll = $minimalDll }

Write-Host "Testing with: $testDll"
Write-Host "DLL exists: $(Test-Path $testDll)"

$psi = New-Object System.Diagnostics.ProcessStartInfo
$psi.FileName = $rootExe
$psi.UseShellExecute = $false
$psi.EnvironmentVariables["DOTNET_STARTUP_HOOKS"] = $testDll

$proc = [System.Diagnostics.Process]::Start($psi)
Write-Host "Root PID: $($proc.Id)"
Write-Host "Waiting 10 seconds..."
Start-Sleep -Seconds 10

if (Test-Path "C:\Users\bash\HOOK_LOADED.txt") {
    Write-Host "SUCCESS: MinimalHook loaded!"
    Get-Content "C:\Users\bash\HOOK_LOADED.txt"
} elseif (Test-Path "C:\Users\bash\HOOK_ERROR.txt") {
    Write-Host "HOOK ERROR:"
    Get-Content "C:\Users\bash\HOOK_ERROR.txt"
} else {
    Write-Host "FAILED: No marker file found"
}

$logPath = "C:\Users\bash\AppData\Local\Root Communications\Root\profile\default\uprooted-hook.log"
if (Test-Path $logPath) {
    Write-Host "`n=== Uprooted Hook Log ==="
    Get-Content $logPath -Tail 30
}

Stop-Process -Id $proc.Id -Force -ErrorAction SilentlyContinue
