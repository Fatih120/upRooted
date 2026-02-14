/*
 * uiohook.dll proxy for Uprooted injection into Root.exe
 *
 * All exports are forwarded to uiohook_real.dll via linker pragmas.
 * DllMain spawns two injection threads:
 *   Thread 1 (IPC): Hooks ipc64.dll to inject JS into DotNetBrowser frames
 *   Thread 2 (CLR): Uses hostfxr to load UprootedHook.dll for Avalonia injection
 */

#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <string.h>

#pragma comment(lib, "kernel32.lib")

/* Forward all uiohook exports to the real DLL */
#pragma comment(linker, "/export:_Avx2WmemEnabledWeakValue=uiohook_real._Avx2WmemEnabledWeakValue")
#pragma comment(linker, "/export:clear_modifier_mask=uiohook_real.clear_modifier_mask")
#pragma comment(linker, "/export:dispatch_button_press=uiohook_real.dispatch_button_press")
#pragma comment(linker, "/export:dispatch_button_release=uiohook_real.dispatch_button_release")
#pragma comment(linker, "/export:dispatch_hook_disable=uiohook_real.dispatch_hook_disable")
#pragma comment(linker, "/export:dispatch_hook_enable=uiohook_real.dispatch_hook_enable")
#pragma comment(linker, "/export:dispatch_key_press=uiohook_real.dispatch_key_press")
#pragma comment(linker, "/export:dispatch_key_release=uiohook_real.dispatch_key_release")
#pragma comment(linker, "/export:dispatch_mouse_move=uiohook_real.dispatch_mouse_move")
#pragma comment(linker, "/export:dispatch_mouse_wheel=uiohook_real.dispatch_mouse_wheel")
#pragma comment(linker, "/export:enumerate_displays=uiohook_real.enumerate_displays")
#pragma comment(linker, "/export:get_largest_negative_coordinates=uiohook_real.get_largest_negative_coordinates")
#pragma comment(linker, "/export:get_modifiers=uiohook_real.get_modifiers")
#pragma comment(linker, "/export:get_vk_code=uiohook_real.get_vk_code")
#pragma comment(linker, "/export:hook_create_screen_info=uiohook_real.hook_create_screen_info")
#pragma comment(linker, "/export:hook_get_auto_repeat_delay=uiohook_real.hook_get_auto_repeat_delay")
#pragma comment(linker, "/export:hook_get_auto_repeat_rate=uiohook_real.hook_get_auto_repeat_rate")
#pragma comment(linker, "/export:hook_get_ax_poll_frequency=uiohook_real.hook_get_ax_poll_frequency")
#pragma comment(linker, "/export:hook_get_multi_click_time=uiohook_real.hook_get_multi_click_time")
#pragma comment(linker, "/export:hook_get_pointer_acceleration_multiplier=uiohook_real.hook_get_pointer_acceleration_multiplier")
#pragma comment(linker, "/export:hook_get_pointer_acceleration_threshold=uiohook_real.hook_get_pointer_acceleration_threshold")
#pragma comment(linker, "/export:hook_get_pointer_sensitivity=uiohook_real.hook_get_pointer_sensitivity")
#pragma comment(linker, "/export:hook_get_post_text_delay_x11=uiohook_real.hook_get_post_text_delay_x11")
#pragma comment(linker, "/export:hook_get_prompt_user_if_ax_api_disabled=uiohook_real.hook_get_prompt_user_if_ax_api_disabled")
#pragma comment(linker, "/export:hook_is_ax_api_enabled=uiohook_real.hook_is_ax_api_enabled")
#pragma comment(linker, "/export:hook_is_key_typed_enabled=uiohook_real.hook_is_key_typed_enabled")
#pragma comment(linker, "/export:hook_post_event=uiohook_real.hook_post_event")
#pragma comment(linker, "/export:hook_post_events=uiohook_real.hook_post_events")
#pragma comment(linker, "/export:hook_post_text=uiohook_real.hook_post_text")
#pragma comment(linker, "/export:hook_run=uiohook_real.hook_run")
#pragma comment(linker, "/export:hook_run_keyboard=uiohook_real.hook_run_keyboard")
#pragma comment(linker, "/export:hook_run_mouse=uiohook_real.hook_run_mouse")
#pragma comment(linker, "/export:hook_set_ax_poll_frequency=uiohook_real.hook_set_ax_poll_frequency")
#pragma comment(linker, "/export:hook_set_dispatch_proc=uiohook_real.hook_set_dispatch_proc")
#pragma comment(linker, "/export:hook_set_key_typed_enabled=uiohook_real.hook_set_key_typed_enabled")
#pragma comment(linker, "/export:hook_set_logger_proc=uiohook_real.hook_set_logger_proc")
#pragma comment(linker, "/export:hook_set_post_text_delay_x11=uiohook_real.hook_set_post_text_delay_x11")
#pragma comment(linker, "/export:hook_set_prompt_user_if_ax_api_disabled=uiohook_real.hook_set_prompt_user_if_ax_api_disabled")
#pragma comment(linker, "/export:hook_stop=uiohook_real.hook_stop")
#pragma comment(linker, "/export:is_scroll_direction_reversed=uiohook_real.is_scroll_direction_reversed")
#pragma comment(linker, "/export:keyboard_hook_event_proc=uiohook_real.keyboard_hook_event_proc")
#pragma comment(linker, "/export:keycode_to_uiocode=uiohook_real.keycode_to_uiocode")
#pragma comment(linker, "/export:keycode_to_unicode=uiohook_real.keycode_to_unicode")
#pragma comment(linker, "/export:logger=uiohook_real.logger")
#pragma comment(linker, "/export:mouse_hook_event_proc=uiohook_real.mouse_hook_event_proc")
#pragma comment(linker, "/export:run=uiohook_real.run")
#pragma comment(linker, "/export:set_always_enumerate_displays=uiohook_real.set_always_enumerate_displays")
#pragma comment(linker, "/export:set_modifier_mask=uiohook_real.set_modifier_mask")
#pragma comment(linker, "/export:uiocode_to_keycode=uiohook_real.uiocode_to_keycode")
#pragma comment(linker, "/export:unregister_running_hooks=uiohook_real.unregister_running_hooks")
#pragma comment(linker, "/export:unset_modifier_mask=uiohook_real.unset_modifier_mask")

