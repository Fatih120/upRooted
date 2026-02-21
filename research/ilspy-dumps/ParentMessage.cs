using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Packets;

public sealed class ParentMessage : IMessage<ParentMessage>, IMessage, IEquatable<ParentMessage>, IDeepCloneable<ParentMessage>, IBufferMessage
{
	private static readonly MessageParser<ParentMessage> _parser = new MessageParser<ParentMessage>(() => new ParentMessage());

	private UnknownFieldSet _unknownFields;

	private MessageUuid id_;

	private UserUuid userId_;

	private static readonly FieldCodec<string> _single_messageContent_codec = FieldCodec.ForClassWrapper<string>(26u);

	private string messageContent_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<ParentMessage> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessageUuid Id
	{
		get
		{
			return id_;
		}
		set
		{
			id_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid UserId
	{
		get
		{
			return userId_;
		}
		set
		{
			userId_ = value;
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
	public ParentMessage()
	{
	}

	[GeneratedCode("protoc", null)]
	public ParentMessage(ParentMessage other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		MessageContent = other.MessageContent;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ParentMessage Clone()
	{
		return new ParentMessage(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ParentMessage);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ParentMessage other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (MessageContent != other.MessageContent)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (messageContent_ != null)
		{
			num ^= MessageContent.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Id);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(18);
			P_0.WriteMessage(UserId);
		}
		if (messageContent_ != null)
		{
			_single_messageContent_codec.WriteTagAndValue(ref P_0, MessageContent);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (messageContent_ != null)
		{
			num += _single_messageContent_codec.CalculateSizeWithTag(MessageContent);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ParentMessage other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new MessageUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.messageContent_ != null && (messageContent_ == null || other.MessageContent != ""))
		{
			MessageContent = other.MessageContent;
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
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				break;
			case 10u:
				if (id_ == null)
				{
					Id = new MessageUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 18u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 26u:
			{
				string text = _single_messageContent_codec.Read(ref P_0);
				if (messageContent_ == null || text != "")
				{
					MessageContent = text;
				}
				break;
			}
			}
		}
	}
}
