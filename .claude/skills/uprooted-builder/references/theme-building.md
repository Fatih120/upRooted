# Theme Building Reference

How to create themes for Uprooted. Themes operate on two layers: CSS variables (TypeScript/browser) and native Avalonia color maps (C# hook).

## CSS Variable System

Root uses `--rootsdk-*` CSS custom properties as overrides for its base `--color-*` variables.

**Override mechanism:**
```css
/* Root's stylesheet defines: */
--color-brand-primary: var(--rootsdk-brand-primary, #3B6AF8);
```

Setting `--rootsdk-brand-primary` overrides the base value. Removing it reverts to the fallback.

### All 25 Override Variables

| Override Variable | Base Variable | Default (Dark) | Category |
|-------------------|--------------|-----------------|----------|
| `--rootsdk-brand-primary` | `--color-brand-primary` | `#3B6AF8` | Brand primary blue |
| `--rootsdk-brand-secondary` | `--color-brand-secondary` | `#A8FF5D` | Brand secondary green |
| `--rootsdk-brand-tertiary` | `--color-brand-tertiary` | `#49D6AC` | Brand tertiary teal |
| `--rootsdk-text-primary` | `--color-text-primary` | `#F2F2F2` | Primary text |
| `--rootsdk-text-secondary` | `--color-text-secondary` | `#A7A7A8` | Secondary text |
| `--rootsdk-text-tertiary` | `--color-text-tertiary` | `#7B7B89` | Tertiary text |
| `--rootsdk-text-white` | `--color-text-white` | `#F2F2F2` | Always-white text |
| `--rootsdk-background-primary` | `--color-background-primary` | `#0D1521` | Main background |
| `--rootsdk-background-secondary` | `--color-background-secondary` | `#121A26` | Secondary background |
| `--rootsdk-background-tertiary` | `--color-background-tertiary` | `#07101B` | Tertiary background |
| `--rootsdk-input` | `--color-input` | `#090E13` | Input field background |
| `--rootsdk-border` | `--color-border` | `#242C36` | Border/stroke color |
| `--rootsdk-highlight-light` | `--color-highlight-light` | `#FFFFFF0A` | Light highlight (4%) |
| `--rootsdk-highlight-normal` | `--color-highlight-normal` | `#FFFFFF19` | Normal highlight (10%) |
| `--rootsdk-highlight-strong` | `--color-highlight-strong` | `#FFFFFF30` | Strong highlight (19%) |
| `--rootsdk-info` | `--color-info` | `#F0F250` | Info/status yellow |
| `--rootsdk-warning` | `--color-warning` | `#E88F3D` | Warning orange |
| `--rootsdk-error` | `--color-error` | `#F03F36` | Error red |
| `--rootsdk-muted` | `--color-muted` | `#4F5C6F` | Muted/disabled state |
| `--rootsdk-link` | `--color-link` | `#88A5FF` | Link color |
| `--rootsdk-background-blur` | `--color-background-blur` | `#00000080` | Blur backdrop overlay |
| `--rootsdk-self-mention` | `--color-self-mention` | `#FF2D1F66` | Self-mention highlight |
| `--rootsdk-community-mention` | `--color-community-mention` | `#A8FF5D33` | Community mention |
| `--rootsdk-channel-mention` | `--color-channel-mention` | `#E88F3D4D` | Channel mention |
| `--rootsdk-transparent` | `--color-transparent` | `transparent` | Fully transparent |

### Root Theme Variants

Root has 3 built-in themes that change these defaults:
- **Dark** (default) — navy-blue backgrounds
- **Light** — white/gray backgrounds (`#FBFBFB`, `#FFFFFF`, `#F5F6F8`), dark text (`#131313`)
- **Pure Dark** — neutral gray backgrounds (`#161617`, `#1F1F22`, `#111113`)

## TypeScript Theme Building

### Adding a Preset Theme

Add an entry to `src/plugins/themes/themes.json`:

```json
{
  "name": "my-theme",
  "variables": {
    "--rootsdk-brand-primary": "#C42B1C",
    "--rootsdk-brand-secondary": "#D94A3D",
    "--rootsdk-brand-tertiary": "#A32417",
    "--rootsdk-background-primary": "#241414",
    "--rootsdk-background-secondary": "#2C1818",
    "--rootsdk-background-tertiary": "#1A0E0E",
    "--rootsdk-input": "#1E1010",
    "--rootsdk-border": "#402828",
    "--rootsdk-link": "#E06B60",
    "--rootsdk-muted": "#6F5050"
  }
}
```

You don't need to override all 25 variables. Unspecified variables keep their Root defaults.

### Programmatic Theming in a Plugin

```typescript
import { native } from "../../api/index.js";

// Apply
native.setCssVariables({
  "--rootsdk-brand-primary": "#ff0000",
  "--rootsdk-background-primary": "#1a1a2e",
});

// Revert a single variable
native.removeCssVariable("--rootsdk-brand-primary");
```

### Custom Theme Generation

The built-in themes plugin can generate a full theme from just accent + background:

```typescript
import { generateCustomVariables } from "../plugins/themes/index.js";

const vars = generateCustomVariables("#C42B1C", "#241414");
// Returns 10 derived variables:
// brand-primary, brand-secondary, brand-tertiary,
// background-primary, background-secondary, background-tertiary,
// input, border, link, muted
```

Algorithm: lighten/darken accent and bg at fixed percentages. Dark vs light detection based on luminance threshold (0.3).

### Cleanup Pattern

Always revert in `stop()`:

```typescript
// Track all variables you set
const allVarNames = [
  "--rootsdk-brand-primary",
  "--rootsdk-background-primary",
  // ...
];

start() {
  // Flush first to clear any stale values
  for (const name of allVarNames) native.removeCssVariable(name);
  // Apply new values
  native.setCssVariables({ ... });
},

stop() {
  for (const name of allVarNames) native.removeCssVariable(name);
},
```

## C# Native ThemeEngine

The native theme engine (`hook/ThemeEngine.cs`) handles Avalonia controls that CSS can't reach: title bar, sidebar, window chrome, overlays.

### Color Map Structure

Each theme defines an ARGB-to-ARGB color mapping dictionary:

```csharp
// Map format: original "#AARRGGBB" -> replacement "#AARRGGBB"
private static readonly Dictionary<string, Dictionary<string, string>> PresetColorMaps = new()
{
    ["crimson"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        // Blue accents -> red accents
        ["#ff3b6af8"] = "#ffc42b1c",    // Primary blue -> deep red
        ["#ff4a78f9"] = "#ffd94a3d",    // Lighter blue -> lighter red

        // Structural backgrounds -> warm dark
        ["#ff0d1521"] = "#ff1a0e0e",    // Main dark bg
        ["#ff0f1923"] = "#ff241414",    // Card bg

        // Text with alpha preservation
        ["#a3f2f2f2"] = "#a3f0dada",    // Muted text
        ["#66f2f2f2"] = "#66f0dada",    // Dim text
    },
};
```

**Rules for color maps:**
- Keys use `#AARRGGBB` format (uppercase, Avalonia's `Color.ToString()` format)
- Each replacement value must be unique within the map (different from all other replacements)
- Never map `#19ffffff` or `#0affffff` — these are hover overlays; theming them causes persistent hover effects
- Alpha channels are preserved (e.g., `#a3` prefix stays `#a3`)

### Resource Key Palette (55 keys)

The ThemeEngine generates a palette of 55 Avalonia resource dictionary keys from accent + background colors:

```
SystemAccentColor, SystemAccentColorLight1/2/3, SystemAccentColorDark1/2/3,
TextFillColorPrimary, TextFillColorSecondary, TextFillColorTertiary, TextFillColorDisabled,
TextOnAccentFillColorPrimary, TextOnAccentFillColorSecondary, TextOnAccentFillColorDisabled,
AccentTextFillColorPrimary, AccentTextFillColorSecondary, AccentTextFillColorTertiary,
ControlFillColorDefault, ControlFillColorSecondary, ControlFillColorTertiary,
ControlFillColorDisabled, ControlFillColorInputActive,
SubtleFillColorTransparent, SubtleFillColorSecondary, SubtleFillColorTertiary, SubtleFillColorDisabled,
ControlElevationBorderBrush, CircleElevationBorderBrush, TextControlElevationBorderBrush,
AccentControlElevationBorderBrush, ControlStrokeColorDefault, ControlStrokeColorSecondary,
CardStrokeColorDefault, ControlStrongStrokeColorDefault, ControlStrongStrokeColorDisabled,
DividerStrokeColorDefault, SurfaceStrokeColorDefault, SurfaceStrokeColorFlyout,
FocusStrokeColorOuter, FocusStrokeColorInner,
CardBackgroundFillColorDefault, CardBackgroundFillColorSecondary,
SolidBackgroundFillColorBase, SolidBackgroundFillColorSecondary,
SolidBackgroundFillColorTertiary, SolidBackgroundFillColorQuarternary,
SystemFillColorSuccess, SystemFillColorCaution, SystemFillColorCritical,
SystemFillColorNeutral, SystemFillColorSolidNeutral,
SystemFillColorAttentionBackground, SystemFillColorSuccessBackground,
SystemFillColorCautionBackground, SystemFillColorCriticalBackground,
SystemFillColorNeutralBackground, SystemFillColorSolidAttentionBackground,
SystemFillColorSolidNeutralBackground
```

These are injected into `Application.Current.Styles[0].Resources` to override Avalonia's built-in resource brushes.

### Two-Phase Application

1. **Resource injection** — Replace resource dictionary entries for DynamicResource bindings
2. **Visual tree walk** — DFS traversal replacing `Background`, `Foreground`, `BorderBrush`, `Fill` properties on controls with hardcoded colors

The tree walk uses:
- **Style priority** for normal application (preserves hover/pressed triggers)
- **LocalValue priority** for live preview during color picker drag (force-overrides existing LocalValues)

### Live Preview Pattern

During color picker drag (16ms throttle to prevent UI lag):

```
1. Generate new palette from accent + bg
2. Update resource dictionary entries
3. Walk visual tree with brush cache (avoid creating new brushes per-control)
4. Update ContentPages themed color statics
5. Rebuild active page with new colors
```

Brush caching: `Dictionary<string, object>` maps hex strings to pre-created brush instances during live preview to avoid GC pressure.

### Cleanup (Revert)

Theme revert is multi-phase:

1. **Disable color map** — set `_activeColorMap = null` to prevent re-application
2. **Restore resources** — put saved originals back into `Styles[0].Resources`, remove added keys
3. **Remove MergedDictionary** — remove injected resource dictionary
4. **Targeted purge** — `ClearValue` on all controls whose current color is in the known theme color set
5. **Follow-up walks** — scheduled at +500ms, +1500ms, +3000ms to catch lazily-loaded controls

**Always clean up native theme state.** Unlike CSS variables which can be simply removed, Avalonia resource changes persist until explicitly reverted.

### Adding a Native Theme Preset

Add a new entry to `PresetColorMaps` in `ThemeEngine.cs`:

```csharp
["my-theme"] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
    // Accent colors (primary blue -> your accent)
    ["#ff3b6af8"] = "#ff......",
    ["#ff4a78f9"] = "#ff......",
    ["#ff2e59d1"] = "#ff......",
    // ... more accent variants with alpha

    // Card background
    ["#ff0f1923"] = "#ff......",

    // Structural backgrounds
    ["#ff0d1521"] = "#ff......",
    ["#ff07101b"] = "#ff......",
    ["#ff090e13"] = "#ff......",
    // ... more bg variants

    // Borders
    ["#ff242c36"] = "#ff......",
    ["#ff1a2230"] = "#ff......",

    // Text (preserve alpha prefixes)
    ["#a3f2f2f2"] = "#a3......",
    ["#66f2f2f2"] = "#66......",
    ["#ffdedede"] = "#ff......",
    ["#fff2f2f2"] = "#ff......",
},
```

The color map must be comprehensive — any unmapped color stays as Root's default, which can create visual inconsistency.

## Dual-Layer Theme Coordination

For a complete theme, you need both layers:

1. **TypeScript** — Override `--rootsdk-*` CSS variables (browser UI)
2. **C#** — Define color map in `ThemeEngine.cs` (native Avalonia UI)

The themes plugin start/stop and the ThemeEngine apply/revert are coordinated through `SidebarInjector` which calls both when the user switches themes.
