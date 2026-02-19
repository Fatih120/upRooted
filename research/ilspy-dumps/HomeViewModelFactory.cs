// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.HomeViewModelFactory
using RootApp.Client.Avalonia.Helpers.Activation;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Helpers.DirectMessages;
using RootApp.Client.Avalonia.Helpers.LinkJoining;
using RootApp.Client.Avalonia.Helpers.Overlay;
using RootApp.Client.Avalonia.Helpers.Panes;
using RootApp.Client.Avalonia.Helpers.Popout;
using RootApp.Client.Avalonia.Helpers.StreamerMode;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.Home.EmailValidation;
using RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Friends;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Notifications;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile;
using RootApp.Client.Avalonia.UI.Home.VoiceBar;
using RootApp.Client.CoreDomain;
using RootApp.Client.Domain.Helpers.Store;

public class HomeViewModelFactory(FriendsViewModelFactory P_0, DirectMessagesViewModelFactory P_1, NotificationsViewModelFactory P_2, ProfileViewModelFactory P_3, IRootSessionAccessor P_4, CommunityTabViewModelFactory P_5, DirectMessageTabViewModelFactory P_6, NewTabViewModelFactory P_7, ILocalDataStore P_8, BitmapCache P_9, PaneDisplayService P_10, VoiceBarViewModelFactory P_11, EnterVerificationCodeViewModelFactory P_12, VerificationCodeCooldownViewModelFactory P_13, DirectMessageIncomingCallServiceFactory P_14, CallPopoutService P_15, IUriSchemeRegistrar P_16, LinkJoiningService P_17, OverlayService P_18, IStreamerModeService P_19, ITabPopoutService P_20)
{
	public HomeViewModel Create()
	{
		return new HomeViewModel(P_0, P_1, P_2, P_3, P_4, P_5, P_6, P_7, P_9, P_8, P_10, P_11, P_12, P_13, P_14, P_15, P_16, P_17, P_18, P_19, P_20);
	}
}

