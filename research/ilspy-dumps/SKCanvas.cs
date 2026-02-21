// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKCanvas
using System;
using SkiaSharp;

public class SKCanvas : SKObject
{
	public unsafe SKMatrix TotalMatrix
	{
		get
		{
			SKMatrix result = default(SKMatrix);
			SkiaApi.sk_canvas_get_total_matrix(Handle, &result);
			return result;
		}
	}

	internal SKCanvas(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKCanvas(SKBitmap P_0)
		: this(IntPtr.Zero, true)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("bitmap");
		}
		Handle = SkiaApi.sk_canvas_new_from_bitmap(P_0.Handle);
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_canvas_destroy(Handle);
	}

	public int Save()
	{
		if (Handle == IntPtr.Zero)
		{
			throw new ObjectDisposedException("SKCanvas");
		}
		return SkiaApi.sk_canvas_save(Handle);
	}

	public unsafe int SaveLayer(SKRect P_0, SKPaint P_1)
	{
		return SkiaApi.sk_canvas_save_layer(Handle, &P_0, P_1?.Handle ?? IntPtr.Zero);
	}

	public unsafe int SaveLayer(SKPaint P_0)
	{
		return SkiaApi.sk_canvas_save_layer(Handle, null, P_0?.Handle ?? IntPtr.Zero);
	}

	public int SaveLayer()
	{
		return SaveLayer(null);
	}

	public void DrawLine(float P_0, float P_1, float P_2, float P_3, SKPaint P_4)
	{
		if (P_4 == null)
		{
			throw new ArgumentNullException("paint");
		}
		SkiaApi.sk_canvas_draw_line(Handle, P_0, P_1, P_2, P_3, P_4.Handle);
	}

	public void Clear()
	{
		Clear(SKColors.Empty);
	}

	public void Clear(SKColor P_0)
	{
		SkiaApi.sk_canvas_clear(Handle, (uint)P_0);
	}

	public void Restore()
	{
		SkiaApi.sk_canvas_restore(Handle);
	}

	public void RestoreToCount(int P_0)
	{
		SkiaApi.sk_canvas_restore_to_count(Handle, P_0);
	}

	public void Scale(float P_0)
	{
		if (P_0 != 1f)
		{
			SkiaApi.sk_canvas_scale(Handle, P_0, P_0);
		}
	}

	public unsafe void ClipRect(SKRect P_0, SKClipOperation P_1 = SKClipOperation.Intersect, bool P_2 = false)
	{
		SkiaApi.sk_canvas_clip_rect_with_operation(Handle, &P_0, P_1, P_2);
	}

	public void ClipRoundRect(SKRoundRect P_0, SKClipOperation P_1 = SKClipOperation.Intersect, bool P_2 = false)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("rect");
		}
		SkiaApi.sk_canvas_clip_rrect_with_operation(Handle, P_0.Handle, P_1, P_2);
	}

	public void ClipPath(SKPath P_0, SKClipOperation P_1 = SKClipOperation.Intersect, bool P_2 = false)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("path");
		}
		SkiaApi.sk_canvas_clip_path_with_operation(Handle, P_0.Handle, P_1, P_2);
	}

	public void ClipRegion(SKRegion P_0, SKClipOperation P_1 = SKClipOperation.Intersect)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("region");
		}
		SkiaApi.sk_canvas_clip_region(Handle, P_0.Handle, P_1);
	}

	public void DrawPaint(SKPaint P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("paint");
		}
		SkiaApi.sk_canvas_draw_paint(Handle, P_0.Handle);
	}

	public void DrawRegion(SKRegion P_0, SKPaint P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("region");
		}
		if (P_1 == null)
		{
			throw new ArgumentNullException("paint");
		}
		SkiaApi.sk_canvas_draw_region(Handle, P_0.Handle, P_1.Handle);
	}

	public unsafe void DrawRect(SKRect P_0, SKPaint P_1)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("paint");
		}
		SkiaApi.sk_canvas_draw_rect(Handle, &P_0, P_1.Handle);
	}

	public void DrawRoundRect(SKRoundRect P_0, SKPaint P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("rect");
		}
		if (P_1 == null)
		{
			throw new ArgumentNullException("paint");
		}
		SkiaApi.sk_canvas_draw_rrect(Handle, P_0.Handle, P_1.Handle);
	}

	public void DrawPath(SKPath P_0, SKPaint P_1)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("paint");
		}
		if (P_0 == null)
		{
			throw new ArgumentNullException("path");
		}
		SkiaApi.sk_canvas_draw_path(Handle, P_0.Handle, P_1.Handle);
	}

	public unsafe void DrawImage(SKImage P_0, SKRect P_1, SKPaint P_2 = null)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("image");
		}
		SkiaApi.sk_canvas_draw_image_rect(Handle, P_0.Handle, null, &P_1, P_2?.Handle ?? IntPtr.Zero);
	}

	public unsafe void DrawImage(SKImage P_0, SKRect P_1, SKRect P_2, SKPaint P_3 = null)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("image");
		}
		SkiaApi.sk_canvas_draw_image_rect(Handle, P_0.Handle, &P_1, &P_2, P_3?.Handle ?? IntPtr.Zero);
	}

	public unsafe void DrawPicture(SKPicture P_0, SKPaint P_1 = null)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("picture");
		}
		SkiaApi.sk_canvas_draw_picture(Handle, P_0.Handle, null, P_1?.Handle ?? IntPtr.Zero);
	}

	public void DrawBitmap(SKBitmap P_0, SKRect P_1, SKPaint P_2 = null)
	{
		using SKImage sKImage = SKImage.FromBitmap(P_0);
		DrawImage(sKImage, P_1, P_2);
	}

	public void DrawText(SKTextBlob P_0, float P_1, float P_2, SKPaint P_3)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("text");
		}
		if (P_3 == null)
		{
			throw new ArgumentNullException("paint");
		}
		SkiaApi.sk_canvas_draw_text_blob(Handle, P_0.Handle, P_1, P_2, P_3.Handle);
	}

	public void DrawText(string P_0, float P_1, float P_2, SKPaint P_3)
	{
		DrawText(P_0, P_1, P_2, P_3.GetFont(), P_3);
	}

	public void DrawText(string P_0, float P_1, float P_2, SKFont P_3, SKPaint P_4)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("text");
		}
		if (P_3 == null)
		{
			throw new ArgumentNullException("font");
		}
		if (P_4 == null)
		{
			throw new ArgumentNullException("paint");
		}
		if (P_4.TextAlign != SKTextAlign.Left)
		{
			float num = P_3.MeasureText(P_0);
			if (P_4.TextAlign == SKTextAlign.Center)
			{
				num *= 0.5f;
			}
			P_1 -= num;
		}
		using SKTextBlob sKTextBlob = SKTextBlob.Create(P_0, P_3);
		if (sKTextBlob != null)
		{
			DrawText(sKTextBlob, P_1, P_2, P_4);
		}
	}

	public void DrawTextOnPath(string P_0, SKPath P_1, float P_2, float P_3, SKPaint P_4)
	{
		DrawTextOnPath(P_0, P_1, new SKPoint(P_2, P_3), true, P_4);
	}

	public void DrawTextOnPath(string P_0, SKPath P_1, SKPoint P_2, bool P_3, SKPaint P_4)
	{
		if (P_4 == null)
		{
			throw new ArgumentNullException("paint");
		}
		DrawTextOnPath(P_0, P_1, P_2, P_3, P_4.GetFont(), P_4);
	}

	public void DrawTextOnPath(string P_0, SKPath P_1, SKPoint P_2, bool P_3, SKFont P_4, SKPaint P_5)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("text");
		}
		if (P_1 == null)
		{
			throw new ArgumentNullException("path");
		}
		if (P_4 == null)
		{
			throw new ArgumentNullException("font");
		}
		if (P_5 == null)
		{
			throw new ArgumentNullException("paint");
		}
		if (P_3)
		{
			using (SKPath sKPath = P_4.GetTextPathOnPath(P_0, P_1, P_5.TextAlign, P_2))
			{
				DrawPath(sKPath, P_5);
				return;
			}
		}
		using SKTextBlob sKTextBlob = SKTextBlob.CreatePathPositioned(P_0, P_4, P_1, P_5.TextAlign, P_2);
		if (sKTextBlob != null)
		{
			DrawText(sKTextBlob, 0f, 0f, P_5);
		}
	}

	public void Flush()
	{
		SkiaApi.sk_canvas_flush(Handle);
	}

	public void ResetMatrix()
	{
		SkiaApi.sk_canvas_reset_matrix(Handle);
	}

	public unsafe void SetMatrix(SKMatrix P_0)
	{
		SkiaApi.sk_canvas_set_matrix(Handle, &P_0);
	}

	public void DrawRoundRectDifference(SKRoundRect P_0, SKRoundRect P_1, SKPaint P_2)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("outer");
		}
		if (P_1 == null)
		{
			throw new ArgumentNullException("inner");
		}
		if (P_2 == null)
		{
			throw new ArgumentNullException("paint");
		}
		SkiaApi.sk_canvas_draw_drrect(Handle, P_0.Handle, P_1.Handle, P_2.Handle);
	}

	internal static SKCanvas GetObject(IntPtr P_0, bool P_1 = true, bool P_2 = true)
	{
		return SKObject.GetOrAddObject(P_0, P_1, P_2, (IntPtr h, bool o) => new SKCanvas(h, o));
	}
}

