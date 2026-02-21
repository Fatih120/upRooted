using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using RootApp.Client.Avalonia.Controls.Emojis;
using RootApp.Client.CoreDomain.Models.Messages;

namespace RootApp.Client.Avalonia.UI.Messages;

public class AddReactionViewModel : ViewModelBase<AddReactionViewModel>
{
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? emojiPickerOpenedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand<EmojiViewModel>? emojiSelectedCommand;

	public Message Message { get; }

	public EmojiPickerViewModel EmojiPickerViewModel { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand EmojiPickerOpenedCommand => emojiPickerOpenedCommand ?? (emojiPickerOpenedCommand = new RelayCommand(EmojiPickerOpened));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand<EmojiViewModel> EmojiSelectedCommand => emojiSelectedCommand ?? (emojiSelectedCommand = new AsyncRelayCommand<EmojiViewModel>(EmojiSelectedAsync));

	public AddReactionViewModel(Message P_0, EmojiPickerViewModel P_1)
		: base((IValidator<AddReactionViewModel>?)null)
	{
		Message = P_0;
		EmojiPickerViewModel = P_1;
	}

	[RelayCommand]
	public void EmojiPickerOpened()
	{
		EmojiPickerViewModel.EmojiSelectedCommand = EmojiSelectedCommand;
	}

	[RelayCommand]
	public async Task EmojiSelectedAsync(EmojiViewModel emojiViewModel)
	{
		try
		{
			await Message.MessageContainer.Messages.CreateReactionAsync(Message.MessageId, emojiViewModel.Emoji.Shortname);
		}
		catch
		{
		}
	}
}
