using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadCommunity : IMessage, IMessage<CommunityLogPayloadCommunity>, IEquatable<CommunityLogPayloadCommunity>, IDeepCloneable<CommunityLogPayloadCommunity>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadCommunity> _parser = new MessageParser<CommunityLogPayloadCommunity>(() => new CommunityLogPayloadCommunity());

	private UnknownFieldSet _unknownFields;

	private CommunityLogAction communityLogAction_ = CommunityLogAction.Unspecified;

	private CommunityUuid id_;

	private CommunityLogPayloadCommunityState original_;

	private CommunityLogPayloadCommunityState current_;

	private FieldMask fieldMask_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadCommunity> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[16];

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
	public CommunityLogPayloadCommunityState Original
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
	public CommunityLogPayloadCommunityState Current
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
			Community = this
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
			if (Original.OwnerUserId != Current.OwnerUserId)
			{
				FieldMask.Paths.Add("owner_user_id");
				FieldMask.Paths.Add("owner_username");
			}
			if (Original.DefaultChannelId != Current.DefaultChannelId)
			{
				FieldMask.Paths.Add("default_channel_id");
				FieldMask.Paths.Add("default_channel_name");
			}
			if (Original.PictureHex != Current.PictureHex)
			{
				FieldMask.Paths.Add("picture_hex");
			}
			if (Original.PictureAssetUri != Current.PictureAssetUri)
			{
				FieldMask.Paths.Add("picture_asset_uri");
			}
			if (Original.RejectUnverifiedEmail != Current.RejectUnverifiedEmail)
			{
				FieldMask.Paths.Add("reject_unverified_email");
			}
			if (!object.Equals(Original.JoinThrottle, Current.JoinThrottle))
			{
				FieldMask.Paths.Add("join_throttle");
			}
		}
	}

	public bool HasChanges()
	{
		CreateFieldMask();
		return FieldMask == null || FieldMask.Paths.Count > 0;
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadCommunity()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadCommunity(CommunityLogPayloadCommunity other)
		: this()
	{
		communityLogAction_ = other.communityLogAction_;
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		original_ = ((other.original_ != null) ? other.original_.Clone() : null);
		current_ = ((other.current_ != null) ? other.current_.Clone() : null);
		fieldMask_ = ((other.fieldMask_ != null) ? other.fieldMask_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadCommunity Clone()
	{
		return new CommunityLogPayloadCommunity(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadCommunity);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadCommunity other)
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
		if (original_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(Original);
		}
		if (current_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(Current);
		}
		if (fieldMask_ != null)
		{
			P_0.WriteRawTag(58);
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
	public void MergeFrom(CommunityLogPayloadCommunity other)
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
				Id = new CommunityUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.original_ != null)
		{
			if (original_ == null)
			{
				Original = new CommunityLogPayloadCommunityState();
			}
			Original.MergeFrom(other.Original);
		}
		if (other.current_ != null)
		{
			if (current_ == null)
			{
				Current = new CommunityLogPayloadCommunityState();
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
					Id = new CommunityUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (original_ == null)
				{
					Original = new CommunityLogPayloadCommunityState();
				}
				P_0.ReadMessage(Original);
				break;
			case 50u:
				if (current_ == null)
				{
					Current = new CommunityLogPayloadCommunityState();
				}
				P_0.ReadMessage(Current);
				break;
			case 58u:
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
