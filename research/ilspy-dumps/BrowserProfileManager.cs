using System;
using System.Threading.Tasks;
using DotNetBrowser.Browser;
using DotNetBrowser.Browser.Handlers;
using DotNetBrowser.Handlers;
using DotNetBrowser.Navigation.Handlers;
using DotNetBrowser.Net.Handlers;
using DotNetBrowser.Profile;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RootApp.Browser.Handlers;
using RootApp.Client.Domain.Helpers.ApplicationConfiguration;

namespace RootApp.Browser;

public sealed class BrowserProfileManager : IDisposable
{
	private readonly BrowserEngineManager _engineManager;

	private readonly DevicePermissionHandlerFactory _devicePermissionHandlerFactory;

	private readonly IHandler<StartTransactionParameters, StartTransactionResponse> _startTransactionHandler;

	private readonly IHandler<SendUrlRequestParameters, SendUrlRequestResponse> _networkRequestGuardHandler;

	private readonly IHandler<StartNavigationParameters, StartNavigationResponse> _navigationGuardHandler;

	private readonly IOptions<RootApplicationOptions> _rootApplicationOptions;

	private readonly BrowserAppearanceHelper _appearanceHelper;

	private readonly ILogger<BrowserProfileManager> _logger;

	private IProfile? _appProfile;

	private IProfile? _rawProfile;

	private IProfile? _webRtcProfile;

	public BrowserProfileManager(BrowserEngineManager P_0, DevicePermissionHandlerFactory P_1, IHandler<StartTransactionParameters, StartTransactionResponse> P_2, IHandler<SendUrlRequestParameters, SendUrlRequestResponse> P_3, IHandler<StartNavigationParameters, StartNavigationResponse> P_4, IOptions<RootApplicationOptions> P_5, BrowserAppearanceHelper P_6, ILoggerFactory P_7)
	{
		_engineManager = P_0;
		_devicePermissionHandlerFactory = P_1;
		_startTransactionHandler = P_2;
		_networkRequestGuardHandler = P_3;
		_navigationGuardHandler = P_4;
		_rootApplicationOptions = P_5;
		_appearanceHelper = P_6;
		_logger = P_7.CreateLogger<BrowserProfileManager>();
	}

	public async Task<IBrowser?> CreateWebRtcBrowserAsync()
	{
		try
		{
			await _engineManager.EnsureEngineReadyAsync();
			if (_engineManager.Engine == null)
			{
				return null;
			}
			if (_webRtcProfile == null)
			{
				_webRtcProfile = _engineManager.Engine.Profiles.Create("WebRTC", ProfileType.Incognito);
				_webRtcProfile.Network.StartTransactionHandler = _startTransactionHandler;
				_webRtcProfile.Permissions.RequestPermissionHandler = _devicePermissionHandlerFactory.Create(true);
			}
			IBrowser browser = _webRtcProfile.CreateBrowser();
			ConfigureWebRtcBrowser(browser);
			return browser;
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to create webrtc browser.");
			return null;
		}
	}

	public async Task<IBrowser?> CreateAppBrowserAsync()
	{
		try
		{
			await _engineManager.EnsureEngineReadyAsync();
			if (_engineManager.Engine == null)
			{
				return null;
			}
			if (_appProfile == null)
			{
				_appProfile = _engineManager.Engine.Profiles.Create("App", ProfileType.Incognito);
				_appProfile.Network.SendUrlRequestHandler = _networkRequestGuardHandler;
				_appProfile.Network.StartTransactionHandler = _startTransactionHandler;
				_appProfile.Permissions.RequestPermissionHandler = _devicePermissionHandlerFactory.Create(false);
			}
			IBrowser browser = _appProfile.CreateBrowser();
			ConfigureAppBrowser(browser);
			return browser;
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to create app browser.");
			return null;
		}
	}

	public async Task<IBrowser?> CreateRawBrowserAsync()
	{
		try
		{
			await _engineManager.EnsureEngineReadyAsync();
			if (_engineManager.Engine == null)
			{
				return null;
			}
			if (_rawProfile == null)
			{
				_rawProfile = _engineManager.Engine.Profiles.Create("Raw", ProfileType.Incognito);
			}
			IBrowser browser = _rawProfile.CreateBrowser();
			ConfigureRawBrowser(browser);
			return browser;
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to create raw browser.");
			return null;
		}
	}

	private void ConfigureWebRtcBrowser(IBrowser P_0)
	{
		P_0.UserAgent += " RootPlatform 1.0";
		P_0.UserAgent = P_0.UserAgent + " " + _rootApplicationOptions.Value.UserProfile;
		P_0.ShowContextMenuHandler = new AsyncHandler<ShowContextMenuParameters, ShowContextMenuResponse>(ContextMenuHandler.ShowContextMenuAsync);
		_appearanceHelper.SetBrowserBackgroundColor(P_0);
	}

	private void ConfigureAppBrowser(IBrowser P_0)
	{
		P_0.Navigation.StartNavigationHandler = _navigationGuardHandler;
		P_0.UserAgent += " RootSDK RootPlatform 1.0";
		P_0.ShowContextMenuHandler = new AsyncHandler<ShowContextMenuParameters, ShowContextMenuResponse>(ContextMenuHandler.ShowContextMenuAsync);
		_appearanceHelper.SetBrowserBackgroundColor(P_0);
	}

	private void ConfigureRawBrowser(IBrowser P_0)
	{
		P_0.UserAgent += " RootPlatform 1.0";
		P_0.ShowContextMenuHandler = new AsyncHandler<ShowContextMenuParameters, ShowContextMenuResponse>(ContextMenuHandler.ShowContextMenuAsync);
		_appearanceHelper.SetBrowserBackgroundColor(P_0);
	}

	public void Dispose()
	{
	}
}
