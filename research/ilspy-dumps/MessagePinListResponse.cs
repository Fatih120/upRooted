using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class MessagePinListResponse : IMessage<MessagePinListResponse>, IMessage, IEquatable<MessagePinListResponse>, IDeepCloneable<MessagePinListResponse>, IBufferMessage
{
	private static readonly MessageParser<MessagePinListResponse> _parser = new MessageParser<MessagePinListResponse>(() => new MessagePinListResponse());

	private UnknownFieldSet _unknownFields;

	private MessageContainerResponse message_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessagePinListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[7];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessageContainerResponse Message
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
	public MessagePinListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessagePinListResponse(MessagePinListResponse other)
		: this()
	{
		message_ = ((other.message_ != null) ? other.message_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessagePinListResponse Clone()
	{
		return new MessagePinListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessagePinListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessagePinListResponse other)
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
			P_0.WriteRawTag(42);
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
	public void MergeFrom(MessagePinListResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.message_ != null)
		{
			if (message_ == null)
			{
				Message = new MessageContainerResponse();
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
			if (num3 != 42)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				continue;
			}
			if (message_ == null)
			{
				Message = new MessageContainerResponse();
			}
			P_0.ReadMessage(Message);
		}
	}
}
