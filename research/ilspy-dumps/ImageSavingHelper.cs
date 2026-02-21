// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.Helpers.ImageSavingHelper
using System;
using System.IO;
using SkiaSharp;

public static class ImageSavingHelper
{
	public static void SaveImage(SKImage P_0, string P_1, int? P_2 = null)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("image");
		}
		if (P_1 == null)
		{
			throw new ArgumentNullException("fileName");
		}
		using FileStream fileStream = File.Create(P_1);
		SaveImage(P_0, fileStream, P_2);
	}

	public static void SaveImage(SKImage P_0, Stream P_1, int? P_2 = null)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("image");
		}
		if (P_1 == null)
		{
			throw new ArgumentNullException("stream");
		}
		if (!P_2.HasValue)
		{
			using (SKData sKData = P_0.Encode())
			{
				sKData.SaveTo(P_1);
				return;
			}
		}
		using SKData sKData2 = P_0.Encode(SKEncodedImageFormat.Png, P_2.Value);
		sKData2.SaveTo(P_1);
	}
}

