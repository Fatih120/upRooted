// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKColorSpaceXyz
using System;
using System.Reflection;
using SkiaSharp;

[DefaultMember("Item")]
public struct SKColorSpaceXyz : IEquatable<SKColorSpaceXyz>
{
	private float fM00;

	private float fM01;

	private float fM02;

	private float fM10;

	private float fM11;

	private float fM12;

	private float fM20;

	private float fM21;

	private float fM22;

	public unsafe static SKColorSpaceXyz Srgb
	{
		get
		{
			SKColorSpaceXyz result = default(SKColorSpaceXyz);
			SkiaApi.sk_colorspace_xyz_named_srgb(&result);
			return result;
		}
	}

	public readonly bool Equals(SKColorSpaceXyz P_0)
	{
		if (fM00 == P_0.fM00 && fM01 == P_0.fM01 && fM02 == P_0.fM02 && fM10 == P_0.fM10 && fM11 == P_0.fM11 && fM12 == P_0.fM12 && fM20 == P_0.fM20 && fM21 == P_0.fM21)
		{
			return fM22 == P_0.fM22;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKColorSpaceXyz sKColorSpaceXyz)
		{
			return Equals(sKColorSpaceXyz);
		}
		return false;
	}

	public static bool operator ==(SKColorSpaceXyz P_0, SKColorSpaceXyz P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKColorSpaceXyz P_0, SKColorSpaceXyz P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fM00);
		hashCode.Add(fM01);
		hashCode.Add(fM02);
		hashCode.Add(fM10);
		hashCode.Add(fM11);
		hashCode.Add(fM12);
		hashCode.Add(fM20);
		hashCode.Add(fM21);
		hashCode.Add(fM22);
		return hashCode.ToHashCode();
	}
}

