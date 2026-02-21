using System;
using System.IO;
using System.Threading.Tasks;
using AvaloniaEdit.Utils;
using DotNetBrowser.Chromium;
using DotNetBrowser.Engine;
using DotNetBrowser.Handlers;
using DotNetBrowser.Net;
using DotNetBrowser.Net.Handlers;
using Microsoft.Extensions.Logging;
using RootApp.Client.Domain.Helpers.ApplicationConfiguration;

namespace RootApp.Browser;

public sealed class BrowserEngineManager : IDisposable
{
	private readonly IRootApplicationDirectories _rootApplicationDirectories;

	private readonly IHandler<InterceptRequestParameters, InterceptRequestResponse> _appInterceptHandler;

	private readonly ILogger<BrowserEngineManager> _logger;

	private IEngine? _engine;

	private Task? _engineCreationTask;

	public IEngine? Engine => _engine;

	public BrowserEngineManager(IRootApplicationDirectories P_0, IHandler<InterceptRequestParameters, InterceptRequestResponse> P_1, ILoggerFactory P_2)
	{
		_rootApplicationDirectories = P_0;
		_appInterceptHandler = P_1;
		_logger = P_2.CreateLogger<BrowserEngineManager>();
		base..ctor();
	}

	public void StartEngineCreation()
	{
		_engineCreationTask = Task.Run(async delegate
		{
			try
			{
				await CreateEngineAsync();
			}
			catch (ChromiumBinariesMissingException)
			{
				await CreateEngineAsync(true);
			}
		});
	}

	public async Task EnsureEngineReadyAsync(TimeSpan? P_0 = null)
	{
		Task engineCreationTask = _engineCreationTask;
		if (engineCreationTask != null && !engineCreationTask.IsCompleted)
		{
			await _engineCreationTask.WaitAsync(P_0 ?? TimeSpan.FromSeconds(30L));
		}
	}

	public async Task CreateEngineAsync(bool P_0 = false)
	{
		try
		{
			if (_engine == null)
			{
				string chromiumDirectory = Path.Combine(_rootApplicationDirectories.LocalDirectory.FullName, "DotNetBrowser");
				if (P_0 && Directory.Exists(chromiumDirectory))
				{
					Directory.Delete(chromiumDirectory, true);
				}
				EngineOptions.Builder builder = new EngineOptions.Builder
				{
					LicenseKey = "119PGCAVU9L7FCU4QRRMLMK2F2WCXFMLJ3QNP5XFZQXMNRDB3XHHEO8989AB7F10DWDKJOKIWG1MQ1QOVNN6PZRVC72S1MI8AKG1Y1N0ZXSW1MAFZNZFIQM7D0B0WF84HIKW81W1UXNMLFEVCW04KG2TTJRUSARM4X4F6S",
					RenderingMode = RenderingMode.HardwareAccelerated,
					ChromiumDirectory = chromiumDirectory,
					ProprietaryFeatures = (ProprietaryFeatures.H264 | ProprietaryFeatures.Hevc | ProprietaryFeatures.Aac),
					IncognitoEnabled = true,
					GpuDisabled = false,
					Schemes = 
					{
						{
							Scheme.Create("rootapp"),
							_appInterceptHandler
						},
						{
							Scheme.Create("root"),
							_appInterceptHandler
						},
						{
							Scheme.Create("avares"),
							_appInterceptHandler
						}
					}
				};
				ConfigureChromiumSwitches(builder);
				EngineOptions options = builder.Build();
				ChromiumBinariesExtractor binaryExtractor = new ChromiumBinariesExtractor();
				binaryExtractor.ExtractBinariesIfNecessary(options);
				_engine = await EngineFactory.CreateAsync(options);
				_engineCreationTask = null;
			}
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to create browser engine.");
			throw;
		}
	}

	private static void ConfigureChromiumSwitches(EngineOptions.Builder P_0)
	{
		bool flag = true;
		ConfigureWindowsSwitches(P_0);
	}

	private static void ConfigureWindowsSwitches(EngineOptions.Builder P_0)
	{
		P_0.ChromiumSwitches.AddRange(new global::<>z__ReadOnlyArray<string>(new string[13]
		{
			"--disable-web-security", "--ignore-gpu-blocklist", "--enable-gpu", "--enable-gpu-compositing", "--enable-gpu-rasterization", "--enable-zero-copy", "--enable-native-gpu-memory-buffers", "--enable-accelerated-video-decode", "--enable-accelerated-video-encode", "--use-angle=d3d11",
			"--use-gl=angle", "--autoplay-policy=no-user-gesture-required", "--enable-features=PlatformH264CbpEncoding"
		}));
	}

	public void Dispose()
	{
		_engine?.Dispose();
	}
}
