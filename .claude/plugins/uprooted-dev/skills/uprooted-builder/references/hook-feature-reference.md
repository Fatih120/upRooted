# C# Hook Feature Building Reference

Complete reference for building features in the Uprooted C# hook layer (`hook/`). All Avalonia access is through reflection via `AvaloniaReflection` — no direct type references.

## AvaloniaReflection Control Creation API

All methods return `object?` (the Avalonia control instance) and accept nullable parameters gracefully.

### Creating Controls

```csharp
// Text display
object? CreateTextBlock(string text, double fontSize = 14, string? foregroundHex = null, string? fontWeight = null)

// Layout containers
object? CreateStackPanel(bool vertical = true, double spacing = 0)
object? CreateGrid()
object? CreatePanel()
object? CreateCanvas()
object? CreateScrollViewer(object? content = null)

// Visual containers
object? CreateBorder(string? bgHex = null, double cornerRadius = 0, object? child = null)

// Input controls
object? CreateTextBox(string? watermark = null, string? text = null, int maxLength = 0)

// Shapes
object? CreateEllipse(double width, double height, string? fillHex = null)

// Brushes & resources
object? CreateBrush(string hex)  // SolidColorBrush from "#AARRGGBB" or "#RRGGBB"
object? CreateLinearGradientBrush(double startX, double startY, double endX, double endY,
    params (string hex, double offset)[] stops)
object? CreateResourceDictionary()
```

### Property Manipulation

```csharp
// Grid positioning
void SetGridColumn(object? control, int column)
void SetGridRow(object? control, int row)
void AddGridColumn(object? grid, double starWidth = 1.0)

// Layout
void SetMargin(object? control, double left, double top, double right, double bottom)
void SetPadding(object? control, double left, double top, double right, double bottom)
void SetWidth(object? control, double width)
void SetHeight(object? control, double height)
void SetMaxHeight(object? control, double maxHeight)
void ClearMaxHeight(object? control)
void SetHorizontalAlignment(object? control, string alignment)  // "Left"|"Center"|"Right"|"Stretch"
void SetVerticalAlignment(object? control, string alignment)    // "Top"|"Center"|"Bottom"|"Stretch"
void SetClipToBounds(object? control, bool clip)

// Appearance
void SetBackground(object? control, string? hex)
void SetBackgroundBrush(object? control, object? brush)
void SetForeground(object? control, string hex)
void SetFontWeight(object? control, string weight)       // "Normal"|"Bold"|"SemiBold"|"Medium"|"Light"
void SetFontWeightNumeric(object? control, int weight)   // 100-900
void SetFontFamily(object? control, object? fontFamily)
void SetTextWrapping(object? control, string wrapping)   // "Wrap"|"NoWrap"|"WrapWithOverflow"
void SetOpacity(object? control, double opacity)
void SetCursorHand(object? control)

// Identification
void SetTag(object? control, string tag)
string? GetTag(object? control)
void SetIsVisible(object? control, bool visible)
bool GetIsVisible(object? control)
void SetIsHitTestVisible(object? control, bool visible)

// Children
void AddChild(object? panel, object? child)
void RemoveChild(object? panel, object? child)
int GetChildCount(object? panel)
object? GetChild(object? panel, int index)
void SetBorderChild(object? border, object? child)
object? GetBorderChild(object? border)
void SetScrollViewerContent(object? sv, object? content)
void SetContent(object? contentControl, object? content)
object? GetContent(object? contentControl)

// Canvas positioning
void SetCanvasPosition(object? control, double left, double top)

// Text
string? GetText(object? control)
string? GetTextBoxText(object? textBox)
void SetTextBoxText(object? textBox, string text)

// Selection (ListBox)
int GetSelectedIndex(object? listBox)
void SetSelectedIndex(object? listBox, int index)

// Tree navigation
object? GetParent(object? node)
object? GetFontWeight(object? control)
object? GetForeground(object? control)
object? GetFontFamily(object? control)
```

### Avalonia Property System

```csharp
// Set value at Style priority (preserves hover/pressed triggers)
bool SetValueStylePriority(object control, string propertyFieldName, object? value)

// Clear a property value (reverts to default/inherited)
bool ClearValue(object? control, string propertyFieldName)
bool ClearValueSilent(object? control, string propertyFieldName)  // No exceptions

// Property field name convention: "PropertyNameProperty"
// e.g., "BackgroundProperty", "ForegroundProperty", "BorderBrushProperty"
// Helper: AvaloniaReflection.PropertyToFieldName("Background") => "BackgroundProperty"
```

