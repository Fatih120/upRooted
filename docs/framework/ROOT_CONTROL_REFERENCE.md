# Root Control Reference

> **Source:** ILSpy decompilation of `RootApp.Client.Avalonia v0.9.92.0` — all findings confirmed from compiled IL.
> **Related:** [Avalonia Patterns](AVALONIA_PATTERNS.md) | [Theme Engine Deep Dive](THEME_ENGINE_DEEP_DIVE.md) | [Hook Reference](HOOK_REFERENCE.md) | [Root Theme System Findings](../../research/ROOT_THEME_SYSTEM_FINDINGS.md)

This document is the authoritative reference for Root's custom controls, style class system, message view structure, and behavioral patterns as discovered by ILSpy decompilation. Use this when building new Uprooted features to match Root's visual language and avoid rediscovering control internals.

---

## Table of Contents

1. [Custom Controls](#custom-controls)
2. [Style Class System](#style-class-system)
3. [Resource Key Reference](#resource-key-reference)
4. [Theme System Mechanics](#theme-system-mechanics)
5. [Message View Internals](#message-view-internals)
6. [Mention and Markdown System](#mention-and-markdown-system)
7. [Settings Page Pattern](#settings-page-pattern)
8. [Ping Color and Mention Highlighting](#ping-color-and-mention-highlighting)
9. [App Startup Chain](#app-startup-chain)
10. [DataStore Keys](#datastore-keys)

---

## Custom Controls

Root defines the following custom controls. All inherit from standard Avalonia base classes.

### RootBorder

```csharp
public class RootBorder : Border {
    protected override Type StyleKeyOverride => typeof(Border);  // Uses Border styles
    public static readonly StyledProperty<Thickness> DynamicBorderThicknessProperty;
}
```

- Inherits all `Border` styles (no custom theme keys needed)
- `DynamicBorderThickness` property: rounds to nearest physical pixel at `MainWindow.RenderScaling`
- Used everywhere as a container: settings cards, content borders, etc.
- Corner radius: 12 for settings cards, 4–8 for smaller elements
- **Pattern:** `DynamicBorderThickness = new Thickness(0.5)` for crisp hairline borders

In Uprooted (via reflection): access as `Border` type — same properties, same style keys.

---

### RootSvgImage

SVG image control with per-theme path binding.

```csharp
public class RootSvgImage : ... {
    public static readonly StyledProperty<string?> SvgPathProperty;
}
```

- `SvgPath` bound via `DynamicResourceExtension("DarkThemePreviewSVG")` etc.
- SVG paths come from theme resource dictionaries (e.g. `"TextChannelWelcomeSectionSVG"`, `"PinSVG"`, `"PinFilledSVG"`)
- MessageView injects SVG paths at runtime: `SystemMessageSvgImage[!RootSvgImage.SvgPathProperty] = new DynamicResourceExtension("ChannelEditedSVG")`

**Key SVG resource keys** (from MessageView):
`ChannelEditedSVG`, `CommunityEditedSVG`, `MessagePinnedSVG`, `MessageUnpinnedSVG`, `UserJoinedSVG`, `UserLeftSVG`, `CallStartedSVG`, `CallEndedSVG`, `UserBannedSVG`, `UserKickedSVG`, `InfoSVG`, `PinSVG`, `PinFilledSVG`, `DarkThemePreviewSVG`, `LightThemePreviewSVG`, `PureDarkThemePreviewSVG`, `SyncThemePreviewSVG`, `TextChannelWelcomeSectionSVG`

---

### RootSvgButton

Icon button using SVG path resource.

```csharp
public class RootSvgButton : ... {
    public static readonly StyledProperty<double> SvgOpacityProperty;
    public static readonly StyledProperty<double> SvgBorderOpacityProperty;
    // Also: SvgPathProperty (same pattern as RootSvgImage)
}
```

**Style classes** (append to button's `.Classes`):

| Class | Default | Hover | Pressed | Description |
|-------|---------|-------|---------|-------------|
| (none) | SvgOpacity=1, BgHighlightNormal | SvgBorderOpacity=0.7 | SvgOpacity=0.5 | Standard icon button |
| `SvgDimmedButton` | SvgOpacity=0.6 | SvgOpacity=1.0 | SvgOpacity=0.4 | Initially dimmed, brightens on hover |
| `Custom` | bg transparent | opacity=0.7 | opacity=0.5 | Custom background |
| `CustomSvgDimmedButton` | SvgOpacity=0.6 | SvgOpacity=1.0 | SvgOpacity=0.4 | Custom bg, dimmed |
| `CustomSvgDimmedButtonSolid` | SvgOpacity=0.6 | SvgOpacity=1.0 | SvgOpacity=0.4 | Solid custom bg |

Named parts: all use `HighlightNormal` for background hover on non-Custom variants.

MessageView action buttons: `AddReactionActionButton`, `ReplyActionButton`, `EditMessageActionButton`, `PinMessageActionButton`, `MoreOptionsActionButton`, `ShiftDeleteActionButton` — all `RootSvgButton`.

---

### RootSvgCheckBox

Checkbox with SVG check mark.

```csharp
public class RootSvgCheckBox : CheckBox {
    public static readonly StyledProperty<double> SvgOpacityProperty;     // unchecked: 0.6, checked: 1.0
    public static readonly StyledProperty<double> SvgBorderOpacityProperty; // unchecked: 0.0, checked: 0.7
}
```

Background: `HighlightNormal`. CornerRadius: 5. No border. Hand cursor.

---

### RootScrollViewer / RootScrollBarThumb

`RootScrollViewer` background: transparent (`#00FFFFFF`). Custom template.

`RootScrollBarThumb` scroll thumb colors:
- Color: `TextPrimary` (the same color as text, at low opacity)
- Default: `opacity=0` (hidden)
- On parent hover: `opacity=0.1`
- Pointer over: `opacity=0.2`
- Pressed: `opacity=0.4`
- Width (vertical): 4px, CornerRadius: 2

---

### RootMarkdownTextBlock

Used as `MessageTextBlock` in MessageView. Renders markdown with `CTextBlock`, `CInline`, `CHyperlink`, `CRun`, `CSpan`, `CCode` components.

```csharp
internal RootMarkdownTextBlock MessageTextBlock;
public DocumentElement? Document => MessageTextBlock.Document;
```

Class name on container element: `RootMarkdown` or `SimpleMessage` (from style selectors).

---

### RootLinkButton

Used for username display in MessageView:
```csharp
internal RootLinkButton UsernameTextBlock;
```

Has `ForegroundProperty` for text color. `updateMemberColor()` sets it via `ThemeService.GetInvertedDefaultColorHex` for role colors.

---

### RootMenuFlyout

Context menu used in MessageView:
```csharp
private RootMenuFlyout? _contextMenu;
_contextMenu = MessageBackgroundBorder.ContextFlyout as RootMenuFlyout;
```

Contains `MenuItem` items with classes like `DeleteMenuItem`. `MenuItem` selected state uses `HighlightNormal`.

---

### RootMessageScrollViewer

Scroll container in the message list. MessageView discovers it via:
```csharp
_parentScrollViewer = this.FindAncestorOfType<RootMessageScrollViewer>();
```

---

## Style Class System

Root uses Avalonia's CSS class system to apply visual variants. These classes must be added to controls' `Classes` collection.

### Button Classes

```
Button.Classes.Add("ListBorderButton")   // Full-width list row button
Button.Classes.Add("BorderButton")       // Standard action button
Button.Classes.Add("BasicButton")        // Simplified, no focus indicator
Button.Classes.Add("BasicButtonNeverOpaque") // Never goes opaque on disabled
Button.Classes.Add("MenuBorderButton")   // Context menu button, transparent bg
Button.Classes.Add("TransparentButton")  // Fully transparent (opacity=0, not clickable)
Button.Classes.Add("TransparentButtonWithHighlight")  // hover=HighlightNormal, pressed=HighlightLight
Button.Classes.Add("TransparentButtonWithOpacity")    // hover=opacity 0.75
Button.Classes.Add("TransparentButtonWithClickEffect") // pressed=scale(0.98)
```

**ListBorderButton hover:** `HighlightNormal` background on the `PART_Border` template element.
**BorderButton hover:** `opacity=0.7`.
**MenuBorderButton hover:** `HighlightNormal` background.

### CheckBox as Toggle Switch

Root renders toggle switches as `CheckBox` with class `.ToggleSwitch`:

```csharp
checkBox.Classes.Add("ToggleSwitch");
```

Style details:
- Size: 45×25, CornerRadius: 12
- Unchecked: background = `Muted` (`#4F5C6F`)
- Checked: background = `BrandPrimary` (`#3B6AF8`)
- Knob (`PART_Ellipse`): stroke = `BrandPrimary`, `HorizontalAlignment=Right` when checked
- Disabled: `opacity=0.3`

**This is exactly what `ContentPages.cs` uses for Uprooted plugin toggle switches** — confirming the pattern is correct.

### TabItem

Selected state:
- `PART_TextBlock.Foreground` = `TextPrimary`
- `PART_SelectedPipe` border becomes visible
- Disabled: `PART_Grid` becomes invisible

### ComboBoxItem Selection States

| State | Background | Border |
|-------|-----------|--------|
| hover | `HighlightNormal` | `Border` |
| pressed | `HighlightNormal` | `Border` |
| selected | `HighlightStrong` | `HighlightStrong` |
| selected+hover | `HighlightNormal` | `Border` |
| selected+pressed | `HighlightStrong` | `HighlightStrong` |

### ListBox / ListBoxItem

**Critical finding:** All `ListBoxItem` states (default, hover, selected, selected+focus, selected+focus+hover) set `ContentPresenter.Background` to `#00FFFFFF` (transparent). **The sidebar nav selection highlight does NOT come from the ListBoxItem style.** Each nav item template must render its own selection indicator.

`ListBox` styles: transparent background, `#00FFFFFF` border.

---

## Resource Key Reference

Root's 32 color resource keys are defined in `Application.Resources.ThemeDictionaries[variant]`. Every Root view uses `DynamicResourceExtension` to bind to these keys. Overriding them propagates instantly to all bound controls.

### Keys Used by Styles (Controls)

These keys are referenced in Root's style files — overriding them themes all controls:

| Key | Control Consumers | Notes |
|-----|------------------|-------|
| `TextPrimary` | All text, ComboBoxItem foreground, ScrollBar thumb | Most important |
| `HighlightNormal` | Button hover, SvgButton bg, ComboBoxItem hover, ListBorderButton hover, MenuItem hover | Hover state |
| `BrandPrimary` | CheckBox checked bg, Slider foreground, CTextBlock selection, otherMention text | Accent |
| `Border` | ComboBoxItem border, CodeBlock border, Separator, Slider bg | Dividers |
| `HighlightStrong` | ComboBoxItem selected/pressed bg (x4) | Strong selection |
| `BackgroundSecondary` | MessageMarkdown code block bg | Code bg |
| `BackgroundTertiary` | DropDownButton | Dark areas |
| `TextSecondary` | MessageMarkdown SimpleMessage text | Secondary text |
| `Input` | ComboBox background | Input fields |
| `Muted` | CheckBox unchecked track | Disabled/muted |
| `HighlightLight` | TransparentButtonWithHighlight pressed | Subtle press |
| `Link` | MessageMarkdown hyperlinks | URL color |
| `SelfMentionBackground` | MessageMarkdown selfMention pill bg | Inline mention |
| `SelfMentionBorder` | MessageMarkdown selfMention pill border | Inline mention |
| `OtherMentionBackground` | MessageMarkdown otherMention pill bg | Inline mention |
| `OtherMentionBorder` | MessageMarkdown otherMention pill border | Inline mention |
| `RoleMentionBackground` | MessageMarkdown roleMention pill bg | Inline mention |
| `RoleMentionBorder` | MessageMarkdown roleMention pill border | Inline mention |
| `RoleMentionText` | MessageMarkdown roleMention text | Inline mention |
| `ChannelMentionBackground` | MessageMarkdown channelMention pill bg | Inline mention |
| `ChannelMentionBorder` | MessageMarkdown channelMention pill border | Inline mention |
| `ChannelMentionText` | MessageMarkdown channelMention text | Inline mention |

### Keys Used Only in Views (Not Styles)

These keys are referenced directly in Root's views — overriding them themes the views themselves:

`BackgroundPrimary`, `BrandSecondary`, `BrandTertiary`, `Error`, `Info`, `ScrollShadow`, `SelfMention`, `TextTertiary`, `TextWhite`, `Warning`

Notably `Error` is used for: failed message border, retry button, notification badge background, and **the message ping/mention left border accent** (see Ping Color section below).

### Non-Root Style Keys (From SimpleTheme)

Only 4 SimpleTheme keys appear in Root's custom styles:
- `ThemeControlHighlightLowBrush` → `RootSplitView.PaneBackground` (sidebar/pane background)
- `ThemeControlLowColor` → `RootSplitView` template
- `ThemeDisabledOpacity` → Slider disabled state (value: 0.5)
- `ThemeBorderThickness` → DropDownButton border

**This means:** To theme the sidebar/pane background, override `ThemeControlHighlightLowBrush` in `Styles[0].Resources` (SimpleTheme), NOT Root's 32 keys.

---

## Theme System Mechanics

### How Root Switches Themes

```csharp
// RootApp.Client.Avalonia.Resources.Themes.ThemeService
public void SetTheme(ThemeVariant variant, bool persist = false) {
    if (persist)
        P_0.SetGlobal(DataStoreKeys.Theme, (int)ThemeMapper.ToRootThemeEnum(variant));
    Application.Current.RequestedThemeVariant = variant;  // This is the entire switch
}
```

Setting `Application.Current.RequestedThemeVariant` triggers Avalonia's native ThemeVariant resolution to select the correct dictionary. All `DynamicResource` bindings automatically re-resolve. No manual resource swapping needed.

### PureDark Inherits From Dark

```csharp
public static readonly ThemeVariant PureDark = new ThemeVariant("PureDark", ThemeVariant.Dark);
```

PureDark uses `ThemeVariant.Dark` as its inherit parent. Any key not explicitly defined in PureDark falls back to the Dark theme dict. This is why PureDark only differs in background/border colors — all other keys are inherited from Dark.

### Theme Dictionary Structure

```
Application.Resources (ResourceDictionary)
├── ThemeDictionaries
│   ├── ThemeVariant.Light  → ResourceDictionary { MergedDictionaries: [Build_Light()] }
│   ├── ThemeVariant.Dark   → ResourceDictionary { MergedDictionaries: [Build_Dark()] }
│   └── ThemeMapper.PureDark → ResourceDictionary { MergedDictionaries: [Build_PureDark()] }
├── MergedDictionaries: [Fonts.axaml, Sounds.axaml]
└── 26 deferred converters (BoolInverterConverter, etc.)
```

Each `Build_*` dictionary contains the 32 color keys + ~220 SVG paths + misc entries.

### ActualThemeVariantChanged Event

Root's own views subscribe to this event to react to theme changes:

```csharp
// In MessageView.Hook():
Application.Current.ActualThemeVariantChanged += onActualThemeVariantChanged;

// Handler:
private void onActualThemeVariantChanged(object? sender, EventArgs e) {
    updateMemberColor();  // Re-apply role colors which use Application.Current.ActualThemeVariant
}
```

**Uprooted should subscribe to this event** to re-apply resource overrides when the user switches themes natively (Dark/Light/PureDark). The correct reflection path:

```csharp
// Subscribe to Application.Current.ActualThemeVariantChanged
var eventInfo = app.GetType().GetEvent("ActualThemeVariantChanged");
// Use AvaloniaReflection.SubscribeEvent or Expression.Lambda
```

### Correct ThemeEngine Override Path

The current ThemeEngine targets FluentTheme/SimpleTheme keys (wrong). Root's controls bind to Root's own 32 keys. The correct approach:

```csharp
// Step 1: Get the theme dictionary for the active variant
var resources = Application.Current.Resources;
var themeDict = resources.ThemeDictionaries[ThemeVariant.Dark]; // or Light/PureDark

// Step 2: Set keys directly on the wrapper dict (direct entries beat MergedDictionaries)
themeDict["BrandPrimary"] = CreateSolidColorBrush("#7328BA");
themeDict["TextPrimary"] = CreateSolidColorBrush("#F2F2F2");
// ... all 32 keys

// Step 3: Subscribe to ActualThemeVariantChanged to re-apply on theme switch
// (when user switches from Dark → Light, re-apply to Light dict)
```

**Why this fixes the known bugs:**
- **Theme switch color inconsistencies:** DynamicResource bindings re-resolve automatically when dict values change. No 500ms timer or tree walk needed for resource-bound controls.
- **Toggle/switch accent color:** CheckBox `.ToggleSwitch` reads `BrandPrimary` and `Muted` from the resource dict. Overriding `BrandPrimary` in the dict means newly-created toggle states automatically pick up the correct color.
- **Settings controls not recoloring instantly:** Same root cause — controls that change visual state (checked→unchecked) create new visuals that resolve resources from the dict, which now has our overridden values.

**What still needs visual tree walk:** Controls that use `Application.Current.FindResource(ActualThemeVariant, key)` imperatively (like MessageView's `updateBackgroundColor()`) or that use `ThemeService.IsDefaultColor` — but these are very few compared to the DynamicResource-bound majority.

---

## Message View Internals

### Control Structure

`MessageView : UserControl, ISelectableMessage` — the single unit for one chat message.

```
MessageView (UserControl)
├── MainView (UserControl)
├── MessageBackgroundBorder (Border)  ← has ContextFlyout = RootMenuFlyout
│   └── MessageBackgroundHighlightBorder (Border)  ← mention/hover overlay
│       └── [message content tree]
│           ├── SystemMessageSvgImage (RootSvgImage)
│           ├── UsernameTextBlock (RootLinkButton)  ← username + role color
│           ├── MessageTextBlock (RootMarkdownTextBlock)  ← message content
│           └── ActionBarBorder (RootBorder)  ← action bar (hover only)
│               ├── AddReactionActionButton (RootSvgButton)
│               ├── ReplyActionButton (RootSvgButton)
│               ├── EditMessageActionButton (RootSvgButton)
│               ├── PinMessageActionButton (RootSvgButton)
│               ├── PinMessageToolTip (TextBlock)
│               ├── MoreOptionsActionButton (RootSvgButton)
│               └── ShiftDeleteActionButton (RootSvgButton)
├── PinnedMessageMenuItem (MenuItem)
└── DeleteMenuItem (MenuItem)
```

### updateBackgroundColor() — How Hover and Mention Highlights Work

```csharp
private void updateBackgroundColor()
{
    // Self-mention: red left border + red highlight background
    if (message.HasSelfMention && !message.HasLocalPendingReply) {
        MessageBackgroundBorder[!BorderBrushProperty] = new DynamicResourceExtension("Error");
        MessageBackgroundHighlightBorder[!BackgroundProperty] = new DynamicResourceExtension("SelfMention");
        return;
    }
    // Pending reply: brand blue left border + brand blue background
    if (message.HasLocalPendingReply) {
        MessageBackgroundBorder.BorderBrush = FindResource(ActualThemeVariant, "BrandPrimary") as IBrush;
        MessageBackgroundHighlightBorder.Background = FindResource(ActualThemeVariant, "BrandPrimary") as IBrush;
        return;
    }
    // Default: transparent
    MessageBackgroundHighlightBorder.Background = Brushes.Transparent;
    // Hover or action bar open: HighlightLight background
    if (IsPointerOver || vm.ActionBarOpen) && !vm.IsInEditMode) {
        MessageBackgroundBorder.Background = FindResource(ActualThemeVariant, "HighlightLight") as IBrush;
        MessageBackgroundBorder.BorderBrush = FindResource(ActualThemeVariant, "HighlightLight") as IBrush;
    } else if (vm.IsInEditMode) {
        // same as hover
    } else {
        MessageBackgroundBorder.Background = Brushes.Transparent;
        MessageBackgroundBorder.BorderBrush = Brushes.Transparent;
    }
}
```

**Key finding for the ping color feature:**
- The mention border (left stripe) uses the **`Error`** key — NOT `SelfMentionBorder`
- The mention background highlight uses the **`SelfMention`** key (40% alpha red wash)
- `Error` = `#F03F36` in all themes — overriding it would break notification badges, failed message indicators, etc.
- To customize the ping highlight color: override **`SelfMention`** (the background) and leave `Error` alone (the border stays red)
- To customize both: create a new approach that avoids `Error` — e.g., patch `updateBackgroundColor()` via reflection or set `MessageBackgroundBorder.BorderBrush` directly via visual tree walk

### Message Properties (from MessageViewModel)

```csharp
// Message.PropertyChanged property names:
"HasSelfMention"          // DynamicResource("Error") + DynamicResource("SelfMention")
"HasLocalPendingReply"    // DynamicResource("BrandPrimary") for both border and highlight
"PinnedAt"                // Updates pinned icon
"SystemMessageLog"        // Updates system message SVG icon
"SenderMember"            // Updates profile picture and badge
```

### Role Color Application (updateMemberColor)

```csharp
if (string.IsNullOrEmpty(role.RoleColorHex))
    UsernameTextBlock[!ForegroundProperty] = DynamicResource("TextPrimary");  // No role color
else if (ThemeService.IsDefaultColor(role.RoleColorHex))
    UsernameTextBlock.Foreground = new SolidColorBrush(  // #000000 on Light, #FFFFFF otherwise
        Color.Parse(ThemeService.GetInvertedDefaultColorHex(role.RoleColorHex)));
else
    UsernameTextBlock.Foreground = new SolidColorBrush(Color.Parse(role.RoleColorHex));
```

`ThemeService.IsDefaultColor`: returns true for `#FFFFFF` and `#000000`.
`ThemeService.GetInvertedDefaultColorHex`: returns `#000000` on Light theme, `#FFFFFF` on all others.

**Re-applied on:** `Application.Current.ActualThemeVariantChanged`

### Message Border Margin

```csharp
MessageBackgroundBorder.Margin = message.ShowUserProfile || message.IsSystemMessage
    ? new Thickness(0, 15, 0, 0)   // First message in group / system message
    : new Thickness(0, 4, 0, 0);    // Continuation message
```

---

## Mention and Markdown System

### Markdown Component Types

Root uses custom Avalonia components for rich text rendering (NOT standard TextBlock):

| Type | Purpose |
|------|---------|
| `CTextBlock` | Primary text renderer with selection support, has `SelectionBrushProperty` |
| `CInline` | Base inline element |
| `CRun` | Plain text run |
| `CHyperlink` | Clickable link, has `CSpan.BorderBackgroundBrushProperty` for pill bg |
| `CCode` | Inline code |
| `CSpan` | Span with border — used for all mention pills |

`CSpan` properties for mention pill styling:
- `BorderBackgroundBrushProperty` — pill background
- `BorderBrushProperty` — pill border
- `BorderThicknessProperty` — pill border thickness (usually 1×1×1×1)
- `CornerRadiusProperty` — pill corner radius (5)
- `PaddingProperty` — pill padding (3×1×3×1)
- `PressedScaleProperty` — press animation scale (0.98)

### Style Selectors for Markdown

Message text containers have either class `RootMarkdown` or `SimpleMessage`:

```
.RootMarkdown CTextBlock           → font=15, TextPrimary, RootFont
.SimpleMessage CTextBlock          → font=14, TextSecondary, FontWeight=450
.RootMarkdown CTextBlock.Heading1  → font=26, Bold
.RootMarkdown CTextBlock.Heading2  → font=22, Medium
.RootMarkdown CTextBlock.Heading3  → font=18, SemiBold
.RootMarkdown CTextBlock.Heading4  → font=15, SemiBold
.RootMarkdown Border.CodeBlock     → bg=BackgroundSecondary, border=Border, radius=8
.RootMarkdown Border.Blockquote    → borderBrush=TextPrimary, left border only (2×0×0×0)
.RootMarkdown CCode.inlineCode     → bg=HighlightNormal, fg=TextPrimary, Consolas font
.RootMarkdown CCode.inlineCode_simple → fg=TextPrimary, italic
.RootMarkdown Grid.List            → margin=0
```

### Mention Style Selectors

All mention pills use `CHyperlink` with a CSS class:

```
.selfMention     → fg=TextPrimary,  bg=SelfMentionBackground,  border=SelfMentionBorder
.otherMention    → fg=BrandPrimary, bg=OtherMentionBackground, border=OtherMentionBorder
.roleMention     → fg=RoleMentionText, bg=RoleMentionBackground, border=RoleMentionBorder
.channelMention  → fg=ChannelMentionText, bg=ChannelMentionBackground, border=ChannelMentionBorder
```

Simple (non-interactive) variants use `CRun` instead of `CHyperlink`:
```
.selfMention_simple    → fg=TextPrimary, Medium weight
.otherMention_simple   → fg=TextPrimary, Medium weight
```

**Important:** These are the resource keys for the **inline mention pills** (text-level). The **message row highlight** (background wash behind the entire message) uses different keys — see [Ping Color section](#ping-color-and-mention-highlighting).

---

## Settings Page Pattern

Root's settings pages follow a consistent pattern (confirmed in `ChatView.cs`, `ChangeThemeView.cs`):

```
RootScrollViewer (outer container)
└── StackPanel (margin=24)
    ├── TextBlock (section header, 20pt, Medium, TextPrimary, RootFont, margin-bottom=28)
    └── RootBorder (setting card)
        ├── BackgroundProperty = DynamicResource("BackgroundSecondary")
        ├── BorderBrushProperty = DynamicResource("Border")
        ├── DynamicBorderThickness = 0.5
        ├── CornerRadius = 12
        ├── Padding = 24
        └── Grid (columns: Star, MinWidth-104/Auto, Auto)
            ├── StackPanel (col 0, title+description)
            │   ├── TextBlock (title: Bold, 14pt, TextPrimary, RootFont)
            │   └── TextBlock (description: FontWeight=450, 14pt, TextSecondary, LineHeight=20)
            └── CheckBox (col 2, Classes="ToggleSwitch", HAlign=Right, VAlign=Center)
                └── IsChecked ← TwoWay binding to ViewModel
```

**Column definition:** `new GridLength(1, Star)` | `new GridLength(0, Auto) { MinWidth=104 }` | `new GridLength(0, Auto)`

### Font Sizes in Settings

```
Section header: 20pt, Medium weight
Card title: 14pt, Bold weight
Card description: 14pt, FontWeight=450, LineHeight=20
```

### ChangeThemeView Pattern

Theme picker uses `RadioButton` with custom template and `ThemeToBoolConverter`:

```csharp
radioButton.IsChecked ← CompiledBinding(ViewModel.Theme)
    .Converter(ThemeToBoolConverter)
    .ConverterParameter(ThemeVariant.Dark)
```

`ThemeToBoolConverter.Convert`: returns `true` if current theme equals parameter.
`ThemeToBoolConverter.ConvertBack`: returns the parameter ThemeVariant if value=true.

State styles:
- Checked: border = `BrandPrimary`, background = `HighlightNormal`
- Unchecked hover: border = `TextSecondary`
- Checked indicator: `TextPrimary` background fill (inner dot)

---

## Ping Color and Mention Highlighting

### The Two-Layer Highlight System

Root uses two separate mechanisms for mention highlighting:

**Layer 1: Message row background** (MessageView code-behind)
- Triggered by `Message.HasSelfMention = true`
- `MessageBackgroundHighlightBorder.Background` ← `DynamicResource("SelfMention")` (40% red wash, `#66FF2D1F`)
- `MessageBackgroundBorder.BorderBrush` ← `DynamicResource("Error")` (solid red, `#F03F36`)
- This is the background stripe behind the entire message row

**Layer 2: Inline mention pills** (MessageMarkdown styles)
- Applies to `@mention` text within message content
- Background ← `SelfMentionBackground` (`#26FF2D1F`, 15% alpha)
- Border ← `SelfMentionBorder` (`#4DFF2D1F`, 30% alpha)

### Custom Ping Color Implementation

The current `ThemeEngine.ApplyPingColorOverride()` approach:

**What it should target:**
- `"SelfMention"` — message row background highlight
- `"SelfMentionBackground"` — inline mention pill background
- `"SelfMentionBorder"` — inline mention pill border

**What it should NOT target:**
- `"Error"` — shares with notification badges, failed message borders, retry button text; do not override this for ping color

**Correct approach via resource override:**

```csharp
// Given customColor = "#FF7328BA" (purple example)
// Derive alpha variants matching Root's convention:
// SelfMention:           base at 40% alpha
// SelfMentionBackground: base at 15% alpha
// SelfMentionBorder:     base at 30% alpha

var baseColor = Color.Parse(customColor);
var themeDict = Application.Current.Resources.ThemeDictionaries[ThemeVariant.Dark];
themeDict["SelfMention"]           = CreateBrush(Color.FromArgb(0x66, baseColor.R, baseColor.G, baseColor.B));
themeDict["SelfMentionBackground"] = CreateBrush(Color.FromArgb(0x26, baseColor.R, baseColor.G, baseColor.B));
themeDict["SelfMentionBorder"]     = CreateBrush(Color.FromArgb(0x4D, baseColor.R, baseColor.G, baseColor.B));
```

Since `updateBackgroundColor()` uses `DynamicResourceExtension("SelfMention")` via the `[!Property] =` setter, overriding the dictionary value will propagate to already-open messages automatically on the next `updateBackgroundColor()` call. However the `DynamicResourceExtension` is set once at mention-detection time, so existing message rows need to have their `updateBackgroundColor()` re-triggered (e.g. by simulating pointer leave/enter or property change).

**The left border (MessageBackgroundBorder.BorderBrush) uses `Error`** which is not customizable without side effects. A future approach could intercept `updateBackgroundColor()` via IL patching, but for now the border stays red regardless of the custom ping color.

---

## App Startup Chain

```csharp
// Program.Main
Thread.SetApartmentState(ApartmentState.STA);
Directory.SetCurrentDirectory(AppContext.BaseDirectory);
ApplyUserProfileEnvironment(args);  // --Application:UserProfile → DOTNET_ENVIRONMENT
VelopackApp.Build().Run();          // Auto-updater check
RootLauncher.Run(args,
    host => new AppCompositionRoot(host),  // DI setup
    compositionRoot => {
        AppBuilder.Configure(compositionRoot.GetRequiredService<App>)
            .UsePlatformDetect()
            .UseReactiveUI()
            .With(new Win32PlatformOptions { OverlayPopups = false })  // WIN32 popups in own windows
            .WithAppNotifications(...)
            .StartWithClassicDesktopLifetime(args);
    });

// App.Initialize()
_0021XamlIlPopulateTrampoline(this);  // Sets up Resources + Styles (XAML compiled)
_themeService.InitializeTheme();       // Sets RequestedThemeVariant from saved setting
DefaultMenuInteractionHandler.MenuShowDelay = TimeSpan.Zero;
```

`Win32PlatformOptions.OverlayPopups = false` — popups (context menus, dropdowns) appear in their own Win32 windows. This is why `OverlayLayer` may not contain all popups.

### App.Initialize Order

1. XAML trampoline runs — registers all Resources (theme dicts, converters) and Styles (27 style files)
2. `ThemeService.InitializeTheme()` — sets `Application.Current.RequestedThemeVariant`
3. `DefaultMenuInteractionHandler.MenuShowDelay = TimeSpan.Zero` — instant menu open
4. `OnFrameworkInitializationCompleted` — creates MainWindow, starts services

**Uprooted timing:** Our CLR profiler hooks `[ModuleInitializer]` which fires before `App.Initialize()`. Phase 0–3 wait for Avalonia assemblies, `Application.Current`, and `MainWindow` respectively. Phase 3.5 applies our theme **after** `_themeService.InitializeTheme()` has set the base theme — so we can safely override.

---

## DataStore Keys

Complete `DataStoreKeys` enum (68 entries):

```csharp
// Layout/Navigation
OpenedTabs, ChannelGroupCollapsed, FriendGroupCollapsed, MembersCollapsed
CommunityChannelsWidth, OpenedSystemTrayPane

// Theme
Theme  // int = RootThemeEnum ordinal (0=Default, 1=Dark, 2=Light, 3=PureDark)

// Input
Keybindings, PushToTalk, PushToTalkKeybind, PushToTalkDelay

// UI Settings
FrequentlyUsedEmojis, AutoConvertEmojis, ZoomLevel, CloseToTray, TapToReply

// Chat
CommunityMemberFilter

// Notifications
DesktopNotifications, TaskbarAttention, NotificationSounds, UseNativeNotifications

// Audio
MediaDevices, ScreenshareAudio, ScreenshareQualityMode, NoiseSuppressionStrength
EchoCancellationEnabled, AutomaticGainControlEnabled, GlobalInputVolume, GlobalOutputVolume
TileVolumeCache

// Game/Chat Overlay (15 keys)
OverlayMasterEnabled, GameOverlayEnabled, GameOverlayPosition, GameOverlayOpacity
GameOverlayScale, GameOverlayShowAvatars, GameOverlayShowNames, GameOverlayOnlyShowSpeaking
GameOverlayCustomX, GameOverlayCustomY, GameOverlayUseCustomPosition
ChatOverlayEnabled, ChatOverlayPosition, ChatOverlayCustomX, ChatOverlayCustomY
ChatOverlayUseCustomPosition

// Analytics (6 keys)
AnalyticsLastCaptureUtc, AnalyticsMessagesSentInCommunities, AnalyticsMessagesSentInDms
AnalyticsCommunitiesCreated, AnalyticsCommunitiesJoined, AnalyticsDmsStarted
AnalyticsVoiceCallsJoined

// Streamer Mode (7 keys)
StreamerModeEnabled, StreamerModeAutoDetect, StreamerModeAutoScreenShare
StreamerModeHidePersonalInfo, StreamerModeHideInviteLinks, StreamerModeDisableNotifications
StreamerModeDisableSounds, StreamerModeHideMessagePreviews

// Window/State
WindowState, PopoutWindowStates

// Developer
DeveloperModeEnabled  // Root's internal developer mode
```

**Access pattern** (from Root's code):
```csharp
// Global setting (not per-user):
_localDataStore.TryGetGlobal(DataStoreKeys.Theme, out int value);   // returns int
_localDataStore.SetGlobal(DataStoreKeys.Theme, (int)themeEnum);

// Per-user setting:
_localDataStore.TryGetWithPath([userId.ToString(), "AutoConvertEmojis"], out int value);
// Default: AutoConvertEmojis=0 (false), TapToReply=1 (true)
```

Settings are stored as **integers** (0/1 for booleans). `Theme` is the RootThemeEnum ordinal. All are accessed via `ILocalDataStore`.

---

*Last updated: 2026-02-19 — sourced from ILSpy decompilation of RootApp.Client.Avalonia v0.9.92.0*
