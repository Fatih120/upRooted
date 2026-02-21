// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.WeakHashList<T>
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Avalonia.Collections.Pooled;
using Avalonia.Utilities;

internal class WeakHashList<T> where T : class
{
	private struct Key
	{
		public WeakReference<T>? Weak;

		public T? Strong;

		public int HashCode;

		public static Key MakeStrong(T P_0)
		{
			return new Key
			{
				HashCode = P_0.GetHashCode(),
				Strong = P_0
			};
		}

		public static Key MakeWeak(T P_0)
		{
			return new Key
			{
				HashCode = P_0.GetHashCode(),
				Weak = new WeakReference<T>(P_0)
			};
		}

		public override int GetHashCode()
		{
			return HashCode;
		}
	}

	private class KeyComparer : IEqualityComparer<Key>
	{
		public static KeyComparer Instance = new KeyComparer();

		public bool Equals(Key P_0, Key P_1)
		{
			if (P_0.HashCode != P_1.HashCode)
			{
				return false;
			}
			if (P_0.Strong != null)
			{
				if (P_1.Strong != null)
				{
					return P_0.Strong == P_1.Strong;
				}
				if (P_1.Weak == null)
				{
					return false;
				}
				if (P_1.Weak.TryGetTarget(out var val))
				{
					return val == P_0.Strong;
				}
				return false;
			}
			if (P_1.Strong != null)
			{
				if (P_0.Weak == null)
				{
					return false;
				}
				if (P_0.Weak.TryGetTarget(out var val2))
				{
					return val2 == P_1.Strong;
				}
				return false;
			}
			if (P_0.Weak == null || !P_0.Weak.TryGetTarget(out var val3))
			{
				WeakReference<T>? weak = P_1.Weak;
				if (weak == null)
				{
					return true;
				}
				T val4;
				return !weak.TryGetTarget(out val4);
			}
			WeakReference<T>? weak2 = P_1.Weak;
			if (weak2 != null && weak2.TryGetTarget(out var val5))
			{
				return val3 == val5;
			}
			return false;
		}

		public int GetHashCode(Key P_0)
		{
			return P_0.HashCode;
		}
	}

	private Dictionary<Key, int>? _dic;

	private WeakReference<T>?[]? _arr;

	private int _arrCount;

	private static readonly Stack<PooledList<T>> s_listPool = new Stack<PooledList<T>>();

	public bool IsEmpty
	{
		get
		{
			if (_dic == null)
			{
				return _arrCount == 0;
			}
			return _dic.Count == 0;
		}
	}

	public bool NeedCompact
	{
		[CompilerGenerated]
		get
		{
			return _003CNeedCompact_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CNeedCompact_003Ek__BackingField = flag;
		}
	}

	public void Add(T P_0)
	{
		if (_dic != null)
		{
			Key key = Key.MakeStrong(P_0);
			if (_dic.TryGetValue(key, out var value))
			{
				_dic[key] = value + 1;
			}
			else
			{
				_dic[Key.MakeWeak(P_0)] = 1;
			}
			return;
		}
		if (_arr == null)
		{
			_arr = new WeakReference<T>[8];
		}
		if (_arrCount < _arr.Length)
		{
			_arr[_arrCount] = new WeakReference<T>(P_0);
			_arrCount++;
			return;
		}
		for (int i = 0; i < _arrCount; i++)
		{
			if (!_arr[i].TryGetTarget(out var _))
			{
				_arr[i] = new WeakReference<T>(P_0);
				return;
			}
		}
		_dic = new Dictionary<Key, int>(KeyComparer.Instance);
		WeakReference<T>[] arr = _arr;
		for (int j = 0; j < arr.Length; j++)
		{
			if (arr[j].TryGetTarget(out var val2))
			{
				Add(val2);
			}
		}
		Add(P_0);
		_arr = null;
		_arrCount = 0;
	}

	public void Remove(T P_0)
	{
		if (_arr != null)
		{
			for (int i = 0; i < _arrCount; i++)
			{
				WeakReference<T>? obj = _arr[i];
				if (obj != null && obj.TryGetTarget(out var val) && val == P_0)
				{
					_arr[i] = null;
					ArrCompact();
					break;
				}
			}
		}
		else if (_dic != null)
		{
			Key key = Key.MakeStrong(P_0);
			if (_dic.TryGetValue(key, out var value) && value > 1)
			{
				_dic[key] = value - 1;
			}
			else
			{
				_dic.Remove(key);
			}
		}
	}

	private void ArrCompact()
	{
		if (_arr == null)
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < _arrCount; i++)
		{
			WeakReference<T> weakReference = _arr[i];
			if (weakReference == null && num == -1)
			{
				num = i;
			}
			if (weakReference != null && num != -1)
			{
				_arr[i] = null;
				_arr[num] = weakReference;
				num++;
			}
		}
		if (num != -1)
		{
			_arrCount = num;
		}
	}

	public void Compact()
	{
		if (_dic == null)
		{
			return;
		}
		PooledList<Key> pooledList = null;
		foreach (KeyValuePair<Key, int> item in _dic)
		{
			WeakReference<T>? weak = item.Key.Weak;
			if (weak == null || !weak.TryGetTarget(out var _))
			{
				(pooledList ?? (pooledList = new PooledList<Key>())).Add(item.Key);
			}
		}
		if (pooledList == null)
		{
			return;
		}
		foreach (Key item2 in pooledList)
		{
			_dic.Remove(item2);
		}
		pooledList.Dispose();
	}

	public static void ReturnToSharedPool(PooledList<T> P_0)
	{
		P_0.Clear();
		s_listPool.Push(P_0);
	}

	public PooledList<T>? GetAlive(Func<PooledList<T>>? P_0 = null)
	{
		PooledList<T> pooledList = null;
		if (_arr != null)
		{
			bool flag = false;
			for (int i = 0; i < _arrCount; i++)
			{
				WeakReference<T>? obj = _arr[i];
				if (obj != null && obj.TryGetTarget(out var val))
				{
					object obj2 = pooledList;
					if (obj2 == null)
					{
						obj2 = P_0?.Invoke() ?? ((s_listPool.Count > 0) ? s_listPool.Pop() : new PooledList<T>());
						pooledList = (PooledList<T>)obj2;
					}
					((PooledList<T>)obj2).Add(val);
				}
				else
				{
					_arr[i] = null;
					flag = true;
				}
			}
			if (flag)
			{
				ArrCompact();
			}
			return pooledList;
		}
		if (_dic != null)
		{
			foreach (KeyValuePair<Key, int> item in _dic)
			{
				WeakReference<T>? weak = item.Key.Weak;
				if (weak != null && weak.TryGetTarget(out var val2))
				{
					object obj3 = pooledList;
					if (obj3 == null)
					{
						obj3 = P_0?.Invoke() ?? ((s_listPool.Count > 0) ? s_listPool.Pop() : new PooledList<T>());
						pooledList = (PooledList<T>)obj3;
					}
					((PooledList<T>)obj3).Add(val2);
				}
				else
				{
					NeedCompact = true;
				}
			}
		}
		return pooledList;
	}
}

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

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ArraySlice<T>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

