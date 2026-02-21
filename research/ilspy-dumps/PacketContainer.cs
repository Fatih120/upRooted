using System;
using System.CodeDom.Compiler;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Packets;

public sealed class PacketContainer : IMessage<PacketContainer>, IMessage, IEquatable<PacketContainer>, IDeepCloneable<PacketContainer>, IBufferMessage
{
	public enum PacketOneofCase
	{
		None = 0,
		Ping = 1,
		HubServerMove = 2,
		ChannelCreated = 30,
		ChannelEdited = 31,
		ChannelMoved = 32,
		ChannelDeleted = 33,
		ChannelGroupCreated = 40,
		ChannelGroupEdited = 41,
		ChannelGroupMoved = 42,
		ChannelGroupDeleted = 43,
		Community = 50,
		CommunityJoined = 51,
		CommunityLeave = 52,
		CommunityDeleted = 53,
		CommunityMemberAttach = 60,
		CommunityMemberDetach = 61,
		CommunityMemberEdited = 62,
		CommunityMemberEditedExternal = 63,
		CommunityMemberBanCreated = 70,
		CommunityMemberBanDeleted = 71,
		CommunityMemberRoleCreated = 80,
		CommunityMemberRoleSetPrimary = 81,
		CommunityMemberRoleDeleted = 82,
		CommunityRole = 90,
		CommunityRoleDeleted = 91,
		CommunityRoleMoved = 92,
		CommunityPermissionUpdate = 100,
		CommunityAppAdded = 110,
		CommunityAppRemoved = 111,
		CommunityAppSetStatus = 112,
		CommunityAppSetSettings = 113,
		CommunityAppVersionUpdateNotification = 114,
		CommunityAppSetButton = 115,
		DirectMessageCreated = 120,
		DirectMessageMemberAdded = 121,
		DirectMessageMemberDeleted = 122,
		DirectMessageRing = 123,
		DirectMessageRingDeclined = 124,
		DirectMessageLastMessageDeleted = 125,
		Directory = 130,
		DirectoryMoved = 131,
		DirectoryDeleted = 132,
		FileCreated = 140,
		FileEdited = 141,
		FileMoved = 142,
		FileDeleted = 143,
		FriendshipCreated = 150,
		FriendshipMoved = 151,
		FriendshipDeleted = 152,
		FriendshipGroupCreated = 160,
		FriendshipGroupEdited = 161,
		FriendshipGroupMoved = 162,
		FriendshipGroupDeleted = 163,
		Message = 170,
		MessageDeleted = 171,
		MessagePin = 172,
		MessageReaction = 173,
		MessageSetTypingIndicator = 174,
		MessageSetViewTime = 175,
		Notification = 180,
		NotificationViewed = 181,
		NotificationViewedAll = 182,
		NotificationDeleted = 183,
		NotificationDeletedAll = 184,
		UserSetProfile = 190,
		UserSetStatus = 191,
		UserSetEmailVerification = 192,
		UserSetMaxStatus = 193,
		UserDeleted = 194,
		UserSetBadges = 195,
		UserSetDirectMessageInviteRequirement = 196,
		UserSetFriendshipInviteRequirement = 197,
		WebRtcUserDevice = 200,
		WebRtcUserDetach = 201,
		WebRtcUserDeviceSetStatus = 202,
		WebRtcUserDeviceSetTransport = 203,
		WebRtcUserDeviceSetDataChannel = 204,
		UserBlockCreated = 210,
		UserBlockDeleted = 211,
		AssetChanged = 220
	}

	private static readonly MessageParser<PacketContainer> _parser = new MessageParser<PacketContainer>(() => new PacketContainer());

	private UnknownFieldSet _unknownFields;

	private object packet_;

	private PacketOneofCase packetCase_ = PacketOneofCase.None;

