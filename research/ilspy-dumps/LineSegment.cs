// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.LineSegment
using System;
using Avalonia;
using Avalonia.Media;

public sealed class LineSegment : PathSegment
{
	public static readonly StyledProperty<Point> PointProperty = AvaloniaProperty.Register<LineSegment, Point>("Point");

	public Point Point
	{
		get
		{
			return GetValue(PointProperty);
		}
		set
		{
			SetValue(PointProperty, value2);
		}
	}

	internal override void ApplyTo(StreamGeometryContext P_0)
	{
		P_0.LineTo(Point, base.IsStroked);
	}

	public override string ToString()
	{
		return FormattableString.Invariant($"L {Point}");
	}
}

