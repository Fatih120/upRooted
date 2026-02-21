// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SkiaExtensions
using System;
using System.ComponentModel;
using SkiaSharp;

public static class SkiaExtensions
{
	public static int GetBytesPerPixel(this SKColorType P_0)
	{
		return P_0 switch
		{
			SKColorType.Unknown => 0, 
			SKColorType.Alpha8 => 1, 
			SKColorType.Gray8 => 1, 
			SKColorType.Rgb565 => 2, 
			SKColorType.Argb4444 => 2, 
			SKColorType.Rg88 => 2, 
			SKColorType.Alpha16 => 2, 
			SKColorType.AlphaF16 => 2, 
			SKColorType.Bgra8888 => 4, 
			SKColorType.Bgra1010102 => 4, 
			SKColorType.Bgr101010x => 4, 
			SKColorType.Rgba8888 => 4, 
			SKColorType.Rgb888x => 4, 
			SKColorType.Rgba1010102 => 4, 
			SKColorType.Rgb101010x => 4, 
			SKColorType.Rg1616 => 4, 
			SKColorType.RgF16 => 4, 
			SKColorType.RgbaF16Clamped => 8, 
			SKColorType.RgbaF16 => 8, 
			SKColorType.Rgba16161616 => 8, 
			SKColorType.RgbaF32 => 16, 
			_ => throw new ArgumentOutOfRangeException("colorType"), 
		};
	}

	internal static SKColorTypeNative ToNative(this SKColorType P_0)
	{
		return P_0 switch
		{
			SKColorType.Unknown => SKColorTypeNative.Unknown, 
			SKColorType.Alpha8 => SKColorTypeNative.Alpha8, 
			SKColorType.Rgb565 => SKColorTypeNative.Rgb565, 
			SKColorType.Argb4444 => SKColorTypeNative.Argb4444, 
			SKColorType.Rgba8888 => SKColorTypeNative.Rgba8888, 
			SKColorType.Rgb888x => SKColorTypeNative.Rgb888x, 
			SKColorType.Bgra8888 => SKColorTypeNative.Bgra8888, 
			SKColorType.Rgba1010102 => SKColorTypeNative.Rgba1010102, 
			SKColorType.Rgb101010x => SKColorTypeNative.Rgb101010x, 
			SKColorType.Gray8 => SKColorTypeNative.Gray8, 
			SKColorType.RgbaF16Clamped => SKColorTypeNative.RgbaF16Norm, 
			SKColorType.RgbaF16 => SKColorTypeNative.RgbaF16, 
			SKColorType.RgbaF32 => SKColorTypeNative.RgbaF32, 
			SKColorType.Rg88 => SKColorTypeNative.R8g8Unorm, 
			SKColorType.AlphaF16 => SKColorTypeNative.A16Float, 
			SKColorType.RgF16 => SKColorTypeNative.R16g16Float, 
			SKColorType.Alpha16 => SKColorTypeNative.A16Unorm, 
			SKColorType.Rg1616 => SKColorTypeNative.R16g16Unorm, 
			SKColorType.Rgba16161616 => SKColorTypeNative.R16g16b16a16Unorm, 
			SKColorType.Bgra1010102 => SKColorTypeNative.Bgra1010102, 
			SKColorType.Bgr101010x => SKColorTypeNative.Bgr101010x, 
			_ => throw new ArgumentOutOfRangeException("colorType"), 
		};
	}

