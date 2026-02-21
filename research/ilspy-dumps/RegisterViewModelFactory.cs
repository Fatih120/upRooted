using Microsoft.Extensions.Options;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.Login;
using RootApp.Client.CoreDomain.Services;
using RootApp.Client.CoreDomain.Utils.Links;
using RootApp.Core;

namespace RootApp.Client.Avalonia.UI.Register;

public class RegisterViewModelFactory(HomeViewModelFactory, IRootService, LinkHelper, IOptions<RootWebApiConfig>)
{
	public RegisterViewModel Create(LoginViewModel P_0)
	{
		return new RegisterViewModel(P_0, P_0, P_1, P_2, P_3.Value);
	}
}