/* ---- Logging ---- */

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

static void LogFmt(const char* fmt, ...) {
    char buf[2048];
    va_list args;
    va_start(args, fmt);
    vsnprintf(buf, sizeof(buf), fmt, args);
    va_end(args);
    WriteLog(buf);
}

/* ================================================================
 * SECTION 1: IPC Hook (JS injection into DotNetBrowser frames)
 * ================================================================ */

/* ---- ipc64.dll function types ---- */

typedef int (*FnSendData)(void* conn, void* data, int size);
typedef void* (*FnOpenConnection)(const char* id);
typedef void (*FnCloseConnection)(void* conn);
typedef int (*FnGetConnectionType)(void* conn);

/* ---- IPC traffic capture ---- */

static CRITICAL_SECTION g_hookLock;
static BYTE g_origSendBytes[16];
static FnSendData g_realSendData = NULL;
static FnOpenConnection g_realOpenConn = NULL;
static FnCloseConnection g_realCloseConn = NULL;
static volatile LONG g_msgCount = 0;

/* Captured frame UUID and connection handle from ExecuteJavaScript traffic */
static char g_frameUuid[64] = {0};
static char g_browserUuid[64] = {0};
static void* g_rootConn = NULL;
static volatile LONG g_uuidCaptured = 0;

static int IsUuidChar(char c) {
    return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
}

static int FindUuid(const unsigned char* data, int size, int startPos, char* out) {
    for (int i = startPos; i < size - 36; i++) {
        if (IsUuidChar(data[i]) && IsUuidChar(data[i+1]) &&
            data[i+8] == '-' && data[i+13] == '-' &&
            data[i+18] == '-' && data[i+23] == '-') {
            int valid = 1;
            for (int j = 0; j < 36; j++) {
                if (j == 8 || j == 13 || j == 18 || j == 23) {
                    if (data[i+j] != '-') { valid = 0; break; }
                } else {
                    if (!IsUuidChar(data[i+j])) { valid = 0; break; }
                }
            }
            if (valid) {
                memcpy(out, data + i, 36);
                out[36] = 0;
                return i;
            }
        }
    }
    return -1;
}

/* IPC traffic log file */
static FILE* g_ipcLog = NULL;

