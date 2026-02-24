using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
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
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Converters.Tabs;
using RootApp.Client.Avalonia.Resources.Strings;
using Tabalonia.Controls;

namespace RootApp.Client.Avalonia.UI.Home;

public class HomeView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_134
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<HomeView> context = CreateContext(P_0);
			return new RightThumbWidthConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<HomeView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<HomeView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<HomeView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Home/HomeView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/HomeView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (HomeView)service;
				}
			}
			return context;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl MainView;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TabsControl TabControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder SystemTrayBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton FriendsButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton DirectMessagesButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton NotificationsButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock NotificationTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Button UserProfileButton;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	public HomeView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		TabControl.Items.CollectionChanged += onItemsCollectionChanged;
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		TabControl.Items.CollectionChanged -= onItemsCollectionChanged;
	}

	private void onItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		if (e.Action == NotifyCollectionChangedAction.Add)
		{
			TabControl.SelectedIndex = e.NewStartingIndex;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			!XamlIlPopulateTrampoline(this);
		}
		INameScope nameScope = this.FindNameScope();
		MainView = nameScope?.Find<UserControl>("MainView");
		TabControl = nameScope?.Find<TabsControl>("TabControl");
		SystemTrayBorder = nameScope?.Find<RootBorder>("SystemTrayBorder");
		FriendsButton = nameScope?.Find<RootSvgButton>("FriendsButton");
		DirectMessagesButton = nameScope?.Find<RootSvgButton>("DirectMessagesButton");
		NotificationsButton = nameScope?.Find<RootSvgButton>("NotificationsButton");
		NotificationTextBlock = nameScope?.Find<TextBlock>("NotificationTextBlock");
		UserProfileButton = nameScope?.Find<Button>("UserProfileButton");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, HomeView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<HomeView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<HomeView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Home/HomeView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/HomeView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Name = "MainView";
		object obj = P_1;
		context.AvaloniaNameScope.Register("MainView", obj);
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		resourceDictionary.AddDeferred("RightThumbWidthConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_134.Build_1), context));
		P_1.Resources = resourceDictionary;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(16.0, GridUnitType.Pixel)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Pixel)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(200.0, GridUnitType.Pixel)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(16.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(60.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children = grid5.Children;
		StreamerModeBanner streamerModeBanner2;
		StreamerModeBanner streamerModeBanner = (streamerModeBanner2 = new StreamerModeBanner());
		((ISupportInitialize)streamerModeBanner).BeginInit();
		children.Add(streamerModeBanner);
		StreamerModeBanner streamerModeBanner4;
		StreamerModeBanner streamerModeBanner3 = (streamerModeBanner4 = streamerModeBanner2);
		context.PushParent(streamerModeBanner4);
		Grid.SetRow(streamerModeBanner4, 0);
		Grid.SetColumnSpan(streamerModeBanner4, 6);
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.StreamerModeBannerViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = StyledElement.DataContextProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_6(streamerModeBanner4, compiledBindingExtension2);
		context.PopParent();
		((ISupportInitialize)streamerModeBanner3).EndInit();
		global::Avalonia.Controls.Controls children2 = grid5.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children2.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		Grid.SetRow(panel5, 0);
		Grid.SetColumnSpan(panel5, 6);
		StyledProperty<IBrush?> backgroundProperty = Panel.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Panel.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel5, backgroundProperty, binding);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension obj2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.UserInfoService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IUserInfoService,RootApp.Client.CoreDomain.SessionUser!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.User.SessionUser,RootApp.Client.CoreDomain.IsEmailVerified!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "false"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension3 = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel5, isVisibleProperty, compiledBindingExtension3);
		global::Avalonia.Controls.Controls children3 = panel5.Children;
		ThemeVariantScope themeVariantScope2;
		ThemeVariantScope themeVariantScope = (themeVariantScope2 = new ThemeVariantScope());
		((ISupportInitialize)themeVariantScope).BeginInit();
		children3.Add(themeVariantScope);
		ThemeVariantScope themeVariantScope4;
		ThemeVariantScope themeVariantScope3 = (themeVariantScope4 = themeVariantScope2);
		context.PushParent(themeVariantScope4);
		themeVariantScope4.RequestedThemeVariant = ThemeVariant.Dark;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		themeVariantScope4.Child = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.Margin = new Thickness(0.0, 5.0, 0.0, 5.0);
		stackPanel5.HorizontalAlignment = HorizontalAlignment.Center;
		stackPanel5.VerticalAlignment = VerticalAlignment.Center;
		stackPanel5.Spacing = 12.0;
		global::Avalonia.Controls.Controls children4 = stackPanel5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children4.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.VerifyEmailPrompt;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj3);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 12.0;
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding2);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children5 = stackPanel5.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children5.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.Classes.Add("BorderButton");
		button5.Padding = new Thickness(8.0, 4.0, 8.0, 4.0);
		button5.Height = 21.0;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj4 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_3(button5, obj4);
		button5.Background = new ImmutableSolidColorBrush(16777215u);
		StyledProperty<IBrush?> borderBrushProperty = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, borderBrushProperty, binding3);
		button5.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button5.VerticalAlignment = VerticalAlignment.Center;
		button5.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.ResendEmailCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty, compiledBindingExtension5);
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		button5.Content = stackPanel6;
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		global::Avalonia.Controls.Controls children6 = stackPanel4.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children6.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.SendVerificationEmail;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj5);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.FontSize = 12.0;
		textBlock9.LineHeight = 12.0;
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding4);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		global::Avalonia.Controls.Controls children7 = stackPanel5.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children7.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Classes.Add("BorderButton");
		button9.Padding = new Thickness(8.0, 4.0, 8.0, 4.0);
		button9.Height = 21.0;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj6 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_3(button9, obj6);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, backgroundProperty2, binding5);
		button9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button9.VerticalAlignment = VerticalAlignment.Center;
		button9.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.EnterVerificationCodeCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension7 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty2, compiledBindingExtension7);
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		button9.Content = textBlock10;
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.EnterCode;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj7 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj7);
		textBlock13.FontWeight = (FontWeight)450;
		textBlock13.FontSize = 12.0;
		textBlock13.LineHeight = 12.0;
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding6);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)themeVariantScope3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		global::Avalonia.Controls.Controls children8 = grid5.Children;
		TabsControl tabsControl2;
		TabsControl tabsControl = (tabsControl2 = new TabsControl());
		((ISupportInitialize)tabsControl).BeginInit();
		children8.Add(tabsControl);
		TabsControl tabsControl4;
		TabsControl tabsControl3 = (tabsControl4 = tabsControl2);
		context.PushParent(tabsControl4);
		tabsControl4.Name = "TabControl";
		obj = tabsControl4;
		context.AvaloniaNameScope.Register("TabControl", obj);
		Grid.SetColumnSpan(tabsControl4, 6);
		Grid.SetColumn(tabsControl4, 0);
		Grid.SetRow(tabsControl4, 1);
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("HeaderOnlyTabsControl");
		context.ProvideTargetProperty = StyledElement.ThemeProperty;
		object? obj8 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_28(tabsControl4, obj8);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.Tabs!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(tabsControl4, itemsSourceProperty, compiledBindingExtension9);
		tabsControl4.TabItemWidth = 200.0;
		StyledProperty<double> rightThumbWidthProperty = TabsControl.RightThumbWidthProperty;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.RightThumbWidth!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TabsControl.RightThumbWidthProperty;
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(tabsControl4, rightThumbWidthProperty, compiledBindingExtension11);
		tabsControl4.LeftThumbWidth = 8.0;
		DirectProperty<TabsControl, ICommand> addItemCommandProperty = TabsControl.AddItemCommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.CreateNewTabCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TabsControl.AddItemCommandProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(tabsControl4, addItemCommandProperty, compiledBindingExtension13);
		DirectProperty<TabsControl, ICommand> closeItemCommandProperty = TabsControl.CloseItemCommandProperty;
		CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.CloseTabCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TabsControl.CloseItemCommandProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(tabsControl4, closeItemCommandProperty, compiledBindingExtension15);
		DirectProperty<TabsControl, ICommand> moveRequestedCommandProperty = TabsControl.MoveRequestedCommandProperty;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.MoveRequestedCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TabsControl.MoveRequestedCommandProperty;
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(tabsControl4, moveRequestedCommandProperty, compiledBindingExtension17);
		CompiledBindingExtension obj9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.SelectedTabViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = SelectingItemsControl.SelectedItemProperty;
		CompiledBindingExtension compiledBindingExtension18 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_7(tabsControl4, compiledBindingExtension18);
		context.PopParent();
		((ISupportInitialize)tabsControl3).EndInit();
		global::Avalonia.Controls.Controls children9 = grid5.Children;
		RootSplitView rootSplitView2;
		RootSplitView rootSplitView = (rootSplitView2 = new RootSplitView());
		((ISupportInitialize)rootSplitView).BeginInit();
		children9.Add(rootSplitView);
		RootSplitView rootSplitView4;
		RootSplitView rootSplitView3 = (rootSplitView4 = rootSplitView2);
		context.PushParent(rootSplitView4);
		rootSplitView4.PanePlacement = SplitViewPanePlacement.Right;
		Grid.SetColumnSpan(rootSplitView4, 6);
		Grid.SetColumn(rootSplitView4, 0);
		Grid.SetRow(rootSplitView4, 2);
		StyledProperty<bool> isPaneOpenProperty = SplitView.IsPaneOpenProperty;
		CompiledBindingExtension obj10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.PaneOpen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = SplitView.IsPaneOpenProperty;
		CompiledBindingExtension compiledBindingExtension19 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSplitView4, isPaneOpenProperty, compiledBindingExtension19);
		StyledProperty<SplitViewDisplayMode> displayModeProperty = SplitView.DisplayModeProperty;
		CompiledBindingExtension compiledBindingExtension20 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.PaneDisplayService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Helpers.Panes.PaneDisplayService,RootApp.Client.Avalonia.GlobalPaneDisplayMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = SplitView.DisplayModeProperty;
		CompiledBindingExtension compiledBindingExtension21 = compiledBindingExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSplitView4, displayModeProperty, compiledBindingExtension21);
		StyledProperty<double> openPaneLengthProperty = SplitView.OpenPaneLengthProperty;
		CompiledBindingExtension compiledBindingExtension22 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.PaneWidth!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = SplitView.OpenPaneLengthProperty;
		CompiledBindingExtension compiledBindingExtension23 = compiledBindingExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSplitView4, openPaneLengthProperty, compiledBindingExtension23);
		rootSplitView4.PaneBackground = new ImmutableSolidColorBrush(16777215u);
		rootSplitView4.UseLightDismissOverlayMode = false;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		rootSplitView4.Pane = rootBorder;
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		RootBorder rootBorder5 = rootBorder4;
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, borderBrushProperty2, binding7);
		rootBorder5.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.0, 0.0);
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		rootBorder5.Child = border;
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty3, binding8);
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		border5.Child = contentControl;
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		ContentControl contentControl5 = contentControl4;
		CompiledBindingExtension compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.PaneViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension25 = compiledBindingExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl5, compiledBindingExtension25);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		ContentControl contentControl7;
		ContentControl contentControl6 = (contentControl7 = new ContentControl());
		((ISupportInitialize)contentControl6).BeginInit();
		rootSplitView4.Content = contentControl6;
		ContentControl contentControl8 = (contentControl4 = contentControl7);
		context.PushParent(contentControl4);
		ContentControl contentControl9 = contentControl4;
		CompiledBindingExtension compiledBindingExtension26 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.SelectedTabViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.ITabViewModel,RootApp.Client.Avalonia.ContentViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension27 = compiledBindingExtension26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl9, compiledBindingExtension27);
		context.PopParent();
		((ISupportInitialize)contentControl8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSplitView3).EndInit();
		global::Avalonia.Controls.Controls children10 = grid5.Children;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		children10.Add(rootBorder6);
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		rootBorder9.Name = "SystemTrayBorder";
		obj = rootBorder9;
		context.AvaloniaNameScope.Register("SystemTrayBorder", obj);
		Grid.SetRow(rootBorder9, 1);
		Grid.SetColumn(rootBorder9, 4);
		rootBorder9.Height = 44.0;
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, backgroundProperty4, binding9);
		StyledProperty<IBrush?> borderBrushProperty3 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty3, binding10);
		rootBorder9.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder9.CornerRadius = new CornerRadius(10.0, 10.0, 10.0, 10.0);
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		rootBorder9.Child = grid6;
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 6;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(16.0, GridUnitType.Pixel)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(16.0, GridUnitType.Pixel)));
		grid9.ColumnDefinitions = columnDefinitions;
		global::Avalonia.Controls.Controls children11 = grid9.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children11.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		Grid.SetColumn(panel9, 1);
		global::Avalonia.Controls.Controls children12 = panel9.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children12.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton5 = rootSvgButton4;
		rootSvgButton5.Classes.Add("Custom");
		rootSvgButton5.Name = "FriendsButton";
		obj = rootSvgButton5;
		context.AvaloniaNameScope.Register("FriendsButton", obj);
		rootSvgButton5.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgButton5.Width = 30.0;
		rootSvgButton5.Height = 30.0;
		StyledProperty<string> svgPathProperty = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("FriendsListSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, svgPathProperty, binding11);
		rootSvgButton5.SvgWidth = 24.0;
		rootSvgButton5.SvgHeight = 24.0;
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension28 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.FriendsPaneToggleCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension29 = compiledBindingExtension28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, commandProperty3, compiledBindingExtension29);
		ToolTip.SetPlacement(rootSvgButton5, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton5, 6.0);
		ToolTip.SetHorizontalOffset(rootSvgButton5, 0.0);
		ToolTip.SetShowDelay(rootSvgButton5, 0);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(rootSvgButton5, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip5 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip5, PlacementMode.Bottom);
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		rootToolTip5.Content = textBlock14;
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Friends;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj11 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock17, obj11);
		textBlock17.FontWeight = (FontWeight)450;
		textBlock17.FontSize = 14.0;
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock17.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		global::Avalonia.Controls.Controls children13 = panel9.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children13.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension30 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.FriendsOpen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension31 = compiledBindingExtension30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, isVisibleProperty2, compiledBindingExtension31);
		border9.VerticalAlignment = VerticalAlignment.Bottom;
		border9.Width = 24.0;
		border9.Height = 4.0;
		StyledProperty<IBrush?> backgroundProperty5 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty5, binding12);
		border9.CornerRadius = new CornerRadius(4.0, 4.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		global::Avalonia.Controls.Controls children14 = grid9.Children;
		Panel panel11;
		Panel panel10 = (panel11 = new Panel());
		((ISupportInitialize)panel10).BeginInit();
		children14.Add(panel10);
		Panel panel12 = (panel4 = panel11);
		context.PushParent(panel4);
		Panel panel13 = panel4;
		Grid.SetColumn(panel13, 2);
		global::Avalonia.Controls.Controls children15 = panel13.Children;
		RootSvgButton rootSvgButton7;
		RootSvgButton rootSvgButton6 = (rootSvgButton7 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton6).BeginInit();
		children15.Add(rootSvgButton6);
		RootSvgButton rootSvgButton8 = (rootSvgButton4 = rootSvgButton7);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton9 = rootSvgButton4;
		rootSvgButton9.Classes.Add("Custom");
		rootSvgButton9.Name = "DirectMessagesButton";
		obj = rootSvgButton9;
		context.AvaloniaNameScope.Register("DirectMessagesButton", obj);
		rootSvgButton9.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgButton9.Width = 30.0;
		rootSvgButton9.Height = 30.0;
		StyledProperty<string> svgPathProperty2 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("SystemTrayDirectMessagesSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, svgPathProperty2, binding13);
		rootSvgButton9.SvgWidth = 21.0;
		rootSvgButton9.SvgHeight = 20.0;
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension32 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.DirectMessagesPaneToggleCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension33 = compiledBindingExtension32.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, commandProperty4, compiledBindingExtension33);
		ToolTip.SetPlacement(rootSvgButton9, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton9, 6.0);
		ToolTip.SetHorizontalOffset(rootSvgButton9, 0.0);
		ToolTip.SetShowDelay(rootSvgButton9, 0);
		RootToolTip rootToolTip7;
		RootToolTip rootToolTip6 = (rootToolTip7 = new RootToolTip());
		((ISupportInitialize)rootToolTip6).BeginInit();
		ToolTip.SetTip(rootSvgButton9, rootToolTip6);
		RootToolTip rootToolTip8 = (rootToolTip4 = rootToolTip7);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip9 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip9, PlacementMode.Bottom);
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		rootToolTip9.Content = textBlock18;
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.DirectMessages;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj12 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock21, obj12);
		textBlock21.FontWeight = (FontWeight)450;
		textBlock21.FontSize = 14.0;
		textBlock21.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock21.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton8).EndInit();
		global::Avalonia.Controls.Controls children16 = panel13.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children16.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension34 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.DirectMessagesOpen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension35 = compiledBindingExtension34.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, isVisibleProperty3, compiledBindingExtension35);
		border13.VerticalAlignment = VerticalAlignment.Bottom;
		border13.Width = 24.0;
		border13.Height = 4.0;
		StyledProperty<IBrush?> backgroundProperty6 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty6, binding14);
		border13.CornerRadius = new CornerRadius(4.0, 4.0, 0.0, 0.0);
		border13.HorizontalAlignment = HorizontalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		global::Avalonia.Controls.Controls children17 = panel13.Children;
		Border border15;
		Border border14 = (border15 = new Border());
		((ISupportInitialize)border14).BeginInit();
		children17.Add(border14);
		Border border16 = (border4 = border15);
		context.PushParent(border4);
		Border border17 = border4;
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension36 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.DirectMessageService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IDirectMessageService,RootApp.Client.CoreDomain.UnreadDMCount!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension37 = compiledBindingExtension36.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, isVisibleProperty4, compiledBindingExtension37);
		border17.Margin = new Thickness(15.0, 0.0, 0.0, 20.0);
		border17.Height = 20.0;
		border17.MinWidth = 20.0;
		border17.IsHitTestVisible = false;
		StyledProperty<IBrush?> backgroundProperty7 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, backgroundProperty7, binding15);
		border17.BorderThickness = new Thickness(3.0, 3.0, 3.0, 3.0);
		StyledProperty<IBrush?> borderBrushProperty4 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, borderBrushProperty4, binding16);
		border17.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		border17.HorizontalAlignment = HorizontalAlignment.Center;
		border17.Padding = new Thickness(1.0, 0.0, 1.0, 0.0);
		TextBlock textBlock23;
		TextBlock textBlock22 = (textBlock23 = new TextBlock());
		((ISupportInitialize)textBlock22).BeginInit();
		border17.Child = textBlock22;
		TextBlock textBlock24 = (textBlock4 = textBlock23);
		context.PushParent(textBlock4);
		TextBlock textBlock25 = textBlock4;
		textBlock25.FontSize = 11.0;
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding17 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock25, foregroundProperty4, binding17);
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj13 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock25, obj13);
		textBlock25.FontWeight = FontWeight.Bold;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension obj14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.DirectMessageService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IDirectMessageService,RootApp.Client.CoreDomain.UnreadDMCount!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "0"
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension38 = obj14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock25, textProperty, compiledBindingExtension38);
		textBlock25.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock25.VerticalAlignment = VerticalAlignment.Center;
		textBlock25.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock24).EndInit();
		context.PopParent();
		((ISupportInitialize)border16).EndInit();
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
		global::Avalonia.Controls.Controls children18 = grid9.Children;
		Panel panel15;
		Panel panel14 = (panel15 = new Panel());
		((ISupportInitialize)panel14).BeginInit();
		children18.Add(panel14);
		Panel panel16 = (panel4 = panel15);
		context.PushParent(panel4);
		Panel panel17 = panel4;
		Grid.SetColumn(panel17, 3);
		global::Avalonia.Controls.Controls children19 = panel17.Children;
		RootSvgButton rootSvgButton11;
		RootSvgButton rootSvgButton10 = (rootSvgButton11 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton10).BeginInit();
		children19.Add(rootSvgButton10);
		RootSvgButton rootSvgButton12 = (rootSvgButton4 = rootSvgButton11);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton13 = rootSvgButton4;
		rootSvgButton13.Classes.Add("Custom");
		rootSvgButton13.Name = "NotificationsButton";
		obj = rootSvgButton13;
		context.AvaloniaNameScope.Register("NotificationsButton", obj);
		rootSvgButton13.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgButton13.Width = 30.0;
		rootSvgButton13.Height = 30.0;
		StyledProperty<string> svgPathProperty3 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("NotificationBellSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding18 = dynamicResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, svgPathProperty3, binding18);
		rootSvgButton13.SvgWidth = 15.17;
		rootSvgButton13.SvgHeight = 19.5;
		StyledProperty<ICommand?> commandProperty5 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension39 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.NotificationsPaneToggleCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension40 = compiledBindingExtension39.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, commandProperty5, compiledBindingExtension40);
		ToolTip.SetPlacement(rootSvgButton13, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton13, 6.0);
		ToolTip.SetHorizontalOffset(rootSvgButton13, 0.0);
		ToolTip.SetShowDelay(rootSvgButton13, 0);
		RootToolTip rootToolTip11;
		RootToolTip rootToolTip10 = (rootToolTip11 = new RootToolTip());
		((ISupportInitialize)rootToolTip10).BeginInit();
		ToolTip.SetTip(rootSvgButton13, rootToolTip10);
		RootToolTip rootToolTip12 = (rootToolTip4 = rootToolTip11);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip13 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip13, PlacementMode.Bottom);
		TextBlock textBlock27;
		TextBlock textBlock26 = (textBlock27 = new TextBlock());
		((ISupportInitialize)textBlock26).BeginInit();
		rootToolTip13.Content = textBlock26;
		TextBlock textBlock28 = (textBlock4 = textBlock27);
		context.PushParent(textBlock4);
		TextBlock textBlock29 = textBlock4;
		textBlock29.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Notifications;
		StaticResourceExtension staticResourceExtension10 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj15 = staticResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock29, obj15);
		textBlock29.FontWeight = (FontWeight)450;
		textBlock29.FontSize = 14.0;
		textBlock29.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock29.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock28).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton12).EndInit();
		global::Avalonia.Controls.Controls children20 = panel17.Children;
		Border border19;
		Border border18 = (border19 = new Border());
		((ISupportInitialize)border18).BeginInit();
		children20.Add(border18);
		Border border20 = (border4 = border19);
		context.PushParent(border4);
		Border border21 = border4;
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension41 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.NotificationsOpen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension42 = compiledBindingExtension41.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border21, isVisibleProperty5, compiledBindingExtension42);
		border21.VerticalAlignment = VerticalAlignment.Bottom;
		border21.Width = 24.0;
		border21.Height = 4.0;
		StyledProperty<IBrush?> backgroundProperty8 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension19 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding19 = dynamicResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border21, backgroundProperty8, binding19);
		border21.CornerRadius = new CornerRadius(4.0, 4.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)border20).EndInit();
		global::Avalonia.Controls.Controls children21 = panel17.Children;
		Border border23;
		Border border22 = (border23 = new Border());
		((ISupportInitialize)border22).BeginInit();
		children21.Add(border22);
		Border border24 = (border4 = border23);
		context.PushParent(border4);
		Border border25 = border4;
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension43 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.NotificationService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.INotificationService,RootApp.Client.CoreDomain.TotalUnviewedNotificationCount!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension44 = compiledBindingExtension43.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border25, isVisibleProperty6, compiledBindingExtension44);
		border25.Margin = new Thickness(15.0, 0.0, 0.0, 20.0);
		border25.Height = 20.0;
		border25.MinWidth = 20.0;
		border25.IsHitTestVisible = false;
		StyledProperty<IBrush?> backgroundProperty9 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension20 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding20 = dynamicResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border25, backgroundProperty9, binding20);
		border25.BorderThickness = new Thickness(3.0, 3.0, 3.0, 3.0);
		StyledProperty<IBrush?> borderBrushProperty5 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension21 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding21 = dynamicResourceExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border25, borderBrushProperty5, binding21);
		border25.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		border25.HorizontalAlignment = HorizontalAlignment.Center;
		border25.Padding = new Thickness(1.0, 0.0, 1.0, 0.0);
		TextBlock textBlock31;
		TextBlock textBlock30 = (textBlock31 = new TextBlock());
		((ISupportInitialize)textBlock30).BeginInit();
		border25.Child = textBlock30;
		TextBlock textBlock32 = (textBlock4 = textBlock31);
		context.PushParent(textBlock4);
		TextBlock textBlock33 = textBlock4;
		textBlock33.Name = "NotificationTextBlock";
		obj = textBlock33;
		context.AvaloniaNameScope.Register("NotificationTextBlock", obj);
		textBlock33.FontSize = 11.0;
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension22 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding22 = dynamicResourceExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock33, foregroundProperty5, binding22);
		StaticResourceExtension staticResourceExtension11 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj16 = staticResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock33, obj16);
		textBlock33.FontWeight = FontWeight.Bold;
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension obj17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.NotificationService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.INotificationService,RootApp.Client.CoreDomain.TotalUnviewedNotificationCount!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "0"
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension45 = obj17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock33, textProperty2, compiledBindingExtension45);
		textBlock33.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock33.VerticalAlignment = VerticalAlignment.Center;
		textBlock33.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock32).EndInit();
		context.PopParent();
		((ISupportInitialize)border24).EndInit();
		context.PopParent();
		((ISupportInitialize)panel16).EndInit();
		global::Avalonia.Controls.Controls children22 = grid9.Children;
		Panel panel19;
		Panel panel18 = (panel19 = new Panel());
		((ISupportInitialize)panel18).BeginInit();
		children22.Add(panel18);
		Panel panel20 = (panel4 = panel19);
		context.PushParent(panel4);
		Panel panel21 = panel4;
		Grid.SetColumn(panel21, 4);
		global::Avalonia.Controls.Controls children23 = panel21.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children23.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		rootImageLoader4.Width = 24.0;
		rootImageLoader4.Height = 24.0;
		rootImageLoader4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush?> backgroundProperty10 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension23 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding23 = dynamicResourceExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty10, binding23);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension46 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.ProfilePictureAsyncBitmapWrapper!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension47 = compiledBindingExtension46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension47);
		rootImageLoader4.LoadingPlaceholderSize = 16.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		global::Avalonia.Controls.Controls children24 = panel21.Children;
		Border border27;
		Border border26 = (border27 = new Border());
		((ISupportInitialize)border26).BeginInit();
		children24.Add(border26);
		Border border28 = (border4 = border27);
		context.PushParent(border4);
		Border border29 = border4;
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension48 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.ProfileOpen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension49 = compiledBindingExtension48.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border29, isVisibleProperty7, compiledBindingExtension49);
		border29.VerticalAlignment = VerticalAlignment.Bottom;
		border29.Width = 24.0;
		border29.Height = 4.0;
		StyledProperty<IBrush?> backgroundProperty11 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension24 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding24 = dynamicResourceExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border29, backgroundProperty11, binding24);
		border29.CornerRadius = new CornerRadius(4.0, 4.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)border28).EndInit();
		global::Avalonia.Controls.Controls children25 = panel21.Children;
		Button button11;
		Button button10 = (button11 = new Button());
		((ISupportInitialize)button10).BeginInit();
		children25.Add(button10);
		Button button12 = (button4 = button11);
		context.PushParent(button4);
		Button button13 = button4;
		button13.Name = "UserProfileButton";
		obj = button13;
		context.AvaloniaNameScope.Register("UserProfileButton", obj);
		button13.Classes.Add("TransparentButton");
		button13.Width = 30.0;
		button13.Height = 30.0;
		StyledProperty<ICommand?> commandProperty6 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension50 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.ProfilePaneToggleCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension51 = compiledBindingExtension50.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, commandProperty6, compiledBindingExtension51);
		ToolTip.SetPlacement(button13, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(button13, 6.0);
		ToolTip.SetHorizontalOffset(button13, 0.0);
		ToolTip.SetShowDelay(button13, 0);
		RootToolTip rootToolTip15;
		RootToolTip rootToolTip14 = (rootToolTip15 = new RootToolTip());
		((ISupportInitialize)rootToolTip14).BeginInit();
		ToolTip.SetTip(button13, rootToolTip14);
		RootToolTip rootToolTip16 = (rootToolTip4 = rootToolTip15);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip17 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip17, PlacementMode.Bottom);
		TextBlock textBlock35;
		TextBlock textBlock34 = (textBlock35 = new TextBlock());
		((ISupportInitialize)textBlock34).BeginInit();
		rootToolTip17.Content = textBlock34;
		TextBlock textBlock36 = (textBlock4 = textBlock35);
		context.PushParent(textBlock4);
		TextBlock textBlock37 = textBlock4;
		textBlock37.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Profile;
		StaticResourceExtension staticResourceExtension12 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj18 = staticResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock37, obj18);
		textBlock37.FontWeight = (FontWeight)450;
		textBlock37.FontSize = 14.0;
		textBlock37.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock37.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock36).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip16).EndInit();
		context.PopParent();
		((ISupportInitialize)button12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel20).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		global::Avalonia.Controls.Controls children26 = grid5.Children;
		Border border31;
		Border border30 = (border31 = new Border());
		((ISupportInitialize)border30).BeginInit();
		children26.Add(border30);
		Border border32 = (border4 = border31);
		context.PushParent(border4);
		Border border33 = border4;
		Grid.SetRow(border33, 3);
		Grid.SetColumn(border33, 0);
		Grid.SetColumnSpan(border33, 6);
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension53;
		CompiledBindingExtension compiledBindingExtension52 = (compiledBindingExtension53 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.VoiceCallConnectionStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build()));
		context.PushParent(compiledBindingExtension53);
		StaticResourceExtension staticResourceExtension13 = new StaticResourceExtension("MediaRoomConnectionStatusToVisibleConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj19 = staticResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension53.Converter = (IValueConverter)obj19;
		compiledBindingExtension53.FallbackValue = "false";
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension54 = compiledBindingExtension52.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border33, isVisibleProperty8, compiledBindingExtension54);
		ContentControl contentControl11;
		ContentControl contentControl10 = (contentControl11 = new ContentControl());
		((ISupportInitialize)contentControl10).BeginInit();
		border33.Child = contentControl10;
		ContentControl contentControl12 = (contentControl4 = contentControl11);
		context.PushParent(contentControl4);
		ContentControl contentControl13 = contentControl4;
		CompiledBindingExtension compiledBindingExtension55 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.HomeViewModel,RootApp.Client.Avalonia.VoiceBarViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension56 = compiledBindingExtension55.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl13, compiledBindingExtension56);
		context.PopParent();
		((ISupportInitialize)contentControl12).EndInit();
		context.PopParent();
		((ISupportInitialize)border32).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void !XamlIlPopulateTrampoline(HomeView P_0)
	{
		if (!XamlIlPopulateOverride != null)
		{
			!XamlIlPopulateOverride(P_0);
		}
		else
		{
			!XamlIlPopulate(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(null), P_0);
		}
	}
}
