@echo off
call "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1

cd /d "C:\Users\bash\claude\Root.Dev\uprooted\tools"

cl.exe /LD /O2 /Fe:version.dll version_proxy.c /link /DEF:version.def user32.lib kernel32.lib

echo.
echo Build result: %ERRORLEVEL%
if exist version.dll (
    echo version.dll created!
    dir version.dll
) else (
    echo FAILED to create version.dll
)
