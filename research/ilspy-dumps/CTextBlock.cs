// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CTextBlock
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Avalonia;
using Avalonia.Automation.Peers;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.VisualTree;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.Components.Geometries;

public class CTextBlock : Control, ITextPointerHandleable
{
	private struct SegmentInfo
	{
		public Rect Bounds;

		public int Start;

		public string Text;

		public bool IsAtomic;

		public int Length => Text.Length;
	}

	private static readonly StyledProperty<double> BaseHeightProperty;

	public static readonly StyledProperty<double> LineHeightProperty;

	public static readonly StyledProperty<double> LineSpacingProperty;

	public static readonly StyledProperty<IBrush?> BackgroundProperty;

	public static readonly StyledProperty<IBrush?> ForegroundProperty;

	public static readonly StyledProperty<FontFamily> FontFamilyProperty;

	public static readonly StyledProperty<FontWeight> FontWeightProperty;

	public static readonly StyledProperty<double> FontSizeProperty;

	public static readonly StyledProperty<FontStyle> FontStyleProperty;

	public static readonly StyledProperty<TextVerticalAlignment> TextVerticalAlignmentProperty;

	public static readonly StyledProperty<TextWrapping> TextWrappingProperty;

	public static readonly DirectProperty<CTextBlock, AvaloniaList<CInline>> ContentProperty;

	public static readonly StyledProperty<IBrush?> SelectionBrushProperty;

	public static readonly StyledProperty<TextAlignment> TextAlignmentProperty;

	public static readonly StyledProperty<double> LeftEdgePaddingProperty;

	public static readonly StyledProperty<double> RightEdgePaddingProperty;

	private double _computedBaseHeight;

	private AvaloniaList<CInline> _content;

	private Size _constraint;

	private Size _measured;

	private readonly List<CGeometry> _metries;

	private readonly List<LineInfo> _lines;

	private readonly List<CInlineUIContainer> _containers;

	private CGeometry? _entered;

	private CGeometry? _pressed;

	private string? _text;

	private bool _measureRequested;

	private TextPointer? _beginSelect;

	private readonly List<CGeometry> _intermediates = new List<CGeometry>();

	private TextPointer? _endSelect;

	private readonly List<SegmentInfo> _segments = new List<SegmentInfo>();

	private readonly StringBuilder _sb = new StringBuilder();

	private readonly List<Rect> _selectionRects = new List<Rect>();

	private double RenderScale => base.VisualRoot?.RenderScaling ?? 1.0;

	public IBrush? Background => GetValue(BackgroundProperty);

	public TextWrapping TextWrapping
	{
		get
		{
			return GetValue(TextWrappingProperty);
		}
		set
		{
			SetValue(TextWrappingProperty, value2);
		}
	}

	public TextAlignment TextAlignment
	{
		get
		{
			return GetValue(TextAlignmentProperty);
		}
		set
		{
			SetValue(TextAlignmentProperty, value2);
		}
	}

	public double LineHeight => GetValue(LineHeightProperty);

	public double LineSpacing => GetValue(LineSpacingProperty);

	public double LeftEdgePadding => GetValue(LeftEdgePaddingProperty);

	public double RightEdgePadding => GetValue(RightEdgePaddingProperty);

	[Content]
	public AvaloniaList<CInline> Content
	{
		get
		{
			return _content;
		}
		set
		{
			AvaloniaList<CInline> content = _content;
			if (SetAndRaise(ContentProperty, ref _content, value2))
			{
				content.CollectionChanged -= ContentCollectionChangedd;
				DetachChildren(content);
				AttachChildren(_content);
				_content.CollectionChanged += ContentCollectionChangedd;
			}
		}
	}

	public string Text => _text ?? (_text = string.Join("", Content.Select((CInline c) => c.AsString())));

	public IBrush? SelectionBrush => GetValue(SelectionBrushProperty);

