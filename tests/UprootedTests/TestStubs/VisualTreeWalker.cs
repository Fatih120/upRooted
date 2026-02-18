namespace Uprooted;

// Stub: returns empty tree — sufficient for ClearUrlsEngine to compile
internal class VisualTreeWalker
{
    internal VisualTreeWalker(AvaloniaReflection resolver) { }
    internal IEnumerable<object> DescendantsDepthFirst(object root) => Enumerable.Empty<object>();
    internal object? FindFirstTextBlock(object root, string exactText) => null;
}
