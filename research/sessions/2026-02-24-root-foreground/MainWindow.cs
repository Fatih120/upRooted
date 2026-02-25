using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
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
using Avalonia.Platform;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Keybindings;
using RootApp.Client.Domain.Helpers.Store;
using RootApp.Client.Domain.Helpers.Store.State.Window;

namespace RootApp.Client.Avalonia.UI.Main;

public class MainWindow : Window
{
	private readonly ILocalDataStore? _localDataStore;

	private readonly KeybindingDispatchService? _keybindingDispatchService;

	private PixelPoint _lastPosition;

	private Size _lastSize;

	private bool _isRestoring;

	private WindowState _stateBeforeHide;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootZoomContainer ZoomContainer;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal DockPanel ContentWrapper;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private MainViewModel _mainViewModel => (MainViewModel)base.DataContext;

	public MainWindow(object P_0, ILocalDataStore P_1, KeybindingDispatchService P_2)
	{
		base.DataContext = P_0;
		_localDataStore = P_1;
		_keybindingDispatchService = P_2;
		InitializeComponent();
		ZoomContainer.SetZoomService(_mainViewModel.ZoomService);
		if (_mainViewModel.TitleBarViewModel == null)
		{
			useNativeTitleBar();
		}
		restoreWindowState();
		base.PositionChanged += onPositionChanged;
		base.PropertyChanged += onPropertyChanged;
		AddHandler(InputElement.KeyDownEvent, onKeyDown, RoutingStrategies.Tunnel);
		AddHandler(InputElement.PointerPressedEvent, onPointerPressed, RoutingStrategies.Tunnel);
	}

	private void onKeyDown(object? sender, KeyEventArgs e)
	{
		if (_keybindingDispatchService != null && _keybindingDispatchService.HandleKeyDown(e))
		{
			e.Handled = true;
		}
	}

