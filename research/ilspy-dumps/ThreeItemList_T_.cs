// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ThreeItemList<T>
using System;
using Avalonia.Utilities;

internal sealed class ThreeItemList<T> : FrugalListBase<T>
{
	private T _entry0;

	private T _entry1;

	private T _entry2;

	public override int Capacity => 3;

	public override FrugalListStoreState Add(T P_0)
	{
		switch (_count)
		{
		case 0:
			_entry0 = P_0;
			break;
		case 1:
			_entry1 = P_0;
			break;
		case 2:
			_entry2 = P_0;
			break;
		default:
			return FrugalListStoreState.SixItemList;
		}
		_count++;
		return FrugalListStoreState.Success;
	}

	public override void Insert(int P_0, T P_1)
	{
		if (_count < 3)
		{
			switch (P_0)
			{
			case 0:
				_entry2 = _entry1;
				_entry1 = _entry0;
				_entry0 = P_1;
				break;
			case 1:
				_entry2 = _entry1;
				_entry1 = P_1;
				break;
			case 2:
				_entry2 = P_1;
				break;
			default:
				throw new ArgumentOutOfRangeException("index");
			}
			_count++;
			return;
		}
		throw new ArgumentOutOfRangeException("index");
	}

	public override void SetAt(int P_0, T P_1)
	{
		switch (P_0)
		{
		case 0:
			_entry0 = P_1;
			break;
		case 1:
			_entry1 = P_1;
			break;
		case 2:
			_entry2 = P_1;
			break;
		default:
			throw new ArgumentOutOfRangeException("index");
		}
	}

	public override void RemoveAt(int P_0)
	{
		switch (P_0)
		{
		case 0:
			_entry0 = _entry1;
			_entry1 = _entry2;
			break;
		case 1:
			_entry1 = _entry2;
			break;
		default:
			throw new ArgumentOutOfRangeException("index");
		case 2:
			break;
		}
		_entry2 = default(T);
		_count--;
	}

	public override T EntryAt(int P_0)
	{
		return P_0 switch
		{
			0 => _entry0, 
			1 => _entry1, 
			2 => _entry2, 
			_ => throw new ArgumentOutOfRangeException("index"), 
		};
	}

	public override void Promote(FrugalListBase<T> P_0)
	{
		int count = P_0.Count;
		if (3 >= count)
		{
			SetCount(P_0.Count);
			switch (count)
			{
			case 3:
				SetAt(0, P_0.EntryAt(0));
				SetAt(1, P_0.EntryAt(1));
				SetAt(2, P_0.EntryAt(2));
				break;
			case 2:
				SetAt(0, P_0.EntryAt(0));
				SetAt(1, P_0.EntryAt(1));
				break;
			case 1:
				SetAt(0, P_0.EntryAt(0));
				break;
			default:
				throw new ArgumentOutOfRangeException("oldList");
			case 0:
				break;
			}
			return;
		}
		throw new ArgumentException($"Cannot promote from '{P_0}' to '{ToString()}' because the target map is too small.", "oldList");
	}

	private void SetCount(int P_0)
	{
		if (P_0 >= 0 && P_0 <= 3)
		{
			_count = P_0;
			return;
		}
		throw new ArgumentOutOfRangeException("value");
	}
}

