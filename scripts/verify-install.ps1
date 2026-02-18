$s = New-Object -ComObject WScript.Shell
$paths = @(
    (Join-Path $env:APPDATA "Microsoft\Windows\Start Menu\Programs\Root.lnk"),
    (Join-Path ([Environment]::GetFolderPath('Desktop')) "Root.lnk"),
    (Join-Path $env:APPDATA "Microsoft\Windows\Start Menu\Programs\Startup\Root.lnk"),
    (Join-Path $env:APPDATA "Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar\Root.lnk")
)
Write-Host "--- Shortcuts ---"
foreach ($p in $paths) {
    if (Test-Path $p) {
        $lnk = $s.CreateShortcut($p)
        Write-Host ((Split-Path $p -Leaf) + ": " + $lnk.TargetPath)
    }
}
Write-Host ""
Write-Host "--- Protocol Handler ---"
$regPath = 'HKCU:\SOFTWARE\Classes\rootapp\shell\open\command'
if (Test-Path $regPath) {
    Write-Host ((Get-ItemProperty $regPath).'(default)')
} else {
    Write-Host "<not registered>"
}
Write-Host ""
Write-Host "--- Settings ---"
$settingsPath = Join-Path $env:LOCALAPPDATA "Root Communications\Root\profile\default\uprooted-settings.ini"
if (Test-Path $settingsPath) {
    Get-Content $settingsPath
} else {
    Write-Host "<settings file not found>"
}
