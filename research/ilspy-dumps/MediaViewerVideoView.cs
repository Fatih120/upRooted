using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using DotNetBrowser.AvaloniaUi;
using DotNetBrowser.Browser;
using DotNetBrowser.Browser.FullScreen.Events;
using Microsoft.VisualStudio.Threading;
using RootApp.Client.Avalonia.UI.Main;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public class MediaViewerVideoView : UserControl
{
	private double _currentZoom = 1.0;

	private FullScreenMediaViewerVideoWindow? _fullScreenWindow;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl MainControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal LayoutTransformControl MainZoomControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal ContentControl MainContent;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal BrowserView MediaBrowser;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private MediaViewerVideoViewModel _mediaViewerVideoViewModel => (MediaViewerVideoViewModel)base.DataContext;

	public MediaViewerVideoView()
	{
		InitializeComponent();
		Dispatcher.UIThread.Post(delegate
		{
			MediaBrowser.InitializeFrom(_mediaViewerVideoViewModel.Browser);
			string text = Convert.ToBase64String(Encoding.UTF8.GetBytes(BuildHtmlPage(Path.Combine(_mediaViewerVideoViewModel.VideoPath, "iframe?autoplay=true&preload=true"))));
			string url = "data:text/html;base64," + text;
			_mediaViewerVideoViewModel.Browser.Navigation.LoadUrl(url).Forget();
		});
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		setZoom();
		_mediaViewerVideoViewModel.ZoomService.ZoomChanged += onZoomServiceZoomChanged;
		_mediaViewerVideoViewModel.Browser.FullScreen.Entered += onFullScreenEntered;
		_mediaViewerVideoViewModel.Browser.FullScreen.Exited += onFullScreenExited;
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		_mediaViewerVideoViewModel.ZoomService.ZoomChanged -= onZoomServiceZoomChanged;
		_mediaViewerVideoViewModel.Browser.FullScreen.Entered -= onFullScreenEntered;
		_mediaViewerVideoViewModel.Browser.FullScreen.Exited -= onFullScreenExited;
		_mediaViewerVideoViewModel.Browser.Dispose();
		cleanupFullScreenWindow();
	}

	private void onFullScreenEntered(object? sender, FullScreenEnteredEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			_fullScreenWindow = new FullScreenMediaViewerVideoWindow();
			_fullScreenWindow.Closed += onFullScreenWindowClosed;
			if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: MainWindow mainWindow })
			{
				_fullScreenWindow.SetContent(_mediaViewerVideoViewModel.Browser, mainWindow);
				if (false)
				{
				}
				_fullScreenWindow.Show(mainWindow);
			}
		});
	}

	private void onFullScreenExited(object? sender, FullScreenExitedEventArgs e)
	{
		cleanupFullScreenWindow();
	}

	private void cleanupFullScreenWindow()
	{
		if (_fullScreenWindow == null)
		{
			return;
		}
		FullScreenMediaViewerVideoWindow fullScreenWindow = _fullScreenWindow;
		_fullScreenWindow = null;
		if (fullScreenWindow != null)
		{
			Dispatcher.UIThread.Post(delegate
			{
				fullScreenWindow.Closed -= onFullScreenWindowClosed;
				fullScreenWindow.SetContent(null, null);
				MediaBrowser.InitializeFrom(_mediaViewerVideoViewModel.Browser);
				fullScreenWindow.Close();
			});
		}
	}

	private void onFullScreenWindowClosed(object? sender, EventArgs e)
	{
		_mediaViewerVideoViewModel.Browser.FullScreen.Exit();
	}

	private void onZoomServiceZoomChanged(double zoomLevel)
	{
		setZoom();
	}

	private void setZoom()
	{
		if (_currentZoom != _mediaViewerVideoViewModel.ZoomService.Zoom)
		{
			Dispatcher.UIThread.Post(delegate
			{
				_currentZoom = _mediaViewerVideoViewModel.ZoomService.Zoom;
				MainZoomControl.LayoutTransform = new ScaleTransform(1.0 / _currentZoom, 1.0 / _currentZoom);
			});
		}
	}

	private string BuildHtmlPage(string P_0)
	{
		return "\r\n<html>\r\n  <head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n    <script src=\"https://embed.cloudflarestream.com/embed/sdk.latest.js\"></script>\r\n    <style>\r\n      html, body {\r\n        margin: 0;\r\n        padding: 0;\r\n        height: 100%;\r\n        width: 100%;\r\n        overflow: hidden;\r\n        background-color: black;\r\n      }\r\n      iframe {\r\n        border: none;\r\n        position: absolute;\r\n        top: 0;\r\n        left: 0;\r\n        height: 100%;\r\n        width: 100%;\r\n      }\r\n    </style>\r\n  </head>\r\n  <body>\r\n    <iframe\r\n      id=\"stream-player\"\r\n      src=\"" + P_0 + "\"\r\n      allow=\"accelerometer; gyroscope; encrypted-media; autoplay; fullscreen; picture-in-picture\"\r\n    ></iframe>\r\n\r\n    <script>\r\n      const player = Stream(document.getElementById('stream-player'));\r\n      player.volume = 0.5;\r\n      player.addEventListener('play', () => {\r\n        console.log('playing!');\r\n      });\r\n      player.play().catch(() => {\r\n        console.log('playback failed, muting to try again');\r\n        player.muted = true;\r\n        player.play();\r\n      });\r\n    </script>\r\n  </body>\r\n</html>";
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
		MainControl = nameScope?.Find<UserControl>("MainControl");
		MainZoomControl = nameScope?.Find<LayoutTransformControl>("MainZoomControl");
		MainContent = nameScope?.Find<ContentControl>("MainContent");
		MediaBrowser = nameScope?.Find<BrowserView>("MediaBrowser");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, MediaViewerVideoView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MediaViewerVideoView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MediaViewerVideoView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FMediaViewer_002FMediaViewerVideoView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/MediaViewer/MediaViewerVideoView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Name = "MainControl";
		object obj = P_1;
		context.AvaloniaNameScope.Register("MainControl", obj);
		LayoutTransformControl layoutTransformControl2;
		LayoutTransformControl layoutTransformControl = (layoutTransformControl2 = new LayoutTransformControl());
		((ISupportInitialize)layoutTransformControl).BeginInit();
		P_1.Content = layoutTransformControl;
		LayoutTransformControl layoutTransformControl4;
		LayoutTransformControl layoutTransformControl3 = (layoutTransformControl4 = layoutTransformControl2);
		context.PushParent(layoutTransformControl4);
		layoutTransformControl4.Name = "MainZoomControl";
		obj = layoutTransformControl4;
		context.AvaloniaNameScope.Register("MainZoomControl", obj);
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		layoutTransformControl4.Child = contentControl;
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
	private static void _0021XamlIlPopulateTrampoline(MediaViewerVideoView P_0)
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
