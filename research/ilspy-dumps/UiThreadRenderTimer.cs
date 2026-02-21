// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.UiThreadRenderTimer
using System;
using System.Diagnostics;
using Avalonia.Metadata;
using Avalonia.Rendering;
using Avalonia.Threading;

[PrivateApi]
public class UiThreadRenderTimer(int P_0) : DefaultRenderTimer(P_0)
{
	private class TimerInstance : IDisposable
	{
		private UiThreadRenderTimer _parent;

		private readonly Action<TimeSpan> _tick;

		private DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Render);

		private static readonly TimeSpan s_minInterval = TimeSpan.FromMilliseconds(1.0);

		private TimeSpan Interval { get; }

		public TimerInstance(UiThreadRenderTimer P_0, Action<TimeSpan> P_1)
		{
			_parent = P_0;
			_tick = P_1;
			_timer.Tick += OnTick;
			_timer.Interval = Interval;
			Interval = TimeSpan.FromSeconds(1.0 / (double)_parent.FramesPerSecond);
			_timer.Start();
		}

		private void OnTick(object? sender, EventArgs e)
		{
			TimeSpan elapsed = _parent._clock.Elapsed;
			TimeSpan timeSpan = elapsed + Interval;
			try
			{
				_tick(elapsed);
			}
			finally
			{
				TimeSpan elapsed2 = _parent._clock.Elapsed;
				TimeSpan timeSpan2 = timeSpan - elapsed2;
				if (timeSpan2 < s_minInterval)
				{
					timeSpan2 = s_minInterval;
				}
				_timer.Interval = timeSpan2;
			}
		}

		public void Dispose()
		{
			_timer.Stop();
		}
	}

	private readonly Stopwatch _clock = Stopwatch.StartNew();

	public override bool RunsInBackground => false;

	protected override IDisposable StartCore(Action<TimeSpan> P_0)
	{
		return new TimerInstance(this, P_0);
	}
}

