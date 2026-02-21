using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class ChannelGroupMovedPacket : IPacketCommunity, IPacket, IMessage<ChannelGroupMovedPacket>, IMessage, IEquatable<ChannelGroupMovedPacket>, IDeepCloneable<ChannelGroupMovedPacket>, IBufferMessage
{
	private static readonly MessageParser<ChannelGroupMovedPacket> _parser = new MessageParser<ChannelGroupMovedPacket>(() => new ChannelGroupMovedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private ChannelGroupUuid id_;

	private ChannelGroupUuid beforeChannelGroupId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<ChannelGroupMovedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ChannelGroupReflection.Descriptor.MessageTypes[2];

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
	public ChannelGroupUuid Id
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
	public ChannelGroupUuid BeforeChannelGroupId
	{
		get
		{
			return beforeChannelGroupId_;
		}
		set
		{
			beforeChannelGroupId_ = value;
		}
	}

	public static implicit operator PacketContainer(ChannelGroupMovedPacket packet)
	{
		return new PacketContainer
		{
			ChannelGroupMoved = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupMovedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupMovedPacket(ChannelGroupMovedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		beforeChannelGroupId_ = ((other.beforeChannelGroupId_ != null) ? other.beforeChannelGroupId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupMovedPacket Clone()
	{
		return new ChannelGroupMovedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as ChannelGroupMovedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(ChannelGroupMovedPacket other)
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
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(BeforeChannelGroupId, other.BeforeChannelGroupId))
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
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (beforeChannelGroupId_ != null)
		{
			num ^= BeforeChannelGroupId.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Id);
		}
		if (beforeChannelGroupId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(BeforeChannelGroupId);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (beforeChannelGroupId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(BeforeChannelGroupId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(ChannelGroupMovedPacket other)
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
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new ChannelGroupUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.beforeChannelGroupId_ != null)
		{
			if (beforeChannelGroupId_ == null)
			{
				BeforeChannelGroupId = new ChannelGroupUuid();
			}
			BeforeChannelGroupId.MergeFrom(other.BeforeChannelGroupId);
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
				if (id_ == null)
				{
					Id = new ChannelGroupUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 42u:
				if (beforeChannelGroupId_ == null)
				{
					BeforeChannelGroupId = new ChannelGroupUuid();
				}
				P_0.ReadMessage(BeforeChannelGroupId);
				break;
			}
		}
	}
}
