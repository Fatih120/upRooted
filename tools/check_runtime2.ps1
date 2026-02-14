$proc = Get-Process Root -ErrorAction SilentlyContinue | Select-Object -First 1
if (-not $proc) { Write-Host "Root.exe not running"; exit 1 }

Write-Host "=== ALL 118 modules ==="
$proc.Modules | Format-Table ModuleName, @{L='Size(KB)';E={[math]::Round($_.ModuleMemorySize/1024)}} -AutoSize

Write-Host ""
Write-Host "=== Check for .NET temp extraction ==="
$tempNet = "$env:LOCALAPPDATA\Temp\.net"
if (Test-Path $tempNet) {
    Get-ChildItem $tempNet -Recurse -Filter "coreclr.dll" -ErrorAction SilentlyContinue |
        ForEach-Object { Write-Host "Found: $($_.FullName)" }
} else {
    Write-Host "No .NET temp extraction directory"
}

Write-Host ""
Write-Host "=== Check Root.exe PE header for CLR ==="
$path = $proc.MainModule.FileName
$bytes = [System.IO.File]::ReadAllBytes($path)
# Check for CLR metadata directory in PE header
# PE sig at offset from 0x3C
$peOffset = [BitConverter]::ToInt32($bytes, 0x3C)
Write-Host "PE offset: 0x$($peOffset.ToString('X'))"
$magic = [BitConverter]::ToUInt16($bytes, $peOffset + 24)
Write-Host "PE Magic: 0x$($magic.ToString('X')) (0x20B=PE32+, 0x10B=PE32)"
# CLR header is data directory entry 14 (index 14)
# For PE32+: optional header starts at peOffset+24, data directories start at peOffset+24+112
$ddOffset = $peOffset + 24 + 112
$clrRva = [BitConverter]::ToUInt32($bytes, $ddOffset + 14*8)
$clrSize = [BitConverter]::ToUInt32($bytes, $ddOffset + 14*8 + 4)
Write-Host "CLR Data Directory: RVA=0x$($clrRva.ToString('X')), Size=$clrSize"
if ($clrRva -eq 0) {
    Write-Host ">>> NO CLR HEADER - This is a Native AOT binary!"
} else {
    Write-Host ">>> HAS CLR HEADER - This has managed metadata"
}
