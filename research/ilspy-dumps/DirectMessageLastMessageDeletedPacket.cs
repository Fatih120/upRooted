using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class DirectMessageLastMessageDeletedPacket : IPacket, IMessage<DirectMessageLastMessageDeletedPacket>, IMessage, IEquatable<DirectMessageLastMessageDeletedPacket>, IDeepCloneable<DirectMessageLastMessageDeletedPacket>, IBufferMessage
{
	private static readonly MessageParser<DirectMessageLastMessageDeletedPacket> _parser = new MessageParser<DirectMessageLastMessageDeletedPacket>(() => new DirectMessageLastMessageDeletedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private DirectMessageUuid id_;

	private MessagePacket lastMessage_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectMessageLastMessageDeletedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectMessageReflection.Descriptor.MessageTypes[5];

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

	[GeneratedCode("protoc", null)]
	public MessagePacket LastMessage
	{
		get
		{
			return lastMessage_;
		}
		set
		{
			lastMessage_ = value;
		}
	}

	public static implicit operator PacketContainer(DirectMessageLastMessageDeletedPacket packet)
	{
		return new PacketContainer
		{
			DirectMessageLastMessageDeleted = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageLastMessageDeletedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageLastMessageDeletedPacket(DirectMessageLastMessageDeletedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		lastMessage_ = ((other.lastMessage_ != null) ? other.lastMessage_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageLastMessageDeletedPacket Clone()
	{
		return new DirectMessageLastMessageDeletedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectMessageLastMessageDeletedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectMessageLastMessageDeletedPacket other)
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
		if (!object.Equals(LastMessage, other.LastMessage))
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
		if (lastMessage_ != null)
		{
			num ^= LastMessage.GetHashCode();
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
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		if (lastMessage_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(LastMessage);
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
		if (lastMessage_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(LastMessage);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectMessageLastMessageDeletedPacket other)
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
				Id = new DirectMessageUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.lastMessage_ != null)
		{
			if (lastMessage_ == null)
			{
				LastMessage = new MessagePacket();
			}
			LastMessage.MergeFrom(other.LastMessage);
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
			case 34u:
				if (id_ == null)
				{
					Id = new DirectMessageUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (lastMessage_ == null)
				{
					LastMessage = new MessagePacket();
				}
				P_0.ReadMessage(LastMessage);
				break;
			}
		}
	}
}
