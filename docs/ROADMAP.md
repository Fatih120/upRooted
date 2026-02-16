# Roadmap

Project direction, known issues, and planned features for Uprooted.

> **Related docs:** [Index](INDEX.md) | [Architecture](ARCHITECTURE.md) | [Contributing](../CONTRIBUTING.md)

---

## Known Issues

Issues are organized by severity. Critical issues block core functionality; high issues risk breakage on upstream updates; medium issues affect reliability or developer experience.

### Critical

**System.Text.Json broken in profiler context**
`System.Text.Json` causes `MissingMethodException` when used inside the CLR profiler-injected hook. This prevents JSON deserialization in the C# layer, so the hook cannot read `uprooted-settings.json`. Native Avalonia UI (themes page, plugins page) returns hardcoded defaults only and cannot reflect actual user configuration. Settings persistence from the C# side is currently disabled.
- Files: `hook/UprootedSettings.cs`
- Workaround in progress: manual INI-style parsing (partially implemented)

**Environment variables affect all .NET apps**
CLR profiler environment variables (`CORECLR_ENABLE_PROFILING`, `CORECLR_PROFILER`, etc.) are user-scoped and persistent. They apply to every .NET process the user launches, not just Root. The profiler has a process name guard that returns `E_FAIL` for non-Root processes, but other .NET apps still incur a slight startup overhead from the profiler loading and unloading.
- Files: `installer/src-tauri/src/hook.rs`, install scripts
- Mitigation: Process name guard in `tools/uprooted_profiler.c`

### High

**AvaloniaReflection brittleness**
The reflection cache (`hook/AvaloniaReflection.cs`, ~815 lines) assumes specific Avalonia type names, property names, and method signatures. Any Avalonia version update that renames or removes types will break all UI injection. The entire C# layer becomes non-functional if the reflection cache cannot resolve its targets.
- Files: `hook/AvaloniaReflection.cs`
- Recommendation: Add Avalonia version detection and per-feature graceful degradation

**Settings page text-based detection fragile**
The sidebar injector locates the settings page by searching the visual tree for the exact text "APP SETTINGS". If Root renames this label, native UI injection silently fails with no error visible to the user.
- Files: `hook/VisualTreeWalker.cs`, `hook/SidebarInjector.cs`
- Recommendation: Add structural fallback detection that does not depend on text anchors

### Medium

**Settings panel DOM discovery fragile**
Browser-side settings panel injection uses TreeWalker text matching and multiple fallback strategies (flex-row detection, grid detection, sibling size detection). If Root changes both the primary selectors and all fallbacks, injection fails silently. No distinction is made between "not on settings page" and "on settings page but structure unrecognized."
- Files: `src/plugins/settings-panel/panel.ts`

**XHR redirect to about:blank**
Sentry XHR blocking redirects intercepted requests to `about:blank`, which causes browser warnings and leaves the XMLHttpRequest in an undefined state. Technically succeeds but is semantically incorrect.
- Files: `src/plugins/sentry-blocker/index.ts`

**MutationObserver debounce timing**
Settings panel injection uses an 80ms debounce on MutationObserver callbacks. Rapid DOM mutations during Root's page load can still trigger repeated injection attempts, causing visible lag.
- Files: `src/plugins/settings-panel/panel.ts`

**`after` patch handler not implemented**
The `after` callback is defined in the `Patch` type interface but the plugin loader does not invoke it at runtime. Plugins defining `after` handlers get no behavior -- the handler is silently ignored.
- Files: `src/types/plugin.ts`, `src/core/pluginLoader.ts`

