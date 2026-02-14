@echo off
del /q "C:\Users\bash\HOOK_LOADED.txt" 2>nul
del /q "C:\Users\bash\root_host_trace.log" 2>nul

set "DOTNET_STARTUP_HOOKS=C:\Users\bash\claude\Root.Dev\uprooted\hook-test\bin\Release\net10.0\MinimalHook.dll"
set "COREHOST_TRACE=1"
set "COREHOST_TRACEFILE=C:\Users\bash\root_host_trace.log"

echo Launching Root with host tracing enabled...
"%LOCALAPPDATA%\Root\current\Root.exe"
