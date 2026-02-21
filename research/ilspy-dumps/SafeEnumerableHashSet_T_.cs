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

