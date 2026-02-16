# Root.exe Theme Resource Extraction

Extracted from: Root.exe v0.9.86 (617 MB)
Date: 2026-02-12

## Architecture

Root.exe uses a **dual theme system**:

1. **Native Avalonia AXAML themes** (Dark.axaml, PureDark.axaml, Light.axaml):
   - Stored as compiled AXAML in the .NET single-file bundle
   - Contain primarily SVG asset path references (theme-specific icons)
   - Three header properties per theme: ThemeName, ThemeMargin, ScrollShadow color
   - 72+ SVG resource key mappings per theme (Light/Dark icon variants)
   - PureDark is minimal (196 bytes) and inherits Dark theme SVGs

2. **CSS theme variables** (injected into DotNetBrowser/Chromium webview):
   - 25 CSS custom properties per theme defining ALL UI colors
   - Three theme variants: dark, light, pure-dark
   - Applied via data-theme attribute on document body
   - Each variable has a --rootsdk-* override via var()
   - Initialized by RootThemeCssPropInit in .NET code

## Binary Locations

| Resource | Offset | Encoding | Size |
|----------|--------|----------|------|
| CSS theme definitions | 0x12AE9676 | ASCII/UTF-8 | ~3.2 KB |
| Light.axaml compiled AXAML | 0x19EED01F | UTF-16LE | ~28.5 KB |
| Dark.axaml compiled AXAML | 0x19EF3FB0 | UTF-16LE | ~21.5 KB |
| PureDark.axaml compiled AXAML | 0x19EF93D8 | UTF-16LE | ~196 B |
| Build markers (UTF-8) | 0x19E06A8A | ASCII | N/A |
| RootThemeCssPropInit methods | 0x19D73C77 | ASCII | 73 refs |

## CSS Theme Color Definitions

### Dark Theme (data-theme=dark)

| Variable | Default Value | Description |
|----------|---------------|-------------|
| --color-brand-primary | #3B6AF8 | Primary brand blue |
| --color-brand-secondary | #A8FF5D | Secondary brand green |
| --color-brand-tertiary | #49D6AC | Tertiary brand teal |
| --color-text-primary | #F2F2F2 | Primary text (white) |
| --color-text-secondary | #A7A7A8 | Secondary text (gray) |
| --color-text-tertiary | #7B7B89 | Tertiary text (dark gray) |
| --color-text-white | #F2F2F2 | White text |
| --color-background-primary | #0D1521 | Main bg (dark navy) |
| --color-background-secondary | #121A26 | Secondary bg (lighter navy) |
| --color-background-tertiary | #07101B | Tertiary bg (darkest navy) |
| --color-input | #090E13 | Input field background |
| --color-border | #242C36 | Border color |
| --color-highlight-light | #FFFFFF0A | Light highlight (4% white) |
| --color-highlight-normal | #FFFFFF19 | Normal highlight (10% white) |
| --color-highlight-strong | #FFFFFF30 | Strong highlight (19% white) |
| --color-info | #F0F250 | Info yellow |
| --color-warning | #E88F3D | Warning orange |
| --color-error | #F03F36 | Error red |
| --color-muted | #4F5C6F | Muted/disabled color |
| --color-link | #88A5FF | Link color (light blue) |
| --color-background-blur | #00000080 | Blur overlay (50% black) |
| --color-self-mention | #FF2D1F66 | Self-mention (40% red) |
| --color-community-mention | #A8FF5D33 | Community mention (20% green) |
| --color-channel-mention | #E88F3D4D | Channel mention (30% orange) |
| --color-transparent | transparent | Transparent |

### Light Theme (data-theme=light)

