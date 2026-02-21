using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Xaml.Interactions.Core;
using Avalonia.Xaml.Interactions.Custom;
using Avalonia.Xaml.Interactivity;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.SkiaImageLoader;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public class LocalMediaViewerView : UserControl
{
	private SkiaImageControl? _gifLoader;

	private FileStream? _gifStream;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal MenuItem CopyToClipboardMenuItem;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal MenuItem DownloadMenuItem;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border BackgroundBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder MainBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border SaveBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton SaveButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootCircleProgressBar ProgressRing;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal ContentControl GifPlaceholder;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private LocalMediaViewerViewModel _viewModel => (LocalMediaViewerViewModel)base.DataContext;

	public LocalMediaViewerView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		MainBorder.Opacity = 1.0;
		base.Focusable = true;
		Focus();
		if (_viewModel.IsGif)
		{
			LoadGif();
		}
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		CleanupGif();
	}

	private void LoadGif()
	{
		string localFilePath = _viewModel.LocalFilePath;
		if (!File.Exists(localFilePath))
		{
			return;
		}
		try
		{
			_gifStream = new FileStream(localFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
			_gifLoader = new SkiaImageControl
			{
				Source = _gifStream,
				Stretch = Stretch.Uniform
			};
			GifPlaceholder.Content = _gifLoader;
		}
		catch
		{
			CleanupGif();
		}
	}

	private void CleanupGif()
	{
		_gifStream?.Dispose();
		_gifStream = null;
		GifPlaceholder.Content = null;
		_gifLoader = null;
	}

	private void onViewKeyDown(object? sender, KeyEventArgs e)
	{
		if (e.Key == Key.Escape && _viewModel.CloseViewModelCommand.CanExecute(null))
		{
			_viewModel.CloseViewModelCommand.Execute(null);
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
		CopyToClipboardMenuItem = nameScope?.Find<MenuItem>("CopyToClipboardMenuItem");
		DownloadMenuItem = nameScope?.Find<MenuItem>("DownloadMenuItem");
		BackgroundBorder = nameScope?.Find<Border>("BackgroundBorder");
		MainBorder = nameScope?.Find<RootBorder>("MainBorder");
		SaveBorder = nameScope?.Find<Border>("SaveBorder");
		SaveButton = nameScope?.Find<RootSvgButton>("SaveButton");
		ProgressRing = nameScope?.Find<RootCircleProgressBar>("ProgressRing");
		GifPlaceholder = nameScope?.Find<ContentControl>("GifPlaceholder");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, LocalMediaViewerView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<LocalMediaViewerView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<LocalMediaViewerView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FMediaViewer_002FLocalMediaViewerView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/MediaViewer/LocalMediaViewerView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		RenderOptions.SetBitmapInterpolationMode(P_1, BitmapInterpolationMode.MediumQuality);
		P_1.AddHandler(InputElement.KeyDownEvent, context.RootObject.onViewKeyDown);
		IResourceDictionary resources = P_1.Resources;
		RootMenuFlyout rootMenuFlyout;
		RootMenuFlyout value = (rootMenuFlyout = new RootMenuFlyout());
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
		menuItem5.Name = "CopyToClipboardMenuItem";
		object obj = menuItem5;
		context.AvaloniaNameScope.Register("CopyToClipboardMenuItem", obj);
		menuItem5.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CopyImage;
		StyledProperty<ICommand?> commandProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002ECopyImageCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, commandProperty, compiledBindingExtension2);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002ECanCopyToClipboard_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, isVisibleProperty, compiledBindingExtension4);
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
		menuItem9.Name = "DownloadMenuItem";
		obj = menuItem9;
		context.AvaloniaNameScope.Register("DownloadMenuItem", obj);
		menuItem9.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.SaveImage;
		StyledProperty<ICommand?> commandProperty2 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002ESaveImageCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, commandProperty2, compiledBindingExtension6);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002ECanDownload_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, isVisibleProperty2, compiledBindingExtension8);
		context.PopParent();
		((ISupportInitialize)menuItem8).EndInit();
		context.PopParent();
		resources.Add("SharedMenu", value);
		StaticResourceExtension obj2 = new StaticResourceExtension
		{
			ResourceKey = "SharedMenu"
		};
		context.ProvideTargetProperty = Control.ContextFlyoutProperty;
		object? obj3 = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_25(P_1, obj3);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		global::Avalonia.Controls.Controls children = panel4.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		border5.Name = "BackgroundBorder";
		obj = border5;
		context.AvaloniaNameScope.Register("BackgroundBorder", obj);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("DropShadow");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty, binding);
		BehaviorCollection behaviors = Interaction.GetBehaviors(border5);
		RoutedEventTriggerBehavior routedEventTriggerBehavior = new RoutedEventTriggerBehavior();
		((ISupportInitialize)routedEventTriggerBehavior).BeginInit();
		RoutedEventTriggerBehavior routedEventTriggerBehavior2 = routedEventTriggerBehavior;
		context.PushParent(routedEventTriggerBehavior2);
		routedEventTriggerBehavior2.RoutedEvent = InputElement.PointerPressedEvent;
		ActionCollection? actions = routedEventTriggerBehavior2.Actions;
		InvokeCommandAction invokeCommandAction = new InvokeCommandAction();
		((ISupportInitialize)invokeCommandAction).BeginInit();
		InvokeCommandAction invokeCommandAction2 = invokeCommandAction;
		context.PushParent(invokeCommandAction2);
		StyledProperty<ICommand?> commandProperty3 = InvokeCommandActionBase.CommandProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = InvokeCommandActionBase.CommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(invokeCommandAction2, commandProperty3, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)invokeCommandAction).EndInit();
		actions.Add(invokeCommandAction);
		context.PopParent();
		((ISupportInitialize)routedEventTriggerBehavior).EndInit();
		behaviors.Add(routedEventTriggerBehavior);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children2 = panel4.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children2.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		rootBorder4.Name = "MainBorder";
		obj = rootBorder4;
		context.AvaloniaNameScope.Register("MainBorder", obj);
		rootBorder4.VerticalAlignment = VerticalAlignment.Center;
		rootBorder4.HorizontalAlignment = HorizontalAlignment.Center;
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty2, binding2);
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding3);
		rootBorder4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootBorder4.Margin = new Thickness(40.0, 40.0, 40.0, 40.0);
		rootBorder4.ClipToBounds = true;
		rootBorder4.Opacity = 0.0;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder4.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(59.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(12.0, GridUnitType.Pixel)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(12.0, GridUnitType.Pixel)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(12.0, GridUnitType.Pixel)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(24.0, GridUnitType.Pixel)
		});
		global::Avalonia.Controls.Controls children3 = grid5.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children3.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		Grid.SetRow(rootImageLoader4, 0);
		Grid.SetColumn(rootImageLoader4, 1);
		rootImageLoader4.Width = 40.0;
		rootImageLoader4.Height = 40.0;
		rootImageLoader4.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		StyledProperty<IBrush?> backgroundProperty3 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty3, binding4);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002EProfilePictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension12);
		rootImageLoader4.LoadingPlaceholderSize = 16.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		global::Avalonia.Controls.Controls children4 = grid5.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children4.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetRow(stackPanel5, 0);
		Grid.SetColumn(stackPanel5, 3);
		stackPanel5.VerticalAlignment = VerticalAlignment.Center;
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
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002EFilename_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension14);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj4 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj4);
		textBlock5.FontWeight = FontWeight.Medium;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding5);
		textBlock5.FontSize = 14.0;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		textBlock5.Margin = new Thickness(0.0, 0.0, 0.0, 3.0);
		textBlock5.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children6 = stackPanel5.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children6.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension16);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj5);
		textBlock9.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding6);
		textBlock9.FontSize = 12.0;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		global::Avalonia.Controls.Controls children7 = grid5.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children7.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		Grid.SetRow(stackPanel9, 0);
		Grid.SetColumn(stackPanel9, 5);
		stackPanel9.Spacing = 4.0;
		stackPanel9.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.HorizontalAlignment = HorizontalAlignment.Right;
		global::Avalonia.Controls.Controls children8 = stackPanel9.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children8.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Name = "SaveBorder";
		obj = border9;
		context.AvaloniaNameScope.Register("SaveBorder", obj);
		border9.Width = 30.0;
		border9.Height = 30.0;
		border9.CornerRadius = new CornerRadius(30.0, 30.0, 30.0, 30.0);
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty4, binding7);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002ECanDownload_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, isVisibleProperty3, compiledBindingExtension18);
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		border9.Child = rootSvgButton;
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton5 = rootSvgButton4;
		rootSvgButton5.Name = "SaveButton";
		obj = rootSvgButton5;
		context.AvaloniaNameScope.Register("SaveButton", obj);
		rootSvgButton5.Classes.Add("CustomSvgDimmedButton");
		rootSvgButton5.Width = 30.0;
		rootSvgButton5.Height = 30.0;
		StyledProperty<string> svgPathProperty = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("DownloadSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, svgPathProperty, binding8);
		rootSvgButton5.CornerRadius = new CornerRadius(30.0, 30.0, 30.0, 30.0);
		rootSvgButton5.SvgWidth = 18.0;
		rootSvgButton5.SvgHeight = 18.0;
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002ESaveImageCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, commandProperty4, compiledBindingExtension20);
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		global::Avalonia.Controls.Controls children9 = stackPanel9.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children9.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		StyledProperty<IBrush?> backgroundProperty5 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty5, binding9);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002EIsDownloading_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, isVisibleProperty4, compiledBindingExtension22);
		border13.Width = 30.0;
		border13.Height = 30.0;
		border13.CornerRadius = new CornerRadius(30.0, 30.0, 30.0, 30.0);
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		border13.Child = grid6;
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		global::Avalonia.Controls.Controls children10 = grid9.Children;
		RootCircleProgressBar rootCircleProgressBar2;
		RootCircleProgressBar rootCircleProgressBar = (rootCircleProgressBar2 = new RootCircleProgressBar());
		((ISupportInitialize)rootCircleProgressBar).BeginInit();
		children10.Add(rootCircleProgressBar);
		RootCircleProgressBar rootCircleProgressBar4;
		RootCircleProgressBar rootCircleProgressBar3 = (rootCircleProgressBar4 = rootCircleProgressBar2);
		context.PushParent(rootCircleProgressBar4);
		rootCircleProgressBar4.Name = "ProgressRing";
		obj = rootCircleProgressBar4;
		context.AvaloniaNameScope.Register("ProgressRing", obj);
		rootCircleProgressBar4.Width = 30;
		rootCircleProgressBar4.Height = 30;
		rootCircleProgressBar4.StrokeWidth = 3;
		StyledProperty<IBrush> strokeBrushProperty = RootCircleProgressBar.StrokeBrushProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = RootCircleProgressBar.StrokeBrushProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootCircleProgressBar4, strokeBrushProperty, binding10);
		StyledProperty<double> valueProperty = RootCircleProgressBar.ValueProperty;
		CompiledBindingExtension compiledBindingExtension23 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002EDownloadPercentage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootCircleProgressBar.ValueProperty;
		CompiledBindingExtension compiledBindingExtension24 = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootCircleProgressBar4, valueProperty, compiledBindingExtension24);
		context.PopParent();
		((ISupportInitialize)rootCircleProgressBar3).EndInit();
		global::Avalonia.Controls.Controls children11 = grid9.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children11.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding11);
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		textBlock13.HorizontalAlignment = HorizontalAlignment.Center;
		StyledProperty<string?> textProperty3 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension25 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002EDownloadPercentage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension26 = compiledBindingExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, textProperty3, compiledBindingExtension26);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj6 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj6);
		textBlock13.FontWeight = FontWeight.Medium;
		textBlock13.FontSize = 11.0;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		global::Avalonia.Controls.Controls children12 = stackPanel9.Children;
		Border border15;
		Border border14 = (border15 = new Border());
		((ISupportInitialize)border14).BeginInit();
		children12.Add(border14);
		Border border16 = (border4 = border15);
		context.PushParent(border4);
		Border border17 = border4;
		border17.Width = 30.0;
		border17.Height = 30.0;
		border17.CornerRadius = new CornerRadius(30.0, 30.0, 30.0, 30.0);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension27 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002EDownloadComplete_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension28 = compiledBindingExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, isVisibleProperty5, compiledBindingExtension28);
		StyledProperty<IBrush?> backgroundProperty6 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, backgroundProperty6, binding12);
		RootSvgButton rootSvgButton7;
		RootSvgButton rootSvgButton6 = (rootSvgButton7 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton6).BeginInit();
		border17.Child = rootSvgButton6;
		RootSvgButton rootSvgButton8 = (rootSvgButton4 = rootSvgButton7);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton9 = rootSvgButton4;
		rootSvgButton9.Classes.Add("CustomSvgDimmedButton");
		rootSvgButton9.Width = 30.0;
		rootSvgButton9.Height = 30.0;
		StyledProperty<string> svgPathProperty2 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("OpenSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, svgPathProperty2, binding13);
		rootSvgButton9.CornerRadius = new CornerRadius(30.0, 30.0, 30.0, 30.0);
		rootSvgButton9.SvgWidth = 15.0;
		rootSvgButton9.SvgHeight = 15.0;
		StyledProperty<ICommand?> commandProperty5 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension29 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002EOpenContainerFolderCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension30 = compiledBindingExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, commandProperty5, compiledBindingExtension30);
		context.PopParent();
		((ISupportInitialize)rootSvgButton8).EndInit();
		context.PopParent();
		((ISupportInitialize)border16).EndInit();
		global::Avalonia.Controls.Controls children13 = stackPanel9.Children;
		RootSvgButton rootSvgButton11;
		RootSvgButton rootSvgButton10 = (rootSvgButton11 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton10).BeginInit();
		children13.Add(rootSvgButton10);
		RootSvgButton rootSvgButton12 = (rootSvgButton4 = rootSvgButton11);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton13 = rootSvgButton4;
		rootSvgButton13.Classes.Add("SvgDimmedButton");
		rootSvgButton13.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
		rootSvgButton13.Width = 25.0;
		rootSvgButton13.Height = 25.0;
		StyledProperty<string> svgPathProperty3 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("EllipsisVerticalSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, svgPathProperty3, binding14);
		rootSvgButton13.SvgWidth = 4.0;
		rootSvgButton13.SvgHeight = 16.0;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("SharedMenu");
		context.ProvideTargetProperty = Button.FlyoutProperty;
		object? obj7 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_24(rootSvgButton13, obj7);
		context.PopParent();
		((ISupportInitialize)rootSvgButton12).EndInit();
		global::Avalonia.Controls.Controls children14 = stackPanel9.Children;
		RootSvgButton rootSvgButton15;
		RootSvgButton rootSvgButton14 = (rootSvgButton15 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton14).BeginInit();
		children14.Add(rootSvgButton14);
		RootSvgButton rootSvgButton16 = (rootSvgButton4 = rootSvgButton15);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton17 = rootSvgButton4;
		rootSvgButton17.Classes.Add("SvgDimmedButton");
		rootSvgButton17.Margin = new Thickness(4.0, 0.0, 0.0, 0.0);
		StyledProperty<ICommand?> commandProperty6 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension31 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension32 = compiledBindingExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton17, commandProperty6, compiledBindingExtension32);
		rootSvgButton17.Width = 25.0;
		rootSvgButton17.Height = 25.0;
		StyledProperty<string> svgPathProperty4 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("ExitThickSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton17, svgPathProperty4, binding15);
		rootSvgButton17.SvgWidth = 13.0;
		rootSvgButton17.SvgHeight = 13.0;
		context.PopParent();
		((ISupportInitialize)rootSvgButton16).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		global::Avalonia.Controls.Controls children15 = grid5.Children;
		Border border19;
		Border border18 = (border19 = new Border());
		((ISupportInitialize)border18).BeginInit();
		children15.Add(border18);
		Border border20 = (border4 = border19);
		context.PushParent(border4);
		Border border21 = border4;
		Grid.SetRow(border21, 1);
		Grid.SetColumnSpan(border21, 7);
		Grid.SetColumn(border21, 0);
		border21.Margin = new Thickness(12.0, 0.0, 12.0, 12.0);
		border21.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		border21.ClipToBounds = true;
		StyledProperty<IBrush?> backgroundProperty7 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border21, backgroundProperty7, binding16);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension33 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002EIsGif_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension34 = compiledBindingExtension33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border21, isVisibleProperty6, compiledBindingExtension34);
		Image image2;
		Image image = (image2 = new Image());
		((ISupportInitialize)image).BeginInit();
		border21.Child = image;
		Image image4;
		Image image3 = (image4 = image2);
		context.PushParent(image4);
		StyledProperty<IImage?> sourceProperty2 = Image.SourceProperty;
		CompiledBindingExtension compiledBindingExtension35 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002EImageBitmap_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Image.SourceProperty;
		CompiledBindingExtension compiledBindingExtension36 = compiledBindingExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(image4, sourceProperty2, compiledBindingExtension36);
		image4.Stretch = Stretch.Uniform;
		image4.StretchDirection = StretchDirection.DownOnly;
		image4.HorizontalAlignment = HorizontalAlignment.Center;
		image4.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)image3).EndInit();
		context.PopParent();
		((ISupportInitialize)border20).EndInit();
		global::Avalonia.Controls.Controls children16 = grid5.Children;
		Border border23;
		Border border22 = (border23 = new Border());
		((ISupportInitialize)border22).BeginInit();
		children16.Add(border22);
		Border border24 = (border4 = border23);
		context.PushParent(border4);
		Border border25 = border4;
		Grid.SetRow(border25, 1);
		Grid.SetColumnSpan(border25, 7);
		Grid.SetColumn(border25, 0);
		border25.Margin = new Thickness(12.0, 0.0, 12.0, 12.0);
		border25.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		border25.ClipToBounds = true;
		StyledProperty<IBrush?> backgroundProperty8 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding17 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border25, backgroundProperty8, binding17);
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension37 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002ELocalMediaViewerViewModel_002CRootApp_002EClient_002EAvalonia_002EIsGif_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension38 = compiledBindingExtension37.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border25, isVisibleProperty7, compiledBindingExtension38);
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		border25.Child = contentControl;
		contentControl2.Name = "GifPlaceholder";
		obj = contentControl2;
		context.AvaloniaNameScope.Register("GifPlaceholder", obj);
		contentControl2.HorizontalAlignment = HorizontalAlignment.Center;
		contentControl2.VerticalAlignment = VerticalAlignment.Center;
		((ISupportInitialize)contentControl2).EndInit();
		context.PopParent();
		((ISupportInitialize)border24).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(LocalMediaViewerView P_0)
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
