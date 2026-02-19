// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.TransferOwnershipViewModelFactory
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;

public class TransferOwnershipViewModelFactory(IRootSessionAccessor P_0, RootMarkdownEngineManager P_1)
{
	public TransferOwnershipViewModel Create(Member P_0)
	{
		return new TransferOwnershipViewModel(P_0, P_0, P_1);
	}
}
