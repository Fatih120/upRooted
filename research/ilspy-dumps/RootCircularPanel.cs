// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootCircularPanel
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using RootApp.Client.Avalonia.Controls;

public class RootCircularPanel : Panel
{
	public static readonly StyledProperty<IEnumerable> ItemsSourceProperty;

	public static readonly StyledProperty<double> StartAngleProperty;

	public static readonly StyledProperty<double> EdgeMarginProperty;

	public static readonly StyledProperty<double> OverlapRatioProperty;

	private INotifyCollectionChanged? _observableItemsSource;

	private double _badgeSize;

	private double _radius;

	public IEnumerable ItemsSource => GetValue(ItemsSourceProperty);

	public double StartAngle => GetValue(StartAngleProperty);

	public double EdgeMargin => GetValue(EdgeMarginProperty);

	public double OverlapRatio => GetValue(OverlapRatioProperty);

	static RootCircularPanel()
	{
		ItemsSourceProperty = AvaloniaProperty.Register<RootCircularPanel, IEnumerable>("ItemsSource");
		StartAngleProperty = AvaloniaProperty.Register<RootCircularPanel, double>("StartAngle", -90.0);
		EdgeMarginProperty = AvaloniaProperty.Register<RootCircularPanel, double>("EdgeMargin", 3.0);
		OverlapRatioProperty = AvaloniaProperty.Register<RootCircularPanel, double>("OverlapRatio", 0.3);
		Layoutable.AffectsMeasure<RootCircularPanel>(new AvaloniaProperty[4] { ItemsSourceProperty, StartAngleProperty, EdgeMarginProperty, OverlapRatioProperty });
		ItemsSourceProperty.Changed.AddClassHandler(delegate(RootCircularPanel x, AvaloniaPropertyChangedEventArgs e)
		{
			x.OnItemsSourceChanged(e);
		});
	}

