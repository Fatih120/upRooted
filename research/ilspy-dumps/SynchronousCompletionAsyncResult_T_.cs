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

