// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GeometryGroupImpl
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class GeometryGroupImpl : GeometryImpl
{
	public override Rect Bounds { get; }

	public override SKPath StrokePath { get; }

	public override SKPath FillPath { get; }

	public GeometryGroupImpl(FillRule P_0, IReadOnlyList<IGeometryImpl> P_1)
	{
		SKPathFillType fillType = ((P_0 != FillRule.NonZero) ? SKPathFillType.EvenOdd : SKPathFillType.Winding);
		int count = P_1.Count;
		SKPath sKPath = new SKPath
		{
			FillType = fillType
		};
		bool flag = false;
		for (int i = 0; i < count; i++)
		{
			if (P_1[i] is GeometryImpl geometryImpl)
			{
				if (geometryImpl.StrokePath != null)
				{
					sKPath.AddPath(geometryImpl.StrokePath);
				}
				if (geometryImpl.StrokePath != geometryImpl.FillPath)
				{
					flag = true;
				}
			}
		}
		StrokePath = sKPath;
		if (flag)
		{
			SKPath sKPath2 = new SKPath
			{
				FillType = fillType
			};
			for (int j = 0; j < count; j++)
			{
				if (P_1[j] is GeometryImpl geometryImpl2)
				{
					SKPath fillPath = geometryImpl2.FillPath;
					if (fillPath != null)
					{
						sKPath2.AddPath(fillPath);
					}
				}
			}
			FillPath = sKPath2;
		}
		else
		{
			FillPath = sKPath;
		}
		Bounds = sKPath.TightBounds.ToAvaloniaRect();
	}
}

