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
