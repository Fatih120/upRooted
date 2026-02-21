using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class CommunityMemberRoleReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CommunityMemberRoleReflection()
	{
		_003C_003Ey__InlineArray22<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray22<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Cit3ZWJhcGkvcmVxdWVzdHMvY29tbXVuaXR5X21lbWJlcl9yb2xlLnByb3Rv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 1) = "EgRyb290GhVjb3JlL3Jvb3RfdXVpZHMucHJvdG8aFHdlYmFwaS9jb250ZXh0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 2) = "LnByb3RvGhNjb3JlL3ZhbGlkYXRlLnByb3RvItwBCh1Db21tdW5pdHlNZW1i";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 3) = "ZXJSb2xlQWRkUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 4) = "Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 5) = "eVV1aWRCBrpIA8gBARI6ChFjb21tdW5pdHlfcm9sZV9pZBgLIAEoCzIXLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 6) = "b3QuQ29tbXVuaXR5Um9sZVV1aWRCBrpIA8gBARIoCgh1c2VyX2lkcxgMIAMo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 7) = "CzIOLnJvb3QuVXNlclV1aWRCBrpIA8gBASLfAQogQ29tbXVuaXR5TWVtYmVy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 8) = "Um9sZVJlbW92ZVJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 9) = "dENvbnRleHQSMQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 10) = "dHlVdWlkQga6SAPIAQESOgoRY29tbXVuaXR5X3JvbGVfaWQYCyABKAsyFy5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 11) = "b290LkNvbW11bml0eVJvbGVVdWlkQga6SAPIAQESKAoIdXNlcl9pZHMYDCAD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 12) = "KAsyDi5yb290LlVzZXJVdWlkQga6SAPIAQEi4gEKJENvbW11bml0eU1lbWJl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 13) = "clJvbGVTZXRQcmltYXJ5UmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 14) = "dC5Sb290Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 15) = "bW11bml0eVV1aWRCBrpIA8gBARInCgd1c2VyX2lkGAsgASgLMg4ucm9vdC5V";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 16) = "c2VyVXVpZEIGukgDyAEBEjoKEWNvbW11bml0eV9yb2xlX2lkGAwgASgLMhcu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 17) = "cm9vdC5Db21tdW5pdHlSb2xlVXVpZEIGukgDyAEBInwKHkNvbW11bml0eU1l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 18) = "bWJlclJvbGVMaXN0UmVxdWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 19) = "b290LkNvbW11bml0eVV1aWRCBrpIA8gBARInCgd1c2VyX2lkGAsgASgLMg4u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 20) = "cm9vdC5Vc2VyVXVpZEIGukgDyAEBQiaqAiNSb290QXBwLldlYkFwaS5TaGFy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 21) = "ZWQuR3JwYy5SZXF1ZXN0c2IGcHJvdG8z";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray22<string>, string>(in _003C_003Ey__InlineArray, 22)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[3]
		{
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(CommunityMemberRoleAddRequest), CommunityMemberRoleAddRequest.Parser, new string[4] { "Context", "CommunityId", "CommunityRoleId", "UserIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberRoleRemoveRequest), CommunityMemberRoleRemoveRequest.Parser, new string[4] { "Context", "CommunityId", "CommunityRoleId", "UserIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberRoleSetPrimaryRequest), CommunityMemberRoleSetPrimaryRequest.Parser, new string[4] { "Context", "CommunityId", "UserId", "CommunityRoleId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberRoleListRequest), CommunityMemberRoleListRequest.Parser, new string[2] { "CommunityId", "UserId" }, null, null, null, null)
		}));
	}
}
