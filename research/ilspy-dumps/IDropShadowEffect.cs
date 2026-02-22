// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IDropShadowEffect
using Avalonia.Media;

public interface IDropShadowEffect : IEffect
{
	double OffsetX { get; }

	double OffsetY { get; }

	double BlurRadius { get; }

	Color Color { get; }

	double Opacity { get; }
}