static void LogIpc(const char* prefix, void* data, int size) {
    if (!g_ipcLog) {
        g_ipcLog = fopen("C:\\Users\\bash\\IPC_TRAFFIC.txt", "a");
    }
    if (!g_ipcLog) return;

    SYSTEMTIME st;
    GetLocalTime(&st);
    fprintf(g_ipcLog, "\n[%02d:%02d:%02d.%03d] %s size=%d\n",
            st.wHour, st.wMinute, st.wSecond, st.wMilliseconds, prefix, size);

    if (data && size > 0) {
        unsigned char* p = (unsigned char*)data;
        int dump = size > 512 ? 512 : size;
        fprintf(g_ipcLog, "HEX: ");
        for (int i = 0; i < dump; i++) {
            fprintf(g_ipcLog, "%02X ", p[i]);
            if ((i + 1) % 32 == 0) fprintf(g_ipcLog, "\n     ");
        }
        fprintf(g_ipcLog, "\nTXT: ");
        for (int i = 0; i < dump; i++) {
            fprintf(g_ipcLog, "%c", (p[i] >= 32 && p[i] < 127) ? p[i] : '.');
        }
        fprintf(g_ipcLog, "\n");
    }
    fflush(g_ipcLog);
}

/* ---- Inline hook helpers ---- */

static void PatchJmp(void* target, void* hook) {
    DWORD oldProt;
    VirtualProtect(target, 16, PAGE_EXECUTE_READWRITE, &oldProt);
    unsigned char jmp[14];
    jmp[0] = 0x48; jmp[1] = 0xB8;
    *(UINT_PTR*)(jmp + 2) = (UINT_PTR)hook;
    jmp[10] = 0xFF; jmp[11] = 0xE0;
    jmp[12] = 0x90; jmp[13] = 0x90;
    memcpy(target, jmp, 14);
    FlushInstructionCache(GetCurrentProcess(), target, 14);
    VirtualProtect(target, 16, oldProt, &oldProt);
}

static void RestoreBytes(void* target, BYTE* saved) {
    DWORD oldProt;
    VirtualProtect(target, 16, PAGE_EXECUTE_READWRITE, &oldProt);
    memcpy(target, saved, 14);
    FlushInstructionCache(GetCurrentProcess(), target, 14);
    VirtualProtect(target, 16, oldProt, &oldProt);
}

/* ---- File reading for inline injection ---- */

#define UPROOTED_DIR "C:\\Users\\bash\\claude\\Root.Dev\\uprooted\\dist\\"
#define MAX_FILE_SIZE (64 * 1024)

static char* g_preloadJs = NULL;
static int   g_preloadJsLen = 0;
static char* g_cssContent = NULL;
static int   g_cssLen = 0;

static char* ReadFileToMem(const char* path, int* outLen) {
    FILE* f = fopen(path, "rb");
    if (!f) return NULL;
    fseek(f, 0, SEEK_END);
    long sz = ftell(f);
    fseek(f, 0, SEEK_SET);
    if (sz <= 0 || sz > MAX_FILE_SIZE) { fclose(f); return NULL; }
    char* buf = (char*)malloc(sz + 1);
    if (!buf) { fclose(f); return NULL; }
    fread(buf, 1, sz, f);
    buf[sz] = 0;
    fclose(f);
    *outLen = (int)sz;
    return buf;
}

static char* EscapeForJs(const char* src, int srcLen, int* outLen) {
    char* buf = (char*)malloc(srcLen * 2 + 1);
    if (!buf) return NULL;
    int j = 0;
    for (int i = 0; i < srcLen; i++) {
        char c = src[i];
        if (c == '\\')      { buf[j++] = '\\'; buf[j++] = '\\'; }
        else if (c == '\'') { buf[j++] = '\\'; buf[j++] = '\''; }
        else if (c == '\n') { buf[j++] = '\\'; buf[j++] = 'n'; }
        else if (c == '\r') { /* skip */ }
        else if (c == '`')  { buf[j++] = '\\'; buf[j++] = '`'; }
        else if (c == 0)    { /* skip nulls */ }
        else                { buf[j++] = c; }
    }
    buf[j] = 0;
    *outLen = j;
    return buf;
}

static void LoadUprootedFiles(void) {
    WriteLog("Loading uprooted-preload.js...");
    g_preloadJs = ReadFileToMem(UPROOTED_DIR "uprooted-preload.js", &g_preloadJsLen);
    LogFmt("  preload.js: %s (%d bytes)", g_preloadJs ? "OK" : "FAIL", g_preloadJsLen);

    WriteLog("Loading uprooted.css...");
    g_cssContent = ReadFileToMem(UPROOTED_DIR "uprooted.css", &g_cssLen);
    LogFmt("  uprooted.css: %s (%d bytes)", g_cssContent ? "OK" : "FAIL", g_cssLen);
}

/* ---- Hooked IPC functions ---- */

static volatile LONG g_injectionDone = 0;
static volatile LONG g_execJsCount = 0;

