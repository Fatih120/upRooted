# C# Hook Patterns

Code patterns used in the Uprooted C# hook (`hook/` directory). All patterns use reflection exclusively — no direct Avalonia type references.

## Reflection Access Pattern

All Avalonia type access goes through the `AvaloniaReflection` singleton. Never use `typeof()`, `Type.GetType()`, or direct assembly references.

```csharp
// Initialization (once, in StartupHook.InjectorLoop)
var resolver = new AvaloniaReflection();
if (!resolver.Resolve()) {
    Logger.Log("Startup", "Type resolution failed, aborting");
    return;
}

// Creating controls
var textBlock = Activator.CreateInstance(resolver.TextBlockType!);

// Setting properties via cached PropertyInfo
resolver.TextBlockType!.GetProperty("Text")!.SetValue(textBlock, "Hello");
resolver.TextBlockType!.GetProperty("FontSize")!.SetValue(textBlock, 14.0);

// Helper methods on AvaloniaReflection for common operations
var tb = resolver.CreateTextBlock(text: "Label", fontSize: 14.0);
resolver.SetGridColumn(control, 1);
resolver.SetGridRow(control, 0);
resolver.AddChild(panel, child);
```

## One-Time Initialization Pattern

Use `Interlocked.CompareExchange` for CAS-based initialization guards. Never use `lock` or a simple `bool` flag.

```csharp
private static int _initialized;

public static void Initialize()
{
    // Only the first thread to CAS from 0 to 1 enters
    if (Interlocked.CompareExchange(ref _initialized, 1, 0) != 0)
        return;

    // Safe to proceed — only one thread reaches here
    StartupHook.Initialize();
}
```

Also used for concurrent operation guards:

```csharp
private int _injecting;

private void CheckAndInject()
{
    // Prevent re-entrant calls from timer overlap
    if (Interlocked.CompareExchange(ref _injecting, 1, 0) != 0)
        return;
    try
    {
        // ... injection logic ...
    }
    finally
    {
        Interlocked.Exchange(ref _injecting, 0);
    }
}
```

## UI Thread Dispatch Pattern

Heavy work runs on a background thread. UI mutations dispatch to `Dispatcher.UIThread`.

```csharp
// Background thread spawning (StartupHook)
var thread = new Thread(InjectorLoop)
{
    IsBackground = true,
    Name = "Uprooted-Injector"
};
thread.Start();

// Timer callback dispatches to UI thread (SidebarInjector)
_timer = new Timer(_ => resolver.RunOnUIThread(() => CheckAndInject()), null, 0, 200);

// RunOnUIThread uses Dispatcher.UIThread.Post
public void RunOnUIThread(Action action)
{
    var dispatcher = _dispatcherUIThread!.GetValue(null);
    _dispatcherPost!.Invoke(dispatcher, new object[] { action, _normalPriority! });
}
```

## Content Overlay Pattern

Display Uprooted pages without modifying Root's content panel. Setting `ContentControl.Content` causes freezes.

```csharp
// WRONG — causes UI freeze:
// contentControl.Content = ourPage;

// CORRECT — add as sibling in layout Grid:
// 1. Find the layout Grid containing Root's content area
// 2. Create Uprooted page with opaque background (#0D1521)
// 3. Add as child of the same Grid with matching Column/Row
resolver.SetGridColumn(uprootedPage, contentColumnIndex);
resolver.SetGridRow(uprootedPage, 0);
resolver.AddChild(layoutGrid, uprootedPage);

// 4. Remove overlay when user clicks Root's sidebar items
// (don't remove Root's original content — it stays underneath)
```

## Tag-Based Identification Pattern

All injected Avalonia controls use `Control.Tag` strings for identification and cleanup.

```csharp
// Tagging injected controls
resolver.SetTag(control, "uprooted-injected");     // Section header
resolver.SetTag(navItem, "uprooted-item-plugins");  // Nav items
resolver.SetTag(page, "uprooted-content");           // Content pages

// Finding injected controls for cleanup
bool IsOurs(object control) =>
    resolver.GetTag(control)?.ToString()?.StartsWith("uprooted-") == true;

// Cleanup: remove all tagged controls from parent
foreach (var child in resolver.GetChildren(navContainer))
{
    if (IsOurs(child))
        resolver.RemoveChild(navContainer, child);
}
```

## Phase Wait Pattern

The startup sequence uses a polling wait with timeout for each initialization phase.

```csharp
private static bool WaitFor(Func<bool> condition, TimeSpan timeout)
{
    var deadline = DateTime.UtcNow + timeout;
    while (DateTime.UtcNow < deadline)
    {
        try
        {
            if (condition()) return true;
        }
        catch { } // Swallow — condition may throw during startup
        Thread.Sleep(500);
    }
    return false;
}

// Usage:
if (!WaitFor(() => resolver.GetAppCurrent() != null, TimeSpan.FromSeconds(30)))
{
    Logger.Log("Startup", "Phase 2 FAILED: Application.Current not available");
    return;
}
```

## Logging Pattern

Thread-safe file logging with category prefix. Logger never throws.

```csharp
// Category-based logging
Logger.Log("Startup", "Phase 1 OK: Avalonia assemblies loaded");
Logger.Log("Injector", "Settings page detected, injecting sidebar");
Logger.Log("Theme", $"Applying theme: {themeName}");

// Log file: uprooted-hook.log in profile directory
// Format: [timestamp] [Category] message
```

## Error Handling Pattern

Never throw from injected code. Catch everything, log, and continue.

```csharp
// Top-level try/catch in injector loop
try
{
    // ... entire initialization sequence ...
}
catch (Exception ex)
{
    Logger.Log("Startup", $"Fatal error in injector: {ex}");
}

// Non-fatal errors continue execution
try
{
    var verifier = new HtmlPatchVerifier();
    verifier.VerifyAndRepair();
}
catch (Exception ex)
{
    Logger.Log("Startup", $"Phase 0 non-fatal error: {ex.Message}");
    // Continue to Phase 1 — HTML patches are not critical for native UI
}
```

## Avalonia Style Matching

Content pages match Root's exact card styling for visual consistency.

```
Card background:    #081408
Corner radius:      12
Border:             #19ffffff, thickness 0.5
Inner padding:      24px
Page margin:        24/24/24/0
Section header:     "APP SETTINGS" style (UpperCase, specific font size)
```

## Assembly Scanning Pattern

`AvaloniaReflection.Resolve()` builds a type dictionary from loaded assemblies.

```csharp
// Filter assemblies by Avalonia prefix
var avaloniaAssemblies = AppDomain.CurrentDomain.GetAssemblies()
    .Where(a => a.GetName().Name?.StartsWith("Avalonia") == true);

// Build dictionary: short type name -> Type
foreach (var asm in avaloniaAssemblies)
{
    foreach (var type in asm.GetExportedTypes())
    {
        _types[type.Name] = type;   // e.g., "TextBlock" -> Avalonia.Controls.TextBlock
        _types[type.FullName!] = type; // e.g., "Avalonia.Controls.TextBlock" -> Type
    }
}

// Resolve specific types by name
TextBlockType = _types.GetValueOrDefault("TextBlock");
GridType = _types.GetValueOrDefault("Grid");
// ... ~80 types total
```
