// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberInviteViewModelFactory
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Community.Roles.Pickers;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Friends;

public class MemberInviteViewModelFactory(BitmapCache P_0, RolePickerViewModelFactory P_1)
{
	public MemberInviteViewModel Create(Friend P_0, Community P_1)
	{
		return new MemberInviteViewModel(P_0, P_1, P_0, P_1);
	}
}

