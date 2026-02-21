// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.FontFamily
using System;
using Avalonia.Media;
using Avalonia.Media.Fonts;
using Avalonia.Utilities;

public sealed class FontFamily
{
	public static FontFamily Default { get; }

	public string Name => FamilyNames.PrimaryFamilyName;

	public FamilyNameCollection FamilyNames { get; }

	public FontFamilyKey? Key { get; }

	static FontFamily()
	{
		Default = new FontFamily("$Default");
	}

	public FontFamily(string P_0)
		: this(null, P_0)
	{
	}

	public FontFamily(Uri? P_0, string P_1)
	{
		if (string.IsNullOrEmpty(P_1))
		{
			throw new ArgumentNullException("name");
		}
		if (P_0 != null && !P_0.IsAbsoluteUri)
		{
			throw new ArgumentException("Base uri must be an absolute uri.", "baseUri");
		}
		FrugalStructList<FontSourceIdentifier> fontSourceIdentifier = GetFontSourceIdentifier(P_1);
		FamilyNames = new FamilyNameCollection(fontSourceIdentifier);
		if (fontSourceIdentifier.Count == 1)
		{
			Uri source = fontSourceIdentifier[0].Source;
			if ((object)source != null)
			{
				if (source.IsAbsoluteUri)
				{
					Key = new FontFamilyKey(source);
				}
				else
				{
					Key = new FontFamilyKey(source, P_0);
				}
			}
			return;
		}
		FontFamilyKey[] array = new FontFamilyKey[fontSourceIdentifier.Count];
		for (int i = 0; i < fontSourceIdentifier.Count; i++)
		{
			FontSourceIdentifier fontSourceIdentifier2 = fontSourceIdentifier[i];
			if ((object)fontSourceIdentifier2.Source != null)
			{
				array[i] = new FontFamilyKey(fontSourceIdentifier2.Source, P_0);
			}
			else
			{
				array[i] = new FontFamilyKey(new Uri("systemfont:" + fontSourceIdentifier2.Name, UriKind.Absolute));
			}
		}
		Key = new CompositeFontFamilyKey(new Uri("compositefont:" + P_1, UriKind.Absolute), array);
	}

	public static implicit operator FontFamily(string P_0)
	{
		return new FontFamily(P_0);
	}

	private static FrugalStructList<FontSourceIdentifier> GetFontSourceIdentifier(string P_0)
	{
		FrugalStructList<FontSourceIdentifier> result = new FrugalStructList<FontSourceIdentifier>(1);
		int num = -1;
		do
		{
			int num2 = num + 1;
			num = P_0.IndexOf(',', num2);
			int num3 = ((num == -1) ? P_0.Length : num);
			ReadOnlySpan<char> span = P_0.AsSpan(num2, num3 - num2).Trim();
			FontSourceIdentifier? fontSourceIdentifier = null;
			int num4 = span.IndexOf('#');
			if (num4 != -1 && span.Slice(num4 + 1).IndexOf('#') == -1)
			{
				ReadOnlySpan<char> span2 = span.Slice(0, num4).Trim();
				ReadOnlySpan<char> readOnlySpan = span.Slice(num4 + 1).Trim();
				if (span2.IsEmpty)
				{
					fontSourceIdentifier = new FontSourceIdentifier(readOnlySpan.ToString(), null);
				}
				else
				{
					string uriString = span2.ToString();
					if (span2.IndexOf('/') >= 0 && Uri.TryCreate(uriString, UriKind.Relative, out Uri result2))
					{
						fontSourceIdentifier = new FontSourceIdentifier(readOnlySpan.ToString(), result2);
					}
					else if (Uri.TryCreate(uriString, UriKind.Absolute, out result2))
					{
						fontSourceIdentifier = new FontSourceIdentifier(readOnlySpan.ToString(), result2);
					}
				}
			}
			FontSourceIdentifier value = fontSourceIdentifier.GetValueOrDefault();
			if (!fontSourceIdentifier.HasValue)
			{
				value = new FontSourceIdentifier((span.Length == P_0.Length) ? P_0 : span.ToString(), null);
				fontSourceIdentifier = value;
			}
			result.Add(fontSourceIdentifier.Value);
		}
		while (num != -1);
		return result;
	}

	public override string ToString()
	{
		if (Key != null)
		{
			return Key?.ToString() + "#" + FamilyNames;
		}
		return FamilyNames.ToString();
	}

	public override int GetHashCode()
	{
		return (FamilyNames.GetHashCode() * 397) ^ (((object)Key != null) ? Key.GetHashCode() : 0);
	}

	public static bool operator !=(FontFamily? P_0, FontFamily? P_1)
	{
		return !(P_0 == P_1);
	}

	public static bool operator ==(FontFamily? P_0, FontFamily? P_1)
	{
		if ((object)P_0 == P_1)
		{
			return true;
		}
		return P_0?.Equals(P_1) ?? false;
	}

	public override bool Equals(object? P_0)
	{
		if (this == P_0)
		{
			return true;
		}
		if (!(P_0 is FontFamily fontFamily))
		{
			return false;
		}
		if (!object.Equals(Key, fontFamily.Key))
		{
			return false;
		}
		return fontFamily.FamilyNames.Equals(FamilyNames);
	}
}

