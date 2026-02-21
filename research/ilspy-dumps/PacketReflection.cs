using System;
using Google.Protobuf.Reflection;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Packets;

public static class PacketReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static PacketReflection()
	{
		_003C_003Ey__InlineArray135<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray135<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Cht3ZWJhcGkvcGFja2V0cy9wYWNrZXQucHJvdG8SBHJvb3QaEndlYmFwaS9l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 1) = "bnVtcy5wcm90bxoad2ViYXBpL3BhY2tldHMvYXNzZXQucHJvdG8aHHdlYmFw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 2) = "aS9wYWNrZXRzL2NoYW5uZWwucHJvdG8aIndlYmFwaS9wYWNrZXRzL2NoYW5u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 3) = "ZWxfZ3JvdXAucHJvdG8aHndlYmFwaS9wYWNrZXRzL2NvbW11bml0eS5wcm90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 4) = "bxoid2ViYXBpL3BhY2tldHMvY29tbXVuaXR5X2FwcC5wcm90bxojd2ViYXBp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 5) = "L3BhY2tldHMvZGlyZWN0X21lc3NhZ2UucHJvdG8aHndlYmFwaS9wYWNrZXRz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 6) = "L2RpcmVjdG9yeS5wcm90bxoZd2ViYXBpL3BhY2tldHMvZmlsZS5wcm90bxof";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 7) = "d2ViYXBpL3BhY2tldHMvZnJpZW5kc2hpcC5wcm90bxocd2ViYXBpL3BhY2tl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 8) = "dHMvbWVzc2FnZS5wcm90bxoid2ViYXBpL3BhY2tldHMvbm90aWZpY2F0aW9u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 9) = "cy5wcm90bxoZd2ViYXBpL3BhY2tldHMvcGluZy5wcm90bxoZd2ViYXBpL3Bh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 10) = "Y2tldHMvdXNlci5wcm90bxocd2ViYXBpL3BhY2tldHMvd2ViX3J0Yy5wcm90";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 11) = "byKyKQoPUGFja2V0Q29udGFpbmVyEiAKBHBpbmcYASABKAsyEC5yb290LlBp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 12) = "bmdQYWNrZXRIABI0Cg9odWJfc2VydmVyX21vdmUYAiABKAsyGS5yb290Lkh1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 13) = "YlNlcnZlck1vdmVQYWNrZXRIABI1Cg9jaGFubmVsX2NyZWF0ZWQYHiABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 14) = "Gi5yb290LkNoYW5uZWxDcmVhdGVkUGFja2V0SAASMwoOY2hhbm5lbF9lZGl0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 15) = "ZWQYHyABKAsyGS5yb290LkNoYW5uZWxFZGl0ZWRQYWNrZXRIABIxCg1jaGFu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 16) = "bmVsX21vdmVkGCAgASgLMhgucm9vdC5DaGFubmVsTW92ZWRQYWNrZXRIABI1";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 17) = "Cg9jaGFubmVsX2RlbGV0ZWQYISABKAsyGi5yb290LkNoYW5uZWxEZWxldGVk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 18) = "UGFja2V0SAASQAoVY2hhbm5lbF9ncm91cF9jcmVhdGVkGCggASgLMh8ucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 19) = "dC5DaGFubmVsR3JvdXBDcmVhdGVkUGFja2V0SAASPgoUY2hhbm5lbF9ncm91";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 20) = "cF9lZGl0ZWQYKSABKAsyHi5yb290LkNoYW5uZWxHcm91cEVkaXRlZFBhY2tl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 21) = "dEgAEjwKE2NoYW5uZWxfZ3JvdXBfbW92ZWQYKiABKAsyHS5yb290LkNoYW5u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 22) = "ZWxHcm91cE1vdmVkUGFja2V0SAASQAoVY2hhbm5lbF9ncm91cF9kZWxldGVk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 23) = "GCsgASgLMh8ucm9vdC5DaGFubmVsR3JvdXBEZWxldGVkUGFja2V0SAASKgoJ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 24) = "Y29tbXVuaXR5GDIgASgLMhUucm9vdC5Db21tdW5pdHlQYWNrZXRIABI3ChBj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 25) = "b21tdW5pdHlfam9pbmVkGDMgASgLMhsucm9vdC5Db21tdW5pdHlKb2luZWRQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 26) = "YWNrZXRIABI1Cg9jb21tdW5pdHlfbGVhdmUYNCABKAsyGi5yb290LkNvbW11";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 27) = "bml0eUxlYXZlUGFja2V0SAASOQoRY29tbXVuaXR5X2RlbGV0ZWQYNSABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 28) = "HC5yb290LkNvbW11bml0eURlbGV0ZWRQYWNrZXRIABJEChdjb21tdW5pdHlf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 29) = "bWVtYmVyX2F0dGFjaBg8IAEoCzIhLnJvb3QuQ29tbXVuaXR5TWVtYmVyQXR0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 30) = "YWNoUGFja2V0SAASRAoXY29tbXVuaXR5X21lbWJlcl9kZXRhY2gYPSABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 31) = "IS5yb290LkNvbW11bml0eU1lbWJlckRldGFjaFBhY2tldEgAEkQKF2NvbW11";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 32) = "bml0eV9tZW1iZXJfZWRpdGVkGD4gASgLMiEucm9vdC5Db21tdW5pdHlNZW1i";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 33) = "ZXJFZGl0ZWRQYWNrZXRIABJVCiBjb21tdW5pdHlfbWVtYmVyX2VkaXRlZF9l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 34) = "eHRlcm5hbBg/IAEoCzIpLnJvb3QuQ29tbXVuaXR5TWVtYmVyRWRpdGVkRXh0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 35) = "ZXJuYWxQYWNrZXRIABJNChxjb21tdW5pdHlfbWVtYmVyX2Jhbl9jcmVhdGVk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 36) = "GEYgASgLMiUucm9vdC5Db21tdW5pdHlNZW1iZXJCYW5DcmVhdGVkUGFja2V0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 37) = "SAASTQocY29tbXVuaXR5X21lbWJlcl9iYW5fZGVsZXRlZBhHIAEoCzIlLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 38) = "b3QuQ29tbXVuaXR5TWVtYmVyQmFuRGVsZXRlZFBhY2tldEgAEk8KHWNvbW11";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 39) = "bml0eV9tZW1iZXJfcm9sZV9jcmVhdGVkGFAgASgLMiYucm9vdC5Db21tdW5p";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 40) = "dHlNZW1iZXJSb2xlQ3JlYXRlZFBhY2tldEgAElYKIWNvbW11bml0eV9tZW1i";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 41) = "ZXJfcm9sZV9zZXRfcHJpbWFyeRhRIAEoCzIpLnJvb3QuQ29tbXVuaXR5TWVt";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 42) = "YmVyUm9sZVNldFByaW1hcnlQYWNrZXRIABJPCh1jb21tdW5pdHlfbWVtYmVy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 43) = "X3JvbGVfZGVsZXRlZBhSIAEoCzImLnJvb3QuQ29tbXVuaXR5TWVtYmVyUm9s";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 44) = "ZURlbGV0ZWRQYWNrZXRIABIzCg5jb21tdW5pdHlfcm9sZRhaIAEoCzIZLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 45) = "b3QuQ29tbXVuaXR5Um9sZVBhY2tldEgAEkIKFmNvbW11bml0eV9yb2xlX2Rl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 46) = "bGV0ZWQYWyABKAsyIC5yb290LkNvbW11bml0eVJvbGVEZWxldGVkUGFja2V0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 47) = "SAASPgoUY29tbXVuaXR5X3JvbGVfbW92ZWQYXCABKAsyHi5yb290LkNvbW11";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 48) = "bml0eVJvbGVNb3ZlZFBhY2tldEgAEkwKG2NvbW11bml0eV9wZXJtaXNzaW9u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 49) = "X3VwZGF0ZRhkIAEoCzIlLnJvb3QuQ29tbXVuaXR5UGVybWlzc2lvblVwZGF0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 50) = "ZVBhY2tldEgAEjwKE2NvbW11bml0eV9hcHBfYWRkZWQYbiABKAsyHS5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 51) = "LkNvbW11bml0eUFwcEFkZGVkUGFja2V0SAASQAoVY29tbXVuaXR5X2FwcF9y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 52) = "ZW1vdmVkGG8gASgLMh8ucm9vdC5Db21tdW5pdHlBcHBSZW1vdmVkUGFja2V0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 53) = "SAASRQoYY29tbXVuaXR5X2FwcF9zZXRfc3RhdHVzGHAgASgLMiEucm9vdC5D";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 54) = "b21tdW5pdHlBcHBTZXRTdGF0dXNQYWNrZXRIABJJChpjb21tdW5pdHlfYXBw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 55) = "X3NldF9zZXR0aW5ncxhxIAEoCzIjLnJvb3QuQ29tbXVuaXR5QXBwU2V0U2V0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 56) = "dGluZ3NQYWNrZXRIABJmCiljb21tdW5pdHlfYXBwX3ZlcnNpb25fdXBkYXRl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 57) = "X25vdGlmaWNhdGlvbhhyIAEoCzIxLnJvb3QuQ29tbXVuaXR5QXBwVmVyc2lv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 58) = "blVwZGF0ZU5vdGlmaWNhdGlvblBhY2tldEgAEkUKGGNvbW11bml0eV9hcHBf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 59) = "c2V0X2J1dHRvbhhzIAEoCzIhLnJvb3QuQ29tbXVuaXR5QXBwU2V0QnV0dG9u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 60) = "UGFja2V0SAASQgoWZGlyZWN0X21lc3NhZ2VfY3JlYXRlZBh4IAEoCzIgLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 61) = "b3QuRGlyZWN0TWVzc2FnZUNyZWF0ZWRQYWNrZXRIABJLChtkaXJlY3RfbWVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 62) = "c2FnZV9tZW1iZXJfYWRkZWQYeSABKAsyJC5yb290LkRpcmVjdE1lc3NhZ2VN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 63) = "ZW1iZXJBZGRlZFBhY2tldEgAEk8KHWRpcmVjdF9tZXNzYWdlX21lbWJlcl9k";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 64) = "ZWxldGVkGHogASgLMiYucm9vdC5EaXJlY3RNZXNzYWdlTWVtYmVyRGVsZXRl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 65) = "ZFBhY2tldEgAEjwKE2RpcmVjdF9tZXNzYWdlX3JpbmcYeyABKAsyHS5yb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 66) = "LkRpcmVjdE1lc3NhZ2VSaW5nUGFja2V0SAASTQocZGlyZWN0X21lc3NhZ2Vf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 67) = "cmluZ19kZWNsaW5lZBh8IAEoCzIlLnJvb3QuRGlyZWN0TWVzc2FnZVJpbmdE";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 68) = "ZWNsaW5lZFBhY2tldEgAEloKI2RpcmVjdF9tZXNzYWdlX2xhc3RfbWVzc2Fn";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 69) = "ZV9kZWxldGVkGH0gASgLMisucm9vdC5EaXJlY3RNZXNzYWdlTGFzdE1lc3Nh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 70) = "Z2VEZWxldGVkUGFja2V0SAASKwoJZGlyZWN0b3J5GIIBIAEoCzIVLnJvb3Qu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 71) = "RGlyZWN0b3J5UGFja2V0SAASNgoPZGlyZWN0b3J5X21vdmVkGIMBIAEoCzIa";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 72) = "LnJvb3QuRGlyZWN0b3J5TW92ZWRQYWNrZXRIABI6ChFkaXJlY3RvcnlfZGVs";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 73) = "ZXRlZBiEASABKAsyHC5yb290LkRpcmVjdG9yeURlbGV0ZWRQYWNrZXRIABIw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 74) = "CgxmaWxlX2NyZWF0ZWQYjAEgASgLMhcucm9vdC5GaWxlQ3JlYXRlZFBhY2tl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 75) = "dEgAEi4KC2ZpbGVfZWRpdGVkGI0BIAEoCzIWLnJvb3QuRmlsZUVkaXRlZFBh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 76) = "Y2tldEgAEiwKCmZpbGVfbW92ZWQYjgEgASgLMhUucm9vdC5GaWxlTW92ZWRQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 77) = "YWNrZXRIABIwCgxmaWxlX2RlbGV0ZWQYjwEgASgLMhcucm9vdC5GaWxlRGVs";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 78) = "ZXRlZFBhY2tldEgAEjwKEmZyaWVuZHNoaXBfY3JlYXRlZBiWASABKAsyHS5y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 79) = "b290LkZyaWVuZHNoaXBDcmVhdGVkUGFja2V0SAASOAoQZnJpZW5kc2hpcF9t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 80) = "b3ZlZBiXASABKAsyGy5yb290LkZyaWVuZHNoaXBNb3ZlZFBhY2tldEgAEjwK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 81) = "EmZyaWVuZHNoaXBfZGVsZXRlZBiYASABKAsyHS5yb290LkZyaWVuZHNoaXBE";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 82) = "ZWxldGVkUGFja2V0SAASRwoYZnJpZW5kc2hpcF9ncm91cF9jcmVhdGVkGKAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 83) = "IAEoCzIiLnJvb3QuRnJpZW5kc2hpcEdyb3VwQ3JlYXRlZFBhY2tldEgAEkUK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 84) = "F2ZyaWVuZHNoaXBfZ3JvdXBfZWRpdGVkGKEBIAEoCzIhLnJvb3QuRnJpZW5k";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 85) = "c2hpcEdyb3VwRWRpdGVkUGFja2V0SAASQwoWZnJpZW5kc2hpcF9ncm91cF9t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 86) = "b3ZlZBiiASABKAsyIC5yb290LkZyaWVuZHNoaXBHcm91cE1vdmVkUGFja2V0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 87) = "SAASRwoYZnJpZW5kc2hpcF9ncm91cF9kZWxldGVkGKMBIAEoCzIiLnJvb3Qu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 88) = "RnJpZW5kc2hpcEdyb3VwRGVsZXRlZFBhY2tldEgAEicKB21lc3NhZ2UYqgEg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 89) = "ASgLMhMucm9vdC5NZXNzYWdlUGFja2V0SAASNgoPbWVzc2FnZV9kZWxldGVk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 90) = "GKsBIAEoCzIaLnJvb3QuTWVzc2FnZURlbGV0ZWRQYWNrZXRIABIuCgttZXNz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 91) = "YWdlX3BpbhisASABKAsyFi5yb290Lk1lc3NhZ2VQaW5QYWNrZXRIABI4ChBt";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 92) = "ZXNzYWdlX3JlYWN0aW9uGK0BIAEoCzIbLnJvb3QuTWVzc2FnZVJlYWN0aW9u";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 93) = "UGFja2V0SAASTgocbWVzc2FnZV9zZXRfdHlwaW5nX2luZGljYXRvchiuASAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 94) = "KAsyJS5yb290Lk1lc3NhZ2VTZXRUeXBpbmdJbmRpY2F0b3JQYWNrZXRIABJA";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 95) = "ChVtZXNzYWdlX3NldF92aWV3X3RpbWUYrwEgASgLMh4ucm9vdC5NZXNzYWdl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 96) = "U2V0Vmlld1RpbWVQYWNrZXRIABIxCgxub3RpZmljYXRpb24YtAEgASgLMhgu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 97) = "cm9vdC5Ob3RpZmljYXRpb25QYWNrZXRIABI+ChNub3RpZmljYXRpb25fdmll";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 98) = "d2VkGLUBIAEoCzIeLnJvb3QuTm90aWZpY2F0aW9uVmlld2VkUGFja2V0SAAS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 99) = "RQoXbm90aWZpY2F0aW9uX3ZpZXdlZF9hbGwYtgEgASgLMiEucm9vdC5Ob3Rp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 100) = "ZmljYXRpb25WaWV3ZWRBbGxQYWNrZXRIABJAChRub3RpZmljYXRpb25fZGVs";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 101) = "ZXRlZBi3ASABKAsyHy5yb290Lk5vdGlmaWNhdGlvbkRlbGV0ZWRQYWNrZXRI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 102) = "ABJHChhub3RpZmljYXRpb25fZGVsZXRlZF9hbGwYuAEgASgLMiIucm9vdC5O";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 103) = "b3RpZmljYXRpb25EZWxldGVkQWxsUGFja2V0SAASNwoQdXNlcl9zZXRfcHJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 104) = "ZmlsZRi+ASABKAsyGi5yb290LlVzZXJTZXRQcm9maWxlUGFja2V0SAASNQoP";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 105) = "dXNlcl9zZXRfc3RhdHVzGL8BIAEoCzIZLnJvb3QuVXNlclNldFN0YXR1c1Bh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 106) = "Y2tldEgAEkwKG3VzZXJfc2V0X2VtYWlsX3ZlcmlmaWNhdGlvbhjAASABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 107) = "JC5yb290LlVzZXJTZXRFbWFpbFZlcmlmaWNhdGlvblBhY2tldEgAEjwKE3Vz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 108) = "ZXJfc2V0X21heF9zdGF0dXMYwQEgASgLMhwucm9vdC5Vc2VyU2V0TWF4U3Rh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 109) = "dHVzUGFja2V0SAASMAoMdXNlcl9kZWxldGVkGMIBIAEoCzIXLnJvb3QuVXNl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 110) = "ckRlbGV0ZWRQYWNrZXRIABI1Cg91c2VyX3NldF9iYWRnZXMYwwEgASgLMhku";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 111) = "cm9vdC5Vc2VyU2V0QmFkZ2VzUGFja2V0SAASaAoqdXNlcl9zZXRfZGlyZWN0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 112) = "X21lc3NhZ2VfaW52aXRlX3JlcXVpcmVtZW50GMQBIAEoCzIxLnJvb3QuVXNl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 113) = "clNldERpcmVjdE1lc3NhZ2VJbnZpdGVSZXF1aXJlbWVudFBhY2tldEgAEmEK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 114) = "JnVzZXJfc2V0X2ZyaWVuZHNoaXBfaW52aXRlX3JlcXVpcmVtZW50GMUBIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 115) = "CzIuLnJvb3QuVXNlclNldEZyaWVuZHNoaXBJbnZpdGVSZXF1aXJlbWVudFBh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 116) = "Y2tldEgAEjwKE3dlYl9ydGNfdXNlcl9kZXZpY2UYyAEgASgLMhwucm9vdC5X";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 117) = "ZWJSdGNVc2VyRGV2aWNlUGFja2V0SAASPAoTd2ViX3J0Y191c2VyX2RldGFj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 118) = "aBjJASABKAsyHC5yb290LldlYlJ0Y1VzZXJEZXRhY2hQYWNrZXRIABJQCh53";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 119) = "ZWJfcnRjX3VzZXJfZGV2aWNlX3NldF9zdGF0dXMYygEgASgLMiUucm9vdC5X";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 120) = "ZWJSdGNVc2VyRGV2aWNlU2V0U3RhdHVzUGFja2V0SAASVgohd2ViX3J0Y191";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 121) = "c2VyX2RldmljZV9zZXRfdHJhbnNwb3J0GMsBIAEoCzIoLnJvb3QuV2ViUnRj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 122) = "VXNlckRldmljZVNldFRyYW5zcG9ydFBhY2tldEgAElsKJHdlYl9ydGNfdXNl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 123) = "cl9kZXZpY2Vfc2V0X2RhdGFfY2hhbm5lbBjMASABKAsyKi5yb290LldlYlJ0";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 124) = "Y1VzZXJEZXZpY2VTZXREYXRhQ2hhbm5lbFBhY2tldEgAEjsKEnVzZXJfYmxv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 125) = "Y2tfY3JlYXRlZBjSASABKAsyHC5yb290LlVzZXJCbG9ja0NyZWF0ZWRQYWNr";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 126) = "ZXRIABI7ChJ1c2VyX2Jsb2NrX2RlbGV0ZWQY0wEgASgLMhwucm9vdC5Vc2Vy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 127) = "QmxvY2tEZWxldGVkUGFja2V0SAASMgoNYXNzZXRfY2hhbmdlZBjcASABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 128) = "GC5yb290LkFzc2V0Q2hhbmdlZFBhY2tldEgAQggKBnBhY2tldCJ/ChJDbGll";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 129) = "bnROb3RpZmljYXRpb24SKQoKZXJyb3JfY29kZRgBIAEoDjIVLnJvb3QuUGFj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 130) = "a2V0RXJyb3JDb2RlEhcKD3NlcXVlbmNlX251bWJlchgCIAEoAxIlCgZwYWNr";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 131) = "ZXQYAyABKAsyFS5yb290LlBhY2tldENvbnRhaW5lciJWCg9IdWJOb3RpZmlj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 132) = "YXRpb24SFwoPc2VxdWVuY2VfbnVtYmVyGAIgASgDEiAKBHBpbmcYBCABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 133) = "EC5yb290LlBpbmdQYWNrZXRIAEIICgZwYWNrZXRCIKoCHVJvb3RBcHAuV2Vi";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray135<string>, string>(ref _003C_003Ey__InlineArray, 134) = "QXBpLlNoYXJlZC5QYWNrZXRzYgZwcm90bzM=";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray135<string>, string>(in _003C_003Ey__InlineArray, 135)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[15]
		{
			EnumsReflection.Descriptor,
			AssetReflection.Descriptor,
			ChannelReflection.Descriptor,
			ChannelGroupReflection.Descriptor,
			CommunityReflection.Descriptor,
			CommunityAppReflection.Descriptor,
			DirectMessageReflection.Descriptor,
			DirectoryReflection.Descriptor,
			FileReflection.Descriptor,
			FriendshipReflection.Descriptor,
			MessageReflection.Descriptor,
			NotificationsReflection.Descriptor,
			PingReflection.Descriptor,
			UserReflection.Descriptor,
			WebRtcReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(PacketContainer), PacketContainer.Parser, new string[80]
			{
				"Ping", "HubServerMove", "ChannelCreated", "ChannelEdited", "ChannelMoved", "ChannelDeleted", "ChannelGroupCreated", "ChannelGroupEdited", "ChannelGroupMoved", "ChannelGroupDeleted",
				"Community", "CommunityJoined", "CommunityLeave", "CommunityDeleted", "CommunityMemberAttach", "CommunityMemberDetach", "CommunityMemberEdited", "CommunityMemberEditedExternal", "CommunityMemberBanCreated", "CommunityMemberBanDeleted",
				"CommunityMemberRoleCreated", "CommunityMemberRoleSetPrimary", "CommunityMemberRoleDeleted", "CommunityRole", "CommunityRoleDeleted", "CommunityRoleMoved", "CommunityPermissionUpdate", "CommunityAppAdded", "CommunityAppRemoved", "CommunityAppSetStatus",
				"CommunityAppSetSettings", "CommunityAppVersionUpdateNotification", "CommunityAppSetButton", "DirectMessageCreated", "DirectMessageMemberAdded", "DirectMessageMemberDeleted", "DirectMessageRing", "DirectMessageRingDeclined", "DirectMessageLastMessageDeleted", "Directory",
				"DirectoryMoved", "DirectoryDeleted", "FileCreated", "FileEdited", "FileMoved", "FileDeleted", "FriendshipCreated", "FriendshipMoved", "FriendshipDeleted", "FriendshipGroupCreated",
				"FriendshipGroupEdited", "FriendshipGroupMoved", "FriendshipGroupDeleted", "Message", "MessageDeleted", "MessagePin", "MessageReaction", "MessageSetTypingIndicator", "MessageSetViewTime", "Notification",
				"NotificationViewed", "NotificationViewedAll", "NotificationDeleted", "NotificationDeletedAll", "UserSetProfile", "UserSetStatus", "UserSetEmailVerification", "UserSetMaxStatus", "UserDeleted", "UserSetBadges",
				"UserSetDirectMessageInviteRequirement", "UserSetFriendshipInviteRequirement", "WebRtcUserDevice", "WebRtcUserDetach", "WebRtcUserDeviceSetStatus", "WebRtcUserDeviceSetTransport", "WebRtcUserDeviceSetDataChannel", "UserBlockCreated", "UserBlockDeleted", "AssetChanged"
			}, new string[1] { "Packet" }, null, null, null),
			new GeneratedClrTypeInfo(typeof(ClientNotification), ClientNotification.Parser, new string[3] { "ErrorCode", "SequenceNumber", "Packet" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(HubNotification), HubNotification.Parser, new string[2] { "SequenceNumber", "Ping" }, new string[1] { "Packet" }, null, null, null)
		}));
	}
}
