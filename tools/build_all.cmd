@echo off
REM Build all native artifacts (uiohook proxy + profiler)
REM Requires Visual Studio Build Tools with C++ workload

setlocal

set "TOOLS_DIR=%~dp0"

REM Find vcvarsall.bat via vswhere
for /f "usebackq tokens=*" %%i in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath 2^>nul`) do set "VSPATH=%%i"

if not defined VSPATH (
    echo ERROR: Visual Studio with C++ tools not found.
    exit /b 1
)

call "%VSPATH%\VC\Auxiliary\Build\vcvarsall.bat" x64 >nul 2>&1

cd /d "%TOOLS_DIR%"

echo === Building uiohook proxy ===
del /q uiohook.dll uiohook.obj uiohook.lib uiohook.exp 2>nul
cl.exe /LD /O2 /Fe:uiohook.dll uiohook_proxy.c /link kernel32.lib ole32.lib
echo.

echo === Building profiler ===
del /q uprooted_profiler.dll uprooted_profiler.obj uprooted_profiler.lib uprooted_profiler.exp 2>nul
cl.exe /LD /O2 /Fe:uprooted_profiler.dll uprooted_profiler.c /link ole32.lib kernel32.lib /DEF:uprooted_profiler.def
echo.

if exist uiohook.dll (echo uiohook.dll OK) else (echo uiohook.dll FAILED)
if exist uprooted_profiler.dll (echo uprooted_profiler.dll OK) else (echo uprooted_profiler.dll FAILED)
