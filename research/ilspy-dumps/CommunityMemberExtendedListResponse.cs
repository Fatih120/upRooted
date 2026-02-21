using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityMemberExtendedListResponse : IMessage<CommunityMemberExtendedListResponse>, IMessage, IEquatable<CommunityMemberExtendedListResponse>, IDeepCloneable<CommunityMemberExtendedListResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberExtendedListResponse> _parser = new MessageParser<CommunityMemberExtendedListResponse>(() => new CommunityMemberExtendedListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<CommunityMemberExtendedResponse> _repeated_communityMembers_codec = FieldCodec.ForMessage(82u, CommunityMemberExtendedResponse.Parser);

	private readonly RepeatedField<CommunityMemberExtendedResponse> communityMembers_ = new RepeatedField<CommunityMemberExtendedResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberExtendedListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityMemberExtendedResponse> CommunityMembers => communityMembers_;

	[GeneratedCode("protoc", null)]
	public CommunityMemberExtendedListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberExtendedListResponse(CommunityMemberExtendedListResponse other)
		: this()
	{
		communityMembers_ = other.communityMembers_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberExtendedListResponse Clone()
	{
		return new CommunityMemberExtendedListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberExtendedListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberExtendedListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!communityMembers_.Equals(other.communityMembers_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= communityMembers_.GetHashCode();
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
		communityMembers_.WriteTo(ref P_0, _repeated_communityMembers_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += communityMembers_.CalculateSize(_repeated_communityMembers_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberExtendedListResponse other)
	{
		if (other != null)
		{
			communityMembers_.Add(other.communityMembers_);
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
				communityMembers_.AddEntriesFrom(ref P_0, _repeated_communityMembers_codec);
			}
		}
	}
}
