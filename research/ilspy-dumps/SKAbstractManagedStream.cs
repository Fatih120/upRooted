// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKAbstractManagedStream
using System;
using System.Runtime.InteropServices;
using System.Threading;
using SkiaSharp;

public abstract class SKAbstractManagedStream : SKStreamAsset
{
	private static readonly SKManagedStreamDelegates delegates;

	private int fromNative;

	unsafe static SKAbstractManagedStream()
	{
		delegates = new SKManagedStreamDelegates
		{
			fRead = ReadInternal,
			fPeek = PeekInternal,
			fIsAtEnd = IsAtEndInternal,
			fHasPosition = HasPositionInternal,
			fHasLength = HasLengthInternal,
			fRewind = RewindInternal,
			fGetPosition = GetPositionInternal,
			fSeek = SeekInternal,
			fMove = MoveInternal,
			fGetLength = GetLengthInternal,
			fDuplicate = DuplicateInternal,
			fFork = ForkInternal,
			fDestroy = DestroyInternal
		};
		SkiaApi.sk_managedstream_set_procs(delegates);
	}

	protected SKAbstractManagedStream()
		: this(true)
	{
	}

	protected unsafe SKAbstractManagedStream(bool P_0)
		: base(IntPtr.Zero, P_0)
	{
		IntPtr intPtr = DelegateProxies.CreateUserData(this, true);
		Handle = SkiaApi.sk_managedstream_new((void*)intPtr);
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		if (Interlocked.CompareExchange(ref fromNative, 0, 0) == 0)
		{
			SkiaApi.sk_managedstream_destroy(Handle);
		}
	}

	protected abstract IntPtr OnRead(IntPtr P_0, IntPtr P_1);

	protected abstract IntPtr OnPeek(IntPtr P_0, IntPtr P_1);

	protected abstract bool OnIsAtEnd();

	protected abstract bool OnHasPosition();

	protected abstract bool OnHasLength();

	protected abstract bool OnRewind();

	protected abstract IntPtr OnGetPosition();

	protected abstract IntPtr OnGetLength();

	protected abstract bool OnSeek(IntPtr P_0);

	protected abstract bool OnMove(int P_0);

	protected abstract IntPtr OnCreateNew();

	protected virtual IntPtr OnFork()
	{
		IntPtr intPtr = OnCreateNew();
		SkiaApi.sk_stream_seek(intPtr, SkiaApi.sk_stream_get_position(Handle));
		return intPtr;
	}

	protected virtual IntPtr OnDuplicate()
	{
		return OnCreateNew();
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamReadProxyDelegate))]
	private unsafe static IntPtr ReadInternal(IntPtr s, void* context, void* buffer, IntPtr size)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnRead((IntPtr)buffer, size);
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamPeekProxyDelegate))]
	private unsafe static IntPtr PeekInternal(IntPtr s, void* context, void* buffer, IntPtr size)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnPeek((IntPtr)buffer, size);
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamIsAtEndProxyDelegate))]
	private unsafe static bool IsAtEndInternal(IntPtr s, void* context)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnIsAtEnd();
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamHasPositionProxyDelegate))]
	private unsafe static bool HasPositionInternal(IntPtr s, void* context)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnHasPosition();
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamHasLengthProxyDelegate))]
	private unsafe static bool HasLengthInternal(IntPtr s, void* context)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnHasLength();
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamRewindProxyDelegate))]
	private unsafe static bool RewindInternal(IntPtr s, void* context)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnRewind();
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamGetPositionProxyDelegate))]
	private unsafe static IntPtr GetPositionInternal(IntPtr s, void* context)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnGetPosition();
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamSeekProxyDelegate))]
	private unsafe static bool SeekInternal(IntPtr s, void* context, IntPtr position)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnSeek(position);
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamMoveProxyDelegate))]
	private unsafe static bool MoveInternal(IntPtr s, void* context, int offset)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnMove(offset);
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamGetLengthProxyDelegate))]
	private unsafe static IntPtr GetLengthInternal(IntPtr s, void* context)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnGetLength();
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamDuplicateProxyDelegate))]
	private unsafe static IntPtr DuplicateInternal(IntPtr s, void* context)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnDuplicate();
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamForkProxyDelegate))]
	private unsafe static IntPtr ForkInternal(IntPtr s, void* context)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		return userData.OnFork();
	}

	[MonoPInvokeCallback(typeof(SKManagedStreamDestroyProxyDelegate))]
	private unsafe static void DestroyInternal(IntPtr s, void* context)
	{
		GCHandle gCHandle;
		SKAbstractManagedStream userData = DelegateProxies.GetUserData<SKAbstractManagedStream>((IntPtr)context, out gCHandle);
		if (userData != null)
		{
			Interlocked.Exchange(ref userData.fromNative, 1);
			userData.Dispose();
		}
		gCHandle.Free();
	}
}

