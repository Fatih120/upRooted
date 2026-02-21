using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Enums;

namespace RootApp.WebApi.Shared.Grpc;

public static class ContextReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ContextReflection()
	{
		_003C_003Ey__InlineArray24<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray24<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Ch1pbnRlcm5hbC93ZWJhcGkvY29udGV4dC5wcm90bxIEcm9vdBoQY29yZS9l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 1) = "bnVtcy5wcm90bxoVY29yZS9yb290X3V1aWRzLnByb3RvIsoBCgtSb290Q29u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 2) = "dGV4dBIvCg5jb21tYW5kX3RhcmdldBgBIAEoCzIXLnJvb3QuVXNlckNvbW1h";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 3) = "bmRUYXJnZXQSMwoSY29tbXVuaXR5X2lkZW50aXR5GAIgASgLMhcucm9vdC5D";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 4) = "b21tdW5pdHlJZGVudGl0eRIjCglkZXZpY2VfaWQYAyABKAsyEC5yb290LkRl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 5) = "dmljZVV1aWQSMAoKY29tbWFuZF9pZBgEIAEoCzIcLnJvb3QuQ29tbWFuZElk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 6) = "ZW1wb3RlbmN5VXVpZCJaChFVc2VyQ29tbWFuZFRhcmdldBIjCgR2ZXJiGAEg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 7) = "ASgOMhUucm9vdC5Vc2VyQ29tbWFuZFZlcmISIAoEdHlwZRgCIAEoDjISLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 8) = "b3QuUm9vdEd1aWRUeXBlIl8KEUNvbW11bml0eUlkZW50aXR5Eh8KB3VzZXJf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 9) = "aWQYASABKAsyDi5yb290LlVzZXJVdWlkEikKDGNvbW11bml0eV9pZBgCIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 10) = "CzITLnJvb3QuQ29tbXVuaXR5VXVpZCq0BAoPVXNlckNvbW1hbmRWZXJiEiEK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 11) = "HVVTRVJfQ09NTUFORF9WRVJCX1VOU1BFQ0lGSUVEEAASGQoVVVNFUl9DT01N";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 12) = "QU5EX1ZFUkJfR0VUEAESHAoYVVNFUl9DT01NQU5EX1ZFUkJfQ1JFQVRFEAIS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 13) = "GgoWVVNFUl9DT01NQU5EX1ZFUkJfRURJVBADEhwKGFVTRVJfQ09NTUFORF9W";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 14) = "RVJCX0RFTEVURRAEEhoKFlVTRVJfQ09NTUFORF9WRVJCX0xJU1QQBRInCiNV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 15) = "U0VSX0NPTU1BTkRfVkVSQl9UQVJHRVRfU1BFQ0lGSUNfMRBlEicKI1VTRVJf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 16) = "Q09NTUFORF9WRVJCX1RBUkdFVF9TUEVDSUZJQ18yEGYSJwojVVNFUl9DT01N";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 17) = "QU5EX1ZFUkJfVEFSR0VUX1NQRUNJRklDXzMQZxInCiNVU0VSX0NPTU1BTkRf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 18) = "VkVSQl9UQVJHRVRfU1BFQ0lGSUNfNBBoEicKI1VTRVJfQ09NTUFORF9WRVJC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 19) = "X1RBUkdFVF9TUEVDSUZJQ181EGkSJwojVVNFUl9DT01NQU5EX1ZFUkJfVEFS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 20) = "R0VUX1NQRUNJRklDXzYQahInCiNVU0VSX0NPTU1BTkRfVkVSQl9UQVJHRVRf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 21) = "U1BFQ0lGSUNfNxBrEicKI1VTRVJfQ09NTUFORF9WRVJCX1RBUkdFVF9TUEVD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 22) = "SUZJQ184EGwSJwojVVNFUl9DT01NQU5EX1ZFUkJfVEFSR0VUX1NQRUNJRklD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray24<string>, string>(ref _003C_003Ey__InlineArray, 23) = "XzkQbUIdqgIaUm9vdEFwcC5XZWJBcGkuU2hhcmVkLkdycGNiBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray24<string>, string>(in _003C_003Ey__InlineArray, 24)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[2]
		{
			EnumsReflection.Descriptor,
			RootUuidsReflection.Descriptor
		}, new GeneratedClrTypeInfo(new Type[1] { typeof(UserCommandVerb) }, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(RootContext), RootContext.Parser, new string[4] { "CommandTarget", "CommunityIdentity", "DeviceId", "CommandId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UserCommandTarget), UserCommandTarget.Parser, new string[2] { "Verb", "Type" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityIdentity), CommunityIdentity.Parser, new string[2] { "UserId", "CommunityId" }, null, null, null, null)
		}));
	}
}
