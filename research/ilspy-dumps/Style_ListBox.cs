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

