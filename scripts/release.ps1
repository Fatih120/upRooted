# release.ps1 — Version bump, tag, push, and create a GitHub release.
#
# Three channels:
#   stable  — public repo, from main branch, triggers package manager updates
#   canary  — canary repo, from main branch, bleeding edge
#   dev     — private repo, from dev branch, invite only
#
# Usage:
#   .\scripts\release.ps1 stable 0.6.0
#   .\scripts\release.ps1 canary 0.6.0-canary.1
#   .\scripts\release.ps1 dev 0.6.0-dev.1
#   .\scripts\release.ps1 stable patch

param(
    [Parameter(Mandatory)]
    [ValidateSet("stable", "canary", "dev")]
    [string]$Channel,

    [Parameter(Mandatory)]
    [string]$VersionArg
)

$ErrorActionPreference = 'Stop'
$RepoRoot = Split-Path -Parent (Split-Path -Parent $PSCommandPath)

# Determine branch
$Branch = if ($Channel -eq "dev") { "dev" } else { "main" }

# Discover remote
$Remote = (git -C $RepoRoot remote | Select-Object -First 1)
Write-Host "Using remote: $Remote"

# Ensure correct branch
$CurrentBranch = git -C $RepoRoot branch --show-current
if ($CurrentBranch -ne $Branch) {
    Write-Host "Switching to $Branch branch..."
    git -C $RepoRoot checkout $Branch
}

# Pull latest
git -C $RepoRoot pull $Remote $Branch

# Read current version
$Current = (Get-Content "$RepoRoot\VERSION" -Raw).Trim()

# Compute new version
switch ($VersionArg) {
    'patch' {
        $parts = $Current.Split('-')[0].Split('.')
        $Version = "$($parts[0]).$($parts[1]).$([int]$parts[2] + 1)"
    }
    'minor' {
        $parts = $Current.Split('-')[0].Split('.')
        $Version = "$($parts[0]).$([int]$parts[1] + 1).0"
    }
    'major' {
        $parts = $Current.Split('-')[0].Split('.')
        $Version = "$([int]$parts[0] + 1).0.0"
    }
    default { $Version = $VersionArg }
}

$PrereleaseFlag = if ($Channel -in "canary", "dev") { "--prerelease" } else { "" }

Write-Host ""
Write-Host "Channel:  $Channel"
Write-Host "Branch:   $Branch"
Write-Host "Version:  $Current -> $Version"
Write-Host "Remote:   $Remote"
Write-Host ""

# 1. Bump versions
& powershell -File "$RepoRoot\scripts\bump-version.ps1" -Version $Version

# 2. Commit
Set-Location $RepoRoot
git add -A
git commit -m "release: v$Version ($Channel)"

# 3. Tag
git tag -a "v$Version" -m "Release v$Version ($Channel channel)"

# 4. Push
git push $Remote $Branch "v$Version"

# 5. Create GitHub release
$notesFlag = ""
if ($Channel -eq "stable" -and (Test-Path "$RepoRoot\NEXT-RELEASE.md")) {
    $notesFlag = "--notes-file NEXT-RELEASE.md"
} else {
    $notesFlag = "--notes `"$($Channel.Substring(0,1).ToUpper() + $Channel.Substring(1)) release v$Version`""
}

$cmd = "gh release create `"v$Version`" --title `"v$Version`" $notesFlag $PrereleaseFlag"
Invoke-Expression $cmd

Write-Host ""
Write-Host "Release v$Version ($Channel) created on $Branch branch."
Write-Host "CI will now build and publish artifacts."
switch ($Channel) {
    "stable" { Write-Host "  -> The-Uprooted-Project/uprooted + all package managers" }
    "canary" { Write-Host "  -> The-Uprooted-Project/uprooted-canary" }
    "dev"    { Write-Host "  -> The-Uprooted-Project/uprooted-private (dev branch)" }
}
