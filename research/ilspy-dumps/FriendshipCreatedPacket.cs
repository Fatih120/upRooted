using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class FriendshipCreatedPacket : IPacket, IMessage<FriendshipCreatedPacket>, IMessage, IEquatable<FriendshipCreatedPacket>, IDeepCloneable<FriendshipCreatedPacket>, IBufferMessage
{
	private static readonly MessageParser<FriendshipCreatedPacket> _parser = new MessageParser<FriendshipCreatedPacket>(() => new FriendshipCreatedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private FriendshipUuid id_;

	private UserUuid friendUserId_;

	private FriendshipGroupUuid friendshipGroupId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendshipCreatedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FriendshipReflection.Descriptor.MessageTypes[0];

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
	public FriendshipUuid Id
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
	public UserUuid FriendUserId
	{
		get
		{
			return friendUserId_;
		}
		set
		{
			friendUserId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupUuid FriendshipGroupId
	{
		get
		{
			return friendshipGroupId_;
		}
		set
		{
			friendshipGroupId_ = value;
		}
	}

	public static implicit operator PacketContainer(FriendshipCreatedPacket packet)
	{
		return new PacketContainer
		{
			FriendshipCreated = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public FriendshipCreatedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public FriendshipCreatedPacket(FriendshipCreatedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		friendUserId_ = ((other.friendUserId_ != null) ? other.friendUserId_.Clone() : null);
		friendshipGroupId_ = ((other.friendshipGroupId_ != null) ? other.friendshipGroupId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FriendshipCreatedPacket Clone()
	{
		return new FriendshipCreatedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendshipCreatedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FriendshipCreatedPacket other)
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
		if (!object.Equals(FriendUserId, other.FriendUserId))
		{
			return false;
		}
		if (!object.Equals(FriendshipGroupId, other.FriendshipGroupId))
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
		if (friendUserId_ != null)
		{
			num ^= FriendUserId.GetHashCode();
		}
		if (friendshipGroupId_ != null)
		{
			num ^= FriendshipGroupId.GetHashCode();
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
		if (friendUserId_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(FriendUserId);
		}
		if (friendshipGroupId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(FriendshipGroupId);
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
		if (friendUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FriendUserId);
		}
		if (friendshipGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(FriendshipGroupId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendshipCreatedPacket other)
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
				Id = new FriendshipUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.friendUserId_ != null)
		{
			if (friendUserId_ == null)
			{
				FriendUserId = new UserUuid();
			}
			FriendUserId.MergeFrom(other.FriendUserId);
		}
		if (other.friendshipGroupId_ != null)
		{
			if (friendshipGroupId_ == null)
			{
				FriendshipGroupId = new FriendshipGroupUuid();
			}
			FriendshipGroupId.MergeFrom(other.FriendshipGroupId);
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
					Id = new FriendshipUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 34u:
				if (friendUserId_ == null)
				{
					FriendUserId = new UserUuid();
				}
				P_0.ReadMessage(FriendUserId);
				break;
			case 42u:
				if (friendshipGroupId_ == null)
				{
					FriendshipGroupId = new FriendshipGroupUuid();
				}
				P_0.ReadMessage(FriendshipGroupId);
				break;
			}
		}
	}
}
