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
