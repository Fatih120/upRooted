using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class LinkReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static LinkReflection()
	{
		_003C_003Ey__InlineArray22<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray22<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Chp3ZWJhcGkvcmVxdWVzdHMvbGluay5wcm90bxIEcm9vdBoVY29yZS9yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 1) = "X3V1aWRzLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0YW1wLnByb3Rv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 2) = "Gh5nb29nbGUvcHJvdG9idWYvd3JhcHBlcnMucHJvdG8aFHdlYmFwaS9jb250";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 3) = "ZXh0LnByb3RvGhNjb3JlL3ZhbGlkYXRlLnByb3RvIlMKHkNvbW11bml0eUlu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 4) = "dml0ZUxpbmtMaXN0UmVxdWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 5) = "b290LkNvbW11bml0eVV1aWRCBrpIA8gBASJXCiJDb21tdW5pdHlJbnZpdGVM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 6) = "aW5rTGlzdE1pbmVSZXF1ZXN0EjEKDGNvbW11bml0eV9pZBgKIAEoCzITLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 7) = "b3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBIocBCh1Db21tdW5pdHlJbnZpdGVM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 8) = "aW5rR2V0UmVxdWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 9) = "bW11bml0eVV1aWRCBrpIA8gBARIzCgJpZBgLIAEoCzIfLnJvb3QuQ29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 10) = "aXR5TWVtYmVySW52aXRlVXVpZEIGukgDyAEBIjEKIUNvbW11bml0eUludml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 11) = "ZUxpbmtHZXRJbmZvUmVxdWVzdBIMCgRjb2RlGAogASgJItgBCiBDb21tdW5p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 12) = "dHlJbnZpdGVMaW5rQ3JlYXRlUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 13) = "cm9vdC5Sb290Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 14) = "LkNvbW11bml0eVV1aWRCBrpIA8gBARItCghtYXhfdXNlcxgLIAEoCzIbLmdv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 15) = "b2dsZS5wcm90b2J1Zi5JbnQzMlZhbHVlEi4KCmV4cGlyZXNfYXQYDCABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 16) = "Gi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wIq4BCiBDb21tdW5pdHlJbnZp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 17) = "dGVMaW5rRGVsZXRlUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5S";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 18) = "b290Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 19) = "bml0eVV1aWRCBrpIA8gBARIzCgJpZBgLIAEoCzIfLnJvb3QuQ29tbXVuaXR5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 20) = "TWVtYmVySW52aXRlVXVpZEIGukgDyAEBQiaqAiNSb290QXBwLldlYkFwaS5T";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray22<string>, string>(ref _003C_003Ey__InlineArray, 21) = "aGFyZWQuR3JwYy5SZXF1ZXN0c2IGcHJvdG8z";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray22<string>, string>(in _003C_003Ey__InlineArray, 22)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[5]
		{
			RootUuidsReflection.Descriptor,
			TimestampReflection.Descriptor,
			WrappersReflection.Descriptor,
			ContextReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(CommunityInviteLinkListRequest), CommunityInviteLinkListRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityInviteLinkListMineRequest), CommunityInviteLinkListMineRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityInviteLinkGetRequest), CommunityInviteLinkGetRequest.Parser, new string[2] { "CommunityId", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityInviteLinkGetInfoRequest), CommunityInviteLinkGetInfoRequest.Parser, new string[1] { "Code" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityInviteLinkCreateRequest), CommunityInviteLinkCreateRequest.Parser, new string[4] { "Context", "CommunityId", "MaxUses", "ExpiresAt" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityInviteLinkDeleteRequest), CommunityInviteLinkDeleteRequest.Parser, new string[3] { "Context", "CommunityId", "Id" }, null, null, null, null)
		}));
	}
}
