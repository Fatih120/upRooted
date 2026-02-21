using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class ChannelEditRequest : IMessage<ChannelEditRequest>, IMessage, IEquatable<ChannelEditRequest>, IDeepCloneable<ChannelEditRequest>, IBufferMessage
{
	private static readonly MessageParser<ChannelEditRequest> _parser = new MessageParser<ChannelEditRequest>(() => new ChannelEditRequest());

	private UnknownFieldSet _unknownFields;

	private RootContext context_;

	private CommunityUuid communityId_;

	private ChannelUuid id_;

	private string name_ = "";

	private static readonly FieldCodec<string> _single_description_codec = FieldCodec.ForClassWrapper<string>(106u);

	private string description_;

	private bool updateIcon_;

	private static readonly FieldCodec<string> _single_iconTokenUri_codec = FieldCodec.ForClassWrapper<string>(122u);

	private string iconTokenUri_;

	private bool useChannelGroupPermission_;

	private AccessRuleUpdateRequest accessRuleUpdate_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<ChannelEditRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChannelReflection.Descriptor.MessageTypes[3];

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
	public bool UpdateIcon
	{
		get
		{
			return updateIcon_;
		}
		set
		{
			updateIcon_ = value;
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
	public AccessRuleUpdateRequest AccessRuleUpdate
	{
		get
		{
			return accessRuleUpdate_;
		}
		set
		{
			accessRuleUpdate_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelEditRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public ChannelEditRequest(ChannelEditRequest other)
		: this()
	{
		context_ = ((other.context_ != null) ? other.context_.Clone() : null);
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		name_ = other.name_;
		Description = other.Description;
		updateIcon_ = other.updateIcon_;
		IconTokenUri = other.IconTokenUri;
		useChannelGroupPermission_ = other.useChannelGroupPermission_;
		accessRuleUpdate_ = ((other.accessRuleUpdate_ != null) ? other.accessRuleUpdate_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ChannelEditRequest Clone()
	{
		return new ChannelEditRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChannelEditRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ChannelEditRequest other)
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
		if (UpdateIcon != other.UpdateIcon)
		{
			return false;
		}
		if (IconTokenUri != other.IconTokenUri)
		{
			return false;
		}
		if (UseChannelGroupPermission != other.UseChannelGroupPermission)
		{
			return false;
		}
		if (!object.Equals(AccessRuleUpdate, other.AccessRuleUpdate))
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
		if (UpdateIcon)
		{
			num ^= UpdateIcon.GetHashCode();
		}
		if (iconTokenUri_ != null)
		{
			num ^= IconTokenUri.GetHashCode();
		}
		if (UseChannelGroupPermission)
		{
			num ^= UseChannelGroupPermission.GetHashCode();
		}
		if (accessRuleUpdate_ != null)
		{
			num ^= AccessRuleUpdate.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(Id);
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
		if (UpdateIcon)
		{
			P_0.WriteRawTag(112);
			P_0.WriteBool(UpdateIcon);
		}
		if (iconTokenUri_ != null)
		{
			_single_iconTokenUri_codec.WriteTagAndValue(ref P_0, IconTokenUri);
		}
		if (UseChannelGroupPermission)
		{
			P_0.WriteRawTag(128, 1);
			P_0.WriteBool(UseChannelGroupPermission);
		}
		if (accessRuleUpdate_ != null)
		{
			P_0.WriteRawTag(138, 1);
			P_0.WriteMessage(AccessRuleUpdate);
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
		if (context_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Context);
		}
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
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
		if (UpdateIcon)
		{
			num += 2;
		}
		if (iconTokenUri_ != null)
		{
			num += _single_iconTokenUri_codec.CalculateSizeWithTag(IconTokenUri);
		}
		if (UseChannelGroupPermission)
		{
			num += 3;
		}
		if (accessRuleUpdate_ != null)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(AccessRuleUpdate);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChannelEditRequest other)
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
		if (other.UpdateIcon)
		{
			UpdateIcon = other.UpdateIcon;
		}
		if (other.iconTokenUri_ != null && (iconTokenUri_ == null || other.IconTokenUri != ""))
		{
			IconTokenUri = other.IconTokenUri;
		}
		if (other.UseChannelGroupPermission)
		{
			UseChannelGroupPermission = other.UseChannelGroupPermission;
		}
		if (other.accessRuleUpdate_ != null)
		{
			if (accessRuleUpdate_ == null)
			{
				AccessRuleUpdate = new AccessRuleUpdateRequest();
			}
			AccessRuleUpdate.MergeFrom(other.AccessRuleUpdate);
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
				if (id_ == null)
				{
					Id = new ChannelUuid();
				}
				P_0.ReadMessage(Id);
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
				UpdateIcon = P_0.ReadBool();
				break;
			case 122u:
			{
				string text = _single_iconTokenUri_codec.Read(ref P_0);
				if (iconTokenUri_ == null || text != "")
				{
					IconTokenUri = text;
				}
				break;
			}
			case 128u:
				UseChannelGroupPermission = P_0.ReadBool();
				break;
			case 138u:
				if (accessRuleUpdate_ == null)
				{
					AccessRuleUpdate = new AccessRuleUpdateRequest();
				}
				P_0.ReadMessage(AccessRuleUpdate);
				break;
			}
		}
	}
}
