// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SKPaintCache
using Avalonia.Skia;
using SkiaSharp;

internal class SKPaintCache : SKCacheBase<SKPaint, SKPaintCache>
{
	public void ReturnReset(SKPaint P_0)
	{
		P_0.Reset();
		Cache.Add(P_0);
	}
}

