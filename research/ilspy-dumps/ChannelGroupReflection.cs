using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class ChannelGroupReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ChannelGroupReflection()
	{
		_003C_003Ey__InlineArray27<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray27<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiN3ZWJhcGkvcmVxdWVzdHMvY2hhbm5lbF9ncm91cC5wcm90bxIEcm9vdBoV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 1) = "Y29yZS9yb290X3V1aWRzLnByb3RvGhR3ZWJhcGkvY29udGV4dC5wcm90bxoh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 2) = "d2ViYXBpL3JlcXVlc3RzL2FjY2Vzc19ydWxlLnByb3RvGhNjb3JlL3ZhbGlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 3) = "YXRlLnByb3RvItABChlDaGFubmVsR3JvdXBDcmVhdGVSZXF1ZXN0EiIKB2Nv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 4) = "bnRleHQYASABKAsyES5yb290LlJvb3RDb250ZXh0EjEKDGNvbW11bml0eV9p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 5) = "ZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEhQKBG5hbWUY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 6) = "CyABKAlCBrpIA8gBARJGChNhY2Nlc3NfcnVsZV9jcmVhdGVzGAwgAygLMiku";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 7) = "cm9vdC5BY2Nlc3NSdWxlQ3JlYXRlUm9sZU9yTWVtYmVyUmVxdWVzdCJMChdD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 8) = "aGFubmVsR3JvdXBMaXN0UmVxdWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 9) = "Ey5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBASLlAQoXQ2hhbm5lbEdyb3Vw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 10) = "RWRpdFJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 11) = "eHQSMQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 12) = "Qga6SAPIAQESKgoCaWQYCyABKAsyFi5yb290LkNoYW5uZWxHcm91cFV1aWRC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 13) = "BrpIA8gBARIMCgRuYW1lGAwgASgJEjkKEmFjY2Vzc19ydWxlX3VwZGF0ZRgN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 14) = "IAEoCzIdLnJvb3QuQWNjZXNzUnVsZVVwZGF0ZVJlcXVlc3Qi1QEKF0NoYW5u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 15) = "ZWxHcm91cE1vdmVSZXF1ZXN0EiIKB2NvbnRleHQYASABKAsyES5yb290LlJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 16) = "b3RDb250ZXh0EjEKDGNvbW11bml0eV9pZBgKIAEoCzITLnJvb3QuQ29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 17) = "aXR5VXVpZEIGukgDyAEBEioKAmlkGAsgASgLMhYucm9vdC5DaGFubmVsR3Jv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 18) = "dXBVdWlkQga6SAPIAQESNwoXYmVmb3JlX2NoYW5uZWxfZ3JvdXBfaWQYDCAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 19) = "KAsyFi5yb290LkNoYW5uZWxHcm91cFV1aWQingEKGUNoYW5uZWxHcm91cERl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 20) = "bGV0ZVJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 21) = "eHQSMQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 22) = "Qga6SAPIAQESKgoCaWQYCyABKAsyFi5yb290LkNoYW5uZWxHcm91cFV1aWRC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 23) = "BrpIA8gBASJ3ChZDaGFubmVsR3JvdXBHZXRSZXF1ZXN0EjEKDGNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 24) = "eV9pZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEioKAmlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 25) = "GAsgASgLMhYucm9vdC5DaGFubmVsR3JvdXBVdWlkQga6SAPIAQFCJqoCI1Jv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray27<string>, string>(ref _003C_003Ey__InlineArray, 26) = "b3RBcHAuV2ViQXBpLlNoYXJlZC5HcnBjLlJlcXVlc3RzYgZwcm90bzM=";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray27<string>, string>(in _003C_003Ey__InlineArray, 27)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[4]
		{
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			AccessRuleReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(ChannelGroupCreateRequest), ChannelGroupCreateRequest.Parser, new string[4] { "Context", "CommunityId", "Name", "AccessRuleCreates" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelGroupListRequest), ChannelGroupListRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelGroupEditRequest), ChannelGroupEditRequest.Parser, new string[5] { "Context", "CommunityId", "Id", "Name", "AccessRuleUpdate" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelGroupMoveRequest), ChannelGroupMoveRequest.Parser, new string[4] { "Context", "CommunityId", "Id", "BeforeChannelGroupId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelGroupDeleteRequest), ChannelGroupDeleteRequest.Parser, new string[3] { "Context", "CommunityId", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChannelGroupGetRequest), ChannelGroupGetRequest.Parser, new string[2] { "CommunityId", "Id" }, null, null, null, null)
		}));
	}
}
