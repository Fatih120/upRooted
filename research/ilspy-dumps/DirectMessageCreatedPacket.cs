using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class DirectMessageCreatedPacket : IPacket, IMessage<DirectMessageCreatedPacket>, IMessage, IEquatable<DirectMessageCreatedPacket>, IDeepCloneable<DirectMessageCreatedPacket>, IBufferMessage
{
	private static readonly MessageParser<DirectMessageCreatedPacket> _parser = new MessageParser<DirectMessageCreatedPacket>(() => new DirectMessageCreatedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private UserUuid creatorUserId_;

	private DirectMessageUuid id_;

	private static readonly FieldCodec<UserUuid> _repeated_memberUserIds_codec = FieldCodec.ForMessage(42u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> memberUserIds_ = new RepeatedField<UserUuid>();

	private MessagePacket lastMessage_;

	private static readonly FieldCodec<WebRtcUserDevicePacket> _repeated_webRtcMembers_codec = FieldCodec.ForMessage(58u, WebRtcUserDevicePacket.Parser);

	private readonly RepeatedField<WebRtcUserDevicePacket> webRtcMembers_ = new RepeatedField<WebRtcUserDevicePacket>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectMessageCreatedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectMessageReflection.Descriptor.MessageTypes[0];

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
	public UserUuid CreatorUserId
	{
		get
		{
			return creatorUserId_;
		}
		set
		{
			creatorUserId_ = value;
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
	public RepeatedField<UserUuid> MemberUserIds => memberUserIds_;

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

	[GeneratedCode("protoc", null)]
	public RepeatedField<WebRtcUserDevicePacket> WebRtcMembers => webRtcMembers_;

	public static implicit operator PacketContainer(DirectMessageCreatedPacket packet)
	{
		return new PacketContainer
		{
			DirectMessageCreated = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageCreatedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageCreatedPacket(DirectMessageCreatedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		creatorUserId_ = ((other.creatorUserId_ != null) ? other.creatorUserId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		memberUserIds_ = other.memberUserIds_.Clone();
		lastMessage_ = ((other.lastMessage_ != null) ? other.lastMessage_.Clone() : null);
		webRtcMembers_ = other.webRtcMembers_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageCreatedPacket Clone()
	{
		return new DirectMessageCreatedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectMessageCreatedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectMessageCreatedPacket other)
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
		if (!object.Equals(CreatorUserId, other.CreatorUserId))
		{
			return false;
		}
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!memberUserIds_.Equals(other.memberUserIds_))
		{
			return false;
		}
		if (!object.Equals(LastMessage, other.LastMessage))
		{
			return false;
		}
		if (!webRtcMembers_.Equals(other.webRtcMembers_))
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
		if (creatorUserId_ != null)
		{
			num ^= CreatorUserId.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		num ^= memberUserIds_.GetHashCode();
		if (lastMessage_ != null)
		{
			num ^= LastMessage.GetHashCode();
		}
		num ^= webRtcMembers_.GetHashCode();
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
		if (creatorUserId_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(CreatorUserId);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		memberUserIds_.WriteTo(ref P_0, _repeated_memberUserIds_codec);
		if (lastMessage_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(LastMessage);
		}
		webRtcMembers_.WriteTo(ref P_0, _repeated_webRtcMembers_codec);
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
		if (creatorUserId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CreatorUserId);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		num += memberUserIds_.CalculateSize(_repeated_memberUserIds_codec);
		if (lastMessage_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(LastMessage);
		}
		num += webRtcMembers_.CalculateSize(_repeated_webRtcMembers_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectMessageCreatedPacket other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PacketType != PacketType.Unspecified)
		{
			PacketType = other.PacketType;
		}
		if (other.creatorUserId_ != null)
		{
			if (creatorUserId_ == null)
			{
				CreatorUserId = new UserUuid();
			}
			CreatorUserId.MergeFrom(other.CreatorUserId);
		}
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new DirectMessageUuid();
			}
			Id.MergeFrom(other.Id);
		}
		memberUserIds_.Add(other.memberUserIds_);
		if (other.lastMessage_ != null)
		{
			if (lastMessage_ == null)
			{
				LastMessage = new MessagePacket();
			}
			LastMessage.MergeFrom(other.LastMessage);
		}
		webRtcMembers_.Add(other.webRtcMembers_);
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
				if (creatorUserId_ == null)
				{
					CreatorUserId = new UserUuid();
				}
				P_0.ReadMessage(CreatorUserId);
				break;
			case 34u:
				if (id_ == null)
				{
					Id = new DirectMessageUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				memberUserIds_.AddEntriesFrom(ref P_0, _repeated_memberUserIds_codec);
				break;
			case 50u:
				if (lastMessage_ == null)
				{
					LastMessage = new MessagePacket();
				}
				P_0.ReadMessage(LastMessage);
				break;
			case 58u:
				webRtcMembers_.AddEntriesFrom(ref P_0, _repeated_webRtcMembers_codec);
				break;
			}
		}
	}
}
