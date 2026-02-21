using System;

namespace RootApp.Utility;

public static class RootCapitalization
{
	public static string ToFirstUpper(string P_0)
	{
		if (string.IsNullOrEmpty(P_0))
		{
			return string.Empty;
		}
		char c = P_0[0];
		char c2 = char.ToUpperInvariant(c);
		if (c == c2)
		{
			return P_0;
		}
		return (P_0.Length == 1) ? new string(c2, 1) : $"{c2}{P_0.AsSpan(1)}";
	}
}
