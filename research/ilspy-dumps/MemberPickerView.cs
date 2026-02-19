// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberPickerView
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Members;

public class MemberPickerView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_78
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberPickerView> context = CreateContext(P_0);
			context.IntermediateRoot = new VirtualizingStackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<MemberPickerView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberPickerView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MemberPickerView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMemberPickerView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MemberPickerView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (MemberPickerView)service;
				}
			}
			return context;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootTextbox SearchTextbox;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public MemberPickerView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		SearchTextbox.FocusTextBox();
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			_0021XamlIlPopulateTrampoline(this);
		}
		SearchTextbox = this.FindNameScope()?.Find<RootTextbox>("SearchTextbox");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, MemberPickerView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MemberPickerView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MemberPickerView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMemberPickerView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MemberPickerView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.MaxHeight = 300.0;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		P_1.Content = rootBorder;
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty, binding);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding2);
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootBorder4.Margin = new Thickness(8.0, 8.0, 8.0, 8.0);
		StyledProperty<BoxShadows> boxShadowProperty = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, boxShadowProperty, binding3);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder4.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.MinWidth = 250.0;
		grid4.MaxHeight = 500.0;
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children = grid4.Children;
		RootTextbox rootTextbox2;
		RootTextbox rootTextbox = (rootTextbox2 = new RootTextbox());
		((ISupportInitialize)rootTextbox).BeginInit();
		children.Add(rootTextbox);
		RootTextbox rootTextbox4;
		RootTextbox rootTextbox3 = (rootTextbox4 = rootTextbox2);
		context.PushParent(rootTextbox4);
		rootTextbox4.Name = "SearchTextbox";
		object obj = rootTextbox4;
		context.AvaloniaNameScope.Register("SearchTextbox", obj);
		rootTextbox4.PlaceholderText = RootApp.Client.Avalonia.Resources.Strings.Resources.SearchMembers;
		StyledProperty<string> textProperty = RootTextbox.TextProperty;
		CompiledBindingExtension obj2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberPickerViewModel_002CRootApp_002EClient_002EAvalonia_002EFilterText_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, textProperty, compiledBindingExtension);
		rootTextbox4.Margin = new Thickness(8.0, 12.0, 8.0, 0.0);
		rootTextbox4.BorderHeight = 34.0;
		StyledProperty<IBrush> borderBackgroundBrushProperty = RootTextbox.BorderBackgroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = RootTextbox.BorderBackgroundBrushProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, borderBackgroundBrushProperty, binding4);
		rootTextbox4.BorderCornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush> borderBorderBrushProperty = RootTextbox.BorderBorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = RootTextbox.BorderBorderBrushProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, borderBorderBrushProperty, binding5);
		rootTextbox4.BorderBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootTextbox4.TextboxFontSize = 14.0;
		rootTextbox4.TextboxMargin = new Thickness(16.0, 0.0, 36.0, 0.0);
		StyledProperty<string> svgPathProperty = RootTextbox.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("SearchSVG");
		context.ProvideTargetProperty = RootTextbox.SvgPathProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, svgPathProperty, binding6);
		rootTextbox4.SvgWidth = 14.21;
		rootTextbox4.SvgHeight = 14.22;
		rootTextbox4.SvgMargin = new Thickness(0.0, 0.0, 16.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootTextbox3).EndInit();
		Controls children2 = grid4.Children;
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		children2.Add(rootScrollViewer);
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		Grid.SetRow(rootScrollViewer4, 1);
		rootScrollViewer4.Margin = new Thickness(-4.0, 0.0, -4.0, 0.0);
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		rootScrollViewer4.Content = itemsControl;
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberPickerViewModel_002CRootApp_002EClient_002EAvalonia_002EMembers_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension3 = compiledBindingExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl4, itemsSourceProperty, compiledBindingExtension3);
		itemsControl4.Margin = new Thickness(12.0, 12.0, 12.0, 12.0);
		itemsControl4.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_78.Build_1), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(MemberPickerView P_0)
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

