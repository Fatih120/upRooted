using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Windows.Win32.Foundation;

namespace Windows.Win32.UI.Shell;

[Guid("73DB1241-1E85-4581-8E4F-A81E1D0F8C57")]
[SupportedOSPlatform("windows5.1.2600")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.264+4d6898735f.RR")]
internal struct IAttachmentExecute
{
	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(1943736897, 7813, 17793, 142, 79, 168, 30, 29, 15, 140, 87);

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IAttachmentExecute*, uint>)lpVtbl[2])((IAttachmentExecute*)Unsafe.AsPointer(in this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void SetClientGuid(in Guid P_0)
	{
		fixed (Guid* clientGuid = &P_0)
		{
			SetClientGuid(clientGuid);
		}
	}

	public unsafe void SetClientGuid(Guid* P_0)
	{
		((delegate* unmanaged[Stdcall]<IAttachmentExecute*, Guid*, HRESULT>)lpVtbl[4])((IAttachmentExecute*)Unsafe.AsPointer(in this), P_0).ThrowOnFailure();
	}

	public unsafe void SetLocalPath(PCWSTR P_0)
	{
		((delegate* unmanaged[Stdcall]<IAttachmentExecute*, PCWSTR, HRESULT>)lpVtbl[5])((IAttachmentExecute*)Unsafe.AsPointer(in this), P_0).ThrowOnFailure();
	}

	public unsafe void SetSource(PCWSTR P_0)
	{
		((delegate* unmanaged[Stdcall]<IAttachmentExecute*, PCWSTR, HRESULT>)lpVtbl[7])((IAttachmentExecute*)Unsafe.AsPointer(in this), P_0).ThrowOnFailure();
	}

	public unsafe HRESULT Save()
	{
		return ((delegate* unmanaged[Stdcall]<IAttachmentExecute*, HRESULT>)lpVtbl[11])((IAttachmentExecute*)Unsafe.AsPointer(in this));
	}
}
