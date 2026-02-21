using System;
using Google.Protobuf.Reflection;
using RootApp.Assets;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Packets;

public static class MessageReferenceMapsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static MessageReferenceMapsReflection()
	{
		_003C_003Ey__InlineArray21<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray21<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Cit3ZWJhcGkvcGFja2V0cy9tZXNzYWdlX3JlZmVyZW5jZV9tYXBzLnByb3Rv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 1) = "EgRyb290GhVjb3JlL3Jvb3RfdXVpZHMucHJvdG8aHGNvcmUvYXNzZXRfaW5m";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 2) = "b3JtYXRpb24ucHJvdG8aE2NvcmUvdmFsaWRhdGUucHJvdG8iWAoXTWVzc2Fn";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 3) = "ZVJlZmVyZW5jZU1hcFVzZXISJwoHdXNlcl9pZBgKIAEoCzIOLnJvb3QuVXNl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 4) = "clV1aWRCBrpIA8gBARIUCgRuYW1lGAsgASgJQga6SAPIAQEiYQoaTWVzc2Fn";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 5) = "ZVJlZmVyZW5jZU1hcENoYW5uZWwSLQoKY2hhbm5lbF9pZBgKIAEoCzIRLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 6) = "b3QuQ2hhbm5lbFV1aWRCBrpIA8gBARIUCgRuYW1lGAsgASgJQga6SAPIAQEi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 7) = "dAogTWVzc2FnZVJlZmVyZW5jZU1hcENvbW11bml0eVJvbGUSOgoRY29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 8) = "aXR5X3JvbGVfaWQYCiABKAsyFy5yb290LkNvbW11bml0eVJvbGVVdWlkQga6";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 9) = "SAPIAQESFAoEbmFtZRgLIAEoCUIGukgDyAEBIrcDChRNZXNzYWdlUmVmZXJl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 10) = "bmNlTWFwcxIsCgV1c2VycxgKIAMoCzIdLnJvb3QuTWVzc2FnZVJlZmVyZW5j";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 11) = "ZU1hcFVzZXISMgoIY2hhbm5lbHMYCyADKAsyIC5yb290Lk1lc3NhZ2VSZWZl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 12) = "cmVuY2VNYXBDaGFubmVsEjUKBXJvbGVzGAwgAygLMiYucm9vdC5NZXNzYWdl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 13) = "UmVmZXJlbmNlTWFwQ29tbXVuaXR5Um9sZRJBCgxpbWFnZV9hc3NldHMYDSAD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 14) = "KAsyKy5yb290Lk1lc3NhZ2VSZWZlcmVuY2VNYXBzLkltYWdlQXNzZXRzRW50";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 15) = "cnkSNgoGYXNzZXRzGA4gAygLMiYucm9vdC5NZXNzYWdlUmVmZXJlbmNlTWFw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 16) = "cy5Bc3NldHNFbnRyeRpEChBJbWFnZUFzc2V0c0VudHJ5EgsKA2tleRgBIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 17) = "CRIfCgV2YWx1ZRgCIAEoCzIQLnJvb3QuQXNzZXRJbWFnZToCOAEaRQoLQXNz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 18) = "ZXRzRW50cnkSCwoDa2V5GAEgASgJEiUKBXZhbHVlGAIgASgLMhYucm9vdC5B";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 19) = "c3NldEluZm9ybWF0aW9uOgI4AUIgqgIdUm9vdEFwcC5XZWJBcGkuU2hhcmVk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray21<string>, string>(ref _003C_003Ey__InlineArray, 20) = "LlBhY2tldHNiBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray21<string>, string>(in _003C_003Ey__InlineArray, 21)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[3]
		{
			RootUuidsReflection.Descriptor,
			AssetInformationReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(MessageReferenceMapUser), MessageReferenceMapUser.Parser, new string[2] { "UserId", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessageReferenceMapChannel), MessageReferenceMapChannel.Parser, new string[2] { "ChannelId", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessageReferenceMapCommunityRole), MessageReferenceMapCommunityRole.Parser, new string[2] { "CommunityRoleId", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessageReferenceMaps), MessageReferenceMaps.Parser, new string[5] { "Users", "Channels", "Roles", "ImageAssets", "Assets" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
