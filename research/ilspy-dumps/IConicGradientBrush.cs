// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IConicGradientBrush
using Avalonia;
using Avalonia.Media;
using Avalonia.Metadata;

[NotClientImplementable]
public interface IConicGradientBrush : IGradientBrush, IBrush
{
	RelativePoint Center { get; }

	double Angle { get; }
}

