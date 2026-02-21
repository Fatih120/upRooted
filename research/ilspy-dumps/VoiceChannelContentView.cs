using System;
using System.CodeDom.Compiler;
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
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using CompiledAvaloniaXaml;
using DotNetBrowser.AvaloniaUi;
using DotNetBrowser.Browser;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Community.Content;

public class VoiceChannelContentView : UserControl
{
	private double _currentZoom = 1.0;

	private UserContextMenuView? _activeContextMenu;

	private RootImageLoader? _channelIconLoader;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border ChannelIconContainer;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Rectangle ChannelDescriptionDivider;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton ChannelOptionsButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal LayoutTransformControl MainZoomControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal BrowserView MediaBrowser;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Panel MainPanel;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private VoiceChannelContentViewModel _voiceChannelContentViewModel => (VoiceChannelContentViewModel)base.DataContext;

	public VoiceChannelContentView()
	{
		InitializeComponent();
		Dispatcher.UIThread.Post(delegate
		{
			VoiceChannelContentViewModel voiceChannelContentViewModel = _voiceChannelContentViewModel;
			if (voiceChannelContentViewModel != null && voiceChannelContentViewModel.Browser != null)
			{
				MediaBrowser.InitializeFrom(_voiceChannelContentViewModel.Browser.DotNetBrowser);
			}
		});
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		setZoom();
		createChannelIconLoader();
		_voiceChannelContentViewModel.PropertyChanged += onViewModelPropertyChanged;
		_voiceChannelContentViewModel.ZoomService.ZoomChanged += onZoomServiceZoomChanged;
		WeakReferenceMessenger.Default.Register<BrowserDisposingMessage>(this, onBrowserDisposing);
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		removeChannelIconLoader();
		_voiceChannelContentViewModel.PropertyChanged -= onViewModelPropertyChanged;
		_voiceChannelContentViewModel.ZoomService.ZoomChanged -= onZoomServiceZoomChanged;
		WeakReferenceMessenger.Default.Unregister<BrowserDisposingMessage>(this);
	}

	private void onBrowserDisposing(object recipient, BrowserDisposingMessage message)
	{
		if (_voiceChannelContentViewModel.Browser?.Id == message.BrowserId)
		{
			MainZoomControl.Child = null;
		}
	}

	private async void createChannelIconLoader()
	{
		_channelIconLoader = new RootImageLoader
		{
			CornerRadius = new CornerRadius(6.0),
			LoadingPlaceholderSize = 10.0,
			Stretch = Stretch.UniformToFill
		};
		ChannelIconContainer.Child = _channelIconLoader;
		BitmapWrapper bitmapWrapper = await _voiceChannelContentViewModel.ChannelIconAsyncBitmapWrapper;
		if (_channelIconLoader != null)
		{
			_channelIconLoader.Source = bitmapWrapper;
		}
	}

	private void removeChannelIconLoader()
	{
		ChannelIconContainer.Child = null;
		_channelIconLoader = null;
	}

	private void onZoomServiceZoomChanged(double zoomLevel)
	{
		setZoom();
	}

