// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ArrayItemList<T>
using System;
using Avalonia.Utilities;

internal sealed class ArrayItemList<T> : FrugalListBase<T>
{
	private T[] _entries;

	public override int Capacity
	{
		get
		{
			T[] entries = _entries;
			if (entries == null)
			{
				return 0;
			}
			return entries.Length;
		}
	}

	public ArrayItemList(int P_0)
	{
		P_0 += 2;
		P_0 -= P_0 % 3;
		_entries = new T[P_0];
	}

	public override FrugalListStoreState Add(T P_0)
	{
		if (_entries != null && _count < _entries.Length)
		{
			_entries[_count] = P_0;
			_count++;
		}
		else
		{
			if (_entries != null)
			{
				int num = _entries.Length;
				num = ((num >= 18) ? (num + (num >> 2)) : (num + 3));
				T[] array = new T[num];
				Array.Copy(_entries, 0, array, 0, _entries.Length);
				_entries = array;
			}
			else
			{
				_entries = new T[9];
			}
			_entries[_count] = P_0;
			_count++;
		}
		return FrugalListStoreState.Success;
	}

	public override void Insert(int P_0, T P_1)
	{
		if (_entries != null && _count < _entries.Length)
		{
			Array.Copy(_entries, P_0, _entries, P_0 + 1, _count - P_0);
			_entries[P_0] = P_1;
			_count++;
			return;
		}
		throw new ArgumentOutOfRangeException("index");
	}

	public override void SetAt(int P_0, T P_1)
	{
		_entries[P_0] = P_1;
	}

	public override void RemoveAt(int P_0)
	{
		int num = _count - P_0 - 1;
		if (num > 0)
		{
			Array.Copy(_entries, P_0 + 1, _entries, P_0, num);
		}
		_entries[_count - 1] = default(T);
		_count--;
	}

	public override T EntryAt(int P_0)
	{
		return _entries[P_0];
	}

	public override void Promote(FrugalListBase<T> P_0)
	{
		for (int i = 0; i < P_0.Count; i++)
		{
			if (Add(P_0.EntryAt(i)) != FrugalListStoreState.Success)
			{
				throw new ArgumentException($"Cannot promote from '{P_0}' to '{ToString()}' because the target map is too small.", "oldList");
			}
		}
	}
}

