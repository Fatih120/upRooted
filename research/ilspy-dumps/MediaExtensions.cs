// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.MediaExtensions
using Avalonia;
using Avalonia.Media;
using Avalonia.Utilities;

public static class MediaExtensions
{
	public static Vector CalculateScaling(this Stretch P_0, Size P_1, Size P_2, StretchDirection P_3 = StretchDirection.Both)
	{
		double num = 1.0;
		double num2 = 1.0;
		bool flag = !double.IsPositiveInfinity(P_1.Width);
		bool flag2 = !double.IsPositiveInfinity(P_1.Height);
		if ((P_0 == Stretch.Uniform || P_0 == Stretch.UniformToFill || P_0 == Stretch.Fill) && (flag || flag2))
		{
			num = (MathUtilities.IsZero(P_2.Width) ? 0.0 : (P_1.Width / P_2.Width));
			num2 = (MathUtilities.IsZero(P_2.Height) ? 0.0 : (P_1.Height / P_2.Height));
			if (!flag)
			{
				num = num2;
			}
			else if (!flag2)
			{
				num2 = num;
			}
			else
			{
				switch (P_0)
				{
				case Stretch.Uniform:
					num = (num2 = ((num < num2) ? num : num2));
					break;
				case Stretch.UniformToFill:
					num = (num2 = ((num > num2) ? num : num2));
					break;
				}
			}
			switch (P_3)
			{
			case StretchDirection.UpOnly:
				if (num < 1.0)
				{
					num = 1.0;
				}
				if (num2 < 1.0)
				{
					num2 = 1.0;
				}
				break;
			case StretchDirection.DownOnly:
				if (num > 1.0)
				{
					num = 1.0;
				}
				if (num2 > 1.0)
				{
					num2 = 1.0;
				}
				break;
			}
		}
		return new Vector(num, num2);
	}

	public static Size CalculateSize(this Stretch P_0, Size P_1, Size P_2, StretchDirection P_3 = StretchDirection.Both)
	{
		return P_2 * P_0.CalculateScaling(P_1, P_2, P_3);
	}
}

