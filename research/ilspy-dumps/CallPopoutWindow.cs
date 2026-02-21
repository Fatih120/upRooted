using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using CompiledAvaloniaXaml;
using DotNetBrowser.AvaloniaUi;
using DotNetBrowser.Browser;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Messages;

namespace RootApp.Client.Avalonia.UI.Call;

public class CallPopoutWindow : Window
{
	private UserContextMenuView? _activeContextMenu;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal LayoutTransformControl MainZoomPanel;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal DockPanel ContentWrapper;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal LayoutTransformControl BrowserZoomPanel;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal BrowserView MediaBrowser;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Panel MainPanel;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private CallPopoutViewModel _callPopoutViewModel => (CallPopoutViewModel)base.DataContext;

	public CallPopoutWindow(object P_0)
	{
		base.DataContext = P_0;
		InitializeComponent();
		if (base.DataContext is CallPopoutViewModel { TitleBarViewModel: null })
		{
			useNativeTitleBar();
		}
		if (_callPopoutViewModel.Browser != null)
		{
			Dispatcher.UIThread.Post(delegate
			{
				MediaBrowser.InitializeFrom(_callPopoutViewModel.Browser.DotNetBrowser);
			});
			WeakReferenceMessenger.Default.Register<BrowserDisposingMessage>(this, onBrowserDisposing);
		}
	}

	private void onBrowserDisposing(object recipient, BrowserDisposingMessage message)
	{
		if (_callPopoutViewModel.Browser?.Id == message.BrowserId)
		{
			BrowserZoomPanel.Child = null;
			Close();
		}
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		_callPopoutViewModel.PropertyChanged += onViewModelPropertyChanged;
		setZoom();
		_callPopoutViewModel.ZoomService.ZoomChanged += onZoomServiceZoomChanged;
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		_callPopoutViewModel.PropertyChanged -= onViewModelPropertyChanged;
		_callPopoutViewModel.ZoomService.ZoomChanged -= onZoomServiceZoomChanged;
	}

	private void onZoomServiceZoomChanged(double zoomLevel)
	{
		setZoom();
	}

	private void setZoom()
	{
		MainZoomPanel.LayoutTransform = new ScaleTransform(_callPopoutViewModel.ZoomService.Zoom, _callPopoutViewModel.ZoomService.Zoom);
		BrowserZoomPanel.LayoutTransform = new ScaleTransform(1.0 / _callPopoutViewModel.ZoomService.Zoom, 1.0 / _callPopoutViewModel.ZoomService.Zoom);
	}

	protected override void OnClosed(EventArgs P_0)
	{
		base.OnClosed(P_0);
		_callPopoutViewModel.Dispose();
		WeakReferenceMessenger.Default.Unregister<BrowserDisposingMessage>(this);
	}

