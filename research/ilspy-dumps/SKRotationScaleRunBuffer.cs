// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKRotationScaleRunBuffer
using System;
using SkiaSharp;

public sealed class SKRotationScaleRunBuffer : SKRunBuffer
{
	internal SKRotationScaleRunBuffer(SKRunBufferInternal P_0, int P_1)
		: base(P_0, P_1)
	{
	}

	public unsafe Span<SKRotationScaleMatrix> GetRotationScaleSpan()
	{
		return new Span<SKRotationScaleMatrix>(internalBuffer.pos, base.Size);
	}
}

