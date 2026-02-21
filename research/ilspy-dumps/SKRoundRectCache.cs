// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SKRoundRectCache
using System.Collections.Concurrent;
using Avalonia;
using Avalonia.Skia;
using SkiaSharp;

internal class SKRoundRectCache : SKCacheBase<SKRoundRect, SKRoundRectCache>
{
	private readonly ConcurrentBag<SKPoint[]> _radiiCache = new ConcurrentBag<SKPoint[]>();

	public SKRoundRect GetAndSetRadii(in SKRect P_0, in RoundedRect P_1)
	{
		if (!Cache.TryTake(out SKRoundRect sKRoundRect))
		{
			sKRoundRect = new SKRoundRect();
		}
		if (!_radiiCache.TryTake(out SKPoint[] array))
		{
			array = new SKPoint[4];
		}
		array[0].X = (float)P_1.RadiiTopLeft.X;
		array[0].Y = (float)P_1.RadiiTopLeft.Y;
		array[1].X = (float)P_1.RadiiTopRight.X;
		array[1].Y = (float)P_1.RadiiTopRight.Y;
		array[2].X = (float)P_1.RadiiBottomRight.X;
		array[2].Y = (float)P_1.RadiiBottomRight.Y;
		array[3].X = (float)P_1.RadiiBottomLeft.X;
		array[3].Y = (float)P_1.RadiiBottomLeft.Y;
		sKRoundRect.SetRectRadii(P_0, array);
		_radiiCache.Add(array);
		return sKRoundRect;
	}

	public SKRoundRect GetAndSetRadii(in SKRect P_0, in SKPoint[] P_1)
	{
		if (!Cache.TryTake(out SKRoundRect sKRoundRect))
		{
			sKRoundRect = new SKRoundRect();
		}
		sKRoundRect.SetRectRadii(P_0, P_1);
		return sKRoundRect;
	}
}

