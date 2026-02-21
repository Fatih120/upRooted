// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.BoxShadows
using System;
using System.ComponentModel;
using System.Text;
using Avalonia;
using Avalonia.Media;
using Avalonia.Utilities;

public struct BoxShadows
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct BoxShadowsEnumerator(BoxShadows P_0)
	{
		private int _index = -1;

		private readonly BoxShadows _shadows = P_0;

		public BoxShadow Current => _shadows[_index];

		public bool MoveNext()
		{
			_index++;
			return _index < _shadows.Count;
		}
	}

	private readonly BoxShadow _first;

	private readonly BoxShadow[]? _list;

	public int Count { get; }

	public BoxShadow this[int P_0]
	{
		get
		{
			if (P_0 < 0 || P_0 >= Count)
			{
				throw new IndexOutOfRangeException();
			}
			if (P_0 == 0)
			{
				return _first;
			}
			return _list[P_0 - 1];
		}
	}

	public bool HasInsetShadows
	{
		get
		{
			BoxShadowsEnumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				BoxShadow current = enumerator.Current;
				if (current != default(BoxShadow) && current.IsInset)
				{
					return true;
				}
			}
			return false;
		}
	}

	public BoxShadows(BoxShadow P_0)
	{
		_first = P_0;
		_list = null;
		Count = ((!(_first == default(BoxShadow))) ? 1 : 0);
	}

	public BoxShadows(BoxShadow P_0, BoxShadow[] P_1)
	{
		_first = P_0;
		_list = P_1;
		Count = 1 + ((P_1 != null) ? P_1.Length : 0);
	}

	public override string ToString()
	{
		if (Count == 0)
		{
			return "none";
		}
		StringBuilder stringBuilder = StringBuilderCache.Acquire();
		BoxShadowsEnumerator enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.ToString(stringBuilder);
			stringBuilder.Append(',');
			stringBuilder.Append(' ');
		}
		stringBuilder.Remove(stringBuilder.Length - 2, 2);
		return StringBuilderCache.GetStringAndRelease(stringBuilder);
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public BoxShadowsEnumerator GetEnumerator()
	{
		return new BoxShadowsEnumerator(this);
	}

	public static BoxShadows Parse(string P_0)
	{
		string[] array = StringSplitter.SplitRespectingBrackets(P_0, ',', '(', ')', StringSplitOptions.RemoveEmptyEntries);
		if (array.Length == 0 || (array.Length == 1 && (string.IsNullOrWhiteSpace(array[0]) || array[0] == "none")))
		{
			return default(BoxShadows);
		}
		BoxShadow boxShadow = BoxShadow.Parse(array[0]);
		if (array.Length == 1)
		{
			return new BoxShadows(boxShadow);
		}
		BoxShadow[] array2 = new BoxShadow[array.Length - 1];
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i] = BoxShadow.Parse(array[i + 1]);
		}
		return new BoxShadows(boxShadow, array2);
	}

	public Rect TransformBounds(in Rect P_0)
	{
		Rect result = P_0;
		BoxShadowsEnumerator enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			result = result.Union(enumerator.Current.TransformBounds(in P_0));
		}
		return result;
	}

	public bool Equals(BoxShadows P_0)
	{
		if (P_0.Count != Count)
		{
			return false;
		}
		for (int i = 0; i < Count; i++)
		{
			if (!this[i].Equals(P_0[i]))
			{
				return false;
			}
		}
		return true;
	}

	public override bool Equals(object? P_0)
	{
		if (P_0 is BoxShadows boxShadows)
		{
			return Equals(boxShadows);
		}
		return false;
	}

	public override int GetHashCode()
	{
		int num = 0;
		BoxShadowsEnumerator enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			num = (num * 397) ^ enumerator.Current.GetHashCode();
		}
		return num;
	}

	public static bool operator ==(BoxShadows P_0, BoxShadows P_1)
	{
		return P_0.Equals(P_1);
	}

	public static bool operator !=(BoxShadows P_0, BoxShadows P_1)
	{
		return !(P_0 == P_1);
	}
}

