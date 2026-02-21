using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class DirectMessageMemberAddedPacket : IPacket, IMessage<DirectMessageMemberAddedPacket>, IMessage, IEquatable<DirectMessageMemberAddedPacket>, IDeepCloneable<DirectMessageMemberAddedPacket>, IBufferMessage
{
	private static readonly MessageParser<DirectMessageMemberAddedPacket> _parser = new MessageParser<DirectMessageMemberAddedPacket>(() => new DirectMessageMemberAddedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private UserUuid agentId_;

	private DirectMessageUuid id_;

	private static readonly FieldCodec<UserUuid> _repeated_memberUserIds_codec = FieldCodec.ForMessage(42u, UserUuid.Parser);

	private readonly RepeatedField<UserUuid> memberUserIds_ = new RepeatedField<UserUuid>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<DirectMessageMemberAddedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => DirectMessageReflection.Descriptor.MessageTypes[1];

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
	public UserUuid AgentId
	{
		get
		{
			return agentId_;
		}
		set
		{
			agentId_ = value;
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

	public static implicit operator PacketContainer(DirectMessageMemberAddedPacket packet)
	{
		return new PacketContainer
		{
			DirectMessageMemberAdded = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageMemberAddedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageMemberAddedPacket(DirectMessageMemberAddedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		agentId_ = ((other.agentId_ != null) ? other.agentId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		memberUserIds_ = other.memberUserIds_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageMemberAddedPacket Clone()
	{
		return new DirectMessageMemberAddedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as DirectMessageMemberAddedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(DirectMessageMemberAddedPacket other)
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
		if (!object.Equals(AgentId, other.AgentId))
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
		if (agentId_ != null)
		{
			num ^= AgentId.GetHashCode();
		}
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		num ^= memberUserIds_.GetHashCode();
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
		if (agentId_ != null)
		{
			P_0.WriteRawTag(26);
			P_0.WriteMessage(AgentId);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		memberUserIds_.WriteTo(ref P_0, _repeated_memberUserIds_codec);
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
		if (agentId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AgentId);
		}
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		num += memberUserIds_.CalculateSize(_repeated_memberUserIds_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(DirectMessageMemberAddedPacket other)
	{
		if (other == null)
		{
			return;
		}
		if (other.PacketType != PacketType.Unspecified)
		{
			PacketType = other.PacketType;
		}
		if (other.agentId_ != null)
		{
			if (agentId_ == null)
			{
				AgentId = new UserUuid();
			}
			AgentId.MergeFrom(other.AgentId);
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
				if (agentId_ == null)
				{
					AgentId = new UserUuid();
				}
				P_0.ReadMessage(AgentId);
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
			}
		}
	}
}
