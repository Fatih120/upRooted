using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Payloads.Message;

public sealed class MessagePayloadItem : IMessage<MessagePayloadItem>, IMessage, IEquatable<MessagePayloadItem>, IDeepCloneable<MessagePayloadItem>, IBufferMessage
{
	public enum ItemOneofCase
	{
		None = 0,
		Community = 4,
		Channel = 5,
		MessagePinned = 7,
		MessageUnPinned = 8,
		CommunityMemberBanned = 10,
		CommunityMemberJoined = 11,
		CommunityMemberKicked = 12,
		DirectMessageUserCallStarted = 14,
		DirectMessageUserCallEnded = 15,
		DirectMessageUsersJoined = 16,
		DirectMessageUserLeft = 17
	}

	private static readonly MessageParser<MessagePayloadItem> _parser = new MessageParser<MessagePayloadItem>(() => new MessagePayloadItem());

	private UnknownFieldSet _unknownFields;

	private object item_;

	private ItemOneofCase itemCase_ = ItemOneofCase.None;

	[GeneratedCode("protoc", null)]
	public static MessageParser<MessagePayloadItem> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => MessagesReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public MessagePayloadCommunity Community
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Community) ? ((MessagePayloadCommunity)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Community : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadChannel Channel
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Channel) ? ((MessagePayloadChannel)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Channel : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadMessagePinned MessagePinned
	{
		get
		{
			return (itemCase_ == ItemOneofCase.MessagePinned) ? ((MessagePayloadMessagePinned)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.MessagePinned : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadMessageUnPinned MessageUnPinned
	{
		get
		{
			return (itemCase_ == ItemOneofCase.MessageUnPinned) ? ((MessagePayloadMessageUnPinned)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.MessageUnPinned : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadCommunityMemberBanned CommunityMemberBanned
	{
		get
		{
			return (itemCase_ == ItemOneofCase.CommunityMemberBanned) ? ((MessagePayloadCommunityMemberBanned)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.CommunityMemberBanned : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadCommunityMemberJoined CommunityMemberJoined
	{
		get
		{
			return (itemCase_ == ItemOneofCase.CommunityMemberJoined) ? ((MessagePayloadCommunityMemberJoined)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.CommunityMemberJoined : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadCommunityMemberKicked CommunityMemberKicked
	{
		get
		{
			return (itemCase_ == ItemOneofCase.CommunityMemberKicked) ? ((MessagePayloadCommunityMemberKicked)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.CommunityMemberKicked : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageCallStarted DirectMessageUserCallStarted
	{
		get
		{
			return (itemCase_ == ItemOneofCase.DirectMessageUserCallStarted) ? ((MessagePayloadDirectMessageCallStarted)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.DirectMessageUserCallStarted : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageCallEnded DirectMessageUserCallEnded
	{
		get
		{
			return (itemCase_ == ItemOneofCase.DirectMessageUserCallEnded) ? ((MessagePayloadDirectMessageCallEnded)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.DirectMessageUserCallEnded : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageUsersJoined DirectMessageUsersJoined
	{
		get
		{
			return (itemCase_ == ItemOneofCase.DirectMessageUsersJoined) ? ((MessagePayloadDirectMessageUsersJoined)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.DirectMessageUsersJoined : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadDirectMessageUserLeft DirectMessageUserLeft
	{
		get
		{
			return (itemCase_ == ItemOneofCase.DirectMessageUserLeft) ? ((MessagePayloadDirectMessageUserLeft)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.DirectMessageUserLeft : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public ItemOneofCase ItemCase => itemCase_;

	[GeneratedCode("protoc", null)]
	public MessagePayloadItem()
	{
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadItem(MessagePayloadItem other)
		: this()
	{
		switch (other.ItemCase)
		{
		case ItemOneofCase.Community:
			Community = other.Community.Clone();
			break;
		case ItemOneofCase.Channel:
			Channel = other.Channel.Clone();
			break;
		case ItemOneofCase.MessagePinned:
			MessagePinned = other.MessagePinned.Clone();
			break;
		case ItemOneofCase.MessageUnPinned:
			MessageUnPinned = other.MessageUnPinned.Clone();
			break;
		case ItemOneofCase.CommunityMemberBanned:
			CommunityMemberBanned = other.CommunityMemberBanned.Clone();
			break;
		case ItemOneofCase.CommunityMemberJoined:
			CommunityMemberJoined = other.CommunityMemberJoined.Clone();
			break;
		case ItemOneofCase.CommunityMemberKicked:
			CommunityMemberKicked = other.CommunityMemberKicked.Clone();
			break;
		case ItemOneofCase.DirectMessageUserCallStarted:
			DirectMessageUserCallStarted = other.DirectMessageUserCallStarted.Clone();
			break;
		case ItemOneofCase.DirectMessageUserCallEnded:
			DirectMessageUserCallEnded = other.DirectMessageUserCallEnded.Clone();
			break;
		case ItemOneofCase.DirectMessageUsersJoined:
			DirectMessageUsersJoined = other.DirectMessageUsersJoined.Clone();
			break;
		case ItemOneofCase.DirectMessageUserLeft:
			DirectMessageUserLeft = other.DirectMessageUserLeft.Clone();
			break;
		}
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public MessagePayloadItem Clone()
	{
		return new MessagePayloadItem(this);
	}

	[GeneratedCode("protoc", null)]
	public void ClearItem()
	{
		itemCase_ = ItemOneofCase.None;
		item_ = null;
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as MessagePayloadItem);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(MessagePayloadItem other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Community, other.Community))
		{
			return false;
		}
		if (!object.Equals(Channel, other.Channel))
		{
			return false;
		}
		if (!object.Equals(MessagePinned, other.MessagePinned))
		{
			return false;
		}
		if (!object.Equals(MessageUnPinned, other.MessageUnPinned))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberBanned, other.CommunityMemberBanned))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberJoined, other.CommunityMemberJoined))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberKicked, other.CommunityMemberKicked))
		{
			return false;
		}
		if (!object.Equals(DirectMessageUserCallStarted, other.DirectMessageUserCallStarted))
		{
			return false;
		}
		if (!object.Equals(DirectMessageUserCallEnded, other.DirectMessageUserCallEnded))
		{
			return false;
		}
		if (!object.Equals(DirectMessageUsersJoined, other.DirectMessageUsersJoined))
		{
			return false;
		}
		if (!object.Equals(DirectMessageUserLeft, other.DirectMessageUserLeft))
		{
			return false;
		}
		if (ItemCase != other.ItemCase)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (itemCase_ == ItemOneofCase.Community)
		{
			num ^= Community.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Channel)
		{
			num ^= Channel.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.MessagePinned)
		{
			num ^= MessagePinned.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.MessageUnPinned)
		{
			num ^= MessageUnPinned.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.CommunityMemberBanned)
		{
			num ^= CommunityMemberBanned.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.CommunityMemberJoined)
		{
			num ^= CommunityMemberJoined.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.CommunityMemberKicked)
		{
			num ^= CommunityMemberKicked.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUserCallStarted)
		{
			num ^= DirectMessageUserCallStarted.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUserCallEnded)
		{
			num ^= DirectMessageUserCallEnded.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUsersJoined)
		{
			num ^= DirectMessageUsersJoined.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUserLeft)
		{
			num ^= DirectMessageUserLeft.GetHashCode();
		}
		num ^= (int)itemCase_;
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
		if (itemCase_ == ItemOneofCase.Community)
		{
			P_0.WriteRawTag(34);
			P_0.WriteMessage(Community);
		}
		if (itemCase_ == ItemOneofCase.Channel)
		{
			P_0.WriteRawTag(42);
			P_0.WriteMessage(Channel);
		}
		if (itemCase_ == ItemOneofCase.MessagePinned)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(MessagePinned);
		}
		if (itemCase_ == ItemOneofCase.MessageUnPinned)
		{
			P_0.WriteRawTag(66);
			P_0.WriteMessage(MessageUnPinned);
		}
		if (itemCase_ == ItemOneofCase.CommunityMemberBanned)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(CommunityMemberBanned);
		}
		if (itemCase_ == ItemOneofCase.CommunityMemberJoined)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(CommunityMemberJoined);
		}
		if (itemCase_ == ItemOneofCase.CommunityMemberKicked)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(CommunityMemberKicked);
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUserCallStarted)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(DirectMessageUserCallStarted);
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUserCallEnded)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(DirectMessageUserCallEnded);
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUsersJoined)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(DirectMessageUsersJoined);
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUserLeft)
		{
			P_0.WriteRawTag(138, 1);
			P_0.WriteMessage(DirectMessageUserLeft);
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
		if (itemCase_ == ItemOneofCase.Community)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Community);
		}
		if (itemCase_ == ItemOneofCase.Channel)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Channel);
		}
		if (itemCase_ == ItemOneofCase.MessagePinned)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(MessagePinned);
		}
		if (itemCase_ == ItemOneofCase.MessageUnPinned)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(MessageUnPinned);
		}
		if (itemCase_ == ItemOneofCase.CommunityMemberBanned)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityMemberBanned);
		}
		if (itemCase_ == ItemOneofCase.CommunityMemberJoined)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityMemberJoined);
		}
		if (itemCase_ == ItemOneofCase.CommunityMemberKicked)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(CommunityMemberKicked);
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUserCallStarted)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DirectMessageUserCallStarted);
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUserCallEnded)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(DirectMessageUserCallEnded);
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUsersJoined)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DirectMessageUsersJoined);
		}
		if (itemCase_ == ItemOneofCase.DirectMessageUserLeft)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DirectMessageUserLeft);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(MessagePayloadItem other)
	{
		if (other == null)
		{
			return;
		}
		switch (other.ItemCase)
		{
		case ItemOneofCase.Community:
			if (Community == null)
			{
				Community = new MessagePayloadCommunity();
			}
			Community.MergeFrom(other.Community);
			break;
		case ItemOneofCase.Channel:
			if (Channel == null)
			{
				Channel = new MessagePayloadChannel();
			}
			Channel.MergeFrom(other.Channel);
			break;
		case ItemOneofCase.MessagePinned:
			if (MessagePinned == null)
			{
				MessagePinned = new MessagePayloadMessagePinned();
			}
			MessagePinned.MergeFrom(other.MessagePinned);
			break;
		case ItemOneofCase.MessageUnPinned:
			if (MessageUnPinned == null)
			{
				MessageUnPinned = new MessagePayloadMessageUnPinned();
			}
			MessageUnPinned.MergeFrom(other.MessageUnPinned);
			break;
		case ItemOneofCase.CommunityMemberBanned:
			if (CommunityMemberBanned == null)
			{
				CommunityMemberBanned = new MessagePayloadCommunityMemberBanned();
			}
			CommunityMemberBanned.MergeFrom(other.CommunityMemberBanned);
			break;
		case ItemOneofCase.CommunityMemberJoined:
			if (CommunityMemberJoined == null)
			{
				CommunityMemberJoined = new MessagePayloadCommunityMemberJoined();
			}
			CommunityMemberJoined.MergeFrom(other.CommunityMemberJoined);
			break;
		case ItemOneofCase.CommunityMemberKicked:
			if (CommunityMemberKicked == null)
			{
				CommunityMemberKicked = new MessagePayloadCommunityMemberKicked();
			}
			CommunityMemberKicked.MergeFrom(other.CommunityMemberKicked);
			break;
		case ItemOneofCase.DirectMessageUserCallStarted:
			if (DirectMessageUserCallStarted == null)
			{
				DirectMessageUserCallStarted = new MessagePayloadDirectMessageCallStarted();
			}
			DirectMessageUserCallStarted.MergeFrom(other.DirectMessageUserCallStarted);
			break;
		case ItemOneofCase.DirectMessageUserCallEnded:
			if (DirectMessageUserCallEnded == null)
			{
				DirectMessageUserCallEnded = new MessagePayloadDirectMessageCallEnded();
			}
			DirectMessageUserCallEnded.MergeFrom(other.DirectMessageUserCallEnded);
			break;
		case ItemOneofCase.DirectMessageUsersJoined:
			if (DirectMessageUsersJoined == null)
			{
				DirectMessageUsersJoined = new MessagePayloadDirectMessageUsersJoined();
			}
			DirectMessageUsersJoined.MergeFrom(other.DirectMessageUsersJoined);
			break;
		case ItemOneofCase.DirectMessageUserLeft:
			if (DirectMessageUserLeft == null)
			{
				DirectMessageUserLeft = new MessagePayloadDirectMessageUserLeft();
			}
			DirectMessageUserLeft.MergeFrom(other.DirectMessageUserLeft);
			break;
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
			case 34u:
			{
				MessagePayloadCommunity messagePayloadCommunity = new MessagePayloadCommunity();
				if (itemCase_ == ItemOneofCase.Community)
				{
					messagePayloadCommunity.MergeFrom(Community);
				}
				P_0.ReadMessage(messagePayloadCommunity);
				Community = messagePayloadCommunity;
				break;
			}
			case 42u:
			{
				MessagePayloadChannel messagePayloadChannel = new MessagePayloadChannel();
				if (itemCase_ == ItemOneofCase.Channel)
				{
					messagePayloadChannel.MergeFrom(Channel);
				}
				P_0.ReadMessage(messagePayloadChannel);
				Channel = messagePayloadChannel;
				break;
			}
			case 58u:
			{
				MessagePayloadMessagePinned messagePayloadMessagePinned = new MessagePayloadMessagePinned();
				if (itemCase_ == ItemOneofCase.MessagePinned)
				{
					messagePayloadMessagePinned.MergeFrom(MessagePinned);
				}
				P_0.ReadMessage(messagePayloadMessagePinned);
				MessagePinned = messagePayloadMessagePinned;
				break;
			}
			case 66u:
			{
				MessagePayloadMessageUnPinned messagePayloadMessageUnPinned = new MessagePayloadMessageUnPinned();
				if (itemCase_ == ItemOneofCase.MessageUnPinned)
				{
					messagePayloadMessageUnPinned.MergeFrom(MessageUnPinned);
				}
				P_0.ReadMessage(messagePayloadMessageUnPinned);
				MessageUnPinned = messagePayloadMessageUnPinned;
				break;
			}
			case 82u:
			{
				MessagePayloadCommunityMemberBanned messagePayloadCommunityMemberBanned = new MessagePayloadCommunityMemberBanned();
				if (itemCase_ == ItemOneofCase.CommunityMemberBanned)
				{
					messagePayloadCommunityMemberBanned.MergeFrom(CommunityMemberBanned);
				}
				P_0.ReadMessage(messagePayloadCommunityMemberBanned);
				CommunityMemberBanned = messagePayloadCommunityMemberBanned;
				break;
			}
			case 90u:
			{
				MessagePayloadCommunityMemberJoined messagePayloadCommunityMemberJoined = new MessagePayloadCommunityMemberJoined();
				if (itemCase_ == ItemOneofCase.CommunityMemberJoined)
				{
					messagePayloadCommunityMemberJoined.MergeFrom(CommunityMemberJoined);
				}
				P_0.ReadMessage(messagePayloadCommunityMemberJoined);
				CommunityMemberJoined = messagePayloadCommunityMemberJoined;
				break;
			}
			case 98u:
			{
				MessagePayloadCommunityMemberKicked messagePayloadCommunityMemberKicked = new MessagePayloadCommunityMemberKicked();
				if (itemCase_ == ItemOneofCase.CommunityMemberKicked)
				{
					messagePayloadCommunityMemberKicked.MergeFrom(CommunityMemberKicked);
				}
				P_0.ReadMessage(messagePayloadCommunityMemberKicked);
				CommunityMemberKicked = messagePayloadCommunityMemberKicked;
				break;
			}
			case 114u:
			{
				MessagePayloadDirectMessageCallStarted messagePayloadDirectMessageCallStarted = new MessagePayloadDirectMessageCallStarted();
				if (itemCase_ == ItemOneofCase.DirectMessageUserCallStarted)
				{
					messagePayloadDirectMessageCallStarted.MergeFrom(DirectMessageUserCallStarted);
				}
				P_0.ReadMessage(messagePayloadDirectMessageCallStarted);
				DirectMessageUserCallStarted = messagePayloadDirectMessageCallStarted;
				break;
			}
			case 122u:
			{
				MessagePayloadDirectMessageCallEnded messagePayloadDirectMessageCallEnded = new MessagePayloadDirectMessageCallEnded();
				if (itemCase_ == ItemOneofCase.DirectMessageUserCallEnded)
				{
					messagePayloadDirectMessageCallEnded.MergeFrom(DirectMessageUserCallEnded);
				}
				P_0.ReadMessage(messagePayloadDirectMessageCallEnded);
				DirectMessageUserCallEnded = messagePayloadDirectMessageCallEnded;
				break;
			}
			case 130u:
			{
				MessagePayloadDirectMessageUsersJoined messagePayloadDirectMessageUsersJoined = new MessagePayloadDirectMessageUsersJoined();
				if (itemCase_ == ItemOneofCase.DirectMessageUsersJoined)
				{
					messagePayloadDirectMessageUsersJoined.MergeFrom(DirectMessageUsersJoined);
				}
				P_0.ReadMessage(messagePayloadDirectMessageUsersJoined);
				DirectMessageUsersJoined = messagePayloadDirectMessageUsersJoined;
				break;
			}
			case 138u:
			{
				MessagePayloadDirectMessageUserLeft messagePayloadDirectMessageUserLeft = new MessagePayloadDirectMessageUserLeft();
				if (itemCase_ == ItemOneofCase.DirectMessageUserLeft)
				{
					messagePayloadDirectMessageUserLeft.MergeFrom(DirectMessageUserLeft);
				}
				P_0.ReadMessage(messagePayloadDirectMessageUserLeft);
				DirectMessageUserLeft = messagePayloadDirectMessageUserLeft;
				break;
			}
			}
		}
	}
}
