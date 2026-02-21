using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class CommunityMemberReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CommunityMemberReflection()
	{
		_003C_003Ey__InlineArray23<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray23<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiZ3ZWJhcGkvcmVxdWVzdHMvY29tbXVuaXR5X21lbWJlci5wcm90bxIEcm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 1) = "dBoVY29yZS9yb290X3V1aWRzLnByb3RvGhR3ZWJhcGkvY29udGV4dC5wcm90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 2) = "bxoTY29yZS92YWxpZGF0ZS5wcm90byKuAQoaQ29tbXVuaXR5TWVtYmVyRWRp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 3) = "dFJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 4) = "MQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 5) = "SAPIAQESJwoHdXNlcl9pZBgLIAEoCzIOLnJvb3QuVXNlclV1aWRCBrpIA8gB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 6) = "ARIQCghuaWNrbmFtZRgMIAEoCSKtAQoaQ29tbXVuaXR5TWVtYmVyTW92ZVJl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 7) = "cXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQSMQoM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 8) = "Y29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 9) = "AQESOAoTYmVmb3JlX2NvbW11bml0eV9pZBgMIAEoCzITLnJvb3QuQ29tbXVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 10) = "aXR5VXVpZEIGukgDyAEBIpcBCiFDb21tdW5pdHlNZW1iZXJTZXRGYXZvcml0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 11) = "ZVJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 12) = "MQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 13) = "SAPIAQESGwoLaXNfZmF2b3JpdGUYDCABKAhCBrpIA8gBASJ3ChlDb21tdW5p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 14) = "dHlNZW1iZXJHZXRSZXF1ZXN0EjEKDGNvbW11bml0eV9pZBgKIAEoCzITLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 15) = "b3QuQ29tbXVuaXR5VXVpZEIGukgDyAEBEicKB3VzZXJfaWQYCyABKAsyDi5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 16) = "b290LlVzZXJVdWlkQga6SAPIAQEieQoaQ29tbXVuaXR5TWVtYmVyTGlzdFJl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 17) = "cXVlc3QSMQoMY29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 18) = "dWlkQga6SAPIAQESKAoIdXNlcl9pZHMYCyADKAsyDi5yb290LlVzZXJVdWlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 19) = "Qga6SAPIAQEiUgodQ29tbXVuaXR5TWVtYmVyTGlzdEFsbFJlcXVlc3QSMQoM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 20) = "Y29tbXVuaXR5X2lkGAogASgLMhMucm9vdC5Db21tdW5pdHlVdWlkQga6SAPI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 21) = "AQFCJqoCI1Jvb3RBcHAuV2ViQXBpLlNoYXJlZC5HcnBjLlJlcXVlc3RzYgZw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray23<string>, string>(ref _003C_003Ey__InlineArray, 22) = "cm90bzM=";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray23<string>, string>(in _003C_003Ey__InlineArray, 23)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[3]
		{
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(CommunityMemberEditRequest), CommunityMemberEditRequest.Parser, new string[4] { "Context", "CommunityId", "UserId", "Nickname" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberMoveRequest), CommunityMemberMoveRequest.Parser, new string[3] { "Context", "CommunityId", "BeforeCommunityId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberSetFavoriteRequest), CommunityMemberSetFavoriteRequest.Parser, new string[3] { "Context", "CommunityId", "IsFavorite" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberGetRequest), CommunityMemberGetRequest.Parser, new string[2] { "CommunityId", "UserId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberListRequest), CommunityMemberListRequest.Parser, new string[2] { "CommunityId", "UserIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CommunityMemberListAllRequest), CommunityMemberListAllRequest.Parser, new string[1] { "CommunityId" }, null, null, null, null)
		}));
	}
}
