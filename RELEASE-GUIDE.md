# /release Skill

```
/release <channels> [local|remote] <version>
```

## Channels (slash-separate for multiple)

| Input | Where it goes |
|-------|---------------|
| `stable` | Public repo — triggers package managers |
| `canary` | Canary repo — bleeding edge |
| `dev` | Private repo — from dev branch |
| `canary/dev` | Both canary + dev |
| `stable/canary/dev` | All three |

## Build mode (optional, default: remote)

| Input | What it does |
|-------|--------------|
| `local` | Builds on this machine, uploads via gh |
| `remote` | Triggers GitHub Actions CI |

## Examples

```
/release canary 0.6.0
/release canary/dev local 0.5.1
/release stable 0.6.0
/release stable/canary/dev 1.0.0
```

## Platform re-triggers (no version bump, just rebuild)

```
/release windows
/release linux
/release macos
/release all
```

## What happens automatically

- Commits uncommitted work to dev
- Merges dev into main (for canary/stable)
- Bumps version everywhere
- Builds, tests, two approval gates
- Tags, pushes, uploads artifacts to target repos
- Syncs dev back ahead of main
