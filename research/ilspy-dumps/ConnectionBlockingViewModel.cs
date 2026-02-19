// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Main.ConnectionBlockingViewModel
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.UI.Main;

public class ConnectionBlockingViewModel : ViewModelBase<ConnectionBlockingViewModel>
{
	public ConnectionBlockingViewModel()
		: base((IValidator<ConnectionBlockingViewModel>?)null)
	{
	}
}

