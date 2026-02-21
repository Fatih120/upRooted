using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class CommunityGetExtendedResponse : IMessage<CommunityGetExtendedResponse>, IMessage, IEquatable<CommunityGetExtendedResponse>, IDeepCloneable<CommunityGetExtendedResponse>, IBufferMessage
{
	private static readonly MessageParser<CommunityGetExtendedResponse> _parser = new MessageParser<CommunityGetExtendedResponse>(() => new CommunityGetExtendedResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid id_;

	private string name_ = "";

	private static readonly FieldCodec<string> _single_pictureAssetUri_codec = FieldCodec.ForClassWrapper<string>(98u);

	private string pictureAssetUri_;

	private string pictureHex_ = "";

	private UserUuid ownerUserId_;

	private ChannelUuid defaultChannelId_;

	private CommunityPermission communityPermission_;

	private static readonly FieldCodec<CommunityMemberShort> _repeated_communityMembers_codec = FieldCodec.ForMessage(138u, CommunityMemberShort.Parser);

	private readonly RepeatedField<CommunityMemberShort> communityMembers_ = new RepeatedField<CommunityMemberShort>();

	private static readonly FieldCodec<CommunityRoleResponse> _repeated_communityRoles_codec = FieldCodec.ForMessage(146u, CommunityRoleResponse.Parser);

	private readonly RepeatedField<CommunityRoleResponse> communityRoles_ = new RepeatedField<CommunityRoleResponse>();

	private static readonly FieldCodec<ChannelGroupResponse> _repeated_channelGroups_codec = FieldCodec.ForMessage(154u, ChannelGroupResponse.Parser);

	private readonly RepeatedField<ChannelGroupResponse> channelGroups_ = new RepeatedField<ChannelGroupResponse>();

	private static readonly FieldCodec<UserUuid> _repeated_attachedUserIds_codec = FieldCodec.ForMessage(162u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> attachedUserIds_ = new RepeatedField<UserUuid>();

	private static readonly FieldCodec<CommunityAppResponse> _repeated_communityApps_codec = FieldCodec.ForMessage(170u, CommunityAppResponse.Parser);

	private readonly RepeatedField<CommunityAppResponse> communityApps_ = new RepeatedField<CommunityAppResponse>();

	private bool rejectUnverifiedEmail_;

	private CommunityJoinThrottle joinThrottle_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityGetExtendedResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityReflection.Descriptor.MessageTypes[3];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityUuid Id
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
	public string Name
	{
		get
		{
			return name_;
		}
		set
		{
			name_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public string PictureAssetUri
	{
		get
		{
			return pictureAssetUri_;
		}
		set
		{
			pictureAssetUri_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string PictureHex
	{
		get
		{
			return pictureHex_;
		}
		set
		{
			pictureHex_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid OwnerUserId
	{
		get
		{
			return ownerUserId_;
		}
		set
		{
			ownerUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelUuid DefaultChannelId
	{
		get
		{
			return defaultChannelId_;
		}
		set
		{
			defaultChannelId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityPermission CommunityPermission
	{
		get
		{
			return communityPermission_;
		}
		set
		{
			communityPermission_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityMemberShort> CommunityMembers => communityMembers_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityRoleResponse> CommunityRoles => communityRoles_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<ChannelGroupResponse> ChannelGroups => channelGroups_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<UserUuid> AttachedUserIds => attachedUserIds_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<CommunityAppResponse> CommunityApps => communityApps_;

	[GeneratedCode("protoc", null)]
	public bool RejectUnverifiedEmail
	{
		get
		{
			return rejectUnverifiedEmail_;
		}
		set
		{
			rejectUnverifiedEmail_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityJoinThrottle JoinThrottle
	{
		get
		{
			return joinThrottle_;
		}
		set
		{
			joinThrottle_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityGetExtendedResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityGetExtendedResponse(CommunityGetExtendedResponse other)
		: this()
	{
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		name_ = other.name_;
		PictureAssetUri = other.PictureAssetUri;
		pictureHex_ = other.pictureHex_;
		ownerUserId_ = ((other.ownerUserId_ != null) ? other.ownerUserId_.Clone() : null);
		defaultChannelId_ = ((other.defaultChannelId_ != null) ? other.defaultChannelId_.Clone() : null);
		communityPermission_ = ((other.communityPermission_ != null) ? other.communityPermission_.Clone() : null);
		communityMembers_ = other.communityMembers_.Clone();
		communityRoles_ = other.communityRoles_.Clone();
		channelGroups_ = other.channelGroups_.Clone();
		attachedUserIds_ = other.attachedUserIds_.Clone();
		communityApps_ = other.communityApps_.Clone();
		rejectUnverifiedEmail_ = other.rejectUnverifiedEmail_;
		joinThrottle_ = ((other.joinThrottle_ != null) ? other.joinThrottle_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityGetExtendedResponse Clone()
	{
		return new CommunityGetExtendedResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityGetExtendedResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityGetExtendedResponse other)
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
		if (Name != other.Name)
		{
			return false;
		}
		if (PictureAssetUri != other.PictureAssetUri)
		{
			return false;
		}
		if (PictureHex != other.PictureHex)
		{
			return false;
		}
		if (!object.Equals(OwnerUserId, other.OwnerUserId))
		{
			return false;
		}
		if (!object.Equals(DefaultChannelId, other.DefaultChannelId))
		{
			return false;
		}
		if (!object.Equals(CommunityPermission, other.CommunityPermission))
		{
			return false;
		}
		if (!communityMembers_.Equals(other.communityMembers_))
		{
			return false;
		}
		if (!communityRoles_.Equals(other.communityRoles_))
		{
			return false;
		}
		if (!channelGroups_.Equals(other.channelGroups_))
		{
			return false;
		}
		if (!attachedUserIds_.Equals(other.attachedUserIds_))
		{
			return false;
		}
		if (!communityApps_.Equals(other.communityApps_))
		{
			return false;
		}
		if (RejectUnverifiedEmail != other.RejectUnverifiedEmail)
		{
			return false;
		}
		if (!object.Equals(JoinThrottle, other.JoinThrottle))
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
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (pictureAssetUri_ != null)
		{
			num ^= PictureAssetUri.GetHashCode();
		}
		if (PictureHex.Length != 0)
		{
			num ^= PictureHex.GetHashCode();
		}
		if (ownerUserId_ != null)
		{
			num ^= OwnerUserId.GetHashCode();
		}
		if (defaultChannelId_ != null)
		{
			num ^= DefaultChannelId.GetHashCode();
		}
		if (communityPermission_ != null)
		{
			num ^= CommunityPermission.GetHashCode();
		}
		num ^= communityMembers_.GetHashCode();
		num ^= communityRoles_.GetHashCode();
		num ^= channelGroups_.GetHashCode();
		num ^= attachedUserIds_.GetHashCode();
		num ^= communityApps_.GetHashCode();
		if (RejectUnverifiedEmail)
		{
			num ^= RejectUnverifiedEmail.GetHashCode();
		}
		if (joinThrottle_ != null)
		{
			num ^= JoinThrottle.GetHashCode();
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
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(90);
			P_0.WriteString(Name);
		}
		if (pictureAssetUri_ != null)
		{
			_single_pictureAssetUri_codec.WriteTagAndValue(ref P_0, PictureAssetUri);
		}
		if (PictureHex.Length != 0)
		{
			P_0.WriteRawTag(106);
			P_0.WriteString(PictureHex);
		}
		if (ownerUserId_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(OwnerUserId);
		}
		if (defaultChannelId_ != null)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(DefaultChannelId);
		}
		if (communityPermission_ != null)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(CommunityPermission);
		}
		communityMembers_.WriteTo(ref P_0, _repeated_communityMembers_codec);
		communityRoles_.WriteTo(ref P_0, _repeated_communityRoles_codec);
		channelGroups_.WriteTo(ref P_0, _repeated_channelGroups_codec);
		attachedUserIds_.WriteTo(ref P_0, _repeated_attachedUserIds_codec);
		communityApps_.WriteTo(ref P_0, _repeated_communityApps_codec);
		if (RejectUnverifiedEmail)
		{
			P_0.WriteRawTag(176, 1);
			P_0.WriteBool(RejectUnverifiedEmail);
		}
		if (joinThrottle_ != null)
		{
			P_0.WriteRawTag(186, 1);
			P_0.WriteMessage(JoinThrottle);
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
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (pictureAssetUri_ != null)
		{
			num += _single_pictureAssetUri_codec.CalculateSizeWithTag(PictureAssetUri);
		}
		if (PictureHex.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(PictureHex);
		}
		if (ownerUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(OwnerUserId);
		}
		if (defaultChannelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DefaultChannelId);
		}
		if (communityPermission_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityPermission);
		}
		num += communityMembers_.CalculateSize(_repeated_communityMembers_codec);
		num += communityRoles_.CalculateSize(_repeated_communityRoles_codec);
		num += channelGroups_.CalculateSize(_repeated_channelGroups_codec);
		num += attachedUserIds_.CalculateSize(_repeated_attachedUserIds_codec);
		num += communityApps_.CalculateSize(_repeated_communityApps_codec);
		if (RejectUnverifiedEmail)
		{
			num += 3;
		}
		if (joinThrottle_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(JoinThrottle);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityGetExtendedResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new CommunityUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.pictureAssetUri_ != null && (pictureAssetUri_ == null || other.PictureAssetUri != ""))
		{
			PictureAssetUri = other.PictureAssetUri;
		}
		if (other.PictureHex.Length != 0)
		{
			PictureHex = other.PictureHex;
		}
		if (other.ownerUserId_ != null)
		{
			if (ownerUserId_ == null)
			{
				OwnerUserId = new UserUuid();
			}
			OwnerUserId.MergeFrom(other.OwnerUserId);
		}
		if (other.defaultChannelId_ != null)
		{
			if (defaultChannelId_ == null)
			{
				DefaultChannelId = new ChannelUuid();
			}
			DefaultChannelId.MergeFrom(other.DefaultChannelId);
		}
		if (other.communityPermission_ != null)
		{
			if (communityPermission_ == null)
			{
				CommunityPermission = new CommunityPermission();
			}
			CommunityPermission.MergeFrom(other.CommunityPermission);
		}
		communityMembers_.Add(other.communityMembers_);
		communityRoles_.Add(other.communityRoles_);
		channelGroups_.Add(other.channelGroups_);
		attachedUserIds_.Add(other.attachedUserIds_);
		communityApps_.Add(other.communityApps_);
		if (other.RejectUnverifiedEmail)
		{
			RejectUnverifiedEmail = other.RejectUnverifiedEmail;
		}
		if (other.joinThrottle_ != null)
		{
			if (joinThrottle_ == null)
			{
				JoinThrottle = new CommunityJoinThrottle();
			}
			JoinThrottle.MergeFrom(other.JoinThrottle);
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
					Id = new CommunityUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 90u:
				Name = P_0.ReadString();
				break;
			case 98u:
			{
				string text = _single_pictureAssetUri_codec.Read(ref P_0);
				if (pictureAssetUri_ == null || text != "")
				{
					PictureAssetUri = text;
				}
				break;
			}
			case 106u:
				PictureHex = P_0.ReadString();
				break;
			case 114u:
				if (ownerUserId_ == null)
				{
					OwnerUserId = new UserUuid();
				}
				P_0.ReadMessage(OwnerUserId);
				break;
			case 122u:
				if (defaultChannelId_ == null)
				{
					DefaultChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(DefaultChannelId);
				break;
			case 130u:
				if (communityPermission_ == null)
				{
					CommunityPermission = new CommunityPermission();
				}
				P_0.ReadMessage(CommunityPermission);
				break;
			case 138u:
				communityMembers_.AddEntriesFrom(ref P_0, _repeated_communityMembers_codec);
				break;
			case 146u:
				communityRoles_.AddEntriesFrom(ref P_0, _repeated_communityRoles_codec);
				break;
			case 154u:
				channelGroups_.AddEntriesFrom(ref P_0, _repeated_channelGroups_codec);
				break;
			case 162u:
				attachedUserIds_.AddEntriesFrom(ref P_0, _repeated_attachedUserIds_codec);
				break;
			case 170u:
				communityApps_.AddEntriesFrom(ref P_0, _repeated_communityApps_codec);
				break;
			case 176u:
				RejectUnverifiedEmail = P_0.ReadBool();
				break;
			case 186u:
				if (joinThrottle_ == null)
				{
					JoinThrottle = new CommunityJoinThrottle();
				}
				P_0.ReadMessage(JoinThrottle);
				break;
			}
		}
	}
}
