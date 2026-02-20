// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.ProfileSettingsViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.Installation;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Settings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.User;

public class ProfileSettingsViewModel : ViewModelBase<ProfileSettingsViewModel>
{
	private readonly BitmapCache _bitmapCache;

	private readonly ClipboardService _clipboardService;

	private readonly RootInstallationManager _installationManager;

	private readonly EditProfileViewModelFactory _editProfileViewModelFactory;

	private readonly ChangeThemeViewModelFactory _changeThemeViewModelFactory;

	private readonly DiscardChangesViewModelFactory _discardChangesViewModelFactory;

	private readonly SignOutConfirmationViewModelFactory _signOutConfirmationViewModelFactory;

	private readonly AudioVideoViewModelFactory _audioVideoViewModelFactory;

	private readonly ChatViewModelFactory _chatViewModelFactory;

	private readonly NotificationSettingsViewModelFactory _notificationSettingsViewModelFactory;

	private readonly WindowsSettingsViewModelFactory _windowsSettingsViewModelFactory;

	private readonly GameOverlaySettingsViewModelFactory _gameOverlaySettingsViewModelFactory;

	private readonly BlockedUsersViewModelFactory _blockedUsersViewModelFactory;

	private readonly PrivacySettingsViewModelFactory _privacySettingsViewModelFactory;

	private readonly KeybindingsViewModelFactory _keybindingsViewModelFactory;

	private readonly StreamerModeSettingsViewModelFactory _streamerModeSettingsViewModelFactory;

	private readonly AdvancedSettingsViewModelFactory _advancedSettingsViewModelFactory;

	private MenuItemPageContainerViewModel? _editProfileContainer;

	private MenuItemPageContainerViewModel? _privacyContainer;

	private MenuItemPageContainerViewModel? _blockedUsersContainer;

	private MenuItemPageContainerViewModel? _audioAndVideoContainer;

	private MenuItemPageContainerViewModel? _chatContainer;

	private MenuItemPageContainerViewModel? _notificationSettingsContainer;

	private MenuItemPageContainerViewModel? _changeThemeContainer;

	private MenuItemPageContainerViewModel? _keybindingsContainer;

	private MenuItemPageContainerViewModel? _streamerModeContainer;

	private MenuItemPageContainerViewModel? _advancedContainer;

	private MenuItemPageContainerViewModel? _windowsContainer;

	private MenuItemPageContainerViewModel? _gameOverlayContainer;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showSignOutConfirmationViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? copySystemInfoCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? checkForUpdatesCommand;

	public SessionUser SessionUser { get; }

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(SessionUser.ProfilePictureUri);

	public static string AppVersion => ((App)Application.Current).AppVersion;

	public string SystemInfo => getSystemInfo();

