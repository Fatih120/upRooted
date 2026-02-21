using System;
using Google.Protobuf.Reflection;

namespace RootApp.AppServices.Grpc;

public static class AppPermissionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static AppPermissionReflection()
	{
		byte[] array = Convert.FromBase64String("CiphcHBfc2VydmljZXMvc2VydmljZXMvYXBwX3Blcm1pc3Npb24ucHJvdG8S" + "CHJvb3QuYXBwIiQKDUFwcFBlcm1pc3Npb24SEwoLaGFzX25ldHdvcmsYASAB" + "KAhCG6oCGFJvb3RBcHAuQXBwU2VydmljZXMuR3JwY2IGcHJvdG8z");
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[1]
		{
			new GeneratedClrTypeInfo(typeof(AppPermission), AppPermission.Parser, new string[1] { "HasNetwork" }, null, null, null, null)
		}));
	}
}
