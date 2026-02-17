use crate::{detection, hook, patcher};
use std::fs;

// ANSI color codes
const GREEN: &str = "\x1b[32m";
const RED: &str = "\x1b[31m";
const YELLOW: &str = "\x1b[33m";
const CYAN: &str = "\x1b[36m";
const BOLD: &str = "\x1b[1m";
const DIM: &str = "\x1b[2m";
const RESET: &str = "\x1b[0m";

fn ok(msg: &str) {
    println!("  {GREEN}\u{2713}{RESET} {msg}");
}

fn fail(msg: &str) {
    println!("  {RED}\u{2717}{RESET} {msg}");
}

fn warn(msg: &str) {
    println!("  {YELLOW}\u{26a0}{RESET} {msg}");
}

fn header(step: &str, title: &str) {
    println!();
    println!("{BOLD}{CYAN}[{step}]{RESET} {BOLD}{title}{RESET}");
    println!("{DIM}{}─{RESET}", "─".repeat(50));
}

pub fn run_debug() {
    println!();
    println!(
        "{BOLD}  Uprooted Installer v{} — Debug Mode{RESET}",
        env!("CARGO_PKG_VERSION")
    );
    println!("{DIM}  {}{RESET}", "═".repeat(45));
    println!("  Platform: {}", std::env::consts::OS);
    println!("  Arch:     {}", std::env::consts::ARCH);
    println!(
        "  Time:     {}",
        chrono_lite()
    );

    // ── [1/7] Detection ──
    header("1/7", "Detection");
    let detection = detection::detect();

    if detection.root_found {
        ok(&format!("Root found: {}", detection.root_path));
    } else {
        fail(&format!("Root NOT found (expected: {})", detection.root_path));
    }

    let profile_dir = detection::get_profile_dir();
    if profile_dir.exists() {
        ok(&format!("Profile dir: {}", profile_dir.display()));
    } else {
        warn(&format!(
            "Profile dir missing: {} (launch Root once to create it)",
            profile_dir.display()
        ));
    }

    println!("  HTML files found: {}", detection.html_files.len());
    for f in &detection.html_files {
        let short = f
            .rsplit_once(std::path::MAIN_SEPARATOR)
            .and_then(|(parent, _)| {
                parent
                    .rsplit_once(std::path::MAIN_SEPARATOR)
                    .map(|(_, dir)| format!("{dir}/index.html"))
            })
            .unwrap_or_else(|| f.clone());
        println!("    {DIM}{short}{RESET}");
    }

    if detection.is_installed {
        ok("HTML patches detected (installed)");
    } else {
        warn("HTML patches NOT detected (not installed)");
    }

    // Hook status detail
    let hs = &detection.hook_status;
    println!();
    println!("  {BOLD}Hook file status:{RESET}");
    status_line("profiler dll/so", hs.profiler_dll);
    status_line("UprootedHook.dll", hs.hook_dll);
    status_line("UprootedHook.deps.json", hs.hook_deps);
    status_line("uprooted-preload.js", hs.preload_js);
    status_line("uprooted.css", hs.theme_css);

    println!();
    println!("  {BOLD}Env var status:{RESET}");
    status_line("CORECLR_ENABLE_PROFILING", hs.env_enable_profiling);
    status_line("CORECLR_PROFILER", hs.env_profiler_guid);
    status_line("CORECLR_PROFILER_PATH", hs.env_profiler_path);
    status_line("DOTNET_ReadyToRun", hs.env_ready_to_run);

    if hs.env_vars_active {
        ok("Env vars active in current session");
    } else {
        warn("Env vars NOT active in current session (re-login may be needed)");
    }

    // ── [2/7] Process check ──
    header("2/7", "Process check");
    if hook::check_root_running() {
        warn("Root is currently RUNNING — installation may require restart");
    } else {
        ok("Root is not running");
    }

    // ── [3/7] File deployment ──
    header("3/7", "File deployment");
    match hook::deploy_files() {
        Ok(()) => {
            ok("Files deployed successfully");
            let dir = hook::get_uprooted_dir();
            let files = [
                "uprooted_profiler.dll",
                "libuprooted_profiler.so",
                "UprootedHook.dll",
                "UprootedHook.deps.json",
                "uprooted-preload.js",
                "uprooted.css",
                "nsfw-filter.js",
                "link-embeds.js",
            ];
            for name in &files {
                let path = dir.join(name);
                if path.exists() {
                    let size = fs::metadata(&path).map(|m| m.len()).unwrap_or(0);
                    println!("    {DIM}{name}{RESET} ({} bytes)", format_size(size));
                }
            }
        }
        Err(e) => {
            fail(&format!("File deployment FAILED: {e}"));
        }
    }

    // ── [4/7] Environment variables ──
    header("4/7", "Environment variables");
    match hook::set_env_vars() {
        Ok(()) => {
            ok("Environment variables set");
        }
        Err(e) => {
            fail(&format!("Env var setup FAILED: {e}"));
        }
    }

    // Re-check env vars after setting
    let hs2 = hook::check_hook_status();
    println!("  {BOLD}Post-set verification:{RESET}");
    status_line("CORECLR_ENABLE_PROFILING", hs2.env_enable_profiling);
    status_line("CORECLR_PROFILER", hs2.env_profiler_guid);
    status_line("CORECLR_PROFILER_PATH", hs2.env_profiler_path);
    status_line("DOTNET_ReadyToRun", hs2.env_ready_to_run);

    // ── [5/7] HTML patching ──
    header("5/7", "HTML patching");
    let patch_result = patcher::install();
    if patch_result.success {
        ok(&patch_result.message);
        for f in &patch_result.files_patched {
            println!("    {DIM}{f}{RESET}");
        }
    } else {
        fail(&format!("HTML patching FAILED: {}", patch_result.message));
    }

    // ── [6/7] Post-install verification ──
    header("6/7", "Post-install verification");
    let final_detection = detection::detect();
    let final_hs = &final_detection.hook_status;

    if final_hs.files_ok {
        ok("All files deployed");
    } else {
        fail("Some files missing");
    }

    if final_hs.env_ok {
        ok("Env vars configured");
    } else {
        fail("Env vars incomplete");
    }

    if final_detection.is_installed {
        ok("HTML patches applied");
    } else {
        fail("HTML patches missing");
    }

    // Overall verdict
    let all_good = final_hs.files_ok && final_hs.env_ok && final_detection.is_installed;
    println!();
    if all_good {
        println!(
            "  {GREEN}{BOLD}\u{2713} Installation looks good!{RESET} Restart Root to activate Uprooted."
        );
    } else {
        println!(
            "  {RED}{BOLD}\u{2717} Installation has issues.{RESET} Check the errors above."
        );
    }

    // ── [7/7] Hook log tail ──
    header("7/7", "Hook log (last 50 lines)");
    let log_path = detection::get_profile_dir().join("uprooted-hook.log");
    if log_path.exists() {
        match fs::read_to_string(&log_path) {
            Ok(content) => {
                let lines: Vec<&str> = content.lines().collect();
                let start = lines.len().saturating_sub(50);
                ok(&format!(
                    "Log file: {} ({} lines total)",
                    log_path.display(),
                    lines.len()
                ));
                for line in &lines[start..] {
                    println!("    {DIM}{line}{RESET}");
                }
            }
            Err(e) => {
                warn(&format!("Could not read log: {e}"));
            }
        }
    } else {
        warn(&format!(
            "No hook log found at {} (hook has never loaded)",
            log_path.display()
        ));
    }

    println!();
}

