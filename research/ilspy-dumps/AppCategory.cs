using Google.Protobuf.Reflection;

namespace RootApp.App;

public enum AppCategory
{
	[OriginalName("APP_CATEGORY_UNSPECIFIED")]
	Unspecified,
	[OriginalName("APP_CATEGORY_SOCIAL")]
	Social,
	[OriginalName("APP_CATEGORY_GAMING")]
	Gaming
}
