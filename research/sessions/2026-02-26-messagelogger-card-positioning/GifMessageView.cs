using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using Microsoft.VisualStudio.Threading;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.SkiaImageLoader;

namespace RootApp.Client.Avalonia.UI.Messages;

public class GifMessageView : UserControl
{
	private SkiaImageControl? _gifLoader;

	private Stream? _gifStream;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border GifPlaceholderContainer;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal ContentControl GifPlaceholder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border SaveBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton SaveButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootCircleProgressBar ProgressRing;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private GifMessageViewModel? _gifMessageViewModel => base.DataContext as GifMessageViewModel;

	public GifMessageView()
	{
		InitializeComponent();
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		if (_gifMessageViewModel == null)
		{
			return;
		}
		Dispatcher.UIThread.InvokeAsync(async delegate
		{
			_gifStream = await _gifMessageViewModel.GetGifPathAsync();
			if (_gifStream != null)
			{
				_gifLoader = new SkiaImageControl
				{
					Source = _gifStream,
					Stretch = Stretch.Uniform
				};
				GifPlaceholder.Content = _gifLoader;
			}
		}).Forget();
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		_gifStream?.Dispose();
		_gifStream = null;
		GifPlaceholder.Content = null;
		_gifLoader = null;
	}

	protected override void OnSizeChanged(SizeChangedEventArgs P_0)
	{
		base.OnSizeChanged(P_0);
		updateSize(base.Bounds.Width);
	}

	private void updateSize(double P_0)
	{
		if (_gifMessageViewModel != null && !(_gifMessageViewModel.PlaceholderWidth <= 0.0) && !(_gifMessageViewModel.PlaceholderHeight <= 0.0))
		{
			double num = Math.Min(P_0, 450.0);
			int num2 = 300;
			double num3 = Math.Min(num / _gifMessageViewModel.PlaceholderWidth, (double)num2 / _gifMessageViewModel.PlaceholderHeight);
			double width = _gifMessageViewModel.PlaceholderWidth * num3;
			double height = _gifMessageViewModel.PlaceholderHeight * num3;
			GifPlaceholderContainer.Width = width;
			GifPlaceholderContainer.Height = height;
		}
	}

