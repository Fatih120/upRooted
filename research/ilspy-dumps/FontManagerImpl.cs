// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.FontManagerImpl
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class FontManagerImpl : IFontManagerImpl, IFontManagerImpl2
{
	private SKFontManager _skFontManager = SKFontManager.Default;

	[ThreadStatic]
	private static string[]? t_languageTagBuffer;

	public string GetDefaultFontFamilyName()
	{
		return SKTypeface.Default.FamilyName;
	}

	public string[] GetInstalledFontFamilyNames(bool P_0 = false)
	{
		if (P_0)
		{
			_skFontManager = SKFontManager.CreateDefault();
		}
		return _skFontManager.GetFontFamilies();
	}

	public bool TryMatchCharacter(int P_0, FontStyle P_1, FontWeight P_2, FontStretch P_3, CultureInfo? P_4, out Typeface P_5)
	{
		if (!TryMatchCharacter(P_0, P_1, P_2, P_3, P_4, out SKTypeface sKTypeface))
		{
			P_5 = default(Typeface);
			return false;
		}
		P_5 = new Typeface(sKTypeface.FamilyName, sKTypeface.FontStyle.Slant.ToAvalonia(), (FontWeight)sKTypeface.FontStyle.Weight, (FontStretch)sKTypeface.FontStyle.Width);
		sKTypeface.Dispose();
		return true;
	}

	public bool TryMatchCharacter(int P_0, FontStyle P_1, FontWeight P_2, FontStretch P_3, CultureInfo? P_4, [NotNullWhen(true)] out IGlyphTypeface? P_5)
	{
		if (!TryMatchCharacter(P_0, P_1, P_2, P_3, P_4, out SKTypeface sKTypeface))
		{
			P_5 = null;
			return false;
		}
		P_5 = new GlyphTypefaceImpl(sKTypeface, FontSimulations.None);
		return true;
	}

	private bool TryMatchCharacter(int P_0, FontStyle P_1, FontWeight P_2, FontStretch P_3, CultureInfo? P_4, [NotNullWhen(true)] out SKTypeface? P_5)
	{
		SKFontStyle sKFontStyle;
		if (P_2 != FontWeight.Normal)
		{
			if (P_2 != FontWeight.Bold)
			{
				goto IL_0056;
			}
			if (P_1 == FontStyle.Normal && P_3 == FontStretch.Normal)
			{
				sKFontStyle = SKFontStyle.Bold;
			}
			else
			{
				if (P_1 != FontStyle.Italic || P_3 != FontStretch.Normal)
				{
					goto IL_0056;
				}
				sKFontStyle = SKFontStyle.BoldItalic;
			}
		}
		else if (P_1 == FontStyle.Normal && P_3 == FontStretch.Normal)
		{
			sKFontStyle = SKFontStyle.Normal;
		}
		else
		{
			if (P_1 != FontStyle.Italic || P_3 != FontStretch.Normal)
			{
				goto IL_0056;
			}
			sKFontStyle = SKFontStyle.Italic;
		}
		goto IL_0065;
		IL_0056:
		sKFontStyle = new SKFontStyle((SKFontStyleWeight)P_2, (SKFontStyleWidth)P_3, P_1.ToSkia());
		goto IL_0065;
		IL_0065:
		if (P_4 == null)
		{
			P_4 = CultureInfo.CurrentUICulture;
		}
		if (t_languageTagBuffer == null)
		{
			t_languageTagBuffer = new string[1];
		}
		t_languageTagBuffer[0] = P_4.Name;
		P_5 = _skFontManager.MatchCharacter(null, sKFontStyle, t_languageTagBuffer, P_0);
		return P_5 != null;
	}

	public bool TryCreateGlyphTypeface(string P_0, FontStyle P_1, FontWeight P_2, FontStretch P_3, [NotNullWhen(true)] out IGlyphTypeface? P_4)
	{
		P_4 = null;
		SKFontStyle sKFontStyle = new SKFontStyle((SKFontStyleWeight)P_2, (SKFontStyleWidth)P_3, P_1.ToSkia());
		SKTypeface sKTypeface = _skFontManager.MatchFamily(P_0, sKFontStyle);
		if (false)
		{
		}
		if (sKTypeface == null)
		{
			return false;
		}
		FontSimulations fontSimulations = FontSimulations.None;
		if (P_2 >= FontWeight.DemiBold && !sKTypeface.IsBold)
		{
			fontSimulations |= FontSimulations.Bold;
		}
		if (P_1 == FontStyle.Italic && !sKTypeface.IsItalic)
		{
			fontSimulations |= FontSimulations.Oblique;
		}
		P_4 = new GlyphTypefaceImpl(sKTypeface, fontSimulations);
		return true;
	}

	public bool TryCreateGlyphTypeface(Stream P_0, FontSimulations P_1, [NotNullWhen(true)] out IGlyphTypeface? P_2)
	{
		SKTypeface sKTypeface = SKTypeface.FromStream(P_0);
		if (sKTypeface != null)
		{
			P_2 = new GlyphTypefaceImpl(sKTypeface, P_1);
			return true;
		}
		P_2 = null;
		return false;
	}
}

