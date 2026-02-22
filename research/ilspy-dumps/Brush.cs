// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.Brush
using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Drawing;
using Avalonia.Rendering.Composition.Server;
using Avalonia.Rendering.Composition.Transport;

[TypeConverter(typeof(BrushConverter))]
public abstract class Brush : Animatable, IBrush, ICompositionRenderResource<IBrush>, ICompositionRenderResource, ICompositorSerializable
{
	public static readonly StyledProperty<double> OpacityProperty = AvaloniaProperty.Register<Brush, double>("Opacity", 1.0);

	public static readonly StyledProperty<ITransform?> TransformProperty = AvaloniaProperty.Register<Brush, ITransform>("Transform");

	public static readonly StyledProperty<RelativePoint> TransformOriginProperty = AvaloniaProperty.Register<Brush, RelativePoint>("TransformOrigin");

	private CompositorResourceHolder<ServerCompositionSimpleBrush> _resource;

	public double Opacity
	{
		get
		{
			return GetValue(OpacityProperty);
		}
		set
		{
			SetValue(OpacityProperty, value2);
		}
	}

	public ITransform? Transform
	{
		get
		{
			return GetValue(TransformProperty);
		}
		set
		{
			SetValue(TransformProperty, value2);
		}
	}

	public RelativePoint TransformOrigin
	{
		get
		{
			return GetValue(TransformOriginProperty);
		}
		set
		{
			SetValue(TransformOriginProperty, value2);
		}
	}

	internal abstract Func<Compositor, ServerCompositionSimpleBrush> Factory { get; }

	public static IBrush Parse(string P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("s");
		}
		if (P_0.Length > 0)
		{
			ISolidColorBrush knownBrush = KnownColors.GetKnownBrush(P_0);
			if (knownBrush != null)
			{
				return knownBrush;
			}
			if (Color.TryParse(P_0, out var color))
			{
				return new ImmutableSolidColorBrush(color);
			}
		}
		throw new FormatException("Invalid brush string: '" + P_0 + "'.");
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		if (P_0.Property == TransformProperty)
		{
			_resource.ProcessPropertyChangeNotification(P_0);
		}
		RegisterForSerialization();
		base.OnPropertyChanged(P_0);
	}

	private protected void RegisterForSerialization()
	{
		_resource.RegisterForInvalidationOnAllCompositors(this);
	}

	private protected bool IsOnCompositor(Compositor P_0)
	{
		return _resource.TryGetForCompositor(P_0) != null;
	}

	IBrush ICompositionRenderResource<IBrush>.GetForCompositor(Compositor P_0)
	{
		return _resource.GetForCompositor(P_0);
	}

	void ICompositionRenderResource.AddRefOnCompositor(Compositor P_0)
	{
		if (_resource.CreateOrAddRef(P_0, this, out ServerCompositionSimpleBrush _, Factory))
		{
			OnReferencedFromCompositor(P_0);
		}
	}

	private protected virtual void OnReferencedFromCompositor(Compositor P_0)
	{
		if (Transform is ICompositionRenderResource<ITransform> compositionRenderResource)
		{
			compositionRenderResource.AddRefOnCompositor(P_0);
		}
	}

	void ICompositionRenderResource.ReleaseOnCompositor(Compositor P_0)
	{
		if (_resource.Release(P_0))
		{
			OnUnreferencedFromCompositor(P_0);
		}
	}

	protected virtual void OnUnreferencedFromCompositor(Compositor P_0)
	{
		if (Transform is ICompositionRenderResource<ITransform> compositionRenderResource)
		{
			compositionRenderResource.ReleaseOnCompositor(P_0);
		}
	}

	SimpleServerObject? ICompositorSerializable.TryGetServer(Compositor P_0)
	{
		return _resource.TryGetForCompositor(P_0);
	}

	private protected virtual void SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		ServerCompositionSimpleBrush.SerializeAllChanges(P_1, Opacity, TransformOrigin, Transform.GetServer(P_0));
	}

	void ICompositorSerializable.SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		SerializeChanges(P_0, P_1);
	}
}

