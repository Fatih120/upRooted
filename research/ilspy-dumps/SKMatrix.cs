// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKMatrix
using System;
using SkiaSharp;

public struct SKMatrix : IEquatable<SKMatrix>
{
	private float scaleX;

	private float skewX;

	private float transX;

	private float skewY;

	private float scaleY;

	private float transY;

	private float persp0;

	private float persp1;

	private float persp2;

	public static readonly SKMatrix Identity = new SKMatrix
	{
		scaleX = 1f,
		scaleY = 1f,
		persp2 = 1f
	};

	public float ScaleX
	{
		readonly get
		{
			return scaleX;
		}
		set
		{
			scaleX = num;
		}
	}

	public float SkewX
	{
		readonly get
		{
			return skewX;
		}
		set
		{
			skewX = num;
		}
	}

	public float TransX
	{
		readonly get
		{
			return transX;
		}
		set
		{
			transX = num;
		}
	}

	public float SkewY
	{
		readonly get
		{
			return skewY;
		}
		set
		{
			skewY = num;
		}
	}

	public float ScaleY
	{
		readonly get
		{
			return scaleY;
		}
		set
		{
			scaleY = num;
		}
	}

	public float TransY
	{
		readonly get
		{
			return transY;
		}
		set
		{
			transY = num;
		}
	}

	public float Persp0
	{
		readonly get
		{
			return persp0;
		}
		set
		{
			persp0 = num;
		}
	}

	public float Persp1
	{
		readonly get
		{
			return persp1;
		}
		set
		{
			persp1 = num;
		}
	}

	public float Persp2
	{
		readonly get
		{
			return persp2;
		}
		set
		{
			persp2 = num;
		}
	}

	public readonly bool Equals(SKMatrix P_0)
	{
		if (scaleX == P_0.scaleX && skewX == P_0.skewX && transX == P_0.transX && skewY == P_0.skewY && scaleY == P_0.scaleY && transY == P_0.transY && persp0 == P_0.persp0 && persp1 == P_0.persp1)
		{
			return persp2 == P_0.persp2;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKMatrix sKMatrix)
		{
			return Equals(sKMatrix);
		}
		return false;
	}

	public static bool operator ==(SKMatrix P_0, SKMatrix P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKMatrix P_0, SKMatrix P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(scaleX);
		hashCode.Add(skewX);
		hashCode.Add(transX);
		hashCode.Add(skewY);
		hashCode.Add(scaleY);
		hashCode.Add(transY);
		hashCode.Add(persp0);
		hashCode.Add(persp1);
		hashCode.Add(persp2);
		return hashCode.ToHashCode();
	}

	public SKMatrix(float P_0, float P_1, float P_2, float P_3, float P_4, float P_5, float P_6, float P_7, float P_8)
	{
		scaleX = P_0;
		skewX = P_1;
		transX = P_2;
		skewY = P_3;
		scaleY = P_4;
		transY = P_5;
		persp0 = P_6;
		persp1 = P_7;
		persp2 = P_8;
	}

	public static SKMatrix CreateIdentity()
	{
		return new SKMatrix
		{
			scaleX = 1f,
			scaleY = 1f,
			persp2 = 1f
		};
	}

	public static SKMatrix CreateTranslation(float P_0, float P_1)
	{
		if (P_0 == 0f && P_1 == 0f)
		{
			return Identity;
		}
		return new SKMatrix
		{
			scaleX = 1f,
			scaleY = 1f,
			transX = P_0,
			transY = P_1,
			persp2 = 1f
		};
	}

	public static SKMatrix CreateScale(float P_0, float P_1)
	{
		if (P_0 == 1f && P_1 == 1f)
		{
			return Identity;
		}
		return new SKMatrix
		{
			scaleX = P_0,
			scaleY = P_1,
			persp2 = 1f
		};
	}

	public static SKMatrix CreateRotation(float P_0, float P_1, float P_2)
	{
		if (P_0 == 0f)
		{
			return Identity;
		}
		float num = (float)Math.Sin(P_0);
		float num2 = (float)Math.Cos(P_0);
		SKMatrix identity = Identity;
		SetSinCos(ref identity, num, num2, P_1, P_2);
		return identity;
	}

	public static SKMatrix CreateRotationDegrees(float P_0, float P_1, float P_2)
	{
		if (P_0 == 0f)
		{
			return Identity;
		}
		return CreateRotation(P_0 * ((float)Math.PI / 180f), P_1, P_2);
	}

	public unsafe readonly SKMatrix PreConcat(SKMatrix P_0)
	{
		SKMatrix result = this;
		SkiaApi.sk_matrix_pre_concat(&result, &P_0);
		return result;
	}

	public unsafe static void Concat(ref SKMatrix P_0, SKMatrix P_1, SKMatrix P_2)
	{
		fixed (SKMatrix* ptr = &P_0)
		{
			SkiaApi.sk_matrix_concat(ptr, &P_1, &P_2);
		}
	}

	public unsafe readonly SKPoint MapPoint(float P_0, float P_1)
	{
		SKPoint result = default(SKPoint);
		fixed (SKMatrix* ptr = &this)
		{
			SkiaApi.sk_matrix_map_xy(ptr, P_0, P_1, &result);
		}
		return result;
	}

	private static void SetSinCos(ref SKMatrix P_0, float P_1, float P_2, float P_3, float P_4)
	{
		float num = 1f - P_2;
		P_0.scaleX = P_2;
		P_0.skewX = 0f - P_1;
		P_0.transX = Dot(P_1, P_4, num, P_3);
		P_0.skewY = P_1;
		P_0.scaleY = P_2;
		P_0.transY = Dot(0f - P_1, P_3, num, P_4);
		P_0.persp0 = 0f;
		P_0.persp1 = 0f;
		P_0.persp2 = 1f;
	}

	private static float Dot(float P_0, float P_1, float P_2, float P_3)
	{
		return P_0 * P_1 + P_2 * P_3;
	}
}

