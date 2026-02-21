using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public sealed class FileMovedPacket : IPacketCommunity, IPacket, IMessage<FileMovedPacket>, IMessage, IEquatable<FileMovedPacket>, IDeepCloneable<FileMovedPacket>, IBufferMessage
{
	private static readonly MessageParser<FileMovedPacket> _parser = new MessageParser<FileMovedPacket>(() => new FileMovedPacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private MessageContainerUuid containerId_;

	private FileUuid id_;

	private DirectoryUuid directoryId_;

	private DirectoryUuid oldDirectoryId_;

	[GeneratedCode("protoc", null)]
	public static MessageParser<FileMovedPacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => FileReflection.Descriptor.MessageTypes[2];

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
	public FileUuid Id
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
	public DirectoryUuid DirectoryId
	{
		get
		{
			return directoryId_;
		}
		set
		{
			directoryId_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectoryUuid OldDirectoryId
	{
		get
		{
			return oldDirectoryId_;
		}
		set
		{
			oldDirectoryId_ = value;
		}
	}

	public static implicit operator PacketContainer(FileMovedPacket packet)
	{
		return new PacketContainer
		{
			FileMoved = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public FileMovedPacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public FileMovedPacket(FileMovedPacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		directoryId_ = ((other.directoryId_ != null) ? other.directoryId_.Clone() : null);
		oldDirectoryId_ = ((other.oldDirectoryId_ != null) ? other.oldDirectoryId_.Clone() : null);
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public FileMovedPacket Clone()
	{
		return new FileMovedPacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as FileMovedPacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(FileMovedPacket other)
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
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(DirectoryId, other.DirectoryId))
		{
			return false;
		}
		if (!object.Equals(OldDirectoryId, other.OldDirectoryId))
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
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (directoryId_ != null)
		{
			num ^= DirectoryId.GetHashCode();
		}
		if (oldDirectoryId_ != null)
		{
			num ^= OldDirectoryId.GetHashCode();
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
			P_0.WriteRawTag(34);
			P_0.WriteMessage(CommunityId);
		}
		if (containerId_ != null)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(ContainerId);
		}
		if (id_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(Id);
		}
		if (directoryId_ != null)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(DirectoryId);
		}
		if (oldDirectoryId_ != null)
		{
			P_0.WriteRawTag(66);
			P_0.WriteMessage(OldDirectoryId);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (directoryId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DirectoryId);
		}
		if (oldDirectoryId_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(OldDirectoryId);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(FileMovedPacket other)
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
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new FileUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.directoryId_ != null)
		{
			if (directoryId_ == null)
			{
				DirectoryId = new DirectoryUuid();
			}
			DirectoryId.MergeFrom(other.DirectoryId);
		}
		if (other.oldDirectoryId_ != null)
		{
			if (oldDirectoryId_ == null)
			{
				OldDirectoryId = new DirectoryUuid();
			}
			OldDirectoryId.MergeFrom(other.OldDirectoryId);
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
				if (communityId_ == null)
				{
					CommunityId = new CommunityUuid();
				}
				P_0.ReadMessage(CommunityId);
				break;
			case 42u:
				if (containerId_ == null)
				{
					ContainerId = new MessageContainerUuid();
				}
				P_0.ReadMessage(ContainerId);
				break;
			case 50u:
				if (id_ == null)
				{
					Id = new FileUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 58u:
				if (directoryId_ == null)
				{
					DirectoryId = new DirectoryUuid();
				}
				P_0.ReadMessage(DirectoryId);
				break;
			case 66u:
				if (oldDirectoryId_ == null)
				{
					OldDirectoryId = new DirectoryUuid();
				}
				P_0.ReadMessage(OldDirectoryId);
				break;
			}
		}
	}
}
