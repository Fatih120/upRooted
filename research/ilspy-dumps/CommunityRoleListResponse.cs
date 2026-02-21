using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityRoleListResponse : IMessage<CommunityRoleListResponse>, IMessage, IEquatable<CommunityRoleListResponse>, IDeepCloneable<CommunityRoleListResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityRoleListResponse> _parser = new MessageParser<CommunityRoleListResponse>(() => new CommunityRoleListResponse());

	private UnknownFieldSet _unknownFields;

	private static readonly FieldCodec<CommunityRoleResponse> _repeated_communityRoles_codec = FieldCodec.ForMessage(82u, CommunityRoleResponse.Parser);

	private readonly RepeatedField<CommunityRoleResponse> communityRoles_ = new RepeatedField<CommunityRoleResponse>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityRoleListResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityRoleReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityRoleResponse> CommunityRoles => communityRoles_;

	[GeneratedCode("protoc", null)]
	public CommunityRoleListResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleListResponse(CommunityRoleListResponse other)
		: this()
	{
		communityRoles_ = other.communityRoles_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleListResponse Clone()
	{
		return new CommunityRoleListResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityRoleListResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityRoleListResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!communityRoles_.Equals(other.communityRoles_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		num ^= communityRoles_.GetHashCode();
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
		communityRoles_.WriteTo(ref P_0, _repeated_communityRoles_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		num += communityRoles_.CalculateSize(_repeated_communityRoles_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityRoleListResponse other)
	{
		if (other != null)
		{
			communityRoles_.Add(other.communityRoles_);
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
				communityRoles_.AddEntriesFrom(ref P_0, _repeated_communityRoles_codec);
			}
		}
	}
}
