using Google.Protobuf.Reflection;

namespace RootApp.Core.Enums;

public enum RootGuidType
{
	[OriginalName("ROOT_GUID_TYPE_UNKNOWN")]
	Unknown = 0,
	[OriginalName("ROOT_GUID_TYPE_PERSON")]
	Person = 1,
	[OriginalName("ROOT_GUID_TYPE_COMMUNITY")]
	Community = 2,
	[OriginalName("ROOT_GUID_TYPE_MESSAGE")]
	Message = 3,
	[OriginalName("ROOT_GUID_TYPE_CHANNEL")]
	Channel = 4,
	[OriginalName("ROOT_GUID_TYPE_CHANNEL_GROUP")]
	ChannelGroup = 5,
	[OriginalName("ROOT_GUID_TYPE_FRIENDSHIP")]
	Friendship = 6,
	[OriginalName("ROOT_GUID_TYPE_FRIENDSHIP_GROUP")]
	FriendshipGroup = 7,
	[OriginalName("ROOT_GUID_TYPE_FRIENDSHIP_INVITE")]
	FriendshipInvite = 8,
	[OriginalName("ROOT_GUID_TYPE_APP")]
	App = 9,
	[OriginalName("ROOT_GUID_TYPE_COMMUNITY_MEMBER_INVITE")]
	CommunityMemberInvite = 10,
	[OriginalName("ROOT_GUID_TYPE_COMMUNITY_ROLE")]
	CommunityRole = 11,
	[OriginalName("ROOT_GUID_TYPE_FILE")]
	File = 12,
	[OriginalName("ROOT_GUID_TYPE_DIRECTORY")]
	Directory = 13,
	[OriginalName("ROOT_GUID_TYPE_MESSAGE_ATTACHMENT")]
	MessageAttachment = 14,
	[OriginalName("ROOT_GUID_TYPE_DIRECT_MESSAGE")]
	DirectMessage = 15,
	[OriginalName("ROOT_GUID_TYPE_DIRECT_MESSAGE_MEMBER")]
	DirectMessageMember = 16,
	[OriginalName("ROOT_GUID_TYPE_NOTIFICATION")]
	Notification = 17,
	[OriginalName("ROOT_GUID_TYPE_DESKTOP")]
	Desktop = 18,
	[OriginalName("ROOT_GUID_TYPE_MOBILE")]
	Mobile = 19,
	[OriginalName("ROOT_GUID_TYPE_EXCEPTION")]
	Exception = 20,
	[OriginalName("ROOT_GUID_TYPE_COMMUNITY_MEMBER_ROLE")]
	CommunityMemberRole = 21,
	[OriginalName("ROOT_GUID_TYPE_THREADED_MESSAGE")]
	ThreadedMessage = 22,
	[OriginalName("ROOT_GUID_TYPE_THREADED_MESSAGE_MESSAGE")]
	ThreadedMessageMessage = 23,
	[OriginalName("ROOT_GUID_TYPE_COMMUNITY_MEMBER_BAN")]
	CommunityMemberBan = 24,
	[OriginalName("ROOT_GUID_TYPE_COMMUNITY_ROLE_MAP")]
	CommunityRoleMap = 25,
	[OriginalName("ROOT_GUID_TYPE_COMMUNITY_LOG")]
	CommunityLog = 26,
	[OriginalName("ROOT_GUID_TYPE_ASSET")]
	Asset = 27,
	[OriginalName("ROOT_GUID_TYPE_APP_ORGANIZATION")]
	AppOrganization = 28,
	[OriginalName("ROOT_GUID_TYPE_COMMUNITY_APP")]
	CommunityApp = 29,
	[OriginalName("ROOT_GUID_TYPE_COMMAND_IDEMPOTENCY")]
	CommandIdempotency = 30,
	[OriginalName("ROOT_GUID_TYPE_COMMUNITY_MEMBER")]
	CommunityMember = 31,
	[OriginalName("ROOT_GUID_TYPE_APP_VERSION")]
	AppVersion = 32,
	[OriginalName("ROOT_GUID_TYPE_WEB")]
	Web = 33,
	[OriginalName("ROOT_GUID_TYPE_BADGE")]
	Badge = 34,
	[OriginalName("ROOT_GUID_TYPE_CUSTOM_RESOURCE")]
	CustomResource = 224,
	[OriginalName("ROOT_GUID_TYPE_CUSTOM_MEMBER_GROUP")]
	CustomMemberGroup = 225,
	[OriginalName("ROOT_GUID_TYPE_APP_DEPLOYMENT")]
	AppDeployment = 249,
	[OriginalName("ROOT_GUID_TYPE_MESSAGE_BUS_SERVER")]
	MessageBusServer = 250,
	[OriginalName("ROOT_GUID_TYPE_WEB_API_SERVER")]
	WebApiServer = 251,
	[OriginalName("ROOT_GUID_TYPE_HUB_SERVER")]
	HubServer = 252,
	[OriginalName("ROOT_GUID_TYPE_APP_HOST_SERVER")]
	AppHostServer = 253,
	[OriginalName("ROOT_GUID_TYPE_APP_HUB_SERVER")]
	AppHubServer = 254,
	[OriginalName("ROOT_GUID_TYPE_MAX")]
	Max = 255
}
