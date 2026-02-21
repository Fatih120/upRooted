// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKTypeface
using System;
using System.IO;
using SkiaSharp;

public class SKTypeface : SKObject, ISKReferenceCounted
{
	private sealed class SKTypefaceStatic : SKTypeface
	{
		internal SKTypefaceStatic(IntPtr P_0)
			: base(P_0, false)
		{
		}

		protected override void Dispose(bool P_0)
		{
		}
	}

	private static readonly SKTypeface defaultTypeface;

	private SKFont font;

	public static SKTypeface Default => defaultTypeface;

	public string FamilyName => (string)SKString.GetObject(SkiaApi.sk_typeface_get_family_name(Handle));

	public SKFontStyle FontStyle => SKFontStyle.GetObject(SkiaApi.sk_typeface_get_fontstyle(Handle));

	public int FontWeight => SkiaApi.sk_typeface_get_font_weight(Handle);

	public int FontWidth => SkiaApi.sk_typeface_get_font_width(Handle);

	public SKFontStyleSlant FontSlant => SkiaApi.sk_typeface_get_font_slant(Handle);

	public bool IsBold => FontStyle.Weight >= 600;

	public bool IsItalic => FontStyle.Slant != SKFontStyleSlant.Upright;

	public bool IsFixedPitch => SkiaApi.sk_typeface_is_fixed_pitch(Handle);

	public int UnitsPerEm => SkiaApi.sk_typeface_get_units_per_em(Handle);

	public int GlyphCount => SkiaApi.sk_typeface_count_glyphs(Handle);

	static SKTypeface()
	{
		defaultTypeface = new SKTypefaceStatic(SkiaApi.sk_typeface_ref_default());
	}

	internal static void EnsureStaticInstanceAreInitialized()
	{
	}

	internal SKTypeface(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public static SKTypeface FromFamilyName(string P_0, int P_1, int P_2, SKFontStyleSlant P_3)
	{
		return FromFamilyName(P_0, new SKFontStyle(P_1, P_2, P_3));
	}

	public unsafe static SKTypeface FromFamilyName(string P_0, SKFontStyle P_1)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("style");
		}
		fixed (byte* encodedText = StringUtilities.GetEncodedText(P_0, SKTextEncoding.Utf8, true))
		{
			SKTypeface sKTypeface = GetObject(SkiaApi.sk_typeface_create_from_name(new IntPtr(encodedText), P_1.Handle));
			sKTypeface?.PreventPublicDisposal();
			return sKTypeface;
		}
	}

	public static SKTypeface FromFamilyName(string P_0, SKFontStyleWeight P_1, SKFontStyleWidth P_2, SKFontStyleSlant P_3)
	{
		return FromFamilyName(P_0, (int)P_1, (int)P_2, P_3);
	}

	public static SKTypeface FromStream(Stream P_0, int P_1 = 0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("stream");
		}
		return FromStream(new SKManagedStream(P_0, true), P_1);
	}

	public static SKTypeface FromStream(SKStreamAsset P_0, int P_1 = 0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("stream");
		}
		if (P_0 is SKManagedStream sKManagedStream)
		{
			P_0 = sKManagedStream.ToMemoryStream();
			sKManagedStream.Dispose();
		}
		SKTypeface sKTypeface = GetObject(SkiaApi.sk_typeface_create_from_stream(P_0.Handle, P_1));
		P_0.RevokeOwnership(sKTypeface);
		return sKTypeface;
	}

	public int GetTableSize(uint P_0)
	{
		return (int)SkiaApi.sk_typeface_get_table_size(Handle, P_0);
	}

	public unsafe bool TryGetTableData(uint P_0, out byte[] P_1)
	{
		int tableSize = GetTableSize(P_0);
		byte[] array = new byte[tableSize];
		fixed (byte* ptr = array)
		{
			if (!TryGetTableData(P_0, 0, tableSize, (IntPtr)ptr))
			{
				P_1 = null;
				return false;
			}
		}
		P_1 = array;
		return true;
	}

	public unsafe bool TryGetTableData(uint P_0, int P_1, int P_2, IntPtr P_3)
	{
		IntPtr intPtr = SkiaApi.sk_typeface_get_table_data(Handle, P_0, (IntPtr)P_1, (IntPtr)P_2, (void*)P_3);
		return intPtr != IntPtr.Zero;
	}

	public bool ContainsGlyph(int P_0)
	{
		return GetFont().ContainsGlyph(P_0);
	}

	internal SKFont GetFont()
	{
		return font ?? (font = SKObject.OwnedBy(new SKFont(this), this));
	}

	public SKStreamAsset OpenStream()
	{
		int num;
		return OpenStream(out num);
	}

	public unsafe SKStreamAsset OpenStream(out int P_0)
	{
		fixed (int* ptr = &P_0)
		{
			return SKStreamAsset.GetObject(SkiaApi.sk_typeface_open_stream(Handle, ptr));
		}
	}

	internal static SKTypeface GetObject(IntPtr P_0)
	{
		return SKObject.GetOrAddObject(P_0, (IntPtr h, bool o) => new SKTypeface(h, o));
	}
}

