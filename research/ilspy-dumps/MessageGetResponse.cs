using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class MessageGetResponse : IMessage<MessageGetResponse>, IMessage, IEquatable<MessageGetResponse>, IDeepCloneable<MessageGetResponse>, IBufferMessage
{
	private static readonly MessageParser<MessageGetResponse> _parser = new MessageParser<MessageGetResponse>(() => new MessageGetResponse());

	private UnknownFieldSet _unknownFields;

	private MessagePacket message_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageGetResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessagePacket Message
	{
		get
		{
			return message_;
		}
		set
		{
			message_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageGetResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageGetResponse(MessageGetResponse other)
		: this()
	{
		message_ = ((other.message_ != null) ? other.message_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageGetResponse Clone()
	{
		return new MessageGetResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageGetResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageGetResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Message, other.Message))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (message_ != null)
		{
			num ^= Message.GetHashCode();
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
		if (message_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Message);
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
		if (message_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Message);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageGetResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.message_ != null)
		{
			if (message_ == null)
			{
				Message = new MessagePacket();
			}
			Message.MergeFrom(other.Message);
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
			if (num3 != 82)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				continue;
			}
			if (message_ == null)
			{
				Message = new MessagePacket();
			}
			P_0.ReadMessage(Message);
		}
	}
}
