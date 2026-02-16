# Uprooted Documentation Index

Navigation hub for all Uprooted documentation. Use this page to find the right document for your goal.

> **Related docs:** [Architecture](ARCHITECTURE.md) | [How It Works](HOW_IT_WORKS.md) | [Contributing](../CONTRIBUTING.md)

---

## Documentation Map

| Document | Path | Description | Audience |
|----------|------|-------------|----------|
| README | [`README.md`](../README.md) | Repository landing page, feature list, terms of use | Everyone |
| Installation Guide | [`docs/INSTALLATION.md`](INSTALLATION.md) | End-user install and uninstall instructions | Users |
| How It Works | [`docs/HOW_IT_WORKS.md`](HOW_IT_WORKS.md) | Narrative walkthrough from reverse engineering to injected UI | All developers |
| Architecture | [`docs/ARCHITECTURE.md`](ARCHITECTURE.md) | System architecture, layer boundaries, and design decisions | Framework contributors |
| Hook Reference | [`docs/HOOK_REFERENCE.md`](HOOK_REFERENCE.md) | C# .NET hook layer deep dive (Avalonia reflection, sidebar injection, startup phases) | Framework contributors |
| TypeScript Reference | [`docs/TYPESCRIPT_REFERENCE.md`](TYPESCRIPT_REFERENCE.md) | TypeScript browser injection layer (plugin runtime, theme engine, bridge proxies) | Plugin authors, framework contributors |
| CLR Profiler | [`docs/CLR_PROFILER.md`](CLR_PROFILER.md) | Native C profiler DLL (IL injection, environment variables, attach flow) | Framework contributors |
| Installer Reference | [`docs/INSTALLER.md`](INSTALLER.md) | Tauri/Rust installer (detection, patching, file deployment) | Framework contributors |
| Build Guide | [`docs/BUILD.md`](BUILD.md) | Build pipeline for all layers (C# hook, TypeScript bundle, Rust installer) | Contributors |
| Roadmap | [`docs/ROADMAP.md`](ROADMAP.md) | Known issues, planned features, and future direction | Everyone |
| Contributing | [`CONTRIBUTING.md`](../CONTRIBUTING.md) | Branch rules, PR process, code style, and contribution guidelines | Contributors |
| AI Contributor Guide | [`CLAUDE.md`](../CLAUDE.md) | Guidance for AI-assisted development sessions | AI contributors |
| Plugin Quickstart | [`docs/plugins/GETTING_STARTED.md`](plugins/GETTING_STARTED.md) | Plugin development quickstart with first plugin tutorial | Plugin authors |
| Plugin API Reference | [`docs/plugins/API_REFERENCE.md`](plugins/API_REFERENCE.md) | Full plugin API surface (lifecycle hooks, settings, storage) | Plugin authors |
| Bridge Reference | [`docs/plugins/BRIDGE_REFERENCE.md`](plugins/BRIDGE_REFERENCE.md) | Root bridge API (IPC proxy methods, interceptors, call patterns) | Plugin authors |
| Root Environment | [`docs/plugins/ROOT_ENVIRONMENT.md`](plugins/ROOT_ENVIRONMENT.md) | Root app internals (Chromium context, DOM structure, CSS variables) | Plugin authors |
| Plugin Examples | [`docs/plugins/EXAMPLES.md`](plugins/EXAMPLES.md) | Annotated example plugins covering common patterns | Plugin authors |
| Theme Engine Deep Dive | [`docs/THEME_ENGINE_DEEP_DIVE.md`](THEME_ENGINE_DEEP_DIVE.md) | ThemeEngine algorithm deep dive (live preview, color audit, revert) | Framework contributors |
| Avalonia Patterns | [`docs/AVALONIA_PATTERNS.md`](AVALONIA_PATTERNS.md) | Avalonia UI concepts through Uprooted's reflection-only lens | Framework contributors |
| .NET Runtime | [`docs/DOTNET_RUNTIME.md`](DOTNET_RUNTIME.md) | CLR profiler, IL injection, assembly scanning, startup hooks | Framework contributors |
| Root Internals | [`docs/ROOT_INTERNALS.md`](ROOT_INTERNALS.md) | Root's native architecture from reverse engineering | Framework contributors |
| gRPC Protocol | [`docs/GRPC_PROTOCOL.md`](GRPC_PROTOCOL.md) | Complete gRPC-web protocol reference (27 services) | Security researchers |
| Reverse Engineering | [`docs/REVERSE_ENGINEERING.md`](REVERSE_ENGINEERING.md) | RE methodology: source maps, binary analysis, protocol discovery | Security researchers |
| Security Research | [`docs/SECURITY_RESEARCH.md`](SECURITY_RESEARCH.md) | 105 security findings structured by category | Security researchers |
| Research Index | [`docs/RESEARCH_INDEX.md`](RESEARCH_INDEX.md) | Navigation guide for research/ directory | Security researchers |
| Planning Reference | [`docs/PLANNING_REFERENCE.md`](PLANNING_REFERENCE.md) | Index of .planning/codebase/ analysis files | Contributors |
| Contributing Technical | [`docs/CONTRIBUTING_TECHNICAL.md`](CONTRIBUTING_TECHNICAL.md) | Technical onboarding: dev environment, debugging, failure modes | Contributors |
| gRPC Library Reference | [`docs/GRPC_LIB_REFERENCE.md`](GRPC_LIB_REFERENCE.md) | API reference for grpc_lib.py encoding/decoding library | Security researchers |
| Advanced Plugin Dev | [`docs/plugins/ADVANCED_DEVELOPMENT.md`](plugins/ADVANCED_DEVELOPMENT.md) | Deep plugin patterns: bridge chains, performance, error recovery | Plugin authors |
| AI Session Onboarding | [`NEW-SESSION.md`](../NEW-SESSION.md) | Context-efficient AI agent onboarding reference card | AI contributors |

---

## Reading Paths

### Install Uprooted

For users who want to install and use Uprooted.

1. [Installation Guide](INSTALLATION.md) -- install, configure, and verify
2. [Build Guide](BUILD.md) -- optional, only if building from source

### Write a Plugin

For developers who want to build plugins for Uprooted.

1. [Plugin Quickstart](plugins/GETTING_STARTED.md) -- scaffold your first plugin
2. [Plugin API Reference](plugins/API_REFERENCE.md) -- lifecycle hooks, settings, storage
3. [Bridge Reference](plugins/BRIDGE_REFERENCE.md) -- intercept and extend Root's IPC bridge
4. [Root Environment](plugins/ROOT_ENVIRONMENT.md) -- DOM structure, CSS variables, Chromium context
5. [Plugin Examples](plugins/EXAMPLES.md) -- annotated real-world patterns

### Contribute to the Framework

For developers who want to work on Uprooted itself.

1. [Architecture](ARCHITECTURE.md) -- layer boundaries, data flow, design decisions
2. [Hook Reference](HOOK_REFERENCE.md) -- C# .NET hook internals
3. [TypeScript Reference](TYPESCRIPT_REFERENCE.md) -- browser injection internals
4. [CLR Profiler](CLR_PROFILER.md) -- native C profiler DLL
5. [Build Guide](BUILD.md) -- build all layers from source
6. [Contributing](../CONTRIBUTING.md) -- branch rules, PR process, code style

### Understand Everything

For those who want the complete technical picture.

1. [How It Works](HOW_IT_WORKS.md) -- full narrative, from reverse engineering to running mod
2. [Architecture](ARCHITECTURE.md) -- system design and layer interactions
3. [Hook Reference](HOOK_REFERENCE.md) -- C# hook deep dive
4. [TypeScript Reference](TYPESCRIPT_REFERENCE.md) -- browser injection deep dive
5. [CLR Profiler](CLR_PROFILER.md) -- native profiler internals
6. [Installer Reference](INSTALLER.md) -- Tauri installer internals
7. [Roadmap](ROADMAP.md) -- where the project is headed

### Security Research

For researchers investigating Root's security posture.

1. [Security Research](SECURITY_RESEARCH.md) -- structured findings by category
2. [Reverse Engineering](REVERSE_ENGINEERING.md) -- methodology and tools
3. [gRPC Protocol](GRPC_PROTOCOL.md) -- backend protocol specification
4. [gRPC Library Reference](GRPC_LIB_REFERENCE.md) -- Python library for gRPC calls
5. [Research Index](RESEARCH_INDEX.md) -- full file inventory

### Root Internals

For understanding Root's application architecture.

1. [Root Internals](ROOT_INTERNALS.md) -- native process architecture
2. [Root Environment](plugins/ROOT_ENVIRONMENT.md) -- browser-side view
3. [gRPC Protocol](GRPC_PROTOCOL.md) -- backend communication
4. [Reverse Engineering](REVERSE_ENGINEERING.md) -- how it was discovered

### Dev Environment

For setting up a development environment.

1. [Contributing Technical](CONTRIBUTING_TECHNICAL.md) -- setup, debugging, failure modes
2. [Build Guide](BUILD.md) -- build pipeline for all layers
3. [Contributing](../CONTRIBUTING.md) -- branch rules and PR process
4. [Planning Reference](PLANNING_REFERENCE.md) -- technical debt and known issues

---

## Quick-Reference Topic Finder

| Topic | Document | Section |
|-------|----------|---------|
| Avalonia patterns | [Avalonia Patterns](AVALONIA_PATTERNS.md) | -- |
| Avalonia reflection | [Hook Reference](HOOK_REFERENCE.md) | Avalonia reflection cache |
| Avalonia visual tree | [Hook Reference](HOOK_REFERENCE.md) | Visual tree traversal |
| Branch rules | [Contributing](../CONTRIBUTING.md) | Branch Rules |
| Bridge API (IPC) | [Bridge Reference](plugins/BRIDGE_REFERENCE.md) | -- |
| Bridge proxies | [TypeScript Reference](TYPESCRIPT_REFERENCE.md) | Bridge proxy system |
| Build pipeline | [Build Guide](BUILD.md) | -- |
| Chromium context | [Root Environment](plugins/ROOT_ENVIRONMENT.md) | Chromium context |
| CLR profiler attach | [CLR Profiler](CLR_PROFILER.md) | Attach flow |
| CLR profiler concepts | [.NET Runtime](DOTNET_RUNTIME.md) | CLR Profiler API |
| Color utilities | [TypeScript Reference](TYPESCRIPT_REFERENCE.md) | Color utilities |
| Content pages | [Hook Reference](HOOK_REFERENCE.md) | Content pages |
| CSS variables | [Root Environment](plugins/ROOT_ENVIRONMENT.md) | CSS variables |
| Custom themes | [Theme Engine Deep Dive](THEME_ENGINE_DEEP_DIVE.md) | Custom theme generation |
| Debugging the hook | [Contributing Technical](CONTRIBUTING_TECHNICAL.md) | Debugging the C# Hook |
| Dev environment setup | [Contributing Technical](CONTRIBUTING_TECHNICAL.md) | Development Environment |
| .NET runtime | [.NET Runtime](DOTNET_RUNTIME.md) | -- |
| DOM structure | [Root Environment](plugins/ROOT_ENVIRONMENT.md) | DOM structure |
| Environment variables | [CLR Profiler](CLR_PROFILER.md) | Environment variables |
| File deployment | [Installer Reference](INSTALLER.md) | File deployment |
| gRPC library | [gRPC Library Reference](GRPC_LIB_REFERENCE.md) | -- |
| gRPC protocol | [gRPC Protocol](GRPC_PROTOCOL.md) | -- |
| HTML patching | [Hook Reference](HOOK_REFERENCE.md) | HTML patch verification |
| IL injection | [CLR Profiler](CLR_PROFILER.md) | IL injection |
| Install / uninstall | [Installation Guide](INSTALLATION.md) | -- |
| Installer detection | [Installer Reference](INSTALLER.md) | Root detection |
| Lifecycle hooks | [Plugin API Reference](plugins/API_REFERENCE.md) | Lifecycle hooks |
| Linux installer | [Installation Guide](INSTALLATION.md) | Linux |
| Live theme preview | [Theme Engine Deep Dive](THEME_ENGINE_DEEP_DIVE.md) | Live Preview System |
| Logging | [Hook Reference](HOOK_REFERENCE.md) | Logging |
| Platform paths | [Hook Reference](HOOK_REFERENCE.md) | Platform paths |
| Plugin advanced patterns | [Advanced Plugin Dev](plugins/ADVANCED_DEVELOPMENT.md) | -- |
| Plugin examples | [Plugin Examples](plugins/EXAMPLES.md) | -- |
| Plugin settings | [Plugin API Reference](plugins/API_REFERENCE.md) | Settings |
| Plugin storage | [Plugin API Reference](plugins/API_REFERENCE.md) | Storage |
| Research files | [Research Index](RESEARCH_INDEX.md) | -- |
| Reverse engineering | [Reverse Engineering](REVERSE_ENGINEERING.md) | -- |
| Root architecture | [Root Internals](ROOT_INTERNALS.md) | -- |
| Root detection | [Installer Reference](INSTALLER.md) | Root detection |
| Security findings | [Security Research](SECURITY_RESEARCH.md) | -- |
| Sidebar injection | [Hook Reference](HOOK_REFERENCE.md) | Sidebar injection |
| Startup phases | [Hook Reference](HOOK_REFERENCE.md) | Startup phases |
| Technical debt | [Planning Reference](PLANNING_REFERENCE.md) | Concerns and Technical Debt |
| Theme engine | [TypeScript Reference](TYPESCRIPT_REFERENCE.md) | Theme engine |
| Theme system (Root) | [Root Environment](plugins/ROOT_ENVIRONMENT.md) | Theme system |
