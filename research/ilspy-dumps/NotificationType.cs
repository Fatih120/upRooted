using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum NotificationType
{
	[OriginalName("NOTIFICATION_TYPE_UNSPECIFIED")]
	Unspecified,
	[OriginalName("NOTIFICATION_TYPE_FRIENDSHIP_INVITE_CREATED")]
	FriendshipInviteCreated,
	[OriginalName("NOTIFICATION_TYPE_FRIENDSHIP_INVITE_RESPONDED")]
	FriendshipInviteResponded,
	[OriginalName("NOTIFICATION_TYPE_MESSAGE_MENTIONED")]
	MessageMentioned,
	[OriginalName("NOTIFICATION_TYPE_COMMUNITY_MEMBER_INVITED")]
	CommunityMemberInvited,
	[OriginalName("NOTIFICATION_TYPE_COMMUNITY_MEMBER_KICKED")]
	CommunityMemberKicked,
	[OriginalName("NOTIFICATION_TYPE_COMMUNITY_MEMBER_BANNED")]
	CommunityMemberBanned,
	[OriginalName("NOTIFICATION_TYPE_THREADED_MESSAGE_MENTIONED")]
	ThreadedMessageMentioned,
	[OriginalName("NOTIFICATION_TYPE_THREADED_MESSAGE_MESSAGE_MENTION")]
	ThreadedMessageMessageMention
}
