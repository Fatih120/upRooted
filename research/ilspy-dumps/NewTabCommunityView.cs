using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.NewTab;

public class NewTabCommunityView : UserControl
{
	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Button MainButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock NotificationTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton FavoriteButton;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public NewTabCommunityView()
	{
		InitializeComponent();
	}

	private void onMainBorderPointerEntered(object? sender, PointerEventArgs e)
	{
		FavoriteButton.IsVisible = true;
	}

	private void onMainBorderPointerExited(object? sender, PointerEventArgs e)
	{
		FavoriteButton.IsVisible = false;
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
		MainButton = nameScope?.Find<Button>("MainButton");
		NotificationTextBlock = nameScope?.Find<TextBlock>("NotificationTextBlock");
		FavoriteButton = nameScope?.Find<RootSvgButton>("FavoriteButton");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, NewTabCommunityView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<NewTabCommunityView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<NewTabCommunityView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FNewTab_002FNewTabCommunityView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/NewTab/NewTabCommunityView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		RenderOptions.SetBitmapInterpolationMode(P_1, BitmapInterpolationMode.MediumQuality);
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		P_1.Content = button;
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Classes.Add("ListBorderButton");
		button4.Name = "MainButton";
		object obj = button4;
		context.AvaloniaNameScope.Register("MainButton", obj);
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, backgroundProperty, binding);
		button4.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		button4.AddHandler(InputElement.PointerEnteredEvent, context.RootObject.onMainBorderPointerEntered);
		button4.AddHandler(InputElement.PointerExitedEvent, context.RootObject.onMainBorderPointerExited);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunitySelectedCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, commandProperty, compiledBindingExtension2);
		RootMenuFlyout rootMenuFlyout;
		RootMenuFlyout contextFlyout = (rootMenuFlyout = new RootMenuFlyout());
		context.PushParent(rootMenuFlyout);
		ItemCollection items = rootMenuFlyout.Items;
		MenuItem menuItem2;
		MenuItem menuItem = (menuItem2 = new MenuItem());
		((ISupportInitialize)menuItem).BeginInit();
		items.Add(menuItem);
		MenuItem menuItem4;
		MenuItem menuItem3 = (menuItem4 = menuItem2);
		context.PushParent(menuItem4);
		MenuItem menuItem5 = menuItem4;
		menuItem5.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.Open;
		StyledProperty<ICommand?> commandProperty2 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunitySelectedCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, commandProperty2, compiledBindingExtension4);
		context.PopParent();
		((ISupportInitialize)menuItem3).EndInit();
		ItemCollection items2 = rootMenuFlyout.Items;
		Separator separator2;
		Separator separator = (separator2 = new Separator());
		((ISupportInitialize)separator).BeginInit();
		items2.Add(separator);
		Separator separator4;
		Separator separator3 = (separator4 = separator2);
		context.PushParent(separator4);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EIsOwner_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(separator4, isVisibleProperty, compiledBindingExtension6);
		context.PopParent();
		((ISupportInitialize)separator3).EndInit();
		ItemCollection items3 = rootMenuFlyout.Items;
		MenuItem menuItem7;
		MenuItem menuItem6 = (menuItem7 = new MenuItem());
		((ISupportInitialize)menuItem6).BeginInit();
		items3.Add(menuItem6);
		MenuItem menuItem8 = (menuItem4 = menuItem7);
		context.PushParent(menuItem4);
		MenuItem menuItem9 = menuItem4;
		menuItem9.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.LeaveCommunity;
		menuItem9.Classes.Add("DeleteMenuItem");
		StyledProperty<ICommand?> commandProperty3 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ELeaveCommunityCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, commandProperty3, compiledBindingExtension8);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EIsOwner_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, isVisibleProperty2, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)menuItem8).EndInit();
		context.PopParent();
		button4.ContextFlyout = contextFlyout;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		button4.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.Margin = new Thickness(10.0, 10.0, 10.0, 10.0);
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(8.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children = grid5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		Grid.SetRow(grid9, 0);
		Grid.SetColumnSpan(grid9, 2);
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		global::Avalonia.Controls.Controls children2 = grid9.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children2.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		rootImageLoader4.Width = 60.0;
		rootImageLoader4.Height = 60.0;
		rootImageLoader4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EPictureHex_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty2, compiledBindingExtension12);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityPictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension14);
		rootImageLoader4.LoadingPlaceholderSize = 0.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		rootImageLoader4.HorizontalAlignment = HorizontalAlignment.Left;
		Grid.SetRowSpan(rootImageLoader4, 2);
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		global::Avalonia.Controls.Controls children3 = grid9.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children3.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		Grid.SetColumn(border5, 2);
		Grid.SetRow(border5, 0);
		border5.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty3, binding2);
		border5.Margin = new Thickness(4.0, 0.0, 0.0, 0.0);
		border5.VerticalAlignment = VerticalAlignment.Top;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		border5.Child = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.Margin = new Thickness(8.0, 4.0, 8.0, 4.0);
		global::Avalonia.Controls.Controls children4 = stackPanel5.Children;
		Ellipse ellipse2;
		Ellipse ellipse = (ellipse2 = new Ellipse());
		((ISupportInitialize)ellipse).BeginInit();
		children4.Add(ellipse);
		Ellipse ellipse4;
		Ellipse ellipse3 = (ellipse4 = ellipse2);
		context.PushParent(ellipse4);
		ellipse4.Width = 6.0;
		ellipse4.Height = 6.0;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("BrandSecondary");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse4, fillProperty, binding3);
		ellipse4.Margin = new Thickness(0.0, 0.0, 8.0, 0.0);
		context.PopParent();
		((ISupportInitialize)ellipse3).EndInit();
		global::Avalonia.Controls.Controls children5 = stackPanel5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children5.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EAttachedUserCount_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension16);
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		textBlock5.FontSize = 12.0;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj2);
		textBlock5.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding4);
		textBlock5.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children6 = grid9.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children6.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		Grid.SetColumn(border9, 3);
		Grid.SetRow(border9, 0);
		border9.Margin = new Thickness(6.0, 0.0, 0.0, 0.0);
		border9.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty4, binding5);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, borderBrushProperty, binding6);
		border9.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ENotifications_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ENotifications_002ENotificationContainer_002CRootApp_002EClient_002ECoreDomain_002EContainerUnviewedNotificationCount_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, isVisibleProperty3, compiledBindingExtension18);
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		border9.Child = textBlock6;
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Name = "NotificationTextBlock";
		obj = textBlock9;
		context.AvaloniaNameScope.Register("NotificationTextBlock", obj);
		textBlock9.FontSize = 12.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding7);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj3);
		textBlock9.FontWeight = FontWeight.Medium;
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ENotifications_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ENotifications_002ENotificationContainer_002CRootApp_002EClient_002ECoreDomain_002EContainerUnviewedNotificationCount_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			FallbackValue = "0"
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension19 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension19);
		textBlock9.Margin = new Thickness(6.0, 3.0, 6.0, 3.0);
		textBlock9.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		global::Avalonia.Controls.Controls children7 = grid9.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children7.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		Grid.SetColumn(border13, 1);
		Grid.SetColumnSpan(border13, 3);
		Grid.SetRow(border13, 1);
		border13.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		border13.Margin = new Thickness(0.0, 6.0, 0.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty5 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty5, binding8);
		border13.VerticalAlignment = VerticalAlignment.Top;
		border13.HorizontalAlignment = HorizontalAlignment.Right;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		border13.Child = textBlock10;
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Margin = new Thickness(8.0, 4.0, 8.0, 4.0);
		StyledProperty<string?> textProperty3 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension20 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EPrimaryRoleName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		compiledBindingExtension20.TargetNullValue = "@Everyone";
		compiledBindingExtension20.StringFormat = "@{0}";
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension21 = compiledBindingExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, textProperty3, compiledBindingExtension21);
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		textBlock13.FontSize = 12.0;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj5);
		textBlock13.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding9);
		textBlock13.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		global::Avalonia.Controls.Controls children8 = grid5.Children;
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		children8.Add(grid10);
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		Grid.SetRow(grid13, 2);
		Grid.SetColumn(grid13, 0);
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children9 = grid13.Children;
		Border border15;
		Border border14 = (border15 = new Border());
		((ISupportInitialize)border14).BeginInit();
		children9.Add(border14);
		Border border16 = (border4 = border15);
		context.PushParent(border4);
		Border border17 = border4;
		Grid.SetColumn(border17, 0);
		border17.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		border17.Margin = new Thickness(0.0, 0.0, 6.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty6 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, backgroundProperty6, binding10);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension22 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EIsOwner_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension23 = compiledBindingExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, isVisibleProperty4, compiledBindingExtension23);
		border17.VerticalAlignment = VerticalAlignment.Top;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		border17.Child = rootSvgImage;
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		rootSvgImage5.Margin = new Thickness(8.0, 4.0, 8.0, 4.0);
		rootSvgImage5.Width = 15.0;
		rootSvgImage5.Height = 15.0;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("OwnerSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty, binding11);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)border16).EndInit();
		global::Avalonia.Controls.Controls children10 = grid13.Children;
		Grid grid15;
		Grid grid14 = (grid15 = new Grid());
		((ISupportInitialize)grid14).BeginInit();
		children10.Add(grid14);
		Grid grid16 = (grid4 = grid15);
		context.PushParent(grid4);
		Grid grid17 = grid4;
		Grid.SetColumn(grid17, 1);
		grid17.HorizontalAlignment = HorizontalAlignment.Left;
		grid17.VerticalAlignment = VerticalAlignment.Center;
		grid17.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid17.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children11 = grid17.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children11.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		Grid.SetColumn(textBlock17, 0);
		StyledProperty<string?> textProperty4 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension25 = compiledBindingExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, textProperty4, compiledBindingExtension25);
		textBlock17.VerticalAlignment = VerticalAlignment.Center;
		textBlock17.HorizontalAlignment = HorizontalAlignment.Left;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj6 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock17, obj6);
		textBlock17.FontWeight = FontWeight.Bold;
		textBlock17.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty4, binding12);
		textBlock17.TextTrimming = TextTrimming.CharacterEllipsis;
		textBlock17.MaxWidth = 200.0;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		global::Avalonia.Controls.Controls children12 = grid17.Children;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		children12.Add(rootSvgImage6);
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		Grid.SetColumn(rootSvgImage9, 1);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension26 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EIsVerified_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension27 = compiledBindingExtension26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, isVisibleProperty5, compiledBindingExtension27);
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("VerifiedCommunitySVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty2, binding13);
		rootSvgImage9.Cursor = new Cursor(StandardCursorType.Hand);
		rootSvgImage9.Width = 16.0;
		rootSvgImage9.Height = 16.0;
		rootSvgImage9.Margin = new Thickness(2.0, 0.0, 0.0, 0.0);
		rootSvgImage9.VerticalAlignment = VerticalAlignment.Center;
		ToolTip.SetPlacement(rootSvgImage9, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgImage9, -2.0);
		ToolTip.SetShowDelay(rootSvgImage9, 300);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(rootSvgImage9, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip5 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip5, PlacementMode.Top);
		Grid grid19;
		Grid grid18 = (grid19 = new Grid());
		((ISupportInitialize)grid18).BeginInit();
		rootToolTip5.Content = grid18;
		Grid grid20 = (grid4 = grid19);
		context.PushParent(grid4);
		Grid grid21 = grid4;
		grid21.MaxWidth = 300.0;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 3;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(12.0, GridUnitType.Pixel)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid21.ColumnDefinitions = columnDefinitions;
		global::Avalonia.Controls.Controls children13 = grid21.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children13.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		rootSvgImage13.Width = 36.0;
		rootSvgImage13.Height = 36.0;
		StyledProperty<string?> svgPathProperty3 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("VerifiedCommunitySVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty3, binding14);
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		global::Avalonia.Controls.Controls children14 = grid21.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children14.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		Grid.SetColumn(stackPanel9, 2);
		stackPanel9.Spacing = 2.0;
		stackPanel9.VerticalAlignment = VerticalAlignment.Center;
		global::Avalonia.Controls.Controls children15 = stackPanel9.Children;
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		children15.Add(textBlock18);
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.VerifiedCommunity;
		textBlock21.HorizontalAlignment = HorizontalAlignment.Center;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj7 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock21, obj7);
		textBlock21.FontWeight = FontWeight.DemiBold;
		textBlock21.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, foregroundProperty5, binding15);
		textBlock21.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid20).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid16).EndInit();
		global::Avalonia.Controls.Controls children16 = grid13.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children16.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		rootSvgButton4.Name = "FavoriteButton";
		obj = rootSvgButton4;
		context.AvaloniaNameScope.Register("FavoriteButton", obj);
		rootSvgButton4.Margin = new Thickness(6.0, 0.0, 0.0, 0.0);
		Grid.SetColumn(rootSvgButton4, 2);
		rootSvgButton4.Classes.Add("CustomSvgDimmedButton");
		StyledProperty<string> svgPathProperty4 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("FavoriteSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, svgPathProperty4, binding16);
		rootSvgButton4.Width = 15.0;
		rootSvgButton4.Height = 15.0;
		rootSvgButton4.SvgWidth = 15.0;
		rootSvgButton4.SvgHeight = 15.0;
		rootSvgButton4.IsVisible = false;
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension28 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ENewTab_002ENewTabCommunityViewModel_002CRootApp_002EClient_002EAvalonia_002EFavoriteSelectedCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension29 = compiledBindingExtension28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, commandProperty4, compiledBindingExtension29);
		ToolTip.SetPlacement(rootSvgButton4, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton4, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton4, 0.0);
		ToolTip.SetShowDelay(rootSvgButton4, 0);
		RootToolTip rootToolTip7;
		RootToolTip rootToolTip6 = (rootToolTip7 = new RootToolTip());
		((ISupportInitialize)rootToolTip6).BeginInit();
		ToolTip.SetTip(rootSvgButton4, rootToolTip6);
		RootToolTip rootToolTip8 = (rootToolTip4 = rootToolTip7);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip9 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip9, PlacementMode.Top);
		TextBlock textBlock23;
		TextBlock textBlock22 = (textBlock23 = new TextBlock());
		((ISupportInitialize)textBlock22).BeginInit();
		rootToolTip9.Content = textBlock22;
		TextBlock textBlock24 = (textBlock4 = textBlock23);
		context.PushParent(textBlock4);
		TextBlock textBlock25 = textBlock4;
		textBlock25.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Favorite;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj8 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock25, obj8);
		textBlock25.FontWeight = (FontWeight)450;
		textBlock25.FontSize = 14.0;
		textBlock25.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock25.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock24).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(NewTabCommunityView P_0)
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
