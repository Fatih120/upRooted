// Prevents additional console window on Windows in release
#![cfg_attr(not(debug_assertions), windows_subsystem = "windows")]

mod detection;
mod patcher;
mod settings;
mod themes;

use detection::DetectionResult;
use patcher::PatchResult;
use settings::UprootedSettings;
use themes::ThemeDefinition;

#[tauri::command]
fn detect_root() -> DetectionResult {
    detection::detect()
}

#[tauri::command]
fn install_uprooted() -> PatchResult {
    patcher::install()
}

#[tauri::command]
fn uninstall_uprooted() -> PatchResult {
    patcher::uninstall()
}

#[tauri::command]
fn repair_uprooted() -> PatchResult {
    patcher::repair()
}

#[tauri::command]
fn load_settings() -> UprootedSettings {
    settings::load_settings()
}

#[tauri::command]
fn save_settings(settings: UprootedSettings) -> Result<(), String> {
    settings::save_settings(&settings)
}

#[tauri::command]
fn list_themes() -> Vec<ThemeDefinition> {
    themes::get_builtin_themes()
}

#[tauri::command]
fn apply_theme(name: String) -> Result<(), String> {
    let mut s = settings::load_settings();
    let theme_settings = s.plugins.entry("themes".to_string()).or_insert_with(|| {
        settings::PluginSettings {
            enabled: true,
            config: std::collections::HashMap::new(),
        }
    });
    theme_settings
        .config
        .insert("theme".to_string(), serde_json::Value::String(name));
    settings::save_settings(&s)
}

#[tauri::command]
fn get_uprooted_version() -> String {
    env!("CARGO_PKG_VERSION").to_string()
}

#[tauri::command]
fn open_profile_dir() -> Result<(), String> {
    let profile = detection::get_profile_dir();
    if profile.exists() {
        opener::open(profile).map_err(|e| e.to_string())
    } else {
        Err("Profile directory does not exist.".to_string())
    }
}

fn main() {
    tauri::Builder::default()
        .plugin(tauri_plugin_shell::init())
        .invoke_handler(tauri::generate_handler![
            detect_root,
            install_uprooted,
            uninstall_uprooted,
            repair_uprooted,
            load_settings,
            save_settings,
            list_themes,
            apply_theme,
            get_uprooted_version,
            open_profile_dir,
        ])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