	static CTextBlock()
	{
		BaseHeightProperty = AvaloniaProperty.Register<CTextBlock, double>("BaseHeight", 0.0);
		LineHeightProperty = AvaloniaProperty.Register<CTextBlock, double>("LineHeight", double.NaN);
		LineSpacingProperty = AvaloniaProperty.Register<CTextBlock, double>("LineSpacing", 0.0);
		BackgroundProperty = Border.BackgroundProperty.AddOwner<CTextBlock>();
		ForegroundProperty = TextBlock.ForegroundProperty.AddOwner<CTextBlock>();
		FontFamilyProperty = TextBlock.FontFamilyProperty.AddOwner<CTextBlock>();
		FontWeightProperty = TextBlock.FontWeightProperty.AddOwner<CTextBlock>();
		FontSizeProperty = TextBlock.FontSizeProperty.AddOwner<CTextBlock>();
		FontStyleProperty = TextBlock.FontStyleProperty.AddOwner<CTextBlock>();
		TextVerticalAlignmentProperty = AvaloniaProperty.Register<CTextBlock, TextVerticalAlignment>("TextVerticalAlignment", TextVerticalAlignment.Center, true);
		TextWrappingProperty = AvaloniaProperty.Register<CTextBlock, TextWrapping>("TextWrapping", TextWrapping.Wrap);
		ContentProperty = AvaloniaProperty.RegisterDirect("Content", (CTextBlock cTextBlock) => cTextBlock.Content, delegate(CTextBlock cTextBlock, AvaloniaList<CInline> content)
		{
			cTextBlock.Content = content;
		});
		SelectionBrushProperty = SelectableTextBlock.SelectionBrushProperty.AddOwner<CTextBlock>();
		TextAlignmentProperty = AvaloniaProperty.Register<CTextBlock, TextAlignment>("TextAlignment", TextAlignment.Left);
		LeftEdgePaddingProperty = AvaloniaProperty.Register<CTextBlock, double>("LeftEdgePadding", double.NaN);
		RightEdgePaddingProperty = AvaloniaProperty.Register<CTextBlock, double>("RightEdgePadding", double.NaN);
		Visual.ClipToBoundsProperty.OverrideDefaultValue<CTextBlock>(true);
		Visual.AffectsRender<CTextBlock>(new AvaloniaProperty[5]
		{
			BackgroundProperty,
			TextBlock.ForegroundProperty,
			TextBlock.FontWeightProperty,
			TextBlock.FontSizeProperty,
			TextBlock.FontStyleProperty
		});
	}

	private double PixelFloor(double P_0)
	{
		return Math.Floor(P_0 * RenderScale) / RenderScale;
	}

	private double PixelRound(double P_0)
	{
		return Math.Round(P_0 * RenderScale) / RenderScale;
	}

	private double PixelCeil(double P_0)
	{
		return Math.Ceiling(P_0 * RenderScale) / RenderScale;
	}

	private Rect PixelSnap(Rect P_0)
	{
		return new Rect(PixelFloor(P_0.X), PixelFloor(P_0.Y), PixelCeil(P_0.Width), PixelCeil(P_0.Height));
	}

	private bool CloseDpiAware(double P_0, double P_1)
	{
		double num = 0.1 / RenderScale;
		return double.IsFinite(P_0) && double.IsFinite(P_1) && Math.Abs(P_0 - P_1) <= num;
	}

	public CTextBlock()
	{
		_content = new AvaloniaList<CInline>();
		_content.CollectionChanged += ContentCollectionChangedd;
		_metries = new List<CGeometry>();
		_lines = new List<LineInfo>();
		_containers = new List<CInlineUIContainer>();
	}

	public CTextBlock(string P_0)
		: this()
	{
		_content.Add(new CRun
		{
			Text = P_0
		});
	}

	protected override void OnPointerExited(PointerEventArgs P_0)
	{
		base.OnPointerExited(P_0);
		if (_entered != null)
		{
			_entered.OnMouseLeave?.Invoke(this);
			_entered = null;
		}
	}

	protected override void OnPointerMoved(PointerEventArgs P_0)
	{
		base.OnPointerMoved(P_0);
		Point point = P_0.GetPosition(this);
		if (_entered != null)
		{
			double num = point.X - _entered.Left;
			double num2 = point.Y - _entered.Top;
			if (isEntered(_entered))
			{
				return;
			}
			_entered.OnMouseLeave?.Invoke(this);
			_entered = null;
		}
		foreach (CGeometry metry in _metries)
		{
			if (isEntered(metry))
			{
				metry.OnMouseEnter?.Invoke(this);
				_entered = metry;
				break;
			}
		}
		bool isEntered(CGeometry cGeometry)
		{
			double num3 = point.X - cGeometry.Left;
			double num4 = point.Y - cGeometry.Top;
			return 0.0 <= num3 && num3 <= cGeometry.Width && 0.0 <= num4 && num4 <= cGeometry.Height;
		}
	}

