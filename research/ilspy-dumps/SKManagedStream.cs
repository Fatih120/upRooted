// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKManagedStream
using System;
using System.IO;
using SkiaSharp;

public class SKManagedStream : SKAbstractManagedStream
{
	private Stream stream;

	private bool isAsEnd;

	private bool disposeStream;

	private bool wasCopied;

	private WeakReference parent;

	private WeakReference child;

	public SKManagedStream(Stream P_0)
		: this(P_0, false)
	{
	}

	public SKManagedStream(Stream P_0, bool P_1)
		: base(true)
	{
		stream = P_0 ?? throw new ArgumentNullException("managedStream");
		disposeStream = P_1;
	}

	public int CopyTo(SKWStream P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("destination");
		}
		int num = 0;
		Utils.RentedArray<byte> rentedArray = Utils.RentArray<byte>(81920);
		try
		{
			int num2;
			while ((num2 = stream.Read((byte[])rentedArray, 0, rentedArray.Length)) > 0)
			{
				P_0.Write((byte[])rentedArray, num2);
				num += num2;
			}
			P_0.Flush();
			return num;
		}
		finally
		{
			rentedArray.Dispose();
		}
	}

	public SKStreamAsset ToMemoryStream()
	{
		using SKDynamicMemoryWStream sKDynamicMemoryWStream = new SKDynamicMemoryWStream();
		CopyTo(sKDynamicMemoryWStream);
		return sKDynamicMemoryWStream.DetachAsStream();
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeManaged()
	{
		SKManagedStream sKManagedStream = child?.Target as SKManagedStream;
		SKManagedStream sKManagedStream2 = parent?.Target as SKManagedStream;
		if (sKManagedStream != null && sKManagedStream2 != null)
		{
			sKManagedStream.parent = parent;
			sKManagedStream2.child = child;
		}
		else if (sKManagedStream != null)
		{
			sKManagedStream.parent = null;
		}
		else if (sKManagedStream2 != null)
		{
			sKManagedStream2.child = null;
			sKManagedStream2.wasCopied = false;
			sKManagedStream2.disposeStream = disposeStream;
			disposeStream = false;
		}
		parent = null;
		child = null;
		if (disposeStream && stream != null)
		{
			stream.Dispose();
			stream = null;
		}
		base.DisposeManaged();
	}

	private IntPtr OnReadManagedStream(IntPtr P_0, IntPtr P_1)
	{
		if ((int)P_1 < 0)
		{
			throw new ArgumentOutOfRangeException("size");
		}
		if (P_1 == IntPtr.Zero)
		{
			return IntPtr.Zero;
		}
		Utils.RentedArray<byte> rentedArray = Utils.RentArray<byte>((int)P_1);
		try
		{
			int num = stream.Read(rentedArray.Array, 0, rentedArray.Length);
			if (P_0 != IntPtr.Zero)
			{
				Span<byte> span = rentedArray.Span.Slice(0, num);
				Span<byte> span2 = P_0.AsSpan(rentedArray.Length);
				span.CopyTo(span2);
			}
			if (!stream.CanSeek && (int)P_1 > 0 && num <= (int)P_1)
			{
				isAsEnd = true;
			}
			return (IntPtr)num;
		}
		finally
		{
			rentedArray.Dispose();
		}
	}

	protected override IntPtr OnRead(IntPtr P_0, IntPtr P_1)
	{
		VerifyOriginal();
		return OnReadManagedStream(P_0, P_1);
	}

	protected override IntPtr OnPeek(IntPtr P_0, IntPtr P_1)
	{
		VerifyOriginal();
		if (!stream.CanSeek)
		{
			return (IntPtr)0;
		}
		long position = stream.Position;
		IntPtr result = OnReadManagedStream(P_0, P_1);
		stream.Position = position;
		return result;
	}

	protected override bool OnIsAtEnd()
	{
		VerifyOriginal();
		if (!stream.CanSeek)
		{
			return isAsEnd;
		}
		return stream.Position >= stream.Length;
	}

	protected override bool OnHasPosition()
	{
		VerifyOriginal();
		return stream.CanSeek;
	}

	protected override bool OnHasLength()
	{
		VerifyOriginal();
		return stream.CanSeek;
	}

	protected override bool OnRewind()
	{
		VerifyOriginal();
		if (!stream.CanSeek)
		{
			return false;
		}
		stream.Position = 0L;
		return true;
	}

	protected override IntPtr OnGetPosition()
	{
		VerifyOriginal();
		if (!stream.CanSeek)
		{
			return (IntPtr)0;
		}
		return (IntPtr)stream.Position;
	}

	protected override IntPtr OnGetLength()
	{
		VerifyOriginal();
		if (!stream.CanSeek)
		{
			return (IntPtr)0;
		}
		return (IntPtr)stream.Length;
	}

	protected override bool OnSeek(IntPtr P_0)
	{
		VerifyOriginal();
		if (!stream.CanSeek)
		{
			return false;
		}
		stream.Position = (long)P_0;
		return true;
	}

	protected override bool OnMove(int P_0)
	{
		VerifyOriginal();
		if (!stream.CanSeek)
		{
			return false;
		}
		stream.Position += P_0;
		return true;
	}

	protected override IntPtr OnCreateNew()
	{
		VerifyOriginal();
		return IntPtr.Zero;
	}

	protected override IntPtr OnDuplicate()
	{
		VerifyOriginal();
		if (!stream.CanSeek)
		{
			return IntPtr.Zero;
		}
		SKManagedStream sKManagedStream = new SKManagedStream(stream, disposeStream);
		sKManagedStream.parent = new WeakReference(this);
		wasCopied = true;
		disposeStream = false;
		child = new WeakReference(sKManagedStream);
		stream.Position = 0L;
		return sKManagedStream.Handle;
	}

	protected override IntPtr OnFork()
	{
		VerifyOriginal();
		SKManagedStream sKManagedStream = new SKManagedStream(stream, disposeStream);
		wasCopied = true;
		disposeStream = false;
		return sKManagedStream.Handle;
	}

	private void VerifyOriginal()
	{
		if (wasCopied)
		{
			throw new InvalidOperationException("This stream was duplicated or forked and cannot be read anymore.");
		}
	}
}

