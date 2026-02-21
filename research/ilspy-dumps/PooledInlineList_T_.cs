// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.PooledInlineList<T>
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

internal struct PooledInlineList<T> : IDisposable, IEnumerable<T>, IEnumerable where T : class
{
	private class SimplePooledList : IDisposable
	{
		public int Count;

		public T[]? Items;

		public void Add(T P_0)
		{
			if (Items == null)
			{
				Items = ArrayPool<T>.Shared.Rent(4);
			}
			else if (Count == Items.Length)
			{
				GrowItems(Count * 2);
			}
			Items[Count] = P_0;
			Count++;
		}

		private void ReturnToPool(T[] P_0)
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				Array.Clear(P_0, 0, Count);
			}
			ArrayPool<T>.Shared.Return(P_0);
		}

		private void GrowItems(int P_0)
		{
			if (P_0 >= Count)
			{
				T[] array = ArrayPool<T>.Shared.Rent(P_0);
				Array.Copy(Items, array, Count);
				ReturnToPool(Items);
				Items = array;
			}
		}

		public void EnsureCapacity(int P_0)
		{
			if (Items == null)
			{
				Items = ArrayPool<T>.Shared.Rent(P_0);
			}
			else if (Items.Length < P_0)
			{
				GrowItems(P_0);
			}
		}

		public void Dispose()
		{
			if (Items != null)
			{
				ReturnToPool(Items);
				Items = null;
				Count = 0;
			}
		}
	}

	public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
	{
		private readonly T? _singleItem;

		private int _index;

		private readonly SimplePooledList? _list;

		object IEnumerator.Current => Current;

		public T Current
		{
			get
			{
				if (_list != null)
				{
					return _list.Items[_index];
				}
				return _singleItem;
			}
		}

		public Enumerator(object? P_0)
		{
			_singleItem = null;
			_index = -1;
			_list = P_0 as SimplePooledList;
			if (_list == null)
			{
				_singleItem = (T)P_0;
			}
		}

		public bool MoveNext()
		{
			if (_singleItem != null)
			{
				if (_index >= 0)
				{
					return false;
				}
				_index = 0;
				return true;
			}
			if (_list != null)
			{
				if (_index >= _list.Count - 1)
				{
					return false;
				}
				_index++;
				return true;
			}
			return false;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		public void Dispose()
		{
		}
	}

	private object? _item;

	public int Count
	{
		get
		{
			if (_item != null)
			{
				if (!(_item is SimplePooledList simplePooledList))
				{
					return 1;
				}
				return simplePooledList.Count;
			}
			return 0;
		}
	}

	public void Add(T P_0)
	{
		if (_item == null)
		{
			_item = P_0;
			return;
		}
		if (_item is SimplePooledList simplePooledList)
		{
			simplePooledList.Add(P_0);
			return;
		}
		ConvertToList();
		Add(P_0);
	}

	private void ConvertToList()
	{
		if (!(_item is SimplePooledList))
		{
			SimplePooledList simplePooledList = new SimplePooledList();
			if (_item != null)
			{
				simplePooledList.Add((T)_item);
			}
			_item = simplePooledList;
		}
	}

	public void EnsureCapacity(int P_0)
	{
		if (P_0 >= 2)
		{
			ConvertToList();
			((SimplePooledList)_item).EnsureCapacity(P_0);
		}
	}

	public void Dispose()
	{
		if (_item is SimplePooledList simplePooledList)
		{
			simplePooledList.Dispose();
		}
		_item = null;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return GetEnumerator();
	}

	public Enumerator GetEnumerator()
	{
		return new Enumerator(_item);
	}
}

