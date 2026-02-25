using Avalonia;
using Avalonia.Controls;

namespace RootApp.Client.Avalonia.Controls;

public class RootSvgButton : Button
{
	public static readonly StyledProperty<string> SvgPathProperty = AvaloniaProperty.Register<RootSvgButton, string>("SvgPath");

	public static readonly StyledProperty<double> SvgWidthProperty = AvaloniaProperty.Register<RootSvgButton, double>("SvgWidth", 0.0);

	public static readonly StyledProperty<double> SvgHeightProperty = AvaloniaProperty.Register<RootSvgButton, double>("SvgHeight", 0.0);

	public static readonly StyledProperty<double> SvgOpacityProperty = AvaloniaProperty.Register<RootSvgImage, double>("SvgOpacity", 0.0);

	public static readonly StyledProperty<double> SvgBorderOpacityProperty = AvaloniaProperty.Register<RootSvgImage, double>("SvgBorderOpacity", 0.0);

	public double SvgWidth
	{
		set
		{
			SetValue(SvgWidthProperty, value2);
		}
	}

	public double SvgHeight
	{
		set
		{
			SetValue(SvgHeightProperty, value2);
		}
	}
}
