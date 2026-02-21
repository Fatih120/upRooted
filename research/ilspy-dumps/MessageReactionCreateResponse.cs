using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class MessageReactionCreateResponse : IMessage<MessageReactionCreateResponse>, IMessage, IEquatable<MessageReactionCreateResponse>, IDeepCloneable<MessageReactionCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<MessageReactionCreateResponse> _parser = new MessageParser<MessageReactionCreateResponse>(() => new MessageReactionCreateResponse());

	private UnknownFieldSet _unknownFields;

	private MessageContainerUuid containerId_;

	private string shortcode_ = "";

	private MessageUuid messageId_;

	private UserUuid userId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageReactionCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[8];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessageContainerUuid ContainerId
	{
		get
		{
			return containerId_;
		}
		set
		{
			containerId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Shortcode
	{
		get
		{
			return shortcode_;
		}
		set
		{
			shortcode_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

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
	public MessageReactionCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageReactionCreateResponse(MessageReactionCreateResponse other)
		: this()
	{
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		shortcode_ = other.shortcode_;
		messageId_ = ((other.messageId_ != null) ? other.messageId_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageReactionCreateResponse Clone()
	{
		return new MessageReactionCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageReactionCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageReactionCreateResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(ContainerId, other.ContainerId))
		{
			return false;
		}
		if (Shortcode != other.Shortcode)
		{
			return false;
		}
		if (!object.Equals(MessageId, other.MessageId))
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		if (Shortcode.Length != 0)
		{
			num ^= Shortcode.GetHashCode();
		}
		if (messageId_ != null)
		{
			num ^= MessageId.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
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
		if (containerId_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(ContainerId);
		}
		if (Shortcode.Length != 0)
		{
			P_0.WriteRawTag(34);
			P_0.WriteString(Shortcode);
		}
		if (messageId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(MessageId);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(UserId);
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
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		if (Shortcode.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Shortcode);
		}
		if (messageId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(MessageId);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageReactionCreateResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new MessageContainerUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		if (other.Shortcode.Length != 0)
		{
			Shortcode = other.Shortcode;
		}
		if (other.messageId_ != null)
		{
			if (messageId_ == null)
			{
				MessageId = new MessageUuid();
			}
			MessageId.MergeFrom(other.MessageId);
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
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
			case 26u:
				if (containerId_ == null)
				{
					ContainerId = new MessageContainerUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 34u:
				Shortcode = P_0.ReadString();
				break;
			case 42u:
				if (messageId_ == null)
				{
					MessageId = new MessageUuid();
				}
				P_0.ReadMessage(MessageId);
				break;
			case 50u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			}
		}
	}
}
