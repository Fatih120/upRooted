// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.GeometryBuilder
using System;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Media;
using Avalonia.Utilities;

internal class GeometryBuilder
{
	internal struct RoundedRectKeypoints
	{
		[CompilerGenerated]
		private Point _003CLeftTop_003Ek__BackingField;

		[CompilerGenerated]
		private Point _003CTopLeft_003Ek__BackingField;

		[CompilerGenerated]
		private Point _003CTopRight_003Ek__BackingField;

		[CompilerGenerated]
		private Point _003CRightTop_003Ek__BackingField;

		[CompilerGenerated]
		private Point _003CRightBottom_003Ek__BackingField;

		[CompilerGenerated]
		private Point _003CBottomRight_003Ek__BackingField;

		[CompilerGenerated]
		private Point _003CBottomLeft_003Ek__BackingField;

		[CompilerGenerated]
		private Point _003CLeftBottom_003Ek__BackingField;

		public Point LeftTop
		{
			[CompilerGenerated]
			readonly get
			{
				return _003CLeftTop_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CLeftTop_003Ek__BackingField = point;
			}
		}

		public Point TopLeft
		{
			[CompilerGenerated]
			readonly get
			{
				return _003CTopLeft_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CTopLeft_003Ek__BackingField = point;
			}
		}

		public Point TopRight
		{
			[CompilerGenerated]
			readonly get
			{
				return _003CTopRight_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CTopRight_003Ek__BackingField = point;
			}
		}

		public Point RightTop
		{
			[CompilerGenerated]
			readonly get
			{
				return _003CRightTop_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CRightTop_003Ek__BackingField = point;
			}
		}

		public Point RightBottom
		{
			[CompilerGenerated]
			readonly get
			{
				return _003CRightBottom_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CRightBottom_003Ek__BackingField = point;
			}
		}

		public Point BottomRight
		{
			[CompilerGenerated]
			readonly get
			{
				return _003CBottomRight_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CBottomRight_003Ek__BackingField = point;
			}
		}

		public Point BottomLeft
		{
			[CompilerGenerated]
			readonly get
			{
				return _003CBottomLeft_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CBottomLeft_003Ek__BackingField = point;
			}
		}

		public Point LeftBottom
		{
			[CompilerGenerated]
			readonly get
			{
				return _003CLeftBottom_003Ek__BackingField;
			}
			[CompilerGenerated]
			set
			{
				_003CLeftBottom_003Ek__BackingField = point;
			}
		}

		public RoundedRectKeypoints()
		{
			LeftTop = default(Point);
			TopLeft = default(Point);
			TopRight = default(Point);
			RightTop = default(Point);
			RightBottom = default(Point);
			BottomRight = default(Point);
			BottomLeft = default(Point);
			LeftBottom = default(Point);
		}
	}

	public static void DrawRoundedCornersRectangle(StreamGeometryContext P_0, ref RoundedRectKeypoints P_1)
	{
		P_0.BeginFigure(P_1.TopLeft, true);
		P_0.LineTo(P_1.TopRight);
		double num = P_1.RightTop.X - P_1.TopRight.X;
		double num2 = P_1.TopRight.Y - P_1.RightTop.Y;
		num = ((num > 0.0) ? num : (0.0 - num));
		num2 = ((num2 > 0.0) ? num2 : (0.0 - num2));
		P_0.ArcTo(P_1.RightTop, new Size(num, num2), 0.0, false, SweepDirection.Clockwise);
		P_0.LineTo(P_1.RightBottom);
		num = P_1.RightBottom.X - P_1.BottomRight.X;
		num2 = P_1.BottomRight.Y - P_1.RightBottom.Y;
		num = ((num > 0.0) ? num : (0.0 - num));
		num2 = ((num2 > 0.0) ? num2 : (0.0 - num2));
		if (num != 0.0 || num2 != 0.0)
		{
			P_0.ArcTo(P_1.BottomRight, new Size(num, num2), 0.0, false, SweepDirection.Clockwise);
		}
		P_0.LineTo(P_1.BottomLeft);
		num = P_1.BottomLeft.X - P_1.LeftBottom.X;
		num2 = P_1.BottomLeft.Y - P_1.LeftBottom.Y;
		num = ((num > 0.0) ? num : (0.0 - num));
		num2 = ((num2 > 0.0) ? num2 : (0.0 - num2));
		if (num != 0.0 || num2 != 0.0)
		{
			P_0.ArcTo(P_1.LeftBottom, new Size(num, num2), 0.0, false, SweepDirection.Clockwise);
		}
		P_0.LineTo(P_1.LeftTop);
		num = P_1.TopLeft.X - P_1.LeftTop.X;
		num2 = P_1.TopLeft.Y - P_1.LeftTop.Y;
		num = ((num > 0.0) ? num : (0.0 - num));
		num2 = ((num2 > 0.0) ? num2 : (0.0 - num2));
		if (num != 0.0 || num2 != 0.0)
		{
			P_0.ArcTo(P_1.TopLeft, new Size(num, num2), 0.0, false, SweepDirection.Clockwise);
		}
		P_0.EndFigure(true);
	}

