mod cli;
mod detection;
mod embedded;
mod hook;
mod patcher;
mod settings;

use clap::Parser;

#[derive(Parser)]
#[command(name = "uprooted", about = "Uprooted installer for Root Communications")]
struct Cli {
    /// Uninstall Uprooted (remove env vars, restore HTML, delete files)
    #[arg(long)]
    uninstall: bool,

    /// Repair installation (re-deploy files, re-patch HTML)
    #[arg(long)]
    repair: bool,

    /// Run diagnostics (check files, env vars, patches)
    #[arg(long)]
    diagnose: bool,

    /// Plain ANSI output instead of TUI (for scripts / CI)
    #[arg(long)]
    plain: bool,
}

fn main() {
    let args = Cli::parse();

    if args.diagnose {
        cli::run_diagnose();
        return;
    }

    if args.uninstall {
        if args.plain {
            cli::run_uninstall_plain();
        } else {
            tui::run_uninstall();
        }
        return;
    }

    if args.repair {
        if args.plain {
            cli::run_repair_plain();
        } else {
            tui::run_repair();
        }
        return;
    }

    // Default: install
    if args.plain {
        cli::run_install_plain();
    } else {
        tui::run_install();
    }
}

// ══════════════════════════════════════════════════════════════════════════════
// TUI module — btop-inspired console UI with ratatui
// ══════════════════════════════════════════════════════════════════════════════

mod tui {
    use crate::{detection, hook, patcher};
    use crossterm::{
        event::{self, Event, KeyEventKind},
        execute,
        terminal::{disable_raw_mode, enable_raw_mode, EnterAlternateScreen, LeaveAlternateScreen},
    };
    use ratatui::{
        backend::CrosstermBackend,
        layout::Rect,
        style::{Color, Modifier, Style},
        text::{Line, Span},
        widgets::{Block, Borders, Paragraph},
        Frame, Terminal,
    };
    use std::io;
    use std::time::{Duration, Instant};

    #[derive(Clone, PartialEq)]
    enum StepStatus {
        Pending,
        Running,
        Done,
        Failed(String),
    }

    struct Step {
        label: &'static str,
        status: StepStatus,
    }

    impl Step {
        fn new(label: &'static str) -> Self {
            Self {
                label,
                status: StepStatus::Pending,
            }
        }
    }

    struct AppState {
        steps: Vec<Step>,
        title: &'static str,
        finished: bool,
        success: bool,
        message: String,
        spinner_tick: usize,
    }

    const SPINNER: &[&str] = &["⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏"];

    fn render(frame: &mut Frame, state: &AppState) {
        let area = frame.area();

        // Center a box in the terminal
        let box_width = 60u16.min(area.width);
        let box_height = (state.steps.len() as u16 + 8).min(area.height);
        let x = (area.width.saturating_sub(box_width)) / 2;
        let y = (area.height.saturating_sub(box_height)) / 2;
        let centered = Rect::new(x, y, box_width, box_height);

        let block = Block::default()
            .borders(Borders::ALL)
            .border_style(Style::default().fg(Color::Cyan))
            .title(Span::styled(
                format!(" {} ", state.title),
                Style::default()
                    .fg(Color::Cyan)
                    .add_modifier(Modifier::BOLD),
            ));

        let inner = block.inner(centered);
        frame.render_widget(block, centered);

        // Build content lines
        let mut lines: Vec<Line> = Vec::new();

        // Version line
        lines.push(Line::from(Span::styled(
            format!("  Uprooted v{}", env!("CARGO_PKG_VERSION")),
            Style::default()
                .fg(Color::White)
                .add_modifier(Modifier::BOLD),
        )));
        lines.push(Line::from(""));

        // Steps
        for step in &state.steps {
            let (icon, style) = match &step.status {
                StepStatus::Pending => (
                    "  ○".to_string(),
                    Style::default().fg(Color::DarkGray),
                ),
                StepStatus::Running => (
                    format!("  {}", SPINNER[state.spinner_tick % SPINNER.len()]),
                    Style::default().fg(Color::Yellow),
                ),
                StepStatus::Done => (
                    "  ✓".to_string(),
                    Style::default().fg(Color::Green),
                ),
                StepStatus::Failed(_) => (
                    "  ✗".to_string(),
                    Style::default().fg(Color::Red),
                ),
            };
            lines.push(Line::from(vec![
                Span::styled(icon, style),
                Span::raw(" "),
                Span::styled(step.label, style),
            ]));

            // Show error detail on failure
            if let StepStatus::Failed(msg) = &step.status {
                lines.push(Line::from(Span::styled(
                    format!("      {}", msg),
                    Style::default().fg(Color::Red),
                )));
            }
        }

        // Footer
        lines.push(Line::from(""));
        if state.finished {
            if state.success {
                lines.push(Line::from(Span::styled(
                    format!("  {}", state.message),
                    Style::default()
                        .fg(Color::Green)
                        .add_modifier(Modifier::BOLD),
                )));
            } else {
                lines.push(Line::from(Span::styled(
                    format!("  {}", state.message),
                    Style::default()
                        .fg(Color::Red)
                        .add_modifier(Modifier::BOLD),
                )));
            }
            lines.push(Line::from(Span::styled(
                "  Press any key to exit...",
                Style::default().fg(Color::DarkGray),
            )));
        }

        let paragraph = Paragraph::new(lines);
        frame.render_widget(paragraph, inner);
    }

