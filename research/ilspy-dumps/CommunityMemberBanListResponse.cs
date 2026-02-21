using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityMemberBanListResponse : IMessage<CommunityMemberBanListResponse>, IMessage, IEquatable<CommunityMemberBanListResponse>, IDeepCloneable<CommunityMemberBanListResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberBanListResponse> _parser = new MessageParser<CommunityMemberBanListResponse>(() => new CommunityMemberBanListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<CommunityMemberBanResponse> _repeated_communityMemberBans_codec = FieldCodec.ForMessage(82u, CommunityMemberBanResponse.Parser);

	private readonly RepeatedField<CommunityMemberBanResponse> communityMemberBans_ = new RepeatedField<CommunityMemberBanResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberBanListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberBanReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityMemberBanResponse> CommunityMemberBans => communityMemberBans_;

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanListResponse(CommunityMemberBanListResponse other)
		: this()
	{
		communityMemberBans_ = other.communityMemberBans_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanListResponse Clone()
	{
		return new CommunityMemberBanListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberBanListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberBanListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!communityMemberBans_.Equals(other.communityMemberBans_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= communityMemberBans_.GetHashCode();
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
		communityMemberBans_.WriteTo(ref P_0, _repeated_communityMemberBans_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += communityMemberBans_.CalculateSize(_repeated_communityMemberBans_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberBanListResponse other)
	{
		if (other != null)
		{
			communityMemberBans_.Add(other.communityMemberBans_);
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
				communityMemberBans_.AddEntriesFrom(ref P_0, _repeated_communityMemberBans_codec);
			}
		}
	}
}
