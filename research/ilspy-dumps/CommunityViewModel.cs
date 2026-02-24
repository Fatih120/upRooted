using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.VisualStudio.Threading;
using RootApp.Browser;
using RootApp.Client.Avalonia.Helpers.Panes;
using RootApp.Client.Avalonia.Helpers.RootApps.Connection;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community.Channels;
using RootApp.Client.Avalonia.UI.Community.Content;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Community.Panes.Directories;
using RootApp.Client.Avalonia.UI.Community.Panes.Pinned;
using RootApp.Client.Avalonia.UI.Community.Panes.Search;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.Domain.Helpers.Store;
using RootApp.Core.Enums;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.Client.Avalonia.UI.Community;

public class CommunityViewModel : CachedViewModelBase
{
	private readonly Dictionary<ChannelGuid, IViewModelBase> _channelContentViewModels = new Dictionary<ChannelGuid, IViewModelBase>();

	private readonly ChannelsViewModelFactory _channelsViewModelFactory;

	private readonly MembersViewModelFactory _memberViewModelFactory;

	private readonly ILocalDataStore _localDataStore;

	private readonly TextChannelContentViewModelFactory _textChannelContentViewModelFactory;

	private readonly VoiceChannelContentViewModelFactory _voiceChannelContentViewModelFactory;

	private readonly AppChannelContentViewModelFactory _appChannelContentViewModelFactory;

	private readonly ICommunityHubConnectionService _communityHubConnectionService;

	private readonly BrowserService _browserService;

	private readonly CommunityGuid _communityId;

	private readonly RootGuid? _startupSubContainerId;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private MessageGuid? _startupMessageId;

	[CompilerGenerated]
	private GridLength <CommunityChannelsWidth>k__BackingField = new GridLength(280.0);

	private readonly PinnedMessagesViewModelFactory _pinnedMessagesViewModelFactory;

	private readonly DirectoriesViewModelFactory _directoriesViewModelFactory;

	private readonly SearchViewModelFactory _searchViewModelFactory;

	private readonly Dictionary<ChannelGuid, PinnedMessagesViewModel> _channelPinnedMessagesViewModels = new Dictionary<ChannelGuid, PinnedMessagesViewModel>();

	private readonly Dictionary<ChannelGuid, DirectoriesViewModel> _channelDirectoriesViewModels = new Dictionary<ChannelGuid, DirectoriesViewModel>();

	private SearchViewModel? _searchViewModel;

	[CompilerGenerated]
	private RootApp.Client.CoreDomain.Models.Community.Community? <Community>k__BackingField;

	[CompilerGenerated]
	private MembersViewModel? <Members>k__BackingField;

	[CompilerGenerated]
	private ChannelsViewModel? <Channels>k__BackingField;

	[CompilerGenerated]
	private IViewModelBase? <SelectedChannelContent>k__BackingField;

	[CompilerGenerated]
	private bool <PaneOpen>k__BackingField;

	[CompilerGenerated]
	private bool <IsPinnedChecked>k__BackingField;

	[CompilerGenerated]
	private bool <IsDirectoriesChecked>k__BackingField;

	[CompilerGenerated]
	private bool <IsSearchChecked>k__BackingField;

