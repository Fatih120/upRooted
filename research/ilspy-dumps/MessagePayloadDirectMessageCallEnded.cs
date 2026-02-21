using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.Message;

public sealed class MessagePayloadDirectMessageCallEnded : IMessage, IMessage<MessagePayloadDirectMessageCallEnded>, IEquatable<MessagePayloadDirectMessageCallEnded>, IDeepCloneable<MessagePayloadDirectMessageCallEnded>, IBufferMessage
{
	private static readonly MessageParser<MessagePayloadDirectMessageCallEnded> _parser = new MessageParser<MessagePayloadDirectMessageCallEnded>(() => new MessagePayloadDirectMessageCallEnded());

	private UnknownFieldSet _unknownFields;

	private MessageUuid startMessageId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessagePayloadDirectMessageCallEnded> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessagesReflection.Descriptor.MessageTypes[14];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessageUuid StartMessageId
	{
		get
		{
			return startMessageId_;
		}
		set
		{
			startMessageId_ = value;
		}
	}

	public MessagePayloadItem ToPayloadItem()
	{
		return new MessagePayloadItem
		{
			DirectMessageUserCallEnded = this
		};
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageCallEnded()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageCallEnded(MessagePayloadDirectMessageCallEnded other)
		: this()
	{
		startMessageId_ = ((other.startMessageId_ != null) ? other.startMessageId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageCallEnded Clone()
	{
		return new MessagePayloadDirectMessageCallEnded(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessagePayloadDirectMessageCallEnded);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessagePayloadDirectMessageCallEnded other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(StartMessageId, other.StartMessageId))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (startMessageId_ != null)
		{
			num ^= StartMessageId.GetHashCode();
		}
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
		if (startMessageId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(StartMessageId);
		}
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (startMessageId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(StartMessageId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessagePayloadDirectMessageCallEnded other)
	{
		if (other == null)
		{
			return;
		}
		if (other.startMessageId_ != null)
		{
			if (startMessageId_ == null)
			{
				StartMessageId = new MessageUuid();
			}
			StartMessageId.MergeFrom(other.StartMessageId);
		}
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
			uint num2 = num;
			uint num3 = num2;
			if (num3 != 34)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				continue;
			}
			if (startMessageId_ == null)
			{
				StartMessageId = new MessageUuid();
			}
			P_0.ReadMessage(StartMessageId);
		}
	}
}
