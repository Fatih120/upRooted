// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKObjectExtensions
using SkiaSharp;

internal static class SKObjectExtensions
{
	public static void SafeUnRef(this ISKReferenceCounted P_0)
	{
		if (P_0 is ISKNonVirtualReferenceCounted iSKNonVirtualReferenceCounted)
		{
			iSKNonVirtualReferenceCounted.UnreferenceNative();
		}
		else
		{
			SkiaApi.sk_refcnt_safe_unref(P_0.Handle);
		}
	}
}

