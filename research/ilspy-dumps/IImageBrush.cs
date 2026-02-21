// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IImageBrush
using Avalonia.Media;
using Avalonia.Metadata;

[NotClientImplementable]
public interface IImageBrush : ITileBrush, IBrush
{
	IImageBrushSource? Source { get; }
}

