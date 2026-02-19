// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootLinkButton
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using RootApp.Client.Avalonia.Controls;

public class RootLinkButton : Button
{
	public static readonly StyledProperty<TextTrimming> TextTrimmingProperty = AvaloniaProperty.Register<RootLinkButton, TextTrimming>("TextTrimming", Avalonia.Media.TextTrimming.None);

	public static readonly StyledProperty<TextWrapping> TextWrappingProperty = AvaloniaProperty.Register<RootLinkButton, TextWrapping>("TextWrapping", TextWrapping.Wrap);

	public TextTrimming TextTrimming
	{
		set
		{
			SetValue(TextTrimmingProperty, value2);
		}
	}

	public TextWrapping TextWrapping
	{
		set
		{
			SetValue(TextWrappingProperty, value2);
		}
	}
}

