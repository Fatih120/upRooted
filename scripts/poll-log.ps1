$log = "$env:LOCALAPPDATA\Root Communications\Root\profile\default\uprooted-hook.log"
for ($i = 0; $i -lt 120; $i++) {
    Start-Sleep -Seconds 5
    $content = Get-Content $log -Raw -ErrorAction SilentlyContinue
    if ($content -match "VERSION BOX RECON") {
        Get-Content $log -Tail 120
        exit 0
    }
}
Write-Host "Timed out waiting for version recon"
Get-Content $log -Tail 30
