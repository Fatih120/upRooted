using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityMemberListResponse : IMessage<CommunityMemberListResponse>, IMessage, IEquatable<CommunityMemberListResponse>, IDeepCloneable<CommunityMemberListResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberListResponse> _parser = new MessageParser<CommunityMemberListResponse>(() => new CommunityMemberListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<CommunityMemberResponse> _repeated_userCommunities_codec = FieldCodec.ForMessage(82u, CommunityMemberResponse.Parser);

	private readonly RepeatedField<CommunityMemberResponse> userCommunities_ = new RepeatedField<CommunityMemberResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityMemberResponse> UserCommunities => userCommunities_;

	[GeneratedCode("protoc", null)]
	public CommunityMemberListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberListResponse(CommunityMemberListResponse other)
		: this()
	{
		userCommunities_ = other.userCommunities_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberListResponse Clone()
	{
		return new CommunityMemberListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!userCommunities_.Equals(other.userCommunities_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= userCommunities_.GetHashCode();
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
		userCommunities_.WriteTo(ref P_0, _repeated_userCommunities_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += userCommunities_.CalculateSize(_repeated_userCommunities_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberListResponse other)
	{
		if (other != null)
		{
			userCommunities_.Add(other.userCommunities_);
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
				userCommunities_.AddEntriesFrom(ref P_0, _repeated_userCommunities_codec);
			}
		}
	}
}
