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
