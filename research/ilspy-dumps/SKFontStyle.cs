// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKFontStyle
using System;
using SkiaSharp;

public class SKFontStyle : SKObject, ISKSkipObjectRegistration
{
	private sealed class SKFontStyleStatic : SKFontStyle
	{
		internal SKFontStyleStatic(SKFontStyleWeight P_0, SKFontStyleWidth P_1, SKFontStyleSlant P_2)
			: base(P_0, P_1, P_2)
		{
		}

		protected override void Dispose(bool P_0)
		{
		}
	}

	private static readonly SKFontStyle normal;

	private static readonly SKFontStyle bold;

	private static readonly SKFontStyle italic;

	private static readonly SKFontStyle boldItalic;

	public int Weight => SkiaApi.sk_fontstyle_get_weight(Handle);

	public int Width => SkiaApi.sk_fontstyle_get_width(Handle);

	public SKFontStyleSlant Slant => SkiaApi.sk_fontstyle_get_slant(Handle);

	public static SKFontStyle Normal => normal;

	public static SKFontStyle Bold => bold;

	public static SKFontStyle Italic => italic;

	public static SKFontStyle BoldItalic => boldItalic;

	static SKFontStyle()
	{
		normal = new SKFontStyleStatic(SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
		bold = new SKFontStyleStatic(SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright);
		italic = new SKFontStyleStatic(SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Italic);
		boldItalic = new SKFontStyleStatic(SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Italic);
	}

	internal SKFontStyle(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKFontStyle()
		: this(SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright)
	{
	}

	public SKFontStyle(SKFontStyleWeight P_0, SKFontStyleWidth P_1, SKFontStyleSlant P_2)
		: this((int)P_0, (int)P_1, P_2)
	{
	}

	public SKFontStyle(int P_0, int P_1, SKFontStyleSlant P_2)
		: this(SkiaApi.sk_fontstyle_new(P_0, P_1, P_2), true)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_fontstyle_delete(Handle);
	}

	internal static SKFontStyle GetObject(IntPtr P_0)
	{
		if (!(P_0 == IntPtr.Zero))
		{
			return new SKFontStyle(P_0, true);
		}
		return null;
	}
}

