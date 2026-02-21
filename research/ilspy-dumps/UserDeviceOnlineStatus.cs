using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Enums;

public enum UserDeviceOnlineStatus
{
	[OriginalName("USER_DEVICE_ONLINE_STATUS_UNSPECIFIED")]
	Unspecified = 0,
	[OriginalName("USER_DEVICE_ONLINE_STATUS_DISCONNECTED")]
	Disconnected = 1,
	[OriginalName("USER_DEVICE_ONLINE_STATUS_INACTIVE")]
	Inactive = 4,
	[OriginalName("USER_DEVICE_ONLINE_STATUS_ACTIVE")]
	Active = 0x10
}
