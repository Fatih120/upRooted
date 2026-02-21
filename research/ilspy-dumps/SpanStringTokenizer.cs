// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpanStringTokenizer
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Avalonia.Utilities;

internal ref struct SpanStringTokenizer
{
	private readonly ReadOnlySpan<char> _s;

	private readonly int _length;

	private readonly char _separator;

	private readonly string? _exceptionMessage;

	private readonly IFormatProvider _formatProvider;

	private int _index;

	private int _tokenIndex;

	private int _tokenLength;

	public int CurrentTokenIndex => _tokenIndex;

	public ReadOnlySpan<char> CurrentTokenSpan
	{
		get
		{
			if (_tokenIndex >= 0)
			{
				return _s.Slice(_tokenIndex, _tokenLength);
			}
			return ReadOnlySpan<char>.Empty;
		}
	}

	public SpanStringTokenizer(string P_0, IFormatProvider P_1, string? P_2 = null)
		: this(P_0.AsSpan(), GetSeparatorFromFormatProvider(P_1), P_2)
	{
		_formatProvider = P_1;
	}

	public SpanStringTokenizer(string P_0, char P_1 = ',', string? P_2 = null)
		: this(P_0.AsSpan(), P_1, P_2)
	{
	}

	public SpanStringTokenizer(ReadOnlySpan<char> P_0, char P_1 = ',', string? P_2 = null)
	{
		_s = P_0;
		_length = P_0.Length;
		_separator = P_1;
		_exceptionMessage = P_2;
		_formatProvider = CultureInfo.InvariantCulture;
		_index = 0;
		_tokenIndex = -1;
		_tokenLength = 0;
		while (_index < _length && char.IsWhiteSpace(_s[_index]))
		{
			_index++;
		}
	}

	public void Dispose()
	{
		if (_index != _length)
		{
			throw GetFormatException();
		}
	}

	public bool TryReadInt32(out int P_0, char? P_1 = null)
	{
		if (TryReadSpan(out var readOnlySpan, P_1) && readOnlySpan.TryParseInt(NumberStyles.Integer, _formatProvider, out P_0))
		{
			return true;
		}
		P_0 = 0;
		return false;
	}

	public bool TryReadDouble(out double P_0, char? P_1 = null)
	{
		if (TryReadSpan(out var readOnlySpan, P_1) && readOnlySpan.TryParseDouble(NumberStyles.Float, _formatProvider, out P_0))
		{
			return true;
		}
		P_0 = 0.0;
		return false;
	}

	public double ReadDouble(char? P_0 = null)
	{
		if (!TryReadDouble(out var result, P_0))
		{
			throw GetFormatException();
		}
		return result;
	}

	public bool TryReadString([NotNull] out string P_0, char? P_1 = null)
	{
		bool result = TryReadToken(P_1 ?? _separator);
		P_0 = CurrentTokenSpan.ToString();
		return result;
	}

	public bool TryReadSpan(out ReadOnlySpan<char> P_0, char? P_1 = null)
	{
		bool result = TryReadToken(P_1 ?? _separator);
		P_0 = CurrentTokenSpan;
		return result;
	}

	public ReadOnlySpan<char> ReadSpan(char? P_0 = null)
	{
		if (!TryReadSpan(out var result, P_0))
		{
			throw GetFormatException();
		}
		return result;
	}

	private bool TryReadToken(char P_0)
	{
		_tokenIndex = -1;
		if (_index >= _length)
		{
			return false;
		}
		char c = _s[_index];
		int index = _index;
		int num = 0;
		while (_index < _length)
		{
			c = _s[_index];
			if (char.IsWhiteSpace(c) || c == P_0)
			{
				break;
			}
			_index++;
			num++;
		}
		SkipToNextToken(P_0);
		_tokenIndex = index;
		_tokenLength = num;
		if (_tokenLength < 1)
		{
			throw GetFormatException();
		}
		return true;
	}

	private void SkipToNextToken(char P_0)
	{
		if (_index >= _length)
		{
			return;
		}
		char c = _s[_index];
		if (c != P_0 && !char.IsWhiteSpace(c))
		{
			throw GetFormatException();
		}
		int num = 0;
		while (_index < _length)
		{
			c = _s[_index];
			if (c == P_0)
			{
				num++;
				_index++;
				if (num > 1)
				{
					throw GetFormatException();
				}
			}
			else
			{
				if (!char.IsWhiteSpace(c))
				{
					break;
				}
				_index++;
			}
		}
		if (num > 0 && _index >= _length)
		{
			throw GetFormatException();
		}
	}

	private FormatException GetFormatException()
	{
		if (_exceptionMessage == null)
		{
			return new FormatException();
		}
		return new FormatException(_exceptionMessage);
	}

	private static char GetSeparatorFromFormatProvider(IFormatProvider P_0)
	{
		char c = ',';
		NumberFormatInfo instance = NumberFormatInfo.GetInstance(P_0);
		if (instance.NumberDecimalSeparator.Length > 0 && c == instance.NumberDecimalSeparator[0])
		{
			c = ';';
		}
		return c;
	}
}

