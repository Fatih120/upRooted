// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpanVector
using System;
using System.Collections;
using Avalonia.Utilities;

internal sealed class SpanVector : IEnumerable
{
	private static readonly Equals s_referenceEquals = object.ReferenceEquals;

	private static readonly Equals s_equals = object.Equals;

	private FrugalStructList<Span> _spans;

	public int Count => _spans.Count;

	public object? Default { get; }

	public Span this[int P_0] => _spans[P_0];

	internal SpanVector(object? P_0, FrugalStructList<Span> P_1 = default(FrugalStructList<Span>))
	{
		Default = P_0;
		_spans = P_1;
	}

	public IEnumerator GetEnumerator()
	{
		return new SpanEnumerator(this);
	}

	private void Add(Span P_0)
	{
		_spans.Add(P_0);
	}

	private void DeleteInternal(int P_0, int P_1)
	{
		for (int num = P_0 + P_1 - 1; num >= P_0; num--)
		{
			_spans.RemoveAt(num);
		}
	}

	private void Insert(int P_0, int P_1)
	{
		for (int i = 0; i < P_1; i++)
		{
			_spans.Insert(P_0, new Span(null, 0));
		}
	}

	internal bool FindSpan(int P_0, SpanPosition P_1, out SpanPosition P_2)
	{
		int count = _spans.Count;
		int i;
		int num;
		if (P_0 == 0)
		{
			i = 0;
			num = 0;
		}
		else if (P_0 >= P_1.Offset || P_0 * 2 < P_1.Offset)
		{
			if (P_0 >= P_1.Offset)
			{
				i = P_1.Index;
				num = P_1.Offset;
			}
			else
			{
				i = 0;
				num = 0;
			}
			for (; i < count; i++)
			{
				int length = _spans[i].length;
				if (P_0 < num + length)
				{
					break;
				}
				num += length;
			}
		}
		else
		{
			i = P_1.Index;
			for (num = P_1.Offset; num > P_0; num -= _spans[--i].length)
			{
			}
		}
		P_2 = new SpanPosition(i, num);
		return i != count;
	}

	public SpanPosition SetValue(int P_0, int P_1, object P_2, SpanPosition P_3)
	{
		return Set(P_0, P_1, P_2, s_equals, P_3);
	}

	private SpanPosition Set(int P_0, int P_1, object? P_2, Equals P_3, SpanPosition P_4)
	{
		bool num = FindSpan(P_0, P_4, out P_4);
		int num2 = P_4.Index;
		int num3 = P_4.Offset;
		if (!num)
		{
			if (num3 < P_0)
			{
				Add(new Span(Default, P_0 - num3));
			}
			if (Count > 0 && P_3(_spans[Count - 1].element, P_2))
			{
				_spans[Count - 1].length += P_1;
				if (num2 == Count)
				{
					num3 += P_1;
				}
			}
			else
			{
				Add(new Span(P_2, P_1));
			}
		}
		else
		{
			int i = num2;
			int num4;
			for (num4 = num3; i < Count && num4 + _spans[i].length <= P_0 + P_1; i++)
			{
				num4 += _spans[i].length;
			}
			if (P_0 == num3)
			{
				if (num2 > 0 && P_3(_spans[num2 - 1].element, P_2))
				{
					num2--;
					num3 -= _spans[num2].length;
					P_0 = num3;
					P_1 += _spans[num2].length;
				}
			}
			else if (P_3(_spans[num2].element, P_2))
			{
				P_1 = P_0 + P_1 - num3;
				P_0 = num3;
			}
			if (i < Count && P_3(_spans[i].element, P_2))
			{
				P_1 = num4 + _spans[i].length - P_0;
				num4 += _spans[i].length;
				i++;
			}
			if (i >= Count)
			{
				if (num3 < P_0)
				{
					if (Count != num2 + 2 && !Resize(num2 + 2))
					{
						throw new OutOfMemoryException();
					}
					_spans[num2].length = P_0 - num3;
					_spans[num2 + 1] = new Span(P_2, P_1);
				}
				else
				{
					if (Count != num2 + 1 && !Resize(num2 + 1))
					{
						throw new OutOfMemoryException();
					}
					_spans[num2] = new Span(P_2, P_1);
				}
			}
			else
			{
				object obj = null;
				int num5 = 0;
				if (P_0 + P_1 > num4)
				{
					obj = _spans[i].element;
					num5 = num4 + _spans[i].length - (P_0 + P_1);
				}
				int num6 = 1 + ((P_0 > num3) ? 1 : 0) - (i - num2);
				if (num6 < 0)
				{
					DeleteInternal(num2 + 1, -num6);
				}
				else if (num6 > 0)
				{
					Insert(num2 + 1, num6);
					for (int j = 0; j < num6; j++)
					{
						_spans[num2 + 1 + j] = new Span(null, 0);
					}
				}
				if (num3 < P_0)
				{
					_spans[num2].length = P_0 - num3;
					num2++;
					num3 = P_0;
				}
				_spans[num2] = new Span(P_2, P_1);
				num2++;
				num3 += P_1;
				if (num4 < P_0 + P_1)
				{
					_spans[num2] = new Span(obj, num5);
				}
			}
		}
		return new SpanPosition(num2, num3);
	}

	private bool Resize(int P_0)
	{
		if (P_0 > Count)
		{
			for (int i = 0; i < P_0 - Count; i++)
			{
				_spans.Add(new Span(null, 0));
			}
		}
		else if (P_0 < Count)
		{
			DeleteInternal(P_0, Count - P_0);
		}
		return true;
	}
}

