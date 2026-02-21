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
using Grpc.Core;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.Login;
using RootApp.Client.CoreDomain.Services;
using RootApp.Client.CoreDomain.Utils.Links;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Exceptions;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Payloads.WebApiException;

namespace RootApp.Client.Avalonia.UI.Register;

public class RegisterViewModel : ViewModelBase<RegisterViewModel>
{
	private readonly LoginViewModel _loginViewModel;

	private readonly HomeViewModelFactory _homeFactory;

	private readonly IRootService _rootService;

	private readonly LinkHelper _linkHelper;

	private readonly bool _isAccessTokenDisabled;

	[CompilerGenerated]
	private string <FailedWebApiStatus>k__BackingField = RootApp.Client.Avalonia.Resources.Strings.Resources.FailedRegister;

	[CompilerGenerated]
	private string? <Username>k__BackingField;

	[CompilerGenerated]
	private string? <Email>k__BackingField;

	[CompilerGenerated]
	private string? <Password>k__BackingField;

	[CompilerGenerated]
	private string? <AccessToken>k__BackingField;

	[CompilerGenerated]
	private WebApiStatus <WebApiStatus>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showLoginViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? registerAccountCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? openTermsOfUseCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? openPrivacyPolicyCommand;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("RegisterAccountCommand")]
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
				OnUsernameChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Username);
				RegisterAccountCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("RegisterAccountCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? Email
	{
		get
		{
			return <Email>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<Email>k__BackingField, text))
			{
				<Email>k__BackingField = text;
				OnEmailChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Email);
				RegisterAccountCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("RegisterAccountCommand")]
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
				OnPasswordChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Password);
				RegisterAccountCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("RegisterAccountCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? AccessToken
	{
		get
		{
			return <AccessToken>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<AccessToken>k__BackingField, text))
			{
				<AccessToken>k__BackingField = text;
				OnAccessTokenChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AccessToken);
				RegisterAccountCommand.NotifyCanExecuteChanged();
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

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string FailedWebApiStatus
	{
		get
		{
			return <FailedWebApiStatus>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<FailedWebApiStatus>k__BackingField, text))
			{
				<FailedWebApiStatus>k__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.FailedWebApiStatus);
			}
		}
	}

	public bool IsAccessTokenVisible => !_isAccessTokenDisabled;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowLoginViewModelCommand => showLoginViewModelCommand ?? (showLoginViewModelCommand = new RelayCommand(ShowLoginViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand RegisterAccountCommand => registerAccountCommand ?? (registerAccountCommand = new AsyncRelayCommand(RegisterAccountAsync, canCreateAccount));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand OpenTermsOfUseCommand => openTermsOfUseCommand ?? (openTermsOfUseCommand = new RelayCommand(OpenTermsOfUse));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand OpenPrivacyPolicyCommand => openPrivacyPolicyCommand ?? (openPrivacyPolicyCommand = new RelayCommand(OpenPrivacyPolicy));

	public RegisterViewModel(LoginViewModel P_0, HomeViewModelFactory P_1, IRootService P_2, LinkHelper P_3, RootWebApiConfig P_4)
		: base((IValidator<RegisterViewModel>?)new RegisterViewModelValidator(P_4.IsProduction))
	{
		_loginViewModel = P_0;
		_homeFactory = P_1;
		_rootService = P_2;
		_linkHelper = P_3;
		_isAccessTokenDisabled = P_4.IsProduction;
	}

	[RelayCommand]
	public void ShowLoginViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}

	[RelayCommand(CanExecute = "canCreateAccount")]
	public async Task RegisterAccountAsync()
	{
		WebApiStatus = WebApiStatus.Sending;
		UserSignUpRequest request = new UserSignUpRequest
		{
			Username = Username,
			Password = Password,
			Email = Email,
			AccessToken = (_isAccessTokenDisabled ? null : AccessToken)
		};
		try
		{
			if (await _rootService.SignUpAsync(request) && await _rootService.SignInAsync(Username, Password))
			{
				WebApiStatus = WebApiStatus.Success;
				WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(_loginViewModel));
				WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
				WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(_homeFactory.Create()));
			}
		}
		catch (RpcException ex)
		{
			handleRpcException(ex);
			WebApiStatus = WebApiStatus.Failed;
		}
	}

	[RelayCommand]
	public void OpenTermsOfUse()
	{
		_linkHelper.OpenLink("https://www.rootapp.com/terms-of-use");
	}

	[RelayCommand]
	public void OpenPrivacyPolicy()
	{
		_linkHelper.OpenLink("https://www.rootapp.com/privacy-policy");
	}

	private void handleRpcException(RpcException P_0)
	{
		RootGrpcException ex = P_0.AsRootGrpcException();
		if (ex == null)
		{
			return;
		}
		switch (ex.ErrorCode)
		{
		case ErrorCodeType.AlreadyExists:
			if (ex.Payload.Email != null)
			{
				FailedWebApiStatus = RootApp.Client.Avalonia.Resources.Strings.Resources.EmailAlreadyExists;
			}
			if (ex.Payload.Username != null)
			{
				FailedWebApiStatus = RootApp.Client.Avalonia.Resources.Strings.Resources.UsernameAlreadyExists;
			}
			break;
		case ErrorCodeType.PolicyViolationWellKnownPassword:
			FailedWebApiStatus = RootApp.Client.Avalonia.Resources.Strings.Resources.WellKnownPasswordMessage;
			break;
		case ErrorCodeType.NotFound:
			if (ex.Payload.AccessToken != null)
			{
				FailedWebApiStatus = RootApp.Client.Avalonia.Resources.Strings.Resources.InvalidAccessToken;
			}
			break;
		default:
			FailedWebApiStatus = RootApp.Client.Avalonia.Resources.Strings.Resources.FailedRegister;
			break;
		}
	}

	private bool canCreateAccount()
	{
		return requiredFieldsAreNotEmpty() && base.HasNoErrors;
	}

	private bool requiredFieldsAreNotEmpty()
	{
		if (_isAccessTokenDisabled)
		{
			return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
		}
		return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(AccessToken);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnUsernameChanged(string? P_0)
	{
		ValidateProperty("Username");
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnEmailChanged(string? P_0)
	{
		ValidateProperty("Email");
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnPasswordChanged(string? P_0)
	{
		ValidateProperty("Password");
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnAccessTokenChanged(string? P_0)
	{
		ValidateProperty("AccessToken");
	}
}
