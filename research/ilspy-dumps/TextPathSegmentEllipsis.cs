// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.TextPathSegmentEllipsis
using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;

public sealed class TextPathSegmentEllipsis : TextCollapsingProperties
{
	private readonly char[] _separators = new char[4]
	{
		Path.DirectorySeparatorChar,
		Path.AltDirectorySeparatorChar,
		'/',
		'\\'
	};

	public override double Width { get; }

	public override TextRun Symbol { get; }

	public override FlowDirection FlowDirection { get; }

	public TextPathSegmentEllipsis(string P_0, double P_1, TextRunProperties P_2, FlowDirection P_3)
	{
		Width = P_1;
		Symbol = new TextCharacters(P_0, P_2);
		FlowDirection = P_3;
	}

	public override TextRun[]? Collapse(TextLine P_0)
	{
		if (P_0.TextRuns.Count == 0)
		{
			return null;
		}
		FormattingObjectPool instance = FormattingObjectPool.Instance;
		ShapedTextRun shapedTextRun = TextFormatter.CreateSymbol(Symbol, FlowDirection);
		if (Width < shapedTextRun.Size.Width)
		{
			return null;
		}
		double width = P_0.Width;
		if (width <= Width)
		{
			return null;
		}
		FormattingObjectPool.RentedList<TextRun> rentedList = null;
		try
		{
			rentedList = instance.TextRunLists.Rent();
			LogicalTextRunEnumerator logicalTextRunEnumerator = new LogicalTextRunEnumerator(P_0);
			TextRun item;
			while (logicalTextRunEnumerator.MoveNext(out item))
			{
				rentedList.Add(item);
			}
			List<(int, int, double, bool)> list = new List<(int, int, double, bool)>();
			List<int> list2 = new List<int>();
			int num = 0;
			int num2 = 0;
			bool flag = false;
			for (int i = 0; i < rentedList.Count; i++)
			{
				TextRun textRun = rentedList[i];
				if (textRun is ShapedTextRun { Text: { Span: var span } })
				{
					int num3 = 0;
					while (num3 < span.Length)
					{
						char c = span[num3];
						if (IsSeparator(c))
						{
							if (!flag && num - num2 > 0)
							{
								double num4 = MeasureSegmentWidth(rentedList, num2, num - num2);
								list.Add((num2, num - num2, num4, false));
							}
							double num5 = MeasureSegmentWidth(rentedList, num, 1);
							list.Add((num, 1, num5, true));
							num2 = num + 1;
							flag = true;
						}
						else if (flag)
						{
							num2 = num;
							flag = false;
						}
						num3++;
						num++;
					}
				}
				else
				{
					if (flag)
					{
						num2 = num;
						flag = false;
					}
					num += textRun.Length;
				}
			}
			if (num - num2 > 0)
			{
				double num6 = MeasureSegmentWidth(rentedList, num2, num - num2);
				list.Add((num2, num - num2, num6, false));
			}
			if (list.Count == 0)
			{
				return null;
			}
			double[] array = new double[list.Count + 1];
			for (int j = 0; j < list.Count; j++)
			{
				(int, int, double, bool) tuple = list[j];
				double item2 = tuple.Item3;
				if (!tuple.Item4)
				{
					list2.Add(j);
				}
				array[j + 1] = array[j] + item2;
			}
			int num7 = num / 2;
			int num8 = 0;
			long num9 = long.MaxValue;
			for (int k = 0; k < list2.Count; k++)
			{
				(int, int, double, bool) tuple2 = list[list2[k]];
				int item3 = tuple2.Item1;
				int item4 = tuple2.Item2;
				int num10 = Math.Abs(item3 + item4 / 2 - num7);
				if (num10 < num9)
				{
					num9 = num10;
					num8 = k;
				}
			}
			int count = list2.Count;
			FormattingObjectPool.RentedList<TextRun> rentedList6;
			FormattingObjectPool.RentedList<TextRun> rentedList7;
			if (count > 0)
			{
				for (int l = 1; l <= count; l++)
				{
					int num11 = (l - 1) / 2;
					int num12 = num8 - num11;
					List<int> list3 = new List<int>();
					int num13 = Math.Max(0, num8 - (l - 1));
					int num14 = Math.Min(count - l, num8 + (l - 1));
					for (int num15 = num12; num15 >= num13; num15--)
					{
						list3.Add(num15);
					}
					for (int m = num12 + 1; m <= num14; m++)
					{
						list3.Add(m);
					}
					foreach (int item7 in list3)
					{
						if (item7 < 0 || item7 + l > count)
						{
							continue;
						}
						int num16 = item7;
						int num17 = item7 + l - 1;
						int num18 = list2[num16];
						int num19 = list2[num17];
						int item5 = list[num18].Item1;
						int num20 = num - (list[num19].Item1 + list[num19].Item2);
						if (item5 <= 0 || num20 <= 0)
						{
							continue;
						}
						double num21 = array[num19 + 1] - array[num18];
						if (!(width - num21 + shapedTextRun.Size.Width <= Width))
						{
							continue;
						}
						int item6 = list[num18].Item1;
						int num22 = list[num19].Item1 + list[num19].Item2 - item6;
						FormattingObjectPool.RentedList<TextRun> rentedList2 = null;
						FormattingObjectPool.RentedList<TextRun> rentedList3 = null;
						FormattingObjectPool.RentedList<TextRun> rentedList4 = null;
						FormattingObjectPool.RentedList<TextRun> rentedList5 = null;
						try
						{
							TextFormatterImpl.SplitTextRuns(rentedList, item6, instance).Deconstruct(out rentedList6, out rentedList7);
							rentedList2 = rentedList6;
							rentedList3 = rentedList7;
							if (rentedList3 == null)
							{
								return null;
							}
							TextFormatterImpl.SplitTextRuns(rentedList3, num22, instance).Deconstruct(out rentedList7, out rentedList6);
							rentedList4 = rentedList7;
							rentedList5 = rentedList6;
							TextRun[] array2 = new TextRun[(rentedList2?.Count ?? 0) + 1 + (rentedList5?.Count ?? 0)];
							int num23 = 0;
							if (rentedList2 != null)
							{
								foreach (TextRun item8 in rentedList2)
								{
									array2[num23++] = item8;
								}
							}
							array2[num23++] = shapedTextRun;
							if (rentedList5 != null)
							{
								foreach (TextRun item9 in rentedList5)
								{
									array2[num23++] = item9;
								}
							}
							return array2;
						}
						finally
						{
							instance.TextRunLists.Return(ref rentedList2);
							instance.TextRunLists.Return(ref rentedList3);
							instance.TextRunLists.Return(ref rentedList4);
							instance.TextRunLists.Return(ref rentedList5);
						}
					}
				}
			}
			int num24 = 0;
			double num25 = P_0.WidthIncludingTrailingWhitespace;
			for (int n = 0; n < list.Count; n++)
			{
				(int, int, double, bool) tuple3 = list[n];
				if (n < list.Count - 1 && num25 - tuple3.Item3 > Width)
				{
					num25 -= tuple3.Item3;
					num24 += tuple3.Item2;
					continue;
				}
				FormattingObjectPool.RentedList<TextRun> rentedList8 = null;
				FormattingObjectPool.RentedList<TextRun> rentedList9 = null;
				try
				{
					TextFormatterImpl.SplitTextRuns(rentedList, num24, instance).Deconstruct(out rentedList6, out rentedList7);
					rentedList8 = rentedList6;
					rentedList9 = rentedList7;
					TextRun textRun2 = null;
					int num26 = 0;
					if (rentedList9 != null && rentedList9.Count > 0)
					{
						num26 = Math.Max(0, rentedList9.Count - 1);
						if (rentedList9[0] is ShapedTextRun shapedTextRun3)
						{
							double num27 = Width - shapedTextRun.Size.Width;
							if (shapedTextRun3.TryMeasureCharactersBackwards(num27, out var num28, out var _))
							{
								int num30 = shapedTextRun3.Length - num28;
								(_, textRun2) = (SplitResult<ShapedTextRun>)(ref shapedTextRun3.Split(num30));
							}
						}
					}
					TextRun[] array3 = new TextRun[((textRun2 != null) ? 1 : 0) + 1 + num26];
					int num31 = 0;
					array3[num31++] = shapedTextRun;
					if (textRun2 != null)
					{
						array3[num31++] = textRun2;
					}
					if (rentedList9 != null)
					{
						for (int num32 = 1; num32 < rentedList9.Count; num32++)
						{
							TextRun textRun3 = rentedList9[num32];
							array3[num31++] = textRun3;
						}
					}
					return array3;
				}
				finally
				{
					instance.TextRunLists.Return(ref rentedList8);
					instance.TextRunLists.Return(ref rentedList9);
				}
			}
			return null;
		}
		finally
		{
			instance.TextRunLists.Return(ref rentedList);
		}
	}

