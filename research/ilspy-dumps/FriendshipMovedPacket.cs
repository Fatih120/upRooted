using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class FriendshipMovedPacket : IPacket, IMessage<FriendshipMovedPacket>, IMessage, IEquatable<FriendshipMovedPacket>, IDeepCloneable<FriendshipMovedPacket>, IBufferMessage
{
	private static readonly MessageParser<FriendshipMovedPacket> _parser = new MessageParser<FriendshipMovedPacket>(() => new FriendshipMovedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private FriendshipUuid id_;

	private UserUuid friendUserId_;

	private FriendshipGroupUuid friendshipGroupId_;

	private FriendshipGroupUuid oldFriendshipGroupId_;

	private FriendshipUuid beforeFriendshipId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FriendshipMovedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FriendshipReflection.Descriptor.MessageTypes[1];

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

	[GeneratedCode("protoc", null)]
	public FriendshipGroupUuid OldFriendshipGroupId
	{
		get
		{
			return oldFriendshipGroupId_;
		}
		set
		{
			oldFriendshipGroupId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipUuid BeforeFriendshipId
	{
		get
		{
			return beforeFriendshipId_;
		}
		set
		{
			beforeFriendshipId_ = value;
		}
	}

	public static implicit operator PacketContainer(FriendshipMovedPacket packet)
	{
		return new PacketContainer
		{
			FriendshipMoved = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public FriendshipMovedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public FriendshipMovedPacket(FriendshipMovedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		friendUserId_ = ((other.friendUserId_ != null) ? other.friendUserId_.Clone() : null);
		friendshipGroupId_ = ((other.friendshipGroupId_ != null) ? other.friendshipGroupId_.Clone() : null);
		oldFriendshipGroupId_ = ((other.oldFriendshipGroupId_ != null) ? other.oldFriendshipGroupId_.Clone() : null);
		beforeFriendshipId_ = ((other.beforeFriendshipId_ != null) ? other.beforeFriendshipId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FriendshipMovedPacket Clone()
	{
		return new FriendshipMovedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FriendshipMovedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FriendshipMovedPacket other)
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
		if (!object.Equals(OldFriendshipGroupId, other.OldFriendshipGroupId))
		{
			return false;
		}
		if (!object.Equals(BeforeFriendshipId, other.BeforeFriendshipId))
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
		if (oldFriendshipGroupId_ != null)
		{
			num ^= OldFriendshipGroupId.GetHashCode();
		}
		if (beforeFriendshipId_ != null)
		{
			num ^= BeforeFriendshipId.GetHashCode();
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
		if (oldFriendshipGroupId_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(OldFriendshipGroupId);
		}
		if (beforeFriendshipId_ != null)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(BeforeFriendshipId);
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
		if (oldFriendshipGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(OldFriendshipGroupId);
		}
		if (beforeFriendshipId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeforeFriendshipId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FriendshipMovedPacket other)
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
		if (other.oldFriendshipGroupId_ != null)
		{
			if (oldFriendshipGroupId_ == null)
			{
				OldFriendshipGroupId = new FriendshipGroupUuid();
			}
			OldFriendshipGroupId.MergeFrom(other.OldFriendshipGroupId);
		}
		if (other.beforeFriendshipId_ != null)
		{
			if (beforeFriendshipId_ == null)
			{
				BeforeFriendshipId = new FriendshipUuid();
			}
			BeforeFriendshipId.MergeFrom(other.BeforeFriendshipId);
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
			case 50u:
				if (oldFriendshipGroupId_ == null)
				{
					OldFriendshipGroupId = new FriendshipGroupUuid();
				}
				P_0.ReadMessage(OldFriendshipGroupId);
				break;
			case 58u:
				if (beforeFriendshipId_ == null)
				{
					BeforeFriendshipId = new FriendshipUuid();
				}
				P_0.ReadMessage(BeforeFriendshipId);
				break;
			}
		}
	}
}
