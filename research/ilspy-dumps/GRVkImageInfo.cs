// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRVkImageInfo
using System;
using SkiaSharp;

public struct GRVkImageInfo : IEquatable<GRVkImageInfo>
{
	private ulong fImage;

	private GRVkAlloc fAlloc;

	private uint fImageTiling;

	private uint fImageLayout;

	private uint fFormat;

	private uint fImageUsageFlags;

	private uint fSampleCount;

	private uint fLevelCount;

	private uint fCurrentQueueFamily;

	private byte fProtected;

	private GrVkYcbcrConversionInfo fYcbcrConversionInfo;

	private uint fSharingMode;

	public ulong Image
	{
		set
		{
			fImage = num;
		}
	}

	public GRVkAlloc Alloc
	{
		set
		{
			fAlloc = gRVkAlloc;
		}
	}

	public uint ImageTiling
	{
		set
		{
			fImageTiling = num;
		}
	}

	public uint ImageLayout
	{
		set
		{
			fImageLayout = num;
		}
	}

	public uint Format
	{
		set
		{
			fFormat = num;
		}
	}

	public uint ImageUsageFlags
	{
		set
		{
			fImageUsageFlags = num;
		}
	}

	public uint SampleCount
	{
		set
		{
			fSampleCount = num;
		}
	}

	public uint LevelCount
	{
		set
		{
			fLevelCount = num;
		}
	}

	public uint CurrentQueueFamily
	{
		set
		{
			fCurrentQueueFamily = num;
		}
	}

	public bool Protected
	{
		set
		{
			fProtected = (flag ? ((byte)1) : ((byte)0));
		}
	}

	public uint SharingMode
	{
		set
		{
			fSharingMode = num;
		}
	}

	public readonly bool Equals(GRVkImageInfo P_0)
	{
		if (fImage == P_0.fImage && fAlloc == P_0.fAlloc && fImageTiling == P_0.fImageTiling && fImageLayout == P_0.fImageLayout && fFormat == P_0.fFormat && fImageUsageFlags == P_0.fImageUsageFlags && fSampleCount == P_0.fSampleCount && fLevelCount == P_0.fLevelCount && fCurrentQueueFamily == P_0.fCurrentQueueFamily && fProtected == P_0.fProtected && fYcbcrConversionInfo == P_0.fYcbcrConversionInfo)
		{
			return fSharingMode == P_0.fSharingMode;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is GRVkImageInfo gRVkImageInfo)
		{
			return Equals(gRVkImageInfo);
		}
		return false;
	}

	public static bool operator ==(GRVkImageInfo P_0, GRVkImageInfo P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(GRVkImageInfo P_0, GRVkImageInfo P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fImage);
		hashCode.Add(fAlloc);
		hashCode.Add(fImageTiling);
		hashCode.Add(fImageLayout);
		hashCode.Add(fFormat);
		hashCode.Add(fImageUsageFlags);
		hashCode.Add(fSampleCount);
		hashCode.Add(fLevelCount);
		hashCode.Add(fCurrentQueueFamily);
		hashCode.Add(fProtected);
		hashCode.Add(fYcbcrConversionInfo);
		hashCode.Add(fSharingMode);
		return hashCode.ToHashCode();
	}
}

