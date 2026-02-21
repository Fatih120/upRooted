namespace RootApp.WebApi.Shared.Packets;

public static class PacketContainerExtensions
{
	public static IPacket? AsIPacket(this PacketContainer P_0)
	{
		PacketContainer.PacketOneofCase packetCase = P_0.PacketCase;
		if (1 == 0)
		{
		}
		IPacket result = packetCase switch
		{
			PacketContainer.PacketOneofCase.Ping => P_0.Ping, 
			PacketContainer.PacketOneofCase.HubServerMove => P_0.HubServerMove, 
			PacketContainer.PacketOneofCase.ChannelCreated => P_0.ChannelCreated, 
			PacketContainer.PacketOneofCase.ChannelEdited => P_0.ChannelEdited, 
			PacketContainer.PacketOneofCase.ChannelMoved => P_0.ChannelMoved, 
			PacketContainer.PacketOneofCase.ChannelDeleted => P_0.ChannelDeleted, 
			PacketContainer.PacketOneofCase.ChannelGroupCreated => P_0.ChannelGroupCreated, 
			PacketContainer.PacketOneofCase.ChannelGroupEdited => P_0.ChannelGroupEdited, 
			PacketContainer.PacketOneofCase.ChannelGroupMoved => P_0.ChannelGroupMoved, 
			PacketContainer.PacketOneofCase.ChannelGroupDeleted => P_0.ChannelGroupDeleted, 
			PacketContainer.PacketOneofCase.Community => P_0.Community, 
			PacketContainer.PacketOneofCase.CommunityJoined => P_0.CommunityJoined, 
			PacketContainer.PacketOneofCase.CommunityLeave => P_0.CommunityLeave, 
			PacketContainer.PacketOneofCase.CommunityDeleted => P_0.CommunityDeleted, 
			PacketContainer.PacketOneofCase.CommunityMemberAttach => P_0.CommunityMemberAttach, 
			PacketContainer.PacketOneofCase.CommunityMemberDetach => P_0.CommunityMemberDetach, 
			PacketContainer.PacketOneofCase.CommunityMemberEdited => P_0.CommunityMemberEdited, 
			PacketContainer.PacketOneofCase.CommunityMemberEditedExternal => P_0.CommunityMemberEditedExternal, 
			PacketContainer.PacketOneofCase.CommunityMemberBanCreated => P_0.CommunityMemberBanCreated, 
			PacketContainer.PacketOneofCase.CommunityMemberBanDeleted => P_0.CommunityMemberBanDeleted, 
			PacketContainer.PacketOneofCase.CommunityMemberRoleCreated => P_0.CommunityMemberRoleCreated, 
			PacketContainer.PacketOneofCase.CommunityMemberRoleSetPrimary => P_0.CommunityMemberRoleSetPrimary, 
			PacketContainer.PacketOneofCase.CommunityMemberRoleDeleted => P_0.CommunityMemberRoleDeleted, 
			PacketContainer.PacketOneofCase.CommunityRole => P_0.CommunityRole, 
			PacketContainer.PacketOneofCase.CommunityRoleDeleted => P_0.CommunityRoleDeleted, 
			PacketContainer.PacketOneofCase.CommunityRoleMoved => P_0.CommunityRoleMoved, 
			PacketContainer.PacketOneofCase.CommunityPermissionUpdate => P_0.CommunityPermissionUpdate, 
			PacketContainer.PacketOneofCase.CommunityAppAdded => P_0.CommunityAppAdded, 
			PacketContainer.PacketOneofCase.CommunityAppRemoved => P_0.CommunityAppRemoved, 
			PacketContainer.PacketOneofCase.CommunityAppSetStatus => P_0.CommunityAppSetStatus, 
			PacketContainer.PacketOneofCase.CommunityAppSetSettings => P_0.CommunityAppSetSettings, 
			PacketContainer.PacketOneofCase.CommunityAppVersionUpdateNotification => P_0.CommunityAppVersionUpdateNotification, 
			PacketContainer.PacketOneofCase.DirectMessageCreated => P_0.DirectMessageCreated, 
			PacketContainer.PacketOneofCase.DirectMessageMemberAdded => P_0.DirectMessageMemberAdded, 
			PacketContainer.PacketOneofCase.DirectMessageMemberDeleted => P_0.DirectMessageMemberDeleted, 
			PacketContainer.PacketOneofCase.DirectMessageRing => P_0.DirectMessageRing, 
			PacketContainer.PacketOneofCase.DirectMessageRingDeclined => P_0.DirectMessageRingDeclined, 
			PacketContainer.PacketOneofCase.DirectMessageLastMessageDeleted => P_0.DirectMessageLastMessageDeleted, 
			PacketContainer.PacketOneofCase.Directory => P_0.Directory, 
			PacketContainer.PacketOneofCase.DirectoryMoved => P_0.DirectoryMoved, 
			PacketContainer.PacketOneofCase.DirectoryDeleted => P_0.DirectoryDeleted, 
			PacketContainer.PacketOneofCase.FileCreated => P_0.FileCreated, 
			PacketContainer.PacketOneofCase.FileEdited => P_0.FileEdited, 
			PacketContainer.PacketOneofCase.FileMoved => P_0.FileMoved, 
			PacketContainer.PacketOneofCase.FileDeleted => P_0.FileDeleted, 
			PacketContainer.PacketOneofCase.FriendshipCreated => P_0.FriendshipCreated, 
			PacketContainer.PacketOneofCase.FriendshipMoved => P_0.FriendshipMoved, 
			PacketContainer.PacketOneofCase.FriendshipDeleted => P_0.FriendshipDeleted, 
			PacketContainer.PacketOneofCase.FriendshipGroupCreated => P_0.FriendshipGroupCreated, 
			PacketContainer.PacketOneofCase.FriendshipGroupEdited => P_0.FriendshipGroupEdited, 
			PacketContainer.PacketOneofCase.FriendshipGroupMoved => P_0.FriendshipGroupMoved, 
			PacketContainer.PacketOneofCase.FriendshipGroupDeleted => P_0.FriendshipGroupDeleted, 
			PacketContainer.PacketOneofCase.Message => P_0.Message, 
			PacketContainer.PacketOneofCase.MessageDeleted => P_0.MessageDeleted, 
			PacketContainer.PacketOneofCase.MessagePin => P_0.MessagePin, 
			PacketContainer.PacketOneofCase.MessageReaction => P_0.MessageReaction, 
			PacketContainer.PacketOneofCase.MessageSetTypingIndicator => P_0.MessageSetTypingIndicator, 
			PacketContainer.PacketOneofCase.MessageSetViewTime => P_0.MessageSetViewTime, 
			PacketContainer.PacketOneofCase.Notification => P_0.Notification, 
			PacketContainer.PacketOneofCase.NotificationViewed => P_0.NotificationViewed, 
			PacketContainer.PacketOneofCase.NotificationViewedAll => P_0.NotificationViewedAll, 
			PacketContainer.PacketOneofCase.NotificationDeleted => P_0.NotificationDeleted, 
			PacketContainer.PacketOneofCase.NotificationDeletedAll => P_0.NotificationDeletedAll, 
			PacketContainer.PacketOneofCase.UserSetBadges => P_0.UserSetBadges, 
			PacketContainer.PacketOneofCase.UserSetProfile => P_0.UserSetProfile, 
			PacketContainer.PacketOneofCase.UserSetStatus => P_0.UserSetStatus, 
			PacketContainer.PacketOneofCase.UserDeleted => P_0.UserDeleted, 
			PacketContainer.PacketOneofCase.UserBlockCreated => P_0.UserBlockCreated, 
			PacketContainer.PacketOneofCase.UserBlockDeleted => P_0.UserBlockDeleted, 
			PacketContainer.PacketOneofCase.WebRtcUserDevice => P_0.WebRtcUserDevice, 
			PacketContainer.PacketOneofCase.WebRtcUserDetach => P_0.WebRtcUserDetach, 
			PacketContainer.PacketOneofCase.WebRtcUserDeviceSetStatus => P_0.WebRtcUserDeviceSetStatus, 
			PacketContainer.PacketOneofCase.WebRtcUserDeviceSetTransport => P_0.WebRtcUserDeviceSetTransport, 
			PacketContainer.PacketOneofCase.WebRtcUserDeviceSetDataChannel => P_0.WebRtcUserDeviceSetDataChannel, 
			PacketContainer.PacketOneofCase.AssetChanged => P_0.AssetChanged, 
			_ => null, 
		};
		if (1 == 0)
		{
		}
		return result;
	}
}
