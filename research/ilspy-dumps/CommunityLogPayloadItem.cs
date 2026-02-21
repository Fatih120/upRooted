using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Payloads.CommunityLog;

public sealed class CommunityLogPayloadItem : IMessage<CommunityLogPayloadItem>, IMessage, IEquatable<CommunityLogPayloadItem>, IDeepCloneable<CommunityLogPayloadItem>, IBufferMessage
{
	public enum ItemOneofCase
	{
		None = 0,
		Community = 4,
		Channel = 5,
		ChannelGroup = 6,
		Role = 7,
		MemberRole = 8,
		AccessRule = 9,
		Message = 10,
		Directory = 11,
		File = 12,
		Member = 13,
		MemberKicked = 14,
		MemberBanned = 15,
		MemberUnBanned = 16,
		MemberInvited = 17,
		App = 18,
		MemberJoined = 19
	}

	private static readonly MessageParser<CommunityLogPayloadItem> _parser = new MessageParser<CommunityLogPayloadItem>(() => new CommunityLogPayloadItem());

	private UnknownFieldSet _unknownFields;

	private object item_;

	private ItemOneofCase itemCase_ = ItemOneofCase.None;

	[GeneratedCode("protoc", null)]
	public static MessageParser<CommunityLogPayloadItem> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => CommunityLogReflection.Descriptor.MessageTypes[1];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadCommunity Community
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Community) ? ((CommunityLogPayloadCommunity)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Community : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadChannel Channel
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Channel) ? ((CommunityLogPayloadChannel)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Channel : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadChannelGroup ChannelGroup
	{
		get
		{
			return (itemCase_ == ItemOneofCase.ChannelGroup) ? ((CommunityLogPayloadChannelGroup)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.ChannelGroup : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadRole Role
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Role) ? ((CommunityLogPayloadRole)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Role : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberRole MemberRole
	{
		get
		{
			return (itemCase_ == ItemOneofCase.MemberRole) ? ((CommunityLogPayloadMemberRole)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.MemberRole : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadAccessRule AccessRule
	{
		get
		{
			return (itemCase_ == ItemOneofCase.AccessRule) ? ((CommunityLogPayloadAccessRule)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.AccessRule : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMessage Message
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Message) ? ((CommunityLogPayloadMessage)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Message : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadDirectory Directory
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Directory) ? ((CommunityLogPayloadDirectory)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Directory : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadFile File
	{
		get
		{
			return (itemCase_ == ItemOneofCase.File) ? ((CommunityLogPayloadFile)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.File : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMember Member
	{
		get
		{
			return (itemCase_ == ItemOneofCase.Member) ? ((CommunityLogPayloadMember)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.Member : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberKicked MemberKicked
	{
		get
		{
			return (itemCase_ == ItemOneofCase.MemberKicked) ? ((CommunityLogPayloadMemberKicked)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.MemberKicked : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberBanned MemberBanned
	{
		get
		{
			return (itemCase_ == ItemOneofCase.MemberBanned) ? ((CommunityLogPayloadMemberBanned)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.MemberBanned : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberUnBanned MemberUnBanned
	{
		get
		{
			return (itemCase_ == ItemOneofCase.MemberUnBanned) ? ((CommunityLogPayloadMemberUnBanned)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.MemberUnBanned : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberInvited MemberInvited
	{
		get
		{
			return (itemCase_ == ItemOneofCase.MemberInvited) ? ((CommunityLogPayloadMemberInvited)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.MemberInvited : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadApp App
	{
		get
		{
			return (itemCase_ == ItemOneofCase.App) ? ((CommunityLogPayloadApp)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.App : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadMemberJoined MemberJoined
	{
		get
		{
			return (itemCase_ == ItemOneofCase.MemberJoined) ? ((CommunityLogPayloadMemberJoined)item_) : null;
		}
		set
		{
			item_ = value;
			itemCase_ = ((value != null) ? ItemOneofCase.MemberJoined : ItemOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public ItemOneofCase ItemCase => itemCase_;

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadItem()
	{
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadItem(CommunityLogPayloadItem other)
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
		case ItemOneofCase.ChannelGroup:
			ChannelGroup = other.ChannelGroup.Clone();
			break;
		case ItemOneofCase.Role:
			Role = other.Role.Clone();
			break;
		case ItemOneofCase.MemberRole:
			MemberRole = other.MemberRole.Clone();
			break;
		case ItemOneofCase.AccessRule:
			AccessRule = other.AccessRule.Clone();
			break;
		case ItemOneofCase.Message:
			Message = other.Message.Clone();
			break;
		case ItemOneofCase.Directory:
			Directory = other.Directory.Clone();
			break;
		case ItemOneofCase.File:
			File = other.File.Clone();
			break;
		case ItemOneofCase.Member:
			Member = other.Member.Clone();
			break;
		case ItemOneofCase.MemberKicked:
			MemberKicked = other.MemberKicked.Clone();
			break;
		case ItemOneofCase.MemberBanned:
			MemberBanned = other.MemberBanned.Clone();
			break;
		case ItemOneofCase.MemberUnBanned:
			MemberUnBanned = other.MemberUnBanned.Clone();
			break;
		case ItemOneofCase.MemberInvited:
			MemberInvited = other.MemberInvited.Clone();
			break;
		case ItemOneofCase.App:
			App = other.App.Clone();
			break;
		case ItemOneofCase.MemberJoined:
			MemberJoined = other.MemberJoined.Clone();
			break;
		}
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public CommunityLogPayloadItem Clone()
	{
		return new CommunityLogPayloadItem(this);
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
		return Equals(other as CommunityLogPayloadItem);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(CommunityLogPayloadItem other)
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
		if (!object.Equals(ChannelGroup, other.ChannelGroup))
		{
			return false;
		}
		if (!object.Equals(Role, other.Role))
		{
			return false;
		}
		if (!object.Equals(MemberRole, other.MemberRole))
		{
			return false;
		}
		if (!object.Equals(AccessRule, other.AccessRule))
		{
			return false;
		}
		if (!object.Equals(Message, other.Message))
		{
			return false;
		}
		if (!object.Equals(Directory, other.Directory))
		{
			return false;
		}
		if (!object.Equals(File, other.File))
		{
			return false;
		}
		if (!object.Equals(Member, other.Member))
		{
			return false;
		}
		if (!object.Equals(MemberKicked, other.MemberKicked))
		{
			return false;
		}
		if (!object.Equals(MemberBanned, other.MemberBanned))
		{
			return false;
		}
		if (!object.Equals(MemberUnBanned, other.MemberUnBanned))
		{
			return false;
		}
		if (!object.Equals(MemberInvited, other.MemberInvited))
		{
			return false;
		}
		if (!object.Equals(App, other.App))
		{
			return false;
		}
		if (!object.Equals(MemberJoined, other.MemberJoined))
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
		if (itemCase_ == ItemOneofCase.ChannelGroup)
		{
			num ^= ChannelGroup.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Role)
		{
			num ^= Role.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.MemberRole)
		{
			num ^= MemberRole.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.AccessRule)
		{
			num ^= AccessRule.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Message)
		{
			num ^= Message.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Directory)
		{
			num ^= Directory.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.File)
		{
			num ^= File.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.Member)
		{
			num ^= Member.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.MemberKicked)
		{
			num ^= MemberKicked.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.MemberBanned)
		{
			num ^= MemberBanned.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.MemberUnBanned)
		{
			num ^= MemberUnBanned.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.MemberInvited)
		{
			num ^= MemberInvited.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.App)
		{
			num ^= App.GetHashCode();
		}
		if (itemCase_ == ItemOneofCase.MemberJoined)
		{
			num ^= MemberJoined.GetHashCode();
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
		if (itemCase_ == ItemOneofCase.ChannelGroup)
		{
			P_0.WriteRawTag(50);
			P_0.WriteMessage(ChannelGroup);
		}
		if (itemCase_ == ItemOneofCase.Role)
		{
			P_0.WriteRawTag(58);
			P_0.WriteMessage(Role);
		}
		if (itemCase_ == ItemOneofCase.MemberRole)
		{
			P_0.WriteRawTag(66);
			P_0.WriteMessage(MemberRole);
		}
		if (itemCase_ == ItemOneofCase.AccessRule)
		{
			P_0.WriteRawTag(74);
			P_0.WriteMessage(AccessRule);
		}
		if (itemCase_ == ItemOneofCase.Message)
		{
			P_0.WriteRawTag(82);
			P_0.WriteMessage(Message);
		}
		if (itemCase_ == ItemOneofCase.Directory)
		{
			P_0.WriteRawTag(90);
			P_0.WriteMessage(Directory);
		}
		if (itemCase_ == ItemOneofCase.File)
		{
			P_0.WriteRawTag(98);
			P_0.WriteMessage(File);
		}
		if (itemCase_ == ItemOneofCase.Member)
		{
			P_0.WriteRawTag(106);
			P_0.WriteMessage(Member);
		}
		if (itemCase_ == ItemOneofCase.MemberKicked)
		{
			P_0.WriteRawTag(114);
			P_0.WriteMessage(MemberKicked);
		}
		if (itemCase_ == ItemOneofCase.MemberBanned)
		{
			P_0.WriteRawTag(122);
			P_0.WriteMessage(MemberBanned);
		}
		if (itemCase_ == ItemOneofCase.MemberUnBanned)
		{
			P_0.WriteRawTag(130, 1);
			P_0.WriteMessage(MemberUnBanned);
		}
		if (itemCase_ == ItemOneofCase.MemberInvited)
		{
			P_0.WriteRawTag(138, 1);
			P_0.WriteMessage(MemberInvited);
		}
		if (itemCase_ == ItemOneofCase.App)
		{
			P_0.WriteRawTag(146, 1);
			P_0.WriteMessage(App);
		}
		if (itemCase_ == ItemOneofCase.MemberJoined)
		{
			P_0.WriteRawTag(154, 1);
			P_0.WriteMessage(MemberJoined);
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
		if (itemCase_ == ItemOneofCase.ChannelGroup)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(ChannelGroup);
		}
		if (itemCase_ == ItemOneofCase.Role)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Role);
		}
		if (itemCase_ == ItemOneofCase.MemberRole)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(MemberRole);
		}
		if (itemCase_ == ItemOneofCase.AccessRule)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(AccessRule);
		}
		if (itemCase_ == ItemOneofCase.Message)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Message);
		}
		if (itemCase_ == ItemOneofCase.Directory)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Directory);
		}
		if (itemCase_ == ItemOneofCase.File)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(File);
		}
		if (itemCase_ == ItemOneofCase.Member)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Member);
		}
		if (itemCase_ == ItemOneofCase.MemberKicked)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(MemberKicked);
		}
		if (itemCase_ == ItemOneofCase.MemberBanned)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(MemberBanned);
		}
		if (itemCase_ == ItemOneofCase.MemberUnBanned)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MemberUnBanned);
		}
		if (itemCase_ == ItemOneofCase.MemberInvited)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MemberInvited);
		}
		if (itemCase_ == ItemOneofCase.App)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(App);
		}
		if (itemCase_ == ItemOneofCase.MemberJoined)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MemberJoined);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(CommunityLogPayloadItem other)
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
				Community = new CommunityLogPayloadCommunity();
			}
			Community.MergeFrom(other.Community);
			break;
		case ItemOneofCase.Channel:
			if (Channel == null)
			{
				Channel = new CommunityLogPayloadChannel();
			}
			Channel.MergeFrom(other.Channel);
			break;
		case ItemOneofCase.ChannelGroup:
			if (ChannelGroup == null)
			{
				ChannelGroup = new CommunityLogPayloadChannelGroup();
			}
			ChannelGroup.MergeFrom(other.ChannelGroup);
			break;
		case ItemOneofCase.Role:
			if (Role == null)
			{
				Role = new CommunityLogPayloadRole();
			}
			Role.MergeFrom(other.Role);
			break;
		case ItemOneofCase.MemberRole:
			if (MemberRole == null)
			{
				MemberRole = new CommunityLogPayloadMemberRole();
			}
			MemberRole.MergeFrom(other.MemberRole);
			break;
		case ItemOneofCase.AccessRule:
			if (AccessRule == null)
			{
				AccessRule = new CommunityLogPayloadAccessRule();
			}
			AccessRule.MergeFrom(other.AccessRule);
			break;
		case ItemOneofCase.Message:
			if (Message == null)
			{
				Message = new CommunityLogPayloadMessage();
			}
			Message.MergeFrom(other.Message);
			break;
		case ItemOneofCase.Directory:
			if (Directory == null)
			{
				Directory = new CommunityLogPayloadDirectory();
			}
			Directory.MergeFrom(other.Directory);
			break;
		case ItemOneofCase.File:
			if (File == null)
			{
				File = new CommunityLogPayloadFile();
			}
			File.MergeFrom(other.File);
			break;
		case ItemOneofCase.Member:
			if (Member == null)
			{
				Member = new CommunityLogPayloadMember();
			}
			Member.MergeFrom(other.Member);
			break;
		case ItemOneofCase.MemberKicked:
			if (MemberKicked == null)
			{
				MemberKicked = new CommunityLogPayloadMemberKicked();
			}
			MemberKicked.MergeFrom(other.MemberKicked);
			break;
		case ItemOneofCase.MemberBanned:
			if (MemberBanned == null)
			{
				MemberBanned = new CommunityLogPayloadMemberBanned();
			}
			MemberBanned.MergeFrom(other.MemberBanned);
			break;
		case ItemOneofCase.MemberUnBanned:
			if (MemberUnBanned == null)
			{
				MemberUnBanned = new CommunityLogPayloadMemberUnBanned();
			}
			MemberUnBanned.MergeFrom(other.MemberUnBanned);
			break;
		case ItemOneofCase.MemberInvited:
			if (MemberInvited == null)
			{
				MemberInvited = new CommunityLogPayloadMemberInvited();
			}
			MemberInvited.MergeFrom(other.MemberInvited);
			break;
		case ItemOneofCase.App:
			if (App == null)
			{
				App = new CommunityLogPayloadApp();
			}
			App.MergeFrom(other.App);
			break;
		case ItemOneofCase.MemberJoined:
			if (MemberJoined == null)
			{
				MemberJoined = new CommunityLogPayloadMemberJoined();
			}
			MemberJoined.MergeFrom(other.MemberJoined);
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
				CommunityLogPayloadCommunity communityLogPayloadCommunity = new CommunityLogPayloadCommunity();
				if (itemCase_ == ItemOneofCase.Community)
				{
					communityLogPayloadCommunity.MergeFrom(Community);
				}
				P_0.ReadMessage(communityLogPayloadCommunity);
				Community = communityLogPayloadCommunity;
				break;
			}
			case 42u:
			{
				CommunityLogPayloadChannel communityLogPayloadChannel = new CommunityLogPayloadChannel();
				if (itemCase_ == ItemOneofCase.Channel)
				{
					communityLogPayloadChannel.MergeFrom(Channel);
				}
				P_0.ReadMessage(communityLogPayloadChannel);
				Channel = communityLogPayloadChannel;
				break;
			}
			case 50u:
			{
				CommunityLogPayloadChannelGroup communityLogPayloadChannelGroup = new CommunityLogPayloadChannelGroup();
				if (itemCase_ == ItemOneofCase.ChannelGroup)
				{
					communityLogPayloadChannelGroup.MergeFrom(ChannelGroup);
				}
				P_0.ReadMessage(communityLogPayloadChannelGroup);
				ChannelGroup = communityLogPayloadChannelGroup;
				break;
			}
			case 58u:
			{
				CommunityLogPayloadRole communityLogPayloadRole = new CommunityLogPayloadRole();
				if (itemCase_ == ItemOneofCase.Role)
				{
					communityLogPayloadRole.MergeFrom(Role);
				}
				P_0.ReadMessage(communityLogPayloadRole);
				Role = communityLogPayloadRole;
				break;
			}
			case 66u:
			{
				CommunityLogPayloadMemberRole communityLogPayloadMemberRole = new CommunityLogPayloadMemberRole();
				if (itemCase_ == ItemOneofCase.MemberRole)
				{
					communityLogPayloadMemberRole.MergeFrom(MemberRole);
				}
				P_0.ReadMessage(communityLogPayloadMemberRole);
				MemberRole = communityLogPayloadMemberRole;
				break;
			}
			case 74u:
			{
				CommunityLogPayloadAccessRule communityLogPayloadAccessRule = new CommunityLogPayloadAccessRule();
				if (itemCase_ == ItemOneofCase.AccessRule)
				{
					communityLogPayloadAccessRule.MergeFrom(AccessRule);
				}
				P_0.ReadMessage(communityLogPayloadAccessRule);
				AccessRule = communityLogPayloadAccessRule;
				break;
			}
			case 82u:
			{
				CommunityLogPayloadMessage communityLogPayloadMessage = new CommunityLogPayloadMessage();
				if (itemCase_ == ItemOneofCase.Message)
				{
					communityLogPayloadMessage.MergeFrom(Message);
				}
				P_0.ReadMessage(communityLogPayloadMessage);
				Message = communityLogPayloadMessage;
				break;
			}
			case 90u:
			{
				CommunityLogPayloadDirectory communityLogPayloadDirectory = new CommunityLogPayloadDirectory();
				if (itemCase_ == ItemOneofCase.Directory)
				{
					communityLogPayloadDirectory.MergeFrom(Directory);
				}
				P_0.ReadMessage(communityLogPayloadDirectory);
				Directory = communityLogPayloadDirectory;
				break;
			}
			case 98u:
			{
				CommunityLogPayloadFile communityLogPayloadFile = new CommunityLogPayloadFile();
				if (itemCase_ == ItemOneofCase.File)
				{
					communityLogPayloadFile.MergeFrom(File);
				}
				P_0.ReadMessage(communityLogPayloadFile);
				File = communityLogPayloadFile;
				break;
			}
			case 106u:
			{
				CommunityLogPayloadMember communityLogPayloadMember = new CommunityLogPayloadMember();
				if (itemCase_ == ItemOneofCase.Member)
				{
					communityLogPayloadMember.MergeFrom(Member);
				}
				P_0.ReadMessage(communityLogPayloadMember);
				Member = communityLogPayloadMember;
				break;
			}
			case 114u:
			{
				CommunityLogPayloadMemberKicked communityLogPayloadMemberKicked = new CommunityLogPayloadMemberKicked();
				if (itemCase_ == ItemOneofCase.MemberKicked)
				{
					communityLogPayloadMemberKicked.MergeFrom(MemberKicked);
				}
				P_0.ReadMessage(communityLogPayloadMemberKicked);
				MemberKicked = communityLogPayloadMemberKicked;
				break;
			}
			case 122u:
			{
				CommunityLogPayloadMemberBanned communityLogPayloadMemberBanned = new CommunityLogPayloadMemberBanned();
				if (itemCase_ == ItemOneofCase.MemberBanned)
				{
					communityLogPayloadMemberBanned.MergeFrom(MemberBanned);
				}
				P_0.ReadMessage(communityLogPayloadMemberBanned);
				MemberBanned = communityLogPayloadMemberBanned;
				break;
			}
			case 130u:
			{
				CommunityLogPayloadMemberUnBanned communityLogPayloadMemberUnBanned = new CommunityLogPayloadMemberUnBanned();
				if (itemCase_ == ItemOneofCase.MemberUnBanned)
				{
					communityLogPayloadMemberUnBanned.MergeFrom(MemberUnBanned);
				}
				P_0.ReadMessage(communityLogPayloadMemberUnBanned);
				MemberUnBanned = communityLogPayloadMemberUnBanned;
				break;
			}
			case 138u:
			{
				CommunityLogPayloadMemberInvited communityLogPayloadMemberInvited = new CommunityLogPayloadMemberInvited();
				if (itemCase_ == ItemOneofCase.MemberInvited)
				{
					communityLogPayloadMemberInvited.MergeFrom(MemberInvited);
				}
				P_0.ReadMessage(communityLogPayloadMemberInvited);
				MemberInvited = communityLogPayloadMemberInvited;
				break;
			}
			case 146u:
			{
				CommunityLogPayloadApp communityLogPayloadApp = new CommunityLogPayloadApp();
				if (itemCase_ == ItemOneofCase.App)
				{
					communityLogPayloadApp.MergeFrom(App);
				}
				P_0.ReadMessage(communityLogPayloadApp);
				App = communityLogPayloadApp;
				break;
			}
			case 154u:
			{
				CommunityLogPayloadMemberJoined communityLogPayloadMemberJoined = new CommunityLogPayloadMemberJoined();
				if (itemCase_ == ItemOneofCase.MemberJoined)
				{
					communityLogPayloadMemberJoined.MergeFrom(MemberJoined);
				}
				P_0.ReadMessage(communityLogPayloadMemberJoined);
				MemberJoined = communityLogPayloadMemberJoined;
				break;
			}
			}
		}
	}
}
