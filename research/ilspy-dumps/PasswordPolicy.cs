using System;
using System.Globalization;
using System.Text;

namespace RootApp.WebApi.Shared.Policies;

public static class PasswordPolicy
{
	public static bool IsValid(string? password)
	{
		return password != null && IsValid(password.AsSpan());
	}

	public static bool IsValid(ReadOnlySpan<char> P_0)
	{
		int length = P_0.Length;
		if ((length < 8 || length > 128) ? true : false)
		{
			return false;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		SpanRuneEnumerator enumerator = P_0.EnumerateRunes().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Rune current = enumerator.Current;
				num++;
				switch (Rune.GetUnicodeCategory(current))
				{
				case UnicodeCategory.LowercaseLetter:
					num2++;
					continue;
				case UnicodeCategory.UppercaseLetter:
					num5++;
					continue;
				case UnicodeCategory.DecimalDigitNumber:
					num3++;
					continue;
				}
				if (Rune.IsSymbol(current) || Rune.IsPunctuation(current))
				{
					num6++;
				}
				else if (Rune.IsLetter(current))
				{
					num4++;
				}
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to .constrained prefix*/).Dispose();
		}
		if ((num < 8 || num > 128) ? true : false)
		{
			return false;
		}
		return (num4 > 0 || (num2 > 0 && num5 > 0)) && num3 > 0 && num6 > 0;
	}
}
