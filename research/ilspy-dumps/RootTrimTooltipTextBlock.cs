// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootTrimTooltipTextBlock
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using RootApp.Client.Avalonia.Controls;

public class RootTrimTooltipTextBlock : TextBlock
{
	protected override void OnPointerEntered(PointerEventArgs P_0)
	{
		base.OnPointerEntered(P_0);
		if (isTrimmed())
		{
			if (ToolTip.GetTip(this) == null)
			{
				ToolTip.SetTip(this, new RootToolTip
				{
					Content = new TextBlock
					{
						Text = base.Text,
						FontFamily = (FontFamily)Application.Current.FindResource("RootFont"),
						FontWeight = (FontWeight)450,
						FontSize = 14.0,
						Padding = new Thickness(0.0),
						VerticalAlignment = VerticalAlignment.Center
					}
				});
			}
			ToolTip.SetIsOpen(this, true);
		}
	}

	protected override void OnPointerExited(PointerEventArgs P_0)
	{
		base.OnPointerExited(P_0);
		ToolTip.SetIsOpen(this, false);
		ToolTip.SetTip(this, null);
	}

	private bool isTrimmed()
	{
		TextLayout textLayout = base.TextLayout;
		return textLayout.TextLines.Any((TextLine x) => x.HasCollapsed);
	}
}

