// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRBackendRenderTargetDesc
using System;
using System.ComponentModel;
using SkiaSharp;

[EditorBrowsable(EditorBrowsableState.Never)]
[Obsolete("Use GRBackendRenderTarget instead.")]
public struct GRBackendRenderTargetDesc : IEquatable<GRBackendRenderTargetDesc>
{
	public int Width { get; }

	public int Height { get; }

	public GRPixelConfig Config { get; }

	public GRSurfaceOrigin Origin { get; }

	public int SampleCount { get; }

	public int StencilBits { get; }

	public IntPtr RenderTargetHandle { get; }

	public readonly bool Equals(GRBackendRenderTargetDesc P_0)
	{
		if (Width == P_0.Width && Height == P_0.Height && Config == P_0.Config && Origin == P_0.Origin && SampleCount == P_0.SampleCount && StencilBits == P_0.StencilBits)
		{
			return RenderTargetHandle == P_0.RenderTargetHandle;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is GRBackendRenderTargetDesc gRBackendRenderTargetDesc)
		{
			return Equals(gRBackendRenderTargetDesc);
		}
		return false;
	}

	public static bool operator ==(GRBackendRenderTargetDesc P_0, GRBackendRenderTargetDesc P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(GRBackendRenderTargetDesc P_0, GRBackendRenderTargetDesc P_1)
	{
		return !P_0.Equals(P_1);
	}

	public override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(Width);
		hashCode.Add(Height);
		hashCode.Add(Config);
		hashCode.Add(Origin);
		hashCode.Add(SampleCount);
		hashCode.Add(StencilBits);
		hashCode.Add(RenderTargetHandle);
		return hashCode.ToHashCode();
	}
}

