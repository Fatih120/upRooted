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
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FTextButton_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FTextButton_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/TextButton.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Style style = new Style();
	style.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TextButton");
	Setter setter = new Setter();
	setter.Property = ContentControl.HorizontalContentAlignmentProperty;
	setter.Value = HorizontalAlignment.Center;
	style.Add(setter);
	Setter setter2 = new Setter();
	setter2.Property = TemplatedControl.TemplateProperty;
	ControlTemplate controlTemplate = new ControlTemplate();
	controlTemplate.TargetType = typeof(Button);
	controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_50.Build_1), context);
	setter2.Value = controlTemplate;
	style.Add(setter2);
	P_1.Add(style);
	Style style2 = new Style();
	style2.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TextButton").Class(":pointerover");
	Setter setter3 = new Setter();
	setter3.Property = Visual.OpacityProperty;
	setter3.Value = 0.7;
	style2.Add(setter3);
	Setter setter4 = new Setter();
	setter4.Property = InputElement.CursorProperty;
	setter4.Value = new Cursor(StandardCursorType.Hand);
	style2.Add(setter4);
	P_1.Add(style2);
	Style style3 = new Style();
	style3.Selector = ((Selector?)null).OfType(typeof(Button)).Class("TextButton").Class(":pressed");
	Setter setter5 = new Setter();
	setter5.Property = Visual.OpacityProperty;
	setter5.Value = 0.5;
	style3.Add(setter5);
	P_1.Add(style3);
	if (P_1 is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