    fn run_tui(mut state: AppState, execute_steps: impl FnOnce(&mut AppState)) {
        // Setup terminal
        let _ = enable_raw_mode();
        let mut stdout = io::stdout();
        let _ = execute!(stdout, EnterAlternateScreen);
        let backend = CrosstermBackend::new(stdout);
        let mut terminal = match Terminal::new(backend) {
            Ok(t) => t,
            Err(_) => {
                let _ = disable_raw_mode();
                eprintln!("Failed to initialize terminal UI");
                return;
            }
        };

        // Initial render
        let _ = terminal.draw(|f| render(f, &state));

        // Execute steps (blocking)
        execute_steps(&mut state);

        // Final render
        let _ = terminal.draw(|f| render(f, &state));

        // Wait for keypress or auto-close on success after 3s
        let deadline = if state.success {
            Some(Instant::now() + Duration::from_secs(3))
        } else {
            None
        };

        loop {
            let timeout = match deadline {
                Some(d) => d.saturating_duration_since(Instant::now()),
                None => Duration::from_secs(60),
            };

            if timeout.is_zero() {
                break;
            }

            if event::poll(timeout.min(Duration::from_millis(100))).unwrap_or(false) {
                if let Ok(Event::Key(key)) = event::read() {
                    if key.kind == KeyEventKind::Press {
                        break;
                    }
                }
            }

            // Update spinner for any running steps
            state.spinner_tick += 1;
            let _ = terminal.draw(|f| render(f, &state));
        }

        // Restore terminal
        let _ = disable_raw_mode();
        let _ = execute!(terminal.backend_mut(), LeaveAlternateScreen);
    }

    pub fn run_install() {
        let state = AppState {
            steps: vec![
                Step::new("Detect Root installation"),
                Step::new("Deploy hook files"),
                Step::new("Set environment variables"),
                Step::new("Patch HTML files"),
                Step::new("Verify installation"),
            ],
            title: "Install",
            finished: false,
            success: false,
            message: String::new(),
            spinner_tick: 0,
        };

        run_tui(state, |state| {
            // Step 0: Detect
            state.steps[0].status = StepStatus::Running;
            let detection = detection::detect();
            if detection.root_found {
                state.steps[0].status = StepStatus::Done;
            } else {
                state.steps[0].status =
                    StepStatus::Failed(format!("Root not found at {}", detection.root_path));
                state.finished = true;
                state.message = "Installation failed.".to_string();
                return;
            }

            // Step 1: Deploy files
            state.steps[1].status = StepStatus::Running;
            match hook::deploy_files() {
                Ok(()) => state.steps[1].status = StepStatus::Done,
                Err(e) => {
                    state.steps[1].status = StepStatus::Failed(e);
                    state.finished = true;
                    state.message = "Installation failed.".to_string();
                    return;
                }
            }

            // Step 2: Set env vars
            state.steps[2].status = StepStatus::Running;
            match hook::set_env_vars() {
                Ok(()) => state.steps[2].status = StepStatus::Done,
                Err(e) => {
                    state.steps[2].status = StepStatus::Failed(e);
                    state.finished = true;
                    state.message = "Installation failed.".to_string();
                    return;
                }
            }

            // Step 3: Patch HTML
            state.steps[3].status = StepStatus::Running;
            let result = patcher::install();
            if result.success {
                state.steps[3].status = StepStatus::Done;
            } else {
                state.steps[3].status = StepStatus::Failed(result.message);
                state.finished = true;
                state.message = "Installation failed.".to_string();
                return;
            }

            // Step 4: Verify
            state.steps[4].status = StepStatus::Running;
            let final_check = detection::detect();
            if final_check.hook_status.files_ok && final_check.is_installed {
                state.steps[4].status = StepStatus::Done;
            } else {
                state.steps[4].status = StepStatus::Failed("Verification found issues".to_string());
                state.finished = true;
                state.message = "Installed with warnings.".to_string();
                state.success = true;
                return;
            }

            state.finished = true;
            state.success = true;
            state.message = "Patch active — restart Root to load Uprooted.".to_string();
        });
    }

