# Roadmap

> **What this is:** Known issues, planned features, and future direction for Uprooted.
> **Read when:** Checking what's planned; understanding known limitations; looking for things to work on.
> **Skip if:** You need the active task board → [TASKS.md](../TASKS.md). You need plugin-specific roadmap → [PLUGIN_ROADMAP.md](PLUGIN_ROADMAP.md).

Project direction, known issues, and planned features for Uprooted.

> **Related docs:** [Index](INDEX.md) | [Architecture](framework/ARCHITECTURE.md) | [Planning Reference](dev/PLANNING_REFERENCE.md) | [Security Research](research/SECURITY_RESEARCH.md) | [Contributing Technical](dev/CONTRIBUTING_TECHNICAL.md)

---

## Known Issues

Issues are organized by severity. Critical issues block core functionality; high issues risk breakage on upstream updates; medium issues affect reliability or developer experience.

### Critical

**System.Text.Json broken in profiler context**
`System.Text.Json` causes `MissingMethodException` when used inside the CLR profiler-injected hook. This prevents JSON deserialization in the C# layer.
- Files: `hook/UprootedSettings.cs`
- Workaround: INI-based `UprootedSettings` (fully implemented). Uses key=value format instead of JSON. Supports theme persistence, plugin toggles, and custom accent/background colors.

**Environment variables affect all .NET apps**
CLR profiler environment variables (`DOTNET_ENABLE_PROFILING`, `DOTNET_PROFILER`, etc. plus legacy `CORECLR_` prefix for .NET 8/9 compatibility) are user-scoped and persistent. They apply to every .NET process the user launches, not just Root. The profiler has a process name guard that returns `E_FAIL` for non-Root processes, but other .NET apps still incur a slight startup overhead from the profiler loading and unloading.
- Files: `installer/src-tauri/src/hook.rs`, install scripts
- Mitigation: Process name guard in `tools/uprooted_profiler.c`

### High

**AvaloniaReflection brittleness**
The reflection cache (`hook/AvaloniaReflection.cs`, ~2383 lines) assumes specific Avalonia type names, property names, and method signatures. Any Avalonia version update that renames or removes types will break all UI injection. The entire C# layer becomes non-functional if the reflection cache cannot resolve its targets.
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