internal readonly struct ArraySlice<T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
{
	private readonly T[] _data;

	public static ArraySlice<T> Empty => new ArraySlice<T>(Array.Empty<T>());

	public bool IsEmpty => Length == 0;

	public int Start { get; }

	public int Length { get; }

	public Span<T> Span => new Span<T>(_data, Start, Length);

	public ref T this[int P_0]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			int num = P_0 + Start;
			return ref _data[num];
		}
	}

	T IReadOnlyList<T>.this[int P_0] => this[P_0];

	int IReadOnlyCollection<T>.Count => Length;

	public ArraySlice(T[] P_0)
		: this(P_0, 0, P_0.Length)
	{
	}

	public ArraySlice(T[] P_0, int P_1, int P_2)
	{
		_data = P_0;
		Start = P_1;
		Length = P_2;
	}

	public static implicit operator ArraySlice<T>(T[] P_0)
	{
		return new ArraySlice<T>(P_0, 0, P_0.Length);
	}

	public void Fill(T P_0)
	{
		Span.Fill(P_0);
	}

	public ArraySlice<T> Slice(int P_0, int P_1)
	{
		return new ArraySlice<T>(_data, P_0, P_1);
	}

	public ImmutableReadOnlyListStructEnumerator<T> GetEnumerator()
	{
		return new ImmutableReadOnlyListStructEnumerator<T>(this);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

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

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.AvaloniaResourcesIndexEntry
using System.Runtime.CompilerServices;

public class AvaloniaResourcesIndexEntry
{
	[CompilerGenerated]
	private string? _003CPath_003Ek__BackingField;

	[CompilerGenerated]
	private int _003COffset_003Ek__BackingField;

	[CompilerGenerated]
	private int _003CSize_003Ek__BackingField;

	public string? Path
	{
		[CompilerGenerated]
		get
		{
			return _003CPath_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CPath_003Ek__BackingField = text;
		}
	}

	public int Offset
	{
		[CompilerGenerated]
		get
		{
			return _003COffset_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003COffset_003Ek__BackingField = num;
		}
	}

	public int Size
	{
		[CompilerGenerated]
		get
		{
			return _003CSize_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSize_003Ek__BackingField = num;
		}
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.AvaloniaResourcesIndexReaderWriter
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Avalonia.Utilities;

public static class AvaloniaResourcesIndexReaderWriter
{
	public static List<AvaloniaResourcesIndexEntry> ReadIndex(Stream P_0)
	{
		using BinaryReader binaryReader = new BinaryReader(P_0, Encoding.UTF8, true);
		int num = binaryReader.ReadInt32();
		return num switch
		{
			1 => ReadXmlIndex(), 
			2 => ReadBinaryIndex(binaryReader), 
			_ => throw new Exception($"Unknown resources index format version {num}"), 
		};
	}

	private static List<AvaloniaResourcesIndexEntry> ReadXmlIndex()
	{
		throw new NotSupportedException("Found legacy resources index format: please recompile your XAML files");
	}

	private static List<AvaloniaResourcesIndexEntry> ReadBinaryIndex(BinaryReader P_0)
	{
		int num = P_0.ReadInt32();
		List<AvaloniaResourcesIndexEntry> list = new List<AvaloniaResourcesIndexEntry>(num);
		for (int i = 0; i < num; i++)
		{
			list.Add(new AvaloniaResourcesIndexEntry
			{
				Path = P_0.ReadString(),
				Offset = P_0.ReadInt32(),
				Size = P_0.ReadInt32()
			});
		}
		return list;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.BidiDictionary<T1,T2>
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Media.TextFormatting;

internal sealed class BidiDictionary<T1, T2> where T1 : notnull where T2 : notnull
{
	private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();

	private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

	public void ClearThenResetIfTooLarge()
	{
		FormattingBufferHelper.ClearThenResetIfTooLarge(ref _forward);
		FormattingBufferHelper.ClearThenResetIfTooLarge(ref _reverse);
	}

	public void Add(T1 P_0, T2 P_1)
	{
		_forward.Add(P_0, P_1);
		_reverse.Add(P_1, P_0);
	}

	public bool TryGetValue(T1 P_0, [MaybeNullWhen(false)] out T2 P_1)
	{
		return _forward.TryGetValue(P_0, out P_1);
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.BinarySearchExtension
using System.Collections.Generic;

internal static class BinarySearchExtension
{
	private static int GetMedian(int P_0, int P_1)
	{
		return P_0 + (P_1 - P_0 >> 1);
	}

	public static int BinarySearch<T>(this IReadOnlyList<T> P_0, T P_1, IComparer<T> P_2)
	{
		return BinarySearch(P_0, 0, P_0.Count, P_1, P_2);
	}

	public static int BinarySearch<T>(this IReadOnlyList<T> P_0, int P_1, int P_2, T P_3, IComparer<T> P_4)
	{
		int num = P_1;
		int num2 = P_1 + P_2 - 1;
		while (num <= num2)
		{
			int median = GetMedian(num, num2);
			int num3 = P_4.Compare(P_0[median], P_3);
			if (num3 == 0)
			{
				return median;
			}
			if (num3 < 0)
			{
				num = median + 1;
			}
			else
			{
				num2 = median - 1;
			}
		}
		return ~num;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ByteSizeHelper
using System;

internal static class ByteSizeHelper
{
	private static readonly string[] Prefixes = new string[9] { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

	public static string ToString(ulong P_0, bool P_1)
	{
		if (P_0 == 0L)
		{
			return string.Format(P_1 ? "{0}{1:0.#} {2}" : "{0}{1:0.#}{2}", null, 0, Prefixes[0]);
		}
		double num = Math.Abs((double)P_0);
		int num2 = (int)Math.Log(num, 1000.0);
		int num3 = ((num2 >= Prefixes.Length) ? (Prefixes.Length - 1) : num2);
		double num4 = num / Math.Pow(1000.0, num3);
		return string.Format("{0}{1:0.#}{2}", (P_0 < 0) ? "-" : null, num4, Prefixes[num3]);
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.CharacterReader
using System;
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

public ref struct CharacterReader
{
	private ReadOnlySpan<char> _s;

	[CompilerGenerated]
	private int _003CPosition_003Ek__BackingField;

	public bool End => _s.IsEmpty;

	public char Peek => _s[0];

	public int Position
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CPosition_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CPosition_003Ek__BackingField = num;
		}
	}

	public CharacterReader(ReadOnlySpan<char> P_0)
	{
		this = default(CharacterReader);
		_s = P_0;
	}

	public char Take()
	{
		Position++;
		char result = _s[0];
		_s = _s.Slice(1);
		return result;
	}

	public void SkipWhitespace()
	{
		ReadOnlySpan<char> s = _s.TrimStart();
		Position += _s.Length - s.Length;
		_s = s;
	}

	public bool TakeIf(char P_0)
	{
		if (Peek == P_0)
		{
			Take();
			return true;
		}
		return false;
	}

	internal bool TakeIf(string P_0)
	{
		if (TryPeek(P_0.Length).SequenceEqual(P_0.AsSpan()))
		{
			_s = _s.Slice(P_0.Length);
			Position += P_0.Length;
			return true;
		}
		return false;
	}

	public ReadOnlySpan<char> TakeWhile(Func<char, bool> P_0)
	{
		int i;
		for (i = 0; i < _s.Length && P_0(_s[i]); i++)
		{
		}
		ReadOnlySpan<char> result = _s.Slice(0, i);
		_s = _s.Slice(i);
		Position += i;
		return result;
	}

	public ReadOnlySpan<char> TryPeek(int P_0)
	{
		if (_s.Length < P_0)
		{
			return ReadOnlySpan<char>.Empty;
		}
		return _s.Slice(0, P_0);
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.EnumHelper
using System;

internal class EnumHelper
{
	public static T Parse<T>(ReadOnlySpan<char> P_0, bool P_1) where T : struct
	{
		return Enum.Parse<T>(P_0, P_1);
	}

	public static bool TryParse<T>(ReadOnlySpan<char> P_0, bool P_1, out T P_2) where T : struct
	{
		return Enum.TryParse<T>(P_0, P_1, out P_2);
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.Equals
internal delegate bool Equals(object? P_0, object? P_1);

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.FrugalListBase<T>
using Avalonia.Utilities;

internal abstract class FrugalListBase<T>
{
	protected int _count;

	public int Count => _count;

	public abstract int Capacity { get; }

	public abstract FrugalListStoreState Add(T P_0);

	public abstract void Insert(int P_0, T P_1);

	public abstract void SetAt(int P_0, T P_1);

	public abstract void RemoveAt(int P_0);

	public abstract T EntryAt(int P_0);

	public abstract void Promote(FrugalListBase<T> P_0);
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.FrugalListStoreState
internal enum FrugalListStoreState
{
	Success,
	SingleItemList,
	ThreeItemList,
	SixItemList,
	Array
}

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

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.IdentifierParser
using System;
using System.Globalization;
using Avalonia.Utilities;

public static class IdentifierParser
{
	public static ReadOnlySpan<char> ParseIdentifier(this scoped ref CharacterReader P_0)
	{
		if (IsValidIdentifierStart(P_0.Peek))
		{
			return P_0.TakeWhile((char c) => IsValidIdentifierChar(c));
		}
		return ReadOnlySpan<char>.Empty;
	}

	private static bool IsValidIdentifierStart(char P_0)
	{
		if (!char.IsLetter(P_0))
		{
			return P_0 == '_';
		}
		return true;
	}

	private static bool IsValidIdentifierChar(char P_0)
	{
		if (IsValidIdentifierStart(P_0))
		{
			return true;
		}
		UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(P_0);
		if (unicodeCategory != UnicodeCategory.NonSpacingMark && unicodeCategory != UnicodeCategory.SpacingCombiningMark && unicodeCategory != UnicodeCategory.ConnectorPunctuation && unicodeCategory != UnicodeCategory.Format)
		{
			return unicodeCategory == UnicodeCategory.DecimalDigitNumber;
		}
		return true;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ImmutableReadOnlyListStructEnumerator<T>
using System;
using System.Collections;
using System.Collections.Generic;

public struct ImmutableReadOnlyListStructEnumerator<T>(IReadOnlyList<T> P_0) : IEnumerator<T>, IEnumerator, IDisposable
{
	private readonly IReadOnlyList<T> _readOnlyList = P_0;

	private int _pos = -1;

	private T? _current = default(T);

	public T Current => _current;

	object? IEnumerator.Current => Current;

	public void Dispose()
	{
	}

	public bool MoveNext()
	{
		if (_pos >= _readOnlyList.Count - 1)
		{
			return false;
		}
		_current = _readOnlyList[++_pos];
		return true;
	}

	public void Reset()
	{
		_pos = -1;
		_current = default(T);
	}
}

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

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.IRef<T>
using System;
using Avalonia.Utilities;

internal interface IRef<out T> : IDisposable where T : class
{
	T Item { get; }

	IRef<T> Clone();
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.IWeakEventSubscriber<TEventArgs>
using Avalonia.Utilities;

public interface IWeakEventSubscriber<in TEventArgs>
{
	void OnEvent(object? P_0, WeakEvent P_1, TEventArgs P_2);
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.MappedArraySlice<T>
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

internal readonly struct MappedArraySlice<T>(in ArraySlice<T> P_0, in ArraySlice<int> P_1) where T : struct
{
	private readonly ArraySlice<T> _data = P_0;

	private readonly ArraySlice<int> _map = P_1;

	public int Length => _map.Length;

	public ref T this[int P_0]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return ref _data[_map[P_0]];
		}
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.MathUtilities
using System;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Metadata;

[Unstable("This API might be removed in next major version. Please use corresponding BCL APIs.")]
public static class MathUtilities
{
	public static bool AreClose(double P_0, double P_1)
	{
		if (P_0 == P_1)
		{
			return true;
		}
		double num = (Math.Abs(P_0) + Math.Abs(P_1) + 10.0) * 2.220446049250313E-16;
		double num2 = P_0 - P_1;
		if (0.0 - num < num2)
		{
			return num > num2;
		}
		return false;
	}

	public static bool AreClose(double P_0, double P_1, double P_2)
	{
		if (P_0 == P_1)
		{
			return true;
		}
		double num = P_0 - P_1;
		if (0.0 - P_2 < num)
		{
			return P_2 > num;
		}
		return false;
	}

	public static bool LessThan(double P_0, double P_1)
	{
		if (P_0 < P_1)
		{
			return !AreClose(P_0, P_1);
		}
		return false;
	}

	public static bool GreaterThan(double P_0, double P_1)
	{
		if (P_0 > P_1)
		{
			return !AreClose(P_0, P_1);
		}
		return false;
	}

	public static bool LessThanOrClose(double P_0, double P_1)
	{
		if (!(P_0 < P_1))
		{
			return AreClose(P_0, P_1);
		}
		return true;
	}

	public static bool GreaterThanOrClose(double P_0, double P_1)
	{
		if (!(P_0 > P_1))
		{
			return AreClose(P_0, P_1);
		}
		return true;
	}

	public static bool IsOne(double P_0)
	{
		return Math.Abs(P_0 - 1.0) < 2.220446049250313E-15;
	}

	public static bool IsZero(double P_0)
	{
		return Math.Abs(P_0) < 2.220446049250313E-15;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double Clamp(double P_0, double P_1, double P_2)
	{
		if (P_1 > P_2)
		{
			ThrowCannotBeGreaterThanException(P_1, P_2);
		}
		if (P_0 < P_1)
		{
			return P_1;
		}
		if (P_0 > P_2)
		{
			return P_2;
		}
		return P_0;
	}

	public static decimal Clamp(decimal P_0, decimal P_1, decimal P_2)
	{
		if (P_1 > P_2)
		{
			ThrowCannotBeGreaterThanException(P_1, P_2);
		}
		if (P_0 < P_1)
		{
			return P_1;
		}
		if (P_0 > P_2)
		{
			return P_2;
		}
		return P_0;
	}

	public static int Clamp(int P_0, int P_1, int P_2)
	{
		if (P_1 > P_2)
		{
			ThrowCannotBeGreaterThanException(P_1, P_2);
		}
		if (P_0 < P_1)
		{
			return P_1;
		}
		if (P_0 > P_2)
		{
			return P_2;
		}
		return P_0;
	}

	public static double Deg2Rad(double P_0)
	{
		return P_0 * (Math.PI / 180.0);
	}

	public static double Grad2Rad(double P_0)
	{
		return P_0 * (Math.PI / 200.0);
	}

	public static double Turn2Rad(double P_0)
	{
		return P_0 * 2.0 * Math.PI;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static bool IsNegativeOrNonFinite(double P_0)
	{
		return BitConverter.DoubleToUInt64Bits(P_0) >= 9218868437227405312L;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static bool IsFinite(double P_0)
	{
		return double.IsFinite(P_0);
	}

	internal static int WhichPolygonSideIntersects(uint P_0, ReadOnlySpan<Vector> P_1, Vector P_2, Vector P_3)
	{
		uint num = 0u;
		uint num2 = 0u;
		uint num3 = 0u;
		Point point = new Point(0.0 - P_3.Y, P_3.X);
		for (int i = 0; i < P_0; i++)
		{
			double num4 = Vector.Dot(P_2 - P_1[i], point);
			if (num4 > 0.0)
			{
				num++;
			}
			else if (num4 < 0.0)
			{
				num2++;
			}
			else
			{
				num3++;
			}
			if ((num != 0 && num2 != 0) || num3 != 0)
			{
				return 0;
			}
		}
		if (num == 0)
		{
			return -1;
		}
		return 1;
	}

	internal static bool DoPolygonsIntersect(uint P_0, ReadOnlySpan<Vector> P_1, uint P_2, ReadOnlySpan<Vector> P_3)
	{
		for (int i = 0; i < P_0; i++)
		{
			Vector vector = P_1[(int)((i + 1) % P_0)] - P_1[i];
			if (WhichPolygonSideIntersects(P_2, P_3, P_1[i], vector) < 0)
			{
				return false;
			}
		}
		for (int j = 0; j < P_2; j++)
		{
			Vector vector2 = P_3[(int)((j + 1) % P_2)] - P_3[j];
			if (WhichPolygonSideIntersects(P_0, P_1, P_3[j], vector2) < 0)
			{
				return false;
			}
		}
		return true;
	}

	internal static bool IsEntirelyContained(uint P_0, ReadOnlySpan<Vector> P_1, uint P_2, ReadOnlySpan<Vector> P_3)
	{
		for (int i = 0; i < P_2; i++)
		{
			Vector vector = P_3[(i + 1) % (int)P_2] - P_3[i];
			if (WhichPolygonSideIntersects(P_0, P_1, P_3[i], vector) <= 0)
			{
				return false;
			}
		}
		return true;
	}

	private static void ThrowCannotBeGreaterThanException<T>(T P_0, T P_1)
	{
		throw new ArgumentException($"{P_0} cannot be greater than {P_1}.");
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.NonPumpingLockHelper
using System;
using Avalonia;
using Avalonia.Threading;
using Avalonia.Utilities;

internal class NonPumpingLockHelper
{
	public interface IHelperImpl
	{
		int Wait(nint[] P_0, bool P_1, int P_2);
	}

	public static IDisposable? Use()
	{
		IHelperImpl service = AvaloniaLocator.Current.GetService<IHelperImpl>();
		if (service == null)
		{
			return null;
		}
		return NonPumpingSyncContext.Use(service);
	}
}

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

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.RefCountable
using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using Avalonia.Utilities;

internal static class RefCountable
{
	private class RefCounter
	{
		private IDisposable? _item;

		private volatile int _refs;

		public RefCounter(IDisposable P_0)
		{
			_item = P_0;
			_refs = 1;
		}

		public void AddRef()
		{
			int num = _refs;
			while (true)
			{
				if (num == 0)
				{
					throw new ObjectDisposedException("Cannot add a reference to a nonreferenced item");
				}
				int num2 = Interlocked.CompareExchange(ref _refs, num + 1, num);
				if (num2 != num)
				{
					num = num2;
					continue;
				}
				break;
			}
		}

		public void Release()
		{
			int num = _refs;
			while (true)
			{
				int num2 = Interlocked.CompareExchange(ref _refs, num - 1, num);
				if (num2 == num)
				{
					break;
				}
				num = num2;
			}
			if (num == 1)
			{
				_item?.Dispose();
				_item = null;
			}
		}
	}

	private class Ref<T> : CriticalFinalizerObject, IRef<T>, IDisposable where T : class
	{
		private T? _item;

		private readonly RefCounter _counter;

		private readonly object _lock = new object();

		public T Item
		{
			get
			{
				lock (_lock)
				{
					return _item;
				}
			}
		}

		public Ref(T P_0, RefCounter P_1)
		{
			_item = P_0;
			_counter = P_1;
		}

		public void Dispose()
		{
			lock (_lock)
			{
				if (_item != null)
				{
					_counter.Release();
					_item = null;
				}
				GC.SuppressFinalize(this);
			}
		}

		~Ref()
		{
			Dispose();
		}

		public IRef<T> Clone()
		{
			lock (_lock)
			{
				if (_item != null)
				{
					Ref<T> result = new Ref<T>(_item, _counter);
					_counter.AddRef();
					return result;
				}
				throw new ObjectDisposedException("Ref<" + typeof(T)?.ToString() + ">");
			}
		}
	}

	public static IRef<T> Create<T>(T P_0) where T : class, IDisposable
	{
		return new Ref<T>(P_0, new RefCounter(P_0));
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.RefCountingSmallDictionary<TKey>
using System.Collections;
using System.Collections.Generic;
using Avalonia.Utilities;

internal struct RefCountingSmallDictionary<TKey> : IEnumerable<KeyValuePair<TKey, int>>, IEnumerable where TKey : class
{
	private InlineDictionary<TKey, int> _counts;

	public bool Add(TKey P_0)
	{
		_counts.GetValueRefOrAddDefault(P_0, out var flag)++;
		return !flag;
	}

	public bool Remove(TKey P_0)
	{
		ref int valueRefOrNullRef = ref _counts.GetValueRefOrNullRef(P_0);
		valueRefOrNullRef--;
		if (valueRefOrNullRef == 0)
		{
			_counts.Remove(P_0);
			return true;
		}
		return false;
	}

	public InlineDictionary<TKey, int>.Enumerator GetEnumerator()
	{
		return _counts.GetEnumerator();
	}

	IEnumerator<KeyValuePair<TKey, int>> IEnumerable<KeyValuePair<TKey, int>>.GetEnumerator()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SafeEnumerableHashSet<T>
using System;
using System.Collections;
using System.Collections.Generic;
using Avalonia.Utilities;

internal class SafeEnumerableHashSet<T> : IEnumerable<T>, IEnumerable
{
	public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
	{
		private readonly SafeEnumerableHashSet<T> _owner;

		private readonly int _generation;

		private HashSet<T>.Enumerator _enumerator;

		public T Current => _enumerator.Current;

		object? IEnumerator.Current => _enumerator.Current;

		internal Enumerator(SafeEnumerableHashSet<T> P_0, HashSet<T> P_1)
		{
			_owner = P_0;
			_generation = P_0._generation;
			_owner._enumCount++;
			_enumerator = P_1.GetEnumerator();
		}

		public void Dispose()
		{
			_enumerator.Dispose();
			if (_owner._generation == _generation)
			{
				_owner._enumCount--;
			}
		}

		public bool MoveNext()
		{
			return _enumerator.MoveNext();
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	private HashSet<T> _hashSet = new HashSet<T>();

	private int _generation;

	private int _enumCount;

	public void Add(T P_0)
	{
		GetSet().Add(P_0);
	}

	public bool Remove(T P_0)
	{
		return GetSet().Remove(P_0);
	}

	public Enumerator GetEnumerator()
	{
		return new Enumerator(this, _hashSet);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	private HashSet<T> GetSet()
	{
		if (_enumCount > 0)
		{
			_hashSet = new HashSet<T>(_hashSet);
			_generation++;
			_enumCount = 0;
		}
		return _hashSet;
	}
}

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

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.Span
internal class Span
{
	public readonly object? element;

	public int length;

	public Span(object? P_0, int P_1)
	{
		element = P_0;
		length = P_1;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpanEnumerator
using System.Collections;
using Avalonia.Utilities;

internal sealed class SpanEnumerator : IEnumerator
{
	private readonly SpanVector _spans;

	private int _current;

	public object Current => _spans[_current];

	internal SpanEnumerator(SpanVector P_0)
	{
		_spans = P_0;
		_current = -1;
	}

	public bool MoveNext()
	{
		_current++;
		return _current < _spans.Count;
	}

	public void Reset()
	{
		_current = -1;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpanHelpers
using System;
using System.Globalization;
using System.Runtime.CompilerServices;

public static class SpanHelpers
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseUInt(this ReadOnlySpan<char> P_0, NumberStyles P_1, IFormatProvider P_2, out uint P_3)
	{
		return uint.TryParse(P_0, P_1, P_2, out P_3);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseInt(this ReadOnlySpan<char> P_0, NumberStyles P_1, IFormatProvider P_2, out int P_3)
	{
		return int.TryParse(P_0, P_1, P_2, out P_3);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseDouble(this ReadOnlySpan<char> P_0, NumberStyles P_1, IFormatProvider P_2, out double P_3)
	{
		return double.TryParse(P_0, P_1, P_2, out P_3);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double ParseDouble(this ReadOnlySpan<char> P_0, IFormatProvider P_1)
	{
		return double.Parse(P_0, P_1);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryParseByte(this ReadOnlySpan<char> P_0, NumberStyles P_1, IFormatProvider P_2, out byte P_3)
	{
		return byte.TryParse(P_0, P_1, P_2, out P_3);
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpanPosition
internal readonly struct SpanPosition(int P_0, int P_1)
{
	internal int Index { get; } = P_0;

	internal int Offset { get; } = P_1;
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpanRider
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

internal struct SpanRider
{
	private readonly SpanVector _spans;

	private SpanPosition _spanPosition;

	[CompilerGenerated]
	private int _003CLength_003Ek__BackingField;

	[CompilerGenerated]
	private int _003CCurrentPosition_003Ek__BackingField;

	public int Length
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CLength_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CLength_003Ek__BackingField = num;
		}
	}

	private int CurrentPosition
	{
		[CompilerGenerated]
		set
		{
			_003CCurrentPosition_003Ek__BackingField = num;
		}
	}

	public object? CurrentElement
	{
		get
		{
			if (_spanPosition.Index < _spans.Count)
			{
				return _spans[_spanPosition.Index].element;
			}
			return _spans.Default;
		}
	}

	public SpanRider(SpanVector P_0, SpanPosition P_1 = default(SpanPosition), int P_2 = 0)
	{
		_spans = P_0;
		_spanPosition = default(SpanPosition);
		CurrentPosition = 0;
		Length = 0;
		At(P_1, P_2);
	}

	public bool At(SpanPosition P_0, int P_1)
	{
		bool num = _spans.FindSpan(P_1, P_0, out _spanPosition);
		if (num)
		{
			Length = _spans[_spanPosition.Index].length - (P_1 - _spanPosition.Offset);
			CurrentPosition = P_1;
			return num;
		}
		Length = int.MaxValue;
		CurrentPosition = _spanPosition.Offset;
		return num;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpanStringTokenizer
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Avalonia.Utilities;

internal ref struct SpanStringTokenizer
{
	private readonly ReadOnlySpan<char> _s;

	private readonly int _length;

	private readonly char _separator;

	private readonly string? _exceptionMessage;

	private readonly IFormatProvider _formatProvider;

	private int _index;

	private int _tokenIndex;

	private int _tokenLength;

	public int CurrentTokenIndex => _tokenIndex;

	public ReadOnlySpan<char> CurrentTokenSpan
	{
		get
		{
			if (_tokenIndex >= 0)
			{
				return _s.Slice(_tokenIndex, _tokenLength);
			}
			return ReadOnlySpan<char>.Empty;
		}
	}

	public SpanStringTokenizer(string P_0, IFormatProvider P_1, string? P_2 = null)
		: this(P_0.AsSpan(), GetSeparatorFromFormatProvider(P_1), P_2)
	{
		_formatProvider = P_1;
	}

	public SpanStringTokenizer(string P_0, char P_1 = ',', string? P_2 = null)
		: this(P_0.AsSpan(), P_1, P_2)
	{
	}

	public SpanStringTokenizer(ReadOnlySpan<char> P_0, char P_1 = ',', string? P_2 = null)
	{
		_s = P_0;
		_length = P_0.Length;
		_separator = P_1;
		_exceptionMessage = P_2;
		_formatProvider = CultureInfo.InvariantCulture;
		_index = 0;
		_tokenIndex = -1;
		_tokenLength = 0;
		while (_index < _length && char.IsWhiteSpace(_s[_index]))
		{
			_index++;
		}
	}

	public void Dispose()
	{
		if (_index != _length)
		{
			throw GetFormatException();
		}
	}

	public bool TryReadInt32(out int P_0, char? P_1 = null)
	{
		if (TryReadSpan(out var readOnlySpan, P_1) && readOnlySpan.TryParseInt(NumberStyles.Integer, _formatProvider, out P_0))
		{
			return true;
		}
		P_0 = 0;
		return false;
	}

	public bool TryReadDouble(out double P_0, char? P_1 = null)
	{
		if (TryReadSpan(out var readOnlySpan, P_1) && readOnlySpan.TryParseDouble(NumberStyles.Float, _formatProvider, out P_0))
		{
			return true;
		}
		P_0 = 0.0;
		return false;
	}

	public double ReadDouble(char? P_0 = null)
	{
		if (!TryReadDouble(out var result, P_0))
		{
			throw GetFormatException();
		}
		return result;
	}

	public bool TryReadString([NotNull] out string P_0, char? P_1 = null)
	{
		bool result = TryReadToken(P_1 ?? _separator);
		P_0 = CurrentTokenSpan.ToString();
		return result;
	}

	public bool TryReadSpan(out ReadOnlySpan<char> P_0, char? P_1 = null)
	{
		bool result = TryReadToken(P_1 ?? _separator);
		P_0 = CurrentTokenSpan;
		return result;
	}

	public ReadOnlySpan<char> ReadSpan(char? P_0 = null)
	{
		if (!TryReadSpan(out var result, P_0))
		{
			throw GetFormatException();
		}
		return result;
	}

	private bool TryReadToken(char P_0)
	{
		_tokenIndex = -1;
		if (_index >= _length)
		{
			return false;
		}
		char c = _s[_index];
		int index = _index;
		int num = 0;
		while (_index < _length)
		{
			c = _s[_index];
			if (char.IsWhiteSpace(c) || c == P_0)
			{
				break;
			}
			_index++;
			num++;
		}
		SkipToNextToken(P_0);
		_tokenIndex = index;
		_tokenLength = num;
		if (_tokenLength < 1)
		{
			throw GetFormatException();
		}
		return true;
	}

	private void SkipToNextToken(char P_0)
	{
		if (_index >= _length)
		{
			return;
		}
		char c = _s[_index];
		if (c != P_0 && !char.IsWhiteSpace(c))
		{
			throw GetFormatException();
		}
		int num = 0;
		while (_index < _length)
		{
			c = _s[_index];
			if (c == P_0)
			{
				num++;
				_index++;
				if (num > 1)
				{
					throw GetFormatException();
				}
			}
			else
			{
				if (!char.IsWhiteSpace(c))
				{
					break;
				}
				_index++;
			}
		}
		if (num > 0 && _index >= _length)
		{
			throw GetFormatException();
		}
	}

	private FormatException GetFormatException()
	{
		if (_exceptionMessage == null)
		{
			return new FormatException();
		}
		return new FormatException(_exceptionMessage);
	}

	private static char GetSeparatorFromFormatProvider(IFormatProvider P_0)
	{
		char c = ',';
		NumberFormatInfo instance = NumberFormatInfo.GetInstance(P_0);
		if (instance.NumberDecimalSeparator.Length > 0 && c == instance.NumberDecimalSeparator[0])
		{
			c = ';';
		}
		return c;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpanVector
using System;
using System.Collections;
using Avalonia.Utilities;

internal sealed class SpanVector : IEnumerable
{
	private static readonly Equals s_referenceEquals = object.ReferenceEquals;

	private static readonly Equals s_equals = object.Equals;

	private FrugalStructList<Span> _spans;

	public int Count => _spans.Count;

	public object? Default { get; }

	public Span this[int P_0] => _spans[P_0];

	internal SpanVector(object? P_0, FrugalStructList<Span> P_1 = default(FrugalStructList<Span>))
	{
		Default = P_0;
		_spans = P_1;
	}

	public IEnumerator GetEnumerator()
	{
		return new SpanEnumerator(this);
	}

	private void Add(Span P_0)
	{
		_spans.Add(P_0);
	}

	private void DeleteInternal(int P_0, int P_1)
	{
		for (int num = P_0 + P_1 - 1; num >= P_0; num--)
		{
			_spans.RemoveAt(num);
		}
	}

	private void Insert(int P_0, int P_1)
	{
		for (int i = 0; i < P_1; i++)
		{
			_spans.Insert(P_0, new Span(null, 0));
		}
	}

	internal bool FindSpan(int P_0, SpanPosition P_1, out SpanPosition P_2)
	{
		int count = _spans.Count;
		int i;
		int num;
		if (P_0 == 0)
		{
			i = 0;
			num = 0;
		}
		else if (P_0 >= P_1.Offset || P_0 * 2 < P_1.Offset)
		{
			if (P_0 >= P_1.Offset)
			{
				i = P_1.Index;
				num = P_1.Offset;
			}
			else
			{
				i = 0;
				num = 0;
			}
			for (; i < count; i++)
			{
				int length = _spans[i].length;
				if (P_0 < num + length)
				{
					break;
				}
				num += length;
			}
		}
		else
		{
			i = P_1.Index;
			for (num = P_1.Offset; num > P_0; num -= _spans[--i].length)
			{
			}
		}
		P_2 = new SpanPosition(i, num);
		return i != count;
	}

	public SpanPosition SetValue(int P_0, int P_1, object P_2, SpanPosition P_3)
	{
		return Set(P_0, P_1, P_2, s_equals, P_3);
	}

	private SpanPosition Set(int P_0, int P_1, object? P_2, Equals P_3, SpanPosition P_4)
	{
		bool num = FindSpan(P_0, P_4, out P_4);
		int num2 = P_4.Index;
		int num3 = P_4.Offset;
		if (!num)
		{
			if (num3 < P_0)
			{
				Add(new Span(Default, P_0 - num3));
			}
			if (Count > 0 && P_3(_spans[Count - 1].element, P_2))
			{
				_spans[Count - 1].length += P_1;
				if (num2 == Count)
				{
					num3 += P_1;
				}
			}
			else
			{
				Add(new Span(P_2, P_1));
			}
		}
		else
		{
			int i = num2;
			int num4;
			for (num4 = num3; i < Count && num4 + _spans[i].length <= P_0 + P_1; i++)
			{
				num4 += _spans[i].length;
			}
			if (P_0 == num3)
			{
				if (num2 > 0 && P_3(_spans[num2 - 1].element, P_2))
				{
					num2--;
					num3 -= _spans[num2].length;
					P_0 = num3;
					P_1 += _spans[num2].length;
				}
			}
			else if (P_3(_spans[num2].element, P_2))
			{
				P_1 = P_0 + P_1 - num3;
				P_0 = num3;
			}
			if (i < Count && P_3(_spans[i].element, P_2))
			{
				P_1 = num4 + _spans[i].length - P_0;
				num4 += _spans[i].length;
				i++;
			}
			if (i >= Count)
			{
				if (num3 < P_0)
				{
					if (Count != num2 + 2 && !Resize(num2 + 2))
					{
						throw new OutOfMemoryException();
					}
					_spans[num2].length = P_0 - num3;
					_spans[num2 + 1] = new Span(P_2, P_1);
				}
				else
				{
					if (Count != num2 + 1 && !Resize(num2 + 1))
					{
						throw new OutOfMemoryException();
					}
					_spans[num2] = new Span(P_2, P_1);
				}
			}
			else
			{
				object obj = null;
				int num5 = 0;
				if (P_0 + P_1 > num4)
				{
					obj = _spans[i].element;
					num5 = num4 + _spans[i].length - (P_0 + P_1);
				}
				int num6 = 1 + ((P_0 > num3) ? 1 : 0) - (i - num2);
				if (num6 < 0)
				{
					DeleteInternal(num2 + 1, -num6);
				}
				else if (num6 > 0)
				{
					Insert(num2 + 1, num6);
					for (int j = 0; j < num6; j++)
					{
						_spans[num2 + 1 + j] = new Span(null, 0);
					}
				}
				if (num3 < P_0)
				{
					_spans[num2].length = P_0 - num3;
					num2++;
					num3 = P_0;
				}
				_spans[num2] = new Span(P_2, P_1);
				num2++;
				num3 += P_1;
				if (num4 < P_0 + P_1)
				{
					_spans[num2] = new Span(obj, num5);
				}
			}
		}
		return new SpanPosition(num2, num3);
	}

	private bool Resize(int P_0)
	{
		if (P_0 > Count)
		{
			for (int i = 0; i < P_0 - Count; i++)
			{
				_spans.Add(new Span(null, 0));
			}
		}
		else if (P_0 < Count)
		{
			DeleteInternal(P_0, Count - P_0);
		}
		return true;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpringSolver
using System;

internal struct SpringSolver
{
	private readonly double m_w0;

	private readonly double m_zeta;

	private readonly double m_wd;

	private readonly double m_A;

	private readonly double m_B;

	public SpringSolver(double P_0, double P_1, double P_2, double P_3)
		: this(Math.Sqrt(P_1 / P_0), P_2 / (2.0 * Math.Sqrt(P_1 * P_0)), P_3)
	{
	}

	public SpringSolver(double P_0, double P_1, double P_2)
	{
		m_w0 = P_0;
		m_zeta = P_1;
		if (m_zeta < 1.0)
		{
			m_wd = m_w0 * Math.Sqrt(1.0 - m_zeta * m_zeta);
			m_A = 1.0;
			m_B = (m_zeta * m_w0 + (0.0 - P_2)) / m_wd;
		}
		else
		{
			m_A = 1.0;
			m_B = 0.0 - P_2 + m_w0;
			m_wd = 0.0;
		}
	}

	public readonly double Solve(double P_0)
	{
		P_0 = ((!(m_zeta < 1.0)) ? ((m_A + m_B * P_0) * Math.Exp((0.0 - P_0) * m_w0)) : (Math.Exp((0.0 - P_0) * m_zeta * m_w0) * (m_A * Math.Cos(m_wd * P_0) + m_B * Math.Sin(m_wd * P_0))));
		return 1.0 - P_0;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.StopwatchHelper
using System;
using System.Diagnostics;

internal static class StopwatchHelper
{
	private static readonly double s_timestampToTicks = 10000000.0 / (double)Stopwatch.Frequency;

	private static readonly double s_timestampToMs = s_timestampToTicks / 10000.0;

	public static TimeSpan GetElapsedTime(long P_0)
	{
		return GetElapsedTime(P_0, Stopwatch.GetTimestamp());
	}

	public static TimeSpan GetElapsedTime(long P_0, long P_1)
	{
		return new TimeSpan((long)((double)(P_1 - P_0) * s_timestampToTicks));
	}

	public static double GetElapsedTimeMs(long P_0)
	{
		return (double)(Stopwatch.GetTimestamp() - P_0) * s_timestampToMs;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.StringBuilderCache
using System;
using System.Text;

internal static class StringBuilderCache
{
	[ThreadStatic]
	private static StringBuilder? t_cachedInstance;

	public static StringBuilder Acquire(int P_0 = 16)
	{
		if (P_0 <= 360)
		{
			StringBuilder stringBuilder = t_cachedInstance;
			if (stringBuilder != null && P_0 <= stringBuilder.Capacity)
			{
				t_cachedInstance = null;
				stringBuilder.Clear();
				return stringBuilder;
			}
		}
		return new StringBuilder(P_0);
	}

	public static void Release(StringBuilder P_0)
	{
		if (P_0.Capacity <= 360)
		{
			t_cachedInstance = P_0;
		}
	}

	public static string GetStringAndRelease(StringBuilder P_0)
	{
		string result = P_0.ToString();
		Release(P_0);
		return result;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.StringSplitter
using System;
using System.Collections.Generic;

internal static class StringSplitter
{
	public static string[] SplitRespectingBrackets(string P_0, char P_1, char P_2 = '(', char P_3 = ')', StringSplitOptions P_4 = StringSplitOptions.None)
	{
		return SplitRespectingBrackets(P_0, new ReadOnlySpan<char>((char)P_1), P_2, P_3, P_4);
	}

	public static string[] SplitRespectingBrackets(string P_0, ReadOnlySpan<char> P_1, char P_2 = '(', char P_3 = ')', StringSplitOptions P_4 = StringSplitOptions.None)
	{
		if (P_2 == P_3)
		{
			throw new ArgumentException($"Opening bracket and closing bracket cannot be the same character '{P_2}'.", "closingBracket");
		}
		if (P_0 == null)
		{
			return Array.Empty<string>();
		}
		ReadOnlySpan<char> readOnlySpan = P_0.AsSpan();
		List<(int start, int length)> ranges = new List<(int, int)>();
		int num = 0;
		int num2 = 0;
		bool removeEmptyEntries = P_4.HasFlag(StringSplitOptions.RemoveEmptyEntries);
		bool trimEntries = P_4.HasFlag(StringSplitOptions.TrimEntries);
		for (int i = 0; i < readOnlySpan.Length; i++)
		{
			char c = readOnlySpan[i];
			if (c == P_2)
			{
				num++;
			}
			else if (c == P_3)
			{
				if (num <= 0)
				{
					throw new FormatException($"Unmatched closing bracket '{P_3}' at position {i}.");
				}
				num--;
			}
			else if (P_1.IndexOf(c) >= 0 && num == 0)
			{
				ProcessSegment(num2, i - 1);
				num2 = i + 1;
			}
		}
		if (num != 0)
		{
			throw new FormatException($"Unmatched opening bracket '{P_2}' in input string.");
		}
		ProcessSegment(num2, readOnlySpan.Length - 1);
		if (ranges.Count == 0)
		{
			return Array.Empty<string>();
		}
		string[] array = new string[ranges.Count];
		for (int j = 0; j < ranges.Count; j++)
		{
			(int, int) tuple = ranges[j];
			array[j] = new string(readOnlySpan.Slice(tuple.Item1, tuple.Item2));
		}
		return array;
		void ProcessSegment(int num3, int num4)
		{
			if (trimEntries)
			{
				while (num3 <= num4 && char.IsWhiteSpace(P_0[num3]))
				{
					num3++;
				}
				while (num4 >= num3 && char.IsWhiteSpace(P_0[num4]))
				{
					num4--;
				}
			}
			int num5 = num4 - num3 + 1;
			if (num5 > 0 || !removeEmptyEntries)
			{
				ranges.Add((num3, num5));
			}
		}
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SynchronousCompletionAsyncResult<T>
using System;
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

public record struct SynchronousCompletionAsyncResult<T> : INotifyCompletion
{
	public bool IsCompleted
	{
		get
		{
			if (!_isValid)
			{
				ThrowNotInitialized();
			}
			if (_source != null)
			{
				return _source.IsCompleted;
			}
			return true;
		}
	}

	private readonly SynchronousCompletionAsyncResultSource<T>? _source;

	private readonly T? _result;

	private readonly bool _isValid;

	internal SynchronousCompletionAsyncResult(SynchronousCompletionAsyncResultSource<T> P_0)
	{
		_source = P_0;
		_result = default(T);
		_isValid = true;
	}

	public SynchronousCompletionAsyncResult(T P_0)
	{
		_result = P_0;
		_source = null;
		_isValid = true;
	}

	private static void ThrowNotInitialized()
	{
		throw new InvalidOperationException("This SynchronousCompletionAsyncResult was not initialized");
	}

	public T GetResult()
	{
		if (!_isValid)
		{
			ThrowNotInitialized();
		}
		if (_source != null)
		{
			return _source.Result;
		}
		return _result;
	}

	public void OnCompleted(Action P_0)
	{
		if (!_isValid)
		{
			ThrowNotInitialized();
		}
		if (_source == null)
		{
			P_0();
		}
		else
		{
			_source.OnCompleted(P_0);
		}
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SynchronousCompletionAsyncResultSource<T>
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

public class SynchronousCompletionAsyncResultSource<T>
{
	private T? _result;

	private List<Action>? _continuations;

	internal bool IsCompleted
	{
		[CompilerGenerated]
		get
		{
			return _003CIsCompleted_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CIsCompleted_003Ek__BackingField = flag;
		}
	}

	public SynchronousCompletionAsyncResult<T> AsyncResult => new SynchronousCompletionAsyncResult<T>(this);

	internal T Result
	{
		get
		{
			if (!IsCompleted)
			{
				throw new InvalidOperationException("Asynchronous operation is not yet completed");
			}
			return _result;
		}
	}

	internal void OnCompleted(Action P_0)
	{
		if (_continuations == null)
		{
			_continuations = new List<Action>();
		}
		_continuations.Add(P_0);
	}

	public void SetResult(T P_0)
	{
		if (IsCompleted)
		{
			throw new InvalidOperationException("Asynchronous operation is already completed");
		}
		_result = P_0;
		IsCompleted = true;
		if (_continuations != null)
		{
			foreach (Action continuation in _continuations)
			{
				continuation();
			}
		}
		_continuations = null;
	}

	public void TrySetResult(T P_0)
	{
		if (!IsCompleted)
		{
			SetResult(P_0);
		}
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.TargetWeakEventSubscriber<TTarget,TEventArgs>
using System;
using Avalonia.Utilities;

public sealed class TargetWeakEventSubscriber<TTarget, TEventArgs> : IWeakEventSubscriber<TEventArgs>
{
	private readonly TTarget _target;

	private readonly Action<TTarget, object?, WeakEvent, TEventArgs> _dispatchFunc;

	public TargetWeakEventSubscriber(TTarget P_0, Action<TTarget, object?, WeakEvent, TEventArgs> P_1)
	{
		_target = P_0;
		_dispatchFunc = P_1;
	}

	void IWeakEventSubscriber<TEventArgs>.OnEvent(object? P_0, WeakEvent P_1, TEventArgs P_2)
	{
		_dispatchFunc(_target, P_0, P_1, P_2);
	}
}

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

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ThrowHelper
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

internal static class ThrowHelper
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowIfNull([NotNull] object? P_0, [CallerArgumentExpression("argument")] string? P_1 = null)
	{
		if (P_0 == null)
		{
			ThrowArgumentNullException(P_1);
		}
		[DoesNotReturn]
		static void ThrowArgumentNullException(string? text)
		{
			throw new ArgumentNullException(text);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowIfNullOrEmpty([NotNull] string? P_0, [CallerArgumentExpression("argument")] string? P_1 = null)
	{
		if (string.IsNullOrEmpty(P_0))
		{
			ThrowNullOrEmptyException(P_0, P_1);
		}
		[DoesNotReturn]
		static void ThrowNullOrEmptyException(string? text, string? text2)
		{
			ThrowIfNull(text, text2);
			throw new ArgumentException("Empty string", text2);
		}
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.TypeUtilities
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Utilities;

public static class TypeUtilities
{
	[Flags]
	internal enum OperatorType
	{
		Implicit = 1,
		Explicit = 2
	}

	private static readonly int[] Conversions = new int[15]
	{
		24573, 17406, 24575, 24575, 24575, 24575, 24575, 24575, 24575, 24575,
		24573, 24573, 24573, 24576, 32767
	};

	private static readonly int[] ImplicitConversions = new int[15]
	{
		1, 7650, 7508, 8184, 7504, 8160, 7488, 8064, 7424, 7680,
		3072, 2048, 4096, 8192, 16384
	};

	private static readonly Type[] InbuiltTypes = new Type[15]
	{
		typeof(bool),
		typeof(char),
		typeof(sbyte),
		typeof(byte),
		typeof(short),
		typeof(ushort),
		typeof(int),
		typeof(uint),
		typeof(long),
		typeof(ulong),
		typeof(float),
		typeof(double),
		typeof(decimal),
		typeof(DateTime),
		typeof(string)
	};

	private static readonly Type[] NumericTypes = new Type[11]
	{
		typeof(byte),
		typeof(decimal),
		typeof(double),
		typeof(short),
		typeof(int),
		typeof(long),
		typeof(sbyte),
		typeof(float),
		typeof(ushort),
		typeof(uint),
		typeof(ulong)
	};

	public static bool AcceptsNull(Type P_0)
	{
		if (P_0.IsValueType)
		{
			return IsNullableType(P_0);
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AcceptsNull<T>()
	{
		return default(T) == null;
	}

	public static bool CanCast<T>(object? P_0)
	{
		if (!(P_0 is T))
		{
			if (P_0 == null)
			{
				return AcceptsNull<T>();
			}
			return false;
		}
		return true;
	}

	[RequiresUnreferencedCode("Conversion methods are required for type conversion, including op_Implicit, op_Explicit, Parse and TypeConverter.")]
	public static bool TryConvert(Type to, object? value, CultureInfo? culture, out object? result)
	{
		if (to == typeof(object))
		{
			result = value;
			return true;
		}
		if (value == null)
		{
			result = null;
			return AcceptsNull(to);
		}
		if (value == AvaloniaProperty.UnsetValue)
		{
			result = value;
			return true;
		}
		Type type = Nullable.GetUnderlyingType(to) ?? to;
		Type type2 = value.GetType();
		if (type.IsAssignableFrom(type2))
		{
			result = value;
			return true;
		}
		if (type == typeof(string))
		{
			result = Convert.ToString(value, culture);
			return true;
		}
		if (type.IsEnum && type2 == typeof(string) && Enum.IsDefined(type, (string)value))
		{
			result = Enum.Parse(type, (string)value);
			return true;
		}
		if (!type2.IsEnum && type.IsEnum)
		{
			result = null;
			if (TryConvert(Enum.GetUnderlyingType(type), value, culture, out object result2))
			{
				result = Enum.ToObject(type, result2);
				return true;
			}
		}
		if (type2.IsEnum && IsNumeric(type))
		{
			try
			{
				result = Convert.ChangeType((int)value, type, culture);
				return true;
			}
			catch
			{
				result = null;
				return false;
			}
		}
		int num = Array.IndexOf(InbuiltTypes, type2);
		int num2 = Array.IndexOf(InbuiltTypes, type);
		if (num != -1 && num2 != -1 && (Conversions[num] & (1 << num2)) != 0)
		{
			try
			{
				result = Convert.ChangeType(value, type, culture);
				return true;
			}
			catch
			{
				result = null;
				return false;
			}
		}
		TypeConverter converter = TypeDescriptor.GetConverter(type);
		if (converter.CanConvertFrom(type2))
		{
			result = converter.ConvertFrom(null, culture, value);
			return true;
		}
		TypeConverter converter2 = TypeDescriptor.GetConverter(type2);
		if (converter2.CanConvertTo(type))
		{
			result = converter2.ConvertTo(null, culture, value, type);
			return true;
		}
		MethodInfo methodInfo = FindTypeConversionOperatorMethod(type2, type, OperatorType.Implicit | OperatorType.Explicit);
		if (methodInfo != null)
		{
			result = methodInfo.Invoke(null, new object[1] { value });
			return true;
		}
		result = null;
		return false;
	}

	[RequiresUnreferencedCode("Implicit conversion methods are required for type conversion.")]
	public static bool TryConvertImplicit(Type P_0, object? P_1, out object? P_2)
	{
		if (P_1 == null)
		{
			P_2 = null;
			return AcceptsNull(P_0);
		}
		if (P_1 == AvaloniaProperty.UnsetValue)
		{
			P_2 = P_1;
			return true;
		}
		Type type = P_1.GetType();
		if (P_0.IsAssignableFrom(type))
		{
			P_2 = P_1;
			return true;
		}
		int num = Array.IndexOf(InbuiltTypes, type);
		int num2 = Array.IndexOf(InbuiltTypes, P_0);
		if (num != -1 && num2 != -1 && (ImplicitConversions[num] & (1 << num2)) != 0)
		{
			try
			{
				P_2 = Convert.ChangeType(P_1, P_0, CultureInfo.InvariantCulture);
				return true;
			}
			catch
			{
				P_2 = null;
				return false;
			}
		}
		MethodInfo methodInfo = FindTypeConversionOperatorMethod(type, P_0, OperatorType.Implicit);
		if (methodInfo != null)
		{
			P_2 = methodInfo.Invoke(null, new object[1] { P_1 });
			return true;
		}
		P_2 = null;
		return false;
	}

	public static bool IsNumeric(Type P_0)
	{
		Type underlyingType = Nullable.GetUnderlyingType(P_0);
		if (underlyingType != null)
		{
			return IsNumeric(underlyingType);
		}
		return Enumerable.Contains(NumericTypes, P_0);
	}

	private static bool IsNullableType(Type P_0)
	{
		if (P_0.IsGenericType)
		{
			return P_0.GetGenericTypeDefinition() == typeof(Nullable<>);
		}
		return false;
	}

	internal static MethodInfo? FindTypeConversionOperatorMethod([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] Type P_0, Type P_1, OperatorType P_2)
	{
		bool flag = P_2.HasAllFlags(OperatorType.Implicit);
		bool flag2 = P_2.HasAllFlags(OperatorType.Explicit);
		MethodInfo[] methods = P_0.GetMethods();
		foreach (MethodInfo methodInfo in methods)
		{
			if (methodInfo.IsSpecialName && !(methodInfo.ReturnType != P_1))
			{
				if (flag && methodInfo.Name == "op_Implicit")
				{
					return methodInfo;
				}
				if (flag2 && methodInfo.Name == "op_Explicit")
				{
					return methodInfo;
				}
			}
		}
		return null;
	}

	internal static bool IdentityEquals(object? P_0, object? P_1, Type P_2)
	{
		if (P_2.IsValueType || P_2 == typeof(string))
		{
			return object.Equals(P_0, P_1);
		}
		return P_0 == P_1;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.UriExtensions
using System;

internal static class UriExtensions
{
	public static bool IsAbsoluteResm(this Uri P_0)
	{
		if (P_0.IsAbsoluteUri)
		{
			return IsResm(P_0);
		}
		return false;
	}

	public static bool IsResm(this Uri P_0)
	{
		return P_0.Scheme == "resm";
	}

	public static bool IsAvares(this Uri P_0)
	{
		return P_0.Scheme == "avares";
	}

	public static bool IsFontCollection(this Uri P_0)
	{
		return P_0.Scheme == "fonts";
	}

	public static Uri EnsureAbsolute(this Uri P_0, Uri? P_1)
	{
		if (P_0.IsAbsoluteUri)
		{
			return P_0;
		}
		if (P_1 == null)
		{
			throw new ArgumentException($"Relative uri {P_0} without base url");
		}
		if (!P_1.IsAbsoluteUri)
		{
			throw new ArgumentException($"Base uri {P_1} is relative");
		}
		return new Uri(P_1, P_0);
	}

	public static string GetUnescapeAbsolutePath(this Uri P_0)
	{
		return Uri.UnescapeDataString(P_0.AbsolutePath);
	}

	public static string GetUnescapeAbsoluteUri(this Uri P_0)
	{
		return Uri.UnescapeDataString(P_0.AbsoluteUri);
	}

	public static string GetAssemblyNameFromQuery(this Uri P_0)
	{
		string text = Uri.UnescapeDataString(P_0.Query);
		int num;
		for (num = 1; num < text.Length; num++)
		{
			bool flag = false;
			for (int i = 0; i < "assembly".Length && text[num] == "assembly"[i]; i++)
			{
				flag = i == "assembly".Length - 1;
				num++;
			}
			num++;
			int num2 = num;
			for (; num < text.Length && text[num] != '&'; num++)
			{
			}
			if (flag)
			{
				return text.Substring(num2, num - num2);
			}
		}
		return "";
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ValueSpan<T>
public readonly record struct ValueSpan<T>
{
	public int Start { get; }

	public int Length { get; }

	public T Value { get; }

	public ValueSpan(int P_0, int P_1, T P_2)
	{
		Start = P_0;
		Length = P_1;
		Value = P_2;
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.WeakEvent
using System;
using Avalonia.Utilities;

public class WeakEvent
{
	public static WeakEvent<TSender, TEventArgs> Register<TSender, TEventArgs>(Action<TSender, EventHandler<TEventArgs>> P_0, Action<TSender, EventHandler<TEventArgs>> P_1) where TSender : class
	{
		return new WeakEvent<TSender, TEventArgs>(P_0, P_1);
	}

	public static WeakEvent<TSender, TEventArgs> Register<TSender, TEventArgs>(Func<TSender, EventHandler<TEventArgs>, Action> P_0) where TSender : class where TEventArgs : EventArgs
	{
		return new WeakEvent<TSender, TEventArgs>(P_0);
	}

	public static WeakEvent<TSender, EventArgs> Register<TSender>(Action<TSender, EventHandler> P_0, Action<TSender, EventHandler> P_1) where TSender : class
	{
		return Register(delegate(TSender s, EventHandler<EventArgs> h)
		{
			EventHandler handler = delegate(object? _, EventArgs e)
			{
				h(s, e);
			};
			P_0(s, handler);
			return delegate
			{
				P_1(s, handler);
			};
		});
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.WeakEvent<TSender,TEventArgs>
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia.Collections.Pooled;
using Avalonia.Threading;
using Avalonia.Utilities;

public sealed class WeakEvent<TSender, TEventArgs> : WeakEvent where TSender : class
{
	private sealed class Subscription
	{
		private readonly WeakEvent<TSender, TEventArgs> _ev;

		private readonly TSender _target;

		private readonly Action _compact;

		private readonly WeakHashList<IWeakEventSubscriber<TEventArgs>> _list = new WeakHashList<IWeakEventSubscriber<TEventArgs>>();

		private readonly object _lock = new object();

		private Action? _unsubscribe;

		private bool _compactScheduled;

		private bool _destroyed;

		public Subscription(WeakEvent<TSender, TEventArgs> P_0, TSender P_1)
		{
			_ev = P_0;
			_target = P_1;
			_compact = Compact;
		}

		private void Destroy()
		{
			if (!_destroyed)
			{
				_destroyed = true;
				_unsubscribe?.Invoke();
				_ev._subscriptions.Remove(_target);
			}
		}

		public bool Add(IWeakEventSubscriber<TEventArgs> P_0)
		{
			if (_destroyed)
			{
				return false;
			}
			lock (_lock)
			{
				if (_destroyed)
				{
					return false;
				}
				if (_unsubscribe == null)
				{
					_unsubscribe = _ev._subscribe(_target, OnEvent);
				}
				_list.Add(P_0);
				return true;
			}
		}

		public void Remove(IWeakEventSubscriber<TEventArgs> P_0)
		{
			if (_destroyed)
			{
				return;
			}
			lock (_lock)
			{
				if (!_destroyed)
				{
					_list.Remove(P_0);
					if (_list.IsEmpty)
					{
						Destroy();
					}
					else if (_list.NeedCompact && _compactScheduled)
					{
						ScheduleCompact();
					}
				}
			}
		}

		private void ScheduleCompact()
		{
			if (!_compactScheduled && !_destroyed)
			{
				_compactScheduled = true;
				Dispatcher.UIThread.Post(_compact, DispatcherPriority.Background);
			}
		}

		private void Compact()
		{
			if (_destroyed)
			{
				return;
			}
			lock (_lock)
			{
				if (!_destroyed && _compactScheduled)
				{
					_compactScheduled = false;
					_list.Compact();
					if (_list.IsEmpty)
					{
						Destroy();
					}
				}
			}
		}

		private void OnEvent(object? sender, TEventArgs eventArgs)
		{
			PooledList<IWeakEventSubscriber<TEventArgs>> alive;
			lock (_lock)
			{
				alive = _list.GetAlive();
				if (alive == null)
				{
					Destroy();
					return;
				}
			}
			Span<IWeakEventSubscriber<TEventArgs>> span = alive.Span;
			for (int i = 0; i < span.Length; i++)
			{
				span[i].OnEvent(_target, _ev, eventArgs);
			}
			lock (_lock)
			{
				WeakHashList<IWeakEventSubscriber<TEventArgs>>.ReturnToSharedPool(alive);
				if (_list.NeedCompact && !_compactScheduled)
				{
					ScheduleCompact();
				}
			}
		}
	}

	private readonly Func<TSender, EventHandler<TEventArgs>, Action> _subscribe;

	private readonly ConditionalWeakTable<TSender, Subscription> _subscriptions = new ConditionalWeakTable<TSender, Subscription>();

	private readonly ConditionalWeakTable<TSender, Subscription>.CreateValueCallback _createSubscription;

	internal WeakEvent(Action<TSender, EventHandler<TEventArgs>> P_0, Action<TSender, EventHandler<TEventArgs>> P_1)
	{
		_subscribe = delegate(TSender t, EventHandler<TEventArgs> s)
		{
			P_0(t, s);
			return delegate
			{
				P_1(t, s);
			};
		};
		_createSubscription = CreateSubscription;
	}

	internal WeakEvent(Func<TSender, EventHandler<TEventArgs>, Action> P_0)
	{
		_subscribe = P_0;
		_createSubscription = CreateSubscription;
	}

	public void Subscribe(TSender P_0, IWeakEventSubscriber<TEventArgs> P_1)
	{
		SpinWait spinWait = default(SpinWait);
		while (!_subscriptions.GetValue(P_0, _createSubscription).Add(P_1))
		{
			spinWait.SpinOnce();
		}
	}

	public void Unsubscribe(TSender P_0, IWeakEventSubscriber<TEventArgs> P_1)
	{
		if (_subscriptions.TryGetValue(P_0, out Subscription subscription))
		{
			subscription.Remove(P_1);
		}
	}

	private Subscription CreateSubscription(TSender key)
	{
		return new Subscription(this, key);
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.WeakEvents
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Threading;
using Avalonia.Utilities;

public class WeakEvents
{
	public static readonly WeakEvent<INotifyCollectionChanged, NotifyCollectionChangedEventArgs> CollectionChanged = WeakEvent.Register(delegate(INotifyCollectionChanged c, EventHandler<NotifyCollectionChangedEventArgs> s)
	{
		NotifyCollectionChangedEventHandler handler = delegate(object? _, NotifyCollectionChangedEventArgs e)
		{
			s(c, e);
		};
		c.CollectionChanged += handler;
		return delegate
		{
			c.CollectionChanged -= handler;
		};
	});

	public static readonly WeakEvent<INotifyPropertyChanged, PropertyChangedEventArgs> ThreadSafePropertyChanged = WeakEvent.Register(delegate(INotifyPropertyChanged s, EventHandler<PropertyChangedEventArgs> h)
	{
		bool unsubscribed = false;
		PropertyChangedEventHandler handler = delegate(object? _, PropertyChangedEventArgs e)
		{
			if (Dispatcher.UIThread.CheckAccess())
			{
				h(s, e);
			}
			else
			{
				Dispatcher.UIThread.Post(delegate
				{
					if (!unsubscribed)
					{
						h(s, e);
					}
				});
			}
		};
		s.PropertyChanged += handler;
		return delegate
		{
			unsubscribed = true;
			s.PropertyChanged -= handler;
		};
	});

	public static readonly WeakEvent<AvaloniaObject, AvaloniaPropertyChangedEventArgs> AvaloniaPropertyChanged = WeakEvent.Register(delegate(AvaloniaObject s, EventHandler<AvaloniaPropertyChangedEventArgs> h)
	{
		EventHandler<AvaloniaPropertyChangedEventArgs> handler = delegate(object? _, AvaloniaPropertyChangedEventArgs e)
		{
			h(s, e);
		};
		s.PropertyChanged += handler;
		return delegate
		{
			s.PropertyChanged -= handler;
		};
	});

	public static readonly WeakEvent<ICommand, EventArgs> CommandCanExecuteChanged = WeakEvent.Register(delegate(ICommand s, EventHandler h)
	{
		s.CanExecuteChanged += h;
	}, delegate(ICommand s, EventHandler h)
	{
		s.CanExecuteChanged -= h;
	});
}
