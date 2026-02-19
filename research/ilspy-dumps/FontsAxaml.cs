// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.XamlIl.Runtime;

public unsafe static void Populate_003A_002FResources_002FFonts_002Eaxaml(IServiceProvider P_0, ResourceDictionary P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FFonts_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Fonts.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	if (P_1 is ResourceDictionary resourceDictionary)
	{
		resourceDictionary.EnsureCapacity(resourceDictionary.Count + 2);
	}
	P_1.AddDeferred("RootFont", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_28.Build_1), context));
	P_1.AddDeferred("RootConsolas", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_28.Build_2), context));
	if (P_1 is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}
