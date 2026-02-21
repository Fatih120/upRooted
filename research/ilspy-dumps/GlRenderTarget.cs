// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GlRenderTarget
using System;
using Avalonia;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Surfaces;
using Avalonia.Skia;
using SkiaSharp;

internal class GlRenderTarget : ISkiaGpuRenderTarget2, ISkiaGpuRenderTarget, IDisposable
{
	private class GlGpuSession : ISkiaGpuRenderSession, IDisposable
	{
		private readonly GRBackendRenderTarget _backendRenderTarget;

		private readonly SKSurface _surface;

		private readonly IGlPlatformSurfaceRenderingSession _glSession;

		public GRSurfaceOrigin SurfaceOrigin { get; }

		public GRContext GrContext { get; }

		public SKSurface SkSurface => _surface;

		public double ScaleFactor => _glSession.Scaling;

		public GlGpuSession(GRContext P_0, GRBackendRenderTarget P_1, SKSurface P_2, IGlPlatformSurfaceRenderingSession P_3)
		{
			GrContext = P_0;
			_backendRenderTarget = P_1;
			_surface = P_2;
			_glSession = P_3;
			SurfaceOrigin = ((!P_3.IsYFlipped) ? GRSurfaceOrigin.BottomLeft : GRSurfaceOrigin.TopLeft);
		}

		public void Dispose()
		{
			_surface.Canvas.Flush();
			_surface.Dispose();
			_backendRenderTarget.Dispose();
			GrContext.Flush();
			_glSession.Dispose();
		}
	}

	private readonly GRContext _grContext;

	private IGlPlatformSurfaceRenderTarget _surface;

	private static readonly SKSurfaceProperties _surfaceProperties = new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal);

	public bool IsCorrupted => (_surface as IGlPlatformSurfaceRenderTargetWithCorruptionInfo)?.IsCorrupted ?? false;

	public GlRenderTarget(GRContext P_0, IGlContext P_1, IGlPlatformSurface P_2)
	{
		_grContext = P_0;
		using (P_1.EnsureCurrent())
		{
			_surface = P_2.CreateGlRenderTarget(P_1);
		}
	}

	public void Dispose()
	{
		_surface.Dispose();
	}

	public ISkiaGpuRenderSession BeginRenderingSession(PixelSize P_0)
	{
		return BeginRenderingSessionCore(P_0);
	}

	public ISkiaGpuRenderSession BeginRenderingSession()
	{
		return BeginRenderingSessionCore(null);
	}

	private ISkiaGpuRenderSession BeginRenderingSessionCore(PixelSize? P_0)
	{
		IGlPlatformSurfaceRenderingSession glPlatformSurfaceRenderingSession = ((P_0.HasValue && _surface is IGlPlatformSurfaceRenderTarget2 glPlatformSurfaceRenderTarget) ? glPlatformSurfaceRenderTarget.BeginDraw(P_0.Value) : _surface.BeginDraw());
		bool flag = false;
		try
		{
			IGlContext context = glPlatformSurfaceRenderingSession.Context;
			context.GlInterface.GetIntegerv(36006, out var num);
			PixelSize size = glPlatformSurfaceRenderingSession.Size;
			SKColorType sKColorType = SKColorType.Rgba8888;
			double scaling = glPlatformSurfaceRenderingSession.Scaling;
			if (size.Width <= 0 || size.Height <= 0 || scaling < 0.0)
			{
				glPlatformSurfaceRenderingSession.Dispose();
				throw new InvalidOperationException($"Can't create drawing context for surface with {size} size and {scaling} scaling");
			}
			lock (_grContext)
			{
				_grContext.ResetContext();
				int num2 = context.SampleCount;
				int maxSurfaceSampleCount = _grContext.GetMaxSurfaceSampleCount(sKColorType);
				if (num2 > maxSurfaceSampleCount)
				{
					num2 = maxSurfaceSampleCount;
				}
				GRBackendRenderTarget gRBackendRenderTarget = new GRBackendRenderTarget(glInfo: new GRGlFramebufferInfo((uint)num, sKColorType.ToGlSizedFormat()), width: size.Width, height: size.Height, sampleCount: num2, stencilBits: context.StencilSize);
				SKSurface sKSurface = SKSurface.Create(_grContext, gRBackendRenderTarget, (!glPlatformSurfaceRenderingSession.IsYFlipped) ? GRSurfaceOrigin.BottomLeft : GRSurfaceOrigin.TopLeft, sKColorType, _surfaceProperties);
				flag = true;
				return new GlGpuSession(_grContext, gRBackendRenderTarget, sKSurface, glPlatformSurfaceRenderingSession);
			}
		}
		finally
		{
			if (!flag)
			{
				glPlatformSurfaceRenderingSession.Dispose();
			}
		}
	}
}

