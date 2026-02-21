// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKPositionedRunBuffer
using System;
using SkiaSharp;

public sealed class SKPositionedRunBuffer : SKRunBuffer
{
	internal SKPositionedRunBuffer(SKRunBufferInternal P_0, int P_1)
		: base(P_0, P_1)
	{
	}

	public unsafe Span<SKPoint> GetPositionSpan()
	{
		return new Span<SKPoint>(internalBuffer.pos, (internalBuffer.pos != null) ? base.Size : 0);
	}

	public void SetPositions(ReadOnlySpan<SKPoint> P_0)
	{
		P_0.CopyTo(GetPositionSpan());
	}
}

