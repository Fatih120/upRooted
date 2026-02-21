// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.InlineDictionary<TKey,TValue>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Avalonia.Utilities;

internal struct InlineDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where TKey : class
{
	private struct KeyValuePair(TKey P_0, TValue? P_1)
	{
		public TKey? Key = P_0;

		public TValue? Value = P_1;

		public static implicit operator KeyValuePair<TKey?, TValue?>(KeyValuePair P_0)
		{
			return new KeyValuePair<TKey, TValue>(P_0.Key, P_0.Value);
		}
	}

	public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
	{
		private enum Type
		{
			Empty,
			Single,
			Array,
			Dictionary
		}

		private Dictionary<TKey, TValue>.Enumerator _inner = default(Dictionary<TKey, TValue>.Enumerator);

		private readonly KeyValuePair[]? _arr = null;

		private KeyValuePair<TKey, TValue> _first = default(KeyValuePair<TKey, TValue>);

		private int _index = -1;

		private readonly Type _type;

		public KeyValuePair<TKey, TValue> Current
		{
			get
			{
				if (_type == Type.Single)
				{
					return _first;
				}
				if (_type == Type.Array)
				{
					return _arr[_index];
				}
				if (_type == Type.Dictionary)
				{
					return _inner.Current;
				}
				throw new InvalidOperationException();
			}
		}

		object IEnumerator.Current => Current;

		public Enumerator(InlineDictionary<TKey, TValue> P_0)
		{
			if (P_0._data is Dictionary<TKey, TValue> dictionary)
			{
				_inner = dictionary.GetEnumerator();
				_type = Type.Dictionary;
			}
			else if (P_0._data is KeyValuePair[] arr)
			{
				_type = Type.Array;
				_arr = arr;
			}
			else if (P_0._data != null)
			{
				_type = Type.Single;
				_first = new KeyValuePair<TKey, TValue>((TKey)P_0._data, P_0._value);
			}
			else
			{
				_type = Type.Empty;
			}
		}

		public bool MoveNext()
		{
			if (_type == Type.Single)
			{
				if (_index != -1)
				{
					return false;
				}
				_index = 0;
				return true;
			}
			if (_type == Type.Array)
			{
				for (int i = _index + 1; i < _arr.Length; i++)
				{
					if (_arr[i].Key != null)
					{
						_index = i;
						return true;
					}
				}
				return false;
			}
			if (_type == Type.Dictionary)
			{
				return _inner.MoveNext();
			}
			return false;
		}

		public void Reset()
		{
			_index = -1;
			if (_type == Type.Dictionary)
			{
				((IEnumerator)_inner).Reset();
			}
		}

		public void Dispose()
		{
		}
	}

	private object? _data;

	private TValue? _value;

	public TValue this[TKey P_0]
	{
		set
		{
			Set(val, val2);
		}
	}

	public bool HasEntries => _data != null;

