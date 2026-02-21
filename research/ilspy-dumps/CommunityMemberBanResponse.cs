using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityMemberBanResponse : IMessage<CommunityMemberBanResponse>, IMessage, IEquatable<CommunityMemberBanResponse>, IDeepCloneable<CommunityMemberBanResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberBanResponse> _parser = new MessageParser<CommunityMemberBanResponse>(() => new CommunityMemberBanResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityMemberBanUuid id_;

	private CommunityUuid communityId_;

	private UserUuid userId_;

	private UserUuid agentUserId_;

	private Timestamp expiresAt_;

	private static readonly FieldCodec<string> _single_reason_codec = FieldCodec.ForClassWrapper<string>(122u);

	private string reason_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberBanResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberBanReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanUuid Id
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
	public UserUuid AgentUserId
	{
		get
		{
			return agentUserId_;
		}
		set
		{
			agentUserId_ = value;
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
	public string Reason
	{
		get
		{
			return reason_;
		}
		set
		{
			reason_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanResponse(CommunityMemberBanResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		agentUserId_ = ((other.agentUserId_ != null) ? other.agentUserId_.Clone() : null);
		expiresAt_ = ((other.expiresAt_ != null) ? other.expiresAt_.Clone() : null);
		Reason = other.Reason;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanResponse Clone()
	{
		return new CommunityMemberBanResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberBanResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberBanResponse other)
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
		if (!object.Equals(AgentUserId, other.AgentUserId))
		{
			return false;
		}
		if (!object.Equals(ExpiresAt, other.ExpiresAt))
		{
			return false;
		}
		if (Reason != other.Reason)
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
		if (agentUserId_ != null)
		{
			num ^= AgentUserId.GetHashCode();
		}
		if (expiresAt_ != null)
		{
			num ^= ExpiresAt.GetHashCode();
		}
		if (reason_ != null)
		{
			num ^= Reason.GetHashCode();
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
		if (agentUserId_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(AgentUserId);
		}
		if (expiresAt_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(ExpiresAt);
		}
		if (reason_ != null)
		{
			_single_reason_codec.WriteTagAndValue(ref P_0, Reason);
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
		if (agentUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AgentUserId);
		}
		if (expiresAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ExpiresAt);
		}
		if (reason_ != null)
		{
			num += _single_reason_codec.CalculateSizeWithTag(Reason);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberBanResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new CommunityMemberBanUuid();
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
		if (other.agentUserId_ != null)
		{
			if (agentUserId_ == null)
			{
				AgentUserId = new UserUuid();
			}
			AgentUserId.MergeFrom(other.AgentUserId);
		}
		if (other.expiresAt_ != null)
		{
			if (expiresAt_ == null)
			{
				ExpiresAt = new Timestamp();
			}
			ExpiresAt.MergeFrom(other.ExpiresAt);
		}
		if (other.reason_ != null && (reason_ == null || other.Reason != ""))
		{
			Reason = other.Reason;
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
					Id = new CommunityMemberBanUuid();
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
				if (agentUserId_ == null)
				{
					AgentUserId = new UserUuid();
				}
				P_0.ReadMessage(AgentUserId);
				break;
			case 114u:
				if (expiresAt_ == null)
				{
					ExpiresAt = new Timestamp();
				}
				P_0.ReadMessage(ExpiresAt);
				break;
			case 122u:
			{
				string text = _single_reason_codec.Read(ref P_0);
				if (reason_ == null || text != "")
				{
					Reason = text;
				}
				break;
			}
			}
		}
	}
}
