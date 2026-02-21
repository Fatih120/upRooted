using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace RootApp.Client.Domain.Windows.Caching.Interop;

[SupportedOSPlatform("windows5.1.2600")]
public static class RootActivator
{
	private unsafe static HRESULT CoCreateInstance(in Guid P_0, nint P_1, CLSCTX P_2, in Guid P_3, out nint P_4)
	{
		void* ptr;
		HRESULT result = PInvoke.CoCreateInstance(in P_0, (IUnknown*)P_1, P_2, in P_3, out ptr);
		P_4 = (nint)ptr;
		return result;
	}

	public unsafe static TComInstance* ActivateClass<TComInstance>(Guid P_0, Guid P_1) where TComInstance : unmanaged
	{
		CoCreateInstance(in P_0, IntPtr.Zero, CLSCTX.CLSCTX_INPROC_SERVER, in P_1, out var num).ThrowOnFailure();
		try
		{
			return (TComInstance*)num;
		}
		catch (Exception)
		{
			int num2 = Marshal.Release(num);
			throw;
		}
	}
}
