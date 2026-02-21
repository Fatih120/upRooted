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

