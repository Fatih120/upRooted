// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.DirectMessageTabView
using System;
using System.CodeDom.Compiler;
using System.Collections;
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
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Avalonia.Threading;
using Avalonia.VisualTree;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home;
using Tabalonia.Controls;

public class DirectMessageTabView : UserControl
{
	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock DirectMessageNameTextBlock;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private DirectMessageTabViewModel _directMessageTabViewModel => (DirectMessageTabViewModel)base.DataContext;

	public DirectMessageTabView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		updateDirectMessageNameActivity();
		_directMessageTabViewModel.PropertyChanged += onDirectMessageTabViewModelPropertyChanged;
		DragTabItem dragTabItem = this.FindAncestorOfType<DragTabItem>();
		if (dragTabItem != null)
		{
			dragTabItem.ContextFlyout = base.ContextFlyout;
			dragTabItem.PointerPressed += onDragItemPointerPressed;
		}
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		_directMessageTabViewModel.PropertyChanged -= onDirectMessageTabViewModelPropertyChanged;
		DragTabItem dragTabItem = this.FindAncestorOfType<DragTabItem>();
		if (dragTabItem != null)
		{
			dragTabItem.PointerPressed -= onDragItemPointerPressed;
		}
	}

	private void onDirectMessageTabViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "HasActivity")
			{
				updateDirectMessageNameActivity();
			}
		});
	}

	private void onDragItemPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		if (e.GetCurrentPoint(this).Properties.IsMiddleButtonPressed && _directMessageTabViewModel.CloseTabCommand.CanExecute(null))
		{
			_directMessageTabViewModel.CloseTabCommand.Execute(null);
		}
	}

	private void updateDirectMessageNameActivity()
	{
		if (_directMessageTabViewModel.HasActivity)
		{
			DirectMessageNameTextBlock[!TemplatedControl.ForegroundProperty] = new DynamicResourceExtension("TextPrimary");
			DirectMessageNameTextBlock.FontWeight = FontWeight.DemiBold;
		}
		else
		{
			DirectMessageNameTextBlock[!TemplatedControl.ForegroundProperty] = new DynamicResourceExtension("TextSecondary");
			DirectMessageNameTextBlock.FontWeight = (FontWeight)450;
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
		DirectMessageNameTextBlock = this.FindNameScope()?.Find<TextBlock>("DirectMessageNameTextBlock");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, DirectMessageTabView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageTabView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageTabView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FHome_002FDirectMessageTabView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/DirectMessageTabView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		RenderOptions.SetBitmapInterpolationMode(P_1, BitmapInterpolationMode.MediumQuality);
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
		menuItem5.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CloseTab;
		StyledProperty<ICommand?> commandProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EDirectMessageTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseTabCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, commandProperty, compiledBindingExtension2);
		context.PopParent();
		((ISupportInitialize)menuItem3).EndInit();
		ItemCollection items2 = rootMenuFlyout.Items;
		MenuItem menuItem7;
		MenuItem menuItem6 = (menuItem7 = new MenuItem());
		((ISupportInitialize)menuItem6).BeginInit();
		items2.Add(menuItem6);
		MenuItem menuItem8 = (menuItem4 = menuItem7);
		context.PushParent(menuItem4);
		MenuItem menuItem9 = menuItem4;
		menuItem9.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.OpenInNewWindow;
		StyledProperty<ICommand?> commandProperty2 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EDirectMessageTabViewModel_002CRootApp_002EClient_002EAvalonia_002EPopoutTabCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, commandProperty2, compiledBindingExtension4);
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension();
		context.ProvideTargetProperty = MenuItem.CommandParameterProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_27(menuItem9, compiledBindingExtension6);
		context.PopParent();
		((ISupportInitialize)menuItem8).EndInit();
		ItemCollection items3 = rootMenuFlyout.Items;
		MenuItem menuItem11;
		MenuItem menuItem10 = (menuItem11 = new MenuItem());
		((ISupportInitialize)menuItem10).BeginInit();
		items3.Add(menuItem10);
		MenuItem menuItem12 = (menuItem4 = menuItem11);
		context.PushParent(menuItem4);
		MenuItem menuItem13 = menuItem4;
		menuItem13.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.MarkAsRead;
		StyledProperty<ICommand?> commandProperty3 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EDirectMessageTabViewModel_002CRootApp_002EClient_002EAvalonia_002EMarkAsReadCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, commandProperty3, compiledBindingExtension8);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EDirectMessageTabViewModel_002CRootApp_002EClient_002EAvalonia_002EHasActivity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, isVisibleProperty, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)menuItem12).EndInit();
		ItemCollection items4 = rootMenuFlyout.Items;
		Separator separator2;
		Separator separator = (separator2 = new Separator());
		((ISupportInitialize)separator).BeginInit();
		items4.Add(separator);
		((ISupportInitialize)separator2).EndInit();
		ItemCollection items5 = rootMenuFlyout.Items;
		MenuItem menuItem15;
		MenuItem menuItem14 = (menuItem15 = new MenuItem());
		((ISupportInitialize)menuItem14).BeginInit();
		items5.Add(menuItem14);
		MenuItem menuItem16 = (menuItem4 = menuItem15);
		context.PushParent(menuItem4);
		MenuItem menuItem17 = menuItem4;
		menuItem17.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.LeaveDirectMessage;
		menuItem17.Classes.Add("DeleteMenuItem");
		StyledProperty<ICommand?> commandProperty4 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EDirectMessageTabViewModel_002CRootApp_002EClient_002EAvalonia_002ELeaveConversationCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem17, commandProperty4, compiledBindingExtension12);
		context.PopParent();
		((ISupportInitialize)menuItem16).EndInit();
		context.PopParent();
		P_1.ContextFlyout = contextFlyout;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.Margin = new Thickness(6.0, 0.0, 6.0, 0.0);
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(6.0, GridUnitType.Pixel)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		Controls children = grid4.Children;
		Ellipse ellipse2;
		Ellipse ellipse = (ellipse2 = new Ellipse());
		((ISupportInitialize)ellipse).BeginInit();
		children.Add(ellipse);
		Ellipse ellipse4;
		Ellipse ellipse3 = (ellipse4 = ellipse2);
		context.PushParent(ellipse4);
		Ellipse ellipse5 = ellipse4;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse5, fillProperty, binding);
		ellipse5.Width = 6.0;
		ellipse5.Height = 6.0;
		ellipse5.Margin = new Thickness(-9.0, 0.0, 0.0, 0.0);
		Grid.SetColumn(ellipse5, 0);
		ellipse5.HorizontalAlignment = HorizontalAlignment.Left;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EDirectMessageTabViewModel_002CRootApp_002EClient_002EAvalonia_002EHasActivity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse5, isVisibleProperty2, compiledBindingExtension14);
		context.PopParent();
		((ISupportInitialize)ellipse3).EndInit();
		Controls children2 = grid4.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children2.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		Grid.SetColumn(border5, 0);
		border5.Width = 28.0;
		border5.Height = 28.0;
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty, binding2);
		border5.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		RootCircularPanel rootCircularPanel2;
		RootCircularPanel rootCircularPanel = (rootCircularPanel2 = new RootCircularPanel());
		((ISupportInitialize)rootCircularPanel).BeginInit();
		border5.Child = rootCircularPanel;
		RootCircularPanel rootCircularPanel4;
		RootCircularPanel rootCircularPanel3 = (rootCircularPanel4 = rootCircularPanel2);
		context.PushParent(rootCircularPanel4);
		StyledProperty<IEnumerable> itemsSourceProperty = RootCircularPanel.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EDirectMessageTabViewModel_002CRootApp_002EClient_002EAvalonia_002EMemberBadges_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootCircularPanel.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootCircularPanel4, itemsSourceProperty, compiledBindingExtension16);
		context.PopParent();
		((ISupportInitialize)rootCircularPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		Controls children3 = grid4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children3.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetColumn(stackPanel5, 2);
		stackPanel5.VerticalAlignment = VerticalAlignment.Center;
		Controls children4 = stackPanel5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children4.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension obj = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EDirectMessageTabViewModel_002CRootApp_002EClient_002EAvalonia_002EDirectMessageName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			FallbackValue = "Direct Message"
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension17 = obj.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension17);
		textBlock5.Name = "DirectMessageNameTextBlock";
		object obj2 = textBlock5;
		context.AvaloniaNameScope.Register("DirectMessageNameTextBlock", obj2);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj3);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 13.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding3);
		textBlock5.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children5 = stackPanel5.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children5.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.Margin = new Thickness(0.0, 1.0, 0.0, 0.0);
		Controls children6 = stackPanel9.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children6.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension18 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EDirectMessageTabViewModel_002CRootApp_002EClient_002EAvalonia_002ETab_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ETabs_002ETab_002CRootApp_002EClient_002ECoreDomain_002ENotifications_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ENotifications_002ENotificationContainer_002CRootApp_002EClient_002ECoreDomain_002EContainerUnviewedNotificationCount_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension19 = compiledBindingExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, isVisibleProperty3, compiledBindingExtension19);
		border9.Height = 14.0;
		border9.MinWidth = 14.0;
		border9.IsHitTestVisible = false;
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty2, binding4);
		border9.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		border9.Child = textBlock6;
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.FontSize = 11.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding5);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj4 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj4);
		textBlock9.FontWeight = FontWeight.Bold;
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension obj5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EDirectMessageTabViewModel_002CRootApp_002EClient_002EAvalonia_002ETab_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ETabs_002ETab_002CRootApp_002EClient_002ECoreDomain_002ENotifications_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ENotifications_002ENotificationContainer_002CRootApp_002EClient_002ECoreDomain_002EContainerUnviewedNotificationCount_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "0"
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension20 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension20);
		textBlock9.Margin = new Thickness(2.0, 0.0, 2.0, 0.0);
		textBlock9.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		Controls children7 = grid4.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children7.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Grid.SetColumn(panel4, 3);
		panel4.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EDirectMessageTabViewModel_002CRootApp_002EClient_002EAvalonia_002EIsOnCall_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel4, isVisibleProperty4, compiledBindingExtension22);
		Controls children8 = panel4.Children;
		ThemeVariantScope themeVariantScope2;
		ThemeVariantScope themeVariantScope = (themeVariantScope2 = new ThemeVariantScope());
		((ISupportInitialize)themeVariantScope).BeginInit();
		children8.Add(themeVariantScope);
		ThemeVariantScope themeVariantScope4;
		ThemeVariantScope themeVariantScope3 = (themeVariantScope4 = themeVariantScope2);
		context.PushParent(themeVariantScope4);
		themeVariantScope4.RequestedThemeVariant = ThemeVariant.Light;
		Ellipse ellipse7;
		Ellipse ellipse6 = (ellipse7 = new Ellipse());
		((ISupportInitialize)ellipse6).BeginInit();
		themeVariantScope4.Child = ellipse6;
		Ellipse ellipse8 = (ellipse4 = ellipse7);
		context.PushParent(ellipse4);
		Ellipse ellipse9 = ellipse4;
		StyledProperty<IBrush?> fillProperty2 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BrandTertiary");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse9, fillProperty2, binding6);
		ellipse9.Width = 18.0;
		ellipse9.Height = 18.0;
		context.PopParent();
		((ISupportInitialize)ellipse8).EndInit();
		context.PopParent();
		((ISupportInitialize)themeVariantScope3).EndInit();
		Controls children9 = panel4.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children9.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("DMCallSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty, binding7);
		rootSvgImage4.Width = 10.0;
		rootSvgImage4.Height = 10.0;
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(DirectMessageTabView P_0)
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

