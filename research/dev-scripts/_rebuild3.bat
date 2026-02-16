@echo off
call "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\VC\Auxiliary\Build\vcvars64.bat"
if errorlevel 1 (
    echo VCVARS FAILED
    exit /b 1
)
echo VCVARS OK
cd /d "C:\Users\bash\claude\Root.Dev\uprooted\tools"
del /q uprooted_profiler.dll uprooted_profiler.obj uprooted_profiler.lib uprooted_profiler.exp 2>nul
echo Building...
cl.exe /LD /O2 /Fe:uprooted_profiler.dll uprooted_profiler.c /link ole32.lib kernel32.lib /DEF:uprooted_profiler.def
if exist uprooted_profiler.dll (
    echo BUILD_OK
) else (
    echo BUILD_FAILED
)
