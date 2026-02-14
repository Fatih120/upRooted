use crate::embedded;
use serde::Serialize;
use std::fs;
use std::path::PathBuf;
use winreg::enums::*;
use winreg::RegKey;

const PROFILER_GUID: &str = "{D1A6F5A0-1234-4567-89AB-CDEF01234567}";

const ENV_VARS: &[&str] = &[
    "CORECLR_ENABLE_PROFILING",
    "CORECLR_PROFILER",
    "CORECLR_PROFILER_PATH",
    "DOTNET_ReadyToRun",
    "DOTNET_STARTUP_HOOKS",
];

#[derive(Serialize, Clone, Default)]
pub struct HookStatus {
    pub profiler_dll: bool,
    pub hook_dll: bool,
    pub hook_deps: bool,
    pub preload_js: bool,
    pub theme_css: bool,
    pub env_enable_profiling: bool,
    pub env_profiler_guid: bool,
    pub env_profiler_path: bool,
    pub env_ready_to_run: bool,
    /// True if all files are deployed
    pub files_ok: bool,
    /// True if all env vars are set correctly
    pub env_ok: bool,
}

/// Returns `%LOCALAPPDATA%\Root\uprooted\`
pub fn get_uprooted_dir() -> PathBuf {
    let local_app_data = std::env::var("LOCALAPPDATA").unwrap_or_default();
    PathBuf::from(local_app_data).join("Root").join("uprooted")
}

/// Deploy all embedded files to the install directory.
pub fn deploy_files() -> Result<(), String> {
    let dir = get_uprooted_dir();
    fs::create_dir_all(&dir).map_err(|e| format!("Failed to create {}: {}", dir.display(), e))?;

    let files: &[(&str, &[u8])] = &[
        ("uprooted_profiler.dll", embedded::PROFILER_DLL),
        ("UprootedHook.dll", embedded::HOOK_DLL),
        ("UprootedHook.deps.json", embedded::HOOK_DEPS_JSON),
        ("uprooted-preload.js", embedded::PRELOAD_JS),
        ("uprooted.css", embedded::THEME_CSS),
    ];

    for (name, data) in files {
        let path = dir.join(name);
        fs::write(&path, data)
            .map_err(|e| format!("Failed to write {}: {}", path.display(), e))?;
    }

    Ok(())
}

/// Set CLR profiler environment variables (user-scoped) and broadcast WM_SETTINGCHANGE.
pub fn set_env_vars() -> Result<(), String> {
    let hkcu = RegKey::predef(HKEY_CURRENT_USER);
    let (env_key, _) = hkcu
        .create_subkey("Environment")
        .map_err(|e| format!("Failed to open HKCU\\Environment: {}", e))?;

    let profiler_path = get_uprooted_dir()
        .join("uprooted_profiler.dll")
        .to_string_lossy()
        .to_string();

    env_key
        .set_value("CORECLR_ENABLE_PROFILING", &"1")
        .map_err(|e| format!("Failed to set CORECLR_ENABLE_PROFILING: {}", e))?;
    env_key
        .set_value("CORECLR_PROFILER", &PROFILER_GUID)
        .map_err(|e| format!("Failed to set CORECLR_PROFILER: {}", e))?;
    env_key
        .set_value("CORECLR_PROFILER_PATH", &profiler_path)
        .map_err(|e| format!("Failed to set CORECLR_PROFILER_PATH: {}", e))?;
    env_key
        .set_value("DOTNET_ReadyToRun", &"0")
        .map_err(|e| format!("Failed to set DOTNET_ReadyToRun: {}", e))?;

    // Remove legacy startup hooks var if present
    let _ = env_key.delete_value("DOTNET_STARTUP_HOOKS");

    broadcast_env_change();
    Ok(())
}

/// Remove all Uprooted-related environment variables (user-scoped).
pub fn remove_env_vars() -> Result<(), String> {
    let hkcu = RegKey::predef(HKEY_CURRENT_USER);
    let env_key = hkcu
        .open_subkey_with_flags("Environment", KEY_WRITE)
        .map_err(|e| format!("Failed to open HKCU\\Environment: {}", e))?;

    for var in ENV_VARS {
        let _ = env_key.delete_value(var);
    }

    broadcast_env_change();
    Ok(())
}

/// Delete the uprooted install directory.
pub fn remove_files() -> Result<(), String> {
    let dir = get_uprooted_dir();
    if dir.exists() {
        fs::remove_dir_all(&dir)
            .map_err(|e| format!("Failed to remove {}: {}", dir.display(), e))?;
    }
    Ok(())
}

