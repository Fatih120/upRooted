using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class MessageSetTypingIndicatorPacket : IPacketCommunity, IPacket, IMessage<MessageSetTypingIndicatorPacket>, IMessage, IEquatable<MessageSetTypingIndicatorPacket>, IDeepCloneable<MessageSetTypingIndicatorPacket>, IBufferMessage
{
	private static readonly MessageParser<MessageSetTypingIndicatorPacket> _parser = new MessageParser<MessageSetTypingIndicatorPacket>(() => new MessageSetTypingIndicatorPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private MessageContainerUuid containerId_;

	private UserUuid userId_;

	private bool isTyping_;

	private Timestamp createdAt_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessageSetTypingIndicatorPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[6];

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
	public CommunityUuid CommunityId
	{
		get
		{
			return communityId_;
		}
		set
		{
			communityId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageContainerUuid ContainerId
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
	public bool IsTyping
	{
		get
		{
			return isTyping_;
		}
		set
		{
			isTyping_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp CreatedAt
	{
		get
		{
			return createdAt_;
		}
		set
		{
			createdAt_ = value;
		}
	}

	public static implicit operator PacketContainer(MessageSetTypingIndicatorPacket packet)
	{
		return new PacketContainer
		{
			MessageSetTypingIndicator = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public MessageSetTypingIndicatorPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessageSetTypingIndicatorPacket(MessageSetTypingIndicatorPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		isTyping_ = other.isTyping_;
		createdAt_ = ((other.createdAt_ != null) ? other.createdAt_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessageSetTypingIndicatorPacket Clone()
	{
		return new MessageSetTypingIndicatorPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessageSetTypingIndicatorPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessageSetTypingIndicatorPacket other)
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
		if (!object.Equals(CommunityId, other.CommunityId))
		{
			return false;
		}
		if (!object.Equals(ContainerId, other.ContainerId))
		{
			return false;
		}
		if (!object.Equals(UserId, other.UserId))
		{
			return false;
		}
		if (IsTyping != other.IsTyping)
		{
			return false;
		}
		if (!object.Equals(CreatedAt, other.CreatedAt))
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
		if (communityId_ != null)
		{
			num ^= CommunityId.GetHashCode();
		}
		if (containerId_ != null)
		{
			num ^= ContainerId.GetHashCode();
		}
		if (userId_ != null)
		{
			num ^= UserId.GetHashCode();
		}
		if (IsTyping)
		{
			num ^= IsTyping.GetHashCode();
		}
		if (createdAt_ != null)
		{
			num ^= CreatedAt.GetHashCode();
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
		if (communityId_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(CommunityId);
		}
		if (containerId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(ContainerId);
		}
		if (userId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(UserId);
		}
		if (IsTyping)
		{
			P_0.WriteRawTag(48);
			P_0.WriteBool(IsTyping);
		}
		if (createdAt_ != null)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(CreatedAt);
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
		if (communityId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityId);
		}
		if (containerId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ContainerId);
		}
		if (userId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(UserId);
		}
		if (IsTyping)
		{
			num += 2;
		}
		if (createdAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CreatedAt);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessageSetTypingIndicatorPacket other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PacketType != PacketType.Unspecified)
		{
			PacketType = other.PacketType;
		}
		if (other.communityId_ != null)
		{
			if (communityId_ == null)
			{
				CommunityId = new CommunityUuid();
			}
			CommunityId.MergeFrom(other.CommunityId);
		}
		if (other.containerId_ != null)
		{
			if (containerId_ == null)
			{
				ContainerId = new MessageContainerUuid();
			}
			ContainerId.MergeFrom(other.ContainerId);
		}
		if (other.userId_ != null)
		{
			if (userId_ == null)
			{
				UserId = new UserUuid();
			}
			UserId.MergeFrom(other.UserId);
		}
		if (other.IsTyping)
		{
			IsTyping = other.IsTyping;
		}
		if (other.createdAt_ != null)
		{
			if (createdAt_ == null)
			{
				CreatedAt = new Timestamp();
			}
			CreatedAt.MergeFrom(other.CreatedAt);
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
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 34u:
				if (containerId_ == null)
				{
					ContainerId = new MessageContainerUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 42u:
				if (userId_ == null)
				{
					UserId = new UserUuid();
				}
				P_0.ReadMessage(UserId);
				break;
			case 48u:
				IsTyping = P_0.ReadBool();
				break;
			case 58u:
				if (createdAt_ == null)
				{
					CreatedAt = new Timestamp();
				}
				P_0.ReadMessage(CreatedAt);
				break;
			}
		}
	}
}
