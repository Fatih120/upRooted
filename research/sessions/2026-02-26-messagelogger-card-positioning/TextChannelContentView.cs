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
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Overlay;
using RootApp.Client.Avalonia.Resources.Converters.Messages;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Community.Content;

public class TextChannelContentView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_73
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<TextChannelContentView> context = CreateContext(P_0);
			return new MessageDateConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<TextChannelContentView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<TextChannelContentView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<TextChannelContentView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Content/TextChannelContentView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Content/TextChannelContentView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (TextChannelContentView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<TextChannelContentView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(Layoutable.MarginProperty, new Thickness(0.0, 0.0, 0.0, 12.0), BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<TextChannelContentView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(Layoutable.MarginProperty, new Thickness(0.0, 12.0, 0.0, 12.0), BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<TextChannelContentView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(StackPanel.OrientationProperty, Orientation.Horizontal, BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Rectangle ChannelDescriptionDivider;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootTrimTooltipTextBlock DescriptionTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgCheckBox SearchButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgCheckBox PinnedMessagesButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgCheckBox PinnedMessagesButtonFilled;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton ChannelOptionsButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootMessageScrollViewer ScrollViewer;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootMessageItemsControl TestItemsControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border MessageBlockerBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder AutoCompleteBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock NewMessagesTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock NewMessagesDateTextBlock;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private TextChannelContentViewModel? _textChannelContentViewModel => base.DataContext as TextChannelContentViewModel;

	public TextChannelContentView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		if (_textChannelContentViewModel != null)
		{
			reportSize();
			AddHandler(DragDrop.DragEnterEvent, DragEnter);
			_textChannelContentViewModel.PropertyChanged += onTextChannelContentViewModelPropertyChanged;
			if (_textChannelContentViewModel.ViewLoadedCommand.CanExecute(null))
			{
				_textChannelContentViewModel.ViewLoadedCommand.Execute(null);
			}
			if (_textChannelContentViewModel.InitialMessagesBeganRendering)
			{
				MessageBlockerBorder.IsVisible = false;
			}
		}
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		if (_textChannelContentViewModel != null)
		{
			RemoveHandler(DragDrop.DragEnterEvent, DragEnter);
			_textChannelContentViewModel.PropertyChanged -= onTextChannelContentViewModelPropertyChanged;
			if (_textChannelContentViewModel.ViewUnloadedCommand.CanExecute(null))
			{
				_textChannelContentViewModel.ViewUnloadedCommand.Execute(null);
			}
		}
	}

	private void onTextChannelContentViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (_textChannelContentViewModel != null && e.PropertyName == "InitialMessagesBeganRendering" && _textChannelContentViewModel.InitialMessagesBeganRendering)
			{
				MessageBlockerBorder.IsVisible = false;
			}
		});
	}

	private void DragEnter(object? sender, DragEventArgs e)
	{
		if (_textChannelContentViewModel == null)
		{
			e.Handled = true;
			return;
		}
		if (_textChannelContentViewModel.IsFileUploaderOpen)
		{
			e.Handled = true;
			return;
		}
		nint valueOrDefault = (TopLevel.GetTopLevel(this)?.TryGetPlatformHandle()?.Handle).GetValueOrDefault();
		if (!OverlayInterop.IsWindowAtCursorPosition(valueOrDefault))
		{
			e.Handled = true;
			return;
		}
		if (!_textChannelContentViewModel.Channel.LocalChannelPermission.ChannelCreateMessage || !_textChannelContentViewModel.Channel.LocalChannelPermission.ChannelCreateMessageAttachment)
		{
			e.Handled = true;
			return;
		}
		IStorageItem[] array = e.DataTransfer.TryGetFiles();
		if (array != null && array.Length != 0)
		{
			_textChannelContentViewModel.ShowFileUploadView();
		}
		e.Handled = true;
	}

	private void reportSize()
	{
		if (_textChannelContentViewModel != null)
		{
			_textChannelContentViewModel.CommunityViewModel.ReportCommunityPaneWidth(base.Bounds.Size.Width);
		}
	}

	protected override void OnSizeChanged(SizeChangedEventArgs P_0)
	{
		base.OnSizeChanged(P_0);
		reportSize();
	}

	private void onChannelOptionsButtonPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		FlyoutBase flyout = ChannelOptionsButton.Flyout;
		if (flyout != null && (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed || e.GetCurrentPoint(this).Properties.IsRightButtonPressed))
		{
			flyout.ShowAt(ChannelOptionsButton);
			e.Handled = true;
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
		ChannelDescriptionDivider = nameScope?.Find<Rectangle>("ChannelDescriptionDivider");
		DescriptionTextBlock = nameScope?.Find<RootTrimTooltipTextBlock>("DescriptionTextBlock");
		SearchButton = nameScope?.Find<RootSvgCheckBox>("SearchButton");
		PinnedMessagesButton = nameScope?.Find<RootSvgCheckBox>("PinnedMessagesButton");
		PinnedMessagesButtonFilled = nameScope?.Find<RootSvgCheckBox>("PinnedMessagesButtonFilled");
		ChannelOptionsButton = nameScope?.Find<RootSvgButton>("ChannelOptionsButton");
		ScrollViewer = nameScope?.Find<RootMessageScrollViewer>("ScrollViewer");
		TestItemsControl = nameScope?.Find<RootMessageItemsControl>("TestItemsControl");
		MessageBlockerBorder = nameScope?.Find<Border>("MessageBlockerBorder");
		AutoCompleteBorder = nameScope?.Find<RootBorder>("AutoCompleteBorder");
		NewMessagesTextBlock = nameScope?.Find<TextBlock>("NewMessagesTextBlock");
		NewMessagesDateTextBlock = nameScope?.Find<TextBlock>("NewMessagesDateTextBlock");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, TextChannelContentView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<TextChannelContentView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<TextChannelContentView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Content/TextChannelContentView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Content/TextChannelContentView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		DragDrop.SetAllowDrop(P_1, true);
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		resourceDictionary.AddDeferred("MessageDateConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_73.Build_1), context));
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
			Width = new GridLength(0.0, GridUnitType.Auto)
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
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(60.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		global::Avalonia.Controls.Controls children = grid5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		Grid.SetColumn(border5, 1);
		Grid.SetRow(border5, 0);
		border5.Width = 26.0;
		border5.Height = 26.0;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2;
		CompiledBindingExtension compiledBindingExtension = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.IconAssetUri!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension3 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("StringNullOrEmptyToVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension3.Converter = (IValueConverter)obj;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, isVisibleProperty, compiledBindingExtension4);
		border5.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		border5.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty, binding);
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		border5.Child = rootImageLoader;
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.ChannelIconAsyncBitmapWrapper!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension6);
		rootImageLoader4.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		rootImageLoader4.LoadingPlaceholderSize = 10.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children2 = grid5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children2.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		Grid.SetColumn(textBlock5, 2);
		Grid.SetRow(textBlock5, 0);
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding2);
		textBlock5.FontSize = 18.0;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj2);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		textBlock5.TextTrimming = TextTrimming.CharacterEllipsis;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("PrependStringConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj3 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding3.Converter = (IMultiValueConverter)obj3;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj4 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Name!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Source = "# "
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item2 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, multiBinding);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children3 = grid5.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children3.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		Rectangle rectangle5 = rectangle4;
		Grid.SetColumn(rectangle5, 4);
		Grid.SetRow(rectangle5, 0);
		rectangle5.Name = "ChannelDescriptionDivider";
		object obj6 = rectangle5;
		context.AvaloniaNameScope.Register("ChannelDescriptionDivider", obj6);
		rectangle5.Width = 0.5;
		rectangle5.Height = 18.0;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle5, fillProperty, binding3);
		rectangle5.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7 = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Description!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("StringNullOrEmptyToVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj7 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension8.Converter = (IValueConverter)obj7;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle5, isVisibleProperty2, compiledBindingExtension9);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		global::Avalonia.Controls.Controls children4 = grid5.Children;
		RootTrimTooltipTextBlock rootTrimTooltipTextBlock2;
		RootTrimTooltipTextBlock rootTrimTooltipTextBlock = (rootTrimTooltipTextBlock2 = new RootTrimTooltipTextBlock());
		((ISupportInitialize)rootTrimTooltipTextBlock).BeginInit();
		children4.Add(rootTrimTooltipTextBlock);
		RootTrimTooltipTextBlock rootTrimTooltipTextBlock4;
		RootTrimTooltipTextBlock rootTrimTooltipTextBlock3 = (rootTrimTooltipTextBlock4 = rootTrimTooltipTextBlock2);
		context.PushParent(rootTrimTooltipTextBlock4);
		Grid.SetColumn(rootTrimTooltipTextBlock4, 5);
		Grid.SetRow(rootTrimTooltipTextBlock4, 0);
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Description!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTrimTooltipTextBlock4, textProperty2, compiledBindingExtension11);
		rootTrimTooltipTextBlock4.Name = "DescriptionTextBlock";
		obj6 = rootTrimTooltipTextBlock4;
		context.AvaloniaNameScope.Register("DescriptionTextBlock", obj6);
		rootTrimTooltipTextBlock4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		rootTrimTooltipTextBlock4.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTrimTooltipTextBlock4, foregroundProperty2, binding4);
		rootTrimTooltipTextBlock4.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj8 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(rootTrimTooltipTextBlock4, obj8);
		rootTrimTooltipTextBlock4.FontWeight = (FontWeight)450;
		rootTrimTooltipTextBlock4.VerticalAlignment = VerticalAlignment.Center;
		rootTrimTooltipTextBlock4.TextTrimming = TextTrimming.CharacterEllipsis;
		ToolTip.SetPlacement(rootTrimTooltipTextBlock4, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootTrimTooltipTextBlock4, 1.0);
		ToolTip.SetHorizontalOffset(rootTrimTooltipTextBlock4, 0.0);
		ToolTip.SetShowDelay(rootTrimTooltipTextBlock4, 0);
		context.PopParent();
		((ISupportInitialize)rootTrimTooltipTextBlock3).EndInit();
		global::Avalonia.Controls.Controls children5 = grid5.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children5.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetColumn(stackPanel5, 6);
		Grid.SetRow(stackPanel5, 0);
		stackPanel5.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.VerticalAlignment = VerticalAlignment.Center;
		stackPanel5.HorizontalAlignment = HorizontalAlignment.Right;
		global::Avalonia.Controls.Controls children6 = stackPanel5.Children;
		RootSvgCheckBox rootSvgCheckBox2;
		RootSvgCheckBox rootSvgCheckBox = (rootSvgCheckBox2 = new RootSvgCheckBox());
		((ISupportInitialize)rootSvgCheckBox).BeginInit();
		children6.Add(rootSvgCheckBox);
		RootSvgCheckBox rootSvgCheckBox4;
		RootSvgCheckBox rootSvgCheckBox3 = (rootSvgCheckBox4 = rootSvgCheckBox2);
		context.PushParent(rootSvgCheckBox4);
		RootSvgCheckBox rootSvgCheckBox5 = rootSvgCheckBox4;
		rootSvgCheckBox5.Classes.Add("SvgDimmedButton");
		rootSvgCheckBox5.Name = "SearchButton";
		obj6 = rootSvgCheckBox5;
		context.AvaloniaNameScope.Register("SearchButton", obj6);
		rootSvgCheckBox5.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgCheckBox5.Width = 32.0;
		rootSvgCheckBox5.Height = 32.0;
		StyledProperty<string> svgPathProperty = RootSvgCheckBox.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("SearchSVG");
		context.ProvideTargetProperty = RootSvgCheckBox.SvgPathProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox5, svgPathProperty, binding5);
		rootSvgCheckBox5.SvgWidth = 17.0;
		rootSvgCheckBox5.SvgHeight = 17.0;
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.CommunityViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.SearchSelectedCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox5, commandProperty, compiledBindingExtension13);
		StyledProperty<bool?> isCheckedProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.CommunityViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.IsSearchChecked!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension14 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox5, isCheckedProperty, compiledBindingExtension14);
		ToolTip.SetPlacement(rootSvgCheckBox5, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgCheckBox5, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgCheckBox5, 0.0);
		ToolTip.SetShowDelay(rootSvgCheckBox5, 0);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(rootSvgCheckBox5, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip5 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip5, PlacementMode.Bottom);
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		rootToolTip5.Content = textBlock6;
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Search;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj10 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj10);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.FontSize = 14.0;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgCheckBox3).EndInit();
		global::Avalonia.Controls.Controls children7 = stackPanel5.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children7.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		panel4.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		global::Avalonia.Controls.Controls children8 = panel4.Children;
		RootSvgCheckBox rootSvgCheckBox7;
		RootSvgCheckBox rootSvgCheckBox6 = (rootSvgCheckBox7 = new RootSvgCheckBox());
		((ISupportInitialize)rootSvgCheckBox6).BeginInit();
		children8.Add(rootSvgCheckBox6);
		RootSvgCheckBox rootSvgCheckBox8 = (rootSvgCheckBox4 = rootSvgCheckBox7);
		context.PushParent(rootSvgCheckBox4);
		RootSvgCheckBox rootSvgCheckBox9 = rootSvgCheckBox4;
		rootSvgCheckBox9.Name = "PinnedMessagesButton";
		obj6 = rootSvgCheckBox9;
		context.AvaloniaNameScope.Register("PinnedMessagesButton", obj6);
		rootSvgCheckBox9.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgCheckBox9.Width = 32.0;
		rootSvgCheckBox9.Height = 32.0;
		StyledProperty<string> svgPathProperty2 = RootSvgCheckBox.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("PinSVG");
		context.ProvideTargetProperty = RootSvgCheckBox.SvgPathProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox9, svgPathProperty2, binding6);
		rootSvgCheckBox9.SvgWidth = 20.0;
		rootSvgCheckBox9.SvgHeight = 20.0;
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.HasPinnedMessages!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox9, isVisibleProperty3, compiledBindingExtension16);
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.CommunityViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.PinnedSelectedCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox9, commandProperty2, compiledBindingExtension18);
		StyledProperty<bool?> isCheckedProperty2 = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.CommunityViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.IsPinnedChecked!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension19 = obj11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox9, isCheckedProperty2, compiledBindingExtension19);
		ToolTip.SetPlacement(rootSvgCheckBox9, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgCheckBox9, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgCheckBox9, 0.0);
		ToolTip.SetShowDelay(rootSvgCheckBox9, 0);
		RootToolTip rootToolTip7;
		RootToolTip rootToolTip6 = (rootToolTip7 = new RootToolTip());
		((ISupportInitialize)rootToolTip6).BeginInit();
		ToolTip.SetTip(rootSvgCheckBox9, rootToolTip6);
		RootToolTip rootToolTip8 = (rootToolTip4 = rootToolTip7);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip9 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip9, PlacementMode.Bottom);
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		rootToolTip9.Content = textBlock10;
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.PinnedMessages;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj12 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj12);
		textBlock13.FontWeight = (FontWeight)450;
		textBlock13.FontSize = 14.0;
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgCheckBox8).EndInit();
		global::Avalonia.Controls.Controls children9 = panel4.Children;
		RootSvgCheckBox rootSvgCheckBox11;
		RootSvgCheckBox rootSvgCheckBox10 = (rootSvgCheckBox11 = new RootSvgCheckBox());
		((ISupportInitialize)rootSvgCheckBox10).BeginInit();
		children9.Add(rootSvgCheckBox10);
		RootSvgCheckBox rootSvgCheckBox12 = (rootSvgCheckBox4 = rootSvgCheckBox11);
		context.PushParent(rootSvgCheckBox4);
		RootSvgCheckBox rootSvgCheckBox13 = rootSvgCheckBox4;
		rootSvgCheckBox13.Name = "PinnedMessagesButtonFilled";
		obj6 = rootSvgCheckBox13;
		context.AvaloniaNameScope.Register("PinnedMessagesButtonFilled", obj6);
		rootSvgCheckBox13.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgCheckBox13.Width = 32.0;
		rootSvgCheckBox13.Height = 32.0;
		StyledProperty<string> svgPathProperty3 = RootSvgCheckBox.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("PinFilledSVG");
		context.ProvideTargetProperty = RootSvgCheckBox.SvgPathProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox13, svgPathProperty3, binding7);
		rootSvgCheckBox13.SvgWidth = 20.0;
		rootSvgCheckBox13.SvgHeight = 20.0;
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension20 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.HasPinnedMessages!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = compiledBindingExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox13, isVisibleProperty4, compiledBindingExtension21);
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension22 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.CommunityViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.PinnedSelectedCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension23 = compiledBindingExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox13, commandProperty3, compiledBindingExtension23);
		StyledProperty<bool?> isCheckedProperty3 = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.CommunityViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.IsPinnedChecked!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension24 = obj13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox13, isCheckedProperty3, compiledBindingExtension24);
		ToolTip.SetPlacement(rootSvgCheckBox13, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgCheckBox13, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgCheckBox13, 0.0);
		ToolTip.SetShowDelay(rootSvgCheckBox13, 0);
		RootToolTip rootToolTip11;
		RootToolTip rootToolTip10 = (rootToolTip11 = new RootToolTip());
		((ISupportInitialize)rootToolTip10).BeginInit();
		ToolTip.SetTip(rootSvgCheckBox13, rootToolTip10);
		RootToolTip rootToolTip12 = (rootToolTip4 = rootToolTip11);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip13 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip13, PlacementMode.Bottom);
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		rootToolTip13.Content = textBlock14;
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.PinnedMessages;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj14 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock17, obj14);
		textBlock17.FontWeight = (FontWeight)450;
		textBlock17.FontSize = 14.0;
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock17.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgCheckBox12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		global::Avalonia.Controls.Controls children10 = stackPanel5.Children;
		RootSvgCheckBox rootSvgCheckBox15;
		RootSvgCheckBox rootSvgCheckBox14 = (rootSvgCheckBox15 = new RootSvgCheckBox());
		((ISupportInitialize)rootSvgCheckBox14).BeginInit();
		children10.Add(rootSvgCheckBox14);
		RootSvgCheckBox rootSvgCheckBox16 = (rootSvgCheckBox4 = rootSvgCheckBox15);
		context.PushParent(rootSvgCheckBox4);
		RootSvgCheckBox rootSvgCheckBox17 = rootSvgCheckBox4;
		rootSvgCheckBox17.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgCheckBox17.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		rootSvgCheckBox17.Width = 32.0;
		rootSvgCheckBox17.Height = 32.0;
		StyledProperty<string> svgPathProperty4 = RootSvgCheckBox.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("FilesSVG");
		context.ProvideTargetProperty = RootSvgCheckBox.SvgPathProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox17, svgPathProperty4, binding8);
		rootSvgCheckBox17.SvgWidth = 24.0;
		rootSvgCheckBox17.SvgHeight = 24.0;
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension25 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.CommunityViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.DirectoriesSelectedCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension26 = compiledBindingExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox17, commandProperty4, compiledBindingExtension26);
		StyledProperty<bool?> isCheckedProperty4 = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.CommunityViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.CommunityViewModel,RootApp.Client.Avalonia.IsDirectoriesChecked!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension27 = obj15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox17, isCheckedProperty4, compiledBindingExtension27);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension28 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelViewFile!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension29 = compiledBindingExtension28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgCheckBox17, isVisibleProperty5, compiledBindingExtension29);
		ToolTip.SetPlacement(rootSvgCheckBox17, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgCheckBox17, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgCheckBox17, 0.0);
		ToolTip.SetShowDelay(rootSvgCheckBox17, 0);
		RootToolTip rootToolTip15;
		RootToolTip rootToolTip14 = (rootToolTip15 = new RootToolTip());
		((ISupportInitialize)rootToolTip14).BeginInit();
		ToolTip.SetTip(rootSvgCheckBox17, rootToolTip14);
		RootToolTip rootToolTip16 = (rootToolTip4 = rootToolTip15);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip17 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip17, PlacementMode.Bottom);
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		rootToolTip17.Content = textBlock18;
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Files;
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj16 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock21, obj16);
		textBlock21.FontWeight = (FontWeight)450;
		textBlock21.FontSize = 14.0;
		textBlock21.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock21.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgCheckBox16).EndInit();
		global::Avalonia.Controls.Controls children11 = stackPanel5.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children11.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		rootSvgButton4.Classes.Add("SvgDimmedButton");
		rootSvgButton4.Name = "ChannelOptionsButton";
		obj6 = rootSvgButton4;
		context.AvaloniaNameScope.Register("ChannelOptionsButton", obj6);
		rootSvgButton4.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgButton4.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		rootSvgButton4.Width = 32.0;
		rootSvgButton4.Height = 32.0;
		StyledProperty<string> svgPathProperty5 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("EllipsisVerticalSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, svgPathProperty5, binding9);
		rootSvgButton4.SvgWidth = 4.0;
		rootSvgButton4.SvgHeight = 16.0;
		rootSvgButton4.AddHandler(InputElement.PointerPressedEvent, context.RootObject.onChannelOptionsButtonPointerPressed);
		ToolTip.SetPlacement(rootSvgButton4, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton4, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton4, 0.0);
		ToolTip.SetShowDelay(rootSvgButton4, 0);
		RootMenuFlyout rootMenuFlyout;
		RootMenuFlyout flyout = (rootMenuFlyout = new RootMenuFlyout());
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
		menuItem5.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.EditChannel;
		StyledProperty<ICommand?> commandProperty5 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension30 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.ShowEditChannelViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension31 = compiledBindingExtension30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, commandProperty5, compiledBindingExtension31);
		StyledProperty<bool> isEnabledProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension32 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelFullControl!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension33 = compiledBindingExtension32.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, isEnabledProperty, compiledBindingExtension33);
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
		menuItem9.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.Delete;
		menuItem9.Classes.Add("DeleteMenuItem");
		StyledProperty<ICommand?> commandProperty6 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension34 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.ShowDeleteChannelViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension35 = compiledBindingExtension34.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, commandProperty6, compiledBindingExtension35);
		StyledProperty<bool> isEnabledProperty2 = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension36 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelFullControl!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension37 = compiledBindingExtension36.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, isEnabledProperty2, compiledBindingExtension37);
		context.PopParent();
		((ISupportInitialize)menuItem8).EndInit();
		context.PopParent();
		rootSvgButton4.Flyout = flyout;
		RootToolTip rootToolTip19;
		RootToolTip rootToolTip18 = (rootToolTip19 = new RootToolTip());
		((ISupportInitialize)rootToolTip18).BeginInit();
		ToolTip.SetTip(rootSvgButton4, rootToolTip18);
		RootToolTip rootToolTip20 = (rootToolTip4 = rootToolTip19);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip21 = rootToolTip4;
		TextBlock textBlock23;
		TextBlock textBlock22 = (textBlock23 = new TextBlock());
		((ISupportInitialize)textBlock22).BeginInit();
		rootToolTip21.Content = textBlock22;
		TextBlock textBlock24 = (textBlock4 = textBlock23);
		context.PushParent(textBlock4);
		TextBlock textBlock25 = textBlock4;
		textBlock25.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ChannelOptions;
		StaticResourceExtension staticResourceExtension10 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj17 = staticResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock25, obj17);
		textBlock25.FontWeight = (FontWeight)450;
		textBlock25.FontSize = 14.0;
		textBlock25.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock25.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock24).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip20).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		global::Avalonia.Controls.Controls children12 = grid5.Children;
		Rectangle rectangle7;
		Rectangle rectangle6 = (rectangle7 = new Rectangle());
		((ISupportInitialize)rectangle6).BeginInit();
		children12.Add(rectangle6);
		Rectangle rectangle8 = (rectangle4 = rectangle7);
		context.PushParent(rectangle4);
		Rectangle rectangle9 = rectangle4;
		Grid.SetColumn(rectangle9, 0);
		Grid.SetColumnSpan(rectangle9, 7);
		Grid.SetRow(rectangle9, 0);
		rectangle9.VerticalAlignment = VerticalAlignment.Bottom;
		rectangle9.Height = 0.5;
		StyledProperty<IBrush?> fillProperty2 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle9, fillProperty2, binding10);
		rectangle9.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rectangle8).EndInit();
		global::Avalonia.Controls.Controls children13 = grid5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children13.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		Grid.SetColumn(grid9, 0);
		Grid.SetColumnSpan(grid9, 7);
		Grid.SetRow(grid9, 1);
		grid9.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid9.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(6.0, GridUnitType.Pixel)
		});
		grid9.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(20.0, GridUnitType.Pixel)
		});
		grid9.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(6.0, GridUnitType.Pixel)
		});
		global::Avalonia.Controls.Controls children14 = grid9.Children;
		RootMessageScrollViewer rootMessageScrollViewer2;
		RootMessageScrollViewer rootMessageScrollViewer = (rootMessageScrollViewer2 = new RootMessageScrollViewer());
		((ISupportInitialize)rootMessageScrollViewer).BeginInit();
		children14.Add(rootMessageScrollViewer);
		RootMessageScrollViewer rootMessageScrollViewer4;
		RootMessageScrollViewer rootMessageScrollViewer3 = (rootMessageScrollViewer4 = rootMessageScrollViewer2);
		context.PushParent(rootMessageScrollViewer4);
		Grid.SetRow(rootMessageScrollViewer4, 0);
		rootMessageScrollViewer4.Name = "ScrollViewer";
		obj6 = rootMessageScrollViewer4;
		context.AvaloniaNameScope.Register("ScrollViewer", obj6);
		StyledProperty<ICommand> downloadNewerMessagesCommandProperty = RootMessageScrollViewer.DownloadNewerMessagesCommandProperty;
		CompiledBindingExtension compiledBindingExtension38 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.DownloadNewerMessagesCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.DownloadNewerMessagesCommandProperty;
		CompiledBindingExtension compiledBindingExtension39 = compiledBindingExtension38.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, downloadNewerMessagesCommandProperty, compiledBindingExtension39);
		StyledProperty<ICommand> downloadOlderMessagesCommandProperty = RootMessageScrollViewer.DownloadOlderMessagesCommandProperty;
		CompiledBindingExtension compiledBindingExtension40 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.DownloadOlderMessagesCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.DownloadOlderMessagesCommandProperty;
		CompiledBindingExtension compiledBindingExtension41 = compiledBindingExtension40.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, downloadOlderMessagesCommandProperty, compiledBindingExtension41);
		StyledProperty<ICommand> setNewMessagesBannerStatusCommandProperty = RootMessageScrollViewer.SetNewMessagesBannerStatusCommandProperty;
		CompiledBindingExtension compiledBindingExtension42 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.SetNewMessagesBannerStatusCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.SetNewMessagesBannerStatusCommandProperty;
		CompiledBindingExtension compiledBindingExtension43 = compiledBindingExtension42.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, setNewMessagesBannerStatusCommandProperty, compiledBindingExtension43);
		StyledProperty<ICommand> messagesBeganRenderingCommandProperty = RootMessageScrollViewer.MessagesBeganRenderingCommandProperty;
		CompiledBindingExtension compiledBindingExtension44 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.MessagesBeganRenderingCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.MessagesBeganRenderingCommandProperty;
		CompiledBindingExtension compiledBindingExtension45 = compiledBindingExtension44.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, messagesBeganRenderingCommandProperty, compiledBindingExtension45);
		StyledProperty<ICommand> setAutoScrollStatusCommandProperty = RootMessageScrollViewer.SetAutoScrollStatusCommandProperty;
		CompiledBindingExtension compiledBindingExtension46 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.SetAutoScrollStatusCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.SetAutoScrollStatusCommandProperty;
		CompiledBindingExtension compiledBindingExtension47 = compiledBindingExtension46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, setAutoScrollStatusCommandProperty, compiledBindingExtension47);
		StyledProperty<ICommand> setShowJumpToPresentCommandProperty = RootMessageScrollViewer.SetShowJumpToPresentCommandProperty;
		CompiledBindingExtension compiledBindingExtension48 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.SetShowJumpToPresentCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.SetShowJumpToPresentCommandProperty;
		CompiledBindingExtension compiledBindingExtension49 = compiledBindingExtension48.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, setShowJumpToPresentCommandProperty, compiledBindingExtension49);
		RootMessageItemsControl rootMessageItemsControl2;
		RootMessageItemsControl rootMessageItemsControl = (rootMessageItemsControl2 = new RootMessageItemsControl());
		((ISupportInitialize)rootMessageItemsControl).BeginInit();
		rootMessageScrollViewer4.Content = rootMessageItemsControl;
		RootMessageItemsControl rootMessageItemsControl4;
		RootMessageItemsControl rootMessageItemsControl3 = (rootMessageItemsControl4 = rootMessageItemsControl2);
		context.PushParent(rootMessageItemsControl4);
		rootMessageItemsControl4.Name = "TestItemsControl";
		obj6 = rootMessageItemsControl4;
		context.AvaloniaNameScope.Register("TestItemsControl", obj6);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension50 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.Messages!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension51 = compiledBindingExtension50.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageItemsControl4, itemsSourceProperty, compiledBindingExtension51);
		rootMessageItemsControl4.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_73.Build_2), context)
		};
		context.PopParent();
		((ISupportInitialize)rootMessageItemsControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootMessageScrollViewer3).EndInit();
		global::Avalonia.Controls.Controls children15 = grid9.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children15.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Name = "MessageBlockerBorder";
		obj6 = border9;
		context.AvaloniaNameScope.Register("MessageBlockerBorder", obj6);
		Grid.SetRow(border9, 0);
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty2, binding11);
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		global::Avalonia.Controls.Controls children16 = grid9.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children16.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		RootBorder rootBorder5 = rootBorder4;
		rootBorder5.Name = "AutoCompleteBorder";
		obj6 = rootBorder5;
		context.AvaloniaNameScope.Register("AutoCompleteBorder", obj6);
		Grid.SetRow(rootBorder5, 0);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, backgroundProperty3, binding12);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, borderBrushProperty, binding13);
		rootBorder5.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder5.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<BoxShadows> boxShadowProperty = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, boxShadowProperty, binding14);
		rootBorder5.Margin = new Thickness(16.0, 0.0, 16.0, 8.0);
		rootBorder5.VerticalAlignment = VerticalAlignment.Bottom;
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension52 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.RootMessageTextboxViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.Messaging.RootMessageTextboxViewModel,RootApp.Client.Avalonia.AutoCompleteItems!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System.Collections.ObjectModel.Collection`1,System.Runtime.Count_74!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension53 = compiledBindingExtension52.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, isVisibleProperty6, compiledBindingExtension53);
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		rootBorder5.Child = rootScrollViewer;
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		rootScrollViewer4.Content = itemsControl;
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl5 = itemsControl4;
		StyledProperty<IEnumerable?> itemsSourceProperty2 = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension54 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.RootMessageTextboxViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.Messaging.RootMessageTextboxViewModel,RootApp.Client.Avalonia.AutoCompleteItems!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension55 = compiledBindingExtension54.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl5, itemsSourceProperty2, compiledBindingExtension55);
		itemsControl5.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_73.Build_3), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		global::Avalonia.Controls.Controls children17 = grid9.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children17.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		Grid.SetRow(border13, 0);
		border13.Height = 56.0;
		border13.VerticalAlignment = VerticalAlignment.Top;
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty4, binding15);
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension56 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.ShowNewMessagesBanner!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension57 = compiledBindingExtension56.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, isVisibleProperty7, compiledBindingExtension57);
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		border13.Child = grid10;
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children18 = grid13.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children18.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Margin = new Thickness(20.0, 0.0, 0.0, 0.0);
		stackPanel9.VerticalAlignment = VerticalAlignment.Center;
		global::Avalonia.Controls.Controls children19 = stackPanel9.Children;
		TextBlock textBlock27;
		TextBlock textBlock26 = (textBlock27 = new TextBlock());
		((ISupportInitialize)textBlock26).BeginInit();
		children19.Add(textBlock26);
		TextBlock textBlock28 = (textBlock4 = textBlock27);
		context.PushParent(textBlock4);
		TextBlock textBlock29 = textBlock4;
		textBlock29.Name = "NewMessagesTextBlock";
		obj6 = textBlock29;
		context.AvaloniaNameScope.Register("NewMessagesTextBlock", obj6);
		textBlock29.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock29, foregroundProperty3, binding16);
		textBlock29.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension11 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj18 = staticResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock29, obj18);
		textBlock29.FontWeight = FontWeight.Bold;
		StyledProperty<string?> textProperty3 = TextBlock.TextProperty;
		CompiledBindingExtension obj19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Messages!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IMessageService,RootApp.Client.CoreDomain.NewMessagesCount!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			StringFormat = RootApp.Client.Avalonia.Resources.Strings.Resources.NewMessages
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension58 = obj19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock29, textProperty3, compiledBindingExtension58);
		context.PopParent();
		((ISupportInitialize)textBlock28).EndInit();
		global::Avalonia.Controls.Controls children20 = stackPanel9.Children;
		TextBlock textBlock31;
		TextBlock textBlock30 = (textBlock31 = new TextBlock());
		((ISupportInitialize)textBlock30).BeginInit();
		children20.Add(textBlock30);
		TextBlock textBlock32 = (textBlock4 = textBlock31);
		context.PushParent(textBlock4);
		TextBlock textBlock33 = textBlock4;
		textBlock33.Name = "NewMessagesDateTextBlock";
		obj6 = textBlock33;
		context.AvaloniaNameScope.Register("NewMessagesDateTextBlock", obj6);
		textBlock33.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock33.Margin = new Thickness(0.0, 5.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding17 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock33, foregroundProperty4, binding17);
		textBlock33.Opacity = 0.64;
		textBlock33.FontSize = 13.0;
		StaticResourceExtension staticResourceExtension12 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj20 = staticResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock33, obj20);
		textBlock33.FontWeight = (FontWeight)450;
		StyledProperty<string?> textProperty4 = TextBlock.TextProperty;
		CompiledBindingExtension obj21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.LastViewedAtString!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			StringFormat = RootApp.Client.Avalonia.Resources.Strings.Resources.SinceDate
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension59 = obj21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock33, textProperty4, compiledBindingExtension59);
		context.PopParent();
		((ISupportInitialize)textBlock32).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		global::Avalonia.Controls.Controls children21 = grid13.Children;
		ThemeVariantScope themeVariantScope2;
		ThemeVariantScope themeVariantScope = (themeVariantScope2 = new ThemeVariantScope());
		((ISupportInitialize)themeVariantScope).BeginInit();
		children21.Add(themeVariantScope);
		ThemeVariantScope themeVariantScope4;
		ThemeVariantScope themeVariantScope3 = (themeVariantScope4 = themeVariantScope2);
		context.PushParent(themeVariantScope4);
		Grid.SetColumn(themeVariantScope4, 1);
		themeVariantScope4.RequestedThemeVariant = ThemeVariant.Dark;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		themeVariantScope4.Child = button;
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.Classes.Add("BasicButton");
		button5.Margin = new Thickness(0.0, 0.0, 20.0, 0.0);
		button5.Height = 36.0;
		button5.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		button5.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty5 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding18 = dynamicResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, backgroundProperty5, binding18);
		StyledProperty<ICommand?> commandProperty7 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension60 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.MarkAsReadCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension61 = compiledBindingExtension60.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty7, compiledBindingExtension61);
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		button5.Content = stackPanel10;
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		stackPanel13.Orientation = Orientation.Horizontal;
		stackPanel13.Margin = new Thickness(12.0, 0.0, 12.0, 0.0);
		global::Avalonia.Controls.Controls children22 = stackPanel13.Children;
		TextBlock textBlock35;
		TextBlock textBlock34 = (textBlock35 = new TextBlock());
		((ISupportInitialize)textBlock34).BeginInit();
		children22.Add(textBlock34);
		TextBlock textBlock36 = (textBlock4 = textBlock35);
		context.PushParent(textBlock4);
		TextBlock textBlock37 = textBlock4;
		textBlock37.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock37.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		textBlock37.VerticalAlignment = VerticalAlignment.Center;
		textBlock37.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.MarkAsRead;
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension19 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding19 = dynamicResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock37, foregroundProperty5, binding19);
		textBlock37.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension13 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj22 = staticResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock37, obj22);
		textBlock37.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock36).EndInit();
		global::Avalonia.Controls.Controls children23 = stackPanel13.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children23.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		rootSvgImage4.Width = 14.0;
		rootSvgImage4.Height = 11.0;
		rootSvgImage4.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<string?> svgPathProperty6 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension20 = new DynamicResourceExtension("MarkAsReadCheckmarkSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding20 = dynamicResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty6, binding20);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		context.PopParent();
		((ISupportInitialize)themeVariantScope3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		global::Avalonia.Controls.Controls children24 = grid9.Children;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		children24.Add(rootBorder6);
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		Grid.SetRow(rootBorder9, 0);
		rootBorder9.HorizontalAlignment = HorizontalAlignment.Center;
		rootBorder9.VerticalAlignment = VerticalAlignment.Bottom;
		rootBorder9.Margin = new Thickness(0.0, 0.0, 0.0, 16.0);
		StyledProperty<IBrush?> backgroundProperty6 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension21 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding21 = dynamicResourceExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, backgroundProperty6, binding21);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension22 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding22 = dynamicResourceExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty2, binding22);
		rootBorder9.DynamicBorderThickness = new Thickness(1.0, 1.0, 1.0, 1.0);
		rootBorder9.CornerRadius = new CornerRadius(20.0, 20.0, 20.0, 20.0);
		rootBorder9.Padding = new Thickness(6.0, 6.0, 6.0, 6.0);
		rootBorder9.BoxShadow = BoxShadows.Parse("0 4 16 0 #60000000");
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		multiBinding5.Converter = BoolConverters.Or;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension obj23 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.ShowJumpToPresent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item3 = obj23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension obj24 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Messages!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IMessageService,RootApp.Client.CoreDomain.InFocusMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item4 = obj24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootBorder9, isVisibleProperty8, multiBinding4);
		StackPanel stackPanel15;
		StackPanel stackPanel14 = (stackPanel15 = new StackPanel());
		((ISupportInitialize)stackPanel14).BeginInit();
		rootBorder9.Child = stackPanel14;
		StackPanel stackPanel16 = (stackPanel4 = stackPanel15);
		context.PushParent(stackPanel4);
		StackPanel stackPanel17 = stackPanel4;
		stackPanel17.Orientation = Orientation.Horizontal;
		global::Avalonia.Controls.Controls children25 = stackPanel17.Children;
		TextBlock textBlock39;
		TextBlock textBlock38 = (textBlock39 = new TextBlock());
		((ISupportInitialize)textBlock38).BeginInit();
		children25.Add(textBlock38);
		TextBlock textBlock40 = (textBlock4 = textBlock39);
		context.PushParent(textBlock4);
		TextBlock textBlock41 = textBlock4;
		textBlock41.Padding = new Thickness(16.0, 8.0, 16.0, 8.0);
		textBlock41.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> foregroundProperty6 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension23 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding23 = dynamicResourceExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock41, foregroundProperty6, binding23);
		textBlock41.FontSize = 13.0;
		StaticResourceExtension staticResourceExtension14 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj25 = staticResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock41, obj25);
		textBlock41.FontWeight = (FontWeight)450;
		textBlock41.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ViewingOlderMessages;
		context.PopParent();
		((ISupportInitialize)textBlock40).EndInit();
		global::Avalonia.Controls.Controls children26 = stackPanel17.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children26.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Classes.Add("TransparentButtonWithClickEffect");
		StyledProperty<IBrush?> backgroundProperty7 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension24 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding24 = dynamicResourceExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, backgroundProperty7, binding24);
		button9.CornerRadius = new CornerRadius(14.0, 14.0, 14.0, 14.0);
		button9.Padding = new Thickness(16.0, 8.0, 16.0, 8.0);
		button9.Cursor = new Cursor(StandardCursorType.Hand);
		StyledProperty<ICommand?> commandProperty8 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension62 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.MarkAsReadCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension63 = compiledBindingExtension62.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty8, compiledBindingExtension63);
		TextBlock textBlock43;
		TextBlock textBlock42 = (textBlock43 = new TextBlock());
		((ISupportInitialize)textBlock42).BeginInit();
		button9.Content = textBlock42;
		TextBlock textBlock44 = (textBlock4 = textBlock43);
		context.PushParent(textBlock4);
		TextBlock textBlock45 = textBlock4;
		StyledProperty<IBrush?> foregroundProperty7 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension25 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding25 = dynamicResourceExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock45, foregroundProperty7, binding25);
		textBlock45.FontSize = 13.0;
		StaticResourceExtension staticResourceExtension15 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj26 = staticResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock45, obj26);
		textBlock45.FontWeight = FontWeight.Bold;
		textBlock45.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.JumpToPresent;
		context.PopParent();
		((ISupportInitialize)textBlock44).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		global::Avalonia.Controls.Controls children27 = grid9.Children;
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		children27.Add(contentControl);
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		Grid.SetRow(contentControl4, 1);
		contentControl4.Margin = new Thickness(16.0, 0.0, 16.0, 0.0);
		CompiledBindingExtension compiledBindingExtension64 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.RootMessageTextboxViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension65 = compiledBindingExtension64.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl4, compiledBindingExtension65);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		global::Avalonia.Controls.Controls children28 = grid9.Children;
		ItemsControl itemsControl7;
		ItemsControl itemsControl6 = (itemsControl7 = new ItemsControl());
		((ISupportInitialize)itemsControl6).BeginInit();
		children28.Add(itemsControl6);
		ItemsControl itemsControl8 = (itemsControl4 = itemsControl7);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl9 = itemsControl4;
		Grid.SetRow(itemsControl9, 3);
		itemsControl9.Margin = new Thickness(16.0, 0.0, 16.0, 0.0);
		StyledProperty<IEnumerable?> itemsSourceProperty3 = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension66 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelContentViewModel,RootApp.Client.Avalonia.TypingUsers!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension67 = compiledBindingExtension66.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl9, itemsSourceProperty3, compiledBindingExtension67);
		itemsControl9.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_73.Build_4), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl8).EndInit();
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
	private static void !XamlIlPopulateTrampoline(TextChannelContentView P_0)
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
