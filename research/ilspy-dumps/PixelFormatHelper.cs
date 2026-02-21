// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.Helpers.PixelFormatHelper
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

public static class PixelFormatHelper
{
	public static SKColorType ResolveColorType(PixelFormat? P_0)
	{
		SKColorType result = P_0?.ToSkColorType() ?? SKImageInfo.PlatformColorType;
		if (false)
		{
		}
		return result;
	}
}

