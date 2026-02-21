// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKString
using System;
using SkiaSharp;

internal class SKString : SKObject, ISKSkipObjectRegistration
{
	internal SKString(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKString()
		: base(SkiaApi.sk_string_new_empty(), true)
	{
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKString instance.");
		}
	}

	public unsafe override string ToString()
	{
		void* ptr = SkiaApi.sk_string_get_c_str(Handle);
		IntPtr intPtr = SkiaApi.sk_string_get_size(Handle);
		return StringUtilities.GetString((IntPtr)ptr, (int)intPtr, SKTextEncoding.Utf8);
	}

	public static explicit operator string(SKString P_0)
	{
		return P_0.ToString();
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_string_destructor(Handle);
	}

	internal static SKString GetObject(IntPtr P_0)
	{
		if (!(P_0 == IntPtr.Zero))
		{
			return new SKString(P_0, true);
		}
		return null;
	}
}

