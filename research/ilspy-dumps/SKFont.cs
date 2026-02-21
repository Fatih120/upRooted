// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKFont
using System;
using System.Collections.Generic;
using SkiaSharp;

public class SKFont : SKObject
{
	internal sealed class GlyphPathCache : Dictionary<ushort, SKPath>, IDisposable
	{
		public SKFont Font { get; }

		public GlyphPathCache(SKFont P_0)
		{
			Font = P_0;
		}

		public SKPath GetPath(ushort P_0)
		{
			if (!TryGetValue(P_0, out var value))
			{
				value = (base[P_0] = Font.GetGlyphPath(P_0));
			}
			return value;
		}

		public void Dispose()
		{
			foreach (SKPath value in base.Values)
			{
				value?.Dispose();
			}
			Clear();
		}
	}

	public bool Subpixel
	{
		get
		{
			return SkiaApi.sk_font_is_subpixel(Handle);
		}
		set
		{
			SkiaApi.sk_font_set_subpixel(Handle, flag);
		}
	}

	public bool LinearMetrics
	{
		set
		{
			SkiaApi.sk_font_set_linear_metrics(Handle, flag);
		}
	}

	public bool Embolden
	{
		get
		{
			return SkiaApi.sk_font_is_embolden(Handle);
		}
		set
		{
			SkiaApi.sk_font_set_embolden(Handle, flag);
		}
	}

	public SKFontEdging Edging
	{
		set
		{
			SkiaApi.sk_font_set_edging(Handle, sKFontEdging);
		}
	}

	public SKFontHinting Hinting
	{
		set
		{
			SkiaApi.sk_font_set_hinting(Handle, sKFontHinting);
		}
	}

	public SKTypeface Typeface
	{
		get
		{
			return SKTypeface.GetObject(SkiaApi.sk_font_get_typeface(Handle));
		}
		set
		{
			SkiaApi.sk_font_set_typeface(Handle, sKTypeface?.Handle ?? IntPtr.Zero);
		}
	}

	public float Size
	{
		get
		{
			return SkiaApi.sk_font_get_size(Handle);
		}
		set
		{
			SkiaApi.sk_font_set_size(Handle, num);
		}
	}

	public float ScaleX => SkiaApi.sk_font_get_scale_x(Handle);

	public float SkewX => SkiaApi.sk_font_get_skew_x(Handle);

	internal SKFont(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKFont(SKTypeface P_0, float P_1 = 12f, float P_2 = 1f, float P_3 = 0f)
		: this(SkiaApi.sk_font_new_with_values(P_0?.Handle ?? IntPtr.Zero, P_1, P_2, P_3), true)
	{
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKFont instance.");
		}
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_font_delete(Handle);
	}

	public unsafe float GetFontMetrics(out SKFontMetrics P_0)
	{
		fixed (SKFontMetrics* ptr = &P_0)
		{
			return SkiaApi.sk_font_get_metrics(Handle, ptr);
		}
	}

	public ushort GetGlyph(int P_0)
	{
		return SkiaApi.sk_font_unichar_to_glyph(Handle, P_0);
	}

	internal unsafe void GetGlyphs(void* P_0, int P_1, SKTextEncoding P_2, Span<ushort> P_3)
	{
		if (ValidateTextArgs(P_0, P_1, P_2))
		{
			fixed (ushort* ptr = P_3)
			{
				SkiaApi.sk_font_text_to_glyphs(Handle, P_0, (IntPtr)P_1, P_2, ptr, P_3.Length);
			}
		}
	}

	public bool ContainsGlyph(int P_0)
	{
		return GetGlyph(P_0) != 0;
	}

	internal unsafe int CountGlyphs(void* P_0, int P_1, SKTextEncoding P_2)
	{
		if (!ValidateTextArgs(P_0, P_1, P_2))
		{
			return 0;
		}
		return SkiaApi.sk_font_text_to_glyphs(Handle, P_0, (IntPtr)P_1, P_2, null, 0);
	}

	internal float MeasureText(string P_0, SKPaint P_1 = null)
	{
		return MeasureText(P_0.AsSpan(), P_1);
	}

	internal unsafe float MeasureText(ReadOnlySpan<char> P_0, SKPaint P_1 = null)
	{
		fixed (char* ptr = P_0)
		{
			void* ptr2 = ptr;
			return MeasureText(ptr2, P_0.Length * 2, SKTextEncoding.Utf16, null, P_1);
		}
	}

	internal float MeasureText(string P_0, out SKRect P_1, SKPaint P_2 = null)
	{
		return MeasureText(P_0.AsSpan(), out P_1, P_2);
	}

	internal unsafe float MeasureText(ReadOnlySpan<char> P_0, out SKRect P_1, SKPaint P_2 = null)
	{
		fixed (char* ptr = P_0)
		{
			void* ptr2 = ptr;
			fixed (SKRect* ptr3 = &P_1)
			{
				return MeasureText(ptr2, P_0.Length * 2, SKTextEncoding.Utf16, ptr3, P_2);
			}
		}
	}

