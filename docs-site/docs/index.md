# Uprooted

A client mod framework for [Root Communications](https://root.gg) desktop app.

## What is Uprooted?

Uprooted is a dual-layer injection framework that adds custom themes, plugins, and UI modifications to the Root Communications desktop application. It works through:

1. **C# .NET Hook** — CLR profiler IL-injects into Root.exe, adds native Avalonia sidebar/settings UI via reflection
2. **TypeScript Browser Injection** — Script tags inject into DotNetBrowser Chromium for WebRTC and sub-app modifications

## Features

- **Theme Engine** — Custom color themes with OKLCH palette generation, 8 built-in presets
- **Plugin System** — Modular plugins with lifecycle management
- **Settings UI** — Native Avalonia settings pages injected into Root's settings panel
- **Link Embeds** — Rich link previews for shared URLs
- **Message Logger** — Edit and delete detection with visual indicators
- **ClearURLs** — Strip tracking parameters from URLs
- **Silent Typing** — Prevent typing indicators from being sent
- **Auto-Updater** — Background update checks with encrypted packages

## Quick Start

See the [Installation Guide](getting-started/installation.md) to get started.
