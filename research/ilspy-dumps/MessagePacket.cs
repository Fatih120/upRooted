using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Payloads.Message;

namespace RootApp.WebApi.Shared.Packets;

public sealed class MessagePacket : IPacketCommunity, IPacket, IMessage<MessagePacket>, IMessage, IEquatable<MessagePacket>, IDeepCloneable<MessagePacket>, IBufferMessage
{
	private static readonly MessageParser<MessagePacket> _parser = new MessageParser<MessagePacket>(() => new MessagePacket());

	private UnknownFieldSet _unknownFields;

	private PacketType packetType_ = PacketType.Unspecified;

	private CommunityUuid communityId_;

	private MessageContainerUuid containerId_;

	private UserUuid userId_;

	private MessageUuid id_;

	private Timestamp deletedAt_;

	private Timestamp editedAt_;

	private Timestamp pinnedAt_;

	private string messageContent_ = "";

	private MessageType messageType_ = MessageType.Unspecified;

	private MessagePayload payload_;

	private static readonly FieldCodec<MessageUri> _repeated_messageUris_codec = FieldCodec.ForMessage(106u, MessageUri.Parser);

	private readonly RepeatedField<MessageUri> messageUris_ = new RepeatedField<MessageUri>();

	private MessageReferenceMaps referenceMaps_;

	private static readonly FieldCodec<MessageReaction> _repeated_reactions_codec = FieldCodec.ForMessage(122u, MessageReaction.Parser);

	private readonly RepeatedField<MessageReaction> reactions_ = new RepeatedField<MessageReaction>();

	private static readonly FieldCodec<ParentMessage> _repeated_parentMessages_codec = FieldCodec.ForMessage(130u, ParentMessage.Parser);

