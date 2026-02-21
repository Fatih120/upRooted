using Google.Protobuf.Reflection;

namespace RootApp.App;

public enum AppStatus
{
	[OriginalName("APP_STATUS_UNSPECIFIED")]
	Unspecified = 0,
	[OriginalName("APP_STATUS_UNPUBLISHED")]
	Unpublished = 1,
	[OriginalName("APP_STATUS_PUBLISHED")]
	Published = 3,
	[OriginalName("APP_STATUS_BLOCKED")]
	Blocked = 4
}
