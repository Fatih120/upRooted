using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class ChannelReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ChannelReflection()
	{
		_003C_003Ey__InlineArray40<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray40<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Ch13ZWJhcGkvcmVxdWVzdHMvY2hhbm5lbC5wcm90bxIEcm9vdBoVY29yZS9y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 1) = "b290X3V1aWRzLnByb3RvGh5nb29nbGUvcHJvdG9idWYvd3JhcHBlcnMucHJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 2) = "dG8aFHdlYmFwaS9jb250ZXh0LnByb3RvGiF3ZWJhcGkvcmVxdWVzdHMvYWNj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 3) = "ZXNzX3J1bGUucHJvdG8aE2NvcmUvdmFsaWRhdGUucHJvdG8ibQoRQ2hhbm5l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 4) = "bEdldFJlcXVlc3QSMQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 5) = "dW5pdHlVdWlkQga6SAPIAQESJQoCaWQYDCABKAsyES5yb290LkNoYW5uZWxV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 6) = "dWlkQga6SAPIAQEigQEKEkNoYW5uZWxMaXN0UmVxdWVzdBIxCgxjb21tdW5p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 7) = "dHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARI4ChBj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 8) = "aGFubmVsX2dyb3VwX2lkGAsgASgLMhYucm9vdC5DaGFubmVsR3JvdXBVdWlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 9) = "Qga6SAPIAQEiugMKFENoYW5uZWxDcmVhdGVSZXF1ZXN0EiIKB2NvbnRleHQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 10) = "ASABKAsyES5yb290LlJvb3RDb250ZXh0EjEKDGNvbW11bml0eV9pZBgKIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 11) = "CzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEjgKEGNoYW5uZWxfZ3Jv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 12) = "dXBfaWQYCyABKAsyFi5yb290LkNoYW5uZWxHcm91cFV1aWRCBrpIA8gBARIU";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 13) = "CgRuYW1lGAwgASgJQga6SAPIAQESMQoLZGVzY3JpcHRpb24YDSABKAsyHC5n";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 14) = "b29nbGUucHJvdG9idWYuU3RyaW5nVmFsdWUSHAoMY2hhbm5lbF90eXBlGA4g";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 15) = "ASgFQga6SAPIAQESLAocdXNlX2NoYW5uZWxfZ3JvdXBfcGVybWlzc2lvbhgP";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 16) = "IAEoCEIGukgDyAEBEjQKDmljb25fdG9rZW5fdXJpGBAgASgLMhwuZ29vZ2xl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 17) = "LnByb3RvYnVmLlN0cmluZ1ZhbHVlEkYKE2FjY2Vzc19ydWxlX2NyZWF0ZXMY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 18) = "ESADKAsyKS5yb290LkFjY2Vzc1J1bGVDcmVhdGVSb2xlT3JNZW1iZXJSZXF1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 19) = "ZXN0Iv8CChJDaGFubmVsRWRpdFJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIR";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 20) = "LnJvb3QuUm9vdENvbnRleHQSMQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 21) = "dC5Db21tdW5pdHlVdWlkQga6SAPIAQESJQoCaWQYCyABKAsyES5yb290LkNo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 22) = "YW5uZWxVdWlkQga6SAPIAQESDAoEbmFtZRgMIAEoCRIxCgtkZXNjcmlwdGlv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 23) = "bhgNIAEoCzIcLmdvb2dsZS5wcm90b2J1Zi5TdHJpbmdWYWx1ZRITCgt1cGRh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 24) = "dGVfaWNvbhgOIAEoCBI0Cg5pY29uX3Rva2VuX3VyaRgPIAEoCzIcLmdvb2ds";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 25) = "ZS5wcm90b2J1Zi5TdHJpbmdWYWx1ZRIkChx1c2VfY2hhbm5lbF9ncm91cF9w";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 26) = "ZXJtaXNzaW9uGBAgASgIEjkKEmFjY2Vzc19ydWxlX3VwZGF0ZRgRIAEoCzId";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 27) = "LnJvb3QuQWNjZXNzUnVsZVVwZGF0ZVJlcXVlc3QivAIKEkNoYW5uZWxNb3Zl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 28) = "UmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 29) = "Cgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0eVV1aWRCBrpI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 30) = "A8gBARIlCgJpZBgLIAEoCzIRLnJvb3QuQ2hhbm5lbFV1aWRCBrpIA8gBARI8";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 31) = "ChRvbGRfY2hhbm5lbF9ncm91cF9pZBgMIAEoCzIWLnJvb3QuQ2hhbm5lbEdy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 32) = "b3VwVXVpZEIGukgDyAEBEjwKFG5ld19jaGFubmVsX2dyb3VwX2lkGA0gASgL";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 33) = "MhYucm9vdC5DaGFubmVsR3JvdXBVdWlkQga6SAPIAQESLAoRYmVmb3JlX2No";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 34) = "YW5uZWxfaWQYDiABKAsyES5yb290LkNoYW5uZWxVdWlkIpQBChRDaGFubmVs";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 35) = "RGVsZXRlUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 36) = "dGV4dBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0eVV1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 37) = "aWRCBrpIA8gBARIlCgJpZBgLIAEoCzIRLnJvb3QuQ2hhbm5lbFV1aWRCBrpI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 38) = "A8gBAUImqgIjUm9vdEFwcC5XZWJBcGkuU2hhcmVkLkdycGMuUmVxdWVzdHNi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray40<string>, string>(ref _003C_003Ey__InlineArray, 39) = "BnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray40<string>, string>(in _003C_003Ey__InlineArray, 40)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[5]
		{
			RootUuidsReflection.Descriptor,
			WrappersReflection.Descriptor,
			ContextReflection.Descriptor,
			AccessRuleReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(ChannelGetRequest), ChannelGetRequest.Parser, new string[2] { "CommunityId", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelListRequest), ChannelListRequest.Parser, new string[2] { "CommunityId", "ChannelGroupId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelCreateRequest), ChannelCreateRequest.Parser, new string[9] { "Context", "CommunityId", "ChannelGroupId", "Name", "Description", "ChannelType", "UseChannelGroupPermission", "IconTokenUri", "AccessRuleCreates" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelEditRequest), ChannelEditRequest.Parser, new string[9] { "Context", "CommunityId", "Id", "Name", "Description", "UpdateIcon", "IconTokenUri", "UseChannelGroupPermission", "AccessRuleUpdate" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelMoveRequest), ChannelMoveRequest.Parser, new string[6] { "Context", "CommunityId", "Id", "OldChannelGroupId", "NewChannelGroupId", "BeforeChannelId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelDeleteRequest), ChannelDeleteRequest.Parser, new string[3] { "Context", "CommunityId", "Id" }, null, null, null, null)
		}));
	}
}
