// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.ProfileSettingsViewModelFactory
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.Installation;
using RootApp.Client.Avalonia.UI.Community.Settings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;

public class ProfileSettingsViewModelFactory(EditProfileViewModelFactory P_0, ChangeThemeViewModelFactory P_1, DiscardChangesViewModelFactory P_2, SignOutConfirmationViewModelFactory P_3, AudioVideoViewModelFactory P_4, ChatViewModelFactory P_5, NotificationSettingsViewModelFactory P_6, WindowsSettingsViewModelFactory P_7, GameOverlaySettingsViewModelFactory P_8, BlockedUsersViewModelFactory P_9, PrivacySettingsViewModelFactory P_10, KeybindingsViewModelFactory P_11, StreamerModeSettingsViewModelFactory P_12, AdvancedSettingsViewModelFactory P_13, BitmapCache P_14, IRootSessionAccessor P_15, ClipboardService P_16, RootInstallationManager P_17)
{
	public ProfileSettingsViewModel Create()
	{
		return new ProfileSettingsViewModel(P_15, P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, P_8, P_9, P_10, P_11, P_12, P_13, P_14, P_16, P_17);
	}
}

