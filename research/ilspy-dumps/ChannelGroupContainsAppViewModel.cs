using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class ChannelGroupContainsAppViewModel : ViewModelBase<ChannelGroupContainsAppViewModel>
{
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	public IMarkdownEngine MarkdownEngine { get; }

	public string WarningMessage => string.Format(RootApp.Client.Avalonia.Resources.Strings.Resources.ChannelGroupContainsAppDescription, <channelGroup>P.Name);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public ChannelGroupContainsAppViewModel(RootMarkdownEngineManager P_0, ChannelGroup P_1)
	{
		<channelGroup>P = P_1;
		MarkdownEngine = P_0.SimpleEngine;
		base..ctor((IValidator<ChannelGroupContainsAppViewModel>?)null);
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}
}
