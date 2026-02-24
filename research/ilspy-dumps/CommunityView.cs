using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Community;

public class CommunityView : UserControl
{
	private CancellationTokenSource? _cancellationTokenSource;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder ContrastSeparatorBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border ChatAreaBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal GridSplitter ChannelGridSplitter;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	public CommunityView()
	{
		InitializeComponent();
	}

	private async void onChannelGridSplitterPointerEntered(object? sender, PointerEventArgs e)
	{
		if (_cancellationTokenSource != null)
		{
			await _cancellationTokenSource.CancelAsync();
		}
		_cancellationTokenSource = new CancellationTokenSource();
		try
		{
			await Task.Delay(TimeSpan.FromMilliseconds(200L), _cancellationTokenSource.Token);
			setSplitterBackground();
		}
		catch (TaskCanceledException)
		{
		}
	}

	private void onChannelGridSplitterPointerExited(object? sender, PointerEventArgs e)
	{
		_cancellationTokenSource?.Cancel();
		resetSplitterBackground();
	}

	private void setSplitterBackground()
	{
		if (ChannelGridSplitter.IsPointerOver)
		{
			ChannelGridSplitter.Background = (IBrush)Application.Current.FindResource(Application.Current.ActualThemeVariant, "BrandPrimary");
		}
	}

