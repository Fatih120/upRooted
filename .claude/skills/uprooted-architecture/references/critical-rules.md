# Critical Rules

These rules prevent real bugs in the Uprooted codebase. Violating any of them causes crashes, freezes, or silent failures.

## Rule 1: Never Use `Type.GetType()` for Avalonia Types

**Forbidden:**
```csharp
var type = Type.GetType("Avalonia.Controls.TextBlock, Avalonia.Controls");
var instance = typeof(TextBlock); // Also forbidden — typeof() won't resolve
```

**Why:** Root.exe is a single-file .NET 10 binary. Assembly-qualified names cannot be resolved because assemblies are embedded. `Type.GetType()` returns `null`, leading to `NullReferenceException` downstream.

**Correct:**
```csharp
var r = new AvaloniaReflection();
r.Resolve(); // Scans loaded assemblies filtered by "Avalonia" prefix
var textBlock = Activator.CreateInstance(r.TextBlockType!);
```

**Always use `AvaloniaReflection`** for ALL Avalonia type access. This class scans `AppDomain.CurrentDomain.GetAssemblies()` and builds a dictionary of ~80 types by name.

---

## Rule 2: Never Modify `ContentControl.Content` Directly

**Forbidden:**
```csharp
contentControl.GetType().GetProperty("Content")!.SetValue(contentControl, ourPage);
```

**Why:** Setting `ContentControl.Content` or `ScrollContentPresenter.Content` in Root's settings page causes the UI to freeze. Root's `OnDetachedFromVisualTreeCore` walks the modified tree and enters an infinite loop or deadlock.

**Correct — Content Overlay Pattern:**
```csharp
// Add Uprooted page as a SIBLING in the layout Grid
// with matching Column/Row and opaque background (#0D1521)
var grid = parentLayoutGrid;
r.SetGridColumn(uprootedPage, contentColumnIndex);
r.SetGridRow(uprootedPage, 0);
r.AddChild(grid, uprootedPage);
// Remove overlay when Root sidebar clicked
```

This places the Uprooted page on top of Root's content area without modifying Root's content tree.

---

## Rule 3: Never Use `System.Text.Json` in Hook

**Forbidden:**
```csharp
using System.Text.Json;
var settings = JsonSerializer.Deserialize<Settings>(json);
```

**Why:** The CLR profiler injection context lacks the metadata tokens needed by `System.Text.Json`'s source generators. Calling any `System.Text.Json` API throws `MissingMethodException` at runtime.

**Correct:**
```csharp
// Use UprootedSettings which reads/writes INI-format files
var settings = new UprootedSettings();
string theme = settings.Get("theme", "default");
settings.Set("theme", "crimson");
settings.Save();
```

`UprootedSettings` uses a simple INI key=value format that avoids `System.Text.Json` entirely.

---

## Rule 4: Never Use `EventInfo.AddEventHandler` for RoutedEvents

**Forbidden:**
```csharp
var eventInfo = control.GetType().GetEvent("PointerPressed");
eventInfo.AddEventHandler(control, handler);
```

**Why:** Avalonia `RoutedEvent`s use a custom event system that is incompatible with CLR's `EventInfo.AddEventHandler`. The handler silently fails to register, or throws at runtime.

**Correct — Expression Lambda Pattern:**
```csharp
// Use Expression.Lambda to compile a delegate that calls AddHandler directly
var addHandlerMethod = controlType.GetMethod("AddHandler", ...);
// Build expression tree: (sender, args) => handler(sender, args)
var lambda = Expression.Lambda(delegateType, body, parameters);
var compiled = lambda.Compile();
addHandlerMethod.Invoke(control, new object[] { routedEvent, compiled, ... });
```

See `AvaloniaReflection.cs` for the full `SubscribeRoutedEvent` implementation.

---

## Rule 5: Never Use localStorage

**Forbidden (JavaScript):**
```javascript
localStorage.setItem("uprooted-settings", JSON.stringify(settings));
const data = localStorage.getItem("uprooted-settings");
```

**Why:** Root runs its embedded Chromium (DotNetBrowser) with `--incognito` flag. All localStorage is cleared on each launch. Data stored in localStorage is lost.

**Correct:**
```typescript
// Settings are persisted to uprooted-settings.json on disk
// by the installer/CLI (which has filesystem access)
// At runtime, settings are inlined into HTML by the patcher:
const settings = window.__UPROOTED_SETTINGS__;
// Runtime changes update in-memory only (session-only)
```

---

## Rule 6: DispatcherPriority Is a Struct, Not an Enum

**Forbidden:**
```csharp
var priority = Enum.Parse(dispatcherPriorityType, "Normal");
```

**Why:** In Avalonia 11+, `DispatcherPriority` changed from an enum to a struct. `Enum.Parse` fails because it's not an enum.

**Correct — Fallback Chain:**
```csharp
// Try static property first (e.g., DispatcherPriority.Normal)
var prop = dispatcherPriorityType.GetProperty("Normal", BindingFlags.Public | BindingFlags.Static);
if (prop != null) return prop.GetValue(null);

// Try static field
var field = dispatcherPriorityType.GetField("Normal", BindingFlags.Public | BindingFlags.Static);
if (field != null) return field.GetValue(null);

// Fallback: try Enum.Parse (works on older Avalonia)
// Final fallback: Activator.CreateInstance (default value)
```

See `AvaloniaReflection.Resolve()` for the actual implementation of this fallback chain.

---

## Additional Constraints

### Environment Variables Are User-Scoped
CLR profiler env vars use dual prefixes: `DOTNET_` (primary, .NET 10+) and `CORECLR_` (legacy fallback). Both `DOTNET_ENABLE_PROFILING`/`CORECLR_ENABLE_PROFILING`, `*_PROFILER`, and `*_PROFILER_PATH` are set at user scope. They affect ALL .NET processes. The profiler has a process name guard (checks for "Root") and returns `E_FAIL` for other processes, but there is slight startup overhead for every .NET app.

### Settings Page Detection Is Text-Based
The `VisualTreeWalker` searches for a TextBlock with exact text `"APP SETTINGS"` as an anchor point. If Root renames this text in an update, injection silently fails with no error visible to the user.

### Bridge Proxy Must Install Early
Root may assign bridge globals (`__nativeToWebRtc`, `__webRtcToNative`) asynchronously after page load. The proxy uses `Object.defineProperty` getter/setter traps for deferred installation. If preload runs after Root's assignment, the traps won't fire.

### Never Throw from Injected Code
Both C# and TypeScript layers prioritize stability over error reporting. If injected code throws, it could crash Root entirely. Always catch and log.

### C# Classes Use `internal` Access
All hook classes use `internal` access modifier except `Entry.cs` (which must be `public` for `CreateInstance`). The `StartupHook` class is in the global namespace (no `namespace` declaration) per .NET startup hook convention.
