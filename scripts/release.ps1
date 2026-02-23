# release.ps1 — Version bump, tag, push, and create a GitHub release.
#
# Usage:
#   .\scripts\release.ps1 0.6.0              # Release a specific version
#   .\scripts\release.ps1 0.6.0 -Prerelease  # Mark as pre-release
#   .\scripts\release.ps1 patch              # Auto-bump patch version
#   .\scripts\release.ps1 minor              # Auto-bump minor version

param(
    [Parameter(Mandatory)]
    [string]$VersionArg,
    [switch]$Prerelease
)

$ErrorActionPreference = 'Stop'
$RepoRoot = Split-Path -Parent (Split-Path -Parent $PSCommandPath)

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
    default {
        $Version = $VersionArg
    }
}

Write-Host "Releasing: $Current -> $Version"

# 1. Bump all version references
& powershell -File "$RepoRoot\scripts\bump-version.ps1" -Version $Version

# 2. Commit
Set-Location $RepoRoot
git add -A
git commit -m "chore: bump to $Version"

# 3. Tag
git tag -a "v$Version" -m "Release v$Version"

# 4. Push
git push origin main "v$Version"

# 5. Create GitHub release
$prereleaseFlag = if ($Prerelease) { "--prerelease" } else { "" }
$notesFlag = ""
if (Test-Path "$RepoRoot\NEXT-RELEASE.md") {
    $notesFlag = "--notes-file NEXT-RELEASE.md"
}

$cmd = "gh release create `"v$Version`" --title `"v$Version`" $notesFlag $prereleaseFlag"
Invoke-Expression $cmd

Write-Host ""
Write-Host "Release v$Version created. CI will now build and publish artifacts."
Write-Host "Monitor: gh run watch"
