using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Payloads.Notification;

namespace RootApp.WebApi.Shared.Packets;

public static class NotificationsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static NotificationsReflection()
	{
		_003C_003Ey__InlineArray22<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray22<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiJ3ZWJhcGkvcGFja2V0cy9ub3RpZmljYXRpb25zLnByb3RvEgRyb290GhVj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 1) = "b3JlL3Jvb3RfdXVpZHMucHJvdG8aEndlYmFwaS9lbnVtcy5wcm90bxojd2Vi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 2) = "YXBpL3BheWxvYWRzL25vdGlmaWNhdGlvbnMucHJvdG8aE2NvcmUvdmFsaWRh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 3) = "dGUucHJvdG8icwoYTm90aWZpY2F0aW9uVmlld2VkUGFja2V0EiUKC3BhY2tl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 4) = "dF90eXBlGAEgASgOMhAucm9vdC5QYWNrZXRUeXBlEjAKEG5vdGlmaWNhdGlv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 5) = "bl9pZHMYAiADKAsyFi5yb290Lk5vdGlmaWNhdGlvblV1aWQiRAobTm90aWZp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 6) = "Y2F0aW9uVmlld2VkQWxsUGFja2V0EiUKC3BhY2tldF90eXBlGAEgASgOMhAu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 7) = "cm9vdC5QYWNrZXRUeXBlIr4BChlOb3RpZmljYXRpb25EZWxldGVkUGFja2V0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 8) = "EiUKC3BhY2tldF90eXBlGAEgASgOMhAucm9vdC5QYWNrZXRUeXBlEioKAmlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 9) = "GAMgASgLMhYucm9vdC5Ob3RpZmljYXRpb25VdWlkQga6SAPIAQESJAoMY29u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 10) = "dGFpbmVyX2lkGAQgASgLMg4ucm9vdC5Sb290VXVpZBIoChBzdWJfY29udGFp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 11) = "bmVyX2lkGAUgASgLMg4ucm9vdC5Sb290VXVpZCJFChxOb3RpZmljYXRpb25E";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 12) = "ZWxldGVkQWxsUGFja2V0EiUKC3BhY2tldF90eXBlGAEgASgOMhAucm9vdC5Q";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 13) = "YWNrZXRUeXBlItoCChJOb3RpZmljYXRpb25QYWNrZXQSJQoLcGFja2V0X3R5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 14) = "cGUYASABKA4yEC5yb290LlBhY2tldFR5cGUSKgoCaWQYAyABKAsyFi5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 15) = "Lk5vdGlmaWNhdGlvblV1aWRCBrpIA8gBARIkCgxjb250YWluZXJfaWQYBCAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 16) = "KAsyDi5yb290LlJvb3RVdWlkEigKEHN1Yl9jb250YWluZXJfaWQYBSABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 17) = "Di5yb290LlJvb3RVdWlkEicKB3VzZXJfaWQYBiABKAsyDi5yb290LlVzZXJV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 18) = "dWlkQga6SAPIAQESMQoRbm90aWZpY2F0aW9uX3R5cGUYByABKA4yFi5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 19) = "Lk5vdGlmaWNhdGlvblR5cGUSMgoHcGF5bG9hZBgIIAEoCzIZLnJvb3QuTm90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 20) = "aWZpY2F0aW9uUGF5bG9hZEIGukgDyAEBEhEKCWlzX3ZpZXdlZBgJIAEoCEIg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 21) = "qgIdUm9vdEFwcC5XZWJBcGkuU2hhcmVkLlBhY2tldHNiBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray22<string>, string>(in _003C_003Ey__InlineArray, 22)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[4]
		{
			RootUuidsReflection.Descriptor,
			EnumsReflection.Descriptor,
			RootApp.WebApi.Shared.Payloads.Notification.NotificationsReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[5]
		{
			new GeneratedClrTypeInfo(typeof(NotificationViewedPacket), NotificationViewedPacket.Parser, new string[2] { "PacketType", "NotificationIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NotificationViewedAllPacket), NotificationViewedAllPacket.Parser, new string[1] { "PacketType" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NotificationDeletedPacket), NotificationDeletedPacket.Parser, new string[4] { "PacketType", "Id", "ContainerId", "SubContainerId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NotificationDeletedAllPacket), NotificationDeletedAllPacket.Parser, new string[1] { "PacketType" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NotificationPacket), NotificationPacket.Parser, new string[8] { "PacketType", "Id", "ContainerId", "SubContainerId", "UserId", "NotificationType", "Payload", "IsViewed" }, null, null, null, null)
		}));
	}
}
