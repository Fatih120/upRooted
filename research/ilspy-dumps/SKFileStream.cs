// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKFileStream
using System;
using System.Runtime.CompilerServices;
using SkiaSharp;

public class SKFileStream : SKStreamAsset
{
	public bool IsValid
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		get
		{
			throw new NotSupportedException("Linked away");
		}
	}
}

