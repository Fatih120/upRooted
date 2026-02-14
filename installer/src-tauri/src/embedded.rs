/// Embedded binary artifacts for deployment.
///
/// These files are compiled into the installer binary via `include_bytes!()`.
/// The build pipeline (`scripts/build_installer.ps1`) stages real builds into
/// `installer/src-tauri/artifacts/` before `cargo tauri build`.

pub const PROFILER_DLL: &[u8] = include_bytes!("../artifacts/uprooted_profiler.dll");
pub const HOOK_DLL: &[u8] = include_bytes!("../artifacts/UprootedHook.dll");
pub const HOOK_DEPS_JSON: &[u8] = include_bytes!("../artifacts/UprootedHook.deps.json");
pub const PRELOAD_JS: &[u8] = include_bytes!("../artifacts/uprooted-preload.js");
pub const THEME_CSS: &[u8] = include_bytes!("../artifacts/uprooted.css");
