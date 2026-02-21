// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ImmutableBitmap
using System;
using System.IO;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;

internal class ImmutableBitmap : IDrawableBitmapImpl, IBitmapImpl, IDisposable, IReadableBitmapWithAlphaImpl, IReadableBitmapImpl
{
	private readonly SKImage _image;

	private readonly SKBitmap? _bitmap;

	private readonly Action? _customImageDispose;

	[CompilerGenerated]
	private readonly int _003CVersion_003Ek__BackingField = 1;

	public Vector Dpi { get; }

	public PixelSize PixelSize { get; }

	public PixelFormat? Format => _bitmap?.ColorType.ToAvalonia();

	public AlphaFormat? AlphaFormat => _bitmap?.AlphaType.ToAlphaFormat();

	public ImmutableBitmap(Stream P_0)
	{
		using SKManagedStream sKManagedStream = new SKManagedStream(P_0);
		using (SKData sKData = SKData.Create(sKManagedStream))
		{
			_bitmap = SKBitmap.Decode(sKData);
		}
		if (_bitmap == null)
		{
			throw new ArgumentException("Unable to load bitmap from provided data");
		}
		_bitmap.SetImmutable();
		_image = SKImage.FromBitmap(_bitmap);
		PixelSize = new PixelSize(_image.Width, _image.Height);
		Dpi = new Vector(96.0, 96.0);
	}

	public ImmutableBitmap(ImmutableBitmap P_0, PixelSize P_1, BitmapInterpolationMode P_2)
	{
		SKImageInfo sKImageInfo = new SKImageInfo(P_1.Width, P_1.Height, SKColorType.Bgra8888);
		_bitmap = new SKBitmap(sKImageInfo);
		P_0._image.ScalePixels(_bitmap.PeekPixels(), P_2.ToSKFilterQuality());
		_bitmap.SetImmutable();
		_image = SKImage.FromBitmap(_bitmap);
		PixelSize = new PixelSize(_image.Width, _image.Height);
		Dpi = new Vector(96.0, 96.0);
	}

	public ImmutableBitmap(Stream P_0, int P_1, bool P_2, BitmapInterpolationMode P_3)
	{
		using SKManagedStream sKManagedStream = new SKManagedStream(P_0);
		using SKData sKData = SKData.Create(sKManagedStream);
		using SKCodec sKCodec = SKCodec.Create(sKData);
		SKImageInfo info = sKCodec.Info;
		SKSizeI scaledDimensions = sKCodec.GetScaledDimensions(P_2 ? ((float)P_1 / (float)info.Width) : ((float)P_1 / (float)info.Height));
		SKImageInfo sKImageInfo = new SKImageInfo(scaledDimensions.Width, scaledDimensions.Height);
		_bitmap = SKBitmap.Decode(sKCodec, sKImageInfo);
		if (_bitmap == null)
		{
			throw new ArgumentException("Unable to load bitmap from provided data");
		}
		double num = (P_2 ? ((double)info.Height / (double)info.Width) : ((double)info.Width / (double)info.Height));
		SKImageInfo sKImageInfo2 = ((!P_2) ? new SKImageInfo((int)(num * (double)P_1), P_1) : new SKImageInfo(P_1, (int)(num * (double)P_1)));
		if (_bitmap.Width != sKImageInfo2.Width || _bitmap.Height != sKImageInfo2.Height)
		{
			SKBitmap bitmap = _bitmap.Resize(sKImageInfo2, P_3.ToSKFilterQuality());
			_bitmap.Dispose();
			_bitmap = bitmap;
		}
		_bitmap.SetImmutable();
		_image = SKImage.FromBitmap(_bitmap);
		if (_image == null)
		{
			throw new ArgumentException("Unable to load bitmap from provided data");
		}
		PixelSize = new PixelSize(_image.Width, _image.Height);
		Dpi = new Vector(96.0, 96.0);
	}

	public ImmutableBitmap(PixelSize P_0, Vector P_1, int P_2, PixelFormat P_3, AlphaFormat P_4, nint P_5)
	{
		using (SKBitmap sKBitmap = new SKBitmap())
		{
			sKBitmap.InstallPixels(new SKImageInfo(P_0.Width, P_0.Height, P_3.ToSkColorType(), P_4.ToSkAlphaType()), P_5, P_2);
			_bitmap = sKBitmap.Copy();
		}
		_bitmap.SetImmutable();
		_image = SKImage.FromBitmap(_bitmap);
		if (_image == null)
		{
			throw new ArgumentException("Unable to create bitmap from provided data");
		}
		PixelSize = P_0;
		Dpi = P_1;
	}

	public void Dispose()
	{
		if (_customImageDispose != null)
		{
			_customImageDispose();
		}
		else
		{
			_image.Dispose();
		}
		_bitmap?.Dispose();
	}

	public void Save(string P_0, int? P_1 = null)
	{
		ImageSavingHelper.SaveImage(_image, P_0, P_1);
	}

	public void Save(Stream P_0, int? P_1 = null)
	{
		ImageSavingHelper.SaveImage(_image, P_0, P_1);
	}

	public void Draw(DrawingContextImpl P_0, SKRect P_1, SKRect P_2, SKPaint P_3)
	{
		P_0.Canvas.DrawImage(_image, P_1, P_2, P_3);
	}

	public ILockedFramebuffer Lock()
	{
		if (_bitmap == null)
		{
			throw new NotSupportedException("A bitmap is needed for locking");
		}
		PixelFormat pixelFormat = _bitmap.ColorType.ToAvalonia() ?? throw new NotSupportedException($"Unsupported format {_bitmap.ColorType}");
		return new LockedFramebuffer(_bitmap.GetPixels(), PixelSize, _bitmap.RowBytes, Dpi, pixelFormat, null);
	}
}

