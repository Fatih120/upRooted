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

