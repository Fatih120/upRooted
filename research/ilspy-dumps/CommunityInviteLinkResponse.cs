using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityInviteLinkResponse : IMessage<CommunityInviteLinkResponse>, IMessage, IEquatable<CommunityInviteLinkResponse>, IDeepCloneable<CommunityInviteLinkResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityInviteLinkResponse> _parser = new MessageParser<CommunityInviteLinkResponse>(() => new CommunityInviteLinkResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityMemberInviteUuid id_;

	private CommunityUuid communityId_;

	private UserUuid userId_;

	private string code_ = "";

	private static readonly FieldCodec<int?> _single_maxUses_codec = FieldCodec.ForStructWrapper<int>(114u);

	private int? maxUses_;

	private long numUses_;

	private Timestamp expiresAt_;

	private string link_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityInviteLinkResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => LinkReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityMemberInviteUuid Id
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
	public CommunityUuid CommunityId
	{
		get
		{
			return communityId_;
		}
		set
		{
			communityId_ = value;
		}
	}

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
	public string Code
	{
		get
		{
			return code_;
		}
		set
		{
			code_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public int? MaxUses
	{
		get
		{
			return maxUses_;
		}
		set
		{
			maxUses_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public long NumUses
	{
		get
		{
			return numUses_;
		}
		set
		{
			numUses_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp ExpiresAt
	{
		get
		{
			return expiresAt_;
		}
		set
		{
			expiresAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string Link
	{
		get
		{
			return link_;
		}
		set
		{
			link_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityInviteLinkResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityInviteLinkResponse(CommunityInviteLinkResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		code_ = other.code_;
		MaxUses = other.MaxUses;
		numUses_ = other.numUses_;
		expiresAt_ = ((other.expiresAt_ != null) ? other.expiresAt_.Clone() : null);
		link_ = other.link_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityInviteLinkResponse Clone()
	{
		return new CommunityInviteLinkResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityInviteLinkResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityInviteLinkResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (Code != other.Code)
		{
			return false;
		}
		if (MaxUses != other.MaxUses)
		{
			return false;
		}
		if (NumUses != other.NumUses)
		{
			return false;
		}
		if (!object.Equals(ExpiresAt, other.ExpiresAt))
		{
			return false;
		}
		if (Link != other.Link)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (Code.Length != 0)
		{
			num ^= Code.GetHashCode();
		}
		if (maxUses_.HasValue)
		{
			num ^= MaxUses.GetHashCode();
		}
		if (NumUses != 0)
		{
			num ^= NumUses.GetHashCode();
		}
		if (expiresAt_ != null)
		{
			num ^= ExpiresAt.GetHashCode();
		}
		if (Link.Length != 0)
		{
			num ^= Link.GetHashCode();
		}
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
		if (id_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Id);
		}
		if (communityId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(CommunityId);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(UserId);
		}
		if (Code.Length != 0)
		{
			P_0.WriteRawTag(106);
			P_0.WriteString(Code);
		}
		if (maxUses_.HasValue)
		{
			_single_maxUses_codec.WriteTagAndValue(ref P_0, MaxUses);
		}
		if (NumUses != 0)
		{
			P_0.WriteRawTag(120);
			P_0.WriteInt64(NumUses);
		}
		if (expiresAt_ != null)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(ExpiresAt);
		}
		if (Link.Length != 0)
		{
			P_0.WriteRawTag(162, 1);
			P_0.WriteString(Link);
		}
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (Code.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Code);
		}
		if (maxUses_.HasValue)
		{
			num += _single_maxUses_codec.CalculateSizeWithTag(MaxUses);
		}
		if (NumUses != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(NumUses);
		}
		if (expiresAt_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ExpiresAt);
		}
		if (Link.Length != 0)
		{
			num += 2 + CodedOutputStream.ComputeStringSize(Link);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityInviteLinkResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new CommunityMemberInviteUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.Code.Length != 0)
		{
			Code = other.Code;
		}
		if (other.maxUses_.HasValue && (!maxUses_.HasValue || other.MaxUses != 0))
		{
			MaxUses = other.MaxUses;
		}
		if (other.NumUses != 0)
		{
			NumUses = other.NumUses;
		}
		if (other.expiresAt_ != null)
		{
			if (expiresAt_ == null)
			{
				ExpiresAt = new Timestamp();
			}
			ExpiresAt.MergeFrom(other.ExpiresAt);
		}
		if (other.Link.Length != 0)
		{
			Link = other.Link;
		}
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
				if (id_ == null)
				{
					Id = new CommunityMemberInviteUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 90u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 98u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 106u:
				Code = P_0.ReadString();
				break;
			case 114u:
			{
				int? num2 = _single_maxUses_codec.Read(ref P_0);
				if (!maxUses_.HasValue || num2 != 0)
				{
					MaxUses = num2;
				}
				break;
			}
			case 120u:
				NumUses = P_0.ReadInt64();
				break;
			case 130u:
				if (expiresAt_ == null)
				{
					ExpiresAt = new Timestamp();
				}
				P_0.ReadMessage(ExpiresAt);
				break;
			case 162u:
				Link = P_0.ReadString();
				break;
			}
		}
	}
}
