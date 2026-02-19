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

public unsafe static void Populate_003A_002FResources_002FStyles_002FLinkButton_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FLinkButton_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/LinkButton.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Style style = new Style();
	style.Selector = ((Selector?)null).OfType(typeof(RootLinkButton));
	Setter setter = new Setter();
	setter.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter.Value = HorizontalAlignment.Left;
	style.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = InputElement.FocusableProperty;
	setter2.Value = false;
	style.Add(setter2);
	Setter setter3 = new Setter();
	setter3.Property = TemplatedControl.TemplateProperty;
	setter3.Value = new ControlTemplate
	{
		Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_37.Build_1), context)
	};
	style.Add(setter3);
	P_1.Add(style);
	Style style2 = new Style();
	style2.Selector = ((Selector?)null).OfType(typeof(RootLinkButton)).Class(":pointerover").Template()
		.OfType(typeof(TextBlock));
	Setter setter4 = new Setter();
	setter4.Property = TextBlock.TextDecorationsProperty;
	setter4.Value = TextDecorations.Underline;
	style2.Add(setter4);
	P_1.Add(style2);
	Style style3 = new Style();
	style3.Selector = ((Selector?)null).OfType(typeof(RootLinkButton)).Class(":pointerover");
	Setter setter5 = new Setter();
	setter5.Property = Visual.OpacityProperty;
	setter5.Value = 0.7;
	style3.Add(setter5);
	Setter setter6 = new Setter();
	setter6.Property = InputElement.CursorProperty;
	setter6.Value = new Cursor(StandardCursorType.Hand);
	style3.Add(setter6);
	P_1.Add(style3);
	Style style4 = new Style();
	style4.Selector = ((Selector?)null).OfType(typeof(RootLinkButton)).Class(":pressed");
	Setter setter7 = new Setter();
	setter7.Property = Visual.OpacityProperty;
	setter7.Value = 0.5;
	style4.Add(setter7);
	P_1.Add(style4);
	if (P_1 is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