/* Forward declaration */
static int BuildExecuteJsMsg(unsigned char* out, int maxLen,
                              int requestId, const char* frameUuid,
                              const char* jsCode);

static int __cdecl HookedSendData(void* conn, void* data, int size) {
    LONG n = InterlockedIncrement(&g_msgCount);

    if (n <= 100) {
        char hdr[128];
        snprintf(hdr, sizeof(hdr), "send_data #%ld conn=%p", n, conn);
        LogIpc(hdr, data, size);
    }

    /* Detect ExecuteJavaScript messages */
    int isExecJs = 0;
    char frameUuid[64] = {0};
    if (data && size > 20) {
        unsigned char* p = (unsigned char*)data;
        for (int i = 0; i < size - 17; i++) {
            if (memcmp(p + i, "ExecuteJavaScript", 17) == 0) {
                isExecJs = 1;
                FindUuid(p, size, i + 17, frameUuid);
                if (!g_uuidCaptured && frameUuid[0]) {
                    memcpy(g_frameUuid, frameUuid, 37);
                    g_rootConn = conn;
                    LogFmt("*** Captured frame UUID: %s (conn=%p)", g_frameUuid, conn);
                    InterlockedExchange(&g_uuidCaptured, 1);
                }
                break;
            }
        }
    }

    /* Replace second ExecuteJavaScript with Uprooted payload */
    void* actualData = data;
    int actualSize = size;
    static unsigned char* replacementMsg = NULL;
    int replacementLen = 0;

    if (isExecJs && frameUuid[0]) {
        LONG ejsN = InterlockedIncrement(&g_execJsCount);
        LogFmt("*** ExecuteJavaScript #%ld detected (conn=%p)", ejsN, conn);

        if (ejsN == 2 && !g_injectionDone) {
            InterlockedExchange(&g_injectionDone, 1);

            unsigned char* p = (unsigned char*)data;
            int reqId = 0;
            if (size > 5 && p[0] == 0x0A) {
                int metaLen = p[1];
                if (metaLen >= 2 && p[2] == 0x08) {
                    int shift = 0;
                    for (int i = 3; i < 3 + metaLen - 1 && i < size; i++) {
                        reqId |= (p[i] & 0x7F) << shift;
                        if (!(p[i] & 0x80)) break;
                        shift += 7;
                    }
                }
            }

            LogFmt("*** REPLACING ExecJS #2 (reqId=%d) with inline Uprooted", reqId);

            int escapedCssLen = 0;
            char* escapedCss = g_cssContent ?
                EscapeForJs(g_cssContent, g_cssLen, &escapedCssLen) : NULL;

            int jsCapacity = 1024 + escapedCssLen + g_preloadJsLen;
            char* jsPayload = (char*)malloc(jsCapacity);
            int jsLen = 0;

            if (jsPayload) {
                jsLen = snprintf(jsPayload, jsCapacity,
                    "window.__UPROOTED_SETTINGS__="
                    "{enabled:true,plugins:{},customCss:''};"
                    "try{"
                    "var _us=document.createElement('style');"
                    "_us.textContent='%s';"
                    "(document.head||document.documentElement).appendChild(_us);"
                    "}catch(e){}"
                    "try{",
                    escapedCss ? escapedCss : "");

                if (g_preloadJs && jsLen + g_preloadJsLen + 64 < jsCapacity) {
                    memcpy(jsPayload + jsLen, g_preloadJs, g_preloadJsLen);
                    jsLen += g_preloadJsLen;
                }

                jsLen += snprintf(jsPayload + jsLen, jsCapacity - jsLen,
                    "}catch(e){"
                    "var _ed=document.createElement('div');"
                    "_ed.style.cssText='position:fixed;top:0;left:0;right:0;"
                    "padding:12px;background:#dc2626;color:#fff;z-index:999999;"
                    "font:14px monospace;white-space:pre-wrap';"
                    "_ed.textContent='Uprooted error: '+e.message+'\\n'+e.stack;"
                    "document.body.appendChild(_ed);"
                    "}"
                    "fetch('http://127.0.0.1:18999/injected?js='+%s+'&css='+%s)"
                    ".catch(function(){});",
                    g_preloadJs ? "encodeURIComponent('ok')" : "encodeURIComponent('nojs')",
                    g_cssContent ? "encodeURIComponent('ok')" : "encodeURIComponent('nocss')");

                LogFmt("JS payload: %d bytes (css=%d, preload=%d)",
                       jsLen, escapedCssLen, g_preloadJsLen);

                replacementMsg = (unsigned char*)malloc(256 * 1024);
                if (replacementMsg) {
                    replacementLen = BuildExecuteJsMsg(
                        replacementMsg, 256 * 1024, reqId, frameUuid, jsPayload);
                    LogFmt("Replacement msg: %d bytes (was %d)", replacementLen, size);
                    actualData = replacementMsg;
                    actualSize = replacementLen;
                }
                free(jsPayload);
            }
            if (escapedCss) free(escapedCss);
        }
    }

    /* Call original send_data */
    EnterCriticalSection(&g_hookLock);
    RestoreBytes((void*)g_realSendData, g_origSendBytes);
    int result = g_realSendData(conn, actualData, actualSize);
    PatchJmp((void*)g_realSendData, HookedSendData);
    LeaveCriticalSection(&g_hookLock);

    if (n <= 50) {
        LogFmt("  ret #%ld = %d (0x%08X)%s", n, result, result,
               isExecJs ? " [ExecJS]" : "");
    }

    return result;
}