	private readonly RepeatedField<ParentMessage> parentMessages_ = new RepeatedField<ParentMessage>();

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessagePacket> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessageReflection.Descriptor.MessageTypes[3];

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
	public MessageUuid Id
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
	public Timestamp DeletedAt
	{
		get
		{
			return deletedAt_;
		}
		set
		{
			deletedAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp EditedAt
	{
		get
		{
			return editedAt_;
		}
		set
		{
			editedAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public Timestamp PinnedAt
	{
		get
		{
			return pinnedAt_;
		}
		set
		{
			pinnedAt_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public string MessageContent
	{
		get
		{
			return messageContent_;
		}
		set
		{
			messageContent_ = ProtoPreconditions.CheckNotNull(value, "value");
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageType MessageType
	{
		get
		{
			return messageType_;
		}
		set
		{
			messageType_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayload Payload
	{
		get
		{
			return payload_;
		}
		set
		{
			payload_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<MessageUri> MessageUris => messageUris_;

	[GeneratedCode("protoc", null)]
	public MessageReferenceMaps ReferenceMaps
	{
		get
		{
			return referenceMaps_;
		}
		set
		{
			referenceMaps_ = value;
		}
	}

	[GeneratedCode("protoc", null)]
	public RepeatedField<MessageReaction> Reactions => reactions_;

	[GeneratedCode("protoc", null)]
	public RepeatedField<ParentMessage> ParentMessages => parentMessages_;

	public static implicit operator PacketContainer(MessagePacket packet)
	{
		return new PacketContainer
		{
			Message = packet
		};
	}

	[GeneratedCode("protoc", null)]
	public MessagePacket()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessagePacket(MessagePacket other)
		: this()
	{
		packetType_ = other.packetType_;
		communityId_ = ((other.communityId_ != null) ? other.communityId_.Clone() : null);
		containerId_ = ((other.containerId_ != null) ? other.containerId_.Clone() : null);
		userId_ = ((other.userId_ != null) ? other.userId_.Clone() : null);
		id_ = ((other.id_ != null) ? other.id_.Clone() : null);
		deletedAt_ = ((other.deletedAt_ != null) ? other.deletedAt_.Clone() : null);
		editedAt_ = ((other.editedAt_ != null) ? other.editedAt_.Clone() : null);
		pinnedAt_ = ((other.pinnedAt_ != null) ? other.pinnedAt_.Clone() : null);
		messageContent_ = other.messageContent_;
		messageType_ = other.messageType_;
		payload_ = ((other.payload_ != null) ? other.payload_.Clone() : null);
		messageUris_ = other.messageUris_.Clone();
		referenceMaps_ = ((other.referenceMaps_ != null) ? other.referenceMaps_.Clone() : null);
		reactions_ = other.reactions_.Clone();
		parentMessages_ = other.parentMessages_.Clone();
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessagePacket Clone()
	{
		return new MessagePacket(this);
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessagePacket);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessagePacket other)
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
		if (!object.Equals(Id, other.Id))
		{
			return false;
		}
		if (!object.Equals(DeletedAt, other.DeletedAt))
		{
			return false;
		}
		if (!object.Equals(EditedAt, other.EditedAt))
		{
			return false;
		}
		if (!object.Equals(PinnedAt, other.PinnedAt))
		{
			return false;
		}
		if (MessageContent != other.MessageContent)
		{
			return false;
		}
		if (MessageType != other.MessageType)
		{
			return false;
		}
		if (!object.Equals(Payload, other.Payload))
		{
			return false;
		}
		if (!messageUris_.Equals(other.messageUris_))
		{
			return false;
		}
		if (!object.Equals(ReferenceMaps, other.ReferenceMaps))
		{
			return false;
		}
		if (!reactions_.Equals(other.reactions_))
		{
			return false;
		}
		if (!parentMessages_.Equals(other.parentMessages_))
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
		if (id_ != null)
		{
			num ^= Id.GetHashCode();
		}
		if (deletedAt_ != null)
		{
			num ^= DeletedAt.GetHashCode();
		}
		if (editedAt_ != null)
		{
			num ^= EditedAt.GetHashCode();
		}
		if (pinnedAt_ != null)
		{
			num ^= PinnedAt.GetHashCode();
		}
		if (MessageContent.Length != 0)
		{
			num ^= MessageContent.GetHashCode();
		}
		if (MessageType != MessageType.Unspecified)
		{
			num ^= MessageType.GetHashCode();
		}
		if (payload_ != null)
		{
			num ^= Payload.GetHashCode();
		}
		num ^= messageUris_.GetHashCode();
		if (referenceMaps_ != null)
		{
			num ^= ReferenceMaps.GetHashCode();
		}
		num ^= reactions_.GetHashCode();
		num ^= parentMessages_.GetHashCode();
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
		if (id_ != null)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(Id);
		}
		if (deletedAt_ != null)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(DeletedAt);
		}
		if (editedAt_ != null)
		{
			P_0.WriteRawTag(66);
			P_0.WriteMessage(EditedAt);
		}
		if (pinnedAt_ != null)
		{
			P_0.WriteRawTag(74);
			P_0.WriteMessage(PinnedAt);
		}
		if (MessageContent.Length != 0)
		{
			P_0.WriteRawTag(82);
			P_0.WriteString(MessageContent);
		}
		if (MessageType != MessageType.Unspecified)
		{
			P_0.WriteRawTag(88);
			P_0.WriteEnum((int)MessageType);
		}
		if (payload_ != null)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(Payload);
		}
		messageUris_.WriteTo(ref P_0, _repeated_messageUris_codec);
		if (referenceMaps_ != null)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(ReferenceMaps);
		}
		reactions_.WriteTo(ref P_0, _repeated_reactions_codec);
		parentMessages_.WriteTo(ref P_0, _repeated_parentMessages_codec);
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
		if (id_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Id);
		}
		if (deletedAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DeletedAt);
		}
		if (editedAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(EditedAt);
		}
		if (pinnedAt_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(PinnedAt);
		}
		if (MessageContent.Length != 0)
		{
			num += 1 + CodedOutputStream.ComputeStringSize(MessageContent);
		}
		if (MessageType != MessageType.Unspecified)
		{
			num += 1 + CodedOutputStream.ComputeEnumSize((int)MessageType);
		}
		if (payload_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Payload);
		}
		num += messageUris_.CalculateSize(_repeated_messageUris_codec);
		if (referenceMaps_ != null)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ReferenceMaps);
		}
		num += reactions_.CalculateSize(_repeated_reactions_codec);
		num += parentMessages_.CalculateSize(_repeated_parentMessages_codec);
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessagePacket other)
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
		if (other.id_ != null)
		{
			if (id_ == null)
			{
				Id = new MessageUuid();
			}
			Id.MergeFrom(other.Id);
		}
		if (other.deletedAt_ != null)
		{
			if (deletedAt_ == null)
			{
				DeletedAt = new Timestamp();
			}
			DeletedAt.MergeFrom(other.DeletedAt);
		}
		if (other.editedAt_ != null)
		{
			if (editedAt_ == null)
			{
				EditedAt = new Timestamp();
			}
			EditedAt.MergeFrom(other.EditedAt);
		}
		if (other.pinnedAt_ != null)
		{
			if (pinnedAt_ == null)
			{
				PinnedAt = new Timestamp();
			}
			PinnedAt.MergeFrom(other.PinnedAt);
		}
		if (other.MessageContent.Length != 0)
		{
			MessageContent = other.MessageContent;
		}
		if (other.MessageType != MessageType.Unspecified)
		{
			MessageType = other.MessageType;
		}
		if (other.payload_ != null)
		{
			if (payload_ == null)
			{
				Payload = new MessagePayload();
			}
			Payload.MergeFrom(other.Payload);
		}
		messageUris_.Add(other.messageUris_);
		if (other.referenceMaps_ != null)
		{
			if (referenceMaps_ == null)
			{
				ReferenceMaps = new MessageReferenceMaps();
			}
			ReferenceMaps.MergeFrom(other.ReferenceMaps);
		}
		reactions_.Add(other.reactions_);
		parentMessages_.Add(other.parentMessages_);
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
			case 50u:
				if (id_ == null)
				{
					Id = new MessageUuid();
				}
				P_0.ReadMessage(Id);
				break;
			case 58u:
				if (deletedAt_ == null)
				{
					DeletedAt = new Timestamp();
				}
				P_0.ReadMessage(DeletedAt);
				break;
			case 66u:
				if (editedAt_ == null)
				{
					EditedAt = new Timestamp();
				}
				P_0.ReadMessage(EditedAt);
				break;
			case 74u:
				if (pinnedAt_ == null)
				{
					PinnedAt = new Timestamp();
				}
				P_0.ReadMessage(PinnedAt);
				break;
			case 82u:
				MessageContent = P_0.ReadString();
				break;
			case 88u:
				MessageType = (MessageType)P_0.ReadEnum();
				break;
			case 98u:
				if (payload_ == null)
				{
					Payload = new MessagePayload();
				}
				P_0.ReadMessage(Payload);
				break;
			case 106u:
				messageUris_.AddEntriesFrom(ref P_0, _repeated_messageUris_codec);
				break;
			case 114u:
				if (referenceMaps_ == null)
				{
					ReferenceMaps = new MessageReferenceMaps();
				}
				P_0.ReadMessage(ReferenceMaps);
				break;
			case 122u:
				reactions_.AddEntriesFrom(ref P_0, _repeated_reactions_codec);
				break;
			case 130u:
				parentMessages_.AddEntriesFrom(ref P_0, _repeated_parentMessages_codec);
				break;
			}
		}
	}
}