**Browser-side settings not persisted to disk**
Runtime changes made via the browser-side settings panel only update the in-memory `window.__UPROOTED_SETTINGS__` object. The browser layer (running in DotNetBrowser's incognito Chromium) has no file system access to write changes back. Settings changed at runtime take effect immediately but revert on Root restart. The installer/CLI can write settings to disk, so initial configuration persists -- only runtime UI changes are session-only.
- Files: `src/plugins/settings-panel/`, `src/core/settings.ts`

---

## Short-term Goals

Next release. Focused on completing core functionality that is currently stubbed or broken.

### Validate MessageLogger plugin
MessageLogger (shipped WIP in v0.4.2) has working deletion detection (INPC-driven + self-delete fallback), edit detection (dual-strategy EditedAt + grace period), author name resolution, and visual indicators. Card injection positioning (`FindMessageGridInContainer` returns null) is the main open issue.
- Files: `hook/MessageLogger.cs`

### Validate Translate plugin
TranslateEngine (shipped post-v0.4.2) provides DeepL-powered message translation via context menu integration. Deployed but needs real-world validation of language detection, translation quality, and config popup.
- Files: `hook/TranslateEngine.cs`, `hook/TranslateConfigPopup.cs`

### Validate Presence Beacon
UprootedPresenceBeacon (shipped post-v0.4.2) detects other Uprooted users via gRPC metadata injection. Deployed but needs validation.
- Files: `hook/UprootedPresenceBeacon.cs`

---

## Medium-term Goals

Next 2-3 releases. Expanding the platform and improving the developer/user experience.

### Plugin marketplace / repository
A discoverable listing of community plugins. Could be a static registry (JSON manifest in a GitHub repo) or a simple web interface. Plugin authors would submit metadata; users would browse and install from within Uprooted.

### Auto-update mechanism — SHIPPED (v0.4.2)
In-process auto-updater (`hook/AutoUpdater.cs`) checks GitHub releases every 1 minute, downloads a single encrypted `.uprpkg` package, decrypts and unpacks 6 update files, and overwrites them in-place. Developer channel with password-gated access to pre-release builds. Changes take effect on next Root restart.

### Linux support improvements
The standalone bash installer (`install-uprooted-linux.sh`) covers basic installation. Improve detection reliability, test across distributions, and ensure the native profiler (`tools/uprooted_profiler_linux.c`) handles Linux-specific edge cases.

### Error recovery in plugin lifecycle
If `plugin.start()` rejects, the plugin is currently logged as errored but may be left in an inconsistent state. Add rollback logic and a `starting`/`stopping` guard set to prevent race conditions from rapid start/stop calls.
- Files: `src/core/pluginLoader.ts`

### Bridge version detection
Add negotiation between Uprooted and Root's bridge interface (`__nativeToWebRtc`, `__webRtcToNative`). Detect bridge version at runtime and warn or degrade gracefully if the interface has changed.

### LiquidGlass visual mod plugin (iOS 26-inspired)
Build a new visual plugin focused on translucent, depth-aware UI treatment inspired by iOS 26's Liquid Glass aesthetic. Initial scope is decorative surfaces (cards/panels/badges) and accent edge effects, with strict perf limits and instant disable fallback.
- **Technical path validated:** Avalonia custom draw operations + Skia lease pattern are now confirmed from dumps (`DrawingContext.Custom` -> `ImmediateDrawingContext.TryGetFeature` -> `ISkiaSharpApiLeaseFeature.Lease`). See `research/docs/reports/REPORT_AVALONIA_SKIA_CUSTOM_DRAW.md`.
- **Phase 1:** Prototype one reusable custom draw operation for a sweep-gradient border stroke with position-aware angle and lightweight blur/glow.
- **Phase 2:** Wrap in a toggleable plugin surface with global enable + intensity presets + fallback to standard Border rendering.
- **Phase 3:** Expand to selected components (settings cards, popups, badges) after perf and readability validation on Dark/Light/PureDark.

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

## Security Hardening

Items derived from security research on Root Communications v0.9.86. Uprooted ships inside the same process and browser context as Root, so these findings directly affect how Uprooted handles tokens, validates input, delivers updates, and protects user privacy.

See [Security Research](research/SECURITY_RESEARCH.md) for the full findings.

### Token handling improvements

Root stores bearer tokens in process memory (H17), on disk without DPAPI (H18, M5), and in sessionStorage as plaintext JSON (M10). Uprooted's bridge proxy intercepts calls that carry these tokens. Hardening work:

- **Avoid retaining tokens in Uprooted's own scope.** Bridge proxy handlers should forward tokens without caching them in closures or module-level variables. Audit `src/api/bridge.ts` to confirm no token values persist beyond the call boundary.
- **Scrub token values from log output.** Console logging in plugin lifecycle and bridge interception must never print raw bearer tokens. Add a log sanitizer that detects the 128-byte token format and replaces it with a truncated fingerprint.
- **Warn users about AuthToken file exposure.** The installer or settings panel should inform users that `Store\AuthToken` is readable without elevation and is not protected by DPAPI. Consider documenting how users can restrict ACLs on this file.

### Input validation enhancements

Several Root findings involve injection vectors (C5 open redirect via `restart(url)`, H1 `encodeURI()` parameter injection, H3 innerHTML XSS via DevBar, M11 URI scheme pass-through). Uprooted can mitigate some of these for its own users:

- **Validate URLs passed through Uprooted APIs.** Any plugin API that accepts a URL (theme CDN, plugin manifest, etc.) must reject `javascript:`, `data:`, `file:`, and `vbscript:` schemes. Implement a shared `validateUrl()` utility in `src/api/`.
- **Sanitize plugin-provided HTML.** Plugins that inject HTML into the settings panel or DOM overlays should pass through a sanitizer. Consider bundling a lightweight sanitizer (or a minimal allowlist-based approach) rather than relying on `textContent` alone.
- **Enforce scheme allowlists on bridge proxy.** If Uprooted adds bridge method interception for `restart()` or navigation calls, enforce an allowlist (`https:`, `http:`, `root:`) and block dangerous schemes before they reach the native layer.

### Update mechanism security

The auto-updater (shipped v0.4.2) downloads an encrypted `.uprpkg` package from GitHub releases over HTTPS. Current protections:

- **Encrypted package format** — multi-layer XOR with 64-byte master key + per-build 32-byte random nonce + position-dependent key derivation. Prevents casual inspection of release artifacts.
- **Staging + verification** — files are unpacked to a staging directory, verified non-empty and complete, then copied to production. Partial downloads or corruption abort without touching production files.
- **Authenticated developer channel** — XOR-obfuscated PAT for private repo API access; password-gated channel selection (SHA-256 hash comparison).

Future hardening opportunities (not yet implemented):

- **Asymmetric signature on package** — sign `.uprpkg` with an offline private key, verify with embedded public key before decryption.
- **SHA-256 hash manifest** — publish per-file hashes in a signed manifest; verify after decryption.
- **Certificate pinning** — pin GitHub's TLS certificate chain for the update channel.

### Privacy protections

Root leaks PII to Sentry (H4 `sendDefaultPii: true`, H5 TURN/ICE credentials, M16 session replay at 25% error rate) and exposes user data through various channels. Uprooted already blocks Sentry via the sentry-blocker plugin. Additional hardening:

- **Expand sentry-blocker transport coverage.** Monitor for Sentry SDK updates that introduce WebSocket or image beacon transports beyond the current fetch/XHR/sendBeacon interception. Consider intercepting at the `Sentry.init()` level for comprehensive blocking.
- **Block session replay recording.** Sentry's session replay (M16) captures video-like DOM recordings. If the sentry-blocker cannot suppress replay initialization, add a dedicated replay interceptor that stubs the recording API.
- **Strip PII from bridge traffic logging.** If Uprooted adds bridge call logging for debugging, ensure user IDs, device IDs, IP addresses, and TURN credentials are redacted before any output.
- **Document privacy posture.** Create a user-facing document explaining what Uprooted blocks (Sentry telemetry), what it does not block (Effects SDK WASM loading, update checks), and what data Uprooted itself collects (none, currently).

---

## Technical Debt

Items from the automated codebase analysis that are not yet tracked elsewhere in this roadmap. These range from type safety gaps to architectural concerns that will cause maintenance burden as the project grows.

See [Planning Reference](dev/PLANNING_REFERENCE.md) for the full analysis.

### Settings persistence (System.Text.Json limitation)

Already tracked in Known Issues (Critical) and Short-term Goals. Additional debt items related to settings:

- **Settings file format migration path.** The INI-style workaround in `hook/UprootedSettings.cs` is a stopgap. Define a migration strategy for when System.Text.Json is eventually resolved (new .NET host, separate process, or alternative parser). The INI format should be forward-compatible with the eventual JSON format.
- **Shallow merge in browser-side settings.** `deepMerge` is now implemented in `src/core/settings.ts`. Ongoing: ensure the implementation handles arrays (plugin lists) correctly and does not duplicate entries on repeated merges.

### Code quality items

These items from CONCERNS.md are not yet on the roadmap:

- **`any[]` typing for XMLHttpRequest parameters.** The rest parameters in `src/plugins/sentry-blocker/index.ts` lose type information. Type them as `[async?: boolean, username?: string, password?: string]` per the MDN XMLHttpRequest.open() spec.
- **Theme variable cleanup duplication.** `src/plugins/themes/index.ts` may still maintain separate CSS variable name lists for cleanup. Verify and consolidate into a single source of truth computed from `generateCustomVariables` keys.

### Linter and formatter configuration

No linter or formatter is configured for the TypeScript layer. Code style currently relies on TypeScript strict mode and manual review. Add:

- ESLint with a strict TypeScript ruleset (or Biome as a faster alternative)
- Prettier (or Biome formatting) for consistent formatting
- Pre-commit hooks via husky or lefthook to enforce on every commit
- CI check to reject unformatted code

---

## Testing Improvements

The TypeScript layer has zero test coverage. No test framework, no test files, no test scripts. The C# layer has a manual test harness and two unit test files. This section tracks the plan to close these gaps.

See [Planning Reference](dev/PLANNING_REFERENCE.md) for the full testing analysis.

### Test framework setup

- **Install Vitest** as the TypeScript test runner. It has native ESM support (the codebase uses ES modules), fast in-memory execution, and minimal configuration. Add `test`, `test:watch`, and `test:coverage` scripts to `package.json`.
- **Adopt co-located test files** using the `.test.ts` suffix: `src/core/pluginLoader.test.ts`, `src/api/bridge.test.ts`, etc.
- **Set initial coverage targets**: 60% statements, 50% branches, 70% functions. These are starting targets, not ceilings.

### Unit test expansion targets

High-priority modules that need test coverage first, in order:

1. **PluginLoader** (`src/core/pluginLoader.ts`) -- Registration, start/stop lifecycle, patch installation, event emission, multi-plugin handler chaining, concurrent start/stop race conditions.
2. **Settings** (`src/core/settings.ts`) -- JSON parsing, merge with defaults, deep merge for nested plugin configs, error recovery from corrupted settings files, file path resolution.
3. **Bridge proxy** (`src/api/bridge.ts`) -- Proxy creation, event interception, call cancellation, return value replacement, deferred setup via `Object.defineProperty`, independent proxying of both bridge objects.
4. **Sentry blocker** (`src/plugins/sentry-blocker/index.ts`) -- Fetch blocking for `*.sentry.io`, XHR redirect behavior, sendBeacon interception, blocked count accuracy, concurrent request handling.
5. **Theme engine** (`src/plugins/themes/index.ts`) -- Color math functions (`darken()`, `lighten()`, `luminance()`), custom variable generation, CSS variable application and cleanup, invalid theme name handling.
6. **CSS injection** (`src/api/css.ts`) -- Style element creation, ID prefixing, targeted removal vs batch removal, isolation from non-Uprooted styles.
7. **DOM utilities** (`src/api/dom.ts`) -- `waitForElement()` immediate resolution, timeout rejection, observer cleanup, `nextFrame()` scheduling.

### Integration test needs

These scenarios require multiple modules working together and are harder to unit test in isolation:

- **Full plugin lifecycle**: Register a plugin, start it (patches install, CSS injects, lifecycle hooks fire), verify bridge interception works, stop it (patches uninstall, CSS removes, cleanup runs), verify no residual state.
- **Settings round-trip**: Load settings from a file, modify via the settings API, verify the in-memory state is correct and that re-loading produces the same result.
- **Preload initialization sequence**: Settings check, plugin registration, `startAll()` execution, error banner display on fatal errors, `window.__UPROOTED_VERSION__` availability.

### Manual test procedures to automate

These are currently verified by manual inspection with console logging. Converting them to automated checks would catch regressions earlier:

- **Sentry blocker count verification**: Start the blocker, simulate Sentry requests via fetch/XHR/sendBeacon, verify the blocked count matches expected values.
- **Theme application verification**: Apply a theme, verify that the expected CSS variables are set on `document.documentElement`, switch themes, verify old variables are removed and new ones applied.
- **Settings panel injection**: Mock a DOM structure resembling Root's settings page, trigger the MutationObserver, verify that Uprooted's sidebar items appear in the correct location.

### C# hook test expansion

The existing `tests/UprootedTests/` project covers ColorUtils and GradientBrush. Additional targets:

- **UprootedSettings INI parsing**: Test the INI-style parser with valid files, missing keys, malformed lines, and empty files. This can be tested without Root's runtime.
- **PlatformPaths resolution**: Test path resolution logic across different platform configurations. Mock environment variables and verify correct path construction.
- **Logger robustness**: Verify that the Logger class swallows its own exceptions and never crashes the host process, even when the log directory is missing or the disk is full.

---

## Version Compatibility Tracking

Uprooted injects into a closed-source application that updates independently. Tracking version compatibility is essential for diagnosing user issues and planning for breaking changes.

### Root version compatibility

| Uprooted Version | Root Versions Tested | Status | Notes |
|-------------------|---------------------|--------|-------|
| 0.4.2 (current) | v0.9.86 - v0.9.93 | Active | Primary development target |

Maintain this matrix as new Root versions are released. When a user reports a bug, the first diagnostic question should be which Root version they are running.

**Monitoring approach:**
- Check for Root updates weekly (installer.rootapp.com serves Velopack manifests)
- When a new Root version is detected, test core injection (HTML patching, profiler loading, bridge proxy, settings panel) before declaring compatibility
- Document any breaking changes discovered during testing

### .NET version requirements

Uprooted's C# hook layer targets .NET 10, matching Root's runtime. Key dependencies:

- **CLR profiler API**: The profiler DLL (`tools/uprooted_profiler.c`) uses the ICorProfilerCallback interface. This API is stable across .NET versions but the profiler GUID and COM interfaces must match the runtime version.
- **Reflection targets**: `hook/AvaloniaReflection.cs` resolves types by name from loaded assemblies. A .NET runtime version change could alter assembly loading order or available types.
- **If Root upgrades .NET version**: Rebuild the profiler DLL against the new CoreCLR headers. Test that the hook DLL loads correctly in the new runtime. Verify all reflection targets still resolve.

### Avalonia version dependencies

Root uses Avalonia 11.x for its native UI. The C# hook depends on specific Avalonia type names, property names, and method signatures:

- **Current target**: Avalonia 11 (specific minor version unknown, inferred from API surface)
- **Critical types**: `ContentControl`, `StackPanel`, `Grid`, `TextBlock`, `Border`, `ScrollViewer`, `Button`, `ToggleSwitch`, `ComboBox`, and approximately 70 others cached in `AvaloniaReflection.cs`
- **Known risk**: `DispatcherPriority` is a struct (not an enum) in Avalonia 11+. Code that treats it as an enum will fail silently.
- **Detection plan**: Add Avalonia assembly version detection at hook startup. Log the detected version. If the version differs from the tested version, log a warning and consider entering a degraded mode that skips native UI injection but leaves HTML patching and TypeScript injection intact.

### Breaking change monitoring

Proactive detection of changes that could break Uprooted:

- **Root HTML structure changes**: The `HtmlPatchVerifier` (Phase 0) already detects when HTML patches are missing and self-heals. Monitor its repair frequency -- if repairs happen on every launch, Root is overwriting patches on update and the install mechanism needs adjustment.
- **Root bridge API changes**: Add version negotiation for `__nativeToWebRtc` and `__webRtcToNative` (already in Medium-term Goals as "Bridge version detection"). Log the set of available bridge methods at startup and compare against the expected set.
- **Root settings page structure changes**: The sidebar injector searches for "APP SETTINGS" text. Add a secondary structural detection method that does not depend on text content, so injection survives label changes.
- **Avalonia API surface changes**: If Root ships a new Avalonia version, the reflection cache may fail to resolve types. Add per-type resolution failure handling so that individual features degrade rather than the entire hook failing.

---

## Feature Requests

Have an idea for Uprooted? We track feature requests as GitHub issues.

- **Suggest a feature:** Open a [feature request](https://github.com/The-Uprooted-Project/uprooted/issues/new?template=feature-request.yml)
- **Report a bug:** Open a [bug report](https://github.com/The-Uprooted-Project/uprooted/issues/new?template=bug-report.yml)
- **Discuss ideas:** Start a conversation in [GitHub Discussions](https://github.com/The-Uprooted-Project/uprooted/discussions) (if enabled)

When suggesting a feature, include:
- A clear description of the behavior you want
- Why it would be useful (what problem does it solve?)
- Any technical considerations you are aware of
- Whether you would be willing to contribute an implementation

---

*Last updated: 2026-02-21 — added LiquidGlass medium-term plan, added Translate + Presence Beacon goals, removed stale NSFW filter issue, updated MessageLogger status*

---

**Canonical for:** known issues, planned features, future direction, priority assessment
**Not canonical for:** active tasks → [TASKS.md](../TASKS.md) | plugin roadmap → [PLUGIN_ROADMAP.md](PLUGIN_ROADMAP.md) | current architecture → [ARCHITECTURE.md](framework/ARCHITECTURE.md)
*Roadmap for Uprooted. Last updated 2026-02-21.*
