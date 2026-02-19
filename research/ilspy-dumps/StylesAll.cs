// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FBorderButton_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FBorderButton_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/BorderButton.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style = new Style();
	style.Selector = ((Selector?)null).OfType(typeof(Button)).Class("ListBorderButton");
	Setter setter = new Setter();
	setter.Property = TemplatedControl.BorderThicknessProperty;
	setter.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = TemplatedControl.PaddingProperty;
	setter2.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style.Add(setter2);
	Setter setter3 = new Setter();
	setter3.Property = Layoutable.HorizontalAlignmentProperty;
	setter3.Value = HorizontalAlignment.Stretch;
	style.Add(setter3);
	Setter setter4 = new Setter();
	setter4.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter4.Value = HorizontalAlignment.Stretch;
	style.Add(setter4);
	Setter setter5 = new Setter();
	setter5.Property = Layoutable.VerticalAlignmentProperty;
	setter5.Value = VerticalAlignment.Stretch;
	style.Add(setter5);
	Setter setter6 = new Setter();
	setter6.Property = ContentControl.VerticalContentAlignmentProperty;
	setter6.Value = VerticalAlignment.Stretch;
	style.Add(setter6);
	Setter setter7 = new Setter();
	setter7.Property = TemplatedControl.TemplateProperty;
	setter7.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_29.Build_1), context)
	};
	style.Add(setter7);
	styles2.Add(style);
	Style style3;
	Style style2 = (style3 = new Style());
	context.PushParent(style3);
	Style style4 = style3;
	style4.Selector = ((Selector?)null).OfType(typeof(Button)).Class("ListBorderButton").Class(":pointerover");
	Setter setter8 = new Setter();
	setter8.Property = InputElement.CursorProperty;
	setter8.Value = new Cursor(StandardCursorType.Hand);
	style4.Add(setter8);
	Style style5 = (style3 = new Style());
	context.PushParent(style3);
	Style style6 = style3;
	style6.Selector = ((Selector?)null).Nesting().Template().OfType(typeof(Border))
		.Name("PART_Border");
	Setter setter10;
	Setter setter9 = (setter10 = new Setter());
	context.PushParent(setter10);
	Setter setter11 = setter10;
	setter11.Property = Border.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter11.Value = value;
	context.PopParent();
	style6.Add(setter9);
	context.PopParent();
	style4.Add(style5);
	context.PopParent();
	styles2.Add(style2);
	Style style7 = (style3 = new Style());
	context.PushParent(style3);
	Style style8 = style3;
	style8.Selector = ((Selector?)null).OfType(typeof(Button)).Class("ListBorderButton").Class(":pressed");
	Setter setter12 = (setter10 = new Setter());
	context.PushParent(setter10);
	Setter setter13 = setter10;
	setter13.Property = Visual.RenderTransformProperty;
	setter13.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
	context.PopParent();
	style8.Add(setter12);
	context.PopParent();
	styles2.Add(style7);
	Style style9 = new Style();
	style9.Selector = ((Selector?)null).OfType(typeof(Button)).Class("BorderButton");
	Setter setter14 = new Setter();
	setter14.Property = TemplatedControl.PaddingProperty;
	setter14.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style9.Add(setter14);
	Setter setter15 = new Setter();
	setter15.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter15.Value = HorizontalAlignment.Center;
	style9.Add(setter15);
	Setter setter16 = new Setter();
	setter16.Property = ContentControl.VerticalContentAlignmentProperty;
	setter16.Value = VerticalAlignment.Center;
	style9.Add(setter16);
	Setter setter17 = new Setter();
	setter17.Property = TemplatedControl.TemplateProperty;
	setter17.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_29.Build_2), context)
	};
	style9.Add(setter17);
	styles2.Add(style9);
	Style style10 = new Style();
	style10.Selector = ((Selector?)null).OfType(typeof(Button)).Class("BorderButton").Class(":pointerover");
	Setter setter18 = new Setter();
	setter18.Property = Visual.OpacityProperty;
	setter18.Value = 0.7;
	style10.Add(setter18);
	Setter setter19 = new Setter();
	setter19.Property = InputElement.CursorProperty;
	setter19.Value = new Cursor(StandardCursorType.Hand);
	style10.Add(setter19);
	styles2.Add(style10);
	Style style11 = (style3 = new Style());
	context.PushParent(style3);
	Style style12 = style3;
	style12.Selector = ((Selector?)null).OfType(typeof(Button)).Class("BorderButton").Class(":pressed");
	Setter setter20 = new Setter();
	setter20.Property = Visual.OpacityProperty;
	setter20.Value = 0.5;
	style12.Add(setter20);
	Setter setter21 = (setter10 = new Setter());
	context.PushParent(setter10);
	Setter setter22 = setter10;
	setter22.Property = Visual.RenderTransformProperty;
	setter22.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
	context.PopParent();
	style12.Add(setter21);
	context.PopParent();
	styles2.Add(style11);
	Style style13 = new Style();
	style13.Selector = ((Selector?)null).OfType(typeof(Button)).Class("BasicButton");
	Setter setter23 = new Setter();
	setter23.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter23.Value = HorizontalAlignment.Center;
	style13.Add(setter23);
	Setter setter24 = new Setter();
	setter24.Property = ContentControl.VerticalContentAlignmentProperty;
	setter24.Value = VerticalAlignment.Center;
	style13.Add(setter24);
	Setter setter25 = new Setter();
	setter25.Property = TemplatedControl.PaddingProperty;
	setter25.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style13.Add(setter25);
	Setter setter26 = new Setter();
	setter26.Property = InputElement.FocusableProperty;
	setter26.Value = false;
	style13.Add(setter26);
	Setter setter27 = new Setter();
	setter27.Property = TemplatedControl.TemplateProperty;
	setter27.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_29.Build_3), context)
	};
	style13.Add(setter27);
	styles2.Add(style13);
	Style style14 = new Style();
	style14.Selector = ((Selector?)null).OfType(typeof(Button)).Class("BasicButton").Class(":pointerover");
	Setter setter28 = new Setter();
	setter28.Property = Visual.OpacityProperty;
	setter28.Value = 0.7;
	style14.Add(setter28);
	Setter setter29 = new Setter();
	setter29.Property = InputElement.CursorProperty;
	setter29.Value = new Cursor(StandardCursorType.Hand);
	style14.Add(setter29);
	styles2.Add(style14);
	Style style15 = (style3 = new Style());
	context.PushParent(style3);
	Style style16 = style3;
	style16.Selector = ((Selector?)null).OfType(typeof(Button)).Class("BasicButton").Class(":pressed");
	Setter setter30 = new Setter();
	setter30.Property = Visual.OpacityProperty;
	setter30.Value = 0.5;
	style16.Add(setter30);
	Setter setter31 = (setter10 = new Setter());
	context.PushParent(setter10);
	Setter setter32 = setter10;
	setter32.Property = Visual.RenderTransformProperty;
	setter32.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
	context.PopParent();
	style16.Add(setter31);
	context.PopParent();
	styles2.Add(style15);
	Style style17 = new Style();
	style17.Selector = ((Selector?)null).OfType(typeof(Button)).Class("BasicButtonNeverOpaque");
	Setter setter33 = new Setter();
	setter33.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter33.Value = HorizontalAlignment.Center;
	style17.Add(setter33);
	Setter setter34 = new Setter();
	setter34.Property = ContentControl.VerticalContentAlignmentProperty;
	setter34.Value = VerticalAlignment.Center;
	style17.Add(setter34);
	Setter setter35 = new Setter();
	setter35.Property = TemplatedControl.PaddingProperty;
	setter35.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style17.Add(setter35);
	Setter setter36 = new Setter();
	setter36.Property = InputElement.FocusableProperty;
	setter36.Value = false;
	style17.Add(setter36);
	Setter setter37 = new Setter();
	setter37.Property = TemplatedControl.TemplateProperty;
	setter37.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_29.Build_4), context)
	};
	style17.Add(setter37);
	styles2.Add(style17);
	Style style18 = new Style();
	style18.Selector = ((Selector?)null).OfType(typeof(Button)).Class("BasicButtonNeverOpaque").Class(":pointerover");
	Setter setter38 = new Setter();
	setter38.Property = Visual.OpacityProperty;
	setter38.Value = 0.7;
	style18.Add(setter38);
	Setter setter39 = new Setter();
	setter39.Property = InputElement.CursorProperty;
	setter39.Value = new Cursor(StandardCursorType.Hand);
	style18.Add(setter39);
	styles2.Add(style18);
	Style style19 = (style3 = new Style());
	context.PushParent(style3);
	Style style20 = style3;
	style20.Selector = ((Selector?)null).OfType(typeof(Button)).Class("BasicButtonNeverOpaque").Class(":pressed");
	Setter setter40 = new Setter();
	setter40.Property = Visual.OpacityProperty;
	setter40.Value = 0.5;
	style20.Add(setter40);
	Setter setter41 = (setter10 = new Setter());
	context.PushParent(setter10);
	Setter setter42 = setter10;
	setter42.Property = Visual.RenderTransformProperty;
	setter42.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
	context.PopParent();
	style20.Add(setter41);
	context.PopParent();
	styles2.Add(style19);
	Style style21 = new Style();
	style21.Selector = ((Selector?)null).OfType(typeof(Button)).Class("BasicButtonNeverOpaque").Class(":disabled");
	Setter setter43 = new Setter();
	setter43.Property = Visual.OpacityProperty;
	setter43.Value = 1.0;
	style21.Add(setter43);
	styles2.Add(style21);
	Style style22 = new Style();
	style22.Selector = ((Selector?)null).OfType(typeof(Button)).Class("MenuBorderButton");
	Setter setter44 = new Setter();
	setter44.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter44.Value = HorizontalAlignment.Center;
	style22.Add(setter44);
	Setter setter45 = new Setter();
	setter45.Property = ContentControl.VerticalContentAlignmentProperty;
	setter45.Value = VerticalAlignment.Center;
	style22.Add(setter45);
	Setter setter46 = new Setter();
	setter46.Property = TemplatedControl.BackgroundProperty;
	setter46.Value = new ImmutableSolidColorBrush(16777215u);
	style22.Add(setter46);
	Setter setter47 = new Setter();
	setter47.Property = TemplatedControl.BorderBrushProperty;
	setter47.Value = new ImmutableSolidColorBrush(16777215u);
	style22.Add(setter47);
	Setter setter48 = new Setter();
	setter48.Property = TemplatedControl.TemplateProperty;
	setter48.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_29.Build_5), context)
	};
	style22.Add(setter48);
	styles2.Add(style22);
	Style style23 = (style3 = new Style());
	context.PushParent(style3);
	Style style24 = style3;
	style24.Selector = ((Selector?)null).OfType(typeof(Button)).Class("MenuBorderButton").Class(":pointerover");
	Setter setter49 = (setter10 = new Setter());
	context.PushParent(setter10);
	Setter setter50 = setter10;
	setter50.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value2 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter50.Value = value2;
	context.PopParent();
	style24.Add(setter49);
	Setter setter51 = new Setter();
	setter51.Property = InputElement.CursorProperty;
	setter51.Value = new Cursor(StandardCursorType.Hand);
	style24.Add(setter51);
	context.PopParent();
	styles2.Add(style23);
	Style style25 = (style3 = new Style());
	context.PushParent(style3);
	Style style26 = style3;
	style26.Selector = ((Selector?)null).OfType(typeof(Button)).Class("MenuBorderButton").Class(":pressed");
	Setter setter52 = (setter10 = new Setter());
	context.PushParent(setter10);
	Setter setter53 = setter10;
	setter53.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value3 = dynamicResourceExtension3.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter53.Value = value3;
	context.PopParent();
	style26.Add(setter52);
	Setter setter54 = (setter10 = new Setter());
	context.PushParent(setter10);
	Setter setter55 = setter10;
	setter55.Property = Visual.RenderTransformProperty;
	setter55.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
	context.PopParent();
	style26.Add(setter54);
	context.PopParent();
	styles2.Add(style25);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Immutable;
using Avalonia.Styling;

