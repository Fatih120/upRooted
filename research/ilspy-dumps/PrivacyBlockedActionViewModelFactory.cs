// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.PrivacyBlockedActionViewModelFactory
using CommunityToolkit.Mvvm.Messaging;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Home;

public class PrivacyBlockedActionViewModelFactory(RootMarkdownEngineManager P_0)
{
	public void Show(PrivacyBlockedActionType P_0)
	{
		PrivacyBlockedActionViewModel privacyBlockedActionViewModel = new PrivacyBlockedActionViewModel(P_0, P_0);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(privacyBlockedActionViewModel));
	}
}

