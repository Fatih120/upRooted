// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.PictureRenderTarget
using System;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Reactive;
using Avalonia.Skia;
using SkiaSharp;

internal class PictureRenderTarget : IDisposable
{
	private readonly ISkiaGpu? _gpu;

	private readonly GRContext? _grContext;

	private readonly Vector _dpi;

	private SKPicture? _picture;

	public PictureRenderTarget(ISkiaGpu? P_0, GRContext? P_1, Vector P_2)
	{
		_gpu = P_0;
		_grContext = P_1;
		_dpi = P_2;
	}

	public SKPicture GetPicture()
	{
		SKPicture? result = _picture ?? throw new InvalidOperationException();
		_picture = null;
		return result;
	}

	public IDrawingContextImpl CreateDrawingContext(Size P_0, bool P_1 = true)
	{
		if (P_1)
		{
			P_0 *= _dpi / 96.0;
		}
		SKPictureRecorder recorder = new SKPictureRecorder();
		SKCanvas canvas = recorder.BeginRecording(new SKRect(0f, 0f, (float)P_0.Width, (float)P_0.Height));
		canvas.RestoreToCount(-1);
		canvas.ResetMatrix();
		return new DrawingContextImpl(new DrawingContextImpl.CreateInfo
		{
			Canvas = canvas,
			ScaleDrawingToDpi = P_1,
			Dpi = _dpi,
			DisableSubpixelTextRendering = true,
			GrContext = _grContext,
			Gpu = _gpu
		}, Disposable.Create(delegate
		{
			_picture = recorder.EndRecording();
			canvas.Dispose();
			recorder.Dispose();
		}));
	}

	public void Dispose()
	{
		_picture?.Dispose();
	}
}

