// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.Settings.MenuItemPageContainerView
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Resources.Converters.Settings;

public class MenuItemPageContainerView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_17
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MenuItemPageContainerView> context = CreateContext(P_0);
			return new MenuItemFontWeightConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<MenuItemPageContainerView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MenuItemPageContainerView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MenuItemPageContainerView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FSettings_002FMenuItemPageContainerView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/Settings/MenuItemPageContainerView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (MenuItemPageContainerView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MenuItemPageContainerView> context = CreateContext(P_0);
			return new MenuItemFontSizeConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MenuItemPageContainerView> context = CreateContext(P_0);
			return new MenuItemForegroundConverter();
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MenuItemPageContainerView> context = CreateContext(P_0);
			return new MenuItemMarginConverter();
		}
	}

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public MenuItemPageContainerView()
	{
		InitializeComponent();
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			_0021XamlIlPopulateTrampoline(this);
		}
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, MenuItemPageContainerView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MenuItemPageContainerView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MenuItemPageContainerView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FSettings_002FMenuItemPageContainerView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/Settings/MenuItemPageContainerView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		if (P_1.Resources is ResourceDictionary resourceDictionary)
		{
			resourceDictionary.EnsureCapacity(resourceDictionary.Count + 4);
		}
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"MenuItemFontWeightConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_17.Build_1), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"MenuItemFontSizeConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_17.Build_2), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"MenuItemForegroundConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_17.Build_3), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"MenuItemMarginConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_17.Build_4), context));
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		P_1.Content = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StyledProperty<Thickness> marginProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension2;
		CompiledBindingExtension compiledBindingExtension = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002EIsHeaderItem_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension3 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("MenuItemMarginConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension3.Converter = (IValueConverter)obj;
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel4, marginProperty, compiledBindingExtension4);
		stackPanel4.Orientation = Orientation.Horizontal;
		Controls children = stackPanel4.Children;
		Ellipse ellipse2;
		Ellipse ellipse = (ellipse2 = new Ellipse());
		((ISupportInitialize)ellipse).BeginInit();
		children.Add(ellipse);
		Ellipse ellipse4;
		Ellipse ellipse3 = (ellipse4 = ellipse2);
		context.PushParent(ellipse4);
		Ellipse ellipse5 = ellipse4;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse5, fillProperty, binding);
		ellipse5.Width = 8.0;
		ellipse5.Height = 8.0;
		ellipse5.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		ellipse5.HorizontalAlignment = HorizontalAlignment.Right;
		ellipse5.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002ENavigator_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EHelpers_002ENavigation_002ENavigator_002CRootApp_002EClient_002EAvalonia_002EHasPendingChanges_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse5, isVisibleProperty, compiledBindingExtension6);
		context.PopParent();
		((ISupportInitialize)ellipse3).EndInit();
		Controls children2 = stackPanel4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children2.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002EMenuTitle_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, textProperty, compiledBindingExtension8);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock4, obj2);
		StyledProperty<double> fontSizeProperty = TextBlock.FontSizeProperty;
		CompiledBindingExtension compiledBindingExtension9 = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002EIsHeaderItem_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("MenuItemFontSizeConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj3 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension10.Converter = (IValueConverter)obj3;
		context.PopParent();
		context.ProvideTargetProperty = TextBlock.FontSizeProperty;
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, fontSizeProperty, compiledBindingExtension11);
		textBlock4.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<FontWeight> fontWeightProperty = TextBlock.FontWeightProperty;
		CompiledBindingExtension compiledBindingExtension12 = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002EIsHeaderItem_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("MenuItemFontWeightConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj4 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension13.Converter = (IValueConverter)obj4;
		context.PopParent();
		context.ProvideTargetProperty = TextBlock.FontWeightProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, fontWeightProperty, compiledBindingExtension14);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("MenuItemForegroundConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj5 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding2.Converter = (IMultiValueConverter)obj5;
		IList<IBinding> bindings = multiBinding2.Bindings;
		CompiledBindingExtension obj6 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002EIsHeaderItem_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding2.Bindings;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension();
		compiledBindingExtension15.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension15.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item2 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, multiBinding);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children3 = stackPanel4.Children;
		Ellipse ellipse7;
		Ellipse ellipse6 = (ellipse7 = new Ellipse());
		((ISupportInitialize)ellipse6).BeginInit();
		children3.Add(ellipse6);
		Ellipse ellipse8 = (ellipse4 = ellipse7);
		context.PushParent(ellipse4);
		Ellipse ellipse9 = ellipse4;
		ellipse9.Fill = new ImmutableSolidColorBrush(4283096704u);
		ellipse9.Width = 8.0;
		ellipse9.Height = 8.0;
		ellipse9.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
		ellipse9.HorizontalAlignment = HorizontalAlignment.Left;
		ellipse9.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002EShowUpdateIndicator_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse9, isVisibleProperty2, compiledBindingExtension17);
		context.PopParent();
		((ISupportInitialize)ellipse8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(MenuItemPageContainerView P_0)
	{
		if (_0021XamlIlPopulateOverride != null)
		{
			_0021XamlIlPopulateOverride(P_0);
		}
		else
		{
			_0021XamlIlPopulate(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(null), P_0);
		}
	}
}

