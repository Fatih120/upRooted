// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.AvaloniaPropertyDictionary<TValue>
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Utilities;

internal struct AvaloniaPropertyDictionary<TValue>
{
	private readonly struct Entry(AvaloniaProperty P_0, TValue P_1)
	{
		public readonly int Id = P_0.Id;

		public readonly TValue Value = P_1;
	}

	private Entry[]? _entries = null;

	private int _entryCount = 0;

	public int Count => _entryCount;

	public TValue this[AvaloniaProperty P_0]
	{
		set
		{
			int num = FindEntry(avaloniaProperty.Id);
			if (num >= 0)
			{
				UnsafeGetEntryRef(num) = new Entry(avaloniaProperty, val);
			}
			else
			{
				InsertEntry(new Entry(avaloniaProperty, val), ~num);
			}
		}
	}

	public TValue this[int P_0]
	{
		get
		{
			if (P_0 >= _entryCount)
			{
				ThrowOutOfRange();
			}
			return UnsafeGetEntryRef(P_0).Value;
		}
	}

	public AvaloniaPropertyDictionary()
	{
	}

	public void Add(AvaloniaProperty P_0, TValue P_1)
	{
		int num = FindEntry(P_0.Id);
		if (num >= 0)
		{
			ThrowDuplicate();
		}
		InsertEntry(new Entry(P_0, P_1), ~num);
	}

	public void Clear()
	{
		if (_entries != null)
		{
			Array.Clear(_entries, 0, _entries.Length);
			_entryCount = 0;
		}
	}

	public bool ContainsKey(AvaloniaProperty P_0)
	{
		return FindEntry(P_0.Id) >= 0;
	}

	public TValue GetValue(int P_0)
	{
		if (P_0 >= _entryCount)
		{
			ThrowOutOfRange();
		}
		return UnsafeGetEntryRef(P_0).Value;
	}

	public bool Remove(AvaloniaProperty P_0)
	{
		int num = FindEntry(P_0.Id);
		if (num >= 0)
		{
			RemoveAt(num);
			return true;
		}
		return false;
	}

	public void RemoveAt(int P_0)
	{
		if (_entries == null)
		{
			ThrowOutOfRange();
		}
		Array.Copy(_entries, P_0 + 1, _entries, P_0, _entryCount - P_0 - 1);
		_entryCount--;
		UnsafeGetEntryRef(_entryCount) = default(Entry);
	}

	public bool TryAdd(AvaloniaProperty P_0, TValue P_1)
	{
		int num = FindEntry(P_0.Id);
		if (num >= 0)
		{
			return false;
		}
		InsertEntry(new Entry(P_0, P_1), ~num);
		return true;
	}

	public bool TryGetValue(AvaloniaProperty P_0, [MaybeNullWhen(false)] out TValue P_1)
	{
		int num = 0;
		int num2 = _entryCount - 1;
		if (num2 >= 0)
		{
			int id = P_0.Id;
			ref Entry reference = ref UnsafeGetEntryRef(0);
			do
			{
				int num3 = num2 + num >>> 1;
				ref Entry reference2 = ref Unsafe.Add(ref reference, (nuint)num3);
				int id2 = reference2.Id;
				if (id2 == id)
				{
					P_1 = reference2.Value;
					return true;
				}
				if (id2 < id)
				{
					num = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			while (num <= num2);
		}
		P_1 = default(TValue);
		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private int FindEntry(int P_0)
	{
		int num = 0;
		int num2 = _entryCount - 1;
		if (num2 >= 0)
		{
			ref Entry reference = ref UnsafeGetEntryRef(0);
			do
			{
				int num3 = num2 + num >>> 1;
				int id = Unsafe.Add(ref reference, (nuint)num3).Id;
				if (id == P_0)
				{
					return num3;
				}
				if (id < P_0)
				{
					num = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			while (num <= num2);
		}
		return ~num;
	}

	[MemberNotNull("_entries")]
	private void InsertEntry(Entry P_0, int P_1)
	{
		if (_entryCount > 0)
		{
			if (_entryCount == _entries.Length)
			{
				Entry[] array = new Entry[(_entryCount == 4) ? 8 : ((int)((double)_entryCount * 1.5))];
				Array.Copy(_entries, 0, array, 0, P_1);
				array[P_1] = P_0;
				Array.Copy(_entries, P_1, array, P_1 + 1, _entryCount - P_1);
				_entries = array;
			}
			else
			{
				Array.Copy(_entries, P_1, _entries, P_1 + 1, _entryCount - P_1);
				UnsafeGetEntryRef(P_1) = P_0;
			}
		}
		else
		{
			if (_entries == null)
			{
				_entries = new Entry[4];
			}
			UnsafeGetEntryRef(0) = P_0;
		}
		_entryCount++;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private ref Entry UnsafeGetEntryRef(int P_0)
	{
		return ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(_entries), (uint)P_0);
	}

	[DoesNotReturn]
	private static void ThrowOutOfRange()
	{
		throw new IndexOutOfRangeException();
	}

	[DoesNotReturn]
	private static void ThrowDuplicate()
	{
		throw new ArgumentException("An item with the same key has already been added.");
	}
}

