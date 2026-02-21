using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;

namespace RootApp.WebApi.Shared.Payloads.Notification;

public sealed class NotificationPayloadFriendshipInviteCreated : IMessage, IMessage<NotificationPayloadFriendshipInviteCreated>, IEquatable<NotificationPayloadFriendshipInviteCreated>, IDeepCloneable<NotificationPayloadFriendshipInviteCreated>, IBufferMessage
{
	private static readonly MessageParser<NotificationPayloadFriendshipInviteCreated> _parser = new MessageParser<NotificationPayloadFriendshipInviteCreated>(() => new NotificationPayloadFriendshipInviteCreated());

	private UnknownFieldSet _unknownFields;

	private UserUuid userId_;

	private UserUuid friendUserId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationPayloadFriendshipInviteCreated> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationsReflection.Descriptor.MessageTypes[4];

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
	public NotificationPayloadFriendshipInviteCreated()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadFriendshipInviteCreated(NotificationPayloadFriendshipInviteCreated other)
		: this()
	{
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		friendUserId_ = ((other.friendUserId_ != null) ? other.friendUserId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadFriendshipInviteCreated Clone()
	{
		return new NotificationPayloadFriendshipInviteCreated(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationPayloadFriendshipInviteCreated);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationPayloadFriendshipInviteCreated other)
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
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotificationPayloadFriendshipInviteCreated other)
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
			}
		}
	}
}
