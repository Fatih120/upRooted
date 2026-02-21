// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.PathSegment
using Avalonia;
using Avalonia.Media;

public abstract class PathSegment : AvaloniaObject
{
	public static readonly StyledProperty<bool> IsStrokedProperty = AvaloniaProperty.Register<PathSegment, bool>("IsStroked", true);

	public bool IsStroked => GetValue(IsStrokedProperty);

	internal abstract void ApplyTo(StreamGeometryContext P_0);
}

