using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Payloads.Notification;

namespace RootApp.WebApi.Shared.Packets;

public sealed class NotificationPacket : IPacket, IMessage<NotificationPacket>, IMessage, IEquatable<NotificationPacket>, IDeepCloneable<NotificationPacket>, IBufferMessage
{
	private static readonly MessageParser<NotificationPacket> _parser = new MessageParser<NotificationPacket>(() => new NotificationPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private NotificationUuid id_;

	private RootUuid containerId_;

	private RootUuid subContainerId_;

	private UserUuid userId_;

	private NotificationType notificationType_ = NotificationType.Unspecified;

	private NotificationPayload payload_;

	private bool isViewed_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationsReflection.Descriptor.MessageTypes[4];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public PacketType PacketType
	{
		get
		{
			return packetType_;
		}
		set
		{
			packetType_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationUuid Id
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
	public RootUuid ContainerId
	{
		get
		{
			return containerId_;
		}
		set
		{
			containerId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RootUuid SubContainerId
	{
		get
		{
			return subContainerId_;
		}
		set
		{
			subContainerId_ = value;
		}
	}

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
	public NotificationType NotificationType
	{
		get
		{
			return notificationType_;
		}
		set
		{
			notificationType_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationPayload Payload
	{
		get
		{
			return payload_;
		}
		set
		{
			payload_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public bool IsViewed
	{
		get
		{
			return isViewed_;
		}
		set
		{
			isViewed_ = value;
		}
	}

	public static implicit operator PacketContainer(NotificationPacket packet)
	{
		return new PacketContainer
		{
			Notification = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public NotificationPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationPacket(NotificationPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		subContainerId_ = ((other.subContainerId_ != null) ? other.subContainerId_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		notificationType_ = other.notificationType_;
		payload_ = ((other.payload_ != null) ? other.payload_.Clone() : null);
		isViewed_ = other.isViewed_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationPacket Clone()
	{
		return new NotificationPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationPacket other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (PacketType != other.PacketType)
		{
			return false;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(ContainerId, other.ContainerId))
		{
			return false;
		}
		if (!object.Equals(SubContainerId, other.SubContainerId))
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (NotificationType != other.NotificationType)
		{
			return false;
		}
		if (!object.Equals(Payload, other.Payload))
		{
			return false;
		}
		if (IsViewed != other.IsViewed)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (PacketType != PacketType.Unspecified)
		{
			num ^= PacketType.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		if (subContainerId_ != null)
		{
			num ^= SubContainerId.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (NotificationType != NotificationType.Unspecified)
		{
			num ^= NotificationType.GetHashCode();
		}
		if (payload_ != null)
		{
			num ^= Payload.GetHashCode();
		}
		if (IsViewed)
		{
			num ^= IsViewed.GetHashCode();
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
		if (PacketType != PacketType.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)PacketType);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(Id);
		}
		if (containerId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(ContainerId);
		}
		if (subContainerId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(SubContainerId);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(UserId);
		}
		if (NotificationType != NotificationType.Unspecified)
		{
			P_0.WriteRawTag(56);
			P_0.WriteEnum((int)NotificationType);
		}
		if (payload_ != null)
		{
			P_0.WriteRawTag(66);
			P_0.WriteMessage(Payload);
		}
		if (IsViewed)
		{
			P_0.WriteRawTag(72);
			P_0.WriteBool(IsViewed);
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
		if (PacketType != PacketType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)PacketType);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		if (subContainerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(SubContainerId);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (NotificationType != NotificationType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)NotificationType);
		}
		if (payload_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Payload);
		}
		if (IsViewed)
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
	public void MergeFrom(NotificationPacket other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PacketType != PacketType.Unspecified)
		{
			PacketType = other.PacketType;
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new NotificationUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new RootUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		if (other.subContainerId_ != null)
		{
			if (subContainerId_ == null)
			{
				SubContainerId = new RootUuid();
			}
			SubContainerId.MergeFrom(other.SubContainerId);
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.NotificationType != NotificationType.Unspecified)
		{
			NotificationType = other.NotificationType;
		}
		if (other.payload_ != null)
		{
			if (payload_ == null)
			{
				Payload = new NotificationPayload();
			}
			Payload.MergeFrom(other.Payload);
		}
		if (other.IsViewed)
		{
			IsViewed = other.IsViewed;
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
				PacketType = (PacketType)P_0.ReadEnum();
				break;
			case 26u:
				if (id_ == null)
				{
					Id = new NotificationUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 34u:
				if (containerId_ == null)
				{
					ContainerId = new RootUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 42u:
				if (subContainerId_ == null)
				{
					SubContainerId = new RootUuid();
				}
				P_0.ReadMessage(SubContainerId);
				break;
			case 50u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 56u:
				NotificationType = (NotificationType)P_0.ReadEnum();
				break;
			case 66u:
				if (payload_ == null)
				{
					Payload = new NotificationPayload();
				}
				P_0.ReadMessage(Payload);
				break;
			case 72u:
				IsViewed = P_0.ReadBool();
				break;
			}
		}
	}
}
