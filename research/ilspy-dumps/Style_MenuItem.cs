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
