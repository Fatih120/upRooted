using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class CommunityMemberInviteReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CommunityMemberInviteReflection()
	{
		_003C_003Ey__InlineArray27<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray27<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Ci13ZWJhcGkvcmVxdWVzdHMvY29tbXVuaXR5X21lbWJlcl9pbnZpdGUucHJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 1) = "dG8SBHJvb3QaFWNvcmUvcm9vdF91dWlkcy5wcm90bxoUd2ViYXBpL2NvbnRl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 2) = "eHQucHJvdG8aE2NvcmUvdmFsaWRhdGUucHJvdG8i4QEKIkNvbW11bml0eU1l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 3) = "bWJlckludml0ZUNyZWF0ZVJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 4) = "b3QuUm9vdENvbnRleHQSMQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5D";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 5) = "b21tdW5pdHlVdWlkQga6SAPIAQESLwoPaW52aXRlZF91c2VyX2lkGAsgASgL";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 6) = "Mg4ucm9vdC5Vc2VyVXVpZEIGukgDyAEBEjMKEmNvbW11bml0eV9yb2xlX2lk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 7) = "cxgMIAMoCzIXLnJvb3QuQ29tbXVuaXR5Um9sZVV1aWQixQEKI0NvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 8) = "eU1lbWJlckludml0ZVJlc3BvbmRSZXF1ZXN0EiIKB2NvbnRleHQYASABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 9) = "ES5yb290LlJvb3RDb250ZXh0EjEKDGNvbW11bml0eV9pZBgKIAEoCzITLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 10) = "b3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEi4KDnNlbmRlcl91c2VyX2lkGAsg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 11) = "ASgLMg4ucm9vdC5Vc2VyVXVpZEIGukgDyAEBEhcKD2ludml0ZV9hY2NlcHRl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 12) = "ZBgMIAEoCCLcAQoiQ29tbXVuaXR5TWVtYmVySW52aXRlRGVsZXRlUmVxdWVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 13) = "dBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIxCgxjb21t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 14) = "dW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARIv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 15) = "Cg9pbnZpdGVkX3VzZXJfaWQYCyABKAsyDi5yb290LlVzZXJVdWlkQga6SAPI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 16) = "AQESLgoOc2VuZGVyX3VzZXJfaWQYDCABKAsyDi5yb290LlVzZXJVdWlkQga6";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 17) = "SAPIAQEitQEKH0NvbW11bml0eU1lbWJlckludml0ZUdldFJlcXVlc3QSMQoM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 18) = "Y29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 19) = "AQESLwoPaW52aXRlZF91c2VyX2lkGAsgASgLMg4ucm9vdC5Vc2VyVXVpZEIG";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 20) = "ukgDyAEBEi4KDnNlbmRlcl91c2VyX2lkGAwgASgLMg4ucm9vdC5Vc2VyVXVp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 21) = "ZEIGukgDyAEBIlUKIENvbW11bml0eU1lbWJlckludml0ZUxpc3RSZXF1ZXN0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 22) = "EjEKDGNvbW11bml0eV9pZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIG";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 23) = "ukgDyAEBIlgKJENvbW11bml0eU1lbWJlckludml0ZUxpbmtKb2luUmVxdWVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 24) = "dBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIMCgRjb2Rl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 25) = "GAogASgJQiaqAiNSb290QXBwLldlYkFwaS5TaGFyZWQuR3JwYy5SZXF1ZXN0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 26) = "c2IGcHJvdG8z";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray27<string>, string>(in _003C_003Ey__InlineArray, 27)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[3]
		{
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(CommunityMemberInviteCreateRequest), CommunityMemberInviteCreateRequest.Parser, new string[4] { "Context", "CommunityId", "InvitedUserId", "CommunityRoleIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberInviteRespondRequest), CommunityMemberInviteRespondRequest.Parser, new string[4] { "Context", "CommunityId", "SenderUserId", "InviteAccepted" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberInviteDeleteRequest), CommunityMemberInviteDeleteRequest.Parser, new string[4] { "Context", "CommunityId", "InvitedUserId", "SenderUserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberInviteGetRequest), CommunityMemberInviteGetRequest.Parser, new string[3] { "CommunityId", "InvitedUserId", "SenderUserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberInviteListRequest), CommunityMemberInviteListRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberInviteLinkJoinRequest), CommunityMemberInviteLinkJoinRequest.Parser, new string[2] { "Context", "Code" }, null, null, null, null)
		}));
	}
}
