@echo off
del /q "C:\Users\bash\HOOK_LOADED.txt" 2>nul
del /q "C:\Users\bash\HOOK_ERROR.txt" 2>nul

set DOTNET_STARTUP_HOOKS=C:\Users\bash\claude\Root.Dev\uprooted\hook-test\bin\Release\net10.0\MinimalHook.dll
echo DOTNET_STARTUP_HOOKS=%DOTNET_STARTUP_HOOKS%
echo.

echo Verifying DLL exists:
if exist "%DOTNET_STARTUP_HOOKS%" (echo   YES) else (echo   NO - DLL not found!)
echo.

echo Launching Root.exe...
start "" "%LOCALAPPDATA%\Root\current\Root.exe"

echo Waiting 8 seconds...
timeout /t 8 /nobreak >nul

echo.
echo Checking for marker files:
if exist "C:\Users\bash\HOOK_LOADED.txt" (
    echo   HOOK_LOADED.txt exists!
    type "C:\Users\bash\HOOK_LOADED.txt"
) else (
    echo   HOOK_LOADED.txt NOT found
)
if exist "C:\Users\bash\HOOK_ERROR.txt" (
    echo   HOOK_ERROR.txt exists!
    type "C:\Users\bash\HOOK_ERROR.txt"
) else (
    echo   HOOK_ERROR.txt NOT found
)
echo.
pause