	private void onPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		if (_keybindingDispatchService != null && _keybindingDispatchService.HandlePointerPressed(e))
		{
			e.Handled = true;
		}
	}

	private void restoreWindowState()
	{
		if (_localDataStore == null || !_localDataStore.TryGetGlobal(DataStoreKeys.WindowState, out var savedWindowState, SavedWindowStateSerializerContext.Default.SavedWindowState) || savedWindowState == null)
		{
			return;
		}
		_isRestoring = true;
		if (savedWindowState.Width > 0.0 && savedWindowState.Height > 0.0)
		{
			base.Width = savedWindowState.Width;
			base.Height = savedWindowState.Height;
		}
		Screens screens = base.Screens;
		if (screens != null)
		{
			int num = (int)savedWindowState.X;
			int num2 = (int)savedWindowState.Y;
			bool flag = false;
			foreach (Screen item in screens.All)
			{
				PixelRect workingArea = item.WorkingArea;
				if (num < workingArea.Right - 50 && (double)num + base.Width > (double)(workingArea.X + 50) && num2 < workingArea.Bottom - 50 && (double)num2 + base.Height > (double)(workingArea.Y + 50))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				base.WindowStartupLocation = WindowStartupLocation.Manual;
				base.Position = new PixelPoint(num, num2);
			}
		}
		_lastSize = new Size(savedWindowState.Width, savedWindowState.Height);
		_lastPosition = base.Position;
		if (savedWindowState.IsMaximized)
		{
			base.Opened += delegate
			{
				base.WindowState = WindowState.Maximized;
			};
		}
		_isRestoring = false;
	}

	private void onPositionChanged(object? sender, PixelPointEventArgs e)
	{
		if (!_isRestoring && base.WindowState == WindowState.Normal)
		{
			_lastPosition = e.Point;
		}
	}

	private void onPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
	{
		if (e.Property == Visual.BoundsProperty && !_isRestoring && base.WindowState == WindowState.Normal)
		{
			_lastSize = base.Bounds.Size;
		}
	}

	public void SaveWindowState()
	{
		if (_localDataStore != null)
		{
			SavedWindowState savedWindowState = new SavedWindowState
			{
				X = ((base.WindowState == WindowState.Maximized) ? _lastPosition.X : base.Position.X),
				Y = ((base.WindowState == WindowState.Maximized) ? _lastPosition.Y : base.Position.Y),
				Width = ((base.WindowState == WindowState.Maximized) ? _lastSize.Width : base.Bounds.Width),
				Height = ((base.WindowState == WindowState.Maximized) ? _lastSize.Height : base.Bounds.Height),
				IsMaximized = (base.WindowState == WindowState.Maximized)
			};
			_localDataStore.SetGlobal(DataStoreKeys.WindowState, savedWindowState, SavedWindowStateSerializerContext.Default.SavedWindowState);
		}
	}

	public void HideToTray()
	{
		_stateBeforeHide = base.WindowState;
		Hide();
	}

	public void RestoreFromTray()
	{
		if (!base.IsVisible)
		{
			Show();
			base.WindowState = _stateBeforeHide;
		}
		else if (base.WindowState == WindowState.Minimized)
		{
			base.WindowState = WindowState.Normal;
		}
		Activate();
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
		ZoomContainer = nameScope?.Find<RootZoomContainer>("ZoomContainer");
		ContentWrapper = nameScope?.Find<DockPanel>("ContentWrapper");
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, MainWindow P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MainWindow> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MainWindow>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Main/MainWindow.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Main/MainWindow.axaml")
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
		P_1.MinWidth = 850.0;
		P_1.MinHeight = 480.0;
		P_1.ExtendClientAreaToDecorationsHint = true;
		P_1.ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
		P_1.ExtendClientAreaTitleBarHeightHint = -1.0;
		RootZoomContainer rootZoomContainer2;
		RootZoomContainer rootZoomContainer = (rootZoomContainer2 = new RootZoomContainer());
		((ISupportInitialize)rootZoomContainer).BeginInit();
		P_1.Content = rootZoomContainer;
		RootZoomContainer rootZoomContainer4;
		RootZoomContainer rootZoomContainer3 = (rootZoomContainer4 = rootZoomContainer2);
		context.PushParent(rootZoomContainer4);
		rootZoomContainer4.Name = "ZoomContainer";
		object obj = rootZoomContainer4;
		context.AvaloniaNameScope.Register("ZoomContainer", obj);
		DockPanel dockPanel2;
		DockPanel dockPanel = (dockPanel2 = new DockPanel());
		((ISupportInitialize)dockPanel).BeginInit();
		rootZoomContainer4.Child = dockPanel;
		DockPanel dockPanel4;
		DockPanel dockPanel3 = (dockPanel4 = dockPanel2);
		context.PushParent(dockPanel4);
		DockPanel dockPanel5 = dockPanel4;
		dockPanel5.HorizontalAlignment = HorizontalAlignment.Stretch;
		dockPanel5.VerticalAlignment = VerticalAlignment.Stretch;
		dockPanel5.Name = "ContentWrapper";
		obj = dockPanel5;
		context.AvaloniaNameScope.Register("ContentWrapper", obj);
		global::Avalonia.Controls.Controls children = dockPanel5.Children;
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		children.Add(contentControl);
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		ContentControl contentControl5 = contentControl4;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Main.MainViewModel,RootApp.Client.Avalonia.TitleBarViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl5, compiledBindingExtension2);
		DockPanel.SetDock(contentControl5, Dock.Top);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		global::Avalonia.Controls.Controls children2 = dockPanel5.Children;
		DockPanel dockPanel7;
		DockPanel dockPanel6 = (dockPanel7 = new DockPanel());
		((ISupportInitialize)dockPanel6).BeginInit();
		children2.Add(dockPanel6);
		DockPanel dockPanel8 = (dockPanel4 = dockPanel7);
		context.PushParent(dockPanel4);
		DockPanel dockPanel9 = dockPanel4;
		StyledProperty<IBrush?> backgroundProperty = Panel.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Panel.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(dockPanel9, backgroundProperty, binding);
		global::Avalonia.Controls.Controls children3 = dockPanel9.Children;
		ContentControl contentControl7;
		ContentControl contentControl6 = (contentControl7 = new ContentControl());
		((ISupportInitialize)contentControl6).BeginInit();
		children3.Add(contentControl6);
		ContentControl contentControl8 = (contentControl4 = contentControl7);
		context.PushParent(contentControl4);
		ContentControl contentControl9 = contentControl4;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension();
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl9, compiledBindingExtension4);
		context.PopParent();
		((ISupportInitialize)contentControl8).EndInit();
		context.PopParent();
		((ISupportInitialize)dockPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)dockPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootZoomContainer3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void !XamlIlPopulateTrampoline(MainWindow P_0)
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
