// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.EditProfileViewModelFactory
using RootApp.Client.Avalonia.Controls.ImageUpload;
using RootApp.Client.Avalonia.Helpers.Navigation;
using RootApp.Client.Avalonia.Helpers.StreamerMode;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;

public class EditProfileViewModelFactory(ImageUploaderViewModelFactory P_0, IRootSessionAccessor P_1, ChangePasswordViewModelFactory P_2, IStreamerModeService P_3)
{
	public EditProfileViewModel Create(Navigator P_0)
	{
		return new EditProfileViewModel(P_0, P_0, P_1, P_2, P_3);
	}
}

