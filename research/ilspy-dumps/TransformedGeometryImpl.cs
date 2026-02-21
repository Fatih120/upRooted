// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.TransformedGeometryImpl
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class TransformedGeometryImpl : GeometryImpl, ITransformedGeometryImpl, IGeometryImpl
{
	public override SKPath? StrokePath { get; }

	public override SKPath? FillPath { get; }

	public IGeometryImpl SourceGeometry { get; }

	public Matrix Transform { get; }

	public override Rect Bounds { get; }

	public TransformedGeometryImpl(GeometryImpl P_0, Matrix P_1)
	{
		SourceGeometry = P_0;
		Transform = P_1;
		SKMatrix sKMatrix = P_1.ToSKMatrix();
		SKPath sKPath = (StrokePath = P_0.StrokePath.Clone());
		sKPath?.Transform(sKMatrix);
		Bounds = sKPath?.TightBounds.ToAvaloniaRect() ?? default(Rect);
		if (P_0.StrokePath == P_0.FillPath)
		{
			FillPath = sKPath;
		}
		else if (P_0.FillPath != null)
		{
			(FillPath = P_0.FillPath.Clone()).Transform(sKMatrix);
		}
	}
}

