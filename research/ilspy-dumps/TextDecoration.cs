// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TextDecoration
using System.Collections.Generic;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;

public class TextDecoration : AvaloniaObject
{
	public static readonly StyledProperty<TextDecorationLocation> LocationProperty = AvaloniaProperty.Register<TextDecoration, TextDecorationLocation>("Location", TextDecorationLocation.Underline);

	public static readonly StyledProperty<IBrush?> StrokeProperty = AvaloniaProperty.Register<TextDecoration, IBrush>("Stroke");

	public static readonly StyledProperty<TextDecorationUnit> StrokeThicknessUnitProperty = AvaloniaProperty.Register<TextDecoration, TextDecorationUnit>("StrokeThicknessUnit", TextDecorationUnit.FontRecommended);

	public static readonly StyledProperty<AvaloniaList<double>?> StrokeDashArrayProperty = AvaloniaProperty.Register<TextDecoration, AvaloniaList<double>>("StrokeDashArray");

	public static readonly StyledProperty<double> StrokeDashOffsetProperty = AvaloniaProperty.Register<TextDecoration, double>("StrokeDashOffset", 0.0);

	public static readonly StyledProperty<double> StrokeThicknessProperty = AvaloniaProperty.Register<TextDecoration, double>("StrokeThickness", 1.0);

	public static readonly StyledProperty<PenLineCap> StrokeLineCapProperty = AvaloniaProperty.Register<TextDecoration, PenLineCap>("StrokeLineCap", PenLineCap.Flat);

	public static readonly StyledProperty<double> StrokeOffsetProperty = AvaloniaProperty.Register<TextDecoration, double>("StrokeOffset", 0.0);

	public static readonly StyledProperty<TextDecorationUnit> StrokeOffsetUnitProperty = AvaloniaProperty.Register<TextDecoration, TextDecorationUnit>("StrokeOffsetUnit", TextDecorationUnit.FontRecommended);

	public TextDecorationLocation Location
	{
		get
		{
			return GetValue(LocationProperty);
		}
		set
		{
			SetValue(LocationProperty, value2);
		}
	}

	public IBrush? Stroke
	{
		get
		{
			return GetValue(StrokeProperty);
		}
		set
		{
			SetValue(StrokeProperty, value2);
		}
	}

	public TextDecorationUnit StrokeThicknessUnit => GetValue(StrokeThicknessUnitProperty);

	public AvaloniaList<double>? StrokeDashArray
	{
		get
		{
			return GetValue(StrokeDashArrayProperty);
		}
		set
		{
			SetValue(StrokeDashArrayProperty, value2);
		}
	}

	public double StrokeDashOffset => GetValue(StrokeDashOffsetProperty);

	public double StrokeThickness
	{
		get
		{
			return GetValue(StrokeThicknessProperty);
		}
		set
		{
			SetValue(StrokeThicknessProperty, value2);
		}
	}

	public PenLineCap StrokeLineCap
	{
		get
		{
			return GetValue(StrokeLineCapProperty);
		}
		set
		{
			SetValue(StrokeLineCapProperty, value2);
		}
	}

	public double StrokeOffset => GetValue(StrokeOffsetProperty);

	public TextDecorationUnit StrokeOffsetUnit => GetValue(StrokeOffsetUnitProperty);

	internal void Draw(DrawingContext P_0, GlyphRun P_1, TextMetrics P_2, IBrush P_3)
	{
		Point baselineOrigin = P_1.BaselineOrigin;
		double num = StrokeThickness;
		switch (StrokeThicknessUnit)
		{
		case TextDecorationUnit.FontRecommended:
			switch (Location)
			{
			case TextDecorationLocation.Underline:
				num = P_2.UnderlineThickness;
				break;
			case TextDecorationLocation.Strikethrough:
				num = P_2.StrikethroughThickness;
				break;
			}
			break;
		case TextDecorationUnit.FontRenderingEmSize:
			num = P_2.FontRenderingEmSize * num;
			break;
		}
		Point point = baselineOrigin;
		switch (Location)
		{
		case TextDecorationLocation.Overline:
			point += new Point(0.0, P_2.Ascent);
			break;
		case TextDecorationLocation.Strikethrough:
			point += new Point(0.0, P_2.StrikethroughPosition);
			break;
		case TextDecorationLocation.Underline:
			point += new Point(0.0, P_2.UnderlinePosition);
			break;
		}
		switch (StrokeOffsetUnit)
		{
		case TextDecorationUnit.FontRenderingEmSize:
			point += new Point(0.0, StrokeOffset * P_2.FontRenderingEmSize);
			break;
		case TextDecorationUnit.Pixel:
			point += new Point(0.0, StrokeOffset);
			break;
		}
		Pen pen = new Pen(Stroke ?? P_3, num, new DashStyle(StrokeDashArray, StrokeDashOffset), StrokeLineCap);
		if (Location != TextDecorationLocation.Strikethrough)
		{
			double num2 = P_1.BaselineOrigin.Y - point.Y;
			IReadOnlyList<float> intersections = P_1.GetIntersections((float)(num * 0.5 - num2), (float)(num * 1.5 - num2));
			if (intersections.Count > 0)
			{
				double num3 = baselineOrigin.X;
				double num4 = num3 + P_1.Bounds.Width;
				double num5 = num3;
				List<double> list = new List<double>();
				for (int i = 0; i < intersections.Count; i += 2)
				{
					double num6 = (double)intersections[i] - num;
					num5 = (double)intersections[i + 1] + num;
					if (num6 > num3 && num3 + P_2.FontRenderingEmSize / 12.0 < num6)
					{
						list.Add(num3);
						list.Add(num6);
					}
					num3 = num5;
				}
				if (num5 < num4)
				{
					list.Add(num5);
					list.Add(num4);
				}
				for (int j = 0; j < list.Count; j += 2)
				{
					Point point2 = new Point(list[j], point.Y);
					Point point3 = new Point(list[j + 1], point.Y);
					P_0.DrawLine(pen, point2, point3);
				}
				return;
			}
		}
		Point point4 = point;
		Point point5 = point4 + new Point(P_1.Metrics.Width, 0.0);
		P_0.DrawLine(pen, point4, point5);
	}
}