	internal unsafe float MeasureText(void* P_0, int P_1, SKTextEncoding P_2, SKRect* P_3, SKPaint P_4)
	{
		if (!ValidateTextArgs(P_0, P_1, P_2))
		{
			return 0f;
		}
		float result = default(float);
		SkiaApi.sk_font_measure_text_no_return(Handle, P_0, (IntPtr)P_1, P_2, P_3, P_4?.Handle ?? IntPtr.Zero, &result);
		return result;
	}

	public unsafe void GetGlyphPositions(ReadOnlySpan<ushort> P_0, Span<SKPoint> P_1, SKPoint P_2 = default(SKPoint))
	{
		if (P_0.Length != P_1.Length)
		{
			throw new ArgumentException("The length of glyphs must be the same as the length of positions.", "positions");
		}
		fixed (ushort* ptr = P_0)
		{
			fixed (SKPoint* ptr2 = P_1)
			{
				SkiaApi.sk_font_get_pos(Handle, ptr, P_0.Length, ptr2, &P_2);
			}
		}
	}

	public unsafe void GetGlyphWidths(ReadOnlySpan<ushort> P_0, Span<float> P_1, Span<SKRect> P_2, SKPaint P_3 = null)
	{
		fixed (ushort* ptr = P_0)
		{
			fixed (float* ptr2 = P_1)
			{
				fixed (SKRect* ptr3 = P_2)
				{
					float* ptr4 = ((P_1.Length > 0) ? ptr2 : null);
					SKRect* ptr5 = ((P_2.Length > 0) ? ptr3 : null);
					SkiaApi.sk_font_get_widths_bounds(Handle, ptr, P_0.Length, ptr4, ptr5, P_3?.Handle ?? IntPtr.Zero);
				}
			}
		}
	}

	public SKPath GetGlyphPath(ushort P_0)
	{
		SKPath sKPath = new SKPath();
		if (!SkiaApi.sk_font_get_path(Handle, P_0, sKPath.Handle))
		{
			sKPath.Dispose();
			sKPath = null;
		}
		return sKPath;
	}

	internal SKPath GetTextPath(string P_0, SKPoint P_1 = default(SKPoint))
	{
		return GetTextPath(P_0.AsSpan(), P_1);
	}

	internal unsafe SKPath GetTextPath(ReadOnlySpan<char> P_0, SKPoint P_1 = default(SKPoint))
	{
		fixed (char* ptr = P_0)
		{
			void* ptr2 = ptr;
			return GetTextPath(ptr2, P_0.Length * 2, SKTextEncoding.Utf16, P_1);
		}
	}

	internal unsafe SKPath GetTextPath(void* P_0, int P_1, SKTextEncoding P_2, SKPoint P_3)
	{
		if (!ValidateTextArgs(P_0, P_1, P_2))
		{
			return new SKPath();
		}
		SKPath sKPath = new SKPath();
		SkiaApi.sk_text_utils_get_path(P_0, (IntPtr)P_1, P_2, P_3.X, P_3.Y, Handle, sKPath.Handle);
		return sKPath;
	}

	internal SKPath GetTextPathOnPath(string P_0, SKPath P_1, SKTextAlign P_2 = SKTextAlign.Left, SKPoint P_3 = default(SKPoint))
	{
		return GetTextPathOnPath(P_0.AsSpan(), P_1, P_2, P_3);
	}

	internal unsafe SKPath GetTextPathOnPath(ReadOnlySpan<char> P_0, SKPath P_1, SKTextAlign P_2 = SKTextAlign.Left, SKPoint P_3 = default(SKPoint))
	{
		fixed (char* ptr = P_0)
		{
			void* ptr2 = ptr;
			return GetTextPathOnPath(ptr2, P_0.Length * 2, SKTextEncoding.Utf16, P_1, P_2, P_3);
		}
	}

	internal unsafe SKPath GetTextPathOnPath(void* P_0, int P_1, SKTextEncoding P_2, SKPath P_3, SKTextAlign P_4 = SKTextAlign.Left, SKPoint P_5 = default(SKPoint))
	{
		if (!ValidateTextArgs(P_0, P_1, P_2))
		{
			return new SKPath();
		}
		int num = CountGlyphs(P_0, P_1, P_2);
		if (num <= 0)
		{
			return new SKPath();
		}
		Utils.RentedArray<ushort> rentedArray = Utils.RentArray<ushort>(num);
		try
		{
			GetGlyphs(P_0, P_1, P_2, rentedArray);
			return GetTextPathOnPath(rentedArray, P_3, P_4, P_5);
		}
		finally
		{
			rentedArray.Dispose();
		}
	}

