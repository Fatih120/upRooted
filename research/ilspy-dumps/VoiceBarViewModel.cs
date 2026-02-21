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
using FluentValidation;
using RootApp.Browser;
using RootApp.Browser.WebRtc.Services;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Helpers.Navigation;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Home.VoiceBar;

public class VoiceBarViewModel : ViewModelBase<VoiceBarViewModel>
{
	private readonly BrowserService _browserService;

	private readonly BitmapCache _bitmapCache;

	private readonly ProfileSettingsViewModelFactory _profileSettingsViewModelFactory;

	private readonly ScreensharePickerViewModelFactory _screensharePickerViewModelFactory;

	private readonly CommunityOpenerService _communityOpenerService;

	private readonly DirectMessageOpenerService _directMessageOpenerService;

	private readonly CallPopoutService? _callPopoutService;

	private bool _hasPickedScreen;

	[CompilerGenerated]
	private bool <IsInPopoutWindow>k__BackingField;

	[CompilerGenerated]
	private bool <ScreenShareIsEnabled>k__BackingField = true;

	[CompilerGenerated]
	private bool <VideoIsEnabled>k__BackingField = true;

	[CompilerGenerated]
	private ScreensharePickerViewModel? <ScreensharePicker>k__BackingField;

	[CompilerGenerated]
	private bool <ScreensharePickerIsOpen>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? popoutCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? disconnectCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleWebcamCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleMuteCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleDeafenCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? openSettingsCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? screenshareOpeningCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<bool>? screenshareClosingCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? focusChannelCommand;

	public IUserInfoService UserInfoService { get; }

	public IRootSessionAccessor RootSessionAccessor { get; }

