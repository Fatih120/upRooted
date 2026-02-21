// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SixItemList<T>
using System;
using Avalonia.Utilities;

internal sealed class SixItemList<T> : FrugalListBase<T>
{
	private T _entry0;

	private T _entry1;

	private T _entry2;

	private T _entry3;

	private T _entry4;

	private T _entry5;

	public override int Capacity => 6;

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
		case 3:
			_entry3 = P_0;
			break;
		case 4:
			_entry4 = P_0;
			break;
		case 5:
			_entry5 = P_0;
			break;
		default:
			return FrugalListStoreState.Array;
		}
		_count++;
		return FrugalListStoreState.Success;
	}

	public override void Insert(int P_0, T P_1)
	{
		if (_count < 6)
		{
			switch (P_0)
			{
			case 0:
				_entry5 = _entry4;
				_entry4 = _entry3;
				_entry3 = _entry2;
				_entry2 = _entry1;
				_entry1 = _entry0;
				_entry0 = P_1;
				break;
			case 1:
				_entry5 = _entry4;
				_entry4 = _entry3;
				_entry3 = _entry2;
				_entry2 = _entry1;
				_entry1 = P_1;
				break;
			case 2:
				_entry5 = _entry4;
				_entry4 = _entry3;
				_entry3 = _entry2;
				_entry2 = P_1;
				break;
			case 3:
				_entry5 = _entry4;
				_entry4 = _entry3;
				_entry3 = P_1;
				break;
			case 4:
				_entry5 = _entry4;
				_entry4 = P_1;
				break;
			case 5:
				_entry5 = P_1;
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
		case 3:
			_entry3 = P_1;
			break;
		case 4:
			_entry4 = P_1;
			break;
		case 5:
			_entry5 = P_1;
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
			_entry2 = _entry3;
			_entry3 = _entry4;
			_entry4 = _entry5;
			break;
		case 1:
			_entry1 = _entry2;
			_entry2 = _entry3;
			_entry3 = _entry4;
			_entry4 = _entry5;
			break;
		case 2:
			_entry2 = _entry3;
			_entry3 = _entry4;
			_entry4 = _entry5;
			break;
		case 3:
			_entry3 = _entry4;
			_entry4 = _entry5;
			break;
		case 4:
			_entry4 = _entry5;
			break;
		default:
			throw new ArgumentOutOfRangeException("index");
		case 5:
			break;
		}
		_entry5 = default(T);
		_count--;
	}

	public override T EntryAt(int P_0)
	{
		return P_0 switch
		{
			0 => _entry0, 
			1 => _entry1, 
			2 => _entry2, 
			3 => _entry3, 
			4 => _entry4, 
			5 => _entry5, 
			_ => throw new ArgumentOutOfRangeException("index"), 
		};
	}

	public override void Promote(FrugalListBase<T> P_0)
	{
		int count = P_0.Count;
		if (6 >= count)
		{
			SetCount(P_0.Count);
			switch (count)
			{
			case 6:
				SetAt(0, P_0.EntryAt(0));
				SetAt(1, P_0.EntryAt(1));
				SetAt(2, P_0.EntryAt(2));
				SetAt(3, P_0.EntryAt(3));
				SetAt(4, P_0.EntryAt(4));
				SetAt(5, P_0.EntryAt(5));
				break;
			case 5:
				SetAt(0, P_0.EntryAt(0));
				SetAt(1, P_0.EntryAt(1));
				SetAt(2, P_0.EntryAt(2));
				SetAt(3, P_0.EntryAt(3));
				SetAt(4, P_0.EntryAt(4));
				break;
			case 4:
				SetAt(0, P_0.EntryAt(0));
				SetAt(1, P_0.EntryAt(1));
				SetAt(2, P_0.EntryAt(2));
				SetAt(3, P_0.EntryAt(3));
				break;
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
		if (P_0 >= 0 && P_0 <= 6)
		{
			_count = P_0;
			return;
		}
		throw new ArgumentOutOfRangeException("value");
	}
}

