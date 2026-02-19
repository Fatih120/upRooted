// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FComboBox_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FComboBox_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/ComboBox.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	if (styles2.Resources is ResourceDictionary resourceDictionary)
	{
		resourceDictionary.EnsureCapacity(resourceDictionary.Count + 5);
	}
	styles2.Resources.Add("ComboBoxTopHeaderMargin", new Thickness(0.0, 0.0, 0.0, 4.0));
	styles2.Resources.Add("ComboBoxPopupMaxNumberOfItems", 15);
	styles2.Resources.Add("ComboBoxPopupMaxNumberOfItemsThatCanBeShownOnOneSide", 7);
	styles2.Resources.Add("ComboBoxEditableTextPadding", new Thickness(11.0, 5.0, 32.0, 6.0));
	styles2.Resources.Add("ComboBoxMinHeight", 32.0);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	Style style3 = style2;
	style3.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox");
	Setter setter = new Setter();
	setter.Property = TemplatedControl.PaddingProperty;
	setter.Value = new Thickness(16.0, 8.0, 16.0, 8.0);
	style3.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = InputElement.CursorProperty;
	setter2.Value = new Cursor(StandardCursorType.Hand);
	style3.Add(setter2);
	Setter setter3 = new Setter();
	setter3.Property = Control.FocusAdornerProperty;
	setter3.Value = null;
	style3.Add(setter3);
	Setter setter4 = new Setter();
	setter4.Property = ComboBox.MaxDropDownHeightProperty;
	setter4.Value = 504.0;
	style3.Add(setter4);
	Setter setter6;
	Setter setter5 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter7 = setter6;
	setter7.Property = TemplatedControl.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter7.Value = value;
	context.PopParent();
	style3.Add(setter5);
	Setter setter8 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter9 = setter6;
	setter9.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Input");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value2 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter9.Value = value2;
	context.PopParent();
	style3.Add(setter8);
	Setter setter10 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter11 = setter6;
	setter11.Property = TemplatedControl.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("ComboBoxBorderBrush");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value3 = dynamicResourceExtension3.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter11.Value = value3;
	context.PopParent();
	style3.Add(setter10);
	Setter setter12 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter13 = setter6;
	setter13.Property = TemplatedControl.BorderThicknessProperty;
	DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("ComboBoxBorderThemeThickness");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value4 = dynamicResourceExtension4.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter13.Value = value4;
	context.PopParent();
	style3.Add(setter12);
	Setter setter14 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter15 = setter6;
	setter15.Property = TemplatedControl.CornerRadiusProperty;
	DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("ControlCornerRadius");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value5 = dynamicResourceExtension5.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter15.Value = value5;
	context.PopParent();
	style3.Add(setter14);
	Setter setter16 = new Setter();
	setter16.Property = ScrollViewer.HorizontalScrollBarVisibilityProperty;
	setter16.Value = ScrollBarVisibility.Disabled;
	style3.Add(setter16);
	Setter setter17 = new Setter();
	setter17.Property = ScrollViewer.VerticalScrollBarVisibilityProperty;
	setter17.Value = ScrollBarVisibility.Auto;
	style3.Add(setter17);
	Setter setter18 = new Setter();
	setter18.Property = ComboBox.HorizontalContentAlignmentProperty;
	setter18.Value = HorizontalAlignment.Stretch;
	style3.Add(setter18);
	Setter setter19 = new Setter();
	setter19.Property = ComboBox.VerticalContentAlignmentProperty;
	setter19.Value = VerticalAlignment.Center;
	style3.Add(setter19);
	Setter setter20 = new Setter();
	setter20.Property = Layoutable.HorizontalAlignmentProperty;
	setter20.Value = HorizontalAlignment.Stretch;
	style3.Add(setter20);
	Setter setter21 = new Setter();
	setter21.Property = Layoutable.VerticalAlignmentProperty;
	setter21.Value = VerticalAlignment.Top;
	style3.Add(setter21);
	Setter setter22 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter23 = setter6;
	setter23.Property = ComboBox.PlaceholderForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("ComboBoxPlaceHolderForeground");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value6 = dynamicResourceExtension6.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter23.Value = value6;
	context.PopParent();
	style3.Add(setter22);
	Setter setter24 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter25 = setter6;
	setter25.Property = TemplatedControl.FontFamilyProperty;
	StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	object? value7 = staticResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter25.Value = value7;
	context.PopParent();
	style3.Add(setter24);
	Setter setter26 = new Setter();
	setter26.Property = TemplatedControl.FontWeightProperty;
	setter26.Value = (FontWeight)450;
	style3.Add(setter26);
	Setter setter27 = new Setter();
	setter27.Property = TemplatedControl.FontSizeProperty;
	setter27.Value = 14.0;
	style3.Add(setter27);
	Setter setter28 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter29 = setter6;
	setter29.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate;
	ControlTemplate value8 = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_32.Build_1), context);
	context.PopParent();
	setter29.Value = value8;
	context.PopParent();
	style3.Add(setter28);
	context.PopParent();
	styles2.Add(style);
	Style style4 = new Style();
	style4.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":pointerover")
		.Template()
		.OfType(typeof(Viewbox))
		.Name("PART_Viewbox");
	Setter setter30 = new Setter();
	setter30.Property = Visual.OpacityProperty;
	setter30.Value = 1.0;
	style4.Add(setter30);
	styles2.Add(style4);
	Style style5 = (style2 = new Style());
	context.PushParent(style2);
	Style style6 = style2;
	style6.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Template()
		.OfType(typeof(TextBlock))
		.Name("PlaceholderTextBlock");
	Setter setter31 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter32 = setter6;
	setter32.Property = TextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("ComboBoxPlaceHolderForeground");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value9 = dynamicResourceExtension7.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter32.Value = value9;
	context.PopParent();
	style6.Add(setter31);
	context.PopParent();
	styles2.Add(style5);
	Style style7 = new Style();
	style7.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Template()
		.OfType(typeof(Border))
		.Name("HighlightBackground");
	Setter setter33 = new Setter();
	setter33.Property = Visual.IsVisibleProperty;
	setter33.Value = false;
	style7.Add(setter33);
	styles2.Add(style7);
	Style style8 = (style2 = new Style());
	context.PushParent(style2);
	Style style9 = style2;
	style9.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":disabled")
		.Template()
		.OfType(typeof(Border))
		.Name("Background");
	Setter setter34 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter35 = setter6;
	setter35.Property = Border.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("ComboBoxBackgroundDisabled");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value10 = dynamicResourceExtension8.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter35.Value = value10;
	context.PopParent();
	style9.Add(setter34);
	Setter setter36 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter37 = setter6;
	setter37.Property = Border.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("ComboBoxBorderBrushDisabled");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value11 = dynamicResourceExtension9.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter37.Value = value11;
	context.PopParent();
	style9.Add(setter36);
	context.PopParent();
	styles2.Add(style8);
	Style style10 = (style2 = new Style());
	context.PushParent(style2);
	Style style11 = style2;
	style11.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":disabled")
		.Template()
		.OfType(typeof(ContentPresenter))
		.Name("HeaderContentPresenter");
	Setter setter38 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter39 = setter6;
	setter39.Property = TextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("ComboBoxForegroundDisabled");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value12 = dynamicResourceExtension10.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter39.Value = value12;
	context.PopParent();
	style11.Add(setter38);
	context.PopParent();
	styles2.Add(style10);
	Style style12 = (style2 = new Style());
	context.PushParent(style2);
	Style style13 = style2;
	style13.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":disabled")
		.Template()
		.OfType(typeof(ContentControl))
		.Name("ContentPresenter");
	Setter setter40 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter41 = setter6;
	setter41.Property = TextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("ComboBoxForegroundDisabled");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value13 = dynamicResourceExtension11.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter41.Value = value13;
	context.PopParent();
	style13.Add(setter40);
	context.PopParent();
	styles2.Add(style12);
	Style style14 = (style2 = new Style());
	context.PushParent(style2);
	Style style15 = style2;
	style15.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":disabled")
		.Template()
		.OfType(typeof(TextBlock))
		.Name("PlaceholderTextBlock");
	Setter setter42 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter43 = setter6;
	setter43.Property = TextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("ComboBoxForegroundDisabled");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value14 = dynamicResourceExtension12.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter43.Value = value14;
	context.PopParent();
	style15.Add(setter42);
	context.PopParent();
	styles2.Add(style14);
	Style style16 = (style2 = new Style());
	context.PushParent(style2);
	Style style17 = style2;
	style17.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":disabled")
		.Template()
		.OfType(typeof(Path))
		.Name("DropDownGlyph");
	Setter setter44 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter45 = setter6;
	setter45.Property = Shape.FillProperty;
	DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("ComboBoxDropDownGlyphForegroundDisabled");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value15 = dynamicResourceExtension13.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter45.Value = value15;
	context.PopParent();
	style17.Add(setter44);
	context.PopParent();
	styles2.Add(style16);
	Style style18 = (style2 = new Style());
	context.PushParent(style2);
	Style style19 = style2;
	style19.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":focus-visible")
		.Template()
		.OfType(typeof(Border))
		.Name("HighlightBackground");
	Setter setter46 = new Setter();
	setter46.Property = Visual.IsVisibleProperty;
	setter46.Value = true;
	style19.Add(setter46);
	Setter setter47 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter48 = setter6;
	setter48.Property = Border.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("ComboBoxBackgroundBorderBrushFocused");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value16 = dynamicResourceExtension14.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter48.Value = value16;
	context.PopParent();
	style19.Add(setter47);
	context.PopParent();
	styles2.Add(style18);
	Style style20 = (style2 = new Style());
	context.PushParent(style2);
	Style style21 = style2;
	style21.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":focus-visible")
		.Template()
		.OfType(typeof(ContentControl))
		.Name("ContentPresenter");
	Setter setter49 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter50 = setter6;
	setter50.Property = TextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("ComboBoxForegroundFocused");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value17 = dynamicResourceExtension15.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter50.Value = value17;
	context.PopParent();
	style21.Add(setter49);
	context.PopParent();
	styles2.Add(style20);
	Style style22 = (style2 = new Style());
	context.PushParent(style2);
	Style style23 = style2;
	style23.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":focus-visible")
		.Template()
		.OfType(typeof(TextBlock))
		.Name("PlaceholderTextBlock");
	Setter setter51 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter52 = setter6;
	setter52.Property = TextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("ComboBoxForegroundFocused");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value18 = dynamicResourceExtension16.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter52.Value = value18;
	context.PopParent();
	style23.Add(setter51);
	context.PopParent();
	styles2.Add(style22);
	Style style24 = (style2 = new Style());
	context.PushParent(style2);
	Style style25 = style2;
	style25.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":focus-visible")
		.Template()
		.OfType(typeof(Path))
		.Name("DropDownGlyph");
	Setter setter53 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter54 = setter6;
	setter54.Property = Shape.FillProperty;
	DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("ComboBoxDropDownGlyphForegroundFocused");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value19 = dynamicResourceExtension17.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter54.Value = value19;
	context.PopParent();
	style25.Add(setter53);
	context.PopParent();
	styles2.Add(style24);
	Style style26 = (style2 = new Style());
	context.PushParent(style2);
	Style style27 = style2;
	style27.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":focused")
		.Class(":pressed")
		.Template()
		.OfType(typeof(ContentControl))
		.Name("ContentPresenter");
	Setter setter55 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter56 = setter6;
	setter56.Property = TextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("ComboBoxForegroundFocusedPressed");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value20 = dynamicResourceExtension18.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter56.Value = value20;
	context.PopParent();
	style27.Add(setter55);
	context.PopParent();
	styles2.Add(style26);
	Style style28 = (style2 = new Style());
	context.PushParent(style2);
	Style style29 = style2;
	style29.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":focused")
		.Class(":pressed")
		.Template()
		.OfType(typeof(TextBlock))
		.Name("PlaceholderTextBlock");
	Setter setter57 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter58 = setter6;
	setter58.Property = TextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension19 = new DynamicResourceExtension("ComboBoxPlaceHolderForegroundFocusedPressed");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value21 = dynamicResourceExtension19.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter58.Value = value21;
	context.PopParent();
	style29.Add(setter57);
	context.PopParent();
	styles2.Add(style28);
	Style style30 = (style2 = new Style());
	context.PushParent(style2);
	Style style31 = style2;
	style31.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":focused")
		.Class(":pressed")
		.Template()
		.OfType(typeof(Path))
		.Name("DropDownGlyph");
	Setter setter59 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter60 = setter6;
	setter60.Property = Shape.FillProperty;
	DynamicResourceExtension dynamicResourceExtension20 = new DynamicResourceExtension("ComboBoxDropDownGlyphForegroundFocusedPressed");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value22 = dynamicResourceExtension20.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter60.Value = value22;
	context.PopParent();
	style31.Add(setter59);
	context.PopParent();
	styles2.Add(style30);
	Style style32 = (style2 = new Style());
	context.PushParent(style2);
	Style style33 = style2;
	style33.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("RootComboBox").Class(":error")
		.Template()
		.OfType(typeof(Border))
		.Name("Background");
	Setter setter61 = (setter6 = new Setter());
	context.PushParent(setter6);
	Setter setter62 = setter6;
	setter62.Property = Border.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension21 = new DynamicResourceExtension("SystemControlErrorTextForegroundBrush");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value23 = dynamicResourceExtension21.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter62.Value = value23;
	context.PopParent();
	style33.Add(setter61);
	context.PopParent();
	styles2.Add(style32);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

