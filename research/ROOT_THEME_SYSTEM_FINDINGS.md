# Root Theme System — ILSpy Findings (2026-02-18)

> **What this is:** Complete ILSpy decompilation of Root's color system — all 32 resource keys with hex values for all 3 themes, App.cs ThemeDictionaries wiring, 27 style files analysis, and the definitive override path.
> **Read when:** Implementing the resource-first ThemeEngine migration; deriving theme palettes; checking exact color values; understanding how Root switches themes.
> **Before this:** [ROOT_CONTROL_REFERENCE.md §Theme System Mechanics](../docs/framework/ROOT_CONTROL_REFERENCE.md#theme-system-mechanics) — summary of findings and implications.
> **Implementation guide:** [THEME_ENGINE_DEEP_DIVE.md §Resource-First Migration](../docs/framework/THEME_ENGINE_DEEP_DIVE.md#resource-first-migration-plan) — how to use these values to fix ThemeEngine.
> **Source:** ILSpy decompilation of `RootApp.Client.Avalonia v0.9.92.0`
> **Status:** Research complete — all 32 keys decoded, 3 themes compared, App.cs wiring confirmed, all 27 style files analyzed.
> **Related:** [THEME_ENGINE_DEEP_DIVE](../docs/framework/THEME_ENGINE_DEEP_DIVE.md) | [ROOT_CONTROL_REFERENCE](../docs/framework/ROOT_CONTROL_REFERENCE.md) | [ROOT_INTERNALS](../docs/research/ROOT_INTERNALS.md)

---

## Critical Correction

**Previous assumption (WRONG):** Root's native Avalonia AXAML themes contain no color brush resources. All UI colors are delivered via CSS variables to the Chromium webview. The native Avalonia side uses hardcoded hex colors in code, not theme-switchable resources.

**Actual finding (CONFIRMED):** Root's AXAML themes define **32 color resource keys** as `ImmutableSolidColorBrush` values via deferred factories. **Every Root view** (DirectMessageTabView, MessageView, ChannelStartMessageView, ChangeThemeView) binds to these keys using `DynamicResourceExtension`. The deferred values are loaded via function pointer delegates (`XamlClosure_53.Build_N`), which is why earlier binary string scanning missed them — the colors exist as uint32 ARGB literals in compiled IL, not as hex strings.

Root has a **dual color system**:
1. **Avalonia ResourceDictionary** — 32 color brush keys for native UI controls (this document)
2. **CSS variables** — 25 `--rootsdk-*` / `--color-*` variables for the Chromium/web side

Both systems operate in parallel, targeting different rendering layers.

---

## Dark Theme — Complete Color Table

Extracted from `XamlClosure_53` (deferred factories for `Resources/Themes/Dark.axaml`).

All color values are `ImmutableSolidColorBrush` (frozen/immutable) except `ScrollShadow` which is a `LinearGradientBrush`.

### Brand Colors

| Key | Hex | Description |
|-----|-----|-------------|
| `BrandPrimary` | `#3B6AF8` | Root's signature blue accent |
| `BrandSecondary` | `#A8FF5D` | Bright green accent |
| `BrandTertiary` | `#49D6AC` | Teal/cyan accent |

### Text Colors

| Key | Hex | Alpha | Description |
|-----|-----|-------|-------------|
| `TextPrimary` | `#F2F2F2` | 100% | Main text — light gray |
| `TextSecondary` | `#A3F2F2F2` | 64% | Muted text — same base, 64% alpha |
| `TextTertiary` | `#66F2F2F2` | 40% | Dim text — same base, 40% alpha |
| `TextWhite` | `#F2F2F2` | 100% | Same as TextPrimary in dark theme |

### Backgrounds

| Key | Hex | Description |
|-----|-----|-------------|
| `BackgroundPrimary` | `#0D1521` | Main app bg — **matches our `DefaultDarkBg` constant** |
| `BackgroundSecondary` | `#121A26` | Elevated surface bg |
| `BackgroundTertiary` | `#07101B` | Deepest/darkest bg |
| `Input` | `#090E13` | Text input field bg (darker than primary) |

### UI Elements

| Key | Hex | Alpha | Description |
|-----|-----|-------|-------------|
| `Border` | `#242C36` | 100% | General border — dark blue-gray |
| `HighlightLight` | `#0AFFFFFF` | 4% | Subtle hover highlight (white at 4%) |
| `HighlightNormal` | `#19FFFFFF` | 10% | Normal highlight (white at 10%) |
| `HighlightStrong` | `#30FFFFFF` | 19% | Strong/selected highlight (white at 19%) |

### Status Colors

| Key | Hex | Description |
|-----|-----|-------------|
| `Info` | `#F0F250` | Yellow — informational |
| `Warning` | `#E88F3D` | Orange — warning |
| `Error` | `#F03F36` | Red — error/danger (notification badges) |
| `Muted` | `#4F5C6F` | Blue-gray — muted/disabled |
| `Link` | `#88A5FF` | Light periwinkle — hyperlinks |

### Mention Colors

All mention colors use alpha-varied base colors for layered transparency effects.

| Key | Hex | Base Color | Alpha | Description |
|-----|-----|------------|-------|-------------|
| `SelfMention` | `#66FF2D1F` | `#FF2D1F` (red) | 40% | Self-mention text overlay |
| `SelfMentionBackground` | `#26FF2D1F` | `#FF2D1F` (red) | 15% | Self-mention highlight bg |
| `SelfMentionBorder` | `#4DFF2D1F` | `#FF2D1F` (red) | 30% | Self-mention border |
| `OtherMentionBackground` | `#1A88A5FF` | `#88A5FF` (link blue) | 10% | Other user mention bg |
| `OtherMentionBorder` | `#3388A5FF` | `#88A5FF` (link blue) | 20% | Other user mention border |
| `RoleMentionBackground` | `#1AB388FF` | `#B388FF` (purple) | 10% | Role mention bg |
| `RoleMentionBorder` | `#33B388FF` | `#B388FF` (purple) | 20% | Role mention border |
| `RoleMentionText` | `#B388FF` | `#B388FF` (purple) | 100% | Role mention text |
| `ChannelMentionBackground` | `#1AE88F3D` | `#E88F3D` (warning) | 10% | Channel mention bg |
| `ChannelMentionBorder` | `#33E88F3D` | `#E88F3D` (warning) | 20% | Channel mention border |
| `ChannelMentionText` | `#E88F3D` | `#E88F3D` (warning) | 100% | Channel mention text (= Warning) |

### Effects

| Key | Value | Type |
|-----|-------|------|
| `ScrollShadow` | `LinearGradientBrush: #00000000 (bottom) → #80000000 (top)` | LinearGradientBrush |
| `DropShadow` | `#80000000` | Color (not brush) |
| `PopupBoxShadow` | `0 0 12 0 #80000000` | BoxShadows |

### Metadata

| Key | Value | Type |
|-----|-------|------|
| `ThemeName` | `"Dark"` | string |
| `DisplayName` | `"Dark Theme"` | string |

---

## Key Observations

### Color Relationships

1. **TextPrimary = TextWhite** in Dark/PureDark (`#F2F2F2`) — **confirmed different in Light**: TextPrimary=`#131313`, TextWhite stays `#F2F2F2`
2. **TextSecondary/Tertiary are alpha variants** of `#F2F2F2` in Dark/PureDark, but **solid opaque colors** in Light (`#282828`, `#5E5E5E`)
3. **Highlights are white at varying alpha** — 4%, 10%, 19% — pure opacity layers, not distinct colors
4. **Mention colors share base colors** with status/link colors, just at different alpha levels:
   - Self mention → `#FF2D1F` (close to Error `#F03F36`)
   - Other mention → `#88A5FF` (= Link)
   - Role mention → `#B388FF` (unique purple)
   - Channel mention → `#E88F3D` (= Warning)
5. **BackgroundPrimary `#0D1521`** matches our ThemeEngine's `DefaultDarkBg` constant — confirms we already knew this value, just didn't know it was in a resource

### Brush Type: ImmutableSolidColorBrush

Root uses `Avalonia.Media.Immutable.ImmutableSolidColorBrush` (frozen/immutable), not `SolidColorBrush`. This is important for our override strategy:
- We can't modify the existing brush objects (they're immutable)
- We must replace the entire resource value in the dictionary with a new brush
- `SolidColorBrush` should work as a replacement (both implement `IBrush`)
- Need to verify: does replacing a deferred value in the dictionary trigger DynamicResource re-resolution?

### SVG Asset Paths (~220 entries)

All SVG paths point to `/Resources/Assets/SVGs/Dark Theme/...`. Categories: badges, channel types, community templates, connection status, explore page, files, message area, system messages, video player, theming previews. These swap per-theme variant (Dark vs Light vs PureDark) for icon variants.

---

## How Views Consume These Keys

Confirmed from decompiled `DirectMessageTabView.axaml`:

```csharp
// Root's views use DynamicResourceExtension to bind to theme keys
new DynamicResourceExtension("TextPrimary")       // Ellipse.Fill → IBrush
new DynamicResourceExtension("HighlightLight")     // Border.Background → IBrush
new DynamicResourceExtension("TextSecondary")      // TextBlock.Foreground → IBrush
new DynamicResourceExtension("Error")              // Notification badge Background → IBrush
new DynamicResourceExtension("TextWhite")          // Notification count Foreground → IBrush
new DynamicResourceExtension("BrandTertiary")      // Call indicator Fill → IBrush
new DynamicResourceExtension("DMCallSVG")          // SVG path (asset, not color) → string
```

All color bindings target `IBrush?` properties (`Shape.FillProperty`, `Border.BackgroundProperty`, `TextBlock.ForegroundProperty`). The dictionary values are `ImmutableSolidColorBrush` which implements `IBrush`.

Controls also use:
- `StaticResourceExtension("RootFont")` for FontFamily
- `ThemeVariantScope` with `RequestedThemeVariant = ThemeVariant.Light` (Avalonia's native theme variant system)
- Compiled bindings to ViewModels (`CompiledBindingExtension`)

### Custom Root Controls Observed

- `RootMenuFlyout` — custom context menu
- `RootCircularPanel` — circular avatar layout (with `ItemsSourceProperty`)
- `RootSvgImage` — SVG renderer (with `SvgPathProperty` bound to theme resource)

---

## MessageView — DynamicResource Universality Confirmed

MessageView.cs (232KB, the most complex view in the app) uses **33 DynamicResourceExtension calls**, exclusively targeting Root's 32 keys:

| Key | Occurrences | Context |
|-----|-------------|---------|
| `Error` | 5 | Failed message border, retry button |
| `TextTertiary` | 5 | Timestamps, metadata text |
| `Border` | 5 | Dividers, popup borders |
| `PopupBoxShadow` | 3 | Action bar, emoji picker, popup shadows |
| `TextPrimary` | 3 | Username, message text |
| `BackgroundSecondary` | 2 | Popup backgrounds |
| `SelfMention` | 1 | Self-mention highlight background |
| `BrandPrimary` | 1 | Primary action button |
| `TextWhite` | 1 | Button text on brand bg |
| `TextSecondary` | 1 | Secondary metadata |
| `BackgroundTertiary` | 1 | Reactions container bg |
| `BackgroundPrimary` | 1 | Action bar bg |
| `Link` | 1 | Link-colored retry text |
| `HighlightNormal` | 1 | Hover highlight |

**Hardcoded colors found: only 4 instances, none are theme colors:**
- Lines 575/579: `SolidColorBrush(Color.Parse(role.RoleColorHex))` — server-defined role colors (not themeable)
- Lines 1471/1589: `ImmutableSolidColorBrush(16777215u)` = `#00FFFFFF` (transparent) — invisible placeholder backgrounds

**ChannelStartMessageView** — same pattern: `TextPrimary`, `TextTertiary`, `TextSecondary`, `Border`. Zero hardcoded colors.

**Conclusion:** DynamicResource usage is **universal** for theme colors across Root's views. Hardcoded colors are limited to server-defined role colors and transparent placeholders. The visual tree walk in our ThemeEngine is almost entirely unnecessary if we override Root's 32 keys directly.

---

## Resource Resolution Priority (from Avalonia.Application source)

```csharp
public bool TryGetResource(object key, ThemeVariant? theme, out object? value)
{
    value = null;
    // Application.Resources checked FIRST
    if (_resources?.TryGetResource(key, theme, out value) == true)
        return true;
    // Application.Styles checked SECOND (fallback)
    return Styles.TryGetResource(key, theme, out value);
}
```

**Update from App.cs:** Root's theme dictionaries are loaded into `Application.Resources.ThemeDictionaries` (NOT Styles). Each variant (Dark/Light/PureDark) is a `ResourceDictionary` whose `MergedDictionaries[0]` contains the 32 keys. To override, set keys directly on the variant's ResourceDictionary — direct entries take priority over MergedDictionaries. See "App.cs — Theme Dictionary Wiring" section below.

---

## App Startup Chain (from Program.cs)

```csharp
// Root, Version=0.9.92.0 — Program.Main
RootLauncher.Run(args,
    host => new AppCompositionRoot(host),  // DI container
    compositionRoot => {
        AppBuilder.Configure(compositionRoot.GetRequiredService<App>)  // App from DI
            .UsePlatformDetect()
            .UseReactiveUI()
            .StartWithClassicDesktopLifetime(args);
    });
```

- Root uses Microsoft.Extensions.Hosting for DI (`IHost` → `AppCompositionRoot`)
- `App` is resolved from the DI container (not `new App()`)
- Uses Velopack for auto-updates
- ReactiveUI.Avalonia for reactive bindings

**DataStoreKeys** — Complete settings enum (68 keys):
- `Theme` (confirmed — int value of `RootThemeEnum` ordinal)
- `DeveloperModeEnabled` — Root has a hidden developer mode
- `ZoomLevel` — UI zoom support
- `StreamerMode*` (7 keys) — Streamer mode features
- `GameOverlay*` / `ChatOverlay*` (15 keys) — In-game overlay settings

---

## RootBorder — DPI-Aware Custom Border

```csharp
public class RootBorder : Border {
    protected override Type StyleKeyOverride => typeof(Border);  // Uses Border's styles
    // DynamicBorderThickness rounds to physical pixels using RenderScaling
}
```

Root's custom border that prevents sub-pixel rendering artifacts by rounding border thickness to the nearest physical pixel based on `MainWindow.RenderScaling`. Uses `StyleKeyOverride => typeof(Border)` so it inherits all standard Border styles — no custom theme keys needed.

---

## Impact on Uprooted's ThemeEngine

### Current Approach (Suboptimal)

Our `ThemeEngine.cs` currently:

1. **Phase 1:** Overrides `Styles[0].Resources` with ~55 keys targeting FluentTheme names (`SystemAccentColor`, `TextFillColorPrimary`, `ThemeAccentBrush`, etc.)
2. **Phase 2:** Injects a `MergedDictionary` into `Application.Resources` with the same FluentTheme keys
3. **Phase 3-4:** Walks the visual tree and physically replaces brush objects on ~25 hardcoded ARGB colors

**Problem:** Root's controls don't bind to FluentTheme keys — they bind to Root's own 32 keys (`TextPrimary`, `BrandPrimary`, `BackgroundPrimary`, etc.). Our resource overrides hit keys that nothing references. The visual tree walk was compensating for this by brute-forcing color replacement, but it suffers from timing gaps (controls created/recycled between walks show wrong colors briefly).

### Correct Approach (Resource-First)

Override Root's 32 actual theme resource keys:

1. **Find where Root loads `Dark.axaml`** — which `Styles` index or `MergedDictionary` it lives in
2. **Override the 32 color keys directly** in that dictionary — `DynamicResource` bindings propagate automatically, zero visual tree walk needed for resource-bound controls
3. **Keep a minimal tree walk** only for any controls with truly hardcoded ARGB values (may be a very small number now)
4. **Target mention keys directly** — `SelfMentionBackground`, `SelfMentionBorder` for the custom ping color feature instead of overriding `HighlightForegroundColor`

> **CRITICAL BUG (confirmed live):** Writing to a ThemeDictionaries variant wrapper (via indexer or `Add`) triggers `ActualThemeVariantChanged` **on the same UI thread, synchronously**. If your handler for that event writes more resources, you get an infinite loop → frozen UI, no error. **Every code path that writes to ThemeDictionaries must be wrapped in a boolean re-entrancy guard**, and the `ActualThemeVariantChanged` handler must bail out when the guard is set. This applies to: theme apply, revert, live preview, and ping color override. See [THEME_ENGINE_DEEP_DIVE.md §Resource-First Migration](../docs/framework/THEME_ENGINE_DEEP_DIVE.md#the-correct-override-path) for the guard pattern.

### Theme Palette Mapping (proposed)

For a custom theme with accent `A` and background `B`:

| Root Key | Derivation from Accent/Background |
|----------|----------------------------------|
| `BrandPrimary` | Accent color directly |
| `BrandSecondary` | Accent complementary or lighter variant |
| `BrandTertiary` | Accent analogous variant |
| `TextPrimary` | Derived from background (high contrast) |
| `TextSecondary` | TextPrimary at 64% alpha |
| `TextTertiary` | TextPrimary at 40% alpha |
| `TextWhite` | Same as TextPrimary (or pure white) |
| `BackgroundPrimary` | Background color directly |
| `BackgroundSecondary` | Background + slight lightness bump |
| `BackgroundTertiary` | Background - slight lightness (darker) |
| `Input` | Background - more lightness (darkest) |
| `Border` | Background desaturated + lightened |
| `HighlightLight` | White at 4% alpha (unchanged?) |
| `HighlightNormal` | White at 10% alpha (unchanged?) |
| `HighlightStrong` | White at 19% alpha (unchanged?) |
| `Info` | Keep `#F0F250` or derive from accent |
| `Warning` | Keep `#E88F3D` |
| `Error` | Keep `#F03F36` |
| `Muted` | Background lightened significantly |
| `Link` | Accent lighter variant |
| `SelfMention*` | Accent-based at matching alphas |
| `OtherMention*` | Link-based at matching alphas |
| `RoleMention*` | Accent secondary at matching alphas |
| `ChannelMention*` | Warning-based at matching alphas |

### Expected Benefits

- **Instant theme propagation** — DynamicResource bindings update automatically when dictionary values change, no 500ms timer or tree walk needed
- **Fixes theme switch inconsistencies** — the "stale recolor" bug where controls show wrong colors after switching themes would be eliminated for resource-bound controls
- **Fixes toggle/switch accent color bug** — controls that change visual state (checked→unchecked) create new visuals that resolve from the dictionary, so they'd pick up our overridden colors automatically
- **Simpler code** — potentially removing or drastically reducing the tree walk, cross-mapping, and revert mechanics
- **Live preview via resource update** — changing a dictionary value triggers all DynamicResource bindings to re-resolve, enabling instant live preview

---

## Theme Variant Comparison

### Structural Comparison (confirmed)

All 3 themes use **identical key names** with the same Build_1 through Build_32 mapping. The only differences are:

| Aspect | Dark | Light | PureDark |
|--------|------|-------|----------|
| Closure class | `XamlClosure_53` | `XamlClosure_54` | `XamlClosure_55` |
| Entry count | 259 | 258 | 257 |
| SVG folder | `Dark Theme/` | `Light Theme/` | `Dark Theme/` (shared) |
| DropShadow | `#80000000` | `#806B6B6B` | `#80000000` |
| PopupBoxShadow | `#80000000` | `#19000000` | `#000000` |
| LoadingSpinner | `LoadingSpinnerDark.json` | `LoadingSpinnerLight.json` | `LoadingSpinnerDark.json` |
| ThemeName | `"Dark"` | `"Light"` | `"PureDark"` |
| DisplayName | `"Dark Theme"` | `"Light Theme"` | `"Pure Dark Theme"` |

PureDark shares Dark's SVG set and loading spinner — it's essentially a darker-background variant of Dark with the same icon set.

### Complete Three-Theme Color Comparison

| Key | Dark | Light | PureDark | Notes |
|-----|------|-------|----------|-------|
| `BrandPrimary` | `#3B6AF8` | `#3B6AF8` | `#3B6AF8` | **Same across all themes** |
| `BrandSecondary` | `#A8FF5D` | `#58AC30` | `#A8FF5D` | Light: darker green |
| `BrandTertiary` | `#49D6AC` | `#08A677` | `#49D6AC` | Light: darker teal |
| `TextPrimary` | `#F2F2F2` | `#131313` | `#F2F2F2` | Light: near-black (inverted) |
| `TextSecondary` | `#A3F2F2F2` | `#282828` | `#A3F2F2F2` | Light: solid dark gray (no alpha) |
| `TextTertiary` | `#66F2F2F2` | `#5E5E5E` | `#66F2F2F2` | Light: solid medium gray (no alpha) |
| `TextWhite` | `#F2F2F2` | `#F2F2F2` | `#F2F2F2` | **Same across all themes** |
| `BackgroundPrimary` | `#0D1521` | `#FBFBFB` | `#1A1A1E` | All different; PureDark is neutral gray |
| `BackgroundSecondary` | `#121A26` | `#FFFFFF` | `#202024` | Light: pure white |
| `BackgroundTertiary` | `#07101B` | `#F5F6F8` | `#121214` | |
| `Input` | `#090E13` | `#F5F5F5` | `#101112` | |
| `Border` | `#242C36` | `#DDDDDD` | `#303030` | PureDark: neutral gray |
| `HighlightLight` | `#0AFFFFFF` | `#0A222222` | `#0AFFFFFF` | Light: dark overlay (same alpha) |
| `HighlightNormal` | `#19FFFFFF` | `#19222222` | `#19FFFFFF` | Light: dark overlay (same alpha) |
| `HighlightStrong` | `#30FFFFFF` | `#26222222` | `#30FFFFFF` | Light: dark overlay (lower alpha) |
| `Info` | `#F0F250` | `#EA9134` | `#F0F250` | Light: orange (not yellow) |
| `Warning` | `#E88F3D` | `#EA9134` | `#E88F3D` | Light: same as Light Info! |
| `Error` | `#F03F36` | `#F03F36` | `#F03F36` | **Same across all themes** |
| `Muted` | `#4F5C6F` | `#A9B3C0` | `#4F5C6F` | Light: lighter blue-gray |
| `Link` | `#88A5FF` | `#006EEB` | `#88A5FF` | Light: vivid blue |
| `SelfMention` | `#66FF2D1F` | `#66FF2D1F` | `#66FF2D1F` | **Same across all themes** |
| `SelfMentionBg` | `#26FF2D1F` | `#1AFF2D1F` | `#26FF2D1F` | Light: slightly less alpha |
| `SelfMentionBorder` | `#4DFF2D1F` | `#40FF2D1F` | `#4DFF2D1F` | Light: slightly less alpha |
| `OtherMentionBg` | `#1A88A5FF` | `#1A006EEB` | `#1A88A5FF` | Light: uses Link base color |
| `OtherMentionBorder` | `#3388A5FF` | `#33006EEB` | `#3388A5FF` | Light: uses Link base color |
| `RoleMentionBg` | `#1AB388FF` | `#1A7C4DFF` | `#1AB388FF` | Light: darker purple |
| `RoleMentionBorder` | `#33B388FF` | `#337C4DFF` | `#33B388FF` | Light: darker purple |
| `RoleMentionText` | `#B388FF` | `#7C4DFF` | `#B388FF` | Light: darker purple |
| `ChannelMentionBg` | `#1AE88F3D` | `#1AD07000` | `#1AE88F3D` | Light: darker orange |
| `ChannelMentionBorder` | `#33E88F3D` | `#33D07000` | `#33E88F3D` | Light: darker orange |
| `ChannelMentionText` | `#E88F3D` | `#D07000` | `#E88F3D` | Light: darker orange |

### Key Patterns Across Themes

1. **BrandPrimary, TextWhite, Error, SelfMention** are identical across all 3 themes
2. **Dark = PureDark** for all non-background/border keys — PureDark only changes the surface colors and border
3. **PureDark backgrounds are neutral gray** (`#1A1A1E`, `#202024`, `#121214`) vs Dark's blue-tinted grays (`#0D1521`, `#121A26`, `#07101B`)
4. **Light theme text uses solid colors** (no alpha), while Dark/PureDark use alpha-varied `#F2F2F2` base
5. **Light highlights use `#222222` (dark)** at same alphas as Dark's `#FFFFFF` (white) — natural inversion
6. **Light theme has a bug or design choice**: Info = Warning = `#EA9134` (same orange)
7. **Light mentions use different base colors** than Dark — they track the Light Link/Role/Channel colors at matching alphas

### Shadow Intensity Pattern

Shadows get progressively stronger: Light → Dark → PureDark

| Theme | DropShadow | PopupBoxShadow |
|-------|-----------|---------------|
| Light | `#806B6B6B` (gray, 50%) | `#19000000` (black, 10%) |
| Dark | `#80000000` (black, 50%) | `#80000000` (black, 50%) |
| PureDark | `#80000000` (black, 50%) | `#000000` (black, 100%) |

---

## AvaloniaEdit Theme Layer (Compose Input)

Root uses AvaloniaEdit v11.3.0 for the compose input (`TextEditor`). AvaloniaEdit has its own theme system separate from Root's 32 keys.

### AvaloniaEdit Resource Keys (10 per variant)

From `Themes/Simple/AvaloniaEdit.xaml`, each `ThemeVariant` dictionary defines:

| Key | Purpose |
|-----|---------|
| `CompletionToolTipBackground` | Autocomplete tooltip bg |
| `CompletionToolTipForeground` | Autocomplete tooltip text |
| `CompletionToolTipBorderBrush` | Autocomplete tooltip border |
| `CompletionToolTipBorderThickness` | Autocomplete tooltip border width |
| `OverloadViewerBackground` | Overload popup bg |
| `OverloadViewerForeground` | Overload popup text |
| `OverloadViewerBorderBrush` | Overload popup border |
| `SearchPanelBackgroundBrush` | Find/replace panel bg |
| `SearchPanelBorderBrush` | Find/replace panel border |
| `TextAreaSelectionBrush` | Text selection highlight |

Plus shared resources: `SearchPanelFontSize`, `SearchPanelFontFamily`.

### AvaloniaEdit Loading

`!XamlLoader.TryLoad` resolves 3 URIs:
- `avares://AvaloniaEdit/Themes/Base.xaml` — control templates (TextEditor, TextArea, SearchPanel, CompletionList)
- `avares://AvaloniaEdit/Themes/Fluent/AvaloniaEdit.xaml` — Fluent theme variant
- `avares://AvaloniaEdit/Themes/Simple/AvaloniaEdit.xaml` — Simple theme variant

**Implication:** AvaloniaEdit uses `ThemeVariant.Default` and `ThemeVariant.Dark` dictionaries. To theme the compose input properly, we may need to override these 10 keys in addition to Root's 32 keys. Or, since AvaloniaEdit uses Avalonia's native `ThemeVariant` system, setting `RequestedThemeVariant` on the app/window might handle it automatically.

---

## Root's Color Picker (RootColorPicker.axaml)

Root has a custom color picker with 46 resources at `/Resources/Styles/ColorPicker/RootColorPicker.axaml`. Notable resources:
- `ColorSpectrum` control template
- Various converters: `ToBrushConverter`, `AccentColorConverter`, `ContrastBrushConverter`, `ColorToHexConverter`
- Slider/previewer dimensions and corner radii
- This is used in community settings for accent color selection — **not directly relevant to our theme override strategy**

---

## Root's Settings Storage (ChatViewModel / ChatView)

Root uses `ILocalDataStore` with per-user keyed paths for settings:
```csharp
// Pattern: localDataStore.TryGetWithPath([userId, settingKey], out int value)
_localDataStore.TryGetWithPath([userId, "AutoConvertEmojis"], out int value);   // default: 0 (false)
_localDataStore.TryGetWithPath([userId, "TapToReply"], out int value2);         // default: 1 (true)
```
Settings are stored as integers (0/1 for booleans). Theme is stored separately via `TryGetGlobal(DataStoreKeys.Theme)` (global, not per-user).

**Settings page UI pattern** (from ChatView.cs):
- `RootScrollViewer` outer container
- `StackPanel` with `Margin(24)` for content
- Section headers: `TextBlock` with `TextPrimary` foreground, `RootFont`, `FontWeight.Medium`, 20pt
- Setting cards: `RootBorder` with `BackgroundSecondary` bg, `Border` border, `DynamicBorderThickness(0.5)`, `CornerRadius(12)`, `Padding(24)`
- Card layout: `Grid` with Star/Auto/Auto columns — StackPanel(title+desc) | spacer | CheckBox
- Title: `TextPrimary`, `FontWeight.Bold`, 14pt
- Description: `TextSecondary`, `FontWeight(450)` (SemiBold), 14pt, `LineHeight(20)`
- Toggle: `CheckBox` with `.ToggleSwitch` class, `TwoWay` binding to ViewModel property

---

## Theme Switching Mechanism (FOUND)

### ThemeService — The Actual Switcher

```csharp
// RootApp.Client.Avalonia.Resources.Themes.ThemeService
public class ThemeService(ILocalDataStore P_0)
{
    public void InitializeTheme()
    {
        // Load saved theme from ILocalDataStore, default to Dark
        if (P_0.TryGetGlobal(DataStoreKeys.Theme, out int num) && Enum.IsDefined(typeof(RootThemeEnum), num))
            SetTheme(ThemeMapper.ToThemeVariant((RootThemeEnum)num));
        else
            SetTheme(ThemeVariant.Dark);
    }

    public void SetTheme(ThemeVariant variant, bool persist = false)
    {
        if (persist)
            P_0.SetGlobal(DataStoreKeys.Theme, (int)ThemeMapper.ToRootThemeEnum(variant));
        Application.Current.RequestedThemeVariant = variant;  // <-- THIS IS THE ENTIRE SWITCH
    }
}
```

**Key finding:** Root uses Avalonia's **native `RequestedThemeVariant` mechanism**. No manual ResourceDictionary swapping. Setting `Application.Current.RequestedThemeVariant = variant` triggers Avalonia's built-in resource resolution to select the correct theme dictionary. All `DynamicResource` bindings automatically re-resolve.

### RootThemeEnum

```csharp
public enum RootThemeEnum { Default, Dark, Light, PureDark }
```

Stored as `int` in `ILocalDataStore` via `DataStoreKeys.Theme` (global, not per-user).

### ThemeMapper — Variant Mapping

```csharp
public static class ThemeMapper
{
    // PureDark INHERITS from Dark — missing keys fall back to Dark
    public static readonly ThemeVariant PureDark = new ThemeVariant("PureDark", ThemeVariant.Dark);

    public static ThemeVariant ToThemeVariant(RootThemeEnum e) => e switch {
        RootThemeEnum.Default  => ThemeVariant.Default,
        RootThemeEnum.Dark     => ThemeVariant.Dark,
        RootThemeEnum.Light    => ThemeVariant.Light,
        RootThemeEnum.PureDark => PureDark,
        _                      => ThemeVariant.Default
    };

    // For Chromium bridge: PureDark collapses to "dark" (same CSS variables)
    public static string ToAppBridgeThemeString(ThemeVariant? v) =>
        (v == ThemeVariant.Light || v?.InheritVariant == ThemeVariant.Light) ? "light" : "dark";

    // For WebRTC: PureDark gets its own string "pure-dark"
    public static string ToWebRtcThemeString(ThemeVariant? v) => /* dark | light | pure-dark */;
}
```

**Critical detail:** `PureDark` inherits from `ThemeVariant.Dark`. This is why PureDark only needs to override surface colors — Avalonia's resource resolution automatically falls back to Dark for any key not explicitly defined in PureDark.

### ChangeThemeViewModel — UI Binding

```csharp
// When user selects a theme radio button, OnThemeChanged fires:
private void OnThemeChanged(ThemeVariant variant)
{
    if (Application.Current.RequestedThemeVariant != Theme)
        _themeService.SetTheme(Theme, persist: true);
}
```

ViewModel uses `CommunityToolkit.Mvvm.ComponentModel` with `[ObservableProperty]` for two-way binding. Theme picker view has 4 RadioButtons: Dark, Light, PureDark, System (Default).

### ChangeThemeView — Confirms DynamicResource Usage

The theme picker view itself uses these Root resource keys:
- `DynamicResourceExtension("HighlightLight")` — unchecked radio background
- `DynamicResourceExtension("HighlightNormal")` — checked radio background
- `DynamicResourceExtension("BrandPrimary")` — checked radio border
- `DynamicResourceExtension("Border")` — border brush
- `DynamicResourceExtension("TextPrimary")` — checkbox border, checked fill, all text foreground
- `DynamicResourceExtension("TextSecondary")` — hover states
- `DynamicResourceExtension("BackgroundSecondary")` — container background

### ThemeService Utility Methods

```csharp
// IsDefaultColor: checks if a hex string is #FFFFFF or #000000 (used for profile color defaults)
public static bool IsDefaultColor(string hex) => sanitized == "#FFFFFF" || sanitized == "#000000";

// GetInvertedDefaultColorHex: returns #000000 for Light theme, #FFFFFF otherwise
public static string GetInvertedDefaultColorHex(string hex) =>
    Application.Current.ActualThemeVariant == ThemeVariant.Light ? "#000000" : "#FFFFFF";

// sanitizeHexString: strips alpha from 9-char hex (#AARRGGBB → #RRGGBB), uppercases
private static string sanitizeHexString(string hex) =>
    hex.Length == 9 ? "#" + hex.Substring(3) : hex.ToUpper();
```

---

## Implications for Uprooted's Override Strategy

### What We Now Know (Complete Picture)

1. **Switching mechanism:** `Application.Current.RequestedThemeVariant = variant` — Avalonia native
2. **Resource resolution:** ThemeVariant-aware — each theme dict (Dark/Light/PureDark) is keyed by its variant
3. **PureDark inheritance:** Falls back to Dark for missing keys via `ThemeVariant.InheritVariant`
4. **Persistence:** `ILocalDataStore.TryGetGlobal(DataStoreKeys.Theme, out int)` — global int (enum ordinal)
5. **Default:** Dark (when no saved preference exists)
6. **Bridge mapping:** Chromium side only sees "dark" or "light" — PureDark collapses to "dark" for CSS

---

## App.cs — Theme Dictionary Wiring (CRITICAL)

Root's `App : Application` class (from `RootApp.Client.Avalonia`) shows the exact resource/style setup:

### Resource Dictionary Structure

```
Application.Resources (ResourceDictionary)
├── ThemeDictionaries
│   ├── ThemeVariant.Light    → ResourceDictionary { MergedDictionaries: [Build_Light()] }
│   ├── ThemeVariant.Dark     → ResourceDictionary { MergedDictionaries: [Build_Dark()] }
│   └── ThemeMapper.PureDark  → ResourceDictionary { MergedDictionaries: [Build_PureDark()] }
├── MergedDictionaries: [Fonts.axaml, Sounds.axaml]
└── 26 deferred converters (ScrollToVisibility, BoolInverter, EnumToBool, etc.)
```

Each theme's 32 color keys (BrandPrimary, TextPrimary, etc.) live inside the `Build_Dark/Light/PureDark` ResourceDictionary, which is added as `MergedDictionaries[0]` of the ThemeDictionaries entry.

### Style Stack (order matters — later wins for conflicting selectors)

```csharp
app.Styles.Add(new SimpleTheme());           // Avalonia minimal base
app.Styles.Add(new MediaFluentTheme());      // NOT standard FluentTheme!
app.Styles.Add(TextBox FocusAdorner = null);  // Inline style: removes focus adorner
app.Styles.Add(Button FocusAdorner = null);   // Inline style: removes focus adorner
app.Styles.Add(AvaloniaEdit.axaml);          // Code editor theme
app.Styles.Add(RootColorPicker.axaml);       // Custom color picker
app.Styles.Add(BorderlessTextbox.axaml);
app.Styles.Add(BorderButton.axaml);
app.Styles.Add(TransparentButton.axaml);
app.Styles.Add(LinkButton.axaml);
app.Styles.Add(TextButton.axaml);
app.Styles.Add(ScrollViewer.axaml);
app.Styles.Add(ListBox.axaml);
app.Styles.Add(SvgButton.axaml);
app.Styles.Add(ListBoxItem.axaml);
app.Styles.Add(TabItem.axaml);
app.Styles.Add(CheckBox.axaml);              // Custom CheckBox template
app.Styles.Add(MenuFlyoutPresenter.axaml);
app.Styles.Add(FlyoutPresenter.axaml);
app.Styles.Add(MenuItem.axaml);
app.Styles.Add(Separator.axaml);
app.Styles.Add(TabsTheme.axaml);
app.Styles.Add(RootImageLoader.axaml);
app.Styles.Add(ComboBox.axaml);
app.Styles.Add(ComboBoxItem.axaml);
app.Styles.Add(MessageMarkdown.axaml);
app.Styles.Add(DropDownButton.axaml);
app.Styles.Add(RootSplitView.axaml);
app.Styles.Add(ToolTip.axaml);
app.Styles.Add(Slider.axaml);
```

**Root uses `MediaFluentTheme`** (NOT standard `FluentTheme`). This is why our current ThemeEngine targeting FluentTheme resource keys (`SystemAccentColor`, `TextFillColorPrimary`) doesn't work — Root's controls are styled by MediaFluentTheme + 25 custom style files (loaded as 30 total `Styles.Add` calls: 2 base themes + 2 inline focus adorner removal + 1 AvaloniaEdit + 25 Root axaml) that reference Root's own 32 resource keys.

### Initialize Order

```csharp
public override void Initialize()
{
    _0021XamlIlPopulateTrampoline(this);  // Sets up Resources + Styles
    _themeService.InitializeTheme();       // Sets RequestedThemeVariant
    // ...
}
```

### Definitive Override Strategy

Since theme colors are in `Application.Resources.ThemeDictionaries`, the override path is:

```csharp
// Get the Dark theme's dictionary wrapper
var darkDict = Application.Current.Resources.ThemeDictionaries[ThemeVariant.Dark] as ResourceDictionary;
// Set keys directly — takes priority over MergedDictionaries[0] entries
darkDict["BrandPrimary"] = new ImmutableSolidColorBrush(ourColor);
darkDict["TextPrimary"] = new ImmutableSolidColorBrush(ourTextColor);
// ... repeat for all 32 keys

// Same for Light and PureDark as needed
```

**Why this works:** In `ResourceDictionary.TryGetResource()`, direct entries are checked before `MergedDictionaries`. By setting keys on the wrapper dict (which only has MergedDictionaries, no direct entries originally), our values win. DynamicResource bindings re-resolve automatically.

---

## Root's 27 Custom Style Files — DynamicResource Analysis

All 27 style files from `Resources/Styles/*.axaml` were split from `StylesAll.cs` into individual `Style_*.cs` files.

### Root's 32 Keys Used in Style Templates

69 DynamicResource usages of 22 unique Root keys across all style files:

| Key | Usages | Primary Consumers |
|-----|--------|-------------------|
| `TextPrimary` | 21 | MessageMarkdown (11), ScrollViewer (2), ComboBox, ComboBoxItem, DropDownButton, TabItem, ChatView |
| `HighlightNormal` | 13 | BorderButton (3), ComboBoxItem (3), SvgButton (2), TransparentButton, MenuItem (2), CheckBox |
| `BrandPrimary` | 7 | MessageMarkdown (3), Slider (2), CheckBox (2) |
| `Border` | 7 | ComboBoxItem (3), Slider, MessageMarkdown, Separator, DropDownButton |
| `HighlightStrong` | 4 | ComboBoxItem (4) — selected/pressed states |
| `BackgroundSecondary` | 1 | MessageMarkdown — code block bg |
| `BackgroundTertiary` | 1 | DropDownButton |
| `TextSecondary` | 1 | MessageMarkdown |
| `Input` | 1 | ComboBox — background |
| `Muted` | 1 | CheckBox — unchecked border |
| `HighlightLight` | 1 | TransparentButton — hover |
| `Link` | 1 | MessageMarkdown — hyperlinks |
| `SelfMentionBackground` | 1 | MessageMarkdown |
| `SelfMentionBorder` | 1 | MessageMarkdown |
| `OtherMentionBackground` | 1 | MessageMarkdown |
| `OtherMentionBorder` | 1 | MessageMarkdown |
| `RoleMentionBackground` | 1 | MessageMarkdown |
| `RoleMentionBorder` | 1 | MessageMarkdown |
| `RoleMentionText` | 1 | MessageMarkdown |
| `ChannelMentionBackground` | 1 | MessageMarkdown |
| `ChannelMentionBorder` | 1 | MessageMarkdown |
| `ChannelMentionText` | 1 | MessageMarkdown |

### Root Keys NOT Used in Any Style File

These 10 keys are only referenced directly in views (MessageView, ChangeThemeView, etc.), not in reusable style templates:

`BackgroundPrimary`, `BrandSecondary`, `BrandTertiary`, `Error`, `Info`, `ScrollShadow`, `SelfMention`, `TextTertiary`, `TextWhite`, `Warning`

### Non-Root Keys in Styles (from SimpleTheme/MediaFluentTheme)

26 unique non-Root keys (41 usages), mostly FluentTheme ComboBox internals:

| Category | Keys |
|----------|------|
| ComboBox | `ComboBoxBorderBrush`, `ComboBoxForegroundDisabled`, `ComboBoxPlaceHolderForeground`, `ComboBoxBackgroundBorderBrushFocused`, etc. (16 keys) |
| SimpleTheme | `ThemeControlHighlightLowBrush`, `ThemeControlLowColor`, `ThemeDisabledOpacity`, `ThemeBorderThickness` |
| Layout | `SplitViewOpenPaneThemeLength`, `SplitViewCompactPaneThemeLength`, `ControlCornerRadius` |
| Other | `SystemControlErrorTextForegroundBrush`, `RootFont` (10x) |

**These come from SimpleTheme/MediaFluentTheme and do NOT need to be overridden** for basic theming — they handle disabled/focus states for standard controls.

### Hardcoded Colors in Styles

Almost all hardcoded `ImmutableSolidColorBrush` values are `#00FFFFFF` (transparent) — used to reset Background/BorderBrush on controls like TransparentButton, ListBox, ListBoxItem, BorderlessTextbox.

Only 2 non-transparent hardcoded colors (both in MessageMarkdown):
- `#FFC3CB` — text selection brush (pink highlight)
- `#FF0000` — `.NoContainer` debug border

Neither is theme-relevant.

### Biggest Style Files (by complexity)

| File | Lines | What it styles |
|------|-------|----------------|
| `Style_MessageMarkdown.cs` | 1,074 | **All markdown rendering** — mentions, links, code blocks, blockquotes |
| `Style_RootSplitView.cs` | 614 | Main layout (sidebar + content split) |
| `Style_ComboBox.cs` | 505 | Dropdown selects with full state machine |
| `Style_BorderButton.cs` | 348 | Primary action buttons |
| `Style_SvgButton.cs` | 329 | Icon buttons with hover/pressed states |
| `Style_ComboBoxItem.cs` | 305 | Dropdown items with selection states |
| `Style_TransparentButton.cs` | 227 | Ghost buttons (transparent bg) |
| `Style_ScrollViewer.cs` | 227 | Scroll containers |
| `Style_Slider.cs` | 218 | Volume/setting sliders |
| `Style_CheckBox.cs` | 212 | Custom checkbox with `.ToggleSwitch` class variant |

---

## SimpleTheme Base Layer (Avalonia.Themes.Simple)

Root loads `SimpleTheme` as the first entry in `app.Styles` — it provides the base Avalonia control primitives.

### SimpleTheme Resource Keys

SimpleTheme defines its own `ThemeDictionaries` with `Default` (light) and `Dark` variants, each containing:
- 16 Color values: `ThemeBackgroundColor`, `ThemeBorderLow/Mid/HighColor`, `ThemeControlLow/Mid/MidHigh/High/VeryHighColor`, `ThemeControlHighlightLow/Mid/HighColor`, `ThemeForegroundColor`, `HighlightColor`, `HighlightColor2`, `HyperlinkVisitedColor`
- 21 Brush values derived from above colors: `ThemeBackgroundBrush`, `ThemeBorderLowBrush`, etc.
- Shared resources: `ThemeAccentColor` (`#CC119EDA`), `ThemeAccentColor2/3/4` (decreasing alpha), `ThemeDisabledOpacity` (0.5)
- Caption button resources, RefreshVisualizer resources

**Root's 4 non-Root style references from SimpleTheme:**
- `ThemeControlHighlightLowBrush` → used in RootSplitView
- `ThemeControlLowColor` → used in RootSplitView
- `ThemeDisabledOpacity` → used in Slider (2x)
- `ThemeBorderThickness` → used in DropDownButton

These SimpleTheme keys provide disabled/base state styling that Root's custom styles build on top of.

### ChatView — Settings Page Pattern

ChatView confirms the universal settings page pattern:
- `RootBorder` for card containers (`BackgroundSecondary` bg, `Border` border)
- Title text: `TextPrimary`, Bold, 20pt
- Description text: `TextSecondary`, Weight 450, 14pt
- `CheckBox` with `.ToggleSwitch` class for toggle settings
- `RootScrollViewer` as outer container
- All colors via DynamicResource — zero hardcoded

ChatViewModel shows per-user settings via `ILocalDataStore.TryGetWithPath([userId, "settingKey"], out int)`:
- `AutoConvertEmojis` (default: false)
- `TapToReply` (default: true)

### Override Strategy Options

**Option A: Inject ResourceDictionary at Application level**
- Add a ResourceDictionary to `Application.Current.Resources.MergedDictionaries` with our 32 key overrides
- Resources in MergedDictionaries at the Application level take priority over theme dictionaries
- Simpler to implement but may not be ThemeVariant-aware (would override all themes with one set of values)

**Option B: Find and modify the theme ResourceDictionary directly**
- Locate the Dark/Light/PureDark ResourceDictionary objects at runtime
- Replace individual key values with our custom `ImmutableSolidColorBrush` instances
- Respects ThemeVariant system — we can provide different overrides per theme
- Need to locate dictionaries via `Application.Current.Styles` or `Application.Current.Resources`

**Option C: Subscribe to `RequestedThemeVariant` changes**
- Listen for `Application.Current.GetPropertyChangedObservable(Application.RequestedThemeVariantProperty)`
- When theme changes, re-apply our overrides for the new variant
- Works with any injection approach (A or B)

### Recommended Approach

**Option B + C combined:** Find the theme dictionaries, override the 32 keys, and subscribe to theme variant changes to re-apply on switch. This gives us:
- Instant propagation via DynamicResource
- Correct per-variant behavior
- Automatic re-application when user switches themes

---

## Still Needed

### Research Complete ✅

- [x] **Find `XamlClosure_53`** — ✅ All 32 Dark color hex values decoded
- [x] **Find `Light.axaml` and `PureDark.axaml` population methods** — ✅ Confirmed identical 32 key names, different XamlClosure classes
- [x] **Dump `XamlClosure_54`** — ✅ Light theme complete (note: Build_10 = `uint.MaxValue` = BackgroundSecondary = pure white)
- [x] **Dump `XamlClosure_55`** — ✅ PureDark theme complete (Dark=PureDark for all non-surface keys)
- [x] **Find theme switching code** — ✅ ThemeService, ThemeMapper, RootThemeEnum, ChangeThemeViewModel, ChangeThemeView all decoded
- [x] **Confirm DynamicResource is universal** — ✅ Every view checked uses DynamicResource for Root's 32 keys

### Remaining Research

- [x] **Dump Root's `App` class** — ✅ Theme dicts in `Application.Resources.ThemeDictionaries` (not Styles). Uses `SimpleTheme` + `MediaFluentTheme` (not FluentTheme). 26 custom style files. Override path confirmed.
- [x] **Look for hardcoded ARGB** — ✅ MessageView (232KB) has only role colors + transparent placeholders. DynamicResource is universal.
- [x] **Dump all style files** — ✅ All 27 styles split from StylesAll.cs. 69 DynamicResource usages of 22 Root keys. Only transparent placeholders for hardcoded colors.
- [x] **Dump SimpleTheme** — ✅ Avalonia base theme with Default+Dark variants, accent colors, disabled opacity. 4 SimpleTheme keys used by Root's styles.
- [x] **Dump theme axaml Populate methods** — ✅ ThemesDarkAxaml.cs, ThemesLightAxaml.cs, ThemesPureDarkAxaml.cs (full 259/258/257-entry dictionaries)
- [x] **Dump ChatView/ChatViewModel** — ✅ Confirms settings page pattern, per-user ILocalDataStore, DynamicResource-only
- [ ] **Test ImmutableSolidColorBrush replacement** — verify replacing a deferred value triggers DynamicResource re-resolution (implementation phase)

### Implementation

- [ ] Map Root's 32 keys to our theme palette generation (accent/bg → all 32 derived values)
- [ ] Find the dictionary at runtime via reflection and override keys directly
- [ ] Subscribe to `RequestedThemeVariant` property changes to re-apply overrides on theme switch
- [ ] Evaluate which FluentTheme overrides (if any) are still needed
- [ ] Evaluate how much of the visual tree walk can be removed
- [ ] Update custom ping color to target `SelfMentionBackground`/`SelfMentionBorder` directly
- [ ] Consider AvaloniaEdit's 10 keys for compose input theming

---

## Raw Decompilation Sources

ILSpy dumps stored in `research/ilspy-dumps/` (219 files committed to repo, 145,733 lines total):

| File | Contents |
|------|----------|
| `XamlClosure_53.cs` | Dark theme — all 32 Build methods with uint32 color values |
| `XamlClosure_54.cs` | Light theme — all 32 Build methods (note: Build_10 = `uint.MaxValue`) |
| `XamlClosure_55.cs` | PureDark theme — all 32 Build methods |
| `ThemeService.cs` | **Theme switcher** — SetTheme, InitializeTheme, persistence |
| `ThemeMapper.cs` | **ThemeVariant mapping** — PureDark inherits Dark, bridge string conversion |
| `RootThemeEnum.cs` | Enum: Default, Dark, Light, PureDark |
| `ChangeThemeViewModel.cs` | Theme picker ViewModel (ObservableProperty, OnThemeChanged) |
| `ChangeThemeViewModelFactory.cs` | Factory for ChangeThemeViewModel (DI) |
| `ChangeThemeView.cs` | Theme picker view (4 RadioButtons, DynamicResource for all colors) |
| `-AvaloniaResources-GREPONLY.cs` | Full CompiledAvaloniaXaml (55k lines) — all Populate/Build methods. Grep-only, never read in full. |
| `XamlDynamicSetters.cs` | Dynamic property setters for compiled XAML |
| `XamlIlContext.cs` | XAML IL compilation context |
| `XamlIlHelpers-GREPONLY.cs` | XAML IL helper methods (33k lines). Grep-only, never read in full. |
| `XamlIlTrampolines.cs` | XAML IL trampoline stubs |
| `Application.cs` | Avalonia.Application base class (resource resolution order) |
| `AppBuilder.cs` | Avalonia.AppBuilder (app setup chain) |
| `Program.cs` | Root.exe entry point (DI, Velopack, startup) |
| `DataStoreKeys.cs` | Complete settings enum (68 keys including Theme) |
| `MessageView.cs` | **Message view** (232KB, 33 DynamicResource calls, 0 theme-related hardcoded colors) |
| `MessageViewModel.cs` | Message ViewModel (CommunityToolkit.Mvvm, role color handling) |
| `ChannelStartMessageView.cs` | Channel welcome view (DynamicResource for all colors) |
| `ChannelStartMessageViewModel.cs` | Channel start ViewModel |
| `ToggleSwitch.cs` | Avalonia ToggleSwitch (standard, no custom theme keys) |
| `CheckBox.cs` | Avalonia CheckBox (standard, no custom theme keys) |
| `RootBorder.cs` | DPI-aware custom Border (StyleKeyOverride → Border) |
| `ThemeToBoolConverter.cs` | ThemeVariant ↔ bool converter for radio button binding |
| `App.cs` | **Root's App class** — theme dictionary wiring, style stack, MediaFluentTheme, 26 custom styles |
| `ThemesDarkAxaml.cs` | Dark.axaml full Populate method — 259 entries incl. 32 color keys + SVG paths + badges |
| `ThemesLightAxaml.cs` | Light.axaml full Populate method — 258 entries |
| `ThemesPureDarkAxaml.cs` | PureDark.axaml full Populate method — 257 entries |
| `FontsAxaml.cs` | Fonts.axaml — `RootFont` and `RootConsolas` font family resources |
| `SoundsAxaml.cs` | Sounds.axaml — 15 sound effect wav paths |
| `SimpleTheme.cs` | Avalonia.Themes.Simple (1022 lines) — base theme with ThemeDictionaries (Default+Dark), accent colors |
| `ChatView.cs` | Chat settings view — DynamicResource pattern, RootBorder cards, ToggleSwitch checkboxes |
| `ChatViewModel.cs` | Chat settings VM — AutoConvertEmojis, TapToReply (per-user ILocalDataStore) |
| `StylesAll-GREPONLY.cs` | Combined 27 style Populate methods (5,228 lines) — split into Style_*.cs below |
| `Style_MessageMarkdown.cs` | **1,074 lines** — all mention types, links, code blocks, blockquotes |
| `Style_RootSplitView.cs` | 614 lines — main layout sidebar + content split |
| `Style_ComboBox.cs` | 505 lines — dropdown with full state machine |
| `Style_BorderButton.cs` | 348 lines — primary action buttons |
| `Style_SvgButton.cs` | 329 lines — icon buttons |
| `Style_ComboBoxItem.cs` | 305 lines — dropdown items |
| `Style_TransparentButton.cs` | 227 lines — ghost buttons |
| `Style_ScrollViewer.cs` | 227 lines — scroll containers |
| `Style_Slider.cs` | 218 lines — sliders |
| `Style_CheckBox.cs` | 212 lines — checkbox + .ToggleSwitch variant |
| `Style_MenuItem.cs` | 177 lines — context menu items |
| `Style_DropDownButton.cs` | 121 lines — dropdown trigger buttons |
| `Style_ListBoxItem.cs` | 96 lines — list items |
| `Style_TabItem.cs` | 92 lines — tab items |
| `Style_RootColorPicker.cs` | 83 lines — color picker styles |
| `Style_LinkButton.cs` | 72 lines — link-styled buttons |
| `Style_Separator.cs` | 68 lines — separators |
| `Style_ListBox.cs` | 63 lines — list containers |
| `Style_TextButton.cs` | 58 lines — text-only buttons |
| `Style_RootImageLoader.cs` | 53 lines — image loader |
| `Style_MenuFlyoutPresenter.cs` | 51 lines — flyout menu presenters |
| `Style_BorderlessTextbox.cs` | 50 lines — textbox without borders |
| `Style_ToolTip.cs` | 49 lines — tooltips |
| `Style_DragTabItem.cs` | 37 lines — draggable tab items |
| `Style_FlyoutPresenter.cs` | 34 lines — generic flyout presenters |
| `Style_TabsTheme.cs` | 33 lines — tab theme |
| `Style_TabsControl.cs` | 32 lines — tab control container |
| `NamespaceInfo_Themes_Base.cs` | AvaloniaEdit XML namespace registrations |
| `NamespaceInfo_Themes_Simple.cs` | AvaloniaEdit Simple theme namespace registrations |
| `NamespaceInfo_Themes_Fluent.cs` | AvaloniaEdit Fluent theme namespace registrations |

The `DirectMessageTabView` decompilation was reviewed in conversation but not saved to a file.

---

**Canonical for:** Root's 32 color resource key hex values (all 3 themes), XamlClosure decompilation, App.cs ThemeDictionaries structure, AvaloniaEdit 10 keys, DynamicResource usage analysis across all 27 style files
**Implementation guidance in:** [THEME_ENGINE_DEEP_DIVE.md §Resource-First Migration](../docs/framework/THEME_ENGINE_DEEP_DIVE.md#resource-first-migration-plan)
*Last updated: 2026-02-19 — Research complete. Full source mapping: App.cs wiring, all 27 style files analyzed (69 DynamicResource usages of 22 Root keys), SimpleTheme base layer decoded, ChatView/ChatViewModel settings pattern, definitive override path confirmed.*
