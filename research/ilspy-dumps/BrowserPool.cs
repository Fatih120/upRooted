using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNetBrowser.Browser;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;
using DotNetBrowser.Navigation;
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia.Helpers.RootApps.Core;

namespace RootApp.Browser;

public sealed class BrowserPool : IDisposable
{
	private readonly BrowserProfileManager _profileManager;

	private readonly BrowserEngineManager _engineManager;

	private readonly AppManager _appManager;

	private readonly ILogger<BrowserPool> _logger;

	private WarmBrowser? _warmBrowser;

	private Task? _warmingTask;

	private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

	private bool _disposed;

	public BrowserPool(BrowserProfileManager P_0, BrowserEngineManager P_1, AppManager P_2, ILoggerFactory P_3)
	{
		_profileManager = P_0;
		_engineManager = P_1;
		_appManager = P_2;
		_logger = P_3.CreateLogger<BrowserPool>();
	}

	public void StartWarming()
	{
		_logger.LogInformation("Browser pool: starting initial pre-warm");
		WarmAsync();
	}

	public async Task<WarmBrowser?> TryAcquireAsync()
	{
		await _lock.WaitAsync().ConfigureAwait(continueOnCapturedContext: false);
		try
		{
			WarmBrowser browser = _warmBrowser;
			_warmBrowser = null;
			if (browser != null)
			{
				_logger.LogDebug("Acquired pre-warmed browser from pool");
			}
			else
			{
				_logger.LogDebug("No pre-warmed browser available in pool");
			}
			return browser;
		}
		finally
		{
			_lock.Release();
			WarmAsync();
		}
	}

	private async Task WarmAsync()
	{
		if (_disposed)
		{
			return;
		}
		await _lock.WaitAsync().ConfigureAwait(continueOnCapturedContext: false);
		try
		{
			int num;
			if (!_disposed && _warmBrowser == null)
			{
				Task warmingTask = _warmingTask;
				num = ((warmingTask != null && !warmingTask.IsCompleted) ? 1 : 0);
			}
			else
			{
				num = 1;
			}
			if (num != 0)
			{
				return;
			}
			_warmingTask = WarmInternalAsync();
		}
		finally
		{
			_lock.Release();
		}
		await _warmingTask.ConfigureAwait(continueOnCapturedContext: false);
	}

	private async Task WarmInternalAsync()
	{
		try
		{
			_logger.LogDebug("Browser pool: warming a browser");
			await _appManager.EnsureInitializedAsync().ConfigureAwait(continueOnCapturedContext: false);
			IBrowser browser = await _profileManager.CreateAppBrowserAsync().ConfigureAwait(continueOnCapturedContext: false);
			if (browser == null)
			{
				_logger.LogWarning("Failed to create browser for pre-warming");
				return;
			}
			string url = new Uri("rootapp://app/__index.html").ToString();
			NavigationResult result = await browser.Navigation.LoadUrl(url).ConfigureAwait(continueOnCapturedContext: false);
			if (result.LoadResult != LoadResult.Completed)
			{
				_logger.LogWarning("Failed to load index.html during pre-warming: {Result}", result.LoadResult);
				try
				{
					browser.Dispose();
					return;
				}
				catch
				{
					return;
				}
			}
			await browser.MainFrame.ExecuteJavaScript("\r\n                document.addEventListener('dragover', function(e) {\r\n                    e.preventDefault();\r\n                }, false);\r\n                document.addEventListener('drop', function(e) {\r\n                    e.preventDefault();\r\n                }, false);\r\n            ").ConfigureAwait(continueOnCapturedContext: false);
			IJsObject window = await browser.MainFrame.ExecuteJavaScript<IJsObject>("window", userGesture: true).ConfigureAwait(continueOnCapturedContext: false);
			IFrame iframeEnv = browser.AllFrames.First((IFrame f) => !f.IsMain);
			await _lock.WaitAsync().ConfigureAwait(continueOnCapturedContext: false);
			try
			{
				if (_disposed)
				{
					try
					{
						browser.Dispose();
						return;
					}
					catch
					{
						return;
					}
				}
				_warmBrowser = new WarmBrowser(browser, window, iframeEnv);
				_logger.LogDebug("Browser pool: warm browser ready");
			}
			finally
			{
				_lock.Release();
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			_logger.LogError(ex2, "Error during browser pre-warming");
		}
	}

	public void Dispose()
	{
		_disposed = true;
		_warmBrowser?.Dispose();
		_warmBrowser = null;
		_lock.Dispose();
	}
}
