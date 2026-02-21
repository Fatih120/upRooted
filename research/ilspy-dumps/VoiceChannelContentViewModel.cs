using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DotNetBrowser.Handlers;
using DotNetBrowser.Input;
using DotNetBrowser.Input.Mouse.Events;
using RootApp.Browser;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Helpers.Windows;
using RootApp.Client.Avalonia.Helpers.Zoom;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community.Channels;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Client.Avalonia.UI.Community.Content;

public class VoiceChannelContentViewModel : CachedViewModelBase
{
	private readonly MemberProfileViewModelFactory _memberProfileViewModelFactory;

	private readonly UserContextMenuViewModelFactory _userContextMenuViewModelFactory;

	private readonly BitmapCache _bitmapCache;

	private readonly CallPopoutService _callPopoutService;

	private readonly EditChannelViewModelFactory _editChannelViewModelFactory;

	private readonly DeleteChannelViewModelFactory _deleteChannelViewModelFactory;

	private readonly OverlayStackService _overlayStackService;

	private IOverlayStackTracker? _overlayTracker;

	[CompilerGenerated]
	private bool <ShouldRenderBrowser>k__BackingField = true;

	[CompilerGenerated]
	private double <PopupTargetX>k__BackingField;

	[CompilerGenerated]
	private double <PopupTargetY>k__BackingField;

	[CompilerGenerated]
	private double <ContextMenuTargetX>k__BackingField;

	[CompilerGenerated]
	private double <ContextMenuTargetY>k__BackingField;

	[CompilerGenerated]
	private bool <IsPopupOpen>k__BackingField;

	[CompilerGenerated]
	private bool <IsContextMenuOpen>k__BackingField;

	[CompilerGenerated]
	private MemberProfileViewModel? <MemberProfile>k__BackingField;

	[CompilerGenerated]
	private UserContextMenuViewModel? <UserContextMenu>k__BackingField;

