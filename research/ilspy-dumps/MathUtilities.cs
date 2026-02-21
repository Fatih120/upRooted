// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.MathUtilities
using System;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Metadata;

[Unstable("This API might be removed in next major version. Please use corresponding BCL APIs.")]
public static class MathUtilities
{
	public static bool AreClose(double P_0, double P_1)
	{
		if (P_0 == P_1)
		{
			return true;
		}
		double num = (Math.Abs(P_0) + Math.Abs(P_1) + 10.0) * 2.220446049250313E-16;
		double num2 = P_0 - P_1;
		if (0.0 - num < num2)
		{
			return num > num2;
		}
		return false;
	}

	public static bool AreClose(double P_0, double P_1, double P_2)
	{
		if (P_0 == P_1)
		{
			return true;
		}
		double num = P_0 - P_1;
		if (0.0 - P_2 < num)
		{
			return P_2 > num;
		}
		return false;
	}

	public static bool LessThan(double P_0, double P_1)
	{
		if (P_0 < P_1)
		{
			return !AreClose(P_0, P_1);
		}
		return false;
	}

	public static bool GreaterThan(double P_0, double P_1)
	{
		if (P_0 > P_1)
		{
			return !AreClose(P_0, P_1);
		}
		return false;
	}

	public static bool LessThanOrClose(double P_0, double P_1)
	{
		if (!(P_0 < P_1))
		{
			return AreClose(P_0, P_1);
		}
		return true;
	}

	public static bool GreaterThanOrClose(double P_0, double P_1)
	{
		if (!(P_0 > P_1))
		{
			return AreClose(P_0, P_1);
		}
		return true;
	}

	public static bool IsOne(double P_0)
	{
		return Math.Abs(P_0 - 1.0) < 2.220446049250313E-15;
	}

	public static bool IsZero(double P_0)
	{
		return Math.Abs(P_0) < 2.220446049250313E-15;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double Clamp(double P_0, double P_1, double P_2)
	{
		if (P_1 > P_2)
		{
			ThrowCannotBeGreaterThanException(P_1, P_2);
		}
		if (P_0 < P_1)
		{
			return P_1;
		}
		if (P_0 > P_2)
		{
			return P_2;
		}
		return P_0;
	}

	public static decimal Clamp(decimal P_0, decimal P_1, decimal P_2)
	{
		if (P_1 > P_2)
		{
			ThrowCannotBeGreaterThanException(P_1, P_2);
		}
		if (P_0 < P_1)
		{
			return P_1;
		}
		if (P_0 > P_2)
		{
			return P_2;
		}
		return P_0;
	}

	public static int Clamp(int P_0, int P_1, int P_2)
	{
		if (P_1 > P_2)
		{
			ThrowCannotBeGreaterThanException(P_1, P_2);
		}
		if (P_0 < P_1)
		{
			return P_1;
		}
		if (P_0 > P_2)
		{
			return P_2;
		}
		return P_0;
	}

	public static double Deg2Rad(double P_0)
	{
		return P_0 * (Math.PI / 180.0);
	}

	public static double Grad2Rad(double P_0)
	{
		return P_0 * (Math.PI / 200.0);
	}

	public static double Turn2Rad(double P_0)
	{
		return P_0 * 2.0 * Math.PI;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static bool IsNegativeOrNonFinite(double P_0)
	{
		return BitConverter.DoubleToUInt64Bits(P_0) >= 9218868437227405312L;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static bool IsFinite(double P_0)
	{
		return double.IsFinite(P_0);
	}

	internal static int WhichPolygonSideIntersects(uint P_0, ReadOnlySpan<Vector> P_1, Vector P_2, Vector P_3)
	{
		uint num = 0u;
		uint num2 = 0u;
		uint num3 = 0u;
		Point point = new Point(0.0 - P_3.Y, P_3.X);
		for (int i = 0; i < P_0; i++)
		{
			double num4 = Vector.Dot(P_2 - P_1[i], point);
			if (num4 > 0.0)
			{
				num++;
			}
			else if (num4 < 0.0)
			{
				num2++;
			}
			else
			{
				num3++;
			}
			if ((num != 0 && num2 != 0) || num3 != 0)
			{
				return 0;
			}
		}
		if (num == 0)
		{
			return -1;
		}
		return 1;
	}

	internal static bool DoPolygonsIntersect(uint P_0, ReadOnlySpan<Vector> P_1, uint P_2, ReadOnlySpan<Vector> P_3)
	{
		for (int i = 0; i < P_0; i++)
		{
			Vector vector = P_1[(int)((i + 1) % P_0)] - P_1[i];
			if (WhichPolygonSideIntersects(P_2, P_3, P_1[i], vector) < 0)
			{
				return false;
			}
		}
		for (int j = 0; j < P_2; j++)
		{
			Vector vector2 = P_3[(int)((j + 1) % P_2)] - P_3[j];
			if (WhichPolygonSideIntersects(P_0, P_1, P_3[j], vector2) < 0)
			{
				return false;
			}
		}
		return true;
	}

	internal static bool IsEntirelyContained(uint P_0, ReadOnlySpan<Vector> P_1, uint P_2, ReadOnlySpan<Vector> P_3)
	{
		for (int i = 0; i < P_2; i++)
		{
			Vector vector = P_3[(i + 1) % (int)P_2] - P_3[i];
			if (WhichPolygonSideIntersects(P_0, P_1, P_3[i], vector) <= 0)
			{
				return false;
			}
		}
		return true;
	}

	private static void ThrowCannotBeGreaterThanException<T>(T P_0, T P_1)
	{
		throw new ArgumentException($"{P_0} cannot be greater than {P_1}.");
	}
}

