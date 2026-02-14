@echo off
call "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1

cd /d "C:\Users\bash\claude\Root.Dev\uprooted\tools"

del /q uiohook.dll uiohook.obj uiohook.lib uiohook.exp 2>nul

cl.exe /LD /O2 /Fe:uiohook.dll uiohook_proxy.c /link kernel32.lib ole32.lib

echo.
echo Build result: %ERRORLEVEL%
if exist uiohook.dll (
    echo SUCCESS: uiohook.dll proxy created!
    dir uiohook.dll
) else (
    echo FAILED: uiohook.dll was not created
)
