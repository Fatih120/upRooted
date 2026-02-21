// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GlyphTypefaceImpl
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Avalonia.Media;
using Avalonia.Media.Fonts.Tables;
using Avalonia.Media.Fonts.Tables.Name;
using HarfBuzzSharp;
using SkiaSharp;

internal class GlyphTypefaceImpl : IGlyphTypeface2, IGlyphTypeface, IDisposable
{
	private bool _isDisposed;

	private readonly NameTable? _nameTable;

	private readonly OS2Table? _os2Table;

	private readonly HorizontalHeadTable? _hhTable;

	[CompilerGenerated]
	private readonly IReadOnlyDictionary<ushort, string> _003CFaceNames_003Ek__BackingField;

	[CompilerGenerated]
	private readonly int _003CGlyphCount_003Ek__BackingField;

	public string TypographicFamilyName { get; }

	public IReadOnlyDictionary<ushort, string> FamilyNames { get; }

	public SKTypeface SKTypeface { get; }

	public Face Face { get; }

	public Font Font { get; }

	public FontSimulations FontSimulations { get; }

	public FontMetrics Metrics { get; }

	public string FamilyName { get; }

	public FontWeight Weight { get; }

	public FontStyle Style { get; }

	public FontStretch Stretch { get; }

	public GlyphTypefaceImpl(SKTypeface P_0, FontSimulations P_1)
	{
		SKTypeface = P_0 ?? throw new ArgumentNullException("typeface");
		Face = new Face(GetTable)
		{
			UnitsPerEm = P_0.UnitsPerEm
		};
		Font = new Font(Face);
		Font.SetFunctionsOpenType();
		Font.OpenTypeMetrics.TryGetPosition(OpenTypeMetricsTag.UnderlineOffset, out var num);
		Font.OpenTypeMetrics.TryGetPosition(OpenTypeMetricsTag.UnderlineSize, out var underlineThickness);
		_os2Table = OS2Table.Load(this);
		_hhTable = HorizontalHeadTable.Load(this);
		int num2 = 0;
		int num3 = 0;
		int lineGap = 0;
		if (_os2Table != null && (_os2Table.FontStyle & OS2Table.FontStyleSelection.USE_TYPO_METRICS) != 0)
		{
			num2 = -_os2Table.TypoAscender;
			num3 = -_os2Table.TypoDescender;
			lineGap = _os2Table.TypoLineGap;
		}
		else if (_hhTable != null)
		{
			num2 = -_hhTable.Ascender;
			num3 = -_hhTable.Descender;
			lineGap = _hhTable.LineGap;
		}
		if (_os2Table != null && (num2 == 0 || num3 == 0))
		{
			if (_os2Table.TypoAscender != 0 || _os2Table.TypoDescender != 0)
			{
				num2 = -_os2Table.TypoAscender;
				num3 = -_os2Table.TypoDescender;
				lineGap = _os2Table.TypoLineGap;
			}
			else
			{
				num2 = -_os2Table.WinAscent;
				num3 = _os2Table.WinDescent;
			}
		}
		Metrics = new FontMetrics
		{
			DesignEmHeight = (short)Face.UnitsPerEm,
			Ascent = num2,
			Descent = num3,
			LineGap = lineGap,
			UnderlinePosition = -num,
			UnderlineThickness = underlineThickness,
			StrikethroughPosition = (-(_os2Table?.StrikeoutPosition)).GetValueOrDefault(),
			StrikethroughThickness = (_os2Table?.StrikeoutSize ?? 0),
			IsFixedPitch = P_0.IsFixedPitch
		};
		_003CGlyphCount_003Ek__BackingField = P_0.GlyphCount;
		FontSimulations = P_1;
		FontWeight fontWeight = (FontWeight)((_os2Table != null) ? _os2Table.WeightClass : 400);
		Weight = (((P_1 & FontSimulations.Bold) != FontSimulations.None) ? FontWeight.Bold : fontWeight);
		FontStyle fontStyle = ((_os2Table != null) ? GetFontStyle(_os2Table.FontStyle) : FontStyle.Normal);
		if (P_0.FontStyle.Slant == SKFontStyleSlant.Oblique)
		{
			fontStyle = FontStyle.Oblique;
		}
		Style = (((P_1 & FontSimulations.Oblique) != FontSimulations.None) ? FontStyle.Italic : fontStyle);
		Stretch = (FontStretch)((_os2Table != null) ? _os2Table.WidthClass : 5);
		_nameTable = NameTable.Load(this);
		FamilyName = _nameTable?.FontFamilyName((ushort)CultureInfo.InvariantCulture.LCID) ?? P_0.FamilyName;
		TypographicFamilyName = _nameTable?.GetNameById((ushort)CultureInfo.InvariantCulture.LCID, KnownNameIds.TypographicFamilyName) ?? FamilyName;
		if (_nameTable != null)
		{
			Dictionary<ushort, string> dictionary = new Dictionary<ushort, string>(1);
			Dictionary<ushort, string> dictionary2 = new Dictionary<ushort, string>(1);
			foreach (NameRecord item in _nameTable)
			{
				if (item.NameID == KnownNameIds.FontFamilyName)
				{
					if (item.Platform != PlatformIDs.Windows || item.LanguageID == 0)
					{
						continue;
					}
					if (!dictionary.ContainsKey(item.LanguageID))
					{
						dictionary[item.LanguageID] = item.Value;
					}
				}
				if (item.NameID == KnownNameIds.FontSubfamilyName && item.Platform == PlatformIDs.Windows && item.LanguageID != 0 && !dictionary2.ContainsKey(item.LanguageID))
				{
					dictionary2[item.LanguageID] = item.Value;
				}
			}
			FamilyNames = dictionary;
			_003CFaceNames_003Ek__BackingField = dictionary2;
		}
		else
		{
			FamilyNames = new Dictionary<ushort, string> { 
			{
				(ushort)CultureInfo.InvariantCulture.LCID,
				FamilyName
			} };
			_003CFaceNames_003Ek__BackingField = new Dictionary<ushort, string> { 
			{
				(ushort)CultureInfo.InvariantCulture.LCID,
				Weight.ToString()
			} };
		}
	}

