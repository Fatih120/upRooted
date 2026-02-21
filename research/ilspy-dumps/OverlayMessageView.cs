using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Resources.Converters.Messages;

namespace RootApp.Client.Avalonia.UI.Overlay;

public class OverlayMessageView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_187
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayMessageView> context = CreateContext(P_0);
			return new MessageTimeConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<OverlayMessageView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayMessageView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<OverlayMessageView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FOverlay_002FOverlayMessageView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Overlay/OverlayMessageView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (OverlayMessageView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayMessageView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayMessageView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayMessageView> context = CreateContext(P_0);
			context.IntermediateRoot = new WrapPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	private OverlayMessageViewModel? _currentMessage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl RootControl;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public OverlayMessageView()
	{
		InitializeComponent();
		base.DataContextChanged += OnDataContextChanged;
	}

	private void OnDataContextChanged(object? sender, EventArgs e)
	{
		_currentMessage = base.DataContext as OverlayMessageViewModel;
		if (_currentMessage == null || _currentMessage.IsFadingOut)
		{
			return;
		}
		Dispatcher.UIThread.Post(delegate
		{
			if (_currentMessage != null)
			{
				_currentMessage.Opacity = 1.0;
			}
		}, DispatcherPriority.Loaded);
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			_0021XamlIlPopulateTrampoline(this);
		}
		RootControl = this.FindNameScope()?.Find<UserControl>("RootControl");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, OverlayMessageView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<OverlayMessageView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<OverlayMessageView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FOverlay_002FOverlayMessageView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Overlay/OverlayMessageView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Name = "RootControl";
		context.AvaloniaNameScope.Register("RootControl", P_1);
		P_1.ClipToBounds = false;
		StyledProperty<double> opacityProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EOpacity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(P_1, opacityProperty, compiledBindingExtension2);
		P_1.IsHitTestVisible = false;
		Transitions transitions = new Transitions();
		DoubleTransition doubleTransition = new DoubleTransition();
		doubleTransition.Property = Visual.OpacityProperty;
		doubleTransition.Duration = TimeSpan.FromTicks(3000000L);
		doubleTransition.Easing = Easing.Parse("CubicEaseOut");
		transitions.Add(doubleTransition);
		P_1.Transitions = transitions;
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"MessageTimeConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_187.Build_1), context));
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.Margin = new Thickness(0.0, 2.0, 0.0, 2.0);
		global::Avalonia.Controls.Controls children = grid5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty, binding);
		border5.Opacity = 0.45;
		border5.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children2 = grid5.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children2.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Padding = new Thickness(8.0, 6.0, 8.0, 6.0);
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		border9.Child = grid6;
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 3;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(26.0, GridUnitType.Pixel)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(10.0, GridUnitType.Pixel)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid9.ColumnDefinitions = columnDefinitions;
		global::Avalonia.Controls.Controls children3 = grid9.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children3.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		Grid.SetColumn(rootImageLoader4, 0);
		rootImageLoader4.Width = 26.0;
		rootImageLoader4.Height = 26.0;
		rootImageLoader4.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty2, binding2);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EAvatarBitmap_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension4);
		rootImageLoader4.LoadingPlaceholderSize = 12.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		rootImageLoader4.VerticalAlignment = VerticalAlignment.Top;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		global::Avalonia.Controls.Controls children4 = grid9.Children;
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		children4.Add(grid10);
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		Grid.SetColumn(grid13, 2);
		RowDefinitions rowDefinitions = new RowDefinitions();
		rowDefinitions.Capacity = 5;
		rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		rowDefinitions.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid13.RowDefinitions = rowDefinitions;
		global::Avalonia.Controls.Controls children5 = grid13.Children;
		Grid grid15;
		Grid grid14 = (grid15 = new Grid());
		((ISupportInitialize)grid14).BeginInit();
		children5.Add(grid14);
		Grid grid16 = (grid4 = grid15);
		context.PushParent(grid4);
		Grid grid17 = grid4;
		Grid.SetRow(grid17, 0);
		ColumnDefinitions columnDefinitions2 = new ColumnDefinitions();
		columnDefinitions2.Capacity = 2;
		columnDefinitions2.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions2.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid17.ColumnDefinitions = columnDefinitions2;
		grid17.Margin = new Thickness(0.0, 0.0, 0.0, 2.0);
		global::Avalonia.Controls.Controls children6 = grid17.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children6.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EContextDisplay_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension6);
		textBlock5.FontSize = 11.0;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontWeight = FontWeight.Normal;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding3);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		textBlock5.TextTrimming = TextTrimming.CharacterEllipsis;
		textBlock5.MaxWidth = 120.0;
		textBlock5.Margin = new Thickness(0.0, 0.0, 3.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children7 = grid17.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children7.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		Grid.SetColumn(textBlock9, 1);
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002ESenderName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension8);
		textBlock9.FontSize = 13.0;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj2);
		textBlock9.FontWeight = FontWeight.DemiBold;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding4);
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid16).EndInit();
		global::Avalonia.Controls.Controls children8 = grid13.Children;
		RootMarkdownTextBlock rootMarkdownTextBlock2;
		RootMarkdownTextBlock rootMarkdownTextBlock = (rootMarkdownTextBlock2 = new RootMarkdownTextBlock());
		((ISupportInitialize)rootMarkdownTextBlock).BeginInit();
		children8.Add(rootMarkdownTextBlock);
		RootMarkdownTextBlock rootMarkdownTextBlock4;
		RootMarkdownTextBlock rootMarkdownTextBlock3 = (rootMarkdownTextBlock4 = rootMarkdownTextBlock2);
		context.PushParent(rootMarkdownTextBlock4);
		Grid.SetRow(rootMarkdownTextBlock4, 1);
		StyledProperty<IMarkdownEngine?> engineProperty = RootMarkdownTextBlock.EngineProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMarkdownEngine_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMarkdownTextBlock.EngineProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, engineProperty, compiledBindingExtension10);
		DirectProperty<RootMarkdownTextBlock, string?> markdownProperty = RootMarkdownTextBlock.MarkdownProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EMessageContent_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMarkdownTextBlock.MarkdownProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, markdownProperty, compiledBindingExtension12);
		RenderOptions.SetBitmapInterpolationMode(rootMarkdownTextBlock4, BitmapInterpolationMode.MediumQuality);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EMessageContent_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Converter = StringConverters.IsNotNullOrEmpty
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, isVisibleProperty, compiledBindingExtension13);
		rootMarkdownTextBlock4.Opacity = 0.65;
		context.PopParent();
		((ISupportInitialize)rootMarkdownTextBlock3).EndInit();
		global::Avalonia.Controls.Controls children9 = grid13.Children;
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		children9.Add(itemsControl);
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl5 = itemsControl4;
		Grid.SetRow(itemsControl5, 2);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002ELinks_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl5, itemsSourceProperty, compiledBindingExtension15);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002ELinks_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_91_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		compiledBindingExtension16.FallbackValue = "False";
		compiledBindingExtension16.TargetNullValue = "False";
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl5, isVisibleProperty2, compiledBindingExtension17);
		itemsControl5.Opacity = 0.3;
		itemsControl5.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_187.Build_2), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		global::Avalonia.Controls.Controls children10 = grid13.Children;
		ItemsControl itemsControl7;
		ItemsControl itemsControl6 = (itemsControl7 = new ItemsControl());
		((ISupportInitialize)itemsControl6).BeginInit();
		children10.Add(itemsControl6);
		ItemsControl itemsControl8 = (itemsControl4 = itemsControl7);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl9 = itemsControl4;
		Grid.SetRow(itemsControl9, 3);
		StyledProperty<IEnumerable?> itemsSourceProperty2 = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension18 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMedia_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension19 = compiledBindingExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl9, itemsSourceProperty2, compiledBindingExtension19);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension20 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMedia_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_84_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		compiledBindingExtension20.FallbackValue = "False";
		compiledBindingExtension20.TargetNullValue = "False";
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = compiledBindingExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl9, isVisibleProperty3, compiledBindingExtension21);
		itemsControl9.Opacity = 0.3;
		itemsControl9.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_187.Build_3), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl8).EndInit();
		global::Avalonia.Controls.Controls children11 = grid13.Children;
		ItemsControl itemsControl11;
		ItemsControl itemsControl10 = (itemsControl11 = new ItemsControl());
		((ISupportInitialize)itemsControl10).BeginInit();
		children11.Add(itemsControl10);
		ItemsControl itemsControl12 = (itemsControl4 = itemsControl11);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl13 = itemsControl4;
		Grid.SetRow(itemsControl13, 4);
		StyledProperty<IEnumerable?> itemsSourceProperty3 = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension22 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EFiles_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension23 = compiledBindingExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl13, itemsSourceProperty3, compiledBindingExtension23);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EOverlay_002EOverlayMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EFiles_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_84_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		compiledBindingExtension24.FallbackValue = "False";
		compiledBindingExtension24.TargetNullValue = "False";
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension25 = compiledBindingExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl13, isVisibleProperty4, compiledBindingExtension25);
		itemsControl13.Opacity = 0.3;
		itemsControl13.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_187.Build_4), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(OverlayMessageView P_0)
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
