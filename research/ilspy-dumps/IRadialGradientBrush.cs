// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IRadialGradientBrush
using Avalonia;
using Avalonia.Media;
using Avalonia.Metadata;

[NotClientImplementable]
public interface IRadialGradientBrush : IGradientBrush, IBrush
{
	RelativePoint Center { get; }

	RelativePoint GradientOrigin { get; }

	RelativeScalar RadiusX { get; }

	RelativeScalar RadiusY { get; }
}

