// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.PrivacyBlockedActionViewModel
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home;

public class PrivacyBlockedActionViewModel(RootMarkdownEngineManager P_0, PrivacyBlockedActionType P_1) : ViewModelBase<PrivacyBlockedActionViewModel>((IValidator<PrivacyBlockedActionViewModel>?)null)
{
	[CompilerGenerated]
	private readonly PrivacyBlockedActionType _003CActionType_003Ek__BackingField = P_1;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	public IMarkdownEngine MarkdownEngine { get; } = P_0.SimpleEngine;

	public string ErrorMessage => Resources.PrivacyBlockedActionDescription;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}
}

