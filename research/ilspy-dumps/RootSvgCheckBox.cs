// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootSvgCheckBox
using Avalonia;
using Avalonia.Controls;
using RootApp.Client.Avalonia.Controls;

public class RootSvgCheckBox : CheckBox
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

