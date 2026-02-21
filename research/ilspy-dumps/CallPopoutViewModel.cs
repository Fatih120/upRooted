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
using FluentValidation;
using RootApp.Browser;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Controls.TitleBars;
using RootApp.Client.Avalonia.Helpers.Zoom;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Home.VoiceBar;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.DirectMessages;
using RootApp.Client.CoreDomain.Models.Media;

namespace RootApp.Client.Avalonia.UI.Call;

public class CallPopoutViewModel : ViewModelBase<CallPopoutViewModel>
{
	private readonly MediaRoom _mediaRoom;

	private readonly MemberProfileViewModelFactory _memberProfileViewModelFactory;

	private readonly UserContextMenuViewModelFactory _userContextMenuViewModelFactory;

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

	public IViewModelBase? TitleBarViewModel { get; }

	public IRootSessionAccessor RootSessionAccessor { get; }

	public ZoomService ZoomService { get; }

	public VoiceBarViewModel VoiceBarViewModel { get; }

	public CallPopoutViewModel(MediaRoom P_0, TitleBarViewModelFactory P_1, MemberProfileViewModelFactory P_2, UserContextMenuViewModelFactory P_3, BrowserService P_4, VoiceBarViewModelFactory P_5, IRootSessionAccessor P_6, ZoomService P_7)
		: base((IValidator<CallPopoutViewModel>?)null)
	{
		_mediaRoom = P_0;
		_memberProfileViewModelFactory = P_2;
		_userContextMenuViewModelFactory = P_3;
		TitleBarViewModel = P_1.Create(_mediaRoom.MessageContainer.Name);
		RootSessionAccessor = P_6;
		ZoomService = P_7;
		VoiceBarViewModel = P_5.Create(null);
		_mediaRoom.SetIsPoppedOut(true);
		RootSessionAccessor.Session.ActiveMediaRoomService.SimulateMediaRoomClosed(_mediaRoom.MessageContainer.ContainerId);
		Browser = P_4.GetBrowser(_mediaRoom.MessageContainer.ContainerId);
		configureBrowserClickHandler();
		WeakReferenceMessenger.Default.Register<ShowProfileFlyoutAtPositionByUserAndContainerMessage>(this, onShowProfileFlyoutAtMousePositionByUserAndContainerMessageReceived);
		WeakReferenceMessenger.Default.Register<ShowUserContextMenuAtPositionMessage>(this, onShowUserContextMenuAtPositionMessageReceived);
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
			MediaMember memberByUserId = _mediaRoom.GetMemberByUserId(message.UserId);
			if (memberByUserId != null)
			{
				MemberProfile = _memberProfileViewModelFactory.Create(memberByUserId, delegate
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
			MediaMember memberByUserId = _mediaRoom.GetMemberByUserId(message.UserId);
			if (memberByUserId != null)
			{
				if (_mediaRoom.MessageContainer is DirectMessage directMessage)
				{
					UserContextMenu = _userContextMenuViewModelFactory.Create(memberByUserId.GlobalUser, directMessage);
				}
				else
				{
					UserContextMenu = _userContextMenuViewModelFactory.Create(memberByUserId.GlobalUser);
				}
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
		MemberProfile?.Dispose();
		UserContextMenu?.Dispose();
		VoiceBarViewModel.Dispose();
		_mediaRoom.SetIsPoppedOut(false);
	}
}
