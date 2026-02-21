using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Payloads.Message;

public sealed class MessagePayloadDirectMessageUserLeft : IMessage, IMessage<MessagePayloadDirectMessageUserLeft>, IEquatable<MessagePayloadDirectMessageUserLeft>, IDeepCloneable<MessagePayloadDirectMessageUserLeft>, IBufferMessage
{
	private static readonly MessageParser<MessagePayloadDirectMessageUserLeft> _parser = new MessageParser<MessagePayloadDirectMessageUserLeft>(() => new MessagePayloadDirectMessageUserLeft());

	private UnknownFieldSet _unknownFields;

	private MessagePayloadUser user_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessagePayloadDirectMessageUserLeft> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessagesReflection.Descriptor.MessageTypes[16];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessagePayloadUser User
	{
		get
		{
			return user_;
		}
		set
		{
			user_ = value;
		}
	}

	public MessagePayloadItem ToPayloadItem()
	{
		return new MessagePayloadItem
		{
			DirectMessageUserLeft = this
		};
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageUserLeft()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageUserLeft(MessagePayloadDirectMessageUserLeft other)
		: this()
	{
		user_ = ((other.user_ != null) ? other.user_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageUserLeft Clone()
	{
		return new MessagePayloadDirectMessageUserLeft(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessagePayloadDirectMessageUserLeft);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessagePayloadDirectMessageUserLeft other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(User, other.User))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (user_ != null)
		{
			num ^= User.GetHashCode();
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
		if (user_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(User);
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
		if (user_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(User);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessagePayloadDirectMessageUserLeft other)
	{
		if (other == null)
		{
			return;
		}
		if (other.user_ != null)
		{
			if (user_ == null)
			{
				User = new MessagePayloadUser();
			}
			User.MergeFrom(other.User);
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
			if (user_ == null)
			{
				User = new MessagePayloadUser();
			}
			P_0.ReadMessage(User);
		}
	}
}
