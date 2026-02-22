// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.PathGeometry
using System;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Platform;
using Avalonia.Visuals.Platform;

public class PathGeometry : StreamGeometry
{
	public static readonly DirectProperty<PathGeometry, PathFigures?> FiguresProperty;

	public static readonly StyledProperty<FillRule> FillRuleProperty;

	private PathFigures? _figures;

	private IDisposable? _figuresObserver;

	private IDisposable? _figuresPropertiesObserver;

	[Content]
	public PathFigures? Figures
	{
		get
		{
			return _figures;
		}
		set
		{
			SetAndRaise(FiguresProperty, ref _figures, value2);
		}
	}

	public FillRule FillRule
	{
		get
		{
			return GetValue(FillRuleProperty);
		}
		set
		{
			SetValue(FillRuleProperty, value2);
		}
	}

	static PathGeometry()
	{
		FiguresProperty = AvaloniaProperty.RegisterDirect("Figures", (PathGeometry g) => g.Figures, delegate(PathGeometry g, PathFigures? f)
		{
			g.Figures = f;
		});
		FillRuleProperty = AvaloniaProperty.Register<PathGeometry, FillRule>("FillRule", FillRule.EvenOdd);
		FiguresProperty.Changed.AddClassHandler(delegate(PathGeometry s, AvaloniaPropertyChangedEventArgs e)
		{
			s.OnFiguresChanged(e.NewValue as PathFigures);
		});
	}

	public PathGeometry()
	{
		Figures = new PathFigures();
	}

	public new static PathGeometry Parse(string P_0)
	{
		PathGeometry pathGeometry = new PathGeometry();
		using PathGeometryContext pathGeometryContext = new PathGeometryContext(pathGeometry);
		using PathMarkupParser pathMarkupParser = new PathMarkupParser(pathGeometryContext);
		pathMarkupParser.Parse(P_0);
		return pathGeometry;
	}

	private protected sealed override IGeometryImpl? CreateDefiningGeometry()
	{
		PathFigures figures = Figures;
		if (figures == null)
		{
			return null;
		}
		IStreamGeometryImpl streamGeometryImpl = AvaloniaLocator.Current.GetRequiredService<IPlatformRenderInterface>().CreateStreamGeometry();
		using StreamGeometryContext streamGeometryContext = new StreamGeometryContext(streamGeometryImpl.Open());
		streamGeometryContext.SetFillRule(FillRule);
		foreach (PathFigure item in figures)
		{
			item.ApplyTo(streamGeometryContext);
		}
		return streamGeometryImpl;
	}

	private void OnFiguresChanged(PathFigures? P_0)
	{
		_figuresObserver?.Dispose();
		_figuresPropertiesObserver?.Dispose();
		_figuresObserver = P_0?.ForEachItem(delegate(PathFigure s)
		{
			s.SegmentsInvalidated += InvalidateGeometryFromSegments;
			InvalidateGeometry();
		}, delegate(PathFigure s)
		{
			s.SegmentsInvalidated -= InvalidateGeometryFromSegments;
			InvalidateGeometry();
		}, base.InvalidateGeometry);
		_figuresPropertiesObserver = P_0?.TrackItemPropertyChanged(delegate
		{
			InvalidateGeometry();
		});
	}

	private void InvalidateGeometryFromSegments(object? _, EventArgs __)
	{
		InvalidateGeometry();
	}

	public override string ToString()
	{
		string text = ((_figures != null) ? string.Join(" ", _figures) : string.Empty);
		return FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}", (FillRule != FillRule.EvenOdd) ? "F1 " : "", text));
	}
}

