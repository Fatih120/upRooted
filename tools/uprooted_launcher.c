/*
 * Uprooted Launcher - transparent Root.exe wrapper
 *
 * Replaces Root.exe in shortcuts and protocol handlers.
 * If Uprooted is installed and enabled, sets CLR profiler env vars
 * before launching Root.exe so that uprooted_profiler.dll gets loaded.
 * If disabled or missing, launches Root.exe normally.
 *
 * No console window (WinMain entry point).
 * Passes through all command-line arguments (e.g., rootapp:// URLs).
 *
 * Build: cl.exe /O2 /Fe:UprootedLauncher.exe uprooted_launcher.c
 *        /link kernel32.lib shell32.lib ole32.lib user32.lib /SUBSYSTEM:WINDOWS
 */

#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <shellapi.h>
#include <shlobj.h>
#include <stdio.h>
#include <string.h>

#pragma warning(disable: 4996) /* _wgetenv deprecation */

#pragma comment(lib, "kernel32.lib")
#pragma comment(lib, "shell32.lib")
#pragma comment(lib, "ole32.lib")

static WCHAR g_localAppData[MAX_PATH];

static BOOL InitLocalAppData(void) {
    PWSTR pPath = NULL;
    if (SUCCEEDED(SHGetKnownFolderPath(&FOLDERID_LocalAppData, 0, NULL, &pPath))) {
        wcscpy_s(g_localAppData, MAX_PATH, pPath);
        CoTaskMemFree(pPath);
        return TRUE;
    }
    return FALSE;
}

/* Read uprooted-settings.ini and check if Enabled=true */
static BOOL IsUprootedEnabled(void) {
    WCHAR settingsPath[MAX_PATH];
    _snwprintf_s(settingsPath, MAX_PATH, _TRUNCATE,
        L"%s\\Root Communications\\Root\\profile\\default\\uprooted-settings.ini",
        g_localAppData);

    FILE* f = NULL;
    if (_wfopen_s(&f, settingsPath, L"r") != 0 || !f)
        return FALSE;

    BOOL enabled = FALSE;
    char line[256];
    while (fgets(line, sizeof(line), f)) {
        if (strncmp(line, "Enabled=true", 12) == 0) {
            enabled = TRUE;
            break;
        }
    }
    fclose(f);
    return enabled;
}

/* Check that the profiler DLL exists */
static BOOL ProfilerExists(const WCHAR* profilerPath) {
    return GetFileAttributesW(profilerPath) != INVALID_FILE_ATTRIBUTES;
}

/* Skip past the exe name in the command line to get just the arguments */
static LPCWSTR GetArgsFromCommandLine(LPCWSTR cmdLine) {
    if (!cmdLine || !*cmdLine) return L"";

    BOOL inQuote = FALSE;
    const WCHAR* p = cmdLine;

    /* Skip past the first token (our own exe path) */
    while (*p) {
        if (*p == L'"') {
            inQuote = !inQuote;
        } else if (*p == L' ' && !inQuote) {
            break;
        }
        p++;
    }

    /* Skip whitespace between exe and args */
    while (*p == L' ') p++;

    return p;
}

int WINAPI wWinMain(HINSTANCE hInst, HINSTANCE hPrev, LPWSTR lpCmdLine, int nCmdShow) {
    if (!InitLocalAppData()) {
        /* Fallback: just launch Root.exe without injection */
        WCHAR fallback[MAX_PATH];
        _snwprintf_s(fallback, MAX_PATH, _TRUNCATE,
            L"C:\\Users\\%s\\AppData\\Local\\Root\\current\\Root.exe", _wgetenv(L"USERNAME"));
        ShellExecuteW(NULL, L"open", fallback, lpCmdLine, NULL, SW_SHOWNORMAL);
        return 1;
    }

    WCHAR rootExe[MAX_PATH];
    WCHAR rootDir[MAX_PATH];
    WCHAR profilerDll[MAX_PATH];

    _snwprintf_s(rootExe, MAX_PATH, _TRUNCATE,
        L"%s\\Root\\current\\Root.exe", g_localAppData);
    _snwprintf_s(rootDir, MAX_PATH, _TRUNCATE,
        L"%s\\Root\\current", g_localAppData);
    _snwprintf_s(profilerDll, MAX_PATH, _TRUNCATE,
        L"%s\\Root\\uprooted\\uprooted_profiler.dll", g_localAppData);

    /* Check if Root.exe exists */
    if (GetFileAttributesW(rootExe) == INVALID_FILE_ATTRIBUTES) {
        MessageBoxW(NULL,
            L"Root.exe not found. Is Root Communications installed?",
            L"Uprooted Launcher", MB_ICONERROR);
        return 1;
    }

    /* Set CLR profiler env vars if Uprooted is enabled and profiler DLL exists.
     * .NET 10 requires DOTNET_ prefix; CORECLR_ prefix kept for older runtimes. */
    if (IsUprootedEnabled() && ProfilerExists(profilerDll)) {
        /* .NET 10+ (DOTNET_ prefix) */
        SetEnvironmentVariableW(L"DOTNET_EnableDiagnostics", L"1");
        SetEnvironmentVariableW(L"DOTNET_ENABLE_PROFILING", L"1");
        SetEnvironmentVariableW(L"DOTNET_PROFILER",
            L"{D1A6F5A0-1234-4567-89AB-CDEF01234567}");
        SetEnvironmentVariableW(L"DOTNET_PROFILER_PATH", profilerDll);
        SetEnvironmentVariableW(L"DOTNET_ReadyToRun", L"0");

        /* Legacy (.NET 8/9, CORECLR_ prefix) */
        SetEnvironmentVariableW(L"CORECLR_ENABLE_PROFILING", L"1");
        SetEnvironmentVariableW(L"CORECLR_PROFILER",
            L"{D1A6F5A0-1234-4567-89AB-CDEF01234567}");
        SetEnvironmentVariableW(L"CORECLR_PROFILER_PATH", profilerDll);
    }

    /* Build command line: "Root.exe" <original args> */
    LPCWSTR origArgs = GetArgsFromCommandLine(GetCommandLineW());
    WCHAR cmdLine[4096];
    if (origArgs && *origArgs) {
        _snwprintf_s(cmdLine, 4096, _TRUNCATE, L"\"%s\" %s", rootExe, origArgs);
    } else {
        _snwprintf_s(cmdLine, 4096, _TRUNCATE, L"\"%s\"", rootExe);
    }

    /* Launch Root.exe */
    STARTUPINFOW si;
    PROCESS_INFORMATION pi;
    ZeroMemory(&si, sizeof(si));
    si.cb = sizeof(si);
    ZeroMemory(&pi, sizeof(pi));

    BOOL ok = CreateProcessW(
        rootExe,        /* lpApplicationName */
        cmdLine,        /* lpCommandLine */
        NULL,           /* lpProcessAttributes */
        NULL,           /* lpThreadAttributes */
        FALSE,          /* bInheritHandles */
        0,              /* dwCreationFlags */
        NULL,           /* lpEnvironment (inherit from us, including our env vars) */
        rootDir,        /* lpCurrentDirectory */
        &si,
        &pi
    );

    if (ok) {
        CloseHandle(pi.hProcess);
        CloseHandle(pi.hThread);
    } else {
        /* CreateProcess failed, try ShellExecute as fallback */
        ShellExecuteW(NULL, L"open", rootExe, origArgs, rootDir, SW_SHOWNORMAL);
    }

    return 0;
}