/// Check per-file and per-env-var status.
pub fn check_hook_status() -> HookStatus {
    let dir = get_uprooted_dir();

    let profiler_dll = dir.join("uprooted_profiler.dll").exists();
    let hook_dll = dir.join("UprootedHook.dll").exists();
    let hook_deps = dir.join("UprootedHook.deps.json").exists();
    let preload_js = dir.join("uprooted-preload.js").exists();
    let theme_css = dir.join("uprooted.css").exists();

    let (env_enable, env_guid, env_path, env_r2r) = check_env_vars();

    let files_ok = profiler_dll && hook_dll && hook_deps && preload_js && theme_css;
    let env_ok = env_enable && env_guid && env_path;

    HookStatus {
        profiler_dll,
        hook_dll,
        hook_deps,
        preload_js,
        theme_css,
        env_enable_profiling: env_enable,
        env_profiler_guid: env_guid,
        env_profiler_path: env_path,
        env_ready_to_run: env_r2r,
        files_ok,
        env_ok,
    }
}

/// Read env var status from the registry (not process env, which may be stale).
fn check_env_vars() -> (bool, bool, bool, bool) {
    let hkcu = RegKey::predef(HKEY_CURRENT_USER);
    let env_key = match hkcu.open_subkey("Environment") {
        Ok(k) => k,
        Err(_) => return (false, false, false, false),
    };

    let enable: bool = env_key
        .get_value::<String, _>("CORECLR_ENABLE_PROFILING")
        .map(|v| v == "1")
        .unwrap_or(false);
    let guid: bool = env_key
        .get_value::<String, _>("CORECLR_PROFILER")
        .map(|v| v == PROFILER_GUID)
        .unwrap_or(false);
    let path: bool = env_key
        .get_value::<String, _>("CORECLR_PROFILER_PATH")
        .map(|v| !v.is_empty())
        .unwrap_or(false);
    let r2r: bool = env_key
        .get_value::<String, _>("DOTNET_ReadyToRun")
        .map(|v| v == "0")
        .unwrap_or(false);

    (enable, guid, path, r2r)
}

/// Broadcast WM_SETTINGCHANGE so other processes pick up env var changes.
fn broadcast_env_change() {
    #[cfg(windows)]
    unsafe {
        use std::ffi::OsStr;
        use std::os::windows::ffi::OsStrExt;

        let env: Vec<u16> = OsStr::new("Environment")
            .encode_wide()
            .chain(std::iter::once(0))
            .collect();

        // HWND_BROADCAST = 0xFFFF, WM_SETTINGCHANGE = 0x001A
        windows_sys::Win32::UI::WindowsAndMessaging::SendMessageTimeoutW(
            0xFFFF_usize as *mut std::ffi::c_void,
            0x001A,
            0,
            env.as_ptr() as isize,
            0x0002, // SMTO_ABORTIFHUNG
            5000,
            std::ptr::null_mut(),
        );
    }
}

/// Check if Root.exe is currently running.
pub fn check_root_running() -> bool {
    #[cfg(windows)]
    {
        !find_root_pids().is_empty()
    }
    #[cfg(not(windows))]
    false
}

/// Terminate all Root.exe processes. Returns the number of processes killed.
pub fn kill_root_processes() -> u32 {
    #[cfg(windows)]
    {
        use windows_sys::Win32::Foundation::CloseHandle;
        use windows_sys::Win32::System::Threading::{OpenProcess, TerminateProcess, PROCESS_TERMINATE};

        let pids = find_root_pids();
        let mut killed = 0u32;
        for pid in &pids {
            unsafe {
                let handle = OpenProcess(PROCESS_TERMINATE, 0, *pid);
                if !handle.is_null() {
                    if TerminateProcess(handle, 1) != 0 {
                        killed += 1;
                    }
                    CloseHandle(handle);
                }
            }
        }
        killed
    }
    #[cfg(not(windows))]
    0
}

/// Find all PIDs for Root.exe.
#[cfg(windows)]
fn find_root_pids() -> Vec<u32> {
    use std::mem::MaybeUninit;
    use windows_sys::Win32::Foundation::{CloseHandle, INVALID_HANDLE_VALUE};
    use windows_sys::Win32::System::Diagnostics::ToolHelp::*;

    let mut pids = Vec::new();
    unsafe {
        let snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
        if snapshot == INVALID_HANDLE_VALUE {
            return pids;
        }

        let mut entry: PROCESSENTRY32W = MaybeUninit::zeroed().assume_init();
        entry.dwSize = std::mem::size_of::<PROCESSENTRY32W>() as u32;

        if Process32FirstW(snapshot, &mut entry) != 0 {
            loop {
                let name_len = entry
                    .szExeFile
                    .iter()
                    .position(|&c| c == 0)
                    .unwrap_or(entry.szExeFile.len());
                let name = String::from_utf16_lossy(&entry.szExeFile[..name_len]);
                if name.eq_ignore_ascii_case("Root.exe") {
                    pids.push(entry.th32ProcessID);
                }
                if Process32NextW(snapshot, &mut entry) == 0 {
                    break;
                }
            }
        }
        CloseHandle(snapshot);
    }
    pids
}
