using System;
using Google.Protobuf.Reflection;
using RootApp.Core;
using RootApp.Core.Validate;

namespace RootApp.WebApi.Shared.Grpc.Requests;

public static class NotificationReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static NotificationReflection()
	{
		_003C_003Ey__InlineArray18<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray18<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiJ3ZWJhcGkvcmVxdWVzdHMvbm90aWZpY2F0aW9uLnByb3RvEgRyb290GhVj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 1) = "b3JlL3Jvb3RfdXVpZHMucHJvdG8aFHdlYmFwaS9jb250ZXh0LnByb3RvGhNj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 2) = "b3JlL3ZhbGlkYXRlLnByb3RvIjUKE05vdGlmaWNhdGlvblJlcXVlc3QSHgoG";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 3) = "YmVmb3JlGAogASgLMg4ucm9vdC5Sb290VXVpZCJICiBOb3RpZmljYXRpb25D";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 4) = "b3VudFVudmlld2VkUmVxdWVzdBIkCgxjb250YWluZXJfaWQYCiABKAsyDi5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 5) = "b290LlJvb3RVdWlkIm4KHE5vdGlmaWNhdGlvblNldFZpZXdlZFJlcXVlc3QS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 6) = "IgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9vdENvbnRleHQSKgoCaWQYBSAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 7) = "KAsyFi5yb290Lk5vdGlmaWNhdGlvblV1aWRCBrpIA8gBASKjAQolTm90aWZp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 8) = "Y2F0aW9uU2V0Q29udGFpbmVyVmlld2VkUmVxdWVzdBIiCgdjb250ZXh0GAEg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 9) = "ASgLMhEucm9vdC5Sb290Q29udGV4dBIsCgxjb250YWluZXJfaWQYCiABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 10) = "Di5yb290LlJvb3RVdWlkQga6SAPIAQESKAoQc3ViX2NvbnRhaW5lcl9pZBgL";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 11) = "IAEoCzIOLnJvb3QuUm9vdFV1aWQiIQofTm90aWZpY2F0aW9uU2V0QWxsVmll";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 12) = "d2VkUmVxdWVzdCJCChxOb3RpZmljYXRpb25EZWxldGVBbGxSZXF1ZXN0EiIK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 13) = "B2NvbnRleHQYASABKAsyES5yb290LlJvb3RDb250ZXh0ImsKGU5vdGlmaWNh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 14) = "dGlvbkRlbGV0ZVJlcXVlc3QSIgoHY29udGV4dBgBIAEoCzIRLnJvb3QuUm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 15) = "dENvbnRleHQSKgoCaWQYBSABKAsyFi5yb290Lk5vdGlmaWNhdGlvblV1aWRC";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 16) = "BrpIA8gBAUImqgIjUm9vdEFwcC5XZWJBcGkuU2hhcmVkLkdycGMuUmVxdWVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray18<string>, string>(ref _003C_003Ey__InlineArray, 17) = "dHNiBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray18<string>, string>(in _003C_003Ey__InlineArray, 18)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[3]
		{
			RootUuidsReflection.Descriptor,
			ContextReflection.Descriptor,
			ValidateReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[7]
		{
			new GeneratedClrTypeInfo(typeof(NotificationRequest), NotificationRequest.Parser, new string[1] { "Before" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NotificationCountUnviewedRequest), NotificationCountUnviewedRequest.Parser, new string[1] { "ContainerId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NotificationSetViewedRequest), NotificationSetViewedRequest.Parser, new string[2] { "Context", "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NotificationSetContainerViewedRequest), NotificationSetContainerViewedRequest.Parser, new string[3] { "Context", "ContainerId", "SubContainerId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NotificationSetAllViewedRequest), NotificationSetAllViewedRequest.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NotificationDeleteAllRequest), NotificationDeleteAllRequest.Parser, new string[1] { "Context" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NotificationDeleteRequest), NotificationDeleteRequest.Parser, new string[2] { "Context", "Id" }, null, null, null, null)
		}));
	}
}
