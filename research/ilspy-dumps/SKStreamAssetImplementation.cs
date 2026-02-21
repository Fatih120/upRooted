// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKStreamAssetImplementation
using System;
using SkiaSharp;

internal class SKStreamAssetImplementation : SKStreamAsset
{
	internal SKStreamAssetImplementation(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_stream_asset_destroy(Handle);
	}
}

