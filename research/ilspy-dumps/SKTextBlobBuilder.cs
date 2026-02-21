// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKTextBlobBuilder
using System;
using SkiaSharp;

public class SKTextBlobBuilder : SKObject, ISKSkipObjectRegistration
{
	internal SKTextBlobBuilder(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKTextBlobBuilder()
		: this(SkiaApi.sk_textblob_builder_new(), true)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_textblob_builder_delete(Handle);
	}

	public SKTextBlob Build()
	{
		SKTextBlob result = SKTextBlob.GetObject(SkiaApi.sk_textblob_builder_make(Handle));
		GC.KeepAlive(this);
		return result;
	}

	public void AddRotationScaleRun(ReadOnlySpan<ushort> P_0, SKFont P_1, ReadOnlySpan<SKRotationScaleMatrix> P_2)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("font");
		}
		SKRotationScaleRunBuffer sKRotationScaleRunBuffer = AllocateRotationScaleRun(P_1, P_0.Length);
		P_0.CopyTo(sKRotationScaleRunBuffer.GetGlyphSpan());
		P_2.CopyTo(sKRotationScaleRunBuffer.GetRotationScaleSpan());
	}

	public void AddPathPositionedRun(ReadOnlySpan<ushort> P_0, SKFont P_1, ReadOnlySpan<float> P_2, ReadOnlySpan<SKPoint> P_3, SKPath P_4, SKTextAlign P_5 = SKTextAlign.Left)
	{
		using SKPathMeasure sKPathMeasure = new SKPathMeasure(P_4);
		float length = sKPathMeasure.Length;
		float num = P_3[P_0.Length - 1].X + P_2[P_0.Length - 1];
		float num2 = (float)P_5 * 0.5f;
		float num3 = P_3[0].X + (length - num) * num2;
		int start = 0;
		int num4 = 0;
		Utils.RentedArray<SKRotationScaleMatrix> rentedArray = Utils.RentArray<SKRotationScaleMatrix>(P_0.Length);
		try
		{
			for (int i = 0; i < P_3.Length; i++)
			{
				SKPoint sKPoint = P_3[i];
				float num5 = P_2[i] * 0.5f;
				float num6 = num3 + sKPoint.X + num5;
				if (num6 >= 0f && num6 < length && sKPathMeasure.GetPositionAndTangent(num6, out var sKPoint2, out var sKPoint3))
				{
					if (num4 == 0)
					{
						start = i;
					}
					float x = sKPoint3.X;
					float y = sKPoint3.Y;
					float x2 = sKPoint2.X;
					float y2 = sKPoint2.Y;
					x2 -= x * num5;
					y2 -= y * num5;
					float y3 = sKPoint.Y;
					x2 -= y3 * y;
					y2 += y3 * x;
					rentedArray.Span[num4++] = new SKRotationScaleMatrix(x, y, x2, y2);
				}
			}
			ReadOnlySpan<ushort> readOnlySpan = P_0.Slice(start, num4);
			Span<SKRotationScaleMatrix> span = rentedArray.Span.Slice(0, num4);
			AddRotationScaleRun(readOnlySpan, P_1, span);
		}
		finally
		{
			rentedArray.Dispose();
		}
	}

	public unsafe SKPositionedRunBuffer AllocatePositionedRun(SKFont P_0, int P_1, SKRect? P_2 = null)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("font");
		}
		SKRunBufferInternal sKRunBufferInternal = default(SKRunBufferInternal);
		if (P_2.HasValue)
		{
			SKRect valueOrDefault = P_2.GetValueOrDefault();
			SkiaApi.sk_textblob_builder_alloc_run_pos(Handle, P_0.Handle, P_1, &valueOrDefault, &sKRunBufferInternal);
		}
		else
		{
			SkiaApi.sk_textblob_builder_alloc_run_pos(Handle, P_0.Handle, P_1, null, &sKRunBufferInternal);
		}
		return new SKPositionedRunBuffer(sKRunBufferInternal, P_1);
	}

	public unsafe SKRotationScaleRunBuffer AllocateRotationScaleRun(SKFont P_0, int P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("font");
		}
		SKRunBufferInternal sKRunBufferInternal = default(SKRunBufferInternal);
		SkiaApi.sk_textblob_builder_alloc_run_rsxform(Handle, P_0.Handle, P_1, &sKRunBufferInternal);
		return new SKRotationScaleRunBuffer(sKRunBufferInternal, P_1);
	}
}

