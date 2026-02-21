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

