// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.EnterVerificationCodeViewModel
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
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain;

public class EnterVerificationCodeViewModel : ViewModelBase<EnterVerificationCodeViewModel>
{
	private readonly IRootSessionAccessor _rootSessionAccessor;

	[CompilerGenerated]
	private string? _003CConfirmationTextBoxText_003Ek__BackingField;

	[CompilerGenerated]
	private WebApiStatus _003CWebApiStatus_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? submitVerificationCodeCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("SubmitVerificationCodeCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? ConfirmationTextBoxText
	{
		get
		{
			return _003CConfirmationTextBoxText_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CConfirmationTextBoxText_003Ek__BackingField, text))
			{
				_003CConfirmationTextBoxText_003Ek__BackingField = text;
				OnConfirmationTextBoxTextChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ConfirmationTextBoxText);
				SubmitVerificationCodeCommand.NotifyCanExecuteChanged();
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

	public IMarkdownEngine MarkdownEngine { get; }

	public string EmailValidationDescription { get; } = Resources.EmailValidationDescription;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand SubmitVerificationCodeCommand => submitVerificationCodeCommand ?? (submitVerificationCodeCommand = new AsyncRelayCommand(SubmitVerificationCodeAsync, () => base.HasNoErrors));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public EnterVerificationCodeViewModel(IRootSessionAccessor P_0, RootMarkdownEngineManager P_1)
		: base((IValidator<EnterVerificationCodeViewModel>?)new EnterVerificationCodeViewModelValidator())
	{
		_rootSessionAccessor = P_0;
		MarkdownEngine = P_1.SimpleEngine;
		ValidateProperty("ConfirmationTextBoxText");
	}

	[RelayCommand(CanExecute = "HasNoErrors")]
	public async Task SubmitVerificationCodeAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			await _rootSessionAccessor.Session.UserInfoService.SetVerificationEmailAsync(ConfirmationTextBoxText);
			WebApiStatus = WebApiStatus.Success;
		}
		catch
		{
			WebApiStatus = WebApiStatus.Failed;
		}
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnConfirmationTextBoxTextChanged(string? P_0)
	{
		ValidateProperty("ConfirmationTextBoxText");
	}
}

