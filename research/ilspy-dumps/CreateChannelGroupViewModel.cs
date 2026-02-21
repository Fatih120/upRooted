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
using RootApp.Client.Avalonia.UI.Community.Channels.Permissions;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class CreateChannelGroupViewModel : ViewModelBase<CreateChannelGroupViewModel>
{
	private readonly RootApp.Client.CoreDomain.Models.Community.Community _community;

	[CompilerGenerated]
	private string <ChannelGroupName>k__BackingField;

	[CompilerGenerated]
	private WebApiStatus <WebApiStatus>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? createChannelGroupCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("CreateChannelGroupCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ChannelGroupName
	{
		get
		{
			return <ChannelGroupName>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<ChannelGroupName>k__BackingField, text))
			{
				<ChannelGroupName>k__BackingField = text;
				OnChannelGroupNameChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ChannelGroupName);
				CreateChannelGroupCommand.NotifyCanExecuteChanged();
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

	public AccessRuleSelectorViewModel AccessRuleSelector { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand CreateChannelGroupCommand => createChannelGroupCommand ?? (createChannelGroupCommand = new AsyncRelayCommand(CreateChannelGroupAsync, () => base.HasNoErrors));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public CreateChannelGroupViewModel(RootApp.Client.CoreDomain.Models.Community.Community P_0, AccessRuleSelectorViewModelFactory P_1)
		: base((IValidator<CreateChannelGroupViewModel>?)new CreateChannelGroupViewModelValidator())
	{
		_community = P_0;
		ChannelGroupName = string.Empty;
		AccessRuleSelector = P_1.Create(_community, true, null, null);
	}

	[RelayCommand(CanExecute = "HasNoErrors")]
	public async Task CreateChannelGroupAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			await _community.Channels.CreateChannelGroupAsync(ChannelGroupName, AccessRuleSelector.GetAccessRuleCreateRoleOrMemberRequests());
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

	public override void Dispose()
	{
		base.Dispose();
		AccessRuleSelector.Dispose();
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnChannelGroupNameChanged(string P_0)
	{
		ValidateProperty("ChannelGroupName");
	}
}
