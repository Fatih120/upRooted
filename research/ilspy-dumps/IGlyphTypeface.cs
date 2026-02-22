// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IGlyphTypeface
using System;
using Avalonia.Media;
using Avalonia.Metadata;

[Unstable]
public interface IGlyphTypeface : IDisposable
{
	string FamilyName { get; }

	FontWeight Weight { get; }

	FontStyle Style { get; }

	FontStretch Stretch { get; }

	FontMetrics Metrics { get; }

	ushort GetGlyph(uint P_0);

	bool TryGetGlyph(uint P_0, out ushort P_1);

	int GetGlyphAdvance(ushort P_0);

	int[] GetGlyphAdvances(ReadOnlySpan<ushort> P_0);

	bool TryGetTable(uint P_0, out byte[] P_1);
}

