@echo off
call "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1

cd /d "C:\Users\bash\claude\Root.Dev\uprooted\tools"

echo Deleting old profiler DLL...
del /q uprooted_profiler.dll uprooted_profiler.obj uprooted_profiler.lib uprooted_profiler.exp 2>nul

echo Building profiler...
cl.exe /LD /O2 /Fe:uprooted_profiler.dll uprooted_profiler.c /link ole32.lib kernel32.lib /DEF:uprooted_profiler.def
echo.
if exist uprooted_profiler.dll (
    echo BUILD OK
    dir uprooted_profiler.dll
) else (
    echo BUILD FAILED
)
