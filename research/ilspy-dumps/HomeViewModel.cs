// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.HomeViewModel
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
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
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
using RootApp.Client.Avalonia.Helpers.Activation;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Helpers.DirectMessages;
using RootApp.Client.Avalonia.Helpers.LinkJoining;
using RootApp.Client.Avalonia.Helpers.Overlay;
using RootApp.Client.Avalonia.Helpers.Panes;
using RootApp.Client.Avalonia.Helpers.Popout;
using RootApp.Client.Avalonia.Helpers.StreamerMode;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.UI.Community;
using RootApp.Client.Avalonia.UI.Community.Content;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.Home.EmailValidation;
using RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Friends;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Notifications;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile;
using RootApp.Client.Avalonia.UI.Home.VoiceBar;
using RootApp.Client.Avalonia.UI.Main;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.DirectMessages;
using RootApp.Client.CoreDomain.Models.Tabs;
using RootApp.Client.Domain.Helpers.Store;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;
using Tabalonia.Controls;

public class HomeViewModel : ViewModelBase<HomeViewModel>
{
	private readonly IDisposable _tabsDisposable;

	private readonly BitmapCache _bitmapCache;

	private readonly ILocalDataStore _localDataStore;

	private readonly EnterVerificationCodeViewModelFactory _enterVerificationCodeViewModelFactory;

	private readonly VerificationCodeCooldownViewModelFactory _verificationCodeCooldownViewModelFactory;

	private readonly LinkJoiningService _linkJoiningService;

	private readonly OverlayService _overlayService;

	private readonly ITabPopoutService _tabPopoutService;

	private readonly ReadOnlyObservableCollection<ITabViewModel> _tabs;

	private readonly FriendsViewModel _friendsViewModel;

	private readonly DirectMessagesViewModel _directMessagesViewModel;

	private readonly NotificationsViewModel _notificationsViewModel;

	private readonly ProfileViewModel _profileViewModel;

	private readonly DirectMessageIncomingCallService _directMessageIncomingCallService;

	[CompilerGenerated]
	private bool _003CPaneOpen_003Ek__BackingField = false;

	[CompilerGenerated]
	private bool _003CProfileOpen_003Ek__BackingField = false;

	[CompilerGenerated]
	private bool _003CFriendsOpen_003Ek__BackingField = false;

	[CompilerGenerated]
	private bool _003CDirectMessagesOpen_003Ek__BackingField = false;

	[CompilerGenerated]
	private bool _003CNotificationsOpen_003Ek__BackingField = false;

	[CompilerGenerated]
	private double _003CPaneWidth_003Ek__BackingField = 320.0;

	[CompilerGenerated]
	private double _003CRightThumbWidth_003Ek__BackingField = 224.0;

	[CompilerGenerated]
	private WebApiStatus _003CResendEmailVerificationWebApiStatus_003Ek__BackingField;

	[CompilerGenerated]
	private ITabViewModel? _003CSelectedTabViewModel_003Ek__BackingField;

	[CompilerGenerated]
	private IViewModelBase? _003CPaneViewModel_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<TabsControl.MoveTabParameters>? moveRequestedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? createNewTabCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<ITabViewModel>? closeTabCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? friendsPaneToggleCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? directMessagesPaneToggleCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? notificationsPaneToggleCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? profilePaneToggleCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? resendEmailCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? enterVerificationCodeCommand;