    pub fn run_uninstall() {
        let state = AppState {
            steps: vec![
                Step::new("Remove environment variables"),
                Step::new("Restore HTML files"),
                Step::new("Remove deployed files"),
            ],
            title: "Uninstall",
            finished: false,
            success: false,
            message: String::new(),
            spinner_tick: 0,
        };

        run_tui(state, |state| {
            // Step 0: Remove env vars
            state.steps[0].status = StepStatus::Running;
            match hook::remove_env_vars() {
                Ok(()) => state.steps[0].status = StepStatus::Done,
                Err(e) => {
                    state.steps[0].status = StepStatus::Failed(e);
                    state.finished = true;
                    state.message = "Uninstall failed.".to_string();
                    return;
                }
            }

            // Step 1: Restore HTML
            state.steps[1].status = StepStatus::Running;
            let result = patcher::uninstall();
            if result.success {
                state.steps[1].status = StepStatus::Done;
            } else {
                state.steps[1].status = StepStatus::Failed(result.message);
            }

            // Step 2: Remove files
            state.steps[2].status = StepStatus::Running;
            match hook::remove_files() {
                Ok(()) => state.steps[2].status = StepStatus::Done,
                Err(e) => {
                    state.steps[2].status = StepStatus::Failed(e);
                    state.finished = true;
                    state.message = "Uninstall had errors.".to_string();
                    return;
                }
            }

            state.finished = true;
            state.success = true;
            state.message = "Uprooted removed.".to_string();
        });
    }

    pub fn run_repair() {
        let state = AppState {
            steps: vec![
                Step::new("Re-deploy hook files"),
                Step::new("Set environment variables"),
                Step::new("Re-patch HTML files"),
                Step::new("Verify installation"),
            ],
            title: "Repair",
            finished: false,
            success: false,
            message: String::new(),
            spinner_tick: 0,
        };

        run_tui(state, |state| {
            // Step 0: Deploy files
            state.steps[0].status = StepStatus::Running;
            match hook::deploy_files() {
                Ok(()) => state.steps[0].status = StepStatus::Done,
                Err(e) => {
                    state.steps[0].status = StepStatus::Failed(e);
                    state.finished = true;
                    state.message = "Repair failed.".to_string();
                    return;
                }
            }

            // Step 1: Set env vars
            state.steps[1].status = StepStatus::Running;
            match hook::set_env_vars() {
                Ok(()) => state.steps[1].status = StepStatus::Done,
                Err(e) => {
                    state.steps[1].status = StepStatus::Failed(e);
                    state.finished = true;
                    state.message = "Repair failed.".to_string();
                    return;
                }
            }

            // Step 2: Repair HTML
            state.steps[2].status = StepStatus::Running;
            let result = patcher::repair();
            if result.success {
                state.steps[2].status = StepStatus::Done;
            } else {
                state.steps[2].status = StepStatus::Failed(result.message);
                state.finished = true;
                state.message = "Repair failed.".to_string();
                return;
            }

            // Step 3: Verify
            state.steps[3].status = StepStatus::Running;
            let final_check = detection::detect();
            if final_check.hook_status.files_ok && final_check.is_installed {
                state.steps[3].status = StepStatus::Done;
            } else {
                state.steps[3].status = StepStatus::Failed("Verification found issues".to_string());
            }

            state.finished = true;
            state.success = true;
            state.message = "Repair complete — restart Root to load Uprooted.".to_string();
        });
    }
}
