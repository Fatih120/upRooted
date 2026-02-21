using System;
using Google.Protobuf.Reflection;

namespace RootApp.WebApi.Shared.Grpc;

public static class WebRtcReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static WebRtcReflection()
	{
		byte[] array = Convert.FromBase64String("ChR3ZWJhcGkvd2ViX3J0Yy5wcm90bxIEcm9vdCI1ChhXZWJSdGNTZXNzaW9u" + "RGVzY3JpcHRpb24SDAoEdHlwZRgEIAEoCRILCgNzZHAYBSABKAlCHaoCGlJv" + "b3RBcHAuV2ViQXBpLlNoYXJlZC5HcnBjYgZwcm90bzM=");
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[1]
		{
			new GeneratedClrTypeInfo(typeof(WebRtcSessionDescription), WebRtcSessionDescription.Parser, new string[2] { "Type", "Sdp" }, null, null, null, null)
		}));
	}
}
