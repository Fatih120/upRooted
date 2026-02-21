// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.WeakEvent<TSender,TEventArgs>
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia.Collections.Pooled;
using Avalonia.Threading;
using Avalonia.Utilities;

public sealed class WeakEvent<TSender, TEventArgs> : WeakEvent where TSender : class
{
	private sealed class Subscription
	{
		private readonly WeakEvent<TSender, TEventArgs> _ev;

		private readonly TSender _target;

		private readonly Action _compact;

		private readonly WeakHashList<IWeakEventSubscriber<TEventArgs>> _list = new WeakHashList<IWeakEventSubscriber<TEventArgs>>();

		private readonly object _lock = new object();

		private Action? _unsubscribe;

		private bool _compactScheduled;

		private bool _destroyed;

		public Subscription(WeakEvent<TSender, TEventArgs> P_0, TSender P_1)
		{
			_ev = P_0;
			_target = P_1;
			_compact = Compact;
		}

		private void Destroy()
		{
			if (!_destroyed)
			{
				_destroyed = true;
				_unsubscribe?.Invoke();
				_ev._subscriptions.Remove(_target);
			}
		}

		public bool Add(IWeakEventSubscriber<TEventArgs> P_0)
		{
			if (_destroyed)
			{
				return false;
			}
			lock (_lock)
			{
				if (_destroyed)
				{
					return false;
				}
				if (_unsubscribe == null)
				{
					_unsubscribe = _ev._subscribe(_target, OnEvent);
				}
				_list.Add(P_0);
				return true;
			}
		}

		public void Remove(IWeakEventSubscriber<TEventArgs> P_0)
		{
			if (_destroyed)
			{
				return;
			}
			lock (_lock)
			{
				if (!_destroyed)
				{
					_list.Remove(P_0);
					if (_list.IsEmpty)
					{
						Destroy();
					}
					else if (_list.NeedCompact && _compactScheduled)
					{
						ScheduleCompact();
					}
				}
			}
		}

		private void ScheduleCompact()
		{
			if (!_compactScheduled && !_destroyed)
			{
				_compactScheduled = true;
				Dispatcher.UIThread.Post(_compact, DispatcherPriority.Background);
			}
		}

		private void Compact()
		{
			if (_destroyed)
			{
				return;
			}
			lock (_lock)
			{
				if (!_destroyed && _compactScheduled)
				{
					_compactScheduled = false;
					_list.Compact();
					if (_list.IsEmpty)
					{
						Destroy();
					}
				}
			}
		}

		private void OnEvent(object? sender, TEventArgs eventArgs)
		{
			PooledList<IWeakEventSubscriber<TEventArgs>> alive;
			lock (_lock)
			{
				alive = _list.GetAlive();
				if (alive == null)
				{
					Destroy();
					return;
				}
			}
			Span<IWeakEventSubscriber<TEventArgs>> span = alive.Span;
			for (int i = 0; i < span.Length; i++)
			{
				span[i].OnEvent(_target, _ev, eventArgs);
			}
			lock (_lock)
			{
				WeakHashList<IWeakEventSubscriber<TEventArgs>>.ReturnToSharedPool(alive);
				if (_list.NeedCompact && !_compactScheduled)
				{
					ScheduleCompact();
				}
			}
		}
	}

	private readonly Func<TSender, EventHandler<TEventArgs>, Action> _subscribe;

	private readonly ConditionalWeakTable<TSender, Subscription> _subscriptions = new ConditionalWeakTable<TSender, Subscription>();

	private readonly ConditionalWeakTable<TSender, Subscription>.CreateValueCallback _createSubscription;

	internal WeakEvent(Action<TSender, EventHandler<TEventArgs>> P_0, Action<TSender, EventHandler<TEventArgs>> P_1)
	{
		_subscribe = delegate(TSender t, EventHandler<TEventArgs> s)
		{
			P_0(t, s);
			return delegate
			{
				P_1(t, s);
			};
		};
		_createSubscription = CreateSubscription;
	}

	internal WeakEvent(Func<TSender, EventHandler<TEventArgs>, Action> P_0)
	{
		_subscribe = P_0;
		_createSubscription = CreateSubscription;
	}

	public void Subscribe(TSender P_0, IWeakEventSubscriber<TEventArgs> P_1)
	{
		SpinWait spinWait = default(SpinWait);
		while (!_subscriptions.GetValue(P_0, _createSubscription).Add(P_1))
		{
			spinWait.SpinOnce();
		}
	}

	public void Unsubscribe(TSender P_0, IWeakEventSubscriber<TEventArgs> P_1)
	{
		if (_subscriptions.TryGetValue(P_0, out Subscription subscription))
		{
			subscription.Remove(P_1);
		}
	}

	private Subscription CreateSubscription(TSender key)
	{
		return new Subscription(this, key);
	}
}

