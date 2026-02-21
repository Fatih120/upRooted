using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public sealed class CommunityMemberInviteGetRequest : IMessage<CommunityMemberInviteGetRequest>, IMessage, IEquatable<CommunityMemberInviteGetRequest>, IDeepCloneable<CommunityMemberInviteGetRequest>, IBufferMessage
{
	private static readonly MessageParser<CommunityMemberInviteGetRequest> _parser = new MessageParser<CommunityMemberInviteGetRequest>(() => new CommunityMemberInviteGetRequest());

	private UnknownFieldSet _unknownFields;

	private CommunityUuid communityId_;

	private UserUuid invitedUserId_;

	private UserUuid senderUserId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityMemberInviteGetRequest> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityMemberInviteReflection.Descriptor.MessageTypes[3];

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
	public UserUuid InvitedUserId
	{
		get
		{
			return invitedUserId_;
		}
		set
		{
			invitedUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid SenderUserId
	{
		get
		{
			return senderUserId_;
		}
		set
		{
			senderUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberInviteGetRequest()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberInviteGetRequest(CommunityMemberInviteGetRequest other)
		: this()
	{
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		invitedUserId_ = ((other.invitedUserId_ != null) ? other.invitedUserId_.Clone() : null);
		senderUserId_ = ((other.senderUserId_ != null) ? other.senderUserId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberInviteGetRequest Clone()
	{
		return new CommunityMemberInviteGetRequest(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as CommunityMemberInviteGetRequest);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityMemberInviteGetRequest other)
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
		if (!object.Equals(InvitedUserId, other.InvitedUserId))
		{
			return false;
		}
		if (!object.Equals(SenderUserId, other.SenderUserId))
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
		if (invitedUserId_ != null)
		{
			num ^= InvitedUserId.GetHashCode();
		}
		if (senderUserId_ != null)
		{
			num ^= SenderUserId.GetHashCode();
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
		if (invitedUserId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(InvitedUserId);
		}
		if (senderUserId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(SenderUserId);
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
		if (invitedUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(InvitedUserId);
		}
		if (senderUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SenderUserId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityMemberInviteGetRequest other)
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
		if (other.invitedUserId_ != null)
		{
			if (invitedUserId_ == null)
			{
				InvitedUserId = new UserUuid();
			}
			InvitedUserId.MergeFrom(other.InvitedUserId);
		}
		if (other.senderUserId_ != null)
		{
			if (senderUserId_ == null)
			{
				SenderUserId = new UserUuid();
			}
			SenderUserId.MergeFrom(other.SenderUserId);
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
				if (invitedUserId_ == null)
				{
					InvitedUserId = new UserUuid();
				}
				P_0.ReadMessage(InvitedUserId);
				break;
			case 98u:
				if (senderUserId_ == null)
				{
					SenderUserId = new UserUuid();
				}
				P_0.ReadMessage(SenderUserId);
				break;
			}
		}
	}
}
