// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ISolidColorBrush
using Avalonia.Media;
using Avalonia.Metadata;

[NotClientImplementable]
public interface ISolidColorBrush : IBrush
{
	Color Color { get; }
}