	public static void DrawRoundedCornersRectangle(StreamGeometryContext P_0, Rect P_1, double P_2, double P_3)
	{
		Size size = new Size(P_2, P_3);
		P_0.BeginFigure(new Point(P_1.Left + P_2, P_1.Top), true);
		P_0.LineTo(new Point(P_1.Right - P_2, P_1.Top));
		P_0.ArcTo(new Point(P_1.Right, P_1.Top + P_3), size, 1.57079633, false, SweepDirection.Clockwise);
		P_0.LineTo(new Point(P_1.Right, P_1.Bottom - P_3));
		P_0.ArcTo(new Point(P_1.Right - P_2, P_1.Bottom), size, 1.57079633, false, SweepDirection.Clockwise);
		P_0.LineTo(new Point(P_1.Left + P_2, P_1.Bottom));
		P_0.ArcTo(new Point(P_1.Left, P_1.Bottom - P_3), size, 1.57079633, false, SweepDirection.Clockwise);
		P_0.LineTo(new Point(P_1.Left, P_1.Top + P_3));
		P_0.ArcTo(new Point(P_1.Left + P_2, P_1.Top), size, 1.57079633, false, SweepDirection.Clockwise);
		P_0.EndFigure(true);
	}

	public static RoundedRectKeypoints CalculateRoundedCornersRectangleWinUI(Rect P_0, Thickness P_1, CornerRadius P_2, BackgroundSizing P_3)
	{
		Rect rect = P_0;
		bool flag;
		switch (P_3)
		{
		case BackgroundSizing.InnerBorderEdge:
			rect = P_0.Deflate(P_1);
			flag = false;
			break;
		case BackgroundSizing.OuterBorderEdge:
			flag = true;
			break;
		default:
			rect = P_0.Deflate(P_1 * 0.5);
			flag = false;
			break;
		}
		double num;
		double num2;
		double num3;
		double num4;
		if (P_1 != default(Thickness))
		{
			num = 0.5 * P_1.Left;
			num2 = 0.5 * P_1.Right;
			num3 = 0.5 * P_1.Top;
			num4 = 0.5 * P_1.Bottom;
		}
		else
		{
			num = 0.0;
			num2 = 0.0;
			num3 = 0.0;
			num4 = 0.0;
		}
		double num5;
		double num6;
		double num7;
		double num8;
		double num9;
		double num10;
		double num11;
		double num12;
		if (flag)
		{
			if (MathUtilities.AreClose(P_2.TopLeft, 0.0, 1.53E-06))
			{
				num5 = 0.0;
				num6 = 0.0;
			}
			else
			{
				num5 = P_2.TopLeft + num;
				num6 = P_2.TopLeft + num3;
			}
			if (MathUtilities.AreClose(P_2.TopRight, 0.0, 1.53E-06))
			{
				num7 = 0.0;
				num8 = 0.0;
			}
			else
			{
				num7 = P_2.TopRight + num3;
				num8 = P_2.TopRight + num2;
			}
			if (MathUtilities.AreClose(P_2.BottomRight, 0.0, 1.53E-06))
			{
				num9 = 0.0;
				num10 = 0.0;
			}
			else
			{
				num9 = P_2.BottomRight + num2;
				num10 = P_2.BottomRight + num4;
			}
			if (MathUtilities.AreClose(P_2.BottomLeft, 0.0, 1.53E-06))
			{
				num11 = 0.0;
				num12 = 0.0;
			}
			else
			{
				num11 = P_2.BottomLeft + num4;
				num12 = P_2.BottomLeft + num;
			}
		}
		else
		{
			num5 = Math.Max(0.0, P_2.TopLeft - num);
			num6 = Math.Max(0.0, P_2.TopLeft - num3);
			num7 = Math.Max(0.0, P_2.TopRight - num3);
			num8 = Math.Max(0.0, P_2.TopRight - num2);
			num9 = Math.Max(0.0, P_2.BottomRight - num2);
			num10 = Math.Max(0.0, P_2.BottomRight - num4);
			num11 = Math.Max(0.0, P_2.BottomLeft - num4);
			num12 = Math.Max(0.0, P_2.BottomLeft - num);
		}
		double num13 = num5;
		double num14 = 0.0;
		double num15 = rect.Width - num8;
		double num16 = 0.0;
		double width = rect.Width;
		double num17 = num7;
		double width2 = rect.Width;
		double num18 = rect.Height - num10;
		double num19 = rect.Width - num9;
		double height = rect.Height;
		double num20 = num12;
		double height2 = rect.Height;
		double num21 = 0.0;
		double num22 = rect.Height - num11;
		double num23 = 0.0;
		double num24 = num6;
		if (num13 > num15)
		{
			num15 = (num13 = num5 / (num5 + num8) * rect.Width);
		}
		if (num17 > num18)
		{
			num18 = (num17 = num7 / (num7 + num10) * rect.Height);
		}
		if (num19 < num20)
		{
			num20 = (num19 = num12 / (num12 + num9) * rect.Width);
		}
		if (num22 < num24)
		{
			num24 = (num22 = num6 / (num6 + num11) * rect.Height);
		}
		RoundedRectKeypoints result = new RoundedRectKeypoints();
		result.TopLeft = new Point(rect.X + num13, rect.Y + num14);
		result.TopRight = new Point(rect.X + num15, rect.Y + num16);
		result.RightTop = new Point(rect.X + width, rect.Y + num17);
		result.RightBottom = new Point(rect.X + width2, rect.Y + num18);
		result.BottomRight = new Point(rect.X + num19, rect.Y + height);
		result.BottomLeft = new Point(rect.X + num20, rect.Y + height2);
		result.LeftBottom = new Point(rect.X + num21, rect.Y + num22);
		result.LeftTop = new Point(rect.X + num23, rect.Y + num24);
		return result;
	}
}