**Browser-side settings not persisted to disk**
Runtime changes made via the browser-side settings panel only update the in-memory `window.__UPROOTED_SETTINGS__` object. The browser layer (running in DotNetBrowser's incognito Chromium) has no file system access to write changes back. Settings changed at runtime take effect immediately but revert on Root restart. The installer/CLI can write settings to disk, so initial configuration persists -- only runtime UI changes are session-only.
- Files: `src/plugins/settings-panel/`, `src/core/settings.ts`

**Shallow settings merge**
Settings loading merges user settings with defaults using a shallow object spread. Nested objects (like `plugins`) get completely replaced rather than merged. If a user's saved settings contain only some plugin entries, other plugins lose their defaults.
- Files: `src/core/settings.ts`

---

## Short-term Goals

Next release. Focused on completing core functionality that is currently stubbed or broken.

### Fix C# settings persistence
Replace `System.Text.Json` usage with manual INI-style parsing in the hook layer. Partial implementation already exists in `hook/UprootedSettings.cs`. This unblocks the native Avalonia settings pages from reflecting actual user configuration.

### Implement theme click handlers in native UI
The native Avalonia Themes page shows themes with "ACTIVE" badges but has no click handlers. Add theme selection behavior so users can switch themes from the native settings UI.
- Files: `hook/ContentPages.cs`, `hook/ThemeEngine.cs`

### Add plugin toggle functionality in native UI
The native Plugins page lists discovered plugins but cannot toggle them on or off. Wire up toggle controls to the settings persistence layer.
- Files: `hook/ContentPages.cs`, `hook/UprootedSettings.cs`

### Implement `after` patch handler
The plugin loader currently ignores `after` callbacks defined in plugin patches. Implement post-execution invocation so plugins can observe return values and side effects.
- Files: `src/core/pluginLoader.ts`, `src/types/plugin.ts`

### Implement deep merge for settings
Replace the shallow spread merge in settings loading with a recursive merge that correctly combines nested objects (especially the `plugins` map).
- Files: `src/core/settings.ts`

---

## Medium-term Goals

Next 2-3 releases. Expanding the platform and improving the developer/user experience.

### Plugin marketplace / repository
A discoverable listing of community plugins. Could be a static registry (JSON manifest in a GitHub repo) or a simple web interface. Plugin authors would submit metadata; users would browse and install from within Uprooted.

### Auto-update mechanism
Detect new Uprooted versions and offer in-app updates. Needs to handle hook DLL replacement while Root is not running, HTML re-patching, and TypeScript bundle updates.

### Linux support improvements
The standalone bash installer (`install-uprooted-linux.sh`) covers basic installation. Improve detection reliability, test across distributions, and ensure the native profiler (`tools/uprooted_profiler_linux.c`) handles Linux-specific edge cases.

### Error recovery in plugin lifecycle
If `plugin.start()` rejects, the plugin is currently logged as errored but may be left in an inconsistent state. Add rollback logic and a `starting`/`stopping` guard set to prevent race conditions from rapid start/stop calls.
- Files: `src/core/pluginLoader.ts`

### Bridge version detection
Add negotiation between Uprooted and Root's bridge interface (`__nativeToWebRtc`, `__webRtcToNative`). Detect bridge version at runtime and warn or degrade gracefully if the interface has changed.

---

## Long-term Vision

Bigger-picture goals that define where the project is headed.

### Panel rearrangement / layout customization
Allow users to rearrange Root's UI panels: move the server/community list to the left or right side, reposition the notifications panel, adjust top/bottom placement of various UI elements. This would require deep DOM manipulation of Root's Avalonia-hosted Chromium layout and likely a dedicated layout engine plugin. The C# hook layer may also need modifications to support native panel repositioning.
- Originally suggested by MrEizy

### Full settings bidirectional sync
Bridge the gap between browser-side settings changes and disk persistence. Either expose a write channel from the browser layer through the C# hook (via the native bridge), or implement a periodic sync mechanism. Goal: runtime settings changes survive Root restarts.

### Version compatibility matrix
Maintain a documented mapping of which Uprooted versions support which Root versions. Add runtime version detection so Uprooted can warn users when running against an untested Root version.

### Community plugin ecosystem
Beyond the marketplace, build out tooling for plugin authors: a CLI scaffolding tool, automated testing harness against a mock Root environment, documentation generator from plugin metadata, and a contribution pipeline for vetted community plugins.

### Sentry blocker hardening
Expand coverage to handle additional transport mechanisms (WebSocket, image beacons) that future Sentry SDK versions may introduce. Consider intercepting at the `Sentry.init()` level for more comprehensive blocking.

---

## Feature Requests

Have an idea for Uprooted? We track feature requests as GitHub issues.

- **Suggest a feature:** Open a [feature request](https://github.com/watchthelight/uprooted/issues/new?template=feature-request.yml)
- **Report a bug:** Open a [bug report](https://github.com/watchthelight/uprooted/issues/new?template=bug-report.yml)
- **Discuss ideas:** Start a conversation in [GitHub Discussions](https://github.com/watchthelight/uprooted/discussions) (if enabled)

When suggesting a feature, include:
- A clear description of the behavior you want
- Why it would be useful (what problem does it solve?)
- Any technical considerations you are aware of
- Whether you would be willing to contribute an implementation

---

*Last updated: 2026-02-16*
