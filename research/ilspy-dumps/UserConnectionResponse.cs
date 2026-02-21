using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class UserConnectionResponse : IMessage<UserConnectionResponse>, IMessage, IEquatable<UserConnectionResponse>, IDeepCloneable<UserConnectionResponse>, IBufferMessage
{
	private static readonly MessageParser<UserConnectionResponse> _parser = new MessageParser<UserConnectionResponse>(() => new UserConnectionResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<UserUuid> _repeated_friends_codec = FieldCodec.ForMessage(82u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> friends_ = new RepeatedField<UserUuid>();

	private static readonly FieldCodec<CommunityUuid> _repeated_communities_codec = FieldCodec.ForMessage(90u, CommunityUuid.Parser);

	private readonly RepeatedField<CommunityUuid> communities_ = new RepeatedField<CommunityUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserConnectionResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[19];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<UserUuid> Friends => friends_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityUuid> Communities => communities_;

	[GeneratedCode("protoc", null)]
	public UserConnectionResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserConnectionResponse(UserConnectionResponse other)
		: this()
	{
		friends_ = other.friends_.Clone();
		communities_ = other.communities_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserConnectionResponse Clone()
	{
		return new UserConnectionResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserConnectionResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserConnectionResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!friends_.Equals(other.friends_))
		{
			return false;
		}
		if (!communities_.Equals(other.communities_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= friends_.GetHashCode();
		num ^= communities_.GetHashCode();
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
		friends_.WriteTo(ref P_0, _repeated_friends_codec);
		communities_.WriteTo(ref P_0, _repeated_communities_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += friends_.CalculateSize(_repeated_friends_codec);
		num += communities_.CalculateSize(_repeated_communities_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserConnectionResponse other)
	{
		if (other != null)
		{
			friends_.Add(other.friends_);
			communities_.Add(other.communities_);
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
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
				break;
			case 82u:
				friends_.AddEntriesFrom(ref P_0, _repeated_friends_codec);
				break;
			case 90u:
				communities_.AddEntriesFrom(ref P_0, _repeated_communities_codec);
				break;
			}
		}
	}
}