	private bool IsInPopoutWindow
	{
		[CompilerGenerated]
		set
		{
			<IsInPopoutWindow>k__BackingField = flag;
		}
	}

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(UserInfoService.SessionUser.ProfilePictureUri, null, 120);

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public ScreensharePickerViewModel? ScreensharePicker
	{
		get
		{
			return <ScreensharePicker>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<ScreensharePickerViewModel>.Default.Equals(<ScreensharePicker>k__BackingField, screensharePickerViewModel))
			{
				<ScreensharePicker>k__BackingField = screensharePickerViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ScreensharePicker);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ScreensharePickerIsOpen
	{
		get
		{
			return <ScreensharePickerIsOpen>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ScreensharePickerIsOpen>k__BackingField, flag))
			{
				<ScreensharePickerIsOpen>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ScreensharePickerIsOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ScreenShareIsEnabled
	{
		get
		{
			return <ScreenShareIsEnabled>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ScreenShareIsEnabled>k__BackingField, flag))
			{
				<ScreenShareIsEnabled>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ScreenShareIsEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool VideoIsEnabled
	{
		get
		{
			return <VideoIsEnabled>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(<VideoIsEnabled>k__BackingField, flag))
			{
				<VideoIsEnabled>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.VideoIsEnabled);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PopoutCommand => popoutCommand ?? (popoutCommand = new RelayCommand(Popout));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand DisconnectCommand => disconnectCommand ?? (disconnectCommand = new RelayCommand(Disconnect));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleWebcamCommand => toggleWebcamCommand ?? (toggleWebcamCommand = new RelayCommand(ToggleWebcam));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleMuteCommand => toggleMuteCommand ?? (toggleMuteCommand = new RelayCommand(ToggleMute));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleDeafenCommand => toggleDeafenCommand ?? (toggleDeafenCommand = new RelayCommand(ToggleDeafen));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand OpenSettingsCommand => openSettingsCommand ?? (openSettingsCommand = new RelayCommand(OpenSettings));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ScreenshareOpeningCommand => screenshareOpeningCommand ?? (screenshareOpeningCommand = new RelayCommand(ScreenshareOpening));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<bool> ScreenshareClosingCommand => screenshareClosingCommand ?? (screenshareClosingCommand = new RelayCommand<bool>(ScreenshareClosing));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand FocusChannelCommand => focusChannelCommand ?? (focusChannelCommand = new RelayCommand(FocusChannel));

	public VoiceBarViewModel(IRootSessionAccessor P_0, BrowserService P_1, BitmapCache P_2, ProfileSettingsViewModelFactory P_3, ScreensharePickerViewModelFactory P_4, CommunityOpenerService P_5, DirectMessageOpenerService P_6, CallPopoutService? P_7)
		: base((IValidator<VoiceBarViewModel>?)null)
	{
		RootSessionAccessor = P_0;
		_browserService = P_1;
		_bitmapCache = P_2;
		_profileSettingsViewModelFactory = P_3;
		_screensharePickerViewModelFactory = P_4;
		_communityOpenerService = P_5;
		_directMessageOpenerService = P_6;
		_callPopoutService = P_7;
		IsInPopoutWindow = _callPopoutService != null;
		UserInfoService = RootSessionAccessor.Session.UserInfoService;
		UserInfoService.SessionUser.PropertyChanged += onSessionUserPropertyChanged;
		WeakReferenceMessenger.Default.Register<ScreenshareEnabledMessage>(this, onScreenshareEnabledMessageReceived);
		WeakReferenceMessenger.Default.Register<VideoEnabledMessage>(this, onVideoEnabledMessageReceived);
		WeakReferenceMessenger.Default.Register<ResetVoiceBarMessage>(this, onResetVoiceBarMessageReceived);
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
	public void Popout()
	{
		MediaRoom activeMediaRoom = RootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom;
		if (activeMediaRoom != null)
		{
			_callPopoutService?.PopoutCall(activeMediaRoom);
		}
	}

	[RelayCommand]
	public void Disconnect()
	{
		MessageContainerGuid? messageContainerGuid = RootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom?.MessageContainer.ContainerId;
		if (messageContainerGuid != null)
		{
			_browserService.RemoveBrowser(messageContainerGuid.Value);
		}
	}

	[RelayCommand]
	public void ToggleWebcam()
	{
		MediaRoom activeMediaRoom = RootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom;
		if (activeMediaRoom == null)
		{
			return;
		}
		MessageContainerGuid containerId = activeMediaRoom.MessageContainer.ContainerId;
		if (containerId != null)
		{
			WebRtcBrowser webRtcBrowser = _browserService.GetWebRtcBrowser(containerId);
			if (webRtcBrowser != null)
			{
				VideoIsEnabled = false;
				WebRtcService webRtc = webRtcBrowser.WebRtc;
				MediaMember? selfMediaMember = activeMediaRoom.SelfMediaMember;
				webRtc.SetIsVideoOn(selfMediaMember == null || !selfMediaMember.IsVideo);
			}
		}
	}

	[RelayCommand]
	public void ToggleMute()
	{
		MediaRoom activeMediaRoom = RootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom;
		if (activeMediaRoom == null)
		{
			return;
		}
		MessageContainerGuid containerId = activeMediaRoom.MessageContainer.ContainerId;
		if (containerId != null)
		{
			WebRtcBrowser webRtcBrowser = _browserService.GetWebRtcBrowser(containerId);
			if (webRtcBrowser != null)
			{
				WebRtcService webRtc = webRtcBrowser.WebRtc;
				MediaMember? selfMediaMember = activeMediaRoom.SelfMediaMember;
				webRtc.SetMute(selfMediaMember == null || !selfMediaMember.IsMuted);
			}
		}
	}

	[RelayCommand]
	public void ToggleDeafen()
	{
		MediaRoom activeMediaRoom = RootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom;
		if (activeMediaRoom == null)
		{
			return;
		}
		MessageContainerGuid containerId = activeMediaRoom.MessageContainer.ContainerId;
		if (containerId != null)
		{
			WebRtcBrowser webRtcBrowser = _browserService.GetWebRtcBrowser(containerId);
			if (webRtcBrowser != null)
			{
				WebRtcService webRtc = webRtcBrowser.WebRtc;
				MediaMember? selfMediaMember = activeMediaRoom.SelfMediaMember;
				webRtc.SetDeafen(selfMediaMember == null || !selfMediaMember.IsDeafened);
			}
		}
	}

	[RelayCommand]
	public void OpenSettings()
	{
		ProfileSettingsViewModel profileSettingsViewModel = _profileSettingsViewModelFactory.Create();
		profileSettingsViewModel.OpenAudioVideoPage();
		WeakReferenceMessenger.Default.Send(new PushViewModelToMainWindowStackMessage(profileSettingsViewModel, "ProfileSettingsViewModel"));
	}

	[RelayCommand]
	public void ScreenshareOpening()
	{
		MediaRoom activeMediaRoom = RootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom;
		if (activeMediaRoom == null)
		{
			return;
		}
		MessageContainerGuid containerId = activeMediaRoom.MessageContainer.ContainerId;
		if (!(containerId != null))
		{
			return;
		}
		WebRtcBrowser webRtcBrowser = _browserService.GetWebRtcBrowser(containerId);
		if (webRtcBrowser != null)
		{
			MediaMember? selfMediaMember = activeMediaRoom.SelfMediaMember;
			if (selfMediaMember != null && selfMediaMember.IsScreen)
			{
				ScreenShareIsEnabled = false;
				webRtcBrowser.WebRtc.SetScreenShare(false, false);
			}
			else
			{
				ScreensharePicker = _screensharePickerViewModelFactory.Create(webRtcBrowser, ScreenshareClosing);
				ScreensharePickerIsOpen = true;
			}
		}
	}

	[RelayCommand]
	public void ScreenshareClosing(bool pickedScreen)
	{
		if (pickedScreen)
		{
			_hasPickedScreen = true;
			ScreenShareIsEnabled = false;
		}
		else if (!_hasPickedScreen)
		{
			MediaRoom activeMediaRoom = RootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom;
			if (activeMediaRoom != null)
			{
				MessageContainerGuid containerId = activeMediaRoom.MessageContainer.ContainerId;
				if (containerId != null)
				{
					_browserService.GetWebRtcBrowser(containerId)?.WebRtc.ScreenPickerDismissed();
				}
			}
		}
		ScreensharePicker?.Dispose();
		ScreensharePicker = null;
		ScreensharePickerIsOpen = false;
		_hasPickedScreen = false;
	}

	[RelayCommand]
	public void FocusChannel()
	{
		if (RootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom == null)
		{
			return;
		}
		if (RootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom.IsPoppedOut)
		{
			_callPopoutService?.FocusPopoutWindow();
			return;
		}
		CommunityGuid? communityId = RootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom.MessageContainer.CommunityId;
		MessageContainerGuid containerId = RootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom.MessageContainer.ContainerId;
		if (communityId != null)
		{
			_communityOpenerService.OpenCommunity(communityId.Value, containerId);
		}
		else
		{
			_directMessageOpenerService.OpenDirectMessage(((DirectMessageGuid?)containerId).Value, null, true);
		}
	}

	private void onScreenshareEnabledMessageReceived(object recipient, ScreenshareEnabledMessage message)
	{
		ScreenShareIsEnabled = message.IsEnabled;
	}

	private void onVideoEnabledMessageReceived(object recipient, VideoEnabledMessage message)
	{
		VideoIsEnabled = message.IsEnabled;
	}

	private void onResetVoiceBarMessageReceived(object recipient, ResetVoiceBarMessage message)
	{
		ScreenShareIsEnabled = true;
		VideoIsEnabled = true;
	}

	public override void Dispose()
	{
		base.Dispose();
		UserInfoService.SessionUser.PropertyChanged -= onSessionUserPropertyChanged;
		ScreensharePicker?.Dispose();
	}
}
