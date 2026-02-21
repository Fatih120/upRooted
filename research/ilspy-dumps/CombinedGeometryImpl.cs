// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.CombinedGeometryImpl
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class CombinedGeometryImpl : GeometryImpl
{
	public override Rect Bounds { get; }

	public override SKPath? StrokePath { get; }

	public override SKPath? FillPath { get; }

	public CombinedGeometryImpl(SKPath? P_0, SKPath? P_1)
	{
		StrokePath = P_0;
		FillPath = P_1;
		SKRect sKRect = P_0?.TightBounds ?? default(SKRect);
		if (P_1 != null)
		{
			sKRect.Union(P_1.TightBounds);
		}
		Bounds = sKRect.ToAvaloniaRect();
	}

	public static CombinedGeometryImpl ForceCreate(GeometryCombineMode P_0, IGeometryImpl P_1, IGeometryImpl P_2)
	{
		if (P_1 is GeometryImpl geometryImpl && P_2 is GeometryImpl geometryImpl2)
		{
			CombinedGeometryImpl combinedGeometryImpl = TryCreate(P_0, geometryImpl, geometryImpl2);
			if (combinedGeometryImpl != null)
			{
				return combinedGeometryImpl;
			}
		}
		return new CombinedGeometryImpl(null, null);
	}

	public static CombinedGeometryImpl? TryCreate(GeometryCombineMode P_0, GeometryImpl P_1, GeometryImpl P_2)
	{
		SKPathOp sKPathOp = P_0 switch
		{
			GeometryCombineMode.Intersect => SKPathOp.Intersect, 
			GeometryCombineMode.Xor => SKPathOp.Xor, 
			GeometryCombineMode.Exclude => SKPathOp.Difference, 
			_ => SKPathOp.Union, 
		};
		SKPath sKPath = ((P_1.StrokePath != null && P_2.StrokePath != null) ? P_1.StrokePath.Op(P_2.StrokePath, sKPathOp) : null);
		SKPath sKPath2 = null;
		if (P_1.FillPath != null && P_2.FillPath != null)
		{
			sKPath2 = ((P_1.FillPath != P_1.StrokePath || P_2.FillPath != P_2.StrokePath) ? P_1.FillPath.Op(P_2.FillPath, sKPathOp) : sKPath);
		}
		if (sKPath == null && sKPath2 == null)
		{
			return null;
		}
		return new CombinedGeometryImpl(sKPath, sKPath2);
	}
}

