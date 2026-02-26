using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace RootApp.Client.Avalonia.Controls;

public class RootLinkButton : Button
{
	public static readonly StyledProperty<TextTrimming> TextTrimmingProperty = AvaloniaProperty.Register<RootLinkButton, TextTrimming>("TextTrimming", global::Avalonia.Media.TextTrimming.None);

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
