using Google.Protobuf.Reflection;

namespace RootApp.App;

public enum AppType
{
	[OriginalName("APP_TYPE_UNSPECIFIED")]
	Unspecified,
	[OriginalName("APP_TYPE_BOT")]
	Bot,
	[OriginalName("APP_TYPE_APP")]
	App
}
