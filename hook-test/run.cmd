@echo off
del /q "C:\Users\bash\HOOK_LOADED.txt" 2>nul
del /q "C:\Users\bash\HOOK_ERROR.txt" 2>nul

set "DOTNET_STARTUP_HOOKS=C:\Users\bash\claude\Root.Dev\uprooted\hook-test\bin\Release\net10.0\MinimalHook.dll"

echo ENV: DOTNET_STARTUP_HOOKS=%DOTNET_STARTUP_HOOKS%
echo.

"%LOCALAPPDATA%\Root\current\Root.exe"
