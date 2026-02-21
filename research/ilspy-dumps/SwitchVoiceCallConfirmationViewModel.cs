using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class SwitchVoiceCallConfirmationViewModel : ViewModelBase<SwitchVoiceCallConfirmationViewModel>
{
	private readonly CallingService _callingService;

	private readonly MessageContainerGuid _targetRoomId;

	private readonly MediaRoom _targetMediaRoom;

	private readonly Func<Task>? _onSwitchCompleted;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? switchCallCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	public IMarkdownEngine MarkdownEngine { get; }

	public string ConfirmationMessage { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand SwitchCallCommand => switchCallCommand ?? (switchCallCommand = new AsyncRelayCommand(SwitchCallAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public SwitchVoiceCallConfirmationViewModel(string P_0, string P_1, CallingService P_2, MessageContainerGuid P_3, MediaRoom P_4, RootMarkdownEngineManager P_5, Func<Task>? P_6 = null)
		: base((IValidator<SwitchVoiceCallConfirmationViewModel>?)null)
	{
		_callingService = P_2;
		_targetRoomId = P_3;
		_targetMediaRoom = P_4;
		_onSwitchCompleted = P_6;
		MarkdownEngine = P_5.SimpleEngine;
		ConfirmationMessage = string.Format(RootApp.Client.Avalonia.Resources.Strings.Resources.SwitchVoiceCallDescription, P_0, P_1);
	}

	[RelayCommand]
	public async Task SwitchCallAsync()
	{
		CloseViewModel();
		await _callingService.JoinVoiceCallAsync(_targetRoomId, _targetMediaRoom, true);
		if (_onSwitchCompleted != null)
		{
			await _onSwitchCompleted();
		}
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}
}
