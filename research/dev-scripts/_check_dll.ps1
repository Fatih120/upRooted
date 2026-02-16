$dll = "C:\Users\bash\claude\Root.Dev\uprooted\tools\uprooted_profiler.dll"
if (Test-Path $dll) {
    $f = Get-Item $dll
    Write-Host "Size: $($f.Length) bytes"
    Write-Host "Modified: $($f.LastWriteTime)"
} else {
    Write-Host "DLL NOT FOUND - build failed"
}