	public ReadOnlyObservableCollection<ITabViewModel> Tabs => _tabs;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public WebApiStatus ResendEmailVerificationWebApiStatus
	{
		set
		{
			if (!EqualityComparer<WebApiStatus>.Default.Equals(_003CResendEmailVerificationWebApiStatus_003Ek__BackingField, webApiStatus))
			{
				_003CResendEmailVerificationWebApiStatus_003Ek__BackingField = webApiStatus;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ResendEmailVerificationWebApiStatus);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public ITabViewModel? SelectedTabViewModel
	{
		get
		{
			return _003CSelectedTabViewModel_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<ITabViewModel>.Default.Equals(_003CSelectedTabViewModel_003Ek__BackingField, tabViewModel))
			{
				_003CSelectedTabViewModel_003Ek__BackingField = tabViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SelectedTabViewModel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IViewModelBase? PaneViewModel
	{
		get
		{
			return _003CPaneViewModel_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<IViewModelBase>.Default.Equals(_003CPaneViewModel_003Ek__BackingField, viewModelBase))
			{
				_003CPaneViewModel_003Ek__BackingField = viewModelBase;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PaneViewModel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool PaneOpen
	{
		get
		{
			return _003CPaneOpen_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CPaneOpen_003Ek__BackingField, flag))
			{
				_003CPaneOpen_003Ek__BackingField = flag;
				OnPaneOpenChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PaneOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ProfileOpen
	{
		get
		{
			return _003CProfileOpen_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CProfileOpen_003Ek__BackingField, flag))
			{
				_003CProfileOpen_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ProfileOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool FriendsOpen
	{
		get
		{
			return _003CFriendsOpen_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CFriendsOpen_003Ek__BackingField, flag))
			{
				_003CFriendsOpen_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.FriendsOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool DirectMessagesOpen
	{
		get
		{
			return _003CDirectMessagesOpen_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CDirectMessagesOpen_003Ek__BackingField, flag))
			{
				_003CDirectMessagesOpen_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DirectMessagesOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool NotificationsOpen
	{
		get
		{
			return _003CNotificationsOpen_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CNotificationsOpen_003Ek__BackingField, flag))
			{
				_003CNotificationsOpen_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.NotificationsOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double PaneWidth
	{
		get
		{
			return _003CPaneWidth_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(_003CPaneWidth_003Ek__BackingField, num))
			{
				_003CPaneWidth_003Ek__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PaneWidth);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double RightThumbWidth
	{
		get
		{
			return _003CRightThumbWidth_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(_003CRightThumbWidth_003Ek__BackingField, num))
			{
				_003CRightThumbWidth_003Ek__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.RightThumbWidth);
			}
		}
	}

	public PaneDisplayService PaneDisplayService { get; }

	public IRootSessionAccessor RootSessionAccessor { get; }

	public VoiceBarViewModel VoiceBarViewModel { get; }

	public StreamerModeBannerViewModel StreamerModeBannerViewModel { get; }

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(RootSessionAccessor.Session.UserInfoService.SessionUser.ProfilePictureUri, null, 120);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<TabsControl.MoveTabParameters> MoveRequestedCommand => moveRequestedCommand ?? (moveRequestedCommand = new RelayCommand<TabsControl.MoveTabParameters>(MoveRequested));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CreateNewTabCommand => createNewTabCommand ?? (createNewTabCommand = new RelayCommand(CreateNewTab));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<ITabViewModel> CloseTabCommand => closeTabCommand ?? (closeTabCommand = new RelayCommand<ITabViewModel>(CloseTab));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand FriendsPaneToggleCommand => friendsPaneToggleCommand ?? (friendsPaneToggleCommand = new RelayCommand(FriendsPaneToggle));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand DirectMessagesPaneToggleCommand => directMessagesPaneToggleCommand ?? (directMessagesPaneToggleCommand = new RelayCommand(DirectMessagesPaneToggle));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand NotificationsPaneToggleCommand => notificationsPaneToggleCommand ?? (notificationsPaneToggleCommand = new RelayCommand(NotificationsPaneToggle));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ProfilePaneToggleCommand => profilePaneToggleCommand ?? (profilePaneToggleCommand = new RelayCommand(ProfilePaneToggle));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ResendEmailCommand => resendEmailCommand ?? (resendEmailCommand = new AsyncRelayCommand(ResendEmailAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand EnterVerificationCodeCommand => enterVerificationCodeCommand ?? (enterVerificationCodeCommand = new RelayCommand(EnterVerificationCode));

	public HomeViewModel(FriendsViewModelFactory P_0, DirectMessagesViewModelFactory P_1, NotificationsViewModelFactory P_2, ProfileViewModelFactory P_3, IRootSessionAccessor P_4, CommunityTabViewModelFactory P_5, DirectMessageTabViewModelFactory P_6, NewTabViewModelFactory P_7, BitmapCache P_8, ILocalDataStore P_9, PaneDisplayService P_10, VoiceBarViewModelFactory P_11, EnterVerificationCodeViewModelFactory P_12, VerificationCodeCooldownViewModelFactory P_13, DirectMessageIncomingCallServiceFactory P_14, CallPopoutService P_15, IUriSchemeRegistrar P_16, LinkJoiningService P_17, OverlayService P_18, IStreamerModeService P_19, ITabPopoutService P_20)
		: base((IValidator<HomeViewModel>?)null)
	{
		_localDataStore = P_9;
		_bitmapCache = P_8;
		_tabPopoutService = P_20;
		RootSessionAccessor = P_4;
		PaneDisplayService = P_10;
		VoiceBarViewModel = P_11.Create(P_15);
		StreamerModeBannerViewModel = new StreamerModeBannerViewModel(P_19);
		_directMessageIncomingCallService = P_14.Create();
		_tabsDisposable = RootSessionAccessor.Session.TabService.ConnectTabs().Transform(delegate(Tab tab)
		{
			if (tab.ContainerId == null)
			{
				return P_7.Create(tab);
			}
			return tab.IsCommunity ? ((ITabViewModel)P_5.Create(tab)) : ((ITabViewModel)P_6.Create(tab));
		}).ObserveOn(AvaloniaScheduler.Instance)
			.Bind(out _tabs)
			.DisposeMany()
			.Subscribe();
		_friendsViewModel = P_0.Create();
		_directMessagesViewModel = P_1.Create();
		_notificationsViewModel = P_2.Create();
		_profileViewModel = P_3.Create();
		_enterVerificationCodeViewModelFactory = P_12;
		_verificationCodeCooldownViewModelFactory = P_13;
		_linkJoiningService = P_17;
		_overlayService = P_18;
		_tabPopoutService.Initialize();
		RootSessionAccessor.Session.UserInfoService.SessionUser.PropertyChanged += onSessionUserPropertyChanged;
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = RootSessionAccessor.Session.UserInfoService.SessionUser.Id;
		_003C_003Ey__InlineArray281[1] = DataStoreKeys.OpenedSystemTrayPane.ToString();
		if (localDataStore.TryGetWithPath((ReadOnlySpan<string>)_003C_003Ey__InlineArray281, out int num))
		{
			togglePane(num, false);
		}
		WeakReferenceMessenger.Default.Register<OpenDirectMessagePaneMessage>(this, onDirectMessagePaneMessageReceived);
		WeakReferenceMessenger.Default.Register<SelectTabMessage>(this, onSelectTabMessageReceived);
		WeakReferenceMessenger.Default.Register<SwitchTabMessage>(this, onSwitchTabMessageReceived);
		WeakReferenceMessenger.Default.Register<PopoutCurrentTabMessage>(this, onPopoutCurrentTabMessageReceived);
		WeakReferenceMessenger.Default.Register<SwitchChannelMessage>(this, onSwitchChannelMessageReceived);
		WeakReferenceMessenger.Default.Register<SearchCurrentChannelMessage>(this, onSearchCurrentChannelMessageReceived);
		WeakReferenceMessenger.Default.Register<SearchCommunityMessage>(this, onSearchCommunityMessageReceived);
		WeakReferenceMessenger.Default.Register<MarkChannelAsReadMessage>(this, onMarkChannelAsReadMessageReceived);
		WeakReferenceMessenger.Default.Register<MarkCommunityAsReadMessage>(this, onMarkCommunityAsReadMessageReceived);
		WeakReferenceMessenger.Default.Register<UploadFileMessage>(this, onUploadFileMessageReceived);
		WeakReferenceMessenger.Default.Register<ReturnToPresentMessage>(this, onReturnToPresentMessageReceived);
		_overlayService.Initialize();
		P_16.EnsureRegistered();
		_linkJoiningService.SetCanOpenLink();
		checkForLaunchArgsAsync().Forget();
	}

	private void onSessionUserPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "ProfilePictureUri")
			{
				OnPropertyChanged("ProfilePictureAsyncBitmapWrapper");
			}
		});
	}

	[RelayCommand]
	public void MoveRequested(TabsControl.MoveTabParameters parameters)
	{
		RootSessionAccessor.Session.TabService.MoveTab(parameters.OldIndex, parameters.NewIndex);
	}

	[RelayCommand]
	public void CreateNewTab()
	{
		if (RootSessionAccessor.Session.TabService.ContainsTab(null))
		{
			WeakReferenceMessenger.Default.Send(new SelectTabMessage(null));
		}
		else
		{
			RootSessionAccessor.Session.TabService.CreateNewTab();
		}
	}

	[RelayCommand]
	public void CloseTab(ITabViewModel tabViewModel)
	{
		RootSessionAccessor.Session.TabService.RemoveTab(tabViewModel.Tab.ContainerId);
	}

	[RelayCommand]
	public void FriendsPaneToggle()
	{
		togglePane(0);
	}

	[RelayCommand]
	public void DirectMessagesPaneToggle()
	{
		togglePane(1);
	}

	[RelayCommand]
	public void NotificationsPaneToggle()
	{
		togglePane(2);
	}

	[RelayCommand]
	public void ProfilePaneToggle()
	{
		togglePane(3);
	}

	[RelayCommand]
	public async Task ResendEmailAsync()
	{
		try
		{
			ResendEmailVerificationWebApiStatus = WebApiStatus.Sending;
			await RootSessionAccessor.Session.UserInfoService.ResendVerificationEmailAsync();
			ResendEmailVerificationWebApiStatus = WebApiStatus.Success;
			EnterVerificationCodeViewModel enterVerificationCodeViewModel = _enterVerificationCodeViewModelFactory.Create();
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(enterVerificationCodeViewModel));
		}
		catch (TimeoutException)
		{
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(_verificationCodeCooldownViewModelFactory.Create()));
			ResendEmailVerificationWebApiStatus = WebApiStatus.Failed;
		}
		catch
		{
			ResendEmailVerificationWebApiStatus = WebApiStatus.Failed;
		}
	}

	[RelayCommand]
	public void EnterVerificationCode()
	{
		EnterVerificationCodeViewModel enterVerificationCodeViewModel = _enterVerificationCodeViewModelFactory.Create();
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(enterVerificationCodeViewModel));
	}

	private void togglePane(int P_0, bool P_1 = true)
	{
		bool paneOpen = PaneOpen;
		switch (P_0)
		{
		case 0:
			if (FriendsOpen)
			{
				PaneOpen = false;
			}
			else
			{
				PaneOpen = true;
				PaneViewModel = _friendsViewModel;
			}
			FriendsOpen = PaneOpen;
			DirectMessagesOpen = false;
			NotificationsOpen = false;
			ProfileOpen = false;
			PaneWidth = 320.0;
			break;
		case 1:
			if (DirectMessagesOpen)
			{
				PaneOpen = false;
			}
			else
			{
				PaneOpen = true;
				PaneViewModel = _directMessagesViewModel;
			}
			DirectMessagesOpen = PaneOpen;
			FriendsOpen = false;
			NotificationsOpen = false;
			ProfileOpen = false;
			PaneWidth = 415.0;
			break;
		case 2:
			if (NotificationsOpen)
			{
				PaneOpen = false;
			}
			else
			{
				PaneOpen = true;
				PaneViewModel = _notificationsViewModel;
			}
			NotificationsOpen = PaneOpen;
			FriendsOpen = false;
			DirectMessagesOpen = false;
			ProfileOpen = false;
			PaneWidth = 320.0;
			break;
		case 3:
			if (ProfileOpen)
			{
				PaneOpen = false;
			}
			else
			{
				PaneOpen = true;
				PaneViewModel = _profileViewModel;
			}
			ProfileOpen = PaneOpen;
			FriendsOpen = false;
			DirectMessagesOpen = false;
			NotificationsOpen = false;
			PaneWidth = 320.0;
			break;
		}
		if (P_1)
		{
			if (PaneOpen)
			{
				ILocalDataStore localDataStore = _localDataStore;
				global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
				_003C_003Ey__InlineArray281[0] = RootSessionAccessor.Session.UserInfoService.SessionUser.Id;
				_003C_003Ey__InlineArray281[1] = "OpenedSystemTrayPane";
				localDataStore.SetWithPath(_003C_003Ey__InlineArray281, P_0);
			}
			else
			{
				ILocalDataStore localDataStore2 = _localDataStore;
				global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray282 = default(global::_003C_003Ey__InlineArray2<string>);
				_003C_003Ey__InlineArray282[0] = RootSessionAccessor.Session.UserInfoService.SessionUser.Id;
				_003C_003Ey__InlineArray282[1] = "OpenedSystemTrayPane";
				localDataStore2.SetWithPath(_003C_003Ey__InlineArray282, -1);
			}
		}
		if (paneOpen != PaneOpen && PaneOpen && PaneDisplayService.CommunityPaneDisplayMode == SplitViewDisplayMode.Overlay)
		{
			PaneDisplayService.SetGlobalPaneDisplayMode(SplitViewDisplayMode.Inline);
		}
	}

	private void onDirectMessagePaneMessageReceived(object recipient, OpenDirectMessagePaneMessage message)
	{
		DirectMessage directMessage = RootSessionAccessor.Session.DirectMessageService.GetDirectMessage(message.DirectMessageId);
		if (directMessage != null)
		{
			_directMessagesViewModel.OpenConversation(directMessage, message.MessageId, message.JoinVoiceCall);
			if (!DirectMessagesOpen)
			{
				togglePane(1);
			}
			if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: MainWindow mainWindow })
			{
				mainWindow.RestoreFromTray();
			}
		}
	}

	private void onSelectTabMessageReceived(object recipient, SelectTabMessage message)
	{
		SelectedTabViewModel = Tabs.FirstOrDefault((ITabViewModel t) => t.Tab.ContainerId == message.TabId);
		if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: MainWindow mainWindow })
		{
			mainWindow.RestoreFromTray();
		}
	}

	private void onSwitchTabMessageReceived(object recipient, SwitchTabMessage message)
	{
		if (Tabs.Count != 0 && SelectedTabViewModel != null)
		{
			int num = Tabs.IndexOf(SelectedTabViewModel);
			if (num != -1)
			{
				int num2 = ((!message.ToPrevious) ? ((num < Tabs.Count - 1) ? (num + 1) : 0) : ((num > 0) ? (num - 1) : (Tabs.Count - 1)));
				SelectedTabViewModel = Tabs[num2];
			}
		}
	}

	private void onPopoutCurrentTabMessageReceived(object recipient, PopoutCurrentTabMessage message)
	{
		if (SelectedTabViewModel != null && !(SelectedTabViewModel.Tab.ContainerId == null))
		{
			_tabPopoutService.PopoutTab(SelectedTabViewModel);
		}
	}

	private void onSwitchChannelMessageReceived(object recipient, SwitchChannelMessage message)
	{
		if (!(SelectedTabViewModel is CommunityTabViewModel communityTabViewModel))
		{
			return;
		}
		Community community = communityTabViewModel.Community;
		if (community?.Channels == null)
		{
			return;
		}
		List<Channel> list = (from id in community.Channels.GetChannelIds()
			select community.Channels.GetChannel(id) into c
			where c != null && c.Type == ChannelType.Text
			orderby c.Position
			select c).ToList();
		if (list.Count != 0)
		{
			Channel currentChannel = community.SelectedChannel;
			int num = ((currentChannel != null) ? list.FindIndex((Channel c) => c.Id == currentChannel.Id) : (-1));
			int num2 = (message.ToPrevious ? ((num > 0) ? (num - 1) : (list.Count - 1)) : ((num >= 0 && num < list.Count - 1) ? (num + 1) : 0));
			Channel channel = list[num2];
			if (channel != null)
			{
				community.SelectChannel(channel);
			}
		}
	}

	private void onSearchCurrentChannelMessageReceived(object recipient, SearchCurrentChannelMessage message)
	{
		if (SelectedTabViewModel is CommunityTabViewModel communityTabViewModel)
		{
			Community community = communityTabViewModel.Community;
			Channel channel = community?.SelectedChannel;
			if (community != null && channel != null)
			{
				WeakReferenceMessenger.Default.Send(new OpenSearchPaneMessage(false, community.Id, channel.Id));
			}
		}
	}

	private void onSearchCommunityMessageReceived(object recipient, SearchCommunityMessage message)
	{
		if (SelectedTabViewModel is CommunityTabViewModel communityTabViewModel)
		{
			Community community = communityTabViewModel.Community;
			if (community != null)
			{
				WeakReferenceMessenger.Default.Send(new OpenSearchPaneMessage(true, community.Id));
			}
		}
	}

	private void onMarkChannelAsReadMessageReceived(object recipient, MarkChannelAsReadMessage message)
	{
		try
		{
			if (SelectedTabViewModel is CommunityTabViewModel communityTabViewModel)
			{
				Community community = communityTabViewModel.Community;
				Channel channel = community?.SelectedChannel;
				if (channel != null && channel.Type == ChannelType.Text)
				{
					channel.Messages.SetViewTimeAsync().Forget();
					RootSessionAccessor.Session.NotificationService.SetContainerAsViewedAsync(community.Id, channel.Id).Forget();
				}
			}
			else if (SelectedTabViewModel is DirectMessageTabViewModel directMessageTabViewModel)
			{
				directMessageTabViewModel.MarkAsReadCommand.Execute(null);
			}
		}
		catch
		{
		}
	}

	private void onMarkCommunityAsReadMessageReceived(object recipient, MarkCommunityAsReadMessage message)
	{
		if (!(SelectedTabViewModel is CommunityTabViewModel communityTabViewModel))
		{
			return;
		}
		try
		{
			Community community = communityTabViewModel.Community;
			if (community?.Channels == null)
			{
				return;
			}
			IEnumerable<ChannelGuid> channelIds = community.Channels.GetChannelIds();
			foreach (ChannelGuid item in channelIds)
			{
				Channel channel = community.Channels.GetChannel(item);
				if (channel != null && channel.Type == ChannelType.Text && channel.HasActivity)
				{
					channel.Messages.SetViewTimeAsync().Forget();
					RootSessionAccessor.Session.NotificationService.SetContainerAsViewedAsync(community.Id, item).Forget();
				}
			}
		}
		catch
		{
		}
	}

	private void onUploadFileMessageReceived(object recipient, UploadFileMessage message)
	{
		if (!(Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime))
		{
			return;
		}
		IStorageProvider storageProvider = classicDesktopStyleApplicationLifetime.MainWindow?.StorageProvider;
		if (storageProvider == null)
		{
			return;
		}
		if (SelectedTabViewModel is CommunityTabViewModel communityTabViewModel)
		{
			Channel channel = communityTabViewModel.Community?.SelectedChannel;
			if (channel != null && channel.Type == ChannelType.Text && communityTabViewModel.ContentViewModel is CommunityViewModel { SelectedChannelContent: TextChannelContentViewModel selectedChannelContent })
			{
				selectedChannelContent.RootMessageTextboxViewModel.OpenFilePickerCommand.Execute(storageProvider);
			}
		}
		else if (SelectedTabViewModel is DirectMessageTabViewModel { ContentViewModel: DirectMessageContentViewModel contentViewModel })
		{
			contentViewModel.RootMessageTextboxViewModel.OpenFilePickerCommand.Execute(storageProvider);
		}
	}

	private void onReturnToPresentMessageReceived(object recipient, ReturnToPresentMessage message)
	{
		if (SelectedTabViewModel is CommunityTabViewModel communityTabViewModel)
		{
			Channel channel = communityTabViewModel.Community?.SelectedChannel;
			if (channel != null && channel.Type == ChannelType.Text && communityTabViewModel.ContentViewModel is CommunityViewModel { SelectedChannelContent: TextChannelContentViewModel selectedChannelContent })
			{
				selectedChannelContent.MarkAsReadCommand.Execute(null);
			}
		}
		else if (SelectedTabViewModel is DirectMessageTabViewModel { ContentViewModel: DirectMessageContentViewModel contentViewModel })
		{
			contentViewModel.MarkAsReadCommand.Execute(null);
		}
	}

	private async Task checkForLaunchArgsAsync()
	{
		IApplicationLifetime applicationLifetime = Application.Current?.ApplicationLifetime;
		if (!(applicationLifetime is IClassicDesktopStyleApplicationLifetime desktop))
		{
			return;
		}
		string[] args = _linkJoiningService.MacLaunchUri ?? desktop.Args;
		if (args == null || args.Length == 0)
		{
			return;
		}
		string[] array = args;
		foreach (string arg in array)
		{
			int idx = arg.IndexOf("/launch/", StringComparison.OrdinalIgnoreCase);
			if (idx >= 0)
			{
				string text = arg;
				int num = idx + "/launch/".Length;
				string launchId = text.Substring(num, text.Length - num);
				if (!string.IsNullOrWhiteSpace(launchId))
				{
					await Task.Delay(500);
					await _linkJoiningService.OpenLinkAsync(launchId);
					break;
				}
			}
		}
	}

	public override void Dispose()
	{
		base.Dispose();
		_tabsDisposable.Dispose();
		_friendsViewModel.Dispose();
		_directMessagesViewModel.Dispose();
		_notificationsViewModel.Dispose();
		_profileViewModel.Dispose();
		_directMessageIncomingCallService.Dispose();
		_overlayService.Dispose();
		RootSessionAccessor.Session.UserInfoService.SessionUser.PropertyChanged -= onSessionUserPropertyChanged;
		WeakReferenceMessenger.Default.Unregister<OpenDirectMessagePaneMessage>(this);
		WeakReferenceMessenger.Default.Unregister<SelectTabMessage>(this);
		WeakReferenceMessenger.Default.Unregister<SwitchTabMessage>(this);
		WeakReferenceMessenger.Default.Unregister<PopoutCurrentTabMessage>(this);
		WeakReferenceMessenger.Default.Unregister<SwitchChannelMessage>(this);
		WeakReferenceMessenger.Default.Unregister<SearchCurrentChannelMessage>(this);
		WeakReferenceMessenger.Default.Unregister<SearchCommunityMessage>(this);
		WeakReferenceMessenger.Default.Unregister<MarkChannelAsReadMessage>(this);
		WeakReferenceMessenger.Default.Unregister<MarkCommunityAsReadMessage>(this);
		WeakReferenceMessenger.Default.Unregister<UploadFileMessage>(this);
		WeakReferenceMessenger.Default.Unregister<ReturnToPresentMessage>(this);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnPaneOpenChanged(bool P_0)
	{
		if (!PaneOpen)
		{
			FriendsOpen = false;
			DirectMessagesOpen = false;
			NotificationsOpen = false;
			ProfileOpen = false;
		}
	}
}

