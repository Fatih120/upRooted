using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityMemberInviteResponse : IMessage<CommunityMemberInviteResponse>, IMessage, IEquatable<CommunityMemberInviteResponse>, IDeepCloneable<CommunityMemberInviteResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberInviteResponse> _parser = new MessageParser<CommunityMemberInviteResponse>(() => new CommunityMemberInviteResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityMemberInviteUuid id_;

	private CommunityUuid communityId_;

	private UserUuid senderUserId_;

	private UserUuid invitedUserId_;

	private string invitedUsername_ = "";

	private static readonly FieldCodec<CommunityRoleUuid> _repeated_communityRoleIds_codec = FieldCodec.ForMessage(122u, CommunityRoleUuid.Parser);

	private readonly RepeatedField<CommunityRoleUuid> communityRoleIds_ = new RepeatedField<CommunityRoleUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberInviteResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberInviteReflection.Descriptor.MessageTypes[0];

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
	public UserUuid SenderUserId
	{
		get
		{
			return senderUserId_;
		}
		set
		{
			senderUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid InvitedUserId
	{
		get
		{
			return invitedUserId_;
		}
		set
		{
			invitedUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string InvitedUsername
	{
		get
		{
			return invitedUsername_;
		}
		set
		{
			invitedUsername_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityRoleUuid> CommunityRoleIds => communityRoleIds_;

	[GeneratedCode("protoc", null)]
	public CommunityMemberInviteResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberInviteResponse(CommunityMemberInviteResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		senderUserId_ = ((other.senderUserId_ != null) ? other.senderUserId_.Clone() : null);
		invitedUserId_ = ((other.invitedUserId_ != null) ? other.invitedUserId_.Clone() : null);
		invitedUsername_ = other.invitedUsername_;
		communityRoleIds_ = other.communityRoleIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberInviteResponse Clone()
	{
		return new CommunityMemberInviteResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberInviteResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberInviteResponse other)
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
		if (!object.Equals(SenderUserId, other.SenderUserId))
		{
			return false;
		}
		if (!object.Equals(InvitedUserId, other.InvitedUserId))
		{
			return false;
		}
		if (InvitedUsername != other.InvitedUsername)
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
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (senderUserId_ != null)
		{
			num ^= SenderUserId.GetHashCode();
		}
		if (invitedUserId_ != null)
		{
			num ^= InvitedUserId.GetHashCode();
		}
		if (InvitedUsername.Length != 0)
		{
			num ^= InvitedUsername.GetHashCode();
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
		if (senderUserId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(SenderUserId);
		}
		if (invitedUserId_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(InvitedUserId);
		}
		if (InvitedUsername.Length != 0)
		{
			P_0.WriteRawTag(114);
			P_0.WriteString(InvitedUsername);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (senderUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SenderUserId);
		}
		if (invitedUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(InvitedUserId);
		}
		if (InvitedUsername.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(InvitedUsername);
		}
		num += communityRoleIds_.CalculateSize(_repeated_communityRoleIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberInviteResponse other)
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
		if (other.senderUserId_ != null)
		{
			if (senderUserId_ == null)
			{
				SenderUserId = new UserUuid();
			}
			SenderUserId.MergeFrom(other.SenderUserId);
		}
		if (other.invitedUserId_ != null)
		{
			if (invitedUserId_ == null)
			{
				InvitedUserId = new UserUuid();
			}
			InvitedUserId.MergeFrom(other.InvitedUserId);
		}
		if (other.InvitedUsername.Length != 0)
		{
			InvitedUsername = other.InvitedUsername;
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
				if (senderUserId_ == null)
				{
					SenderUserId = new UserUuid();
				}
				P_0.ReadMessage(SenderUserId);
				break;
			case 106u:
				if (invitedUserId_ == null)
				{
					InvitedUserId = new UserUuid();
				}
				P_0.ReadMessage(InvitedUserId);
				break;
			case 114u:
				InvitedUsername = P_0.ReadString();
				break;
			case 122u:
				communityRoleIds_.AddEntriesFrom(ref P_0, _repeated_communityRoleIds_codec);
				break;
			}
		}
	}
}
