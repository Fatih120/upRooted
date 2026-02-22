// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.GradientBrush
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Metadata;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Transport;

public abstract class GradientBrush : Brush, IGradientBrush, IBrush, IMutableBrush
{
	public static readonly StyledProperty<GradientSpreadMethod> SpreadMethodProperty = AvaloniaProperty.Register<GradientBrush, GradientSpreadMethod>("SpreadMethod", GradientSpreadMethod.Pad);

	public static readonly StyledProperty<GradientStops> GradientStopsProperty = AvaloniaProperty.Register<GradientBrush, GradientStops>("GradientStops");

	private IDisposable? _gradientStopsSubscription;

	public GradientSpreadMethod SpreadMethod => GetValue(SpreadMethodProperty);

	[Content]
	public GradientStops GradientStops
	{
		get
		{
			return GetValue(GradientStopsProperty);
		}
		set
		{
			SetValue(GradientStopsProperty, value2);
		}
	}

	IReadOnlyList<IGradientStop> IGradientBrush.GradientStops => GradientStops;

	internal GradientBrush()
	{
		GradientStops = new GradientStops();
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		if (P_0.Property == GradientStopsProperty)
		{
			var (gradientStops, gradientStops2) = P_0.GetOldAndNewValue<GradientStops>();
			if (gradientStops != null)
			{
				gradientStops.CollectionChanged -= GradientStopsChanged;
				_gradientStopsSubscription?.Dispose();
			}
			if (gradientStops2 != null)
			{
				gradientStops2.CollectionChanged += GradientStopsChanged;
				_gradientStopsSubscription = gradientStops2.TrackItemPropertyChanged(GradientStopChanged);
			}
		}
		base.OnPropertyChanged(P_0);
	}

	private void GradientStopsChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		RegisterForSerialization();
	}

	private void GradientStopChanged(Tuple<object?, PropertyChangedEventArgs> e)
	{
		RegisterForSerialization();
	}

	private protected override void SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		base.SerializeChanges(P_0, P_1);
		P_1.Write(SpreadMethod);
		P_1.Write(GradientStops.Count);
		foreach (GradientStop gradientStop in GradientStops)
		{
			P_1.WriteObject(new ImmutableGradientStop(gradientStop.Offset, gradientStop.Color));
		}
	}

	public abstract IImmutableBrush ToImmutable();
}

