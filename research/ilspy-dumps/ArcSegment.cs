// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ArcSegment
using System;
using Avalonia;
using Avalonia.Media;

public sealed class ArcSegment : PathSegment
{
	public static readonly StyledProperty<bool> IsLargeArcProperty = AvaloniaProperty.Register<ArcSegment, bool>("IsLargeArc", false);

	public static readonly StyledProperty<Point> PointProperty = AvaloniaProperty.Register<ArcSegment, Point>("Point");

	public static readonly StyledProperty<double> RotationAngleProperty = AvaloniaProperty.Register<ArcSegment, double>("RotationAngle", 0.0);

	public static readonly StyledProperty<Size> SizeProperty = AvaloniaProperty.Register<ArcSegment, Size>("Size");

	public static readonly StyledProperty<SweepDirection> SweepDirectionProperty = AvaloniaProperty.Register<ArcSegment, SweepDirection>("SweepDirection", SweepDirection.Clockwise);

	public bool IsLargeArc
	{
		get
		{
			return GetValue(IsLargeArcProperty);
		}
		set
		{
			SetValue(IsLargeArcProperty, value2);
		}
	}

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

	public double RotationAngle
	{
		get
		{
			return GetValue(RotationAngleProperty);
		}
		set
		{
			SetValue(RotationAngleProperty, value2);
		}
	}

	public Size Size
	{
		get
		{
			return GetValue(SizeProperty);
		}
		set
		{
			SetValue(SizeProperty, value2);
		}
	}

	public SweepDirection SweepDirection
	{
		get
		{
			return GetValue(SweepDirectionProperty);
		}
		set
		{
			SetValue(SweepDirectionProperty, value2);
		}
	}

	internal override void ApplyTo(StreamGeometryContext P_0)
	{
		P_0.ArcTo(Point, Size, RotationAngle, IsLargeArc, SweepDirection, base.IsStroked);
	}

	public override string ToString()
	{
		return FormattableString.Invariant($"A {Size} {RotationAngle} {(IsLargeArc ? 1 : 0)} {(int)SweepDirection} {Point}");
	}
}

