# bump-version.ps1 — Sync all version references from the VERSION file.
#
# Usage:
#   .\scripts\bump-version.ps1              # Read version from VERSION file
#   .\scripts\bump-version.ps1 0.6.0        # Override with explicit version

param(
    [string]$Version
)

$ErrorActionPreference = 'Stop'
$RepoRoot = Split-Path -Parent (Split-Path -Parent $PSCommandPath)

# Resolve version
if ($Version) {
    Set-Content -Path "$RepoRoot\VERSION" -Value $Version -NoNewline
    Write-Host "Set VERSION file to $Version"
} else {
    $Version = (Get-Content "$RepoRoot\VERSION" -Raw).Trim()
}

if (-not $Version) {
    Write-Error "No version found. Pass as argument or populate VERSION file."
    exit 1
}

Write-Host "Bumping all version references to: $Version"

# 1. package.json
$pkg = Get-Content "$RepoRoot\package.json" -Raw
$pkg = $pkg -replace '"version":\s*"[^"]*"', "`"version`": `"$Version`""
Set-Content -Path "$RepoRoot\package.json" -Value $pkg -NoNewline
Write-Host "  updated package.json"

# 2. installer/src-tauri/Cargo.toml
$cargo = Get-Content "$RepoRoot\installer\src-tauri\Cargo.toml" -Raw
$cargo = $cargo -replace '(?m)^version = "[^"]*"', "version = `"$Version`""
Set-Content -Path "$RepoRoot\installer\src-tauri\Cargo.toml" -Value $cargo -NoNewline
Write-Host "  updated installer/src-tauri/Cargo.toml"

# 3. hook/UprootedSettings.cs
$settings = Get-Content "$RepoRoot\hook\UprootedSettings.cs" -Raw
$settings = $settings -replace 'Version \{ get; set; \} = "[^"]*"', "Version { get; set; } = `"$Version`""
Set-Content -Path "$RepoRoot\hook\UprootedSettings.cs" -Value $settings -NoNewline
Write-Host "  updated hook/UprootedSettings.cs"

# 4. hook/StartupHook.cs
$startup = Get-Content "$RepoRoot\hook\StartupHook.cs" -Raw
$startup = $startup -replace 'CurrentVersion = "[^"]*"', "CurrentVersion = `"$Version`""
Set-Content -Path "$RepoRoot\hook\StartupHook.cs" -Value $startup -NoNewline
Write-Host "  updated hook/StartupHook.cs"

# 5. hook/ContentPages.cs — plugin version strings
$content = Get-Content "$RepoRoot\hook\ContentPages.cs" -Raw
$content = $content -replace 'Version = "[0-9][^"]*"', "Version = `"$Version`""
Set-Content -Path "$RepoRoot\hook\ContentPages.cs" -Value $content -NoNewline
Write-Host "  updated hook/ContentPages.cs"

# 6. install-uprooted-linux.sh
$installer = Get-Content "$RepoRoot\install-uprooted-linux.sh" -Raw
$installer = $installer -replace '(?m)^VERSION="[^"]*"', "VERSION=`"$Version`""
Set-Content -Path "$RepoRoot\install-uprooted-linux.sh" -Value $installer -NoNewline
Write-Host "  updated install-uprooted-linux.sh"

# 7. site/src/pages/index.astro
$astroPath = "$RepoRoot\site\src\pages\index.astro"
if (Test-Path $astroPath) {
    $astro = Get-Content $astroPath -Raw
    $astro = $astro -replace '<span class="version">[^<]*<', "<span class=`"version`">$Version<"
    Set-Content -Path $astroPath -Value $astro -NoNewline
    Write-Host "  updated site/src/pages/index.astro"
}

Write-Host ""
Write-Host "All version references bumped to $Version"
