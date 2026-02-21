using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Platform;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using DotNetBrowser.AvaloniaUi;
using DotNetBrowser.Browser;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public class FullScreenMediaViewerVideoWindow : Window
{
	private IBrowser? _browser;

	private IDisposable? _windowStateSubscription;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Window MainControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal ContentControl MainContent;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal BrowserView MediaBrowser;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public FullScreenMediaViewerVideoWindow()
	{
		InitializeComponent();
	}

	public void SetContent(IBrowser? P_0, Window? P_1)
	{
		_browser = P_0;
		if (P_1 != null && false)
		{
			Screen screen = P_1.Screens.ScreenFromWindow(P_1);
			if (screen == null)
			{
				screen = P_1.Screens.Primary;
			}
			if (screen != null)
			{
				base.Position = new PixelPoint(screen.Bounds.X, screen.Bounds.Y);
			}
		}
	}

	protected override void OnOpened(EventArgs P_0)
	{
		base.OnOpened(P_0);
		_windowStateSubscription = this.GetObservable(Window.WindowStateProperty).Subscribe(delegate
		{
			if (false)
			{
			}
			if (false)
			{
				Dispatcher.UIThread.Post(base.Close);
			}
		});
	}

	protected override void OnClosed(EventArgs P_0)
	{
		base.OnClosed(P_0);
		_windowStateSubscription?.Dispose();
		_windowStateSubscription = null;
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		base.KeyDown += OnKeyDown;
		if (_browser != null)
		{
			MediaBrowser.InitializeFrom(_browser);
		}
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		base.KeyDown -= OnKeyDown;
	}

	private void OnKeyDown(object? sender, KeyEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.Key == Key.Escape || e.Key == Key.F11)
			{
				e.Handled = true;
				Close();
			}
		});
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true, bool P_1 = true)
	{
		if (P_0)
		{
			_0021XamlIlPopulateTrampoline(this);
		}
		INameScope nameScope = this.FindNameScope();
		MainControl = nameScope?.Find<Window>("MainControl");
		MainContent = nameScope?.Find<ContentControl>("MainContent");
		MediaBrowser = nameScope?.Find<BrowserView>("MediaBrowser");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, FullScreenMediaViewerVideoWindow P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<FullScreenMediaViewerVideoWindow> context = new CompiledAvaloniaXaml.XamlIlContext.Context<FullScreenMediaViewerVideoWindow>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FMediaViewer_002FFullScreenMediaViewerVideoWindow_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/MediaViewer/FullScreenMediaViewerVideoWindow.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Title = "Root Video Player";
		P_1.ShowInTaskbar = false;
		P_1.Background = new ImmutableSolidColorBrush(4278190080u);
		P_1.Topmost = true;
		P_1.TransparencyLevelHint = new WindowTransparencyLevel[1] { WindowTransparencyLevel.None };
		P_1.WindowState = WindowState.FullScreen;
		P_1.WindowStartupLocation = WindowStartupLocation.CenterOwner;
		P_1.Name = "MainControl";
		object obj = P_1;
		context.AvaloniaNameScope.Register("MainControl", obj);
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		P_1.Content = contentControl;
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		contentControl4.Name = "MainContent";
		obj = contentControl4;
		context.AvaloniaNameScope.Register("MainContent", obj);
		BrowserView browserView2;
		BrowserView browserView = (browserView2 = new BrowserView());
		((ISupportInitialize)browserView).BeginInit();
		contentControl4.Content = browserView;
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
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(FullScreenMediaViewerVideoWindow P_0)
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
