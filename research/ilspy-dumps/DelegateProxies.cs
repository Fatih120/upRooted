// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.DelegateProxies
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SkiaSharp;

internal static class DelegateProxies
{
	public unsafe static readonly SKBitmapReleaseProxyDelegate SKBitmapReleaseDelegateProxy = SKBitmapReleaseDelegateProxyImplementation;

	public unsafe static readonly SKDataReleaseProxyDelegate SKDataReleaseDelegateProxy = SKDataReleaseDelegateProxyImplementation;

	public unsafe static readonly SKImageRasterReleaseProxyDelegate SKImageRasterReleaseDelegateProxy = SKImageRasterReleaseDelegateProxyImplementation;

	public unsafe static readonly SKImageRasterReleaseProxyDelegate SKImageRasterReleaseDelegateProxyForCoTaskMem = SKImageRasterReleaseDelegateProxyImplementationForCoTaskMem;

	public unsafe static readonly SKImageTextureReleaseProxyDelegate SKImageTextureReleaseDelegateProxy = SKImageTextureReleaseDelegateProxyImplementation;

	public unsafe static readonly SKSurfaceRasterReleaseProxyDelegate SKSurfaceReleaseDelegateProxy = SKSurfaceReleaseDelegateProxyImplementation;

	public unsafe static readonly GRGlGetProcProxyDelegate GRGlGetProcDelegateProxy = GRGlGetProcDelegateProxyImplementation;

	public unsafe static readonly GRVkGetProcProxyDelegate GRVkGetProcDelegateProxy = GRVkGetProcDelegateProxyImplementation;

