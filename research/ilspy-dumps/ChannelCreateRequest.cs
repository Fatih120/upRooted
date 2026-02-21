using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class ChannelCreateRequest : IMessage<ChannelCreateRequest>, IMessage, IEquatable<ChannelCreateRequest>, IDeepCloneable<ChannelCreateRequest>, IBufferMessage
{
	private static readonly MessageParser<ChannelCreateRequest> _parser = new MessageParser<ChannelCreateRequest>(() => new ChannelCreateRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private ChannelGroupUuid channelGroupId_;

	private string name_ = "";

	private static readonly FieldCodec<string> _single_description_codec = FieldCodec.ForClassWrapper<string>(106u);

	private string description_;

	private int channelType_;

	private bool useChannelGroupPermission_;

	private static readonly FieldCodec<string> _single_iconTokenUri_codec = FieldCodec.ForClassWrapper<string>(130u);

	private string iconTokenUri_;

	private static readonly FieldCodec<AccessRuleCreateRoleOrMemberRequest> _repeated_accessRuleCreates_codec = FieldCodec.ForMessage(138u, AccessRuleCreateRoleOrMemberRequest.Parser);

	private readonly RepeatedField<AccessRuleCreateRoleOrMemberRequest> accessRuleCreates_ = new RepeatedField<AccessRuleCreateRoleOrMemberRequest>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<ChannelCreateRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChannelReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public RootContext Context
	{
		get
		{
			return context_;
		}
		set
		{
			context_ = value;
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
	public string IconTokenUri
	{
		get
		{
			return iconTokenUri_;
		}
		set
		{
			iconTokenUri_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<AccessRuleCreateRoleOrMemberRequest> AccessRuleCreates => accessRuleCreates_;

	[GeneratedCode("protoc", null)]
	public ChannelCreateRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public ChannelCreateRequest(ChannelCreateRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		channelGroupId_ = ((other.channelGroupId_ != null) ? other.channelGroupId_.Clone() : null);
		name_ = other.name_;
		Description = other.Description;
		channelType_ = other.channelType_;
		useChannelGroupPermission_ = other.useChannelGroupPermission_;
		IconTokenUri = other.IconTokenUri;
		accessRuleCreates_ = other.accessRuleCreates_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ChannelCreateRequest Clone()
	{
		return new ChannelCreateRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChannelCreateRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ChannelCreateRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Context, other.Context))
		{
			return false;
		}
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(ChannelGroupId, other.ChannelGroupId))
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
		if (ChannelType != other.ChannelType)
		{
			return false;
		}
		if (UseChannelGroupPermission != other.UseChannelGroupPermission)
		{
			return false;
		}
		if (IconTokenUri != other.IconTokenUri)
		{
			return false;
		}
		if (!accessRuleCreates_.Equals(other.accessRuleCreates_))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (context_ != null)
		{
			num ^= Context.GetHashCode();
		}
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (channelGroupId_ != null)
		{
			num ^= ChannelGroupId.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (description_ != null)
		{
			num ^= Description.GetHashCode();
		}
		if (ChannelType != 0)
		{
			num ^= ChannelType.GetHashCode();
		}
		if (UseChannelGroupPermission)
		{
			num ^= UseChannelGroupPermission.GetHashCode();
		}
		if (iconTokenUri_ != null)
		{
			num ^= IconTokenUri.GetHashCode();
		}
		num ^= accessRuleCreates_.GetHashCode();
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
		if (context_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Context);
		}
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
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(98);
			P_0.WriteString(Name);
		}
		if (description_ != null)
		{
			_single_description_codec.WriteTagAndValue(ref P_0, Description);
		}
		if (ChannelType != 0)
		{
			P_0.WriteRawTag(112);
			P_0.WriteInt32(ChannelType);
		}
		if (UseChannelGroupPermission)
		{
			P_0.WriteRawTag(120);
			P_0.WriteBool(UseChannelGroupPermission);
		}
		if (iconTokenUri_ != null)
		{
			_single_iconTokenUri_codec.WriteTagAndValue(ref P_0, IconTokenUri);
		}
		accessRuleCreates_.WriteTo(ref P_0, _repeated_accessRuleCreates_codec);
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref P_0);
		}
	}

	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (context_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Context);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (channelGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelGroupId);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (description_ != null)
		{
			num += _single_description_codec.CalculateSizeWithTag(Description);
		}
		if (ChannelType != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(ChannelType);
		}
		if (UseChannelGroupPermission)
		{
			num += 2;
		}
		if (iconTokenUri_ != null)
		{
			num += _single_iconTokenUri_codec.CalculateSizeWithTag(IconTokenUri);
		}
		num += accessRuleCreates_.CalculateSize(_repeated_accessRuleCreates_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChannelCreateRequest other)
	{
		if (other == null)
		{
			return;
		}
		if (other.context_ != null)
		{
			if (context_ == null)
			{
				Context = new RootContext();
			}
			Context.MergeFrom(other.Context);
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
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.description_ != null && (description_ == null || other.Description != ""))
		{
			Description = other.Description;
		}
		if (other.ChannelType != 0)
		{
			ChannelType = other.ChannelType;
		}
		if (other.UseChannelGroupPermission)
		{
			UseChannelGroupPermission = other.UseChannelGroupPermission;
		}
		if (other.iconTokenUri_ != null && (iconTokenUri_ == null || other.IconTokenUri != ""))
		{
			IconTokenUri = other.IconTokenUri;
		}
		accessRuleCreates_.Add(other.accessRuleCreates_);
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
			case 10u:
				if (context_ == null)
				{
					Context = new RootContext();
				}
				P_0.ReadMessage(Context);
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
				Name = P_0.ReadString();
				break;
			case 106u:
			{
				string text2 = _single_description_codec.Read(ref P_0);
				if (description_ == null || text2 != "")
				{
					Description = text2;
				}
				break;
			}
			case 112u:
				ChannelType = P_0.ReadInt32();
				break;
			case 120u:
				UseChannelGroupPermission = P_0.ReadBool();
				break;
			case 130u:
			{
				string text = _single_iconTokenUri_codec.Read(ref P_0);
				if (iconTokenUri_ == null || text != "")
				{
					IconTokenUri = text;
				}
				break;
			}
			case 138u:
				accessRuleCreates_.AddEntriesFrom(ref P_0, _repeated_accessRuleCreates_codec);
				break;
			}
		}
	}
}
