// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.BezierSegment
using System;
using Avalonia;
using Avalonia.Media;

public sealed class BezierSegment : PathSegment
{
	public static readonly StyledProperty<Point> Point1Property = AvaloniaProperty.Register<BezierSegment, Point>("Point1");

	public static readonly StyledProperty<Point> Point2Property = AvaloniaProperty.Register<BezierSegment, Point>("Point2");

	public static readonly StyledProperty<Point> Point3Property = AvaloniaProperty.Register<BezierSegment, Point>("Point3");

	public Point Point1
	{
		get
		{
			return GetValue(Point1Property);
		}
		set
		{
			SetValue(Point1Property, value2);
		}
	}

	public Point Point2
	{
		get
		{
			return GetValue(Point2Property);
		}
		set
		{
			SetValue(Point2Property, value2);
		}
	}

	public Point Point3
	{
		get
		{
			return GetValue(Point3Property);
		}
		set
		{
			SetValue(Point3Property, value2);
		}
	}

	internal override void ApplyTo(StreamGeometryContext P_0)
	{
		P_0.CubicBezierTo(Point1, Point2, Point3, base.IsStroked);
	}

	public override string ToString()
	{
		return FormattableString.Invariant($"C {Point1} {Point2} {Point3}");
	}
}

