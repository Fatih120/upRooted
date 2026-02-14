@echo off
set DOTNET_STARTUP_HOOKS=C:\Users\bash\claude\Root.Dev\uprooted\hook\bin\Release\net10.0\UprootedHook.dll
set UPROOTED_DIST=C:\Users\bash\claude\Root.Dev\uprooted\dist
del /q "%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log" 2>nul
echo Starting Root with Uprooted hook...
start "" "%LOCALAPPDATA%\Root\current\Root.exe"
echo Root launched. Check uprooted-hook.log for output.
