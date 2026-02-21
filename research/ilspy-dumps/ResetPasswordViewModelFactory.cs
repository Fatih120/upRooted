using RootApp.Client.CoreDomain.Services;

namespace RootApp.Client.Avalonia.UI.Login;

public class ResetPasswordViewModelFactory(IRootService)
{
	public ResetPasswordViewModel Create(string P_0)
	{
		return new ResetPasswordViewModel(P_0, P_0);
	}
}
