// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MembersViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using FluentValidation;
using Microsoft.VisualStudio.Threading;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.DeveloperMode;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Community.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.Domain.Helpers.Store;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;

public class MembersViewModel : ViewModelBase<MembersViewModel>
{
	private readonly BitmapCache _bitmapCache;

	private readonly ILocalDataStore _localDataStore;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly IDeveloperModeService _developerModeService;

	private readonly ClipboardService _clipboardService;

	private readonly IDisposable? _cacheCleanup;

	private IDisposable? _activityDisposable;

	private readonly CommunitySettingsViewModelFactory _communitySettingsViewModelFactory;

	private readonly InviteMembersViewModelFactory _inviteMembersViewModelFactory;

	private readonly DeleteCommunityViewModelFactory _deleteCommunityViewModelFactory;

	private readonly LeaveCommunityViewModelFactory _leaveCommunityViewModelFactory;

	private readonly ReadOnlyObservableCollection<IViewModelBase>? _members;

	[CompilerGenerated]
	private bool _003CMenuIn_003Ek__BackingField = true;

	[CompilerGenerated]
	private Community _003CCommunity_003Ek__BackingField;

	[CompilerGenerated]
	private MemberVisibilityOption _003CCommunityMemberFilter_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CHasAnyActivity_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? attachedToVisualTreeCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleMenuCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showCommunitySettingsViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showInviteMembersViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showDeleteCommunityViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showSearchPaneCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? markAllChannelsAsReadCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? copyCommunityIdCommand;

