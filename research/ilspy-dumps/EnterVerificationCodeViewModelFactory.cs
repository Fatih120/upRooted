// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.EnterVerificationCodeViewModelFactory
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain;

public class EnterVerificationCodeViewModelFactory(IRootSessionAccessor P_0, RootMarkdownEngineManager P_1)
{
	public EnterVerificationCodeViewModel Create()
	{
		return new EnterVerificationCodeViewModel(P_0, P_1);
	}
}