	public ObservableCollection<MenuItemPageContainerViewModel> MenuItemPageContainers { get; } = new ObservableCollection<MenuItemPageContainerViewModel>();

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowSignOutConfirmationViewModelCommand => showSignOutConfirmationViewModelCommand ?? (showSignOutConfirmationViewModelCommand = new RelayCommand(ShowSignOutConfirmationViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CopySystemInfoCommand => copySystemInfoCommand ?? (copySystemInfoCommand = new RelayCommand(CopySystemInfo));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand CheckForUpdatesCommand => checkForUpdatesCommand ?? (checkForUpdatesCommand = new AsyncRelayCommand(CheckForUpdatesAsync));

	public ProfileSettingsViewModel(IRootSessionAccessor P_0, EditProfileViewModelFactory P_1, ChangeThemeViewModelFactory P_2, DiscardChangesViewModelFactory P_3, SignOutConfirmationViewModelFactory P_4, AudioVideoViewModelFactory P_5, ChatViewModelFactory P_6, NotificationSettingsViewModelFactory P_7, WindowsSettingsViewModelFactory P_8, GameOverlaySettingsViewModelFactory P_9, BlockedUsersViewModelFactory P_10, PrivacySettingsViewModelFactory P_11, KeybindingsViewModelFactory P_12, StreamerModeSettingsViewModelFactory P_13, AdvancedSettingsViewModelFactory P_14, BitmapCache P_15, ClipboardService P_16, RootInstallationManager P_17)
		: base((IValidator<ProfileSettingsViewModel>?)null)
	{
		_editProfileViewModelFactory = P_1;
		_changeThemeViewModelFactory = P_2;
		_discardChangesViewModelFactory = P_3;
		_signOutConfirmationViewModelFactory = P_4;
		_audioVideoViewModelFactory = P_5;
		_chatViewModelFactory = P_6;
		_notificationSettingsViewModelFactory = P_7;
		_windowsSettingsViewModelFactory = P_8;
		_gameOverlaySettingsViewModelFactory = P_9;
		_blockedUsersViewModelFactory = P_10;
		_privacySettingsViewModelFactory = P_11;
		_keybindingsViewModelFactory = P_12;
		_streamerModeSettingsViewModelFactory = P_13;
		_advancedSettingsViewModelFactory = P_14;
		_bitmapCache = P_15;
		_clipboardService = P_16;
		_installationManager = P_17;
		SessionUser = P_0.Session.UserInfoService.SessionUser;
		SessionUser.PropertyChanged += onSessionUserPropertyChanged;
		generateUserMenuItems();
		generateAppMenuItems();
	}

	public void OpenAudioVideoPage()
	{
		_audioAndVideoContainer?.SetForceInitialLoad();
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

	private void generateUserMenuItems()
	{
		MenuItemPageContainers.Add(new MenuItemPageContainerViewModel(Resources.UserSettings, true));
		_editProfileContainer = new MenuItemPageContainerViewModel(Resources.UserProfile);
		_editProfileContainer.MenuItemSelected += editProfileContainerOnMenuItemSelected;
		MenuItemPageContainers.Add(_editProfileContainer);
		_privacyContainer = new MenuItemPageContainerViewModel(Resources.Privacy);
		_privacyContainer.MenuItemSelected += privacyContainerOnMenuItemSelected;
		MenuItemPageContainers.Add(_privacyContainer);
		_blockedUsersContainer = new MenuItemPageContainerViewModel(Resources.BlockedUsers);
		_blockedUsersContainer.MenuItemSelected += blockedUsersContainerOnMenuItemSelected;
		MenuItemPageContainers.Add(_blockedUsersContainer);
	}

	private void generateAppMenuItems()
	{
		MenuItemPageContainers.Add(new MenuItemPageContainerViewModel(Resources.AppSettings, true));
		_audioAndVideoContainer = new MenuItemPageContainerViewModel(Resources.AudioAndVideo);
		_audioAndVideoContainer.MenuItemSelected += onAudioAndVideoContainerMenuItemSelected;
		MenuItemPageContainers.Add(_audioAndVideoContainer);
		_chatContainer = new MenuItemPageContainerViewModel(Resources.Chat);
		_chatContainer.MenuItemSelected += onChatContainerMenuItemSelected;
		MenuItemPageContainers.Add(_chatContainer);
		_notificationSettingsContainer = new MenuItemPageContainerViewModel(Resources.Notifications);
		_notificationSettingsContainer.MenuItemSelected += onNotificationsContainerMenuItemSelected;
		MenuItemPageContainers.Add(_notificationSettingsContainer);
		_changeThemeContainer = new MenuItemPageContainerViewModel(Resources.Theme);
		_changeThemeContainer.MenuItemSelected += changeThemeContainerOnMenuItemSelected;
		MenuItemPageContainers.Add(_changeThemeContainer);
		_keybindingsContainer = new MenuItemPageContainerViewModel(Resources.Keybindings);
		_keybindingsContainer.MenuItemSelected += onKeybindingsContainerMenuItemSelected;
		MenuItemPageContainers.Add(_keybindingsContainer);
		_streamerModeContainer = new MenuItemPageContainerViewModel(Resources.StreamerMode);
		_streamerModeContainer.MenuItemSelected += onStreamerModeContainerMenuItemSelected;
		MenuItemPageContainers.Add(_streamerModeContainer);
		bool flag = true;
		_windowsContainer = new MenuItemPageContainerViewModel(Resources.Windows);
		_windowsContainer.MenuItemSelected += onWindowsContainerMenuItemSelected;
		MenuItemPageContainers.Add(_windowsContainer);
		_gameOverlayContainer = new MenuItemPageContainerViewModel(Resources.GameOverlay);
		_gameOverlayContainer.MenuItemSelected += onGameOverlayContainerMenuItemSelected;
		MenuItemPageContainers.Add(_gameOverlayContainer);
		_advancedContainer = new MenuItemPageContainerViewModel(Resources.Advanced);
		_advancedContainer.MenuItemSelected += onAdvancedContainerMenuItemSelected;
		MenuItemPageContainers.Add(_advancedContainer);
	}

	private void editProfileContainerOnMenuItemSelected(MenuItemPageContainerViewModel editProfileContainerOnMenuItem)
	{
		_editProfileContainer?.Navigator.Push(_editProfileViewModelFactory.Create(_editProfileContainer.Navigator));
	}

	private void privacyContainerOnMenuItemSelected(MenuItemPageContainerViewModel privacyContainerOnMenuItem)
	{
		_privacyContainer?.Navigator.Push(_privacySettingsViewModelFactory.Create());
	}

	private void blockedUsersContainerOnMenuItemSelected(MenuItemPageContainerViewModel blockedUsersContainerOnMenuItem)
	{
		_blockedUsersContainer?.Navigator.Push(_blockedUsersViewModelFactory.Create());
	}

	private void changeThemeContainerOnMenuItemSelected(MenuItemPageContainerViewModel changeThemeContainerOnMenuItem)
	{
		_changeThemeContainer?.Navigator.Push(_changeThemeViewModelFactory.Create());
	}

	private void onAudioAndVideoContainerMenuItemSelected(MenuItemPageContainerViewModel audioAndVideoContainerOnMenuItem)
	{
		_audioAndVideoContainer?.Navigator.Push(_audioVideoViewModelFactory.Create());
	}

	private void onChatContainerMenuItemSelected(MenuItemPageContainerViewModel chatContainerOnMenuItem)
	{
		_chatContainer?.Navigator.Push(_chatViewModelFactory.Create());
	}

	private void onNotificationsContainerMenuItemSelected(MenuItemPageContainerViewModel notificationsContainerOnMenuItem)
	{
		_notificationSettingsContainer?.Navigator.Push(_notificationSettingsViewModelFactory.Create());
	}

	private void onStreamerModeContainerMenuItemSelected(MenuItemPageContainerViewModel streamerModeContainerOnMenuItem)
	{
		_streamerModeContainer?.Navigator.Push(_streamerModeSettingsViewModelFactory.Create());
	}

	private void onAdvancedContainerMenuItemSelected(MenuItemPageContainerViewModel advancedContainerOnMenuItem)
	{
		_advancedContainer?.Navigator.Push(_advancedSettingsViewModelFactory.Create());
	}

	private void onKeybindingsContainerMenuItemSelected(MenuItemPageContainerViewModel keybindingsContainerOnMenuItem)
	{
		_keybindingsContainer?.Navigator.Push(_keybindingsViewModelFactory.Create());
	}

	private void onWindowsContainerMenuItemSelected(MenuItemPageContainerViewModel windowsContainerOnMenuItem)
	{
		_windowsContainer?.Navigator.Push(_windowsSettingsViewModelFactory.Create());
	}

	private void onGameOverlayContainerMenuItemSelected(MenuItemPageContainerViewModel gameOverlayContainerOnMenuItem)
	{
		_gameOverlayContainer?.Navigator.Push(_gameOverlaySettingsViewModelFactory.Create());
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		if (MenuItemPageContainers.Any((MenuItemPageContainerViewModel menuItemPageContainerViewModel) => menuItemPageContainerViewModel.Navigator.HasPendingChanges))
		{
			DiscardChangesViewModel discardChangesViewModel = _discardChangesViewModelFactory.Create(this, "ProfileSettingsViewModel");
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(discardChangesViewModel));
		}
		else
		{
			WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this, "ProfileSettingsViewModel"));
		}
	}

