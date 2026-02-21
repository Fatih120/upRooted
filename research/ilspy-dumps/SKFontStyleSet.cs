// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKFontStyleSet
using System;
using System.Collections;
using System.Collections.Generic;
using SkiaSharp;

public class SKFontStyleSet : SKObject, ISKReferenceCounted, IEnumerable<SKFontStyle>, IEnumerable, IReadOnlyCollection<SKFontStyle>, IReadOnlyList<SKFontStyle>
{
	public int Count => SkiaApi.sk_fontstyleset_get_count(Handle);

	public SKFontStyle this[int P_0] => GetStyle(P_0);

	internal SKFontStyleSet(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public IEnumerator<SKFontStyle> GetEnumerator()
	{
		return GetStyles().GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetStyles().GetEnumerator();
	}

	private IEnumerable<SKFontStyle> GetStyles()
	{
		int count = Count;
		for (int i = 0; i < count; i++)
		{
			yield return GetStyle(i);
		}
	}

	private SKFontStyle GetStyle(int P_0)
	{
		SKFontStyle sKFontStyle = new SKFontStyle();
		SkiaApi.sk_fontstyleset_get_style(Handle, P_0, sKFontStyle.Handle, IntPtr.Zero);
		return sKFontStyle;
	}

	internal static SKFontStyleSet GetObject(IntPtr P_0)
	{
		return SKObject.GetOrAddObject(P_0, (IntPtr h, bool o) => new SKFontStyleSet(h, o));
	}
}

