$proc = Get-Process Root -ErrorAction SilentlyContinue | Select-Object -First 1
if (-not $proc) { Write-Host "Root.exe not running"; exit 1 }

Write-Host "Root.exe PID: $($proc.Id)"
Write-Host ""
Write-Host "=== .NET Runtime DLLs ==="
$proc.Modules | Where-Object { $_.ModuleName -match 'coreclr|hostfxr|hostpolicy|clrjit|System\.' } |
    Select-Object -First 20 |
    Format-Table ModuleName, FileName -AutoSize

Write-Host ""
Write-Host "=== All loaded DLLs (count) ==="
$mods = $proc.Modules
Write-Host "Total modules: $($mods.Count)"

Write-Host ""
Write-Host "=== Interesting modules ==="
$proc.Modules | Where-Object { $_.ModuleName -match 'Avalonia|DotNet|Root|Harmony|ipc64|uiohook|hostfxr|coreclr|clrjit' } |
    Format-Table ModuleName, FileName -AutoSize
