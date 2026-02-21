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

