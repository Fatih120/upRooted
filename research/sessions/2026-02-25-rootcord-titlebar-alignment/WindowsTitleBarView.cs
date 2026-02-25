using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Resources.Converters.Installation;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.Controls.TitleBars;

public class WindowsTitleBarView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_21
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<WindowsTitleBarView> context = CreateContext(P_0);
			return new UpdateTextConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<WindowsTitleBarView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<WindowsTitleBarView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<WindowsTitleBarView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/Controls/TitleBars/WindowsTitleBarView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/TitleBars/WindowsTitleBarView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (WindowsTitleBarView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<WindowsTitleBarView> context = CreateContext(P_0);
			return new UpdateVisibilityConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<WindowsTitleBarView> context = CreateContext(P_0);
			return new DownloadVisibilityConverter();
		}
	}

	private readonly Button? _minimizeButton;

	private readonly Button? _maximizeButton;

	private readonly Button? _closeButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Panel ProgressBarGrid;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border ProgressBaseBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border ProgressStatusBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton UpdateButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton SupportButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton MinimizeButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton MaximizeButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton CloseButton;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private WindowsTitleBarViewModel _windowsTitleBarViewModel => (WindowsTitleBarViewModel)base.DataContext;

	public WindowsTitleBarView()
	{
		InitializeComponent();
		_minimizeButton = this.FindControl<Button>("MinimizeButton");
		_maximizeButton = this.FindControl<Button>("MaximizeButton");
		_closeButton = this.FindControl<Button>("CloseButton");
		_minimizeButton.Click += minimizeWindow;
		_maximizeButton.Click += maximizeWindow;
		_closeButton.Click += closeWindow;
		subscribeToWindowStateAsync();
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		_windowsTitleBarViewModel.InstallationManager.PropertyChanged += onInstallationManagerPropertyChanged;
		updateProgress();
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		_windowsTitleBarViewModel.PropertyChanged -= onInstallationManagerPropertyChanged;
	}

	private void onInstallationManagerPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "DownloadProgress")
		{
			updateProgress();
		}
	}

	private void updateProgress()
	{
		Dispatcher.UIThread.Post(delegate
		{
			double width = ProgressBaseBorder.Bounds.Width;
			double width2 = width * ((double)_windowsTitleBarViewModel.InstallationManager.DownloadProgress / 100.0);
			ProgressStatusBorder.Width = width2;
		});
	}

	private void closeWindow(object? sender, RoutedEventArgs e)
	{
		Window window = (Window)base.VisualRoot;
		window.Close();
	}

	private void maximizeWindow(object? sender, RoutedEventArgs e)
	{
		Window window = (Window)base.VisualRoot;
		if (window.WindowState == WindowState.Normal)
		{
			window.WindowState = WindowState.Maximized;
		}
		else
		{
			window.WindowState = WindowState.Normal;
		}
	}

	private void minimizeWindow(object? sender, RoutedEventArgs e)
	{
		Window window = (Window)base.VisualRoot;
		window.WindowState = WindowState.Minimized;
	}

	private async Task subscribeToWindowStateAsync()
	{
		while (base.VisualRoot == null)
		{
			await Task.Delay(50);
		}
		Window hostWindow = (Window)base.VisualRoot;
		hostWindow.GetObservable(Window.WindowStateProperty).Subscribe(delegate(WindowState s)
		{
			if (s != WindowState.Maximized)
			{
				hostWindow.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
			}
			if (s == WindowState.Maximized)
			{
				hostWindow.Padding = new Thickness(7.0, 7.0, 7.0, 7.0);
			}
		});
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
		ProgressBarGrid = nameScope?.Find<Panel>("ProgressBarGrid");
		ProgressBaseBorder = nameScope?.Find<Border>("ProgressBaseBorder");
		ProgressStatusBorder = nameScope?.Find<Border>("ProgressStatusBorder");
		UpdateButton = nameScope?.Find<RootSvgButton>("UpdateButton");
		SupportButton = nameScope?.Find<RootSvgButton>("SupportButton");
		MinimizeButton = nameScope?.Find<RootSvgButton>("MinimizeButton");
		MaximizeButton = nameScope?.Find<RootSvgButton>("MaximizeButton");
		CloseButton = nameScope?.Find<RootSvgButton>("CloseButton");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, WindowsTitleBarView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<WindowsTitleBarView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<WindowsTitleBarView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/Controls/TitleBars/WindowsTitleBarView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/TitleBars/WindowsTitleBarView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Height = 36.0;
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		if (resourceDictionary is ResourceDictionary resourceDictionary2)
		{
			resourceDictionary2.EnsureCapacity(resourceDictionary2.Count + 3);
		}
		resourceDictionary.AddDeferred("UpdateTextConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_21.Build_1), context));
		resourceDictionary.AddDeferred("UpdateVisibilityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_21.Build_2), context));
		resourceDictionary.AddDeferred("DownloadVisibilityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_21.Build_3), context));
		P_1.Resources = resourceDictionary;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		global::Avalonia.Controls.Controls children = panel5.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		panel9.IsHitTestVisible = false;
		StyledProperty<IBrush?> backgroundProperty = Panel.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Panel.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel9, backgroundProperty, binding);
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		global::Avalonia.Controls.Controls children2 = panel5.Children;
		Panel panel11;
		Panel panel10 = (panel11 = new Panel());
		((ISupportInitialize)panel10).BeginInit();
		children2.Add(panel10);
		Panel panel12 = (panel4 = panel11);
		context.PushParent(panel4);
		Panel panel13 = panel4;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.TitleBars.TitleBarViewModelBase`1,RootApp.Client.Avalonia.IsConnected_22!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel13, isVisibleProperty, compiledBindingExtension2);
		panel13.IsHitTestVisible = false;
		StyledProperty<IBrush?> backgroundProperty2 = Panel.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Warning");
		context.ProvideTargetProperty = Panel.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel13, backgroundProperty2, binding2);
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
		global::Avalonia.Controls.Controls children3 = panel5.Children;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		children3.Add(grid);
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children4 = grid4.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children4.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		Grid.SetColumn(rootSvgImage4, 0);
		rootSvgImage4.Height = 21.0;
		rootSvgImage4.Width = 20.0;
		rootSvgImage4.VerticalAlignment = VerticalAlignment.Center;
		rootSvgImage4.Margin = new Thickness(11.0, 0.0, 3.0, 0.0);
		rootSvgImage4.IsHitTestVisible = false;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TaskBarRootLogoSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty, binding3);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		global::Avalonia.Controls.Controls children5 = grid4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children5.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetColumn(stackPanel5, 1);
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.Spacing = 16.0;
		stackPanel5.HorizontalAlignment = HorizontalAlignment.Center;
		stackPanel5.VerticalAlignment = VerticalAlignment.Center;
		global::Avalonia.Controls.Controls children6 = stackPanel5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children6.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.TitleBars.TitleBarViewModelBase`1,RootApp.Client.Avalonia.WebApiUrl_23!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension4);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.TitleBars.TitleBarViewModelBase`1,RootApp.Client.Avalonia.ShowConnectLabel_24!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, isVisibleProperty2, compiledBindingExtension6);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding4);
		textBlock5.IsHitTestVisible = false;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children7 = stackPanel5.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children7.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension8;
		CompiledBindingExtension compiledBindingExtension7 = (compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.TitleBars.TitleBarViewModelBase`1,RootApp.Client.Avalonia.InstallationManager_25!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Helpers.Installation.RootInstallationManager,RootApp.Client.Avalonia.DownloadStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension8);
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension8;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("UpdateTextConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension9.Converter = (IValueConverter)obj2;
		context.PopParent();
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension10);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension11 = (compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.TitleBars.TitleBarViewModelBase`1,RootApp.Client.Avalonia.InstallationManager_25!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Helpers.Installation.RootInstallationManager,RootApp.Client.Avalonia.DownloadStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension8);
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension8;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("UpdateVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj3 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension12.Converter = (IValueConverter)obj3;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, isVisibleProperty3, compiledBindingExtension13);
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj4 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj4);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding5);
		textBlock9.IsHitTestVisible = false;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		global::Avalonia.Controls.Controls children8 = stackPanel5.Children;
		Panel panel15;
		Panel panel14 = (panel15 = new Panel());
		((ISupportInitialize)panel14).BeginInit();
		children8.Add(panel14);
		Panel panel16 = (panel4 = panel15);
		context.PushParent(panel4);
		Panel panel17 = panel4;
		panel17.Name = "ProgressBarGrid";
		object obj5 = panel17;
		context.AvaloniaNameScope.Register("ProgressBarGrid", obj5);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension14 = (compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.TitleBars.TitleBarViewModelBase`1,RootApp.Client.Avalonia.InstallationManager_25!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Helpers.Installation.RootInstallationManager,RootApp.Client.Avalonia.DownloadProgress!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension8);
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension8;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("PercentageToVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj6 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension15.Converter = (IValueConverter)obj6;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel17, isVisibleProperty4, compiledBindingExtension16);
		panel17.Width = 150.0;
		global::Avalonia.Controls.Controls children9 = panel17.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children9.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		border5.Name = "ProgressBaseBorder";
		obj5 = border5;
		context.AvaloniaNameScope.Register("ProgressBaseBorder", obj5);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty3, binding6);
		border5.Opacity = 0.1;
		border5.Height = 4.0;
		border5.CornerRadius = new CornerRadius(2.0, 2.0, 2.0, 2.0);
		border5.HorizontalAlignment = HorizontalAlignment.Stretch;
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children10 = panel17.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children10.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Name = "ProgressStatusBorder";
		obj5 = border9;
		context.AvaloniaNameScope.Register("ProgressStatusBorder", obj5);
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty4, binding7);
		border9.Height = 4.0;
		border9.CornerRadius = new CornerRadius(2.0, 2.0, 2.0, 2.0);
		border9.HorizontalAlignment = HorizontalAlignment.Left;
		Transitions transitions = new Transitions();
		DoubleTransition doubleTransition = new DoubleTransition();
		doubleTransition.Easing = Easing.Parse("CircularEaseOut");
		doubleTransition.Property = Layoutable.WidthProperty;
		doubleTransition.Duration = TimeSpan.FromTicks(6000000L);
		transitions.Add(doubleTransition);
		border9.Transitions = transitions;
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel16).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		global::Avalonia.Controls.Controls children11 = grid4.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children11.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		Grid.SetColumn(stackPanel9, 2);
		stackPanel9.Height = 22.0;
		stackPanel9.HorizontalAlignment = HorizontalAlignment.Right;
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.Spacing = 0.0;
		stackPanel9.VerticalAlignment = VerticalAlignment.Center;
		stackPanel9.IsHitTestVisible = true;
		global::Avalonia.Controls.Controls children12 = stackPanel9.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children12.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton5 = rootSvgButton4;
		rootSvgButton5.Name = "UpdateButton";
		obj5 = rootSvgButton5;
		context.AvaloniaNameScope.Register("UpdateButton", obj5);
		StyledProperty<string> svgPathProperty2 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("InstallUpdateSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, svgPathProperty2, binding8);
		rootSvgButton5.SvgWidth = 18.0;
		rootSvgButton5.SvgHeight = 18.0;
		rootSvgButton5.Width = 20.0;
		rootSvgButton5.Height = 20.0;
		rootSvgButton5.Margin = new Thickness(0.0, 0.0, 8.0, 0.0);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.TitleBars.TitleBarViewModelBase`1,RootApp.Client.Avalonia.RestartClientCommand_26!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, commandProperty, compiledBindingExtension18);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension19 = (compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.TitleBars.TitleBarViewModelBase`1,RootApp.Client.Avalonia.InstallationManager_25!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Helpers.Installation.RootInstallationManager,RootApp.Client.Avalonia.DownloadStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension8);
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension8;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("DownloadVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj7 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension20.Converter = (IValueConverter)obj7;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, isVisibleProperty5, compiledBindingExtension21);
		ToolTip.SetPlacement(rootSvgButton5, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton5, 1.0);
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
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		rootToolTip5.Content = textBlock10;
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.UpdateClient;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj8 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj8);
		textBlock13.FontWeight = (FontWeight)450;
		textBlock13.FontSize = 14.0;
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		global::Avalonia.Controls.Controls children13 = stackPanel9.Children;
		RootSvgButton rootSvgButton7;
		RootSvgButton rootSvgButton6 = (rootSvgButton7 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton6).BeginInit();
		children13.Add(rootSvgButton6);
		RootSvgButton rootSvgButton8 = (rootSvgButton4 = rootSvgButton7);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton9 = rootSvgButton4;
		rootSvgButton9.Name = "SupportButton";
		obj5 = rootSvgButton9;
		context.AvaloniaNameScope.Register("SupportButton", obj5);
		StyledProperty<string> svgPathProperty3 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("SupportSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, svgPathProperty3, binding9);
		rootSvgButton9.SvgWidth = 18.0;
		rootSvgButton9.SvgHeight = 18.0;
		rootSvgButton9.Width = 20.0;
		rootSvgButton9.Height = 20.0;
		rootSvgButton9.Margin = new Thickness(0.0, 0.0, 24.0, 0.0);
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension22 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.TitleBars.TitleBarViewModelBase`1,RootApp.Client.Avalonia.OpenSupportModalCommand_27!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension23 = compiledBindingExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, commandProperty2, compiledBindingExtension23);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.TitleBars.TitleBarViewModelBase`1,RootApp.Client.Avalonia.IsConnected_22!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension25 = compiledBindingExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, isVisibleProperty6, compiledBindingExtension25);
		ToolTip.SetPlacement(rootSvgButton9, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton9, 1.0);
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
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		rootToolTip9.Content = textBlock14;
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Support;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock17, obj9);
		textBlock17.FontWeight = (FontWeight)450;
		textBlock17.FontSize = 14.0;
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock17.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton8).EndInit();
		global::Avalonia.Controls.Controls children14 = stackPanel9.Children;
		RootSvgButton rootSvgButton11;
		RootSvgButton rootSvgButton10 = (rootSvgButton11 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton10).BeginInit();
		children14.Add(rootSvgButton10);
		RootSvgButton rootSvgButton12 = (rootSvgButton4 = rootSvgButton11);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton13 = rootSvgButton4;
		rootSvgButton13.Name = "MinimizeButton";
		obj5 = rootSvgButton13;
		context.AvaloniaNameScope.Register("MinimizeButton", obj5);
		StyledProperty<string> svgPathProperty4 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("MinimizeWindowSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, svgPathProperty4, binding10);
		rootSvgButton13.SvgWidth = 12.0;
		rootSvgButton13.SvgHeight = 2.0;
		rootSvgButton13.Width = 20.0;
		rootSvgButton13.Height = 20.0;
		rootSvgButton13.Margin = new Thickness(0.0, 0.0, 13.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootSvgButton12).EndInit();
		global::Avalonia.Controls.Controls children15 = stackPanel9.Children;
		RootSvgButton rootSvgButton15;
		RootSvgButton rootSvgButton14 = (rootSvgButton15 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton14).BeginInit();
		children15.Add(rootSvgButton14);
		RootSvgButton rootSvgButton16 = (rootSvgButton4 = rootSvgButton15);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton17 = rootSvgButton4;
		rootSvgButton17.Name = "MaximizeButton";
		obj5 = rootSvgButton17;
		context.AvaloniaNameScope.Register("MaximizeButton", obj5);
		StyledProperty<string> svgPathProperty5 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("ExpandWindowSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton17, svgPathProperty5, binding11);
		rootSvgButton17.SvgWidth = 10.0;
		rootSvgButton17.SvgHeight = 10.0;
		rootSvgButton17.Width = 20.0;
		rootSvgButton17.Height = 20.0;
		rootSvgButton17.Margin = new Thickness(0.0, 0.0, 13.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootSvgButton16).EndInit();
		global::Avalonia.Controls.Controls children16 = stackPanel9.Children;
		RootSvgButton rootSvgButton19;
		RootSvgButton rootSvgButton18 = (rootSvgButton19 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton18).BeginInit();
		children16.Add(rootSvgButton18);
		RootSvgButton rootSvgButton20 = (rootSvgButton4 = rootSvgButton19);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton21 = rootSvgButton4;
		rootSvgButton21.Name = "CloseButton";
		obj5 = rootSvgButton21;
		context.AvaloniaNameScope.Register("CloseButton", obj5);
		StyledProperty<string> svgPathProperty6 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("ExitWindowSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton21, svgPathProperty6, binding12);
		rootSvgButton21.SvgWidth = 12.0;
		rootSvgButton21.SvgHeight = 12.0;
		rootSvgButton21.Width = 20.0;
		rootSvgButton21.Height = 20.0;
		rootSvgButton21.Margin = new Thickness(0.0, 0.0, 13.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootSvgButton20).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
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
	private static void !XamlIlPopulateTrampoline(WindowsTitleBarView P_0)
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
