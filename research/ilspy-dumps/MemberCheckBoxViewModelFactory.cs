// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberCheckBoxViewModelFactory
using System;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Core.Identifiers;

public class MemberCheckBoxViewModelFactory(BitmapCache P_0)
{
	public MemberCheckBoxViewModel Create(Member P_0, bool P_1, Action<RootGuid> P_2, Action<RootGuid> P_3)
	{
		return new MemberCheckBoxViewModel(P_0, P_1, P_2, P_3, P_0);
	}
}

