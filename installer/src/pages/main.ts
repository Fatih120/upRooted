import {
  detectRoot,
  installUprooted,
  uninstallUprooted,
  repairUprooted,
  getUprootedVersion,
  type DetectionResult,
} from "../lib/tauri.js";

let logEl: HTMLDivElement;
let detection: DetectionResult | null = null;

function log(text: string, type: "info" | "success" | "error" | "" = ""): void {
  const line = document.createElement("div");
  line.className = `log-line ${type}`;
  line.innerHTML = `<span class="prefix">&gt;</span>${escapeHtml(text)}`;
  logEl.appendChild(line);
  logEl.scrollTop = logEl.scrollHeight;
}

function escapeHtml(s: string): string {
  return s.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
}

function statusDot(color: "green" | "red" | "yellow"): string {
  return `<span class="status-dot ${color}"></span>`;
}

function truncatePath(p: string, maxLen = 45): string {
  if (p.length <= maxLen) return p;
  return "..." + p.slice(p.length - maxLen + 3);
}

async function runDetection(): Promise<void> {
  log("detecting root installation...");
  try {
    detection = await detectRoot();

    if (detection.root_found) {
      log(`found Root.exe at ${detection.root_path}`, "success");
    } else {
      log("Root.exe not found", "error");
    }

    log(`profile: ${detection.profile_dir}`);
    log(`${detection.html_files.length} HTML files detected`);

    if (detection.is_installed) {
      log("uprooted is currently installed", "success");
    } else {
      log("uprooted is not installed");
    }

    updateStatusDisplay();
    updateButtons();
  } catch (err) {
    log(`detection failed: ${err}`, "error");
  }
}

function updateStatusDisplay(): void {
  const el = document.getElementById("status-rows");
  if (!el || !detection) return;

  el.innerHTML = `
    <div class="status-row">
      ${statusDot(detection.root_found ? "green" : "red")}
      <span class="status-label">Root.exe</span>
      <span class="status-value">${detection.root_found ? truncatePath(detection.root_path) : "not found"}</span>
    </div>
    <div class="status-row">
      ${statusDot(detection.html_files.length > 0 ? "green" : "yellow")}
      <span class="status-label">Profile</span>
      <span class="status-value">${detection.html_files.length} HTML files detected</span>
    </div>
    <div class="status-row">
      ${statusDot(detection.is_installed ? "green" : "red")}
      <span class="status-label">Uprooted</span>
      <span class="status-value">${detection.is_installed ? "installed" : "not installed"}</span>
    </div>
  `;
}

function updateButtons(): void {
  const installBtn = document.getElementById("btn-install") as HTMLButtonElement | null;
  const uninstallBtn = document.getElementById("btn-uninstall") as HTMLButtonElement | null;
  const repairBtn = document.getElementById("btn-repair") as HTMLButtonElement | null;

  if (!detection) return;

  if (installBtn) installBtn.disabled = !detection.root_found || detection.is_installed;
  if (uninstallBtn) uninstallBtn.disabled = !detection.is_installed;
  if (repairBtn) repairBtn.disabled = !detection.is_installed;
}

function setButtonsDisabled(disabled: boolean): void {
  for (const id of ["btn-install", "btn-uninstall", "btn-repair"]) {
    const btn = document.getElementById(id) as HTMLButtonElement | null;
    if (btn) btn.disabled = disabled;
  }
}

async function handleInstall(): Promise<void> {
  setButtonsDisabled(true);
  log("installing uprooted...");
  try {
    const result = await installUprooted();
    if (result.success) {
      log(result.message, "success");
      for (const f of result.files_patched) {
        log(`  patched: ${f}`, "success");
      }
    } else {
      log(result.message, "error");
    }
    await runDetection();
  } catch (err) {
    log(`install failed: ${err}`, "error");
    setButtonsDisabled(false);
  }
}

async function handleUninstall(): Promise<void> {
  setButtonsDisabled(true);
  log("uninstalling uprooted...");
  try {
    const result = await uninstallUprooted();
    if (result.success) {
      log(result.message, "success");
    } else {
      log(result.message, "error");
    }
    await runDetection();
  } catch (err) {
    log(`uninstall failed: ${err}`, "error");
    setButtonsDisabled(false);
  }
}

async function handleRepair(): Promise<void> {
  setButtonsDisabled(true);
  log("repairing uprooted...");
  try {
    const result = await repairUprooted();
    if (result.success) {
      log(result.message, "success");
    } else {
      log(result.message, "error");
    }
    await runDetection();
  } catch (err) {
    log(`repair failed: ${err}`, "error");
    setButtonsDisabled(false);
  }
}

export async function init(container: HTMLElement): Promise<void> {
  let version = "0.1.0";
  try {
    version = await getUprootedVersion();
  } catch {
    // use default
  }

  container.innerHTML = `
    <div class="header">
      <h1>uprooted <span class="dim">v${version}</span></h1>
      <p class="sub">a client mod framework for root communications</p>
    </div>

    <div class="status-section">
      <div class="status-header">-- status --</div>
      <div id="status-rows">
        <div class="status-row">
          <span class="status-dot yellow"></span>
          <span class="status-label">detecting...</span>
        </div>
      </div>
    </div>

    <div class="actions">
      <button id="btn-install" class="btn primary" disabled>install</button>
      <button id="btn-uninstall" class="btn danger" disabled>uninstall</button>
      <button id="btn-repair" class="btn warn" disabled>repair</button>
    </div>

    <div class="log-section">
      <div class="log-header">-- log --</div>
      <div id="log" class="log"></div>
    </div>
  `;

  logEl = document.getElementById("log") as HTMLDivElement;

  document.getElementById("btn-install")!.addEventListener("click", handleInstall);
  document.getElementById("btn-uninstall")!.addEventListener("click", handleUninstall);
  document.getElementById("btn-repair")!.addEventListener("click", handleRepair);

  await runDetection();
}
