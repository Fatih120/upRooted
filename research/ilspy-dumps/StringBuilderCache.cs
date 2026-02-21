// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.StringBuilderCache
using System;
using System.Text;

internal static class StringBuilderCache
{
	[ThreadStatic]
	private static StringBuilder? t_cachedInstance;

	public static StringBuilder Acquire(int P_0 = 16)
	{
		if (P_0 <= 360)
		{
			StringBuilder stringBuilder = t_cachedInstance;
			if (stringBuilder != null && P_0 <= stringBuilder.Capacity)
			{
				t_cachedInstance = null;
				stringBuilder.Clear();
				return stringBuilder;
			}
		}
		return new StringBuilder(P_0);
	}

	public static void Release(StringBuilder P_0)
	{
		if (P_0.Capacity <= 360)
		{
			t_cachedInstance = P_0;
		}
	}

	public static string GetStringAndRelease(StringBuilder P_0)
	{
		string result = P_0.ToString();
		Release(P_0);
		return result;
	}
}

