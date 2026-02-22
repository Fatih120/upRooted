// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.Color
using System;
using System.Globalization;
using System.Text;
using Avalonia.Media;
using Avalonia.Utilities;

public readonly struct Color : IEquatable<Color>
{
	public byte A { get; }

	public byte R { get; }

	public byte G { get; }

	public byte B { get; }

	public Color(byte P_0, byte P_1, byte P_2, byte P_3)
	{
		A = P_0;
		R = P_1;
		G = P_2;
		B = P_3;
	}

	public static Color FromArgb(byte P_0, byte P_1, byte P_2, byte P_3)
	{
		return new Color(P_0, P_1, P_2, P_3);
	}

	public static Color FromRgb(byte P_0, byte P_1, byte P_2)
	{
		return new Color(byte.MaxValue, P_0, P_1, P_2);
	}

	public static Color FromUInt32(uint P_0)
	{
		return new Color((byte)((P_0 >> 24) & 0xFF), (byte)((P_0 >> 16) & 0xFF), (byte)((P_0 >> 8) & 0xFF), (byte)(P_0 & 0xFF));
	}

	public static Color Parse(string P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("s");
		}
		if (TryParse(P_0, out var result))
		{
			return result;
		}
		throw new FormatException("Invalid color string: '" + P_0 + "'.");
	}

	public static bool TryParse(string? P_0, out Color P_1)
	{
		P_1 = default(Color);
		if (string.IsNullOrEmpty(P_0))
		{
			return false;
		}
		if (P_0[0] == '#' && TryParseHexFormat(P_0.AsSpan(), out P_1))
		{
			return true;
		}
		if (P_0.Length >= 10 && (P_0[0] == 'r' || P_0[0] == 'R') && (P_0[1] == 'g' || P_0[1] == 'G') && (P_0[2] == 'b' || P_0[2] == 'B') && TryParseCssFormat(P_0, out P_1))
		{
			return true;
		}
		if (P_0.Length >= 10 && (P_0[0] == 'h' || P_0[0] == 'H') && (P_0[1] == 's' || P_0[1] == 'S') && (P_0[2] == 'l' || P_0[2] == 'L') && HslColor.TryParse(P_0, out var hslColor))
		{
			P_1 = hslColor.ToRgb();
			return true;
		}
		if (P_0.Length >= 10 && (P_0[0] == 'h' || P_0[0] == 'H') && (P_0[1] == 's' || P_0[1] == 'S') && (P_0[2] == 'v' || P_0[2] == 'V') && HsvColor.TryParse(P_0, out var hsvColor))
		{
			P_1 = hsvColor.ToRgb();
			return true;
		}
		KnownColor knownColor = KnownColors.GetKnownColor(P_0);
		if (knownColor != KnownColor.None)
		{
			P_1 = knownColor.ToColor();
			return true;
		}
		return false;
	}

	public static bool TryParse(ReadOnlySpan<char> P_0, out Color P_1)
	{
		if (P_0.Length == 0)
		{
			P_1 = default(Color);
			return false;
		}
		if (P_0[0] == '#' && TryParseHexFormat(P_0, out P_1))
		{
			return true;
		}
		string text = P_0.ToString();
		if (P_0.Length >= 10 && (P_0[0] == 'r' || P_0[0] == 'R') && (P_0[1] == 'g' || P_0[1] == 'G') && (P_0[2] == 'b' || P_0[2] == 'B') && TryParseCssFormat(text, out P_1))
		{
			return true;
		}
		if (P_0.Length >= 10 && (P_0[0] == 'h' || P_0[0] == 'H') && (P_0[1] == 's' || P_0[1] == 'S') && (P_0[2] == 'l' || P_0[2] == 'L') && HslColor.TryParse(text, out var hslColor))
		{
			P_1 = hslColor.ToRgb();
			return true;
		}
		if (P_0.Length >= 10 && (P_0[0] == 'h' || P_0[0] == 'H') && (P_0[1] == 's' || P_0[1] == 'S') && (P_0[2] == 'v' || P_0[2] == 'V') && HsvColor.TryParse(text, out var hsvColor))
		{
			P_1 = hsvColor.ToRgb();
			return true;
		}
		KnownColor knownColor = KnownColors.GetKnownColor(text);
		if (knownColor != KnownColor.None)
		{
			P_1 = knownColor.ToColor();
			return true;
		}
		P_1 = default(Color);
		return false;
	}

	private static bool TryParseHexFormat(ReadOnlySpan<char> P_0, out Color P_1)
	{
		P_1 = default(Color);
		ReadOnlySpan<char> readOnlySpan = P_0.Slice(1);
		if (readOnlySpan.Length == 3 || readOnlySpan.Length == 4)
		{
			Span<char> span = stackalloc char[2 * readOnlySpan.Length];
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				span[2 * i] = readOnlySpan[i];
				span[2 * i + 1] = readOnlySpan[i];
			}
			return TryParseCore(span, ref P_1);
		}
		return TryParseCore(readOnlySpan, ref P_1);
		static bool TryParseCore(ReadOnlySpan<char> readOnlySpan2, ref Color reference)
		{
			uint num = 0u;
			if (readOnlySpan2.Length == 6)
			{
				num = 4278190080u;
			}
			else if (readOnlySpan2.Length != 8)
			{
				return false;
			}
			if (!readOnlySpan2.TryParseUInt(NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var num2))
			{
				return false;
			}
			reference = FromUInt32(num2 | num);
			return true;
		}
	}

	private static bool TryParseCssFormat(string? P_0, out Color P_1)
	{
		bool flag = false;
		P_1 = default(Color);
		if (P_0 == null)
		{
			return false;
		}
		string text = P_0.Trim();
		if (text.Length == 0 || text.IndexOf(",", StringComparison.Ordinal) < 0)
		{
			return false;
		}
		if (text.Length >= 11 && text.StartsWith("rgba(", StringComparison.OrdinalIgnoreCase) && text.EndsWith(')'))
		{
			text = text.Substring(5, text.Length - 6);
			flag = true;
		}
		if (!flag && text.Length >= 10 && text.StartsWith("rgb(", StringComparison.OrdinalIgnoreCase) && text.EndsWith(')'))
		{
			text = text.Substring(4, text.Length - 5);
			flag = true;
		}
		if (!flag)
		{
			return false;
		}
		string[] array = text.Split(',');
		byte b4;
		byte b5;
		byte b6;
		double num;
		if (array.Length == 3)
		{
			if (InternalTryParseByte(array[0].AsSpan(), out var b) && InternalTryParseByte(array[1].AsSpan(), out var b2) && InternalTryParseByte(array[2].AsSpan(), out var b3))
			{
				P_1 = new Color(byte.MaxValue, b, b2, b3);
				return true;
			}
		}
		else if (array.Length == 4 && InternalTryParseByte(array[0].AsSpan(), out b4) && InternalTryParseByte(array[1].AsSpan(), out b5) && InternalTryParseByte(array[2].AsSpan(), out b6) && InternalTryParseDouble(array[3].AsSpan(), out num))
		{
			P_1 = new Color((byte)Math.Round(num * 255.0), b4, b5, b6);
			return true;
		}
		return false;
		static bool InternalTryParseByte(ReadOnlySpan<char> readOnlySpan, out byte reference)
		{
			int num2 = readOnlySpan.IndexOf("%".AsSpan(), StringComparison.Ordinal);
			if (num2 >= 0)
			{
				double num3;
				bool result = readOnlySpan.Slice(0, num2).TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture, out num3);
				reference = (byte)Math.Round(num3 / 100.0 * 255.0);
				return result;
			}
			return readOnlySpan.TryParseByte(NumberStyles.Number, CultureInfo.InvariantCulture, out reference);
		}
		static bool InternalTryParseDouble(ReadOnlySpan<char> readOnlySpan, out double reference)
		{
			int num2 = readOnlySpan.IndexOf("%".AsSpan(), StringComparison.Ordinal);
			if (num2 >= 0)
			{
				double num3;
				bool result = readOnlySpan.Slice(0, num2).TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture, out num3);
				reference = num3 / 100.0;
				return result;
			}
			return readOnlySpan.TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture, out reference);
		}
	}

	public override string ToString()
	{
		uint num = ToUInt32();
		return KnownColors.GetKnownColorName(num) ?? ("#" + num.ToString("x8", CultureInfo.InvariantCulture));
	}

	internal void ToString(StringBuilder P_0)
	{
		uint num = ToUInt32();
		if (KnownColors.TryGetKnownColorName(num, out string value))
		{
			P_0.Append(value);
			return;
		}
		P_0.Append('#');
		P_0.AppendFormat(CultureInfo.InvariantCulture, "{0:x8}", num);
	}

	public uint ToUInt32()
	{
		return (uint)((A << 24) | (R << 16) | (G << 8) | B);
	}

	public HsvColor ToHsv()
	{
		return ToHsv(R, G, B, A);
	}

	public bool Equals(Color P_0)
	{
		if (A == P_0.A && R == P_0.R && G == P_0.G)
		{
			return B == P_0.B;
		}
		return false;
	}

	public override bool Equals(object? P_0)
	{
		if (P_0 is Color color)
		{
			return Equals(color);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (((((A.GetHashCode() * 397) ^ R.GetHashCode()) * 397) ^ G.GetHashCode()) * 397) ^ B.GetHashCode();
	}

	public static HsvColor ToHsv(byte P_0, byte P_1, byte P_2, byte P_3 = byte.MaxValue)
	{
		return ToHsv(1.0 / 255.0 * (double)(int)P_0, 1.0 / 255.0 * (double)(int)P_1, 1.0 / 255.0 * (double)(int)P_2, 1.0 / 255.0 * (double)(int)P_3);
	}

	internal static HsvColor ToHsv(double P_0, double P_1, double P_2, double P_3 = 1.0)
	{
		double num = ((!(P_0 >= P_1)) ? ((P_1 >= P_2) ? P_1 : P_2) : ((P_0 >= P_2) ? P_0 : P_2));
		double num2 = ((!(P_0 <= P_1)) ? ((P_1 <= P_2) ? P_1 : P_2) : ((P_0 <= P_2) ? P_0 : P_2));
		double num3 = num;
		double num4 = num - num2;
		double num5;
		double num6;
		if (num4 == 0.0)
		{
			num5 = 0.0;
			num6 = 0.0;
		}
		else
		{
			num5 = ((P_0 == num) ? (60.0 * (P_1 - P_2) / num4) : ((P_1 != num) ? (240.0 + 60.0 * (P_0 - P_1) / num4) : (120.0 + 60.0 * (P_2 - P_0) / num4)));
			if (num5 < 0.0)
			{
				num5 += 360.0;
			}
			num6 = num4 / num3;
		}
		return new HsvColor(P_3, num5, num6, num3, false);
	}

	public static bool operator ==(Color P_0, Color P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(Color P_0, Color P_1)
	{
		return !P_0.Equals(P_1);
	}
}