/* ---- Protobuf message construction ---- */

static int WriteVarint(unsigned char* buf, uint64_t val) {
    int i = 0;
    while (val >= 0x80) {
        buf[i++] = (unsigned char)(val | 0x80);
        val >>= 7;
    }
    buf[i++] = (unsigned char)val;
    return i;
}

static int WriteField(unsigned char* buf, int fieldNum, const void* data, int dataLen) {
    int pos = 0;
    pos += WriteVarint(buf + pos, (fieldNum << 3) | 2);
    pos += WriteVarint(buf + pos, dataLen);
    memcpy(buf + pos, data, dataLen);
    pos += dataLen;
    return pos;
}

static int WriteVarintField(unsigned char* buf, int fieldNum, uint64_t val) {
    int pos = 0;
    pos += WriteVarint(buf + pos, (fieldNum << 3) | 0);
    pos += WriteVarint(buf + pos, val);
    return pos;
}

static int BuildExecuteJsMsg(unsigned char* out, int maxLen,
                              int requestId, const char* frameUuid,
                              const char* jsCode) {
    int pos = 0;
    int jsLen = (int)strlen(jsCode);
    int bufSize = jsLen + 1024;

    unsigned char frameId[128];
    int frameIdLen = WriteField(frameId, 1, frameUuid, (int)strlen(frameUuid));

    unsigned char* inner = (unsigned char*)malloc(bufSize);
    if (!inner) return 0;
    int innerLen = 0;
    innerLen += WriteField(inner + innerLen, 1, frameId, frameIdLen);
    innerLen += WriteField(inner + innerLen, 2, jsCode, jsLen);

    unsigned char* payload = (unsigned char*)malloc(bufSize);
    if (!payload) { free(inner); return 0; }
    int payloadLen = WriteField(payload, 1, inner, innerLen);

    unsigned char meta[16];
    int metaLen = WriteVarintField(meta, 1, requestId);

    pos += WriteField(out + pos, 1, meta, metaLen);
    pos += WriteField(out + pos, 2, "rpc.Frame", 9);
    pos += WriteField(out + pos, 3, "ExecuteJavaScript", 17);
    pos += WriteField(out + pos, 5, payload, payloadLen);

    free(inner);
    free(payload);
    return pos;
}

/* ---- IPC hook installation ---- */

static void InstallIpcHooks(void) {
    HMODULE hIpc = GetModuleHandleA("ipc64.dll");
    if (!hIpc) {
        hIpc = LoadLibraryA(
            "C:\\Users\\bash\\AppData\\Local\\Root Communications\\Root\\"
            "profile\\default\\DotNetBrowser\\WindowsX64\\ipc64.dll");
    }
    if (!hIpc) {
        WriteLog("ipc64.dll not found!");
        return;
    }

    g_realSendData = (FnSendData)GetProcAddress(hIpc, "send_data");
    g_realOpenConn = (FnOpenConnection)GetProcAddress(hIpc, "open_connection");
    g_realCloseConn = (FnCloseConnection)GetProcAddress(hIpc, "close_connection");

    LogFmt("ipc64: send=%p open=%p close=%p",
           (void*)g_realSendData, (void*)g_realOpenConn, (void*)g_realCloseConn);

    if (!g_realSendData || !g_realOpenConn || !g_realCloseConn) {
        WriteLog("Missing IPC exports!");
        return;
    }

    InitializeCriticalSection(&g_hookLock);

    memcpy(g_origSendBytes, (void*)g_realSendData, 14);
    PatchJmp((void*)g_realSendData, HookedSendData);

    WriteLog("send_data hook installed!");
}

