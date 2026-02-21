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