	private void resetSplitterBackground()
	{
		ChannelGridSplitter.Background = Brushes.Transparent;
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
		ContrastSeparatorBorder = nameScope?.Find<RootBorder>("ContrastSeparatorBorder");
		ChatAreaBorder = nameScope?.Find<Border>("ChatAreaBorder");
		ChannelGridSplitter = nameScope?.Find<GridSplitter>("ChannelGridSplitter");
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, CommunityView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<CommunityView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<CommunityView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/CommunityView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/CommunityView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
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
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder5.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(25.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(203.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		ColumnDefinitions columnDefinitions = grid5.ColumnDefinitions;
		ColumnDefinition columnDefinition = new ColumnDefinition();
		columnDefinition.Width = new GridLength(0.0, GridUnitType.Auto);
		columnDefinition.MinWidth = 60.0;
		columnDefinitions.Add(columnDefinition);
		ColumnDefinitions columnDefinitions2 = grid5.ColumnDefinitions;
		ColumnDefinition columnDefinition3;
		ColumnDefinition columnDefinition2 = (columnDefinition3 = new ColumnDefinition());
		context.PushParent(columnDefinition3);
		StyledProperty<GridLength> widthProperty = ColumnDefinition.WidthProperty;
		CompiledBindingExtension obj2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.CommunityChannelsWidth!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ColumnDefinition.WidthProperty;
		CompiledBindingExtension compiledBindingExtension = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(columnDefinition3, widthProperty, compiledBindingExtension);
		columnDefinition3.MinWidth = 107.0;
		columnDefinition3.MaxWidth = 450.0;
		context.PopParent();
		columnDefinitions2.Add(columnDefinition2);
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		global::Avalonia.Controls.Controls children = grid5.Children;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		children.Add(rootBorder6);
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		Grid.SetColumn(rootBorder9, 0);
		Grid.SetRow(rootBorder9, 0);
		Grid.SetRowSpan(rootBorder9, 3);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, backgroundProperty, binding2);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty2, binding3);
		rootBorder9.DynamicBorderThickness = new Thickness(0.0, 0.0, 0.5, 0.0);
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		rootBorder9.Child = contentControl;
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		ContentControl contentControl5 = contentControl4;
		CompiledBindingExtension compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.Members!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension3 = compiledBindingExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl5, compiledBindingExtension3);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		global::Avalonia.Controls.Controls children2 = grid5.Children;
		RootBorder rootBorder11;
		RootBorder rootBorder10 = (rootBorder11 = new RootBorder());
		((ISupportInitialize)rootBorder10).BeginInit();
		children2.Add(rootBorder10);
		RootBorder rootBorder12 = (rootBorder4 = rootBorder11);
		context.PushParent(rootBorder4);
		RootBorder rootBorder13 = rootBorder4;
		Grid.SetColumn(rootBorder13, 1);
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder13, backgroundProperty2, binding4);
		StyledProperty<IBrush?> borderBrushProperty3 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder13, borderBrushProperty3, binding5);
		rootBorder13.DynamicBorderThickness = new Thickness(0.0, 0.0, 0.5, 0.0);
		Grid.SetRow(rootBorder13, 0);
		Grid.SetRowSpan(rootBorder13, 3);
		ContentControl contentControl7;
		ContentControl contentControl6 = (contentControl7 = new ContentControl());
		((ISupportInitialize)contentControl6).BeginInit();
		rootBorder13.Child = contentControl6;
		ContentControl contentControl8 = (contentControl4 = contentControl7);
		context.PushParent(contentControl4);
		ContentControl contentControl9 = contentControl4;
		CompiledBindingExtension compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.Channels!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl9, compiledBindingExtension5);
		context.PopParent();
		((ISupportInitialize)contentControl8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder12).EndInit();
		global::Avalonia.Controls.Controls children3 = grid5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children3.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		border5.Name = "ChatAreaBorder";
		obj = border5;
		context.AvaloniaNameScope.Register("ChatAreaBorder", obj);
		Grid.SetColumn(border5, 2);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty3, binding6);
		Grid.SetRow(border5, 0);
		Grid.SetRowSpan(border5, 3);
		RootSplitView rootSplitView2;
		RootSplitView rootSplitView = (rootSplitView2 = new RootSplitView());
		((ISupportInitialize)rootSplitView).BeginInit();
		border5.Child = rootSplitView;
		RootSplitView rootSplitView4;
		RootSplitView rootSplitView3 = (rootSplitView4 = rootSplitView2);
		context.PushParent(rootSplitView4);
		rootSplitView4.PanePlacement = SplitViewPanePlacement.Right;
		StyledProperty<bool> isPaneOpenProperty = SplitView.IsPaneOpenProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.PaneOpen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = SplitView.IsPaneOpenProperty;
		CompiledBindingExtension compiledBindingExtension6 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSplitView4, isPaneOpenProperty, compiledBindingExtension6);
		StyledProperty<SplitViewDisplayMode> displayModeProperty = SplitView.DisplayModeProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.PaneDisplayService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Helpers.Panes.PaneDisplayService,RootApp.Client.Avalonia.CommunityPaneDisplayMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = SplitView.DisplayModeProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSplitView4, displayModeProperty, compiledBindingExtension8);
		rootSplitView4.OpenPaneLength = 350.0;
		rootSplitView4.PaneBackground = new ImmutableSolidColorBrush(16777215u);
		rootSplitView4.UseLightDismissOverlayMode = false;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		rootSplitView4.Pane = border6;
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty4, binding7);
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		border9.Child = grid6;
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		global::Avalonia.Controls.Controls children4 = grid9.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children4.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		Grid.SetColumn(rectangle4, 0);
		rectangle4.Width = 0.5;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle4, fillProperty, binding8);
		rectangle4.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		global::Avalonia.Controls.Controls children5 = grid9.Children;
		ContentControl contentControl11;
		ContentControl contentControl10 = (contentControl11 = new ContentControl());
		((ISupportInitialize)contentControl10).BeginInit();
		children5.Add(contentControl10);
		ContentControl contentControl12 = (contentControl4 = contentControl11);
		context.PushParent(contentControl4);
		ContentControl contentControl13 = contentControl4;
		Grid.SetColumn(contentControl13, 1);
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.PaneContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl13, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)contentControl12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		ContentControl contentControl15;
		ContentControl contentControl14 = (contentControl15 = new ContentControl());
		((ISupportInitialize)contentControl14).BeginInit();
		rootSplitView4.Content = contentControl14;
		ContentControl contentControl16 = (contentControl4 = contentControl15);
		context.PushParent(contentControl4);
		ContentControl contentControl17 = contentControl4;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.SelectedChannelContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl17, compiledBindingExtension12);
		context.PopParent();
		((ISupportInitialize)contentControl16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSplitView3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children6 = grid5.Children;
		GridSplitter gridSplitter2;
		GridSplitter gridSplitter = (gridSplitter2 = new GridSplitter());
		((ISupportInitialize)gridSplitter).BeginInit();
		children6.Add(gridSplitter);
		GridSplitter gridSplitter4;
		GridSplitter gridSplitter3 = (gridSplitter4 = gridSplitter2);
		context.PushParent(gridSplitter4);
		Grid.SetColumn(gridSplitter4, 1);
		Grid.SetRow(gridSplitter4, 0);
		Grid.SetRowSpan(gridSplitter4, 3);
		gridSplitter4.Name = "ChannelGridSplitter";
		obj = gridSplitter4;
		context.AvaloniaNameScope.Register("ChannelGridSplitter", obj);
		gridSplitter4.Background = new ImmutableSolidColorBrush(16777215u);
		gridSplitter4.ResizeDirection = GridResizeDirection.Columns;
		gridSplitter4.HorizontalAlignment = HorizontalAlignment.Right;
		gridSplitter4.Width = 4.0;
		gridSplitter4.AddHandler(InputElement.PointerEnteredEvent, context.RootObject.onChannelGridSplitterPointerEntered);
		gridSplitter4.AddHandler(InputElement.PointerExitedEvent, context.RootObject.onChannelGridSplitterPointerExited);
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
		menuItem4.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.ResetSize;
		StyledProperty<ICommand?> commandProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.ResetCommunityChannelsWidthCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem4, commandProperty, compiledBindingExtension14);
		context.PopParent();
		((ISupportInitialize)menuItem3).EndInit();
		context.PopParent();
		gridSplitter4.ContextFlyout = contextFlyout;
		context.PopParent();
		((ISupportInitialize)gridSplitter3).EndInit();
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
	private static void !XamlIlPopulateTrampoline(CommunityView P_0)
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
