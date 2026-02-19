// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.TransferOwnershipViewModel
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
using RootApp.Client.CoreDomain.Models.Community;

public class TransferOwnershipViewModel : ViewModelBase<TransferOwnershipViewModel>
{
	private readonly IRootSessionAccessor _rootSessionAccessor;

	[CompilerGenerated]
	private WebApiStatus _003CWebApiStatus_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CConfirmationTextBoxText_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? transferOwnershipCommand;

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
	[NotifyCanExecuteChangedFor("TransferOwnershipCommand")]
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
				TransferOwnershipCommand.NotifyCanExecuteChanged();
			}
		}
	}

	public Member Member { get; }

	public IMarkdownEngine MarkdownEngine { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand TransferOwnershipCommand => transferOwnershipCommand ?? (transferOwnershipCommand = new AsyncRelayCommand(TransferOwnershipAsync, () => base.HasNoErrors));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public TransferOwnershipViewModel(Member P_0, IRootSessionAccessor P_1, RootMarkdownEngineManager P_2)
		: base((IValidator<TransferOwnershipViewModel>?)new TransferOwnershipViewModelValidator())
	{
		_rootSessionAccessor = P_1;
		ConfirmationTextBoxText = string.Empty;
		Member = P_0;
		MarkdownEngine = P_2.SimpleEngine;
	}

	[RelayCommand(CanExecute = "HasNoErrors")]
	public async Task TransferOwnershipAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			await _rootSessionAccessor.Session.CommunityService.EditOwnerAsync(Member.Community.Id, Member.UserId, ConfirmationTextBoxText);
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

