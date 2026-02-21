using System.Numerics;

namespace RootApp.Utility.Extensions;

public static class ScaleBytes
{
	public static double ToMiB<T>(this T P_0) where T : INumber<T>
	{
		return double.CreateChecked(P_0) * 9.5367431640625E-07;
	}

	public static double ToGiB<T>(this T P_0) where T : INumber<T>
	{
		return double.CreateChecked(P_0) * 9.313225746154785E-10;
	}
}
