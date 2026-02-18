@echo off
REM Build profiler DLL and launcher from the tools directory
REM Requires Visual Studio Build Tools with C++ workload

setlocal

REM Use script's own directory as tools dir
set "TOOLS_DIR=%~dp0"

REM Find vcvarsall.bat via vswhere
for /f "usebackq tokens=*" %%i in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath 2^>nul`) do set "VSPATH=%%i"

if not defined VSPATH (
    echo ERROR: Visual Studio with C++ tools not found.
    exit /b 1
)

call "%VSPATH%\VC\Auxiliary\Build\vcvarsall.bat" x64 >nul 2>&1
if errorlevel 1 (
    echo vcvarsall.bat failed
    exit /b 1
)

cd /d "%TOOLS_DIR%"

echo === Building profiler ===
del /q uprooted_profiler.dll uprooted_profiler.obj uprooted_profiler.lib uprooted_profiler.exp 2>nul
cl.exe /LD /O2 /Fe:uprooted_profiler.dll uprooted_profiler.c /link ole32.lib kernel32.lib shell32.lib /DEF:uprooted_profiler.def
if exist uprooted_profiler.dll (
    echo PROFILER BUILD SUCCESS
) else (
    echo PROFILER BUILD FAILED
    exit /b 1
)

echo.
echo === Building launcher ===
del /q UprootedLauncher.exe UprootedLauncher.obj 2>nul
cl.exe /O2 /Fe:UprootedLauncher.exe uprooted_launcher.c /link kernel32.lib shell32.lib ole32.lib user32.lib /SUBSYSTEM:WINDOWS
if exist UprootedLauncher.exe (
    echo LAUNCHER BUILD SUCCESS
) else (
    echo LAUNCHER BUILD FAILED
    exit /b 1
)
