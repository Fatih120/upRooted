// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberGroupViewModelFactory
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;

public class MemberGroupViewModelFactory
{
	public MemberGroupViewModel Create(MemberGroup P_0, bool P_1)
	{
		return new MemberGroupViewModel(P_0, P_1);
	}
}

