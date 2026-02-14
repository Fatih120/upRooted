use serde::Serialize;
use std::fs;
use std::path::PathBuf;

const INJECTION_MARKER: &str = "<!-- uprooted -->";

#[derive(Serialize, Clone)]
pub struct DetectionResult {
    pub root_found: bool,
    pub root_path: String,
    pub profile_dir: String,
    pub html_files: Vec<String>,
    pub is_installed: bool,
}

pub fn get_profile_dir() -> PathBuf {
    let local_app_data = std::env::var("LOCALAPPDATA").unwrap_or_default();
    PathBuf::from(local_app_data)
        .join("Root Communications")
        .join("Root")
        .join("profile")
        .join("default")
}

pub fn get_root_exe_path() -> PathBuf {
    let local_app_data = std::env::var("LOCALAPPDATA").unwrap_or_default();
    PathBuf::from(local_app_data)
        .join("Root")
        .join("current")
        .join("Root.exe")
}

pub fn find_target_html_files() -> Vec<PathBuf> {
    let profile = get_profile_dir();
    let mut targets = Vec::new();

    // WebRtcBundle/index.html
    let webrtc_index = profile.join("WebRtcBundle").join("index.html");
    if webrtc_index.exists() {
        targets.push(webrtc_index);
    }

    // RootApps/*/index.html
    let root_apps_dir = profile.join("RootApps");
    if root_apps_dir.exists() {
        if let Ok(entries) = fs::read_dir(&root_apps_dir) {
            for entry in entries.flatten() {
                if entry.file_type().map(|t| t.is_dir()).unwrap_or(false) {
                    let app_index = entry.path().join("index.html");
                    if app_index.exists() {
                        targets.push(app_index);
                    }
                }
            }
        }
    }

    targets
}

pub fn check_is_installed(html_files: &[PathBuf]) -> bool {
    for file in html_files {
        if let Ok(content) = fs::read_to_string(file) {
            if content.contains(INJECTION_MARKER) {
                return true;
            }
        }
    }
    false
}

pub fn detect() -> DetectionResult {
    let root_exe = get_root_exe_path();
    let profile = get_profile_dir();
    let html_files = find_target_html_files();
    let is_installed = check_is_installed(&html_files);

    DetectionResult {
        root_found: root_exe.exists(),
        root_path: root_exe.to_string_lossy().to_string(),
        profile_dir: profile.to_string_lossy().to_string(),
        html_files: html_files
            .iter()
            .map(|p| p.to_string_lossy().to_string())
            .collect(),
        is_installed,
    }
}
