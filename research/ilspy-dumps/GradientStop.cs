// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.GradientStop
using Avalonia;
using Avalonia.Media;

public sealed class GradientStop : AvaloniaObject, IGradientStop
{
	public static readonly StyledProperty<double> OffsetProperty = AvaloniaProperty.Register<GradientStop, double>("Offset", 0.0);

	public static readonly StyledProperty<Color> ColorProperty = AvaloniaProperty.Register<GradientStop, Color>("Color");

	public double Offset
	{
		get
		{
			return GetValue(OffsetProperty);
		}
		set
		{
			SetValue(OffsetProperty, value2);
		}
	}

	public Color Color
	{
		get
		{
			return GetValue(ColorProperty);
		}
		set
		{
			SetValue(ColorProperty, value2);
		}
	}
}

