// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IGlyphTypeface2
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Avalonia.Media;

internal interface IGlyphTypeface2 : IGlyphTypeface, IDisposable
{
	string TypographicFamilyName { get; }

	IReadOnlyDictionary<ushort, string> FamilyNames { get; }

	bool TryGetStream([NotNullWhen(true)] out Stream? P_0);
}

