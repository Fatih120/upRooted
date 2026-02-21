using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Converters.Tabs;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.NewTab;

public class NewTabContentView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_183
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<NewTabContentView> context = CreateContext(P_0);
			return new NewTabHorizontalAlignmentConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<NewTabContentView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<NewTabContentView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<NewTabContentView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FNewTab_002FNewTabContentView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/NewTab/NewTabContentView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (NewTabContentView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<NewTabContentView> context = CreateContext(P_0);
			context.IntermediateRoot = new NewTabFavoriteCommunityView();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<NewTabContentView> context = CreateContext(P_0);
			context.IntermediateRoot = new NewTabCommunityView();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder ContrastSeparatorBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootTextbox SearchTextBox;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private NewTabContentViewModel? _newTabContentViewModel => base.DataContext as NewTabContentViewModel;

	public NewTabContentView()
	{
		InitializeComponent();
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		setPaneDisplayMode();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		SearchTextBox.FocusTextBox();
	}

	private void setPaneDisplayMode()
	{
		executePaneDisplayModeCommand(SplitViewDisplayMode.Inline);
	}

	private void executePaneDisplayModeCommand(SplitViewDisplayMode P_0)
	{
		if (_newTabContentViewModel != null && _newTabContentViewModel.SetPaneDisplayModeCommand.CanExecute(P_0))
		{
			_newTabContentViewModel.SetPaneDisplayModeCommand.Execute(P_0);
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			_0021XamlIlPopulateTrampoline(this);
		}
		INameScope nameScope = this.FindNameScope();
		ContrastSeparatorBorder = nameScope?.Find<RootBorder>("ContrastSeparatorBorder");
		SearchTextBox = nameScope?.Find<RootTextbox>("SearchTextBox");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, NewTabContentView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<NewTabContentView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<NewTabContentView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FNewTab_002FNewTabContentView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/NewTab/NewTabContentView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		resourceDictionary.AddDeferred("NewTabHorizontalAlignmentConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_183.Build_1), context));
		P_1.Resources = resourceDictionary;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		P_1.Content = rootBorder;
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		RootBorder rootBorder5 = rootBorder4;
		rootBorder5.Name = "ContrastSeparatorBorder";
		object obj = rootBorder5;
		context.AvaloniaNameScope.Register("ContrastSeparatorBorder", obj);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, borderBrushProperty, binding);
		rootBorder5.DynamicBorderThickness = new Thickness(0.0, 0.5, 0.0, 0.0);
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		rootBorder5.Child = border;
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty, binding2);
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		border4.Child = rootScrollViewer;
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		rootScrollViewer4.BringIntoViewOnFocusChange = false;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootScrollViewer4.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.Margin = new Thickness(40.0, 40.0, 40.0, 40.0);
		grid4.MaxWidth = 956.0;
		StyledProperty<HorizontalAlignment> horizontalAlignmentProperty = Layoutable.HorizontalAlignmentProperty;
		CompiledBindingExtension compiledBindingExtension2;
		CompiledBindingExtension compiledBindingExtension = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunities_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_184_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build()));
		context.PushParent(compiledBindingExtension2);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("NewTabHorizontalAlignmentConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension2.Converter = (IValueConverter)obj2;
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.HorizontalAlignmentProperty;
		CompiledBindingExtension compiledBindingExtension3 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid4, horizontalAlignmentProperty, compiledBindingExtension3);
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children = grid4.Children;
		RootTextbox rootTextbox2;
		RootTextbox rootTextbox = (rootTextbox2 = new RootTextbox());
		((ISupportInitialize)rootTextbox).BeginInit();
		children.Add(rootTextbox);
		RootTextbox rootTextbox4;
		RootTextbox rootTextbox3 = (rootTextbox4 = rootTextbox2);
		context.PushParent(rootTextbox4);
		Grid.SetRow(rootTextbox4, 0);
		rootTextbox4.Name = "SearchTextBox";
		obj = rootTextbox4;
		context.AvaloniaNameScope.Register("SearchTextBox", obj);
		StyledProperty<string> textProperty = RootTextbox.TextProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002ESearchTerm_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension4 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, textProperty, compiledBindingExtension4);
		rootTextbox4.BorderHeight = 52.0;
		rootTextbox4.PlaceholderText = RootApp.Client.Avalonia.Resources.Strings.Resources.SearchCommunities;
		rootTextbox4.BorderCornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush> borderBackgroundBrushProperty = RootTextbox.BorderBackgroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = RootTextbox.BorderBackgroundBrushProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, borderBackgroundBrushProperty, binding3);
		StyledProperty<IBrush> borderBorderBrushProperty = RootTextbox.BorderBorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = RootTextbox.BorderBorderBrushProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, borderBorderBrushProperty, binding4);
		rootTextbox4.BorderBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootTextbox4.TextboxFontSize = 16.0;
		rootTextbox4.TextboxMargin = new Thickness(16.0, 0.0, 36.0, 0.0);
		StyledProperty<string> svgPathProperty = RootTextbox.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("SearchSVG");
		context.ProvideTargetProperty = RootTextbox.SvgPathProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, svgPathProperty, binding5);
		rootTextbox4.SvgWidth = 18.0;
		rootTextbox4.SvgHeight = 18.0;
		rootTextbox4.SvgMargin = new Thickness(0.0, 0.0, 16.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootTextbox3).EndInit();
		global::Avalonia.Controls.Controls children2 = grid4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children2.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetColumn(stackPanel5, 1);
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.Spacing = 12.0;
		stackPanel5.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		global::Avalonia.Controls.Controls children3 = stackPanel5.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children3.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Classes.Add("BorderButton");
		button4.Height = 52.0;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj4 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button4, obj4);
		button4.FontWeight = FontWeight.Medium;
		button4.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.CreateCommunity;
		StyledProperty<IBrush?> borderBrushProperty2 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, borderBrushProperty2, binding6);
		StyledProperty<IBrush?> foregroundProperty = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, foregroundProperty, binding7);
		button4.Background = new ImmutableSolidColorBrush(16777215u);
		button4.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button4.FontSize = 14.0;
		button4.CornerRadius = new CornerRadius(18.0, 18.0, 18.0, 18.0);
		button4.Padding = new Thickness(40.0, 0.0, 40.0, 0.0);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002ECreateCommunityCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, commandProperty, compiledBindingExtension6);
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		global::Avalonia.Controls.Controls children4 = stackPanel5.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children4.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		rootSvgButton4.Classes.Add("Custom");
		rootSvgButton4.Width = 52.0;
		rootSvgButton4.Height = 52.0;
		StyledProperty<string> svgPathProperty2 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("DiscoverSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, svgPathProperty2, binding8);
		rootSvgButton4.SvgWidth = 20.0;
		rootSvgButton4.SvgHeight = 20.0;
		rootSvgButton4.Background = new ImmutableSolidColorBrush(16777215u);
		StyledProperty<IBrush?> borderBrushProperty3 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, borderBrushProperty3, binding9);
		rootSvgButton4.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootSvgButton4.CornerRadius = new CornerRadius(18.0, 18.0, 18.0, 18.0);
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002EDiscoverVerifiedCommunitiesCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, commandProperty2, compiledBindingExtension8);
		ToolTip.SetPlacement(rootSvgButton4, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton4, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton4, 0.0);
		ToolTip.SetShowDelay(rootSvgButton4, 0);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(rootSvgButton4, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		ToolTip.SetPlacement(rootToolTip4, PlacementMode.Bottom);
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		rootToolTip4.Content = textBlock;
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.DiscoverCommunities;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj5);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 14.0;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		global::Avalonia.Controls.Controls children5 = grid4.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children5.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		Grid.SetRow(textBlock9, 1);
		Grid.SetColumn(textBlock9, 0);
		Grid.SetColumnSpan(textBlock9, 2);
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.FavoriteCommunities;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002EFavoriteCommunities_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_185_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, isVisibleProperty, compiledBindingExtension10);
		textBlock9.Margin = new Thickness(0.0, 40.0, 0.0, 12.0);
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.HorizontalAlignment = HorizontalAlignment.Left;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj6 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj6);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding10);
		textBlock9.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		global::Avalonia.Controls.Controls children6 = grid4.Children;
		ItemsRepeater itemsRepeater2;
		ItemsRepeater itemsRepeater = (itemsRepeater2 = new ItemsRepeater());
		((ISupportInitialize)itemsRepeater).BeginInit();
		children6.Add(itemsRepeater);
		ItemsRepeater itemsRepeater4;
		ItemsRepeater itemsRepeater3 = (itemsRepeater4 = itemsRepeater2);
		context.PushParent(itemsRepeater4);
		ItemsRepeater itemsRepeater5 = itemsRepeater4;
		Grid.SetRow(itemsRepeater5, 2);
		Grid.SetColumn(itemsRepeater5, 0);
		Grid.SetColumnSpan(itemsRepeater5, 2);
		DirectProperty<ItemsRepeater, IEnumerable?> itemsSourceProperty = ItemsRepeater.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002EFavoriteCommunities_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsRepeater.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsRepeater5, itemsSourceProperty, compiledBindingExtension12);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002EFavoriteCommunities_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_185_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsRepeater5, isVisibleProperty2, compiledBindingExtension14);
		UniformGridLayout uniformGridLayout = new UniformGridLayout();
		uniformGridLayout.MinColumnSpacing = 12.0;
		uniformGridLayout.MinRowSpacing = 12.0;
		uniformGridLayout.MinItemWidth = 179.0;
		uniformGridLayout.Orientation = Orientation.Horizontal;
		itemsRepeater5.Layout = uniformGridLayout;
		itemsRepeater5.ItemTemplate = new DataTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_183.Build_2), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsRepeater3).EndInit();
		global::Avalonia.Controls.Controls children7 = grid4.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children7.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		Grid.SetRow(textBlock13, 3);
		Grid.SetColumn(textBlock13, 0);
		Grid.SetColumnSpan(textBlock13, 2);
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.MyCommunities;
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunities_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_184_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, isVisibleProperty3, compiledBindingExtension16);
		textBlock13.Margin = new Thickness(0.0, 40.0, 0.0, 12.0);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		textBlock13.HorizontalAlignment = HorizontalAlignment.Left;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj7 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj7);
		textBlock13.FontWeight = (FontWeight)450;
		textBlock13.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding11);
		textBlock13.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		global::Avalonia.Controls.Controls children8 = grid4.Children;
		ItemsRepeater itemsRepeater7;
		ItemsRepeater itemsRepeater6 = (itemsRepeater7 = new ItemsRepeater());
		((ISupportInitialize)itemsRepeater6).BeginInit();
		children8.Add(itemsRepeater6);
		ItemsRepeater itemsRepeater8 = (itemsRepeater4 = itemsRepeater7);
		context.PushParent(itemsRepeater4);
		ItemsRepeater itemsRepeater9 = itemsRepeater4;
		Grid.SetRow(itemsRepeater9, 4);
		Grid.SetColumn(itemsRepeater9, 0);
		Grid.SetColumnSpan(itemsRepeater9, 2);
		DirectProperty<ItemsRepeater, IEnumerable?> itemsSourceProperty2 = ItemsRepeater.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunities_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsRepeater.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsRepeater9, itemsSourceProperty2, compiledBindingExtension18);
		UniformGridLayout uniformGridLayout2 = new UniformGridLayout();
		uniformGridLayout2.MinColumnSpacing = 12.0;
		uniformGridLayout2.MinRowSpacing = 12.0;
		uniformGridLayout2.MinItemWidth = 227.0;
		uniformGridLayout2.Orientation = Orientation.Horizontal;
		uniformGridLayout2.ItemsStretch = UniformGridLayoutItemsStretch.Fill;
		itemsRepeater9.Layout = uniformGridLayout2;
		itemsRepeater9.ItemTemplate = new DataTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_183.Build_3), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsRepeater8).EndInit();
		global::Avalonia.Controls.Controls children9 = grid4.Children;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		children9.Add(rootBorder6);
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		Grid.SetRow(rootBorder9, 4);
		Grid.SetColumn(rootBorder9, 0);
		Grid.SetColumnSpan(rootBorder9, 2);
		rootBorder9.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		StyledProperty<IBrush?> borderBrushProperty4 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty4, binding12);
		rootBorder9.Opacity = 0.1;
		rootBorder9.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootBorder9.Margin = new Thickness(0.0, 40.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj8 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding3.Converter = (IMultiValueConverter)obj8;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj9 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunities_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_184_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension obj10 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002EFavoriteCommunities_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_185_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item2 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootBorder9, isVisibleProperty4, multiBinding);
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		global::Avalonia.Controls.Controls children10 = grid4.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children10.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.VerticalAlignment = VerticalAlignment.Center;
		Grid.SetRow(stackPanel9, 4);
		Grid.SetColumn(stackPanel9, 0);
		Grid.SetColumnSpan(stackPanel9, 2);
		stackPanel9.MaxWidth = 340.0;
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj11 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding5.Converter = (IMultiValueConverter)obj11;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension obj12 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunities_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_184_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item3 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension obj13 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002EFavoriteCommunities_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_185_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item4 = obj13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(stackPanel9, isVisibleProperty5, multiBinding4);
		global::Avalonia.Controls.Controls children11 = stackPanel9.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children11.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		rootSvgImage4.Width = 78.0;
		rootSvgImage4.Height = 78.0;
		StyledProperty<string?> svgPathProperty3 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("EmptyStateSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty3, binding13);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		global::Avalonia.Controls.Controls children12 = stackPanel9.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children12.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.CommunitiesEmpty;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj14 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock17, obj14);
		textBlock17.FontWeight = FontWeight.Bold;
		textBlock17.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		textBlock17.FontSize = 24.0;
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty4, binding14);
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock17.HorizontalAlignment = HorizontalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		global::Avalonia.Controls.Controls children13 = stackPanel9.Children;
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		children13.Add(stackPanel10);
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		stackPanel13.Orientation = Orientation.Horizontal;
		stackPanel13.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
		stackPanel13.HorizontalAlignment = HorizontalAlignment.Center;
		global::Avalonia.Controls.Controls children14 = stackPanel13.Children;
		RootLinkButton rootLinkButton2;
		RootLinkButton rootLinkButton = (rootLinkButton2 = new RootLinkButton());
		((ISupportInitialize)rootLinkButton).BeginInit();
		children14.Add(rootLinkButton);
		RootLinkButton rootLinkButton4;
		RootLinkButton rootLinkButton3 = (rootLinkButton4 = rootLinkButton2);
		context.PushParent(rootLinkButton4);
		rootLinkButton4.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.CreateACommunity;
		rootLinkButton4.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty5 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("Link");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton4, foregroundProperty5, binding15);
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj15 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(rootLinkButton4, obj15);
		rootLinkButton4.FontWeight = (FontWeight)450;
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabContentViewModel_002CRootApp_002EClient_002EAvalonia_002ECreateCommunityCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton4, commandProperty3, compiledBindingExtension20);
		context.PopParent();
		((ISupportInitialize)rootLinkButton3).EndInit();
		global::Avalonia.Controls.Controls children15 = stackPanel13.Children;
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		children15.Add(textBlock18);
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ToGetStarted;
		StaticResourceExtension staticResourceExtension10 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj16 = staticResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock21, obj16);
		textBlock21.FontWeight = FontWeight.Medium;
		textBlock21.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty6 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, foregroundProperty6, binding16);
		textBlock21.Margin = new Thickness(4.0, 0.0, 0.0, 0.0);
		textBlock21.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(NewTabContentView P_0)
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
