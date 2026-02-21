// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ByteSizeHelper
using System;

internal static class ByteSizeHelper
{
	private static readonly string[] Prefixes = new string[9] { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

	public static string ToString(ulong P_0, bool P_1)
	{
		if (P_0 == 0L)
		{
			return string.Format(P_1 ? "{0}{1:0.#} {2}" : "{0}{1:0.#}{2}", null, 0, Prefixes[0]);
		}
		double num = Math.Abs((double)P_0);
		int num2 = (int)Math.Log(num, 1000.0);
		int num3 = ((num2 >= Prefixes.Length) ? (Prefixes.Length - 1) : num2);
		double num4 = num / Math.Pow(1000.0, num3);
		return string.Format("{0}{1:0.#}{2}", (P_0 < 0) ? "-" : null, num4, Prefixes[num3]);
	}
}

