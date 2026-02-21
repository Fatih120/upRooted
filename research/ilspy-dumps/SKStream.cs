// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKStream
using System;
using SkiaSharp;

public abstract class SKStream : SKObject
{
	public bool IsAtEnd => SkiaApi.sk_stream_is_at_end(Handle);

	public bool HasPosition => SkiaApi.sk_stream_has_position(Handle);

	public int Position => (int)SkiaApi.sk_stream_get_position(Handle);

	public bool HasLength => SkiaApi.sk_stream_has_length(Handle);

	public int Length => (int)SkiaApi.sk_stream_get_length(Handle);

	internal SKStream(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public unsafe int Read(byte[] P_0, int P_1)
	{
		fixed (byte* ptr = P_0)
		{
			return Read((IntPtr)ptr, P_1);
		}
	}

	public unsafe int Read(IntPtr P_0, int P_1)
	{
		return (int)SkiaApi.sk_stream_read(Handle, (void*)P_0, (IntPtr)P_1);
	}
}

