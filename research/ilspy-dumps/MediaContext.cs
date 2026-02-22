// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.MediaContext
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Reactive;
using Avalonia.Rendering;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Transport;
using Avalonia.Threading;
using Avalonia.Utilities;

internal class MediaContext : ICompositorScheduler
{
	private class MediaContextClock : IGlobalClock, IClock, IObservable<TimeSpan>
	{
		private readonly MediaContext _parent;

		private readonly List<IObserver<TimeSpan>> _observers = new List<IObserver<TimeSpan>>();

		private readonly List<IObserver<TimeSpan>> _newObservers = new List<IObserver<TimeSpan>>();

		private Queue<Action<TimeSpan>> _queuedAnimationFrames = new Queue<Action<TimeSpan>>();

		private Queue<Action<TimeSpan>> _queuedAnimationFramesNext = new Queue<Action<TimeSpan>>();

		private TimeSpan _currentAnimationTimestamp;

		public bool HasNewSubscriptions => _newObservers.Count > 0;

		public bool HasSubscriptions
		{
			get
			{
				if (_observers.Count <= 0)
				{
					return _queuedAnimationFrames.Count > 0;
				}
				return true;
			}
		}

		public PlayState PlayState
		{
			get
			{
				return PlayState.Run;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public MediaContextClock(MediaContext P_0)
		{
			_parent = P_0;
		}

		public IDisposable Subscribe(IObserver<TimeSpan> P_0)
		{
			_parent.ScheduleRender(false);
			_parent._dispatcher.VerifyAccess();
			_observers.Add(P_0);
			_newObservers.Add(P_0);
			return Disposable.Create(delegate
			{
				_parent._dispatcher.VerifyAccess();
				_observers.Remove(P_0);
			});
		}

		public void RequestAnimationFrame(Action<TimeSpan> P_0)
		{
			_parent.ScheduleRender(false);
			_queuedAnimationFrames.Enqueue(P_0);
		}

		public void Pulse(TimeSpan P_0)
		{
			_newObservers.Clear();
			_currentAnimationTimestamp = P_0;
			Queue<Action<TimeSpan>> queuedAnimationFramesNext = _queuedAnimationFramesNext;
			Queue<Action<TimeSpan>> queuedAnimationFrames = _queuedAnimationFrames;
			_queuedAnimationFrames = queuedAnimationFramesNext;
			_queuedAnimationFramesNext = queuedAnimationFrames;
			Queue<Action<TimeSpan>> queuedAnimationFramesNext2 = _queuedAnimationFramesNext;
			Action<TimeSpan> result;
			while (queuedAnimationFramesNext2.TryDequeue(out result))
			{
				result(P_0);
			}
			IObserver<TimeSpan>[] array = _observers.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnNext(_currentAnimationTimestamp);
			}
		}

		public void PulseNewSubscriptions()
		{
			IObserver<TimeSpan>[] array = _newObservers.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnNext(_currentAnimationTimestamp);
			}
			_newObservers.Clear();
		}
	}

