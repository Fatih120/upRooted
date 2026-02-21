// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.HandleDictionary
using System;
using System.Collections.Generic;
using SkiaSharp;
using SkiaSharp.Internals;

internal static class HandleDictionary
{
	private static readonly Type SkipObjectRegistrationType = typeof(ISKSkipObjectRegistration);

	internal static readonly Dictionary<IntPtr, WeakReference> instances = new Dictionary<IntPtr, WeakReference>();

	internal static readonly IPlatformLock instancesLock = PlatformLock.Create();

	internal static TSkiaObject GetOrAddObject<TSkiaObject>(IntPtr P_0, bool P_1, bool P_2, Func<IntPtr, bool, TSkiaObject> P_3) where TSkiaObject : SKObject
	{
		if (P_0 == IntPtr.Zero)
		{
			return null;
		}
		if (SkipObjectRegistrationType.IsAssignableFrom(typeof(TSkiaObject)))
		{
			return P_3(P_0, P_1);
		}
		instancesLock.EnterUpgradeableReadLock();
		try
		{
			if (GetInstanceNoLocks<TSkiaObject>(P_0, out var val))
			{
				if (P_2 && val is ISKReferenceCounted iSKReferenceCounted)
				{
					iSKReferenceCounted.SafeUnRef();
				}
				return val;
			}
			return P_3(P_0, P_1);
		}
		finally
		{
			instancesLock.ExitUpgradeableReadLock();
		}
	}

	private static bool GetInstanceNoLocks<TSkiaObject>(IntPtr P_0, out TSkiaObject P_1) where TSkiaObject : SKObject
	{
		if (instances.TryGetValue(P_0, out var value) && value.IsAlive && value.Target is TSkiaObject val && !val.IsDisposed)
		{
			P_1 = val;
			return true;
		}
		P_1 = null;
		return false;
	}

	internal static void RegisterHandle(IntPtr P_0, SKObject P_1)
	{
		if (P_0 == IntPtr.Zero || P_1 == null || P_1 is ISKSkipObjectRegistration)
		{
			return;
		}
		SKObject sKObject = null;
		instancesLock.EnterWriteLock();
		try
		{
			if (instances.TryGetValue(P_0, out var value) && value.Target is SKObject { IsDisposed: false } sKObject2)
			{
				sKObject = sKObject2;
			}
			instances[P_0] = new WeakReference(P_1);
		}
		finally
		{
			instancesLock.ExitWriteLock();
		}
		sKObject?.DisposeInternal();
	}

	internal static void DeregisterHandle(IntPtr P_0, SKObject P_1)
	{
		if (P_0 == IntPtr.Zero || P_1 is ISKSkipObjectRegistration)
		{
			return;
		}
		instancesLock.EnterWriteLock();
		try
		{
			if (instances.TryGetValue(P_0, out var value) && (!value.IsAlive || value.Target == P_1))
			{
				instances.Remove(P_0);
			}
		}
		finally
		{
			instancesLock.ExitWriteLock();
		}
	}
}

