using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class DirectMessageRingPacket : IPacket, IMessage<DirectMessageRingPacket>, IMessage, IEquatable<DirectMessageRingPacket>, IDeepCloneable<DirectMessageRingPacket>, IBufferMessage
{
	private static readonly MessageParser<DirectMessageRingPacket> _parser = new MessageParser<DirectMessageRingPacket>(() => new DirectMessageRingPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private UserUuid callerUserId_;

	private DirectMessageUuid id_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectMessageRingPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectMessageReflection.Descriptor.MessageTypes[3];

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
	public UserUuid CallerUserId
	{
		get
		{
			return callerUserId_;
		}
		set
		{
			callerUserId_ = value;
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

	public static implicit operator PacketContainer(DirectMessageRingPacket packet)
	{
		return new PacketContainer
		{
			DirectMessageRing = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageRingPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageRingPacket(DirectMessageRingPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		callerUserId_ = ((other.callerUserId_ != null) ? other.callerUserId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageRingPacket Clone()
	{
		return new DirectMessageRingPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectMessageRingPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectMessageRingPacket other)
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
		if (!object.Equals(CallerUserId, other.CallerUserId))
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
		if (callerUserId_ != null)
		{
			num ^= CallerUserId.GetHashCode();
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
		if (callerUserId_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(CallerUserId);
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
		if (callerUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CallerUserId);
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
	public void MergeFrom(DirectMessageRingPacket other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PacketType != PacketType.Unspecified)
		{
			PacketType = other.PacketType;
		}
		if (other.callerUserId_ != null)
		{
			if (callerUserId_ == null)
			{
				CallerUserId = new UserUuid();
			}
			CallerUserId.MergeFrom(other.CallerUserId);
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
				if (callerUserId_ == null)
				{
					CallerUserId = new UserUuid();
				}
				P_0.ReadMessage(CallerUserId);
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
