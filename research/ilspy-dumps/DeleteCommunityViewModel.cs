// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.DeleteCommunityViewModel
using System;
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
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain;
using RootApp.Core.Identifiers;

public class DeleteCommunityViewModel : ViewModelBase<DeleteCommunityViewModel>
{
	private readonly CommunityGuid _communityId;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly Action? _onCloseCallBack;

	[CompilerGenerated]
	private WebApiStatus _003CWebApiStatus_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CConfirmationTextBoxText_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? deleteCommunityCommand;

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
	[NotifyCanExecuteChangedFor("DeleteCommunityCommand")]
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
				DeleteCommunityCommand.NotifyCanExecuteChanged();
			}
		}
	}

	public string CommunityName { get; }

	public IMarkdownEngine MarkdownEngine { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand DeleteCommunityCommand => deleteCommunityCommand ?? (deleteCommunityCommand = new AsyncRelayCommand(DeleteCommunityAsync, () => base.HasNoErrors));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public DeleteCommunityViewModel(string P_0, CommunityGuid P_1, IRootSessionAccessor P_2, RootMarkdownEngineManager P_3, Action? P_4 = null)
		: base((IValidator<DeleteCommunityViewModel>?)new DeleteCommunityViewModelValidator())
	{
		_communityId = P_1;
		_rootSessionAccessor = P_2;
		_onCloseCallBack = P_4;
		CommunityName = P_0;
		MarkdownEngine = P_3.SimpleEngine;
		ConfirmationTextBoxText = string.Empty;
	}

	[RelayCommand(CanExecute = "HasNoErrors")]
	public async Task DeleteCommunityAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			await _rootSessionAccessor.Session.CommunityService.DeleteCommunityAsync(_communityId);
			_onCloseCallBack?.Invoke();
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

