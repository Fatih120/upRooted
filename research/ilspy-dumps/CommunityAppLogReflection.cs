using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class CommunityAppLogReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CommunityAppLogReflection()
	{
		_003C_003Ey__InlineArray17<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray17<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Cid3ZWJhcGkvcmVxdWVzdHMvY29tbXVuaXR5X2FwcF9sb2cucHJvdG8SBHJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 1) = "b3QaFWNvcmUvcm9vdF91dWlkcy5wcm90bxoUd2ViYXBpL2NvbnRleHQucHJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 2) = "dG8aEndlYmFwaS9lbnVtcy5wcm90bxoTY29yZS92YWxpZGF0ZS5wcm90byK/";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 3) = "AQoaQ29tbXVuaXR5QXBwTG9nTGlzdFJlcXVlc3QSMQoMY29tbXVuaXR5X2lk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 4) = "GAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPIAQESOAoQY29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 5) = "aXR5X2FwcF9pZBgLIAEoCzIWLnJvb3QuQ29tbXVuaXR5QXBwVXVpZEIGukgD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 6) = "yAEBEjQKGWxhc3RfY29tbXVuaXR5X2FwcF9sb2dfaWQYDCABKAsyES5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 7) = "LlVua25vd25VdWlkIq8BChlDb21tdW5pdHlBcHBMb2dHZXRSZXF1ZXN0EjEK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 8) = "DGNvbW11bml0eV9pZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 9) = "yAEBEjgKEGNvbW11bml0eV9hcHBfaWQYCyABKAsyFi5yb290LkNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 10) = "eUFwcFV1aWRCBrpIA8gBARIlCgJpZBgMIAEoCzIRLnJvb3QuVW5rbm93blV1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 11) = "aWRCBrpIA8gBASLJAQocQ29tbXVuaXR5QXBwTG9nQ3JlYXRlUmVxdWVzdBIi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 12) = "Cgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIxCgxjb21tdW5p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 13) = "dHlfaWQYBCABKAsyEy5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARJBChZj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 14) = "b21tdW5pdHlfYXBwX2xvZ190eXBlGAYgASgOMhkucm9vdC5Db21tdW5pdHlB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 15) = "cHBMb2dUeXBlQga6SAPIAQESDwoHbWVzc2FnZRgHIAEoCUImqgIjUm9vdEFw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray17<string>, string>(ref _003C_003Ey__InlineArray, 16) = "cC5XZWJBcGkuU2hhcmVkLkdycGMuUmVxdWVzdHNiBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray17<string>, string>(in _003C_003Ey__InlineArray, 17)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[4]
		{
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			EnumsReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(CommunityAppLogListRequest), CommunityAppLogListRequest.Parser, new string[3] { "CommunityId", "CommunityAppId", "LastCommunityAppLogId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppLogGetRequest), CommunityAppLogGetRequest.Parser, new string[3] { "CommunityId", "CommunityAppId", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityAppLogCreateRequest), CommunityAppLogCreateRequest.Parser, new string[4] { "Context", "CommunityId", "CommunityAppLogType", "Message" }, null, null, null, null)
		}));
	}
}
