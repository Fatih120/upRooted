// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKPathEffect
using System;
using SkiaSharp;

public class SKPathEffect : SKObject, ISKReferenceCounted
{
	internal SKPathEffect(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public unsafe static SKPathEffect CreateDash(float[] P_0, float P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("intervals");
		}
		if (P_0.Length % 2 != 0)
		{
			throw new ArgumentException("The intervals must have an even number of entries.", "intervals");
		}
		fixed (float* ptr = P_0)
		{
			return GetObject(SkiaApi.sk_path_effect_create_dash(ptr, P_0.Length, P_1));
		}
	}

	internal static SKPathEffect GetObject(IntPtr P_0)
	{
		return SKObject.GetOrAddObject(P_0, (IntPtr h, bool o) => new SKPathEffect(h, o));
	}
}

