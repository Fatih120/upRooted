using System;
using System.Collections.Frozen;
using System.Text;

namespace RootApp.Utility.Normalization;

public static class FilenameUtility
{
	private static readonly Rune _period = new Rune('.');

	private static readonly Rune _space = new Rune(' ');

	private static readonly Rune _safeRune = new Rune('_');

	private static readonly FrozenSet<string> _windowsDeviceNamesBase;

	private static readonly FrozenSet<string>.AlternateLookup<ReadOnlySpan<char>> _windowsDeviceNamesBaseSpan;

	private static readonly FrozenSet<string> _windowsDeviceNamesPrefixes;

	private static readonly FrozenSet<string>.AlternateLookup<ReadOnlySpan<char>> _windowsDeviceNamesPrefixesSpan;

	public static string NormalizeOrEmpty(string P_0)
	{
		ReadOnlySpan<char> readOnlySpan = P_0.AsSpan().Trim();
		int length = readOnlySpan.Length;
		if ((length < 1 || length > 250) ? true : false)
		{
			return string.Empty;
		}
		Span<char> span = stackalloc char[readOnlySpan.Length];
		Span<char> span2 = span;
		bool flag = P_0.Length != readOnlySpan.Length;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		SpanRuneEnumerator enumerator = readOnlySpan.EnumerateRunes().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Rune current = enumerator.Current;
				bool flag6 = IsValid(current);
				bool flag7 = _period == current;
				bool flag8 = _space == current;
				if (flag7 && !flag5)
				{
					flag = true;
					continue;
				}
				if (flag7 && flag2)
				{
					flag6 = false;
				}
				else if (flag8 && flag3)
				{
					flag = true;
					continue;
				}
				flag2 = flag7;
				flag3 = flag8;
				if (!flag6)
				{
					flag = true;
					if (!flag4)
					{
						continue;
					}
				}
				flag4 = flag6;
				flag5 = true;
				if (!(flag6 ? current : _safeRune).TryEncodeToUtf16(span2, out var num))
				{
					return string.Empty;
				}
				length = num;
				span2 = span2.Slice(length, span2.Length - length);
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to .constrained prefix*/).Dispose();
		}
		length = span2.Length;
		Span<char> span3 = span.Slice(0, span.Length - length);
		if (span3.Length > 0)
		{
			if (span3[span3.Length - 1] == '.')
			{
				span3[span3.Length - 1] = '_';
				flag = true;
			}
		}
		return flag ? new string(span3) : P_0;
	}

	private static bool IsValid(Rune P_0)
	{
		if (Rune.IsLetterOrDigit(P_0))
		{
			return true;
		}
		if (!P_0.IsAscii)
		{
			return false;
		}
		bool result;
		switch ((char)(ushort)P_0.Value)
		{
		case ' ':
		case '-':
		case '.':
		case '_':
			result = true;
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	static FilenameUtility()
	{
		StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
		global::_003C_003Ey__InlineArray4<string> _003C_003Ey__InlineArray = default(global::_003C_003Ey__InlineArray4<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray4<string>, string>(ref _003C_003Ey__InlineArray, 0) = "CON";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray4<string>, string>(ref _003C_003Ey__InlineArray, 1) = "PRN";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray4<string>, string>(ref _003C_003Ey__InlineArray, 2) = "AUX";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray4<string>, string>(ref _003C_003Ey__InlineArray, 3) = "NUL";
		_windowsDeviceNamesBase = FrozenSet.Create(ordinalIgnoreCase, global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<global::_003C_003Ey__InlineArray4<string>, string>(in _003C_003Ey__InlineArray, 4));
		_windowsDeviceNamesBaseSpan = _windowsDeviceNamesBase.GetAlternateLookup<ReadOnlySpan<char>>();
		StringComparer ordinalIgnoreCase2 = StringComparer.OrdinalIgnoreCase;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray2 = default(global::_003C_003Ey__InlineArray2<string>);
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray2<string>, string>(ref _003C_003Ey__InlineArray2, 0) = "COM";
		global::_003CPrivateImplementationDetails_003E.InlineArrayElementRef<global::_003C_003Ey__InlineArray2<string>, string>(ref _003C_003Ey__InlineArray2, 1) = "LPT";
		_windowsDeviceNamesPrefixes = FrozenSet.Create(ordinalIgnoreCase2, global::_003CPrivateImplementationDetails_003E.InlineArrayAsReadOnlySpan<global::_003C_003Ey__InlineArray2<string>, string>(in _003C_003Ey__InlineArray2, 2));
		_windowsDeviceNamesPrefixesSpan = _windowsDeviceNamesPrefixes.GetAlternateLookup<ReadOnlySpan<char>>();
	}
}
