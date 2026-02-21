// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ArrayBuilder<T>
using System;
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

internal struct ArrayBuilder<T>
{
	private T[]? _data;

	private int _size;

	public int Length
	{
		get
		{
			return _size;
		}
		set
		{
			if (num != _size)
			{
				if (num > 0)
				{
					EnsureCapacity(num);
					_size = num;
				}
				else
				{
					_size = 0;
				}
			}
		}
	}

	public int Capacity
	{
		get
		{
			T[]? data = _data;
			if (data == null)
			{
				return 0;
			}
			return data.Length;
		}
	}

	public ref T this[int P_0]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return ref _data[P_0];
		}
	}

	public ArraySlice<T> Add(int P_0, bool P_1 = true)
	{
		int size = _size;
		Length += P_0;
		ArraySlice<T> result = AsSlice(size, Length - size);
		if (P_1)
		{
			result.Span.Clear();
		}
		return result;
	}

	public ArraySlice<T> Add(in ArraySlice<T> P_0)
	{
		int size = _size;
		Length += P_0.Length;
		ArraySlice<T> result = AsSlice(size, Length - size);
		P_0.Span.CopyTo(result.Span);
		return result;
	}

	public void AddItem(T P_0)
	{
		int num = Length++;
		_data[num] = P_0;
	}

	public void Clear()
	{
		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
		{
			ClearArray();
		}
		else
		{
			_size = 0;
		}
	}

	private void ClearArray()
	{
		int size = _size;
		_size = 0;
		if (size > 0)
		{
			Array.Clear(_data, 0, size);
		}
	}

	private void EnsureCapacity(int P_0)
	{
		T[]? data = _data;
		int num = ((data != null) ? data.Length : 0);
		if (num < P_0)
		{
			uint num2 = ((num == 0) ? 4u : ((uint)(num * 2)));
			if (num2 > 2146435071)
			{
				num2 = 2146435071u;
			}
			if (num2 < P_0)
			{
				num2 = (uint)P_0;
			}
			T[] array = new T[num2];
			if (_size > 0)
			{
				Array.Copy(_data, array, _size);
			}
			_data = array;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ArraySlice<T> AsSlice()
	{
		return AsSlice(Length);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ArraySlice<T> AsSlice(int P_0)
	{
		return new ArraySlice<T>(_data, 0, P_0);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ArraySlice<T> AsSlice(int P_0, int P_1)
	{
		return new ArraySlice<T>(_data, P_0, P_1);
	}
}

