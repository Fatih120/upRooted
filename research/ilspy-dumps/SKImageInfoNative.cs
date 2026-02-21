// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKImageInfoNative
using System;
using SkiaSharp;

internal struct SKImageInfoNative : IEquatable<SKImageInfoNative>
{
	public IntPtr colorspace;

	public int width;

	public int height;

	public SKColorTypeNative colorType;

	public SKAlphaType alphaType;

	public readonly bool Equals(SKImageInfoNative P_0)
	{
		if (colorspace == P_0.colorspace && width == P_0.width && height == P_0.height && colorType == P_0.colorType)
		{
			return alphaType == P_0.alphaType;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKImageInfoNative sKImageInfoNative)
		{
			return Equals(sKImageInfoNative);
		}
		return false;
	}

	public static bool operator ==(SKImageInfoNative P_0, SKImageInfoNative P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKImageInfoNative P_0, SKImageInfoNative P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(colorspace);
		hashCode.Add(width);
		hashCode.Add(height);
		hashCode.Add(colorType);
		hashCode.Add(alphaType);
		return hashCode.ToHashCode();
	}

	public static SKImageInfoNative FromManaged(ref SKImageInfo P_0)
	{
		return new SKImageInfoNative
		{
			colorspace = (P_0.ColorSpace?.Handle ?? IntPtr.Zero),
			width = P_0.Width,
			height = P_0.Height,
			colorType = P_0.ColorType.ToNative(),
			alphaType = P_0.AlphaType
		};
	}

	public static SKImageInfo ToManaged(ref SKImageInfoNative P_0)
	{
		return new SKImageInfo
		{
			ColorSpace = SKColorSpace.GetObject(P_0.colorspace),
			Width = P_0.width,
			Height = P_0.height,
			ColorType = P_0.colorType.FromNative(),
			AlphaType = P_0.alphaType
		};
	}
}

