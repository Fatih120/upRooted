// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.FrugalStructList<T>
using System;
using Avalonia.Utilities;

internal struct FrugalStructList<T>
{
	internal FrugalListBase<T> _listStore = null;

	public int Capacity
	{
		get
		{
			if (_listStore != null)
			{
				return _listStore.Capacity;
			}
			return 0;
		}
		set
		{
			int num = 0;
			if (_listStore != null)
			{
				num = _listStore.Capacity;
			}
			if (num < num2)
			{
				FrugalListBase<T> frugalListBase = ((num2 == 1) ? new SingleItemList<T>() : ((num2 <= 3) ? new ThreeItemList<T>() : ((num2 > 6) ? ((FrugalListBase<T>)new ArrayItemList<T>(num2)) : ((FrugalListBase<T>)new SixItemList<T>()))));
				if (_listStore != null)
				{
					frugalListBase.Promote(_listStore);
				}
				_listStore = frugalListBase;
			}
		}
	}

	public int Count => _listStore?.Count ?? 0;

	public T this[int P_0]
	{
		get
		{
			if (_listStore != null && P_0 < _listStore.Count && P_0 >= 0)
			{
				return _listStore.EntryAt(P_0);
			}
			throw new ArgumentOutOfRangeException("index");
		}
		set
		{
			if (_listStore != null && num < _listStore.Count && num >= 0)
			{
				_listStore.SetAt(num, val);
				return;
			}
			throw new ArgumentOutOfRangeException("index");
		}
	}

	public FrugalStructList(int P_0)
	{
		Capacity = P_0;
	}

	public int Add(T P_0)
	{
		if (_listStore == null)
		{
			_listStore = new SingleItemList<T>();
		}
		switch (_listStore.Add(P_0))
		{
		case FrugalListStoreState.ThreeItemList:
		{
			ThreeItemList<T> threeItemList = new ThreeItemList<T>();
			threeItemList.Promote(_listStore);
			threeItemList.Add(P_0);
			_listStore = threeItemList;
			break;
		}
		case FrugalListStoreState.SixItemList:
		{
			SixItemList<T> sixItemList = new SixItemList<T>();
			sixItemList.Promote(_listStore);
			_listStore = sixItemList;
			sixItemList.Add(P_0);
			_listStore = sixItemList;
			break;
		}
		case FrugalListStoreState.Array:
		{
			ArrayItemList<T> arrayItemList = new ArrayItemList<T>(_listStore.Count + 1);
			arrayItemList.Promote(_listStore);
			_listStore = arrayItemList;
			arrayItemList.Add(P_0);
			_listStore = arrayItemList;
			break;
		}
		default:
			throw new InvalidOperationException("Cannot promote from Array.");
		case FrugalListStoreState.Success:
			break;
		}
		return _listStore.Count - 1;
	}

	public void Insert(int P_0, T P_1)
	{
		if (P_0 == 0 || (_listStore != null && P_0 <= _listStore.Count && P_0 >= 0))
		{
			int capacity = 1;
			if (_listStore != null && _listStore.Count == _listStore.Capacity)
			{
				capacity = Capacity + 1;
			}
			Capacity = capacity;
			_listStore.Insert(P_0, P_1);
			return;
		}
		throw new ArgumentOutOfRangeException("index");
	}

	public void RemoveAt(int P_0)
	{
		if (_listStore != null && P_0 < _listStore.Count && P_0 >= 0)
		{
			_listStore.RemoveAt(P_0);
			return;
		}
		throw new ArgumentOutOfRangeException("index");
	}
}

