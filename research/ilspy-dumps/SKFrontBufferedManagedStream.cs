// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKFrontBufferedManagedStream
using System;
using System.IO;
using System.Runtime.InteropServices;
using SkiaSharp;

public class SKFrontBufferedManagedStream : SKAbstractManagedStream
{
	private SKStream stream;

	private bool disposeStream;

	private readonly bool hasLength;

	private readonly int streamLength;

	private readonly int bufferLength;

	private byte[] frontBuffer;

	private int bufferedSoFar;

	private int offset;

	public SKFrontBufferedManagedStream(Stream P_0, int P_1, bool P_2)
		: this(new SKManagedStream(P_0, P_2), P_1, true)
	{
	}

	public SKFrontBufferedManagedStream(SKStream P_0, int P_1, bool P_2)
	{
		int num = (P_0.HasLength ? P_0.Length : 0);
		int num2 = (P_0.HasPosition ? P_0.Position : 0);
		disposeStream = P_2;
		stream = P_0;
		hasLength = P_0.HasPosition && P_0.HasLength;
		streamLength = num - num2;
		offset = 0;
		bufferedSoFar = 0;
		bufferLength = P_1;
		frontBuffer = new byte[P_1];
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeManaged()
	{
		if (disposeStream && stream != null)
		{
			stream.Dispose();
			stream = null;
		}
		base.DisposeManaged();
	}

	protected override IntPtr OnRead(IntPtr P_0, IntPtr P_1)
	{
		int num = offset;
		if ((int)P_1 > 0 && offset < bufferedSoFar)
		{
			int num2 = Math.Min((int)P_1, bufferedSoFar - offset);
			if (P_0 != IntPtr.Zero)
			{
				Marshal.Copy(frontBuffer, offset, P_0, num2);
				P_0 += num2;
			}
			offset += num2;
			P_1 -= num2;
		}
		bool flag = false;
		if ((int)P_1 > 0 && bufferedSoFar < bufferLength)
		{
			int num3 = Math.Min((int)P_1, bufferLength - bufferedSoFar);
			IntPtr intPtr = Marshal.AllocCoTaskMem(num3);
			int num4 = stream.Read(intPtr, num3);
			Marshal.Copy(intPtr, frontBuffer, offset, num4);
			Marshal.FreeCoTaskMem(intPtr);
			flag = num4 < num3;
			bufferedSoFar += num4;
			if (P_0 != IntPtr.Zero)
			{
				Marshal.Copy(frontBuffer, offset, P_0, num4);
				P_0 += num4;
			}
			offset += num4;
			P_1 -= num4;
		}
		if ((int)P_1 > 0 && !flag)
		{
			int num5 = stream.Read(P_0, (int)P_1);
			if (num5 > 0)
			{
				frontBuffer = null;
			}
			offset += num5;
			P_1 -= num5;
		}
		return (IntPtr)(offset - num);
	}

	protected override IntPtr OnPeek(IntPtr P_0, IntPtr P_1)
	{
		if (offset >= bufferLength)
		{
			return (IntPtr)0;
		}
		int num = offset;
		int num2 = Math.Min((int)P_1, bufferLength - offset);
		int num3 = Read(P_0, num2);
		offset = num;
		return (IntPtr)num3;
	}

	protected override bool OnIsAtEnd()
	{
		if (offset < bufferedSoFar)
		{
			return false;
		}
		return stream.IsAtEnd;
	}

	protected override bool OnRewind()
	{
		if (offset <= bufferLength)
		{
			offset = 0;
			return true;
		}
		return false;
	}

	protected override bool OnHasLength()
	{
		return hasLength;
	}

	protected override IntPtr OnGetLength()
	{
		return (IntPtr)streamLength;
	}

	protected override bool OnHasPosition()
	{
		return false;
	}

	protected override IntPtr OnGetPosition()
	{
		return (IntPtr)0;
	}

	protected override bool OnSeek(IntPtr P_0)
	{
		return false;
	}

	protected override bool OnMove(int P_0)
	{
		return false;
	}

	protected override IntPtr OnCreateNew()
	{
		return IntPtr.Zero;
	}
}