/* ---- Thread 1: IPC injection ---- */

static DWORD WINAPI IpcInjectorThread(LPVOID param) {
    DWORD pid = GetCurrentProcessId();
    LogFmt("=== IPC injector starting (PID %lu) ===", pid);

    WriteLog("Preloading ipc64.dll...");
    HMODULE hIpc = LoadLibraryA(
        "C:\\Users\\bash\\AppData\\Local\\Root Communications\\Root\\"
        "profile\\default\\DotNetBrowser\\WindowsX64\\ipc64.dll");
    if (hIpc) {
        LogFmt("ipc64.dll preloaded at %p", (void*)hIpc);
    } else {
        LogFmt("ipc64.dll preload failed: %lu, waiting...", GetLastError());
        for (int i = 0; i < 120; i++) {
            Sleep(100);
            if (GetModuleHandleA("ipc64.dll")) {
                LogFmt("ipc64.dll appeared after %dms", (i+1)*100);
                break;
            }
        }
    }

    LoadUprootedFiles();
    InstallIpcHooks();

    WriteLog("IPC hooks installed. Waiting for ExecuteJavaScript...");

    for (int i = 0; i < 120; i++) {
        Sleep(500);
        if (g_injectionDone) {
            LogFmt("JS injection done after %dms", (i+1)*500);
            break;
        }
    }

    if (!g_injectionDone) {
        WriteLog("WARNING: No ExecuteJavaScript seen after 60s!");
    }

    return 0;
}

/* ================================================================
 * SECTION 2: CLR Hosting (managed assembly loading via hostfxr)
 * ================================================================ */

/* hostfxr function signatures */
typedef int (*hostfxr_initialize_for_runtime_config_fn)(
    const wchar_t* runtime_config_path,
    const void* parameters,
    void** host_context_handle);

typedef int (*hostfxr_get_runtime_delegate_fn)(
    void* host_context_handle,
    int type,
    void** delegate);

typedef int (*hostfxr_close_fn)(
    void* host_context_handle);

/* load_assembly_and_get_function_pointer delegate type */
typedef int (*load_assembly_and_get_function_pointer_fn)(
    const wchar_t* assembly_path,
    const wchar_t* type_name,
    const wchar_t* method_name,
    const wchar_t* delegate_type_name,
    void* reserved,
    void** delegate);

/* Our managed entry point: int Initialize(IntPtr args, int sizeBytes) */
typedef int (*managed_entry_point_fn)(void* args, int size);

/* hostfxr_delegate_type enum value */
#define hdt_load_assembly_and_get_function_pointer 5

/* Paths for CLR hosting */
#define HOSTFXR_PATH L"C:\\Program Files\\dotnet\\host\\fxr\\10.0.3\\hostfxr.dll"
#define HOOK_DLL_PATH L"C:\\Users\\bash\\claude\\Root.Dev\\uprooted\\hook\\bin\\Release\\net10.0\\UprootedHook.dll"
#define HOOK_RUNTIMECONFIG L"C:\\Users\\bash\\claude\\Root.Dev\\uprooted\\hook\\UprootedHook.runtimeconfig.json"

/*
 * Find hostfxr.dll. Tries multiple locations:
 * 1. Already loaded as module
 * 2. Known SDK path (10.0.3)
 * 3. Scan fxr directory for any version
 */
static HMODULE FindHostfxr(void) {
    HMODULE h;

    /* Check if already loaded */
    h = GetModuleHandleA("hostfxr");
    if (h) {
        LogFmt("CLR: hostfxr already loaded at %p", (void*)h);
        return h;
    }

    /* Try known path */
    h = LoadLibraryW(HOSTFXR_PATH);
    if (h) {
        LogFmt("CLR: Loaded hostfxr from SDK at %p", (void*)h);
        return h;
    }
    LogFmt("CLR: hostfxr not at SDK path, err=%lu", GetLastError());

    /* Try scanning fxr directory */
    WIN32_FIND_DATAW fd;
    HANDLE hFind = FindFirstFileW(L"C:\\Program Files\\dotnet\\host\\fxr\\*", &fd);
    if (hFind != INVALID_HANDLE_VALUE) {
        do {
            if (fd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) {
                if (fd.cFileName[0] != L'.') {
                    wchar_t path[MAX_PATH];
                    _snwprintf(path, MAX_PATH,
                        L"C:\\Program Files\\dotnet\\host\\fxr\\%s\\hostfxr.dll",
                        fd.cFileName);
                    h = LoadLibraryW(path);
                    if (h) {
                        LogFmt("CLR: Loaded hostfxr from %ls", path);
                        FindClose(hFind);
                        return h;
                    }
                }
            }
        } while (FindNextFileW(hFind, &fd));
        FindClose(hFind);
    }

    WriteLog("CLR: hostfxr.dll not found anywhere!");
    return NULL;
}

