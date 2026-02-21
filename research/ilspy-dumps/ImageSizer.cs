using System;

namespace RootApp.Utility.ImageSize;

public static class ImageSizer
{
	public static bool TryGetSize(string P_0, int P_1, ReadOnlySpan<byte> P_2, out int P_3, out int P_4)
	{
		P_3 = 0;
		P_4 = 0;
		try
		{
			switch (P_0)
			{
			case "image/jpeg":
				(P_3, P_4) = JpegSizer.GetSize(P_2);
				break;
			case "image/png":
				(P_3, P_4) = PngSizer.GetSize(P_2);
				break;
			case "image/gif":
				(P_3, P_4) = GifSizer.GetSize(P_2);
				break;
			case "image/avif":
				(P_3, P_4) = AvifSizer.GetSize(P_1, P_2);
				break;
			case "image/webp":
				(P_3, P_4) = WebPSizer.GetSize(P_1, P_2);
				break;
			default:
				return false;
			}
			return P_3 > 0 && P_4 > 0;
		}
		catch (Exception)
		{
			return false;
		}
	}
}
