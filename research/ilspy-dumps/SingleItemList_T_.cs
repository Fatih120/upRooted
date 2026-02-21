// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SingleItemList<T>
using System;
using Avalonia.Utilities;

internal sealed class SingleItemList<T> : FrugalListBase<T>
{
	private T _loneEntry;

	public override int Capacity => 1;

	public override FrugalListStoreState Add(T P_0)
	{
		if (_count == 0)
		{
			_loneEntry = P_0;
			_count++;
			return FrugalListStoreState.Success;
		}
		return FrugalListStoreState.ThreeItemList;
	}

	public override void Insert(int P_0, T P_1)
	{
		if (_count < 1 && P_0 < 1)
		{
			_loneEntry = P_1;
			_count++;
			return;
		}
		throw new ArgumentOutOfRangeException("index");
	}

	public override void SetAt(int P_0, T P_1)
	{
		_loneEntry = P_1;
	}

	public override void RemoveAt(int P_0)
	{
		if (P_0 == 0)
		{
			_loneEntry = default(T);
			_count--;
			return;
		}
		throw new ArgumentOutOfRangeException("index");
	}

	public override T EntryAt(int P_0)
	{
		return _loneEntry;
	}

	public override void Promote(FrugalListBase<T> P_0)
	{
		if (1 == P_0.Count)
		{
			SetCount(1);
			SetAt(0, P_0.EntryAt(0));
			return;
		}
		throw new ArgumentException($"Cannot promote from '{P_0}' to '{ToString()}' because the target map is too small.", "oldList");
	}

	private void SetCount(int P_0)
	{
		if (P_0 >= 0 && P_0 <= 1)
		{
			_count = P_0;
			return;
		}
		throw new ArgumentOutOfRangeException("value");
	}
}

