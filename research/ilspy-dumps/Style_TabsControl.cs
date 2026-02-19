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

public unsafe static void Populate_003A_002FResources_002FStyles_002FTabsControl_002Eaxaml(IServiceProvider P_0, ResourceDictionary P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FTabsControl_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/TabsControl.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	ResourceDictionary resourceDictionary2;
	ResourceDictionary resourceDictionary = (resourceDictionary2 = P_1);
	context.PushParent(resourceDictionary2);
	if (resourceDictionary2 is ResourceDictionary resourceDictionary3)
	{
		resourceDictionary3.EnsureCapacity(resourceDictionary3.Count + 2);
	}
	resourceDictionary2.AddDeferred(typeof(TabsControl), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_49.Build_1), context));
	resourceDictionary2.AddDeferred("HeaderOnlyTabsControl", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_49.Build_3), context));
	context.PopParent();
	if (resourceDictionary is StyledElement styledElement)
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
using Avalonia.Styling;
