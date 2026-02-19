// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;

public unsafe static void Populate_003A_002FResources_002FStyles_002FColorPicker_002FRootColorPicker_002Eaxaml(IServiceProvider P_0, Styles P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FStyles_002FColorPicker_002FRootColorPicker_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Styles/ColorPicker/RootColorPicker.axaml")
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
	if (resourceDictionary is ResourceDictionary resourceDictionary2)
	{
		resourceDictionary2.EnsureCapacity(resourceDictionary2.Count + 46);
	}
	resourceDictionary.AddDeferred(typeof(ColorSpectrum), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_1), context));
	resourceDictionary.AddDeferred("ColorControlCheckeredBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_3), context));
	resourceDictionary.AddDeferred("ColorControlLightSelectorBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_4), context));
	resourceDictionary.AddDeferred("ColorControlDarkSelectorBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_5), context));
	resourceDictionary.AddDeferred("ColorViewContentBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_6), context));
	resourceDictionary.AddDeferred("ColorViewContentBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_7), context));
	resourceDictionary.AddDeferred("ColorViewTabBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_8), context));
	resourceDictionary.AddDeferred("EnumToBoolConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_9), context));
	resourceDictionary.AddDeferred("ToBrushConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_10), context));
	resourceDictionary.AddDeferred("LeftCornerRadiusFilterConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_11), context));
	resourceDictionary.AddDeferred("RightCornerRadiusFilterConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_12), context));
	resourceDictionary.AddDeferred("TopCornerRadiusFilterConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_13), context));
	resourceDictionary.AddDeferred("BottomCornerRadiusFilterConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_14), context));
	resourceDictionary.AddDeferred("TopLeftCornerRadiusConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_15), context));
	resourceDictionary.AddDeferred("BottomRightCornerRadiusConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_16), context));
	resourceDictionary.AddDeferred("AccentColorConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_17), context));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorPreviewerAccentSectionWidth", (object)80.0);
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorPreviewerAccentSectionHeight", (object)40.0);
	resourceDictionary.AddDeferred(typeof(ColorPreviewer), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_18), context));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorSliderSize", (object)20.0);
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorSliderTrackSize", (object)20.0);
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorSliderCornerRadius", (object)new CornerRadius(10.0, 10.0, 10.0, 10.0));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorSliderTrackCornerRadius", (object)new CornerRadius(10.0, 10.0, 10.0, 10.0));
	resourceDictionary.AddDeferred("ColorSliderThumbTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_20), context));
	resourceDictionary.AddDeferred(typeof(ColorSlider), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_22), context));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorPickerFlyoutPlacement", (object)PlacementMode.BottomEdgeAlignedRight);
	resourceDictionary.AddDeferred("ColorToHexWithoutAlphaConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_29), context));
	resourceDictionary.AddDeferred("ColorToHexConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_30), context));
	resourceDictionary.AddDeferred("HexValidationConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_31), context));
	resourceDictionary.AddDeferred(typeof(ColorPicker), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_32), context));
	resourceDictionary.AddDeferred("ContrastBrushConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_38), context));
	resourceDictionary.AddDeferred("ColorToDisplayNameConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_39), context));
	resourceDictionary.AddDeferred("DoNothingForNullConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_40), context));
	resourceDictionary.AddDeferred("ColorViewComponentNumberFormat", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_41), context));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorViewTabStripHeight", (object)48.0);
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorViewComponentLabelWidth", (object)30.0);
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorViewComponentTextInputWidth", (object)80.0);
	resourceDictionary.AddDeferred("ColorViewSpectrumIconGeometry", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_42), context));
	resourceDictionary.AddDeferred("ColorViewPaletteIconGeometry", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_43), context));
	resourceDictionary.AddDeferred("ColorViewComponentsIconGeometry", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_44), context));
	((IDictionary<object, object>)resourceDictionary).Add((object)"ColorViewTabBackgroundCornerRadius", (object)new CornerRadius(3.0, 3.0, 3.0, 3.0));
	resourceDictionary.AddDeferred("ColorViewPaletteListBoxTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_45), context));
	resourceDictionary.AddDeferred("ColorViewPaletteListBoxItemTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_47), context));
	resourceDictionary.AddDeferred("ColorViewColorModelRadioButtonTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_49), context));
	resourceDictionary.AddDeferred("ColorViewTabItemTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_51), context));
	resourceDictionary.AddDeferred(typeof(ColorView), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_31.Build_53), context));
	context.PopParent();
	styles2.Resources = resources;
	context.PopParent();
	if (styles is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}

