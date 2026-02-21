using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class PingPacket : IPacket, IMessage<PingPacket>, IMessage, IEquatable<PingPacket>, IDeepCloneable<PingPacket>, IBufferMessage
{
	private static readonly MessageParser<PingPacket> _parser = new MessageParser<PingPacket>(() => new PingPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private long sequenceNumber_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<PingPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PingReflection.Descriptor.MessageTypes[0];

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
	public long SequenceNumber
	{
		get
		{
			return sequenceNumber_;
		}
		set
		{
			sequenceNumber_ = value;
		}
	}

	public static implicit operator HubNotification(PingPacket packet)
	{
		return new HubNotification
		{
			Ping = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public PingPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public PingPacket(PingPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		sequenceNumber_ = other.sequenceNumber_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public PingPacket Clone()
	{
		return new PingPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PingPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(PingPacket other)
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
		if (SequenceNumber != other.SequenceNumber)
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
		if (SequenceNumber != 0)
		{
			num ^= SequenceNumber.GetHashCode();
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
		if (SequenceNumber != 0)
		{
			P_0.WriteRawTag(16);
			P_0.WriteInt64(SequenceNumber);
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
		if (SequenceNumber != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(SequenceNumber);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(PingPacket other)
	{
		if (other != null)
		{
			if (other.PacketType != PacketType.Unspecified)
			{
				PacketType = other.PacketType;
			}
			if (other.SequenceNumber != 0)
			{
				SequenceNumber = other.SequenceNumber;
			}
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
			case 16u:
				SequenceNumber = P_0.ReadInt64();
				break;
			}
		}
	}
}
