@echo off
call "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1
if errorlevel 1 (
    echo vcvars64.bat failed
    exit /b 1
)
echo cl.exe at:
where cl.exe
cd /d "C:\Users\bash\workspace\no-more-configs\projects\uprooted\uprooted-private\tools"
del /q uprooted_profiler.dll uprooted_profiler.obj uprooted_profiler.lib uprooted_profiler.exp 2>nul
cl.exe /LD /O2 /Fe:uprooted_profiler.dll uprooted_profiler.c /link ole32.lib kernel32.lib shell32.lib /DEF:uprooted_profiler.def
if exist uprooted_profiler.dll (
    echo PROFILER BUILD SUCCESS
) else (
    echo PROFILER BUILD FAILED
    exit /b 1
)
echo.
echo Building launcher...
del /q UprootedLauncher.exe UprootedLauncher.obj 2>nul
cl.exe /O2 /Fe:UprootedLauncher.exe uprooted_launcher.c /link kernel32.lib shell32.lib ole32.lib user32.lib /SUBSYSTEM:WINDOWS
if exist UprootedLauncher.exe (
    echo LAUNCHER BUILD SUCCESS
) else (
    echo LAUNCHER BUILD FAILED
    exit /b 1
)
