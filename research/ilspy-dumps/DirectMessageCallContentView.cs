using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
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
using RootApp.Client.Avalonia.Messages;

namespace RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages;

public class DirectMessageCallContentView : UserControl
{
	private double _currentZoom = 1.0;

	private UserContextMenuView? _activeContextMenu;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal LayoutTransformControl MainZoomControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal BrowserView MediaBrowser;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Panel MainPanel;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private DirectMessageCallContentViewModel _directMessageCallContentViewModel => (DirectMessageCallContentViewModel)base.DataContext;

	public DirectMessageCallContentView()
	{
		InitializeComponent();
		Dispatcher.UIThread.Post(delegate
		{
			DirectMessageCallContentViewModel directMessageCallContentViewModel = _directMessageCallContentViewModel;
			if (directMessageCallContentViewModel != null && directMessageCallContentViewModel.Browser != null)
			{
				MediaBrowser.InitializeFrom(_directMessageCallContentViewModel.Browser.DotNetBrowser);
			}
		});
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		setZoom();
		_directMessageCallContentViewModel.PropertyChanged += onViewModelPropertyChanged;
		_directMessageCallContentViewModel.ZoomService.ZoomChanged += onZoomServiceZoomChanged;
		WeakReferenceMessenger.Default.Register<BrowserDisposingMessage>(this, onBrowserDisposing);
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		_directMessageCallContentViewModel.PropertyChanged -= onViewModelPropertyChanged;
		_directMessageCallContentViewModel.ZoomService.ZoomChanged -= onZoomServiceZoomChanged;
		WeakReferenceMessenger.Default.Unregister<BrowserDisposingMessage>(this);
	}

	private void onBrowserDisposing(object recipient, BrowserDisposingMessage message)
	{
		if (_directMessageCallContentViewModel.Browser?.Id == message.BrowserId)
		{
			MainZoomControl.Child = null;
		}
	}

	private void onZoomServiceZoomChanged(double zoomLevel)
	{
		setZoom();
	}

	private void setZoom()
	{
		if (_currentZoom == _directMessageCallContentViewModel.ZoomService.Zoom)
		{
			return;
		}
		Dispatcher.UIThread.Post(delegate
		{
			_currentZoom = _directMessageCallContentViewModel.ZoomService.Zoom;
			MainZoomControl.LayoutTransform = new ScaleTransform(1.0 / _currentZoom, 1.0 / _currentZoom);
			if (_directMessageCallContentViewModel.Browser != null)
			{
				_directMessageCallContentViewModel.Browser.DotNetBrowser.Zoom.Level = _directMessageCallContentViewModel.ZoomService.ToNearestDotNetBrowserZoomLevel();
			}
		});
	}

	private void onViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "IsPopupOpen")
			{
				if (FlyoutBase.GetAttachedFlyout(MainPanel) is RootFlyout rootFlyout)
				{
					double zoom = _directMessageCallContentViewModel.ZoomService.Zoom;
					rootFlyout.Placement = PlacementMode.TopEdgeAlignedLeft;
					rootFlyout.HorizontalOffset = (_directMessageCallContentViewModel.PopupTargetX - 12.0) * zoom;
					rootFlyout.VerticalOffset = _directMessageCallContentViewModel.PopupTargetY * zoom;
					Popup flyoutPopup = rootFlyout.FlyoutPopup;
					if (_directMessageCallContentViewModel.IsPopupOpen)
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
				if (_directMessageCallContentViewModel.IsContextMenuOpen && _directMessageCallContentViewModel.UserContextMenu != null)
				{
					_activeContextMenu = new UserContextMenuView
					{
						DataContext = _directMessageCallContentViewModel.UserContextMenu,
						Placement = PlacementMode.TopEdgeAlignedLeft,
						HorizontalOffset = _directMessageCallContentViewModel.ContextMenuTargetX,
						VerticalOffset = _directMessageCallContentViewModel.ContextMenuTargetY
					};
					_activeContextMenu.Closed += delegate
					{
						_directMessageCallContentViewModel.IsContextMenuOpen = false;
						_activeContextMenu = null;
					};
					_activeContextMenu.ShowAt(MainPanel, false);
				}
				else if (!_directMessageCallContentViewModel.IsContextMenuOpen && _activeContextMenu != null)
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
		MainZoomControl = nameScope?.Find<LayoutTransformControl>("MainZoomControl");
		MediaBrowser = nameScope?.Find<BrowserView>("MediaBrowser");
		MainPanel = nameScope?.Find<Panel>("MainPanel");
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, DirectMessageCallContentView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageCallContentView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageCallContentView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Home/SystemTray/DirectMessages/DirectMessageCallContentView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/SystemTray/DirectMessages/DirectMessageCallContentView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		panel5.Margin = new Thickness(1.0, 0.0, 1.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty = Panel.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Panel.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel5, backgroundProperty, binding);
		global::Avalonia.Controls.Controls children = panel5.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageCallContentViewModel,RootApp.Client.Avalonia.ShouldRenderBrowser!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel9, isVisibleProperty, compiledBindingExtension2);
		panel9.HorizontalAlignment = HorizontalAlignment.Stretch;
		panel9.VerticalAlignment = VerticalAlignment.Stretch;
		global::Avalonia.Controls.Controls children2 = panel9.Children;
		LayoutTransformControl layoutTransformControl2;
		LayoutTransformControl layoutTransformControl = (layoutTransformControl2 = new LayoutTransformControl());
		((ISupportInitialize)layoutTransformControl).BeginInit();
		children2.Add(layoutTransformControl);
		LayoutTransformControl layoutTransformControl4;
		LayoutTransformControl layoutTransformControl3 = (layoutTransformControl4 = layoutTransformControl2);
		context.PushParent(layoutTransformControl4);
		layoutTransformControl4.Name = "MainZoomControl";
		object obj = layoutTransformControl4;
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
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(browserView4, backgroundProperty2, binding2);
		browserView4.HorizontalAlignment = HorizontalAlignment.Stretch;
		browserView4.VerticalAlignment = VerticalAlignment.Stretch;
		context.PopParent();
		((ISupportInitialize)browserView3).EndInit();
		context.PopParent();
		((ISupportInitialize)layoutTransformControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		global::Avalonia.Controls.Controls children3 = panel5.Children;
		Panel panel11;
		Panel panel10 = (panel11 = new Panel());
		((ISupportInitialize)panel10).BeginInit();
		children3.Add(panel10);
		Panel panel12 = (panel4 = panel11);
		context.PushParent(panel4);
		Panel panel13 = panel4;
		panel13.Name = "MainPanel";
		obj = panel13;
		context.AvaloniaNameScope.Register("MainPanel", obj);
		RootFlyout rootFlyout2;
		RootFlyout rootFlyout = (rootFlyout2 = new RootFlyout());
		context.PushParent(rootFlyout2);
		rootFlyout2.LimitSizeToWindow = false;
		StyledProperty<bool> isPopupOpenProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension obj2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageCallContentViewModel,RootApp.Client.Avalonia.IsPopupOpen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
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
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		rootScrollViewer4.Content = contentControl;
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		CompiledBindingExtension compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageCallContentViewModel,RootApp.Client.Avalonia.MemberProfile!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl4, compiledBindingExtension5);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		FlyoutBase.SetAttachedFlyout(panel13, rootFlyout);
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
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
	private static void !XamlIlPopulateTrampoline(DirectMessageCallContentView P_0)
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