	private void onViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "IsPopupOpen")
			{
				if (FlyoutBase.GetAttachedFlyout(MainPanel) is RootFlyout rootFlyout)
				{
					rootFlyout.Placement = PlacementMode.TopEdgeAlignedLeft;
					rootFlyout.HorizontalOffset = _callPopoutViewModel.PopupTargetX - 12.0;
					rootFlyout.VerticalOffset = _callPopoutViewModel.PopupTargetY;
					Popup flyoutPopup = rootFlyout.FlyoutPopup;
					if (_callPopoutViewModel.IsPopupOpen)
					{
						FlyoutBase.ShowAttachedFlyout(MainPanel);
						if (flyoutPopup.Host is PopupRoot popupRoot)
						{
							flyoutPopup.MinHeight = popupRoot.Bounds.Height;
							flyoutPopup.MaxHeight = popupRoot.Bounds.Height;
						}
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
				if (_callPopoutViewModel.IsContextMenuOpen && _callPopoutViewModel.UserContextMenu != null)
				{
					_activeContextMenu = new UserContextMenuView
					{
						DataContext = _callPopoutViewModel.UserContextMenu,
						Placement = PlacementMode.TopEdgeAlignedLeft,
						HorizontalOffset = _callPopoutViewModel.ContextMenuTargetX,
						VerticalOffset = _callPopoutViewModel.ContextMenuTargetY
					};
					_activeContextMenu.Closed += delegate
					{
						_callPopoutViewModel.IsContextMenuOpen = false;
						_activeContextMenu = null;
					};
					_activeContextMenu.ShowAt(MainPanel, false);
				}
				else if (!_callPopoutViewModel.IsContextMenuOpen && _activeContextMenu != null)
				{
					_activeContextMenu.Hide();
					_activeContextMenu = null;
				}
			}
		});
	}

	private void useNativeTitleBar()
	{
		base.ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.SystemChrome;
		base.ExtendClientAreaTitleBarHeightHint = -1.0;
		base.ExtendClientAreaToDecorationsHint = false;
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true, bool P_1 = true)
	{
		if (P_0)
		{
			!XamlIlPopulateTrampoline(this);
		}
		INameScope nameScope = this.FindNameScope();
		MainZoomPanel = nameScope?.Find<LayoutTransformControl>("MainZoomPanel");
		ContentWrapper = nameScope?.Find<DockPanel>("ContentWrapper");
		BrowserZoomPanel = nameScope?.Find<LayoutTransformControl>("BrowserZoomPanel");
		MediaBrowser = nameScope?.Find<BrowserView>("MediaBrowser");
		MainPanel = nameScope?.Find<Panel>("MainPanel");
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, CallPopoutWindow P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<CallPopoutWindow> context = new CompiledAvaloniaXaml.XamlIlContext.Context<CallPopoutWindow>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Call/CallPopoutWindow.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Call/CallPopoutWindow.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Title = "Root";
		P_1.Width = 1200.0;
		P_1.Height = 800.0;
		P_1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
		P_1.ExtendClientAreaToDecorationsHint = true;
		P_1.ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
		P_1.ExtendClientAreaTitleBarHeightHint = -1.0;
		LayoutTransformControl layoutTransformControl2;
		LayoutTransformControl layoutTransformControl = (layoutTransformControl2 = new LayoutTransformControl());
		((ISupportInitialize)layoutTransformControl).BeginInit();
		P_1.Content = layoutTransformControl;
		LayoutTransformControl layoutTransformControl4;
		LayoutTransformControl layoutTransformControl3 = (layoutTransformControl4 = layoutTransformControl2);
		context.PushParent(layoutTransformControl4);
		LayoutTransformControl layoutTransformControl5 = layoutTransformControl4;
		layoutTransformControl5.Name = "MainZoomPanel";
		object obj = layoutTransformControl5;
		context.AvaloniaNameScope.Register("MainZoomPanel", obj);
		DockPanel dockPanel2;
		DockPanel dockPanel = (dockPanel2 = new DockPanel());
		((ISupportInitialize)dockPanel).BeginInit();
		layoutTransformControl5.Child = dockPanel;
		DockPanel dockPanel4;
		DockPanel dockPanel3 = (dockPanel4 = dockPanel2);
		context.PushParent(dockPanel4);
		dockPanel4.HorizontalAlignment = HorizontalAlignment.Stretch;
		dockPanel4.VerticalAlignment = VerticalAlignment.Stretch;
		dockPanel4.Name = "ContentWrapper";
		obj = dockPanel4;
		context.AvaloniaNameScope.Register("ContentWrapper", obj);
		global::Avalonia.Controls.Controls children = dockPanel4.Children;
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		children.Add(contentControl);
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		ContentControl contentControl5 = contentControl4;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Call.CallPopoutViewModel,RootApp.Client.Avalonia.TitleBarViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl5, compiledBindingExtension2);
		DockPanel.SetDock(contentControl5, Dock.Top);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		global::Avalonia.Controls.Controls children2 = dockPanel4.Children;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		children2.Add(grid);
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		StyledProperty<IBrush?> backgroundProperty = Panel.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Panel.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid4, backgroundProperty, binding);
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children3 = grid4.Children;
		LayoutTransformControl layoutTransformControl7;
		LayoutTransformControl layoutTransformControl6 = (layoutTransformControl7 = new LayoutTransformControl());
		((ISupportInitialize)layoutTransformControl6).BeginInit();
		children3.Add(layoutTransformControl6);
		LayoutTransformControl layoutTransformControl8 = (layoutTransformControl4 = layoutTransformControl7);
		context.PushParent(layoutTransformControl4);
		LayoutTransformControl layoutTransformControl9 = layoutTransformControl4;
		layoutTransformControl9.Name = "BrowserZoomPanel";
		obj = layoutTransformControl9;
		context.AvaloniaNameScope.Register("BrowserZoomPanel", obj);
		Grid.SetRow(layoutTransformControl9, 0);
		layoutTransformControl9.Margin = new Thickness(2.0, 0.0, 2.0, 0.0);
		BrowserView browserView2;
		BrowserView browserView = (browserView2 = new BrowserView());
		((ISupportInitialize)browserView).BeginInit();
		layoutTransformControl9.Child = browserView;
		BrowserView browserView4;
		BrowserView browserView3 = (browserView4 = browserView2);
		context.PushParent(browserView4);
		browserView4.Name = "MediaBrowser";
		obj = browserView4;
		context.AvaloniaNameScope.Register("MediaBrowser", obj);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(browserView4, backgroundProperty2, binding2);
		browserView4.HorizontalAlignment = HorizontalAlignment.Stretch;
		browserView4.VerticalAlignment = VerticalAlignment.Stretch;
		context.PopParent();
		((ISupportInitialize)browserView3).EndInit();
		context.PopParent();
		((ISupportInitialize)layoutTransformControl8).EndInit();
		global::Avalonia.Controls.Controls children4 = grid4.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children4.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		panel4.Name = "MainPanel";
		obj = panel4;
		context.AvaloniaNameScope.Register("MainPanel", obj);
		Grid.SetRow(panel4, 0);
		Grid.SetRowSpan(panel4, 2);
		RootFlyout rootFlyout2;
		RootFlyout rootFlyout = (rootFlyout2 = new RootFlyout());
		context.PushParent(rootFlyout2);
		rootFlyout2.LimitSizeToWindow = false;
		StyledProperty<bool> isPopupOpenProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension obj2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Call.CallPopoutViewModel,RootApp.Client.Avalonia.IsPopupOpen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension3 = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootFlyout2, isPopupOpenProperty, compiledBindingExtension3);
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		rootFlyout2.Content = rootBorder;
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty3, binding3);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding4);
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootBorder4.Margin = new Thickness(12.0, 12.0, 12.0, 12.0);
		StyledProperty<BoxShadows> boxShadowProperty = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, boxShadowProperty, binding5);
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		rootBorder4.Child = rootScrollViewer;
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		ContentControl contentControl7;
		ContentControl contentControl6 = (contentControl7 = new ContentControl());
		((ISupportInitialize)contentControl6).BeginInit();
		rootScrollViewer4.Content = contentControl6;
		ContentControl contentControl8 = (contentControl4 = contentControl7);
		context.PushParent(contentControl4);
		ContentControl contentControl9 = contentControl4;
		CompiledBindingExtension compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Call.CallPopoutViewModel,RootApp.Client.Avalonia.MemberProfile!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl9, compiledBindingExtension5);
		context.PopParent();
		((ISupportInitialize)contentControl8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		FlyoutBase.SetAttachedFlyout(panel4, rootFlyout);
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		global::Avalonia.Controls.Controls children5 = grid4.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children5.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Grid.SetRow(border4, 1);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7;
		CompiledBindingExtension compiledBindingExtension6 = (compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Call.CallPopoutViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.VoiceCallConnectionStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build()));
		context.PushParent(compiledBindingExtension7);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("MediaRoomConnectionStatusToVisibleConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj3 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension7.Converter = (IValueConverter)obj3;
		compiledBindingExtension7.FallbackValue = "false";
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, isVisibleProperty, compiledBindingExtension8);
		ContentControl contentControl11;
		ContentControl contentControl10 = (contentControl11 = new ContentControl());
		((ISupportInitialize)contentControl10).BeginInit();
		border4.Child = contentControl10;
		ContentControl contentControl12 = (contentControl4 = contentControl11);
		context.PushParent(contentControl4);
		ContentControl contentControl13 = contentControl4;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Call.CallPopoutViewModel,RootApp.Client.Avalonia.VoiceBarViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl13, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)contentControl12).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)dockPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)layoutTransformControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void !XamlIlPopulateTrampoline(CallPopoutWindow P_0)
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
