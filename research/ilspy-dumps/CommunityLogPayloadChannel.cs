using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadChannel : IMessage, IMessage<CommunityLogPayloadChannel>, IEquatable<CommunityLogPayloadChannel>, IDeepCloneable<CommunityLogPayloadChannel>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadChannel> _parser = new MessageParser<CommunityLogPayloadChannel>(() => new CommunityLogPayloadChannel());

	private UnknownFieldSet _unknownFields;

	private CommunityLogAction communityLogAction_ = CommunityLogAction.Unspecified;

	private ChannelUuid id_;

	private int channelType_;

	private CommunityLogPayloadChannelState original_;

	private CommunityLogPayloadChannelState current_;

	private FieldMask fieldMask_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadChannel> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[23];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityLogAction CommunityLogAction
	{
		get
		{
			return communityLogAction_;
		}
		set
		{
			communityLogAction_ = value;
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
	public CommunityLogPayloadChannelState Original
	{
		get
		{
			return original_;
		}
		set
		{
			original_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadChannelState Current
	{
		get
		{
			return current_;
		}
		set
		{
			current_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FieldMask FieldMask
	{
		get
		{
			return fieldMask_;
		}
		set
		{
			fieldMask_ = value;
		}
	}

	public CommunityLogPayloadItem ToPayloadItem()
	{
		return new CommunityLogPayloadItem
		{
			Channel = this
		};
	}

	public void CreateFieldMask()
	{
		if (Current != null && Original != null)
		{
			FieldMask = new FieldMask();
			if (Original.Name != Current.Name)
			{
				FieldMask.Paths.Add("name");
			}
			if (Original.ChannelGroupId != Current.ChannelGroupId)
			{
				FieldMask.Paths.Add("channel_group_id");
				FieldMask.Paths.Add("channel_group_name");
			}
			if (Original.Description != Current.Description)
			{
				FieldMask.Paths.Add("description");
			}
			if (Original.UseChannelGroupPermission != Current.UseChannelGroupPermission)
			{
				FieldMask.Paths.Add("use_channel_group_permission");
			}
			if (Original.IconAssetUri != Current.IconAssetUri)
			{
				FieldMask.Paths.Add("icon_asset_uri");
			}
		}
	}

	public bool HasChanges()
	{
		CreateFieldMask();
		return FieldMask == null || FieldMask.Paths.Count > 0;
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadChannel()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadChannel(CommunityLogPayloadChannel other)
		: this()
	{
		communityLogAction_ = other.communityLogAction_;
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		channelType_ = other.channelType_;
		original_ = ((other.original_ != null) ? other.original_.Clone() : null);
		current_ = ((other.current_ != null) ? other.current_.Clone() : null);
		fieldMask_ = ((other.fieldMask_ != null) ? other.fieldMask_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadChannel Clone()
	{
		return new CommunityLogPayloadChannel(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadChannel);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadChannel other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (CommunityLogAction != other.CommunityLogAction)
		{
			return false;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (ChannelType != other.ChannelType)
		{
			return false;
		}
		if (!object.Equals(Original, other.Original))
		{
			return false;
		}
		if (!object.Equals(Current, other.Current))
		{
			return false;
		}
		if (!object.Equals(FieldMask, other.FieldMask))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (CommunityLogAction != CommunityLogAction.Unspecified)
		{
			num ^= CommunityLogAction.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (ChannelType != 0)
		{
			num ^= ChannelType.GetHashCode();
		}
		if (original_ != null)
		{
			num ^= Original.GetHashCode();
		}
		if (current_ != null)
		{
			num ^= Current.GetHashCode();
		}
		if (fieldMask_ != null)
		{
			num ^= FieldMask.GetHashCode();
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
		if (CommunityLogAction != CommunityLogAction.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)CommunityLogAction);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		if (ChannelType != 0)
		{
			P_0.WriteRawTag(40);
			P_0.WriteInt32(ChannelType);
		}
		if (original_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(Original);
		}
		if (current_ != null)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(Current);
		}
		if (fieldMask_ != null)
		{
			P_0.WriteRawTag(66);
			P_0.WriteMessage(FieldMask);
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
		if (CommunityLogAction != CommunityLogAction.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)CommunityLogAction);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (ChannelType != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt32Size(ChannelType);
		}
		if (original_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Original);
		}
		if (current_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Current);
		}
		if (fieldMask_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FieldMask);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadChannel other)
	{
		if (other == null)
		{
			return;
		}
		if (other.CommunityLogAction != CommunityLogAction.Unspecified)
		{
			CommunityLogAction = other.CommunityLogAction;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new ChannelUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.ChannelType != 0)
		{
			ChannelType = other.ChannelType;
		}
		if (other.original_ != null)
		{
			if (original_ == null)
			{
				Original = new CommunityLogPayloadChannelState();
			}
			Original.MergeFrom(other.Original);
		}
		if (other.current_ != null)
		{
			if (current_ == null)
			{
				Current = new CommunityLogPayloadChannelState();
			}
			Current.MergeFrom(other.Current);
		}
		if (other.fieldMask_ != null)
		{
			if (fieldMask_ == null)
			{
				FieldMask = new FieldMask();
			}
			FieldMask.MergeFrom(other.FieldMask);
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
			case 8u:
				CommunityLogAction = (CommunityLogAction)P_0.ReadEnum();
				break;
			case 34u:
				if (id_ == null)
				{
					Id = new ChannelUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 40u:
				ChannelType = P_0.ReadInt32();
				break;
			case 50u:
				if (original_ == null)
				{
					Original = new CommunityLogPayloadChannelState();
				}
				P_0.ReadMessage(Original);
				break;
			case 58u:
				if (current_ == null)
				{
					Current = new CommunityLogPayloadChannelState();
				}
				P_0.ReadMessage(Current);
				break;
			case 66u:
				if (fieldMask_ == null)
				{
					FieldMask = new FieldMask();
				}
				P_0.ReadMessage(FieldMask);
				break;
			}
		}
	}
}