public static void Populate_003A_002FResources_002FStyles_002FBorderlessTextbox_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FBorderlessTextbox_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/BorderlessTextbox.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Style style = new Style();
	style.Selector = ((Selector?)null).OfType(typeof(TextBox)).Class("BorderlessTextBox");
	Setter setter = new Setter();
	setter.Property = TemplatedControl.BackgroundProperty;
	setter.Value = new ImmutableSolidColorBrush(16777215u);
	style.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = TemplatedControl.BorderThicknessProperty;
	setter2.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style.Add(setter2);
	Style style2 = new Style();
	style2.Selector = ((Selector?)null).Nesting().Template().OfType(typeof(Border))
		.Name("PART_BorderElement");
	Setter setter3 = new Setter();
	setter3.Property = Border.BackgroundProperty;
	setter3.Value = new ImmutableSolidColorBrush(16777215u);
	style2.Add(setter3);
	Setter setter4 = new Setter();
	setter4.Property = Border.BorderBrushProperty;
	setter4.Value = new ImmutableSolidColorBrush(16777215u);
	style2.Add(setter4);
	Setter setter5 = new Setter();
	setter5.Property = Border.BorderThicknessProperty;
	setter5.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style2.Add(setter5);
	style.Add(style2);
	P_1.Add(style);
	if (P_1 is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
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
using RootApp.Client.Avalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FCheckBox_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FCheckBox_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/CheckBox.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	Style style3 = style2;
	style3.Selector = ((Selector?)null).OfType(typeof(CheckBox)).Class("ToggleSwitch");
	Setter setter = new Setter();
	setter.Property = InputElement.CursorProperty;
	setter.Value = new Cursor(StandardCursorType.Hand);
	style3.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = TemplatedControl.CornerRadiusProperty;
	setter2.Value = new CornerRadius(12.0, 12.0, 12.0, 12.0);
	style3.Add(setter2);
	Setter setter4;
	Setter setter3 = (setter4 = new Setter());
	context.PushParent(setter4);
	Setter setter5 = setter4;
	setter5.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("Muted");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter5.Value = value;
	context.PopParent();
	style3.Add(setter3);
	Setter setter6 = new Setter();
	setter6.Property = Layoutable.WidthProperty;
	setter6.Value = 45.0;
	style3.Add(setter6);
	Setter setter7 = new Setter();
	setter7.Property = Layoutable.HeightProperty;
	setter7.Value = 25.0;
	style3.Add(setter7);
	Setter setter8 = (setter4 = new Setter());
	context.PushParent(setter4);
	Setter setter9 = setter4;
	setter9.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate;
	ControlTemplate value2 = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_30.Build_1), context);
	context.PopParent();
	setter9.Value = value2;
	context.PopParent();
	style3.Add(setter8);
	Style style4 = (style2 = new Style());
	context.PushParent(style2);
	Style style5 = style2;
	style5.Selector = ((Selector?)null).Nesting().Class(":checked");
	Setter setter10 = (setter4 = new Setter());
	context.PushParent(setter4);
	Setter setter11 = setter4;
	setter11.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BrandPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value3 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter11.Value = value3;
	context.PopParent();
	style5.Add(setter10);
	Style style6 = (style2 = new Style());
	context.PushParent(style2);
	Style style7 = style2;
	style7.Selector = ((Selector?)null).Nesting().Template().OfType(typeof(Ellipse))
		.Name("PART_Ellipse");
	Setter setter12 = (setter4 = new Setter());
	context.PushParent(setter4);
	Setter setter13 = setter4;
	setter13.Property = Shape.StrokeProperty;
	DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("BrandPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value4 = dynamicResourceExtension3.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter13.Value = value4;
	context.PopParent();
	style7.Add(setter12);
	Setter setter14 = new Setter();
	setter14.Property = Layoutable.HorizontalAlignmentProperty;
	setter14.Value = HorizontalAlignment.Right;
	style7.Add(setter14);
	context.PopParent();
	style5.Add(style6);
	context.PopParent();
	style3.Add(style4);
	Style style8 = new Style();
	style8.Selector = ((Selector?)null).Nesting().Class(":disabled");
	Setter setter15 = new Setter();
	setter15.Property = InputElement.CursorProperty;
	setter15.Value = null;
	style8.Add(setter15);
	Setter setter16 = new Setter();
	setter16.Property = Visual.OpacityProperty;
	setter16.Value = 0.3;
	style8.Add(setter16);
	style3.Add(style8);
	context.PopParent();
	styles2.Add(style);
	Style style9 = (style2 = new Style());
	context.PushParent(style2);
	Style style10 = style2;
	style10.Selector = ((Selector?)null).OfType(typeof(RootSvgCheckBox));
	Setter setter17 = (setter4 = new Setter());
	context.PushParent(setter4);
	Setter setter18 = setter4;
	setter18.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value5 = dynamicResourceExtension4.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter18.Value = value5;
	context.PopParent();
	style10.Add(setter17);
	Setter setter19 = new Setter();
	setter19.Property = TemplatedControl.CornerRadiusProperty;
	setter19.Value = new CornerRadius(5.0, 5.0, 5.0, 5.0);
	style10.Add(setter19);
	Setter setter20 = new Setter();
	setter20.Property = TemplatedControl.BorderThicknessProperty;
	setter20.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style10.Add(setter20);
	Setter setter21 = new Setter();
	setter21.Property = RootSvgCheckBox.SvgOpacityProperty;
	setter21.Value = 0.6;
	style10.Add(setter21);
	Setter setter22 = new Setter();
	setter22.Property = RootSvgCheckBox.SvgBorderOpacityProperty;
	setter22.Value = 0.0;
	style10.Add(setter22);
	Setter setter23 = new Setter();
	setter23.Property = InputElement.CursorProperty;
	setter23.Value = new Cursor(StandardCursorType.Hand);
	style10.Add(setter23);
	Setter setter24 = new Setter();
	setter24.Property = Visual.OpacityProperty;
	setter24.Value = 1.0;
	style10.Add(setter24);
	Setter setter25 = new Setter();
	setter25.Property = TemplatedControl.TemplateProperty;
	setter25.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_30.Build_2), context)
	};
	style10.Add(setter25);
	Style style11 = new Style();
	style11.Selector = ((Selector?)null).Nesting().Class(":checked");
	Setter setter26 = new Setter();
	setter26.Property = RootSvgCheckBox.SvgOpacityProperty;
	setter26.Value = 1.0;
	style11.Add(setter26);
	Setter setter27 = new Setter();
	setter27.Property = RootSvgCheckBox.SvgBorderOpacityProperty;
	setter27.Value = 0.7;
	style11.Add(setter27);
	style10.Add(style11);
	Style style12 = new Style();
	style12.Selector = ((Selector?)null).Nesting().Class(":pointerover");
	Setter setter28 = new Setter();
	setter28.Property = RootSvgCheckBox.SvgBorderOpacityProperty;
	setter28.Value = 0.7;
	style12.Add(setter28);
	style10.Add(style12);
	Style style13 = (style2 = new Style());
	context.PushParent(style2);
	Style style14 = style2;
	style14.Selector = ((Selector?)null).Nesting().Class(":pressed");
	Setter setter29 = (setter4 = new Setter());
	context.PushParent(setter4);
	Setter setter30 = setter4;
	setter30.Property = Visual.RenderTransformProperty;
	setter30.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
	context.PopParent();
	style14.Add(setter29);
	context.PopParent();
	style10.Add(style13);
	context.PopParent();
	styles2.Add(style9);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FColorPicker_002FRootColorPicker_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FColorPicker_002FRootColorPicker_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/ColorPicker/RootColorPicker.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	ResourceDictionary resourceDictionary;
	ResourceDictionary resources = (resourceDictionary = new ResourceDictionary());
	context.PushParent(resourceDictionary);
	if (resourceDictionary is ResourceDictionary resourceDictionary2)
	{
		resourceDictionary2.EnsureCapacity(resourceDictionary2.Count + 46);
	}
	resourceDictionary.AddDeferred(typeof(ColorSpectrum), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_1), context));
	resourceDictionary.AddDeferred("ColorControlCheckeredBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_3), context));
	resourceDictionary.AddDeferred("ColorControlLightSelectorBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_4), context));
	resourceDictionary.AddDeferred("ColorControlDarkSelectorBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_5), context));
	resourceDictionary.AddDeferred("ColorViewContentBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_6), context));
	resourceDictionary.AddDeferred("ColorViewContentBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_7), context));
	resourceDictionary.AddDeferred("ColorViewTabBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_8), context));
	resourceDictionary.AddDeferred("EnumToBoolConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_9), context));
	resourceDictionary.AddDeferred("ToBrushConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_10), context));
	resourceDictionary.AddDeferred("LeftCornerRadiusFilterConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_11), context));
	resourceDictionary.AddDeferred("RightCornerRadiusFilterConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_12), context));
	resourceDictionary.AddDeferred("TopCornerRadiusFilterConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_13), context));
	resourceDictionary.AddDeferred("BottomCornerRadiusFilterConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_14), context));
	resourceDictionary.AddDeferred("TopLeftCornerRadiusConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_15), context));
	resourceDictionary.AddDeferred("BottomRightCornerRadiusConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_16), context));
	resourceDictionary.AddDeferred("AccentColorConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_17), context));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorPreviewerAccentSectionWidth", (object)80.0);
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorPreviewerAccentSectionHeight", (object)40.0);
	resourceDictionary.AddDeferred(typeof(ColorPreviewer), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_18), context));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorSliderSize", (object)20.0);
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorSliderTrackSize", (object)20.0);
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorSliderCornerRadius", (object)new CornerRadius(10.0, 10.0, 10.0, 10.0));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorSliderTrackCornerRadius", (object)new CornerRadius(10.0, 10.0, 10.0, 10.0));
	resourceDictionary.AddDeferred("ColorSliderThumbTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_20), context));
	resourceDictionary.AddDeferred(typeof(ColorSlider), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_22), context));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorPickerFlyoutPlacement", (object)PlacementMode.BottomEdgeAlignedRight);
	resourceDictionary.AddDeferred("ColorToHexWithoutAlphaConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_29), context));
	resourceDictionary.AddDeferred("ColorToHexConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_30), context));
	resourceDictionary.AddDeferred("HexValidationConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_31), context));
	resourceDictionary.AddDeferred(typeof(ColorPicker), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_32), context));
	resourceDictionary.AddDeferred("ContrastBrushConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_38), context));
	resourceDictionary.AddDeferred("ColorToDisplayNameConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_39), context));
	resourceDictionary.AddDeferred("DoNothingForNullConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_40), context));
	resourceDictionary.AddDeferred("ColorViewComponentNumberFormat", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_41), context));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorViewTabStripHeight", (object)48.0);
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorViewComponentLabelWidth", (object)30.0);
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorViewComponentTextInputWidth", (object)80.0);
	resourceDictionary.AddDeferred("ColorViewSpectrumIconGeometry", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_42), context));
	resourceDictionary.AddDeferred("ColorViewPaletteIconGeometry", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_43), context));
	resourceDictionary.AddDeferred("ColorViewComponentsIconGeometry", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_44), context));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorViewTabBackgroundCornerRadius", (object)new CornerRadius(3.0, 3.0, 3.0, 3.0));
	resourceDictionary.AddDeferred("ColorViewPaletteListBoxTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_45), context));
	resourceDictionary.AddDeferred("ColorViewPaletteListBoxItemTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_47), context));
	resourceDictionary.AddDeferred("ColorViewColorModelRadioButtonTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_49), context));
	resourceDictionary.AddDeferred("ColorViewTabItemTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_51), context));
	resourceDictionary.AddDeferred(typeof(ColorView), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_53), context));
	context.PopParent();
	styles2.Resources = resources;
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

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

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FComboBoxItem_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FComboBoxItem_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/ComboBoxItem.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	Style style3 = style2;
	style3.Selector = ((Selector?)null).OfType(typeof(ComboBoxItem));
	Setter setter2;
	Setter setter = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter3 = setter2;
	setter3.Property = TemplatedControl.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter3.Value = value;
	context.PopParent();
	style3.Add(setter);
	Setter setter4 = new Setter();
	setter4.Property = TemplatedControl.BackgroundProperty;
	setter4.Value = new ImmutableSolidColorBrush(16777215u);
	style3.Add(setter4);
	Setter setter5 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter6 = setter2;
	setter6.Property = TemplatedControl.FontFamilyProperty;
	StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	object? value2 = staticResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter6.Value = value2;
	context.PopParent();
	style3.Add(setter5);
	Setter setter7 = new Setter();
	setter7.Property = TemplatedControl.FontWeightProperty;
	setter7.Value = (FontWeight)450;
	style3.Add(setter7);
	Setter setter8 = new Setter();
	setter8.Property = TemplatedControl.FontSizeProperty;
	setter8.Value = 14.0;
	style3.Add(setter8);
	Setter setter9 = new Setter();
	setter9.Property = TemplatedControl.PaddingProperty;
	setter9.Value = new Thickness(8.0, 8.0, 8.0, 8.0);
	style3.Add(setter9);
	Setter setter10 = new Setter();
	setter10.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter10.Value = HorizontalAlignment.Stretch;
	style3.Add(setter10);
	Setter setter11 = new Setter();
	setter11.Property = TemplatedControl.TemplateProperty;
	setter11.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_33.Build_1), context)
	};
	style3.Add(setter11);
	context.PopParent();
	styles2.Add(style);
	Style style4 = (style2 = new Style());
	context.PushParent(style2);
	Style style5 = style2;
	style5.Selector = ((Selector?)null).OfType(typeof(ComboBoxItem)).Class(":pointerover").Template()
		.OfType(typeof(ContentPresenter));
	Setter setter12 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter13 = setter2;
	setter13.Property = ContentPresenter.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value3 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter13.Value = value3;
	context.PopParent();
	style5.Add(setter12);
	Setter setter14 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter15 = setter2;
	setter15.Property = ContentPresenter.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Border");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value4 = dynamicResourceExtension3.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter15.Value = value4;
	context.PopParent();
	style5.Add(setter14);
	context.PopParent();
	styles2.Add(style4);
	Style style6 = (style2 = new Style());
	context.PushParent(style2);
	Style style7 = style2;
	style7.Selector = ((Selector?)null).OfType(typeof(ComboBoxItem)).Class(":disabled").Template()
		.OfType(typeof(ContentPresenter));
	Setter setter16 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter17 = setter2;
	setter17.Property = ContentPresenter.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("ComboBoxItemBackgroundDisabled");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value5 = dynamicResourceExtension4.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter17.Value = value5;
	context.PopParent();
	style7.Add(setter16);
	Setter setter18 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter19 = setter2;
	setter19.Property = ContentPresenter.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("ComboBoxItemBorderBrushDisabled");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value6 = dynamicResourceExtension5.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter19.Value = value6;
	context.PopParent();
	style7.Add(setter18);
	context.PopParent();
	styles2.Add(style6);
	Style style8 = (style2 = new Style());
	context.PushParent(style2);
	Style style9 = style2;
	style9.Selector = ((Selector?)null).OfType(typeof(ComboBoxItem)).Class(":pressed").Template()
		.OfType(typeof(ContentPresenter));
	Setter setter20 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter21 = setter2;
	setter21.Property = ContentPresenter.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value7 = dynamicResourceExtension6.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter21.Value = value7;
	context.PopParent();
	style9.Add(setter20);
	Setter setter22 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter23 = setter2;
	setter23.Property = ContentPresenter.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value8 = dynamicResourceExtension7.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter23.Value = value8;
	context.PopParent();
	style9.Add(setter22);
	Setter setter24 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter25 = setter2;
	setter25.Property = Visual.RenderTransformProperty;
	setter25.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
	context.PopParent();
	style9.Add(setter24);
	context.PopParent();
	styles2.Add(style8);
	Style style10 = (style2 = new Style());
	context.PushParent(style2);
	Style style11 = style2;
	style11.Selector = ((Selector?)null).OfType(typeof(ComboBoxItem)).Class(":selected").Template()
		.OfType(typeof(ContentPresenter));
	Setter setter26 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter27 = setter2;
	setter27.Property = ContentPresenter.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("HighlightStrong");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value9 = dynamicResourceExtension8.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter27.Value = value9;
	context.PopParent();
	style11.Add(setter26);
	Setter setter28 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter29 = setter2;
	setter29.Property = ContentPresenter.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("HighlightStrong");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value10 = dynamicResourceExtension9.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter29.Value = value10;
	context.PopParent();
	style11.Add(setter28);
	context.PopParent();
	styles2.Add(style10);
	Style style12 = (style2 = new Style());
	context.PushParent(style2);
	Style style13 = style2;
	style13.Selector = ((Selector?)null).OfType(typeof(ComboBoxItem)).Class(":selected").Class(":disabled")
		.Template()
		.OfType(typeof(ContentPresenter));
	Setter setter30 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter31 = setter2;
	setter31.Property = ContentPresenter.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("ComboBoxItemBackgroundSelectedDisabled");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value11 = dynamicResourceExtension10.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter31.Value = value11;
	context.PopParent();
	style13.Add(setter30);
	Setter setter32 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter33 = setter2;
	setter33.Property = ContentPresenter.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("ComboBoxItemBorderBrushSelectedDisabled");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value12 = dynamicResourceExtension11.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter33.Value = value12;
	context.PopParent();
	style13.Add(setter32);
	context.PopParent();
	styles2.Add(style12);
	Style style14 = (style2 = new Style());
	context.PushParent(style2);
	Style style15 = style2;
	style15.Selector = ((Selector?)null).OfType(typeof(ComboBoxItem)).Class(":selected").Class(":pointerover")
		.Template()
		.OfType(typeof(ContentPresenter));
	Setter setter34 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter35 = setter2;
	setter35.Property = ContentPresenter.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value13 = dynamicResourceExtension12.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter35.Value = value13;
	context.PopParent();
	style15.Add(setter34);
	Setter setter36 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter37 = setter2;
	setter37.Property = ContentPresenter.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("Border");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value14 = dynamicResourceExtension13.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter37.Value = value14;
	context.PopParent();
	style15.Add(setter36);
	context.PopParent();
	styles2.Add(style14);
	Style style16 = (style2 = new Style());
	context.PushParent(style2);
	Style style17 = style2;
	style17.Selector = ((Selector?)null).OfType(typeof(ComboBoxItem)).Class(":selected").Class(":pressed")
		.Template()
		.OfType(typeof(ContentPresenter));
	Setter setter38 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter39 = setter2;
	setter39.Property = ContentPresenter.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("HighlightStrong");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value15 = dynamicResourceExtension14.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter39.Value = value15;
	context.PopParent();
	style17.Add(setter38);
	Setter setter40 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter41 = setter2;
	setter41.Property = ContentPresenter.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("HighlightStrong");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value16 = dynamicResourceExtension15.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter41.Value = value16;
	context.PopParent();
	style17.Add(setter40);
	context.PopParent();
	styles2.Add(style16);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Tabalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FDragTabItem_002Eaxaml(IServiceProvider P_0, ResourceDictionary P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FDragTabItem_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/DragTabItem.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	ResourceDictionary resourceDictionary2;
	ResourceDictionary resourceDictionary = (resourceDictionary2 = P_1);
	context.PushParent(resourceDictionary2);
	if (resourceDictionary2 is ResourceDictionary resourceDictionary3)
	{
		resourceDictionary3.EnsureCapacity(resourceDictionary3.Count + 6);
	}
	((IDictionary<object, object>)resourceDictionary2).Add((object)"TabItemMinHeight", (object)48.0);
	((IDictionary<object, object>)resourceDictionary2).Add((object)"TabItemVerticalPipeHeight", (object)24.0);
	((IDictionary<object, object>)resourceDictionary2).Add((object)"TabItemPipeThickness", (object)2.0);
	resourceDictionary2.AddDeferred("ShowDefaultCloseButtonConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_34.Build_1), context));
	resourceDictionary2.AddDeferred("DragTabItemThumb", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_34.Build_2), context));
	resourceDictionary2.AddDeferred(typeof(DragTabItem), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_34.Build_4), context));
	context.PopParent();
	if (resourceDictionary is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FFlyoutPresenter_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FFlyoutPresenter_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/FlyoutPresenter.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Style style = new Style();
	style.Selector = ((Selector?)null).OfType(typeof(FlyoutPresenter));
	Setter setter = new Setter();
	setter.Property = TemplatedControl.TemplateProperty;
	setter.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_36.Build_1), context)
	};
	style.Add(setter);
	P_1.Add(style);
	if (P_1 is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FDropDownButton_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FDropDownButton_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/DropDownButton.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	style2.Selector = ((Selector?)null).OfType(typeof(DropDownButton));
	Setter setter2;
	Setter setter = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter3 = setter2;
	setter3.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundTertiary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter3.Value = value;
	context.PopParent();
	style2.Add(setter);
	Setter setter4 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter5 = setter2;
	setter5.Property = TemplatedControl.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("ComboBoxBorderBrush");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value2 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter5.Value = value2;
	context.PopParent();
	style2.Add(setter4);
	Setter setter6 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter7 = setter2;
	setter7.Property = TemplatedControl.BorderThicknessProperty;
	DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("ThemeBorderThickness");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value3 = dynamicResourceExtension3.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter7.Value = value3;
	context.PopParent();
	style2.Add(setter6);
	Setter setter8 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter9 = setter2;
	setter9.Property = TemplatedControl.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value4 = dynamicResourceExtension4.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter9.Value = value4;
	context.PopParent();
	style2.Add(setter8);
	Setter setter10 = new Setter();
	setter10.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter10.Value = HorizontalAlignment.Center;
	style2.Add(setter10);
	Setter setter11 = new Setter();
	setter11.Property = ContentControl.VerticalContentAlignmentProperty;
	setter11.Value = VerticalAlignment.Center;
	style2.Add(setter11);
	Setter setter12 = new Setter();
	setter12.Property = InputElement.CursorProperty;
	setter12.Value = new Cursor(StandardCursorType.Hand);
	style2.Add(setter12);
	Setter setter13 = new Setter();
	setter13.Property = TemplatedControl.PaddingProperty;
	setter13.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style2.Add(setter13);
	Setter setter14 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter15 = setter2;
	setter15.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate;
	ControlTemplate value5 = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_35.Build_1), context);
	context.PopParent();
	setter15.Value = value5;
	context.PopParent();
	style2.Add(setter14);
	Style style3 = new Style();
	style3.Selector = ((Selector?)null).Nesting().Class(":pointerover").Template()
		.OfType(typeof(RootSvgImage))
		.Name("DropDownGlyph");
	Setter setter16 = new Setter();
	setter16.Property = Visual.OpacityProperty;
	setter16.Value = 1.0;
	style3.Add(setter16);
	style2.Add(style3);
	context.PopParent();
	styles2.Add(style);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FLinkButton_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FLinkButton_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/LinkButton.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Style style = new Style();
	style.Selector = ((Selector?)null).OfType(typeof(RootLinkButton));
	Setter setter = new Setter();
	setter.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter.Value = HorizontalAlignment.Left;
	style.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = InputElement.FocusableProperty;
	setter2.Value = false;
	style.Add(setter2);
	Setter setter3 = new Setter();
	setter3.Property = TemplatedControl.TemplateProperty;
	setter3.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_37.Build_1), context)
	};
	style.Add(setter3);
	P_1.Add(style);
	Style style2 = new Style();
	style2.Selector = ((Selector?)null).OfType(typeof(RootLinkButton)).Class(":pointerover").Template()
		.OfType(typeof(TextBlock));
	Setter setter4 = new Setter();
	setter4.Property = TextBlock.TextDecorationsProperty;
	setter4.Value = TextDecorations.Underline;
	style2.Add(setter4);
	P_1.Add(style2);
	Style style3 = new Style();
	style3.Selector = ((Selector?)null).OfType(typeof(RootLinkButton)).Class(":pointerover");
	Setter setter5 = new Setter();
	setter5.Property = Visual.OpacityProperty;
	setter5.Value = 0.7;
	style3.Add(setter5);
	Setter setter6 = new Setter();
	setter6.Property = InputElement.CursorProperty;
	setter6.Value = new Cursor(StandardCursorType.Hand);
	style3.Add(setter6);
	P_1.Add(style3);
	Style style4 = new Style();
	style4.Selector = ((Selector?)null).OfType(typeof(RootLinkButton)).Class(":pressed");
	Setter setter7 = new Setter();
	setter7.Property = Visual.OpacityProperty;
	setter7.Value = 0.5;
	style4.Add(setter7);
	P_1.Add(style4);
	if (P_1 is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media.Immutable;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FListBox_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FListBox_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/ListBox.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Style style = new Style();
	style.Selector = ((Selector?)null).OfType(typeof(ListBox));
	Setter setter = new Setter();
	setter.Property = TemplatedControl.BackgroundProperty;
	setter.Value = new ImmutableSolidColorBrush(16777215u);
	style.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = TemplatedControl.BorderBrushProperty;
	setter2.Value = new ImmutableSolidColorBrush(16777215u);
	style.Add(setter2);
	Setter setter3 = new Setter();
	setter3.Property = TemplatedControl.BorderThicknessProperty;
	setter3.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style.Add(setter3);
	Setter setter4 = new Setter();
	setter4.Property = ScrollViewer.HorizontalScrollBarVisibilityProperty;
	setter4.Value = ScrollBarVisibility.Disabled;
	style.Add(setter4);
	Setter setter5 = new Setter();
	setter5.Property = ScrollViewer.VerticalScrollBarVisibilityProperty;
	setter5.Value = ScrollBarVisibility.Auto;
	style.Add(setter5);
	Setter setter6 = new Setter();
	setter6.Property = ScrollViewer.IsScrollChainingEnabledProperty;
	setter6.Value = true;
	style.Add(setter6);
	Setter setter7 = new Setter();
	setter7.Property = ScrollViewer.IsScrollInertiaEnabledProperty;
	setter7.Value = true;
	style.Add(setter7);
	Setter setter8 = new Setter();
	setter8.Property = TemplatedControl.TemplateProperty;
	setter8.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_38.Build_1), context)
	};
	style.Add(setter8);
	P_1.Add(style);
	if (P_1 is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media.Immutable;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FListBoxItem_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FListBoxItem_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/ListBoxItem.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Style style = new Style();
	style.Selector = ((Selector?)null).OfType(typeof(ListBoxItem));
	Setter setter = new Setter();
	setter.Property = TemplatedControl.BackgroundProperty;
	setter.Value = new ImmutableSolidColorBrush(16777215u);
	style.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = TemplatedControl.BorderBrushProperty;
	setter2.Value = new ImmutableSolidColorBrush(16777215u);
	style.Add(setter2);
	Setter setter3 = new Setter();
	setter3.Property = TemplatedControl.BorderThicknessProperty;
	setter3.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style.Add(setter3);
	Setter setter4 = new Setter();
	setter4.Property = TemplatedControl.PaddingProperty;
	setter4.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style.Add(setter4);
	Setter setter5 = new Setter();
	setter5.Property = TemplatedControl.TemplateProperty;
	setter5.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_39.Build_1), context)
	};
	style.Add(setter5);
	Style style2 = new Style();
	style2.Selector = ((Selector?)null).Nesting().Class(":pointerover").Template()
		.OfType(typeof(ContentPresenter));
	Setter setter6 = new Setter();
	setter6.Property = ContentPresenter.BackgroundProperty;
	setter6.Value = new ImmutableSolidColorBrush(16777215u);
	style2.Add(setter6);
	style.Add(style2);
	Style style3 = new Style();
	style3.Selector = ((Selector?)null).Nesting().Class(":selected").Template()
		.OfType(typeof(ContentPresenter));
	Setter setter7 = new Setter();
	setter7.Property = ContentPresenter.BackgroundProperty;
	setter7.Value = new ImmutableSolidColorBrush(16777215u);
	style3.Add(setter7);
	style.Add(style3);
	Style style4 = new Style();
	style4.Selector = ((Selector?)null).Nesting().Class(":selected").Class(":focus")
		.Template()
		.OfType(typeof(ContentPresenter));
	Setter setter8 = new Setter();
	setter8.Property = ContentPresenter.BackgroundProperty;
	setter8.Value = new ImmutableSolidColorBrush(16777215u);
	style4.Add(setter8);
	style.Add(style4);
	Style style5 = new Style();
	style5.Selector = ((Selector?)null).Nesting().Class(":selected").Class(":pointerover")
		.Template()
		.OfType(typeof(ContentPresenter));
	Setter setter9 = new Setter();
	setter9.Property = ContentPresenter.BackgroundProperty;
	setter9.Value = new ImmutableSolidColorBrush(16777215u);
	style5.Add(setter9);
	style.Add(style5);
	Style style6 = new Style();
	style6.Selector = ((Selector?)null).Nesting().Class(":selected").Class(":focus")
		.Class(":pointerover")
		.Template()
		.OfType(typeof(ContentPresenter));
	Setter setter10 = new Setter();
	setter10.Property = ContentPresenter.BackgroundProperty;
	setter10.Value = new ImmutableSolidColorBrush(16777215u);
	style6.Add(setter10);
	style.Add(style6);
	P_1.Add(style);
	if (P_1 is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FMenuFlyoutPresenter_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FMenuFlyoutPresenter_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/MenuFlyoutPresenter.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	style2.Selector = ((Selector?)null).OfType(typeof(MenuFlyoutPresenter));
	Setter setter = new Setter();
	setter.Property = Layoutable.MinWidthProperty;
	setter.Value = 225.0;
	style2.Add(setter);
	Setter setter3;
	Setter setter2 = (setter3 = new Setter());
	context.PushParent(setter3);
	setter3.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate;
	ControlTemplate value = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_40.Build_1), context);
	context.PopParent();
	setter3.Value = value;
	context.PopParent();
	style2.Add(setter2);
	context.PopParent();
	styles2.Add(style);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FMenuItem_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FMenuItem_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/MenuItem.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	Style style3 = style2;
	style3.Selector = ((Selector?)null).OfType(typeof(MenuItem));
	Setter setter2;
	Setter setter = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter3 = setter2;
	setter3.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate;
	ControlTemplate value = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_41.Build_1), context);
	context.PopParent();
	setter3.Value = value;
	context.PopParent();
	style3.Add(setter);
	Style style4 = new Style();
	style4.Selector = ((Selector?)null).Nesting().Class(":separator");
	Setter setter4 = new Setter();
	setter4.Property = TemplatedControl.TemplateProperty;
	setter4.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_41.Build_3), context)
	};
	style4.Add(setter4);
	style3.Add(style4);
	Style style5 = (style2 = new Style());
	context.PushParent(style2);
	Style style6 = style2;
	style6.Selector = ((Selector?)null).Nesting().Class(":selected").Template()
		.OfType(typeof(Border))
		.Name("root");
	Setter setter5 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter6 = setter2;
	setter6.Property = Border.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value2 = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter6.Value = value2;
	context.PopParent();
	style6.Add(setter5);
	context.PopParent();
	style3.Add(style5);
	Style style7 = new Style();
	style7.Selector = ((Selector?)null).Nesting().Class(":empty").Template()
		.OfType(typeof(RootSvgImage))
		.Name("rightArrow");
	Setter setter7 = new Setter();
	setter7.Property = Visual.IsVisibleProperty;
	setter7.Value = false;
	style7.Add(setter7);
	style3.Add(style7);
	style3.Add(new Style
	{
		Selector = ((Selector?)null).Nesting().Class(":disabled")
	});
	context.PopParent();
	styles2.Add(style);
	Style style8 = (style2 = new Style());
	context.PushParent(style2);
	Style style9 = style2;
	style9.Selector = ((Selector?)null).OfType(typeof(MenuItem)).Class("DeleteMenuItem");
	Setter setter8 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter9 = setter2;
	setter9.Property = TemplatedControl.TemplateProperty;
	ControlTemplate value3 = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_41.Build_4), context);
	context.PopParent();
	setter9.Value = value3;
	context.PopParent();
	style9.Add(setter8);
	Style style10 = new Style();
	style10.Selector = ((Selector?)null).Nesting().Class(":separator");
	Setter setter10 = new Setter();
	setter10.Property = TemplatedControl.TemplateProperty;
	setter10.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_41.Build_6), context)
	};
	style10.Add(setter10);
	style9.Add(style10);
	Style style11 = (style2 = new Style());
	context.PushParent(style2);
	Style style12 = style2;
	style12.Selector = ((Selector?)null).Nesting().Class(":selected").Template()
		.OfType(typeof(Border))
		.Name("root");
	Setter setter11 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter12 = setter2;
	setter12.Property = Border.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value4 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter12.Value = value4;
	context.PopParent();
	style12.Add(setter11);
	context.PopParent();
	style9.Add(style11);
	Style style13 = new Style();
	style13.Selector = ((Selector?)null).Nesting().Class(":empty").Template()
		.OfType(typeof(RootSvgImage))
		.Name("rightArrow");
	Setter setter13 = new Setter();
	setter13.Property = Visual.IsVisibleProperty;
	setter13.Value = false;
	style13.Add(setter13);
	style9.Add(style13);
	style9.Add(new Style
	{
		Selector = ((Selector?)null).Nesting().Class(":disabled")
	});
	context.PopParent();
	styles2.Add(style8);
	Style style14 = new Style();
	style14.Selector = ((Selector?)null).OfType(typeof(MenuItem)).Class("SliderMenuItem").Template()
		.OfType(typeof(Border))
		.Name("root");
	Setter setter14 = new Setter();
	setter14.Property = Layoutable.HeightProperty;
	setter14.Value = double.NaN;
	style14.Add(setter14);
	Setter setter15 = new Setter();
	setter15.Property = Decorator.PaddingProperty;
	setter15.Value = new Thickness(0.0, 8.0, 0.0, 8.0);
	style14.Add(setter15);
	styles2.Add(style14);
	Style style15 = new Style();
	style15.Selector = ((Selector?)null).OfType(typeof(MenuItem)).Class("SliderMenuItem").Class(":selected")
		.Template()
		.OfType(typeof(Border))
		.Name("root");
	Setter setter16 = new Setter();
	setter16.Property = Border.BackgroundProperty;
	setter16.Value = new ImmutableSolidColorBrush(16777215u);
	style15.Add(setter16);
	styles2.Add(style15);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Markdown.Components;

