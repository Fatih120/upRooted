// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRVkBackendContextNative
using System;
using SkiaSharp;

internal struct GRVkBackendContextNative : IEquatable<GRVkBackendContextNative>
{
	public IntPtr fInstance;

	public IntPtr fPhysicalDevice;

	public IntPtr fDevice;

	public IntPtr fQueue;

	public uint fGraphicsQueueIndex;

	public uint fMinAPIVersion;

	public uint fInstanceVersion;

	public uint fMaxAPIVersion;

	public uint fExtensions;

	public IntPtr fVkExtensions;

	public uint fFeatures;

	public IntPtr fDeviceFeatures;

	public IntPtr fDeviceFeatures2;

	public IntPtr fMemoryAllocator;

	public GRVkGetProcProxyDelegate fGetProc;

	public unsafe void* fGetProcUserData;

	public byte fOwnsInstanceAndDevice;

	public byte fProtectedContext;

	public unsafe readonly bool Equals(GRVkBackendContextNative P_0)
	{
		if (fInstance == P_0.fInstance && fPhysicalDevice == P_0.fPhysicalDevice && fDevice == P_0.fDevice && fQueue == P_0.fQueue && fGraphicsQueueIndex == P_0.fGraphicsQueueIndex && fMinAPIVersion == P_0.fMinAPIVersion && fInstanceVersion == P_0.fInstanceVersion && fMaxAPIVersion == P_0.fMaxAPIVersion && fExtensions == P_0.fExtensions && fVkExtensions == P_0.fVkExtensions && fFeatures == P_0.fFeatures && fDeviceFeatures == P_0.fDeviceFeatures && fDeviceFeatures2 == P_0.fDeviceFeatures2 && fMemoryAllocator == P_0.fMemoryAllocator && fGetProc == P_0.fGetProc && fGetProcUserData == P_0.fGetProcUserData && fOwnsInstanceAndDevice == P_0.fOwnsInstanceAndDevice)
		{
			return fProtectedContext == P_0.fProtectedContext;
		}
		return false;
	}

	public override readonly bool Equals(object P_0)
	{
		if (P_0 is GRVkBackendContextNative gRVkBackendContextNative)
		{
			return Equals(gRVkBackendContextNative);
		}
		return false;
	}

	public static bool operator ==(GRVkBackendContextNative P_0, GRVkBackendContextNative P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(GRVkBackendContextNative P_0, GRVkBackendContextNative P_1)
	{
		return !P_0.Equals(P_1);
	}

	public unsafe override readonly int GetHashCode()
	{
		SkiaSharp.HashCode hashCode = default(SkiaSharp.HashCode);
		hashCode.Add(fInstance);
		hashCode.Add(fPhysicalDevice);
		hashCode.Add(fDevice);
		hashCode.Add(fQueue);
		hashCode.Add(fGraphicsQueueIndex);
		hashCode.Add(fMinAPIVersion);
		hashCode.Add(fInstanceVersion);
		hashCode.Add(fMaxAPIVersion);
		hashCode.Add(fExtensions);
		hashCode.Add(fVkExtensions);
		hashCode.Add(fFeatures);
		hashCode.Add(fDeviceFeatures);
		hashCode.Add(fDeviceFeatures2);
		hashCode.Add(fMemoryAllocator);
		hashCode.Add(fGetProc);
		hashCode.Add(fGetProcUserData);
		hashCode.Add(fOwnsInstanceAndDevice);
		hashCode.Add(fProtectedContext);
		return hashCode.ToHashCode();
	}
}

