using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Packets;

public sealed class HubNotification : IMessage<HubNotification>, IMessage, IEquatable<HubNotification>, IDeepCloneable<HubNotification>, IBufferMessage
{
	public enum PacketOneofCase
	{
		None = 0,
		Ping = 4
	}

	private static readonly MessageParser<HubNotification> _parser = new MessageParser<HubNotification>(() => new HubNotification());

	private UnknownFieldSet _unknownFields;

	private long sequenceNumber_;

	private object packet_;

	private PacketOneofCase packetCase_ = PacketOneofCase.None;

	[GeneratedCode("protoc", null)]
	public static MessageParser<HubNotification> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PacketReflection.Descriptor.MessageTypes[2];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

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

	[GeneratedCode("protoc", null)]
	public PingPacket Ping
	{
		get
		{
			return (packetCase_ == PacketOneofCase.Ping) ? ((PingPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.Ping : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public PacketOneofCase PacketCase => packetCase_;

	[GeneratedCode("protoc", null)]
	public HubNotification()
	{
	}

	[GeneratedCode("protoc", null)]
	public HubNotification(HubNotification other)
		: this()
	{
		sequenceNumber_ = other.sequenceNumber_;
		PacketOneofCase packetCase = other.PacketCase;
		PacketOneofCase packetOneofCase = packetCase;
		if (packetOneofCase == PacketOneofCase.Ping)
		{
			Ping = other.Ping.Clone();
		}
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public HubNotification Clone()
	{
		return new HubNotification(this);
	}

	[GeneratedCode("protoc", null)]
	public void ClearPacket()
	{
		packetCase_ = PacketOneofCase.None;
		packet_ = null;
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as HubNotification);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(HubNotification other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (SequenceNumber != other.SequenceNumber)
		{
			return false;
		}
		if (!object.Equals(Ping, other.Ping))
		{
			return false;
		}
		if (PacketCase != other.PacketCase)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (SequenceNumber != 0)
		{
			num ^= SequenceNumber.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.Ping)
		{
			num ^= Ping.GetHashCode();
		}
		num ^= (int)packetCase_;
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
		if (SequenceNumber != 0)
		{
			P_0.WriteRawTag(16);
			P_0.WriteInt64(SequenceNumber);
		}
		if (packetCase_ == PacketOneofCase.Ping)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Ping);
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
		if (SequenceNumber != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(SequenceNumber);
		}
		if (packetCase_ == PacketOneofCase.Ping)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Ping);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(HubNotification other)
	{
		if (other == null)
		{
			return;
		}
		if (other.SequenceNumber != 0)
		{
			SequenceNumber = other.SequenceNumber;
		}
		PacketOneofCase packetCase = other.PacketCase;
		PacketOneofCase packetOneofCase = packetCase;
		if (packetOneofCase == PacketOneofCase.Ping)
		{
			if (Ping == null)
			{
				Ping = new PingPacket();
			}
			Ping.MergeFrom(other.Ping);
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
			case 16u:
				SequenceNumber = P_0.ReadInt64();
				break;
			case 34u:
			{
				PingPacket pingPacket = new PingPacket();
				if (packetCase_ == PacketOneofCase.Ping)
				{
					pingPacket.MergeFrom(Ping);
				}
				P_0.ReadMessage(pingPacket);
				Ping = pingPacket;
				break;
			}
			}
		}
	}
}