	public ushort GetGlyph(uint P_0)
	{
		if (Font.TryGetGlyph(P_0, out var num))
		{
			return (ushort)num;
		}
		return 0;
	}

	public bool TryGetGlyph(uint P_0, out ushort P_1)
	{
		P_1 = GetGlyph(P_0);
		return P_1 != 0;
	}

	public int GetGlyphAdvance(ushort P_0)
	{
		return Font.GetHorizontalGlyphAdvance(P_0);
	}

	public int[] GetGlyphAdvances(ReadOnlySpan<ushort> P_0)
	{
		uint[] array = new uint[P_0.Length];
		for (int i = 0; i < P_0.Length; i++)
		{
			array[i] = P_0[i];
		}
		return Font.GetHorizontalGlyphAdvances(array);
	}

	private static FontStyle GetFontStyle(OS2Table.FontStyleSelection P_0)
	{
		if ((P_0 & OS2Table.FontStyleSelection.ITALIC) != 0)
		{
			return FontStyle.Italic;
		}
		if ((P_0 & OS2Table.FontStyleSelection.OBLIQUE) != 0)
		{
			return FontStyle.Oblique;
		}
		return FontStyle.Normal;
	}

	private Blob? GetTable(Face face, Tag tag)
	{
		int tableSize = SKTypeface.GetTableSize(tag);
		nint data = Marshal.AllocCoTaskMem(tableSize);
		ReleaseDelegate releaseDelegate = delegate
		{
			Marshal.FreeCoTaskMem(data);
		};
		if (!SKTypeface.TryGetTableData(tag, 0, tableSize, data))
		{
			return null;
		}
		return new Blob(data, tableSize, MemoryMode.ReadOnly, releaseDelegate);
	}

	public SKFont CreateSKFont(float P_0)
	{
		return new SKFont(SKTypeface, P_0, 1f, ((FontSimulations & FontSimulations.Oblique) != FontSimulations.None) ? (-0.3f) : 0f)
		{
			LinearMetrics = true,
			Embolden = ((FontSimulations & FontSimulations.Bold) != 0)
		};
	}

	private void Dispose(bool P_0)
	{
		if (!_isDisposed)
		{
			_isDisposed = true;
			if (P_0)
			{
				Font.Dispose();
				Face.Dispose();
				SKTypeface.Dispose();
			}
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	public bool TryGetTable(uint P_0, out byte[] P_1)
	{
		return SKTypeface.TryGetTableData(P_0, out P_1);
	}

	public bool TryGetStream([NotNullWhen(true)] out Stream? P_0)
	{
		try
		{
			SKStreamAsset sKStreamAsset = SKTypeface.OpenStream();
			int length = sKStreamAsset.Length;
			byte[] array = new byte[length];
			sKStreamAsset.Read(array, length);
			P_0 = new MemoryStream(array);
			return true;
		}
		catch
		{
			P_0 = null;
			return false;
		}
	}
}

