// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.Typeface
using System;
using Avalonia.Media;

public readonly struct Typeface : IEquatable<Typeface>
{
	public static Typeface Default { get; } = new Typeface(Avalonia.Media.FontFamily.Default);

	public FontFamily FontFamily { get; }

	public FontStyle Style { get; }

	public FontWeight Weight { get; }

	public FontStretch Stretch { get; }

	public IGlyphTypeface GlyphTypeface
	{
		get
		{
			if (FontManager.Current.TryGetGlyphTypeface(this, out IGlyphTypeface result))
			{
				return result;
			}
			throw new InvalidOperationException($"Could not create glyphTypeface. Font family: {FontFamily?.Name} (key: {FontFamily?.Key}). Style: {Style}. Weight: {Weight}. Stretch: {Stretch}");
		}
	}

	public Typeface(FontFamily P_0, FontStyle P_1 = FontStyle.Normal, FontWeight P_2 = FontWeight.Normal, FontStretch P_3 = FontStretch.Normal)
	{
		if (P_2 <= (FontWeight)0)
		{
			throw new ArgumentException("Font weight must be > 0.");
		}
		if (P_3 < FontStretch.UltraCondensed)
		{
			throw new ArgumentException("Font stretch must be > 1.");
		}
		FontFamily = P_0 ?? Avalonia.Media.FontFamily.Default;
		Style = P_1;
		Weight = P_2;
		Stretch = P_3;
	}

	public Typeface(string P_0, FontStyle P_1 = FontStyle.Normal, FontWeight P_2 = FontWeight.Normal, FontStretch P_3 = FontStretch.Normal)
		: this(string.IsNullOrEmpty(P_0) ? Avalonia.Media.FontFamily.Default : new FontFamily(P_0), P_1, P_2, P_3)
	{
	}

	public static bool operator !=(Typeface P_0, Typeface P_1)
	{
		return !(P_0 == P_1);
	}

	public static bool operator ==(Typeface P_0, Typeface P_1)
	{
		return P_0.Equals(P_1);
	}

	public override bool Equals(object? P_0)
	{
		if (P_0 is Typeface typeface)
		{
			return Equals(typeface);
		}
		return false;
	}

	public bool Equals(Typeface P_0)
	{
		if (FontFamily == P_0.FontFamily && Style == P_0.Style && Weight == P_0.Weight)
		{
			return Stretch == P_0.Stretch;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (int)(((((uint)(((FontFamily != null) ? FontFamily.GetHashCode() : 0) * 397) ^ (uint)Style) * 397) ^ (uint)Weight) * 397) ^ (int)Stretch;
	}
}

