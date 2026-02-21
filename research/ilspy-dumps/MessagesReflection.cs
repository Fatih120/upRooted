using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using RootApp.Core;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.WebApi.Shared.Payloads.Message;

public static class MessagesReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static MessagesReflection()
	{
		_003C_003Ey__InlineArray73<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray73<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 0) = "Ch53ZWJhcGkvcGF5bG9hZHMvbWVzc2FnZXMucHJvdG8SBHJvb3QaFWNvcmUv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 1) = "cm9vdF91dWlkcy5wcm90bxogZ29vZ2xlL3Byb3RvYnVmL2ZpZWxkX21hc2su";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 2) = "cHJvdG8aHmdvb2dsZS9wcm90b2J1Zi93cmFwcGVycy5wcm90bxoSd2ViYXBp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 3) = "L2VudW1zLnByb3RvIoUBCg5NZXNzYWdlUGF5bG9hZBIfCgd1c2VyX2lkGAQg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 4) = "ASgLMg4ucm9vdC5Vc2VyVXVpZBInCgVpdGVtcxgIIAMoCzIYLnJvb3QuTWVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 5) = "c2FnZVBheWxvYWRJdGVtEikKB3ByaW1hcnkYCSABKAsyGC5yb290Lk1lc3Nh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 6) = "Z2VQYXlsb2FkSXRlbSK9BgoSTWVzc2FnZVBheWxvYWRJdGVtEjIKCWNvbW11";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 7) = "bml0eRgEIAEoCzIdLnJvb3QuTWVzc2FnZVBheWxvYWRDb21tdW5pdHlIABIu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 8) = "CgdjaGFubmVsGAUgASgLMhsucm9vdC5NZXNzYWdlUGF5bG9hZENoYW5uZWxI";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 9) = "ABI7Cg5tZXNzYWdlX3Bpbm5lZBgHIAEoCzIhLnJvb3QuTWVzc2FnZVBheWxv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 10) = "YWRNZXNzYWdlUGlubmVkSAASQAoRbWVzc2FnZV91bl9waW5uZWQYCCABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 11) = "Iy5yb290Lk1lc3NhZ2VQYXlsb2FkTWVzc2FnZVVuUGlubmVkSAASTAoXY29t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 12) = "bXVuaXR5X21lbWJlcl9iYW5uZWQYCiABKAsyKS5yb290Lk1lc3NhZ2VQYXls";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 13) = "b2FkQ29tbXVuaXR5TWVtYmVyQmFubmVkSAASTAoXY29tbXVuaXR5X21lbWJl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 14) = "cl9qb2luZWQYCyABKAsyKS5yb290Lk1lc3NhZ2VQYXlsb2FkQ29tbXVuaXR5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 15) = "TWVtYmVySm9pbmVkSAASTAoXY29tbXVuaXR5X21lbWJlcl9raWNrZWQYDCAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 16) = "KAsyKS5yb290Lk1lc3NhZ2VQYXlsb2FkQ29tbXVuaXR5TWVtYmVyS2lja2Vk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 17) = "SAASWAogZGlyZWN0X21lc3NhZ2VfdXNlcl9jYWxsX3N0YXJ0ZWQYDiABKAsy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 18) = "LC5yb290Lk1lc3NhZ2VQYXlsb2FkRGlyZWN0TWVzc2FnZUNhbGxTdGFydGVk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 19) = "SAASVAoeZGlyZWN0X21lc3NhZ2VfdXNlcl9jYWxsX2VuZGVkGA8gASgLMiou";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 20) = "cm9vdC5NZXNzYWdlUGF5bG9hZERpcmVjdE1lc3NhZ2VDYWxsRW5kZWRIABJT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 21) = "ChtkaXJlY3RfbWVzc2FnZV91c2Vyc19qb2luZWQYECABKAsyLC5yb290Lk1l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 22) = "c3NhZ2VQYXlsb2FkRGlyZWN0TWVzc2FnZVVzZXJzSm9pbmVkSAASTQoYZGly";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 23) = "ZWN0X21lc3NhZ2VfdXNlcl9sZWZ0GBEgASgLMikucm9vdC5NZXNzYWdlUGF5";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 24) = "bG9hZERpcmVjdE1lc3NhZ2VVc2VyTGVmdEgAQgYKBGl0ZW0ikQIKF01lc3Nh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 25) = "Z2VQYXlsb2FkQ29tbXVuaXR5EjoKFm1lc3NhZ2VfcGF5bG9hZF9hY3Rpb24Y";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 26) = "ASABKA4yGi5yb290Lk1lc3NhZ2VQYXlsb2FkQWN0aW9uEh8KAmlkGAQgASgL";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 27) = "MhMucm9vdC5Db21tdW5pdHlVdWlkEjQKCG9yaWdpbmFsGAUgASgLMiIucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 28) = "dC5NZXNzYWdlUGF5bG9hZENvbW11bml0eVN0YXRlEjMKB2N1cnJlbnQYBiAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 29) = "KAsyIi5yb290Lk1lc3NhZ2VQYXlsb2FkQ29tbXVuaXR5U3RhdGUSLgoKZmll";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 30) = "bGRfbWFzaxgHIAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5GaWVsZE1hc2si6QIK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 31) = "HE1lc3NhZ2VQYXlsb2FkQ29tbXVuaXR5U3RhdGUSDAoEbmFtZRgBIAEoCRIt";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 32) = "ChJkZWZhdWx0X2NoYW5uZWxfaWQYBCABKAsyES5yb290LkNoYW5uZWxVdWlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 33) = "EiUKDW93bmVyX3VzZXJfaWQYBSABKAsyDi5yb290LlVzZXJVdWlkEhwKFGRl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 34) = "ZmF1bHRfY2hhbm5lbF9uYW1lGAYgASgJEhYKDm93bmVyX3VzZXJuYW1lGAcg";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 35) = "ASgJEhMKC3BpY3R1cmVfaGV4GAggASgJEjcKEXBpY3R1cmVfYXNzZXRfdXJp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 36) = "GAkgASgLMhwuZ29vZ2xlLnByb3RvYnVmLlN0cmluZ1ZhbHVlEh8KF3JlamVj";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 37) = "dF91bnZlcmlmaWVkX2VtYWlsGAogASgIEkAKDWpvaW5fdGhyb3R0bGUYCyAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 38) = "KAsyKS5yb290Lk1lc3NhZ2VQYXlsb2FkQ29tbXVuaXR5Sm9pblRocm90dGxl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 39) = "IlYKI01lc3NhZ2VQYXlsb2FkQ29tbXVuaXR5Sm9pblRocm90dGxlEhQKDHJl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 40) = "ZmlsbF9jb3VudBgFIAEoBRIZChF3aW5kb3dfaW5fbWludXRlcxgGIAEoBSKy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 41) = "AgoVTWVzc2FnZVBheWxvYWRDaGFubmVsEjoKFm1lc3NhZ2VfcGF5bG9hZF9h";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 42) = "Y3Rpb24YASABKA4yGi5yb290Lk1lc3NhZ2VQYXlsb2FkQWN0aW9uEh0KAmlk";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 43) = "GAQgASgLMhEucm9vdC5DaGFubmVsVXVpZBInCgxjaGFubmVsX3R5cGUYBiAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 44) = "KA4yES5yb290LkNoYW5uZWxUeXBlEjIKCG9yaWdpbmFsGAkgASgLMiAucm9v";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 45) = "dC5NZXNzYWdlUGF5bG9hZENoYW5uZWxTdGF0ZRIxCgdjdXJyZW50GAogASgL";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 46) = "MiAucm9vdC5NZXNzYWdlUGF5bG9hZENoYW5uZWxTdGF0ZRIuCgpmaWVsZF9t";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 47) = "YXNrGAsgASgLMhouZ29vZ2xlLnByb3RvYnVmLkZpZWxkTWFzayKNAQoaTWVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 48) = "c2FnZVBheWxvYWRDaGFubmVsU3RhdGUSMAoQY2hhbm5lbF9ncm91cF9pZBgE";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 49) = "IAEoCzIWLnJvb3QuQ2hhbm5lbEdyb3VwVXVpZBIaChJjaGFubmVsX2dyb3Vw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 50) = "X25hbWUYBSABKAkSDAoEbmFtZRgIIAEoCRITCgtkZXNjcmlwdGlvbhgJIAEo";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 51) = "CSKRAQobTWVzc2FnZVBheWxvYWRNZXNzYWdlUGlubmVkEiUKCm1lc3NhZ2Vf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 52) = "aWQYBCABKAsyES5yb290Lk1lc3NhZ2VVdWlkEjUKD21lc3NhZ2VfY29udGVu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 53) = "dBgFIAEoCzIcLmdvb2dsZS5wcm90b2J1Zi5TdHJpbmdWYWx1ZRIUCgxtZXNz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 54) = "YWdlX3VyaXMYBiADKAkikwEKHU1lc3NhZ2VQYXlsb2FkTWVzc2FnZVVuUGlu";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 55) = "bmVkEiUKCm1lc3NhZ2VfaWQYBCABKAsyES5yb290Lk1lc3NhZ2VVdWlkEjUK";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 56) = "D21lc3NhZ2VfY29udGVudBgFIAEoCzIcLmdvb2dsZS5wcm90b2J1Zi5TdHJp";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 57) = "bmdWYWx1ZRIUCgxtZXNzYWdlX3VyaXMYBiADKAkiWAojTWVzc2FnZVBheWxv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 58) = "YWRDb21tdW5pdHlNZW1iZXJKb2luZWQSHwoHdXNlcl9pZBgEIAEoCzIOLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 59) = "b3QuVXNlclV1aWQSEAoIdXNlcm5hbWUYBSABKAkiWAojTWVzc2FnZVBheWxv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 60) = "YWRDb21tdW5pdHlNZW1iZXJLaWNrZWQSHwoHdXNlcl9pZBgEIAEoCzIOLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 61) = "b3QuVXNlclV1aWQSEAoIdXNlcm5hbWUYBSABKAkiWAojTWVzc2FnZVBheWxv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 62) = "YWRDb21tdW5pdHlNZW1iZXJCYW5uZWQSHwoHdXNlcl9pZBgEIAEoCzIOLnJv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 63) = "b3QuVXNlclV1aWQSEAoIdXNlcm5hbWUYBSABKAkiRwoSTWVzc2FnZVBheWxv";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 64) = "YWRVc2VyEh8KB3VzZXJfaWQYBCABKAsyDi5yb290LlVzZXJVdWlkEhAKCHVz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 65) = "ZXJuYW1lGAUgASgJIigKJk1lc3NhZ2VQYXlsb2FkRGlyZWN0TWVzc2FnZUNh";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 66) = "bGxTdGFydGVkIlMKJE1lc3NhZ2VQYXlsb2FkRGlyZWN0TWVzc2FnZUNhbGxF";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 67) = "bmRlZBIrChBzdGFydF9tZXNzYWdlX2lkGAQgASgLMhEucm9vdC5NZXNzYWdl";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 68) = "VXVpZCJRCiZNZXNzYWdlUGF5bG9hZERpcmVjdE1lc3NhZ2VVc2Vyc0pvaW5l";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 69) = "ZBInCgV1c2VycxgEIAMoCzIYLnJvb3QuTWVzc2FnZVBheWxvYWRVc2VyIk0K";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 70) = "I01lc3NhZ2VQYXlsb2FkRGlyZWN0TWVzc2FnZVVzZXJMZWZ0EiYKBHVzZXIY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 71) = "BCABKAsyGC5yb290Lk1lc3NhZ2VQYXlsb2FkVXNlckIpqgImUm9vdEFwcC5X";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray73<string>, string>(ref _003C_003Ey__InlineArray, 72) = "ZWJBcGkuU2hhcmVkLlBheWxvYWRzLk1lc3NhZ2ViBnByb3RvMw==";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray73<string>, string>(in _003C_003Ey__InlineArray, 73)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[4]
		{
			RootUuidsReflection.Descriptor,
			FieldMaskReflection.Descriptor,
			WrappersReflection.Descriptor,
			EnumsReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[17]
		{
			new GeneratedClrTypeInfo(typeof(MessagePayload), MessagePayload.Parser, new string[3] { "UserId", "Items", "Primary" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadItem), MessagePayloadItem.Parser, new string[11]
			{
				"Community", "Channel", "MessagePinned", "MessageUnPinned", "CommunityMemberBanned", "CommunityMemberJoined", "CommunityMemberKicked", "DirectMessageUserCallStarted", "DirectMessageUserCallEnded", "DirectMessageUsersJoined",
				"DirectMessageUserLeft"
			}, new string[1] { "Item" }, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadCommunity), MessagePayloadCommunity.Parser, new string[5] { "MessagePayloadAction", "Id", "Original", "Current", "FieldMask" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadCommunityState), MessagePayloadCommunityState.Parser, new string[9] { "Name", "DefaultChannelId", "OwnerUserId", "DefaultChannelName", "OwnerUsername", "PictureHex", "PictureAssetUri", "RejectUnverifiedEmail", "JoinThrottle" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadCommunityJoinThrottle), MessagePayloadCommunityJoinThrottle.Parser, new string[2] { "RefillCount", "WindowInMinutes" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadChannel), MessagePayloadChannel.Parser, new string[6] { "MessagePayloadAction", "Id", "ChannelType", "Original", "Current", "FieldMask" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadChannelState), MessagePayloadChannelState.Parser, new string[4] { "ChannelGroupId", "ChannelGroupName", "Name", "Description" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadMessagePinned), MessagePayloadMessagePinned.Parser, new string[3] { "MessageId", "MessageContent", "MessageUris" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadMessageUnPinned), MessagePayloadMessageUnPinned.Parser, new string[3] { "MessageId", "MessageContent", "MessageUris" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadCommunityMemberJoined), MessagePayloadCommunityMemberJoined.Parser, new string[2] { "UserId", "Username" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadCommunityMemberKicked), MessagePayloadCommunityMemberKicked.Parser, new string[2] { "UserId", "Username" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadCommunityMemberBanned), MessagePayloadCommunityMemberBanned.Parser, new string[2] { "UserId", "Username" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadUser), MessagePayloadUser.Parser, new string[2] { "UserId", "Username" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadDirectMessageCallStarted), MessagePayloadDirectMessageCallStarted.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadDirectMessageCallEnded), MessagePayloadDirectMessageCallEnded.Parser, new string[1] { "StartMessageId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadDirectMessageUsersJoined), MessagePayloadDirectMessageUsersJoined.Parser, new string[1] { "Users" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MessagePayloadDirectMessageUserLeft), MessagePayloadDirectMessageUserLeft.Parser, new string[1] { "User" }, null, null, null, null)
		}));
	}
}