	[CompilerGenerated]
	private Channel <Channel>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? popoutCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showEditChannelViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showDeleteChannelViewModelCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsPopupOpen
	{
		get
		{
			return <IsPopupOpen>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsPopupOpen>k__BackingField, flag))
			{
				<IsPopupOpen>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsPopupOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsContextMenuOpen
	{
		get
		{
			return <IsContextMenuOpen>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsContextMenuOpen>k__BackingField, flag))
			{
				<IsContextMenuOpen>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsContextMenuOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MemberProfileViewModel? MemberProfile
	{
		get
		{
			return <MemberProfile>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<MemberProfileViewModel>.Default.Equals(<MemberProfile>k__BackingField, memberProfileViewModel))
			{
				<MemberProfile>k__BackingField = memberProfileViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MemberProfile);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public UserContextMenuViewModel? UserContextMenu
	{
		get
		{
			return <UserContextMenu>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<UserContextMenuViewModel>.Default.Equals(<UserContextMenu>k__BackingField, userContextMenuViewModel))
			{
				<UserContextMenu>k__BackingField = userContextMenuViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.UserContextMenu);
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
	public bool ShouldRenderBrowser
	{
		get
		{
			return <ShouldRenderBrowser>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShouldRenderBrowser>k__BackingField, flag))
			{
				<ShouldRenderBrowser>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShouldRenderBrowser);
			}
		}
	}

	public double PopupTargetX
	{
		[CompilerGenerated]
		get
		{
			return <PopupTargetX>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<PopupTargetX>k__BackingField = num;
		}
	}

	public double PopupTargetY
	{
		[CompilerGenerated]
		get
		{
			return <PopupTargetY>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<PopupTargetY>k__BackingField = num;
		}
	}

	public double ContextMenuTargetX
	{
		[CompilerGenerated]
		get
		{
			return <ContextMenuTargetX>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<ContextMenuTargetX>k__BackingField = num;
		}
	}

	public double ContextMenuTargetY
	{
		[CompilerGenerated]
		get
		{
			return <ContextMenuTargetY>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<ContextMenuTargetY>k__BackingField = num;
		}
	}

	public IRootBrowser? Browser { get; }

	public ZoomService ZoomService { get; }

	public Task<BitmapWrapper?> ChannelIconAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(Channel.IconAssetUri, null, 120);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PopoutCommand => popoutCommand ?? (popoutCommand = new RelayCommand(Popout));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowEditChannelViewModelCommand => showEditChannelViewModelCommand ?? (showEditChannelViewModelCommand = new RelayCommand(ShowEditChannelViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowDeleteChannelViewModelCommand => showDeleteChannelViewModelCommand ?? (showDeleteChannelViewModelCommand = new RelayCommand(ShowDeleteChannelViewModel));

	public VoiceChannelContentViewModel(Channel P_0, BrowserService P_1, MemberProfileViewModelFactory P_2, UserContextMenuViewModelFactory P_3, BitmapCache P_4, CallPopoutService P_5, EditChannelViewModelFactory P_6, DeleteChannelViewModelFactory P_7, ZoomService P_8, OverlayStackService P_9)
	{
		_memberProfileViewModelFactory = P_2;
		_userContextMenuViewModelFactory = P_3;
		_bitmapCache = P_4;
		_callPopoutService = P_5;
		_editChannelViewModelFactory = P_6;
		_deleteChannelViewModelFactory = P_7;
		_overlayStackService = P_9;
		Channel = P_0;
		ZoomService = P_8;
		Channel.PropertyChanged += onChannelPropertyChanged;
		Browser = P_1.GetBrowser(P_0.Id);
		configureBrowserClickHandler();
		WeakReferenceMessenger.Default.Register<ShowProfileFlyoutAtPositionByUserAndContainerMessage>(this, onShowProfileFlyoutAtMousePositionByUserAndContainerMessageReceived);
		WeakReferenceMessenger.Default.Register<ShowUserContextMenuAtPositionMessage>(this, onShowUserContextMenuAtPositionMessageReceived);
		SubscribeToOverlayTracker();
	}

	private void SubscribeToOverlayTracker()
	{
		_overlayTracker = _overlayStackService.GetTrackerForContainer(Channel.Community.Id);
		if (_overlayTracker != null)
		{
			_overlayTracker.OverlayCountChanged += OnOverlayCountChanged;
			ShouldRenderBrowser = _overlayTracker.OverlayCount == 0;
		}
	}

	private void OnOverlayCountChanged(int overlayCount)
	{
		Dispatcher.UIThread.Post(delegate
		{
			ShouldRenderBrowser = overlayCount == 0;
		});
	}

	private void onChannelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "IconAssetUri")
		{
			OnPropertyChanged("ChannelIconAsyncBitmapWrapper");
		}
	}

	private void configureBrowserClickHandler()
	{
		if (Browser == null)
		{
			return;
		}
		Browser.DotNetBrowser.Mouse.Pressed.Handler = new Handler<IMousePressedEventArgs, InputEventResponse>(delegate(IMousePressedEventArgs parameters)
		{
			bool isPopupOpen = IsPopupOpen;
			bool isContextMenuOpen = IsContextMenuOpen;
			Dispatcher.UIThread.Post(delegate
			{
				MouseButton button = parameters.Button;
				if ((button == MouseButton.Left || button == MouseButton.Right) ? true : false)
				{
					if (IsPopupOpen)
					{
						IsPopupOpen = false;
					}
					if (IsContextMenuOpen)
					{
						IsContextMenuOpen = false;
					}
				}
			});
			return (!(isPopupOpen || isContextMenuOpen)) ? InputEventResponse.Proceed : InputEventResponse.Suppress;
		});
	}

	private void onShowProfileFlyoutAtMousePositionByUserAndContainerMessageReceived(object recipient, ShowProfileFlyoutAtPositionByUserAndContainerMessage message)
	{
		try
		{
			MemberProfile?.Dispose();
			Member member = Channel.Community.Members?.GetMemberFromCache(message.UserId);
			if (member != null)
			{
				MemberProfile = _memberProfileViewModelFactory.Create(member, delegate
				{
					IsPopupOpen = false;
				});
				PopupTargetX = message.X;
				PopupTargetY = message.Y;
				IsPopupOpen = true;
			}
		}
		catch
		{
		}
	}

	private void onShowUserContextMenuAtPositionMessageReceived(object recipient, ShowUserContextMenuAtPositionMessage message)
	{
		try
		{
			UserContextMenu?.Dispose();
			Member member = Channel.Community.Members?.GetMemberFromCache(message.UserId);
			if (member != null)
			{
				UserContextMenu = _userContextMenuViewModelFactory.Create(member);
				if (message.IsStream)
				{
					UserContextMenu.IsStreamContext = true;
				}
				ContextMenuTargetX = message.X;
				ContextMenuTargetY = message.Y;
				IsContextMenuOpen = true;
			}
		}
		catch
		{
		}
	}

	[RelayCommand]
	public void Popout()
	{
		_callPopoutService.PopoutCall(Channel.MediaRoom);
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

	public override void Dispose()
	{
		base.Dispose();
		if (_overlayTracker != null)
		{
			_overlayTracker.OverlayCountChanged -= OnOverlayCountChanged;
			_overlayTracker = null;
		}
		Channel.PropertyChanged -= onChannelPropertyChanged;
		MemberProfile?.Dispose();
		UserContextMenu?.Dispose();
	}
}
