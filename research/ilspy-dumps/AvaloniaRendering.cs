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

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.ICustomHitTest
using Avalonia;

public interface ICustomHitTest
{
	bool HitTest(Point P_0);
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IHitTester
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Metadata;

[PrivateApi]
public interface IHitTester
{
	IEnumerable<Visual> HitTest(Point P_0, Visual P_1, Func<Visual, bool>? P_2);

	Visual? HitTestFirst(Point P_0, Visual P_1, Func<Visual, bool>? P_2);
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.ImmediateRenderer
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using Avalonia.Rendering;
using Avalonia.VisualTree;

internal class ImmediateRenderer
{
	public static void Render(DrawingContext P_0, Visual P_1)
	{
		Render(P_0, P_1, new Rect(P_1.Bounds.Size));
	}

	public static void Render(DrawingContext P_0, Visual P_1, Rect P_2)
	{
		using (P_0.PushTransform(Matrix.CreateTranslation(0.0 - P_2.Position.X, 0.0 - P_2.Position.Y)))
		{
			using (P_0.PushClip(P_2))
			{
				Render(P_0, P_1, new Rect(P_1.Bounds.Size), Matrix.Identity, new Rect(P_2.Size));
			}
		}
	}

	private static void Render(DrawingContext P_0, Visual P_1, Rect P_2, Matrix P_3, Rect P_4)
	{
		if (!P_1.IsVisible)
		{
			return;
		}
		double opacity = P_1.Opacity;
		if (!(opacity > 0.0))
		{
			return;
		}
		Rect rect = new Rect(P_2.Size);
		Matrix? matrix = P_1.RenderTransform?.Value;
		Matrix matrix3;
		if (matrix.HasValue)
		{
			Matrix valueOrDefault = matrix.GetValueOrDefault();
			Matrix matrix2 = Matrix.CreateTranslation(P_1.RenderTransformOrigin.ToPixels(P_1.Bounds.Size));
			matrix3 = -matrix2 * valueOrDefault * matrix2 * Matrix.CreateTranslation(P_2.Position);
		}
		else
		{
			matrix3 = Matrix.CreateTranslation(P_2.Position);
		}
		using ((P_1.RenderOptions != default(RenderOptions)) ? new DrawingContext.PushedState?(P_0.PushRenderOptions(P_1.RenderOptions)) : ((DrawingContext.PushedState?)null))
		{
			using (P_0.PushTransform(matrix3))
			{
				using (P_1.HasMirrorTransform ? new DrawingContext.PushedState?(P_0.PushTransform(new Matrix(-1.0, 0.0, 0.0, 1.0, P_1.Bounds.Width, 0.0))) : ((DrawingContext.PushedState?)null))
				{
					using (P_0.PushOpacity(opacity))
					{
						DrawingContext.PushedState? pushedState = ((P_1 == null || !P_1.ClipToBounds) ? ((DrawingContext.PushedState?)null) : ((!(P_1 is IVisualWithRoundRectClip visualWithRoundRectClip)) ? new DrawingContext.PushedState?(P_0.PushClip(rect)) : new DrawingContext.PushedState?(P_0.PushClip(new RoundedRect(in rect, visualWithRoundRectClip.ClipToBoundsRadius)))));
						using (pushedState)
						{
							Geometry clip = P_1.Clip;
							using ((clip != null) ? new DrawingContext.PushedState?(P_0.PushGeometryClip(clip)) : ((DrawingContext.PushedState?)null))
							{
								IBrush opacityMask = P_1.OpacityMask;
								using ((opacityMask != null) ? new DrawingContext.PushedState?(P_0.PushOpacityMask(opacityMask, rect)) : ((DrawingContext.PushedState?)null))
								{
									Matrix matrix4 = matrix3 * P_3;
									if (rect.TransformToAABB(matrix4).Intersects(P_4))
									{
										P_1.Render(P_0);
									}
									IEnumerable<Visual> enumerable;
									if (!P_1.HasNonUniformZIndexChildren)
									{
										IEnumerable<Visual> visualChildren = P_1.VisualChildren;
										enumerable = visualChildren;
									}
									else
									{
										IEnumerable<Visual> visualChildren = P_1.VisualChildren.OrderBy((Visual x) => x, ZIndexComparer.Instance);
										enumerable = visualChildren;
									}
									if (P_1.ClipToBounds)
									{
										matrix4 = Matrix.Identity;
										P_4 = rect;
									}
									foreach (Visual item in enumerable)
									{
										Render(P_0, item, item.Bounds, matrix4, P_4);
									}
								}
							}
						}
					}
				}
			}
		}
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IRenderer
using System;
using Avalonia;
using Avalonia.Metadata;
using Avalonia.Rendering;

[PrivateApi]
public interface IRenderer : IDisposable
{
	RendererDiagnostics Diagnostics { get; }

	void AddDirty(Visual P_0);

	void RecalculateChildren(Visual P_0);
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IRendererWithCompositor
using System;
using Avalonia.Rendering;
using Avalonia.Rendering.Composition;

internal interface IRendererWithCompositor : IRenderer, IDisposable
{
	Compositor Compositor { get; }
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IRenderLoop
using Avalonia.Metadata;
using Avalonia.Rendering;

[NotClientImplementable]
internal interface IRenderLoop
{
	bool RunsInBackground { get; }

	void Add(IRenderLoopTask P_0);
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IRenderLoopTask
internal interface IRenderLoopTask
{
	void Render();
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IRenderRoot
using Avalonia;
using Avalonia.Metadata;
using Avalonia.Rendering;

[NotClientImplementable]
public interface IRenderRoot
{
	Size ClientSize { get; }

	IRenderer Renderer { get; }

	IHitTester HitTester { get; }

	double RenderScaling { get; }

	Point PointToClient(PixelPoint P_0);

	PixelPoint PointToScreen(Point P_0);
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IRenderTimer
using System;
using Avalonia.Metadata;

[PrivateApi]
public interface IRenderTimer
{
	bool RunsInBackground { get; }

	event Action<TimeSpan> Tick;
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IVisualBrushInitialize
using Avalonia.Metadata;

[Unstable]
internal interface IVisualBrushInitialize
{
	void EnsureInitialized();
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.LayoutPassTiming
using System;

internal readonly record struct LayoutPassTiming
{
	public int PassCounter { get; }

	public TimeSpan Elapsed { get; }

	public LayoutPassTiming(int P_0, TimeSpan P_1)
	{
		PassCounter = P_0;
		Elapsed = P_1;
	}
}

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

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.PlatformRenderInterfaceContextManager
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Reactive;
using Avalonia.Rendering;

internal class PlatformRenderInterfaceContextManager
{
	private readonly IPlatformGraphics? _graphics;

	private IPlatformRenderInterfaceContext? _backend;

	private OwnedDisposable<IPlatformGraphicsContext>? _gpuContext;

	private readonly IPlatformGraphicsReadyStateFeature? _readyStateFeature;

	[CompilerGenerated]
	private Action? m_ContextDisposed;

	[CompilerGenerated]
	private Action<IPlatformRenderInterfaceContext>? m_ContextCreated;

	public bool IsReady => _readyStateFeature?.IsReady ?? true;

	public event Action? ContextDisposed
	{
		[CompilerGenerated]
		add
		{
			Action action = this.m_ContextDisposed;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_ContextDisposed, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action action = this.m_ContextDisposed;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_ContextDisposed, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action<IPlatformRenderInterfaceContext>? ContextCreated
	{
		[CompilerGenerated]
		add
		{
			Action<IPlatformRenderInterfaceContext> action = this.m_ContextCreated;
			Action<IPlatformRenderInterfaceContext> action2;
			do
			{
				action2 = action;
				Action<IPlatformRenderInterfaceContext> action3 = (Action<IPlatformRenderInterfaceContext>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_ContextCreated, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<IPlatformRenderInterfaceContext> action = this.m_ContextCreated;
			Action<IPlatformRenderInterfaceContext> action2;
			do
			{
				action2 = action;
				Action<IPlatformRenderInterfaceContext> action3 = (Action<IPlatformRenderInterfaceContext>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_ContextCreated, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public PlatformRenderInterfaceContextManager(IPlatformGraphics? P_0)
	{
		_graphics = P_0;
		_readyStateFeature = (_graphics as IPlatformGraphicsWithFeatures)?.TryGetFeature<IPlatformGraphicsReadyStateFeature>();
	}

	public void EnsureValidBackendContext()
	{
		if (!IsReady)
		{
			throw new InvalidOperationException("Platform graphics isn't ready yet");
		}
		if (_backend != null)
		{
			ref OwnedDisposable<IPlatformGraphicsContext>? gpuContext = ref _gpuContext;
			if (!gpuContext.HasValue || !gpuContext.GetValueOrDefault().Value.IsLost)
			{
				return;
			}
		}
		_backend?.Dispose();
		_backend = null;
		if (_gpuContext.HasValue)
		{
			_gpuContext?.Dispose();
			_gpuContext = null;
			this.ContextDisposed?.Invoke();
		}
		if (_graphics != null)
		{
			IPlatformGraphicsReadyStateFeature? readyStateFeature = _readyStateFeature;
			if (readyStateFeature == null || readyStateFeature.UsesContexts)
			{
				if (_graphics.UsesSharedContext)
				{
					_gpuContext = new OwnedDisposable<IPlatformGraphicsContext>(_graphics.GetSharedContext(), false);
				}
				else
				{
					_gpuContext = new OwnedDisposable<IPlatformGraphicsContext>(_graphics.CreateContext(), true);
				}
			}
		}
		_backend = AvaloniaLocator.Current.GetRequiredService<IPlatformRenderInterface>().CreateBackendContext(_gpuContext?.Value);
		this.ContextCreated?.Invoke(_backend);
	}

	public IDisposable EnsureCurrent()
	{
		EnsureValidBackendContext();
		if (_gpuContext.HasValue)
		{
			return _gpuContext.Value.Value.EnsureCurrent();
		}
		return Disposable.Empty;
	}

	public IRenderTarget CreateRenderTarget(IEnumerable<object> P_0)
	{
		EnsureValidBackendContext();
		return _backend.CreateRenderTarget(P_0);
	}
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.RendererDebugOverlays
using System;

[Flags]
public enum RendererDebugOverlays
{
	None = 0,
	Fps = 1,
	DirtyRects = 2,
	LayoutTimeGraph = 4,
	RenderTimeGraph = 8
}

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.RendererDiagnostics
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia.Rendering;

public class RendererDiagnostics : INotifyPropertyChanged
{
	private RendererDebugOverlays _debugOverlays;

	private LayoutPassTiming _lastLayoutPassTiming;

	private PropertyChangedEventArgs? _debugOverlaysChangedEventArgs;

	private PropertyChangedEventArgs? _lastLayoutPassTimingChangedEventArgs;

	[CompilerGenerated]
	private PropertyChangedEventHandler? m_PropertyChanged;

	public RendererDebugOverlays DebugOverlays
	{
		get
		{
			return _debugOverlays;
		}
		set
		{
			if (_debugOverlays != rendererDebugOverlays)
			{
				_debugOverlays = rendererDebugOverlays;
				OnPropertyChanged(_debugOverlaysChangedEventArgs ?? (_debugOverlaysChangedEventArgs = new PropertyChangedEventArgs("DebugOverlays")));
			}
		}
	}

	internal LayoutPassTiming LastLayoutPassTiming
	{
		get
		{
			return _lastLayoutPassTiming;
		}
		set
		{
			if (!_lastLayoutPassTiming.Equals(layoutPassTiming))
			{
				_lastLayoutPassTiming = layoutPassTiming;
				OnPropertyChanged(_lastLayoutPassTimingChangedEventArgs ?? (_lastLayoutPassTimingChangedEventArgs = new PropertyChangedEventArgs("LastLayoutPassTiming")));
			}
		}
	}

	public event PropertyChangedEventHandler? PropertyChanged
	{
		[CompilerGenerated]
		add
		{
			PropertyChangedEventHandler propertyChangedEventHandler = this.m_PropertyChanged;
			PropertyChangedEventHandler propertyChangedEventHandler2;
			do
			{
				propertyChangedEventHandler2 = propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, b);
				propertyChangedEventHandler = Interlocked.CompareExchange(ref this.m_PropertyChanged, propertyChangedEventHandler3, propertyChangedEventHandler2);
			}
			while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			PropertyChangedEventHandler propertyChangedEventHandler = this.m_PropertyChanged;
			PropertyChangedEventHandler propertyChangedEventHandler2;
			do
			{
				propertyChangedEventHandler2 = propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value2);
				propertyChangedEventHandler = Interlocked.CompareExchange(ref this.m_PropertyChanged, propertyChangedEventHandler3, propertyChangedEventHandler2);
			}
			while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
		}
	}

	protected virtual void OnPropertyChanged(PropertyChangedEventArgs P_0)
	{
		this.PropertyChanged?.Invoke(this, P_0);
	}
}

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

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.SceneInvalidatedEventArgs
using System;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Metadata;
using Avalonia.Rendering;

[PrivateApi]
public class SceneInvalidatedEventArgs : EventArgs
{
	[CompilerGenerated]
	private readonly IRenderRoot _003CRenderRoot_003Ek__BackingField;

	public Rect DirtyRect { get; }

	public SceneInvalidatedEventArgs(IRenderRoot P_0, Rect P_1)
	{
		_003CRenderRoot_003Ek__BackingField = P_0;
		DirtyRect = P_1;
	}
}

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

// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.ZIndexComparer
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Rendering;

internal class ZIndexComparer : IComparer<Visual>
{
	public static readonly ZIndexComparer Instance = new ZIndexComparer();

	public static readonly Comparison<Visual> ComparisonInstance = Instance.Compare;

	public int Compare(Visual? x, Visual? y)
	{
		return (x?.ZIndex ?? 0).CompareTo(y?.ZIndex ?? 0);
	}
}
