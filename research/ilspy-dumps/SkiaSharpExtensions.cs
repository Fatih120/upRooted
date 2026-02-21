// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SkiaSharpExtensions
using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SkiaSharp;

public static class SkiaSharpExtensions
{
	public static SKFilterQuality ToSKFilterQuality(this BitmapInterpolationMode P_0)
	{
		switch (P_0)
		{
		case BitmapInterpolationMode.Unspecified:
		case BitmapInterpolationMode.LowQuality:
			return SKFilterQuality.Low;
		case BitmapInterpolationMode.MediumQuality:
			return SKFilterQuality.Medium;
		case BitmapInterpolationMode.HighQuality:
			return SKFilterQuality.High;
		case BitmapInterpolationMode.None:
			return SKFilterQuality.None;
		default:
			throw new ArgumentOutOfRangeException("interpolationMode", P_0, null);
		}
	}

	public static SKBlendMode ToSKBlendMode(this BitmapBlendingMode P_0)
	{
		return P_0 switch
		{
			BitmapBlendingMode.Unspecified => SKBlendMode.SrcOver, 
			BitmapBlendingMode.SourceOver => SKBlendMode.SrcOver, 
			BitmapBlendingMode.Source => SKBlendMode.Src, 
			BitmapBlendingMode.SourceIn => SKBlendMode.SrcIn, 
			BitmapBlendingMode.SourceOut => SKBlendMode.SrcOut, 
			BitmapBlendingMode.SourceAtop => SKBlendMode.SrcATop, 
			BitmapBlendingMode.Destination => SKBlendMode.Dst, 
			BitmapBlendingMode.DestinationIn => SKBlendMode.DstIn, 
			BitmapBlendingMode.DestinationOut => SKBlendMode.DstOut, 
			BitmapBlendingMode.DestinationOver => SKBlendMode.DstOver, 
			BitmapBlendingMode.DestinationAtop => SKBlendMode.DstATop, 
			BitmapBlendingMode.Xor => SKBlendMode.Xor, 
			BitmapBlendingMode.Plus => SKBlendMode.Plus, 
			BitmapBlendingMode.Screen => SKBlendMode.Screen, 
			BitmapBlendingMode.Overlay => SKBlendMode.Overlay, 
			BitmapBlendingMode.Darken => SKBlendMode.Darken, 
			BitmapBlendingMode.Lighten => SKBlendMode.Lighten, 
			BitmapBlendingMode.ColorDodge => SKBlendMode.ColorDodge, 
			BitmapBlendingMode.ColorBurn => SKBlendMode.ColorBurn, 
			BitmapBlendingMode.HardLight => SKBlendMode.HardLight, 
			BitmapBlendingMode.SoftLight => SKBlendMode.SoftLight, 
			BitmapBlendingMode.Difference => SKBlendMode.Difference, 
			BitmapBlendingMode.Exclusion => SKBlendMode.Exclusion, 
			BitmapBlendingMode.Multiply => SKBlendMode.Multiply, 
			BitmapBlendingMode.Hue => SKBlendMode.Hue, 
			BitmapBlendingMode.Saturation => SKBlendMode.Saturation, 
			BitmapBlendingMode.Color => SKBlendMode.Color, 
			BitmapBlendingMode.Luminosity => SKBlendMode.Luminosity, 
			_ => throw new ArgumentOutOfRangeException("blendingMode", P_0, null), 
		};
	}

	public static SKPoint ToSKPoint(this Point P_0)
	{
		return new SKPoint((float)P_0.X, (float)P_0.Y);
	}

	public static SKPoint ToSKPoint(this Vector P_0)
	{
		return new SKPoint((float)P_0.X, (float)P_0.Y);
	}

	public static SKRect ToSKRect(this Rect P_0)
	{
		return new SKRect((float)P_0.X, (float)P_0.Y, (float)P_0.Right, (float)P_0.Bottom);
	}

	public static Rect ToAvaloniaRect(this SKRect P_0)
	{
		return new Rect(P_0.Left, P_0.Top, P_0.Right - P_0.Left, P_0.Bottom - P_0.Top);
	}

	internal static LtrbPixelRect ToAvaloniaLtrbPixelRect(this SKRectI P_0)
	{
		return new LtrbPixelRect(P_0.Left, P_0.Top, P_0.Right, P_0.Bottom);
	}

	public static SKMatrix ToSKMatrix(this Matrix P_0)
	{
		return new SKMatrix
		{
			ScaleX = (float)P_0.M11,
			SkewX = (float)P_0.M21,
			TransX = (float)P_0.M31,
			SkewY = (float)P_0.M12,
			ScaleY = (float)P_0.M22,
			TransY = (float)P_0.M32,
			Persp0 = (float)P_0.M13,
			Persp1 = (float)P_0.M23,
			Persp2 = (float)P_0.M33
		};
	}