	public unsafe static readonly SKGlyphPathProxyDelegate SKGlyphPathDelegateProxy = SKGlyphPathDelegateProxyImplementation;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Create<T>(object P_0, T P_1, out GCHandle P_2, out IntPtr P_3)
	{
		if (P_0 == null)
		{
			P_2 = default(GCHandle);
			P_3 = IntPtr.Zero;
			return default(T);
		}
		P_2 = GCHandle.Alloc(P_0);
		P_3 = GCHandle.ToIntPtr(P_2);
		return P_1;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Create(object P_0, out GCHandle P_1, out IntPtr P_2)
	{
		if (P_0 == null)
		{
			P_1 = default(GCHandle);
			P_2 = IntPtr.Zero;
		}
		else
		{
			P_1 = GCHandle.Alloc(P_0);
			P_2 = GCHandle.ToIntPtr(P_1);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Get<T>(IntPtr P_0, out GCHandle P_1)
	{
		if (P_0 == IntPtr.Zero)
		{
			P_1 = default(GCHandle);
			return default(T);
		}
		P_1 = GCHandle.FromIntPtr(P_0);
		return (T)P_1.Target;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IntPtr CreateUserData(object P_0, bool P_1 = false)
	{
		P_0 = (P_1 ? new WeakReference(P_0) : P_0);
		UserDataDelegate userDataDelegate = () => P_0;
		Create(userDataDelegate, out var _, out var result);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetUserData<T>(IntPtr P_0, out GCHandle P_1)
	{
		UserDataDelegate userDataDelegate = Get<UserDataDelegate>(P_0, out P_1);
		object obj = userDataDelegate();
		if (!(obj is WeakReference weakReference))
		{
			return (T)obj;
		}
		return (T)weakReference.Target;
	}

	[MonoPInvokeCallback(typeof(SKBitmapReleaseProxyDelegate))]
	private unsafe static void SKBitmapReleaseDelegateProxyImplementation(void* address, void* context)
	{
		GCHandle gCHandle;
		SKBitmapReleaseDelegate sKBitmapReleaseDelegate = Get<SKBitmapReleaseDelegate>((IntPtr)context, out gCHandle);
		try
		{
			sKBitmapReleaseDelegate((IntPtr)address, null);
		}
		finally
		{
			gCHandle.Free();
		}
	}

	[MonoPInvokeCallback(typeof(SKDataReleaseProxyDelegate))]
	private unsafe static void SKDataReleaseDelegateProxyImplementation(void* address, void* context)
	{
		GCHandle gCHandle;
		SKDataReleaseDelegate sKDataReleaseDelegate = Get<SKDataReleaseDelegate>((IntPtr)context, out gCHandle);
		try
		{
			sKDataReleaseDelegate((IntPtr)address, null);
		}
		finally
		{
			gCHandle.Free();
		}
	}

	[MonoPInvokeCallback(typeof(SKImageRasterReleaseProxyDelegate))]
	private unsafe static void SKImageRasterReleaseDelegateProxyImplementationForCoTaskMem(void* pixels, void* context)
	{
		Marshal.FreeCoTaskMem((IntPtr)pixels);
	}

	[MonoPInvokeCallback(typeof(SKImageRasterReleaseProxyDelegate))]
	private unsafe static void SKImageRasterReleaseDelegateProxyImplementation(void* pixels, void* context)
	{
		GCHandle gCHandle;
		SKImageRasterReleaseDelegate sKImageRasterReleaseDelegate = Get<SKImageRasterReleaseDelegate>((IntPtr)context, out gCHandle);
		try
		{
			sKImageRasterReleaseDelegate((IntPtr)pixels, null);
		}
		finally
		{
			gCHandle.Free();
		}
	}

	[MonoPInvokeCallback(typeof(SKImageTextureReleaseProxyDelegate))]
	private unsafe static void SKImageTextureReleaseDelegateProxyImplementation(void* context)
	{
		GCHandle gCHandle;
		SKImageTextureReleaseDelegate sKImageTextureReleaseDelegate = Get<SKImageTextureReleaseDelegate>((IntPtr)context, out gCHandle);
		try
		{
			sKImageTextureReleaseDelegate(null);
		}
		finally
		{
			gCHandle.Free();
		}
	}

	[MonoPInvokeCallback(typeof(SKSurfaceRasterReleaseProxyDelegate))]
	private unsafe static void SKSurfaceReleaseDelegateProxyImplementation(void* address, void* context)
	{
		GCHandle gCHandle;
		SKSurfaceReleaseDelegate sKSurfaceReleaseDelegate = Get<SKSurfaceReleaseDelegate>((IntPtr)context, out gCHandle);
		try
		{
			sKSurfaceReleaseDelegate((IntPtr)address, null);
		}
		finally
		{
			gCHandle.Free();
		}
	}

	[MonoPInvokeCallback(typeof(GRGlGetProcProxyDelegate))]
	private unsafe static IntPtr GRGlGetProcDelegateProxyImplementation(void* context, void* name)
	{
		GCHandle gCHandle;
		GRGlGetProcedureAddressDelegate gRGlGetProcedureAddressDelegate = Get<GRGlGetProcedureAddressDelegate>((IntPtr)context, out gCHandle);
		return gRGlGetProcedureAddressDelegate(Marshal.PtrToStringAnsi((IntPtr)name));
	}

	[MonoPInvokeCallback(typeof(GRVkGetProcProxyDelegate))]
	private unsafe static IntPtr GRVkGetProcDelegateProxyImplementation(void* context, void* name, IntPtr instance, IntPtr device)
	{
		GCHandle gCHandle;
		GRVkGetProcedureAddressDelegate gRVkGetProcedureAddressDelegate = Get<GRVkGetProcedureAddressDelegate>((IntPtr)context, out gCHandle);
		return gRVkGetProcedureAddressDelegate(Marshal.PtrToStringAnsi((IntPtr)name), instance, device);
	}

	[MonoPInvokeCallback(typeof(SKGlyphPathProxyDelegate))]
	private unsafe static void SKGlyphPathDelegateProxyImplementation(IntPtr pathOrNull, SKMatrix* matrix, void* context)
	{
		GCHandle gCHandle;
		SKGlyphPathDelegate sKGlyphPathDelegate = Get<SKGlyphPathDelegate>((IntPtr)context, out gCHandle);
		SKPath sKPath = SKPath.GetObject(pathOrNull, false);
		sKGlyphPathDelegate(sKPath, *matrix);
	}
}

