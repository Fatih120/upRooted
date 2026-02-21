// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKNativeObject
using System;
using System.Runtime.CompilerServices;
using System.Threading;

public abstract class SKNativeObject : IDisposable
{
	internal bool fromFinalizer;

	private int isDisposed;

	[CompilerGenerated]
	private IntPtr _003CHandle_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003COwnsHandle_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIgnorePublicDispose_003Ek__BackingField;

	public virtual IntPtr Handle
	{
		[CompilerGenerated]
		get
		{
			return _003CHandle_003Ek__BackingField;
		}
		[CompilerGenerated]
		protected set
		{
			_003CHandle_003Ek__BackingField = intPtr;
		}
	}

	protected internal virtual bool OwnsHandle
	{
		[CompilerGenerated]
		get
		{
			return _003COwnsHandle_003Ek__BackingField;
		}
		[CompilerGenerated]
		protected set
		{
			_003COwnsHandle_003Ek__BackingField = flag;
		}
	}

	protected internal bool IgnorePublicDispose
	{
		[CompilerGenerated]
		get
		{
			return _003CIgnorePublicDispose_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIgnorePublicDispose_003Ek__BackingField = flag;
		}
	}

	protected internal bool IsDisposed => isDisposed == 1;

	internal SKNativeObject(IntPtr P_0, bool P_1)
	{
		Handle = P_0;
		OwnsHandle = P_1;
	}

	~SKNativeObject()
	{
		fromFinalizer = true;
		Dispose(false);
	}

	protected virtual void DisposeUnownedManaged()
	{
	}

	protected virtual void DisposeManaged()
	{
	}

	protected virtual void DisposeNative()
	{
	}

	protected virtual void Dispose(bool P_0)
	{
		if (Interlocked.CompareExchange(ref isDisposed, 1, 0) == 0)
		{
			if (P_0)
			{
				DisposeUnownedManaged();
			}
			if (Handle != IntPtr.Zero && OwnsHandle)
			{
				DisposeNative();
			}
			if (P_0)
			{
				DisposeManaged();
			}
			Handle = IntPtr.Zero;
		}
	}

	public void Dispose()
	{
		if (!IgnorePublicDispose)
		{
			DisposeInternal();
		}
	}

	protected internal void DisposeInternal()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}

