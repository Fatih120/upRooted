using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityMemberInviteListResponse : IMessage<CommunityMemberInviteListResponse>, IMessage, IEquatable<CommunityMemberInviteListResponse>, IDeepCloneable<CommunityMemberInviteListResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberInviteListResponse> _parser = new MessageParser<CommunityMemberInviteListResponse>(() => new CommunityMemberInviteListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<CommunityMemberInviteResponse> _repeated_communityMemberInvites_codec = FieldCodec.ForMessage(82u, CommunityMemberInviteResponse.Parser);

	private readonly RepeatedField<CommunityMemberInviteResponse> communityMemberInvites_ = new RepeatedField<CommunityMemberInviteResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberInviteListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberInviteReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityMemberInviteResponse> CommunityMemberInvites => communityMemberInvites_;

	[GeneratedCode("protoc", null)]
	public CommunityMemberInviteListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberInviteListResponse(CommunityMemberInviteListResponse other)
		: this()
	{
		communityMemberInvites_ = other.communityMemberInvites_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberInviteListResponse Clone()
	{
		return new CommunityMemberInviteListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberInviteListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberInviteListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!communityMemberInvites_.Equals(other.communityMemberInvites_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= communityMemberInvites_.GetHashCode();
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
		communityMemberInvites_.WriteTo(ref P_0, _repeated_communityMemberInvites_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += communityMemberInvites_.CalculateSize(_repeated_communityMemberInvites_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberInviteListResponse other)
	{
		if (other != null)
		{
			communityMemberInvites_.Add(other.communityMemberInvites_);
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
				communityMemberInvites_.AddEntriesFrom(ref P_0, _repeated_communityMemberInvites_codec);
			}
		}
	}
}
