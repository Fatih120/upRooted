using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.Message;

public sealed class MessagePayloadMessagePinned : IMessage, IMessage<MessagePayloadMessagePinned>, IEquatable<MessagePayloadMessagePinned>, IDeepCloneable<MessagePayloadMessagePinned>, IBufferMessage
{
	private static readonly MessageParser<MessagePayloadMessagePinned> _parser = new MessageParser<MessagePayloadMessagePinned>(() => new MessagePayloadMessagePinned());

	private UnknownFieldSet _unknownFields;

	private MessageUuid messageId_;

	private static readonly FieldCodec<string> _single_messageContent_codec = FieldCodec.ForClassWrapper<string>(42u);

	private string messageContent_;

	private static readonly FieldCodec<string> _repeated_messageUris_codec = FieldCodec.ForString(50u);

	private readonly RepeatedField<string> messageUris_ = new RepeatedField<string>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessagePayloadMessagePinned> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessagesReflection.Descriptor.MessageTypes[7];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessageUuid MessageId
	{
		get
		{
			return messageId_;
		}
		set
		{
			messageId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string MessageContent
	{
		get
		{
			return messageContent_;
		}
		set
		{
			messageContent_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<string> MessageUris => messageUris_;

	public MessagePayloadItem ToPayloadItem()
	{
		return new MessagePayloadItem
		{
			MessagePinned = this
		};
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadMessagePinned()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadMessagePinned(MessagePayloadMessagePinned other)
		: this()
	{
		messageId_ = ((other.messageId_ != null) ? other.messageId_.Clone() : null);
		MessageContent = other.MessageContent;
		messageUris_ = other.messageUris_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadMessagePinned Clone()
	{
		return new MessagePayloadMessagePinned(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessagePayloadMessagePinned);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessagePayloadMessagePinned other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(MessageId, other.MessageId))
		{
			return false;
		}
		if (MessageContent != other.MessageContent)
		{
			return false;
		}
		if (!messageUris_.Equals(other.messageUris_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (messageId_ != null)
		{
			num ^= MessageId.GetHashCode();
		}
		if (messageContent_ != null)
		{
			num ^= MessageContent.GetHashCode();
		}
		num ^= messageUris_.GetHashCode();
		if (_unknownFields != null)
		{
			num ^= _unknownFields.GetHashCode();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public override string ToString()
	{
		return JsonFormatter.ToDiagnosticString(this);
	}

	[GeneratedCode("protoc", null)]
	public void WriteTo(CodedOutputStream output)
	{
		output.WriteRawMessage(this);
	}

	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalWriteTo(ref WriteContext P_0)
	{
		if (messageId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(MessageId);
		}
		if (messageContent_ != null)
		{
			_single_messageContent_codec.WriteTagAndValue(ref P_0, MessageContent);
		}
		messageUris_.WriteTo(ref P_0, _repeated_messageUris_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (messageId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(MessageId);
		}
		if (messageContent_ != null)
		{
			num += _single_messageContent_codec.CalculateSizeWithTag(MessageContent);
		}
		num += messageUris_.CalculateSize(_repeated_messageUris_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessagePayloadMessagePinned other)
	{
		if (other == null)
		{
			return;
		}
		if (other.messageId_ != null)
		{
			if (messageId_ == null)
			{
				MessageId = new MessageUuid();
			}
			MessageId.MergeFrom(other.MessageId);
		}
		if (other.messageContent_ != null && (messageContent_ == null || other.MessageContent != ""))
		{
			MessageContent = other.MessageContent;
		}
		messageUris_.Add(other.messageUris_);
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CodedInputStream input)
	{
		input.ReadRawMessage(this);
	}

	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalMergeFrom(ref ParseContext P_0)
	{
		uint num;
		while ((num = P_0.ReadTag()) != 0 && (num & 7) != 4)
		{
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				break;
			case 34u:
				if (messageId_ == null)
				{
					MessageId = new MessageUuid();
				}
				P_0.ReadMessage(MessageId);
				break;
			case 42u:
			{
				string text = _single_messageContent_codec.Read(ref P_0);
				if (messageContent_ == null || text != "")
				{
					MessageContent = text;
				}
				break;
			}
			case 50u:
				messageUris_.AddEntriesFrom(ref P_0, _repeated_messageUris_codec);
				break;
			}
		}
	}
}