	internal SKPath GetTextPathOnPath(ReadOnlySpan<ushort> P_0, SKPath P_1, SKTextAlign P_2 = SKTextAlign.Left, SKPoint P_3 = default(SKPoint))
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("path");
		}
		if (P_0.Length == 0)
		{
			return new SKPath();
		}
		Utils.RentedArray<float> rentedArray = Utils.RentArray<float>(P_0.Length);
		try
		{
			Utils.RentedArray<SKPoint> rentedArray2 = Utils.RentArray<SKPoint>(P_0.Length);
			try
			{
				GetGlyphWidths(P_0, rentedArray, Span<SKRect>.Empty);
				GetGlyphPositions(P_0, rentedArray2, P_3);
				return GetTextPathOnPath(P_0, rentedArray, rentedArray2, P_1, P_2);
			}
			finally
			{
				rentedArray2.Dispose();
			}
		}
		finally
		{
			rentedArray.Dispose();
		}
	}

	internal SKPath GetTextPathOnPath(ReadOnlySpan<ushort> P_0, ReadOnlySpan<float> P_1, ReadOnlySpan<SKPoint> P_2, SKPath P_3, SKTextAlign P_4 = SKTextAlign.Left)
	{
		if (P_0.Length != P_1.Length)
		{
			throw new ArgumentException("The number of glyphs and glyph widths must be the same.");
		}
		if (P_0.Length != P_2.Length)
		{
			throw new ArgumentException("The number of glyphs and glyph offsets must be the same.");
		}
		if (P_3 == null)
		{
			throw new ArgumentNullException("path");
		}
		if (P_0.Length == 0)
		{
			return new SKPath();
		}
		using (GlyphPathCache glyphPathCache = new GlyphPathCache(this))
		{
			using SKPathMeasure sKPathMeasure = new SKPathMeasure(P_3);
			float length = sKPathMeasure.Length;
			float num = P_2[P_0.Length - 1].X + P_1[P_0.Length - 1];
			float num2 = (float)P_4 * 0.5f;
			float num3 = P_2[0].X + (length - num) * num2;
			SKPath sKPath = new SKPath();
			for (int i = 0; i < P_2.Length; i++)
			{
				SKPoint sKPoint = P_2[i];
				float num4 = P_1[i];
				float num5 = num3 + sKPoint.X;
				float num6 = num5 + num4;
				if (num6 >= 0f && num5 <= length)
				{
					ushort num7 = P_0[i];
					SKPath path = glyphPathCache.GetPath(num7);
					if (path != null)
					{
						MorphPath(sKPath, path, sKPathMeasure, SKMatrix.CreateTranslation(num5, sKPoint.Y));
					}
				}
			}
			return sKPath;
		}
		static void MorphPath(SKPath sKPath3, SKPath sKPath2, SKPathMeasure sKPathMeasure2, in SKMatrix reference)
		{
			using SKPath.Iterator iterator = sKPath2.CreateIterator(false);
			Span<SKPoint> span = stackalloc SKPoint[4];
			Span<SKPoint> span2 = stackalloc SKPoint[4];
			SKPathVerb sKPathVerb;
			while ((sKPathVerb = iterator.Next(span)) != SKPathVerb.Done)
			{
				switch (sKPathVerb)
				{
				case SKPathVerb.Move:
					MorphPoints(span2, span, 1, sKPathMeasure2, in reference);
					sKPath3.MoveTo(span2[0]);
					break;
				case SKPathVerb.Line:
					span[0].X = (span[0].X + span[1].X) * 0.5f;
					span[0].Y = (span[0].Y + span[1].Y) * 0.5f;
					MorphPoints(span2, span, 2, sKPathMeasure2, in reference);
					sKPath3.QuadTo(span2[0], span2[1]);
					break;
				case SKPathVerb.Quad:
					MorphPoints(span2, span.Slice(1, 2), 2, sKPathMeasure2, in reference);
					sKPath3.QuadTo(span2[0], span2[1]);
					break;
				case SKPathVerb.Conic:
					MorphPoints(span2, span.Slice(1, 2), 2, sKPathMeasure2, in reference);
					sKPath3.ConicTo(span2[0], span2[1], iterator.ConicWeight());
					break;
				case SKPathVerb.Cubic:
					MorphPoints(span2, span.Slice(1, 3), 3, sKPathMeasure2, in reference);
					sKPath3.CubicTo(span2[0], span2[1], span2[2]);
					break;
				case SKPathVerb.Close:
					sKPath3.Close();
					break;
				}
			}
		}
		static void MorphPoints(Span<SKPoint> span2, Span<SKPoint> span, int num8, SKPathMeasure sKPathMeasure2, in SKMatrix reference)
		{
			for (int j = 0; j < num8; j++)
			{
				SKPoint sKPoint2 = reference.MapPoint(span[j].X, span[j].Y);
				if (!sKPathMeasure2.GetPositionAndTangent(sKPoint2.X, out var sKPoint3, out var empty))
				{
					empty = SKPoint.Empty;
				}
				span2[j] = new SKPoint(sKPoint3.X - empty.Y * sKPoint2.Y, sKPoint3.Y + empty.X * sKPoint2.Y);
			}
		}
	}

	private unsafe bool ValidateTextArgs(void* P_0, int P_1, SKTextEncoding P_2)
	{
		if (P_1 == 0)
		{
			return false;
		}
		if (P_0 == null)
		{
			throw new ArgumentNullException("text");
		}
		return true;
	}

	internal static SKFont GetObject(IntPtr P_0, bool P_1 = true)
	{
		return SKObject.GetOrAddObject(P_0, P_1, (IntPtr h, bool o) => new SKFont(h, o));
	}
}

