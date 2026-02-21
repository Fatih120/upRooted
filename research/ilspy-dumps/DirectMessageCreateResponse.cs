using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class DirectMessageCreateResponse : IMessage<DirectMessageCreateResponse>, IMessage, IEquatable<DirectMessageCreateResponse>, IDeepCloneable<DirectMessageCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<DirectMessageCreateResponse> _parser = new MessageParser<DirectMessageCreateResponse>(() => new DirectMessageCreateResponse());

	private UnknownFieldSet _unknownFields;

	private UserUuid creatorUserId_;

	private DirectMessageUuid id_;

	private static readonly FieldCodec<UserUuid> _repeated_memberUserIds_codec = FieldCodec.ForMessage(50u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> memberUserIds_ = new RepeatedField<UserUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectMessageCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectMessageReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public UserUuid CreatorUserId
	{
		get
		{
			return creatorUserId_;
		}
		set
		{
			creatorUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageUuid Id
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
	public RepeatedField<UserUuid> MemberUserIds => memberUserIds_;

	[GeneratedCode("protoc", null)]
	public DirectMessageCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageCreateResponse(DirectMessageCreateResponse other)
		: this()
	{
		creatorUserId_ = ((other.creatorUserId_ != null) ? other.creatorUserId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		memberUserIds_ = other.memberUserIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageCreateResponse Clone()
	{
		return new DirectMessageCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectMessageCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectMessageCreateResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CreatorUserId, other.CreatorUserId))
		{
			return false;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!memberUserIds_.Equals(other.memberUserIds_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (creatorUserId_ != null)
		{
			num ^= CreatorUserId.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		num ^= memberUserIds_.GetHashCode();
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
		if (creatorUserId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CreatorUserId);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(Id);
		}
		memberUserIds_.WriteTo(ref P_0, _repeated_memberUserIds_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (creatorUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CreatorUserId);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		num += memberUserIds_.CalculateSize(_repeated_memberUserIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectMessageCreateResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.creatorUserId_ != null)
		{
			if (creatorUserId_ == null)
			{
				CreatorUserId = new UserUuid();
			}
			CreatorUserId.MergeFrom(other.CreatorUserId);
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new DirectMessageUuid();
			}
			Id.MergeFrom(other.Id);
		}
		memberUserIds_.Add(other.memberUserIds_);
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
				if (creatorUserId_ == null)
				{
					CreatorUserId = new UserUuid();
				}
				P_0.ReadMessage(CreatorUserId);
				break;
			case 42u:
				if (id_ == null)
				{
					Id = new DirectMessageUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 50u:
				memberUserIds_.AddEntriesFrom(ref P_0, _repeated_memberUserIds_codec);
				break;
			}
		}
	}
}
