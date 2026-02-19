// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Messages.ChannelStartMessageViewModel
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.UI.Messages;
using RootApp.Client.CoreDomain.Models.Messages;

public class ChannelStartMessageViewModel : ViewModelBase<ChannelStartMessageViewModel>
{
	public Message Message { get; }

	public ChannelStartMessageViewModel(Message P_0)
		: base((IValidator<ChannelStartMessageViewModel>?)null)
	{
		Message = P_0;
	}
}
