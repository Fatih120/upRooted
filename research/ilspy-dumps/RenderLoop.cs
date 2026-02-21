// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.RenderLoop
using System;
using System.Collections.Generic;
using System.Threading;
using Avalonia;
using Avalonia.Logging;
using Avalonia.Rendering;
using Avalonia.Threading;

internal class RenderLoop : IRenderLoop
{
	private readonly List<IRenderLoopTask> _items = new List<IRenderLoopTask>();

	private readonly List<IRenderLoopTask> _itemsCopy = new List<IRenderLoopTask>();

	private IRenderTimer? _timer;

	private int _inTick;

	public static IRenderLoop LocatorAutoInstance
	{
		get
		{
			IRenderLoop renderLoop = AvaloniaLocator.Current.GetService<IRenderLoop>();
			if (renderLoop == null)
			{
				IRenderTimer requiredService = AvaloniaLocator.Current.GetRequiredService<IRenderTimer>();
				AvaloniaLocator.CurrentMutable.Bind<IRenderLoop>().ToConstant(renderLoop = new RenderLoop(requiredService));
			}
			return renderLoop;
		}
	}

	protected IRenderTimer Timer => _timer ?? (_timer = AvaloniaLocator.Current.GetRequiredService<IRenderTimer>());

	public bool RunsInBackground => Timer.RunsInBackground;

	public RenderLoop(IRenderTimer P_0)
	{
		_timer = P_0;
	}

	public void Add(IRenderLoopTask P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("i");
		}
		Dispatcher.UIThread.VerifyAccess();
		lock (_items)
		{
			_items.Add(P_0);
			if (_items.Count == 1)
			{
				Timer.Tick += TimerTick;
			}
		}
	}

	private void TimerTick(TimeSpan time)
	{
		if (Interlocked.CompareExchange(ref _inTick, 1, 0) != 0)
		{
			return;
		}
		try
		{
			lock (_items)
			{
				_itemsCopy.Clear();
				_itemsCopy.AddRange(_items);
			}
			for (int i = 0; i < _itemsCopy.Count; i++)
			{
				_itemsCopy[i].Render();
			}
			_itemsCopy.Clear();
		}
		catch (Exception ex)
		{
			Logger.TryGet(LogEventLevel.Error, "Visual")?.Log(this, "Exception in render loop: {Error}", ex);
		}
		finally
		{
			Interlocked.Exchange(ref _inTick, 0);
		}
	}
}

