using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class AccessRuleResponse : IMessage<AccessRuleResponse>, IMessage, IEquatable<AccessRuleResponse>, IDeepCloneable<AccessRuleResponse>, IBufferMessage
{
	private static readonly MessageParser<AccessRuleResponse> _parser = new MessageParser<AccessRuleResponse>(() => new AccessRuleResponse());

	private UnknownFieldSet _unknownFields;

	private ChannelOrChannelGroupUuid channelOrChannelGroupId_;

	private RoleOrMemberUuid roleOrMemberId_;

	private ChannelOverlayPermission overlay_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AccessRuleResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AccessRuleReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public ChannelOverlayPermission Overlay
	{
		get
		{
			return overlay_;
		}
		set
		{
			overlay_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public AccessRuleResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public AccessRuleResponse(AccessRuleResponse other)
		: this()
	{
		channelOrChannelGroupId_ = ((other.channelOrChannelGroupId_ != null) ? other.channelOrChannelGroupId_.Clone() : null);
		roleOrMemberId_ = ((other.roleOrMemberId_ != null) ? other.roleOrMemberId_.Clone() : null);
		overlay_ = ((other.overlay_ != null) ? other.overlay_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AccessRuleResponse Clone()
	{
		return new AccessRuleResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AccessRuleResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AccessRuleResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(ChannelOrChannelGroupId, other.ChannelOrChannelGroupId))
		{
			return false;
		}
		if (!object.Equals(RoleOrMemberId, other.RoleOrMemberId))
		{
			return false;
		}
		if (!object.Equals(Overlay, other.Overlay))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (channelOrChannelGroupId_ != null)
		{
			num ^= ChannelOrChannelGroupId.GetHashCode();
		}
		if (roleOrMemberId_ != null)
		{
			num ^= RoleOrMemberId.GetHashCode();
		}
		if (overlay_ != null)
		{
			num ^= Overlay.GetHashCode();
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
		if (channelOrChannelGroupId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(ChannelOrChannelGroupId);
		}
		if (roleOrMemberId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(RoleOrMemberId);
		}
		if (overlay_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(Overlay);
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
		if (channelOrChannelGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelOrChannelGroupId);
		}
		if (roleOrMemberId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(RoleOrMemberId);
		}
		if (overlay_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Overlay);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AccessRuleResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.channelOrChannelGroupId_ != null)
		{
			if (channelOrChannelGroupId_ == null)
			{
				ChannelOrChannelGroupId = new ChannelOrChannelGroupUuid();
			}
			ChannelOrChannelGroupId.MergeFrom(other.ChannelOrChannelGroupId);
		}
		if (other.roleOrMemberId_ != null)
		{
			if (roleOrMemberId_ == null)
			{
				RoleOrMemberId = new RoleOrMemberUuid();
			}
			RoleOrMemberId.MergeFrom(other.RoleOrMemberId);
		}
		if (other.overlay_ != null)
		{
			if (overlay_ == null)
			{
				Overlay = new ChannelOverlayPermission();
			}
			Overlay.MergeFrom(other.Overlay);
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
			case 34u:
				if (channelOrChannelGroupId_ == null)
				{
					ChannelOrChannelGroupId = new ChannelOrChannelGroupUuid();
				}
				P_0.ReadMessage(ChannelOrChannelGroupId);
				break;
			case 42u:
				if (roleOrMemberId_ == null)
				{
					RoleOrMemberId = new RoleOrMemberUuid();
				}
				P_0.ReadMessage(RoleOrMemberId);
				break;
			case 50u:
				if (overlay_ == null)
				{
					Overlay = new ChannelOverlayPermission();
				}
				P_0.ReadMessage(Overlay);
				break;
			}
		}
	}
}
