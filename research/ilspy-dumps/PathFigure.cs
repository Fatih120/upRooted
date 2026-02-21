// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.PathFigure
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Metadata;

public sealed class PathFigure : AvaloniaObject
{
	public static readonly StyledProperty<bool> IsClosedProperty;

	public static readonly StyledProperty<bool> IsFilledProperty;

	public static readonly DirectProperty<PathFigure, PathSegments?> SegmentsProperty;

	public static readonly StyledProperty<Point> StartPointProperty;

	[CompilerGenerated]
	private EventHandler? m_SegmentsInvalidated;

	private PathSegments? _segments;

	private IDisposable? _segmentsDisposable;

	private IDisposable? _segmentsPropertiesDisposable;

	public bool IsClosed
	{
		get
		{
			return GetValue(IsClosedProperty);
		}
		set
		{
			SetValue(IsClosedProperty, value2);
		}
	}

	public bool IsFilled
	{
		get
		{
			return GetValue(IsFilledProperty);
		}
		set
		{
			SetValue(IsFilledProperty, value2);
		}
	}

	[Content]
	public PathSegments? Segments
	{
		get
		{
			return _segments;
		}
		set
		{
			SetAndRaise(SegmentsProperty, ref _segments, value2);
		}
	}

	public Point StartPoint
	{
		get
		{
			return GetValue(StartPointProperty);
		}
		set
		{
			SetValue(StartPointProperty, value2);
		}
	}

	internal event EventHandler? SegmentsInvalidated
	{
		[CompilerGenerated]
		add
		{
			EventHandler eventHandler = this.m_SegmentsInvalidated;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Combine(eventHandler2, b);
				eventHandler = Interlocked.CompareExchange(ref this.m_SegmentsInvalidated, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler eventHandler = this.m_SegmentsInvalidated;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Remove(eventHandler2, value2);
				eventHandler = Interlocked.CompareExchange(ref this.m_SegmentsInvalidated, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	public PathFigure()
	{
		Segments = new PathSegments();
	}

	static PathFigure()
	{
		IsClosedProperty = AvaloniaProperty.Register<PathFigure, bool>("IsClosed", true);
		IsFilledProperty = AvaloniaProperty.Register<PathFigure, bool>("IsFilled", true);
		SegmentsProperty = AvaloniaProperty.RegisterDirect("Segments", (PathFigure f) => f.Segments, delegate(PathFigure f, PathSegments? s)
		{
			f.Segments = s;
		});
		StartPointProperty = AvaloniaProperty.Register<PathFigure, Point>("StartPoint");
		SegmentsProperty.Changed.AddClassHandler(delegate(PathFigure s, AvaloniaPropertyChangedEventArgs e)
		{
			s.OnSegmentsChanged();
		});
	}

	private void OnSegmentsChanged()
	{
		_segmentsDisposable?.Dispose();
		_segmentsPropertiesDisposable?.Dispose();
		_segmentsDisposable = _segments?.ForEachItem((Action<PathSegment>)delegate
		{
			InvalidateSegments();
		}, (Action<PathSegment>)delegate
		{
			InvalidateSegments();
		}, (Action)InvalidateSegments, false);
		_segmentsPropertiesDisposable = _segments?.TrackItemPropertyChanged(delegate
		{
			InvalidateSegments();
		});
	}

	private void InvalidateSegments()
	{
		this.SegmentsInvalidated?.Invoke(this, EventArgs.Empty);
	}

	public override string ToString()
	{
		object[] obj = new object[3] { StartPoint, null, null };
		IEnumerable<PathSegment> segments = _segments;
		obj[1] = string.Join(" ", segments ?? Enumerable.Empty<PathSegment>());
		obj[2] = (IsClosed ? "Z" : "");
		return FormattableString.Invariant(FormattableStringFactory.Create("M {0} {1}{2}", obj));
	}

	internal void ApplyTo(StreamGeometryContext P_0)
	{
		P_0.BeginFigure(StartPoint, IsFilled);
		if (Segments != null)
		{
			foreach (PathSegment segment in Segments)
			{
				segment.ApplyTo(P_0);
			}
		}
		P_0.EndFigure(IsClosed);
	}
}

