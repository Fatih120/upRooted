// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.LineGeometryImpl
using System;
using Avalonia;
using Avalonia.Skia;
using SkiaSharp;

internal class LineGeometryImpl : GeometryImpl
{
	public override Rect Bounds { get; }

	public override SKPath StrokePath { get; }

	public override SKPath? FillPath => null;

	public LineGeometryImpl(Point P_0, Point P_1)
	{
		SKPath sKPath = new SKPath();
		sKPath.MoveTo(P_0.ToSKPoint());
		sKPath.LineTo(P_1.ToSKPoint());
		StrokePath = sKPath;
		Bounds = new Rect(new Point(Math.Min(P_0.X, P_1.X), Math.Min(P_0.Y, P_1.Y)), new Point(Math.Max(P_0.X, P_1.X), Math.Max(P_0.Y, P_1.Y)));
	}
}

