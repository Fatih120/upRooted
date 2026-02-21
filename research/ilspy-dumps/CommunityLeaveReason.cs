using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum CommunityLeaveReason
{
	[OriginalName("COMMUNITY_LEAVE_REASON_UNSPECIFIED")]
	Unspecified = 0,
	[OriginalName("COMMUNITY_LEAVE_REASON_USER")]
	User = 1,
	[OriginalName("COMMUNITY_LEAVE_REASON_KICKED")]
	Kicked = 4,
	[OriginalName("COMMUNITY_LEAVE_REASON_BANNED")]
	Banned = 7
}
