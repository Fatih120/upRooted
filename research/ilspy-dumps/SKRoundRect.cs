// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKRoundRect
using System;
using SkiaSharp;

public class SKRoundRect : SKObject, ISKSkipObjectRegistration
{
	public unsafe SKRect Rect
	{
		get
		{
			SKRect result = default(SKRect);
			SkiaApi.sk_rrect_get_rect(Handle, &result);
			return result;
		}
	}

	public SKPoint[] Radii => new SKPoint[4]
	{
		GetRadii(SKRoundRectCorner.UpperLeft),
		GetRadii(SKRoundRectCorner.UpperRight),
		GetRadii(SKRoundRectCorner.LowerRight),
		GetRadii(SKRoundRectCorner.LowerLeft)
	};

	internal SKRoundRect(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKRoundRect()
		: this(SkiaApi.sk_rrect_new(), true)
	{
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKRoundRect instance.");
		}
		SetEmpty();
	}

	public SKRoundRect(SKRect P_0)
		: this(SkiaApi.sk_rrect_new(), true)
	{
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKRoundRect instance.");
		}
		SetRect(P_0);
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_rrect_delete(Handle);
	}

	public void SetEmpty()
	{
		SkiaApi.sk_rrect_set_empty(Handle);
	}

	public unsafe void SetRect(SKRect P_0)
	{
		SkiaApi.sk_rrect_set_rect(Handle, &P_0);
	}

	public unsafe void SetRectRadii(SKRect P_0, SKPoint[] P_1)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("radii");
		}
		if (P_1.Length != 4)
		{
			throw new ArgumentException("Radii must have a length of 4.", "radii");
		}
		fixed (SKPoint* ptr = P_1)
		{
			SkiaApi.sk_rrect_set_rect_radii(Handle, &P_0, ptr);
		}
	}

	public unsafe SKPoint GetRadii(SKRoundRectCorner P_0)
	{
		SKPoint result = default(SKPoint);
		SkiaApi.sk_rrect_get_radii(Handle, P_0, &result);
		return result;
	}

	public void Deflate(float P_0, float P_1)
	{
		SkiaApi.sk_rrect_inset(Handle, P_0, P_1);
	}

	public void Inflate(float P_0, float P_1)
	{
		SkiaApi.sk_rrect_outset(Handle, P_0, P_1);
	}
}

