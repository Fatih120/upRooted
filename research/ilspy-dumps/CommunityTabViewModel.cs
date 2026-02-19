// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.CommunityTabViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Disposables;
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
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Threading;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.DeveloperMode;
using RootApp.Client.Avalonia.Helpers.Popout;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Community.Settings;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Client.CoreDomain.Models.Tabs;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;

public class CommunityTabViewModel : ViewModelBase<CommunityTabViewModel>, ITabViewModel, IDisposable
{
	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly ILogger<CommunityTabViewModel> _logger;

	private readonly CommunitySettingsViewModelFactory _communitySettingsViewModelFactory;

	private readonly InviteMembersViewModelFactory _inviteMembersViewModelFactory;

	private readonly DeleteCommunityViewModelFactory _deleteCommunityViewModelFactory;

	private readonly LeaveCommunityViewModelFactory _leaveCommunityViewModelFactory;

	private readonly VoiceCallMemberAvatarViewModelFactory _voiceCallMemberAvatarViewModelFactory;

	private readonly ITabPopoutService? _tabPopoutService;

	private readonly BitmapCache _bitmapCache;

	private readonly IDeveloperModeService _developerModeService;

	private readonly ClipboardService _clipboardService;

	private IDisposable? _activityDisposable;

	private IDisposable? _voiceCallMembersDisposable;

	private readonly SourceList<MediaMember> _voiceCallMembersCache = new SourceList<MediaMember>();

	[CompilerGenerated]
	private double _003CAvailableWidth_003Ek__BackingField = 200.0;

	private readonly ReadOnlyObservableCollection<VoiceCallMemberAvatarViewModel> _voiceCallMembers;

	[CompilerGenerated]
	private bool _003CIsCommunityOwner_003Ek__BackingField;

	private IDisposable? _voiceChannelChangesSubscription;

	private IDisposable? _voiceMemberSubscription;

	[CompilerGenerated]
	private Community? _003CCommunity_003Ek__BackingField;

	[CompilerGenerated]
	private IViewModelBase? _003CContentViewModel_003Ek__BackingField;

	[CompilerGenerated]
	private IMemberService? _003CMembers_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsOnCall_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CHasAnyActivity_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CHasActiveVoiceCalls_003Ek__BackingField;

	[CompilerGenerated]
	private int _003CTotalVoiceCallMemberCount_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showCommunitySettingsViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showInviteMembersViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showDeleteCommunityViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeTabCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? copyCommunityIdCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? popoutTabCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? markAllChannelsAsReadCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Community? Community
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
	public IViewModelBase? ContentViewModel
	{
		get
		{
			return _003CContentViewModel_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<IViewModelBase>.Default.Equals(_003CContentViewModel_003Ek__BackingField, viewModelBase))
			{
				_003CContentViewModel_003Ek__BackingField = viewModelBase;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ContentViewModel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IMemberService? Members
	{
		get
		{
			return _003CMembers_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<IMemberService>.Default.Equals(_003CMembers_003Ek__BackingField, memberService))
			{
				_003CMembers_003Ek__BackingField = memberService;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Members);
			}
		}
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor("ShowVoiceCallIndicator")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsOnCall
	{
		get
		{
			return _003CIsOnCall_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsOnCall_003Ek__BackingField, flag))
			{
				_003CIsOnCall_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsOnCall);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowVoiceCallIndicator);
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

