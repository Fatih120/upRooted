// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.ISKNonVirtualReferenceCounted
using SkiaSharp;

internal interface ISKNonVirtualReferenceCounted : ISKReferenceCounted
{
	void UnreferenceNative();
}

