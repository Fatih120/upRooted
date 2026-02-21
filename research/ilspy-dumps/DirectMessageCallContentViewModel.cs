using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Messaging;
using DotNetBrowser.Handlers;
using DotNetBrowser.Input;
using DotNetBrowser.Input.Mouse.Events;
using RootApp.Browser;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Windows;
using RootApp.Client.Avalonia.Helpers.Zoom;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.DirectMessages;
using RootApp.Client.CoreDomain.Models.Messages;

namespace RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages;

public class DirectMessageCallContentViewModel : CachedViewModelBase
{
	private readonly MemberProfileViewModelFactory _memberProfileViewModelFactory;

	private readonly UserContextMenuViewModelFactory _userContextMenuViewModelFactory;

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

	public DirectMessage DirectMessage { get; }

	public ZoomService ZoomService { get; }

	public IRootBrowser? Browser { get; }

	public DirectMessageCallContentViewModel(DirectMessage P_0, BrowserService P_1, MemberProfileViewModelFactory P_2, UserContextMenuViewModelFactory P_3, ZoomService P_4, OverlayStackService P_5)
	{
		DirectMessage = P_0;
		ZoomService = P_4;
		_memberProfileViewModelFactory = P_2;
		_userContextMenuViewModelFactory = P_3;
		_overlayStackService = P_5;
		Browser = P_1.GetBrowser(DirectMessage.Id);
		configureBrowserClickHandler();
		WeakReferenceMessenger.Default.Register<ShowProfileFlyoutAtPositionByUserAndContainerMessage>(this, onShowProfileFlyoutAtMousePositionByUserAndContainerMessageReceived);
		WeakReferenceMessenger.Default.Register<ShowUserContextMenuAtPositionMessage>(this, onShowUserContextMenuAtPositionMessageReceived);
		SubscribeToOverlayTracker();
	}

	private void SubscribeToOverlayTracker()
	{
		_overlayTracker = _overlayStackService.GetTrackerForContainer(DirectMessage.Id);
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

	private async void onShowProfileFlyoutAtMousePositionByUserAndContainerMessageReceived(object recipient, ShowProfileFlyoutAtPositionByUserAndContainerMessage message)
	{
		try
		{
			MemberProfile?.Dispose();
			IMessageContainerMember member = await DirectMessage.GetMemberAsync(message.UserId);
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

	private async void onShowUserContextMenuAtPositionMessageReceived(object recipient, ShowUserContextMenuAtPositionMessage message)
	{
		try
		{
			UserContextMenu?.Dispose();
			IMessageContainerMember member = await DirectMessage.GetMemberAsync(message.UserId);
			if (member != null)
			{
				UserContextMenu = _userContextMenuViewModelFactory.Create(member.GlobalUser, DirectMessage);
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

	public override void Dispose()
	{
		base.Dispose();
		if (_overlayTracker != null)
		{
			_overlayTracker.OverlayCountChanged -= OnOverlayCountChanged;
			_overlayTracker = null;
		}
		MemberProfile?.Dispose();
		UserContextMenu?.Dispose();
	}
}
