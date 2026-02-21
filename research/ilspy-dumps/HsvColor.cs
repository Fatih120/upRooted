// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.HsvColor
using System;
using System.Globalization;
using System.Text;
using Avalonia.Media;
using Avalonia.Utilities;

public readonly struct HsvColor : IEquatable<HsvColor>
{
	public double A { get; }

	public double H { get; }

	public double S { get; }

	public double V { get; }

	public HsvColor(double P_0, double P_1, double P_2, double P_3)
	{
		A = MathUtilities.Clamp(P_0, 0.0, 1.0);
		H = MathUtilities.Clamp(P_1, 0.0, 360.0);
		S = MathUtilities.Clamp(P_2, 0.0, 1.0);
		V = MathUtilities.Clamp(P_3, 0.0, 1.0);
		H = ((H == 360.0) ? 0.0 : H);
	}

	internal HsvColor(double P_0, double P_1, double P_2, double P_3, bool P_4)
	{
		if (P_4)
		{
			A = MathUtilities.Clamp(P_0, 0.0, 1.0);
			H = MathUtilities.Clamp(P_1, 0.0, 360.0);
			S = MathUtilities.Clamp(P_2, 0.0, 1.0);
			V = MathUtilities.Clamp(P_3, 0.0, 1.0);
			H = ((H == 360.0) ? 0.0 : H);
		}
		else
		{
			A = P_0;
			H = P_1;
			S = P_2;
			V = P_3;
		}
	}

	public bool Equals(HsvColor P_0)
	{
		if (P_0.A == A && P_0.H == H && P_0.S == S)
		{
			return P_0.V == V;
		}
		return false;
	}

	public override bool Equals(object? P_0)
	{
		if (P_0 is HsvColor hsvColor)
		{
			return Equals(hsvColor);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (((((A.GetHashCode() * 397) ^ H.GetHashCode()) * 397) ^ S.GetHashCode()) * 397) ^ V.GetHashCode();
	}

	public Color ToRgb()
	{
		return ToRgb(H, S, V, A);
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = StringBuilderCache.Acquire();
		stringBuilder.Append("hsva(");
		stringBuilder.Append(H.ToString(CultureInfo.InvariantCulture));
		stringBuilder.Append(", ");
		stringBuilder.Append(S.ToString(CultureInfo.InvariantCulture));
		stringBuilder.Append(", ");
		stringBuilder.Append(V.ToString(CultureInfo.InvariantCulture));
		stringBuilder.Append(", ");
		stringBuilder.Append(A.ToString(CultureInfo.InvariantCulture));
		stringBuilder.Append(')');
		return StringBuilderCache.GetStringAndRelease(stringBuilder);
	}

	public static bool TryParse(string? P_0, out HsvColor P_1)
	{
		bool flag = false;
		P_1 = default(HsvColor);
		if (P_0 == null)
		{
			return false;
		}
		string text = P_0.Trim();
		if (text.Length == 0 || text.IndexOf(",", StringComparison.Ordinal) < 0)
		{
			return false;
		}
		if (text.Length >= 11 && text.StartsWith("hsva(", StringComparison.OrdinalIgnoreCase) && text.EndsWith(")", StringComparison.Ordinal))
		{
			text = text.Substring(5, text.Length - 6);
			flag = true;
		}
		if (!flag && text.Length >= 10 && text.StartsWith("hsv(", StringComparison.OrdinalIgnoreCase) && text.EndsWith(")", StringComparison.Ordinal))
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
				P_1 = new HsvColor(1.0, num, num2, num3);
				return true;
			}
		}
		else if (array.Length == 4 && array[0].AsSpan().TryParseDouble(NumberStyles.Number, CultureInfo.InvariantCulture, out num4) && TryInternalParse(array[1].AsSpan(), out num5) && TryInternalParse(array[2].AsSpan(), out num6) && TryInternalParse(array[3].AsSpan(), out num7))
		{
			P_1 = new HsvColor(num7, num4, num5, num6);
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

	public static HsvColor FromAhsv(double P_0, double P_1, double P_2, double P_3)
	{
		return new HsvColor(P_0, P_1, P_2, P_3);
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
		double num = P_1 * P_2;
		double num2 = P_2 - num;
		if (num == 0.0)
		{
			return Color.FromArgb((byte)Math.Round(P_3 * 255.0), (byte)Math.Round(num2 * 255.0), (byte)Math.Round(num2 * 255.0), (byte)Math.Round(num2 * 255.0));
		}
		int num3 = (int)(P_0 / 60.0);
		double num4 = P_0 / 60.0 - (double)num3;
		double num5 = num + num2;
		double num6 = 0.0;
		double num7 = 0.0;
		double num8 = 0.0;
		switch (num3)
		{
		case 0:
			num6 = num5;
			num7 = num2 + num * num4;
			num8 = num2;
			break;
		case 1:
			num6 = num2 + num * (1.0 - num4);
			num7 = num5;
			num8 = num2;
			break;
		case 2:
			num6 = num2;
			num7 = num5;
			num8 = num2 + num * num4;
			break;
		case 3:
			num6 = num2;
			num7 = num2 + num * (1.0 - num4);
			num8 = num5;
			break;
		case 4:
			num6 = num2 + num * num4;
			num7 = num2;
			num8 = num5;
			break;
		case 5:
			num6 = num5;
			num7 = num2;
			num8 = num2 + num * (1.0 - num4);
			break;
		}
		return new Color((byte)Math.Round(P_3 * 255.0), (byte)Math.Round(num6 * 255.0), (byte)Math.Round(num7 * 255.0), (byte)Math.Round(num8 * 255.0));
	}

	public static bool operator ==(HsvColor P_0, HsvColor P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(HsvColor P_0, HsvColor P_1)
	{
		return !(P_0 == P_1);
	}

	public static explicit operator Color(HsvColor P_0)
	{
		return P_0.ToRgb();
	}
}

