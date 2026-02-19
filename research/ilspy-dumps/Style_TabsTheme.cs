// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;

public static void Populate_003A_002FResources_002FStyles_002FTabsTheme_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FTabsTheme_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/TabsTheme.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	Styles styles2;
	Styles styles = (styles2 = P_1);
	context.PushParent(styles2);
	ResourceDictionary resourceDictionary;
	ResourceDictionary resources = (resourceDictionary = new ResourceDictionary());
	context.PushParent(resourceDictionary);
	resourceDictionary.MergedDictionaries.Add(Build_003A_002FResources_002FStyles_002FTabsControl_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
	resourceDictionary.MergedDictionaries.Add(Build_003A_002FResources_002FStyles_002FDragTabItem_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
	context.PopParent();
	styles2.Resources = resources;
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

