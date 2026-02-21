using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Home.VoiceBar;

public class ScreenshareAudioFailedViewModel : ViewModelBase<ScreenshareAudioFailedViewModel>
{
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	public IMarkdownEngine MarkdownEngine { get; } = P_0.SimpleEngine;

	public string ErrorMessage => RootApp.Client.Avalonia.Resources.Strings.Resources.ScreenshareAudioFailed;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public ScreenshareAudioFailedViewModel(RootMarkdownEngineManager P_0)
		: base((IValidator<ScreenshareAudioFailedViewModel>?)null)
	{
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}
}
