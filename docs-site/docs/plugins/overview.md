# Plugins

Uprooted supports two types of plugins:

## C# Hook Plugins (Avalonia-native)

These run inside Root's .NET process and manipulate the Avalonia visual tree directly. They have full access to Root's UI controls.

Built-in plugins: Theme Engine, ClearURLs, Link Embeds, Message Logger, Silent Typing, NSFW Filter, Rootcord, Translate, Who Reacted, User Bio, Presence Beacon.

## TypeScript Plugins (Browser-side)

These run inside the DotNetBrowser Chromium context. They can modify WebRTC sub-apps and intercept the JS-to-native bridge.

Built-in plugins: Sentry Blocker, Settings Panel, Theme CSS.

## Plugin Architecture

See [API Reference](api-reference.md) for the plugin API surface and [Getting Started](getting-started.md) for a tutorial.
