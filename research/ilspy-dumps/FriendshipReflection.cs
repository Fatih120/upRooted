using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class FriendshipReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FriendshipReflection()
	{
		_003C_003Ey__InlineArray31<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray31<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiB3ZWJhcGkvcmVxdWVzdHMvZnJpZW5kc2hpcC5wcm90bxIEcm9vdBoVY29y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 1) = "ZS9yb290X3V1aWRzLnByb3RvGhR3ZWJhcGkvY29udGV4dC5wcm90bxoTY29y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 2) = "ZS92YWxpZGF0ZS5wcm90byIcChpGcmllbmRzaGlwR3JvdXBMaXN0UmVxdWVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 3) = "dCJQChxGcmllbmRzaGlwR3JvdXBDcmVhdGVSZXF1ZXN0EiIKB2NvbnRleHQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 4) = "ASABKAsyES5yb290LlJvb3RDb250ZXh0EgwKBG5hbWUYCiABKAkihQEKGkZy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 5) = "aWVuZHNoaXBHcm91cEVkaXRSZXF1ZXN0EiIKB2NvbnRleHQYASABKAsyES5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 6) = "b290LlJvb3RDb250ZXh0Ei0KAmlkGAogASgLMhkucm9vdC5GcmllbmRzaGlw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 7) = "R3JvdXBVdWlkQga6SAPIAQESFAoEbmFtZRgLIAEoCUIGukgDyAEBIq4BChpG";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 8) = "cmllbmRzaGlwR3JvdXBNb3ZlUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 9) = "cm9vdC5Sb290Q29udGV4dBItCgJpZBgKIAEoCzIZLnJvb3QuRnJpZW5kc2hp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 10) = "cEdyb3VwVXVpZEIGukgDyAEBEj0KGmJlZm9yZV9mcmllbmRzaGlwX2dyb3Vw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 11) = "X2lkGAwgASgLMhkucm9vdC5GcmllbmRzaGlwR3JvdXBVdWlkInEKHEZyaWVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 12) = "ZHNoaXBHcm91cERlbGV0ZVJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 13) = "b3QuUm9vdENvbnRleHQSLQoCaWQYCiABKAsyGS5yb290LkZyaWVuZHNoaXBH";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 14) = "cm91cFV1aWRCBrpIA8gBASKnAgoVRnJpZW5kc2hpcE1vdmVSZXF1ZXN0EiIK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 15) = "B2NvbnRleHQYASABKAsyES5yb290LlJvb3RDb250ZXh0Ei4KDmZyaWVuZF91";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 16) = "c2VyX2lkGAogASgLMg4ucm9vdC5Vc2VyVXVpZEIGukgDyAEBEkIKF29sZF9m";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 17) = "cmllbmRzaGlwX2dyb3VwX2lkGAsgASgLMhkucm9vdC5GcmllbmRzaGlwR3Jv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 18) = "dXBVdWlkQga6SAPIAQESQgoXbmV3X2ZyaWVuZHNoaXBfZ3JvdXBfaWQYDCAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 19) = "KAsyGS5yb290LkZyaWVuZHNoaXBHcm91cFV1aWRCBrpIA8gBARIyChRiZWZv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 20) = "cmVfZnJpZW5kc2hpcF9pZBgNIAEoCzIULnJvb3QuRnJpZW5kc2hpcFV1aWQi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 21) = "ZgoXRnJpZW5kc2hpcERlbGV0ZVJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIR";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 22) = "LnJvb3QuUm9vdENvbnRleHQSJwoHdXNlcl9pZBgFIAEoCzIOLnJvb3QuVXNl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 23) = "clV1aWRCBrpIA8gBASJVCh1GcmllbmRzaGlwSW52aXRlQ3JlYXRlUmVxdWVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 24) = "dBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIQCgh1c2Vy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 25) = "bmFtZRgKIAEoCSLNAQoeRnJpZW5kc2hpcEludml0ZVJlc3BvbmRSZXF1ZXN0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 26) = "EiIKB2NvbnRleHQYASABKAsyES5yb290LlJvb3RDb250ZXh0EjcKD25vdGlm";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 27) = "aWNhdGlvbl9pZBgKIAEoCzIWLnJvb3QuTm90aWZpY2F0aW9uVXVpZEIGukgD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 28) = "yAEBEi4KDmZyaWVuZF91c2VyX2lkGAsgASgLMg4ucm9vdC5Vc2VyVXVpZEIG";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 29) = "ukgDyAEBEh4KFmlzX2ZyaWVuZHNoaXBfYWNjZXB0ZWQYDCABKAhCJqoCI1Jv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray31<string>, string>(ref _003C_003Ey__InlineArray, 30) = "b3RBcHAuV2ViQXBpLlNoYXJlZC5HcnBjLlJlcXVlc3RzYgZwcm90bzM=";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray31<string>, string>(in _003C_003Ey__InlineArray, 31)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[3]
		{
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[9]
		{
			new GeneratedClrTypeInfo(typeof(FriendshipGroupListRequest), FriendshipGroupListRequest.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendshipGroupCreateRequest), FriendshipGroupCreateRequest.Parser, new string[2] { "Context", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendshipGroupEditRequest), FriendshipGroupEditRequest.Parser, new string[3] { "Context", "Id", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendshipGroupMoveRequest), FriendshipGroupMoveRequest.Parser, new string[3] { "Context", "Id", "BeforeFriendshipGroupId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendshipGroupDeleteRequest), FriendshipGroupDeleteRequest.Parser, new string[2] { "Context", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendshipMoveRequest), FriendshipMoveRequest.Parser, new string[5] { "Context", "FriendUserId", "OldFriendshipGroupId", "NewFriendshipGroupId", "BeforeFriendshipId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendshipDeleteRequest), FriendshipDeleteRequest.Parser, new string[2] { "Context", "UserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendshipInviteCreateRequest), FriendshipInviteCreateRequest.Parser, new string[2] { "Context", "Username" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FriendshipInviteRespondRequest), FriendshipInviteRespondRequest.Parser, new string[4] { "Context", "NotificationId", "FriendUserId", "IsFriendshipAccepted" }, null, null, null, null)
		}));
	}
}
