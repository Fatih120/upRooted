// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.RenderTargetBitmapImpl
using System;
using Avalonia;
using Avalonia.Controls.Platform.Surfaces;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class RenderTargetBitmapImpl : WriteableBitmapImpl, IRenderTargetBitmapImpl, IBitmapImpl, IDisposable, IRenderTarget, IFramebufferPlatformSurface
{
	private readonly FramebufferRenderTarget _renderTarget;

	public bool IsCorrupted => false;

	public RenderTargetBitmapImpl(PixelSize P_0, Vector P_1)
		: base(P_0, P_1, (SKImageInfo.PlatformColorType == SKColorType.Rgba8888) ? PixelFormats.Rgba8888 : PixelFormat.Bgra8888, Avalonia.Platform.AlphaFormat.Premul)
	{
		_renderTarget = new FramebufferRenderTarget(this);
	}

	IDrawingContextImpl IRenderTarget.CreateDrawingContext(bool P_0)
	{
		return _renderTarget.CreateDrawingContext(P_0);
	}

	public override void Dispose()
	{
		_renderTarget.Dispose();
		base.Dispose();
	}

	public IFramebufferRenderTarget CreateFramebufferRenderTarget()
	{
		return new FuncFramebufferRenderTarget(Lock);
	}
}

