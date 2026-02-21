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

