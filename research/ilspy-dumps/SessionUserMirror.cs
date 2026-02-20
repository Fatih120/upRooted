// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.SessionUserMirror
using System.Runtime.CompilerServices;
using RootApp.Client.CoreDomain.Models.User;

public class SessionUserMirror(SessionUser P_0)
{
	[CompilerGenerated]
	private string _003CEmail_003Ek__BackingField = P_0.Email;

	public string Username { get; } = P_0.UserName;

	public string? PictureUrl { get; } = P_0.ProfilePictureUri;
}