	private void OnItemsSourceChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		if (_observableItemsSource != null)
		{
			_observableItemsSource.CollectionChanged -= ItemsSourceCollectionChanged;
			_observableItemsSource = null;
		}
		if (P_0.OldValue is INotifyCollectionChanged notifyCollectionChanged)
		{
			notifyCollectionChanged.CollectionChanged -= ItemsSourceCollectionChanged;
		}
		if (P_0.NewValue is IEnumerable enumerable)
		{
			if (enumerable is INotifyCollectionChanged observableItemsSource)
			{
				_observableItemsSource = observableItemsSource;
				_observableItemsSource.CollectionChanged += ItemsSourceCollectionChanged;
			}
			RebuildChildren();
		}
		else
		{
			base.Children.Clear();
		}
		InvalidateMeasure();
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		if (_observableItemsSource != null)
		{
			_observableItemsSource.CollectionChanged -= ItemsSourceCollectionChanged;
			_observableItemsSource = null;
		}
	}

	private void ItemsSourceCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		switch (e.Action)
		{
		case NotifyCollectionChangedAction.Add:
			AddChildren(e.NewItems);
			break;
		case NotifyCollectionChangedAction.Remove:
			RemoveChildren(e.OldItems);
			break;
		case NotifyCollectionChangedAction.Replace:
			ReplaceChildren(e.OldItems, e.NewItems);
			break;
		case NotifyCollectionChangedAction.Reset:
			RebuildChildren();
			break;
		}
		InvalidateMeasure();
	}

	private void RebuildChildren()
	{
		base.Children.Clear();
		if (ItemsSource != null)
		{
			foreach (object item in ItemsSource)
			{
				Control control = CreateChild(item);
				base.Children.Add(control);
			}
		}
		InvalidateMeasure();
	}

	private void AddChildren(IList? P_0)
	{
		if (P_0 == null)
		{
			return;
		}
		foreach (object item in P_0)
		{
			Control control = CreateChild(item);
			base.Children.Add(control);
		}
	}

	private void RemoveChildren(IList? P_0)
	{
		if (P_0 == null)
		{
			return;
		}
		foreach (object item in P_0)
		{
			Control control = base.Children.FirstOrDefault((Control c) => c.DataContext == item);
			if (control != null)
			{
				base.Children.Remove(control);
			}
		}
	}

	private void ReplaceChildren(IList? P_0, IList? P_1)
	{
		RemoveChildren(P_0);
		AddChildren(P_1);
	}

	private Control CreateChild(object P_0)
	{
		return new ContentControl
		{
			DataContext = P_0,
			Content = P_0
		};
	}

	protected override Size MeasureOverride(Size P_0)
	{
		CalculateLayout(P_0);
		foreach (Control child in base.Children)
		{
			child.Measure(new Size(_badgeSize, _badgeSize));
		}
		return P_0;
	}

	protected override Size ArrangeOverride(Size P_0)
	{
		CalculateLayout(P_0);
		int count = base.Children.Count;
		if (count == 0)
		{
			return P_0;
		}
		double num = P_0.Width / 2.0;
		double num2 = P_0.Height / 2.0;
		if (count >= 4)
		{
			int num3 = count - 1;
			for (int i = 0; i < num3; i++)
			{
				double num4 = 360.0 / (double)num3;
				double num5 = StartAngle + (double)i * num4;
				double num6 = num5 * Math.PI / 180.0;
				double val = num + _radius * Math.Cos(num6) - _badgeSize / 2.0;
				double val2 = num2 - _radius * Math.Sin(num6) - _badgeSize / 2.0;
				val = Math.Max(EdgeMargin, Math.Min(val, P_0.Width - EdgeMargin - _badgeSize));
				val2 = Math.Max(EdgeMargin, Math.Min(val2, P_0.Height - EdgeMargin - _badgeSize));
				base.Children[i].Arrange(new Rect(val, val2, _badgeSize, _badgeSize));
			}
			int num7 = base.Children.Count - 1;
			Control control = base.Children[num7];
			control.Arrange(new Rect(num - _badgeSize / 2.0, num2 - _badgeSize / 2.0, _badgeSize, _badgeSize));
		}
		else
		{
			for (int j = 0; j < count; j++)
			{
				double val3;
				double val4;
				switch (count)
				{
				case 1:
					val3 = 0.0;
					val4 = 0.0;
					break;
				case 2:
				{
					double[] array2 = new double[2] { 135.0, -45.0 };
					double num10 = array2[j];
					double num11 = num10 * Math.PI / 180.0;
					val3 = num + _radius * Math.Cos(num11) - _badgeSize / 2.0;
					val4 = num2 - _radius * Math.Sin(num11) - _badgeSize / 2.0;
					break;
				}
				case 3:
				{
					double[] array = new double[3] { 90.0, 210.0, 330.0 };
					double num8 = array[j];
					double num9 = num8 * Math.PI / 180.0;
					val3 = num + _radius * Math.Cos(num9) - _badgeSize / 2.0;
					val4 = num2 - _radius * Math.Sin(num9) - _badgeSize / 2.0;
					break;
				}
				default:
					val3 = 0.0;
					val4 = 0.0;
					break;
				}
				if (count > 1)
				{
					val3 = Math.Max(EdgeMargin, Math.Min(val3, P_0.Width - EdgeMargin - _badgeSize));
					val4 = Math.Max(EdgeMargin, Math.Min(val4, P_0.Height - EdgeMargin - _badgeSize));
				}
				else
				{
					val3 = 0.0;
					val4 = 0.0;
				}
				base.Children[j].Arrange(new Rect(val3, val4, _badgeSize, _badgeSize));
			}
		}
		return P_0;
	}

	private void CalculateLayout(Size P_0)
	{
		int count = base.Children.Count;
		double num = Math.Min(P_0.Width, P_0.Height);
		if (count == 1)
		{
			_badgeSize = num;
			_radius = 0.0;
		}
		else if (count >= 4)
		{
			int num2 = count - 1;
			double num3 = Math.Sin(Math.PI / (double)num2);
			double num4 = num / 2.0 - EdgeMargin;
			double num5 = 1.0 - OverlapRatio + num3;
			_radius = num4 * (1.0 - OverlapRatio) / num5;
			_badgeSize = 2.0 * _radius * num3 / (1.0 - OverlapRatio);
			_badgeSize = Math.Min(_badgeSize, num - 2.0 * EdgeMargin);
			_radius = num / 2.0 - EdgeMargin - _badgeSize / 2.0;
		}
		else
		{
			double num6 = Math.Sin(Math.PI / (double)count);
			double num7 = num / 2.0 - EdgeMargin;
			double num8 = 1.0 - OverlapRatio + num6;
			_radius = num7 * (1.0 - OverlapRatio) / num8;
			_badgeSize = 2.0 * _radius * num6 / (1.0 - OverlapRatio);
			_badgeSize = Math.Min(_badgeSize, num - 2.0 * EdgeMargin);
			_radius = num / 2.0 - EdgeMargin - _badgeSize / 2.0;
		}
		foreach (Control child in base.Children)
		{
			child.Width = _badgeSize;
			child.Height = _badgeSize;
		}
	}
}

