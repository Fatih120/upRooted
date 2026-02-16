# Rebuild profiler DLL using MSVC
$toolsDir = "C:\Users\bash\claude\Root.Dev\uprooted\tools"

# Delete old files
Remove-Item "$toolsDir\uprooted_profiler.dll" -ErrorAction SilentlyContinue
Remove-Item "$toolsDir\uprooted_profiler.obj" -ErrorAction SilentlyContinue
Remove-Item "$toolsDir\uprooted_profiler.lib" -ErrorAction SilentlyContinue
Remove-Item "$toolsDir\uprooted_profiler.exp" -ErrorAction SilentlyContinue

# Run build in cmd with vcvars
$buildCmd = @"
call "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1
cd /d "$toolsDir"
cl.exe /LD /O2 /Fe:uprooted_profiler.dll uprooted_profiler.c /link ole32.lib kernel32.lib /DEF:uprooted_profiler.def
"@

$result = cmd /c $buildCmd 2>&1
Write-Host $result

if (Test-Path "$toolsDir\uprooted_profiler.dll") {
    $f = Get-Item "$toolsDir\uprooted_profiler.dll"
    Write-Host "`nBUILD OK - Size: $($f.Length) Modified: $($f.LastWriteTime)"
} else {
    Write-Host "`nBUILD FAILED"
}
