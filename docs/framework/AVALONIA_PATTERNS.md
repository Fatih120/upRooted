# Avalonia Patterns

> **What this is:** Avalonia reflection patterns and pitfalls — property system, visual tree traversal, threading/DispatcherPriority, control creation, Expression.Lambda events, WindowImpl internals.
> **Read when:** Writing C# code that creates or modifies Avalonia controls; debugging UI thread issues; understanding why reflection patterns are needed.
> **Skip if:** You need the system architecture overview → [ARCHITECTURE.md](ARCHITECTURE.md). You need per-class implementation detail → [HOOK_REFERENCE.md](HOOK_REFERENCE.md).
> **Does NOT cover:** Architecture overview → [ARCHITECTURE.md](ARCHITECTURE.md) | Root's custom control types (detailed reference) → [ROOT_CONTROL_REFERENCE.md](ROOT_CONTROL_REFERENCE.md) | Theme algorithm → [THEME_ENGINE_DEEP_DIVE.md](THEME_ENGINE_DEEP_DIVE.md)

> **Related docs:** [Hook Reference](HOOK_REFERENCE.md) | [Architecture](ARCHITECTURE.md) | [Theme Engine Deep Dive](THEME_ENGINE_DEEP_DIVE.md) | [.NET Runtime](DOTNET_RUNTIME.md) | [Root Control Reference](ROOT_CONTROL_REFERENCE.md)

---

## Table of Contents

