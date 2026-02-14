$log = "$env:LOCALAPPDATA\Root Communications\Root\profile\default\uprooted-hook.log"
for ($i = 0; $i -lt 120; $i++) {
    Start-Sleep -Seconds 5
    $content = Get-Content $log -Raw -ErrorAction SilentlyContinue
    if ($content -match "END STYLE RECON") {
        Get-Content $log -Tail 150
        exit 0
    }
}
Write-Host "Timed out"
Get-Content $log -Tail 30