	[CompilerGenerated]
	private IViewModelBase? <PaneContent>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? resetCommunityChannelsWidthCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? pinnedSelectedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? directoriesSelectedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? searchSelectedCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public RootApp.Client.CoreDomain.Models.Community.Community? Community
	{
		get
		{
			return <Community>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<RootApp.Client.CoreDomain.Models.Community.Community>.Default.Equals(<Community>k__BackingField, community))
			{
				<Community>k__BackingField = community;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Community);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MembersViewModel? Members
	{
		get
		{
			return <Members>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<MembersViewModel>.Default.Equals(<Members>k__BackingField, membersViewModel))
			{
				<Members>k__BackingField = membersViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Members);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public ChannelsViewModel? Channels
	{
		get
		{
			return <Channels>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<ChannelsViewModel>.Default.Equals(<Channels>k__BackingField, channelsViewModel))
			{
				<Channels>k__BackingField = channelsViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Channels);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IViewModelBase? SelectedChannelContent
	{
		get
		{
			return <SelectedChannelContent>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<IViewModelBase>.Default.Equals(<SelectedChannelContent>k__BackingField, viewModelBase))
			{
				<SelectedChannelContent>k__BackingField = viewModelBase;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SelectedChannelContent);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public GridLength CommunityChannelsWidth
	{
		get
		{
			return <CommunityChannelsWidth>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<GridLength>.Default.Equals(<CommunityChannelsWidth>k__BackingField, gridLength))
			{
				<CommunityChannelsWidth>k__BackingField = gridLength;
				OnCommunityChannelsWidthChanged(gridLength);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CommunityChannelsWidth);
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
			return <PaneOpen>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<PaneOpen>k__BackingField, flag))
			{
				<PaneOpen>k__BackingField = flag;
				OnPaneOpenChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PaneOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsPinnedChecked
	{
		get
		{
			return <IsPinnedChecked>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsPinnedChecked>k__BackingField, flag))
			{
				<IsPinnedChecked>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsPinnedChecked);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsDirectoriesChecked
	{
		get
		{
			return <IsDirectoriesChecked>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsDirectoriesChecked>k__BackingField, flag))
			{
				<IsDirectoriesChecked>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsDirectoriesChecked);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsSearchChecked
	{
		get
		{
			return <IsSearchChecked>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsSearchChecked>k__BackingField, flag))
			{
				<IsSearchChecked>k__BackingField = flag;
				OnIsSearchCheckedChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsSearchChecked);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IViewModelBase? PaneContent
	{
		get
		{
			return <PaneContent>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<IViewModelBase>.Default.Equals(<PaneContent>k__BackingField, viewModelBase))
			{
				<PaneContent>k__BackingField = viewModelBase;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PaneContent);
			}
		}
	}

	public PaneDisplayService PaneDisplayService { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ResetCommunityChannelsWidthCommand => resetCommunityChannelsWidthCommand ?? (resetCommunityChannelsWidthCommand = new RelayCommand(ResetCommunityChannelsWidth));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PinnedSelectedCommand => pinnedSelectedCommand ?? (pinnedSelectedCommand = new RelayCommand(PinnedSelected));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand DirectoriesSelectedCommand => directoriesSelectedCommand ?? (directoriesSelectedCommand = new RelayCommand(DirectoriesSelected));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand SearchSelectedCommand => searchSelectedCommand ?? (searchSelectedCommand = new RelayCommand(SearchSelected));

	public CommunityViewModel(CommunityGuid P_0, RootGuid? P_1, MessageGuid? P_2, MembersViewModelFactory P_3, ChannelsViewModelFactory P_4, IRootSessionAccessor P_5, TextChannelContentViewModelFactory P_6, VoiceChannelContentViewModelFactory P_7, AppChannelContentViewModelFactory P_8, PinnedMessagesViewModelFactory P_9, DirectoriesViewModelFactory P_10, SearchViewModelFactory P_11, ILocalDataStore P_12, PaneDisplayService P_13, ICommunityHubConnectionService P_14, BrowserService P_15)
	{
		PaneDisplayService = P_13;
		_communityId = P_0;
		_startupSubContainerId = P_1;
		_startupMessageId = P_2;
		_channelsViewModelFactory = P_4;
		_memberViewModelFactory = P_3;
		_pinnedMessagesViewModelFactory = P_9;
		_directoriesViewModelFactory = P_10;
		_searchViewModelFactory = P_11;
		_localDataStore = P_12;
		_communityHubConnectionService = P_14;
		_browserService = P_15;
		_rootSessionAccessor = P_5;
		_textChannelContentViewModelFactory = P_6;
		_voiceChannelContentViewModelFactory = P_7;
		_appChannelContentViewModelFactory = P_8;
		_rootSessionAccessor.Session.ActiveMediaRoomService.MediaRoomClosed += onActiveMediaRoomServiceMediaRoomClosed;
		WeakReferenceMessenger.Default.Register<OpenSearchPaneMessage>(this, onSearchPaneMessageReceived);
		WeakReferenceMessenger.Default.Register<SelectChannelMessage>(this, onSelectChannelMessageReceived);
		WeakReferenceMessenger.Default.Register<ToggleMembersListMessage>(this, onToggleMembersListMessageReceived);
		WeakReferenceMessenger.Default.Register<BrowserDisposingMessage>(this, onBrowserDisposingMessageReceived);
		ILocalDataStore localDataStore = _localDataStore;
		global::<>y__InlineArray3<string> <>y__InlineArray = default(global::<>y__InlineArray3<string>);
		global::<PrivateImplementationDetails>.InlineArrayElementRef<global::<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 0) = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id;
		global::<PrivateImplementationDetails>.InlineArrayElementRef<global::<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 1) = P_0;
		global::<PrivateImplementationDetails>.InlineArrayElementRef<global::<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 2) = "CommunityChannelsWidth";
		if (localDataStore.TryGetWithPath(global::<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<global::<>y__InlineArray3<string>, string>(in <>y__InlineArray, 3), out double num))
		{
			CommunityChannelsWidth = new GridLength(num);
		}
		loadCommunityAsync().Forget();
	}

