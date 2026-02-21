using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.Register;
using RootApp.Client.CoreDomain.Services;

namespace RootApp.Client.Avalonia.UI.Login;

public class LoginViewModel : ViewModelBase<LoginViewModel>
{
	private readonly RegisterViewModelFactory _registerFactory;

	private readonly HomeViewModelFactory _homeFactory;

	private readonly ForgotUsernameOrPasswordPickerViewModelFactory _forgotUsernameOrPasswordPickerViewModelFactory;

	private readonly IRootService _rootService;

	[CompilerGenerated]
	private string? <Username>k__BackingField;

	[CompilerGenerated]
	private string? <Password>k__BackingField;

	[CompilerGenerated]
	private WebApiStatus <WebApiStatus>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showRegisterViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showForgotUsernameOrPasswordPickerViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? loginCommand;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("LoginCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? Username
	{
		get
		{
			return <Username>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<Username>k__BackingField, text))
			{
				<Username>k__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Username);
				LoginCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("LoginCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? Password
	{
		get
		{
			return <Password>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<Password>k__BackingField, text))
			{
				<Password>k__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Password);
				LoginCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public WebApiStatus WebApiStatus
	{
		get
		{
			return <WebApiStatus>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<WebApiStatus>.Default.Equals(<WebApiStatus>k__BackingField, webApiStatus))
			{
				<WebApiStatus>k__BackingField = webApiStatus;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.WebApiStatus);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowRegisterViewModelCommand => showRegisterViewModelCommand ?? (showRegisterViewModelCommand = new RelayCommand(ShowRegisterViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowForgotUsernameOrPasswordPickerViewModelCommand => showForgotUsernameOrPasswordPickerViewModelCommand ?? (showForgotUsernameOrPasswordPickerViewModelCommand = new RelayCommand(ShowForgotUsernameOrPasswordPickerViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand LoginCommand => loginCommand ?? (loginCommand = new AsyncRelayCommand(LoginAsync, canLogin));

	public LoginViewModel(RegisterViewModelFactory P_0, HomeViewModelFactory P_1, ForgotUsernameOrPasswordPickerViewModelFactory P_2, IRootService P_3)
		: base((IValidator<LoginViewModel>?)null)
	{
		_registerFactory = P_0;
		_homeFactory = P_1;
		_forgotUsernameOrPasswordPickerViewModelFactory = P_2;
		_rootService = P_3;
	}

	[RelayCommand]
	public void ShowRegisterViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(_registerFactory.Create(this)));
	}

	[RelayCommand]
	public void ShowForgotUsernameOrPasswordPickerViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(_forgotUsernameOrPasswordPickerViewModelFactory.Create()));
	}

	[RelayCommand(CanExecute = "canLogin")]
	public async Task LoginAsync()
	{
		WebApiStatus = WebApiStatus.Sending;
		if (await _rootService.SignInAsync(Username, Password))
		{
			WebApiStatus = WebApiStatus.Success;
			WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(_homeFactory.Create()));
		}
		else
		{
			WebApiStatus = WebApiStatus.Failed;
		}
	}

	private bool canLogin()
	{
		return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
	}
}
