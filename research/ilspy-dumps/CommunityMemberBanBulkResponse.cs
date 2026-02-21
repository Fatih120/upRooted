using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityMemberBanBulkResponse : IMessage<CommunityMemberBanBulkResponse>, IMessage, IEquatable<CommunityMemberBanBulkResponse>, IDeepCloneable<CommunityMemberBanBulkResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberBanBulkResponse> _parser = new MessageParser<CommunityMemberBanBulkResponse>(() => new CommunityMemberBanBulkResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<UserUuid> _repeated_userIds_codec = FieldCodec.ForMessage(82u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> userIds_ = new RepeatedField<UserUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberBanBulkResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberBanReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<UserUuid> UserIds => userIds_;

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanBulkResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanBulkResponse(CommunityMemberBanBulkResponse other)
		: this()
	{
		userIds_ = other.userIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanBulkResponse Clone()
	{
		return new CommunityMemberBanBulkResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberBanBulkResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberBanBulkResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!userIds_.Equals(other.userIds_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= userIds_.GetHashCode();
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
		userIds_.WriteTo(ref P_0, _repeated_userIds_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += userIds_.CalculateSize(_repeated_userIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberBanBulkResponse other)
	{
		if (other != null)
		{
			userIds_.Add(other.userIds_);
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
			if (num3 != 82)
			{
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref P_0);
			}
			else
			{
				userIds_.AddEntriesFrom(ref P_0, _repeated_userIds_codec);
			}
		}
	}
}
