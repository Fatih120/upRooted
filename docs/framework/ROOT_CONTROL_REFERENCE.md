# Root Control Reference

> **What this is:** Authoritative reference for Root's custom controls, style classes, resource key usage, message view internals, and DataStore keys — all confirmed from ILSpy decompilation of v0.9.92.
> **Read when:** Building any C# feature that touches Root's visual tree; fixing theme bugs; building message plugins; matching Root's UI aesthetic.
> **Before this:** [ARCHITECTURE.md §Critical Rules](ARCHITECTURE.md#9-critical-rules) — constraints on how to interact with the tree.
> **After this, for:**
> - Theme implementation → [THEME_ENGINE_DEEP_DIVE.md §Resource-First Migration](THEME_ENGINE_DEEP_DIVE.md#resource-first-migration-plan)
> - Avalonia reflection patterns → [AVALONIA_PATTERNS.md](AVALONIA_PATTERNS.md)
> - Current feature WIP state → [hook/SESSION_STATE.md](../../hook/SESSION_STATE.md)
> **Related docs:** [HOOK_REFERENCE](HOOK_REFERENCE.md) | [AVALONIA_PATTERNS](AVALONIA_PATTERNS.md) | [THEME_ENGINE_DEEP_DIVE](THEME_ENGINE_DEEP_DIVE.md) | [ROOT_THEME_SYSTEM_FINDINGS](../../research/ROOT_THEME_SYSTEM_FINDINGS.md)

This document is the authoritative reference for Root's custom controls, style class system, message view structure, and behavioral patterns as discovered by ILSpy decompilation. Use this when building new Uprooted features to match Root's visual language and avoid rediscovering control internals.

---

## Table of Contents

1. [Custom Controls](#custom-controls)
2. [Style Class System](#style-class-system)
3. [Resource Key Reference](#resource-key-reference)
4. [Theme System Mechanics](#theme-system-mechanics)
5. [Main Window and View Stack](#main-window-and-view-stack)
6. [Message View Internals](#message-view-internals)
7. [RootMessageItemsControl](#rootmessageitemscontrol)
8. [Markdown Rendering System](#markdown-rendering-system)
9. [Mention and Markdown System](#mention-and-markdown-system)
10. [Settings Infrastructure](#settings-infrastructure)
11. [Settings Page Pattern](#settings-page-pattern)
12. [Ping Color and Mention Highlighting](#ping-color-and-mention-highlighting)
13. [App Startup Chain](#app-startup-chain)
14. [Data Store and Persistence](#data-store-and-persistence)
15. [Session and Identity](#session-and-identity)
16. [Member Profile Popup](#member-profile-popup)
17. [ViewFactory (VM-to-View Registry)](#viewfactory-vm-to-view-registry)
18. [DataStore Keys](#datastore-keys)

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

`MenuFlyout` subclass (97 lines) — thin wrapper that adds zoom support.

```csharp
public class RootMenuFlyout : MenuFlyout {
    // CreatePresenter: wraps inner presenter in LayoutTransformControl with ScaleTransform
    // OnOpened: adjusts popup offset by -12px H and V, subscribes to ZoomChanged
    // OnClosed: unsubscribes from ZoomChanged
}
```

- Gets `ZoomService` from `Application.Current.ApplicationLifetime.MainWindow.DataContext as MainViewModel`
- **No custom item injection** — items live in the base `MenuFlyout.Items` collection
- Contains `MenuItem` items with classes like `DeleteMenuItem`. `MenuItem` selected state uses `HighlightNormal`.

Context menu used in MessageView:
```csharp
private RootMenuFlyout? _contextMenu;
_contextMenu = MessageBackgroundBorder.ContextFlyout as RootMenuFlyout;
```

**For plugin item injection (e.g. Translate plugin):** Inject `MenuItem` instances into `MenuFlyout.Items`. The flyout has no protection against additional items — no count validation, no sealed collection.

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

## Main Window and View Stack

### MainWindow (317 lines, UI.Main)

`Window` subclass — the top-level application window.

```
MainWindow (title: "Root", default 1200x800, min 850x480)
├── RootZoomContainer ("ZoomContainer")
│   └── DockPanel ("ContentWrapper")
│       ├── ContentControl (TitleBar, Dock.Top)
│       └── DockPanel (bg = BackgroundTertiary)
│           └── ContentControl (DataContext binding)
```

- Custom title bar: `ExtendClientAreaToDecorationsHint = true`, `NoChrome` chrome hints
- Keybinding: tunneling `KeyDown` + `PointerPressed` dispatched to `KeybindingDispatchService`
- Window state persistence: saves position/size/maximized to `DataStoreKeys.WindowState` via `ILocalDataStore`
- Tray: `HideToTray()` saves state, `RestoreFromTray()` restores
- Constructor receives: `MainViewModel` (DataContext), `ILocalDataStore`, `KeybindingDispatchService`

### MainView (259 lines, UI.Main)

`UserControl` with `DataContext = MainViewModel`. Hosts the entire content area.

```csharp
// Content: ItemsControl bound to ViewModels collection
// ItemsPanel = Grid (stacked overlays) — NOT StackPanel
// ItemTemplate = ContentControl
```

- **ViewModels are overlaid in a Grid, not sequentially stacked.** Last VM is on top (z-order).
- Attached flyout: `RootFlyout` for profile popup, bound to `IsPopupOpen` / `MemberProfile`
- Profile popup: `RootBorder(BackgroundSecondary, Border brush, CornerRadius 8, Margin 12)` > `RootScrollViewer` > `ContentControl(MemberProfile)`

### MainViewModel (400 lines, UI.Main)

`ViewModelBase<MainViewModel>, IOverlayStackTracker` — the root of the DataContext chain.

Key DI fields:

| Field | Type | Purpose |
|-------|------|---------|
| `_loginFactory` | factory | Creates LoginViewModel |
| `_homeFactory` | factory | Creates HomeViewModel |
| `_rootService` | `IRootService` | Connection/session management |
| `_memberProfileViewModelFactory` | factory | **DotNetBrowser discovery chain root** |
| `_rootSessionAccessor` | `IRootSessionAccessor` | Session/user info |
| `_focusService` | service | Focus tracking |
| `_activityTrackerService` | service | Activity tracking |
| `_appBadgeService` | service | App badge/notification count |
| `_windowRegistry` | registry | Window management |
| `_overlayStackService` | service | Overlay stack management |

Key properties:
- `ViewModels` (`ObservableCollection<IViewModelBase>`) — active VM stack
- `TitleBarViewModel` — title bar VM
- `ZoomService` — zoom level management
- `IsPopupOpen` (`[ObservableProperty]`) — profile flyout state
- `MemberProfile` (`[ObservableProperty]`) — profile flyout content
- `OverlayCount` — overlay stack depth

Lifecycle:
- `ViewLoadedAsync`: connects via `IRootService`, adds `LoginViewModel` or `HomeViewModel`
- Messaging: `WeakReferenceMessenger` for Push/Pop/Toggle ViewModel stack operations
- Profile flyout: `onShowProfileFlyoutAtMousePositionByUrlMessage` walks logical tree to find `MessageView` > `MessageViewModel` > `Message.MessageContainer.GetMemberAsync(userGuid)`

**Impact on DotNetBrowser discovery:** `_memberProfileViewModelFactory` is the exact field walked in the DotNetBrowser discovery chain (confirmed from SESSION_STATE.md ViewModel chain). This is the injection point for Phase 4.5 browser discovery.

### DirectMessageOpenerService (109 lines, Helpers.Navigation)

Constructor receives: `IRootSessionAccessor`, `CallingServiceFactory`, `BrowserService`, `PrivacyBlockedActionViewModelFactory`

This is the `<directMessageOpenerService>P` field in the DotNetBrowser discovery chain (MainViewModel → _memberProfileViewModelFactory → <directMessageOpenerService>P → <browserService>P → ...).

- `OpenDirectMessageAsync(GlobalUser, text, startCall)` — creates/opens DM, optionally sends message and starts call
- `OpenDirectMessage(DirectMessageGuid, focusMessage, startCall)` — opens existing DM by ID
- Uses `WeakReferenceMessenger` for tab navigation (CheckPopoutFocusMessage, SelectTabMessage, OpenDirectMessagePaneMessage)

**DotNetBrowser chain confirmation**: The `BrowserService P_2` constructor parameter is the `<browserService>P` field that leads to `_engineManager` → `.Engine` → `.Profiles[0].Browsers.__values` → IBrowser.

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

## RootMessageItemsControl

### RootMessageItemsControl (378 lines, Controls)

`ItemsControl` subclass — the message list container. **NOT a VirtualizingStackPanel itself** — it IS an `ItemsControl` with `StyleKeyOverride = typeof(ItemsControl)`. The VirtualizingStackPanel is the `ItemsPanel` inside it.

```csharp
public class RootMessageItemsControl : ItemsControl {
    protected override Type StyleKeyOverride => typeof(ItemsControl);
    // Background = Brushes.Transparent (set in constructor)
}
```

**Child access pattern:**
```csharp
ItemsPanelRoot.Children.OfType<ContentPresenter>().Select(c => c.Child)
```

Children must implement `ISelectableMessage` interface:
```csharp
interface ISelectableMessage {
    DocumentElement? Document { get; }  // For text selection
}
```

**Key finding for MessageLogger:** Items are `ContentPresenter` > `ISelectableMessage`, not bare controls. When walking the message list, unwrap the `ContentPresenter.Child` to get the actual `MessageView`.

Text selection infrastructure:
- Pointer press/move/release handlers for drag selection
- Double-click: word select
- Triple-click: line select
- `GetSelectedText()` builds text from selection across multiple messages
- Attaches/detaches pointer handlers in `OnAttachedToVisualTree` / `OnDetachedFromVisualTree`

Markdown types used in selection (from `DocumentElement` hierarchy):
`DocumentRootElement`, `ListBlockElement`, `ListItemElement`, `CTextBlockElement`, `CTextBlock`, `CInline`, `CHyperlink`, `CSpan`, `CImage`, `CCode`

---

## Markdown Rendering System

Root implements its own rich text rendering pipeline using custom `CInline` / `CTextBlock` types. These are **NOT** standard Avalonia controls -- they are a parallel text layout system built on `StyledElement` (for inlines) and `Control` (for the text block). Understanding this hierarchy is essential for any feature that reads, modifies, or injects content into messages.

### Type Hierarchy

```
CInline (abstract, StyledElement)                    -- 195 lines
+-- CRun                                              -- 75 lines (plain text)
+-- CSpan                                             -- 306 lines (rich container)
|   +-- CHyperlink                                    -- 155 lines (clickable link)
|   +-- CCode                                         -- 13 lines (code span, trivial CSpan subclass)
|   +-- CBold                                         -- (formatting span)
|   +-- CItalic                                       -- (formatting span)
|   +-- CStrikethrough                                -- (formatting span)
+-- CImage                                            -- 174 lines (inline image)
+-- CInlineUIContainer                                -- 44 lines (embedded Control)
+-- CLineBreak                                        -- 22 lines (line break marker)

CTextBlock (Control, ITextPointerHandleable)          -- 988 lines
    Contains: AvaloniaList<CInline> Content
    Renders: List<CGeometry> _metries (measured geometries)
    Layout: List<LineInfo> _lines
```

### CInline (abstract base -- 195 lines)

`StyledElement` subclass (NOT Control -- no visual tree presence of its own). All inline elements derive from this.

Inherited text properties from Avalonia: `Foreground`, `FontFamily`, `FontWeight`, `FontSize`, `FontStyle`, `FontStretch`.

Custom properties: `Background`, `IsUnderline`, `IsStrikethrough`, `TextVerticalAlignment`.

Abstract methods:
- `MeasureOverride(entireWidth, remainWidth)` -- yields `CGeometry` items describing measured layout segments
- `AsString()` -- returns plain text representation

Invalidation chain:
- `RequestMeasure()` walks parent chain: if parent is `CInline`, delegate up; if `CTextBlock`, call `OnMeasureSourceChanged()`; if `Layoutable`, call `InvalidateMeasure()`
- `RequestRender()` follows the same chain for visual invalidation
- `Typeface` property rebuilt from font properties on change

### CRun (plain text -- 75 lines)

Leaf inline: holds a `Text` (string) property.

`MeasureOverride` uses `TextFormatter.Current.FormatLine()` to break text into `TextLineGeometry` segments. First line uses `min(entireWidth, remainWidth)`, subsequent lines use `entireWidth`.

`AsString()` returns `Text`.

### CSpan (rich container -- 306 lines)

Container inline: holds `IEnumerable<CInline> Content` (child inlines). This is the base class for all rich formatting spans (bold, italic, strikethrough, code, hyperlink).

Border support properties: `BorderThickness`, `BorderBrush`, `BorderBackgroundBrush`, `CornerRadius`, `BoxShadow`, `Padding`, `Margin`, `PressedScale`.

`HasBorder` property -- returns true when any border/padding/corner/margin/shadow is set. When bordered, creates an Avalonia `Border` control and wraps child geometries in `DecoratorGeometry`.

`MeasureOverride` iterates children, measures each, wraps in `DecoratorGeometry` if bordered. Line break handling: if content fits on one line, emits a single `DecoratorGeometry`; if multiline, splits into per-line `DecoratorGeometry` chunks.

`AsString()` concatenates all children's `AsString()`.

### CHyperlink (clickable link -- 155 lines)

Extends `CSpan` -- inherits all border/container behavior.

Added properties: `HoverBackground`, `HoverForeground` brushes; `Command` (`Action<string>`), `CommandParameter` (`string`).

`MeasureOverride` calls base, then attaches click/hover/press handlers to each `CGeometry`:
- **OnClick** -- `Command?.Invoke(CommandParameter)` (the URL)
- **OnMouseEnter** -- adds `:pointerover` + `:hover` pseudo-classes, sets cursor to Hand, applies `TemporaryForeground`/`TemporaryBackground` to `TextGeometry`
- **OnMouseLeave** -- removes pseudo-classes, restores cursor, clears temporary brushes
- **OnMousePressed/Released** -- `:pressed` pseudo-class, `PressedScale` render

**For Translate plugin:** Hyperlinks in messages have a `Command` action. Clicking invokes the action with the URL as parameter. To intercept link clicks, wrap or replace the `Command` delegate.

### CCode (code span -- 13 lines)

Trivial `CSpan` subclass: `CCode(IEnumerable<CInline>) : base(content)` -- no overrides.

Visual difference comes entirely from external styling (`BorderBackgroundBrush`, `CornerRadius` set by `Style_MessageMarkdown.cs`).

### CImage (inline image -- 174 lines)

Holds `Task<BitmapWrapper?>` for async image loading, resolves to `IImage?` result.

Properties: `LayoutWidth`/`LayoutHeight` (override), `RelativeWidth` (fraction of line width), `FittingWhenProtrude` (shrink-to-fit, default true), `SaveAspectRatio`.

`MeasureOverride` resolves image from task on first measure, calculates display size with aspect ratio, emits `ImageGeometry`. If image exceeds remaining width: emits `LineBreakMarkGeometry` first, then fits to entire width.

### CTextBlock (markdown text renderer -- 988 lines)

Root's custom rich text block -- **NOT Avalonia's TextBlock**. This is the primary control that renders `CInline` content trees into measured `CGeometry` layout.

**Content model:**
- `Content` property: `AvaloniaList<CInline>` -- the inline tree (CRun, CSpan, CHyperlink, CImage, etc.)
- `Text` property: lazy concatenation of all `CInline.AsString()` -- plain text view of content

**Layout pipeline:**
- `UpdateGeometry()` measures all inlines into `List<CGeometry>` with `(Left, Top, Width, Height)` positions
- `List<LineInfo>` -- each line has its geometries, baseline height, vertical alignment
- Rendering iterates `_metries`, calls `CGeometry.Render(DrawingContext)`; selection rectangles drawn separately
- Text alignment (Left/Center/Right) via edge padding calculation
- DPI awareness: `PixelFloor`/`PixelRound`/`PixelCeil` ensure pixel-perfect rendering

**Pointer interaction:**
- PointerMoved: hit-tests `_metries` by bounds, fires `OnMouseEnter`/`OnMouseLeave` on `CGeometry`
- PointerPressed: left button finds matching geometry, fires `OnMousePressed`, marks as `_pressed`
- PointerReleased: if still over `_pressed` geometry, fires `OnClick`

**Text selection:**
- `Select(TextPointer, TextPointer)` / `Select(int, int)` -- sets begin/end selection range
- `ComplementIntermediate()` -- collects geometries between selection endpoints for full-range highlighting
- `GetSelectedText()` -- extracts text from selected segments, handles atomic (image/hyperlink) vs text segments
- `GetBegin()`/`GetEnd()` -- returns TextPointers at start/end of all content
- `CalcuatePointerFrom(x, y)` -- hit-test to find TextPointer at pixel position (line-then-geometry scan)

**For Translate plugin -- injecting translated text below a message:**
1. Find the `CTextBlock` in the message's visual tree (search by type name `CTextBlock`, NOT Avalonia's `TextBlock`)
2. Read the `Text` property for the plain text content
3. Inject a new visual element (`Border` + `TextBlock`) below the `CTextBlock` in the parent container
4. Do NOT modify `Content` directly -- it would corrupt the markdown inline tree

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

## Settings Infrastructure

### RootSettingsContainer (990 lines, Controls.Settings)

`UserControl` subclass — the main settings page host. This is the top-level container for all Root settings.

```
RootSettingsContainer (named: "RootSettingsContainerUserControl")
├── Grid (4 columns, 3 rows)
│   ├── Column 0: BackgroundTertiary full-height panel (spans all 3 rows)
│   ├── Column 1: Left sidebar
│   │   ├── StackPanel
│   │   │   ├── MainListBox (class "SettingsContainerListBox")
│   │   │   ├── ListFooter (ContentControl)
│   │   │   └── SidePanelFooter (ContentControl)
│   │   └── bg = BackgroundTertiary
│   ├── Separator: 0.5px Rectangle, Fill = DynamicResource("Border")
│   ├── Column 2: Content area (max 900px)
│   │   ├── Row 0: Header (85px)
│   │   │   ├── Back button (RootSvgButton, DownArrowSVG rotated 90°)
│   │   │   ├── Title TextBlock (24px, FontWeight.Medium, TextPrimary)
│   │   │   └── Close button (RootSvgButton, ExitThickSVG, BackgroundSecondary)
│   │   ├── Row 1: Page content
│   │   └── Row 1-2: SaveChangesView overlay
│   └── Column 3: right margin (1*)
```

Grid layout: `1* | Auto (separator) | 20* max 900 | 1*` columns, `85px | 1* | Auto` rows.

StyledProperties:

| Property | Type | Purpose |
|----------|------|---------|
| `MenuWidth` | `double` | Sidebar width |
| `MenuItemPageContainers` | `ObservableCollection<MenuItemPageContainerViewModel>` | Settings categories |
| `SelectedMenuItemPageContainer` | `MenuItemPageContainerViewModel` | Active category |
| `CloseViewModelCommand` | `ICommand` | Close settings panel |
| `WebApiStatus` | object | API status indicator |
| `ListHeader` | `ContentControl` | Sidebar header slot |
| `ListFooter` | `ContentControl` | Sidebar footer slot |
| `SidePanelFooter` | `ContentControl` | Sidebar bottom slot |

Behavior:
- `OnLoaded`: selects first item with `ForceInitialLoad = true`, or first non-header item
- Back: calls `SelectedMenuItemPageContainer.Navigator.Pop()`
- Save/Revert: calls `Navigator.SaveChanges()` / `RevertChanges()` via `[RelayCommand]`
- Page title: bound to `SelectedMenuItemPageContainer.Navigator.CurrentViewModel` cast to `IPage.PageTitle`
- ListBox item styles: `:pointerover` = `HighlightLight` bg, `:selected` = `HighlightNormal` bg
- Header items disabled via `BoolInverter` on `IsHeaderItem`

### SaveChangesView (396 lines, Controls)

The "unsaved changes" bar that appears at bottom of settings pages.

```
SaveChangesView (named: "RootSaveChangesControl")
├── BrandPrimary background bar (height 56px, CornerRadius 12)
│   ├── TextBlock: "You Have Unsaved Changes" (TextWhite, 14px, Bold)
│   ├── RevertChangesButton (.TextButton class, TextWhite, 35px, CornerRadius 16)
│   └── ThemeVariantScope(Light)
│       └── SaveChangesButton (.BorderButton class, TextWhite bg, 32x122px, CornerRadius 16)
│           ├── SaveChangesButtonTextBlock (normal state)
│           └── LoadingSpinner (Lottie, loading state)
```

- Starts with `IsVisible = false` (controlled by parent binding to `Navigator.HasPendingChanges`)
- Loading state: swaps TextBlock for Lottie spinner, disables control
- Save button wrapped in `ThemeVariantScope(Light)` to force light-theme styling

**Impact on SidebarInjector:** The save bar visibility is controlled by `Navigator.HasPendingChanges` binding. The current opacity hack in SidebarInjector may be avoidable by manipulating the `Navigator.HasPendingChanges` property or by targeting the `SaveChangesView` control directly.

### IPage Interface

```csharp
interface IPage {
    string PageTitle { get; }
}
```

Settings ViewModels implement this interface. The page title is displayed in the header row of `RootSettingsContainer` via binding to `Navigator.CurrentViewModel` cast to `IPage`.

### MenuItemPageContainerViewModel (135 lines, Controls.Settings)

Each settings category is represented by one `MenuItemPageContainerViewModel` with its own `Navigator` stack.

| Property | Type | Purpose |
|----------|------|---------|
| `Navigator` | `Navigator` | Page navigation stack for this category |
| `MenuTitle` | `string` | Display name in sidebar |
| `IsHeaderItem` | `bool` | If true, renders as non-clickable section header |
| `ForceInitialLoad` | `bool` | If true, auto-selects on settings open |
| `ShowUpdateIndicator` | `bool` (`[ObservableProperty]`) | Shows update dot on menu item |

- `MenuItemSelected` event (`Action<MenuItemPageContainerViewModel>`) — fired when item is selected
- `SelectMenuItem()` fires the event; called when `SelectedMenuItemPageContainer` changes and Navigator is empty

### Navigator (287 lines, Helpers.Navigation)

`ObservableObject` subclass — manages the settings page navigation stack.

Properties (all [ObservableProperty]):
- `CurrentViewModel` (IViewModelBase?) — the currently displayed VM
- `HasPendingChanges` (bool, default false) — controls SaveChangesView visibility
- `CanSave` (bool, default true) — controls save button enabled state
- `CanGoBack` (bool) — true when stack.Count > 1
- `WebApiStatus` (WebApiStatus) — loading/success/failed
- `PromptToDiscardChanges` (bool) — if true, Pop() prompts instead of discarding
- `FirstViewModel` — bottom of stack
- `Count` — stack depth

Methods:
- `Push(IViewModelBase)` — pushes VM, sets CurrentViewModel, updates CanGoBack
- `Pop()` — if PromptToDiscardChanges && HasPendingChanges, fires DiscardChangesAndGoBackRequested event. Otherwise pops, disposes old VM, sets CurrentViewModel to new peek, updates CanGoBack.
- `SaveChanges()` — fires SaveChangesRequested event
- `RevertChanges()` — fires RevertChangesRequested event

Events: SaveChangesRequested, RevertChangesRequested, DiscardChangesAndGoBackRequested

Internal: `Stack<IViewModelBase>` navigation stack.

**Impact for SidebarInjector**: Setting `HasPendingChanges = false` on the Navigator would hide the save bar via binding, avoiding the current opacity hack. To find the Navigator: `RootSettingsContainer.SelectedMenuItemPageContainer.Navigator`.

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

## Data Store and Persistence

### ILocalDataStore Interface

```csharp
interface ILocalDataStore {
    void Set<T>(ReadOnlySpan<string> path, T value, JsonTypeInfo<T> typeInfo);
    bool TryGet<T>(ReadOnlySpan<string> path, out T value, JsonTypeInfo<T> typeInfo);
}
```

- Path segments create nested JSON objects: `["userId", "theme"]` produces `{"userId": {"theme": value}}`
- Uses `System.Text.Json` source-generated serialization (NOT reflection-based)

### LocalDataStore (271 lines, Domain.Helpers.Store)

File-backed settings persistence.

| Aspect | Detail |
|--------|--------|
| File | `data.json` in `ApplicationDirectory` (profile/default/) |
| Encoding | XOR "obfuscation" with 32-byte static key (**not real encryption**, trivially reversible) |
| Obfuscation key | `[138, 79, 46, 29, 156, 123, 106, 95, 62, 45, 28, 11, ...]` (32 bytes, repeating) |
| Atomic writes | Write to `data.tmp.json`, then `File.Move(temp, real, overwrite: true)` |
| Corruption handling | Quarantines to `invalid-data.json`, starts fresh |
| Migration | Auto-detects plain JSON (first non-whitespace = `{`) and re-saves as obfuscated |
| Thread safety | All reads/writes under `lock(_sync)` |

### LocalDataStoreExtensions

Convenience methods:

```csharp
// Global setting (single-segment path):
SetGlobal(DataStoreKeys key, T value)
TryGetGlobal(DataStoreKeys key, out T value)

// Per-user setting (multi-segment path):
SetWithPath(string[] path, T value)
TryGetWithPath(string[] path, out T value)
```

Source-generated serializer context supports: `int`, `long`, `double`, `string`.

### SecureStorageImplementation (86 lines, Domain.Helpers.Store)

Encrypted credential storage — used for tokens and credentials, NOT for settings.

| Aspect | Detail |
|--------|--------|
| Encryption | Windows DPAPI (`ProtectedData.Protect/Unprotect`) |
| Entropy | HMAC-SHA384 derived from key name using 80-byte static hex key |
| Storage | File-based: one file per key in `Store/` subdirectory |
| Atomic writes | Temp file + rename pattern |

**Security note:** The DPAPI key is tied to the Windows user account. The HMAC entropy key is a static hex string embedded in the binary — provides per-key differentiation but not additional secrecy beyond DPAPI's machine/user binding.

---

## Session and Identity

### IRootSessionAccessor

```csharp
public interface IRootSessionAccessor : INotifyPropertyChanged {
    RootSession? Session { get; }
    void SetSession(RootSession);
    void ClearSession();
}
```

- Held by MainViewModel as `_rootSessionAccessor`
- `RootSessionAccessor` implementation: simple ObservableObject wrapper around `Session` [ObservableProperty]

### RootSession (505 lines)

The core session object — holds ALL service references after login.

Key service properties:

| Property | Type | Purpose |
|----------|------|---------|
| `UserInfoService` | `IUserInfoService` | Current user info, `SessionUser` |
| `UserBlockService` | `IUserBlockService` | Block/unblock users |
| `FriendService` | `IFriendService` | Friend list management |
| `TabService` | `ITabService` | Open tabs tracking |
| `CommunityService` | `ICommunityService` | Community (server) management |
| `FileUploadService` | `IFileUploadService` | File upload |
| `NotificationService` | `INotificationService` | Push notifications |
| `DirectMessageService` | `IDirectMessageService` | DMs |
| `ActiveMediaRoomService` | `IActiveMediaRoomService` | Voice/video calls |
| `SupportService` | `ISupportService` | Support tickets |
| `NotificationCountService` | `AllNotificationCountService` | Badge counts |
| `AssetService` | `IAssetService` | Asset management |
| `LinkService` | `ILinkService` | Link preview/unfurl |

Key identity:
- `UserId` → `UserInfoService.SessionUser.Id` (UserGuid)
- `DeviceId` → connection ClientToken DeviceId (DeviceGuid)

Packet reader: reads `ClientHubPacket` from `IApiConnection.ReadPacketsAsync()`, dispatches by PacketType to the appropriate service handler (community, DM, notification, user status, friend, asset).

Reconnect: epoch-based gate prevents stale reconnect attempts, warms up gRPC with `User.GetSelfAsync()` health check.

**Access path**: `IRootSessionAccessor.Session.UserInfoService.SessionUser.Id` — this is how to get the current user's ID from anywhere with a DI reference.

### IViewModelBase

```csharp
public interface IViewModelBase : INotifyDataErrorInfo, IDisposable {
    bool IsTopMostViewModel { get; set; }
    void ValidateProperty(string propertyName);
}
```

Used by MainViewModel's ViewModels stack. IsTopMostViewModel marks the currently-visible overlay.

---

## Member Profile Popup

### MemberProfileView (1,582 lines, UI.Members)

`UserControl` — the profile popup that appears when clicking a user's avatar or username. 305px wide panel with `ClipToBounds`.

Named parts:
- `MemberContextMenuButton` (RootSvgButton) — top-right "..." context menu
- `OnlineBorder` (Border) — 10x10 online status dot (bottom-right of avatar)
- `UsernameTextBlock` (TextBlock) — username (16px, FontWeight.Medium)
- `NoteTextbox` (RootTextbox) — user notes input
- `MessageTextbox` (RootTextbox) — quick message input (focused on load)

```
Panel (305px)
├── RootSvgButton "MemberContextMenuButton" (top-right, EllipsisVerticalSVG, SvgDimmedButton class)
│   └── UserContextMenuView flyout (Placement=RightEdgeAlignedTop)
└── StackPanel (margin 24,28,24,20)
    ├── Grid (centered) — avatar
    │   ├── Border (60x60, CornerRadius 8, GeometryGroup clip with online cutout)
    │   │   └── RootImageLoader (profile pic, 60x60, UniformToFill, BackgroundSecondary)
    │   └── Border "OnlineBorder" (10x10, CornerRadius 10, bottom-right)
    ├── StackPanel (horizontal, centered, margin-top 12) — identity row
    │   ├── TextBlock "UsernameTextBlock" (16px, Medium, bound to GlobalUser.UserName)
    │   ├── RootSvgImage (OwnerSVG, 15x15, visible if CommunityMember.IsOwner)
    │   └── Border (App badge, BrandPrimary bg, 13px "App" text, visible if IsApp)
    ├── RootBorder (badges, HighlightLight bg, Border brush, CornerRadius 6, padding 4)
    │   │  Visible when: BadgeDisplays.Count > 0 (GreaterThanZeroToTrueConverter)
    │   └── ItemsControl (BadgeDisplays, WrapPanel ItemsPanel with 4px spacing)
    │       └── DataTemplate: RootSvgImage (24x24) with tooltip (36x36 icon + name + description)
    ├── Grid (action buttons, visible if !IsSelf && !IsApp via AndConverter MultiBinding)
    │   └── ItemsControl (WrapPanel, centered) — Send Message, Add Friend, Call buttons
    ├── Roles area (ItemsControl bound to Roles, WrapPanel)
    ├── RootTextbox "NoteTextbox" (notes)
    └── RootTextbox "MessageTextbox" (message, Enter sends via SendMessageCommand)
```

**Online status colors:**

| Status | Color Key | Avatar Cutout |
|--------|-----------|---------------|
| OnlineAndAttached / Active | `BrandSecondary` | No cutout |
| Online | `BrandSecondary` | Yes (EllipseGeometry hole) |
| AwayAndAttached / Inactive | `Warning` | No cutout |
| Away | `Warning` | Yes |
| Offline / Disconnected | `Muted` | No cutout |

**Username coloring:** Uses `PrimaryRole.RoleColorHex`. If null/empty, uses `DynamicResource("TextPrimary")`. If default color, uses `ThemeService.GetInvertedDefaultColorHex()`. Otherwise uses `Color.Parse(hex)`.

**For ProfileBadgeInjector:** The `UsernameTextBlock` is a named part -- find it directly. Badges are in the `ItemsControl` inside the `RootBorder` immediately below the username row StackPanel. Two injection approaches:
1. **VM approach**: Get `MemberProfileViewModel.BadgeDisplays` (`ObservableCollection<MemberBadgeDisplay>`) from DataContext and add a custom badge
2. **Visual tree approach**: Walk from `UsernameTextBlock` parent (StackPanel) to find the badges `RootBorder` > `ItemsControl` and inject a visual element

### MemberProfileViewModel (408 lines, UI.Members)

`ViewModelBase` subclass -- the DataContext for MemberProfileView.

Key fields: `_closeProfileCallback` (Action), `_bitmapCache`, `_directMessageOpenerService`, `_rootSessionAccessor`, `_streamerModeService`, `_developerModeService`

Properties:

| Property | Type | Notes |
|----------|------|-------|
| `BadgeDisplays` | `ObservableCollection<MemberBadgeDisplay>` | Loaded from `BadgeSvgMapper.TryGetBadgeInfo(badge.Id)` |
| `FriendRequestSent` | bool `[ObservableProperty]` | Friend request state |
| `Note` | string `[ObservableProperty]` | User note text |
| `ShouldHideNotes` | bool `[ObservableProperty]` | Notes visibility |
| `MessageContainerMember` | `IMessageContainerMember` | DM container reference |
| `CommunityMember` | `Member?` | Community member data (roles, owner) |
| `IsSelf` | bool | `_rootSessionAccessor.Session.UserInfoService.SessionUser.Id == GlobalUser.Id` |
| `ShowContextMenuButton` | bool | `(!IsSelf && !IsApp) \|\| (IsSelf && DeveloperModeEnabled)` |
| `Roles` | `ReadOnlyObservableCollection<IViewModelBase>` | DynamicData reactive pipeline from `Member.Roles` |
| `ProfilePictureAsyncBitmapWrapper` | async bitmap | `_bitmapCache.GetBitmapAsync(GlobalUser.ProfilePictureUri, null, 120)` |
| `DeveloperModeEnabled` | bool | `_developerModeService.IsEnabled` |

Commands: `SendMessage` (opens DM), `SendFriendRequest` (async), `Call` (opens DM with call), `CloseProfileCallback`, `Unloaded` (saves note)

**Impact on DotNetBrowser discovery:** `_directMessageOpenerService` is the same field in the DotNetBrowser discovery chain (see [Main Window and View Stack](#main-window-and-view-stack) > MainViewModel).

---

## ViewFactory (VM-to-View Registry)

### ViewFactory (1,725 lines, static class)

Single method: `CreateView(IViewModelBase) -> Control` -- a massive type-switch (`is` chain) mapping **237 ViewModel types** to their View counterparts.

Pattern:
```csharp
if (P_0 is FooViewModel dataContext) return new FooView { DataContext = dataContext };
```

This is Root's entire VM-to-View registry. Used by `ViewLocator.Build(object data)` which calls `ViewFactory.CreateView()` -- Avalonia's data template resolution for `ContentControl` bindings.

**Key mappings for Uprooted:**

| ViewModel | View |
|-----------|------|
| `MainViewModel` | `MainView` |
| `HomeViewModel` | `HomeView` |
| `MemberProfileViewModel` | `MemberProfileView` |
| `CommunityTabViewModel` | `CommunityTabView` |
| `DirectMessageTabViewModel` | `DirectMessageTabView` |
| `MessageViewModel` | `MessageView` |
| `LoginViewModel` | `LoginView` |
| `ChangeThemeViewModel` | `ChangeThemeView` |
| `ChatViewModel` | `ChatView` |
| `MembersViewModel` | `MembersView` |

**237 total mappings** -- every ViewModel in Root has an entry. New VMs added to the app require a corresponding entry here. The method is pure pattern-matching with no reflection or dictionary lookup.

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

**Canonical for:** Root custom control types, style class system (buttons/toggles/tabs), resource key per-control mapping, MainWindow/MainView/MainViewModel chain, DirectMessageOpenerService (DotNetBrowser chain), message view named controls + `updateBackgroundColor()` logic, RootMessageItemsControl internals, markdown rendering system (CInline/CRun/CSpan/CHyperlink/CCode/CImage/CTextBlock), mention/markdown system, settings infrastructure (RootSettingsContainer/SaveChangesView/Navigator), settings page layout pattern, data store persistence (LocalDataStore/SecureStorage), session and identity (IRootSessionAccessor/RootSession/IViewModelBase), member profile popup (MemberProfileView/MemberProfileViewModel, badge injection, online status), ViewFactory VM-to-View registry (237 mappings), DataStore keys, App startup chain
**Supersedes (for control detail):** ROOT_INTERNALS.md §3 DotNetBrowser | ROOT_INTERNALS.md §6 Theme System (use ROOT_THEME_SYSTEM_FINDINGS.md for hex values)
**For implementation patterns:** [AVALONIA_PATTERNS.md](AVALONIA_PATTERNS.md) | [HOOK_REFERENCE.md](HOOK_REFERENCE.md)
*Last updated: 2026-02-19 — sourced from ILSpy decompilation of RootApp.Client.Avalonia v0.9.92.0*
