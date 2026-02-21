using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Windows.Win32.Foundation;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.264+4d6898735f.RR")]
internal readonly struct HRESULT : IEquatable<HRESULT>
{
	internal readonly int Value;

	internal static readonly HRESULT S_OK = (HRESULT)0;

	internal static readonly HRESULT S_FALSE = (HRESULT)1;

	internal static readonly HRESULT INET_E_SECURITY_PROBLEM = (HRESULT)(-2146697202);

	internal static readonly HRESULT E_FAIL = (HRESULT)(-2147467259);

	internal static readonly HRESULT REGDB_E_CLASSNOTREG = (HRESULT)(-2147221164);

	internal bool Succeeded => Value >= 0;

	internal HRESULT(int P_0)
	{
		Value = P_0;
	}

	public static implicit operator int(HRESULT P_0)
	{
		return P_0.Value;
	}

	public static explicit operator HRESULT(int P_0)
	{
		return new HRESULT(P_0);
	}

	public static bool operator ==(HRESULT P_0, HRESULT P_1)
	{
		return P_0.Value == P_1.Value;
	}

	public static bool operator !=(HRESULT P_0, HRESULT P_1)
	{
		return !(P_0 == P_1);
	}

	public bool Equals(HRESULT P_0)
	{
		return Value == P_0.Value;
	}

	public override bool Equals(object P_0)
	{
		return P_0 is HRESULT hRESULT && Equals(hRESULT);
	}

	public override int GetHashCode()
	{
		return Value.GetHashCode();
	}

	public override string ToString()
	{
		return string.Format(CultureInfo.InvariantCulture, "0x{0:X8}", Value);
	}

	public static implicit operator uint(HRESULT P_0)
	{
		return (uint)P_0.Value;
	}

	public static explicit operator HRESULT(uint P_0)
	{
		return new HRESULT((int)P_0);
	}

	internal HRESULT ThrowOnFailure(nint P_0 = 0)
	{
		Marshal.ThrowExceptionForHR(Value, P_0);
		return this;
	}
}
