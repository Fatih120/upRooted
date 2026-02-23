# Release System Guide

Hey — here's how the new release system works. We have 3 channels, automated CI/CD, and a skill that handles everything.

## Three Channels

| Channel | Repo | Who gets it | How often |
|---------|------|-------------|-----------|
| **Stable** | `uprooted` (public) | Everyone | When we're confident a build is solid |
| **Canary** | `uprooted-canary` (public) | Users who opt in via About page toggle | Every time we want to ship bleeding edge |
| **Dev** | `uprooted-private` (private) | Us + testers with the password | Whenever, from the dev branch |

Users switch channels from the About page in Uprooted's settings. There's a Stable/Canary toggle (pill switch). Dev channel is hidden — spam the toggle 6 times fast and it shows a password prompt.

## Branch Model

```
dev    ●──●──●──●──●──●──●──●──●     (working branch, always ahead)
            \              \
main         ●──── v0.5.0   ●──── v0.6.0   (release snapshots)
```

- **dev** is where all work happens. Every commit goes here first.
- **main** only moves forward during releases (dev gets merged in).
- After releasing, dev merges main back so it stays ahead.

## How to Release

### The `/release` command

```
/release canary 0.6.0              # canary only
/release canary/dev 0.6.0          # canary + dev
/release stable 0.6.0              # stable (triggers package managers too)
/release stable/canary/dev 0.6.0   # all three at once
```

Add `local` if GitHub Actions minutes are exhausted:
```
/release canary local 0.6.0        # build on this machine instead of CI
```

### What the skill does automatically

1. **Commits any uncommitted work to dev** (nothing lost)
2. **Merges dev → main** (for canary/stable releases)
3. **Bumps version** across all files (VERSION, package.json, Cargo.toml, StartupHook.cs, etc.)
4. **Runs doc sweep** — updates changelogs, session state, file maps
5. **Builds and tests** — dotnet build, dotnet test (170 tests), pnpm build
6. **Two approval gates** — you review before anything is committed or pushed
7. **Commits, tags, pushes** on main
8. **Syncs dev** — merges main back into dev so dev stays ahead
9. **Builds artifacts** (local or CI) and uploads to target repos
10. **Verifies** all assets are present on each repo

### Platform re-triggers

If a single platform build failed:
```
/release windows     # just rebuild Windows
/release linux       # just rebuild Linux
```

## Release Artifacts (per repo)

| File | What |
|------|------|
| `Uprooted-X.Y.Z-windows-amd64.exe` | Windows TUI installer |
| `Uprooted-X.Y.Z-linux-amd64` | Linux standalone binary |
| `Uprooted-X.Y.Z-x86_64.AppImage` | Linux AppImage |
| `auto-update.uprpkg` | Encrypted update package (6 files) |
| `uprooted-linux-artifacts.tar.gz` | Shared artifacts for bash installer |
| `install-uprooted-linux.sh` | Bash one-liner installer |
| `checksums.txt` | SHA256 hashes |

## Auto-Updater

The hook checks for updates every 60 seconds based on the user's channel:
- **Stable**: hits `/releases/latest` on the public repo
- **Canary**: hits `/releases?per_page=1` on the canary repo
- **Dev**: hits `/releases?per_page=1` on the private repo (with encrypted PAT auth)

Downloads `auto-update.uprpkg`, decrypts, overwrites 6 files in the install dir. Changes apply on next Root restart.

## Package Managers (stable only)

When `/release stable` runs, it also triggers `publish-packages.yml` which pushes to:
- Chocolatey (`choco install uprooted`)
- winget (`winget install TheUprootedProject.Uprooted`)
- Scoop (via `The-Uprooted-Project/scoop-bucket`)
- Homebrew (via `The-Uprooted-Project/homebrew-tap`)
- AUR (`yay -S uprooted-bin`)
- Cloudsmith APT (`apt install uprooted`)

These require API keys configured as GitHub Actions secrets. Chocolatey and Cloudsmith keys aren't set yet.

## Quick Reference

```bash
# Day-to-day work: commit to dev
git add -A && git commit -m "feat: whatever" && git push private dev

# Release canary (merges dev→main, tags, builds, uploads)
/release canary 0.6.0

# Release to everyone
/release stable 0.6.0

# Emergency local build when CI is down
/release canary/dev local 0.6.0

# Just the version bump (no release)
/nose 0.6.0
```
