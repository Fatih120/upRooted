// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.Helpers.SKPathHelper
using Avalonia.Media;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using Avalonia.Utilities;
using SkiaSharp;

internal static class SKPathHelper
{
	public static SKPath? CreateStrokedPath(SKPath P_0, IPen P_1)
	{
		if (MathUtilities.IsZero(P_1.Thickness))
		{
			return null;
		}
		SKPaint sKPaint = SKCacheBase<SKPaint, SKPaintCache>.Shared.Get();
		sKPaint.IsStroke = true;
		sKPaint.StrokeWidth = (float)P_1.Thickness;
		sKPaint.StrokeCap = P_1.LineCap.ToSKStrokeCap();
		sKPaint.StrokeJoin = P_1.LineJoin.ToSKStrokeJoin();
		sKPaint.StrokeMiter = (float)P_1.MiterLimit;
		if (DrawingContextHelper.TryCreateDashEffect(P_1, out SKPathEffect pathEffect))
		{
			sKPaint.PathEffect = pathEffect;
		}
		SKPath sKPath = new SKPath();
		sKPaint.GetFillPath(P_0, sKPath);
		sKPaint.PathEffect?.Dispose();
		SKCacheBase<SKPaint, SKPaintCache>.Shared.ReturnReset(sKPaint);
		return sKPath;
	}
}
