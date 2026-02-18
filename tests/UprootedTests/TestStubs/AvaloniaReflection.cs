namespace Uprooted;

// Stub: minimal surface needed for ClearUrlsEngine to compile — no Avalonia needed in tests
internal class AvaloniaReflection
{
    internal void RunOnUIThread(Action action) => action();
    internal IEnumerable<object> GetVisualChildren(object node) => Enumerable.Empty<object>();
    internal bool IsTextBlock(object node) => false;
    internal string? GetText(object node) => null;
}
