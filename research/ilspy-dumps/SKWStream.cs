// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKWStream
using System;
using SkiaSharp;

public abstract class SKWStream : SKObject
{
	internal SKWStream(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public unsafe virtual bool Write(byte[] P_0, int P_1)
	{
		fixed (byte* ptr = P_0)
		{
			return SkiaApi.sk_wstream_write(Handle, ptr, (IntPtr)P_1);
		}
	}

	public virtual void Flush()
	{
		SkiaApi.sk_wstream_flush(Handle);
	}
}

