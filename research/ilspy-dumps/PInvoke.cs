using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.264+4d6898735f.RR")]
internal static class PInvoke
{
	internal static HRESULT HRESULT_FROM_WIN32(WIN32_ERROR P_0)
	{
		return new HRESULT((P_0 == WIN32_ERROR.NO_ERROR) ? ((int)P_0) : ((int)(P_0 & (WIN32_ERROR)65535u) | -2147024896));
	}

	[SupportedOSPlatform("windows5.0")]
	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT CoCreateInstance(in Guid P_0, [Optional] IUnknown* P_1, CLSCTX P_2, in Guid P_3, out void* P_4)
	{
		fixed (void** ptr = &P_4)
		{
			fixed (Guid* ptr2 = &P_3)
			{
				fixed (Guid* ptr3 = &P_0)
				{
					return CoCreateInstance(ptr3, P_1, P_2, ptr2, ptr);
				}
			}
		}
	}

	[DllImport("OLE32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	[SupportedOSPlatform("windows5.0")]
	internal unsafe static extern HRESULT CoCreateInstance(Guid* P_0, [Optional] IUnknown* P_1, CLSCTX P_2, Guid* P_3, void** P_4);
}
