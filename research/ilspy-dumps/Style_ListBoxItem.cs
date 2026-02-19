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

