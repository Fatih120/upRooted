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

