using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using DotNetBrowser.Browser;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RootApp.Browser.Turnstile;
using RootApp.Client.Avalonia.Helpers.RootApps.Core;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Core;
using RootApp.Core.Identifiers;
using RootApp.Utility;
using RootApp.WebApi.Client.Shared;

namespace RootApp.Browser;

public sealed class BrowserService : IDisposable
{
	private readonly BrowserEngineManager _engineManager;

	private readonly BrowserProfileManager _profileManager;

	private readonly BrowserRegistry _registry;

	private readonly BrowserPool _browserPool;

	private readonly RootWebApiConfig _options;

	private readonly WebRtcBrowserFactory _webRtcBrowserFactory;

	private readonly DeviceBrowserFactory _deviceBrowserFactory;

	private readonly RootAppBrowserFactory _rootAppBrowserFactory;

	private readonly TurnstileBrowserFactory _turnstileBrowserFactory;

	private readonly AppManager _appManager;

	private readonly IConnectionStatusService _connectionStatusService;

	private readonly ILogger<BrowserService> _logger;

	private readonly SemaphoreSlim _webRtcCreationLock = new SemaphoreSlim(1, 1);

	public BrowserService(IOptions<RootWebApiConfig> P_0, BrowserEngineManager P_1, BrowserProfileManager P_2, BrowserRegistry P_3, BrowserPool P_4, WebRtcBrowserFactory P_5, DeviceBrowserFactory P_6, RootAppBrowserFactory P_7, TurnstileBrowserFactory P_8, AppManager P_9, IConnectionStatusService P_10, IApplicationRestart P_11, ILoggerFactory P_12)
	{
		_options = P_0.Value;
		_engineManager = P_1;
		_profileManager = P_2;
		_registry = P_3;
		_browserPool = P_4;
		_webRtcBrowserFactory = P_5;
		_deviceBrowserFactory = P_6;
		_rootAppBrowserFactory = P_7;
		_turnstileBrowserFactory = P_8;
		_appManager = P_9;
		_connectionStatusService = P_10;
		_logger = P_12.CreateLogger<BrowserService>();
		WeakReferenceMessenger.Default.Register<RemoveBrowserMessage>(this, OnRemoveBrowserMessageReceived);
		_connectionStatusService.PropertyChanged += OnConnectionStatusServicePropertyChanged;
		P_11.RegisterShutdownHandler(CleanupAsync);
		_engineManager.StartEngineCreation();
		_browserPool.StartWarming();
	}

	private Task CleanupAsync()
	{
		_logger.LogInformation("Browser shutdown handler - cleaning up active browsers");
		_registry.RemoveAll<WebRtcBrowser>();
		return Task.CompletedTask;
	}

	public async Task<IBrowser> CreateRawBrowserAsync()
	{
		IBrowser browser = await _profileManager.CreateRawBrowserAsync();
		if (browser != null)
		{
			return browser;
		}
		throw new Exception("Failed to create raw browser.");
	}

	public async Task<WebRtcBrowser?> CreateWebRtcBrowserAsync(RootGuid P_0, MediaRoom P_1)
	{
		await _webRtcCreationLock.WaitAsync();
		try
		{
			if (_registry.Contains(P_0))
			{
				RemoveBrowser(P_0);
				await Task.Delay(300);
			}
			IBrowser dotNetBrowser = await _profileManager.CreateWebRtcBrowserAsync();
			if (dotNetBrowser != null)
			{
				WebRtcBrowser webRtcBrowser = _webRtcBrowserFactory.Create(P_0, P_1, dotNetBrowser, _options.WebApiUrl.ToString(), this);
				await webRtcBrowser.InitializeAsync();
				_registry.AddOrReplace(P_0, webRtcBrowser);
				return webRtcBrowser;
			}
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to create webrtc browser.");
		}
		finally
		{
			_webRtcCreationLock.Release();
		}
		return null;
	}

