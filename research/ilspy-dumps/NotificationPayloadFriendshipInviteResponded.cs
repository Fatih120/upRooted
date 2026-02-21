using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.Notification;

public sealed class NotificationPayloadFriendshipInviteResponded : IMessage, IMessage<NotificationPayloadFriendshipInviteResponded>, IEquatable<NotificationPayloadFriendshipInviteResponded>, IDeepCloneable<NotificationPayloadFriendshipInviteResponded>, IBufferMessage
{
	private static readonly MessageParser<NotificationPayloadFriendshipInviteResponded> _parser = new MessageParser<NotificationPayloadFriendshipInviteResponded>(() => new NotificationPayloadFriendshipInviteResponded());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private UserUuid friendUserId_;

	private bool isFriendshipAccepted_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationPayloadFriendshipInviteResponded> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationsReflection.Descriptor.MessageTypes[5];

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
	public bool IsFriendshipAccepted
	{
		get
		{
			return isFriendshipAccepted_;
		}
		set
		{
			isFriendshipAccepted_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadFriendshipInviteResponded()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadFriendshipInviteResponded(NotificationPayloadFriendshipInviteResponded other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		friendUserId_ = ((other.friendUserId_ != null) ? other.friendUserId_.Clone() : null);
		isFriendshipAccepted_ = other.isFriendshipAccepted_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadFriendshipInviteResponded Clone()
	{
		return new NotificationPayloadFriendshipInviteResponded(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationPayloadFriendshipInviteResponded);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationPayloadFriendshipInviteResponded other)
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
		if (IsFriendshipAccepted != other.IsFriendshipAccepted)
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
		if (IsFriendshipAccepted)
		{
			num ^= IsFriendshipAccepted.GetHashCode();
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
			P_0.WriteRawTag(34);
			P_0.WriteMessage(UserId);
		}
		if (friendUserId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(FriendUserId);
		}
		if (IsFriendshipAccepted)
		{
			P_0.WriteRawTag(48);
			P_0.WriteBool(IsFriendshipAccepted);
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
		if (IsFriendshipAccepted)
		{
			num += 2;
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotificationPayloadFriendshipInviteResponded other)
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
		if (other.IsFriendshipAccepted)
		{
			IsFriendshipAccepted = other.IsFriendshipAccepted;
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
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 42u:
				if (friendUserId_ == null)
				{
					FriendUserId = new UserUuid();
				}
				P_0.ReadMessage(FriendUserId);
				break;
			case 48u:
				IsFriendshipAccepted = P_0.ReadBool();
				break;
			}
		}
	}
}
