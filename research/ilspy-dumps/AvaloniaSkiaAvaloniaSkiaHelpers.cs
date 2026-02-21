// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.Helpers.DrawingContextHelper
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Media;
using SkiaSharp;

public static class DrawingContextHelper
{
	public static bool TryCreateDashEffect(IPen? P_0, [NotNullWhen(true)] out SKPathEffect? P_1)
	{
		if (P_0?.DashStyle?.Dashes != null && P_0.DashStyle.Dashes.Count > 0)
		{
			IReadOnlyList<double> dashes = P_0.DashStyle.Dashes;
			int num = ((dashes.Count % 2 == 0) ? dashes.Count : (dashes.Count * 2));
			float[] array = new float[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (float)dashes[i % dashes.Count] * (float)P_0.Thickness;
			}
			float num2 = (float)(P_0.DashStyle.Offset * P_0.Thickness);
			P_1 = SKPathEffect.CreateDash(array, num2);
			return true;
		}
		P_1 = null;
		return false;
	}
}

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

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.Helpers.PenHelper
using System;
using Avalonia.Media;

internal static class PenHelper
{
	public static int GetHashCode(IPen? P_0, bool P_1)
	{
		if (P_0 == null)
		{
			return 0;
		}
		HashCode hashCode = default(HashCode);
		hashCode.Add(P_0.LineCap);
		hashCode.Add(P_0.LineJoin);
		hashCode.Add(P_0.MiterLimit);
		hashCode.Add(P_0.Thickness);
		IDashStyle dashStyle = P_0.DashStyle;
		if (dashStyle != null)
		{
			hashCode.Add(dashStyle.Offset);
			for (int i = 0; i < dashStyle.Dashes?.Count; i++)
			{
				hashCode.Add(dashStyle.Dashes[i]);
			}
		}
		if (P_1)
		{
			hashCode.Add(P_0.Brush);
		}
		return hashCode.ToHashCode();
	}
}

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

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.Helpers.SKPathHelper
using Avalonia.Media;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using Avalonia.Utilities;
using SkiaSharp;

internal static class SKPathHelper
{
	public static SKPath? CreateStrokedPath(SKPath P_0, IPen P_1)
	{
		if (MathUtilities.IsZero(P_1.Thickness))
		{
			return null;
		}
		SKPaint sKPaint = SKCacheBase<SKPaint, SKPaintCache>.Shared.Get();
		sKPaint.IsStroke = true;
		sKPaint.StrokeWidth = (float)P_1.Thickness;
		sKPaint.StrokeCap = P_1.LineCap.ToSKStrokeCap();
		sKPaint.StrokeJoin = P_1.LineJoin.ToSKStrokeJoin();
		sKPaint.StrokeMiter = (float)P_1.MiterLimit;
		if (DrawingContextHelper.TryCreateDashEffect(P_1, out SKPathEffect pathEffect))
		{
			sKPaint.PathEffect = pathEffect;
		}
		SKPath sKPath = new SKPath();
		sKPaint.GetFillPath(P_0, sKPath);
		sKPaint.PathEffect?.Dispose();
		SKCacheBase<SKPaint, SKPaintCache>.Shared.ReturnReset(sKPaint);
		return sKPath;
	}
}