	public ReadOnlyObservableCollection<IViewModelBase>? Members => _members;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Community Community
	{
		get
		{
			return _003CCommunity_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<RootApp.Client.CoreDomain.Models.Community.Community>.Default.Equals(_003CCommunity_003Ek__BackingField, community))
			{
				_003CCommunity_003Ek__BackingField = community;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Community);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool MenuIn
	{
		get
		{
			return _003CMenuIn_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CMenuIn_003Ek__BackingField, flag))
			{
				_003CMenuIn_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MenuIn);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MemberVisibilityOption CommunityMemberFilter
	{
		get
		{
			return _003CCommunityMemberFilter_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<MemberVisibilityOption>.Default.Equals(_003CCommunityMemberFilter_003Ek__BackingField, memberVisibilityOption))
			{
				_003CCommunityMemberFilter_003Ek__BackingField = memberVisibilityOption;
				OnCommunityMemberFilterChanged(memberVisibilityOption);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CommunityMemberFilter);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HasAnyActivity
	{
		get
		{
			return _003CHasAnyActivity_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CHasAnyActivity_003Ek__BackingField, flag))
			{
				_003CHasAnyActivity_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasAnyActivity);
			}
		}
	}

	public bool DeveloperModeEnabled => _developerModeService.IsEnabled;

	public Task<BitmapWrapper?> CommunityPictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(Community.PictureUrl, null, 560);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand AttachedToVisualTreeCommand => attachedToVisualTreeCommand ?? (attachedToVisualTreeCommand = new RelayCommand(AttachedToVisualTree));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleMenuCommand => toggleMenuCommand ?? (toggleMenuCommand = new RelayCommand(ToggleMenu));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowCommunitySettingsViewModelCommand => showCommunitySettingsViewModelCommand ?? (showCommunitySettingsViewModelCommand = new RelayCommand(ShowCommunitySettingsViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowInviteMembersViewModelCommand => showInviteMembersViewModelCommand ?? (showInviteMembersViewModelCommand = new RelayCommand(ShowInviteMembersViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowDeleteCommunityViewModelCommand => showDeleteCommunityViewModelCommand ?? (showDeleteCommunityViewModelCommand = new RelayCommand(ShowDeleteCommunityViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowSearchPaneCommand => showSearchPaneCommand ?? (showSearchPaneCommand = new RelayCommand(ShowSearchPane));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand MarkAllChannelsAsReadCommand => markAllChannelsAsReadCommand ?? (markAllChannelsAsReadCommand = new RelayCommand(MarkAllChannelsAsRead));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CopyCommunityIdCommand => copyCommunityIdCommand ?? (copyCommunityIdCommand = new RelayCommand(CopyCommunityId));

	public MembersViewModel(Community P_0, CommunitySettingsViewModelFactory P_1, InviteMembersViewModelFactory P_2, DeleteCommunityViewModelFactory P_3, LeaveCommunityViewModelFactory P_4, MemberViewModelFactory P_5, MemberGroupViewModelFactory P_6, BitmapCache P_7, ILocalDataStore P_8, IRootSessionAccessor P_9, IDeveloperModeService P_10, ClipboardService P_11)
		: base((IValidator<MembersViewModel>?)null)
	{
		MembersViewModel membersViewModel = this;
		Community = P_0;
		_communitySettingsViewModelFactory = P_1;
		_inviteMembersViewModelFactory = P_2;
		_deleteCommunityViewModelFactory = P_3;
		_leaveCommunityViewModelFactory = P_4;
		_bitmapCache = P_7;
		_localDataStore = P_8;
		_rootSessionAccessor = P_9;
		_developerModeService = P_10;
		_clipboardService = P_11;
		_developerModeService.PropertyChanged += onDeveloperModePropertyChanged;
		getInitialMembersCollapsedState();
		if (Community.Members != null)
		{
			_cacheCleanup = Community.Members.MembersXGroups.ConnectMembers().Transform((Func<ObservableObject, IViewModelBase>)delegate(ObservableObject item)
			{
				if (item is Member member)
				{
					return P_5.Create(member, membersViewModel.Community, membersViewModel.MenuIn);
				}
				if (!(item is MemberGroup memberGroup))
				{
					throw new InvalidOperationException("Should never get here");
				}
				return P_6.Create(memberGroup, !membersViewModel.MenuIn);
			}, false).ObserveOn(AvaloniaScheduler.Instance)
				.Bind(out _members)
				.DisposeMany()
				.Subscribe();
		}
		Community.PropertyChanged += onCommunityPropertyChanged;
		if (Community.Channels != null)
		{
			HasAnyActivity = Community.Channels.AnyChannelHasActivity();
			_activityDisposable = Community.Channels.ConnectAllChannels().AutoRefresh((Channel c) => c.HasActivity).Subscribe(delegate
			{
				membersViewModel.HasAnyActivity = membersViewModel.Community.Channels?.AnyChannelHasActivity() ?? false;
			});
		}
	}

	[RelayCommand]
	public void AttachedToVisualTree()
	{
		getCommunityFilterState();
	}

	private void getCommunityFilterState()
	{
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = DataStoreKeys.CommunityMemberFilter.ToString();
		if (!localDataStore.TryGetWithPath((ReadOnlySpan<string>)_003C_003Ey__InlineArray281, out int value))
		{
			value = 1;
		}
		bool flag = Convert.ToBoolean(value);
		CommunityMemberFilter = (flag ? MemberVisibilityOption.CommunityMembers : MemberVisibilityOption.ChannelMembers);
	}

	private void getInitialMembersCollapsedState()
	{
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray3<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray3<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = Community.Id.ToString();
		_003C_003Ey__InlineArray281[2] = DataStoreKeys.MembersCollapsed.ToString();
		if (!localDataStore.TryGetWithPath((ReadOnlySpan<string>)_003C_003Ey__InlineArray281, out int value))
		{
			value = 1;
		}
		MenuIn = Convert.ToBoolean(value);
		updateMemberGroupsRenderState();
		updateMemberViewModelsMenuInState();
	}

	private void onCommunityPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "PictureUrl")
			{
				OnPropertyChanged("CommunityPictureAsyncBitmapWrapper");
			}
			else if (e.PropertyName == "Channels" && Community.Channels != null && _activityDisposable == null)
			{
				HasAnyActivity = Community.Channels.AnyChannelHasActivity();
				_activityDisposable = Community.Channels.ConnectAllChannels().AutoRefresh((Channel c) => c.HasActivity).Subscribe(delegate
				{
					HasAnyActivity = Community.Channels.AnyChannelHasActivity();
				});
			}
		});
	}

	[RelayCommand]
	public void ToggleMenu()
	{
		MenuIn = !MenuIn;
		updateMemberGroupsRenderState();
		updateMemberViewModelsMenuInState();
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray3<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray3<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = Community.Id.ToString();
		_003C_003Ey__InlineArray281[2] = DataStoreKeys.MembersCollapsed.ToString();
		localDataStore.SetWithPath(_003C_003Ey__InlineArray281, Convert.ToInt32(MenuIn));
	}

	private void updateMemberGroupsRenderState()
	{
		IEnumerable<MemberGroupViewModel> enumerable = Members?.OfType<MemberGroupViewModel>();
		if (enumerable == null)
		{
			return;
		}
		foreach (MemberGroupViewModel item in enumerable)
		{
			item.ShouldRender = !MenuIn;
		}
	}

	private void updateMemberViewModelsMenuInState()
	{
		IEnumerable<MemberViewModel> enumerable = Members?.OfType<MemberViewModel>();
		if (enumerable == null)
		{
			return;
		}
		foreach (MemberViewModel item in enumerable)
		{
			item.MenuIn = MenuIn;
		}
	}

	[RelayCommand]
	public void ShowCommunitySettingsViewModel()
	{
		CommunitySettingsViewModel communitySettingsViewModel = _communitySettingsViewModelFactory.Create(Community);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(communitySettingsViewModel));
	}

	[RelayCommand]
	public void ShowInviteMembersViewModel()
	{
		InviteMembersViewModel inviteMembersViewModel = _inviteMembersViewModelFactory.Create(Community);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(inviteMembersViewModel));
	}

	[RelayCommand]
	public void ShowDeleteCommunityViewModel()
	{
		DeleteCommunityViewModel deleteCommunityViewModel = _deleteCommunityViewModelFactory.Create(Community.Name, Community.Id);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(deleteCommunityViewModel));
	}

	[RelayCommand]
	public void ShowLeaveCommunityViewModel()
	{
		LeaveCommunityViewModel leaveCommunityViewModel = _leaveCommunityViewModelFactory.Create(Community);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(leaveCommunityViewModel));
	}

	[RelayCommand]
	public void ShowSearchPane()
	{
		WeakReferenceMessenger.Default.Send(new OpenSearchPaneMessage(true, Community.Id));
	}

	[RelayCommand]
	public void MarkAllChannelsAsRead()
	{
		try
		{
			if (Community.Channels == null)
			{
				return;
			}
			IEnumerable<ChannelGuid> channelIds = Community.Channels.GetChannelIds();
			foreach (ChannelGuid item in channelIds)
			{
				Channel channel = Community.Channels.GetChannel(item);
				if (channel != null && channel.Type == ChannelType.Text && channel.HasActivity)
				{
					channel.Messages.SetViewTimeAsync().Forget();
					_rootSessionAccessor.Session.NotificationService.SetContainerAsViewedAsync(Community.Id, item).Forget();
				}
			}
		}
		catch
		{
		}
	}

	[RelayCommand]
	public void CopyCommunityId()
	{
		_clipboardService.CopyTextToClipboard(Community.Id.ToString());
	}

	private void onDeveloperModePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "IsEnabled")
		{
			Dispatcher.UIThread.Post(delegate
			{
				OnPropertyChanged("DeveloperModeEnabled");
			});
		}
	}

	public override void Dispose()
	{
		_cacheCleanup?.Dispose();
		_activityDisposable?.Dispose();
		Community.PropertyChanged -= onCommunityPropertyChanged;
		_developerModeService.PropertyChanged -= onDeveloperModePropertyChanged;
		base.Dispose();
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnCommunityMemberFilterChanged(MemberVisibilityOption P_0)
	{
		bool value = P_0 == MemberVisibilityOption.CommunityMembers;
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = DataStoreKeys.CommunityMemberFilter.ToString();
		localDataStore.SetWithPath(_003C_003Ey__InlineArray281, Convert.ToInt32(value));
		Community.Members.UpdateChannelMemberVisibility();
	}
}

