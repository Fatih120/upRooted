// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.Geometry
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Reactive;

[TypeConverter(typeof(GeometryTypeConverter))]
public abstract class Geometry : AvaloniaObject
{
	public static readonly StyledProperty<Transform?> TransformProperty;

	private bool _isDirty = true;

	private readonly bool _canInvaldate = true;

	private IGeometryImpl? _platformImpl;

	[CompilerGenerated]
	private EventHandler? m_Changed;

	public Rect Bounds => PlatformImpl?.Bounds ?? default(Rect);

	internal IGeometryImpl? PlatformImpl
	{
		get
		{
			if (_isDirty)
			{
				IGeometryImpl geometryImpl = CreateDefiningGeometry();
				Transform transform = Transform;
				if (geometryImpl != null && transform != null && transform.Value != Matrix.Identity)
				{
					geometryImpl = geometryImpl.WithTransform(transform.Value);
				}
				_platformImpl = geometryImpl;
				_isDirty = false;
			}
			return _platformImpl;
		}
	}

	public Transform? Transform
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

	static Geometry()
	{
		TransformProperty = AvaloniaProperty.Register<Geometry, Transform>("Transform");
		TransformProperty.Changed.AddClassHandler(delegate(Geometry x, AvaloniaPropertyChangedEventArgs e)
		{
			x.TransformChanged(e);
		});
	}

	internal Geometry()
	{
	}

	public static Geometry Parse(string P_0)
	{
		return StreamGeometry.Parse(P_0);
	}

	public abstract Geometry Clone();

	public Rect GetRenderBounds(IPen P_0)
	{
		return PlatformImpl?.GetRenderBounds(P_0) ?? default(Rect);
	}

	protected static void AffectsGeometry(params AvaloniaProperty[] P_0)
	{
		AnonymousObserver<AvaloniaPropertyChangedEventArgs> anonymousObserver = new AnonymousObserver<AvaloniaPropertyChangedEventArgs>(AffectsGeometryInvalidate);
		for (int i = 0; i < P_0.Length; i++)
		{
			P_0[i].Changed.Subscribe(anonymousObserver);
		}
	}

	private protected abstract IGeometryImpl? CreateDefiningGeometry();

	protected void InvalidateGeometry()
	{
		if (_canInvaldate)
		{
			_isDirty = true;
			_platformImpl = null;
			this.Changed?.Invoke(this, EventArgs.Empty);
		}
	}

	private void TransformChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		Transform transform = (Transform)P_0.OldValue;
		Transform transform2 = (Transform)P_0.NewValue;
		if (transform != null)
		{
			transform.Changed -= TransformChanged;
		}
		if (transform2 != null)
		{
			transform2.Changed += TransformChanged;
		}
		TransformChanged(transform2, EventArgs.Empty);
	}

	private void TransformChanged(object? sender, EventArgs e)
	{
		Matrix? matrix = ((Transform)sender)?.Value;
		if (_platformImpl is ITransformedGeometryImpl transformedGeometryImpl)
		{
			if (!matrix.HasValue || matrix == Matrix.Identity)
			{
				_platformImpl = transformedGeometryImpl.SourceGeometry;
			}
			else if (matrix != transformedGeometryImpl.Transform)
			{
				_platformImpl = transformedGeometryImpl.SourceGeometry.WithTransform(matrix.Value);
			}
		}
		else if (_platformImpl != null && matrix.HasValue && matrix != Matrix.Identity)
		{
			_platformImpl = _platformImpl.WithTransform(matrix.Value);
		}
		this.Changed?.Invoke(this, EventArgs.Empty);
	}

	private static void AffectsGeometryInvalidate(AvaloniaPropertyChangedEventArgs e)
	{
		(e.Sender as Geometry)?.InvalidateGeometry();
	}
}

