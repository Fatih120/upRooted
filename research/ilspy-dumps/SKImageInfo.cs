// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKImageInfo
using System;
using System.Runtime.CompilerServices;
using SkiaSharp;

public struct SKImageInfo : IEquatable<SKImageInfo>
{
	public static readonly SKColorType PlatformColorType;

	public static readonly int PlatformColorAlphaShift;

	public static readonly int PlatformColorRedShift;

	public static readonly int PlatformColorGreenShift;

	public static readonly int PlatformColorBlueShift;

	[CompilerGenerated]
	private int _003CWidth_003Ek__BackingField;

	[CompilerGenerated]
	private int _003CHeight_003Ek__BackingField;

	[CompilerGenerated]
	private SKColorType _003CColorType_003Ek__BackingField;

	[CompilerGenerated]
	private SKAlphaType _003CAlphaType_003Ek__BackingField;

	[CompilerGenerated]
	private SKColorSpace _003CColorSpace_003Ek__BackingField;

	public int Width
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CWidth_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CWidth_003Ek__BackingField = num;
		}
	}

	public int Height
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CHeight_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CHeight_003Ek__BackingField = num;
		}
	}

	public SKColorType ColorType
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CColorType_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CColorType_003Ek__BackingField = sKColorType;
		}
	}

	public SKAlphaType AlphaType
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CAlphaType_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CAlphaType_003Ek__BackingField = sKAlphaType;
		}
	}

	public SKColorSpace ColorSpace
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CColorSpace_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CColorSpace_003Ek__BackingField = sKColorSpace;
		}
	}

	public readonly int BytesPerPixel => ColorType.GetBytesPerPixel();

	public readonly int BytesSize => Width * Height * BytesPerPixel;

	public readonly int RowBytes => Width * BytesPerPixel;

	public readonly bool IsEmpty
	{
		get
		{
			if (Width > 0)
			{
				return Height <= 0;
			}
			return true;
		}
	}

	unsafe static SKImageInfo()
	{
		PlatformColorType = SkiaApi.sk_colortype_get_default_8888().FromNative();
		fixed (int* platformColorAlphaShift = &PlatformColorAlphaShift)
		{
			fixed (int* platformColorRedShift = &PlatformColorRedShift)
			{
				fixed (int* platformColorGreenShift = &PlatformColorGreenShift)
				{
					fixed (int* platformColorBlueShift = &PlatformColorBlueShift)
					{
						SkiaApi.sk_color_get_bit_shift(platformColorAlphaShift, platformColorRedShift, platformColorGreenShift, platformColorBlueShift);
					}
				}
			}
		}
	}

	public SKImageInfo(int P_0, int P_1)
	{
		Width = P_0;
		Height = P_1;
		ColorType = PlatformColorType;
		AlphaType = SKAlphaType.Premul;
		ColorSpace = null;
	}

	public SKImageInfo(int P_0, int P_1, SKColorType P_2)
	{
		Width = P_0;
		Height = P_1;
		ColorType = P_2;
		AlphaType = SKAlphaType.Premul;
		ColorSpace = null;
	}

	public SKImageInfo(int P_0, int P_1, SKColorType P_2, SKAlphaType P_3)
	{
		Width = P_0;
		Height = P_1;
		ColorType = P_2;
		AlphaType = P_3;
		ColorSpace = null;
	}

	public readonly SKImageInfo WithSize(int P_0, int P_1)
	{
		SKImageInfo result = this;
		result.Width = P_0;
		result.Height = P_1;
		return result;
	}

	public readonly SKImageInfo WithColorType(SKColorType P_0)
	{
		SKImageInfo result = this;
		result.ColorType = P_0;
		return result;
	}

	public readonly bool Equals(SKImageInfo P_0)
	{
		if (ColorSpace == P_0.ColorSpace && Width == P_0.Width && Height == P_0.Height && ColorType == P_0.ColorType)
		{
			return AlphaType == P_0.AlphaType;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKImageInfo sKImageInfo)
		{
			return Equals(sKImageInfo);
		}
		return false;
	}

	public static bool operator ==(SKImageInfo P_0, SKImageInfo P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKImageInfo P_0, SKImageInfo P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(ColorSpace);
		hashCode.Add(Width);
		hashCode.Add(Height);
		hashCode.Add(ColorType);
		hashCode.Add(AlphaType);
		return hashCode.ToHashCode();
	}
}