| Variable | Default Value | Notes vs Dark |
|----------|---------------|---------------|
| --color-brand-primary | #3B6AF8 | Same |
| --color-brand-secondary | #58AC30 | Darker green |
| --color-brand-tertiary | #08a677 | Darker teal |
| --color-text-primary | #131313 | Near-black |
| --color-text-secondary | #282828 | Dark gray |
| --color-text-tertiary | #5E5E5E | Medium gray |
| --color-text-white | #F2F2F2 | Same |
| --color-background-primary | #FBFBFB | Near-white |
| --color-background-secondary | #FFFFFF | Pure white |
| --color-background-tertiary | #F5F6F8 | Light gray |
| --color-input | #F5F5F5 | Light gray |
| --color-border | #DDDDDD | Light border |
| --color-highlight-light | #2222220A | Dark highlight (4%) |
| --color-highlight-normal | #22222219 | Dark highlight (10%) |
| --color-highlight-strong | #22222226 | Dark highlight (15%) |
| --color-info | #EA9134 | Orange (differs\!) |
| --color-warning | #EA9134 | Same as info |
| --color-error | #F03F36 | Same |
| --color-muted | #A9B3C0 | Lighter muted |
| --color-link | #006eeb | Darker blue |
| --color-background-blur | #6B6B6B80 | Gray blur (50%) |
| --color-self-mention | #FF2D1F66 | Same |
| --color-community-mention | #90CC5C4D | Different green (30%) |
| --color-channel-mention | #E88F3D4D | Same |
| --color-transparent | transparent | Same |

### Pure Dark Theme (data-theme=pure-dark)

| Variable | Default Value | Notes vs Dark |
|----------|---------------|---------------|
| --color-brand-* | Same as Dark | Identical |
| --color-text-* | Same as Dark | Identical |
| --color-background-primary | #161617 | True dark gray (not navy) |
| --color-background-secondary | #1F1F22 | Dark gray (not navy) |
| --color-background-tertiary | #111113 | Very dark gray |
| --color-input | #101112 | Nearly black |
| --color-border | #303030 | Neutral gray border |
| --color-highlight-* | Same as Dark | Identical |
| --color-info/warning/error | Same as Dark | Identical |
| --color-muted/link | Same as Dark | Identical |
| --color-*-mention | Same as Dark | Identical |

## Native Avalonia Theme Properties

Each compiled AXAML theme file defines these native properties:

| Property | Light | Dark | PureDark |
|----------|-------|------|----------|
| ThemeName | Light Theme | Dark Theme | Pure Dark Theme |
| ThemeMargin | 0 0 12 0 | 0 0 12 0 | 0 0 12 0 |
| ScrollShadow | #19000000 (10% black) | #80000000 (50% black) | #000000 (pure black) |
| LoadingSpinner | LoadingSpinnerLight.json | LoadingSpinnerDark.json | (inherits Dark) |

## Key Observations

1. **Dark theme uses navy-blue backgrounds** (#0D1521, #121A26, #07101B) while Pure Dark uses neutral grays (#161617, #1F1F22, #111113)
2. **Brand primary (#3B6AF8)** is consistent across all themes - the distinctive Root blue
3. **Brand secondary differs**: Dark/PureDark=#A8FF5D (bright green), Light=#58AC30 (muted green)
4. **PureDark is minimal** (196 bytes AXAML) - overrides only header props, inherits all SVG refs from Dark
5. **CSS var override**: Each --color-* uses var(--rootsdk-*) allowing SDK consumers to override
6. **ScrollShadow encodes theme intensity**: Light=10% black, Dark=50% black, PureDark=100% black
7. **Info color inconsistency**: Dark uses yellow (#F0F250) but Light uses orange (#EA9134, same as warning)
8. **Theme colors are CSS-only** - the native Avalonia AXAML contains no color brush resources; all UI colors are delivered via CSS to the Chromium webview
9. **The compiled AXAML themes serve as icon theme packs** - mapping ~72 SVG resource keys to Light/Dark icon variants

## Files Generated

-  - This report
-  - Complete CSS with all three theme color definitions
-  - All 72 SVG resource key names from the AXAML themes