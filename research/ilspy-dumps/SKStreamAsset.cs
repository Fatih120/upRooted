// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKStreamAsset
using System;
using SkiaSharp;

public abstract class SKStreamAsset : SKStreamSeekable
{
	internal SKStreamAsset(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	internal static SKStreamAsset GetObject(IntPtr P_0)
	{
		return SKObject.GetOrAddObject(P_0, (Func<IntPtr, bool, SKStreamAsset>)((IntPtr h, bool o) => new SKStreamAssetImplementation(h, o)));
	}
}

