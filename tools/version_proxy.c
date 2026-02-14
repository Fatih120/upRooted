/*
 * version.dll proxy for Uprooted injection into Root.exe
 *
 * Forwards all version.dll exports to the real system DLL.
 * On DLL_PROCESS_ATTACH, spawns a thread that writes a marker file.
 */

#define WIN32_LEAN_AND_MEAN
#define NOVERSION
#include <windows.h>
#include <stdio.h>

#pragma comment(lib, "user32.lib")
#pragma comment(lib, "kernel32.lib")

/* Handle to real version.dll */
static HMODULE hReal = NULL;

/* Generic function pointer type */
typedef void* FARPROC_T;

/* Pointers to real functions - indexed by ordinal-1 */
static FARPROC realFuncs[17];

static const char* funcNames[] = {
    "GetFileVersionInfoA",
    "GetFileVersionInfoByHandle",
    "GetFileVersionInfoExA",
    "GetFileVersionInfoExW",
    "GetFileVersionInfoSizeA",
    "GetFileVersionInfoSizeExA",
    "GetFileVersionInfoSizeExW",
    "GetFileVersionInfoSizeW",
    "GetFileVersionInfoW",
    "VerFindFileA",
    "VerFindFileW",
    "VerInstallFileA",
    "VerInstallFileW",
    "VerLanguageNameA",
    "VerLanguageNameW",
    "VerQueryValueA",
    "VerQueryValueW"
};

static void WriteLog(const char* msg) {
    FILE* f = fopen("C:\\Users\\bash\\PROXY_DLL_LOG.txt", "a");
    if (f) {
        SYSTEMTIME st;
        GetLocalTime(&st);
        fprintf(f, "[%02d:%02d:%02d.%03d] %s\n",
                st.wHour, st.wMinute, st.wSecond, st.wMilliseconds, msg);
        fclose(f);
    }
}

static BOOL LoadRealDLL(void) {
    char sysDir[MAX_PATH];
    char dllPath[MAX_PATH];

    GetSystemDirectoryA(sysDir, MAX_PATH);
    snprintf(dllPath, MAX_PATH, "%s\\version.dll", sysDir);

    hReal = LoadLibraryA(dllPath);
    if (!hReal) {
        WriteLog("FATAL: Could not load real version.dll");
        return FALSE;
    }

    for (int i = 0; i < 17; i++) {
        realFuncs[i] = GetProcAddress(hReal, funcNames[i]);
    }

    return TRUE;
}

/* Injection thread */
static DWORD WINAPI InjectorThread(LPVOID param) {
    char buf[512];
    DWORD pid = GetCurrentProcessId();

    snprintf(buf, sizeof(buf), "Proxy DLL loaded in PID %lu", pid);
    WriteLog(buf);

    FILE* f = fopen("C:\\Users\\bash\\HOOK_LOADED.txt", "w");
    if (f) {
        fprintf(f, "version.dll proxy loaded!\nPID: %lu\n", pid);
        fclose(f);
    }

    WriteLog("Marker file written. Waiting for .NET runtime...");
    Sleep(8000);

    /* Check for .NET diagnostics pipe */
    char pipeName[256];
    snprintf(pipeName, sizeof(pipeName), "\\\\.\\pipe\\dotnet-diagnostic-%lu", pid);

    HANDLE hPipe = CreateFileA(pipeName, GENERIC_READ | GENERIC_WRITE,
                               0, NULL, OPEN_EXISTING, 0, NULL);
    if (hPipe != INVALID_HANDLE_VALUE) {
        snprintf(buf, sizeof(buf), "Connected to diagnostics pipe: %s", pipeName);
        WriteLog(buf);
        CloseHandle(hPipe);
    } else {
        snprintf(buf, sizeof(buf), "Diagnostics pipe not found: %s (err %lu)",
                 pipeName, GetLastError());
        WriteLog(buf);
    }

    /* Check for coreclr.dll */
    HMODULE hCoreCLR = GetModuleHandleA("coreclr.dll");
    if (hCoreCLR) {
        snprintf(buf, sizeof(buf), "coreclr.dll at %p", (void*)hCoreCLR);
        WriteLog(buf);
    } else {
        WriteLog("coreclr.dll not found (embedded in singlefilehost)");
        HMODULE hHostPolicy = GetModuleHandleA("hostpolicy.dll");
        HMODULE hHostFxr = GetModuleHandleA("hostfxr.dll");
        snprintf(buf, sizeof(buf), "hostpolicy=%p hostfxr=%p",
                 (void*)hHostPolicy, (void*)hHostFxr);
        WriteLog(buf);
    }

    /* Try to find coreclr exports in the main exe module */
    HMODULE hExe = GetModuleHandleA(NULL);
    if (hExe) {
        /* In single-file hosts, coreclr_* functions are in the exe itself */
        FARPROC pInit = GetProcAddress(hExe, "coreclr_initialize");
        FARPROC pExec = GetProcAddress(hExe, "coreclr_execute_assembly");
        FARPROC pCreateDelegate = GetProcAddress(hExe, "coreclr_create_delegate");
        snprintf(buf, sizeof(buf),
                 "exe=%p coreclr_initialize=%p execute=%p create_delegate=%p",
                 (void*)hExe, (void*)pInit, (void*)pExec, (void*)pCreateDelegate);
        WriteLog(buf);
    }

    WriteLog("Injector thread completed.");
    return 0;
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD reason, LPVOID reserved) {
    if (reason == DLL_PROCESS_ATTACH) {
        DisableThreadLibraryCalls(hModule);
        if (!LoadRealDLL()) return FALSE;
        WriteLog("version.dll proxy attached to process");
        CreateThread(NULL, 0, InjectorThread, NULL, 0, NULL);
    }
    else if (reason == DLL_PROCESS_DETACH) {
        if (hReal) FreeLibrary(hReal);
    }
    return TRUE;
}

