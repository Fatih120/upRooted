using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum CommunityAppLogType
{
	[OriginalName("COMMUNITY_APP_LOG_TYPE_UNSPECIFIED")]
	Unspecified,
	[OriginalName("COMMUNITY_APP_LOG_TYPE_INFO")]
	Info,
	[OriginalName("COMMUNITY_APP_LOG_TYPE_WARN")]
	Warn,
	[OriginalName("COMMUNITY_APP_LOG_TYPE_ERROR")]
	Error,
	[OriginalName("COMMUNITY_APP_LOG_TYPE_FATAL")]
	Fatal
}
