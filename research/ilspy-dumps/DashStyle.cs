// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.DashStyle
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Collections;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Reactive;

public sealed class DashStyle : Animatable, IDashStyle
{
	public static readonly StyledProperty<AvaloniaList<double>?> DashesProperty;

	public static readonly StyledProperty<double> OffsetProperty;

	private static ImmutableDashStyle? s_dot;

	[CompilerGenerated]
	private EventHandler? m_Invalidated;

	public static IDashStyle Dot => s_dot ?? (s_dot = new ImmutableDashStyle(new double[2] { 0.0, 2.0 }, 0.0));

	public AvaloniaList<double>? Dashes
	{
		get
		{
			return GetValue(DashesProperty);
		}
		set
		{
			SetValue(DashesProperty, value2);
		}
	}

	public double Offset
	{
		get
		{
			return GetValue(OffsetProperty);
		}
		set
		{
			SetValue(OffsetProperty, value2);
		}
	}

	IReadOnlyList<double>? IDashStyle.Dashes => Dashes;

	internal event EventHandler? Invalidated
	{
		[CompilerGenerated]
		add
		{
			EventHandler eventHandler = this.m_Invalidated;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Combine(eventHandler2, b);
				eventHandler = Interlocked.CompareExchange(ref this.m_Invalidated, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler eventHandler = this.m_Invalidated;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Remove(eventHandler2, value2);
				eventHandler = Interlocked.CompareExchange(ref this.m_Invalidated, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	public DashStyle()
	{
	}

	public DashStyle(IEnumerable<double>? P_0, double P_1)
	{
		Dashes = (P_0 as AvaloniaList<double>) ?? new AvaloniaList<double>(P_0 ?? Array.Empty<double>());
		Offset = P_1;
	}

	static DashStyle()
	{
		DashesProperty = AvaloniaProperty.Register<DashStyle, AvaloniaList<double>>("Dashes");
		OffsetProperty = AvaloniaProperty.Register<DashStyle, double>("Offset", 0.0);
		AnonymousObserver<AvaloniaPropertyChangedEventArgs> anonymousObserver = new AnonymousObserver<AvaloniaPropertyChangedEventArgs>(delegate(AvaloniaPropertyChangedEventArgs e)
		{
			((DashStyle)e.Sender).Invalidated?.Invoke(e.Sender, EventArgs.Empty);
		});
		DashesProperty.Changed.Subscribe(anonymousObserver);
		OffsetProperty.Changed.Subscribe(anonymousObserver);
	}

	public ImmutableDashStyle ToImmutable()
	{
		return new ImmutableDashStyle(Dashes, Offset);
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == DashesProperty)
		{
			var (avaloniaList, avaloniaList2) = P_0.GetOldAndNewValue<AvaloniaList<double>>();
			if (avaloniaList != null)
			{
				avaloniaList.CollectionChanged -= DashesChanged;
			}
			if (avaloniaList2 != null)
			{
				avaloniaList2.CollectionChanged += DashesChanged;
			}
		}
	}

	private void DashesChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		this.Invalidated?.Invoke(this, e);
	}
}

