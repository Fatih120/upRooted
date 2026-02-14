@echo off
rem Test 1: Point to nonexistent DLL to see if runtime checks it
set DOTNET_STARTUP_HOOKS=C:\nonexistent\fake_hook.dll
echo Testing with nonexistent hook DLL...
echo If Root crashes or shows an error, DOTNET_STARTUP_HOOKS is working.
echo If Root starts normally, the env var is being ignored.
"%LOCALAPPDATA%\Root\current\Root.exe"
echo Root exited with code: %ERRORLEVEL%
pause
