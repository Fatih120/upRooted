// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.TextWithBadgePanel
using System;
using Avalonia;
using Avalonia.Controls;

public class TextWithBadgePanel : Panel
{
	protected override Size MeasureOverride(Size P_0)
	{
		if (base.Children.Count == 0)
		{
			return new Size(0.0, 0.0);
		}
		Control control = ((base.Children.Count > 0) ? base.Children[0] : null);
		Control control2 = ((base.Children.Count > 1) ? base.Children[1] : null);
		double num = 0.0;
		double num2 = 0.0;
		if (control2 != null)
		{
			control2.Measure(P_0);
			num = Math.Max(num, control2.DesiredSize.Height);
		}
		if (control != null)
		{
			double num3 = control2?.DesiredSize.Width ?? 0.0;
			double num4 = Math.Max(0.0, P_0.Width - num3);
			control.Measure(new Size(num4, P_0.Height));
			num = Math.Max(num, control.DesiredSize.Height);
			num2 = control.DesiredSize.Width;
		}
		if (control2 != null)
		{
			num2 += control2.DesiredSize.Width;
		}
		return new Size(Math.Min(num2, P_0.Width), num);
	}

	protected override Size ArrangeOverride(Size P_0)
	{
		if (base.Children.Count == 0)
		{
			return P_0;
		}
		Control control = ((base.Children.Count > 0) ? base.Children[0] : null);
		Control control2 = ((base.Children.Count > 1) ? base.Children[1] : null);
		double val = 0.0;
		if (control != null)
		{
			double num = control2?.DesiredSize.Width ?? 0.0;
			double num2 = Math.Max(0.0, P_0.Width - num);
			control.Arrange(new Rect(0.0, 0.0, num2, P_0.Height));
			val = ((!(control is TextBlock textBlock)) ? Math.Min(control.DesiredSize.Width, num2) : Math.Min(textBlock.TextLayout.Width, num2));
		}
		if (control2 != null)
		{
			double width = control2.DesiredSize.Width;
			double height = control2.DesiredSize.Height;
			double num3 = Math.Max(0.0, (P_0.Height - height) / 2.0);
			double num4 = Math.Min(val, P_0.Width - width);
			control2.Arrange(new Rect(num4, num3, width, height));
		}
		return P_0;
	}
}

