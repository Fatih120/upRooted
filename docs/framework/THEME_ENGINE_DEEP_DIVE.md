# Theme Engine Deep Dive

> **What this is:** ThemeEngine v2 implementation deep dive — architecture (in-place switching, bind-once walker, WeakRef live preview), data structures, palette generation, and key gotchas.
> **Read when:** Modifying ThemeEngine; fixing theme switch bugs; changing custom theme palette generation; implementing ping color override.
> **Before this:** [ROOT_CONTROL_REFERENCE §Theme System](ROOT_CONTROL_REFERENCE.md#theme-system-mechanics) — Root's 32-key color system that ThemeEngine v2 targets.
> **Color key hex values:** [ROOT_THEME_SYSTEM_FINDINGS](../../research/ROOT_THEME_SYSTEM_FINDINGS.md) — all 32 keys across 3 themes with exact ARGB values.
> **Does NOT cover:** Root's custom control types or style class system → [ROOT_CONTROL_REFERENCE](ROOT_CONTROL_REFERENCE.md).
> **Related docs:** [HOOK_REFERENCE](HOOK_REFERENCE.md) | [ARCHITECTURE](ARCHITECTURE.md) | [AVALONIA_PATTERNS](AVALONIA_PATTERNS.md) | [ROOT_THEME_SYSTEM_FINDINGS](../../research/ROOT_THEME_SYSTEM_FINDINGS.md) | [ROOT_CONTROL_REFERENCE](ROOT_CONTROL_REFERENCE.md)

For the overview, see [Hook Reference](HOOK_REFERENCE.md#theme-engine).

> **Architecture (2026-02-22):** ThemeEngine v2 overrides Root's 32 custom resource keys (`BrandPrimary`, `TextPrimary`, etc.) directly in `Application.Resources.ThemeDictionaries[variant]`. DynamicResource propagation handles most controls instantly. A bind-once visual tree walker converts remaining hardcoded-color controls to DynamicResource bindings on first encounter. In-place theme switching (`PrepareForNewTheme`) eliminates the flash-of-defaults between themes. A WeakReference tracking list enables O(~16) live preview updates. See [`research/ROOT_THEME_SYSTEM_FINDINGS.md`](../../research/ROOT_THEME_SYSTEM_FINDINGS.md) for the full 32-key catalog.

---

## Table of Contents

1. [Overview](#overview)
2. [Resource-First Architecture (Implemented)](#resource-first-architecture-implemented)
3. [Resource Dictionary Injection](#resource-dictionary-injection)
4. [Theme Palette Format](#theme-palette-format)
5. [Live Preview System](#live-preview-system)
6. [Tree Fingerprinting](#tree-fingerprinting)
7. [Color Audit Algorithm](#color-audit-algorithm)
8. [Custom Theme Generation](#custom-theme-generation)
9. [Custom Ping Color Override](#custom-ping-color-override)
10. [Revert Mechanics](#revert-mechanics)
11. [In-Place Theme Switching (PrepareForNewTheme)](#in-place-theme-switching-preparefornewtheme)
12. [Bind-Once Walker (Untagged → DynamicResource)](#bind-once-walker-untagged--dynamicresource)
13. [WeakRef Live Preview (O(n) Fast Path)](#weakref-live-preview-on-fast-path)
14. [CSS Variable Bridge](#css-variable-bridge)
15. [Error Handling](#error-handling)
16. [Key Data Structures](#key-data-structures)
17. [Performance Considerations](#performance-considerations)

---

## Overview

**File:** `hook/ThemeEngine.cs` (~2932 lines)

The theme engine is the largest single component in the Uprooted hook layer. It
transforms Root Communications' default dark-blue UI into arbitrary color schemes at
runtime, without patching any files on disk and without restarting the application.

Root is an Avalonia 11 desktop app. Root's color system uses **32 custom resource keys**
(`BrandPrimary`, `TextPrimary`, `BackgroundPrimary`, etc.) in
`Application.Resources.ThemeDictionaries[variant]`. Every Root view binds to these keys
via `DynamicResourceExtension`. ThemeEngine v2 overrides these keys directly — most
controls auto-update instantly with zero tree walking.

A secondary set of FluentTheme/SimpleTheme keys (`SystemAccentColor`, `ThemeAccentBrush`,
etc.) is also overridden for the handful of base control states that reference them.

### Current Architecture (v2)

**Theme apply** (`ApplyThemeInternal`) uses in-place switching:

| Step | What it does | Mechanism |
|------|-------------|-----------|
| Prep | `PrepareForNewTheme` removes stale keys (theme→theme) or `RevertTheme` (first apply) | Diff-based |
| 1 | Override Root's 32 keys in `ThemeDictionaries[variant]` | DynamicResource propagation |
| 2 | Override `Styles[0].Resources` + inject `MergedDictionary` | FluentTheme/SimpleTheme compat |
| 3 | Delayed tree walk: bind-once converts untagged → DynamicResource, updates dyn-tagged | Visual tree walk |
| 4 | DWM title bar color via Win32 API | P/Invoke |
| 5 | Custom ping color override if set | ThemeDictionaries |

**Live preview** (`UpdateCustomThemeLive`) during color slider drag:
1. Update ThemeDictionaries → DynamicResource-bound controls update at 16ms
2. `UpdateDynTaggedControlsFromPalette` iterates WeakRef list → O(~16) targeted updates
3. 100ms throttled full tree walk as safety net

**Revert to native** (`RevertTheme`) — full cleanup: remove overrides, ClearValue on known colors, purge walker artifacts.

### WalkAndRecolor Control Flow

The visual tree walker (`WalkAndRecolor`) handles three categories per control:

```
for each visual in tree (DFS, max depth 50):
    tag = GetTag(visual)

    if tag == "uprooted-no-recolor":
        SKIP (Uprooted's own UI manages its colors)

    else if tag starts with "dyn-":
        // DYN-TAGGED: parse tag parts, look up palette value, set property
        // e.g. "dyn-fg:TextPrimary,dyn-bg:BackgroundSecondary"
        for each part in tag.Split(','):
            mode, key = parse "dyn-{mode}:{key}"
            brush = palette[key]
            visual.{mode→property} = brush
        register in _dynTaggedControls WeakRef list

    else:
        // UNTAGGED (bind-once): convert to DynamicResource if foreground matches
        colorStr = GetBrushColorString(visual.Foreground)
        if ColorToPaletteKey.TryGetValue(colorStr, out paletteKey):
            BindToDynamicResource(visual, "Foreground", paletteKey)
            SetTag(visual, "dyn-fg:" + paletteKey)
            register in _dynTaggedControls
        else:
            // Unknown color — direct prop.SetValue fallback if in color map
```

### Key Gotchas for ThemeEngine Work

These cause real bugs — check before modifying walker or theme apply code:

1. **Bind-once `else` guard**: The untagged block MUST be guarded by `else` so it only runs for non-dyn-tagged controls. Without it, a control tagged `dyn-bg:X,dyn-bb:Y` gets its tag overwritten to `dyn-fg:TextPrimary`, destroying the bg/bb bindings.

2. **Card borders need both tag AND binding**: Setting `dyn-bb:Border` tag alone is NOT enough for live updates. You must also call `BindToDynamicResource(card, "BorderBrush", "Border")` at creation time. The tag is for the walker; the binding is for DynamicResource propagation.

3. **PointerExited must re-bind**: If a hover handler sets a static brush on a DynamicResource-bound control, `PointerExited` must call `BindToDynamicResource` to restore the binding — not set a static value.

4. **ThemeDictionaries writes trigger `ActualThemeVariantChanged`**: Must use `_applyingThemeDictOverrides` re-entrancy guard on ALL write paths (apply, revert, live preview, ping color).

5. **RegisterDynTaggedControl for external callers**: SidebarInjector tags controls with `dyn-fg:` etc. but those controls aren't discovered by the walker until the next full walk. Call `ThemeEngine.RegisterDynTaggedControl(control)` to add them to the WeakRef list immediately.

6. **Computed palette keys**: `BackgroundButtonOnElevated` and `BackgroundButtonOnSecondary` must be added to `GenerateV2Palette` AND all preset theme dictionaries when adding new derived keys.

### Role in the Dual-Layer Architecture

The theme engine is a C# component on the native Avalonia side. It communicates
with the TypeScript layer indirectly — `ContentPages.UpdateLiveColors()` syncs
static color fields that flow to CSS variables in the browser DOM.
See [CSS Variable Bridge](#css-variable-bridge) for details.

---

## Resource-First Architecture (Implemented)

> **Status:** Complete. ThemeEngine v2 overrides Root's 32 keys directly in `ThemeDictionaries[variant]`. The migration from the old FluentTheme-targeting approach is done. This section documents the override path for reference.

### Why DynamicResource Propagation Works

Root's views bind to 32 custom keys via `DynamicResourceExtension`. Overriding these keys in `ThemeDictionaries` causes instant propagation to all bound controls — no tree walk needed. This fixes all the historical issues (recycled controls, state-change visuals, settings tabs) that required the old 500ms timer walker.

### The Correct Override Path

```csharp
// Step 1: Get the theme dict for the active (or target) variant
// Application.Current.Resources is a ResourceDictionary
// Its ThemeDictionaries is IDictionary<ThemeVariant, IThemeVariantProvider>

var appType = Application.Current.GetType().BaseType; // Avalonia.Application
var resourcesProp = appType.GetProperty("Resources");
var resources = resourcesProp.GetValue(Application.Current);

// resources is ResourceDictionary — get ThemeDictionaries
var themeDictProp = resources.GetType().GetProperty("ThemeDictionaries");
// IDictionary<ThemeVariant, IThemeVariantProvider>
var themeDict = themeDictProp.GetValue(resources) as System.Collections.IDictionary;

// Step 2: Get the wrapper dict for the target variant (e.g. ThemeVariant.Dark)
// Each wrapper is a ResourceDictionary with its colors in MergedDictionaries[0]
// But we add directly to the wrapper dict — direct entries beat MergedDictionaries

// Step 3: Set keys directly on the wrapper ResourceDictionary
// ResourceDictionary implements IDictionary<object, object>
var variantWrapper = themeDict[ThemeVariant.Dark] as System.Collections.IDictionary;
variantWrapper["BrandPrimary"] = CreateImmutableBrush(accentHex);
variantWrapper["TextPrimary"] = CreateImmutableBrush("#F2F2F2");
// ... all 32 keys

// Step 4: Subscribe to ActualThemeVariantChanged to re-apply on native theme switch
// (when user switches from Dark → Light in Root's settings, re-apply to Light dict)
```

**Creating brushes:** Use `ImmutableSolidColorBrush` (matching Root's type) or `SolidColorBrush` (both implement `IBrush`). `SolidColorBrush` is easier via reflection. `ImmutableSolidColorBrush(uint argb)` takes a `uint` ARGB — can cast from `Color.ToUint32()`.

> **CRITICAL: Re-entrancy guard required.** Writing to a ThemeDictionaries variant wrapper via `AddResource`/indexer triggers `ActualThemeVariantChanged` on the same thread. If your `ActualThemeVariantChanged` handler calls `ApplyThemeDictionariesOverride` (which writes more resources), you get an **infinite loop that freezes the UI thread**. Root becomes unresponsive with no error — just a hung window. The fix is a boolean guard flag:
>
> ```csharp
> private bool _applyingThemeDictOverrides;
>
> private int ApplyThemeDictionariesOverride(Dictionary<string, string> rootKeyValues)
> {
>     if (_applyingThemeDictOverrides) return 0; // prevent re-entrancy
>     _applyingThemeDictOverrides = true;
>     try
>     {
>         // ... write to ThemeDictionaries wrappers ...
>     }
>     finally { _applyingThemeDictOverrides = false; }
> }
>
> private void OnActualThemeVariantChanged()
> {
>     if (_applyingThemeDictOverrides) return; // our own write triggered this
>     // ... handle real variant change ...
> }
> ```
>
> This guard must wrap EVERY code path that writes to ThemeDictionaries: apply, revert, live preview, ping color override. Confirmed via live testing — without the guard, Root freezes on first theme application.

### What No Longer Needs Tree Walk

Once Root's 32 keys are overridden correctly:
- All `DynamicResource`-bound controls update instantly (no 500ms timer needed)
- Toggle switches checked state (uses `BrandPrimary`) works immediately
- ComboBox selected state (uses `HighlightStrong`) works immediately
- MessageMarkdown mention colors (uses all mention keys) work immediately
- Tab/nav item colors (uses `TextPrimary`) work immediately

**What still needs tree walk (edge cases):**
- `RootSplitView.PaneBackground` — uses `ThemeControlHighlightLowBrush` from SimpleTheme, not Root's 32 keys (override separately in `Styles[0].Resources`)
- Controls set via `Application.Current.FindResource(ActualThemeVariant, key)` imperatively (like MessageView's hover state) — already DynamicResource-compatible once key is in dict
- Any hardcoded ARGB values — near-zero in Root (only role colors and transparent placeholders)

### The 32-Key Mapping for Custom Themes

For a theme with accent color A and background color B, all 32 keys derive as:

| Key | Derivation |
|-----|-----------|
| `BrandPrimary` | Accent A directly |
| `BrandSecondary` | Accent complementary or lighter |
| `BrandTertiary` | Accent analogous |
| `TextPrimary` | `#F2F2F2` (dark) or `#131313` (light) |
| `TextSecondary` | TextPrimary at 64% alpha |
| `TextTertiary` | TextPrimary at 40% alpha |
| `TextWhite` | `#F2F2F2` always (used for text on branded bg) |
| `BackgroundPrimary` | Background B |
| `BackgroundSecondary` | B slightly lighter |
| `BackgroundTertiary` | B slightly darker |
| `Input` | B, darkest variant |
| `Border` | B desaturated + lightened |
| `HighlightLight` | White at 4% alpha (`#0AFFFFFF`) |
| `HighlightNormal` | White at 10% alpha (`#19FFFFFF`) |
| `HighlightStrong` | White at 19% alpha (`#30FFFFFF`) |
| `Info` | `#F0F250` (keep or use accent) |
| `Warning` | `#E88F3D` (keep) |
| `Error` | `#F03F36` (keep — used for notification badges and message ping border) |
| `Muted` | B with raised lightness |
| `Link` | Accent lighter variant |
| `SelfMention` | Accent at 40% alpha (message row highlight) |
| `SelfMentionBackground` | Accent at 15% alpha (inline mention pill bg) |
| `SelfMentionBorder` | Accent at 30% alpha (inline mention pill border) |
| `OtherMention*` | Link-based at matching alphas |
| `RoleMention*` | Accent secondary at matching alphas |
| `ChannelMention*` | Warning-based at matching alphas |
| `ScrollShadow` | `LinearGradientBrush` (keep) |
| `DropShadow` | `#80000000` (keep) |
| `PopupBoxShadow` | `BoxShadows` (keep) |

### Ping Color Fix

Currently the custom ping color feature targets wrong keys. The correct keys:
- `SelfMention` — message row background wash (what the user sees as the "ping highlight")
- `SelfMentionBackground` + `SelfMentionBorder` — inline @mention pill in message text

Do NOT override `Error` for ping color — it also controls notification badge background, failed send indicator, and retry button color.

---

## Resource Dictionary Injection

> **↳ Canonical source for exact hex values:** [ROOT_THEME_SYSTEM_FINDINGS.md §Dark Theme Color Table](../../research/ROOT_THEME_SYSTEM_FINDINGS.md#dark-theme--complete-color-table) and [§Three-Theme Color Comparison](../../research/ROOT_THEME_SYSTEM_FINDINGS.md#complete-three-theme-color-comparison).
>
> **Note on line numbers:** Sections below reference specific line numbers from earlier versions. These are approximate — use method names (in **bold**) to locate code. The file has grown from ~1280 to ~2932 lines.

### Where Root Stores Its Colors

Root's actual 32 color keys live in `Application.Resources.ThemeDictionaries[variant]`.
ThemeEngine v2 overrides these directly via `OverrideThemeDictKeys`. The Styles[0] and
MergedDictionary overrides below are a secondary compatibility layer for FluentTheme/
SimpleTheme keys that a handful of base Avalonia controls reference.

Root's Avalonia application uses two resource locations that the current engine targets:

- **`Application.Styles[0].Resources`** -- SimpleTheme keys like `ThemeAccentColor`,
  `ThemeAccentBrush`, `ThemeForegroundLowColor`, and `HighlightForegroundColor`.
  These are SimpleTheme keys, not Root's actual theme keys. Root's views bind to
  32 custom keys (`BrandPrimary`, `TextPrimary`, etc.) in `ThemeDictionaries` instead.

- **`Application.Resources.MergedDictionaries`** -- Standard Avalonia FluentTheme keys
  like `SystemAccentColor`, `TextFillColorPrimary`, `ControlFillColorDefault`, etc.
  Root uses `MediaFluentTheme` (not standard `FluentTheme`), and its controls do not
  bind to these keys.

### Phase 1: Styles[0] Override Algorithm

(`hook/ThemeEngine.cs:379-439`)

For each key-value pair in the theme palette:

```
for each (key, hex) in palette:
    1. Save original value:
       - If key not yet saved AND not yet tracked as added:
         - Try to read current value via _r.GetResource(styleRes, key)
         - If value exists: save to _savedOriginals[key]
         - If value is null: add key to _addedKeys (for removal on revert)
    2. Determine type:
       - If key contains "Brush" OR ends with "Fill": create SolidColorBrush
       - Otherwise: create Color struct
    3. Write via _r.AddResource(styleRes, key, value)
```

The type detection heuristic (`hook/ThemeEngine.cs:408`) is critical. Avalonia's
resource system is strongly typed -- setting a `SolidColorBrush` where a `Color` is
expected (or vice versa) causes a silent binding failure. The naming convention is
reliable: Root follows the pattern where Brush keys contain `"Brush"` and Fill keys
end with `"Fill"`.

### Phase 2: MergedDictionary Injection

(`hook/ThemeEngine.cs:441-493`)

```
1. Get Application.Resources via _r.GetAppResources()
2. Get MergedDictionaries list via _r.GetMergedDictionaries(resources)
3. Create new ResourceDictionary via _r.CreateResourceDictionary()
4. For each (key, hex) in palette:
   a. If isBrush: create SolidColorBrush, add to dict
   b. If isColor: create Color, add to dict
      ALSO auto-generate: create SolidColorBrush, add as key + "Brush"
5. mergedDicts.Add(dict)
6. Store dict reference in _injectedDict for later removal
```

The auto-generation step (`hook/ThemeEngine.cs:474-479`) is important: when a Color
key like `SystemAccentColor` is added, a corresponding `SystemAccentColorBrush` is
also created. This covers controls that bind to the Brush variant of a Color key.

### Reflection Mechanics

All resource operations go through `AvaloniaReflection` because Uprooted cannot
reference Avalonia assemblies at compile time. The key methods:

- `_r.CreateResourceDictionary()` -- `Activator.CreateInstance` on the cached
  `ResourceDictionaryType`
- `_r.AddResource(dict, key, value)` -- Calls the indexer setter or `Add` method
  via reflection
- `_r.GetResource(dict, key)` -- Calls the indexer getter via reflection
- `_r.CreateBrush(hex)` -- Creates `SolidColorBrush` via parameterless constructor
  plus Color property setter (the `SolidColorBrush(Color)` constructor is trimmed)
- `_r.ParseColor(hex)` -- Calls `Color.Parse(string)` via cached `MethodInfo`

---

## Theme Palette Format

### Preset Theme Structure

Each preset theme is a `Dictionary<string, string>` mapping resource key names to hex
color strings. The two presets are defined in the static `Themes` dictionary
(`hook/ThemeEngine.cs:2029-2217`):

**Crimson** (`hook/ThemeEngine.cs:2031-2128`): 55 keys, accent `#C42B1C` (deep red)
**Loki** (`hook/ThemeEngine.cs:2130-2215`): 55 keys, accent `#2A5A40` (moss green)

### Key Categories

> **Note:** The 55 keys below are FluentTheme/SimpleTheme-oriented. Root's views actually bind to 32 custom keys (`BrandPrimary`, `TextPrimary`, `BackgroundPrimary`, etc.) stored in `ThemeDictionaries`. A mapping from these 55 keys to Root's 32 actual keys is the planned migration path. See the proposed mapping table in [`research/ROOT_THEME_SYSTEM_FINDINGS.md`](../../research/ROOT_THEME_SYSTEM_FINDINGS.md#impact-on-uprooteds-themeengine).

The 55+ keys fall into these categories:

| Category | Example Keys | Count |
|----------|-------------|-------|
| Root custom theme | `ThemeAccentColor`, `ThemeAccentBrush`, `ThemeForegroundLowColor` | 16 |
| System accent | `SystemAccentColor`, `SystemAccentColorDark1..3`, `SystemAccentColorLight1..3` | 7 |
| Text fill | `TextFillColorPrimary`, `TextFillColorSecondary`, `TextFillColorTertiary`, `TextFillColorDisabled` | 4 |
| Control fill | `ControlFillColorDefault`, `ControlFillColorSecondary`, `ControlFillColorTertiary`, `ControlFillColorDisabled` | 4 |
| Solid backgrounds | `SolidBackgroundFillColorBase`, `...Secondary`, `...Tertiary`, `...Quarternary` | 4 |
| Card/layer | `CardBackgroundFillColorDefault`, `LayerFillColorDefault`, `LayerFillColorAlt` | 4 |
| Accent fill brushes | `AccentFillColorDefaultBrush`, `...SecondaryBrush`, `...TertiaryBrush`, `...DisabledBrush` | 4 |
| Strokes | `ControlStrokeColorDefault`, `CardStrokeColorDefault`, `SurfaceStrokeColorDefault` | 4 |
| Buttons | `ButtonBackground`, `ButtonBackgroundPointerOver`, `ButtonBackgroundPressed`, `ButtonBackgroundDisabled` | 4 |
| ListBox | `ListBoxItemBackgroundPointerOver`, `...Pressed`, `...Selected`, `...SelectedPointerOver`, `...SelectedPressed` | 5 |
| ToggleSwitch | `ToggleSwitchFillOn`, `...PointerOver`, `...Pressed` | 3 |
| ScrollBar | `ScrollBarThumbFill`, `...PointerOver`, `...Pressed` | 3 |
| TextControl | `TextControlBackgroundFocused`, `TextControlBorderBrushFocused` | 2 |
| Selection | `TextSelectionHighlightColor` | 1 |

### Naming Convention for Type Detection

The engine determines whether to create a `SolidColorBrush` or a `Color` struct
based on the key name (`hook/ThemeEngine.cs:408`):

```csharp
bool isBrush = key.Contains("Brush") || key.EndsWith("Fill");
```

Keys like `ThemeAccentBrush` and `ToggleSwitchFillOn` become brushes. Keys like
`ThemeAccentColor` and `SystemAccentColor` become Color structs. For Color keys in
the MergedDictionary, a Brush variant is auto-generated.

### Tree Color Maps

Separate from the resource palette, each theme has a tree color map in the static
`TreeColorMaps` dictionary (`hook/ThemeEngine.cs:865-958`). These map hardcoded ARGB
color strings (as they appear in `Color.ToString()` output) to replacement colors.

Each map covers approximately 25 color mappings:

- Blue accent variants (primary through semi-transparent)
- Structural dark backgrounds (main, darker, near-black, panel, chat)
- Dark borders (primary, darker, gray)
- Semi-transparent text
- Solid text

**Critical constraint:** Every replacement value must be unique within its map. The
reverse map (replacement -> original) would lose data if two originals shared a
replacement. This is enforced by hand for presets and by algorithm for custom themes
(see [Custom Theme Generation](#custom-theme-generation)).

---

## Live Preview System

### Architecture

The live preview system (`UpdateCustomThemeLive`, `hook/ThemeEngine.cs:170-359`) enables
real-time theme updates as the user drags the color picker. It is fundamentally different
from a full `ApplyTheme` call: it skips `RevertTheme()`, skips audits, and updates
resources and the color map in-place rather than tearing down and rebuilding.

### Throttling

(`hook/ThemeEngine.cs:186-188`)

```csharp
long now = Environment.TickCount64;
if (now - _lastLiveUpdateTick < 16) return;
_lastLiveUpdateTick = now;
```

Updates are throttled to a maximum of once per 16ms (~60fps). This prevents the UI
thread from being overwhelmed during rapid color picker drags.

### Bootstrap Guard

(`hook/ThemeEngine.cs:177-183`)

If the custom theme is not yet fully active (no `_injectedDict` or `_activeThemeName`
is not `"custom"`), the method falls through to a full `ApplyCustomTheme()` call first.
This handles the case where the user opens the color picker before ever applying a
custom theme.

### Three-Phase Update

**Phase 1: Update Styles[0].Resources in-place** (`hook/ThemeEngine.cs:200-221`)

Regenerates the full palette from the new colors, then iterates every key and writes
the new value directly into `Styles[0].Resources`. No save/restore of originals --
the originals are already saved from the initial `ApplyCustomTheme` bootstrap.

**Phase 2: Replace MergedDictionary contents** (`hook/ThemeEngine.cs:224-249`)

Same iteration but writing into the existing `_injectedDict`. This is an in-place
update, not a remove-and-readd. The dictionary object stays in the
`MergedDictionaries` list.

**Phase 3: Immediate tree walk** (`hook/ThemeEngine.cs:341-354`)

```csharp
_liveBrushCache = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
var sw = System.Diagnostics.Stopwatch.StartNew();
int liveRecolored = WalkAllWindows();
sw.Stop();
_liveBrushCache = null;
```

A full visual tree walk runs synchronously on the UI thread. The `_liveBrushCache`
is active during this walk -- see [Performance Considerations](#performance-considerations).

### Cross-Mapping for Intermediate Colors

The most subtle part of live preview is the cross-mapping logic
(`hook/ThemeEngine.cs:252-339`). When the user drags the color picker, controls
already show colors from the *previous* live update, not Root's original colors. The
tree walker needs to find these intermediate colors and map them to the new colors.

The algorithm builds a `combinedMap` with three layers:

1. **Base map** -- the new theme's tree color map (Root original -> new replacement)
2. **Previous-map cross-mapping** (`hook/ThemeEngine.cs:261-274`) -- for each entry in
   the previous `_activeColorMap`, if the original has a new replacement, add
   `previousReplacement -> newReplacement`. This catches controls showing the
   previous live update's colors.
3. **Root-originals cross-mapping** (`hook/ThemeEngine.cs:278-286`) -- for each entry in
   `_rootOriginals`, if the stale replacement color has a new mapping, add
   `staleReplacement -> newReplacement`. This catches colors from themes applied
   two or more switches ago.

Additionally, Uprooted's own UI elements (`ContentPages` static colors) are tracked:

```csharp
var oldAccent = NormalizeArgb(ContentPages.AccentGreen);
// ... capture all old values ...
ContentPages.UpdateLiveColors(accentHex, bgHex, palette);
var newAccent = NormalizeArgb(ContentPages.AccentGreen);
// ... add old->new mappings for each ...
```

(`hook/ThemeEngine.cs:298-331`)

This ensures that Uprooted's own settings page UI updates in real time along with
Root's native UI.

### Undo/Revert During Preview

There is no explicit undo for live preview drags. The user can:

1. Type a different hex value in the text box to jump to any color.
2. Apply a preset theme to abandon the custom theme.
3. Close and reopen the settings page (which triggers a full page rebuild).

The live preview state is always consistent because each update regenerates the full
palette and tree map from the raw accent/background hex values.

---

## Tree Fingerprinting

### Purpose

The tree fingerprint (`ComputeTreeFingerprint`, `hook/ThemeEngine.cs:698-722`)
provides a lightweight structural hash of the visual tree. It detects when Root
navigates to a different view (channels, communities, settings, etc.), which causes
new controls to be created with Root's original colors.

### Algorithm

```
function ComputeTreeFingerprint(mainWindow):
    hash = 0
    count = 0
    for each level-1 child c1 of mainWindow:
        hash = hash * 31 + c1.GetType().Name.GetHashCode()
        for each level-2 child c2 of c1:
            count++
            for each level-3 child c3 of c2:
                count++
                for each level-4 child c4 of c3:
                    count++
    return hash XOR (count * 997)
```

The fingerprint walks exactly 4 levels deep. It hashes type names at level 1 (which
captures the major structural components like panels and content controls) and counts
nodes at levels 2-4 (which captures the density of the subtree). The XOR with
`count * 997` mixes the structural hash with the population count.

### Walk Triggers

The fingerprint is not the primary walk trigger. Instead, three mechanisms work in
concert:

1. **Continuous timer** (`ScheduleVisualTreeWalks`, `hook/ThemeEngine.cs:599-624`):
   fires every 500ms, first walk at 200ms after theme apply. This is the background
   safety net that catches any changes the other mechanisms miss.

2. **LayoutUpdated interceptor** (`InstallLayoutInterceptor`, `hook/ThemeEngine.cs:631-666`):
   hooks into `MainWindow.LayoutUpdated` via `SubscribeEvent`. This fires on every
   layout pass, which happens when Root navigates (switches channels, opens settings,
   etc.). Debounced to 80ms minimum interval.

3. **Rapid follow-up** (`ScheduleRapidFollowUp`, `hook/ThemeEngine.cs:672-692`):
   scheduled walks at +200ms, +500ms, +1000ms after a navigation event. This catches
   controls that load asynchronously after the initial navigation.

### All-Windows Walk

(`hook/ThemeEngine.cs:728-738`)

```csharp
private int WalkAllWindows()
{
    int total = 0;
    var topLevels = _r.GetAllTopLevels();
    foreach (var topLevel in topLevels)
    {
        try { total += WalkAndRecolor(topLevel, 0); }
        catch { }
    }
    return total;
}
```

This walks ALL TopLevel instances, not just `MainWindow`. Avalonia creates separate
`PopupRoot` windows for profile cards, context menus, and overlays.
`_r.GetAllTopLevels()` uses Avalonia's internal `WindowImpl.s_instances` list to
discover these.

---

## Color Audit Algorithm

### Purpose

The color audit (`RunColorAudit`, `hook/ThemeEngine.cs:745-826`) is a diagnostic pass
that runs 1.5 seconds after a theme is applied. Its job is to find colors that "escaped"
-- original Root colors that survived all the tree walks and still appear on controls.

### Scheduling

(`hook/ThemeEngine.cs:566-579`)

```csharp
System.Threading.ThreadPool.QueueUserWorkItem(_ =>
{
    Thread.Sleep(1500);
    if (_activeThemeName != auditName) return; // theme changed, skip
    _r.RunOnUIThread(() =>
    {
        try { RunColorAudit(auditMap, auditReverse, auditName); }
        catch (Exception ex) { Logger.Log("Theme", "Audit error: " + ex.Message); }
    });
});
```

The audit is scheduled on a background thread with a 1.5 second delay, then
marshaled to the UI thread for tree access. If the theme changed during the delay
(the user switched themes rapidly), the audit is abandoned.

### Walk Algorithm

(`hook/ThemeEngine.cs:783-826`)

```
function AuditNode(visual, depth, activeMap, reverseMap, originals, staleColors, unmappedColors):
    if depth > 50: return
    if visual.Tag == "uprooted-no-recolor": return

    for each propName in [Background, Foreground, BorderBrush]:
        brush = visual.propName
        colorStr = GetBrushColorString(brush)

        totalProps++

        if originals.Contains(colorStr):
            // This is an original Root color that SHOULD have been recolored
            matchedProps++
            staleColors[propName + ":" + colorStr + " on " + typeName]++

    for each child of visual:
        AuditNode(child, depth + 1, ...)
```

The `originals` set contains the *keys* of `_activeColorMap` -- these are the colors
that should have been replaced. Any control still showing one of these colors is a
"stale" entry.

### Output

The audit logs:
- Total color properties scanned
- Count of properties still showing original colors
- Top 10 stale colors by frequency, with property name, color value, and control type

Example log output:
```
=== COLOR AUDIT (crimson) after 1.5s ===
  Total color props scanned: 847
  Still matching original (need recolor): 3
  --- STALE (original Root colors still present, 3 total) ---
    [2x] Background:#FF0D1521 on Border
    [1x] Foreground:#FFF2F2F2 on TextBlock
=== END AUDIT ===
```

This data drives iterative improvement of the tree color maps.

---

## Custom Theme Generation

### Entry Point

(`hook/ThemeEngine.cs:135-149`)

```csharp
public bool ApplyCustomTheme(string accentHex, string bgHex)
{
    var palette = GenerateCustomTheme(accentHex, bgHex);
    var treeMap = GenerateCustomTreeColorMap(accentHex, bgHex);
    _customPalette = palette;
    _customAccent = accentHex;
    _customBg = bgHex;
    return ApplyThemeInternal("custom", palette, treeMap);
}
```

From two user-chosen colors (accent and background), the engine generates both a
55-key resource palette and a 25-entry tree color map.

### Palette Generation Algorithm

(`GenerateCustomTheme`, `hook/ThemeEngine.cs:1778-1920`)

**Step 1: HSL decomposition and capping**

```csharp
var (ah, asat, al) = ColorUtils.RgbToHsl(accent);
var (bh, bsat, bl) = ColorUtils.RgbToHsl(bg);

double cappedAsat = Math.Min(asat, 0.88);
double cappedAl = Math.Clamp(al, 0.02, 0.65);
```

The accent saturation is capped at 0.88 to prevent garish neon colors. Lightness is
clamped to [0.02, 0.65] -- the low bound of 0.02 allows very dark accents (near-black)
while the high bound of 0.65 prevents washed-out pastels.

**Step 2: Accent shade ladder**

(`hook/ThemeEngine.cs:1789-1794`)

Six variants are generated from the accent hue:

| Variant | Saturation | Lightness |
|---------|-----------|-----------|
| `accentLight1` | `min(0.88, cappedAsat * 1.05)` | `min(0.75, cappedAl + 0.12)` |
| `accentLight2` | `min(0.82, cappedAsat * 1.0)` | `min(0.82, cappedAl + 0.22)` |
| `accentLight3` | `min(0.75, cappedAsat * 0.85)` | `min(0.88, cappedAl + 0.32)` |
| `accentDark1` | `min(0.88, cappedAsat * 1.1)` | `max(0.01, cappedAl - 0.12)` |
| `accentDark2` | `min(0.88, cappedAsat * 1.1)` | `max(0.005, cappedAl - 0.20)` |
| `accentDark3` | `min(0.85, cappedAsat * 1.0)` | `max(0.002, cappedAl - 0.28)` |

Lighter variants decrease saturation slightly to avoid over-saturation at higher
lightness. Darker variants increase saturation slightly to maintain vibrancy at
lower lightness.

**Step 3: Background hierarchy**

(`hook/ThemeEngine.cs:1798-1806`)

```csharp
double bgHue = bh;
double bgSat = Math.Clamp(bsat, 0.06, 0.35);
double bgL = Math.Clamp(bl, 0.03, 0.18);

var bgBase        = ColorUtils.HslToHex(bgHue, bgSat, bgL);
var bgSecondary   = ColorUtils.HslToHex(bgHue, bgSat * 0.95, bgL + 0.035);
var bgTertiary    = ColorUtils.HslToHex(bgHue, bgSat * 0.90, bgL + 0.07);
var bgQuarternary = ColorUtils.HslToHex(bgHue, bgSat * 0.85, bgL + 0.11);
```

Backgrounds use the background color's own hue (not the accent hue), preserving the
user's intent. Saturation is clamped to [0.06, 0.35] so backgrounds are always subtly
tinted but never vivid. Lightness is clamped to [0.03, 0.18] to keep backgrounds dark.
Each level increases lightness by 3.5% and decreases saturation by 5%.

**Step 4: Hue-tinted text**

(`hook/ThemeEngine.cs:1809-1815`)

```csharp
var textColor = ColorUtils.DeriveTextColorTinted(bgBase, accent);
var (th, ts, tl) = ColorUtils.RgbToHsl(textColor);
var textPrimary = ColorUtils.HslToHex(th, ts, Math.Min(0.98, tl + 0.02));
```

`DeriveTextColorTinted` (from `hook/ColorUtils.cs:95-109`) creates text that carries
a subtle hue from the accent color. For dark backgrounds, this means near-white text
with 3-12% accent saturation. The primary text variant is nudged 2% brighter.

Alpha variants are generated at 78%, 55%, and 36% for secondary, tertiary, and
disabled text.

**Step 5: Derived values**

The remaining ~35 keys are derived from the accent and background colors using alpha
overlays and the shade ladder:

- Control fills: accent with alpha `0x1A` / `0x14` / `0x0C`
- Card fill: accent with alpha `0x20`
- Strokes: accent with alpha `0x42` / `0x2C` / `0x36` / `0x48`
- Buttons: accent with alpha `0x1A` (normal), `0x2C` (hover), `0x14` (pressed)
- ListBox items: accent with alpha `0x1A` through `0x35`
- ToggleSwitch: raw accent, `accentLight1`, `accentDark1`
- ScrollBar: accent with alpha `0x58` / `0x88` / full
- Highlight foreground: if accent lightness < 0.4, uses `accentLight2`; otherwise
  uses the raw accent

### Tree Color Map Generation

(`GenerateCustomTreeColorMap`, `hook/ThemeEngine.cs:1928-2022`)

This function maps Root's ~25 hardcoded ARGB colors to custom-derived equivalents.
The approach uses a dual-hue strategy:

- **Backgrounds** use the background hue (`bh`) with the clamped saturation/lightness
- **Borders** use the accent hue (`ah`) for a dual-tone effect
- **Accents** use the raw accent color and its shade ladder
- **Text** uses the hue-tinted text color

Example mapping for structural backgrounds:

```csharp
map["#ff0d1521"] = Hsl(bgHue, bgSat, bgL);                    // Main dark bg
map["#ff07101b"] = Hsl(bgHue, bgSat * 1.05, max(0.02, bgL - 0.03)); // Darkest bg
map["#ff101c2e"] = Hsl(bgHue, bgSat * 0.97, bgL + 0.025);     // Slightly lighter
```

Each replacement gets a unique lightness value to maintain visual hierarchy.

### Uniqueness Enforcement

(`hook/ThemeEngine.cs:1997-2019`)

After generating all mappings, a uniqueness pass ensures no two replacement values
collide:

```csharp
var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
foreach (var key in keys)
{
    var val = map[key];
    while (seen.Contains(val))
    {
        // Nudge green channel by +1
        byte g = Math.Min((byte)255, (byte)(Convert.ToByte(hex[4..6], 16) + 1));
        val = $"#{a:X2}{r:X2}{g:X2}{b:X2}";
        map[key] = val;
    }
    seen.Add(val);
}
```

This nudges the green channel by +1 until uniqueness is achieved. The visual
difference of a single green channel step is imperceptible, but it ensures the
reverse map (replacement -> original) is bijective.

---

## Custom Ping Color Override

The custom ping color feature lets users override the mention/reply highlight color
independently from the active theme. The override persists across theme switches --
applying a new theme will change everything except the highlight color.

### Target Resource Keys

> **Note:** The correct target keys for mention/ping highlighting are Root's `SelfMentionBackground`, `SelfMentionBorder`, and `SelfMention` in `ThemeDictionaries`. The current implementation targets `HighlightForegroundColor` (a SimpleTheme key that Root's views don't bind to), which is why the tree walk is needed to propagate the ping color.

| Key | Location | Override Value |
|-----|----------|---------------|
| `HighlightForegroundColor` | `Styles[0].Resources` + `MergedDictionary` | User's chosen color |
| `HighlightForegroundBrush` | `Styles[0].Resources` + `MergedDictionary` | User's chosen color (auto-generated brush) |
| `TextSelectionHighlightColor` | `Styles[0].Resources` + `MergedDictionary` | User's chosen color with `0x60` alpha |

### How It Works

**`ApplyPingColorOverride()`** is the core method. It:

1. Parses the stored hex color (`_customPingColor`).
2. Generates a semi-transparent variant (`0x60` alpha) for text selection.
3. Overrides the three keys in `Styles[0].Resources` (Color + Brush).
4. Overrides the same keys in the injected `MergedDictionary`.

**Integration points:**

- **Phase 6 in `ApplyThemeInternal()`**: After all theme phases complete, re-applies
  the ping color override. This ensures theme switches don't clobber the override.
- **End of `UpdateCustomThemeLive()`**: After live preview updates resources, re-applies
  the override so color picker drag doesn't reset it.
- **Phase 3.5 at startup**: `StartupHook` loads `CustomPingColor` from settings and
  calls `SetCustomPingColor()` after theme initialization.

### Clearing the Override

**`ClearCustomPingColor()`** restores the highlight keys to the active theme's values:

1. Sets `_customPingColor = null`.
2. Reads the active theme's palette via `GetPalette()`.
3. Calls `RestoreThemeHighlightKeys(palette)` to write the theme's original highlight
   values back to both resource locations.
4. **Default-dark fallback**: If no theme is active (`_activeThemeName == null`), uses
   Root's hardcoded defaults (`#3B6AF8` for highlight, `#603B6AF8` for selection).

### UI Integration

The "HIGHLIGHT OVERRIDE" card on the Themes settings page (`ContentPages.BuildPingColorSection()`) provides:

- Toggle indicator (filled/empty circle) showing whether the override is active.
- Color input row (hex TextBox + swatch) with live preview on change.
- Reset button to clear the override.
- Debounced auto-save (1s timer).
- Swatch tagged `uprooted-no-recolor` to prevent tree walker interference.

---

## Revert Mechanics

### Overview

`RevertTheme()` (`hook/ThemeEngine.cs:1174-1320`) undoes all theme changes. The
algorithm is carefully ordered: resources must be restored BEFORE the visual tree
walk so that `ClearValue` causes `DynamicResource` bindings to resolve correctly.

### Step-by-Step Algorithm

**Step 0: Disable active theme**

```csharp
_walkTimer?.Dispose();
_walkTimer = null;
var savedActiveMap = _activeColorMap;
_activeColorMap = null;  // Disables layout interceptor
```

(`hook/ThemeEngine.cs:1176-1185`)

The walk timer is cancelled and `_activeColorMap` is set to null immediately. This
prevents the layout interceptor from re-applying theme colors during the revert
process. The active map is saved for the purge step.

**Step 1: Restore Styles[0].Resources**

(`hook/ThemeEngine.cs:1191-1227`)

Two substeps:

1. For each entry in `_savedOriginals`, write the original value back:
   ```csharp
   _r.AddResource(styleRes, key, original);
   ```

2. For each key in `_addedKeys` (keys that had no original), remove them:
   ```csharp
   _r.RemoveResource(styleRes, key);
   ```

**Step 2: Remove MergedDictionary**

(`hook/ThemeEngine.cs:1230-1247`)

```csharp
mergedDicts.Remove(_injectedDict);
_injectedDict = null;
```

**Step 3: Targeted purge via PurgeKnownColors**

(`hook/ThemeEngine.cs:1249-1310`)

This is the most sophisticated step. A naive approach would `ClearValue` on every
control in the tree, but that would destroy Root's structural backgrounds that were
never theme-related. Instead, the purge builds a known-color set and only clears
controls whose current color is in that set.

```
knownColors = union of:
    - All keys and values from savedActiveMap
    - All keys and values from _rootOriginals
    - All keys and values from _reverseColorMap
```

The `PurgeKnownColors` walker (`hook/ThemeEngine.cs:1365-1436`):

```
function PurgeKnownColors(visual, depth, knownColors):
    if visual.Tag == "uprooted-no-recolor": skip
    for each propName in [Background, Foreground, BorderBrush, Fill]:
        colorStr = GetBrushColorString(visual.propName)
        if knownColors.Contains(colorStr):
            ClearValue(visual, propName)
            if visual.propName is now null:
                // DynamicResource didn't reassert -- explicit fallback
                if _rootOriginals has colorStr:
                    restore = _rootOriginals[colorStr]
                else if _reverseColorMap has colorStr:
                    restore = _reverseColorMap[colorStr]
                SetValueLocalPriority(visual, propName, restore)
        else:
            // Track as orphan for diagnostics
    recurse into children
```

The null-fallback logic is critical. When `ClearValue` removes a `LocalValue`
override, controls with `DynamicResource` bindings automatically re-resolve from
the now-restored resource dictionaries. But controls with hardcoded brushes (no
`DynamicResource` binding) go null after `ClearValue`. For these, the engine falls
back to `_rootOriginals` (the persistent map of all theme replacements back to Root's
true original colors) to restore the correct value.

**Step 4: Cleanup**

```csharp
_savedOriginals.Clear();
_addedKeys.Clear();
UpdateTitleBarColor(DefaultDarkBg);  // "#0D1521"
_activeThemeName = null;
_reverseColorMap = null;
```

### Revert Follow-Ups

(`ScheduleRevertFollowUps`, `hook/ThemeEngine.cs:1326-1353`)

After the immediate revert, follow-up walks are scheduled at +500ms, +1500ms, and
+3000ms. These catch controls that were created after the initial revert (e.g., lazy-
loaded views or async content). Each follow-up uses the saved reverse map and stops
if a new theme has been applied in the interim.

---

## In-Place Theme Switching (PrepareForNewTheme)

### Problem: Flash of Root Defaults

Prior to this optimization, `ApplyThemeInternal` called `RevertTheme()` before every theme apply. This removed ALL ThemeDictionary overrides (DynamicResource bindings fell back to Root defaults = visible color flash), then re-added all overrides (second full resolution pass). The result was a brief but noticeable flash of Root's default colors during every theme-to-theme switch.

### Solution: Diff-Based Key Removal

`PrepareForNewTheme(Dictionary<string, string> newRootKeys)` replaces `RevertTheme()` for **theme-to-theme transitions** (when `_activeThemeName != null`). It does a lightweight diff:

```csharp
private void PrepareForNewTheme(Dictionary<string, string> newRootKeys)
{
    // 1. Dispose walk timer
    _walkTimer?.Dispose();
    _walkTimer = null;

    // 2. Remove stale ThemeDictionary keys (in old theme but not new).
    //    Only diff COLOR keys — SVG keys (ending "SVG") are managed by SwapSvgPathsIfNeeded.
    var staleColorKeys = _overriddenThemeDictKeys
        .Where(k => !k.EndsWith("SVG") && !newRootKeys.ContainsKey(k))
        .ToList();
    foreach (var key in staleColorKeys)
        RemoveThemeDictKey(key);

    // 3. Clear saved Style originals (will be re-populated by Phase 2).
    _savedStyleOriginals.Clear();
    _addedStyleKeys.Clear();

    // 4. Reset state for fresh apply — DO NOT: ClearValue on walker-recolored controls,
    //    RestoreTaggedControls, ReadLiveRootColors, toggle variant, update title bar.
    //    New theme's OverrideThemeDictKeys overwrites in-place.
    _activeThemeName = null;
    _customPalette = null;
}
```

**Key insight:** Shared keys (present in both old and new themes) stay in the ThemeDictionaries during the transition. When `ApplyThemeInternal` writes the new theme's values, these keys are overwritten in-place. DynamicResource-bound controls see a single value change, never a revert-to-default.

**`RevertTheme()` is unchanged** for the revert-to-native path (clicking Native preset). Full revert with ClearValue, RestoreTaggedControls, variant restore is correct there.

### Decision Logic in ApplyThemeInternal

```csharp
// Theme-to-theme transition: lightweight in-place prep (no flash of Root defaults).
// Only full RevertTheme for first-time apply (no active theme yet).
if (_activeThemeName != null)
    PrepareForNewTheme(rootKeys);
else
    RevertTheme(forceNativeRefresh: false, restoreOriginalVariant: false);
```

---

## Bind-Once Walker (Untagged → DynamicResource)

### Problem: Destructive prop.SetValue on DynamicResource-Bound Controls

The visual tree walker's untagged-Foreground handler used `prop.SetValue()` on Root's DynamicResource-bound TextBlocks. This overwrote the binding at LocalValue priority. Those controls then stopped responding to ThemeDictionary updates until the next walker pass (100ms throttle). During live preview (dragging color sliders), this created visible desync — native text lagged behind the color picker.

### Solution: Bind-Once Pattern

Instead of destructive `prop.SetValue`, the walker now:

1. Detects untagged controls with foreground colors matching known palette values
2. Looks up which palette key the color maps to via `ColorToPaletteKey`
3. Calls `BindToDynamicResource(visual, "Foreground", paletteKey)` to create a live resource binding
4. Tags the control with `dyn-fg:{paletteKey}` so subsequent walks use the efficient tag-based path
5. Registers the control in the WeakRef list for live preview updates

```csharp
// Static lookup: known ARGB text colors → palette key names
private static readonly Dictionary<string, string> ColorToPaletteKey =
    new(StringComparer.OrdinalIgnoreCase)
{
    ["#FFF2F2F2"] = "TextPrimary",   // Dark theme primary
    ["#A3F2F2F2"] = "TextSecondary", // Dark theme secondary (alpha)
    ["#66F2F2F2"] = "TextTertiary",  // Dark theme tertiary (alpha)
    ["#FF131313"] = "TextPrimary",   // Light theme primary
    ["#FF282828"] = "TextSecondary", // Light theme secondary
    ["#FF5E5E5E"] = "TextTertiary",  // Light theme tertiary
    // ... additional known variants
};
```

**The `else` guard is critical:** The bind-once block only runs for controls that are NOT already dyn-tagged. Without this guard, a control with tag `dyn-bg:BackgroundElevated,dyn-bb:Border` would have its tag overwritten to `dyn-fg:TextPrimary`, destroying the bg/bb bindings.

**Fallback:** If `BindToDynamicResource` fails or the color doesn't map to a known palette key, the walker falls back to direct `prop.SetValue` (previous behavior).

### Benefits

- **DynamicResource-bound controls** — binding preserved, auto-updates from ThemeDictionary changes
- **Hardcoded controls** (lazy popups, programmatic text) — get DynamicResource binding, self-healing
- **Future controls** Root creates after initial walk — caught on next walk pass, bound once

---

## WeakRef Live Preview (O(n) Fast Path)

### Problem: Full Tree Walk for ~16 Controls

Live custom theme preview (dragging color sliders) triggered a full visual tree walk on every 100ms tick — traversing ~500+ nodes to find and update ~16 dyn-tagged controls. The traversal cost was the same whether 2 or 200 controls needed updating.

### Solution: WeakReference Tracking List

```csharp
private readonly List<WeakReference<object>> _dynTaggedControls = new();
```

When the walker processes a dyn-tagged control (either pre-existing or newly tagged by bind-once), it registers it:

```csharp
_dynTaggedControls.Add(new WeakReference<object>(visual));
```

During live preview, `UpdateDynTaggedControlsFromPalette` iterates this list directly:

```csharp
private int UpdateDynTaggedControlsFromPalette(Dictionary<string, string> palette)
{
    int updated = 0;
    for (int i = _dynTaggedControls.Count - 1; i >= 0; i--)
    {
        if (!_dynTaggedControls[i].TryGetTarget(out var visual))
        {
            _dynTaggedControls.RemoveAt(i); // GC'd control
            continue;
        }
        var tag = _r.GetTag(visual);
        if (tag == null || !tag.StartsWith("dyn-")) continue;
        // Parse tag parts: dyn-fg:TextPrimary, dyn-bg:BackgroundSecondary, etc.
        foreach (var part in tag.Split(','))
        {
            // ... look up palette value, create brush, set property
        }
    }
    return updated;
}
```

### Live Preview Flow

1. ThemeDictionary updates → DynamicResource-bound controls update instantly (~16ms frame)
2. `UpdateDynTaggedControlsFromPalette()` → ~16 WeakRef iterations, O(1) per control
3. 100ms throttled full walk still fires — discovers new dyn-tagged controls, populates WeakRef list

### Public Registration API

External callers (SidebarInjector) can register controls that were dyn-tagged outside the walker:

```csharp
public void RegisterDynTaggedControl(object control)
{
    _dynTaggedControls.Add(new WeakReference<object>(control));
}
```

### Computed Palette Keys

Two derived keys enable DynamicResource binding for computed button backgrounds:

| Key | Derivation | Use |
|-----|-----------|-----|
| `BackgroundButtonOnElevated` | `DeriveHighlightSurface(Elevated, 12)` | Theme card gear button |
| `BackgroundButtonOnSecondary` | `DeriveHighlightSurface(Secondary, 12)` | Plugin card gear/info buttons |

`DeriveHighlightSurface` is luminance-aware: lightens on dark backgrounds, darkens on light backgrounds (same logic as `AdjustForHighlight` in ContentPages). These keys are generated in `GenerateV2Palette` and all 3 preset themes, enabling DynamicResource binding for surfaces that were previously computed at build time.

---

## CSS Variable Bridge

### How Native Themes Sync with TypeScript

The C# theme engine operates on Avalonia's native resource system and visual tree.
The TypeScript layer operates on the embedded Chromium browser's DOM and CSS. These
two worlds are bridged through `ContentPages.UpdateLiveColors()`.

When a theme is applied or updated live, `ContentPages` updates its static color
fields:

```csharp
// hook/ThemeEngine.cs:307
ContentPages.UpdateLiveColors(accentHex, bgHex, palette);
```

These static fields (`ContentPages.AccentGreen`, `ContentPages.CardBg`,
`ContentPages.TextWhite`, `ContentPages.TextMuted`, `ContentPages.TextDim`) are used
both by the C# side (for building Uprooted's settings pages) and are injected into
the HTML patches as part of `window.__UPROOTED_SETTINGS__`.

The TypeScript theme plugin reads these settings at startup and translates them into
CSS custom properties (e.g., `--uprooted-accent`, `--uprooted-bg-primary`). Native
Avalonia UI and browser DOM UI thus share the same color values through this indirect
bridge.

### Limitations

The bridge is not truly real-time. CSS variables in the browser are set at page load
from the settings object. During a live preview drag, the native Avalonia side updates
instantly, but the browser side does not update until the next page load or explicit
CSS variable update from the TypeScript layer. The primary theming target for the C#
engine is Root's Avalonia UI, not the embedded browser content.

---

## Error Handling

### Failure Modes and Recovery

The theme engine is designed to be resilient. Every tree walk, resource operation, and
reflection call is wrapped in try-catch blocks. The principle is: a failure in one
control should never prevent the rest of the tree from being themed.

**Resource creation failure**: If `_r.CreateBrush(hex)` or `_r.ParseColor(hex)` returns
null (malformed hex, reflection failure), the key is silently skipped
(`hook/ThemeEngine.cs:413-427`). The theme applies with a reduced palette.

**Walk failure**: Each `WalkAndRecolor` call in `WalkAllWindows` is individually
wrapped (`hook/ThemeEngine.cs:734-735`):
```csharp
try { total += WalkAndRecolor(topLevel, 0); }
catch { }
```
A crash in one window's tree does not prevent other windows from being walked.

**Property access failure**: Inside `CollectColorChanges`, each property read is
individually wrapped (`hook/ThemeEngine.cs:1042-1062`). Properties that throw
(e.g., trimmed getters, disposed controls) are skipped.

**Layout interceptor failure**: If the `LayoutUpdated` subscription fails
(`hook/ThemeEngine.cs:657-665`), the engine falls back to the 500ms timer. The
interceptor is an optimization, not a requirement.

**Revert failure**: If `RevertTheme()` partially fails (e.g., MergedDictionary removal
throws), the state cleanup still runs (`hook/ThemeEngine.cs:1312-1319`). The engine
may leave some visual artifacts, but it will not be in an inconsistent state for the
next theme apply.

**Audit failure**: The color audit runs on a background thread and swallows all
exceptions (`hook/ThemeEngine.cs:577`). A failed audit has zero impact on theme
functionality.

### The `uprooted-no-recolor` Tag

Controls tagged with `"uprooted-no-recolor"` are excluded from all tree walks:

- `CollectColorChanges` (`hook/ThemeEngine.cs:1033`)
- `AuditNode` (`hook/ThemeEngine.cs:791`)
- `PurgeKnownColors` (`hook/ThemeEngine.cs:1368`)
- `WalkAndRestore` (`hook/ThemeEngine.cs:1119`)

This tag is applied to Uprooted's own UI elements that manage their own colors (e.g.,
theme preview swatches on the Themes settings page). Without this tag, the tree walker
would fight with ContentPages over the colors of these elements.

---

## Key Data Structures

### Instance Fields

| Field | Type | Purpose |
|-------|------|---------|
| `_r` | `AvaloniaReflection` | Cached reflection handles for all Avalonia operations |
| `_injectedDict` | `object?` | Reference to our `ResourceDictionary` in `MergedDictionaries` |
| `_activeThemeName` | `string?` | Current theme name (`"crimson"`, `"loki"`, `"custom"`, or null) |
| `_savedOriginals` | `Dictionary<string, object?>` | Original resource values from `Styles[0]` for revert |
| `_addedKeys` | `HashSet<string>` | Keys we added to `Styles[0]` that had no original (remove on revert) |
| `_rootOriginals` | `Dictionary<string, string>` | Persistent map: any theme replacement -> Root's true original color |
| `_activeColorMap` | `Dictionary<string, string>?` | Current forward map: original ARGB -> replacement ARGB |
| `_reverseColorMap` | `Dictionary<string, string>?` | Current reverse map: replacement ARGB -> original ARGB |
| `_customPalette` | `Dictionary<string, string>?` | Full palette for the active custom theme |
| `_customAccent` | `string?` | Raw accent hex for the active custom theme |
| `_customBg` | `string?` | Raw background hex for the active custom theme |
| `_walkTimer` | `System.Threading.Timer?` | 500ms continuous walk timer |
| `_walkCount` | `int` | Walk iteration counter (for logging) |
| `_layoutInterceptorInstalled` | `bool` | Whether `LayoutUpdated` hook is active |
| `_lastLayoutWalkTick` | `long` | Debounce timestamp for layout interceptor |
| `_lastLiveUpdateTick` | `long` | Throttle timestamp for live preview |
| `_liveBrushCache` | `Dictionary<string, object>?` | Per-update brush cache during live preview walks |
| `_dynTaggedControls` | `List<WeakReference<object>>` | Discovered dyn-tagged controls for O(n) live preview |
| `_overriddenThemeDictKeys` | `HashSet<string>` | ThemeDictionary keys we've overridden (for PrepareForNewTheme diff) |

### Static Lookup

| Field | Type | Purpose |
|-------|------|---------|
| `ColorToPaletteKey` | `Dictionary<string, string>` | Maps known ARGB text colors to palette key names for bind-once targeting |

### The `_rootOriginals` Map

(`hook/ThemeEngine.cs:32-34`)

```csharp
private readonly Dictionary<string, string> _rootOriginals =
    new(StringComparer.OrdinalIgnoreCase);
```

This is the most important data structure for multi-theme correctness. It is a
*persistent* map that accumulates across theme switches. The key is any replacement
color ever used by any theme; the value is the original Root color.

It is pre-populated in the constructor from all static `TreeColorMaps`
(`hook/ThemeEngine.cs:42-51`) and grows as custom themes are applied. It enables:

1. **Stale color recovery during theme switching** -- when switching from Crimson to
   Loki, controls may still show Crimson colors that were missed by the revert walk.
   `_rootOriginals` maps these Crimson colors back to Root's originals, which then
   map to Loki's colors in the combined map.

2. **Null-fallback during purge** -- when `ClearValue` leaves a property null during
   revert, `_rootOriginals` provides the correct Root original color.

### Static Dictionaries

| Dictionary | Type | Purpose |
|-----------|------|---------|
| `Themes` | `Dict<string, Dict<string, string>>` | Preset theme palettes (55 keys each) |
| `TreeColorMaps` | `Dict<string, Dict<string, string>>` | Preset tree color maps (~25 entries each) |

### Color Map Lifecycle

```
ApplyTheme("crimson") [first apply, no active theme]:
    1. RevertTheme() — full revert (safe when no active theme)
    2. _activeColorMap = combinedMap (base + cross-mapped + stale-mapped)
    3. _reverseColorMap = inverse of combinedMap
    4. _rootOriginals += crimson's replacement->original entries

ApplyTheme("loki") [switching from crimson]:
    1. PrepareForNewTheme(lokiKeys) — removes stale crimson-only keys, shared keys stay
    2. _activeColorMap = combinedMap (loki base + crimson cross-map + stale-map)
    3. _reverseColorMap = inverse of combinedMap

RevertTheme() [reverting to Native]:
    1. _activeColorMap = null
    2. _reverseColorMap = null (after purge completes)
    3. _rootOriginals persists (never cleared)
    4. _dynTaggedControls cleared
```

---

## Performance Considerations

### Tree Walk Cost

Each tree walk reads 4 properties (`Background`, `Foreground`, `BorderBrush`, `Fill`)
on every visual tree node via reflection. For a typical Root window with 500+ nodes,
the walk takes approximately 2-5ms.

The walk is split into two passes (`hook/ThemeEngine.cs:970-1023`):

**Pass 1 (Collect)**: Read-only traversal that builds a list of pending changes.
This avoids modifying the tree during traversal, which could cause iterator
invalidation or missed nodes.

**Pass 2 (Apply)**: Iterate the pending list and apply changes. This is safe because
the tree structure is not being traversed during modification.

### Brush Caching in Live Preview

(`hook/ThemeEngine.cs:986-996`)

During live preview, `_liveBrushCache` maps replacement hex strings to already-created
`SolidColorBrush` objects:

```csharp
if (_liveBrushCache != null)
{
    if (!_liveBrushCache.TryGetValue(replacement, out newBrush))
    {
        newBrush = _r.CreateBrush(replacement);
        if (newBrush != null)
            _liveBrushCache[replacement] = newBrush;
    }
}
```

Without this cache, the engine would create a new `SolidColorBrush` for every control
that shares a color. With ~25 unique replacement colors and 500+ nodes, this saves
hundreds of `Activator.CreateInstance` calls per live update.

The cache is scoped to a single update cycle (created before the walk, nulled after).
This prevents brush objects from accumulating across many rapid updates.

### Style Priority vs. LocalValue Priority

> **Note:** The method formerly called `SetValueStylePriority` has been renamed to `SetValueLocalPriority` — the original name was misleading. `Enum.ToObject(bpType, 0)` resolves to `BindingPriority.LocalValue`, NOT Style priority. The field `_bindingPriorityStyle` was also renamed to `_bindingPriorityLocal`.

In the dyn-tagged walker, brushes are set at **LocalValue priority** via `prop.SetValue()` for controls whose DynamicResource binding didn't survive or wasn't established. The bind-once pattern (`BindToDynamicResource`) avoids this by creating live resource bindings that auto-update from ThemeDictionary changes.

### Live Preview Performance: O(n) vs O(N)

With the WeakRef tracking list, live preview updates are now O(n) where n is the number of dyn-tagged controls (~16), not O(N) where N is the total visual tree size (~500+). The breakdown:

| Path | Cost | When |
|------|------|------|
| ThemeDictionary update | O(1) — DynamicResource propagation | Every live preview tick |
| `UpdateDynTaggedControlsFromPalette` | O(~16) WeakRef iterations | Every live preview tick |
| Full tree walk | O(500+) nodes | Non-live: theme apply, sidebar injection, burst walks |

### Throttle and Debounce Summary

| Mechanism | Interval | Purpose |
|-----------|---------|---------|
| Live preview throttle | 16ms | Cap at ~60fps during color picker drag |
| Layout interceptor debounce | 80ms | Prevent rapid-fire walks during layout |
| Continuous walk timer | 500ms (first at 200ms) | Background safety net |
| Rapid follow-up walks | 200ms, 500ms, 1000ms | Catch async-loaded controls |
| Color audit delay | 1500ms | Allow tree to settle before diagnostic |
| Revert follow-ups | 500ms, 1500ms, 3000ms | Catch post-revert lazy loads |

### Reflection Cost Management

All Avalonia types, properties, and methods are resolved once during startup by
`AvaloniaReflection.Resolve()` and cached as `Type`, `PropertyInfo`, and `MethodInfo`
handles. The per-call cost of reflection in the tree walk is primarily
`PropertyInfo.GetValue()` and `PropertyInfo.SetValue()`, which are fast for
non-trimmed properties.

The tree walk also uses `visual.GetType().GetProperty(propName)` on each node
(`hook/ThemeEngine.cs:1039`), which is not cached. This is acceptable because:

1. The CLR caches type metadata internally, so repeated `GetProperty` calls on the
   same type are fast.
2. The walk checks only 4 fixed property names per node.
3. The overall walk time (2-5ms for 500+ nodes) fits well within the 16ms frame budget.

### DWM Title Bar Color

(`hook/ThemeEngine.cs:54-113`)

The Windows 11 title bar color is set via P/Invoke to `DwmSetWindowAttribute`. This
is called once per theme apply, not per walk. The HWND is obtained via
`TopLevel.TryGetPlatformHandle().Handle` through reflection. On non-Windows platforms,
the call is skipped via `RuntimeInformation.IsOSPlatform` check.

The hex-to-COLORREF conversion (`hook/ThemeEngine.cs:75-81`):

```csharp
byte r = Convert.ToByte(hex[0..2], 16);
byte g = Convert.ToByte(hex[2..4], 16);
byte b = Convert.ToByte(hex[4..6], 16);
uint colorRef = (uint)(r | (g << 8) | (b << 16));  // COLORREF = 0x00BBGGRR
```

Note the byte order reversal: COLORREF uses BGR, not RGB.

---

## Appendix: Diagnostic Methods

The theme engine includes several diagnostic methods that are not part of the normal
theme flow but are invaluable for development:

**`DumpVisualTreeColors()`** (`hook/ThemeEngine.cs:1496-1545`): Scans all TopLevel
instances and logs the top 80 color/property combinations by frequency. Also logs
the top 20 control types and searches for DotNetBrowser/WebView controls.

**`DumpVisualTreeStructure()`** (`hook/ThemeEngine.cs:1616-1671`): Logs the visual
tree to 6 levels of depth with type names, child counts, and background colors. Shows
full type names for DotNetBrowser controls.

**`DumpResourceKeys()`** (`hook/ThemeEngine.cs:1676-1729`): Dumps all keys from
`Styles[0].Resources` (up to 60), `Application.Resources` (up to 30), and each
`MergedDictionary` (up to 15 per dict). Shows key name, value type, and value.

These methods were used to discover Root's color scheme, identify which resource keys
affect which controls, and build the tree color maps. They can be called from the
Uprooted settings page via the theme engine instance.

---

**Canonical for:** ThemeEngine algorithm (phases 1–6), resource-first migration plan, 32-key derivation table for custom themes, ping color fix strategy, CSS variable bridge, live preview system, revert mechanics
**Not canonical for:** Root's 32 key hex values → [ROOT_THEME_SYSTEM_FINDINGS.md](../../research/ROOT_THEME_SYSTEM_FINDINGS.md) | Root control types/style classes → [ROOT_CONTROL_REFERENCE.md](ROOT_CONTROL_REFERENCE.md)
*Last updated: 2026-02-22*
