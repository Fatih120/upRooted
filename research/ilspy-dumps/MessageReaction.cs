using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Packets;

public sealed class MessageReaction : IMessage<MessageReaction>, IMessage, IEquatable<MessageReaction>, IDeepCloneable<MessageReaction>, IBufferMessage
{
	private static readonly MessageParser<MessageReaction> _parser = new MessageParser<MessageReaction>(() => new MessageReaction());

	private UnknownFieldSet _unknownFields;

	private string shortcode_ = "";

	private UserUuid userId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageReaction> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public MessageReaction()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageReaction(MessageReaction other)
		: this()
	{
		shortcode_ = other.shortcode_;
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageReaction Clone()
	{
		return new MessageReaction(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageReaction);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageReaction other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (Shortcode != other.Shortcode)
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
		if (Shortcode.Length != 0)
		{
			num ^= Shortcode.GetHashCode();
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
		if (Shortcode.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(Shortcode);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(90);
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
		if (Shortcode.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Shortcode);
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
	public void MergeFrom(MessageReaction other)
	{
		if (other == null)
		{
			return;
		}
		if (other.Shortcode.Length != 0)
		{
			Shortcode = other.Shortcode;
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
			case 82u:
				Shortcode = P_0.ReadString();
				break;
			case 90u:
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
