using System;
using System.Runtime.Versioning;
using Windows.Win32.UI.Shell;

namespace RootApp.Client.Domain.Windows.Caching.Interop;

[SupportedOSPlatform("windows5.1.2600")]
internal static class AttachmentExecuteActivator
{
	private static readonly Guid CLSID_AttachmentServices = new Guid(1093000598u, 57402, 16643, 143, 112, 224, 89, 125, 128, 59, 156);

	internal unsafe static IAttachmentExecute* ActivateAttachmentExecute()
	{
		return RootActivator.ActivateClass<IAttachmentExecute>(CLSID_AttachmentServices, IAttachmentExecute.IID_Guid);
	}
}
