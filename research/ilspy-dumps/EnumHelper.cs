// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.EnumHelper
using System;

internal class EnumHelper
{
	public static T Parse<T>(ReadOnlySpan<char> P_0, bool P_1) where T : struct
	{
		return Enum.Parse<T>(P_0, P_1);
	}

	public static bool TryParse<T>(ReadOnlySpan<char> P_0, bool P_1, out T P_2) where T : struct
	{
		return Enum.TryParse<T>(P_0, P_1, out P_2);
	}
}

