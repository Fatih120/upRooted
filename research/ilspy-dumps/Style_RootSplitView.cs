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

