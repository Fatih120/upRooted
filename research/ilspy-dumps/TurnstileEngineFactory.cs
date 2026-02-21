using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DotNetBrowser.Chromium;
using DotNetBrowser.Engine;
using Microsoft.Extensions.Logging;
using RootApp.Client.Domain.Helpers.ApplicationConfiguration;

namespace RootApp.Browser.Turnstile;

public sealed class TurnstileEngineFactory
{
	private readonly IRootApplicationDirectories _rootApplicationDirectories;

	private readonly ILogger<TurnstileEngineFactory> _logger;

	private static int _instanceCounter;

	public TurnstileEngineFactory(IRootApplicationDirectories P_0, ILoggerFactory P_1)
	{
		_rootApplicationDirectories = P_0;
		_logger = P_1.CreateLogger<TurnstileEngineFactory>();
		base._002Ector();
	}

	public async Task<IEngine> CreateEngineAsync()
	{
		int instanceId = Interlocked.Increment(ref _instanceCounter);
		try
		{
			string chromiumDirectory = Path.Combine(_rootApplicationDirectories.LocalDirectory.FullName, "DotNetBrowser.Turnstile", $"Instance{instanceId}");
			if (Directory.Exists(chromiumDirectory))
			{
				try
				{
					Directory.Delete(chromiumDirectory, true);
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					_logger.LogWarning(ex2, "Failed to clean up old Turnstile engine directory: {Path}", chromiumDirectory);
				}
			}
			EngineOptions.Builder builder = new EngineOptions.Builder
			{
				LicenseKey = "119PGCAVU9L7FCU4QRRMLMK2F2WCXFMLJ3QNP5XFZQXMNRDB3XHHEO8989AB7F10DWDKJOKIWG1MQ1QOVNN6PZRVC72S1MI8AKG1Y1N0ZXSW1MAFZNZFIQM7D0B0WF84HIKW81W1UXNMLFEVCW04KG2TTJRUSARM4X4F6S",
				RenderingMode = RenderingMode.HardwareAccelerated,
				ChromiumDirectory = chromiumDirectory,
				GpuDisabled = false
			};
			ConfigureChromiumSwitches(builder);
			EngineOptions options = builder.Build();
			ChromiumBinariesExtractor binaryExtractor = new ChromiumBinariesExtractor();
			binaryExtractor.ExtractBinariesIfNecessary(options);
			IEngine engine = await EngineFactory.CreateAsync(options);
			_logger.LogDebug("Created Turnstile engine instance {InstanceId}", instanceId);
			return engine;
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogError(e, "Failed to create Turnstile engine instance {InstanceId}", instanceId);
			throw;
		}
	}

	private static void ConfigureChromiumSwitches(EngineOptions.Builder P_0)
	{
		if (0 == 0)
		{
		}
	}
}
