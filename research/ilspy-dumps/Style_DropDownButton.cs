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
