using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Platform;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using Avalonia.Themes.Simple;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Threading;
using RootApp.Browser;
using RootApp.Client.Avalonia.DI;
using RootApp.Client.Avalonia.Helpers.Activation;
using RootApp.Client.Avalonia.Helpers.Keybindings;
using RootApp.Client.Avalonia.Helpers.LinkJoining;
using RootApp.Client.Avalonia.Helpers.Sentry;
using RootApp.Client.Avalonia.Resources.Converters;
using RootApp.Client.Avalonia.Resources.Converters.CommunityMembers;
using RootApp.Client.Avalonia.Resources.Converters.Files;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.Resources.Themes;
using RootApp.Client.Avalonia.UI.Main;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Utils.Extensions;
using RootApp.Client.Domain.Helpers.ApplicationConfiguration;
using RootApp.Client.Domain.Helpers.Store;
using RootApp.Core;
using RootApp.Utility;
using RootApp.WebApi.Shared.Enums;
using Sentry;
using Sentry.Extensions.Logging;

namespace RootApp.Client.Avalonia;

public class App : Application
{
	[CompilerGenerated]
	private class XamlClosure_1
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new ScrollToVisibilityConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<App> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = new CompiledAvaloniaXaml.XamlIlContext.Context<App>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/App.axaml.Singleton }, "avares://RootApp.Client.Avalonia/App.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (App)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new BoolInverterConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new MultiBoolInverterConverter();
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new GreaterThanZeroToTrueConverter();
		}

		public static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new StringNullOrEmptyToVisibilityConverter();
		}

		public static object Build_6(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new StringFormatConverter();
		}

		public static object Build_7(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new EmptyStringFallbackConverter();
		}

		public static object Build_8(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new OnlineTextStatusConverter();
		}

		public static object Build_9(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new OnlineStatusToIndexConverter();
		}

		public static object Build_10(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new PrependStringConverter();
		}

		public static object Build_11(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new AndConverter();
		}

		public static object Build_12(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new OrConverter();
		}

		public static object Build_13(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new MediaRoomConnectionStatusToVisibleConverter();
		}

		public static object Build_14(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new BoolToWidthConverter();
		}

		public static object Build_15(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new ByteSizeToReadableStringConverter();
		}

		public static object Build_16(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new MarginIfFalseConverter();
		}

		public static object Build_17(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new MarginIfTrueConverter();
		}

		public static object Build_18(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new PercentageToVisibilityConverter();
		}

		public static object Build_19(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new VisibilityToOpacityConverter();
		}

		public static object Build_20(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new BoolToOpacityConverter();
		}

		public static object Build_21(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new BoolToTopBorderThicknessConverter();
		}

		public static object Build_22(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new PathToBitmapConverter();
		}

		public static object Build_23(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new ItemsControlHasItemsConverter();
		}

		public static object Build_24(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new ScrollViewerScrollableMarginConverter();
		}

		public static object Build_25(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new MenuItemToggleTypeConverter();
		}

		public static object Build_26(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<App> context = CreateContext(P_0);
			return new EnumToBoolConverter();
		}
	}

	private readonly RootAppVersion _appVersion;

	private readonly IHostApplicationLifetime _hostApplicationLifetime;

	private readonly ThemeService _themeService;

	private readonly BrowserService _browserService;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly IApplicationRestart _applicationRestart;

	private readonly ILocalDataStore _localDataStore;

	private readonly IOptions<RootApplicationOptions> _rootApplicationOptions;

	private readonly LinkJoiningService _linkJoiningService;

	private readonly KeybindingDispatchService _keybindingDispatchService;

	private readonly ILogger<App> _logger;

	private readonly HostedServicesRunner _servicesRunner;

	private readonly MainViewModelFactory _mainViewModelFactory;

	private TaskHandle _openWindowTask;

	private SynchronizationContext? _synchronizationContext;

	private bool _closeToTray;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	public string AppVersion => _appVersion.Version.ToString();

	public App(MainViewModelFactory P_0, RootAppVersion P_1, ILoggerFactory P_2, HostedServicesRunner P_3, IHostApplicationLifetime P_4, ThemeService P_5, BrowserService P_6, IRootSessionAccessor P_7, IApplicationRestart P_8, ILocalDataStore P_9, IOptions<RootApplicationOptions> P_10, LinkJoiningService P_11, KeybindingDispatchService P_12)
	{
		_appVersion = P_1;
		_servicesRunner = P_3;
		_hostApplicationLifetime = P_4;
		_themeService = P_5;
		_browserService = P_6;
		_rootSessionAccessor = P_7;
		_applicationRestart = P_8;
		_localDataStore = P_9;
		_rootApplicationOptions = P_10;
		_linkJoiningService = P_11;
		_keybindingDispatchService = P_12;
		_mainViewModelFactory = P_0;
		configureTrayIcons();
		RootAppService.SetDefaults();
		P_2.AddSentry(delegate(SentryLoggingOptions o)
		{
			o.Dsn = "https://89f5d53859240a8c0100dfe7ac936753@o4509469920133120.ingest.us.sentry.io/4509470042554378";
			o.InitializeSdk = true;
			o.Debug = false;
			o.StackTraceMode = StackTraceMode.Enhanced;
			o.SetBeforeSend(delegate(SentryEvent sentryEvent, SentryHint _)
			{
				if (RootApp.Client.CoreDomain.Utils.Extensions.LoggerExtensions.IsExpectedFailure(sentryEvent.Exception))
				{
					return (SentryEvent?)null;
				}
				return SentryEventThrottle.ShouldThrottle(sentryEvent) ? null : sentryEvent;
			});
		});
		_logger = P_2.CreateLogger<App>();
	}

	public override void Initialize()
	{
		_logger.LogInformation("Initializing application");
		_synchronizationContext = SynchronizationContext.Current ?? throw new InvalidOperationException("No synchronization context found.");
		!XamlIlPopulateTrampoline(this);
		_themeService.InitializeTheme();
		DefaultMenuInteractionHandler.MenuShowDelay = TimeSpan.Zero;
		Dispatcher.UIThread.UnhandledException += delegate(object _, DispatcherUnhandledExceptionEventArgs e)
		{
			_logger.LogError(e.Exception, "UI Thread Unhandled Exception");
		};
	}

	public override void OnFrameworkInitializationCompleted()
	{
		base.OnFrameworkInitializationCompleted();
		global::Avalonia.Controls.ApplicationLifetimes.IApplicationLifetime applicationLifetime = base.ApplicationLifetime;
		global::Avalonia.Controls.ApplicationLifetimes.IApplicationLifetime applicationLifetime2 = applicationLifetime;
		IClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime = applicationLifetime2 as IClassicDesktopStyleApplicationLifetime;
		if (classicDesktopStyleApplicationLifetime == null)
		{
			ISingleViewApplicationLifetime singleViewApplicationLifetime = applicationLifetime2 as ISingleViewApplicationLifetime;
			if (singleViewApplicationLifetime == null)
			{
				_logger.LogCritical("Unsupported application type.");
			}
		}
		else
		{
			CancellationTokenRegistration cleanupRegistration = _hostApplicationLifetime.ApplicationStopping.Register(StoppingCallback, true);
			if (false)
			{
			}
			classicDesktopStyleApplicationLifetime.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			classicDesktopStyleApplicationLifetime.Exit += delegate
			{
				try
				{
					_logger.LogInformation("Shutting down application");
					_rootSessionAccessor.Session?.UserInfoService.SetDeviceOnlineStatusAsync(UserDeviceOnlineStatus.Disconnected).Forget();
					_servicesRunner.Stop();
					cleanupRegistration.Dispose();
					_openWindowTask.WaitAsync().Wait();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Application shutdown failed");
				}
			};
		}
		_logger.LogInformation("Starting {UserAgent} ({Instance}/{Environment})", _appVersion.UserAgent, _appVersion.Instance, _appVersion.RuntimeEnvironment);
		_logger.LogInformation("{Branch} {Commit}", _appVersion.Branch, _appVersion.Commit);
		_openWindowTask = TaskHandle.Run(OpenMainWindowAsync, _hostApplicationLifetime.ApplicationStopping);
		void StoppingCallback()
		{
			try
			{
				classicDesktopStyleApplicationLifetime.TryShutdown(1);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, "Application is already shutting down.");
			}
		}
	}

	private async Task OpenMainWindowAsync()
	{
		try
		{
			await _servicesRunner.WaitForStartedAsync();
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			_logger.LogError(ex2, "Application initialization failed");
			_hostApplicationLifetime.StopApplication();
			return;
		}
		_logger.LogInformation("Services have started");
		AwaitExtensions.SynchronizationContextAwaiter awaiter = _synchronizationContext.GetAwaiter();
		_ = awaiter.IsCompleted;
		await awaiter;
		AwaitExtensions.SynchronizationContextAwaiter synchronizationContextAwaiter = default(AwaitExtensions.SynchronizationContextAwaiter);
		awaiter = synchronizationContextAwaiter;
		awaiter.GetResult();
		try
		{
			MainViewModel context = _mainViewModelFactory.Create();
			global::Avalonia.Controls.ApplicationLifetimes.IApplicationLifetime applicationLifetime = base.ApplicationLifetime;
			global::Avalonia.Controls.ApplicationLifetimes.IApplicationLifetime applicationLifetime2 = applicationLifetime;
			if (!(applicationLifetime2 is IClassicDesktopStyleApplicationLifetime desktop))
			{
				if (applicationLifetime2 is ISingleViewApplicationLifetime singleViewPlatform)
				{
					singleViewPlatform.MainView = new MainView
					{
						DataContext = context
					};
				}
				else
				{
					_logger.LogCritical("Unsupported application type.");
				}
				return;
			}
			MainWindow window = (MainWindow)(desktop.MainWindow = new MainWindow(context, _localDataStore, _keybindingDispatchService));
			window.Show();
			string userProfile = _rootApplicationOptions.Value.UserProfile;
			string appId = "RootApp-" + userProfile;
			string sanitizedAppId = ActivationPipe.SanitizeForOsIdentifier(appId);
			ActivationPipe.StartServerLoop(ActivationPipe.PipeName(sanitizedAppId), delegate(string? launchId)
			{
				if (launchId != null)
				{
					_linkJoiningService.OpenLinkAsync(launchId).Forget();
				}
			}, _hostApplicationLifetime.ApplicationStopping);
			window.Closing += onWindowClosing;
		}
		catch (Exception ex)
		{
			Exception ex3 = ex;
			_logger.LogError(ex3, "Application startup failed");
			_hostApplicationLifetime.StopApplication();
		}
	}

	private void configureTrayIcons()
	{
		bool flag = true;
		if (!_localDataStore.TryGetGlobal(DataStoreKeys.CloseToTray, out int value))
		{
			value = 1;
		}
		_closeToTray = Convert.ToBoolean(value);
		if (_closeToTray)
		{
			TrayIcon trayIcon = new TrayIcon
			{
				Icon = (_rootApplicationOptions.Value.UserProfile.ToLower().Contains("staging") ? new WindowIcon("Resources/Assets/rooticonstaging.ico") : new WindowIcon("Resources/Assets/rooticon.ico")),
				ToolTipText = "Root",
				IsVisible = true
			};
			NativeMenu nativeMenu = new NativeMenu();
			NativeMenuItem nativeMenuItem = new NativeMenuItem(RootApp.Client.Avalonia.Resources.Strings.Resources.ShowRoot);
			nativeMenuItem.Click += onNativeMenuShowItemClick;
			trayIcon.Clicked += onNativeMenuShowItemClick;
			NativeMenuItemSeparator item = new NativeMenuItemSeparator();
			NativeMenuItem nativeMenuItem2 = new NativeMenuItem(RootApp.Client.Avalonia.Resources.Strings.Resources.Quit);
			nativeMenuItem2.Click += onNativeMenuExitClick;
			nativeMenu.Items.Add(nativeMenuItem);
			nativeMenu.Items.Add(item);
			nativeMenu.Items.Add(nativeMenuItem2);
			trayIcon.Menu = nativeMenu;
			TrayIcon.SetIcons(this, new TrayIcons { trayIcon });
		}
	}

	private void onWindowClosing(object? sender, WindowClosingEventArgs e)
	{
		if (!(base.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: MainWindow mainWindow }))
		{
			return;
		}
		mainWindow.SaveWindowState();
		bool flag = true;
		if (_closeToTray)
		{
			if (!_applicationRestart.ForceShutdown)
			{
				e.Cancel = true;
				mainWindow.HideToTray();
			}
		}
		else
		{
			_applicationRestart.RequestShutdown();
		}
	}

	private void onNativeMenuShowItemClick(object? sender, EventArgs e)
	{
		if (base.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: MainWindow mainWindow })
		{
			mainWindow.RestoreFromTray();
		}
	}

	private void onNativeMenuExitClick(object? sender, EventArgs e)
	{
		if (base.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: MainWindow })
		{
			_applicationRestart.RequestShutdown();
		}
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, App P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<App> context = new CompiledAvaloniaXaml.XamlIlContext.Context<App>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/App.axaml.Singleton }, "avares://RootApp.Client.Avalonia/App.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		App app2;
		App app = (app2 = P_1);
		context.PushParent(app2);
		app2.RequestedThemeVariant = ThemeVariant.Default;
		app2.Name = "Root";
		app2.DataTemplates.Add(new ViewLocator());
		NativeMenu.SetMenu(app2, new NativeMenu());
		ResourceDictionary resourceDictionary;
		ResourceDictionary resources = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		ResourceDictionary resourceDictionary2 = resourceDictionary;
		if (resourceDictionary2 is ResourceDictionary resourceDictionary3)
		{
			resourceDictionary3.EnsureCapacity(resourceDictionary3.Count + 26);
		}
		IDictionary<ThemeVariant, IThemeVariantProvider> themeDictionaries = resourceDictionary2.ThemeDictionaries;
		ThemeVariant light = ThemeVariant.Light;
		ResourceDictionary value = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		ResourceDictionary resourceDictionary4 = resourceDictionary;
		resourceDictionary4.MergedDictionaries.Add(!AvaloniaResources.Build:/Resources/Themes/Light.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		((IThemeVariantProvider)resourceDictionary4).Key = ThemeVariant.Light;
		context.PopParent();
		themeDictionaries.Add(light, value);
		IDictionary<ThemeVariant, IThemeVariantProvider> themeDictionaries2 = resourceDictionary2.ThemeDictionaries;
		ThemeVariant dark = ThemeVariant.Dark;
		ResourceDictionary value2 = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		ResourceDictionary resourceDictionary5 = resourceDictionary;
		resourceDictionary5.MergedDictionaries.Add(!AvaloniaResources.Build:/Resources/Themes/Dark.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		((IThemeVariantProvider)resourceDictionary5).Key = ThemeVariant.Dark;
		context.PopParent();
		themeDictionaries2.Add(dark, value2);
		IDictionary<ThemeVariant, IThemeVariantProvider> themeDictionaries3 = resourceDictionary2.ThemeDictionaries;
		ThemeVariant pureDark = ThemeMapper.PureDark;
		ResourceDictionary value3 = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		ResourceDictionary resourceDictionary6 = resourceDictionary;
		resourceDictionary6.MergedDictionaries.Add(!AvaloniaResources.Build:/Resources/Themes/PureDark.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		((IThemeVariantProvider)resourceDictionary6).Key = ThemeMapper.PureDark;
		context.PopParent();
		themeDictionaries3.Add(pureDark, value3);
		resourceDictionary2.MergedDictionaries.Add(!AvaloniaResources.Build:/Resources/Fonts.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		resourceDictionary2.MergedDictionaries.Add(!AvaloniaResources.Build:/Resources/Sounds.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		resourceDictionary2.AddDeferred("ScrollToVisibilityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_1), context));
		resourceDictionary2.AddDeferred("BoolInverterConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_2), context));
		resourceDictionary2.AddDeferred("MultiBoolInverterConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_3), context));
		resourceDictionary2.AddDeferred("GreaterThanZeroToTrueConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_4), context));
		resourceDictionary2.AddDeferred("StringNullOrEmptyToVisibilityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_5), context));
		resourceDictionary2.AddDeferred("StringFormatConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_6), context));
		resourceDictionary2.AddDeferred("EmptyStringFallbackConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_7), context));
		resourceDictionary2.AddDeferred("OnlineTextStatusConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_8), context));
		resourceDictionary2.AddDeferred("OnlineStatusToIndexConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_9), context));
		resourceDictionary2.AddDeferred("PrependStringConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_10), context));
		resourceDictionary2.AddDeferred("AndConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_11), context));
		resourceDictionary2.AddDeferred("OrConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_12), context));
		resourceDictionary2.AddDeferred("MediaRoomConnectionStatusToVisibleConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_13), context));
		resourceDictionary2.AddDeferred("CommunityMembersBoolToWidthConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_14), context));
		resourceDictionary2.AddDeferred("ByteSizeToReadableStringConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_15), context));
		resourceDictionary2.AddDeferred("MarginIfFalseConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_16), context));
		resourceDictionary2.AddDeferred("MarginIfTrueConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_17), context));
		resourceDictionary2.AddDeferred("PercentageToVisibilityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_18), context));
		resourceDictionary2.AddDeferred("VisibilityToOpacityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_19), context));
		resourceDictionary2.AddDeferred("BoolToOpacityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_20), context));
		resourceDictionary2.AddDeferred("BoolToTopBorderThicknessConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_21), context));
		resourceDictionary2.AddDeferred("PathToBitmapConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_22), context));
		resourceDictionary2.AddDeferred("ItemsControlHasItemsConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_23), context));
		resourceDictionary2.AddDeferred("ScrollViewerScrollableMarginConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_24), context));
		resourceDictionary2.AddDeferred("MenuItemToggleTypeConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_25), context));
		resourceDictionary2.AddDeferred("EnumToBoolConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_26), context));
		context.PopParent();
		app2.Resources = resources;
		app2.Styles.Add(new SimpleTheme(context));
		app2.Styles.Add(new MediaFluentTheme(context));
		Styles styles = app2.Styles;
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(TextBox));
		Setter setter = new Setter();
		setter.Property = Control.FocusAdornerProperty;
		setter.Value = null;
		style.Add(setter);
		styles.Add(style);
		Styles styles2 = app2.Styles;
		Style style2 = new Style();
		style2.Selector = ((Selector?)null).OfType(typeof(Button));
		Setter setter2 = new Setter();
		setter2.Property = Control.FocusAdornerProperty;
		setter2.Value = null;
		style2.Add(setter2);
		styles2.Add(style2);
		app2.Styles.Add(!AvaloniaResources.Build:/Themes/Simple/AvaloniaEdit.xaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/ColorPicker/RootColorPicker.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/BorderlessTextbox.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/BorderButton.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/TransparentButton.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/LinkButton.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/TextButton.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/ScrollViewer.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/ListBox.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/SvgButton.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/ListBoxItem.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/TabItem.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/CheckBox.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/MenuFlyoutPresenter.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/FlyoutPresenter.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/MenuItem.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/Separator.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/TabsTheme.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/RootImageLoader.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/ComboBox.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/ComboBoxItem.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/MessageMarkdown.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/DropDownButton.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/RootSplitView.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/ToolTip.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		app2.Styles.Add(!AvaloniaResources.Build:/Resources/Styles/Slider.axaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		context.PopParent();
		if (app is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void !XamlIlPopulateTrampoline(App P_0)
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