fn status_line(label: &str, ok_val: bool) {
    if ok_val {
        println!("    {GREEN}\u{2713}{RESET} {label}");
    } else {
        println!("    {RED}\u{2717}{RESET} {label}");
    }
}

fn format_size(bytes: u64) -> String {
    if bytes < 1024 {
        format!("{bytes}")
    } else if bytes < 1024 * 1024 {
        format!("{:.1} KB", bytes as f64 / 1024.0)
    } else {
        format!("{:.1} MB", bytes as f64 / (1024.0 * 1024.0))
    }
}

/// Minimal timestamp without pulling in the chrono crate.
/// Returns a human-readable string from UNIX epoch.
fn chrono_lite() -> String {
    use std::time::SystemTime;
    match SystemTime::now().duration_since(SystemTime::UNIX_EPOCH) {
        Ok(d) => {
            let secs = d.as_secs();
            // Basic UTC breakdown
            let days = secs / 86400;
            let time_secs = secs % 86400;
            let hours = time_secs / 3600;
            let minutes = (time_secs % 3600) / 60;
            let seconds = time_secs % 60;

            // Days since epoch to Y-M-D (simplified leap year calculation)
            let (year, month, day) = days_to_date(days);
            format!(
                "{year:04}-{month:02}-{day:02} {hours:02}:{minutes:02}:{seconds:02} UTC"
            )
        }
        Err(_) => "unknown".to_string(),
    }
}

fn days_to_date(days: u64) -> (u64, u64, u64) {
    // Civil calendar algorithm from Howard Hinnant
    let z = days + 719468;
    let era = z / 146097;
    let doe = z - era * 146097;
    let yoe = (doe - doe / 1460 + doe / 36524 - doe / 146096) / 365;
    let y = yoe + era * 400;
    let doy = doe - (365 * yoe + yoe / 4 - yoe / 100);
    let mp = (5 * doy + 2) / 153;
    let d = doy - (153 * mp + 2) / 5 + 1;
    let m = if mp < 10 { mp + 3 } else { mp - 9 };
    let y = if m <= 2 { y + 1 } else { y };
    (y, m, d)
}