/*
 * Load UprootedHook.dll via hostfxr component hosting API.
 * Uses hostfxr_initialize_for_runtime_config to attach to the
 * existing .NET runtime in Root.exe, then loads our assembly.
 */
static void LoadManagedHook(void) {
    WriteLog("CLR: === Starting managed hook loading ===");

    /* Step 1: Find and load hostfxr */
    HMODULE hHostfxr = FindHostfxr();
    if (!hHostfxr) return;

    /* Step 2: Resolve hostfxr functions */
    hostfxr_initialize_for_runtime_config_fn init_fn =
        (hostfxr_initialize_for_runtime_config_fn)
        GetProcAddress(hHostfxr, "hostfxr_initialize_for_runtime_config");
    hostfxr_get_runtime_delegate_fn get_delegate_fn =
        (hostfxr_get_runtime_delegate_fn)
        GetProcAddress(hHostfxr, "hostfxr_get_runtime_delegate");
    hostfxr_close_fn close_fn =
        (hostfxr_close_fn)
        GetProcAddress(hHostfxr, "hostfxr_close");

    LogFmt("CLR: hostfxr exports: init=%p delegate=%p close=%p",
           (void*)init_fn, (void*)get_delegate_fn, (void*)close_fn);

    if (!init_fn || !get_delegate_fn || !close_fn) {
        WriteLog("CLR: Missing hostfxr exports!");
        return;
    }

    /* Step 3: Initialize for our runtime config.
     * Since Root.exe already loaded .NET 10.0.3, this should return
     * Success_HostAlreadyInitialized (0x00000001) and reuse the runtime. */
    void* ctx = NULL;
    int rc = init_fn(HOOK_RUNTIMECONFIG, NULL, &ctx);
    LogFmt("CLR: hostfxr_initialize rc=0x%08X ctx=%p", rc, ctx);

    /* rc=0: Success (new runtime), rc=1: HostAlreadyInitialized (reusing) */
    if (rc < 0) {
        LogFmt("CLR: hostfxr_initialize FAILED: 0x%08X", rc);
        if (ctx) close_fn(ctx);
        return;
    }
    if (rc == 1) {
        WriteLog("CLR: Reusing existing .NET runtime (HostAlreadyInitialized)");
    }

    /* Step 4: Get the load_assembly_and_get_function_pointer delegate */
    load_assembly_and_get_function_pointer_fn load_fn = NULL;
    rc = get_delegate_fn(ctx, hdt_load_assembly_and_get_function_pointer, (void**)&load_fn);
    LogFmt("CLR: get_runtime_delegate rc=0x%08X load_fn=%p", rc, (void*)load_fn);

    if (rc < 0 || !load_fn) {
        LogFmt("CLR: Failed to get load delegate: 0x%08X", rc);
        close_fn(ctx);
        return;
    }

    /* Step 5: Load our managed assembly and get the entry point.
     * delegate_type_name=NULL means the method should have signature:
     *   public static int Initialize(IntPtr args, int sizeBytes) */
    managed_entry_point_fn managed_init = NULL;
    rc = load_fn(
        HOOK_DLL_PATH,
        L"Uprooted.NativeEntry, UprootedHook",
        L"Initialize",
        NULL,   /* default delegate type */
        NULL,   /* reserved */
        (void**)&managed_init);
    LogFmt("CLR: load_assembly rc=0x%08X fn=%p", rc, (void*)managed_init);

    if (rc < 0 || !managed_init) {
        LogFmt("CLR: Failed to load managed assembly: 0x%08X", rc);
        close_fn(ctx);
        return;
    }

    /* Step 6: Call our managed entry point.
     * This calls StartupHook.Initialize() which spawns the Avalonia
     * injector thread. Returns 0 on success. */
    WriteLog("CLR: Calling managed NativeEntry.Initialize()...");
    rc = managed_init(NULL, 0);
    LogFmt("CLR: managed Initialize returned %d", rc);

    if (rc == 0) {
        WriteLog("CLR: === Managed hook loaded successfully! ===");
    } else {
        LogFmt("CLR: Managed hook returned error: %d", rc);
    }

    close_fn(ctx);
}

/* ---- Thread 2: CLR hosting ---- */

