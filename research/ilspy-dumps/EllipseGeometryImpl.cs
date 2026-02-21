// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.EllipseGeometryImpl
using Avalonia;
using Avalonia.Skia;
using SkiaSharp;

internal class EllipseGeometryImpl : GeometryImpl
{
	public override Rect Bounds { get; }

	public override SKPath StrokePath { get; }

	public override SKPath FillPath => StrokePath;

	public EllipseGeometryImpl(Rect P_0)
	{
		SKPath sKPath = new SKPath();
		sKPath.AddOval(P_0.ToSKRect());
		StrokePath = sKPath;
		Bounds = P_0;
	}
}

