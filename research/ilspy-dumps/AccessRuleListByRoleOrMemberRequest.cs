using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class AccessRuleListByRoleOrMemberRequest : IMessage<AccessRuleListByRoleOrMemberRequest>, IMessage, IEquatable<AccessRuleListByRoleOrMemberRequest>, IDeepCloneable<AccessRuleListByRoleOrMemberRequest>, IBufferMessage
{
	private static readonly MessageParser<AccessRuleListByRoleOrMemberRequest> _parser = new MessageParser<AccessRuleListByRoleOrMemberRequest>(() => new AccessRuleListByRoleOrMemberRequest());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private RoleOrMemberUuid roleOrMemberId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<AccessRuleListByRoleOrMemberRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => AccessRuleReflection.Descriptor.MessageTypes[6];

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
	public AccessRuleListByRoleOrMemberRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public AccessRuleListByRoleOrMemberRequest(AccessRuleListByRoleOrMemberRequest other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		roleOrMemberId_ = ((other.roleOrMemberId_ != null) ? other.roleOrMemberId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public AccessRuleListByRoleOrMemberRequest Clone()
	{
		return new AccessRuleListByRoleOrMemberRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as AccessRuleListByRoleOrMemberRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(AccessRuleListByRoleOrMemberRequest other)
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
		if (!object.Equals(RoleOrMemberId, other.RoleOrMemberId))
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
		if (roleOrMemberId_ != null)
		{
			num ^= RoleOrMemberId.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityId);
		}
		if (roleOrMemberId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(RoleOrMemberId);
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
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (roleOrMemberId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(RoleOrMemberId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(AccessRuleListByRoleOrMemberRequest other)
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
		if (other.roleOrMemberId_ != null)
		{
			if (roleOrMemberId_ == null)
			{
				RoleOrMemberId = new RoleOrMemberUuid();
			}
			RoleOrMemberId.MergeFrom(other.RoleOrMemberId);
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
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 90u:
				if (roleOrMemberId_ == null)
				{
					RoleOrMemberId = new RoleOrMemberUuid();
				}
				P_0.ReadMessage(RoleOrMemberId);
				break;
			}
		}
	}
}
