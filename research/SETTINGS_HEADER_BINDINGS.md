# Settings Header Bindings & Back Arrow Mechanics

> Findings from decompiling `RootSettingsContainer.cs` and `ProfileSettingsViewModel.cs`.
> Source dumps: `research/ilspy-dumps/RootSettingsContainer.cs`, `research/ilspy-dumps/ProfileSettingsViewModel.cs`

## Header Grid Layout (grid9)

The settings header is a 3-column Grid at Row=0, Column=2 of the main settings layout Grid.

```
grid9.ColumnDefinitions:
  [0] Auto    — back button (RootSvgButton)
  [1] Star(1) — title (TextBlock)
  [2] Auto    — close button (RootSvgButton)
```

### Back Button (Column 0)

- Type: `RootSvgButton` with `SvgPath = DynamicResource("DownArrowSVG")`, `RenderTransform = RotateTransform { Angle = 90 }`
- Size: `Width = 40`, `Height = 40`
- Margin: `(24, 0, 0, 0)`
- Border: `BorderBrush = DynamicResource("Border")`, `BorderThickness = (1,1,1,1)`, `CornerRadius = (20,20,20,20)`
- Event: `Button.ClickEvent` → `onBackButtonClicked` (Direct | Bubble)
- **IsVisible binding** (compiled):
  ```
  RootSettingsContainerUserControl.SelectedMenuItemPageContainer
    .Navigator
    .CanGoBack
  ```

### Title TextBlock (Column 1)

- Margin: `(24, 0, 24, 0)`
- FontFamily: `StaticResource("RootFont")`
- FontWeight: `Medium`
- FontSize: `24`
- TextTrimming: `CharacterEllipsis`
- Foreground: `DynamicResource("TextPrimary")`
- HorizontalAlignment: `Left`
- VerticalAlignment: `Center`
- **Text binding** (compiled):
  ```
  RootSettingsContainerUserControl.SelectedMenuItemPageContainer
    .Navigator
    .CurrentViewModel
    → TypeCast<IPage>
    → .PageTitle
  FallbackValue = null
  ```

### Close Button (Column 2)

- Type: `RootSvgButton` with `SvgPath = DynamicResource("DismissSVG")`
- Size: `Width = 40`, `Height = 40`
- Margin: `(0, 0, 24, 0)`
- Event: `Button.ClickEvent` → `onCloseButtonClicked`

## The Back Arrow Glitch (Root Cause)

**Symptom:** When navigating to Uprooted's injected tabs, the back arrow appears in the header alongside the page title.

**Root cause:** Deselecting the ListBox sets `SelectedMenuItemPageContainer = null`. The compiled IsVisible binding on the back button has **no FallbackValue**, so when the binding source is null, Avalonia falls back to the **property default** for `IsVisible`, which is `true`.

This is NOT caused by a "deep navigator stack" (as previously documented). The binding chain breaks at the very first link — `SelectedMenuItemPageContainer` is null because nothing is selected.

```
ListBox.SelectedItem ←→ SelectedMenuItemPageContainer (TwoWay binding)
  SelectedIndex = -1  →  SelectedMenuItemPageContainer = null
    →  .Navigator  →  null dereference
    →  binding fails  →  fallback to IsVisible default = true
    →  back button appears!
```

The title TextBlock has `FallbackValue = null`, so it shows empty text (harmless). But IsVisible has no FallbackValue set, so it uses the Avalonia property default (true).

## SelectedMenuItemPageContainer Binding

```csharp
// RootSettingsContainer.cs, line 735-742
// MainListBox.SelectedItem ↔ SelectedMenuItemPageContainer (TwoWay)
AvaloniaObjectExtensions.Bind(
    listBox,                           // MainListBox
    ListBox.SelectedItemProperty,      // SelectedItem
    new CompiledBindingExtension(      // Source: this.SelectedMenuItemPageContainer
        ...SelectedMenuItemPageContainerProperty...
    ) { Mode = BindingMode.TwoWay }
);
```

When `SelectedIndex = -1`:
- `SelectedItem = null`
- TwoWay propagates → `SelectedMenuItemPageContainer = null`
- All bindings that start from `SelectedMenuItemPageContainer` break
- Each broken binding falls to its own fallback (or property default if none)

## OnLoaded Auto-Select

```csharp
// RootSettingsContainer.cs, line 174-184
private void RootSettingsContainer_OnLoaded(object? sender, RoutedEventArgs e)
{
    // Selects first non-header item in the ListBox
    foreach (var item in MenuItemPageContainers)
    {
        if (!item.IsHeader)  // IsHeader=true items are section labels ("USER SETTINGS", "APP SETTINGS")
        {
            SelectedMenuItemPageContainer = item;  // "User Profile"
            break;
        }
    }
}
```

**Timing implication:** If Uprooted detects and injects before `OnLoaded` fires, `SelectedMenuItemPageContainer` is still null. If we inject after, it's set to "User Profile" (ListBox idx=1).

## Menu Item Structure (from ProfileSettingsViewModel)

```
MenuItemPageContainers (ObservableCollection):
  [0] "User Settings"      (IsHeader=true)
  [1] "User Profile"       (IsHeader=false) ← OnLoaded selects this
  [2] "Privacy"             (IsHeader=false)
  [3] "Blocked Users"       (IsHeader=false)
  [4] "App Settings"        (IsHeader=true)
  [5] "Audio & Video"       (IsHeader=false)
  [6] "Chat"                (IsHeader=false)
  [7] "Notifications"       (IsHeader=false)
  [8] "Theme"               (IsHeader=false)
  [9] "Keybindings"         (IsHeader=false)
  [10] "Streamer Mode"      (IsHeader=false)
  [11] "Windows"            (IsHeader=false)
  [12] "Game Overlay"       (IsHeader=false)
  [13] "Advanced"           (IsHeader=false)
```

Each non-header item has a `Navigator` (navigation stack). `CanGoBack = stack.Count > 1`. Fresh items have stack depth 0 until their `MenuItemSelected` event fires and pushes the first ViewModel.

## Implications for SidebarInjector

### Why `SetIsVisible(backButton, false)` doesn't work
The compiled binding on `IsVisible` re-evaluates whenever `SelectedMenuItemPageContainer` changes. Setting `IsVisible = false` via reflection is immediately overwritten the next time the binding updates (e.g., when ListBox selection changes).

### The collapse pattern
Must use properties that have **no Avalonia binding**:
- `Opacity = 0` (no binding on this property)
- `MaxWidth = 0`, `MaxHeight = 0` (no binding)
- `Width = 0` (inline value, no binding)
- `Margin = (0,0,0,0)` (inline value, no binding)
- `IsHitTestVisible = false` (no binding)

The Auto column in the header Grid measures to 0px when the child has zero desired size and zero margin.

### Restore values (from decompiled source)
- `Width = 40.0`
- `Height = 40.0`
- `Margin = (24, 0, 0, 0)`
- `Opacity = 1.0`
- `MaxWidth = double.PositiveInfinity`
- `MaxHeight = double.PositiveInfinity`
- `IsHitTestVisible = true`

### Selection suppression window
After deselecting the ListBox (to hide Root's content), Root's `OnLoaded` may fire and re-select. The suppression window (`_selectionSuppressedUntilMs`) catches this and re-deselects. Each re-deselection triggers the binding chain again (SelectedMenuItemPageContainer → null → IsVisible defaults to true, title → null). Must re-apply `CollapseBackButton()` + `SetHeaderTitle()` after each suppression re-deselection.
