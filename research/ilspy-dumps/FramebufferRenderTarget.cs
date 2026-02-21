// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.FramebufferRenderTarget
using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls.Platform.Surfaces;
using Avalonia.Platform;
using Avalonia.Reactive;
using Avalonia.Skia;
using SkiaSharp;

internal class FramebufferRenderTarget : IRenderTarget2, IRenderTarget, IDisposable
{
	private class PixelFormatConversionShim : IDisposable
	{
		private readonly SKBitmap _bitmap;

		private readonly SKImageInfo _destinationInfo;

		private readonly nint _framebufferAddress;

		public SKSurface Surface { get; }

		public IDisposable SurfaceCopyHandler { get; }

		public PixelFormatConversionShim(SKImageInfo P_0, nint P_1)
		{
			_destinationInfo = P_0;
			_framebufferAddress = P_1;
			_bitmap = new SKBitmap(P_0.Width, P_0.Height);
			if (!_bitmap.CanCopyTo(P_0.ColorType))
			{
				SKColorType colorType = _bitmap.ColorType;
				_bitmap.Dispose();
				throw new Exception($"Unable to create pixel format shim for conversion from {colorType} to {P_0.ColorType}");
			}
			Surface = SKSurface.Create(_bitmap.Info, _bitmap.GetPixels(), _bitmap.RowBytes, new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal));
			if (Surface == null)
			{
				SKColorType colorType = _bitmap.ColorType;
				_bitmap.Dispose();
				throw new Exception($"Unable to create pixel format shim surface for conversion from {colorType} to {P_0.ColorType}");
			}
			SurfaceCopyHandler = Disposable.Create(CopySurface);
		}

		public void Dispose()
		{
			Surface.Dispose();
			_bitmap.Dispose();
		}

		private void CopySurface()
		{
			using SKImage sKImage = Surface.Snapshot();
			sKImage.ReadPixels(_destinationInfo, _framebufferAddress, _destinationInfo.RowBytes, 0, 0, SKImageCachingHint.Disallow);
		}
	}

	private SKImageInfo _currentImageInfo;

	private nint _currentFramebufferAddress;

	private SKSurface? _framebufferSurface;

	private PixelFormatConversionShim? _conversionShim;

	private IDisposable? _preFramebufferCopyHandler;

	private IFramebufferRenderTarget? _renderTarget;

	private IFramebufferRenderTargetWithProperties? _renderTargetWithProperties;

	private bool _hadConversionShim;

	public RenderTargetProperties Properties => new RenderTargetProperties
	{
		RetainsPreviousFrameContents = (!_hadConversionShim && (_renderTargetWithProperties?.RetainsFrameContents ?? false)),
		IsSuitableForDirectRendering = true
	};

	public bool IsCorrupted => false;

	public FramebufferRenderTarget(IFramebufferPlatformSurface P_0)
	{
		_renderTarget = P_0.CreateFramebufferRenderTarget();
		_renderTargetWithProperties = _renderTarget as IFramebufferRenderTargetWithProperties;
	}

	public void Dispose()
	{
		_renderTarget?.Dispose();
		_renderTarget = null;
		_renderTargetWithProperties = null;
		FreeSurface();
	}

	public IDrawingContextImpl CreateDrawingContext(bool P_0)
	{
		RenderTargetDrawingContextProperties renderTargetDrawingContextProperties;
		return CreateDrawingContextCore(P_0, out renderTargetDrawingContextProperties);
	}

	public IDrawingContextImpl CreateDrawingContext(PixelSize P_0, out RenderTargetDrawingContextProperties P_1)
	{
		return CreateDrawingContextCore(false, out P_1);
	}

	private IDrawingContextImpl CreateDrawingContextCore(bool P_0, out RenderTargetDrawingContextProperties P_1)
	{
		if (_renderTarget == null)
		{
			throw new ObjectDisposedException("FramebufferRenderTarget");
		}
		FramebufferLockProperties framebufferLockProperties = default(FramebufferLockProperties);
		ILockedFramebuffer lockedFramebuffer = _renderTargetWithProperties?.Lock(out framebufferLockProperties) ?? _renderTarget.Lock();
		SKImageInfo sKImageInfo = new SKImageInfo(lockedFramebuffer.Size.Width, lockedFramebuffer.Size.Height, lockedFramebuffer.Format.ToSkColorType(), (lockedFramebuffer.Format == PixelFormat.Rgb565) ? SKAlphaType.Opaque : SKAlphaType.Premul);
		CreateSurface(sKImageInfo, lockedFramebuffer);
		_hadConversionShim |= _conversionShim != null;
		SKCanvas canvas = _framebufferSurface.Canvas;
		canvas.RestoreToCount(-1);
		canvas.Save();
		canvas.ResetMatrix();
		DrawingContextImpl.CreateInfo obj = new DrawingContextImpl.CreateInfo
		{
			Surface = _framebufferSurface,
			Dpi = lockedFramebuffer.Dpi,
			ScaleDrawingToDpi = P_0
		};
		P_1 = new RenderTargetDrawingContextProperties
		{
			PreviousFrameIsRetained = (!_hadConversionShim && framebufferLockProperties.PreviousFrameIsRetained)
		};
		return new DrawingContextImpl(obj, _preFramebufferCopyHandler, canvas, lockedFramebuffer);
	}

	private static bool AreImageInfosCompatible(SKImageInfo P_0, SKImageInfo P_1)
	{
		if (P_0.Width == P_1.Width && P_0.Height == P_1.Height)
		{
			return P_0.ColorType == P_1.ColorType;
		}
		return false;
	}

	[MemberNotNull("_framebufferSurface")]
	private void CreateSurface(SKImageInfo P_0, ILockedFramebuffer P_1)
	{
		if (_framebufferSurface == null || !AreImageInfosCompatible(_currentImageInfo, P_0) || _currentFramebufferAddress != P_1.Address)
		{
			FreeSurface();
			_currentFramebufferAddress = P_1.Address;
			SKSurface sKSurface = SKSurface.Create(P_0, _currentFramebufferAddress, P_1.RowBytes, new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal));
			if (sKSurface == null)
			{
				_conversionShim = new PixelFormatConversionShim(P_0, P_1.Address);
				_preFramebufferCopyHandler = _conversionShim.SurfaceCopyHandler;
				sKSurface = _conversionShim.Surface;
			}
			_framebufferSurface = sKSurface ?? throw new Exception("Unable to create a surface for pixel format " + P_1.Format.ToString() + " or pixel format translator");
			_currentImageInfo = P_0;
		}
	}

	private void FreeSurface()
	{
		_conversionShim?.Dispose();
		_conversionShim = null;
		_preFramebufferCopyHandler = null;
		_framebufferSurface?.Dispose();
		_framebufferSurface = null;
		_currentFramebufferAddress = IntPtr.Zero;
	}
}

