using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class ChannelResponse : IMessage<ChannelResponse>, IMessage, IEquatable<ChannelResponse>, IDeepCloneable<ChannelResponse>, IBufferMessage
{
	private static readonly MessageParser<ChannelResponse> _parser = new MessageParser<ChannelResponse>(() => new ChannelResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private ChannelGroupUuid channelGroupId_;

	private ChannelUuid id_;

	private string name_ = "";

	private static readonly FieldCodec<string> _single_description_codec = FieldCodec.ForClassWrapper<string>(114u);

	private string description_;

	private static readonly FieldCodec<string> _single_iconAssetUri_codec = FieldCodec.ForClassWrapper<string>(122u);

	private string iconAssetUri_;

	private int channelType_;

	private float position_;

	private CommunityAppUuid communityAppId_;

	private ChannelPermission channelPermission_;

	private Timestamp lastActivityAt_;

	private Timestamp userLastViewedAt_;

	private bool useChannelGroupPermission_;

	private static readonly FieldCodec<WebRtcUserDevicePacket> _repeated_webRtcMembers_codec = FieldCodec.ForMessage(186u, WebRtcUserDevicePacket.Parser);

	private readonly RepeatedField<WebRtcUserDevicePacket> webRtcMembers_ = new RepeatedField<WebRtcUserDevicePacket>();

	private static readonly FieldCodec<RoleOrMemberUuid> _repeated_roleOrMemberIds_codec = FieldCodec.ForMessage(194u, RoleOrMemberUuid.Parser);

	private readonly RepeatedField<RoleOrMemberUuid> roleOrMemberIds_ = new RepeatedField<RoleOrMemberUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<ChannelResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChannelReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public ChannelGroupUuid ChannelGroupId
	{
		get
		{
			return channelGroupId_;
		}
		set
		{
			channelGroupId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelUuid Id
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
	public string Description
	{
		get
		{
			return description_;
		}
		set
		{
			description_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string IconAssetUri
	{
		get
		{
			return iconAssetUri_;
		}
		set
		{
			iconAssetUri_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public int ChannelType
	{
		get
		{
			return channelType_;
		}
		set
		{
			channelType_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public float Position
	{
		get
		{
			return position_;
		}
		set
		{
			position_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppUuid CommunityAppId
	{
		get
		{
			return communityAppId_;
		}
		set
		{
			communityAppId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelPermission ChannelPermission
	{
		get
		{
			return channelPermission_;
		}
		set
		{
			channelPermission_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp LastActivityAt
	{
		get
		{
			return lastActivityAt_;
		}
		set
		{
			lastActivityAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp UserLastViewedAt
	{
		get
		{
			return userLastViewedAt_;
		}
		set
		{
			userLastViewedAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool UseChannelGroupPermission
	{
		get
		{
			return useChannelGroupPermission_;
		}
		set
		{
			useChannelGroupPermission_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<WebRtcUserDevicePacket> WebRtcMembers => webRtcMembers_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<RoleOrMemberUuid> RoleOrMemberIds => roleOrMemberIds_;

	[GeneratedCode("protoc", null)]
	public ChannelResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public ChannelResponse(ChannelResponse other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		channelGroupId_ = ((other.channelGroupId_ != null) ? other.channelGroupId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		name_ = other.name_;
		Description = other.Description;
		IconAssetUri = other.IconAssetUri;
		channelType_ = other.channelType_;
		position_ = other.position_;
		communityAppId_ = ((other.communityAppId_ != null) ? other.communityAppId_.Clone() : null);
		channelPermission_ = ((other.channelPermission_ != null) ? other.channelPermission_.Clone() : null);
		lastActivityAt_ = ((other.lastActivityAt_ != null) ? other.lastActivityAt_.Clone() : null);
		userLastViewedAt_ = ((other.userLastViewedAt_ != null) ? other.userLastViewedAt_.Clone() : null);
		useChannelGroupPermission_ = other.useChannelGroupPermission_;
		webRtcMembers_ = other.webRtcMembers_.Clone();
		roleOrMemberIds_ = other.roleOrMemberIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ChannelResponse Clone()
	{
		return new ChannelResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChannelResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ChannelResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(ChannelGroupId, other.ChannelGroupId))
		{
			return false;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (Name != other.Name)
		{
			return false;
		}
		if (Description != other.Description)
		{
			return false;
		}
		if (IconAssetUri != other.IconAssetUri)
		{
			return false;
		}
		if (ChannelType != other.ChannelType)
		{
			return false;
		}
		if (!ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Position, other.Position))
		{
			return false;
		}
		if (!object.Equals(CommunityAppId, other.CommunityAppId))
		{
			return false;
		}
		if (!object.Equals(ChannelPermission, other.ChannelPermission))
		{
			return false;
		}
		if (!object.Equals(LastActivityAt, other.LastActivityAt))
		{
			return false;
		}
		if (!object.Equals(UserLastViewedAt, other.UserLastViewedAt))
		{
			return false;
		}
		if (UseChannelGroupPermission != other.UseChannelGroupPermission)
		{
			return false;
		}
		if (!webRtcMembers_.Equals(other.webRtcMembers_))
		{
			return false;
		}
		if (!roleOrMemberIds_.Equals(other.roleOrMemberIds_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (channelGroupId_ != null)
		{
			num ^= ChannelGroupId.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (description_ != null)
		{
			num ^= Description.GetHashCode();
		}
		if (iconAssetUri_ != null)
		{
			num ^= IconAssetUri.GetHashCode();
		}
		if (ChannelType != 0)
		{
			num ^= ChannelType.GetHashCode();
		}
		if (Position != 0f)
		{
			num ^= ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Position);
		}
		if (communityAppId_ != null)
		{
			num ^= CommunityAppId.GetHashCode();
		}
		if (channelPermission_ != null)
		{
			num ^= ChannelPermission.GetHashCode();
		}
		if (lastActivityAt_ != null)
		{
			num ^= LastActivityAt.GetHashCode();
		}
		if (userLastViewedAt_ != null)
		{
			num ^= UserLastViewedAt.GetHashCode();
		}
		if (UseChannelGroupPermission)
		{
			num ^= UseChannelGroupPermission.GetHashCode();
		}
		num ^= webRtcMembers_.GetHashCode();
		num ^= roleOrMemberIds_.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityId);
		}
		if (channelGroupId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(ChannelGroupId);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(Id);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(106);
			P_0.WriteString(Name);
		}
		if (description_ != null)
		{
			_single_description_codec.WriteTagAndValue(ref P_0, Description);
		}
		if (iconAssetUri_ != null)
		{
			_single_iconAssetUri_codec.WriteTagAndValue(ref P_0, IconAssetUri);
		}
		if (ChannelType != 0)
		{
			P_0.WriteRawTag(128, 1);
			P_0.WriteInt32(ChannelType);
		}
		if (Position != 0f)
		{
			P_0.WriteRawTag(141, 1);
			P_0.WriteFloat(Position);
		}
		if (communityAppId_ != null)
		{
			P_0.WriteRawTag(146, 1);
			P_0.WriteMessage(CommunityAppId);
		}
		if (channelPermission_ != null)
		{
			P_0.WriteRawTag(154, 1);
			P_0.WriteMessage(ChannelPermission);
		}
		if (lastActivityAt_ != null)
		{
			P_0.WriteRawTag(162, 1);
			P_0.WriteMessage(LastActivityAt);
		}
		if (userLastViewedAt_ != null)
		{
			P_0.WriteRawTag(170, 1);
			P_0.WriteMessage(UserLastViewedAt);
		}
		if (UseChannelGroupPermission)
		{
			P_0.WriteRawTag(176, 1);
			P_0.WriteBool(UseChannelGroupPermission);
		}
		webRtcMembers_.WriteTo(ref P_0, _repeated_webRtcMembers_codec);
		roleOrMemberIds_.WriteTo(ref P_0, _repeated_roleOrMemberIds_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (channelGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelGroupId);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (description_ != null)
		{
			num += _single_description_codec.CalculateSizeWithTag(Description);
		}
		if (iconAssetUri_ != null)
		{
			num += _single_iconAssetUri_codec.CalculateSizeWithTag(IconAssetUri);
		}
		if (ChannelType != 0)
		{
			num += 2 + CodedOutputStream.ComputeInt32Size(ChannelType);
		}
		if (Position != 0f)
		{
			num += 6;
		}
		if (communityAppId_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityAppId);
		}
		if (channelPermission_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelPermission);
		}
		if (lastActivityAt_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(LastActivityAt);
		}
		if (userLastViewedAt_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UserLastViewedAt);
		}
		if (UseChannelGroupPermission)
		{
			num += 3;
		}
		num += webRtcMembers_.CalculateSize(_repeated_webRtcMembers_codec);
		num += roleOrMemberIds_.CalculateSize(_repeated_roleOrMemberIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChannelResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		if (other.channelGroupId_ != null)
		{
			if (channelGroupId_ == null)
			{
				ChannelGroupId = new ChannelGroupUuid();
			}
			ChannelGroupId.MergeFrom(other.ChannelGroupId);
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new ChannelUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.description_ != null && (description_ == null || other.Description != ""))
		{
			Description = other.Description;
		}
		if (other.iconAssetUri_ != null && (iconAssetUri_ == null || other.IconAssetUri != ""))
		{
			IconAssetUri = other.IconAssetUri;
		}
		if (other.ChannelType != 0)
		{
			ChannelType = other.ChannelType;
		}
		if (other.Position != 0f)
		{
			Position = other.Position;
		}
		if (other.communityAppId_ != null)
		{
			if (communityAppId_ == null)
			{
				CommunityAppId = new CommunityAppUuid();
			}
			CommunityAppId.MergeFrom(other.CommunityAppId);
		}
		if (other.channelPermission_ != null)
		{
			if (channelPermission_ == null)
			{
				ChannelPermission = new ChannelPermission();
			}
			ChannelPermission.MergeFrom(other.ChannelPermission);
		}
		if (other.lastActivityAt_ != null)
		{
			if (lastActivityAt_ == null)
			{
				LastActivityAt = new Timestamp();
			}
			LastActivityAt.MergeFrom(other.LastActivityAt);
		}
		if (other.userLastViewedAt_ != null)
		{
			if (userLastViewedAt_ == null)
			{
				UserLastViewedAt = new Timestamp();
			}
			UserLastViewedAt.MergeFrom(other.UserLastViewedAt);
		}
		if (other.UseChannelGroupPermission)
		{
			UseChannelGroupPermission = other.UseChannelGroupPermission;
		}
		webRtcMembers_.Add(other.webRtcMembers_);
		roleOrMemberIds_.Add(other.roleOrMemberIds_);
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
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 90u:
				if (channelGroupId_ == null)
				{
					ChannelGroupId = new ChannelGroupUuid();
				}
				P_0.ReadMessage(ChannelGroupId);
				break;
			case 98u:
				if (id_ == null)
				{
					Id = new ChannelUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 106u:
				Name = P_0.ReadString();
				break;
			case 114u:
			{
				string text2 = _single_description_codec.Read(ref P_0);
				if (description_ == null || text2 != "")
				{
					Description = text2;
				}
				break;
			}
			case 122u:
			{
				string text = _single_iconAssetUri_codec.Read(ref P_0);
				if (iconAssetUri_ == null || text != "")
				{
					IconAssetUri = text;
				}
				break;
			}
			case 128u:
				ChannelType = P_0.ReadInt32();
				break;
			case 141u:
				Position = P_0.ReadFloat();
				break;
			case 146u:
				if (communityAppId_ == null)
				{
					CommunityAppId = new CommunityAppUuid();
				}
				P_0.ReadMessage(CommunityAppId);
				break;
			case 154u:
				if (channelPermission_ == null)
				{
					ChannelPermission = new ChannelPermission();
				}
				P_0.ReadMessage(ChannelPermission);
				break;
			case 162u:
				if (lastActivityAt_ == null)
				{
					LastActivityAt = new Timestamp();
				}
				P_0.ReadMessage(LastActivityAt);
				break;
			case 170u:
				if (userLastViewedAt_ == null)
				{
					UserLastViewedAt = new Timestamp();
				}
				P_0.ReadMessage(UserLastViewedAt);
				break;
			case 176u:
				UseChannelGroupPermission = P_0.ReadBool();
				break;
			case 186u:
				webRtcMembers_.AddEntriesFrom(ref P_0, _repeated_webRtcMembers_codec);
				break;
			case 194u:
				roleOrMemberIds_.AddEntriesFrom(ref P_0, _repeated_roleOrMemberIds_codec);
				break;
			}
		}
	}
}
