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

