// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.BlockedUsersViewModel
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DynamicData;
using FluentValidation;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;

public class BlockedUsersViewModel : ViewModelBase<BlockedUsersViewModel>, IPage
{
	private readonly IDisposable _cacheCleanup;

	private readonly ReadOnlyObservableCollection<BlockedUserViewModel> _blockedUsers;

	public string PageTitle => Resources.BlockedUsers;

	public ReadOnlyObservableCollection<BlockedUserViewModel> BlockedUsers => _blockedUsers;

	public BlockedUsersViewModel(BlockedUserViewModelFactory P_0, IRootSessionAccessor P_1)
		: base((IValidator<BlockedUsersViewModel>?)null)
	{
		_cacheCleanup = P_1.Session.UserBlockService.ConnectBlockedUsers().Transform(P_0.Create).ObserveOn(AvaloniaScheduler.Instance)
			.Bind(out _blockedUsers)
			.DisposeMany()
			.Subscribe();
	}

	public override void Dispose()
	{
		base.Dispose();
		_cacheCleanup.Dispose();
	}
}

