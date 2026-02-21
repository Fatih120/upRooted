using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class AccessRuleReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static AccessRuleReflection()
	{
		_003C_003Ey__InlineArray44<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray44<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiF3ZWJhcGkvcmVxdWVzdHMvYWNjZXNzX3J1bGUucHJvdG8SBHJvb3QaFWNv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 1) = "cmUvcm9vdF91dWlkcy5wcm90bxoUd2ViYXBpL2NvbnRleHQucHJvdG8aF3dl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 2) = "YmFwaS9wZXJtaXNzaW9uLnByb3RvGhNjb3JlL3ZhbGlkYXRlLnByb3RvIqoC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 3) = "ChdBY2Nlc3NSdWxlQ3JlYXRlUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 4) = "cm9vdC5Sb290Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 5) = "LkNvbW11bml0eVV1aWRCBrpIA8gBARJMChtjaGFubmVsX29yX2NoYW5uZWxf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 6) = "Z3JvdXBfaWQYCyABKAsyHy5yb290LkNoYW5uZWxPckNoYW5uZWxHcm91cFV1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 7) = "aWRCBrpIA8gBARI5ChFyb2xlX29yX21lbWJlcl9pZBgMIAEoCzIWLnJvb3Qu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 8) = "Um9sZU9yTWVtYmVyVXVpZEIGukgDyAEBEi8KB292ZXJsYXkYDSABKAsyHi5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 9) = "b290LkNoYW5uZWxPdmVybGF5UGVybWlzc2lvbiKRAQojQWNjZXNzUnVsZUNy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 10) = "ZWF0ZVJvbGVPck1lbWJlclJlcXVlc3QSOQoRcm9sZV9vcl9tZW1iZXJfaWQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 11) = "CiABKAsyFi5yb290LlJvbGVPck1lbWJlclV1aWRCBrpIA8gBARIvCgdvdmVy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 12) = "bGF5GAsgASgLMh4ucm9vdC5DaGFubmVsT3ZlcmxheVBlcm1pc3Npb24isAIK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 13) = "FUFjY2Vzc1J1bGVFZGl0UmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 14) = "dC5Sb290Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 15) = "bW11bml0eVV1aWRCBrpIA8gBARJMChtjaGFubmVsX29yX2NoYW5uZWxfZ3Jv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 16) = "dXBfaWQYCyABKAsyHy5yb290LkNoYW5uZWxPckNoYW5uZWxHcm91cFV1aWRC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 17) = "BrpIA8gBARI5ChFyb2xlX29yX21lbWJlcl9pZBgMIAEoCzIWLnJvb3QuUm9s";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 18) = "ZU9yTWVtYmVyVXVpZEIGukgDyAEBEjcKB292ZXJsYXkYDSABKAsyHi5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 19) = "LkNoYW5uZWxPdmVybGF5UGVybWlzc2lvbkIGukgDyAEBIvkBChdBY2Nlc3NS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 20) = "dWxlRGVsZXRlUmVxdWVzdBIiCgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 21) = "Q29udGV4dBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 22) = "eVV1aWRCBrpIA8gBARJMChtjaGFubmVsX29yX2NoYW5uZWxfZ3JvdXBfaWQY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 23) = "CyABKAsyHy5yb290LkNoYW5uZWxPckNoYW5uZWxHcm91cFV1aWRCBrpIA8gB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 24) = "ARI5ChFyb2xlX29yX21lbWJlcl9pZBgMIAEoCzIWLnJvb3QuUm9sZU9yTWVt";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 25) = "YmVyVXVpZEIGukgDyAEBIvwBChdBY2Nlc3NSdWxlVXBkYXRlUmVxdWVzdBIi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 26) = "Cgdjb250ZXh0GAEgASgLMhEucm9vdC5Sb290Q29udGV4dBIxCgxjb21tdW5p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 27) = "dHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARIuCgdj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 28) = "cmVhdGVzGAsgAygLMh0ucm9vdC5BY2Nlc3NSdWxlQ3JlYXRlUmVxdWVzdBIq";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 29) = "CgVlZGl0cxgMIAMoCzIbLnJvb3QuQWNjZXNzUnVsZUVkaXRSZXF1ZXN0Ei4K";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 30) = "B2RlbGV0ZXMYDSADKAsyHS5yb290LkFjY2Vzc1J1bGVEZWxldGVSZXF1ZXN0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 31) = "IqcBCixBY2Nlc3NSdWxlTGlzdEJ5Q2hhbm5lbE9yQ2hhbm5lbEdyb3VwUmVx";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 32) = "dWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsyEy5yb290LkNvbW11bml0eVV1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 33) = "aWRCBrpIA8gBARJEChtjaGFubmVsX29yX2NoYW5uZWxfZ3JvdXBfaWQYCyAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 34) = "KAsyHy5yb290LkNoYW5uZWxPckNoYW5uZWxHcm91cFV1aWQiiwEKI0FjY2Vz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 35) = "c1J1bGVMaXN0QnlSb2xlT3JNZW1iZXJSZXF1ZXN0EjEKDGNvbW11bml0eV9p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 36) = "ZBgKIAEoCzITLnJvb3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEjEKEXJvbGVf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 37) = "b3JfbWVtYmVyX2lkGAsgASgLMhYucm9vdC5Sb2xlT3JNZW1iZXJVdWlkItIB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 38) = "ChRBY2Nlc3NSdWxlR2V0UmVxdWVzdBIxCgxjb21tdW5pdHlfaWQYCiABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 39) = "Ey5yb290LkNvbW11bml0eVV1aWRCBrpIA8gBARJMChtjaGFubmVsX29yX2No";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 40) = "YW5uZWxfZ3JvdXBfaWQYCyABKAsyHy5yb290LkNoYW5uZWxPckNoYW5uZWxH";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 41) = "cm91cFV1aWRCBrpIA8gBARI5ChFyb2xlX29yX21lbWJlcl9pZBgMIAEoCzIW";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 42) = "LnJvb3QuUm9sZU9yTWVtYmVyVXVpZEIGukgDyAEBQiaqAiNSb290QXBwLldl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray44<string>, string>(ref _003C_003Ey__InlineArray, 43) = "YkFwaS5TaGFyZWQuR3JwYy5SZXF1ZXN0c2IGcHJvdG8z";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray44<string>, string>(in _003C_003Ey__InlineArray, 44)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[4]
		{
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			PermissionReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[8]
		{
			new GeneratedClrTypeInfo(typeof(AccessRuleCreateRequest), AccessRuleCreateRequest.Parser, new string[5] { "Context", "CommunityId", "ChannelOrChannelGroupId", "RoleOrMemberId", "Overlay" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AccessRuleCreateRoleOrMemberRequest), AccessRuleCreateRoleOrMemberRequest.Parser, new string[2] { "RoleOrMemberId", "Overlay" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AccessRuleEditRequest), AccessRuleEditRequest.Parser, new string[5] { "Context", "CommunityId", "ChannelOrChannelGroupId", "RoleOrMemberId", "Overlay" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AccessRuleDeleteRequest), AccessRuleDeleteRequest.Parser, new string[4] { "Context", "CommunityId", "ChannelOrChannelGroupId", "RoleOrMemberId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AccessRuleUpdateRequest), AccessRuleUpdateRequest.Parser, new string[5] { "Context", "CommunityId", "Creates", "Edits", "Deletes" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AccessRuleListByChannelOrChannelGroupRequest), AccessRuleListByChannelOrChannelGroupRequest.Parser, new string[2] { "CommunityId", "ChannelOrChannelGroupId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AccessRuleListByRoleOrMemberRequest), AccessRuleListByRoleOrMemberRequest.Parser, new string[2] { "CommunityId", "RoleOrMemberId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AccessRuleGetRequest), AccessRuleGetRequest.Parser, new string[3] { "CommunityId", "ChannelOrChannelGroupId", "RoleOrMemberId" }, null, null, null, null)
		}));
	}
}
