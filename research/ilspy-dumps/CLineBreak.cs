// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CLineBreak
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Media;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.Components.Geometries;

public class CLineBreak : CRun
{
	public CLineBreak()
	{
		base.Text = "\n";
	}

	protected override IEnumerable<CGeometry> MeasureOverride(double P_0, double P_1)
	{
		FormattedText ftxt = new FormattedText("Ty", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(base.FontFamily, base.FontStyle, base.FontWeight), base.FontSize, base.Foreground);
		yield return new LineBreakMarkGeometry(this, ftxt.Height);
	}
}

