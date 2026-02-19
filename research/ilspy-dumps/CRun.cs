// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CRun
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using Avalonia.Metadata;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.Components.Geometries;

public class CRun : CInline
{
	public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<CRun, string>("Text");

	[Content]
	public string Text
	{
		get
		{
			return GetValue(TextProperty);
		}
		set
		{
			SetValue(TextProperty, value2);
		}
	}

	protected override IEnumerable<CGeometry> MeasureOverride(double P_0, double P_1)
	{
		if (string.IsNullOrEmpty(Text))
		{
			yield break;
		}
		TextRunProperties runProps = CreateTextRunProperties(base.Foreground);
		TextParagraphProperties parProps = CreateTextParagraphProperties(runProps);
		SimpleTextSource source = new SimpleTextSource(Text.AsMemory(), runProps);
		int offset = 0;
		int totalLength = Text.Length;
		TextLine prevLine = null;
		double lineWidth = Math.Min(P_0, P_1);
		int lineIndex = 0;
		while (offset < totalLength)
		{
			TextLine line = TextFormatter.Current.FormatLine(source, offset, lineWidth, parProps, prevLine?.TextLineBreak);
			if (line == null)
			{
				break;
			}
			int consumed = line.Length;
			bool lineBreak = offset + consumed < totalLength;
			yield return new TextLineGeometry(this, source, line, lineBreak);
			offset += consumed;
			lineWidth = P_0;
			prevLine = line;
			lineIndex++;
		}
	}

	internal TextParagraphProperties CreateTextParagraphProperties(TextRunProperties P_0)
	{
		return new GenericTextParagraphProperties(FlowDirection.LeftToRight, TextAlignment.Left, true, false, P_0, TextWrapping.Wrap, double.NaN, 0.0, 0.0);
	}

	internal TextRunProperties CreateTextRunProperties(IBrush? P_0)
	{
		return new GenericTextRunProperties(base.Typeface, base.FontSize, null, P_0);
	}

	public override string AsString()
	{
		return Text;
	}
}