	[ObservableProperty]
	[NotifyPropertyChangedFor("HasMoreThanThreeVoiceCallMembers")]
	[NotifyPropertyChangedFor("ExtraVoiceCallMemberCount")]
	[NotifyPropertyChangedFor("ShowVoiceCallIndicator")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HasActiveVoiceCalls
	{
		get
		{
			return _003CHasActiveVoiceCalls_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CHasActiveVoiceCalls_003Ek__BackingField, flag))
			{
				_003CHasActiveVoiceCalls_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasActiveVoiceCalls);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasMoreThanThreeVoiceCallMembers);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ExtraVoiceCallMemberCount);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowVoiceCallIndicator);
			}
		}
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor("HasMoreThanThreeVoiceCallMembers")]
	[NotifyPropertyChangedFor("ExtraVoiceCallMemberCount")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int TotalVoiceCallMemberCount
	{
		set
		{
			if (!EqualityComparer<int>.Default.Equals(_003CTotalVoiceCallMemberCount_003Ek__BackingField, num))
			{
				_003CTotalVoiceCallMemberCount_003Ek__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.TotalVoiceCallMemberCount);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasMoreThanThreeVoiceCallMembers);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ExtraVoiceCallMemberCount);
			}
		}
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor("MaxVisibleVoiceCallMembers")]
	[NotifyPropertyChangedFor("ShowVoiceCallIndicator")]
	[NotifyPropertyChangedFor("HasMoreThanMaxVisibleVoiceCallMembers")]
	[NotifyPropertyChangedFor("ExtraVoiceCallMemberCountDisplay")]
	[NotifyPropertyChangedFor("ShowNotifications")]
	[NotifyPropertyChangedFor("ShowMemberCount")]
	[NotifyPropertyChangedFor("ShowBottomRow")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double AvailableWidth
	{
		get
		{
			return _003CAvailableWidth_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(_003CAvailableWidth_003Ek__BackingField, num))
			{
				_003CAvailableWidth_003Ek__BackingField = num;
				OnAvailableWidthChanged(num);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AvailableWidth);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MaxVisibleVoiceCallMembers);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowVoiceCallIndicator);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasMoreThanMaxVisibleVoiceCallMembers);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ExtraVoiceCallMemberCountDisplay);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowNotifications);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowMemberCount);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowBottomRow);
			}
		}
	}

	public bool ShowBottomRow => AvailableWidth >= 70.0;

	public bool ShowMemberCount => AvailableWidth >= 70.0;

	public bool ShowNotifications => AvailableWidth >= 95.0;

	private double VoiceIndicatorAvailableWidth => Math.Max(0.0, AvailableWidth - 46.0 - 35.0 - (double)((ShowNotifications && Tab.Notifications.ContainerUnviewedNotificationCount > 0) ? 24 : 0));

	public int MaxVisibleVoiceCallMembers
	{
		get
		{
			double voiceIndicatorAvailableWidth = VoiceIndicatorAvailableWidth;
			if (1 == 0)
			{
			}
			int result = ((!(voiceIndicatorAvailableWidth < 30.0)) ? ((!(voiceIndicatorAvailableWidth < 45.0)) ? ((voiceIndicatorAvailableWidth < 55.0) ? 1 : ((!(voiceIndicatorAvailableWidth < 65.0)) ? 3 : 2)) : 0) : 0);
			if (1 == 0)
			{
			}
			return result;
		}
	}

	public bool ShowVoiceCallIndicator => HasActiveVoiceCalls && VoiceIndicatorAvailableWidth >= 30.0;

	public ReadOnlyObservableCollection<VoiceCallMemberAvatarViewModel> VoiceCallMembers => _voiceCallMembers;

	public bool IsCommunityOwner
	{
		[CompilerGenerated]
		get
		{
			return _003CIsCommunityOwner_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CIsCommunityOwner_003Ek__BackingField = flag;
		}
	}

	public bool DeveloperModeEnabled => _developerModeService.IsEnabled;

	public Tab Tab { get; }

	public Task<BitmapWrapper?> CommunityPictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(Community?.PictureUrl, null, 560);

	public bool CanPopoutTab => _tabPopoutService != null;

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
	public IRelayCommand CloseTabCommand => closeTabCommand ?? (closeTabCommand = new RelayCommand(CloseTab));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CopyCommunityIdCommand => copyCommunityIdCommand ?? (copyCommunityIdCommand = new RelayCommand(CopyCommunityId));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PopoutTabCommand => popoutTabCommand ?? (popoutTabCommand = new RelayCommand(PopoutTab, () => CanPopoutTab));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand MarkAllChannelsAsReadCommand => markAllChannelsAsReadCommand ?? (markAllChannelsAsReadCommand = new RelayCommand(MarkAllChannelsAsRead));

	public CommunityTabViewModel(Tab P_0, IRootSessionAccessor P_1, CommunityViewModelFactory P_2, CommunitySettingsViewModelFactory P_3, InviteMembersViewModelFactory P_4, DeleteCommunityViewModelFactory P_5, LeaveCommunityViewModelFactory P_6, VoiceCallMemberAvatarViewModelFactory P_7, ITabPopoutService? P_8, ILoggerFactory P_9, BitmapCache P_10, IDeveloperModeService P_11, ClipboardService P_12)
		: base((IValidator<CommunityTabViewModel>?)null)
	{
		CommunityTabViewModel communityTabViewModel = this;
		Tab = P_0;
		_rootSessionAccessor = P_1;
		_logger = P_9.CreateLogger<CommunityTabViewModel>();
		_communitySettingsViewModelFactory = P_3;
		_inviteMembersViewModelFactory = P_4;
		_deleteCommunityViewModelFactory = P_5;
		_leaveCommunityViewModelFactory = P_6;
		_voiceCallMemberAvatarViewModelFactory = P_7;
		_tabPopoutService = P_8;
		_bitmapCache = P_10;
		_developerModeService = P_11;
		_clipboardService = P_12;
		_developerModeService.PropertyChanged += onDeveloperModePropertyChanged;
		_voiceCallMembersDisposable = _voiceCallMembersCache.Connect().Transform((MediaMember mediaMember, int index) => communityTabViewModel._voiceCallMemberAvatarViewModelFactory.Create(mediaMember, index)).ObserveOn(AvaloniaScheduler.Instance)
			.Bind(out _voiceCallMembers)
			.DisposeMany()
			.Subscribe();
		Community = _rootSessionAccessor.Session.CommunityService.GetCommunity((CommunityGuid)Tab.ContainerId.Value);
		if (Community == null)
		{
			Dispatcher.UIThread.Post(delegate
			{
				P_1.Session.TabService.RemoveTab(communityTabViewModel.Tab.ContainerId.Value);
			});
			return;
		}
		IsCommunityOwner = Community.IsOwner;
		Community.PropertyChanged += onCommunityPropertyChanged;
		OnPropertyChanged("CommunityPictureAsyncBitmapWrapper");
		if (Community.Members != null)
		{
			Members = Community.Members;
		}
		ContentViewModel = P_2.Create(Community.Id, Tab.StartupSubContainerId, Tab.StartupMessageId);
		determineIfOnCall();
		_rootSessionAccessor.Session.ActiveMediaRoomService.PropertyChanged += onActiveMediaRoomServicePropertyChanged;
		if (Community.Channels != null)
		{
			HasAnyActivity = Community.Channels.AnyChannelHasActivity();
			_activityDisposable = Community.Channels.ConnectAllChannels().AutoRefresh((Channel c) => c.HasActivity).Subscribe(delegate
			{
				communityTabViewModel.HasAnyActivity = communityTabViewModel.Community.Channels?.AnyChannelHasActivity() ?? false;
			});
			subscribeToVoiceCallMembers();
		}
	}

	private void onCommunityPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "PictureUrl")
			{
				OnPropertyChanged("CommunityPictureAsyncBitmapWrapper");
			}
			else if (e.PropertyName == "Members")
			{
				Members = Community.Members;
			}
			else if (e.PropertyName == "Channels" && Community.Channels != null && _activityDisposable == null)
			{
				HasAnyActivity = Community.Channels.AnyChannelHasActivity();
				_activityDisposable = Community.Channels.ConnectAllChannels().AutoRefresh((Channel c) => c.HasActivity).Subscribe(delegate
				{
					HasAnyActivity = Community.Channels.AnyChannelHasActivity();
				});
				subscribeToVoiceCallMembers();
			}
		});
	}

	[RelayCommand]
	public void ShowCommunitySettingsViewModel()
	{
		if (Community != null)
		{
			CommunitySettingsViewModel communitySettingsViewModel = _communitySettingsViewModelFactory.Create(Community);
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(communitySettingsViewModel));
		}
	}

	[RelayCommand]
	public void ShowInviteMembersViewModel()
	{
		if (Community != null)
		{
			InviteMembersViewModel inviteMembersViewModel = _inviteMembersViewModelFactory.Create(Community);
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(inviteMembersViewModel));
		}
	}

	[RelayCommand]
	public void ShowDeleteCommunityViewModel()
	{
		if (Community != null)
		{
			DeleteCommunityViewModel deleteCommunityViewModel = _deleteCommunityViewModelFactory.Create(Community.Name, Community.Id);
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(deleteCommunityViewModel));
		}
	}

	[RelayCommand]
	public void ShowLeaveCommunityViewModel()
	{
		if (Community != null)
		{
			LeaveCommunityViewModel leaveCommunityViewModel = _leaveCommunityViewModelFactory.Create(Community);
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(leaveCommunityViewModel));
		}
	}

	[RelayCommand]
	public void CloseTab()
	{
		_rootSessionAccessor.Session.TabService.RemoveTab(Tab.ContainerId.Value);
	}

	[RelayCommand]
	public void CopyCommunityId()
	{
		if (Community != null)
		{
			_clipboardService.CopyTextToClipboard(Community.Id.ToString());
		}
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

	[RelayCommand(CanExecute = "CanPopoutTab")]
	public void PopoutTab()
	{
		_tabPopoutService?.PopoutTab(this);
	}

	[RelayCommand]
	public void MarkAllChannelsAsRead()
	{
		try
		{
			if (Community?.Channels == null)
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

	private void onActiveMediaRoomServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "ActiveMediaRoom")
			{
				determineIfOnCall();
			}
		});
	}

	private void determineIfOnCall()
	{
		if (_rootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom != null && _rootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom.MessageContainer.CommunityId == Community.Id)
		{
			IsOnCall = true;
		}
		else
		{
			IsOnCall = false;
		}
	}

	private void subscribeToVoiceCallMembers()
	{
		if (Community?.Channels != null)
		{
			_voiceChannelChangesSubscription?.Dispose();
			_voiceMemberSubscription?.Dispose();
			IObservable<IChangeSet<Channel, RootGuid>> observable = Community.Channels.ConnectAllChannels().Filter((Channel c) => c.Type == ChannelType.Voice).AutoRefresh((Channel c) => c.MediaRoom);
			_voiceChannelChangesSubscription = observable.Subscribe(delegate
			{
				refreshVoiceCallMembers();
				resubscribeToMemberChanges();
			});
			resubscribeToMemberChanges();
			refreshVoiceCallMembers();
		}
	}

	private void resubscribeToMemberChanges()
	{
		if (Community?.Channels == null)
		{
			return;
		}
		_voiceMemberSubscription?.Dispose();
		List<IDisposable> subscriptions = new List<IDisposable>();
		foreach (ChannelGuid channelId in Community.Channels.GetChannelIds())
		{
			Channel channel = Community.Channels.GetChannel(channelId);
			if (channel != null && channel.Type == ChannelType.Voice && channel.MediaRoom != null)
			{
				IDisposable item = channel.MediaRoom.ConnectMembers().Subscribe(delegate
				{
					refreshVoiceCallMembers();
				});
				subscriptions.Add(item);
			}
		}
		_voiceMemberSubscription = Disposable.Create(delegate
		{
			foreach (IDisposable item2 in subscriptions)
			{
				item2.Dispose();
			}
		});
	}

	private void refreshVoiceCallMembers()
	{
		if (Community?.Channels == null)
		{
			return;
		}
		Dispatcher.UIThread.Post(delegate
		{
			List<MediaMember> list = new List<MediaMember>();
			foreach (ChannelGuid channelId in Community.Channels.GetChannelIds())
			{
				Channel channel = Community.Channels.GetChannel(channelId);
				if (channel != null && channel.Type == ChannelType.Voice)
				{
					MediaRoom mediaRoom = channel.MediaRoom;
					if (mediaRoom != null && mediaRoom.HasActiveCall)
					{
						list.AddRange(channel.MediaRoom.GetMembers());
					}
				}
			}
			TotalVoiceCallMemberCount = list.Count;
			HasActiveVoiceCalls = list.Count > 0;
			int num = Math.Max(0, MaxVisibleVoiceCallMembers);
			List<MediaMember> uniqueMembers = (from mediaMember in list
				group mediaMember by mediaMember.UserId into g
				select g.First()).Take(num).ToList();
			_voiceCallMembersCache.Edit(delegate(IExtendedList<MediaMember> updater)
			{
				updater.Clear();
				updater.AddRange(uniqueMembers);
			});
		});
	}

	public override void Dispose()
	{
		base.Dispose();
		_activityDisposable?.Dispose();
		_voiceCallMembersDisposable?.Dispose();
		_voiceCallMembersCache.Dispose();
		_voiceChannelChangesSubscription?.Dispose();
		_voiceMemberSubscription?.Dispose();
		ContentViewModel?.Dispose();
		if (Community != null)
		{
			Community.PropertyChanged -= onCommunityPropertyChanged;
		}
		_rootSessionAccessor.Session.ActiveMediaRoomService.PropertyChanged -= onActiveMediaRoomServicePropertyChanged;
		_developerModeService.PropertyChanged -= onDeveloperModePropertyChanged;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnAvailableWidthChanged(double P_0)
	{
		refreshVoiceCallMembers();
	}
}

