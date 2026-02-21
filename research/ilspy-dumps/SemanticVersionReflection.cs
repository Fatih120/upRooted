using System;
using Google.Protobuf.Reflection;

namespace RootApp.Core;

public static class SemanticVersionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static SemanticVersionReflection()
	{
		byte[] array = Convert.FromBase64String("Chtjb3JlL3NlbWFudGljX3ZlcnNpb24ucHJvdG8SBHJvb3QiPgoPU2VtYW50" + "aWNWZXJzaW9uEg0KBW1ham9yGBUgASgNEg0KBW1pbm9yGBYgASgNEg0KBXBh" + "dGNoGBcgASgNQg+qAgxSb290QXBwLkNvcmViBnByb3RvMw==");
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[1]
		{
			new GeneratedClrTypeInfo(typeof(SemanticVersion), SemanticVersion.Parser, new string[3] { "Major", "Minor", "Patch" }, null, null, null, null)
		}));
	}
}
