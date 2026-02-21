// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SkiaRegionImpl
using System;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class SkiaRegionImpl : IPlatformRenderInterfaceRegion, IDisposable
{
	private SKRegion? _region = new SKRegion();

	private bool _rectsValid;

	public SKRegion Region => _region ?? throw new ObjectDisposedException("SkiaRegionImpl");

	public bool IsEmpty => Region.IsEmpty;

	public LtrbPixelRect Bounds => Region.Bounds.ToAvaloniaLtrbPixelRect();

	public void Dispose()
	{
		_region?.Dispose();
		_region = null;
	}

	public void AddRect(LtrbPixelRect P_0)
	{
		_rectsValid = false;
		Region.Op(P_0.Left, P_0.Top, P_0.Right, P_0.Bottom, SKRegionOperation.Union);
	}

	public void Reset()
	{
		_rectsValid = false;
		Region.SetEmpty();
	}

	public bool Intersects(LtrbRect P_0)
	{
		return Region.Intersects(new SKRectI((int)P_0.Left, (int)P_0.Top, (int)Math.Ceiling(P_0.Right), (int)Math.Ceiling(P_0.Bottom)));
	}
}

