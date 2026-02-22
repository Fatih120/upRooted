// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ILinearGradientBrush
using Avalonia;
using Avalonia.Media;
using Avalonia.Metadata;

[NotClientImplementable]
public interface ILinearGradientBrush : IGradientBrush, IBrush
{
	RelativePoint StartPoint { get; }

	RelativePoint EndPoint { get; }
}

