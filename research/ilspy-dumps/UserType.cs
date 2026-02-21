using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum UserType
{
	[OriginalName("USER_TYPE_ANONYMOUS")]
	Anonymous,
	[OriginalName("USER_TYPE_BASIC")]
	Basic
}
