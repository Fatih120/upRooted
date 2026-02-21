// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SurfaceRenderTarget
using System;
using System.IO;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Reactive;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;

internal class SurfaceRenderTarget : IDrawingContextLayerImpl, IRenderTargetBitmapImpl, IBitmapImpl, IDisposable, IRenderTarget, IDrawableBitmapImpl
{
	private class SkiaSurfaceWrapper : ISkiaSurface, IDisposable
	{
		private SKSurface? _surface;

		public SKSurface Surface => _surface ?? throw new ObjectDisposedException("SkiaSurfaceWrapper");

		public bool CanBlit => false;

		public void Blit(SKCanvas P_0)
		{
			throw new NotSupportedException();
		}

		public SkiaSurfaceWrapper(SKSurface P_0)
		{
			_surface = P_0;
		}

		public void Dispose()
		{
			_surface?.Dispose();
			_surface = null;
		}
	}

	public struct CreateInfo
	{
		public int Width;

		public int Height;

		public Vector Dpi;

		public PixelFormat? Format;

		public bool DisableTextLcdRendering;

		public GRContext? GrContext;

		public ISkiaGpu? Gpu;

		public ISkiaGpuRenderSession? Session;

		public bool DisableManualFbo;
	}

	private readonly ISkiaSurface _surface;

	private readonly SKCanvas _canvas;

	private readonly bool _disableLcdRendering;

	private readonly GRContext? _grContext;

	private readonly ISkiaGpu? _gpu;

	[CompilerGenerated]
	private int _003CVersion_003Ek__BackingField;

	public bool IsCorrupted => _gpu?.IsLost ?? false;

	public Vector Dpi { get; }

	public PixelSize PixelSize { get; }

	public int Version
	{
		[CompilerGenerated]
		get
		{
			return _003CVersion_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CVersion_003Ek__BackingField = num;
		}
	}

	public bool CanBlit => true;

	public SurfaceRenderTarget(CreateInfo P_0)
	{
		Version = 1;
		base._002Ector();
		PixelSize = new PixelSize(P_0.Width, P_0.Height);
		Dpi = P_0.Dpi;
		_disableLcdRendering = P_0.DisableTextLcdRendering;
		_grContext = P_0.GrContext;
		_gpu = P_0.Gpu;
		ISkiaSurface skiaSurface = null;
		if (!P_0.DisableManualFbo)
		{
			skiaSurface = _gpu?.TryCreateSurface(PixelSize, P_0.Session);
		}
		if (skiaSurface == null)
		{
			SKSurface sKSurface = CreateSurface(P_0.GrContext, PixelSize.Width, PixelSize.Height, P_0.Format);
			if (sKSurface != null)
			{
				skiaSurface = new SkiaSurfaceWrapper(sKSurface);
			}
		}
		SKCanvas sKCanvas = skiaSurface?.Surface.Canvas;
		if (sKCanvas == null)
		{
			throw new InvalidOperationException("Failed to create Skia render target surface");
		}
		_surface = skiaSurface;
		_canvas = sKCanvas;
	}

	private static SKSurface? CreateSurface(GRContext? P_0, int P_1, int P_2, PixelFormat? P_3)
	{
		SKImageInfo sKImageInfo = MakeImageInfo(P_1, P_2, P_3);
		if (P_0 != null)
		{
			return SKSurface.Create(P_0, false, sKImageInfo, new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal));
		}
		return SKSurface.Create(sKImageInfo, new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal));
	}

	public void Dispose()
	{
		_canvas.Dispose();
		_surface.Dispose();
	}

	public IDrawingContextImpl CreateDrawingContext(bool P_0)
	{
		_canvas.RestoreToCount(-1);
		_canvas.ResetMatrix();
		return new DrawingContextImpl(new DrawingContextImpl.CreateInfo
		{
			Surface = _surface.Surface,
			Dpi = Dpi,
			ScaleDrawingToDpi = P_0,
			DisableSubpixelTextRendering = _disableLcdRendering,
			GrContext = _grContext,
			Gpu = _gpu
		}, Disposable.Create(delegate
		{
			Version++;
		}));
	}

	public void Save(string P_0, int? P_1 = null)
	{
		using SKImage sKImage = SnapshotImage();
		ImageSavingHelper.SaveImage(sKImage, P_0, P_1);
	}

	public void Save(Stream P_0, int? P_1 = null)
	{
		using SKImage sKImage = SnapshotImage();
		ImageSavingHelper.SaveImage(sKImage, P_0, P_1);
	}

	public void Blit(IDrawingContextImpl P_0)
	{
		DrawingContextImpl drawingContextImpl = (DrawingContextImpl)P_0;
		if (_surface.CanBlit)
		{
			_surface.Surface.Canvas.Flush();
			_surface.Blit(drawingContextImpl.Canvas);
			return;
		}
		SKMatrix totalMatrix = drawingContextImpl.Canvas.TotalMatrix;
		drawingContextImpl.Canvas.ResetMatrix();
		_surface.Surface.Draw(drawingContextImpl.Canvas, 0f, 0f, null);
		drawingContextImpl.Canvas.SetMatrix(totalMatrix);
	}

	public void Draw(DrawingContextImpl P_0, SKRect P_1, SKRect P_2, SKPaint P_3)
	{
		using SKImage sKImage = SnapshotImage();
		P_0.Canvas.DrawImage(sKImage, P_1, P_2, P_3);
	}

	public SKImage SnapshotImage()
	{
		return _surface.Surface.Snapshot();
	}

	private static SKImageInfo MakeImageInfo(int P_0, int P_1, PixelFormat? P_2)
	{
		SKColorType sKColorType = PixelFormatHelper.ResolveColorType(P_2);
		return new SKImageInfo(Math.Max(P_0, 1), Math.Max(P_1, 1), sKColorType, SKAlphaType.Premul);
	}
}

