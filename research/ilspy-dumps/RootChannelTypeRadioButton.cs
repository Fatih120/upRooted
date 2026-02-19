// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootChannelTypeRadioButton
using Avalonia;
using Avalonia.Controls;
using RootApp.Client.Avalonia.Controls;

public class RootChannelTypeRadioButton : RadioButton
{
	public static readonly StyledProperty<string> SvgPathProperty = AvaloniaProperty.Register<RootChannelTypeRadioButton, string>("SvgPath");

	public static readonly StyledProperty<double> SvgWidthProperty = AvaloniaProperty.Register<RootChannelTypeRadioButton, double>("SvgWidth", 0.0);

	public static readonly StyledProperty<double> SvgHeightProperty = AvaloniaProperty.Register<RootChannelTypeRadioButton, double>("SvgHeight", 0.0);

	public static readonly StyledProperty<string> TitleTextProperty = AvaloniaProperty.Register<RootChannelTypeRadioButton, string>("TitleText");

	public static readonly StyledProperty<string> DescriptionTextProperty = AvaloniaProperty.Register<RootChannelTypeRadioButton, string>("DescriptionText");

	public static readonly StyledProperty<bool> ComingSoonStatusProperty = AvaloniaProperty.Register<RootChannelTypeRadioButton, bool>("ComingSoonStatus", false);

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

	public string TitleText
	{
		set
		{
			SetValue(TitleTextProperty, value2);
		}
	}

	public string DescriptionText
	{
		set
		{
			SetValue(DescriptionTextProperty, value2);
		}
	}

	public bool ComingSoonStatus
	{
		set
		{
			SetValue(ComingSoonStatusProperty, value2);
		}
	}
}

