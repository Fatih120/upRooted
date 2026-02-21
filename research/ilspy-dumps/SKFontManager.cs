// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKFontManager
using System;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;

public class SKFontManager : SKObject, ISKReferenceCounted
{
	private sealed class SKFontManagerStatic : SKFontManager
	{
		internal SKFontManagerStatic(IntPtr P_0)
			: base(P_0, false)
		{
		}

		protected override void Dispose(bool P_0)
		{
		}
	}

	private static readonly SKFontManager defaultManager;

	public static SKFontManager Default => defaultManager;

	public int FontFamilyCount => SkiaApi.sk_fontmgr_count_families(Handle);

	public IEnumerable<string> FontFamilies
	{
		get
		{
			int count = FontFamilyCount;
			for (int i = 0; i < count; i++)
			{
				yield return GetFamilyName(i);
			}
		}
	}

	static SKFontManager()
	{
		defaultManager = new SKFontManagerStatic(SkiaApi.sk_fontmgr_ref_default());
	}

	internal static void EnsureStaticInstanceAreInitialized()
	{
	}

	internal SKFontManager(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public string GetFamilyName(int P_0)
	{
		using SKString sKString = new SKString();
		SkiaApi.sk_fontmgr_get_family_name(Handle, P_0, sKString.Handle);
		return (string)sKString;
	}

	public string[] GetFontFamilies()
	{
		return FontFamilies.ToArray();
	}

	public unsafe SKFontStyleSet GetFontStyles(string P_0)
	{
		fixed (byte* encodedText = StringUtilities.GetEncodedText(P_0, SKTextEncoding.Utf8, true))
		{
			return SKFontStyleSet.GetObject(SkiaApi.sk_fontmgr_match_family(Handle, new IntPtr(encodedText)));
		}
	}

	public unsafe SKTypeface MatchFamily(string P_0, SKFontStyle P_1)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("style");
		}
		fixed (byte* encodedText = StringUtilities.GetEncodedText(P_0, SKTextEncoding.Utf8, true))
		{
			SKTypeface sKTypeface = SKTypeface.GetObject(SkiaApi.sk_fontmgr_match_family_style(Handle, new IntPtr(encodedText), P_1.Handle));
			sKTypeface?.PreventPublicDisposal();
			return sKTypeface;
		}
	}

	public SKTypeface MatchCharacter(int P_0)
	{
		return MatchCharacter(null, SKFontStyle.Normal, null, P_0);
	}

	public SKTypeface MatchCharacter(string P_0, SKFontStyleWeight P_1, SKFontStyleWidth P_2, SKFontStyleSlant P_3, string[] P_4, int P_5)
	{
		return MatchCharacter(P_0, new SKFontStyle(P_1, P_2, P_3), P_4, P_5);
	}

	public unsafe SKTypeface MatchCharacter(string P_0, SKFontStyle P_1, string[] P_2, int P_3)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("style");
		}
		if (P_0 == null)
		{
			P_0 = string.Empty;
		}
		fixed (byte* encodedText = StringUtilities.GetEncodedText(P_0, SKTextEncoding.Utf8, true))
		{
			SKTypeface sKTypeface = SKTypeface.GetObject(SkiaApi.sk_fontmgr_match_family_style_character(Handle, new IntPtr(encodedText), P_1.Handle, P_2, (P_2 != null) ? P_2.Length : 0, P_3));
			sKTypeface?.PreventPublicDisposal();
			return sKTypeface;
		}
	}

	public static SKFontManager CreateDefault()
	{
		return GetObject(SkiaApi.sk_fontmgr_create_default());
	}

	internal static SKFontManager GetObject(IntPtr P_0)
	{
		return SKObject.GetOrAddObject(P_0, (IntPtr h, bool o) => new SKFontManager(h, o));
	}
}

