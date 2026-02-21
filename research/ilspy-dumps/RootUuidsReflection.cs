using System;
using Google.Protobuf.Reflection;

namespace RootApp.Core;

public static class RootUuidsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static RootUuidsReflection()
	{
		_003C_003Ey__InlineArray48<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray48<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 0) = "ChVjb3JlL3Jvb3RfdXVpZHMucHJvdG8SBHJvb3QiKQoIUm9vdFV1aWQSDgoG";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 1) = "aGlnaDY0GAEgASgGEg0KBWxvdzY0GAIgASgGIigKB0FwcFV1aWQSDgoGaGln";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 2) = "aDY0GAEgASgGEg0KBWxvdzY0GAIgASgGIi8KDkFwcFZlcnNpb25VdWlkEg4K";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 3) = "BmhpZ2g2NBgBIAEoBhINCgVsb3c2NBgCIAEoBiIyChFBcHBEZXBsb3ltZW50";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 4) = "VXVpZBIOCgZoaWdoNjQYASABKAYSDQoFbG93NjQYAiABKAYiKwoKUGVyc29u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 5) = "VXVpZBIOCgZoaWdoNjQYASABKAYSDQoFbG93NjQYAiABKAYiKgoJQXNzZXRV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 6) = "dWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2NBgCIAEoBiIqCglCYWRnZVV1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 7) = "aWQSDgoGaGlnaDY0GAEgASgGEg0KBWxvdzY0GAIgASgGIjEKEENoYW5uZWxH";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 8) = "cm91cFV1aWQSDgoGaGlnaDY0GAEgASgGEg0KBWxvdzY0GAIgASgGIiwKC0No";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 9) = "YW5uZWxVdWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2NBgCIAEoBiI3ChZD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 10) = "b21tYW5kSWRlbXBvdGVuY3lVdWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 11) = "NBgCIAEoBiIxChBDb21tdW5pdHlBcHBVdWlkEg4KBmhpZ2g2NBgBIAEoBhIN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 12) = "CgVsb3c2NBgCIAEoBiI3ChZDb21tdW5pdHlNZW1iZXJCYW5VdWlkEg4KBmhp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 13) = "Z2g2NBgBIAEoBhINCgVsb3c2NBgCIAEoBiIuCg1Db21tdW5pdHlVdWlkEg4K";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 14) = "BmhpZ2g2NBgBIAEoBhINCgVsb3c2NBgCIAEoBiI6ChlDb21tdW5pdHlNZW1i";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 15) = "ZXJJbnZpdGVVdWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2NBgCIAEoBiIx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 16) = "ChBDb21tdW5pdHlMb2dVdWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2NBgC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 17) = "IAEoBiI0ChNDb21tdW5pdHlNZW1iZXJVdWlkEg4KBmhpZ2g2NBgBIAEoBhIN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 18) = "CgVsb3c2NBgCIAEoBiIyChFDb21tdW5pdHlSb2xlVXVpZBIOCgZoaWdoNjQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 19) = "ASABKAYSDQoFbG93NjQYAiABKAYiNQoUQ29tbXVuaXR5Um9sZU1hcFV1aWQS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 20) = "DgoGaGlnaDY0GAEgASgGEg0KBWxvdzY0GAIgASgGIjgKF0NvbW11bml0eU1l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 21) = "bWJlclJvbGVVdWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2NBgCIAEoBiI0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 22) = "ChNBcHBPcmdhbml6YXRpb25VdWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 23) = "NBgCIAEoBiIrCgpEZXZpY2VVdWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 24) = "NBgCIAEoBiIyChFEaXJlY3RNZXNzYWdlVXVpZBIOCgZoaWdoNjQYASABKAYS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 25) = "DQoFbG93NjQYAiABKAYiOAoXRGlyZWN0TWVzc2FnZU1lbWJlclV1aWQSDgoG";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 26) = "aGlnaDY0GAEgASgGEg0KBWxvdzY0GAIgASgGIi4KDURpcmVjdG9yeVV1aWQS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 27) = "DgoGaGlnaDY0GAEgASgGEg0KBWxvdzY0GAIgASgGIikKCEZpbGVVdWlkEg4K";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 28) = "BmhpZ2g2NBgBIAEoBhINCgVsb3c2NBgCIAEoBiI0ChNGcmllbmRzaGlwR3Jv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 29) = "dXBVdWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2NBgCIAEoBiIvCg5Gcmll";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 30) = "bmRzaGlwVXVpZBIOCgZoaWdoNjQYASABKAYSDQoFbG93NjQYAiABKAYiNQoU";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 31) = "RnJpZW5kc2hpcEludml0ZVV1aWQSDgoGaGlnaDY0GAEgASgGEg0KBWxvdzY0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 32) = "GAIgASgGIi4KDUh1YlNlcnZlclV1aWQSDgoGaGlnaDY0GAEgASgGEg0KBWxv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 33) = "dzY0GAIgASgGIjYKFU1lc3NhZ2VBdHRhY2htZW50VXVpZBIOCgZoaWdoNjQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 34) = "ASABKAYSDQoFbG93NjQYAiABKAYiNQoUTWVzc2FnZUJ1c1NlcnZlclV1aWQS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 35) = "DgoGaGlnaDY0GAEgASgGEg0KBWxvdzY0GAIgASgGIjUKFE1lc3NhZ2VDb250";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 36) = "YWluZXJVdWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2NBgCIAEoBiIsCgtN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 37) = "ZXNzYWdlVXVpZBIOCgZoaWdoNjQYASABKAYSDQoFbG93NjQYAiABKAYiMQoQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 38) = "Tm90aWZpY2F0aW9uVXVpZBIOCgZoaWdoNjQYASABKAYSDQoFbG93NjQYAiAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 39) = "KAYiLAoLVW5rbm93blV1aWQSDgoGaGlnaDY0GAEgASgGEg0KBWxvdzY0GAIg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 40) = "ASgGIikKCFVzZXJVdWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2NBgCIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 41) = "BiIxChBXZWJBcGlTZXJ2ZXJVdWlkEg4KBmhpZ2g2NBgBIAEoBhINCgVsb3c2";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 42) = "NBgCIAEoBiIxChBBcHBIdWJTZXJ2ZXJVdWlkEg4KBmhpZ2g2NBgBIAEoBhIN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 43) = "CgVsb3c2NBgCIAEoBiIyChFBcHBIb3N0U2VydmVyVXVpZBIOCgZoaWdoNjQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 44) = "ASABKAYSDQoFbG93NjQYAiABKAYiMQoQUm9sZU9yTWVtYmVyVXVpZBIOCgZo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 45) = "aWdoNjQYASABKAYSDQoFbG93NjQYAiABKAYiOgoZQ2hhbm5lbE9yQ2hhbm5l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 46) = "bEdyb3VwVXVpZBIOCgZoaWdoNjQYASABKAYSDQoFbG93NjQYAiABKAZCD6oC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray48<string>, string>(ref _003C_003Ey__InlineArray, 47) = "DFJvb3RBcHAuQ29yZWIGcHJvdG8z";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray48<string>, string>(in _003C_003Ey__InlineArray, 48)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[41]
		{
			new GeneratedClrTypeInfo(typeof(RootUuid), RootUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AppUuid), AppUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AppVersionUuid), AppVersionUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AppDeploymentUuid), AppDeploymentUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PersonUuid), PersonUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AssetUuid), AssetUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(BadgeUuid), BadgeUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelGroupUuid), ChannelGroupUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelUuid), ChannelUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommandIdempotencyUuid), CommandIdempotencyUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppUuid), CommunityAppUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberBanUuid), CommunityMemberBanUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityUuid), CommunityUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberInviteUuid), CommunityMemberInviteUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityLogUuid), CommunityLogUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberUuid), CommunityMemberUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityRoleUuid), CommunityRoleUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityRoleMapUuid), CommunityRoleMapUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberRoleUuid), CommunityMemberRoleUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AppOrganizationUuid), AppOrganizationUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DeviceUuid), DeviceUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectMessageUuid), DirectMessageUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectMessageMemberUuid), DirectMessageMemberUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DirectoryUuid), DirectoryUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FileUuid), FileUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendshipGroupUuid), FriendshipGroupUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendshipUuid), FriendshipUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendshipInviteUuid), FriendshipInviteUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(HubServerUuid), HubServerUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessageAttachmentUuid), MessageAttachmentUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessageBusServerUuid), MessageBusServerUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessageContainerUuid), MessageContainerUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessageUuid), MessageUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NotificationUuid), NotificationUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UnknownUuid), UnknownUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserUuid), UserUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(WebApiServerUuid), WebApiServerUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AppHubServerUuid), AppHubServerUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AppHostServerUuid), AppHostServerUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RoleOrMemberUuid), RoleOrMemberUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelOrChannelGroupUuid), ChannelOrChannelGroupUuid.Parser, new string[2] { "High64", "Low64" }, null, null, null, null)
		}));
	}
}
