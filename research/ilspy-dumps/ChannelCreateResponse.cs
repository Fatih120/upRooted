using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class ChannelCreateResponse : IMessage<ChannelCreateResponse>, IMessage, IEquatable<ChannelCreateResponse>, IDeepCloneable<ChannelCreateResponse>, IBufferMessage
{
	private static readonly MessageParser<ChannelCreateResponse> _parser = new MessageParser<ChannelCreateResponse>(() => new ChannelCreateResponse());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private ChannelGroupUuid channelGroupId_;

	private ChannelUuid id_;

	private string name_ = "";

	private static readonly FieldCodec<string> _single_description_codec = FieldCodec.ForClassWrapper<string>(66u);

	private string description_;

	private static readonly FieldCodec<string> _single_iconAssetUri_codec = FieldCodec.ForClassWrapper<string>(74u);

	private string iconAssetUri_;

	private ChannelUuid beforeChannelId_;

	private bool useChannelGroupPermission_;

	private ChannelPermission channelPermission_;

	private int channelType_;

	private CommunityAppUuid communityAppId_;

	private static readonly FieldCodec<RoleOrMemberUuid> _repeated_roleOrMemberIds_codec = FieldCodec.ForMessage(122u, RoleOrMemberUuid.Parser);

	private readonly RepeatedField<RoleOrMemberUuid> roleOrMemberIds_ = new RepeatedField<RoleOrMemberUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<ChannelCreateResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChannelReflection.Descriptor.MessageTypes[1];

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
	public ChannelUuid BeforeChannelId
	{
		get
		{
			return beforeChannelId_;
		}
		set
		{
			beforeChannelId_ = value;
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
	public RepeatedField<RoleOrMemberUuid> RoleOrMemberIds => roleOrMemberIds_;

	[GeneratedCode("protoc", null)]
	public ChannelCreateResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public ChannelCreateResponse(ChannelCreateResponse other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		channelGroupId_ = ((other.channelGroupId_ != null) ? other.channelGroupId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		name_ = other.name_;
		Description = other.Description;
		IconAssetUri = other.IconAssetUri;
		beforeChannelId_ = ((other.beforeChannelId_ != null) ? other.beforeChannelId_.Clone() : null);
		useChannelGroupPermission_ = other.useChannelGroupPermission_;
		channelPermission_ = ((other.channelPermission_ != null) ? other.channelPermission_.Clone() : null);
		channelType_ = other.channelType_;
		communityAppId_ = ((other.communityAppId_ != null) ? other.communityAppId_.Clone() : null);
		roleOrMemberIds_ = other.roleOrMemberIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ChannelCreateResponse Clone()
	{
		return new ChannelCreateResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChannelCreateResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ChannelCreateResponse other)
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
		if (!object.Equals(BeforeChannelId, other.BeforeChannelId))
		{
			return false;
		}
		if (UseChannelGroupPermission != other.UseChannelGroupPermission)
		{
			return false;
		}
		if (!object.Equals(ChannelPermission, other.ChannelPermission))
		{
			return false;
		}
		if (ChannelType != other.ChannelType)
		{
			return false;
		}
		if (!object.Equals(CommunityAppId, other.CommunityAppId))
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
		if (beforeChannelId_ != null)
		{
			num ^= BeforeChannelId.GetHashCode();
		}
		if (UseChannelGroupPermission)
		{
			num ^= UseChannelGroupPermission.GetHashCode();
		}
		if (channelPermission_ != null)
		{
			num ^= ChannelPermission.GetHashCode();
		}
		if (ChannelType != 0)
		{
			num ^= ChannelType.GetHashCode();
		}
		if (communityAppId_ != null)
		{
			num ^= CommunityAppId.GetHashCode();
		}
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
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CommunityId);
		}
		if (channelGroupId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(ChannelGroupId);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(Id);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(58);
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
		if (beforeChannelId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(BeforeChannelId);
		}
		if (UseChannelGroupPermission)
		{
			P_0.WriteRawTag(88);
			P_0.WriteBool(UseChannelGroupPermission);
		}
		if (channelPermission_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(ChannelPermission);
		}
		if (ChannelType != 0)
		{
			P_0.WriteRawTag(104);
			P_0.WriteInt32(ChannelType);
		}
		if (communityAppId_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(CommunityAppId);
		}
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
		if (beforeChannelId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeforeChannelId);
		}
		if (UseChannelGroupPermission)
		{
			num += 2;
		}
		if (channelPermission_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelPermission);
		}
		if (ChannelType != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(ChannelType);
		}
		if (communityAppId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityAppId);
		}
		num += roleOrMemberIds_.CalculateSize(_repeated_roleOrMemberIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChannelCreateResponse other)
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
		if (other.beforeChannelId_ != null)
		{
			if (beforeChannelId_ == null)
			{
				BeforeChannelId = new ChannelUuid();
			}
			BeforeChannelId.MergeFrom(other.BeforeChannelId);
		}
		if (other.UseChannelGroupPermission)
		{
			UseChannelGroupPermission = other.UseChannelGroupPermission;
		}
		if (other.channelPermission_ != null)
		{
			if (channelPermission_ == null)
			{
				ChannelPermission = new ChannelPermission();
			}
			ChannelPermission.MergeFrom(other.ChannelPermission);
		}
		if (other.ChannelType != 0)
		{
			ChannelType = other.ChannelType;
		}
		if (other.communityAppId_ != null)
		{
			if (communityAppId_ == null)
			{
				CommunityAppId = new CommunityAppUuid();
			}
			CommunityAppId.MergeFrom(other.CommunityAppId);
		}
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
			case 34u:
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 42u:
				if (channelGroupId_ == null)
				{
					ChannelGroupId = new ChannelGroupUuid();
				}
				P_0.ReadMessage(ChannelGroupId);
				break;
			case 50u:
				if (id_ == null)
				{
					Id = new ChannelUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 58u:
				Name = P_0.ReadString();
				break;
			case 66u:
			{
				string text = _single_description_codec.Read(ref P_0);
				if (description_ == null || text != "")
				{
					Description = text;
				}
				break;
			}
			case 74u:
			{
				string text2 = _single_iconAssetUri_codec.Read(ref P_0);
				if (iconAssetUri_ == null || text2 != "")
				{
					IconAssetUri = text2;
				}
				break;
			}
			case 82u:
				if (beforeChannelId_ == null)
				{
					BeforeChannelId = new ChannelUuid();
				}
				P_0.ReadMessage(BeforeChannelId);
				break;
			case 88u:
				UseChannelGroupPermission = P_0.ReadBool();
				break;
			case 98u:
				if (channelPermission_ == null)
				{
					ChannelPermission = new ChannelPermission();
				}
				P_0.ReadMessage(ChannelPermission);
				break;
			case 104u:
				ChannelType = P_0.ReadInt32();
				break;
			case 114u:
				if (communityAppId_ == null)
				{
					CommunityAppId = new CommunityAppUuid();
				}
				P_0.ReadMessage(CommunityAppId);
				break;
			case 122u:
				roleOrMemberIds_.AddEntriesFrom(ref P_0, _repeated_roleOrMemberIds_codec);
				break;
			}
		}
	}
}
