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
using CompiledAvaloniaXaml;
using DotNetBrowser.AvaloniaUi;
using DotNetBrowser.Browser;

namespace RootApp.Client.Avalonia.UI.Community.Content;

public class AppChannelContentView : UserControl
{
	private double _currentZoom = 1.0;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal LayoutTransformControl MainZoomControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal BrowserView MediaBrowser;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private AppChannelContentViewModel _appChannelContentViewModel => (AppChannelContentViewModel)base.DataContext;

	public AppChannelContentView()
	{
		InitializeComponent();
		Dispatcher.UIThread.Post(delegate
		{
			AppChannelContentViewModel appChannelContentViewModel = _appChannelContentViewModel;
			if (appChannelContentViewModel != null && appChannelContentViewModel.Browser != null)
			{
				MediaBrowser.InitializeFrom(_appChannelContentViewModel.Browser.DotNetBrowser);
			}
			_appChannelContentViewModel.BrowserRecreated += onBrowserRecreated;
		});
	}

	private void onBrowserRecreated()
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (_appChannelContentViewModel.Browser != null)
			{
				MediaBrowser.InitializeFrom(_appChannelContentViewModel.Browser.DotNetBrowser);
				setZoom();
			}
		});
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		setZoom();
		_appChannelContentViewModel.ZoomService.ZoomChanged += onZoomServiceZoomChanged;
		_appChannelContentViewModel.OnBrowserBecameVisible();
		IBrowser browser = _appChannelContentViewModel.Browser?.DotNetBrowser;
		if (browser != null && !browser.IsDisposed)
		{
			browser.Audio.Muted = false;
		}
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		_appChannelContentViewModel.ZoomService.ZoomChanged -= onZoomServiceZoomChanged;
		IBrowser browser = _appChannelContentViewModel.Browser?.DotNetBrowser;
		if (browser != null && !browser.IsDisposed)
		{
			browser.Audio.Muted = true;
		}
		_appChannelContentViewModel.OnBrowserBecameInvisible();
	}

	private void onZoomServiceZoomChanged(double zoomLevel)
	{
		setZoom();
	}

	private void setZoom()
	{
		if (_currentZoom == _appChannelContentViewModel.ZoomService.Zoom)
		{
			return;
		}
		Dispatcher.UIThread.Post(delegate
		{
			_currentZoom = _appChannelContentViewModel.ZoomService.Zoom;
			MainZoomControl.LayoutTransform = new ScaleTransform(1.0 / _currentZoom, 1.0 / _currentZoom);
			if (_appChannelContentViewModel.Browser != null)
			{
				_appChannelContentViewModel.Browser.DotNetBrowser.Zoom.Level = _appChannelContentViewModel.ZoomService.ToNearestDotNetBrowserZoomLevel();
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
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, AppChannelContentView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<AppChannelContentView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<AppChannelContentView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Content/AppChannelContentView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Content/AppChannelContentView.axaml")
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
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.AppChannelContentViewModel,RootApp.Client.Avalonia.ShouldRenderBrowser!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel4, isVisibleProperty, compiledBindingExtension2);
		panel4.HorizontalAlignment = HorizontalAlignment.Stretch;
		panel4.VerticalAlignment = VerticalAlignment.Stretch;
		global::Avalonia.Controls.Controls children = panel4.Children;
		LayoutTransformControl layoutTransformControl2;
		LayoutTransformControl layoutTransformControl = (layoutTransformControl2 = new LayoutTransformControl());
		((ISupportInitialize)layoutTransformControl).BeginInit();
		children.Add(layoutTransformControl);
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
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(browserView4, backgroundProperty, binding);
		browserView4.HorizontalAlignment = HorizontalAlignment.Stretch;
		browserView4.VerticalAlignment = VerticalAlignment.Stretch;
		context.PopParent();
		((ISupportInitialize)browserView3).EndInit();
		context.PopParent();
		((ISupportInitialize)layoutTransformControl3).EndInit();
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
	private static void !XamlIlPopulateTrampoline(AppChannelContentView P_0)
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