	private async Task loadCommunityAsync()
	{
		try
		{
			Community = await _rootSessionAccessor.Session.CommunityService.GetCommunityAsync(_communityId, true);
			if (Community != null)
			{
				Community.PropertyChanged += onCommunityPropertyChanged;
				if (Community.Channels != null)
				{
					Community.Channels.ChannelDeleted += onChannelsChannelDeleted;
				}
				_communityHubConnectionService.OnCommunityLoaded(Community);
				if (_startupSubContainerId != null)
				{
					await focusChannelAsync((ChannelGuid)_startupSubContainerId.Value, _startupMessageId);
				}
				else
				{
					renderSelectedChannelContent();
				}
				Members = _memberViewModelFactory.Create(Community);
				Channels = _channelsViewModelFactory.Create(Community);
			}
		}
		catch
		{
			_rootSessionAccessor.Session.TabService.RemoveTab(_communityId);
		}
	}

	private void renderSelectedChannelContent()
	{
		if (Community?.SelectedChannel == null)
		{
			return;
		}
		if (!_channelContentViewModels.TryGetValue(Community.SelectedChannel.Id, out IViewModelBase value))
		{
			if (Community.SelectedChannel.Type != ChannelType.Text)
			{
				value = ((Community.SelectedChannel.Type != ChannelType.Voice) ? ((CachedViewModelBase)_appChannelContentViewModelFactory.Create(Community.SelectedChannel)) : ((CachedViewModelBase)_voiceChannelContentViewModelFactory.Create(Community.SelectedChannel)));
			}
			else
			{
				value = _textChannelContentViewModelFactory.Create(Community.SelectedChannel, this, _startupMessageId);
				_startupMessageId = null;
			}
			_channelContentViewModels.Add(Community.SelectedChannel.Id, value);
		}
		SelectedChannelContent = value;
		renderSelectedChannelPaneContent();
	}

	private void renderSelectedChannelPaneContent()
	{
		if (Community?.SelectedChannel == null)
		{
			return;
		}
		if (Community.SelectedChannel.Type != ChannelType.Text)
		{
			CloseAllPanes();
		}
		else if (PaneOpen && IsPinnedChecked)
		{
			if (!_channelPinnedMessagesViewModels.TryGetValue(Community.SelectedChannel.Id, out PinnedMessagesViewModel value))
			{
				value = _pinnedMessagesViewModelFactory.Create(Community.SelectedChannel, CloseAllPanes);
				_channelPinnedMessagesViewModels.Add(Community.SelectedChannel.Id, value);
			}
			PaneContent = value;
		}
		else if (PaneOpen && IsDirectoriesChecked)
		{
			if (!Community.SelectedChannel.LocalChannelPermission.ChannelViewFile)
			{
				CloseAllPanes();
				return;
			}
			if (!_channelDirectoriesViewModels.TryGetValue(Community.SelectedChannel.Id, out DirectoriesViewModel value2))
			{
				value2 = _directoriesViewModelFactory.Create(Community.SelectedChannel, CloseAllPanes);
				_channelDirectoriesViewModels.Add(Community.SelectedChannel.Id, value2);
			}
			PaneContent = value2;
		}
		else if (PaneOpen && IsSearchChecked)
		{
			if (_searchViewModel == null)
			{
				_searchViewModel = _searchViewModelFactory.Create(Community, CloseAllPanes);
				_searchViewModel.SetChannelFilter(Community?.SelectedChannel?.Id);
			}
			PaneContent = _searchViewModel;
			if (_searchViewModel.HasSingleChannel)
			{
				_searchViewModel.SetChannelFilter(Community?.SelectedChannel?.Id);
			}
		}
	}

