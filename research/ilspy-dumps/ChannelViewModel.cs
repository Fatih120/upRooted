using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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
using RootApp.Browser;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.DeveloperMode;
using RootApp.Client.Avalonia.Helpers.RootApps;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class ChannelViewModel : ViewModelBase<ChannelViewModel>
{
	private readonly IDisposable? _cacheCleanup;

	private readonly BitmapCache _bitmapCache;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly IAppChannelService _appChannelService;

	private readonly CallPopoutService _callPopoutService;

	private readonly BrowserService _browserService;

	private readonly EditChannelViewModelFactory _editChannelViewModelFactory;

	private readonly DeleteChannelViewModelFactory _deleteChannelViewModelFactory;

	private readonly CommunitySettingsViewModelFactory _communitySettingsViewModelFactory;

	private readonly RootMarkdownEngineManager _rootMarkdownEngineManager;

	private readonly IDeveloperModeService _developerModeService;

	private readonly ClipboardService _clipboardService;

	private IDisposable? _appStatusSubscription;

	private CommunityApp? _communityApp;

	[CompilerGenerated]
	private Channel <Channel>k__BackingField = null;

	private readonly ReadOnlyObservableCollection<ChannelMediaMemberViewModel>? _mediaMembers;

	private readonly CallingService _callingService;

	[CompilerGenerated]
	private bool <HasCustomChannelIcon>k__BackingField;

	[CompilerGenerated]
	private bool <IsSelectedOrHighlighted>k__BackingField;

	[CompilerGenerated]
	private bool <CanSelectChannel>k__BackingField;

	[CompilerGenerated]
	private AppChannelStatus? <AppStatus>k__BackingField;

	[CompilerGenerated]
	private string? <AppErrorTooltip>k__BackingField;

	[CompilerGenerated]
	private bool <AppHasUpdateAvailable>k__BackingField;

	[CompilerGenerated]
	private string? <AppUpdateStatusText>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showEditChannelViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showDeleteChannelViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showAppSettingsViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? copyChannelIdCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? markAsReadCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? pointerEnteredCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? pointerExitedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? selectChannelCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HasCustomChannelIcon
	{
		get
		{
			return <HasCustomChannelIcon>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<HasCustomChannelIcon>k__BackingField, flag))
			{
				<HasCustomChannelIcon>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasCustomChannelIcon);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsSelectedOrHighlighted
	{
		get
		{
			return <IsSelectedOrHighlighted>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsSelectedOrHighlighted>k__BackingField, flag))
			{
				<IsSelectedOrHighlighted>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsSelectedOrHighlighted);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Channel Channel
	{
		get
		{
			return <Channel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<RootApp.Client.CoreDomain.Models.Community.Channel>.Default.Equals(<Channel>k__BackingField, channel))
			{
				<Channel>k__BackingField = channel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Channel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool CanSelectChannel
	{
		get
		{
			return <CanSelectChannel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<CanSelectChannel>k__BackingField, flag))
			{
				<CanSelectChannel>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CanSelectChannel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public AppChannelStatus? AppStatus
	{
		get
		{
			return <AppStatus>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<AppChannelStatus>.Default.Equals(<AppStatus>k__BackingField, appChannelStatus))
			{
				<AppStatus>k__BackingField = appChannelStatus;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AppStatus);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? AppErrorTooltip
	{
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<AppErrorTooltip>k__BackingField, text))
			{
				<AppErrorTooltip>k__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AppErrorTooltip);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool AppHasUpdateAvailable
	{
		get
		{
			return <AppHasUpdateAvailable>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<AppHasUpdateAvailable>k__BackingField, flag))
			{
				<AppHasUpdateAvailable>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AppHasUpdateAvailable);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? AppUpdateStatusText
	{
		get
		{
			return <AppUpdateStatusText>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<AppUpdateStatusText>k__BackingField, text))
			{
				<AppUpdateStatusText>k__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AppUpdateStatusText);
			}
		}
	}

	public string? DisplayDescription => (!string.IsNullOrEmpty(AppUpdateStatusText)) ? AppUpdateStatusText : Channel.Description;

	public bool IsAppChannel => Channel.Type == ChannelType.App;

	public bool CanDeleteChannel => Channel.LocalChannelPermission.ChannelFullControl && !IsAppChannel;

	public bool CanShowAppSettings => IsAppChannel && Channel.Community.LocalCommunityPermission.CommunityManageApps;

	public bool DeveloperModeEnabled => _developerModeService.IsEnabled;

	public ReadOnlyObservableCollection<ChannelMediaMemberViewModel>? MediaMembers => _mediaMembers;

	public Task<BitmapWrapper?> ChannelPictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(Channel.IconAssetUri, null, 120);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowEditChannelViewModelCommand => showEditChannelViewModelCommand ?? (showEditChannelViewModelCommand = new RelayCommand(ShowEditChannelViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowDeleteChannelViewModelCommand => showDeleteChannelViewModelCommand ?? (showDeleteChannelViewModelCommand = new RelayCommand(ShowDeleteChannelViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowAppSettingsViewModelCommand => showAppSettingsViewModelCommand ?? (showAppSettingsViewModelCommand = new RelayCommand(ShowAppSettingsViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CopyChannelIdCommand => copyChannelIdCommand ?? (copyChannelIdCommand = new RelayCommand(CopyChannelId));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand MarkAsReadCommand => markAsReadCommand ?? (markAsReadCommand = new RelayCommand(MarkAsRead));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PointerEnteredCommand => pointerEnteredCommand ?? (pointerEnteredCommand = new RelayCommand(PointerEntered));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PointerExitedCommand => pointerExitedCommand ?? (pointerExitedCommand = new RelayCommand(PointerExited));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand SelectChannelCommand => selectChannelCommand ?? (selectChannelCommand = new AsyncRelayCommand(SelectChannelAsync));

	public ChannelViewModel(Channel P_0, EditChannelViewModelFactory P_1, DeleteChannelViewModelFactory P_2, ChannelMediaMemberViewModelFactory P_3, BrowserService P_4, BitmapCache P_5, IRootSessionAccessor P_6, CallingServiceFactory P_7, IAppChannelService P_8, CallPopoutService P_9, CommunitySettingsViewModelFactory P_10, RootMarkdownEngineManager P_11, IDeveloperModeService P_12, ClipboardService P_13)
		: base((IValidator<ChannelViewModel>?)null)
	{
		ChannelViewModel channelViewModel = this;
		Channel = P_0;
		HasCustomChannelIcon = !string.IsNullOrEmpty(Channel.IconAssetUri);
		_editChannelViewModelFactory = P_1;
		_deleteChannelViewModelFactory = P_2;
		_communitySettingsViewModelFactory = P_10;
		_rootMarkdownEngineManager = P_11;
		_developerModeService = P_12;
		_clipboardService = P_13;
		_browserService = P_4;
		_bitmapCache = P_5;
		_rootSessionAccessor = P_6;
		_appChannelService = P_8;
		_callPopoutService = P_9;
		_callingService = P_7.Create(_browserService);
		CanSelectChannel = Channel.Type != ChannelType.App;
		Channel.PropertyChanged += onPropertyChanged;
		Channel.Community.PropertyChanged += onPropertyChanged;
		_developerModeService.PropertyChanged += onDeveloperModePropertyChanged;
		determineSelectionState();
		WeakReferenceMessenger.Default.Register<NavigateToChannelMessage>(this, onNavigateToChannelMessageReceived);
		if (Channel.MediaRoom != null)
		{
			_cacheCleanup = Channel.MediaRoom.ConnectMembers().Transform((MediaMember m) => P_3.Create(m, channelViewModel.Channel)).ObserveOn(AvaloniaScheduler.Instance)
				.Bind(out _mediaMembers)
				.DisposeMany()
				.Subscribe();
		}
		if (Channel.Type == ChannelType.App)
		{
			initializeAppChannel();
		}
	}

	private void initializeAppChannel()
	{
		_appChannelService.BeginPrepare(Channel);
		_appStatusSubscription = _appChannelService.ObserveStatus(Channel.Id).ObserveOn(AvaloniaScheduler.Instance).Subscribe(onAppStatusChanged);
		if (Channel.CommunityAppId.HasValue)
		{
			_communityApp = Channel.Community.Apps?.GetApp(Channel.CommunityAppId.Value);
			if (_communityApp != null)
			{
				_communityApp.PropertyChanged += onCommunityAppPropertyChanged;
				updateAppUpdateProperties();
			}
		}
	}

	private void onCommunityAppPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "UpdateStatus" || e.PropertyName == "LatestVersionId" || e.PropertyName == "VersionId")
		{
			Dispatcher.UIThread.Post(delegate
			{
				updateAppUpdateProperties();
			});
		}
	}

	private void updateAppUpdateProperties()
	{
		if (_communityApp == null)
		{
			AppHasUpdateAvailable = false;
			AppUpdateStatusText = null;
			OnPropertyChanged("DisplayDescription");
			return;
		}
		AppHasUpdateAvailable = _communityApp.HasUpdateAvailable;
		if (_communityApp.IsUpdateImminent)
		{
			string text = _communityApp.LatestVersion ?? "new version";
			AppUpdateStatusText = "Updating to " + text + "...";
		}
		else if (_communityApp.HasUpdateAvailable)
		{
			string text2 = _communityApp.LatestVersion ?? "new version";
			AppUpdateStatusText = "Update available (" + text2 + ")";
		}
		else
		{
			AppUpdateStatusText = null;
		}
		OnPropertyChanged("DisplayDescription");
	}

	private void onAppStatusChanged(AppChannelStatus status)
	{
		AppStatus = status;
		CanSelectChannel = status.IsReady || status.IsBrowserDisposed;
		if (status.HasError)
		{
			AppErrorTooltip = status.ErrorMessage ?? "Failed to load app";
		}
		else
		{
			AppErrorTooltip = null;
		}
	}

	[RelayCommand]
	public void ShowEditChannelViewModel()
	{
		EditChannelViewModel editChannelViewModel = _editChannelViewModelFactory.Create(Channel);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(editChannelViewModel));
	}

	[RelayCommand]
	public void ShowDeleteChannelViewModel()
	{
		DeleteChannelViewModel deleteChannelViewModel = _deleteChannelViewModelFactory.Create(Channel);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(deleteChannelViewModel));
	}

	[RelayCommand]
	public void ShowAppSettingsViewModel()
	{
		CommunitySettingsViewModel communitySettingsViewModel = _communitySettingsViewModelFactory.Create(Channel.Community, Channel.CommunityAppId);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(communitySettingsViewModel));
	}

	[RelayCommand]
	public void CopyChannelId()
	{
		_clipboardService.CopyTextToClipboard(Channel.Id.ToString());
	}

	[RelayCommand]
	public void MarkAsRead()
	{
		try
		{
			if (Channel.Type == ChannelType.Text)
			{
				Channel.Messages.SetViewTimeAsync().Forget();
				_rootSessionAccessor.Session.NotificationService.SetContainerAsViewedAsync(Channel.Community.Id, Channel.Id).Forget();
			}
		}
		catch
		{
		}
	}

	[RelayCommand]
	public void PointerEntered()
	{
		if (Channel.Community.SelectedChannel != Channel)
		{
			IsSelectedOrHighlighted = true;
		}
	}

	[RelayCommand]
	public void PointerExited()
	{
		if (Channel.Community.SelectedChannel != Channel)
		{
			IsSelectedOrHighlighted = false;
		}
	}

	[RelayCommand]
	public async Task SelectChannelAsync()
	{
		if (Channel.Community.SelectedChannel == Channel)
		{
			return;
		}
		if (Channel.Type == ChannelType.Voice)
		{
			await connectToVoiceChannelAsync();
		}
		else if (Channel.Type == ChannelType.App)
		{
			await connectToAppChannelAsync();
		}
		if (Channel.Type == ChannelType.Voice)
		{
			if ((Channel.MediaRoom?.HasMediaView() ?? false) || Channel.MediaRoom?.SelfMediaMember != null)
			{
				MediaRoom? mediaRoom = Channel.MediaRoom;
				if (mediaRoom != null && !mediaRoom.IsPoppedOut)
				{
					Channel.Community.SelectChannel(Channel);
				}
				else
				{
					_callPopoutService.FocusPopoutWindow();
				}
			}
		}
		else
		{
			Channel.Community.SelectChannel(Channel);
		}
	}

	private void determineSelectionState()
	{
		IsSelectedOrHighlighted = Channel.Community.SelectedChannel == Channel;
	}

	private void onPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "SelectedChannel")
			{
				determineSelectionState();
			}
			else if (e.PropertyName == "IconAssetUri")
			{
				OnPropertyChanged("ChannelPictureAsyncBitmapWrapper");
				HasCustomChannelIcon = !string.IsNullOrEmpty(Channel.IconAssetUri);
			}
			else if (e.PropertyName == "Description")
			{
				OnPropertyChanged("DisplayDescription");
			}
		});
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

	private void onNavigateToChannelMessageReceived(object recipient, NavigateToChannelMessage message)
	{
		if (message.ChannelId == Channel.Id)
		{
			SelectChannelAsync().Forget();
		}
	}

	private async Task connectToVoiceChannelAsync()
	{
		MediaRoom activeMediaRoom = _rootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom;
		if (activeMediaRoom != null && (MessageContainerGuid?)activeMediaRoom.MessageContainer.ContainerId != (MessageContainerGuid?)(MessageContainerGuid)Channel.Id)
		{
			SwitchVoiceCallConfirmationViewModel confirmationViewModel = new SwitchVoiceCallConfirmationViewModel(activeMediaRoom.MessageContainer.Name, Channel.Name, _callingService, Channel.Id, Channel.MediaRoom, _rootMarkdownEngineManager);
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(confirmationViewModel));
		}
		else
		{
			await _callingService.JoinVoiceCallAsync(Channel.Id, Channel.MediaRoom);
		}
	}

	private async Task connectToAppChannelAsync()
	{
		if (AppStatus?.IsBrowserDisposed ?? false)
		{
			await _appChannelService.RecreateBrowserAsync(Channel);
			return;
		}
		AppChannelStatus? appStatus = AppStatus;
		if ((object)appStatus != null && appStatus.IsReady && _browserService.GetBrowser(Channel.Id) == null)
		{
			await _appChannelService.CreateBrowserAsync(Channel);
		}
	}

	public override void Dispose()
	{
		base.Dispose();
		if (!Channel.IsLocked)
		{
			_browserService.RemoveBrowser(Channel.Id);
			if (Channel.Type == ChannelType.App)
			{
				_appChannelService.ReleaseChannel(Channel.Id);
			}
		}
		if (Channel.IsLocked)
		{
			Channel.SetLock(false);
		}
		_appStatusSubscription?.Dispose();
		if (_communityApp != null)
		{
			_communityApp.PropertyChanged -= onCommunityAppPropertyChanged;
		}
		Channel.PropertyChanged -= onPropertyChanged;
		Channel.Community.PropertyChanged -= onPropertyChanged;
		_developerModeService.PropertyChanged -= onDeveloperModePropertyChanged;
		_cacheCleanup?.Dispose();
	}
}
