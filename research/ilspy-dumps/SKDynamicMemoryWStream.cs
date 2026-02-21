// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKDynamicMemoryWStream
using System;
using SkiaSharp;

public class SKDynamicMemoryWStream : SKWStream
{
	public SKDynamicMemoryWStream()
		: base(SkiaApi.sk_dynamicmemorywstream_new(), true)
	{
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKDynamicMemoryWStream instance.");
		}
	}

	public SKStreamAsset DetachAsStream()
	{
		return SKStreamAsset.GetObject(SkiaApi.sk_dynamicmemorywstream_detach_as_stream(Handle));
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_dynamicmemorywstream_destroy(Handle);
	}
}

