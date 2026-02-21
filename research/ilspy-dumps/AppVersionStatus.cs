using Google.Protobuf.Reflection;

namespace RootApp.App;

public enum AppVersionStatus
{
	[OriginalName("APP_VERSION_STATUS_UNSPECIFIED")]
	Unspecified = 0,
	[OriginalName("APP_VERSION_STATUS_UNRELEASED")]
	Unreleased = 1,
	[OriginalName("APP_VERSION_STATUS_UNDER_REVIEW")]
	UnderReview = 4,
	[OriginalName("APP_VERSION_STATUS_APPROVED")]
	Approved = 5,
	[OriginalName("APP_VERSION_STATUS_RELEASED")]
	Released = 6,
	[OriginalName("APP_VERSION_STATUS_DEPRECATED")]
	Deprecated = 7,
	[OriginalName("APP_VERSION_STATUS_REJECTED")]
	Rejected = 8
}
