// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CInline
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.Components.Geometries;
using RootApp.Client.Avalonia.Markdown.Utils;

[TypeConverter(typeof(StringToRunConverter))]
public abstract class CInline : StyledElement
{
	public static readonly StyledProperty<IBrush?> BackgroundProperty = AvaloniaProperty.Register<CInline, IBrush>("Background", null, true);

	public static readonly StyledProperty<IBrush?> ForegroundProperty = TextBlock.ForegroundProperty.AddOwner<CInline>();

	public static readonly StyledProperty<FontFamily> FontFamilyProperty = TextBlock.FontFamilyProperty.AddOwner<CInline>();

	public static readonly StyledProperty<FontWeight> FontWeightProperty = TextBlock.FontWeightProperty.AddOwner<CInline>();

	public static readonly StyledProperty<FontStretch> FontStretchProperty = TextBlock.FontStretchProperty.AddOwner<CInline>();

	public static readonly StyledProperty<double> FontSizeProperty = TextBlock.FontSizeProperty.AddOwner<CInline>();

	public static readonly StyledProperty<FontStyle> FontStyleProperty = TextBlock.FontStyleProperty.AddOwner<CInline>();

	public static readonly StyledProperty<TextVerticalAlignment> TextVerticalAlignmentProperty = CTextBlock.TextVerticalAlignmentProperty.AddOwner<CInline>();

	public static readonly StyledProperty<bool> IsUnderlineProperty = AvaloniaProperty.Register<CInline, bool>("IsUnderline", false, true);

	public static readonly StyledProperty<bool> IsStrikethroughProperty = AvaloniaProperty.Register<CInline, bool>("IsStrikethrough", false, true);

	[CompilerGenerated]
	private Typeface _003CTypeface_003Ek__BackingField;

	public IBrush? Background
	{
		get
		{
			return GetValue(BackgroundProperty);
		}
		set
		{
			SetValue(BackgroundProperty, value2);
		}
	}

	public IBrush? Foreground => GetValue(ForegroundProperty);

	public FontFamily FontFamily => GetValue(FontFamilyProperty);

	public double FontSize => GetValue(FontSizeProperty);

	public FontStyle FontStyle
	{
		get
		{
			return GetValue(FontStyleProperty);
		}
		set
		{
			SetValue(FontStyleProperty, value2);
		}
	}

	public FontWeight FontWeight
	{
		get
		{
			return GetValue(FontWeightProperty);
		}
		set
		{
			SetValue(FontWeightProperty, value2);
		}
	}

	public FontStretch FontStretch => GetValue(FontStretchProperty);

	public Typeface Typeface
	{
		[CompilerGenerated]
		get
		{
			return _003CTypeface_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CTypeface_003Ek__BackingField = typeface;
		}
	}

	public bool IsUnderline => GetValue(IsUnderlineProperty);

	public bool IsStrikethrough
	{
		get
		{
			return GetValue(IsStrikethroughProperty);
		}
		set
		{
			SetValue(IsStrikethroughProperty, value2);
		}
	}

	public TextVerticalAlignment TextVerticalAlignment
	{
		get
		{
			return GetValue(TextVerticalAlignmentProperty);
		}
		set
		{
			SetValue(TextVerticalAlignmentProperty, value2);
		}
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		switch (P_0.Property.Name)
		{
		default:
			return;
		case "Background":
		case "Foreground":
		case "IsUnderline":
		case "IsStrikethrough":
			RequestRender();
			return;
		case "FontFamily":
		case "FontSize":
		case "FontStyle":
		case "FontWeight":
		case "FontStretch":
			Typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
			break;
		case "TextVerticalAlignment":
			break;
		}
		RequestMeasure();
	}

	protected void RequestMeasure()
	{
		if (base.Parent is CInline cInline)
		{
			cInline.RequestMeasure();
		}
		else if (base.Parent is CTextBlock cTextBlock)
		{
			cTextBlock.OnMeasureSourceChanged();
		}
		else if (base.Parent is Layoutable layoutable)
		{
			layoutable.InvalidateMeasure();
		}
	}

	protected void RequestRender()
	{
		try
		{
			if (base.Parent is CInline cInline)
			{
				cInline.RequestRender();
			}
			else if (base.Parent is Layoutable layoutable)
			{
				layoutable.InvalidateVisual();
			}
		}
		catch
		{
		}
	}

	internal IEnumerable<CGeometry> Measure(double P_0, double P_1)
	{
		Typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
		ApplyStyling();
		return MeasureOverride(P_0, P_1);
	}

	protected abstract IEnumerable<CGeometry> MeasureOverride(double P_0, double P_1);

	public abstract string AsString();
}

