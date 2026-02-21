// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.UriExtensions
using System;

internal static class UriExtensions
{
	public static bool IsAbsoluteResm(this Uri P_0)
	{
		if (P_0.IsAbsoluteUri)
		{
			return IsResm(P_0);
		}
		return false;
	}

	public static bool IsResm(this Uri P_0)
	{
		return P_0.Scheme == "resm";
	}

	public static bool IsAvares(this Uri P_0)
	{
		return P_0.Scheme == "avares";
	}

	public static bool IsFontCollection(this Uri P_0)
	{
		return P_0.Scheme == "fonts";
	}

	public static Uri EnsureAbsolute(this Uri P_0, Uri? P_1)
	{
		if (P_0.IsAbsoluteUri)
		{
			return P_0;
		}
		if (P_1 == null)
		{
			throw new ArgumentException($"Relative uri {P_0} without base url");
		}
		if (!P_1.IsAbsoluteUri)
		{
			throw new ArgumentException($"Base uri {P_1} is relative");
		}
		return new Uri(P_1, P_0);
	}

	public static string GetUnescapeAbsolutePath(this Uri P_0)
	{
		return Uri.UnescapeDataString(P_0.AbsolutePath);
	}

	public static string GetUnescapeAbsoluteUri(this Uri P_0)
	{
		return Uri.UnescapeDataString(P_0.AbsoluteUri);
	}

	public static string GetAssemblyNameFromQuery(this Uri P_0)
	{
		string text = Uri.UnescapeDataString(P_0.Query);
		int num;
		for (num = 1; num < text.Length; num++)
		{
			bool flag = false;
			for (int i = 0; i < "assembly".Length && text[num] == "assembly"[i]; i++)
			{
				flag = i == "assembly".Length - 1;
				num++;
			}
			num++;
			int num2 = num;
			for (; num < text.Length && text[num] != '&'; num++)
			{
			}
			if (flag)
			{
				return text.Substring(num2, num - num2);
			}
		}
		return "";
	}
}

