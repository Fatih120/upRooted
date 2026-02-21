using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class AccessRuleCreateRoleOrMemberRequest : IMessage<AccessRuleCreateRoleOrMemberRequest>, IMessage, IEquatable<AccessRuleCreateRoleOrMemberRequest>, IDeepCloneable<AccessRuleCreateRoleOrMemberRequest>, IBufferMessage
{
	private static readonly MessageParser<AccessRuleCreateRoleOrMemberRequest> _parser = new MessageParser<AccessRuleCreateRoleOrMemberRequest>(() => new AccessRuleCreateRoleOrMemberRequest());

	private UnknownFieldSet _unknownFields;

	private RoleOrMemberUuid roleOrMemberId_;

	private ChannelOverlayPermission overlay_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AccessRuleCreateRoleOrMemberRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AccessRuleReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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
	public AccessRuleCreateRoleOrMemberRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public AccessRuleCreateRoleOrMemberRequest(AccessRuleCreateRoleOrMemberRequest other)
		: this()
	{
		roleOrMemberId_ = ((other.roleOrMemberId_ != null) ? other.roleOrMemberId_.Clone() : null);
		overlay_ = ((other.overlay_ != null) ? other.overlay_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AccessRuleCreateRoleOrMemberRequest Clone()
	{
		return new AccessRuleCreateRoleOrMemberRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AccessRuleCreateRoleOrMemberRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AccessRuleCreateRoleOrMemberRequest other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
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
		if (roleOrMemberId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(RoleOrMemberId);
		}
		if (overlay_ != null)
		{
			P_0.WriteRawTag(90);
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
	public void MergeFrom(AccessRuleCreateRoleOrMemberRequest other)
	{
		if (other == null)
		{
			return;
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
			case 82u:
				if (roleOrMemberId_ == null)
				{
					RoleOrMemberId = new RoleOrMemberUuid();
				}
				P_0.ReadMessage(RoleOrMemberId);
				break;
			case 90u:
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
