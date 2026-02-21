using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class CommunityMemberBanReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CommunityMemberBanReflection()
	{
		global::_003C_003Ey__InlineArray32<string> _003C_003Ey__InlineArray = default(global::_003C_003Ey__InlineArray32<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Cip3ZWJhcGkvcmVxdWVzdHMvY29tbXVuaXR5X21lbWJlcl9iYW4ucHJvdG8S";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 1) = "BHJvb3QaFWNvcmUvcm9vdF91dWlkcy5wcm90bxofZ29vZ2xlL3Byb3RvYnVm";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 2) = "L3RpbWVzdGFtcC5wcm90bxoeZ29vZ2xlL3Byb3RvYnVmL3dyYXBwZXJzLnBy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 3) = "b3RvGhR3ZWJhcGkvY29udGV4dC5wcm90bxoTY29yZS92YWxpZGF0ZS5wcm90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 4) = "byL/AQofQ29tbXVuaXR5TWVtYmVyQmFuQ3JlYXRlUmVxdWVzdBIiCgdjb250";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 5) = "ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIxCgxjb21tdW5pdHlfaWQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 6) = "CiABKAsyEy5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARInCgd1c2VyX2lk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 7) = "GAsgASgLMg4ucm9vdC5Vc2VyVXVpZEIGukgDyAEBEiwKBnJlYXNvbhgMIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 8) = "CzIcLmdvb2dsZS5wcm90b2J1Zi5TdHJpbmdWYWx1ZRIuCgpleHBpcmVzX2F0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 9) = "GA0gASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcCL8AQojQ29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 10) = "aXR5TWVtYmVyQmFuQ3JlYXRlQnVsa1JlcXVlc3QSIgoHY29udGV4dBgBIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 11) = "CzIRLnJvb3QuUm9vdENvbnRleHQSMQoMY29tbXVuaXR5X2lkGAogASgLMhMu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 12) = "cm9vdC5Db21tdW5pdHlVdWlkQga6SAPIAQESIAoIdXNlcl9pZHMYCyADKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 13) = "Di5yb290LlVzZXJVdWlkEiwKBnJlYXNvbhgMIAEoCzIcLmdvb2dsZS5wcm90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 14) = "b2J1Zi5TdHJpbmdWYWx1ZRIuCgpleHBpcmVzX2F0GA0gASgLMhouZ29vZ2xl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 15) = "LnByb3RvYnVmLlRpbWVzdGFtcCKhAQofQ29tbXVuaXR5TWVtYmVyQmFuRGVs";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 16) = "ZXRlUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 17) = "dBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0eVV1aWRC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 18) = "BrpIA8gBARInCgd1c2VyX2lkGAsgASgLMg4ucm9vdC5Vc2VyVXVpZEIGukgD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 19) = "yAEBIp8BCh1Db21tdW5pdHlNZW1iZXJCYW5LaWNrUmVxdWVzdBIiCgdjb250";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 20) = "ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIxCgxjb21tdW5pdHlfaWQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 21) = "CiABKAsyEy5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARInCgd1c2VyX2lk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 22) = "GAsgASgLMg4ucm9vdC5Vc2VyVXVpZEIGukgDyAEBIpwBCiFDb21tdW5pdHlN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 23) = "ZW1iZXJCYW5LaWNrQnVsa1JlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 24) = "b3QuUm9vdENvbnRleHQSMQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5D";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 25) = "b21tdW5pdHlVdWlkQga6SAPIAQESIAoIdXNlcl9pZHMYCyADKAsyDi5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 26) = "LlVzZXJVdWlkInoKHENvbW11bml0eU1lbWJlckJhbkdldFJlcXVlc3QSMQoM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 27) = "Y29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 28) = "AQESJwoHdXNlcl9pZBgLIAEoCzIOLnJvb3QuVXNlclV1aWRCBrpIA8gBASJS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 29) = "Ch1Db21tdW5pdHlNZW1iZXJCYW5MaXN0UmVxdWVzdBIxCgxjb21tdW5pdHlf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 30) = "aWQYCiABKAsyEy5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBAUImqgIjUm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray32<string>, string>(ref _003C_003Ey__InlineArray, 31) = "dEFwcC5XZWJBcGkuU2hhcmVkLkdycGMuUmVxdWVzdHNiBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<global::_003C_003Ey__InlineArray32<string>, string>(in _003C_003Ey__InlineArray, 32)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[5]
		{
			RootUuidsReflection.Descriptor,
			TimestampReflection.Descriptor,
			WrappersReflection.Descriptor,
			ContextReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[7]
		{
			new GeneratedClrTypeInfo(typeof(CommunityMemberBanCreateRequest), CommunityMemberBanCreateRequest.Parser, new string[5] { "Context", "CommunityId", "UserId", "Reason", "ExpiresAt" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberBanCreateBulkRequest), CommunityMemberBanCreateBulkRequest.Parser, new string[5] { "Context", "CommunityId", "UserIds", "Reason", "ExpiresAt" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberBanDeleteRequest), CommunityMemberBanDeleteRequest.Parser, new string[3] { "Context", "CommunityId", "UserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberBanKickRequest), CommunityMemberBanKickRequest.Parser, new string[3] { "Context", "CommunityId", "UserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberBanKickBulkRequest), CommunityMemberBanKickBulkRequest.Parser, new string[3] { "Context", "CommunityId", "UserIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberBanGetRequest), CommunityMemberBanGetRequest.Parser, new string[2] { "CommunityId", "UserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberBanListRequest), CommunityMemberBanListRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null)
		}));
	}
}
