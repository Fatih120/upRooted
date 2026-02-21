using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using RootApp.Client.CoreDomain.Models.Messages;

namespace RootApp.Client.Avalonia.UI.Messages;

public class MessageRepliesViewModel : ViewModelBase<MessageRepliesViewModel>
{
	private readonly Message _message;

	private readonly MessageReplyViewModelFactory _messageReplyViewModelFactory;

	public IEnumerable<MessageReplyViewModel>? MessageReplies => _message.MessageReplies?.Select((MessageReply r) => _messageReplyViewModelFactory.Create(r, _message.MessageContainer));

	public MessageRepliesViewModel(Message P_0, MessageReplyViewModelFactory P_1)
		: base((IValidator<MessageRepliesViewModel>?)null)
	{
		_message = P_0;
		_messageReplyViewModelFactory = P_1;
	}
}
