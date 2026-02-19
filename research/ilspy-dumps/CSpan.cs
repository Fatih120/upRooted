// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CSpan
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Metadata;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.Components.Geometries;

public class CSpan : CInline
{
	public static readonly StyledProperty<Thickness> BorderThicknessProperty;

	public static readonly StyledProperty<IBrush> BorderBrushProperty;

	public static readonly StyledProperty<IBrush> BorderBackgroundBrushProperty;

	public static readonly StyledProperty<CornerRadius> CornerRadiusProperty;

	public static readonly StyledProperty<BoxShadows> BoxShadowProperty;

	public static readonly StyledProperty<Thickness> PaddingProperty;

	public static readonly StyledProperty<Thickness> MarginProperty;

	public static readonly StyledProperty<double> PressedScaleProperty;

	public static readonly StyledProperty<IEnumerable<CInline>> ContentProperty;

	private Border? _border;

	[CompilerGenerated]
	private bool _003CIsPressed_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CHasBorder_003Ek__BackingField;

	public Thickness BorderThickness => GetValue(BorderThicknessProperty);

	public IBrush BorderBrush => GetValue(BorderBrushProperty);

	public IBrush BorderBackgroundBrush => GetValue(BorderBackgroundBrushProperty);

	public CornerRadius CornerRadius => GetValue(CornerRadiusProperty);

	public BoxShadows BoxShadow => GetValue(BoxShadowProperty);

	public Thickness Padding => GetValue(PaddingProperty);

	public Thickness Margin => GetValue(MarginProperty);

	public double PressedScale => GetValue(PressedScaleProperty);

	[Content]
	public IEnumerable<CInline> Content
	{
		get
		{
			return GetValue(ContentProperty);
		}
		set
		{
			SetValue(ContentProperty, value2);
		}
	}

	public bool IsPressed
	{
		[CompilerGenerated]
		get
		{
			return _003CIsPressed_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIsPressed_003Ek__BackingField = flag;
		}
	}

	public bool HasBorder
	{
		[CompilerGenerated]
		get
		{
			return _003CHasBorder_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CHasBorder_003Ek__BackingField = flag;
		}
	}

	static CSpan()
	{
		BorderThicknessProperty = AvaloniaProperty.Register<CSpan, Thickness>("BorderThickness");
		BorderBrushProperty = AvaloniaProperty.Register<CSpan, IBrush>("BorderBrush");
		BorderBackgroundBrushProperty = AvaloniaProperty.Register<CSpan, IBrush>("BorderBackgroundBrush");
		CornerRadiusProperty = AvaloniaProperty.Register<CSpan, CornerRadius>("CornerRadius");
		BoxShadowProperty = AvaloniaProperty.Register<CSpan, BoxShadows>("BoxShadow");
		PaddingProperty = AvaloniaProperty.Register<CSpan, Thickness>("Padding");
		MarginProperty = Layoutable.MarginProperty.AddOwner<CSpan>();
		PressedScaleProperty = AvaloniaProperty.Register<CSpan, double>("PressedScale", 1.0);
		ContentProperty = AvaloniaProperty.Register<CSpan, IEnumerable<CInline>>("Content");
		ContentProperty.Changed.AddClassHandler(delegate(CSpan x, AvaloniaPropertyChangedEventArgs e)
		{
			if (e.OldValue is IEnumerable<CInline> enumerable)
			{
				foreach (CInline item in enumerable)
				{
					x.LogicalChildren.Remove(item);
				}
			}
			if (e.NewValue is IEnumerable<CInline> enumerable2)
			{
				foreach (CInline item2 in enumerable2)
				{
					x.LogicalChildren.Add(item2);
				}
			}
		});
	}

	public CSpan(IEnumerable<CInline> P_0)
	{
		Content = P_0.ToArray();
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		switch (P_0.Property.Name)
		{
		case "BorderThickness":
		case "CornerRadius":
		case "BoxShadow":
		case "Padding":
		case "Margin":
			OnBorderPropertyChanged(true);
			break;
		case "BorderBrush":
			OnBorderPropertyChanged(false);
			break;
		case "BorderBackgroundBrush":
			OnBorderPropertyChanged(false);
			break;
		}
	}