	protected override void OnPointerPressed(PointerPressedEventArgs P_0)
	{
		base.OnPointerPressed(P_0);
		if (!P_0.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
		{
			return;
		}
		Point point = P_0.GetPosition(this);
		foreach (CGeometry metry in _metries)
		{
			if ((metry.OnMousePressed != null || metry.OnMouseReleased != null) && isEntered(metry))
			{
				metry.OnMousePressed?.Invoke(this);
				_pressed = metry;
				P_0.Handled = true;
				break;
			}
		}
		bool isEntered(CGeometry cGeometry)
		{
			double num = point.X - cGeometry.Left;
			double num2 = point.Y - cGeometry.Top;
			return 0.0 <= num && num <= cGeometry.Width && 0.0 <= num2 && num2 <= cGeometry.Height;
		}
	}

	protected override void OnPointerReleased(PointerReleasedEventArgs P_0)
	{
		base.OnPointerReleased(P_0);
		if (_pressed != null && P_0.InitialPressMouseButton == MouseButton.Left)
		{
			P_0.Handled = true;
			_pressed.OnMouseReleased?.Invoke(this);
			Point position = P_0.GetPosition(this);
			double num = position.X - _pressed.Left;
			double num2 = position.Y - _pressed.Top;
			if (0.0 <= num && num <= _pressed.Width && 0.0 <= num2 && num2 <= _pressed.Height)
			{
				_pressed.OnClick?.Invoke(this);
			}
			_pressed = null;
		}
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		switch (P_0.Property.Name)
		{
		case "Content":
		case "FontSize":
		case "FontStyle":
		case "FontWeight":
		case "FontFamily":
		case "TextWrapping":
		case "Bounds":
		case "TextVerticalAlignment":
		case "LineHeight":
		case "LineSpacing":
		case "TextAlignment":
		case "LeftEdgePadding":
		case "RightEdgePadding":
			OnMeasureSourceChanged();
			break;
		case "BaseHeightProperty":
			if (_computedBaseHeight != GetValue(BaseHeightProperty))
			{
				_measureRequested = true;
				InvalidateMeasure();
				InvalidateArrange();
			}
			break;
		}
	}

	public void ObserveBaseHeightOf(CTextBlock P_0)
	{
		if (P_0 != null)
		{
			Bind(BaseHeightProperty, P_0.GetBindingObservable(BaseHeightProperty));
		}
	}

	private void ContentCollectionChangedd(object? sender, NotifyCollectionChangedEventArgs e)
	{
		switch (e.Action)
		{
		case NotifyCollectionChangedAction.Remove:
		case NotifyCollectionChangedAction.Reset:
			if (e.OldItems != null)
			{
				DetachChildren(e.OldItems.Cast<CInline>());
			}
			break;
		case NotifyCollectionChangedAction.Replace:
			if (e.OldItems != null)
			{
				DetachChildren(e.OldItems.Cast<CInline>());
			}
			if (e.NewItems != null)
			{
				AttachChildren(e.NewItems.Cast<CInline>());
			}
			break;
		case NotifyCollectionChangedAction.Add:
			if (e.NewItems != null)
			{
				AttachChildren(e.NewItems.Cast<CInline>());
			}
			break;
		case NotifyCollectionChangedAction.Move:
			break;
		}
	}

	private void AttachChildren(IEnumerable<CInline> P_0)
	{
		foreach (CInline item in P_0)
		{
			base.LogicalChildren.Add(item);
			AttachForVisual(item);
		}
		void AttachForVisual(CInline cInline)
		{
			CInlineUIContainer cInlineUIContainer = null;
			if (cInlineUIContainer != null)
			{
				Control content = cInlineUIContainer.Content;
				if (cInlineUIContainer.Content != null)
				{
					Visual visualParent = cInlineUIContainer.Content.GetVisualParent();
					if (visualParent is CTextBlock cTextBlock)
					{
						if (content != null)
						{
							cTextBlock.VisualChildren.Remove(content);
							cTextBlock.LogicalChildren.Remove(content);
						}
					}
					else if (visualParent != null)
					{
						return;
					}
				}
				if (cInlineUIContainer.Content != null)
				{
					base.VisualChildren.Add(cInlineUIContainer.Content);
					base.LogicalChildren.Add(cInlineUIContainer.Content);
				}
				_containers.Add(cInlineUIContainer);
			}
			else if (cInline is CSpan cSpan)
			{
				foreach (CInline item2 in cSpan.Content)
				{
					AttachForVisual(item2);
				}
			}
		}
	}

	private void DetachChildren(IEnumerable<CInline> P_0)
	{
		foreach (CInline item in P_0)
		{
			base.LogicalChildren.Remove(item);
			DetachForVisual(item);
		}
		void DetachForVisual(CInline cInline)
		{
			CInlineUIContainer cInlineUIContainer = null;
			if (cInlineUIContainer != null)
			{
				if (cInlineUIContainer.Content != null)
				{
					base.VisualChildren.Remove(cInlineUIContainer.Content);
					base.LogicalChildren.Remove(cInlineUIContainer.Content);
				}
				_containers.Remove(cInlineUIContainer);
			}
			else if (cInline is CSpan cSpan)
			{
				foreach (CInline item2 in cSpan.Content)
				{
					DetachForVisual(item2);
				}
			}
		}
	}

	internal void OnMeasureSourceChanged()
	{
		SetValue(BaseHeightProperty, 0.0);
		_measureRequested = true;
		InvalidateMeasure();
		InvalidateArrange();
	}

	private void RepaintRequested()
	{
		InvalidateVisual();
	}

	private static double Clamp(double P_0)
	{
		return (double.IsFinite(P_0) && P_0 >= 0.0) ? P_0 : 0.0;
	}

	private static Size Sanitize(Size P_0)
	{
		return new Size(Clamp(P_0.Width), Clamp(P_0.Height));
	}

	private static double FiniteOrZero(double P_0)
	{
		return double.IsFinite(P_0) ? P_0 : 0.0;
	}

	protected override Size MeasureOverride(Size P_0)
	{
		Size constraint = Sanitize(P_0);
		if (_measured.Width == 0.0 || !CloseDpiAware(constraint.Width, _constraint.Width) || _measureRequested)
		{
			_measureRequested = false;
			_constraint = constraint;
			_measured = Sanitize(UpdateGeometry());
			InvalidateArrange();
		}
		return _measured;
	}

	protected override Size ArrangeOverride(Size P_0)
	{
		Size size = Sanitize(P_0);
		foreach (CInlineUIContainer container in _containers)
		{
			DummyGeometryForControl indicator = container.Indicator;
			if (indicator != null)
			{
				Rect rect = new Rect(Clamp(indicator.Left), Clamp(indicator.Top), Clamp(indicator.Width), Clamp(indicator.Height));
				indicator.Control.Arrange(PixelSnap(rect));
			}
		}
		if (!CloseDpiAware(_constraint.Width, size.Width))
		{
			_constraint = new Size(size.Width, double.PositiveInfinity);
			_measured = Sanitize(UpdateGeometry());
			InvalidateVisual();
		}
		return P_0;
	}

	private Size UpdateGeometry()
	{
		_metries.Clear();
		_lines.Clear();
		double num = (double.IsNaN(LeftEdgePadding) ? (1.0 / RenderScale) : Math.Max(0.0, LeftEdgePadding));
		double num2 = (double.IsNaN(RightEdgePadding) ? (1.0 / RenderScale) : Math.Max(0.0, RightEdgePadding));
		double width = _constraint.Width;
		if (double.IsInfinity(width) && base.Bounds.Width != 0.0)
		{
			width = base.Bounds.Width;
		}
		double num3 = (double.IsInfinity(width) ? double.PositiveInfinity : Math.Max(0.0, width - num - num2));
		double num4 = ((TextWrapping == TextWrapping.NoWrap) ? double.PositiveInfinity : num3);
		double num5 = 0.0;
		double num6 = 0.0;
		double value = GetValue(BaseHeightProperty);
		double lineHeight = LineHeight;
		double num7 = FiniteOrZero(LineSpacing);
		LineInfo lineInfo = null;
		double num8 = num4;
		foreach (CInline item in Content)
		{
			double num9 = ((TextWrapping == TextWrapping.NoWrap) ? double.PositiveInfinity : num8);
			foreach (CGeometry item2 in item.Measure(num4, num9))
			{
				if (lineInfo == null)
				{
					_lines.Add(lineInfo = new LineInfo());
					if (_lines.Count == 1)
					{
						lineInfo.RequestBaseHeight = value;
					}
				}
				if (lineInfo.Add(item2))
				{
					if (!double.IsNaN(lineHeight))
					{
						lineInfo.OverwriteHeight(lineHeight);
					}
					num5 = Math.Max(num5, Clamp(lineInfo.Width));
					num6 += Clamp(lineInfo.Height);
					lineInfo = null;
					num8 = num4;
				}
				else
				{
					num8 = Math.Max(0.0, num8 - Clamp(item2.Width));
				}
			}
		}
		if (lineInfo != null)
		{
			if (!double.IsNaN(lineHeight))
			{
				lineInfo.OverwriteHeight(lineHeight);
			}
			num5 = Math.Max(num5, Clamp(lineInfo.Width));
			num6 += Clamp(lineInfo.Height);
		}
		if (_lines.Count > 0)
		{
			_computedBaseHeight = _lines[0].BaseHeight;
			SetValue(BaseHeightProperty, _lines[0].BaseHeight);
		}
		num6 += num7 * (double)Math.Max(0, _lines.Count - 1);
		double num10 = 0.0;
		foreach (LineInfo line in _lines)
		{
			line.Top = PixelRound(num10);
			double num11 = (double.IsInfinity(width) ? Clamp(line.Width) : Math.Max(0.0, num3));
			double num12 = Clamp(line.Width);
			double num13 = PixelRound(TextAlignment switch
			{
				TextAlignment.Left => num, 
				TextAlignment.Center => num + Clamp((num11 - num12) / 2.0), 
				TextAlignment.Right => num + Clamp(num11 - num12), 
				_ => num, 
			});
			foreach (CGeometry metry in line.Metries)
			{
				double num14 = Clamp(line.Height);
				double num15 = Clamp(metry.Height);
				double num16 = Clamp(line.BaseHeight);
				double num17 = Clamp(metry.BaseHeight);
				TextVerticalAlignment textVerticalAlignment = metry.TextVerticalAlignment;
				if (1 == 0)
				{
				}
				double num18 = textVerticalAlignment switch
				{
					TextVerticalAlignment.Top => num10, 
					TextVerticalAlignment.Center => num10 + Clamp((num14 - num15) / 2.0), 
					TextVerticalAlignment.Bottom => num10 + Clamp(num14 - num15), 
					TextVerticalAlignment.Base => num10 + Clamp(num16 - num17), 
					_ => num10, 
				};
				if (1 == 0)
				{
				}
				double num19 = num18;
				metry.Left = PixelRound(num13);
				metry.Top = PixelRound(num19);
				num13 += Clamp(metry.Width);
				_metries.Add(metry);
				metry.Arranged();
			}
			num10 += Clamp(line.Height) + num7;
		}
		foreach (CGeometry metry2 in _metries)
		{
			metry2.RepaintRequested += RepaintRequested;
		}
		if (_beginSelect != null && _endSelect != null)
		{
			Select(_beginSelect.Index, _endSelect.Index);
		}
		PopulateSegments();
		double num20 = num5;
		if (!double.IsInfinity(width))
		{
			num20 += num + num2;
		}
		double num21 = PixelCeil(num20) + 1.0 / RenderScale;
		double num22 = PixelCeil(num6);
		return Sanitize(new Size(num21, num22));
	}

	private void PopulateSegments()
	{
		_segments.Clear();
		int num = 0;
		foreach (CGeometry metry in _metries)
		{
			string text = GeometryToString(metry);
			_segments.Add(new SegmentInfo
			{
				Bounds = new Rect(metry.Left, metry.Top, metry.Width, metry.Height),
				Start = num,
				Text = text,
				IsAtomic = (metry is ImageGeometry || metry.Owner is CHyperlink)
			});
			num += text.Length;
		}
	}

	public override void Render(DrawingContext P_0)
	{
		if (Background != null)
		{
			P_0.FillRectangle(Background, new Rect(0.0, 0.0, base.Bounds.Width, base.Bounds.Height));
		}
		_selectionRects.Clear();
		if (_beginSelect != null && _endSelect != null)
		{
			TextPointer textPointer;
			TextPointer textPointer2;
			if (!(_beginSelect < _endSelect))
			{
				TextPointer? endSelect = _endSelect;
				TextPointer beginSelect = _beginSelect;
				textPointer = beginSelect;
				textPointer2 = endSelect;
			}
			else
			{
				TextPointer? beginSelect2 = _beginSelect;
				TextPointer beginSelect = _endSelect;
				textPointer = beginSelect;
				textPointer2 = beginSelect2;
			}
			CGeometry geometry = textPointer2.Geometry;
			CGeometry geometry2 = textPointer.Geometry;
			if (geometry != null && geometry2 != null)
			{
				if (geometry == geometry2)
				{
					double num = Clamp(textPointer2.Distance, 0.0, geometry.Width);
					double num2 = Clamp(textPointer.Distance, 0.0, geometry.Width);
					double num3 = geometry.Left + Math.Min(num, num2);
					double num4 = Math.Abs(num2 - num);
					TryRenderRect(geometry, new Rect(num3, geometry.Top, num4, geometry.Height));
				}
				else
				{
					double num5 = Clamp(textPointer2.Distance, 0.0, geometry.Width);
					double num6 = geometry.Width - num5;
					TryRenderRect(geometry, new Rect(geometry.Left + num5, geometry.Top, num6, geometry.Height));
					if (_intermediates != null)
					{
						foreach (CGeometry intermediate in _intermediates)
						{
							if (intermediate != null)
							{
								TryRenderRect(intermediate, new Rect(intermediate.Left, intermediate.Top, intermediate.Width, intermediate.Height));
							}
						}
					}
					double num7 = Clamp(textPointer.Distance, 0.0, geometry2.Width);
					TryRenderRect(geometry2, new Rect(geometry2.Left, geometry2.Top, num7, geometry2.Height));
				}
			}
		}
		foreach (CGeometry metry in _metries)
		{
			metry.Render(P_0);
		}
		if (_selectionRects.Count <= 0)
		{
			return;
		}
		IBrush brush = SelectionBrush ?? Brushes.Cyan;
		if (brush is ISolidColorBrush solidColorBrush)
		{
			SolidColorBrush solidColorBrush2 = new SolidColorBrush(solidColorBrush.Color, 0.5);
			{
				foreach (Rect selectionRect in _selectionRects)
				{
					P_0.FillRectangle(solidColorBrush2, selectionRect);
				}
				return;
			}
		}
		Pen pen = new Pen(brush, 2.0);
		foreach (Rect selectionRect2 in _selectionRects)
		{
			Rect rect = new Rect(selectionRect2.Left - 1.0, selectionRect2.Top - 1.0, selectionRect2.Width + 2.0, selectionRect2.Height + 2.0);
			P_0.DrawRectangle(pen, rect);
		}
		static double Clamp(double num8, double num9, double num10)
		{
			return (num8 < num9) ? num9 : ((num8 > num10) ? num10 : num8);
		}
		void TryRenderRect(CGeometry cGeometry, Rect item)
		{
			if (cGeometry != null && !(item.Width <= 0.0) && !(item.Height <= 0.0))
			{
				_selectionRects.Add(item);
			}
		}
	}

	protected override AutomationPeer OnCreateAutomationPeer()
	{
		return new CTextBlockAutomationPeer(this);
	}

	public void Select(int P_0, int P_1)
	{
		int num = P_0;
		int num2 = P_1;
		for (int i = 0; i < _metries.Count; i++)
		{
			CGeometry cGeometry = _metries[i];
			int caretLength = cGeometry.CaretLength;
			if (P_0 < caretLength || (i == _metries.Count - 1 && P_0 == caretLength))
			{
				_beginSelect = cGeometry.CalcuatePointerFrom(P_0).Wrap(this, num - P_0);
				P_0 = int.MaxValue;
			}
			else
			{
				P_0 -= caretLength;
			}
			if (P_1 < caretLength || (i == _metries.Count - 1 && P_1 == caretLength))
			{
				_endSelect = cGeometry.CalcuatePointerFrom(P_1).Wrap(this, num2 - P_1);
				if (num2 != _endSelect.Index)
				{
					throw new Exception();
				}
				P_1 = int.MaxValue;
			}
			else
			{
				P_1 -= caretLength;
			}
		}
		ComplementIntermediate();
		InvalidateVisual();
	}

	public void Select(TextPointer P_0, TextPointer P_1)
	{
		_beginSelect = P_0;
		_endSelect = P_1;
		ComplementIntermediate();
		InvalidateVisual();
	}

	private void ComplementIntermediate()
	{
		bool flag = false;
		bool flag2 = false;
		_intermediates.Clear();
		foreach (CGeometry metry in _metries)
		{
			bool flag3 = false;
			bool flag4 = false;
			flag |= (flag3 = metry == _beginSelect?.Geometry);
			flag2 |= (flag4 = metry == _endSelect?.Geometry);
			if (flag && flag2)
			{
				break;
			}
			if (!(flag3 || flag4) && (flag || flag2))
			{
				_intermediates.Add(metry);
			}
		}
	}

	public void ClearSelection()
	{
		_beginSelect = null;
		_endSelect = null;
		_intermediates.Clear();
		InvalidateVisual();
	}

	public TextPointer CalcuatePointerFrom(double P_0, double P_1)
	{
		if (P_1 < 0.0)
		{
			return GetBegin();
		}
		int num = 0;
		foreach (LineInfo line in _lines)
		{
			if (P_1 <= line.Top + line.Height)
			{
				foreach (CGeometry metry in line.Metries)
				{
					if (P_0 <= metry.Left + metry.Width)
					{
						TextPointer textPointer = metry.CalcuatePointerFrom(P_0, P_1);
						return textPointer.Wrap(this, num);
					}
					num += metry.CaretLength;
				}
			}
			else
			{
				num += line.Metries.Sum((CGeometry t) => t.CaretLength);
			}
		}
		return GetEnd();
	}

	public TextPointer CalcuatePointerFrom(int P_0)
	{
		if (P_0 < 0)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		int num = 0;
		foreach (CGeometry metry in _metries)
		{
			int caretLength = metry.CaretLength;
			if (P_0 < caretLength)
			{
				TextPointer textPointer = metry.CalcuatePointerFrom(P_0);
				return textPointer.Wrap(this, num);
			}
			P_0 -= caretLength;
			num += caretLength;
		}
		throw new ArgumentOutOfRangeException("index");
	}

	public TextPointer GetBegin()
	{
		if (_metries.Count != 0)
		{
			return _metries[0].GetBegin().Wrap(this, 0);
		}
		return new TextPointer(this, 0);
	}

	public TextPointer GetEnd()
	{
		if (_metries.Count != 0)
		{
			TextPointer end = _metries[_metries.Count - 1].GetEnd();
			int num = _metries.Take(_metries.Count - 1).Sum((CGeometry t) => t.CaretLength);
			return end.Wrap(this, num);
		}
		return new TextPointer(this, 0);
	}

	public string GetSelectedText()
	{
		if (_beginSelect == null || _endSelect == null)
		{
			return string.Empty;
		}
		int num = Math.Min(_beginSelect.Index, _endSelect.Index);
		int num2 = Math.Max(_beginSelect.Index, _endSelect.Index);
		_sb.Clear();
		foreach (SegmentInfo segment in _segments)
		{
			if (segment.Start + segment.Length <= num)
			{
				continue;
			}
			if (segment.Start >= num2)
			{
				break;
			}
			if (segment.IsAtomic)
			{
				if (segment.Start < num2 && segment.Start + segment.Length > num)
				{
					_sb.Append(segment.Text);
				}
				continue;
			}
			int num3 = Math.Max(0, num - segment.Start);
			int num4 = Math.Min(segment.Length, num2 - segment.Start);
			int num5 = num4 - num3;
			if (num5 > 0)
			{
				_sb.Append(segment.Text.AsSpan(num3, num5));
			}
		}
		return _sb.ToString();
	}

	private string GeometryToString(CGeometry P_0)
	{
		if (P_0 is DecoratorGeometry decoratorGeometry)
		{
			if (decoratorGeometry.Targets.Length == 1)
			{
				return GeometryToString(decoratorGeometry.Targets[0]);
			}
			_sb.Clear();
			CGeometry[] targets = decoratorGeometry.Targets;
			foreach (CGeometry cGeometry in targets)
			{
				_sb.Append(GeometryToString(cGeometry));
			}
			return _sb.ToString();
		}
		if (P_0 is ImageGeometry imageGeometry)
		{
			return imageGeometry.ToString();
		}
		if (P_0 is TextLineGeometry textLineGeometry)
		{
			return textLineGeometry.ToString();
		}
		if (P_0 is LineBreakMarkGeometry lineBreakMarkGeometry)
		{
			return lineBreakMarkGeometry.ToString();
		}
		return string.Empty;
	}
}

