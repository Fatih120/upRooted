const { invoke } = (window as any).__TAURI__.core;

export interface DetectionResult {
  root_found: boolean;
  root_path: string;
  profile_dir: string;
  html_files: string[];
  is_installed: boolean;
}

export interface PatchResult {
  success: boolean;
  message: string;
  files_patched: string[];
}

export interface PreviewColors {
  background: string;
  text: string;
  accent: string;
  border: string;
}

export interface ThemeDefinition {
  name: string;
  display_name: string;
  description: string;
  author: string;
  variables: Record<string, string>;
  preview_colors: PreviewColors;
}

export interface PluginSettings {
  enabled: boolean;
  config: Record<string, unknown>;
}

export interface UprootedSettings {
  enabled: boolean;
  plugins: Record<string, PluginSettings>;
  customCss: string;
}

export async function detectRoot(): Promise<DetectionResult> {
  return invoke("detect_root");
}

export async function installUprooted(): Promise<PatchResult> {
  return invoke("install_uprooted");
}

export async function uninstallUprooted(): Promise<PatchResult> {
  return invoke("uninstall_uprooted");
}

export async function repairUprooted(): Promise<PatchResult> {
  return invoke("repair_uprooted");
}

export async function loadSettings(): Promise<UprootedSettings> {
  return invoke("load_settings");
}

export async function saveSettings(settings: UprootedSettings): Promise<void> {
  return invoke("save_settings", { settings });
}

export async function listThemes(): Promise<ThemeDefinition[]> {
  return invoke("list_themes");
}

export async function applyTheme(name: string): Promise<void> {
  return invoke("apply_theme", { name });
}

export async function getUprootedVersion(): Promise<string> {
  return invoke("get_uprooted_version");
}

export async function openProfileDir(): Promise<void> {
  return invoke("open_profile_dir");
}
