using System;
using Google.Protobuf.Reflection;

namespace RootApp.Core.Enums;

public static class EnumsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static EnumsReflection()
	{
		_003C_003Ey__InlineArray39<string> _003C_003Ey__InlineArray = default(_003C_003Ey__InlineArray39<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 0) = "ChBjb3JlL2VudW1zLnByb3RvEgRyb290Kt0LCgxSb290R3VpZFR5cGUSGgoW";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 1) = "Uk9PVF9HVUlEX1RZUEVfVU5LTk9XThAAEhkKFVJPT1RfR1VJRF9UWVBFX1BF";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 2) = "UlNPThABEhwKGFJPT1RfR1VJRF9UWVBFX0NPTU1VTklUWRACEhoKFlJPT1Rf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 3) = "R1VJRF9UWVBFX01FU1NBR0UQAxIaChZST09UX0dVSURfVFlQRV9DSEFOTkVM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 4) = "EAQSIAocUk9PVF9HVUlEX1RZUEVfQ0hBTk5FTF9HUk9VUBAFEh0KGVJPT1Rf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 5) = "R1VJRF9UWVBFX0ZSSUVORFNISVAQBhIjCh9ST09UX0dVSURfVFlQRV9GUklF";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 6) = "TkRTSElQX0dST1VQEAcSJAogUk9PVF9HVUlEX1RZUEVfRlJJRU5EU0hJUF9J";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 7) = "TlZJVEUQCBIWChJST09UX0dVSURfVFlQRV9BUFAQCRIqCiZST09UX0dVSURf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 8) = "VFlQRV9DT01NVU5JVFlfTUVNQkVSX0lOVklURRAKEiEKHVJPT1RfR1VJRF9U";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 9) = "WVBFX0NPTU1VTklUWV9ST0xFEAsSFwoTUk9PVF9HVUlEX1RZUEVfRklMRRAM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 10) = "EhwKGFJPT1RfR1VJRF9UWVBFX0RJUkVDVE9SWRANEiUKIVJPT1RfR1VJRF9U";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 11) = "WVBFX01FU1NBR0VfQVRUQUNITUVOVBAOEiEKHVJPT1RfR1VJRF9UWVBFX0RJ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 12) = "UkVDVF9NRVNTQUdFEA8SKAokUk9PVF9HVUlEX1RZUEVfRElSRUNUX01FU1NB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 13) = "R0VfTUVNQkVSEBASHwobUk9PVF9HVUlEX1RZUEVfTk9USUZJQ0FUSU9OEBES";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 14) = "GgoWUk9PVF9HVUlEX1RZUEVfREVTS1RPUBASEhkKFVJPT1RfR1VJRF9UWVBF";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 15) = "X01PQklMRRATEhwKGFJPT1RfR1VJRF9UWVBFX0VYQ0VQVElPThAUEigKJFJP";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 16) = "T1RfR1VJRF9UWVBFX0NPTU1VTklUWV9NRU1CRVJfUk9MRRAVEiMKH1JPT1Rf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 17) = "R1VJRF9UWVBFX1RIUkVBREVEX01FU1NBR0UQFhIrCidST09UX0dVSURfVFlQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 18) = "RV9USFJFQURFRF9NRVNTQUdFX01FU1NBR0UQFxInCiNST09UX0dVSURfVFlQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 19) = "RV9DT01NVU5JVFlfTUVNQkVSX0JBThAYEiUKIVJPT1RfR1VJRF9UWVBFX0NP";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 20) = "TU1VTklUWV9ST0xFX01BUBAZEiAKHFJPT1RfR1VJRF9UWVBFX0NPTU1VTklU";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 21) = "WV9MT0cQGhIYChRST09UX0dVSURfVFlQRV9BU1NFVBAbEiMKH1JPT1RfR1VJ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 22) = "RF9UWVBFX0FQUF9PUkdBTklaQVRJT04QHBIgChxST09UX0dVSURfVFlQRV9D";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 23) = "T01NVU5JVFlfQVBQEB0SJgoiUk9PVF9HVUlEX1RZUEVfQ09NTUFORF9JREVN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 24) = "UE9URU5DWRAeEiMKH1JPT1RfR1VJRF9UWVBFX0NPTU1VTklUWV9NRU1CRVIQ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 25) = "HxIeChpST09UX0dVSURfVFlQRV9BUFBfVkVSU0lPThAgEhYKElJPT1RfR1VJ";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 26) = "RF9UWVBFX1dFQhAhEhgKFFJPT1RfR1VJRF9UWVBFX0JBREdFECISIwoeUk9P";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 27) = "VF9HVUlEX1RZUEVfQ1VTVE9NX1JFU09VUkNFEOABEicKIlJPT1RfR1VJRF9U";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 28) = "WVBFX0NVU1RPTV9NRU1CRVJfR1JPVVAQ4QESIgodUk9PVF9HVUlEX1RZUEVf";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 29) = "QVBQX0RFUExPWU1FTlQQ+QESJgohUk9PVF9HVUlEX1RZUEVfTUVTU0FHRV9C";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 30) = "VVNfU0VSVkVSEPoBEiIKHVJPT1RfR1VJRF9UWVBFX1dFQl9BUElfU0VSVkVS";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 31) = "EPsBEh4KGVJPT1RfR1VJRF9UWVBFX0hVQl9TRVJWRVIQ/AESIwoeUk9PVF9H";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 32) = "VUlEX1RZUEVfQVBQX0hPU1RfU0VSVkVSEP0BEiIKHVJPT1RfR1VJRF9UWVBF";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 33) = "X0FQUF9IVUJfU0VSVkVSEP4BEhcKElJPT1RfR1VJRF9UWVBFX01BWBD/ASq+";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 34) = "AQoRVXBsb2FkVG9rZW5TdGF0dXMSIwofVVBMT0FEX1RPS0VOX1NUQVRVU19V";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 35) = "TlNQRUNJRklFRBAAEh8KG1VQTE9BRF9UT0tFTl9TVEFUVVNfUEVORElORxAB";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 36) = "Eh8KG1VQTE9BRF9UT0tFTl9TVEFUVVNfSU5WQUxJRBACEiAKHFVQTE9BRF9U";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 37) = "T0tFTl9TVEFUVVNfQUNDRVBURUQQAxIgChxVUExPQURfVE9LRU5fU1RBVFVT";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<_003C_003Ey__InlineArray39<string>, string>(ref _003C_003Ey__InlineArray, 38) = "X1JFSkVDVEVEEARCFaoCElJvb3RBcHAuQ29yZS5FbnVtc2IGcHJvdG8z";
		byte[] array = Convert.FromBase64String(string.Concat(global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<_003C_003Ey__InlineArray39<string>, string>(in _003C_003Ey__InlineArray, 39)));
		descriptor = FileDescriptor.FromGeneratedCode(array, new FileDescriptor[0], new GeneratedClrTypeInfo(new Type[2]
		{
			typeof(RootGuidType),
			typeof(UploadTokenStatus)
		}, null, null));
	}
}