	private void OnBorderPropertyChanged(bool P_0)
	{
		if (BorderThickness != default(Thickness) || Padding != default(Thickness) || CornerRadius != default(CornerRadius) || Margin != default(Thickness) || !BoxShadow.Equals(default(BoxShadows)))
		{
			if (_border == null)
			{
				_border = new Border();
				base.LogicalChildren.Add(_border);
				HasBorder = true;
			}
			_border.BorderThickness = BorderThickness;
			_border.BorderBrush = BorderBrush;
			_border.CornerRadius = CornerRadius;
			_border.BoxShadow = BoxShadow;
			_border.Padding = Padding;
			_border.Margin = Margin;
			_border.Background = BorderBackgroundBrush;
			base.Background = null;
		}
		else
		{
			if (_border != null)
			{
				base.LogicalChildren.Remove(_border);
			}
			_border = null;
		}
		if (P_0)
		{
			RequestMeasure();
		}
		else
		{
			RequestRender();
		}
	}

	protected override IEnumerable<CGeometry> MeasureOverride(double P_0, double P_1)
	{
		if (_border != null)
		{
			_border.Measure(Size.Infinity);
			double borderWidth = _border.DesiredSize.Width;
			double adjustedEntireWidth = P_0 - borderWidth;
			double adjustedRemainWidth = P_1 - borderWidth;
			List<CGeometry> allGeometries = new List<CGeometry>();
			double totalContentWidth = 0.0;
			bool hasLineBreak = false;
			foreach (CInline inline in Content)
			{
				foreach (CGeometry geo in inline.Measure(adjustedEntireWidth, adjustedRemainWidth))
				{
					allGeometries.Add(geo);
					totalContentWidth += geo.Width;
					if (geo.LineBreak)
					{
						hasLineBreak = true;
					}
					adjustedRemainWidth = ((!geo.LineBreak) ? (adjustedRemainWidth - geo.Width) : adjustedEntireWidth);
				}
			}
			double totalDecoratedWidth = totalContentWidth + borderWidth;
			if (!hasLineBreak)
			{
				yield return DecoratorGeometry.New(this, allGeometries, _border, false);
				yield break;
			}
			if (totalDecoratedWidth <= P_0)
			{
				allGeometries.Clear();
				foreach (CInline inline2 in Content)
				{
					foreach (CGeometry geo2 in inline2.Measure(adjustedEntireWidth, adjustedEntireWidth))
					{
						allGeometries.Add(geo2);
					}
				}
				yield return new LineBreakMarkGeometry(this);
				yield return DecoratorGeometry.New(this, allGeometries, _border, false);
				yield break;
			}
			foreach (CGeometry item in PrivateMeasure(_border, allGeometries))
			{
				yield return item;
			}
			yield break;
		}
		foreach (CGeometry item2 in PrivateMeasure(P_0, P_1))
		{
			yield return item2;
		}
	}

	private IEnumerable<CGeometry> PrivateMeasure(Border P_0, IEnumerable<CGeometry> P_1)
	{
		List<CGeometry> buffer = new List<CGeometry>();
		foreach (CGeometry adding in P_1)
		{
			if (adding is LineBreakMarkGeometry && buffer.Count == 0)
			{
				yield return adding;
				continue;
			}
			buffer.Add(adding);
			if (adding.LineBreak)
			{
				yield return DecoratorGeometry.New(this, buffer, P_0);
				buffer.Clear();
			}
		}
		if (buffer.Count != 0)
		{
			yield return DecoratorGeometry.New(this, buffer, P_0);
		}
	}

	private IEnumerable<CGeometry> PrivateMeasure(double P_0, double P_1)
	{
		foreach (CInline inline in Content)
		{
			IEnumerable<CGeometry> addings = inline.Measure(P_0, P_1);
			foreach (CGeometry add in addings)
			{
				yield return add;
				P_1 = ((!add.LineBreak) ? (P_1 - add.Width) : P_0);
			}
		}
	}

	public override string AsString()
	{
		using IEnumerator<CInline> enumerator = Content.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return string.Empty;
		}
		string text = enumerator.Current.AsString();
		if (!enumerator.MoveNext())
		{
			return text;
		}
		StringBuilder stringBuilder = new StringBuilder(text);
		do
		{
			stringBuilder.Append(enumerator.Current.AsString());
		}
		while (enumerator.MoveNext());
		return stringBuilder.ToString();
	}
}

