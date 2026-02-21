using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Payloads.Message;

public sealed class MessagePayloadDirectMessageUsersJoined : IMessage, IMessage<MessagePayloadDirectMessageUsersJoined>, IEquatable<MessagePayloadDirectMessageUsersJoined>, IDeepCloneable<MessagePayloadDirectMessageUsersJoined>, IBufferMessage
{
	private static readonly MessageParser<MessagePayloadDirectMessageUsersJoined> _parser = new MessageParser<MessagePayloadDirectMessageUsersJoined>(() => new MessagePayloadDirectMessageUsersJoined());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<MessagePayloadUser> _repeated_users_codec = FieldCodec.ForMessage(34u, MessagePayloadUser.Parser);

	private readonly RepeatedField<MessagePayloadUser> users_ = new RepeatedField<MessagePayloadUser>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessagePayloadDirectMessageUsersJoined> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessagesReflection.Descriptor.MessageTypes[15];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<MessagePayloadUser> Users => users_;

	public MessagePayloadItem ToPayloadItem()
	{
		return new MessagePayloadItem
		{
			DirectMessageUsersJoined = this
		};
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageUsersJoined()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageUsersJoined(MessagePayloadDirectMessageUsersJoined other)
		: this()
	{
		users_ = other.users_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageUsersJoined Clone()
	{
		return new MessagePayloadDirectMessageUsersJoined(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessagePayloadDirectMessageUsersJoined);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessagePayloadDirectMessageUsersJoined other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!users_.Equals(other.users_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= users_.GetHashCode();
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
		users_.WriteTo(ref P_0, _repeated_users_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += users_.CalculateSize(_repeated_users_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessagePayloadDirectMessageUsersJoined other)
	{
		if (other != null)
		{
			users_.Add(other.users_);
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
			if (num3 != 34)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				users_.AddEntriesFrom(ref P_0, _repeated_users_codec);
			}
		}
	}
}
