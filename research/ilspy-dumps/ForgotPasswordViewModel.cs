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

public class ForgotPasswordViewModel : ViewModelBase<ForgotPasswordViewModel>
{
	private readonly ResetPasswordViewModelFactory _resetPasswordViewModelFactory;

	private readonly IRootService _rootService;

	[CompilerGenerated]
	private string? _003CEmail_003Ek__BackingField = string.Empty;

	[CompilerGenerated]
	private WebApiStatus _003CWebApiStatus_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? forgotPasswordCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? nextStepCommand;

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
	[NotifyCanExecuteChangedFor("ForgotPasswordCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? Email
	{
		get
		{
			return _003CEmail_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CEmail_003Ek__BackingField, text))
			{
				_003CEmail_003Ek__BackingField = text;
				OnEmailChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Email);
				ForgotPasswordCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ForgotPasswordCommand => forgotPasswordCommand ?? (forgotPasswordCommand = new AsyncRelayCommand(ForgotPasswordAsync, () => base.HasNoErrors));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand NextStepCommand => nextStepCommand ?? (nextStepCommand = new RelayCommand(NextStep));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public ForgotPasswordViewModel(IRootService P_0, ResetPasswordViewModelFactory P_1)
		: base((IValidator<ForgotPasswordViewModel>?)new ForgotPasswordViewModelValidator())
	{
		_rootService = P_0;
		_resetPasswordViewModelFactory = P_1;
		ValidateProperty("Email");
	}

	[RelayCommand(CanExecute = "HasNoErrors")]
	public async Task ForgotPasswordAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			await _rootService.ForgotPasswordAsync(Email);
			WebApiStatus = WebApiStatus.Success;
		}
		catch
		{
			WebApiStatus = WebApiStatus.Failed;
		}
	}

	[RelayCommand]
	public void NextStep()
	{
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(_resetPasswordViewModelFactory.Create(Email)));
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnEmailChanged(string? P_0)
	{
		ValidateProperty("Email");
	}
}
