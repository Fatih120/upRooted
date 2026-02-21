// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRVkBackendContext
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SkiaSharp;

public class GRVkBackendContext : IDisposable
{
	private GRVkGetProcedureAddressDelegate getProc;

	private GRVkGetProcProxyDelegate getProcProxy;

	private GCHandle getProcHandle;

	private unsafe void* getProcContext;

	[CompilerGenerated]
	private IntPtr _003CVkInstance_003Ek__BackingField;

	[CompilerGenerated]
	private IntPtr _003CVkPhysicalDevice_003Ek__BackingField;

	[CompilerGenerated]
	private IntPtr _003CVkDevice_003Ek__BackingField;

	[CompilerGenerated]
	private IntPtr _003CVkQueue_003Ek__BackingField;

	[CompilerGenerated]
	private uint _003CGraphicsQueueIndex_003Ek__BackingField;

	public IntPtr VkInstance
	{
		[CompilerGenerated]
		get
		{
			return _003CVkInstance_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CVkInstance_003Ek__BackingField = intPtr;
		}
	}

	public IntPtr VkPhysicalDevice
	{
		[CompilerGenerated]
		get
		{
			return _003CVkPhysicalDevice_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CVkPhysicalDevice_003Ek__BackingField = intPtr;
		}
	}

	public IntPtr VkDevice
	{
		[CompilerGenerated]
		get
		{
			return _003CVkDevice_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CVkDevice_003Ek__BackingField = intPtr;
		}
	}

	public IntPtr VkQueue
	{
		[CompilerGenerated]
		get
		{
			return _003CVkQueue_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CVkQueue_003Ek__BackingField = intPtr;
		}
	}

	public uint GraphicsQueueIndex
	{
		[CompilerGenerated]
		get
		{
			return _003CGraphicsQueueIndex_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CGraphicsQueueIndex_003Ek__BackingField = num;
		}
	}

	public uint MaxAPIVersion { get; }

	public GRVkExtensions Extensions { get; }

	public IntPtr VkPhysicalDeviceFeatures { get; }

	public IntPtr VkPhysicalDeviceFeatures2 { get; }

	public unsafe GRVkGetProcedureAddressDelegate GetProcedureAddress
	{
		set
		{
			getProc = gRVkGetProcedureAddressDelegate;
			if (getProcHandle.IsAllocated)
			{
				getProcHandle.Free();
			}
			getProcProxy = null;
			getProcHandle = default(GCHandle);
			getProcContext = null;
			if (gRVkGetProcedureAddressDelegate != null)
			{
				getProcProxy = DelegateProxies.Create(gRVkGetProcedureAddressDelegate, DelegateProxies.GRVkGetProcDelegateProxy, out var gCHandle, out var intPtr);
				getProcHandle = gCHandle;
				getProcContext = (void*)intPtr;
			}
		}
	}

	public bool ProtectedContext { get; }

	protected virtual void Dispose(bool P_0)
	{
		if (P_0 && getProcHandle.IsAllocated)
		{
			getProcHandle.Free();
			getProcHandle = default(GCHandle);
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	internal unsafe GRVkBackendContextNative ToNative()
	{
		return new GRVkBackendContextNative
		{
			fInstance = VkInstance,
			fDevice = VkDevice,
			fPhysicalDevice = VkPhysicalDevice,
			fQueue = VkQueue,
			fGraphicsQueueIndex = GraphicsQueueIndex,
			fMaxAPIVersion = MaxAPIVersion,
			fVkExtensions = (Extensions?.Handle ?? IntPtr.Zero),
			fDeviceFeatures = VkPhysicalDeviceFeatures,
			fDeviceFeatures2 = VkPhysicalDeviceFeatures2,
			fGetProcUserData = getProcContext,
			fGetProc = getProcProxy,
			fProtectedContext = (ProtectedContext ? ((byte)1) : ((byte)0))
		};
	}
}

