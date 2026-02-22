// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.HslColor
using System;
using System.Globalization;
using System.Text;
using Avalonia.Media;
using Avalonia.Utilities;

public readonly struct HslColor : IEquatable<HslColor>
{
	public double A { get; }

	public double H { get; }

	public double S { get; }

	public double L { get; }

	public HslColor(double P_0, double P_1, double P_2, double P_3)
	{
		A = MathUtilities.Clamp(P_0, 0.0, 1.0);
		H = MathUtilities.Clamp(P_1, 0.0, 360.0);
		S = MathUtilities.Clamp(P_2, 0.0, 1.0);
		L = MathUtilities.Clamp(P_3, 0.0, 1.0);
		H = ((H == 360.0) ? 0.0 : H);
	}

	public bool Equals(HslColor P_0)
	{
		if (P_0.A == A && P_0.H == H && P_0.S == S)
		{
			return P_0.L == L;
		}
		return false;
	}

	public override bool Equals(object? P_0)
	{
		if (P_0 is HslColor hslColor)
		{
			return Equals(hslColor);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (((((A.GetHashCode() * 397) ^ H.GetHashCode()) * 397) ^ S.GetHashCode()) * 397) ^ L.GetHashCode();
	}

	public Color ToRgb()
	{
		return ToRgb(H, S, L, A);
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = StringBuilderCache.Acquire();
		stringBuilder.Append("hsva(");
		stringBuilder.Append(H.ToString(CultureInfo.InvariantCulture));
		stringBuilder.Append(", ");
		stringBuilder.Append(S.ToString(CultureInfo.InvariantCulture));
		stringBuilder.Append(", ");
		stringBuilder.Append(L.ToString(CultureInfo.InvariantCulture));
		stringBuilder.Append(", ");
		stringBuilder.Append(A.ToString(CultureInfo.InvariantCulture));
		stringBuilder.Append(')');
		return StringBuilderCache.GetStringAndRelease(stringBuilder);
	}

	public static bool TryParse(string? P_0, out HslColor P_1)
	{
		bool flag = false;
		P_1 = default(HslColor);
		if (P_0 == null)
		{
			return false;
		}
		string text = P_0.Trim();
		if (text.Length == 0 || text.IndexOf(",", StringComparison.Ordinal) < 0)
		{
			return false;
		}
		if (text.Length >= 11 && text.StartsWith("hsla(", StringComparison.OrdinalIgnoreCase) && text.EndsWith(')'))
		{
			text = text.Substring(5, text.Length - 6);
			flag = true;
		}
		if (!flag && text.Length >= 10 && text.StartsWith("hsl(", StringComparison.OrdinalIgnoreCase) && text.EndsWith(')'))
		{
			text = text.Substring(4, text.Length - 5);
			flag = true;
		}
		if (!flag)
		{
			return false;
		}
		string[] array = text.Split(',');
		double num4;
		double num5;
		double num6;
		double num7;
		if (array.Length == 3)
		{
			if (array[0].AsSpan().TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture, out var num) && TryInternalParse(array[1].AsSpan(), out var num2) && TryInternalParse(array[2].AsSpan(), out var num3))
			{
				P_1 = new HslColor(1.0, num, num2, num3);
				return true;
			}
		}
		else if (array.Length == 4 && array[0].AsSpan().TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture, out num4) && TryInternalParse(array[1].AsSpan(), out num5) && TryInternalParse(array[2].AsSpan(), out num6) && TryInternalParse(array[3].AsSpan(), out num7))
		{
			P_1 = new HslColor(num7, num4, num5, num6);
			return true;
		}
		return false;
		static bool TryInternalParse(ReadOnlySpan<char> readOnlySpan, out double reference)
		{
			int num8 = readOnlySpan.IndexOf("%".AsSpan(), StringComparison.Ordinal);
			if (num8 >= 0)
			{
				double num9;
				bool result = readOnlySpan.Slice(0, num8).TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture, out num9);
				reference = num9 / 100.0;
				return result;
			}
			return readOnlySpan.TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture, out reference);
		}
	}

	public static Color ToRgb(double P_0, double P_1, double P_2, double P_3 = 1.0)
	{
		while (P_0 >= 360.0)
		{
			P_0 -= 360.0;
		}
		while (P_0 < 0.0)
		{
			P_0 += 360.0;
		}
		P_1 = ((P_1 < 0.0) ? 0.0 : P_1);
		P_1 = ((P_1 > 1.0) ? 1.0 : P_1);
		P_2 = ((P_2 < 0.0) ? 0.0 : P_2);
		P_2 = ((P_2 > 1.0) ? 1.0 : P_2);
		P_3 = ((P_3 < 0.0) ? 0.0 : P_3);
		P_3 = ((P_3 > 1.0) ? 1.0 : P_3);
		double num = (1.0 - Math.Abs(2.0 * P_2 - 1.0)) * P_1;
		double num2 = P_0 / 60.0;
		double num3 = num * (1.0 - Math.Abs(num2 % 2.0 - 1.0));
		double num4 = P_2 - 0.5 * num;
		double num5;
		double num6;
		double num7;
		if (num2 < 1.0)
		{
			num5 = num;
			num6 = num3;
			num7 = 0.0;
		}
		else if (num2 < 2.0)
		{
			num5 = num3;
			num6 = num;
			num7 = 0.0;
		}
		else if (num2 < 3.0)
		{
			num5 = 0.0;
			num6 = num;
			num7 = num3;
		}
		else if (num2 < 4.0)
		{
			num5 = 0.0;
			num6 = num3;
			num7 = num;
		}
		else if (num2 < 5.0)
		{
			num5 = num3;
			num6 = 0.0;
			num7 = num;
		}
		else
		{
			num5 = num;
			num6 = 0.0;
			num7 = num3;
		}
		return new Color((byte)Math.Round(255.0 * P_3), (byte)Math.Round(255.0 * (num5 + num4)), (byte)Math.Round(255.0 * (num6 + num4)), (byte)Math.Round(255.0 * (num7 + num4)));
	}

	public static bool operator ==(HslColor P_0, HslColor P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(HslColor P_0, HslColor P_1)
	{
		return !(P_0 == P_1);
	}

	public static explicit operator Color(HslColor P_0)
	{
		return P_0.ToRgb();
	}
}