	internal static SKColorType FromNative(this SKColorTypeNative P_0)
	{
		return P_0 switch
		{
			SKColorTypeNative.Unknown => SKColorType.Unknown, 
			SKColorTypeNative.Alpha8 => SKColorType.Alpha8, 
			SKColorTypeNative.Rgb565 => SKColorType.Rgb565, 
			SKColorTypeNative.Argb4444 => SKColorType.Argb4444, 
			SKColorTypeNative.Rgba8888 => SKColorType.Rgba8888, 
			SKColorTypeNative.Rgb888x => SKColorType.Rgb888x, 
			SKColorTypeNative.Bgra8888 => SKColorType.Bgra8888, 
			SKColorTypeNative.Rgba1010102 => SKColorType.Rgba1010102, 
			SKColorTypeNative.Rgb101010x => SKColorType.Rgb101010x, 
			SKColorTypeNative.Gray8 => SKColorType.Gray8, 
			SKColorTypeNative.RgbaF16Norm => SKColorType.RgbaF16Clamped, 
			SKColorTypeNative.RgbaF16 => SKColorType.RgbaF16, 
			SKColorTypeNative.RgbaF32 => SKColorType.RgbaF32, 
			SKColorTypeNative.R8g8Unorm => SKColorType.Rg88, 
			SKColorTypeNative.A16Float => SKColorType.AlphaF16, 
			SKColorTypeNative.R16g16Float => SKColorType.RgF16, 
			SKColorTypeNative.A16Unorm => SKColorType.Alpha16, 
			SKColorTypeNative.R16g16Unorm => SKColorType.Rg1616, 
			SKColorTypeNative.R16g16b16a16Unorm => SKColorType.Rgba16161616, 
			SKColorTypeNative.Bgra1010102 => SKColorType.Bgra1010102, 
			SKColorTypeNative.Bgr101010x => SKColorType.Bgr101010x, 
			_ => throw new ArgumentOutOfRangeException("colorType"), 
		};
	}

	public static uint ToGlSizedFormat(this SKColorType P_0)
	{
		return P_0 switch
		{
			SKColorType.Unknown => 0u, 
			SKColorType.Alpha8 => 32828u, 
			SKColorType.Gray8 => 32832u, 
			SKColorType.Rgb565 => 36194u, 
			SKColorType.Argb4444 => 32854u, 
			SKColorType.Rgba8888 => 32856u, 
			SKColorType.Rgb888x => 32849u, 
			SKColorType.Bgra8888 => 37793u, 
			SKColorType.Rgba1010102 => 32857u, 
			SKColorType.AlphaF16 => 33325u, 
			SKColorType.RgbaF16 => 34842u, 
			SKColorType.RgbaF16Clamped => 34842u, 
			SKColorType.Alpha16 => 33322u, 
			SKColorType.Rg1616 => 33324u, 
			SKColorType.Rgba16161616 => 32859u, 
			SKColorType.RgF16 => 33327u, 
			SKColorType.Rg88 => 33323u, 
			SKColorType.Rgb101010x => 0u, 
			SKColorType.RgbaF32 => 0u, 
			_ => throw new ArgumentOutOfRangeException("colorType"), 
		};
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use SKColorType instead.")]
	public static uint ToGlSizedFormat(this GRPixelConfig P_0)
	{
		return P_0 switch
		{
			GRPixelConfig.Unknown => 0u, 
			GRPixelConfig.Alpha8 => 32828u, 
			GRPixelConfig.Alpha8AsAlpha => 32828u, 
			GRPixelConfig.Alpha8AsRed => 32828u, 
			GRPixelConfig.Gray8 => 32832u, 
			GRPixelConfig.Gray8AsLum => 32832u, 
			GRPixelConfig.Gray8AsRed => 32832u, 
			GRPixelConfig.Rgb565 => 36194u, 
			GRPixelConfig.Rgba4444 => 32854u, 
			GRPixelConfig.Rgba8888 => 32856u, 
			GRPixelConfig.Rgb888 => 32849u, 
			GRPixelConfig.Rgb888x => 32856u, 
			GRPixelConfig.Rg88 => 33323u, 
			GRPixelConfig.Bgra8888 => 37793u, 
			GRPixelConfig.Srgba8888 => 35907u, 
			GRPixelConfig.Rgba1010102 => 32857u, 
			GRPixelConfig.AlphaHalf => 33325u, 
			GRPixelConfig.AlphaHalfAsLum => 34846u, 
			GRPixelConfig.AlphaHalfAsRed => 33325u, 
			GRPixelConfig.RgbaHalf => 34842u, 
			GRPixelConfig.RgbaHalfClamped => 34842u, 
			GRPixelConfig.RgbEtc1 => 36196u, 
			GRPixelConfig.Alpha16 => 33322u, 
			GRPixelConfig.Rg1616 => 33324u, 
			GRPixelConfig.Rgba16161616 => 32859u, 
			GRPixelConfig.RgHalf => 33327u, 
			_ => throw new ArgumentOutOfRangeException("config"), 
		};
	}
}

