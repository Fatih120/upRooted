using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class DirectMessageRingDeclinedPacket : IPacket, IMessage<DirectMessageRingDeclinedPacket>, IMessage, IEquatable<DirectMessageRingDeclinedPacket>, IDeepCloneable<DirectMessageRingDeclinedPacket>, IBufferMessage
{
	private static readonly MessageParser<DirectMessageRingDeclinedPacket> _parser = new MessageParser<DirectMessageRingDeclinedPacket>(() => new DirectMessageRingDeclinedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private UserUuid userId_;

	private DirectMessageUuid id_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectMessageRingDeclinedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectMessageReflection.Descriptor.MessageTypes[4];

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
	public DirectMessageUuid Id
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

	public static implicit operator PacketContainer(DirectMessageRingDeclinedPacket packet)
	{
		return new PacketContainer
		{
			DirectMessageRingDeclined = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageRingDeclinedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageRingDeclinedPacket(DirectMessageRingDeclinedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageRingDeclinedPacket Clone()
	{
		return new DirectMessageRingDeclinedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectMessageRingDeclinedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectMessageRingDeclinedPacket other)
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
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (!object.Equals(Id, other.Id))
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
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
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
		if (userId_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(UserId);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
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
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectMessageRingDeclinedPacket other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PacketType != PacketType.Unspecified)
		{
			PacketType = other.PacketType;
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new DirectMessageUuid();
			}
			Id.MergeFrom(other.Id);
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
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 34u:
				if (id_ == null)
				{
					Id = new DirectMessageUuid();
				}
				P_0.ReadMessage(Id);
				break;
			}
		}
	}
}