	internal static Matrix ToAvaloniaMatrix(this SKMatrix P_0)
	{
		return new Matrix(P_0.ScaleX, P_0.SkewY, P_0.Persp0, P_0.SkewX, P_0.ScaleY, P_0.Persp1, P_0.TransX, P_0.TransY, P_0.Persp2);
	}

	public static SKColor ToSKColor(this Color P_0)
	{
		return new SKColor(P_0.R, P_0.G, P_0.B, P_0.A);
	}

	public static SKColorType ToSkColorType(this PixelFormat P_0)
	{
		if (P_0 == PixelFormat.Rgb565)
		{
			return SKColorType.Rgb565;
		}
		if (P_0 == PixelFormat.Bgra8888)
		{
			return SKColorType.Bgra8888;
		}
		if (P_0 == PixelFormat.Rgba8888)
		{
			return SKColorType.Rgba8888;
		}
		if (P_0 == PixelFormat.Rgb32)
		{
			return SKColorType.Rgb888x;
		}
		PixelFormat pixelFormat = P_0;
		throw new ArgumentException("Unknown pixel format: " + pixelFormat.ToString());
	}

	public static PixelFormat? ToAvalonia(this SKColorType P_0)
	{
		return P_0 switch
		{
			SKColorType.Rgb565 => PixelFormats.Rgb565, 
			SKColorType.Bgra8888 => PixelFormats.Bgra8888, 
			SKColorType.Rgba8888 => PixelFormats.Rgba8888, 
			SKColorType.Rgb888x => PixelFormats.Rgb32, 
			_ => null, 
		};
	}

	public static PixelFormat ToPixelFormat(this SKColorType P_0)
	{
		return P_0 switch
		{
			SKColorType.Rgb565 => PixelFormat.Rgb565, 
			SKColorType.Bgra8888 => PixelFormat.Bgra8888, 
			SKColorType.Rgba8888 => PixelFormat.Rgba8888, 
			_ => throw new ArgumentException("Unknown pixel format: " + P_0), 
		};
	}

	public static SKAlphaType ToSkAlphaType(this AlphaFormat P_0)
	{
		return P_0 switch
		{
			AlphaFormat.Premul => SKAlphaType.Premul, 
			AlphaFormat.Unpremul => SKAlphaType.Unpremul, 
			AlphaFormat.Opaque => SKAlphaType.Opaque, 
			_ => throw new ArgumentException($"Unknown alpha format: {P_0}"), 
		};
	}

	public static AlphaFormat ToAlphaFormat(this SKAlphaType P_0)
	{
		return P_0 switch
		{
			SKAlphaType.Premul => AlphaFormat.Premul, 
			SKAlphaType.Unpremul => AlphaFormat.Unpremul, 
			SKAlphaType.Opaque => AlphaFormat.Opaque, 
			_ => throw new ArgumentException($"Unknown alpha format: {P_0}"), 
		};
	}

	public static SKShaderTileMode ToSKShaderTileMode(this GradientSpreadMethod P_0)
	{
		return P_0 switch
		{
			GradientSpreadMethod.Reflect => SKShaderTileMode.Mirror, 
			GradientSpreadMethod.Repeat => SKShaderTileMode.Repeat, 
			_ => SKShaderTileMode.Clamp, 
		};
	}

	public static SKStrokeCap ToSKStrokeCap(this PenLineCap P_0)
	{
		return P_0 switch
		{
			PenLineCap.Round => SKStrokeCap.Round, 
			PenLineCap.Square => SKStrokeCap.Square, 
			_ => SKStrokeCap.Butt, 
		};
	}

	public static SKStrokeJoin ToSKStrokeJoin(this PenLineJoin P_0)
	{
		return P_0 switch
		{
			PenLineJoin.Bevel => SKStrokeJoin.Bevel, 
			PenLineJoin.Round => SKStrokeJoin.Round, 
			_ => SKStrokeJoin.Miter, 
		};
	}

	public static FontStyle ToAvalonia(this SKFontStyleSlant P_0)
	{
		return P_0 switch
		{
			SKFontStyleSlant.Upright => FontStyle.Normal, 
			SKFontStyleSlant.Italic => FontStyle.Italic, 
			SKFontStyleSlant.Oblique => FontStyle.Oblique, 
			_ => throw new ArgumentOutOfRangeException("slant", P_0, null), 
		};
	}

	public static SKFontStyleSlant ToSkia(this FontStyle P_0)
	{
		return P_0 switch
		{
			FontStyle.Normal => SKFontStyleSlant.Upright, 
			FontStyle.Italic => SKFontStyleSlant.Italic, 
			FontStyle.Oblique => SKFontStyleSlant.Oblique, 
			_ => throw new ArgumentOutOfRangeException("style", P_0, null), 
		};
	}

	[return: NotNullIfNotNull("src")]
	public static SKPath? Clone(this SKPath? P_0)
	{
		if (P_0 == null)
		{
			return null;
		}
		return new SKPath(P_0);
	}
}

