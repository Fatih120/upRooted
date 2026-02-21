using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.Foundation;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.264+4d6898735f.RR")]
internal readonly struct PCWSTR : IEquatable<PCWSTR>
{
	internal unsafe readonly char* Value;

	internal unsafe PCWSTR(char* P_0)
	{
		Value = P_0;
	}

	public unsafe static explicit operator char*(PCWSTR P_0)
	{
		return P_0.Value;
	}

	public unsafe static implicit operator PCWSTR(char* P_0)
	{
		return new PCWSTR(P_0);
	}

	public unsafe bool Equals(PCWSTR P_0)
	{
		return Value == P_0.Value;
	}

	public override bool Equals(object P_0)
	{
		return P_0 is PCWSTR pCWSTR && Equals(pCWSTR);
	}

	public unsafe override int GetHashCode()
	{
		return (int)Value;
	}

	public unsafe override string ToString()
	{
		return (Value == null) ? null : new string(Value);
	}
}
