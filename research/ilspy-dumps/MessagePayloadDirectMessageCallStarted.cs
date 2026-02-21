using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Payloads.Message;

public sealed class MessagePayloadDirectMessageCallStarted : IMessage, IMessage<MessagePayloadDirectMessageCallStarted>, IEquatable<MessagePayloadDirectMessageCallStarted>, IDeepCloneable<MessagePayloadDirectMessageCallStarted>, IBufferMessage
{
	private static readonly MessageParser<MessagePayloadDirectMessageCallStarted> _parser = new MessageParser<MessagePayloadDirectMessageCallStarted>(() => new MessagePayloadDirectMessageCallStarted());

	private UnknownFieldSet _unknownFields;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessagePayloadDirectMessageCallStarted> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessagesReflection.Descriptor.MessageTypes[13];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	public MessagePayloadItem ToPayloadItem()
	{
		return new MessagePayloadItem
		{
			DirectMessageUserCallStarted = this
		};
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageCallStarted()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageCallStarted(MessagePayloadDirectMessageCallStarted other)
		: this()
	{
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageCallStarted Clone()
	{
		return new MessagePayloadDirectMessageCallStarted(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessagePayloadDirectMessageCallStarted);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessagePayloadDirectMessageCallStarted other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
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
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessagePayloadDirectMessageCallStarted other)
	{
		if (other != null)
		{
			_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
		}
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
			_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
		}
	}
}