1. [Overview](#overview)
2. [Property System](#property-system)
3. [Visual Tree](#visual-tree)
4. [Styling System](#styling-system)
5. [Threading Model](#threading-model)
6. [Control Hierarchy](#control-hierarchy)
7. [Root Custom Control Types](#root-custom-control-types)
8. [Layout System](#layout-system)
9. [Advanced Patterns](#advanced-patterns)
10. [Common Reflection Patterns](#common-reflection-patterns)
11. [Pitfalls](#pitfalls)

---

## Overview

Uprooted injects into Root Communications' desktop application, which is built on
.NET 10 with Avalonia UI. Root ships as a trimmed single-file binary, so Uprooted
cannot reference Avalonia assemblies at compile time. Every interaction with the UI
framework happens through runtime reflection.

This document explains Avalonia concepts through Uprooted's reflection-only lens.
If you are contributing to the C# hook layer, you need to understand both the
Avalonia concept and the reflection pattern used to reach it.

**Why this matters:**

- Controls are instantiated via `Activator.CreateInstance` on runtime-discovered types.
- Properties are set through cached `PropertyInfo` or `MethodInfo` handles.
- Events are wired via `Expression.Lambda` because delegate types are unknown at compile time.
- Mistakes produce silent failures, cryptic exceptions, or UI freezes.

The central class is `AvaloniaReflection` (`hook/AvaloniaReflection.cs`, ~2030
lines). It resolves roughly 50 Avalonia types and 55 member handles during startup,
then exposes them through typed wrapper methods.

---

## Property System

Avalonia has its own property system, similar to WPF's dependency properties. Some
properties cannot be accessed through normal CLR setters in Root's trimmed binary.

### Property Types

- **StyledProperty** -- Supports styling, binding, inheritance. Most common.
  Examples: `Control.MarginProperty`, `TextBlock.FontSizeProperty`.
- **DirectProperty** -- Wraps a CLR backing field. No styling. Faster.
  Example: `TextBox.TextProperty`.
- **AttachedProperty** -- Defined by one type, set on another via static methods.
  Examples: `Grid.ColumnProperty`, `Canvas.LeftProperty`.

All derive from `AvaloniaProperty`.

### Pattern 1: CLR PropertyInfo (When Available)

Most properties retain their CLR wrappers. Uprooted caches `PropertyInfo` handles
at startup (`hook/AvaloniaReflection.cs:310-314`):

```csharp
_textBlockText = TextBlockType?.GetProperty("Text", pub);
_textBlockFontSize = TextBlockType?.GetProperty("FontSize", pub);
```

Then uses `PropertyInfo.SetValue` (`hook/AvaloniaReflection.cs:654-656`):

```csharp
var tb = Activator.CreateInstance(TextBlockType);
_textBlockText?.SetValue(tb, text);
_textBlockFontSize?.SetValue(tb, fontSize);
```

### Pattern 2: Avalonia SetValue (When CLR Is Trimmed)

TextBox has trimmed CLR setters. Uprooted resolves the static `AvaloniaProperty`
fields as `FieldInfo` (`hook/AvaloniaReflection.cs:343-346`):

```csharp
_textBoxTextProperty = TextBoxType?.GetField("TextProperty", staticPub);
_textBoxWatermarkProperty = TextBoxType?.GetField("WatermarkProperty", staticPub);
```

Then invokes `SetValue(AvaloniaProperty, object, BindingPriority)` via the
`SetAvaloniaProperty` helper (`hook/AvaloniaReflection.cs:770-803`):

```csharp
var avProp = avaloniaPropertyField.GetValue(null);   // Static field -> AvaloniaProperty
_setValueWithPriority.Invoke(control, new[] { avProp, value, _bindingPriorityStyle });
```

### Pattern 3: Attached Properties via Static Methods

Grid.Column and Grid.Row are accessed through static methods
(`hook/AvaloniaReflection.cs:334-337, 1081-1094`):

```csharp
_gridSetColumn = GridType?.GetMethod("SetColumn", stat);
// Usage:
_gridSetColumn?.Invoke(null, new[] { control, (object)column });
```

### ClearValue

Removes a local value override so bindings or styles can reassert. Essential for
theme revert (`hook/AvaloniaReflection.cs:1178-1218`):

```csharp
var field = control.GetType().GetField(propertyFieldName,
    BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
var avaloniaProperty = field.GetValue(null);
// Find and invoke ClearValue(AvaloniaProperty) non-generic overload
```

---

## Visual Tree

Avalonia uses a visual tree to represent the rendered UI hierarchy. Uprooted
traverses this tree to find controls, inject new ones, and modify existing ones.

### Visual vs. Logical Children

- **Visual tree** -- The actual rendered hierarchy, including template internals
  (borders, content presenters, scrollbar tracks).
- **Logical tree** -- The developer-authored hierarchy. Popup controls appear as
  logical children but may not share the visual tree with the owning window.

Uprooted primarily uses the visual tree, since it needs template-generated elements.

### GetVisualChildren

Accessed via `VisualExtensions.GetVisualChildren(Visual)`, a static extension
(`hook/AvaloniaReflection.cs:294-295, 606-621`):

```csharp
_getVisualChildren = VisualExtensionsType?.GetMethods(stat)
    .FirstOrDefault(m => m.Name == "GetVisualChildren" && m.GetParameters().Length == 1);

// Usage: invoke as static, enumerate IEnumerable result
result = _getVisualChildren.Invoke(null, new[] { visual });
```

### Depth-First Traversal

`VisualTreeWalker` provides stack-based DFS (`hook/VisualTreeWalker.cs:35-49`):

```csharp
public IEnumerable<object> DescendantsDepthFirst(object root)
{
    var stack = new Stack<object>();
    foreach (var child in _r.GetVisualChildren(root))
        stack.Push(child);
    while (stack.Count > 0)
    {
        var node = stack.Pop();
        yield return node;
        foreach (var child in _r.GetVisualChildren(node))
            stack.Push(child);
    }
}
```

Used for: finding "APP SETTINGS" text, locating ListBox/NavContainer/content area,
searching by text or type, and checking for already-injected controls via tags.

### Settings Layout Discovery

`FindSettingsLayout` (`hook/VisualTreeWalker.cs:79-181`) demonstrates a complete
discovery workflow using text anchors and upward walks:

1. Find "APP SETTINGS" TextBlock via DFS text search
2. Walk up to find the NavContainer StackPanel (identified by having 8+ children)
3. Walk further up to find the Grid containing both sidebar and content columns
4. Within those containers, locate ListBox, version box, save bar, back button

The NavContainer heuristic (`hook/VisualTreeWalker.cs:374-381`):

```csharp
if (typeName == "StackPanel" && childCount >= 8)
    return current;  // This is the nav container
```

### Parent Navigation

`GetParent` tries `VisualParent` first, then `Parent`
(`hook/AvaloniaReflection.cs:1398-1409`):

```csharp
var vpProp = type.GetProperty("VisualParent");
if (vpProp != null) return vpProp.GetValue(node);
return type.GetProperty("Parent")?.GetValue(node);
```

---

## Styling System

Uprooted's ThemeEngine manipulates Avalonia's styling system to apply color themes
across the entire native UI.

### Resource Dictionaries

Key-value stores for reusable values (colors, brushes). They exist at multiple levels:

- `Application.Resources` -- App-wide (includes `ThemeDictionaries` for per-variant resources)
- `Application.Resources.ThemeDictionaries[variant]` -- Per-theme-variant (Root's 32 color keys live here)
- `Application.Styles[n].Resources` -- Per-style (SimpleTheme/MediaFluentTheme keys)
- `Control.Resources` -- Per-control

Accessing style resources (`hook/AvaloniaReflection.cs:1588-1616`):

```csharp
var stylesProp = app.GetType().GetProperty("Styles");
// Enumerate to Styles[0], get its Resources property
```

### MergedDictionaries

Uprooted injects a custom `ResourceDictionary` into `Application.Resources.MergedDictionaries`
(`hook/AvaloniaReflection.cs:1642-1696`):

```csharp
var dict = Activator.CreateInstance(ResourceDictionaryType);
// Add entries via IDictionary indexer reflection:
var indexer = dict.GetType().GetProperty("Item", ...);
indexer.SetValue(dict, value, new object[] { key });
// Merge into app resources:
GetMergedDictionaries(appResources)?.Add(dict);
```

> **Research Update (2026-02-19):** Root's actual theme colors (`BrandPrimary`, `TextPrimary`,
> `BackgroundPrimary`, etc.) live in `Application.Resources.ThemeDictionaries[variant]` — not
> in `Styles[0].Resources`. Root uses `SimpleTheme` + `MediaFluentTheme` (NOT standard
> `FluentTheme`), so keys like `ThemeAccentColor` and `SystemAccentColor` are not referenced
> by Root's views. The current ThemeEngine writes to both `Styles[0].Resources` and
> `MergedDictionaries` as a compatibility layer; a migration to target `ThemeDictionaries`
> directly is planned. See [`research/ROOT_THEME_SYSTEM_FINDINGS.md`](../../research/ROOT_THEME_SYSTEM_FINDINGS.md)
> for the correct 32-key catalog.

### BindingPriority

Determines which value wins when multiple sources set the same property. From
highest to lowest: Animation, LocalValue, StyleTrigger, Style, Template.

The ThemeEngine uses Style priority (lower than LocalValue) so hover/pressed
triggers can temporarily override themed values
(`hook/AvaloniaReflection.cs:1266-1307`):

```csharp
_setValueWithPriority.Invoke(control,
    new[] { avaloniaProperty, value, _bindingPriorityStyle });
```

`BindingPriority` is resolved once by scanning assemblies for
`Avalonia.Data.BindingPriority` (`hook/AvaloniaReflection.cs:807-820`).

---

## Threading Model

Avalonia uses a single-threaded UI model. All visual tree modifications must happen
on the UI thread.

### Dispatcher

Accessed via `Dispatcher.UIThread` static property. The `Post` method is resolved
with three fallbacks (`hook/AvaloniaReflection.cs:262-289`):

1. `Post(Action, DispatcherPriority)` -- 2-param (Avalonia 11+)
2. `Post(Action)` -- 1-param
3. `InvokeAsync(Action)` -- older API

### DispatcherPriority Is a Struct

**CRITICAL**: In Avalonia 11+, `DispatcherPriority` is a **struct** with static
properties (`Normal`, `Background`, `Render`), not an enum. `RunOnUIThread`
handles this with a four-level fallback (`hook/AvaloniaReflection.cs:553-604`):

```csharp
var priorityType = _dispatcherPost.GetParameters()[1].ParameterType;

// 1. Try static property: DispatcherPriority.Normal
var normalProp = priorityType.GetProperty("Normal", BindingFlags.Public | BindingFlags.Static);
// 2. Try static field
// 3. Try Enum.Parse (if it IS an enum in some version)
// 4. Last resort: Activator.CreateInstance for default struct value
```

### Threading in SidebarInjector

Primary detection uses `LayoutUpdated` on the main window — fires directly on
the UI thread during the same render frame, with zero dispatch latency. A 200ms
`System.Threading.Timer` serves as a safety net (alive checks, edge cases),
marshaling to the UI thread via `RunOnUIThread`. Both paths share an
`Interlocked.CompareExchange` guard to prevent re-entrant injection:

```csharp
// LayoutUpdated path (already on UI thread — no dispatch needed):
if (Interlocked.CompareExchange(ref _injecting, 1, 0) != 0) return;
try { CheckAndInject(); }
finally { Interlocked.Exchange(ref _injecting, 0); }

// Timer path (threadpool → UI thread dispatch):
_r.RunOnUIThread(() => {
    try { CheckAndInject(); }
    finally { Interlocked.Exchange(ref _injecting, 0); }
});
```

---

## Control Hierarchy

### Key Types

| Type | Purpose in Uprooted |
|------|---------------------|
| `Control` | Base type: Tag, IsVisible, Margin, Cursor |
| `Panel` | Container base with Children collection |
| `StackPanel` | Vertical/horizontal layout with Spacing -- most common container |
| `Grid` | Row/column layout -- 2-column plugin cards, sidebar structure |
| `Border` | Single-child decorator: Background, CornerRadius, BorderThickness |
| `TextBlock` | Text display: FontSize, FontWeight, Foreground, TextWrapping |
| `TextBox` | Text input: Watermark, MaxLength (CLR setters trimmed). **Not the compose input** (see below) |
| `ScrollViewer` | Scrollable wrapper for NavContainer and content pages |
| `ContentControl` | Root's content area (never modify Content directly) |
| `Canvas` | Absolute positioning via Left/Top attached properties |
| `Window` | Top-level: Clipboard access, OverlayLayer |

### Creating Controls

All use `Activator.CreateInstance` on cached types. Examples:

**TextBlock** (`hook/AvaloniaReflection.cs:650-667`):
```csharp
var tb = Activator.CreateInstance(TextBlockType);
_textBlockText?.SetValue(tb, text);
_textBlockFontSize?.SetValue(tb, fontSize);
```

**StackPanel** (`hook/AvaloniaReflection.cs:669-685`):
```csharp
var sp = Activator.CreateInstance(StackPanelType);
var orientation = Enum.Parse(OrientationType, vertical ? "Vertical" : "Horizontal");
_stackPanelOrientation?.SetValue(sp, orientation);
```

**Border** (`hook/AvaloniaReflection.cs:687-709`):
```csharp
var border = Activator.CreateInstance(BorderType);
var cr = Activator.CreateInstance(CornerRadiusType, cornerRadius);
_borderCornerRadius?.SetValue(border, cr);
```

### Type Checking

Since everything is `object`, use `IsAssignableFrom`
(`hook/AvaloniaReflection.cs:1133-1137`):

```csharp
public bool IsTextBlock(object? obj)
    => obj != null && TextBlockType?.IsAssignableFrom(obj.GetType()) == true;
public bool IsPanel(object? obj)
    => obj != null && PanelType?.IsAssignableFrom(obj.GetType()) == true;
```

### Third-Party Controls: AvaloniaEdit

Root's message compose area uses `AvaloniaEdit.TextEditor`, a third-party code editor
library — NOT `Avalonia.Controls.TextBox`. This is significant because:

- `AvaloniaReflection.TextBoxType` does NOT match (0 TextBoxes in the chat visual tree)
- AvaloniaEdit types are NOT trimmed (third-party libraries survive Root's single-file
  trimming), so CLR `PropertyInfo.GetValue`/`SetValue` work normally
- The visual tree path is: `RootMessageTextboxView` > `TextEditor` > `TextArea` >
  `TextView` > `TextLayer`

| AvaloniaEdit Type | Purpose |
|-------------------|---------|
| `AvaloniaEdit.TextEditor` | Top-level control, has `Text` CLR property for reading/writing content |
| `AvaloniaEdit.Editing.TextArea` | Actual editing surface, receives keyboard input |
| `AvaloniaEdit.Rendering.TextView` | Text rendering layer |

To interact with compose input, resolve these types from loaded assemblies (NOT via
`AvaloniaReflection`):

```csharp
foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
{
    if (asm.GetName().Name?.StartsWith("AvaloniaEdit") == true)
        foreach (var t in asm.GetTypes())
        {
            if (t.FullName == "AvaloniaEdit.TextEditor") textEditorType = t;
            if (t.FullName == "AvaloniaEdit.Editing.TextArea") textAreaType = t;
        }
}
```

See [Hook Reference: Compose Input Interception](HOOK_REFERENCE.md#clearurlsengine-compose-input-interception)
for the full Enter key interception technique.

---

## Root Custom Control Types

Root defines custom controls that appear in the visual tree. When walking the tree, you'll encounter these type names. See [Root Control Reference](ROOT_CONTROL_REFERENCE.md) for full details.

| Type | Base | Key Properties | Notes |
|------|------|---------------|-------|
| `RootBorder` | `Border` | `DynamicBorderThicknessProperty` | `StyleKeyOverride=typeof(Border)` — uses Border styles |
| `RootSvgImage` | (control) | `SvgPathProperty` | SVG renderer, path from DynamicResource key |
| `RootSvgButton` | (button) | `SvgOpacityProperty`, `SvgBorderOpacityProperty` | Icon button, multiple CSS class variants |
| `RootSvgCheckBox` | `CheckBox` | `SvgOpacityProperty`, `SvgBorderOpacityProperty` | Checkbox with SVG checkmark |
| `RootScrollViewer` | `ScrollViewer` | — | Transparent background scroll container |
| `RootScrollBarThumb` | (thumb) | — | 4px wide, TextPrimary color at low opacity |
| `RootMarkdownTextBlock` | (control) | `Document` property | Markdown renderer — **not a TextBlock** |
| `RootLinkButton` | (button) | `ForegroundProperty` | Username link in MessageView |
| `RootMenuFlyout` | (menu) | — | Context menu flyout |
| `RootMessageScrollViewer` | `ScrollViewer` | — | Message list scroll container |
| `RootSplitView` | `SplitView` | — | PaneBackground=ThemeControlHighlightLowBrush (SimpleTheme) |
| `RootPercentageSlider` | `Slider` | — | FG=BrandPrimary, BG=Border |
| `CTextBlock` | (control) | `SelectionBrushProperty`, `FontSizeProperty`, `ForegroundProperty` | Markdown text |
| `CInline`, `CRun`, `CHyperlink`, `CSpan`, `CCode` | — | border/bg/fg props | Markdown inline elements |

### Imperative DynamicResource Binding

Root's code-behind uses a special Avalonia syntax to set a DynamicResource binding imperatively:

```csharp
// Root's updateBackgroundColor() pattern:
MessageBackgroundBorder[!TemplatedControl.BorderBrushProperty] = new DynamicResourceExtension("Error");
MessageBackgroundHighlightBorder[!TemplatedControl.BackgroundProperty] = new DynamicResourceExtension("SelfMention");

// Equivalent to XAML:
// <Border BorderBrush="{DynamicResource Error}" />
```

This creates a live DynamicResource binding — if the resource changes (e.g., theme switch or our ThemeEngine override), the property auto-updates.

To replicate this in Uprooted via reflection:
```csharp
// Find DynamicResourceExtension type from Avalonia.Markup.Xaml assembly
// Call ProvideValue() on it with the correct IServiceProvider context
// Or use AvaloniaObjectExtensions.Bind() with the binding
```

Alternatively, use direct `SetValue` for one-time application (not reactive to resource changes).

### ActualThemeVariantChanged

Root's own views subscribe to `Application.Current.ActualThemeVariantChanged` to re-apply code-behind colors when the user switches themes natively:

```csharp
// In MessageView.Hook():
Application.Current.ActualThemeVariantChanged += onActualThemeVariantChanged;
// Handler re-applies member role colors

// In Uprooted, subscribe to re-apply ThemeEngine overrides after a native theme switch:
var eventInfo = app.GetType().GetEvent("ActualThemeVariantChanged");
// Use SubscribeEvent with an Action<object, EventArgs>
```

Note: `RequestedThemeVariant` changes trigger `ActualThemeVariantChanged`. When Root's user picks a different theme via the settings panel, our ThemeEngine needs to re-apply to the new variant's dictionary.

---

## Layout System

Avalonia uses a two-pass layout (Measure then Arrange). Uprooted does not call
these directly -- it sets layout properties and lets Avalonia handle the passes.

### Margin and Padding

Both are `Thickness` structs (`hook/AvaloniaReflection.cs:879-893`):

```csharp
var thickness = Activator.CreateInstance(ThicknessType, left, top, right, bottom);
_controlMargin?.SetValue(control, thickness);       // Margin: cached PropertyInfo
control.GetType().GetProperty("Padding")?.SetValue(  // Padding: runtime search
    control, thickness);                              // (lives on different base classes)
```

### Alignment

Enums parsed from strings (`hook/AvaloniaReflection.cs:981-1001`):

```csharp
var val = Enum.Parse(HorizontalAlignmentType, alignment);  // "Left", "Center", "Right", "Stretch"
control.GetType().GetProperty("HorizontalAlignment")?.SetValue(control, val);
```

### Width, Height, and Bounds

Explicit sizing (`hook/AvaloniaReflection.cs:1543-1553`):

```csharp
control.GetType().GetProperty("Width")?.SetValue(control, width);
```

Reading computed Bounds after layout (`hook/AvaloniaReflection.cs:1451-1470`):

```csharp
var bounds = _layoutableBounds.GetValue(control);  // Rect struct
var x = (double)(bounds.GetType().GetProperty("X")?.GetValue(bounds) ?? 0.0);
// ... same for Y, Width, Height
```

### Grid Layout

Column definitions use GridLength + GridUnitType
(`hook/AvaloniaReflection.cs:1107-1129`):

```csharp
var starUnit = Enum.Parse(GridUnitTypeEnum, "Star");
var gridLength = Activator.CreateInstance(GridLengthType, starWidth, starUnit);
var colDef = Activator.CreateInstance(ColumnDefinitionType);
ColumnDefinitionType.GetProperty("Width")?.SetValue(colDef, gridLength);
```

Used in ContentPages for 2-column plugin card rows
(`hook/ContentPages.cs:476-494`):

```csharp
var rowGrid = r.CreateGrid();
r.AddGridColumn(rowGrid, 1.0);
r.AddGridColumn(rowGrid, 1.0);
r.SetGridColumn(visible[i], 0);
r.SetGridColumn(visible[i + 1], 1);
```

### Panel Children

`Panel.Children` returns `IList`. All manipulation goes through it
(`hook/AvaloniaReflection.cs:1043-1074`):

```csharp
public IList? GetChildren(object? panel) => _panelChildren?.GetValue(panel) as IList;
public void AddChild(object? panel, object? child) => GetChildren(panel)?.Add(child);
public void RemoveChild(object? panel, object? child) => GetChildren(panel)?.Remove(child);
```

---

## Advanced Patterns

### WindowImpl.s_instances for Window Discovery

Avalonia popups live in separate Window/PopupRoot objects outside the main window's
tree. The public `Windows` list does not include popup roots.

Uprooted accesses the internal `WindowImpl.s_instances` static field, then reads
each instance's `Owner` property to get the `TopLevel`
(`hook/AvaloniaReflection.cs:524-551`):

```csharp
_windowImplSInstances = type.GetField("s_instances",
    BindingFlags.NonPublic | BindingFlags.Static);
_windowImplOwner = type.GetProperty("Owner",
    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
```

This is an internal API. Fallback: `GetMainWindow()`.

### TranslatePoint for Coordinate Mapping

Maps coordinates between controls for overlay positioning. Handles both instance
(Avalonia 11+) and static extension forms
(`hook/AvaloniaReflection.cs:1477-1520`):

```csharp
var point = Activator.CreateInstance(PointType, x, y);
if (_translatePoint.IsStatic)
    result = _translatePoint.Invoke(null, new[] { from, point, to });
else
    result = _translatePoint.Invoke(from, new[] { point, to });
// Result is Nullable<Point> -- unwrap via HasValue/Value reflection
```

Resolution uses three fallback strategies: type hierarchy walk, VisualExtensions
check, then brute-force scan of all Avalonia types
(`hook/AvaloniaReflection.cs:362-403`).

### Expression.Lambda for Event Subscription

Avalonia routed events use custom delegate types unknown at compile time. Uprooted
builds correctly-typed delegates via expression trees
(`hook/AvaloniaReflection.cs:1141-1170`):

```csharp
var handlerType = eventInfo.EventHandlerType;
var paramTypes = handlerType.GetMethod("Invoke").GetParameters()
    .Select(p => p.ParameterType).ToArray();
var p0 = Expression.Parameter(paramTypes[0], "sender");
var p1 = Expression.Parameter(paramTypes[1], "e");
var lambda = Expression.Lambda(handlerType,
    Expression.Invoke(Expression.Constant(callback)), p0, p1);
eventInfo.AddEventHandler(control, lambda.Compile());
```

For pointer events needing coordinates, `SubscribePointerEvent`
(`hook/AvaloniaReflection.cs:1846-1901`) builds an expression that calls
`e.GetPosition(sender)` and extracts `X`/`Y`.

### OverlayLayer for Popups

A Canvas above the normal visual tree. Used for filter dropdowns and info lightboxes
(`hook/AvaloniaReflection.cs:1417-1446`):

```csharp
var overlay = _overlayGetOverlayLayer.Invoke(null, new[] { mainWindow });
_canvasSetLeft?.Invoke(null, new[] { control, (object)left });
_canvasSetTop?.Invoke(null, new[] { control, (object)top });
GetChildren(overlay)?.Add(child);
```

Typical workflow (see `hook/ContentPages.cs:665-767`):
1. Get OverlayLayer from main window
2. Create transparent backdrop for click-to-dismiss
3. Position dropdown using TranslatePoint + GetBounds
4. Add backdrop + panel to overlay
5. On dismiss, remove both

### Application.Current

Entry point for the running application. Chain: `Application.Current` ->
`ApplicationLifetime` -> `MainWindow` (`hook/AvaloniaReflection.cs:430-438`):

```csharp
public object? GetAppCurrent() => _appCurrent?.GetValue(null);
public object? GetMainWindow() {
    var app = GetAppCurrent();
    var lifetime = _appLifetime?.GetValue(app);
    return _lifetimeMainWindow?.GetValue(lifetime);
}
```

---

## Common Reflection Patterns

Quick reference for patterns that appear throughout the codebase.

### Type Lookup

Scan loaded assemblies, never use `Type.GetType`
(`hook/AvaloniaReflection.cs:148-166`):

```csharp
foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
{
    if (!asm.GetName().Name?.StartsWith("Avalonia") == true) continue;
    foreach (var type in asm.GetTypes())
        typeMap[type.FullName] = type;
}
Type? Find(string fullName) => typeMap.TryGetValue(fullName, out var t) ? t : null;
```

### Property Access

**Cached** (frequent use): resolve once at startup, call `SetValue`/`GetValue` many times.
**Runtime** (rare use): `control.GetType().GetProperty("Height")?.SetValue(control, 36.0)`.

### Struct/Enum Creation

**Enums**: `Enum.Parse(OrientationType, "Vertical")`
**Structs**: `Activator.CreateInstance(ThicknessType, left, top, right, bottom)`

### Brush Creation

Root's trimmed binary strips the `SolidColorBrush(Color)` constructor. Use
parameterless ctor + Color setter (`hook/AvaloniaReflection.cs:861-875`):

```csharp
var color = _colorParse.Invoke(null, new object[] { hex });
var brush = Activator.CreateInstance(SolidColorBrushType);
SolidColorBrushType.GetProperty("Color")?.SetValue(brush, color);
```

### Control Tagging

`Control.Tag` marks injected controls for identification and cleanup:

| Tag | Purpose |
|-----|---------|
| `uprooted-injected` | Sidebar container (duplicate injection guard) |
| `uprooted-nav-{page}` | Individual nav items |
| `uprooted-highlight-{page}` | Nav highlight borders |
| `uprooted-content` | Content pages |
| `uprooted-no-recolor` | Excluded from theme engine tree walks |

---

## Pitfalls

> These pitfalls implement the [Critical Rules from ARCHITECTURE.md](ARCHITECTURE.md#9-critical-rules). Code examples below show the reflection-level detail.

Real bugs encountered during development. Each is a rule in `ARCHITECTURE.md`.

### Never Use Type.GetType() for Avalonia Types

Returns null in Root's single-file binary. Assembly-qualified names do not resolve
when assemblies are bundled. Use `AvaloniaReflection`'s cached types instead.

### Never Modify ContentControl.Content Directly

Setting `Content` triggers a detach walk on the old content. If that tree was
modified by injection, Avalonia crashes or freezes. Instead, hide Root's children
with `SetIsVisible(false)` and add content alongside them
(`hook/SidebarInjector.cs:545-559`).

### Never Use System.Text.Json in Hook

The CLR profiler injection context causes `MissingMethodException`. The JSON
serializer relies on code generation unavailable at profiler load time. Use
INI-based settings (`UprootedSettings.cs`).

### Never Use EventInfo.AddEventHandler with Wrong Delegate Type

Avalonia routed events need `EventHandler<PointerPressedEventArgs>` etc., not
plain `EventHandler`. Use `Expression.Lambda` to build the exact delegate type.

### DispatcherPriority Is a Struct, Not an Enum

In Avalonia 11+, `Enum.Parse` fails on `DispatcherPriority`. Try static property
access first, then field, then enum parse, then default struct value
(`hook/AvaloniaReflection.cs:570-598`).

### Brush Constructors May Be Trimmed

`SolidColorBrush(Color)` is stripped. Use parameterless constructor + `Color`
property setter.

### CornerRadius Requires Explicit Construction

For non-uniform corners, use the four-parameter constructor
(`hook/ContentPages.cs:1615-1616`):

```csharp
var cr = Activator.CreateInstance(r.CornerRadiusType, 5.0, 5.0, 0.0, 0.0);
```

### ScrollBarVisibility Requires Dynamic Enum Parse

The enum lives in `Avalonia.Controls.Primitives`, not a pre-cached type. Resolve
via `assembly.GetType()` (`hook/SidebarInjector.cs:658-661`).

### AddHandler Requires All Routing Strategies for AvaloniaEdit Enter Key

AvaloniaEdit marks Enter (`Key.Return`) as `Handled=true` internally. This means:
- CLR `EventInfo.AddEventHandler` does NOT receive Enter (fires for modifier keys only)
- `AddHandler` with `RoutingStrategies.Bubble` alone does NOT receive Enter
- You MUST use all three strategies combined (`Bubble | Tunnel | Direct = 7`) with
  `handledEventsToo=true`

```csharp
// Will NOT work:
addHandler.Invoke(target, new[] { routedEvent, handler, bubble, (object)true });

// WILL work:
var all = Enum.ToObject(routingType, 4 | 2 | 1); // Bubble | Tunnel | Direct
addHandler.Invoke(target, new[] { routedEvent, handler, all, (object)true });
```

Hook `TextArea` (not `TextEditor`) — it's the actual input surface and receives events
first. See [Hook Reference: Compose Input Interception](HOOK_REFERENCE.md#clearurlsengine-compose-input-interception).

### Color.ToString() Returns Named Colors

Avalonia's `Color.ToString()` can return named color strings like `"White"` instead of `"#FFFFFFFF"`. This breaks `ColorUtils.ParseHex()` which expects `#RRGGBB`/`#AARRGGBB`. Always extract color values via the struct's `A`, `R`, `G`, `B` byte properties:

```csharp
var ct = color.GetType();
var a = ct.GetProperty("A")?.GetValue(color) as byte?;
var r = ct.GetProperty("R")?.GetValue(color) as byte?;
var g = ct.GetProperty("G")?.GetValue(color) as byte?;
var b = ct.GetProperty("B")?.GetValue(color) as byte?;
if (a.HasValue && r.HasValue && g.HasValue && b.HasValue)
    hex = $"#{a.Value:X2}{r.Value:X2}{g.Value:X2}{b.Value:X2}";
```

### ResourceDictionary Indexer Doesn't Resolve MergedDictionaries

`dict["key"]` only returns direct entries. Root's 32 color keys are added via `AddDeferred` to variant wrapper dicts — the indexer may return the deferred wrapper, not the resolved brush. Use `Application.TryGetResource(key, ThemeVariant, out value)` for full resolution (MergedDictionaries + deferred factories + variant matching). This is the same path `DynamicResource` uses at runtime.

### SVG Resources Are Direct Dict Entries

Root's ~220 SVG path resources (keys ending in `SVG`) are added directly to ThemeDictionary variant wrappers via `IDictionary.Add`, NOT via `AddDeferred` like color brushes. They're plain `string` values. To enumerate them, iterate the variant wrapper dict itself, not `MergedDictionaries[0]`.

### Transparent Backgrounds Break Pointer Hit-Testing

A control with `Background="#00000000"` (fully transparent) does NOT receive pointer events. Child controls inside it will get events, but the transparent parent won't — causing `PointerEntered`/`PointerExited` gaps when the mouse moves between children. Fix: set `IsHitTestVisible=false` on overlay elements so events pass through to the parent.

### Light Theme Text Colors Are Solid (Not Alpha Variants)

Root's Dark theme uses alpha variants for text hierarchy: `TextSecondary=#A3F2F2F2` (64% alpha), `TextTertiary=#66F2F2F2` (40% alpha). Root's Light theme uses **solid opaque colors**: `TextSecondary=#282828`, `TextTertiary=#5E5E5E`. Never derive Light text colors by applying alpha to `TextPrimary` — always read the actual `TextSecondary`/`TextTertiary` resources.

### Never Use localStorage in TypeScript

Root runs Chromium with `--incognito`. `localStorage` is not persisted.

---

## UI Standards for Injected Controls

Patterns established for Uprooted's injected Avalonia controls (ContentPages cards, SidebarInjector nav items). These match Root's native settings UI conventions.

### Card Border Highlights

All cards use **constant border thickness** (`1.5px`) — never change thickness on hover/selection (causes layout shift as cards grow/shrink by a few pixels).

- **Resting**: 15% contrast from `CardBg` via `AdjustForHighlight(CardBg, 15)`
- **Hover (theme cards)**: Card border brightens to 35% contrast. Radio indicator border brightens to 50% contrast. Inner radio dot fills with 40% contrast highlight. No background change.
- **Hover (plugin cards)**: No highlight — plugin cards are not clickable as a whole. Only the toggle/button inside is interactive.
- **Selected**: Accent color border on the card + accent-filled inner radio dot.

```csharp
// Luminance-aware highlight: darkens on light bg, lightens on dark bg
private static string AdjustForHighlight(string bg, double percent)
    => ColorUtils.Luminance(bg) > 0.5
        ? ColorUtils.Darken(bg, percent)
        : ColorUtils.Lighten(bg, percent);
```

### Sidebar Nav Item Highlights

Read highlight colors from Root's live ThemeDictionaries resources — these automatically adapt to Dark/Light/PureDark:

- **Hover**: `HighlightLight` — white at 4% on Dark (`#0AFFFFFF`), black at 4% on Light (`#0A000000`)
- **Selected**: `HighlightNormal` — white at 10% on Dark (`#19FFFFFF`), black at 10% on Light (`#19000000`)

### Theme Selection Interaction

- Use `PointerReleased` (not `PointerPressed`) for theme activation — matches Root's native theme selector
- Radio inner dot shows highlight on hover as a preview of selection
- Preview swatch has `IsHitTestVisible=false` so pointer events pass through to the card

### ContentPages Color Derivation

Colors come from `Application.TryGetResource` reading Root's live theme keys. This works for all variants and when Uprooted themes are active:

| Static Field | Root Resource Key | Dark Value | Light Value |
|---|---|---|---|
| `TextWhite` | `TextPrimary` | `#FFF2F2F2` | `#FF131313` |
| `TextMuted` | `TextSecondary` | `#A3F2F2F2` | `#FF282828` |
| `TextDim` | `TextTertiary` | `#66F2F2F2` | `#FF5E5E5E` |
| `CardBg` | `BackgroundSecondary` | `#FF121A26` | `#FFFFFFFF` |
| `CardBorder` | `Border` | `#FF242C36` | `#FFDBDBDB` |
| `AccentGreen` | `BrandPrimary` | `#FF3B6AF8` | `#FF3B6AF8` |

Fallback: if `ReadLiveRootColors()` returns null, hardcoded Dark defaults are used.

### Variant Change Re-injection

When Root switches theme variant (Dark↔Light↔PureDark), the SidebarInjector must:

1. **Unwrap ScrollViewer** (restores NavContainer to grid — critical for layout detection)
2. **Remove injected controls** from nav panel
3. **Remove content page** and **version text**
4. **Null all state** (`_injected = false`)
5. Do NOT try to restore Root's controls (SignOut, VersionBorder) — Root's own `DynamicResource` bindings handle them

Next `LayoutUpdated` pass detects `_injected=false`, runs `CheckAndInject` → `InjectIntoSettings` with fresh native colors from the new variant.

### ImmutableSolidColorBrush(16777215u) Means Transparent

`0x00FFFFFF` = fully transparent (alpha=0, white RGB). Root uses this pattern pervasively to "clear" backgrounds and borders:

```csharp
setter.Value = new ImmutableSolidColorBrush(16777215u);  // = #00FFFFFF = transparent
```

You'll see this in ListBox, ListBoxItem, TransparentButton, RootScrollViewer, MenuBorderButton, etc. It is **not** white — it's invisible. When you see this in a dump, the control intentionally has no background/border for that state.

### ListBoxItem Has Fully Transparent Selection Styles

**All** ListBoxItem states — including `:selected`, `:selected:focus`, `:selected:pointerover` — set `ContentPresenter.Background = #00FFFFFF` (transparent). Root's sidebar navigation does NOT use Avalonia's built-in selection highlighting from the ListBoxItem style system.

**Implication:** To find selection highlights in nav items, look inside the item's own data template, not the ListBoxItem template. The selection visual (e.g., left border highlight, background wash) is custom-built into each nav item template.

### RootSplitView Pane Background Is Not in Root's 32 Keys

`RootSplitView.PaneBackgroundProperty` resolves from `ThemeControlHighlightLowBrush` — a SimpleTheme key, NOT one of Root's 32 custom keys. To theme the sidebar/pane background color, override this key in `Styles[0].Resources` (SimpleTheme), not in the ThemeDictionaries.

### Win32 Popups Appear in Separate Win32 Windows

`AppBuilder.With(new Win32PlatformOptions { OverlayPopups = false })` means context menus, dropdowns, and flyouts open in their own Win32 windows. They are NOT in the main window's OverlayLayer. To find/inject into a popup, use `WindowImpl.s_instances` (see AvaloniaReflection) rather than traversing the main visual tree.

### ThemeEngine Currently Targets Wrong Keys

The existing ThemeEngine overrides FluentTheme/SimpleTheme keys (`SystemAccentColor`, `TextFillColorPrimary`, etc.) which Root's controls do NOT reference. Root's views exclusively bind to Root's own 32 keys (`BrandPrimary`, `TextPrimary`, `BackgroundPrimary`, etc.) via `DynamicResourceExtension`. This is why some controls remain unthemed after theme application and why toggling a switch mid-theme shows the wrong accent color — the style-level brush still comes from the unmodified root 32 keys. The correct fix is to override Root's 32 keys in `Application.Resources.ThemeDictionaries[variant]`. See [Root Control Reference: Theme System Mechanics](ROOT_CONTROL_REFERENCE.md#theme-system-mechanics) and [Root Theme System Findings](../../research/ROOT_THEME_SYSTEM_FINDINGS.md).

---

**Canonical for:** Avalonia reflection patterns, property system (StyledProperty/DirectProperty/AttachedProperty), visual tree traversal, threading/DispatcherPriority, control creation via reflection, Expression.Lambda event subscription, WindowImpl.s_instances, TranslatePoint, OverlayLayer, Root custom control types summary, AvaloniaEdit integration, 14 pitfall solutions with code
**Not canonical for:** critical rules (text) → [ARCHITECTURE.md](ARCHITECTURE.md#9-critical-rules) | Root control exhaustive reference → [ROOT_CONTROL_REFERENCE.md](ROOT_CONTROL_REFERENCE.md) | theme algorithm → [THEME_ENGINE_DEEP_DIVE.md](THEME_ENGINE_DEEP_DIVE.md)
*Avalonia patterns reference for Uprooted v0.4.2. Last updated 2026-02-19.*
