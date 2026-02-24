# RootToolTip Template Analysis: Notch/Arrow Tooltip

> Decompiled from `RootApp.Client.Avalonia.Controls.RootToolTip` + `ToolTip.axaml` style
> Session: 2026-02-24 — Rootcord ServerBar tooltip notch investigation

## Source Files

- `RootToolTip.cs` (control): extends `ToolTip`, adds zoom support via `PART_ZoomWrapper`
- `Style_ToolTip.cs` (style): targets `RootToolTip`, sets ControlTemplate
- `XamlClosure_51.Build_2` (template builder): in `-AvaloniaResources-GREPONLY.cs` lines 18557-18920
- `PlacementToBoolConverter`: simple Placement→string equality check

## How MemberView Uses It

From `MemberView.cs` lines 190-239:
```csharp
ToolTip.SetPlacement(P_1, PlacementMode.Right);   // tooltip appears to the RIGHT of target
ToolTip.SetVerticalOffset(P_1, 0.0);
ToolTip.SetHorizontalOffset(P_1, 4.0);            // 4px gap
ToolTip.SetShowDelay(P_1, 0);                      // instant

var tip = new RootToolTip();
ToolTip.SetTip(P_1, tip);
ToolTip.SetPlacement(tip, PlacementMode.Right);    // also set on the tooltip itself

// Content: StackPanel > TextBlock (bound to Member.GlobalUser.UserName)
// TextBlock: RootFont, FontWeight=450, FontSize=14, Padding=0, VCenter
```

## Complete Template Visual Tree

```
LayoutTransformControl (Name="PART_ZoomWrapper")
└─ Grid (IsHitTestVisible=false)
   ├─ Panel (Margin=7,7,7,7)                           ← content area with space for notch
   │  ├─ RootBorder                                     ← SHADOW LAYER
   │  │  CornerRadius=8, Bg=BackgroundSecondary
   │  │  Border=Border (0.5px), BoxShadow=PopupBoxShadow
   │  └─ RootBorder                                     ← CONTENT LAYER
   │     CornerRadius=8, Bg=HighlightLight
   │     Padding=12,8,12,8
   │     └─ ContentPresenter
   │        Font=RootFont, Weight=450, Size=14
   │        VCenter, HCenter, HitTestVisible=false
   │
   ├─ RootBorder (Name="PointingLeft")                  ← NOTCH: points left (visible when Placement=Right)
   │  6×6, Margin=4,0,0,0, VCenter, HLeft
   │  Bg=BackgroundSecondary, Border=Border (1,1,0,0)
   │  RenderTransform=RotateTransform(315°)
   │  └─ RootBorder (Bg=HighlightLight)
   │
   ├─ RootBorder (Name="PointingRight")                 ← NOTCH: points right (visible when Placement=Left)
   │  6×6, Margin=0,0,4,0, VCenter, HRight
   │  Bg=BackgroundSecondary, Border=Border (1,1,0,0)
   │  RenderTransform=RotateTransform(135°)
   │  └─ RootBorder (Bg=HighlightLight)
   │
   ├─ RootBorder (Name="PointingUp")                    ← NOTCH: points up (visible when Placement=Bottom)
   │  6×6, Margin=0,4,0,0, VTop, HCenter
   │  Bg=BackgroundSecondary, Border=Border (1,1,0,0)
   │  RenderTransform=RotateTransform(45°)
   │  └─ RootBorder (Bg=HighlightLight)
   │
   └─ RootBorder (Name="PointingDown")                  ← NOTCH: points down (visible when Placement=Top)
       6×6, Margin=0,0,0,4, VBottom, HCenter
       Bg=BackgroundSecondary, Border=Border (1,1,0,0)
       RenderTransform=RotateTransform(225°)
       └─ RootBorder (Bg=HighlightLight)
```

## How the Notch Works

The notch is a **6×6 RootBorder rotated 45°** to form a diamond shape.
Only 2 sides have borders (`Thickness(1,1,0,0)` = top+left) so after rotation only the visible edges have a border line.

**Visibility logic**: `PlacementToBoolConverter` compares `ToolTip.Placement` enum to a string parameter:
- PointingLeft visible when `Placement == Right` (tooltip right of target → notch points back left)
- PointingRight visible when `Placement == Left`
- PointingUp visible when `Placement == Bottom`
- PointingDown visible when `Placement == Top`

**Rotation angles** (degrees):
| Notch       | Angle | Effect                          |
|-------------|-------|---------------------------------|
| PointingLeft  | 315° | Diamond with corner pointing left  |
| PointingRight | 135° | Diamond with corner pointing right |
| PointingUp    | 45°  | Diamond with corner pointing up    |
| PointingDown  | 225° | Diamond with corner pointing down  |

## Theme Resources Used

| Resource Key        | Usage                                      |
|--------------------|--------------------------------------------|
| `BackgroundSecondary` | Main border bg + notch outer bg            |
| `HighlightLight`      | Content area bg + notch inner fill         |
| `Border`              | Border brush (outer border + notch border) |
| `PopupBoxShadow`      | Box shadow on outer border                 |
| `RootFont`            | Content font family                        |

## For Rootcord ServerBar Implementation

The current `ShowIconTooltip()` uses a flat `_r.CreateBorder()` overlay with no notch.
To match Root's native tooltip:

1. **Use a Grid** (not just a Border) as the tooltip container
2. **Add a 6×6 rotated border** as the notch, positioned at VCenter/HLeft with 4px left margin
3. **Use HighlightLight** for the content fill (not CardBg)
4. **Use BackgroundSecondary** for the outer shadow/border layer
5. **7px margin** on the content panel so notch overlaps correctly
6. **Content**: RootFont, 450 weight, 14px, padding 12,8,12,8
7. **For Rootcord**: the tooltip appears to the right of server icons, so the notch should point LEFT (rotate 315°)

### Notch positioning detail:
- The notch sits inside the Grid alongside the content Panel
- It protrudes into the 7px margin area
- Margin=4,0,0,0 means the notch's left edge is 4px from the Grid's left edge
- The content Panel has 7px margin, so there's a 3px overlap where the notch sits against the content area
