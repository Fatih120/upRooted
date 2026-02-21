# Changelog

All notable changes to Uprooted are documented in this file.

Format follows [Keep a Changelog](https://keepachangelog.com/). Versions follow [Semantic Versioning](https://semver.org/) starting from v0.2.0.

---

## [Unreleased]

### Added

- **Wide event structured logging** — New `WideEvent` builder emits structured `[Category|operation] key=value key=value dur_ms=N` log lines following [loggingsucks.com](https://loggingsucks.com) methodology. ~1200 freeform `Logger.Log` calls migrated to ~100 wide events across 11 files. Old format still works (backwards compatible).
  - Files: `hook/WideEvent.cs` (new), `hook/Logger.cs`
- **Tail sampling for scan engines** — `TailSampler` aggregates high-frequency scan timer results into 30-second heartbeat summaries. Applied to 4 scan engines (LinkEmbedEngine, MessageLogger, NsfwFilter, ThemeEngine), eliminating thousands of repetitive log lines per session.
  - File: `hook/TailSampler.cs` (new)
- **Log Console** — Dev-channel-only "Live Console" button on the About page opens a named pipe server that streams log lines in real time. `Logger.OnLine` callback feeds new lines to the pipe.
  - File: `hook/LogConsole.cs` (new)
- **Native theme settings button** — Gear button on the "Native" preset card opens Root's native Change Theme page. Uses ViewModel-driven `ListBox.SelectedItem` binding for proper navigation. New `SelectRootTab()` helper enables programmatic Root settings tab navigation by `MenuTitle` match.
- **Custom theme overhaul** — Auto-apply on keystroke, full OKLCH lightness range (light backgrounds work), smooth direction-aware derivation, custom text color input, tag-based visual tree walker for live recoloring, variant switching for light custom themes
- **DynamicResource binding attempt** — `BindToDynamicResource` in AvaloniaReflection (silently fails; walker is real mechanism)
- **Themes page refresh button** — Added a `Refresh` button to Theme Settings that reapplies the currently active theme (`default-dark`, custom, or preset) and rebuilds the page. Uses standard Uprooted button styling (rounded border, highlight-derived stroke, hover tint, release-activated click).
  - File: `hook/ContentPages.cs`

### Changed

- **Logging format** — Log lines now support two formats: the original `[HH:mm:ss.fff] [Category] message` and the new structured `[HH:mm:ss.fff] [Category|operation] key=value dur_ms=N`. Logger.cs grew from 113 to ~170 lines with the addition of `EmitWideEvent` and `OnLine` callback support.
- **Injected control interaction model now matches Root native semantics** — Click-like actions execute on release and are gated by in-bounds press+release (`SubscribeClickReleased`), so press-drag-off-release no longer toggles actions. Applied broadly to settings buttons, toggles, theme cards, nav items, and popup controls.
  - Files: `hook/AvaloniaReflection.cs`, `hook/ContentPages.cs`, `hook/SidebarInjector.cs`, `hook/TranslateConfigPopup.cs`
- **Native press feedback for injected clickable controls** — `SetCursorHand` now wires centralized press feedback (proportional shrink + subtle press shade/restore) via render transform + opacity, without causing layout reflow.
  - File: `hook/AvaloniaReflection.cs`
- **Card border thickness** — 1.5px → 1.0px to match Root's native divider lines; color from Root's `Border` resource
- **Nav item borders** — Visible 1px borders using Root's highlight resources, adapts to light/dark
- **Custom theme island** — Hardcoded colors immune to walker recoloring; ping toggle uses hardcoded off-color
- **ScrollViewer** — `HorizontalScrollBarVisibility = Disabled` on content ScrollViewers for correct width
- **Plugin filter** — Cycling toggle pill with Bold text, `AdjustForHighlight` 1.5px border, `MinWidth` to prevent width jitter
- **Plugin sort** — Stability tier prioritized over enabled/disabled (Stable always before Beta)
- **Themes "Open" button** — Sized to match toggle switches (44×24)
- **Experimental banner** — Theme-aware toggle: theme colors when off, amber warning when on; 20px icon hidden when off
- **Restart banners** — Title + subtitle layout, burnt orange tint (`#2A1D15`/`#D06818`), accent-format restart buttons
- **Vector icons** — Plugin card gear/info icons use `Shapes.Path` with `Stretch.Uniform` (Material Design 24x24); replaced `PathIcon` which didn't scale
- **Plugin card text** — Bold plugin names (was silently failing), Bold status badges with 1px border
- **Cards-in-a-card layout** — Preset and custom theme sections use Root-native two-level card hierarchy (outer container + inner 2nd-order cards with lighter bg, thicker borders, Grid column layout)
- **DPI-aware borders** — `ThinBorder` (1px physical) and `ThickBorder` (next pixel boundary) computed from `RenderScaling`; always visually distinct at any DPI
- **Radio indicators** — Grid overlay with sibling Borders (18×18 ring + 10×10 centered dot); `(18-10)*scale` always even for perfect centering at 100%/150%
- **Toggle switch** — Thumb 18→16 for even centering gaps at all DPI scales
- **Typography** — Page titles 20px Bold, section headers 14px Bold TextPrimary, About page → "About Uprooted"
- **Button borders** — Accent buttons get `AdjustForHighlight(color, 30)` 1.5px borders with Bold text; Developer/Stable border updates on toggle
- **Restart button** — Deeper burnt orange (`#D06818`)

### Fixed

- **Full codebase bug audit** — 15-commit sweep covering thread safety, timer leaks, fire-and-forget task accumulation, error handling gaps, and type correctness:
  - `UprootedSettings`: thread-safe cache read (local copy prevents TOCTOU null return), atomic `Save()` via temp-rename, `OrdinalIgnoreCase` plugins dict
  - `MessageStore`: `IDisposable` + timer disposal, `WriteLock` covers full file I/O (prevents Truncate/FlushBuffer interleave), skip records with bad timestamps instead of inventing `UtcNow`
  - `MessageLogger`, `AuditLogEngine`, `LinkEmbedEngine`: `IDisposable` added; each disposes its `_scanTimer` on shutdown. `MessageLogger` also disposes its owned `MessageStore`.
  - `ClearUrlsEngine`, `SidebarInjector`: redundant `Task.Delay().ContinueWith()` timeout tasks removed (accumulated thousands of orphaned background tasks per hour; `finally` blocks already reset the guard)
  - `NsfwFilter`: `_seenImages` dictionary now capped at 2000 entries to prevent unbounded growth during long sessions
  - `RootcordEngine`: `Task.Delay(300)` in `SubscribeTabChanges` and `Task.Delay(350)` in `OpenSettingsDirectly` now receive a `CancellationToken` cancelled in `Revert()`, preventing stale callbacks from firing after the plugin is disabled
  - `Logger`: static constructor wrapped in try/catch with `Path.GetTempPath()` fallback — prevents `TypeInitializationException` from crashing the entire hook if the profile directory is unresolvable
  - `HtmlPatchVerifier`: `_debounceTimer = null` added after disposal in `Dispose()` to prevent use-after-free
  - `StartupHook`: diagnostic `DumpVisualTreeColors` exception now logged instead of silently swallowed
  - `plugin.ts` / `pluginLoader.ts`: `Patch` handler signatures restricted to synchronous return types — `Promise<>` return would silently break `before()` cancellation because the bridge proxy cannot await
  - `ContentPages`: lightbox dismiss methods (`DismissPluginInfoLightbox`, `DismissPluginSettingsLightbox`, `DismissUpdateNotification`) now null the overlay ref before calling `RemoveFromOverlay`, guarding against re-entrant dismiss on rapid double-click; `NsfwFilterInstance.UpdateConfig()` calls wrapped in try/catch with logging

- **Theme revert** — Variant toggle + `RestoreTaggedControls` for complete restoration
- **Custom Theme card interaction quirk** — Toggling Ping Color no longer bubbles into and activates the parent Custom Theme card; large Custom card press feedback is intentionally disabled to avoid over-aggressive card-scale depression.
  - File: `hook/ContentPages.cs`
- **Theme card no-op retrigger** — Clicking a theme card that is already active now exits early instead of reapplying/rebuilding unnecessarily.
  - File: `hook/ContentPages.cs`
- **Auto-nav on variant change** — `_hasAutoNavigated` flag prevents re-navigation to About tab
- **Settings header back arrow** — Structural search finds back button by child order in header Grid (not by bounds or text). Collapse pattern (Opacity/MaxWidth/MaxHeight/Width/Margin all zeroed) avoids fighting Root's `IsVisible` binding on `SelectedMenuItemPageContainer.Navigator.CanGoBack`. Title TextBlock overridden after ListBox deselection. Restores original values (Width=40, Margin=24,0,0,0) when switching back to Root tabs.
- **Settings detection delay** — Lowered LayoutUpdated throttle from 500ms to 50ms; removed `_injecting` reentracy guard from LayoutUpdated path (both paths run on UI thread); reset stale timer lock in NullState. Detection is now near-instant on every open including rapid close/reopen cycles.
- **Title disappearing on rapid re-opens** — Selection suppression re-deselection now re-applies CollapseBackButton + SetHeaderTitle to restore header state that the binding fallback destroys

---

## [0.4.2] - 2026-02-20

### Added

- **Custom theme: text color control** — Custom themes now support an explicit text color input. When left empty, text color is auto-derived from background luminance (unchanged behavior). Stored in `CustomText` INI key.
  - Files: `hook/ThemeEngine.cs`, `hook/ContentPages.cs`, `hook/UprootedSettings.cs`
- **Light/PureDark theme compatibility** — Uprooted's injected UI (settings pages, sidebar nav, cards, version text) now adapts to Root's active theme variant. Colors read live from Root's ThemeDictionaries via `Application.TryGetResource` at page build time. Previously hardcoded for Dark only — Light theme was completely unusable (white text on white background).
  - `ThemeEngine.ReadLiveRootColors()` / `ReadLiveRootColor(key)` — reads Root's 20 color keys from live Application resources
  - `ContentPages.AdjustForHighlight()` — luminance-aware highlight (darkens on light bg, lightens on dark bg)
  - `SidebarInjector.SyncContentPagesFromNativeTree()` — captures native fg/bg from visual tree at injection time
  - Files: `hook/ThemeEngine.cs`, `hook/ContentPages.cs`, `hook/SidebarInjector.cs`, `hook/AvaloniaReflection.cs`
- **SVG path swap for Uprooted themes** — When an Uprooted dark theme is active on Root's Light variant, ~220 SVG asset paths are swapped from `Light Theme/` to `Dark Theme/` folder. Determined automatically by background luminance.
  - File: `hook/ThemeEngine.cs`
- **Experimental plugins opt-in toggle** — New toggle in Plugin Settings opts into showing plugins with Experimental testing status. Hidden by default to reduce clutter.
  - File: `hook/ContentPages.cs`
- **Desktop notifications** — Auto-updater shows an OS-level toast (Windows via WinRT) or `notify-send` (Linux) when a background update is applied, in addition to the in-app overlay. Respects the `AutoUpdateNotify` setting (previously ignored).
  - Files: `hook/DesktopNotification.cs` (new), `hook/StartupHook.cs`
- **Rootcord plugin** — Discord-style vertical server sidebar added as an experimental plugin. Supports live toggle without restart.
  - File: `hook/RootcordEngine.cs` (new)
- **Plugin Show More** — Plugin page opens with 4 cards (2 rows) to avoid scrolling. A "Show N More" button expands the full list; search and filter bypass the limit.
  - File: `hook/ContentPages.cs`

### Changed

- **Custom theme: live recoloring** — Removed the "Apply Custom" button; custom themes now auto-apply on every keystroke. A tag-based visual tree walker (`dyn-fg:`, `dyn-bg:`, `dyn-bb:` tags) recolors injected controls on 100ms intervals; Root native controls use a text-only color map for Foreground matching. Custom theme card is exempt ("no-recolor island"). `ColorPickerPopup.IsOpen` guard prevents rebuild during color picking. Full OKLCH range (0.05–0.93) replaces the previous dark-only lightness clamp, enabling correct light-background custom themes. Smooth direction-aware derivation replaces binary isDark snapping.
  - Files: `hook/ThemeEngine.cs`, `hook/ContentPages.cs`, `hook/AvaloniaReflection.cs`, `hook/UprootedSettings.cs`
- **Custom theme: variant switching for light themes** — SVG path swap now activates correctly when a custom theme has a light background and Root is on its Light variant. Revert forces DynamicResource re-resolution via variant toggle. `RestoreTaggedControls` walk restores injected elements after revert.
  - File: `hook/ThemeEngine.cs`
- **Rootcord: member count tooltip** — Server icon tooltip now shows member counts below the server name ("N online • M members"). Uses a 5×5 BrandPrimary dot + 11px medium-weight TextSecondary text, matching Root's native `CommunityTabView` pill style. `GetTabMemberCounts()` reads `Members.AttachedMemberCount` / `Members.MemberCount` from `CommunityTabViewModel`.
  - File: `hook/RootcordEngine.cs`
- **SilentTypingEngine: DiagnosticListener rewrite** (by Kurumi Nanase) — Replaced the 482-line HttpClient/GrpcChannel handler injection with ~90-line DiagnosticListener approach. Subscribes to .NET's built-in HTTP diagnostics (`DiagnosticListener.AllListeners`), intercepts `System.Net.Http.HttpRequestOut.Start` events, and redirects SetTypingIndicator requests to `localhost:0`. No discovery, no field walking, no handler patching.
  - File: `hook/SilentTypingEngine.cs`
- **Themes tab: Open button** — Replaced the Themes plugin toggle in Plugin Settings with an "Open" button that navigates directly to the Themes settings tab.
  - File: `hook/ContentPages.cs`
- **Sidebar re-injects on variant change** — When Root switches Dark↔Light↔PureDark, the sidebar removes its injected controls (UnwrapScrollViewer + remove nav items + RemoveVersionText + NullState) and lets the next LayoutUpdated pass re-inject with fresh native colors. Does not try to restore Root's controls (they use DynamicResource).
  - File: `hook/SidebarInjector.cs`
- **Auto-revert Uprooted theme on Root variant change** — If an Uprooted theme is active and the user switches Root's native variant, the Uprooted theme auto-reverts. Respects the user's choice of Root theme.
  - File: `hook/ThemeEngine.cs`
- **Variant change subscription unconditional** — `EnsureVariantChangeSubscribed()` called from startup, not just when applying a theme. Needed for sidebar re-injection even when no Uprooted theme is active.
  - Files: `hook/ThemeEngine.cs`, `hook/StartupHook.cs`
- **Card border highlights** — Cards use 1.0px border thickness matching Root's native divider lines; border color reads from Root's `Border` resource. Plugin cards: visible resting border, no hover highlight. Theme preset cards: resting border + radio dot/border highlight on hover + accent border when selected. Theme selection uses `PointerReleased` (matches Root's native selector).
  - File: `hook/ContentPages.cs`
- **Custom theme: no-recolor island** — Custom theme card uses hardcoded colors immune to the tag-based walker. Ping color toggle's off-color is hardcoded (`#2A2A44`) so the pill thumb doesn't blend into the card background when disabled.
  - File: `hook/ContentPages.cs`
- **ScrollViewer: horizontal scroll disabled** — All content ScrollViewers set `HorizontalScrollBarVisibility = Disabled` so content stretches to fill available width correctly.
  - File: `hook/AvaloniaReflection.cs`
- **Nav item highlights use Root resources** — Hover uses live `HighlightLight`, selection uses `HighlightNormal` from ThemeDictionaries. Nav items also have visible 1px resting borders using Root's highlight resources (`HighlightLight`/`HighlightNormal`/`HighlightStrong`). Automatically adapts to Dark (white alpha) / Light (black alpha).
  - File: `hook/SidebarInjector.cs`
- **Version text matches Root's native color** — Reads foreground from Root's existing "Root Version" TextBlock instead of hardcoded `#66f2f2f2`.
  - File: `hook/SidebarInjector.cs`
- **Plugin sort order** — Plugins sort enabled-first, then Stable → Experimental, then A-Z. Sort is dynamic and updates on every toggle.
  - File: `hook/ContentPages.cs`
- **Auto-update interval** — Background check interval reduced from 6 hours to 1 minute.
  - File: `hook/AutoUpdater.cs`
- **About page** — Removed Links and Diagnostics cards. Added compact "Open Logs" button to the page title row. No more scrolling required.
  - File: `hook/ContentPages.cs`
- **Testing status changes** — ClearURLs promoted to Stable, Themes demoted to Beta, SilentTyping demoted to Experimental.
  - File: `hook/ContentPages.cs`

### Fixed

- **Auto-nav on variant change** — Settings page no longer auto-navigates to the About tab when Root's theme variant changes (e.g. switching Dark↔Light). `_hasAutoNavigated` flag ensures auto-nav only fires on the first settings open.
  - File: `hook/SidebarInjector.cs`
- **Settings navigation freeze** — `ScheduleWalkBurst` now debounces to a single 150ms delayed walk instead of firing `WalkVisualTreeNow` immediately on every tab switch. Rapid navigation no longer stacks UI-thread tree walks.
  - File: `hook/ThemeEngine.cs`
- **Experimental toggle unclickable** — Fixed z-order in the Plugin Settings experimental banner so the toggle pill renders on top of the banner and receives pointer events.
  - File: `hook/ContentPages.cs`
- **Named color crash** — `Color.ToString()` returns `"White"` for `#FFFFFFFF` in Avalonia, crashing `ColorUtils.ParseHex()`. Now extracts via `Color.A`/`R`/`G`/`B` byte properties.
  - File: `hook/ThemeEngine.cs`
- **ResourceDictionary indexer miss** — `dict["key"]` only returns direct entries, not MergedDictionaries. Switched to `Application.TryGetResource` for full resolution chain.
  - Files: `hook/ThemeEngine.cs`, `hook/AvaloniaReflection.cs`
- **Preview swatch breaks hover** — Transparent background on theme preview caused `PointerExited` when mouse moved over it. Fixed with `IsHitTestVisible=false`.
  - File: `hook/ContentPages.cs`
- **ThemeEngine crash: InvalidCastException on startup** — Fixed ThemeEngine storing `Avalonia.Media.Color` objects in ThemeDictionaries where Root's converters expect `Avalonia.Media.IBrush`. Only `DropShadow` is stored as Color; all other keys now correctly stored as `SolidColorBrush` (or `LinearGradientBrush` for `ScrollShadow`).
  - File: `hook/ThemeEngine.cs`
- **Online status indicators recolored by themes** — ThemeEngine no longer overrides `BrandSecondary` in ThemeDictionaries. Root uses this key for online status dots (green `#A8FF5D`); overriding it caused all online indicators to show the theme accent color instead.
  - File: `hook/ThemeEngine.cs`

### Infrastructure

- Pinned CI .NET SDK to 10.0.103 to match Root's runtime

### Documentation

- **AVALONIA_PATTERNS.md** — 6 new pitfalls + UI Standards section
- **HOOK_REFERENCE.md** — ContentPages color theming rewritten, sidebar variant change sections
- **ROOT_THEME_SYSTEM_FINDINGS.md** — Live-testing confirmations
- **THEME_ENGINE_DEEP_DIVE.md** — ThemeDictionaries re-entrancy trap
- Documented ThemeDictionaries re-entrancy trap (confirmed live)
- Updated docs for MessageLogger overhaul and SilentTyping rewrite

---

## [0.4.1] - 2026-02-19

### Changed

- **Theme Engine v2: resource-first rewrite with OKLCH palette generation** — Complete rewrite of `ThemeEngine.cs` (2638 → ~900 lines). Now overrides Root's actual 32 DynamicResource keys in `ThemeDictionaries[activeVariant]` instead of ~70 FluentTheme/SimpleTheme keys Root doesn't reference. DynamicResource propagation replaces continuous timer-based visual tree walking (11 walk methods, 500ms timer, layout interceptor, burst walks, cross-mapping all eliminated). Three override layers: (1) ThemeDictionaries for Root's 32 keys, (2) Styles[0].Resources for SimpleTheme keys, (3) single delayed one-shot walk for hardcoded code-behind colors.
  - OKLCH (perceptually uniform) replaces HSL for palette generation — lightness steps now look consistent across all hues. Conversion chain: sRGB ↔ OKLab ↔ OKLCH with gamut mapping via binary search on chroma (20 iterations).
  - New `ColorUtils.cs` functions: `SrgbToLinear`, `LinearToSrgb`, `RgbToOklab`, `OklabToRgb`, `RgbToOklch`, `OklchToRgb`, `OklchToHex`, `HexToOklch`.
  - New `AvaloniaReflection.cs` methods: `GetThemeDictionaries`, `GetActiveThemeVariant`, `FindVariantByName`, `SubscribeActualThemeVariantChanged`.
  - Live preview uses dict updates only (no tree walk during drag) — 60fps throttle.
  - Revert removes direct entries from ThemeDictionaries; MergedDictionaries originals reassert via DynamicResource.
  - Ping color now overrides `SelfMention`/`SelfMentionBackground`/`SelfMentionBorder` resource keys directly (no tree walk).
  - Files: `hook/ThemeEngine.cs`, `hook/ColorUtils.cs`, `hook/AvaloniaReflection.cs`, `hook/ContentPages.cs`
- **Silent Typing: restored JS interception** — Replaced the no-op TypeScript stub with Kurumi Nanase's working fetch/XHR interception. Blocks `SetTypingIndicator` gRPC calls at the browser network level. Both C# (`SilentTypingEngine.cs`) and JS (`silent-typing/index.ts`) interception now active for defense-in-depth.
  - File: `src/plugins/silent-typing/index.ts`

### Fixed

- **Theme switch color inconsistencies** — Targeting Root's actual 32 DynamicResource keys means controls bind directly to overridden resources. No more stale colors from missed tree walks — DynamicResource propagates instantly to all bound controls.
- **Custom theme: Root settings controls don't recolor instantly** — Same root cause as above. Controls using `DynamicResource` bindings now update automatically when theme keys change, without requiring a tab switch or page reload.
- **Linux crash: `MissingMethodException` on startup** — `ManualResetEventSlim.Wait(TimeSpan)` doesn't exist on .NET runtime Root ships on Linux; replaced with `Wait(int)` which is universally available.

### Documentation

- **Root theme system research** (`research/ROOT_THEME_SYSTEM_FINDINGS.md`) — ILSpy decompilation documenting Root's 32-key native Avalonia color system, complete color tables for Dark/Light/PureDark, theme switching mechanism, and correct override strategy
- **ILSpy decompilation analysis** — 219 files in `research/ilspy-dumps/` (156 newly split from grouped dumps), 29 analyzed into `ROOT_CONTROL_REFERENCE.md`
- **ROOT_CONTROL_REFERENCE.md** — Authoritative reference for Root's custom controls, style classes, resource keys, message view internals, settings infrastructure, and DataStore keys
- Corrected 12 docs that incorrectly claimed Root uses FluentTheme keys — all now reference Root's actual 32 ThemeDictionary keys

---

## [0.4.0] - 2026-02-18

### Added

- **SilentTyping: C# reimplementation** — Replaced the ineffective JS fetch/XHR intercept with a native `SilentTypingEngine` that patches Root's `HttpClient` handler chain at the .NET level.
  - `TypingBlockerHandler` (a `DelegatingHandler`) intercepts any outbound request whose path contains `"SetTypingIndicator"` and returns a synthetic `200 OK`, preventing the typing indicator from reaching Root's servers.
  - Discovery: static field scan across non-framework assemblies for `HttpClient` on gRPC/messaging types; ViewModel chain walk from `MainWindow.DataContext` up to 8 levels deep.
  - Timer-based: 12s startup delay, then every 5s until patched, then backs off to 30s.
  - Enable/disable re-checked on every intercepted request — toggle is live without restart.
  - TypeScript plugin `silent-typing` gutted to a no-op stub (v0.1.0 → v0.2.0); JS fetch/XHR interception had no effect because `SetTypingIndicator` originates from Root's .NET layer, not DotNetBrowser.
  - Files: `hook/SilentTypingEngine.cs` (new), `hook/StartupHook.cs`, `src/plugins/silent-typing/index.ts`
- **FontScale system** — modular font scaling for settings pages. `FontScale` record struct with `PageScale` (11/13/12/13) and `LightboxScale` (18/20/17/18) presets. Shared builders (`CreateSectionHeader`, `BuildSettingsToggle`) accept optional scale parameter, defaulting to `PageScale`. Plugin info/settings lightboxes pass `LightboxScale` for larger, more readable text in modal overlays. Main pages unaffected.
  - File: `hook/ContentPages.cs`
- **MessageLogger plugin** (WIP) — logs deleted messages in Avalonia-native chat with visual indicators
  - Per-item async deletion pollers: each removed item is polled for `HasBeenDeleted` every 300ms for 3s; true = genuine deletion, false = buffer management (discard)
  - Epoch-based channel switch cancellation: pollers check birth epoch against current and self-cancel when stale
  - Per-type property cache (`Dictionary<Type, TypeProps>`) handles multiple ViewModel types with nested `.Message` bridge property resolution
  - Diagnostic instrumentation: DIAG-INJ/DIAG-FLUSH logging, boolean property dump, collection size tracking
  - Deleted messages re-injected as Discord-style full-width red-tinted background stripes with 3px red left accent border, right-click "Clear message history" context menu
  - Edit detection: event-driven via CollectionChanged Replace events (`HandleReplaced`) — two gates prevent false positives: (1) message must have arrived via an Add event (`_addedViaEvent` dict, not initial snapshot); (2) Replace must arrive >5s after Add (`EditGracePeriodSeconds = 5.0`) to filter send-completion content settling (optimistic Replaces arrive within 0.5–2s; genuine user edits arrive after the grace window)
  - Discord-style edit indicators — amber-tinted inline card injected below edited messages via the same Grid row pattern as deleted message cards; shows previous content (faded, italic) + `(edited)` / `(edited Nx)` label with amber left accent border; tag-based dedup and re-injection on scroll recycling via `InjectEditIndicators()` + `BuildEditIndicatorCard()`
  - Analysis tool: `scripts/analyze-msglogger.ps1` parses hook log for MsgLogger/DIAG entries
  - Files: `hook/MessageLogger.cs`, `hook/MessageStore.cs`
- **MessageStore** — flat-file persistence for message log data
  - Pipe-delimited append-only format (`MSG|EDIT|DEL` record types) with URI-encoded fields
  - Buffered flush every 5 seconds, automatic retention enforcement (configurable max messages)
  - File: `hook/MessageStore.cs`; location: `{profileDir}/uprooted-message-log.dat`
- **AutoUpdater** — in-process background updater with encrypted package delivery
  - Checks GitHub releases API every 6 hours; downloads single encrypted `.uprpkg` package containing all update files
  - Multi-layer XOR decryption: 64-byte master key + per-build 32-byte nonce + position-dependent key derivation
  - Files overwritten in-place; changes take effect on next Root restart (no user action required)
  - Developer channel (password-gated, pulls from private repo releases)
  - Reflection-based `HttpClient` to avoid trimmed method exceptions
  - Packing tool: `scripts/pack-update.py` (standalone Python, `--verify` round-trip flag)
  - File: `hook/AutoUpdater.cs`
- **AutoUpdater: hash-based same-version hotfix detection** — "Check for Updates" button now applies an update even on same version number if the downloaded `.uprpkg` SHA-256 differs from the last applied package hash (stored in `AutoUpdate.LastPackageHash`). Enables hotfix delivery without a version bump. Early-return with staging cleanup when hash matches.
- **AutoUpdater: background update notification popup** — Background auto-updates (6-hour timer) no longer restart Root immediately. A dismissable overlay card ("Version X was installed. Restart Root to apply.") is shown via `BackgroundUpdateApplied` static event → `ContentPages.ShowUpdateNotification()` on UI thread. Manual check-for-updates still auto-restarts.
- **Updates settings page** in native Avalonia UI — "Auto-check for updates", "Update notifications", and "Update channel" (stable/dev) controls; wired to `AutoUpdate.*` INI keys
- **MessageLogger settings page** in native Avalonia UI — Log Deleted Messages, Log Edited Messages, Ignore Own Messages toggles; Max Messages retention limit input
- **Version-gated plugin force-disable on upgrade** — `ForceDisableOnUpgrade` dictionary in `StartupHook` declares which plugins to force-disable when users upgrade to (or through) a given version; cumulative application for skipped versions; downgrade-safe (stamps version only); `CurrentVersion` const replaces hardcoded banner string. Ships with MessageLogger and ContentFilter disabled for v0.4.0 (both unvalidated).
- **Developer-channel-only logging** — hook log file (`uprooted-hook.log`) is only written when the update channel is set to "developer"; stable channel users produce no log file; `Logger.Disable()` suppresses all writes and deletes any brief log fragment from early startup
- **`BuildSettingsToggle` helper** in `ContentPages` — reusable pill-toggle + label + description component for any boolean plugin setting
- **TUI installer interactive mode selector** — launching the installer with no flags now shows an arrow-key menu (Install / Uninstall / Repair) instead of defaulting to install
- **LinkEmbedEngine.Instance** static property — enables ContentPages to call `RefreshTitleVisibility()` on settings change
- **Plugin Roadmap** (`docs/PLUGIN_ROADMAP.md`) — planned plugins with architectural notes: ClearURLs, MessageLogger (design reference), NoReplyPing, Translate
- **Built-in plugin documentation** (`docs/plugins/builtin/`) — design doc for MessageLogger

### Changed

- **NSFW content filter: Avalonia-native redesign** — Replaced the DotNetBrowser JS injection approach with a C# visual tree scanner. The filter now DFS-walks the Avalonia tree every 500ms looking for `Image` controls, encodes each new bitmap to PNG via reflection (`Bitmap.Save(Stream)`), and POSTs to the Google Vision SAFE_SEARCH_DETECTION API directly in C#. NSFW images are hidden and replaced with a native `Border > StackPanel` overlay ("⚠ NSFW / Click to reveal"); clicking the overlay reveals the image. `SemaphoreSlim` caps concurrent API calls at 3; bitmapId-based dedup skips already-classified images; `PixelSize` guard skips avatars (<100×100). `RevealAllBlocked()` restores all hidden images when the filter is disabled. Moved from Phase 5 (DotNetBrowser-gated) → Phase 4.5g (standalone, 20s startup delay). No `DotNetBrowserReflection` dependency.
  - Files: `hook/NsfwFilter.cs`, `hook/StartupHook.cs`
- **Settings page font sizes**: tuned to match Root's native settings page scale (section headers 11, labels 13, descriptions 12, page titles 15, lightbox titles 16; toggle pills 44×24, card width 560)
- **ContentFilter API key textbox**: vertically centered text with padding (matches search box style)
- **Log startup separator**: 3 blank lines before first `[Entry]` log on Root launch for clear visual separation
- **watch-log.ps1**: `[Entry]` lines colored green; lines matching `fail`/`error` that also contain `fallback` shown as yellow (warning) instead of red
- **Plugin names**: renamed to PascalCase convention (SentryBlocker, LinkEmbeds, MessageLogger, ContentFilter) matching similar services like Vencord; Themes and ClearURLs unchanged
- **SentryBlocker**: testing status promoted from Alpha → Beta
- **LinkEmbeds**: testing status promoted from Alpha → Beta
- **SidebarInjector**: UPROOTED nav section repositioned above "APP SETTINGS" (was appended at bottom); uses `FindAppSettingsInsertionPoint` to locate insertion index, falls back to append
- Rust installer `detection.rs`: `get_root_exe_path()` updated with 7-strategy Root detection to match bash installer (exact paths → glob patterns → `.desktop` file scan → `/proc/*/exe` → PATH lookup → `locate` database → shallow `find`)

### Fixed

- **Version migration never read Version from INI** — `UprootedSettings.Load()` had no `case "Version":` in its INI parser switch, so the `Version` property always retained the hardcoded default. Version migration (`ForceDisableOnUpgrade`) never detected upgrades because both sides of the comparison were the same default value. Added the missing case.
  - File: `hook/UprootedSettings.cs`
- **Force-disable on upgrade didn't affect ContentFilter** — ContentFilter's enabled state is read from `NsfwFilterEnabled`, not `Plugins["content-filter"]`. The migration loop only set `Plugins[pluginId] = false`, which the UI and engine ignored. Now also sets `NsfwFilterEnabled = false` when disabling `content-filter`.
  - File: `hook/StartupHook.cs`
- **About > Status plugin count** — enabled count was inflated by ghost entries in `settings.Plugins` (e.g. `settings-panel` from legacy migration). Now iterates `KnownPlugins` with the same enabled-check logic as the Plugins page, including the `content-filter` → `NsfwFilterEnabled` special case.
  - File: `hook/ContentPages.cs`
- **Plugin restart banner disappears on tab switch** — restart banner was lost when navigating away from the Plugins tab and back because `initialPluginStates` was re-snapshotted from current settings on each page rebuild. Now persists as static `_launchPluginStates` (set once on first build), and the banner starts visible on rebuild if any plugin already diverges from launch state.
  - File: `hook/ContentPages.cs`
  - File: `hook/ContentPages.cs`
- **Custom ping color no longer bleeds into global accent** — `ApplyPingColorOverride()` was overriding `ThemeAccentColor` and `ThemeAccentBrush` in both Styles[0] and the injected MergedDictionary, causing the entire UI accent (buttons, active states, selection indicators) to change color when a custom ping color was set. The visual tree walk already sets the ping color directly on tagged controls, making the global accent override unnecessary. Removed from both resource dictionaries.
  - File: `hook/ThemeEngine.cs`
- **MessageLogger: deleted cards now inject at correct chat position** — Timestamp-based insertion targeting was unreliable: Root's timestamp property may not resolve, and uncached deletions defaulted to `DateTime.UtcNow` (deletion time, not send time), causing all deleted cards to land after the last visible message (bottom). Replaced with insertion-order tracking (`_orderedIds` list + `_orderedIdIndex` dict): each card now injects after the visible container with the largest collection-order index ≤ the deleted message's index.
  - File: `hook/MessageLogger.cs`
- **Custom theme color swatches** — accent and background swatches in the custom theme section now display the correct color; ThemeEngine visual tree walker was recoloring them because they lacked the `uprooted-no-recolor` tag. Both swatches now tagged correctly in `ContentPages.cs`.
- **Tenor embed bypass too aggressive** — `NativeEmbedDomainRegex` was skipping all `tenor.com` URLs, but Root only natively embeds `media.tenor.com` (direct CDN GIF URLs). Now only `media.tenor.com` is skipped; `tenor.com/view/` pages go through the OG pipeline, extracting `og:image` GIF URL and rendering as animated inline embed.
- **Animated GIF frame compositing** — delta-encoded GIF frames had black pixels and wrong ordering because each frame was decoded into a fresh zeroed bitmap while `PriorFrame` told SKCodec the buffer already contained previous frame data. Now uses a persistent canvas bitmap that accumulates frame data across all frames.
- **Video embed filename toggle** — direct video embeds (`.mp4`, `.webm`, `.mov`) now respect the "Show file names" LinkEmbeds setting; `isImageOnlyEmbed` check previously excluded `VideoId="direct"`.
- **TypeScript layer code quality** (7 fixes) — `after` patch callback now invoked by PluginLoader; `deepMerge` for settings (fixes nested-object overwrite); theme var names derived from `generateCustomVariables` keys; MutationObserver skips own `[data-uprooted]` mutations; sidebar click listener removed in cleanup (no more accumulation); `Object.freeze` on settings global; sentry-blocker `!` assertions replaced with null-check fallbacks.
  - Files: `src/core/pluginLoader.ts`, `src/core/settings.ts`, `src/plugins/themes/index.ts`, `src/plugins/settings-panel/panel.ts`, `src/core/preload.ts`, `src/plugins/sentry-blocker/index.ts`
- **SidebarInjector**: phantom "Unsaved Changes" save bar no longer flashes when switching to Uprooted settings tabs — Root creates a `SaveChangesView` ~200ms after ListBox deselection; `LayoutUpdated` intercept now catches it on the same render frame (zero visible flash). Save bar visibility snapshotted before deselection and defaults to false.
- **Channel badge stale after tab switch** — switching update channel (developer↔stable) at runtime then tabbing away and back showed the old badge text/color because `SidebarInjector._settings` was loaded once at construction. `OnNavItemClicked` now reloads settings before every page build.
  - File: `hook/SidebarInjector.cs`
  - File: `hook/SidebarInjector.cs`
- **SidebarInjector**: save bar restore now only triggers when leaving Uprooted tabs, not on Root-to-Root tab switches — prevents suppressing the legitimate "Unsaved Changes" bar when users actually change Root settings
- **SidebarInjector**: save bar no longer corrupts Root's `IsVisible` binding — replaced all `SetIsVisible`/`ClearValue` calls on the save bar with `Opacity=0`/`MaxHeight=0`/`IsHitTestVisible=false` manipulation. Root never binds these properties, so local values don't conflict with Root's data binding on `IsVisible`. Save bar collapses on Uprooted pages and restores cleanly when returning to Root tabs.
- **SidebarInjector**: UPROOTED section header spacing now matches Root's native section categories (16px top margin on container, -4px bottom margin on header wrapper)
- **SidebarInjector**: switching from Uprooted tabs back to Root tabs is now instant (subscribed `ListBox.SelectionChanged` instead of waiting for 200ms timer poll)
- **SidebarInjector**: eliminated visible pop-in delay when opening settings — now uses `LayoutUpdated` event on MainWindow for same-frame detection instead of relying solely on 200ms timer poll; diagnostics (`DumpVersionRecon`) moved to run after injection so first-open UI is not blocked
- **ThemeEngine**: eliminated theme flash when opening settings or switching tabs — walk bursts (immediate + 50ms/200ms/500ms/1s follow-ups) triggered after injection completes, on ListBox selection changes, and on Uprooted tab switches; added 50ms rapid follow-up to catch async-loaded content faster
- **ProfileBadgeInjector**: fixed `double?` to `double` implicit conversion error in username font size comparison
- **ProfileBadgeInjector**: badge was appearing beside the username (inside the horizontal name row) instead of below it — fixed by walking up the visual tree to find the first vertical StackPanel (`Orientation == Vertical`), then inserting at the username row's index+1; added `IsVerticalPanel()` helper (checks `Orientation` property for StackPanels, falls back to Y-bounds delta comparison for Grid/DockPanel)
- **ProfileBadgeInjector**: badge made smaller and centered (font 12→10, padding 10,4→7,2, dot 8×8→6×6, `HorizontalAlignment=Center`)
- **ProfileBadgeInjector**: badge was appearing on every profile popup indiscriminately — now gated to hardcoded developer usernames (`DeveloperUsernames` HashSet, case-insensitive). Detection switched from 500ms-only polling to event-driven via `OverlayLayer.Children.CollectionChanged` (instant) + 500ms fallback poll for TopLevel popups. Startup delay reduced from 25s to 5s.
- **ProfileBadgeInjector**: `IsProfilePopup` heuristic tightened to reject notification toasts — minimum avatar size raised 30px→40px; added `hasStatusText` (matches "Online"/"Away"/"Busy"/"Offline"/"Do Not Disturb") and `textBlockCount`; conjunction now requires `hasRolesText` OR `(hasLargeText AND hasStatusText)` OR `(hasLargeText AND textBlockCount≥3)`; diagnostic log now prints all five signals.
- **Deploy script**: `deploy-hook.ps1` now relaunches Root via `UprootedLauncher.exe` (sets CLR profiler env vars) instead of bare `Root.exe`
- **MessageLogger**: restored missing `_initialSnapshotIds` and `_lastSubscriptionTime` field declarations (build fix)
- **LinkEmbedEngine**: image embed borders now round all 4 corners (set `HorizontalAlignment("Left")` on imgBorder so it shrink-wraps the image instead of stretching to fill the StackPanel)
- **LinkEmbedEngine**: `HttpGetBytes` now validates HTTP status code (rejects non-2xx) and Content-Type (rejects non-`image/*`) — catches Cloudflare challenge pages and other non-image responses that previously failed silently
- **LinkEmbedEngine**: `HttpGetWithContentType` now validates HTTP status code (rejects non-2xx) for OG metadata fetches
- **LinkEmbedEngine**: image fast-path URLs that serve HTML (e.g. Zipline `/view/`, `/u/` pages with `.png` extension) now fall back to OG metadata fetch to find the real `og:image` URL
- **LinkEmbedEngine**: OG regex bridge pattern fixed to explicitly match complete HTML attributes — handles `<meta>` tags with extra attributes (e.g. `itemProp="image"`, `data-next-head=""`) between `property` and `content`
- **providers.ts**: same OG regex bridge fix applied to TypeScript `parseOpenGraph()` function
- **LinkEmbedEngine**: HTTP timeout increased from 5s to 10s for CDN-hosted images
- **LinkEmbedEngine**: embed card accent color (left border strip) now uses the active theme's accent instead of hardcoded Discord blue (`#5865F2`); site-specific OG theme-colors (Reddit orange, website brand colors) are preserved; card background updates live during color-picker drag and on theme switch via `NotifyThemeChanged()`
- **AutoUpdater**: post-update "Restart" button (was "OK" — required manual relaunch); `AutoUpdater.Instance` set immediately at Phase 4.5d startup so ContentPages restart button always has a reference
- **AutoUpdater**: DLL update now uses rename-then-copy (rename existing to `.old`, copy new, delete `.old`) to avoid file-locked copy failures on Windows
- **AutoUpdater** (dev channel): pre-releases now detected via `/releases?per_page=1` instead of `/releases/latest` (which skips pre-releases)
- **Build scripts portability**: all `tools/*.cmd` scripts (`_build_profiler.cmd`, `_build.bat`, `build_all.cmd`, `build_proxy.cmd`, `build_uiohook_proxy.cmd`, `launch_hooked.cmd`) replaced hardcoded `C:\Users\bash\...` paths with `%~dp0` relative paths and `vswhere.exe` auto-detection
- **test-hook.ps1**: updated from deprecated `CORECLR_*` env vars to `DOTNET_*` (required for .NET 10+)
- **diagnose.ps1**: now checks `DOTNET_*` profiler env vars (primary) and warns if legacy `CORECLR_*` vars are detected
- **verify-install.ps1**: replaced hardcoded user paths with dynamic `$env:APPDATA`/`$env:LOCALAPPDATA` resolution
- **install-hook.ps1**: version now read from `package.json` instead of hardcoded string
- **build-installer.ps1**: renamed from `build_installer.ps1`; updated from Tauri to console TUI installer build; profiling disable now handles both `DOTNET_*` and `CORECLR_*` vars; fixed `cl.exe` stderr banner crash (`$ErrorActionPreference` temporarily set to `Continue` around `2>&1`)
- **deploy-hook.ps1**: JS files now warn when missing instead of silently skipping

### Removed

- **About Themes card** — removed informational "ABOUT THEMES" card from the bottom of the Themes settings page.

### Infrastructure

- **CI: dynamic version** — build workflows no longer hardcode the version; read from `package.json` by default, accepts optional `version` input on `workflow_dispatch`. Releases only publish when triggered manually — push builds run full pipeline and upload CI artifacts only.
- **CI: prerelease flag** — `workflow_dispatch` now accepts a `prerelease` boolean input (defaults `true`) to control whether the GitHub release is marked as a pre-release.
- **Unit testing expanded to 170 tests** — 113 new xUnit tests covering three pure-logic C# modules:
  - `ClearUrlsEngineTests.cs` (58 tests) — all 33 tracking params, fragments, case insensitivity, idempotency, prefix-safety edge cases
  - `UprootedSettingsTests.cs` (22 tests) — INI parse, type coercion, cache/invalidation, save/load roundtrip, migration
  - `MessageStoreTests.cs` (18 tests) — MSG/EDIT/DEL/CLR records, URI encoding roundtrip, malformed line tolerance, Truncate semantics
  - Test stubs for `Logger`, `PlatformPaths`, `AvaloniaReflection`, `VisualTreeWalker` eliminate all disk I/O in tests; sequential collection fixture prevents static-state cross-contamination
  - All 170 tests pass; zero bugs discovered in the three new modules
- **Docker unit test sandbox** — `tests/Dockerfile.unittest` + `tests/run-docker-tests.sh`: builds a clean `mcr.microsoft.com/dotnet/sdk:10.0` container, runs all tests with XPlat code coverage, extracts results to `tests/coverage/`
- **Linux installer Docker sandbox** — `tests/docker-installer/Dockerfile`: Ubuntu 24.04 container runs `install-uprooted-linux.sh` end-to-end (curl shim bypasses GitHub download, fake Root + profile dir provided), `verify.sh` checks 14 post-install conditions (env vars, wrapper script, HTML patch, artifact presence)

### Known Issues

- **MessageLogger: edit detection needs validation** — Edit detection (CollectionChanged Replace events with 5s grace period) is deployed but not yet validated in real-world use. False positives from content changes during send/render may still occur. Edit indicator cards may break message layout in some cases.
- **MessageLogger: deletion pollers need validation** — Async deletion pollers (`HasBeenDeleted` probe every 300ms for 3s) are deployed but unvalidated. If Root sets `HasBeenDeleted` asynchronously after a longer delay, the 3s timeout may be insufficient.
- **ProfileBadgeInjector: false positives on non-profile popups** — The `IsProfilePopup` heuristic (avatar + username + roles/status/text count) may incorrectly identify non-profile popups as profile popups and inject the dev badge.
- **Theme Engine v2: unvalidated in production** — The resource-first rewrite (OKLCH, ThemeDictionaries override) is deployed but not yet tested against Root in production. Preset themes, custom themes, live preview, revert, and variant switching all need manual verification.
- **NSFW filter: unvalidated** — Avalonia-native redesign (Phase 4.5g) is deployed but has not been tested with the Google Vision API in production. Image classification, blur overlay, and click-to-reveal may not function correctly.
- **SilentTyping: unvalidated** — Both C# `TypingBlockerHandler` (Phase 4.5f) and JS fetch/XHR intercept are deployed but have not been validated with two accounts. The dual interception approach (C# + JS) provides defense-in-depth but needs real-world testing.

### Documentation

- Added `docs/PLUGIN_ROADMAP.md` with implementation strategies for 4 planned plugins
- Added `docs/plugins/builtin/message-logger.md` — MessageLogger design reference
- Updated `ARCHITECTURE.md`, `HOOK_REFERENCE.md`, `INSTALLER.md`, `AVALONIA_PATTERNS.md`, `THEME_ENGINE_DEEP_DIVE.md` for current state

---

## [0.3.6-rc] - 2026-02-18

### Added

- **All plugins disabled by default** on new installs — existing users' settings preserved via INI migration (`PluginDefaults.Migrated` flag)
- **Silent Typing plugin** — suppresses typing indicators by intercepting `SetTypingIndicator` calls (contributed by Kurumi Nanase)
  - File: `src/plugins/silent-typing/index.ts`
- **Reddit link embeds** — dedicated handler with `old.reddit.com` OG fetch, subreddit provider label (e.g. "r/programming"), Reddit orange (`#FF4500`) accent color; falls through to generic OG if no title found
- **Video preview embeds** (.mp4, .webm, .mov) — direct video URLs show a dark 16:9 placeholder with centered play button overlay; clicking opens the URL in the default browser. Detected by file extension (`VideoUrlRegex`) or `video/*` Content-Type for extensionless URLs.
- **LinkEmbeds "Show file names" toggle** — image-only embeds now hide the filename title by default; new `LinkEmbedsShowFilenames` setting with live toggle in LinkEmbeds plugin settings lightbox
- **Custom ping/reply highlight color** — standalone override for the mention/reply highlight color, independent of active theme; "Ping Color" toggle inside the Custom Theme card
- **Plugin toggle functionality** — enable/disable individual plugins from settings; state-aware restart banner (hides when user reverts); Restart button launches new Root process
- **ProfileBadgeInjector** — "Uprooted Dev" badge on profile popups (developer channel only, hardcoded dev usernames)
- **Message deletion pollers** — per-item async pollers check `HasBeenDeleted` every 300ms for 3s with epoch-based channel switch cancellation and diagnostic instrumentation

---

## [0.3.5] - 2026-02-18

### Added

- ClearURLs plugin — strips tracking parameters (utm_source, fbclid, gclid, si, etc.) from URLs in the compose editor when the user presses Enter to send (`hook/ClearUrlsEngine.cs`). Hooks AvaloniaEdit.TextEditor's TextArea via Avalonia routed events with `handledEventsToo=true` (all routing strategies required). 33 tracking params, idempotent, fragment-preserving.
- Animated image embeds — `.gif` and `.webp` URLs play inline with frame-accurate timing via SkiaSharp `SKCodec` reflection (`hook/AnimatedImage.cs`), with per-embed animation timers and automatic cleanup on card removal; graceful fallback to static first frame if SkiaSharp frame APIs are unavailable
- Link embeds: direct image URL fast path — `.jpg`, `.png`, `.gif`, `.webp`, `.bmp`, `.svg` URLs render instantly with zero network
- Link embeds: oEmbed discovery — scans HTML for `<link rel="alternate" type="application/json+oembed">` to support any oEmbed-compatible site
- Link embeds: Content-Type gate — skips OG parsing for PDFs, binaries; synthesizes image embed for `image/*` responses
- Link embeds: `twitter:image`, `twitter:title`, `twitter:description` meta tag fallbacks
- Link embeds: oEmbed photo type support — extracts `url` field for photo-type oEmbed responses per spec
- Link embeds: fallback to domain-only card when URL returns no OG metadata or title (e.g. Google Docs login redirects, JS-only SPAs)
- Theme: "Cosmic Smoothie" preset — deep purple accent (`#7328BA`) with dark space background (`#0A041E`), full TreeColorMap + ResourceDictionary + CSS variables
- Settings cache: 10-second TTL cache on `UprootedSettings.Load()` to avoid disk I/O on every 500ms timer tick

### Changed

- Link embeds: Chrome-like User-Agent replaces bot UA (`Uprooted/0.2`) for better site compatibility
- Link embeds: per-request bot UA for Twitter/X and embed-fixer domains (vxtwitter, fxtwitter, fixupx) that serve OG only to crawlers
- Link embeds: embed-fixer domain normalization — fixupx/fxtwitter/fixvx URLs normalized to vxtwitter.com for richer OG metadata with images
- Link embeds: skip Tenor URLs (`tenor.com`, `media.tenor.com`) and rootapp.gg invite links — Root renders these natively, avoids double-embedding
- Link embeds: verbose diagnostic logging gated behind `UPROOTED_VERBOSE=1` env var
- Settings sidebar: "Uprooted" nav item renamed to "About"
- Settings content headers: "Plugins" → "Plugin Settings", "Themes" → "Theme Settings"
- Plugin search box: increased font size (13→14), added horizontal padding and vertical centering for better placeholder text fit
- Linux installer: auto-fetches latest release version from GitHub API (10s timeout, graceful fallback to bundled version)
- Linux installer: `.desktop` file creation is now opt-in via `--desktop` flag (was auto-created)
- Linux installer: download errors now distinguish HTTP 404 from network errors, with actionable suggestions
- Linux installer: validates tarball integrity (gzip magic bytes) before extraction
- Linux installer: build-from-source falls back to pre-built artifacts on failure
- Linux installer: standalone script auto-detects when run outside the repo and uses pre-built artifacts
- Linux installer: post-install/repair messaging uses prominent ANSI box emphasizing "MUST log out and log back in"
- Deploy script: launch Root.exe directly instead of UprootedLauncher.exe

### Fixed

- Settings crash: clicking the back arrow on Uprooted tabs no longer freezes Root — back arrow hidden by position detection, `DetachedFromVisualTree` safety net clears ScrollViewer before recursive detach, Button events use `Click` instead of `PointerPressed`
- Settings header: Uprooted tabs now show page title and preserve X close button, matching Root's native `TabName [spacer] X` format
- Settings section header: "UPROOTED" sidebar section now uses 40px wrapper matching native ListBoxItem height for consistent vertical spacing
- Link embeds: `DecodeJsonString` crash — `Regex.Replace` with `MatchEvaluator` lambda triggers trimmed method; replaced with manual `\uXXXX` decoding loop
- Link embeds: oEmbed endpoint fetch failures — `ReadAsStringAsync` triggers trimmed charset/encoding methods; switched to `ReadAsStreamAsync` + `StreamReader`

## [0.3.2] - 2026-02-17

### Changed

- Installer auto-closes Root before install, repair, and uninstall operations
- Wait for Root process exit before deploying files

### Fixed

- Link embed text readability improvements

## [0.3.0] - 2026-02-17

### Added

- Console TUI installer replacing Tauri GUI (~600KB vs ~100MB)
- `--debug` CLI mode with live installation diagnostics
- Link embeds plugin (Discord-style OpenGraph + YouTube previews)
- Dual-prefix environment variables: `DOTNET_` (primary) + `CORECLR_` (legacy .NET 8/9)
- Phase 4.5 (BrowserDiscovery) and Phase 5 (DotNetBrowser features)
- DotNetBrowserReflection: full type cache, IBrowser discovery via ViewModel chain walking
- Event-driven DotNetBrowser assembly detection (ManualResetEventSlim, 90s timeout)
- KDE Plasma environment variable propagation for profiler loading

### Changed

- Anti-reverse-engineering hardening: stripped symbols, LTO, no PDBs
- Hook log now read from profile directory instead of deploy directory

### Fixed

- `MYGUID` uses unsigned long (8 bytes on Linux x64), fixing all GUID comparisons on Linux
- `Assembly.CreateInstance` replaced with `GetType` + `Activator.CreateInstance`
- Link embeds registered in C# KnownPlugins array
- Wayland white window resolved (disabled WebKit GPU compositing)
- Line endings enforced as LF in bash installer

## [0.2.3] - 2026-02-16

### Fixed

- `TypeLoadException` in profiler context by replacing `ValueTuple` with plain class
- `file://` URL handling on Linux
- CRLF enforcement for cross-platform compatibility

## [0.2.2] - 2026-02-16

### Fixed

- Wayland blank window on Linux (disabled GPU compositing)
- `file://` URL resolution on Linux
- Bash installer improvements

## [0.2.1] - 2026-02-16

### Fixed

- Click-handler crash on Uprooted settings pages
- Release artifact naming consistency

## [0.2.0] - 2026-02-16

First stable baseline. Consolidates all prior development (v0.1.x series) into a versioned release with conventional commit history.

### Added

- **C# .NET hook** with multi-phase startup (Phase 0--5), Avalonia reflection cache (~80 types), sidebar injection, native settings pages
- **TypeScript browser injection** with plugin runtime, bridge proxies, CSS theme engine
- **Tauri/Rust installer** with Root auto-detection, HTML patching, file deployment, environment variable management
- **CLR profiler** (native C) for IL injection into Root.exe on Windows and Linux
- **Self-healing HTML patches** (Phase 0 + FileSystemWatcher) that auto-repair after Root updates
- **Theme engine** with live preview, custom accent/background colors, HSV color picker
- **Sentry blocker** plugin (fetch, XHR, sendBeacon interception)
- **NSFW content filter** plugin
- **Content Filter** plugin card with gear and info lightboxes
- INI-based settings persistence (`UprootedSettings`) as System.Text.Json workaround
- Linux support: AppImage loading, systemd environment.d integration, standalone bash installer
- Arch Linux CI and PKGBUILD packaging
- Plugin testing status badges
- Close-Root popup guard on install/uninstall/repair
- CI pipeline: artifact builds, public repo release publishing

---

[0.4.2]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v0.4.1...v0.4.2
[0.4.1]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v0.4.0...v0.4.1
[0.4.0]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v0.3.6-rc...v0.4.0
[0.3.6-rc]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v0.3.5...v0.3.6-rc
[0.3.5]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v0.3.2...v0.3.5
[0.3.2]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v0.3.0...v0.3.2
[0.3.0]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v0.2.3...v0.3.0
[0.2.3]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v0.2.2...v0.2.3
[0.2.2]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v0.2.1...v0.2.2
[0.2.1]: https://github.com/The-Uprooted-Project/uprooted-private/compare/v0.2.0...v0.2.1
[0.2.0]: https://github.com/The-Uprooted-Project/uprooted-private/releases/tag/v0.2.0
