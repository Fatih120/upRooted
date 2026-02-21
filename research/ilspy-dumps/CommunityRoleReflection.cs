using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class CommunityRoleReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CommunityRoleReflection()
	{
		_003C_003Ey__InlineArray33<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray33<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiR3ZWJhcGkvcmVxdWVzdHMvY29tbXVuaXR5X3JvbGUucHJvdG8SBHJvb3Qa";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 1) = "FWNvcmUvcm9vdF91dWlkcy5wcm90bxoeZ29vZ2xlL3Byb3RvYnVmL3dyYXBw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 2) = "ZXJzLnByb3RvGhR3ZWJhcGkvY29udGV4dC5wcm90bxoXd2ViYXBpL3Blcm1p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 3) = "c3Npb24ucHJvdG8aE2NvcmUvdmFsaWRhdGUucHJvdG8iuAIKGkNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 4) = "eVJvbGVDcmVhdGVSZXF1ZXN0EiIKB2NvbnRleHQYASABKAsyES5yb290LlJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 5) = "b3RDb250ZXh0EjEKDGNvbW11bml0eV9pZBgKIAEoCzITLnJvb3QuQ29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 6) = "aXR5VXVpZEIGukgDyAEBEgwKBG5hbWUYCyABKAkSLwoJY29sb3JfaGV4GAwg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 7) = "ASgLMhwuZ29vZ2xlLnByb3RvYnVmLlN0cmluZ1ZhbHVlEjcKFGNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 8) = "eV9wZXJtaXNzaW9uGA8gASgLMhkucm9vdC5Db21tdW5pdHlQZXJtaXNzaW9u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 9) = "EjMKEmNoYW5uZWxfcGVybWlzc2lvbhgQIAEoCzIXLnJvb3QuQ2hhbm5lbFBl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 10) = "cm1pc3Npb24SFgoOaXNfbWVudGlvbmFibGUYESABKAgixQIKGENvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 11) = "eVJvbGVFZGl0UmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 12) = "Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 13) = "eVV1aWRCBrpIA8gBARIrCgJpZBgLIAEoCzIXLnJvb3QuQ29tbXVuaXR5Um9s";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 14) = "ZVV1aWRCBrpIA8gBARIMCgRuYW1lGA0gASgJEhEKCWNvbG9yX2hleBgOIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 15) = "CRI3ChRjb21tdW5pdHlfcGVybWlzc2lvbhgPIAEoCzIZLnJvb3QuQ29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 16) = "aXR5UGVybWlzc2lvbhIzChJjaGFubmVsX3Blcm1pc3Npb24YECABKAsyFy5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 17) = "b290LkNoYW5uZWxQZXJtaXNzaW9uEhYKDmlzX21lbnRpb25hYmxlGBEgASgI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 18) = "IqABChpDb21tdW5pdHlSb2xlRGVsZXRlUmVxdWVzdBIiCgdjb250ZXh0GAEg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 19) = "ASgLMhEucm9vdC5Sb290Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYCiABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 20) = "Ey5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARIrCgJpZBgLIAEoCzIXLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 21) = "b3QuQ29tbXVuaXR5Um9sZVV1aWRCBrpIA8gBASJ5ChdDb21tdW5pdHlSb2xl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 22) = "R2V0UmVxdWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 23) = "bml0eVV1aWRCBrpIA8gBARIrCgJpZBgLIAEoCzIXLnJvb3QuQ29tbXVuaXR5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 24) = "Um9sZVV1aWRCBrpIA8gBASJNChhDb21tdW5pdHlSb2xlTGlzdFJlcXVlc3QS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 25) = "MQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 26) = "SAPIAQEi2QEKGENvbW11bml0eVJvbGVNb3ZlUmVxdWVzdBIiCgdjb250ZXh0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 27) = "GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYCiAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 28) = "KAsyEy5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARIrCgJpZBgLIAEoCzIX";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 29) = "LnJvb3QuQ29tbXVuaXR5Um9sZVV1aWRCBrpIA8gBARI5ChhiZWZvcmVfY29t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 30) = "bXVuaXR5X3JvbGVfaWQYDCABKAsyFy5yb290LkNvbW11bml0eVJvbGVVdWlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 31) = "QiaqAiNSb290QXBwLldlYkFwaS5TaGFyZWQuR3JwYy5SZXF1ZXN0c2IGcHJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray33<string>, string>(ref _003C_003Ey__InlineArray, 32) = "dG8z";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray33<string>, string>(in _003C_003Ey__InlineArray, 33)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[5]
		{
			RootUuidsReflection.Descriptor,
			WrappersReflection.Descriptor,
			ContextReflection.Descriptor,
			PermissionReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(CommunityRoleCreateRequest), CommunityRoleCreateRequest.Parser, new string[7] { "Context", "CommunityId", "Name", "ColorHex", "CommunityPermission", "ChannelPermission", "IsMentionable" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityRoleEditRequest), CommunityRoleEditRequest.Parser, new string[8] { "Context", "CommunityId", "Id", "Name", "ColorHex", "CommunityPermission", "ChannelPermission", "IsMentionable" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityRoleDeleteRequest), CommunityRoleDeleteRequest.Parser, new string[3] { "Context", "CommunityId", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityRoleGetRequest), CommunityRoleGetRequest.Parser, new string[2] { "CommunityId", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityRoleListRequest), CommunityRoleListRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityRoleMoveRequest), CommunityRoleMoveRequest.Parser, new string[4] { "Context", "CommunityId", "Id", "BeforeCommunityRoleId" }, null, null, null, null)
		}));
	}
}
