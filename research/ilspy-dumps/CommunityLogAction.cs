using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum CommunityLogAction
{
	[OriginalName("COMMUNITY_LOG_ACTION_UNSPECIFIED")]
	Unspecified,
	[OriginalName("COMMUNITY_LOG_ACTION_CREATED")]
	Created,
	[OriginalName("COMMUNITY_LOG_ACTION_UPDATED")]
	Updated,
	[OriginalName("COMMUNITY_LOG_ACTION_DELETED")]
	Deleted,
	[OriginalName("COMMUNITY_LOG_ACTION_KICKED")]
	Kicked,
	[OriginalName("COMMUNITY_LOG_ACTION_BANNED")]
	Banned,
	[OriginalName("COMMUNITY_LOG_ACTION_JOINED")]
	Joined,
	[OriginalName("COMMUNITY_LOG_ACTION_LEFT")]
	Left,
	[OriginalName("COMMUNITY_LOG_ACTION_REJECTED")]
	Rejected,
	[OriginalName("COMMUNITY_LOG_ACTION_UN_BANNED")]
	UnBanned,
	[OriginalName("COMMUNITY_LOG_ACTION_MESSAGE_PINNED")]
	MessagePinned,
	[OriginalName("COMMUNITY_LOG_ACTION_MESSAGE_UN_PINNED")]
	MessageUnPinned
}
