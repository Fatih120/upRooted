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

