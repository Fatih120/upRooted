// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberPickerViewModelFactory
using System;
using System.Collections.Generic;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Core.Identifiers;

public class MemberPickerViewModelFactory(MemberCheckBoxViewModelFactory P_0)
{
	public MemberPickerViewModel Create(Community P_0, Action<RootGuid> P_1, Action<RootGuid> P_2, IEnumerable<RootGuid> P_3)
	{
		return new MemberPickerViewModel(P_0, P_1, P_2, P_3, P_0);
	}
}