static DWORD WINAPI ClrHostThread(LPVOID param) {
    DWORD pid = GetCurrentProcessId();
    LogFmt("=== CLR host thread starting (PID %lu) ===", pid);

    /*
     * Wait for the .NET runtime to fully initialize.
     * Root.exe IS a .NET app, so the CLR starts early, but we need
     * to wait until it's fully ready before calling hostfxr.
     * Poll for coreclr.dll or well-known .NET modules.
     */
    WriteLog("CLR: Waiting for .NET runtime to initialize...");

    int clrReady = 0;
    for (int i = 0; i < 120; i++) {  /* up to 60 seconds */
        Sleep(500);

        /* Check for coreclr.dll (loaded separately in some configs) */
        if (GetModuleHandleA("coreclr")) {
            LogFmt("CLR: coreclr.dll found after %dms", (i+1)*500);
            clrReady = 1;
            break;
        }

        /* Check for System.Private.CoreLib (loaded by all .NET apps) */
        if (GetModuleHandleA("System.Private.CoreLib")) {
            LogFmt("CLR: System.Private.CoreLib found after %dms", (i+1)*500);
            clrReady = 1;
            break;
        }

        /* Check for clrjit.dll (JIT compiler, always loaded) */
        if (GetModuleHandleA("clrjit")) {
            LogFmt("CLR: clrjit.dll found after %dms", (i+1)*500);
            clrReady = 1;
            break;
        }

        /* After 5 seconds, try anyway - the CLR should be ready */
        if (i >= 10) {
            LogFmt("CLR: 5s elapsed, proceeding without module detection");
            clrReady = 1;
            break;
        }
    }

    if (!clrReady) {
        WriteLog("CLR: Runtime never detected, aborting");
        return 1;
    }

    /* Give the runtime a moment to finish initialization */
    Sleep(1000);

    /* Load our managed assembly */
    LoadManagedHook();

    return 0;
}

/* ================================================================
 * SECTION 3: DLL Entry Point
 * ================================================================ */

BOOL APIENTRY DllMain(HMODULE hModule, DWORD reason, LPVOID reserved) {
    if (reason == DLL_PROCESS_ATTACH) {
        DisableThreadLibraryCalls(hModule);
        WriteLog("=== uiohook proxy DllMain (dual injection) ===");

        /*
         * Set CORECLR_PROFILER env vars BEFORE the CLR initializes.
         * uiohook.dll is loaded via PE import table, so DllMain fires
         * before Root.exe's entry point (which initializes the CLR).
         * The profiler loads into Root's actual CLR, not a separate one.
         */
        /* Try both CORECLR_ and COMPlus_ prefixes for maximum compatibility */
        SetEnvironmentVariableA("CORECLR_ENABLE_PROFILING", "1");
        SetEnvironmentVariableA("CORECLR_PROFILER",
            "{D1A6F5A0-1234-4567-89AB-CDEF01234567}");
        SetEnvironmentVariableA("CORECLR_PROFILER_PATH",
            "C:\\Users\\bash\\claude\\Root.Dev\\uprooted\\tools\\uprooted_profiler.dll");

        /* COMPlus_ prefix (used by older CLR versions, still checked) */
        SetEnvironmentVariableA("COMPlus_EnableDiagnostics", "1");

        /* Verify env vars were set */
        {
            char buf[512];
            GetEnvironmentVariableA("CORECLR_ENABLE_PROFILING", buf, sizeof(buf));
            LogFmt("ENV CORECLR_ENABLE_PROFILING=%s", buf);
            GetEnvironmentVariableA("CORECLR_PROFILER_PATH", buf, sizeof(buf));
            LogFmt("ENV CORECLR_PROFILER_PATH=%s", buf);

            /* Verify profiler DLL exists */
            DWORD attr = GetFileAttributesA(
                "C:\\Users\\bash\\claude\\Root.Dev\\uprooted\\tools\\uprooted_profiler.dll");
            LogFmt("Profiler DLL exists: %s (attr=0x%08X)",
                   attr != INVALID_FILE_ATTRIBUTES ? "YES" : "NO", attr);
        }
        WriteLog("CORECLR_PROFILER env vars set");

        /* Thread 1: IPC hooks for JS injection into DotNetBrowser */
        CreateThread(NULL, 0, IpcInjectorThread, NULL, 0, NULL);

        /* Thread 2: CLR hosting for managed Avalonia injection (backup) */
        /* Disabled - using profiler approach instead */
        /* CreateThread(NULL, 0, ClrHostThread, NULL, 0, NULL); */
    }
    return TRUE;
}
