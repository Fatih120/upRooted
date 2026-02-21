// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IPen
using Avalonia.Media;
using Avalonia.Metadata;

[NotClientImplementable]
public interface IPen
{
	IBrush? Brush { get; }

	IDashStyle? DashStyle { get; }

	PenLineCap LineCap { get; }

	PenLineJoin LineJoin { get; }

	double MiterLimit { get; }

	double Thickness { get; }
}

