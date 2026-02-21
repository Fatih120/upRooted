using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadChannelState : IMessage, IMessage<CommunityLogPayloadChannelState>, IEquatable<CommunityLogPayloadChannelState>, IDeepCloneable<CommunityLogPayloadChannelState>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadChannelState> _parser = new MessageParser<CommunityLogPayloadChannelState>(() => new CommunityLogPayloadChannelState());

	private UnknownFieldSet _unknownFields;

	private ChannelGroupUuid channelGroupId_;

	private string channelGroupName_ = "";

	private bool useChannelGroupPermission_;

	private string iconAssetUri_ = "";

	private string name_ = "";

	private string description_ = "";

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadChannelState> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[24];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public string ChannelGroupName
	{
		get
		{
			return channelGroupName_;
		}
		set
		{
			channelGroupName_ = ProtoPreconditions.CheckNotNull(value, "value");
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
	public string IconAssetUri
	{
		get
		{
			return iconAssetUri_;
		}
		set
		{
			iconAssetUri_ = ProtoPreconditions.CheckNotNull(value, "value");
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
			description_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadChannelState()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadChannelState(CommunityLogPayloadChannelState other)
		: this()
	{
		channelGroupId_ = ((other.channelGroupId_ != null) ? other.channelGroupId_.Clone() : null);
		channelGroupName_ = other.channelGroupName_;
		useChannelGroupPermission_ = other.useChannelGroupPermission_;
		iconAssetUri_ = other.iconAssetUri_;
		name_ = other.name_;
		description_ = other.description_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadChannelState Clone()
	{
		return new CommunityLogPayloadChannelState(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadChannelState);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadChannelState other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(ChannelGroupId, other.ChannelGroupId))
		{
			return false;
		}
		if (ChannelGroupName != other.ChannelGroupName)
		{
			return false;
		}
		if (UseChannelGroupPermission != other.UseChannelGroupPermission)
		{
			return false;
		}
		if (IconAssetUri != other.IconAssetUri)
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
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (channelGroupId_ != null)
		{
			num ^= ChannelGroupId.GetHashCode();
		}
		if (ChannelGroupName.Length != 0)
		{
			num ^= ChannelGroupName.GetHashCode();
		}
		if (UseChannelGroupPermission)
		{
			num ^= UseChannelGroupPermission.GetHashCode();
		}
		if (IconAssetUri.Length != 0)
		{
			num ^= IconAssetUri.GetHashCode();
		}
		if (Name.Length != 0)
		{
			num ^= Name.GetHashCode();
		}
		if (Description.Length != 0)
		{
			num ^= Description.GetHashCode();
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
		if (channelGroupId_ != null)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(ChannelGroupId);
		}
		if (ChannelGroupName.Length != 0)
		{
			P_0.WriteRawTag(18);
			P_0.WriteString(ChannelGroupName);
		}
		if (UseChannelGroupPermission)
		{
			P_0.WriteRawTag(24);
			P_0.WriteBool(UseChannelGroupPermission);
		}
		if (IconAssetUri.Length != 0)
		{
			P_0.WriteRawTag(34);
			P_0.WriteString(IconAssetUri);
		}
		if (Name.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(Name);
		}
		if (Description.Length != 0)
		{
			P_0.WriteRawTag(50);
			P_0.WriteString(Description);
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
		if (channelGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelGroupId);
		}
		if (ChannelGroupName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ChannelGroupName);
		}
		if (UseChannelGroupPermission)
		{
			num += 2;
		}
		if (IconAssetUri.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(IconAssetUri);
		}
		if (Name.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Name);
		}
		if (Description.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(Description);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadChannelState other)
	{
		if (other == null)
		{
			return;
		}
		if (other.channelGroupId_ != null)
		{
			if (channelGroupId_ == null)
			{
				ChannelGroupId = new ChannelGroupUuid();
			}
			ChannelGroupId.MergeFrom(other.ChannelGroupId);
		}
		if (other.ChannelGroupName.Length != 0)
		{
			ChannelGroupName = other.ChannelGroupName;
		}
		if (other.UseChannelGroupPermission)
		{
			UseChannelGroupPermission = other.UseChannelGroupPermission;
		}
		if (other.IconAssetUri.Length != 0)
		{
			IconAssetUri = other.IconAssetUri;
		}
		if (other.Name.Length != 0)
		{
			Name = other.Name;
		}
		if (other.Description.Length != 0)
		{
			Description = other.Description;
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
				if (channelGroupId_ == null)
				{
					ChannelGroupId = new ChannelGroupUuid();
				}
				P_0.ReadMessage(ChannelGroupId);
				break;
			case 18u:
				ChannelGroupName = P_0.ReadString();
				break;
			case 24u:
				UseChannelGroupPermission = P_0.ReadBool();
				break;
			case 34u:
				IconAssetUri = P_0.ReadString();
				break;
			case 42u:
				Name = P_0.ReadString();
				break;
			case 50u:
				Description = P_0.ReadString();
				break;
			}
		}
	}
}
