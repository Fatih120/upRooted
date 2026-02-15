@echo off
REM Build UprootedLauncher.exe - the transparent Root.exe wrapper
REM Requires Visual Studio Build Tools with C++ workload

setlocal

set "TOOLS_DIR=%~dp0"
set "SRC=%TOOLS_DIR%uprooted_launcher.c"
set "OUT=%TOOLS_DIR%UprootedLauncher.exe"

REM Find vcvarsall.bat
for /f "usebackq tokens=*" %%i in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath 2^>nul`) do set "VSPATH=%%i"

if not defined VSPATH (
    echo ERROR: Visual Studio with C++ tools not found.
    exit /b 1
)

call "%VSPATH%\VC\Auxiliary\Build\vcvarsall.bat" x64 >nul 2>&1

echo Building UprootedLauncher.exe...
cl.exe /O2 /W3 /Fe:"%OUT%" "%SRC%" /link kernel32.lib shell32.lib ole32.lib user32.lib /SUBSYSTEM:WINDOWS /ENTRY:wWinMainCRTStartup

if errorlevel 1 (
    echo BUILD FAILED
    exit /b 1
)

echo.
echo Built: %OUT%
for %%f in ("%OUT%") do echo Size: %%~zf bytes
