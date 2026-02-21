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

