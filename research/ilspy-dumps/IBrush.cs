// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IBrush
using System.ComponentModel;
using Avalonia;
using Avalonia.Media;
using Avalonia.Metadata;

[TypeConverter(typeof(BrushConverter))]
[NotClientImplementable]
public interface IBrush
{
	double Opacity { get; }

	ITransform? Transform { get; }

	RelativePoint TransformOrigin { get; }
}

