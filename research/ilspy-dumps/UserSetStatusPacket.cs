using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class UserSetStatusPacket : IPacket, IMessage<UserSetStatusPacket>, IMessage, IEquatable<UserSetStatusPacket>, IDeepCloneable<UserSetStatusPacket>, IBufferMessage
{
	private static readonly MessageParser<UserSetStatusPacket> _parser = new MessageParser<UserSetStatusPacket>(() => new UserSetStatusPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private UserUuid userId_;

	private UserOnlineStatus onlineStatus_ = UserOnlineStatus.Unspecified;

	[GeneratedCode("protoc", null)]
	public static MessageParser<UserSetStatusPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => UserReflection.Descriptor.MessageTypes[1];

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
	public UserOnlineStatus OnlineStatus
	{
		get
		{
			return onlineStatus_;
		}
		set
		{
			onlineStatus_ = value;
		}
	}

	public static implicit operator PacketContainer(UserSetStatusPacket packet)
	{
		return new PacketContainer
		{
			UserSetStatus = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public UserSetStatusPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public UserSetStatusPacket(UserSetStatusPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		onlineStatus_ = other.onlineStatus_;
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public UserSetStatusPacket Clone()
	{
		return new UserSetStatusPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as UserSetStatusPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(UserSetStatusPacket other)
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
		if (OnlineStatus != other.OnlineStatus)
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
		if (OnlineStatus != UserOnlineStatus.Unspecified)
		{
			num ^= OnlineStatus.GetHashCode();
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
		if (OnlineStatus != UserOnlineStatus.Unspecified)
		{
			P_0.WriteRawTag(32);
			P_0.WriteEnum((int)OnlineStatus);
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
		if (OnlineStatus != UserOnlineStatus.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)OnlineStatus);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(UserSetStatusPacket other)
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
		if (other.OnlineStatus != UserOnlineStatus.Unspecified)
		{
			OnlineStatus = other.OnlineStatus;
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
			case 32u:
				OnlineStatus = (UserOnlineStatus)P_0.ReadEnum();
				break;
			}
		}
	}
}
