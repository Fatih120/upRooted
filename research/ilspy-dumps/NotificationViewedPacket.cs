using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class NotificationViewedPacket : IPacket, IMessage<NotificationViewedPacket>, IMessage, IEquatable<NotificationViewedPacket>, IDeepCloneable<NotificationViewedPacket>, IBufferMessage
{
	private static readonly MessageParser<NotificationViewedPacket> _parser = new MessageParser<NotificationViewedPacket>(() => new NotificationViewedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private static readonly FieldCodec<NotificationUuid> _repeated_notificationIds_codec = FieldCodec.ForMessage(18u, NotificationUuid.Parser);

	private readonly RepeatedField<NotificationUuid> notificationIds_ = new RepeatedField<NotificationUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<NotificationViewedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => NotificationsReflection.Descriptor.MessageTypes[0];

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
	public RepeatedField<NotificationUuid> NotificationIds => notificationIds_;

	public static implicit operator PacketContainer(NotificationViewedPacket packet)
	{
		return new PacketContainer
		{
			NotificationViewed = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public NotificationViewedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public NotificationViewedPacket(NotificationViewedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		notificationIds_ = other.notificationIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public NotificationViewedPacket Clone()
	{
		return new NotificationViewedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as NotificationViewedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(NotificationViewedPacket other)
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
		if (!notificationIds_.Equals(other.notificationIds_))
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
		num ^= notificationIds_.GetHashCode();
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
		notificationIds_.WriteTo(ref P_0, _repeated_notificationIds_codec);
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
		num += notificationIds_.CalculateSize(_repeated_notificationIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(NotificationViewedPacket other)
	{
		if (other != null)
		{
			if (other.PacketType != PacketType.Unspecified)
			{
				PacketType = other.PacketType;
			}
			notificationIds_.Add(other.notificationIds_);
			_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
		}
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
			case 18u:
				notificationIds_.AddEntriesFrom(ref P_0, _repeated_notificationIds_codec);
				break;
			}
		}
	}
}
