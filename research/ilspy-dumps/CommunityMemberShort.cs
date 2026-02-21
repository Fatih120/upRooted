using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityMemberShort : IMessage<CommunityMemberShort>, IMessage, IEquatable<CommunityMemberShort>, IDeepCloneable<CommunityMemberShort>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberShort> _parser = new MessageParser<CommunityMemberShort>(() => new CommunityMemberShort());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private static readonly FieldCodec<CommunityRoleUuid> _repeated_communityRoleIds_codec = FieldCodec.ForMessage(90u, CommunityRoleUuid.Parser);

	private readonly RepeatedField<CommunityRoleUuid> communityRoleIds_ = new RepeatedField<CommunityRoleUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberShort> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityReflection.Descriptor.MessageTypes[6];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public UserUuid UserId
	{
		get
		{
			return userId_;
		}
		set
		{
			userId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityRoleUuid> CommunityRoleIds => communityRoleIds_;

	[GeneratedCode("protoc", null)]
	public CommunityMemberShort()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberShort(CommunityMemberShort other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		communityRoleIds_ = other.communityRoleIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberShort Clone()
	{
		return new CommunityMemberShort(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberShort);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberShort other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (!communityRoleIds_.Equals(other.communityRoleIds_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		num ^= communityRoleIds_.GetHashCode();
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
		if (userId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(UserId);
		}
		communityRoleIds_.WriteTo(ref P_0, _repeated_communityRoleIds_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		num += communityRoleIds_.CalculateSize(_repeated_communityRoleIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberShort other)
	{
		if (other == null)
		{
			return;
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		communityRoleIds_.Add(other.communityRoleIds_);
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
			case 82u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 90u:
				communityRoleIds_.AddEntriesFrom(ref P_0, _repeated_communityRoleIds_codec);
				break;
			}
		}
	}
}
