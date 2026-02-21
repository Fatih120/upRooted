// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SkiaGpuRenderTarget
using System;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;

internal class SkiaGpuRenderTarget : IRenderTarget2, IRenderTarget, IDisposable
{
	private readonly ISkiaGpu _skiaGpu;

	private readonly ISkiaGpuRenderTarget _renderTarget;

	public bool IsCorrupted => _renderTarget.IsCorrupted;

	public RenderTargetProperties Properties { get; }

	public SkiaGpuRenderTarget(ISkiaGpu P_0, ISkiaGpuRenderTarget P_1)
	{
		_skiaGpu = P_0;
		_renderTarget = P_1;
	}

	public void Dispose()
	{
		_renderTarget.Dispose();
	}

	public IDrawingContextImpl CreateDrawingContext(PixelSize P_0, out RenderTargetDrawingContextProperties P_1)
	{
		return CreateDrawingContextCore(P_0, false, out P_1);
	}

	public IDrawingContextImpl CreateDrawingContext(bool P_0)
	{
		RenderTargetDrawingContextProperties renderTargetDrawingContextProperties;
		return CreateDrawingContextCore(null, P_0, out renderTargetDrawingContextProperties);
	}

	private IDrawingContextImpl CreateDrawingContextCore(PixelSize? P_0, bool P_1, out RenderTargetDrawingContextProperties P_2)
	{
		P_2 = default(RenderTargetDrawingContextProperties);
		ISkiaGpuRenderSession skiaGpuRenderSession = ((P_0.HasValue && _renderTarget is ISkiaGpuRenderTarget2 skiaGpuRenderTarget) ? skiaGpuRenderTarget.BeginRenderingSession(P_0.Value) : _renderTarget.BeginRenderingSession());
		return new DrawingContextImpl(new DrawingContextImpl.CreateInfo
		{
			GrContext = skiaGpuRenderSession.GrContext,
			Surface = skiaGpuRenderSession.SkSurface,
			Dpi = SkiaPlatform.DefaultDpi * skiaGpuRenderSession.ScaleFactor,
			ScaleDrawingToDpi = P_1,
			Gpu = _skiaGpu,
			CurrentSession = skiaGpuRenderSession
		}, skiaGpuRenderSession);
	}
}

