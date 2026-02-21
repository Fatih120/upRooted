using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum UserFlagProperty
{
	[OriginalName("USER_FLAG_PROPERTY_UNSPECIFIED")]
	Unspecified,
	[OriginalName("USER_FLAG_PROPERTY_USERNAME")]
	Username,
	[OriginalName("USER_FLAG_PROPERTY_IMAGE")]
	Image,
	[OriginalName("USER_FLAG_PROPERTY_OTHER")]
	Other
}