	public async Task<RootAppBrowser?> CreateRootAppBrowserAsync(ChannelGuid P_0, AppGuid P_1, string P_2, CommunityAppGuid P_3, CommunityGuid P_4)
	{
		try
		{
			await _appManager.EnsureInitializedAsync().ConfigureAwait(continueOnCapturedContext: false);
			if (_registry.Contains(P_3))
			{
				RemoveBrowser(P_3);
				await Task.Delay(300);
			}
			WarmBrowser warmBrowser = await _browserPool.TryAcquireAsync().ConfigureAwait(continueOnCapturedContext: false);
			RootAppBrowser rootAppBrowser;
			if (warmBrowser != null)
			{
				_logger.LogDebug("Using pre-warmed browser for app {AppId}", P_1);
				rootAppBrowser = _rootAppBrowserFactory.CreateFromWarm(P_1, P_2, P_3, P_4, warmBrowser);
			}
			else
			{
				_logger.LogDebug("No pre-warmed browser available, creating cold browser for app {AppId}", P_1);
				IBrowser dotNetBrowser = await _profileManager.CreateAppBrowserAsync();
				if (dotNetBrowser == null)
				{
					return null;
				}
				rootAppBrowser = _rootAppBrowserFactory.Create(P_1, P_2, P_3, P_4, dotNetBrowser);
			}
			await rootAppBrowser.InitializeAsync();
			_registry.Add(P_0, rootAppBrowser);
			return rootAppBrowser;
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to create root app browser.");
		}
		return null;
	}

	public async Task<DeviceBrowser?> CreateDeviceBrowserAsync()
	{
		try
		{
			IBrowser dotNetBrowser = await _profileManager.CreateWebRtcBrowserAsync();
			if (dotNetBrowser != null)
			{
				DeviceBrowser deviceBrowser = _deviceBrowserFactory.Create(RootGuid.Empty, dotNetBrowser);
				await deviceBrowser.InitializeAsync();
				_registry.Add(RootGuid.Empty, deviceBrowser);
				return deviceBrowser;
			}
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to create device browser.");
		}
		return null;
	}

	public async Task<TurnstileBrowser?> CreateTurnstileBrowserAsync(string P_0, CancellationToken P_1 = default(CancellationToken))
	{
		try
		{
			RootGuid id = new RootGuid(DateTime.UtcNow);
			TurnstileBrowser turnstileBrowser = await _turnstileBrowserFactory.CreateWithEngineAsync(id, P_0);
			await turnstileBrowser.InitializeAsync(P_1);
			_registry.Add(id, turnstileBrowser);
			return turnstileBrowser;
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to create Turnstile browser.");
		}
		return null;
	}

	public void RemoveTurnstileBrowser(RootGuid P_0)
	{
		_registry.Remove(P_0);
	}

	public WebRtcBrowser? GetWebRtcBrowser(RootGuid P_0)
	{
		return _registry.Get<WebRtcBrowser>(P_0);
	}

	public WebRtcBrowser? GetFirstWebRtcBrowser()
	{
		return _registry.GetFirst<WebRtcBrowser>();
	}

	public IRootBrowser? GetBrowser(RootGuid P_0)
	{
		return _registry.Get(P_0);
	}

	public void RemoveBrowser(RootGuid P_0)
	{
		_registry.Remove(P_0);
	}

	public void RemoveAllAppBrowsersForCommunity(CommunityGuid P_0)
	{
		_registry.RemoveAllAppBrowsersForCommunity(P_0);
	}

	private void OnRemoveBrowserMessageReceived(object recipient, RemoveBrowserMessage message)
	{
		RemoveBrowser(message.BrowserId);
	}

	private void OnConnectionStatusServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "IsConnectedToHubServer" && !_connectionStatusService.IsConnectedToHubServer)
			{
				_registry.RemoveAll<WebRtcBrowser>();
			}
		});
	}

	public void Dispose()
	{
		_webRtcCreationLock.Dispose();
		_registry.Dispose();
		_browserPool.Dispose();
		WeakReferenceMessenger.Default.Unregister<RemoveBrowserMessage>(this);
		_connectionStatusService.PropertyChanged -= OnConnectionStatusServicePropertyChanged;
		_profileManager.Dispose();
		_engineManager.Dispose();
	}
}
