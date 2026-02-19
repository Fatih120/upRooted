// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.VoiceCallMemberAvatarViewModelFactory
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain.Models.Media;

public class VoiceCallMemberAvatarViewModelFactory(BitmapCache P_0)
{
	public VoiceCallMemberAvatarViewModel Create(MediaMember P_0, int P_1)
	{
		return new VoiceCallMemberAvatarViewModel(P_0, P_0, P_1);
	}
}