	private bool IsSeparator(char P_0)
	{
		char[] separators = _separators;
		for (int i = 0; i < separators.Length; i++)
		{
			if (separators[i] == P_0)
			{
				return true;
			}
		}
		return false;
	}

	private static double MeasureSegmentWidth(IReadOnlyList<TextRun> P_0, int P_1, int P_2)
	{
		int num = P_1 + P_2;
		int num2 = 0;
		double num3 = 0.0;
		for (int i = 0; i < P_0.Count; i++)
		{
			TextRun textRun = P_0[i];
			int num4 = num2;
			int num5 = num4 + textRun.Length;
			if (num5 <= P_1)
			{
				num2 = num5;
				continue;
			}
			if (num4 >= num)
			{
				break;
			}
			int num6 = Math.Max(P_1, num4);
			int num7 = Math.Min(num, num5);
			int num8 = num7 - num6;
			if (num8 <= 0)
			{
				num2 = num5;
				continue;
			}
			if (!(textRun is ShapedTextRun shapedTextRun))
			{
				if (textRun is DrawableTextRun drawableTextRun && num8 >= drawableTextRun.Length)
				{
					num3 += drawableTextRun.Size.Width;
				}
			}
			else
			{
				ShapedBuffer shapedBuffer = shapedTextRun.ShapedBuffer;
				if (shapedBuffer.Length != 0)
				{
					int num9 = num6 - num4;
					int num10 = num7 - num4;
					int glyphCluster = shapedBuffer[0].GlyphCluster;
					for (int j = 0; j < shapedBuffer.Length; j++)
					{
						GlyphInfo glyphInfo = shapedBuffer[j];
						int num11 = glyphInfo.GlyphCluster - glyphCluster;
						if (num11 >= num9)
						{
							if (num11 >= num10)
							{
								break;
							}
							num3 += glyphInfo.GlyphAdvance;
						}
					}
				}
			}
			num2 = num5;
		}
		return num3;
	}
}