/* Proxy exports - use assembly-style trampolines via function pointers */
/* We use __declspec(naked) on MSVC x64 won't work, so we use regular wrappers */

/* Each proxy just calls through the function pointer.
   We rely on the .def file to export these with the correct names. */

__int64 __stdcall proxy_GetFileVersionInfoA(void* a, unsigned long b, unsigned long c, void* d) {
    typedef __int64(__stdcall* FN)(void*, unsigned long, unsigned long, void*);
    return ((FN)realFuncs[0])(a, b, c, d);
}
__int64 __stdcall proxy_GetFileVersionInfoByHandle(unsigned long a, void* b) {
    typedef __int64(__stdcall* FN)(unsigned long, void*);
    return ((FN)realFuncs[1])(a, b);
}
__int64 __stdcall proxy_GetFileVersionInfoExA(unsigned long fl, void* f, unsigned long h, unsigned long s, void* d) {
    typedef __int64(__stdcall* FN)(unsigned long, void*, unsigned long, unsigned long, void*);
    return ((FN)realFuncs[2])(fl, f, h, s, d);
}
__int64 __stdcall proxy_GetFileVersionInfoExW(unsigned long fl, void* f, unsigned long h, unsigned long s, void* d) {
    typedef __int64(__stdcall* FN)(unsigned long, void*, unsigned long, unsigned long, void*);
    return ((FN)realFuncs[3])(fl, f, h, s, d);
}
__int64 __stdcall proxy_GetFileVersionInfoSizeA(void* f, void* h) {
    typedef __int64(__stdcall* FN)(void*, void*);
    return ((FN)realFuncs[4])(f, h);
}
__int64 __stdcall proxy_GetFileVersionInfoSizeExA(unsigned long fl, void* f, void* h) {
    typedef __int64(__stdcall* FN)(unsigned long, void*, void*);
    return ((FN)realFuncs[5])(fl, f, h);
}
__int64 __stdcall proxy_GetFileVersionInfoSizeExW(unsigned long fl, void* f, void* h) {
    typedef __int64(__stdcall* FN)(unsigned long, void*, void*);
    return ((FN)realFuncs[6])(fl, f, h);
}
__int64 __stdcall proxy_GetFileVersionInfoSizeW(void* f, void* h) {
    typedef __int64(__stdcall* FN)(void*, void*);
    return ((FN)realFuncs[7])(f, h);
}
__int64 __stdcall proxy_GetFileVersionInfoW(void* f, unsigned long h, unsigned long s, void* d) {
    typedef __int64(__stdcall* FN)(void*, unsigned long, unsigned long, void*);
    return ((FN)realFuncs[8])(f, h, s, d);
}
__int64 __stdcall proxy_VerFindFileA(unsigned long fl, void* a, void* b, void* c, void* d, void* e, void* f, void* g) {
    typedef __int64(__stdcall* FN)(unsigned long, void*, void*, void*, void*, void*, void*, void*);
    return ((FN)realFuncs[9])(fl, a, b, c, d, e, f, g);
}
__int64 __stdcall proxy_VerFindFileW(unsigned long fl, void* a, void* b, void* c, void* d, void* e, void* f, void* g) {
    typedef __int64(__stdcall* FN)(unsigned long, void*, void*, void*, void*, void*, void*, void*);
    return ((FN)realFuncs[10])(fl, a, b, c, d, e, f, g);
}
__int64 __stdcall proxy_VerInstallFileA(unsigned long fl, void* a, void* b, void* c, void* d, void* e, void* f, void* g) {
    typedef __int64(__stdcall* FN)(unsigned long, void*, void*, void*, void*, void*, void*, void*);
    return ((FN)realFuncs[11])(fl, a, b, c, d, e, f, g);
}
__int64 __stdcall proxy_VerInstallFileW(unsigned long fl, void* a, void* b, void* c, void* d, void* e, void* f, void* g) {
    typedef __int64(__stdcall* FN)(unsigned long, void*, void*, void*, void*, void*, void*, void*);
    return ((FN)realFuncs[12])(fl, a, b, c, d, e, f, g);
}
__int64 __stdcall proxy_VerLanguageNameA(unsigned long l, void* b, unsigned long s) {
    typedef __int64(__stdcall* FN)(unsigned long, void*, unsigned long);
    return ((FN)realFuncs[13])(l, b, s);
}
__int64 __stdcall proxy_VerLanguageNameW(unsigned long l, void* b, unsigned long s) {
    typedef __int64(__stdcall* FN)(unsigned long, void*, unsigned long);
    return ((FN)realFuncs[14])(l, b, s);
}
__int64 __stdcall proxy_VerQueryValueA(void* b, void* sb, void* bp, void* pl) {
    typedef __int64(__stdcall* FN)(void*, void*, void*, void*);
    return ((FN)realFuncs[15])(b, sb, bp, pl);
}
__int64 __stdcall proxy_VerQueryValueW(void* b, void* sb, void* bp, void* pl) {
    typedef __int64(__stdcall* FN)(void*, void*, void*, void*);
    return ((FN)realFuncs[16])(b, sb, bp, pl);
}
