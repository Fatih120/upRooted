using Google.Protobuf.Reflection;

namespace RootApp.App;

public enum DeveloperStatus
{
	[OriginalName("DEVELOPER_STATUS_UNSPECIFIED")]
	Unspecified,
	[OriginalName("DEVELOPER_STATUS_COMPLETE")]
	Complete,
	[OriginalName("DEVELOPER_STATUS_NOT_REGISTERED")]
	NotRegistered,
	[OriginalName("DEVELOPER_STATUS_BLOCKED")]
	Blocked
}
