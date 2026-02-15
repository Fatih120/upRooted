$s = New-Object -ComObject WScript.Shell
$paths = @(
    'C:\Users\bash\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Root.lnk',
    'C:\Users\bash\Desktop\Root.lnk',
    'C:\Users\bash\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\Root.lnk',
    'C:\Users\bash\AppData\Roaming\Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar\Root.lnk'
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
Write-Host ((Get-ItemProperty 'HKCU:\SOFTWARE\Classes\rootapp\shell\open\command').'(default)')
Write-Host ""
Write-Host "--- Settings ---"
Get-Content 'C:\Users\bash\AppData\Local\Root Communications\Root\profile\default\uprooted-settings.ini'
