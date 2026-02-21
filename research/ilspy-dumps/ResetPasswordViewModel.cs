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
using RootApp.Client.CoreDomain.Services;

namespace RootApp.Client.Avalonia.UI.Login;

public class ResetPasswordViewModel : ViewModelBase<ResetPasswordViewModel>
{
	private readonly IRootService _rootService;

	[CompilerGenerated]
	private WebApiStatus _003CWebApiStatus_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CEmailResent_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CNewPassword_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CConfirmNewPassword_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CVerificationCode_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? resetPasswordCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? resendEmailCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public WebApiStatus WebApiStatus
	{
		get
		{
			return _003CWebApiStatus_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<WebApiStatus>.Default.Equals(_003CWebApiStatus_003Ek__BackingField, webApiStatus))
			{
				_003CWebApiStatus_003Ek__BackingField = webApiStatus;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.WebApiStatus);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool EmailResent
	{
		get
		{
			return _003CEmailResent_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CEmailResent_003Ek__BackingField, flag))
			{
				_003CEmailResent_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.EmailResent);
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("ResetPasswordCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? NewPassword
	{
		get
		{
			return _003CNewPassword_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CNewPassword_003Ek__BackingField, text))
			{
				_003CNewPassword_003Ek__BackingField = text;
				OnNewPasswordChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.NewPassword);
				ResetPasswordCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("ResetPasswordCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? ConfirmNewPassword
	{
		get
		{
			return _003CConfirmNewPassword_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CConfirmNewPassword_003Ek__BackingField, text))
			{
				_003CConfirmNewPassword_003Ek__BackingField = text;
				OnConfirmNewPasswordChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ConfirmNewPassword);
				ResetPasswordCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("ResetPasswordCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? VerificationCode
	{
		get
		{
			return _003CVerificationCode_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CVerificationCode_003Ek__BackingField, text))
			{
				_003CVerificationCode_003Ek__BackingField = text;
				OnVerificationCodeChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.VerificationCode);
				ResetPasswordCommand.NotifyCanExecuteChanged();
			}
		}
	}

	public string Email { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ResetPasswordCommand => resetPasswordCommand ?? (resetPasswordCommand = new AsyncRelayCommand(ResetPasswordAsync, canResetPassword));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ResendEmailCommand => resendEmailCommand ?? (resendEmailCommand = new AsyncRelayCommand(ResendEmailAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public ResetPasswordViewModel(IRootService P_0, string P_1)
		: base((IValidator<ResetPasswordViewModel>?)new ResetPasswordViewModelValidator())
	{
		_rootService = P_0;
		Email = P_1;
		NewPassword = string.Empty;
		ConfirmNewPassword = string.Empty;
		VerificationCode = string.Empty;
	}

	[RelayCommand(CanExecute = "canResetPassword")]
	public async Task ResetPasswordAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			await _rootService.ResetPasswordAsync(VerificationCode, Email, NewPassword);
			WebApiStatus = WebApiStatus.Success;
		}
		catch
		{
			WebApiStatus = WebApiStatus.Failed;
		}
	}

	[RelayCommand]
	public async Task ResendEmailAsync()
	{
		try
		{
			await _rootService.ForgotPasswordAsync(Email);
			EmailResent = true;
		}
		catch
		{
		}
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}

	private bool canResetPassword()
	{
		return requiredFieldsAreNotEmpty() && base.HasNoErrors;
	}

	private bool requiredFieldsAreNotEmpty()
	{
		return !string.IsNullOrEmpty(VerificationCode) && !string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmNewPassword);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnNewPasswordChanged(string? P_0)
	{
		ValidateProperty("NewPassword");
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnConfirmNewPasswordChanged(string? P_0)
	{
		ValidateProperty("ConfirmNewPassword");
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnVerificationCodeChanged(string? P_0)
	{
		ValidateProperty("VerificationCode");
	}
}
