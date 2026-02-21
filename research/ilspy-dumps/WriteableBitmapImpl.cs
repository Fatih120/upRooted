// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.WriteableBitmapImpl
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Platform.Internal;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;

internal class WriteableBitmapImpl : IWriteableBitmapImpl, IBitmapImpl, IDisposable, IReadableBitmapWithAlphaImpl, IReadableBitmapImpl, IDrawableBitmapImpl
{
	private class BitmapFramebuffer : ILockedFramebuffer, IDisposable
	{
		private WriteableBitmapImpl _parent;

		private SKBitmap _bitmap;

		public nint Address => _bitmap.GetPixels();

		public PixelSize Size => new PixelSize(_bitmap.Width, _bitmap.Height);

		public int RowBytes => _bitmap.RowBytes;

		public Vector Dpi => _parent.Dpi;

		public PixelFormat Format => _bitmap.ColorType.ToPixelFormat();

		public BitmapFramebuffer(WriteableBitmapImpl P_0, SKBitmap P_1)
		{
			_parent = P_0;
			_bitmap = P_1;
			Monitor.Enter(P_0._lock);
		}

		public void Dispose()
		{
			_bitmap.NotifyPixelsChanged();
			_parent.Version++;
			_parent._imageValid = false;
			Monitor.Exit(_parent._lock);
			_bitmap = null;
			_parent = null;
		}
	}

	private static readonly SKBitmapReleaseDelegate s_releaseDelegate = ReleaseProc;

	private SKBitmap _bitmap;

	private SKImage? _image;

	private bool _imageValid;

	private readonly object _lock = new object();

	[CompilerGenerated]
	private int _003CVersion_003Ek__BackingField;

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

	public PixelFormat? Format => _bitmap.ColorType.ToAvalonia();

	public AlphaFormat? AlphaFormat => _bitmap.AlphaType.ToAlphaFormat();

	public WriteableBitmapImpl(PixelSize P_0, Vector P_1, PixelFormat P_2, AlphaFormat P_3)
	{
		Version = 1;
		base._002Ector();
		PixelSize = P_0;
		Dpi = P_1;
		SKColorType sKColorType = P_2.ToSkColorType();
		SKAlphaType sKAlphaType = P_3.ToSkAlphaType();
		_bitmap = new SKBitmap();
		SKImageInfo sKImageInfo = new SKImageInfo(P_0.Width, P_0.Height, sKColorType, sKAlphaType);
		UnmanagedBlob unmanagedBlob = new UnmanagedBlob(sKImageInfo.BytesSize);
		_bitmap.InstallPixels(sKImageInfo, unmanagedBlob.Address, sKImageInfo.RowBytes, s_releaseDelegate, unmanagedBlob);
		_bitmap.Erase(SKColor.Empty);
	}

	public void Draw(DrawingContextImpl P_0, SKRect P_1, SKRect P_2, SKPaint P_3)
	{
		lock (_lock)
		{
			if (_image == null || !_imageValid)
			{
				_image?.Dispose();
				_image = null;
				_image = GetSnapshot();
				_imageValid = true;
			}
			P_0.Canvas.DrawImage(_image, P_1, P_2, P_3);
		}
	}

	public virtual void Dispose()
	{
		lock (_lock)
		{
			_image?.Dispose();
			_image = null;
			_bitmap.Dispose();
			_bitmap = null;
		}
	}

	public void Save(Stream P_0, int? P_1 = null)
	{
		using SKImage sKImage = GetSnapshot();
		ImageSavingHelper.SaveImage(sKImage, P_0, P_1);
	}

	public void Save(string P_0, int? P_1 = null)
	{
		using SKImage sKImage = GetSnapshot();
		ImageSavingHelper.SaveImage(sKImage, P_0, P_1);
	}

	public ILockedFramebuffer Lock()
	{
		return new BitmapFramebuffer(this, _bitmap);
	}

	public SKImage GetSnapshot()
	{
		lock (_lock)
		{
			return SKImage.FromPixels(_bitmap.Info, _bitmap.GetPixels(), _bitmap.RowBytes);
		}
	}

	private static void ReleaseProc(nint address, object ctx)
	{
		((UnmanagedBlob)ctx).Dispose();
	}
}
