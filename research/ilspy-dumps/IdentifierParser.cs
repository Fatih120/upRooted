// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.IdentifierParser
using System;
using System.Globalization;
using Avalonia.Utilities;

public static class IdentifierParser
{
	public static ReadOnlySpan<char> ParseIdentifier(this scoped ref CharacterReader P_0)
	{
		if (IsValidIdentifierStart(P_0.Peek))
		{
			return P_0.TakeWhile((char c) => IsValidIdentifierChar(c));
		}
		return ReadOnlySpan<char>.Empty;
	}

	private static bool IsValidIdentifierStart(char P_0)
	{
		if (!char.IsLetter(P_0))
		{
			return P_0 == '_';
		}
		return true;
	}

	private static bool IsValidIdentifierChar(char P_0)
	{
		if (IsValidIdentifierStart(P_0))
		{
			return true;
		}
		UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(P_0);
		if (unicodeCategory != UnicodeCategory.NonSpacingMark && unicodeCategory != UnicodeCategory.SpacingCombiningMark && unicodeCategory != UnicodeCategory.ConnectorPunctuation && unicodeCategory != UnicodeCategory.Format)
		{
			return unicodeCategory == UnicodeCategory.DecimalDigitNumber;
		}
		return true;
	}
}

