using System;
using Google.Protobuf.Reflection;

namespace RootApp.App;

public static class AppEnumsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static AppEnumsReflection()
	{
		_003C_003Ey__InlineArray30<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray30<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CiVhcHBfc2VydmljZXMvc2VydmljZXMvYXBwX2VudW1zLnByb3RvEghyb290";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 1) = "LmFwcCp1CglBcHBTdGF0dXMSGgoWQVBQX1NUQVRVU19VTlNQRUNJRklFRBAA";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 2) = "EhoKFkFQUF9TVEFUVVNfVU5QVUJMSVNIRUQQARIYChRBUFBfU1RBVFVTX1BV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 3) = "QkxJU0hFRBADEhYKEkFQUF9TVEFUVVNfQkxPQ0tFRBAEKoQCChBBcHBWZXJz";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 4) = "aW9uU3RhdHVzEiIKHkFQUF9WRVJTSU9OX1NUQVRVU19VTlNQRUNJRklFRBAA";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 5) = "EiEKHUFQUF9WRVJTSU9OX1NUQVRVU19VTlJFTEVBU0VEEAESIwofQVBQX1ZF";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 6) = "UlNJT05fU1RBVFVTX1VOREVSX1JFVklFVxAEEh8KG0FQUF9WRVJTSU9OX1NU";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 7) = "QVRVU19BUFBST1ZFRBAFEh8KG0FQUF9WRVJTSU9OX1NUQVRVU19SRUxFQVNF";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 8) = "RBAGEiEKHUFQUF9WRVJTSU9OX1NUQVRVU19ERVBSRUNBVEVEEAcSHwobQVBQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 9) = "X1ZFUlNJT05fU1RBVFVTX1JFSkVDVEVEEAgq7wEKE0FwcERlcGxveW1lbnRT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 10) = "dGF0dXMSJQohQVBQX0RFUExPWU1FTlRfU1RBVFVTX1VOU1BFQ0lGSUVEEAAS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 11) = "IQodQVBQX0RFUExPWU1FTlRfU1RBVFVTX1JVTk5JTkcQARIhCh1BUFBfREVQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 12) = "TE9ZTUVOVF9TVEFUVVNfU1RPUFBFRBACEiIKHkFQUF9ERVBMT1lNRU5UX1NU";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 13) = "QVRVU19TVEFSVElORxADEiIKHkFQUF9ERVBMT1lNRU5UX1NUQVRVU19TVE9Q";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 14) = "UElORxAEEiMKH0FQUF9ERVBMT1lNRU5UX1NUQVRVU19TVVNQRU5ERUQQBSpH";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 15) = "CgdBcHBUeXBlEhgKFEFQUF9UWVBFX1VOU1BFQ0lGSUVEEAASEAoMQVBQX1RZ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 16) = "UEVfQk9UEAESEAoMQVBQX1RZUEVfQVBQEAIqXQoLQXBwQ2F0ZWdvcnkSHAoY";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 17) = "QVBQX0NBVEVHT1JZX1VOU1BFQ0lGSUVEEAASFwoTQVBQX0NBVEVHT1JZX1NP";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 18) = "Q0lBTBABEhcKE0FQUF9DQVRFR09SWV9HQU1JTkcQAiqVAQoPRGV2ZWxvcGVy";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 19) = "U3RhdHVzEiAKHERFVkVMT1BFUl9TVEFUVVNfVU5TUEVDSUZJRUQQABIdChlE";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 20) = "RVZFTE9QRVJfU1RBVFVTX0NPTVBMRVRFEAESIwofREVWRUxPUEVSX1NUQVRV";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 21) = "U19OT1RfUkVHSVNURVJFRBACEhwKGERFVkVMT1BFUl9TVEFUVVNfQkxPQ0tF";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 22) = "RBADKqYCChpSb2xlT3JNZW1iZXJQaWNrZXJCZWhhdmlvchIuCipST0xFX09S";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 23) = "X01FTUJFUl9QSUNLRVJfQkVIQVZJT1JfVU5TUEVDSUZJRUQQABInCiNST0xF";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 24) = "X09SX01FTUJFUl9QSUNLRVJfQkVIQVZJT1JfVVNFUhABEigKJFJPTEVfT1Jf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 25) = "TUVNQkVSX1BJQ0tFUl9CRUhBVklPUl9VU0VSUxACEicKI1JPTEVfT1JfTUVN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 26) = "QkVSX1BJQ0tFUl9CRUhBVklPUl9ST0xFEAMSKAokUk9MRV9PUl9NRU1CRVJf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 27) = "UElDS0VSX0JFSEFWSU9SX1JPTEVTEAQSMgouUk9MRV9PUl9NRU1CRVJfUElD";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 28) = "S0VSX0JFSEFWSU9SX1JPTEVTX0FORF9VU0VSUxAFQg6qAgtSb290QXBwLkFw";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray30<string>, string>(ref _003C_003Ey__InlineArray, 29) = "cGIGcHJvdG8z";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray30<string>, string>(in _003C_003Ey__InlineArray, 30)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[0], new GeneratedClrTypeInfo(new Type[7]
		{
			typeof(AppStatus),
			typeof(AppVersionStatus),
			typeof(AppDeploymentStatus),
			typeof(AppType),
			typeof(AppCategory),
			typeof(DeveloperStatus),
			typeof(RoleOrMemberPickerBehavior)
		}, null, null));
	}
}
