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

public class EditChannelGroupViewModel : ViewModelBase<EditChannelGroupViewModel>
{
	private readonly ChannelGroup _channelGroup;

	[CompilerGenerated]
	private string <ChannelGroupName>k__BackingField;

	[CompilerGenerated]
	private WebApiStatus <WebApiStatus>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? editChannelGroupCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("EditChannelGroupCommand")]
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
				EditChannelGroupCommand.NotifyCanExecuteChanged();
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
	public IAsyncRelayCommand EditChannelGroupCommand => editChannelGroupCommand ?? (editChannelGroupCommand = new AsyncRelayCommand(EditChannelGroupAsync, () => base.HasNoErrors));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public EditChannelGroupViewModel(ChannelGroup P_0, AccessRuleSelectorViewModelFactory P_1)
		: base((IValidator<EditChannelGroupViewModel>?)new EditChannelGroupViewModelValidator())
	{
		_channelGroup = P_0;
		ChannelGroupName = P_0.Name;
		AccessRuleSelector = P_1.Create(P_0.Community, false, null, P_0.Id);
	}

	[RelayCommand(CanExecute = "HasNoErrors")]
	public async Task EditChannelGroupAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			await _channelGroup.Community.Channels.EditChannelGroupAsync(_channelGroup.Id, ChannelGroupName, AccessRuleSelector.GetAccessRuleUpdateRequest());
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
