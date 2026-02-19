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

