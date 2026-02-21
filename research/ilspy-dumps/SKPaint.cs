// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKPaint
using System;
using SkiaSharp;

public class SKPaint : SKObject, ISKSkipObjectRegistration
{
	private SKFont font;

	private bool lcdRenderText;

	public bool IsAntialias
	{
		get
		{
			return SkiaApi.sk_paint_is_antialias(Handle);
		}
		set
		{
			SkiaApi.sk_paint_set_antialias(Handle, flag);
			UpdateFontEdging(flag);
		}
	}

	public bool IsDither
	{
		set
		{
			SkiaApi.sk_paint_set_dither(Handle, flag);
		}
	}

	public bool SubpixelText
	{
		get
		{
			return GetFont().Subpixel;
		}
		set
		{
			GetFont().Subpixel = subpixel;
		}
	}

	public bool LcdRenderText
	{
		get
		{
			return lcdRenderText;
		}
		set
		{
			lcdRenderText = flag;
			UpdateFontEdging(IsAntialias);
		}
	}

	public bool FakeBoldText
	{
		get
		{
			return GetFont().Embolden;
		}
		set
		{
			GetFont().Embolden = embolden;
		}
	}

	public bool IsStroke
	{
		set
		{
			Style = (flag ? SKPaintStyle.Stroke : SKPaintStyle.Fill);
		}
	}

	public SKPaintStyle Style
	{
		set
		{
			SkiaApi.sk_paint_set_style(Handle, sKPaintStyle);
		}
	}

	public SKColor Color
	{
		set
		{
			SkiaApi.sk_paint_set_color(Handle, (uint)sKColor);
		}
	}

	public unsafe SKColorF ColorF
	{
		set
		{
			SkiaApi.sk_paint_set_color4f(Handle, &sKColorF, IntPtr.Zero);
		}
	}

	public float StrokeWidth
	{
		set
		{
			SkiaApi.sk_paint_set_stroke_width(Handle, num);
		}
	}

	public float StrokeMiter
	{
		set
		{
			SkiaApi.sk_paint_set_stroke_miter(Handle, num);
		}
	}

	public SKStrokeCap StrokeCap
	{
		set
		{
			SkiaApi.sk_paint_set_stroke_cap(Handle, sKStrokeCap);
		}
	}

	public SKStrokeJoin StrokeJoin
	{
		set
		{
			SkiaApi.sk_paint_set_stroke_join(Handle, sKStrokeJoin);
		}
	}

	public SKShader Shader
	{
		set
		{
			SkiaApi.sk_paint_set_shader(Handle, sKShader?.Handle ?? IntPtr.Zero);
		}
	}

	public SKColorFilter ColorFilter
	{
		set
		{
			SkiaApi.sk_paint_set_colorfilter(Handle, sKColorFilter?.Handle ?? IntPtr.Zero);
		}
	}

	public SKImageFilter ImageFilter
	{
		set
		{
			SkiaApi.sk_paint_set_imagefilter(Handle, sKImageFilter?.Handle ?? IntPtr.Zero);
		}
	}

	public SKBlendMode BlendMode
	{
		set
		{
			SkiaApi.sk_paint_set_blendmode(Handle, sKBlendMode);
		}
	}

	public SKFilterQuality FilterQuality
	{
		set
		{
			SkiaApi.sk_paint_set_filter_quality(Handle, sKFilterQuality);
		}
	}

	public SKTypeface Typeface
	{
		get
		{
			return GetFont().Typeface;
		}
		set
		{
			GetFont().Typeface = typeface;
		}
	}

	public float TextSize
	{
		get
		{
			return GetFont().Size;
		}
		set
		{
			GetFont().Size = size;
		}
	}

	public SKTextAlign TextAlign
	{
		get
		{
			return SkiaApi.sk_compatpaint_get_text_align(Handle);
		}
		set
		{
			SkiaApi.sk_compatpaint_set_text_align(Handle, sKTextAlign);
		}
	}

	public SKTextEncoding TextEncoding
	{
		set
		{
			SkiaApi.sk_compatpaint_set_text_encoding(Handle, sKTextEncoding);
		}
	}

	public float TextScaleX => GetFont().ScaleX;

	public float TextSkewX => GetFont().SkewX;

	public SKPathEffect PathEffect
	{
		get
		{
			return SKPathEffect.GetObject(SkiaApi.sk_paint_get_path_effect(Handle));
		}
		set
		{
			SkiaApi.sk_paint_set_path_effect(Handle, sKPathEffect?.Handle ?? IntPtr.Zero);
		}
	}

	internal SKPaint(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKPaint()
		: this(SkiaApi.sk_compatpaint_new(), true)
	{
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKPaint instance.");
		}
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_compatpaint_delete(Handle);
	}

	public void Reset()
	{
		SkiaApi.sk_compatpaint_reset(Handle);
	}

	public float GetFontMetrics(out SKFontMetrics P_0)
	{
		return GetFont().GetFontMetrics(out P_0);
	}

	public float MeasureText(string P_0)
	{
		return GetFont().MeasureText(P_0, this);
	}

	public float MeasureText(string P_0, ref SKRect P_1)
	{
		return GetFont().MeasureText(P_0, out P_1, this);
	}

	public SKPath GetTextPath(string P_0, float P_1, float P_2)
	{
		return GetFont().GetTextPath(P_0, new SKPoint(P_1, P_2));
	}

	public bool GetFillPath(SKPath P_0, SKPath P_1)
	{
		return GetFillPath(P_0, P_1, 1f);
	}

	public unsafe bool GetFillPath(SKPath P_0, SKPath P_1, float P_2)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("src");
		}
		if (P_1 == null)
		{
			throw new ArgumentNullException("dst");
		}
		return SkiaApi.sk_paint_get_fill_path(Handle, P_0.Handle, P_1.Handle, null, P_2);
	}

	public SKFont ToFont()
	{
		return SKFont.GetObject(SkiaApi.sk_compatpaint_make_font(Handle));
	}

	internal SKFont GetFont()
	{
		return font ?? (font = SKObject.OwnedBy(SKFont.GetObject(SkiaApi.sk_compatpaint_get_font(Handle), false), this));
	}

	private void UpdateFontEdging(bool P_0)
	{
		SKFontEdging edging = SKFontEdging.Alias;
		if (P_0)
		{
			edging = ((!lcdRenderText) ? SKFontEdging.Antialias : SKFontEdging.SubpixelAntialias);
		}
		GetFont().Edging = edging;
	}
}

