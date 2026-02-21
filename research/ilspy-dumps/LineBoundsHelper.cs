// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.SceneGraph.LineBoundsHelper
using System;
using Avalonia;
using Avalonia.Media;

internal static class LineBoundsHelper
{
	private static double CalculateAngle(Point P_0, Point P_1)
	{
		double num = P_1.X - P_0.X;
		return Math.Atan2(P_1.Y - P_0.Y, num);
	}

	internal static double CalculateOppSide(double P_0, double P_1)
	{
		return Math.Sin(P_0) * P_1;
	}

	internal static double CalculateAdjSide(double P_0, double P_1)
	{
		return Math.Cos(P_0) * P_1;
	}

	private static (Point p1, Point p2) TranslatePointsAlongTangent(Point P_0, Point P_1, double P_2, double P_3)
	{
		double num = CalculateOppSide(P_2, P_3);
		double num2 = CalculateAdjSide(P_2, P_3);
		Point point = new Point(P_0.X + num, P_0.Y - num2);
		Point point2 = new Point(P_0.X - num, P_0.Y + num2);
		Point point3 = new Point(P_1.X + num, P_1.Y - num2);
		Point point4 = new Point(P_1.X - num, P_1.Y + num2);
		double num3 = Math.Min(point.X, Math.Min(point2.X, Math.Min(point3.X, point4.X)));
		double num4 = Math.Min(point.Y, Math.Min(point2.Y, Math.Min(point3.Y, point4.Y)));
		double num5 = Math.Max(point.X, Math.Max(point2.X, Math.Max(point3.X, point4.X)));
		double num6 = Math.Max(point.Y, Math.Max(point2.Y, Math.Max(point3.Y, point4.Y)));
		return (p1: new Point(num3, num4), p2: new Point(num5, num6));
	}

	private static Rect CalculateBounds(Point P_0, Point P_1, double P_2, double P_3)
	{
		(Point, Point) tuple = TranslatePointsAlongTangent(P_0, P_1, P_3, P_2 / 2.0);
		return new Rect(tuple.Item1, tuple.Item2);
	}

	public static Rect CalculateBounds(Point P_0, Point P_1, IPen P_2)
	{
		double num = CalculateAngle(P_0, P_1);
		if (P_2.LineCap != PenLineCap.Flat)
		{
			(Point, Point) tuple = TranslatePointsAlongTangent(P_0, P_1, num - Math.PI / 2.0, P_2.Thickness / 2.0);
			return CalculateBounds(tuple.Item1, tuple.Item2, P_2.Thickness, num);
		}
		return CalculateBounds(P_0, P_1, P_2.Thickness, num);
	}
}
