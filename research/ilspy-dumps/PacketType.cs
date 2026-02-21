using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum PacketType
{
	[OriginalName("PACKET_TYPE_UNSPECIFIED")]
	Unspecified = 0,
	[OriginalName("PACKET_TYPE_PING")]
	Ping = 1,
	[OriginalName("PACKET_TYPE_HUB_SERVER_MOVE")]
	HubServerMove = 2,
	[OriginalName("PACKET_TYPE_USER_BOUNDARY_MIN_VALUE")]
	UserBoundaryMinValue = 100,
	[OriginalName("PACKET_TYPE_USER_BOUNDARY_MAX_VALUE")]
	UserBoundaryMaxValue = 4999,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_BOUNDARY_MIN_VALUE")]
	DirectMessageBoundaryMinValue = 200,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_BOUNDARY_MAX_VALUE")]
	DirectMessageBoundaryMaxValue = 299,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_CREATED")]
	DirectMessageCreated = 201,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_MEMBER_ADDED")]
	DirectMessageMemberAdded = 202,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_MEMBER_DELETED")]
	DirectMessageMemberDeleted = 203,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_MESSAGE_REACTION_CREATED")]
	DirectMessageMessageReactionCreated = 204,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_MESSAGE_REACTION_DELETED")]
	DirectMessageMessageReactionDeleted = 205,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_MESSAGE_PIN_CREATED")]
	DirectMessageMessagePinCreated = 206,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_MESSAGE_PIN_DELETED")]
	DirectMessageMessagePinDeleted = 207,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_MESSAGE_CREATED")]
	DirectMessageMessageCreated = 208,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_MESSAGE_EDITED")]
	DirectMessageMessageEdited = 209,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_MESSAGE_DELETED")]
	DirectMessageMessageDeleted = 210,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_MESSAGE_SET_TYPING_INDICATOR")]
	DirectMessageMessageSetTypingIndicator = 211,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_MESSAGE_SET_VIEW_TIME")]
	DirectMessageMessageSetViewTime = 212,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_RING")]
	DirectMessageRing = 213,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_RING_DECLINED")]
	DirectMessageRingDeclined = 214,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_LAST_MESSAGE_DELETED")]
	DirectMessageLastMessageDeleted = 215,
	[OriginalName("PACKET_TYPE_NOTIFICATION_BOUNDARY_MIN_VALUE")]
	NotificationBoundaryMinValue = 300,
	[OriginalName("PACKET_TYPE_NOTIFICATION_BOUNDARY_MAX_VALUE")]
	NotificationBoundaryMaxValue = 399,
	[OriginalName("PACKET_TYPE_NOTIFICATION_VIEWED")]
	NotificationViewed = 301,
	[OriginalName("PACKET_TYPE_NOTIFICATION_VIEWED_ALL")]
	NotificationViewedAll = 302,
	[OriginalName("PACKET_TYPE_NOTIFICATION_DELETED")]
	NotificationDeleted = 303,
	[OriginalName("PACKET_TYPE_NOTIFICATION_DELETED_ALL")]
	NotificationDeletedAll = 304,
	[OriginalName("PACKET_TYPE_NOTIFICATION_CREATED")]
	NotificationCreated = 305,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_WEB_RTC_BOUNDARY_MIN_VALUE")]
	DirectMessageWebRtcBoundaryMinValue = 400,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_WEB_RTC_BOUNDARY_MAX_VALUE")]
	DirectMessageWebRtcBoundaryMaxValue = 499,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_WEB_RTC_USER_ATTACH")]
	DirectMessageWebRtcUserAttach = 401,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_WEB_RTC_USER_DETACH")]
	DirectMessageWebRtcUserDetach = 402,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_WEB_RTC_USER_DEVICE_SET_STATUS")]
	DirectMessageWebRtcUserDeviceSetStatus = 404,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_WEB_RTC_USER_DEVICE_SET_TRANSPORT")]
	DirectMessageWebRtcUserDeviceSetTransport = 405,
	[OriginalName("PACKET_TYPE_DIRECT_MESSAGE_WEB_RTC_USER_DEVICE_SET_DATA_CHANNEL")]
	DirectMessageWebRtcUserDeviceSetDataChannel = 406,
	[OriginalName("PACKET_TYPE_FRIENDSHIP_BOUNDARY_MIN_VALUE")]
	FriendshipBoundaryMinValue = 500,
	[OriginalName("PACKET_TYPE_FRIENDSHIP_BOUNDARY_MAX_VALUE")]
	FriendshipBoundaryMaxValue = 599,
	[OriginalName("PACKET_TYPE_FRIENDSHIP_CREATED")]
	FriendshipCreated = 501,
	[OriginalName("PACKET_TYPE_FRIENDSHIP_MOVED")]
	FriendshipMoved = 502,
	[OriginalName("PACKET_TYPE_FRIENDSHIP_DELETED")]
	FriendshipDeleted = 503,
	[OriginalName("PACKET_TYPE_FRIENDSHIP_GROUP_BOUNDARY_MIN_VALUE")]
	FriendshipGroupBoundaryMinValue = 600,
	[OriginalName("PACKET_TYPE_FRIENDSHIP_GROUP_BOUNDARY_MAX_VALUE")]
	FriendshipGroupBoundaryMaxValue = 699,
	[OriginalName("PACKET_TYPE_FRIENDSHIP_GROUP_CREATED")]
	FriendshipGroupCreated = 601,
	[OriginalName("PACKET_TYPE_FRIENDSHIP_GROUP_EDITED")]
	FriendshipGroupEdited = 602,
	[OriginalName("PACKET_TYPE_FRIENDSHIP_GROUP_DELETED")]
	FriendshipGroupDeleted = 603,
	[OriginalName("PACKET_TYPE_FRIENDSHIP_GROUP_MOVED")]
	FriendshipGroupMoved = 604,
	[OriginalName("PACKET_TYPE_USER_STATUS_BOUNDARY_MIN_VALUE")]
	UserStatusBoundaryMinValue = 700,
	[OriginalName("PACKET_TYPE_USER_STATUS_BOUNDARY_MAX_VALUE")]
	UserStatusBoundaryMaxValue = 799,
	[OriginalName("PACKET_TYPE_USER_SET_STATUS")]
	UserSetStatus = 701,
	[OriginalName("PACKET_TYPE_USER_SET_PROFILE")]
	UserSetProfile = 702,
	[OriginalName("PACKET_TYPE_USER_SET_EMAIL_VERIFICATION")]
	UserSetEmailVerification = 703,
	[OriginalName("PACKET_TYPE_USER_SET_MAX_STATUS")]
	UserSetMaxStatus = 704,
	[OriginalName("PACKET_TYPE_USER_DELETED")]
	UserDeleted = 705,
	[OriginalName("PACKET_TYPE_USER_SET_BADGES")]
	UserSetBadges = 706,
	[OriginalName("PACKET_TYPE_USER_SET_DIRECT_MESSAGE_REQUIREMENT")]
	UserSetDirectMessageRequirement = 707,
	[OriginalName("PACKET_TYPE_USER_SET_FRIENDSHIP_REQUIREMENT")]
	UserSetFriendshipRequirement = 708,
	[OriginalName("PACKET_TYPE_EXTERNAL_COMMUNITY_BOUNDARY_MIN_VALUE")]
	ExternalCommunityBoundaryMinValue = 800,
	[OriginalName("PACKET_TYPE_EXTERNAL_COMMUNITY_BOUNDARY_MAX_VALUE")]
	ExternalCommunityBoundaryMaxValue = 899,
	[OriginalName("PACKET_TYPE_COMMUNITY_CREATED_EXTERNAL")]
	CommunityCreatedExternal = 801,
	[OriginalName("PACKET_TYPE_COMMUNITY_EDITED_EXTERNAL")]
	CommunityEditedExternal = 802,
	[OriginalName("PACKET_TYPE_COMMUNITY_DELETED_EXTERNAL")]
	CommunityDeletedExternal = 803,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_EDITED_EXTERNAL")]
	CommunityMemberEditedExternal = 851,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_MOVED_EXTERNAL")]
	CommunityMemberMovedExternal = 852,
	[OriginalName("PACKET_TYPE_USER_BLOCK_BOUNDARY_MIN_VALUE")]
	UserBlockBoundaryMinValue = 900,
	[OriginalName("PACKET_TYPE_USER_BLOCK_BOUNDARY_MAX_VALUE")]
	UserBlockBoundaryMaxValue = 999,
	[OriginalName("PACKET_TYPE_USER_BLOCK_CREATED")]
	UserBlockCreated = 901,
	[OriginalName("PACKET_TYPE_USER_BLOCK_DELETED")]
	UserBlockDeleted = 902,
	[OriginalName("PACKET_TYPE_COMMUNITY_BOUNDARY_MIN_VALUE")]
	CommunityBoundaryMinValue = 5000,
	[OriginalName("PACKET_TYPE_COMMUNITY_BOUNDARY_MAX_VALUE")]
	CommunityBoundaryMaxValue = 9999,
	[OriginalName("PACKET_TYPE_CHANNEL_BOUNDARY_MIN_VALUE")]
	ChannelBoundaryMinValue = 5100,
	[OriginalName("PACKET_TYPE_CHANNEL_BOUNDARY_MAX_VALUE")]
	ChannelBoundaryMaxValue = 5199,
	[OriginalName("PACKET_TYPE_CHANNEL_CREATED")]
	ChannelCreated = 5101,
	[OriginalName("PACKET_TYPE_CHANNEL_EDITED")]
	ChannelEdited = 5102,
	[OriginalName("PACKET_TYPE_CHANNEL_DELETED")]
	ChannelDeleted = 5103,
	[OriginalName("PACKET_TYPE_CHANNEL_MOVED")]
	ChannelMoved = 5104,
	[OriginalName("PACKET_TYPE_CHANNEL_GROUP_BOUNDARY_MIN_VALUE")]
	ChannelGroupBoundaryMinValue = 5200,
	[OriginalName("PACKET_TYPE_CHANNEL_GROUP_BOUNDARY_MAX_VALUE")]
	ChannelGroupBoundaryMaxValue = 5299,
	[OriginalName("PACKET_TYPE_CHANNEL_GROUP_CREATED")]
	ChannelGroupCreated = 5201,
	[OriginalName("PACKET_TYPE_CHANNEL_GROUP_EDITED")]
	ChannelGroupEdited = 5202,
	[OriginalName("PACKET_TYPE_CHANNEL_GROUP_DELETED")]
	ChannelGroupDeleted = 5203,
	[OriginalName("PACKET_TYPE_CHANNEL_GROUP_MOVED")]
	ChannelGroupMoved = 5204,
	[OriginalName("PACKET_TYPE_COMMUNITY_INFO_BOUNDARY_MIN_VALUE")]
	CommunityInfoBoundaryMinValue = 5300,
	[OriginalName("PACKET_TYPE_COMMUNITY_INFO_BOUNDARY_MAX_VALUE")]
	CommunityInfoBoundaryMaxValue = 5399,
	[OriginalName("PACKET_TYPE_COMMUNITY_JOINED")]
	CommunityJoined = 5302,
	[OriginalName("PACKET_TYPE_COMMUNITY_LEAVE")]
	CommunityLeave = 5303,
	[OriginalName("PACKET_TYPE_COMMUNITY_ROLE_BOUNDARY_MIN_VALUE")]
	CommunityRoleBoundaryMinValue = 5400,
	[OriginalName("PACKET_TYPE_COMMUNITY_ROLE_BOUNDARY_MAX_VALUE")]
	CommunityRoleBoundaryMaxValue = 5499,
	[OriginalName("PACKET_TYPE_COMMUNITY_ROLE_CREATED")]
	CommunityRoleCreated = 5401,
	[OriginalName("PACKET_TYPE_COMMUNITY_ROLE_EDITED")]
	CommunityRoleEdited = 5402,
	[OriginalName("PACKET_TYPE_COMMUNITY_ROLE_DELETED")]
	CommunityRoleDeleted = 5403,
	[OriginalName("PACKET_TYPE_COMMUNITY_ROLE_MOVED")]
	CommunityRoleMoved = 5404,
	[OriginalName("PACKET_TYPE_COMMUNITY_PERMISSION_UPDATE")]
	CommunityPermissionUpdate = 5405,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_BOUNDARY_MIN_VALUE")]
	CommunityMemberBoundaryMinValue = 5500,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_BOUNDARY_MAX_VALUE")]
	CommunityMemberBoundaryMaxValue = 5599,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_EDITED")]
	CommunityMemberEdited = 5501,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_ATTACH")]
	CommunityMemberAttach = 5502,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_DETACH")]
	CommunityMemberDetach = 5503,
	[OriginalName("PACKET_TYPE_CHANNEL_DIRECTORY_BOUNDARY_MIN_VALUE")]
	ChannelDirectoryBoundaryMinValue = 5600,
	[OriginalName("PACKET_TYPE_CHANNEL_DIRECTORY_BOUNDARY_MAX_VALUE")]
	ChannelDirectoryBoundaryMaxValue = 5699,
	[OriginalName("PACKET_TYPE_CHANNEL_DIRECTORY_CREATED")]
	ChannelDirectoryCreated = 5601,
	[OriginalName("PACKET_TYPE_CHANNEL_DIRECTORY_EDITED")]
	ChannelDirectoryEdited = 5602,
	[OriginalName("PACKET_TYPE_CHANNEL_DIRECTORY_DELETED")]
	ChannelDirectoryDeleted = 5603,
	[OriginalName("PACKET_TYPE_CHANNEL_DIRECTORY_MOVED")]
	ChannelDirectoryMoved = 5604,
	[OriginalName("PACKET_TYPE_CHANNEL_FILE_BOUNDARY_MIN_VALUE")]
	ChannelFileBoundaryMinValue = 5700,
	[OriginalName("PACKET_TYPE_CHANNEL_FILE_BOUNDARY_MAX_VALUE")]
	ChannelFileBoundaryMaxValue = 5799,
	[OriginalName("PACKET_TYPE_CHANNEL_FILE_CREATED")]
	ChannelFileCreated = 5701,
	[OriginalName("PACKET_TYPE_CHANNEL_FILE_EDITED")]
	ChannelFileEdited = 5702,
	[OriginalName("PACKET_TYPE_CHANNEL_FILE_DELETED")]
	ChannelFileDeleted = 5703,
	[OriginalName("PACKET_TYPE_CHANNEL_FILE_MOVED")]
	ChannelFileMoved = 5704,
	[OriginalName("PACKET_TYPE_CHANNEL_MESSAGE_BOUNDARY_MIN_VALUE")]
	ChannelMessageBoundaryMinValue = 5800,
	[OriginalName("PACKET_TYPE_CHANNEL_MESSAGE_BOUNDARY_MAX_VALUE")]
	ChannelMessageBoundaryMaxValue = 5899,
	[OriginalName("PACKET_TYPE_CHANNEL_MESSAGE_REACTION_CREATED")]
	ChannelMessageReactionCreated = 5801,
	[OriginalName("PACKET_TYPE_CHANNEL_MESSAGE_REACTION_DELETED")]
	ChannelMessageReactionDeleted = 5802,
	[OriginalName("PACKET_TYPE_CHANNEL_MESSAGE_PIN_CREATED")]
	ChannelMessagePinCreated = 5803,
	[OriginalName("PACKET_TYPE_CHANNEL_MESSAGE_PIN_DELETED")]
	ChannelMessagePinDeleted = 5804,
	[OriginalName("PACKET_TYPE_CHANNEL_MESSAGE_CREATED")]
	ChannelMessageCreated = 5805,
	[OriginalName("PACKET_TYPE_CHANNEL_MESSAGE_EDITED")]
	ChannelMessageEdited = 5806,
	[OriginalName("PACKET_TYPE_CHANNEL_MESSAGE_DELETED")]
	ChannelMessageDeleted = 5807,
	[OriginalName("PACKET_TYPE_CHANNEL_MESSAGE_SET_TYPING_INDICATOR")]
	ChannelMessageSetTypingIndicator = 5808,
	[OriginalName("PACKET_TYPE_CHANNEL_MESSAGE_SET_VIEW_TIME")]
	ChannelMessageSetViewTime = 5809,
	[OriginalName("PACKET_TYPE_CHANNEL_WEB_RTC_BOUNDARY_MIN_VALUE")]
	ChannelWebRtcBoundaryMinValue = 6000,
	[OriginalName("PACKET_TYPE_CHANNEL_WEB_RTC_BOUNDARY_MAX_VALUE")]
	ChannelWebRtcBoundaryMaxValue = 6099,
	[OriginalName("PACKET_TYPE_CHANNEL_WEB_RTC_USER_ATTACH")]
	ChannelWebRtcUserAttach = 6001,
	[OriginalName("PACKET_TYPE_CHANNEL_WEB_RTC_USER_DETACH")]
	ChannelWebRtcUserDetach = 6002,
	[OriginalName("PACKET_TYPE_CHANNEL_WEB_RTC_USER_DEVICE_SET_STATUS")]
	ChannelWebRtcUserDeviceSetStatus = 6004,
	[OriginalName("PACKET_TYPE_CHANNEL_WEB_RTC_USER_DEVICE_SET_TRANSPORT")]
	ChannelWebRtcUserDeviceSetTransport = 6005,
	[OriginalName("PACKET_TYPE_CHANNEL_WEB_RTC_USER_DEVICE_SET_DATA_CHANNEL")]
	ChannelWebRtcUserDeviceSetDataChannel = 6006,
	[OriginalName("PACKET_TYPE_COMMUNITY_APP_BOUNDARY_MIN_VALUE")]
	CommunityAppBoundaryMinValue = 6100,
	[OriginalName("PACKET_TYPE_COMMUNITY_APP_BOUNDARY_MAX_VALUE")]
	CommunityAppBoundaryMaxValue = 6199,
	[OriginalName("PACKET_TYPE_COMMUNITY_APP_ADDED")]
	CommunityAppAdded = 6101,
	[OriginalName("PACKET_TYPE_COMMUNITY_APP_SET_STATUS")]
	CommunityAppSetStatus = 6102,
	[OriginalName("PACKET_TYPE_COMMUNITY_APP_REMOVED")]
	CommunityAppRemoved = 6103,
	[OriginalName("PACKET_TYPE_COMMUNITY_APP_SET_SETTINGS")]
	CommunityAppSetSettings = 6104,
	[OriginalName("PACKET_TYPE_COMMUNITY_APP_VERSION_UPDATE")]
	CommunityAppVersionUpdate = 6105,
	[OriginalName("PACKET_TYPE_COMMUNITY_APP_SET_BUTTON")]
	CommunityAppSetButton = 6106,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_BAN_BOUNDARY_MIN_VALUE")]
	CommunityMemberBanBoundaryMinValue = 6200,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_BAN_BOUNDARY_MAX_VALUE")]
	CommunityMemberBanBoundaryMaxValue = 6299,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_BAN_CREATED")]
	CommunityMemberBanCreated = 6201,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_BAN_DELETED")]
	CommunityMemberBanDeleted = 6202,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_ROLE_BOUNDARY_MIN_VALUE")]
	CommunityMemberRoleBoundaryMinValue = 6300,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_ROLE_BOUNDARY_MAX_VALUE")]
	CommunityMemberRoleBoundaryMaxValue = 6399,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_ROLE_SET_PRIMARY")]
	CommunityMemberRoleSetPrimary = 6301,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_ROLE_DELETED")]
	CommunityMemberRoleDeleted = 6302,
	[OriginalName("PACKET_TYPE_COMMUNITY_MEMBER_ROLE_CREATED")]
	CommunityMemberRoleCreated = 6303,
	[OriginalName("PACKET_TYPE_ASSET_BOUNDARY_MIN_VALUE")]
	AssetBoundaryMinValue = 10000,
	[OriginalName("PACKET_TYPE_ASSET_BOUNDARY_MAX_VALUE")]
	AssetBoundaryMaxValue = 11000,
	[OriginalName("PACKET_TYPE_ASSET_CHANGED")]
	AssetChanged = 10001
}
