// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKColorFilter
using System;
using SkiaSharp;

public class SKColorFilter : SKObject, ISKReferenceCounted
{
	internal SKColorFilter(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public static SKColorFilter CreateBlendMode(SKColor P_0, SKBlendMode P_1)
	{
		return GetObject(SkiaApi.sk_colorfilter_new_mode((uint)P_0, P_1));
	}

	public unsafe static SKColorFilter CreateColorMatrix(float[] P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("matrix");
		}
		if (P_0.Length != 20)
		{
			throw new ArgumentException("Matrix must have a length of 20.", "matrix");
		}
		fixed (float* ptr = P_0)
		{
			return GetObject(SkiaApi.sk_colorfilter_new_color_matrix(ptr));
		}
	}

	public static SKColorFilter CreateLumaColor()
	{
		return GetObject(SkiaApi.sk_colorfilter_new_luma_color());
	}

	public unsafe static SKColorFilter CreateTable(byte[] P_0, byte[] P_1, byte[] P_2, byte[] P_3)
	{
		if (P_0 != null && P_0.Length != 256)
		{
			throw new ArgumentException($"Table A must have a length of {256}.", "tableA");
		}
		if (P_1 != null && P_1.Length != 256)
		{
			throw new ArgumentException($"Table R must have a length of {256}.", "tableR");
		}
		if (P_2 != null && P_2.Length != 256)
		{
			throw new ArgumentException($"Table G must have a length of {256}.", "tableG");
		}
		if (P_3 != null && P_3.Length != 256)
		{
			throw new ArgumentException($"Table B must have a length of {256}.", "tableB");
		}
		fixed (byte* ptr = P_0)
		{
			fixed (byte* ptr2 = P_1)
			{
				fixed (byte* ptr3 = P_2)
				{
					fixed (byte* ptr4 = P_3)
					{
						return GetObject(SkiaApi.sk_colorfilter_new_table_argb(ptr, ptr2, ptr3, ptr4));
					}
				}
			}
		}
	}

	internal static SKColorFilter GetObject(IntPtr P_0)
	{
		return SKObject.GetOrAddObject(P_0, (IntPtr h, bool o) => new SKColorFilter(h, o));
	}
}