	private void SetCore(TKey P_0, TValue P_1, bool P_2)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException();
		}
		if (_data == null)
		{
			_data = P_0;
			_value = P_1;
		}
		else if (_data is KeyValuePair[] array)
		{
			int num = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Key == P_0)
				{
					if (P_2)
					{
						array[i] = new KeyValuePair(P_0, P_1);
						return;
					}
					throw new ArgumentException("Key already exists in dictionary");
				}
				if (array[i].Key == null && num == -1)
				{
					num = i;
				}
			}
			if (num != -1)
			{
				array[num] = new KeyValuePair(P_0, P_1);
				return;
			}
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
			KeyValuePair[] array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				KeyValuePair keyValuePair = array2[j];
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			dictionary.Add(P_0, P_1);
			_data = dictionary;
		}
		else if (_data is Dictionary<TKey, TValue> dictionary2)
		{
			if (P_2)
			{
				dictionary2[P_0] = P_1;
			}
			else
			{
				dictionary2.Add(P_0, P_1);
			}
		}
		else
		{
			TKey val = (TKey)_data;
			if (val == P_0 && P_2)
			{
				_value = P_1;
				return;
			}
			_data = new KeyValuePair[6]
			{
				new KeyValuePair(val, _value),
				new KeyValuePair(P_0, P_1),
				default(KeyValuePair),
				default(KeyValuePair),
				default(KeyValuePair),
				default(KeyValuePair)
			};
			_value = default(TValue);
		}
	}

	public void Add(TKey P_0, TValue P_1)
	{
		SetCore(P_0, P_1, false);
	}

	public void Set(TKey P_0, TValue P_1)
	{
		SetCore(P_0, P_1, true);
	}

	public bool Remove(TKey P_0)
	{
		if (_data == P_0)
		{
			_data = null;
			_value = default(TValue);
			return true;
		}
		if (_data is KeyValuePair[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Key == P_0)
				{
					array[i] = default(KeyValuePair);
					return true;
				}
			}
			return false;
		}
		if (_data is Dictionary<TKey, TValue> dictionary)
		{
			return dictionary.Remove(P_0);
		}
		return false;
	}

	public bool TryGetValue(TKey P_0, [MaybeNullWhen(false)] out TValue P_1)
	{
		if (_data == P_0)
		{
			P_1 = _value;
			return true;
		}
		if (_data is KeyValuePair[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Key == P_0)
				{
					P_1 = array[i].Value;
					return true;
				}
			}
			P_1 = default(TValue);
			return false;
		}
		if (_data is Dictionary<TKey, TValue> dictionary)
		{
			return dictionary.TryGetValue(P_0, out P_1);
		}
		P_1 = default(TValue);
		return false;
	}

	[UnscopedRef]
	public ref TValue GetValueRefOrNullRef(TKey P_0)
	{
		if (_data == P_0)
		{
			return ref _value;
		}
		if (_data is KeyValuePair[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Key == P_0)
				{
					return ref array[i].Value;
				}
			}
			return ref Unsafe.NullRef<TValue>();
		}
		if (_data is Dictionary<TKey, TValue> dictionary)
		{
			return ref CollectionsMarshal.GetValueRefOrNullRef(dictionary, P_0);
		}
		return ref Unsafe.NullRef<TValue>();
	}

	[UnscopedRef]
	public ref TValue GetValueRefOrAddDefault(TKey P_0, [UnscopedRef] out bool P_1)
	{
		if (_data == null)
		{
			P_1 = false;
			_data = P_0;
			return ref _value;
		}
		if (_data == P_0)
		{
			P_1 = true;
			return ref _value;
		}
		if (_data is KeyValuePair[] array)
		{
			int num = -1;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Key == P_0)
				{
					P_1 = true;
					return ref array[i].Value;
				}
				if (num == -1 && array[i].Key == null)
				{
					num = i;
				}
			}
			if (num != -1)
			{
				array[num] = new KeyValuePair(P_0, default(TValue));
				P_1 = false;
				return ref array[num].Value;
			}
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
			KeyValuePair[] array2 = array;
			for (int j = 0; j < array2.Length; j++)
			{
				KeyValuePair keyValuePair = array2[j];
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			_data = dictionary;
			return ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary, P_0, out P_1);
		}
		if (_data is Dictionary<TKey, TValue> dictionary2)
		{
			return ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary2, P_0, out P_1);
		}
		KeyValuePair[] array3 = (KeyValuePair[])(_data = new KeyValuePair[6]
		{
			new KeyValuePair((TKey)_data, _value),
			new KeyValuePair(P_0, default(TValue)),
			default(KeyValuePair),
			default(KeyValuePair),
			default(KeyValuePair),
			default(KeyValuePair)
		});
		_value = default(TValue);
		P_1 = false;
		return ref array3[1].Value;
	}

	public bool TryGetAndRemoveValue(TKey P_0, [MaybeNullWhen(false)] out TValue P_1)
	{
		if (_data == P_0)
		{
			P_1 = _value;
			_value = default(TValue);
			_data = null;
			return true;
		}
		if (_data is KeyValuePair[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Key == P_0)
				{
					P_1 = array[i].Value;
					array[i] = default(KeyValuePair);
					return true;
				}
			}
			P_1 = default(TValue);
			return false;
		}
		if (_data is Dictionary<TKey, TValue> dictionary)
		{
			if (!dictionary.TryGetValue(P_0, out P_1))
			{
				return false;
			}
			dictionary.Remove(P_0);
		}
		P_1 = default(TValue);
		return false;
	}

	public TValue GetAndRemove(TKey P_0)
	{
		if (TryGetAndRemoveValue(P_0, out var result))
		{
			return result;
		}
		throw new KeyNotFoundException();
	}

	public Enumerator GetEnumerator()
	{
		return new Enumerator(this);
	}

	IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

