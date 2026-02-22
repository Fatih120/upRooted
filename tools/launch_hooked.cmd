@echo off
REM Launch Root with Uprooted hook via DOTNET_STARTUP_HOOKS
REM Requires hook to be built first: dotnet build hook/UprootedHook.csproj -c Release

setlocal

set "REPO_ROOT=%~dp0.."
set DOTNET_STARTUP_HOOKS=%REPO_ROOT%\hook\bin\Release\net10.0\UprootedHook.dll
set UPROOTED_DIST=%REPO_ROOT%\dist
del /q "%LOCALAPPDATA%\Root Communications\Root\profile\default\uprooted-hook.log" 2>nul
echo Starting Root with Uprooted hook...
start "" "%LOCALAPPDATA%\Root\current\Root.exe"
echo Root launched. Check uprooted-hook.log for output.
