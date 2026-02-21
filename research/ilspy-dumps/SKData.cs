// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKData
using System;
using System.IO;
using System.Runtime.InteropServices;
using SkiaSharp;

public class SKData : SKObject, ISKNonVirtualReferenceCounted, ISKReferenceCounted
{
	private sealed class SKDataStatic : SKData
	{
		internal SKDataStatic(IntPtr P_0)
			: base(P_0, false)
		{
		}

		protected override void Dispose(bool P_0)
		{
		}
	}

	private static readonly SKData empty;

	public long Size => (long)SkiaApi.sk_data_get_size(Handle);

	public unsafe IntPtr Data => (IntPtr)SkiaApi.sk_data_get_data(Handle);

	static SKData()
	{
		empty = new SKDataStatic(SkiaApi.sk_data_new_empty());
	}

	internal static void EnsureStaticInstanceAreInitialized()
	{
	}

	internal SKData(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	void ISKNonVirtualReferenceCounted.UnreferenceNative()
	{
		SkiaApi.sk_data_unref(Handle);
	}

	public static SKData CreateCopy(byte[] P_0)
	{
		return CreateCopy(P_0, (ulong)P_0.Length);
	}

	public unsafe static SKData CreateCopy(byte[] P_0, ulong P_1)
	{
		fixed (byte* ptr = P_0)
		{
			return GetObject(SkiaApi.sk_data_new_with_copy(ptr, (IntPtr)(long)P_1));
		}
	}

	public static SKData Create(SKStream P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("stream");
		}
		return Create(P_0, P_0.Length);
	}

	public static SKData Create(SKStream P_0, int P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("stream");
		}
		return GetObject(SkiaApi.sk_data_new_from_stream(P_0.Handle, (IntPtr)P_1));
	}

	public byte[] ToArray()
	{
		byte[] result = AsSpan().ToArray();
		GC.KeepAlive(this);
		return result;
	}

	public unsafe ReadOnlySpan<byte> AsSpan()
	{
		return new ReadOnlySpan<byte>((void*)Data, (int)Size);
	}

	public void SaveTo(Stream P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("target");
		}
		IntPtr data = Data;
		long size = Size;
		Utils.RentedArray<byte> rentedArray = Utils.RentArray<byte>(81920);
		try
		{
			long num = size;
			while (num > 0)
			{
				int num2 = (int)Math.Min(81920L, num);
				Marshal.Copy(data, (byte[])rentedArray, 0, num2);
				num -= num2;
				data += num2;
				P_0.Write((byte[])rentedArray, 0, num2);
			}
			GC.KeepAlive(this);
		}
		finally
		{
			rentedArray.Dispose();
		}
	}

	internal static SKData GetObject(IntPtr P_0)
	{
		return SKObject.GetOrAddObject(P_0, (IntPtr h, bool o) => new SKData(h, o));
	}
}

