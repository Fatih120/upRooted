// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.CompositeFontFamilyKey
using System;
using System.Collections.Generic;
using Avalonia.Media.Fonts;

internal class CompositeFontFamilyKey : FontFamilyKey
{
	public IReadOnlyList<FontFamilyKey> Keys { get; }

	public CompositeFontFamilyKey(Uri P_0, FontFamilyKey[] P_1)
		: base(P_0)
	{
		Keys = P_1;
	}
}

