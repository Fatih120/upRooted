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
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class DeleteChannelGroupViewModel : ViewModelBase<DeleteChannelGroupViewModel>
{
	[CompilerGenerated]
	private WebApiStatus <WebApiStatus>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? deleteChannelGroupCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

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

	public ChannelGroup ChannelGroup { get; }

	public IMarkdownEngine MarkdownEngine { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand DeleteChannelGroupCommand => deleteChannelGroupCommand ?? (deleteChannelGroupCommand = new AsyncRelayCommand(DeleteChannelGroupAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public DeleteChannelGroupViewModel(ChannelGroup P_0, RootMarkdownEngineManager P_1)
		: base((IValidator<DeleteChannelGroupViewModel>?)null)
	{
		ChannelGroup = P_0;
		MarkdownEngine = P_1.SimpleEngine;
	}

	[RelayCommand]
	public async Task DeleteChannelGroupAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			await ChannelGroup.Community.Channels.DeleteChannelGroupAsync(ChannelGroup.Id);
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
}
