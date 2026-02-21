// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKRunBuffer
using System;
using SkiaSharp;

public class SKRunBuffer
{
	internal readonly SKRunBufferInternal internalBuffer;

	public int Size { get; }

	internal SKRunBuffer(SKRunBufferInternal P_0, int P_1)
	{
		internalBuffer = P_0;
		Size = P_1;
	}

	public unsafe Span<ushort> GetGlyphSpan()
	{
		return new Span<ushort>(internalBuffer.glyphs, (internalBuffer.glyphs != null) ? Size : 0);
	}

	public void SetGlyphs(ReadOnlySpan<ushort> P_0)
	{
		P_0.CopyTo(GetGlyphSpan());
	}
}