	[GeneratedCode("protoc", null)]
	public static MessageParser<PacketContainer> Parser => _parser;

	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => PacketReflection.Descriptor.MessageTypes[0];

	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[GeneratedCode("protoc", null)]
	public PingPacket Ping
	{
		get
		{
			return (packetCase_ == PacketOneofCase.Ping) ? ((PingPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.Ping : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public HubServerMovePacket HubServerMove
	{
		get
		{
			return (packetCase_ == PacketOneofCase.HubServerMove) ? ((HubServerMovePacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.HubServerMove : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelCreatedPacket ChannelCreated
	{
		get
		{
			return (packetCase_ == PacketOneofCase.ChannelCreated) ? ((ChannelCreatedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.ChannelCreated : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelEditedPacket ChannelEdited
	{
		get
		{
			return (packetCase_ == PacketOneofCase.ChannelEdited) ? ((ChannelEditedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.ChannelEdited : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelMovedPacket ChannelMoved
	{
		get
		{
			return (packetCase_ == PacketOneofCase.ChannelMoved) ? ((ChannelMovedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.ChannelMoved : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelDeletedPacket ChannelDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.ChannelDeleted) ? ((ChannelDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.ChannelDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupCreatedPacket ChannelGroupCreated
	{
		get
		{
			return (packetCase_ == PacketOneofCase.ChannelGroupCreated) ? ((ChannelGroupCreatedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.ChannelGroupCreated : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupEditedPacket ChannelGroupEdited
	{
		get
		{
			return (packetCase_ == PacketOneofCase.ChannelGroupEdited) ? ((ChannelGroupEditedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.ChannelGroupEdited : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupMovedPacket ChannelGroupMoved
	{
		get
		{
			return (packetCase_ == PacketOneofCase.ChannelGroupMoved) ? ((ChannelGroupMovedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.ChannelGroupMoved : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public ChannelGroupDeletedPacket ChannelGroupDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.ChannelGroupDeleted) ? ((ChannelGroupDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.ChannelGroupDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityPacket Community
	{
		get
		{
			return (packetCase_ == PacketOneofCase.Community) ? ((CommunityPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.Community : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityJoinedPacket CommunityJoined
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityJoined) ? ((CommunityJoinedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityJoined : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityLeavePacket CommunityLeave
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityLeave) ? ((CommunityLeavePacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityLeave : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityDeletedPacket CommunityDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityDeleted) ? ((CommunityDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberAttachPacket CommunityMemberAttach
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityMemberAttach) ? ((CommunityMemberAttachPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityMemberAttach : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberDetachPacket CommunityMemberDetach
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityMemberDetach) ? ((CommunityMemberDetachPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityMemberDetach : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberEditedPacket CommunityMemberEdited
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityMemberEdited) ? ((CommunityMemberEditedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityMemberEdited : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberEditedExternalPacket CommunityMemberEditedExternal
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityMemberEditedExternal) ? ((CommunityMemberEditedExternalPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityMemberEditedExternal : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanCreatedPacket CommunityMemberBanCreated
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityMemberBanCreated) ? ((CommunityMemberBanCreatedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityMemberBanCreated : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberBanDeletedPacket CommunityMemberBanDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityMemberBanDeleted) ? ((CommunityMemberBanDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityMemberBanDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberRoleCreatedPacket CommunityMemberRoleCreated
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityMemberRoleCreated) ? ((CommunityMemberRoleCreatedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityMemberRoleCreated : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberRoleSetPrimaryPacket CommunityMemberRoleSetPrimary
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityMemberRoleSetPrimary) ? ((CommunityMemberRoleSetPrimaryPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityMemberRoleSetPrimary : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityMemberRoleDeletedPacket CommunityMemberRoleDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityMemberRoleDeleted) ? ((CommunityMemberRoleDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityMemberRoleDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityRolePacket CommunityRole
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityRole) ? ((CommunityRolePacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityRole : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleDeletedPacket CommunityRoleDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityRoleDeleted) ? ((CommunityRoleDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityRoleDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityRoleMovedPacket CommunityRoleMoved
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityRoleMoved) ? ((CommunityRoleMovedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityRoleMoved : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityPermissionUpdatePacket CommunityPermissionUpdate
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityPermissionUpdate) ? ((CommunityPermissionUpdatePacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityPermissionUpdate : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppAddedPacket CommunityAppAdded
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityAppAdded) ? ((CommunityAppAddedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityAppAdded : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppRemovedPacket CommunityAppRemoved
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityAppRemoved) ? ((CommunityAppRemovedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityAppRemoved : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppSetStatusPacket CommunityAppSetStatus
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityAppSetStatus) ? ((CommunityAppSetStatusPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityAppSetStatus : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppSetSettingsPacket CommunityAppSetSettings
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityAppSetSettings) ? ((CommunityAppSetSettingsPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityAppSetSettings : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppVersionUpdateNotificationPacket CommunityAppVersionUpdateNotification
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityAppVersionUpdateNotification) ? ((CommunityAppVersionUpdateNotificationPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityAppVersionUpdateNotification : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public CommunityAppSetButtonPacket CommunityAppSetButton
	{
		get
		{
			return (packetCase_ == PacketOneofCase.CommunityAppSetButton) ? ((CommunityAppSetButtonPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.CommunityAppSetButton : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageCreatedPacket DirectMessageCreated
	{
		get
		{
			return (packetCase_ == PacketOneofCase.DirectMessageCreated) ? ((DirectMessageCreatedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.DirectMessageCreated : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageMemberAddedPacket DirectMessageMemberAdded
	{
		get
		{
			return (packetCase_ == PacketOneofCase.DirectMessageMemberAdded) ? ((DirectMessageMemberAddedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.DirectMessageMemberAdded : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageMemberDeletedPacket DirectMessageMemberDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.DirectMessageMemberDeleted) ? ((DirectMessageMemberDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.DirectMessageMemberDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageRingPacket DirectMessageRing
	{
		get
		{
			return (packetCase_ == PacketOneofCase.DirectMessageRing) ? ((DirectMessageRingPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.DirectMessageRing : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageRingDeclinedPacket DirectMessageRingDeclined
	{
		get
		{
			return (packetCase_ == PacketOneofCase.DirectMessageRingDeclined) ? ((DirectMessageRingDeclinedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.DirectMessageRingDeclined : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectMessageLastMessageDeletedPacket DirectMessageLastMessageDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.DirectMessageLastMessageDeleted) ? ((DirectMessageLastMessageDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.DirectMessageLastMessageDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectoryPacket Directory
	{
		get
		{
			return (packetCase_ == PacketOneofCase.Directory) ? ((DirectoryPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.Directory : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectoryMovedPacket DirectoryMoved
	{
		get
		{
			return (packetCase_ == PacketOneofCase.DirectoryMoved) ? ((DirectoryMovedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.DirectoryMoved : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public DirectoryDeletedPacket DirectoryDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.DirectoryDeleted) ? ((DirectoryDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.DirectoryDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public FileCreatedPacket FileCreated
	{
		get
		{
			return (packetCase_ == PacketOneofCase.FileCreated) ? ((FileCreatedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.FileCreated : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public FileEditedPacket FileEdited
	{
		get
		{
			return (packetCase_ == PacketOneofCase.FileEdited) ? ((FileEditedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.FileEdited : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public FileMovedPacket FileMoved
	{
		get
		{
			return (packetCase_ == PacketOneofCase.FileMoved) ? ((FileMovedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.FileMoved : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public FileDeletedPacket FileDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.FileDeleted) ? ((FileDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.FileDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipCreatedPacket FriendshipCreated
	{
		get
		{
			return (packetCase_ == PacketOneofCase.FriendshipCreated) ? ((FriendshipCreatedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.FriendshipCreated : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipMovedPacket FriendshipMoved
	{
		get
		{
			return (packetCase_ == PacketOneofCase.FriendshipMoved) ? ((FriendshipMovedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.FriendshipMoved : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipDeletedPacket FriendshipDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.FriendshipDeleted) ? ((FriendshipDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.FriendshipDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupCreatedPacket FriendshipGroupCreated
	{
		get
		{
			return (packetCase_ == PacketOneofCase.FriendshipGroupCreated) ? ((FriendshipGroupCreatedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.FriendshipGroupCreated : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupEditedPacket FriendshipGroupEdited
	{
		get
		{
			return (packetCase_ == PacketOneofCase.FriendshipGroupEdited) ? ((FriendshipGroupEditedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.FriendshipGroupEdited : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupMovedPacket FriendshipGroupMoved
	{
		get
		{
			return (packetCase_ == PacketOneofCase.FriendshipGroupMoved) ? ((FriendshipGroupMovedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.FriendshipGroupMoved : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public FriendshipGroupDeletedPacket FriendshipGroupDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.FriendshipGroupDeleted) ? ((FriendshipGroupDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.FriendshipGroupDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePacket Message
	{
		get
		{
			return (packetCase_ == PacketOneofCase.Message) ? ((MessagePacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.Message : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageDeletedPacket MessageDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.MessageDeleted) ? ((MessageDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.MessageDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessagePinPacket MessagePin
	{
		get
		{
			return (packetCase_ == PacketOneofCase.MessagePin) ? ((MessagePinPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.MessagePin : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageReactionPacket MessageReaction
	{
		get
		{
			return (packetCase_ == PacketOneofCase.MessageReaction) ? ((MessageReactionPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.MessageReaction : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageSetTypingIndicatorPacket MessageSetTypingIndicator
	{
		get
		{
			return (packetCase_ == PacketOneofCase.MessageSetTypingIndicator) ? ((MessageSetTypingIndicatorPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.MessageSetTypingIndicator : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public MessageSetViewTimePacket MessageSetViewTime
	{
		get
		{
			return (packetCase_ == PacketOneofCase.MessageSetViewTime) ? ((MessageSetViewTimePacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.MessageSetViewTime : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationPacket Notification
	{
		get
		{
			return (packetCase_ == PacketOneofCase.Notification) ? ((NotificationPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.Notification : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationViewedPacket NotificationViewed
	{
		get
		{
			return (packetCase_ == PacketOneofCase.NotificationViewed) ? ((NotificationViewedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.NotificationViewed : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationViewedAllPacket NotificationViewedAll
	{
		get
		{
			return (packetCase_ == PacketOneofCase.NotificationViewedAll) ? ((NotificationViewedAllPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.NotificationViewedAll : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationDeletedPacket NotificationDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.NotificationDeleted) ? ((NotificationDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.NotificationDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public NotificationDeletedAllPacket NotificationDeletedAll
	{
		get
		{
			return (packetCase_ == PacketOneofCase.NotificationDeletedAll) ? ((NotificationDeletedAllPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.NotificationDeletedAll : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSetProfilePacket UserSetProfile
	{
		get
		{
			return (packetCase_ == PacketOneofCase.UserSetProfile) ? ((UserSetProfilePacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.UserSetProfile : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSetStatusPacket UserSetStatus
	{
		get
		{
			return (packetCase_ == PacketOneofCase.UserSetStatus) ? ((UserSetStatusPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.UserSetStatus : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSetEmailVerificationPacket UserSetEmailVerification
	{
		get
		{
			return (packetCase_ == PacketOneofCase.UserSetEmailVerification) ? ((UserSetEmailVerificationPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.UserSetEmailVerification : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSetMaxStatusPacket UserSetMaxStatus
	{
		get
		{
			return (packetCase_ == PacketOneofCase.UserSetMaxStatus) ? ((UserSetMaxStatusPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.UserSetMaxStatus : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public UserDeletedPacket UserDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.UserDeleted) ? ((UserDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.UserDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSetBadgesPacket UserSetBadges
	{
		get
		{
			return (packetCase_ == PacketOneofCase.UserSetBadges) ? ((UserSetBadgesPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.UserSetBadges : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSetDirectMessageInviteRequirementPacket UserSetDirectMessageInviteRequirement
	{
		get
		{
			return (packetCase_ == PacketOneofCase.UserSetDirectMessageInviteRequirement) ? ((UserSetDirectMessageInviteRequirementPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.UserSetDirectMessageInviteRequirement : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public UserSetFriendshipInviteRequirementPacket UserSetFriendshipInviteRequirement
	{
		get
		{
			return (packetCase_ == PacketOneofCase.UserSetFriendshipInviteRequirement) ? ((UserSetFriendshipInviteRequirementPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.UserSetFriendshipInviteRequirement : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserDevicePacket WebRtcUserDevice
	{
		get
		{
			return (packetCase_ == PacketOneofCase.WebRtcUserDevice) ? ((WebRtcUserDevicePacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.WebRtcUserDevice : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserDetachPacket WebRtcUserDetach
	{
		get
		{
			return (packetCase_ == PacketOneofCase.WebRtcUserDetach) ? ((WebRtcUserDetachPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.WebRtcUserDetach : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserDeviceSetStatusPacket WebRtcUserDeviceSetStatus
	{
		get
		{
			return (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetStatus) ? ((WebRtcUserDeviceSetStatusPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.WebRtcUserDeviceSetStatus : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserDeviceSetTransportPacket WebRtcUserDeviceSetTransport
	{
		get
		{
			return (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetTransport) ? ((WebRtcUserDeviceSetTransportPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.WebRtcUserDeviceSetTransport : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public WebRtcUserDeviceSetDataChannelPacket WebRtcUserDeviceSetDataChannel
	{
		get
		{
			return (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetDataChannel) ? ((WebRtcUserDeviceSetDataChannelPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.WebRtcUserDeviceSetDataChannel : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public UserBlockCreatedPacket UserBlockCreated
	{
		get
		{
			return (packetCase_ == PacketOneofCase.UserBlockCreated) ? ((UserBlockCreatedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.UserBlockCreated : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public UserBlockDeletedPacket UserBlockDeleted
	{
		get
		{
			return (packetCase_ == PacketOneofCase.UserBlockDeleted) ? ((UserBlockDeletedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.UserBlockDeleted : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public AssetChangedPacket AssetChanged
	{
		get
		{
			return (packetCase_ == PacketOneofCase.AssetChanged) ? ((AssetChangedPacket)packet_) : null;
		}
		set
		{
			packet_ = value;
			packetCase_ = ((value != null) ? PacketOneofCase.AssetChanged : PacketOneofCase.None);
		}
	}

	[GeneratedCode("protoc", null)]
	public PacketOneofCase PacketCase => packetCase_;

	[GeneratedCode("protoc", null)]
	public PacketContainer()
	{
	}

	[GeneratedCode("protoc", null)]
	public PacketContainer(PacketContainer other)
		: this()
	{
		switch (other.PacketCase)
		{
		case PacketOneofCase.Ping:
			Ping = other.Ping.Clone();
			break;
		case PacketOneofCase.HubServerMove:
			HubServerMove = other.HubServerMove.Clone();
			break;
		case PacketOneofCase.ChannelCreated:
			ChannelCreated = other.ChannelCreated.Clone();
			break;
		case PacketOneofCase.ChannelEdited:
			ChannelEdited = other.ChannelEdited.Clone();
			break;
		case PacketOneofCase.ChannelMoved:
			ChannelMoved = other.ChannelMoved.Clone();
			break;
		case PacketOneofCase.ChannelDeleted:
			ChannelDeleted = other.ChannelDeleted.Clone();
			break;
		case PacketOneofCase.ChannelGroupCreated:
			ChannelGroupCreated = other.ChannelGroupCreated.Clone();
			break;
		case PacketOneofCase.ChannelGroupEdited:
			ChannelGroupEdited = other.ChannelGroupEdited.Clone();
			break;
		case PacketOneofCase.ChannelGroupMoved:
			ChannelGroupMoved = other.ChannelGroupMoved.Clone();
			break;
		case PacketOneofCase.ChannelGroupDeleted:
			ChannelGroupDeleted = other.ChannelGroupDeleted.Clone();
			break;
		case PacketOneofCase.Community:
			Community = other.Community.Clone();
			break;
		case PacketOneofCase.CommunityJoined:
			CommunityJoined = other.CommunityJoined.Clone();
			break;
		case PacketOneofCase.CommunityLeave:
			CommunityLeave = other.CommunityLeave.Clone();
			break;
		case PacketOneofCase.CommunityDeleted:
			CommunityDeleted = other.CommunityDeleted.Clone();
			break;
		case PacketOneofCase.CommunityMemberAttach:
			CommunityMemberAttach = other.CommunityMemberAttach.Clone();
			break;
		case PacketOneofCase.CommunityMemberDetach:
			CommunityMemberDetach = other.CommunityMemberDetach.Clone();
			break;
		case PacketOneofCase.CommunityMemberEdited:
			CommunityMemberEdited = other.CommunityMemberEdited.Clone();
			break;
		case PacketOneofCase.CommunityMemberEditedExternal:
			CommunityMemberEditedExternal = other.CommunityMemberEditedExternal.Clone();
			break;
		case PacketOneofCase.CommunityMemberBanCreated:
			CommunityMemberBanCreated = other.CommunityMemberBanCreated.Clone();
			break;
		case PacketOneofCase.CommunityMemberBanDeleted:
			CommunityMemberBanDeleted = other.CommunityMemberBanDeleted.Clone();
			break;
		case PacketOneofCase.CommunityMemberRoleCreated:
			CommunityMemberRoleCreated = other.CommunityMemberRoleCreated.Clone();
			break;
		case PacketOneofCase.CommunityMemberRoleSetPrimary:
			CommunityMemberRoleSetPrimary = other.CommunityMemberRoleSetPrimary.Clone();
			break;
		case PacketOneofCase.CommunityMemberRoleDeleted:
			CommunityMemberRoleDeleted = other.CommunityMemberRoleDeleted.Clone();
			break;
		case PacketOneofCase.CommunityRole:
			CommunityRole = other.CommunityRole.Clone();
			break;
		case PacketOneofCase.CommunityRoleDeleted:
			CommunityRoleDeleted = other.CommunityRoleDeleted.Clone();
			break;
		case PacketOneofCase.CommunityRoleMoved:
			CommunityRoleMoved = other.CommunityRoleMoved.Clone();
			break;
		case PacketOneofCase.CommunityPermissionUpdate:
			CommunityPermissionUpdate = other.CommunityPermissionUpdate.Clone();
			break;
		case PacketOneofCase.CommunityAppAdded:
			CommunityAppAdded = other.CommunityAppAdded.Clone();
			break;
		case PacketOneofCase.CommunityAppRemoved:
			CommunityAppRemoved = other.CommunityAppRemoved.Clone();
			break;
		case PacketOneofCase.CommunityAppSetStatus:
			CommunityAppSetStatus = other.CommunityAppSetStatus.Clone();
			break;
		case PacketOneofCase.CommunityAppSetSettings:
			CommunityAppSetSettings = other.CommunityAppSetSettings.Clone();
			break;
		case PacketOneofCase.CommunityAppVersionUpdateNotification:
			CommunityAppVersionUpdateNotification = other.CommunityAppVersionUpdateNotification.Clone();
			break;
		case PacketOneofCase.CommunityAppSetButton:
			CommunityAppSetButton = other.CommunityAppSetButton.Clone();
			break;
		case PacketOneofCase.DirectMessageCreated:
			DirectMessageCreated = other.DirectMessageCreated.Clone();
			break;
		case PacketOneofCase.DirectMessageMemberAdded:
			DirectMessageMemberAdded = other.DirectMessageMemberAdded.Clone();
			break;
		case PacketOneofCase.DirectMessageMemberDeleted:
			DirectMessageMemberDeleted = other.DirectMessageMemberDeleted.Clone();
			break;
		case PacketOneofCase.DirectMessageRing:
			DirectMessageRing = other.DirectMessageRing.Clone();
			break;
		case PacketOneofCase.DirectMessageRingDeclined:
			DirectMessageRingDeclined = other.DirectMessageRingDeclined.Clone();
			break;
		case PacketOneofCase.DirectMessageLastMessageDeleted:
			DirectMessageLastMessageDeleted = other.DirectMessageLastMessageDeleted.Clone();
			break;
		case PacketOneofCase.Directory:
			Directory = other.Directory.Clone();
			break;
		case PacketOneofCase.DirectoryMoved:
			DirectoryMoved = other.DirectoryMoved.Clone();
			break;
		case PacketOneofCase.DirectoryDeleted:
			DirectoryDeleted = other.DirectoryDeleted.Clone();
			break;
		case PacketOneofCase.FileCreated:
			FileCreated = other.FileCreated.Clone();
			break;
		case PacketOneofCase.FileEdited:
			FileEdited = other.FileEdited.Clone();
			break;
		case PacketOneofCase.FileMoved:
			FileMoved = other.FileMoved.Clone();
			break;
		case PacketOneofCase.FileDeleted:
			FileDeleted = other.FileDeleted.Clone();
			break;
		case PacketOneofCase.FriendshipCreated:
			FriendshipCreated = other.FriendshipCreated.Clone();
			break;
		case PacketOneofCase.FriendshipMoved:
			FriendshipMoved = other.FriendshipMoved.Clone();
			break;
		case PacketOneofCase.FriendshipDeleted:
			FriendshipDeleted = other.FriendshipDeleted.Clone();
			break;
		case PacketOneofCase.FriendshipGroupCreated:
			FriendshipGroupCreated = other.FriendshipGroupCreated.Clone();
			break;
		case PacketOneofCase.FriendshipGroupEdited:
			FriendshipGroupEdited = other.FriendshipGroupEdited.Clone();
			break;
		case PacketOneofCase.FriendshipGroupMoved:
			FriendshipGroupMoved = other.FriendshipGroupMoved.Clone();
			break;
		case PacketOneofCase.FriendshipGroupDeleted:
			FriendshipGroupDeleted = other.FriendshipGroupDeleted.Clone();
			break;
		case PacketOneofCase.Message:
			Message = other.Message.Clone();
			break;
		case PacketOneofCase.MessageDeleted:
			MessageDeleted = other.MessageDeleted.Clone();
			break;
		case PacketOneofCase.MessagePin:
			MessagePin = other.MessagePin.Clone();
			break;
		case PacketOneofCase.MessageReaction:
			MessageReaction = other.MessageReaction.Clone();
			break;
		case PacketOneofCase.MessageSetTypingIndicator:
			MessageSetTypingIndicator = other.MessageSetTypingIndicator.Clone();
			break;
		case PacketOneofCase.MessageSetViewTime:
			MessageSetViewTime = other.MessageSetViewTime.Clone();
			break;
		case PacketOneofCase.Notification:
			Notification = other.Notification.Clone();
			break;
		case PacketOneofCase.NotificationViewed:
			NotificationViewed = other.NotificationViewed.Clone();
			break;
		case PacketOneofCase.NotificationViewedAll:
			NotificationViewedAll = other.NotificationViewedAll.Clone();
			break;
		case PacketOneofCase.NotificationDeleted:
			NotificationDeleted = other.NotificationDeleted.Clone();
			break;
		case PacketOneofCase.NotificationDeletedAll:
			NotificationDeletedAll = other.NotificationDeletedAll.Clone();
			break;
		case PacketOneofCase.UserSetProfile:
			UserSetProfile = other.UserSetProfile.Clone();
			break;
		case PacketOneofCase.UserSetStatus:
			UserSetStatus = other.UserSetStatus.Clone();
			break;
		case PacketOneofCase.UserSetEmailVerification:
			UserSetEmailVerification = other.UserSetEmailVerification.Clone();
			break;
		case PacketOneofCase.UserSetMaxStatus:
			UserSetMaxStatus = other.UserSetMaxStatus.Clone();
			break;
		case PacketOneofCase.UserDeleted:
			UserDeleted = other.UserDeleted.Clone();
			break;
		case PacketOneofCase.UserSetBadges:
			UserSetBadges = other.UserSetBadges.Clone();
			break;
		case PacketOneofCase.UserSetDirectMessageInviteRequirement:
			UserSetDirectMessageInviteRequirement = other.UserSetDirectMessageInviteRequirement.Clone();
			break;
		case PacketOneofCase.UserSetFriendshipInviteRequirement:
			UserSetFriendshipInviteRequirement = other.UserSetFriendshipInviteRequirement.Clone();
			break;
		case PacketOneofCase.WebRtcUserDevice:
			WebRtcUserDevice = other.WebRtcUserDevice.Clone();
			break;
		case PacketOneofCase.WebRtcUserDetach:
			WebRtcUserDetach = other.WebRtcUserDetach.Clone();
			break;
		case PacketOneofCase.WebRtcUserDeviceSetStatus:
			WebRtcUserDeviceSetStatus = other.WebRtcUserDeviceSetStatus.Clone();
			break;
		case PacketOneofCase.WebRtcUserDeviceSetTransport:
			WebRtcUserDeviceSetTransport = other.WebRtcUserDeviceSetTransport.Clone();
			break;
		case PacketOneofCase.WebRtcUserDeviceSetDataChannel:
			WebRtcUserDeviceSetDataChannel = other.WebRtcUserDeviceSetDataChannel.Clone();
			break;
		case PacketOneofCase.UserBlockCreated:
			UserBlockCreated = other.UserBlockCreated.Clone();
			break;
		case PacketOneofCase.UserBlockDeleted:
			UserBlockDeleted = other.UserBlockDeleted.Clone();
			break;
		case PacketOneofCase.AssetChanged:
			AssetChanged = other.AssetChanged.Clone();
			break;
		}
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public PacketContainer Clone()
	{
		return new PacketContainer(this);
	}

	[GeneratedCode("protoc", null)]
	public void ClearPacket()
	{
		packetCase_ = PacketOneofCase.None;
		packet_ = null;
	}

	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as PacketContainer);
	}

	[GeneratedCode("protoc", null)]
	public bool Equals(PacketContainer other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(Ping, other.Ping))
		{
			return false;
		}
		if (!object.Equals(HubServerMove, other.HubServerMove))
		{
			return false;
		}
		if (!object.Equals(ChannelCreated, other.ChannelCreated))
		{
			return false;
		}
		if (!object.Equals(ChannelEdited, other.ChannelEdited))
		{
			return false;
		}
		if (!object.Equals(ChannelMoved, other.ChannelMoved))
		{
			return false;
		}
		if (!object.Equals(ChannelDeleted, other.ChannelDeleted))
		{
			return false;
		}
		if (!object.Equals(ChannelGroupCreated, other.ChannelGroupCreated))
		{
			return false;
		}
		if (!object.Equals(ChannelGroupEdited, other.ChannelGroupEdited))
		{
			return false;
		}
		if (!object.Equals(ChannelGroupMoved, other.ChannelGroupMoved))
		{
			return false;
		}
		if (!object.Equals(ChannelGroupDeleted, other.ChannelGroupDeleted))
		{
			return false;
		}
		if (!object.Equals(Community, other.Community))
		{
			return false;
		}
		if (!object.Equals(CommunityJoined, other.CommunityJoined))
		{
			return false;
		}
		if (!object.Equals(CommunityLeave, other.CommunityLeave))
		{
			return false;
		}
		if (!object.Equals(CommunityDeleted, other.CommunityDeleted))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberAttach, other.CommunityMemberAttach))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberDetach, other.CommunityMemberDetach))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberEdited, other.CommunityMemberEdited))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberEditedExternal, other.CommunityMemberEditedExternal))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberBanCreated, other.CommunityMemberBanCreated))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberBanDeleted, other.CommunityMemberBanDeleted))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberRoleCreated, other.CommunityMemberRoleCreated))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberRoleSetPrimary, other.CommunityMemberRoleSetPrimary))
		{
			return false;
		}
		if (!object.Equals(CommunityMemberRoleDeleted, other.CommunityMemberRoleDeleted))
		{
			return false;
		}
		if (!object.Equals(CommunityRole, other.CommunityRole))
		{
			return false;
		}
		if (!object.Equals(CommunityRoleDeleted, other.CommunityRoleDeleted))
		{
			return false;
		}
		if (!object.Equals(CommunityRoleMoved, other.CommunityRoleMoved))
		{
			return false;
		}
		if (!object.Equals(CommunityPermissionUpdate, other.CommunityPermissionUpdate))
		{
			return false;
		}
		if (!object.Equals(CommunityAppAdded, other.CommunityAppAdded))
		{
			return false;
		}
		if (!object.Equals(CommunityAppRemoved, other.CommunityAppRemoved))
		{
			return false;
		}
		if (!object.Equals(CommunityAppSetStatus, other.CommunityAppSetStatus))
		{
			return false;
		}
		if (!object.Equals(CommunityAppSetSettings, other.CommunityAppSetSettings))
		{
			return false;
		}
		if (!object.Equals(CommunityAppVersionUpdateNotification, other.CommunityAppVersionUpdateNotification))
		{
			return false;
		}
		if (!object.Equals(CommunityAppSetButton, other.CommunityAppSetButton))
		{
			return false;
		}
		if (!object.Equals(DirectMessageCreated, other.DirectMessageCreated))
		{
			return false;
		}
		if (!object.Equals(DirectMessageMemberAdded, other.DirectMessageMemberAdded))
		{
			return false;
		}
		if (!object.Equals(DirectMessageMemberDeleted, other.DirectMessageMemberDeleted))
		{
			return false;
		}
		if (!object.Equals(DirectMessageRing, other.DirectMessageRing))
		{
			return false;
		}
		if (!object.Equals(DirectMessageRingDeclined, other.DirectMessageRingDeclined))
		{
			return false;
		}
		if (!object.Equals(DirectMessageLastMessageDeleted, other.DirectMessageLastMessageDeleted))
		{
			return false;
		}
		if (!object.Equals(Directory, other.Directory))
		{
			return false;
		}
		if (!object.Equals(DirectoryMoved, other.DirectoryMoved))
		{
			return false;
		}
		if (!object.Equals(DirectoryDeleted, other.DirectoryDeleted))
		{
			return false;
		}
		if (!object.Equals(FileCreated, other.FileCreated))
		{
			return false;
		}
		if (!object.Equals(FileEdited, other.FileEdited))
		{
			return false;
		}
		if (!object.Equals(FileMoved, other.FileMoved))
		{
			return false;
		}
		if (!object.Equals(FileDeleted, other.FileDeleted))
		{
			return false;
		}
		if (!object.Equals(FriendshipCreated, other.FriendshipCreated))
		{
			return false;
		}
		if (!object.Equals(FriendshipMoved, other.FriendshipMoved))
		{
			return false;
		}
		if (!object.Equals(FriendshipDeleted, other.FriendshipDeleted))
		{
			return false;
		}
		if (!object.Equals(FriendshipGroupCreated, other.FriendshipGroupCreated))
		{
			return false;
		}
		if (!object.Equals(FriendshipGroupEdited, other.FriendshipGroupEdited))
		{
			return false;
		}
		if (!object.Equals(FriendshipGroupMoved, other.FriendshipGroupMoved))
		{
			return false;
		}
		if (!object.Equals(FriendshipGroupDeleted, other.FriendshipGroupDeleted))
		{
			return false;
		}
		if (!object.Equals(Message, other.Message))
		{
			return false;
		}
		if (!object.Equals(MessageDeleted, other.MessageDeleted))
		{
			return false;
		}
		if (!object.Equals(MessagePin, other.MessagePin))
		{
			return false;
		}
		if (!object.Equals(MessageReaction, other.MessageReaction))
		{
			return false;
		}
		if (!object.Equals(MessageSetTypingIndicator, other.MessageSetTypingIndicator))
		{
			return false;
		}
		if (!object.Equals(MessageSetViewTime, other.MessageSetViewTime))
		{
			return false;
		}
		if (!object.Equals(Notification, other.Notification))
		{
			return false;
		}
		if (!object.Equals(NotificationViewed, other.NotificationViewed))
		{
			return false;
		}
		if (!object.Equals(NotificationViewedAll, other.NotificationViewedAll))
		{
			return false;
		}
		if (!object.Equals(NotificationDeleted, other.NotificationDeleted))
		{
			return false;
		}
		if (!object.Equals(NotificationDeletedAll, other.NotificationDeletedAll))
		{
			return false;
		}
		if (!object.Equals(UserSetProfile, other.UserSetProfile))
		{
			return false;
		}
		if (!object.Equals(UserSetStatus, other.UserSetStatus))
		{
			return false;
		}
		if (!object.Equals(UserSetEmailVerification, other.UserSetEmailVerification))
		{
			return false;
		}
		if (!object.Equals(UserSetMaxStatus, other.UserSetMaxStatus))
		{
			return false;
		}
		if (!object.Equals(UserDeleted, other.UserDeleted))
		{
			return false;
		}
		if (!object.Equals(UserSetBadges, other.UserSetBadges))
		{
			return false;
		}
		if (!object.Equals(UserSetDirectMessageInviteRequirement, other.UserSetDirectMessageInviteRequirement))
		{
			return false;
		}
		if (!object.Equals(UserSetFriendshipInviteRequirement, other.UserSetFriendshipInviteRequirement))
		{
			return false;
		}
		if (!object.Equals(WebRtcUserDevice, other.WebRtcUserDevice))
		{
			return false;
		}
		if (!object.Equals(WebRtcUserDetach, other.WebRtcUserDetach))
		{
			return false;
		}
		if (!object.Equals(WebRtcUserDeviceSetStatus, other.WebRtcUserDeviceSetStatus))
		{
			return false;
		}
		if (!object.Equals(WebRtcUserDeviceSetTransport, other.WebRtcUserDeviceSetTransport))
		{
			return false;
		}
		if (!object.Equals(WebRtcUserDeviceSetDataChannel, other.WebRtcUserDeviceSetDataChannel))
		{
			return false;
		}
		if (!object.Equals(UserBlockCreated, other.UserBlockCreated))
		{
			return false;
		}
		if (!object.Equals(UserBlockDeleted, other.UserBlockDeleted))
		{
			return false;
		}
		if (!object.Equals(AssetChanged, other.AssetChanged))
		{
			return false;
		}
		if (PacketCase != other.PacketCase)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (packetCase_ == PacketOneofCase.Ping)
		{
			num ^= Ping.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.HubServerMove)
		{
			num ^= HubServerMove.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.ChannelCreated)
		{
			num ^= ChannelCreated.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.ChannelEdited)
		{
			num ^= ChannelEdited.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.ChannelMoved)
		{
			num ^= ChannelMoved.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.ChannelDeleted)
		{
			num ^= ChannelDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupCreated)
		{
			num ^= ChannelGroupCreated.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupEdited)
		{
			num ^= ChannelGroupEdited.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupMoved)
		{
			num ^= ChannelGroupMoved.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupDeleted)
		{
			num ^= ChannelGroupDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.Community)
		{
			num ^= Community.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityJoined)
		{
			num ^= CommunityJoined.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityLeave)
		{
			num ^= CommunityLeave.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityDeleted)
		{
			num ^= CommunityDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberAttach)
		{
			num ^= CommunityMemberAttach.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberDetach)
		{
			num ^= CommunityMemberDetach.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberEdited)
		{
			num ^= CommunityMemberEdited.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberEditedExternal)
		{
			num ^= CommunityMemberEditedExternal.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberBanCreated)
		{
			num ^= CommunityMemberBanCreated.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberBanDeleted)
		{
			num ^= CommunityMemberBanDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberRoleCreated)
		{
			num ^= CommunityMemberRoleCreated.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberRoleSetPrimary)
		{
			num ^= CommunityMemberRoleSetPrimary.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberRoleDeleted)
		{
			num ^= CommunityMemberRoleDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityRole)
		{
			num ^= CommunityRole.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityRoleDeleted)
		{
			num ^= CommunityRoleDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityRoleMoved)
		{
			num ^= CommunityRoleMoved.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityPermissionUpdate)
		{
			num ^= CommunityPermissionUpdate.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityAppAdded)
		{
			num ^= CommunityAppAdded.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityAppRemoved)
		{
			num ^= CommunityAppRemoved.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityAppSetStatus)
		{
			num ^= CommunityAppSetStatus.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityAppSetSettings)
		{
			num ^= CommunityAppSetSettings.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityAppVersionUpdateNotification)
		{
			num ^= CommunityAppVersionUpdateNotification.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.CommunityAppSetButton)
		{
			num ^= CommunityAppSetButton.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.DirectMessageCreated)
		{
			num ^= DirectMessageCreated.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.DirectMessageMemberAdded)
		{
			num ^= DirectMessageMemberAdded.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.DirectMessageMemberDeleted)
		{
			num ^= DirectMessageMemberDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.DirectMessageRing)
		{
			num ^= DirectMessageRing.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.DirectMessageRingDeclined)
		{
			num ^= DirectMessageRingDeclined.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.DirectMessageLastMessageDeleted)
		{
			num ^= DirectMessageLastMessageDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.Directory)
		{
			num ^= Directory.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.DirectoryMoved)
		{
			num ^= DirectoryMoved.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.DirectoryDeleted)
		{
			num ^= DirectoryDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.FileCreated)
		{
			num ^= FileCreated.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.FileEdited)
		{
			num ^= FileEdited.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.FileMoved)
		{
			num ^= FileMoved.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.FileDeleted)
		{
			num ^= FileDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.FriendshipCreated)
		{
			num ^= FriendshipCreated.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.FriendshipMoved)
		{
			num ^= FriendshipMoved.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.FriendshipDeleted)
		{
			num ^= FriendshipDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupCreated)
		{
			num ^= FriendshipGroupCreated.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupEdited)
		{
			num ^= FriendshipGroupEdited.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupMoved)
		{
			num ^= FriendshipGroupMoved.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupDeleted)
		{
			num ^= FriendshipGroupDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.Message)
		{
			num ^= Message.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.MessageDeleted)
		{
			num ^= MessageDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.MessagePin)
		{
			num ^= MessagePin.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.MessageReaction)
		{
			num ^= MessageReaction.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.MessageSetTypingIndicator)
		{
			num ^= MessageSetTypingIndicator.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.MessageSetViewTime)
		{
			num ^= MessageSetViewTime.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.Notification)
		{
			num ^= Notification.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.NotificationViewed)
		{
			num ^= NotificationViewed.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.NotificationViewedAll)
		{
			num ^= NotificationViewedAll.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.NotificationDeleted)
		{
			num ^= NotificationDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.NotificationDeletedAll)
		{
			num ^= NotificationDeletedAll.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.UserSetProfile)
		{
			num ^= UserSetProfile.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.UserSetStatus)
		{
			num ^= UserSetStatus.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.UserSetEmailVerification)
		{
			num ^= UserSetEmailVerification.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.UserSetMaxStatus)
		{
			num ^= UserSetMaxStatus.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.UserDeleted)
		{
			num ^= UserDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.UserSetBadges)
		{
			num ^= UserSetBadges.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.UserSetDirectMessageInviteRequirement)
		{
			num ^= UserSetDirectMessageInviteRequirement.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.UserSetFriendshipInviteRequirement)
		{
			num ^= UserSetFriendshipInviteRequirement.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDevice)
		{
			num ^= WebRtcUserDevice.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDetach)
		{
			num ^= WebRtcUserDetach.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetStatus)
		{
			num ^= WebRtcUserDeviceSetStatus.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetTransport)
		{
			num ^= WebRtcUserDeviceSetTransport.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetDataChannel)
		{
			num ^= WebRtcUserDeviceSetDataChannel.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.UserBlockCreated)
		{
			num ^= UserBlockCreated.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.UserBlockDeleted)
		{
			num ^= UserBlockDeleted.GetHashCode();
		}
		if (packetCase_ == PacketOneofCase.AssetChanged)
		{
			num ^= AssetChanged.GetHashCode();
		}
		num ^= (int)packetCase_;
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
		if (packetCase_ == PacketOneofCase.Ping)
		{
			P_0.WriteRawTag(10);
			P_0.WriteMessage(Ping);
		}
		if (packetCase_ == PacketOneofCase.HubServerMove)
		{
			P_0.WriteRawTag(18);
			P_0.WriteMessage(HubServerMove);
		}
		if (packetCase_ == PacketOneofCase.ChannelCreated)
		{
			P_0.WriteRawTag(242, 1);
			P_0.WriteMessage(ChannelCreated);
		}
		if (packetCase_ == PacketOneofCase.ChannelEdited)
		{
			P_0.WriteRawTag(250, 1);
			P_0.WriteMessage(ChannelEdited);
		}
		if (packetCase_ == PacketOneofCase.ChannelMoved)
		{
			P_0.WriteRawTag(130, 2);
			P_0.WriteMessage(ChannelMoved);
		}
		if (packetCase_ == PacketOneofCase.ChannelDeleted)
		{
			P_0.WriteRawTag(138, 2);
			P_0.WriteMessage(ChannelDeleted);
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupCreated)
		{
			P_0.WriteRawTag(194, 2);
			P_0.WriteMessage(ChannelGroupCreated);
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupEdited)
		{
			P_0.WriteRawTag(202, 2);
			P_0.WriteMessage(ChannelGroupEdited);
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupMoved)
		{
			P_0.WriteRawTag(210, 2);
			P_0.WriteMessage(ChannelGroupMoved);
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupDeleted)
		{
			P_0.WriteRawTag(218, 2);
			P_0.WriteMessage(ChannelGroupDeleted);
		}
		if (packetCase_ == PacketOneofCase.Community)
		{
			P_0.WriteRawTag(146, 3);
			P_0.WriteMessage(Community);
		}
		if (packetCase_ == PacketOneofCase.CommunityJoined)
		{
			P_0.WriteRawTag(154, 3);
			P_0.WriteMessage(CommunityJoined);
		}
		if (packetCase_ == PacketOneofCase.CommunityLeave)
		{
			P_0.WriteRawTag(162, 3);
			P_0.WriteMessage(CommunityLeave);
		}
		if (packetCase_ == PacketOneofCase.CommunityDeleted)
		{
			P_0.WriteRawTag(170, 3);
			P_0.WriteMessage(CommunityDeleted);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberAttach)
		{
			P_0.WriteRawTag(226, 3);
			P_0.WriteMessage(CommunityMemberAttach);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberDetach)
		{
			P_0.WriteRawTag(234, 3);
			P_0.WriteMessage(CommunityMemberDetach);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberEdited)
		{
			P_0.WriteRawTag(242, 3);
			P_0.WriteMessage(CommunityMemberEdited);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberEditedExternal)
		{
			P_0.WriteRawTag(250, 3);
			P_0.WriteMessage(CommunityMemberEditedExternal);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberBanCreated)
		{
			P_0.WriteRawTag(178, 4);
			P_0.WriteMessage(CommunityMemberBanCreated);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberBanDeleted)
		{
			P_0.WriteRawTag(186, 4);
			P_0.WriteMessage(CommunityMemberBanDeleted);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberRoleCreated)
		{
			P_0.WriteRawTag(130, 5);
			P_0.WriteMessage(CommunityMemberRoleCreated);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberRoleSetPrimary)
		{
			P_0.WriteRawTag(138, 5);
			P_0.WriteMessage(CommunityMemberRoleSetPrimary);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberRoleDeleted)
		{
			P_0.WriteRawTag(146, 5);
			P_0.WriteMessage(CommunityMemberRoleDeleted);
		}
		if (packetCase_ == PacketOneofCase.CommunityRole)
		{
			P_0.WriteRawTag(210, 5);
			P_0.WriteMessage(CommunityRole);
		}
		if (packetCase_ == PacketOneofCase.CommunityRoleDeleted)
		{
			P_0.WriteRawTag(218, 5);
			P_0.WriteMessage(CommunityRoleDeleted);
		}
		if (packetCase_ == PacketOneofCase.CommunityRoleMoved)
		{
			P_0.WriteRawTag(226, 5);
			P_0.WriteMessage(CommunityRoleMoved);
		}
		if (packetCase_ == PacketOneofCase.CommunityPermissionUpdate)
		{
			P_0.WriteRawTag(162, 6);
			P_0.WriteMessage(CommunityPermissionUpdate);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppAdded)
		{
			P_0.WriteRawTag(242, 6);
			P_0.WriteMessage(CommunityAppAdded);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppRemoved)
		{
			P_0.WriteRawTag(250, 6);
			P_0.WriteMessage(CommunityAppRemoved);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppSetStatus)
		{
			P_0.WriteRawTag(130, 7);
			P_0.WriteMessage(CommunityAppSetStatus);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppSetSettings)
		{
			P_0.WriteRawTag(138, 7);
			P_0.WriteMessage(CommunityAppSetSettings);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppVersionUpdateNotification)
		{
			P_0.WriteRawTag(146, 7);
			P_0.WriteMessage(CommunityAppVersionUpdateNotification);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppSetButton)
		{
			P_0.WriteRawTag(154, 7);
			P_0.WriteMessage(CommunityAppSetButton);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageCreated)
		{
			P_0.WriteRawTag(194, 7);
			P_0.WriteMessage(DirectMessageCreated);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageMemberAdded)
		{
			P_0.WriteRawTag(202, 7);
			P_0.WriteMessage(DirectMessageMemberAdded);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageMemberDeleted)
		{
			P_0.WriteRawTag(210, 7);
			P_0.WriteMessage(DirectMessageMemberDeleted);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageRing)
		{
			P_0.WriteRawTag(218, 7);
			P_0.WriteMessage(DirectMessageRing);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageRingDeclined)
		{
			P_0.WriteRawTag(226, 7);
			P_0.WriteMessage(DirectMessageRingDeclined);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageLastMessageDeleted)
		{
			P_0.WriteRawTag(234, 7);
			P_0.WriteMessage(DirectMessageLastMessageDeleted);
		}
		if (packetCase_ == PacketOneofCase.Directory)
		{
			P_0.WriteRawTag(146, 8);
			P_0.WriteMessage(Directory);
		}
		if (packetCase_ == PacketOneofCase.DirectoryMoved)
		{
			P_0.WriteRawTag(154, 8);
			P_0.WriteMessage(DirectoryMoved);
		}
		if (packetCase_ == PacketOneofCase.DirectoryDeleted)
		{
			P_0.WriteRawTag(162, 8);
			P_0.WriteMessage(DirectoryDeleted);
		}
		if (packetCase_ == PacketOneofCase.FileCreated)
		{
			P_0.WriteRawTag(226, 8);
			P_0.WriteMessage(FileCreated);
		}
		if (packetCase_ == PacketOneofCase.FileEdited)
		{
			P_0.WriteRawTag(234, 8);
			P_0.WriteMessage(FileEdited);
		}
		if (packetCase_ == PacketOneofCase.FileMoved)
		{
			P_0.WriteRawTag(242, 8);
			P_0.WriteMessage(FileMoved);
		}
		if (packetCase_ == PacketOneofCase.FileDeleted)
		{
			P_0.WriteRawTag(250, 8);
			P_0.WriteMessage(FileDeleted);
		}
		if (packetCase_ == PacketOneofCase.FriendshipCreated)
		{
			P_0.WriteRawTag(178, 9);
			P_0.WriteMessage(FriendshipCreated);
		}
		if (packetCase_ == PacketOneofCase.FriendshipMoved)
		{
			P_0.WriteRawTag(186, 9);
			P_0.WriteMessage(FriendshipMoved);
		}
		if (packetCase_ == PacketOneofCase.FriendshipDeleted)
		{
			P_0.WriteRawTag(194, 9);
			P_0.WriteMessage(FriendshipDeleted);
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupCreated)
		{
			P_0.WriteRawTag(130, 10);
			P_0.WriteMessage(FriendshipGroupCreated);
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupEdited)
		{
			P_0.WriteRawTag(138, 10);
			P_0.WriteMessage(FriendshipGroupEdited);
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupMoved)
		{
			P_0.WriteRawTag(146, 10);
			P_0.WriteMessage(FriendshipGroupMoved);
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupDeleted)
		{
			P_0.WriteRawTag(154, 10);
			P_0.WriteMessage(FriendshipGroupDeleted);
		}
		if (packetCase_ == PacketOneofCase.Message)
		{
			P_0.WriteRawTag(210, 10);
			P_0.WriteMessage(Message);
		}
		if (packetCase_ == PacketOneofCase.MessageDeleted)
		{
			P_0.WriteRawTag(218, 10);
			P_0.WriteMessage(MessageDeleted);
		}
		if (packetCase_ == PacketOneofCase.MessagePin)
		{
			P_0.WriteRawTag(226, 10);
			P_0.WriteMessage(MessagePin);
		}
		if (packetCase_ == PacketOneofCase.MessageReaction)
		{
			P_0.WriteRawTag(234, 10);
			P_0.WriteMessage(MessageReaction);
		}
		if (packetCase_ == PacketOneofCase.MessageSetTypingIndicator)
		{
			P_0.WriteRawTag(242, 10);
			P_0.WriteMessage(MessageSetTypingIndicator);
		}
		if (packetCase_ == PacketOneofCase.MessageSetViewTime)
		{
			P_0.WriteRawTag(250, 10);
			P_0.WriteMessage(MessageSetViewTime);
		}
		if (packetCase_ == PacketOneofCase.Notification)
		{
			P_0.WriteRawTag(162, 11);
			P_0.WriteMessage(Notification);
		}
		if (packetCase_ == PacketOneofCase.NotificationViewed)
		{
			P_0.WriteRawTag(170, 11);
			P_0.WriteMessage(NotificationViewed);
		}
		if (packetCase_ == PacketOneofCase.NotificationViewedAll)
		{
			P_0.WriteRawTag(178, 11);
			P_0.WriteMessage(NotificationViewedAll);
		}
		if (packetCase_ == PacketOneofCase.NotificationDeleted)
		{
			P_0.WriteRawTag(186, 11);
			P_0.WriteMessage(NotificationDeleted);
		}
		if (packetCase_ == PacketOneofCase.NotificationDeletedAll)
		{
			P_0.WriteRawTag(194, 11);
			P_0.WriteMessage(NotificationDeletedAll);
		}
		if (packetCase_ == PacketOneofCase.UserSetProfile)
		{
			P_0.WriteRawTag(242, 11);
			P_0.WriteMessage(UserSetProfile);
		}
		if (packetCase_ == PacketOneofCase.UserSetStatus)
		{
			P_0.WriteRawTag(250, 11);
			P_0.WriteMessage(UserSetStatus);
		}
		if (packetCase_ == PacketOneofCase.UserSetEmailVerification)
		{
			P_0.WriteRawTag(130, 12);
			P_0.WriteMessage(UserSetEmailVerification);
		}
		if (packetCase_ == PacketOneofCase.UserSetMaxStatus)
		{
			P_0.WriteRawTag(138, 12);
			P_0.WriteMessage(UserSetMaxStatus);
		}
		if (packetCase_ == PacketOneofCase.UserDeleted)
		{
			P_0.WriteRawTag(146, 12);
			P_0.WriteMessage(UserDeleted);
		}
		if (packetCase_ == PacketOneofCase.UserSetBadges)
		{
			P_0.WriteRawTag(154, 12);
			P_0.WriteMessage(UserSetBadges);
		}
		if (packetCase_ == PacketOneofCase.UserSetDirectMessageInviteRequirement)
		{
			P_0.WriteRawTag(162, 12);
			P_0.WriteMessage(UserSetDirectMessageInviteRequirement);
		}
		if (packetCase_ == PacketOneofCase.UserSetFriendshipInviteRequirement)
		{
			P_0.WriteRawTag(170, 12);
			P_0.WriteMessage(UserSetFriendshipInviteRequirement);
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDevice)
		{
			P_0.WriteRawTag(194, 12);
			P_0.WriteMessage(WebRtcUserDevice);
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDetach)
		{
			P_0.WriteRawTag(202, 12);
			P_0.WriteMessage(WebRtcUserDetach);
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetStatus)
		{
			P_0.WriteRawTag(210, 12);
			P_0.WriteMessage(WebRtcUserDeviceSetStatus);
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetTransport)
		{
			P_0.WriteRawTag(218, 12);
			P_0.WriteMessage(WebRtcUserDeviceSetTransport);
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetDataChannel)
		{
			P_0.WriteRawTag(226, 12);
			P_0.WriteMessage(WebRtcUserDeviceSetDataChannel);
		}
		if (packetCase_ == PacketOneofCase.UserBlockCreated)
		{
			P_0.WriteRawTag(146, 13);
			P_0.WriteMessage(UserBlockCreated);
		}
		if (packetCase_ == PacketOneofCase.UserBlockDeleted)
		{
			P_0.WriteRawTag(154, 13);
			P_0.WriteMessage(UserBlockDeleted);
		}
		if (packetCase_ == PacketOneofCase.AssetChanged)
		{
			P_0.WriteRawTag(226, 13);
			P_0.WriteMessage(AssetChanged);
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
		if (packetCase_ == PacketOneofCase.Ping)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(Ping);
		}
		if (packetCase_ == PacketOneofCase.HubServerMove)
		{
			num += 1 + CodedOutputStream.ComputeMessageSize(HubServerMove);
		}
		if (packetCase_ == PacketOneofCase.ChannelCreated)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelCreated);
		}
		if (packetCase_ == PacketOneofCase.ChannelEdited)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelEdited);
		}
		if (packetCase_ == PacketOneofCase.ChannelMoved)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelMoved);
		}
		if (packetCase_ == PacketOneofCase.ChannelDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelDeleted);
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupCreated)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelGroupCreated);
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupEdited)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelGroupEdited);
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupMoved)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelGroupMoved);
		}
		if (packetCase_ == PacketOneofCase.ChannelGroupDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChannelGroupDeleted);
		}
		if (packetCase_ == PacketOneofCase.Community)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Community);
		}
		if (packetCase_ == PacketOneofCase.CommunityJoined)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityJoined);
		}
		if (packetCase_ == PacketOneofCase.CommunityLeave)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityLeave);
		}
		if (packetCase_ == PacketOneofCase.CommunityDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityDeleted);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberAttach)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityMemberAttach);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberDetach)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityMemberDetach);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberEdited)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityMemberEdited);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberEditedExternal)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityMemberEditedExternal);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberBanCreated)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityMemberBanCreated);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberBanDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityMemberBanDeleted);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberRoleCreated)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityMemberRoleCreated);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberRoleSetPrimary)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityMemberRoleSetPrimary);
		}
		if (packetCase_ == PacketOneofCase.CommunityMemberRoleDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityMemberRoleDeleted);
		}
		if (packetCase_ == PacketOneofCase.CommunityRole)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityRole);
		}
		if (packetCase_ == PacketOneofCase.CommunityRoleDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityRoleDeleted);
		}
		if (packetCase_ == PacketOneofCase.CommunityRoleMoved)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityRoleMoved);
		}
		if (packetCase_ == PacketOneofCase.CommunityPermissionUpdate)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityPermissionUpdate);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppAdded)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityAppAdded);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppRemoved)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityAppRemoved);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppSetStatus)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityAppSetStatus);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppSetSettings)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityAppSetSettings);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppVersionUpdateNotification)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityAppVersionUpdateNotification);
		}
		if (packetCase_ == PacketOneofCase.CommunityAppSetButton)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CommunityAppSetButton);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageCreated)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DirectMessageCreated);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageMemberAdded)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DirectMessageMemberAdded);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageMemberDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DirectMessageMemberDeleted);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageRing)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DirectMessageRing);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageRingDeclined)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DirectMessageRingDeclined);
		}
		if (packetCase_ == PacketOneofCase.DirectMessageLastMessageDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DirectMessageLastMessageDeleted);
		}
		if (packetCase_ == PacketOneofCase.Directory)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Directory);
		}
		if (packetCase_ == PacketOneofCase.DirectoryMoved)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DirectoryMoved);
		}
		if (packetCase_ == PacketOneofCase.DirectoryDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DirectoryDeleted);
		}
		if (packetCase_ == PacketOneofCase.FileCreated)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FileCreated);
		}
		if (packetCase_ == PacketOneofCase.FileEdited)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FileEdited);
		}
		if (packetCase_ == PacketOneofCase.FileMoved)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FileMoved);
		}
		if (packetCase_ == PacketOneofCase.FileDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FileDeleted);
		}
		if (packetCase_ == PacketOneofCase.FriendshipCreated)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendshipCreated);
		}
		if (packetCase_ == PacketOneofCase.FriendshipMoved)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendshipMoved);
		}
		if (packetCase_ == PacketOneofCase.FriendshipDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendshipDeleted);
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupCreated)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendshipGroupCreated);
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupEdited)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendshipGroupEdited);
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupMoved)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendshipGroupMoved);
		}
		if (packetCase_ == PacketOneofCase.FriendshipGroupDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendshipGroupDeleted);
		}
		if (packetCase_ == PacketOneofCase.Message)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Message);
		}
		if (packetCase_ == PacketOneofCase.MessageDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MessageDeleted);
		}
		if (packetCase_ == PacketOneofCase.MessagePin)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MessagePin);
		}
		if (packetCase_ == PacketOneofCase.MessageReaction)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MessageReaction);
		}
		if (packetCase_ == PacketOneofCase.MessageSetTypingIndicator)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MessageSetTypingIndicator);
		}
		if (packetCase_ == PacketOneofCase.MessageSetViewTime)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MessageSetViewTime);
		}
		if (packetCase_ == PacketOneofCase.Notification)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Notification);
		}
		if (packetCase_ == PacketOneofCase.NotificationViewed)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(NotificationViewed);
		}
		if (packetCase_ == PacketOneofCase.NotificationViewedAll)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(NotificationViewedAll);
		}
		if (packetCase_ == PacketOneofCase.NotificationDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(NotificationDeleted);
		}
		if (packetCase_ == PacketOneofCase.NotificationDeletedAll)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(NotificationDeletedAll);
		}
		if (packetCase_ == PacketOneofCase.UserSetProfile)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UserSetProfile);
		}
		if (packetCase_ == PacketOneofCase.UserSetStatus)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UserSetStatus);
		}
		if (packetCase_ == PacketOneofCase.UserSetEmailVerification)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UserSetEmailVerification);
		}
		if (packetCase_ == PacketOneofCase.UserSetMaxStatus)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UserSetMaxStatus);
		}
		if (packetCase_ == PacketOneofCase.UserDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UserDeleted);
		}
		if (packetCase_ == PacketOneofCase.UserSetBadges)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UserSetBadges);
		}
		if (packetCase_ == PacketOneofCase.UserSetDirectMessageInviteRequirement)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UserSetDirectMessageInviteRequirement);
		}
		if (packetCase_ == PacketOneofCase.UserSetFriendshipInviteRequirement)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UserSetFriendshipInviteRequirement);
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDevice)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(WebRtcUserDevice);
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDetach)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(WebRtcUserDetach);
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetStatus)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(WebRtcUserDeviceSetStatus);
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetTransport)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(WebRtcUserDeviceSetTransport);
		}
		if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetDataChannel)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(WebRtcUserDeviceSetDataChannel);
		}
		if (packetCase_ == PacketOneofCase.UserBlockCreated)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UserBlockCreated);
		}
		if (packetCase_ == PacketOneofCase.UserBlockDeleted)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UserBlockDeleted);
		}
		if (packetCase_ == PacketOneofCase.AssetChanged)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(AssetChanged);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[GeneratedCode("protoc", null)]
	public void MergeFrom(PacketContainer other)
	{
		if (other == null)
		{
			return;
		}
		switch (other.PacketCase)
		{
		case PacketOneofCase.Ping:
			if (Ping == null)
			{
				Ping = new PingPacket();
			}
			Ping.MergeFrom(other.Ping);
			break;
		case PacketOneofCase.HubServerMove:
			if (HubServerMove == null)
			{
				HubServerMove = new HubServerMovePacket();
			}
			HubServerMove.MergeFrom(other.HubServerMove);
			break;
		case PacketOneofCase.ChannelCreated:
			if (ChannelCreated == null)
			{
				ChannelCreated = new ChannelCreatedPacket();
			}
			ChannelCreated.MergeFrom(other.ChannelCreated);
			break;
		case PacketOneofCase.ChannelEdited:
			if (ChannelEdited == null)
			{
				ChannelEdited = new ChannelEditedPacket();
			}
			ChannelEdited.MergeFrom(other.ChannelEdited);
			break;
		case PacketOneofCase.ChannelMoved:
			if (ChannelMoved == null)
			{
				ChannelMoved = new ChannelMovedPacket();
			}
			ChannelMoved.MergeFrom(other.ChannelMoved);
			break;
		case PacketOneofCase.ChannelDeleted:
			if (ChannelDeleted == null)
			{
				ChannelDeleted = new ChannelDeletedPacket();
			}
			ChannelDeleted.MergeFrom(other.ChannelDeleted);
			break;
		case PacketOneofCase.ChannelGroupCreated:
			if (ChannelGroupCreated == null)
			{
				ChannelGroupCreated = new ChannelGroupCreatedPacket();
			}
			ChannelGroupCreated.MergeFrom(other.ChannelGroupCreated);
			break;
		case PacketOneofCase.ChannelGroupEdited:
			if (ChannelGroupEdited == null)
			{
				ChannelGroupEdited = new ChannelGroupEditedPacket();
			}
			ChannelGroupEdited.MergeFrom(other.ChannelGroupEdited);
			break;
		case PacketOneofCase.ChannelGroupMoved:
			if (ChannelGroupMoved == null)
			{
				ChannelGroupMoved = new ChannelGroupMovedPacket();
			}
			ChannelGroupMoved.MergeFrom(other.ChannelGroupMoved);
			break;
		case PacketOneofCase.ChannelGroupDeleted:
			if (ChannelGroupDeleted == null)
			{
				ChannelGroupDeleted = new ChannelGroupDeletedPacket();
			}
			ChannelGroupDeleted.MergeFrom(other.ChannelGroupDeleted);
			break;
		case PacketOneofCase.Community:
			if (Community == null)
			{
				Community = new CommunityPacket();
			}
			Community.MergeFrom(other.Community);
			break;
		case PacketOneofCase.CommunityJoined:
			if (CommunityJoined == null)
			{
				CommunityJoined = new CommunityJoinedPacket();
			}
			CommunityJoined.MergeFrom(other.CommunityJoined);
			break;
		case PacketOneofCase.CommunityLeave:
			if (CommunityLeave == null)
			{
				CommunityLeave = new CommunityLeavePacket();
			}
			CommunityLeave.MergeFrom(other.CommunityLeave);
			break;
		case PacketOneofCase.CommunityDeleted:
			if (CommunityDeleted == null)
			{
				CommunityDeleted = new CommunityDeletedPacket();
			}
			CommunityDeleted.MergeFrom(other.CommunityDeleted);
			break;
		case PacketOneofCase.CommunityMemberAttach:
			if (CommunityMemberAttach == null)
			{
				CommunityMemberAttach = new CommunityMemberAttachPacket();
			}
			CommunityMemberAttach.MergeFrom(other.CommunityMemberAttach);
			break;
		case PacketOneofCase.CommunityMemberDetach:
			if (CommunityMemberDetach == null)
			{
				CommunityMemberDetach = new CommunityMemberDetachPacket();
			}
			CommunityMemberDetach.MergeFrom(other.CommunityMemberDetach);
			break;
		case PacketOneofCase.CommunityMemberEdited:
			if (CommunityMemberEdited == null)
			{
				CommunityMemberEdited = new CommunityMemberEditedPacket();
			}
			CommunityMemberEdited.MergeFrom(other.CommunityMemberEdited);
			break;
		case PacketOneofCase.CommunityMemberEditedExternal:
			if (CommunityMemberEditedExternal == null)
			{
				CommunityMemberEditedExternal = new CommunityMemberEditedExternalPacket();
			}
			CommunityMemberEditedExternal.MergeFrom(other.CommunityMemberEditedExternal);
			break;
		case PacketOneofCase.CommunityMemberBanCreated:
			if (CommunityMemberBanCreated == null)
			{
				CommunityMemberBanCreated = new CommunityMemberBanCreatedPacket();
			}
			CommunityMemberBanCreated.MergeFrom(other.CommunityMemberBanCreated);
			break;
		case PacketOneofCase.CommunityMemberBanDeleted:
			if (CommunityMemberBanDeleted == null)
			{
				CommunityMemberBanDeleted = new CommunityMemberBanDeletedPacket();
			}
			CommunityMemberBanDeleted.MergeFrom(other.CommunityMemberBanDeleted);
			break;
		case PacketOneofCase.CommunityMemberRoleCreated:
			if (CommunityMemberRoleCreated == null)
			{
				CommunityMemberRoleCreated = new CommunityMemberRoleCreatedPacket();
			}
			CommunityMemberRoleCreated.MergeFrom(other.CommunityMemberRoleCreated);
			break;
		case PacketOneofCase.CommunityMemberRoleSetPrimary:
			if (CommunityMemberRoleSetPrimary == null)
			{
				CommunityMemberRoleSetPrimary = new CommunityMemberRoleSetPrimaryPacket();
			}
			CommunityMemberRoleSetPrimary.MergeFrom(other.CommunityMemberRoleSetPrimary);
			break;
		case PacketOneofCase.CommunityMemberRoleDeleted:
			if (CommunityMemberRoleDeleted == null)
			{
				CommunityMemberRoleDeleted = new CommunityMemberRoleDeletedPacket();
			}
			CommunityMemberRoleDeleted.MergeFrom(other.CommunityMemberRoleDeleted);
			break;
		case PacketOneofCase.CommunityRole:
			if (CommunityRole == null)
			{
				CommunityRole = new CommunityRolePacket();
			}
			CommunityRole.MergeFrom(other.CommunityRole);
			break;
		case PacketOneofCase.CommunityRoleDeleted:
			if (CommunityRoleDeleted == null)
			{
				CommunityRoleDeleted = new CommunityRoleDeletedPacket();
			}
			CommunityRoleDeleted.MergeFrom(other.CommunityRoleDeleted);
			break;
		case PacketOneofCase.CommunityRoleMoved:
			if (CommunityRoleMoved == null)
			{
				CommunityRoleMoved = new CommunityRoleMovedPacket();
			}
			CommunityRoleMoved.MergeFrom(other.CommunityRoleMoved);
			break;
		case PacketOneofCase.CommunityPermissionUpdate:
			if (CommunityPermissionUpdate == null)
			{
				CommunityPermissionUpdate = new CommunityPermissionUpdatePacket();
			}
			CommunityPermissionUpdate.MergeFrom(other.CommunityPermissionUpdate);
			break;
		case PacketOneofCase.CommunityAppAdded:
			if (CommunityAppAdded == null)
			{
				CommunityAppAdded = new CommunityAppAddedPacket();
			}
			CommunityAppAdded.MergeFrom(other.CommunityAppAdded);
			break;
		case PacketOneofCase.CommunityAppRemoved:
			if (CommunityAppRemoved == null)
			{
				CommunityAppRemoved = new CommunityAppRemovedPacket();
			}
			CommunityAppRemoved.MergeFrom(other.CommunityAppRemoved);
			break;
		case PacketOneofCase.CommunityAppSetStatus:
			if (CommunityAppSetStatus == null)
			{
				CommunityAppSetStatus = new CommunityAppSetStatusPacket();
			}
			CommunityAppSetStatus.MergeFrom(other.CommunityAppSetStatus);
			break;
		case PacketOneofCase.CommunityAppSetSettings:
			if (CommunityAppSetSettings == null)
			{
				CommunityAppSetSettings = new CommunityAppSetSettingsPacket();
			}
			CommunityAppSetSettings.MergeFrom(other.CommunityAppSetSettings);
			break;
		case PacketOneofCase.CommunityAppVersionUpdateNotification:
			if (CommunityAppVersionUpdateNotification == null)
			{
				CommunityAppVersionUpdateNotification = new CommunityAppVersionUpdateNotificationPacket();
			}
			CommunityAppVersionUpdateNotification.MergeFrom(other.CommunityAppVersionUpdateNotification);
			break;
		case PacketOneofCase.CommunityAppSetButton:
			if (CommunityAppSetButton == null)
			{
				CommunityAppSetButton = new CommunityAppSetButtonPacket();
			}
			CommunityAppSetButton.MergeFrom(other.CommunityAppSetButton);
			break;
		case PacketOneofCase.DirectMessageCreated:
			if (DirectMessageCreated == null)
			{
				DirectMessageCreated = new DirectMessageCreatedPacket();
			}
			DirectMessageCreated.MergeFrom(other.DirectMessageCreated);
			break;
		case PacketOneofCase.DirectMessageMemberAdded:
			if (DirectMessageMemberAdded == null)
			{
				DirectMessageMemberAdded = new DirectMessageMemberAddedPacket();
			}
			DirectMessageMemberAdded.MergeFrom(other.DirectMessageMemberAdded);
			break;
		case PacketOneofCase.DirectMessageMemberDeleted:
			if (DirectMessageMemberDeleted == null)
			{
				DirectMessageMemberDeleted = new DirectMessageMemberDeletedPacket();
			}
			DirectMessageMemberDeleted.MergeFrom(other.DirectMessageMemberDeleted);
			break;
		case PacketOneofCase.DirectMessageRing:
			if (DirectMessageRing == null)
			{
				DirectMessageRing = new DirectMessageRingPacket();
			}
			DirectMessageRing.MergeFrom(other.DirectMessageRing);
			break;
		case PacketOneofCase.DirectMessageRingDeclined:
			if (DirectMessageRingDeclined == null)
			{
				DirectMessageRingDeclined = new DirectMessageRingDeclinedPacket();
			}
			DirectMessageRingDeclined.MergeFrom(other.DirectMessageRingDeclined);
			break;
		case PacketOneofCase.DirectMessageLastMessageDeleted:
			if (DirectMessageLastMessageDeleted == null)
			{
				DirectMessageLastMessageDeleted = new DirectMessageLastMessageDeletedPacket();
			}
			DirectMessageLastMessageDeleted.MergeFrom(other.DirectMessageLastMessageDeleted);
			break;
		case PacketOneofCase.Directory:
			if (Directory == null)
			{
				Directory = new DirectoryPacket();
			}
			Directory.MergeFrom(other.Directory);
			break;
		case PacketOneofCase.DirectoryMoved:
			if (DirectoryMoved == null)
			{
				DirectoryMoved = new DirectoryMovedPacket();
			}
			DirectoryMoved.MergeFrom(other.DirectoryMoved);
			break;
		case PacketOneofCase.DirectoryDeleted:
			if (DirectoryDeleted == null)
			{
				DirectoryDeleted = new DirectoryDeletedPacket();
			}
			DirectoryDeleted.MergeFrom(other.DirectoryDeleted);
			break;
		case PacketOneofCase.FileCreated:
			if (FileCreated == null)
			{
				FileCreated = new FileCreatedPacket();
			}
			FileCreated.MergeFrom(other.FileCreated);
			break;
		case PacketOneofCase.FileEdited:
			if (FileEdited == null)
			{
				FileEdited = new FileEditedPacket();
			}
			FileEdited.MergeFrom(other.FileEdited);
			break;
		case PacketOneofCase.FileMoved:
			if (FileMoved == null)
			{
				FileMoved = new FileMovedPacket();
			}
			FileMoved.MergeFrom(other.FileMoved);
			break;
		case PacketOneofCase.FileDeleted:
			if (FileDeleted == null)
			{
				FileDeleted = new FileDeletedPacket();
			}
			FileDeleted.MergeFrom(other.FileDeleted);
			break;
		case PacketOneofCase.FriendshipCreated:
			if (FriendshipCreated == null)
			{
				FriendshipCreated = new FriendshipCreatedPacket();
			}
			FriendshipCreated.MergeFrom(other.FriendshipCreated);
			break;
		case PacketOneofCase.FriendshipMoved:
			if (FriendshipMoved == null)
			{
				FriendshipMoved = new FriendshipMovedPacket();
			}
			FriendshipMoved.MergeFrom(other.FriendshipMoved);
			break;
		case PacketOneofCase.FriendshipDeleted:
			if (FriendshipDeleted == null)
			{
				FriendshipDeleted = new FriendshipDeletedPacket();
			}
			FriendshipDeleted.MergeFrom(other.FriendshipDeleted);
			break;
		case PacketOneofCase.FriendshipGroupCreated:
			if (FriendshipGroupCreated == null)
			{
				FriendshipGroupCreated = new FriendshipGroupCreatedPacket();
			}
			FriendshipGroupCreated.MergeFrom(other.FriendshipGroupCreated);
			break;
		case PacketOneofCase.FriendshipGroupEdited:
			if (FriendshipGroupEdited == null)
			{
				FriendshipGroupEdited = new FriendshipGroupEditedPacket();
			}
			FriendshipGroupEdited.MergeFrom(other.FriendshipGroupEdited);
			break;
		case PacketOneofCase.FriendshipGroupMoved:
			if (FriendshipGroupMoved == null)
			{
				FriendshipGroupMoved = new FriendshipGroupMovedPacket();
			}
			FriendshipGroupMoved.MergeFrom(other.FriendshipGroupMoved);
			break;
		case PacketOneofCase.FriendshipGroupDeleted:
			if (FriendshipGroupDeleted == null)
			{
				FriendshipGroupDeleted = new FriendshipGroupDeletedPacket();
			}
			FriendshipGroupDeleted.MergeFrom(other.FriendshipGroupDeleted);
			break;
		case PacketOneofCase.Message:
			if (Message == null)
			{
				Message = new MessagePacket();
			}
			Message.MergeFrom(other.Message);
			break;
		case PacketOneofCase.MessageDeleted:
			if (MessageDeleted == null)
			{
				MessageDeleted = new MessageDeletedPacket();
			}
			MessageDeleted.MergeFrom(other.MessageDeleted);
			break;
		case PacketOneofCase.MessagePin:
			if (MessagePin == null)
			{
				MessagePin = new MessagePinPacket();
			}
			MessagePin.MergeFrom(other.MessagePin);
			break;
		case PacketOneofCase.MessageReaction:
			if (MessageReaction == null)
			{
				MessageReaction = new MessageReactionPacket();
			}
			MessageReaction.MergeFrom(other.MessageReaction);
			break;
		case PacketOneofCase.MessageSetTypingIndicator:
			if (MessageSetTypingIndicator == null)
			{
				MessageSetTypingIndicator = new MessageSetTypingIndicatorPacket();
			}
			MessageSetTypingIndicator.MergeFrom(other.MessageSetTypingIndicator);
			break;
		case PacketOneofCase.MessageSetViewTime:
			if (MessageSetViewTime == null)
			{
				MessageSetViewTime = new MessageSetViewTimePacket();
			}
			MessageSetViewTime.MergeFrom(other.MessageSetViewTime);
			break;
		case PacketOneofCase.Notification:
			if (Notification == null)
			{
				Notification = new NotificationPacket();
			}
			Notification.MergeFrom(other.Notification);
			break;
		case PacketOneofCase.NotificationViewed:
			if (NotificationViewed == null)
			{
				NotificationViewed = new NotificationViewedPacket();
			}
			NotificationViewed.MergeFrom(other.NotificationViewed);
			break;
		case PacketOneofCase.NotificationViewedAll:
			if (NotificationViewedAll == null)
			{
				NotificationViewedAll = new NotificationViewedAllPacket();
			}
			NotificationViewedAll.MergeFrom(other.NotificationViewedAll);
			break;
		case PacketOneofCase.NotificationDeleted:
			if (NotificationDeleted == null)
			{
				NotificationDeleted = new NotificationDeletedPacket();
			}
			NotificationDeleted.MergeFrom(other.NotificationDeleted);
			break;
		case PacketOneofCase.NotificationDeletedAll:
			if (NotificationDeletedAll == null)
			{
				NotificationDeletedAll = new NotificationDeletedAllPacket();
			}
			NotificationDeletedAll.MergeFrom(other.NotificationDeletedAll);
			break;
		case PacketOneofCase.UserSetProfile:
			if (UserSetProfile == null)
			{
				UserSetProfile = new UserSetProfilePacket();
			}
			UserSetProfile.MergeFrom(other.UserSetProfile);
			break;
		case PacketOneofCase.UserSetStatus:
			if (UserSetStatus == null)
			{
				UserSetStatus = new UserSetStatusPacket();
			}
			UserSetStatus.MergeFrom(other.UserSetStatus);
			break;
		case PacketOneofCase.UserSetEmailVerification:
			if (UserSetEmailVerification == null)
			{
				UserSetEmailVerification = new UserSetEmailVerificationPacket();
			}
			UserSetEmailVerification.MergeFrom(other.UserSetEmailVerification);
			break;
		case PacketOneofCase.UserSetMaxStatus:
			if (UserSetMaxStatus == null)
			{
				UserSetMaxStatus = new UserSetMaxStatusPacket();
			}
			UserSetMaxStatus.MergeFrom(other.UserSetMaxStatus);
			break;
		case PacketOneofCase.UserDeleted:
			if (UserDeleted == null)
			{
				UserDeleted = new UserDeletedPacket();
			}
			UserDeleted.MergeFrom(other.UserDeleted);
			break;
		case PacketOneofCase.UserSetBadges:
			if (UserSetBadges == null)
			{
				UserSetBadges = new UserSetBadgesPacket();
			}
			UserSetBadges.MergeFrom(other.UserSetBadges);
			break;
		case PacketOneofCase.UserSetDirectMessageInviteRequirement:
			if (UserSetDirectMessageInviteRequirement == null)
			{
				UserSetDirectMessageInviteRequirement = new UserSetDirectMessageInviteRequirementPacket();
			}
			UserSetDirectMessageInviteRequirement.MergeFrom(other.UserSetDirectMessageInviteRequirement);
			break;
		case PacketOneofCase.UserSetFriendshipInviteRequirement:
			if (UserSetFriendshipInviteRequirement == null)
			{
				UserSetFriendshipInviteRequirement = new UserSetFriendshipInviteRequirementPacket();
			}
			UserSetFriendshipInviteRequirement.MergeFrom(other.UserSetFriendshipInviteRequirement);
			break;
		case PacketOneofCase.WebRtcUserDevice:
			if (WebRtcUserDevice == null)
			{
				WebRtcUserDevice = new WebRtcUserDevicePacket();
			}
			WebRtcUserDevice.MergeFrom(other.WebRtcUserDevice);
			break;
		case PacketOneofCase.WebRtcUserDetach:
			if (WebRtcUserDetach == null)
			{
				WebRtcUserDetach = new WebRtcUserDetachPacket();
			}
			WebRtcUserDetach.MergeFrom(other.WebRtcUserDetach);
			break;
		case PacketOneofCase.WebRtcUserDeviceSetStatus:
			if (WebRtcUserDeviceSetStatus == null)
			{
				WebRtcUserDeviceSetStatus = new WebRtcUserDeviceSetStatusPacket();
			}
			WebRtcUserDeviceSetStatus.MergeFrom(other.WebRtcUserDeviceSetStatus);
			break;
		case PacketOneofCase.WebRtcUserDeviceSetTransport:
			if (WebRtcUserDeviceSetTransport == null)
			{
				WebRtcUserDeviceSetTransport = new WebRtcUserDeviceSetTransportPacket();
			}
			WebRtcUserDeviceSetTransport.MergeFrom(other.WebRtcUserDeviceSetTransport);
			break;
		case PacketOneofCase.WebRtcUserDeviceSetDataChannel:
			if (WebRtcUserDeviceSetDataChannel == null)
			{
				WebRtcUserDeviceSetDataChannel = new WebRtcUserDeviceSetDataChannelPacket();
			}
			WebRtcUserDeviceSetDataChannel.MergeFrom(other.WebRtcUserDeviceSetDataChannel);
			break;
		case PacketOneofCase.UserBlockCreated:
			if (UserBlockCreated == null)
			{
				UserBlockCreated = new UserBlockCreatedPacket();
			}
			UserBlockCreated.MergeFrom(other.UserBlockCreated);
			break;
		case PacketOneofCase.UserBlockDeleted:
			if (UserBlockDeleted == null)
			{
				UserBlockDeleted = new UserBlockDeletedPacket();
			}
			UserBlockDeleted.MergeFrom(other.UserBlockDeleted);
			break;
		case PacketOneofCase.AssetChanged:
			if (AssetChanged == null)
			{
				AssetChanged = new AssetChangedPacket();
			}
			AssetChanged.MergeFrom(other.AssetChanged);
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
			case 10u:
			{
				PingPacket pingPacket = new PingPacket();
				if (packetCase_ == PacketOneofCase.Ping)
				{
					pingPacket.MergeFrom(Ping);
				}
				P_0.ReadMessage(pingPacket);
				Ping = pingPacket;
				break;
			}
			case 18u:
			{
				HubServerMovePacket hubServerMovePacket = new HubServerMovePacket();
				if (packetCase_ == PacketOneofCase.HubServerMove)
				{
					hubServerMovePacket.MergeFrom(HubServerMove);
				}
				P_0.ReadMessage(hubServerMovePacket);
				HubServerMove = hubServerMovePacket;
				break;
			}
			case 242u:
			{
				ChannelCreatedPacket channelCreatedPacket = new ChannelCreatedPacket();
				if (packetCase_ == PacketOneofCase.ChannelCreated)
				{
					channelCreatedPacket.MergeFrom(ChannelCreated);
				}
				P_0.ReadMessage(channelCreatedPacket);
				ChannelCreated = channelCreatedPacket;
				break;
			}
			case 250u:
			{
				ChannelEditedPacket channelEditedPacket = new ChannelEditedPacket();
				if (packetCase_ == PacketOneofCase.ChannelEdited)
				{
					channelEditedPacket.MergeFrom(ChannelEdited);
				}
				P_0.ReadMessage(channelEditedPacket);
				ChannelEdited = channelEditedPacket;
				break;
			}
			case 258u:
			{
				ChannelMovedPacket channelMovedPacket = new ChannelMovedPacket();
				if (packetCase_ == PacketOneofCase.ChannelMoved)
				{
					channelMovedPacket.MergeFrom(ChannelMoved);
				}
				P_0.ReadMessage(channelMovedPacket);
				ChannelMoved = channelMovedPacket;
				break;
			}
			case 266u:
			{
				ChannelDeletedPacket channelDeletedPacket = new ChannelDeletedPacket();
				if (packetCase_ == PacketOneofCase.ChannelDeleted)
				{
					channelDeletedPacket.MergeFrom(ChannelDeleted);
				}
				P_0.ReadMessage(channelDeletedPacket);
				ChannelDeleted = channelDeletedPacket;
				break;
			}
			case 322u:
			{
				ChannelGroupCreatedPacket channelGroupCreatedPacket = new ChannelGroupCreatedPacket();
				if (packetCase_ == PacketOneofCase.ChannelGroupCreated)
				{
					channelGroupCreatedPacket.MergeFrom(ChannelGroupCreated);
				}
				P_0.ReadMessage(channelGroupCreatedPacket);
				ChannelGroupCreated = channelGroupCreatedPacket;
				break;
			}
			case 330u:
			{
				ChannelGroupEditedPacket channelGroupEditedPacket = new ChannelGroupEditedPacket();
				if (packetCase_ == PacketOneofCase.ChannelGroupEdited)
				{
					channelGroupEditedPacket.MergeFrom(ChannelGroupEdited);
				}
				P_0.ReadMessage(channelGroupEditedPacket);
				ChannelGroupEdited = channelGroupEditedPacket;
				break;
			}
			case 338u:
			{
				ChannelGroupMovedPacket channelGroupMovedPacket = new ChannelGroupMovedPacket();
				if (packetCase_ == PacketOneofCase.ChannelGroupMoved)
				{
					channelGroupMovedPacket.MergeFrom(ChannelGroupMoved);
				}
				P_0.ReadMessage(channelGroupMovedPacket);
				ChannelGroupMoved = channelGroupMovedPacket;
				break;
			}
			case 346u:
			{
				ChannelGroupDeletedPacket channelGroupDeletedPacket = new ChannelGroupDeletedPacket();
				if (packetCase_ == PacketOneofCase.ChannelGroupDeleted)
				{
					channelGroupDeletedPacket.MergeFrom(ChannelGroupDeleted);
				}
				P_0.ReadMessage(channelGroupDeletedPacket);
				ChannelGroupDeleted = channelGroupDeletedPacket;
				break;
			}
			case 402u:
			{
				CommunityPacket communityPacket = new CommunityPacket();
				if (packetCase_ == PacketOneofCase.Community)
				{
					communityPacket.MergeFrom(Community);
				}
				P_0.ReadMessage(communityPacket);
				Community = communityPacket;
				break;
			}
			case 410u:
			{
				CommunityJoinedPacket communityJoinedPacket = new CommunityJoinedPacket();
				if (packetCase_ == PacketOneofCase.CommunityJoined)
				{
					communityJoinedPacket.MergeFrom(CommunityJoined);
				}
				P_0.ReadMessage(communityJoinedPacket);
				CommunityJoined = communityJoinedPacket;
				break;
			}
			case 418u:
			{
				CommunityLeavePacket communityLeavePacket = new CommunityLeavePacket();
				if (packetCase_ == PacketOneofCase.CommunityLeave)
				{
					communityLeavePacket.MergeFrom(CommunityLeave);
				}
				P_0.ReadMessage(communityLeavePacket);
				CommunityLeave = communityLeavePacket;
				break;
			}
			case 426u:
			{
				CommunityDeletedPacket communityDeletedPacket = new CommunityDeletedPacket();
				if (packetCase_ == PacketOneofCase.CommunityDeleted)
				{
					communityDeletedPacket.MergeFrom(CommunityDeleted);
				}
				P_0.ReadMessage(communityDeletedPacket);
				CommunityDeleted = communityDeletedPacket;
				break;
			}
			case 482u:
			{
				CommunityMemberAttachPacket communityMemberAttachPacket = new CommunityMemberAttachPacket();
				if (packetCase_ == PacketOneofCase.CommunityMemberAttach)
				{
					communityMemberAttachPacket.MergeFrom(CommunityMemberAttach);
				}
				P_0.ReadMessage(communityMemberAttachPacket);
				CommunityMemberAttach = communityMemberAttachPacket;
				break;
			}
			case 490u:
			{
				CommunityMemberDetachPacket communityMemberDetachPacket = new CommunityMemberDetachPacket();
				if (packetCase_ == PacketOneofCase.CommunityMemberDetach)
				{
					communityMemberDetachPacket.MergeFrom(CommunityMemberDetach);
				}
				P_0.ReadMessage(communityMemberDetachPacket);
				CommunityMemberDetach = communityMemberDetachPacket;
				break;
			}
			case 498u:
			{
				CommunityMemberEditedPacket communityMemberEditedPacket = new CommunityMemberEditedPacket();
				if (packetCase_ == PacketOneofCase.CommunityMemberEdited)
				{
					communityMemberEditedPacket.MergeFrom(CommunityMemberEdited);
				}
				P_0.ReadMessage(communityMemberEditedPacket);
				CommunityMemberEdited = communityMemberEditedPacket;
				break;
			}
			case 506u:
			{
				CommunityMemberEditedExternalPacket communityMemberEditedExternalPacket = new CommunityMemberEditedExternalPacket();
				if (packetCase_ == PacketOneofCase.CommunityMemberEditedExternal)
				{
					communityMemberEditedExternalPacket.MergeFrom(CommunityMemberEditedExternal);
				}
				P_0.ReadMessage(communityMemberEditedExternalPacket);
				CommunityMemberEditedExternal = communityMemberEditedExternalPacket;
				break;
			}
			case 562u:
			{
				CommunityMemberBanCreatedPacket communityMemberBanCreatedPacket = new CommunityMemberBanCreatedPacket();
				if (packetCase_ == PacketOneofCase.CommunityMemberBanCreated)
				{
					communityMemberBanCreatedPacket.MergeFrom(CommunityMemberBanCreated);
				}
				P_0.ReadMessage(communityMemberBanCreatedPacket);
				CommunityMemberBanCreated = communityMemberBanCreatedPacket;
				break;
			}
			case 570u:
			{
				CommunityMemberBanDeletedPacket communityMemberBanDeletedPacket = new CommunityMemberBanDeletedPacket();
				if (packetCase_ == PacketOneofCase.CommunityMemberBanDeleted)
				{
					communityMemberBanDeletedPacket.MergeFrom(CommunityMemberBanDeleted);
				}
				P_0.ReadMessage(communityMemberBanDeletedPacket);
				CommunityMemberBanDeleted = communityMemberBanDeletedPacket;
				break;
			}
			case 642u:
			{
				CommunityMemberRoleCreatedPacket communityMemberRoleCreatedPacket = new CommunityMemberRoleCreatedPacket();
				if (packetCase_ == PacketOneofCase.CommunityMemberRoleCreated)
				{
					communityMemberRoleCreatedPacket.MergeFrom(CommunityMemberRoleCreated);
				}
				P_0.ReadMessage(communityMemberRoleCreatedPacket);
				CommunityMemberRoleCreated = communityMemberRoleCreatedPacket;
				break;
			}
			case 650u:
			{
				CommunityMemberRoleSetPrimaryPacket communityMemberRoleSetPrimaryPacket = new CommunityMemberRoleSetPrimaryPacket();
				if (packetCase_ == PacketOneofCase.CommunityMemberRoleSetPrimary)
				{
					communityMemberRoleSetPrimaryPacket.MergeFrom(CommunityMemberRoleSetPrimary);
				}
				P_0.ReadMessage(communityMemberRoleSetPrimaryPacket);
				CommunityMemberRoleSetPrimary = communityMemberRoleSetPrimaryPacket;
				break;
			}
			case 658u:
			{
				CommunityMemberRoleDeletedPacket communityMemberRoleDeletedPacket = new CommunityMemberRoleDeletedPacket();
				if (packetCase_ == PacketOneofCase.CommunityMemberRoleDeleted)
				{
					communityMemberRoleDeletedPacket.MergeFrom(CommunityMemberRoleDeleted);
				}
				P_0.ReadMessage(communityMemberRoleDeletedPacket);
				CommunityMemberRoleDeleted = communityMemberRoleDeletedPacket;
				break;
			}
			case 722u:
			{
				CommunityRolePacket communityRolePacket = new CommunityRolePacket();
				if (packetCase_ == PacketOneofCase.CommunityRole)
				{
					communityRolePacket.MergeFrom(CommunityRole);
				}
				P_0.ReadMessage(communityRolePacket);
				CommunityRole = communityRolePacket;
				break;
			}
			case 730u:
			{
				CommunityRoleDeletedPacket communityRoleDeletedPacket = new CommunityRoleDeletedPacket();
				if (packetCase_ == PacketOneofCase.CommunityRoleDeleted)
				{
					communityRoleDeletedPacket.MergeFrom(CommunityRoleDeleted);
				}
				P_0.ReadMessage(communityRoleDeletedPacket);
				CommunityRoleDeleted = communityRoleDeletedPacket;
				break;
			}
			case 738u:
			{
				CommunityRoleMovedPacket communityRoleMovedPacket = new CommunityRoleMovedPacket();
				if (packetCase_ == PacketOneofCase.CommunityRoleMoved)
				{
					communityRoleMovedPacket.MergeFrom(CommunityRoleMoved);
				}
				P_0.ReadMessage(communityRoleMovedPacket);
				CommunityRoleMoved = communityRoleMovedPacket;
				break;
			}
			case 802u:
			{
				CommunityPermissionUpdatePacket communityPermissionUpdatePacket = new CommunityPermissionUpdatePacket();
				if (packetCase_ == PacketOneofCase.CommunityPermissionUpdate)
				{
					communityPermissionUpdatePacket.MergeFrom(CommunityPermissionUpdate);
				}
				P_0.ReadMessage(communityPermissionUpdatePacket);
				CommunityPermissionUpdate = communityPermissionUpdatePacket;
				break;
			}
			case 882u:
			{
				CommunityAppAddedPacket communityAppAddedPacket = new CommunityAppAddedPacket();
				if (packetCase_ == PacketOneofCase.CommunityAppAdded)
				{
					communityAppAddedPacket.MergeFrom(CommunityAppAdded);
				}
				P_0.ReadMessage(communityAppAddedPacket);
				CommunityAppAdded = communityAppAddedPacket;
				break;
			}
			case 890u:
			{
				CommunityAppRemovedPacket communityAppRemovedPacket = new CommunityAppRemovedPacket();
				if (packetCase_ == PacketOneofCase.CommunityAppRemoved)
				{
					communityAppRemovedPacket.MergeFrom(CommunityAppRemoved);
				}
				P_0.ReadMessage(communityAppRemovedPacket);
				CommunityAppRemoved = communityAppRemovedPacket;
				break;
			}
			case 898u:
			{
				CommunityAppSetStatusPacket communityAppSetStatusPacket = new CommunityAppSetStatusPacket();
				if (packetCase_ == PacketOneofCase.CommunityAppSetStatus)
				{
					communityAppSetStatusPacket.MergeFrom(CommunityAppSetStatus);
				}
				P_0.ReadMessage(communityAppSetStatusPacket);
				CommunityAppSetStatus = communityAppSetStatusPacket;
				break;
			}
			case 906u:
			{
				CommunityAppSetSettingsPacket communityAppSetSettingsPacket = new CommunityAppSetSettingsPacket();
				if (packetCase_ == PacketOneofCase.CommunityAppSetSettings)
				{
					communityAppSetSettingsPacket.MergeFrom(CommunityAppSetSettings);
				}
				P_0.ReadMessage(communityAppSetSettingsPacket);
				CommunityAppSetSettings = communityAppSetSettingsPacket;
				break;
			}
			case 914u:
			{
				CommunityAppVersionUpdateNotificationPacket communityAppVersionUpdateNotificationPacket = new CommunityAppVersionUpdateNotificationPacket();
				if (packetCase_ == PacketOneofCase.CommunityAppVersionUpdateNotification)
				{
					communityAppVersionUpdateNotificationPacket.MergeFrom(CommunityAppVersionUpdateNotification);
				}
				P_0.ReadMessage(communityAppVersionUpdateNotificationPacket);
				CommunityAppVersionUpdateNotification = communityAppVersionUpdateNotificationPacket;
				break;
			}
			case 922u:
			{
				CommunityAppSetButtonPacket communityAppSetButtonPacket = new CommunityAppSetButtonPacket();
				if (packetCase_ == PacketOneofCase.CommunityAppSetButton)
				{
					communityAppSetButtonPacket.MergeFrom(CommunityAppSetButton);
				}
				P_0.ReadMessage(communityAppSetButtonPacket);
				CommunityAppSetButton = communityAppSetButtonPacket;
				break;
			}
			case 962u:
			{
				DirectMessageCreatedPacket directMessageCreatedPacket = new DirectMessageCreatedPacket();
				if (packetCase_ == PacketOneofCase.DirectMessageCreated)
				{
					directMessageCreatedPacket.MergeFrom(DirectMessageCreated);
				}
				P_0.ReadMessage(directMessageCreatedPacket);
				DirectMessageCreated = directMessageCreatedPacket;
				break;
			}
			case 970u:
			{
				DirectMessageMemberAddedPacket directMessageMemberAddedPacket = new DirectMessageMemberAddedPacket();
				if (packetCase_ == PacketOneofCase.DirectMessageMemberAdded)
				{
					directMessageMemberAddedPacket.MergeFrom(DirectMessageMemberAdded);
				}
				P_0.ReadMessage(directMessageMemberAddedPacket);
				DirectMessageMemberAdded = directMessageMemberAddedPacket;
				break;
			}
			case 978u:
			{
				DirectMessageMemberDeletedPacket directMessageMemberDeletedPacket = new DirectMessageMemberDeletedPacket();
				if (packetCase_ == PacketOneofCase.DirectMessageMemberDeleted)
				{
					directMessageMemberDeletedPacket.MergeFrom(DirectMessageMemberDeleted);
				}
				P_0.ReadMessage(directMessageMemberDeletedPacket);
				DirectMessageMemberDeleted = directMessageMemberDeletedPacket;
				break;
			}
			case 986u:
			{
				DirectMessageRingPacket directMessageRingPacket = new DirectMessageRingPacket();
				if (packetCase_ == PacketOneofCase.DirectMessageRing)
				{
					directMessageRingPacket.MergeFrom(DirectMessageRing);
				}
				P_0.ReadMessage(directMessageRingPacket);
				DirectMessageRing = directMessageRingPacket;
				break;
			}
			case 994u:
			{
				DirectMessageRingDeclinedPacket directMessageRingDeclinedPacket = new DirectMessageRingDeclinedPacket();
				if (packetCase_ == PacketOneofCase.DirectMessageRingDeclined)
				{
					directMessageRingDeclinedPacket.MergeFrom(DirectMessageRingDeclined);
				}
				P_0.ReadMessage(directMessageRingDeclinedPacket);
				DirectMessageRingDeclined = directMessageRingDeclinedPacket;
				break;
			}
			case 1002u:
			{
				DirectMessageLastMessageDeletedPacket directMessageLastMessageDeletedPacket = new DirectMessageLastMessageDeletedPacket();
				if (packetCase_ == PacketOneofCase.DirectMessageLastMessageDeleted)
				{
					directMessageLastMessageDeletedPacket.MergeFrom(DirectMessageLastMessageDeleted);
				}
				P_0.ReadMessage(directMessageLastMessageDeletedPacket);
				DirectMessageLastMessageDeleted = directMessageLastMessageDeletedPacket;
				break;
			}
			case 1042u:
			{
				DirectoryPacket directoryPacket = new DirectoryPacket();
				if (packetCase_ == PacketOneofCase.Directory)
				{
					directoryPacket.MergeFrom(Directory);
				}
				P_0.ReadMessage(directoryPacket);
				Directory = directoryPacket;
				break;
			}
			case 1050u:
			{
				DirectoryMovedPacket directoryMovedPacket = new DirectoryMovedPacket();
				if (packetCase_ == PacketOneofCase.DirectoryMoved)
				{
					directoryMovedPacket.MergeFrom(DirectoryMoved);
				}
				P_0.ReadMessage(directoryMovedPacket);
				DirectoryMoved = directoryMovedPacket;
				break;
			}
			case 1058u:
			{
				DirectoryDeletedPacket directoryDeletedPacket = new DirectoryDeletedPacket();
				if (packetCase_ == PacketOneofCase.DirectoryDeleted)
				{
					directoryDeletedPacket.MergeFrom(DirectoryDeleted);
				}
				P_0.ReadMessage(directoryDeletedPacket);
				DirectoryDeleted = directoryDeletedPacket;
				break;
			}
			case 1122u:
			{
				FileCreatedPacket fileCreatedPacket = new FileCreatedPacket();
				if (packetCase_ == PacketOneofCase.FileCreated)
				{
					fileCreatedPacket.MergeFrom(FileCreated);
				}
				P_0.ReadMessage(fileCreatedPacket);
				FileCreated = fileCreatedPacket;
				break;
			}
			case 1130u:
			{
				FileEditedPacket fileEditedPacket = new FileEditedPacket();
				if (packetCase_ == PacketOneofCase.FileEdited)
				{
					fileEditedPacket.MergeFrom(FileEdited);
				}
				P_0.ReadMessage(fileEditedPacket);
				FileEdited = fileEditedPacket;
				break;
			}
			case 1138u:
			{
				FileMovedPacket fileMovedPacket = new FileMovedPacket();
				if (packetCase_ == PacketOneofCase.FileMoved)
				{
					fileMovedPacket.MergeFrom(FileMoved);
				}
				P_0.ReadMessage(fileMovedPacket);
				FileMoved = fileMovedPacket;
				break;
			}
			case 1146u:
			{
				FileDeletedPacket fileDeletedPacket = new FileDeletedPacket();
				if (packetCase_ == PacketOneofCase.FileDeleted)
				{
					fileDeletedPacket.MergeFrom(FileDeleted);
				}
				P_0.ReadMessage(fileDeletedPacket);
				FileDeleted = fileDeletedPacket;
				break;
			}
			case 1202u:
			{
				FriendshipCreatedPacket friendshipCreatedPacket = new FriendshipCreatedPacket();
				if (packetCase_ == PacketOneofCase.FriendshipCreated)
				{
					friendshipCreatedPacket.MergeFrom(FriendshipCreated);
				}
				P_0.ReadMessage(friendshipCreatedPacket);
				FriendshipCreated = friendshipCreatedPacket;
				break;
			}
			case 1210u:
			{
				FriendshipMovedPacket friendshipMovedPacket = new FriendshipMovedPacket();
				if (packetCase_ == PacketOneofCase.FriendshipMoved)
				{
					friendshipMovedPacket.MergeFrom(FriendshipMoved);
				}
				P_0.ReadMessage(friendshipMovedPacket);
				FriendshipMoved = friendshipMovedPacket;
				break;
			}
			case 1218u:
			{
				FriendshipDeletedPacket friendshipDeletedPacket = new FriendshipDeletedPacket();
				if (packetCase_ == PacketOneofCase.FriendshipDeleted)
				{
					friendshipDeletedPacket.MergeFrom(FriendshipDeleted);
				}
				P_0.ReadMessage(friendshipDeletedPacket);
				FriendshipDeleted = friendshipDeletedPacket;
				break;
			}
			case 1282u:
			{
				FriendshipGroupCreatedPacket friendshipGroupCreatedPacket = new FriendshipGroupCreatedPacket();
				if (packetCase_ == PacketOneofCase.FriendshipGroupCreated)
				{
					friendshipGroupCreatedPacket.MergeFrom(FriendshipGroupCreated);
				}
				P_0.ReadMessage(friendshipGroupCreatedPacket);
				FriendshipGroupCreated = friendshipGroupCreatedPacket;
				break;
			}
			case 1290u:
			{
				FriendshipGroupEditedPacket friendshipGroupEditedPacket = new FriendshipGroupEditedPacket();
				if (packetCase_ == PacketOneofCase.FriendshipGroupEdited)
				{
					friendshipGroupEditedPacket.MergeFrom(FriendshipGroupEdited);
				}
				P_0.ReadMessage(friendshipGroupEditedPacket);
				FriendshipGroupEdited = friendshipGroupEditedPacket;
				break;
			}
			case 1298u:
			{
				FriendshipGroupMovedPacket friendshipGroupMovedPacket = new FriendshipGroupMovedPacket();
				if (packetCase_ == PacketOneofCase.FriendshipGroupMoved)
				{
					friendshipGroupMovedPacket.MergeFrom(FriendshipGroupMoved);
				}
				P_0.ReadMessage(friendshipGroupMovedPacket);
				FriendshipGroupMoved = friendshipGroupMovedPacket;
				break;
			}
			case 1306u:
			{
				FriendshipGroupDeletedPacket friendshipGroupDeletedPacket = new FriendshipGroupDeletedPacket();
				if (packetCase_ == PacketOneofCase.FriendshipGroupDeleted)
				{
					friendshipGroupDeletedPacket.MergeFrom(FriendshipGroupDeleted);
				}
				P_0.ReadMessage(friendshipGroupDeletedPacket);
				FriendshipGroupDeleted = friendshipGroupDeletedPacket;
				break;
			}
			case 1362u:
			{
				MessagePacket messagePacket = new MessagePacket();
				if (packetCase_ == PacketOneofCase.Message)
				{
					messagePacket.MergeFrom(Message);
				}
				P_0.ReadMessage(messagePacket);
				Message = messagePacket;
				break;
			}
			case 1370u:
			{
				MessageDeletedPacket messageDeletedPacket = new MessageDeletedPacket();
				if (packetCase_ == PacketOneofCase.MessageDeleted)
				{
					messageDeletedPacket.MergeFrom(MessageDeleted);
				}
				P_0.ReadMessage(messageDeletedPacket);
				MessageDeleted = messageDeletedPacket;
				break;
			}
			case 1378u:
			{
				MessagePinPacket messagePinPacket = new MessagePinPacket();
				if (packetCase_ == PacketOneofCase.MessagePin)
				{
					messagePinPacket.MergeFrom(MessagePin);
				}
				P_0.ReadMessage(messagePinPacket);
				MessagePin = messagePinPacket;
				break;
			}
			case 1386u:
			{
				MessageReactionPacket messageReactionPacket = new MessageReactionPacket();
				if (packetCase_ == PacketOneofCase.MessageReaction)
				{
					messageReactionPacket.MergeFrom(MessageReaction);
				}
				P_0.ReadMessage(messageReactionPacket);
				MessageReaction = messageReactionPacket;
				break;
			}
			case 1394u:
			{
				MessageSetTypingIndicatorPacket messageSetTypingIndicatorPacket = new MessageSetTypingIndicatorPacket();
				if (packetCase_ == PacketOneofCase.MessageSetTypingIndicator)
				{
					messageSetTypingIndicatorPacket.MergeFrom(MessageSetTypingIndicator);
				}
				P_0.ReadMessage(messageSetTypingIndicatorPacket);
				MessageSetTypingIndicator = messageSetTypingIndicatorPacket;
				break;
			}
			case 1402u:
			{
				MessageSetViewTimePacket messageSetViewTimePacket = new MessageSetViewTimePacket();
				if (packetCase_ == PacketOneofCase.MessageSetViewTime)
				{
					messageSetViewTimePacket.MergeFrom(MessageSetViewTime);
				}
				P_0.ReadMessage(messageSetViewTimePacket);
				MessageSetViewTime = messageSetViewTimePacket;
				break;
			}
			case 1442u:
			{
				NotificationPacket notificationPacket = new NotificationPacket();
				if (packetCase_ == PacketOneofCase.Notification)
				{
					notificationPacket.MergeFrom(Notification);
				}
				P_0.ReadMessage(notificationPacket);
				Notification = notificationPacket;
				break;
			}
			case 1450u:
			{
				NotificationViewedPacket notificationViewedPacket = new NotificationViewedPacket();
				if (packetCase_ == PacketOneofCase.NotificationViewed)
				{
					notificationViewedPacket.MergeFrom(NotificationViewed);
				}
				P_0.ReadMessage(notificationViewedPacket);
				NotificationViewed = notificationViewedPacket;
				break;
			}
			case 1458u:
			{
				NotificationViewedAllPacket notificationViewedAllPacket = new NotificationViewedAllPacket();
				if (packetCase_ == PacketOneofCase.NotificationViewedAll)
				{
					notificationViewedAllPacket.MergeFrom(NotificationViewedAll);
				}
				P_0.ReadMessage(notificationViewedAllPacket);
				NotificationViewedAll = notificationViewedAllPacket;
				break;
			}
			case 1466u:
			{
				NotificationDeletedPacket notificationDeletedPacket = new NotificationDeletedPacket();
				if (packetCase_ == PacketOneofCase.NotificationDeleted)
				{
					notificationDeletedPacket.MergeFrom(NotificationDeleted);
				}
				P_0.ReadMessage(notificationDeletedPacket);
				NotificationDeleted = notificationDeletedPacket;
				break;
			}
			case 1474u:
			{
				NotificationDeletedAllPacket notificationDeletedAllPacket = new NotificationDeletedAllPacket();
				if (packetCase_ == PacketOneofCase.NotificationDeletedAll)
				{
					notificationDeletedAllPacket.MergeFrom(NotificationDeletedAll);
				}
				P_0.ReadMessage(notificationDeletedAllPacket);
				NotificationDeletedAll = notificationDeletedAllPacket;
				break;
			}
			case 1522u:
			{
				UserSetProfilePacket userSetProfilePacket = new UserSetProfilePacket();
				if (packetCase_ == PacketOneofCase.UserSetProfile)
				{
					userSetProfilePacket.MergeFrom(UserSetProfile);
				}
				P_0.ReadMessage(userSetProfilePacket);
				UserSetProfile = userSetProfilePacket;
				break;
			}
			case 1530u:
			{
				UserSetStatusPacket userSetStatusPacket = new UserSetStatusPacket();
				if (packetCase_ == PacketOneofCase.UserSetStatus)
				{
					userSetStatusPacket.MergeFrom(UserSetStatus);
				}
				P_0.ReadMessage(userSetStatusPacket);
				UserSetStatus = userSetStatusPacket;
				break;
			}
			case 1538u:
			{
				UserSetEmailVerificationPacket userSetEmailVerificationPacket = new UserSetEmailVerificationPacket();
				if (packetCase_ == PacketOneofCase.UserSetEmailVerification)
				{
					userSetEmailVerificationPacket.MergeFrom(UserSetEmailVerification);
				}
				P_0.ReadMessage(userSetEmailVerificationPacket);
				UserSetEmailVerification = userSetEmailVerificationPacket;
				break;
			}
			case 1546u:
			{
				UserSetMaxStatusPacket userSetMaxStatusPacket = new UserSetMaxStatusPacket();
				if (packetCase_ == PacketOneofCase.UserSetMaxStatus)
				{
					userSetMaxStatusPacket.MergeFrom(UserSetMaxStatus);
				}
				P_0.ReadMessage(userSetMaxStatusPacket);
				UserSetMaxStatus = userSetMaxStatusPacket;
				break;
			}
			case 1554u:
			{
				UserDeletedPacket userDeletedPacket = new UserDeletedPacket();
				if (packetCase_ == PacketOneofCase.UserDeleted)
				{
					userDeletedPacket.MergeFrom(UserDeleted);
				}
				P_0.ReadMessage(userDeletedPacket);
				UserDeleted = userDeletedPacket;
				break;
			}
			case 1562u:
			{
				UserSetBadgesPacket userSetBadgesPacket = new UserSetBadgesPacket();
				if (packetCase_ == PacketOneofCase.UserSetBadges)
				{
					userSetBadgesPacket.MergeFrom(UserSetBadges);
				}
				P_0.ReadMessage(userSetBadgesPacket);
				UserSetBadges = userSetBadgesPacket;
				break;
			}
			case 1570u:
			{
				UserSetDirectMessageInviteRequirementPacket userSetDirectMessageInviteRequirementPacket = new UserSetDirectMessageInviteRequirementPacket();
				if (packetCase_ == PacketOneofCase.UserSetDirectMessageInviteRequirement)
				{
					userSetDirectMessageInviteRequirementPacket.MergeFrom(UserSetDirectMessageInviteRequirement);
				}
				P_0.ReadMessage(userSetDirectMessageInviteRequirementPacket);
				UserSetDirectMessageInviteRequirement = userSetDirectMessageInviteRequirementPacket;
				break;
			}
			case 1578u:
			{
				UserSetFriendshipInviteRequirementPacket userSetFriendshipInviteRequirementPacket = new UserSetFriendshipInviteRequirementPacket();
				if (packetCase_ == PacketOneofCase.UserSetFriendshipInviteRequirement)
				{
					userSetFriendshipInviteRequirementPacket.MergeFrom(UserSetFriendshipInviteRequirement);
				}
				P_0.ReadMessage(userSetFriendshipInviteRequirementPacket);
				UserSetFriendshipInviteRequirement = userSetFriendshipInviteRequirementPacket;
				break;
			}
			case 1602u:
			{
				WebRtcUserDevicePacket webRtcUserDevicePacket = new WebRtcUserDevicePacket();
				if (packetCase_ == PacketOneofCase.WebRtcUserDevice)
				{
					webRtcUserDevicePacket.MergeFrom(WebRtcUserDevice);
				}
				P_0.ReadMessage(webRtcUserDevicePacket);
				WebRtcUserDevice = webRtcUserDevicePacket;
				break;
			}
			case 1610u:
			{
				WebRtcUserDetachPacket webRtcUserDetachPacket = new WebRtcUserDetachPacket();
				if (packetCase_ == PacketOneofCase.WebRtcUserDetach)
				{
					webRtcUserDetachPacket.MergeFrom(WebRtcUserDetach);
				}
				P_0.ReadMessage(webRtcUserDetachPacket);
				WebRtcUserDetach = webRtcUserDetachPacket;
				break;
			}
			case 1618u:
			{
				WebRtcUserDeviceSetStatusPacket webRtcUserDeviceSetStatusPacket = new WebRtcUserDeviceSetStatusPacket();
				if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetStatus)
				{
					webRtcUserDeviceSetStatusPacket.MergeFrom(WebRtcUserDeviceSetStatus);
				}
				P_0.ReadMessage(webRtcUserDeviceSetStatusPacket);
				WebRtcUserDeviceSetStatus = webRtcUserDeviceSetStatusPacket;
				break;
			}
			case 1626u:
			{
				WebRtcUserDeviceSetTransportPacket webRtcUserDeviceSetTransportPacket = new WebRtcUserDeviceSetTransportPacket();
				if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetTransport)
				{
					webRtcUserDeviceSetTransportPacket.MergeFrom(WebRtcUserDeviceSetTransport);
				}
				P_0.ReadMessage(webRtcUserDeviceSetTransportPacket);
				WebRtcUserDeviceSetTransport = webRtcUserDeviceSetTransportPacket;
				break;
			}
			case 1634u:
			{
				WebRtcUserDeviceSetDataChannelPacket webRtcUserDeviceSetDataChannelPacket = new WebRtcUserDeviceSetDataChannelPacket();
				if (packetCase_ == PacketOneofCase.WebRtcUserDeviceSetDataChannel)
				{
					webRtcUserDeviceSetDataChannelPacket.MergeFrom(WebRtcUserDeviceSetDataChannel);
				}
				P_0.ReadMessage(webRtcUserDeviceSetDataChannelPacket);
				WebRtcUserDeviceSetDataChannel = webRtcUserDeviceSetDataChannelPacket;
				break;
			}
			case 1682u:
			{
				UserBlockCreatedPacket userBlockCreatedPacket = new UserBlockCreatedPacket();
				if (packetCase_ == PacketOneofCase.UserBlockCreated)
				{
					userBlockCreatedPacket.MergeFrom(UserBlockCreated);
				}
				P_0.ReadMessage(userBlockCreatedPacket);
				UserBlockCreated = userBlockCreatedPacket;
				break;
			}
			case 1690u:
			{
				UserBlockDeletedPacket userBlockDeletedPacket = new UserBlockDeletedPacket();
				if (packetCase_ == PacketOneofCase.UserBlockDeleted)
				{
					userBlockDeletedPacket.MergeFrom(UserBlockDeleted);
				}
				P_0.ReadMessage(userBlockDeletedPacket);
				UserBlockDeleted = userBlockDeletedPacket;
				break;
			}
			case 1762u:
			{
				AssetChangedPacket assetChangedPacket = new AssetChangedPacket();
				if (packetCase_ == PacketOneofCase.AssetChanged)
				{
					assetChangedPacket.MergeFrom(AssetChanged);
				}
				P_0.ReadMessage(assetChangedPacket);
				AssetChanged = assetChangedPacket;
				break;
			}
			}
		}
	}
}
