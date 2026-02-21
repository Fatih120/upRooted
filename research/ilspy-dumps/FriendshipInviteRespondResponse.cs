using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Grpc.Responses;

public sealed class FriendshipInviteRespondResponse : IMessage<FriendshipInviteRespondResponse>, IMessage, IEquatable<FriendshipInviteRespondResponse>, IDeepCloneable<FriendshipInviteRespondResponse>, IBufferMessage
{
	private static readonly MessageParser<FriendshipInviteRespondResponse> _parser = new MessageParser<FriendshipInviteRespondResponse>(() => new FriendshipInviteRespondResponse());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private UserUuid friendUserId_;

	private FriendshipUuid friendshipId_;

	private FriendshipGroupUuid friendshipGroupId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendshipInviteRespondResponse> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FriendshipReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public UserUuid UserId
	{
		get
		{
			return userId_;
		}
		set
		{
			userId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public UserUuid FriendUserId
	{
		get
		{
			return friendUserId_;
		}
		set
		{
			friendUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipUuid FriendshipId
	{
		get
		{
			return friendshipId_;
		}
		set
		{
			friendshipId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupUuid FriendshipGroupId
	{
		get
		{
			return friendshipGroupId_;
		}
		set
		{
			friendshipGroupId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipInviteRespondResponse()
	{
	}

	[GeneratedCode("protoc", null)]
	public FriendshipInviteRespondResponse(FriendshipInviteRespondResponse other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		friendUserId_ = ((other.friendUserId_ != null) ? other.friendUserId_.Clone() : null);
		friendshipId_ = ((other.friendshipId_ != null) ? other.friendshipId_.Clone() : null);
		friendshipGroupId_ = ((other.friendshipGroupId_ != null) ? other.friendshipGroupId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FriendshipInviteRespondResponse Clone()
	{
		return new FriendshipInviteRespondResponse(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendshipInviteRespondResponse);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FriendshipInviteRespondResponse other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (!object.Equals(FriendUserId, other.FriendUserId))
		{
			return false;
		}
		if (!object.Equals(FriendshipId, other.FriendshipId))
		{
			return false;
		}
		if (!object.Equals(FriendshipGroupId, other.FriendshipGroupId))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (friendUserId_ != null)
		{
			num ^= FriendUserId.GetHashCode();
		}
		if (friendshipId_ != null)
		{
			num ^= FriendshipId.GetHashCode();
		}
		if (friendshipGroupId_ != null)
		{
			num ^= FriendshipGroupId.GetHashCode();
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
		if (userId_ != null)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(UserId);
		}
		if (friendUserId_ != null)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(FriendUserId);
		}
		if (friendshipId_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(FriendshipId);
		}
		if (friendshipGroupId_ != null)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(FriendshipGroupId);
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
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (friendUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FriendUserId);
		}
		if (friendshipId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FriendshipId);
		}
		if (friendshipGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FriendshipGroupId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendshipInviteRespondResponse other)
	{
		if (other == null)
		{
			return;
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.friendUserId_ != null)
		{
			if (friendUserId_ == null)
			{
				FriendUserId = new UserUuid();
			}
			FriendUserId.MergeFrom(other.FriendUserId);
		}
		if (other.friendshipId_ != null)
		{
			if (friendshipId_ == null)
			{
				FriendshipId = new FriendshipUuid();
			}
			FriendshipId.MergeFrom(other.FriendshipId);
		}
		if (other.friendshipGroupId_ != null)
		{
			if (friendshipGroupId_ == null)
			{
				FriendshipGroupId = new FriendshipGroupUuid();
			}
			FriendshipGroupId.MergeFrom(other.FriendshipGroupId);
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
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 90u:
				if (friendUserId_ == null)
				{
					FriendUserId = new UserUuid();
				}
				P_0.ReadMessage(FriendUserId);
				break;
			case 98u:
				if (friendshipId_ == null)
				{
					FriendshipId = new FriendshipUuid();
				}
				P_0.ReadMessage(FriendshipId);
				break;
			case 106u:
				if (friendshipGroupId_ == null)
				{
					FriendshipGroupId = new FriendshipGroupUuid();
				}
				P_0.ReadMessage(FriendshipGroupId);
				break;
			}
		}
	}
}
