# Uprooted Documentation Index

Navigation hub for all Uprooted documentation. Use this page to find the right document for your goal.

> **AI contributors:** Use [`NEW-SESSION.md`](../NEW-SESSION.md) § Task Dispatch Table instead of this file for task-specific doc routing.

> **Related docs:** [Architecture](framework/ARCHITECTURE.md) | [How It Works](HOW_IT_WORKS.md) | [Contributing](../CONTRIBUTING.md)

---

## Documentation Map

| Document                   | Path                                                                                    | Description                                                                                                                  | Audience                               |
| -------------------------- | --------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------- | -------------------------------------- |
| README                     | [`README.md`](../README.md)                                                             | Repository landing page, feature list, terms of use                                                                          | Everyone                               |
| Installation Guide         | [`docs/install/INSTALLATION.md`](install/INSTALLATION.md)                               | End-user install and uninstall instructions                                                                                  | Users                                  |
| How It Works               | [`docs/HOW_IT_WORKS.md`](HOW_IT_WORKS.md)                                               | Narrative walkthrough from reverse engineering to injected UI                                                                | All developers                         |
| Architecture               | [`docs/framework/ARCHITECTURE.md`](framework/ARCHITECTURE.md)                           | System architecture, layer boundaries, and design decisions                                                                  | Framework contributors                 |
| Hook Reference             | [`docs/framework/HOOK_REFERENCE.md`](framework/HOOK_REFERENCE.md)                       | C# .NET hook layer deep dive (Avalonia reflection, sidebar injection, startup phases)                                        | Framework contributors                 |
| TypeScript Reference       | [`docs/framework/TYPESCRIPT_REFERENCE.md`](framework/TYPESCRIPT_REFERENCE.md)           | TypeScript browser injection layer (plugin runtime, theme engine, bridge proxies)                                            | Plugin authors, framework contributors |
| CLR Profiler               | [`docs/framework/CLR_PROFILER.md`](framework/CLR_PROFILER.md)                           | Native C profiler DLL (IL injection, environment variables, attach flow)                                                     | Framework contributors                 |
| Installer Reference        | [`docs/framework/INSTALLER.md`](framework/INSTALLER.md)                                 | Console TUI installer (detection, patching, file deployment)                                                                 | Framework contributors                 |
| Build Guide                | [`docs/install/BUILD.md`](install/BUILD.md)                                             | Build pipeline for all layers (C# hook, TypeScript bundle, Rust installer)                                                   | Contributors                           |
| Tasks                      | [`TASKS.md`](../TASKS.md)                                                               | Active task board — pick up work, track progress                                                                             | Contributors                           |
| Roadmap                    | [`docs/ROADMAP.md`](ROADMAP.md)                                                         | Known issues, planned features, and future direction                                                                         | Developers, insiders                   |
| Plugin Roadmap             | [`docs/PLUGIN_ROADMAP.md`](PLUGIN_ROADMAP.md)                                           | Planned plugins with architecture notes and implementation strategies                                                        | Developers, plugin authors             |
| Next Release               | [`NEXT-RELEASE.md`](../NEXT-RELEASE.md)                                                 | Changes since last release (replaced each release)                                                                           | Contributors                           |
| Changelog (public)         | [`CHANGELOG_PUBLIC.md`](../CHANGELOG_PUBLIC.md)                                         | GitHub release notes mirror (v0.1.6 — present)                                                                               | Everyone                               |
| Changelog (internal)       | [`CHANGELOG.md`](../CHANGELOG.md)                                                       | Detailed Keep a Changelog format                                                                                             | Contributors                           |
| Contributing               | [`CONTRIBUTING.md`](../CONTRIBUTING.md)                                                 | Branch rules, PR process, code style, and contribution guidelines                                                            | Contributors                           |
| AI Contributor Guide       | [`CLAUDE.md`](../CLAUDE.md)                                                             | Guidance for AI-assisted development sessions                                                                                | AI contributors                        |
| Plugin Quickstart          | [`docs/plugins/GETTING_STARTED.md`](plugins/GETTING_STARTED.md)                         | Plugin development quickstart with first plugin tutorial                                                                     | Plugin authors                         |
| Plugin API Reference       | [`docs/plugins/API_REFERENCE.md`](plugins/API_REFERENCE.md)                             | Full plugin API surface (lifecycle hooks, settings, storage)                                                                 | Plugin authors                         |
| Bridge Reference           | [`docs/plugins/BRIDGE_REFERENCE.md`](plugins/BRIDGE_REFERENCE.md)                       | Root bridge API (IPC proxy methods, interceptors, call patterns)                                                             | Plugin authors                         |
| Root Environment           | [`docs/plugins/ROOT_ENVIRONMENT.md`](plugins/ROOT_ENVIRONMENT.md)                       | Root app internals (Chromium context, DOM structure, CSS variables)                                                          | Plugin authors                         |
| Plugin Examples            | [`docs/plugins/EXAMPLES.md`](plugins/EXAMPLES.md)                                       | Annotated example plugins covering common patterns                                                                           | Plugin authors                         |
| Theme Engine Deep Dive     | [`docs/framework/THEME_ENGINE_DEEP_DIVE.md`](framework/THEME_ENGINE_DEEP_DIVE.md)       | ThemeEngine algorithm deep dive (live preview, color audit, revert)                                                          | Framework contributors                 |
| Avalonia Patterns          | [`docs/framework/AVALONIA_PATTERNS.md`](framework/AVALONIA_PATTERNS.md)                 | Avalonia UI concepts through Uprooted's reflection-only lens                                                                 | Framework contributors                 |
| LiquidGlass Draw Skeleton  | [`docs/framework/LIQUID_GLASS_CUSTOM_DRAW_SKELETON.md`](framework/LIQUID_GLASS_CUSTOM_DRAW_SKELETON.md) | Typed Avalonia 11 + Skia custom draw skeleton for LiquidGlass border stroke (`DrawingContext.Custom` + `ICustomDrawOperation`) | Framework contributors                 |
| Root Control Reference     | [`docs/framework/ROOT_CONTROL_REFERENCE.md`](framework/ROOT_CONTROL_REFERENCE.md)       | Root's custom controls, style classes, message view structure, theme mechanics, DataStore keys — from ILSpy decompilation    | Framework contributors                 |
| .NET Runtime               | [`docs/framework/DOTNET_RUNTIME.md`](framework/DOTNET_RUNTIME.md)                       | CLR profiler, IL injection, assembly scanning, startup hooks                                                                 | Framework contributors                 |
| Root Internals             | [`docs/research/ROOT_INTERNALS.md`](research/ROOT_INTERNALS.md)                         | Root's native architecture from reverse engineering                                                                          | Framework contributors                 |
| gRPC Protocol              | [`docs/research/GRPC_PROTOCOL.md`](research/GRPC_PROTOCOL.md)                           | Complete gRPC-web protocol reference (27 services)                                                                           | Security researchers                   |
| Reverse Engineering        | [`docs/research/REVERSE_ENGINEERING.md`](research/REVERSE_ENGINEERING.md)               | RE methodology: source maps, binary analysis, protocol discovery                                                             | Security researchers                   |
| Security Research          | [`docs/research/SECURITY_RESEARCH.md`](research/SECURITY_RESEARCH.md)                   | 105 security findings structured by category                                                                                 | Security researchers                   |
| Root Theme System Findings | [`research/ROOT_THEME_SYSTEM_FINDINGS.md`](../research/ROOT_THEME_SYSTEM_FINDINGS.md)   | Root's 32-key native Avalonia color system (ILSpy decompilation)                                                             | Framework contributors                 |
| ILSpy Dump Index           | [`research/ILSPY_DUMP_INDEX.md`](../research/ILSPY_DUMP_INDEX.md)                       | Master index for 219 ILSpy decompiled files (56 analyzed, 163 pending)                                                       | Framework contributors                 |
| Research Index             | [`docs/research/RESEARCH_INDEX.md`](research/RESEARCH_INDEX.md)                         | Navigation guide for research/ directory                                                                                     | Security researchers                   |
| Mitigation Countermeasures | [`docs/research/MITIGATION_COUNTERMEASURES.md`](research/MITIGATION_COUNTERMEASURES.md) | Counter-strategies for all five proposed Root mitigations (CLR kill switch, HTML integrity, `file://`, CSP, signed launcher) | Security researchers, contributors     |
| Planning Reference         | [`docs/dev/PLANNING_REFERENCE.md`](dev/PLANNING_REFERENCE.md)                           | Index of .planning/codebase/ analysis files                                                                                  | Contributors                           |
| Contributing Technical     | [`docs/dev/CONTRIBUTING_TECHNICAL.md`](dev/CONTRIBUTING_TECHNICAL.md)                   | Technical onboarding: dev environment, debugging, failure modes                                                              | Contributors                           |
| Testing Guide              | [`docs/dev/TESTING.md`](dev/TESTING.md)                                                 | Unit tests, Docker sandboxes, stubs, coverage, adding new tests                                                              | Contributors                           |
| gRPC Library Reference     | [`docs/research/GRPC_LIB_REFERENCE.md`](research/GRPC_LIB_REFERENCE.md)                 | API reference for grpc_lib.py encoding/decoding library                                                                      | Security researchers                   |
| Advanced Plugin Dev        | [`docs/plugins/ADVANCED_DEVELOPMENT.md`](plugins/ADVANCED_DEVELOPMENT.md)               | Deep plugin patterns: bridge chains, performance, error recovery                                                             | Plugin authors                         |
| Plugin Contribution Guide  | [`docs/plugins/CONTRIBUTING_PLUGINS.md`](plugins/CONTRIBUTING_PLUGINS.md)               | Fork-to-PR workflow for contributing plugins                                                                                 | Plugin authors                         |
| Built-in Plugins Index     | [`docs/plugins/builtin/INDEX.md`](plugins/builtin/INDEX.md)                             | Overview of 7 built-in plugins (TypeScript + C# hook)                                                                        | Everyone                               |
| Sentry Blocker             | [`docs/plugins/builtin/sentry-blocker.md`](plugins/builtin/sentry-blocker.md)           | Privacy plugin: blocks Sentry telemetry                                                                                      | Everyone                               |
| Themes                     | [`docs/plugins/builtin/themes.md`](plugins/builtin/themes.md)                           | CSS variable theme engine with presets and custom colors                                                                     | Everyone                               |
| Settings Panel             | [`docs/plugins/builtin/settings-panel.md`](plugins/builtin/settings-panel.md)           | In-app settings UI injected into Root's sidebar                                                                              | Everyone                               |
| Link Embeds                | [`docs/plugins/builtin/link-embeds.md`](plugins/builtin/link-embeds.md)                 | Discord-style rich link previews and YouTube embeds                                                                          | Everyone                               |
| Message Logger             | [`docs/plugins/builtin/message-logger.md`](plugins/builtin/message-logger.md)           | Logs deleted messages with red visual indicators (C# hook; deletion working, edit detection WIP)                             | Everyone                               |
| Archives                   | [`docs/archives/`](archives/)                                                           | Miscellaneous notes, one-off fixes, and historical context                                                                   | Contributors                           |
| AI Session Onboarding      | [`NEW-SESSION.md`](../NEW-SESSION.md)                                                   | Context-efficient AI agent onboarding reference card                                                                         | AI contributors                        |

---

## Reading Paths

### Install Uprooted

For users who want to install and use Uprooted.

1. [Installation Guide](install/INSTALLATION.md) -- install, configure, and verify
2. [Build Guide](install/BUILD.md) -- optional, only if building from source

### Write a Plugin

For developers who want to build plugins for Uprooted.

1. [Plugin Quickstart](plugins/GETTING_STARTED.md) -- scaffold your first plugin
2. [Plugin API Reference](plugins/API_REFERENCE.md) -- lifecycle hooks, settings, storage
3. [Bridge Reference](plugins/BRIDGE_REFERENCE.md) -- intercept and extend Root's IPC bridge
4. [Root Environment](plugins/ROOT_ENVIRONMENT.md) -- DOM structure, CSS variables, Chromium context
5. [Plugin Examples](plugins/EXAMPLES.md) -- annotated real-world patterns
6. [Built-in Plugins](plugins/builtin/INDEX.md) -- study the built-in plugins as reference implementations
7. [Plugin Contribution Guide](plugins/CONTRIBUTING_PLUGINS.md) -- fork-to-PR workflow for submitting your plugin
8. [Plugin Roadmap](PLUGIN_ROADMAP.md) -- planned plugins and their implementation strategies

### Contribute to the Framework

For developers who want to work on Uprooted itself.

1. [Architecture](framework/ARCHITECTURE.md) -- layer boundaries, data flow, design decisions
2. [Hook Reference](framework/HOOK_REFERENCE.md) -- C# .NET hook internals
3. [TypeScript Reference](framework/TYPESCRIPT_REFERENCE.md) -- browser injection internals
4. [CLR Profiler](framework/CLR_PROFILER.md) -- native C profiler DLL
5. [Build Guide](install/BUILD.md) -- build all layers from source
6. [Contributing](../CONTRIBUTING.md) -- branch rules, PR process, code style

### Understand Everything

For those who want the complete technical picture.

1. [How It Works](HOW_IT_WORKS.md) -- full narrative, from reverse engineering to running mod
2. [Architecture](framework/ARCHITECTURE.md) -- system design and layer interactions
3. [Hook Reference](framework/HOOK_REFERENCE.md) -- C# hook deep dive
4. [TypeScript Reference](framework/TYPESCRIPT_REFERENCE.md) -- browser injection deep dive
5. [CLR Profiler](framework/CLR_PROFILER.md) -- native profiler internals
6. [Installer Reference](framework/INSTALLER.md) -- Console TUI installer internals
7. [Roadmap](ROADMAP.md) -- where the project is headed
8. [Plugin Roadmap](PLUGIN_ROADMAP.md) -- planned plugin designs and implementation strategies

### Security Research

For researchers investigating Root's security posture.

1. [Security Research](research/SECURITY_RESEARCH.md) -- structured findings by category
2. [Reverse Engineering](research/REVERSE_ENGINEERING.md) -- methodology and tools
3. [gRPC Protocol](research/GRPC_PROTOCOL.md) -- backend protocol specification
4. [gRPC Library Reference](research/GRPC_LIB_REFERENCE.md) -- Python library for gRPC calls
5. [Research Index](research/RESEARCH_INDEX.md) -- full file inventory

### Root Internals

For understanding Root's application architecture.

1. [Root Internals](research/ROOT_INTERNALS.md) -- native process architecture
2. [Root Environment](plugins/ROOT_ENVIRONMENT.md) -- browser-side view
3. [gRPC Protocol](research/GRPC_PROTOCOL.md) -- backend communication
4. [Reverse Engineering](research/REVERSE_ENGINEERING.md) -- how it was discovered

### Dev Environment

For setting up a development environment.

1. [Contributing Technical](dev/CONTRIBUTING_TECHNICAL.md) -- setup, debugging, failure modes
2. [Build Guide](install/BUILD.md) -- build pipeline for all layers
3. [Contributing](../CONTRIBUTING.md) -- branch rules and PR process
4. [Planning Reference](dev/PLANNING_REFERENCE.md) -- technical debt and known issues
