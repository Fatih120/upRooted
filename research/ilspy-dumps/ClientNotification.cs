using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class ClientNotification : IMessage<ClientNotification>, IMessage, IEquatable<ClientNotification>, IDeepCloneable<ClientNotification>, IBufferMessage
{
	private static readonly MessageParser<ClientNotification> _parser = new MessageParser<ClientNotification>(() => new ClientNotification());

	private UnknownFieldSet _unknownFields;

	private PacketErrorCode errorCode_ = PacketErrorCode.Unspecified;

	private long sequenceNumber_;

	private PacketContainer packet_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<ClientNotification> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PacketReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public PacketErrorCode ErrorCode
	{
		get
		{
			return errorCode_;
		}
		set
		{
			errorCode_ = value;
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

	[GeneratedCode("protoc", null)]
	public PacketContainer Packet
	{
		get
		{
			return packet_;
		}
		set
		{
			packet_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public ClientNotification()
	{
	}

	[GeneratedCode("protoc", null)]
	public ClientNotification(ClientNotification other)
		: this()
	{
		errorCode_ = other.errorCode_;
		sequenceNumber_ = other.sequenceNumber_;
		packet_ = ((other.packet_ != null) ? other.packet_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ClientNotification Clone()
	{
		return new ClientNotification(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ClientNotification);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ClientNotification other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (ErrorCode != other.ErrorCode)
		{
			return false;
		}
		if (SequenceNumber != other.SequenceNumber)
		{
			return false;
		}
		if (!object.Equals(Packet, other.Packet))
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (ErrorCode != PacketErrorCode.Unspecified)
		{
			num ^= ErrorCode.GetHashCode();
		}
		if (SequenceNumber != 0)
		{
			num ^= SequenceNumber.GetHashCode();
		}
		if (packet_ != null)
		{
			num ^= Packet.GetHashCode();
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
		if (ErrorCode != PacketErrorCode.Unspecified)
		{
			P_0.WriteRawTag(8);
			P_0.WriteEnum((int)ErrorCode);
		}
		if (SequenceNumber != 0)
		{
			P_0.WriteRawTag(16);
			P_0.WriteInt64(SequenceNumber);
		}
		if (packet_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(Packet);
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
		if (ErrorCode != PacketErrorCode.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)ErrorCode);
		}
		if (SequenceNumber != 0)
		{
			num += 1 + CodedOutputStream.ComputeInt64Size(SequenceNumber);
		}
		if (packet_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Packet);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ClientNotification other)
	{
		if (other == null)
		{
			return;
		}
		if (other.ErrorCode != PacketErrorCode.Unspecified)
		{
			ErrorCode = other.ErrorCode;
		}
		if (other.SequenceNumber != 0)
		{
			SequenceNumber = other.SequenceNumber;
		}
		if (other.packet_ != null)
		{
			if (packet_ == null)
			{
				Packet = new PacketContainer();
			}
			Packet.MergeFrom(other.Packet);
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
				ErrorCode = (PacketErrorCode)P_0.ReadEnum();
				break;
			case 16u:
				SequenceNumber = P_0.ReadInt64();
				break;
			case 26u:
				if (packet_ == null)
				{
					Packet = new PacketContainer();
				}
				P_0.ReadMessage(Packet);
				break;
			}
		}
	}
}
