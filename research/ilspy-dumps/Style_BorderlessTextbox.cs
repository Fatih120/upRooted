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

public static void Populate_003A_002FResources_002FStyles_002FBorderlessTextbox_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FBorderlessTextbox_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/BorderlessTextbox.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Style style = new Style();
	style.Selector = ((Selector?)null).OfType(typeof(TextBox)).Class("BorderlessTextBox");
	Setter setter = new Setter();
	setter.Property = TemplatedControl.BackgroundProperty;
	setter.Value = new ImmutableSolidColorBrush(16777215u);
	style.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = TemplatedControl.BorderThicknessProperty;
	setter2.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style.Add(setter2);
	Style style2 = new Style();
	style2.Selector = ((Selector?)null).Nesting().Template().OfType(typeof(Border))
		.Name("PART_BorderElement");
	Setter setter3 = new Setter();
	setter3.Property = Border.BackgroundProperty;
	setter3.Value = new ImmutableSolidColorBrush(16777215u);
	style2.Add(setter3);
	Setter setter4 = new Setter();
	setter4.Property = Border.BorderBrushProperty;
	setter4.Value = new ImmutableSolidColorBrush(16777215u);
	style2.Add(setter4);
	Setter setter5 = new Setter();
	setter5.Property = Border.BorderThicknessProperty;
	setter5.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
	style2.Add(setter5);
	style.Add(style2);
	P_1.Add(style);
	if (P_1 is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Controls;
