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