### Event Subscription

```csharp
// Generic event (CLR event, not RoutedEvent)
void SubscribeEvent(object control, string eventName, Action callback)

// Pointer events with coordinates (uses Expression.Lambda internally)
void SubscribePointerEvent(object control, string eventName, Action<double, double> callback)
// eventName: "PointerPressed", "PointerReleased", "PointerMoved", "PointerEntered", "PointerExited"
```

**Critical:** Never use `EventInfo.AddEventHandler` for Avalonia RoutedEvents. The `SubscribeEvent` and `SubscribePointerEvent` methods use the correct Expression.Lambda pattern internally.

### UI Thread & Resources

```csharp
// Dispatch to Avalonia UI thread
void RunOnUIThread(Action action)

// Application resources
object? GetAppResources()
object? GetStyleResources(int styleIndex)
object? GetResource(object? dict, string key)
void AddResource(object? dict, string key, object? value)
bool RemoveResource(object? dict, string key)

// Top-level windows
object? GetMainWindow()
object? GetAppCurrent()
IEnumerable<object> GetAllTopLevels()  // All open windows
```

## Adding a New Settings Page

### Step 1: Create the Build Method in ContentPages.cs

```csharp
private static object? BuildMyFeaturePage(AvaloniaReflection r, UprootedSettings settings,
    object? font, ThemeEngine? themeEngine = null)
{
    ApplyThemedColors(themeEngine);

    var page = r.CreateStackPanel(vertical: true, spacing: 0);
    if (page == null) return null;
    r.SetMargin(page, 24, 24, 24, 0);
    r.SetTag(page, "uprooted-content");

    // Page title
    var title = r.CreateTextBlock("My Feature", fontSize: 20, foregroundHex: TextWhite, fontWeight: "SemiBold");
    ApplyFont(r, title, font);
    r.AddChild(page, title);

    // Spacing
    var spacer = r.CreateBorder();
    r.SetHeight(spacer, 16);
    r.AddChild(page, spacer);

    // Card
    var card = BuildCard(r, font, themeEngine);
    // ... add content to card ...
    r.AddChild(page, card);

    // Wrap in ScrollViewer
    var sv = r.CreateScrollViewer(page);
    return sv;
}
```

### Step 2: Register in BuildPage Switch

```csharp
public static object? BuildPage(string pageName, AvaloniaReflection r, ...)
{
    var page = pageName switch
    {
        "uprooted" => BuildUprootedPage(r, settings, nativeFontFamily, themeEngine),
        "plugins" => BuildPluginsPage(r, settings, nativeFontFamily, themeEngine),
        "themes" => BuildThemesPage(r, settings, nativeFontFamily, themeEngine, onThemeChanged),
        "myfeature" => BuildMyFeaturePage(r, settings, nativeFontFamily, themeEngine),  // ADD THIS
        _ => null
    };
    // ...
}
```

### Step 3: Add Nav Item in SidebarInjector.cs

Follow the existing pattern for adding sidebar navigation items. Nav items use the `uprooted-item-{pagename}` tag convention and trigger page builds when clicked.

## Styling Constants

Match Root's native settings page styling:

```
Card background:       #0f1923 (themed via ContentPages.CardBg)
Card corner radius:    12
Card border:           #19ffffff, thickness 0.5
Card inner padding:    24px
Page margin:           24, 24, 24, 0

Section header:        FontSize=12, Medium(500), #66f2f2f2
Field labels:          FontSize=13, Weight=450, #a3f2f2f2
Body text:             FontSize=13, Normal, #a3f2f2f2
Page title:            FontSize=20, SemiBold(600), #fff2f2f2
Font family:           CircularXX TT (from native controls)
```

Colors are themed dynamically. Use `ContentPages.CardBg`, `ContentPages.TextWhite`, `ContentPages.TextMuted`, `ContentPages.TextDim`, `ContentPages.AccentGreen` instead of hardcoded values.

## Tag-Based Identification

All injected controls **must** use the `uprooted-` prefix for tags:

```csharp
r.SetTag(control, "uprooted-injected");       // Section headers
r.SetTag(navItem, "uprooted-item-plugins");    // Nav items (uprooted-item-{page})
r.SetTag(page, "uprooted-content");            // Content pages
r.SetTag(preview, "uprooted-no-recolor");      // Exempt from theme recoloring
```

