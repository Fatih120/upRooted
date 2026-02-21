// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRVkAlloc
using System;
using SkiaSharp;

public struct GRVkAlloc : IEquatable<GRVkAlloc>
{
	private ulong fMemory;

	private ulong fOffset;

	private ulong fSize;

	private uint fFlags;

	private IntPtr fBackendMemory;

	private byte fUsesSystemHeap;

	public ulong Memory
	{
		set
		{
			fMemory = num;
		}
	}

	public ulong Size
	{
		set
		{
			fSize = num;
		}
	}

	public readonly bool Equals(GRVkAlloc P_0)
	{
		if (fMemory == P_0.fMemory && fOffset == P_0.fOffset && fSize == P_0.fSize && fFlags == P_0.fFlags && fBackendMemory == P_0.fBackendMemory)
		{
			return fUsesSystemHeap == P_0.fUsesSystemHeap;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is GRVkAlloc gRVkAlloc)
		{
			return Equals(gRVkAlloc);
		}
		return false;
	}

	public static bool operator ==(GRVkAlloc P_0, GRVkAlloc P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(GRVkAlloc P_0, GRVkAlloc P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fMemory);
		hashCode.Add(fOffset);
		hashCode.Add(fSize);
		hashCode.Add(fFlags);
		hashCode.Add(fBackendMemory);
		hashCode.Add(fUsesSystemHeap);
		return hashCode.ToHashCode();
	}
}

