// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.DefaultRenderTimer
using System;
using System.Threading;
using Avalonia.Metadata;
using Avalonia.Rendering;

[PrivateApi]
public class DefaultRenderTimer : IRenderTimer
{
	private int _subscriberCount;

	private Action<TimeSpan>? _tick;

	private IDisposable? _subscription;

	public int FramesPerSecond { get; }

	public virtual bool RunsInBackground => true;

	public event Action<TimeSpan> Tick
	{
		add
		{
			_tick = (Action<TimeSpan>)Delegate.Combine(_tick, b);
			if (_subscriberCount++ == 0)
			{
				Start();
			}
		}
		remove
		{
			if (--_subscriberCount == 0)
			{
				Stop();
			}
			_tick = (Action<TimeSpan>)Delegate.Remove(_tick, value2);
		}
	}

	public DefaultRenderTimer(int P_0)
	{
		FramesPerSecond = P_0;
	}

	protected void Start()
	{
		_subscription = StartCore(InternalTick);
	}

	protected virtual IDisposable StartCore(Action<TimeSpan> P_0)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds(1.0 / (double)FramesPerSecond);
		return new Timer(delegate
		{
			P_0(TimeSpan.FromMilliseconds((double)Environment.TickCount));
		}, null, timeSpan, timeSpan);
	}

	protected void Stop()
	{
		_subscription?.Dispose();
		_subscription = null;
	}

	private void InternalTick(TimeSpan tickCount)
	{
		_tick?.Invoke(tickCount);
	}
}

