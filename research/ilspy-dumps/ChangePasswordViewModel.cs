// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.ChangePasswordViewModel
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
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;

public class ChangePasswordViewModel : ViewModelBase<ChangePasswordViewModel>, IPage
{
	private readonly IRootSessionAccessor _rootSessionAccessor;

	[CompilerGenerated]
	private string _003CPassword_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CNewPassword_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CConfirmNewPassword_003Ek__BackingField;

	[CompilerGenerated]
	private WebApiStatus _003CWebApiStatus_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? savePasswordCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string Password
	{
		get
		{
			return _003CPassword_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CPassword_003Ek__BackingField, text))
			{
				_003CPassword_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Password);
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("SavePasswordCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string NewPassword
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
				SavePasswordCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("SavePasswordCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ConfirmNewPassword
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
				SavePasswordCommand.NotifyCanExecuteChanged();
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

	public string PageTitle => Resources.ChangePassword;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand SavePasswordCommand => savePasswordCommand ?? (savePasswordCommand = new AsyncRelayCommand(SavePasswordAsync, () => base.HasNoErrors));

	public ChangePasswordViewModel(IRootSessionAccessor P_0)
		: base((IValidator<ChangePasswordViewModel>?)new ChangePasswordViewModelValidator())
	{
		_rootSessionAccessor = P_0;
		Password = string.Empty;
		NewPassword = string.Empty;
		ConfirmNewPassword = string.Empty;
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}

	[RelayCommand(CanExecute = "HasNoErrors")]
	public async Task SavePasswordAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			await _rootSessionAccessor.Session.UserInfoService.UpdatePasswordAsync(Password, NewPassword);
			WebApiStatus = WebApiStatus.Success;
		}
		catch
		{
			WebApiStatus = WebApiStatus.Failed;
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnNewPasswordChanged(string P_0)
	{
		ValidateProperty("NewPassword");
		ValidateProperty("ConfirmNewPassword");
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnConfirmNewPasswordChanged(string P_0)
	{
		ValidateProperty("ConfirmNewPassword");
	}
}