	private class TopLevelInfo(Compositor P_0, CompositingRenderer P_1, ILayoutManager P_2) : IEquatable<TopLevelInfo>
	{
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(TopLevelInfo);
			}
		}

		public Compositor Compositor { get; } = P_0;

		public CompositingRenderer Renderer { get; } = P_1;

		public ILayoutManager LayoutManager { get; } = P_2;

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("TopLevelInfo");
			stringBuilder.Append(" { ");
			if (PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder P_0)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			P_0.Append("Compositor = ");
			P_0.Append(Compositor);
			P_0.Append(", Renderer = ");
			P_0.Append(Renderer);
			P_0.Append(", LayoutManager = ");
			P_0.Append(LayoutManager);
			return true;
		}

		[CompilerGenerated]
		public static bool operator !=(TopLevelInfo? P_0, TopLevelInfo? P_1)
		{
			return !(P_0 == P_1);
		}

		[CompilerGenerated]
		public static bool operator ==(TopLevelInfo? P_0, TopLevelInfo? P_1)
		{
			if ((object)P_0 != P_1)
			{
				return P_0?.Equals(P_1) ?? false;
			}
			return true;
		}

		[CompilerGenerated]
		public override int GetHashCode()
		{
			return ((EqualityComparer<Type>.Default.GetHashCode(EqualityContract) * -1521134295 + EqualityComparer<Avalonia.Rendering.Composition.Compositor>.Default.GetHashCode(Compositor)) * -1521134295 + EqualityComparer<CompositingRenderer>.Default.GetHashCode(Renderer)) * -1521134295 + EqualityComparer<ILayoutManager>.Default.GetHashCode(LayoutManager);
		}

		[CompilerGenerated]
		public override bool Equals(object? P_0)
		{
			return Equals(P_0 as TopLevelInfo);
		}

		[CompilerGenerated]
		public virtual bool Equals(TopLevelInfo? P_0)
		{
			if ((object)this != P_0)
			{
				if ((object)P_0 != null && EqualityContract == P_0.EqualityContract && EqualityComparer<Avalonia.Rendering.Composition.Compositor>.Default.Equals(Compositor, P_0.Compositor) && EqualityComparer<CompositingRenderer>.Default.Equals(Renderer, P_0.Renderer))
				{
					return EqualityComparer<ILayoutManager>.Default.Equals(LayoutManager, P_0.LayoutManager);
				}
				return false;
			}
			return true;
		}
	}

	private readonly MediaContextClock _clock;

	private readonly Stopwatch _time = Stopwatch.StartNew();

	private DispatcherOperation? _nextRenderOp;

	private DispatcherOperation? _inputMarkerOp;

	private TimeSpan _inputMarkerAddedAt;

	private bool _isRendering;

	private bool _animationsAreWaitingForComposition;

	private readonly double MaxSecondsWithoutInput;

	private readonly Action _render;

	private readonly Action _inputMarkerHandler;

	private readonly HashSet<Compositor> _requestedCommits = new HashSet<Compositor>();

	private readonly Dictionary<Compositor, CompositionBatch> _pendingCompositionBatches = new Dictionary<Compositor, CompositionBatch>();

	private readonly Dispatcher _dispatcher;

	private List<Action>? _invokeOnRenderCallbacks;

	private readonly Stack<List<Action>> _invokeOnRenderCallbackListPool = new Stack<List<Action>>();

	private readonly DispatcherTimer _animationsTimer = new DispatcherTimer(DispatcherPriority.Render)
	{
		Interval = TimeSpan.FromMilliseconds(16.0)
	};

	private readonly Dictionary<object, TopLevelInfo> _topLevels = new Dictionary<object, TopLevelInfo>();

	public IGlobalClock Clock => _clock;

	public static MediaContext Instance
	{
		get
		{
			MediaContext mediaContext = AvaloniaLocator.Current.GetService<MediaContext>();
			if (mediaContext == null)
			{
				DispatcherOptions dispatcherOptions = AvaloniaLocator.Current.GetService<DispatcherOptions>() ?? new DispatcherOptions();
				mediaContext = new MediaContext(Dispatcher.UIThread, dispatcherOptions.InputStarvationTimeout);
				AvaloniaLocator.CurrentMutable.Bind<MediaContext>().ToConstant(mediaContext);
			}
			return mediaContext;
		}
	}

	public void RequestAnimationFrame(Action<TimeSpan> P_0)
	{
		_clock.RequestAnimationFrame(P_0);
	}

	private CompositionBatch CommitCompositor(Compositor P_0)
	{
		_requestedCommits.Remove(P_0);
		CompositionBatch commit = P_0.Commit();
		_pendingCompositionBatches[P_0] = commit;
		commit.Processed.ContinueWith(delegate
		{
			_dispatcher.Post(delegate
			{
				CompositionBatchFinished(P_0, commit);
			}, DispatcherPriority.Send);
		}, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		return commit;
	}

	private void CompositionBatchFinished(Compositor P_0, CompositionBatch P_1)
	{
		if (_pendingCompositionBatches.TryGetValue(P_0, out CompositionBatch value) && value == P_1)
		{
			_pendingCompositionBatches.Remove(P_0);
		}
		if (_pendingCompositionBatches.Count == 0)
		{
			_animationsAreWaitingForComposition = false;
			if (_requestedCommits.Count != 0 || _clock.HasSubscriptions)
			{
				ScheduleRender(false);
			}
		}
	}

	private bool CommitCompositorsWithThrottling()
	{
		Dispatcher.UIThread.VerifyAccess();
		if (_pendingCompositionBatches.Count > 0)
		{
			return true;
		}
		if (_requestedCommits.Count == 0)
		{
			return false;
		}
		Compositor[] array = _requestedCommits.ToArray();
		foreach (Compositor compositor in array)
		{
			CommitCompositor(compositor);
		}
		return true;
	}

	private void SyncCommit(Compositor P_0, bool P_1, bool P_2)
	{
		if (AvaloniaLocator.Current.GetService<IPlatformRenderInterface>() == null)
		{
			return;
		}
		using (NonPumpingLockHelper.Use())
		{
			SyncWaitCompositorBatch(P_0, CommitCompositor(P_0), P_1, P_2);
		}
	}

	private void SyncWaitCompositorBatch(Compositor P_0, CompositionBatch P_1, bool P_2, bool P_3)
	{
		using (NonPumpingLockHelper.Use())
		{
			if (P_0 != null && !P_0.UseUiThreadForSynchronousCommits)
			{
				IRenderLoop loop = P_0.Loop;
				if (loop != null && loop.RunsInBackground)
				{
					(P_2 ? P_1.Rendered : P_1.Processed).Wait();
					return;
				}
			}
			P_0.Server.Render(P_3);
		}
	}

	public void ImmediateRenderRequested(CompositionTarget P_0, bool P_1)
	{
		SyncCommit(P_0.Compositor, true, P_1);
	}

	public void SyncDisposeCompositionTarget(CompositionTarget P_0)
	{
		using (NonPumpingLockHelper.Use())
		{
			CompositionBatch compositionBatch = P_0.Compositor.OobDispose(P_0);
			SyncWaitCompositorBatch(P_0.Compositor, compositionBatch, false, true);
		}
	}

	void ICompositorScheduler.CommitRequested(Compositor P_0)
	{
		if (_requestedCommits.Add(P_0))
		{
			ScheduleRender(false);
		}
	}

	private MediaContext(Dispatcher P_0, TimeSpan P_1)
	{
		_render = Render;
		_inputMarkerHandler = InputMarkerHandler;
		_clock = new MediaContextClock(this);
		_dispatcher = P_0;
		MaxSecondsWithoutInput = P_1.TotalSeconds;
		_animationsTimer.Tick += delegate
		{
			_animationsTimer.Stop();
			ScheduleRender(false);
		};
	}

	private void ScheduleRender(bool P_0)
	{
		if (_nextRenderOp != null)
		{
			if (P_0)
			{
				_nextRenderOp.Priority = DispatcherPriority.Render;
			}
			return;
		}
		DispatcherPriority dispatcherPriority = DispatcherPriority.Render;
		if (_inputMarkerOp == null)
		{
			_inputMarkerOp = _dispatcher.InvokeAsync(_inputMarkerHandler, DispatcherPriority.Input);
			_inputMarkerAddedAt = _time.Elapsed;
		}
		else if (!P_0 && (_time.Elapsed - _inputMarkerAddedAt).TotalSeconds > MaxSecondsWithoutInput)
		{
			dispatcherPriority = DispatcherPriority.Input;
		}
		DispatcherOperation dispatcherOperation = (_nextRenderOp = new DispatcherOperation(_dispatcher, dispatcherPriority, _render, true));
		_dispatcher.InvokeAsyncImpl(dispatcherOperation, CancellationToken.None);
	}

	private void InputMarkerHandler()
	{
		_inputMarkerOp = null;
	}

	private void Render()
	{
		try
		{
			_isRendering = true;
			RenderCore();
		}
		finally
		{
			_nextRenderOp = null;
			_isRendering = false;
		}
	}

	private void RenderCore()
	{
		TimeSpan elapsed = _time.Elapsed;
		if (!_animationsAreWaitingForComposition)
		{
			_clock.Pulse(elapsed);
		}
		for (int i = 0; i < 10; i++)
		{
			FireInvokeOnRenderCallbacks();
			if (!_clock.HasNewSubscriptions)
			{
				break;
			}
			_clock.PulseNewSubscriptions();
		}
		if (_requestedCommits.Count > 0 || _clock.HasSubscriptions)
		{
			_animationsAreWaitingForComposition = CommitCompositorsWithThrottling();
			if (!_animationsAreWaitingForComposition && _clock.HasSubscriptions)
			{
				_animationsTimer.Start();
			}
		}
	}

	public void AddTopLevel(object P_0, ILayoutManager P_1, IRenderer P_2)
	{
		if (!_topLevels.ContainsKey(P_0))
		{
			CompositingRenderer compositingRenderer = (CompositingRenderer)P_2;
			_topLevels.Add(P_0, new TopLevelInfo(compositingRenderer.Compositor, compositingRenderer, P_1));
			compositingRenderer.Start();
			ScheduleRender(true);
		}
	}

	public void RemoveTopLevel(object P_0)
	{
		if (_topLevels.TryGetValue(P_0, out TopLevelInfo value))
		{
			_topLevels.Remove(P_0);
			value.Renderer.Stop();
		}
	}

	private void FireInvokeOnRenderCallbacks()
	{
		int num = 0;
		int num2 = _invokeOnRenderCallbacks?.Count ?? 0;
		while (true)
		{
			if (num2 > 0)
			{
				num++;
				if (num > 153)
				{
					throw new InvalidOperationException("Infinite layout loop detected");
				}
				List<Action> invokeOnRenderCallbacks = _invokeOnRenderCallbacks;
				_invokeOnRenderCallbacks = null;
				for (int i = 0; i < num2; i++)
				{
					invokeOnRenderCallbacks[i]();
				}
				invokeOnRenderCallbacks.Clear();
				_invokeOnRenderCallbackListPool.Push(invokeOnRenderCallbacks);
				num2 = _invokeOnRenderCallbacks?.Count ?? 0;
			}
			else
			{
				num2 = _invokeOnRenderCallbacks?.Count ?? 0;
				if (num2 <= 0)
				{
					break;
				}
			}
		}
	}

	public void BeginInvokeOnRender(Action P_0)
	{
		if (_invokeOnRenderCallbacks == null)
		{
			_invokeOnRenderCallbacks = ((_invokeOnRenderCallbackListPool.Count > 0) ? _invokeOnRenderCallbackListPool.Pop() : new List<Action>());
		}
		_invokeOnRenderCallbacks.Add(P_0);
		if (!_isRendering)
		{
			ScheduleRender(true);
		}
	}
}

