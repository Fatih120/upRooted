using RootApp.Client.CoreDomain.Services;

namespace RootApp.Client.Avalonia.UI.Login;

public class ForgotPasswordViewModelFactory(IRootService, ResetPasswordViewModelFactory)
{
	public ForgotPasswordViewModel Create()
	{
		return new ForgotPasswordViewModel(P_0, P_1);
	}
}