public static void Populate_003A_002FResources_002FStyles_002FMessageMarkdown_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FMessageMarkdown_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/MessageMarkdown.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	Style style3 = style2;
	style3.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CTextBlock));
	IList<SetterBase> setters = style3.Setters;
	Setter setter;
	Setter item = (setter = new Setter());
	context.PushParent(setter);
	Setter setter2 = setter;
	setter2.Property = CTextBlock.SelectionBrushProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BrandPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter2.Value = value;
	context.PopParent();
	setters.Add(item);
	IList<SetterBase> setters2 = style3.Setters;
	Setter setter3 = new Setter();
	setter3.Property = CTextBlock.FontSizeProperty;
	setter3.Value = 15.0;
	setters2.Add(setter3);
	IList<SetterBase> setters3 = style3.Setters;
	Setter item2 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter4 = setter;
	setter4.Property = CTextBlock.FontFamilyProperty;
	StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	object? value2 = staticResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter4.Value = value2;
	context.PopParent();
	setters3.Add(item2);
	IList<SetterBase> setters4 = style3.Setters;
	Setter setter5 = new Setter();
	setter5.Property = CTextBlock.FontWeightProperty;
	setter5.Value = FontWeight.Normal;
	setters4.Add(setter5);
	IList<SetterBase> setters5 = style3.Setters;
	Setter item3 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter6 = setter;
	setter6.Property = CTextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value3 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter6.Value = value3;
	context.PopParent();
	setters5.Add(item3);
	IList<SetterBase> setters6 = style3.Setters;
	Setter setter7 = new Setter();
	setter7.Property = CTextBlock.LineSpacingProperty;
	setter7.Value = 4.0;
	setters6.Add(setter7);
	context.PopParent();
	styles2.Add(style);
	Style style4 = (style2 = new Style());
	context.PushParent(style2);
	Style style5 = style2;
	style5.Selector = ((Selector?)null).Class("SimpleMessage").Descendant().OfType(typeof(CTextBlock));
	IList<SetterBase> setters7 = style5.Setters;
	Setter item4 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter8 = setter;
	setter8.Property = CTextBlock.SelectionBrushProperty;
	DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("BrandPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value4 = dynamicResourceExtension3.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter8.Value = value4;
	context.PopParent();
	setters7.Add(item4);
	IList<SetterBase> setters8 = style5.Setters;
	Setter setter9 = new Setter();
	setter9.Property = CTextBlock.FontSizeProperty;
	setter9.Value = 14.0;
	setters8.Add(setter9);
	IList<SetterBase> setters9 = style5.Setters;
	Setter item5 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter10 = setter;
	setter10.Property = CTextBlock.FontFamilyProperty;
	StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	object? value5 = staticResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter10.Value = value5;
	context.PopParent();
	setters9.Add(item5);
	IList<SetterBase> setters10 = style5.Setters;
	Setter setter11 = new Setter();
	setter11.Property = CTextBlock.FontWeightProperty;
	setter11.Value = (FontWeight)450;
	setters10.Add(setter11);
	IList<SetterBase> setters11 = style5.Setters;
	Setter item6 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter12 = setter;
	setter12.Property = CTextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextSecondary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value6 = dynamicResourceExtension4.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter12.Value = value6;
	context.PopParent();
	setters11.Add(item6);
	IList<SetterBase> setters12 = style5.Setters;
	Setter setter13 = new Setter();
	setter13.Property = CTextBlock.LineSpacingProperty;
	setter13.Value = 2.0;
	setters12.Add(setter13);
	context.PopParent();
	styles2.Add(style4);
	Style style6 = (style2 = new Style());
	context.PushParent(style2);
	Style style7 = style2;
	style7.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CTextBlock))
		.Class("Heading1");
	IList<SetterBase> setters13 = style7.Setters;
	Setter setter14 = new Setter();
	setter14.Property = CTextBlock.FontSizeProperty;
	setter14.Value = 26.0;
	setters13.Add(setter14);
	IList<SetterBase> setters14 = style7.Setters;
	Setter item7 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter15 = setter;
	setter15.Property = CTextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value7 = dynamicResourceExtension5.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter15.Value = value7;
	context.PopParent();
	setters14.Add(item7);
	IList<SetterBase> setters15 = style7.Setters;
	Setter item8 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter16 = setter;
	setter16.Property = CTextBlock.FontFamilyProperty;
	StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	object? value8 = staticResourceExtension3.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter16.Value = value8;
	context.PopParent();
	setters15.Add(item8);
	IList<SetterBase> setters16 = style7.Setters;
	Setter setter17 = new Setter();
	setter17.Property = CTextBlock.FontWeightProperty;
	setter17.Value = FontWeight.Bold;
	setters16.Add(setter17);
	context.PopParent();
	styles2.Add(style6);
	Style style8 = (style2 = new Style());
	context.PushParent(style2);
	Style style9 = style2;
	style9.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CTextBlock))
		.Class("Heading2");
	IList<SetterBase> setters17 = style9.Setters;
	Setter setter18 = new Setter();
	setter18.Property = CTextBlock.FontSizeProperty;
	setter18.Value = 22.0;
	setters17.Add(setter18);
	IList<SetterBase> setters18 = style9.Setters;
	Setter item9 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter19 = setter;
	setter19.Property = CTextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value9 = dynamicResourceExtension6.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter19.Value = value9;
	context.PopParent();
	setters18.Add(item9);
	IList<SetterBase> setters19 = style9.Setters;
	Setter item10 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter20 = setter;
	setter20.Property = CTextBlock.FontFamilyProperty;
	StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	object? value10 = staticResourceExtension4.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter20.Value = value10;
	context.PopParent();
	setters19.Add(item10);
	IList<SetterBase> setters20 = style9.Setters;
	Setter setter21 = new Setter();
	setter21.Property = CTextBlock.FontWeightProperty;
	setter21.Value = FontWeight.Medium;
	setters20.Add(setter21);
	context.PopParent();
	styles2.Add(style8);
	Style style10 = (style2 = new Style());
	context.PushParent(style2);
	Style style11 = style2;
	style11.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CTextBlock))
		.Class("Heading3");
	IList<SetterBase> setters21 = style11.Setters;
	Setter setter22 = new Setter();
	setter22.Property = CTextBlock.FontSizeProperty;
	setter22.Value = 18.0;
	setters21.Add(setter22);
	IList<SetterBase> setters22 = style11.Setters;
	Setter item11 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter23 = setter;
	setter23.Property = CTextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value11 = dynamicResourceExtension7.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter23.Value = value11;
	context.PopParent();
	setters22.Add(item11);
	IList<SetterBase> setters23 = style11.Setters;
	Setter item12 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter24 = setter;
	setter24.Property = CTextBlock.FontFamilyProperty;
	StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	object? value12 = staticResourceExtension5.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter24.Value = value12;
	context.PopParent();
	setters23.Add(item12);
	IList<SetterBase> setters24 = style11.Setters;
	Setter setter25 = new Setter();
	setter25.Property = CTextBlock.FontWeightProperty;
	setter25.Value = (FontWeight)450;
	setters24.Add(setter25);
	context.PopParent();
	styles2.Add(style10);
	Style style12 = (style2 = new Style());
	context.PushParent(style2);
	Style style13 = style2;
	style13.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CTextBlock))
		.Class("Heading4");
	IList<SetterBase> setters25 = style13.Setters;
	Setter setter26 = new Setter();
	setter26.Property = CTextBlock.FontSizeProperty;
	setter26.Value = 15.0;
	setters25.Add(setter26);
	IList<SetterBase> setters26 = style13.Setters;
	Setter item13 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter27 = setter;
	setter27.Property = CTextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value13 = dynamicResourceExtension8.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter27.Value = value13;
	context.PopParent();
	setters26.Add(item13);
	IList<SetterBase> setters27 = style13.Setters;
	Setter item14 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter28 = setter;
	setter28.Property = CTextBlock.FontFamilyProperty;
	StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	object? value14 = staticResourceExtension6.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter28.Value = value14;
	context.PopParent();
	setters27.Add(item14);
	IList<SetterBase> setters28 = style13.Setters;
	Setter setter29 = new Setter();
	setter29.Property = CTextBlock.FontWeightProperty;
	setter29.Value = (FontWeight)450;
	setters28.Add(setter29);
	context.PopParent();
	styles2.Add(style12);
	Style style14 = (style2 = new Style());
	context.PushParent(style2);
	Style style15 = style2;
	style15.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(Border))
		.Class("CodeBlock");
	IList<SetterBase> setters29 = style15.Setters;
	Setter item15 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter30 = setter;
	setter30.Property = Border.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("BackgroundSecondary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value15 = dynamicResourceExtension9.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter30.Value = value15;
	context.PopParent();
	setters29.Add(item15);
	IList<SetterBase> setters30 = style15.Setters;
	Setter item16 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter31 = setter;
	setter31.Property = Border.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("Border");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value16 = dynamicResourceExtension10.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter31.Value = value16;
	context.PopParent();
	setters30.Add(item16);
	IList<SetterBase> setters31 = style15.Setters;
	Setter setter32 = new Setter();
	setter32.Property = Border.CornerRadiusProperty;
	setter32.Value = new CornerRadius(8.0, 8.0, 8.0, 8.0);
	setters31.Add(setter32);
	context.PopParent();
	styles2.Add(style14);
	Style style16 = (style2 = new Style());
	context.PushParent(style2);
	Style style17 = style2;
	style17.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CTextBlock))
		.Class("CodeBlock");
	IList<SetterBase> setters32 = style17.Setters;
	Setter setter33 = new Setter();
	setter33.Property = CTextBlock.FontSizeProperty;
	setter33.Value = 15.0;
	setters32.Add(setter33);
	IList<SetterBase> setters33 = style17.Setters;
	Setter item17 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter34 = setter;
	setter34.Property = CTextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value17 = dynamicResourceExtension11.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter34.Value = value17;
	context.PopParent();
	setters33.Add(item17);
	IList<SetterBase> setters34 = style17.Setters;
	Setter setter35 = new Setter();
	setter35.Property = CTextBlock.FontFamilyProperty;
	setter35.Value = new FontFamily(((IUriContext)context).BaseUri, "Consolas");
	setters34.Add(setter35);
	IList<SetterBase> setters35 = style17.Setters;
	Setter setter36 = new Setter();
	setter36.Property = CTextBlock.SelectionBrushProperty;
	setter36.Value = new ImmutableSolidColorBrush(4294951115u);
	setters35.Add(setter36);
	IList<SetterBase> setters36 = style17.Setters;
	Setter setter37 = new Setter();
	setter37.Property = Layoutable.MarginProperty;
	setter37.Value = new Thickness(16.0, 16.0, 16.0, 16.0);
	setters36.Add(setter37);
	context.PopParent();
	styles2.Add(style16);
	Style style18 = new Style();
	style18.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(Border))
		.Class("NoContainer");
	IList<SetterBase> setters37 = style18.Setters;
	Setter setter38 = new Setter();
	setter38.Property = Border.BorderBrushProperty;
	setter38.Value = new ImmutableSolidColorBrush(4294901760u);
	setters37.Add(setter38);
	styles2.Add(style18);
	Style style19 = (style2 = new Style());
	context.PushParent(style2);
	Style style20 = style2;
	style20.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CCode))
		.Class("inlineCode");
	IList<SetterBase> setters38 = style20.Setters;
	Setter item18 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter39 = setter;
	setter39.Property = CInline.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value18 = dynamicResourceExtension12.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter39.Value = value18;
	context.PopParent();
	setters38.Add(item18);
	IList<SetterBase> setters39 = style20.Setters;
	Setter item19 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter40 = setter;
	setter40.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value19 = dynamicResourceExtension13.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter40.Value = value19;
	context.PopParent();
	setters39.Add(item19);
	context.PopParent();
	styles2.Add(style19);
	Style style21 = (style2 = new Style());
	context.PushParent(style2);
	Style style22 = style2;
	style22.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CCode))
		.Class("inlineCode_simple");
	IList<SetterBase> setters40 = style22.Setters;
	Setter item20 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter41 = setter;
	setter41.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value20 = dynamicResourceExtension14.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter41.Value = value20;
	context.PopParent();
	setters40.Add(item20);
	IList<SetterBase> setters41 = style22.Setters;
	Setter setter42 = new Setter();
	setter42.Property = CInline.FontStyleProperty;
	setter42.Value = FontStyle.Italic;
	setters41.Add(setter42);
	context.PopParent();
	styles2.Add(style21);
	Style style23 = new Style();
	style23.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(Grid))
		.Class("List");
	IList<SetterBase> setters42 = style23.Setters;
	Setter setter43 = new Setter();
	setter43.Property = Layoutable.MarginProperty;
	setter43.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	setters42.Add(setter43);
	styles2.Add(style23);
	Style style24 = new Style();
	style24.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CTextBlock))
		.Class("ListMarker");
	IList<SetterBase> setters43 = style24.Setters;
	Setter setter44 = new Setter();
	setter44.Property = Layoutable.MarginProperty;
	setter44.Value = new Thickness(0.0, 3.0, 10.0, 3.0);
	setters43.Add(setter44);
	IList<SetterBase> setters44 = style24.Setters;
	Setter setter45 = new Setter();
	setter45.Property = CTextBlock.FontWeightProperty;
	setter45.Value = (FontWeight)450;
	setters44.Add(setter45);
	styles2.Add(style24);
	Style style25 = (style2 = new Style());
	context.PushParent(style2);
	Style style26 = style2;
	style26.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(Border))
		.Class("Blockquote");
	IList<SetterBase> setters45 = style26.Setters;
	Setter item21 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter46 = setter;
	setter46.Property = Border.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value21 = dynamicResourceExtension15.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter46.Value = value21;
	context.PopParent();
	setters45.Add(item21);
	IList<SetterBase> setters46 = style26.Setters;
	Setter setter47 = new Setter();
	setter47.Property = Border.BackgroundProperty;
	setter47.Value = new ImmutableSolidColorBrush(16777215u);
	setters46.Add(setter47);
	IList<SetterBase> setters47 = style26.Setters;
	Setter setter48 = new Setter();
	setter48.Property = Border.BorderThicknessProperty;
	setter48.Value = new Thickness(2.0, 0.0, 0.0, 0.0);
	setters47.Add(setter48);
	IList<SetterBase> setters48 = style26.Setters;
	Setter setter49 = new Setter();
	setter49.Property = Layoutable.MarginProperty;
	setter49.Value = new Thickness(12.0, 5.0, 0.0, 5.0);
	setters48.Add(setter49);
	context.PopParent();
	styles2.Add(style25);
	Style style27 = new Style();
	style27.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(StackPanel))
		.Class("Blockquote");
	IList<SetterBase> setters49 = style27.Setters;
	Setter setter50 = new Setter();
	setter50.Property = Layoutable.MarginProperty;
	setter50.Value = new Thickness(18.0, 5.0, 0.0, 5.0);
	setters49.Add(setter50);
	styles2.Add(style27);
	Style style28 = (style2 = new Style());
	context.PushParent(style2);
	Style style29 = style2;
	style29.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CHyperlink))
		.Class("selfMention");
	IList<SetterBase> setters50 = style29.Setters;
	Setter item22 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter51 = setter;
	setter51.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value22 = dynamicResourceExtension16.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter51.Value = value22;
	context.PopParent();
	setters50.Add(item22);
	IList<SetterBase> setters51 = style29.Setters;
	Setter item23 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter52 = setter;
	setter52.Property = CInline.FontFamilyProperty;
	DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value23 = dynamicResourceExtension17.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter52.Value = value23;
	context.PopParent();
	setters51.Add(item23);
	IList<SetterBase> setters52 = style29.Setters;
	Setter setter53 = new Setter();
	setter53.Property = CInline.FontWeightProperty;
	setter53.Value = (FontWeight)450;
	setters52.Add(setter53);
	IList<SetterBase> setters53 = style29.Setters;
	Setter item24 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter54 = setter;
	setter54.Property = CSpan.BorderBackgroundBrushProperty;
	DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("SelfMentionBackground");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value24 = dynamicResourceExtension18.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter54.Value = value24;
	context.PopParent();
	setters53.Add(item24);
	IList<SetterBase> setters54 = style29.Setters;
	Setter item25 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter55 = setter;
	setter55.Property = CSpan.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension19 = new DynamicResourceExtension("SelfMentionBorder");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value25 = dynamicResourceExtension19.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter55.Value = value25;
	context.PopParent();
	setters54.Add(item25);
	IList<SetterBase> setters55 = style29.Setters;
	Setter setter56 = new Setter();
	setter56.Property = CSpan.BorderThicknessProperty;
	setter56.Value = new Thickness(1.0, 1.0, 1.0, 1.0);
	setters55.Add(setter56);
	IList<SetterBase> setters56 = style29.Setters;
	Setter setter57 = new Setter();
	setter57.Property = CSpan.CornerRadiusProperty;
	setter57.Value = new CornerRadius(5.0, 5.0, 5.0, 5.0);
	setters56.Add(setter57);
	IList<SetterBase> setters57 = style29.Setters;
	Setter setter58 = new Setter();
	setter58.Property = CSpan.PaddingProperty;
	setter58.Value = new Thickness(3.0, 1.0, 3.0, 1.0);
	setters57.Add(setter58);
	IList<SetterBase> setters58 = style29.Setters;
	Setter setter59 = new Setter();
	setter59.Property = CSpan.PressedScaleProperty;
	setter59.Value = 0.98;
	setters58.Add(setter59);
	context.PopParent();
	styles2.Add(style28);
	Style style30 = (style2 = new Style());
	context.PushParent(style2);
	Style style31 = style2;
	style31.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CRun))
		.Class("selfMention_simple");
	IList<SetterBase> setters59 = style31.Setters;
	Setter item26 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter60 = setter;
	setter60.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension20 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value26 = dynamicResourceExtension20.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter60.Value = value26;
	context.PopParent();
	setters59.Add(item26);
	IList<SetterBase> setters60 = style31.Setters;
	Setter item27 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter61 = setter;
	setter61.Property = CInline.FontFamilyProperty;
	DynamicResourceExtension dynamicResourceExtension21 = new DynamicResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value27 = dynamicResourceExtension21.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter61.Value = value27;
	context.PopParent();
	setters60.Add(item27);
	IList<SetterBase> setters61 = style31.Setters;
	Setter setter62 = new Setter();
	setter62.Property = CInline.FontWeightProperty;
	setter62.Value = FontWeight.Medium;
	setters61.Add(setter62);
	context.PopParent();
	styles2.Add(style30);
	Style style32 = (style2 = new Style());
	context.PushParent(style2);
	Style style33 = style2;
	style33.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CHyperlink))
		.Class("otherMention");
	IList<SetterBase> setters62 = style33.Setters;
	Setter item28 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter63 = setter;
	setter63.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension22 = new DynamicResourceExtension("BrandPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value28 = dynamicResourceExtension22.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter63.Value = value28;
	context.PopParent();
	setters62.Add(item28);
	IList<SetterBase> setters63 = style33.Setters;
	Setter item29 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter64 = setter;
	setter64.Property = CInline.FontFamilyProperty;
	DynamicResourceExtension dynamicResourceExtension23 = new DynamicResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value29 = dynamicResourceExtension23.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter64.Value = value29;
	context.PopParent();
	setters63.Add(item29);
	IList<SetterBase> setters64 = style33.Setters;
	Setter setter65 = new Setter();
	setter65.Property = CInline.FontWeightProperty;
	setter65.Value = (FontWeight)450;
	setters64.Add(setter65);
	IList<SetterBase> setters65 = style33.Setters;
	Setter item30 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter66 = setter;
	setter66.Property = CSpan.BorderBackgroundBrushProperty;
	DynamicResourceExtension dynamicResourceExtension24 = new DynamicResourceExtension("OtherMentionBackground");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value30 = dynamicResourceExtension24.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter66.Value = value30;
	context.PopParent();
	setters65.Add(item30);
	IList<SetterBase> setters66 = style33.Setters;
	Setter item31 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter67 = setter;
	setter67.Property = CSpan.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension25 = new DynamicResourceExtension("OtherMentionBorder");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value31 = dynamicResourceExtension25.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter67.Value = value31;
	context.PopParent();
	setters66.Add(item31);
	IList<SetterBase> setters67 = style33.Setters;
	Setter setter68 = new Setter();
	setter68.Property = CSpan.BorderThicknessProperty;
	setter68.Value = new Thickness(1.0, 1.0, 1.0, 1.0);
	setters67.Add(setter68);
	IList<SetterBase> setters68 = style33.Setters;
	Setter setter69 = new Setter();
	setter69.Property = CSpan.CornerRadiusProperty;
	setter69.Value = new CornerRadius(5.0, 5.0, 5.0, 5.0);
	setters68.Add(setter69);
	IList<SetterBase> setters69 = style33.Setters;
	Setter setter70 = new Setter();
	setter70.Property = CSpan.PaddingProperty;
	setter70.Value = new Thickness(3.0, 1.0, 3.0, 1.0);
	setters69.Add(setter70);
	IList<SetterBase> setters70 = style33.Setters;
	Setter setter71 = new Setter();
	setter71.Property = CSpan.PressedScaleProperty;
	setter71.Value = 0.98;
	setters70.Add(setter71);
	context.PopParent();
	styles2.Add(style32);
	Style style34 = (style2 = new Style());
	context.PushParent(style2);
	Style style35 = style2;
	style35.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CRun))
		.Class("otherMention_simple");
	IList<SetterBase> setters71 = style35.Setters;
	Setter item32 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter72 = setter;
	setter72.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension26 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value32 = dynamicResourceExtension26.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter72.Value = value32;
	context.PopParent();
	setters71.Add(item32);
	IList<SetterBase> setters72 = style35.Setters;
	Setter item33 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter73 = setter;
	setter73.Property = CInline.FontFamilyProperty;
	DynamicResourceExtension dynamicResourceExtension27 = new DynamicResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value33 = dynamicResourceExtension27.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter73.Value = value33;
	context.PopParent();
	setters72.Add(item33);
	IList<SetterBase> setters73 = style35.Setters;
	Setter setter74 = new Setter();
	setter74.Property = CInline.FontWeightProperty;
	setter74.Value = FontWeight.Medium;
	setters73.Add(setter74);
	context.PopParent();
	styles2.Add(style34);
	Style style36 = (style2 = new Style());
	context.PushParent(style2);
	Style style37 = style2;
	style37.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CHyperlink))
		.Class("roleMention");
	IList<SetterBase> setters74 = style37.Setters;
	Setter item34 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter75 = setter;
	setter75.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension28 = new DynamicResourceExtension("RoleMentionText");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value34 = dynamicResourceExtension28.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter75.Value = value34;
	context.PopParent();
	setters74.Add(item34);
	IList<SetterBase> setters75 = style37.Setters;
	Setter item35 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter76 = setter;
	setter76.Property = CInline.FontFamilyProperty;
	DynamicResourceExtension dynamicResourceExtension29 = new DynamicResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value35 = dynamicResourceExtension29.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter76.Value = value35;
	context.PopParent();
	setters75.Add(item35);
	IList<SetterBase> setters76 = style37.Setters;
	Setter setter77 = new Setter();
	setter77.Property = CInline.FontWeightProperty;
	setter77.Value = (FontWeight)450;
	setters76.Add(setter77);
	IList<SetterBase> setters77 = style37.Setters;
	Setter item36 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter78 = setter;
	setter78.Property = CSpan.BorderBackgroundBrushProperty;
	DynamicResourceExtension dynamicResourceExtension30 = new DynamicResourceExtension("RoleMentionBackground");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value36 = dynamicResourceExtension30.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter78.Value = value36;
	context.PopParent();
	setters77.Add(item36);
	IList<SetterBase> setters78 = style37.Setters;
	Setter item37 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter79 = setter;
	setter79.Property = CSpan.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension31 = new DynamicResourceExtension("RoleMentionBorder");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value37 = dynamicResourceExtension31.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter79.Value = value37;
	context.PopParent();
	setters78.Add(item37);
	IList<SetterBase> setters79 = style37.Setters;
	Setter setter80 = new Setter();
	setter80.Property = CSpan.BorderThicknessProperty;
	setter80.Value = new Thickness(1.0, 1.0, 1.0, 1.0);
	setters79.Add(setter80);
	IList<SetterBase> setters80 = style37.Setters;
	Setter setter81 = new Setter();
	setter81.Property = CSpan.CornerRadiusProperty;
	setter81.Value = new CornerRadius(5.0, 5.0, 5.0, 5.0);
	setters80.Add(setter81);
	IList<SetterBase> setters81 = style37.Setters;
	Setter setter82 = new Setter();
	setter82.Property = CSpan.PaddingProperty;
	setter82.Value = new Thickness(3.0, 1.0, 3.0, 1.0);
	setters81.Add(setter82);
	context.PopParent();
	styles2.Add(style36);
	Style style38 = (style2 = new Style());
	context.PushParent(style2);
	Style style39 = style2;
	style39.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CRun))
		.Class("roleMention_simple");
	IList<SetterBase> setters82 = style39.Setters;
	Setter item38 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter83 = setter;
	setter83.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension32 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value38 = dynamicResourceExtension32.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter83.Value = value38;
	context.PopParent();
	setters82.Add(item38);
	IList<SetterBase> setters83 = style39.Setters;
	Setter item39 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter84 = setter;
	setter84.Property = CInline.FontFamilyProperty;
	DynamicResourceExtension dynamicResourceExtension33 = new DynamicResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value39 = dynamicResourceExtension33.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter84.Value = value39;
	context.PopParent();
	setters83.Add(item39);
	IList<SetterBase> setters84 = style39.Setters;
	Setter setter85 = new Setter();
	setter85.Property = CInline.FontWeightProperty;
	setter85.Value = FontWeight.Medium;
	setters84.Add(setter85);
	context.PopParent();
	styles2.Add(style38);
	Style style40 = (style2 = new Style());
	context.PushParent(style2);
	Style style41 = style2;
	style41.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CHyperlink))
		.Class("channelMention");
	IList<SetterBase> setters85 = style41.Setters;
	Setter item40 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter86 = setter;
	setter86.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension34 = new DynamicResourceExtension("ChannelMentionText");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value40 = dynamicResourceExtension34.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter86.Value = value40;
	context.PopParent();
	setters85.Add(item40);
	IList<SetterBase> setters86 = style41.Setters;
	Setter item41 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter87 = setter;
	setter87.Property = CInline.FontFamilyProperty;
	DynamicResourceExtension dynamicResourceExtension35 = new DynamicResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value41 = dynamicResourceExtension35.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter87.Value = value41;
	context.PopParent();
	setters86.Add(item41);
	IList<SetterBase> setters87 = style41.Setters;
	Setter setter88 = new Setter();
	setter88.Property = CInline.FontWeightProperty;
	setter88.Value = (FontWeight)450;
	setters87.Add(setter88);
	IList<SetterBase> setters88 = style41.Setters;
	Setter item42 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter89 = setter;
	setter89.Property = CSpan.BorderBackgroundBrushProperty;
	DynamicResourceExtension dynamicResourceExtension36 = new DynamicResourceExtension("ChannelMentionBackground");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value42 = dynamicResourceExtension36.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter89.Value = value42;
	context.PopParent();
	setters88.Add(item42);
	IList<SetterBase> setters89 = style41.Setters;
	Setter item43 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter90 = setter;
	setter90.Property = CSpan.BorderBrushProperty;
	DynamicResourceExtension dynamicResourceExtension37 = new DynamicResourceExtension("ChannelMentionBorder");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value43 = dynamicResourceExtension37.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter90.Value = value43;
	context.PopParent();
	setters89.Add(item43);
	IList<SetterBase> setters90 = style41.Setters;
	Setter setter91 = new Setter();
	setter91.Property = CSpan.BorderThicknessProperty;
	setter91.Value = new Thickness(1.0, 1.0, 1.0, 1.0);
	setters90.Add(setter91);
	IList<SetterBase> setters91 = style41.Setters;
	Setter setter92 = new Setter();
	setter92.Property = CSpan.CornerRadiusProperty;
	setter92.Value = new CornerRadius(5.0, 5.0, 5.0, 5.0);
	setters91.Add(setter92);
	IList<SetterBase> setters92 = style41.Setters;
	Setter setter93 = new Setter();
	setter93.Property = CSpan.PaddingProperty;
	setter93.Value = new Thickness(3.0, 1.0, 3.0, 1.0);
	setters92.Add(setter93);
	IList<SetterBase> setters93 = style41.Setters;
	Setter setter94 = new Setter();
	setter94.Property = CSpan.PressedScaleProperty;
	setter94.Value = 0.98;
	setters93.Add(setter94);
	context.PopParent();
	styles2.Add(style40);
	Style style42 = (style2 = new Style());
	context.PushParent(style2);
	Style style43 = style2;
	style43.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CRun))
		.Class("channelMention_simple");
	IList<SetterBase> setters94 = style43.Setters;
	Setter item44 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter95 = setter;
	setter95.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension38 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value44 = dynamicResourceExtension38.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter95.Value = value44;
	context.PopParent();
	setters94.Add(item44);
	IList<SetterBase> setters95 = style43.Setters;
	Setter item45 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter96 = setter;
	setter96.Property = CInline.FontFamilyProperty;
	DynamicResourceExtension dynamicResourceExtension39 = new DynamicResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value45 = dynamicResourceExtension39.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter96.Value = value45;
	context.PopParent();
	setters95.Add(item45);
	IList<SetterBase> setters96 = style43.Setters;
	Setter setter97 = new Setter();
	setter97.Property = CInline.FontWeightProperty;
	setter97.Value = FontWeight.Medium;
	setters96.Add(setter97);
	context.PopParent();
	styles2.Add(style42);
	Style style44 = (style2 = new Style());
	context.PushParent(style2);
	Style style45 = style2;
	style45.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CHyperlink))
		.Class("externalLink");
	IList<SetterBase> setters97 = style45.Setters;
	Setter item46 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter98 = setter;
	setter98.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension40 = new DynamicResourceExtension("Link");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value46 = dynamicResourceExtension40.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter98.Value = value46;
	context.PopParent();
	setters97.Add(item46);
	IList<SetterBase> setters98 = style45.Setters;
	Setter item47 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter99 = setter;
	setter99.Property = CInline.FontFamilyProperty;
	DynamicResourceExtension dynamicResourceExtension41 = new DynamicResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value47 = dynamicResourceExtension41.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter99.Value = value47;
	context.PopParent();
	setters98.Add(item47);
	IList<SetterBase> setters99 = style45.Setters;
	Setter setter100 = new Setter();
	setter100.Property = CInline.FontWeightProperty;
	setter100.Value = FontWeight.Normal;
	setters99.Add(setter100);
	context.PopParent();
	styles2.Add(style44);
	Style style46 = (style2 = new Style());
	context.PushParent(style2);
	Style style47 = style2;
	style47.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CHyperlink))
		.Class("externalLink_simple");
	IList<SetterBase> setters100 = style47.Setters;
	Setter item48 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter101 = setter;
	setter101.Property = CInline.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension42 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value48 = dynamicResourceExtension42.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter101.Value = value48;
	context.PopParent();
	setters100.Add(item48);
	IList<SetterBase> setters101 = style47.Setters;
	Setter item49 = (setter = new Setter());
	context.PushParent(setter);
	Setter setter102 = setter;
	setter102.Property = CInline.FontFamilyProperty;
	DynamicResourceExtension dynamicResourceExtension43 = new DynamicResourceExtension("RootFont");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value49 = dynamicResourceExtension43.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter102.Value = value49;
	context.PopParent();
	setters101.Add(item49);
	IList<SetterBase> setters102 = style47.Setters;
	Setter setter103 = new Setter();
	setter103.Property = CInline.FontWeightProperty;
	setter103.Value = (FontWeight)450;
	setters102.Add(setter103);
	IList<SetterBase> setters103 = style47.Setters;
	Setter setter104 = new Setter();
	setter104.Property = CInline.FontStyleProperty;
	setter104.Value = FontStyle.Italic;
	setters103.Add(setter104);
	context.PopParent();
	styles2.Add(style46);
	Style style48 = new Style();
	style48.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CHyperlink))
		.Class("externalLink")
		.Class(":pointerover");
	IList<SetterBase> setters104 = style48.Setters;
	Setter setter105 = new Setter();
	setter105.Property = CInline.IsUnderlineProperty;
	setter105.Value = true;
	setters104.Add(setter105);
	styles2.Add(style48);
	Style style49 = new Style();
	style49.Selector = ((Selector?)null).Class("RootMarkdown").Descendant().OfType(typeof(CHyperlink))
		.Class("externalLink_simple")
		.Class(":pointerover");
	IList<SetterBase> setters105 = style49.Setters;
	Setter setter106 = new Setter();
	setter106.Property = CInline.IsUnderlineProperty;
	setter106.Value = true;
	setters105.Add(setter106);
	styles2.Add(style49);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FRootImageLoader_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FRootImageLoader_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/RootImageLoader.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	style2.Selector = ((Selector?)null).OfType(typeof(RootImageLoader));
	Setter setter = new Setter();
	setter.Property = RootImageLoader.StretchProperty;
	setter.Value = Stretch.UniformToFill;
	style2.Add(setter);
	Setter setter3;
	Setter setter2 = (setter3 = new Setter());
	context.PushParent(setter3);
	setter3.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate;
	ControlTemplate value = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.TargetType = typeof(RootImageLoader);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_42.Build_1), context);
	context.PopParent();
	setter3.Value = value;
	context.PopParent();
	style2.Add(setter2);
	context.PopParent();
	styles2.Add(style);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FRootSplitView_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FRootSplitView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/RootSplitView.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	Style style3 = style2;
	style3.Selector = ((Selector?)null).OfType(typeof(RootSplitView));
	ResourceDictionary resourceDictionary = new ResourceDictionary();
	if (resourceDictionary is ResourceDictionary resourceDictionary2)
	{
		resourceDictionary2.EnsureCapacity(resourceDictionary2.Count + 2);
	}
	((IDictionary<object, object>)resourceDictionary).Add((object)"SplitViewOpenPaneThemeLength", (object)320.0);
	((IDictionary<object, object>)resourceDictionary).Add((object)"SplitViewCompactPaneThemeLength", (object)48.0);
	style3.Resources = resourceDictionary;
	Setter setter2;
	Setter setter = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter3 = setter2;
	setter3.Property = SplitView.OpenPaneLengthProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("SplitViewOpenPaneThemeLength");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter3.Value = value;
	context.PopParent();
	style3.Add(setter);
	Setter setter4 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter5 = setter2;
	setter5.Property = SplitView.CompactPaneLengthProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("SplitViewCompactPaneThemeLength");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value2 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter5.Value = value2;
	context.PopParent();
	style3.Add(setter4);
	Setter setter6 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter7 = setter2;
	setter7.Property = SplitView.PaneBackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("ThemeControlHighlightLowBrush");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value3 = dynamicResourceExtension3.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter7.Value = value3;
	context.PopParent();
	style3.Add(setter6);
	Style style4 = (style2 = new Style());
	context.PushParent(style2);
	Style style5 = style2;
	style5.Selector = ((Selector?)null).Nesting().Class(":left");
	Setter setter8 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter9 = setter2;
	setter9.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate;
	ControlTemplate value4 = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	ControlTemplate controlTemplate2 = controlTemplate;
	controlTemplate2.TargetType = typeof(RootSplitView);
	controlTemplate2.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_43.Build_1), context);
	context.PopParent();
	setter9.Value = value4;
	context.PopParent();
	style5.Add(setter8);
	context.PopParent();
	style3.Add(style4);
	Style style6 = (style2 = new Style());
	context.PushParent(style2);
	Style style7 = style2;
	style7.Selector = ((Selector?)null).Nesting().Class(":overlay").Class(":left")
		.Template()
		.OfType(typeof(Panel))
		.Name("PART_PaneRoot");
	Setter setter10 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter11 = setter2;
	setter11.Property = Layoutable.WidthProperty;
	CompiledBindingExtension obj = new CompiledBindingExtension
	{
		Path = new CompiledBindingPathBuilder(1).TemplatedParent().Property(SplitView.TemplateSettingsProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(SplitViewTemplateSettings.ClosedPaneWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build()
	};
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	CompiledBindingExtension value5 = obj.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter11.Value = value5;
	context.PopParent();
	style7.Add(setter10);
	Setter setter12 = new Setter();
	setter12.Property = Grid.ColumnSpanProperty;
	setter12.Value = 1;
	style7.Add(setter12);
	Setter setter13 = new Setter();
	setter13.Property = Grid.ColumnProperty;
	setter13.Value = 0;
	style7.Add(setter13);
	context.PopParent();
	style3.Add(style6);
	Style style8 = new Style();
	style8.Selector = ((Selector?)null).Nesting().Class(":overlay").Class(":left")
		.Template()
		.OfType(typeof(Panel))
		.Name("ContentRoot");
	Setter setter14 = new Setter();
	setter14.Property = Grid.ColumnProperty;
	setter14.Value = 1;
	style8.Add(setter14);
	Setter setter15 = new Setter();
	setter15.Property = Grid.ColumnSpanProperty;
	setter15.Value = 2;
	style8.Add(setter15);
	style3.Add(style8);
	Style style9 = (style2 = new Style());
	context.PushParent(style2);
	Style style10 = style2;
	style10.Selector = ((Selector?)null).Nesting().Class(":compactinline").Class(":left")
		.Template()
		.OfType(typeof(Panel))
		.Name("PART_PaneRoot");
	Setter setter16 = new Setter();
	setter16.Property = Grid.ColumnSpanProperty;
	setter16.Value = 1;
	style10.Add(setter16);
	Setter setter17 = new Setter();
	setter17.Property = Grid.ColumnProperty;
	setter17.Value = 0;
	style10.Add(setter17);
	Setter setter18 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter19 = setter2;
	setter19.Property = Layoutable.WidthProperty;
	CompiledBindingExtension obj2 = new CompiledBindingExtension
	{
		Path = new CompiledBindingPathBuilder(1).TemplatedParent().Property(SplitView.TemplateSettingsProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(SplitViewTemplateSettings.ClosedPaneWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build()
	};
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	CompiledBindingExtension value6 = obj2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter19.Value = value6;
	context.PopParent();
	style10.Add(setter18);
	context.PopParent();
	style3.Add(style9);
	Style style11 = new Style();
	style11.Selector = ((Selector?)null).Nesting().Class(":compactinline").Class(":left")
		.Template()
		.OfType(typeof(Panel))
		.Name("ContentRoot");
	Setter setter20 = new Setter();
	setter20.Property = Grid.ColumnProperty;
	setter20.Value = 1;
	style11.Add(setter20);
	Setter setter21 = new Setter();
	setter21.Property = Grid.ColumnSpanProperty;
	setter21.Value = 1;
	style11.Add(setter21);
	style3.Add(style11);
	Style style12 = (style2 = new Style());
	context.PushParent(style2);
	Style style13 = style2;
	style13.Selector = ((Selector?)null).Nesting().Class(":compactoverlay").Class(":left")
		.Template()
		.OfType(typeof(Panel))
		.Name("PART_PaneRoot");
	Setter setter22 = new Setter();
	setter22.Property = Grid.ColumnSpanProperty;
	setter22.Value = 1;
	style13.Add(setter22);
	Setter setter23 = new Setter();
	setter23.Property = Grid.ColumnProperty;
	setter23.Value = 0;
	style13.Add(setter23);
	Setter setter24 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter25 = setter2;
	setter25.Property = Layoutable.WidthProperty;
	CompiledBindingExtension obj3 = new CompiledBindingExtension
	{
		Path = new CompiledBindingPathBuilder(1).TemplatedParent().Property(SplitView.TemplateSettingsProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(SplitViewTemplateSettings.ClosedPaneWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build()
	};
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	CompiledBindingExtension value7 = obj3.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter25.Value = value7;
	context.PopParent();
	style13.Add(setter24);
	context.PopParent();
	style3.Add(style12);
	Style style14 = new Style();
	style14.Selector = ((Selector?)null).Nesting().Class(":compactoverlay").Class(":left")
		.Template()
		.OfType(typeof(Panel))
		.Name("ContentRoot");
	Setter setter26 = new Setter();
	setter26.Property = Grid.ColumnProperty;
	setter26.Value = 1;
	style14.Add(setter26);
	Setter setter27 = new Setter();
	setter27.Property = Grid.ColumnSpanProperty;
	setter27.Value = 1;
	style14.Add(setter27);
	style3.Add(style14);
	Style style15 = (style2 = new Style());
	context.PushParent(style2);
	Style style16 = style2;
	style16.Selector = ((Selector?)null).Nesting().Class(":inline").Class(":left")
		.Template()
		.OfType(typeof(Panel))
		.Name("PART_PaneRoot");
	Setter setter28 = new Setter();
	setter28.Property = Grid.ColumnSpanProperty;
	setter28.Value = 1;
	style16.Add(setter28);
	Setter setter29 = new Setter();
	setter29.Property = Grid.ColumnProperty;
	setter29.Value = 0;
	style16.Add(setter29);
	Setter setter30 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter31 = setter2;
	setter31.Property = Layoutable.WidthProperty;
	CompiledBindingExtension obj4 = new CompiledBindingExtension
	{
		Path = new CompiledBindingPathBuilder(1).TemplatedParent().Property(SplitView.TemplateSettingsProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(SplitViewTemplateSettings.ClosedPaneWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build()
	};
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	CompiledBindingExtension value8 = obj4.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter31.Value = value8;
	context.PopParent();
	style16.Add(setter30);
	context.PopParent();
	style3.Add(style15);
	Style style17 = new Style();
	style17.Selector = ((Selector?)null).Nesting().Class(":inline").Class(":left")
		.Template()
		.OfType(typeof(Panel))
		.Name("ContentRoot");
	Setter setter32 = new Setter();
	setter32.Property = Grid.ColumnProperty;
	setter32.Value = 1;
	style17.Add(setter32);
	Setter setter33 = new Setter();
	setter33.Property = Grid.ColumnSpanProperty;
	setter33.Value = 1;
	style17.Add(setter33);
	style3.Add(style17);
	Style style18 = (style2 = new Style());
	context.PushParent(style2);
	Style style19 = style2;
	style19.Selector = ((Selector?)null).Nesting().Class(":right");
	Setter setter34 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter35 = setter2;
	setter35.Property = TemplatedControl.TemplateProperty;
	ControlTemplate value9 = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	ControlTemplate controlTemplate3 = controlTemplate;
	controlTemplate3.TargetType = typeof(RootSplitView);
	controlTemplate3.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_43.Build_2), context);
	context.PopParent();
	setter35.Value = value9;
	context.PopParent();
	style19.Add(setter34);
	context.PopParent();
	style3.Add(style18);
	Style style20 = (style2 = new Style());
	context.PushParent(style2);
	Style style21 = style2;
	style21.Selector = ((Selector?)null).Nesting().Class(":overlay").Class(":right")
		.Template()
		.OfType(typeof(Panel))
		.Name("PART_PaneRoot");
	Setter setter36 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter37 = setter2;
	setter37.Property = Layoutable.WidthProperty;
	CompiledBindingExtension obj5 = new CompiledBindingExtension
	{
		Path = new CompiledBindingPathBuilder(1).TemplatedParent().Property(SplitView.TemplateSettingsProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(SplitViewTemplateSettings.ClosedPaneWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build()
	};
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	CompiledBindingExtension value10 = obj5.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter37.Value = value10;
	context.PopParent();
	style21.Add(setter36);
	Setter setter38 = new Setter();
	setter38.Property = Grid.ColumnSpanProperty;
	setter38.Value = 2;
	style21.Add(setter38);
	Setter setter39 = new Setter();
	setter39.Property = Grid.ColumnProperty;
	setter39.Value = 1;
	style21.Add(setter39);
	context.PopParent();
	style3.Add(style20);
	Style style22 = new Style();
	style22.Selector = ((Selector?)null).Nesting().Class(":overlay").Class(":right")
		.Template()
		.OfType(typeof(Panel))
		.Name("ContentRoot");
	Setter setter40 = new Setter();
	setter40.Property = Grid.ColumnProperty;
	setter40.Value = 0;
	style22.Add(setter40);
	Setter setter41 = new Setter();
	setter41.Property = Grid.ColumnSpanProperty;
	setter41.Value = 2;
	style22.Add(setter41);
	style3.Add(style22);
	Style style23 = (style2 = new Style());
	context.PushParent(style2);
	Style style24 = style2;
	style24.Selector = ((Selector?)null).Nesting().Class(":compactinline").Class(":right")
		.Template()
		.OfType(typeof(Panel))
		.Name("PART_PaneRoot");
	Setter setter42 = new Setter();
	setter42.Property = Grid.ColumnSpanProperty;
	setter42.Value = 1;
	style24.Add(setter42);
	Setter setter43 = new Setter();
	setter43.Property = Grid.ColumnProperty;
	setter43.Value = 1;
	style24.Add(setter43);
	Setter setter44 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter45 = setter2;
	setter45.Property = Layoutable.WidthProperty;
	CompiledBindingExtension obj6 = new CompiledBindingExtension
	{
		Path = new CompiledBindingPathBuilder(1).TemplatedParent().Property(SplitView.TemplateSettingsProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(SplitViewTemplateSettings.ClosedPaneWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build()
	};
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	CompiledBindingExtension value11 = obj6.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter45.Value = value11;
	context.PopParent();
	style24.Add(setter44);
	context.PopParent();
	style3.Add(style23);
	Style style25 = new Style();
	style25.Selector = ((Selector?)null).Nesting().Class(":compactinline").Class(":right")
		.Template()
		.OfType(typeof(Panel))
		.Name("ContentRoot");
	Setter setter46 = new Setter();
	setter46.Property = Grid.ColumnProperty;
	setter46.Value = 0;
	style25.Add(setter46);
	Setter setter47 = new Setter();
	setter47.Property = Grid.ColumnSpanProperty;
	setter47.Value = 1;
	style25.Add(setter47);
	style3.Add(style25);
	Style style26 = (style2 = new Style());
	context.PushParent(style2);
	Style style27 = style2;
	style27.Selector = ((Selector?)null).Nesting().Class(":compactoverlay").Class(":right")
		.Template()
		.OfType(typeof(Panel))
		.Name("PART_PaneRoot");
	Setter setter48 = new Setter();
	setter48.Property = Grid.ColumnSpanProperty;
	setter48.Value = 2;
	style27.Add(setter48);
	Setter setter49 = new Setter();
	setter49.Property = Grid.ColumnProperty;
	setter49.Value = 1;
	style27.Add(setter49);
	Setter setter50 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter51 = setter2;
	setter51.Property = Layoutable.WidthProperty;
	CompiledBindingExtension obj7 = new CompiledBindingExtension
	{
		Path = new CompiledBindingPathBuilder(1).TemplatedParent().Property(SplitView.TemplateSettingsProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(SplitViewTemplateSettings.ClosedPaneWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build()
	};
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	CompiledBindingExtension value12 = obj7.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter51.Value = value12;
	context.PopParent();
	style27.Add(setter50);
	context.PopParent();
	style3.Add(style26);
	Style style28 = new Style();
	style28.Selector = ((Selector?)null).Nesting().Class(":compactoverlay").Class(":right")
		.Template()
		.OfType(typeof(Panel))
		.Name("ContentRoot");
	Setter setter52 = new Setter();
	setter52.Property = Grid.ColumnProperty;
	setter52.Value = 0;
	style28.Add(setter52);
	Setter setter53 = new Setter();
	setter53.Property = Grid.ColumnSpanProperty;
	setter53.Value = 1;
	style28.Add(setter53);
	style3.Add(style28);
	Style style29 = (style2 = new Style());
	context.PushParent(style2);
	Style style30 = style2;
	style30.Selector = ((Selector?)null).Nesting().Class(":inline").Class(":right")
		.Template()
		.OfType(typeof(Panel))
		.Name("PART_PaneRoot");
	Setter setter54 = new Setter();
	setter54.Property = Grid.ColumnSpanProperty;
	setter54.Value = 1;
	style30.Add(setter54);
	Setter setter55 = new Setter();
	setter55.Property = Grid.ColumnProperty;
	setter55.Value = 1;
	style30.Add(setter55);
	Setter setter56 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter57 = setter2;
	setter57.Property = Layoutable.WidthProperty;
	CompiledBindingExtension obj8 = new CompiledBindingExtension
	{
		Path = new CompiledBindingPathBuilder(1).TemplatedParent().Property(SplitView.TemplateSettingsProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(SplitViewTemplateSettings.ClosedPaneWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build()
	};
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	CompiledBindingExtension value13 = obj8.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter57.Value = value13;
	context.PopParent();
	style30.Add(setter56);
	context.PopParent();
	style3.Add(style29);
	Style style31 = new Style();
	style31.Selector = ((Selector?)null).Nesting().Class(":inline").Class(":right")
		.Template()
		.OfType(typeof(Panel))
		.Name("ContentRoot");
	Setter setter58 = new Setter();
	setter58.Property = Grid.ColumnProperty;
	setter58.Value = 0;
	style31.Add(setter58);
	Setter setter59 = new Setter();
	setter59.Property = Grid.ColumnSpanProperty;
	setter59.Value = 1;
	style31.Add(setter59);
	style3.Add(style31);
	Style style32 = (style2 = new Style());
	context.PushParent(style2);
	Style style33 = style2;
	style33.Selector = ((Selector?)null).Nesting().Class(":open").Template()
		.OfType(typeof(Panel))
		.Name("PART_PaneRoot");
	Setter setter60 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter61 = setter2;
	setter61.Property = Layoutable.WidthProperty;
	CompiledBindingExtension obj9 = new CompiledBindingExtension
	{
		Path = new CompiledBindingPathBuilder(1).TemplatedParent().Property(SplitView.OpenPaneLengthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
	};
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	CompiledBindingExtension value14 = obj9.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter61.Value = value14;
	context.PopParent();
	style33.Add(setter60);
	context.PopParent();
	style3.Add(style32);
	Style style34 = new Style();
	style34.Selector = ((Selector?)null).Nesting().Class(":open").Template()
		.OfType(typeof(Rectangle))
		.Name("LightDismissLayer");
	Setter setter62 = new Setter();
	setter62.Property = Visual.OpacityProperty;
	setter62.Value = 1.0;
	style34.Add(setter62);
	style3.Add(style34);
	Style style35 = (style2 = new Style());
	context.PushParent(style2);
	Style style36 = style2;
	style36.Selector = ((Selector?)null).Nesting().Class(":closed").Template()
		.OfType(typeof(Panel))
		.Name("PART_PaneRoot");
	Setter setter63 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter64 = setter2;
	setter64.Property = Layoutable.WidthProperty;
	CompiledBindingExtension obj10 = new CompiledBindingExtension
	{
		Path = new CompiledBindingPathBuilder(1).TemplatedParent().Property(SplitView.TemplateSettingsProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(SplitViewTemplateSettings.ClosedPaneWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build()
	};
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	CompiledBindingExtension value15 = obj10.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter64.Value = value15;
	context.PopParent();
	style36.Add(setter63);
	context.PopParent();
	style3.Add(style35);
	Style style37 = new Style();
	style37.Selector = ((Selector?)null).Nesting().Class(":closed").Template()
		.OfType(typeof(Rectangle))
		.Name("LightDismissLayer");
	Setter setter65 = new Setter();
	setter65.Property = Visual.OpacityProperty;
	setter65.Value = 0.0;
	style37.Add(setter65);
	style3.Add(style37);
	Style style38 = new Style();
	style38.Selector = ((Selector?)null).Nesting().Template().OfType(typeof(Rectangle))
		.Name("LightDismissLayer");
	Setter setter66 = new Setter();
	setter66.Property = Visual.IsVisibleProperty;
	setter66.Value = false;
	style38.Add(setter66);
	Setter setter67 = new Setter();
	setter67.Property = Shape.FillProperty;
	setter67.Value = new ImmutableSolidColorBrush(16777215u);
	style38.Add(setter67);
	style3.Add(style38);
	Style style39 = (style2 = new Style());
	context.PushParent(style2);
	Style style40 = style2;
	style40.Selector = ((Selector?)null).Nesting().Class(":lightDismiss").Template()
		.OfType(typeof(Rectangle))
		.Name("LightDismissLayer");
	Setter setter68 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter69 = setter2;
	setter69.Property = Shape.FillProperty;
	SolidColorBrush solidColorBrush;
	SolidColorBrush value16 = (solidColorBrush = new SolidColorBrush());
	context.PushParent(solidColorBrush);
	StyledProperty<Color> colorProperty = SolidColorBrush.ColorProperty;
	DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("ThemeControlLowColor");
	context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
	IBinding binding = dynamicResourceExtension4.ProvideValue(context);
	context.ProvideTargetProperty = null;
	AvaloniaObjectExtensions.Bind(solidColorBrush, colorProperty, binding);
	solidColorBrush.SetValue(Brush.OpacityProperty, 0.6, BindingPriority.Template);
	context.PopParent();
	setter69.Value = value16;
	context.PopParent();
	style40.Add(setter68);
	context.PopParent();
	style3.Add(style39);
	Style style41 = new Style();
	style41.Selector = ((Selector?)null).Nesting().Class(":overlay").Class(":open")
		.Template()
		.OfType(typeof(Rectangle))
		.Name("LightDismissLayer");
	Setter setter70 = new Setter();
	setter70.Property = Visual.IsVisibleProperty;
	setter70.Value = true;
	style41.Add(setter70);
	style3.Add(style41);
	Style style42 = new Style();
	style42.Selector = ((Selector?)null).Nesting().Class(":compactoverlay").Class(":open")
		.Template()
		.OfType(typeof(Rectangle))
		.Name("LightDismissLayer");
	Setter setter71 = new Setter();
	setter71.Property = Visual.IsVisibleProperty;
	setter71.Value = true;
	style42.Add(setter71);
	style3.Add(style42);
	context.PopParent();
	styles2.Add(style);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FScrollViewer_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FScrollViewer_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/ScrollViewer.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	Style style3 = style2;
	style3.Selector = ((Selector?)null).OfType(typeof(RootScrollViewer));
	Setter setter = new Setter();
	setter.Property = TemplatedControl.BackgroundProperty;
	setter.Value = new ImmutableSolidColorBrush(16777215u);
	style3.Add(setter);
	Setter setter3;
	Setter setter2 = (setter3 = new Setter());
	context.PushParent(setter3);
	Setter setter4 = setter3;
	setter4.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate;
	ControlTemplate value = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_44.Build_1), context);
	context.PopParent();
	setter4.Value = value;
	context.PopParent();
	style3.Add(setter2);
	context.PopParent();
	styles2.Add(style);
	Style style4 = (style2 = new Style());
	context.PushParent(style2);
	Style style5 = style2;
	style5.Selector = ((Selector?)null).OfType(typeof(ScrollBar));
	Style style6 = (style2 = new Style());
	context.PushParent(style2);
	Style style7 = style2;
	style7.Selector = ((Selector?)null).Nesting().Class(":vertical");
	Setter setter5 = (setter3 = new Setter());
	context.PushParent(setter3);
	Setter setter6 = setter3;
	setter6.Property = TemplatedControl.TemplateProperty;
	ControlTemplate value2 = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_44.Build_2), context);
	context.PopParent();
	setter6.Value = value2;
	context.PopParent();
	style7.Add(setter5);
	context.PopParent();
	style5.Add(style6);
	Style style8 = (style2 = new Style());
	context.PushParent(style2);
	Style style9 = style2;
	style9.Selector = ((Selector?)null).Nesting().Class(":horizontal");
	Setter setter7 = (setter3 = new Setter());
	context.PushParent(setter3);
	Setter setter8 = setter3;
	setter8.Property = TemplatedControl.TemplateProperty;
	ControlTemplate value3 = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_44.Build_3), context);
	context.PopParent();
	setter8.Value = value3;
	context.PopParent();
	style9.Add(setter7);
	context.PopParent();
	style5.Add(style8);
	context.PopParent();
	styles2.Add(style4);
	Style style10 = (style2 = new Style());
	context.PushParent(style2);
	Style style11 = style2;
	style11.Selector = ((Selector?)null).OfType(typeof(ScrollBar)).Class(":vertical").Template()
		.OfType(typeof(RootScrollBarThumb));
	Setter setter9 = new Setter();
	setter9.Property = Visual.OpacityProperty;
	setter9.Value = 0.0;
	style11.Add(setter9);
	Setter setter10 = new Setter();
	setter10.Property = Layoutable.WidthProperty;
	setter10.Value = 4.0;
	style11.Add(setter10);
	Setter setter11 = new Setter();
	setter11.Property = TemplatedControl.CornerRadiusProperty;
	setter11.Value = new CornerRadius(2.0, 2.0, 2.0, 2.0);
	style11.Add(setter11);
	Setter setter12 = (setter3 = new Setter());
	context.PushParent(setter3);
	Setter setter13 = setter3;
	setter13.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value4 = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter13.Value = value4;
	context.PopParent();
	style11.Add(setter12);
	Setter setter14 = new Setter();
	setter14.Property = TemplatedControl.TemplateProperty;
	setter14.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_44.Build_4), context)
	};
	style11.Add(setter14);
	Style style12 = new Style();
	style12.Selector = ((Selector?)null).Nesting().Class(":parentIsMouseOver");
	Setter setter15 = new Setter();
	setter15.Property = Visual.OpacityProperty;
	setter15.Value = 0.1;
	style12.Add(setter15);
	style11.Add(style12);
	Style style13 = new Style();
	style13.Selector = ((Selector?)null).Nesting().Class(":pointerover");
	Setter setter16 = new Setter();
	setter16.Property = Visual.OpacityProperty;
	setter16.Value = 0.2;
	style13.Add(setter16);
	style11.Add(style13);
	Style style14 = new Style();
	style14.Selector = ((Selector?)null).Nesting().Class(":pressed");
	Setter setter17 = new Setter();
	setter17.Property = Visual.OpacityProperty;
	setter17.Value = 0.4;
	style14.Add(setter17);
	style11.Add(style14);
	Style style15 = new Style();
	style15.Selector = ((Selector?)null).Nesting().Class(":disabled");
	Setter setter18 = new Setter();
	setter18.Property = Visual.OpacityProperty;
	setter18.Value = 0.2;
	style15.Add(setter18);
	style11.Add(style15);
	context.PopParent();
	styles2.Add(style10);
	Style style16 = (style2 = new Style());
	context.PushParent(style2);
	Style style17 = style2;
	style17.Selector = ((Selector?)null).OfType(typeof(ScrollBar)).Class(":horizontal").Template()
		.OfType(typeof(RootScrollBarThumb));
	Setter setter19 = new Setter();
	setter19.Property = Visual.OpacityProperty;
	setter19.Value = 0.0;
	style17.Add(setter19);
	Setter setter20 = new Setter();
	setter20.Property = TemplatedControl.CornerRadiusProperty;
	setter20.Value = new CornerRadius(2.0, 2.0, 2.0, 2.0);
	style17.Add(setter20);
	Setter setter21 = (setter3 = new Setter());
	context.PushParent(setter3);
	Setter setter22 = setter3;
	setter22.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value5 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter22.Value = value5;
	context.PopParent();
	style17.Add(setter21);
	Setter setter23 = new Setter();
	setter23.Property = Layoutable.HeightProperty;
	setter23.Value = 4.0;
	style17.Add(setter23);
	Setter setter24 = new Setter();
	setter24.Property = TemplatedControl.TemplateProperty;
	setter24.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_44.Build_5), context)
	};
	style17.Add(setter24);
	Style style18 = new Style();
	style18.Selector = ((Selector?)null).Nesting().Class(":parentIsMouseOver");
	Setter setter25 = new Setter();
	setter25.Property = Visual.OpacityProperty;
	setter25.Value = 0.1;
	style18.Add(setter25);
	style17.Add(style18);
	Style style19 = new Style();
	style19.Selector = ((Selector?)null).Nesting().Class(":pointerover");
	Setter setter26 = new Setter();
	setter26.Property = Visual.OpacityProperty;
	setter26.Value = 0.2;
	style19.Add(setter26);
	style17.Add(style19);
	Style style20 = new Style();
	style20.Selector = ((Selector?)null).Nesting().Class(":pressed");
	Setter setter27 = new Setter();
	setter27.Property = Visual.OpacityProperty;
	setter27.Value = 0.4;
	style20.Add(setter27);
	style17.Add(style20);
	Style style21 = new Style();
	style21.Selector = ((Selector?)null).Nesting().Class(":disabled");
	Setter setter28 = new Setter();
	setter28.Property = Visual.OpacityProperty;
	setter28.Value = 0.2;
	style21.Add(setter28);
	style17.Add(style21);
	context.PopParent();
	styles2.Add(style16);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FSeparator_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FSeparator_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/Separator.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	style2.Selector = ((Selector?)null).OfType(typeof(Separator));
	Setter setter = new Setter();
	setter.Property = InputElement.FocusableProperty;
	setter.Value = false;
	style2.Add(setter);
	Setter setter3;
	Setter setter2 = (setter3 = new Setter());
	context.PushParent(setter3);
	setter3.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("Border");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter3.Value = value;
	context.PopParent();
	style2.Add(setter2);
	Setter setter4 = new Setter();
	setter4.Property = Layoutable.MarginProperty;
	setter4.Value = new Thickness(15.0, 5.0, 15.0, 5.0);
	style2.Add(setter4);
	Setter setter5 = new Setter();
	setter5.Property = Layoutable.HeightProperty;
	setter5.Value = 0.5;
	style2.Add(setter5);
	Setter setter6 = new Setter();
	setter6.Property = TemplatedControl.TemplateProperty;
	setter6.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_45.Build_1), context)
	};
	style2.Add(setter6);
	context.PopParent();
	styles2.Add(style);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FSlider_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FSlider_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/Slider.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	Style style3 = style2;
	style3.Selector = ((Selector?)null).OfType(typeof(Slider)).Class("RootSlider");
	Setter setter2;
	Setter setter = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter3 = setter2;
	setter3.Property = TemplatedControl.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BrandPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter3.Value = value;
	context.PopParent();
	style3.Add(setter);
	Setter setter4 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter5 = setter2;
	setter5.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Border");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value2 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter5.Value = value2;
	context.PopParent();
	style3.Add(setter4);
	Setter setter6 = new Setter();
	setter6.Property = Slider.TickFrequencyProperty;
	setter6.Value = 1.0;
	style3.Add(setter6);
	Setter setter7 = new Setter();
	setter7.Property = Slider.IsSnapToTickEnabledProperty;
	setter7.Value = true;
	style3.Add(setter7);
	Setter setter8 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter9 = setter2;
	setter9.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate;
	ControlTemplate value3 = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_46.Build_1), context);
	context.PopParent();
	setter9.Value = value3;
	context.PopParent();
	style3.Add(setter8);
	Style style4 = new Style();
	style4.Selector = ((Selector?)null).Nesting().Template().OfType(typeof(Track))
		.Name("PART_Track");
	Setter setter10 = new Setter();
	setter10.Property = Track.MinimumProperty;
	setter10.Value = new TemplateBinding(RangeBase.MinimumProperty).ProvideValue();
	style4.Add(setter10);
	Setter setter11 = new Setter();
	setter11.Property = Track.MaximumProperty;
	setter11.Value = new TemplateBinding(RangeBase.MaximumProperty).ProvideValue();
	style4.Add(setter11);
	Setter setter12 = new Setter();
	setter12.Property = Track.ValueProperty;
	setter12.Value = new TemplateBinding(RangeBase.ValueProperty)
	{
		Mode = BindingMode.TwoWay
	}.ProvideValue();
	style4.Add(setter12);
	style3.Add(style4);
	Style style5 = new Style();
	style5.Selector = ((Selector?)null).Nesting().Template().OfType(typeof(TickBar));
	Setter setter13 = new Setter();
	setter13.Property = TickBar.TicksProperty;
	setter13.Value = new TemplateBinding(Slider.TicksProperty).ProvideValue();
	style5.Add(setter13);
	style3.Add(style5);
	Style style6 = (style2 = new Style());
	context.PushParent(style2);
	Style style7 = style2;
	style7.Selector = ((Selector?)null).Nesting().Class(":disabled").Template()
		.OfType(typeof(Grid))
		.Name("grid");
	Setter setter14 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter15 = setter2;
	setter15.Property = Visual.OpacityProperty;
	DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("ThemeDisabledOpacity");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value4 = dynamicResourceExtension3.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter15.Value = value4;
	context.PopParent();
	style7.Add(setter14);
	context.PopParent();
	style3.Add(style6);
	context.PopParent();
	styles2.Add(style);
	Style style8 = (style2 = new Style());
	context.PushParent(style2);
	Style style9 = style2;
	style9.Selector = ((Selector?)null).OfType(typeof(RootPercentageSlider));
	Setter setter16 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter17 = setter2;
	setter17.Property = TemplatedControl.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("BrandPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value5 = dynamicResourceExtension4.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter17.Value = value5;
	context.PopParent();
	style9.Add(setter16);
	Setter setter18 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter19 = setter2;
	setter19.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("Border");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value6 = dynamicResourceExtension5.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter19.Value = value6;
	context.PopParent();
	style9.Add(setter18);
	Setter setter20 = new Setter();
	setter20.Property = Slider.TickFrequencyProperty;
	setter20.Value = 1.0;
	style9.Add(setter20);
	Setter setter21 = new Setter();
	setter21.Property = Slider.IsSnapToTickEnabledProperty;
	setter21.Value = true;
	style9.Add(setter21);
	Setter setter22 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter23 = setter2;
	setter23.Property = TemplatedControl.TemplateProperty;
	ControlTemplate value7 = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_46.Build_5), context);
	context.PopParent();
	setter23.Value = value7;
	context.PopParent();
	style9.Add(setter22);
	Style style10 = new Style();
	style10.Selector = ((Selector?)null).Nesting().Template().OfType(typeof(Track))
		.Name("PART_Track");
	Setter setter24 = new Setter();
	setter24.Property = Track.MinimumProperty;
	setter24.Value = new TemplateBinding(RangeBase.MinimumProperty).ProvideValue();
	style10.Add(setter24);
	Setter setter25 = new Setter();
	setter25.Property = Track.MaximumProperty;
	setter25.Value = new TemplateBinding(RangeBase.MaximumProperty).ProvideValue();
	style10.Add(setter25);
	Setter setter26 = new Setter();
	setter26.Property = Track.ValueProperty;
	setter26.Value = new TemplateBinding(RangeBase.ValueProperty)
	{
		Mode = BindingMode.TwoWay
	}.ProvideValue();
	style10.Add(setter26);
	style9.Add(style10);
	Style style11 = new Style();
	style11.Selector = ((Selector?)null).Nesting().Template().OfType(typeof(TickBar));
	Setter setter27 = new Setter();
	setter27.Property = TickBar.TicksProperty;
	setter27.Value = new TemplateBinding(Slider.TicksProperty).ProvideValue();
	style11.Add(setter27);
	style9.Add(style11);
	Style style12 = (style2 = new Style());
	context.PushParent(style2);
	Style style13 = style2;
	style13.Selector = ((Selector?)null).Nesting().Class(":disabled").Template()
		.OfType(typeof(Grid))
		.Name("grid");
	Setter setter28 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter29 = setter2;
	setter29.Property = Visual.OpacityProperty;
	DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("ThemeDisabledOpacity");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value8 = dynamicResourceExtension6.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter29.Value = value8;
	context.PopParent();
	style13.Add(setter28);
	context.PopParent();
	style9.Add(style12);
	context.PopParent();
	styles2.Add(style8);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FSvgButton_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FSvgButton_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/SvgButton.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	Style style3 = style2;
	style3.Selector = ((Selector?)null).OfType(typeof(RootSvgButton));
	Setter setter2;
	Setter setter = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter3 = setter2;
	setter3.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter3.Value = value;
	context.PopParent();
	style3.Add(setter);
	Setter setter4 = new Setter();
	setter4.Property = TemplatedControl.CornerRadiusProperty;
	setter4.Value = new CornerRadius(4.0, 4.0, 4.0, 4.0);
	style3.Add(setter4);
	Setter setter5 = new Setter();
	setter5.Property = TemplatedControl.BorderThicknessProperty;
	setter5.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style3.Add(setter5);
	Setter setter6 = new Setter();
	setter6.Property = RootSvgButton.SvgOpacityProperty;
	setter6.Value = 1.0;
	style3.Add(setter6);
	Setter setter7 = new Setter();
	setter7.Property = RootSvgButton.SvgBorderOpacityProperty;
	setter7.Value = 0.0;
	style3.Add(setter7);
	Setter setter8 = new Setter();
	setter8.Property = Visual.OpacityProperty;
	setter8.Value = 1.0;
	style3.Add(setter8);
	Setter setter9 = new Setter();
	setter9.Property = InputElement.FocusableProperty;
	setter9.Value = false;
	style3.Add(setter9);
	Setter setter10 = new Setter();
	setter10.Property = TemplatedControl.TemplateProperty;
	setter10.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_47.Build_1), context)
	};
	style3.Add(setter10);
	context.PopParent();
	styles2.Add(style);
	Style style4 = new Style();
	style4.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class(":pointerover");
	Setter setter11 = new Setter();
	setter11.Property = RootSvgButton.SvgBorderOpacityProperty;
	setter11.Value = 0.7;
	style4.Add(setter11);
	Setter setter12 = new Setter();
	setter12.Property = InputElement.CursorProperty;
	setter12.Value = new Cursor(StandardCursorType.Hand);
	style4.Add(setter12);
	styles2.Add(style4);
	Style style5 = new Style();
	style5.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class(":pressed");
	Setter setter13 = new Setter();
	setter13.Property = RootSvgButton.SvgOpacityProperty;
	setter13.Value = 0.5;
	style5.Add(setter13);
	Setter setter14 = new Setter();
	setter14.Property = RootSvgButton.SvgBorderOpacityProperty;
	setter14.Value = 0.5;
	style5.Add(setter14);
	styles2.Add(style5);
	Style style6 = (style2 = new Style());
	context.PushParent(style2);
	Style style7 = style2;
	style7.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("SvgDimmedButton");
	Setter setter15 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter16 = setter2;
	setter16.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value2 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter16.Value = value2;
	context.PopParent();
	style7.Add(setter15);
	Setter setter17 = new Setter();
	setter17.Property = TemplatedControl.CornerRadiusProperty;
	setter17.Value = new CornerRadius(5.0, 5.0, 5.0, 5.0);
	style7.Add(setter17);
	Setter setter18 = new Setter();
	setter18.Property = TemplatedControl.BorderThicknessProperty;
	setter18.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style7.Add(setter18);
	Setter setter19 = new Setter();
	setter19.Property = RootSvgButton.SvgOpacityProperty;
	setter19.Value = 0.6;
	style7.Add(setter19);
	Setter setter20 = new Setter();
	setter20.Property = RootSvgButton.SvgBorderOpacityProperty;
	setter20.Value = 0.0;
	style7.Add(setter20);
	Setter setter21 = new Setter();
	setter21.Property = Visual.OpacityProperty;
	setter21.Value = 1.0;
	style7.Add(setter21);
	Setter setter22 = new Setter();
	setter22.Property = InputElement.FocusableProperty;
	setter22.Value = false;
	style7.Add(setter22);
	Setter setter23 = new Setter();
	setter23.Property = TemplatedControl.TemplateProperty;
	setter23.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_47.Build_2), context)
	};
	style7.Add(setter23);
	context.PopParent();
	styles2.Add(style6);
	Style style8 = new Style();
	style8.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("SvgDimmedButton").Class(":pointerover");
	Setter setter24 = new Setter();
	setter24.Property = RootSvgButton.SvgOpacityProperty;
	setter24.Value = 1.0;
	style8.Add(setter24);
	Setter setter25 = new Setter();
	setter25.Property = RootSvgButton.SvgBorderOpacityProperty;
	setter25.Value = 0.7;
	style8.Add(setter25);
	Setter setter26 = new Setter();
	setter26.Property = InputElement.CursorProperty;
	setter26.Value = new Cursor(StandardCursorType.Hand);
	style8.Add(setter26);
	styles2.Add(style8);
	Style style9 = (style2 = new Style());
	context.PushParent(style2);
	Style style10 = style2;
	style10.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("SvgDimmedButton").Class(":pressed");
	Setter setter27 = new Setter();
	setter27.Property = RootSvgButton.SvgOpacityProperty;
	setter27.Value = 0.4;
	style10.Add(setter27);
	Setter setter28 = new Setter();
	setter28.Property = RootSvgButton.SvgBorderOpacityProperty;
	setter28.Value = 0.5;
	style10.Add(setter28);
	Setter setter29 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter30 = setter2;
	setter30.Property = Visual.RenderTransformProperty;
	setter30.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
	context.PopParent();
	style10.Add(setter29);
	context.PopParent();
	styles2.Add(style9);
	Style style11 = new Style();
	style11.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("Custom");
	Setter setter31 = new Setter();
	setter31.Property = TemplatedControl.BackgroundProperty;
	setter31.Value = new ImmutableSolidColorBrush(16777215u);
	style11.Add(setter31);
	Setter setter32 = new Setter();
	setter32.Property = TemplatedControl.BorderThicknessProperty;
	setter32.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style11.Add(setter32);
	Setter setter33 = new Setter();
	setter33.Property = InputElement.FocusableProperty;
	setter33.Value = false;
	style11.Add(setter33);
	Setter setter34 = new Setter();
	setter34.Property = TemplatedControl.TemplateProperty;
	setter34.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_47.Build_3), context)
	};
	style11.Add(setter34);
	styles2.Add(style11);
	Style style12 = new Style();
	style12.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("Custom").Class(":pointerover");
	Setter setter35 = new Setter();
	setter35.Property = Visual.OpacityProperty;
	setter35.Value = 0.7;
	style12.Add(setter35);
	Setter setter36 = new Setter();
	setter36.Property = InputElement.CursorProperty;
	setter36.Value = new Cursor(StandardCursorType.Hand);
	style12.Add(setter36);
	styles2.Add(style12);
	Style style13 = new Style();
	style13.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("Custom").Class(":pressed");
	Setter setter37 = new Setter();
	setter37.Property = Visual.OpacityProperty;
	setter37.Value = 0.5;
	style13.Add(setter37);
	styles2.Add(style13);
	Style style14 = new Style();
	style14.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("Custom").Class(":disabled");
	Setter setter38 = new Setter();
	setter38.Property = Visual.OpacityProperty;
	setter38.Value = 0.4;
	style14.Add(setter38);
	styles2.Add(style14);
	Style style15 = new Style();
	style15.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("CustomSvgDimmedButton");
	Setter setter39 = new Setter();
	setter39.Property = TemplatedControl.BackgroundProperty;
	setter39.Value = new ImmutableSolidColorBrush(16777215u);
	style15.Add(setter39);
	Setter setter40 = new Setter();
	setter40.Property = TemplatedControl.BorderThicknessProperty;
	setter40.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style15.Add(setter40);
	Setter setter41 = new Setter();
	setter41.Property = Visual.OpacityProperty;
	setter41.Value = 1.0;
	style15.Add(setter41);
	Setter setter42 = new Setter();
	setter42.Property = RootSvgButton.SvgOpacityProperty;
	setter42.Value = 0.6;
	style15.Add(setter42);
	Setter setter43 = new Setter();
	setter43.Property = InputElement.FocusableProperty;
	setter43.Value = false;
	style15.Add(setter43);
	Setter setter44 = new Setter();
	setter44.Property = TemplatedControl.TemplateProperty;
	setter44.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_47.Build_4), context)
	};
	style15.Add(setter44);
	styles2.Add(style15);
	Style style16 = new Style();
	style16.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("CustomSvgDimmedButton").Class(":pointerover");
	Setter setter45 = new Setter();
	setter45.Property = RootSvgButton.SvgOpacityProperty;
	setter45.Value = 1.0;
	style16.Add(setter45);
	Setter setter46 = new Setter();
	setter46.Property = InputElement.CursorProperty;
	setter46.Value = new Cursor(StandardCursorType.Hand);
	style16.Add(setter46);
	styles2.Add(style16);
	Style style17 = (style2 = new Style());
	context.PushParent(style2);
	Style style18 = style2;
	style18.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("CustomSvgDimmedButton").Class(":pressed");
	Setter setter47 = new Setter();
	setter47.Property = RootSvgButton.SvgOpacityProperty;
	setter47.Value = 0.4;
	style18.Add(setter47);
	Setter setter48 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter49 = setter2;
	setter49.Property = Visual.RenderTransformProperty;
	setter49.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
	context.PopParent();
	style18.Add(setter48);
	context.PopParent();
	styles2.Add(style17);
	Style style19 = new Style();
	style19.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("CustomSvgDimmedButton").Class(":disabled");
	Setter setter50 = new Setter();
	setter50.Property = Visual.OpacityProperty;
	setter50.Value = 0.4;
	style19.Add(setter50);
	styles2.Add(style19);
	Style style20 = new Style();
	style20.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("CustomSvgDimmedButtonSolid");
	Setter setter51 = new Setter();
	setter51.Property = TemplatedControl.BackgroundProperty;
	setter51.Value = new ImmutableSolidColorBrush(16777215u);
	style20.Add(setter51);
	Setter setter52 = new Setter();
	setter52.Property = TemplatedControl.BorderThicknessProperty;
	setter52.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style20.Add(setter52);
	Setter setter53 = new Setter();
	setter53.Property = Visual.OpacityProperty;
	setter53.Value = 1.0;
	style20.Add(setter53);
	Setter setter54 = new Setter();
	setter54.Property = InputElement.FocusableProperty;
	setter54.Value = false;
	style20.Add(setter54);
	Setter setter55 = new Setter();
	setter55.Property = TemplatedControl.TemplateProperty;
	setter55.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_47.Build_5), context)
	};
	style20.Add(setter55);
	styles2.Add(style20);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FTabItem_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FTabItem_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/TabItem.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	Style style3 = style2;
	style3.Selector = ((Selector?)null).OfType(typeof(TabItem));
	Setter setter2;
	Setter setter = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter3 = setter2;
	setter3.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate;
	ControlTemplate value = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_48.Build_1), context);
	context.PopParent();
	setter3.Value = value;
	context.PopParent();
	style3.Add(setter);
	Style style4 = (style2 = new Style());
	context.PushParent(style2);
	Style style5 = style2;
	style5.Selector = ((Selector?)null).Nesting().Class(":selected").Template()
		.OfType(typeof(TextBlock))
		.Name("PART_TextBlock");
	Setter setter4 = (setter2 = new Setter());
	context.PushParent(setter2);
	Setter setter5 = setter2;
	setter5.Property = TextBlock.ForegroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value2 = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter5.Value = value2;
	context.PopParent();
	style5.Add(setter4);
	Setter setter6 = new Setter();
	setter6.Property = TextBlock.FontWeightProperty;
	setter6.Value = FontWeight.Medium;
	style5.Add(setter6);
	context.PopParent();
	style3.Add(style4);
	Style style6 = new Style();
	style6.Selector = ((Selector?)null).Nesting().Class(":selected").Template()
		.OfType(typeof(Border))
		.Name("PART_SelectedPipe");
	Setter setter7 = new Setter();
	setter7.Property = Visual.IsVisibleProperty;
	setter7.Value = true;
	style6.Add(setter7);
	style3.Add(style6);
	Style style7 = new Style();
	style7.Selector = ((Selector?)null).Nesting().Class(":disabled").Template()
		.OfType(typeof(Grid))
		.Name("PART_Grid");
	Setter setter8 = new Setter();
	setter8.Property = Visual.IsVisibleProperty;
	setter8.Value = false;
	style7.Add(setter8);
	style3.Add(style7);
	context.PopParent();
	styles2.Add(style);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Tabalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FTabsControl_002Eaxaml(IServiceProvider P_0, ResourceDictionary P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FTabsControl_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/TabsControl.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	ResourceDictionary resourceDictionary2;
	ResourceDictionary resourceDictionary = (resourceDictionary2 = P_1);
	context.PushParent(resourceDictionary2);
	if (resourceDictionary2 is ResourceDictionary resourceDictionary3)
	{
		resourceDictionary3.EnsureCapacity(resourceDictionary3.Count + 2);
	}
	resourceDictionary2.AddDeferred(typeof(TabsControl), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_49.Build_1), context));
	resourceDictionary2.AddDeferred("HeaderOnlyTabsControl", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_49.Build_3), context));
	context.PopParent();
	if (resourceDictionary is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;

public static void Populate_003A_002FResources_002FStyles_002FTabsTheme_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FTabsTheme_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/TabsTheme.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	ResourceDictionary resourceDictionary;
	ResourceDictionary resources = (resourceDictionary = new ResourceDictionary());
	context.PushParent(resourceDictionary);
	resourceDictionary.MergedDictionaries.Add(Build_003A_002FResources_002FStyles_002FTabsControl_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
	resourceDictionary.MergedDictionaries.Add(Build_003A_002FResources_002FStyles_002FDragTabItem_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
	context.PopParent();
	styles2.Resources = resources;
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FTextButton_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FTextButton_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/TextButton.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Style style = new Style();
	style.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TextButton");
	Setter setter = new Setter();
	setter.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter.Value = HorizontalAlignment.Center;
	style.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate = new ControlTemplate();
	controlTemplate.TargetType = typeof(Button);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_50.Build_1), context);
	setter2.Value = controlTemplate;
	style.Add(setter2);
	P_1.Add(style);
	Style style2 = new Style();
	style2.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TextButton").Class(":pointerover");
	Setter setter3 = new Setter();
	setter3.Property = Visual.OpacityProperty;
	setter3.Value = 0.7;
	style2.Add(setter3);
	Setter setter4 = new Setter();
	setter4.Property = InputElement.CursorProperty;
	setter4.Value = new Cursor(StandardCursorType.Hand);
	style2.Add(setter4);
	P_1.Add(style2);
	Style style3 = new Style();
	style3.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TextButton").Class(":pressed");
	Setter setter5 = new Setter();
	setter5.Property = Visual.OpacityProperty;
	setter5.Value = 0.5;
	style3.Add(setter5);
	P_1.Add(style3);
	if (P_1 is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Controls;

public unsafe static void Populate_003A_002FResources_002FStyles_002FToolTip_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FToolTip_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/ToolTip.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	((ResourceDictionary)styles2.Resources).AddDeferred((object)"PlacementToBoolConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_51.Build_1), context));
	Style style2;
	Style style = (style2 = new Style());
	context.PushParent(style2);
	style2.Selector = ((Selector?)null).OfType(typeof(RootToolTip));
	Setter setter2;
	Setter setter = (setter2 = new Setter());
	context.PushParent(setter2);
	setter2.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate;
	ControlTemplate value = (controlTemplate = new ControlTemplate());
	context.PushParent(controlTemplate);
	controlTemplate.TargetType = typeof(RootToolTip);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_51.Build_2), context);
	context.PopParent();
	setter2.Value = value;
	context.PopParent();
	style2.Add(setter);
	context.PopParent();
	styles2.Add(style);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FTransparentButton_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FTransparentButton_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/TransparentButton.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	Style style = new Style();
	style.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TransparentButton");
	Setter setter = new Setter();
	setter.Property = TemplatedControl.BackgroundProperty;
	setter.Value = new ImmutableSolidColorBrush(16777215u);
	style.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = TemplatedControl.BorderBrushProperty;
	setter2.Value = new ImmutableSolidColorBrush(16777215u);
	style.Add(setter2);
	Setter setter3 = new Setter();
	setter3.Property = TemplatedControl.ForegroundProperty;
	setter3.Value = new ImmutableSolidColorBrush(16777215u);
	style.Add(setter3);
	Setter setter4 = new Setter();
	setter4.Property = InputElement.CursorProperty;
	setter4.Value = new Cursor(StandardCursorType.Hand);
	style.Add(setter4);
	Setter setter5 = new Setter();
	setter5.Property = Visual.OpacityProperty;
	setter5.Value = 0.0;
	style.Add(setter5);
	styles2.Add(style);
	Style style2 = new Style();
	style2.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TransparentButtonWithHighlight");
	Setter setter6 = new Setter();
	setter6.Property = InputElement.CursorProperty;
	setter6.Value = new Cursor(StandardCursorType.Hand);
	style2.Add(setter6);
	Setter setter7 = new Setter();
	setter7.Property = TemplatedControl.BorderThicknessProperty;
	setter7.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style2.Add(setter7);
	Setter setter8 = new Setter();
	setter8.Property = TemplatedControl.BorderBrushProperty;
	setter8.Value = new ImmutableSolidColorBrush(16777215u);
	style2.Add(setter8);
	Setter setter9 = new Setter();
	setter9.Property = TemplatedControl.BackgroundProperty;
	setter9.Value = new ImmutableSolidColorBrush(16777215u);
	style2.Add(setter9);
	Setter setter10 = new Setter();
	setter10.Property = TemplatedControl.TemplateProperty;
	setter10.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_52.Build_1), context)
	};
	style2.Add(setter10);
	styles2.Add(style2);
	Style style4;
	Style style3 = (style4 = new Style());
	context.PushParent(style4);
	Style style5 = style4;
	style5.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TransparentButtonWithHighlight").Class(":pointerover");
	Setter setter12;
	Setter setter11 = (setter12 = new Setter());
	context.PushParent(setter12);
	Setter setter13 = setter12;
	setter13.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HighlightNormal");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value = dynamicResourceExtension.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter13.Value = value;
	context.PopParent();
	style5.Add(setter11);
	context.PopParent();
	styles2.Add(style3);
	Style style6 = (style4 = new Style());
	context.PushParent(style4);
	Style style7 = style4;
	style7.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TransparentButtonWithHighlight").Class(":pressed");
	Setter setter14 = (setter12 = new Setter());
	context.PushParent(setter12);
	Setter setter15 = setter12;
	setter15.Property = TemplatedControl.BackgroundProperty;
	DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightLight");
	context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
	IBinding value2 = dynamicResourceExtension2.ProvideValue(context);
	context.ProvideTargetProperty = null;
	setter15.Value = value2;
	context.PopParent();
	style7.Add(setter14);
	Setter setter16 = (setter12 = new Setter());
	context.PushParent(setter12);
	Setter setter17 = setter12;
	setter17.Property = Visual.RenderTransformProperty;
	setter17.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
	context.PopParent();
	style7.Add(setter16);
	context.PopParent();
	styles2.Add(style6);
	Style style8 = new Style();
	style8.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TransparentButtonWithOpacity");
	Setter setter18 = new Setter();
	setter18.Property = InputElement.CursorProperty;
	setter18.Value = new Cursor(StandardCursorType.Hand);
	style8.Add(setter18);
	Setter setter19 = new Setter();
	setter19.Property = TemplatedControl.BorderThicknessProperty;
	setter19.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style8.Add(setter19);
	Setter setter20 = new Setter();
	setter20.Property = TemplatedControl.BorderBrushProperty;
	setter20.Value = new ImmutableSolidColorBrush(16777215u);
	style8.Add(setter20);
	Setter setter21 = new Setter();
	setter21.Property = TemplatedControl.BackgroundProperty;
	setter21.Value = new ImmutableSolidColorBrush(16777215u);
	style8.Add(setter21);
	Setter setter22 = new Setter();
	setter22.Property = TemplatedControl.PaddingProperty;
	setter22.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style8.Add(setter22);
	Setter setter23 = new Setter();
	setter23.Property = TemplatedControl.TemplateProperty;
	setter23.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_52.Build_2), context)
	};
	style8.Add(setter23);
	styles2.Add(style8);
	Style style9 = new Style();
	style9.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TransparentButtonWithOpacity").Class(":pointerover");
	Setter setter24 = new Setter();
	setter24.Property = Visual.OpacityProperty;
	setter24.Value = 0.75;
	style9.Add(setter24);
	styles2.Add(style9);
	Style style10 = (style4 = new Style());
	context.PushParent(style4);
	Style style11 = style4;
	style11.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TransparentButtonWithOpacity").Class(":pressed");
	Setter setter25 = new Setter();
	setter25.Property = Visual.OpacityProperty;
	setter25.Value = 0.5;
	style11.Add(setter25);
	Setter setter26 = (setter12 = new Setter());
	context.PushParent(setter12);
	Setter setter27 = setter12;
	setter27.Property = Visual.RenderTransformProperty;
	setter27.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.99)");
	context.PopParent();
	style11.Add(setter26);
	context.PopParent();
	styles2.Add(style10);
	Style style12 = new Style();
	style12.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TransparentButtonWithClickEffect");
	Setter setter28 = new Setter();
	setter28.Property = TemplatedControl.BorderThicknessProperty;
	setter28.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style12.Add(setter28);
	Setter setter29 = new Setter();
	setter29.Property = TemplatedControl.BorderBrushProperty;
	setter29.Value = new ImmutableSolidColorBrush(16777215u);
	style12.Add(setter29);
	Setter setter30 = new Setter();
	setter30.Property = TemplatedControl.BackgroundProperty;
	setter30.Value = new ImmutableSolidColorBrush(16777215u);
	style12.Add(setter30);
	Setter setter31 = new Setter();
	setter31.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter31.Value = HorizontalAlignment.Stretch;
	style12.Add(setter31);
	Setter setter32 = new Setter();
	setter32.Property = TemplatedControl.PaddingProperty;
	setter32.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style12.Add(setter32);
	Setter setter33 = new Setter();
	setter33.Property = InputElement.CursorProperty;
	setter33.Value = new Cursor(StandardCursorType.Hand);
	style12.Add(setter33);
	Setter setter34 = new Setter();
	setter34.Property = TemplatedControl.TemplateProperty;
	setter34.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_52.Build_3), context)
	};
	style12.Add(setter34);
	styles2.Add(style12);
	Style style13 = (style4 = new Style());
	context.PushParent(style4);
	Style style14 = style4;
	style14.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TransparentButtonWithClickEffect").Class(":pressed");
	Setter setter35 = (setter12 = new Setter());
	context.PushParent(setter12);
	Setter setter36 = setter12;
	setter36.Property = Visual.RenderTransformProperty;
	setter36.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
	context.PopParent();
	style14.Add(setter35);
	context.PopParent();
	styles2.Add(style13);
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}
