// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKTextBlob
using System;
using SkiaSharp;

public class SKTextBlob : SKObject, ISKNonVirtualReferenceCounted, ISKReferenceCounted, ISKSkipObjectRegistration
{
	internal SKTextBlob(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	void ISKNonVirtualReferenceCounted.UnreferenceNative()
	{
		SkiaApi.sk_textblob_unref(Handle);
	}

	public static SKTextBlob Create(string P_0, SKFont P_1, SKPoint P_2 = default(SKPoint))
	{
		return Create(P_0.AsSpan(), P_1, P_2);
	}

	public unsafe static SKTextBlob Create(ReadOnlySpan<char> P_0, SKFont P_1, SKPoint P_2 = default(SKPoint))
	{
		fixed (char* ptr = P_0)
		{
			void* ptr2 = ptr;
			return Create(ptr2, P_0.Length * 2, SKTextEncoding.Utf16, P_1, P_2);
		}
	}

	internal unsafe static SKTextBlob Create(void* P_0, int P_1, SKTextEncoding P_2, SKFont P_3, SKPoint P_4)
	{
		if (P_3 == null)
		{
			throw new ArgumentNullException("font");
		}
		int num = P_3.CountGlyphs(P_0, P_1, P_2);
		if (num <= 0)
		{
			return null;
		}
		using SKTextBlobBuilder sKTextBlobBuilder = new SKTextBlobBuilder();
		SKPositionedRunBuffer sKPositionedRunBuffer = sKTextBlobBuilder.AllocatePositionedRun(P_3, num);
		P_3.GetGlyphs(P_0, P_1, P_2, sKPositionedRunBuffer.GetGlyphSpan());
		P_3.GetGlyphPositions(sKPositionedRunBuffer.GetGlyphSpan(), sKPositionedRunBuffer.GetPositionSpan(), P_4);
		return sKTextBlobBuilder.Build();
	}

	public static SKTextBlob CreatePositioned(string P_0, SKFont P_1, ReadOnlySpan<SKPoint> P_2)
	{
		return CreatePositioned(P_0.AsSpan(), P_1, P_2);
	}

	public unsafe static SKTextBlob CreatePositioned(ReadOnlySpan<char> P_0, SKFont P_1, ReadOnlySpan<SKPoint> P_2)
	{
		fixed (char* ptr = P_0)
		{
			void* ptr2 = ptr;
			return CreatePositioned(ptr2, P_0.Length * 2, SKTextEncoding.Utf16, P_1, P_2);
		}
	}

	internal unsafe static SKTextBlob CreatePositioned(void* P_0, int P_1, SKTextEncoding P_2, SKFont P_3, ReadOnlySpan<SKPoint> P_4)
	{
		if (P_3 == null)
		{
			throw new ArgumentNullException("font");
		}
		int num = P_3.CountGlyphs(P_0, P_1, P_2);
		if (num <= 0)
		{
			return null;
		}
		using SKTextBlobBuilder sKTextBlobBuilder = new SKTextBlobBuilder();
		SKPositionedRunBuffer sKPositionedRunBuffer = sKTextBlobBuilder.AllocatePositionedRun(P_3, num);
		P_3.GetGlyphs(P_0, P_1, P_2, sKPositionedRunBuffer.GetGlyphSpan());
		P_4.CopyTo(sKPositionedRunBuffer.GetPositionSpan());
		return sKTextBlobBuilder.Build();
	}

	public static SKTextBlob CreatePathPositioned(string P_0, SKFont P_1, SKPath P_2, SKTextAlign P_3 = SKTextAlign.Left, SKPoint P_4 = default(SKPoint))
	{
		return CreatePathPositioned(P_0.AsSpan(), P_1, P_2, P_3, P_4);
	}

	public unsafe static SKTextBlob CreatePathPositioned(ReadOnlySpan<char> P_0, SKFont P_1, SKPath P_2, SKTextAlign P_3 = SKTextAlign.Left, SKPoint P_4 = default(SKPoint))
	{
		fixed (char* ptr = P_0)
		{
			void* ptr2 = ptr;
			return CreatePathPositioned(ptr2, P_0.Length * 2, SKTextEncoding.Utf16, P_1, P_2, P_3, P_4);
		}
	}

	internal unsafe static SKTextBlob CreatePathPositioned(void* P_0, int P_1, SKTextEncoding P_2, SKFont P_3, SKPath P_4, SKTextAlign P_5 = SKTextAlign.Left, SKPoint P_6 = default(SKPoint))
	{
		if (P_3 == null)
		{
			throw new ArgumentNullException("font");
		}
		int num = P_3.CountGlyphs(P_0, P_1, P_2);
		if (num <= 0)
		{
			return null;
		}
		Utils.RentedArray<ushort> rentedArray = Utils.RentArray<ushort>(num);
		try
		{
			Utils.RentedArray<float> rentedArray2 = Utils.RentArray<float>(rentedArray.Length);
			try
			{
				Utils.RentedArray<SKPoint> rentedArray3 = Utils.RentArray<SKPoint>(rentedArray.Length);
				try
				{
					P_3.GetGlyphs(P_0, P_1, P_2, rentedArray);
					P_3.GetGlyphWidths(rentedArray, rentedArray2, Span<SKRect>.Empty);
					P_3.GetGlyphPositions(rentedArray, rentedArray3, P_6);
					using SKTextBlobBuilder sKTextBlobBuilder = new SKTextBlobBuilder();
					sKTextBlobBuilder.AddPathPositionedRun(rentedArray, P_3, rentedArray2, rentedArray3, P_4, P_5);
					return sKTextBlobBuilder.Build();
				}
				finally
				{
					rentedArray3.Dispose();
				}
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

	public float[] GetIntercepts(float P_0, float P_1, SKPaint P_2 = null)
	{
		int num = CountIntercepts(P_0, P_1, P_2);
		float[] array = new float[num];
		GetIntercepts(P_0, P_1, array, P_2);
		return array;
	}

	public unsafe void GetIntercepts(float P_0, float P_1, Span<float> P_2, SKPaint P_3 = null)
	{
		float* ptr = stackalloc float[2];
		*ptr = P_0;
		ptr[1] = P_1;
		fixed (float* ptr2 = P_2)
		{
			SkiaApi.sk_textblob_get_intercepts(Handle, ptr, ptr2, P_3?.Handle ?? IntPtr.Zero);
		}
	}

	public unsafe int CountIntercepts(float P_0, float P_1, SKPaint P_2 = null)
	{
		float* ptr = stackalloc float[2];
		*ptr = P_0;
		ptr[1] = P_1;
		return SkiaApi.sk_textblob_get_intercepts(Handle, ptr, null, P_2?.Handle ?? IntPtr.Zero);
	}

	internal static SKTextBlob GetObject(IntPtr P_0)
	{
		if (!(P_0 == IntPtr.Zero))
		{
			return new SKTextBlob(P_0, true);
		}
		return null;
	}
}