	[RelayCommand]
	public void ShowSignOutConfirmationViewModel()
	{
		SignOutConfirmationViewModel signOutConfirmationViewModel = _signOutConfirmationViewModelFactory.Create();
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(signOutConfirmationViewModel));
	}

	[RelayCommand]
	public void CopySystemInfo()
	{
		_clipboardService.CopyTextToClipboard("Root Version: " + AppVersion + "\nSystem Info: " + SystemInfo);
	}

	[RelayCommand]
	public async Task CheckForUpdatesAsync()
	{
		await _installationManager.CheckAndDownloadAsync(false);
	}

	private static string getSystemInfo()
	{
		string text = Environment.OSVersion.Version.ToString();
		string text2 = RuntimeInformation.OSArchitecture.ToString().ToLower();
		_ = 0;
		if (1 == 0)
		{
		}
		string text3 = "Windows";
		return $"{text3} {text} ({text2})";
	}

	public override void Dispose()
	{
		base.Dispose();
		SessionUser.PropertyChanged -= onSessionUserPropertyChanged;
		if (_editProfileContainer != null)
		{
			_editProfileContainer.MenuItemSelected -= editProfileContainerOnMenuItemSelected;
		}
		if (_privacyContainer != null)
		{
			_privacyContainer.MenuItemSelected -= privacyContainerOnMenuItemSelected;
		}
		if (_blockedUsersContainer != null)
		{
			_blockedUsersContainer.MenuItemSelected -= blockedUsersContainerOnMenuItemSelected;
		}
		if (_changeThemeContainer != null)
		{
			_changeThemeContainer.MenuItemSelected -= editProfileContainerOnMenuItemSelected;
		}
		if (_audioAndVideoContainer != null)
		{
			_audioAndVideoContainer.MenuItemSelected -= onAudioAndVideoContainerMenuItemSelected;
		}
		if (_chatContainer != null)
		{
			_chatContainer.MenuItemSelected -= onChatContainerMenuItemSelected;
		}
		if (_notificationSettingsContainer != null)
		{
			_notificationSettingsContainer.MenuItemSelected -= onNotificationsContainerMenuItemSelected;
		}
		if (_streamerModeContainer != null)
		{
			_streamerModeContainer.MenuItemSelected -= onStreamerModeContainerMenuItemSelected;
		}
		if (_advancedContainer != null)
		{
			_advancedContainer.MenuItemSelected -= onAdvancedContainerMenuItemSelected;
		}
		if (_keybindingsContainer != null)
		{
			_keybindingsContainer.MenuItemSelected -= onKeybindingsContainerMenuItemSelected;
		}
		_ = 1;
		if (_windowsContainer != null)
		{
			_windowsContainer.MenuItemSelected -= onWindowsContainerMenuItemSelected;
		}
		_ = 1;
		if (_gameOverlayContainer != null)
		{
			_gameOverlayContainer.MenuItemSelected -= onGameOverlayContainerMenuItemSelected;
		}
		foreach (MenuItemPageContainerViewModel menuItemPageContainer in MenuItemPageContainers)
		{
			menuItemPageContainer.Dispose();
		}
		MenuItemPageContainers.Clear();
	}
}

