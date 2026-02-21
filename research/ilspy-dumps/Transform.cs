// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.Transform
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Drawing;
using Avalonia.Rendering.Composition.Server;
using Avalonia.Rendering.Composition.Transport;

public abstract class Transform : Animatable, IMutableTransform, ITransform, ICompositionRenderResource<ITransform>, ICompositionRenderResource, ICompositorSerializable
{
	[CompilerGenerated]
	private EventHandler? m_Changed;

	private CompositorResourceHolder<ServerCompositionSimpleTransform> _resource;

	public abstract Matrix Value { get; }

	public event EventHandler? Changed
	{
		[CompilerGenerated]
		add
		{
			EventHandler eventHandler = this.m_Changed;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Combine(eventHandler2, b);
				eventHandler = Interlocked.CompareExchange(ref this.m_Changed, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler eventHandler = this.m_Changed;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Remove(eventHandler2, value2);
				eventHandler = Interlocked.CompareExchange(ref this.m_Changed, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	internal Transform()
	{
	}

	protected void RaiseChanged()
	{
		this.Changed?.Invoke(this, EventArgs.Empty);
	}

	public ImmutableTransform ToImmutable()
	{
		return new ImmutableTransform(Value);
	}

	public override string ToString()
	{
		return Value.ToString();
	}

	ITransform ICompositionRenderResource<ITransform>.GetForCompositor(Compositor P_0)
	{
		return _resource.GetForCompositor(P_0);
	}

	SimpleServerObject? ICompositorSerializable.TryGetServer(Compositor P_0)
	{
		return _resource.TryGetForCompositor(P_0);
	}

	void ICompositionRenderResource.AddRefOnCompositor(Compositor P_0)
	{
		_resource.CreateOrAddRef(P_0, this, out ServerCompositionSimpleTransform _, (Compositor cc) => new ServerCompositionSimpleTransform(cc.Server));
	}

	void ICompositionRenderResource.ReleaseOnCompositor(Compositor P_0)
	{
		_resource.Release(P_0);
	}

	void ICompositorSerializable.SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		ServerCompositionSimpleTransform.SerializeAllChanges(P_1, Value);
	}
}