	private void onCommunityPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "SelectedChannel")
			{
				renderSelectedChannelContent();
			}
		});
	}

	[RelayCommand]
	public void ResetCommunityChannelsWidth()
	{
		CommunityChannelsWidth = new GridLength(280.0);
	}

	private void onActiveMediaRoomServiceMediaRoomClosed(MessageContainerGuid containerId)
	{
		if ((RootGuidType)containerId == RootGuidType.Channel && RootGuid.TryParse<ChannelGuid>(containerId, out var channelGuid))
		{
			removeChannelContent(channelGuid);
		}
	}

	private void onChannelsChannelDeleted(ChannelGuid channelId)
	{
		removeChannelContent(channelId);
	}

	private void onBrowserDisposingMessageReceived(object recipient, BrowserDisposingMessage message)
	{
		try
		{
			if (RootGuid.TryParse<ChannelGuid>(message.BrowserId, out var channelGuid))
			{
				removeChannelContent(channelGuid);
			}
		}
		catch
		{
		}
	}

	private void removeChannelContent(ChannelGuid P_0)
	{
		bool flag = Community?.SelectedChannel?.Id == P_0;
		if (_channelContentViewModels.Remove(P_0, out IViewModelBase viewModelBase))
		{
			viewModelBase.Dispose();
		}
		if (_channelPinnedMessagesViewModels.Remove(P_0, out PinnedMessagesViewModel pinnedMessagesViewModel))
		{
			pinnedMessagesViewModel.Dispose();
		}
		if (_channelDirectoriesViewModels.Remove(P_0, out DirectoriesViewModel directoriesViewModel))
		{
			directoriesViewModel.Dispose();
		}
		if (flag)
		{
			SelectedChannelContent = null;
			renderSelectedChannelContent();
		}
	}

	public void ReportCommunityPaneWidth(double P_0)
	{
		PaneDisplayService.ReportCommunityPaneWidth(P_0, PaneOpen);
	}

	[RelayCommand]
	public void PinnedSelected()
	{
		if (!IsDirectoriesChecked && !IsSearchChecked)
		{
			PaneOpen = !PaneOpen;
		}
		IsPinnedChecked = PaneOpen;
		IsDirectoriesChecked = false;
		IsSearchChecked = false;
		if (PaneOpen)
		{
			renderSelectedChannelPaneContent();
		}
	}

	[RelayCommand]
	public void DirectoriesSelected()
	{
		if (!IsPinnedChecked && !IsSearchChecked)
		{
			PaneOpen = !PaneOpen;
		}
		IsDirectoriesChecked = PaneOpen;
		IsPinnedChecked = false;
		IsSearchChecked = false;
		if (PaneOpen)
		{
			renderSelectedChannelPaneContent();
		}
	}

	[RelayCommand]
	public void SearchSelected()
	{
		if (!IsPinnedChecked && !IsDirectoriesChecked)
		{
			PaneOpen = !PaneOpen;
		}
		IsSearchChecked = PaneOpen;
		IsPinnedChecked = false;
		IsDirectoriesChecked = false;
		if (PaneOpen)
		{
			_searchViewModel?.SetChannelFilter(Community?.SelectedChannel?.Id);
			renderSelectedChannelPaneContent();
		}
	}

	public void CloseAllPanes()
	{
		PaneOpen = false;
		IsPinnedChecked = false;
		IsDirectoriesChecked = false;
		IsSearchChecked = false;
	}

	private void onSearchPaneMessageReceived(object recipient, OpenSearchPaneMessage message)
	{
		if (!(message.CommunityId != Community?.Id))
		{
			if (!IsSearchChecked)
			{
				SearchSelected();
			}
			_searchViewModel?.SetChannelFilter(message.ChannelId);
		}
	}

	private void onSelectChannelMessageReceived(object recipient, SelectChannelMessage message)
	{
		if (!(message.CommunityId != Community?.Id))
		{
			if (!_channelContentViewModels.ContainsKey(message.ChannelId))
			{
				_startupMessageId = message.MessageId;
			}
			focusChannelAsync(message.ChannelId, message.MessageId).Forget();
		}
	}

	private void onToggleMembersListMessageReceived(object recipient, ToggleMembersListMessage message)
	{
		Members?.ToggleMenu();
	}

	private async Task focusChannelAsync(ChannelGuid P_0, MessageGuid? P_1)
	{
		Channel channel = Community?.Channels?.GetChannel(P_0);
		if (channel != null)
		{
			if (P_1 != null)
			{
				IViewModelBase selectedChannelContent = _channelContentViewModels.GetValueOrDefault(channel.Id);
				TextChannelContentViewModel textChannelContentViewModel = selectedChannelContent as TextChannelContentViewModel;
				if (textChannelContentViewModel != null)
				{
					textChannelContentViewModel.StartupMessageId = P_1.Value;
				}
				await channel.Messages.FocusMessageAsync(P_1.Value);
				if (_startupMessageId == null)
				{
					textChannelContentViewModel?.FocusMessage(P_1.Value);
				}
			}
			if (Community?.SelectedChannel != channel)
			{
				Community?.SelectChannel(channel);
			}
			else if (SelectedChannelContent == null)
			{
				renderSelectedChannelContent();
			}
		}
		else
		{
			_startupMessageId = null;
			Community?.SelectFirstTextChannel();
			renderSelectedChannelContent();
		}
	}

	public override void Dispose()
	{
		base.Dispose();
		_browserService.RemoveAllAppBrowsersForCommunity(_communityId);
		_communityHubConnectionService.OnCommunityUnloadingAsync(_communityId);
		Members?.Dispose();
		Channels?.Dispose();
		foreach (KeyValuePair<ChannelGuid, PinnedMessagesViewModel> channelPinnedMessagesViewModel in _channelPinnedMessagesViewModels)
		{
			channelPinnedMessagesViewModel.Value.Dispose();
		}
		foreach (KeyValuePair<ChannelGuid, DirectoriesViewModel> channelDirectoriesViewModel in _channelDirectoriesViewModels)
		{
			channelDirectoriesViewModel.Value.Dispose();
		}
		_searchViewModel?.Dispose();
		foreach (KeyValuePair<ChannelGuid, IViewModelBase> channelContentViewModel in _channelContentViewModels)
		{
			channelContentViewModel.Value.Dispose();
		}
		_rootSessionAccessor.Session.ActiveMediaRoomService.MediaRoomClosed -= onActiveMediaRoomServiceMediaRoomClosed;
		if (Community != null)
		{
			Community.PropertyChanged -= onCommunityPropertyChanged;
			if (Community.Channels != null)
			{
				Community.Channels.ChannelDeleted -= onChannelsChannelDeleted;
			}
		}
		WeakReferenceMessenger.Default.Unregister<OpenSearchPaneMessage>(this);
		WeakReferenceMessenger.Default.Unregister<SelectChannelMessage>(this);
		WeakReferenceMessenger.Default.Unregister<ToggleMembersListMessage>(this);
		WeakReferenceMessenger.Default.Unregister<BrowserDisposingMessage>(this);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnCommunityChannelsWidthChanged(GridLength P_0)
	{
		ILocalDataStore localDataStore = _localDataStore;
		global::<>y__InlineArray3<string> <>y__InlineArray = default(global::<>y__InlineArray3<string>);
		global::<PrivateImplementationDetails>.InlineArrayElementRef<global::<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 0) = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id;
		global::<PrivateImplementationDetails>.InlineArrayElementRef<global::<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 1) = _communityId;
		global::<PrivateImplementationDetails>.InlineArrayElementRef<global::<>y__InlineArray3<string>, string>(ref <>y__InlineArray, 2) = "CommunityChannelsWidth";
		localDataStore.SetWithPath(global::<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<global::<>y__InlineArray3<string>, string>(in <>y__InlineArray, 3), P_0.Value);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnPaneOpenChanged(bool P_0)
	{
		if (!PaneOpen)
		{
			CloseAllPanes();
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnIsSearchCheckedChanged(bool P_0)
	{
		_searchViewModel?.ClearSearchTerm();
	}
}