This enables cleanup: `r.GetTag(control)?.StartsWith("uprooted-") == true`

## UI Thread Dispatch

```csharp
// Heavy work on background thread
var thread = new Thread(() => {
    // ... compute ...
    r.RunOnUIThread(() => {
        // Mutate UI here — must be on UI thread
        r.AddChild(panel, newControl);
    });
}) { IsBackground = true, Name = "Uprooted-MyFeature" };
thread.Start();

// Timer-based polling (dispatches to UI thread)
var timer = new Timer(_ => r.RunOnUIThread(() => {
    // Check state, update UI
}), null, 0, 200);  // 200ms interval
```

## Critical Rules

These cause real bugs — never violate:

1. **Never use `Type.GetType()` for Avalonia types** — use `AvaloniaReflection`. Single-file .NET apps can't resolve assembly-qualified names.
2. **Never modify `ContentControl.Content` directly** — causes UI freeze. Use the Grid overlay pattern (add as sibling with opaque background).
3. **Never use `System.Text.Json`** — `MissingMethodException` in profiler context. Use manual string parsing or `UprootedSettings` INI format.
4. **Never use `EventInfo.AddEventHandler` for RoutedEvents** — silently fails. Use `SubscribeEvent` or `SubscribePointerEvent` which use Expression.Lambda.
5. **`DispatcherPriority` is a struct not enum** in Avalonia 11+ — `Enum.Parse` fails. `AvaloniaReflection.Resolve()` handles this with a fallback chain.
6. **Never block the UI thread** — all heavy work on background threads, UI mutations via `RunOnUIThread()`. Cross-thread Avalonia access crashes.
7. **Never add children to `VirtualizingStackPanel`** — items get recycled by the virtualizer. Add to the parent container instead.
8. **Never cross-contaminate repos** — private (`uprooted-private`) and public (`uprooted`) repos are strictly separate. Never copy code between them.

## Additional Pitfalls

- **Creating Avalonia controls on a background thread** — always dispatch via `r.RunOnUIThread(action)`. Cross-thread control creation crashes.
- **Ignoring null from AvaloniaReflection methods** — every method can return `null`. Types may not exist in future Avalonia versions. Always null-check.
- **Confusing the two settings systems** — C# uses `uprooted-settings.ini` (INI format via `UprootedSettings`); TypeScript uses `uprooted-settings.json` (JSON inlined as `window.__UPROOTED_SETTINGS__`). They are separate files.
- **Assuming TypeScript changes affect native Avalonia UI** — they are separate runtimes (Chromium vs .NET). No shared state. CSS variables don't reach Avalonia controls; resource dictionaries don't reach the browser.

## Error Handling

```csharp
// Top-level: catch everything, log, continue
try
{
    // Feature initialization
}
catch (Exception ex)
{
    Logger.Log("MyFeature", $"Init error: {ex.Message}");
    // Don't rethrow — would crash Root
}

// Non-fatal operations: catch and continue
try
{
    r.AddChild(panel, child);
}
catch (Exception ex)
{
    Logger.Log("MyFeature", $"Non-fatal: {ex.Message}");
}
```

**Never throw from injected code.** An unhandled exception can crash Root entirely. Logger itself swallows its own exceptions.

## File Structure Conventions

```csharp
namespace Uprooted;       // All hook classes (except StartupHook which is global namespace)

internal class MyFeature  // internal access (never public except Entry.cs)
{
    private readonly AvaloniaReflection _r;

    // Use constructor injection of AvaloniaReflection
    public MyFeature(AvaloniaReflection resolver)
    {
        _r = resolver;
    }
}
```

- Zero NuGet dependencies — everything via reflection
- `internal` access on all classes (except `Entry.cs` which needs `public` for `CreateInstance`)
- `StartupHook` class lives in global namespace (per .NET startup hook convention)
- Logging: `Logger.Log("Category", "message")` — file-based, thread-safe, never throws

## One-Time Initialization Guard

```csharp
private static int _initialized;

public static void Initialize()
{
    if (Interlocked.CompareExchange(ref _initialized, 1, 0) != 0)
        return;
    // Only first caller reaches here
}
```

Use `Interlocked.CompareExchange` CAS pattern. Never `lock` or simple `bool` flags.
