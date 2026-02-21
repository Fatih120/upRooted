using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadAccessRule : IMessage, IMessage<CommunityLogPayloadAccessRule>, IEquatable<CommunityLogPayloadAccessRule>, IDeepCloneable<CommunityLogPayloadAccessRule>, IBufferMessage
{
	private static readonly MessageParser<CommunityLogPayloadAccessRule> _parser = new MessageParser<CommunityLogPayloadAccessRule>(() => new CommunityLogPayloadAccessRule());

	private UnknownFieldSet _unknownFields;

	private CommunityLogAction communityLogAction_ = CommunityLogAction.Unspecified;

	private RoleOrMemberUuid roleOrMemberId_;

	private string roleOrMemberName_ = "";

	private ChannelOrChannelGroupUuid channelOrChannelGroupId_;

	private string channelOrChannelGroupName_ = "";

	private CommunityLogPayloadAccessRuleState original_;

	private CommunityLogPayloadAccessRuleState current_;

	private FieldMask fieldMask_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadAccessRule> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[7];

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
	public RoleOrMemberUuid RoleOrMemberId
	{
		get
		{
			return roleOrMemberId_;
		}
		set
		{
			roleOrMemberId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string RoleOrMemberName
	{
		get
		{
			return roleOrMemberName_;
		}
		set
		{
			roleOrMemberName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelOrChannelGroupUuid ChannelOrChannelGroupId
	{
		get
		{
			return channelOrChannelGroupId_;
		}
		set
		{
			channelOrChannelGroupId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string ChannelOrChannelGroupName
	{
		get
		{
			return channelOrChannelGroupName_;
		}
		set
		{
			channelOrChannelGroupName_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadAccessRuleState Original
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
	public CommunityLogPayloadAccessRuleState Current
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
			AccessRule = this
		};
	}

	public void CreateFieldMask()
	{
		if (Current != null && Original != null)
		{
			FieldMask = new FieldMask();
			Original.Overlay.MergeFieldMask(Current.Overlay, FieldMask, "overlay.");
		}
	}

	public bool HasChanges()
	{
		CreateFieldMask();
		return FieldMask == null || FieldMask.Paths.Count > 0;
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadAccessRule()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadAccessRule(CommunityLogPayloadAccessRule other)
		: this()
	{
		communityLogAction_ = other.communityLogAction_;
		roleOrMemberId_ = ((other.roleOrMemberId_ != null) ? other.roleOrMemberId_.Clone() : null);
		roleOrMemberName_ = other.roleOrMemberName_;
		channelOrChannelGroupId_ = ((other.channelOrChannelGroupId_ != null) ? other.channelOrChannelGroupId_.Clone() : null);
		channelOrChannelGroupName_ = other.channelOrChannelGroupName_;
		original_ = ((other.original_ != null) ? other.original_.Clone() : null);
		current_ = ((other.current_ != null) ? other.current_.Clone() : null);
		fieldMask_ = ((other.fieldMask_ != null) ? other.fieldMask_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadAccessRule Clone()
	{
		return new CommunityLogPayloadAccessRule(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityLogPayloadAccessRule);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadAccessRule other)
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
		if (!object.Equals(RoleOrMemberId, other.RoleOrMemberId))
		{
			return false;
		}
		if (RoleOrMemberName != other.RoleOrMemberName)
		{
			return false;
		}
		if (!object.Equals(ChannelOrChannelGroupId, other.ChannelOrChannelGroupId))
		{
			return false;
		}
		if (ChannelOrChannelGroupName != other.ChannelOrChannelGroupName)
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
		if (roleOrMemberId_ != null)
		{
			num ^= RoleOrMemberId.GetHashCode();
		}
		if (RoleOrMemberName.Length != 0)
		{
			num ^= RoleOrMemberName.GetHashCode();
		}
		if (channelOrChannelGroupId_ != null)
		{
			num ^= ChannelOrChannelGroupId.GetHashCode();
		}
		if (ChannelOrChannelGroupName.Length != 0)
		{
			num ^= ChannelOrChannelGroupName.GetHashCode();
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
		if (roleOrMemberId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(RoleOrMemberId);
		}
		if (RoleOrMemberName.Length != 0)
		{
			P_0.WriteRawTag(42);
			P_0.WriteString(RoleOrMemberName);
		}
		if (channelOrChannelGroupId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(ChannelOrChannelGroupId);
		}
		if (ChannelOrChannelGroupName.Length != 0)
		{
			P_0.WriteRawTag(58);
			P_0.WriteString(ChannelOrChannelGroupName);
		}
		if (original_ != null)
		{
			P_0.WriteRawTag(66);
			P_0.WriteMessage(Original);
		}
		if (current_ != null)
		{
			P_0.WriteRawTag(74);
			P_0.WriteMessage(Current);
		}
		if (fieldMask_ != null)
		{
			P_0.WriteRawTag(82);
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
		if (roleOrMemberId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(RoleOrMemberId);
		}
		if (RoleOrMemberName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(RoleOrMemberName);
		}
		if (channelOrChannelGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelOrChannelGroupId);
		}
		if (ChannelOrChannelGroupName.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(ChannelOrChannelGroupName);
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
	public void MergeFrom(CommunityLogPayloadAccessRule other)
	{
		if (other == null)
		{
			return;
		}
		if (other.CommunityLogAction != CommunityLogAction.Unspecified)
		{
			CommunityLogAction = other.CommunityLogAction;
		}
		if (other.roleOrMemberId_ != null)
		{
			if (roleOrMemberId_ == null)
			{
				RoleOrMemberId = new RoleOrMemberUuid();
			}
			RoleOrMemberId.MergeFrom(other.RoleOrMemberId);
		}
		if (other.RoleOrMemberName.Length != 0)
		{
			RoleOrMemberName = other.RoleOrMemberName;
		}
		if (other.channelOrChannelGroupId_ != null)
		{
			if (channelOrChannelGroupId_ == null)
			{
				ChannelOrChannelGroupId = new ChannelOrChannelGroupUuid();
			}
			ChannelOrChannelGroupId.MergeFrom(other.ChannelOrChannelGroupId);
		}
		if (other.ChannelOrChannelGroupName.Length != 0)
		{
			ChannelOrChannelGroupName = other.ChannelOrChannelGroupName;
		}
		if (other.original_ != null)
		{
			if (original_ == null)
			{
				Original = new CommunityLogPayloadAccessRuleState();
			}
			Original.MergeFrom(other.Original);
		}
		if (other.current_ != null)
		{
			if (current_ == null)
			{
				Current = new CommunityLogPayloadAccessRuleState();
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
				if (roleOrMemberId_ == null)
				{
					RoleOrMemberId = new RoleOrMemberUuid();
				}
				P_0.ReadMessage(RoleOrMemberId);
				break;
			case 42u:
				RoleOrMemberName = P_0.ReadString();
				break;
			case 50u:
				if (channelOrChannelGroupId_ == null)
				{
					ChannelOrChannelGroupId = new ChannelOrChannelGroupUuid();
				}
				P_0.ReadMessage(ChannelOrChannelGroupId);
				break;
			case 58u:
				ChannelOrChannelGroupName = P_0.ReadString();
				break;
			case 66u:
				if (original_ == null)
				{
					Original = new CommunityLogPayloadAccessRuleState();
				}
				P_0.ReadMessage(Original);
				break;
			case 74u:
				if (current_ == null)
				{
					Current = new CommunityLogPayloadAccessRuleState();
				}
				P_0.ReadMessage(Current);
				break;
			case 82u:
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
