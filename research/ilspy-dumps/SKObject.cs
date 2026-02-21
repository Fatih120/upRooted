// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKObject
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SkiaSharp;

public abstract class SKObject : SKNativeObject
{
	private readonly object locker = new object();

	private ConcurrentDictionary<IntPtr, SKObject> ownedObjects;

	private ConcurrentDictionary<IntPtr, SKObject> keepAliveObjects;

	internal ConcurrentDictionary<IntPtr, SKObject> OwnedObjects
	{
		get
		{
			if (ownedObjects == null)
			{
				lock (locker)
				{
					if (ownedObjects == null)
					{
						ownedObjects = new ConcurrentDictionary<IntPtr, SKObject>();
					}
				}
			}
			return ownedObjects;
		}
	}

	public override IntPtr Handle
	{
		get
		{
			return base.Handle;
		}
		protected set
		{
			if (intPtr == IntPtr.Zero)
			{
				DeregisterHandle(Handle, this);
				base.Handle = intPtr;
			}
			else
			{
				base.Handle = intPtr;
				RegisterHandle(Handle, this);
			}
		}
	}

	static SKObject()
	{
		SkiaSharpVersion.CheckNativeLibraryCompatible(true);
		SKColorSpace.EnsureStaticInstanceAreInitialized();
		SKData.EnsureStaticInstanceAreInitialized();
		SKFontManager.EnsureStaticInstanceAreInitialized();
		SKTypeface.EnsureStaticInstanceAreInitialized();
	}

	internal SKObject(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeUnownedManaged()
	{
		if (ownedObjects == null)
		{
			return;
		}
		foreach (KeyValuePair<IntPtr, SKObject> ownedObject in ownedObjects)
		{
			SKObject value = ownedObject.Value;
			if (value != null && !value.OwnsHandle)
			{
				value.DisposeInternal();
			}
		}
	}

	protected override void DisposeManaged()
	{
		if (ownedObjects != null)
		{
			foreach (KeyValuePair<IntPtr, SKObject> ownedObject in ownedObjects)
			{
				SKObject value = ownedObject.Value;
				if (value != null && value.OwnsHandle)
				{
					value.DisposeInternal();
				}
			}
			ownedObjects.Clear();
		}
		keepAliveObjects?.Clear();
	}

	protected override void DisposeNative()
	{
		if (this is ISKReferenceCounted iSKReferenceCounted)
		{
			iSKReferenceCounted.SafeUnRef();
		}
	}

	internal static TSkiaObject GetOrAddObject<TSkiaObject>(IntPtr P_0, Func<IntPtr, bool, TSkiaObject> P_1) where TSkiaObject : SKObject
	{
		if (P_0 == IntPtr.Zero)
		{
			return null;
		}
		return HandleDictionary.GetOrAddObject(P_0, true, true, P_1);
	}

	internal static TSkiaObject GetOrAddObject<TSkiaObject>(IntPtr P_0, bool P_1, Func<IntPtr, bool, TSkiaObject> P_2) where TSkiaObject : SKObject
	{
		if (P_0 == IntPtr.Zero)
		{
			return null;
		}
		return HandleDictionary.GetOrAddObject(P_0, P_1, true, P_2);
	}

	internal static TSkiaObject GetOrAddObject<TSkiaObject>(IntPtr P_0, bool P_1, bool P_2, Func<IntPtr, bool, TSkiaObject> P_3) where TSkiaObject : SKObject
	{
		if (P_0 == IntPtr.Zero)
		{
			return null;
		}
		return HandleDictionary.GetOrAddObject(P_0, P_1, P_2, P_3);
	}

	internal static void RegisterHandle(IntPtr P_0, SKObject P_1)
	{
		if (!(P_0 == IntPtr.Zero) && P_1 != null)
		{
			HandleDictionary.RegisterHandle(P_0, P_1);
		}
	}

	internal static void DeregisterHandle(IntPtr P_0, SKObject P_1)
	{
		if (!(P_0 == IntPtr.Zero))
		{
			HandleDictionary.DeregisterHandle(P_0, P_1);
		}
	}

	internal void PreventPublicDisposal()
	{
		base.IgnorePublicDispose = true;
	}

	internal void RevokeOwnership(SKObject P_0)
	{
		OwnsHandle = false;
		base.IgnorePublicDispose = true;
		if (P_0 == null)
		{
			DisposeInternal();
		}
		else
		{
			P_0.OwnedObjects[Handle] = this;
		}
	}

	internal static T OwnedBy<T>(T P_0, SKObject P_1) where T : SKObject
	{
		if (P_0 != null)
		{
			P_1.OwnedObjects[P_0.Handle] = P_0;
		}
		return P_0;
	}
}

