using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Payloads.Notification;

public sealed class NotificationPayload : IMessage<NotificationPayload>, IMessage, IEquatable<NotificationPayload>, IDeepCloneable<NotificationPayload>, IBufferMessage
{
	public enum PayloadOneofCase
	{
		None,
		CommunityMemberInvited,
		CommunityMemberBanned,
		CommunityMemberKicked,
		FriendshipInviteCreated,
		FriendshipInviteResponded,
		MessageMentioned
	}

	private static readonly MessageParser<NotificationPayload> _parser = new MessageParser<NotificationPayload>(() => new NotificationPayload());

	private UnknownFieldSet _unknownFields;

	private object payload_;

	private PayloadOneofCase payloadCase_ = PayloadOneofCase.None;

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationPayload> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationsReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public NotificationPayloadCommunityMemberInvited CommunityMemberInvited
	{
		get
		{
			return (payloadCase_ == PayloadOneofCase.CommunityMemberInvited) ? ((NotificationPayloadCommunityMemberInvited)payload_) : null;
		}
		set
		{
			payload_ = value;
			payloadCase_ = ((value != null) ? PayloadOneofCase.CommunityMemberInvited : PayloadOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadCommunityMemberBanned CommunityMemberBanned
	{
		get
		{
			return (payloadCase_ == PayloadOneofCase.CommunityMemberBanned) ? ((NotificationPayloadCommunityMemberBanned)payload_) : null;
		}
		set
		{
			payload_ = value;
			payloadCase_ = ((value != null) ? PayloadOneofCase.CommunityMemberBanned : PayloadOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadCommunityMemberKicked CommunityMemberKicked
	{
		get
		{
			return (payloadCase_ == PayloadOneofCase.CommunityMemberKicked) ? ((NotificationPayloadCommunityMemberKicked)payload_) : null;
		}
		set
		{
			payload_ = value;
			payloadCase_ = ((value != null) ? PayloadOneofCase.CommunityMemberKicked : PayloadOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadFriendshipInviteCreated FriendshipInviteCreated
	{
		get
		{
			return (payloadCase_ == PayloadOneofCase.FriendshipInviteCreated) ? ((NotificationPayloadFriendshipInviteCreated)payload_) : null;
		}
		set
		{
			payload_ = value;
			payloadCase_ = ((value != null) ? PayloadOneofCase.FriendshipInviteCreated : PayloadOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadFriendshipInviteResponded FriendshipInviteResponded
	{
		get
		{
			return (payloadCase_ == PayloadOneofCase.FriendshipInviteResponded) ? ((NotificationPayloadFriendshipInviteResponded)payload_) : null;
		}
		set
		{
			payload_ = value;
			payloadCase_ = ((value != null) ? PayloadOneofCase.FriendshipInviteResponded : PayloadOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayloadMessageMentioned MessageMentioned
	{
		get
		{
			return (payloadCase_ == PayloadOneofCase.MessageMentioned) ? ((NotificationPayloadMessageMentioned)payload_) : null;
		}
		set
		{
			payload_ = value;
			payloadCase_ = ((value != null) ? PayloadOneofCase.MessageMentioned : PayloadOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public PayloadOneofCase PayloadCase => payloadCase_;

	[GeneratedCode("protoc", null)]
	public NotificationPayload()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayload(NotificationPayload other)
		: this()
	{
		switch (other.PayloadCase)
		{
		case PayloadOneofCase.CommunityMemberInvited:
			CommunityMemberInvited = other.CommunityMemberInvited.Clone();
			break;
		case PayloadOneofCase.CommunityMemberBanned:
			CommunityMemberBanned = other.CommunityMemberBanned.Clone();
			break;
		case PayloadOneofCase.CommunityMemberKicked:
			CommunityMemberKicked = other.CommunityMemberKicked.Clone();
			break;
		case PayloadOneofCase.FriendshipInviteCreated:
			FriendshipInviteCreated = other.FriendshipInviteCreated.Clone();
			break;
		case PayloadOneofCase.FriendshipInviteResponded:
			FriendshipInviteResponded = other.FriendshipInviteResponded.Clone();
			break;
		case PayloadOneofCase.MessageMentioned:
			MessageMentioned = other.MessageMentioned.Clone();
			break;
		}
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayload Clone()
	{
		return new NotificationPayload(this);
	}

	[GeneratedCode("protoc", null)]
	public void ClearPayload()
	{
		payloadCase_ = PayloadOneofCase.None;
		payload_ = null;
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationPayload);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationPayload other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(CommunityMemberInvited, other.CommunityMemberInvited))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberBanned, other.CommunityMemberBanned))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberKicked, other.CommunityMemberKicked))
		{
			return false;
		}
		if (!object.Equals(FriendshipInviteCreated, other.FriendshipInviteCreated))
		{
			return false;
		}
		if (!object.Equals(FriendshipInviteResponded, other.FriendshipInviteResponded))
		{
			return false;
		}
		if (!object.Equals(MessageMentioned, other.MessageMentioned))
		{
			return false;
		}
		if (PayloadCase != other.PayloadCase)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (payloadCase_ == PayloadOneofCase.CommunityMemberInvited)
		{
			num ^= CommunityMemberInvited.GetHashCode();
		}
		if (payloadCase_ == PayloadOneofCase.CommunityMemberBanned)
		{
			num ^= CommunityMemberBanned.GetHashCode();
		}
		if (payloadCase_ == PayloadOneofCase.CommunityMemberKicked)
		{
			num ^= CommunityMemberKicked.GetHashCode();
		}
		if (payloadCase_ == PayloadOneofCase.FriendshipInviteCreated)
		{
			num ^= FriendshipInviteCreated.GetHashCode();
		}
		if (payloadCase_ == PayloadOneofCase.FriendshipInviteResponded)
		{
			num ^= FriendshipInviteResponded.GetHashCode();
		}
		if (payloadCase_ == PayloadOneofCase.MessageMentioned)
		{
			num ^= MessageMentioned.GetHashCode();
		}
		num ^= (int)payloadCase_;
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
		if (payloadCase_ == PayloadOneofCase.CommunityMemberInvited)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(CommunityMemberInvited);
		}
		if (payloadCase_ == PayloadOneofCase.CommunityMemberBanned)
		{
			P_0.WriteRawTag(18);
			P_0.WriteMessage(CommunityMemberBanned);
		}
		if (payloadCase_ == PayloadOneofCase.CommunityMemberKicked)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(CommunityMemberKicked);
		}
		if (payloadCase_ == PayloadOneofCase.FriendshipInviteCreated)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(FriendshipInviteCreated);
		}
		if (payloadCase_ == PayloadOneofCase.FriendshipInviteResponded)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(FriendshipInviteResponded);
		}
		if (payloadCase_ == PayloadOneofCase.MessageMentioned)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(MessageMentioned);
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
		if (payloadCase_ == PayloadOneofCase.CommunityMemberInvited)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityMemberInvited);
		}
		if (payloadCase_ == PayloadOneofCase.CommunityMemberBanned)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityMemberBanned);
		}
		if (payloadCase_ == PayloadOneofCase.CommunityMemberKicked)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityMemberKicked);
		}
		if (payloadCase_ == PayloadOneofCase.FriendshipInviteCreated)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FriendshipInviteCreated);
		}
		if (payloadCase_ == PayloadOneofCase.FriendshipInviteResponded)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FriendshipInviteResponded);
		}
		if (payloadCase_ == PayloadOneofCase.MessageMentioned)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(MessageMentioned);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotificationPayload other)
	{
		if (other == null)
		{
			return;
		}
		switch (other.PayloadCase)
		{
		case PayloadOneofCase.CommunityMemberInvited:
			if (CommunityMemberInvited == null)
			{
				CommunityMemberInvited = new NotificationPayloadCommunityMemberInvited();
			}
			CommunityMemberInvited.MergeFrom(other.CommunityMemberInvited);
			break;
		case PayloadOneofCase.CommunityMemberBanned:
			if (CommunityMemberBanned == null)
			{
				CommunityMemberBanned = new NotificationPayloadCommunityMemberBanned();
			}
			CommunityMemberBanned.MergeFrom(other.CommunityMemberBanned);
			break;
		case PayloadOneofCase.CommunityMemberKicked:
			if (CommunityMemberKicked == null)
			{
				CommunityMemberKicked = new NotificationPayloadCommunityMemberKicked();
			}
			CommunityMemberKicked.MergeFrom(other.CommunityMemberKicked);
			break;
		case PayloadOneofCase.FriendshipInviteCreated:
			if (FriendshipInviteCreated == null)
			{
				FriendshipInviteCreated = new NotificationPayloadFriendshipInviteCreated();
			}
			FriendshipInviteCreated.MergeFrom(other.FriendshipInviteCreated);
			break;
		case PayloadOneofCase.FriendshipInviteResponded:
			if (FriendshipInviteResponded == null)
			{
				FriendshipInviteResponded = new NotificationPayloadFriendshipInviteResponded();
			}
			FriendshipInviteResponded.MergeFrom(other.FriendshipInviteResponded);
			break;
		case PayloadOneofCase.MessageMentioned:
			if (MessageMentioned == null)
			{
				MessageMentioned = new NotificationPayloadMessageMentioned();
			}
			MessageMentioned.MergeFrom(other.MessageMentioned);
			break;
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
			case 10u:
			{
				NotificationPayloadCommunityMemberInvited notificationPayloadCommunityMemberInvited = new NotificationPayloadCommunityMemberInvited();
				if (payloadCase_ == PayloadOneofCase.CommunityMemberInvited)
				{
					notificationPayloadCommunityMemberInvited.MergeFrom(CommunityMemberInvited);
				}
				P_0.ReadMessage(notificationPayloadCommunityMemberInvited);
				CommunityMemberInvited = notificationPayloadCommunityMemberInvited;
				break;
			}
			case 18u:
			{
				NotificationPayloadCommunityMemberBanned notificationPayloadCommunityMemberBanned = new NotificationPayloadCommunityMemberBanned();
				if (payloadCase_ == PayloadOneofCase.CommunityMemberBanned)
				{
					notificationPayloadCommunityMemberBanned.MergeFrom(CommunityMemberBanned);
				}
				P_0.ReadMessage(notificationPayloadCommunityMemberBanned);
				CommunityMemberBanned = notificationPayloadCommunityMemberBanned;
				break;
			}
			case 26u:
			{
				NotificationPayloadCommunityMemberKicked notificationPayloadCommunityMemberKicked = new NotificationPayloadCommunityMemberKicked();
				if (payloadCase_ == PayloadOneofCase.CommunityMemberKicked)
				{
					notificationPayloadCommunityMemberKicked.MergeFrom(CommunityMemberKicked);
				}
				P_0.ReadMessage(notificationPayloadCommunityMemberKicked);
				CommunityMemberKicked = notificationPayloadCommunityMemberKicked;
				break;
			}
			case 34u:
			{
				NotificationPayloadFriendshipInviteCreated notificationPayloadFriendshipInviteCreated = new NotificationPayloadFriendshipInviteCreated();
				if (payloadCase_ == PayloadOneofCase.FriendshipInviteCreated)
				{
					notificationPayloadFriendshipInviteCreated.MergeFrom(FriendshipInviteCreated);
				}
				P_0.ReadMessage(notificationPayloadFriendshipInviteCreated);
				FriendshipInviteCreated = notificationPayloadFriendshipInviteCreated;
				break;
			}
			case 42u:
			{
				NotificationPayloadFriendshipInviteResponded notificationPayloadFriendshipInviteResponded = new NotificationPayloadFriendshipInviteResponded();
				if (payloadCase_ == PayloadOneofCase.FriendshipInviteResponded)
				{
					notificationPayloadFriendshipInviteResponded.MergeFrom(FriendshipInviteResponded);
				}
				P_0.ReadMessage(notificationPayloadFriendshipInviteResponded);
				FriendshipInviteResponded = notificationPayloadFriendshipInviteResponded;
				break;
			}
			case 50u:
			{
				NotificationPayloadMessageMentioned notificationPayloadMessageMentioned = new NotificationPayloadMessageMentioned();
				if (payloadCase_ == PayloadOneofCase.MessageMentioned)
				{
					notificationPayloadMessageMentioned.MergeFrom(MessageMentioned);
				}
				P_0.ReadMessage(notificationPayloadMessageMentioned);
				MessageMentioned = notificationPayloadMessageMentioned;
				break;
			}
			}
		}
	}
}
