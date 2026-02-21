// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.OwnedDisposable<T>
using System;

internal struct OwnedDisposable<T> : IDisposable where T : class, IDisposable
{
	private readonly bool _owns;

	private T? _value;

	public T Value => _value ?? throw new ObjectDisposedException("OwnedDisposable");

	public OwnedDisposable(T P_0, bool P_1)
	{
		_owns = P_1;
		_value = P_0;
	}

	public void Dispose()
	{
		if (_owns)
		{
			_value?.Dispose();
		}
		_value = null;
	}
}

