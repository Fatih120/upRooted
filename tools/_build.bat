@echo off
call "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1

cd /d "C:\Users\bash\workspace\no-more-configs\projects\uprooted\uprooted-private\tools"

echo === Building profiler ===
del /q uprooted_profiler.dll uprooted_profiler.obj uprooted_profiler.lib uprooted_profiler.exp 2>nul
cl.exe /LD /O2 /GS- /Fe:uprooted_profiler.dll uprooted_profiler.c /link ole32.lib kernel32.lib shell32.lib /DEF:uprooted_profiler.def /DEBUG:NONE /OPT:REF /OPT:ICF
if not exist uprooted_profiler.dll (
    echo PROFILER BUILD FAILED
    exit /b 1
)
echo PROFILER BUILD SUCCESS

echo === Building launcher ===
del /q UprootedLauncher.exe uprooted_launcher.obj 2>nul
cl.exe /O2 /GS- /Fe:UprootedLauncher.exe uprooted_launcher.c /link kernel32.lib shell32.lib ole32.lib user32.lib /SUBSYSTEM:WINDOWS /DEBUG:NONE /OPT:REF /OPT:ICF
if not exist UprootedLauncher.exe (
    echo LAUNCHER BUILD FAILED
    exit /b 1
)
echo LAUNCHER BUILD SUCCESS
echo === ALL BUILDS DONE ===