	private void setZoom()
	{
		if (_currentZoom == _voiceChannelContentViewModel.ZoomService.Zoom)
		{
			return;
		}
		Dispatcher.UIThread.Post(delegate
		{
			_currentZoom = _voiceChannelContentViewModel.ZoomService.Zoom;
			MainZoomControl.LayoutTransform = new ScaleTransform(1.0 / _currentZoom, 1.0 / _currentZoom);
			if (_voiceChannelContentViewModel.Browser != null)
			{
				_voiceChannelContentViewModel.Browser.DotNetBrowser.Zoom.Level = _voiceChannelContentViewModel.ZoomService.ToNearestDotNetBrowserZoomLevel();
			}
		});
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

	private void onViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "IsPopupOpen")
			{
				if (FlyoutBase.GetAttachedFlyout(MainPanel) is RootFlyout rootFlyout)
				{
					double zoom = _voiceChannelContentViewModel.ZoomService.Zoom;
					rootFlyout.Placement = PlacementMode.TopEdgeAlignedLeft;
					rootFlyout.HorizontalOffset = (_voiceChannelContentViewModel.PopupTargetX - 12.0) * zoom;
					rootFlyout.VerticalOffset = _voiceChannelContentViewModel.PopupTargetY * zoom;
					Popup flyoutPopup = rootFlyout.FlyoutPopup;
					if (_voiceChannelContentViewModel.IsPopupOpen)
					{
						FlyoutBase.ShowAttachedFlyout(MainPanel);
						flyoutPopup.MinHeight = (flyoutPopup.Host as PopupRoot).Bounds.Height;
						flyoutPopup.MaxHeight = (flyoutPopup.Host as PopupRoot).Bounds.Height;
					}
					else
					{
						flyoutPopup.MinHeight = 0.0;
						flyoutPopup.MaxHeight = double.PositiveInfinity;
					}
				}
			}
			else if (e.PropertyName == "IsContextMenuOpen")
			{
				if (_voiceChannelContentViewModel.IsContextMenuOpen && _voiceChannelContentViewModel.UserContextMenu != null)
				{
					_activeContextMenu = new UserContextMenuView
					{
						DataContext = _voiceChannelContentViewModel.UserContextMenu,
						Placement = PlacementMode.TopEdgeAlignedLeft,
						HorizontalOffset = _voiceChannelContentViewModel.ContextMenuTargetX,
						VerticalOffset = _voiceChannelContentViewModel.ContextMenuTargetY
					};
					_activeContextMenu.Closed += delegate
					{
						_voiceChannelContentViewModel.IsContextMenuOpen = false;
						_activeContextMenu = null;
					};
					_activeContextMenu.ShowAt(MainPanel, false);
				}
				else if (!_voiceChannelContentViewModel.IsContextMenuOpen && _activeContextMenu != null)
				{
					_activeContextMenu.Hide();
					_activeContextMenu = null;
				}
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
		ChannelIconContainer = nameScope?.Find<Border>("ChannelIconContainer");
		ChannelDescriptionDivider = nameScope?.Find<Rectangle>("ChannelDescriptionDivider");
		ChannelOptionsButton = nameScope?.Find<RootSvgButton>("ChannelOptionsButton");
		MainZoomControl = nameScope?.Find<LayoutTransformControl>("MainZoomControl");
		MediaBrowser = nameScope?.Find<BrowserView>("MediaBrowser");
		MainPanel = nameScope?.Find<Panel>("MainPanel");
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, VoiceChannelContentView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<VoiceChannelContentView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<VoiceChannelContentView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Content/VoiceChannelContentView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Content/VoiceChannelContentView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(16.0, GridUnitType.Pixel)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(12.0, GridUnitType.Pixel)
		});
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
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(60.0, GridUnitType.Pixel)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		global::Avalonia.Controls.Controls children = grid4.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		border4.Name = "ChannelIconContainer";
		object obj = border4;
		context.AvaloniaNameScope.Register("ChannelIconContainer", obj);
		Grid.SetColumn(border4, 1);
		Grid.SetRow(border4, 0);
		border4.Width = 26.0;
		border4.Height = 26.0;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2;
		CompiledBindingExtension compiledBindingExtension = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.IconAssetUri!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension3 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("StringNullOrEmptyToVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension3.Converter = (IValueConverter)obj2;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, isVisibleProperty, compiledBindingExtension4);
		border4.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		border4.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty, binding);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children2 = grid4.Children;
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
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj3);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		textBlock5.TextTrimming = TextTrimming.CharacterEllipsis;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("PrependStringConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj4 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding2.Converter = (IMultiValueConverter)obj4;
		IList<IBinding> bindings = multiBinding2.Bindings;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Name!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding2.Bindings;
		CompiledBindingExtension obj6 = new CompiledBindingExtension
		{
			Source = "# "
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item2 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, multiBinding);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children3 = grid4.Children;
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
		obj = rectangle5;
		context.AvaloniaNameScope.Register("ChannelDescriptionDivider", obj);
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
		CompiledBindingExtension compiledBindingExtension5 = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Description!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("StringNullOrEmptyToVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj7 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension6.Converter = (IValueConverter)obj7;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle5, isVisibleProperty2, compiledBindingExtension7);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		global::Avalonia.Controls.Controls children4 = grid4.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children4.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		Grid.SetColumn(textBlock9, 5);
		Grid.SetRow(textBlock9, 0);
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Description!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension9);
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding4);
		textBlock9.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj8 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj8);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		global::Avalonia.Controls.Controls children5 = grid4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children5.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		stackPanel4.Orientation = Orientation.Horizontal;
		Grid.SetColumn(stackPanel4, 6);
		Grid.SetRow(stackPanel4, 0);
		global::Avalonia.Controls.Controls children6 = stackPanel4.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children6.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton5 = rootSvgButton4;
		rootSvgButton5.Classes.Add("SvgDimmedButton");
		rootSvgButton5.Width = 32.0;
		rootSvgButton5.Height = 32.0;
		rootSvgButton5.SvgWidth = 21.0;
		rootSvgButton5.SvgHeight = 21.0;
		rootSvgButton5.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		StyledProperty<string> svgPathProperty = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("PopoutSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, svgPathProperty, binding5);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.PopoutCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, commandProperty, compiledBindingExtension11);
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
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.OpenInNewWindow;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj9);
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
		global::Avalonia.Controls.Controls children7 = stackPanel4.Children;
		RootSvgButton rootSvgButton7;
		RootSvgButton rootSvgButton6 = (rootSvgButton7 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton6).BeginInit();
		children7.Add(rootSvgButton6);
		RootSvgButton rootSvgButton8 = (rootSvgButton4 = rootSvgButton7);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton9 = rootSvgButton4;
		rootSvgButton9.Classes.Add("SvgDimmedButton");
		rootSvgButton9.Name = "ChannelOptionsButton";
		obj = rootSvgButton9;
		context.AvaloniaNameScope.Register("ChannelOptionsButton", obj);
		rootSvgButton9.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgButton9.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		rootSvgButton9.Width = 32.0;
		rootSvgButton9.Height = 32.0;
		StyledProperty<string> svgPathProperty2 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("EllipsisVerticalSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, svgPathProperty2, binding6);
		rootSvgButton9.SvgWidth = 4.0;
		rootSvgButton9.SvgHeight = 16.0;
		rootSvgButton9.AddHandler(InputElement.PointerPressedEvent, context.RootObject.onChannelOptionsButtonPointerPressed);
		ToolTip.SetPlacement(rootSvgButton9, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton9, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton9, 0.0);
		ToolTip.SetShowDelay(rootSvgButton9, 0);
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
		StyledProperty<ICommand?> commandProperty2 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.ShowEditChannelViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, commandProperty2, compiledBindingExtension13);
		StyledProperty<bool> isEnabledProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelFullControl!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, isEnabledProperty, compiledBindingExtension15);
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
		StyledProperty<ICommand?> commandProperty3 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.ShowDeleteChannelViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, commandProperty3, compiledBindingExtension17);
		StyledProperty<bool> isEnabledProperty2 = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension18 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelFullControl!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension19 = compiledBindingExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, isEnabledProperty2, compiledBindingExtension19);
		context.PopParent();
		((ISupportInitialize)menuItem8).EndInit();
		context.PopParent();
		rootSvgButton9.Flyout = flyout;
		RootToolTip rootToolTip7;
		RootToolTip rootToolTip6 = (rootToolTip7 = new RootToolTip());
		((ISupportInitialize)rootToolTip6).BeginInit();
		ToolTip.SetTip(rootSvgButton9, rootToolTip6);
		RootToolTip rootToolTip8 = (rootToolTip4 = rootToolTip7);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip9 = rootToolTip4;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		rootToolTip9.Content = textBlock14;
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ChannelOptions;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj10 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock17, obj10);
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
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		global::Avalonia.Controls.Controls children8 = grid4.Children;
		Rectangle rectangle7;
		Rectangle rectangle6 = (rectangle7 = new Rectangle());
		((ISupportInitialize)rectangle6).BeginInit();
		children8.Add(rectangle6);
		Rectangle rectangle8 = (rectangle4 = rectangle7);
		context.PushParent(rectangle4);
		Rectangle rectangle9 = rectangle4;
		Grid.SetColumn(rectangle9, 0);
		Grid.SetColumnSpan(rectangle9, 7);
		Grid.SetRow(rectangle9, 0);
		rectangle9.VerticalAlignment = VerticalAlignment.Bottom;
		rectangle9.Height = 0.5;
		StyledProperty<IBrush?> fillProperty2 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle9, fillProperty2, binding7);
		rectangle9.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rectangle8).EndInit();
		global::Avalonia.Controls.Controls children9 = grid4.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children9.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension20 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.ShouldRenderBrowser!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = compiledBindingExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel5, isVisibleProperty3, compiledBindingExtension21);
		Grid.SetRow(panel5, 1);
		Grid.SetColumn(panel5, 0);
		Grid.SetColumnSpan(panel5, 7);
		panel5.HorizontalAlignment = HorizontalAlignment.Stretch;
		panel5.VerticalAlignment = VerticalAlignment.Stretch;
		global::Avalonia.Controls.Controls children10 = panel5.Children;
		LayoutTransformControl layoutTransformControl2;
		LayoutTransformControl layoutTransformControl = (layoutTransformControl2 = new LayoutTransformControl());
		((ISupportInitialize)layoutTransformControl).BeginInit();
		children10.Add(layoutTransformControl);
		LayoutTransformControl layoutTransformControl4;
		LayoutTransformControl layoutTransformControl3 = (layoutTransformControl4 = layoutTransformControl2);
		context.PushParent(layoutTransformControl4);
		layoutTransformControl4.Name = "MainZoomControl";
		obj = layoutTransformControl4;
		context.AvaloniaNameScope.Register("MainZoomControl", obj);
		BrowserView browserView2;
		BrowserView browserView = (browserView2 = new BrowserView());
		((ISupportInitialize)browserView).BeginInit();
		layoutTransformControl4.Child = browserView;
		BrowserView browserView4;
		BrowserView browserView3 = (browserView4 = browserView2);
		context.PushParent(browserView4);
		browserView4.Name = "MediaBrowser";
		obj = browserView4;
		context.AvaloniaNameScope.Register("MediaBrowser", obj);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(browserView4, backgroundProperty2, binding8);
		browserView4.HorizontalAlignment = HorizontalAlignment.Stretch;
		browserView4.VerticalAlignment = VerticalAlignment.Stretch;
		context.PopParent();
		((ISupportInitialize)browserView3).EndInit();
		context.PopParent();
		((ISupportInitialize)layoutTransformControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		global::Avalonia.Controls.Controls children11 = grid4.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children11.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		panel9.Name = "MainPanel";
		obj = panel9;
		context.AvaloniaNameScope.Register("MainPanel", obj);
		Grid.SetRow(panel9, 1);
		Grid.SetColumn(panel9, 0);
		Grid.SetColumnSpan(panel9, 7);
		RootFlyout rootFlyout2;
		RootFlyout rootFlyout = (rootFlyout2 = new RootFlyout());
		context.PushParent(rootFlyout2);
		rootFlyout2.LimitSizeToWindow = false;
		StyledProperty<bool> isPopupOpenProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension obj11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.IsPopupOpen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension22 = obj11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootFlyout2, isPopupOpenProperty, compiledBindingExtension22);
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		rootFlyout2.Content = rootBorder;
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty3, binding9);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding10);
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootBorder4.Margin = new Thickness(12.0, 12.0, 12.0, 12.0);
		StyledProperty<BoxShadows> boxShadowProperty = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, boxShadowProperty, binding11);
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		rootBorder4.Child = rootScrollViewer;
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		rootScrollViewer4.Content = contentControl;
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		CompiledBindingExtension compiledBindingExtension23 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.VoiceChannelContentViewModel,RootApp.Client.Avalonia.MemberProfile!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension24 = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl4, compiledBindingExtension24);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		FlyoutBase.SetAttachedFlyout(panel9, rootFlyout);
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
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
	private static void !XamlIlPopulateTrampoline(VoiceChannelContentView P_0)
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
