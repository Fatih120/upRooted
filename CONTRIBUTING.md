# Contributing to Uprooted

Thanks for your interest in contributing! Here's how to get started.

## Status

Uprooted is currently **awaiting approval** from Root's developers. We're accepting contributions to the framework scaffold, type definitions, documentation, and the landing page. We are **not** accepting contributions that distribute working injection code until approval is granted.

## Development Setup

```bash
git clone https://github.com/watchthelight/uprooted.git
cd uprooted
pnpm install
pnpm build
```

## Code Style

- TypeScript strict mode
- ES modules (`import`/`export`)
- No default exports except for plugin definitions
- Use descriptive variable names — no abbreviations

## Pull Requests

1. Fork the repository and create a branch from `main`
2. If you've added code, add or update types accordingly
3. Make sure `pnpm build` succeeds without errors
4. Write a clear PR description explaining what changed and why
5. Link any related issues

## Reporting Bugs

Use the [bug report template](https://github.com/watchthelight/uprooted/issues/new?template=bug-report.yml) on GitHub.

## Suggesting Features

Use the [feature request template](https://github.com/watchthelight/uprooted/issues/new?template=feature-request.yml) on GitHub.

## License

By contributing, you agree that your contributions will be licensed under the GPL-3.0 license.
