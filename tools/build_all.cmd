@echo off
call "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1

cd /d "C:\Users\bash\claude\Root.Dev\uprooted\tools"

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
