// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKRunBufferInternal
using System;
using SkiaSharp;

internal struct SKRunBufferInternal : IEquatable<SKRunBufferInternal>
{
	public unsafe void* glyphs;

	public unsafe void* pos;

	public unsafe void* utf8text;

	public unsafe void* clusters;

	public unsafe readonly bool Equals(SKRunBufferInternal P_0)
	{
		if (glyphs == P_0.glyphs && pos == P_0.pos && utf8text == P_0.utf8text)
		{
			return clusters == P_0.clusters;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is SKRunBufferInternal sKRunBufferInternal)
		{
			return Equals(sKRunBufferInternal);
		}
		return false;
	}

	public static bool operator ==(SKRunBufferInternal P_0, SKRunBufferInternal P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(SKRunBufferInternal P_0, SKRunBufferInternal P_1)
	{
		return !P_0.Equals(P_1);
	}

	public unsafe override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(glyphs);
		hashCode.Add(pos);
		hashCode.Add(utf8text);
		hashCode.Add(clusters);
		return hashCode.ToHashCode();
	}
}