	private void onGifClicked(object? sender, RoutedEventArgs e)
	{
		if (_gifMessageViewModel != null && _gifMessageViewModel.OpenPreviewCommand.CanExecute(null))
		{
			_gifMessageViewModel.OpenPreviewCommand.Execute(null);
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
		GifPlaceholderContainer = nameScope?.Find<Border>("GifPlaceholderContainer");
		GifPlaceholder = nameScope?.Find<ContentControl>("GifPlaceholder");
		SaveBorder = nameScope?.Find<Border>("SaveBorder");
		SaveButton = nameScope?.Find<RootSvgButton>("SaveButton");
		ProgressRing = nameScope?.Find<RootCircleProgressBar>("ProgressRing");
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, GifMessageView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<GifMessageView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<GifMessageView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Messages/GifMessageView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Messages/GifMessageView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.MaxHeight = 300.0;
		P_1.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.HorizontalAlignment = HorizontalAlignment.Left;
		global::Avalonia.Controls.Controls children = grid5.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Classes.Add("BasicButton");
		button4.AddHandler((RoutedEvent)Button.ClickEvent, (Delegate)new EventHandler<RoutedEventArgs>(context.RootObject.onGifClicked), RoutingStrategies.Direct | RoutingStrategies.Bubble, false);
		button4.HorizontalAlignment = HorizontalAlignment.Left;
		button4.HorizontalContentAlignment = HorizontalAlignment.Left;
		button4.Background = new ImmutableSolidColorBrush(16777215u);
		button4.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
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
		menuItem5.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.Save;
		StyledProperty<ICommand?> commandProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.GifMessageViewModel,RootApp.Client.Avalonia.SaveCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, commandProperty, compiledBindingExtension2);
		context.PopParent();
		((ISupportInitialize)menuItem3).EndInit();
		ItemCollection items2 = rootMenuFlyout.Items;
		Separator separator2;
		Separator separator = (separator2 = new Separator());
		((ISupportInitialize)separator).BeginInit();
		items2.Add(separator);
		((ISupportInitialize)separator2).EndInit();
		ItemCollection items3 = rootMenuFlyout.Items;
		MenuItem menuItem7;
		MenuItem menuItem6 = (menuItem7 = new MenuItem());
		((ISupportInitialize)menuItem6).BeginInit();
		items3.Add(menuItem6);
		MenuItem menuItem8 = (menuItem4 = menuItem7);
		context.PushParent(menuItem4);
		MenuItem menuItem9 = menuItem4;
		menuItem9.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CopyLink;
		StyledProperty<ICommand?> commandProperty2 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.GifMessageViewModel,RootApp.Client.Avalonia.CopyLinkCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, commandProperty2, compiledBindingExtension4);
		context.PopParent();
		((ISupportInitialize)menuItem8).EndInit();
		ItemCollection items4 = rootMenuFlyout.Items;
		MenuItem menuItem11;
		MenuItem menuItem10 = (menuItem11 = new MenuItem());
		((ISupportInitialize)menuItem10).BeginInit();
		items4.Add(menuItem10);
		MenuItem menuItem12 = (menuItem4 = menuItem11);
		context.PushParent(menuItem4);
		MenuItem menuItem13 = menuItem4;
		menuItem13.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.OpenLink;
		StyledProperty<ICommand?> commandProperty3 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.GifMessageViewModel,RootApp.Client.Avalonia.OpenLinkCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, commandProperty3, compiledBindingExtension6);
		context.PopParent();
		((ISupportInitialize)menuItem12).EndInit();
		context.PopParent();
		button4.ContextFlyout = contextFlyout;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		button4.Content = border;
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		border5.Name = "GifPlaceholderContainer";
		object obj = border5;
		context.AvaloniaNameScope.Register("GifPlaceholderContainer", obj);
		border5.CornerRadius = new CornerRadius(10.0, 10.0, 10.0, 10.0);
		border5.ClipToBounds = true;
		border5.HorizontalAlignment = HorizontalAlignment.Left;
		StyledProperty<double> maxHeightProperty = Layoutable.MaxHeightProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.GifMessageViewModel,RootApp.Client.Avalonia.PlaceholderHeight!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Layoutable.MaxHeightProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, maxHeightProperty, compiledBindingExtension8);
		StyledProperty<double> maxWidthProperty = Layoutable.MaxWidthProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.GifMessageViewModel,RootApp.Client.Avalonia.PlaceholderWidth!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Layoutable.MaxWidthProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, maxWidthProperty, compiledBindingExtension10);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty, binding);
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		border5.Child = contentControl;
		contentControl2.Name = "GifPlaceholder";
		obj = contentControl2;
		context.AvaloniaNameScope.Register("GifPlaceholder", obj);
		((ISupportInitialize)contentControl2).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		global::Avalonia.Controls.Controls children2 = grid5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children2.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Ancestor(typeof(UserControl), 0).Property(InputElement.IsPointerOverProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid9, isVisibleProperty, compiledBindingExtension12);
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 3;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid9.ColumnDefinitions = columnDefinitions;
		grid9.HorizontalAlignment = HorizontalAlignment.Stretch;
		grid9.VerticalAlignment = VerticalAlignment.Top;
		grid9.Margin = new Thickness(0.0, 4.0, 4.0, 0.0);
		global::Avalonia.Controls.Controls children3 = grid9.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children3.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Name = "SaveBorder";
		obj = border9;
		context.AvaloniaNameScope.Register("SaveBorder", obj);
		Grid.SetColumn(border9, 1);
		border9.Width = 30.0;
		border9.Height = 30.0;
		border9.CornerRadius = new CornerRadius(30.0, 30.0, 30.0, 30.0);
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty2, binding2);
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
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("DownloadSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, svgPathProperty, binding3);
		rootSvgButton5.CornerRadius = new CornerRadius(30.0, 30.0, 30.0, 30.0);
		rootSvgButton5.SvgWidth = 18.0;
		rootSvgButton5.SvgHeight = 18.0;
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.GifMessageViewModel,RootApp.Client.Avalonia.SaveCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, commandProperty4, compiledBindingExtension14);
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		global::Avalonia.Controls.Controls children4 = grid9.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children4.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		Grid.SetColumn(border13, 2);
		border13.Margin = new Thickness(4.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty3, binding4);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.GifMessageViewModel,RootApp.Client.Avalonia.IsDownloading!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, isVisibleProperty2, compiledBindingExtension16);
		border13.CornerRadius = new CornerRadius(30.0, 30.0, 30.0, 30.0);
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		border13.Child = grid10;
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		global::Avalonia.Controls.Controls children5 = grid13.Children;
		RootCircleProgressBar rootCircleProgressBar2;
		RootCircleProgressBar rootCircleProgressBar = (rootCircleProgressBar2 = new RootCircleProgressBar());
		((ISupportInitialize)rootCircleProgressBar).BeginInit();
		children5.Add(rootCircleProgressBar);
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
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = RootCircleProgressBar.StrokeBrushProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootCircleProgressBar4, strokeBrushProperty, binding5);
		StyledProperty<double> valueProperty = RootCircleProgressBar.ValueProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.GifMessageViewModel,RootApp.Client.Avalonia.DownloadPercentage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootCircleProgressBar.ValueProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootCircleProgressBar4, valueProperty, compiledBindingExtension18);
		context.PopParent();
		((ISupportInitialize)rootCircleProgressBar3).EndInit();
		global::Avalonia.Controls.Controls children6 = grid13.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children6.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, binding6);
		textBlock4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock4.VerticalAlignment = VerticalAlignment.Center;
		textBlock4.HorizontalAlignment = HorizontalAlignment.Center;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.GifMessageViewModel,RootApp.Client.Avalonia.DownloadPercentage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, textProperty, compiledBindingExtension20);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock4, obj2);
		textBlock4.FontWeight = FontWeight.Medium;
		textBlock4.FontSize = 11.0;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		global::Avalonia.Controls.Controls children7 = grid9.Children;
		Border border15;
		Border border14 = (border15 = new Border());
		((ISupportInitialize)border14).BeginInit();
		children7.Add(border14);
		Border border16 = (border4 = border15);
		context.PushParent(border4);
		Border border17 = border4;
		Grid.SetColumn(border17, 2);
		border17.Margin = new Thickness(4.0, 0.0, 0.0, 0.0);
		border17.Width = 30.0;
		border17.Height = 30.0;
		border17.CornerRadius = new CornerRadius(30.0, 30.0, 30.0, 30.0);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.GifMessageViewModel,RootApp.Client.Avalonia.DownloadComplete!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, isVisibleProperty3, compiledBindingExtension22);
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, backgroundProperty4, binding7);
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
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("OpenSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, svgPathProperty2, binding8);
		rootSvgButton9.CornerRadius = new CornerRadius(30.0, 30.0, 30.0, 30.0);
		rootSvgButton9.SvgWidth = 15.0;
		rootSvgButton9.SvgHeight = 15.0;
		StyledProperty<ICommand?> commandProperty5 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension23 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.GifMessageViewModel,RootApp.Client.Avalonia.OpenContainerFolderCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension24 = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, commandProperty5, compiledBindingExtension24);
		context.PopParent();
		((ISupportInitialize)rootSvgButton8).EndInit();
		context.PopParent();
		((ISupportInitialize)border16).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
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
	private static void !XamlIlPopulateTrampoline(GifMessageView P_0)
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
